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
using PV.Clases.Remision;

namespace PV
{
    public partial class ConceptosGlobalesPartidaRemision : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        DBRemiision r = new DBRemiision();
        string recibo = string.Empty;
        string reciboCol = string.Empty;

        public ConceptosGlobalesPartidaRemision(string Folio, string Recibo, string ReciboCol)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
        }

        private void ConceptosGlobalesPartidaGastos_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoGlobalesRecibo(cmbConcepto);
            r.ConsultaTotalRemision(TxtFolio.Text, txtSubtotal);
            
        }
        private void ActualizarCalculos()
        {
            try
            {
                decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);
                decimal descuento = CalcularDescuento(txtTipo.Text, Convert.ToDecimal(txtPorcentaje.Text), subtotal);
                txtDescuento.Text = descuento.ToString("N2");

                decimal total = CalcularTotal(txtclase.Text, subtotal, descuento);
                decimal Impuesto = CalcularIva(descuento, txtIncluyeIva.Text);
                txtImpuesto.Text = Impuesto.ToString("N2");
                decimal TotalFinal = total + Impuesto;

                txtTotal.Text = TotalFinal.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private decimal CalcularDescuento(string tipo, decimal porcentaje, decimal subtotal)
        {
            switch (tipo)
            {
                case "Importe":
                    return porcentaje;

                case "Porcentaje":
                    return (porcentaje / 100) * subtotal;

                default:
                    return 0;
            }
        }

        private decimal CalcularTotal(string clase, decimal subtotal, decimal descuento)
        {
            switch (clase)
            {
                case "Cargo":
                case "Impuesto":
                    return subtotal + descuento;

                case "Descuento":
                    return subtotal - descuento;

                default:
                    return subtotal; // Por defecto, retornar el subtotal
            }
        }
        private decimal CalcularIva(decimal total, string iva)
        {
            switch (iva)
            {
                case "1":
          
                    return (total*0.16m);

                case "0":
                    return total;

                default:
                    return total; // Por defecto, retornar el subtotal
            }
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
                    txtIncluyeIva.Text = valores[5];

                    // Calcular y actualizar los valores
                    ActualizarCalculos();
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
                    c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, txtIncluyeIva.Text);
                    //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    break;

                case "Impuesto":
                    if (impuesto == 0)
                    {
                        c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, txtIncluyeIva.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    }
                    else
                    {
                        subtotal -= impuesto;
                        c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, subtotal, impuesto, total, clase, txtIncluyeIva.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(folio, total);
                    }
                    break;

                default:
                    MessageBox.Show("Clase no reconocida");
                    return;
            }

            // Operaciones comunes después del switch
            //c.ActualizarPartidaReciboConceptoGlobalRemision(folio, Convert.ToDecimal(txtPorcentaje.Text), clase, txtIncluyeIva.Text);
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
                    c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text, txtIncluyeIva.Text);
                    //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                }
                else if (txtclase.Text == "Impuesto")
                {
                    if (txtImpuesto.Text == "0.00")
                    {
                        c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text, txtIncluyeIva.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }
                    else
                    {
                        txtSubtotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtImpuesto.Text)).ToString("N2");
                        c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text, txtIncluyeIva.Text);
                        //c.ActualizarReciboConceptoGlobalRemision(Convert.ToInt32(TxtFolio.Text), Convert.ToDecimal(txtTotal.Text));
                    }

                }
                else if (txtclase.Text == "Descuento")
                {

                    c.InsertarReciboConceptoGlobalRemision2(txtClave.Text, TxtFolio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), txtclase.Text, txtIncluyeIva.Text);
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
            txtIncluyeIva.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSubtotal);
        }

      

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtTotal);
        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescuento_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuento);
            ActualizarCalculos();
        }
    }
}
