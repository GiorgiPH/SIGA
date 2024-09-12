using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Presupuesto;

namespace PV
{
    public partial class CerrarPresupuesto : Form
    {
        DBPresupuesto c = new DBPresupuesto();

        public CerrarPresupuesto()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionConceptoPresupuesto(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEjercicio.Text != string.Empty && cmbTipo.Text != string.Empty && cmbCondominio.Text!= string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominio.Text);
                txtCondominio.Text = valores[0];
                c.SeleccionarConceptoCerrar(cmbConcepto, cmbTipo.Text, txtEjercicio.Text, txtCondominio.Text);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CerrarPresupuesto_Load(object sender, EventArgs e)
        {
            c.SeleccionarEjercicio(txtEjercicio);
            c.ConsultaConcepto(txtConceptoM);
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEjercicio.Text!= string.Empty && cmbTipo.Text!=string.Empty)
            {
                c.SeleccionarCondominiCerrar(cmbCondominio, cmbTipo.Text, txtEjercicio.Text);
            }
        }

        private void txtEjercicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEjercicio.Text != string.Empty && cmbTipo.Text != string.Empty)
            {
                c.SeleccionarCondominiCerrar(cmbCondominio, cmbTipo.Text, txtEjercicio.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtEjercicio.Text==string.Empty)
            {
                MessageBox.Show("Seleccione el ejercicio para continuar");

            }
            else if (cmbTipo.Text==string.Empty)
            {
                MessageBox.Show("Seleccione el tipo para continuar");
            }
            else if (cmbCondominio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el condominio para continuar");
            }
            else if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el concepto para continuar");
            }
            else
            {
                c.ConsultaPresupuesto2(txtEjercicio.Text, txtConcepto.Text, cmbTipo.Text, txtCondominio.Text, txtImporteE);

                if (cmbTipo.Text == "Ingreso")
                {
                    c.ConsultaConcepto(txtConceptoM);
                    //if (txtConceptoM.Text == txtConcepto.Text)
                    //{
                        MessageBox.Show( c.RegistroFormaPago2(txtCondominio.Text, Convert.ToDecimal(txtImporteE.Text)));
                        limpiar();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Presupuesto Cerrado");
                    //    limpiar();
                    //}
                }
                else if (cmbTipo.Text == "Egreso")
                {
                    c.ConsultaConceptoE(txtConceptoE);
                    //if (txtConceptoE.Text == txtConcepto.Text)
                    //{
                        MessageBox.Show( c.RegistroFormaPago3(txtCondominio.Text, Convert.ToDecimal(txtImporteE.Text)));
                        limpiar();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Presupuesto Cerrado");
                    //    limpiar();
                    //}
                }
            }
           
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

        private void txtImporteE_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporteE);
        }

        void limpiar ()
        {
            txtConcepto.Clear();
            txtEjercicio.Text = null;
            cmbConcepto.Text = null;
            cmbCondominio.Text = null;
            cmbTipo.Text = null;
            txtCondominio.Clear();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
