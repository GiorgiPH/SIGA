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


namespace PV
{
    public partial class ReporteSaldoDetalladoProveedoresFiltro : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        string propietario1 = string.Empty;
        string propietario2 = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;
        string Saldados = string.Empty;
        string divisa = string.Empty;
        public static string TipoCambio = string.Empty;

        public ReporteSaldoDetalladoProveedoresFiltro()
        {
            InitializeComponent();
        }

        private void ReporteSaldoDetalladoProveedoresFiltro_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietariosNombreProveedor(cmbPropietario1);
            c.SeleccionarPropietariosNombreProveedor(cmbpropietario2);
            c.SeleccionarDivisa(cmbDivisa);
            cmbpropietario2.SelectedIndex = 0;
            cmbPropietario1.SelectedIndex = 0;
            cmbSaldos.SelectedIndex = 0;
            cmbDivisa.SelectedIndex = 0;
            propietario1 = cmbPropietario1.Text;
            propietario2 = cmbpropietario2.Text;
            Saldados = cmbSaldos.Text;
            divisa = cmbDivisa.Text;
            if (cmbDivisa.Text != "TODAS")
            {
                c.TipoCambio4(divisa);
            }
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

        private void cmbSaldos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSaldos.Text == "Si")
            {
                Saldados = "Si";
            }
            else
            {
                Saldados = "No";
            }
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
            ReporteSaldoDetalladoProveedor reporteSaldoPropietario = new ReporteSaldoDetalladoProveedor(propietario1, propietario2, fecha, fecha1, fecha2, Saldados, divisa, TipoCambio);
            reporteSaldoPropietario.ShowDialog();
        }

        private void cmbDivisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            divisa = cmbDivisa.Text;
            if (cmbDivisa.Text != "TODAS")
            {
                c.TipoCambio4(divisa);
            }
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                label9.Text = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                label9.Text = "No";
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
