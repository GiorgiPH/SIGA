using Condominios.Clases.CentroCostos;
using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.Almacenes;
using PV.Clases.CentroCostos;
using PV.Clases.Clientes;
using PV.Clases.Facturas;
using PV.Clases.PedidoCliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Registro de Facturas. Adaptado de OrdenPedidoCliente.cs, conservando
    /// únicamente la rama de lógica equivalente a "Remision" (por eso el
    /// parecido casi total en nombres de método/control), y retirando por
    /// completo la vinculación a Pedido a Cliente (txtFolioPedido, el botón
    /// de documento vinculado "d"/btnDocumento/label66, y la validación de
    /// cantidad pendiente contra un pedido). Si tu Designer copiado todavía
    /// tiene esos controles, esta clase simplemente no los referencia; puedes
    /// dejarlos ocultos o quitarlos del diseñador cuando gustes.
    /// </summary>
    public partial class Facturas : Form
    {
        public static string Matricula = string.Empty;
        public static string M2 = string.Empty;
        public static int Opcion = 0;

        // Específico de Facturas (Factura / PartidaFactura)
        DBFacturas f = new DBFacturas();

        // Compartidas/genéricas: se siguen usando igual que en OrdenPedidoCliente
        // porque no dependen de la tabla Remision/Factura (catálogo de
        // documentos, información de producto, movimientos de almacén,
        // búsqueda de clientes, etc.)
        DBPedidoCliente c = new DBPedidoCliente();
        DBAlmacenes a = new DBAlmacenes();
        DBClientes cl = new DBClientes();
        DBCentroCostos cc = new DBCentroCostos();
        DBDatosProyecto dp = new DBDatosProyecto();

        private bool mostrarCentroCosto = false;

        public Facturas()
        {
            InitializeComponent();

            ToolTip T = new ToolTip();
            label18.Text = "Factura";
            T.SetToolTip(guna2Button15, "Nueva Factura");
            T.SetToolTip(guna2Button16, "Consultar Factura");
            T.SetToolTip(button10, "Imprimir Factura");
            T.SetToolTip(btnCliente, "Buscar Cliente");

            // Controles heredados del copiado de OrdenPedidoCliente que eran
            // específicos de la vinculación con Pedido a Cliente: no aplican
            // en Facturas.
            if (d != null) d.Visible = false;
            if (btnDocumento != null) btnDocumento.Visible = false;
            if (label66 != null) label66.Visible = false;

            c.BuscarProveedor(guna2DataGridView2);
        }

        #region Ciclo de vida del formulario

        private void Facturas_Load(object sender, EventArgs e)
        {
            f.CargarFacturas(dataGridView1, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
            f.CargarFacturas(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);

            c.SeleccionarConceptoDocumentoV(cmbDocumento, "Factura");
            a.SeleccionarAlmacen(cmbAlmacen);

            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;

            txtDiasVence.Text = "0";
            RecalcularFechaVencimiento();

            ConfigurarToolStripExpandido();
            LlenarComboCentro();
        }

        private void Facturas_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X" && !string.IsNullOrEmpty(txtFolio.Text))
            {
                f.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
            }

            if (Opcion == 1)
            {
                txtAutoriza.Text = DBRegistrarIngresos.usuario;
                txtFechaAuto.Text = DateTime.Today.ToString("yyyy/MM/dd");
                f.ActualizarFacturaAutorizacion(txtFolio.Text, txtAutoriza.Text, txtFechaAuto.Text);
                MessageBox.Show("Factura Autorizada");
                Limpiar();

                f.CargarFacturas(dataGridView1, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                f.CargarFacturas(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
            }
        }

        private void Facturas_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nada que cerrar: cada método de DBFacturas abre y cierra su
            // propia conexión, no se mantiene ninguna conexión persistente
            // a nivel de formulario.
        }

        #endregion

        #region Catálogo de documento / consecutivo

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
            if (txtFolio.Text == "X" || cmbDocumento.Text == string.Empty)
                return;

            string[] valores = c.InformacionDocumento(cmbDocumento.Text);
            txtDocumento.Text = valores[0];
            txtClave.Text = valores[1];

            if (txtFolio.Text == string.Empty)
            {
                if (valores.Length > 2)
                {
                    mostrarCentroCosto = ParsearBooleano(valores[2]);
                    cmbCentroCostos.Enabled = mostrarCentroCosto;
                }

                c.ConsecutivoCompra(txtConsecutivo, txtClave.Text);
            }

            txtDiasVence.Focus();
        }

        #endregion

        #region Cliente

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty)
                return;

            string[] valores = cl.InformacionCliente(txtMatricular.Text);
            txtNombreAlumnno.Text = valores[1];
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

        private void guna2DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            M2 = guna2DataGridView2.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
            txtMatricular.Text = M2;
            guna2GradientPanel5.Visible = false;
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel5.Visible = false;
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro1.Text == string.Empty)
                c.BuscarProveedor(guna2DataGridView2);
            else
                c.BuscarAlumnosFiltro(guna2DataGridView2, txtFiltro1.Text);
        }

        #endregion

        #region Encabezado - alta / limpieza

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al cliente antes de continuar");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una factura Bloqueada o Cancelada");
                return;
            }
            if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }
            if (string.IsNullOrEmpty(cmbAlmacen.Text))
            {
                MessageBox.Show("Selecciona un almacén");
                return;
            }

            string almacen = cmbAlmacen.Text.Split('-')[0];

            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                string centroCosto = null;
                string proyecto = ComboUtil.ObtenerSelectedValue(cmbproyecto);

                if (mostrarCentroCosto)
                {
                    centroCosto = ComboUtil.ObtenerSelectedValue(cmbCentroCostos);

                    if (string.IsNullOrWhiteSpace(centroCosto))
                    {
                        MessageBox.Show("Seleccione un centro de costos antes de continuar.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                f.InsertarFactura(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text,
                    txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text,
                    txtElaborado.Text, txtConsecutivo.Text, almacen, centroCosto, proyecto);
            }

            cmbAlmacen.Enabled = false;
            TxtFolio2.Text = txtFolio.Text;
            guna2TabControl1.SelectedIndex = 1;

            c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
            f.Consulta5Factura(TxtFolio2.Text, txtPartida);

            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";

            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
        }

        void Limpiar()
        {
            txtFolio.Clear();
            TxtFolio2.Clear();
            txtConsecutivo.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            cmbAlmacen.Enabled = false;
            txtFecha.Enabled = false;
            btnCliente.Enabled = false;
            txtNotas.Enabled = false;

            Matricula = string.Empty;
            Opcion = 0;
            txtAutoriza.Clear();
            txtFechaAuto.Clear();

            cmbDocumento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            cmbConcepto.SelectedIndex = -1;
            cmbAlmacen.SelectedIndex = -1;
            cmbCentroCostos.SelectedIndex = -1;
            cmbproyecto.SelectedIndex = -1;

            PanelPartidasRequisicion.Visible = false;
            guna2TabControl1.SelectedIndex = 0;
            guna2DataGridView1.Rows.Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La pantalla se limpiará, ¿Desea continuar?", "Documento",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar();
            }
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

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtFolio2.Text) && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nueva Factura",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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

        #endregion

        #region Partidas - captura

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }

            PanelPartidasRequisicion.Visible = true;
            ConfigurarPartida(false);

            cmbConcepto.Text = "";
            txtCantidad.Text = "1";
            txtUnidad.Text = "";
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";

            c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
            f.Consulta5Factura(TxtFolio2.Text, txtPartida);
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
                return;

            // Facturas no maneja vinculación a Pedido a Cliente: siempre se
            // usa la consulta genérica de producto (equivalente a la rama
            // "sin folio de pedido" del formulario original).
            string[] valores = c.InformacionRecibo(cmbConcepto.Text, string.Empty);
            txtConcepto2.Text = valores[0];
            txtConcepto.Text = valores[1];
            txtPrecio.Text = valores[11];
            txtUnidad.Text = valores[3];
            txtImpuesto1.Text = valores[4];
            txtDescuento1.Text = valores[12];
            lblExistencias.Text = valores[5];
            lblPedidosProveedor.Text = valores[7];
            lblPedidosCliente.Text = valores[8];
            txtCantidad.Text = "1";

            Calcular();
        }

        private bool ValidarExistencias()
        {
            decimal existencias;
            decimal cantidad;

            if (!decimal.TryParse(lblExistencias.Text, out existencias))
            {
                MessageBox.Show("No fue posible obtener las existencias del producto seleccionado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("La cantidad capturada no es válida.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidad.Focus();
                return false;
            }

            if (cantidad > existencias)
            {
                MessageBox.Show("No hay suficiente inventario del producto seleccionado.", "Inventario insuficiente",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return false;
            }

            return true;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TerminarCapturaPartidas();
                }
                return;
            }

            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TerminarCapturaPartidas();
                }
                return;
            }

            if (!GuardarPartidaActual())
                return;

            CargarPartidas();
            PanelPartidasRequisicion.Visible = false;
            guna2Button9.Visible = true;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }

            if (!GuardarPartidaActual())
                return;

            f.ReciboSaldosPartidas(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
            c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
            CargarPartidas();
            LimpiarPartida();
        }

        /// <summary>Inserta la partida actualmente capturada y refresca los totales del encabezado.</summary>
        private bool GuardarPartidaActual()
        {
            if (!ValidarExistencias())
                return false;

            f.InsertarPartidaFactura(
                TxtFolio2.Text,
                txtPartida.Text,
                txtConcepto2.Text,
                txtConcepto2.Text,
                txtCantidad.Text,
                txtUnidad.Text,
                txtDivisa1.Text,
                txtTipoCambio1.Text,
                Convert.ToDecimal(txtImporte1.Text),
                Convert.ToDecimal(txtDescuento1.Text),
                Convert.ToDecimal(txtTotal1.Text),
                Convert.ToDecimal(txtPrecio.Text),
                Convert.ToDecimal(txtImpuesto1.Text));

            f.ActualizarTotalesFactura(TxtFolio2.Text, txtPartida.Text);
            return true;
        }

        private void TerminarCapturaPartidas()
        {
            int partida = Convert.ToInt32(txtPartida.Text) - 1;
            if (partida > 0)
            {
                f.ActualizarTotalesFactura(TxtFolio2.Text, partida.ToString());
            }
            PanelPartidasRequisicion.Visible = false;
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LimpiarPartida();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            string mensaje = f.EliminarPartidaFactura(TxtFolio2.Text, txtPartida.Text);
            if (!string.IsNullOrEmpty(mensaje))
            {
                MessageBox.Show(mensaje);
            }

            string totalPartidas = f.ObtenerTotalPartidasFactura(TxtFolio2.Text);
            f.ActualizarTotalesFactura(TxtFolio2.Text, totalPartidas);
            f.ReciboSaldosPartidas(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);

            ConfigurarPartida(true);
            LimpiarPartida();
            CargarPartidas();
        }

        private void CargarPartidas()
        {
            f.CargarPartidasFactura(guna2DataGridView1, TxtFolio2.Text);
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
            cmbConcepto.SelectedIndex = -1;
            lblExistencias.Text = "0";
            lblPedidosCliente.Text = "0.00";
            lblPedidosProveedor.Text = "0.00";
            lblDisponible.Text = "0.00";
            txtCostoUnitario.Text = "0.00";
            txtConcepto2.Text = string.Empty;
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            string partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();

            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

            f.ConsultaPartidaFactura(TxtFolio2.Text, partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad,
                txtUnidad, txtDivisa1, txtTipoCambio1, txtImporte1, txtDescuento1, txtTotal1, txtPrecio,
                txtImpuesto1, txtEntregado, cmbConcepto);

            string[] valores = c.InformacionRecibo(cmbConcepto.Text, string.Empty);

            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

            lblExistencias.Text = valores[5];
            lblPedidosProveedor.Text = valores[7];
            lblPedidosCliente.Text = valores[8];

            PanelPartidasRequisicion.Visible = true;
            txtPartida.Text = partida;

            ConfigurarPartida(!cmbEstatus.Text.Equals("Abierto", StringComparison.OrdinalIgnoreCase));
        }

        private void ConfigurarPartida(bool bloquear)
        {
            label56.Visible = bloquear;
            txtEntregado.Visible = bloquear;

            guna2Button5.Visible = !bloquear;
            guna2Button6.Visible = !bloquear;
            guna2Button7.Visible = !bloquear;
            btnSiguiente.Visible = !bloquear;

            txtCantidad.Enabled = !bloquear;
            txtUnidad.Enabled = !bloquear;
            txtPrecio.Enabled = !bloquear;
            txtDescuento1.Enabled = !bloquear;
            txtImpuesto1.Enabled = !bloquear;
            cmbConcepto.Enabled = !bloquear;
            txtConcepto2.Enabled = !bloquear;
        }

        #endregion

        #region Cálculo de importes por partida (idéntico al original)

        private void Calcular()
        {
            decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtCantidad.Text);
            txtImporte1.Text = Convert.ToString(sub);

            decimal descuento = (Convert.ToDecimal(txtDescuento1.Text) / 100) * sub;
            txtDescuentoIm.Text = descuento.ToString("N2");

            sub -= descuento;
            txtSubtotal1.Text = sub.ToString("N2");

            decimal impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * sub;
            txtImpuestoIm.Text = impuesto.ToString("N2");

            txtTotal1.Text = (sub + impuesto).ToString("N2");
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento1);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImpuesto1);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSubtotal1);
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoIm);
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal1);
        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e) => Moneda(ref txtSubtotalR);
        private void txtDescuentoR_TextChanged(object sender, EventArgs e) => Moneda(ref txtDescuentoR);
        private void txtTotalR_TextChanged(object sender, EventArgs e) => Moneda(ref txtTotalR);

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e) => c.Monto(e);
        private void txtTotal1_KeyPress(object sender, KeyPressEventArgs e) => c.Monto(e);

        private void Moneda(ref Guna2TextBox txt)
        {
            try
            {
                string n = txt.Text.Replace(",", "").Replace(".", "");
                n = n.PadLeft(3, '0');
                double v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Vencimiento

        private void RecalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;
                int dias = Convert.ToInt32(diasv);
                DateTime fechaVence = Convert.ToDateTime(txtFecha.Text);
                fechaVence = fechaVence.AddDays(dias);
                txtFechaVence.Text = fechaVence.ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e) => RecalcularFechaVencimiento();
        private void txtFecha_ValueChanged(object sender, EventArgs e) => RecalcularFechaVencimiento();

        #endregion

        #region Confirmación (bloqueo) + salida de almacén

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar una factura Bloqueada o Cancelada");
                return;
            }
            if (MessageBox.Show("Al confirmar la factura no podra realizar modificaciones, ¿Desea continuar?",
                "Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            Matricula = string.Empty;
            cmbEstatus.Text = "Bloqueado";

            string almacen = cmbAlmacen.Text.Split('-')[0];
            List<List<string>> lista = f.ObtenerPartidas(TxtFolio2.Text);
            string folioMovimiento = IngresarAlmacen(lista, almacen);
            f.ActualizarFacturaEstatus(TxtFolio2.Text, "Bloqueado", "", folioMovimiento);

            MessageBox.Show("Se realizo exitosamente la salida");

            if (MessageBox.Show("¿Imprimir Documento?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ReporteFactura r = new ReporteFactura(txtFolio.Text, txtMatricular.Text);
                r.ShowDialog();
            }

            Limpiar();
            LimpiarPartida();

            f.CargarFacturas(dataGridView1, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
            f.CargarFacturas(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);

            guna2TabControl1.SelectedIndex = 0;
            guna2Button9.Visible = false;
        }

        /// <summary>
        /// Genera el movimiento de salida de almacén para la Factura
        /// confirmada. Se reutiliza tal cual la infraestructura genérica de
        /// movimientos (ValidarDocumentoSPR / RegistroMovimientoInventario /
        /// RegistroProductoSalidas / RegistroPartida / ActualizarMovimientoJ)
        /// que ya usa OrdenPedidoCliente para Remision, únicamente cambiando
        /// el origen de las partidas a f.ObtenerPartidas (PartidaFactura).
        /// </summary>
        private string IngresarAlmacen(List<List<string>> lista, string almacen)
        {
            TextBox t = new TextBox();
            c.ValidarDocumentoSPR();

            string folio = c.RegistroMovimientoInventario("", "S", "SPR", txtFecha.Text, cmbEstatus.Text, "",
                almacen, lista.Count.ToString(), txtDivisa.Text, txtTipoCambio.Text.Replace(",", ""),
                txtTotal.Text.Replace(",", ""), txtNotas.Text, txtElaborado.Text, t, "", "", "", "", "");

            for (int i = 0; i < lista.Count; i++)
            {
                string clave = lista[i][0];
                string cantidad = lista[i][1];
                string precio = lista[i][3];
                string unidad = lista[i][5];
                string total = lista[i][6];

                c.RegistroProductoSalidas(clave, cantidad.Replace(",", ""), almacen);
                c.RegistroPartida(t.Text, "S", "SPR", (i + 1).ToString(), clave, cantidad.Replace(",", ""), unidad,
                    Convert.ToDecimal(precio.Replace(",", "")), txtDivisa.Text, txtTipoCambio.Text,
                    Convert.ToDecimal(total.Replace(",", "")), "");
            }

            c.ActualizarMovimientoJ(folio, "S", "SPR", "");
            return folio;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la factura");
                return;
            }
            if (cmbEstatus.Text != "Bloqueado" && cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible cancelar una factura que no esta bloqueada o abierta");
                return;
            }
            if (MessageBox.Show("La factura será cancelada, ¿Desea continuar?", "Factura",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                f.CancelarFactura(txtFolio.Text);
                cmbEstatus.Text = "Cancelado";
                Limpiar();
            }
        }

        #endregion

        #region Consulta / filtros / grillas de encabezado

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            CargarEncabezadoDesdeGrilla(dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString());
        }

        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            CargarEncabezadoDesdeGrilla(DataGridView2.Rows[e.RowIndex].Cells["Folio2"].Value.ToString());
        }

        private void CargarEncabezadoDesdeGrilla(string folio)
        {
            txtFolio.Text = "X";

            f.ConsultaFactura(folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa,
                txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado,
                txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, out string cliente,
                cmbCentroCostos, cmbproyecto);

            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            txtNotas.Enabled = false;
            cmbAlmacen.Enabled = false;
            TxtFolio2.Text = txtFolio.Text;

            txtMatricular.Text = cliente;
            Matricula = cliente;

            string[] valores = c.InformacionDocumento2(txtClave.Text);
            txtDocumento.Text = valores[0];
            txtClave.Text = valores[1];
            cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

            guna2GradientPanel2.Visible = false;
            CargarPartidas();

            if (cmbEstatus.Text != "Abierto")
            {
                c.SeleccionarProducto2(cmbConcepto, "");
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e) => RecargarGrillasFacturas();
        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e) => RecargarGrillasFacturas();
        private void txtFiltroNombre_TextChanged(object sender, EventArgs e) => RecargarGrillasFacturas();

        private void RecargarGrillasFacturas()
        {
            f.CargarFacturas(dataGridView1, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
            f.CargarFacturas(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
        }

        #endregion

        #region Imprimir / Enviar correo

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                MessageBox.Show("Es necesario seleccionar una factura");
                return;
            }

            ReporteFactura r = new ReporteFactura(txtFolio.Text, txtMatricular.Text);
            r.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty || txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la factura para enviar el correo");
                return;
            }

            string[] valores = cl.InformacionCliente(txtMatricular.Text);

            ReporteFactura r = new ReporteFactura(txtFolio.Text, txtMatricular.Text);
            string carpeta = Utilerias.SavePDF(r.reportViewer1, "Factura", txtDocumento.Text, txtConsecutivo.Text);

            bool enviado = CorreosMasivos.EnviarCorreos(
                "Factura",
                @"<html>
                    <body style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>
                        <p>Estimado Socio Comercial,</p>
                        <p>Enviamos su factura correspondiente.</p>
                        <p>Si tiene alguna duda o requiere realizar algún ajuste, no dude en contactar a su vendedor,
                           quien estará encantado en asistirle.</p>
                        <p>Reciban un cordial saludo y que tengan un excelente día.</p>
                    </body>
                  </html>",
                Utilerias.ConvertirReportViewerAPdf(r.reportViewer1),
                "Factura-" + txtConsecutivo.Text + ".pdf",
                valores[5]);

            if (enviado)
            {
                MessageBox.Show("Correo enviado exitosamente");
            }
        }

        #endregion

        #region Centro de costos / proyecto

        private void LlenarComboCentro()
        {
            try
            {
                DataTable centros = cc.ConsultarTodos();
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostos.DataSource = centros;
                cmbCentroCostos.DisplayMember = "Nombre";
                cmbCentroCostos.ValueMember = "Clave";
                cmbCentroCostos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtProyectos = dp.ObtenerProyectosPorCentroCostos(cmbCentroCostos.Text);
            ComboUtil.LlenarComboBox(cmbproyecto, dtProyectos, "Proyecto", "Id");
        }

        #endregion

        #region Barra lateral (toolStrip2) - menú NUEVO / CONSULTAR / IMPRIMIR / ENVIAR CORREO / AUTORIZAR

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    OnMenuNuevo();
                    break;
                case "CONSULTAR":
                    guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
                    if (guna2GradientPanel2.Visible) guna2GradientPanel2.BringToFront(); else guna2GradientPanel2.SendToBack();
                    ConfigurarConsulta();
                    break;
                case "IMPRIMIR":
                    ReporteFactura reporte = new ReporteFactura(txtFolio.Text, txtMatricular.Text);
                    reporte.ShowDialog();
                    break;
                case "ENVIAR CORREO":
                    button11_Click(sender, e);
                    break;
                case "AUTORIZAR":
                    AutentificarAdmin autentificarAdmin = new AutentificarAdmin();
                    autentificarAdmin.ShowDialog();
                    break;
            }

            ConfigurarToolStripCompacto();
        }

        private void OnMenuNuevo()
        {
            if (cmbEstatus.Text != "Abierto")
            {
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
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
                MessageBox.Show("Confirme la factura antes de continuar");
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

            f.CargarPartidasFactura(guna2DataGridView1, TxtFolio2.Text);
        }

        private void ConfigurarConsulta()
        {
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
        }

        /// <summary>
        /// Ajusta tamaños/orientación del toolStrip lateral cuando está
        /// contraído. Se factoriza en un único método porque en el
        /// formulario original este mismo bloque se repetía de forma
        /// idéntica más de media docena de veces.
        /// </summary>
        private void ConfigurarToolStripCompacto()
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;

            foreach (var boton in new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 })
            {
                boton.TextDirection = ToolStripTextDirection.Vertical270;
                boton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                boton.Size = new Size(23, 79);
            }
        }

        private void ConfigurarToolStripExpandido()
        {
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;

            foreach (var boton in new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 })
            {
                boton.TextDirection = ToolStripTextDirection.Horizontal;
                boton.DisplayStyle = ToolStripItemDisplayStyle.Image;
                boton.Size = new Size(85, 75);
                boton.AutoSize = false;
                boton.Visible = true;
            }

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
            ConfigurarToolStripExpandido();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            ConfigurarToolStripCompacto();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1 && string.IsNullOrEmpty(TxtFolio2.Text))
            {
                MessageBox.Show("Es necesario crear el encabezado");
                guna2TabControl1.SelectedIndex = 0;
            }
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            guna2Panel2.Visible = !guna2Panel2.Visible;
        }

        private void txtPartidas_TextChanged(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && !string.IsNullOrEmpty(txtPartidas.Text) && txtPartidas.Text != "0")
            {
                guna2Button9.Visible = true;
            }
        }

        #endregion

        #region Compatibilidad con el Designer copiado de OrdenPedidoCliente

        // El Facturas.Designer.cs quedó tal cual del copy-paste de
        // OrdenPedidoCliente.Designer.cs, así que sigue enganchando eventos a
        // nombres de método que aquí renombramos o que ya no hacen nada
        // (porque eran no-ops en el original, o dependían de la lógica de
        // Pedido a Cliente que se retiró). En vez de editar el .Designer.cs
        // a mano (generado por el diseñador, fácil de romper si luego abres
        // el formulario en modo diseño), se agregan aquí los métodos que el
        // Designer espera encontrar.

        // Eventos del formulario que renombramos:
        private void OrdenCompra2_Load(object sender, EventArgs e) => Facturas_Load(sender, e);
        private void OrdenCompra2_Activated(object sender, EventArgs e) => Facturas_Activated(sender, e);
        private void OrdenPedidoCliente_FormClosing(object sender, FormClosingEventArgs e) => Facturas_FormClosing(sender, e);

        // Handlers que en el original eran no-ops (no hacían nada) y que el
        // Designer sigue teniendo enganchados:
        private void label9_Click(object sender, EventArgs e) { }
        private void label37_Click(object sender, EventArgs e) { }
        private void label49_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e) { }
        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e) { }
        private void txtNombreAlumnno_TextChanged(object sender, EventArgs e) { }
        private void txtTipoCambio_TextChanged(object sender, EventArgs e) { }
        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e) { }

        // El Designer registró el TextChanged de txtSubtotal1 dos veces con
        // nombres distintos (típico de Visual Studio cuando el evento se
        // engancha más de una vez desde el panel de Propiedades). Con que
        // exista, basta:
        private void txtSubtotal1_TextChanged_1(object sender, EventArgs e) { }

        // btnDocumento ya no se usa (no hay vinculación a Pedido a Cliente en
        // Facturas) pero sigue oculto en el formulario copiado; se deja el
        // handler vacío para que compile. Si prefieres, quita el control del
        // Designer y este método también.
        private void btnDocumento_Click(object sender, EventArgs e) { }

        // txtCantidad_Leave validaba cantidad contra un pedido vinculado;
        // Facturas no maneja esa vinculación, así que queda vacío.
        private void txtCantidad_Leave(object sender, EventArgs e) { }

        #endregion
    }
}