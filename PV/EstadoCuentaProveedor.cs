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
using PV.Clases.ReporteCompras;
using PV.Clases.CuentasBancarias;

namespace PV
{
    public partial class EstadoCuentaProveedor : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string Pagos = string.Empty;
        string Saldo = string.Empty;
        string Fecha = string.Empty;
        string Fecha1 = string.Empty;
        string Fecha2 = string.Empty;
        string empresa = string.Empty;
        string TTotal = string.Empty;
        string TSaldo = string.Empty;
        int firma = 0;
        string Anticipo = string.Empty;

        public EstadoCuentaProveedor(string Proveedor, string pagos, string saldo, string fecha, string fecha1, string fecha2, int Firma, string anticipo)
        {
            InitializeComponent();
            provedor = Proveedor;
            Pagos = pagos;
            Saldo = saldo;
            Fecha = fecha;
            Fecha1 = fecha1;
            Fecha2 = fecha2;
            empresa = DBLogin.DatosEmpresa;
            firma = Firma;
            Anticipo = anticipo;
        }

        private void EstadoCuentaProveedor_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor2(cmbPropietario1);
            //cmbPropietario1.SelectedIndex=0;
            //cmbPropietario1.Text = provedor;

            int? claveProveedor = (string.IsNullOrWhiteSpace(provedor) || provedor == "0") ? (int?)null : Convert.ToInt32(provedor);
            DateTime? fechaInicio = Fecha=="No" ? (DateTime?)null : Convert.ToDateTime(Fecha1);
            DateTime? fechaFin = Fecha == "No" ? (DateTime?)null : Convert.ToDateTime(Fecha2);
            string claveDocumento =  null;
            int? cuentaBancaria = (int?)null ;

            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            //if (Fecha != string.Empty)
            //{
            //    c.totalesFecha(provedor, Fecha2);
            //}
            //else
            //{
            //    c.totales2(provedor);
            //}

            TTotal = DBLogin.Total.ToString("N", formato);
            TSaldo = DBLogin.Saldo.ToString("N", formato);
            //ReportParameter[] parameters = new ReportParameter[6];
            ////Establecemos el valor de los parámetros
            //parameters[0] = new ReportParameter("Fecha1", Fecha1);
            //parameters[1] = new ReportParameter("Fecha2", Fecha2);
            //parameters[2] = new ReportParameter("empresa", empresa);
            //parameters[3] = new ReportParameter("total", "$" + TTotal);
            //parameters[4] = new ReportParameter("saldo", "$" + TSaldo);
            //parameters[5] = new ReportParameter("firma", firma.ToString());
            //this.reportViewer1.LocalReport.SetParameters(parameters);

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet38.Egreso' Puede moverla o quitarla según sea necesario.
            this.EgresoTableAdapter.Fill(
                            this.ControlCondominiosDataSet38.Egreso,
                            claveProveedor,
                            fechaInicio,
                            fechaFin,
                            claveDocumento,
                            cuentaBancaria

                        );            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet40.Proveedor' Puede moverla o quitarla según sea necesario.
            this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet40.Proveedor, cmbPropietario1.Text);

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet38.Egreso, cmbPropietario1.Text);
            this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet40.Proveedor, cmbPropietario1.Text);

            this.reportViewer1.RefreshReport();
        }
    }
}
