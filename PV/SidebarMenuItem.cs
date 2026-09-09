using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace PV
{
    public partial class SidebarMenuItem : UserControl
    {
        private readonly List<SidebarMenuItem> subMenus = new List<SidebarMenuItem>();
        private bool expandido;
        private bool organizando;
        private string titulo = "Nuevo elemento";
        private string clave = "";
        private int nivel = 0;
        private bool seleccionado;
        private bool puedeSerFavorito = true;
        private bool esFavorito = false;
        private Guna2Button btnFavorito;
        private bool ajustarDpi = true;

        private const float FuenteModuloBase = 10.5F;
        private const float FuenteSubMenuBase = 9.5F;
        private const float FuenteSubSubMenuBase = 9F;

        private const int AltoModuloBase = 46;
        private const int AltoSubMenuBase = 34;
        private const int AltoSubMenuDosLineasBase = 48;
        private const int AltoSubSubMenuBase = 32;
        private const int AltoSubSubMenuDosLineasBase = 48;

        private const int LimiteCaracteresDosLineas = 26;

        private static readonly Color ColorModulo = Color.FromArgb(57, 101, 145);
        private static readonly Color ColorModuloExpandido = Color.FromArgb(45, 91, 137);
        private static readonly Color ColorModuloHover = Color.FromArgb(66, 113, 159);
        private static readonly Color ColorModuloExpandidoHover = Color.FromArgb(52, 103, 153);

        private static readonly Color ColorSubMenu = Color.FromArgb(35, 58, 88);
        private static readonly Color ColorSubMenuHover = Color.FromArgb(50, 79, 112);
        private static readonly Color ColorTextoSubMenu = Color.FromArgb(238, 244, 250);

        private static readonly Color ColorSubSubMenu = Color.FromArgb(43, 69, 101);
        private static readonly Color ColorSubSubMenuHover = Color.FromArgb(58, 88, 121);
        private static readonly Color ColorTextoSubSubMenu = Color.FromArgb(205, 220, 235);

        private static readonly Color ColorSeleccionado = Color.FromArgb(57, 101, 145);
        private static readonly Color ColorSeleccionadoHover = Color.FromArgb(66, 115, 161);

        private static readonly Color ColorTextoDeshabilitado = Color.FromArgb(115, 137, 160);

        public event EventHandler<OpcionMenuSeleccionadaEventArgs> OpcionSeleccionada;
        public event EventHandler ModuloPrincipalExpandido;
        public event EventHandler FavoritoCambiado;
        public event EventHandler FavoritoSolicitado;

        public SidebarMenuItem()
        {
            InitializeComponent();

            btnHeader.Click += btnHeader_Click;
            SizeChanged += SidebarMenuItem_SizeChanged;
            btnHeader.AutoSize = false;

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        public void Seleccionar()
        {
            Seleccionado = true;

            if (OpcionSeleccionada != null)
                OpcionSeleccionada(this, new OpcionMenuSeleccionadaEventArgs(this));
        }

        public void Deseleccionar()
        {
            Seleccionado = false;
        }

        public void LimpiarSeleccion()
        {
            Seleccionado = false;

            foreach (SidebarMenuItem subMenu in subMenus)
                subMenu.LimpiarSeleccion();

            ActualizarTextoEncabezado();
            OrganizarBotonFavorito();
            ActualizarColorBotonFavorito();

            if (btnFavorito != null)
            {
                btnFavorito.Invalidate();
                btnFavorito.Refresh();
            }

            Invalidate();
        }

        public void EjecutarOpcion()
        {
            if (string.IsNullOrWhiteSpace(Clave))
                return;

            if (OpcionSeleccionada != null)
                OpcionSeleccionada(this, new OpcionMenuSeleccionadaEventArgs(this));
        }

        [Category("Favoritos")]
        [DefaultValue(true)]
        public bool PuedeSerFavorito
        {
            get { return puedeSerFavorito; }
            set
            {
                puedeSerFavorito = value;

                if (!puedeSerFavorito)
                    EsFavorito = false;

                ActualizarTextoEncabezado();
                OrganizarSubMenus();
            }
        }

        [Category("Favoritos")]
        [DefaultValue(false)]
        public bool EsFavorito
        {
            get { return esFavorito; }
            set
            {
                if (esFavorito == value)
                    return;

                if (string.IsNullOrWhiteSpace(Clave))
                    value = false;

                esFavorito = value;

                ActualizarTextoEncabezado();
                ActualizarColorBotonFavorito();

                if (FavoritoCambiado != null)
                    FavoritoCambiado(this, EventArgs.Empty);
            }
        }

        public void EstablecerFavoritoInicial(bool favorito)
        {
            if (!PuedeSerFavorito || TieneSubMenus || string.IsNullOrWhiteSpace(Clave))
                return;

            esFavorito = favorito;

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        private void CrearBotonFavorito()
        {
            if (btnFavorito != null)
                return;

            btnFavorito = new Guna2Button();
            btnFavorito.Name = "btnFavorito";
            btnFavorito.Text = "☆";
            btnFavorito.Font = new Font("Segoe UI Symbol", 11F, FontStyle.Regular);
            btnFavorito.ForeColor = ColorTextoSubMenu;
            btnFavorito.FillColor = Color.Transparent;
            btnFavorito.BorderThickness = 0;
            btnFavorito.BorderRadius = 0;
            btnFavorito.HoverState.FillColor = Color.Transparent;
            btnFavorito.HoverState.ForeColor = Color.White;
            btnFavorito.PressedColor = Color.Transparent;
            btnFavorito.DisabledState.FillColor = Color.Transparent;
            btnFavorito.DisabledState.ForeColor = ColorTextoDeshabilitado;
            btnFavorito.TextAlign = HorizontalAlignment.Center;
            btnFavorito.Cursor = Cursors.Hand;
            btnFavorito.TabStop = false;
            btnFavorito.Click += btnFavorito_Click;

            Controls.Add(btnFavorito);
            btnFavorito.BringToFront();
        }

        private void btnFavorito_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Clave))
                return;

            if (FavoritoSolicitado != null)
                FavoritoSolicitado(this, EventArgs.Empty);
        }

        private void OrganizarBotonFavorito()
        {
            if (btnFavorito == null)
                return;

            if (nivel == 0 || !PuedeSerFavorito || TieneSubMenus)
            {
                btnFavorito.Visible = false;
                btnHeader.Width = Width;
                return;
            }

            const int anchoFavorito = 34;

            btnFavorito.Visible = true;
            btnFavorito.Enabled = !string.IsNullOrWhiteSpace(Clave);
            btnFavorito.Width = anchoFavorito;
            btnFavorito.Height = btnHeader.Height;
            btnFavorito.Left = Width - anchoFavorito;
            btnFavorito.Top = 0;

            btnHeader.Width = Math.Max(1, Width - anchoFavorito);

            btnFavorito.BringToFront();
        }

        private void ActualizarColorBotonFavorito()
        {
            if (btnFavorito == null || !btnFavorito.Visible)
                return;

            btnFavorito.FillColor = Color.Transparent;
            btnFavorito.HoverState.FillColor = Color.Transparent;
            btnFavorito.PressedColor = Color.Transparent;
            btnFavorito.DisabledState.FillColor = Color.Transparent;
            btnFavorito.DisabledState.ForeColor = ColorTextoDeshabilitado;
            btnFavorito.ForeColor = EsFavorito ? Color.White : ColorTextoSubMenu;
            btnFavorito.HoverState.ForeColor = Color.White;
            btnFavorito.Invalidate();
        }

        private void ConfigurarFavorito()
        {
            if (PuedeSerFavorito && !TieneSubMenus)
            {
                CrearBotonFavorito();

                btnFavorito.Text = EsFavorito ? "★" : "☆";
                btnFavorito.Visible = true;
                btnFavorito.Enabled = !string.IsNullOrWhiteSpace(Clave);
            }
            else
            {
                if (btnFavorito != null)
                    btnFavorito.Visible = false;
            }
        }

        private void ActualizarEstadoHabilitado()
        {
            bool tieneClave = !string.IsNullOrWhiteSpace(Clave);

            if (nivel == 0 || TieneSubMenus)
                btnHeader.Enabled = true;
            else
                btnHeader.Enabled = tieneClave;

            if (btnFavorito != null)
                btnFavorito.Enabled = tieneClave && PuedeSerFavorito && !TieneSubMenus;
        }

        [Category("Menu")]
        [DefaultValue(true)]
        public bool AjustarDpi
        {
            get { return ajustarDpi; }
            set
            {
                if (ajustarDpi == value)
                    return;

                ajustarDpi = value;

                ActualizarTextoEncabezado();
                OrganizarSubMenus();
            }
        }

        private float ObtenerEscalaDpi()
        {
            if (!AjustarDpi)
                return 1F;

            return DeviceDpi / 96F;
        }

        private float ObtenerFuenteModulo()
        {
            return FuenteModuloBase * Math.Min(ObtenerEscalaDpi(), 1.35F);
        }

        private float ObtenerFuenteSubMenu()
        {
            return FuenteSubMenuBase * Math.Min(ObtenerEscalaDpi(), 1.35F);
        }

        private float ObtenerFuenteSubSubMenu()
        {
            return FuenteSubSubMenuBase * Math.Min(ObtenerEscalaDpi(), 1.35F);
        }

        private int ObtenerAltoModulo()
        {
            return (int)Math.Round(AltoModuloBase * Math.Min(ObtenerEscalaDpi(), 1.35F));
        }

        private int ObtenerAltoSubMenu()
        {
            return (int)Math.Round(AltoSubMenuBase * Math.Min(ObtenerEscalaDpi(), 1.35F));
        }

        private int ObtenerAltoSubMenuDosLineas()
        {
            return (int)Math.Round(AltoSubMenuDosLineasBase * Math.Min(ObtenerEscalaDpi(), 1.35F));
        }

        private int ObtenerAltoSubSubMenu()
        {
            return (int)Math.Round(AltoSubSubMenuBase * Math.Min(ObtenerEscalaDpi(), 1.35F));
        }

        private int ObtenerAltoSubSubMenuDosLineas()
        {
            return (int)Math.Round(AltoSubSubMenuDosLineasBase * Math.Min(ObtenerEscalaDpi(), 1.35F));
        }

        [Category("Menu")]
        [DefaultValue(false)]
        public bool Seleccionado
        {
            get { return seleccionado; }
            set
            {
                if (seleccionado == value)
                    return;

                seleccionado = value;

                ActualizarTextoEncabezado();
                OrganizarBotonFavorito();
                ActualizarColorBotonFavorito();

                btnHeader.Invalidate();

                if (btnFavorito != null)
                {
                    btnFavorito.Invalidate();
                    btnFavorito.Refresh();
                }

                Invalidate();
            }
        }

        [Browsable(false)]
        public int Nivel
        {
            get { return nivel; }
        }

        [Browsable(false)]
        public List<SidebarMenuItem> SubMenus
        {
            get { return subMenus; }
        }

        [Category("Menu")]
        public Image Icono
        {
            get { return btnHeader.Image; }
            set
            {
                btnHeader.Image = value;
                ActualizarTextoEncabezado();
            }
        }

        [Category("Menu")]
        [DefaultValue("Nuevo elemento")]
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value ?? "";
                ActualizarTextoEncabezado();
            }
        }

        [Category("Menu")]
        [DefaultValue("")]
        public string Clave
        {
            get { return clave; }
            set
            {
                clave = (value ?? "").Trim();

                if (string.IsNullOrWhiteSpace(clave))
                    esFavorito = false;

                ActualizarTextoEncabezado();
                OrganizarSubMenus();
            }
        }

        [Browsable(false)]
        public bool TieneSubMenus
        {
            get { return subMenus.Count > 0; }
        }

        [Category("Menu")]
        [DefaultValue(false)]
        public bool Expandido
        {
            get { return expandido; }
            set
            {
                if (!TieneSubMenus)
                    value = false;

                if (!value)
                {
                    LimpiarSeleccion();

                    foreach (SidebarMenuItem subMenu in subMenus)
                        subMenu.Expandir(false);
                }

                if (expandido == value)
                    return;

                expandido = value;

                ActualizarTextoEncabezado();
                OrganizarSubMenus();
            }
        }

        public void Expandir(bool expandir = true)
        {
            Expandido = expandir;
        }

        public SidebarMenuItem AgregarSubMenu(string tituloSubMenu, string claveSubMenu = "")
        {
            SidebarMenuItem subMenu = new SidebarMenuItem();

            subMenu.Titulo = tituloSubMenu;
            subMenu.Clave = claveSubMenu;
            subMenu.AjustarDpi = AjustarDpi;

            AgregarSubMenu(subMenu);

            return subMenu;
        }

        public void AgregarSubMenu(SidebarMenuItem subMenu)
        {
            if (subMenu == null)
                throw new ArgumentNullException("subMenu");

            subMenus.Add(subMenu);
            pnlSubMenu.Controls.Add(subMenu);

            EsFavorito = false;

            subMenu.EstablecerNivel(nivel + 1);
            subMenu.AjustarDpi = AjustarDpi;
            subMenu.OpcionSeleccionada += SubMenu_OpcionSeleccionada;

            subMenu.SizeChanged += delegate
            {
                OrganizarSubMenus();
            };

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        public void RegistrarFavoritos(FavoritosMenu favoritosMenu)
        {
            if (favoritosMenu == null)
                return;

            foreach (SidebarMenuItem subMenu in subMenus)
            {
                if (subMenu.TieneSubMenus)
                {
                    subMenu.RegistrarFavoritos(favoritosMenu);
                }
                else if (subMenu.PuedeSerFavorito && !string.IsNullOrWhiteSpace(subMenu.Clave))
                {
                    favoritosMenu.RegistrarOpcion(subMenu);
                }
            }
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            if (TieneSubMenus)
            {
                if (!Expandido && nivel == 0 && ModuloPrincipalExpandido != null)
                    ModuloPrincipalExpandido(this, EventArgs.Empty);

                Expandido = !Expandido;
            }

            EjecutarOpcion();
        }

        private void SubMenu_OpcionSeleccionada(object sender, OpcionMenuSeleccionadaEventArgs e)
        {
            if (OpcionSeleccionada != null)
                OpcionSeleccionada(this, e);
        }

        private void SidebarMenuItem_SizeChanged(object sender, EventArgs e)
        {
            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        private void EstablecerNivel(int nuevoNivel)
        {
            nivel = nuevoNivel;

            if (nivel == 0)
                Margin = new Padding(0, 0, 0, 4);
            else
                Margin = Padding.Empty;

            foreach (SidebarMenuItem subMenu in subMenus)
                subMenu.EstablecerNivel(nivel + 1);

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        private void ActualizarTextoEncabezado()
        {
            if (btnHeader == null)
                return;

            if (nivel == 0)
                ConfigurarModuloPrincipal();
            else if (nivel == 1)
                ConfigurarSubMenuNivel1();
            else
                ConfigurarSubMenuNivel2();

            ActualizarEstadoHabilitado();
        }

        private void ConfigurarModuloPrincipal()
        {
            Margin = new Padding(0, 0, 0, 4);

            string flecha = TieneSubMenus ? (expandido ? "  ▾" : "  ▸") : "";

            btnHeader.Height = ObtenerAltoModulo();
            btnHeader.Width = Math.Max(1, Width);
            btnHeader.Text = titulo + flecha;
            btnHeader.ImageAlign = HorizontalAlignment.Left;
            btnHeader.ImageOffset = new Point(10, 0);
            btnHeader.TextAlign = HorizontalAlignment.Left;
            btnHeader.TextOffset = new Point(5, 0);
            btnHeader.ForeColor = Color.White;
            btnHeader.Font = new Font("Segoe UI", ObtenerFuenteModulo(), FontStyle.Bold);
            btnHeader.BorderRadius = 8;
            btnHeader.BorderThickness = 0;
            btnHeader.Padding = new Padding(16, 0, 12, 0);

            btnHeader.FillColor = expandido ? ColorModuloExpandido : ColorModulo;
            btnHeader.HoverState.FillColor = expandido ? ColorModuloExpandidoHover : ColorModuloHover;
            btnHeader.DisabledState.FillColor = btnHeader.FillColor;
            btnHeader.DisabledState.ForeColor = Color.White;

            if (btnFavorito != null)
                btnFavorito.Visible = false;
        }

        private void ConfigurarSubMenuNivel1()
        {
            Margin = Padding.Empty;

            string flecha = TieneSubMenus ? (expandido ? "  ▾" : "  ▸") : "";

            ConfigurarFavorito();

            int anchoFavorito = TieneBotonFavoritoVisible() ? 34 : 0;

            btnHeader.Width = Math.Max(1, Width - anchoFavorito);
            btnHeader.ImageAlign = HorizontalAlignment.Left;
            btnHeader.ImageOffset = new Point(8, 0);
            btnHeader.TextAlign = HorizontalAlignment.Left;
            btnHeader.TextOffset = Point.Empty;
            btnHeader.Font = new Font("Segoe UI", ObtenerFuenteSubMenu(), FontStyle.Regular);
            btnHeader.BorderRadius = 2;
            btnHeader.BorderThickness = 0;
            btnHeader.Padding = new Padding(22, 0, 6, 0);

            string textoTitulo = LimpiarTexto(titulo);
            string prefijo = seleccionado ? "▌ " : "• ";

            if (textoTitulo.Length > LimiteCaracteresDosLineas)
            {
                btnHeader.Height = ObtenerAltoSubMenuDosLineas();
                btnHeader.Text = prefijo + DividirTextoEnDosLineas(textoTitulo, LimiteCaracteresDosLineas) + flecha;
            }
            else
            {
                btnHeader.Height = ObtenerAltoSubMenu();
                btnHeader.Text = prefijo + textoTitulo + flecha;
            }

            if (seleccionado)
            {
                btnHeader.FillColor = ColorSeleccionado;
                btnHeader.ForeColor = Color.White;
                btnHeader.HoverState.FillColor = ColorSeleccionadoHover;
            }
            else
            {
                btnHeader.FillColor = ColorSubMenu;
                btnHeader.ForeColor = ColorTextoSubMenu;
                btnHeader.HoverState.FillColor = ColorSubMenuHover;
            }

            btnHeader.DisabledState.FillColor = btnHeader.FillColor;
            btnHeader.DisabledState.ForeColor = ColorTextoDeshabilitado;

            OrganizarBotonFavorito();
            ActualizarColorBotonFavorito();
        }

        private void ConfigurarSubMenuNivel2()
        {
            Margin = Padding.Empty;

            string flecha = TieneSubMenus ? (expandido ? "  ▾" : "  ▸") : "";

            ConfigurarFavorito();

            int anchoFavorito = TieneBotonFavoritoVisible() ? 34 : 0;
            int anchoControl = Math.Max(1, Width - anchoFavorito);

            btnHeader.Width = anchoControl;
            btnHeader.ImageAlign = HorizontalAlignment.Left;
            btnHeader.ImageOffset = new Point(4, 0);

            int indentacion = nivel == 2 ? 14 : 20 + ((nivel - 3) * 5);

            btnHeader.Padding = new Padding(indentacion, 0, 4, 0);
            btnHeader.Font = new Font("Segoe UI", ObtenerFuenteSubSubMenu(), FontStyle.Regular);
            btnHeader.TextAlign = HorizontalAlignment.Left;
            btnHeader.TextOffset = Point.Empty;
            btnHeader.BorderRadius = 0;
            btnHeader.BorderThickness = 0;

            string textoTitulo = LimpiarTexto(titulo);
            string prefijo = seleccionado ? "▌ " : "• ";

            if (textoTitulo.Length > LimiteCaracteresDosLineas)
            {
                btnHeader.Height = ObtenerAltoSubSubMenuDosLineas();
                btnHeader.Text = prefijo + DividirTextoEnDosLineas(textoTitulo, LimiteCaracteresDosLineas) + flecha;
            }
            else
            {
                btnHeader.Height = ObtenerAltoSubSubMenu();
                btnHeader.Text = prefijo + textoTitulo + flecha;
            }

            if (seleccionado)
            {
                btnHeader.FillColor = ColorSeleccionado;
                btnHeader.ForeColor = Color.White;
                btnHeader.HoverState.FillColor = ColorSeleccionadoHover;
            }
            else
            {
                btnHeader.FillColor = ColorSubSubMenu;
                btnHeader.ForeColor = ColorTextoSubSubMenu;
                btnHeader.HoverState.FillColor = ColorSubSubMenuHover;
            }

            btnHeader.DisabledState.FillColor = btnHeader.FillColor;
            btnHeader.DisabledState.ForeColor = ColorTextoDeshabilitado;

            OrganizarBotonFavorito();
            ActualizarColorBotonFavorito();
        }

        private string LimpiarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return "";

            return texto.Replace("\r", " ").Replace("\n", " ").Trim();
        }

        private string DividirTextoEnDosLineas(string texto, int limite)
        {
            texto = LimpiarTexto(texto);

            if (texto.Length <= limite)
                return texto;

            int posicionCorte = -1;

            for (int i = Math.Min(limite, texto.Length - 1); i >= 0; i--)
            {
                if (char.IsWhiteSpace(texto[i]))
                {
                    posicionCorte = i;
                    break;
                }
            }

            if (posicionCorte <= 0)
            {
                for (int i = limite + 1; i < texto.Length; i++)
                {
                    if (char.IsWhiteSpace(texto[i]))
                    {
                        posicionCorte = i;
                        break;
                    }
                }
            }

            if (posicionCorte <= 0)
                return texto;

            string linea1 = texto.Substring(0, posicionCorte).Trim();
            string linea2 = texto.Substring(posicionCorte + 1).Trim();

            return linea1 + Environment.NewLine + linea2;
        }

        private bool TieneBotonFavoritoVisible()
        {
            return btnFavorito != null && PuedeSerFavorito && !TieneSubMenus;
        }

        private void OrganizarSubMenus()
        {
            if (organizando || Width <= 0)
                return;

            organizando = true;

            OrganizarBotonFavorito();

            int posicionActual = 0;

            if (expandido)
            {
                foreach (SidebarMenuItem subMenu in subMenus)
                {
                    subMenu.Width = Width;
                    subMenu.Left = 0;
                    subMenu.Top = posicionActual;

                    posicionActual += subMenu.Height;
                }
            }

            pnlSubMenu.Left = 0;
            pnlSubMenu.Top = btnHeader.Height;
            pnlSubMenu.Width = Width;
            pnlSubMenu.Height = posicionActual;
            pnlSubMenu.Visible = expandido && TieneSubMenus;

            Height = btnHeader.Height + posicionActual;

            organizando = false;
        }
    }

    public class OpcionMenuSeleccionadaEventArgs : EventArgs
    {
        public SidebarMenuItem Opcion { get; private set; }

        public OpcionMenuSeleccionadaEventArgs(SidebarMenuItem opcion)
        {
            Opcion = opcion;
        }
    }
}