using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class FiltroEntradaSalidaCond : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string condominio = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public FiltroEntradaSalidaCond()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FiltroEntradaSalidaCond_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietario(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;
            provedor = cmbPropietario1.Text;
            c.SeleccionarCondominio(cmbCondominio);
            cmbCondominio.SelectedIndex = 0;
            condominio = cmbCondominio.Text;

        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if(cbFechas.Checked == true)
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
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteEntradaSalidaCond reporteEntradaSalidaCond = new ReporteEntradaSalidaCond(provedor, condominio, fecha, fecha1, fecha2);
            reporteEntradaSalidaCond.ShowDialog();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedor = cmbPropietario1.Text;
        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            condominio = cmbCondominio.Text;
        }
    }
}
