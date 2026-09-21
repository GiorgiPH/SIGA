using Condominios;
using PuntoVentas;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.Usuarios;
using PV.Clases.Usuarios;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PV
{
    public partial class MenuPrincipal_v2 : Form
    {
        public static int Opcion = 0;

        private FavoritosMenu favoritosMenu;
        private SidebarMenuItem opcionSeleccionadaActual;

        private SidebarMenuItem moduloInventarios;
        private SidebarMenuItem moduloCompras;
        private SidebarMenuItem moduloVentas;
        private SidebarMenuItem moduloTesoreria;

        private Guna2Panel pnlDashboard;

        // ============================================================
        // MENÚ DINÁMICO / PERMISOS
        // ============================================================

        private DBPermisos dbPermisos;

        private List<OpcionMenu> arbolMenuUsuario =
            new List<OpcionMenu>();

        private readonly Dictionary<string, SidebarMenuItem>
            controlesMenuPorClave =
                new Dictionary<string, SidebarMenuItem>(
                    StringComparer.OrdinalIgnoreCase);

        // ============================================================
        // COLORES
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

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public MenuPrincipal_v2()
        {
            InitializeComponent();

            pnlMenu.Width = 270;

            ConfigurarMenu();

            // El menú se genera desde:
            //
            // OpcionMenu
            // UsuarioPermiso
            //
            // utilizando el usuario que inició sesión.
            CrearMenu();

            lblTipoUsuario.Text =
                DBLogin.TipoUsuario;

            lblNombreUsuario.Text =
                DBLogin.usuario;

            CargarFotoUsuario();

            CrearDashboardInicio();
        }

        // ============================================================
        // MOVIMIENTO DE VENTANA
        // ============================================================

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            IntPtr wParam,
            IntPtr lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        // ============================================================
        // CONFIGURACIÓN MENÚ
        // ============================================================

        private void ConfigurarMenu()
        {
            flpMenu.FlowDirection =
                FlowDirection.TopDown;

            flpMenu.WrapContents =
                false;

            flpMenu.AutoScroll =
                true;

            flpMenu.Padding =
                new Padding(8, 6, 8, 6);

            flpMenu.HorizontalScroll.Enabled =
                false;

            flpMenu.HorizontalScroll.Visible =
                false;

            flpMenu.SizeChanged -=
                flpMenu_SizeChanged;

            flpMenu.SizeChanged +=
                flpMenu_SizeChanged;
        }

        // ============================================================
        // MEJORAR ICONO
        // ============================================================

        private Image MejorarIcono(
            Image imagen)
        {
            if (imagen == null)
                return null;

            Bitmap resultado =
                new Bitmap(
                    imagen.Width,
                    imagen.Height);

            using (Graphics g =
                Graphics.FromImage(resultado))
            {
                ColorMatrix matriz =
                    new ColorMatrix(
                        new float[][]
                        {
                            new float[]
                            {
                                1.35F, 0, 0, 0, 0
                            },

                            new float[]
                            {
                                0, 1.35F, 0, 0, 0
                            },

                            new float[]
                            {
                                0, 0, 1.35F, 0, 0
                            },

                            new float[]
                            {
                                0, 0, 0, 1F, 0
                            },

                            new float[]
                            {
                                -0.08F,
                                -0.08F,
                                -0.08F,
                                0,
                                1F
                            }
                        });

                using (ImageAttributes atributos =
                    new ImageAttributes())
                {
                    atributos.SetColorMatrix(
                        matriz);

                    g.DrawImage(
                        imagen,
                        new Rectangle(
                            0,
                            0,
                            resultado.Width,
                            resultado.Height),
                        0,
                        0,
                        imagen.Width,
                        imagen.Height,
                        GraphicsUnit.Pixel,
                        atributos);
                }
            }

            return resultado;
        }

        // ============================================================
        // FOTO USUARIO
        // ============================================================

        private void CargarFotoUsuario()
        {
            pbFoto.SizeMode =
                PictureBoxSizeMode.Zoom;

            pbFoto.BorderRadius =
                pbFoto.Width / 2;

            DBUsuarios dbUsuarios =
                new DBUsuarios();

            dbUsuarios.CargarFotoUsuario(
                DBLogin.usuario,
                pbFoto);
        }

        // ============================================================
        // DASHBOARD INICIO
        // ============================================================

        private void CrearDashboardInicio()
        {
            pnlContenido.SuspendLayout();

            pnlContenido.Controls.Clear();

            pnlContenido.BackColor =
                ColorFondo;

            pnlContenido.Padding =
                new Padding(
                    28,
                    22,
                    28,
                    24);

            pnlDashboard =
                new Guna2Panel();

            pnlDashboard.Dock =
                DockStyle.Fill;

            pnlDashboard.FillColor =
                ColorBlanco;

            pnlDashboard.BorderRadius =
                18;

            pnlDashboard.BorderThickness =
                1;

            pnlDashboard.BorderColor =
                ColorBorde;

            pnlDashboard.Padding =
                new Padding(26);

            pnlDashboard.ShadowDecoration.Enabled =
                true;

            pnlDashboard.ShadowDecoration.Depth =
                3;

            pnlDashboard.ShadowDecoration.Color =
                Color.FromArgb(
                    60,
                    80,
                    100);

            pnlContenido.Controls.Add(
                pnlDashboard);

            CrearAyudaDashboard();
            CrearModulosPrincipalesDashboard();
            CrearBienvenidaDashboard();
            CrearEncabezadoDashboard();
            CrearLogoEmpresa();

            pnlContenido.ResumeLayout(true);
        }

        // ============================================================
        // ENCABEZADO DASHBOARD
        // ============================================================

        private void CrearEncabezadoDashboard()
        {
            Panel encabezado =
                new Panel();

            encabezado.Dock =
                DockStyle.Top;

            encabezado.Height =
                88;

            encabezado.BackColor =
                ColorBlanco;

            PictureBox pbLogoSiga =
                new PictureBox();

            pbLogoSiga.Image =
                PV.Properties.Resources.siga_logo;

            pbLogoSiga.Size =
                new Size(150, 65);

            pbLogoSiga.Location =
                new Point(4, 4);

            pbLogoSiga.SizeMode =
                PictureBoxSizeMode.Zoom;

            pbLogoSiga.BackColor =
                Color.Transparent;

            Panel separador =
                new Panel();

            separador.Location =
                new Point(180, 10);

            separador.Size =
                new Size(1, 55);

            separador.BackColor =
                Color.FromArgb(
                    220,
                    226,
                    234);

            Label lblSistema =
                new Label();

            lblSistema.Text =
                "Sistema Integral de Gestión Administrativa";

            lblSistema.AutoSize =
                true;

            lblSistema.Location =
                new Point(210, 12);

            lblSistema.ForeColor =
                ColorAzulOscuro;

            lblSistema.Font =
                new Font(
                    "Segoe UI Semibold",
                    12F,
                    FontStyle.Bold);

            Label lblSubtitulo =
                new Label();

            lblSubtitulo.Text =
                "Bienvenido al sistema";

            lblSubtitulo.AutoSize =
                true;

            lblSubtitulo.Location =
                new Point(210, 40);

            lblSubtitulo.ForeColor =
                ColorTextoSecundario;

            lblSubtitulo.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            encabezado.Controls.Add(
                pbLogoSiga);

            encabezado.Controls.Add(
                separador);

            encabezado.Controls.Add(
                lblSistema);

            encabezado.Controls.Add(
                lblSubtitulo);

            pnlDashboard.Controls.Add(
                encabezado);
        }

        // ============================================================
        // BIENVENIDA
        // ============================================================

        private void CrearBienvenidaDashboard()
        {
            Guna2Panel bienvenida =
                new Guna2Panel();

            bienvenida.Dock =
                DockStyle.Top;

            bienvenida.Height =
                245;

            bienvenida.FillColor =
                ColorBlanco;

            bienvenida.BorderRadius =
                14;

            bienvenida.BorderThickness =
                1;

            bienvenida.BorderColor =
                ColorBorde;

            Guna2Panel fondoIcono =
                new Guna2Panel();

            fondoIcono.Size =
                new Size(105, 105);

            fondoIcono.Location =
                new Point(35, 65);

            fondoIcono.BorderRadius =
                52;

            fondoIcono.FillColor =
                Color.FromArgb(
                    224,
                    235,
                    251);

            PictureBox icono =
                new PictureBox();

            icono.Size =
                new Size(54, 54);

            icono.Location =
                new Point(25, 25);

            icono.SizeMode =
                PictureBoxSizeMode.Zoom;

            icono.BackColor =
                Color.Transparent;

            icono.Image =
                MejorarIcono(
                    PV.Properties.Resources.folder);

            fondoIcono.Controls.Add(
                icono);

            Label lblBienvenido =
                new Label();

            lblBienvenido.Text =
                "Bienvenido, " +
                DBLogin.usuario;

            lblBienvenido.AutoSize =
                true;

            lblBienvenido.Location =
                new Point(175, 77);

            lblBienvenido.ForeColor =
                ColorAzulOscuro;

            lblBienvenido.Font =
                new Font(
                    "Segoe UI Semibold",
                    16F,
                    FontStyle.Bold);

            Label lblMensaje =
                new Label();

            lblMensaje.Text =
                "Selecciona una opción del menú\n" +
                "para comenzar.";

            lblMensaje.AutoSize =
                true;

            lblMensaje.Location =
                new Point(177, 118);

            lblMensaje.ForeColor =
                ColorTextoSecundario;

            lblMensaje.Font =
                new Font(
                    "Segoe UI",
                    10F);

            Guna2Panel ilustracion =
                new Guna2Panel();

            ilustracion.Size =
                new Size(340, 170);

            ilustracion.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            ilustracion.BorderRadius =
                24;

            ilustracion.FillColor =
                Color.FromArgb(
                    247,
                    249,
                    253);

            Guna2Panel monitor =
                new Guna2Panel();

            monitor.Size =
                new Size(190, 108);

            monitor.Location =
                new Point(75, 22);

            monitor.BorderRadius =
                8;

            monitor.FillColor =
                Color.FromArgb(
                    43,
                    68,
                    111);

            Panel pantalla =
                new Panel();

            pantalla.Location =
                new Point(10, 9);

            pantalla.Size =
                new Size(170, 85);

            pantalla.BackColor =
                Color.White;

            Panel grafica1 =
                new Panel();

            grafica1.Location =
                new Point(25, 49);

            grafica1.Size =
                new Size(12, 24);

            grafica1.BackColor =
                Color.FromArgb(
                    175,
                    195,
                    235);

            Panel grafica2 =
                new Panel();

            grafica2.Location =
                new Point(44, 37);

            grafica2.Size =
                new Size(12, 36);

            grafica2.BackColor =
                Color.FromArgb(
                    135,
                    165,
                    220);

            Panel grafica3 =
                new Panel();

            grafica3.Location =
                new Point(63, 26);

            grafica3.Size =
                new Size(12, 47);

            grafica3.BackColor =
                Color.FromArgb(
                    95,
                    135,
                    200);

            Label circuloGrafica =
                new Label();

            circuloGrafica.Text =
                "◕";

            circuloGrafica.AutoSize =
                true;

            circuloGrafica.Location =
                new Point(112, 25);

            circuloGrafica.ForeColor =
                Color.FromArgb(
                    110,
                    140,
                    205);

            circuloGrafica.Font =
                new Font(
                    "Segoe UI Symbol",
                    27F);

            pantalla.Controls.Add(
                grafica1);

            pantalla.Controls.Add(
                grafica2);

            pantalla.Controls.Add(
                grafica3);

            pantalla.Controls.Add(
                circuloGrafica);

            monitor.Controls.Add(
                pantalla);

            Panel pieMonitor =
                new Panel();

            pieMonitor.Size =
                new Size(10, 16);

            pieMonitor.Location =
                new Point(165, 128);

            pieMonitor.BackColor =
                Color.FromArgb(
                    130,
                    150,
                    195);

            Panel baseMonitor =
                new Panel();

            baseMonitor.Size =
                new Size(70, 8);

            baseMonitor.Location =
                new Point(135, 143);

            baseMonitor.BackColor =
                Color.FromArgb(
                    130,
                    150,
                    195);

            ilustracion.Controls.Add(
                monitor);

            ilustracion.Controls.Add(
                pieMonitor);

            ilustracion.Controls.Add(
                baseMonitor);

            bienvenida.Controls.Add(
                fondoIcono);

            bienvenida.Controls.Add(
                lblBienvenido);

            bienvenida.Controls.Add(
                lblMensaje);

            bienvenida.Controls.Add(
                ilustracion);

            bienvenida.Resize +=
                delegate
                {
                    ilustracion.Left =
                        bienvenida.ClientSize.Width -
                        ilustracion.Width -
                        28;

                    ilustracion.Top =
                        36;

                    ilustracion.Visible =
                        bienvenida.ClientSize.Width >=
                        900;
                };

            pnlDashboard.Controls.Add(
                bienvenida);
        }

        // ============================================================
        // MÓDULOS PRINCIPALES DASHBOARD
        // ============================================================

        private void CrearModulosPrincipalesDashboard()
        {
            Panel seccion =
                new Panel();

            seccion.Dock =
                DockStyle.Top;

            seccion.Height =
                215;

            seccion.BackColor =
                ColorBlanco;

            Label titulo =
                new Label();

            titulo.Text =
                "Módulos principales";

            titulo.AutoSize =
                true;

            titulo.Location =
                new Point(3, 18);

            titulo.ForeColor =
                ColorAzulOscuro;

            titulo.Font =
                new Font(
                    "Segoe UI Semibold",
                    10F,
                    FontStyle.Bold);

            Panel linea =
                new Panel();

            linea.Height =
                1;

            linea.Location =
                new Point(145, 29);

            linea.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            linea.BackColor =
                ColorBorde;

            Guna2Panel inventarios =
                CrearTarjetaModulo(
                    "Inventarios",
                    "Consulta y control\nde existencias",
                    PV.Properties.Resources.lista_de_verificacion,
                    Color.FromArgb(220, 234, 255),
                    delegate
                    {
                        AbrirModuloDesdeDashboard(
                            moduloInventarios);
                    });

            Guna2Panel compras =
                CrearTarjetaModulo(
                    "Compras",
                    "Órdenes, recepciones\ny proveedores",
                    PV.Properties.Resources.carrito_de_compras,
                    Color.FromArgb(220, 242, 224),
                    delegate
                    {
                        AbrirModuloDesdeDashboard(
                            moduloCompras);
                    });

            Guna2Panel ventas =
                CrearTarjetaModulo(
                    "Ventas",
                    "Pedidos, remisiones\ny facturación",
                    PV.Properties.Resources.grafico_de_barras,
                    Color.FromArgb(255, 239, 185),
                    delegate
                    {
                        AbrirModuloDesdeDashboard(
                            moduloVentas);
                    });

            Guna2Panel tesoreria =
                CrearTarjetaModulo(
                    "Tesorería",
                    "Cobros, pagos y\nmovimientos",
                    PV.Properties.Resources.banco,
                    Color.FromArgb(232, 217, 250),
                    delegate
                    {
                        AbrirModuloDesdeDashboard(
                            moduloTesoreria);
                    });

            seccion.Controls.Add(titulo);
            seccion.Controls.Add(linea);
            seccion.Controls.Add(inventarios);
            seccion.Controls.Add(compras);
            seccion.Controls.Add(ventas);
            seccion.Controls.Add(tesoreria);

            EventHandler ajustar =
                delegate
                {
                    int margen = 3;
                    int separacion = 16;

                    int disponible =
                        seccion.ClientSize.Width -
                        margen * 2 -
                        separacion * 3;

                    int ancho =
                        disponible / 4;

                    inventarios.Size =
                        new Size(ancho, 124);

                    compras.Size =
                        new Size(ancho, 124);

                    ventas.Size =
                        new Size(ancho, 124);

                    tesoreria.Size =
                        new Size(ancho, 124);

                    inventarios.Location =
                        new Point(
                            margen,
                            62);

                    compras.Location =
                        new Point(
                            inventarios.Right +
                            separacion,
                            62);

                    ventas.Location =
                        new Point(
                            compras.Right +
                            separacion,
                            62);

                    tesoreria.Location =
                        new Point(
                            ventas.Right +
                            separacion,
                            62);

                    linea.Width =
                        Math.Max(
                            0,
                            seccion.ClientSize.Width -
                            linea.Left -
                            3);
                };

            seccion.Resize +=
                ajustar;

            pnlDashboard.Controls.Add(
                seccion);

            ajustar(
                null,
                EventArgs.Empty);
        }

        // ============================================================
        // TARJETA MÓDULO
        // ============================================================

        private Guna2Panel CrearTarjetaModulo(
            string titulo,
            string descripcion,
            Image imagen,
            Color fondo,
            EventHandler accion)
        {
            Guna2Panel tarjeta =
                new Guna2Panel();

            tarjeta.BorderRadius =
                12;

            tarjeta.FillColor =
                Color.White;

            tarjeta.BorderColor =
                ColorBorde;

            tarjeta.BorderThickness =
                1;

            tarjeta.Cursor =
                Cursors.Hand;

            Guna2Panel fondoIcono =
                new Guna2Panel();

            fondoIcono.Size =
                new Size(70, 70);

            fondoIcono.Location =
                new Point(18, 26);

            fondoIcono.BorderRadius =
                35;

            fondoIcono.FillColor =
                fondo;

            fondoIcono.Cursor =
                Cursors.Hand;

            PictureBox icono =
                new PictureBox();

            icono.Image =
                MejorarIcono(imagen);

            icono.Size =
                new Size(40, 40);

            icono.Location =
                new Point(15, 15);

            icono.SizeMode =
                PictureBoxSizeMode.Zoom;

            icono.BackColor =
                Color.Transparent;

            icono.Cursor =
                Cursors.Hand;

            fondoIcono.Controls.Add(
                icono);

            Label lblTitulo =
                new Label();

            lblTitulo.Text =
                titulo;

            lblTitulo.AutoSize =
                true;

            lblTitulo.Location =
                new Point(103, 29);

            lblTitulo.ForeColor =
                ColorTexto;

            lblTitulo.Font =
                new Font(
                    "Segoe UI Semibold",
                    10.5F,
                    FontStyle.Bold);

            lblTitulo.BackColor =
                Color.Transparent;

            lblTitulo.Cursor =
                Cursors.Hand;

            Label lblDescripcion =
                new Label();

            lblDescripcion.Text =
                descripcion;

            lblDescripcion.AutoSize =
                true;

            lblDescripcion.Location =
                new Point(104, 58);

            lblDescripcion.ForeColor =
                ColorTextoSecundario;

            lblDescripcion.Font =
                new Font(
                    "Segoe UI",
                    9F);

            lblDescripcion.BackColor =
                Color.Transparent;

            lblDescripcion.Cursor =
                Cursors.Hand;

            tarjeta.Controls.Add(
                fondoIcono);

            tarjeta.Controls.Add(
                lblTitulo);

            tarjeta.Controls.Add(
                lblDescripcion);

            tarjeta.Click += accion;
            fondoIcono.Click += accion;
            icono.Click += accion;
            lblTitulo.Click += accion;
            lblDescripcion.Click += accion;

            tarjeta.MouseEnter +=
                delegate
                {
                    tarjeta.FillColor =
                        Color.FromArgb(
                            248,
                            250,
                            253);

                    tarjeta.BorderColor =
                        Color.FromArgb(
                            202,
                            214,
                            228);
                };

            tarjeta.MouseLeave +=
                delegate
                {
                    tarjeta.FillColor =
                        Color.White;

                    tarjeta.BorderColor =
                        ColorBorde;
                };

            return tarjeta;
        }

        // ============================================================
        // ABRIR MÓDULO DESDE DASHBOARD
        // ============================================================

        private void AbrirModuloDesdeDashboard(
            SidebarMenuItem modulo)
        {
            // Si el usuario no tiene permiso para el módulo,
            // la referencia será null.
            if (modulo == null)
                return;

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem item =
                    control as SidebarMenuItem;

                if (item != null &&
                    !object.ReferenceEquals(
                        item,
                        modulo))
                {
                    item.Expandir(false);
                }
            }

            modulo.Expandir(true);

            flpMenu.PerformLayout();

            flpMenu.ScrollControlIntoView(
                modulo);

            modulo.Focus();
        }

        // ============================================================
        // AYUDA DASHBOARD
        // ============================================================

        private void CrearAyudaDashboard()
        {
            Guna2Panel ayuda =
                new Guna2Panel();

            ayuda.Dock =
                DockStyle.Top;

            ayuda.Height =
                165;

            ayuda.FillColor =
                Color.White;

            ayuda.BorderRadius =
                14;

            ayuda.BorderColor =
                ColorBorde;

            ayuda.BorderThickness =
                1;

            Label titulo =
                new Label();

            titulo.Text =
                "Documentación y ayuda";

            titulo.AutoSize =
                true;

            titulo.Location =
                new Point(25, 18);

            titulo.ForeColor =
                ColorAzulOscuro;

            titulo.Font =
                new Font(
                    "Segoe UI Semibold",
                    10F,
                    FontStyle.Bold);

            Guna2Button btnInstructivo =
                CrearBotonAyuda(
                    "Instructivo",
                    "Manual de usuario",
                    "▣");

            btnInstructivo.Location =
                new Point(25, 58);

            Guna2Button btnLegal =
                CrearBotonAyuda(
                    "Información legal",
                    "Avisos y políticas",
                    "▤");

            btnLegal.Location =
                new Point(288, 58);

            Label lblSoporte =
                new Label();

            lblSoporte.Text =
                "¿Necesitas ayuda?\n" +
                "Contacta a soporte técnico";

            lblSoporte.AutoSize =
                true;

            lblSoporte.TextAlign =
                ContentAlignment.MiddleRight;

            lblSoporte.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblSoporte.ForeColor =
                ColorTextoSecundario;

            lblSoporte.Font =
                new Font(
                    "Segoe UI",
                    9F);

            Guna2Panel circuloSoporte =
                new Guna2Panel();

            circuloSoporte.Size =
                new Size(62, 62);

            circuloSoporte.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            circuloSoporte.BorderRadius =
                31;

            circuloSoporte.FillColor =
                Color.FromArgb(
                    245,
                    247,
                    251);

            Label iconoSoporte =
                new Label();

            iconoSoporte.Text =
                "☏";

            iconoSoporte.Dock =
                DockStyle.Fill;

            iconoSoporte.TextAlign =
                ContentAlignment.MiddleCenter;

            iconoSoporte.ForeColor =
                Color.FromArgb(
                    70,
                    88,
                    114);

            iconoSoporte.Font =
                new Font(
                    "Segoe UI Symbol",
                    22F);

            circuloSoporte.Controls.Add(
                iconoSoporte);

            ayuda.Controls.Add(
                titulo);

            ayuda.Controls.Add(
                btnInstructivo);

            ayuda.Controls.Add(
                btnLegal);

            ayuda.Controls.Add(
                lblSoporte);

            ayuda.Controls.Add(
                circuloSoporte);

            EventHandler ajustar =
                delegate
                {
                    circuloSoporte.Location =
                        new Point(
                            ayuda.ClientSize.Width -
                            circuloSoporte.Width -
                            28,
                            57);

                    lblSoporte.Location =
                        new Point(
                            circuloSoporte.Left -
                            lblSoporte.Width -
                            25,
                            69);
                };

            ayuda.Resize +=
                ajustar;

            pnlDashboard.Controls.Add(
                ayuda);

            ajustar(
                null,
                EventArgs.Empty);
        }

        // ============================================================
        // BOTÓN AYUDA
        // ============================================================

        private Guna2Button CrearBotonAyuda(
            string titulo,
            string subtitulo,
            string simbolo)
        {
            Guna2Button boton =
                new Guna2Button();

            boton.Size =
                new Size(245, 76);

            boton.BorderRadius =
                10;

            boton.FillColor =
                Color.FromArgb(
                    250,
                    251,
                    253);

            boton.BorderThickness =
                1;

            boton.BorderColor =
                ColorBorde;

            boton.ForeColor =
                ColorTexto;

            boton.Cursor =
                Cursors.Hand;

            boton.TextAlign =
                HorizontalAlignment.Left;

            boton.Padding =
                new Padding(
                    52,
                    0,
                    5,
                    0);

            boton.Font =
                new Font(
                    "Segoe UI",
                    9F);

            boton.Text =
                titulo +
                Environment.NewLine +
                subtitulo;

            Label icono =
                new Label();

            icono.Text =
                simbolo;

            icono.Size =
                new Size(45, 45);

            icono.Location =
                new Point(8, 16);

            icono.TextAlign =
                ContentAlignment.MiddleCenter;

            icono.BackColor =
                Color.Transparent;

            icono.ForeColor =
                Color.FromArgb(
                    59,
                    115,
                    205);

            icono.Font =
                new Font(
                    "Segoe UI Symbol",
                    22F);

            boton.Controls.Add(
                icono);

            boton.MouseEnter +=
                delegate
                {
                    boton.FillColor =
                        Color.FromArgb(
                            245,
                            248,
                            252);
                };

            boton.MouseLeave +=
                delegate
                {
                    boton.FillColor =
                        Color.FromArgb(
                            250,
                            251,
                            253);
                };

            return boton;
        }

        // ============================================================
        // LOGO EMPRESA
        // ============================================================

        private void CrearLogoEmpresa()
        {
            PictureBox pbLogoEmpresa =
                new PictureBox();

            pbLogoEmpresa.Image =
                PV.Properties.Resources.Logo_PTIA;

            pbLogoEmpresa.Size =
                new Size(115, 55);

            pbLogoEmpresa.SizeMode =
                PictureBoxSizeMode.Zoom;

            pbLogoEmpresa.BackColor =
                Color.Transparent;

            pbLogoEmpresa.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            pbLogoEmpresa.Location =
                new Point(
                    pnlDashboard.ClientSize.Width -
                    pbLogoEmpresa.Width -
                    24,
                    pnlDashboard.ClientSize.Height -
                    pbLogoEmpresa.Height -
                    18);

            pnlDashboard.Controls.Add(
                pbLogoEmpresa);

            pbLogoEmpresa.BringToFront();

            pnlDashboard.Resize +=
                delegate
                {
                    pbLogoEmpresa.Location =
                        new Point(
                            pnlDashboard.ClientSize.Width -
                            pbLogoEmpresa.Width -
                            24,
                            pnlDashboard.ClientSize.Height -
                            pbLogoEmpresa.Height -
                            18);
                };
        }

        // ============================================================
        // EXPANSIÓN EXCLUSIVA
        // ============================================================

        private void ModuloPrincipal_Expandido(
            object sender,
            EventArgs e)
        {
            flpMenu.SuspendLayout();

            try
            {
                foreach (Control control in flpMenu.Controls)
                {
                    SidebarMenuItem modulo =
                        control as SidebarMenuItem;

                    if (modulo != null &&
                        !object.ReferenceEquals(
                            modulo,
                            sender))
                    {
                        modulo.Expandir(false);
                    }
                }
            }
            finally
            {
                flpMenu.ResumeLayout(true);
            }

            ActualizarLayoutMenu();
        }

        private void ActualizarLayoutMenu()
        {
            if (flpMenu == null ||
                flpMenu.IsDisposed)
            {
                return;
            }

            AjustarAnchoMenu();

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo =
                    control as SidebarMenuItem;

                if (modulo != null)
                {
                    modulo.RecalcularLayoutCompleto();
                }
            }

            flpMenu.PerformLayout();
            flpMenu.Invalidate(true);
        }

        // ============================================================
        // LIMPIAR SELECCIÓN
        // ============================================================

        private void LimpiarSeleccionMenu()
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo =
                    control as SidebarMenuItem;

                if (modulo != null)
                {
                    modulo.LimpiarSeleccion();
                }
            }

            opcionSeleccionadaActual =
                null;

            flpMenu.PerformLayout();

            flpMenu.Invalidate(true);

            flpMenu.Refresh();
        }

        // ============================================================
        // CREAR MENÚ DESDE BASE DE DATOS
        // ============================================================

        private void CrearMenu()
        {
            flpMenu.SuspendLayout();

            try
            {
                // ====================================================
                // LIMPIAR MENÚ ACTUAL
                // ====================================================

                flpMenu.Controls.Clear();

                controlesMenuPorClave.Clear();

                favoritosMenu = null;

                moduloInventarios = null;
                moduloCompras = null;
                moduloVentas = null;
                moduloTesoreria = null;

                // ====================================================
                // CARGAR ÁRBOL DE PERMISOS DEL USUARIO AUTENTICADO
                // ====================================================

                dbPermisos =
                    new DBPermisos();

                arbolMenuUsuario =
                    dbPermisos
                    .ObtenerArbolPermisosUsuario(
                        DBLogin.usuario)
                    ?? new List<OpcionMenu>();

                // ====================================================
                // CREAR ÚNICAMENTE MÓDULOS PERMITIDOS
                // ====================================================

                foreach (OpcionMenu modulo in arbolMenuUsuario
                    .Where(
                        x =>
                            x.Activo &&
                            x.Permitido)
                    .OrderBy(
                        x => x.Orden)
                    .ThenBy(
                        x => x.IdOpcionMenu))
                {
                    SidebarMenuItem controlModulo =
                        CrearModuloDesdeBaseDatos(
                            modulo);

                    if (controlModulo == null)
                        continue;

                    flpMenu.Controls.Add(
                        controlModulo);

                    RegistrarReferenciaModulo(
                        modulo.Clave,
                        controlModulo);
                }

                // ====================================================
                // FAVORITOS
                // ====================================================

                favoritosMenu =
                    new FavoritosMenu();

                favoritosMenu.Margin =
                    new Padding(
                        0,
                        12,
                        0,
                        0);

                flpMenu.Controls.Add(
                    favoritosMenu);

                // Solo se registran como candidatos a favoritos
                // las opciones que fueron creadas para este usuario.
                foreach (Control control in flpMenu.Controls)
                {
                    SidebarMenuItem modulo =
                        control as SidebarMenuItem;

                    if (modulo != null)
                    {
                        modulo.RegistrarFavoritos(
                            favoritosMenu);
                    }
                }

                // Primero establecemos el ancho definitivo.
                AjustarAnchoMenu();

                // Después cargamos los favoritos del usuario.
                //
                // Si perdió permiso sobre una opción, esa opción no
                // existe en el árbol visual y no aparecerá aquí.
                favoritosMenu.UsuarioActual =
                    DBLogin.usuario;

                favoritosMenu.CargarFavoritosUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible cargar el menú del usuario." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                flpMenu.ResumeLayout(true);
            }
        }

        // ============================================================
        // CREAR MÓDULO RAÍZ DESDE BD
        // ============================================================

        private SidebarMenuItem CrearModuloDesdeBaseDatos(
            OpcionMenu modulo)
        {
            // Si el módulo no está activo o el usuario no tiene
            // permiso, ni siquiera se crea.
            if (modulo == null ||
                !modulo.Activo ||
                !modulo.Permitido)
            {
                return null;
            }

            SidebarMenuItem control =
                new SidebarMenuItem();

            control.Titulo =
                modulo.Nombre;

            control.Clave =
                modulo.EsOpcion
                    ? modulo.Clave
                    : "";

            control.Icono =
                ObtenerIconoModulo(
                    modulo.Clave);

            control.Margin =
                Padding.Empty;

            RegistrarControlMenu(
                modulo,
                control);

            // ========================================================
            // CREAR HIJOS PERMITIDOS
            // ========================================================

            if (modulo.Hijos != null)
            {
                foreach (OpcionMenu hijo in modulo.Hijos
                    .OrderBy(x => x.Orden)
                    .ThenBy(x => x.IdOpcionMenu))
                {
                    CrearHijoDesdeBaseDatos(
                        control,
                        hijo);
                }
            }

            // SidebarMenuItem propaga el evento de los descendientes
            // hasta el módulo raíz.
            control.OpcionSeleccionada +=
                Menu_OpcionSeleccionada;

            control.ModuloPrincipalExpandido +=
                ModuloPrincipal_Expandido;

            return control;
        }

        // ============================================================
        // CREAR HIJO RECURSIVAMENTE
        // ============================================================

        private SidebarMenuItem CrearHijoDesdeBaseDatos(
            SidebarMenuItem controlPadre,
            OpcionMenu opcion)
        {
            if (controlPadre == null ||
                opcion == null)
            {
                return null;
            }

            // ========================================================
            // PERMISO EFECTIVO
            // ========================================================
            //
            // Si esta opción está desactivada o no está permitida,
            // se corta TODA la rama aquí.
            //
            // Sus hijos pueden conservar Permitido = 1 en la BD,
            // pero no serán creados mientras su padre esté OFF.
            // ========================================================

            if (!opcion.Activo ||
                !opcion.Permitido)
            {
                return null;
            }

            string clave =
                opcion.EsOpcion
                    ? opcion.Clave
                    : "";

            SidebarMenuItem controlHijo =
                controlPadre.AgregarSubMenu(
                    opcion.Nombre,
                    clave);

            RegistrarControlMenu(
                opcion,
                controlHijo);

            if (opcion.Hijos != null)
            {
                foreach (OpcionMenu hijo in opcion.Hijos
                    .OrderBy(x => x.Orden)
                    .ThenBy(x => x.IdOpcionMenu))
                {
                    CrearHijoDesdeBaseDatos(
                        controlHijo,
                        hijo);
                }
            }

            return controlHijo;
        }

        // ============================================================
        // REGISTRAR CONTROL POR CLAVE
        // ============================================================

        private void RegistrarControlMenu(
            OpcionMenu opcion,
            SidebarMenuItem control)
        {
            if (opcion == null ||
                control == null ||
                string.IsNullOrWhiteSpace(
                    opcion.Clave))
            {
                return;
            }

            // Normalizamos para evitar problemas por espacios
            // accidentales almacenados en la BD.
            string clave =
                opcion.Clave.Trim();

            controlesMenuPorClave[clave] =
                control;
        }

        // ============================================================
        // REFERENCIAS DE MÓDULOS PARA EL DASHBOARD
        // ============================================================

        private void RegistrarReferenciaModulo(
            string clave,
            SidebarMenuItem modulo)
        {
            if (string.IsNullOrWhiteSpace(clave) ||
                modulo == null)
            {
                return;
            }

            switch (clave.Trim().ToUpperInvariant())
            {
                case "INVENTARIOS":
                    moduloInventarios =
                        modulo;
                    break;

                case "COMPRAS":
                    moduloCompras =
                        modulo;
                    break;

                case "VENTAS":
                    moduloVentas =
                        modulo;
                    break;

                case "TESORERIA":
                    moduloTesoreria =
                        modulo;
                    break;
            }
        }

        // ============================================================
        // ICONOS DE MÓDULOS
        // ============================================================

        private Image ObtenerIconoModulo(
            string clave)
        {
            if (string.IsNullOrWhiteSpace(
                clave))
            {
                return null;
            }

            switch (clave.Trim().ToUpperInvariant())
            {
                case "PARAMETROS":
                    return
                        PV.Properties.Resources.filtrar;

                case "CATALOGOS":
                    return
                        PV.Properties.Resources.folder;

                case "INVENTARIOS":
                    return
                        PV.Properties.Resources.lista_de_verificacion;

                case "COMPRAS":
                    return
                        PV.Properties.Resources.carrito_de_compras;

                case "VENTAS":
                    return
                        PV.Properties.Resources.grafico_de_barras;

                case "TESORERIA":
                    return
                        PV.Properties.Resources.banco;

                case "UTILERIAS":
                    return
                        PV.Properties.Resources.renovacion;

                case "PRESUPUESTO":
                    return
                        PV.Properties.Resources.presupuesto;

                default:
                    return null;
            }
        }
        // ============================================================
        // ABRIR ADMINISTRACIÓN DEL MENÚ
        // ============================================================

        private void AbrirAdministrarMenu()
        {
            try
            {
                using (AdministrarMenu formulario =
                    new AdministrarMenu())
                {
                    formulario.StartPosition =
                        FormStartPosition.CenterParent;

                    formulario.ShowInTaskbar =
                        false;

                    formulario.ShowDialog(this);
                }

                // Volvemos a consultar:
                //
                // OpcionMenu
                // UsuarioPermiso
                //
                // y reconstruimos completamente el menú visual.
                RecargarMenuUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible abrir la administración del menú." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "SIGA",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                LimpiarSeleccionMenu();
            }
        }

        // ============================================================
        // OPCIÓN SELECCIONADA
        // ============================================================

        private void Menu_OpcionSeleccionada(
            object sender,
            OpcionMenuSeleccionadaEventArgs e)
        {
            if (e == null ||
                e.Opcion == null ||
                string.IsNullOrWhiteSpace(
                    e.Opcion.Clave))
            {
                return;
            }

            // Normalizamos la clave recibida.
            string clave =
                e.Opcion.Clave.Trim();

            // ========================================================
            // VALIDAR PERMISO
            // ========================================================
            //
            // La opción solamente puede ejecutarse si realmente
            // pertenece al menú que fue generado para el usuario
            // que inició sesión.
            //
            // controlesMenuPorClave solamente contiene opciones
            // que superaron:
            //
            // Activo = 1
            // Permitido = 1
            // Padres permitidos
            //
            // ========================================================

            if (!controlesMenuPorClave.ContainsKey(
                clave))
            {
                MessageBox.Show(
                    "No tiene permiso para acceder a esta opción.",
                    "Permisos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            LimpiarSeleccionMenu();

            opcionSeleccionadaActual =
                e.Opcion;

            opcionSeleccionadaActual.Seleccionado =
                true;

            switch (clave)
            {
                // ====================================================
                // PARÁMETROS
                // ====================================================

                case "DATOS_EMPRESA":

                    AbrirFormulario(
                        new DatosEmpresas());

                    break;

                case "USUARIOS":

                    AbrirFormulario(
                        new Usuarios());

                    break;

                case "ADMINISTRAR_MENU":

                    AbrirAdministrarMenu();

                    break;


                // ====================================================
                // CATÁLOGOS
                // ====================================================

                case "DIVISAS":

                    AbrirFormulario(
                        new CatalogoDivisa());

                    break;

                case "ALMACENES":

                    AbrirFormulario(
                        new Almacenes());

                    break;

                case "CATEGORIAS_FAMILIAS":

                    AbrirFormulario(
                        new CatalogoFamilias());

                    break;

                case "PRODUCTOS":

                    AbrirFormulario(
                        new CatalogoProductosServicios(0));

                    break;

                case "SERVICIOS":

                    AbrirFormulario(
                        new CatalogoProductosServicios(0));

                    break;

                case "CENTROS_COSTOS":

                    AbrirFormulario(
                        new CentroCostos());

                    break;

                case "DOCUMENTOS":

                    AbrirFormulario(
                        new Documentos());

                    break;

                case "CONCEPTOS_GLOBALES":

                    AbrirFormulario(
                        new ConceptosGlobales());

                    break;

                case "FORMAS_PAGO":

                    AbrirFormulario(
                        new CatalogoFormasPago());

                    break;

                case "EMPLEADOS":

                    AbrirFormulario(
                        new CatalogoPersonal());

                    break;

                case "TIPOS_ZONAS":

                    AbrirFormulario(
                        new TiposZonas());

                    break;

                case "CLIENTES":

                    AbrirFormulario(
                        new Clientes());

                    break;

                case "PROVEEDORES":

                    AbrirFormulario(
                        new Proveedores());

                    break;

                case "CUENTAS_BANCARIAS":

                    AbrirFormulario(
                        new CuentasBancarias());

                    break;


                // ====================================================
                // INVENTARIOS
                // ====================================================

                case "TIPO_MOVIMIENTOS":

                    AbrirFormulario(
                        new TipoMovimientos());

                    break;

                case "REGISTRAR_ENTRADAS":

                    AbrirFormulario(
                        new RegistrarEntrada2("E"));

                    break;

                case "REGISTRAR_SALIDAS":

                    AbrirFormulario(
                        new RegistrarEntrada2("S"));

                    break;

                case "REGISTRAR_TRASPASOS":

                    AbrirFormulario(
                        new RegistrarEntrada2("T"));

                    break;

                case "CONSULTA_INVENTARIOS":

                    AbrirFormulario(
                        new ConsultaInventario2(0));

                    break;

                case "REPORTE_EXISTENCIAS_ALMACEN":

                    AbrirFormulario(
                        new FiltroExistencias());

                    break;

                case "REPORTE_COSTO_PRODUCTO":

                    AbrirFormulario(
                        new FiltroValorProducto());

                    break;


                // ====================================================
                // COMPRAS
                // ====================================================

                case "REQUISICIONES":

                    AbrirFormulario(
                        new Requisicion2());

                    break;

                case "PEDIDOS_PROVEEDORES":

                    AbrirFormulario(
                        new OrdenCompra2());

                    break;

                case "COMPRAS_GASTOS":

                    AbrirFormulario(
                        new RegistroGastos2());

                    break;

                case "COMPRAS_REEMBOLSO":

                    AbrirFormulario(
                        new RegistroReembolsos());

                    break;

                case "COMPRAS_INVENTARIABLES":

                    AbrirFormulario(
                        new RecepcionProductos2());

                    break;

                case "DIARIO_REQUISICIONES":

                    AbrirFormulario(
                        new ReporteDiarioRequisicionFiltro());

                    break;

                case "DIARIO_ORDENES_COMPRA":

                    AbrirFormulario(
                        new ReporteDiarioOrdenesComprasFiltro());

                    break;

                case "DIARIO_COMPRAS_INVENTARIABLES":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Compras"));

                    break;

                case "DIARIO_REEMBOLSOS":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Reembolsos"));

                    break;

                case "DIARIO_GASTOS":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Gastos"));

                    break;

                case "EGRESOS_COMPRAS":

                    AbrirFormulario(
                        new ReporteEgresosFiltro());

                    break;

                case "REPORTE_DIARIO_INGRESOS":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Ingresos"));

                    break;

                case "SALDO_COMPRAS":

                    AbrirFormulario(
                        new ReporteComprasFiltro());

                    break;

                case "ANTICIPOS_COMPRAS_REGISTRO":

                    AbrirFormulario(
                        new ReporteAnticipoProveedorFiltro());

                    break;

                case "ESTADO_CUENTA_PROVEEDORES":

                    AbrirFormulario(
                        new ReporteEstadoCuentaProveedor());

                    break;


                // ====================================================
                // VENTAS
                // ====================================================

                case "PEDIDOS_CLIENTES":

                    AbrirFormulario(
                        new OrdenPedidoCliente(
                            "Pedido"));

                    break;

                case "REMISIONES":

                    AbrirFormulario(
                        new OrdenPedidoCliente(
                            "Remision"));

                    break;

                case "REPORTE_DIARIO_REMISIONES":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Remisiones"));

                    break;

                case "REPORTE_UTILIDAD_PEDIDO":

                    AbrirFormulario(
                        new FiltroFecha(
                            "Utilidad Pedido"));

                    break;

                case "REPORTE_UTILIDAD_PRODUCTO":

                    AbrirFormulario(
                        new FiltroFecha(
                            "Utilidad Producto"));

                    break;

                case "FACTURAS":

                    AbrirFormulario(
                        new Facturas());

                    break;

                case "REPORTE_DIARIO_FACTURAS":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Facturas"));

                    break;

                case "DASHBOARD_VENTAS":

                    AbrirFormulario(
                        new DashboardVentas());

                    break;


                // ====================================================
                // TESORERÍA - BANCOS
                // ====================================================

                case "REGISTRAR_MOVIMIENTOS_BANCOS":

                    AbrirFormulario(
                        new RegistroMovimientoBancos());

                    break;

                case "CONCEPTOS_COBRO_PAGO":

                    AbrirFormulario(
                        new CatalogoConceptosTesoreria());

                    break;


                // ====================================================
                // TESORERÍA - CxC
                // ====================================================

                case "REGISTRAR_INGRESO":

                    AbrirFormulario(
                        new registroIngresos(
                            "Remision",
                            ""));

                    break;

                case "REGISTRAR_ANTICIPO_CLIENTE":

                    AbrirFormulario(
                        new RegistrarAnticipo(
                            "Propietario"));

                    break;

                case "APLICAR_ANTICIPO_CLIENTE":

                    AbrirFormulario(
                        new AplicarAnticipo());

                    break;


                // ====================================================
                // TESORERÍA - CxP
                // ====================================================

                case "PAGOS_PROVEEDOR":

                    AbrirFormulario(
                        new RegistroEgreso());

                    break;

                case "PAGOS_VENCIMIENTO":

                    AbrirFormulario(
                        new ConsultarEgreso());

                    break;

                case "REPORTE_INGRESOS_TESORERIA":

                    AbrirFormulario(
                        new ReporteIngresoFormulario());

                    break;

                case "REPORTE_ANTICIPO_TESORERIA":

                    AbrirFormulario(
                        new ReporteAnticiposFiltro());

                    break;

                case "REPORTE_DIARIO_EGRESOS":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Diario Egresos"));

                    break;

                case "SALDOS_PROVEEDORES_TESORERIA":

                    AbrirFormulario(
                        new ReporteDiarioComprasFiltro(
                            "Saldos Proveedor"));

                    break;

                case "ESTADO_CUENTA_PROVEEDOR_TESORERIA":

                    AbrirFormulario(
                        new ReporteEstadoCuentaProveedor());

                    break;

                case "DEFINIR_POLIZAS_EGRESOS":

                    AbrirFormulario(
                        new DefinePolizas(
                            "Definiciones Compras"));

                    break;

                case "GENERAR_POLIZAS_EGRESOS":

                    AbrirFormulario(
                        new GENERARPOLIZAS(
                            "Polizas Compras"));

                    break;


                // ====================================================
                // TESORERÍA - FINANZAS
                // ====================================================

                case "REPORTE_RESULTADOS_GLOBAL":

                    AbrirFormulario(
                        new FiltrarReporteResultadosGlobal());

                    break;


                // ====================================================
                // PRESUPUESTO
                // ====================================================

                case "PERIODOS_PRESUPUESTO":

                    AbrirFormulario(
                        new CatalogoPeriodos());

                    break;

                case "CONCEPTOS_PRESUPUESTO":

                    AbrirFormulario(
                        new ConceptosPresupuesto());

                    break;


                // ====================================================
                // SIN FORMULARIO ASOCIADO
                // ====================================================

                default:

                    MessageBox.Show(
                        "La opción \"" +
                        clave +
                        "\" no tiene una ventana asociada.",
                        "SIGA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    break;
            }
        }

        // ============================================================
        // ABRIR FORMULARIO COMO VENTANA MODAL
        // ============================================================

        private void AbrirFormulario(
            Form formulario)
        {
            if (formulario == null)
                return;

            try
            {
                formulario.StartPosition =
                    FormStartPosition.CenterParent;

                formulario.ShowInTaskbar =
                    false;

                using (formulario)
                {
                    formulario.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible abrir la ventana." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "SIGA",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Al cerrar la ventana eliminamos el estado visual
                // de selección del menú.
                LimpiarSeleccionMenu();
            }
        }

        // ============================================================
        // REGRESAR AL DASHBOARD
        // ============================================================

        private void MostrarDashboard()
        {
            LimpiarSeleccionMenu();

            CrearDashboardInicio();
        }

        // ============================================================
        // AJUSTAR ANCHO DEL MENÚ
        // ============================================================

        private void AjustarAnchoMenu()
        {
            if (flpMenu == null ||
                flpMenu.IsDisposed)
            {
                return;
            }

            // Margen fijo que evita que los controles cambien de
            // ancho cuando aparece/desaparece la barra vertical.
            const int margenDerecho = 13;

            int anchoDisponible =
                flpMenu.Width -
                flpMenu.Padding.Left -
                flpMenu.Padding.Right -
                margenDerecho;

            anchoDisponible =
                Math.Max(
                    100,
                    anchoDisponible);

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo =
                    control as SidebarMenuItem;

                if (modulo != null)
                {
                    if (modulo.Width !=
                        anchoDisponible)
                    {
                        modulo.Width =
                            anchoDisponible;
                    }

                    modulo
                        .RecalcularLayoutCompleto();
                }
                else if (
                    control is FavoritosMenu)
                {
                    FavoritosMenu favoritos =
                        control as FavoritosMenu;

                    favoritos.Margin =
                        new Padding(
                            0,
                            12,
                            0,
                            0);

                    favoritos.Width =
                        anchoDisponible;
                }
                else
                {
                    control.Width =
                        anchoDisponible;
                }
            }
        }

        // ============================================================
        // CAMBIO DE TAMAÑO DEL MENÚ
        // ============================================================

        private void flpMenu_SizeChanged(
            object sender,
            EventArgs e)
        {
            AjustarAnchoMenu();
        }

        // ============================================================
        // RECARGAR MENÚ DEL USUARIO
        // ============================================================
        //
        // Permite volver a consultar OpcionMenu + UsuarioPermiso
        // sin tener que reconstruir manualmente el menú.
        // ============================================================

        public void RecargarMenuUsuario()
        {
            CrearMenu();

            CrearDashboardInicio();
        }

        // ============================================================
        // VERIFICAR SI UNA CLAVE ESTÁ DISPONIBLE
        // ============================================================

        private bool UsuarioTieneOpcion(
            string clave)
        {
            if (string.IsNullOrWhiteSpace(
                clave))
            {
                return false;
            }

            return
                controlesMenuPorClave
                .ContainsKey(
                    clave.Trim());
        }

        // ============================================================
        // CLIC EN INICIO
        // ============================================================

        private void btnInicio_Click(
            object sender,
            EventArgs e)
        {
            MostrarDashboard();
        }

        // ============================================================
        // MINIMIZAR
        // ============================================================

        private void btnMinimizar_Click(
            object sender,
            EventArgs e)
        {
            WindowState =
                FormWindowState.Minimized;
        }

        // ============================================================
        // MAXIMIZAR / RESTAURAR
        // ============================================================

        private void btnMaximizar_Click(
            object sender,
            EventArgs e)
        {
            if (WindowState ==
                FormWindowState.Maximized)
            {
                WindowState =
                    FormWindowState.Normal;
            }
            else
            {
                WindowState =
                    FormWindowState.Maximized;
            }
        }

        // ============================================================
        // CERRAR
        // ============================================================

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            DialogResult resultado =
                MessageBox.Show(
                    "¿Desea cerrar el sistema?",
                    "SIGA",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado ==
                DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ============================================================
        // MOVER VENTANA DESDE ENCABEZADO
        // ============================================================

        private void pnlBarraVentana_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            if (e.Button !=
                MouseButtons.Left)
            {
                return;
            }

            ReleaseCapture();

            SendMessage(
                Handle,
                WM_NCLBUTTONDOWN,
                new IntPtr(
                    HT_CAPTION),
                IntPtr.Zero);
        }

        // ============================================================
        // CERRAR SESIÓN
        // ============================================================

        private void CerrarSesion()
        {
            DialogResult resultado =
                MessageBox.Show(
                    "¿Desea cerrar la sesión actual?",
                    "Cerrar sesión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado !=
                DialogResult.Yes)
            {
                return;
            }

            Hide();

            Login login =
                new Login();

            login.Show();

            Close();
        }

        // ============================================================
        // EVENTO FORM CLOSING
        // ============================================================

        private void MenuPrincipal_v2_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            // Aquí puedes conservar cualquier lógica adicional
            // que ya tengas para el cierre del formulario.
        }
    }
}