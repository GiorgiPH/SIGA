using Condominios.Clases.GenerarRecibo;
using PV.Clases.Clientes;
using Guna.UI2.WinForms;
using PuntoVentas;
using System;
using System.Windows.Forms;
using System.Data;
using PV.Clases.CuentasBancarias;

namespace PV
{
    public partial class ReporteIngresoFormulario : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        DBClientes cl = new DBClientes();
        DBCUentaBancaria cu = new DBCUentaBancaria();
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;

        public ReporteIngresoFormulario()
        {
            InitializeComponent();
        }
        private void CargarClientes( string nombre)
        {
            try
            {
                var pagos = cl.CargarClientes(nombre);

                dataGridView1.Rows.Clear();

                foreach (DataRow pago in pagos.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = pago["IdCliente"];
                    dataGridView1.Rows[n].Cells[1].Value = pago["RazonSocial"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }
        public void CargarCuentasBancariasEnComboBox(ComboBox cb)
        {
            var cuentas = cu.ObtenerCuentasBancarias();
            var row = cuentas.NewRow();
    row["Clave"] = 0;
    row["Nombre"] = "TODOS";
            cuentas.Rows.InsertAt(row, 0);
            cb.DataSource = cuentas;
            cb.DisplayMember = "Nombre"; // Lo que se mostrará en el ComboBox
            cb.ValueMember = "Clave";    // Lo que se usará como valor seleccionado
        }

        private void ReporteIngresoFormulario_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietarios(cmbpropietario1);
            c.SeleccionarPropietarios(cmbpropietario2);
            c.SeleccionarConceptoDocumentoRemision(cmbDocumento);
            CargarCuentasBancariasEnComboBox(cmbCuenta);
            c.SeleccionarFormaPAgo2(cmbFormaPago);
            CargarClientes(txtFiltroNombre.Text);



            cmbpropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
            cmbDocumento.SelectedIndex = 0;
            cmbFormaPago.SelectedIndex = 0;
            cbAnticipo.SelectedIndex = 0;
            cmbCuenta.SelectedIndex = 0;
            cbAnticipo.SelectedIndex = 1;

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

            Propietario1 = cmbpropietario1.Text;
            Propietario2 = cmbpropietario2.Text;
            Documento = cmbDocumento.Text;
            FormaPago = cmbFormaPago.Text;
            string Cuenta = cmbCuenta.SelectedValue?.ToString();

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

            //if (cmbpropietario1.SelectedIndex == 0)
            //{
            //    cmbpropietario1.SelectedIndex = 0;
            //    cmbpropietario2.SelectedIndex = 0;
            //}

            //if (cmbpropietario1.Text != "TODOS")
            //{
            //    pro1 = cmbpropietario1.Text.IndexOf(" -");
            //    Propi1 = cmbpropietario1.Text.Substring(0, pro1);
            //    pro2 = cmbpropietario2.Text.IndexOf(" -");
            //    Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            //}

            ReporteIngresos reporteIngresos = new ReporteIngresos(txtClavePaciente.Text, Documento, FormaPago, FechaPago, FechaPago1, FechaPago2, FechaRecibo, FechaRecibo1, FechaRecibo2, cbAnticipo.Text, Seccion, Cuenta);
            reporteIngresos.ShowDialog();

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
            cmbpropietario1.SelectedIndex = 0;
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

        private void Propi1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpropietario2.Text = cmbpropietario1.Text;

            if (cmbpropietario1.Text != "TODOS")
            {
                pro1 = cmbpropietario1.Text.IndexOf(" -");
                Propi1 = cmbpropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
            }
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpropietario2.Text == "TODOS")
            {
                cmbpropietario1.Text = "TODOS";
            }
            if (cmbpropietario1.Text != "TODOS")
            {
                pro1 = cmbpropietario1.Text.IndexOf(" -");
                Propi1 = cmbpropietario1.Text.Substring(0, pro1);
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

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {

        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            CargarClientes(txtFiltroNombre.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                string propietario = dataGridView1.Rows[e.RowIndex].Cells["Propietario"].Value.ToString();
                txtClavePaciente.Text = clave + " - " + propietario;
                cmbpropietario1.Text = clave + " - " + propietario;
                cmbpropietario2.Text = clave + " - " + propietario;
            }
            else
            {
                return;
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
   
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Guna2ComboBox)
                {
                    if (((Guna2ComboBox)c).Name == "cmbPropiedad")
                    {
                        ((Guna2ComboBox)c).SelectedIndex = -1;
                    }
                    else
                    {
                        ((Guna2ComboBox)c).SelectedIndex = 0;

                    }
                }
                if (c is Guna2TextBox)
                {
                    ((Guna2TextBox)c).Text = string.Empty;
                }
                if (c is Guna2DateTimePicker)
                {
                    ((Guna2DateTimePicker)c).ResetText();
                    ((Guna2DateTimePicker)c).Enabled = false;
                }
                cbFechas.Checked = false;
                cbFechasRecibo.Checked = false;

            }
            cmbpropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
            pro1 = 0;
            Propi1 = string.Empty;
            pro2 = 0;
            Propi2 = string.Empty;
        }

        private void cbAnticipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
