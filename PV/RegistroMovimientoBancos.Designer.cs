namespace PV
{
    partial class RegistroMovimientoBancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistroMovimientoBancos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txtImporte = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNotas = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbCuentaBancaria = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbConcepto = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpFecha = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnConsultarMovimeintosRecientes = new Guna.UI2.WinForms.Guna2Button();
            this.btnReporteMovimientos = new Guna.UI2.WinForms.Guna2Button();
            this.btnNuevoMovimiento = new Guna.UI2.WinForms.Guna2Button();
            this.PanelUsuario = new System.Windows.Forms.Panel();
            this.dgvMovimientosRecientes = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnLimpiarMovimiento = new Guna.UI2.WinForms.Guna2Button();
            this.btnConfirmarMovimiento = new Guna.UI2.WinForms.Guna2Button();
            this.groupBox1.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.PanelUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosRecientes)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.BorderRadius = 20;
            this.groupBox1.BorderThickness = 0;
            this.groupBox1.Controls.Add(this.txtImporte);
            this.groupBox1.Controls.Add(this.txtNotas);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.cmbCuentaBancaria);
            this.groupBox1.Controls.Add(this.cmbConcepto);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.guna2Separator1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.groupBox1.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.groupBox1.CustomizableEdges.TopLeft = false;
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.groupBox1.Location = new System.Drawing.Point(21, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 335);
            this.groupBox1.TabIndex = 77;
            // 
            // txtImporte
            // 
            this.txtImporte.AutoRoundedCorners = true;
            this.txtImporte.BorderColor = System.Drawing.Color.Gray;
            this.txtImporte.BorderRadius = 11;
            this.txtImporte.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtImporte.DefaultText = "0.00";
            this.txtImporte.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtImporte.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtImporte.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtImporte.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtImporte.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtImporte.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtImporte.ForeColor = System.Drawing.Color.Black;
            this.txtImporte.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtImporte.Location = new System.Drawing.Point(436, 153);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.PlaceholderText = "";
            this.txtImporte.SelectedText = "";
            this.txtImporte.Size = new System.Drawing.Size(105, 25);
            this.txtImporte.TabIndex = 355;
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImporte.TextOffset = new System.Drawing.Point(5, 0);
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            // 
            // txtNotas
            // 
            this.txtNotas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotas.BorderColor = System.Drawing.Color.Gray;
            this.txtNotas.BorderRadius = 15;
            this.txtNotas.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNotas.DefaultText = "";
            this.txtNotas.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNotas.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNotas.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNotas.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNotas.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNotas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNotas.ForeColor = System.Drawing.Color.Black;
            this.txtNotas.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNotas.Location = new System.Drawing.Point(37, 226);
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.PlaceholderText = "";
            this.txtNotas.SelectedText = "";
            this.txtNotas.Size = new System.Drawing.Size(658, 63);
            this.txtNotas.TabIndex = 354;
            // 
            // cmbTipo
            // 
            this.cmbTipo.AutoRoundedCorners = true;
            this.cmbTipo.BackColor = System.Drawing.Color.Transparent;
            this.cmbTipo.BorderColor = System.Drawing.Color.Gray;
            this.cmbTipo.BorderRadius = 12;
            this.cmbTipo.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbTipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTipo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTipo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.ForeColor = System.Drawing.Color.Black;
            this.cmbTipo.IntegralHeight = false;
            this.cmbTipo.ItemHeight = 21;
            this.cmbTipo.Items.AddRange(new object[] {
            "Egreso",
            "Ingreso"});
            this.cmbTipo.Location = new System.Drawing.Point(434, 57);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(261, 27);
            this.cmbTipo.TabIndex = 353;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // cmbCuentaBancaria
            // 
            this.cmbCuentaBancaria.AutoRoundedCorners = true;
            this.cmbCuentaBancaria.BackColor = System.Drawing.Color.Transparent;
            this.cmbCuentaBancaria.BorderColor = System.Drawing.Color.Gray;
            this.cmbCuentaBancaria.BorderRadius = 12;
            this.cmbCuentaBancaria.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbCuentaBancaria.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCuentaBancaria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuentaBancaria.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuentaBancaria.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuentaBancaria.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCuentaBancaria.ForeColor = System.Drawing.Color.Black;
            this.cmbCuentaBancaria.IntegralHeight = false;
            this.cmbCuentaBancaria.ItemHeight = 21;
            this.cmbCuentaBancaria.Location = new System.Drawing.Point(44, 57);
            this.cmbCuentaBancaria.Name = "cmbCuentaBancaria";
            this.cmbCuentaBancaria.Size = new System.Drawing.Size(228, 27);
            this.cmbCuentaBancaria.TabIndex = 353;
            // 
            // cmbConcepto
            // 
            this.cmbConcepto.AutoRoundedCorners = true;
            this.cmbConcepto.BackColor = System.Drawing.Color.Transparent;
            this.cmbConcepto.BorderColor = System.Drawing.Color.Gray;
            this.cmbConcepto.BorderRadius = 12;
            this.cmbConcepto.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbConcepto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbConcepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcepto.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbConcepto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbConcepto.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConcepto.ForeColor = System.Drawing.Color.Black;
            this.cmbConcepto.IntegralHeight = false;
            this.cmbConcepto.ItemHeight = 21;
            this.cmbConcepto.Location = new System.Drawing.Point(37, 151);
            this.cmbConcepto.Name = "cmbConcepto";
            this.cmbConcepto.Size = new System.Drawing.Size(282, 27);
            this.cmbConcepto.TabIndex = 353;
            this.cmbConcepto.SelectedIndexChanged += new System.EventHandler(this.cmbConcepto_SelectedIndexChanged);
            // 
            // dtpFecha
            // 
            this.dtpFecha.AutoRoundedCorners = true;
            this.dtpFecha.BorderRadius = 11;
            this.dtpFecha.Checked = true;
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFecha.ForeColor = System.Drawing.Color.White;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(284, 58);
            this.dtpFecha.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFecha.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(119, 25);
            this.dtpFecha.TabIndex = 236;
            this.dtpFecha.Value = new System.DateTime(2022, 11, 4, 18, 45, 56, 112);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Separator1.FillThickness = 5;
            this.guna2Separator1.Location = new System.Drawing.Point(0, -4);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(385, 10);
            this.guna2Separator1.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(433, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 83;
            this.label6.Text = "IMPORTE:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(433, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 17);
            this.label5.TabIndex = 83;
            this.label5.Text = "TIPO DE MOVIMIENTO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(34, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "NOTAS:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(41, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "CUENTA BANCARIA:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(34, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "CONCEPTO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(281, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "FECHA:";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(61, 21);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(318, 32);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "Registrar Movimientos a Bancos";
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
            this.guna2GradientPanel1.Size = new System.Drawing.Size(867, 75);
            this.guna2GradientPanel1.TabIndex = 81;
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
            this.guna2CircleButton1.Location = new System.Drawing.Point(783, 5);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 70);
            this.guna2CircleButton1.TabIndex = 79;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.Controls.Add(this.btnConsultarMovimeintosRecientes);
            this.guna2Panel1.Controls.Add(this.btnReporteMovimientos);
            this.guna2Panel1.Controls.Add(this.btnNuevoMovimiento);
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2Panel1.Location = new System.Drawing.Point(783, 106);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(65, 339);
            this.guna2Panel1.TabIndex = 87;
            // 
            // btnConsultarMovimeintosRecientes
            // 
            this.btnConsultarMovimeintosRecientes.BackColor = System.Drawing.Color.Transparent;
            this.btnConsultarMovimeintosRecientes.BorderRadius = 20;
            this.btnConsultarMovimeintosRecientes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConsultarMovimeintosRecientes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConsultarMovimeintosRecientes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConsultarMovimeintosRecientes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConsultarMovimeintosRecientes.FillColor = System.Drawing.Color.Transparent;
            this.btnConsultarMovimeintosRecientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConsultarMovimeintosRecientes.ForeColor = System.Drawing.Color.White;
            this.btnConsultarMovimeintosRecientes.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarMovimeintosRecientes.Image")));
            this.btnConsultarMovimeintosRecientes.ImageSize = new System.Drawing.Size(35, 35);
            this.btnConsultarMovimeintosRecientes.Location = new System.Drawing.Point(2, 69);
            this.btnConsultarMovimeintosRecientes.Name = "btnConsultarMovimeintosRecientes";
            this.btnConsultarMovimeintosRecientes.Size = new System.Drawing.Size(65, 65);
            this.btnConsultarMovimeintosRecientes.TabIndex = 88;
            // 
            // btnReporteMovimientos
            // 
            this.btnReporteMovimientos.BackColor = System.Drawing.Color.Transparent;
            this.btnReporteMovimientos.BorderRadius = 20;
            this.btnReporteMovimientos.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReporteMovimientos.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReporteMovimientos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReporteMovimientos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReporteMovimientos.FillColor = System.Drawing.Color.Transparent;
            this.btnReporteMovimientos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReporteMovimientos.ForeColor = System.Drawing.Color.White;
            this.btnReporteMovimientos.Image = ((System.Drawing.Image)(resources.GetObject("btnReporteMovimientos.Image")));
            this.btnReporteMovimientos.ImageSize = new System.Drawing.Size(35, 35);
            this.btnReporteMovimientos.Location = new System.Drawing.Point(2, 131);
            this.btnReporteMovimientos.Name = "btnReporteMovimientos";
            this.btnReporteMovimientos.Size = new System.Drawing.Size(65, 65);
            this.btnReporteMovimientos.TabIndex = 86;
            // 
            // btnNuevoMovimiento
            // 
            this.btnNuevoMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.btnNuevoMovimiento.BorderRadius = 20;
            this.btnNuevoMovimiento.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNuevoMovimiento.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNuevoMovimiento.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNuevoMovimiento.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNuevoMovimiento.FillColor = System.Drawing.Color.Transparent;
            this.btnNuevoMovimiento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNuevoMovimiento.ForeColor = System.Drawing.Color.White;
            this.btnNuevoMovimiento.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoMovimiento.Image")));
            this.btnNuevoMovimiento.ImageSize = new System.Drawing.Size(35, 35);
            this.btnNuevoMovimiento.Location = new System.Drawing.Point(2, 6);
            this.btnNuevoMovimiento.Name = "btnNuevoMovimiento";
            this.btnNuevoMovimiento.Size = new System.Drawing.Size(65, 65);
            this.btnNuevoMovimiento.TabIndex = 87;
            // 
            // PanelUsuario
            // 
            this.PanelUsuario.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PanelUsuario.BackColor = System.Drawing.Color.White;
            this.PanelUsuario.Controls.Add(this.dgvMovimientosRecientes);
            this.PanelUsuario.Location = new System.Drawing.Point(220, 106);
            this.PanelUsuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanelUsuario.Name = "PanelUsuario";
            this.PanelUsuario.Size = new System.Drawing.Size(557, 339);
            this.PanelUsuario.TabIndex = 88;
            this.PanelUsuario.Visible = false;
            // 
            // dgvMovimientosRecientes
            // 
            this.dgvMovimientosRecientes.AllowUserToAddRows = false;
            this.dgvMovimientosRecientes.AllowUserToDeleteRows = false;
            this.dgvMovimientosRecientes.AllowUserToResizeColumns = false;
            this.dgvMovimientosRecientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            this.dgvMovimientosRecientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMovimientosRecientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvMovimientosRecientes.ColumnHeadersHeight = 19;
            this.dgvMovimientosRecientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(114)))), ((int)(((byte)(169)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMovimientosRecientes.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvMovimientosRecientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovimientosRecientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMovimientosRecientes.Location = new System.Drawing.Point(0, 0);
            this.dgvMovimientosRecientes.Name = "dgvMovimientosRecientes";
            this.dgvMovimientosRecientes.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMovimientosRecientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvMovimientosRecientes.RowHeadersVisible = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            this.dgvMovimientosRecientes.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvMovimientosRecientes.Size = new System.Drawing.Size(557, 339);
            this.dgvMovimientosRecientes.TabIndex = 0;
            this.dgvMovimientosRecientes.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMovimientosRecientes.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMovimientosRecientes.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMovimientosRecientes.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvMovimientosRecientes.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvMovimientosRecientes.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvMovimientosRecientes.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvMovimientosRecientes.ThemeStyle.HeaderStyle.Height = 19;
            this.dgvMovimientosRecientes.ThemeStyle.ReadOnly = true;
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.Height = 22;
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMovimientosRecientes.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // btnLimpiarMovimiento
            // 
            this.btnLimpiarMovimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpiarMovimiento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnLimpiarMovimiento.BorderRadius = 20;
            this.btnLimpiarMovimiento.BorderThickness = 1;
            this.btnLimpiarMovimiento.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLimpiarMovimiento.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLimpiarMovimiento.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLimpiarMovimiento.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLimpiarMovimiento.FillColor = System.Drawing.Color.Transparent;
            this.btnLimpiarMovimiento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarMovimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnLimpiarMovimiento.Image = global::PV.Properties.Resources.cancelar;
            this.btnLimpiarMovimiento.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLimpiarMovimiento.Location = new System.Drawing.Point(479, 466);
            this.btnLimpiarMovimiento.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLimpiarMovimiento.Name = "btnLimpiarMovimiento";
            this.btnLimpiarMovimiento.Size = new System.Drawing.Size(128, 46);
            this.btnLimpiarMovimiento.TabIndex = 109;
            this.btnLimpiarMovimiento.Text = "Cancelar";
            this.btnLimpiarMovimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLimpiarMovimiento.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // btnConfirmarMovimiento
            // 
            this.btnConfirmarMovimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarMovimiento.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnConfirmarMovimiento.BorderRadius = 20;
            this.btnConfirmarMovimiento.BorderThickness = 1;
            this.btnConfirmarMovimiento.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmarMovimiento.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConfirmarMovimiento.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConfirmarMovimiento.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConfirmarMovimiento.FillColor = System.Drawing.Color.Transparent;
            this.btnConfirmarMovimiento.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarMovimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnConfirmarMovimiento.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmarMovimiento.Image")));
            this.btnConfirmarMovimiento.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnConfirmarMovimiento.ImageSize = new System.Drawing.Size(48, 48);
            this.btnConfirmarMovimiento.Location = new System.Drawing.Point(613, 466);
            this.btnConfirmarMovimiento.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirmarMovimiento.Name = "btnConfirmarMovimiento";
            this.btnConfirmarMovimiento.Size = new System.Drawing.Size(128, 46);
            this.btnConfirmarMovimiento.TabIndex = 108;
            this.btnConfirmarMovimiento.Text = "Confirmar";
            this.btnConfirmarMovimiento.TextOffset = new System.Drawing.Point(23, 0);
            // 
            // RegistroMovimientoBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(867, 547);
            this.ControlBox = false;
            this.Controls.Add(this.PanelUsuario);
            this.Controls.Add(this.btnLimpiarMovimiento);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnConfirmarMovimiento);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RegistroMovimientoBancos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Catalogo de Almacenes";
            this.Load += new System.EventHandler(this.Almacenes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.PanelUsuario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosRecientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnConsultarMovimeintosRecientes;
        private Guna.UI2.WinForms.Guna2Button btnReporteMovimientos;
        private Guna.UI2.WinForms.Guna2Button btnNuevoMovimiento;
        private System.Windows.Forms.Panel PanelUsuario;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMovimientosRecientes;
        private Guna.UI2.WinForms.Guna2Button btnLimpiarMovimiento;
        private Guna.UI2.WinForms.Guna2Button btnConfirmarMovimiento;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFecha;
        private Guna.UI2.WinForms.Guna2ComboBox cmbConcepto;
        private Guna.UI2.WinForms.Guna2TextBox txtNotas;
        private Guna.UI2.WinForms.Guna2TextBox txtImporte;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTipo;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCuentaBancaria;
        private System.Windows.Forms.Label label1;
    }
}