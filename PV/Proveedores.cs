using Condominios.Clases.TiposZonas;
using PV.Clases;
using PV.Clases.Proveedores;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PV
{
    public partial class Proveedores : Form
    {
        DBProveedores c = new DBProveedores();
        DBTiposZonas dbTiposZonas = new DBTiposZonas();

        public Proveedores()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();

            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button4, "Nuevo");
            T.SetToolTip(button8, "Proveedores");
            T.SetToolTip(button9, "Imprimir");
            T.SetToolTip(guna2PictureBox1, "Clic para Desplegar");
            T.SetToolTip(guna2PictureBox2, "Clic para Ocultar");
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            c.CargarClientes(dataGridView1);
            c.SeleccionarZona(cmbZona);
            c.SeleccionarDivisa(cmbDivisaOperacion);
            cmbEstatus.SelectedIndex = 0;

            DataTable dtTiposProveedor = dbTiposZonas.ObtenerTiposProveedor();

            ComboUtil.LlenarComboBox(
                cmbTipoProveedor,
                dtTiposProveedor,
                "Descripcion",
                "Clave"
            );
        }

        void GenerarNoCliente()
        {
            DBProveedores.Folio = 0;
            c.ClaveClienteSiguiente();
            if (DBProveedores.Folio == 0)
            {
                DBProveedores.Folio = 1;
                txtClaveCliente.Text = Convert.ToString(DBProveedores.Folio);

            }
            else
            {
                DBProveedores.Folio = DBProveedores.Folio + 1;
                txtClaveCliente.Text = Convert.ToString(DBProveedores.Folio);

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
            cmbListaPrecios.Text = null;
            dtpDel.ResetText();
            dtpAl.ResetText();
            txtContacto.Clear();
            txtFormaEmbarque.Clear();
            cmbDivisaOperacion.Text = null;
            txtDiasCredito.Clear();
            txtLimiteCredito.Clear();
            txtPorcentajeDescuento.Clear();
            txtBancoPago.Clear();
            txtDomicilioFiscal.Clear();
            txtRegimenFiscal.Clear();
            txtSaldo.Clear();
            tabControl1.Enabled = false;
            groupBox1.Enabled = false;
            tabControl1.Enabled = false;
            rdSi.Checked = false;
            rdNo.Checked = false;
            rdSi2.Checked = false;
            rdNo2.Checked = false;
            PanelUsuario.Visible = false;
            cmbTipoProveedor.SelectedIndex = -1;
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
            else
            {
                if (cmbTipoProveedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de proveedor para continuar.");
                    return;
                }
                MessageBox.Show(c.RegistroCliente(txtClaveCliente.Text, txtRazonSocial.Text, txtRFC.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCodigoPostal.Text, txtCiudad.Text, txtPais.Text, txtReferencia.Text, cmbMetodoPago.Text, cmbFormaPago.Text, cmbCFDI.Text, cmbListaPrecios.Text, dtpDel.Text, dtpAl.Text, cmbZona.Text, txtContacto.Text, txtFormaEmbarque.Text, cmbDivisaOperacion.Text, txtDiasCredito.Text, txtLimiteCredito.Text, txtPorcentajeDescuento.Text, txtBancoPago.Text, txtDomicilioFiscal.Text, txtRegimenFiscal.Text, txtEstado.Text, txtTelefono.Text, txtCelular.Text, txtCorreo.Text, txtCorreo2.Text, cmbEstatus.Text, cmbTipoProveedor.SelectedValue.ToString()));
                Limpiar();
                c.CargarClientes(dataGridView1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                c.ConsultaClienteSeleccionado(Clave, txtRazonSocial, txtRFC, txtCalle, txtNoExterior, txtNoInterior, txtColonia, txtMunicipio, txtCodigoPostal, txtCiudad, txtPais, txtReferencia, cmbMetodoPago, cmbFormaPago, cmbCFDI, cmbListaPrecios, dtpDel, dtpAl, cmbZona, txtContacto, txtFormaEmbarque, cmbDivisaOperacion, txtDiasCredito, txtLimiteCredito, txtPorcentajeDescuento, txtBancoPago, txtDomicilioFiscal, txtRegimenFiscal, txtEstado, txtTelefono, txtCelular, txtCorreo, rdSi, rdNo, txtCorreo2, rdSi2, rdNo2, cmbEstatus, txtSaldo, txtAnticipo, cmbTipoProveedor);
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
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty &&  cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty &&  cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text != string.Empty && txtRFC.Text != string.Empty &&  cmbEstatus.Text != string.Empty)
            {
                tabControl1.Enabled = true;
            }
        }

        private void txtLimiteCredito_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtLimiteCredito);
        }

        private void txtLimiteCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
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

        private void txtPorcentajeDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteProveedor reporteProveedor = new ReporteProveedor();
            reporteProveedor.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                c.CargarClientesFiltro(dataGridView1, txtFiltro.Text);
            }
            else
            {
                c.CargarClientes(dataGridView1);
                txtFiltro.Clear();
            }
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }

        private void txtAnticipo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAnticipo);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(919, 88);

            guna2GradientPanel6.Size = new Size(112, 583);

            // guna2GradientPanel7.Location = new Point(1013, 83);
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


            guna2GradientPanel6.Location = new Point(978, 88);
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
                guna2GradientPanel6.Location = new Point(978, 88);
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
            else if (e.ClickedItem.Text == "PROVEEDORES")
            {

                guna2GradientPanel6.Location = new Point(978, 88);
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
                guna2GradientPanel6.Location = new Point(978, 88);
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

                ReporteProveedor reporteProveedor = new ReporteProveedor();
                reporteProveedor.ShowDialog();
            }
        }
    }
}
