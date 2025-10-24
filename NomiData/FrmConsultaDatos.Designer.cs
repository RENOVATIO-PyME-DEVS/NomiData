namespace NomiData
{
    partial class FrmConsultaDatos
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
            this.lblBaseSeleccionada = new System.Windows.Forms.Label();
            this.lblParam1 = new System.Windows.Forms.Label();
            this.lblParam2 = new System.Windows.Forms.Label();
            this.lblParam3 = new System.Windows.Forms.Label();
            this.txtParam1 = new System.Windows.Forms.TextBox();
            this.txtParam2 = new System.Windows.Forms.TextBox();
            this.txtParam3 = new System.Windows.Forms.TextBox();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBaseSeleccionada
            // 
            this.lblBaseSeleccionada.AutoSize = true;
            this.lblBaseSeleccionada.Location = new System.Drawing.Point(12, 9);
            this.lblBaseSeleccionada.Name = "lblBaseSeleccionada";
            this.lblBaseSeleccionada.Size = new System.Drawing.Size(143, 15);
            this.lblBaseSeleccionada.TabIndex = 0;
            this.lblBaseSeleccionada.Text = "Base seleccionada: (N/A)";
            // 
            // lblParam1
            // 
            this.lblParam1.AutoSize = true;
            this.lblParam1.Location = new System.Drawing.Point(12, 42);
            this.lblParam1.Name = "lblParam1";
            this.lblParam1.Size = new System.Drawing.Size(73, 15);
            this.lblParam1.TabIndex = 1;
            this.lblParam1.Text = "Parámetro 1";
            // 
            // lblParam2
            // 
            this.lblParam2.AutoSize = true;
            this.lblParam2.Location = new System.Drawing.Point(12, 78);
            this.lblParam2.Name = "lblParam2";
            this.lblParam2.Size = new System.Drawing.Size(73, 15);
            this.lblParam2.TabIndex = 2;
            this.lblParam2.Text = "Parámetro 2";
            // 
            // lblParam3
            // 
            this.lblParam3.AutoSize = true;
            this.lblParam3.Location = new System.Drawing.Point(12, 114);
            this.lblParam3.Name = "lblParam3";
            this.lblParam3.Size = new System.Drawing.Size(73, 15);
            this.lblParam3.TabIndex = 3;
            this.lblParam3.Text = "Parámetro 3";
            // 
            // txtParam1
            // 
            this.txtParam1.Location = new System.Drawing.Point(108, 39);
            this.txtParam1.Name = "txtParam1";
            this.txtParam1.Size = new System.Drawing.Size(220, 23);
            this.txtParam1.TabIndex = 4;
            // 
            // txtParam2
            // 
            this.txtParam2.Location = new System.Drawing.Point(108, 75);
            this.txtParam2.Name = "txtParam2";
            this.txtParam2.Size = new System.Drawing.Size(220, 23);
            this.txtParam2.TabIndex = 5;
            // 
            // txtParam3
            // 
            this.txtParam3.Location = new System.Drawing.Point(108, 111);
            this.txtParam3.Name = "txtParam3";
            this.txtParam3.Size = new System.Drawing.Size(220, 23);
            this.txtParam3.TabIndex = 6;
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(348, 39);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(120, 95);
            this.btnEjecutar.TabIndex = 7;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(12, 152);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowTemplate.Height = 25;
            this.dgvResultados.Size = new System.Drawing.Size(776, 286);
            this.dgvResultados.TabIndex = 8;
            // 
            // FrmConsultaDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.txtParam3);
            this.Controls.Add(this.txtParam2);
            this.Controls.Add(this.txtParam1);
            this.Controls.Add(this.lblParam3);
            this.Controls.Add(this.lblParam2);
            this.Controls.Add(this.lblParam1);
            this.Controls.Add(this.lblBaseSeleccionada);
            this.Name = "FrmConsultaDatos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consulta de Datos";
            this.Load += new System.EventHandler(this.FrmConsultaDatos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBaseSeleccionada;
        private System.Windows.Forms.Label lblParam1;
        private System.Windows.Forms.Label lblParam2;
        private System.Windows.Forms.Label lblParam3;
        private System.Windows.Forms.TextBox txtParam1;
        private System.Windows.Forms.TextBox txtParam2;
        private System.Windows.Forms.TextBox txtParam3;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.DataGridView dgvResultados;
    }
}
