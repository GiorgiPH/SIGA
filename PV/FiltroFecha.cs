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
    public partial class FiltroFecha : Form
    {
        string tipoReporte = string.Empty;
        public FiltroFecha(string tipoReporte="")
        {
            InitializeComponent();
            this.tipoReporte = tipoReporte;
            dtFecha1.Value=DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteEstadoCuenta.Fecha1 = dtFecha1.Text;
            ReporteEstadoCuenta.Fecha2 = dtFecha2.Text;
            ReporteEstadoCuenta2.Fecha1 = dtFecha1.Text;
            ReporteEstadoCuenta2.Fecha2 = dtFecha2.Text;
            ReporteEstadoCuenta.Fecha = "Si";
            ReporteEstadoCuenta2.Fecha = "Si";
            ReporteAnticipos.Fecha1 = dtFecha1.Text;
            ReporteAnticipos.Fecha2 = dtFecha2.Text;
            ReporteAnticipos.Fecha = "Si";
            ReporteAnticiposAplicados.Fecha1 = dtFecha1.Text;
            ReporteAnticiposAplicados.Fecha2 = dtFecha2.Text;
            ReporteAnticiposAplicados.Fecha = "Si";
            ReporteSaldoCondominio.fecha1 = dtFecha1.Text;
            ReporteSaldoCondominio.fecha2 = dtFecha2.Text;
            ReporteSaldoCondominio.fecha = "Si";
            ReporteSaldoPropietario.fecha1 = dtFecha1.Text;
            ReporteSaldoPropietario.fecha2 = dtFecha2.Text;
            ReporteSaldoPropietario.fecha = "Si";
            if (tipoReporte == "Utilidad Producto")
            {
                ReporteUtilidadPedido r = new ReporteUtilidadPedido(dtFecha1.Text, dtFecha2.Text);
                r.ShowDialog();
            }
            else if (tipoReporte == "Utilidad Pedido")
            {
                ReporteUtilidadProducto r = new ReporteUtilidadProducto(dtFecha1.Text, dtFecha2.Text);
                r.ShowDialog();
            }
            else if (tipoReporte == "Diario Pedidos")
            {
                ReporteDiarioP r = new ReporteDiarioP(dtFecha1.Text, dtFecha2.Text);
                r.ShowDialog();
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
