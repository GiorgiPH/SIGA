using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class ReportesCondominio : Form
    {
        public ReportesCondominio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ReporteSaldoPropietario reporteSaldoPropietario = new ReporteSaldoPropietario();
            //reporteSaldoPropietario.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ReporteSaldoCondominio reporteSaldoCondominio = new ReporteSaldoCondominio();
            //reporteSaldoCondominio.ShowDialog();
        }
    }
}
