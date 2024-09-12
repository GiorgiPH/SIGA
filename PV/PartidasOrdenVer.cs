using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class PartidasOrdenVer : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();

        public static string Concep = string.Empty;

        public PartidasOrdenVer(string Folio)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
        }

        private void PartidasOrdenVer_Load(object sender, EventArgs e)
        {
            c.CargarRecibosPartidas(dataGridView1, TxtFolio.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Partida = dataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
               // c.ConsultaPartida(TxtFolio.Text, Partida, txtClaveConcepto, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtTotal, txtPrecio, txtImpuesto, txtEntregado);
                txtPartida.Text = Partida;
                panel2.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
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

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
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

        private void txtImpuesto_TextChanged(object sender, EventArgs e)
        {
            if (txtImpuesto.Text != string.Empty)
            {
                decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                txtImpuestoIm.Text = Impuesto.ToString("N2");
            }
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoIm);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
        }
    }
}
