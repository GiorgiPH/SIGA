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
    public partial class ReporteComprobanteOrdenCompra : Form
    {
        string folio;
        public ReporteComprobanteOrdenCompra(string folio)
        {
            InitializeComponent();
            this.folio = folio;
        }

        private void ReporteComprobanteOrdenCompra_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            this.sp_ReporteComprobanteOrdenCompraTableAdapter.Fill(this.controlCondominiosDataSet27.sp_ReporteComprobanteOrdenCompra, int.Parse(folio));

            this.reportViewer1.RefreshReport();
        }
    }
}
