using System;
using System.Windows.Forms;
using Condominios.Clases.ConceptosGlobales;
using PuntoVentas;
using PV;
using PuntoVentas.Clases.Login;
using Guna.UI2.WinForms;

namespace Condominios
{
    public partial class ConceptosGlobales : Form
    {
        DBConceptosGlobales c = new DBConceptosGlobales();

        public ConceptosGlobales()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nuevo Concepto");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Concepto");
            T.SetToolTip(button9, "Imprimir");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Registre la clave del concepto para continuar");
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre el nombre del concepto para continuar");
            }
            else if (cmbClase.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase del concepto para continuar");
            }
            else if (cmbTipo.Text == string.Empty)
            {
                MessageBox.Show("Registre el tipo del concepto para continuar");
            }
            else if (cmbRelativo.Text == string.Empty && cmbTipo.Text=="Porcentaje")
            {
                MessageBox.Show("Registre el relativo del concepto para continuar");
            }
            else if (txtCuenta.Text== string.Empty)
            {
                MessageBox.Show("Registre la cuenta del concepto para continuar");
            }
            else if (txtImporte.Visible==true && txtImporte.Text == string.Empty)
            {
                MessageBox.Show("Registre el importe del concepto para continuar");
            }
            else if (txtPorcentaje.Visible == true && txtPorcentaje.Text == string.Empty)
            {
                MessageBox.Show("Registre el porcentaje del concepto para continuar");
            }
            else
            {
                if (txtPorcentaje.Visible == true)
                {
                    MessageBox.Show(c.RegistrConcepto(txtClave.Text, txtNombre.Text, cmbClase.Text, cmbTipo.Text, cmbRelativo.Text, txtCuenta.Text, Convert.ToDecimal(txtPorcentaje.Text), Convert.ToInt16(tgIva.Checked)));
                }
                else
                {
                    MessageBox.Show(c.RegistrConcepto(txtClave.Text, txtNombre.Text, cmbClase.Text, cmbTipo.Text, cmbRelativo.Text, txtCuenta.Text, Convert.ToDecimal(txtImporte.Text), Convert.ToInt16(tgIva.Checked)));
                }
                Limpiar();
                c.CargarConceptos(dataGridView1);
            }
        }

        private void ConceptosGlobales_Load(object sender, EventArgs e)
        {
            c.CargarConceptos(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtNombre.Clear();
            cmbClase.Text = null;
            cmbTipo.Text = null;
            cmbRelativo.Text = null;
            txtCuenta.Clear();
            txtImporte.Clear();
            txtPorcentaje.Clear();
            txtClave.Enabled = true;
            txtNombre.Enabled = true;
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
            tgIva.Checked = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                c.ConsultConceptoSeleccionado(Clave, Nombre, cmbClase, cmbTipo, cmbRelativo, txtCuenta, txtImporte, tgIva);
                if (cmbTipo.Text == "Porcentaje")
                {
                    txtPorcentaje.Clear();
                    txtImporte.Clear();
                    c.ConsultConceptoSeleccionado(Clave, Nombre, cmbClase, cmbTipo, cmbRelativo, txtCuenta, txtPorcentaje, tgIva);
                }
                else
                {
                    txtPorcentaje.Clear();
                    txtImporte.Clear();
                    c.ConsultConceptoSeleccionado(Clave, Nombre, cmbClase, cmbTipo, cmbRelativo, txtCuenta, txtImporte, tgIva);
                }
                txtClave.Text = Clave;
                txtNombre.Text = Nombre;
                txtClave.Enabled = false;
                txtNombre.Enabled = false;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal MENU = new MenuPrincipal();
            //MENU.Show();
            this.Hide();
        }

        private void Moneda(ref Guna2TextBox txt)
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

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporte);
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteConceptosGlobales reporteConceptosGlobales = new ReporteConceptosGlobales();
            reporteConceptosGlobales.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtClave.Focus();
            groupBox1.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text == "Porcentaje")
            {
                txtPorcentaje.Visible = true;
                lbPorcentaje.Visible = true;
                cmbRelativo.Enabled = true;
                txtImporte.Clear();
                txtImporte.Visible = false;
            }
            else
            {
                txtPorcentaje.Visible = false;
                lbPorcentaje.Visible = false;
                cmbRelativo.Enabled = false;
                txtPorcentaje.Clear();
                txtImporte.Visible = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show( c.EliminarDivisa(txtClave.Text));
                        c.CargarConceptos(dataGridView1);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
