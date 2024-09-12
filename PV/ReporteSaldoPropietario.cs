using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;
using Microsoft.Reporting.WinForms;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class ReporteSaldoPropietario : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        public static string propietario1 = string.Empty;
        public static string propietario2 = string.Empty;
        string propietario3 = string.Empty;
        string propietario4 = string.Empty;
        public static string fecha = string.Empty;
        public static string fecha1 = string.Empty;
        public static string fecha2 = string.Empty;
        string fecha3 = string.Empty;
        string fecha4 = string.Empty;
        string fecha5 = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;
        string Divisa = string.Empty;
        string TipoCambio = string.Empty;

        public ReporteSaldoPropietario(string Propietario1, string Propietario2, string Fecha, string Fecha1, string Fecha2, string divisa, string tipocambio)
        {
            InitializeComponent();
            propietario1 = Propietario1;
            propietario2 = Propietario2;
            propietario3 = Propietario1;
            propietario4 = Propietario2;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
            fecha3 = Fecha;
            fecha4 = Fecha1;
            fecha5 = Fecha2;
            Divisa = divisa;
            TipoCambio = tipocambio;
        }

        private void ReporteSaldoPropietario_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietarios(cmbPropietario1);
            c.SeleccionarPropietarios(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;
            cmbSaldos.Text = "Si";

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet15.Recibo' Puede moverla o quitarla según sea necesario.
            cmbPropietario1.Text = propietario1;
            cmbpropietario2.Text = propietario2;
            if (fecha == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }

            if (cmbPropietario1.Text != "TODOS")
            {
                 pro1 = cmbPropietario1.Text.IndexOf(" -");
                 Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                 pro2 = cmbpropietario2.Text.IndexOf(" -");
                 Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }
            ReportParameter[] parameters = new ReportParameter[5];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            parameters[2] = new ReportParameter("Divisa", Divisa);
            parameters[3] = new ReportParameter("TipoCambio", TipoCambio);
            parameters[4] = new ReportParameter("Empresa", DBLogin.DatosEmpresa);
           
            this.reportViewer1.LocalReport.SetParameters(parameters);
            //this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (cmbSaldos.Text=="Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
          

        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpropietario2.Text = cmbPropietario1.Text;

            if (cmbPropietario1.Text!= "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }


            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpropietario2.Text == "TODOS")
            {
                cmbPropietario1.Text = "TODOS";
            }
            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }
            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
            }
            ReportParameter[] parameters = new ReportParameter[5];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            parameters[2] = new ReportParameter("Divisa", Divisa);
            parameters[3] = new ReportParameter("TipoCambio", TipoCambio);
            parameters[4] = new ReportParameter("Empresa", DBLogin.DatosEmpresa);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
            }

            ReportParameter[] parameters = new ReportParameter[5];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            parameters[2] = new ReportParameter("Divisa", Divisa);
            parameters[3] = new ReportParameter("TipoCambio", TipoCambio);
            parameters[4] = new ReportParameter("Empresa", DBLogin.DatosEmpresa);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
            }

            ReportParameter[] parameters = new ReportParameter[5];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            parameters[2] = new ReportParameter("Divisa", Divisa);
            parameters[3] = new ReportParameter("TipoCambio", TipoCambio);
            parameters[4] = new ReportParameter("Empresa", DBLogin.DatosEmpresa);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            if (dtFecha2.Value < dtFecha1.Value)
            {
                dtFecha2.Text = dtFecha1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            propietario1 = propietario3;
            propietario2 = propietario4;

            cmbPropietario1.Text = propietario1;
            cmbpropietario2.Text = propietario2;

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }
            cmbSaldos.Text = "Si";
            fecha = fecha3;
            fecha1 = fecha4;
            fecha3 = fecha5;

            if (fecha4 == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }
            else
            {
                cbFechas.Checked = false;
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroFecha filtroFecha = new FiltroFecha();
            filtroFecha.ShowDialog();
        }

        private void ReporteSaldoPropietario_Activated(object sender, EventArgs e)
        {
            cmbPropietario1.Text = propietario1;
            cmbpropietario2.Text = propietario2;

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (fecha == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void ReporteSaldoPropietario_FormClosing(object sender, FormClosingEventArgs e)
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
            ReporteAnticiposAplicados.Fecha1 = string.Empty;
            ReporteAnticiposAplicados.Fecha2 = string.Empty;
            ReporteAnticiposAplicados.Fecha = string.Empty;
            ReporteSaldoCondominio.fecha1 = string.Empty;
            ReporteSaldoCondominio.fecha2 = string.Empty;
            ReporteSaldoCondominio.fecha = string.Empty;
            ReporteSaldoCondominio.propietario1 = cmbPropietario1.Text;
            ReporteSaldoCondominio.propietario2 = cmbpropietario2.Text;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            FiltroPropietario filtroPropietario = new FiltroPropietario();
            filtroPropietario.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbSaldos.Text == "Si")
            {
                cmbSaldos.Text = "No";
            }
            else
            {
                cmbSaldos.Text = "Si";
            }
        }

        private void cmbSaldos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbSaldos.Text == "Si")
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy2(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy1(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy6(this.ControlCondominiosDataSet15.Recibo, dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ReciboTableAdapter.FillBy5(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy4(this.ControlCondominiosDataSet15.Recibo, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ReciboTableAdapter.FillBy3(this.ControlCondominiosDataSet15.Recibo);

                    this.reportViewer1.RefreshReport();
                }
            }
        }
    }
}
