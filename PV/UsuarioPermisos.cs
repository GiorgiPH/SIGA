using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Usuarios;

namespace PV
{
    public partial class UsuarioPermisos : Form
    {
        // ============================================================
        // DATOS
        // ============================================================

        private readonly string usuario;
        private readonly string NombreUsuario;
        private readonly DBPermisos dbPermisos;

        private List<OpcionMenu> modulos;
        private OpcionMenu moduloSeleccionado;

        private bool cargandoFormulario;
        private bool cargandoPermisos;

        // ============================================================
        // COLORES - MISMOS DE MENUPRINCIPAL_V2
        // ============================================================

        private readonly Color ColorFondo =
            Color.FromArgb(242, 245, 249);

        private readonly Color ColorBlanco =
            Color.White;

        private readonly Color ColorAzulOscuro =
            Color.FromArgb(21, 48, 87);

        private readonly Color ColorTexto =
            Color.FromArgb(31, 57, 91);

        private readonly Color ColorTextoSecundario =
            Color.FromArgb(95, 114, 139);

        private readonly Color ColorBorde =
            Color.FromArgb(226, 231, 238);

        private readonly Color ColorAzul =
            Color.FromArgb(59, 115, 205);

        private readonly Color ColorAzulModulo =
            Color.FromArgb(58, 111, 157);

        private readonly Color ColorAzulHover =
            Color.FromArgb(68, 126, 177);

        private readonly Color ColorSwitchInactivo =
            Color.FromArgb(145, 162, 181);

        // ============================================================
        // CONTROLES
        // ============================================================

        private Guna2Panel pnlEncabezado;
        private Guna2Panel pnlUsuario;
        private Guna2Panel pnlMenu;
        private Guna2Panel pnlContenido;
        private Guna2Panel pnlModuloHeader;
        private Guna2Panel pnlPie;

        private FlowLayoutPanel flpModulos;
        private FlowLayoutPanel flpPermisos;

        private Label lblTitulo;
        private Label lblSubtitulo;

        private Label lblUsuarioTitulo;
        private Label lblUsuario;
        private Label lblTipoUsuario;

        private Label lblTituloModulo;
        private Label lblDescripcionModulo;
        private Label lblInformacion;

        private Guna2ToggleSwitch swTodos;

        private Guna2Button btnInicio;
        private Guna2Button btnGuardar;

        private Guna2BorderlessForm borderlessForm;

        private readonly Dictionary<int, Guna2ToggleSwitch>
            switchesPermisos =
                new Dictionary<int, Guna2ToggleSwitch>();

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public UsuarioPermisos(
            string usuario,
            string nombreUsuario)
        {
            InitializeComponent();

            this.usuario = usuario;
            this.NombreUsuario = nombreUsuario;
            this.dbPermisos = new DBPermisos();
            this.modulos = new List<OpcionMenu>();

            // Elimina los controles que únicamente se utilizan
            // para mostrar una vista previa en el diseñador.
            LimpiarVistaPreviaDisenador();

            // Construye la interfaz real y dinámica.
            ConstruirInterfaz();

            this.Load += UsuarioPermisos_Load;
        }

        // ============================================================
        // LIMPIAR VISTA PREVIA DEL DISEÑADOR
        // ============================================================

        private void LimpiarVistaPreviaDisenador()
        {
            if (pnlDisenoEncabezado != null)
            {
                Controls.Remove(pnlDisenoEncabezado);
                pnlDisenoEncabezado.Dispose();
                pnlDisenoEncabezado = null;
            }

            if (pnlDisenoUsuario != null)
            {
                Controls.Remove(pnlDisenoUsuario);
                pnlDisenoUsuario.Dispose();
                pnlDisenoUsuario = null;
            }

            if (pnlDisenoMenu != null)
            {
                Controls.Remove(pnlDisenoMenu);
                pnlDisenoMenu.Dispose();
                pnlDisenoMenu = null;
            }

            if (pnlDisenoContenido != null)
            {
                Controls.Remove(pnlDisenoContenido);
                pnlDisenoContenido.Dispose();
                pnlDisenoContenido = null;
            }

            if (pnlDisenoPie != null)
            {
                Controls.Remove(pnlDisenoPie);
                pnlDisenoPie.Dispose();
                pnlDisenoPie = null;
            }
        }

        // ============================================================
        // CONSTRUIR INTERFAZ
        // ============================================================

        private void ConstruirInterfaz()
        {
            SuspendLayout();

            BackColor = ColorFondo;

            ConstruirEncabezado();
            ConstruirUsuario();
            ConstruirMenu();
            ConstruirContenido();
            ConstruirPie();

            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.BorderRadius = 14;
            borderlessForm.TransparentWhileDrag = false;

            Resize += UsuarioPermisos_Resize;

            ResumeLayout(true);

            AjustarInterfaz();
        }

        // ============================================================
        // ENCABEZADO
        // ============================================================

        private void ConstruirEncabezado()
        {
            pnlEncabezado = new Guna2Panel();

            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Height = 105;
            pnlEncabezado.FillColor = ColorAzulOscuro;

            Controls.Add(pnlEncabezado);

            // ========================================================
            // TÍTULO
            // ========================================================

            lblTitulo = new Label();

            lblTitulo.AutoSize = true;
            lblTitulo.Text = "PERMISOS DE USUARIO";

            lblTitulo.Font = new Font(
                "Segoe UI Semibold",
                20F,
                FontStyle.Bold);

            lblTitulo.ForeColor = Color.White;
            lblTitulo.BackColor = Color.Transparent;

            lblTitulo.Location = new Point(35, 23);

            pnlEncabezado.Controls.Add(lblTitulo);

            // ========================================================
            // SUBTÍTULO
            // ========================================================

            lblSubtitulo = new Label();

            lblSubtitulo.AutoSize = true;

            lblSubtitulo.Text =
                "Administra los permisos de acceso a los módulos y opciones del sistema.";

            lblSubtitulo.Font = new Font(
                "Segoe UI",
                10F);

            lblSubtitulo.ForeColor =
                Color.FromArgb(210, 220, 232);

            lblSubtitulo.BackColor =
                Color.Transparent;

            lblSubtitulo.Location =
                new Point(37, 62);

            pnlEncabezado.Controls.Add(lblSubtitulo);

            // ========================================================
            // BOTÓN INICIO
            // ========================================================

            btnInicio = new Guna2Button();

            btnInicio.Size = new Size(65, 65);

            btnInicio.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnInicio.Location =
                new Point(
                    pnlEncabezado.ClientSize.Width - 85,
                    20);

            btnInicio.Image =
                PV.Properties.Resources.home;

            btnInicio.ImageSize =
                new Size(45, 45);

            btnInicio.Text = "";

            btnInicio.FillColor =
                ColorAzulOscuro;

            btnInicio.BackColor =
                ColorAzulOscuro;

            btnInicio.BorderThickness = 0;
            btnInicio.BorderRadius = 0;

            btnInicio.HoverState.FillColor =
                ColorAzulOscuro;

            btnInicio.HoverState.BorderColor =
                ColorAzulOscuro;

            btnInicio.PressedColor =
                ColorAzulOscuro;

            btnInicio.Cursor =
                Cursors.Hand;

            btnInicio.Click += delegate
            {
                Close();
            };

            pnlEncabezado.Controls.Add(btnInicio);
        }

        // ============================================================
        // USUARIO
        // ============================================================

        private void ConstruirUsuario()
        {
            pnlUsuario = new Guna2Panel();

            pnlUsuario.Dock = DockStyle.Top;
            pnlUsuario.Height = 85;
            pnlUsuario.FillColor = ColorBlanco;

            Controls.Add(pnlUsuario);
            pnlUsuario.BringToFront();

            // ========================================================
            // CÍRCULO USUARIO
            // ========================================================

            Guna2CircleButton btnUsuario =
                new Guna2CircleButton();

            btnUsuario.Size =
                new Size(52, 52);

            btnUsuario.Location =
                new Point(28, 16);

            btnUsuario.FillColor =
                Color.FromArgb(224, 235, 251);

            btnUsuario.Text = "●";

            btnUsuario.Font =
                new Font("Segoe UI", 12F);

            btnUsuario.ForeColor =
                ColorAzul;

            btnUsuario.Enabled = false;

            pnlUsuario.Controls.Add(btnUsuario);

            // ========================================================
            // LABEL USUARIO
            // ========================================================

            lblUsuarioTitulo = new Label();

            lblUsuarioTitulo.AutoSize = true;
            lblUsuarioTitulo.Text = "Usuario:";

            lblUsuarioTitulo.Font =
                new Font(
                    "Segoe UI Semibold",
                    10.5F,
                    FontStyle.Bold);

            lblUsuarioTitulo.ForeColor =
                ColorTexto;

            lblUsuarioTitulo.BackColor =
                Color.Transparent;

            lblUsuarioTitulo.Location =
                new Point(95, 32);

            pnlUsuario.Controls.Add(
                lblUsuarioTitulo);

            // ========================================================
            // USUARIO
            // ========================================================

            lblUsuario = new Label();

            lblUsuario.Text =
                usuario;

            lblUsuario.Font =
                new Font(
                    "Segoe UI Semibold",
                    10.5F,
                    FontStyle.Bold);

            lblUsuario.ForeColor =
                ColorTexto;

            lblUsuario.BackColor =
                Color.FromArgb(247, 249, 253);

            lblUsuario.BorderStyle =
                BorderStyle.FixedSingle;

            lblUsuario.TextAlign =
                ContentAlignment.MiddleLeft;

            lblUsuario.Padding =
                new Padding(12, 0, 0, 0);

            lblUsuario.Location =
                new Point(175, 18);

            lblUsuario.Size =
                new Size(220, 48);

            pnlUsuario.Controls.Add(lblUsuario);

            // ========================================================
            // NOMBRE / TIPO DE USUARIO
            // ========================================================

            lblTipoUsuario = new Label();

            lblTipoUsuario.Text =
                NombreUsuario;

            lblTipoUsuario.Font =
                new Font(
                    "Segoe UI",
                    10F);

            lblTipoUsuario.ForeColor =
                ColorTextoSecundario;

            lblTipoUsuario.BackColor =
                Color.FromArgb(247, 249, 253);

            lblTipoUsuario.BorderStyle =
                BorderStyle.FixedSingle;

            lblTipoUsuario.TextAlign =
                ContentAlignment.MiddleLeft;

            lblTipoUsuario.Padding =
                new Padding(12, 0, 0, 0);

            lblTipoUsuario.Location =
                new Point(410, 18);

            lblTipoUsuario.Height = 48;

            lblTipoUsuario.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            pnlUsuario.Controls.Add(
                lblTipoUsuario);

            AjustarFilaUsuario();
        }

        // ============================================================
        // AJUSTAR FILA USUARIO
        // ============================================================

        private void AjustarFilaUsuario()
        {
            if (pnlUsuario == null ||
                lblTipoUsuario == null)
            {
                return;
            }

            const int margenDerecho = 28;

            int ancho =
                pnlUsuario.ClientSize.Width -
                lblTipoUsuario.Left -
                margenDerecho;

            lblTipoUsuario.Width =
                Math.Max(150, ancho);
        }

        // ============================================================
        // MENÚ
        // ============================================================

        private void ConstruirMenu()
        {
            pnlMenu = new Guna2Panel();

            pnlMenu.FillColor =
                ColorAzulOscuro;

            pnlMenu.Location =
                new Point(18, 205);

            pnlMenu.Size =
                new Size(320, 445);

            pnlMenu.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left;

            pnlMenu.BorderRadius = 8;

            Controls.Add(pnlMenu);

            // ========================================================
            // ENCABEZADO MÓDULOS
            // ========================================================

            Label lblModulos =
                new Label();

            lblModulos.Text = "MÓDULOS";

            lblModulos.Dock =
                DockStyle.Top;

            lblModulos.Height = 62;

            lblModulos.Padding =
                new Padding(18, 0, 0, 0);

            lblModulos.Font =
                new Font(
                    "Segoe UI Semibold",
                    13F,
                    FontStyle.Bold);

            lblModulos.ForeColor =
                Color.White;

            lblModulos.BackColor =
                ColorAzulOscuro;

            lblModulos.TextAlign =
                ContentAlignment.MiddleLeft;

            pnlMenu.Controls.Add(
                lblModulos);

            // ========================================================
            // LISTA MÓDULOS
            // ========================================================

            flpModulos =
                new FlowLayoutPanel();

            flpModulos.Dock =
                DockStyle.Fill;

            flpModulos.FlowDirection =
                FlowDirection.TopDown;

            flpModulos.WrapContents =
                false;

            flpModulos.AutoScroll =
                true;

            flpModulos.BackColor =
                ColorAzulOscuro;

            flpModulos.Padding =
                new Padding(0);

            pnlMenu.Controls.Add(
                flpModulos);

            flpModulos.BringToFront();
        }

        // ============================================================
        // CONTENIDO
        // ============================================================

        private void ConstruirContenido()
        {
            pnlContenido =
                new Guna2Panel();

            pnlContenido.FillColor =
                ColorBlanco;

            pnlContenido.BorderColor =
                ColorBorde;

            pnlContenido.BorderThickness = 1;
            pnlContenido.BorderRadius = 8;

            pnlContenido.Location =
                new Point(350, 205);

            pnlContenido.Size =
                new Size(
                    ClientSize.Width - 370,
                    445);

            pnlContenido.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            Controls.Add(pnlContenido);

            // ========================================================
            // ENCABEZADO MÓDULO
            // ========================================================

            pnlModuloHeader =
                new Guna2Panel();

            pnlModuloHeader.Location =
                new Point(0, 0);

            pnlModuloHeader.Size =
                new Size(
                    pnlContenido.ClientSize.Width,
                    90);

            pnlModuloHeader.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            pnlModuloHeader.FillColor =
                Color.FromArgb(247, 249, 253);

            pnlModuloHeader.BorderColor =
                ColorBorde;

            pnlModuloHeader.BorderThickness =
                1;

            pnlContenido.Controls.Add(
                pnlModuloHeader);

            // ========================================================
            // TÍTULO MÓDULO
            // ========================================================

            lblTituloModulo =
                new Label();

            lblTituloModulo.AutoSize =
                true;

            lblTituloModulo.Font =
                new Font(
                    "Segoe UI Semibold",
                    14F,
                    FontStyle.Bold);

            lblTituloModulo.ForeColor =
                ColorAzulOscuro;

            lblTituloModulo.BackColor =
                Color.Transparent;

            lblTituloModulo.Location =
                new Point(28, 18);

            pnlModuloHeader.Controls.Add(
                lblTituloModulo);

            // ========================================================
            // DESCRIPCIÓN
            // ========================================================

            lblDescripcionModulo =
                new Label();

            lblDescripcionModulo.AutoSize =
                true;

            lblDescripcionModulo.Font =
                new Font(
                    "Segoe UI",
                    10F);

            lblDescripcionModulo.ForeColor =
                ColorTextoSecundario;

            lblDescripcionModulo.BackColor =
                Color.Transparent;

            lblDescripcionModulo.Location =
                new Point(30, 52);

            pnlModuloHeader.Controls.Add(
                lblDescripcionModulo);

            // ========================================================
            // TODOS
            // ========================================================

            Label lblTodos =
                new Label();

            lblTodos.Text = "Todos";
            lblTodos.AutoSize = true;

            lblTodos.Font =
                new Font(
                    "Segoe UI Semibold",
                    10F,
                    FontStyle.Bold);

            lblTodos.ForeColor =
                ColorTexto;

            lblTodos.BackColor =
                Color.Transparent;

            lblTodos.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblTodos.Location =
                new Point(
                    pnlModuloHeader.ClientSize.Width - 175,
                    34);

            pnlModuloHeader.Controls.Add(
                lblTodos);

            swTodos =
                CrearSwitch();

            swTodos.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            swTodos.Location =
                new Point(
                    pnlModuloHeader.ClientSize.Width - 85,
                    30);

            swTodos.CheckedChanged +=
                SwTodos_CheckedChanged;

            pnlModuloHeader.Controls.Add(
                swTodos);

            // ========================================================
            // LISTA PERMISOS
            // ========================================================

            flpPermisos =
                new FlowLayoutPanel();

            flpPermisos.Location =
                new Point(0, 90);

            flpPermisos.Size =
                new Size(
                    pnlContenido.ClientSize.Width,
                    pnlContenido.ClientSize.Height - 90);

            flpPermisos.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            flpPermisos.FlowDirection =
                FlowDirection.TopDown;

            flpPermisos.WrapContents =
                false;

            flpPermisos.AutoScroll =
                true;

            flpPermisos.BackColor =
                ColorBlanco;

            flpPermisos.Padding =
                new Padding(10);

            flpPermisos.SizeChanged +=
                delegate
                {
                    AjustarAnchoFilasPermisos();
                };

            pnlContenido.Controls.Add(
                flpPermisos);

            pnlModuloHeader.BringToFront();
            flpPermisos.BringToFront();
        }

        // ============================================================
        // PIE
        // ============================================================

        private void ConstruirPie()
        {
            pnlPie =
                new Guna2Panel();

            pnlPie.Dock =
                DockStyle.Bottom;

            pnlPie.Height = 75;

            pnlPie.FillColor =
                Color.FromArgb(247, 249, 253);

            pnlPie.BorderColor =
                ColorBorde;

            pnlPie.BorderThickness = 1;

            Controls.Add(pnlPie);

            pnlPie.BringToFront();

            // ========================================================
            // INFORMACIÓN
            // ========================================================

            lblInformacion =
                new Label();

            lblInformacion.AutoSize =
                true;

            lblInformacion.Text =
                "Seleccione un módulo para ver y configurar sus permisos.";

            lblInformacion.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            lblInformacion.ForeColor =
                ColorTextoSecundario;

            lblInformacion.Location =
                new Point(25, 29);

            pnlPie.Controls.Add(
                lblInformacion);

            // ========================================================
            // GUARDAR
            // ========================================================

            btnGuardar =
                new Guna2Button();

            btnGuardar.Text =
                "✓   Guardar cambios";

            btnGuardar.Font =
                new Font(
                    "Segoe UI Semibold",
                    10F,
                    FontStyle.Bold);

            btnGuardar.ForeColor =
                Color.White;

            btnGuardar.FillColor =
                ColorAzulModulo;

            btnGuardar.HoverState.FillColor =
                ColorAzulHover;

            btnGuardar.BorderRadius =
                8;

            btnGuardar.Size =
                new Size(205, 45);

            btnGuardar.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnGuardar.Location =
                new Point(
                    pnlPie.ClientSize.Width -
                    btnGuardar.Width -
                    20,
                    15);

            btnGuardar.Cursor =
                Cursors.Hand;

            btnGuardar.Click +=
                BtnGuardar_Click;

            pnlPie.Controls.Add(
                btnGuardar);
        }

        // ============================================================
        // RESIZE
        // ============================================================

        private void UsuarioPermisos_Resize(
            object sender,
            EventArgs e)
        {
            AjustarInterfaz();
        }

        private void AjustarInterfaz()
        {
            AjustarFilaUsuario();
            AjustarAnchoFilasPermisos();
        }

        // ============================================================
        // LOAD
        // ============================================================

        private void UsuarioPermisos_Load(
            object sender,
            EventArgs e)
        {
            CargarPermisos();
        }

        // ============================================================
        // CARGAR PERMISOS
        // ============================================================

        private void CargarPermisos()
        {
            try
            {
                cargandoFormulario = true;

                modulos =
                    dbPermisos
                    .ObtenerArbolPermisosUsuario(
                        usuario);

                if (modulos == null)
                {
                    modulos =
                        new List<OpcionMenu>();
                }

                CargarListaModulos();

                moduloSeleccionado =
                    modulos
                    .OrderBy(x => x.Orden)
                    .FirstOrDefault();

                if (moduloSeleccionado != null)
                {
                    SeleccionarModulo(
                        moduloSeleccionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los permisos.\n\n" +
                    ex.Message,
                    "Permisos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cargandoFormulario = false;
            }
        }

        // ============================================================
        // LISTA DE MÓDULOS
        // ============================================================

        private void CargarListaModulos()
        {
            flpModulos.Controls.Clear();

            foreach (
                OpcionMenu modulo in
                modulos.OrderBy(x => x.Orden))
            {
                Guna2Button boton =
                    new Guna2Button();

                boton.Name =
                    "btnModulo_" +
                    modulo.IdOpcionMenu;

                boton.Text =
                    modulo.Nombre;

                boton.Tag =
                    modulo;

                boton.Size =
                    new Size(
                        pnlMenu.ClientSize.Width - 20,
                        55);

                boton.Margin =
                    new Padding(0);

                boton.BorderRadius =
                    0;

                boton.TextAlign =
                    HorizontalAlignment.Left;

                boton.Font =
                    new Font(
                        "Segoe UI",
                        10.5F);

                boton.ForeColor =
                    Color.White;

                boton.FillColor =
                    ColorAzulOscuro;

                boton.HoverState.FillColor =
                    ColorAzulModulo;

                boton.HoverState.ForeColor =
                    Color.White;

                boton.Padding =
                    new Padding(
                        25,
                        0,
                        0,
                        0);

                boton.Cursor =
                    Cursors.Hand;

                boton.Click +=
                    BtnModulo_Click;

                flpModulos.Controls.Add(
                    boton);
            }
        }

        // ============================================================
        // CLICK MÓDULO
        // ============================================================

        private void BtnModulo_Click(
            object sender,
            EventArgs e)
        {
            Guna2Button boton =
                sender as Guna2Button;

            if (boton == null)
            {
                return;
            }

            OpcionMenu modulo =
                boton.Tag as OpcionMenu;

            if (modulo == null)
            {
                return;
            }

            SeleccionarModulo(modulo);
        }

        // ============================================================
        // SELECCIONAR MÓDULO
        // ============================================================

        private void SeleccionarModulo(
            OpcionMenu modulo)
        {
            if (modulo == null)
            {
                return;
            }

            moduloSeleccionado =
                modulo;

            ActualizarSeleccionMenu();

            lblTituloModulo.Text =
                modulo.Nombre.ToUpper();

            lblDescripcionModulo.Text =
                "Configura los permisos de este módulo.";

            MostrarPermisosModulo(
                modulo);
        }

        // ============================================================
        // SELECCIÓN VISUAL
        // ============================================================

        private void ActualizarSeleccionMenu()
        {
            foreach (
                Control control in
                flpModulos.Controls)
            {
                Guna2Button boton =
                    control as Guna2Button;

                if (boton == null)
                {
                    continue;
                }

                OpcionMenu modulo =
                    boton.Tag as OpcionMenu;

                bool seleccionado =
                    modulo != null &&
                    moduloSeleccionado != null &&
                    modulo.IdOpcionMenu ==
                    moduloSeleccionado.IdOpcionMenu;

                if (seleccionado)
                {
                    boton.FillColor =
                        ColorAzulModulo;

                    boton.ForeColor =
                        Color.White;

                    boton.Font =
                        new Font(
                            "Segoe UI Semibold",
                            10.5F,
                            FontStyle.Bold);
                }
                else
                {
                    boton.FillColor =
                        ColorAzulOscuro;

                    boton.ForeColor =
                        Color.White;

                    boton.Font =
                        new Font(
                            "Segoe UI",
                            10.5F);
                }
            }
        }
        // ============================================================
        // MOSTRAR PERMISOS
        // ============================================================

        private void MostrarPermisosModulo(
            OpcionMenu modulo)
        {
            if (modulo == null)
            {
                return;
            }

            cargandoPermisos = true;

            try
            {
                flpPermisos.SuspendLayout();

                flpPermisos.Controls.Clear();
                switchesPermisos.Clear();

                CrearFilaPermiso(
                    modulo,
                    0);

                if (modulo.Hijos != null)
                {
                    foreach (
                        OpcionMenu hijo in
                        modulo.Hijos
                        .OrderBy(x => x.Orden))
                    {
                        CrearPermisosRecursivamente(
                            hijo,
                            1);
                    }
                }

                ActualizarEstadoVisualPermisos();

                List<OpcionMenu> opciones =
                    ObtenerOpcionesModulo(
                        modulo);

                swTodos.Checked =
                    opciones.Count > 0 &&
                    opciones.All(
                        x => x.Permitido);
            }
            finally
            {
                flpPermisos.ResumeLayout(true);
                flpPermisos.PerformLayout();

                AjustarAnchoFilasPermisos();

                cargandoPermisos = false;
            }
        }

        // ============================================================
        // RECURSIVIDAD
        // ============================================================

        private void CrearPermisosRecursivamente(
            OpcionMenu opcion,
            int nivelVisual)
        {
            if (opcion == null)
            {
                return;
            }

            CrearFilaPermiso(
                opcion,
                nivelVisual);

            if (opcion.Hijos == null)
            {
                return;
            }

            foreach (
                OpcionMenu hijo in
                opcion.Hijos
                .OrderBy(x => x.Orden))
            {
                CrearPermisosRecursivamente(
                    hijo,
                    nivelVisual + 1);
            }
        }

        // ============================================================
        // CREAR FILA
        // ============================================================

        private void CrearFilaPermiso(
            OpcionMenu opcion,
            int nivelVisual)
        {
            if (opcion == null)
            {
                return;
            }

            Guna2Panel fila =
                new Guna2Panel();

            fila.Name =
                "pnlPermiso_" +
                opcion.IdOpcionMenu;

            fila.Tag =
                opcion;

            fila.Size =
                new Size(
                    ObtenerAnchoFilaPermisos(),
                    55);

            fila.Margin =
                new Padding(
                    0,
                    0,
                    0,
                    4);

            fila.FillColor =
                ColorBlanco;

            fila.BorderColor =
                ColorBorde;

            fila.BorderThickness =
                1;

            // ========================================================
            // JERARQUÍA
            // ========================================================

            Label lblJerarquia =
                new Label();

            lblJerarquia.AutoSize =
                false;

            lblJerarquia.Text =
                nivelVisual == 0
                    ? ""
                    : "└";

            lblJerarquia.Font =
                new Font(
                    "Segoe UI Symbol",
                    10F);

            lblJerarquia.ForeColor =
                Color.FromArgb(
                    180,
                    194,
                    211);

            lblJerarquia.BackColor =
                Color.Transparent;

            lblJerarquia.TextAlign =
                ContentAlignment.MiddleCenter;

            lblJerarquia.Location =
                new Point(
                    15 +
                    (nivelVisual * 25),
                    0);

            lblJerarquia.Size =
                new Size(25, 55);

            fila.Controls.Add(
                lblJerarquia);

            // ========================================================
            // NOMBRE
            // ========================================================

            Label lblNombre =
                new Label();

            lblNombre.AutoSize =
                false;

            lblNombre.Text =
                opcion.Nombre;

            lblNombre.Font =
                new Font(
                    nivelVisual == 0
                        ? "Segoe UI Semibold"
                        : "Segoe UI",
                    10F,
                    nivelVisual == 0
                        ? FontStyle.Bold
                        : FontStyle.Regular);

            lblNombre.ForeColor =
                ColorTexto;

            lblNombre.BackColor =
                Color.Transparent;

            lblNombre.TextAlign =
                ContentAlignment.MiddleLeft;

            int posicionNombre =
                45 +
                (nivelVisual * 25);

            lblNombre.Location =
                new Point(
                    posicionNombre,
                    0);

            lblNombre.Size =
                new Size(
                    Math.Max(
                        150,
                        fila.Width -
                        posicionNombre -
                        130),
                    55);

            lblNombre.Anchor =
                AnchorStyles.Left |
                AnchorStyles.Top |
                AnchorStyles.Right;

            fila.Controls.Add(
                lblNombre);

            // ========================================================
            // SWITCH
            // ========================================================

            Guna2ToggleSwitch sw =
                CrearSwitch();

            sw.Name =
                "swPermiso_" +
                opcion.IdOpcionMenu;

            sw.Tag =
                opcion;

            sw.Checked =
                opcion.Permitido;

            sw.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            sw.Location =
                new Point(
                    fila.Width - 75,
                    16);

            sw.CheckedChanged +=
                SwPermiso_CheckedChanged;

            fila.Controls.Add(sw);

            switchesPermisos[
                opcion.IdOpcionMenu] = sw;

            flpPermisos.Controls.Add(
                fila);
        }

        // ============================================================
        // ANCHO FILAS
        // ============================================================

        private int ObtenerAnchoFilaPermisos()
        {
            if (flpPermisos == null)
            {
                return 400;
            }

            int ancho =
                flpPermisos.ClientSize.Width -
                flpPermisos.Padding.Horizontal -
                8;

            if (flpPermisos.VerticalScroll.Visible)
            {
                ancho -=
                    SystemInformation
                    .VerticalScrollBarWidth;
            }

            return Math.Max(
                350,
                ancho);
        }

        private void AjustarAnchoFilasPermisos()
        {
            if (flpPermisos == null)
            {
                return;
            }

            int ancho =
                ObtenerAnchoFilaPermisos();

            foreach (
                Control control in
                flpPermisos.Controls)
            {
                control.Width =
                    ancho;
            }
        }

        // ============================================================
        // CREAR SWITCH
        // ============================================================

        private Guna2ToggleSwitch CrearSwitch()
        {
            Guna2ToggleSwitch sw =
                new Guna2ToggleSwitch();

            sw.Size =
                new Size(48, 24);

            sw.CheckedState.FillColor =
                ColorAzul;

            sw.CheckedState.BorderColor =
                ColorAzul;

            sw.CheckedState.InnerColor =
                Color.White;

            sw.UncheckedState.FillColor =
                ColorSwitchInactivo;

            sw.UncheckedState.BorderColor =
                ColorSwitchInactivo;

            sw.UncheckedState.InnerColor =
                Color.White;

            return sw;
        }

        // ============================================================
        // SWITCH PERMISO
        // ============================================================

        private void SwPermiso_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (cargandoPermisos)
            {
                return;
            }

            Guna2ToggleSwitch sw =
                sender as Guna2ToggleSwitch;

            if (sw == null)
            {
                return;
            }

            OpcionMenu opcion =
                sw.Tag as OpcionMenu;

            if (opcion == null)
            {
                return;
            }

            opcion.Permitido =
                sw.Checked;

            ActualizarEstadoVisualPermisos();
            ActualizarSwitchTodos();
        }

        // ============================================================
        // TODOS
        // ============================================================

        private void SwTodos_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (cargandoPermisos)
            {
                return;
            }

            if (moduloSeleccionado == null)
            {
                return;
            }

            bool permitido =
                swTodos.Checked;

            EstablecerPermisoRecursivamente(
                moduloSeleccionado,
                permitido);

            cargandoPermisos =
                true;

            try
            {
                foreach (
                    KeyValuePair<
                        int,
                        Guna2ToggleSwitch>
                    item in switchesPermisos)
                {
                    OpcionMenu opcion =
                        BuscarOpcionPorId(
                            item.Key);

                    if (opcion != null)
                    {
                        item.Value.Checked =
                            opcion.Permitido;
                    }
                }
            }
            finally
            {
                cargandoPermisos =
                    false;
            }

            ActualizarEstadoVisualPermisos();
        }

        // ============================================================
        // ACTUALIZAR TODOS
        // ============================================================

        private void ActualizarSwitchTodos()
        {
            if (moduloSeleccionado == null)
            {
                return;
            }

            List<OpcionMenu> opciones =
                ObtenerOpcionesModulo(
                    moduloSeleccionado);

            bool todos =
                opciones.Count > 0 &&
                opciones.All(
                    x => x.Permitido);

            cargandoPermisos =
                true;

            try
            {
                swTodos.Checked =
                    todos;
            }
            finally
            {
                cargandoPermisos =
                    false;
            }
        }

        // ============================================================
        // ESTADO VISUAL
        // ============================================================

        private void ActualizarEstadoVisualPermisos()
        {
            foreach (
                KeyValuePair<
                    int,
                    Guna2ToggleSwitch>
                item in switchesPermisos)
            {
                OpcionMenu opcion =
                    BuscarOpcionPorId(
                        item.Key);

                if (opcion == null)
                {
                    continue;
                }

                bool padrePermitido =
                    PadreTienePermisoEfectivo(
                        opcion);

                item.Value.Enabled =
                    padrePermitido;
            }
        }

        // ============================================================
        // PERMISO EFECTIVO PADRE
        // ============================================================

        private bool PadreTienePermisoEfectivo(
            OpcionMenu opcion)
        {
            if (opcion == null)
            {
                return false;
            }

            if (!opcion.IdOpcionPadre.HasValue)
            {
                return true;
            }

            OpcionMenu padre =
                BuscarOpcionPorId(
                    opcion.IdOpcionPadre.Value);

            if (padre == null)
            {
                return false;
            }

            if (!padre.Permitido)
            {
                return false;
            }

            return PadreTienePermisoEfectivo(
                padre);
        }

        // ============================================================
        // OBTENER TODAS LAS OPCIONES
        // ============================================================

        private List<OpcionMenu>
            ObtenerTodasLasOpciones()
        {
            List<OpcionMenu> resultado =
                new List<OpcionMenu>();

            foreach (
                OpcionMenu modulo in
                modulos)
            {
                AgregarOpcionRecursivamente(
                    modulo,
                    resultado);
            }

            return resultado;
        }

        private void AgregarOpcionRecursivamente(
            OpcionMenu opcion,
            List<OpcionMenu> resultado)
        {
            if (opcion == null)
            {
                return;
            }

            resultado.Add(
                opcion);

            if (opcion.Hijos == null)
            {
                return;
            }

            foreach (
                OpcionMenu hijo in
                opcion.Hijos)
            {
                AgregarOpcionRecursivamente(
                    hijo,
                    resultado);
            }
        }

        // ============================================================
        // OPCIONES DEL MÓDULO
        // ============================================================

        private List<OpcionMenu>
            ObtenerOpcionesModulo(
                OpcionMenu modulo)
        {
            List<OpcionMenu> resultado =
                new List<OpcionMenu>();

            if (modulo == null)
            {
                return resultado;
            }

            AgregarOpcionRecursivamente(
                modulo,
                resultado);

            return resultado;
        }

        // ============================================================
        // BUSCAR OPCIÓN
        // ============================================================

        private OpcionMenu BuscarOpcionPorId(
            int idOpcionMenu)
        {
            return ObtenerTodasLasOpciones()
                .FirstOrDefault(
                    x =>
                        x.IdOpcionMenu ==
                        idOpcionMenu);
        }

        // ============================================================
        // ESTABLECER RECURSIVAMENTE
        // ============================================================

        private void EstablecerPermisoRecursivamente(
            OpcionMenu opcion,
            bool permitido)
        {
            if (opcion == null)
            {
                return;
            }

            opcion.Permitido =
                permitido;

            if (opcion.Hijos == null)
            {
                return;
            }

            foreach (
                OpcionMenu hijo in
                opcion.Hijos)
            {
                EstablecerPermisoRecursivamente(
                    hijo,
                    permitido);
            }
        }

        // ============================================================
        // GUARDAR
        // ============================================================

        private void BtnGuardar_Click(
            object sender,
            EventArgs e)
        {
            GuardarPermisos();
        }

        private void GuardarPermisos()
        {
            if (cargandoFormulario)
            {
                return;
            }

            try
            {
                List<OpcionMenu> opciones =
                    ObtenerTodasLasOpciones();

                string resultado =
                    dbPermisos
                    .GuardarPermisosUsuario(
                        usuario,
                        opciones);

                MessageBox.Show(
                    resultado,
                    "Permisos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar los permisos.\n\n" +
                    ex.Message,
                    "Permisos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}