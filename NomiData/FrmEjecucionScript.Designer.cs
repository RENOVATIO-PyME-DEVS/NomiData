using System.Windows.Forms;

namespace NomiData
{
    partial class FrmEjecucionScript
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbStored = new System.Windows.Forms.GroupBox();
            this.btnEjecutarSP = new System.Windows.Forms.Button();
            this.txtParam3 = new System.Windows.Forms.TextBox();
            this.lblParam3 = new System.Windows.Forms.Label();
            this.txtParam2 = new System.Windows.Forms.TextBox();
            this.lblParam2 = new System.Windows.Forms.Label();
            this.txtParam1 = new System.Windows.Forms.TextBox();
            this.lblParam1 = new System.Windows.Forms.Label();
            this.gbLibre = new System.Windows.Forms.GroupBox();
            this.btnEjecutarLibre = new System.Windows.Forms.Button();
            this.txtScriptLibre = new System.Windows.Forms.RichTextBox();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.gbStored.SuspendLayout();
            this.gbLibre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // gbStored
            // 
            this.gbStored.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStored.Controls.Add(this.btnEjecutarSP);
            this.gbStored.Controls.Add(this.txtParam3);
            this.gbStored.Controls.Add(this.lblParam3);
            this.gbStored.Controls.Add(this.txtParam2);
            this.gbStored.Controls.Add(this.lblParam2);
            this.gbStored.Controls.Add(this.txtParam1);
            this.gbStored.Controls.Add(this.lblParam1);
            this.gbStored.Location = new System.Drawing.Point(12, 12);
            this.gbStored.Name = "gbStored";
            this.gbStored.Size = new System.Drawing.Size(470, 160);
            this.gbStored.TabIndex = 0;
            this.gbStored.TabStop = false;
            this.gbStored.Text = "Stored procedure (3 parámetros)";
            // 
            // btnEjecutarSP
            // 
            this.btnEjecutarSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEjecutarSP.Location = new System.Drawing.Point(330, 126);
            this.btnEjecutarSP.Name = "btnEjecutarSP";
            this.btnEjecutarSP.Size = new System.Drawing.Size(110, 28);
            this.btnEjecutarSP.TabIndex = 6;
            this.btnEjecutarSP.Text = "Ejecutar SP";
            this.btnEjecutarSP.UseVisualStyleBackColor = true;
            this.btnEjecutarSP.Click += new System.EventHandler(this.btnEjecutarSP_Click);
            // 
            // txtParam3
            // 
            this.txtParam3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParam3.Location = new System.Drawing.Point(120, 96);
            this.txtParam3.Name = "txtParam3";
            this.txtParam3.Size = new System.Drawing.Size(320, 26);
            this.txtParam3.TabIndex = 5;
            // 
            // lblParam3
            // 
            this.lblParam3.AutoSize = false;
            this.lblParam3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblParam3.Location = new System.Drawing.Point(16, 100);
            this.lblParam3.Name = "lblParam3";
            this.lblParam3.Size = new System.Drawing.Size(100, 20);
            this.lblParam3.TabIndex = 4;
            this.lblParam3.Text = "Parámetro 3";
            // 
            // txtParam2
            // 
            this.txtParam2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParam2.Location = new System.Drawing.Point(120, 62);
            this.txtParam2.Name = "txtParam2";
            this.txtParam2.Size = new System.Drawing.Size(320, 26);
            this.txtParam2.TabIndex = 3;
            // 
            // lblParam2
            // 
            this.lblParam2.AutoSize = false;
            this.lblParam2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblParam2.Location = new System.Drawing.Point(16, 66);
            this.lblParam2.Name = "lblParam2";
            this.lblParam2.Size = new System.Drawing.Size(100, 20);
            this.lblParam2.TabIndex = 2;
            this.lblParam2.Text = "Parámetro 2";
            // 
            // txtParam1
            // 
            this.txtParam1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParam1.Location = new System.Drawing.Point(120, 28);
            this.txtParam1.Name = "txtParam1";
            this.txtParam1.Size = new System.Drawing.Size(320, 26);
            this.txtParam1.TabIndex = 1;
            // 
            // lblParam1
            // 
            this.lblParam1.AutoSize = false;
            this.lblParam1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblParam1.Location = new System.Drawing.Point(16, 32);
            this.lblParam1.Name = "lblParam1";
            this.lblParam1.Size = new System.Drawing.Size(100, 20);
            this.lblParam1.TabIndex = 0;
            this.lblParam1.Text = "Parámetro 1";
            // 
            // gbLibre
            // 
            this.gbLibre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLibre.Controls.Add(this.btnEjecutarLibre);
            this.gbLibre.Controls.Add(this.txtScriptLibre);
            this.gbLibre.Location = new System.Drawing.Point(488, 12);
            this.gbLibre.Name = "gbLibre";
            this.gbLibre.Size = new System.Drawing.Size(500, 160);
            this.gbLibre.TabIndex = 1;
            this.gbLibre.TabStop = false;
            this.gbLibre.Text = "Script libre (solo SELECT; se bloquea DELETE/DROP/ALTER/TRUNCATE)";
            // 
            // btnEjecutarLibre
            // 
            this.btnEjecutarLibre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEjecutarLibre.Location = new System.Drawing.Point(374, 126);
            this.btnEjecutarLibre.Name = "btnEjecutarLibre";
            this.btnEjecutarLibre.Size = new System.Drawing.Size(110, 28);
            this.btnEjecutarLibre.TabIndex = 8;
            this.btnEjecutarLibre.Text = "Ejecutar script";
            this.btnEjecutarLibre.UseVisualStyleBackColor = true;
            this.btnEjecutarLibre.Click += new System.EventHandler(this.btnEjecutarLibre_Click);
            // 
            // txtScriptLibre
            // 
            this.txtScriptLibre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScriptLibre.Location = new System.Drawing.Point(16, 24);
            this.txtScriptLibre.Name = "txtScriptLibre";
            this.txtScriptLibre.Size = new System.Drawing.Size(468, 98);
            this.txtScriptLibre.TabIndex = 7;
            this.txtScriptLibre.Text = "";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportarExcel.Location = new System.Drawing.Point(12, 182);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(140, 30);
            this.btnExportarExcel.TabIndex = 9;
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = false;
            this.lblInfo.Location = new System.Drawing.Point(170, 188);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(120, 20);
            this.lblInfo.TabIndex = 10;
            this.lblInfo.Text = "Resultados";
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(12, 220);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersWidth = 62;
            this.dgvResultados.RowTemplate.Height = 28;
            this.dgvResultados.Size = new System.Drawing.Size(976, 420);
            this.dgvResultados.TabIndex = 11;
            // 
            // FrmEjecucionScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.gbStored);
            this.Controls.Add(this.gbLibre);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvResultados);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "FrmEjecucionScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ejecución de Script";
            this.gbStored.ResumeLayout(false);
            this.gbStored.PerformLayout();
            this.gbLibre.ResumeLayout(false);
            this.gbLibre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox gbStored;
        private System.Windows.Forms.Button btnEjecutarSP;
        private System.Windows.Forms.TextBox txtParam3;
        private System.Windows.Forms.Label lblParam3;
        private System.Windows.Forms.TextBox txtParam2;
        private System.Windows.Forms.Label lblParam2;
        private System.Windows.Forms.TextBox txtParam1;
        private System.Windows.Forms.Label lblParam1;
        private System.Windows.Forms.GroupBox gbLibre;
        private System.Windows.Forms.Button btnEjecutarLibre;
        private System.Windows.Forms.RichTextBox txtScriptLibre;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvResultados;
    }
}
