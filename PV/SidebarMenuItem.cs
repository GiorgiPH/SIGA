using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class SidebarMenuItem : UserControl
    {
        public SidebarMenuItem()
        {
            InitializeComponent();
            btnHeader.Click += btnHeader_Click;
            SizeChanged += SidebarMenuItem_SizeChanged;

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        private readonly List<SidebarMenuItem> subMenus =
        new List<SidebarMenuItem>();

        private bool expandido;
        private bool organizando;
        private string titulo = "Nuevo elemento";
        private string clave = "";
        private int nivel = 0;
        private bool seleccionado;

        public event EventHandler<OpcionMenuSeleccionadaEventArgs> OpcionSeleccionada;

        public event EventHandler ModuloPrincipalExpandido;

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

        public void LimpiarSeleccion()
        {
            Seleccionado = false;

            foreach (SidebarMenuItem subMenu in subMenus)
            {
                subMenu.LimpiarSeleccion();
            }
        }

        public Image Icono
        {
            get { return btnHeader.Image; }
            set { btnHeader.Image = value; }
        }
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value ?? "";
                ActualizarTextoEncabezado();
            }
        }

        // Identificador para saber qué formulario o acción debe abrirse.
        public string Clave
        {
            get { return clave; }
            set { clave = value ?? ""; }
        }

        public bool TieneSubMenus
        {
            get { return subMenus.Count > 0; }
        }

        public bool Expandido
        {
            get { return expandido; }
            set
            {
                if (!TieneSubMenus)
                    value = false;

                // Al contraer una opción, contrae todos sus niveles hijos.
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

            AgregarSubMenu(subMenu);

            return subMenu;
        }

        public void AgregarSubMenu(SidebarMenuItem subMenu)
        {
            if (subMenu == null)
                throw new ArgumentNullException("subMenu");

            subMenus.Add(subMenu);
            pnlSubMenu.Controls.Add(subMenu);

            subMenu.EstablecerNivel(nivel + 1);
            subMenu.OpcionSeleccionada += SubMenu_OpcionSeleccionada;
            subMenu.SizeChanged += delegate { OrganizarSubMenus(); };

            ActualizarTextoEncabezado();
            OrganizarSubMenus();
        }

        public void Expandir(bool expandir = true)
        {
            Expandido = expandir;
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            if (TieneSubMenus)
            {
                // Antes de expandir un módulo principal, avisa al formulario
                // para que contraiga los demás.
                if (!Expandido && nivel == 0 &&
                    ModuloPrincipalExpandido != null)
                {
                    ModuloPrincipalExpandido(this, EventArgs.Empty);
                }

                Expandido = !Expandido;
            }

            if (!string.IsNullOrWhiteSpace(Clave) &&
                OpcionSeleccionada != null)
            {
                OpcionSeleccionada(
                    this,
                    new OpcionMenuSeleccionadaEventArgs(this)
                );
            }
        }

        private void SubMenu_OpcionSeleccionada(
            object sender,
            OpcionMenuSeleccionadaEventArgs e)
        {
            // El formulario principal escucha únicamente a los módulos principales.
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
                string flecha = TieneSubMenus
                    ? (expandido ? " ▾" : " ▸")
                    : "";

                btnHeader.Height = 46;
                btnHeader.Text = titulo + flecha;
                btnHeader.TextAlign = HorizontalAlignment.Center;
                btnHeader.TextOffset = Point.Empty;
                btnHeader.ForeColor = Color.White;
                btnHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnHeader.BorderRadius = 6;
                btnHeader.Padding = new Padding(16, 0, 12, 0);

                // El módulo abierto se distingue de los demás.
                btnHeader.FillColor = expandido
                    ? Color.FromArgb(91, 132, 173)
                    : Color.FromArgb(145, 174, 205);

                btnHeader.HoverState.FillColor = expandido
                    ? Color.FromArgb(102, 144, 185)
                    : Color.FromArgb(121, 157, 195);
            }
            else
            {
                string flechaSubMenu = TieneSubMenus
                    ? (expandido ? " ▾" : " ▸")
                    : "";

                btnHeader.Height = 34;
                btnHeader.BorderRadius = 0;
                btnHeader.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

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

                    btnHeader.HoverState.FillColor =
                        Color.FromArgb(66, 115, 161);
                }
                else
                {
                    btnHeader.Text = "•  " + titulo + flechaSubMenu;
                    btnHeader.FillColor = Color.FromArgb(35, 58, 88);
                    btnHeader.ForeColor = Color.FromArgb(220, 232, 244);

                    btnHeader.HoverState.FillColor =
                        Color.FromArgb(62, 94, 130);
                }
            }
        }

        private void OrganizarSubMenus()
        {
           
            if (organizando || Width <= 0)
                return;

            organizando = true;

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
