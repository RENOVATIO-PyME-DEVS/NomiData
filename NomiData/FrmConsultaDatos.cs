using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NomiData
{
    public partial class FrmConsultaDatos : Form
    {
        private readonly string connectionString;
        private readonly string databaseName;
        private string queryBase;

        public FrmConsultaDatos(string connectionString, string databaseName)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            queryBase = "SELECT TOP (100) * FROM sys.tables ORDER BY name;";
        }

        public string QueryBase
        {
            get => queryBase;
            set => queryBase = value ?? string.Empty;
        }

        private void FrmConsultaDatos_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(databaseName))
            {
                lblBaseSeleccionada.Text = $"Base seleccionada: {databaseName}";
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(queryBase))
            {
                MessageBox.Show("La consulta base no está configurada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(queryBase, connection))
                {
                    command.CommandType = CommandType.Text;

                    AddParameterIfNeeded(command, "@Param1", txtParam1.Text);
                    AddParameterIfNeeded(command, "@Param2", txtParam2.Text);
                    AddParameterIfNeeded(command, "@Param3", txtParam3.Text);

                    var adapter = new SqlDataAdapter(command);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvResultados.DataSource = table;

                    MessageBox.Show($"Consulta ejecutada. Registros obtenidos: {table.Rows.Count}.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AddParameterIfNeeded(SqlCommand command, string parameterName, string value)
        {
            if (command.CommandText.IndexOf(parameterName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var parameterValue = string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
                command.Parameters.AddWithValue(parameterName, parameterValue);
            }
        }
    }
}
