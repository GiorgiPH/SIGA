namespace PV
{
    partial class RegistroEgreso
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistroEgreso));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpFecha = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.button6 = new Guna.UI2.WinForms.Guna2Button();
            this.button5 = new Guna.UI2.WinForms.Guna2Button();
            this.txtTotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDescuentos = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtRecargos = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSubtotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAlumno = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatricula = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCaja = new Guna.UI2.WinForms.Guna2TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvPagosPendientes = new System.Windows.Forms.DataGridView();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnReportePreeliminar = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).BeginInit();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnReportePreeliminar);
            this.panel1.Controls.Add(this.dtpFecha);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.txtDescuentos);
            this.panel1.Controls.Add(this.txtRecargos);
            this.panel1.Controls.Add(this.txtSubtotal);
            this.panel1.Controls.Add(this.txtAlumno);
            this.panel1.Controls.Add(this.txtMatricula);
            this.panel1.Controls.Add(this.txtCaja);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.dgvPagosPendientes);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 562);
            this.panel1.TabIndex = 37;
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
            this.dtpFecha.Location = new System.Drawing.Point(5, 48);
            this.dtpFecha.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFecha.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(119, 25);
            this.dtpFecha.TabIndex = 363;
            this.dtpFecha.Value = new System.DateTime(2024, 11, 11, 21, 47, 21, 59);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button6.BorderRadius = 20;
            this.button6.BorderThickness = 1;
            this.button6.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button6.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button6.FillColor = System.Drawing.Color.Transparent;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button6.Image = global::PV.Properties.Resources.cancelar;
            this.button6.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button6.Location = new System.Drawing.Point(681, 507);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(128, 46);
            this.button6.TabIndex = 362;
            this.button6.Text = "Cancelar";
            this.button6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button6.TextOffset = new System.Drawing.Point(10, 0);
            this.button6.Click += new System.EventHandler(this.button6_Click);
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
            this.button5.Location = new System.Drawing.Point(815, 507);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 46);
            this.button5.TabIndex = 359;
            this.button5.Text = "Terminar";
            this.button5.TextOffset = new System.Drawing.Point(23, 0);
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.AutoRoundedCorners = true;
            this.txtTotal.BorderColor = System.Drawing.Color.Gray;
            this.txtTotal.BorderRadius = 11;
            this.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTotal.DefaultText = "0.00";
            this.txtTotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTotal.Enabled = false;
            this.txtTotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtTotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTotal.Location = new System.Drawing.Point(808, 475);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.PlaceholderText = "";
            this.txtTotal.SelectedText = "";
            this.txtTotal.Size = new System.Drawing.Size(135, 25);
            this.txtTotal.TabIndex = 358;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // txtDescuentos
            // 
            this.txtDescuentos.AutoRoundedCorners = true;
            this.txtDescuentos.BorderColor = System.Drawing.Color.Gray;
            this.txtDescuentos.BorderRadius = 11;
            this.txtDescuentos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescuentos.DefaultText = "0.00";
            this.txtDescuentos.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDescuentos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDescuentos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescuentos.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescuentos.Enabled = false;
            this.txtDescuentos.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtDescuentos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescuentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtDescuentos.ForeColor = System.Drawing.Color.White;
            this.txtDescuentos.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescuentos.Location = new System.Drawing.Point(808, 449);
            this.txtDescuentos.Name = "txtDescuentos";
            this.txtDescuentos.PlaceholderText = "";
            this.txtDescuentos.SelectedText = "";
            this.txtDescuentos.Size = new System.Drawing.Size(135, 25);
            this.txtDescuentos.TabIndex = 358;
            this.txtDescuentos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDescuentos.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // txtRecargos
            // 
            this.txtRecargos.AutoRoundedCorners = true;
            this.txtRecargos.BorderColor = System.Drawing.Color.Gray;
            this.txtRecargos.BorderRadius = 11;
            this.txtRecargos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRecargos.DefaultText = "0.00";
            this.txtRecargos.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtRecargos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtRecargos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRecargos.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRecargos.Enabled = false;
            this.txtRecargos.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtRecargos.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRecargos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtRecargos.ForeColor = System.Drawing.Color.White;
            this.txtRecargos.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRecargos.Location = new System.Drawing.Point(807, 423);
            this.txtRecargos.Name = "txtRecargos";
            this.txtRecargos.PlaceholderText = "";
            this.txtRecargos.SelectedText = "";
            this.txtRecargos.Size = new System.Drawing.Size(135, 25);
            this.txtRecargos.TabIndex = 358;
            this.txtRecargos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRecargos.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.AutoRoundedCorners = true;
            this.txtSubtotal.BorderColor = System.Drawing.Color.Gray;
            this.txtSubtotal.BorderRadius = 11;
            this.txtSubtotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSubtotal.DefaultText = "0.00";
            this.txtSubtotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSubtotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSubtotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSubtotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.txtSubtotal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtSubtotal.ForeColor = System.Drawing.Color.White;
            this.txtSubtotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSubtotal.Location = new System.Drawing.Point(807, 397);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.PlaceholderText = "";
            this.txtSubtotal.SelectedText = "";
            this.txtSubtotal.Size = new System.Drawing.Size(135, 25);
            this.txtSubtotal.TabIndex = 358;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubtotal.TextOffset = new System.Drawing.Point(5, 0);
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
            this.txtAlumno.Location = new System.Drawing.Point(563, 48);
            this.txtAlumno.Name = "txtAlumno";
            this.txtAlumno.PlaceholderText = "";
            this.txtAlumno.SelectedText = "";
            this.txtAlumno.Size = new System.Drawing.Size(248, 25);
            this.txtAlumno.TabIndex = 335;
            this.txtAlumno.TextChanged += new System.EventHandler(this.txtMatricula_TextChanged);
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
            this.txtMatricula.Location = new System.Drawing.Point(383, 48);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.PlaceholderText = "";
            this.txtMatricula.SelectedText = "";
            this.txtMatricula.Size = new System.Drawing.Size(171, 25);
            this.txtMatricula.TabIndex = 335;
            this.txtMatricula.TextChanged += new System.EventHandler(this.txtMatricula_TextChanged);
            // 
            // txtCaja
            // 
            this.txtCaja.AutoRoundedCorners = true;
            this.txtCaja.BorderColor = System.Drawing.Color.Gray;
            this.txtCaja.BorderRadius = 11;
            this.txtCaja.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCaja.DefaultText = "";
            this.txtCaja.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCaja.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCaja.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCaja.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCaja.Enabled = false;
            this.txtCaja.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCaja.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCaja.ForeColor = System.Drawing.Color.Black;
            this.txtCaja.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCaja.Location = new System.Drawing.Point(130, 48);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.PlaceholderText = "";
            this.txtCaja.SelectedText = "";
            this.txtCaja.Size = new System.Drawing.Size(138, 25);
            this.txtCaja.TabIndex = 335;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(832, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 59;
            this.button1.Text = "Pagos Registrados";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(699, 454);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "- Descuentos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(699, 480);
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
            this.label12.Location = new System.Drawing.Point(699, 428);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 52;
            this.label12.Text = "+ Recargos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(699, 402);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "Sub Total";
            // 
            // dgvPagosPendientes
            // 
            this.dgvPagosPendientes.AllowUserToAddRows = false;
            this.dgvPagosPendientes.AllowUserToDeleteRows = false;
            this.dgvPagosPendientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPagosPendientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
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
            this.Seleccionar,
            this.Tipo,
            this.FolioDocumento,
            this.Documento,
            this.Concepto,
            this.Fecha,
            this.Importe,
            this.SaldoActual});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPagosPendientes.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPagosPendientes.EnableHeadersVisualStyles = false;
            this.dgvPagosPendientes.Location = new System.Drawing.Point(11, 132);
            this.dgvPagosPendientes.MultiSelect = false;
            this.dgvPagosPendientes.Name = "dgvPagosPendientes";
            this.dgvPagosPendientes.RowHeadersVisible = false;
            this.dgvPagosPendientes.RowHeadersWidth = 45;
            this.dgvPagosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagosPendientes.Size = new System.Drawing.Size(942, 258);
            this.dgvPagosPendientes.TabIndex = 44;
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
            this.FolioDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
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
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Importe.DefaultCellStyle = dataGridViewCellStyle2;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            // 
            // SaldoActual
            // 
            this.SaldoActual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.SaldoActual.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaldoActual.HeaderText = "Saldo Actual";
            this.SaldoActual.Name = "SaldoActual";
            this.SaldoActual.ReadOnly = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(832, 48);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(121, 23);
            this.btnBuscar.TabIndex = 43;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(380, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Clave:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(560, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Proveedor:";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(0, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(962, 2);
            this.label5.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Caja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 32);
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
            this.guna2GradientPanel1.Size = new System.Drawing.Size(973, 75);
            this.guna2GradientPanel1.TabIndex = 106;
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
            this.guna2CircleButton1.Location = new System.Drawing.Point(856, 2);
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
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(61, 21);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(162, 32);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "Registrar Egreso";
            // 
            // btnReportePreeliminar
            // 
            this.btnReportePreeliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.btnReportePreeliminar.BorderRadius = 50;
            this.btnReportePreeliminar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReportePreeliminar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReportePreeliminar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReportePreeliminar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReportePreeliminar.FillColor = System.Drawing.Color.Transparent;
            this.btnReportePreeliminar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReportePreeliminar.ForeColor = System.Drawing.Color.White;
            this.btnReportePreeliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnReportePreeliminar.Image")));
            this.btnReportePreeliminar.ImageSize = new System.Drawing.Size(35, 35);
            this.btnReportePreeliminar.Location = new System.Drawing.Point(11, 488);
            this.btnReportePreeliminar.Name = "btnReportePreeliminar";
            this.btnReportePreeliminar.Size = new System.Drawing.Size(65, 65);
            this.btnReportePreeliminar.TabIndex = 364;
            this.btnReportePreeliminar.Click += new System.EventHandler(this.btnReportePreeliminar_Click);
            // 
            // RegistroEgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(973, 665);
            this.ControlBox = false;
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistroEgreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Registro de Egresos";
            this.Activated += new System.EventHandler(this.RegistroEgreso_Activated);
            this.Load += new System.EventHandler(this.RegistroEgreso_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagosPendientes)).EndInit();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvPagosPendientes;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtAlumno;
        private Guna.UI2.WinForms.Guna2TextBox txtMatricula;
        private Guna.UI2.WinForms.Guna2TextBox txtCaja;
        private Guna.UI2.WinForms.Guna2TextBox txtTotal;
        private Guna.UI2.WinForms.Guna2TextBox txtDescuentos;
        private Guna.UI2.WinForms.Guna2TextBox txtRecargos;
        private Guna.UI2.WinForms.Guna2TextBox txtSubtotal;
        private Guna.UI2.WinForms.Guna2Button button5;
        private Guna.UI2.WinForms.Guna2Button button6;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFecha;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaldoActual;
        private Guna.UI2.WinForms.Guna2Button btnReportePreeliminar;
    }
}