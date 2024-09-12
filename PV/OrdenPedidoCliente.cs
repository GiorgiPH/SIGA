using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;
using SqlServerTypes;
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
        DBProductosServicios p = new DBProductosServicios();
        string recibo = string.Empty;
        string reciboCol = string.Empty;
        string tipo=string.Empty;

        public OrdenPedidoCliente(string tipo)
        {
            InitializeComponent();
            this.tipo = tipo;
            if (tipo == "Remision")
            {
                label18.Text = "Remisión";
                
            }
            else
            {
                d.Visible = false;
                btnDocumento.Visible = false;
                label18.Text = "Pedidos a Cliente";

            }
            c.BuscarProveedor(guna2DataGridView2);
        }

        private void OrdenCompra2_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                // txtMatricular.Text = Matricula;

                c.ReciboSaldos(txtFolio.Text, txtSubtotal1, txtDescuento1, txtRecargo, txtTotal1, txtPartidas);
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
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            }
        }

        private void OrdenCompra2_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoDocumentoV(cmbDocumento, tipo);
            c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            c.SeleccionarAlmacen(cmbAlmacen);
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
                        c.ConsecutivoCompra(txtConsecutivo, txtClave.Text);
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
            txtMatricular.Text = M2;
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores = c.InformacionPropietarioRecibo(txtMatricular.Text);
                txtNombreAlumnno.Text = valores[1];
            }



        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (guna2GradientPanel5.Visible)
            {
                guna2GradientPanel5.Visible=false;
            }
            else
            {
                guna2GradientPanel5.Visible = true;
            }
            
           

        }

        private void button2_Click(object sender, EventArgs e)
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
                if (tipo == "Remisión")
                {
                    o.InsertarPartida(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    Limpiar();
                    o.Consulta5OrdenCompra(TxtFolio2.Text, txtPartida);
                    o.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR);
                    o.ReciboSaldosPartidasOrden2(TxtFolio2.Text, txtImpuestoR);

                    if (!string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendiente(cantneg.ToString(), cant.ToString(), txtFolioPedido.Text, txtClave.Text);
                        
                        p.RegistroPedidosCliente(txtClave.Text, cantneg.ToString().Replace(",", ""));



                    }
                    
                }
                else
                {
                    



                    c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    Limpiar();
                    c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
                    c.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR);
                    c.ReciboSaldosPartidasOrden2(TxtFolio2.Text, txtImpuestoR);
                    
                }
         
                
                
            
            }
            LimpiarPartida();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        if (tipo == "Remisión")
                        {
                            c.ActualizarOrden(TxtFolio2.Text, Partida.ToString());

                        }
                        else
                        {
                            c.ActualizarOrdenCliente(TxtFolio2.Text, Partida.ToString());

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
                        if (tipo == "Remisión")
                        {
                            c.ActualizarOrden(TxtFolio2.Text, Partida.ToString());

                        }
                        else
                        {
                            c.ActualizarOrdenCliente(TxtFolio2.Text, Partida.ToString());

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
                if (tipo == "Remisión")
                {
                    o.InsertarPartida(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    o.ActualizarOrden(TxtFolio2.Text, txtPartida.Text);
                    if (!string.IsNullOrEmpty(txtFolioPedido.Text))
                    {

                        decimal cant = Convert.ToDecimal(txtCantidad.Text);
                        decimal cantneg = Convert.ToDecimal(txtCantidad.Text) * -1;
                        c.ActualizaCantidadPendiente(cantneg.ToString(), cant.ToString(), txtFolioPedido.Text, txtClave.Text);

                        p.RegistroPedidosCliente(txtClave.Text, cantneg.ToString().Replace(",", ""));



                    }

                }
                else
                {

                    c.InsertarPartidaOrdenCliente(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                    c.ActualizarOrdenCliente(TxtFolio2.Text, txtPartida.Text);
                    p.RegistroPedidosCliente(txtClave.Text, txtCantidad.Text);
                    

                }
                //    this.Close();
            }
            if (tipo == "Remisión")
            {
                o.CargarRecibosPartidas(guna2DataGridView1, TxtFolio2.Text);


            }
            else
            {
                c.CargarRecibosPartidas(guna2DataGridView1, TxtFolio2.Text);

            }
            PanelPartidasRequisicion.Visible = false;
            guna2Button9.Visible = true;
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

                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarOrden(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, almacen, tipo);
                    //    MessageBox.Show(txtFolio.Text);

                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                //PartidasOrden partidas = new PartidasOrden(txtFolio.Text, txtReciboInsc.Text, ReciboCol);
                //partidas.ShowDialog();

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
                
               
                c.Consulta5OrdenCliente(TxtFolio2.Text, txtPartida);
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
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtTotal.Clear();
            // txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            txtConsecutivo.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            cmbAlmacen.Enabled = false;
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
            lblExistencias.Text = "0";
            lblPedidosCliente.Text = "0.00";
            lblPedidosProveedor.Text= "0.00";
            lblDisponible.Text= "0.00";
            cmbAlmacen.SelectedIndex= -1;
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
                guna2Button8.Visible = true;

                guna2Button2.Enabled = true;
                guna2Button5.Enabled = true;
                guna2Button6.Enabled = true;
                guna2Button7.Enabled = true;
                guna2Button8.Enabled = true;

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
                guna2Button8.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                guna2Button8.Enabled = false;

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
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el recibo");
                return;
            }
            else if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una orden de compra que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El saldo de esta orden de compra sera cancelado, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Cancelado";
                c.ActualizarRecibo3(txtFolio.Text, cmbEstatus.Text);

                //c.ActualizarReciboEstatus(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                Limpiar();
                //MessageBox.Show("Orden de Compra Cancelado");
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

            if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
            {
                Calcular();
            }
            /*  try
              {
                  if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                  {
                      if (Convert.ToInt32(txtCantidad.Text) < 0)
                      {
                          MessageBox.Show("No es posible registrar un cantidad menor a 0");
                      }
                      else
                      {
                          decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                          txtSubtotal1.Text = sub.ToString();

                          decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(txtSubtotal1.Text);
                          txtImpuestoIm.Text = Impuesto.ToString("N2");
                          txtTotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                      }

                  }
              }
              catch (Exception)
              {

                  MessageBox.Show("Formato de cantidad incorrecto");
              }*/
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo(cmbConcepto.Text, txtFolioPedido.Text);
                txtClave.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtPrecio.Text = valores[2];
                txtUnidad.Text = valores[3];
                txtImpuesto1.Text = valores[4];
                lblExistencias.Text = valores[5];
                lblPedidosProveedor.Text = valores[7];
                lblPedidosCliente.Text = valores[8];
                txtCantidadP.Text = string.IsNullOrEmpty(valores[10]) ? "" : valores[10]; 

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal1.Text = sub.ToString();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = true;
            txtPartida.Enabled = false;
            txtCantidad.Enabled = true;
            txtUnidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtDescuento1.Enabled = true;
            txtImpuesto1.Enabled = true;

            guna2Button5.Visible = false;

            guna2Button6.Visible = true;
            guna2Button7.Visible = true;
            guna2Button8.Visible = true;

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
            decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
            decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(sub);
            txtImpuestoIm.Text = Impuesto.ToString("N2");
            txtTotal1.Text = (Convert.ToDecimal(sub) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
            txtSubtotal1.Text = Convert.ToString(sub);
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
            c.Monto(e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal1);

            try
            {
                if (txtSubtotal.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(txtSubtotal1.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                }
                else if (txtSubtotal.Text == string.Empty)
                {
                    txtSubtotal1.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de subtotal incorrecto");
            }

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
                if (txtDescuento.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(txtSubtotal1.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                }
                else if (txtDescuento.Text == string.Empty)
                {
                    txtDescuento1.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de descuento incorrecto");
            }

        }

        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            /*    try
                {
                    if (txtImpuesto1.Text != string.Empty)
                    {
                        decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(txtSubtotal1.Text);
                        txtImpuestoIm.Text = Impuesto.ToString("N2");
                        txtTotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                    }
                    else if (txtImpuesto1.Text == string.Empty)
                    {
                        txtImpuesto1.Text = "0";
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Foramto de impuesto incorrecto");
                }*/
            Utilerias.Moneda2(ref txtImpuesto1);
        }

        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
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
            //    txtSubtotal1.Text= "0.00";
            txtImpuesto1.Text = "0";
            txtImpuestoIm.Text = "0";
            cmbConcepto.Text = "";
            c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
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
                c.ConsultaPartida(TxtFolio2.Text, Partida, cmbconcepto2, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtSubtotal1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtEntregado);
                txtPartida.Text = Partida;
                // panel2.Visible = false;
                label56.Visible = true;
                txtEntregado.Visible = true;
                PanelPartidasRequisicion.Visible = Visible;
                guna2Button11.Visible = true;
                guna2Button5.Visible = false;
                guna2Button6.Visible = false;
                guna2Button7.Visible = false;
                guna2Button8.Visible = false;



                txtCantidad.Enabled = false;
                txtUnidad.Enabled = false;
                txtPrecio.Enabled = false;
                txtDescuento1.Enabled = false;
                txtImpuesto1.Enabled = false;
                guna2Button5.Visible = false;
                guna2Button6.Visible = false;
                guna2Button7.Visible = false;
                guna2Button8.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            else if (txtPartidas.Text == string.Empty)
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
                if (tipo == "Remisión")
                {
                    string almacen = cmbAlmacen.Text.Split('-')[0];
                    //MessageBox.Show(TxtFolio2.Text);
                    List<List<string>> lista = c.ObtenerPartidas(TxtFolio2.Text);
                    IngresarAlmacen(lista, almacen);
                    o.ActualizarReciboEstatus(TxtFolio2.Text, "Bloqueado", "");

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

           

                Limpiar();
                c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            }
            guna2TabControl1.SelectedIndex = 0;
            guna2Button9.Visible = false;
        }
        private void IngresarAlmacen(List<List<string>> lista, string almacen)
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

        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            c.CargarRecibos(dataGridView1, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            c.CargarRecibos2(DataGridView2, tipo, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;

                TxtFolio2.Text = txtFolio.Text;
                //  button3.Enabled = false;
                //       button7.Enabled = true;
                //   //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBPedidoCliente.MatriculaC;
                Matricula = DBPedidoCliente.MatriculaC;
                //   panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                //   groupBox2.Enabled = true;
                guna2GradientPanel2.Visible= false;
            }
            else
            {
                return;
            }
        }

        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = DataGridView2.Rows[e.RowIndex].Cells["Folio2"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                TxtFolio2.Text = txtFolio.Text;
                //button3.Enabled = false;

                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBPedidoCliente.MatriculaC;
                Matricula = DBPedidoCliente.MatriculaC;
                //panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                //groupBox2.Enabled = true;
                guna2GradientPanel2.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            cmbConcepto.Text = "";
            
            txtCantidad.Text = "1";
            txtUnidad.Text = "";
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";
            c.SeleccionarProducto2(cmbConcepto, TxtFolio2.Text);
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
            return;
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
            if (!string.IsNullOrEmpty(txtCantidadP.Text))
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
            BuscarDocumento b = new BuscarDocumento("OrdenPedido");
            b.ShowDialog();
            if (!string.IsNullOrEmpty(BuscarDocumento.FolioO))
            {
                txtFolioPedido.Text = BuscarDocumento.FolioO;
                d.Text = BuscarDocumento.DocumentoO + "-" + BuscarDocumento.Conscutivo;// + " - " + BuscarDocumento.Nombre;
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
    }

    }
    
    



