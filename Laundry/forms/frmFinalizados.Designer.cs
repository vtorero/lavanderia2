namespace Lavanderia.forms
{
    partial class frmFinalizados
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
            this.txtIdCliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnSrcCliente = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnAddPrenda = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.rdpago1E = new System.Windows.Forms.RadioButton();
            this.rdpago1T = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpPago2 = new System.Windows.Forms.GroupBox();
            this.rdpago2T = new System.Windows.Forms.RadioButton();
            this.rdpago2E = new System.Windows.Forms.RadioButton();
            this.btnCambiaModo = new System.Windows.Forms.Button();
            this.txtIdPago = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpPago2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.AutoSize = true;
            this.txtIdCliente.Location = new System.Drawing.Point(189, 106);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(58, 13);
            this.txtIdCliente.TabIndex = 14;
            this.txtIdCliente.Text = "txtidCliente";
            this.txtIdCliente.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Dni:";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(301, 49);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(100, 20);
            this.txtDni.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumero);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIdCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDni);
            this.groupBox1.Controls.Add(this.dtFechaFin);
            this.groupBox1.Controls.Add(this.btnSrcCliente);
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.dtFechaInicial);
            this.groupBox1.Controls.Add(this.btnAddPrenda);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Location = new System.Drawing.Point(18, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 132);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(78, 17);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(127, 20);
            this.txtNumero.TabIndex = 18;
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Numero:";
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaFin.Location = new System.Drawing.Point(205, 83);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Size = new System.Drawing.Size(103, 20);
            this.dtFechaFin.TabIndex = 11;
            // 
            // btnSrcCliente
            // 
            this.btnSrcCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSrcCliente.Image = global::Lavanderia.Properties.Resources.magnify;
            this.btnSrcCliente.Location = new System.Drawing.Point(446, 79);
            this.btnSrcCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnSrcCliente.Name = "btnSrcCliente";
            this.btnSrcCliente.Size = new System.Drawing.Size(73, 40);
            this.btnSrcCliente.TabIndex = 5;
            this.btnSrcCliente.UseVisualStyleBackColor = true;
            this.btnSrcCliente.Click += new System.EventHandler(this.btnSrcCliente_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(16, 89);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 13);
            this.lblFecha.TabIndex = 10;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtFechaInicial
            // 
            this.dtFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaInicial.Location = new System.Drawing.Point(77, 83);
            this.dtFechaInicial.Name = "dtFechaInicial";
            this.dtFechaInicial.Size = new System.Drawing.Size(109, 20);
            this.dtFechaInicial.TabIndex = 9;
            // 
            // btnAddPrenda
            // 
            this.btnAddPrenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPrenda.Location = new System.Drawing.Point(408, 47);
            this.btnAddPrenda.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddPrenda.Name = "btnAddPrenda";
            this.btnAddPrenda.Size = new System.Drawing.Size(111, 24);
            this.btnAddPrenda.TabIndex = 7;
            this.btnAddPrenda.Text = "Buscar Cliente";
            this.btnAddPrenda.UseVisualStyleBackColor = true;
            this.btnAddPrenda.Click += new System.EventHandler(this.btnAddPrenda_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(78, 51);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(185, 20);
            this.txtCliente.TabIndex = 2;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(16, 55);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "Cliente:";
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Location = new System.Drawing.Point(18, 148);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.RowHeadersVisible = false;
            this.dgvOrdenes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(907, 126);
            this.dgvOrdenes.TabIndex = 14;
            this.dgvOrdenes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrdenes_MouseClick);
            this.dgvOrdenes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrdenes_MouseDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvDetalles);
            this.groupBox3.Location = new System.Drawing.Point(8, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(936, 211);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalle de la Orden";
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Location = new System.Drawing.Point(9, 19);
            this.dgvDetalles.MultiSelect = false;
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(908, 180);
            this.dgvDetalles.TabIndex = 1;
            // 
            // rdpago1E
            // 
            this.rdpago1E.AutoSize = true;
            this.rdpago1E.Enabled = false;
            this.rdpago1E.Location = new System.Drawing.Point(16, 19);
            this.rdpago1E.Name = "rdpago1E";
            this.rdpago1E.Size = new System.Drawing.Size(64, 17);
            this.rdpago1E.TabIndex = 17;
            this.rdpago1E.TabStop = true;
            this.rdpago1E.Text = "Efectivo";
            this.rdpago1E.UseVisualStyleBackColor = true;
            // 
            // rdpago1T
            // 
            this.rdpago1T.AutoSize = true;
            this.rdpago1T.Enabled = false;
            this.rdpago1T.Location = new System.Drawing.Point(89, 19);
            this.rdpago1T.Name = "rdpago1T";
            this.rdpago1T.Size = new System.Drawing.Size(58, 17);
            this.rdpago1T.TabIndex = 18;
            this.rdpago1T.TabStop = true;
            this.rdpago1T.Text = "Tarjeta";
            this.rdpago1T.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdpago1T);
            this.groupBox2.Controls.Add(this.rdpago1E);
            this.groupBox2.Location = new System.Drawing.Point(579, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 49);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pago 1";
            // 
            // grpPago2
            // 
            this.grpPago2.Controls.Add(this.rdpago2T);
            this.grpPago2.Controls.Add(this.rdpago2E);
            this.grpPago2.Location = new System.Drawing.Point(749, 10);
            this.grpPago2.Name = "grpPago2";
            this.grpPago2.Size = new System.Drawing.Size(167, 47);
            this.grpPago2.TabIndex = 20;
            this.grpPago2.TabStop = false;
            this.grpPago2.Text = "Pago 2";
            this.grpPago2.Visible = false;
            // 
            // rdpago2T
            // 
            this.rdpago2T.AutoSize = true;
            this.rdpago2T.Enabled = false;
            this.rdpago2T.Location = new System.Drawing.Point(89, 19);
            this.rdpago2T.Name = "rdpago2T";
            this.rdpago2T.Size = new System.Drawing.Size(58, 17);
            this.rdpago2T.TabIndex = 19;
            this.rdpago2T.TabStop = true;
            this.rdpago2T.Text = "Tarjeta";
            this.rdpago2T.UseVisualStyleBackColor = true;
            this.rdpago2T.Visible = false;
            // 
            // rdpago2E
            // 
            this.rdpago2E.AutoSize = true;
            this.rdpago2E.Enabled = false;
            this.rdpago2E.Location = new System.Drawing.Point(6, 19);
            this.rdpago2E.Name = "rdpago2E";
            this.rdpago2E.Size = new System.Drawing.Size(64, 17);
            this.rdpago2E.TabIndex = 18;
            this.rdpago2E.TabStop = true;
            this.rdpago2E.Text = "Efectivo";
            this.rdpago2E.UseVisualStyleBackColor = true;
            this.rdpago2E.Visible = false;
            // 
            // btnCambiaModo
            // 
            this.btnCambiaModo.Location = new System.Drawing.Point(755, 72);
            this.btnCambiaModo.Name = "btnCambiaModo";
            this.btnCambiaModo.Size = new System.Drawing.Size(162, 41);
            this.btnCambiaModo.TabIndex = 21;
            this.btnCambiaModo.Text = "&Modificar";
            this.btnCambiaModo.UseVisualStyleBackColor = true;
            this.btnCambiaModo.Click += new System.EventHandler(this.btnCambiaModo_Click);
            // 
            // txtIdPago
            // 
            this.txtIdPago.Location = new System.Drawing.Point(755, 116);
            this.txtIdPago.Name = "txtIdPago";
            this.txtIdPago.Size = new System.Drawing.Size(52, 20);
            this.txtIdPago.TabIndex = 22;
            this.txtIdPago.Visible = false;
            this.txtIdPago.TextChanged += new System.EventHandler(this.txtIdPago_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Location = new System.Drawing.Point(579, 65);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 71);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Estado";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(74, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Entregado";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Por Entregar";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // frmFinalizados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 510);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtIdPago);
            this.Controls.Add(this.btnCambiaModo);
            this.Controls.Add(this.grpPago2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvOrdenes);
            this.Name = "frmFinalizados";
            this.Text = "frmFinalizados";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpPago2.ResumeLayout(false);
            this.grpPago2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSrcCliente;
        private System.Windows.Forms.Button btnAddPrenda;
        private System.Windows.Forms.Label txtIdCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtFechaFin;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtFechaInicial;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.RadioButton rdpago1E;
        private System.Windows.Forms.RadioButton rdpago1T;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpPago2;
        private System.Windows.Forms.RadioButton rdpago2T;
        private System.Windows.Forms.RadioButton rdpago2E;
        private System.Windows.Forms.Button btnCambiaModo;
        private System.Windows.Forms.TextBox txtIdPago;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}