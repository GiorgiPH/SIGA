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
    public partial class ReporteCondominios : Form
    {
        public ReporteCondominios()
        {
            InitializeComponent();
        }

        private void ReporteCondominios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet2.Condominio' Puede moverla o quitarla según sea necesario.
            this.CondominioTableAdapter.Fill(this.ControlCondominiosDataSet2.Condominio);

            this.reportViewer1.RefreshReport();
        }
    }
}
