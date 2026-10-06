using Condominios.Clases.CentroCostos;
using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.Almacenes;
using PV.Clases.CentroCostos;
using PV.Clases.Clientes;
using PV.Clases.Inventario;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;
using PV.Clases.Proveedores;
using PV.Clases.Remision;
using PV.Clases.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Formulario combinado de Remisión y Pedido a Cliente (la rama de lógica
    /// que se ejecuta depende del parámetro "tipo" del constructor: "Remision"
    /// o cualquier otro valor para Pedido a Cliente). La lógica de negocio de
    /// Producto (existencias, pedidos pendientes, vínculo con un Pedido a
    /// Cliente previo, salida de almacén al confirmar una Remisión) ya estaba
    /// totalmente funcional y NO se modifica aquí.
    ///
    /// AJUSTE (Producto o Servicio por partida): se agrega el combo "cmbTipo"
    /// para que cada partida -tanto en Remisión como en Pedido a Cliente-
    /// pueda capturarse contra el catálogo de Productos (ProductosServicios,
    /// comportamiento histórico, SIN CAMBIOS) o contra el catálogo de
    /// Servicios (DBServicios, nuevo). Decisiones de diseño:
    ///
    ///   1) El camino "Producto" se deja INTACTO: mismos métodos, mismos
    ///      parámetros, mismo orden de llamadas que ya tenías. Sólo se le
    ///      agregó, a cada método que lo necesitaba, un nuevo parámetro
    ///      "tipoConcepto" al final de la lista de parámetros de captura (ver
    ///      nota de "CAMBIO DE FIRMA REQUERIDO" en cada sitio). El camino
    ///      "Servicio" es 100% nuevo y vive en ramas "if (cmbTipo.Text ==
    ///      'Servicio')" separadas.
    ///
    ///   2) Un Servicio no maneja inventario: ValidarExistencias() no hace
    ///      nada (regresa true) cuando cmbTipo = 'Servicio'; los contadores
    ///      de existencias/pedidos pendientes (lblExistencias,
    ///      lblPedidosProveedor, lblPedidosCliente, lblDisponible) se marcan
    ///      "N/D"; no se actualiza ProductosServicios.PedidosCliente
    ///      (DBProductosServicios.RegistroPedidosCliente) ni la cantidad
    ///      pendiente de un Pedido a Cliente vinculado
    ///      (ActualizaCantidadPendientePartidaOrden); y, al confirmar una
    ///      Remisión, sólo las partidas de tipo 'Producto' (y, de ésas,
    ///      sólo las inventariables - ver Ajuste 5 abajo) generan movimiento
    ///      de salida de almacén; las de 'Servicio' no, porque no hay
    ///      existencia física que mover.
    ///
    ///   3) cmbConcepto arranca vacío cada vez que se abre una partida nueva
    ///      (cmbTipo sin selección), igual que en Facturas/Cotizaciones: el
    ///      usuario debe elegir primero Producto o Servicio, lo que dispara
    ///      la carga del catálogo correspondiente (CargarComboConceptos).
    ///
    ///   4) Varios métodos de DBOrdenCompra (o) y DBPedidoCliente (c) deben
    ///      actualizar su firma para soportar esto. Como esas clases son muy
    ///      grandes y no se compartieron completas, este archivo YA ASUME las
    ///      nuevas firmas (marcadas con "// CAMBIO DE FIRMA REQUERIDO EN..."
    ///      en cada sitio de la llamada) y por lo tanto VA A MARCAR ERROR DE
    ///      COMPILACIÓN hasta que esos métodos se actualicen. Ver el archivo
    ///      "DBOrdenCompra_DBPedidoCliente_MetodosAfectados.cs" para una
    ///      propuesta completa de cada método afectado, lista para adaptar
    ///      contra tus implementaciones reales.
    ///
    /// Ajuste 5 (homologación de la salida de almacén con Facturas): el
    /// método privado "IngresarAlmacen" que reimplementaba aquí todo el
    /// proceso (validar TipoMovimiento, crear el encabezado, recorrer
    /// partidas descontando existencia, cerrar el movimiento) SE QUITÓ por
    /// completo. En su lugar, guna2Button9_Click llama directamente a
    /// DBRegistrarEntradas.GenerarSalidaAlmacen -el mismo método que ya usa
    /// Facturas-, con documento 'SPR'/"SALIDA POR REMISIÓN". El filtro de
    /// qué partidas califican (antes sólo miraba TipoConcepto = 'Producto')
    /// ahora es el mismo que en Facturas: TipoConcepto = 'Producto' Y
    /// ProductosServicios.Inventariable = 'Si', vía el nuevo
    /// DBPedidoCliente.ObtenerPartidasInventariables (reemplaza, para este
    /// uso puntual, a ObtenerPartidas). Efecto colateral intencional: si una
    /// Remisión no tiene ninguna partida inventariable ya NO se crea un
    /// MovimientoInventario vacío (antes siempre se creaba uno, tuviera o no
    /// renglones).
    /// </summary>
    public partial class OrdenPedidoCliente : Form
    {

        public static string Matricula = string.Empty;
        public static string M2 = string.Empty;
        public static int Opcion = 0;


        DBPedidoCliente c = new DBPedidoCliente();
        DBOrdenCompra o = new DBOrdenCompra();
        DBAlmacenes a = new DBAlmacenes();
        DBClientes cl = new DBClientes();
        DBCentroCostos cc = new DBCentroCostos();
        DBDatosProyecto dp = new DBDatosProyecto();
        DBProductosServicios p = new DBProductosServicios();

        // Catálogo de Servicios, para cuando cmbTipo = "Servicio" (nuevo).
        DBServicios srv = new DBServicios();

        // Salida de almacén al confirmar la Remisión (sólo partidas de tipo
        // 'Producto' con ProductosServicios.Inventariable = 'Si'). Mismo
        // proceso homologado que ya usa Facturas: ver
        // DBRegistrarEntradas.GenerarSalidaAlmacen.
        DBRegistrarEntradas movInventario = new DBRegistrarEntradas();

        string recibo = string.Empty;
        string reciboCol = string.Empty;
        string tipo = string.Empty;

        private bool mostrrcentorcosto = false;

        public OrdenPedidoCliente(string tipo)
        {
            InitializeComponent();
            this.tipo = tipo;
            ToolTip T = new ToolTip();
            if (tipo == "Remision")
            {
                label18.Text = "Remisión";

                T.SetToolTip(guna2Button15, "Nueva Remisión");
                T.SetToolTip(guna2Button16, "Consultar Remisión");
                T.SetToolTip(button10, "Imprimir Remisión");
                T.SetToolTip(btnRemisionXML, "generar XML");


            }
            else
            {
                d.Visible = false;
                btnDocumento.Visible = false;
                label66.Visible = false;
                label18.Text = "Pedidos a Cliente";
                T.SetToolTip(guna2Button15, "Nuevo Orden Pedido");
                T.SetToolTip(guna2Button16, "Consultar Orden Pedido");
                T.SetToolTip(button10, "Imprimir Orden Pedido");

            }
            T.SetToolTip(btnCliente, "Buscar Cliente");
            T.SetToolTip(btnCliente, "Buscar Orden Pedido");

            // cmbTipo se agrega directamente en el Designer (sin wiring de
            // evento todavía); se engancha aquí por código para no depender
            // de que el evento haya quedado conectado desde el panel de
            // Propiedades de Visual Studio.
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;

            c.BuscarProveedor(guna2DataGridView2);
        }

        private void OrdenCompra2_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                // txtMatricular.Text = Matricula;
                if (tipo == "Remision")
                {
                    o.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);

                }
                else
                {
                    c.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);

                }
                //c.ReciboSaldosImpuesto(txtFolio.Text, txtRecargo);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
                else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                {
                    //7 button1.BackColor = Color.Red;
                }
            }
            if (Opcion == 1)
            {
                txtAutoriza.Text = DBRegistrarIngresos.usuario;
                txtFechaAuto.Text = DateTime.Today.ToString("yyyy/MM/dd");
                c.ActualizarOrdenAuto(txtFolio.Text, txtAutoriza.Text, txtFechaAuto.Text);
                MessageBox.Show("Orden de Compra Autorizada");
                Limpiar();
                if (tipo == "Remision")
                {
                    o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                    o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
                }
                else
                {
                    c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                    c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                }
            }
        }

        private void OrdenCompra2_Load(object sender, EventArgs e)
        {
            if (tipo == "Remision")
            {
                o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);

            }
            else
            {
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                btnRemisionXML.Visible = false;
            }
            c.SeleccionarConceptoDocumentoV(cmbDocumento, tipo);

            ConfigurarComboTipo();

            a.SeleccionarAlmacen(cmbAlmacen);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;

            //  cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            int Dias = Convert.ToInt32(txtDiasVence.Text);
            DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
            FechaVence = FechaVence.AddDays(Dias);
            txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);


            //guna2GradientPanel6.Location = new Point(1017, 83);
            //guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;
            toolStripButton5.Visible = true;


            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            //guna2GradientPanel6.Location = new Point(1077, 83);
            //guna2GradientPanel6.Size = new Size(23, 569);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);
            LlenarComboCentro();

        }

        /// <summary>
        /// Llena cmbTipo con las dos opciones fijas Producto/Servicio y lo
        /// deja en modo "sólo selección de lista", para que su .Text siempre
        /// sea exactamente "Producto" o "Servicio" tal como lo esperan
        /// CargarComboConceptos() y cmbConcepto_SelectedIndexChanged.
        /// </summary>
        private void ConfigurarComboTipo()
        {
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Producto");
            cmbTipo.Items.Add("Servicio");
            cmbTipo.SelectedIndex = -1;
        }

        private void LlenarComboCentro()
        {
            try
            {
                DataTable menus = cc.ConsultarTodos();

                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostos.DataSource = menus;
                cmbCentroCostos.DisplayMember = "Nombre";
                cmbCentroCostos.ValueMember = "Clave";
                cmbCentroCostos.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ParsearBooleano(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            switch (valor.Trim().ToUpperInvariant())
            {
                case "1":
                case "TRUE":
                case "SI":
                case "SÍ":
                case "S":
                case "YES":
                    return true;
                default:
                    return false;
            }
        }
        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];
                    if (txtFolio.Text == string.Empty)
                    {
                        if (tipo == "Remision")
                        {

                            // valores[2] indica si el documento permite capturar Centro de Costos
                            if (valores.Length > 2)
                            {
                                mostrrcentorcosto = ParsearBooleano(valores[2]);
                                cmbCentroCostos.Enabled = mostrrcentorcosto;
                            }
                            o.ConsecutivoCompra(txtConsecutivo, txtClave.Text);

                        }
                        else
                        {
                            c.ConsecutivoCompra(txtConsecutivo, txtClave.Text);

                        }
                    }
                    // groupBox2.Enabled = true;
                    txtDiasVence.Focus();
                }

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtNombreAlumnno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            //txtMatricular.Text = M2;
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores = cl.InformacionCliente(txtMatricular.Text);
                txtNombreAlumnno.Text = valores[1];
            }



        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {

            BuscarCliente b = new BuscarCliente();
            b.ShowDialog();

            if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
            {
                txtMatricular.Text = BuscarCliente.Cliente;
            }


        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        if (tipo == "Remision")
                        {
                            o.ActualizarTotalesRemision(TxtFolio2.Text, Partida.ToString());

                        }
                        else
                        {
                            c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, Partida.ToString());

                        }
                    }
                    // this.Close();
                    PanelPartidasRequisicion.Visible = false;
                }
            }
            else if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        if (tipo == "Remision")
                        {
                            o.ActualizarTotalesRemision(TxtFolio2.Text, Partida.ToString());

                        }
                        else
                        {
                            c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, Partida.ToString());

                        }
                    }
                    //  this.Close();
                    PanelPartidasRequisicion.Visible = false;
                }
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (tipo == "Remision")
                {
                    if (!ValidarExistencias())
                    {
                        return;
                    }

                    // "Producto": misma clave que siempre (txtConcepto2.Text,
                    // sin cambios). "Servicio" (nuevo): la clave real viene
                    // del ValueMember de cmbConcepto (ClaveServicio).
                    string claveConceptoInsert = cmbTipo.Text == "Servicio"
                        ? cmbConcepto.SelectedValue?.ToString()
                        : txtConcepto2.Text;

                    // CAMBIO DE FIRMA REQUERIDO EN DBOrdenCompra.InsertarPartidaRemision:
                    // se agrega "tipoConcepto" (cmbTipo.Text) como tercer parámetro.
                    o.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, cmbTipo.Text, claveConceptoInsert, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    o.ActualizarTotalesRemision(TxtFolio2.Text, txtPartida.Text);

                    // Un Servicio no afecta la cantidad pendiente de un
                    // Pedido a Cliente vinculado ni PedidosCliente en
                    // ProductosServicios (no aplica, no maneja inventario).
                    if (cmbTipo.Text != "Servicio" && !string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendientePartidaOrden(cant.ToString(), cant.ToString(), txtFolioPedido.Text, txtConcepto2.Text);

                        p.RegistroPedidosCliente(txtConcepto2.Text, cantneg.ToString().Replace(",", ""));
                    }
                    //ConceptosGlobalesPartidaRemision cg = new ConceptosGlobalesPartidaRemision(txtFolio.Text, "", "");
                    //cg.ShowDialog();

                }
                else
                {
                    string claveConceptoInsert = cmbTipo.Text == "Servicio"
                        ? cmbConcepto.SelectedValue?.ToString()
                        : txtConcepto2.Text;

                    // CAMBIO DE FIRMA REQUERIDO EN DBPedidoCliente.InsertarPartidaOrdenCliente:
                    // se agrega "tipoConcepto" (cmbTipo.Text) como tercer parámetro.
                    string mensaje = c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, cmbTipo.Text, claveConceptoInsert, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show(mensaje);
                    }
                    c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, txtPartida.Text);

                    if (cmbTipo.Text != "Servicio")
                    {
                        p.RegistroPedidosCliente(txtConcepto2.Text, txtCantidad.Text);
                    }


                }
                CargarPartidas();
                PanelPartidasRequisicion.Visible = false;
                guna2Button9.Visible = true;
            }

        }
        private bool ValidarExistencias()
        {
            // Un Servicio no maneja inventario: no hay existencias que
            // validar, así que simplemente se permite continuar.
            if (cmbTipo.Text == "Servicio")
            {
                return true;
            }

            decimal existencias;
            decimal cantidad;

            if (!decimal.TryParse(lblExistencias.Text, out existencias))
            {
                MessageBox.Show(
                    "No fue posible obtener las existencias del producto seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show(
                    "La cantidad capturada no es válida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtCantidad.Focus();
                return false;
            }

            if (cantidad > existencias)
            {
                MessageBox.Show(
                    "No hay suficiente inventario del producto seleccionado.",
                    "Inventario insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCantidad.Focus();
                return false;
            }

            return true;
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            else if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }
            else if (txtFrecuencia.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (tipo == "Remision")
                {
                    if (!ValidarExistencias())
                    {
                        return;
                    }

                    string claveConceptoInsert = cmbTipo.Text == "Servicio"
                        ? cmbConcepto.SelectedValue?.ToString()
                        : txtConcepto2.Text;

                    // CAMBIO DE FIRMA REQUERIDO EN DBOrdenCompra.InsertarPartidaRemision (igual que en btnConfirmar_Click).
                    o.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, cmbTipo.Text, claveConceptoInsert, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    o.ActualizarTotalesRemision(TxtFolio2.Text, txtPartida.Text);

                    o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);
                    o.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
                    //o.ReciboSaldosPartidasOrden2(TxtFolio2.Text, txtImpuestoR);

                    if (cmbTipo.Text != "Servicio" && !string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendientePartidaOrden(cant.ToString(), cant.ToString(), txtFolioPedido.Text, txtConcepto2.Text);

                        p.RegistroPedidosCliente(txtConcepto2.Text, cantneg.ToString().Replace(",", ""));



                    }

                }
                else
                {
                    string claveConceptoInsert = cmbTipo.Text == "Servicio"
                        ? cmbConcepto.SelectedValue?.ToString()
                        : txtConcepto2.Text;

                    // CAMBIO DE FIRMA REQUERIDO EN DBPedidoCliente.InsertarPartidaOrdenCliente (igual que en btnConfirmar_Click).
                    string mensaje = c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, cmbTipo.Text, claveConceptoInsert, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show(mensaje);
                    }
                    c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, txtPartida.Text);
                    c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
                    c.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);

                    if (cmbTipo.Text != "Servicio")
                    {
                        p.RegistroPedidosCliente(txtConcepto2.Text, txtCantidad.Text);
                    }


                }

                // Vuelve a cargar el mismo catálogo (Producto o Servicio,
                // según lo que se acaba de usar) para la siguiente partida.
                CargarComboConceptos();
                CargarPartidas();

            }
            LimpiarPartida();
        }


        private void CargarPartidas()
        {
            if (tipo == "Remision")
            {
                o.CargarRecibosPartidas(guna2DataGridView1, TxtFolio2.Text);


            }
            else
            {
                c.CargarOrdenPedidoClientePartidas(guna2DataGridView1, TxtFolio2.Text);

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            else if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una orden de compra Bloqueado o Cancelado");
                return;
            }
            else if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }
            else if (string.IsNullOrEmpty(cmbAlmacen.Text))
            {
                MessageBox.Show("Selecciona un almacén");
                return;
            }
            else
            {
                string almacen = cmbAlmacen.Text.Split('-')[0];
                string ReciboCol = string.Empty;

                if (string.IsNullOrEmpty(txtFolio.Text))
                {
                    if (tipo.Equals("Remision"))
                    {
                        string centroCosto = null;
                        string proyecto = ComboUtil.ObtenerSelectedValue(cmbproyecto);

                        if (mostrrcentorcosto)
                        {
                            centroCosto = ComboUtil.ObtenerSelectedValue(cmbCentroCostos);

                            if (string.IsNullOrWhiteSpace(centroCosto))
                            {
                                MessageBox.Show(
                                    "Seleccione un centro de costos antes de continuar.",
                                    "Validación",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                return;
                            }
                        }
                        o.InsertarRemision(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, almacen, txtFolioPedido.Text, centroCosto, proyecto);

                    }
                    else
                    {
                        c.InsertarOrden(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, almacen, tipo);

                    }
                    //    MessageBox.Show(txtFolio.Text);

                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                //PartidasOrden partidas = new PartidasOrden(txtFolio.Text, txtReciboInsc.Text, ReciboCol);
                //partidas.ShowDialog();
                cmbAlmacen.Enabled = false;
                TxtFolio2.Text = txtFolio.Text;
                guna2TabControl1.SelectedIndex = 1;
                TxtFolio2.Text = txtFolio.Text;
                recibo = txtReciboInsc.Text;
                reciboCol = ReciboCol;

                // cmbTipo arranca sin selección: el usuario debe elegir
                // Producto o Servicio antes de que se cargue el catálogo
                // correspondiente en cmbConcepto (vía cmbTipo_SelectedIndexChanged).
                cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
                cmbTipo.SelectedIndex = -1;
                cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
                cmbConcepto.DataSource = null;
                cmbConcepto.Items.Clear();

                if (tipo == "Remision")
                {
                    o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);
                }
                else
                {
                    c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
                }
                txtCantidad.Text = "1";
                txtUnidad.Clear();
                txtDivisa1.Text = "MXN";
                txtTipoCambio1.Text = "1.00";
            }

            //    cmbDocumento.DroppedDown = false;
            //  button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            // button6.BackColor = Color.Gainsboro;
        }

        private void txtTipoCambio_TextChanged(object sender, EventArgs e)
        {

        }

        void Limpiar()
        {
            txtFolio.Clear();
            TxtFolio2.Clear();
            txtConsecutivo.Text = string.Empty;
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtMatricular.Text = string.Empty;
            txtNombreAlumnno.Text = string.Empty;
            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            txtConsecutivo.Clear();
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            cmbAlmacen.Enabled = false;
            txtFecha.Enabled = false;
            btnCliente.Enabled = false;
            //  button1.BackColor = Color.Gainsboro;
            txtNotas.Enabled = false;
            //   button3.Enabled = false;
            //groupBox2.Enabled = false;
            Matricula = string.Empty;
            //button7.Enabled = false;
            Opcion = 0;
            txtAutoriza.Clear();
            txtFechaAuto.Clear();
            cmbDocumento.DroppedDown = false;
            //button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            //button2.BackColor = Color.Gainsboro;
            //button6.BackColor = Color.Gainsboro;

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            cmbConcepto.SelectedIndex = -1;
            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();

            cmbAlmacen.SelectedIndex = -1;
            PanelPartidasRequisicion.Visible = false;
            guna2TabControl1.SelectedIndex = 0;
            guna2DataGridView1.Rows.Clear();
            txtFolioPedido.Text = string.Empty;
            d.Clear();
            cmbCentroCostos.SelectedIndex = -1;
            cmbproyecto.SelectedIndex = -1;
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {

                if (cmbEstatus.Text != "Abierto")
                {
                    //Limpiar();
                    cmbDocumento.Enabled = true;
                    txtDiasVence.Enabled = true;
                    txtNotas.Enabled = true;
                    //     button3.Enabled = true;
                    //    cmbDocumento.Focus();
                    //     cmbDocumento.DroppedDown = true;

                }
                else if (cmbEstatus.Text == "Abierto" && cmbDocumento.Text == string.Empty)
                {
                    Limpiar();
                    cmbDocumento.Enabled = true;
                    txtDiasVence.Enabled = true;
                    txtNotas.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Confirme la orden de compra antes de continuar");
                }

                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                btnSiguiente.Visible = true;

                guna2Button2.Enabled = true;
                guna2Button5.Enabled = true;
                guna2Button6.Enabled = true;
                guna2Button7.Enabled = true;
                btnSiguiente.Enabled = true;

                cmbDocumento.Enabled = true;
                c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                //guna2GradientPanel6.Location = new Point(1077, 83);
                //guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);
                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);

            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                if (guna2GradientPanel2.Visible == true)
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel2.SendToBack();
                }
                else
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = true;
                    guna2GradientPanel2.BringToFront();
                }

                cmbDocumento.Enabled = false;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                btnSiguiente.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                btnSiguiente.Enabled = false;

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                //guna2GradientPanel6.Location = new Point(1077, 83);
                //guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);
                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {

                ReporteOrdenCompra reporteOrdenCompra = new ReporteOrdenCompra();
                reporteOrdenCompra.ShowDialog();

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                //guna2GradientPanel6.Location = new Point(1077, 83);
                //guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);
                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);
            }
            else if (e.ClickedItem.Text == "ENVIAR CORREO")
            {

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                //guna2GradientPanel6.Location = new Point(1077, 83);
                //guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);
                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);
            }
            else if (e.ClickedItem.Text == "AUTORIZAR")
            {

                AutentificarAdmin autentificarAdmin = new AutentificarAdmin();
                autentificarAdmin.ShowDialog();

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                //guna2GradientPanel6.Location = new Point(1077, 83);
                //guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);
                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);
            }
        }

        private void toolStrip2_Move(object sender, EventArgs e)
        {

        }

        private void toolStrip2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void toolStrip2_MouseLeave(object sender, EventArgs e)
        {
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro1.Text == string.Empty)
            {
                c.BuscarProveedor(guna2DataGridView2);
            }
            else
            {
                c.BuscarAlumnosFiltro(guna2DataGridView2, txtFiltro1.Text);
            }
        }

        private void guna2DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                M2 = guna2DataGridView2.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
                txtMatricular.Text = guna2DataGridView2.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
                guna2GradientPanel5.Visible = false;

            }
            else
            {
                return;
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel5.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("La pantalla se limpiará, ¿Desea continuar?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Limpiar();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            //Utilerias.ValidarFormatoMoneda;

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }

        }

        /// <summary>
        /// A diferencia de la versión anterior (que sólo sabía resolver
        /// contra el catálogo de Productos), ahora primero se revisa
        /// cmbTipo: si es "Servicio", se resuelve contra DBServicios
        /// (nuevo, sin existencias ni pedidos pendientes). Si es "Producto"
        /// (o viene vacío por compatibilidad), el comportamiento es EL MISMO
        /// de siempre, sin tocar una sola línea de esa lógica.
        /// </summary>
        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
                return;

            if (cmbTipo.Text == "Servicio")
            {
                string[] valoresServicio = srv.InformacionServicio(cmbConcepto.Text);
                if (valoresServicio == null)
                    return;

                txtConcepto2.Text = valoresServicio[0];
                txtConcepto.Text = valoresServicio[1];
                txtPrecio.Text = valoresServicio[2];
                txtUnidad.Text = valoresServicio[3];
                txtImpuesto1.Text = valoresServicio[4];
                txtDescuento1.Text = valoresServicio[5];
                txtCantidad.Text = "1";

                // Un Servicio no maneja inventario.
                lblExistencias.Text = "N/D";
                lblPedidosProveedor.Text = "0";
                lblPedidosCliente.Text = "0";
                lblDisponible.Text = "N/D";
                txtCostoUnitario.Text = "0.00";
                txtCantidadP.Text = string.Empty;
            }
            else if (!tipo.Equals("Remision"))
            {
                string[] valores = c.InformacionRecibo(cmbConcepto.Text, txtFolioPedido.Text);
                txtConcepto2.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtPrecio.Text = valores[11];
                txtUnidad.Text = valores[3];
                txtImpuesto1.Text = valores[4];
                txtDescuento1.Text = valores[12];
                lblExistencias.Text = valores[5];
                lblPedidosProveedor.Text = valores[7];
                lblPedidosCliente.Text = valores[8];
                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                //txtImporte1.Text = sub.ToString();
                txtCantidad.Text = "1";
            }


            else if (tipo.Equals("Remision"))
            {
                string[] valores = c.InformacionPartidaOrden(cmbConcepto.Text, txtFolioPedido.Text);

                txtConcepto2.Text = valores[0];
                //txtConcepto.Text = valores[1];
                txtPrecio.Text = valores[12];
                txtCostoUnitario.Text = valores[13];
                txtUnidad.Text = valores[3];
                txtImpuesto1.Text = valores[5];
                txtDescuento1.Text = valores[4];
                lblExistencias.Text = valores[7];
                lblPedidosProveedor.Text = valores[8];
                lblPedidosCliente.Text = valores[9];
                txtCantidadP.Text = string.IsNullOrEmpty(valores[11]) ? "" : valores[11];
                decimal valorDecimal = Convert.ToDecimal(txtCantidadP.Text);
                int cantidad = Convert.ToInt16(valorDecimal) == 0 ? 1 : Convert.ToInt16(valorDecimal);
                txtCantidad.Text = cantidad.ToString();
                txtImporte1.Text = valores[2];

            }
            Calcular();



        }

        /// <summary>
        /// Llena cmbConcepto según cmbTipo:
        ///   - "Servicio" (nuevo): DBServicios.ObtenerProductosGasto().
        ///   - "Producto" (histórico, SIN CAMBIOS): exactamente la misma
        ///     lógica de siempre (SeleccionarProducto2 / SeleccionarProductoOrdenPedido,
        ///     según si la partida está o no vinculada a un Pedido a Cliente).
        /// Si cmbTipo no tiene selección, el combo queda vacío.
        /// </summary>
        private void CargarComboConceptos()
        {
            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

            if (cmbTipo.Text == "Servicio")
            {
                DataTable dtServicios = srv.ObtenerProductosGasto();
                ComboUtil.LlenarComboBox(cmbConcepto, dtServicios, "Descripcion", "ClaveServicio");
            }
            else if (cmbTipo.Text == "Producto")
            {
                if (string.IsNullOrEmpty(txtFolioPedido.Text))
                {
                    c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
                }
                else
                {
                    c.SeleccionarProductoOrdenPedido(cmbConcepto, txtFolioPedido.Text);
                }
            }
            else
            {
                cmbConcepto.DataSource = null;
                cmbConcepto.Items.Clear();
            }

            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboConceptos();
            txtConcepto2.Clear();
            txtConcepto.Clear();
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";
            txtUnidad.Clear();
            lblExistencias.Text = "0";
            lblPedidosProveedor.Text = "0.00";
            lblPedidosCliente.Text = "0.00";
            lblDisponible.Text = "0.00";
            txtCostoUnitario.Text = "0.00";
            txtCantidadP.Text = string.Empty;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }
            PanelPartidasRequisicion.Visible = true;

            ConfigurarPartida(false);

            // cmbTipo arranca sin selección en cada partida nueva: obliga a
            // elegir Producto o Servicio antes de poder elegir el concepto.
            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;

            cmbConcepto.Text = "";
            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();

            txtCantidad.Text = "1";
            txtUnidad.Text = "";
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00"; ;
            txtImpuesto1.Text = "0";

            if (tipo == "Remision")
            {

                o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);

            }
            else
            {

                c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);

            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void Calcular()
        {
            decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtCantidad.Text);
            txtImporte1.Text = Convert.ToString(sub);
            decimal descuento = (Convert.ToDecimal(txtDescuento1.Text) / 100) * Convert.ToDecimal(sub);
            txtDescuentoIm.Text = descuento.ToString("N2");
            sub = sub - descuento;
            txtSubtotal1.Text = sub.ToString("N2");
            decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(sub);
            txtImpuestoIm.Text = Impuesto.ToString("N2");
            txtTotal1.Text = (Convert.ToDecimal(sub) + Impuesto).ToString("N2");
        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void Moneda(ref Guna.UI2.WinForms.Guna2TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporte1);

            //try
            //{
            //    if (txtSubtotal.Text != string.Empty)
            //    {
            //        decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(txtSubtotal1.Text);
            //        txtImpuestoIm.Text = Impuesto.ToString("N2");
            //        txtTotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
            //    }
            //    else if (txtSubtotal.Text == string.Empty)
            //    {
            //        txtSubtotal1.Text = "0.00";
            //    }
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Formato de subtotal incorrecto");
            //}

        }

        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento1);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }

        }

        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {

            Utilerias.Moneda2(ref txtImpuesto1);


            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoIm);
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtTotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotalR);
        }

        private void txtDescuentoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoR);
        }

        private void txtTotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotalR);
        }

        void LimpiarPartida()
        {
            txtCantidad.Text = "1";
            txtUnidad.Clear();
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImporte1.Text = "0.00";
            txtImpuesto1.Text = "0.00";
            txtImpuestoIm.Text = "0.00";

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;

            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();
            cmbConcepto.SelectedIndex = -1;
            lblExistencias.Text = "0";
            lblPedidosCliente.Text = "0.00";
            lblPedidosProveedor.Text = "0.00";
            lblDisponible.Text = "0.00";
            txtCostoUnitario.Text = "0.00";
            txtConcepto2.Text = string.Empty;

            //c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
        }

        private void toolStrip2_MouseEnter(object sender, EventArgs e)
        {
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Doble clic sobre una partida para editarla.
        ///
        /// CAMBIO DE FIRMA REQUERIDO EN DBOrdenCompra.ConsultaPartidaOrden y
        /// DBPedidoCliente.ConsultaPartidaOrdenPedido: ya NO reciben el
        /// ComboBox cmbConcepto (antes lo recibían y le asignaban
        /// SelectedValue directamente); en su lugar regresan "out string
        /// tipoConcepto" y "out string claveConcepto" crudos. Esto es
        /// necesario porque ahora cmbConcepto puede alimentarse de dos
        /// catálogos distintos (Productos o Servicios) según TipoConcepto, y
        /// decidir cuál cargar antes de poder seleccionar el valor correcto
        /// le corresponde al formulario, no a la capa de datos (ver
        /// "DBOrdenCompra_DBPedidoCliente_MetodosAfectados.cs").
        /// </summary>
        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();

                cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
                cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

                string tipoConcepto;
                string claveConcepto;

                if (tipo == "Remision")
                {
                    o.ConsultaPartidaOrden(TxtFolio2.Text, Partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtImporte1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtEntregado, out tipoConcepto, out claveConcepto);

                }
                else
                {
                    c.ConsultaPartidaOrdenPedido(TxtFolio2.Text, Partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtImporte1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtEntregado, out tipoConcepto, out claveConcepto);

                }

                // Partidas capturadas antes de este cambio no tendrán
                // TipoConcepto (columna nueva); se asume "Producto" por ser
                // el comportamiento histórico de este formulario.
                cmbTipo.Text = string.IsNullOrEmpty(tipoConcepto) ? "Producto" : tipoConcepto;
                CargarComboConceptos();

                if (!string.IsNullOrEmpty(claveConcepto) && int.TryParse(claveConcepto, out int claveNumerica))
                {
                    cmbConcepto.SelectedValue = claveNumerica;
                }

                cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

                if (cmbTipo.Text == "Servicio")
                {
                    // Un Servicio no maneja existencias ni pedidos pendientes.
                    lblExistencias.Text = "N/D";
                    lblPedidosProveedor.Text = "0";
                    lblPedidosCliente.Text = "0";
                    txtCantidadP.Text = string.Empty;
                }
                else
                {
                    // Producto: comportamiento histórico, SIN CAMBIOS (antes
                    // esta misma consulta se hacía justo después de que el
                    // método de arriba dejaba seleccionado cmbConcepto).
                    string[] valores = c.InformacionRecibo(cmbConcepto.Text, "");
                    lblExistencias.Text = valores[5];
                    lblPedidosProveedor.Text = valores[7];
                    lblPedidosCliente.Text = valores[8];
                    txtCantidadP.Text = string.IsNullOrEmpty(valores[10]) ? "" : valores[10];
                }

                PanelPartidasRequisicion.Visible = true;

                txtPartida.Text = Partida;
                // panel2.Visible = false;
                if (cmbEstatus.Text.Equals("Abierto", StringComparison.OrdinalIgnoreCase) & tipo != "Remision")
                {
                    ConfigurarPartida(false); // Habilitar

                }
                else
                {
                    ConfigurarPartida(true); // Bloquear
                }
            }
            else
            {
                return;
            }
        }
        private void ConfigurarPartida(bool bloquear)
        {
            // Mostrar/ocultar etiquetas y paneles según el estado
            label56.Visible = bloquear;
            txtEntregado.Visible = bloquear;

            // Cambiar estado de botones
            guna2Button5.Visible = !bloquear; // Botones visibles si no está bloqueado
            guna2Button6.Visible = !bloquear;
            guna2Button7.Visible = !bloquear;
            btnSiguiente.Visible = !bloquear;

            // Cambiar estado de los campos
            txtCantidad.Enabled = !bloquear;
            txtUnidad.Enabled = !bloquear;
            txtPrecio.Enabled = !bloquear;
            txtDescuento1.Enabled = !bloquear;
            txtImpuesto1.Enabled = !bloquear;
            cmbTipo.Enabled = !bloquear;
            cmbConcepto.Enabled = !bloquear;
            txtConcepto2.Enabled = !bloquear;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            else if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar una orden de compra Bloqueada o Cancelada");
                return;
            }

            else if (MessageBox.Show("Al confirmar la orden de compra no podra realizar modificaciones, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Matricula = string.Empty;
                cmbEstatus.Text = "Bloqueado";
                if (tipo == "Remision")
                {
                    string almacen = cmbAlmacen.Text.Split('-')[0];

                    // Igual que ya quedó en Facturas: sólo las partidas de
                    // tipo 'Producto' cuyo ProductosServicios.Inventariable =
                    // 'Si' generan salida de almacén (las de 'Servicio', y
                    // las de Producto no inventariable, se excluyen desde la
                    // propia consulta - no tienen existencia física que
                    // mover). El proceso de salida ya NO se reimplementa
                    // aquí: se delega por completo en
                    // DBRegistrarEntradas.GenerarSalidaAlmacen, el mismo
                    // método que ya usa Facturas, que internamente valida/da
                    // de alta el TipoMovimiento ('S'/'SPR'), crea el
                    // encabezado, descuenta existencias y cierra el
                    // movimiento. Si no hay ninguna partida inventariable no
                    // se genera ningún MovimientoInventario (antes siempre
                    // se creaba uno, incluso vacío).
                    //
                    // CAMBIO REQUERIDO: agrega DBPedidoCliente.ObtenerPartidasInventariables
                    // (ver DBOrdenCompra_DBPedidoCliente_MetodosAfectados.cs).
                    List<List<string>> partidasInventariables = o.ObtenerPartidasInventariables(TxtFolio2.Text);
                    string folio = string.Empty;
                    if (partidasInventariables.Count > 0)
                    {
                        folio = movInventario.GenerarSalidaAlmacen(
                            partidasInventariables,
                            almacen,
                            "SPR",
                            "SALIDA POR REMISIÓN",
                            txtFecha.Text,
                            cmbEstatus.Text,
                            txtDivisa.Text,
                            txtTipoCambio.Text,
                            txtTotal.Text.Replace(",", ""),
                            txtNotas.Text,
                            txtElaborado.Text,
                            "Remisión " + TxtFolio2.Text);
                    }

                    o.ActualizarReciboEstatus(TxtFolio2.Text, "Bloqueado", "", folio);

                    MessageBox.Show("Se realizo exitosamente la salida");
                    if (!string.IsNullOrEmpty(txtFolioPedido.Text))
                    {
                        bool pendiente = c.PartidasPendientesOrdenPedido(txtFolioPedido.Text);
                        if (!pendiente)
                        {
                            c.ActualizarReciboEstatus(txtFolioPedido.Text, "Cerrada");

                        }

                    }

                }
                else
                {
                    c.ActualizarReciboEstatus(TxtFolio2.Text, "Bloqueado");


                }

                if (MessageBox.Show("¿Imprimir Documento?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (tipo == "Remision")
                    {
                        ReporteRemision r = new ReporteRemision(txtFolio.Text, txtMatricular.Text);
                        r.ShowDialog();
                    }
                    else
                    {
                        ReporteOrdenPedidoCliente r = new ReporteOrdenPedidoCliente(txtFolio.Text, txtMatricular.Text);
                        r.ShowDialog();
                    }

                }

                Limpiar();
                LimpiarPartida();
                if (tipo == "Remision")
                {
                    o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                    o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
                }
                else
                {
                    c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                    c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                }
                guna2TabControl1.SelectedIndex = 0;
                guna2Button9.Visible = false;

                guna2Button1.Visible = false;
                guna2Button1.Visible = false;

            }

        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (tipo == "Remision")
            {
                o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
            }
            else
            {
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (tipo == "Remision")
            {
                o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
            }
            else
            {
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (tipo == "Remision")
            {
                o.CargarRemisiones(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                o.CargarRemisiones(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
            }
            else
            {
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                string cliente = string.Empty;
                if (tipo == "Remision")
                {
                    o.ConsultaRemision(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtSaldo, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, txtFolioPedido, d, out cliente, cmbCentroCostos, cmbproyecto);
                }
                else
                {
                    c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, out cliente);

                }
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                cmbAlmacen.Enabled = false;
                TxtFolio2.Text = txtFolio.Text;
                //  button3.Enabled = false;
                //       button7.Enabled = true;
                //   //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = cliente;
                Matricula = cliente;
                //   panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                //   groupBox2.Enabled = true;
                guna2GradientPanel2.Visible = false;


                CargarPartidas();
                if (cmbEstatus.Text != "Abierto")
                {
                    // Sólo lectura: se deja el combo sin catálogo cargado
                    // (nada se va a insertar); si antes mostraba el
                    // catálogo de Productos por defecto, ya no es
                    // necesario porque cmbTipo decide qué mostrar.
                    cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
                    cmbTipo.SelectedIndex = -1;
                    cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
                    cmbConcepto.DataSource = null;
                    cmbConcepto.Items.Clear();
                }

            }
            else
            {
                return;
            }
        }

        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cliente = string.Empty;
            if (e.RowIndex != -1)
            {
                string Folio = DataGridView2.Rows[e.RowIndex].Cells["Folio2"].Value.ToString();
                txtFolio.Text = "X";
                if (tipo == "Remision")
                {
                    o.ConsultaRemision(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtSaldo, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, txtFolioPedido, d, out cliente, cmbCentroCostos, cmbproyecto);
                }
                else
                {
                    c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, out cliente);

                }
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                TxtFolio2.Text = txtFolio.Text;
                //button3.Enabled = false;

                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = cliente;
                Matricula = cliente;
                //panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                //groupBox2.Enabled = true;
                guna2GradientPanel2.Visible = false;
                c.CargarOrdenPedidoClientePartidas(guna2DataGridView1, TxtFolio2.Text);

            }
            else
            {
                return;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LimpiarPartida();
        }

        void CambioTamañotoolstripPequeño()
        {
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            if (guna2GradientPanel2.Visible)
            {
                guna2GradientPanel2.Visible = false;

            }
            else
            {
                guna2GradientPanel2.Visible = true;

            }
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;
            toolStripButton5.Visible = true;


            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            //guna2GradientPanel6.Location = new Point(1077, 83);
            //guna2GradientPanel6.Size = new Size(23, 569);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);
        }
        private void CalcularDisponibilidad()
        {
            lblDisponible.Text = (Convert.ToDecimal(lblExistencias.Text) - Convert.ToDecimal(lblPedidosCliente.Text)).ToString();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            if (guna2Panel2.Visible)
            {
                guna2Panel2.Visible = false;
            }
            else
            {
                guna2Panel2.Visible = true;
            }
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidadP.Text) && tipo.Equals("Remision") && !string.IsNullOrEmpty(txtFolioPedido.Text))
            {
                decimal cantidadp = Convert.ToDecimal(txtCantidadP.Text);
                decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
                if (cantidad > cantidadp)
                {
                    MessageBox.Show("la cantidad no puede ser mayor a la cantidad pendiente");
                    txtCantidad.Text = "";
                }
            }
        }

        private void btnDocumento_Click(object sender, EventArgs e)
        {
            BuscarDocumento b = new BuscarDocumento("OrdenPedido", txtMatricular.Text);
            b.ShowDialog();
            if (!string.IsNullOrEmpty(BuscarDocumento.FolioO))
            {

                txtFolioPedido.Text = BuscarDocumento.FolioO;
                d.Text = BuscarDocumento.DocumentoO + "-" + BuscarDocumento.Conscutivo;// + " - " + BuscarDocumento.Nombre;
                string[] datos = c.InformacionOrdenPedido(txtFolioPedido.Text);
                string[] datosAlmacen = a.InformacionAlmacen(datos[3]);
                txtMatricular.Text = datos[2];
                txtNotas.Text = datos[5];
                cmbAlmacen.Text = datosAlmacen[0] + " - " + datosAlmacen[1];
                cmbAlmacen.Enabled = false;
                btnCliente.Enabled = false;

            }
        }

        private void CalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;

                int Dias = Convert.ToInt32(diasv);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");


            }
            catch (Exception)
            {

                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }
        }
        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            CalcularFechaVencimiento();
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            //CalcularFechaVencimiento();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            LimpiarPartida();
            Limpiar();
            guna2DataGridView1.Rows.Clear();
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;
            txtNotas.Enabled = true;
            cmbAlmacen.Enabled = true;

            cmbDocumento.Focus();

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(TxtFolio2.Text))
                {
                    MessageBox.Show("Es necesario crear el encabezado");
                    guna2TabControl1.SelectedIndex = 0;
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtFolio2.Text) && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nuevo Orden Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }
            Limpiar();

            cmbDocumento.Enabled = true;
            cmbAlmacen.Enabled = true;
            txtDiasVence.Enabled = true;
            txtFecha.Enabled = true;
            txtNotas.Enabled = true;
            cmbDocumento.DroppedDown = true;
            btnCliente.Enabled = true;
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            if (guna2GradientPanel2.Visible)
            {
                guna2GradientPanel2.Visible = false;

            }
            else
            {
                guna2GradientPanel2.Visible = true;

            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            if (tipo == "Remision")
            {

            }
            else
            {
                p.EliminarOrdenCliente(TxtFolio2.Text, txtPartida.Text);
                MessageBox.Show(c.EliminarPartidaOrdenPedidoCliente(TxtFolio2.Text, txtPartida.Text));
                c.CargarOrdenPedidoClientePartidas(dataGridView1, TxtFolio2.Text);
                string maximo = c.ObtenerTotalPartidasOrdenPedidoCliente(TxtFolio2.Text);
                c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, maximo);
                c.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);


            }
            ConfigurarPartida(true);
            LimpiarPartida();
            CargarPartidas();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (tipo == "Remision")
                {
                    if (string.IsNullOrEmpty(txtFolio.Text))
                    {
                        MessageBox.Show("Es necesario seleccionar una remisión");
                        return;
                    }

                    if (cmbEstatus.Text == "Abierto")
                    {
                        MessageBox.Show("Es necesario que la remisión esté bloqueada");
                        return;
                    }

                    using (ReporteRemision r = new ReporteRemision(
                        txtFolio.Text,
                        txtMatricular.Text))
                    {
                        r.ShowDialog();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtFolio.Text))
                    {
                        MessageBox.Show("Es necesario seleccionar una orden de pedido cliente");
                        return;
                    }

                    using (ReporteOrdenPedidoCliente r = new ReporteOrdenPedidoCliente(
                        txtFolio.Text,
                        txtMatricular.Text))
                    {
                        r.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtSubtotal1_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSubtotal1);
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la recepcion de productos");
                return;
            }

            else if ((cmbEstatus.Text != "Bloqueado" && cmbEstatus.Text != "Abierto"))
            {
                MessageBox.Show("No es posible cancelar un orden de pedido cliente que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El orden de pedido cliente será cancelado, ¿Desea continuar?", "Orden de pedido cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                o.CancelarOrdenPedidoCliente(txtFolio.Text);
                cmbEstatus.Text = "Cancelado";

                Limpiar();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty && txtFolio.Text != string.Empty)
            {
                string tipo = string.Empty;
                string[] valores = cl.InformacionCliente(txtMatricular.Text);

                if (tipo == "Remision")
                {
                    tipo = "Remisión Vitalvet";

                    ReporteRemision r = new ReporteRemision(txtFolio.Text, txtMatricular.Text);
                    string carpeta = Utilerias.SavePDF(r.reportViewer1, "Remision", txtDocumento.Text, txtConsecutivo.Text);
                    bool enviado = CorreosMasivos.EnviarCorreos(
                     tipo,
                     @"<html>
                        <body style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>
                            <p>Estimado Socio Comercial,</p>

                            <p>
                                Enviamos la remisión de su pedido confirmado.<br/>
                                Su factura se emitirá a la brevedad y será enviada a su correo registrado.
                            </p>

                            <p>
                                Si tiene alguna duda o requiere realizar algún ajuste, no dude en contactar a su vendedor,
                                quien estará encantado en asistirle.
                            </p>

                            <p>
                                Reciban un cordial saludo y que tengan un excelente día.
                            </p>
                        </body>
                      </html>",
                     Utilerias.ConvertirReportViewerAPdf(r.reportViewer1),
                     "Remision-" + txtConsecutivo.Text + ".pdf",
                     valores[5]);
                    if (enviado)
                    {
                        MessageBox.Show("Correo enviado exitosamente");
                    }

                }
                else
                {
                    tipo = "Orden Pedido Cliente";
                    ReporteOrdenPedidoCliente r = new ReporteOrdenPedidoCliente(txtFolio.Text, txtMatricular.Text);
                    string carpeta = Utilerias.SavePDF(r.reportViewer1, "Orden Pedido Cliente", txtDocumento.Text, txtConsecutivo.Text);
                    bool enviado = CorreosMasivos.EnviarCorreos(
                                tipo,
                                "",
                                Utilerias.ConvertirReportViewerAPdf(r.reportViewer1),
                                "OrdenPedido-" + txtConsecutivo.Text + ".pdf",
                                valores[5]);
                    if (enviado)
                    {
                        MessageBox.Show("Correo enviado exitosamente");
                    }
                }

            }
            else
            {
                MessageBox.Show("Seleccione la requisicion para enviar el correo");
            }
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtFecha_ValueChanged(object sender, EventArgs e)
        {
            CalcularFechaVencimiento();
        }

        private void txtPartidas_TextChanged(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto")
            {
                if (!string.IsNullOrEmpty(txtPartidas.Text) && txtPartidas.Text != "0")
                    guna2Button9.Visible = true;
            }
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OrdenPedidoCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            o.CerrarConexion();
        }

        private void cmbCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dtProyectos = dp.ObtenerProyectosPorCentroCostos(
       cmbCentroCostos.Text
                );

            ComboUtil.LlenarComboBox(
                cmbproyecto,
                dtProyectos,
                "Proyecto",
                "Id"
            );

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void btnRemisionXML_Click(object sender, EventArgs e)
        {
            try
            {
                string folio = txtFolio.Text.Trim(); // ajusta al control real donde tienes el folio

                if (string.IsNullOrWhiteSpace(folio))
                {
                    MessageBox.Show("Debes indicar el folio de la remisión.");
                    return;
                }

                DBRemiision db = new DBRemiision();
                string xml = db.GenerarXmlRemision(folio);

                if (string.IsNullOrEmpty(xml))
                {
                    MessageBox.Show("No se encontró la remisión con ese folio.");
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivo XML (*.xml)|*.xml";
                    sfd.FileName = $"Remision_{folio}.xml";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(sfd.FileName, xml, System.Text.Encoding.UTF8);
                        MessageBox.Show("XML generado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar XML: " + ex.ToString());
            }
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtRecargo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtRecargo);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }
    }
}