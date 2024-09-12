using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Anticipo;

namespace PV
{
    public partial class ReporeteAnticiposProveedorAplicadosFiltro : Form
    {
        DBAnticipo c = new DBAnticipo();
        string propietario1 = string.Empty;
        string propietario2 = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporeteAnticiposProveedorAplicadosFiltro()
        {
            InitializeComponent();
        }

        private void ReporeteAnticiposProveedorAplicados_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor(cmbPropietario1);
            c.SeleccionarProveedor(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;

            propietario1 = cmbPropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            cmbpropietario2.Text = cmbPropietario1.Text;
            propietario1 = cmbPropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            propietario1 = cmbPropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }

        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            if (dtFecha2.Value < dtFecha1.Value)
            {
                dtFecha2.Text = dtFecha1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteAnticiposProveedoresAplicado reporteSaldoPropietario = new ReporteAnticiposProveedoresAplicado(propietario1, propietario2, fecha, fecha1, fecha2);
            reporteSaldoPropietario.ShowDialog();
        }
    }
}
