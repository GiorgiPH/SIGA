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
    public partial class ReporteComprobantePrestamo : Form
    {
        string folio;
        public ReporteComprobantePrestamo(string folio)
        {
            InitializeComponent();
            this.folio = folio;
        }

        private void ReporteComprobantePrestamo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);
            this.sp_ComprobantePrestamoTableAdapter.Fill(this.dTSPrestamos.sp_ComprobantePrestamo, int.Parse(folio));
            this.reportViewer1.RefreshReport();
        }
    }
}
