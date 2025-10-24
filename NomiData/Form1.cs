using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NomiData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtServidor.Text = @".\SQLEXPRESS";
            txtBaseDatos.Text = "master";
            chkWindowsAuth.Checked = true;
            txtUsuario.Enabled = txtPassword.Enabled = !chkWindowsAuth.Checked;

            chkWindowsAuth.CheckedChanged += (_, __) =>
            {
                txtUsuario.Enabled = txtPassword.Enabled = !chkWindowsAuth.Checked;
            };

            //btnProbar.Click += (_, __) => ProbarConexion();
            //btnConectar.Click += (_, __) =>
            //{
            //    if (ProbarConexion())
            //    {
            //        var connString = ConstruirConnectionString();
            //        Hide();
            //        using (var frm = new FrmMenu(connString))
            //            frm.ShowDialog();
            //        Close();
            //    }
            //};
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string script = File.ReadAllText(@"Scripts\reporte_nominas.sql");

        }

        private string ConstruirConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServidor.Text.Trim(),
                InitialCatalog = txtBaseDatos.Text.Trim(),
                TrustServerCertificate = true,     
                Encrypt = false                    // true si tu servidor lo requiere
            };

            if (chkWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = txtUsuario.Text.Trim();
                builder.Password = txtPassword.Text;
            }
            return builder.ToString();
        }

        private bool ProbarConexion()
        {
            try
            {
                lblEstado.Text = "Conectando...";
                using (var cn = new SqlConnection(ConstruirConnectionString()))
                {
                    cn.Open();
                    var cmd = new SqlCommand("SELECT @@VERSION", cn);
                    var ver = (string)cmd.ExecuteScalar();
                    lblEstado.Text = "OK: " + ver.Split('\n')[0].Trim();
                }
                return true;
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.Message, "Conexión fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {

        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            ProbarConexion();
        }
    }
}
