using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.ActivoFijo;

namespace PV
{
    public partial class ReporteResguardoActivoFijo2 : Form
    {
        DBActivoFijo c = new DBActivoFijo();

        public ReporteResguardoActivoFijo2()
        {
            InitializeComponent();
        }

        private void ReporteResguardoActivoFijo2_Load(object sender, EventArgs e)
        {
            c.SeleccionarActivoFijo2(cmbActivo);
            c.SeleccionarProveedor(cmbProveedor);
            cmbActivo.SelectedIndex = 0;
            cmbProveedor.SelectedIndex = 0;
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet20.ResguardoActivoFijo' Puede moverla o quitarla según sea necesario.
            this.ResguardoActivoFijoTableAdapter.Fill(this.ControlCondominiosDataSet20.ResguardoActivoFijo);

            this.reportViewer1.RefreshReport();
        }

        private void cmbActivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
        }

        private void label3_Click(object sender, EventArgs e)
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

            if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy7(this.ControlCondominiosDataSet20.ResguardoActivoFijo, dtFecha1.Text, dtFecha2.Text);

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

            if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy7(this.ControlCondominiosDataSet20.ResguardoActivoFijo, dtFecha1.Text, dtFecha2.Text);

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

            if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy7(this.ControlCondominiosDataSet20.ResguardoActivoFijo, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.Text != "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ResguardoActivoFijoTableAdapter.FillBy4(this.ControlCondominiosDataSet20.ResguardoActivoFijo, cmbProveedor.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
        }
    }
}
