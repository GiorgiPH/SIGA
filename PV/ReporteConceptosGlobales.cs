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
    public partial class ReporteConceptosGlobales : Form
    {
        public ReporteConceptosGlobales()
        {
            InitializeComponent();
        }

        private void ReporteConceptosGlobales_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet9.ConceptosGlobales' Puede moverla o quitarla según sea necesario.
            this.ConceptosGlobalesTableAdapter.Fill(this.ControlCondominiosDataSet9.ConceptosGlobales);

            this.reportViewer1.RefreshReport();
        }
    }
}
