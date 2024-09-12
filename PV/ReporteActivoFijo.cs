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
    public partial class ReporteActivoFijo : Form
    {
        DBActivoFijo c = new DBActivoFijo();

        public ReporteActivoFijo()
        {
            InitializeComponent();
        }

        private void ReporteActivoFijo_Load(object sender, EventArgs e)
        {
            c.SeleccionarActivoFijo2(cmbActivo);
            c.SeleccionarProveedor(cmbProveedor);
            cmbProveedor.SelectedIndex = 0;
            cmbActivo.SelectedIndex = 0;

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet18.ActivoFijo' Puede moverla o quitarla según sea necesario.
            this.ActivoFijoTableAdapter.Fill(this.ControlCondominiosDataSet18.ActivoFijo);

            this.reportViewer1.RefreshReport();
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.Text != "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked==false)
            {
                this.ActivoFijoTableAdapter.FillBy(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy4(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }

        }

        private void cmbActivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy3(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

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
                this.ActivoFijoTableAdapter.FillBy3(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet18.ActivoFijo, dtFecha1.Text, dtFecha2.Text);

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
                this.ActivoFijoTableAdapter.FillBy3(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet18.ActivoFijo, dtFecha1.Text, dtFecha2.Text);

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
                this.ActivoFijoTableAdapter.FillBy3(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == false)
            {
                this.ActivoFijoTableAdapter.FillBy1(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text != "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy2(this.ControlCondominiosDataSet18.ActivoFijo, cmbProveedor.Text, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text != "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy5(this.ControlCondominiosDataSet18.ActivoFijo, cmbActivo.Text, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
            else if (cmbProveedor.Text == "TODOS" && cmbActivo.Text == "TODOS" && cbFechas.Checked == true)
            {
                this.ActivoFijoTableAdapter.FillBy6(this.ControlCondominiosDataSet18.ActivoFijo, dtFecha1.Text, dtFecha2.Text);

                this.reportViewer1.RefreshReport();
            }
        }
    }
}
