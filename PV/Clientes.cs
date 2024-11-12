using System;
using System.Windows.Forms;
using PV.Clases.Clientes;
using PuntoVentas;
using PV;
using System.Drawing;
using PV.Clases.Divisas;
using System.Data;

namespace PV
{
    public partial class Clientes : Form
    {
        DBClientes c = new DBClientes();
        DBDivisas d = new DBDivisas();
        DBClientes cl = new DBClientes();   

        public Clientes()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();

            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button4, "Nuevo");
            T.SetToolTip(button8, "Clientes");
            T.SetToolTip(button9, "Imprimir");
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            //GenerarNoCliente();
            CargarClientes();
            d.SeleccionarDivisaActivas(cmbDivisaOperacion);
            c.SeleccionarTipoCliente(cmbTipoCliente2);
            c.SeleccionarZona(cmbZona);
            cmbEstatus.SelectedIndex = 0;
        }
        private void CargarClientes()
        {
            try
            {
                var pagos = cl.CargarClientes("");

                dataGridView1.Rows.Clear();

                foreach (DataRow pago in pagos.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = pago["IdCliente"];
                    dataGridView1.Rows[n].Cells[1].Value = pago["RazonSocial"];
                    dataGridView1.Rows[n].Cells[2].Value = pago["TipoCliente"].ToString();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }
        void GenerarNoCliente()
        {
            DBClientes.Folio = 0;
            c.ClaveClienteSiguiente();
            if (DBClientes.Folio == 0)
            {
                DBClientes.Folio = 1;
                txtClaveCliente.Text = Convert.ToString(DBClientes.Folio);

            }
            else
            {
                DBClientes.Folio = DBClientes.Folio + 1;
                txtClaveCliente.Text = Convert.ToString(DBClientes.Folio);

            }
        }

        void Limpiar()
        {
            txtClaveCliente.Clear();
            txtRazonSocial.Clear();
            txtRFC.Clear();
            txtCalle.Clear();
            cmbEstatus.SelectedIndex = 0;
            txtNoInterior.Clear();
            txtNoExterior.Clear();
            txtMunicipio.Clear();
            txtCodigoPostal.Clear();
            txtCiudad.Clear();
            txtPais.Clear();
            txtEstado.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtCorreo.Clear();
            txtCorreo2.Clear();
            txtColonia.Clear();
            txtReferencia.Clear();
            cmbCFDI.Text = null;
            cmbFormaPago.Text = null;
            cmbMetodoPago.Text = null;
            cmbTipoCliente.Text = null;
            cmbListaPrecios.Text = null;
            dtpDel.ResetText();
            dtpAl.ResetText();
            cmbTipoCliente2.Text = null;
            txtContacto.Clear();
            txtFormaEmbarque.Clear();
            txtDomicilioEntrega.Clear();
            cmbAgenteVentas.Text = null;
            txtPorcentajeComision.Clear();
            txtAnticipoPedidos.Clear();
            cmbDivisaOperacion.Text = null;
            txtDiasCredito.Clear();
            txtLimiteCredito.Clear();
            txtPorcentajeDescuento.Clear();
            cmbBaseComision.Text = null;
            txtPorcentajeRecargos.Clear();
            cmbEncargadoCuentasPagar.Text = null;
            txtBancoPago.Clear();
            txtDomicilioFiscal.Clear();
            txtRegimenFiscal.Clear();
            txtExportacion.Clear();
            lbNombre.Visible = true;
            lbrazonSocial.Visible = false;
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
                MessageBox.Show("Generar un nuevo registro.");
            }
            else if (txtRazonSocial.Text == string.Empty)
            {
                MessageBox.Show("Registre la razon social del cliente para continuar.");
            }
            else if (txtRFC.Text == string.Empty)
            {
                MessageBox.Show("Registre el RFC paterno del cliente para continuar.");
            }
            else if (cmbTipoCliente.Text == string.Empty)
            {
                MessageBox.Show("Registre el tipo de cliente para continuar.");
            }
            else
            {
                MessageBox.Show(c.RegistroCliente(txtClaveCliente.Text, txtRazonSocial.Text, cmbTipoCliente.Text, txtRFC.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCodigoPostal.Text, txtCiudad.Text, txtPais.Text, txtReferencia.Text, cmbMetodoPago.Text, cmbFormaPago.Text, cmbCFDI.Text, cmbListaPrecios.Text, dtpDel.Text, dtpAl.Text, cmbTipoCliente2.Text, cmbZona.Text, txtContacto.Text, txtFormaEmbarque.Text, txtDomicilioEntrega.Text, cmbAgenteVentas.Text, txtPorcentajeComision.Text, txtAnticipoPedidos.Text, cmbDivisaOperacion.Text, txtDiasCredito.Text, txtLimiteCredito.Text, txtPorcentajeDescuento.Text, cmbBaseComision.Text, txtPorcentajeRecargos.Text, cmbEncargadoCuentasPagar.Text, txtBancoPago.Text, txtDomicilioFiscal.Text, txtRegimenFiscal.Text, txtExportacion.Text, txtEstado.Text, txtTelefono.Text, txtCelular.Text, txtCorreo.Text, rdSi, rdNo, txtCorreo2.Text, rdSi2, rdNo2, cmbEstatus.Text));
                Limpiar();
                //GenerarNoCliente();
                CargarClientes();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCliente();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
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
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveCliente"].Value.ToString();
                c.ConsultaClienteSeleccionado(Clave, txtRazonSocial, cmbTipoCliente, txtRFC, txtCalle, txtNoExterior, txtNoInterior, txtColonia, txtMunicipio, txtCodigoPostal, txtCiudad, txtPais, txtReferencia, cmbMetodoPago, cmbFormaPago, cmbCFDI, cmbListaPrecios, dtpDel, dtpAl, cmbTipoCliente2, cmbZona, txtContacto, txtFormaEmbarque, txtDomicilioEntrega, cmbAgenteVentas, txtPorcentajeComision, txtAnticipoPedidos, cmbDivisaOperacion, txtDiasCredito, txtLimiteCredito, txtPorcentajeDescuento, cmbBaseComision, txtPorcentajeRecargos, cmbEncargadoCuentasPagar, txtBancoPago, txtDomicilioFiscal, txtRegimenFiscal, txtExportacion, txtEstado, txtTelefono, txtCelular, txtCorreo, rdSi, rdNo, txtCorreo2, rdSi2, rdNo2, cmbEstatus, txtAnticipo);
                txtClaveCliente.Text = Clave;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteClientes reporteClientes = new ReporteClientes();
            reporteClientes.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveCliente.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCliente();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCliente();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
            }
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

            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty)
            {
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
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty && cmbTipoCliente.Text != string.Empty && cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void txtLimiteCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(900, 90);
            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
       //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;



            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;


            guna2GradientPanel6.Location = new Point(969, 90);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2GradientPanel6.Location = new Point(969, 90);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                if (txtClaveCliente.Text == string.Empty)
                {
                    Limpiar();
                    GenerarNoCliente();
                    groupBox1.Enabled = true;
                    tabControl1.Enabled = true;
                }
                else
                {
                    Limpiar();
                    GenerarNoCliente();
                    groupBox1.Enabled = true;
                    tabControl1.Enabled = true;
                }
            }
            else if (e.ClickedItem.Text == "CLIENTES")
            {

                guna2GradientPanel6.Location = new Point(969, 90);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;


                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);


                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;


                if (PanelUsuario.Visible == false)
                {
                    PanelUsuario.Visible = true;
                }
                else if (PanelUsuario.Visible == true)
                {
                    PanelUsuario.Visible = false;
                }
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel6.Location = new Point(969, 90);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;



                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);



                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                ReporteClientes reporteClientes = new ReporteClientes();
                reporteClientes.ShowDialog();
            }
        }
    }
}
