namespace PV
{
    partial class ReporteAnticiposFiltro
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteAnticiposFiltro));
            this.label5 = new System.Windows.Forms.Label();
            this.cbFechas = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClavePaciente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.conceptosIngresoTableAdapter1 = new PV.ControlCondominiosDataSet10TableAdapters.ConceptosIngresoTableAdapter();
            this.button3 = new Guna.UI2.WinForms.Guna2Button();
            this.txtFiltroNombre = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbpropietario1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbpropietario2 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtFecha1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtFecha2 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.cmbCuenta = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbFormaPago = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propietario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(36, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 148;
            this.label5.Text = "Al";
            // 
            // cbFechas
            // 
            this.cbFechas.AutoSize = true;
            this.cbFechas.Location = new System.Drawing.Point(45, 284);
            this.cbFechas.Name = "cbFechas";
            this.cbFechas.Size = new System.Drawing.Size(15, 14);
            this.cbFechas.TabIndex = 145;
            this.cbFechas.UseVisualStyleBackColor = true;
            this.cbFechas.CheckedChanged += new System.EventHandler(this.cbFechas_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(240, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 143;
            this.label4.Text = "Al";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(66, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 141;
            this.label3.Text = "Fecha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(36, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Rango:";
            // 
            // txtClavePaciente
            // 
            this.txtClavePaciente.Location = new System.Drawing.Point(5, 58);
            this.txtClavePaciente.Name = "txtClavePaciente";
            this.txtClavePaciente.ReadOnly = true;
            this.txtClavePaciente.Size = new System.Drawing.Size(32, 20);
            this.txtClavePaciente.TabIndex = 151;
            this.txtClavePaciente.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(36, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 149;
            this.label8.Text = "Cliente:";
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
            this.dataGridView1.Location = new System.Drawing.Point(46, 311);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(334, 293);
            this.dataGridView1.TabIndex = 166;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // conceptosIngresoTableAdapter1
            // 
            this.conceptosIngresoTableAdapter1.ClearBeforeFill = true;
            // 
            // button3
            // 
            this.button3.AutoRoundedCorners = true;
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BorderRadius = 15;
            this.button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.button3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(186, 242);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 33);
            this.button3.TabIndex = 219;
            this.button3.Text = "Limpiar";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFiltroNombre
            // 
            this.txtFiltroNombre.AutoRoundedCorners = true;
            this.txtFiltroNombre.BackColor = System.Drawing.Color.Transparent;
            this.txtFiltroNombre.BorderColor = System.Drawing.Color.Gray;
            this.txtFiltroNombre.BorderRadius = 10;
            this.txtFiltroNombre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFiltroNombre.DefaultText = "";
            this.txtFiltroNombre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFiltroNombre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFiltroNombre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFiltroNombre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFiltroNombre.Enabled = false;
            this.txtFiltroNombre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFiltroNombre.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroNombre.ForeColor = System.Drawing.Color.Black;
            this.txtFiltroNombre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFiltroNombre.Location = new System.Drawing.Point(141, 78);
            this.txtFiltroNombre.Name = "txtFiltroNombre";
            this.txtFiltroNombre.PasswordChar = '\0';
            this.txtFiltroNombre.PlaceholderText = "";
            this.txtFiltroNombre.SelectedText = "";
            this.txtFiltroNombre.Size = new System.Drawing.Size(252, 23);
            this.txtFiltroNombre.TabIndex = 220;
            // 
            // cmbpropietario1
            // 
            this.cmbpropietario1.AutoRoundedCorners = true;
            this.cmbpropietario1.BackColor = System.Drawing.Color.Transparent;
            this.cmbpropietario1.BorderColor = System.Drawing.Color.Gray;
            this.cmbpropietario1.BorderRadius = 12;
            this.cmbpropietario1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbpropietario1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpropietario1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpropietario1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpropietario1.ForeColor = System.Drawing.Color.Black;
            this.cmbpropietario1.IntegralHeight = false;
            this.cmbpropietario1.ItemHeight = 21;
            this.cmbpropietario1.Location = new System.Drawing.Point(141, 176);
            this.cmbpropietario1.Name = "cmbpropietario1";
            this.cmbpropietario1.Size = new System.Drawing.Size(252, 27);
            this.cmbpropietario1.TabIndex = 221;
            this.cmbpropietario1.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario1_SelectedIndexChanged);
            // 
            // cmbpropietario2
            // 
            this.cmbpropietario2.AutoRoundedCorners = true;
            this.cmbpropietario2.BackColor = System.Drawing.Color.Transparent;
            this.cmbpropietario2.BorderColor = System.Drawing.Color.Gray;
            this.cmbpropietario2.BorderRadius = 12;
            this.cmbpropietario2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.cmbpropietario2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpropietario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario2.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpropietario2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbpropietario2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpropietario2.ForeColor = System.Drawing.Color.Black;
            this.cmbpropietario2.IntegralHeight = false;
            this.cmbpropietario2.ItemHeight = 21;
            this.cmbpropietario2.Location = new System.Drawing.Point(141, 209);
            this.cmbpropietario2.Name = "cmbpropietario2";
            this.cmbpropietario2.Size = new System.Drawing.Size(252, 27);
            this.cmbpropietario2.TabIndex = 221;
            this.cmbpropietario2.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario2_SelectedIndexChanged);
            // 
            // dtFecha1
            // 
            this.dtFecha1.AutoRoundedCorners = true;
            this.dtFecha1.BorderRadius = 10;
            this.dtFecha1.Checked = true;
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.dtFecha1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtFecha1.ForeColor = System.Drawing.Color.White;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(118, 279);
            this.dtFecha1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtFecha1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(116, 22);
            this.dtFecha1.TabIndex = 254;
            this.dtFecha1.Value = new System.DateTime(2022, 11, 4, 18, 45, 56, 112);
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged);
            // 
            // dtFecha2
            // 
            this.dtFecha2.AutoRoundedCorners = true;
            this.dtFecha2.BorderRadius = 10;
            this.dtFecha2.Checked = true;
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.dtFecha2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtFecha2.ForeColor = System.Drawing.Color.White;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(264, 279);
            this.dtFecha2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtFecha2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(116, 22);
            this.dtFecha2.TabIndex = 254;
            this.dtFecha2.Value = new System.DateTime(2022, 11, 4, 18, 45, 56, 112);
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged);
            this.dtFecha2.Leave += new System.EventHandler(this.dtFecha2_Leave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.button1.BorderRadius = 20;
            this.button1.BorderThickness = 1;
            this.button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button1.FillColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button1.ImageSize = new System.Drawing.Size(48, 48);
            this.button1.Location = new System.Drawing.Point(226, 612);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 46);
            this.button1.TabIndex = 255;
            this.button1.Text = "Confirmar";
            this.button1.TextOffset = new System.Drawing.Point(23, 0);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel2);
            this.guna2GradientPanel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(429, 55);
            this.guna2GradientPanel1.TabIndex = 257;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(22)))), ((int)(((byte)(59)))));
            this.guna2Panel2.Controls.Add(this.guna2Panel4);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Panel2.Location = new System.Drawing.Point(382, 0);
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
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(211, 27);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "REPORTE ANTICIPOS";
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.guna2GradientPanel1;
            this.guna2DragControl1.UseTransparentDrag = true;
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
            this.cmbCuenta.Location = new System.Drawing.Point(141, 142);
            this.cmbCuenta.Name = "cmbCuenta";
            this.cmbCuenta.Size = new System.Drawing.Size(252, 27);
            this.cmbCuenta.TabIndex = 260;
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
            this.cmbFormaPago.Location = new System.Drawing.Point(141, 108);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(252, 27);
            this.cmbFormaPago.TabIndex = 261;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 258;
            this.label2.Text = "Cuenta Bancaria:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(36, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 259;
            this.label7.Text = "Forma de Pago:";
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
            // ReporteAnticiposFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(429, 671);
            this.ControlBox = false;
            this.Controls.Add(this.cmbCuenta);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtFecha2);
            this.Controls.Add(this.dtFecha1);
            this.Controls.Add(this.cmbpropietario2);
            this.Controls.Add(this.cmbpropietario1);
            this.Controls.Add(this.txtFiltroNombre);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtClavePaciente);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbFechas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReporteAnticiposFiltro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SICC - Reporte Anticipos Filtro";
            this.Load += new System.EventHandler(this.ReporteAnticiposFiltro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbFechas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClavePaciente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ControlCondominiosDataSet10TableAdapters.ConceptosIngresoTableAdapter conceptosIngresoTableAdapter1;
        private Guna.UI2.WinForms.Guna2Button button3;
        private Guna.UI2.WinForms.Guna2TextBox txtFiltroNombre;
        private Guna.UI2.WinForms.Guna2ComboBox cmbpropietario1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbpropietario2;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtFecha1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtFecha2;
        private Guna.UI2.WinForms.Guna2Button button1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCuenta;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFormaPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propietario;
    }
}