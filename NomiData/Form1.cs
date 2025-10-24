using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NomiData
{
    public partial class Form1 : Form
    {
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

                            // Filtrar bases que contengan la palabra "directorio" (sin importar mayúsculas/minúsculas).
                            if (nombreBase.IndexOf("directorio", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                continue;
                            }

                            bases.Add(nombreBase);
                        }

                        // Vincular los nombres obtenidos al ComboBox.
                        cmbBasesDatos.DataSource = bases;
                    }
                }

                MessageBox.Show("Conexión exitosa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Mostrar cualquier error durante la conexión o la consulta.
                MessageBox.Show(ex.Message, "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
