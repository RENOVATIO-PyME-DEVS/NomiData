using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                Size = new Size(860, 300)
            };

            txtScriptLibre = new RichTextBox
            {
                Name = "txtScriptLibre",
                Location = new Point(20, 30),
                Size = new Size(820, 200),
                DetectUrls = false
            };

            btnEjecutarLibre = new Button
            {
                Name = "btnEjecutarLibre",
                Text = "Ejecutar script libre",
                Location = new Point(20, 240),
                Size = new Size(200, 35)
            };
            btnEjecutarLibre.Click += btnEjecutarLibre_Click;

            grpLibre.Controls.Add(txtScriptLibre);
            grpLibre.Controls.Add(btnEjecutarLibre);

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
                CrearParametro("@Param1", txtParam1.Text),
                CrearParametro("@Param2", txtParam2.Text),
                CrearParametro("@Param3", txtParam3.Text)
            };

            EjecutarComandoSQL(scriptStoredProcedure, parametros);
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

            EjecutarComandoSQL(scriptLibre);
        }

        /// <summary>
        /// Ejecuta un comando SQL contra la base de datos seleccionada.
        /// </summary>
        /// <param name="comando">Texto del comando o nombre del stored procedure.</param>
        /// <param name="parametros">Parámetros opcionales a enviar.</param>
        public void EjecutarComandoSQL(string comando, List<SqlParameter> parametros = null)
        {
            if (string.IsNullOrWhiteSpace(comando))
            {
                MessageBox.Show("El comando SQL no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(comando, conexion))
                {
                    cmd.CommandType = DeterminarTipoComando(comando, parametros);

                    if (parametros != null && parametros.Any())
                    {
                        cmd.Parameters.AddRange(parametros.ToArray());
                    }

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ejecución correcta", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

