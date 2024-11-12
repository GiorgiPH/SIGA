using System;
using System.Globalization;
using System.Windows.Forms;
using PV.Clases.Divisas;
using PuntoVentas.Clases.Login;
using PV;

namespace PuntoVentas
{
    public partial class CatalogoDivisa : Form
    {
        DBDivisas c = new DBDivisas();
        DBLogin s = new DBLogin();

        public CatalogoDivisa()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nueva Divisa");
            T.SetToolTip(button8, "Consultar Divisa");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button9, "Imprimir");
            T.SetToolTip(button3, "Historial");

            T.SetToolTip(guna2CircleButton1, "Menú");
        }

        void GenerarNoDivisa()
        {
            DBDivisas.Folio = 0;
            c.ClaveDivisaSiguiente();
            if (DBDivisas.Folio == 0)
            {
                DBDivisas.Folio = 1;
                txtClaveDivisa.Text = Convert.ToString(DBDivisas.Folio);
               
            }
            else
            {
                DBDivisas.Folio = DBDivisas.Folio + 1;
                txtClaveDivisa.Text = Convert.ToString(DBDivisas.Folio);
               
            }
        }

        void Limpiar ()
        {
            txtClaveDivisa.Clear();
            txtNombre.Clear();
            cmbEstatus.ResetText();
            txtTipoCambio.Clear();
            dtpFechaCambio.ResetText();
            txtNotas.Clear();
            guna2Panel2.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void CatalogoDivisa_Load(object sender, EventArgs e)
        {
            //GenerarNoDivisa();
            c.CargarDivisa(dataGridView1);
            cmbEstatus.Text = "Activo";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text == string.Empty)
            {
                MessageBox.Show("Genere un registro nuevo.");
            }
            else if (txtNombre.Text== string.Empty)
            {
                MessageBox.Show("Registre el nombre de la divisa para continuar.");
            }
            else if (cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Registre el estatus de la divisa para continuar.");
            }
            else if (txtTipoCambio.Text== string.Empty)
            {
                MessageBox.Show("Registre el tipo de cambio de la divisa para continuar.");
            }
            else
            {
                MessageBox.Show(c.RegistroDivisa(txtClaveDivisa.Text, txtNombre.Text, cmbEstatus.Text, txtTipoCambio.Text, dtpFechaCambio.Text, txtNotas.Text));
                c.RegistroDivisaHistorial(txtClaveDivisa.Text, txtNombre.Text, cmbEstatus.Text, txtTipoCambio.Text, dtpFechaCambio.Text, txtNotas.Text, DBLogin.usuario);
                Limpiar();
                //GenerarNoDivisa();
                c.CargarDivisa(dataGridView1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoDivisa();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClaveDivisa = dataGridView1.Rows[e.RowIndex].Cells["ClaveDivisa"].Value.ToString();
                c.ConsultaDivisaSeleccionado(ClaveDivisa, txtNombre, cmbEstatus, txtTipoCambio, dtpFechaCambio, txtNotas);
                txtClaveDivisa.Text = ClaveDivisa;
                PanelUsuario.Visible = false;
                guna2Panel2.Enabled = true;
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

        private void txtTipoCambio_TextChanged(object sender, EventArgs e)
        {
            //Moneda(ref txtTipoCambio);

            dtpFechaCambio.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void CatalogoDivisa_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteDivisas reporteDivisas = new ReporteDivisas();
            reporteDivisas.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text != string.Empty)
            {
                Limpiar();
                GenerarNoDivisa();
                guna2Panel2.Enabled = true;
                dtpFechaCambio.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            else
            {
                Limpiar();
                GenerarNoDivisa();
                guna2Panel2.Enabled = true;
                dtpFechaCambio.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReporteDivisaHistorial reporteDivisaHistorial = new ReporteDivisaHistorial();
            reporteDivisaHistorial.ShowDialog();
        }

        private void txtTipoCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtTipoCambio_Leave(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;
            if (txtTipoCambio.Text!= string.Empty)
            {
                decimal decim = Convert.ToDecimal(txtTipoCambio.Text);
                txtTipoCambio.Text = decim.ToString("N4", formato);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text==string.Empty && txtNombre.Text==string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveDivisa.Text));

                        Limpiar();
                        c.CargarDivisa(dataGridView1);
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
    }
}
