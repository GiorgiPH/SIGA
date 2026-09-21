using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PV.Clases.Usuarios;

namespace PV
{
    public partial class FavoritosMenu : UserControl
    {
        private readonly List<SidebarMenuItem> favoritos =
            new List<SidebarMenuItem>();

        private readonly ToolTip toolTipFavoritos =
            new ToolTip();

        private const int MaxFavoritos = 7;

        // Margen interno de las filas respecto al contenedor FAVORITOS.
        private const int MargenFavorito = 8;

        private const int SeparacionFavoritos = 2;
        private const int EspacioTitulo = 8;
        private const int AltoFavorito = 32;
        private const int AnchoBotonEliminar = 28;

        private string usuarioActual = "";

        private DBUsuariosFavoritos dbFavoritos;

        private bool ajustandoLayout = false;

        public FavoritosMenu()
        {
            InitializeComponent();

            ConfigurarControl();

            dbFavoritos =
                new DBUsuariosFavoritos();

            SizeChanged +=
                FavoritosMenu_SizeChanged;
        }

        [Browsable(false)]
        public string UsuarioActual
        {
            get
            {
                return usuarioActual;
            }

            set
            {
                usuarioActual =
                    (value ?? "").Trim();
            }
        }

        // ============================================================
        // CONFIGURACIÓN VISUAL
        // ============================================================

        private void ConfigurarControl()
        {
            BackColor =
                Color.FromArgb(24, 43, 66);

            lblTitulo.Text =
                "FAVORITOS";

            lblTitulo.ForeColor =
                Color.FromArgb(180, 200, 220);

            lblTitulo.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);

            lblTitulo.TextAlign =
                ContentAlignment.MiddleLeft;

            lblTitulo.Padding =
                new Padding(14, 0, 0, 0);

            // --------------------------------------------------------
            // FLOW LAYOUT
            // --------------------------------------------------------

            flpFavoritos.FlowDirection =
                FlowDirection.TopDown;

            flpFavoritos.WrapContents =
                false;

            // IMPORTANTE:
            // Ya no permitimos que AutoSize modifique también el ancho.
            flpFavoritos.AutoSize =
                false;

            flpFavoritos.Dock =
                DockStyle.None;

            flpFavoritos.Padding =
                Padding.Empty;

            flpFavoritos.Margin =
                Padding.Empty;

            flpFavoritos.AutoScroll =
                false;

            toolTipFavoritos.InitialDelay =
                400;

            toolTipFavoritos.ReshowDelay =
                100;

            toolTipFavoritos.AutoPopDelay =
                5000;

            Margin =
                new Padding(0, 14, 0, 0);

            Visible =
                false;

            AjustarLayoutFavoritos();
        }

        // ============================================================
        // CAMBIO DE TAMAÑO
        // ============================================================

        private void FavoritosMenu_SizeChanged(
            object sender,
            EventArgs e)
        {
            AjustarLayoutFavoritos();
        }

        // ============================================================
        // AJUSTAR TODO EL CONTENIDO AL ANCHO DEL CONTROL
        // ============================================================

        private void AjustarLayoutFavoritos()
        {
            if (ajustandoLayout ||
                IsDisposed ||
                flpFavoritos == null ||
                flpFavoritos.IsDisposed)
            {
                return;
            }

            ajustandoLayout =
                true;

            try
            {
                int anchoDisponible =
                    Math.Max(
                        1,
                        ClientSize.Width);

                // El título ocupa todo el ancho.
                lblTitulo.Left =
                    0;

                lblTitulo.Width =
                    anchoDisponible;

                // El FlowLayout ocupa exactamente el mismo ancho
                // que FavoritosMenu.
                flpFavoritos.Left =
                    0;

                flpFavoritos.Top =
                    lblTitulo.Bottom +
                    EspacioTitulo;

                flpFavoritos.Width =
                    anchoDisponible;

                // Ajustamos todas las filas existentes.
                foreach (Control control in flpFavoritos.Controls)
                {
                    Panel panel =
                        control as Panel;

                    if (panel == null)
                        continue;

                    panel.Width =
                        Math.Max(
                            1,
                            anchoDisponible -
                            (MargenFavorito * 2));

                    panel.Margin =
                        new Padding(
                            MargenFavorito,
                            0,
                            MargenFavorito,
                            SeparacionFavoritos);

                    ActualizarTextoPanelFavorito(
                        panel);
                }

                int altoContenido =
                    CalcularAltoContenidoFavoritos();

                flpFavoritos.Height =
                    altoContenido;

                Height =
                    flpFavoritos.Top +
                    altoContenido;
            }
            finally
            {
                ajustandoLayout =
                    false;
            }
        }

        // ============================================================
        // CALCULAR ALTURA DE FAVORITOS
        // ============================================================

        private int CalcularAltoContenidoFavoritos()
        {
            if (favoritos.Count == 0)
                return 0;

            return
                (favoritos.Count * AltoFavorito) +
                (favoritos.Count * SeparacionFavoritos);
        }

        // ============================================================
        // RECALCULAR TEXTO DE UNA FILA
        // ============================================================

        private void ActualizarTextoPanelFavorito(
            Panel panel)
        {
            if (panel == null)
                return;

            Button botonOpcion =
                null;

            foreach (Control control in panel.Controls)
            {
                Button boton =
                    control as Button;

                if (boton == null)
                    continue;

                if (boton.Tag is SidebarMenuItem &&
                    boton.Dock == DockStyle.Fill)
                {
                    botonOpcion =
                        boton;

                    break;
                }
            }

            if (botonOpcion == null)
                return;

            SidebarMenuItem opcion =
                botonOpcion.Tag as SidebarMenuItem;

            if (opcion == null)
                return;

            string textoCompleto =
                "•  " + opcion.Titulo;

            int anchoDisponible =
                panel.Width -
                AnchoBotonEliminar -
                botonOpcion.Padding.Left -
                botonOpcion.Padding.Right -
                6;

            anchoDisponible =
                Math.Max(
                    1,
                    anchoDisponible);

            botonOpcion.Text =
                AjustarTextoFavorito(
                    textoCompleto,
                    botonOpcion.Font,
                    anchoDisponible);

            if (botonOpcion.Text != textoCompleto)
            {
                toolTipFavoritos.SetToolTip(
                    botonOpcion,
                    opcion.Titulo);
            }
            else
            {
                toolTipFavoritos.SetToolTip(
                    botonOpcion,
                    "");
            }
        }

        // ============================================================
        // CARGAR FAVORITOS
        // ============================================================

        public void CargarFavoritosUsuario()
        {
            if (string.IsNullOrWhiteSpace(
                UsuarioActual))
            {
                LimpiarListaVisual();
                ActualizarFavoritos();

                return;
            }

            LimpiarListaVisual();

            List<string> clavesFavoritos =
                dbFavoritos.CargarFavoritos(
                    UsuarioActual);

            foreach (string clave in clavesFavoritos)
            {
                SidebarMenuItem opcion =
                    BuscarOpcionPorClave(
                        clave);

                if (opcion != null &&
                    !favoritos.Contains(opcion) &&
                    favoritos.Count < MaxFavoritos)
                {
                    opcion.EstablecerFavoritoInicial(
                        true);

                    favoritos.Add(
                        opcion);
                }
            }

            ActualizarFavoritos();
        }

        // ============================================================
        // BUSCAR OPCIÓN
        // ============================================================

        private SidebarMenuItem BuscarOpcionPorClave(
            string clave)
        {
            if (string.IsNullOrWhiteSpace(
                clave))
            {
                return null;
            }

            SidebarMenuItem[] opciones =
                ObtenerTodasLasOpciones();

            foreach (SidebarMenuItem opcion in opciones)
            {
                if (string.Equals(
                    opcion.Clave,
                    clave,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return opcion;
                }
            }

            return null;
        }

        // ============================================================
        // OBTENER TODAS LAS OPCIONES
        // ============================================================

        private SidebarMenuItem[] ObtenerTodasLasOpciones()
        {
            List<SidebarMenuItem> resultado =
                new List<SidebarMenuItem>();

            Form formulario =
                FindForm();

            if (formulario == null)
                return resultado.ToArray();

            ObtenerOpcionesDesdeControl(
                formulario,
                resultado);

            return resultado.ToArray();
        }

        private void ObtenerOpcionesDesdeControl(
            Control control,
            List<SidebarMenuItem> resultado)
        {
            foreach (Control hijo in control.Controls)
            {
                SidebarMenuItem opcion =
                    hijo as SidebarMenuItem;

                if (opcion != null)
                {
                    if (!opcion.TieneSubMenus &&
                        !string.IsNullOrWhiteSpace(
                            opcion.Clave))
                    {
                        resultado.Add(
                            opcion);
                    }

                    ObtenerOpcionesDesdeControl(
                        opcion,
                        resultado);
                }
                else
                {
                    ObtenerOpcionesDesdeControl(
                        hijo,
                        resultado);
                }
            }
        }

        // ============================================================
        // LIMPIAR LISTA
        // ============================================================

        private void LimpiarListaVisual()
        {
            foreach (
                SidebarMenuItem opcion
                in new List<SidebarMenuItem>(
                    favoritos))
            {
                opcion.EstablecerFavoritoInicial(
                    false);
            }

            favoritos.Clear();

            flpFavoritos.Controls.Clear();
        }

        // ============================================================
        // SOLICITUD FAVORITO
        // ============================================================

        private void Opcion_FavoritoSolicitado(
            object sender,
            EventArgs e)
        {
            SidebarMenuItem opcion =
                sender as SidebarMenuItem;

            if (opcion == null)
                return;

            if (opcion.EsFavorito)
            {
                opcion.EsFavorito =
                    false;

                return;
            }

            if (favoritos.Contains(
                opcion))
            {
                return;
            }

            if (favoritos.Count >=
                MaxFavoritos)
            {
                MessageBox.Show(
                    "Solo puedes seleccionar hasta 7 opciones favoritas.",
                    "Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            opcion.EsFavorito =
                true;
        }

        // ============================================================
        // REGISTRAR OPCIÓN
        // ============================================================

        public void RegistrarOpcion(
            SidebarMenuItem opcion)
        {
            if (opcion == null ||
                opcion.TieneSubMenus ||
                !opcion.PuedeSerFavorito ||
                string.IsNullOrWhiteSpace(
                    opcion.Clave))
            {
                return;
            }

            opcion.FavoritoSolicitado -=
                Opcion_FavoritoSolicitado;

            opcion.FavoritoSolicitado +=
                Opcion_FavoritoSolicitado;

            opcion.FavoritoCambiado -=
                Opcion_FavoritoCambiado;

            opcion.FavoritoCambiado +=
                Opcion_FavoritoCambiado;

            if (opcion.EsFavorito)
            {
                AgregarFavorito(
                    opcion,
                    false);
            }
        }

        // ============================================================
        // FAVORITO CAMBIADO
        // ============================================================

        private void Opcion_FavoritoCambiado(
            object sender,
            EventArgs e)
        {
            SidebarMenuItem opcion =
                sender as SidebarMenuItem;

            if (opcion == null)
                return;

            if (opcion.EsFavorito)
            {
                bool agregado =
                    AgregarFavorito(
                        opcion,
                        true);

                if (!agregado)
                {
                    opcion.EstablecerFavoritoInicial(
                        false);
                }
            }
            else
            {
                bool eliminado =
                    QuitarFavorito(
                        opcion,
                        true);

                if (!eliminado)
                {
                    opcion.EstablecerFavoritoInicial(
                        true);
                }
            }
        }

        // ============================================================
        // AGREGAR FAVORITO
        // ============================================================

        public bool AgregarFavorito(
            SidebarMenuItem opcion,
            bool guardarEnBaseDatos)
        {
            if (opcion == null)
                return false;

            if (opcion.TieneSubMenus ||
                !opcion.PuedeSerFavorito)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                opcion.Clave))
            {
                return false;
            }

            if (favoritos.Contains(
                opcion))
            {
                return true;
            }

            if (favoritos.Count >=
                MaxFavoritos)
            {
                return false;
            }

            if (guardarEnBaseDatos)
            {
                if (string.IsNullOrWhiteSpace(
                    UsuarioActual))
                {
                    return false;
                }

                bool guardado =
                    dbFavoritos.AgregarFavorito(
                        UsuarioActual,
                        opcion.Clave);

                if (!guardado)
                    return false;
            }

            favoritos.Add(
                opcion);

            ActualizarFavoritos();

            return true;
        }

        // ============================================================
        // QUITAR FAVORITO
        // ============================================================

        public void QuitarFavorito(
            SidebarMenuItem opcion)
        {
            QuitarFavorito(
                opcion,
                true);
        }

        private bool QuitarFavorito(
            SidebarMenuItem opcion,
            bool eliminarDeBaseDatos)
        {
            if (opcion == null)
                return false;

            if (eliminarDeBaseDatos)
            {
                if (string.IsNullOrWhiteSpace(
                    UsuarioActual))
                {
                    return false;
                }

                bool eliminado =
                    dbFavoritos.EliminarFavorito(
                        UsuarioActual,
                        opcion.Clave);

                if (!eliminado)
                    return false;
            }

            favoritos.Remove(
                opcion);

            ActualizarFavoritos();

            return true;
        }

        // ============================================================
        // LIMPIAR FAVORITOS
        // ============================================================

        public void LimpiarFavoritos()
        {
            if (favoritos.Count == 0)
            {
                ActualizarFavoritos();

                return;
            }

            if (!string.IsNullOrWhiteSpace(
                UsuarioActual))
            {
                bool eliminados =
                    dbFavoritos
                    .EliminarTodosLosFavoritos(
                        UsuarioActual);

                if (!eliminados)
                    return;
            }

            List<SidebarMenuItem> copia =
                new List<SidebarMenuItem>(
                    favoritos);

            foreach (SidebarMenuItem opcion in copia)
            {
                opcion.EstablecerFavoritoInicial(
                    false);
            }

            favoritos.Clear();

            flpFavoritos.Controls.Clear();

            ActualizarFavoritos();
        }

        // ============================================================
        // CONSULTAS
        // ============================================================

        public bool ContieneFavorito(
            SidebarMenuItem opcion)
        {
            return
                opcion != null &&
                favoritos.Contains(
                    opcion);
        }

        public int CantidadFavoritos
        {
            get
            {
                return favoritos.Count;
            }
        }

        // ============================================================
        // ACTUALIZAR FAVORITOS
        // ============================================================

        private void ActualizarFavoritos()
        {
            flpFavoritos.SuspendLayout();

            try
            {
                flpFavoritos.Controls.Clear();

                foreach (
                    SidebarMenuItem opcion
                    in favoritos)
                {
                    Panel panel =
                        CrearFavorito(
                            opcion);

                    flpFavoritos.Controls.Add(
                        panel);
                }
            }
            finally
            {
                flpFavoritos.ResumeLayout(
                    true);
            }

            Visible =
                favoritos.Count > 0;

            AjustarLayoutFavoritos();
        }

        // ============================================================
        // CREAR FILA FAVORITO
        // ============================================================

        private Panel CrearFavorito(
            SidebarMenuItem opcion)
        {
            Panel panel =
                new Panel();

            panel.Height =
                AltoFavorito;

            panel.Width =
                Math.Max(
                    1,
                    ClientSize.Width -
                    (MargenFavorito * 2));

            panel.Margin =
                new Padding(
                    MargenFavorito,
                    0,
                    MargenFavorito,
                    SeparacionFavoritos);

            panel.BackColor =
                Color.FromArgb(
                    35,
                    58,
                    88);

            // --------------------------------------------------------
            // BOTÓN ELIMINAR
            // --------------------------------------------------------

            Button eliminar =
                new Button();

            eliminar.Text =
                "×";

            eliminar.Tag =
                opcion;

            eliminar.Width =
                AnchoBotonEliminar;

            eliminar.Dock =
                DockStyle.Right;

            eliminar.FlatStyle =
                FlatStyle.Flat;

            eliminar.FlatAppearance.BorderSize =
                0;

            eliminar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(
                    50,
                    79,
                    112);

            eliminar.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(
                    50,
                    79,
                    112);

            eliminar.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Regular);

            eliminar.ForeColor =
                Color.FromArgb(
                    180,
                    200,
                    220);

            eliminar.BackColor =
                Color.FromArgb(
                    35,
                    58,
                    88);

            eliminar.Cursor =
                Cursors.Hand;

            eliminar.Click +=
                EliminarFavorito_Click;

            // --------------------------------------------------------
            // BOTÓN OPCIÓN
            // --------------------------------------------------------

            Button boton =
                new Button();

            boton.Tag =
                opcion;

            boton.Dock =
                DockStyle.Fill;

            boton.FlatStyle =
                FlatStyle.Flat;

            boton.FlatAppearance.BorderSize =
                0;

            boton.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(
                    50,
                    79,
                    112);

            boton.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(
                    50,
                    79,
                    112);

            boton.TextAlign =
                ContentAlignment.MiddleLeft;

            boton.Padding =
                new Padding(
                    12,
                    0,
                    6,
                    0);

            boton.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Regular);

            boton.ForeColor =
                Color.FromArgb(
                    220,
                    232,
                    244);

            boton.BackColor =
                Color.FromArgb(
                    35,
                    58,
                    88);

            boton.Cursor =
                Cursors.Hand;

            string textoCompleto =
                "•  " +
                opcion.Titulo;

            int anchoDisponible =
                panel.Width -
                AnchoBotonEliminar -
                boton.Padding.Left -
                boton.Padding.Right -
                6;

            boton.Text =
                AjustarTextoFavorito(
                    textoCompleto,
                    boton.Font,
                    anchoDisponible);

            if (boton.Text !=
                textoCompleto)
            {
                toolTipFavoritos.SetToolTip(
                    boton,
                    opcion.Titulo);
            }
            else
            {
                toolTipFavoritos.SetToolTip(
                    boton,
                    "");
            }

            boton.Click +=
                Favorito_Click;

            panel.Controls.Add(
                boton);

            panel.Controls.Add(
                eliminar);

            eliminar.BringToFront();

            return panel;
        }

        // ============================================================
        // AJUSTAR TEXTO
        // ============================================================

        private string AjustarTextoFavorito(
            string texto,
            Font fuente,
            int anchoDisponible)
        {
            if (string.IsNullOrWhiteSpace(
                texto))
            {
                return "";
            }

            texto =
                texto
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Trim();

            Size medida =
                TextRenderer.MeasureText(
                    texto,
                    fuente,
                    new Size(
                        int.MaxValue,
                        int.MaxValue),
                    TextFormatFlags.SingleLine |
                    TextFormatFlags.NoPadding);

            if (medida.Width <=
                anchoDisponible)
            {
                return texto;
            }

            const string puntos =
                "...";

            string textoRecortado =
                texto;

            while (
                textoRecortado.Length > 1)
            {
                textoRecortado =
                    textoRecortado
                    .Substring(
                        0,
                        textoRecortado.Length - 1)
                    .TrimEnd();

                string candidato =
                    textoRecortado +
                    puntos;

                medida =
                    TextRenderer.MeasureText(
                        candidato,
                        fuente,
                        new Size(
                            int.MaxValue,
                            int.MaxValue),
                        TextFormatFlags.SingleLine |
                        TextFormatFlags.NoPadding);

                if (medida.Width <=
                    anchoDisponible)
                {
                    return candidato;
                }
            }

            return puntos;
        }

        // ============================================================
        // ABRIR FAVORITO
        // ============================================================

        private void Favorito_Click(
            object sender,
            EventArgs e)
        {
            Button boton =
                sender as Button;

            if (boton == null)
                return;

            SidebarMenuItem opcion =
                boton.Tag as SidebarMenuItem;

            if (opcion == null)
                return;

            opcion.Seleccionar();
        }

        // ============================================================
        // ELIMINAR FAVORITO
        // ============================================================

        private void EliminarFavorito_Click(
            object sender,
            EventArgs e)
        {
            Button boton =
                sender as Button;

            if (boton == null)
                return;

            SidebarMenuItem opcion =
                boton.Tag as SidebarMenuItem;

            if (opcion == null)
                return;

            opcion.EsFavorito =
                false;
        }
    }
}