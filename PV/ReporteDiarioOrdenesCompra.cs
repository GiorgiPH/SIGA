using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteDiarioOrdenesCompra : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteDiarioOrdenesCompra(string Proveedor,string Fecha, string Fecha1, string Fecha2)
        {
            InitializeComponent();
            provedor = Proveedor;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
        }

        private void ReporteDiarioOrdenesCompra_Load(object sender, EventArgs e)
        {

            c.SeleccionarProveedor(cmbPropietario1);
            //cmbPropietario1.SelectedIndex = 0;

            cmbPropietario1.Text = provedor;
            if (fecha=="Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet34.OrdenCompra);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.FillBy(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy1(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy2(this.ControlCondominiosDataSet34.OrdenCompra, dtFecha1.Text, dtFecha2.Text);
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
                this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet34.OrdenCompra);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.FillBy(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy1(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy2(this.ControlCondominiosDataSet34.OrdenCompra, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet34.OrdenCompra);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.FillBy(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy1(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy2(this.ControlCondominiosDataSet34.OrdenCompra, dtFecha1.Text, dtFecha2.Text);
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
                this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet34.OrdenCompra);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.FillBy(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy1(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy2(this.ControlCondominiosDataSet34.OrdenCompra, dtFecha1.Text, dtFecha2.Text);
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
                this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet34.OrdenCompra);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.OrdenCompraTableAdapter.FillBy(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy1(this.ControlCondominiosDataSet34.OrdenCompra, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.OrdenCompraTableAdapter.FillBy2(this.ControlCondominiosDataSet34.OrdenCompra, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
