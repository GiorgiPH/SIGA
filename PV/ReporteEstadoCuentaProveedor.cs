using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteEstadoCuentaProveedor : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;

        public ReporteEstadoCuentaProveedor()
        {
            InitializeComponent();
        }

        private void ReporteEstadoCuentaProveedor_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor2(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;
            provedor = cmbPropietario1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedor = cmbPropietario1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == string.Empty)
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
                if (cmbPagos.Text == "Si")
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

                    EstadoCuentaProveedor reporteDiarioOrdenesCompra = new EstadoCuentaProveedor(provedor, cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo);
                    reporteDiarioOrdenesCompra.ShowDialog();
                }
            }
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

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                label9.Text = "Si";
            }
            else
            {
                label9.Text = "No";
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                dtFecha2.ResetText();
            }
        }

        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                cbFirma.Checked = true;
                label2.Text = "Si";
            }
            else
            {
                cbFirma.Checked = false;
                label2.Text = "No";
            }
          
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPagos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == string.Empty)
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
                if (cmbPagos.Text == "Si")
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

                    EstadoCuentaProveedor reporteDiarioOrdenesCompra = new EstadoCuentaProveedor(provedor, cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo);
                    reporteDiarioOrdenesCompra.ShowDialog();
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
             

        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
