using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using PuntoVentas.Clases.Login;
using PV;
using System.Drawing;

namespace PV
{
    public partial class NotasCargo : Form
    {
        public static string Matricula = string.Empty;
        DBOrdenCompra c = new DBOrdenCompra();

        public NotasCargo()
        {
            InitializeComponent();
        }

        private void NotasCargo_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtOrdenCompra.Text != string.Empty)
            {
                MessageBox.Show("No es posible cambiar proveedor de la orden de compra");
            }
            else
            {
                BuscarListaProveedores buscarListaAlumnos2 = new BuscarListaProveedores();
                buscarListaAlumnos2.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
         
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme la recepcion antes de continuar");
                return;
            }
            else
            {
                Matricula = string.Empty;
                RegistroGastos.Matricula = string.Empty;
                DBOrdenCompra.MatriculaC = string.Empty;
                RecepcionProductos.Matricula = string.Empty;
                OrdenCompra.Matricula = string.Empty;
                RegistroEgreso.matricula = string.Empty;
                RegistroEgreso.nombre = string.Empty;
                this.Close();
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
           //             c.ConsecutivoNotaCargo(txtConsecutivo, txtClave.Text);
                    }
                    groupBox2.Enabled = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button8_Click(object sender, EventArgs e)
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
                button3.Enabled = true;
                btLimpiarOrden.Enabled = true;
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
                button3.Enabled = true;
                btLimpiarOrden.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else
            {
                MessageBox.Show("Confirme la nota de cargo antes de continuar");
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
            btLimpiarOrden.Enabled = false;
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
            button3.Enabled = false;
            txtConsecutivo.Clear();
            txtReferencia.Clear();
            txtReferencia.Enabled = false;
            //c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text);
            button1.BackColor = Color.Gainsboro;
            groupBox2.Enabled = false;
            txtSaldo.Text = "0.00";
            txtAbono.Text = "0.00";
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione una nota de cargo confirmada para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme la nota de cargo antes de continuar");
            }
            else
            {
                ConceptosGlobalesNotasCargoVer documentoConceptoGlobalVer = new ConceptosGlobalesNotasCargoVer(txtFolio.Text);
                documentoConceptoGlobalVer.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

        }

        private void btLimpiarOrden_Click(object sender, EventArgs e)
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
          
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void button2_Click(object sender, EventArgs e)
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

                ConceptosGlobalesPartidasNotaCargo partidas = new ConceptosGlobalesPartidasNotaCargo(txtFolio.Text, txtDocumento.Text, FolioOrden);
                partidas.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
          
            button7.BackColor = Color.Gainsboro;
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty && txtOrdenCompra.Text == string.Empty)
            {
                string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores2[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                Limpiar();
                c.CargarNotaCargo(dataGridView1);
            }
        }

        private void NotasCargo_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                txtMatricular.Text = Matricula;

                c.ReciboSaldosNotaCargo(txtFolio.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
                else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                {
                    button1.BackColor = Color.Red;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
            //    c.ConsultaNotasGasto(Folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo, txtReferencia, txtSaldo);
                cmbOrdenCompra.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
           //     c.ConsultaNotaCargo(txtFolio.Text, txtAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                panel2.Visible = false;

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
            }
            else
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;

            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
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
                    //cmbEstatus.Text = valores[1];
                    //txtFecha.Text = Convert.ToDateTime(valores[2]).ToString("yyyy/MM/dd");
                    txtMatricular.Text = valores[3];
                    Matricula = txtMatricular.Text;
                    txtDivisa.Text = valores[4];
                    txtTipoCambio.Text = valores[5];
                    //txtSubtotal.Text = valores[6];
                    //txtDescuento.Text = valores[7];
                    //txtRecargo.Text = valores[8];
                    //txtTotal.Text = valores[9];
                    //txtPartidas.Text = valores[10];
                    //txtNotas.Text = valores[11];
                    //txtElaborado.Text = valores[12];

                    string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                    txtNombreAlumnno.Text = valores2[0];

                    txtElaborado.Text = DBLogin.usuario;
                }

            }
        }

        private void Moneda(ref TextBox txt)
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

        private void button9_Click(object sender, EventArgs e)
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

        private void cmbDocumento_Leave(object sender, EventArgs e)
        {
            cmbFiltroDocumentoC.DroppedDown = true;
        }

        private void cmbFiltroDocumentoC_Leave(object sender, EventArgs e)
        {

            cmbProveedor.DroppedDown = true;
        }

        private void cmbProveedor_Leave(object sender, EventArgs e)
        {
            cmbOrdenCompra.DroppedDown = true;
        }

        private void cmbOrdenCompra_Leave(object sender, EventArgs e)
        {
            btLimpiarOrden.BackColor = Color.Orange;
        }

        private void btLimpiarOrden_Leave(object sender, EventArgs e)
        {
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Orange;

        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.Orange;
 
        }

        private void txtReferencia_Leave(object sender, EventArgs e)
        {
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.Orange;
  
        }

        private void txtNotas_Leave(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Orange;

        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Orange;

        }

        private void button7_Leave(object sender, EventArgs e)
        {
            button7.BackColor = Color.Gainsboro;
        }

        private void cmbDocumento_Click(object sender, EventArgs e)
        {

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
         
            button7.BackColor = Color.Gainsboro;
        }

        private void cmbFiltroDocumentoC_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

           
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void cmbProveedor_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
           
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void cmbOrdenCompra_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
           
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void txtReferencia_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
           
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void txtNotas_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
           
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }
    }
}
