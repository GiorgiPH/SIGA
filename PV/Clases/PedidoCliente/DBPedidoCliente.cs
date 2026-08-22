using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.PedidoCliente
{
    internal class DBPedidoCliente
    {
        // NOTA IMPORTANTE:
        // Se eliminaron los campos de instancia (cn, cmd, dr, da, dt) que antes se
        // compartían entre métodos. Esa práctica era la causa de conexiones y
        // DataReaders que quedaban abiertos (el constructor abría "cn" una sola vez
        // y nunca se cerraba explícitamente, y cada método reutilizaba el mismo
        // SqlDataReader sin garantía de que el anterior estuviera cerrado).
        //
        // Ahora cada método abre su propia conexión dentro de un bloque "using",
        // por lo que la conexión, el/los SqlCommand y el SqlDataReader se cierran
        // y liberan automáticamente (incluso si ocurre una excepción).
        //
        // Las firmas de los métodos, los parámetros de entrada y los valores de
        // retorno son EXACTAMENTE los mismos que en la clase original, así como
        // la lógica de negocio (mismas consultas, mismo orden de operaciones).

        public static int Folio = 0;
        public static string MatriculaC = string.Empty;
        public static string Ruta = string.Empty;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Servidor = string.Empty;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBPedidoCliente()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Como ahora cada método administra su propia
            // conexión (abrir/usar/cerrar), ya no es necesario ni conveniente abrir
            // una conexión aquí. Se conserva el constructor vacío para no romper
            // el código existente que hace "new DBPedidoCliente()".
        }

        public void ConsultaTotalOrdenPedidoCliente(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from OrdenPedidoCliente where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Total"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void BuscarClienteFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes where RazonSocial like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }

        //______________________________________________________________________________________________________________
        public void BuscarProveedor(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }

        //______________________________________________________________________________________________________________
        public void BuscarAlumnosFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes where RazonSocial like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }

        public void SeleccionarConceptoDocumentoV(ComboBox cb, string tipo)
        {
            cb.Items.Clear();

            string clase;

            switch (tipo)
            {
                case "Remision":
                    clase = "Remision";
                    break;

                case "Pedido":
                    clase = "Pedido a Clientes";
                    break;

                default:
                    return;
            }

            const string query = @"
        SELECT Clave + ' - ' + Nombre AS Nombre
        FROM Documento
        WHERE TipoDocumento = 'Venta'
          AND Clase = @Clase
        ORDER BY Nombre";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@Clase", SqlDbType.VarChar, 50).Value = clase;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr["Nombre"].ToString());
                    }
                }
            }
        }

        //__________________________________________________________________________________________________________-
        public void CargarRecibos(DataGridView dgv, string tipo, string consecutivo, string documento, string proveedor)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.IdCliente=P.IdCliente and Autorizado is null and tipo like '%" + tipo + "%' and O.Consecutivo like '%" + consecutivo + "%' and O.ClaveDocumento like '%" + documento + "%' and P.RazonSocial like '%" + proveedor + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        //__________________________________________________________________________________________________________-
        public void CargarRecibos2(DataGridView dgv, string tipo, string consecutivo, string documento, string proveedor)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, CLientes as P where O.IdCliente=P.IdCliente and Autorizado<>'' and tipo like '%" + tipo + "%' and O.Consecutivo like '%" + consecutivo + "%' and O.ClaveDocumento like '%" + documento + "%' and P.RazonSocial like '%" + proveedor + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        public string[] InformacionDocumento(string Documento)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[3].ToString(),
                            dr[2].ToString(),
                            dr["MostrarCentroCosto"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //______________________________________________________________________________________________
        public void ActualizarOrdenAuto(string txtFolio, string txtAurizado, string fecha)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update OrdenPedidoCliente set Autorizado='Si', UsuarioAutoriza='" + txtAurizado + "', FechaAutoriza='" + fecha + "'  where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //______________________________________________________________________________________________
        public void InsertarOrden(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string Consecutivo, string Almacen, string Tipo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int Folio;
                    bool existeRegistro;

                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from OrdenPedidoCliente order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        existeRegistro = dr.Read();
                        Folio = existeRegistro ? Convert.ToInt32(dr["Folio"].ToString()) : 0;
                    }

                    if (existeRegistro)
                    {
                        Folio++;
                        txtFolio.Text = Convert.ToString(Folio);

                        using (SqlCommand cmd = new SqlCommand("insert into OrdenPedidoCliente (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, IdCliente, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, Tipo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + Almacen + "', '" + Tipo + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        txtFolio.Text = "1";

                        using (SqlCommand cmd = new SqlCommand("insert into OrdenPedidoCliente (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, IdCliente, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, Tipo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + Almacen + "', '" + Tipo + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //______________________________________________________________________________________________
        public void ConsecutivoCompra(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from OrdenPedidoCliente where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                            Folio++;
                            txtConsecutivo.Text = Folio.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //_______________________________________________________________________________________________
        public void SeleccionarProducto2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Descripcion from ProductosServicios where ClaveProducto not in (Select ClaveProducto from PartidaOrdenPedidoCliente where FolioOrden='" + Folio + "') and inventariable='Si'", cn))
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

        //_______________________________________________________________________________________________
        public void SeleccionarProductoOrdenPedido(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" Select P.Descripcion from ProductosServicios as P Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto=P.ClaveProducto Join OrdenPedidoCliente as O on PO.FolioOrden=O.Folio where PO.FolioOrden='" + Folio + "' and PO.CantidadPendiente>0", cn))
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

        public void Consulta5OrdenCliente(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from [PartidaOrdenPedidoCliente] where FolioOrden='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString());
                            Partida++;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //_____________________________________________________________________________________________________
        public string[] InformacionRecibo(string Recibo, string folio)
        {
            string query = @"
            Select top 1 P.*, PO.CantidadEntregada, PO.CantidadPendiente from ProductosServicios as P
            Left Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto=P.ClaveProducto
            where P.Descripcion= '" + Recibo + "'";
            if (!string.IsNullOrEmpty(folio))
            {
                query += " and PO.FolioOrden='" + folio + "'";
            }

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
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
                            dr[2].ToString(),
                            dr[17].ToString(),
                            dr[5].ToString(),
                            dr[21].ToString(),
                            dr["ExActual"].ToString(),
                            dr[16].ToString(),
                            dr["PedidosProveedor"].ToString(),
                            dr["PedidosCliente"].ToString(),
                            dr["CantidadEntregada"].ToString(),
                            dr["CantidadPendiente"].ToString(),
                            dr["PrecioVenta"].ToString(),
                            dr["DescuentoPorc"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        public string[] InformacionPartidaOrden(string Recibo, string folio)
        {
            string query;

            if (string.IsNullOrEmpty(folio))
            {
                // Si no hay folio, solo consultamos la tabla ProductosServicios
                query = @"
        Select top 1 
            P.ClaveProducto,
            '' as Unidad,
            P.ExActual, 
            P.PedidosProveedor, 
            P.PedidosCliente, 
            P.PrecioVenta,
            1 as Cantidad,
            0 as Subtotal,
            0 as Descuento,
            0 as Impuesto,
            0 as Total,
            P.PrecioVenta as Precio,
            0 as CantidadEntregada,
            0 as CantidadPendiente,
            P.CostoUnitario
        from ProductosServicios as P
        where P.Descripcion = @Recibo";
            }
            else
            {
                // Si hay folio, hacemos el JOIN con PartidaOrdenPedidoCliente
                query = @"
        Select top 1 
            P.ClaveProducto,
            PO.Unidad,
            P.ExActual, 
            P.PedidosProveedor, 
            P.PedidosCliente, 
            P.PrecioVenta,
            PO.Cantidad,
            PO.Subtotal,
            PO.Descuento,
            PO.Impuesto,
            PO.Total,
            PO.Precio,
            PO.CantidadEntregada,
            PO.CantidadPendiente,
            P.CostoUnitario
        from ProductosServicios as P
        Inner Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto = P.ClaveProducto
        where P.Descripcion = @Recibo AND PO.FolioOrden = @Folio";
            }

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@Recibo", Recibo);
                if (!string.IsNullOrEmpty(folio))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                }

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    string[] resultado = null;

                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr["ClaveProducto"].ToString(),
                            dr["Cantidad"].ToString(),
                            dr["Subtotal"].ToString(),
                            dr["Unidad"].ToString(),
                            dr["Descuento"].ToString(),
                            dr["Impuesto"].ToString(),
                            dr["Total"].ToString(),
                            dr["ExActual"].ToString(),
                            dr["PedidosProveedor"].ToString(),
                            dr["PedidosCliente"].ToString(),
                            dr["CantidadEntregada"].ToString(),
                            dr["CantidadPendiente"].ToString(),
                            dr["Precio"].ToString(),
                            dr["CostoUnitario"].ToString(),
                        };
                        resultado = valores;
                    }

                    return resultado;
                }
            }
        }

        public void BuscarOrdenPedidoPendiente(DataGridView dgv, string documento, string consecutivo, string cliente)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlDataAdapter da = new SqlDataAdapter(@" Select OC.Folio, Oc.Consecutivo, OC.ClaveDocumento, C.RazonSocial
 from OrdenPedidoCliente as OC, Documento as D, Clientes as C 
 where C.IdCliente=OC.IdCliente and OC.ClaveDocumento=D.Clave and OC.ClaveDocumento like '%" + documento + "%' and OC.Estatus in ('Bloqueado') and OC.Consecutivo like '%" + consecutivo + "%' and Cast(OC.iDCLiente as varchar) like '%" + cliente + "%' and (select Count(*) from PartidaOrdenPedidoCliente where CantidadPendiente>0 and FolioOrden=OC.Folio)>0", cn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                }
            }
        }

        public void ActualizaCantidadPendientePartidaOrden(string Cantidad, string CantidadEntre, string Folio, string ClaveP)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("update PartidaOrdenPedidoCliente set CantidadPendiente=CantidadPendiente-'" + Cantidad + "', CantidadEntregada=CantidadEntregada+'" + CantidadEntre + "' where FolioOrden='" + Folio + "' and ClaveProducto='" + ClaveP + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

        public string InsertarPartidaOrdenCliente(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Precio, decimal Impuesto)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int existe;
                    using (SqlCommand cmdExiste = new SqlCommand("SELECT COUNT(*) FROM [PartidaOrdenPedidoCliente] WHERE FolioOrden = @Folio AND Partida = @Partida", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Folio", Folio);
                        cmdExiste.Parameters.AddWithValue("@Partida", Partida);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar());
                    }

                    string query;
                    if (existe > 0) // Si el registro existe, actualizamos
                    {
                        query = @"
                UPDATE [PartidaOrdenPedidoCliente]
                SET 
                    ClaveProducto = @ClaveRecibo,
                    Concepto2 = @Concepto2,
                    Cantidad = @Cantidad,
                    Unidad = @Unidad,
                    Divisa = @Divisa,
                    TipoCambio = @TipoCambio,
                    Precio = @Precio,
                    Subtotal = @Subtotal,
                    Descuento = @Descuento,
                    Total = @Total,
                    CantidadRecibida = @Cantidad,
                    Impuesto = @Impuesto,
                    CantidadPendiente = @Cantidad
                WHERE FolioOrden = @Folio AND Partida = @Partida";

                        mensaje = "Registro actualizado correctamente.";
                    }
                    else // Si no existe, insertamos
                    {
                        query = @"
                INSERT INTO [PartidaOrdenPedidoCliente] 
                (FolioOrden, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Subtotal, Descuento, Total, CantidadRecibida, Impuesto, CantidadPendiente)
                VALUES 
                (@Folio, @Partida, @ClaveRecibo, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio, @Precio, @Subtotal, @Descuento, @Total, @Cantidad, @Impuesto, @Cantidad)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Parámetros compartidos
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        cmd.Parameters.AddWithValue("@ClaveRecibo", ClaveRecibo);
                        cmd.Parameters.AddWithValue("@Concepto2", Concepto2);
                        cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                        cmd.Parameters.AddWithValue("@Unidad", Unidad);
                        cmd.Parameters.AddWithValue("@Divisa", Divisa);
                        cmd.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                        cmd.Parameters.AddWithValue("@Precio", Precio);
                        cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                        cmd.Parameters.AddWithValue("@Descuento", Descuento);
                        cmd.Parameters.AddWithValue("@Total", Total);
                        cmd.Parameters.AddWithValue("@Impuesto", Impuesto);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return mensaje;
        }

   

        public void ActualizarTotalesOrdenPedidoCliente(string txtFolio, string txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Subtotal, Descuentos, Total, Impuesto;
                    bool hayDatos;

                    using (SqlCommand cmd = new SqlCommand("select coalesce(sum(Subtotal),0.00) as Subtotal, coalesce(sum((Convert(decimal, Impuesto) / 100) * (Subtotal-Descuento)),0.00) as Impuesto, coalesce(sum((Convert(decimal, Descuento) / 100) * (Subtotal)),0.00) as Descuento, coalesce(sum(Total),0.00) as Total from [PartidaOrdenPedidoCliente] where FolioOrden='" + txtFolio + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        hayDatos = dr.Read();
                        if (hayDatos)
                        {
                            Subtotal = dr["Subtotal"].ToString();
                            Descuentos = dr["Descuento"].ToString();
                            Total = dr["Total"].ToString();
                            Impuesto = dr["Impuesto"].ToString();
                        }
                        else
                        {
                            Subtotal = Descuentos = Total = Impuesto = null;
                        }
                    }

                    if (hayDatos)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update OrdenPedidoCliente set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "', Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //____________________________________________________________________________________________________
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

        public List<List<string>> ObtenerPartidas(string Folio)
        {
            List<List<string>> listam = new List<List<string>>();

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from [PartidaRemision] as PR Join Remision as R On R.Folio=PR.[FolioRemision] Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioRemision=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Folio);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        List<string> lista = new List<string>();
                        lista.Add(dr[0].ToString());
                        lista.Add(dr[1].ToString());
                        lista.Add(dr[2].ToString());
                        lista.Add(dr[3].ToString());
                        lista.Add(dr[4].ToString());
                        lista.Add(dr[5].ToString());
                        lista.Add(dr[6].ToString());
                        listam.Add(lista);
                    }
                }
            }
            return listam;
        }

        //_______________________________________________________________________________________________________________
        public void ActualizarReciboEstatus(string Folio, string Estatus)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update OrdenPedidoCliente set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public bool PartidasPendientesOrdenPedido(string Folio)
        {
            bool pendiente = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select 1 from partidaOrdenPedidoCliente where CantidadPendiente>0 and FolioOrden='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            pendiente = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return pendiente;
        }

        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasOrden(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtImpuesto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                    SUM(ISNULL(Subtotal, 0)) AS Subtotal, 
                    SUM(ISNULL(CAST((Descuento / 100.0) * Subtotal AS decimal(18, 2)), 0)) AS Descuento, 
                    SUM(ISNULL(Total, 0)) AS Total, 
                    SUM(ISNULL(CAST((Impuesto / 100.0) * (Subtotal - (Subtotal * (Descuento / 100.0))) AS decimal(18, 2)), 0)) AS Impuesto
                    FROM [PartidaOrdenPedidoCliente]
                     where FolioOrden = '" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //_______________________________________________________________________________________________________________
        public void ReciboSaldos(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtRecargo, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtTotalPartidas)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from OrdenPedidoCliente where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //______________________________________________________________________________________________________-
        public void ConsultaRecibo(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2DateTimePicker Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza, ComboBox cmbAlmacen, out string cliente)
        {
            cliente = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select *, (Cast(A.Clave as varchar)+' - '+A.Nombre) as Alm from OrdenPedidoCliente as O Left Join Almacenes as A on A.Clave=O.Almacen where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Documento.Text = dr["ClaveDocumento"].ToString();
                            Estatus.Text = dr["Estatus"].ToString();
                            Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                            Dias.Text = dr["DiasVencen"].ToString();
                            FechaVence.Text = Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy-MM-dd");

                            Divisa.Text = dr["Divisa"].ToString();
                            TipoCambio.Text = dr["TipoCambio"].ToString();
                            Subtotal.Text = dr["Subtotal"].ToString();
                            Descuentos.Text = dr["Descuento"].ToString();
                            Cargo.Text = dr["Cargo"].ToString();
                            Total.Text = dr["Total"].ToString();

                            Notas.Text = dr["Notas"].ToString();
                            Elaborado.Text = dr["Elaborado"].ToString();

                            MatriculaC = dr["IdCliente"].ToString();
                            txtFolio.Text = dr["Folio"].ToString();
                            txtConsecutivo.Text = dr["Consecutivo"].ToString();
                            txtAutoriza.Text = dr["UsuarioAutoriza"].ToString();
                            if (dr["FechaAutoriza"].ToString() != string.Empty)
                            {
                                txtFechaAutoriza.Text = Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy-MM-dd");
                            }
                            cmbAlmacen.Text = dr["Alm"].ToString();
                            cliente = dr["IdCliente"].ToString();
                            Partidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //_________________________________________________________________________________________
        public string[] InformacionDocumento2(string Documento)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Documento where Clave= '" + Documento + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[3].ToString(),
                            dr[2].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        public void ConsultarPartidasOrdenPedido(DataGridView dgv, string Orden)
        {
            // Se mantienen los parámetros para evitar SQL Injection (ya venía así en el original)
            string query = "Select * from PartidaOrdenPedidoCliente where FolioOrden = @FolioOrden";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@FolioOrden", Orden);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dgv.Rows.Clear();

                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                            dr[1].ToString(),
                            dr[2].ToString(),
                            // Agrega más columnas si es necesario
                        };

                        dgv.Rows.Add(valores);
                    }
                }
            }
        }

        //_________________________________________________________________________________________
        public string[] InformacionOrdenPedido(string Orden)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" select OC.Folio, OC.ClaveDocumento, Oc.IdCliente, OC.Almacen, OC.Tipo, OC.Notas from OrdenPedidoCliente as OC where Folio='" + Orden + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr["Folio"].ToString(),
                            dr["ClaveDocumento"].ToString(),
                            dr["IdCliente"].ToString(),
                            dr["Almacen"].ToString(),
                            dr["Tipo"].ToString(),
                            dr["Notas"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //_______________________________________________________
        public void CargarOrdenPedidoClientePartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from [PartidaOrdenPedidoCliente] as PO, ProductosServicios as PS where FolioOrden='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioOrden"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Cantidad"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //_______________________________________________________
        public void CargarRequisicionPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRequisicion as PO, ProductosServicios as PS where FolioRequisicion='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioRequisicion"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //............................................................................................................
        public void ConsultaPartidaOrdenPedido(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox subtotal, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox TxtPrecio, Guna.UI2.WinForms.Guna2TextBox txtImpuesto, Guna.UI2.WinForms.Guna2TextBox txtEntregado, ComboBox cmbConcepto2)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select P.*, C.Descripcion from [PartidaOrdenPedidoCliente] as P, ProductosServicios as C where FolioOrden='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
                            Concepto2.Text = dr["ClaveProducto"].ToString();
                            cantidad.Text = dr["Cantidad"].ToString();
                            unidad.Text = dr["Unidad"].ToString();
                            divisa.Text = dr["Divisa"].ToString();
                            tipocambio.Text = dr["TipoCambio"].ToString();
                            TxtPrecio.Text = dr["Precio"].ToString();
                            subtotal.Text = dr["Subtotal"].ToString();
                            descuento.Text = dr["Descuento"].ToString();
                            total.Text = dr["Total"].ToString();
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                            txtEntregado.Text = dr["CantidadRecibida"].ToString();
                            cmbConcepto2.Items.Add(dr["Descripcion"].ToString());
                            cmbConcepto2.Text = dr["Descripcion"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ValidarDocumentoSPR()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM TipoMovimiento WHERE Documento = 'SPR' AND TipoMovimiento = 'S') " +
                                                     "BEGIN " +
                                                     "    INSERT INTO TipoMovimiento VALUES ('S', 'SPR', 'SALIDA POR REMISIÓN', 'Activo', '0', 'Si', 'Si', '', '') " +
                                                     "END", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ActualizarMovimientoJ(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(@"
    UPDATE R 
    SET 
        Estatus='Bloqueado', 
        R.Descuento = (
            SELECT COALESCE(SUM(CAST(PP.Descuento AS DECIMAL)), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        R.Impuestos = (
            SELECT COALESCE(SUM(CAST(PP.IVA AS DECIMAL)), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        Total = (
            SELECT COALESCE(SUM(PP.Total), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        Subtotal = (
            SELECT COALESCE(SUM(PP.Subtotal), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        TotalPartidas = (
            SELECT COALESCE(Count(*), 0) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        )
    FROM 
        MovimientoInventario AS R 
    JOIN 
        PartidasMovimientoInventario AS P 
    ON 
        R.Folio = P.FolioMovimiento AND R.Descripcion = P.Descripcion 
    WHERE 
        R.Folio = @txtFolio 
        AND R.TipoDocumento = @txtTipoMovimiento 
        AND R.Descripcion = @txtDocumento", cn))
                {
                    cmd.Parameters.AddWithValue("@txtPartidas", txtPartidas);
                    cmd.Parameters.AddWithValue("@txtFolio", txtFolio);
                    cmd.Parameters.AddWithValue("@txtTipoMovimiento", txtTipoMovimiento);
                    cmd.Parameters.AddWithValue("@txtDocumento", txtDocumento);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRORac" + ex.ToString());
            }
        }

        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion, string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas, string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado, TextBox FOlioP, string txtAlmacenSalida, string txtCentroCosto, string Propietario, string OrdenTrabajo, string CajaR)
        {
            string mensaje = "";
            int contador = 0;
            int FolioM = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from MovimientoInventario order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            FolioM = Convert.ToInt32(dr["Folio"].ToString());
                            FolioM++;
                            FOlioP.Text = FolioM.ToString();
                        }
                        else
                        {
                            FolioM = 1;
                            FOlioP.Text = FolioM.ToString();
                        }
                    }

                    int cons = 1;
                    using (SqlCommand cmd = new SqlCommand("Select UltimoFolio from TipoMovimiento where TipoMovimiento='" + txtTipoDocumento + "' and Documento='" + cmbDescripcion + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cons = Convert.ToInt32(dr["UltimoFolio"].ToString());
                            cons++;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("select * from MovimientoInventario where Folio='" + FolioM + "' and TipoDocumento='" + txtTipoDocumento + "' and Referencias='" + txtReferencias + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into MovimientoInventario (Folio, TipoDocumento, Descripcion, Fecha, Estatus, Referencias, Almacen, TotalPartidas, Divisa, TipoCambio, Total, Notas, Elaborado, Consecutivo, AlmacenSalida) values ('" + FolioM + "', '" + txtTipoDocumento + "',  '" + cmbDescripcion + "',  '" + dtpFecha + "', '" + cmbEstatus + "',  '" + txtReferencias + "', '" + txtAlmacen + "', '" + txtTotalPartidas + "', '" + cmbDivisa + "', '" + txtTipoCambio + "', '" + txtTotal + "', '" + txtNotas + "', '" + txtElaborado + "', '" + cons + "', '" + txtAlmacenSalida + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        mensaje = "Registro guardado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return FolioM.ToString();
        }

        public void RegistroPartida(string txtFolioMovimiento, string txtTipoDocumento, string txtDescripcion, string txtNoPartida, string cmbClaveProducto, string txtCantidad, string txtunidad, decimal txtPrecio, string cmbDivisa, string txtTipoCambio, decimal txtTotal, string Concepto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Insert into PartidasMovimientoInventario (FolioMovimiento, TipoDocumento, Descripcion, NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto) values ('" + txtFolioMovimiento + "', '" + txtTipoDocumento + "', '" + txtDescripcion + "','" + txtNoPartida + "', '" + cmbClaveProducto + "', '" + txtCantidad + "','" + txtunidad + "', " + txtPrecio + ", '" + cmbDivisa + "', '" + txtTipoCambio + "', " + txtTotal + ", '" + Concepto + "' )", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

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
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) VALUES (@ClaveAlmacen, @ClaveProducto, 0, 0, 0 - @ExistenciaActual, 0 - @ExistenciaActual)", cn))
                        {
                            cmd.Parameters.AddWithValue("@ClaveAlmacen", txtAlmacen);
                            cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                            cmd.Parameters.AddWithValue("@ExistenciaActual", Convert.ToDecimal(txtExActual));
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "'  where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Update AlmacenProducto set Salidas = Salidas + @ExActual, ExistenciaActual = ExistenciaInicial + Entradas - @ExActual - Salidas where ClaveProducto = @ClaveProducto and ClaveAlmacen = @ClaveAlmacen", cn))
                        {
                            cmd.Parameters.AddWithValue("@ExActual", Convert.ToDecimal(txtExActual));
                            cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                            cmd.Parameters.AddWithValue("@ClaveAlmacen", txtAlmacen);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set ExActual = ExActual - @ExActual where ClaveProducto = @ClaveProducto and Inventariable = 'Si'", cn))
                        {
                            cmd.Parameters.AddWithValue("@ExActual", Convert.ToDecimal(txtExActual));
                            cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
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

        public decimal ObtenerTotalOrdenPedidoCliente(string Folio)
        {
            decimal total = 0.00m;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Total from OrdenPedidoCliente where Folio=" + Folio + "", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            total = Convert.ToDecimal(dr[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return total;
        }

        public string EliminarPartidaOrdenPedidoCliente(string Folio, string Partida)
        {
            string mensaje = string.Empty;
            try
            {
                // Se combinan ambas operaciones en una sola conexión (antes se abrían dos
                // conexiones independientes para la misma operación lógica).
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("Delete from PartidaOrdenPedidoCliente where FolioOrden=@Folio and Partida=@Partida", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        mensaje = filasAfectadas > 0
                            ? "Eliminación exitosa"
                            : "No se encontró ninguna fila para eliminar";
                    }

                    using (SqlCommand command = new SqlCommand("UPDATE PartidaOrdenPedidoCliente SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and FolioOrden=@Folio", cn))
                    {
                        command.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        command.Parameters.AddWithValue("@Folio", Folio);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error." + ex.ToString();
            }
            return mensaje;
        }

        public string ObtenerTotalPartidasOrdenPedidoCliente(string Folio)
        {
            string maximo = "0";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select max(Partida) as maximo from PartidaOrdenPedidoCliente where FolioOrden='" + Folio + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        maximo = dr[0].ToString();
                    }
                }
            }
            return maximo;
        }
    }
}