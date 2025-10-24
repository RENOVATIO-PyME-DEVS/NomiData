using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NomiData
{
    public partial class FrmMenu : Form
    {
        private readonly string _connectionString;
        DataTable dtER = new DataTable();
        public FrmMenu(string connectionString)
        {
            InitializeComponent();

            _connectionString = connectionString;

            // SQL por defecto
            txtSql.Text = "SELECT TOP (100) name, type_desc FROM sys.objects ORDER BY create_date DESC;";
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {

        }

        //private async Task EjecutarAsync()
        //{
        //    btnEjecutar.Enabled = false;
        //    lblInfo.Text = "Ejecutando...";

        //    try
        //    {
        //        var sql = txtSql.Text;

        //        // ¿Soportar scripts con GO?
        //        var partes = chkSplitGO.Checked
        //            ? Regex.Split(sql, @"^\s*GO\s*;?\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase)
        //                   .Where(p => !string.IsNullOrWhiteSpace(p)).ToArray()
        //            : new[] { sql };
              
        //        DataTable ultimo = null;
        //        using (var cn = new SqlConnection(_connectionString))
        //        {
        //            await cn.OpenAsync();

        //            foreach (var bloque in partes)
        //            {
        //                var cmd = new SqlCommand(bloque, cn)
        //                {
        //                    CommandTimeout = 0 // sin límite; ajusta si quieres
        //                };

        //                // Intento: si devuelve filas, llenar DataTable; si no, ExecuteNonQuery
        //                var da = new SqlDataAdapter(cmd);
        //                var dt = new DataTable();
        //                var llenadas = da.Fill(dt);

        //                if (dt.Columns.Count > 0) // hubo result set
        //                {
        //                    ultimo = dt; // nos quedamos con el último conjunto para mostrar
        //                }
        //                else
        //                {
        //                    await cmd.ExecuteNonQueryAsync();
        //                }
        //            }
        //        }

        //        if (ultimo != null)
        //        {
        //            grid.DataSource = ultimo;
        //            lblInfo.Text = $"Filas: {ultimo.Rows.Count:N0}  ·  Columnas: {ultimo.Columns.Count}";
        //        }
        //        else
        //        {
        //            grid.DataSource = null;
        //            lblInfo.Text = "Script ejecutado. (Sin resultados para mostrar)";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error al ejecutar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        lblInfo.Text = "Error";
        //    }
        //    finally
        //    {
        //        btnEjecutar.Enabled = true;
        //    }
        //}

        private async Task EjecutarScriptNominas()
        {
            try
            {
                string script = File.ReadAllText(@"Scripts\reporte_nominas.sql");

                using (var cn = new SqlConnection(_connectionString))
                {
                    await cn.OpenAsync();

                    var cmd = new SqlCommand(script, cn);
                    cmd.CommandTimeout = 0; // sin límite
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    grid.DataSource = dt;
                    dtER = dt;
                    lblInfo.Text = $"Filas: {dt.Rows.Count}, Columnas: {dt.Columns.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ejecutando script: " + ex.Message);
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            Task task = EjecutarScriptNominas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaIniSeleccionada = DateTime.Now;

            string path = @"C:\RENOVATIO\NOMIDATA\";
            //MessageBox.Show(dataGridView2.Rows.Count.ToString());
            if (dtER.Rows.Count <= 0)
            {
                MessageBox.Show("Nada por exportar.", "RENOVATIO PyME");
            }
            else
            {
                DataTable dt = dtER;


                try
                {
                    Directory.CreateDirectory(path.Trim());
                    string ruot = $"reporte de nominas - {fechaIniSeleccionada.ToString("yyyy-MM-dd H:m:s")}.xlsx";
                    //if (tabla_a_excel(dt, ruot, $"{comboBox1.Text}"))
                    if (TablaAExcel(dt, ruot, $"NOMINAS"))
                    {
                        MessageBox.Show($"Datos exportados exitosamente a Excel, su archivo se encuentra en: {ruot}", "RENOVATIO PyME");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Valio ocurrio el siguiente error: {ex.Message}", "RENOVATIO PyME");
                }


               
            }
        }

        public static bool TablaAExcel(DataTable tabla, string rutaArchivo, string nombreHoja)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            try
            {
                // Inicializar Excel
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                workbook = excelApp.Workbooks.Add();
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = nombreHoja;

                // Escribir encabezados
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = tabla.Columns[i].ColumnName;
                }

                // Escribir datos
                object[,] data = new object[tabla.Rows.Count, tabla.Columns.Count];
                for (int row = 0; row < tabla.Rows.Count; row++)
                {
                    for (int col = 0; col < tabla.Columns.Count; col++)
                    {
                        data[row, col] = tabla.Rows[row][col];
                    }
                }

                // Usar Range para escribir todos los datos de una vez (más eficiente)
                Microsoft.Office.Interop.Excel.Range range = worksheet.Range[
                    worksheet.Cells[2, 1],
                    worksheet.Cells[tabla.Rows.Count + 1, tabla.Columns.Count]];
                range.Value = data;

                // Formatear encabezados
                Microsoft.Office.Interop.Excel.Range headerRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[1, tabla.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Ajustar columnas
                worksheet.Columns.AutoFit();

                // Guardar y cerrar
                workbook.SaveAs(rutaArchivo);
                workbook.Close(true);
                excelApp.Quit();

                return true;
            }
            catch (Exception ex)
            {
                // Limpiar en caso de error
                workbook?.Close(false);
                excelApp?.Quit();

                MessageBox.Show($"Error al exportar a Excel: {ex.Message}", "RENOVATIO PyME");
                return false;
            }
            finally
            {
                // Liberar recursos COM
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

    }
}
