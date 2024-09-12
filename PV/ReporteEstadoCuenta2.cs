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
using PuntoVentas.Clases.Login;
using System.Globalization;

namespace PV
{
    public partial class ReporteEstadoCuenta2 : Form
    {
        DBLogin c = new DBLogin();
        string RazonSocial = string.Empty;
        string Pagos = string.Empty;
        string Saldo = string.Empty;
        public static string Fecha = string.Empty;
        public static string Fecha1 = string.Empty;
        public static string Fecha2 = string.Empty;
        string Fecha3 = string.Empty;
        string Fecha4 = string.Empty;
        string empresa = string.Empty;
        string TTotal = string.Empty;
        string TSaldo = string.Empty;
        int firma = 0;
        string Anticipo = string.Empty;
        string Propiedad = string.Empty;

        public ReporteEstadoCuenta2(string razonsocial, string pagos, string saldo, string fecha, string fecha1, string fecha2, int Firma, string anticipo, string propiedad)
        {
            InitializeComponent();
            RazonSocial = razonsocial;
            Pagos = pagos;
            Saldo = saldo;
            Fecha = fecha;
            Fecha4 = fecha;
            Fecha1 = fecha1;
            Fecha2 = fecha2;
            Fecha3 = fecha2;
            empresa = DBLogin.DatosEmpresa;
            firma = Firma;
            Anticipo = anticipo;
            Propiedad = propiedad;
        }

        private void ReporteEstadoCuenta2_Load(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            if (Fecha != string.Empty)
            {
                c.totalesFecha(RazonSocial, Fecha1, Fecha2);
            }
            else
            {
                c.totales(RazonSocial);
            }
            TTotal = DBLogin.Total.ToString("N", formato);
            TSaldo = DBLogin.Saldo.ToString("N", formato);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet22.Recibo' Puede moverla o quitarla según sea necesario.
            ReportParameter[] parameters = new ReportParameter[6];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", Fecha1);
            parameters[1] = new ReportParameter("Fecha2", Fecha2);
            parameters[2] = new ReportParameter("empresa", empresa);
            parameters[3] = new ReportParameter("total", "$" + TTotal);
            parameters[4] = new ReportParameter("saldo", "$" + TSaldo);
            parameters[5] = new ReportParameter("firma", firma.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (Pagos == "No")
            {
                if (Saldo == "Si")
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy8(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy7(this.ControlCondominiosDataSet22.Recibo, Propiedad, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
                else
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy10(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy9(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroFecha filtroFecha = new FiltroFecha();
            filtroFecha.ShowDialog();
        }

        private void ReporteEstadoCuenta2_Activated(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            if (Fecha != string.Empty)
            {
                c.totalesFecha(RazonSocial, Fecha1, Fecha2);
            }
            else
            {
                c.totales(RazonSocial);
            }
            TTotal = DBLogin.Total.ToString("N", formato);
            TSaldo = DBLogin.Saldo.ToString("N", formato);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet22.Recibo' Puede moverla o quitarla según sea necesario.
            ReportParameter[] parameters = new ReportParameter[6];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", Fecha1);
            parameters[1] = new ReportParameter("Fecha2", Fecha2);
            parameters[2] = new ReportParameter("empresa", empresa);
            parameters[3] = new ReportParameter("total", "$" + TTotal);
            parameters[4] = new ReportParameter("saldo", "$" + TSaldo);
            parameters[5] = new ReportParameter("firma", firma.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (Pagos == "No")
            {
                if (Saldo == "Si")
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy8(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy7(this.ControlCondominiosDataSet22.Recibo, Propiedad, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
                else
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy10(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy9(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fecha1 = "1990/01/01";
            Fecha2 = Fecha3;
            Fecha = Fecha4;

            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            if (Fecha != string.Empty)
            {
                c.totalesFecha(RazonSocial, Fecha1, Fecha2);
            }
            else
            {
                c.totales(RazonSocial);
            }
            TTotal = DBLogin.Total.ToString("N", formato);
            TSaldo = DBLogin.Saldo.ToString("N", formato);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet22.Recibo' Puede moverla o quitarla según sea necesario.
            ReportParameter[] parameters = new ReportParameter[6];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", Fecha1);
            parameters[1] = new ReportParameter("Fecha2", Fecha2);
            parameters[2] = new ReportParameter("empresa", empresa);
            parameters[3] = new ReportParameter("total", "$" + TTotal);
            parameters[4] = new ReportParameter("saldo", "$" + TSaldo);
            parameters[5] = new ReportParameter("firma", firma.ToString());
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (Pagos == "No")
            {
                if (Saldo == "Si")
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy8(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy7(this.ControlCondominiosDataSet22.Recibo, Propiedad, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
                else
                {
                    if (Fecha == "Si")
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy10(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Fecha1, Fecha2, Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                    else
                    {
                        if (Propiedad == string.Empty)
                        {
                            this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial));
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            this.ReciboTableAdapter.FillBy9(this.ControlCondominiosDataSet22.Recibo, Convert.ToInt32(RazonSocial), Propiedad);
                            this.reportViewer1.RefreshReport();
                        }
                    }
                }
            }
        }

        private void ReporteEstadoCuenta2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReporteEstadoCuenta.Fecha1 = string.Empty;
            ReporteEstadoCuenta.Fecha2 = string.Empty;
            ReporteEstadoCuenta.Fecha = string.Empty;
            ReporteAnticipos.Fecha1 = string.Empty;
            ReporteAnticipos.Fecha2 = string.Empty;
            ReporteAnticipos.Fecha = string.Empty;
            ReporteAnticiposAplicados.Fecha1 = string.Empty;
            ReporteAnticiposAplicados.Fecha2 = string.Empty;
            ReporteAnticiposAplicados.Fecha = string.Empty;
            ReporteSaldoCondominio.fecha1 = string.Empty;
            ReporteSaldoCondominio.fecha2 = string.Empty;
            ReporteSaldoCondominio.fecha = string.Empty;
            ReporteSaldoPropietario.fecha1 = string.Empty;
            ReporteSaldoPropietario.fecha2 = string.Empty;
            ReporteSaldoPropietario.fecha = string.Empty;
        }
    }
}
