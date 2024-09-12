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
        string propietario1 = string.Empty;
        string propietario2 = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;
        string Divisa = string.Empty;
        string TipoCambio = string.Empty;

        public ReporteSaldosProveedores(string Propietario1, string Propietario2, string Fecha, string Fecha1, string Fecha2, string divisa, string tipocambio)
        {
            InitializeComponent();
            propietario1 = Propietario1;
            propietario2 = Propietario2;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
            Divisa = divisa;
            TipoCambio = tipocambio;
        }

        private void ReporteSaldosProveedores_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietariosNombreProveedor(cmbPropietario1);
            c.SeleccionarPropietariosNombreProveedor(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;

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
            ReportParameter[] parameters = new ReportParameter[2];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Fecha1", dtFecha1.Text);
            parameters[1] = new ReportParameter("Fecha2", dtFecha2.Text);
            this.reportViewer1.LocalReport.SetParameters(parameters);
            //this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Divisa != "TODAS")
            {

                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

                    this.reportViewer1.RefreshReport();
                }
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


            if (Divisa != "TODAS")
            {

                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

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

            if (Divisa != "TODAS")
            {

                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy6(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy5(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2, Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy4(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Divisa);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy3(this.ControlCondominiosDataSet56.Proveedor, Divisa);

                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy2(this.ControlCondominiosDataSet56.Proveedor, fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == true)
                {

                    this.ProveedorTableAdapter.FillBy1(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), fecha1, fecha2);

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.FillBy(this.ControlCondominiosDataSet56.Proveedor, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));

                    this.reportViewer1.RefreshReport();
                }
                else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS" && cbFechas.Checked == false)
                {
                    this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet56.Proveedor);

                    this.reportViewer1.RefreshReport();
                }
            }
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
