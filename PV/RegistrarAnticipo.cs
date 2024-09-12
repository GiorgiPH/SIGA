using System;
using System.Windows.Forms;
using ControlAcademico;
using Condominios;
using PV.Clases.Anticipo;

namespace PV
{
    public partial class RegistrarAnticipo : Form
    {
        DBAnticipo c = new DBAnticipo();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;
        string Opcion = string.Empty;

        public RegistrarAnticipo(string opcion)
        {
            InitializeComponent();
            Opcion = opcion;
        }

        private void RegistrarAnticipo_Load(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCaja.Text = "1";
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
            c.SeleccionarFormaPago(cmbFormaPago);
            

            if (Opcion == "Propietario")
            {
                c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
                c.CargarAnticipo(dataGridView1);
                lbProp.Visible = true;
            }
            else
            {
                c.CargarAnticipoProveedor(dataGridView1);
                lbProv.Visible = true;
                txtConcepto.Text = "ANP - Anticipo Proveedores";
                txtConceptoClave.Text = "ANP";
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                BuscarListaAlumnos2 buscar = new BuscarListaAlumnos2();
                buscar.ShowDialog();
            }
            else
            {
                BuscarListaProveedores buscar = new BuscarListaProveedores();
                buscar.ShowDialog();
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            matricula = string.Empty;
            nombre = string.Empty;
            GenerarRecibo.Matricula = string.Empty;
            registroIngresos.matricula = string.Empty;
            registroIngresos.nombre = string.Empty;
        }

        private void RegistrarAnticipo_Activated(object sender, EventArgs e)
        {
            if (btnBuscar.Enabled==true)
            {
                txtMatricula.Text = matricula;
                txtAlumno.Text = nombre;
            }
        }

        void GenerarNoCategoria()
        {
            DBAnticipo.Folio = 0;
            c.ClaveProductoSiguiente();
            if (DBAnticipo.Folio == 0)
            {
                DBAnticipo.Folio = 1;
                txtFolio.Text = Convert.ToString(DBAnticipo.Folio);

            }
            else
            {
                DBAnticipo.Folio = DBAnticipo.Folio + 1;
                txtFolio.Text = Convert.ToString(DBAnticipo.Folio);

            }
        }

        void GenerarNoCategoria2()
        {
            DBAnticipo.Folio = 0;
            c.ClaveProductoSiguiente2();
            if (DBAnticipo.Folio == 0)
            {
                DBAnticipo.Folio = 1;
                txtFolio.Text = Convert.ToString(DBAnticipo.Folio);

            }
            else
            {
                DBAnticipo.Folio = DBAnticipo.Folio + 1;
                txtFolio.Text = Convert.ToString(DBAnticipo.Folio);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtMatricula.Text == string.Empty)
            {
                MessageBox.Show("Registre al propietario para continuar.");
            }
            else if (cmbCuentaBancaria.Text == string.Empty)
            {
                MessageBox.Show("Registre la cuenta bancaria para continuar.");
            }
            else if (cmbFormaPago.Text == string.Empty)
            {
                MessageBox.Show("Registre la forma de pago para continuar.");
            }
            else if (txtimporte.Text == string.Empty)
            {
                MessageBox.Show("Registre el importe para continuar.");
            }
            else
            {
                if (Opcion == "Propietario")
                {
                    MessageBox.Show(c.RegistroAnticipo(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text, cmbFormaPago.Text, txtConceptoClave.Text, txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text, Convert.ToDecimal(txtimporte.Text)));
                    c.CargarAnticipo(dataGridView1);
                }
                else
                {
                    MessageBox.Show(c.RegistroAnticipoProveedor(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text, cmbFormaPago.Text, txtConceptoClave.Text, txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text, Convert.ToDecimal(txtimporte.Text)));
                    c.CargarAnticipoProveedor(dataGridView1);
                }

                if (MessageBox.Show("¿Imprimir Recibo?", "Registrar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Opcion == "Propietario")
                    {
                        if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                        {
                            ReciboAnticipo reciboAnticipo = new ReciboAnticipo(txtFolio.Text, "0", txtMatricula.Text);
                            reciboAnticipo.ShowDialog();
                        }
                    }
                    else
                    {
                        if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                        {
                            ReciboAnticipoProveedor reciboAnticipo = new ReciboAnticipoProveedor(txtFolio.Text, "0", txtMatricula.Text);
                            reciboAnticipo.ShowDialog();
                        }
                    }
                }
                Limpiar();
            }
        }

        void Limpiar()
        {
            txtFolio.Clear();
            txtMatricula.Clear();
            txtConceptoClave.Clear();
            cmbFormaPago.Text = null;
            txtConcepto.Clear();
            txtReferencia.Clear();
            txtCuenta.Clear();
            cmbCuentaBancaria.Text = null;
            txtNumOperacion.Clear();
            txtimporte.Text = "0.00";
            txtAlumno.Clear();
            panel1.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            btnBuscar.Enabled = true;
            //c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
            if (Opcion == "Propietario")
            {
                c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
                c.CargarAnticipo(dataGridView1);
                lbProp.Visible = true;
            }
            else
            {
                c.CargarAnticipoProveedor(dataGridView1);
                lbProv.Visible = true;
                txtConcepto.Text = "ANP - Anticipo Proveedores";
                txtConceptoClave.Text = "ANP";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                Limpiar();
                if (Opcion == "Propietario")
                {
                    GenerarNoCategoria();
                }
                else
                {
                    GenerarNoCategoria2();
                }
                panel1.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
            }
            else
            {
                Limpiar();
                if (Opcion == "Propietario")
                {
                    GenerarNoCategoria();
                }
                else
                {
                    GenerarNoCategoria2();
                }
                panel1.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
            }
        }

        private void txtimporte_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtimporte);
        }

        private void txtimporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void cmbCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta(cmbCuentaBancaria.Text);
                txtCuenta.Text = valores[0];
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                if (Opcion == "Propietario")
                {
                    c.ConsultaProductoSeleccionado(Clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago, txtConceptoClave, txtReferencia, txtCuenta, txtNumOperacion, txtimporte);

                }
                else
                {
                    c.ConsultaProductoSeleccionadoProveedor(Clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago, txtConceptoClave, txtReferencia, txtCuenta, txtNumOperacion, txtimporte);

                }
                panel1.Enabled = true;
                txtFolio.Text = Clave;
                PanelUsuario.Visible = false;
                button10.Enabled = true;
                button11.Enabled = true;
                btnBuscar.Enabled = false;

                if (txtCuenta.Text != string.Empty)
                {
                    string[] valores = c.InformacionCuenta2(txtCuenta.Text);
                    cmbCuentaBancaria.Text = valores[0];
                }

                if (txtConceptoClave.Text != string.Empty)
                {
                    string[] valores = c.InformacionConcepto(txtConceptoClave.Text);
                    txtConcepto.Text = valores[0];
                }

                if (Opcion == "Propietario")
                {
                    if (txtMatricula.Text != string.Empty)
                    {
                        string[] valores = c.InformacionPropietario(txtMatricula.Text);
                        txtAlumno.Text = valores[0];
                    }
                }
                else
                {
                    if (txtMatricula.Text != string.Empty)
                    {
                        string[] valores = c.InformacionProveedor(txtMatricula.Text);
                        txtAlumno.Text = valores[0];
                    }
                }

            }
            else
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Limpiar();
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

        private void button10_Click(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                {
                    ReciboAnticipo reciboAnticipo = new ReciboAnticipo(txtFolio.Text, "0", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                }
            }
            else
            {
                if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                {
                    ReciboAnticipoProveedor reciboAnticipo = new ReciboAnticipoProveedor(txtFolio.Text, "0", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                }
            }
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                {
                    ReciboAnticipo reciboAnticipo = new ReciboAnticipo(txtFolio.Text, "1", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                }
            }
            else
            {
                if (txtFolio.Text != string.Empty && txtimporte.Text != "0.00")
                {
                    ReciboAnticipoProveedor reciboAnticipo = new ReciboAnticipoProveedor(txtFolio.Text, "1", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                }
            }
        }
    }
}
