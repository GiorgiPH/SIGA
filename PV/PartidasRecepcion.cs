using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class PartidasRecepcion : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        string recibo = string.Empty;
        string reciboCol = string.Empty;
        public static string Carpeta = string.Empty;
        int opcion = 0;

        public PartidasRecepcion(string Folio, string Recibo, string orden, string almacen, int Opcion)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            txtOrden.Text = orden;
            txtAlmacen.Text = almacen;
            opcion = Opcion;
        }

        private void PartidasRecepcion_Load(object sender, EventArgs e)
        {
            if (txtOrden.Text != string.Empty)
            {
                c.SeleccionarProductoRecepcion(cmbConcepto, txtOrden.Text);
                c.ConsultaRecepcion(TxtFolio.Text, txtPartida);
                txtPrecio.Enabled = false;
                txtCantidad.Text = "1";
                txtUnidad.Text = "Servicio";
                txtDivisa.Text = "MXN";
                txtTipoCambio.Text = "1.00";
            }
            else
            {
                c.SeleccionarProducto(cmbConcepto);
                c.ConsultaRecepcion(TxtFolio.Text, txtPartida);
                txtPrecio.Enabled = true;
                txtCantidad.Text = "1";
                txtUnidad.Text = "Servicio";
                txtDivisa.Text = "MXN";
                txtTipoCambio.Text = "1.00";
            }
            if (opcion!=0)
            {
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (txtOrden.Text != string.Empty)
                {
                    string[] valores = c.InformacionRecepcion(cmbConcepto.Text, txtOrden.Text);
                    txtClave.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtDescuento.Text = valores[3];
                    txtTotal.Text = valores[4];
                    txtConcepto2.Text = valores[5];
                    txtPartidaOrden.Text= valores[7];
                    txtCantidad2.Text = valores[8];
                    txtCantidad.Text = valores[8];
                    txtImpuesto.Text = valores[9];
                    txtUnidad.Text = valores[10];
                    txtCosteo.Text = valores[11];
                }
                else
                {
                    string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                    txtClave.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtUnidad.Text = valores[3];
                    txtImpuesto.Text = valores[4];
                    txtCosteo.Text = valores[6];
                }

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el Producto para continuar");
                return;
            }
            else if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }
            else if (txtOrden.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRecepcion(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtImpuesto.Text), txtArchivo.Text);
                c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text, txtCosteo.Text, Convert.ToDecimal(txtPrecio.Text));
                Limpiar();
                c.ConsultaRecepcion(TxtFolio.Text, txtPartida);
                c.ReciboSaldosPartidasRecepcion(TxtFolio.Text, txtSubtotalR, txtDescuentoR, txtTotalR);
                c.ReciboSaldosPartidasRecepcion2(TxtFolio.Text, txtImpuestoR);

                if (txtOrden.Text != string.Empty)
                {
                    c.SeleccionarProductoRecepcion(cmbConcepto, txtOrden.Text);
                }
                else
                {
                  
                    c.SeleccionarProducto(cmbConcepto);
                }
            }
        }

        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtImpuesto.Text = "0";
            txtClave.Clear();
            txtConcepto.Clear();
            //cmbConcepto.Text = null;
            txtArchivo.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (txtTotal.Text == "0.00" || txtTotal.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRecepcion(TxtFolio.Text, Partida.ToString());
                        c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                    }
                   
                    this.Close();
                    ConceptosGlobalesPartidaRecepcion documentoConceptoGlobal = new ConceptosGlobalesPartidaRecepcion(TxtFolio.Text, recibo, reciboCol);
                    documentoConceptoGlobal.ShowDialog();
                }
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRecepcion(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtImpuesto.Text), txtArchivo.Text);
                c.ActualizarRecepcion(TxtFolio.Text, txtPartida.Text);
                c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text, txtCosteo.Text, Convert.ToDecimal(txtPrecio.Text));
                this.Close();
                ConceptosGlobalesPartidaRecepcion documentoConceptoGlobal = new ConceptosGlobalesPartidaRecepcion(TxtFolio.Text, recibo, reciboCol);
                documentoConceptoGlobal.ShowDialog();
            }

        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);

            try
            {
                if (txtSubtotal.Text != string.Empty)
                {
                    //txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtSubtotal.Text == string.Empty)
                {
                    txtSubtotal.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de subtotal incorrecto");
            }
            
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);

            try
            {
                if (txtDescuento.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtDescuento.Text == string.Empty)
                {
                    txtDescuento.Text = "0.00";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de descuento incorrecto");
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtSubtotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty && txtCantidad2.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtCantidad2.Text))
                    {
                        MessageBox.Show("No es posible registrar una cantidad mayor a la registrada en la orden de commpra");
                        txtCantidad.Text = txtCantidad2.Text;
                    }
                    else
                    {
                        decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                        txtSubtotal.Text = sub.ToString();
                        decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                        txtImpuestoIm.Text = Impuesto.ToString("N2");
                        txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                    }
                }
                else if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                    txtSubtotal.Text = sub.ToString();
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de cantidad incorrecto");
            }
            
        }

        private void txtImpuesto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtImpuesto.Text != string.Empty)
                {
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                }
                else if (txtImpuesto.Text == string.Empty)
                {
                    txtImpuesto.Text = "0";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de impuesto incorrecto");
            }
           
        }

        private void txtImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty && txtCantidad2.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtCantidad2.Text))
                    {
                        MessageBox.Show("No es posible registrar una cantidad mayor a la registrada en la orden de commpra");
                        txtCantidad.Text = txtCantidad2.Text;
                    }
                    else
                    {
                        decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                        txtSubtotal.Text = sub.ToString();
                        decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                        txtImpuestoIm.Text = Impuesto.ToString("N2");
                        txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");
                    }
                }
                else if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                    txtSubtotal.Text = sub.ToString();
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Impuesto - Convert.ToDecimal(txtDescuento.Text)).ToString("N2");

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de precio incorrecto");
            }
           

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PartidasRecepcionVer partidasRecepcionVer = new PartidasRecepcionVer(TxtFolio.Text);
            partidasRecepcionVer.ShowDialog();
        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotalR);
        }

        private void txtDescuentoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoR);
        }

        private void txtImpuestoR_TextChanged(object sender, EventArgs e)
        {
            //Moneda(ref txtImpuestoR);
        }

        private void txtTotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotalR);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio.Text != string.Empty)
                {
                    c.InsertarPartidaRecepcion(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDescuento.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtImpuesto.Text), txtArchivo.Text);
                    string NoOrdenResl = TxtFolio.Text;
                    string Descripcion = txtPartida.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "P" + NoOrdenResl;

                    try
                    {
                        if (Directory.Exists(Carpeta))
                        {

                        }
                        else
                        {
                            Directory.CreateDirectory(Carpeta);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "P" + NoOrdenResl;

                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "All Files|*.*";

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string archivo = open.FileName;
                        string ext = Path.GetExtension(archivo);
                        try
                        {
                            File.Copy(archivo, Carpeta + @"\" + Descripcion + ext);
                            txtArchivo.Text = Descripcion + ext;
                            c.ModificarExtension4(TxtFolio.Text, txtPartida.Text, ext);
                            c.ActualizarRecepcion2(TxtFolio.Text, txtPartida.Text, txtArchivo.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ya hay un archivo guardado" + ex.ToString());
                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = TxtFolio.Text;
                    string Descripcion = txtPartida.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "P" + NoOrdenResl;

                    Process.Start(Carpeta + @"\" + txtArchivo.Text);
                }
                else if (TxtFolio.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }

            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = TxtFolio.Text;
                    string Descripcion = txtPartida.Text;
                    Carpeta = DBOrdenCompra.Ruta + @"\" + "P" + NoOrdenResl;
                    if (Directory.Exists(Carpeta))
                    {
                        File.Delete(Carpeta + @"\" + txtArchivo.Text);
                        txtArchivo.Clear();
                        c.ModificarExtension3(TxtFolio.Text, txtPartida.Text);
                    }
                }
                else if (TxtFolio.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }
    }
}
