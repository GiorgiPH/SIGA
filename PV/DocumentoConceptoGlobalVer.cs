using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;

namespace Condominios
{
    public partial class DocumentoConceptoGlobalVer : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public static string Concep = string.Empty;

        public DocumentoConceptoGlobalVer(string Folio)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
        }

        private void DocumentoConceptoGlobalVer_Load(object sender, EventArgs e)
        {
            c.CargarRecibosConcepto(dataGridView1, TxtFolio.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Concepto = dataGridView1.Rows[e.RowIndex].Cells["Concepto"].Value.ToString();
                string Total = dataGridView1.Rows[e.RowIndex].Cells["Total"].Value.ToString();
                c.ConsultaConcepto(TxtFolio.Text, Concepto, Total, txtCleveConcepto, txtConcepto,txtclase,txtTipo, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtTotal);
                txtCleveConcepto.Text = Concepto;
                panel2.Visible = false;
            }
            else
            {
                return;
            }
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

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }
    }
}
