using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Inventario
{
    class DBPartidas
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Eliminado = 0;
        public static int Cantidad = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBPartidas()
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
        //_________________________________________________________________________________________________________________________
        // registrar forma partida 
        public void RegistroPartida(string txtFolioMovimiento, string txtTipoDocumento, string txtDescripcion, string txtNoPartida, string cmbClaveProducto, string txtCantidad, string txtunidad, decimal txtPrecio, string cmbDivisa, string txtTipoCambio, decimal txtTotal, string Concepto)
        {
            try
            {

                cmd = new SqlCommand("Insert into PartidasMovimientoInventario (FolioMovimiento, TipoDocumento, Descripcion, NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto) values ('" + txtFolioMovimiento + "', '" + txtTipoDocumento + "', '" + txtDescripcion + "','" + txtNoPartida + "', '" + cmbClaveProducto + "', '" + txtCantidad + "','" + txtunidad + "', " + txtPrecio + ", '" + cmbDivisa + "', '" + txtTipoCambio + "', " + txtTotal + ", '" + Concepto + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        // registrar forma partida 
        public void ConsultarPartida(string FolioMovimiento, string TipoDocumento, string Descripcion, string NoPartida, Guna.UI2.WinForms.Guna2TextBox ClaveProducto, Guna.UI2.WinForms.Guna2TextBox Cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox Precio, Label Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Concepto)
        {
            try
            {
                cmd = new SqlCommand("select   NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto from PartidasMovimientoInventario where FolioMovimiento = '"+FolioMovimiento+"' and TipoDocumento = '"+TipoDocumento+"' and Descripcion = '"+Descripcion+ "' and NoPartida='"+NoPartida+"'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    
                 //   NoPartida.Text = dr["NoPartida"].ToString();
                    ClaveProducto.Text = dr["ClaveProducto"].ToString();
                    Cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["unidad"].ToString();
                    Precio.Text = dr["Precio"].ToString();
                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Concepto.Text = dr["Concepto"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
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
        public void SeleccionarProducto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from ProductosServicios where Inventariable = 'Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionProducto(string Documento)
        {
            cmd = new SqlCommand("Select * from ProductosServicios where ClaveProducto = " + Documento + "", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[14].ToString(),
                    dr[1].ToString(),
                     dr[5].ToString(),
                       dr[17].ToString(),
                        dr[16].ToString(),
                        dr[18].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionProductoAlmacen(string Documento, string Almacen)
        {
            cmd = new SqlCommand("Select convert(int, ExistenciaActual) from AlmacenProducto where ClaveProducto = " + Documento + " and ClaveAlmacen='"+Almacen+"'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                };
                resultado = valores;
                Cantidad = 1;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDivisa(string Nombre)
        {
            cmd = new SqlCommand("Select TipoCambio from Divisas where Nombre= '" + Nombre + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public void RegistroProducto(string txtClaveProducto, string txtExActual, string txtAlmacen, string costeo, decimal precio, string TipoCosteo)
        {
            int contador = 0;
            int ExActual = 0;
            decimal Total = 0;
            decimal GranTotal = 0;

            dr.Close();
            try
            {
                cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "', '" + txtExActual + "',  '" + txtExActual + "', '0','" + txtExActual + "')", cn);
                    cmd.ExecuteNonQuery();

                    if (costeo == "Si")
                    {
                        if (TipoCosteo == "Ultima Compra")
                        {
                            cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                        else if (TipoCosteo == "Promedio")
                        {
                            cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn);
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                 ExActual = Convert.ToInt32(dr["ExActual"].ToString()) + Convert.ToInt32(txtExActual);
                                 Total = Convert.ToDecimal(dr["Total"].ToString()) + precio;
                                 GranTotal = Total / ExActual;
                            }
                            else
                            {
                                 ExActual = Convert.ToInt32(txtExActual);
                                 Total = precio;
                                 GranTotal = Total;
                            }
                            dr.Close();

                            cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("Update AlmacenProducto set ExistenciaInicial= ExistenciaActual, Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);
                    cmd.ExecuteNonQuery();

                    if (costeo == "Si")
                    {
                        if (TipoCosteo == "Ultima Compra")
                        {
                            cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                        else if (TipoCosteo == "Promedio")
                        {
                            cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn);
                            dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                ExActual = Convert.ToInt32(dr["ExActual"].ToString()) + Convert.ToInt32(txtExActual);
                                Total = Convert.ToDecimal(dr["Total"].ToString()) + precio;
                                GranTotal = Total / ExActual;
                            }
                            else
                            {
                                ExActual = Convert.ToInt32(txtExActual);
                                Total = precio;
                                GranTotal = Total;
                            }
                            dr.Close();

                            cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error aqui." + ex.ToString());
            }

        }

        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public void RegistroProductoSalidas(string txtClaveProducto, string txtExActual, string txtAlmacen)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "',  '" + txtExActual + "', '0','" + txtExActual + "', '" + txtExActual + "')", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("Update AlmacenProducto set ExistenciaInicial= ExistenciaActual, Salidas= Salidas + '" + txtExActual + "', ExistenciaActual= ExistenciaActual - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
  
        public string[] SeleccionarProducto2(string clave)
        {
            cmd = new SqlCommand("select * from ProductosServicios where ClaveProducto='" + clave + "' and Inventariable ='Si'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString()+ '-' + dr[2].ToString() + ' ' + dr[4].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;

        }

        public void SeleccionarProducto3(Guna2ComboBox cb,string clave)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select * from ProductosServicios where ClaveProducto='" + clave + "' and Inventariable ='Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
            }
            dr.Close();
        }
        public string Eliminarpartida(string FolioM,string TipoD ,string Descripcion ,string EliminarP)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("delete PartidasMovimientoInventario where  FolioMovimiento='"+FolioM+ "' and tipodocumento='"+TipoD+ "' and Descripcion='"+Descripcion+ "' and NoPartida='"+EliminarP+"'", cn);
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
