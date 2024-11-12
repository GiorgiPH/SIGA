namespace PV
{
    partial class ReporteIngresoFormulario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteIngresoFormulario));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbpropietario2 = new System.Windows.Forms.ComboBox();
            this.cmbpropietario1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbFechas = new System.Windows.Forms.CheckBox();
            this.dtFecha2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFechasRecibo = new System.Windows.Forms.CheckBox();
            this.dtFechaRecibo2 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtFechaRecibo1 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnConfirmar = new Guna.UI2.WinForms.Guna2Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbDocumento = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbFormaPago = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbAnticipo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtClavePaciente = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFiltroNombre = new Guna.UI2.WinForms.Guna2TextBox();
            this.button5 = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCuenta = new Guna.UI2.WinForms.Guna2ComboBox();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propietario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "AL";
            this.label2.Visible = false;
            // 
            // cmbpropietario2
            // 
            this.cmbpropietario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario2.FormattingEnabled = true;
            this.cmbpropietario2.Location = new System.Drawing.Point(-11, 46);
            this.cmbpropietario2.Name = "cmbpropietario2";
            this.cmbpropietario2.Size = new System.Drawing.Size(65, 21);
            this.cmbpropietario2.TabIndex = 76;
            this.cmbpropietario2.Visible = false;
            this.cmbpropietario2.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario2_SelectedIndexChanged);
            // 
            // cmbpropietario1
            // 
            this.cmbpropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario1.FormattingEnabled = true;
            this.cmbpropietario1.Location = new System.Drawing.Point(107, 75);
            this.cmbpropietario1.Name = "cmbpropietario1";
            this.cmbpropietario1.Size = new System.Drawing.Size(44, 21);
            this.cmbpropietario1.TabIndex = 75;
            this.cmbpropietario1.Visible = false;
            this.cmbpropietario1.SelectedIndexChanged += new System.EventHandler(this.Propi1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "CLIENTES:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 81;
            this.label5.Text = "DOCUMENTO:";
            // 
            // cbFechas
            // 
            this.cbFechas.AutoSize = true;
            this.cbFechas.Location = new System.Drawing.Point(26, 320);
            this.cbFechas.Name = "cbFechas";
            this.cbFechas.Size = new System.Drawing.Size(15, 14);
            this.cbFechas.TabIndex = 87;
            this.cbFechas.UseVisualStyleBackColor = true;
            this.cbFechas.CheckedChanged += new System.EventHandler(this.cbFechas_CheckedChanged);
            // 
            // dtFecha2
            // 
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(321, 314);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(118, 20);
            this.dtFecha2.TabIndex = 86;
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged);
            this.dtFecha2.Leave += new System.EventHandler(this.dtFecha2_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(297, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 85;
            this.label4.Text = "Al";
            // 
            // dtFecha1
            // 
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(173, 314);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(118, 20);
            this.dtFecha1.TabIndex = 84;
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(45, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "FECHA DE PAGO:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 88;
            this.label7.Text = "FORMA DE PAGO:";
            // 
            // cbFechasRecibo
            // 
            this.cbFechasRecibo.AutoSize = true;
            this.cbFechasRecibo.Location = new System.Drawing.Point(26, 346);
            this.cbFechasRecibo.Name = "cbFechasRecibo";
            this.cbFechasRecibo.Size = new System.Drawing.Size(15, 14);
            this.cbFechasRecibo.TabIndex = 94;
            this.cbFechasRecibo.UseVisualStyleBackColor = true;
            this.cbFechasRecibo.CheckedChanged += new System.EventHandler(this.cbFechasRecibo_CheckedChanged);
            // 
            // dtFechaRecibo2
            // 
            this.dtFechaRecibo2.CustomFormat = "yyyy/MM/dd";
            this.dtFechaRecibo2.Enabled = false;
            this.dtFechaRecibo2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFechaRecibo2.Location = new System.Drawing.Point(321, 340);
            this.dtFechaRecibo2.Name = "dtFechaRecibo2";
            this.dtFechaRecibo2.Size = new System.Drawing.Size(118, 20);
            this.dtFechaRecibo2.TabIndex = 93;
            this.dtFechaRecibo2.Leave += new System.EventHandler(this.dtFechaRecibo2_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(297, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 92;
            this.label8.Text = "Al";
            // 
            // dtFechaRecibo1
            // 
            this.dtFechaRecibo1.CustomFormat = "yyyy/MM/dd";
            this.dtFechaRecibo1.Enabled = false;
            this.dtFechaRecibo1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFechaRecibo1.Location = new System.Drawing.Point(173, 340);
            this.dtFechaRecibo1.Name = "dtFechaRecibo1";
            this.dtFechaRecibo1.Size = new System.Drawing.Size(118, 20);
            this.dtFechaRecibo1.TabIndex = 91;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(45, 346);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 90;
            this.label9.Text = "FECHA DE RECIBO:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 99;
            this.label10.Text = "ANTICIPOS:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(45, 363);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(251, 26);
            this.label11.TabIndex = 101;
            this.label11.Text = "(Seleccionar esta opcion puede no coincidir con los\r\n importes totales del period" +
    "o seleccionado)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 168;
            this.label13.Text = "CLAVE:";
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.Controls.Add(this.guna2CircleButton1);
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel2);
            this.guna2GradientPanel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2GradientPanel1.Controls.Add(this.cmbpropietario2);
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2GradientPanel1.ForeColor = System.Drawing.Color.Black;
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(804, 55);
            this.guna2GradientPanel1.TabIndex = 216;
            this.guna2GradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2GradientPanel1_Paint);
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
            this.guna2CircleButton1.Location = new System.Drawing.Point(675, -3);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 70);
            this.guna2CircleButton1.TabIndex = 81;
            this.guna2CircleButton1.Visible = false;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2Panel2.Controls.Add(this.guna2Panel4);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Panel2.Location = new System.Drawing.Point(757, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(47, 55);
            this.guna2Panel2.TabIndex = 80;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel4.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(47, 29);
            this.guna2Panel4.TabIndex = 0;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.BorderRadius = 10;
            this.guna2ControlBox1.CustomizableEdges.BottomRight = false;
            this.guna2ControlBox1.CustomizableEdges.TopLeft = false;
            this.guna2ControlBox1.CustomizableEdges.TopRight = false;
            this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Red;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(2, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 0;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(81, 15);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(238, 27);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "REPORTE DE INGRESOS";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirmar.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.BorderColor = System.Drawing.Color.DodgerBlue;
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
            this.btnConfirmar.Location = new System.Drawing.Point(648, 369);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(142, 46);
            this.btnConfirmar.TabIndex = 244;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextOffset = new System.Drawing.Point(23, 0);
            this.btnConfirmar.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.Propietario});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(458, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(334, 301);
            this.dataGridView1.TabIndex = 245;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // cmbDocumento
            // 
            this.cmbDocumento.AutoRoundedCorners = true;
            this.cmbDocumento.BackColor = System.Drawing.Color.Transparent;
            this.cmbDocumento.BorderColor = System.Drawing.Color.Gray;
            this.cmbDocumento.BorderRadius = 12;
            this.cmbDocumento.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbDocumento.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumento.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDocumento.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDocumento.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDocumento.ForeColor = System.Drawing.Color.Black;
            this.cmbDocumento.IntegralHeight = false;
            this.cmbDocumento.ItemHeight = 21;
            this.cmbDocumento.Location = new System.Drawing.Point(138, 132);
            this.cmbDocumento.Name = "cmbDocumento";
            this.cmbDocumento.Size = new System.Drawing.Size(301, 27);
            this.cmbDocumento.TabIndex = 246;
            this.cmbDocumento.SelectedIndexChanged += new System.EventHandler(this.cmbDocumento_SelectedIndexChanged);
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.AutoRoundedCorners = true;
            this.cmbFormaPago.BackColor = System.Drawing.Color.Transparent;
            this.cmbFormaPago.BorderColor = System.Drawing.Color.Gray;
            this.cmbFormaPago.BorderRadius = 12;
            this.cmbFormaPago.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbFormaPago.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFormaPago.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFormaPago.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.ForeColor = System.Drawing.Color.Black;
            this.cmbFormaPago.IntegralHeight = false;
            this.cmbFormaPago.ItemHeight = 21;
            this.cmbFormaPago.Location = new System.Drawing.Point(138, 163);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(301, 27);
            this.cmbFormaPago.TabIndex = 246;
            this.cmbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cmbFormaPago_SelectedIndexChanged);
            // 
            // cbAnticipo
            // 
            this.cbAnticipo.AutoRoundedCorners = true;
            this.cbAnticipo.BackColor = System.Drawing.Color.Transparent;
            this.cbAnticipo.BorderColor = System.Drawing.Color.Gray;
            this.cbAnticipo.BorderRadius = 12;
            this.cbAnticipo.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cbAnticipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbAnticipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnticipo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbAnticipo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbAnticipo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAnticipo.ForeColor = System.Drawing.Color.Black;
            this.cbAnticipo.IntegralHeight = false;
            this.cbAnticipo.ItemHeight = 21;
            this.cbAnticipo.Items.AddRange(new object[] {
            "Si",
            "No"});
            this.cbAnticipo.Location = new System.Drawing.Point(138, 230);
            this.cbAnticipo.Name = "cbAnticipo";
            this.cbAnticipo.Size = new System.Drawing.Size(107, 27);
            this.cbAnticipo.TabIndex = 246;
            this.cbAnticipo.SelectedIndexChanged += new System.EventHandler(this.cbAnticipo_SelectedIndexChanged);
            // 
            // txtClavePaciente
            // 
            this.txtClavePaciente.AutoRoundedCorners = true;
            this.txtClavePaciente.BorderColor = System.Drawing.Color.Gray;
            this.txtClavePaciente.BorderRadius = 10;
            this.txtClavePaciente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtClavePaciente.DefaultText = "";
            this.txtClavePaciente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtClavePaciente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtClavePaciente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtClavePaciente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtClavePaciente.Enabled = false;
            this.txtClavePaciente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtClavePaciente.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClavePaciente.ForeColor = System.Drawing.Color.Black;
            this.txtClavePaciente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtClavePaciente.Location = new System.Drawing.Point(138, 102);
            this.txtClavePaciente.Name = "txtClavePaciente";
            this.txtClavePaciente.PasswordChar = '\0';
            this.txtClavePaciente.PlaceholderText = "";
            this.txtClavePaciente.SelectedText = "";
            this.txtClavePaciente.Size = new System.Drawing.Size(301, 23);
            this.txtClavePaciente.TabIndex = 247;
            // 
            // txtFiltroNombre
            // 
            this.txtFiltroNombre.AutoRoundedCorners = true;
            this.txtFiltroNombre.BorderColor = System.Drawing.Color.Gray;
            this.txtFiltroNombre.BorderRadius = 10;
            this.txtFiltroNombre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFiltroNombre.DefaultText = "";
            this.txtFiltroNombre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFiltroNombre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFiltroNombre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFiltroNombre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFiltroNombre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFiltroNombre.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroNombre.ForeColor = System.Drawing.Color.Black;
            this.txtFiltroNombre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFiltroNombre.Location = new System.Drawing.Point(138, 73);
            this.txtFiltroNombre.Name = "txtFiltroNombre";
            this.txtFiltroNombre.PasswordChar = '\0';
            this.txtFiltroNombre.PlaceholderText = "";
            this.txtFiltroNombre.SelectedText = "";
            this.txtFiltroNombre.Size = new System.Drawing.Size(301, 23);
            this.txtFiltroNombre.TabIndex = 248;
            this.txtFiltroNombre.TextChanged += new System.EventHandler(this.txtFiltroNombre_TextChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BorderColor = System.Drawing.Color.DodgerBlue;
            this.button5.BorderRadius = 20;
            this.button5.BorderThickness = 1;
            this.button5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button5.FillColor = System.Drawing.Color.White;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button5.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button5.ImageSize = new System.Drawing.Size(48, 48);
            this.button5.Location = new System.Drawing.Point(514, 369);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 46);
            this.button5.TabIndex = 249;
            this.button5.Text = "Cancelar";
            this.button5.TextOffset = new System.Drawing.Point(20, 0);
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "CUENTA BANCARIA:";
            // 
            // cmbCuenta
            // 
            this.cmbCuenta.AutoRoundedCorners = true;
            this.cmbCuenta.BackColor = System.Drawing.Color.Transparent;
            this.cmbCuenta.BorderColor = System.Drawing.Color.Gray;
            this.cmbCuenta.BorderRadius = 12;
            this.cmbCuenta.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbCuenta.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuenta.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuenta.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCuenta.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCuenta.ForeColor = System.Drawing.Color.Black;
            this.cmbCuenta.IntegralHeight = false;
            this.cmbCuenta.ItemHeight = 21;
            this.cmbCuenta.Location = new System.Drawing.Point(138, 197);
            this.cmbCuenta.Name = "cmbCuenta";
            this.cmbCuenta.Size = new System.Drawing.Size(301, 27);
            this.cmbCuenta.TabIndex = 246;
            this.cmbCuenta.SelectedIndexChanged += new System.EventHandler(this.cmbCuenta_SelectedIndexChanged);
            // 
            // Clave
            // 
            this.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 68;
            // 
            // Propietario
            // 
            this.Propietario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Propietario.HeaderText = "Cliente";
            this.Propietario.Name = "Propietario";
            this.Propietario.ReadOnly = true;
            // 
            // ReporteIngresoFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(804, 428);
            this.ControlBox = false;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtFiltroNombre);
            this.Controls.Add(this.txtClavePaciente);
            this.Controls.Add(this.cbAnticipo);
            this.Controls.Add(this.cmbCuenta);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.cmbDocumento);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbFechasRecibo);
            this.Controls.Add(this.dtFechaRecibo2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtFechaRecibo1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbFechas);
            this.Controls.Add(this.dtFecha2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtFecha1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbpropietario1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReporteIngresoFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SICC- Reporte de Ingresos";
            this.Load += new System.EventHandler(this.ReporteIngresoFormulario_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbpropietario2;
        private System.Windows.Forms.ComboBox cmbpropietario1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbFechas;
        private System.Windows.Forms.DateTimePicker dtFecha2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFecha1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbFechasRecibo;
        private System.Windows.Forms.DateTimePicker dtFechaRecibo2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtFechaRecibo1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnConfirmar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbDocumento;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFormaPago;
        private Guna.UI2.WinForms.Guna2ComboBox cbAnticipo;
        private Guna.UI2.WinForms.Guna2TextBox txtClavePaciente;
        private Guna.UI2.WinForms.Guna2TextBox txtFiltroNombre;
        private Guna.UI2.WinForms.Guna2Button button5;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propietario;
    }
}