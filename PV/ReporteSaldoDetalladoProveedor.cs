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
    public partial class ReporteSaldoDetalladoProveedor : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        string propietario1 = string.Empty;
        string propietario2 = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;
        string Saldados = string.Empty;
        string empresa = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;
        string Divisa = string.Empty;
        string TipoCambio = string.Empty;

        public ReporteSaldoDetalladoProveedor(string Propietario1, string Propietario2, string Fecha, string Fecha1, string Fecha2, string saldados, string divisa, string tipocambio)
        {
            InitializeComponent();
            propietario1 = Propietario1;
            propietario2 = Propietario2;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
            Saldados = saldados;
            empresa = DBLogin.DatosEmpresa;
            Divisa = divisa;
            TipoCambio = tipocambio;
        }

        private void ReporteSaldoDetalladoProveedor_Load(object sender, EventArgs e)
        {
            ControlCondominiosDataSet57.EnforceConstraints = false;
            c.SeleccionarPropietariosNombreProveedor(cmbPropietario1);
            c.SeleccionarPropietariosNombreProveedor(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;

            cmbPropietario1.Text = propietario1;
            cmbpropietario2.Text = propietario2;
            cmbSaldos.Text = Saldados;
            if (fecha == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }
            ReportParameter[] parameters = new ReportParameter[5];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            parameters[2] = new ReportParameter("Empresa", empresa);
            parameters[3] = new ReportParameter("Divisa", Divisa);
            parameters[4] = new ReportParameter("TipoCambio", TipoCambio);
            this.reportViewer1.LocalReport.SetParameters(parameters);


            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpropietario2.Text = cmbPropietario1.Text;

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
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


            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
        }

        private void cmbSaldos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
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
            parameters[2] = new ReportParameter("Empresa", empresa);
            parameters[3] = new ReportParameter("Divisa", Divisa);
            parameters[4] = new ReportParameter("TipoCambio", TipoCambio);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }


            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
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
            parameters[2] = new ReportParameter("Empresa", empresa);
            parameters[3] = new ReportParameter("Divisa", Divisa);
            parameters[4] = new ReportParameter("TipoCambio", TipoCambio);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
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
            parameters[2] = new ReportParameter("Empresa", empresa);
            parameters[3] = new ReportParameter("Divisa", Divisa);
            parameters[4] = new ReportParameter("TipoCambio", TipoCambio);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy2(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "Si")
            {

                this.EgresoTableAdapter.FillBy1(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.FillBy(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "Si")
            {
                this.EgresoTableAdapter.Fill(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
            }
            //___
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy6(this.ControlCondominiosDataSet57.Egreso, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true && cmbSaldos.Text == "No")
            {

                this.EgresoTableAdapter.FillBy5(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy4(this.ControlCondominiosDataSet57.Egreso, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                this.reportViewer1.RefreshReport();
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false && cmbSaldos.Text == "No")
            {
                this.EgresoTableAdapter.FillBy3(this.ControlCondominiosDataSet57.Egreso);

                this.reportViewer1.RefreshReport();
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
    }
}
