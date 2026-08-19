using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteCuentasPorPagar : Form
    {
        private readonly DateTime vencimiento;
        private readonly DateTime fecha1;
        private readonly DateTime fecha2;
        private readonly int proveedor1;
        private readonly int proveedor2;
        private readonly bool usarFechas;

        public ReporteCuentasPorPagar(string vencimiento, string fecha1, string fecha2,
            int proveedor1, int proveedor2, bool usarFechas)
        {
            InitializeComponent();

            this.vencimiento = Convert.ToDateTime(vencimiento);
            this.fecha1 = Convert.ToDateTime(fecha1);
            this.fecha2 = Convert.ToDateTime(fecha2);
            this.proveedor1 = proveedor1;
            this.proveedor2 = proveedor2;
            this.usarFechas = usarFechas;
        }

        private void ReporteCuentasPorPagar_Load(object sender, EventArgs e)
        {
            controlCondominiosDataSet49.EnforceConstraints = false;
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSetXX.DatosEmpresa'
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSetXX.sp_CuentasPorPagar'
            // Orden de parámetros = orden de parámetros del SP:
            // @Proveedor1, @Proveedor2, @UsarFechas, @Fecha1, @Fecha2, @Vencimiento
            this.sp_CuentasPorPagarTableAdapter.Fill(
                this.controlCondominiosDataSet49.sp_CuentasPorPagar,
                proveedor1,
                proveedor2,
                usarFechas,
                fecha1,
                fecha2,
                vencimiento);

            this.reportViewer1.RefreshReport();

            PageSettings pg = new PageSettings();
            pg.PaperSize = reportViewer1.GetPageSettings().PaperSize;
            pg.Margins.Left = 2;
            pg.Margins.Right = 2;
            pg.Margins.Top = 2;
            pg.Margins.Bottom = 2;
            this.reportViewer1.SetPageSettings(pg);
        }
    }
}
