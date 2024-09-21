using PuntoVentas;
using PuntoVentas.Clases.Login;
using PV.Clases.Inventario;
using PV.Clases.TipoMovimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarEntrada2 : Form
    {
        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBTipoMovimiento s = new DBTipoMovimiento();
        public static int Bloqueo = 0;
        public static int Opcion = 0;

        string Documento = string.Empty;

        DBPartidas c1 = new DBPartidas();

        public static double Subtotal = 0.00;
        public static double Descuento = 0.00;
        public static double Impuesto = 0.00;
        public static double Total = 0.00;
        public static int Partida1 = 0;

        string UltimoFolio;
        string Descripcion;
        string Descripcio2n;
        string TipoDocumento;
        string Divisa;
        string Almacen;
        string Costeo;
        string AlmacenSalida;
        public RegistrarEntrada2(string TipoDocumento)
        {
            InitializeComponent();
            Documento = TipoDocumento;

            ToolTip T = new ToolTip();
            /*   T.SetToolTip(button7, "Nuevo");
               T.SetToolTip(guna2Button1, "Productos / Servicios");
               T.SetToolTip(guna2CircleButton1, "Menú Principal");
               T.SetToolTip(guna2Button2, "Bloqueo / Desbloqueo");
               T.SetToolTip(guna2Button3, "Bloquear");
               T.SetToolTip(guna2Button4, "Desbloquear");
               T.SetToolTip(guna2Button3, "Bloquear");
              
               T.SetToolTip(btnarticulos, "Registrar Articulos");
               T.SetToolTip(button1, "Cancelar Registro");*/
            
            T.SetToolTip(button1, "Confirmar Registro");
        }

        private void btnarticulos_Click(object sender, EventArgs e)
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
                //   MovimientosInventario mov = new MovimientosInventario(txtFolioP.Text, cmbDescripcion.Text, txtDescripcion.Text, txtTipoDocumento.Text, cmbDivisa.Text, Convert.ToInt32(txtTotalPartidas.Text), txtAlmacen.Text, txtCosteo.Text, txtAlmacenSalida.Text);
                //  mov.ShowDialog();

          


                UltimoFolio = txtFolioP.Text;
                Descripcion = cmbDescripcion.Text;
                Descripcio2n = txtDescripcion.Text;
                TipoDocumento = txtTipoDocumento.Text;
                Divisa = cmbDivisa.Text;
                Partida1 = Convert.ToInt32(txtTotalPartidas.Text);
                Almacen = txtAlmacen.Text;
                Costeo = txtCosteo.Text;
                AlmacenSalida = txtAlmacenSalida.Text;

                c.SeleccionarDivisa(cmbDivisa1);
         
                c.SeleccionarProducto(cmbProducto);
                
                txtTipoDocumento1.Text = TipoDocumento;
                cmbDescripcion1.Text = Descripcion;
                txtDescripcion1.Text = Descripcio2n;
                txtUltimoFolio1.Text = UltimoFolio;
                cmbDivisa1.Text = Divisa;
                Subtotal = 0.00;
                Descuento = 0.00;
                Impuesto = 0.00;
                Total = 0.00;
                txtTotal1.Clear();
              //  MessageBox.Show("3");
                if (Partida1 != 0)
                {
                    txtNoPartida.Text = (Convert.ToInt32(Partida1) + 1).ToString();
                }

                if (cmbDescripcion.Text == "S" || cmbDescripcion.Text == "T")
                {
                    txtPrecio.Enabled = false;
                }
             //   MessageBox.Show("4");


                guna2TabControl1.SelectedIndex = 1;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
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
               /* if (cmbDivisa.Enabled == true)
                {
                    string[] valores = c.InformacionDivisa(cmbDivisa.Text);
                    txtTipoCambio.Text = valores[0];
                }*/
            }
            else
            {
                MessageBox.Show("Seleccione el documento de entrada para continuar");
            }
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

        private void cmbDivisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivisa.Enabled == true)
            {
                string[] valores = c.InformacionDivisa(cmbDivisa.Text);
                txtTipoCambio.Text = valores[0];
            }
        }

      /*  private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                //   Limpiar();


                guna2TabControl1.Enabled = true;
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
              
                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;
                Limpiar();
                cmbDescripcion.SelectedIndex = 0;
                Desbloquear();
                cmbDescripcion.DroppedDown = true;
                cmbDescripcion.Focus();
                DesbloquearDetalle();
                DesbloquearEncabezado();
                CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                if (guna2GradientPanel2.Visible == true)
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel2.SendToBack();
                }
                else
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = true;
                    guna2GradientPanel2.BringToFront();
                }
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                guna2TabControl1.Enabled = true;

                CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "CATALOGO PRODUCTOS")
            {
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                int consulta = 1;
                CatalogoProductosServicios prod = new CatalogoProductosServicios(consulta);
                prod.ShowDialog();
                CambioTamañotoolstripPequeño();
            }
        }*/
     /*   void CambioTamañotoolstripPequeño()
        {

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);


            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton14.Size = new Size(23, 79);
        }*/
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
            txtReferencia.Clear();
            txtFolioRegistrar.Text=string.Empty;

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
            guna2DataGridView1.Rows.Clear();
            // groupBox2.Enabled = false;
            MovimientosInventario.Subtotal = 0.00;
            MovimientosInventario.Descuento = 0.00;
            MovimientosInventario.Impuesto = 0.00;
            MovimientosInventario.Total = 0.00;
            MovimientosInventario.Partida = 0;
        }

        void LimpiarDetalle()
        {
            c.SeleccionarProducto(cmbProducto);
            txtExistencias.Clear();
            txtAlias.Clear();
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtUnidad.Clear();
            txtTotal.Text = "0.00";
            txtExAlmacen.Clear();
            txtConcepto.Clear();
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

        void bloquearEncabezado()
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

        void bloquearDetalle()
        {
            cmbProducto.Enabled = false;
            txtConcepto.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            guna2Button6.Enabled = false;

        }

        void DesbloquearEncabezado()
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

        void DesbloquearDetalle()
        {
            cmbProducto.Enabled = true;
            txtConcepto.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
        }


        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == string.Empty || txtCantidad.Text == "0")
            {
                MessageBox.Show("Registre la cantidad para continuar");
            }
            else if (txtPrecio.Text == string.Empty || txtPrecio.Text == "0.00")
            {
                MessageBox.Show("Producto debe tener precio");
            }
            else if (cmbDivisa.Text == string.Empty)
            {
                MessageBox.Show("Registre la divisa para continuar");
            }
            else if ((cmbDescripcion.Text == "T" || cmbDescripcion.Text == "S") && (txtExAlmacen.Text == "0" || txtExAlmacen.Text == string.Empty))
            {
                MessageBox.Show("El almacen de salida debe tener existencia del producto");
            }
            else
            {
                int index = cmbProducto.Text.IndexOf("-");
                string producto = cmbProducto.Text;
                string clave = producto.Substring(0, index);

                if (cmbDescripcion.Text == "S" || cmbDescripcion.Text == "T")
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtExAlmacen.Text))
                    {
                        MessageBox.Show("La cantidad no puede ser mayor a la existencia del almacen");
                        return;
                    }
                }

                c1.RegistroPartida(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, Convert.ToDecimal(txtPrecio.Text), cmbDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtTotal1.Text), txtConcepto.Text);
                Total = Total + (Convert.ToDouble(txtTotal1.Text));

                if (cmbDescripcion.Text == "E")
                {
                    c1.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }
                else if (cmbDescripcion.Text == "S")
                {
                    c1.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                }
                else if (cmbDescripcion.Text == "T")
                {
                    c1.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                    c1.RegistroProducto(clave, txtCantidad.Text, AlmacenSalida, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }

                LimpiarDetalle();

                Partida1 = Convert.ToInt32(txtNoPartida.Text) + 1;
                txtNoPartida.Text = Partida1.ToString();
            }

        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == string.Empty || txtCantidad.Text == "0")
            {
                MessageBox.Show("Registre la cantidad para continuar");
            }
            else if (txtPrecio.Text == string.Empty || txtPrecio.Text == "0.00")
            {
                MessageBox.Show("Registre el precio para continuar");
            }
            else if (cmbDivisa.Text == string.Empty)
            {
                MessageBox.Show("Registre la divisa para continuar");
            }
            else if ((cmbDescripcion.Text == "T" || cmbDescripcion.Text == "S") && (txtExAlmacen.Text == "0" || txtExAlmacen.Text == string.Empty))
            {
                MessageBox.Show("El almacen de salida debe tener existencia del producto");
            }
            else
            {
                int index = cmbProducto.Text.IndexOf("-");
                string producto = cmbProducto.Text;
                string clave = producto.Substring(0, index);

                if (cmbDescripcion.Text == "S" || cmbDescripcion.Text == "T")
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtExAlmacen.Text))
                    {
                        MessageBox.Show("La cantidad no puede ser mayor a la existencia del almacen");
                        return;
                    }
                }

                c1.RegistroPartida(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, Convert.ToDecimal(txtPrecio.Text), cmbDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtTotal1.Text), txtConcepto.Text);
                Subtotal = Subtotal + Convert.ToDouble(txtTotal1 .Text);
                Total = Total + (Convert.ToDouble(txtTotal1.Text));

                if (cmbDescripcion.Text == "E")
                {
                    c1.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }
                else if (cmbDescripcion.Text == "S")
                {
                    c1.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                }
                else if (cmbDescripcion.Text == "T")
                {
                    c1.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                    c1.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }


           
               // RegistrarEntrada.Bloqueo = 1;
                // this.Close();

                PanelPartidasRequisicion.Visible = false;
                 
            string  TipoM = txtTipoDocumento.Text;
                Descripcion = cmbDescripcion.Text;
           string     Folio1 = txtFolioRegistrar.Text;

                c.CargarPartida(guna2DataGridView1, TipoM, Descripcion, txtFolioRegistrar.Text);
                LimpiarDetalle();

                guna2Button9.Visible = true;
                guna2Button9.Enabled = true;
                //  Partida1 = Convert.ToInt32(txtNoPartida.Text);

            //    MessageBox.Show(""+Partida1);
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

           /* if (Partida1 > 1)
            {
                Partida1 = Convert.ToInt32(txtNoPartida.Text) - 1;
                RegistrarEntrada.Bloqueo = 1;
            }*/
            PanelPartidasRequisicion.Visible = false;
           // this.Close();
        }

        private void RegistrarEntrada2_Load(object sender, EventArgs e)
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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2Button11.Visible = false;
            LimpiarDetalle();
            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();
            if (Partida1 == 0)
            {
                Partida1 = 1;
                txtNoPartida.Text = Partida1.ToString();
                //MessageBox.Show("a:" + Partida1);
            }
          else  if (Partida1 >= 1)
            {
                Partida1 = Convert.ToInt32(txtNoPartida.Text) + 1;
                txtNoPartida.Text = Partida1.ToString();
                //MessageBox.Show("b:"+Partida1);
            }
            guna2Button10.Enabled = true;
            guna2Button12.Enabled = true;
            guna2Button8.Enabled = true;
            guna2Button7.Enabled = false;
            c.SeleccionarProducto(cmbProducto);

            
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbProducto.Text.IndexOf("-");
            string producto = cmbProducto.Text;
            string clave = producto.Substring(0, index);

            string[] valores = c1.InformacionProducto(clave);
            txtExistencias.Text = valores[0];
            txtAlias.Text = valores[1];
            txtUnidad.Text = valores[2];
            txtCantidad.Text = "1";
            txtPrecio.Text = valores[3];
            txtTipoCosteo.Text = valores[4];
            cmbDivisa1.Text = valores[5];

            string[] valores2 = c1.InformacionProductoAlmacen(clave, Almacen);

            if (DBPartidas.Cantidad == 1)
            {
                txtExAlmacen.Text = valores2[0];
                DBPartidas.Cantidad = 0;
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    txtTotal1.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de cantidad incorrecto");
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c1.Monto(e);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    txtTotal1.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c1.Monto(e);
        }

        private void Moneda(ref Guna.UI2.WinForms.Guna2TextBox txt)
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

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void cmbDivisa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] valores = c.InformacionDivisa(cmbDivisa1.Text);
            txtTipoCambio1.Text = valores[0];

        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string NoPartida = guna2DataGridView1.Rows[e.RowIndex].Cells["NoPartida"].Value.ToString();
            string cantidad = string.Empty;
            string total = string.Empty;

            c1.ConsultarPartida(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, NoPartida, txtclave, txtCantidad, txtUnidad, txtPrecio, lblDivisa1, txtTipoCambio1, txtTotal1, txtConcepto);
            cantidad = txtCantidad.Text;
            cmbDivisa1.Items.Add(lblDivisa1.Text);
            PanelPartidasRequisicion.Visible = true;
            guna2Button11.Visible = true;
            txtNoPartida.Text = NoPartida;

            if (txtclave.Text != string.Empty || txtclave.Text != "")
            {
                cmbProducto.Items.Clear();
                /*   string[] valores1 = c1.SeleccionarProducto2(txtclave.Text);
                   {
                       MessageBox.Show(valores1[0]);
                       cmbProducto.Items.Add( valores1[0]);
                      // cmbProducto.SelectedIndex = 0;
                   }*/

                c1.SeleccionarProducto3(cmbProducto, txtclave.Text);
                cmbProducto.SelectedIndex = 0;
            }
            txtCantidad.Text = cantidad;
            guna2Button10.Enabled = false;
            guna2Button12.Enabled = false;
            guna2Button8.Enabled = false;
            guna2Button7.Enabled = true;
            txtAlias.Enabled = false;
            cmbDivisa1.Enabled = false;
        }


        private void guna2Button9_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("P:" + Partida1);
            txtTotalPartidas.Text = Partida1.ToString();
            BLoqueo();

            guna2TabControl1.SelectedIndex = 0;
           
            txtTipoDocumento1.Text = "";
            cmbDescripcion1.Text = "";
            txtDescripcion1.Text = "";
            guna2TabControl1.Enabled = false;

            Limpiar();
            LimpiarDetalle();
          
            guna2Button9.Enabled = false;
            guna2DataGridView1.Rows.Clear();


        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            c1.Eliminarpartida(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtNoPartida.Text);
            MessageBox.Show("Partida Eliminada");
            PanelPartidasRequisicion.Visible = false;
            
            string TipoM = txtTipoDocumento.Text;
            Descripcion = cmbDescripcion.Text;
            string Folio1 = txtUltimoFolio.Text;
            c.CargarPartida(guna2DataGridView1, TipoM, Descripcion, txtFolioRegistrar.Text);
            LimpiarDetalle();
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

                    txtTotal.Text = Total.ToString();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            BLoqueo();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //Bloquear();
                cmbDivisa.Items.Clear();


                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();
                string Consecutivo = dataGridView1.Rows[e.RowIndex].Cells["Consecutivo"].Value.ToString();
                c.ConsultaEntradaSeleccionado(Folio, Tipo, Documento, txtTotalPartidas, txtReferencia, txtAlmacen, txtTotal, txtNotas, cmbEstatus, txtElaborado, lbldivisa, txtTipoCambio, txtFolioP);
                
                txtFolioRegistrar.Text = Consecutivo;
                txtTipoDocumento.Text = Tipo;
                cmbDescripcion.Text = Documento;
                MovimientosInventario.Partida = Convert.ToInt32(txtTotalPartidas.Text);

                if (txtAlmacen.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacen.Text);
                    cmbAlmacen.Text = valores[0];
                }
                
                cmbDivisa.Items.Add(lbldivisa.Text);
                cmbDivisa.SelectedIndex = 0;
                //    PanelUsuario.Visible = false;
                string TipoM = txtTipoDocumento.Text;
                Descripcion = cmbDescripcion.Text;
                string Folio1 = txtUltimoFolio.Text;

              
                bloquearDetalle();
                bloquearEncabezado();
                c.CargarPartida(guna2DataGridView1, txtTipoDocumento.Text, cmbDescripcion.Text, txtFolioRegistrar.Text);
            }
            else
            {
                return;
            }
            guna2GradientPanel2.Visible = false;

        }

   /*     private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            toolStripButton1.Size = new Size(50, 60);

            //------------
            guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;


            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton14.Size = new Size(85, 75);
            toolStripButton14.AutoSize = false;

        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
           guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip1.Size = new Size(22, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            
            guna2GradientPanel4.Location = new Point(1076, 83);
            guna2GradientPanel4.Size = new Size(23, 569);

            toolStrip1.Size = new Size(23, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);

            // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);


            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);

            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton14.Size = new Size(23, 79);
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            toolStripButton1.Size = new Size(50, 60);
            //----

            guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton14.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;

            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton14.Size = new Size(85, 75);
            toolStripButton14.AutoSize = false;

        }
        */
        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
        
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1017, 82);

            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
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

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;


            guna2GradientPanel6.Location = new Point(1077, 82);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2GradientPanel6.Location = new Point(1077, 82);
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

                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;
                Limpiar();
                cmbDescripcion.SelectedIndex = 0;
                Desbloquear();
                cmbDescripcion.DroppedDown = true;
                cmbDescripcion.Focus();
                DesbloquearDetalle();
                DesbloquearEncabezado();
                
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 82);
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


                if (guna2GradientPanel2.Visible == true)
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel2.SendToBack();
                }
                else
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = true;
                    guna2GradientPanel2.BringToFront();
                }
             

                guna2TabControl1.Enabled = true;

              
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 82);
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
            else if (e.ClickedItem.Text == "CATALOGO PRODUCTOS")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 82);
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

                int consulta = 1;
                CatalogoProductosServicios prod = new CatalogoProductosServicios(consulta);
                prod.ShowDialog();
              
            }
        }
    }

}
    


