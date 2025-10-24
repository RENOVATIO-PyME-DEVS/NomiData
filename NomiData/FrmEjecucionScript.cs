using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NomiData
{
    /// <summary>
    /// Formulario responsable de ejecutar scripts SQL ya definidos o capturados por el usuario.
    /// </summary>
    public class FrmEjecucionScript : Form
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        // Variable interna donde se almacena el comando que invoca al stored procedure.
        private string scriptStoredProcedure;

        // Controles para la captura de parámetros del stored procedure.
        private TextBox txtParam1;
        private TextBox txtParam2;
        private TextBox txtParam3;
        private Button btnEjecutarSP;

        // Controles para la captura de scripts libres.
        private RichTextBox txtScriptLibre;
        private Button btnEjecutarLibre;
        private Button btnExportarExcel;

        private DataGridView dgvResultados;

        private Label lblBaseDatosSeleccionada;

        private static readonly string[] PalabrasRestringidas = { "DELETE", "DROP", "ALTER", "TRUNCATE" };

        /// <summary>
        /// Inicializa una nueva instancia del formulario indicando la conexión y la base de datos seleccionada.
        /// </summary>
        /// <param name="baseConnectionString">Cadena de conexión base al servidor SQL.</param>
        /// <param name="databaseName">Nombre de la base de datos seleccionada por el usuario.</param>
        /// <param name="storedProcedureScript">Script o nombre del stored procedure a ejecutar.</param>
        public FrmEjecucionScript(string baseConnectionString, string databaseName, string storedProcedureScript = "")
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString))
            {
                throw new ArgumentException("La cadena de conexión no puede ser nula o vacía.", nameof(baseConnectionString));
            }

            _databaseName = databaseName;
            _connectionString = ConstruirCadenaConexion(baseConnectionString, databaseName);
            scriptStoredProcedure = storedProcedureScript ?? string.Empty;

            InitializeComponent();
        }

        /// <summary>
        /// Permite establecer o recuperar el script usado para el stored procedure.
        /// </summary>
        public string ScriptStoredProcedure
        {
            get => scriptStoredProcedure;
            set => scriptStoredProcedure = value ?? string.Empty;
        }

        /// <summary>
        /// Configura el diseño de los controles del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            Text = "Ejecución de scripts";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;

            lblBaseDatosSeleccionada = new Label
            {
                AutoSize = true,
                Location = new Point(15, 15),
                Text = string.IsNullOrWhiteSpace(_databaseName)
                    ? "Base de datos no especificada"
                    : $"Base de datos seleccionada: {_databaseName}"
            };

            var grpStoredProcedure = new GroupBox
            {
                Text = "Ejecución de stored procedure",
                Location = new Point(12, 45),
                Size = new Size(860, 180)
            };

            var lblParam1 = new Label
            {
                AutoSize = true,
                Location = new Point(20, 35),
                Text = "Parámetro 1"
            };

            txtParam1 = new TextBox
            {
                Name = "txtParam1",
                Location = new Point(120, 32),
                Width = 250
            };

            var lblParam2 = new Label
            {
                AutoSize = true,
                Location = new Point(20, 75),
                Text = "Parámetro 2"
            };

            txtParam2 = new TextBox
            {
                Name = "txtParam2",
                Location = new Point(120, 72),
                Width = 250
            };

            var lblParam3 = new Label
            {
                AutoSize = true,
                Location = new Point(20, 115),
                Text = "Parámetro 3"
            };

            txtParam3 = new TextBox
            {
                Name = "txtParam3",
                Location = new Point(120, 112),
                Width = 250
            };

            btnEjecutarSP = new Button
            {
                Name = "btnEjecutarSP",
                Text = "Ejecutar stored procedure",
                Location = new Point(400, 70),
                Size = new Size(200, 35)
            };
            btnEjecutarSP.Click += btnEjecutarSP_Click;

            grpStoredProcedure.Controls.Add(lblParam1);
            grpStoredProcedure.Controls.Add(txtParam1);
            grpStoredProcedure.Controls.Add(lblParam2);
            grpStoredProcedure.Controls.Add(txtParam2);
            grpStoredProcedure.Controls.Add(lblParam3);
            grpStoredProcedure.Controls.Add(txtParam3);
            grpStoredProcedure.Controls.Add(btnEjecutarSP);

            var grpLibre = new GroupBox
            {
                Text = "Ejecución de script libre",
                Location = new Point(12, 240),
                Size = new Size(860, 320)
            };

            txtScriptLibre = new RichTextBox
            {
                Name = "txtScriptLibre",
                Location = new Point(20, 30),
                Size = new Size(820, 180),
                DetectUrls = false
            };

            btnEjecutarLibre = new Button
            {
                Name = "btnEjecutarLibre",
                Text = "Ejecutar script libre",
                Location = new Point(20, 220),
                Size = new Size(200, 35)
            };
            btnEjecutarLibre.Click += btnEjecutarLibre_Click;

            btnExportarExcel = new Button
            {
                Name = "btnExportarExcel",
                Text = "Exportar resultados",
                Location = new Point(240, 220),
                Size = new Size(200, 35)
            };
            btnExportarExcel.Click += btnExportarExcel_Click;

            dgvResultados = new DataGridView
            {
                Name = "dgvResultados",
                Location = new Point(20, 260),
                Size = new Size(820, 50),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            };

            grpLibre.Controls.Add(txtScriptLibre);
            grpLibre.Controls.Add(btnEjecutarLibre);
            grpLibre.Controls.Add(btnExportarExcel);
            grpLibre.Controls.Add(dgvResultados);

            Controls.Add(lblBaseDatosSeleccionada);
            Controls.Add(grpStoredProcedure);
            Controls.Add(grpLibre);
        }

        /// <summary>
        /// Maneja la ejecución del stored procedure definido en <see cref="scriptStoredProcedure"/>.
        /// </summary>
        private void btnEjecutarSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(scriptStoredProcedure))
            {
                MessageBox.Show("Debe definir el script del stored procedure antes de ejecutar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var parametros = new List<SqlParameter>
            {
                CrearParametro("@param1", txtParam1.Text),
                CrearParametro("@param2", txtParam2.Text),
                CrearParametro("@param3", txtParam3.Text)
            };

            var resultado = EjecutarConsulta(scriptStoredProcedure, parametros);
            MostrarResultados(resultado);
        }

        /// <summary>
        /// Maneja la ejecución de scripts libres escritos por el usuario.
        /// </summary>
        private void btnEjecutarLibre_Click(object sender, EventArgs e)
        {
            var scriptLibre = txtScriptLibre.Text;

            if (string.IsNullOrWhiteSpace(scriptLibre))
            {
                MessageBox.Show("Debe ingresar un script para ejecutar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ContienePalabrasRestringidas(scriptLibre))
            {
                MessageBox.Show("Operación no permitida", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = EjecutarConsulta(scriptLibre);
            MostrarResultados(resultado);
        }

        /// <summary>
        /// Ejecuta una consulta SQL contra la base de datos seleccionada y devuelve un DataTable con los resultados.
        /// </summary>
        /// <param name="query">Texto del comando o nombre del stored procedure.</param>
        /// <param name="parametros">Parámetros opcionales a enviar.</param>
        public DataTable EjecutarConsulta(string query, List<SqlParameter> parametros = null)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("El comando SQL no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = DeterminarTipoComando(query, parametros);

                    if (parametros != null && parametros.Any())
                    {
                        cmd.Parameters.AddRange(parametros.ToArray());
                    }

                    using (var adaptador = new SqlDataAdapter(cmd))
                    {
                        var dataTable = new DataTable();
                        adaptador.Fill(dataTable);

                        MessageBox.Show("Consulta ejecutada correctamente.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Muestra los resultados de la consulta en el DataGridView.
        /// </summary>
        /// <param name="dt">Tabla con los datos obtenidos.</param>
        private void MostrarResultados(DataTable dt)
        {
            dgvResultados.DataSource = null;

            if (dt == null)
            {
                MessageBox.Show("No se devolvieron resultados.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvResultados.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No se devolvieron resultados.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Exporta a un archivo CSV los resultados mostrados en el DataGridView.
        /// </summary>
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (!(dgvResultados.DataSource is DataTable dataTable) || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay resultados para exportar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"Resultados_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            })
            {
                if (dialogo.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    using (var writer = new StreamWriter(dialogo.FileName))
                    {
                        // Escribir encabezados
                        var columnas = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                        writer.WriteLine(string.Join(",", columnas));

                        // Escribir filas
                        foreach (DataRow fila in dataTable.Rows)
                        {
                            var valores = fila.ItemArray.Select(valor =>
                            {
                                var texto = valor?.ToString() ?? string.Empty;
                                // Escapar comillas y separar valores
                                if (texto.Contains("\""))
                                {
                                    texto = texto.Replace("\"", "\"\"");
                                }

                                if (texto.Contains(",") || texto.Contains("\n"))
                                {
                                    texto = $"\"{texto}\"";
                                }

                                return texto;
                            });

                            writer.WriteLine(string.Join(",", valores));
                        }
                    }

                    MessageBox.Show("Exportación completada correctamente.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Determina el tipo de comando a ejecutar.
        /// </summary>
        private static CommandType DeterminarTipoComando(string comando, List<SqlParameter> parametros)
        {
            var texto = comando?.Trim() ?? string.Empty;

            if (!string.IsNullOrEmpty(texto) && parametros != null && parametros.Any())
            {
                // Si el script parece ser únicamente el nombre del stored procedure, se ejecuta como tal.
                if (!texto.Contains(" ") && !texto.Contains("\n") && !texto.Contains("\r"))
                {
                    return CommandType.StoredProcedure;
                }
            }

            return CommandType.Text;
        }

        /// <summary>
        /// Crea un parámetro SQL asignando DBNull.Value cuando el valor está vacío.
        /// </summary>
        private static SqlParameter CrearParametro(string nombre, string valor)
        {
            return new SqlParameter(nombre, string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor);
        }

        /// <summary>
        /// Verifica si el script contiene palabras reservadas no permitidas.
        /// </summary>
        private static bool ContienePalabrasRestringidas(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
            {
                return false;
            }

            var mayusculas = script.ToUpperInvariant();
            return PalabrasRestringidas.Any(palabra => mayusculas.Contains(palabra));
        }

        /// <summary>
        /// Construye la cadena de conexión asegurando que se utilice la base de datos seleccionada.
        /// </summary>
        private static string ConstruirCadenaConexion(string baseConnectionString, string databaseName)
        {
            var builder = new SqlConnectionStringBuilder(baseConnectionString);

            if (!string.IsNullOrWhiteSpace(databaseName))
            {
                builder.InitialCatalog = databaseName;
            }

            return builder.ConnectionString;
        }
    }
}

