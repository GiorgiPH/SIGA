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
    public partial class ReporteMovimientoInventario : Form
    {
        string folio= "";
        string tipoMovimiento = "";
        public ReporteMovimientoInventario(string folio, string tipoMovimiento)
        {
            InitializeComponent();
            this.folio = folio;
            this.tipoMovimiento = tipoMovimiento;
        }

        private void ReporteMovimientoInventario_Load(object sender, EventArgs e)
        {
            this.controlCondominiosDataSet31.EnforceConstraints = false;
            this.dTSReporteMovimientoInventario.EnforceConstraints = false;
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet31.DatosEmpresa);

            this.sp_ReporteMovimientoInventarioTableAdapter.Fill(this.dTSReporteMovimientoInventario.sp_ReporteMovimientoInventario, int.Parse(folio), tipoMovimiento);
            this.reportViewer1.RefreshReport();
        }
    }
}
