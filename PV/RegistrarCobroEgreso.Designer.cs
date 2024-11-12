namespace PV
{
    partial class RegistrarCobroEgreso
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarCobroEgreso));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvPagosPendientes = new System.Windows.Forms.DataGridView();
            this.FormaPago = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.formasPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet29 = new PV.ControlCondominiosDataSet29();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recargos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasAbono = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Abono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.formasPagoTableAdapter = new PV.ControlCondominiosDataSet29TableAdapters.FormasPagoTableAdapter();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioGeneral = new System.Windows.Forms.TextBox();
            this.txtMatricula = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAlumno = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtReferncia = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNumOperacion = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNumAutorizacion = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtObservaciones = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbCuentaBancaria = new Guna.UI2.WinForms.Guna2ComboBox();
            this.button5 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.txtReciboCol = new System.Windows.Forms.TextBox();
            this.txtImporteTotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTotalPagado = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formasPagoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet29)).BeginInit();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 515);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "Numero Autorización:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 487);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Numero Operación:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 110;
            this.label1.Text = "Referencia:";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(-29, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(962, 2);
            this.label5.TabIndex = 109;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(637, 493);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 108;
            this.label14.Text = "Total Pagado:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(637, 456);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 107;
            this.label11.Text = "Importe Total;";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(786, 83);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(107, 20);
            this.dtpFecha.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(786, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Fecha";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Clave:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(194, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 101;
            this.label7.Text = "Proveedor:";
            // 
            // dgvPagosPendientes
            // 
            this.dgvPagosPendientes.AllowUserToAddRows = false;
            this.dgvPagosPendientes.AllowUserToDeleteRows = false;
            this.dgvPagosPendientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPagosPendientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagosPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagosPendientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FormaPago,
            this.Tipo,
            this.FolioDocumento,
            this.Documento,
            this.Concepto,
            this.Recargos,
            this.Descuento,
            this.Importe,
            this.MasAbono,
            this.Abono,
            this.Saldo});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagosPendientes.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPagosPendientes.EnableHeadersVisualStyles = false;
            this.dgvPagosPendientes.Location = new System.Drawing.Point(12, 158);
            this.dgvPagosPendientes.MultiSelect = false;
            this.dgvPagosPendientes.Name = "dgvPagosPendientes";
            this.dgvPagosPendientes.RowHeadersVisible = false;
            this.dgvPagosPendientes.RowHeadersWidth = 45;
            this.dgvPagosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagosPendientes.Size = new System.Drawing.Size(880, 258);
            this.dgvPagosPendientes.TabIndex = 100;
            this.dgvPagosPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellClick);
            this.dgvPagosPendientes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellEndEdit);
            // 
            // FormaPago
            // 
            this.FormaPago.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FormaPago.DataSource = this.formasPagoBindingSource;
            this.FormaPago.DisplayMember = "Descripcion";
            this.FormaPago.HeaderText = "Forma de Pago";
            this.FormaPago.Name = "FormaPago";
            this.FormaPago.ValueMember = "Descripcion";
            // 
            // formasPagoBindingSource
            // 
            this.formasPagoBindingSource.DataMember = "FormasPago";
            this.formasPagoBindingSource.DataSource = this.controlCondominiosDataSet29;
            // 
            // controlCondominiosDataSet29
            // 
            this.controlCondominiosDataSet29.DataSetName = "ControlCondominiosDataSet29";
            this.controlCondominiosDataSet29.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Tipo
            // 
            this.Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 55;
            // 
            // FolioDocumento
            // 
            this.FolioDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FolioDocumento.HeaderText = "Folio";
            this.FolioDocumento.Name = "FolioDocumento";
            this.FolioDocumento.ReadOnly = true;
            this.FolioDocumento.Width = 57;
            // 
            // Documento
            // 
            this.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Documento.HeaderText = "Documento";
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            // 
            // Concepto
            // 
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            this.Concepto.Width = 84;
            // 
            // Recargos
            // 
            this.Recargos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Recargos.DefaultCellStyle = dataGridViewCellStyle2;
            this.Recargos.HeaderText = "Recargos";
            this.Recargos.Name = "Recargos";
            this.Recargos.ReadOnly = true;
            this.Recargos.Width = 84;
            // 
            // Descuento
            // 
            this.Descuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.Descuento.DefaultCellStyle = dataGridViewCellStyle3;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            this.Descuento.Width = 91;
            // 
            // Importe
            // 
            this.Importe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.Importe.DefaultCellStyle = dataGridViewCellStyle4;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 72;
            // 
            // MasAbono
            // 
            this.MasAbono.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MasAbono.HeaderText = "+";
            this.MasAbono.Name = "MasAbono";
            this.MasAbono.Text = "+";
            this.MasAbono.UseColumnTextForButtonValue = true;
            this.MasAbono.Width = 18;
            // 
            // Abono
            // 
            this.Abono.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0.00";
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Abono.DefaultCellStyle = dataGridViewCellStyle5;
            this.Abono.HeaderText = "Abono";
            this.Abono.Name = "Abono";
            this.Abono.ReadOnly = true;
            this.Abono.Width = 66;
            // 
            // Saldo
            // 
            this.Saldo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0.00";
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle6;
            this.Saldo.HeaderText = "Saldo";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Width = 62;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 16);
            this.label9.TabIndex = 99;
            this.label9.Text = "Lista de Pagos";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 543);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 98;
            this.label10.Text = "Observaciones:";
            // 
            // formasPagoTableAdapter
            // 
            this.formasPagoTableAdapter.ClearBeforeFill = true;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(509, 458);
            this.txtCuenta.MaxLength = 100;
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(94, 20);
            this.txtCuenta.TabIndex = 115;
            this.txtCuenta.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 459);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 113;
            this.label6.Text = "Cuenta Bancaria:";
            // 
            // txtFolioGeneral
            // 
            this.txtFolioGeneral.Location = new System.Drawing.Point(509, 484);
            this.txtFolioGeneral.MaxLength = 100;
            this.txtFolioGeneral.Name = "txtFolioGeneral";
            this.txtFolioGeneral.Size = new System.Drawing.Size(94, 20);
            this.txtFolioGeneral.TabIndex = 116;
            this.txtFolioGeneral.Visible = false;
            // 
            // txtMatricula
            // 
            this.txtMatricula.AutoRoundedCorners = true;
            this.txtMatricula.BorderColor = System.Drawing.Color.Gray;
            this.txtMatricula.BorderRadius = 11;
            this.txtMatricula.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatricula.DefaultText = "";
            this.txtMatricula.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatricula.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatricula.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatricula.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatricula.Enabled = false;
            this.txtMatricula.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatricula.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatricula.ForeColor = System.Drawing.Color.Black;
            this.txtMatricula.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatricula.Location = new System.Drawing.Point(17, 83);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.PasswordChar = '\0';
            this.txtMatricula.PlaceholderText = "";
            this.txtMatricula.SelectedText = "";
            this.txtMatricula.Size = new System.Drawing.Size(174, 25);
            this.txtMatricula.TabIndex = 341;
            // 
            // txtAlumno
            // 
            this.txtAlumno.AutoRoundedCorners = true;
            this.txtAlumno.BorderColor = System.Drawing.Color.Gray;
            this.txtAlumno.BorderRadius = 11;
            this.txtAlumno.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAlumno.DefaultText = "";
            this.txtAlumno.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAlumno.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAlumno.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlumno.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlumno.Enabled = false;
            this.txtAlumno.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlumno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAlumno.ForeColor = System.Drawing.Color.Black;
            this.txtAlumno.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlumno.Location = new System.Drawing.Point(199, 83);
            this.txtAlumno.Name = "txtAlumno";
            this.txtAlumno.PasswordChar = '\0';
            this.txtAlumno.PlaceholderText = "";
            this.txtAlumno.SelectedText = "";
            this.txtAlumno.Size = new System.Drawing.Size(304, 25);
            this.txtAlumno.TabIndex = 342;
            // 
            // txtReferncia
            // 
            this.txtReferncia.AutoRoundedCorners = true;
            this.txtReferncia.BorderColor = System.Drawing.Color.Gray;
            this.txtReferncia.BorderRadius = 11;
            this.txtReferncia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtReferncia.DefaultText = "";
            this.txtReferncia.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtReferncia.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtReferncia.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtReferncia.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtReferncia.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtReferncia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReferncia.ForeColor = System.Drawing.Color.Black;
            this.txtReferncia.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtReferncia.Location = new System.Drawing.Point(148, 425);
            this.txtReferncia.Name = "txtReferncia";
            this.txtReferncia.PasswordChar = '\0';
            this.txtReferncia.PlaceholderText = "";
            this.txtReferncia.SelectedText = "";
            this.txtReferncia.Size = new System.Drawing.Size(166, 25);
            this.txtReferncia.TabIndex = 342;
            // 
            // txtNumOperacion
            // 
            this.txtNumOperacion.AutoRoundedCorners = true;
            this.txtNumOperacion.BorderColor = System.Drawing.Color.Gray;
            this.txtNumOperacion.BorderRadius = 11;
            this.txtNumOperacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNumOperacion.DefaultText = "";
            this.txtNumOperacion.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNumOperacion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNumOperacion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNumOperacion.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNumOperacion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNumOperacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNumOperacion.ForeColor = System.Drawing.Color.Black;
            this.txtNumOperacion.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNumOperacion.Location = new System.Drawing.Point(148, 477);
            this.txtNumOperacion.Name = "txtNumOperacion";
            this.txtNumOperacion.PasswordChar = '\0';
            this.txtNumOperacion.PlaceholderText = "";
            this.txtNumOperacion.SelectedText = "";
            this.txtNumOperacion.Size = new System.Drawing.Size(166, 25);
            this.txtNumOperacion.TabIndex = 342;
            // 
            // txtNumAutorizacion
            // 
            this.txtNumAutorizacion.AutoRoundedCorners = true;
            this.txtNumAutorizacion.BorderColor = System.Drawing.Color.Gray;
            this.txtNumAutorizacion.BorderRadius = 11;
            this.txtNumAutorizacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNumAutorizacion.DefaultText = "";
            this.txtNumAutorizacion.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNumAutorizacion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNumAutorizacion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNumAutorizacion.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNumAutorizacion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNumAutorizacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNumAutorizacion.ForeColor = System.Drawing.Color.Black;
            this.txtNumAutorizacion.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNumAutorizacion.Location = new System.Drawing.Point(148, 505);
            this.txtNumAutorizacion.Name = "txtNumAutorizacion";
            this.txtNumAutorizacion.PasswordChar = '\0';
            this.txtNumAutorizacion.PlaceholderText = "";
            this.txtNumAutorizacion.SelectedText = "";
            this.txtNumAutorizacion.Size = new System.Drawing.Size(166, 25);
            this.txtNumAutorizacion.TabIndex = 342;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.AutoRoundedCorners = true;
            this.txtObservaciones.BorderColor = System.Drawing.Color.Gray;
            this.txtObservaciones.BorderRadius = 11;
            this.txtObservaciones.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtObservaciones.DefaultText = "";
            this.txtObservaciones.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtObservaciones.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtObservaciones.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtObservaciones.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtObservaciones.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtObservaciones.Location = new System.Drawing.Point(148, 533);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PasswordChar = '\0';
            this.txtObservaciones.PlaceholderText = "";
            this.txtObservaciones.SelectedText = "";
            this.txtObservaciones.Size = new System.Drawing.Size(355, 25);
            this.txtObservaciones.TabIndex = 342;
            // 
            // cmbCuentaBancaria
            // 
            this.cmbCuentaBancaria.AutoRoundedCorners = true;
            this.cmbCuentaBancaria.BackColor = System.Drawing.Color.Transparent;
            this.cmbCuentaBancaria.BorderColor = System.Drawing.Color.Gray;
            this.cmbCuentaBancaria.BorderRadius = 12;
            this.cmbCuentaBancaria.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCuentaBancaria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuentaBancaria.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuentaBancaria.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuentaBancaria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCuentaBancaria.ForeColor = System.Drawing.Color.Black;
            this.cmbCuentaBancaria.IntegralHeight = false;
            this.cmbCuentaBancaria.ItemHeight = 20;
            this.cmbCuentaBancaria.Location = new System.Drawing.Point(148, 453);
            this.cmbCuentaBancaria.Name = "cmbCuentaBancaria";
            this.cmbCuentaBancaria.Size = new System.Drawing.Size(355, 26);
            this.cmbCuentaBancaria.TabIndex = 347;
            this.cmbCuentaBancaria.SelectedIndexChanged += new System.EventHandler(this.cmbCuentaBancaria_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button5.BorderRadius = 20;
            this.button5.BorderThickness = 1;
            this.button5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button5.FillColor = System.Drawing.Color.Transparent;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button5.ImageSize = new System.Drawing.Size(48, 48);
            this.button5.Location = new System.Drawing.Point(765, 532);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 46);
            this.button5.TabIndex = 348;
            this.button5.Text = "Registrar";
            this.button5.TextOffset = new System.Drawing.Point(23, 0);
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.Controls.Add(this.guna2CircleButton1);
            this.guna2GradientPanel1.Controls.Add(this.txtReciboCol);
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(905, 45);
            this.guna2GradientPanel1.TabIndex = 349;
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CircleButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Image = global::PV.Properties.Resources.home;
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2CircleButton1.Location = new System.Drawing.Point(845, 0);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(55, 45);
            this.guna2CircleButton1.TabIndex = 112;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // txtReciboCol
            // 
            this.txtReciboCol.Enabled = false;
            this.txtReciboCol.Location = new System.Drawing.Point(958, 46);
            this.txtReciboCol.Name = "txtReciboCol";
            this.txtReciboCol.Size = new System.Drawing.Size(74, 20);
            this.txtReciboCol.TabIndex = 111;
            this.txtReciboCol.TabStop = false;
            this.txtReciboCol.Visible = false;
            // 
            // txtImporteTotal
            // 
            this.txtImporteTotal.AutoRoundedCorners = true;
            this.txtImporteTotal.BorderColor = System.Drawing.Color.Gray;
            this.txtImporteTotal.BorderRadius = 11;
            this.txtImporteTotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtImporteTotal.DefaultText = "0.00";
            this.txtImporteTotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtImporteTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtImporteTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtImporteTotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtImporteTotal.Enabled = false;
            this.txtImporteTotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtImporteTotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtImporteTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtImporteTotal.ForeColor = System.Drawing.Color.White;
            this.txtImporteTotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtImporteTotal.Location = new System.Drawing.Point(746, 450);
            this.txtImporteTotal.Name = "txtImporteTotal";
            this.txtImporteTotal.PasswordChar = '\0';
            this.txtImporteTotal.PlaceholderText = "";
            this.txtImporteTotal.SelectedText = "";
            this.txtImporteTotal.Size = new System.Drawing.Size(135, 25);
            this.txtImporteTotal.TabIndex = 350;
            this.txtImporteTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalPagado
            // 
            this.txtTotalPagado.AutoRoundedCorners = true;
            this.txtTotalPagado.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalPagado.BorderRadius = 11;
            this.txtTotalPagado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTotalPagado.DefaultText = "0.00";
            this.txtTotalPagado.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTotalPagado.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTotalPagado.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotalPagado.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotalPagado.Enabled = false;
            this.txtTotalPagado.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtTotalPagado.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotalPagado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTotalPagado.ForeColor = System.Drawing.Color.White;
            this.txtTotalPagado.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotalPagado.Location = new System.Drawing.Point(746, 487);
            this.txtTotalPagado.Name = "txtTotalPagado";
            this.txtTotalPagado.PasswordChar = '\0';
            this.txtTotalPagado.PlaceholderText = "";
            this.txtTotalPagado.SelectedText = "";
            this.txtTotalPagado.Size = new System.Drawing.Size(135, 25);
            this.txtTotalPagado.TabIndex = 350;
            this.txtTotalPagado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RegistrarCobroEgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(905, 591);
            this.ControlBox = false;
            this.Controls.Add(this.txtTotalPagado);
            this.Controls.Add(this.txtImporteTotal);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cmbCuentaBancaria);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.txtNumAutorizacion);
            this.Controls.Add(this.txtNumOperacion);
            this.Controls.Add(this.txtReferncia);
            this.Controls.Add(this.txtAlumno);
            this.Controls.Add(this.txtMatricula);
            this.Controls.Add(this.txtFolioGeneral);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvPagosPendientes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Name = "RegistrarCobroEgreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA -Registrar Pago";
            this.Load += new System.EventHandler(this.RegistrarCobroEgreso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formasPagoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet29)).EndInit();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvPagosPendientes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private ControlCondominiosDataSet29 controlCondominiosDataSet29;
        private System.Windows.Forms.BindingSource formasPagoBindingSource;
        private ControlCondominiosDataSet29TableAdapters.FormasPagoTableAdapter formasPagoTableAdapter;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewComboBoxColumn FormaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recargos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewButtonColumn MasAbono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Abono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.TextBox txtFolioGeneral;
        private Guna.UI2.WinForms.Guna2TextBox txtMatricula;
        private Guna.UI2.WinForms.Guna2TextBox txtAlumno;
        private Guna.UI2.WinForms.Guna2TextBox txtReferncia;
        private Guna.UI2.WinForms.Guna2TextBox txtNumOperacion;
        private Guna.UI2.WinForms.Guna2TextBox txtNumAutorizacion;
        private Guna.UI2.WinForms.Guna2TextBox txtObservaciones;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCuentaBancaria;
        private Guna.UI2.WinForms.Guna2Button button5;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.TextBox txtReciboCol;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2TextBox txtImporteTotal;
        private Guna.UI2.WinForms.Guna2TextBox txtTotalPagado;
    }
}