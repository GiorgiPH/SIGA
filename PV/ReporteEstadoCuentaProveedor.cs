using System;
using System.Data;
using System.Windows.Forms;
using PV.Clases.Proveedores;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteEstadoCuentaProveedor : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        DBProveedores p = new DBProveedores();
        string provedor = string.Empty;

        public ReporteEstadoCuentaProveedor()
        {
            InitializeComponent();
        }

        private void ReporteEstadoCuentaProveedor_Load(object sender, EventArgs e)
        {
            LlenarComboProveedores();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LlenarComboProveedores()
        {
            try
            {
                DataTable menus = p.ConsultarProveedores();
                // Crear fila "TODOS"
                DataRow filaTodos = menus.NewRow();
                filaTodos["IdProveedor"] = "0"; // Asegúrate de que la columna "Clave" exista
                filaTodos["RazonSocial"] = "TODOS"; // Asegúrate de que la columna "Clave" exista

                menus.Rows.InsertAt(filaTodos, 0); // Insertar al principio
                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbPropietario1.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbPropietario1.DataSource = menus;
                cmbPropietario1.DisplayMember = "RazonSocial"; // Campo visible
                cmbPropietario1.ValueMember = "IdProveedor";   // Campo interno
                cmbPropietario1.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    EstadoCuentaProveedor reporteDiarioOrdenesCompra = new EstadoCuentaProveedor(cmbPropietario1?.SelectedValue?.ToString(), cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo);
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

                    EstadoCuentaProveedor reporteDiarioOrdenesCompra = new EstadoCuentaProveedor(cmbPropietario1?.SelectedValue?.ToString(), cmbPagos.Text, cmbSaldos.Text, fecha, "1990/01/01", dtFecha2.Text, firma, Anticipo);
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
