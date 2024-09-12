using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;
using Condominios;

namespace ControlAcademico
{
    public partial class Partidas : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public Partidas(string Folio, string Recibo, string ReciboCol)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
        }

        private void Partidas_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoRecibo(cmbConcepto);
            c.Consulta5(TxtFolio.Text, txtPartida);
            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";

        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] valores = c.InformacionRecibo(cmbConcepto.Text);
            txtClave.Text= valores[0];
            txtConcepto.Text = valores[1];
            txtFrecuencia.Text = valores[2];
            txtSubtotal.Text = valores[3];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }
            else if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }
            else if (txtFrecuencia.Text == "Automatico")
            {
                MessageBox.Show("Concepto asignado a recibos automáticos, no se permite el registro de más partidas, si así lo requiere asigne otro concepto de ingreso, de lo contrario, termine el registro");
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartida(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                Limpiar();
                c.Consulta5(TxtFolio.Text, txtPartida);
                c.ReciboSaldosPartidas(TxtFolio.Text, txtSubtotalR, txtDescuentoR, txtTotalR);
            }

        }

        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }
            else if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    c.ActualizarRecibo(TxtFolio.Text, Partida.ToString());
                    this.Close();
                    DocumentoConceptoGlobal documentoConceptoGlobal = new DocumentoConceptoGlobal(TxtFolio.Text, recibo, reciboCol);
                    documentoConceptoGlobal.ShowDialog();
                }
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartida(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                c.ActualizarRecibo(TxtFolio.Text, txtPartida.Text);
                this.Close();
                DocumentoConceptoGlobal documentoConceptoGlobal = new DocumentoConceptoGlobal(TxtFolio.Text, recibo, reciboCol);
                documentoConceptoGlobal.ShowDialog();
            }

        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);

            try
            {
                if (txtSubtotal.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
                }
                else if (txtSubtotal.Text == string.Empty)
                {
                    txtSubtotal.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de subtotal incorrecto");
            }
           

        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);

            try
            {
                if (txtDescuento.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
                }
                else if (txtDescuento.Text == string.Empty)
                {
                    txtDescuento.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de descuento incorrecto");
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

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtSubtotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Moneda(ref txtColegiatura);
        }


        private void txtBeca_TextChanged(object sender, EventArgs e)
        {
            //Moneda(ref txtBeca);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PartidasVer partidasVer = new PartidasVer(TxtFolio.Text);
            partidasVer.ShowDialog();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtTotalR);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotalR);
        }

        private void txtDescuentoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoR);
        }
    }
}
