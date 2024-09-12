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
    public partial class ReporteAnticiposAplicados : Form
    {
        string Propietario1 = string.Empty;
        string Propietario2 = string.Empty;
        public static string Fecha = string.Empty;
        public static string Fecha1 = string.Empty;
        public static string Fecha2 = string.Empty;
        string Fecha3 = string.Empty;
        string Fecha4 = string.Empty;
        string Fecha5 = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;

        public ReporteAnticiposAplicados(string propietario1, string propietario2, string fecha, string fecha1, string fecha2)
        {
            InitializeComponent();
            Propietario1 = propietario1;
            Propietario2 = propietario2;
            Fecha = fecha;
            Fecha1 = fecha1;
            Fecha2 = fecha2;
            Fecha3 = fecha;
            Fecha4 = fecha1;
            Fecha5 = fecha2;
        }

        private void ReporteAnticiposAplicados_Load(object sender, EventArgs e)
        {
            if (Propietario1 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.Fill(this.ControlCondominiosDataSet52.AnticipoCobros);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy1(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy2(this.ControlCondominiosDataSet52.AnticipoCobros, Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet52.AnticipoCobros' Puede moverla o quitarla según sea necesario.
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fecha = Fecha3;
            Fecha1 = Fecha4;
            Fecha2 = Fecha5;

            if (Propietario1 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.Fill(this.ControlCondominiosDataSet52.AnticipoCobros);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy1(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy2(this.ControlCondominiosDataSet52.AnticipoCobros, Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroFecha filtroFecha = new FiltroFecha();
            filtroFecha.ShowDialog();
        }

        private void ReporteAnticiposAplicados_Activated(object sender, EventArgs e)
        {
            if (Propietario1 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.Fill(this.ControlCondominiosDataSet52.AnticipoCobros);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy1(this.ControlCondominiosDataSet52.AnticipoCobros, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            {
                this.AnticipoCobrosTableAdapter.FillBy2(this.ControlCondominiosDataSet52.AnticipoCobros, Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
        }

        private void ReporteAnticiposAplicados_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReporteEstadoCuenta.Fecha1 = string.Empty;
            ReporteEstadoCuenta.Fecha2 = string.Empty;
            ReporteEstadoCuenta2.Fecha1 = string.Empty;
            ReporteEstadoCuenta2.Fecha2 = string.Empty;
            ReporteEstadoCuenta.Fecha = string.Empty;
            ReporteEstadoCuenta2.Fecha = string.Empty;
            ReporteAnticipos.Fecha1 = string.Empty;
            ReporteAnticipos.Fecha2 = string.Empty;
            ReporteAnticipos.Fecha = string.Empty;
            ReporteSaldoCondominio.fecha1 = string.Empty;
            ReporteSaldoCondominio.fecha2 = string.Empty;
            ReporteSaldoCondominio.fecha = string.Empty;
            ReporteSaldoPropietario.fecha1 = string.Empty;
            ReporteSaldoPropietario.fecha2 = string.Empty;
            ReporteSaldoPropietario.fecha = string.Empty;
        }
    }
}
