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
        // Se agregan "compras" y "ventas" (tgCompras/tgVentas en el form):
        // indican si el servicio aparece en el catalogo de Compras, en el
        // de Ventas, en ambos, o en ninguno. Son independientes entre si.
        public string RegistroProducto(string txtClaveProducto, string txtAlias, string txtDescripcion, string cmbEstatus, string txtCategoria, string txtFamilia, string cmbTipoCosteo, string txtCostoUnitario, string cmbDivisa, string txtDescuentoPorc, string txtDescuentoCant, string txtImpuestoPorc, string txtImpuestoCant, string txtPrecioVenta, PictureBox Foto, string Concepto, string IEPS, string CuentaContable, bool compras, bool ventas)
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
                        cmd = new SqlCommand("Insert into Servicios (ClaveServicio, Alias, Descripcion, Estatus, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, ConceptoGlobales,IEPS, CuentaContable, Compras, Ventas) values (@ClaveServicio, @Alias, @Descripcion, @Estatus, @Categoria, @Familia, @Proveedor, @ExMinimo, @ExMaximo, @ExActual, @Ubicacion, @TipoCosteo, @CostoUnitario, @Divisa, @DescuentoPorc, @DescuentoCant, @ImpuestoPorc, @ImpuestoCant, @PrecioVenta, @ConceptoGlobales,@IEPS, @CuentaContable, @Compras, @Ventas)", cn);

                        // Añadir parámetros
                        cmd.Parameters.AddWithValue("@ClaveServicio", txtClaveProducto);
                        cmd.Parameters.AddWithValue("@Alias", txtAlias);
                        cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                        cmd.Parameters.AddWithValue("@Estatus", cmbEstatus);
                        cmd.Parameters.AddWithValue("@Categoria", txtCategoria);
                        cmd.Parameters.AddWithValue("@Familia", txtFamilia);
                        cmd.Parameters.AddWithValue("@Proveedor", 0);  // Asegúrate de agregar el proveedor, que falta en el query original
                        cmd.Parameters.AddWithValue("@ExMinimo", 0);     // Estos campos faltan también
                        cmd.Parameters.AddWithValue("@ExMaximo", 0);
                        cmd.Parameters.AddWithValue("@ExActual", 0);
                        cmd.Parameters.AddWithValue("@Ubicacion", "");   // Este también está faltando
                        cmd.Parameters.AddWithValue("@TipoCosteo", cmbTipoCosteo);
                        cmd.Parameters.AddWithValue("@CostoUnitario", txtCostoUnitario);
                        cmd.Parameters.AddWithValue("@Divisa", cmbDivisa);
                        cmd.Parameters.AddWithValue("@DescuentoPorc", txtDescuentoPorc);
                        cmd.Parameters.AddWithValue("@DescuentoCant", txtDescuentoCant);
                        cmd.Parameters.AddWithValue("@ImpuestoPorc", txtImpuestoPorc);
                        cmd.Parameters.AddWithValue("@ImpuestoCant", txtImpuestoCant);
                        cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta);
                        cmd.Parameters.AddWithValue("@ConceptoGlobales", Concepto);
                        cmd.Parameters.AddWithValue("@IEPS", IEPS);
                        cmd.Parameters.AddWithValue("@CuentaContable", CuentaContable);
                        cmd.Parameters.AddWithValue("@Compras", compras ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Ventas", ventas ? 1 : 0);
                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Servicios (ClaveServicio, Alias, Descripcion, Estatus, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, ConceptoGlobales, IEPS, CuentaContable, Compras, Ventas) values (@ClaveServicio, @Alias, @Descripcion, @Estatus, @Categoria, @Familia, @Proveedor, @ExMinimo, @ExMaximo, @ExActual, @Ubicacion, @TipoCosteo, @CostoUnitario, @Divisa, @DescuentoPorc, @DescuentoCant, @ImpuestoPorc, @ImpuestoCant, @PrecioVenta, @ConceptoGlobales, @IEPS, @CuentaContable, @Compras, @Ventas)", cn);

                        // Añadir parámetros
                        cmd.Parameters.AddWithValue("@ClaveServicio", txtClaveProducto);
                        cmd.Parameters.AddWithValue("@Alias", txtAlias);
                        cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                        cmd.Parameters.AddWithValue("@Estatus", cmbEstatus);
                        cmd.Parameters.AddWithValue("@Categoria", txtCategoria);
                        cmd.Parameters.AddWithValue("@Familia", txtFamilia);
                        cmd.Parameters.AddWithValue("@Proveedor", 0);  // Asegúrate de agregar el proveedor, que falta en el query original
                        cmd.Parameters.AddWithValue("@ExMinimo", 0);     // Estos campos faltan también
                        cmd.Parameters.AddWithValue("@ExMaximo", 0);
                        cmd.Parameters.AddWithValue("@ExActual", 0);
                        cmd.Parameters.AddWithValue("@Ubicacion", "");   // Este también está faltando
                        cmd.Parameters.AddWithValue("@TipoCosteo", cmbTipoCosteo);
                        cmd.Parameters.AddWithValue("@CostoUnitario", txtCostoUnitario);
                        cmd.Parameters.AddWithValue("@Divisa", cmbDivisa);
                        cmd.Parameters.AddWithValue("@DescuentoPorc", txtDescuentoPorc);
                        cmd.Parameters.AddWithValue("@DescuentoCant", txtDescuentoCant);
                        cmd.Parameters.AddWithValue("@ImpuestoPorc", txtImpuestoPorc);
                        cmd.Parameters.AddWithValue("@ImpuestoCant", txtImpuestoCant);
                        cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta);
                        cmd.Parameters.AddWithValue("@ConceptoGlobales", Concepto);
                        cmd.Parameters.AddWithValue("@IEPS", IEPS);
                        cmd.Parameters.AddWithValue("@CuentaContable", CuentaContable);
                        cmd.Parameters.AddWithValue("@Compras", compras ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Ventas", ventas ? 1 : 0);

                        // Preparar el parámetro de imagen
                        cmd.Parameters.Add("@Foto", SqlDbType.Image);

                        // Guardar la imagen en un MemoryStream
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            cmd.Parameters["@Foto"].Value = ms.ToArray(); // Usar ToArray() para obtener los bytes exactos
                        }

                        // Ejecutar el comando
                        cmd.ExecuteNonQuery();


                    }
                    mensaje = "Registro guardado.";
                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Inventariable = string.Empty;

                        string valorCompras = compras ? "1" : "0";
                        string valorVentas = ventas ? "1" : "0";

                        if (Foto.Image == null)
                        {
                            cmd = new SqlCommand("Update Servicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Inventariable='" + Inventariable + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', ConceptoGlobales='" + Concepto + "', IEPS = '" + IEPS + "', CuentaContable='" + CuentaContable + "', Compras=" + valorCompras + ", Ventas=" + valorVentas + " where ClaveServicio= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Servicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Inventariable='" + Inventariable + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', Foto=@Foto, ConceptoGlobales='" + Concepto + "', IEPS = '" + IEPS + "', CuentaContable='" + CuentaContable + "', Compras=" + valorCompras + ", Ventas=" + valorVentas + " where ClaveServicio= '" + txtClaveProducto + "'", cn);
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
            cmd = new SqlCommand("Select * from Familias where ClaveCategoria= " + ClaveCategoria + " and Vincular in ('Ambos','Productos','Servicios')", cn);
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
        // Se agregan tgCompras/tgVentas para reflejar en el form si el
        // servicio esta habilitado en Compras y/o en Ventas.
        public void ConsultaProductoSeleccionado(string txtClaveProducto, Guna2TextBox txtAlias, Guna2TextBox txtDescripcion, ComboBox cmbEstatus, TextBox txtCategoria, TextBox txtFamilia, ComboBox cmbCategoria, ComboBox cmbFamilia, ComboBox cmbTipoCosteo, Guna2TextBox txtCostoUnitario, ComboBox cmbDivisa, Guna2TextBox txtDescuentoPorc, Guna2TextBox txtDescuentoCant, Guna2TextBox txtImpuestoPorc, Guna2TextBox txtImpuestoCant, Guna2TextBox txtPrecioVenta, PictureBox Foto, TextBox txtConcepto, ComboBox cmbConcepto, ComboBox cmbIEPS, Guna2TextBox txtCuentaContable, Guna2ToggleSwitch tgCompras, Guna2ToggleSwitch tgVentas)
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
                    cmbIEPS.Text = dr["IEPS"].ToString();
                    txtCuentaContable.Text = dr["CuentaContable"].ToString();
                    tgCompras.Checked = dr["Compras"] != DBNull.Value && Convert.ToBoolean(dr["Compras"]);
                    tgVentas.Checked = dr["Ventas"] != DBNull.Value && Convert.ToBoolean(dr["Ventas"]);
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
        public DataTable ObtenerProductosGasto()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ClaveServicio, Descripcion FROM Servicios WHERE Estatus = 'Activo' and Ventas=1 ORDER BY Descripcion", cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos de gasto: " + ex.Message);
            }
            return dt;
        }

        public DataTable ObtenerProductosGastoPorOrden(string folioOrden)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT P.ClaveProducto AS ClaveServicio, P.Descripcion as Descripcion
                         FROM PartidaOrden AS PA
                         INNER JOIN Servicios AS P ON PA.ClaveProducto = P.ClaveProducto
                         WHERE PA.FolioOrden = @FolioOrden
                         AND PA.CantidadRecibida > 0
                         AND P.Estatus = 'Activo'
                         ORDER BY P.Descripcion";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@FolioOrden", folioOrden);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos por orden: " + ex.Message);
            }
            return dt;
        }

        //_________________________________________________________________________________________________________________________--
        // Información de un servicio para precargar la captura de partidas
        // (usado por Facturas.cs). Sustituye a DBPedidoCliente.InformacionRecibo,
        // que consultaba Productos/Servicios + Almacenes; aquí se consulta
        // únicamente la tabla Servicios, sin existencias ni pedidos de
        // almacén, ya que Facturas dejó de manejar esa lógica.
        //
        // Se busca por Descripcion porque así es como cmbConcepto expone su
        // .Text (DisplayMember = "Descripcion"). Se usa un SqlDataReader
        // local (no el campo de clase "dr") para no interferir con otros
        // métodos de esta clase que reutilizan ese campo.
        //
        // Índices del arreglo devuelto:
        //   [0] Descripcion  (concepto amplio -> txtConcepto2)
        //   [1] ClaveServicio(clave           -> txtConcepto)
        //   [2] PrecioVenta  (precio unitario  -> txtPrecio)
        //   [3] Unidad       (fija "Servicio", la tabla Servicios no maneja unidad)
        //   [4] ImpuestoPorc (%                -> txtImpuesto1)
        //   [5] DescuentoPorc(%                -> txtDescuento1)
        // Devuelve null si no encuentra el servicio o si ocurre un error.
        public string[] InformacionServicio(string descripcion)
        {
            string[] resultado = null;

            try
            {
                using (SqlCommand comando = new SqlCommand(
                    "SELECT ClaveServicio, Descripcion, PrecioVenta, ImpuestoPorc, DescuentoPorc " +
                    "FROM Servicios WHERE Descripcion = @Descripcion", cn))
                {
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            resultado = new string[]
                            {
                                lector["Descripcion"].ToString(),
                                lector["ClaveServicio"].ToString(),
                                lector["PrecioVenta"].ToString(),
                                "Servicio",
                                lector["ImpuestoPorc"].ToString(),
                                lector["DescuentoPorc"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la información del servicio: " + ex.ToString());
            }

            return resultado;
        }
    }
}