using System;
using System.Windows.Forms;
using PV.Clases.ActivoFijo;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class ActivoFijo : Form
    {


        DBActivoFijo c = new DBActivoFijo();

        public ActivoFijo()
        {
            InitializeComponent();
        }

        private void ActivoFijo_Load(object sender, EventArgs e)
        {
            c.CargarActivoFijo(dataGridView2);
            txtElaborado.Text = DBLogin.usuario;
            c.SeleccionarArticulo(cmbArticulo);
            c.DocumentoRecepciopn(cmbDocumento);
            c.Proveedor(cmbProveedor);
            txtProvedor.Focus();
        }

        void GenerarNoActivo()
        {
            DBActivoFijo.Folio = 0;
            c.ClaveSiguiente();
            if (DBActivoFijo.Folio == 0)
            {
                DBActivoFijo.Folio = 1;
                txtFolio.Text = Convert.ToString(DBActivoFijo.Folio);

            }
            else
            {
                DBActivoFijo.Folio = DBActivoFijo.Folio + 1;
                txtFolio.Text = Convert.ToString(DBActivoFijo.Folio);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
               
                GenerarNoActivo();
                cmbEstatus.SelectedIndex = 0;
                cmbDocumento.Enabled = true;
                cmbProveedor.Enabled = true;
                guna2Panel1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoActivo();
                cmbEstatus.SelectedIndex = 0;
                cmbDocumento.Enabled = true;
                cmbProveedor.Enabled = true;
                guna2Panel1.Enabled = true;
            }
        }

        void Limpiar()
        {
            txtFolio.Clear();
            cmbEstatus.Text = null;
            cmbDocumento.Text = null;
            txtDocumento.Clear();
            cmbProveedor.Text = null;
            txtProvedor.Clear();
            txtClave.Clear();
            txtDescripcion.Clear();
            txtFactura.Clear();
            cmbArticulo.Text = null;
            txtUnidad.Clear();
            txtGarantia.Clear();
            txtBaja.Clear();
            txtPersona.Clear();
            txtCentroCosto.Clear();
            txtResguardo.Clear();
            txtDevolucion.Clear();
            txtProvedor.Focus();
            txtFechaVence.Clear();
            txtDiasRestatntes.Clear();
            guna2Panel1.Enabled = false;
            txtNUmeroSerie.Clear();
            PanelUsuario.Visible = false;
            cmbDocumento.Enabled = false;
            cmbProveedor.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbArticulo.Text == string.Empty)
            {
                MessageBox.Show("Registre el articulo para continuar");
            }
            else if (txtUnidad.Text == string.Empty)
            {
                MessageBox.Show("Registre la unidad para continuar");
            }
            else if (txtGarantia.Text == string.Empty)
            {
                MessageBox.Show("Registre la garantia para continuar");
            }
            else if (cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Registre el estatus para continuar");
            }
            else
            {
                if (txtResguardo.Text != string.Empty && txtDevolucion.Text == string.Empty)
                {
                    MessageBox.Show("El activo esta en resguardo registre devolucion para modificar");
                }
                else
                {
                    MessageBox.Show(c.RegistroActivoFijo(txtFolio.Text, cmbEstatus.Text, cmbProveedor.Text, txtFactura.Text, dtpFecha.Text, txtClave.Text, txtDescripcion.Text, cmbArticulo.Text, txtUnidad.Text, txtGarantia.Text, txtElaborado.Text, txtNUmeroSerie.Text, txtDocumento.Text));
                    if (DBActivoFijo.Opcion == 0)
                    {
                        Limpiar();
                        DBActivoFijo.Opcion = 0;

                    }
                    c.CargarActivoFijo(dataGridView2);
                }

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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                c.ConsultaActivoSeleccionada(Clave, cmbEstatus, cmbProveedor, txtFactura, dtpFecha, txtClave, txtDescripcion, cmbArticulo, txtUnidad, txtGarantia, txtElaborado, txtNUmeroSerie, txtDocumento);
                c.ConsultaUltimoResguardoActivoSeleccionada((txtClave.Text + " - " + txtDescripcion.Text), txtPersona, txtCentroCosto, txtResguardo, txtDevolucion);

                if (txtDocumento.Text!= string.Empty && txtDocumento.Text!="0")
                {
                    string[] valores = c.InformacionDocumentoRecepciopn2(txtDocumento.Text);
                    cmbDocumento.Text = valores[0];
                }

                txtFolio.Text = Clave;
                guna2Panel1.Enabled = true;
                cmbDocumento.Enabled = true;
                cmbProveedor.Enabled = true;
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteActivoFijo reporteActivoFijo = new ReporteActivoFijo();
            reporteActivoFijo.ShowDialog();
        }

        private void txtGarantia_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtGarantia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGarantia.Text != string.Empty)
                {
                    int Dias = Convert.ToInt32(txtGarantia.Text);
                    DateTime FechaVence = Convert.ToDateTime(dtpFecha.Text);
                    FechaVence = FechaVence.AddDays(Dias);
                    txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");

                    DateTime oldDate = Convert.ToDateTime(txtFechaVence.Text);
                    DateTime newDate = DateTime.Now;


                    TimeSpan ts = oldDate - Convert.ToDateTime(newDate.ToString("yyyy/MM/dd"));

                    int differenceInDays = ts.Days;
                    if (differenceInDays < 0)
                    {
                        txtDiasRestatntes.Text = "0";
                    }
                    else
                    {
                        txtDiasRestatntes.Text = differenceInDays.ToString();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("El formato de dias de garantia es incorrecto");
            }

        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocumento.Text != string.Empty)
            {
                string[] valores = c.InformacionDocumentoRecepciopn(cmbDocumento.Text);
                txtDocumento.Text = valores[0];
                txtProvedor.Text = valores[1];
                txtFactura.Text = valores[3];
                cmbProveedor.Text = valores[2];
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
