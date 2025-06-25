using Microsoft.Reporting.WinForms;
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
    public partial class ReporteUtilidadProducto : Form
    {

        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteUtilidadProducto(string fecha1, string fecha2)
        {
            InitializeComponent();
            this.fecha1 = fecha1;
            this.fecha2 = fecha2;
        }

        private void ReporteUtilidadProducto_Load(object sender, EventArgs e)
        {
            this.dTSReporteOrdenesPedidoUtilidad.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'cajaRDataSet.Empresa' table. You can move, or remove it, as needed.
            this.empresaTableAdapter.Fill(this.cajaRDataSet.Empresa);

            this.sp_ReporteOrdenesPedidoUtilidadTableAdapter.Fill(this.dTSReporteOrdenesPedidoUtilidad.sp_ReporteOrdenesPedidoUtilidad, Convert.ToDateTime(this.fecha1), Convert.ToDateTime(this.fecha2), 1);
            ReportParameter[] parameters = new ReportParameter[2];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("FechaI", fecha1);
            parameters[1] = new ReportParameter("FechaF", fecha2);


            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
