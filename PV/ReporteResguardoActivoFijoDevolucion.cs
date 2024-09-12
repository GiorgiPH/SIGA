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
    public partial class ReporteResguardoActivoFijoDevolucion : Form
    {
        string Folio = string.Empty;

        public ReporteResguardoActivoFijoDevolucion(string folio)
        {
            InitializeComponent();
            Folio = folio;

        }

        private void ReporteResguardoActivoFijoDevolucion_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet20.ResguardoActivoFijo' Puede moverla o quitarla según sea necesario.
            this.ResguardoActivoFijoTableAdapter.FillBy3(this.ControlCondominiosDataSet20.ResguardoActivoFijo, Convert.ToInt32(Folio));

            this.reportViewer1.RefreshReport();
        }
    }
}
