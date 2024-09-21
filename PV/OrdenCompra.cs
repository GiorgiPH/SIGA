using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using PuntoVentas.Clases.Login;
using PV;
using System.Drawing;
using Condominios.Clases.RegistrarIngresos;

namespace PV
{
    public partial class OrdenCompra : Form
    {
        public static string Matricula = string.Empty;
        public static int Opcion = 0;
        DBOrdenCompra c = new DBOrdenCompra();

        public OrdenCompra()
        {
            InitializeComponent();
        }

        private void OrdenCompra_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoDocumento(cmbDocumento);
            c.CargarRecibos(dataGridView1);
            c.CargarRecibos2(dataGridView2);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;

            cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            int Dias = Convert.ToInt32(txtDiasVence.Text);
            DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
            FechaVence = FechaVence.AddDays(Dias);
            txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarListaProveedores buscarListaAlumnos2 = new BuscarListaProveedores();
            buscarListaAlumnos2.ShowDialog();

            cmbDocumento.DroppedDown = false;
          
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme el recibo antes de continuar");
                return;
            }
            else
            {
                Matricula = string.Empty;
                DBOrdenCompra.MatriculaC = string.Empty;
                RecepcionProductos.Matricula = string.Empty;
                RegistroGastos.Matricula = string.Empty;
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
                    if (txtFolio.Text==string.Empty)
                    {
                      //  c.ConsecutivoCompra(txtConsecutivo, txtClave.Text);
                    }
                   groupBox2.Enabled = true;
                    txtDiasVence.Focus();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    c.InsertarOrden(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text, "0");
                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                PartidasOrden partidas = new PartidasOrden(txtFolio.Text, txtReciboInsc.Text, ReciboCol);
                partidas.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
           
            button6.BackColor = Color.Gainsboro;
         
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores = c.InformacionPropietarioRecibo(txtMatricular.Text);
                txtNombreAlumnno.Text = valores[1];
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                c.ActualizarReciboEstatus(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                Limpiar();
                c.CargarRecibos(dataGridView1);
            }
        }

        void Limpiar()
        {
            txtFolio.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtTotal.Clear();
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Clear();
            txtDescuento.Clear();
            txtTotal.Clear();
            txtNotas.Clear();
            txtConsecutivo.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            button1.BackColor = Color.Gainsboro;
            txtNotas.Enabled = false;
            button3.Enabled = false;
            groupBox2.Enabled = false;
            Matricula = string.Empty;
            button7.Enabled = false;
            Opcion = 0;
            txtAutoriza.Clear();
            txtFechaAuto.Clear();
            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && txtPartidas.Text!="0")
            {
                MessageBox.Show("No es posible limpiar la orden de compra, confirme la orden para continuar");
            }
            else if (cmbEstatus.Text == "Abierto" && txtPartidas.Text == "0")
            {
                c.eliminarOrden(txtFolio.Text);
                Limpiar();
                txtDiasVence.Focus();
            }
            else
            {
                Limpiar();
                txtDiasVence.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione una orden de compra para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme la orden de compra antes de continuar");
            }
            else
            {
                PartidasOrdenVer ver = new PartidasOrdenVer(txtFolio.Text);
                ver.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
           
          
        }

        private void button9_Click(object sender, EventArgs e)
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

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltro(dataGridView1, txtFiltro.Text);
                c.CargarRecibosFiltro2(dataGridView2, txtFiltro.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(dataGridView2);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroP(dataGridView1, txtFiltroNombre.Text);
                c.CargarRecibosFiltroP2(dataGridView2, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(dataGridView2);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;

            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
        }

        private void OrdenCompra_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                txtMatricular.Text = Matricula;

            //    c.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
                //c.ReciboSaldosImpuesto(txtFolio.Text, txtRecargo);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
                else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                {
                    button1.BackColor = Color.Red;
                }
            }
            if (Opcion == 1)
            {
                txtAutoriza.Text= DBRegistrarIngresos.usuario;
                txtFechaAuto.Text = DateTime.Today.ToString("yyyy/MM/dd");
                c.ActualizarOrdenAuto(txtFolio.Text, txtAutoriza.Text, txtFechaAuto.Text);
                MessageBox.Show("Orden de Compra Autorizada");
                Limpiar();
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(dataGridView2);
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDiasVence.Text != string.Empty)
                {
                    int Dias = Convert.ToInt32(txtDiasVence.Text);
                    DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                    FechaVence = FechaVence.AddDays(Dias);
                    txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de dias requiere incorrecto");
            }

            txtDiasVence.BackColor = Color.White;
            button3.BackColor = Color.Orange;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
              //  c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
                button7.Enabled = true;
                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                Matricula= DBOrdenCompra.MatriculaC;
                panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                groupBox2.Enabled = true;
            }
            else
            {
                return;
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

        private void txtRecargo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtRecargo);
            //if (txtRecargo.Text != string.Empty || txtRecargo.Text !="0.00")
            //{
            //    txtRecargo.Text = Convert.ToDecimal(txtRecargo.Text).ToString();
            //}

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
                button3.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else if (cmbEstatus.Text == "Abierto" && cmbDocumento.Text == string.Empty)
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
                button3.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else
            {
                MessageBox.Show("Confirme la orden de compra antes de continuar");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReporteOrdenCompra reporteOrdenCompra = new ReporteOrdenCompra();
            reporteOrdenCompra.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocumento(dataGridView1, txtFiltroDocumento.Text);
                c.CargarRecibosFiltroDocumento2(dataGridView2, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
                c.CargarRecibos2(dataGridView2);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView2.Rows[e.RowIndex].Cells["Folio2"].Value.ToString();
                txtFolio.Text = "X";
              //  c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
    
                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                Matricula = DBOrdenCompra.MatriculaC;
                panel2.Visible = false;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                groupBox2.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AutentificarAdmin autentificarAdmin = new AutentificarAdmin();
            autentificarAdmin.ShowDialog();
        }

        private void cmbDocumento_Leave(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.Orange;
        }

        private void txtDiasVence_TextChanged(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.White;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gainsboro;
            txtNotas.BackColor = Color.Orange;
        }

        private void txtNotas_Leave(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Orange;
        }

        private void txtNotas_TextChanged(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Orange;
        }

        private void cmbDocumento_Click(object sender, EventArgs e)
        {
           
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
           
        }

        private void txtDiasVence_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
           
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
          
        }

        private void txtNotas_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
           
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
          
        }
    }
}
