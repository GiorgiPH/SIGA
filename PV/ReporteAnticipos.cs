using Microsoft.Reporting.WinForms;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteAnticipos : Form
    {
        public static string Propietario1 = string.Empty;
        public static string Propietario2 = string.Empty;
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
        string FormaPago=string.Empty;
        string Cuenta = string.Empty;

        public ReporteAnticipos(string propietario1, string propietario2, string fecha, string fecha1, string fecha2, string formaPago, string cuenta)
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
            FormaPago= formaPago;
            Cuenta= cuenta;
        }

        private void ReporteAnticipos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.LocalReport.DataSources.Clear();
            DataTable dataTable2 = new DataTable();

            // Crea una conexión a la base de datos
            SqlConnection connection2 = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

            // Crea un comando para la consulta SQL
            SqlCommand command2 = new SqlCommand("SELECT RazonSocial, NombreComercial, RFC, Telefono1, Telefono2, Telefono3, Correo, PaginaWeb, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Referencias, LeyendaTicket, Foto, Contraseña, Recargos, Descuentos, RecibosAutomaticos, Documento FROM dbo.DatosEmpresa", connection2);

            // Crea un adaptador de datos para recuperar los resultados de la consulta
            SqlDataAdapter adapter2 = new SqlDataAdapter(command2);

            // Llena el DataTable con los resultados de la consulta
            adapter2.Fill(dataTable2);












            //this.reportViewer1.LocalReport.DataSources.Clear();
            if (Propietario1 != "TODOS" && Propietario2 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }
            else
            {
                Propi1 = string.Empty;
                Propi2 = string.Empty;
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            string p = string.Empty;
            if (Propietario1 == "TODOS" || Propietario2 == "TODOS")
            {
                p = "TODOS";
            }


            string query = "select A.*, P.RazonSocial, C.Descripcion from Anticipo as A, Clientes as P, ConceptosIngreso as C, CuentasBancarias as CB where CB.Clave=A.CuentaBancaria and A.ClavePropietario=P.IdCliente and A.Concepto=C.Clave and Activo is null";
            if(Propi1 != string.Empty && Propi2 != string.Empty)
            {
                query += " and A.ClavePropietario between '" + Convert.ToInt32(Propi1) + "' and '" + Convert.ToInt32(Propi2) + "'";
            }
            if(Fecha != string.Empty)
            {
                query += " and A.Fecha between '" + Fecha1 + "' and '" + Fecha2 + "'";
            }
            if(FormaPago != "TODOS")
            {
                query += " and A.FormaPago='" + FormaPago  + "'";
            }
            if(Cuenta != "0")
            {
                query += " and cuentabancaria='" + Cuenta+"'";
            }
            SqlConnection connection = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            // Crea una conexión a la base de datos
            // Crea un adaptador de datos para recuperar los resultados de la consulta
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // Llena el DataTable con los resultados de la consulta
            adapter.Fill(dataTable);
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataTable2));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dataTable));


            ReportParameter[] parameters = new ReportParameter[1];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Propietario", p);

            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
            //if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            //{
            //    this.AnticipoTableAdapter.Fill(this.ControlCondominiosDataSet53.Anticipo);
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy1(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy2(this.ControlCondominiosDataSet53.Anticipo, Fecha1, Fecha2);
            //    this.reportViewer1.RefreshReport();
            //}
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet53.Anticipo' Puede moverla o quitarla según sea necesario.
            this.reportViewer1.ZoomPercent = 150;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroFecha filtroFecha = new FiltroFecha();
            filtroFecha.ShowDialog();
        }

        private void ReporteAnticipos_Activated(object sender, EventArgs e)
        {
            //if (Propietario1 != "TODOS" && Propietario2 != "TODOS")
            //{
            //    pro1 = Propietario1.IndexOf(" -");
            //    Propi1 = Propietario1.Substring(0, pro1);
            //    pro2 = Propietario2.IndexOf(" -");
            //    Propi2 = Propietario2.Substring(0, pro2);
            //}
            //else
            //{
            //    Propi1 = string.Empty;
            //    Propi2 = string.Empty;
            //}
            //string p = string.Empty;
            //if (Propietario1 == "TODOS" || Propietario2 == "TODOS")
            //{
            //    p = "TODOS";
            //}

            //ReportParameter[] parameters = new ReportParameter[1];
            ////Establecemos el valor de los parámetros
            //parameters[0] = new ReportParameter("Propietario", p);

            //this.reportViewer1.LocalReport.SetParameters(parameters);

            //this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            //if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            //{
            //    this.AnticipoTableAdapter.Fill(this.ControlCondominiosDataSet53.Anticipo);
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy1(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
            //    this.reportViewer1.RefreshReport();
            //}
            //else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            //{
            //    this.AnticipoTableAdapter.FillBy2(this.ControlCondominiosDataSet53.Anticipo, Fecha1, Fecha2);
            //    this.reportViewer1.RefreshReport();
            //}
            //this.reportViewer1.ZoomPercent = 150;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fecha = Fecha3;
            Fecha1 = Fecha4;
            Fecha2 = Fecha5;
            Propietario1 = "TODOS";
            Propietario2 = "TODOS";

            if (Propietario1 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }
            else
            {
                Propi1 = string.Empty;
                Propi2 = string.Empty;
            }
            string p = string.Empty;
            if (Propietario1 == "TODOS" || Propietario2 == "TODOS")
            {
                p = "TODOS";
            }

            ReportParameter[] parameters = new ReportParameter[1];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Propietario", p);

            this.reportViewer1.LocalReport.SetParameters(parameters);

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            {
                this.AnticipoTableAdapter.Fill(this.ControlCondominiosDataSet53.Anticipo);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            {
                this.AnticipoTableAdapter.FillBy(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            {
                this.AnticipoTableAdapter.FillBy1(this.ControlCondominiosDataSet53.Anticipo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            {
                this.AnticipoTableAdapter.FillBy2(this.ControlCondominiosDataSet53.Anticipo, Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            this.reportViewer1.ZoomPercent = 150;
        }

        private void ReporteAnticipos_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReporteEstadoCuenta.Fecha1 = string.Empty;
            ReporteEstadoCuenta.Fecha2 = string.Empty;
            ReporteEstadoCuenta2.Fecha1 = string.Empty;
            ReporteEstadoCuenta2.Fecha2 = string.Empty;
            ReporteEstadoCuenta.Fecha = string.Empty;
            ReporteEstadoCuenta2.Fecha = string.Empty;
            ReporteAnticiposAplicados.Fecha1 = string.Empty;
            ReporteAnticiposAplicados.Fecha2 = string.Empty;
            ReporteAnticiposAplicados.Fecha = string.Empty;
            ReporteSaldoCondominio.fecha1 = string.Empty;
            ReporteSaldoCondominio.fecha2 = string.Empty;
            ReporteSaldoCondominio.fecha = string.Empty;
            ReporteSaldoPropietario.fecha1 = string.Empty;
            ReporteSaldoPropietario.fecha2 = string.Empty;
            ReporteSaldoPropietario.fecha = string.Empty;
            ReporteSaldoCondominio.propietario1 = string.Empty;
            ReporteSaldoCondominio.propietario2 = string.Empty;
            ReporteSaldoPropietario.propietario1 = string.Empty;
            ReporteSaldoPropietario.propietario2 = string.Empty;
            ReporteIngresos.Propietario1 = string.Empty;
            ReporteIngresos.Propietario2 = string.Empty;
            ReporteAnticipos.Propietario1 = string.Empty;
            ReporteAnticipos.Propietario2 = string.Empty;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FiltroPropietario filtroPropiedad = new FiltroPropietario();
            filtroPropiedad.ShowDialog();
        }
    }
}
