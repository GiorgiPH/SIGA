using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuntoVentas;

namespace MEDCON
{
    public partial class ConsultaFechaNombre : Form
    {
        public ConsultaFechaNombre()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuPrincipal.Opcion = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuPrincipal.Opcion = 2;
            this.Close();
        }

        private void ConsultaFechaNombre_Load(object sender, EventArgs e)
        {

        }
    }
}
