using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class PartidasRequisicionVer : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();

        public static string Concep = string.Empty;

        public PartidasRequisicionVer(string Folio)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
        }

        private void PartidasRequisicionVer_Load(object sender, EventArgs e)
        {
            c.CargarRequisicionPartidas(dataGridView1, TxtFolio.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                string Partida = dataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
              //  c.ConsultaPartidaRequisicion(TxtFolio.Text, Partida, txtClaveConcepto, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtExistencia);
                txtPartida.Text = Partida;
                panel2.Visible = false;
            }
            else
            {
                return;
            }
        }
    }
}
