using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.Condominios;

namespace PV
{
    public partial class AgregarEstructura : Form
    {
        string clave = string.Empty;
        string clavedep = string.Empty;
        DBCondominios c = new DBCondominios();

        public AgregarEstructura(string Clave, string ClaveDep)
        {
            InitializeComponent();
            clave = Clave;
            clavedep = ClaveDep;
        }

        private void AgregarEstructura_Load(object sender, EventArgs e)
        {
            if (clavedep!= string.Empty)
            {
                c.Consultaestructura(clavedep, txtNombre, txtDescripcion, txtIndiviso, txtCuota, dtDel, dtAl);
                txtClave.Text = clavedep;
            }
            else
            {
                c.ClaveEstructura(clave, txtClave);
                txtClave.Text = clave + '-' + txtClave.Text;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text== string.Empty)
            {
                MessageBox.Show("Registre el nombre para continuar");
            }
            else if (txtCuota.Text == "0.00")
            {
                MessageBox.Show("Registre la cuota de mantenimiento para continuar");
            }
            else
            {
               MessageBox.Show( c.agregarEstructura( txtClave.Text,  txtNombre.Text,  clave,  txtDescripcion.Text, Convert.ToDecimal( txtIndiviso.Text), Convert.ToDecimal( txtCuota.Text), Convert.ToDateTime( dtDel.Text).ToString("yyyy/MM/dd"), Convert.ToDateTime(dtAl.Text).ToString("yyyy/MM/dd")));
                if (clavedep == string.Empty)
                {
                    Limpiar();
                }
                else
                {
                    this.Close();
                }
               
            }
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtIndiviso.Clear();
            txtCuota.Clear();
            dtDel.ResetText();
            dtAl.ResetText();
            c.ClaveEstructura(clave, txtClave);
            txtClave.Text = clave + '-' + txtClave.Text;
            txtNombre.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIndiviso_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtCuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtCuota_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtCuota);
        }

        private void Moneda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtIndiviso_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtIndiviso);
        }

        private void dtAl_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime( dtAl.Text) < Convert.ToDateTime( dtDel.Text))
            {
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial");
                dtAl.Text = dtDel.Text;
            }
        }

        private void dtDel_ValueChanged(object sender, EventArgs e)
        {
            dtAl.Text = dtDel.Text;
        }
    }
}
