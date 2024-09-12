using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;
namespace PV
{
    public partial class ReporteComprasFiltro : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteComprasFiltro()
        {
            InitializeComponent();
        }

        private void ReporteComprasFiltro_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;
            provedor = cmbPropietario1.Text;
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedor = cmbPropietario1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteCompras reporteDiarioOrdenesCompra = new ReporteCompras(provedor, fecha, fecha1, fecha2);
            reporteDiarioOrdenesCompra.ShowDialog();
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {

            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                label2.Text = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                label2.Text = "No";
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
