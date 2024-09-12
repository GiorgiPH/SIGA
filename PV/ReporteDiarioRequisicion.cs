using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteDiarioRequisicion : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteDiarioRequisicion(string Proveedor, string Fecha, string Fecha1, string Fecha2)
        {
            InitializeComponent();
            provedor = Proveedor;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
        }

        private void ReporteDiarioRequisicion_Load(object sender, EventArgs e)
        {
            c.SeleccionarCentroCosto(cmbPropietario1);
            //cmbPropietario1.SelectedIndex = 0;

            cmbPropietario1.Text = provedor;
            if (fecha == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet35.Requisicion' Puede moverla o quitarla según sea necesario.

            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.Fill(this.ControlCondominiosDataSet35.Requisicion);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.FillBy(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy1(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy2(this.ControlCondominiosDataSet35.Requisicion, dtFecha1.Text, dtFecha2.Text);
            }

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.Fill(this.ControlCondominiosDataSet35.Requisicion);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.FillBy(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy1(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy2(this.ControlCondominiosDataSet35.Requisicion, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
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

            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.Fill(this.ControlCondominiosDataSet35.Requisicion);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.FillBy(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy1(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy2(this.ControlCondominiosDataSet35.Requisicion, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
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

            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.Fill(this.ControlCondominiosDataSet35.Requisicion);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.FillBy(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy1(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy2(this.ControlCondominiosDataSet35.Requisicion, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
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

            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.Fill(this.ControlCondominiosDataSet35.Requisicion);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RequisicionTableAdapter.FillBy(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy1(this.ControlCondominiosDataSet35.Requisicion, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RequisicionTableAdapter.FillBy2(this.ControlCondominiosDataSet35.Requisicion, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
