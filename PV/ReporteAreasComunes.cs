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
    public partial class ReporteAreasComunes : Form
    {
        public ReporteAreasComunes()
        {
            InitializeComponent();
        }

        private void ReporteAreasComunes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet1.AreasComunes' Puede moverla o quitarla según sea necesario.
            this.AreasComunesTableAdapter.Fill(this.ControlCondominiosDataSet1.AreasComunes);

            this.reportViewer1.RefreshReport();
        }
    }
}
