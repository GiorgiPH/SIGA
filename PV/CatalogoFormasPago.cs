using System;
using System.Windows.Forms;
using PuntoVentas.Clases.FormasPago;
using PV;
using PuntoVentas.Clases.Login;

namespace PuntoVentas
{
    public partial class CatalogoFormasPago : Form
    {
        DBFormasPagos c = new DBFormasPagos();

        public CatalogoFormasPago()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nueva Forma de Pago");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Formas de Pago");
            T.SetToolTip(button9, "Imprimir");
            
        }

        private void CatalogoFormasPago_Load(object sender, EventArgs e)
        {
            //GenerarNoCategoria();
            tgAutorizacion.Checked = false;
            cmbEstatus.Text = "Activo";
            c.CargarFormasPago(dataGridView1);
        }

        void GenerarNoCategoria()
        {
            DBFormasPagos.Folio = 0;
            c.ClaveFormaPagoSiguiente();
            if (DBFormasPagos.Folio == 0)
            {
                DBFormasPagos.Folio = 1;
                txtClaveFormasPago.Text = Convert.ToString(DBFormasPagos.Folio);

            }
            else
            {
                DBFormasPagos.Folio = DBFormasPagos.Folio + 1;
                txtClaveFormasPago.Text = Convert.ToString(DBFormasPagos.Folio);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Close();
        }

     

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion de la forma de pago para continuar.");
            }
            else if (tgAutorizacion.Checked == true)
            {
                if (rdbAdmin.Checked == false && rdbOtro.Checked == false)
                {
                    MessageBox.Show("Registre el tipo de autorizacion para continuar.");
                }
                else
                {
                    MessageBox.Show(c.RegistroFormaPago(txtClaveFormasPago.Text, txtDescripcion.Text, cmbEstatus.Text, cmbClase.Text, cmbCambio.Text, cmbReferencia.Text, tgAutorizacion, rdbAdmin, rdbOtro, txtNotas.Text));
                    Limpiar();
                    //GenerarNoCategoria();
                    c.CargarFormasPago(dataGridView1);
                }
            }
            else
            {
                MessageBox.Show(c.RegistroFormaPago(txtClaveFormasPago.Text, txtDescripcion.Text, cmbEstatus.Text, cmbClase.Text, cmbCambio.Text, cmbReferencia.Text, tgAutorizacion, rdbAdmin, rdbOtro, txtNotas.Text));
                Limpiar();
                //GenerarNoCategoria();
                c.CargarFormasPago(dataGridView1);
            }
        }

        void Limpiar ()
        {
            txtClaveFormasPago.Clear();
            txtDescripcion.Clear(); 
            cmbEstatus.Text= "Activo";
            cmbClase.Text = null;
            cmbCambio.Text = null;
            cmbReferencia.Text = null;
            tgAutorizacion.Checked = false;
            rdbAdmin.Checked = false;
            rdbOtro.Checked = false;
            txtNotas.Clear();
            groupBox4.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCategoria();
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
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                c.ConsultaFormasPagoSeleccionado(Clave, txtDescripcion, cmbEstatus, cmbClase, cmbCambio, cmbReferencia, tgAutorizacion,rdbAdmin, rdbOtro, txtNotas);
                txtClaveFormasPago.Text = Clave;
                PanelUsuario.Visible = false;
                groupBox4.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteFormasPago reporteFormasPago = new ReporteFormasPago();
            reporteFormasPago.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty && txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                       MessageBox.Show( c.EliminarDivisa(txtClaveFormasPago.Text));
                        c.CargarFormasPago(dataGridView1);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rdbSi_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void tgAutorizacion_CheckedChanged(object sender, EventArgs e)
        {
            if (tgAutorizacion.Checked == true)
            {
                rdbAdmin.Checked = false;
                rdbOtro.Checked = false;
                lbTipoAuto.Visible = true;
                gbTipoAuto.Visible = true;
                label1.Text = "Si";
            }
            else
            {
                rdbAdmin.Checked = false;
                rdbOtro.Checked = false;
                lbTipoAuto.Visible = false;
                gbTipoAuto.Visible = false;
                label1.Text = "No";
            }
        }
    }
}
