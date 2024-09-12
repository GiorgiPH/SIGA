using System;
using System.Windows.Forms;
using PV.Clases.CuentasBancarias;
using PuntoVentas.Clases.Login;
using PV;

namespace PV
{
    public partial class CuentasBancarias : Form
    {
        DBCUentaBancaria c = new DBCUentaBancaria();
        DBLogin s = new DBLogin();

        public CuentasBancarias()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();

            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button4, "Nuevo");
            T.SetToolTip(button8, "Consultar Cuentas Bancarias");
            T.SetToolTip(button9, "Imprimir");
        }

        private void CuentasBancarias_Load(object sender, EventArgs e)
        {
            c.CargarCuentas(dataGridView1);
            cmbEstatus.Text = "Activo";
        }

        void GenerarNoDivisa()
        {
            DBCUentaBancaria.Folio = 0;
            c.ClaveDivisaSiguiente();
            if (DBCUentaBancaria.Folio == 0)
            {
                DBCUentaBancaria.Folio = 1;
                txtClaveDivisa.Text = Convert.ToString(DBCUentaBancaria.Folio);

            }
            else
            {
                DBCUentaBancaria.Folio = DBCUentaBancaria.Folio + 1;
                txtClaveDivisa.Text = Convert.ToString(DBCUentaBancaria.Folio);

            }
        }

        void Limpiar()
        {
            txtClaveDivisa.Clear();
            txtNombre.Clear();
            cmbEstatus.ResetText();
            dtpFechaCambio.ResetText();
            txtNotas.Clear();
            txtcuentaSat.Clear();
            txtCuentaContable.Clear();
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text == string.Empty)
            {
                MessageBox.Show("Genere un registro nuevo.");
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre el nombre de la cuenta bancaria para continuar.");
            }
            else if (cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Registre el estatus de la cuenta bancaria para continuar.");
            }
            else
            {
                MessageBox.Show(c.RegistroCuenta(txtClaveDivisa.Text, txtNombre.Text, cmbEstatus.Text, dtpFechaCambio.Text, txtNotas.Text, txtcuentaSat.Text, txtCuentaContable.Text));
                Limpiar();
                //GenerarNoDivisa();
                c.CargarCuentas(dataGridView1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClaveDivisa = dataGridView1.Rows[e.RowIndex].Cells["ClaveDivisa"].Value.ToString();
                c.ConsultaCuentaSeleccionado(ClaveDivisa, txtNombre, cmbEstatus, dtpFechaCambio, txtNotas, txtcuentaSat, txtCuentaContable);
                txtClaveDivisa.Text = ClaveDivisa;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text != string.Empty)
            {
                Limpiar();
                GenerarNoDivisa();
                groupBox1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoDivisa();
                groupBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text == string.Empty && txtNombre.Text == string.Empty)
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
                        c.CargarCuentas(dataGridView1);
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

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
