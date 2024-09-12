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
    public partial class ReporteZonas : Form
    {
        public ReporteZonas()
        {
            InitializeComponent();
        }

        private void ReporteZonas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet14.Zona' Puede moverla o quitarla según sea necesario.
            this.ZonaTableAdapter.Fill(this.ControlCondominiosDataSet14.Zona);

            this.reportViewer1.RefreshReport();
        }
    }
}
