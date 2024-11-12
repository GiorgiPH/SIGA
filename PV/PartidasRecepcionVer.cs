using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;


namespace PV
{
    public partial class PartidasRecepcionVer : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();

        public static string Concep = string.Empty;
        public static string Carpeta = string.Empty;

        public PartidasRecepcionVer(string Folio)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
        }

        private void PartidasRecepcionVer_Load(object sender, EventArgs e)
        {
            c.CargarRecibosPartidasRecepcion(dataGridView1, TxtFolio.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Partida = dataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
                //c.ConsultaPartidaRecepcion(TxtFolio.Text, Partida, txtClaveConcepto, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtTotal, txtImpuesto, txtArchivo);
                txtPartida.Text = Partida;
                txtPrecio.Text = (Convert.ToDecimal(txtSubtotal.Text) / Convert.ToDecimal(txtCantidad.Text)).ToString("N2");
                panel2.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
           Moneda(ref txtTotal);
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

        private void txtDescuento_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtTotal_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtImpuesto_TextChanged(object sender, EventArgs e)
        {
            if (txtImpuesto.Text != string.Empty)
            {
                decimal Impuesto = (Convert.ToDecimal(txtImpuesto.Text) / 100) * Convert.ToDecimal(txtSubtotal.Text);
                txtImpuestoIm.Text = Impuesto.ToString("N2");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio.Text != string.Empty)
                {
                    
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
