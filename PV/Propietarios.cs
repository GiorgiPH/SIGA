using System;
using System.Collections;
using System.Windows.Forms;
using Condominios.Clases.Propietarios;
using PuntoVentas;
using PV;

namespace Condominios
{
    public partial class Propietarios : Form
    {
        DBPropietario c = new DBPropietario();

        public Propietarios()
        {
            InitializeComponent();
        }

        private void Propietarios_Load(object sender, EventArgs e)
        {
            //GenerarNoCliente();
            c.CargarPropietarios(dataGridView1);
            c.SeleccionarCondominio(cmbCondominio);
            cmbEstatus.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        void GenerarNoCliente()
        {
            DBPropietario.Folio = 0;
            c.ClavePropietarioSiguiente();
            if (DBPropietario.Folio == 0)
            {
                DBPropietario.Folio = 1;
                txtClaveCliente.Text = Convert.ToString(DBPropietario.Folio);

            }
            else
            {
                DBPropietario.Folio = DBPropietario.Folio + 1;
                txtClaveCliente.Text = Convert.ToString(DBPropietario.Folio);

            }
        }

        void Limpiar()
        {
            txtClaveCliente.Clear();
            txtRazonSocial.Clear();
            txtRFC.Clear();
            txtCalle.Clear();
            txtNoInterior.Clear();
            txtNoExterior.Clear();
            txtMunicipio.Clear();
            txtEstado.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtCorreo.Clear();
            txtCorreo2.Clear();
            txtCodigoPostal.Clear();
            txtCiudad.Clear();
            txtPais.Clear();
            txtColonia.Clear();
            txtReferencia.Clear();
            cmbEstatus.SelectedIndex = 0;
            cmbCFDI.Text = null;
            cmbFormaPago.Text = null;
            cmbMetodoPago.Text = null;
            cmbTipoCliente.Text = null;
            cmbCondominio.Text = null;
            txtBancoPago.Clear();
            txtDomicilioFiscal.Clear();
            txtRegimenFiscal.Clear();
            txtCondominio.Clear();
            dataGridView2.Rows.Clear();
            tabControl1.Enabled = false;
            groupBox1.Enabled = false;
            tabControl1.Enabled = false;
            rdSi.Checked = false;
            rdNo.Checked = false;
            rdSi2.Checked = false;
            rdNo2.Checked = false;
            PanelUsuario.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveCliente.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtRazonSocial.Text == string.Empty)
            {
                MessageBox.Show("Registre la razon social del propietario para continuar.");
            }
            else if (txtRFC.Text == string.Empty)
            {
                MessageBox.Show("Registre el RFC del propietario para continuar.");
            }
            else if (cmbTipoCliente.Text == string.Empty)
            {
                MessageBox.Show("Registre el tipo de propietario para continuar.");
            }
            else
            {
                ArrayList ListaConcept = new ArrayList();
                ArrayList ListaConcept2 = new ArrayList();
                ArrayList ListaConcept3 = new ArrayList();
                ArrayList ListaConcept4 = new ArrayList();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Seleccionar"].Value == null)
                    {
                        
                    }
                    else if ((bool)row.Cells["Seleccionar"].Value == true)
                    {
                        ListaConcept.Add(row.Cells["Clave"].Value.ToString());
                        ListaConcept2.Add(row.Cells["Departamento"].Value.ToString());
                        if (row.Cells["NumEscritura"].Value == null)
                        {
                            MessageBox.Show("Registre el numero de escritura en los registros seleccionados");
                            return;
                        }
                        else
                        {
                            ListaConcept3.Add(row.Cells["NumEscritura"].Value.ToString());
                        }
                        ListaConcept4.Add(row.Cells["Caracteristicas"].Value.ToString());
                    }
                }

                MessageBox.Show(c.RegistroPropietarios(txtClaveCliente.Text, txtRazonSocial.Text, cmbTipoCliente.Text, txtRFC.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCodigoPostal.Text, txtCiudad.Text, txtPais.Text, txtReferencia.Text, cmbMetodoPago.Text, cmbFormaPago.Text, cmbCFDI.Text, txtBancoPago.Text, txtDomicilioFiscal.Text, txtRegimenFiscal.Text, txtEstado.Text, txtTelefono.Text, txtCelular.Text, txtCorreo.Text, rdSi, rdNo, txtCorreo2.Text, rdSi2, rdNo2, cmbEstatus.Text, ListaConcept, ListaConcept2, ListaConcept3, ListaConcept4));
                Limpiar();
                //GenerarNoCliente();
                c.CargarPropietarios(dataGridView1);
                c.CargarCondominios(txtClaveCliente.Text, dataGridView3);
                c.CargarCondominiosDisponibles(txtCondominio.Text, dataGridView2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCliente();
            c.CargarCondominios(txtClaveCliente.Text, dataGridView3);
            c.CargarCondominiosDisponibles(txtCondominio.Text, dataGridView2);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveCliente"].Value.ToString();
                c.ConsultaPropietariosSeleccionado(Clave, txtRazonSocial, cmbTipoCliente, txtRFC, txtCalle, txtNoExterior, txtNoInterior, txtColonia, txtMunicipio, txtCodigoPostal, txtCiudad, txtPais, txtReferencia, cmbMetodoPago, cmbFormaPago, cmbCFDI, txtBancoPago, txtDomicilioFiscal, txtRegimenFiscal, txtEstado, txtTelefono, txtCelular, txtCorreo, rdSi, rdNo, txtCorreo2, rdSi2, rdNo2, cmbEstatus);
                txtClaveCliente.Text = Clave;
                PanelUsuario.Visible = false;
                c.CargarCondominios(txtClaveCliente.Text, dataGridView3);
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominio.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominio.Text);
                txtCondominio.Text = valores[0];
                c.CargarCondominiosDisponibles(txtCondominio.Text, dataGridView2);
            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView3.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView3.Rows[e.RowIndex].Cells["ClaveDP"].Value != null)
                    {
                        string Clave = dataGridView3.Rows[e.RowIndex].Cells["ClaveDP"].Value.ToString();

                        if (c.ConsultaSaldoEliminarPropiedad(txtClaveCliente.Text, Clave)>0)
                        {
                            MessageBox.Show("No es posible eliminar propiedad con saldo pendiente");
                        }
                        else
                        {
                            MessageBox.Show(c.EliminarDepartamento(Clave));
                            dataGridView3.Rows.Remove(dataGridView3.CurrentRow);
                            c.CargarCondominiosDisponibles(txtCondominio.Text, dataGridView2);
                        }
                       
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReportePropietarios reportePropietarios = new ReportePropietarios();
            reportePropietarios.ShowDialog();
        }

        private void cmbTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoCliente.Text == "Persona Fisica")
            {
                lbNombre.Visible = true;
                lbrazonSocial.Visible = false;
            }
            else
            {
                lbNombre.Visible = false;
                lbrazonSocial.Visible = true;
            }

            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty && cmbEstatus.Text != "Inactivo")
            {
                tabControl1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveCliente.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCliente();
                cmbTipoCliente.Focus();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCliente();
                cmbTipoCliente.Focus();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
        }

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtRazonSocial_TextChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty && cmbEstatus.Text != "Inactivo")
            {
                tabControl1.Enabled = true;
            }
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty && cmbEstatus.Text != "Inactivo")
            {
                tabControl1.Enabled = true;
            }
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty && cmbEstatus.Text !="Inactivo")
            {
                tabControl1.Enabled = true;
            }

            if (cmbEstatus.Text== "Inactivo" && txtClaveCliente.Text != string.Empty)
            {
                if (c.ConsultaPropieddad(txtClaveCliente.Text) > 0)
                {
                    MessageBox.Show("No es posible cambiar a inactivo si cuenta con propiedades");
                    cmbEstatus.SelectedIndex = 0;
                }
                else if (c.ConsultaSaldo(txtClaveCliente.Text) > 0)
                {
                    MessageBox.Show("No es posible cambiar a inactivo si cuenta con saldo pendiente");
                    cmbEstatus.SelectedIndex = 0;
                }
            }
        }
    }
}
