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
    public partial class ReporteResguardoActivoFijo : Form
    {
        string Folio = string.Empty;

        public ReporteResguardoActivoFijo(string folio)
        {
            InitializeComponent();
            Folio = folio;
        }

        private void ReporteResguardoActivoFijo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet19.ResguardoActivoFijo' Puede moverla o quitarla según sea necesario.
            this.ResguardoActivoFijoTableAdapter.FillBy(this.ControlCondominiosDataSet19.ResguardoActivoFijo,Convert.ToInt32( Folio));

            this.reportViewer1.RefreshReport();
        }
    }
}
