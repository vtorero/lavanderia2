namespace Lavanderia.forms
{
    partial class frmClientesHistorico
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
            this.label2 = new System.Windows.Forms.Label();
            this.chKSucursal = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnSrcCliente = new System.Windows.Forms.Button();
            this.cheRecord = new System.Windows.Forms.CheckBox();
            this.chkCliente = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Sucursal";
            // 
            // chKSucursal
            // 
            this.chKSucursal.CheckOnClick = true;
            this.chKSucursal.FormattingEnabled = true;
            this.chKSucursal.Location = new System.Drawing.Point(83, 26);
            this.chKSucursal.Name = "chKSucursal";
            this.chKSucursal.Size = new System.Drawing.Size(129, 94);
            this.chKSucursal.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Fecha fin:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(229, 27);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(67, 13);
            this.lblFecha.TabIndex = 34;
            this.lblFecha.Text = "Fecha inicio:";
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaFin.Location = new System.Drawing.Point(336, 61);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Size = new System.Drawing.Size(109, 20);
            this.dtFechaFin.TabIndex = 33;
            // 
            // dtFechaInicial
            // 
            this.dtFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaInicial.Location = new System.Drawing.Point(336, 26);
            this.dtFechaInicial.Name = "dtFechaInicial";
            this.dtFechaInicial.Size = new System.Drawing.Size(109, 20);
            this.dtFechaInicial.TabIndex = 32;
            // 
            // btnSrcCliente
            // 
            this.btnSrcCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSrcCliente.Image = global::Lavanderia.Properties.Resources.printer;
            this.btnSrcCliente.Location = new System.Drawing.Point(364, 111);
            this.btnSrcCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnSrcCliente.Name = "btnSrcCliente";
            this.btnSrcCliente.Size = new System.Drawing.Size(81, 84);
            this.btnSrcCliente.TabIndex = 31;
            this.btnSrcCliente.UseVisualStyleBackColor = true;
            this.btnSrcCliente.Click += new System.EventHandler(this.btnSrcCliente_Click);
            // 
            // cheRecord
            // 
            this.cheRecord.AutoSize = true;
            this.cheRecord.Location = new System.Drawing.Point(232, 111);
            this.cheRecord.Name = "cheRecord";
            this.cheRecord.Size = new System.Drawing.Size(97, 17);
            this.cheRecord.TabIndex = 38;
            this.cheRecord.Text = "Reporte Pagos";
            this.cheRecord.UseVisualStyleBackColor = true;
            // 
            // chkCliente
            // 
            this.chkCliente.AutoSize = true;
            this.chkCliente.Location = new System.Drawing.Point(232, 134);
            this.chkCliente.Name = "chkCliente";
            this.chkCliente.Size = new System.Drawing.Size(117, 17);
            this.chkCliente.TabIndex = 39;
            this.chkCliente.Text = "Agrupar Por Cliente";
            this.chkCliente.UseVisualStyleBackColor = true;
            // 
            // frmClientesHistorico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 223);
            this.Controls.Add(this.chkCliente);
            this.Controls.Add(this.cheRecord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chKSucursal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtFechaFin);
            this.Controls.Add(this.btnSrcCliente);
            this.Controls.Add(this.dtFechaInicial);
            this.Name = "frmClientesHistorico";
            this.Text = "frmClientesHistorico";
            this.Load += new System.EventHandler(this.frmClientesHistorico_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox chKSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtFechaFin;
        private System.Windows.Forms.Button btnSrcCliente;
        private System.Windows.Forms.DateTimePicker dtFechaInicial;
        private System.Windows.Forms.CheckBox cheRecord;
        private System.Windows.Forms.CheckBox chkCliente;
    }
}