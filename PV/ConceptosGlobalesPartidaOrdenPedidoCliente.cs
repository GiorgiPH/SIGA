using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Clases;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;
using PV.Clases.Remision;

namespace PV
{
    public partial class ConceptosGlobalesPartidaOrdenPedidoCliente : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        DBPedidoCliente r = new DBPedidoCliente();
        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public ConceptosGlobalesPartidaOrdenPedidoCliente(string Folio, string Recibo, string ReciboCol)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
        }

        private void ConceptosGlobalesPartidaGastos_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoGlobalesRecibo(cmbConcepto);
            r.ConsultaTotalOrdenPedidoCliente(TxtFolio.Text, txtSubtotal);
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbConcepto.Text))
            {
                try
                {
                    // Obtener valores
                    string[] valores = c.InformacionReciboConceptoGlobal(cmbConcepto.Text);
                    txtClave.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtclase.Text = valores[2];
                    txtTipo.Text = valores[3];
                    txtPorcentaje.Text = valores[4];

                    decimal descuento = 0;
                    decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);

                    // Calcular descuento según el tipo
                    switch (txtTipo.Text)
                    {
                        case "Importe":
                            descuento = Convert.ToDecimal(valores[4]);
                            break;

                        case "Porcentaje":
                            descuento = (Convert.ToDecimal(valores[4]) / 100) * subtotal;
                            break;

                        default:
                            descuento = 0;
                            break;
                    }

                    txtDescuento.Text = descuento.ToString("N2");

                    // Calcular el total según la clase
                    decimal total = 0;
                    switch (txtclase.Text)
                    {
                        case "Cargo":
                        case "Impuesto":
                            total = subtotal + descuento;
                            break;

                        case "Descuento":
                            total = subtotal - descuento;
                            break;

                        default:
                            total = subtotal; // Por si acaso no se cumple ninguna condición
                            break;
                    }

                    txtTotal.Text = total.ToString("N2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbConcepto.Text))
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }

            if (decimal.TryParse(txtTotal.Text, out decimal total) && total < 0)
            {
                MessageBox.Show("No es posible continuar con un Total menor a 0");
                return;
            }

            decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);
            decimal descuento = Convert.ToDecimal(txtDescuento.Text);
            decimal impuesto = Convert.ToDecimal(txtImpuesto.Text);
            int folio = Convert.ToInt32(TxtFolio.Text);
            string clase = txtclase.Text;

            switch (clase)
            {
                case "Cargo":
                case "Descuento":
                    c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase);
                    //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    break;

                case "Impuesto":
                    if (impuesto == 0)
                    {
                        c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase);
                        //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    }
                    else
                    {
                        subtotal -= impuesto;
                        c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, subtotal, impuesto, total, clase);
                        //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    }
                    break;

                default:
                    MessageBox.Show("Clase no reconocida");
                    return;
            }

            // Operaciones comunes después del switch
            //c.ActualizarPartidaReciboConceptoGlobalRemision(folio, Convert.ToDecimal(txtPorcentaje.Text), clase);
            c.EliminarReciboConceptoGlobalRemision3(TxtFolio.Text);
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
                //c.InsertarReciboConceptoGlobalRemisions3(txtClave.Text, TxtFolio.Text);
                if (txtclase.Text == "Cargo")
                {
                    c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text);
                    //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }
                    else
                    {
                        txtSubtotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtImpuesto.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }

                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalOrdenPedidoCliente2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text);
                    //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));

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

        private void Moneda(ref Guna2TextBox txt)
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescuento_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuento);
        }
    }
}
