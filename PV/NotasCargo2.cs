using PV.Clases.OrdenCompra;
using PuntoVentas.Clases.Login;
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
    public partial class NotasCargo2 : Form
    {

        DBOrdenCompra c = new DBOrdenCompra();
        public static string Matricula = string.Empty;
        string recibo = string.Empty;
        string reciboCol = string.Empty;
        public NotasCargo2()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtUnidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotasCargo2_Load(object sender, EventArgs e)
        {
            c.SeleccionarNotaCargo(cmbDocumento);
            c.SeleccionarConceptoDocumentoNotaCargo(cmbFiltroDocumentoC);
            //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
            c.CargarNotaCargo(dataGridView1);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "NUEVO")
            {
                if (cmbEstatus.Text != "Abierto")
                {
                    Limpiar();
                    cmbOrdenCompra.Enabled = true;
                    cmbDocumento.Enabled = true;
                    cmbFiltroDocumentoC.Enabled = true;
                    cmbProveedor.Enabled = true;
                    txtNotas.Enabled = true;
                    txtReferencia.Enabled = true;
                   // button3.Enabled = true;
                   // btLimpiarOrden.Enabled = true;
                    cmbDocumento.Focus();
                    cmbDocumento.DroppedDown = true;
                }
                else if (cmbEstatus.Text == "Abierto" && cmbOrdenCompra.Text == string.Empty)
                {
                    Limpiar();
                    cmbOrdenCompra.Enabled = true;
                    cmbDocumento.Enabled = true;
                    cmbFiltroDocumentoC.Enabled = true;
                    cmbProveedor.Enabled = true;
                    txtNotas.Enabled = true;
                    txtReferencia.Enabled = true;
  //                  button3.Enabled = true;
//                    btLimpiarOrden.Enabled = true;
                    cmbDocumento.Focus();
                    cmbDocumento.DroppedDown = true;

                }
                else
                {
                    MessageBox.Show("Confirme la nota de cargo antes de continuar");
                }
                guna2TabControl1.Enabled = true;
            /**    guna2GradientPanel4.Size = new Size(22, 569);
                toolStripButton11.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                */
                //toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
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
                //   Tipo = "Nuevo";
                cmbDocumento.Enabled = true;
                // cmbCentroCosto.Enabled = true;
                //  cmbDepartamento.Enabled = true;
         //       c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                c.CargarNotaCargo(dataGridView1);
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
          /*      guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;*/
                
                //  toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                cmbDocumento.Enabled = false;
        

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                guna2Button8.Enabled = false;
                //  Tipo = "Consulta";
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
               /* guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                */
                //    toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            }
            else if (e.ClickedItem.Text == "ENVIAR CORREO")
            {
             /*   guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;*/
                
            }
            else if (e.ClickedItem.Text == "AUTORIZAR")
            {
              /*  guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                */
                //    toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            }
        }


        void Limpiar()
        {
            txtFolio.Clear();
            txtDocumento.Clear();
            txtClave.Clear();
            txtFiltroOrdenC.Clear();
            txtDocumentoCol.Clear();
            txtReciboCol.Clear();
            cmbEstatus.Text = "Abierto";
            txtTotal.Text = "0.00";
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtImpuestos.Text = "0.00";
            txtNotas.Clear();
       //     btLimpiarOrden.Enabled = false;
            txtOrdenCompra.Clear();
            cmbOrdenCompra.Text = null;
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            cmbFiltroDocumentoC.Text = null;
            cmbFiltroDocumentoC.Enabled = false;
            cmbProveedor.Enabled = false;
            cmbProveedor.Text = null;
            cmbOrdenCompra.Enabled = false;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtNotas.Enabled = false;
        //    button3.Enabled = false;
            txtConsecutivo.Clear();
            txtReferencia.Clear();
            txtReferencia.Enabled = false;
            //c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text);
         //   button1.BackColor = Color.Gainsboro;
           // groupBox2.Enabled = false;
            txtSaldo.Text = "0.00";
            txtAbono.Text = "0.00";
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            //btLimpiarOrden.BackColor = Color.Gainsboro;
            //button3.BackColor = Color.Gainsboro;
            //txtReferencia.BackColor = Color.White;
            //txtNotas.BackColor = Color.White;
        //    button2.BackColor = Color.Gainsboro;

          //  button7.BackColor = Color.Gainsboro;
        }

        void Limpiarpartidas()
        {
            cmbConcepto.Text = "";
            c.SeleccionarConceptoGlobalesReciboNotaCargo(cmbConcepto);
            txtclase.Text = "";
                txtTipo.Text = "";
            txtSubtotal1.Text = "0.00";
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar conceptos a una nota de cargo Bloqueada o Cancelada");
                return;
            }
            else
            {
                string FolioOrden = txtOrdenCompra.Text;

                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarNotaCargo(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, FolioOrden, txtConsecutivo.Text, txtReferencia.Text);
                }

               // ConceptosGlobalesPartidasNotaCargo partidas = new ConceptosGlobalesPartidasNotaCargo(txtFolio.Text, txtDocumento.Text, FolioOrden);
               // partidas.ShowDialog();
               TxtFolio2.Text = txtFolio.Text;
            recibo = txtDocumento.Text;
            reciboCol = FolioOrden;
            }

            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            guna2TabControl1.SelectedIndex = 1;




         

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el registro de nota de cargo");
                return;
            }
            else if (txtTotal.Text != txtSaldo.Text)
            {
                MessageBox.Show("No es posible cancelar una nota de cargocon un pago total o parcial");
                return;
            }
            else if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una nota de cargo que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El saldo de esta nota de cargo sera cancelado, ¿Desea continuar?", "Nota de Cargo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Cancelado";
                c.ActualizarSaldoProveedor(DBOrdenCompra.MatriculaC, Convert.ToDecimal(txtTotal.Text));
                c.ActualizarRegistroNotaCargo3(txtFolio.Text, cmbEstatus.Text);
                Limpiar();
                MessageBox.Show("Registro de Gasto Cancelado");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && txtPartidas.Text != "0")
            {
                MessageBox.Show("No es posible limpiar la nota de cargo, confirme para continuar");
            }
            else if (cmbEstatus.Text == "Abierto" && txtPartidas.Text == "0")
            {
                c.eliminarNotaCargo(txtFolio.Text);
                Limpiar();
            }
            else
            {
                Limpiar();
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel5.Visible = false;
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            guna2GradientPanel5.Visible = true;
            c.BuscarProveedor(guna2DataGridView2);
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == string.Empty)
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
                txtMatricular.Text = guna2DataGridView2.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
                guna2GradientPanel5.Visible = false;
            }
            else
            {
                return;
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
                        c.ConsecutivoNotaCargo(txtConsecutivo, txtClave.Text);
                    }
                    //  groupBox2.Enabled = true;
                }
            }
        }

        private void cmbFiltroDocumentoC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbFiltroDocumentoC.Text);
                    txtFiltroOrdenC.Text = valores[1];

                    c.SeleccionarProvedor(cmbProveedor, txtFiltroOrdenC.Text);
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }

            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }

            }
        }

        private void cmbOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbOrdenCompra.Text != string.Empty && cmbFiltroDocumentoC.Text != string.Empty && cmbProveedor.Text != string.Empty)
                {
                    string[] valores = c.InformacionOrdenCompra(cmbOrdenCompra.Text, cmbFiltroDocumentoC.Text);
                    txtOrdenCompra.Text = valores[0];
 
                    txtMatricular.Text = valores[3];
                    Matricula = txtMatricular.Text;
                    txtDivisa.Text = valores[4];
                    txtTipoCambio.Text = valores[5];
       

                    string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                    txtNombreAlumnno.Text = valores2[0];

                    txtElaborado.Text = DBLogin.usuario;
                }

            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (cmbFiltroDocumentoC.Text != string.Empty)
            {
                txtOrdenCompra.Clear();
                txtFiltroOrdenC.Clear();
                txtMatricular.Clear();
                txtNombreAlumnno.Clear();
                Matricula = string.Empty;
                cmbFiltroDocumentoC.Text = null;
                cmbOrdenCompra.Text = null;
                cmbProveedor.Text = null;
                //Limpiar();
            }

            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;

           // button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            // button2.BackColor = Color.Gainsboro;

            // button7.BackColor = Color.Gainsboro;
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

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtImpuestos_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestos);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtAbono_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAbono);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty && txtOrdenCompra.Text == string.Empty)
            {
                string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores2[0];
            }
        }




        /// <summary>
        /// comienza parte del Formulario  de partidas
        /// </summary>

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = true;
            c.ConsultaNotaCargoPartida(TxtFolio2.Text, txtPartida);
            c.SeleccionarConceptoGlobalesReciboNotaCargo(cmbConcepto);
            c.ConsultaSubtotalNotaCargo(TxtFolio2.Text, txtSubtotal1);
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtSubtotal1.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";

        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionReciboConceptoGlobal(cmbConcepto.Text);
                txtClave.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtclase.Text = valores[2];
                txtTipo.Text = valores[3];


                if (txtTipo.Text == "Importe")
                {
                    txtDescuento.Text = valores[4];
                }
                else if (txtTipo.Text == "Porcentaje")
                {
                    decimal Descuento = Convert.ToDecimal(valores[4]);
                    Descuento = Descuento / 100;
                    decimal Totaldescuento = Convert.ToDecimal(txtSubtotal.Text) * Descuento;
                    txtDescuento.Text = Totaldescuento.ToString("N2");
                }

                //if (c.ConsultaConceptoPartidasGasto(TxtFolio.Text, txtClave.Text, txtImpuesto) > 0)
                //{
                //    txtTotal.Text = txtSubtotal.Text;
                //    txtDescuento.Text = "0.00";
                //}
                if (txtclase.Text == "Cargo" || txtclase.Text == "Impuesto")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtclase.Text == "Descuento")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }

            }
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);

            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionReciboConceptoGlobal(cmbConcepto.Text);
                txtClave.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtclase.Text = valores[2];
                txtTipo.Text = valores[3];


                if (txtTipo.Text == "Importe")
                {
                    txtDescuento.Text = valores[4];
                }
                else if (txtTipo.Text == "Porcentaje")
                {
                    decimal Descuento = Convert.ToDecimal(valores[4]);
                    Descuento = Descuento / 100;
                    decimal Totaldescuento = Convert.ToDecimal(txtSubtotal.Text) * Descuento;
                    txtDescuento.Text = Totaldescuento.ToString("N2");
                }

                //if (c.ConsultaConceptoPartidasGasto(TxtFolio.Text, txtClave.Text, txtImpuesto) > 0)
                //{
                //    txtTotal.Text = txtSubtotal.Text;
                //    txtDescuento.Text = "0.00";
                //}
                if (txtclase.Text == "Cargo" || txtclase.Text == "Impuesto")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtclase.Text == "Descuento")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }

            }
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);

            if (txtDescuento.Text != string.Empty)
            {
                if (txtclase.Text == "Cargo" || txtclase.Text == "Impuesto")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtclase.Text == "Descuento")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }
            else if (Convert.ToDecimal(txtTotal.Text) < 0)
            {
                MessageBox.Show("No es posible continuar con un Total menor a 0");
                return;
            }
            else
            {
               // c.InsertarReciboConceptoGlobalNotaCargo3(txtClave.Text, TxtFolio2.Text);
                if (txtclase.Text == "Cargo")
                {

                    c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                    c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);


                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto1.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                        c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);
                    }
                    else
                    {
                        txtSubtotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) - Convert.ToDecimal(txtImpuesto1.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtImpuesto1.Text), Convert.ToDecimal(txtTotal1.Text));
                        c.ActualizarReciboConceptoGlobalNotaCargo2(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);
                    }

                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalNotaCredito(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                    c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);

                }
                c.ConsultaSubtotalNotaCargo(TxtFolio2.Text, txtSubtotal1);
                c.ConsultaNotaCargoPartida(TxtFolio2.Text, txtPartida);
                // Limpiar();
                Limpiarpartidas();

            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }
            else if (Convert.ToDecimal(txtTotal.Text) < 0)
            {
                MessageBox.Show("No es posible continuar con un Total menor a 0");
                return;
            }
            else
            {
                //c.InsertarReciboConceptoGlobalNotaCargo3(txtClave.Text, TxtFolio2.Text);
                if (txtclase.Text == "Cargo")
                {

                    c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                    c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);


                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto1.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                        c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);
                    }
                    else
                    {
                        txtSubtotal1.Text = (Convert.ToDecimal(txtSubtotal1.Text) - Convert.ToDecimal(txtImpuesto1.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalNotaCargo2(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtImpuesto1.Text), Convert.ToDecimal(txtTotal1.Text));
                        c.ActualizarReciboConceptoGlobalNotaCargo2(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);
                    }

                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalNotaCredito(txtClave.Text, TxtFolio2.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text));
                    c.ActualizarReciboConceptoGlobalNotaCArgo(Convert.ToInt32(TxtFolio2.Text), Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtTotal1.Text), txtPartida.Text);

                }
                //c.EliminarReciboConceptoGlobalNotaCargo3(TxtFolio2.Text);
                c.CargarConceptosGloblaesNotascargo(guna2DataGridView1, TxtFolio2.Text);
                PanelPartidasRequisicion.Visible = false;
                //  this.Close();
                guna2Button14.Visible = true;
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            Limpiar();
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Enabled = false;
            if (txtPartidas.Text == string.Empty)
            {
                MessageBox.Show("Registre concepto para continuar");
                return;
            }
            else if (cmbOrdenCompra.Text == string.Empty && cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar nota de cargo Bloqueada o Cancelada");
                return;
            }
            else if (MessageBox.Show("Al confirmar la nota de cargo no podra realizar modificaciones, ¿Desea continuar?", "Nota de Cargo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Matricula = string.Empty;
                cmbEstatus.Text = "Bloqueado";
                c.ActualizarNotaCargo(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                c.ActualizarSaldoProveedor2(txtMatricular.Text, Convert.ToDecimal(txtTotal.Text));
                //Limpiar();
              //  c.CargarNotaCargo(dataGridView1);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                Limpiar();
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio1"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaNotasGasto(Folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo, txtReferencia, txtSaldo);
                cmbOrdenCompra.Enabled = false;
                txtNotas.Enabled = false;
            //    button3.Enabled = false;
                c.ConsultaNotaCargo(txtFolio.Text, txtAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
             //   panel2.Visible = false;

                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

                if (txtReciboCol.Text != "0")
                {
                    c.SeleccionarOrdenEntrega2(cmbOrdenCompra, txtReciboCol.Text);
                    cmbOrdenCompra.SelectedIndex = 0;

                    c.SeleccionarProvedor2(cmbProveedor, txtReciboCol.Text);
                    cmbProveedor.SelectedIndex = 0;

                    string[] valores2 = c.InformacionDocumento3(cmbOrdenCompra.Text);
                    txtFiltroOrdenC.Text = valores2[0];
                    txtDocumentoCol.Text = valores2[1];

                    cmbFiltroDocumentoC.Text = txtFiltroOrdenC.Text + " - " + txtDocumentoCol.Text;
                }
                c.CargarConceptosGloblaesNotascargo(guna2DataGridView1, Folio);
                guna2GradientPanel2.Visible = false;
                guna2TabControl1.Enabled = true;
                Bloquearencabezado();
            }
            else
            {
                return;
            }
        }

        void Bloquearencabezado()
        {
            cmbDocumento.Enabled = false;
            cmbEstatus.Enabled = false;
            txtConsecutivo.Enabled = false;
            txtFecha.Enabled = false;
            cmbFiltroDocumentoC.Enabled = false;
            cmbProveedor.Enabled = false;
            cmbOrdenCompra.Enabled = false;
            txtMatricular.Enabled = false;
            txtNombreAlumnno.Enabled = false;
            guna2Button12.Enabled = false;

            txtReferencia.Enabled = false;
            txtSubtotal.Enabled = false;
            txtDescuento.Enabled = false;
            txtDivisa.Enabled = false;
            txtTipoCambio.Enabled = false;
            txtImpuestos.Enabled = false;
            txtTotal.Enabled = false;
            txtAbono.Enabled = false;
            txtSaldo.Enabled = false;
            guna2Button1.Enabled = false;
            guna2Button3.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button14.Enabled = false;


        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroNotaCargo(dataGridView1, txtFiltro.Text);
            }
            else
            {
                c.CargarNotaCargo(dataGridView1);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocuemtnoNotaCargo(dataGridView1, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarNotaCargo(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroPNotaCargo(dataGridView1, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarNotaCargo(dataGridView1);
            }
        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
         /*   guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            toolStripButton11.Size = new Size(50, 60);*/
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
          /*  guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip1.Size = new Size(22, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;*/
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
     /*     guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            toolStripButton11.Size = new Size(50, 60);*/
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
//            xtConcepto3.Text = null;

            if (e.RowIndex != -1)
            {

            /*    string Partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
                c.ConsultaPartidaRequisicion(txtFolio.Text, Partida, txtClaveConcepto, txtConcepto3, txtConcepto2, txtCantidad, txtUnidad, txtExistencia);

                txtPartida.Text = Partida;
                PanelPartidasRequisicion.Visible = true;
                PanelPartidasRequisicion.BringToFront();
                guna2Button11.Visible = true;
                cmbConcepto.Items.Clear();
                //cmbConcepto.Text = txtConcepto3.Text;
                cmbConcepto.Items.Add(txtConcepto3.Text);
                cmbConcepto.SelectedIndex = 0;
                txtPartida.Enabled = false;
                cmbConcepto.Enabled = false;
                txtClaveConcepto.Enabled = false;
                txtConcepto2.Enabled = false;
                txtUnidad.Enabled = false;
                txtExistencia.Enabled = false;
                txtDepartamento2.Enabled = false;
                txtSubDepartamento.Enabled = false;
                if (Tipo == "Nuevo")
                {
                    guna2Button7.Visible = false;
                    guna2Button8.Visible = false;
                    guna2Button5.Visible = true;
                    guna2Button5.Enabled = true;
                }
            */
            }
            else
            {
                return;
            }

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {


            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 621);

            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
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

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;



            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;


            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 621);

            guna2GradientPanel7.Size = new Size(23, 621);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;


                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 621);

                guna2GradientPanel7.Size = new Size(23, 621);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                if (cmbEstatus.Text != "Abierto")
                {
                    Limpiar();
                    cmbOrdenCompra.Enabled = true;
                    cmbDocumento.Enabled = true;
                    cmbFiltroDocumentoC.Enabled = true;
                    cmbProveedor.Enabled = true;
                    txtNotas.Enabled = true;
                    txtReferencia.Enabled = true;
                    // button3.Enabled = true;
                    // btLimpiarOrden.Enabled = true;
                    cmbDocumento.Focus();
                    cmbDocumento.DroppedDown = true;
                }
                else if (cmbEstatus.Text == "Abierto" && cmbOrdenCompra.Text == string.Empty)
                {
                    Limpiar();
                    cmbOrdenCompra.Enabled = true;
                    cmbDocumento.Enabled = true;
                    cmbFiltroDocumentoC.Enabled = true;
                    cmbProveedor.Enabled = true;
                    txtNotas.Enabled = true;
                    txtReferencia.Enabled = true;
                    //                  button3.Enabled = true;
                    //                    btLimpiarOrden.Enabled = true;
                    cmbDocumento.Focus();
                    cmbDocumento.DroppedDown = true;

                }
                else
                {
                    MessageBox.Show("Confirme la nota de cargo antes de continuar");
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
                //   Tipo = "Nuevo";
                cmbDocumento.Enabled = true;
                // cmbCentroCosto.Enabled = true;
                //  cmbDepartamento.Enabled = true;
                //       c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;


                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 621);

                guna2GradientPanel7.Size = new Size(23, 621);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                c.CargarNotaCargo(dataGridView1);
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


                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                guna2Button8.Enabled = false;
                //  Tipo = "Consulta";
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
              
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;


                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 621);

                guna2GradientPanel7.Size = new Size(23, 621);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

            }
            
        }
    }
}

