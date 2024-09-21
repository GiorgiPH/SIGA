using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PuntoVentas.Clases.ProductosServicios
{
    class DBProductosServicios
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

        public DBProductosServicios()
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
                cmd = new SqlCommand("select max(ClaveProducto) from ProductosServicios", cn);
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
        public string RegistroProducto(string txtClaveProducto, string txtAlias, string txtDescripcion, string cmbEstatus, string txtMarca, string txtUnidadMedida, string txtPresentacion, Guna2ToggleSwitch tgInventariable, string txtCaducidad, string txtCategoria, string txtFamilia, string txtProveedor, string txtExMinimo, string txtExMaximo, string txtExActual, string txtUbicacion, string cmbTipoCosteo, string txtCostoUnitario, string cmbDivisa, string txtDescuentoPorc, string txtDescuentoCant, string txtImpuestoPorc, string txtImpuestoCant, string txtPrecioVenta, PictureBox Foto, string Concepto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ProductosServicios where ClaveProducto='" + txtClaveProducto + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string Inventariable = string.Empty;

                    if (tgInventariable.Checked == true)
                    {
                        Inventariable = "Si";
                    }
                    else
                    {
                        Inventariable = "No";
                    }

                    if (Foto.Image == null)
                    {
                        cmd = new SqlCommand("Insert into ProductosServicios (ClaveProducto, Alias, Descripcion, Estatus, Marca, UnidadMedida, Presentacion, Inventariable, Caducidad, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, ConceptoGlobales) values ('" + txtClaveProducto + "', '" + txtAlias + "',  '" + txtDescripcion + "',  '" + cmbEstatus + "', '" + txtMarca + "',  '" + txtUnidadMedida + "',  '" + txtPresentacion + "', '" + Inventariable + "',  '" + txtCaducidad + "',  '" + txtCategoria + "','" + txtFamilia + "', '" + txtProveedor + "',  '" + txtExMinimo + "',  '" + txtExMaximo + "', '" + txtExActual + "',  '" + txtUbicacion + "',  '" + cmbTipoCosteo + "', '" + txtCostoUnitario + "',  '" + cmbDivisa + "',  '" + txtDescuentoPorc + "', '" + txtDescuentoCant + "',  '" + txtImpuestoPorc + "',  '" + txtImpuestoCant + "', '" + txtPrecioVenta + "', '" + Concepto + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into ProductosServicios (ClaveProducto, Alias, Descripcion, Estatus, Marca, UnidadMedida, Presentacion, Inventariable, Caducidad, Categoria, Familia, Proveedor, ExMinimo, ExMaximo, ExActual, Ubicacion, TipoCosteo, CostoUnitario, Divisa, DescuentoPorc, DescuentoCant, ImpuestoPorc, ImpuestoCant, PrecioVenta, Foto, ConceptoGlobales) values ('" + txtClaveProducto + "', '" + txtAlias + "',  '" + txtDescripcion + "',  '" + cmbEstatus + "', '" + txtMarca + "',  '" + txtUnidadMedida + "',  '" + txtPresentacion + "', '" + Inventariable + "',  '" + txtCaducidad + "',  '" + txtCategoria + "','" + txtFamilia + "', '" + txtProveedor + "',  '" + txtExMinimo + "',  '" + txtExMaximo + "', '" + txtExActual + "',  '" + txtUbicacion + "',  '" + cmbTipoCosteo + "', '" + txtCostoUnitario + "',  '" + cmbDivisa + "',  '" + txtDescuentoPorc + "', '" + txtDescuentoCant + "',  '" + txtImpuestoPorc + "',  '" + txtImpuestoCant + "', '" + txtPrecioVenta + "', @Foto, '" + Concepto + "')", cn);
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

                        if (tgInventariable.Checked == true)
                        {
                            Inventariable = "Si";
                        }
                        else
                        {
                            Inventariable = "No";
                        }

                        if (Foto.Image == null)
                        {
                            cmd = new SqlCommand("Update ProductosServicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Marca='" + txtMarca + "', UnidadMedida='" + txtUnidadMedida + "', Presentacion='" + txtPresentacion + "', Inventariable='" + Inventariable + "', Caducidad='" + txtCaducidad + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', Proveedor='" + txtProveedor + "', ExMinimo='" + txtExMinimo + "', ExMaximo='" + txtExMaximo + "', ExActual='" + txtExActual + "', Ubicacion='" + txtUbicacion + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', ConceptoGlobales='" + Concepto + "' where ClaveProducto= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("Update ProductosServicios set Alias='" + txtAlias + "', Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', Marca='" + txtMarca + "', UnidadMedida='" + txtUnidadMedida + "', Presentacion='" + txtPresentacion + "', Inventariable='" + Inventariable + "', Caducidad='" + txtCaducidad + "', Categoria= '" + txtCategoria + "', Familia= '" + txtFamilia + "', Proveedor='" + txtProveedor + "', ExMinimo='" + txtExMinimo + "', ExMaximo='" + txtExMaximo + "', ExActual='" + txtExActual + "', Ubicacion='" + txtUbicacion + "', TipoCosteo='" + cmbTipoCosteo + "', CostoUnitario='" + txtCostoUnitario + "', Divisa='" + cmbDivisa + "', DescuentoPorc='" + txtDescuentoPorc + "', DescuentoCant='" + txtDescuentoCant + "', ImpuestoPorc='" + txtImpuestoPorc + "', ImpuestoCant='" + txtImpuestoCant + "', PrecioVenta='" + txtPrecioVenta + "', Foto=@Foto, ConceptoGlobales='" + Concepto + "' where ClaveProducto= '" + txtClaveProducto + "'", cn);
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
        public string RegistroPedidosProveedor(string txtClaveProducto, string Pedido)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("Update ProductosServicios Set PedidosProveedor=PedidosProveedor+@Pedido Where claveProducto=@Clave", cn);
                cmd.Parameters.AddWithValue("@Pedido", Pedido);
                cmd.Parameters.AddWithValue("@Clave", txtClaveProducto);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
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
            int contador = 0;

            try
            {
                cmd = new SqlCommand("Update ProductosServicios Set PedidosCliente=PedidosCliente+@Pedido Where claveProducto=@Clave", cn);
                cmd.Parameters.AddWithValue("@Pedido", Pedido);
                cmd.Parameters.AddWithValue("@Clave", txtClaveProducto);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;

        }
        public string RegistroOrdenCliente(string txtClaveProducto, string Pedido)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("Update ProductosServicios Set PedidosCliente=PedidosCliente+@Pedido Where claveProducto=@Clave", cn);
                cmd.Parameters.AddWithValue("@Pedido", Pedido);
                cmd.Parameters.AddWithValue("@Clave", txtClaveProducto);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
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
            cmd = new SqlCommand("Select * from Familias where ClaveCategoria= " + ClaveCategoria + "", cn);
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
        public string[] Informacion(string nombre)
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
        public string[] InformacionCatergoria(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("Select ClaveCategoria from Categorias where Nombre = '" + nombre + "'", cn);
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

        //______________________________________________________________________________________________________________________________
        public void CargarFamilias(ComboBox cb, string claveFamilia)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select Nombre from Familias where ClaveCategoria='"+ claveFamilia + "' and Vincular in ('Ambos','Productos')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
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
                da = new SqlDataAdapter("Select * from ProductosServicios", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveProducto"].ToString();
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
        public void ConsultaProductoSeleccionado(string txtClaveProducto, Guna2TextBox txtAlias, Guna2TextBox txtDescripcion, ComboBox cmbEstatus, Guna2TextBox txtMarca, Guna2TextBox txtUnidadMedida, Guna2TextBox txtPresentacion, Guna2ToggleSwitch tgInventariable, Guna2TextBox txtCaducidad, TextBox txtCategoria, TextBox txtFamilia, ComboBox cmbCategoria, ComboBox cmbFamilia, Guna2TextBox txtProveedor, Guna2TextBox txtExMinimo, Guna2TextBox txtExMaximo, Guna2TextBox txtExActual, Guna2TextBox txtUbicacion, ComboBox cmbTipoCosteo, Guna2TextBox txtCostoUnitario, ComboBox cmbDivisa, Guna2TextBox txtDescuentoPorc, Guna2TextBox txtDescuentoCant, Guna2TextBox txtImpuestoPorc, Guna2TextBox txtImpuestoCant, Guna2TextBox txtPrecioVenta, PictureBox Foto, TextBox txtConcepto, ComboBox cmbConcepto, Guna2TextBox txtPedidosProveedor, Guna2TextBox txtPedidosCliente)
        {
            try
            {
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
                dr.Close();
                cmd = new SqlCommand("Select * from Categorias where ClaveCategoria= " + txtCategoria.Text + "", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmbCategoria.Text = dr["Nombre"].ToString();
                }
                dr.Close();

                cmd = new SqlCommand("Select * from Familias where ClaveFamilia= '" + txtFamilia.Text + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmbFamilia.Text = dr["Nombre"].ToString();
                }
                dr.Close();

                cmd = new SqlCommand("Select * from ConceptosGlobales where Clave= '" + txtConcepto.Text + "'", cn);
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
                cmd = new SqlCommand("Delete ProductosServicios where ClaveProducto='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception )
            {
               mensaje="El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
    }
}
