using System;
using System.Windows.Forms;

namespace MEDCON
{
    public partial class BuscarFecha : Form
    {
        public BuscarFecha()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CancelarCitas.Fecha = dpFecha.Text;
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CancelarCitas.Fecha = "";
            this.Close();
        }

        private void BuscarFecha_Load(object sender, EventArgs e)
        {

        }
    }
}
