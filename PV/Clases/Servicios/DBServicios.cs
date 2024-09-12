using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases.Servicios
{
    class DBServicios
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBServicios()
        {
            try
            {
                cn = new SqlConnection(ObtenerCn());
                cn.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveProductoSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(ClaveServicio) from Servicios", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() != string.Empty)
                    {
                        Folio = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
            return contador;
        }
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public string RegistroProducto(string txtClaveProducto, string txtAlias, string txtDescripcion, string cmbEstatus, string txtCategoria, string txtFamilia, string cmbTipoCosteo, string txtCostoUnitario, string cmbDivisa, string txtDescuentoPorc, string txtDescuentoCant, string txtImpuestoPorc, string txtImpuestoCant, string txtPrecioVenta, PictureBox Foto, string Concepto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Servicios where ClaveServicio='" + txtClaveProducto + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                   

                    if (Foto.Image == null)
                    {
                        cmd = new SqlCommand("Insert into Servicios (ClaveServicio, Alias, Descripcion, Estatus, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, ConceptoGlobales) values ('" + txtClaveProducto + "', '" + txtAlias + "',  '" + txtDescripcion + "',  '" + cmbEstatus + "','" + txtCategoria + "','" + txtFamilia + "','" + cmbTipoCosteo + "', '" + txtCostoUnitario + "',  '" + cmbDivisa + "',  '" + txtDescuentoPorc + "', '" + txtDescuentoCant + "',  '" + txtImpuestoPorc + "',  '" + txtImpuestoCant + "', '" + txtPrecioVenta + "', '" + Concepto + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Servicios (ClaveServicio, Alias, Descripcion, Estatus, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, Foto, ConceptoGlobales) values ('" + txtClaveProducto + "', '" + txtAlias + "',  '" + txtDescripcion + "',  '" + cmbEstatus + "','" + txtCategoria + "','" + txtFamilia + "','" + cmbTipoCosteo + "', '" + txtCostoUnitario + "',  '" + cmbDivisa + "',  '" + txtDescuentoPorc + "', '" + txtDescuentoCant + "',  '" + txtImpuestoPorc + "',  '" + txtImpuestoCant + "', '" + txtPrecioVenta + "', @Foto, '" + Concepto + "')", cn);
                        cmd.Parameters.Add("@Foto", SqlDbType.Image);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cmd.Parameters["@Foto"].Value = ms.GetBuffer();
                        cmd.ExecuteNonQuery();

                        mensaje = "Registro guardado.";
                    }

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Inventariable = string.Empty;

                        

                        if (Foto.Image == null)
                        {
                            cmd = new SqlCommand("Update Servicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Inventariable='" + Inventariable + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', ConceptoGlobales='" + Concepto + "' where ClaveServicio= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Servicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Inventariable='" + Inventariable + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', Foto=@Foto, ConceptoGlobales='" + Concepto + "' where ClaveServicio= '" + txtClaveProducto + "'", cn);
                            cmd.Parameters.Add("@Foto", SqlDbType.Image);
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            cmd.Parameters["@Foto"].Value = ms.GetBuffer();
                            cmd.ExecuteNonQuery();
                        }

                        mensaje = "Registro modificado.";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //___________________________________________________________________________________________________________________
        public void Monto(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarCategorias(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("select * from Categorias", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarConceptoGlobal(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from ConceptosGlobales where Clase='Impuesto'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarFamilia(ComboBox cb, string ClaveCategoria)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Familias where ClaveCategoria= " + ClaveCategoria + " and Vincular in ('Ambos','Servicios')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionCategorias(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from Categorias where Nombre = '" + nombre + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),

            };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionConcepto(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ConceptosGlobales where Nombre = '" + nombre + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),

            };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionFamilia(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from Familias where Nombre = '" + nombre + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),

            };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        //________________________________________________________________________________________________
        //tiendas Registrados
        public void CargarProductos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Servicios", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveServicio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaProductoSeleccionado(string txtClaveProducto, Guna2TextBox txtAlias, Guna2TextBox txtDescripcion, ComboBox cmbEstatus, TextBox txtCategoria, TextBox txtFamilia, ComboBox cmbCategoria, ComboBox cmbFamilia, ComboBox cmbTipoCosteo, Guna2TextBox txtCostoUnitario, ComboBox cmbDivisa, Guna2TextBox txtDescuentoPorc, Guna2TextBox txtDescuentoCant, Guna2TextBox txtImpuestoPorc, Guna2TextBox txtImpuestoCant, Guna2TextBox txtPrecioVenta, PictureBox Foto, TextBox txtConcepto, ComboBox cmbConcepto)
        {
            try
            {
                cmd = new SqlCommand("Select * from Servicios where ClaveServicio='" + txtClaveProducto + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAlias.Text = dr["Alias"].ToString();
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    cmbTipoCosteo.Text = dr["TipoCosteo"].ToString();
                    txtCostoUnitario.Text = dr["CostoUnitario"].ToString();
                    cmbDivisa.Text = dr["Divisa"].ToString();
                    txtDescuentoPorc.Text = dr["DescuentoPorc"].ToString();
                    txtDescuentoCant.Text = dr["DescuentoCant"].ToString();
                    txtImpuestoPorc.Text = dr["ImpuestoPorc"].ToString();
                    txtImpuestoCant.Text = dr["ImpuestoCant"].ToString();
                    txtPrecioVenta.Text = dr["PrecioVenta"].ToString();

                    string Imagen = dr["Foto"].ToString();

                    if (Imagen != "")
                    {
                        byte[] datos = new byte[0];
                        datos = (byte[])dr["Foto"];

                        System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                        Foto.Image = System.Drawing.Bitmap.FromStream(ms);
                    }

                    txtCategoria.Text = dr["Categoria"].ToString();
                    txtFamilia.Text = dr["Familia"].ToString();
                    txtConcepto.Text = dr["ConceptoGlobales"].ToString();

                    string categoria = dr["Categoria"].ToString();
                    string familia = dr["Familia"].ToString();
                    string Concepto = dr["ConceptoGlobales"].ToString();
                    dr.Close();

                    cmd = new SqlCommand("Select * from Categorias where ClaveCategoria= " + categoria + "", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        cmbCategoria.Text = dr["Nombre"].ToString();
                    }
                    dr.Close();

                    cmd = new SqlCommand("Select * from Familias where ClaveFamilia= '" + familia + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        cmbFamilia.Text = dr["Nombre"].ToString();
                    }
                    dr.Close();

                    cmd = new SqlCommand("Select * from ConceptosGlobales where Clave= '" + Concepto + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        cmbConcepto.Text = dr["Nombre"].ToString();
                    }
                    else
                    {
                        cmbConcepto.Text = null;
                    }
                    dr.Close();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete Servicios where ClaveServicio='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
    }
}
