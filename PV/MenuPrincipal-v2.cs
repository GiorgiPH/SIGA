using Condominios;
using Microsoft.Office.Interop.Excel;
using PuntoVentas;
using PuntoVentas.Clases.Login;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PV
{
    public partial class MenuPrincipal_v2 : Form
    {
        public static int Opcion = 0;
        private FavoritosMenu favoritosMenu;
        public MenuPrincipal_v2()
        {
            InitializeComponent();
            pnlMenu.Width = 270;
            ConfigurarMenu();
            CrearMenu();
            lblTipoUsuario.Text = DBLogin.TipoUsuario;
            lblNombreUsuario.Text = DBLogin.usuario;
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            IntPtr wParam,
            IntPtr lParam
        );

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
        private void ModuloPrincipal_Expandido(object sender, EventArgs e)
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo = control as SidebarMenuItem;

                if (modulo != null &&
                    !object.ReferenceEquals(modulo, sender))
                {
                    modulo.Expandir(false);
                }
            }
        }
        private void LimpiarSeleccionMenu()
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo = control as SidebarMenuItem;

                if (modulo != null)
                {
                    modulo.LimpiarSeleccion();
                }
            }
        }
        private void CrearMenu()
        {
            flpMenu.Controls.Clear();
            //----------------
            SidebarMenuItem parametros = new SidebarMenuItem();
            parametros.Titulo = "PARÁMETROS";
            parametros.Margin = Padding.Empty;

            parametros.AgregarSubMenu("Datos de Empresa","DATOS_EMPRESA");
            parametros.AgregarSubMenu("Usuarios","USUARIOS");

            parametros.OpcionSeleccionada += Menu_OpcionSeleccionada;
            parametros.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;
            //----------------

            SidebarMenuItem catalogos = new SidebarMenuItem();
            catalogos.Titulo = "CATÁLOGOS";
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
            //----------------

            SidebarMenuItem inventarios = new SidebarMenuItem();
            inventarios.Titulo = "INVENTARIOS";
            inventarios.Margin = Padding.Empty;

            // sub menu de segundo nivel
            SidebarMenuItem movimientos = inventarios.AgregarSubMenu("Movimientos");

            movimientos.AgregarSubMenu("Tipo Movimientos", "TIPO_MOVIMIENTOS");
            movimientos.AgregarSubMenu("Registrar Entradas", "REGISTRAR_ENTRADAS");
            movimientos.AgregarSubMenu("Registrar Salidas", "REGISTRAR_SALIDAS");
            movimientos.AgregarSubMenu("Registrar Traspasos", "REGISTRAR_TRASPASOS");
            movimientos.AgregarSubMenu("Consulta Inventarios", "CONSULTA_INVENTARIOS");

            // sub menu de tercer nivel
            SidebarMenuItem reportesMovimientos = movimientos.AgregarSubMenu("Reportes");

            reportesMovimientos.AgregarSubMenu("Reporte existencias por almacén", "REPORTE_EXISTENCIAS_ALMACEN");
            reportesMovimientos.AgregarSubMenu("Reporte costo por producto","REPORTE_COSTO_PRODUCTO");

            inventarios.AgregarSubMenu("Inventarios Físicos", "INVENTARIOS_FISICOS");
            inventarios.AgregarSubMenu("Explosión de Material", "EXPLOSION_MATERIAL");
            inventarios.AgregarSubMenu("Reportes Inventarios", "REPORTES_INVENTARIOS");

            inventarios.OpcionSeleccionada += Menu_OpcionSeleccionada;
            inventarios.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;
            //----------------

            SidebarMenuItem compras = new SidebarMenuItem();
            compras.Titulo = "COMPRAS";
            compras.Margin = Padding.Empty;

            compras.AgregarSubMenu("Requisiciones", "REQUISICIONES");
            compras.AgregarSubMenu("Cotizaciones", "COTIZACIONES");
            compras.AgregarSubMenu("Pedidos Proveedores", "PEDIDOS_PROVEEDORES");
            compras.AgregarSubMenu("Compras Gastos", "COMPRAS_GASTOS");
            compras.AgregarSubMenu("Compras Vía Reembolso", "COMPRAS_REEMBOLSO");
            compras.AgregarSubMenu("Compras Inventariables", "COMPRAS_INVENTARIABLES");

            // sub menu de segundo nivel 
            SidebarMenuItem reportesCompras = compras.AgregarSubMenu("Reportes");
            reportesCompras.AgregarSubMenu("Diario de requisiciones", "DIARIO_REQUISICIONES");
            reportesCompras.AgregarSubMenu("Diario órdenes de compra", "DIARIO_ORDENES_COMPRA");
            reportesCompras.AgregarSubMenu("Diario de compras inventariables", "DIARIO_COMPRAS_INVENTARIABLES");
            reportesCompras.AgregarSubMenu("Diario de reembolsos", "DIARIO_REEMBOLSOS");
            reportesCompras.AgregarSubMenu("Diario de gastos", "DIARIO_GASTOS");
            reportesCompras.AgregarSubMenu("Saldo de compras", "SALDO_COMPRAS");

            // sub menu de tercer nivel
            SidebarMenuItem anticiposCompras = reportesCompras.AgregarSubMenu("Anticipos");

            //anticiposCompras.AgregarSubMenu("Anticipos", "ANTICIPOS_COMPRAS_REGISTRO");
            anticiposCompras.AgregarSubMenu("Anticipos Aplicados", "ANTICIPOS_COMPRAS_APLICADOS");

            // sub menu tercer nivel
            SidebarMenuItem proveedoresCompras = reportesCompras.AgregarSubMenu("Proveedores");

           // proveedoresCompras.AgregarSubMenu("Estado de cuenta proveedores", "ESTADO_CUENTA_PROVEEDORES");
            //proveedoresCompras.AgregarSubMenu("Saldos proveedores", "SALDOS_PROVEEDORES");
            //proveedoresCompras.AgregarSubMenu("Detalle de saldos", "DETALLE_SALDOS_PROVEEDORES");

            compras.AgregarSubMenu("Gráficas", "GRAFICAS_COMPRAS");

            compras.OpcionSeleccionada += Menu_OpcionSeleccionada;
            compras.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            //----------------
            SidebarMenuItem ventas = new SidebarMenuItem();
            ventas.Titulo = "VENTAS";
            ventas.Margin = Padding.Empty;

            ventas.AgregarSubMenu("Pedidos Clientes", "PEDIDOS_CLIENTES");
            ventas.AgregarSubMenu("Remisiones", "REMISIONES");
            ventas.AgregarSubMenu("Factura (CFDI)", "FACTURAS");
            ventas.AgregarSubMenu("Notas Crédito");
            ventas.AgregarSubMenu("Notas Cargo");

            // sub menu de segundo nivel
            SidebarMenuItem reportesVentas = ventas.AgregarSubMenu("Reportes");
            reportesVentas.AgregarSubMenu("Reporte diario de Pedidos Clientes");
            reportesVentas.AgregarSubMenu("Reporte diario de Remisiones", "REPORTE_DIARIO_REMISIONES");
            reportesVentas.AgregarSubMenu("Reporte diario Facturas", "REPORTE_DIARIO_FACTURAS");

            reportesVentas.AgregarSubMenu("Reporte diario Notas Crédito");
            reportesVentas.AgregarSubMenu("Reporte diario Notas Cargo");
            reportesVentas.AgregarSubMenu("Reporte Analítico Pedidos");
            reportesVentas.AgregarSubMenu("Reporte Analítico ventas");

            // sub menu de segundo nivel
            SidebarMenuItem graficasVentas = ventas.AgregarSubMenu("Gráficas");
            graficasVentas.AgregarSubMenu("Gráfica de Ventas Mes y Acumulado");
            graficasVentas.AgregarSubMenu("Gráfica Ventas x Prod/Servicio");
            graficasVentas.AgregarSubMenu("Gráfica Ventas x Proyecto");
            graficasVentas.AgregarSubMenu("Gráfica Ventas x Cliente");

            // sub menu de segundo nivel
            SidebarMenuItem polizasVentas = ventas.AgregarSubMenu("Pólizas");
            polizasVentas.AgregarSubMenu("Definir Pólizas");
            polizasVentas.AgregarSubMenu("Generar Pólizas");

            ventas.OpcionSeleccionada += Menu_OpcionSeleccionada;
            ventas.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;

            //----------------
            SidebarMenuItem tesoreria = new SidebarMenuItem();
            tesoreria.Titulo = "TESORERÍA";
            tesoreria.Margin = Padding.Empty;

            // sub menu de segundo nivel
            SidebarMenuItem bancos = tesoreria.AgregarSubMenu("Bancos");
            bancos.AgregarSubMenu("Catalogo Conceptos Cobro Pago", "CONCEPTOS_COBRO_PAGO");

            bancos.AgregarSubMenu("Registra Movimientos a Bancos", "REGISTRAR_MOVIMIENTOS_BANCOS");
            bancos.AgregarSubMenu("Importar Edo Cuenta");

   


            // sub menu de segundo nivel
            SidebarMenuItem cxcIngresos = tesoreria.AgregarSubMenu("CxC - Ingresos");
            cxcIngresos.AgregarSubMenu("Registra Ingresos", "REGISTRAR_INGRESO");
            cxcIngresos.AgregarSubMenu("Registra Anticipo Cliente", "REGISTRAR_ANTICIPO_CLIENTE");
            cxcIngresos.AgregarSubMenu("Aplicar Anticipos Clientes", "APLICAR_ANTICIPO_CLIENTE");
            cxcIngresos.AgregarSubMenu("Registrar Préstamos");

            // sub menu de tercer nivel
            SidebarMenuItem reportesCxc = cxcIngresos.AgregarSubMenu("Reportes CxC Ingresos");
            reportesCxc.AgregarSubMenu("Diario de Ingresos", "REPORTE_DIARIO_INGRESOS");
            reportesCxc.AgregarSubMenu("Saldos Clientes");
            reportesCxc.AgregarSubMenu("Saldos Clientes Detalle");
            reportesCxc.AgregarSubMenu("Antigüedad Saldos");
            reportesCxc.AgregarSubMenu("Estado Cuenta");
            reportesCxc.AgregarSubMenu("Anticipos de Clientes");

            // sub menu de segundo nivel
            SidebarMenuItem cxpEgresos = tesoreria.AgregarSubMenu("CxP - Egresos");
            cxpEgresos.AgregarSubMenu("Pagos por Vencimiento", "PAGOS_VENCIMIENTO");
            cxpEgresos.AgregarSubMenu("Pagos por Proveedor", "PAGOS_PROVEEDOR");
            cxpEgresos.AgregarSubMenu("Registrar Anticipo Proveedor");
            cxpEgresos.AgregarSubMenu("Aplicar Anticipo Proveedor");
            cxpEgresos.AgregarSubMenu("Préstamos");

            // sub menu de segundo nivel
            SidebarMenuItem reportesCxp = cxpEgresos.AgregarSubMenu("Reportes CxP Egresos");
            reportesCxp.AgregarSubMenu("Reporte Diario de Egresos", "REPORTE_DIARIO_EGRESOS");
            reportesCxp.AgregarSubMenu("Saldos Proveedor");
            reportesCxp.AgregarSubMenu("Saldos Proveedor Detalle");
            reportesCxp.AgregarSubMenu("Antigüedad Saldos Proveedor");
            reportesCxp.AgregarSubMenu("Estado Cuenta Proveedor");
            reportesCxp.AgregarSubMenu("Reporte Anticipos Proveedor");
            reportesCxp.AgregarSubMenu("Reporte Saldos Anticipos");
            reportesCxp.AgregarSubMenu("Reporte Saldos Préstamos");

            // sub menu de segundo nivel
            SidebarMenuItem polizasContables = tesoreria.AgregarSubMenu("Pólizas Contables");
            polizasContables.AgregarSubMenu("Define Pólizas Ingresos");
            polizasContables.AgregarSubMenu("Registra Pólizas Ingresos");
            polizasContables.AgregarSubMenu("Define Pólizas Egresos");
            polizasContables.AgregarSubMenu("Registra Pólizas Egresos");

            // sub menu de segundo nivel
            SidebarMenuItem finanzas = tesoreria.AgregarSubMenu("Finanzas");
            finanzas.AgregarSubMenu("Reporte Resultados Global", "REPORTE_RESULTADOS_GLOBAL");

            finanzas.AgregarSubMenu("Reporte Resultados CC");
            finanzas.AgregarSubMenu("Reporte Flujo Real");
            finanzas.AgregarSubMenu("Reporte Flujo Saldos");

            tesoreria.OpcionSeleccionada += Menu_OpcionSeleccionada;
            tesoreria.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;
            //----------------
            SidebarMenuItem utilerias = new SidebarMenuItem();
            utilerias.Titulo = "UTILERÍAS";
            utilerias.Margin = Padding.Empty;

            utilerias.AgregarSubMenu("Contabilidad", "CONTABILIDAD");
            utilerias.AgregarSubMenu("ODBC", "ODBC");
            utilerias.AgregarSubMenu("Correo Electrónico", "CORREO_ELECTRONICO");

            utilerias.OpcionSeleccionada += Menu_OpcionSeleccionada;
            utilerias.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;
            //----------------

            SidebarMenuItem presupuesto = new SidebarMenuItem();
            presupuesto.Titulo = "PRESUPUESTO";
            presupuesto.Margin = Padding.Empty;

            presupuesto.AgregarSubMenu("Periodos", "PERIODOS_PRESUPUESTO");
            presupuesto.AgregarSubMenu("Conceptos", "CONCEPTOS_PRESUPUESTO");
            presupuesto.AgregarSubMenu("Registrar Presupuestos", "REGISTRAR_PRESUPUESTOS");
            presupuesto.AgregarSubMenu("Crear Presupuestos", "CREAR_PRESUPUESTOS");
            presupuesto.AgregarSubMenu("Cerrar Presupuestos", "CERRAR_PRESUPUESTOS");
            presupuesto.AgregarSubMenu("Reportes", "REPORTES_PRESUPUESTO");

            presupuesto.OpcionSeleccionada += Menu_OpcionSeleccionada;
            presupuesto.ModuloPrincipalExpandido += ModuloPrincipal_Expandido;
            //----------------

            parametros.Icono = Properties.Resources.filtrar;
            catalogos.Icono = Properties.Resources.folder;
            inventarios.Icono = Properties.Resources.lista_de_verificacion;
            compras.Icono = Properties.Resources.carrito_de_compras;
            ventas.Icono = Properties.Resources.grafico_de_barras;
            tesoreria.Icono = Properties.Resources.banco;
            utilerias.Icono = Properties.Resources.renovacion;
            presupuesto.Icono = Properties.Resources.presupuesto;
         

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

            // El control necesita conocer al usuario antes de intentar
            // cargar o guardar cualquier favorito.
            favoritosMenu.UsuarioActual = DBLogin.usuario;
            favoritosMenu.CargarFavoritosUsuario();
        }

        private void flpMenu_SizeChanged(object sender, EventArgs e)
        {
            AjustarAnchoMenu();
        }

        private void AjustarAnchoMenu()
        {
            int anchoDisponible = flpMenu.Width
                - flpMenu.Padding.Horizontal
                - SystemInformation.VerticalScrollBarWidth;

            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem opcion = control as SidebarMenuItem;

                if (opcion != null)
                {
                    opcion.Width = Math.Max(
                        0,
                        anchoDisponible - opcion.Margin.Horizontal
                    );
                }

                FavoritosMenu favoritos = control as FavoritosMenu;

                if (favoritos != null)
                {
                    favoritos.Width = Math.Max(
                        0,
                        anchoDisponible - favoritos.Margin.Horizontal
                    );
                }
            }
        }

        private void Menu_OpcionSeleccionada(object sender,OpcionMenuSeleccionadaEventArgs e)
        {
            foreach (Control control in flpMenu.Controls)
            {
                SidebarMenuItem modulo = control as SidebarMenuItem;

                if (modulo != null)
                {
                    modulo.LimpiarSeleccion();
                }
            }
            LimpiarSeleccionMenu();
            e.Opcion.Seleccionado = true;

            switch (e.Opcion.Clave)
            {
                // PARÁMETROS
                case "DATOS_EMPRESA":
                    AbrirFormulario(new DatosEmpresas());
                    break;

                case "USUARIOS":
                    AbrirFormulario(new Usuarios());
                    break;

                // CATÁLOGOS
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


                // INVENTARIOS
                //    break; SUB MENU Tipo Movimientos, Registrar Entradas, Registrar Salidas, Registrar Traspasos, Consulta Inventarios, Reporte
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


                    // SUB MENU REPORTE EXISTENCIAS POR ALMACEN, REPORTE COSTO POR PRODUCTO
                        case "REPORTE_EXISTENCIAS_ALMACEN":
                            AbrirFormulario(new FiltroExistencias());
                            break;

                        case "REPORTE_COSTO_PRODUCTO":
                            AbrirFormulario(new FiltroValorProducto());
                            break;

                //case "INVENTARIOS_FISICOS":
                //    AbrirFormulario(new );
                //    break;

                //case "EXPLOSION_MATERIAL":
                //    AbrirFormulario(new ());
                //    break;

                //case "REPORTES_INVENTARIOS":
                //    AbrirFormulario(new ());
                //    break;


                // COMPRAS
                case "REQUISICIONES":
                    AbrirFormulario(new Requisicion2());
                    break;

                //case "COTIZACIONES":
                //    AbrirFormulario(new Cotizaciones());
                //    break;

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

                //SUB MENU DIARIO DE REQUISICIONES, DIARIO ORDENES COMPRA, DIARIO DE CONPRAS INVENTARIABLES, DIARIO DE REEMBOLSOS, DIARIO DE GASTOS, EGRESOS, SALDO DE COMPRAS, ANTICIPOS, PROVEEDORES
                    case "DIARIO_REQUISICIONES":
                        AbrirFormulario(new ReporteDiarioRequisicionFiltro());
                        break;

                    case "DIARIO_ORDENES_COMPRA":
                        AbrirFormulario(new ReporteDiarioOrdenesComprasFiltro());
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


                    // SUB MENU ANTICIPOS, ANTICIPOS APLICADOS
                        case "ANTICIPOS_COMPRAS_REGISTRO":
                            AbrirFormulario(new ReporteAnticipoProveedorFiltro());
                            break;

                        //case "ANTICIPOS_COMPRAS_APLICADOS":
                        //    AbrirFormulario(new ());
                        //    break;

  
                    // SUB MENU ESTADO DE CUENTA PROVEEDORES, SALDOS PROVEEDORES, DETALLE DE SALDOS
                        case "ESTADO_CUENTA_PROVEEDORES":
                            AbrirFormulario(new ReporteEstadoCuentaProveedor());
                            break;

                        //case "SALDOS_PROVEEDORES":
                        //    AbrirFormulario(new SaldosProveedores());
                        //    break;

                        //case "DETALLE_SALDOS_PROVEEDORES":
                        //    AbrirFormulario(new ());
                        //    break;

                //case "GRAFICAS_COMPRAS":
                //    AbrirFormulario(new ());
                //    break;


                // VENTAS
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
               


                // TESORERÍA
                // SUB MENU REGISTRAR MOVIMIENTOS A BANCOS, REPORTE FLUJO REAL, REPORTE FLUJO SALDOS
                case "REGISTRAR_MOVIMIENTOS_BANCOS":
                    AbrirFormulario(new RegistroMovimientoBancos());
                    break;

                //case "REPORTE_FLUJO_REAL":
                //    AbrirFormulario(new ReporteFlujoReal());
                //    break;

                //case "REPORTE_FLUJO_SALDOS":
                //    AbrirFormulario(new ReporteFlujoSaldos());
                //    break;

                case "CONCEPTOS_COBRO_PAGO":
                    AbrirFormulario(new CatalogoConceptosTesoreria());
                    break;

                // SUB MENU REGISTRAR INGRESO
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
           


                // SUB MENU  REPORTE INGRESOS, REPORTE ANTICIPO, REPORTE DIARIO EGRESOS, SALDOS PROVEEDORES, ESTADO DE CUENTA PROVEEDOR
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

                // UTILIDADES
                //case "CONTABILIDAD":
                //    AbrirFormulario(new Contabilidad());
                //    break;

                //case "ODBC":
                //    AbrirFormulario(new Odbc());
                //    break;

                //case "CORREO_ELECTRONICO":
                //    AbrirFormulario(new CorreoElectronico());
                //    break;


                // PRESUPUESTO
                case "PERIODOS_PRESUPUESTO":
                    AbrirFormulario(new CatalogoPeriodos());
                    break;

                case "CONCEPTOS_PRESUPUESTO":
                    AbrirFormulario(new ConceptosPresupuesto());
                    break;

                //case "REGISTRAR_PRESUPUESTOS":
                //    AbrirFormulario(new RegistrarPresupuestos());
                //    break;

                //case "CREAR_PRESUPUESTOS":
                //    AbrirFormulario(new CrearPresupuestos());
                //    break;

                case "CERRAR_PRESUPUESTOS":
                    AbrirFormulario(new CerrarPresupuesto()); 
                    break;

                //case "REPORTES_PRESUPUESTO":
                //    AbrirFormulario(new ());
                //    break;
            }

        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterParent;

            try
            {
                formulario.ShowDialog(this);
            }
            finally
            {
                formulario.Dispose();

                // Al cerrar la ventana flotante, elimina el marcador activo.
                LimpiarSeleccionMenu();
            }
        }

        private void pnlBarraVentana_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                
                SendMessage(
                    Handle,
                    WM_NCLBUTTONDOWN,
                    (IntPtr)HT_CAPTION,
                    IntPtr.Zero
                );
            }
        }
    }
}