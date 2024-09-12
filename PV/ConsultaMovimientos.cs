using System;
using System.Windows.Forms;
using PV.Clases.Inventario;
using PuntoVentas.Clases.Login;
using PV.Clases.TipoMovimiento;
using PV;

namespace PV
{
    public partial class ConsultaMovimientos : Form
    {
        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBPartidas s = new DBPartidas();

        string TipoM = string.Empty;
        string Descripcion = string.Empty;
        string Folio = string.Empty;

        public ConsultaMovimientos(string tipo, string descripcion, string folio)
        {
            InitializeComponent();
            TipoM = tipo;
            Descripcion = descripcion;
            Folio = folio;
        }

        private void ConsultaMovimientos_Load(object sender, EventArgs e)
        {
            txtTipoDocumento.Text = TipoM;
            txtDescripcion.Text = Descripcion;
            txtUltimoFolio.Text = Folio;
            c.CargarPartida(dataGridView1, TipoM, Descripcion, Folio);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Partida = dataGridView1.Rows[e.RowIndex].Cells["NoPartida"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();
                txtNoPartida.Text = Partida;

           //     c.ConsultaPartida(txtUltimoFolio.Text, Tipo, Partida, Documento,  cmbProducto, txtConcepto, txtAlias,  txtTipoCosteo,  txtExistencias, txtCantidad, txtUnidad, txtPrecio, cmbDivisa, txtTipoCambio, txtTotal);
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }
    }
}
