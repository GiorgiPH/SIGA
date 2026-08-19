using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Condominios;
using PuntoVentas.Clases.Login;
using PV;
using ControlAcademico;
using PuntoVentas;
using MEDCON;
using PV.Clases.Respaldo;
using PuntoVentas.Clases.Usuarios;
using Guna.UI2.WinForms;

namespace PuntoVentas
{
    public partial class MenuPrincipal : Form
    {
        DBLogin c = new DBLogin();
        DBRespaldo R = new DBRespaldo();
        DBPermisos p = new DBPermisos();

        public static int Opcion = 0;
        public static int Avisos = 0;

        private Dictionary<string, string> _permisos;
        private MenuLayoutController _menuLayout;

        public MenuPrincipal()
        {
            InitializeComponent();
            lblTipoUsuario.Text = DBLogin.TipoUsuario;
            lblUsuario.Text = DBLogin.usuario;
            InicializarEstructuraMenu();
        }

        private void InicializarEstructuraMenu()
        {
            // Mapeo de botones principales a sus respectivos paneles de grupo
            var modulos = new List<ModuloMenu>
            {
                new ModuloMenu { BotonIzquierdo = BtnParametros1, BotonDerecho = BtnParametros2, PanelGrupo = GrupoParametros },
                new ModuloMenu { BotonIzquierdo = btncatalogos1, BotonDerecho = btncatalogos2, PanelGrupo = GrupoCatalogos },
                new ModuloMenu { BotonIzquierdo = btnInventario, BotonDerecho = BtnInventario1, PanelGrupo = Grupoinventarios },
                new ModuloMenu { BotonIzquierdo = btnCompras1, BotonDerecho = btnCompras2, PanelGrupo = GrupoCompras },
                new ModuloMenu { BotonIzquierdo = btnventas1, BotonDerecho = btnventas2, PanelGrupo = GrupoVentas },
                new ModuloMenu { BotonIzquierdo = btnTesoreria1, BotonDerecho = btnTesoreria2, PanelGrupo = GrupoTesoreria },
                new ModuloMenu { BotonIzquierdo = btnPresupuesto1, BotonDerecho = btnPresupuesto2, PanelGrupo = GrupoPresupuesto },
                new ModuloMenu { BotonIzquierdo = btnUtilerias1, BotonDerecho = btnUtilerias2, PanelGrupo = GrupoUtilerias }
            };

            // Lista de todos los subgrupos para cerrarlos cuando sea necesario
            var subgrupos = new List<Guna2GroupBox>
            {
                SubgrupoMovimientos, SubGrupoReportesMovimientos, SubGrupoReportesCompras,
                subGrupoGraficasCompras, SubgrupoIngresos, SubGrupoReportesProveedores,
                SubGrupoReportesAnticipos, pnReportesEgresos, subgrupoPresupuesto3, SubGrupoBancos
            };

            _menuLayout = new MenuLayoutController(guna2Panel5, modulos, subgrupos);
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            c.empresa();
            _permisos = p.CargarPermisosUsuario(DBLogin.usuario);
            AplicarPermisosMenu();
        }

        private void AplicarPermisosMenu()
        {
            var mapaPermisos = new Dictionary<string, Action<bool>>
            {
                { "MOD_PARAMETROS", val => { BtnParametros1.Enabled = val; BtnParametros2.Enabled = val; } },
                { "MOD_CATALOGOS", val => { btncatalogos1.Enabled = val; btncatalogos2.Enabled = val; } },
                { "MOD_INVENTARIOS", val => { btnInventario.Enabled = val; BtnInventario1.Enabled = val; } },
                { "MOD_COMPRAS", val => { btnCompras1.Enabled = val; btnCompras2.Enabled = val; } },
                { "MOD_VENTAS", val => { btnventas1.Enabled = val; btnventas2.Enabled = val; } },
                { "VRegistrarCobranza", val => btnRegistrarINgresos.Enabled = val },
                { "VPedidosClientes", val => btnPedidos.Enabled = val },
                { "VRemisiones", val => btnRemisiones.Enabled = val },
                { "VReportes", val => {
                    btnReporteAnticipos.Enabled = val;
                    btnReporteIngresos.Enabled = val;
                    btnReporteUtilidadPedido.Enabled = val;
                    btnReporteUtilidadProducto.Enabled = val;
                    btnReporteRemisiones.Enabled = val;
                }}
            };

            foreach (var permiso in _permisos)
            {
                bool habilitado = permiso.Value.ToLower() == "activo";
                if (mapaPermisos.ContainsKey(permiso.Key))
                {
                    mapaPermisos[permiso.Key](habilitado);
                }
            }
        }

        // --- PATRÓN COMMAND CENTRALIZADO PARA APERTURA DE FORMULARIOS ---
        private void AbrirFormulario(Func<Form> crearFormulario, bool cerrarSubgrupos = true, int left = 0, int top = 0)
        {
            if (cerrarSubgrupos)
            {
                _menuLayout.OcultarTodosSubgrupos();
            }

            using (var form = crearFormulario())
            {
                if (left > 0 || top > 0)
                {
                    form.StartPosition = FormStartPosition.Manual;
                    form.Left = left;
                    form.Top = top;
                }
                form.ShowDialog();
            }
        }

        // --- EVENTOS DE APERTURA DE GRUPOS PRINCIPALES ---
        private void guna2GradientButton4_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoParametros);
        private void guna2GradientButton6_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoCatalogos);
        private void guna2GradientButton4_Click_1(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoParametros);
        private void guna2GradientButton21_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(Grupoinventarios);
        private void btnventas2_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoVentas);
        private void btnCompras2_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoCompras);
        private void btnPresupuesto2_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoPresupuesto);
        private void btnTesoreria2_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoTesoreria);
        private void btnUtilerias2_Click(object sender, EventArgs e) => _menuLayout.AlternarGrupo(GrupoUtilerias);

        // --- EVENTOS DE APERTURA DE SUBGRUPOS ---
        private void BtnMovimientos_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubgrupoMovimientos.Visible = true;
            SubgrupoMovimientos.Location = new Point(1, 340);
        }

        private void btnReportesMovimientosInventarios_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubGrupoReportesMovimientos.Visible = true;
            SubGrupoReportesMovimientos.Location = new Point(202, 575);
        }

        private void btnReportesCompras_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubGrupoReportesCompras.Visible = true;
            SubGrupoReportesCompras.Location = new Point(1, 260);
        }

        private void guna2GradientButton56_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            subGrupoGraficasCompras.Visible = true;
            subGrupoGraficasCompras.Location = new Point(1, 260);
        }

        private void guna2GradientButton18_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubgrupoIngresos.Visible = true;
            SubgrupoIngresos.Location = new Point(1, 600);
        }

        private void guna2GradientButton20_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            subgrupoPresupuesto3.Visible = true;
            subgrupoPresupuesto3.Location = new Point(2, 555);
        }

        private void guna2GradientButton36_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubGrupoReportesAnticipos.Visible = true;
            SubGrupoReportesAnticipos.Location = new Point(236, 475);
        }

        private void guna2GradientButton35_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubGrupoReportesProveedores.Visible = true;
            SubGrupoReportesProveedores.Location = new Point(236, 580);
        }

        private void btnReportesTesoreria_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            pnReportesEgresos.Visible = true;
            pnReportesEgresos.Location = new Point(1, 630);
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e) => _menuLayout.OcultarTodosSubgrupos();
        private void guna2GradientButton39_Click(object sender, EventArgs e) => _menuLayout.OcultarTodosSubgrupos();
        private void guna2GradientButton41_Click(object sender, EventArgs e) => _menuLayout.OcultarTodosSubgrupos();
        private void guna2GradientButton43_Click(object sender, EventArgs e) => _menuLayout.OcultarTodosSubgrupos();
        private void guna2GradientButton19_Click(object sender, EventArgs e)
        {
            _menuLayout.OcultarTodosSubgrupos();
            SubGrupoBancos.Visible = true;
            SubGrupoBancos.Location = new Point(0, 520);
        }
        private void guna2GradientButton49_Click_1(object sender, EventArgs e) => _menuLayout.OcultarTodosSubgrupos();

        // --- EVENTOS DE APERTURA DE FORMULARIOS INDIVIDUALES ---

        // MenuStrip Items
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => AbrirFormulario(() => new DatosEmpresas());
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => AbrirFormulario(() => new Usuarios());
        private void divisasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoDivisa());
        private void categoriasYFamiliasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoFamilias());
        private void productosYServiciosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoProductosServicios(0));
        private void formasDePagoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoFormasPago());
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoPersonal());
        private void documentosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Documentos());
        private void conceptosGlobalesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConceptosGlobales());
        private void centrosDeCostosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CentroCostos());
        private void tiposYZonasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new TiposZonas());
        private void condominiosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoCondominios());
        private void areasComunesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoAreasComunes());
        private void propietariosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Propietarios());
        private void conceptosIngresosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConceptosIngresos());
        private void generarRecibosAutomaticosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReciboAutomaticos());
        private void generarRecibosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new GenerarRecibo());
        private void registrarCobranzaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new registroIngresos("", ""));
        private void consultaDatosCondominioToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConsultaCondominio());
        private void ocupacionCondominioToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new OcupacionCondominio());
        private void controlDeActivoFijoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ActivoFijo());
        private void resguardoDeActivoFijoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ResguardoActivoFijo());
        private void activoFijoDiarioToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteActivoFijo2());
        private void resguardoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteResguardoActivoFijo2());
        private void saldoPorPropietarioToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoPropietarioFiltro());
        private void saldoPorPropiedadToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoCondominioFiltro());
        private void saldoDetalladoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoDetalladoFiltro());
        private void catalagoClientesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Clientes());
        private void disponibilidadToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ParametrosGenerales());
        private void reservaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarCitas());
        private void cancelarReservaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CancelarCitas());
        private void consultaReservaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConsultaFechaNombre());
        private void avisosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Avisos());
        private void ingresosToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteIngresoFormulario());
        private void estadoDeCuentaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaFormulario());
        private void almacenesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Almacenes());
        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Proveedores());
        private void ordenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new OrdenCompra());
        private void recepcionDeProductosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new RecepcionProductos2());
        private void egresosToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroEgreso());
        private void cuentasBancariasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CuentasBancarias());
        private void requisicionesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Requisicion());
        private void reporteKardexToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroKardex());
        private void reporteExistenciasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroExistencias());
        private void reporteCostoPorProductosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroValorProducto());
        private void notasDeCargosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new NotasCargo());
        private void reporteDiarioDeOrdenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioOrdenesComprasFiltro());
        private void reporteDiarioDeRequisicionesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioRequisicionFiltro());
        private void reporteDiarioDeComprasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro());
        private void reporteDiarioNotasDeCargoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioNotasCargoFiltro());
        private void reporteDeEgresosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEgresosFiltro());
        private void reporteSaldoDeComprasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteComprasFiltro());
        private void estadoDeCuentaProveedoresToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaProveedor());
        private void base0ToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoPeriodos());
        private void historicoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConceptosPresupuesto());
        private void ingresosToolStripMenuItem2_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroPresupuesto("Ingreso"));
        private void egresosToolStripMenuItem2_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroPresupuesto("Egreso"));
        private void ingreosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new PresupuestoHistorico("Ingreso"));
        private void egresosToolStripMenuItem3_Click(object sender, EventArgs e) => AbrirFormulario(() => new PresupuestoHistorico("Egreso"));
        private void cerrarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CerrarPresupuesto());
        private void registrarAnticipoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarAnticipo("Propietario"));
        private void registrarAnticipoToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarAnticipo("Proveedor"));
        private void aplicarAnticipoToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new AplicarAnticipo());
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            R.ruta();
            MessageBox.Show(R.Respaldo());
        }
        private void ocupaciónENtradasYSalidasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroEntradaSalidaCond());
        private void aplicarAnticipoToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new AplicarAnticipoProveedor());
        private void anticiposAplicadosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticiposAplicadosFiltro());
        private void anticiposToolStripMenuItem2_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticiposFiltro());
        private void saldoDetalladoToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoDetalladoFiltro());
        private void anticiposToolStripMenuItem4_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticipoProveedorFiltro());
        private void anticiposAplicadosToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporeteAnticiposProveedorAplicadosFiltro());
        private void estadoDeCuentaProveedoresToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaProveedor());
        private void saldosProveedoresToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoProveedorFiltro());
        private void detallesDeSaldosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteSaldoDetalladoProveedoresFiltro());
        private void ingresosToolStripMenuItem3_Click(object sender, EventArgs e) => AbrirFormulario(() => new GraficasIngresos());
        private void almacenesToolStripMenuItem1_Click(object sender, EventArgs e) => AbrirFormulario(() => new Almacenes());
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new Clientes());
        private void proveedoresToolStripMenuItem2_Click(object sender, EventArgs e) => AbrirFormulario(() => new Proveedores());
        private void tipoMovimientosToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new TipoMovimientos());
        private void registrarEntradasToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada("E"));
        private void registrarSalidaToolStripMenuItem1_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada("S"));
        private void registrarTraspasosToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada2("T"), true, 280, 80);
        private void consultaInventariosToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ConsultaInventario(0));
        private void reporteKardexToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new FiltroKardex());
        private void reporteExistenciasToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new FiltroExistencias());
        private void reporteCostoPorProductosToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new FiltroValorProducto());
        private void requisicionesToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new Requisicion2());
        private void comprasToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroGastos2());
        private void notasDeCrYCaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new NotasCargo2());
        private void reporteDiarioDeRequisicionesToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioRequisicionFiltro());
        private void reporteDiarioDeOrdenesDeCompraToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioOrdenesComprasFiltro());
        private void reporteDiarioDeComprasToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro());
        private void reporteDiarioNotasDeCargoToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioNotasCargoFiltro());
        private void reporteDeEgresosToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEgresosFiltro());
        private void reporteSaldoDeComprasToolStripMenuItem_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteComprasFiltro());
        private void anticiposToolStripMenuItem4_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticipoProveedorFiltro());
        private void anticiposAplicadosToolStripMenuItem1_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new ReporeteAnticiposProveedorAplicadosFiltro());
        private void estadoDeCuentaProveedoresToolStripMenuItem_Click_2(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaProveedor());
        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoServicios(0));
        private void DefinepolizaStripMenuItem7_Click(object sender, EventArgs e)
        {
            DefinePoliza.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new DefinePoliza("Definiciones Compras"));
        }
        private void pedidosProveedoresToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new OrdenCompra2());
        private void comprasRecepciónEntradaProductosAlmacénToolStripMenuItem_Click(object sender, EventArgs e) => AbrirFormulario(() => new RecepcionProductos2());
        private void GenerePolizaStripMenuItem7_Click(object sender, EventArgs e)
        {
            GENERARPOLIZAS.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new GENERARPOLIZAS("Polizas Compras"));
        }

        // Botones de Paneles
        private void btnDatosMmpresa_Click(object sender, EventArgs e) => AbrirFormulario(() => new DatosEmpresas());
        private void BtnUsuarios_Click(object sender, EventArgs e) => AbrirFormulario(() => new Usuarios());
        private void btnDivisas_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoDivisa());
        private void btnAlmacen_Click(object sender, EventArgs e) => AbrirFormulario(() => new Almacenes());
        private void BtnCategorias_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoFamilias());
        private void BtnProductos_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoProductosServicios(0));
        private void BtnServicios_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoServicios(0));
        private void BtnCentroCosto_Click(object sender, EventArgs e) => AbrirFormulario(() => new CentroCostos());
        private void BtnDocumentos_Click(object sender, EventArgs e) => AbrirFormulario(() => new Documentos());
        private void BtnConceptosGlobales_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConceptosGlobales());
        private void BtnFormaPago_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoFormasPago());
        private void BtnEmpleados_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoPersonal());
        private void BtnTiposZonas_Click(object sender, EventArgs e) => AbrirFormulario(() => new TiposZonas());
        private void BtnClientes_Click(object sender, EventArgs e) => AbrirFormulario(() => new Clientes());
        private void BtnProveedores_Click(object sender, EventArgs e) => AbrirFormulario(() => new Proveedores());
        private void BtnCuentasBancarias_Click(object sender, EventArgs e) => AbrirFormulario(() => new CuentasBancarias());
        private void btnTipoMovimientos_Click(object sender, EventArgs e) => AbrirFormulario(() => new TipoMovimientos(), true, 260, 80);
        private void btnRegistrarEntradas_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada2("E"), true, 260, 80);
        private void btnRegistrarSalidas_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada2("S"), true, 260, 80);
        private void btnRegistrarTrasnpasos_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarEntrada2("T"), true, 280, 80);
        private void btnConsultaInventarios_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConsultaInventario2(0), true, 280, 80);
        private void btnReporteExistencias_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroExistencias());
        private void btnReporteCostoxProducto_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroValorProducto());
        private void guna2GradientButton6_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new Requisicion2());
        private void guna2GradientButton4_Click_2(object sender, EventArgs e) => AbrirFormulario(() => new OrdenCompra2());
        private void guna2GradientButton3_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroGastos2());
        private void guna2GradientButton2_Click(object sender, EventArgs e) => AbrirFormulario(() => new NotasCargo2());
        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            DefinePoliza.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new DefinePoliza("Definiciones Compras"));
        }
        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            GENERARPOLIZAS.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new GENERARPOLIZAS("Polizas Compras"));
        }
        private void guna2GradientButton32_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioRequisicionFiltro());
        private void guna2GradientButton40_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticipoProveedorFiltro());
        private void guna2GradientButton26_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioOrdenesComprasFiltro());
        private void guna2GradientButton34_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro("Diario Compras"));
        private void guna2GradientButton33_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro("Diario Gastos"));
        private void guna2GradientButton38_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEgresosFiltro());
        private void guna2GradientButton37_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteComprasFiltro());
        private void guna2GradientButton42_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaProveedor());
        private void guna2GradientButton25_Click(object sender, EventArgs e) => AbrirFormulario(() => new CatalogoPeriodos());
        private void guna2GradientButton24_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConceptosPresupuesto());
        private void guna2GradientButton21_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new CerrarPresupuesto());
        private void guna2GradientButton13_Click(object sender, EventArgs e) => AbrirFormulario(() => new OrdenPedidoCliente("Remision"));
        private void guna2GradientButton14_Click(object sender, EventArgs e) => AbrirFormulario(() => new OrdenPedidoCliente("Pedido"));
        private void guna2GradientButton15_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteIngresoFormulario());
        private void guna2GradientButton50_Click(object sender, EventArgs e) => AbrirFormulario(() => new RecepcionProductos2());
        private void guna2GradientButton12_Click(object sender, EventArgs e) => AbrirFormulario(() => new registroIngresos("Remision", ""));
        private void guna2GradientButton11_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistrarAnticipo("Propietario"));
        private void guna2GradientButton10_Click(object sender, EventArgs e) => AbrirFormulario(() => new AplicarAnticipo());
        private void guna2GradientButton52_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteAnticiposFiltro());
        private void guna2GradientButton2_Click_1(object sender, EventArgs e) => AbrirFormulario(() => new FiltroFecha("Diario Pedidos"));
        private void guna2GradientButton54_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroFecha("Utilidad Pedido"));
        private void guna2GradientButton55_Click(object sender, EventArgs e) => AbrirFormulario(() => new FiltroFecha("Utilidad Producto"));
        private void guna2GradientButton58_Click(object sender, EventArgs e)
        {
            subGrupoGraficasCompras.Visible = false;
            guna2GradientButton58.Visible = false;
            AbrirFormulario(() => new GraficaMontoGastosxMes());
        }
        private void guna2GradientButton17_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroEgreso());
        private void guna2GradientButton6_Click_2(object sender, EventArgs e) => AbrirFormulario(() => new Requisicion2());
        private void guna2GradientButton57_Click(object sender, EventArgs e) => AbrirFormulario(() => new RegistroReembolsos());
        private void guna2GradientButton59_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro("Diario Reembolsos"));
        private void guna2GradientButton62_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro("Diario Egresos"));
        private void guna2GradientButton61_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteDiarioComprasFiltro("Saldos Proveedor"));
        private void guna2GradientButton60_Click(object sender, EventArgs e) => AbrirFormulario(() => new ReporteEstadoCuentaProveedor());
        private void guna2GradientButton73_Click(object sender, EventArgs e)
        {
            DefinePolizas.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new DefinePolizas("Definiciones Compras"));
        }
        private void guna2GradientButton10_Click_1(object sender, EventArgs e)
        {
            GENERARPOLIZAS.TipopolizaCompras = "Egresos";
            AbrirFormulario(() => new GENERARPOLIZAS("Polizas Compras"));
        }
        private void guna2GradientButton73_Click_1(object sender, EventArgs e)
        {
            DefinePolizas.TipopolizaCompras = "Compras";
            AbrirFormulario(() => new DefinePolizas("Definiciones Compras"));
        }
        private void guna2GradientButton66_Click(object sender, EventArgs e)
        {
            GENERARPOLIZAS.TipopolizaCompras = "Egresos";
            AbrirFormulario(() => new GENERARPOLIZAS("Polizas Compras"));
        }

        // --- LÓGICA DE CIERRE DE SESIÓN Y APLICACIÓN ---
        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarSalidaSistema();
            PuntoVentas.Opcion = 0;
            PuntoVentas med = new PuntoVentas();
            med.Show();
            this.Hide();
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistrarSalidaSistema();
            PuntoVentas.Opcion = 0;
            Application.Exit();
        }

        private void RegistrarSalidaSistema()
        {
            DateTime FechaSalida = DateTime.Today;
            string Salida = DateTime.Now.ToString("HH:mm:ss tt");
            c.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);
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

        // Eventos vacíos originales que se mantienen para no romper el Designer
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void toolStripMenuItem6_Click(object sender, EventArgs e) { }
        private void notasDeCreditoToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void anticiposToolStripMenuItem3_Click(object sender, EventArgs e) { }
        private void guna2GradientButton49_Click(object sender, EventArgs e) { }
        private void guna2GradientButton51_Click(object sender, EventArgs e) { }

        private void btnConceptosCobro_Click(object sender, EventArgs e) => AbrirFormulario(() =>
        new CatalogoConceptosTesoreria());

        private void btnCxPEgresos_Click(object sender, EventArgs e) => AbrirFormulario(() => new ConsultarEgreso());
    }

    // --- CLASES DE APOYO PARA LA ESTRUCTURA DEL MENÚ ---
    public class ModuloMenu
    {
        public Control BotonIzquierdo { get; set; }
        public Control BotonDerecho { get; set; }
        public Guna2GroupBox PanelGrupo { get; set; }
    }

    public class MenuLayoutController
    {
        private readonly List<ModuloMenu> _modulos;
        private readonly List<Guna2GroupBox> _subgrupos;
        private readonly ScrollableControl _contenedorPrincipal;

        private const int X_Izquierdo = 9;
        private const int X_Derecho = 165;
        private const int Y_Inicial = 4;
        private const int EspacioEntreBotones = 55;
        private const int AlturaBoton = 45;

        public MenuLayoutController(ScrollableControl contenedorPrincipal, List<ModuloMenu> modulos, List<Guna2GroupBox> subgrupos)
        {
            _contenedorPrincipal = contenedorPrincipal;
            _modulos = modulos;
            _subgrupos = subgrupos;

            // SOLUCIÓN: Forzar el anclaje superior e izquierdo para evitar que 
            // los botones se empujen hacia la izquierda cuando aparece la barra de desplazamiento vertical.
            foreach (var modulo in _modulos)
            {
                modulo.BotonIzquierdo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                modulo.BotonDerecho.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                modulo.PanelGrupo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }

        public void OcultarTodosSubgrupos()
        {
            foreach (var subgrupo in _subgrupos)
            {
                subgrupo.Visible = false;
            }
        }

        public void AlternarGrupo(Guna2GroupBox grupoSeleccionado)
        {
            OcultarTodosSubgrupos();

            bool mostrar = !grupoSeleccionado.Visible;

            foreach (var modulo in _modulos)
            {
                modulo.PanelGrupo.Visible = false;
                _contenedorPrincipal.Controls.Remove(modulo.PanelGrupo);
            }

            if (mostrar)
            {
                _contenedorPrincipal.VerticalScroll.Value = 0;
                _contenedorPrincipal.Controls.Add(grupoSeleccionado);
                grupoSeleccionado.Visible = true;
            }

            RecalcularPosiciones(grupoSeleccionado, mostrar);
        }

        private void RecalcularPosiciones(Guna2GroupBox grupoSeleccionado, bool grupoVisible)
        {
            int yActual = Y_Inicial;

            foreach (var modulo in _modulos)
            {
                modulo.BotonIzquierdo.Location = new Point(X_Izquierdo, yActual);
                modulo.BotonDerecho.Location = new Point(X_Derecho, yActual);

                int ySiguiente = yActual + EspacioEntreBotones;

                if (grupoVisible && modulo.PanelGrupo == grupoSeleccionado)
                {
                    grupoSeleccionado.Location = new Point(X_Izquierdo, yActual + AlturaBoton);
                    ySiguiente = yActual + AlturaBoton + grupoSeleccionado.Height;
                }

                if (modulo.BotonIzquierdo.Visible)
                {
                    yActual = ySiguiente;
                }
            }
        }
    }
}