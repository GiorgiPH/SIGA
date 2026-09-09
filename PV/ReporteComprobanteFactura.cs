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
    public partial class ReporteComprobanteFactura : Form
    {
        string folio;
        public ReporteComprobanteFactura(string folio)
        {
            InitializeComponent();
            this.folio = folio;
        }

        private void ReporteComprobanteFactura_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);
            this.sp_ComprobanteFacturaTableAdapter.Fill(this.dTSFactura.sp_ComprobanteFactura, int.Parse(folio));

            this.reportViewer1.RefreshReport();
        }
    }
}
