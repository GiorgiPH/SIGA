namespace PV
{
    partial class ReporteDiarioComprasFiltro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteDiarioComprasFiltro));
            this.dtFecha2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnProveedor = new System.Windows.Forms.Panel();
            this.cmbPropietario1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnCuentaBancaria = new System.Windows.Forms.Panel();
            this.cmbCuentaBancaria = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pnCliente = new System.Windows.Forms.Panel();
            this.cmbCliente = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnTipoDocumento = new System.Windows.Forms.Panel();
            this.cmbTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnCentroCostos = new System.Windows.Forms.Panel();
            this.cmbCentroCostos = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnProyecto = new System.Windows.Forms.Panel();
            this.cmbProyecto = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnFechas = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2ToggleSwitch1 = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button9 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnProveedor.SuspendLayout();
            this.pnCuentaBancaria.SuspendLayout();
            this.pnCliente.SuspendLayout();
            this.pnTipoDocumento.SuspendLayout();
            this.pnCentroCostos.SuspendLayout();
            this.pnProyecto.SuspendLayout();
            this.pnFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtFecha2
            // 
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(317, 9);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(116, 20);
            this.dtFecha2.TabIndex = 104;
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(278, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "Al";
            // 
            // dtFecha1
            // 
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(156, 9);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(106, 20);
            this.dtFecha1.TabIndex = 102;
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 101;
            this.label3.Text = "Fecha:";
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.Controls.Add(this.guna2CircleButton1);
            this.guna2GradientPanel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(589, 75);
            this.guna2GradientPanel1.TabIndex = 112;
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
            this.guna2CircleButton1.Location = new System.Drawing.Point(498, 2);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 70);
            this.guna2CircleButton1.TabIndex = 79;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(50, 16);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(64, 32);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "Filtros";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.Controls.Add(this.flowLayoutPanel1);
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Controls.Add(this.guna2Button3);
            this.guna2Panel1.Controls.Add(this.guna2Button9);
            this.guna2Panel1.Controls.Add(this.guna2Separator1);
            this.guna2Panel1.CustomizableEdges.TopLeft = false;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(18, 81);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(550, 402);
            this.guna2Panel1.TabIndex = 113;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnProveedor);
            this.flowLayoutPanel1.Controls.Add(this.pnCuentaBancaria);
            this.flowLayoutPanel1.Controls.Add(this.pnCliente);
            this.flowLayoutPanel1.Controls.Add(this.pnTipoDocumento);
            this.flowLayoutPanel1.Controls.Add(this.pnCentroCostos);
            this.flowLayoutPanel1.Controls.Add(this.pnProyecto);
            this.flowLayoutPanel1.Controls.Add(this.pnFechas);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(503, 299);
            this.flowLayoutPanel1.TabIndex = 262;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pnProveedor
            // 
            this.pnProveedor.Controls.Add(this.cmbPropietario1);
            this.pnProveedor.Controls.Add(this.label1);
            this.pnProveedor.Location = new System.Drawing.Point(3, 3);
            this.pnProveedor.Name = "pnProveedor";
            this.pnProveedor.Size = new System.Drawing.Size(495, 36);
            this.pnProveedor.TabIndex = 263;
            // 
            // cmbPropietario1
            // 
            this.cmbPropietario1.AutoRoundedCorners = true;
            this.cmbPropietario1.BackColor = System.Drawing.Color.Transparent;
            this.cmbPropietario1.BorderColor = System.Drawing.Color.Gray;
            this.cmbPropietario1.BorderRadius = 12;
            this.cmbPropietario1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPropietario1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPropietario1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPropietario1.ForeColor = System.Drawing.Color.Black;
            this.cmbPropietario1.IntegralHeight = false;
            this.cmbPropietario1.ItemHeight = 20;
            this.cmbPropietario1.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbPropietario1.Location = new System.Drawing.Point(89, 5);
            this.cmbPropietario1.Name = "cmbPropietario1";
            this.cmbPropietario1.Size = new System.Drawing.Size(393, 26);
            this.cmbPropietario1.TabIndex = 257;
            this.cmbPropietario1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbPropietario1.SelectedIndexChanged += new System.EventHandler(this.cmbPropietario1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "Proveedor:";
            // 
            // pnCuentaBancaria
            // 
            this.pnCuentaBancaria.Controls.Add(this.cmbCuentaBancaria);
            this.pnCuentaBancaria.Controls.Add(this.label11);
            this.pnCuentaBancaria.Location = new System.Drawing.Point(3, 45);
            this.pnCuentaBancaria.Name = "pnCuentaBancaria";
            this.pnCuentaBancaria.Size = new System.Drawing.Size(495, 36);
            this.pnCuentaBancaria.TabIndex = 265;
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
            this.cmbCuentaBancaria.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbCuentaBancaria.Location = new System.Drawing.Point(110, 5);
            this.cmbCuentaBancaria.Name = "cmbCuentaBancaria";
            this.cmbCuentaBancaria.Size = new System.Drawing.Size(372, 26);
            this.cmbCuentaBancaria.TabIndex = 257;
            this.cmbCuentaBancaria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 114;
            this.label11.Text = "Cuenta Bancaria";
            // 
            // pnCliente
            // 
            this.pnCliente.Controls.Add(this.cmbCliente);
            this.pnCliente.Controls.Add(this.label9);
            this.pnCliente.Location = new System.Drawing.Point(3, 87);
            this.pnCliente.Name = "pnCliente";
            this.pnCliente.Size = new System.Drawing.Size(495, 36);
            this.pnCliente.TabIndex = 264;
            // 
            // cmbCliente
            // 
            this.cmbCliente.AutoRoundedCorners = true;
            this.cmbCliente.BackColor = System.Drawing.Color.Transparent;
            this.cmbCliente.BorderColor = System.Drawing.Color.Gray;
            this.cmbCliente.BorderRadius = 12;
            this.cmbCliente.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCliente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCliente.ForeColor = System.Drawing.Color.Black;
            this.cmbCliente.IntegralHeight = false;
            this.cmbCliente.ItemHeight = 20;
            this.cmbCliente.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbCliente.Location = new System.Drawing.Point(89, 5);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(393, 26);
            this.cmbCliente.TabIndex = 257;
            this.cmbCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 114;
            this.label9.Text = "Cliente:";
            // 
            // pnTipoDocumento
            // 
            this.pnTipoDocumento.Controls.Add(this.cmbTipo);
            this.pnTipoDocumento.Controls.Add(this.label5);
            this.pnTipoDocumento.Location = new System.Drawing.Point(3, 129);
            this.pnTipoDocumento.Name = "pnTipoDocumento";
            this.pnTipoDocumento.Size = new System.Drawing.Size(495, 36);
            this.pnTipoDocumento.TabIndex = 263;
            // 
            // cmbTipo
            // 
            this.cmbTipo.AutoRoundedCorners = true;
            this.cmbTipo.BackColor = System.Drawing.Color.Transparent;
            this.cmbTipo.BorderColor = System.Drawing.Color.Gray;
            this.cmbTipo.BorderRadius = 12;
            this.cmbTipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTipo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipo.ForeColor = System.Drawing.Color.Black;
            this.cmbTipo.IntegralHeight = false;
            this.cmbTipo.ItemHeight = 20;
            this.cmbTipo.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbTipo.Location = new System.Drawing.Point(124, 5);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(358, 26);
            this.cmbTipo.TabIndex = 256;
            this.cmbTipo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "Tipo Documento:";
            // 
            // pnCentroCostos
            // 
            this.pnCentroCostos.Controls.Add(this.cmbCentroCostos);
            this.pnCentroCostos.Controls.Add(this.label6);
            this.pnCentroCostos.Location = new System.Drawing.Point(3, 171);
            this.pnCentroCostos.Name = "pnCentroCostos";
            this.pnCentroCostos.Size = new System.Drawing.Size(495, 36);
            this.pnCentroCostos.TabIndex = 263;
            // 
            // cmbCentroCostos
            // 
            this.cmbCentroCostos.AutoRoundedCorners = true;
            this.cmbCentroCostos.BackColor = System.Drawing.Color.Transparent;
            this.cmbCentroCostos.BorderColor = System.Drawing.Color.Gray;
            this.cmbCentroCostos.BorderRadius = 12;
            this.cmbCentroCostos.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCentroCostos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCentroCostos.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCentroCostos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCentroCostos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCentroCostos.ForeColor = System.Drawing.Color.Black;
            this.cmbCentroCostos.IntegralHeight = false;
            this.cmbCentroCostos.ItemHeight = 20;
            this.cmbCentroCostos.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbCentroCostos.Location = new System.Drawing.Point(110, 5);
            this.cmbCentroCostos.Name = "cmbCentroCostos";
            this.cmbCentroCostos.Size = new System.Drawing.Size(372, 26);
            this.cmbCentroCostos.TabIndex = 257;
            this.cmbCentroCostos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbCentroCostos.SelectedIndexChanged += new System.EventHandler(this.cmbCentroCostos_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 114;
            this.label6.Text = "Centro Costos:";
            // 
            // pnProyecto
            // 
            this.pnProyecto.Controls.Add(this.cmbProyecto);
            this.pnProyecto.Controls.Add(this.label10);
            this.pnProyecto.Location = new System.Drawing.Point(3, 213);
            this.pnProyecto.Name = "pnProyecto";
            this.pnProyecto.Size = new System.Drawing.Size(495, 36);
            this.pnProyecto.TabIndex = 264;
            // 
            // cmbProyecto
            // 
            this.cmbProyecto.AutoRoundedCorners = true;
            this.cmbProyecto.BackColor = System.Drawing.Color.Transparent;
            this.cmbProyecto.BorderColor = System.Drawing.Color.Gray;
            this.cmbProyecto.BorderRadius = 12;
            this.cmbProyecto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProyecto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProyecto.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbProyecto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbProyecto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbProyecto.ForeColor = System.Drawing.Color.Black;
            this.cmbProyecto.IntegralHeight = false;
            this.cmbProyecto.ItemHeight = 20;
            this.cmbProyecto.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbProyecto.Location = new System.Drawing.Point(110, 5);
            this.cmbProyecto.Name = "cmbProyecto";
            this.cmbProyecto.Size = new System.Drawing.Size(372, 26);
            this.cmbProyecto.TabIndex = 257;
            this.cmbProyecto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 114;
            this.label10.Text = "Proyecto";
            // 
            // pnFechas
            // 
            this.pnFechas.Controls.Add(this.dtFecha2);
            this.pnFechas.Controls.Add(this.label4);
            this.pnFechas.Controls.Add(this.dtFecha1);
            this.pnFechas.Controls.Add(this.label2);
            this.pnFechas.Controls.Add(this.guna2ToggleSwitch1);
            this.pnFechas.Controls.Add(this.label3);
            this.pnFechas.Location = new System.Drawing.Point(3, 255);
            this.pnFechas.Name = "pnFechas";
            this.pnFechas.Size = new System.Drawing.Size(495, 36);
            this.pnFechas.TabIndex = 263;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 261;
            this.label2.Text = "No";
            // 
            // guna2ToggleSwitch1
            // 
            this.guna2ToggleSwitch1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ToggleSwitch1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ToggleSwitch1.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.guna2ToggleSwitch1.CheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2ToggleSwitch1.Location = new System.Drawing.Point(65, 9);
            this.guna2ToggleSwitch1.Name = "guna2ToggleSwitch1";
            this.guna2ToggleSwitch1.Size = new System.Drawing.Size(35, 20);
            this.guna2ToggleSwitch1.TabIndex = 260;
            this.guna2ToggleSwitch1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2ToggleSwitch1.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2ToggleSwitch1.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.guna2ToggleSwitch1.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.guna2ToggleSwitch1.CheckedChanged += new System.EventHandler(this.guna2ToggleSwitch1_CheckedChanged);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button1.BorderRadius = 20;
            this.guna2Button1.BorderThickness = 1;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button1.Image = global::PV.Properties.Resources.cancelar;
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.Location = new System.Drawing.Point(259, 342);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(128, 46);
            this.guna2Button1.TabIndex = 255;
            this.guna2Button1.Text = "Cancelar";
            this.guna2Button1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.TextOffset = new System.Drawing.Point(10, 0);
            this.guna2Button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // guna2Button3
            // 
            this.guna2Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.guna2Button3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button3.BorderRadius = 20;
            this.guna2Button3.BorderThickness = 1;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button3.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button3.Image")));
            this.guna2Button3.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button3.ImageSize = new System.Drawing.Size(48, 48);
            this.guna2Button3.Location = new System.Drawing.Point(393, 342);
            this.guna2Button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(128, 46);
            this.guna2Button3.TabIndex = 254;
            this.guna2Button3.Text = "Confirmar";
            this.guna2Button3.TextOffset = new System.Drawing.Point(23, 0);
            this.guna2Button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // guna2Button9
            // 
            this.guna2Button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.guna2Button9.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button9.BorderRadius = 20;
            this.guna2Button9.BorderThickness = 1;
            this.guna2Button9.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button9.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button9.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button9.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button9.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Button9.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button9.Image")));
            this.guna2Button9.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button9.ImageSize = new System.Drawing.Size(48, 48);
            this.guna2Button9.Location = new System.Drawing.Point(829, 351);
            this.guna2Button9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Button9.Name = "guna2Button9";
            this.guna2Button9.Size = new System.Drawing.Size(128, 46);
            this.guna2Button9.TabIndex = 236;
            this.guna2Button9.Text = "Terminar";
            this.guna2Button9.TextOffset = new System.Drawing.Point(23, 0);
            this.guna2Button9.Visible = false;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Separator1.FillThickness = 5;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 0);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(283, 10);
            this.guna2Separator1.TabIndex = 253;
            // 
            // ReporteDiarioComprasFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 495);
            this.ControlBox = false;
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReporteDiarioComprasFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Diario de Compras";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReporteDiarioComprasFiltro_FormClosing);
            this.Load += new System.EventHandler(this.ReporteDiarioComprasFiltro_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnProveedor.ResumeLayout(false);
            this.pnProveedor.PerformLayout();
            this.pnCuentaBancaria.ResumeLayout(false);
            this.pnCuentaBancaria.PerformLayout();
            this.pnCliente.ResumeLayout(false);
            this.pnCliente.PerformLayout();
            this.pnTipoDocumento.ResumeLayout(false);
            this.pnTipoDocumento.PerformLayout();
            this.pnCentroCostos.ResumeLayout(false);
            this.pnCentroCostos.PerformLayout();
            this.pnProyecto.ResumeLayout(false);
            this.pnProyecto.PerformLayout();
            this.pnFechas.ResumeLayout(false);
            this.pnFechas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtFecha2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFecha1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button9;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPropietario1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTipo;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ToggleSwitch guna2ToggleSwitch1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnProveedor;
        private System.Windows.Forms.Panel pnTipoDocumento;
        private System.Windows.Forms.Panel pnFechas;
        private System.Windows.Forms.Panel pnCliente;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnCentroCostos;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCentroCostos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnProyecto;
        private Guna.UI2.WinForms.Guna2ComboBox cmbProyecto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnCuentaBancaria;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCuentaBancaria;
        private System.Windows.Forms.Label label11;
    }
}