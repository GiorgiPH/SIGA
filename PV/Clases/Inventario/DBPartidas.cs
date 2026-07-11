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
        public static int Folio = 0;
        public static int Eliminado = 0;
        public static int Cantidad = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBPartidas()
        {
            // Ya no se abre una conexión compartida aquí.
            // Cada método abre y cierra su propia conexión con "using"
            // para evitar conexiones o DataReaders que se quedan abiertos.
        }

        //_________________________________________________________________________________________________________________________
        // registrar forma partida 
        public void RegistroPartida(string txtFolioMovimiento, string txtTipoDocumento, string txtDescripcion, string txtNoPartida, string cmbClaveProducto, string txtCantidad, string txtunidad, decimal txtPrecio, string cmbDivisa, string txtTipoCambio, decimal txtTotal, string Concepto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Insert into PartidasMovimientoInventario (FolioMovimiento, TipoDocumento, Descripcion, NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto) values ('" + txtFolioMovimiento + "', '" + txtTipoDocumento + "', '" + txtDescripcion + "','" + txtNoPartida + "', '" + cmbClaveProducto + "', '" + txtCantidad + "','" + txtunidad + "', " + txtPrecio + ", '" + cmbDivisa + "', '" + txtTipoCambio + "', " + txtTotal + ", '" + Concepto + "')", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select   NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto from PartidasMovimientoInventario where FolioMovimiento = '" + FolioMovimiento + "' and TipoDocumento = '" + TipoDocumento + "' and Descripcion = '" + Descripcion + "' and NoPartida='" + NoPartida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
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
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from Divisas", cn))
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
        public void SeleccionarProducto(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from ProductosServicios where Inventariable = 'Si'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
                    }
                }
            }
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionProducto(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from ProductosServicios where ClaveProducto = " + Documento + "", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionProductoAlmacen(string Documento, string Almacen)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select convert(int, ExistenciaActual) from AlmacenProducto where ClaveProducto = " + Documento + " and ClaveAlmacen='" + Almacen + "'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                             dr[0].ToString(),
                        };
                        resultado = valores;
                        Cantidad = 1;
                    }
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDivisa(string Nombre)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select TipoCambio from Divisas where Nombre= '" + Nombre + "'", cn))
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
            }
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

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "', '" + txtExActual + "',  '0', '0','" + txtExActual + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        if (costeo == "Si")
                        {
                            if (TipoCosteo == "Ultima Compra")
                            {
                                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (TipoCosteo == "Promedio")
                            {
                                using (SqlCommand cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn))
                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
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
                                }

                                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + " where ClaveProducto= '" + txtClaveProducto + "'", cn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'-- and Inventariable='Si'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Update AlmacenProducto set Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaInicial + '" + txtExActual + "' + Entradas - Salidas where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        if (costeo == "Si")
                        {
                            if (TipoCosteo == "Ultima Compra")
                            {
                                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (TipoCosteo == "Promedio")
                            {
                                using (SqlCommand cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn))
                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
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
                                }

                                using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + " where ClaveProducto= '" + txtClaveProducto + "'", cn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "',  '0', '0','" + txtExActual + "', 0-'" + txtExActual + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Update AlmacenProducto set Salidas= Salidas + '" + txtExActual + "', ExistenciaActual= ExistenciaInicial + Entradas - Salidas - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }

      
      
      

        public void SeleccionarProducto3(Guna2ComboBox cb, string clave)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from ProductosServicios where ClaveProducto='" + clave + "' and Inventariable ='Si'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
                    }
                }
            }
        }
        public string Eliminarpartida(string FolioM, string TipoD, string Descripcion, string EliminarP)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("delete PartidasMovimientoInventario where  FolioMovimiento='" + FolioM + "' and tipodocumento='" + TipoD + "' and Descripcion='" + Descripcion + "' and NoPartida='" + EliminarP + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }

        //_________________________________________________________________________________________________________________________
        // Registrar inventario del documento completo (consolidado)
        public void RegistrarInventarioDelDocumento(string FolioMovimiento, string TipoDocumento, string Descripcion, string Almacen, string AlmacenSalida, string Costeo, string TipoCosteo)
        {
            try
            {
                // Recuperar todas las partidas del documento
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    // Primero leemos todas las partidas a memoria para poder procesar
                    // cada una (ProcesarEntrada/ProcesarSalida abren su propia conexión)
                    // sin tener un DataReader abierto al mismo tiempo.
                    var partidas = new System.Collections.Generic.List<(string ClaveProducto, int Cantidad, decimal Precio)>();

                    using (SqlCommand cmd = new SqlCommand("SELECT ClaveProducto, Cantidad, Precio FROM PartidasMovimientoInventario WHERE FolioMovimiento = '" + FolioMovimiento + "' AND TipoDocumento = '" + TipoDocumento + "' AND Descripcion = '" + Descripcion + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string claveProducto = dr["ClaveProducto"].ToString();
                            int cantidad = Convert.ToInt32(dr["Cantidad"]);
                            decimal precio = Convert.ToDecimal(dr["Precio"]);
                            partidas.Add((claveProducto, cantidad, precio));
                        }
                    }

                    foreach (var partida in partidas)
                    {
                        // Procesar según tipo de documento
                        if (TipoDocumento == "E") // Entrada
                        {
                            ProcesarEntrada(partida.ClaveProducto, partida.Cantidad, Almacen, Costeo, partida.Precio, TipoCosteo);
                        }
                        else if (TipoDocumento == "S") // Salida
                        {
                            ProcesarSalida(partida.ClaveProducto, partida.Cantidad, Almacen);
                        }
                        else if (TipoDocumento == "T") // Traslado
                        {
                            ProcesarSalida(partida.ClaveProducto, partida.Cantidad, Almacen);
                            ProcesarEntrada(partida.ClaveProducto, partida.Cantidad, AlmacenSalida, Costeo, partida.Precio, TipoCosteo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar inventario del documento: " + ex.ToString());
            }
        }

        //_________________________________________________________________________________________________________________________
        // Procesar entrada de productos
        private void ProcesarEntrada(string claveProducto, int cantidad, string almacen, string costeo, decimal precio, string tipoCosteo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;

                    // Verificar si el producto ya existe en el almacén
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as cnt FROM AlmacenProducto WHERE ClaveProducto = '" + claveProducto + "' AND ClaveAlmacen = '" + almacen + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            contador = Convert.ToInt32(dr["cnt"]);
                        }
                    }

                    if (contador <= 0)
                    {
                        // Insertar nuevo registro en AlmacenProducto
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) VALUES ('" + almacen + "', '" + claveProducto + "', '" + cantidad + "', '0', '0', '" + cantidad + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Actualizar Entradas y ExistenciaActual
                        using (SqlCommand cmd = new SqlCommand("UPDATE AlmacenProducto SET Entradas = Entradas + '" + cantidad + "', ExistenciaActual = ExistenciaInicial + Entradas + '" + cantidad + "' - Salidas WHERE ClaveProducto = '" + claveProducto + "' AND ClaveAlmacen = '" + almacen + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Actualizar costo unitario si es necesario
                    if (costeo == "Si")
                    {
                        if (tipoCosteo == "Ultima Compra")
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE ProductosServicios SET CostoUnitario = " + precio + " WHERE ClaveProducto = '" + claveProducto + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (tipoCosteo == "Promedio")
                        {
                            // Obtener existencia actual y costo actual
                            int exActual = 0;
                            decimal costoUnitario = 0;

                            using (SqlCommand cmd = new SqlCommand("SELECT ExActual, CostoUnitario FROM ProductosServicios WHERE ClaveProducto = '" + claveProducto + "' AND Inventariable = 'Si'", cn))
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    exActual = Convert.ToInt32(dr["ExActual"]);
                                    costoUnitario = Convert.ToDecimal(dr["CostoUnitario"]);
                                }
                            }

                            // Calcular promedio: (existencia anterior * costo anterior + cantidad nueva * precio nuevo) / (existencia anterior + cantidad nueva)
                            int nuevaExistencia = exActual + cantidad;
                            decimal nuevoCosto = (exActual * costoUnitario + cantidad * precio) / nuevaExistencia;

                            using (SqlCommand cmd = new SqlCommand("UPDATE ProductosServicios SET CostoUnitario = " + nuevoCosto + " WHERE ClaveProducto = '" + claveProducto + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Actualizar existencia en ProductosServicios
                    using (SqlCommand cmd = new SqlCommand("UPDATE ProductosServicios SET ExActual = ExActual + '" + cantidad + "' WHERE ClaveProducto = '" + claveProducto + "' AND Inventariable = 'Si'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar entrada: " + ex.ToString());
            }
        }

        //_________________________________________________________________________________________________________________________
        // Procesar salida de productos
        private void ProcesarSalida(string claveProducto, int cantidad, string almacen)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;

                    // Verificar si el producto existe en el almacén
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as cnt FROM AlmacenProducto WHERE ClaveProducto = '" + claveProducto + "' AND ClaveAlmacen = '" + almacen + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            contador = Convert.ToInt32(dr["cnt"]);
                        }
                    }

                    if (contador <= 0)
                    {
                        // Insertar nuevo registro con salida negativa
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) VALUES ('" + almacen + "', '" + claveProducto + "', '0', '0', '" + cantidad + "', " + (0 - cantidad) + ")", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Actualizar Salidas y ExistenciaActual
                        using (SqlCommand cmd = new SqlCommand("UPDATE AlmacenProducto SET Salidas = Salidas + '" + cantidad + "', ExistenciaActual = ExistenciaInicial + Entradas - (Salidas + '" + cantidad + "') WHERE ClaveProducto = '" + claveProducto + "' AND ClaveAlmacen = '" + almacen + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Actualizar existencia en ProductosServicios
                    using (SqlCommand cmd = new SqlCommand("UPDATE ProductosServicios SET ExActual = ExActual - '" + cantidad + "' WHERE ClaveProducto = '" + claveProducto + "' AND Inventariable = 'Si'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar salida: " + ex.ToString());
            }
        }
    }
}