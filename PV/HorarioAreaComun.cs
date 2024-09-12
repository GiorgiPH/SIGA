using System;
using System.Drawing;
using System.Windows.Forms;
using PV.Clases.HorariosAreasComunes;

namespace MEDCON
{
    public partial class ParametrosGenerales : Form
    {
        DBHorariosAreasComunes c = new DBHorariosAreasComunes();

        public ParametrosGenerales()
        {
            InitializeComponent();
        }

        private void ParametrosGenerales_Load(object sender, EventArgs e)
        {
            Limpiar();
            c.SeleccionarCondominio(cmbCondominios);
           
        }

        void Limpiar()
        {
            cmbCondominios.Text = null;
            txtCondominio.Clear();
            cmbAreasComunes.Text = null;
            txtAreaComun.Clear();
            txtD1.CustomFormat = " ";
            txtD2.CustomFormat = " ";
            txtD3.CustomFormat = " ";
            txtD4.CustomFormat = " ";
            txtD5.CustomFormat = " ";
            txtD6.CustomFormat = " ";
            txtD7.CustomFormat = " ";
            txtH1.CustomFormat = " ";
            txtH2.CustomFormat = " ";
            txtH3.CustomFormat = " ";
            txtH4.CustomFormat = " ";
            txtH5.CustomFormat = " ";
            txtH6.CustomFormat = " ";
            txtH7.CustomFormat = " ";
            groupBox2.Enabled = false;
        }

        void Limpiar2()
        {
            txtD1.CustomFormat = " ";
            txtD2.CustomFormat = " ";
            txtD3.CustomFormat = " ";
            txtD4.CustomFormat = " ";
            txtD5.CustomFormat = " ";
            txtD6.CustomFormat = " ";
            txtD7.CustomFormat = " ";
            txtH1.CustomFormat = " ";
            txtH2.CustomFormat = " ";
            txtH3.CustomFormat = " ";
            txtH4.CustomFormat = " ";
            txtH5.CustomFormat = " ";
            txtH6.CustomFormat = " ";
            txtH7.CustomFormat = " ";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtD1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtD1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD2_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH2_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD3_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH3_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD4_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH4_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD5_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH5_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD6_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH6_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtD7_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtH7_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

       

        private void ParametrosGenerales_Leave(object sender, EventArgs e)
        {

        }

        private void ParametrosGenerales_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dtpHoraInicial_Enter(object sender, EventArgs e)
        {
            txtD1.CustomFormat = "HH:mm";
            txtH1.CustomFormat = "HH:mm";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtD1.CustomFormat = " ";
            txtH1.CustomFormat = " ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtD2.CustomFormat = " ";
            txtH2.CustomFormat = " ";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtD3.CustomFormat = " ";
            txtH3.CustomFormat = " ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtD4.CustomFormat = " ";
            txtH4.CustomFormat = " ";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtD5.CustomFormat = " ";
            txtH5.CustomFormat = " ";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtD6.CustomFormat = " ";
            txtH6.CustomFormat = " ";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtD7.CustomFormat = " ";
            txtH7.CustomFormat = " ";
        }

        private void txtD2_Enter(object sender, EventArgs e)
        {
            txtD2.CustomFormat = "HH:mm";
            txtH2.CustomFormat = "HH:mm";
        }

        private void txtD3_Enter(object sender, EventArgs e)
        {
            txtD3.CustomFormat = "HH:mm";
            txtH3.CustomFormat = "HH:mm";
        }

        private void txtD4_Enter(object sender, EventArgs e)
        {
            txtD4.CustomFormat = "HH:mm";
            txtH4.CustomFormat = "HH:mm";
        }

        private void txtD5_Enter(object sender, EventArgs e)
        {
            txtD5.CustomFormat = "HH:mm";
            txtH5.CustomFormat = "HH:mm";
        }

        private void txtD6_Enter(object sender, EventArgs e)
        {
            txtD6.CustomFormat = "HH:mm";
            txtH6.CustomFormat = "HH:mm";
        }

        private void txtD7_Enter(object sender, EventArgs e)
        {
            txtD7.CustomFormat = "HH:mm";
            txtH7.CustomFormat = "HH:mm";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (cmbCondominios.Text == string.Empty)
            {
                MessageBox.Show("Ingresa el Condominio para continuar");
                cmbCondominios.Focus();
                return;
            }
            else if (cmbAreasComunes.Text == string.Empty)
            {
                MessageBox.Show("Ingresa el Area Comun para continuar");
                cmbAreasComunes.Focus();
                return;
            }
            else if (txtD1.Text != string.Empty && txtH1.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD1.Focus();
                return;
            }
            else if (txtD1.Text != string.Empty && txtH1.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD1.Focus();
                return;
            }
            else if (txtD1.Text == string.Empty && txtH1.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD1.Focus();
                return;
            }
            else if (txtD2.Text != string.Empty && txtH2.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD2.Focus();
                return;
            }
            else if (txtD2.Text == string.Empty && txtH2.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD2.Focus();
                return;
            }
            else if (txtD3.Text != string.Empty && txtH3.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD3.Focus();
                return;
            }
            else if (txtD3.Text == string.Empty && txtH3.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD3.Focus();
                return;
            }
            else if (txtD4.Text != string.Empty && txtH4.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD4.Focus();
                return;
            }
            else if (txtD4.Text == string.Empty && txtH4.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD4.Focus();
                return;
            }
            else if (txtD5.Text != string.Empty && txtH5.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD5.Focus();
                return;
            }
            else if (txtD5.Text == string.Empty && txtH5.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD5.Focus();
                return;
            }
            else if (txtD6.Text != string.Empty && txtH6.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD6.Focus();
                return;
            }
            else if (txtD6.Text == string.Empty && txtH6.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD6.Focus();
                return;
            }
            else if (txtD7.Text != string.Empty && txtH7.Text == string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD7.Focus();
                return;
            }
            else if (txtD7.Text == string.Empty && txtH7.Text != string.Empty)
            {
                MessageBox.Show("Ingresa hora inicio y termino");
                txtD7.Focus();
                return;
            }
            else
            {
               MessageBox.Show(c.RegistroHorario(txtCondominio.Text, txtAreaComun.Text, txtD1.Text.Trim(), txtD2.Text.Trim(), txtD3.Text.Trim(), txtD4.Text.Trim(), txtD5.Text.Trim(), txtD6.Text.Trim(), txtD7.Text.Trim(), txtH1.Text.Trim(), txtH2.Text.Trim(), txtH3.Text.Trim(), txtH4.Text.Trim(), txtH5.Text.Trim(), txtH6.Text.Trim(), txtH7.Text.Trim()));
                Limpiar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void cmbCondominios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominios.Text);
                txtCondominio.Text = valores[0];
                Limpiar2();

                c.SeleccionarAreaComun(cmbAreasComunes, txtCondominio.Text);
            }
        }

        private void cmbAreasComunes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAreasComunes.Text != string.Empty)
            {
                string[] valores = c.InformacionAreaComun(cmbAreasComunes.Text);
                txtAreaComun.Text = valores[0];
                Limpiar2();

                c.ConsultaGeneral( txtCondominio.Text,  txtAreaComun.Text,  txtD1,  txtD2,  txtD3,  txtD4,  txtD5,  txtD6,  txtD7,  txtH1,  txtH2,  txtH3,  txtH4,  txtH5,  txtH6, txtH7);
                groupBox2.Enabled = true;
            }
        }
    }
}
