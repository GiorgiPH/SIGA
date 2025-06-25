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
    public partial class ReporteComprasReembolso : Form
    {
        string folio=string.Empty;
        public ReporteComprasReembolso(string folio)
        {
            InitializeComponent();
            this.folio = folio;
        }

        private void ReporteComprasReembolso_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);
            this.sp_ReporteReembolsoTableAdapter.Fill(this.dTSReporteDiarioReembolsos.sp_ReporteReembolso, folio);

            this.reportViewer1.RefreshReport();
        }
    }
}
