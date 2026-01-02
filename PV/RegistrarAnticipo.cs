using Condominios;
using ControlAcademico;
using Guna.UI2.WinForms;
using PuntoVentas;
using PV.Clases;
using PV.Clases.Anticipo;
using PV.Clases.Clientes;
using PV.Clases.Divisas;

using System;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarAnticipo : Form
    {
        DBAnticipo c = new DBAnticipo();
        DBDivisas d = new DBDivisas();
        DBClientes cl = new DBClientes();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;
        public static string FolioC = string.Empty;
        public static string FolioCGeneral = string.Empty;
        public static bool AnticipoRealizado = false;
        string Opcion = string.Empty;

        public RegistrarAnticipo(string opcion)
        {
            InitializeComponent();
            ToolTip tt=new ToolTip();
            tt.SetToolTip(button1, "Nuevo");
            tt.SetToolTip(button10, "Imprimir Anticipo");
            tt.SetToolTip(button11, "Enviar Anticipo");
            tt.SetToolTip(button8, "Consultar Anticipo");
            Opcion = opcion;
            matricula = string.Empty;
            nombre = string.Empty;
            foreach (Control l in panel1.Controls)
            {
                if (l is Label)
                {
                    l.Text = l.Text.ToUpper();
                }
            }
            foreach (Control l in pnRegistrar.Controls)
            {
                if (l is Label)
                {
                    l.Text = l.Text.ToUpper();
                }
            }
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCaja.Text = "1";
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
            c.SeleccionarFormaPago(cmbFormaPago);
            d.SeleccionarDivisa(cmbDivisas);
            if (cmbDivisas.Items.Count > 0)
            {
                cmbDivisas.SelectedIndex = 0;
            }



            if (Opcion == "Propietario")
            {
                c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
                c.CargarAnticipoCliente(dataGridView1, txtFiltro.Text);

                label12.Text = "CLIENTE";
                lbProv.Text = "Cliente";
                dataGridView1.Columns[1].HeaderText = "Cliente";
            }
            else
            {
                c.CargarAnticipoProveedor(dataGridView1, txtFiltro.Text);
                lbProv.Visible = true;
                txtConcepto.Text = "APR - Anticipo Proveedores";
                txtConceptoClave.Text = "APR";
                label12.Text = "PROVEEDOR";
                lbProv.Text = "Proveedor";
            }
        }


        private void RegistrarAnticipo_Load(object sender, EventArgs e)
        {
            txtimporte.Text = txtimporte.Text;
            

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                BuscarCliente buscar = new BuscarCliente();
                buscar.ShowDialog();
                if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
                {
                    txtMatricula.Text = BuscarCliente.Cliente;
                    txtAlumno.Text = BuscarCliente.NombreCliente;
                }
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
            
                //txtMatricula.Text = matricula;
                //txtAlumno.Text = nombre;
            
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
            else if (Convert.ToDecimal(txtimporte.Text)<=0)
            {
                MessageBox.Show("Registre el importe para continuar.");
            }
            else
            {
                if (Opcion == "Propietario")
                {
                    MessageBox.Show(c.RegistroAnticipo(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text, cmbFormaPago.Text, txtConceptoClave.Text, txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text, Convert.ToDecimal(txtImporteMXN.Text), cmbDivisas.Text, txtTipoCambio.Text, FolioC, FolioCGeneral));
                    c.CargarAnticipo(dataGridView1, txtFiltro.Text);
                }
                else
                {
                    MessageBox.Show(c.RegistroAnticipoProveedor(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text, cmbFormaPago.Text, txtConceptoClave.Text, txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text, Convert.ToDecimal(txtImporteMXN.Text), cmbDivisas.Text, txtTipoCambio.Text));
                    c.CargarAnticipoProveedor(dataGridView1, txtFiltro.Text);
                }
                AnticipoRealizado = true;
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
            txtMatricula.Text = string.Empty;
            txtAlumno.Text = string.Empty;
            txtConceptoClave.Clear();
            cmbFormaPago.Text = null;
            txtConcepto.Clear();
            txtReferencia.Text = string.Empty;
            txtCuenta.Clear();
            cmbCuentaBancaria.Text = null;
            txtNumOperacion.Text = string.Empty;
            txtimporte.Text = "0.00";
            txtAlumno.Clear();
            panel1.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            btnBuscar.Enabled = true;
            cmbDivisas.Text = null;
            txtTipoCambio.Clear();
            //txtimporte.Clear();
            txtImporteMXN.Clear();
            txtSaldo.Text = "0.00";
            txtimporte.Enabled = true;
  
            //c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
            if (Opcion == "Propietario")
            {
                c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
                c.CargarAnticipoCliente(dataGridView1, txtFiltro.Text);
            }
            else
            {
                c.CargarAnticipoProveedor(dataGridView1, txtFiltro.Text);
                lbProv.Visible = true;
                txtConcepto.Text = "APR - Anticipo Proveedores";
                txtConceptoClave.Text = "APR";
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
            pnRegistrar.Enabled = false;
            dtpFecha.Enabled = true;


        }

        private void txtimporte_TextChanged(object sender, EventArgs e)
        {
           
                Utilerias.Moneda2(ref txtimporte);
            


            //if (txtimporte.Text == "0.00" || txtimporte.Text == "0")
            //{
            //    txtImporteMXN.Text = "0.00";

            //}
            
                if (txtTipoCambio.Text == string.Empty)
                {
                    MessageBox.Show("Seleccione una divisa con tipo de cambio registrado");
                    txtimporte.Text = "0.00";
                    txtImporteMXN.Text = "0.00";
                    return;
                }
                decimal importemxn = (Convert.ToDecimal(txtimporte.Text) * decimal.Round(Convert.ToDecimal(txtTipoCambio.Text),2));

                txtImporteMXN.Text = importemxn.ToString("0.00");
            
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

                    if (c.ConsultaProductoSeleccionado(Clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago, txtConceptoClave, txtReferencia, txtCuenta, txtNumOperacion, txtImporteMXN, cmbDivisas, txtTipoCambio, txtSaldo, txtimporte) == "1")
                    {
                        lbEstatus.Text = "Estatus:  CANCELADO";
                    }
                    else
                    {
                        lbEstatus.Text = " ";
                    }

                }
                else
                {
                    c.ConsultaProductoSeleccionadoProveedor(Clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago, txtConceptoClave, txtReferencia, txtCuenta, txtNumOperacion, txtImporteMXN, cmbDivisas, txtTipoCambio, txtSaldo, txtimporte);

                }


                //double importemxn = (Convert.ToDouble(txtImporteMXN.Text) / Convert.ToDouble(txtTipoCambio.Text));
                ////importemxn = importemxn * 100;

                //txtimporte.Text = importemxn.ToString();

                //decimal sal = Convert.ToDecimal(txtSaldo.Text);
                //txtSaldo.Text = sal.ToString();
                pnRegistrar.Enabled = false;
                dtpFecha.Enabled = false;
                panel1.Enabled = true;
                txtFolio.Text = Clave;
                PanelUsuario.Visible = false;
                button10.Enabled = true;
                button11.Enabled = true;
                txtimporte.Enabled = false;
                btnBuscar.Enabled = false;

                if (txtCuenta.Text != string.Empty)
                {
                    string[] valores = c.InformacionCuenta2(txtCuenta.Text);
                    cmbCuentaBancaria.Text = valores[0];
                }

                if (txtConceptoClave.Text != string.Empty && Opcion == "Propietario")
                {
                    string[] valores = c.InformacionConcepto(txtConceptoClave.Text);
                    txtConcepto.Text = valores[0];
                }

                if (Opcion == "Propietario")
                {
                    if (txtMatricula.Text != string.Empty)
                    {
                        string[] valores = cl.InformacionCliente(txtMatricula.Text);
                        txtAlumno.Text = valores[1];
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

                if (cmbDivisas.Text != string.Empty)
                {
                    string[] valores = d.InformacionDivisa(cmbDivisas.Text);
                    txtTipoCambio.Text = valores[0];
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

        private void cmbDivisas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivisas.Text != string.Empty)
            {
                string[] valores = d.InformacionDivisa(cmbDivisas.Text);
                txtTipoCambio.Text = valores[0];
                txtimporte.Text = "0.00";
                txtImporteMXN.Text = "0.00";
            }
        }

        private void txtImporteMXN_TextChanged(object sender, EventArgs e)
        {

            Utilerias.Moneda2(ref txtImporteMXN);
            


        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if(Opcion== "Propietario")
            {
                
                    c.CargarAnticipo(dataGridView1, txtFiltro.Text);
                
            }
            else
            {
             
                    c.CargarAnticipoProveedor(dataGridView1, txtFiltro.Text);

                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el anticipo para continuar");
            }
            else if (txtImporteMXN.Text != txtSaldo.Text)
            {
                MessageBox.Show("No es posible cancelar anticipos aplicados");
            }
            else
            {
                if (Opcion == "Propietario")
                {
                    c.EliminarAnticipo(txtFolio.Text);
                    MessageBox.Show("Anticipo cancelado");
                    ReciboAnticipo reciboAnticipo = new ReciboAnticipo(txtFolio.Text, "0", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                    
                }
                else
                {
                    c.EliminarAnticipoProveedor(txtFolio.Text);
                    MessageBox.Show("Anticipo cancelado");
                    ReciboAnticipoProveedor reciboAnticipo = new ReciboAnticipoProveedor(txtFolio.Text, "0", txtMatricula.Text);
                    reciboAnticipo.ShowDialog();
                }
                Limpiar();
            }
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            
                Utilerias.Moneda2(ref txtSaldo);
            


        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMatricula.Text))
            {
                pnRegistrar.Enabled = true;
            }
            else
            {
                pnRegistrar.Enabled = false;
            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolio.Text) )
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
