using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;
namespace PV
{
    public partial class ReporteDiarioCompras : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string cuentaBancaria = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteDiarioCompras(string Proveedor, string Tipo, string Fecha, string Fecha1, string Fecha2)
        {
            InitializeComponent();
            provedor = Proveedor;
            cuentaBancaria = Tipo;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
        }

        private void ReporteDiarioCompras_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;
            cmbTipo.SelectedIndex = 0;
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            cmbPropietario1.Text = provedor;
            cmbTipo.Text = cuentaBancaria;
            if (fecha == "Si")
            {
                cbFechas.Checked = true;
                dtFecha1.Text = fecha1;
                dtFecha2.Text = fecha2;
            }

            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
            }

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text=="TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
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

            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
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

            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
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

            if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.Fill(this.ControlCondominiosDataSet36.RecepcionProducto);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy1(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy2(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.RecepcionProductoTableAdapter.FillBy3(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy4(this.ControlCondominiosDataSet36.RecepcionProducto, cmbPropietario1.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy5(this.ControlCondominiosDataSet36.RecepcionProducto, cmbTipo.Text, dtFecha1.Text, dtFecha2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbTipo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.RecepcionProductoTableAdapter.FillBy6(this.ControlCondominiosDataSet36.RecepcionProducto, dtFecha1.Text, dtFecha2.Text);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
