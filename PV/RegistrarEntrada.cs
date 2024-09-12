using System;
using System.Windows.Forms;
using PV.Clases.Inventario;
using PuntoVentas.Clases.Login;
using PV.Clases.TipoMovimiento;
using PV;

namespace PuntoVentas
{
    public partial class RegistrarEntrada : Form
    {
        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBTipoMovimiento s = new DBTipoMovimiento();
        public static int Bloqueo = 0;
        public static int Opcion = 0;

        string Documento = string.Empty;

        public RegistrarEntrada(string TipoDocumento)
        {
            InitializeComponent();
            Documento = TipoDocumento;

            ToolTip T = new ToolTip();
            T.SetToolTip(button7, "Nuevo");
            T.SetToolTip(guna2Button1, "Productos / Servicios");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(guna2Button2, "Bloqueo / Desbloqueo");
            T.SetToolTip(guna2Button3, "Bloquear");
            T.SetToolTip(guna2Button4, "Desbloquear");
            T.SetToolTip(guna2Button3, "Bloquear");
            T.SetToolTip(button1, "Confirmar Registro");
            T.SetToolTip(btnarticulos, "Registrar Articulos");
            T.SetToolTip(button1, "Cancelar Registro");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Bloquear()
        {
            cmbDescripcion.Enabled = false;
            txtReferencia.Enabled = false;
            cmbAlmacen.Enabled = false;
            cmbDivisa.Enabled = false;
            txtNotas.Enabled = false;
            btnarticulos.Enabled = false;
            cmbEstatus.Enabled = false;
            button1.Enabled = false;
        }

        void Desbloquear()
        {
            cmbDescripcion.Enabled = true;
            txtReferencia.Enabled = true;
            cmbAlmacen.Enabled = true;
            cmbDivisa.Enabled = true;
            txtNotas.Enabled = true;
            btnarticulos.Enabled = true;
            cmbEstatus.Enabled = true;
            button1.Enabled = true;
        }

        private void RegistrarEntrada_Load(object sender, EventArgs e)
        {
            if (Documento == "E")
            {
                c.SeleccionarDocumentoEntrada(cmbDescripcion);
                txtTipoDocumento.Text = "E";
                c.CargarEntrada(dataGridView1);
            }
            else if (Documento == "S")
            {
                c.SeleccionarDocumentoSalida(cmbDescripcion);
                txtTipoDocumento.Text = "S";
                c.CargarSalida(dataGridView1);
            }
            else if (Documento == "T")
            {
                c.SeleccionarDocumentoTraslado(cmbDescripcion);
                txtTipoDocumento.Text = "T";
                lbSalida.Visible = true;
                lbEntrada.Visible = true;
                cmbAlmacenSalida.Visible = true;
                c.CargarTraspaso(dataGridView1);
            }

            c.SeleccionarDivisa(cmbDivisa);
            c.SeleccionarAlmacen(cmbAlmacen);
            c.SeleccionarAlmacen(cmbAlmacenSalida);
            Bloqueo = 0;

        }

        private void cmbDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "E")
            {
                string[] valores = c.InformacionEntrada(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
                txtCosteo.Text = valores[2];
            }
            else if (txtTipoDocumento.Text == "S")
            {
                string[] valores = c.InformacionSalida(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
            }
            else if (txtTipoDocumento.Text == "T")
            {
                string[] valores = c.InformacionTraspaso(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
            }


        }

        private void cmbDivisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivisa.Enabled == true)
            {
                string[] valores = c.InformacionDivisa(cmbDivisa.Text);
                txtTipoCambio.Text = valores[0];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUltimoFolio.Text != string.Empty)
            {
                int ultimofolio = Convert.ToInt32(txtUltimoFolio.Text);
                ultimofolio = ultimofolio + 1;
                txtFolioRegistrar.Text = Convert.ToString(ultimofolio);
                cmbEstatus.Text = "Activo";

                string Hoy = DateTime.Today.ToString();
                dtpFecha.Text = Hoy;

                cmbDivisa.Text = "MXN";
                txtTotalPartidas.Text = "0";
                txtTotal.Text = "0.00";

                txtElaborado.Text = DBLogin.usuario;
              //  groupBox2.Enabled = true;
                if (cmbDivisa.Enabled == true)
                {
                    string[] valores = c.InformacionDivisa(cmbDivisa.Text);
                    txtTipoCambio.Text = valores[0];
                }
            }
            else
            {
                MessageBox.Show("Seleccione el documento de entrada para continuar");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text == string.Empty)
            {
                MessageBox.Show("Registre la referencia para continuar");
            }
            else if (txtFolioRegistrar.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el documente y confirme el registro para continuar");
            }
            else if (cmbDivisa.Text == string.Empty)
            {
                MessageBox.Show("Registre la divisa para continuar");
            }
            else if (cmbAlmacen.Text == string.Empty)
            {
                MessageBox.Show("Registre el almacen para continuar");
            }
            else if (txtTipoDocumento.Text == "T" && cmbAlmacenSalida.Text == string.Empty)
            {
                MessageBox.Show("Registre el almacen de entrada para continuar");
            }
            else if (txtTipoDocumento.Text == "T" && cmbAlmacenSalida.Text == cmbAlmacen.Text)
            {
                MessageBox.Show("El almacen de Salida y Entrada deben ser diferentes");
            }
            else
            {
                if (txtFolioP.Text == string.Empty)
                {
                    c.RegistroMovimientoInventario(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, dtpFecha.Text, cmbEstatus.Text, txtReferencia.Text, txtAlmacen.Text, txtTotalPartidas.Text, cmbDivisa.Text, txtTipoCambio.Text, txtTotal.Text, txtNotas.Text, txtElaborado.Text, txtFolioP, txtAlmacenSalida.Text);
                    c.RegistroMovimiento(txtTipoDocumento.Text, cmbDescripcion.Text, txtFolioRegistrar.Text);

                }
                MovimientosInventario mov = new MovimientosInventario(txtFolioP.Text, cmbDescripcion.Text, txtDescripcion.Text, txtTipoDocumento.Text, cmbDivisa.Text, Convert.ToInt32(txtTotalPartidas.Text), txtAlmacen.Text, txtCosteo.Text, txtAlmacenSalida.Text);
                mov.ShowDialog();
            }
        }

        private void RegistrarEntrada_Activated(object sender, EventArgs e)
        {
            if (txtTotal.Text == "0.00")
            {
                txtTotal.Text = MovimientosInventario.Total.ToString();
                txtTotalPartidas.Text = MovimientosInventario.Partida.ToString();
            }
            else
            {
                txtTotal.Text = (MovimientosInventario.Total + Convert.ToDouble(txtTotal.Text)).ToString();
                txtTotalPartidas.Text = MovimientosInventario.Partida.ToString();
            }

            if (Bloqueo == 1)
            {
                BLoqueo();
                Bloqueo = 0;
            }

            if (Opcion == 1)
            {
                c.ActualizarMovimiento(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtTotalPartidas.Text, txtTotal.Text);
                Desbloquear();
                Opcion = 0;
            }
        }

        void Limpiar()
        {

            if (Documento == "E")
            {
                c.SeleccionarDocumentoEntrada(cmbDescripcion);
                txtTipoDocumento.Text = "E";
                c.CargarEntrada(dataGridView1);
            }
            else if (Documento == "S")
            {
                c.SeleccionarDocumentoSalida(cmbDescripcion);
                txtTipoDocumento.Text = "S";
                c.CargarSalida(dataGridView1);
            }
            else if (Documento == "T")
            {
                c.SeleccionarDocumentoTraslado(cmbDescripcion);
                txtTipoDocumento.Text = "T";
                lbSalida.Visible = true;
                lbEntrada.Visible = true;
                cmbAlmacenSalida.Visible = true;
                c.CargarTraspaso(dataGridView1);
            }

            c.SeleccionarDivisa(cmbDivisa);
            txtUltimoFolio.Clear();
            txtFolioRegistrar.Clear();
            txtReferencia.Clear();
            cmbAlmacen.Text = null;
            txtTotalPartidas.Text = "0";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            txtElaborado.Clear();
            txtDescripcion.Clear();
            txtAlmacen.Clear();
            txtCosteo.Clear();
            txtAlmacenSalida.Clear();
            txtFolioP.Clear();
            cmbAlmacenSalida.Text = null;
           // groupBox2.Enabled = false;
            MovimientosInventario.Subtotal = 0.00;
            MovimientosInventario.Descuento = 0.00;
            MovimientosInventario.Impuesto = 0.00;
            MovimientosInventario.Total = 0.00;
            MovimientosInventario.Partida = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtTotalPartidas.Text != "0" || txtTotalPartidas.Text != string.Empty)
            {
                c.ActualizarMovimiento2(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtTotalPartidas.Text, txtTotal.Text);
                Limpiar();
                Desbloquear();
            }
            else
            {
                Limpiar();
                Desbloquear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BLoqueo();

        }

        void BLoqueo()
        {
            if (txtTotalPartidas.Text == "0")
            {
                MessageBox.Show("Es necesario registrar articulos para bloquear.");
            }
            else
            {
                if (MessageBox.Show("El registro quedara bloqueado, ¿desea continuar?", "Movimiento de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    c.ActualizarMovimiento(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtTotalPartidas.Text, txtTotal.Text);
                    Limpiar();

                    if (Documento == "E")
                    {
                        c.SeleccionarDocumentoEntrada(cmbDescripcion);
                        txtTipoDocumento.Text = "E";
                        c.CargarEntrada(dataGridView1);
                    }
                    else if (Documento == "S")
                    {
                        c.SeleccionarDocumentoSalida(cmbDescripcion);
                        txtTipoDocumento.Text = "S";
                        c.CargarSalida(dataGridView1);
                    }
                    else if (Documento == "T")
                    {
                        c.SeleccionarDocumentoTraslado(cmbDescripcion);
                        txtTipoDocumento.Text = "T";
                        c.CargarTraspaso(dataGridView1);
                    }

                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Cancelado")
            {
                AutentificarAdmin autentificarAdmin = new AutentificarAdmin();
                autentificarAdmin.ShowDialog();

            }
            else
            {
                MessageBox.Show("El documento esta cancelado no se puede desbloquear");
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



        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Bloquear();

                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();
                string Consecutivo = dataGridView1.Rows[e.RowIndex].Cells["Consecutivo"].Value.ToString();
           //     c.ConsultaEntradaSeleccionado(Folio, Tipo, Documento, txtTotalPartidas, txtReferencia, txtAlmacen, txtTotal, txtNotas, cmbEstatus, txtElaborado, cmbDivisa, txtTipoCambio, txtFolioP);
                txtFolioRegistrar.Text = Consecutivo;
                txtTipoDocumento.Text = Tipo;
                cmbDescripcion.Text = Documento;
                MovimientosInventario.Partida = Convert.ToInt32(txtTotalPartidas.Text);

                if (txtAlmacen.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacen.Text);
                    cmbAlmacen.Text = valores[0];
                }
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int consulta = 1;
            CatalogoProductosServicios prod = new CatalogoProductosServicios(consulta);
            prod.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int consulta = 1;
            ConsultaInventario prod = new ConsultaInventario(consulta);
            prod.ShowDialog();
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacen.Text = valores[0];
            }
        }

        private void cmbAlmacenSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacenSalida.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacenSalida.Text);
                txtAlmacenSalida.Text = valores[0];
            }
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            Limpiar();
            cmbDescripcion.SelectedIndex = 0;
            Desbloquear();
            cmbDescripcion.DroppedDown = true;
            cmbDescripcion.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
