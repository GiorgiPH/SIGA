namespace ControlAcademico
{
    partial class registroIngresos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registroIngresos));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgvPagosPendientes = new System.Windows.Forms.DataGridView();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FolioDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasRecargo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MenosRecargo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Recargos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasDescuentos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MenosDescuentos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCalculo = new System.Windows.Forms.TextBox();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.txtDiarioMensual = new System.Windows.Forms.TextBox();
            this.txtImporteConcepto = new System.Windows.Forms.TextBox();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtGenerarRecargo = new System.Windows.Forms.TextBox();
            this.txtImportePorcentaje = new System.Windows.Forms.TextBox();
            this.txtMontoD = new System.Windows.Forms.TextBox();
            this.txtMontoR = new System.Windows.Forms.TextBox();
            this.txtDescuentosSiNo = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.txtRecargosSiNo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDescuentos = new System.Windows.Forms.TextBox();
            this.txtRecargos = new System.Windows.Forms.TextBox();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCaja = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.txtAlumno = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPagosPendientes
            // 
            this.dgvPagosPendientes.AllowUserToAddRows = false;
            this.dgvPagosPendientes.AllowUserToDeleteRows = false;
            this.dgvPagosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagosPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagosPendientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.FolioDocumento,
            this.Documento,
            this.Concepto,
            this.Fecha,
            this.Importe,
            this.SaldoActual,
            this.Vencimiento,
            this.MasRecargo,
            this.MenosRecargo,
            this.Recargos,
            this.MasDescuentos,
            this.MenosDescuentos,
            this.Descuento,
            this.Saldo});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagosPendientes.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPagosPendientes.Location = new System.Drawing.Point(11, 132);
            this.dgvPagosPendientes.MultiSelect = false;
            this.dgvPagosPendientes.Name = "dgvPagosPendientes";
            this.dgvPagosPendientes.RowHeadersVisible = false;
            this.dgvPagosPendientes.RowHeadersWidth = 45;
            this.dgvPagosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagosPendientes.Size = new System.Drawing.Size(942, 258);
            this.dgvPagosPendientes.TabIndex = 44;
            this.dgvPagosPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellClick);
            this.dgvPagosPendientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellContentClick);
            this.dgvPagosPendientes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellEndEdit);
            this.dgvPagosPendientes.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvPagosPendientes_CurrentCellDirtyStateChanged);
            // 
            // Seleccionar
            // 
            this.Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Seleccionar.HeaderText = "Seleccionar";
            this.Seleccionar.Name = "Seleccionar";
            // 
            // FolioDocumento
            // 
            this.FolioDocumento.HeaderText = "Folio";
            this.FolioDocumento.Name = "FolioDocumento";
            this.FolioDocumento.ReadOnly = true;
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
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Importe.DefaultCellStyle = dataGridViewCellStyle2;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            // 
            // SaldoActual
            // 
            this.SaldoActual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.SaldoActual.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaldoActual.HeaderText = "Saldo Actual";
            this.SaldoActual.Name = "SaldoActual";
            this.SaldoActual.ReadOnly = true;
            // 
            // Vencimiento
            // 
            this.Vencimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Vencimiento.HeaderText = "Vencimiento";
            this.Vencimiento.Name = "Vencimiento";
            this.Vencimiento.ReadOnly = true;
            // 
            // MasRecargo
            // 
            this.MasRecargo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MasRecargo.HeaderText = "+";
            this.MasRecargo.Name = "MasRecargo";
            this.MasRecargo.Text = "+";
            this.MasRecargo.UseColumnTextForButtonValue = true;
            this.MasRecargo.Width = 20;
            // 
            // MenosRecargo
            // 
            this.MenosRecargo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MenosRecargo.HeaderText = "-";
            this.MenosRecargo.Name = "MenosRecargo";
            this.MenosRecargo.Text = "-";
            this.MenosRecargo.UseColumnTextForButtonValue = true;
            this.MenosRecargo.Width = 17;
            // 
            // Recargos
            // 
            this.Recargos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = "0.00";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Recargos.DefaultCellStyle = dataGridViewCellStyle4;
            this.Recargos.HeaderText = "Recargos";
            this.Recargos.Name = "Recargos";
            this.Recargos.ReadOnly = true;
            // 
            // MasDescuentos
            // 
            this.MasDescuentos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MasDescuentos.HeaderText = "+";
            this.MasDescuentos.Name = "MasDescuentos";
            this.MasDescuentos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MasDescuentos.Text = "+";
            this.MasDescuentos.UseColumnTextForButtonValue = true;
            this.MasDescuentos.Width = 20;
            // 
            // MenosDescuentos
            // 
            this.MenosDescuentos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MenosDescuentos.HeaderText = "-";
            this.MenosDescuentos.Name = "MenosDescuentos";
            this.MenosDescuentos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MenosDescuentos.Text = "-";
            this.MenosDescuentos.UseColumnTextForButtonValue = true;
            this.MenosDescuentos.Width = 17;
            // 
            // Descuento
            // 
            this.Descuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = "0.00";
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Descuento.DefaultCellStyle = dataGridViewCellStyle5;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            // 
            // Saldo
            // 
            this.Saldo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = "0.00";
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle6;
            this.Saldo.HeaderText = "Saldo (R + D)";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Width = 78;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(827, 46);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(121, 23);
            this.btnBuscar.TabIndex = 43;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(694, 516);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(108, 31);
            this.button6.TabIndex = 32;
            this.button6.Text = "Cancelar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.txtCalculo);
            this.panel1.Controls.Add(this.txtConcepto);
            this.panel1.Controls.Add(this.txtDiarioMensual);
            this.panel1.Controls.Add(this.txtImporteConcepto);
            this.panel1.Controls.Add(this.txtPorcentaje);
            this.panel1.Controls.Add(this.txtImporte);
            this.panel1.Controls.Add(this.txtGenerarRecargo);
            this.panel1.Controls.Add(this.txtImportePorcentaje);
            this.panel1.Controls.Add(this.txtMontoD);
            this.panel1.Controls.Add(this.txtMontoR);
            this.panel1.Controls.Add(this.txtDescuentosSiNo);
            this.panel1.Controls.Add(this.txtFecha);
            this.panel1.Controls.Add(this.txtRecargosSiNo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.txtDescuentos);
            this.panel1.Controls.Add(this.txtRecargos);
            this.panel1.Controls.Add(this.txtSubtotal);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtCaja);
            this.panel1.Controls.Add(this.dgvPagosPendientes);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtMatricula);
            this.panel1.Controls.Add(this.txtAlumno);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtpFecha);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 568);
            this.panel1.TabIndex = 1;
            // 
            // txtCalculo
            // 
            this.txtCalculo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCalculo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCalculo.Enabled = false;
            this.txtCalculo.Location = new System.Drawing.Point(385, 424);
            this.txtCalculo.Name = "txtCalculo";
            this.txtCalculo.Size = new System.Drawing.Size(174, 20);
            this.txtCalculo.TabIndex = 73;
            this.txtCalculo.Visible = false;
            // 
            // txtConcepto
            // 
            this.txtConcepto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtConcepto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtConcepto.Enabled = false;
            this.txtConcepto.Location = new System.Drawing.Point(205, 424);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(174, 20);
            this.txtConcepto.TabIndex = 72;
            this.txtConcepto.Visible = false;
            // 
            // txtDiarioMensual
            // 
            this.txtDiarioMensual.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDiarioMensual.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDiarioMensual.Enabled = false;
            this.txtDiarioMensual.Location = new System.Drawing.Point(205, 476);
            this.txtDiarioMensual.Name = "txtDiarioMensual";
            this.txtDiarioMensual.Size = new System.Drawing.Size(174, 20);
            this.txtDiarioMensual.TabIndex = 71;
            this.txtDiarioMensual.Visible = false;
            // 
            // txtImporteConcepto
            // 
            this.txtImporteConcepto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtImporteConcepto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtImporteConcepto.Enabled = false;
            this.txtImporteConcepto.Location = new System.Drawing.Point(385, 502);
            this.txtImporteConcepto.Name = "txtImporteConcepto";
            this.txtImporteConcepto.Size = new System.Drawing.Size(174, 20);
            this.txtImporteConcepto.TabIndex = 70;
            this.txtImporteConcepto.Visible = false;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPorcentaje.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPorcentaje.Enabled = false;
            this.txtPorcentaje.Location = new System.Drawing.Point(385, 477);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(174, 20);
            this.txtPorcentaje.TabIndex = 69;
            this.txtPorcentaje.Visible = false;
            // 
            // txtImporte
            // 
            this.txtImporte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtImporte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtImporte.Enabled = false;
            this.txtImporte.Location = new System.Drawing.Point(385, 451);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(174, 20);
            this.txtImporte.TabIndex = 68;
            this.txtImporte.Visible = false;
            // 
            // txtGenerarRecargo
            // 
            this.txtGenerarRecargo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGenerarRecargo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtGenerarRecargo.Enabled = false;
            this.txtGenerarRecargo.Location = new System.Drawing.Point(205, 451);
            this.txtGenerarRecargo.Name = "txtGenerarRecargo";
            this.txtGenerarRecargo.Size = new System.Drawing.Size(174, 20);
            this.txtGenerarRecargo.TabIndex = 67;
            this.txtGenerarRecargo.Visible = false;
            // 
            // txtImportePorcentaje
            // 
            this.txtImportePorcentaje.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtImportePorcentaje.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtImportePorcentaje.Enabled = false;
            this.txtImportePorcentaje.Location = new System.Drawing.Point(205, 502);
            this.txtImportePorcentaje.Name = "txtImportePorcentaje";
            this.txtImportePorcentaje.Size = new System.Drawing.Size(174, 20);
            this.txtImportePorcentaje.TabIndex = 66;
            this.txtImportePorcentaje.Visible = false;
            // 
            // txtMontoD
            // 
            this.txtMontoD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMontoD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMontoD.Enabled = false;
            this.txtMontoD.Location = new System.Drawing.Point(20, 527);
            this.txtMontoD.Name = "txtMontoD";
            this.txtMontoD.Size = new System.Drawing.Size(174, 20);
            this.txtMontoD.TabIndex = 65;
            this.txtMontoD.Visible = false;
            // 
            // txtMontoR
            // 
            this.txtMontoR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMontoR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMontoR.Enabled = false;
            this.txtMontoR.Location = new System.Drawing.Point(20, 502);
            this.txtMontoR.Name = "txtMontoR";
            this.txtMontoR.Size = new System.Drawing.Size(174, 20);
            this.txtMontoR.TabIndex = 64;
            this.txtMontoR.Visible = false;
            // 
            // txtDescuentosSiNo
            // 
            this.txtDescuentosSiNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDescuentosSiNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDescuentosSiNo.Enabled = false;
            this.txtDescuentosSiNo.Location = new System.Drawing.Point(20, 476);
            this.txtDescuentosSiNo.Name = "txtDescuentosSiNo";
            this.txtDescuentosSiNo.Size = new System.Drawing.Size(174, 20);
            this.txtDescuentosSiNo.TabIndex = 63;
            this.txtDescuentosSiNo.Visible = false;
            // 
            // txtFecha
            // 
            this.txtFecha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFecha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(20, 424);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(174, 20);
            this.txtFecha.TabIndex = 62;
            this.txtFecha.Visible = false;
            // 
            // txtRecargosSiNo
            // 
            this.txtRecargosSiNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRecargosSiNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRecargosSiNo.Enabled = false;
            this.txtRecargosSiNo.Location = new System.Drawing.Point(20, 450);
            this.txtRecargosSiNo.Name = "txtRecargosSiNo";
            this.txtRecargosSiNo.Size = new System.Drawing.Size(174, 20);
            this.txtRecargosSiNo.TabIndex = 61;
            this.txtRecargosSiNo.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(827, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "Pagos Registrados";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.MediumAquamarine;
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(808, 479);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(135, 20);
            this.txtTotal.TabIndex = 58;
            this.txtTotal.Text = "0.00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescuentos
            // 
            this.txtDescuentos.BackColor = System.Drawing.Color.MediumAquamarine;
            this.txtDescuentos.Enabled = false;
            this.txtDescuentos.Location = new System.Drawing.Point(808, 454);
            this.txtDescuentos.Name = "txtDescuentos";
            this.txtDescuentos.Size = new System.Drawing.Size(135, 20);
            this.txtDescuentos.TabIndex = 57;
            this.txtDescuentos.Text = "0.00";
            this.txtDescuentos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRecargos
            // 
            this.txtRecargos.BackColor = System.Drawing.Color.MediumAquamarine;
            this.txtRecargos.Enabled = false;
            this.txtRecargos.Location = new System.Drawing.Point(808, 428);
            this.txtRecargos.Name = "txtRecargos";
            this.txtRecargos.Size = new System.Drawing.Size(135, 20);
            this.txtRecargos.TabIndex = 56;
            this.txtRecargos.Text = "0.00";
            this.txtRecargos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.BackColor = System.Drawing.Color.MediumAquamarine;
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.Location = new System.Drawing.Point(808, 405);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(135, 20);
            this.txtSubtotal.TabIndex = 55;
            this.txtSubtotal.Text = "0.00";
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(699, 457);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "- Descuentos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(699, 482);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "Total";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(699, 431);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 52;
            this.label12.Text = "+ Recargos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(699, 408);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "Sub Total";
            // 
            // txtCaja
            // 
            this.txtCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCaja.Enabled = false;
            this.txtCaja.Location = new System.Drawing.Point(125, 46);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.Size = new System.Drawing.Size(138, 20);
            this.txtCaja.TabIndex = 45;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(808, 516);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 31);
            this.button5.TabIndex = 31;
            this.button5.Text = "Registrar el Cobro";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Lista de Pagos pendientes:";
            // 
            // txtMatricula
            // 
            this.txtMatricula.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMatricula.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMatricula.Enabled = false;
            this.txtMatricula.Location = new System.Drawing.Point(365, 48);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(174, 20);
            this.txtMatricula.TabIndex = 24;
            this.txtMatricula.TextChanged += new System.EventHandler(this.txtMatricula_TextChanged);
            // 
            // txtAlumno
            // 
            this.txtAlumno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAlumno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtAlumno.Enabled = false;
            this.txtAlumno.Location = new System.Drawing.Point(545, 48);
            this.txtAlumno.Name = "txtAlumno";
            this.txtAlumno.Size = new System.Drawing.Size(257, 20);
            this.txtAlumno.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(362, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Clave:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(542, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Propietario:";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(0, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(962, 2);
            this.label5.TabIndex = 13;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(12, 46);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(107, 20);
            this.dtpFecha.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(134, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Caja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos para registro y control del pago";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkSalmon;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(975, 25);
            this.toolStrip1.TabIndex = 36;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(59, 22);
            this.toolStripButton1.Text = "Cerrar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // registroIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(975, 604);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "registroIngresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Registra Cobranza";
            this.Activated += new System.EventHandler(this.registroIngresos_Activated);
            this.Load += new System.EventHandler(this.registroIngresos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgvPagosPendientes;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.TextBox txtAlumno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaja;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtDescuentos;
        private System.Windows.Forms.TextBox txtRecargos;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.TextBox txtRecargosSiNo;
        private System.Windows.Forms.TextBox txtDescuentosSiNo;
        private System.Windows.Forms.TextBox txtMontoD;
        private System.Windows.Forms.TextBox txtMontoR;
        private System.Windows.Forms.TextBox txtDiarioMensual;
        private System.Windows.Forms.TextBox txtImporteConcepto;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtGenerarRecargo;
        private System.Windows.Forms.TextBox txtImportePorcentaje;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vencimiento;
        private System.Windows.Forms.DataGridViewButtonColumn MasRecargo;
        private System.Windows.Forms.DataGridViewButtonColumn MenosRecargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recargos;
        private System.Windows.Forms.DataGridViewButtonColumn MasDescuentos;
        private System.Windows.Forms.DataGridViewButtonColumn MenosDescuentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.TextBox txtCalculo;
    }
}