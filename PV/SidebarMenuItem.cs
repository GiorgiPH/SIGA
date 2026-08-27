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

        private const float FuenteModuloBase = 10F;
        private const float FuenteSubMenuBase = 9F;

        private const int AltoModuloBase = 46;
        private const int AltoSubMenuBase = 34;

        public event EventHandler<OpcionMenuSeleccionadaEventArgs> OpcionSeleccionada;
        public event EventHandler ModuloPrincipalExpandido;
        public event EventHandler FavoritoCambiado;
        public event EventHandler FavoritoSolicitado;

        public SidebarMenuItem()
        {
            InitializeComponent();

            btnHeader.Click += btnHeader_Click;
            SizeChanged += SidebarMenuItem_SizeChanged;

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        public void Seleccionar()
        {
            Seleccionado = true;

            if (OpcionSeleccionada != null)
            {
                OpcionSeleccionada(
                    this,
                    new OpcionMenuSeleccionadaEventArgs(this));
            }
        }

        public void EjecutarOpcion()
        {
            if (string.IsNullOrWhiteSpace(Clave))
                return;

            if (OpcionSeleccionada != null)
            {
                OpcionSeleccionada(
                    this,
                    new OpcionMenuSeleccionadaEventArgs(this));
            }
        }

        [Category("Favoritos")]
        [DefaultValue(true)]
        [Description("Indica si esta opción puede agregarse a favoritos.")]
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
        [Description("Indica si esta opción está actualmente marcada como favorita.")]
        public bool EsFavorito
        {
            get { return esFavorito; }
            set
            {
                if (esFavorito == value)
                    return;

                esFavorito = value;

                ActualizarTextoEncabezado();

                if (FavoritoCambiado != null)
                    FavoritoCambiado(this, EventArgs.Empty);
            }
        }

        public void EstablecerFavoritoInicial(bool favorito)
        {
            if (!PuedeSerFavorito || TieneSubMenus)
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
            btnFavorito.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btnFavorito.ForeColor = Color.FromArgb(220, 232, 244);
            btnFavorito.FillColor = Color.Transparent;
            btnFavorito.BorderThickness = 0;
            btnFavorito.BorderRadius = 0;
            btnFavorito.HoverState.FillColor = Color.FromArgb(62, 94, 130);
            btnFavorito.HoverState.ForeColor = Color.White;
            btnFavorito.PressedColor = Color.Transparent;
            btnFavorito.TextAlign = HorizontalAlignment.Center;
            btnFavorito.Cursor = Cursors.Hand;
            btnFavorito.TabStop = false;

            btnFavorito.Click += btnFavorito_Click;

            Controls.Add(btnFavorito);
            btnFavorito.BringToFront();
        }

        private void btnFavorito_Click(object sender, EventArgs e)
        {
            if (FavoritoSolicitado != null)
                FavoritoSolicitado(this, EventArgs.Empty);
        }

        private void OrganizarBotonFavorito()
        {
            if (btnFavorito == null)
                return;

            if (nivel == 0 ||
                !PuedeSerFavorito ||
                TieneSubMenus ||
                string.IsNullOrWhiteSpace(Clave))
            {
                btnFavorito.Visible = false;
                btnHeader.Width = Width;
                return;
            }

            const int anchoFavorito = 32;

            btnFavorito.Visible = true;
            btnFavorito.Width = anchoFavorito;
            btnFavorito.Height = btnHeader.Height;
            btnFavorito.Left = Width - anchoFavorito;
            btnFavorito.Top = 0;

            btnHeader.Width = Width - anchoFavorito;

            btnFavorito.BringToFront();
        }

        [Category("Menu")]
        [DefaultValue(true)]
        [Description("Ajusta automáticamente el tamaño de fuente y altura del menú según el DPI de Windows.")]
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

            float escala = DeviceDpi / 96F;

            return escala;
        }

        private float ObtenerFuenteModulo()
        {
            float escala = ObtenerEscalaDpi();

            escala = Math.Min(escala, 1.35F);

            return FuenteModuloBase * escala;
        }

        private float ObtenerFuenteSubMenu()
        {
            float escala = ObtenerEscalaDpi();

            escala = Math.Min(escala, 1.35F);

            return FuenteSubMenuBase * escala;
        }

        private int ObtenerAltoModulo()
        {
            float escala = ObtenerEscalaDpi();

            escala = Math.Min(escala, 1.35F);

            return (int)Math.Round(AltoModuloBase * escala);
        }

        private int ObtenerAltoSubMenu()
        {
            float escala = ObtenerEscalaDpi();

            escala = Math.Min(escala, 1.35F);

            return (int)Math.Round(AltoSubMenuBase * escala);
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

        public void LimpiarSeleccion()
        {
            Seleccionado = false;

            foreach (SidebarMenuItem subMenu in subMenus)
            {
                subMenu.LimpiarSeleccion();
            }
        }

        [Category("Menu")]
        public Image Icono
        {
            get { return btnHeader.Image; }
            set { btnHeader.Image = value; }
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
                    {
                        subMenu.Expandir(false);
                    }
                }

                if (expandido == value)
                    return;

                expandido = value;

                ActualizarTextoEncabezado();
                OrganizarSubMenus();
            }
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
                else if (subMenu.PuedeSerFavorito &&
                    !string.IsNullOrWhiteSpace(subMenu.Clave))
                {
                    favoritosMenu.RegistrarOpcion(subMenu);
                }
            }
        }

        public void Expandir(bool expandir = true)
        {
            Expandido = expandir;
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            if (TieneSubMenus)
            {
                if (!Expandido &&
                    nivel == 0 &&
                    ModuloPrincipalExpandido != null)
                {
                    ModuloPrincipalExpandido(this, EventArgs.Empty);
                }

                Expandido = !Expandido;
            }

            EjecutarOpcion();
        }

        private void SubMenu_OpcionSeleccionada(object sender, OpcionMenuSeleccionadaEventArgs e)
        {
            if (OpcionSeleccionada != null)
            {
                OpcionSeleccionada(this, e);
            }
        }

        private void SidebarMenuItem_SizeChanged(object sender, EventArgs e)
        {
            OrganizarSubMenus();
        }

        private void EstablecerNivel(int nuevoNivel)
        {
            nivel = nuevoNivel;

            foreach (SidebarMenuItem subMenu in subMenus)
            {
                subMenu.EstablecerNivel(nivel + 1);
            }

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        private void ActualizarTextoEncabezado()
        {
            if (nivel == 0)
            {
                string flecha = TieneSubMenus ? (expandido ? " ▾" : " ▸") : "";

                btnHeader.Height = ObtenerAltoModulo();
                btnHeader.Width = Width;
                btnHeader.Text = titulo + flecha;
                btnHeader.TextAlign = HorizontalAlignment.Center;
                btnHeader.TextOffset = Point.Empty;
                btnHeader.ForeColor = Color.White;
                btnHeader.Font = new Font("Segoe UI", ObtenerFuenteModulo(), FontStyle.Bold);
                btnHeader.BorderRadius = 6;
                btnHeader.Padding = new Padding(16, 0, 12, 0);

                btnHeader.FillColor = expandido
                    ? Color.FromArgb(91, 132, 173)
                    : Color.FromArgb(145, 174, 205);

                btnHeader.HoverState.FillColor = expandido
                    ? Color.FromArgb(102, 144, 185)
                    : Color.FromArgb(121, 157, 195);

                if (btnFavorito != null)
                    btnFavorito.Visible = false;

                return;
            }

            string flechaSubMenu = TieneSubMenus ? (expandido ? " ▾" : " ▸") : "";

            btnHeader.Height = ObtenerAltoSubMenu();
            btnHeader.BorderRadius = 0;
            btnHeader.Font = new Font("Segoe UI", ObtenerFuenteSubMenu(), FontStyle.Regular);
            btnHeader.TextAlign = HorizontalAlignment.Left;
            btnHeader.TextOffset = Point.Empty;

            btnHeader.Padding = new Padding(
                14 + ((nivel - 1) * 14),
                0,
                12,
                0);

            if (seleccionado)
            {
                btnHeader.Text = "▌  " + titulo + flechaSubMenu;
                btnHeader.FillColor = Color.FromArgb(57, 101, 145);
                btnHeader.ForeColor = Color.White;
                btnHeader.HoverState.FillColor = Color.FromArgb(66, 115, 161);
            }
            else
            {
                btnHeader.Text = "•  " + titulo + flechaSubMenu;
                btnHeader.FillColor = Color.FromArgb(35, 58, 88);
                btnHeader.ForeColor = Color.FromArgb(220, 232, 244);
                btnHeader.HoverState.FillColor = Color.FromArgb(62, 94, 130);
            }

            if (PuedeSerFavorito &&
                !TieneSubMenus &&
                !string.IsNullOrWhiteSpace(Clave))
            {
                CrearBotonFavorito();

                btnFavorito.Text = EsFavorito ? "★" : "☆";
                btnFavorito.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                btnFavorito.ForeColor = EsFavorito
                    ? Color.White
                    : Color.FromArgb(220, 232, 244);

                btnFavorito.Visible = true;
            }
            else
            {
                if (btnFavorito != null)
                    btnFavorito.Visible = false;
            }
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
        public SidebarMenuItem Opcion
        {
            get;
            private set;
        }

        public OpcionMenuSeleccionadaEventArgs(SidebarMenuItem opcion)
        {
            Opcion = opcion;
        }
    }
}