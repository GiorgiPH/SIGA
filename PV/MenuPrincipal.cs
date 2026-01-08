using System;
using System.Windows.Forms;
using Condominios;
using PuntoVentas.Clases.Login;
using PV;
using ControlAcademico;
using PuntoVentas;
using MEDCON;
using PV.Clases.Respaldo;
using System.Drawing;
using System.Collections.Generic;
using PuntoVentas.Clases.Usuarios;

namespace PuntoVentas
{
    public partial class MenuPrincipal : Form
    {
        DBLogin c = new DBLogin();
        DBRespaldo R = new DBRespaldo();
        DBPermisos p = new DBPermisos();
        public static int Opcion = 0;
        public static int Avisos = 0;
        static string Seleccion = string.Empty;
        private Dictionary<string, string> _permisos;

        public MenuPrincipal()
        {
            InitializeComponent();
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Renderer = new customProfessionalRender();
            lblTipoUsuario.Text = DBLogin.TipoUsuario;
            lblUsuario.Text = DBLogin.usuario;
            btnParametros.DropDownDirection = ToolStripDropDownDirection.Right;
            btnInventarios.DropDownDirection = ToolStripDropDownDirection.Right;
            btnCatalogos.DropDownDirection = ToolStripDropDownDirection.Right;
            btnCompras.DropDownDirection = ToolStripDropDownDirection.Right;
            btnPresupuesto.DropDownDirection = ToolStripDropDownDirection.Right;
            btnTesoreria.DropDownDirection = ToolStripDropDownDirection.Right;
            btnUtilerias.DropDownDirection = ToolStripDropDownDirection.Right;
            btnVentas.DropDownDirection = ToolStripDropDownDirection.Right;
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Renderer = new customProfessionalRender();




        }



        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            c.empresa();
            _permisos = p.CargarPermisosUsuario(DBLogin.usuario);
            AplicarPermisosMenu();
        }

        private void AplicarPermisosMenu()
        {
            foreach (var permiso in _permisos)
            {
                bool habilitado = permiso.Value.ToLower() == "activo";

                switch (permiso.Key)
                {
                    case "MOD_PARAMETROS":
                        BtnParametros1.Enabled = habilitado;
                        BtnParametros2.Enabled = habilitado;
                        break;

                    case "MOD_CATALOGOS":
                        btncatalogos1.Enabled = habilitado;
                        btncatalogos2.Enabled = habilitado;
                        break;

                    case "MOD_INVENTARIOS":
                        btnInventario.Enabled = habilitado;
                        BtnInventario1.Enabled = habilitado;
                        break;

                    case "MOD_COMPRAS":
                        btnCompras1.Enabled = habilitado;
                        btnCompras2.Enabled = habilitado;
                        break;

                    case "MOD_VENTAS":
                        btnventas1.Enabled = habilitado;
                        btnventas2.Enabled = habilitado;
                        break;

                    // 🔽 NUEVOS PERMISOS DE VENTAS 🔽

                    case "VRegistrarCobranza":
                        btnRegistrarINgresos.Enabled = habilitado;
                        break;

                    case "VPedidosClientes":
                        btnPedidos.Enabled = habilitado;
                        break;

                    case "VRemisiones":
                        btnRemisiones.Enabled = habilitado;
                        break;


                    case "VReportes":
                        btnReporteAnticipos.Enabled = habilitado;
                        btnReporteIngresos.Enabled = habilitado;

                        btnReporteUtilidadPedido.Enabled = habilitado;

                        btnReporteUtilidadProducto.Enabled = habilitado;
                        btnReporteRemisiones.Enabled = habilitado;



                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Hoy = DateTime.Today.ToString();
            DateTime FechaSalida = Convert.ToDateTime(Hoy);

            string HoraSalida = DateTime.Now.ToString("HH");
            string MinutoSalida = DateTime.Now.ToString("mm");
            string SegundoSalida = DateTime.Now.ToString("ss tt");
            string Salida = HoraSalida + ":" + MinutoSalida + ":" + SegundoSalida;

            c.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);

            PuntoVentas.Opcion = 0;
            PuntoVentas med = new PuntoVentas();
            med.Show();
            this.Hide();
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            string Hoy = DateTime.Today.ToString();
            DateTime FechaSalida = Convert.ToDateTime(Hoy);

            string HoraSalida = DateTime.Now.ToString("HH");
            string MinutoSalida = DateTime.Now.ToString("mm");
            string SegundoSalida = DateTime.Now.ToString("ss tt");
            string Salida = HoraSalida + ":" + MinutoSalida + ":" + SegundoSalida;

            c.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);

            PuntoVentas.Opcion = 0;

            Application.Exit();
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DatosEmpresas datosEmpresas = new DatosEmpresas();
            datosEmpresas.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.ShowDialog();
        }

        private void divisasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoDivisa catalogoDivisa = new CatalogoDivisa();
            catalogoDivisa.ShowDialog();
        }

        private void categoriasYFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoFamilias catalogoFamilias = new CatalogoFamilias();
            catalogoFamilias.ShowDialog();
        }

        private void productosYServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Consulta = 0;
            CatalogoProductosServicios catalogoProductosServicios = new CatalogoProductosServicios(Consulta);
            catalogoProductosServicios.ShowDialog();
        }

        private void formasDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoFormasPago catalogoFormasPago = new CatalogoFormasPago();
            catalogoFormasPago.ShowDialog();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoPersonal catalogoPersonal = new CatalogoPersonal();
            catalogoPersonal.ShowDialog();
        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Documentos documento = new Documentos();
            documento.ShowDialog();
        }

        private void conceptosGlobalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConceptosGlobales conceptosGlobales = new ConceptosGlobales();
            conceptosGlobales.ShowDialog();
        }

        private void centrosDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CentroCostos centroCostos = new CentroCostos();
            centroCostos.ShowDialog();
        }

        private void tiposYZonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TiposZonas tiposZonas = new TiposZonas();
            tiposZonas.ShowDialog();
        }

        private void condominiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoCondominios catalogoCondominios = new CatalogoCondominios();
            catalogoCondominios.ShowDialog();
        }

        private void areasComunesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoAreasComunes catalogoAreasComunes = new CatalogoAreasComunes();
            catalogoAreasComunes.ShowDialog();
        }

        private void propietariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Propietarios propietarios = new Propietarios();
            propietarios.ShowDialog();
        }

        private void conceptosIngresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConceptosIngresos conceptosIngresos = new ConceptosIngresos();
            conceptosIngresos.ShowDialog();
        }

        private void generarRecibosAutomaticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReciboAutomaticos reciboAutomaticos = new ReciboAutomaticos();
            reciboAutomaticos.ShowDialog();
        }

        private void generarRecibosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerarRecibo generarRecibo = new GenerarRecibo();
            generarRecibo.ShowDialog();
        }

        private void registrarCobranzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registroIngresos registroIngresos = new registroIngresos("","");
            registroIngresos.ShowDialog();
        }

        private void consultaDatosCondominioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaCondominio consultaCondominio = new ConsultaCondominio();
            consultaCondominio.ShowDialog();
        }

        private void ocupacionCondominioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcupacionCondominio consultaCondominio = new OcupacionCondominio();
            consultaCondominio.ShowDialog();
        }

        private void controlDeActivoFijoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivoFijo activoFijo = new ActivoFijo();
            activoFijo.ShowDialog();
        }

        private void resguardoDeActivoFijoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResguardoActivoFijo resguardoActivoFijo = new ResguardoActivoFijo();
            resguardoActivoFijo.ShowDialog();
        }

        private void activoFijoDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteActivoFijo2 reporteActivoFijo2 = new ReporteActivoFijo2();
            reporteActivoFijo2.ShowDialog();
        }

        private void resguardoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteResguardoActivoFijo2 reporteResguardoActivoFijo2 = new ReporteResguardoActivoFijo2();
            reporteResguardoActivoFijo2.ShowDialog();
        }

        private void saldoPorPropietarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteSaldoPropietarioFiltro reporteSaldoPropietario = new ReporteSaldoPropietarioFiltro();
            reporteSaldoPropietario.ShowDialog();
        }

        private void saldoPorPropiedadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteSaldoCondominioFiltro reporteSaldoCondominio = new ReporteSaldoCondominioFiltro();
            reporteSaldoCondominio.ShowDialog();
        }

        private void saldoDetalladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteSaldoDetalladoFiltro reporteSaldosDetallados = new ReporteSaldoDetalladoFiltro();
            reporteSaldosDetallados.ShowDialog();
        }

        private void catalagoClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void disponibilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParametrosGenerales parametrosGenerales = new ParametrosGenerales();
            parametrosGenerales.ShowDialog();
        }

        private void reservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarCitas registrarCitas = new RegistrarCitas();
            registrarCitas.ShowDialog();
        }

        private void cancelarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelarCitas cancelar = new CancelarCitas();
            cancelar.ShowDialog();
        }

        private void MenuPrincipal_Activated(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                ConsultarFecha fecha = new ConsultarFecha();
                fecha.ShowDialog();
            }
            else if (Opcion == 2)
            {
                ConsultarNombre nombre = new ConsultarNombre();
                nombre.ShowDialog();
            }
        }

        private void consultaReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaFechaNombre consultaFechaNombre = new ConsultaFechaNombre();
            consultaFechaNombre.ShowDialog();
        }

        private void avisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Avisos avisos = new Avisos();
            avisos.ShowDialog();
        }

        private void ingresosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReporteIngresoFormulario reporteIngresos = new ReporteIngresoFormulario();
            reporteIngresos.ShowDialog();
        }

        private void estadoDeCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteEstadoCuentaFormulario reporteEstadoCuentaFormulario = new ReporteEstadoCuentaFormulario();
            reporteEstadoCuentaFormulario.ShowDialog();
        }

        private void almacenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Almacenes almacenes = new Almacenes();
            almacenes.ShowDialog();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            proveedores.ShowDialog();
        }

        private void tipoMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoMovimientos mov = new TipoMovimientos();

            mov.StartPosition = FormStartPosition.Manual;
            mov.Left = 260;
            mov.Top = 80;
            mov.ShowDialog();

        
            
        }

        private void registrarEntradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string movimiento = "E";
            RegistrarEntrada2 entrada = new RegistrarEntrada2(movimiento);
            entrada.StartPosition = FormStartPosition.Manual;
            entrada.Left = 260;
            entrada.Top = 80;
         
            entrada.ShowDialog();
        }

        private void registrarSalidaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string movimiento = "S";
            RegistrarEntrada2 sald = new RegistrarEntrada2(movimiento);
            sald.StartPosition = FormStartPosition.Manual;
            sald.Left = 260;
            sald.Top = 80;
            sald.ShowDialog();
        }

        private void registrarTraspasosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string movimiento = "T";
            RegistrarEntrada2 tras = new RegistrarEntrada2(movimiento);
            tras.StartPosition = FormStartPosition.Manual;
            tras.Left = 260;
            tras.Top = 80;
            tras.ShowDialog();
        }

        private void consultaInventariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Consulta = 0;
            ConsultaInventario2 consulta = new ConsultaInventario2(Consulta);
            consulta.StartPosition = FormStartPosition.Manual;
            consulta.Left = 280;
            consulta.Top = 80;
            consulta.ShowDialog();
        }

        private void ordenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdenCompra ordenCompra = new OrdenCompra();
            ordenCompra.ShowDialog();
        }

        private void recepcionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecepcionProductos2 recepcionProductos = new RecepcionProductos2();
            recepcionProductos.ShowDialog();
        }

        private void registroDeGastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroGastos registroGastos = new RegistroGastos();
            registroGastos.ShowDialog();
        }

        private void egresosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegistroEgreso registroEgreso = new RegistroEgreso();
            registroEgreso.ShowDialog();
        }

        private void cuentasBancariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CuentasBancarias cuentasBancarias = new CuentasBancarias();
            cuentasBancarias.ShowDialog();
        }

        private void requisicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Requisicion requisicion = new Requisicion();
            requisicion.ShowDialog();
        }

        private void reporteKardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroKardex filtroKardex = new FiltroKardex();
            filtroKardex.ShowDialog();
        }

        private void reporteExistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroExistencias filtroExistencias = new FiltroExistencias();
            filtroExistencias.ShowDialog();
        }

        private void reporteCostoPorProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroValorProducto filtroValorProducto = new FiltroValorProducto();
            filtroValorProducto.ShowDialog();
        }

        private void notasDeCargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotasCargo notasCargo = new NotasCargo();
            notasCargo.ShowDialog();
        }

        private void reporteDiarioDeOrdenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteDiarioOrdenesComprasFiltro reporteDiarioOrdenesCompra = new ReporteDiarioOrdenesComprasFiltro();
            reporteDiarioOrdenesCompra.ShowDialog();
        }

        private void reporteDiarioDeRequisicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteDiarioRequisicionFiltro reporteDiarioRequisicion = new ReporteDiarioRequisicionFiltro();
            reporteDiarioRequisicion.ShowDialog();
        }

        private void reporteDiarioDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteDiarioComprasFiltro reporteDiarioCompras = new ReporteDiarioComprasFiltro();
            reporteDiarioCompras.ShowDialog();
        }

        private void reporteDiarioNotasDeCargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteDiarioNotasCargoFiltro reporteDiarioNotasCargo = new ReporteDiarioNotasCargoFiltro();
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void reporteDeEgresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteEgresosFiltro reporteEgresos = new ReporteEgresosFiltro();
            reporteEgresos.ShowDialog();
        }

        private void reporteSaldoDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteComprasFiltro reporteCompras = new ReporteComprasFiltro();
            reporteCompras.ShowDialog();
        }

        private void estadoDeCuentaProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteEstadoCuentaProveedor estadoCuentaProveedor = new ReporteEstadoCuentaProveedor();
            estadoCuentaProveedor.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void base0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatalogoPeriodos catalogoPeriodos = new CatalogoPeriodos();
            catalogoPeriodos.ShowDialog();
        }

        private void historicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConceptosPresupuesto conceptosPresupuesto = new ConceptosPresupuesto();
            conceptosPresupuesto.ShowDialog();
        }

        private void ingresosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RegistroPresupuesto registroPresupuesto = new RegistroPresupuesto("Ingreso");
            registroPresupuesto.ShowDialog();
        }

        private void egresosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RegistroPresupuesto registroPresupuesto = new RegistroPresupuesto("Egreso");
            registroPresupuesto.ShowDialog();
        }

        private void ingreosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresupuestoHistorico registroPresupuesto = new PresupuestoHistorico("Ingreso");
            registroPresupuesto.ShowDialog();
        }

        private void egresosToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PresupuestoHistorico registroPresupuesto = new PresupuestoHistorico("Egreso");
            registroPresupuesto.ShowDialog();
        }

        private void cerrarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarPresupuesto cerrarPresupuesto = new CerrarPresupuesto();
            cerrarPresupuesto.ShowDialog();
        }

        private void registrarAnticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarAnticipo registrarAnticipo = new RegistrarAnticipo("Propietario");
            registrarAnticipo.ShowDialog();
        }

        private void registrarAnticipoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegistrarAnticipo registrarAnticipo = new RegistrarAnticipo("Proveedor");
            registrarAnticipo.ShowDialog();
        }

        private void aplicarAnticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarAnticipo aplicarAnticipo = new AplicarAnticipo();
            aplicarAnticipo.ShowDialog();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            R.ruta();
            MessageBox.Show( R.Respaldo());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void ocupaciónENtradasYSalidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltroEntradaSalidaCond filtroEntradaSalidaCond = new FiltroEntradaSalidaCond();
            filtroEntradaSalidaCond.ShowDialog();
        }

        private void aplicarAnticipoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AplicarAnticipoProveedor aplicarAnticipoProveedor = new AplicarAnticipoProveedor();
            aplicarAnticipoProveedor.ShowDialog();
        }

        private void anticiposAplicadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteAnticiposAplicadosFiltro reporteAnticiposAplicados = new ReporteAnticiposAplicadosFiltro();
            reporteAnticiposAplicados.ShowDialog();
        }

        private void anticiposToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ReporteAnticiposFiltro reporteAnticipos = new ReporteAnticiposFiltro();
            reporteAnticipos.ShowDialog();
        }

        private void saldoDetalladoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteSaldoDetalladoFiltro reporteSaldoDetalladoFiltro = new ReporteSaldoDetalladoFiltro();
            reporteSaldoDetalladoFiltro.ShowDialog();
        }

        private void anticiposToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ReporteAnticipoProveedorFiltro reporteAnticipoProveedorFiltro = new ReporteAnticipoProveedorFiltro();
            reporteAnticipoProveedorFiltro.ShowDialog();
        }

        private void anticiposAplicadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReporeteAnticiposProveedorAplicadosFiltro reporeteAnticiposProveedorAplicadosFiltro = new ReporeteAnticiposProveedorAplicadosFiltro();
            reporeteAnticiposProveedorAplicadosFiltro.ShowDialog();
        }

        private void estadoDeCuentaProveedoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteEstadoCuentaProveedor estadoCuentaProveedor = new ReporteEstadoCuentaProveedor();
            estadoCuentaProveedor.ShowDialog();
        }

        private void saldosProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteSaldoProveedorFiltro reporteSaldosProveedores = new ReporteSaldoProveedorFiltro();
            reporteSaldosProveedores.ShowDialog();
        }

        private void detallesDeSaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteSaldoDetalladoProveedoresFiltro reporteSaldoDetalladoProveedoresFiltro = new ReporteSaldoDetalladoProveedoresFiltro();
            reporteSaldoDetalladoProveedoresFiltro.ShowDialog();
        }

        private void notasDeCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ingresosToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GraficasIngresos graficasIngresos = new GraficasIngresos();
            graficasIngresos.ShowDialog();
        }

        private void almacenesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Almacenes almacenes = new Almacenes();
            almacenes.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void proveedoresToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            proveedores.ShowDialog();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tipoMovimientosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TipoMovimientos mov = new TipoMovimientos();
            mov.ShowDialog();
        }

        private void registrarEntradasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string movimiento = "E";
            RegistrarEntrada entrada = new RegistrarEntrada(movimiento);
            entrada.ShowDialog();
        }

        private void registrarSalidaToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            string movimiento = "S";
            RegistrarEntrada sald = new RegistrarEntrada(movimiento);
            sald.ShowDialog();
        }

        private void registrarTraspasosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string movimiento = "T";
            RegistrarEntrada2 tras = new RegistrarEntrada2(movimiento);
            tras.StartPosition = FormStartPosition.Manual;
            tras.Left = 280;
            tras.Top = 80;
            tras.ShowDialog();
        }

        private void consultaInventariosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int Consulta = 0;
            ConsultaInventario consulta = new ConsultaInventario(Consulta);
            consulta.ShowDialog();
        }

        private void reporteKardexToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FiltroKardex filtroKardex = new FiltroKardex();
            filtroKardex.ShowDialog();
        }

        private void reporteExistenciasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FiltroExistencias filtroExistencias = new FiltroExistencias();
            filtroExistencias.ShowDialog();
        }

        private void reporteCostoPorProductosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FiltroValorProducto filtroValorProducto = new FiltroValorProducto();
            filtroValorProducto.ShowDialog();
        }

        private void requisicionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            /*Requisicion requisicion = new Requisicion();
            requisicion.ShowDialog();
            */
            Requisicion2 requisicion = new Requisicion2();
            requisicion.ShowDialog();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OrdenCompra2 ordenCompra = new OrdenCompra2();
            //    ordenCompra.ShowDialog();
            RegistroGastos2 RG = new RegistroGastos2();
            RG.ShowDialog();
        }

        private void notasDeCrYCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotasCargo2 notasCargo = new NotasCargo2();
            notasCargo.ShowDialog();
        }

        private void reporteDiarioDeRequisicionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteDiarioRequisicionFiltro reporteDiarioRequisicion = new ReporteDiarioRequisicionFiltro();
            reporteDiarioRequisicion.ShowDialog();
        }

        private void reporteDiarioDeOrdenesDeCompraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteDiarioOrdenesComprasFiltro reporteDiarioOrdenesCompra = new ReporteDiarioOrdenesComprasFiltro();
            reporteDiarioOrdenesCompra.ShowDialog();
        }

        private void reporteDiarioDeComprasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteDiarioComprasFiltro reporteDiarioCompras = new ReporteDiarioComprasFiltro();
            reporteDiarioCompras.ShowDialog();
        }

        private void reporteDiarioNotasDeCargoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteDiarioNotasCargoFiltro reporteDiarioNotasCargo = new ReporteDiarioNotasCargoFiltro();
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void reporteDeEgresosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteEgresosFiltro reporteEgresos = new ReporteEgresosFiltro();
            reporteEgresos.ShowDialog();
        }

        private void reporteSaldoDeComprasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReporteComprasFiltro reporteCompras = new ReporteComprasFiltro();
            reporteCompras.ShowDialog();
        }

        private void anticiposToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void anticiposToolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            ReporteAnticipoProveedorFiltro reporteAnticipoProveedorFiltro = new ReporteAnticipoProveedorFiltro();
            reporteAnticipoProveedorFiltro.ShowDialog();
        }

        private void anticiposAplicadosToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ReporeteAnticiposProveedorAplicadosFiltro reporeteAnticiposProveedorAplicadosFiltro = new ReporeteAnticiposProveedorAplicadosFiltro();
            reporeteAnticiposProveedorAplicadosFiltro.ShowDialog();
        }

        private void estadoDeCuentaProveedoresToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            ReporteEstadoCuentaProveedor estadoCuentaProveedor = new ReporteEstadoCuentaProveedor();
            estadoCuentaProveedor.ShowDialog();
        }

        private void remisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void periodicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

        }

        private void btnParametros_Click(object sender, EventArgs e)
        {

        }

        private void inventariosFisicosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Consulta = 0;
            CatalogoServicios catalogoServicios = new CatalogoServicios(Consulta);
            catalogoServicios.ShowDialog();
        }

        private void comprasRegistroDeGastosServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroGastos Rg = new RegistroGastos();
            Rg.ShowDialog();
        }

        private void anticiposToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {

        }

        private void anticiposAplicadosToolStripMenuItem1_Click_2(object sender, EventArgs e)
        {

        }

        private void notasDeCargosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCatalogos_Click(object sender, EventArgs e)
        {

        }

        private void DefinepolizaStripMenuItem7_Click(object sender, EventArgs e)
        {
            DefinePoliza DP = new DefinePoliza("Definiciones Compras");
            DefinePoliza.TipopolizaCompras = "Compras";
            DP.ShowDialog();
        }

        private void pedidosProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdenCompra2 ordenCompra = new OrdenCompra2();
            ordenCompra.ShowDialog();
        }

        private void comprasRegistroDeGastosServicioToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RegistroGastos registroGastos = new RegistroGastos();
            registroGastos.ShowDialog();
        }

        private void comprasRecepciónEntradaProductosAlmacénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecepcionProductos2 recepcionProductos = new RecepcionProductos2();
            recepcionProductos.ShowDialog();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void GenerePolizaStripMenuItem7_Click(object sender, EventArgs e)
        {

            GENERARPOLIZAS GP = new GENERARPOLIZAS("Polizas Compras");
            GENERARPOLIZAS.TipopolizaCompras = "Compras";
            GP.ShowDialog();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (GruopParametros.Visible == true)
            {
                GruopParametros.Visible = false;

            }else
            {
                GruopParametros.Visible = true;
                    }
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (GroupCatalogo.Visible == true)           
            {
               
                GroupCatalogo.Visible = false;


                /*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);

            }
            else
            {
                guna2Panel5.AutoSize = false;
                guna2Panel5.VerticalScroll.Value = 0;
                /*oculta las otrasopciones*/
                GruopParametros.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                guna2Panel5.Controls.Remove(GruopParametros);
                guna2Panel5.Controls.Remove(Grupoinventarios);
                guna2Panel5.Controls.Remove(GrupoCompras);
                guna2Panel5.Controls.Remove(GrupoVentas);
                guna2Panel5.Controls.Remove(GrupoTesoreria);
                guna2Panel5.Controls.Remove(GrupoPresupuesto);
                guna2Panel5.Controls.Remove(GrupoUtilerias);


                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);

                //-------------------------------------//

                guna2Panel5.Controls.Add(GroupCatalogo);
                

                GroupCatalogo.Visible = true;
                GroupCatalogo.Location = new Point(9, 114);
                /*Coloca en la posición Final los botones, cuando se consulta el grupo de catalogos*/
                btnInventario.Location = new Point(9, 769);
                BtnInventario1.Location = new Point(165, 769);
                btnCompras1.Location = new Point(9, 824);
                btnCompras2.Location = new Point(165, 824);
                btnventas1.Location = new Point(9, 879);
                btnventas2.Location = new Point(165, 879);

                btnTesoreria1.Location = new Point(9, 934);
                btnTesoreria2.Location = new Point(165, 934);

                btnPresupuesto1.Location = new Point(9, 989);
                btnPresupuesto2.Location = new Point(165, 989);

                btnUtilerias1.Location = new Point(9, 989);
                btnUtilerias2.Location = new Point(165, 989);
            }
          

        }

        private void guna2GradientButton4_Click_1(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (GruopParametros.Visible == true)
            {

                GruopParametros.Visible = false;
                /*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);


            }
            else
            {
                guna2Panel5.AutoSize = true;
                Grupoinventarios.Visible = false;
                GroupCatalogo.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;
                

                guna2Panel5.Controls.Add(GruopParametros);
                GruopParametros.Visible = true;
                GruopParametros.Location = new Point(9, 51);

           
             

                /*Coloca en la posición Final los botones, cuando se consulta el grupo de catalogos*/

                btncatalogos1.Location = new Point(9, 123);
                btncatalogos2.Location = new Point(165, 123);
                btnInventario.Location = new Point(9, 178);
                BtnInventario1.Location = new Point(165, 178);
                btnCompras1.Location = new Point(9, 233);
                btnCompras2.Location = new Point(165, 233);
                btnventas1.Location = new Point(9, 288);
                btnventas2.Location = new Point(165, 288);
                btnTesoreria1.Location = new Point(9, 343);
                btnTesoreria2.Location = new Point(165, 343);
                btnPresupuesto1.Location = new Point(9, 398);
                btnPresupuesto2.Location = new Point(165, 398);
                btnUtilerias1.Location = new Point(9, 398);
                btnUtilerias2.Location = new Point(165, 398);
            }
            
        }

        private void guna2GradientButton28_Click(object sender, EventArgs e)
        {


        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton21_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (Grupoinventarios.Visible == true)
            {
                Grupoinventarios.Visible = false;
                ///*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
               btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);

            }
            else
            {
                guna2Panel5.VerticalScroll.Value = 0;
                

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                guna2Panel5.Controls.Remove(GruopParametros);
                guna2Panel5.Controls.Remove(GroupCatalogo);
              
                guna2Panel5.Controls.Remove(GrupoCompras);
                guna2Panel5.Controls.Remove(GrupoVentas);
                guna2Panel5.Controls.Remove(GrupoTesoreria);
                guna2Panel5.Controls.Remove(GrupoPresupuesto);
                guna2Panel5.Controls.Remove(GrupoUtilerias);

                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);



                guna2Panel5.Controls.Add(Grupoinventarios);
                Grupoinventarios.Visible = true;
                Grupoinventarios.Location = new Point(9, 161);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/
               
                btnCompras1.Location = new Point(9, 355);
                btnCompras2.Location = new Point(165, 355);
                btnventas1.Location = new Point(9, 410);
                btnventas2.Location = new Point(165, 410);

                btnTesoreria1.Location = new Point(9, 465);
                btnTesoreria2.Location = new Point(165, 465);

                btnPresupuesto1.Location = new Point(9, 520);
                btnPresupuesto2.Location = new Point(165, 520);

                btnUtilerias1.Location = new Point(9, 520);
                btnUtilerias2.Location = new Point(165, 520);

            }
        }

        private void btnventas2_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (GrupoVentas.Visible == true)
            {
           
                GrupoVentas.Visible = false;
                /*Coloca en la posición inicial los botones*/

                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);

                //btnPresupuesto1.Location = new Point(9, 353);
               // btnPresupuesto2.Location = new Point(165, 353);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334); 

            }
            else
            {

                guna2Panel5.VerticalScroll.Value = 0;

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                /* btnTesoreria1.Location = new Point(9, 279);
                 btnTesoreria2.Location = new Point(165, 279);
                 btnPresupuesto1.Location = new Point(9, 334);
                 btnPresupuesto2.Location = new Point(165, 334);
                 btnUtilerias1.Location = new Point(9, 334);
                 btnUtilerias2.Location = new Point(165, 334);
             */
                btnTesoreria1.Location = new Point(9, 735);
                btnTesoreria2.Location = new Point(165, 735);
                btnPresupuesto1.Location = new Point(9, 611);
                btnPresupuesto2.Location = new Point(165, 611);
                btnUtilerias1.Location = new Point(9, 780);
                btnUtilerias2.Location = new Point(165, 780);

                guna2Panel5.Controls.Add(GrupoVentas);
                GrupoVentas.Visible = true;
                GrupoVentas.Location = new Point(9, 270);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/
            }
        }

        private void btnCompras2_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (GrupoCompras.Visible == true)
            {
               
                GrupoCompras.Visible = false;
                /*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 334);
                btnUtilerias2.Location = new Point(165, 334);
            }
            else
            {
                guna2Panel5.VerticalScroll.Value = 0;

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                /*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 4);
                BtnParametros2.Location = new Point(165, 4);
                btncatalogos1.Location = new Point(9, 59);
                btncatalogos2.Location = new Point(165, 59);
                btnInventario.Location = new Point(9, 114);
                BtnInventario1.Location = new Point(165, 114);
                btnCompras1.Location = new Point(9, 169);
                btnCompras2.Location = new Point(165, 169);
                btnventas1.Location = new Point(9, 224);
                btnventas2.Location = new Point(165, 224);
                btnTesoreria1.Location = new Point(9, 279);
                btnTesoreria2.Location = new Point(165, 279);
                btnPresupuesto1.Location = new Point(9, 334);
                btnPresupuesto2.Location = new Point(165, 334);
                btnUtilerias1.Location = new Point(9, 389);
                btnUtilerias2.Location = new Point(165, 389);

                guna2Panel5.Controls.Add(GrupoCompras);
                GrupoCompras.Visible = true;
                GrupoCompras.Location = new Point(9, 218);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/
                btnventas1.Location = new Point(9, 642);
                btnventas2.Location = new Point(165, 642);
                btnTesoreria1.Location = new Point(9, 697);
                btnTesoreria2.Location = new Point(165, 697);
                btnPresupuesto1.Location = new Point(9, 752);
                btnPresupuesto2.Location = new Point(165, 752);
                btnUtilerias1.Location = new Point(9, 752);
                btnUtilerias2.Location = new Point(165, 752);

            }
        }

        private void btnPresupuesto2_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportes.Visible = false;
            gbGraficas.Visible = false;
            if (GrupoPresupuesto.Visible == true)
            {
       
                GrupoPresupuesto.Visible = false;
                /*Coloca en la posición inicial los botones*/
                BtnParametros1.Location = new Point(9, 19);
                BtnParametros2.Location = new Point(165, 19);
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else
            {
                guna2Panel5.VerticalScroll.Value = 0;

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoUtilerias.Visible = false;

                BtnParametros1.Location = new Point(9, 19);
                BtnParametros2.Location = new Point(165, 19);
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);

                guna2Panel5.Controls.Add(GrupoPresupuesto);
                GrupoPresupuesto.Visible = true;
                GrupoPresupuesto.Location = new Point(9, 395);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/

                btnUtilerias1.Location = new Point(9, 674);
                btnUtilerias2.Location = new Point(165, 674);

            }
        }

        private void btnTesoreria2_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            gbGraficas.Visible = false;
            if (GrupoTesoreria.Visible == true)
            {
              
                GrupoTesoreria.Visible = false;
                /*Coloca en la posición inicial los botones*/

                BtnParametros1.Location = new Point(9, 19);
                BtnParametros2.Location = new Point(165, 19);
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 400);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 353);
                btnUtilerias2.Location = new Point(165, 353);
            }
            else
            {
                guna2Panel5.VerticalScroll.Value = 0;

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                BtnParametros1.Location = new Point(9, 19);
                BtnParametros2.Location = new Point(165, 19);
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 353);
                btnUtilerias2.Location = new Point(165, 353);


                guna2Panel5.Controls.Add(GrupoTesoreria);
                GrupoTesoreria.Visible = true;
                GrupoTesoreria.Location = new Point(9, 345);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/

                btnPresupuesto1.Location = new Point(9, 760);
                btnPresupuesto2.Location = new Point(165, 760);
                btnUtilerias1.Location = new Point(9, 760);
                btnUtilerias2.Location = new Point(165, 760);
            }
        }

        private void btnUtilerias2_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            gbGraficas.Visible = false;
            if (GrupoUtilerias.Visible == true)
            {
              
                GrupoUtilerias.Visible = false;
                /*Coloca en la posición inicial los botones*/
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 353);
                btnUtilerias2.Location = new Point(165, 353);

            }
            else
            {
                guna2Panel5.VerticalScroll.Value = 0;

                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;

                BtnParametros1.Location = new Point(9, 19);
                BtnParametros2.Location = new Point(165, 19);
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 353);
                btnUtilerias2.Location = new Point(165, 353);

                guna2Panel5.Controls.Add(GrupoUtilerias);
                GrupoUtilerias.Visible = true;
                GrupoUtilerias.Location = new Point(9, 390);

                /*Coloca en la posición final los botones cuando esta desplegado el inventario*/


            }
        }

        void OcultarGrupos()
        {
          
            if (GruopParametros.Visible == true && Seleccion == "Parametros")
            {
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;


                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);

            }
            else if (GroupCatalogo.Visible == true && Seleccion == "Catalogos")
            {
                GruopParametros.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;

                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (Grupoinventarios.Visible == true && Seleccion == "Inventarios")
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (GrupoCompras.Visible == true && Seleccion == "Compras")
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (GrupoVentas.Visible == true && Seleccion == "Ventas")
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (GrupoTesoreria.Visible == true && Seleccion == "Tesoreria")
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoPresupuesto.Visible = false;
                GrupoUtilerias.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (GrupoPresupuesto.Visible == true && Seleccion == "Presupuesto")
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoUtilerias.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
            else if (GrupoUtilerias.Visible == true && Seleccion == "Utilerias") 
            {
                GruopParametros.Visible = false;
                GroupCatalogo.Visible = false;
                Grupoinventarios.Visible = false;
                GrupoCompras.Visible = false;
                GrupoVentas.Visible = false;
                GrupoTesoreria.Visible = false;
                GrupoPresupuesto.Visible = false;
                btncatalogos1.Location = new Point(9, 74);
                btncatalogos2.Location = new Point(165, 74);
                btnInventario.Location = new Point(9, 132);
                BtnInventario1.Location = new Point(165, 132);
                btnCompras1.Location = new Point(9, 188);
                btnCompras2.Location = new Point(165, 188);
                btnventas1.Location = new Point(9, 246);
                btnventas2.Location = new Point(165, 246);

                btnTesoreria1.Location = new Point(9, 301);
                btnTesoreria2.Location = new Point(165, 301);

                btnPresupuesto1.Location = new Point(9, 353);
                btnPresupuesto2.Location = new Point(165, 353);

                btnUtilerias1.Location = new Point(9, 407);
                btnUtilerias2.Location = new Point(165, 407);
            }
           
        }

        private void GroupCatalogo_Click(object sender, EventArgs e)
        {

        }

        private void GrpoMovimientos_Click(object sender, EventArgs e)
        {

        }

        private void GrupoVentas_Click(object sender, EventArgs e)
        {

        }

        private void btnDatosMmpresa_Click(object sender, EventArgs e)
        {
            DatosEmpresas datosEmpresas = new DatosEmpresas();
            datosEmpresas.ShowDialog();
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.ShowDialog();
        }

        private void btnDivisas_Click(object sender, EventArgs e)
        {
            CatalogoDivisa catalogoDivisa = new CatalogoDivisa();
            catalogoDivisa.ShowDialog();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            Almacenes almacenes = new Almacenes();
            almacenes.ShowDialog();
        }

        private void BtnCategorias_Click(object sender, EventArgs e)
        {
            CatalogoFamilias catalogoFamilias = new CatalogoFamilias();
            catalogoFamilias.ShowDialog();
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            int Consulta = 0;
            CatalogoProductosServicios catalogoProductosServicios = new CatalogoProductosServicios(Consulta);
            catalogoProductosServicios.ShowDialog();
        }

        private void BtnServicios_Click(object sender, EventArgs e)
        {
            int Consulta = 0;
            CatalogoServicios catalogoServicios = new CatalogoServicios(Consulta);
            catalogoServicios.ShowDialog();
        }

        private void BtnCentroCosto_Click(object sender, EventArgs e)
        {
            CentroCostos centroCostos = new CentroCostos();
            centroCostos.ShowDialog();
        }

        private void BtnDocumentos_Click(object sender, EventArgs e)
        {
            Documentos documento = new Documentos();
            documento.ShowDialog();
        }

        private void BtnConceptosGlobales_Click(object sender, EventArgs e)
        {
            ConceptosGlobales conceptosGlobales = new ConceptosGlobales();
            conceptosGlobales.ShowDialog();
        }

        private void BtnFormaPago_Click(object sender, EventArgs e)
        {
            CatalogoFormasPago catalogoFormasPago = new CatalogoFormasPago();
            catalogoFormasPago.ShowDialog();
        }

        private void BtnEmpleados_Click(object sender, EventArgs e)
        {
            CatalogoPersonal catalogoPersonal = new CatalogoPersonal();
            catalogoPersonal.ShowDialog();
        }

        private void BtnTiposZonas_Click(object sender, EventArgs e)
        {
            TiposZonas tiposZonas = new TiposZonas();
            tiposZonas.ShowDialog();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            proveedores.ShowDialog();
        }

        private void BtnCuentasBancarias_Click(object sender, EventArgs e)
        {
            CuentasBancarias cuentasBancarias = new CuentasBancarias();
            cuentasBancarias.ShowDialog();
        }

        private void BtnMovimientos_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = true;
            GrpoMovimientos.Location = new Point(1, 340);
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            SubGrupoReportes.Visible = true;
            SubGrupoReportes.Location = new Point(202, 575);
        }

        private void btnTipoMovimientos_Click(object sender, EventArgs e)
        {
            
            GrpoMovimientos.Visible = false;

            TipoMovimientos mov = new TipoMovimientos();

            mov.StartPosition = FormStartPosition.Manual;
            mov.Left = 260;
            mov.Top = 80;
            mov.ShowDialog();
            
        }

        private void btnRegistrarEntradas_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;

            string movimiento = "E";
            RegistrarEntrada2 entrada = new RegistrarEntrada2(movimiento);
            entrada.StartPosition = FormStartPosition.Manual;
            entrada.Left = 260;
            entrada.Top = 80;

            entrada.ShowDialog();
          
        }

        private void btnRegistrarSalidas_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;

            string movimiento = "S";
            RegistrarEntrada2 sald = new RegistrarEntrada2(movimiento);
            sald.StartPosition = FormStartPosition.Manual;
            sald.Left = 260;
            sald.Top = 80;
            sald.ShowDialog();
           
        }

        private void btnRegistrarTrasnpasos_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = true;


            string movimiento = "T";
            RegistrarEntrada2 tras = new RegistrarEntrada2(movimiento);
            tras.StartPosition = FormStartPosition.Manual;
            tras.Left = 280;
            tras.Top = 80;
            tras.ShowDialog();
           
        }

        private void btnConsultaInventarios_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;

            int Consulta = 0;
            ConsultaInventario2 consulta = new ConsultaInventario2(Consulta);
            consulta.StartPosition = FormStartPosition.Manual;
            consulta.Left = 280;
            consulta.Top = 80;

            consulta.ShowDialog();
            

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void btnReporteExistencias_Click(object sender, EventArgs e)
        { 
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;

            FiltroExistencias filtroExistencias = new FiltroExistencias();

            filtroExistencias.ShowDialog();
          
        }

        private void btnReporteCostoxProducto_Click(object sender, EventArgs e)
        {
            GrpoMovimientos.Visible = false;
            SubGrupoReportes.Visible = false;

            FiltroValorProducto filtroValorProducto = new FiltroValorProducto();
            filtroValorProducto.ShowDialog();
         
        }

        private void guna2GradientButton6_Click_1(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            Requisicion2 requisicion = new Requisicion2();
            requisicion.ShowDialog();
        }

        private void guna2GradientButton4_Click_2(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            OrdenCompra2 ordenCompra = new OrdenCompra2();
            ordenCompra.ShowDialog();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
            gbGraficas.Visible = false; 
            RegistroGastos2 RG = new RegistroGastos2();
            RG.ShowDialog();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
            NotasCargo2 notasCargo = new NotasCargo2();
            notasCargo.ShowDialog();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
            DefinePoliza DP = new DefinePoliza("Definiciones Compras");
            DefinePoliza.TipopolizaCompras = "Compras";
            DP.ShowDialog();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
            GENERARPOLIZAS GP = new GENERARPOLIZAS("Polizas Compras");
            GENERARPOLIZAS.TipopolizaCompras = "Compras";
            GP.ShowDialog();
        }

        private void guna2GradientButton36_Click(object sender, EventArgs e)
        {
            SubGrupoReportesAnticipos.Visible = true;
            
            SubGrupoReportesAnticipos.Location = new Point(236, 475);

            SubGrupoReportesProveedores.Visible = false;
        }

        private void guna2GradientButton35_Click(object sender, EventArgs e)
        {
            SubGrupoReportesAnticipos.Visible = false;
            SubGrupoReportesProveedores.Visible = true;
            SubGrupoReportesProveedores.Location = new Point(236, 580);

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //gbGraficas.Visible = true;
            SubGrupoReportesCompras.Visible = true;
            SubGrupoReportesCompras.Location = new Point(1, 260);
        }

        private void guna2GradientButton32_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteDiarioRequisicionFiltro reporteDiarioRequisicion = new ReporteDiarioRequisicionFiltro();
            reporteDiarioRequisicion.ShowDialog();

        }

        private void guna2GradientButton40_Click(object sender, EventArgs e)
        {

            SubGrupoReportesAnticipos.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteAnticipoProveedorFiltro reporteAnticipoProveedorFiltro = new ReporteAnticipoProveedorFiltro();
            reporteAnticipoProveedorFiltro.ShowDialog();

        }

        private void guna2GradientButton26_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteDiarioOrdenesComprasFiltro reporteDiarioOrdenesCompra = new ReporteDiarioOrdenesComprasFiltro();
            reporteDiarioOrdenesCompra.ShowDialog();
        }

        private void guna2GradientButton34_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteDiarioComprasFiltro reporteDiarioCompras = new ReporteDiarioComprasFiltro("Diario Compras");
            reporteDiarioCompras.ShowDialog();
        }

        private void guna2GradientButton33_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteDiarioComprasFiltro reporteDiarioNotasCargo = new ReporteDiarioComprasFiltro("Diario Gastos");
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void guna2GradientButton38_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteEgresosFiltro reporteEgresos = new ReporteEgresosFiltro();
            reporteEgresos.ShowDialog();
        }

        private void guna2GradientButton37_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteComprasFiltro reporteCompras = new ReporteComprasFiltro();
            reporteCompras.ShowDialog();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

        }

        private void guna2GradientButton39_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
        }

        private void guna2GradientButton42_Click(object sender, EventArgs e)
        {
            SubGrupoReportesAnticipos.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteEstadoCuentaProveedor estadoCuentaProveedor = new ReporteEstadoCuentaProveedor();
            estadoCuentaProveedor.ShowDialog();
        }

        private void guna2GradientButton41_Click(object sender, EventArgs e)
        {
            SubGrupoReportesAnticipos.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
        }

        private void guna2GradientButton43_Click(object sender, EventArgs e)
        {
            SubGrupoReportesAnticipos.Visible = false;
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
        }

        private void guna2GradientButton25_Click(object sender, EventArgs e)
        {
            CatalogoPeriodos catalogoPeriodos = new CatalogoPeriodos();
            catalogoPeriodos.ShowDialog();
        }

        private void guna2GradientButton24_Click(object sender, EventArgs e)
        {
            ConceptosPresupuesto conceptosPresupuesto = new ConceptosPresupuesto();
            conceptosPresupuesto.ShowDialog();
        }

        private void guna2GradientButton21_Click_1(object sender, EventArgs e)
        {
            CerrarPresupuesto cerrarPresupuesto = new CerrarPresupuesto();
            cerrarPresupuesto.ShowDialog();
        }

        private void guna2GradientButton23_Click(object sender, EventArgs e)
        {
            subgrupoPresupuesto.Visible = true;
            subgrupoPresupuesto.Location = new Point(1, 391);
        }

        private void guna2GradientButton22_Click(object sender, EventArgs e)
        {
          

            subgrupoPresupuesto.Visible = false;

            //subgrupoPresupuesto1.Visible = true;
            //subgrupoPresupuesto1.Location = new Point(1, 407);
        }

        private void guna2GradientButton20_Click(object sender, EventArgs e)
        {
            subgrupoPresupuesto.Visible = false;
           // subgrupoPresupuesto1.Visible = false;
            subgrupoPresupuesto3.Visible = true;
            subgrupoPresupuesto3.Location = new Point(2, 555);
           

        }

        private void guna2GradientButton23_Click_1(object sender, EventArgs e)
        {
            subgrupoPresupuesto.Visible = true;
            subgrupoPresupuesto.Location = new Point(1, 391);
        }

        private void guna2GradientButton47_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton44_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton49_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;
            //OrdenPedidoCliente RG = new OrdenPedidoCliente();
            //RG.ShowDialog();
        }

        private void guna2GradientButton30_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            OrdenPedidoCliente o = new OrdenPedidoCliente("Remision");
            o.ShowDialog();
        }

        private void guna2GradientButton14_Click(object sender, EventArgs e)
        {
            OrdenPedidoCliente o = new OrdenPedidoCliente("Pedido");
            o.ShowDialog();
        }

        private void guna2GradientButton15_Click(object sender, EventArgs e)
        {
            ReporteIngresoFormulario reporteIngresos = new ReporteIngresoFormulario();
            reporteIngresos.ShowDialog();
        }

        private void guna2GradientButton50_Click(object sender, EventArgs e)
        {
            gbGraficas.Visible = false;
            RecepcionProductos2 recepcionProductos = new RecepcionProductos2();
            recepcionProductos.ShowDialog();
        }

        private void guna2GradientButton51_Click(object sender, EventArgs e)
        {
          /*  gbGraficas.Visible = false;
            RegistroEgreso r =new RegistroEgreso();
            r.ShowDialog();*/
        }

        private void guna2GradientButton12_Click(object sender, EventArgs e)
        {
            registroIngresos r = new registroIngresos("Remision", "");
            r.ShowDialog();
        }

        private void guna2GradientButton11_Click(object sender, EventArgs e)
        {
            RegistrarAnticipo r = new RegistrarAnticipo("Propietario");
            r.ShowDialog();
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            AplicarAnticipo aplicarAnticipo = new AplicarAnticipo();
            aplicarAnticipo.ShowDialog();
        }

        private void guna2GradientButton52_Click(object sender, EventArgs e)
        {
            ReporteAnticiposFiltro reporteAnticipos = new ReporteAnticiposFiltro();
            reporteAnticipos.ShowDialog();
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            FiltroFecha f = new FiltroFecha("Diario Pedidos");
            f.ShowDialog();
        }

        private void guna2GradientButton54_Click(object sender, EventArgs e)
        {
            FiltroFecha f = new FiltroFecha("Utilidad Pedido");
            f.ShowDialog();
        }

        private void guna2GradientButton55_Click(object sender, EventArgs e)
        {
            FiltroFecha f = new FiltroFecha("Utilidad Producto");
            f.ShowDialog();
        }

        private void guna2GradientButton58_Click(object sender, EventArgs e)
        {
            gbGraficas.Visible = false;
            guna2GradientButton58.Visible = false;
            GraficaMontoGastosxMes f = new GraficaMontoGastosxMes();
            f.ShowDialog();
        }

        private void guna2GradientButton56_Click(object sender, EventArgs e)
        {
            gbGraficas.Visible = true;
            gbGraficas.Location = new Point(1, 260);
        }

        private void guna2GradientButton17_Click(object sender, EventArgs e)
        {
            gbGraficas.Visible = false;
            RegistroEgreso r = new RegistroEgreso();
            r.ShowDialog();
        }

        private void guna2GradientButton6_Click_2(object sender, EventArgs e)
        {
            Requisicion2 Rembolso = new Requisicion2();
            Rembolso.ShowDialog();
        }

        private void guna2GradientButton57_Click(object sender, EventArgs e)
        {
            RegistroReembolsos Rembolso = new RegistroReembolsos();
            Rembolso.ShowDialog();

        }

        private void guna2GradientButton59_Click(object sender, EventArgs e)
        {
            SubGrupoReportesCompras.Visible = false;
            SubGrupoReportesProveedores.Visible = false;

            ReporteDiarioComprasFiltro reporteDiarioNotasCargo = new ReporteDiarioComprasFiltro("Diario Reembolsos");
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void guna2GradientButton16_Click(object sender, EventArgs e)
        {


            pnReportesEgresos.Visible = true;
            pnReportesEgresos.Location = new Point(1, 630);


        }

        private void btnCompras1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton62_Click(object sender, EventArgs e)
        {
            ReporteDiarioComprasFiltro reporteDiarioNotasCargo = new ReporteDiarioComprasFiltro("Diario Egresos");
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void guna2GradientButton61_Click(object sender, EventArgs e)
        {
            ReporteDiarioComprasFiltro reporteDiarioNotasCargo = new ReporteDiarioComprasFiltro("Saldos Proveedor");
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void guna2GradientButton60_Click(object sender, EventArgs e)
        {
            ReporteEstadoCuentaProveedor reporteDiarioNotasCargo = new ReporteEstadoCuentaProveedor();
            reporteDiarioNotasCargo.ShowDialog();
        }

        private void guna2GradientButton73_Click(object sender, EventArgs e)
        {
            DefinePolizas DP = new DefinePolizas("Definiciones Compras");
            DefinePolizas.TipopolizaCompras = "Compras";
        }

        private void guna2GradientButton10_Click_1(object sender, EventArgs e)
        {
            GENERARPOLIZAS GP = new GENERARPOLIZAS("Polizas Compras");
            GENERARPOLIZAS.TipopolizaCompras = "Egresos";
            GP.ShowDialog();
        }

        private void guna2GradientButton73_Click_1(object sender, EventArgs e)
        {
            DefinePolizas DP = new DefinePolizas("Definiciones Compras");
            DefinePolizas.TipopolizaCompras = "Compras";
            DP.ShowDialog();
        }

        private void guna2GradientButton66_Click(object sender, EventArgs e)
        {
            GENERARPOLIZAS GP = new GENERARPOLIZAS("Polizas Compras");
            GENERARPOLIZAS.TipopolizaCompras = "Egresos";
            GP.ShowDialog();
        }
    }
}
