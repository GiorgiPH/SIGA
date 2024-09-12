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
    public partial class ReporteEstadoCuentaFormulario : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public ReporteEstadoCuentaFormulario()
        {
            InitializeComponent();
        }

        private void ReporteEstadoCuentaFormulario_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietariosNombre(cmbPropietario);
            cmbPagos.SelectedIndex = 0;
            cmbSaldos.SelectedIndex = 0;
            cmbAnticipos.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
                dtFecha2.ResetText();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbPropietario.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el propietario para continuar");
            }
            else if (cmbPagos.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la opcion para pagos");
            }
            else if (cmbSaldos.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la opcion para saldados");
            }
            else
            {
                if (cmbPagos.Text=="Si")
                {
                    string fecha = string.Empty;

                    if (cbFechas.Checked == true)
                    {
                        fecha = "Si";
                    }
                    int firma = 0;

                    if (cbFirma.Checked==true)
                    {
                        firma = 1;
                    }

                    string Anticipo = string.Empty;

                    if (cmbAnticipos.Text!= string.Empty)
                    {
                        Anticipo = cmbAnticipos.Text;
                    }
                    else
                    {
                        Anticipo = "No";
                    }

                    string Propiedad = txtPropiedad.Text;

                    ReporteEstadoCuenta reporteEstadoCuenta = new ReporteEstadoCuenta(txtPropietario.Text, cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo, Propiedad);
                    reporteEstadoCuenta.ShowDialog();
                }
                else
                {
                    string fecha = string.Empty;

                    if (cbFechas.Checked == true)
                    {
                        fecha = "Si";
                    }
                    int firma = 0;

                    if (cbFirma.Checked == true)
                    {
                        firma = 1;
                    }

                    string Anticipo = string.Empty;

                    if (cmbAnticipos.Text != string.Empty)
                    {
                        Anticipo = cmbAnticipos.Text;
                    }
                    else
                    {
                        Anticipo = "No";
                    }

                    string Propiedad = txtPropiedad.Text;

                    ReporteEstadoCuenta2 reporteEstadoCuenta = new ReporteEstadoCuenta2(txtPropietario.Text, cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo, Propiedad);
                    reporteEstadoCuenta.ShowDialog();
                }
                
            }
            
        }

        private void cmbPropietario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario.Text != string.Empty)
            {
                string[] valores = c.InformacionPropietarioNombre(cmbPropietario.Text);
                txtPropietario.Text = valores[0];

                c.SeleccionarPropiedadNombre(cmbPropiedad, txtPropietario.Text);

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

        private void cmbPropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPropiedad.Clear();
            if (cmbPropiedad.Text != string.Empty && cmbPropiedad.Text!="TODOS")
            {
                string[] valores = c.InformacionPropiedadNombre(cmbPropiedad.Text);
                txtPropiedad.Text = valores[0];
            }

        }
    }
}
