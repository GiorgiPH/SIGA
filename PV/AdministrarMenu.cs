using PuntoVentas.Clases.Usuarios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PV
{
    public partial class AdministrarMenu : Form
    {
        private class OpcionPadreItem
        {
            public int? IdOpcionMenu { get; set; }
            public int Nivel { get; set; }
            public string Nombre { get; set; }
            public string Ruta { get; set; }

            public override string ToString()
            {
                return Ruta ?? string.Empty;
            }
        }

        private readonly DBMenu dbMenu;

        private List<OpcionMenu> opcionesMenu;

        private bool cargandoFormulario;

        // null = nuevo
        // valor = editando registro existente
        private int? idOpcionEditando;

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

        public AdministrarMenu()
        {
            InitializeComponent();

            dbMenu =
                new DBMenu();

            opcionesMenu =
                new List<OpcionMenu>();

            ConfigurarEventos();
        }

        // ============================================================
        // EVENTOS
        // ============================================================

        private void ConfigurarEventos()
        {
            Load +=
                AdministrarMenu_Load;

            cmbPadre.SelectedIndexChanged +=
                cmbPadre_SelectedIndexChanged;

            txtNombre.Leave +=
                txtNombre_Leave;

            tvMenu.AfterSelect +=
                tvMenu_AfterSelect;
        }

        // ============================================================
        // LOAD
        // ============================================================

        private void AdministrarMenu_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                cargandoFormulario =
                    true;

                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible cargar la administración del menú." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cargandoFormulario =
                    false;
            }

            // Esperamos a que WinForms termine de inicializar
            // visualmente el TreeView.
            BeginInvoke(
                new Action(
                    delegate
                    {
                        PrepararNuevo();
                    }));
        }

        // ============================================================
        // CARGAR DATOS
        // ============================================================

        private void CargarDatos()
        {
            opcionesMenu =
                dbMenu.ObtenerOpciones()
                ?? new List<OpcionMenu>();

            CargarArbol();
            CargarOpcionesPadre();
        }

        // ============================================================
        // TREEVIEW
        // ============================================================

        private void CargarArbol()
        {
            tvMenu.BeginUpdate();

            try
            {
                tvMenu.Nodes.Clear();

                Dictionary<int, TreeNode> nodos =
                    new Dictionary<int, TreeNode>();

                foreach (OpcionMenu opcion in opcionesMenu
                    .OrderBy(x => x.Nivel)
                    .ThenBy(x => x.Orden)
                    .ThenBy(x => x.IdOpcionMenu))
                {
                    nodos[opcion.IdOpcionMenu] =
                        CrearNodo(opcion);
                }

                foreach (OpcionMenu opcion in opcionesMenu
                    .OrderBy(x => x.Nivel)
                    .ThenBy(x => x.Orden)
                    .ThenBy(x => x.IdOpcionMenu))
                {
                    TreeNode nodo;

                    if (!nodos.TryGetValue(
                        opcion.IdOpcionMenu,
                        out nodo))
                    {
                        continue;
                    }

                    if (!opcion.IdOpcionPadre.HasValue)
                    {
                        tvMenu.Nodes.Add(nodo);
                        continue;
                    }

                    TreeNode nodoPadre;

                    if (nodos.TryGetValue(
                        opcion.IdOpcionPadre.Value,
                        out nodoPadre))
                    {
                        nodoPadre.Nodes.Add(nodo);
                    }
                    else
                    {
                        tvMenu.Nodes.Add(nodo);
                    }
                }

                OrdenarNodos(
                    tvMenu.Nodes);

                tvMenu.ExpandAll();
            }
            finally
            {
                tvMenu.EndUpdate();
            }
        }

        private TreeNode CrearNodo(
            OpcionMenu opcion)
        {
            string texto =
                opcion.Nombre ?? "";

            if (!opcion.Activo)
            {
                texto +=
                    "  [INACTIVO]";
            }

            TreeNode nodo =
                new TreeNode(texto);

            nodo.Name =
                opcion.Clave ?? "";

            nodo.Tag =
                opcion;

            if (!opcion.Activo)
            {
                nodo.ForeColor =
                    System.Drawing.Color.FromArgb(
                        145,
                        155,
                        170);
            }
            else if (opcion.EsOpcion)
            {
                nodo.ForeColor =
                    System.Drawing.Color.FromArgb(
                        31,
                        57,
                        91);
            }
            else
            {
                nodo.ForeColor =
                    System.Drawing.Color.FromArgb(
                        21,
                        48,
                        87);

                nodo.NodeFont =
                    new System.Drawing.Font(
                        tvMenu.Font,
                        System.Drawing.FontStyle.Bold);
            }

            return nodo;
        }

        private void OrdenarNodos(
            TreeNodeCollection nodos)
        {
            if (nodos == null ||
                nodos.Count == 0)
            {
                return;
            }

            List<TreeNode> lista =
                nodos
                .Cast<TreeNode>()
                .OrderBy(
                    x =>
                    {
                        OpcionMenu opcion =
                            x.Tag as OpcionMenu;

                        return opcion != null
                            ? opcion.Orden
                            : int.MaxValue;
                    })
                .ThenBy(
                    x =>
                    {
                        OpcionMenu opcion =
                            x.Tag as OpcionMenu;

                        return opcion != null
                            ? opcion.IdOpcionMenu
                            : int.MaxValue;
                    })
                .ToList();

            nodos.Clear();

            foreach (TreeNode nodo in lista)
            {
                nodos.Add(nodo);

                if (nodo.Nodes.Count > 0)
                {
                    OrdenarNodos(
                        nodo.Nodes);
                }
            }
        }

        // ============================================================
        // SELECCIÓN DEL TREEVIEW
        // ============================================================

        private void tvMenu_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            if (cargandoFormulario ||
                e == null ||
                e.Node == null)
            {
                return;
            }

            OpcionMenu opcion =
                e.Node.Tag as OpcionMenu;

            if (opcion == null)
                return;

            CargarOpcionParaEditar(
                opcion.IdOpcionMenu);
        }

        // ============================================================
        // CARGAR OPCIÓN PARA EDITAR
        // ============================================================

        private void CargarOpcionParaEditar(
            int idOpcionMenu)
        {
            OpcionMenu opcion =
                opcionesMenu.FirstOrDefault(
                    x =>
                        x.IdOpcionMenu ==
                        idOpcionMenu);

            if (opcion == null)
                return;

            cargandoFormulario =
                true;

            try
            {
                idOpcionEditando =
                    opcion.IdOpcionMenu;

                txtNombre.Text =
                    opcion.Nombre ?? "";

                txtClave.Text =
                    opcion.Clave ?? "";

                // Recargamos padres excluyendo la propia opción
                // y todos sus descendientes.
                CargarOpcionesPadre();

                SeleccionarPadre(
                    opcion.IdOpcionPadre);

                txtNivel.Text =
                    opcion.Nivel.ToString();

                decimal orden =
                    opcion.Orden;

                if (orden < nudOrden.Minimum)
                    orden = nudOrden.Minimum;

                if (orden > nudOrden.Maximum)
                    orden = nudOrden.Maximum;

                nudOrden.Value =
                    orden;

                rbOpcion.Checked =
                    opcion.EsOpcion;

                rbContenedor.Checked =
                    !opcion.EsOpcion;

                chkActivo.Checked =
                    opcion.Activo;

                btnGuardar.Text =
                    "Guardar cambios";

                // Solo se puede eliminar cuando estamos
                // editando una opción existente.
                btnEliminar.Enabled =
                    true;
            }
            finally
            {
                cargandoFormulario =
                    false;
            }
        }

        // ============================================================
        // PADRES
        // ============================================================

        private void CargarOpcionesPadre()
        {
            int? padreSeleccionado =
                ObtenerIdPadreSeleccionado();

            List<OpcionPadreItem> elementos =
                new List<OpcionPadreItem>();

            elementos.Add(
                new OpcionPadreItem
                {
                    IdOpcionMenu = null,
                    Nivel = 0,
                    Nombre = "(Sin padre)",
                    Ruta = "(Sin padre)"
                });

            Dictionary<int, OpcionMenu> indice =
                opcionesMenu.ToDictionary(
                    x => x.IdOpcionMenu,
                    x => x);

            foreach (OpcionMenu opcion in opcionesMenu
                .Where(x => !x.EsOpcion)
                .OrderBy(x => x.Nivel)
                .ThenBy(x => x.Orden)
                .ThenBy(x => x.IdOpcionMenu))
            {
                // No puede ser padre de sí misma.
                if (idOpcionEditando.HasValue &&
                    opcion.IdOpcionMenu ==
                    idOpcionEditando.Value)
                {
                    continue;
                }

                // Tampoco puede seleccionar un descendiente suyo.
                if (idOpcionEditando.HasValue &&
                    EsDescendienteLocal(
                        idOpcionEditando.Value,
                        opcion.IdOpcionMenu))
                {
                    continue;
                }

                string ruta =
                    ConstruirRuta(
                        opcion,
                        indice);

                if (!opcion.Activo)
                {
                    ruta +=
                        " [INACTIVO]";
                }

                elementos.Add(
                    new OpcionPadreItem
                    {
                        IdOpcionMenu =
                            opcion.IdOpcionMenu,

                        Nivel =
                            opcion.Nivel,

                        Nombre =
                            opcion.Nombre,

                        Ruta =
                            ruta
                    });
            }

            cmbPadre.BeginUpdate();

            try
            {
                cmbPadre.DataSource =
                    null;

                cmbPadre.DataSource =
                    elementos;
            }
            finally
            {
                cmbPadre.EndUpdate();
            }

            SeleccionarPadre(
                padreSeleccionado);
        }

        // ============================================================
        // DESCENDENCIA LOCAL
        // ============================================================

        private bool EsDescendienteLocal(
            int idOpcionMenu,
            int idPosibleDescendiente)
        {
            int? actual =
                idPosibleDescendiente;

            HashSet<int> visitados =
                new HashSet<int>();

            while (actual.HasValue)
            {
                if (!visitados.Add(
                    actual.Value))
                {
                    return false;
                }

                if (actual.Value ==
                    idOpcionMenu)
                {
                    return true;
                }

                OpcionMenu opcion =
                    opcionesMenu.FirstOrDefault(
                        x =>
                            x.IdOpcionMenu ==
                            actual.Value);

                if (opcion == null)
                    return false;

                actual =
                    opcion.IdOpcionPadre;
            }

            return false;
        }

        private string ConstruirRuta(
            OpcionMenu opcion,
            Dictionary<int, OpcionMenu> indice)
        {
            if (opcion == null)
                return "";

            List<string> partes =
                new List<string>();

            HashSet<int> visitados =
                new HashSet<int>();

            OpcionMenu actual =
                opcion;

            while (actual != null)
            {
                if (!visitados.Add(
                    actual.IdOpcionMenu))
                {
                    partes.Add(
                        "[Referencia circular]");

                    break;
                }

                partes.Add(
                    actual.Nombre ?? "");

                if (!actual.IdOpcionPadre.HasValue)
                    break;

                OpcionMenu padre;

                if (!indice.TryGetValue(
                    actual.IdOpcionPadre.Value,
                    out padre))
                {
                    break;
                }

                actual =
                    padre;
            }

            partes.Reverse();

            return string.Join(
                " > ",
                partes);
        }

        private int? ObtenerIdPadreSeleccionado()
        {
            OpcionPadreItem item =
                cmbPadre.SelectedItem
                as OpcionPadreItem;

            return item != null
                ? item.IdOpcionMenu
                : null;
        }

        private void SeleccionarPadre(
            int? idOpcionPadre)
        {
            if (cmbPadre.Items.Count == 0)
                return;

            for (int i = 0;
                i < cmbPadre.Items.Count;
                i++)
            {
                OpcionPadreItem item =
                    cmbPadre.Items[i]
                    as OpcionPadreItem;

                if (item == null)
                    continue;

                if (item.IdOpcionMenu ==
                    idOpcionPadre)
                {
                    cmbPadre.SelectedIndex =
                        i;

                    return;
                }
            }

            cmbPadre.SelectedIndex =
                0;
        }

        // ============================================================
        // CAMBIO DE PADRE
        // ============================================================

        private void cmbPadre_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cargandoFormulario)
                return;

            ActualizarNivelYOrden();
        }

        private void ActualizarNivelYOrden()
        {
            OpcionPadreItem padre =
                cmbPadre.SelectedItem
                as OpcionPadreItem;

            if (padre == null)
            {
                txtNivel.Text = "1";
                nudOrden.Value = 1;
                return;
            }

            int nivel =
                padre.IdOpcionMenu.HasValue
                    ? padre.Nivel + 1
                    : 1;

            txtNivel.Text =
                nivel.ToString();

            int siguienteOrden =
                dbMenu.ObtenerSiguienteOrden(
                    padre.IdOpcionMenu);

            if (siguienteOrden <
                nudOrden.Minimum)
            {
                siguienteOrden =
                    Convert.ToInt32(
                        nudOrden.Minimum);
            }

            if (siguienteOrden >
                nudOrden.Maximum)
            {
                siguienteOrden =
                    Convert.ToInt32(
                        nudOrden.Maximum);
            }

            nudOrden.Value =
                siguienteOrden;
        }

        // ============================================================
        // NUEVO
        // ============================================================

        private void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            PrepararNuevo();
        }

        private void PrepararNuevo()
        {
            bool estadoAnterior =
                cargandoFormulario;

            cargandoFormulario =
                true;

            try
            {
                // ========================================================
                // MODO NUEVO
                // ========================================================

                idOpcionEditando =
                    null;

                // ========================================================
                // LIMPIAR CAMPOS
                // ========================================================

                txtNombre.Clear();

                txtClave.Clear();

                // ========================================================
                // RECARGAR PADRES
                //
                // Como ya no estamos editando, vuelven a estar disponibles
                // todos los contenedores válidos.
                // ========================================================

                CargarOpcionesPadre();

                if (cmbPadre.Items.Count > 0)
                {
                    cmbPadre.SelectedIndex =
                        0;
                }

                // ========================================================
                // VALORES PREDETERMINADOS
                // ========================================================

                txtNivel.Text =
                    "1";

                rbContenedor.Checked =
                    true;

                rbOpcion.Checked =
                    false;

                chkActivo.Checked =
                    true;

                // ========================================================
                // QUITAR SELECCIÓN DEL ÁRBOL
                // ========================================================

                tvMenu.SelectedNode =
                    null;

                // ========================================================
                // BOTONES
                // ========================================================

                btnGuardar.Text =
                    "Guardar";

                btnEliminar.Enabled =
                    false;
            }
            finally
            {
                cargandoFormulario =
                    estadoAnterior;
            }

            // Calcula el siguiente orden disponible para el padre actual.
            ActualizarNivelYOrden();

            // Dejamos el cursor listo para capturar una nueva opción.
            txtNombre.Focus();
        }

        // ============================================================
        // CLAVE AUTOMÁTICA
        // ============================================================

        private void txtNombre_Leave(
            object sender,
            EventArgs e)
        {
            // En edición no cambiamos la clave automáticamente.
            if (idOpcionEditando.HasValue)
                return;

            if (!string.IsNullOrWhiteSpace(
                txtClave.Text))
            {
                return;
            }

            txtClave.Text =
                GenerarClave(
                    txtNombre.Text);
        }

        private string GenerarClave(
            string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return "";

            string normalizado =
                texto
                .Trim()
                .ToUpperInvariant()
                .Normalize(
                    NormalizationForm.FormD);

            StringBuilder resultado =
                new StringBuilder();

            foreach (char caracter in normalizado)
            {
                UnicodeCategory categoria =
                    CharUnicodeInfo.GetUnicodeCategory(
                        caracter);

                if (categoria ==
                    UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                if (char.IsLetterOrDigit(
                    caracter))
                {
                    resultado.Append(caracter);
                    continue;
                }

                if (caracter == ' ' ||
                    caracter == '-' ||
                    caracter == '/' ||
                    caracter == '\\' ||
                    caracter == '.')
                {
                    if (resultado.Length > 0 &&
                        resultado[
                            resultado.Length - 1] != '_')
                    {
                        resultado.Append('_');
                    }
                }
            }

            string clave =
                resultado
                .ToString()
                .Trim('_');

            while (clave.Contains("__"))
            {
                clave =
                    clave.Replace(
                        "__",
                        "_");
            }

            if (clave.Length > 100)
            {
                clave =
                    clave
                    .Substring(0, 100)
                    .TrimEnd('_');
            }

            return clave;
        }

        // ============================================================
        // GUARDAR
        // ============================================================

        private void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                    return;

                OpcionPadreItem padre =
                    cmbPadre.SelectedItem
                    as OpcionPadreItem;

                int nivel;

                if (!int.TryParse(
                    txtNivel.Text,
                    out nivel))
                {
                    MessageBox.Show(
                        "No fue posible determinar el nivel de la opción.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                OpcionMenu opcion =
                    new OpcionMenu();

                if (idOpcionEditando.HasValue)
                {
                    opcion.IdOpcionMenu =
                        idOpcionEditando.Value;
                }

                opcion.IdOpcionPadre =
                    padre != null
                        ? padre.IdOpcionMenu
                        : null;

                opcion.Clave =
                    txtClave.Text
                    .Trim()
                    .ToUpperInvariant();

                opcion.Nombre =
                    txtNombre.Text.Trim();

                opcion.Nivel =
                    nivel;

                opcion.Orden =
                    Convert.ToInt32(
                        nudOrden.Value);

                opcion.EsOpcion =
                    rbOpcion.Checked;

                opcion.Activo =
                    chkActivo.Checked;

                int idGuardar;

                if (idOpcionEditando.HasValue)
                {
                    dbMenu.ActualizarOpcion(
                        opcion);

                    idGuardar =
                        opcion.IdOpcionMenu;

                    MessageBox.Show(
                        "Los cambios de \"" +
                        opcion.Nombre +
                        "\" se guardaron correctamente.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    idGuardar =
                        dbMenu.InsertarOpcion(
                            opcion);

                    MessageBox.Show(
                        "La opción \"" +
                        opcion.Nombre +
                        "\" se guardó correctamente.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                RecargarDespuesDeGuardar(
                    idGuardar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible guardar la opción." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // ELIMINAR
        // ============================================================

        private void btnEliminar_Click(
            object sender,
            EventArgs e)
        {
            // ========================================================
            // DEBE EXISTIR UNA OPCIÓN EN EDICIÓN
            // ========================================================

            if (!idOpcionEditando.HasValue)
            {
                MessageBox.Show(
                    "Seleccione una opción del menú para eliminar.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int idOpcionMenu =
                idOpcionEditando.Value;

            OpcionMenu opcion =
                opcionesMenu.FirstOrDefault(
                    x =>
                        x.IdOpcionMenu ==
                        idOpcionMenu);

            if (opcion == null)
            {
                MessageBox.Show(
                    "La opción seleccionada ya no se encuentra disponible.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ========================================================
            // NO PERMITIR ELIMINAR OPCIONES CON HIJOS
            // ========================================================

            try
            {
                if (dbMenu.TieneHijos(
                    idOpcionMenu))
                {
                    MessageBox.Show(
                        "No se puede eliminar \"" +
                        opcion.Nombre +
                        "\" porque contiene otras opciones." +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Primero debe eliminar o mover las opciones " +
                        "que dependen de ella.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible validar la opción seleccionada." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // ========================================================
            // CONFIRMACIÓN
            // ========================================================

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Desea eliminar permanentemente la opción \"" +
                    opcion.Nombre +
                    "\"?" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "También se eliminarán los permisos de usuario " +
                    "asociados a esta opción." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Esta operación no se puede deshacer." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Si solamente desea ocultar temporalmente la opción, " +
                    "desmarque \"Activo\" en lugar de eliminarla.",
                    "Eliminar opción",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

            if (respuesta !=
                DialogResult.Yes)
            {
                return;
            }

            // ========================================================
            // ELIMINAR
            // ========================================================

            try
            {
                dbMenu.EliminarOpcion(
                    idOpcionMenu);

                // ====================================================
                // RECARGAR DATOS
                // ====================================================

                cargandoFormulario =
                    true;

                try
                {
                    CargarDatos();
                }
                finally
                {
                    cargandoFormulario =
                        false;
                }

                // ====================================================
                // REGRESAR A MODO NUEVO
                // ====================================================

                PrepararNuevo();

                MessageBox.Show(
                    "La opción \"" +
                    opcion.Nombre +
                    "\" se eliminó correctamente.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No fue posible eliminar la opción." +
                    Environment.NewLine +
                    Environment.NewLine +
                    ex.Message,
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // VALIDACIÓN
        // ============================================================

        private bool ValidarDatos()
        {
            string nombre =
                txtNombre.Text.Trim();

            string clave =
                txtClave.Text
                .Trim()
                .ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la opción.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show(
                    "Ingrese la clave de la opción.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtClave.Focus();
                return false;
            }

            if (!Regex.IsMatch(
                clave,
                @"^[A-Z0-9_]+$"))
            {
                MessageBox.Show(
                    "La clave solamente puede contener letras, " +
                    "números y guion bajo (_)." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Ejemplo: DASHBOARD_VENTAS",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtClave.Focus();
                return false;
            }

            OpcionPadreItem padre =
                cmbPadre.SelectedItem
                as OpcionPadreItem;

            if (padre == null)
            {
                MessageBox.Show(
                    "Seleccione la opción padre.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbPadre.Focus();
                return false;
            }

            // --------------------------------------------------------
            // CLAVE DUPLICADA
            // --------------------------------------------------------

            bool claveDuplicada;

            if (idOpcionEditando.HasValue)
            {
                claveDuplicada =
                    dbMenu.ExisteClave(
                        clave,
                        idOpcionEditando.Value);
            }
            else
            {
                claveDuplicada =
                    dbMenu.ExisteClave(
                        clave);
            }

            if (claveDuplicada)
            {
                MessageBox.Show(
                    "Ya existe una opción del menú con la clave \"" +
                    clave +
                    "\".",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtClave.Focus();
                return false;
            }

            // --------------------------------------------------------
            // PADRE
            // --------------------------------------------------------

            if (padre.IdOpcionMenu.HasValue)
            {
                OpcionMenu opcionPadre =
                    opcionesMenu.FirstOrDefault(
                        x =>
                            x.IdOpcionMenu ==
                            padre.IdOpcionMenu.Value);

                if (opcionPadre == null)
                {
                    MessageBox.Show(
                        "La opción padre seleccionada ya no existe.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                if (opcionPadre.EsOpcion)
                {
                    MessageBox.Show(
                        "Una opción ejecutable no puede contener otras opciones.",
                        "Administración del menú",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }
            }

            // --------------------------------------------------------
            // EDICIÓN: NO PUEDE SER SU PROPIO PADRE
            // --------------------------------------------------------

            if (idOpcionEditando.HasValue &&
                padre.IdOpcionMenu.HasValue &&
                padre.IdOpcionMenu.Value ==
                idOpcionEditando.Value)
            {
                MessageBox.Show(
                    "Una opción no puede ser padre de sí misma.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // --------------------------------------------------------
            // EDICIÓN: NO PUEDE MOVERSE DEBAJO DE UN DESCENDIENTE
            // --------------------------------------------------------

            if (idOpcionEditando.HasValue &&
                padre.IdOpcionMenu.HasValue &&
                dbMenu.EsDescendiente(
                    idOpcionEditando.Value,
                    padre.IdOpcionMenu.Value))
            {
                MessageBox.Show(
                    "No puede mover esta opción dentro de uno de " +
                    "sus propios elementos descendientes.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // --------------------------------------------------------
            // CONTENEDOR CON HIJOS -> OPCIÓN EJECUTABLE
            // --------------------------------------------------------

            if (idOpcionEditando.HasValue &&
                rbOpcion.Checked &&
                dbMenu.TieneHijos(
                    idOpcionEditando.Value))
            {
                MessageBox.Show(
                    "Esta opción contiene elementos secundarios y no " +
                    "puede convertirse en una opción ejecutable." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Primero debe mover o eliminar sus elementos hijos.",
                    "Administración del menú",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        // ============================================================
        // RECARGAR
        // ============================================================

        private void RecargarDespuesDeGuardar(
            int idOpcionMenu)
        {
            cargandoFormulario =
                true;

            try
            {
                CargarDatos();

                TreeNode nodo =
                    BuscarNodoPorId(
                        tvMenu.Nodes,
                        idOpcionMenu);

                if (nodo != null)
                {
                    tvMenu.SelectedNode =
                        nodo;

                    nodo.EnsureVisible();
                }
            }
            finally
            {
                cargandoFormulario =
                    false;
            }

            // Después de guardar permanecemos editando el elemento.
            CargarOpcionParaEditar(
                idOpcionMenu);
        }

        private TreeNode BuscarNodoPorId(
            TreeNodeCollection nodos,
            int idOpcionMenu)
        {
            foreach (TreeNode nodo in nodos)
            {
                OpcionMenu opcion =
                    nodo.Tag as OpcionMenu;

                if (opcion != null &&
                    opcion.IdOpcionMenu ==
                    idOpcionMenu)
                {
                    return nodo;
                }

                TreeNode encontrado =
                    BuscarNodoPorId(
                        nodo.Nodes,
                        idOpcionMenu);

                if (encontrado != null)
                    return encontrado;
            }

            return null;
        }

        // ============================================================
        // CERRAR
        // ============================================================

        private void btnInicio_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        // ============================================================
        // MOVER VENTANA
        // ============================================================

        private void pnlEncabezado_MouseDown(
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
                new IntPtr(HT_CAPTION),
                IntPtr.Zero);
        }
    }
}