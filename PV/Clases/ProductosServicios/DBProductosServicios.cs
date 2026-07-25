using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PuntoVentas.Clases.ProductosServicios
{
    class DBProductosServicios
    {
        // NOTA: se eliminaron los campos de instancia (cn, cmd, dr, da, dt) que se
        // compartían entre métodos. Esa práctica dejaba conexiones y DataReaders
        // abiertos (el constructor abría "cn" una sola vez y nunca se cerraba).
        // Ahora cada método abre su propia conexión dentro de un bloque "using",
        // por lo que se cierra y libera automáticamente, incluso si hay una excepción.
        //
        // Las firmas de los métodos, los parámetros de entrada y los valores de
        // retorno son EXACTAMENTE los mismos que en la clase original, así como
        // la lógica de negocio (mismas consultas, mismo orden de operaciones).

        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBProductosServicios()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Ahora cada método administra su propia
            // conexión, así que ya no es necesario abrir nada aquí. Se conserva
            // el constructor vacío para no romper código existente que hace
            // "new DBProductosServicios()".
        }

        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveProductoSiguiente()
        {
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select max(ClaveProducto) from ProductosServicios", cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                contador++;
                            }
                        }

                        if (contador > 0)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows[0][0].ToString() != string.Empty)
                                {
                                    Folio = Convert.ToInt32(dt.Rows[0][0].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }

        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public string RegistroProducto(
      string txtClaveProducto,
      string txtAlias,
      string txtDescripcion,
      string cmbEstatus,
      string txtMarca,
      string txtUnidadMedida,
      string txtPresentacion,
      Guna2ToggleSwitch tgInventariable,
      string txtCaducidad,
      string txtCategoria,
      string txtFamilia,
      string txtProveedor,
      string txtExMinimo,
      string txtExMaximo,
      string txtExActual,
      string txtUbicacion,
      string cmbTipoCosteo,
      string txtCostoUnitario,
      string cmbDivisa,
      string txtDescuentoPorc,
      string txtDescuentoCant,
      string txtImpuestoPorc,
      string txtImpuestoCant,
      string txtPrecioVenta,
      PictureBox Foto,
      string Concepto)
        {
            try
            {
                string inventariable = tgInventariable.Checked ? "Si" : "No";

                byte[] fotoBytes = null;

                if (Foto != null && Foto.Image != null)
                {
                    using (Bitmap bmp = new Bitmap(Foto.Image))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bmp.Save(ms, ImageFormat.Jpeg);
                            fotoBytes = ms.ToArray();
                        }
                    }
                }

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    bool existe;

                    using (SqlCommand cmdExiste = new SqlCommand(
                        "SELECT COUNT(1) FROM ProductosServicios WHERE ClaveProducto=@ClaveProducto", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar()) > 0;
                    }

                    if (existe)
                    {
                        if (MessageBox.Show("¿Desea actualizar el registro actual?",
                            "Datos de la Tienda",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return "";
                        }
                    }

                    string sql = existe
                        ? @"UPDATE ProductosServicios SET
                    Alias=@Alias,
                    Descripcion=@Descripcion,
                    Estatus=@Estatus,
                    Marca=@Marca,
                    UnidadMedida=@UnidadMedida,
                    Presentacion=@Presentacion,
                    Inventariable=@Inventariable,
                    Caducidad=@Caducidad,
                    Categoria=@Categoria,
                    Familia=@Familia,
                    Proveedor=@Proveedor,
                    ExMinimo=@ExMinimo,
                    ExMaximo=@ExMaximo,
                    ExActual=@ExActual,
                    Ubicacion=@Ubicacion,
                    TipoCosteo=@TipoCosteo,
                    CostoUnitario=@CostoUnitario,
                    Divisa=@Divisa,
                    DescuentoPorc=@DescuentoPorc,
                    DescuentoCant=@DescuentoCant,
                    ImpuestoPorc=@ImpuestoPorc,
                    ImpuestoCant=@ImpuestoCant,
                    PrecioVenta=@PrecioVenta,
                    Foto=@Foto,
                    ConceptoGlobales=@Concepto
              WHERE ClaveProducto=@ClaveProducto"
                        : @"INSERT INTO ProductosServicios
                (
                    ClaveProducto,
                    Alias,
                    Descripcion,
                    Estatus,
                    Marca,
                    UnidadMedida,
                    Presentacion,
                    Inventariable,
                    Caducidad,
                    Categoria,
                    Familia,
                    Proveedor,
                    ExMinimo,
                    ExMaximo,
                    ExActual,
                    Ubicacion,
                    TipoCosteo,
                    CostoUnitario,
                    Divisa,
                    DescuentoPorc,
                    DescuentoCant,
                    ImpuestoPorc,
                    ImpuestoCant,
                    PrecioVenta,
                    Foto,
                    ConceptoGlobales
                )
                VALUES
                (
                    @ClaveProducto,
                    @Alias,
                    @Descripcion,
                    @Estatus,
                    @Marca,
                    @UnidadMedida,
                    @Presentacion,
                    @Inventariable,
                    @Caducidad,
                    @Categoria,
                    @Familia,
                    @Proveedor,
                    @ExMinimo,
                    @ExMaximo,
                    @ExActual,
                    @Ubicacion,
                    @TipoCosteo,
                    @CostoUnitario,
                    @Divisa,
                    @DescuentoPorc,
                    @DescuentoCant,
                    @ImpuestoPorc,
                    @ImpuestoCant,
                    @PrecioVenta,
                    @Foto,
                    @Concepto)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                        cmd.Parameters.AddWithValue("@Alias", txtAlias);
                        cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                        cmd.Parameters.AddWithValue("@Estatus", cmbEstatus);
                        cmd.Parameters.AddWithValue("@Marca", txtMarca);
                        cmd.Parameters.AddWithValue("@UnidadMedida", txtUnidadMedida);
                        cmd.Parameters.AddWithValue("@Presentacion", txtPresentacion);
                        cmd.Parameters.AddWithValue("@Inventariable", inventariable);
                        cmd.Parameters.AddWithValue("@Caducidad", txtCaducidad);
                        cmd.Parameters.AddWithValue("@Categoria", txtCategoria);
                        cmd.Parameters.AddWithValue("@Familia", txtFamilia);
                        cmd.Parameters.AddWithValue("@Proveedor", txtProveedor);
                        cmd.Parameters.AddWithValue("@ExMinimo", txtExMinimo);
                        cmd.Parameters.AddWithValue("@ExMaximo", txtExMaximo);
                        cmd.Parameters.AddWithValue("@ExActual", txtExActual);
                        cmd.Parameters.AddWithValue("@Ubicacion", txtUbicacion);
                        cmd.Parameters.AddWithValue("@TipoCosteo", cmbTipoCosteo);
                        cmd.Parameters.AddWithValue("@CostoUnitario", txtCostoUnitario);
                        cmd.Parameters.AddWithValue("@Divisa", cmbDivisa);
                        cmd.Parameters.AddWithValue("@DescuentoPorc", txtDescuentoPorc);
                        cmd.Parameters.AddWithValue("@DescuentoCant", txtDescuentoCant);
                        cmd.Parameters.AddWithValue("@ImpuestoPorc", txtImpuestoPorc);
                        cmd.Parameters.AddWithValue("@ImpuestoCant", txtImpuestoCant);
                        cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta);
                        cmd.Parameters.AddWithValue("@Concepto", Concepto);

                        SqlParameter foto = cmd.Parameters.Add("@Foto", SqlDbType.VarBinary);

                        if (fotoBytes == null)
                            foto.Value = DBNull.Value;
                        else
                            foto.Value = fotoBytes;

                        cmd.ExecuteNonQuery();
                    }

                    return existe ? "Registro modificado." : "Registro guardado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return "";
            }
        }

        public string RegistroPedidosProveedor(string txtClaveProducto, string Pedido)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios Set PedidosProveedor=PedidosProveedor+@Pedido Where claveProducto=@Clave", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Pedido", Pedido);
                    cmd.Parameters.AddWithValue("@Clave", txtClaveProducto);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro Eliminado";
                }
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }

        public string RegistroPedidosCliente(string txtClaveProducto, string Pedido)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios Set PedidosCliente=PedidosCliente+@Pedido Where claveProducto=@Clave", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Pedido", Pedido);
                    cmd.Parameters.AddWithValue("@Clave", txtClaveProducto);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro Eliminado";
                }
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
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
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select * from Categorias", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }

        //______________________________________________________________________________________________________________________________
        public void SeleccionarConceptoGlobal(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from ConceptosGlobales where Clase='Impuesto'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }

        //______________________________________________________________________________________________________________________________
        public void SeleccionarFamilia(ComboBox cb, string ClaveCategoria)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Familias where ClaveCategoria= " + ClaveCategoria + "", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionCategorias(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Categorias where Nombre = '" + nombre + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] Informacion(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Categorias where Nombre = '" + nombre + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionConcepto(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from ConceptosGlobales where Nombre = '" + nombre + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionCatergoria(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select ClaveCategoria from Categorias where Nombre = '" + nombre + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionFamilia(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Familias where Nombre = '" + nombre + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Divisas", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }

        //______________________________________________________________________________________________________________________________
        public void CargarFamilias(ComboBox cb, string claveFamilia)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select Nombre from Familias where ClaveCategoria='" + claveFamilia + "' and Vincular in ('Ambos','Productos')", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        //________________________________________________________________________________________________
        //tiendas Registrados
        public void CargarProductos(DataGridView dgv, string filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select * from ProductosServicios where descripcion like '%" + filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["ClaveProducto"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaProductoSeleccionado(string txtClaveProducto, Guna2TextBox txtAlias, Guna2TextBox txtDescripcion, ComboBox cmbEstatus, Guna2TextBox txtMarca, Guna2TextBox txtUnidadMedida, Guna2TextBox txtPresentacion, Guna2ToggleSwitch tgInventariable, Guna2TextBox txtCaducidad, TextBox txtCategoria, TextBox txtFamilia, ComboBox cmbCategoria, ComboBox cmbFamilia, Guna2TextBox txtProveedor, Guna2TextBox txtExMinimo, Guna2TextBox txtExMaximo, Guna2TextBox txtExActual, Guna2TextBox txtUbicacion, ComboBox cmbTipoCosteo, Guna2TextBox txtCostoUnitario, ComboBox cmbDivisa, Guna2TextBox txtDescuentoPorc, Guna2TextBox txtDescuentoCant, Guna2TextBox txtImpuestoPorc, Guna2TextBox txtImpuestoCant, Guna2TextBox txtPrecioVenta, PictureBox Foto, TextBox txtConcepto, ComboBox cmbConcepto, Guna2TextBox txtPedidosProveedor, Guna2TextBox txtPedidosCliente)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductosServicios WHERE ClaveProducto = @ClaveProducto", cn))
                    {
                        cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Asignar valores a los controles
                                txtAlias.Text = dr["Alias"].ToString();
                                txtDescripcion.Text = dr["Descripcion"].ToString();
                                cmbEstatus.Text = dr["Estatus"].ToString();
                                txtMarca.Text = dr["Marca"].ToString();
                                txtUnidadMedida.Text = dr["UnidadMedida"].ToString();
                                txtPresentacion.Text = dr["Presentacion"].ToString();
                                tgInventariable.Checked = dr["Inventariable"].ToString() == "Si";
                                txtCaducidad.Text = dr["Caducidad"].ToString();
                                txtProveedor.Text = dr["Proveedor"].ToString();
                                txtExMinimo.Text = dr["ExMinimo"].ToString();
                                txtExMaximo.Text = dr["ExMaximo"].ToString();
                                txtExActual.Text = dr["ExActual"].ToString();
                                txtUbicacion.Text = dr["Ubicacion"].ToString();
                                cmbTipoCosteo.Text = dr["TipoCosteo"].ToString();
                                txtCostoUnitario.Text = dr["CostoUnitario"].ToString();
                                cmbDivisa.Text = dr["Divisa"].ToString();
                                txtDescuentoPorc.Text = dr["DescuentoPorc"].ToString();
                                txtDescuentoCant.Text = dr["DescuentoCant"].ToString();
                                txtImpuestoPorc.Text = dr["ImpuestoPorc"].ToString();
                                txtImpuestoCant.Text = dr["ImpuestoCant"].ToString();
                                txtPrecioVenta.Text = dr["PrecioVenta"].ToString();

                                // Cargar imagen si existe
                                if (dr["Foto"] != DBNull.Value)
                                {
                                    byte[] datos = (byte[])dr["Foto"];
                                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(datos))
                                    {
                                        Foto.Image = System.Drawing.Bitmap.FromStream(ms);
                                    }
                                }

                                // Asignar categorías, familias y conceptos
                                txtCategoria.Text = dr["Categoria"].ToString();
                                txtFamilia.Text = dr["Familia"].ToString();
                                txtConcepto.Text = dr["ConceptoGlobales"].ToString();
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Select * from Categorias where ClaveCategoria= " + txtCategoria.Text + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cmbCategoria.Text = dr["Nombre"].ToString();
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Select * from Familias where ClaveFamilia= '" + txtFamilia.Text + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cmbFamilia.Text = dr["Nombre"].ToString();
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Select * from ConceptosGlobales where Clave= '" + txtConcepto.Text + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cmbConcepto.Text = dr["Nombre"].ToString();
                        }
                        else
                        {
                            cmbConcepto.Text = null;
                        }
                    }

                    // Consultar PedidosProveedor y PedidosCliente
                    using (SqlCommand cmdPedidos = new SqlCommand("SELECT PedidosProveedor, PedidosCliente FROM ProductosServicios WHERE ClaveProducto = @ClaveProducto", cn))
                    {
                        cmdPedidos.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                        using (SqlDataReader drPedidos = cmdPedidos.ExecuteReader())
                        {
                            if (drPedidos.Read())
                            {
                                txtPedidosProveedor.Text = drPedidos["PedidosProveedor"].ToString();
                                txtPedidosCliente.Text = drPedidos["PedidosCliente"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete ProductosServicios where ClaveProducto='" + txtClaveDivisa + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro Eliminado";
                }
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }

        public string EliminarOrdenCliente(string Folio, string Partida)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update  P  Set PedidosCliente=P.PedidosCliente-PO.Cantidad From ProductosServicios as P Join PartidaOrdenPedidoCliente as PO ON P.claveProducto=PO.ClaveProducto Where PO.FolioOrden=@Folio and PO.Partida=@Partida", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Partida", Partida);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro Eliminado";
                }
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
    }
}