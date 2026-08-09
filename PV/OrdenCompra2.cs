using Condominios.Clases.RegistrarIngresos;
using PuntoVentas.Clases.Login;
using PV.Clases.OrdenCompra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class OrdenCompra2 : Form
    {

        public static string Matricula = string.Empty;
        public static string M2 = string.Empty;
        public static int Opcion = 0;


        DBOrdenCompra c = new DBOrdenCompra();

        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public OrdenCompra2()
        {
            InitializeComponent();
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
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(DataGridView2);
            }
        }

        private void OrdenCompra2_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoDocumento(cmbDocumento);
            c.CargarRecibos(dataGridView1);
            c.CargarRecibos2(DataGridView2);
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

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
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
            c.BuscarProveedor(guna2DataGridView2);
            guna2GradientPanel5.Visible = true;

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
                c.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                Limpiar();
                c.Consulta5(TxtFolio2.Text, txtPartida);
                c.ReciboSaldosPartidasOrden(TxtFolio2.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
                //c.ReciboSaldosPartidasOrden2(TxtFolio2.Text, txtImpuestoR);
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
                        c.ActualizarTotalesRemision(TxtFolio2.Text, Partida.ToString());
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
                        c.ActualizarTotalesRemision(TxtFolio2.Text, Partida.ToString());
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
                c.InsertarPartidaRemision(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto1.Text));
                c.ActualizarTotalesRemision(TxtFolio2.Text, txtPartida.Text);
                //    this.Close();
            }
            PanelPartidasRequisicion.Visible = false;
            c.CargarRecibosPartidas(guna2DataGridView1, TxtFolio2.Text);
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
            else
            {
                string ReciboCol = string.Empty;

                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarOrden(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, "0", "");
                    //    MessageBox.Show(txtFolio.Text);

                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                //PartidasOrden partidas = new PartidasOrden(txtFolio.Text, txtReciboInsc.Text, ReciboCol);
                //partidas.ShowDialog();


                guna2TabControl1.SelectedIndex = 1;
                TxtFolio2.Text = txtFolio.Text;
                recibo = txtReciboInsc.Text;
                reciboCol = ReciboCol;

                c.SeleccionarProducto2(cmbConcepto);
                c.Consulta5(TxtFolio2.Text, txtPartida);
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
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                txtClave.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtPrecio.Text = valores[2];
                txtUnidad.Text = valores[3];
                txtImpuesto1.Text = valores[4];

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
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";
            c.SeleccionarProducto2(cmbConcepto);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(sub);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal1.Text = (Convert.ToDecimal(sub) + Impuesto - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                    txtSubtotal1.Text = Convert.ToString(sub);
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
            c.SeleccionarProducto2(cmbConcepto);
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
                c.ActualizarReciboEstatus(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text, "");
                Limpiar();
                c.CargarRecibos(dataGridView1);
            }
            guna2TabControl1.SelectedIndex = 0;
            guna2Button9.Visible = false;
        }

        private void 
            _Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltro(dataGridView1, txtFiltro.Text);
                c.CargarRecibosFiltro2(DataGridView2, txtFiltro.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(DataGridView2);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocumento(dataGridView1, txtFiltroDocumento.Text);
                c.CargarRecibosFiltroDocumento2(DataGridView2, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(DataGridView2);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroP(dataGridView1, txtFiltroNombre.Text);
                c.CargarRecibosFiltroP2(DataGridView2, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(DataGridView2);
            }
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
                //  button3.Enabled = false;
                //       button7.Enabled = true;
                //   //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                Matricula = DBOrdenCompra.MatriculaC;
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
                //button3.Enabled = false;

                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                Matricula = DBOrdenCompra.MatriculaC;
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
            c.SeleccionarProducto2(cmbConcepto);
        }

       

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
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
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
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

    }
    
    



