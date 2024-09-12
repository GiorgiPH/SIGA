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
    public partial class ReporteConceptoIngresos : Form
    {
        public ReporteConceptoIngresos()
        {
            InitializeComponent();
        }

        private void ReporteConceptoIngresos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet10.ConceptosIngreso' Puede moverla o quitarla según sea necesario.
            this.ConceptosIngresoTableAdapter.Fill(this.ControlCondominiosDataSet10.ConceptosIngreso);

            this.reportViewer1.RefreshReport();
        }
    }
}
