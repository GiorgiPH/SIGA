using PuntoVentas;
using PuntoVentas.Clases.FormasPago;
using PV.Clases.Anticipo;
using PV.Clases.Clientes;
using PV.Clases.CuentasBancarias;
using System;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteAnticiposFiltro : Form
    {
        DBAnticipo c = new DBAnticipo();
        DBClientes cl = new DBClientes();
        DBCUentaBancaria cu = new DBCUentaBancaria();
        DBFormasPagos f = new DBFormasPagos();
        string propietario1 = string.Empty;
        string propietario2 = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteAnticiposFiltro()
        {
            InitializeComponent();
            dtFecha1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtFecha2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        }
        private void CargarClientes(string nombre)
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
        private void ReporteAnticiposFiltro_Load(object sender, EventArgs e)
        {
            cl.SeleccionarPropietarios(cmbpropietario1);
            cl.SeleccionarPropietarios(cmbpropietario2);
            cmbpropietario2.SelectedIndex = 0;
            cmbpropietario1.SelectedIndex = 0;

            CargarClientes(txtFiltroNombre.Text);
            propietario1 = cmbpropietario1.Text;
            propietario2 = cmbpropietario2.Text;
            CargarCuentasBancariasEnComboBox(cmbCuenta);
            f.SeleccionarFormaPAgo2(cmbFormaPago);
            cmbFormaPago.SelectedIndex = 0;
            cmbCuenta.SelectedIndex = 0;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void Propi1_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbpropietario2.Text = cmbpropietario1.Text;
            propietario1 = cmbpropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }



        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            if (dtFecha2.Value < dtFecha1.Value)
            {
                dtFecha2.Text = dtFecha1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cuenta = cmbCuenta.SelectedValue?.ToString();
            ReporteAnticipos reporteSaldoPropietario = new ReporteAnticipos(propietario1, propietario2, fecha, fecha1, fecha2, cmbFormaPago.Text, Cuenta);
            reporteSaldoPropietario.ShowDialog();
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
                cmbpropietario1.Text = clave + " - " + propietario;
                cmbpropietario2.Text = clave + " - " + propietario;
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbpropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
            cmbCuenta.SelectedIndex = 0;
            cmbFormaPago.SelectedIndex = 0;
        }

        private void cmbpropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* if (cmbpropietario1.SelectedIndex == 0)
             {
                 cmbpropietario2.SelectedIndex = 0;
             }
             else if (cmbpropietario1.SelectedIndex > cmbpropietario2.SelectedIndex)
             {
                 cmbpropietario2.SelectedIndex = cmbpropietario1.SelectedIndex;
             }*/
            propietario1 = cmbpropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }
        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (cmbpropietario2.SelectedIndex == 0)
            {
                cmbpropietario1.SelectedIndex = 0;
            }
            else if (cmbpropietario2.SelectedIndex < cmbpropietario1.SelectedIndex)
            {
                cmbpropietario1.SelectedIndex = cmbpropietario2.SelectedIndex;
            }*/
            propietario1 = cmbpropietario1.Text;
            propietario2 = cmbpropietario2.Text;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
          

        }
    }
}
