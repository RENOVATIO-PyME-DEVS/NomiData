using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NomiData
{
    public partial class FrmEjecucionScript : Form
    {
        public FrmEjecucionScript()
        {
            InitializeComponent();
        }

        private void btnEjecutarSP_Click(object sender, EventArgs e)
        {
            // TODO: Ejecutar SP y llenar dgvResultados
            // TODO: Configurar la conexión a la base de datos y ejecutar el stored procedure con parámetros.
        }

        private void btnEjecutarLibre_Click(object sender, EventArgs e)
        {
            // TODO: Validar sentencia y ejecutar SELECT
            // TODO: Implementar validaciones para evitar DELETE, DROP, ALTER o TRUNCATE antes de ejecutar el script.
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            // TODO: Exportar dgvResultados a Excel o CSV
            // TODO: Implementar la lógica de exportación utilizando los datos del DataGridView.
        }
    }
}
