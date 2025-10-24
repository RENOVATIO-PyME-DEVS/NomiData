using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NomiData
{
    public partial class FrmEjecucionScript : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia del formulario y prepara los controles de la interfaz.
        /// </summary>
        public FrmEjecucionScript()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa una nueva instancia del formulario permitiendo especificar datos de conexión.
        /// </summary>
        /// <param name="baseConnectionString">Cadena de conexión base al servidor SQL.</param>
        /// <param name="databaseName">Nombre de la base de datos seleccionada.</param>
        /// <param name="storedProcedureScript">Script o nombre del stored procedure que se ejecutará.</param>
        public FrmEjecucionScript(string baseConnectionString, string databaseName, string storedProcedureScript = "")
            : this()
        {
            // TODO: Utilizar baseConnectionString, databaseName y storedProcedureScript para configurar la ejecución SQL.
        }

        private void btnEjecutarSP_Click(object sender, EventArgs e)
        {
            // TODO: Agregar la lógica para ejecutar el stored procedure usando System.Data.SqlClient.
        }

        private void btnEjecutarLibre_Click(object sender, EventArgs e)
        {
            // TODO: Agregar la lógica para ejecutar scripts libres utilizando System.Data.SqlClient.
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            // TODO: Agregar la lógica para exportar los resultados a Excel o CSV.
        }
    }
}
