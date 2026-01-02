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

namespace PV
{
    public partial class ReporteSaldosProveedores : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        string IdProveedor = string.Empty;
        string FechaInicio = string.Empty;
        string FechaFin = string.Empty;
        string Documento = string.Empty;
        string CuentaBancaria = string.Empty;

        public ReporteSaldosProveedores(string idProveedor, string documento, string fechaInicio, string fechaFin, string cuentaBancaria)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Documento = documento;
            CuentaBancaria = cuentaBancaria;
        }

        private void ReporteSaldosProveedores_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietariosNombreProveedor(cmbPropietario1);
            c.SeleccionarPropietariosNombreProveedor(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;

        
         
            ReportParameter[] parameters = new ReportParameter[2];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            this.reportViewer1.LocalReport.SetParameters(parameters);
            //this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            int? claveProveedor = (string.IsNullOrWhiteSpace(IdProveedor) || IdProveedor == "0") ? (int?)null : Convert.ToInt32(IdProveedor);
            DateTime? fechaInicio = string.IsNullOrWhiteSpace(FechaInicio) ? (DateTime?)null : Convert.ToDateTime(FechaInicio);
            DateTime? fechaFin = string.IsNullOrWhiteSpace(FechaFin) ? (DateTime?)null : Convert.ToDateTime(FechaFin);
            string claveDocumento = string.IsNullOrWhiteSpace(Documento) ? null : Documento;

            this.ControlCondominiosDataSet56.EnforceConstraints = false;

            this.ProveedorTableAdapter.Fill(
                this.ControlCondominiosDataSet56.Proveedor,
                claveProveedor,
                claveProveedor,
                string.IsNullOrEmpty(FechaInicio) ? "No" : "Si",
                fechaInicio,
                fechaFin

            );
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", this.ControlCondominiosDataSet56.Tables[0]));



            this.reportViewer1.RefreshReport();


        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
        
        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbFechas.Checked == true)
            //{
            //    dtFecha1.Enabled = true;
            //    dtFecha2.Enabled = true;
            //}
            //else
            //{
            //    dtFecha1.Enabled = false;
            //    dtFecha2.Enabled = false;
            //}
            //ReportParameter[] parameters = new ReportParameter[2];
            ////Establecemos el valor de los parámetros
            //parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            //parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            //this.reportViewer1.LocalReport.SetParameters(parameters);

            //if (cmbPropietario1.Text != "TODOS")
            //{
            //    pro1 = cmbPropietario1.Text.IndexOf(" -");
            //    Propi1 = cmbPropietario1.Text.Substring(0, pro1);
            //    pro2 = cmbpropietario2.Text.IndexOf(" -");
            //    Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            //}

            //if (Divisa != "TODAS")
            //{

            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
            //else
            //{
            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            //if (cbFechas.Checked == true)
            //{
            //    dtFecha1.Enabled = true;
            //    dtFecha2.Enabled = true;
            //}
            //else
            //{
            //    dtFecha1.Enabled = false;
            //    dtFecha2.Enabled = false;
            //}

            //ReportParameter[] parameters = new ReportParameter[2];
            ////Establecemos el valor de los parámetros
            //parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            //parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            //this.reportViewer1.LocalReport.SetParameters(parameters);

            //if (cmbPropietario1.Text != "TODOS")
            //{
            //    pro1 = cmbPropietario1.Text.IndexOf(" -");
            //    Propi1 = cmbPropietario1.Text.Substring(0, pro1);
            //    pro2 = cmbpropietario2.Text.IndexOf(" -");
            //    Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            //}

            //if (Divisa != "TODAS")
            //{

            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
            //else
            //{
            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            //if (cbFechas.Checked == true)
            //{
            //    dtFecha1.Enabled = true;
            //    dtFecha2.Enabled = true;
            //}
            //else
            //{
            //    dtFecha1.Enabled = false;
            //    dtFecha2.Enabled = false;
            //}

            //ReportParameter[] parameters = new ReportParameter[2];
            ////Establecemos el valor de los parámetros
            //parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            //parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            //this.reportViewer1.LocalReport.SetParameters(parameters);

            //if (cmbPropietario1.Text != "TODOS")
            //{
            //    pro1 = cmbPropietario1.Text.IndexOf(" -");
            //    Propi1 = cmbPropietario1.Text.Substring(0, pro1);
            //    pro2 = cmbpropietario2.Text.IndexOf(" -");
            //    Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            //}

            //if (Divisa != "TODAS")
            //{

            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
            //else
            //{
            //    if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
            //    {

            //        this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

            //        this.reportViewer1.RefreshReport();
            //    }
            //    else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
            //    {
            //        this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

            //        this.reportViewer1.RefreshReport();
            //    }
            //}
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
