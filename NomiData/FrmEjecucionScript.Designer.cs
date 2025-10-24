namespace NomiData
{
    partial class FrmEjecucionScript
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true para desechar los recursos administrados; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por Windows Form Designer

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.btnEjecutarSP = new System.Windows.Forms.Button();
            this.btnEjecutarLibre = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.txtScriptLibre = new System.Windows.Forms.RichTextBox();
            this.txtParam1 = new System.Windows.Forms.TextBox();
            this.txtParam2 = new System.Windows.Forms.TextBox();
            this.txtParam3 = new System.Windows.Forms.TextBox();
            this.lblParametros = new System.Windows.Forms.Label();
            this.lblScriptLibre = new System.Windows.Forms.Label();
            this.lblResultados = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dgvResultados
            // 
            this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(12, 315);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.Size = new System.Drawing.Size(560, 150);
            this.dgvResultados.TabIndex = 10;
            // 
            // btnEjecutarSP
            // 
            this.btnEjecutarSP.Location = new System.Drawing.Point(434, 25);
            this.btnEjecutarSP.Name = "btnEjecutarSP";
            this.btnEjecutarSP.Size = new System.Drawing.Size(138, 30);
            this.btnEjecutarSP.TabIndex = 4;
            this.btnEjecutarSP.Text = "Ejecutar SP";
            this.btnEjecutarSP.UseVisualStyleBackColor = true;
            this.btnEjecutarSP.Click += new System.EventHandler(this.btnEjecutarSP_Click);
            // 
            // btnEjecutarLibre
            // 
            this.btnEjecutarLibre.Location = new System.Drawing.Point(12, 276);
            this.btnEjecutarLibre.Name = "btnEjecutarLibre";
            this.btnEjecutarLibre.Size = new System.Drawing.Size(138, 30);
            this.btnEjecutarLibre.TabIndex = 8;
            this.btnEjecutarLibre.Text = "Ejecutar script";
            this.btnEjecutarLibre.UseVisualStyleBackColor = true;
            this.btnEjecutarLibre.Click += new System.EventHandler(this.btnEjecutarLibre_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Location = new System.Drawing.Point(156, 276);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(138, 30);
            this.btnExportarExcel.TabIndex = 9;
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // txtScriptLibre
            // 
            this.txtScriptLibre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScriptLibre.Location = new System.Drawing.Point(12, 158);
            this.txtScriptLibre.Name = "txtScriptLibre";
            this.txtScriptLibre.Size = new System.Drawing.Size(560, 112);
            this.txtScriptLibre.TabIndex = 7;
            this.txtScriptLibre.Text = "";
            // 
            // txtParam1
            // 
            this.txtParam1.Location = new System.Drawing.Point(12, 39);
            this.txtParam1.Name = "txtParam1";
            this.txtParam1.Size = new System.Drawing.Size(200, 23);
            this.txtParam1.TabIndex = 1;
            // 
            // txtParam2
            // 
            this.txtParam2.Location = new System.Drawing.Point(12, 82);
            this.txtParam2.Name = "txtParam2";
            this.txtParam2.Size = new System.Drawing.Size(200, 23);
            this.txtParam2.TabIndex = 2;
            // 
            // txtParam3
            // 
            this.txtParam3.Location = new System.Drawing.Point(12, 125);
            this.txtParam3.Name = "txtParam3";
            this.txtParam3.Size = new System.Drawing.Size(200, 23);
            this.txtParam3.TabIndex = 3;
            // 
            // lblParametros
            // 
            this.lblParametros.AutoSize = true;
            this.lblParametros.Location = new System.Drawing.Point(12, 21);
            this.lblParametros.Name = "lblParametros";
            this.lblParametros.Size = new System.Drawing.Size(198, 15);
            this.lblParametros.TabIndex = 0;
            this.lblParametros.Text = "Parámetros del stored procedure:";
            // 
            // lblScriptLibre
            // 
            this.lblScriptLibre.AutoSize = true;
            this.lblScriptLibre.Location = new System.Drawing.Point(12, 140);
            this.lblScriptLibre.Name = "lblScriptLibre";
            this.lblScriptLibre.Size = new System.Drawing.Size(131, 15);
            this.lblScriptLibre.TabIndex = 6;
            this.lblScriptLibre.Text = "Script libre para ejecutar:";
            // 
            // lblResultados
            // 
            this.lblResultados.AutoSize = true;
            this.lblResultados.Location = new System.Drawing.Point(12, 297);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(68, 15);
            this.lblResultados.TabIndex = 11;
            this.lblResultados.Text = "Resultados:";
            // 
            // FrmEjecucionScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 477);
            this.Controls.Add(this.lblResultados);
            this.Controls.Add(this.lblScriptLibre);
            this.Controls.Add(this.lblParametros);
            this.Controls.Add(this.txtParam3);
            this.Controls.Add(this.txtParam2);
            this.Controls.Add(this.txtParam1);
            this.Controls.Add(this.txtScriptLibre);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnEjecutarLibre);
            this.Controls.Add(this.btnEjecutarSP);
            this.Controls.Add(this.dgvResultados);
            this.MinimumSize = new System.Drawing.Size(600, 516);
            this.Name = "FrmEjecucionScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ejecución de Scripts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Button btnEjecutarSP;
        private System.Windows.Forms.Button btnEjecutarLibre;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.RichTextBox txtScriptLibre;
        private System.Windows.Forms.TextBox txtParam1;
        private System.Windows.Forms.TextBox txtParam2;
        private System.Windows.Forms.TextBox txtParam3;
        private System.Windows.Forms.Label lblParametros;
        private System.Windows.Forms.Label lblScriptLibre;
        private System.Windows.Forms.Label lblResultados;
    }
}
