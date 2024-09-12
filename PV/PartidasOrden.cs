using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class PartidasOrden : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public PartidasOrden(string Folio, string Recibo, string ReciboCol)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
        }

        private void PartidasOrden_Load(object sender, EventArgs e)
        {
            c.SeleccionarProducto2(cmbConcepto);
            //c.Consulta5(TxtFolio.Text, txtPartida);
            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
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
                txtImpuesto.Text = valores[4];

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            else if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
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
                c.InsertarPartida(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto.Text));
                Limpiar();
             //   c.Consulta5(TxtFolio.Text, txtPartida);
         //       c.ReciboSaldosPartidasOrden(TxtFolio.Text, txtSubtotalR, txtDescuentoR, txtTotalR);
           //     c.ReciboSaldosPartidasOrden2(TxtFolio.Text, txtImpuestoR);
            }

        }

        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtCantidad.Text = "1";
            txtImpuesto.Text ="0.00";
            cmbConcepto.Text = null;
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
                        c.ActualizarOrden(TxtFolio.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarOrden(TxtFolio.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartida(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtPrecio.Text), Convert.ToDecimal(txtImpuesto.Text));
                c.ActualizarOrden(TxtFolio.Text, txtPartida.Text);
                this.Close();
            }

        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);

            try
            {
                if (txtSubtotal.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
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
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
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
                        txtSubtotal.Text = sub.ToString();

                        decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                        txtImpuestoIm.Text = Impuesto.ToString("N2");
                        txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de cantidad incorrecto");
            }
           
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                    txtSubtotal.Text = sub.ToString();

                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
           
        }

        private void txtImpuesto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtImpuesto.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtImpuesto.Text == string.Empty)
                {
                    txtImpuesto.Text = "0";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Foramto de impuesto incorrecto");
            }          
        }

        private void txtImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoIm);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PartidasOrdenVer partidasOrdenVer = new PartidasOrdenVer(TxtFolio.Text);
            partidasOrdenVer.ShowDialog();
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
    }
}
