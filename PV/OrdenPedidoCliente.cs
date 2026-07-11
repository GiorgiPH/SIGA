using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.Almacenes;
using PV.Clases.Clientes;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;
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
    public partial class OrdenPedidoCliente : Form
    {

        public static string Matricula = string.Empty;
        public static string M2 = string.Empty;
        public static int Opcion = 0;


        DBPedidoCliente c = new DBPedidoCliente();
        DBOrdenCompra o = new DBOrdenCompra();
        DBAlmacenes a = new DBAlmacenes();
        DBClientes cl = new DBClientes();
        DBProductosServicios p = new DBProductosServicios();
        string recibo = string.Empty;
        string reciboCol = string.Empty;
        string tipo=string.Empty;

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
            }
            c.SeleccionarConceptoDocumentoV(cmbDocumento, tipo);
            
            
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

                    o.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, txtConcepto2.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    o.ActualizarTotalesRemision(TxtFolio2.Text, txtPartida.Text);
                    if (!string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendientePartidaOrden(cant.ToString(), cant.ToString(), txtFolioPedido.Text, txtConcepto2.Text);

                        p.RegistroPedidosCliente(txtConcepto2.Text, cantneg.ToString().Replace(",", ""));
                    }
                    ConceptosGlobalesPartidaRemision cg = new ConceptosGlobalesPartidaRemision(txtFolio.Text, "", "");
                    cg.ShowDialog();

                }
                else
                {

                    string mensaje = c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, txtConcepto2.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show(mensaje);
                    }
                    c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, txtPartida.Text);
                    p.RegistroPedidosCliente(txtConcepto2.Text, txtCantidad.Text);


                }
                CargarPartidas();
                PanelPartidasRequisicion.Visible = false;
                guna2Button9.Visible = true;
            }

        }
        private bool ValidarExistencias()
        {
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

                    o.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, txtConcepto2.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    o.ActualizarTotalesRemision(TxtFolio2.Text, txtPartida.Text);

                    o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);
                    o.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
                    //o.ReciboSaldosPartidasOrden2(TxtFolio2.Text, txtImpuestoR);

                    if (!string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendientePartidaOrden(cant.ToString(), cant.ToString(), txtFolioPedido.Text, txtConcepto2.Text);
                        
                        p.RegistroPedidosCliente(txtConcepto2.Text, cantneg.ToString().Replace(",", ""));



                    }
                   
                }
                else
                {
                    string mensaje=c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, txtConcepto2.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtImporte1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show(mensaje);
                    }
                    c.ActualizarTotalesOrdenPedidoCliente(TxtFolio2.Text, txtPartida.Text);
                    c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
                    c.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
                    p.RegistroPedidosCliente(txtConcepto2.Text, txtCantidad.Text);


                }


                if (string.IsNullOrEmpty(txtFolioPedido.Text))
                {
                    c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
                }
                else
                {
                    c.SeleccionarProductoOrdenPedido(cmbConcepto, txtFolioPedido.Text);

                }
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
                        o.InsertarRemision(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, almacen, txtFolioPedido.Text);

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

                if (string.IsNullOrEmpty(txtFolioPedido.Text))
                {
                    c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
                }
                else
                {
                    c.SeleccionarProductoOrdenPedido(cmbConcepto, txtFolioPedido.Text);

                }

                if (tipo == "Remision")
                {
                    o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);                 
                }
                else
                {
                    c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
                }
                txtCantidad.Text = "1";
                txtUnidad.Text = "Servicio";
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
            txtMatricular.Clear ();
            txtNombreAlumnno.Clear ();
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
            cmbConcepto.SelectedIndex = -1;
            
            cmbAlmacen.SelectedIndex= -1;
            PanelPartidasRequisicion.Visible = false;
            guna2TabControl1.SelectedIndex = 0;
            guna2DataGridView1.Rows.Clear();
            txtFolioPedido.Text=string.Empty;
            d.Clear();
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

            /* guna2GradientPanel4.Size = new Size(80, 569);
             toolStrip2.Size = new Size(112, 569);
             toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
             toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
             toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
             toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
             toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            */
            //  toolStripButton1.Size = new Size(50, 60);
       /*     guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton15.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;

            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton14.Size = new Size(85, 75);
            toolStripButton14.AutoSize = false;

            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton15.Size = new Size(85, 75);
            toolStripButton15.AutoSize = false;

            */
        }

        private void toolStrip2_MouseLeave(object sender, EventArgs e)
        {
            /*guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip2.Size = new Size(22, 569);
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            */
           /* guna2GradientPanel4.Location = new Point(1076, 83);
            guna2GradientPanel4.Size = new Size(23, 569);

            toolStrip1.Size = new Size(23, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton15.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);

            // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);


            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);


            //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton14.Size = new Size(23, 79);

            // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton15.Size = new Size(23, 79);*/
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

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (!tipo.Equals("Remision"))
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
                    int cantidad = Convert.ToInt16(valorDecimal)==0?1: Convert.ToInt16(valorDecimal);
                    txtCantidad.Text = cantidad.ToString();
                    txtImporte1.Text = valores[2];

                }
                Calcular();
                

            }
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

            cmbConcepto.Text = "";

            txtCantidad.Text = "1";
            txtUnidad.Text = "";
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";;
            txtImpuesto1.Text = "0";
            if (string.IsNullOrEmpty(txtFolioPedido.Text))
            {
                c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
            }
            else
            {
                c.SeleccionarProductoOrdenPedido(cmbConcepto, txtFolioPedido.Text);

            }
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
            txtImporte1.Text= "0.00";
            txtImpuesto1.Text = "0.00";
            txtImpuestoIm.Text = "0.00";
            cmbConcepto.SelectedIndex=-1;
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
            /*    guna2GradientPanel4.Size = new Size(80, 569);
                toolStrip2.Size = new Size(112, 569);
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;*/

           /* guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton15.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;

            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton14.Size = new Size(85, 75);
            toolStripButton14.AutoSize = false;

            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton15.Size = new Size(85, 75);
            toolStripButton15.AutoSize = false;*/


        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
                cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
                if (tipo == "Remision")
                {
                    o.ConsultaPartidaOrden(TxtFolio2.Text, Partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtImporte1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtEntregado, cmbConcepto);

                }
                else
                {
                    c.ConsultaPartidaOrdenPedido(TxtFolio2.Text, Partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtImporte1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtEntregado, cmbConcepto);

                }
                string[] valores = c.InformacionRecibo(cmbConcepto.Text, "");
                
               
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
                lblExistencias.Text = valores[5];
                lblPedidosProveedor.Text = valores[7];
                lblPedidosCliente.Text = valores[8];
                txtCantidadP.Text = string.IsNullOrEmpty(valores[10]) ? "" : valores[10];
                PanelPartidasRequisicion.Visible = true;

                txtPartida.Text = Partida;
                // panel2.Visible = false;
                if (cmbEstatus.Text.Equals("Abierto", StringComparison.OrdinalIgnoreCase) & tipo != "Remision" )
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
            else if (guna2DataGridView1.Rows.Count==0)
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
                    //MessageBox.Show(TxtFolio2.Text);
                    List<List<string>> lista = c.ObtenerPartidas(TxtFolio2.Text);
                    string folio=IngresarAlmacen(lista, almacen);
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
        private string IngresarAlmacen(List<List<string>> lista, string almacen)
        {
            TextBox t = new TextBox();
            c.ValidarDocumentoSPR();

            string folio=c.RegistroMovimientoInventario("", "S", "SPR", txtFecha.Text, cmbEstatus.Text, "", almacen, lista.Count.ToString(), txtDivisa.Text, txtTipoCambio.Text.Replace(",", ""), txtTotal.Text.Replace(",", ""), txtNotas.Text, txtElaborado.Text, t, "", "", "", "", "");

            for (int i = 0; i < lista.Count; i++)
            {
                string clave = lista[i][0];
                string cantidad = lista[i][1];
                string costeo = lista[i][2];
                string precio = lista[i][3];
                string partidaOrden = lista[i][4];
                string unidad = lista[i][5];
                string total = lista[i][6];
                c.RegistroProductoSalidas(clave, cantidad.Replace(",", ""), almacen);
                if (!string.IsNullOrEmpty(txtFolio.Text))
                {
                    //c.ActualizarPartidaOrden(txtFolio.Text, partidaOrden, cantidad.Replace(",", ""), clave);
                }


                //Realiza el movimiento inventario
                c.RegistroPartida(t.Text, "S", "SPR", (i + 1).ToString(), clave, cantidad.Replace(",", ""), unidad, Convert.ToDecimal(precio.Replace(",", "")), txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(total.Replace(",", "")), "");
            }
            c.ActualizarMovimientoJ(folio, "S", "SPR", "");
            return folio;

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
                    o.ConsultaRemision(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, txtFolioPedido, d, out cliente);
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
                guna2GradientPanel2.Visible= false;


                CargarPartidas();
                if (cmbEstatus.Text != "Abierto")
                {
                    c.SeleccionarProducto2(cmbConcepto, "");
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
                    o.ConsultaRemision(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto, cmbAlmacen, txtFolioPedido, d, out cliente);
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
         /*   this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton14.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton15.Size = new Size(23, 79);*/

     
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
                string[] datos=c.InformacionOrdenPedido(txtFolioPedido.Text);
                string[] datosAlmacen = a.InformacionAlmacen(datos[3]);
                txtMatricular.Text= datos[2];
                txtNotas.Text = datos[5];
                cmbAlmacen.Text= datosAlmacen[0]+" - "+datosAlmacen[1];
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
            cmbDocumento.Enabled=true;
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
            if(!string.IsNullOrEmpty(TxtFolio2.Text) && cmbEstatus.Text == "Abierto")
            {
                if(MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nuevo Orden Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes){
                    return;
                }
            }
            Limpiar();
           
            cmbDocumento.Enabled = true;
            cmbAlmacen.Enabled=true;
            txtDiasVence.Enabled=true;
            txtFecha.Enabled = true;
            txtNotas.Enabled=true;
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
            if (tipo == "Remision")
            {
                if (string.IsNullOrEmpty(txtFolio.Text))
                {
                    MessageBox.Show("Es necesario selecionar una remisión");
                    return;
                }
                if (cmbEstatus.Text == "Abierto")
                {
                    MessageBox.Show("Es necesario que la remisión este bloqueada");
                    return;
                }
                ReporteRemision r = new ReporteRemision(txtFolio.Text, txtMatricular.Text);
                r.ShowDialog();
            }
            else
            {
                if (string.IsNullOrEmpty(txtFolio.Text))
                {
                    MessageBox.Show("Es necesario seleccionar una orden de pedido cliente");
                    return;
                }
                //if (cmbEstatus.Text == "Abierto")
                //{
                //    MessageBox.Show("Es necesario que la orden de pedido cliente este bloqueada");
                //    return;
                //}
                ReporteOrdenPedidoCliente r = new ReporteOrdenPedidoCliente(txtFolio.Text, txtMatricular.Text);
                r.ShowDialog();

                
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
                if(!string.IsNullOrEmpty(txtPartidas.Text) && txtPartidas.Text!="0")
                guna2Button9.Visible = true;
            }
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    }
    
    



