using Condominios;
using PuntoVentas;
using PuntoVentas.Clases.Login;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PV.Clases.Usuarios;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Usuarios;
using System.Drawing.Imaging;

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

        private readonly Color ColorFondo = Color.FromArgb(242, 245, 249);
        private readonly Color ColorBlanco = Color.White;
        private readonly Color ColorAzulOscuro = Color.FromArgb(21, 48, 87);
        private readonly Color ColorTexto = Color.FromArgb(31, 57, 91);
        private readonly Color ColorTextoSecundario = Color.FromArgb(95, 114, 139);
        private readonly Color ColorBorde = Color.FromArgb(226, 231, 238);

        public MenuPrincipal_v2()
        {
            InitializeComponent();

            pnlMenu.Width = 270;

            ConfigurarMenu();
            CrearMenu();

            lblTipoUsuario.Text = DBLogin.TipoUsuario;
            lblNombreUsuario.Text = DBLogin.usuario;

            CargarFotoUsuario();
            CrearDashboardInicio();
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void ConfigurarMenu()
        {
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.WrapContents = false;
            flpMenu.AutoScroll = true;
            flpMenu.Padding = new Padding(8, 6, 8, 6);
            flpMenu.HorizontalScroll.Enabled = false;
            flpMenu.HorizontalScroll.Visible = false;
            flpMenu.SizeChanged += flpMenu_SizeChanged;
        }
        private Image MejorarIcono(Image imagen)
        {
            if (imagen == null)
                return null;

            Bitmap resultado = new Bitmap(imagen.Width, imagen.Height);

            using (Graphics g = Graphics.FromImage(resultado))
            {
                ColorMatrix matriz = new ColorMatrix(
                    new float[][]
                    {
                new float[] { 1.35F, 0, 0, 0, 0 },
                new float[] { 0, 1.35F, 0, 0, 0 },
                new float[] { 0, 0, 1.35F, 0, 0 },
                new float[] { 0, 0, 0, 1F, 0 },
                new float[] { -0.08F, -0.08F, -0.08F, 0, 1F }
                    });

                using (ImageAttributes atributos = new ImageAttributes())
                {
                    atributos.SetColorMatrix(matriz);

                    g.DrawImage(
                        imagen,
                        new Rectangle(0, 0, resultado.Width, resultado.Height),
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

        private void CargarFotoUsuario()
        {
            pbFoto.SizeMode = PictureBoxSizeMode.Zoom;
            pbFoto.BorderRadius = pbFoto.Width / 2;

            DBUsuarios dbUsuarios = new DBUsuarios();
            dbUsuarios.CargarFotoUsuario(DBLogin.usuario, pbFoto);
        }

        private void CrearDashboardInicio()
        {
            pnlContenido.SuspendLayout();
            pnlContenido.Controls.Clear();

            pnlContenido.BackColor = ColorFondo;
            pnlContenido.Padding = new Padding(28, 22, 28, 24);

            pnlDashboard = new Guna2Panel();
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.FillColor = ColorBlanco;
            pnlDashboard.BorderRadius = 18;
            pnlDashboard.BorderThickness = 1;
            pnlDashboard.BorderColor = ColorBorde;
            pnlDashboard.Padding = new Padding(26);

            pnlDashboard.ShadowDecoration.Enabled = true;
            pnlDashboard.ShadowDecoration.Depth = 3;
            pnlDashboard.ShadowDecoration.Color = Color.FromArgb(60, 80, 100);

            pnlContenido.Controls.Add(pnlDashboard);

            CrearAyudaDashboard();
            CrearModulosPrincipalesDashboard();
            CrearBienvenidaDashboard();
            CrearEncabezadoDashboard();
            CrearLogoEmpresa();

            pnlContenido.ResumeLayout(true);
        }

        private void CrearEncabezadoDashboard()
        {
            Panel encabezado = new Panel();
            encabezado.Dock = DockStyle.Top;
            encabezado.Height = 88;
            encabezado.BackColor = ColorBlanco;

            PictureBox pbLogoSiga = new PictureBox();
            pbLogoSiga.Image = PV.Properties.Resources.siga_logo;
            pbLogoSiga.Size = new Size(150, 65);
            pbLogoSiga.Location = new Point(4, 4);
            pbLogoSiga.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogoSiga.BackColor = Color.Transparent;

            Panel separador = new Panel();
            separador.Location = new Point(180, 10);
            separador.Size = new Size(1, 55);
            separador.BackColor = Color.FromArgb(220, 226, 234);

            Label lblSistema = new Label();
            lblSistema.Text = "Sistema Integral de Gestión Administrativa";
            lblSistema.AutoSize = true;
            lblSistema.Location = new Point(210, 12);
            lblSistema.ForeColor = ColorAzulOscuro;
            lblSistema.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, FontStyle.Bold);

            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Bienvenido al sistema";
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(210, 40);
            lblSubtitulo.ForeColor = ColorTextoSecundario;
            lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            encabezado.Controls.Add(pbLogoSiga);
            encabezado.Controls.Add(separador);
            encabezado.Controls.Add(lblSistema);
            encabezado.Controls.Add(lblSubtitulo);

            pnlDashboard.Controls.Add(encabezado);
        }

        private void CrearBienvenidaDashboard()
        {
            Guna2Panel bienvenida = new Guna2Panel();
            bienvenida.Dock = DockStyle.Top;
            bienvenida.Height = 245;
            bienvenida.FillColor = ColorBlanco;
            bienvenida.BorderRadius = 14;
            bienvenida.BorderThickness = 1;
            bienvenida.BorderColor = ColorBorde;

            Guna2Panel fondoIcono = new Guna2Panel();
            fondoIcono.Size = new Size(105, 105);
            fondoIcono.Location = new Point(35, 65);
            fondoIcono.BorderRadius = 52;
            fondoIcono.FillColor = Color.FromArgb(224, 235, 251);

            PictureBox icono = new PictureBox();
            icono.Size = new Size(54, 54);
            icono.Location = new Point(25, 25);
            icono.SizeMode = PictureBoxSizeMode.Zoom;
            icono.BackColor = Color.Transparent;
            icono.Image = MejorarIcono(PV.Properties.Resources.folder);

            fondoIcono.Controls.Add(icono);

            Label lblBienvenido = new Label();
            lblBienvenido.Text = "Bienvenido, " + DBLogin.usuario;
            lblBienvenido.AutoSize = true;
            lblBienvenido.Location = new Point(175, 77);
            lblBienvenido.ForeColor = ColorAzulOscuro;
            lblBienvenido.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, FontStyle.Bold);

            Label lblMensaje = new Label();
            lblMensaje.Text = "Selecciona una opción del menú\npara comenzar.";
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(177, 118);
            lblMensaje.ForeColor = ColorTextoSecundario;
            lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F);

            Guna2Panel ilustracion = new Guna2Panel();
            ilustracion.Size = new Size(340, 170);
            ilustracion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ilustracion.BorderRadius = 24;
            ilustracion.FillColor = Color.FromArgb(247, 249, 253);

            Guna2Panel monitor = new Guna2Panel();
            monitor.Size = new Size(190, 108);
            monitor.Location = new Point(75, 22);
            monitor.BorderRadius = 8;
            monitor.FillColor = Color.FromArgb(43, 68, 111);

            Panel pantalla = new Panel();
            pantalla.Location = new Point(10, 9);
            pantalla.Size = new Size(170, 85);
            pantalla.BackColor = Color.White;

            Panel grafica1 = new Panel();
            grafica1.Location = new Point(25, 49);
            grafica1.Size = new Size(12, 24);
            grafica1.BackColor = Color.FromArgb(175, 195, 235);

            Panel grafica2 = new Panel();
            grafica2.Location = new Point(44, 37);
            grafica2.Size = new Size(12, 36);
            grafica2.BackColor = Color.FromArgb(135, 165, 220);

            Panel grafica3 = new Panel();
            grafica3.Location = new Point(63, 26);
            grafica3.Size = new Size(12, 47);
            grafica3.BackColor = Color.FromArgb(95, 135, 200);

            Label circuloGrafica = new Label();
            circuloGrafica.Text = "◕";
            circuloGrafica.AutoSize = true;
            circuloGrafica.Location = new Point(112, 25);
            circuloGrafica.ForeColor = Color.FromArgb(110, 140, 205);
            circuloGrafica.Font = new System.Drawing.Font("Segoe UI Symbol", 27F);

            pantalla.Controls.Add(grafica1);
            pantalla.Controls.Add(grafica2);
            pantalla.Controls.Add(grafica3);
            pantalla.Controls.Add(circuloGrafica);

            monitor.Controls.Add(pantalla);

            Panel pieMonitor = new Panel();
            pieMonitor.Size = new Size(10, 16);
            pieMonitor.Location = new Point(165, 128);
            pieMonitor.BackColor = Color.FromArgb(130, 150, 195);

            Panel baseMonitor = new Panel();
            baseMonitor.Size = new Size(70, 8);
            baseMonitor.Location = new Point(135, 143);
            baseMonitor.BackColor = Color.FromArgb(130, 150, 195);

            ilustracion.Controls.Add(monitor);
            ilustracion.Controls.Add(pieMonitor);
            ilustracion.Controls.Add(baseMonitor);

            bienvenida.Controls.Add(fondoIcono);
            bienvenida.Controls.Add(lblBienvenido);
            bienvenida.Controls.Add(lblMensaje);
            bienvenida.Controls.Add(ilustracion);

            bienvenida.Resize += delegate
            {
                ilustracion.Left = bienvenida.ClientSize.Width - ilustracion.Width - 28;
                ilustracion.Top = 36;
                ilustracion.Visible = bienvenida.ClientSize.Width >= 900;
            };

            pnlDashboard.Controls.Add(bienvenida);
        }

        private void CrearModulosPrincipalesDashboard()
        {
            Panel seccion = new Panel();
            seccion.Dock = DockStyle.Top;
            seccion.Height = 215;
            seccion.BackColor = ColorBlanco;

            Label titulo = new Label();
            titulo.Text = "Módulos principales";
            titulo.AutoSize = true;
            titulo.Location = new Point(3, 18);
            titulo.ForeColor = ColorAzulOscuro;
            titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            Panel linea = new Panel();
            linea.Height = 1;
            linea.Location = new Point(145, 29);
            linea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linea.BackColor = ColorBorde;

            Guna2Panel inventarios = CrearTarjetaModulo(
                "Inventarios",
                "Consulta y control\nde existencias",
                PV.Properties.Resources.lista_de_verificacion,
                Color.FromArgb(220, 234, 255),
                delegate
                {
                    AbrirModuloDesdeDashboard(moduloInventarios);
                });

            Guna2Panel compras = CrearTarjetaModulo(
                "Compras",
                "Órdenes, recepciones\ny proveedores",
                PV.Properties.Resources.carrito_de_compras,
                Color.FromArgb(220, 242, 224),
                delegate
                {
                    AbrirModuloDesdeDashboard(moduloCompras);
                });

            Guna2Panel ventas = CrearTarjetaModulo(
                "Ventas",
                "Pedidos, remisiones\ny facturación",
                PV.Properties.Resources.grafico_de_barras,
                Color.FromArgb(255, 239, 185),
                delegate
                {
                    AbrirModuloDesdeDashboard(moduloVentas);
                });

            Guna2Panel tesoreria = CrearTarjetaModulo(
                "Tesorería",
                "Cobros, pagos y\nmovimientos",
                PV.Properties.Resources.banco,
                Color.FromArgb(232, 217, 250),
                delegate
                {
                    AbrirModuloDesdeDashboard(moduloTesoreria);
                });

            seccion.Controls.Add(titulo);
            seccion.Controls.Add(linea);
            seccion.Controls.Add(inventarios);
            seccion.Controls.Add(compras);
            seccion.Controls.Add(ventas);
            seccion.Controls.Add(tesoreria);

            EventHandler ajustar = delegate
            {
                int margen = 3;
                int separacion = 16;
                int disponible = seccion.ClientSize.Width - margen * 2 - separacion * 3;
                int ancho = disponible / 4;

                inventarios.Size = new Size(ancho, 124);
                compras.Size = new Size(ancho, 124);
                ventas.Size = new Size(ancho, 124);
                tesoreria.Size = new Size(ancho, 124);

                inventarios.Location = new Point(margen, 62);
                compras.Location = new Point(inventarios.Right + separacion, 62);
                ventas.Location = new Point(compras.Right + separacion, 62);
                tesoreria.Location = new Point(ventas.Right + separacion, 62);

                linea.Width = Math.Max(0, seccion.ClientSize.Width - linea.Left - 3);
            };

            seccion.Resize += ajustar;

            pnlDashboard.Controls.Add(seccion);

            ajustar(null, EventArgs.Empty);
        }

        private Guna2Panel CrearTarjetaModulo(string titulo, string descripcion, Image imagen, Color fondo, EventHandler accion)
        {
            Guna2Panel tarjeta = new Guna2Panel();
            tarjeta.BorderRadius = 12;
            tarjeta.FillColor = Color.White;
            tarjeta.BorderColor = ColorBorde;
            tarjeta.BorderThickness = 1;
            tarjeta.Cursor = Cursors.Hand;

            Guna2Panel fondoIcono = new Guna2Panel();
            fondoIcono.Size = new Size(70, 70);
            fondoIcono.Location = new Point(18, 26);
            fondoIcono.BorderRadius = 35;
            fondoIcono.FillColor = fondo;
            fondoIcono.Cursor = Cursors.Hand;

            PictureBox icono = new PictureBox();
            icono.Image = MejorarIcono(imagen);
            icono.Size = new Size(40, 40);
            icono.Location = new Point(15, 15);
            icono.SizeMode = PictureBoxSizeMode.Zoom;
            icono.BackColor = Color.Transparent;
            icono.Cursor = Cursors.Hand;

            fondoIcono.Controls.Add(icono);

            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(103, 29);
            lblTitulo.ForeColor = ColorTexto;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Cursor = Cursors.Hand;

            Label lblDescripcion = new Label();
            lblDescripcion.Text = descripcion;
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(104, 58);
            lblDescripcion.ForeColor = ColorTextoSecundario;
            lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDescripcion.BackColor = Color.Transparent;
            lblDescripcion.Cursor = Cursors.Hand;

            tarjeta.Controls.Add(fondoIcono);
            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Controls.Add(lblDescripcion);

            tarjeta.Click += accion;
            fondoIcono.Click += accion;
            icono.Click += accion;
            lblTitulo.Click += accion;
            lblDescripcion.Click += accion;

            tarjeta.MouseEnter += delegate
            {
                tarjeta.FillColor = Color.FromArgb(248, 250, 253);
                tarjeta.BorderColor = Color.FromArgb(202, 214, 228);
            };

            tarjeta.MouseLeave += delegate
            {
                tarjeta.FillColor = Color.White;
                tarjeta.BorderColor = ColorBorde;
            };

            return tarjeta;
        }

        private void AbrirModuloDesdeDashboard(SidebarMenuItem modulo)
        {
            if (modulo == null)
                return;

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem item = control as SidebarMenuItem;

                if (item != null && !object.ReferenceEquals(item, modulo))
                    item.Expandir(false);
            }

            modulo.Expandir(true);

            flpMenu.PerformLayout();
            flpMenu.ScrollControlIntoView(modulo);
            modulo.Focus();
        }

        private void CrearAyudaDashboard()
        {
            Guna2Panel ayuda = new Guna2Panel();
            ayuda.Dock = DockStyle.Top;
            ayuda.Height = 165;
            ayuda.FillColor = Color.White;
            ayuda.BorderRadius = 14;
            ayuda.BorderColor = ColorBorde;
            ayuda.BorderThickness = 1;

            Label titulo = new Label();
            titulo.Text = "Documentación y ayuda";
            titulo.AutoSize = true;
            titulo.Location = new Point(25, 18);
            titulo.ForeColor = ColorAzulOscuro;
            titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            Guna2Button btnInstructivo = CrearBotonAyuda(
                "Instructivo",
                "Manual de usuario",
                "▣");

            btnInstructivo.Location = new Point(25, 58);

            Guna2Button btnLegal = CrearBotonAyuda(
                "Información legal",
                "Avisos y políticas",
                "▤");

            btnLegal.Location = new Point(288, 58);

            Label lblSoporte = new Label();
            lblSoporte.Text = "¿Necesitas ayuda?\nContacta a soporte técnico";
            lblSoporte.AutoSize = true;
            lblSoporte.TextAlign = ContentAlignment.MiddleRight;
            lblSoporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSoporte.ForeColor = ColorTextoSecundario;
            lblSoporte.Font = new System.Drawing.Font("Segoe UI", 9F);

            Guna2Panel circuloSoporte = new Guna2Panel();
            circuloSoporte.Size = new Size(62, 62);
            circuloSoporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            circuloSoporte.BorderRadius = 31;
            circuloSoporte.FillColor = Color.FromArgb(245, 247, 251);

            Label iconoSoporte = new Label();
            iconoSoporte.Text = "☏";
            iconoSoporte.Dock = DockStyle.Fill;
            iconoSoporte.TextAlign = ContentAlignment.MiddleCenter;
            iconoSoporte.ForeColor = Color.FromArgb(70, 88, 114);
            iconoSoporte.Font = new System.Drawing.Font("Segoe UI Symbol", 22F);

            circuloSoporte.Controls.Add(iconoSoporte);

            ayuda.Controls.Add(titulo);
            ayuda.Controls.Add(btnInstructivo);
            ayuda.Controls.Add(btnLegal);
            ayuda.Controls.Add(lblSoporte);
            ayuda.Controls.Add(circuloSoporte);

            EventHandler ajustar = delegate
            {
                circuloSoporte.Location = new Point(
                    ayuda.ClientSize.Width - circuloSoporte.Width - 28,
                    57);

                lblSoporte.Location = new Point(
                    circuloSoporte.Left - lblSoporte.Width - 25,
                    69);
            };

            ayuda.Resize += ajustar;

            pnlDashboard.Controls.Add(ayuda);

            ajustar(null, EventArgs.Empty);
        }

        private Guna2Button CrearBotonAyuda(string titulo, string subtitulo, string simbolo)
        {
            Guna2Button boton = new Guna2Button();
            boton.Size = new Size(245, 76);
            boton.BorderRadius = 10;
            boton.FillColor = Color.FromArgb(250, 251, 253);
            boton.BorderThickness = 1;
            boton.BorderColor = ColorBorde;
            boton.ForeColor = ColorTexto;
            boton.Cursor = Cursors.Hand;
            boton.TextAlign = HorizontalAlignment.Left;
            boton.Padding = new Padding(52, 0, 5, 0);
            boton.Font = new System.Drawing.Font("Segoe UI", 9F);
            boton.Text = titulo + Environment.NewLine + subtitulo;

            Label icono = new Label();
            icono.Text = simbolo;
            icono.Size = new Size(45, 45);
            icono.Location = new Point(8, 16);
            icono.TextAlign = ContentAlignment.MiddleCenter;
            icono.BackColor = Color.Transparent;
            icono.ForeColor = Color.FromArgb(59, 115, 205);
            icono.Font = new System.Drawing.Font("Segoe UI Symbol", 22F);

            boton.Controls.Add(icono);

            boton.MouseEnter += delegate
            {
                boton.FillColor = Color.FromArgb(245, 248, 252);
            };

            boton.MouseLeave += delegate
            {
                boton.FillColor = Color.FromArgb(250, 251, 253);
            };

            return boton;
        }

        private void CrearLogoEmpresa()
        {
            PictureBox pbLogoEmpresa = new PictureBox();
            pbLogoEmpresa.Image = PV.Properties.Resources.Logo_PTIA;
            pbLogoEmpresa.Size = new Size(115, 55);
            pbLogoEmpresa.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogoEmpresa.BackColor = Color.Transparent;
            pbLogoEmpresa.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            pbLogoEmpresa.Location = new Point(
                pnlDashboard.ClientSize.Width - pbLogoEmpresa.Width - 24,
                pnlDashboard.ClientSize.Height - pbLogoEmpresa.Height - 18);

            pnlDashboard.Controls.Add(pbLogoEmpresa);
            pbLogoEmpresa.BringToFront();

            pnlDashboard.Resize += delegate
            {
                pbLogoEmpresa.Location = new Point(
                    pnlDashboard.ClientSize.Width - pbLogoEmpresa.Width - 24,
                    pnlDashboard.ClientSize.Height - pbLogoEmpresa.Height - 18);
            };
        }

        private void ModuloPrincipal_Expandido(object sender, EventArgs e)
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo = control as SidebarMenuItem;

                if (modulo != null && !object.ReferenceEquals(modulo, sender))
                    modulo.Expandir(false);
            }
        }

        private void LimpiarSeleccionMenu()
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo = control as SidebarMenuItem;

                if (modulo != null)
                    modulo.LimpiarSeleccion();
            }

            opcionSeleccionadaActual = null;

            flpMenu.PerformLayout();
            flpMenu.Invalidate(true);
            flpMenu.Refresh();
        }

        private void CrearMenu()
        {
            SidebarMenuItem parametros = new SidebarMenuItem();
            parametros.Titulo = "PARÁMETROS";
            parametros.Icono = PV.Properties.Resources.filtrar;
            parametros.Margin = Padding.Empty;
            parametros.AgregarSubMenu("Datos de Empresa", "DATOS_EMPRESA");
            parametros.AgregarSubMenu("Usuarios", "USUARIOS");
            parametros.OpcionSeleccionada += Menu_OpcionSeleccionada;
            parametros.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            SidebarMenuItem catalogos = new SidebarMenuItem();
            catalogos.Titulo = "CATÁLOGOS";
            catalogos.Icono = PV.Properties.Resources.folder;
            catalogos.Margin = Padding.Empty;
            catalogos.AgregarSubMenu("Divisas", "DIVISAS");
            catalogos.AgregarSubMenu("Almacenes", "ALMACENES");
            catalogos.AgregarSubMenu("Categorías y Familias", "CATEGORIAS_FAMILIAS");
            catalogos.AgregarSubMenu("Productos", "PRODUCTOS");
            catalogos.AgregarSubMenu("Servicios", "SERVICIOS");
            catalogos.AgregarSubMenu("Centros de Costos", "CENTROS_COSTOS");
            catalogos.AgregarSubMenu("Documentos", "DOCUMENTOS");
            catalogos.AgregarSubMenu("Conceptos Globales", "CONCEPTOS_GLOBALES");
            catalogos.AgregarSubMenu("Formas de Pago", "FORMAS_PAGO");
            catalogos.AgregarSubMenu("Empleados", "EMPLEADOS");
            catalogos.AgregarSubMenu("Tipos y Zonas", "TIPOS_ZONAS");
            catalogos.AgregarSubMenu("Clientes", "CLIENTES");
            catalogos.AgregarSubMenu("Proveedores", "PROVEEDORES");
            catalogos.AgregarSubMenu("Cuentas Bancarias", "CUENTAS_BANCARIAS");
            catalogos.OpcionSeleccionada += Menu_OpcionSeleccionada;
            catalogos.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            moduloInventarios = new SidebarMenuItem();
            SidebarMenuItem inventarios = moduloInventarios;
            inventarios.Titulo = "INVENTARIOS";
            inventarios.Icono = PV.Properties.Resources.lista_de_verificacion;
            inventarios.Margin = Padding.Empty;

            SidebarMenuItem movimientos = inventarios.AgregarSubMenu("Movimientos");
            movimientos.AgregarSubMenu("Tipo Movimientos", "TIPO_MOVIMIENTOS");
            movimientos.AgregarSubMenu("Registrar Entradas", "REGISTRAR_ENTRADAS");
            movimientos.AgregarSubMenu("Registrar Salidas", "REGISTRAR_SALIDAS");
            movimientos.AgregarSubMenu("Registrar Traspasos", "REGISTRAR_TRASPASOS");
            movimientos.AgregarSubMenu("Consulta Inventarios", "CONSULTA_INVENTARIOS");

            SidebarMenuItem reportesMovimientos = movimientos.AgregarSubMenu("Reportes");
            reportesMovimientos.AgregarSubMenu("Reporte existencias por almacén", "REPORTE_EXISTENCIAS_ALMACEN");
            reportesMovimientos.AgregarSubMenu("Reporte costo por producto", "REPORTE_COSTO_PRODUCTO");

            inventarios.AgregarSubMenu("Inventarios Físicos");
            inventarios.AgregarSubMenu("Explosión de Material");
            inventarios.AgregarSubMenu("Reportes Inventarios");

            inventarios.OpcionSeleccionada += Menu_OpcionSeleccionada;
            inventarios.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            moduloCompras = new SidebarMenuItem();
            SidebarMenuItem compras = moduloCompras;
            compras.Titulo = "COMPRAS";
            compras.Icono = PV.Properties.Resources.carrito_de_compras;
            compras.Margin = Padding.Empty;

            compras.AgregarSubMenu("Requisiciones", "REQUISICIONES");
            compras.AgregarSubMenu("Cotizaciones");
            compras.AgregarSubMenu("Pedidos Proveedores", "PEDIDOS_PROVEEDORES");
            compras.AgregarSubMenu("Compras Gastos", "COMPRAS_GASTOS");
            compras.AgregarSubMenu("Compras Vía Reembolso", "COMPRAS_REEMBOLSO");
            compras.AgregarSubMenu("Compras Inventariables", "COMPRAS_INVENTARIABLES");

            SidebarMenuItem reportesCompras = compras.AgregarSubMenu("Reportes");
            reportesCompras.AgregarSubMenu("Diario de requisiciones", "DIARIO_REQUISICIONES");
            reportesCompras.AgregarSubMenu("Diario órdenes de compra", "DIARIO_ORDENES_COMPRA");
            reportesCompras.AgregarSubMenu("Diario de compras inventariables", "DIARIO_COMPRAS_INVENTARIABLES");
            reportesCompras.AgregarSubMenu("Diario de reembolsos", "DIARIO_REEMBOLSOS");
            reportesCompras.AgregarSubMenu("Diario de gastos", "DIARIO_GASTOS");
            reportesCompras.AgregarSubMenu("Saldo de compras", "SALDO_COMPRAS");

            SidebarMenuItem anticiposCompras = reportesCompras.AgregarSubMenu("Anticipos");
            anticiposCompras.AgregarSubMenu("Anticipos Aplicados");

            reportesCompras.AgregarSubMenu("Proveedores");
            compras.AgregarSubMenu("Gráficas");

            compras.OpcionSeleccionada += Menu_OpcionSeleccionada;
            compras.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            moduloVentas = new SidebarMenuItem();
            SidebarMenuItem ventas = moduloVentas;
            ventas.Titulo = "VENTAS";
            ventas.Icono = PV.Properties.Resources.grafico_de_barras;
            ventas.Margin = Padding.Empty;

            ventas.AgregarSubMenu("Pedidos Clientes", "PEDIDOS_CLIENTES");
            ventas.AgregarSubMenu("Remisiones", "REMISIONES");
            ventas.AgregarSubMenu("Factura (CFDI)", "FACTURAS");
            ventas.AgregarSubMenu("Notas Crédito");
            ventas.AgregarSubMenu("Notas Cargo");

            SidebarMenuItem reportesVentas = ventas.AgregarSubMenu("Reportes");
            reportesVentas.AgregarSubMenu("Reporte diario de Pedidos Clientes");
            reportesVentas.AgregarSubMenu("Reporte diario de Remisiones", "REPORTE_DIARIO_REMISIONES");
            reportesVentas.AgregarSubMenu("Reporte diario Facturas", "REPORTE_DIARIO_FACTURAS");
            reportesVentas.AgregarSubMenu("Reporte diario Notas Crédito");
            reportesVentas.AgregarSubMenu("Reporte diario Notas Cargo");
            reportesVentas.AgregarSubMenu("Reporte Analítico Pedidos");
            reportesVentas.AgregarSubMenu("Reporte Analítico ventas");

            SidebarMenuItem graficasVentas = ventas.AgregarSubMenu("Gráficas");
            graficasVentas.AgregarSubMenu("Gráfica de Ventas");
            graficasVentas.AgregarSubMenu("Gráfica de Productos");
            graficasVentas.AgregarSubMenu("Gráfica de Clientes");

            SidebarMenuItem polizasVentas = ventas.AgregarSubMenu("Pólizas");
            polizasVentas.AgregarSubMenu("Definir Pólizas");
            polizasVentas.AgregarSubMenu("Generar Pólizas");

            ventas.OpcionSeleccionada += Menu_OpcionSeleccionada;
            ventas.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            moduloTesoreria = new SidebarMenuItem();
            SidebarMenuItem tesoreria = moduloTesoreria;
            tesoreria.Titulo = "TESORERÍA";
            tesoreria.Icono = PV.Properties.Resources.banco;
            tesoreria.Margin = Padding.Empty;

            SidebarMenuItem bancos = tesoreria.AgregarSubMenu("Bancos");
            bancos.AgregarSubMenu("Catalogo Conceptos Cobro Pago", "CONCEPTOS_COBRO_PAGO");
            bancos.AgregarSubMenu("Registra Movimientos a Bancos", "REGISTRAR_MOVIMIENTOS_BANCOS");
            bancos.AgregarSubMenu("Importar Edo Cuenta");

            SidebarMenuItem cxc = tesoreria.AgregarSubMenu("CxC - Ingresos");
            cxc.AgregarSubMenu("Registra Ingresos", "REGISTRAR_INGRESO");
            cxc.AgregarSubMenu("Registra Anticipo Cliente", "REGISTRAR_ANTICIPO_CLIENTE");
            cxc.AgregarSubMenu("Aplicar Anticipos Clientes", "APLICAR_ANTICIPO_CLIENTE");
            cxc.AgregarSubMenu("Registrar Préstamos");

            SidebarMenuItem reportesCxc = cxc.AgregarSubMenu("Reportes CxC Ingresos");
            reportesCxc.AgregarSubMenu("Diario de Ingresos", "REPORTE_DIARIO_INGRESOS");
            reportesCxc.AgregarSubMenu("Reporte de Ingresos");
            reportesCxc.AgregarSubMenu("Reporte de Anticipos");
            reportesCxc.AgregarSubMenu("Saldos de Clientes");
            reportesCxc.AgregarSubMenu("Estado de Cuenta Cliente");

            SidebarMenuItem cxp = tesoreria.AgregarSubMenu("CxP - Egresos");
            cxp.AgregarSubMenu("Pagos por Vencimiento", "PAGOS_VENCIMIENTO");
            cxp.AgregarSubMenu("Pagos por Proveedor", "PAGOS_PROVEEDOR");
            cxp.AgregarSubMenu("Registrar Anticipo Proveedor");
            cxp.AgregarSubMenu("Aplicar Anticipo Proveedor");

            SidebarMenuItem reportesCxp = cxp.AgregarSubMenu("Reportes CxP Egresos");
            reportesCxp.AgregarSubMenu("Reporte Diario de Egresos", "REPORTE_DIARIO_EGRESOS");
            reportesCxp.AgregarSubMenu("Saldos Proveedores");
            reportesCxp.AgregarSubMenu("Estado de Cuenta Proveedor");

            SidebarMenuItem polizasTesoreria = tesoreria.AgregarSubMenu("Pólizas Contables");
            polizasTesoreria.AgregarSubMenu("Definir Pólizas");
            polizasTesoreria.AgregarSubMenu("Generar Pólizas");

            SidebarMenuItem finanzas = tesoreria.AgregarSubMenu("Finanzas");
            finanzas.AgregarSubMenu("Reporte Resultados Global", "REPORTE_RESULTADOS_GLOBAL");
            finanzas.AgregarSubMenu("Flujo de Efectivo");
            finanzas.AgregarSubMenu("Posición Financiera");

            tesoreria.OpcionSeleccionada += Menu_OpcionSeleccionada;
            tesoreria.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            SidebarMenuItem utilerias = new SidebarMenuItem();
            utilerias.Titulo = "UTILERÍAS";
            utilerias.Icono = PV.Properties.Resources.renovacion;
            utilerias.Margin = Padding.Empty;
            utilerias.AgregarSubMenu("Contabilidad");
            utilerias.AgregarSubMenu("ODBC");
            utilerias.AgregarSubMenu("Correo Electrónico");
            utilerias.OpcionSeleccionada += Menu_OpcionSeleccionada;
            utilerias.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            SidebarMenuItem presupuesto = new SidebarMenuItem();
            presupuesto.Titulo = "PRESUPUESTO";
            presupuesto.Icono = PV.Properties.Resources.presupuesto;
            presupuesto.Margin = Padding.Empty;
            presupuesto.AgregarSubMenu("Periodos", "PERIODOS_PRESUPUESTO");
            presupuesto.AgregarSubMenu("Conceptos", "CONCEPTOS_PRESUPUESTO");
            presupuesto.AgregarSubMenu("Registrar Presupuestos");
            presupuesto.AgregarSubMenu("Crear Presupuestos");
            presupuesto.AgregarSubMenu("Cerrar Presupuestos", "CERRAR_PRESUPUESTOS");
            presupuesto.AgregarSubMenu("Reportes");
            presupuesto.OpcionSeleccionada += Menu_OpcionSeleccionada;
            presupuesto.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            flpMenu.Controls.Add(parametros);
            flpMenu.Controls.Add(catalogos);
            flpMenu.Controls.Add(inventarios);
            flpMenu.Controls.Add(compras);
            flpMenu.Controls.Add(ventas);
            flpMenu.Controls.Add(tesoreria);
            flpMenu.Controls.Add(utilerias);
            flpMenu.Controls.Add(presupuesto);

            favoritosMenu = new FavoritosMenu();
            favoritosMenu.Margin = new Padding(0, 12, 0, 0);
            favoritosMenu.Width = flpMenu.ClientSize.Width;

            flpMenu.Controls.Add(favoritosMenu);

            parametros.RegistrarFavoritos(favoritosMenu);
            catalogos.RegistrarFavoritos(favoritosMenu);
            inventarios.RegistrarFavoritos(favoritosMenu);
            compras.RegistrarFavoritos(favoritosMenu);
            ventas.RegistrarFavoritos(favoritosMenu);
            tesoreria.RegistrarFavoritos(favoritosMenu);
            utilerias.RegistrarFavoritos(favoritosMenu);
            presupuesto.RegistrarFavoritos(favoritosMenu);

            AjustarAnchoMenu();

            favoritosMenu.UsuarioActual = DBLogin.usuario;
            favoritosMenu.CargarFavoritosUsuario();
        }

        private void flpMenu_SizeChanged(object sender, EventArgs e)
        {
            AjustarAnchoMenu();
        }

        private void AjustarAnchoMenu()
        {
            int anchoDisponible = flpMenu.Width - flpMenu.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth;

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem opcion = control as SidebarMenuItem;

                if (opcion != null)
                    opcion.Width = Math.Max(0, anchoDisponible - opcion.Margin.Horizontal);

                FavoritosMenu favoritos = control as FavoritosMenu;

                if (favoritos != null)
                    favoritos.Width = Math.Max(0, anchoDisponible - favoritos.Margin.Horizontal);
            }
        }

        private void Menu_OpcionSeleccionada(object sender, OpcionMenuSeleccionadaEventArgs e)
        {
            LimpiarSeleccionMenu();

            opcionSeleccionadaActual = e.Opcion;
            opcionSeleccionadaActual.Seleccionado = true;

            switch (e.Opcion.Clave)
            {
                case "DATOS_EMPRESA":
                    AbrirFormulario(new DatosEmpresas());
                    break;
                case "USUARIOS":
                    AbrirFormulario(new Usuarios());
                    break;
                case "DIVISAS":
                    AbrirFormulario(new CatalogoDivisa());
                    break;
                case "ALMACENES":
                    AbrirFormulario(new Almacenes());
                    break;
                case "CATEGORIAS_FAMILIAS":
                    AbrirFormulario(new CatalogoFamilias());
                    break;
                case "PRODUCTOS":
                    AbrirFormulario(new CatalogoProductosServicios(0));
                    break;
                case "SERVICIOS":
                    AbrirFormulario(new CatalogoServicios(0));
                    break;
                case "CENTROS_COSTOS":
                    AbrirFormulario(new CentroCostos());
                    break;
                case "DOCUMENTOS":
                    AbrirFormulario(new Documentos());
                    break;
                case "CONCEPTOS_GLOBALES":
                    AbrirFormulario(new ConceptosGlobales());
                    break;
                case "FORMAS_PAGO":
                    AbrirFormulario(new CatalogoFormasPago());
                    break;
                case "EMPLEADOS":
                    AbrirFormulario(new CatalogoPersonal());
                    break;
                case "TIPOS_ZONAS":
                    AbrirFormulario(new TiposZonas());
                    break;
                case "CLIENTES":
                    AbrirFormulario(new Clientes());
                    break;
                case "PROVEEDORES":
                    AbrirFormulario(new Proveedores());
                    break;
                case "CUENTAS_BANCARIAS":
                    AbrirFormulario(new CuentasBancarias());
                    break;
                case "TIPO_MOVIMIENTOS":
                    AbrirFormulario(new TipoMovimientos());
                    break;
                case "REGISTRAR_ENTRADAS":
                    AbrirFormulario(new RegistrarEntrada2("E"));
                    break;
                case "REGISTRAR_SALIDAS":
                    AbrirFormulario(new RegistrarEntrada2("S"));
                    break;
                case "REGISTRAR_TRASPASOS":
                    AbrirFormulario(new RegistrarEntrada2("T"));
                    break;
                case "CONSULTA_INVENTARIOS":
                    AbrirFormulario(new ConsultaInventario2(0));
                    break;
                case "REPORTE_EXISTENCIAS_ALMACEN":
                    AbrirFormulario(new FiltroExistencias());
                    break;
                case "REPORTE_COSTO_PRODUCTO":
                    AbrirFormulario(new FiltroValorProducto());
                    break;
                case "REQUISICIONES":
                    AbrirFormulario(new Requisicion2());
                    break;
                case "PEDIDOS_PROVEEDORES":
                    AbrirFormulario(new OrdenCompra2());
                    break;
                case "COMPRAS_GASTOS":
                    AbrirFormulario(new RegistroGastos2());
                    break;
                case "COMPRAS_REEMBOLSO":
                    AbrirFormulario(new RegistroReembolsos());
                    break;
                case "COMPRAS_INVENTARIABLES":
                    AbrirFormulario(new RecepcionProductos2());
                    break;
                case "DIARIO_REQUISICIONES":
                    AbrirFormulario(new ReporteDiarioRequisicionFiltro());
                    break;
                case "DIARIO_ORDENES_COMPRA":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Ordenes Compras"));
                    break;
                case "DIARIO_COMPRAS_INVENTARIABLES":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Compras"));
                    break;
                case "DIARIO_REEMBOLSOS":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Reembolsos"));
                    break;
                case "DIARIO_GASTOS":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Gastos"));
                    break;
                case "EGRESOS_COMPRAS":
                    AbrirFormulario(new ReporteEgresosFiltro());
                    break;
                case "REPORTE_DIARIO_INGRESOS":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Ingresos"));
                    break;
                case "SALDO_COMPRAS":
                    AbrirFormulario(new ReporteComprasFiltro());
                    break;
                case "ANTICIPOS_COMPRAS_REGISTRO":
                    AbrirFormulario(new ReporteAnticipoProveedorFiltro());
                    break;
                case "ESTADO_CUENTA_PROVEEDORES":
                    AbrirFormulario(new ReporteEstadoCuentaProveedor());
                    break;
                case "PEDIDOS_CLIENTES":
                    AbrirFormulario(new OrdenPedidoCliente("Pedido"));
                    break;
                case "REMISIONES":
                    AbrirFormulario(new OrdenPedidoCliente("Remision"));
                    break;
                case "REPORTE_DIARIO_REMISIONES":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Remisiones"));
                    break;
                case "REPORTE_UTILIDAD_PEDIDO":
                    AbrirFormulario(new FiltroFecha("Utilidad Pedido"));
                    break;
                case "REPORTE_UTILIDAD_PRODUCTO":
                    AbrirFormulario(new FiltroFecha("Utilidad Producto"));
                    break;
                case "FACTURAS":
                    AbrirFormulario(new Facturas());
                    break;
                case "REPORTE_DIARIO_FACTURAS":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Facturas"));
                    break;
                case "REGISTRAR_MOVIMIENTOS_BANCOS":
                    AbrirFormulario(new RegistroMovimientoBancos());
                    break;
                case "CONCEPTOS_COBRO_PAGO":
                    AbrirFormulario(new CatalogoConceptosTesoreria());
                    break;
                case "REGISTRAR_INGRESO":
                    AbrirFormulario(new registroIngresos("Remision", ""));
                    break;
                case "REGISTRAR_ANTICIPO_CLIENTE":
                    AbrirFormulario(new RegistrarAnticipo("Propietario"));
                    break;
                case "APLICAR_ANTICIPO_CLIENTE":
                    AbrirFormulario(new AplicarAnticipo());
                    break;
                case "PAGOS_PROVEEDOR":
                    AbrirFormulario(new RegistroEgreso());
                    break;
                case "PAGOS_VENCIMIENTO":
                    AbrirFormulario(new ConsultarEgreso());
                    break;
                case "REPORTE_INGRESOS_TESORERIA":
                    AbrirFormulario(new ReporteIngresoFormulario());
                    break;
                case "REPORTE_ANTICIPO_TESORERIA":
                    AbrirFormulario(new ReporteAnticiposFiltro());
                    break;
                case "REPORTE_DIARIO_EGRESOS":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Diario Egresos"));
                    break;
                case "SALDOS_PROVEEDORES_TESORERIA":
                    AbrirFormulario(new ReporteDiarioComprasFiltro("Saldos Proveedor"));
                    break;
                case "ESTADO_CUENTA_PROVEEDOR_TESORERIA":
                    AbrirFormulario(new ReporteEstadoCuentaProveedor());
                    break;
                case "DEFINIR_POLIZAS_EGRESOS":
                    AbrirFormulario(new DefinePolizas("Definiciones Compras"));
                    break;
                case "GENERAR_POLIZAS_EGRESOS":
                    AbrirFormulario(new GENERARPOLIZAS("Polizas Compras"));
                    break;
                case "REPORTE_RESULTADOS_GLOBAL":
                    AbrirFormulario(new FiltrarReporteResultadosGlobal());
                    break;
                case "PERIODOS_PRESUPUESTO":
                    AbrirFormulario(new CatalogoPeriodos());
                    break;
                case "CONCEPTOS_PRESUPUESTO":
                    AbrirFormulario(new ConceptosPresupuesto());
                    break;
                case "CERRAR_PRESUPUESTOS":
                    AbrirFormulario(new CerrarPresupuesto());
                    break;
                default:
                    LimpiarSeleccionMenu();
                    break;
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            if (formulario == null)
                return;

            formulario.StartPosition = FormStartPosition.CenterParent;

            try
            {
                formulario.ShowDialog(this);
            }
            finally
            {
                formulario.Dispose();
                LimpiarSeleccionMenu();
            }
        }

        private void pnlBarraVentana_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }
    }
}