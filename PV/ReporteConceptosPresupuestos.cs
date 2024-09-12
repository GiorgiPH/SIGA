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
    public partial class ReporteConceptosPresupuestos : Form
    {
        public ReporteConceptosPresupuestos()
        {
            InitializeComponent();
        }

        private void ReporteConceptosPresupuestos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet44.ConceptoPresupuesto' Puede moverla o quitarla según sea necesario.
            this.ConceptoPresupuestoTableAdapter.Fill(this.ControlCondominiosDataSet44.ConceptoPresupuesto);

            this.reportViewer1.RefreshReport();
        }
    }
}
