using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace PuntoVentas.Clases.Usuarios
{
    public class PermisosManager
    {
        private TabControl tabControl;
        private string usuarioActual;
        private DBPermisos dbPermisos;

        // Mapeo de nombres de controles a nombres de columnas en la base de datos
        private Dictionary<string, string> columnMapping = new Dictionary<string, string>
        {
            { "ParamSwitch", "MOD_PARAMETROS" },
            { "CatSwitch", "MOD_CATALOGOS" },
            { "InvSwitch", "MOD_INVENTARIOS" },
            { "CompSwitch", "MOD_COMPRAS" },
            { "VenSwitch", "MOD_VENTAS" },
            { "TesSwitch", "MOD_TESORERIA" },
            { "PreSwitch", "MOD_PRESUPUESTO" },
            { "UtiSwitch", "MOD_UTILERIAS" },
           
            // PARAMETROS
            { "ParamDatosEmpresa", "PDatosEmpresa" },
            { "ParamUsuarios", "PUsuarios" },
                        { "CatAlmacenes", "CAlmacenes" },
                        { "CatCategoriasyFamilias", "CCategoriasYFamilias" },
            { "CatDivisas", "CDivisas" },

            { "CatProductos", "CProductos" },
            { "CatServicios", "CServicios" },
            { "CatCentroCostos", "CCentroCostos" },
            { "CatDocumentos", "CDocumentos" },
            { "CatConceptosGlobales", "CConceptosGlobales" },
            { "CatFormasPago", "CFormasdePago" },
            { "CatEmpleados", "CEmpleados" },
            { "CatTipoZonas", "CTiposZonas" },
            { "CatClientes", "CClientes" },
            { "CatProveedores", "CProveedores" },
            { "CatCuentasBancarias", "CcuentasBancarias" },
            
            // INVENTARIOS
            { "InvTiposMovimientos", "ITipoMovimientos" },
            { "InvRegistrarEntradas", "IRegistrarEntradas" },
            { "InvRegistrarSalidas", "IRegistrarSalidas" },
            { "InvRegistrarTraspasos", "IRegistrarTranspasos" },
            { "InvConsultarInventarios", "IConsultarInventarios" },
            { "InvReportes", "IReportes" },
            
            // COMPRAS
            { "CompRequisiciones", "CORequisiciones" },
            { "CompCotizaciones", "COCotizaciones" },
            { "CompPedidosProveedores", "COPedidosProveedores" },
            { "CompCompras", "COCompras" },
            { "CompNotasCRyCA", "CONotasdeCRCA" },
            { "CompReportes", "COReportes" },
            { "CompDefinePoliza", "CODefinePoliza" },
            { "CompGeneraPoliza", "COGeneraPoliza" },
            
            // VENTAS
            { "venCotizaciones", "VCotizaciones" },
            { "venPedidosClientes", "VPedidosClientes" },
            { "venRemisiones", "VRemisiones" },
            { "venCFDI", "VCFDI" },
            { "venNotasCRyCA", "VNotasdeCRCA" },
            { "venReportes", "VReportes" },
            { "venRegistrarCobranza", "VRegistrarCobranza" }
        };

        public PermisosManager(TabControl tabControl, string usuarioActual)
        {
            this.tabControl = tabControl;
            this.usuarioActual = usuarioActual;
            this.dbPermisos = new DBPermisos();
        }

        /// <summary>
        /// Carga los permisos desde la base de datos y los aplica a los controles
        /// </summary>
        public void CargarPermisos()
        {
            try
            {
                var permisos = dbPermisos.CargarPermisosUsuario(usuarioActual);

                // Iterar por cada TabPage en el TabControl
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    // Buscar todos los Guna2ToggleSwitch en este TabPage
                    var switches = EncontrarSwitchesEnContenedor(tabPage);

                    foreach (var toggleSwitch in switches)
                    {
                        string nombreControl = toggleSwitch.Name;
                        
                        // Obtener el nombre de la columna en la base de datos
                        string nombreColumna = ObtenerNombreColumna(nombreControl);

                        if (permisos.ContainsKey(nombreColumna))
                        {
                            string valor = permisos[nombreColumna];
                            toggleSwitch.Checked = (valor == "activo");

                            // Actualizar el label correspondiente si existe
                            ActualizarLabelEstado(tabPage, nombreControl, valor);
                        }
                        else
                        {
                            // Si no existe en la BD, establecer como inactivo
                            toggleSwitch.Checked = false;
                            ActualizarLabelEstado(tabPage, nombreControl, "inactivo");
                        }
                    }

                    // Configurar el evento para el switch principal del módulo (si existe)
                    ConfigurarSwitchPrincipal(tabPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.Message);
            }
        }

        /// <summary>
        /// Guarda los permisos actuales en la base de datos
        /// </summary>
        /// <returns>Mensaje de resultado</returns>
        public string GuardarPermisos()
        {
            try
            {
                var permisos = new Dictionary<string, string>();

                // Recopilar el estado de todos los switches
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    var switches = EncontrarSwitchesEnContenedor(tabPage);

                    foreach (var toggleSwitch in switches)
                    {
                        string estado = toggleSwitch.Checked ? "activo" : "inactivo";
                        string nombreColumna = ObtenerNombreColumna(toggleSwitch.Name);
                        permisos[nombreColumna] = estado;
                    }
                }

                // Guardar en la base de datos
                return dbPermisos.GuardarPermisosUsuario(usuarioActual, permisos);
            }
            catch (Exception ex)
            {
                return "Error al guardar permisos: " + ex.Message;
            }
        }

        /// <summary>
        /// Obtiene el nombre de la columna en la base de datos para un control
        /// </summary>
        /// <param name="nombreControl">Nombre del control</param>
        /// <returns>Nombre de la columna en la base de datos</returns>
        private string ObtenerNombreColumna(string nombreControl)
        {
            // Si existe en el mapeo, usar el nombre de columna mapeado
            if (columnMapping.ContainsKey(nombreControl))
            {
                return columnMapping[nombreControl];
            }
            
            // Si no existe en el mapeo, usar el nombre del control como está
            return nombreControl;
        }

        /// <summary>
        /// Encuentra todos los Guna2ToggleSwitch en un contenedor
        /// </summary>
        private List<Guna2ToggleSwitch> EncontrarSwitchesEnContenedor(Control contenedor)
        {
            var switches = new List<Guna2ToggleSwitch>();

            foreach (Control control in contenedor.Controls)
            {
                if (control is Guna2ToggleSwitch toggleSwitch)
                {
                    switches.Add(toggleSwitch);
                }
                else if (control.HasChildren)
                {
                    // Buscar recursivamente en controles hijos
                    switches.AddRange(EncontrarSwitchesEnContenedor(control));
                }
            }

            return switches;
        }

        /// <summary>
        /// Actualiza el label que muestra el estado de un permiso
        /// </summary>
        private void ActualizarLabelEstado(Control contenedor, string nombreSwitch, string estado)
        {
            // Buscar un label cuyo nombre coincida con el patrón "lbl" + nombreSwitch
            string nombreLabel = "lbl" + nombreSwitch;

            foreach (Control control in contenedor.Controls)
            {
                if (control.Name == nombreLabel && control is Guna.UI2.WinForms.Guna2HtmlLabel label)
                {
                    label.Text = estado;
                    return;
                }
                else if (control.HasChildren)
                {
                    ActualizarLabelEstado(control, nombreSwitch, estado);
                }
            }
        }

        /// <summary>
        /// Configura el evento para el switch principal del módulo
        /// </summary>
        private void ConfigurarSwitchPrincipal(TabPage tabPage)
        {
            // Buscar el switch principal (que termina con "Switch" y no tiene prefijo de módulo específico)
            var switches = EncontrarSwitchesEnContenedor(tabPage);

            foreach (var toggleSwitch in switches)
            {
                if (toggleSwitch.Name.EndsWith("Switch") && toggleSwitch.Name.Length > 6)
                {
                    // Verificar si es un switch principal (no tiene otro prefijo conocido)
                    string nombre = toggleSwitch.Name.ToLower();
                    if (nombre == "paramswitch" || nombre == "catswitch" || nombre == "invswitch" || 
                        nombre == "compswitch1" || nombre == "venswitch" || nombre == "tesswitch" || nombre == "preswitch" || nombre == "utiswitch"	)
                    {
                        // Remover eventos anteriores para evitar duplicados
                        toggleSwitch.CheckedChanged -= SwitchPrincipal_CheckedChanged;
                        toggleSwitch.CheckedChanged += SwitchPrincipal_CheckedChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia un switch principal
        /// </summary>
        private void SwitchPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            var switchPrincipal = sender as Guna2ToggleSwitch;
            if (switchPrincipal == null) return;

            // Encontrar el TabPage padre
            TabPage tabPage = EncontrarTabPagePadre(switchPrincipal);
            if (tabPage == null) return;

            // Obtener todos los switches secundarios en este TabPage
            var switchesSecundarios = ObtenerSwitchesSecundarios(tabPage, switchPrincipal.Name);

            // Aplicar el estado del switch principal a todos los secundarios
            foreach (var switchSecundario in switchesSecundarios)
            {
                switchSecundario.Checked = switchPrincipal.Checked;

                // Actualizar el label correspondiente
                ActualizarLabelEstado(tabPage, switchSecundario.Name, 
                    switchPrincipal.Checked ? "activo" : "inactivo");
            }
        }

        /// <summary>
        /// Encuentra el TabPage que contiene un control
        /// </summary>
        private TabPage EncontrarTabPagePadre(Control control)
        {
            Control parent = control.Parent;
            while (parent != null)
            {
                if (parent is TabPage tabPage)
                {
                    return tabPage;
                }
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// Obtiene todos los switches secundarios en un TabPage (excluyendo el principal)
        /// </summary>
        private List<Guna2ToggleSwitch> ObtenerSwitchesSecundarios(TabPage tabPage, string nombreSwitchPrincipal)
        {
            var switches = EncontrarSwitchesEnContenedor(tabPage);
            var switchesSecundarios = new List<Guna2ToggleSwitch>();

            foreach (var toggleSwitch in switches)
            {
                if (toggleSwitch.Name != nombreSwitchPrincipal)
                {
                    switchesSecundarios.Add(toggleSwitch);
                }
            }

            return switchesSecundarios;
        }

        /// <summary>
        /// Configura eventos para todos los switches secundarios
        /// </summary>
        public void ConfigurarEventosSwitchesSecundarios()
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                var switches = EncontrarSwitchesEnContenedor(tabPage);

                foreach (var toggleSwitch in switches)
                {
                    // Solo configurar switches que no son principales
                    string nombre = toggleSwitch.Name.ToLower();
                    if (!(nombre == "paramswitch" || nombre == "catswitch1" || nombre == "invswitch1" || 
                          nombre == "compswitch1" || nombre == "venswitch"))
                    {
                        // Remover eventos anteriores para evitar duplicados
                        toggleSwitch.CheckedChanged -= SwitchSecundario_CheckedChanged;
                        toggleSwitch.CheckedChanged += SwitchSecundario_CheckedChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia un switch secundario
        /// </summary>
        private void SwitchSecundario_CheckedChanged(object sender, EventArgs e)
        {
            var switchSecundario = sender as Guna2ToggleSwitch;
            if (switchSecundario == null) return;

            // Encontrar el TabPage padre
            TabPage tabPage = EncontrarTabPagePadre(switchSecundario);
            if (tabPage == null) return;

            // Actualizar el label correspondiente
            string estado = switchSecundario.Checked ? "activo" : "inactivo";
            ActualizarLabelEstado(tabPage, switchSecundario.Name, estado);
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        public void CerrarConexion()
        {
            dbPermisos.CerrarConexion();
        }
    }
}
