using Condominios;
using Condominios.Clases.CentroCostos;
using PuntoVentas.Clases.Login;
using PV;
using PV.Clases.CuentasBancarias;
using System;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class CuentasBancarias : Form
    {
        DBCUentaBancaria c = new DBCUentaBancaria();
        DBCentroCostos centroCostos = new DBCentroCostos();
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
            LlenarComboCentroCostos();
        }
        private void LlenarComboCentroCostos()
        {
            try
            {
                DataTable menus = centroCostos.ConsultarTodos();
                // Crear fila "TODOS"


                // Configurar estilo y autocompletado
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbCentroCostos.DataSource = menus;
                cmbCentroCostos.DisplayMember = "Nombre"; // Campo visible
                cmbCentroCostos.ValueMember = "Clave";   // Campo interno
                cmbCentroCostos.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            cmbCentroCostos.SelectedIndex = -1;
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
            else if (cmbCentroCostos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el Centro de Costos para continuar.");
            }
            else
            {
                MessageBox.Show(c.RegistroCuenta(
                    txtClaveDivisa.Text,
                    txtNombre.Text,
                    cmbEstatus.Text,
                    dtpFechaCambio.Text,
                    txtNotas.Text,
                    txtcuentaSat.Text,
                    txtCuentaContable.Text,
                    cmbCentroCostos.SelectedValue?.ToString()));
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
                c.ConsultaCuentaSeleccionado(ClaveDivisa, txtNombre, cmbEstatus, dtpFechaCambio, txtNotas, txtcuentaSat, txtCuentaContable, cmbCentroCostos);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}