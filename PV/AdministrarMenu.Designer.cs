namespace PV
{
    partial class AdministrarMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.btnInicio = new Guna.UI2.WinForms.Guna2Button();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();

            this.pnlContenido = new System.Windows.Forms.Panel();

            this.pnlIzquierdo = new System.Windows.Forms.Panel();
            this.tvMenu = new System.Windows.Forms.TreeView();
            this.lblEstructura = new System.Windows.Forms.Label();

            this.pnlSeparador = new System.Windows.Forms.Panel();

            this.pnlDerecho = new System.Windows.Forms.Panel();
            this.lblDatosOpcion = new System.Windows.Forms.Label();

            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();

            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblAyudaClave = new System.Windows.Forms.Label();

            this.lblPadre = new System.Windows.Forms.Label();
            this.cmbPadre = new System.Windows.Forms.ComboBox();

            this.lblNivel = new System.Windows.Forms.Label();
            this.txtNivel = new System.Windows.Forms.Label();

            this.lblOrden = new System.Windows.Forms.Label();
            this.nudOrden = new System.Windows.Forms.NumericUpDown();

            this.lblTipo = new System.Windows.Forms.Label();
            this.rbContenedor = new System.Windows.Forms.RadioButton();
            this.rbOpcion = new System.Windows.Forms.RadioButton();
            this.lblAyudaTipo = new System.Windows.Forms.Label();

            this.chkActivo = new System.Windows.Forms.CheckBox();

            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();

            this.pnlEncabezado.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlIzquierdo.SuspendLayout();
            this.pnlDerecho.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudOrden)).BeginInit();

            this.pnlBotones.SuspendLayout();

            this.SuspendLayout();

            // ========================================================
            // pnlEncabezado
            // ========================================================

            this.pnlEncabezado.BackColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.pnlEncabezado.Controls.Add(
                this.btnInicio);

            this.pnlEncabezado.Controls.Add(
                this.lblSubtitulo);

            this.pnlEncabezado.Controls.Add(
                this.lblTitulo);

            this.pnlEncabezado.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlEncabezado.Location =
                new System.Drawing.Point(0, 0);

            this.pnlEncabezado.Name =
                "pnlEncabezado";

            this.pnlEncabezado.Size =
                new System.Drawing.Size(
                    1100,
                    92);

            this.pnlEncabezado.TabIndex =
                0;

            this.pnlEncabezado.MouseDown +=
                new System.Windows.Forms.MouseEventHandler(
                    this.pnlEncabezado_MouseDown);

            // ========================================================
            // lblTitulo
            // ========================================================

            this.lblTitulo.AutoSize =
                true;

            this.lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    16F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitulo.ForeColor =
                System.Drawing.Color.White;

            this.lblTitulo.Location =
                new System.Drawing.Point(
                    28,
                    16);

            this.lblTitulo.Name =
                "lblTitulo";

            this.lblTitulo.Size =
                new System.Drawing.Size(
                    278,
                    30);

            this.lblTitulo.TabIndex =
                0;

            this.lblTitulo.Text =
                "Administración del menú";

            this.lblTitulo.MouseDown +=
                new System.Windows.Forms.MouseEventHandler(
                    this.pnlEncabezado_MouseDown);

            // ========================================================
            // lblSubtitulo
            // ========================================================

            this.lblSubtitulo.AutoSize =
                true;

            this.lblSubtitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.lblSubtitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    205,
                    214,
                    225);

            this.lblSubtitulo.Location =
                new System.Drawing.Point(
                    30,
                    52);

            this.lblSubtitulo.Name =
                "lblSubtitulo";

            this.lblSubtitulo.Size =
                new System.Drawing.Size(
                    378,
                    17);

            this.lblSubtitulo.TabIndex =
                1;

            this.lblSubtitulo.Text =
                "Crea y organiza las opciones disponibles en SIGA.";

            this.lblSubtitulo.MouseDown +=
                new System.Windows.Forms.MouseEventHandler(
                    this.pnlEncabezado_MouseDown);

            // ========================================================
            // btnInicio
            // ========================================================

            this.btnInicio.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnInicio.BackColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnInicio.BorderRadius =
                0;

            this.btnInicio.BorderThickness =
                0;

            this.btnInicio.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnInicio.FillColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnInicio.HoverState.BorderColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnInicio.HoverState.FillColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnInicio.Image =
                global::PV.Properties.Resources.home;

            this.btnInicio.ImageAlign =
                System.Windows.Forms.HorizontalAlignment.Center;

            this.btnInicio.ImageSize =
                new System.Drawing.Size(
                    45,
                    45);

            this.btnInicio.Location =
                new System.Drawing.Point(
                    1030,
                    23);

            this.btnInicio.Name =
                "btnInicio";

            this.btnInicio.PressedColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnInicio.Size =
                new System.Drawing.Size(
                    42,
                    42);

            this.btnInicio.TabIndex =
                2;

            this.btnInicio.TabStop =
                false;

            this.btnInicio.Click +=
                new System.EventHandler(
                    this.btnInicio_Click);

            // ========================================================
            // pnlContenido
            // ========================================================

            this.pnlContenido.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    245,
                    249);

            this.pnlContenido.Controls.Add(
                this.pnlDerecho);

            this.pnlContenido.Controls.Add(
                this.pnlSeparador);

            this.pnlContenido.Controls.Add(
                this.pnlIzquierdo);

            this.pnlContenido.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlContenido.Location =
                new System.Drawing.Point(
                    0,
                    92);

            this.pnlContenido.Name =
                "pnlContenido";

            this.pnlContenido.Padding =
                new System.Windows.Forms.Padding(
                    24);

            this.pnlContenido.Size =
                new System.Drawing.Size(
                    1100,
                    608);

            this.pnlContenido.TabIndex =
                1;

            // ========================================================
            // pnlIzquierdo
            // ========================================================

            this.pnlIzquierdo.BackColor =
                System.Drawing.Color.White;

            this.pnlIzquierdo.Controls.Add(
                this.tvMenu);

            this.pnlIzquierdo.Controls.Add(
                this.lblEstructura);

            this.pnlIzquierdo.Dock =
                System.Windows.Forms.DockStyle.Left;

            this.pnlIzquierdo.Location =
                new System.Drawing.Point(
                    24,
                    24);

            this.pnlIzquierdo.Name =
                "pnlIzquierdo";

            this.pnlIzquierdo.Padding =
                new System.Windows.Forms.Padding(
                    20,
                    18,
                    20,
                    20);

            this.pnlIzquierdo.Size =
                new System.Drawing.Size(
                    430,
                    560);

            this.pnlIzquierdo.TabIndex =
                0;

            // ========================================================
            // lblEstructura
            // ========================================================

            this.lblEstructura.BackColor =
                System.Drawing.Color.FromArgb(
                    238,
                    242,
                    247);

            this.lblEstructura.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.lblEstructura.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblEstructura.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblEstructura.Location =
                new System.Drawing.Point(
                    20,
                    18);

            this.lblEstructura.Name =
                "lblEstructura";

            this.lblEstructura.Padding =
                new System.Windows.Forms.Padding(
                    10,
                    0,
                    0,
                    0);

            this.lblEstructura.Size =
                new System.Drawing.Size(
                    390,
                    38);

            this.lblEstructura.TabIndex =
                0;

            this.lblEstructura.Text =
                "ESTRUCTURA DEL MENÚ";

            this.lblEstructura.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ========================================================
            // tvMenu
            // ========================================================

            this.tvMenu.BackColor =
                System.Drawing.Color.White;

            this.tvMenu.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.tvMenu.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tvMenu.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.tvMenu.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.tvMenu.FullRowSelect =
                true;

            this.tvMenu.HideSelection =
                false;

            this.tvMenu.Indent =
                22;

            this.tvMenu.ItemHeight =
                28;

            this.tvMenu.LineColor =
                System.Drawing.Color.FromArgb(
                    205,
                    214,
                    225);

            this.tvMenu.Location =
                new System.Drawing.Point(
                    20,
                    56);

            this.tvMenu.Name =
                "tvMenu";

            this.tvMenu.ShowLines =
                true;

            this.tvMenu.ShowPlusMinus =
                true;

            this.tvMenu.ShowRootLines =
                true;

            this.tvMenu.Size =
                new System.Drawing.Size(
                    390,
                    484);

            this.tvMenu.TabIndex =
                0;

            // ========================================================
            // pnlSeparador
            // ========================================================

            this.pnlSeparador.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    245,
                    249);

            this.pnlSeparador.Dock =
                System.Windows.Forms.DockStyle.Left;

            this.pnlSeparador.Location =
                new System.Drawing.Point(
                    454,
                    24);

            this.pnlSeparador.Name =
                "pnlSeparador";

            this.pnlSeparador.Size =
                new System.Drawing.Size(
                    18,
                    560);

            this.pnlSeparador.TabIndex =
                1;

            // ========================================================
            // pnlDerecho
            // ========================================================

            this.pnlDerecho.BackColor =
                System.Drawing.Color.White;

            this.pnlDerecho.Controls.Add(
                this.pnlBotones);

            this.pnlDerecho.Controls.Add(
                this.chkActivo);

            this.pnlDerecho.Controls.Add(
                this.lblAyudaTipo);

            this.pnlDerecho.Controls.Add(
                this.rbOpcion);

            this.pnlDerecho.Controls.Add(
                this.rbContenedor);

            this.pnlDerecho.Controls.Add(
                this.lblTipo);

            this.pnlDerecho.Controls.Add(
                this.nudOrden);

            this.pnlDerecho.Controls.Add(
                this.lblOrden);

            this.pnlDerecho.Controls.Add(
                this.txtNivel);

            this.pnlDerecho.Controls.Add(
                this.lblNivel);

            this.pnlDerecho.Controls.Add(
                this.cmbPadre);

            this.pnlDerecho.Controls.Add(
                this.lblPadre);

            this.pnlDerecho.Controls.Add(
                this.lblAyudaClave);

            this.pnlDerecho.Controls.Add(
                this.txtClave);

            this.pnlDerecho.Controls.Add(
                this.lblClave);

            this.pnlDerecho.Controls.Add(
                this.txtNombre);

            this.pnlDerecho.Controls.Add(
                this.lblNombre);

            this.pnlDerecho.Controls.Add(
                this.lblDatosOpcion);

            this.pnlDerecho.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlDerecho.Location =
                new System.Drawing.Point(
                    472,
                    24);

            this.pnlDerecho.Name =
                "pnlDerecho";

            this.pnlDerecho.Padding =
                new System.Windows.Forms.Padding(
                    28,
                    18,
                    28,
                    20);

            this.pnlDerecho.Size =
                new System.Drawing.Size(
                    604,
                    560);

            this.pnlDerecho.TabIndex =
                2;

            // ========================================================
            // lblDatosOpcion
            // ========================================================

            this.lblDatosOpcion.BackColor =
                System.Drawing.Color.FromArgb(
                    238,
                    242,
                    247);

            this.lblDatosOpcion.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblDatosOpcion.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblDatosOpcion.Location =
                new System.Drawing.Point(
                    28,
                    18);

            this.lblDatosOpcion.Name =
                "lblDatosOpcion";

            this.lblDatosOpcion.Padding =
                new System.Windows.Forms.Padding(
                    10,
                    0,
                    0,
                    0);

            this.lblDatosOpcion.Size =
                new System.Drawing.Size(
                    548,
                    38);

            this.lblDatosOpcion.TabIndex =
                0;

            this.lblDatosOpcion.Text =
                "DATOS DE LA OPCIÓN";

            this.lblDatosOpcion.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ========================================================
            // lblNombre
            // ========================================================

            this.lblNombre.AutoSize =
                true;

            this.lblNombre.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblNombre.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblNombre.Location =
                new System.Drawing.Point(
                    29,
                    72);

            this.lblNombre.Name =
                "lblNombre";

            this.lblNombre.Size =
                new System.Drawing.Size(
                    51,
                    15);

            this.lblNombre.TabIndex =
                1;

            this.lblNombre.Text =
                "Nombre";

            // ========================================================
            // txtNombre
            // ========================================================

            this.txtNombre.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.txtNombre.BackColor =
                System.Drawing.Color.White;

            this.txtNombre.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtNombre.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtNombre.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.txtNombre.Location =
                new System.Drawing.Point(
                    32,
                    92);

            this.txtNombre.MaxLength =
                150;

            this.txtNombre.Name =
                "txtNombre";

            this.txtNombre.Size =
                new System.Drawing.Size(
                    540,
                    25);

            this.txtNombre.TabIndex =
                1;

            // ========================================================
            // lblClave
            // ========================================================

            this.lblClave.AutoSize =
                true;

            this.lblClave.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblClave.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblClave.Location =
                new System.Drawing.Point(
                    29,
                    132);

            this.lblClave.Name =
                "lblClave";

            this.lblClave.Size =
                new System.Drawing.Size(
                    36,
                    15);

            this.lblClave.TabIndex =
                3;

            this.lblClave.Text =
                "Clave";

            // ========================================================
            // txtClave
            // ========================================================

            this.txtClave.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.txtClave.BackColor =
                System.Drawing.Color.White;

            this.txtClave.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtClave.CharacterCasing =
                System.Windows.Forms.CharacterCasing.Upper;

            this.txtClave.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtClave.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.txtClave.Location =
                new System.Drawing.Point(
                    32,
                    152);

            this.txtClave.MaxLength =
                100;

            this.txtClave.Name =
                "txtClave";

            this.txtClave.Size =
                new System.Drawing.Size(
                    540,
                    25);

            this.txtClave.TabIndex =
                2;

            // ========================================================
            // lblAyudaClave
            // ========================================================

            this.lblAyudaClave.AutoSize =
                true;

            this.lblAyudaClave.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            this.lblAyudaClave.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblAyudaClave.Location =
                new System.Drawing.Point(
                    30,
                    181);

            this.lblAyudaClave.Name =
                "lblAyudaClave";

            this.lblAyudaClave.Size =
                new System.Drawing.Size(
                    251,
                    13);

            this.lblAyudaClave.TabIndex =
                5;

            this.lblAyudaClave.Text =
                "Ejemplo: DASHBOARD_VENTAS";

            // ========================================================
            // lblPadre
            // ========================================================

            this.lblPadre.AutoSize =
                true;

            this.lblPadre.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblPadre.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblPadre.Location =
                new System.Drawing.Point(
                    29,
                    211);

            this.lblPadre.Name =
                "lblPadre";

            this.lblPadre.Size =
                new System.Drawing.Size(
                    80,
                    15);

            this.lblPadre.TabIndex =
                6;

            this.lblPadre.Text =
                "Opción padre";

            // ========================================================
            // cmbPadre
            // ========================================================

            this.cmbPadre.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.cmbPadre.BackColor =
                System.Drawing.Color.White;

            this.cmbPadre.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbPadre.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.cmbPadre.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.cmbPadre.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.cmbPadre.FormattingEnabled =
                true;

            this.cmbPadre.Location =
                new System.Drawing.Point(
                    32,
                    231);

            this.cmbPadre.Name =
                "cmbPadre";

            this.cmbPadre.Size =
                new System.Drawing.Size(
                    540,
                    25);

            this.cmbPadre.TabIndex =
                3;

            // ========================================================
            // lblNivel
            // ========================================================

            this.lblNivel.AutoSize =
                true;

            this.lblNivel.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblNivel.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblNivel.Location =
                new System.Drawing.Point(
                    29,
                    277);

            this.lblNivel.Name =
                "lblNivel";

            this.lblNivel.Size =
                new System.Drawing.Size(
                    34,
                    15);

            this.lblNivel.TabIndex =
                8;

            this.lblNivel.Text =
                "Nivel";

            // ========================================================
            // txtNivel
            //
            // LABEL INFORMATIVO:
            // no recibe foco, no muestra cursor de edición y
            // no puede modificarse manualmente.
            // ========================================================

            this.txtNivel.BackColor =
                System.Drawing.Color.FromArgb(
                    226,
                    231,
                    238);

            this.txtNivel.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.txtNivel.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.txtNivel.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.txtNivel.Location =
                new System.Drawing.Point(
                    32,
                    297);

            this.txtNivel.Name =
                "txtNivel";

            this.txtNivel.Padding =
                new System.Windows.Forms.Padding(
                    6,
                    0,
                    0,
                    0);

            this.txtNivel.Size =
                new System.Drawing.Size(
                    180,
                    27);

            this.txtNivel.TabIndex =
                9;

            this.txtNivel.Text =
                "1";

            this.txtNivel.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ========================================================
            // lblOrden
            // ========================================================

            this.lblOrden.AutoSize =
                true;

            this.lblOrden.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblOrden.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblOrden.Location =
                new System.Drawing.Point(
                    242,
                    277);

            this.lblOrden.Name =
                "lblOrden";

            this.lblOrden.Size =
                new System.Drawing.Size(
                    41,
                    15);

            this.lblOrden.TabIndex =
                10;

            this.lblOrden.Text =
                "Orden";

            // ========================================================
            // nudOrden
            // ========================================================

            this.nudOrden.BackColor =
                System.Drawing.Color.White;

            this.nudOrden.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.nudOrden.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.nudOrden.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.nudOrden.Location =
                new System.Drawing.Point(
                    245,
                    297);

            this.nudOrden.Maximum =
                new decimal(
                    new int[]
                    {
                        9999,
                        0,
                        0,
                        0
                    });

            this.nudOrden.Minimum =
                new decimal(
                    new int[]
                    {
                        1,
                        0,
                        0,
                        0
                    });

            this.nudOrden.Name =
                "nudOrden";

            this.nudOrden.Size =
                new System.Drawing.Size(
                    180,
                    25);

            this.nudOrden.TabIndex =
                4;

            this.nudOrden.Value =
                new decimal(
                    new int[]
                    {
                        1,
                        0,
                        0,
                        0
                    });

            // ========================================================
            // lblTipo
            // ========================================================

            this.lblTipo.AutoSize =
                true;

            this.lblTipo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.lblTipo.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.lblTipo.Location =
                new System.Drawing.Point(
                    29,
                    347);

            this.lblTipo.Name =
                "lblTipo";

            this.lblTipo.Size =
                new System.Drawing.Size(
                    95,
                    15);

            this.lblTipo.TabIndex =
                12;

            this.lblTipo.Text =
                "Tipo de elemento";

            // ========================================================
            // rbContenedor
            // ========================================================

            this.rbContenedor.AutoSize =
                true;

            this.rbContenedor.Checked =
                true;

            this.rbContenedor.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.rbContenedor.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.rbContenedor.Location =
                new System.Drawing.Point(
                    32,
                    372);

            this.rbContenedor.Name =
                "rbContenedor";

            this.rbContenedor.Size =
                new System.Drawing.Size(
                    91,
                    21);

            this.rbContenedor.TabIndex =
                5;

            this.rbContenedor.TabStop =
                true;

            this.rbContenedor.Text =
                "Contenedor";

            this.rbContenedor.UseVisualStyleBackColor =
                true;

            // ========================================================
            // rbOpcion
            // ========================================================

            this.rbOpcion.AutoSize =
                true;

            this.rbOpcion.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.rbOpcion.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.rbOpcion.Location =
                new System.Drawing.Point(
                    163,
                    372);

            this.rbOpcion.Name =
                "rbOpcion";

            this.rbOpcion.Size =
                new System.Drawing.Size(
                    136,
                    21);

            this.rbOpcion.TabIndex =
                6;

            this.rbOpcion.Text =
                "Opción ejecutable";

            this.rbOpcion.UseVisualStyleBackColor =
                true;

            // ========================================================
            // lblAyudaTipo
            // ========================================================

            this.lblAyudaTipo.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.lblAyudaTipo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            this.lblAyudaTipo.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblAyudaTipo.Location =
                new System.Drawing.Point(
                    30,
                    400);

            this.lblAyudaTipo.Name =
                "lblAyudaTipo";

            this.lblAyudaTipo.Size =
                new System.Drawing.Size(
                    540,
                    36);

            this.lblAyudaTipo.TabIndex =
                15;

            this.lblAyudaTipo.Text =
                "Un contenedor agrupa otras opciones. " +
                "Una opción ejecutable representa una función " +
                "o ventana del sistema.";

            // ========================================================
            // chkActivo
            // ========================================================

            this.chkActivo.AutoSize =
                true;

            this.chkActivo.Checked =
                true;

            this.chkActivo.CheckState =
                System.Windows.Forms.CheckState.Checked;

            this.chkActivo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9.5F);

            this.chkActivo.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.chkActivo.Location =
                new System.Drawing.Point(
                    32,
                    447);

            this.chkActivo.Name =
                "chkActivo";

            this.chkActivo.Size =
                new System.Drawing.Size(
                    64,
                    21);

            this.chkActivo.TabIndex =
                7;

            this.chkActivo.Text =
                "Activo";

            this.chkActivo.UseVisualStyleBackColor =
                true;

            // ========================================================
            // pnlBotones
            // ========================================================

            this.pnlBotones.BackColor =
                System.Drawing.Color.White;

            this.pnlBotones.Controls.Add(
                this.btnEliminar);

            this.pnlBotones.Controls.Add(
                this.btnGuardar);

            this.pnlBotones.Controls.Add(
                this.btnNuevo);

            this.pnlBotones.Dock =
                System.Windows.Forms.DockStyle.Bottom;

            this.pnlBotones.Location =
                new System.Drawing.Point(
                    28,
                    487);

            this.pnlBotones.Name =
                "pnlBotones";

            this.pnlBotones.Size =
                new System.Drawing.Size(
                    548,
                    53);

            this.pnlBotones.TabIndex =
                17;

            // ========================================================
            // btnEliminar
            // ========================================================

            this.btnEliminar.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnEliminar.BackColor =
                System.Drawing.Color.White;

            this.btnEliminar.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnEliminar.Enabled =
                false;

            this.btnEliminar.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(
                    192,
                    57,
                    43);

            this.btnEliminar.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(
                    245,
                    221,
                    218);

            this.btnEliminar.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(
                    252,
                    239,
                    237);

            this.btnEliminar.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnEliminar.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.btnEliminar.ForeColor =
                System.Drawing.Color.FromArgb(
                    192,
                    57,
                    43);

            this.btnEliminar.Location =
                new System.Drawing.Point(
                    201,
                    8);

            this.btnEliminar.Name =
                "btnEliminar";

            this.btnEliminar.Size =
                new System.Drawing.Size(
                    105,
                    36);

            this.btnEliminar.TabIndex =
                8;

            this.btnEliminar.Text =
                "Eliminar";

            this.btnEliminar.UseVisualStyleBackColor =
                false;

            this.btnEliminar.Click +=
                new System.EventHandler(
                    this.btnEliminar_Click);

            // ========================================================
            // btnNuevo
            // ========================================================

            this.btnNuevo.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnNuevo.BackColor =
                System.Drawing.Color.White;

            this.btnNuevo.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnNuevo.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(
                    205,
                    214,
                    225);

            this.btnNuevo.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(
                    226,
                    231,
                    238);

            this.btnNuevo.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    245,
                    249);

            this.btnNuevo.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnNuevo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F);

            this.btnNuevo.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.btnNuevo.Location =
                new System.Drawing.Point(
                    316,
                    8);

            this.btnNuevo.Name =
                "btnNuevo";

            this.btnNuevo.Size =
                new System.Drawing.Size(
                    105,
                    36);

            this.btnNuevo.TabIndex =
                9;

            this.btnNuevo.Text =
                "Nuevo";

            this.btnNuevo.UseVisualStyleBackColor =
                false;

            this.btnNuevo.Click +=
                new System.EventHandler(
                    this.btnNuevo_Click);

            // ========================================================
            // btnGuardar
            // ========================================================

            this.btnGuardar.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnGuardar.BackColor =
                System.Drawing.Color.FromArgb(
                    47,
                    105,
                    163);

            this.btnGuardar.Cursor =
                System.Windows.Forms.Cursors.Hand;

            this.btnGuardar.FlatAppearance.BorderSize =
                0;

            this.btnGuardar.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnGuardar.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(
                    57,
                    119,
                    180);

            this.btnGuardar.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnGuardar.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.btnGuardar.ForeColor =
                System.Drawing.Color.White;

            this.btnGuardar.Location =
                new System.Drawing.Point(
                    431,
                    8);

            this.btnGuardar.Name =
                "btnGuardar";

            this.btnGuardar.Size =
                new System.Drawing.Size(
                    110,
                    36);

            this.btnGuardar.TabIndex =
                10;

            this.btnGuardar.Text =
                "Guardar";

            this.btnGuardar.UseVisualStyleBackColor =
                false;

            this.btnGuardar.Click +=
                new System.EventHandler(
                    this.btnGuardar_Click);

            // ========================================================
            // AdministrarMenu
            // ========================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(
                    7F,
                    15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    245,
                    249);

            this.ClientSize =
                new System.Drawing.Size(
                    1100,
                    700);

            this.Controls.Add(
                this.pnlContenido);

            this.Controls.Add(
                this.pnlEncabezado);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.None;

            this.MinimumSize =
                new System.Drawing.Size(
                    950,
                    650);

            this.Name =
                "AdministrarMenu";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Administración del menú";

            this.pnlEncabezado.ResumeLayout(
                false);

            this.pnlEncabezado.PerformLayout();

            this.pnlContenido.ResumeLayout(
                false);

            this.pnlIzquierdo.ResumeLayout(
                false);

            this.pnlDerecho.ResumeLayout(
                false);

            this.pnlDerecho.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudOrden)).EndInit();

            this.pnlBotones.ResumeLayout(
                false);

            this.ResumeLayout(
                false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private Guna.UI2.WinForms.Guna2Button btnInicio;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;

        private System.Windows.Forms.Panel pnlContenido;

        private System.Windows.Forms.Panel pnlIzquierdo;
        private System.Windows.Forms.Label lblEstructura;
        private System.Windows.Forms.TreeView tvMenu;

        private System.Windows.Forms.Panel pnlSeparador;

        private System.Windows.Forms.Panel pnlDerecho;
        private System.Windows.Forms.Label lblDatosOpcion;

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;

        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblAyudaClave;

        private System.Windows.Forms.Label lblPadre;
        private System.Windows.Forms.ComboBox cmbPadre;

        private System.Windows.Forms.Label lblNivel;

        // Es Label intencionalmente.
        private System.Windows.Forms.Label txtNivel;

        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.NumericUpDown nudOrden;

        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.RadioButton rbContenedor;
        private System.Windows.Forms.RadioButton rbOpcion;
        private System.Windows.Forms.Label lblAyudaTipo;

        private System.Windows.Forms.CheckBox chkActivo;

        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
    }
}