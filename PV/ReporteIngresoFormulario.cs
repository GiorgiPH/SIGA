using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;

namespace PV
{
    public partial class ReporteIngresoFormulario : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;

        public ReporteIngresoFormulario()
        {
            InitializeComponent();
        }

        private void ReporteIngresoFormulario_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietarios(cmbPropietario1);
            c.SeleccionarPropietarios(cmbpropietario2);
            c.SeleccionarConceptoDocumento3(cmbDocumento);
            c.SeleccionarFormaPAgo2(cmbFormaPago);
            c.SeleccionarCondominio(cmbSeccion);

            cmbPropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
            cmbDocumento.SelectedIndex = 0;
            cmbFormaPago.SelectedIndex = 0;
            cmbSeccion.SelectedIndex = 0;
            cbAnticipo.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Propietario1 = string.Empty;
            string Propietario2 = string.Empty;
            string Documento = string.Empty;
            string FormaPago = string.Empty;
            string Seccion = string.Empty;
            string FechaPago = string.Empty;
            string FechaPago1 = string.Empty;
            string FechaPago2 = string.Empty;
            string FechaRecibo = string.Empty;
            string FechaRecibo1 = string.Empty;
            string FechaRecibo2 = string.Empty;

            Propietario1 = cmbPropietario1.Text;
            Propietario2 = cmbpropietario2.Text;
            Documento = cmbDocumento.Text;
            FormaPago = cmbFormaPago.Text;
            Seccion = cmbSeccion.Text;

            if (cbFechas.Checked == true)
            {
                FechaPago = "Si";
                FechaPago1 = dtFecha1.Text;
                FechaPago2 = dtFecha2.Text;
            }
            else
            {
               
                dtFecha1.ResetText();
                dtFecha2.ResetText();
                FechaPago1 = dtFecha1.Text;
                FechaPago2 = dtFecha2.Text;
            }

            if (cbFechasRecibo.Checked == true)
            {
                FechaRecibo = "Si";
                FechaRecibo1 = dtFechaRecibo1.Text;
                FechaRecibo2 = dtFechaRecibo2.Text;
            }
            else
            {
                
                dtFechaRecibo1.ResetText();
                dtFechaRecibo2.ResetText();
                FechaRecibo1 = dtFechaRecibo1.Text;
                FechaRecibo2 = dtFechaRecibo2.Text;
            }

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }

            ReporteIngresos reporteIngresos = new ReporteIngresos(Propi1, Propi2, Documento, FormaPago, FechaPago, FechaPago1, FechaPago2, FechaRecibo, FechaRecibo1, FechaRecibo2, cbAnticipo.Text, Seccion);
            reporteIngresos.ShowDialog();

        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked==true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
            }
            else
            {
                dtFecha1.ResetText();
                dtFecha2.ResetText();
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
            }
        }

        private void cbFechasRecibo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechasRecibo.Checked == true)
            {
                dtFechaRecibo1.Enabled = true;
                dtFechaRecibo2.Enabled = true;
            }
            else
            {
                dtFechaRecibo1.ResetText();
                dtFechaRecibo2.ResetText();
                dtFechaRecibo1.Enabled = false;
                dtFechaRecibo2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbPropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmbDocumento.Text = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmbFormaPago.Text = null;
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
        }

        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            if (dtFecha2.Value < dtFecha1.Value)
            {
                dtFecha2.Text = dtFecha1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }

        private void dtFechaRecibo2_Leave(object sender, EventArgs e)
        {
            if (dtFechaRecibo2.Value < dtFechaRecibo1.Value)
            {
                dtFechaRecibo2.Text = dtFechaRecibo1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }
    }
}
