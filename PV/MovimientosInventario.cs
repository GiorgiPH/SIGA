using System;
using System.Windows.Forms;
using PV.Clases.Inventario;
using PV;

namespace PuntoVentas
{
    public partial class MovimientosInventario : Form
    {
        DBPartidas c = new DBPartidas();

        public static double Subtotal = 0.00;
        public static double Descuento = 0.00;
        public static double Impuesto = 0.00;
        public static double Total = 0.00;
        public static int Partida = 0;

        string UltimoFolio;
        string Descripcion;
        string Descripcio2n;
        string TipoDocumento;
        string Divisa;
        string Almacen;
        string Costeo;
        string AlmacenSalida;

        public MovimientosInventario(string txtUltimoFolio, string cmbDescripcion, string txtDescripcion, string txtTipoDocumento, string cmbDivisa, int txtPartidas, string almacen, string costeo, string almacensalida)
        {
            InitializeComponent();
            UltimoFolio = txtUltimoFolio;
            Descripcion = cmbDescripcion;
            Descripcio2n = txtDescripcion;
            TipoDocumento = txtTipoDocumento;
            Divisa = cmbDivisa;
            Partida = txtPartidas;
            Almacen = almacen;
            Costeo = costeo;
            AlmacenSalida = almacensalida;
        }

        private void MovimientosInventario_Load(object sender, EventArgs e)
        {
            c.SeleccionarDivisa(cmbDivisa);
            c.SeleccionarProducto(cmbProducto);
            cmbDescripcion.Text = TipoDocumento;
            cmbDescripcion.Text = Descripcion;
            cmbDescripcion.Text = Descripcio2n;
            txtUltimoFolio.Text = UltimoFolio;
            cmbDivisa.Text = Divisa;
            Subtotal = 0.00;
            Descuento = 0.00;
            Impuesto= 0.00;
            Total = 0.00;
            txtTotal.Clear();

            if (Partida != 0)
            {
                txtNoPartida.Text = (Convert.ToInt32(Partida) + 1).ToString();
            }

            if (cmbDescripcion.Text == "S" || cmbDescripcion.Text == "T")
            {
                txtPrecio.Enabled = false;
            }
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

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
           
        }

        void Limpiar()
        {
            c.SeleccionarProducto(cmbProducto);
            txtExistencias.Clear();
            txtAlias.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            txtUnidad.Clear();
            txtTotal.Clear();
            txtExAlmacen.Clear();
            txtConcepto.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == string.Empty || txtCantidad.Text=="0")
            {
                MessageBox.Show("Registre la cantidad para continuar");
            }
            else if (txtPrecio.Text == string.Empty || txtPrecio.Text== "0.00")
            {
                MessageBox.Show("Producto debe tener precio");
            }
            else if (cmbDivisa.Text == string.Empty)
            {
                MessageBox.Show("Registre la divisa para continuar");
            }
            else if ((cmbDescripcion.Text == "T" || cmbDescripcion.Text == "S") && (txtExAlmacen.Text=="0" || txtExAlmacen.Text== string.Empty))
            {
                MessageBox.Show("El almacen de salida debe tener existencia del producto");
            }
            else
            {
                int index = cmbProducto.Text.IndexOf("-");
                string producto = cmbProducto.Text;
                string clave = producto.Substring(0, index);

                if (cmbDescripcion.Text == "S" || cmbDescripcion.Text=="T")
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtExAlmacen.Text))
                    {
                        MessageBox.Show("La cantidad no puede ser mayor a la existencia del almacen");
                        return;
                    }
                }

                c.RegistroPartida(txtUltimoFolio.Text, cmbDescripcion.Text, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, Convert.ToDecimal(txtPrecio.Text), cmbDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtTotal.Text), txtConcepto.Text);
                Total = Total + (Convert.ToDouble(txtTotal.Text));

                if (cmbDescripcion.Text == "E")
                {
                    c.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }
                else if (cmbDescripcion.Text == "S")
                {
                    c.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                }
                else if (cmbDescripcion.Text == "T")
                {
                    c.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                    c.RegistroProducto(clave, txtCantidad.Text, AlmacenSalida, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }

                Limpiar();

                Partida = Convert.ToInt32(txtNoPartida.Text) + 1;
                txtNoPartida.Text = Partida.ToString();
            }


        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtIEPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbProducto.Text.IndexOf("-");
            string producto = cmbProducto.Text;
            string clave = producto.Substring(0, index);

            string[] valores = c.InformacionProducto(clave);
            txtExistencias.Text = valores[0];
            txtAlias.Text = valores[1];
            txtUnidad.Text = valores[2];
            txtCantidad.Text = "1";
            txtPrecio.Text = valores[3];
            txtTipoCosteo.Text = valores[4];
            cmbDivisa.Text = valores[5];

            string[] valores2 = c.InformacionProductoAlmacen(clave, Almacen);

            if (DBPartidas.Cantidad == 1)
            {
                txtExAlmacen.Text = valores2[0];
                DBPartidas.Cantidad = 0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Partida > 1)
            {
                Partida = Convert.ToInt32( txtNoPartida.Text) - 1;
                RegistrarEntrada.Bloqueo = 1;
            }
           
            this.Close();
        }

        private void cmbDivisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] valores = c.InformacionDivisa(cmbDivisa.Text);
            txtTipoCambio.Text = valores[0];

        }

        private void button4_Click(object sender, EventArgs e)
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

                c.RegistroPartida(txtUltimoFolio.Text, cmbDescripcion.Text, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, Convert.ToDecimal(txtPrecio.Text), cmbDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtTotal.Text), txtConcepto.Text);
                Subtotal = Subtotal + Convert.ToDouble(txtTotal.Text);
                Total = Total + (Convert.ToDouble(txtTotal.Text));

                if (cmbDescripcion.Text == "E")
                {
                    c.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }
                else if (cmbDescripcion.Text == "S")
                {
                    c.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                }
                else if (cmbDescripcion.Text == "T")
                {
                    c.RegistroProductoSalidas(clave, txtCantidad.Text, Almacen);
                    c.RegistroProducto(clave, txtCantidad.Text, Almacen, Costeo, Convert.ToDecimal(txtPrecio.Text), txtTipoCosteo.Text);
                }

                Limpiar();
                Partida = Convert.ToInt32(txtNoPartida.Text);
                RegistrarEntrada.Bloqueo = 1;
                this.Close();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de cantidad incorrecto");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultaMovimientos consultaMovimientos = new ConsultaMovimientos(cmbDescripcion.Text, cmbDescripcion.Text, txtUltimoFolio.Text);
            consultaMovimientos.ShowDialog();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
