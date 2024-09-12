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
    public partial class ConceptosGlobalesPartidaGastos : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public ConceptosGlobalesPartidaGastos(string Folio, string Recibo, string ReciboCol)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
        }

        private void ConceptosGlobalesPartidaGastos_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoGlobalesRecibo(cmbConcepto);
            c.ConsultaSubtotalGasto(TxtFolio.Text, txtSubtotal);
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
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

                if (c.ConsultaConceptoPartidasGasto(TxtFolio.Text, txtClave.Text, txtImpuesto) > 0)
                {
                    txtTotal.Text = txtSubtotal.Text;
                    txtDescuento.Text = "0.00";
                }
                else if (txtclase.Text == "Cargo" || txtclase.Text == "Impuesto")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtclase.Text == "Descuento")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }

            }
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button1_Click(object sender, EventArgs e)
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
                c.InsertarReciboConceptoGlobalGastos3(txtClave.Text, TxtFolio.Text);
                if (txtclase.Text == "Cargo")
                {

                    c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                    c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));


                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                        c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }
                    else
                    {
                        txtSubtotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtImpuesto.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text));
                        c.ActualizarReciboConceptoGlobalGasto2(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }

                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                    c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));

                }
                c.EliminarReciboConceptoGlobalGastos3(TxtFolio.Text);
                
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
                c.InsertarReciboConceptoGlobalGastos3(txtClave.Text, TxtFolio.Text);
                if (txtclase.Text == "Cargo")
                {
                    c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                    c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                        c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }
                    else
                    {
                        txtSubtotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtImpuesto.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalGasto2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text));
                        c.ActualizarReciboConceptoGlobalGasto2(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }
                    
                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text));
                    c.ActualizarReciboConceptoGlobalGasto(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));

                }
                c.ConsultaSubtotalGasto(TxtFolio.Text, txtSubtotal);
                Limpiar();
            }
        }

        void Limpiar()
        {
            cmbConcepto.Text = null;
            txtConcepto.Clear();
            txtclase.Clear();
            txtTipo.Clear();
            txtClave.Clear();
            txtConcepto.Clear();
            txtclase.Clear();
            txtTipo.Clear();
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtImpuesto.Text = "0.00";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
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

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
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

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
