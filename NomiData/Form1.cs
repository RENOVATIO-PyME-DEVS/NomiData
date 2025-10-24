using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NomiData
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Conserva la cadena de conexión generada al conectarse al servidor.
        /// Permite reutilizarla al momento de abrir el formulario de ejecución de scripts.
        /// </summary>
        private string baseConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Click del botón de conexión.
        /// Establece la conexión con SQL Server y carga las bases de datos.
        /// </summary>
        private void btnConectar_Click(object sender, EventArgs e)
        {
            // Validar los datos mínimos necesarios.
            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                MessageBox.Show("Ingrese el nombre del servidor.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServidor.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese el usuario.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            // Preparar la cadena de conexión a SQL Server.
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = txtServidor.Text.Trim(),
                UserID = txtUsuario.Text.Trim(),
                Password = txtContrasena.Text,
                InitialCatalog = "master",          // Usamos la base master para enumerar las demás bases.
                IntegratedSecurity = false,
                TrustServerCertificate = true,
                Encrypt = false
            };

            try
            {
                using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
                {
                    connection.Open();

                    // Consulta para traer todas las bases de datos del servidor.
                    using (var command = new SqlCommand("SELECT name FROM sys.databases", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var bases = new List<string>();

                        while (reader.Read())
                        {
                            var nombreBase = reader.GetString(0);

                            // Filtrar bases que contengan palabras restringidas (sin importar mayúsculas/minúsculas).
                            if (nombreBase.IndexOf("document", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                nombreBase.IndexOf("other", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                continue;
                            }

                            bases.Add(nombreBase);
                        }

                        // Insertar una opción vacía para permitir limpiar la selección.
                        bases.Insert(0, string.Empty);

                        // Vincular los nombres obtenidos al ComboBox.
                        cmbBasesDatos.DataSource = bases;
                        cmbBasesDatos.SelectedIndex = 0;
                    }
                }

                // Conservar la cadena de conexión utilizada para poder reabrirla posteriormente.
                baseConnectionString = connectionStringBuilder.ToString();

                MessageBox.Show("Conexión exitosa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Mostrar cualquier error durante la conexión o la consulta.
                MessageBox.Show(ex.Message, "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el cambio de selección en el ComboBox de bases de datos.
        /// Muestra u oculta el botón de ingreso según exista una base seleccionada.
        /// </summary>
        private void cmbBasesDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Determinar si la selección es válida (no nula ni vacía).
            var baseSeleccionada = cmbBasesDatos.SelectedItem as string;
            var haySeleccion = !string.IsNullOrWhiteSpace(baseSeleccionada);

            // Mostrar u ocultar el botón dinámicamente.
            btnIngresar.Visible = haySeleccion;
        }

        /// <summary>
        /// Abre el formulario de ejecución de scripts validando la conexión a la base seleccionada.
        /// </summary>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validar que exista una cadena de conexión configurada.
            if (string.IsNullOrWhiteSpace(baseConnectionString))
            {
                MessageBox.Show("Debe conectarse al servidor antes de ingresar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que se haya seleccionado una base de datos.
            var baseSeleccionada = cmbBasesDatos.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(baseSeleccionada))
            {
                MessageBox.Show("Seleccione una base de datos para continuar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Probar la conexión a la base de datos seleccionada antes de abrir el nuevo formulario.
                var builder = new SqlConnectionStringBuilder(baseConnectionString)
                {
                    InitialCatalog = baseSeleccionada
                };

                using (var connection = new SqlConnection(builder.ToString()))
                {
                    connection.Open();
                }

                // Abrir el formulario de ejecución de scripts enviando la cadena de conexión base y la base seleccionada.
                var formulario = new FrmEjecucionScript(baseConnectionString, baseSeleccionada)
                {
                    // Definir un stored procedure de ejemplo a ejecutar desde el nuevo formulario.
                    ScriptStoredProcedure = "sp_MiStoredProcedure"
                };

                formulario.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al validar la base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
