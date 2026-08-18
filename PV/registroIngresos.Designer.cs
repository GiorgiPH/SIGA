namespace PV
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registroIngresos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpFecha = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnLimpiar = new Guna.UI2.WinForms.Guna2Button();
            this.txtTotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDescuentos = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtRecargos = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTotalRecibo = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSubtotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnConfirmar = new Guna.UI2.WinForms.Guna2Button();
            this.btnPagosRegistrados = new Guna.UI2.WinForms.Guna2Button();
            this.btnBuscar = new Guna.UI2.WinForms.Guna2Button();
            this.txtAlumno = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatricula = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCaja = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPagosPendientes = new System.Windows.Forms.DataGridView();
            this.txtFechaRecargo = new System.Windows.Forms.TextBox();
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
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTipoPersona = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2Button14 = new Guna.UI2.WinForms.Guna2Button();
            this.txtFolioPedido = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Consecutivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propiedad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasDescuentos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MenosDescuentos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).BeginInit();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dtpFecha);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.txtDescuentos);
            this.panel1.Controls.Add(this.txtRecargos);
            this.panel1.Controls.Add(this.txtTotalRecibo);
            this.panel1.Controls.Add(this.txtSubtotal);
            this.panel1.Controls.Add(this.btnConfirmar);
            this.panel1.Controls.Add(this.btnPagosRegistrados);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtAlumno);
            this.panel1.Controls.Add(this.txtMatricula);
            this.panel1.Controls.Add(this.txtCaja);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dgvPagosPendientes);
            this.panel1.Controls.Add(this.txtFechaRecargo);
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
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblTipoPersona);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 584);
            this.panel1.TabIndex = 1;
            // 
            // dtpFecha
            // 
            this.dtpFecha.AutoRoundedCorners = true;
            this.dtpFecha.BorderRadius = 11;
            this.dtpFecha.Checked = true;
            this.dtpFecha.CustomFormat = "yyyy/MM/dd";
            this.dtpFecha.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFecha.ForeColor = System.Drawing.Color.White;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(15, 46);
            this.dtpFecha.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFecha.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(119, 25);
            this.dtpFecha.TabIndex = 231;
            this.dtpFecha.Value = new System.DateTime(2024, 11, 11, 21, 47, 21, 59);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLimpiar.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnLimpiar.BorderRadius = 20;
            this.btnLimpiar.BorderThickness = 1;
            this.btnLimpiar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLimpiar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLimpiar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLimpiar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLimpiar.FillColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnLimpiar.Image = global::PV.Properties.Resources.cancelar;
            this.btnLimpiar.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLimpiar.Location = new System.Drawing.Point(908, 524);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(128, 46);
            this.btnLimpiar.TabIndex = 230;
            this.btnLimpiar.Text = "Cancelar";
            this.btnLimpiar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLimpiar.TextOffset = new System.Drawing.Point(10, 0);
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.AutoRoundedCorners = true;
            this.txtTotal.BackColor = System.Drawing.Color.Transparent;
            this.txtTotal.BorderColor = System.Drawing.Color.Gray;
            this.txtTotal.BorderRadius = 10;
            this.txtTotal.BorderThickness = 0;
            this.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTotal.DefaultText = "0.00";
            this.txtTotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotal.Enabled = false;
            this.txtTotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtTotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotal.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotal.Location = new System.Drawing.Point(1049, 483);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.PlaceholderText = "";
            this.txtTotal.SelectedText = "";
            this.txtTotal.Size = new System.Drawing.Size(134, 23);
            this.txtTotal.TabIndex = 229;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // txtDescuentos
            // 
            this.txtDescuentos.AutoRoundedCorners = true;
            this.txtDescuentos.BackColor = System.Drawing.Color.Transparent;
            this.txtDescuentos.BorderColor = System.Drawing.Color.Gray;
            this.txtDescuentos.BorderRadius = 10;
            this.txtDescuentos.BorderThickness = 0;
            this.txtDescuentos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescuentos.DefaultText = "0.00";
            this.txtDescuentos.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDescuentos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDescuentos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescuentos.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescuentos.Enabled = false;
            this.txtDescuentos.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtDescuentos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescuentos.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentos.ForeColor = System.Drawing.Color.White;
            this.txtDescuentos.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescuentos.Location = new System.Drawing.Point(1049, 456);
            this.txtDescuentos.Name = "txtDescuentos";
            this.txtDescuentos.PlaceholderText = "";
            this.txtDescuentos.SelectedText = "";
            this.txtDescuentos.Size = new System.Drawing.Size(134, 23);
            this.txtDescuentos.TabIndex = 229;
            this.txtDescuentos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDescuentos.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // txtRecargos
            // 
            this.txtRecargos.AutoRoundedCorners = true;
            this.txtRecargos.BackColor = System.Drawing.Color.Transparent;
            this.txtRecargos.BorderColor = System.Drawing.Color.Gray;
            this.txtRecargos.BorderRadius = 10;
            this.txtRecargos.BorderThickness = 0;
            this.txtRecargos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRecargos.DefaultText = "0.00";
            this.txtRecargos.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtRecargos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtRecargos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRecargos.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRecargos.Enabled = false;
            this.txtRecargos.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtRecargos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRecargos.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecargos.ForeColor = System.Drawing.Color.White;
            this.txtRecargos.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRecargos.Location = new System.Drawing.Point(1049, 430);
            this.txtRecargos.Name = "txtRecargos";
            this.txtRecargos.PlaceholderText = "";
            this.txtRecargos.SelectedText = "";
            this.txtRecargos.Size = new System.Drawing.Size(134, 23);
            this.txtRecargos.TabIndex = 229;
            this.txtRecargos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRecargos.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // txtTotalRecibo
            // 
            this.txtTotalRecibo.AutoRoundedCorners = true;
            this.txtTotalRecibo.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalRecibo.BorderRadius = 10;
            this.txtTotalRecibo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTotalRecibo.DefaultText = "0.00";
            this.txtTotalRecibo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTotalRecibo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTotalRecibo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotalRecibo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotalRecibo.Enabled = false;
            this.txtTotalRecibo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotalRecibo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRecibo.ForeColor = System.Drawing.Color.Black;
            this.txtTotalRecibo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotalRecibo.Location = new System.Drawing.Point(800, 404);
            this.txtTotalRecibo.Name = "txtTotalRecibo";
            this.txtTotalRecibo.PlaceholderText = "";
            this.txtTotalRecibo.SelectedText = "";
            this.txtTotalRecibo.Size = new System.Drawing.Size(134, 23);
            this.txtTotalRecibo.TabIndex = 229;
            this.txtTotalRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalRecibo.TextOffset = new System.Drawing.Point(10, 0);
            this.txtTotalRecibo.TextChanged += new System.EventHandler(this.txtSubtotal_TextChanged);
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.AutoRoundedCorners = true;
            this.txtSubtotal.BackColor = System.Drawing.Color.Transparent;
            this.txtSubtotal.BorderColor = System.Drawing.Color.Gray;
            this.txtSubtotal.BorderRadius = 10;
            this.txtSubtotal.BorderThickness = 0;
            this.txtSubtotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSubtotal.DefaultText = "0.00";
            this.txtSubtotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSubtotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSubtotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSubtotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtSubtotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSubtotal.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubtotal.ForeColor = System.Drawing.Color.White;
            this.txtSubtotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSubtotal.Location = new System.Drawing.Point(1049, 404);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.PlaceholderText = "";
            this.txtSubtotal.SelectedText = "";
            this.txtSubtotal.Size = new System.Drawing.Size(134, 23);
            this.txtSubtotal.TabIndex = 229;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubtotal.TextOffset = new System.Drawing.Point(10, 0);
            this.txtSubtotal.TextChanged += new System.EventHandler(this.txtSubtotal_TextChanged);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirmar.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnConfirmar.BorderRadius = 20;
            this.btnConfirmar.BorderThickness = 1;
            this.btnConfirmar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConfirmar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConfirmar.FillColor = System.Drawing.Color.White;
            this.btnConfirmar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmar.Image")));
            this.btnConfirmar.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnConfirmar.ImageSize = new System.Drawing.Size(48, 48);
            this.btnConfirmar.Location = new System.Drawing.Point(1042, 524);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(131, 46);
            this.btnConfirmar.TabIndex = 225;
            this.btnConfirmar.Text = "Registrar";
            this.btnConfirmar.TextOffset = new System.Drawing.Point(23, 0);
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnPagosRegistrados
            // 
            this.btnPagosRegistrados.AutoRoundedCorners = true;
            this.btnPagosRegistrados.BorderRadius = 13;
            this.btnPagosRegistrados.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPagosRegistrados.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPagosRegistrados.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPagosRegistrados.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPagosRegistrados.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnPagosRegistrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagosRegistrados.ForeColor = System.Drawing.Color.White;
            this.btnPagosRegistrados.Location = new System.Drawing.Point(1034, 56);
            this.btnPagosRegistrados.Name = "btnPagosRegistrados";
            this.btnPagosRegistrados.Size = new System.Drawing.Size(150, 28);
            this.btnPagosRegistrados.TabIndex = 224;
            this.btnPagosRegistrados.Text = "Pagos Registrados";
            this.btnPagosRegistrados.Click += new System.EventHandler(this.btnPagosRegistrados_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.AutoRoundedCorners = true;
            this.btnBuscar.BorderRadius = 13;
            this.btnBuscar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBuscar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBuscar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBuscar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBuscar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(1034, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtAlumno
            // 
            this.txtAlumno.AutoRoundedCorners = true;
            this.txtAlumno.BorderColor = System.Drawing.Color.Gray;
            this.txtAlumno.BorderRadius = 10;
            this.txtAlumno.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAlumno.DefaultText = "";
            this.txtAlumno.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAlumno.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAlumno.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlumno.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlumno.Enabled = false;
            this.txtAlumno.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlumno.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlumno.ForeColor = System.Drawing.Color.Black;
            this.txtAlumno.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlumno.Location = new System.Drawing.Point(578, 48);
            this.txtAlumno.Name = "txtAlumno";
            this.txtAlumno.PlaceholderText = "";
            this.txtAlumno.SelectedText = "";
            this.txtAlumno.Size = new System.Drawing.Size(430, 23);
            this.txtAlumno.TabIndex = 213;
            // 
            // txtMatricula
            // 
            this.txtMatricula.AutoRoundedCorners = true;
            this.txtMatricula.BorderColor = System.Drawing.Color.Gray;
            this.txtMatricula.BorderRadius = 10;
            this.txtMatricula.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatricula.DefaultText = "";
            this.txtMatricula.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatricula.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatricula.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatricula.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatricula.Enabled = false;
            this.txtMatricula.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatricula.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatricula.ForeColor = System.Drawing.Color.Black;
            this.txtMatricula.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatricula.Location = new System.Drawing.Point(385, 48);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.PlaceholderText = "";
            this.txtMatricula.SelectedText = "";
            this.txtMatricula.Size = new System.Drawing.Size(99, 23);
            this.txtMatricula.TabIndex = 213;
            this.txtMatricula.TextChanged += new System.EventHandler(this.txtMatricula_TextChanged);
            // 
            // txtCaja
            // 
            this.txtCaja.AutoRoundedCorners = true;
            this.txtCaja.BorderColor = System.Drawing.Color.Gray;
            this.txtCaja.BorderRadius = 10;
            this.txtCaja.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCaja.DefaultText = "";
            this.txtCaja.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCaja.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCaja.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCaja.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCaja.Enabled = false;
            this.txtCaja.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCaja.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaja.ForeColor = System.Drawing.Color.Black;
            this.txtCaja.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCaja.Location = new System.Drawing.Point(140, 48);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.PlaceholderText = "";
            this.txtCaja.SelectedText = "";
            this.txtCaja.Size = new System.Drawing.Size(87, 23);
            this.txtCaja.TabIndex = 213;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(660, 408);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 210;
            this.label4.Text = "Total de Saldo:";
            // 
            // dgvPagosPendientes
            // 
            this.dgvPagosPendientes.AllowUserToAddRows = false;
            this.dgvPagosPendientes.AllowUserToDeleteRows = false;
            this.dgvPagosPendientes.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPagosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPagosPendientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagosPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagosPendientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.Documento,
            this.FolioDocumento,
            this.Consecutivo,
            this.Propiedad1,
            this.Concepto,
            this.Fecha,
            this.Importe,
            this.SaldoActual,
            this.Vencimiento,
            this.MasDescuentos,
            this.MenosDescuentos,
            this.Descuento,
            this.Saldo});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagosPendientes.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPagosPendientes.EnableHeadersVisualStyles = false;
            this.dgvPagosPendientes.Location = new System.Drawing.Point(11, 132);
            this.dgvPagosPendientes.MultiSelect = false;
            this.dgvPagosPendientes.Name = "dgvPagosPendientes";
            this.dgvPagosPendientes.RowHeadersVisible = false;
            this.dgvPagosPendientes.RowHeadersWidth = 45;
            this.dgvPagosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagosPendientes.Size = new System.Drawing.Size(1172, 258);
            this.dgvPagosPendientes.TabIndex = 209;
            this.dgvPagosPendientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellContentClick_1);
            this.dgvPagosPendientes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagosPendientes_CellEndEdit_1);
            this.dgvPagosPendientes.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvPagosPendientes_CurrentCellDirtyStateChanged_1);
            // 
            // txtFechaRecargo
            // 
            this.txtFechaRecargo.Enabled = false;
            this.txtFechaRecargo.Location = new System.Drawing.Point(565, 438);
            this.txtFechaRecargo.Name = "txtFechaRecargo";
            this.txtFechaRecargo.Size = new System.Drawing.Size(103, 20);
            this.txtFechaRecargo.TabIndex = 208;
            this.txtFechaRecargo.TabStop = false;
            this.txtFechaRecargo.Visible = false;
            // 
            // txtCalculo
            // 
            this.txtCalculo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCalculo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCalculo.Enabled = false;
            this.txtCalculo.Location = new System.Drawing.Point(385, 438);
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
            this.txtConcepto.Location = new System.Drawing.Point(205, 438);
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
            this.txtDiarioMensual.Location = new System.Drawing.Point(205, 490);
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
            this.txtImporteConcepto.Location = new System.Drawing.Point(385, 516);
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
            this.txtPorcentaje.Location = new System.Drawing.Point(385, 491);
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
            this.txtImporte.Location = new System.Drawing.Point(385, 465);
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
            this.txtGenerarRecargo.Location = new System.Drawing.Point(205, 465);
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
            this.txtImportePorcentaje.Location = new System.Drawing.Point(205, 516);
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
            this.txtMontoD.Location = new System.Drawing.Point(20, 541);
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
            this.txtMontoR.Location = new System.Drawing.Point(20, 516);
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
            this.txtDescuentosSiNo.Location = new System.Drawing.Point(20, 490);
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
            this.txtFecha.Location = new System.Drawing.Point(20, 438);
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
            this.txtRecargosSiNo.Location = new System.Drawing.Point(20, 464);
            this.txtRecargosSiNo.Name = "txtRecargosSiNo";
            this.txtRecargosSiNo.Size = new System.Drawing.Size(174, 20);
            this.txtRecargosSiNo.TabIndex = 61;
            this.txtRecargosSiNo.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(940, 457);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 17);
            this.label15.TabIndex = 54;
            this.label15.Text = "- Descuentos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(940, 482);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 17);
            this.label14.TabIndex = 53;
            this.label14.Text = "Total";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(940, 431);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 52;
            this.label12.Text = "+ Cargos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(940, 408);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 17);
            this.label11.TabIndex = 51;
            this.label11.Text = "Sub Total";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(200, 18);
            this.label9.TabIndex = 25;
            this.label9.Text = "Lista de Pagos pendientes:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(334, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Clave:";
            // 
            // lblTipoPersona
            // 
            this.lblTipoPersona.AutoSize = true;
            this.lblTipoPersona.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPersona.Location = new System.Drawing.Point(494, 51);
            this.lblTipoPersona.Name = "lblTipoPersona";
            this.lblTipoPersona.Size = new System.Drawing.Size(53, 17);
            this.lblTipoPersona.TabIndex = 21;
            this.lblTipoPersona.Text = "Cliente";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(0, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1196, 10);
            this.label5.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(164, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Caja";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos para registro y control del pago";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 20;
            this.guna2Elipse2.TargetControl = this.panel1;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.Controls.Add(this.guna2Button14);
            this.guna2GradientPanel1.Controls.Add(this.txtFolioPedido);
            this.guna2GradientPanel1.Controls.Add(this.label18);
            this.guna2GradientPanel1.Controls.Add(this.guna2CircleButton1);
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1232, 75);
            this.guna2GradientPanel1.TabIndex = 96;
            // 
            // guna2Button14
            // 
            this.guna2Button14.Animated = true;
            this.guna2Button14.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button14.BorderRadius = 20;
            this.guna2Button14.CheckedState.FillColor = System.Drawing.Color.Black;
            this.guna2Button14.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button14.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button14.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button14.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button14.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button14.ForeColor = System.Drawing.Color.White;
            this.guna2Button14.ImageSize = new System.Drawing.Size(35, 35);
            this.guna2Button14.Location = new System.Drawing.Point(527, 5);
            this.guna2Button14.Name = "guna2Button14";
            this.guna2Button14.Size = new System.Drawing.Size(65, 65);
            this.guna2Button14.TabIndex = 265;
            // 
            // txtFolioPedido
            // 
            this.txtFolioPedido.Enabled = false;
            this.txtFolioPedido.Location = new System.Drawing.Point(740, 27);
            this.txtFolioPedido.Name = "txtFolioPedido";
            this.txtFolioPedido.Size = new System.Drawing.Size(74, 20);
            this.txtFolioPedido.TabIndex = 264;
            this.txtFolioPedido.TabStop = false;
            this.txtFolioPedido.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(49, 17);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(198, 30);
            this.label18.TabIndex = 80;
            this.label18.Text = "Registrar Cobranza";
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
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(60, 60);
            this.guna2CircleButton1.Location = new System.Drawing.Point(1141, 2);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 70);
            this.guna2CircleButton1.TabIndex = 79;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // Seleccionar
            // 
            this.Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Seleccionar.HeaderText = "Selec";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.Width = 49;
            // 
            // Documento
            // 
            this.Documento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Documento.HeaderText = "Documento";
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            // 
            // FolioDocumento
            // 
            this.FolioDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FolioDocumento.HeaderText = "Folio";
            this.FolioDocumento.Name = "FolioDocumento";
            this.FolioDocumento.ReadOnly = true;
            this.FolioDocumento.Visible = false;
            this.FolioDocumento.Width = 62;
            // 
            // Consecutivo
            // 
            this.Consecutivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Consecutivo.HeaderText = "Folio";
            this.Consecutivo.Name = "Consecutivo";
            this.Consecutivo.ReadOnly = true;
            this.Consecutivo.Width = 62;
            // 
            // Propiedad1
            // 
            this.Propiedad1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Propiedad1.HeaderText = "Propiedad";
            this.Propiedad1.Name = "Propiedad1";
            this.Propiedad1.Visible = false;
            this.Propiedad1.Width = 97;
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
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 72;
            // 
            // Importe
            // 
            this.Importe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Importe.DefaultCellStyle = dataGridViewCellStyle2;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 80;
            // 
            // SaldoActual
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.SaldoActual.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaldoActual.HeaderText = "Saldo";
            this.SaldoActual.Name = "SaldoActual";
            this.SaldoActual.ReadOnly = true;
            this.SaldoActual.Width = 117;
            // 
            // Vencimiento
            // 
            this.Vencimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Vencimiento.HeaderText = "Vencimiento";
            this.Vencimiento.Name = "Vencimiento";
            this.Vencimiento.ReadOnly = true;
            this.Vencimiento.Width = 110;
            // 
            // MasDescuentos
            // 
            this.MasDescuentos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MasDescuentos.HeaderText = "+";
            this.MasDescuentos.Name = "MasDescuentos";
            this.MasDescuentos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MasDescuentos.Text = "+";
            this.MasDescuentos.UseColumnTextForButtonValue = true;
            this.MasDescuentos.Visible = false;
            this.MasDescuentos.Width = 21;
            // 
            // MenosDescuentos
            // 
            this.MenosDescuentos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MenosDescuentos.HeaderText = "-";
            this.MenosDescuentos.Name = "MenosDescuentos";
            this.MenosDescuentos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MenosDescuentos.Text = "-";
            this.MenosDescuentos.UseColumnTextForButtonValue = true;
            this.MenosDescuentos.Visible = false;
            this.MenosDescuentos.Width = 17;
            // 
            // Descuento
            // 
            this.Descuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = "0.00";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Descuento.DefaultCellStyle = dataGridViewCellStyle4;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            this.Descuento.Visible = false;
            this.Descuento.Width = 102;
            // 
            // Saldo
            // 
            this.Saldo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = "0.00";
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle5;
            this.Saldo.HeaderText = "Saldo (R + D)";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Visible = false;
            this.Saldo.Width = 121;
            // 
            // registroIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(1232, 682);
            this.ControlBox = false;
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "registroIngresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " SICC - Registra Cobranza";
            this.Activated += new System.EventHandler(this.registroIngresos_Activated);
            this.Load += new System.EventHandler(this.registroIngresos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).EndInit();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTipoPersona;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.TextBox txtCalculo;
        private System.Windows.Forms.TextBox txtFechaRecargo;
        private System.Windows.Forms.DataGridView dgvPagosPendientes;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtCaja;
        private Guna.UI2.WinForms.Guna2Button btnPagosRegistrados;
        private Guna.UI2.WinForms.Guna2Button btnBuscar;
        private Guna.UI2.WinForms.Guna2Button btnConfirmar;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2TextBox txtTotal;
        private Guna.UI2.WinForms.Guna2TextBox txtDescuentos;
        private Guna.UI2.WinForms.Guna2TextBox txtRecargos;
        private Guna.UI2.WinForms.Guna2TextBox txtSubtotal;
        private Guna.UI2.WinForms.Guna2TextBox txtTotalRecibo;
        public Guna.UI2.WinForms.Guna2TextBox txtAlumno;
        public Guna.UI2.WinForms.Guna2TextBox txtMatricula;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button14;
        private System.Windows.Forms.TextBox txtFolioPedido;
        private System.Windows.Forms.Label label18;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2Button btnLimpiar;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFecha;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Consecutivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vencimiento;
        private System.Windows.Forms.DataGridViewButtonColumn MasDescuentos;
        private System.Windows.Forms.DataGridViewButtonColumn MenosDescuentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
    }
}