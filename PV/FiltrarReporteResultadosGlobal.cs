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
    public partial class FiltrarReporteResultadosGlobal : Form
    {
        public FiltrarReporteResultadosGlobal()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ReporteResultadosGlobal reporteResultadoGlobal = new ReporteResultadosGlobal(dtpMes.Value.ToString("MM"), dtpAño.Value.ToString("yyyy"), txtNotas.Text);
            reporteResultadoGlobal.ShowDialog();

        }
    }
}
