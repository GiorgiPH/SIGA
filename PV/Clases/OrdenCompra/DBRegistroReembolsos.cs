using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.OrdenCompra
{
    class DBRegistroReembolso
    {
        public static int Folio = 0;
        public static string MatriculaC = string.Empty;
        public static string Ruta = string.Empty;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Servidor = string.Empty;
        public static List<string> datosCombo = new List<string>();

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBRegistroReembolso()
        {
            // Ya no se abre ni se mantiene una conexión compartida a nivel de instancia.
            // Cada método abre y cierra su propia conexión (patrón "using") para evitar
            // fugas de conexiones y problemas de concurrencia.
        }

        //====================================================================
        // Métodos auxiliares privados (evitan duplicar el mismo patrón ADO.NET)
        //====================================================================

        // Llena un ComboBox con la primera columna del resultado (Items.Clear() lo hace el llamador).
        private void LlenarComboBox(ComboBox cb, string query)
        {
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        // Devuelve el arreglo de columnas (por índice) de la última fila leída, o null si no hay filas.
        private string[] ObtenerFila(string query, params int[] columnas)
        {
            string[] resultado = null;
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultado = new string[columnas.Length];
                        for (int i = 0; i < columnas.Length; i++)
                        {
                            resultado[i] = dr[columnas[i]].ToString();
                        }
                    }
                }
            }
            return resultado;
        }

        // Verifica si existen filas con queryExistencia; si existen, ejecuta queryActualizacion.
        private void ActualizarSiExiste(string queryExistencia, string queryActualizacion)
        {
            int contador = 0;
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryExistencia, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        contador++;
                    }
                }

                if (contador > 0)
                {
                    using (SqlCommand cmd = new SqlCommand(queryActualizacion, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Obtiene el siguiente número de partida para un folio de gasto (o "1" si no hay partidas).
        private void ObtenerSiguientePartida(string folioGasto, Guna2TextBox txtPartida)
        {
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select top 1 * from PartidaRegistroReembolso where FolioGasto='" + folioGasto + "' order by Partida Desc", conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int partida = Convert.ToInt32(dr["Partida"].ToString());
                        partida++;
                        txtPartida.Text = partida.ToString();
                    }
                    else
                    {
                        txtPartida.Text = "1";
                    }
                }
            }
        }

        // Llena un DataGridView con el listado estándar de RegistroReembolso + Proveedor.
        private void LlenarGridRegistroReembolso(DataGridView dgv, string query)
        {
            dgv.Rows.Clear();
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
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

        //====================================================================
        // Métodos públicos
        //====================================================================

        public void ActualizarReembolso(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RegistroReembolso set Estatus='" + Estatus + "' where Folio='" + Folio + "'", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public void ActualizarSaldoProveedor2(string Clave, decimal Saldo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Proveedor set Saldo= Saldo + " + Saldo + " where IdProveedor=" + Clave, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }

        public void CargarGasto(DataGridView dgv)
        {
            try
            {
                LlenarGridRegistroReembolso(dgv, "select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        public string CancelarRegistroGasto(string folio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("CancelarRegistroReembolso", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@FolioRegistro", folio);

                    // Parámetro de salida para el mensaje
                    SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Captura el mensaje de resultado
                    string mensaje = mensajeParam.Value?.ToString();
                    return mensaje ?? "No se recibió ningún mensaje del procedimiento almacenado.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Registrar datos del aviso
        public void ModificarExtension4(string Folio, string Partida)
        {
            try
            {
                ActualizarSiExiste(
                    "select * from RegistroReembolso where Folio=" + Folio + "",
                    "Update RegistroReembolso set Extension='', Archivo='' where Folio=" + Folio + "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

        public void InsertarRegistroGasto(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia, string DiasVence, string FechaVence, string centrocosto, int semana, string anio, string proveedorAlterno, string proyecto, string totalRetenciones)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    conn.Open();

                    int nuevoFolio;
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) + 1 FROM RegistroReembolso", conn))
                    {
                        nuevoFolio = (int)cmd.ExecuteScalar();
                    }

                    txtFolio.Text = nuevoFolio.ToString();

                    string orden = string.IsNullOrEmpty(RecepcionProducto) ? "0" : RecepcionProducto;

                    string query = @"INSERT INTO RegistroReembolso 
                        (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, DiasVence, FechaVence, CentroCostos, Semana, Anio, ProveedorAlterno,Proyecto,TotalRetenciones)
                        VALUES 
                        (@Folio, @ClaveDocumento, @Estatus, @Fecha, @ClaveProveedor, @Divisa, @TipoCambio, @Notas, @Elaborado, @FolioOrden, @Consecutivo, @Referencia, @DiasVence, @FechaVence, @CentroCostos, @Semana, @Anio, @ProveedorAlterno,@proyecto,@TotalRetenciones)";

                    using (SqlCommand cmdInsert = new SqlCommand(query, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@Folio", nuevoFolio);
                        cmdInsert.Parameters.AddWithValue("@ClaveDocumento", ClaveDocumento);
                        cmdInsert.Parameters.AddWithValue("@Estatus", Estatus);
                        cmdInsert.Parameters.AddWithValue("@Fecha", Fecha);
                        cmdInsert.Parameters.AddWithValue("@ClaveProveedor", ClavePropietario);
                        cmdInsert.Parameters.AddWithValue("@Divisa", Divisa);
                        cmdInsert.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                        cmdInsert.Parameters.AddWithValue("@Notas", Notas);
                        cmdInsert.Parameters.AddWithValue("@Elaborado", Elaborado);
                        cmdInsert.Parameters.AddWithValue("@FolioOrden", orden);
                        cmdInsert.Parameters.AddWithValue("@Consecutivo", Consecutivo);
                        cmdInsert.Parameters.AddWithValue("@Referencia", Referencia);
                        cmdInsert.Parameters.AddWithValue("@DiasVence", DiasVence);
                        cmdInsert.Parameters.AddWithValue("@FechaVence", FechaVence);
                        cmdInsert.Parameters.AddWithValue("@CentroCostos", centrocosto);
                        cmdInsert.Parameters.AddWithValue("@Semana", semana);
                        cmdInsert.Parameters.AddWithValue("@Anio", anio);
                        cmdInsert.Parameters.AddWithValue("@ProveedorAlterno", proveedorAlterno);
                        cmdInsert.Parameters.AddWithValue("@proyecto", proyecto);
                        cmdInsert.Parameters.AddWithValue("@TotalRetenciones", totalRetenciones);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        // Registrar datos del aviso
        public void ModificarExtension5gasto(string Folio, string Partida, string Extension)
        {
            try
            {
                ActualizarSiExiste(
                    "select * from RegistroReembolso where Folio=" + Folio + "",
                    "Update RegistroReembolso set Extension='" + Extension + "' where Folio=" + Folio + "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

        public void ActualizarRecepcion3gasto(string Folio, string Partida, string Archivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RegistroReembolso set Archivo='" + Archivo + "' where Folio='" + Folio + "'", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public string[] InformacionDocumento(string Documento)
        {
            return ObtenerFila("Select * from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", 3, 2);
        }

        public void ConsecutivoGasto(Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from RegistroReembolso where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                            folio++;
                            txtConsecutivo.Text = folio.ToString();
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

        public void SeleccionarOrdenEntrega(ComboBox cb, string Filtro, string Proveedor)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, " Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" + Proveedor + "' and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0");
        }

        public void ConsultaGasto(string Folio, Guna2TextBox txtPartida)
        {
            try
            {
                ObtenerSiguientePartida(Folio, txtPartida);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// CAMBIO: antes filtraba por P.Descripcion = cmbConcepto.Text (texto libre,
        /// se rompía con apóstrofes o si dos conceptos compartían descripción).
        /// Ahora recibe la Clave (SelectedValue del combo) y filtra por
        /// PA.ClaveProducto, la misma clave que ya se usa como ValueMember del combo
        /// cuando está ligado a una orden. Las columnas devueltas no cambian.
        /// </summary>
        public DataTable ObtenerInformacionGasto(string claveProducto, string orden)
        {
            DataTable dt = new DataTable();

            const string sql = @"
        SELECT
            PA.Id,
            PA.FolioOrden,
            PA.Partida,
            PA.ClaveProducto,
            PA.Cantidad,
            PA.Unidad,
            PA.Precio,
            PA.Subtotal,
            PA.Descuento,
            PA.Total,
            PA.Impuesto,
            PA.Concepto2,
            P.Descripcion,
            P.TipoCosteo
        FROM PartidaOrden PA
        INNER JOIN ProductosServicios P
            ON PA.ClaveProducto = P.ClaveProducto
        WHERE PA.FolioOrden = @Orden
          AND PA.ClaveProducto = @ClaveProducto;";

            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Orden", SqlDbType.VarChar, 50).Value = orden;
                cmd.Parameters.Add("@ClaveProducto", SqlDbType.Int).Value =
                    Convert.ToInt32(claveProducto);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
        /// <summary>
        /// CAMBIO: antes filtraba por Descripcion = cmbConcepto.Text. Ahora recibe la
        /// Clave (SelectedValue del combo, columna "ClaveServicio") y filtra por
        /// ClaveServicio, el mismo campo usado como ValueMember cuando NO está ligado
        /// a una orden.
        /// </summary>
        public DataTable InformacionGasto(string claveServicio)
        {
            DataTable dt = new DataTable();

            const string sql = @"
        SELECT
            ClaveServicio,
            Descripcion,
            UnidadMedida,
            TipoCosteo,
            CostoUnitario,
            PrecioVenta,
            ImpuestoPorc,
            ImpuestoCant,
            IEPS
        FROM Servicios
        WHERE ClaveServicio = @ClaveServicio";

            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ClaveServicio", SqlDbType.Int).Value =
                    Convert.ToInt32(claveServicio);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // Registrar datos del aviso
        public void ModificarExtension4gasto(string Folio, string Partida, string Extension)
        {
            try
            {
                ActualizarSiExiste(
                    "select * from PartidaRegistroReembolso where FolioGasto=" + Folio + " and Partida='" + Partida + "'",
                    "Update PartidaRegistroReembolso set Extension='" + Extension + "' where FolioGasto=" + Folio + " and Partida='" + Partida + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

        public void ActualizarRecepcion2gasto(string Folio, string Partida, string Archivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update PartidaRegistroReembolso set  Archivo='" + Archivo + "' where FolioGasto='" + Folio + "' and Partida='" + Partida + "'", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        // Registrar datos del aviso
        public void ModificarExtension3gasto(string Folio, string Partida)
        {
            try
            {
                ActualizarSiExiste(
                    "select * from PartidaRegistroReembolso where FolioGasto=" + Folio + " and Partida='" + Partida + "'",
                    "Update PartidaRegistroReembolso set Extension='', Archivo='' where FolioGasto=" + Folio + " and Partida='" + Partida + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }

        public void InsertarPartidaGasto(
            string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad,
            string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento,
            decimal Total, decimal Impuesto, string archivo, string proveedorAlterno, string centroCostosAlterno, string DescuentoImporte, string ImpuestoImporte,
            string proyecto, string Fechacompra, string formadepago, string referencia, string IEPS, string Retencion, string precio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    conn.Open();

                    // Verifica si existe la partida
                    int count;
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM PartidaRegistroReembolso WHERE FolioGasto = @Folio AND Partida = @Partida", conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@Folio", Folio);
                        cmdCheck.Parameters.AddWithValue("@Partida", Partida);
                        count = (int)cmdCheck.ExecuteScalar();
                    }

                    string query;
                    if (count > 0)
                    {
                        // Si existe, actualiza
                        query = @"UPDATE PartidaRegistroReembolso SET 
                ClaveProducto = @ClaveRecibo, Concepto2 = @Concepto2, Cantidad = @Cantidad,
                Unidad = @Unidad, Divisa = @Divisa, TipoCambio = @TipoCambio, Subtotal = @Subtotal,
                Descuento = @Descuento, Total = @Total, Impuesto = @Impuesto, Archivo = @Archivo,
                ProveedorAlterno = @ProveedorAlterno, CentroCostosAlterno = @CentroCostosAlterno, DescuentoImporte=@DescuentoImporte, ImpuestoImporte=@ImpuestoImporte,
proyecto=@proyecto,Fechacompra=@Fechacompra,formadepago=@formadepago,referencia=@referencia, IEPS = @IEPS,Retencion=@Retencion,precio=@precio 
                WHERE FolioGasto = @Folio AND Partida = @Partida";
                    }
                    else
                    {
                        // Si no existe, inserta
                        query = @"INSERT INTO PartidaRegistroReembolso
                (FolioGasto, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, 
                Subtotal, Descuento, Total, Impuesto, Archivo, ProveedorAlterno, CentroCostosAlterno, DescuentoImporte, ImpuestoImporte,
proyecto,Fechacompra,formadepago,referencia, IEPS,Retencion,precio)
                
VALUES 
                (@Folio, @Partida, @ClaveRecibo, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio, 
                @Subtotal, @Descuento, @Total, @Impuesto, @Archivo, @ProveedorAlterno, @CentroCostosAlterno,@DescuentoImporte, @ImpuestoImporte,
                @proyecto,@Fechacompra,@formadepago,@referencia, @IEPS,@Retencion,@precio)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Agrega parámetros comunes
                        cmd.Parameters.AddWithValue("@ClaveRecibo", ClaveRecibo);
                        cmd.Parameters.AddWithValue("@Concepto2", Concepto2);
                        cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                        cmd.Parameters.AddWithValue("@Unidad", Unidad);
                        cmd.Parameters.AddWithValue("@Divisa", Divisa);
                        cmd.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                        cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                        cmd.Parameters.AddWithValue("@Descuento", Descuento);
                        cmd.Parameters.AddWithValue("@Total", Total);
                        cmd.Parameters.AddWithValue("@Impuesto", Impuesto);
                        cmd.Parameters.AddWithValue("@Archivo", archivo);
                        cmd.Parameters.AddWithValue("@ProveedorAlterno", string.IsNullOrWhiteSpace(proveedorAlterno) ? DBNull.Value : (object)proveedorAlterno);
                        cmd.Parameters.AddWithValue("@CentroCostosAlterno", string.IsNullOrWhiteSpace(centroCostosAlterno) ? DBNull.Value : (object)centroCostosAlterno);
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        cmd.Parameters.AddWithValue("@DescuentoImporte", DescuentoImporte);
                        cmd.Parameters.AddWithValue("@ImpuestoImporte", ImpuestoImporte);
                        cmd.Parameters.AddWithValue("@proyecto", proyecto);
                        cmd.Parameters.AddWithValue("@Fechacompra", Fechacompra);
                        cmd.Parameters.AddWithValue("@formadepago", string.IsNullOrWhiteSpace(formadepago) ? DBNull.Value : (object)formadepago);
                        cmd.Parameters.AddWithValue("@referencia", referencia);
                        cmd.Parameters.AddWithValue("@IEPS", IEPS);
                        cmd.Parameters.AddWithValue("@Retencion", Retencion);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarPartidaOrden(string Folio, string Partida, string Cantidad)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update PartidaOrden set CantidadRecibida= CantidadRecibida - '" + Cantidad + "' where FolioOrden='" + Folio + "' and  Partida='" + Partida + "'", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public void ActualizarReembolso(string txtFolio)
        {
            try
            {
                string subtotal, descuentos, total, impuesto, ieps, totalPartidas, totalRetenciones;

                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(@"SELECT 
                          ISNULL(SUM(DescuentoImporte), 0) AS Descuento, 
                          ISNULL(SUM(ImpuestoImporte), 0) AS Impuesto, 
                          ISNULL(SUM(IEPS), 0) AS IEPS, 
                          ISNULL(SUM(Subtotal), 0) AS Subtotal, 
                          ISNULL(SUM(Total), 0) AS Total, 
                          COUNT(*) AS TotalPartidas,
                          ISNULL(SUM(Retencion), 0) AS Retencion
                       FROM PartidaRegistroReembolso 
                       WHERE FolioGasto = @Folio", conn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", txtFolio);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return;
                            }

                            subtotal = dr["Subtotal"].ToString();
                            descuentos = dr["Descuento"].ToString();
                            total = dr["Total"].ToString();
                            impuesto = dr["Impuesto"].ToString();
                            ieps = dr["IEPS"].ToString();
                            totalPartidas = dr["TotalPartidas"].ToString();
                            totalRetenciones = dr["Retencion"].ToString();
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Update RegistroReembolso set TotalPartidas='" + totalPartidas + "', Subtotal='" + subtotal + "', Descuento='" + descuentos + "',  Cargo='" + impuesto + "', IEPS = '" + ieps + "', Total='" + total + "', Saldo='" + total + "', TotalRetenciones='" + totalRetenciones + "' where Folio='" + txtFolio + "'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }



        public void ReciboSaldosPartidasGasto(string txtFolio, Guna2TextBox txtSubtoral, Guna2TextBox txtDescuento, Guna2TextBox txtTotal, Guna2TextBox txtImpuesto, Guna2TextBox txtIEPS, Guna2TextBox txtRetencion, Guna2TextBox txtPartidas)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(DescuentoImporte) as Descuento,sum(ImpuestoImporte) as Impuesto, sum(Total) as Total, sum(IEPS) as IEPS, sum(retencion) as retencion, count(*) as Partidas from PartidaRegistroReembolso where FolioGasto='" + txtFolio + "'", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                            txtIEPS.Text = dr["IEPS"].ToString();
                            txtRetencion.Text = dr["retencion"].ToString();
                            txtPartidas.Text = dr["Partidas"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public void SeleccionarProductoGasto(ComboBox cb, string Orden)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "select P.Descripcion from PartidaOrden as PA, Servicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Estatus='Activo'");
        }



        public void SeleccionarProductoGasto(ComboBox cb)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "Select Descripcion from Servicios where Estatus='Activo'");
        }


        public void ObtenerPartidasReembolso(string folio, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT PO.*,PS.Descripcion,CG.Clave,CG.Clase,CG.Tipo,CGG.Cargo as CargoConcepto,CGG.Descuento as DescuentoConcepto,PO.Cantidad,  PO.precio,    CGG.Descuento FROM PartidaRegistroReembolso AS PO Left Join ConceptoGlobalesGasto as CGG On PO.FolioGasto=CGG.Folio and PO.Partida=CGG.Partida LEFT JOIN ConceptosGlobales as CG On CG.Clave=CGG.ClaveConceptoG JOIN servicios AS PS ON PO.ClaveProducto = PS.ClaveServicio WHERE PO.FolioGasto = '" + folio + "' ORDER BY Partida ASC", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveProducto"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();

                        // Numeric fields: use .Field<T?>() and handle potential nulls
                        dgv.Rows[n].Cells[4].Value = item.Field<int?>("Cantidad")?.ToString() ?? "0"; // int
                        dgv.Rows[n].Cells[5].Value = item["Unidad"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["Divisa"].ToString();
                        dgv.Rows[n].Cells[7].Value = item.Field<decimal?>("TipoCambio")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[8].Value = item.Field<decimal?>("Subtotal")?.ToString("N2") ?? "0.00"; // decimal

                        // Nota: en el SQL hay dos "Descuento" (PO.Descuento y CGG.Descuento as DescuentoConcepto).
                        // Se conserva el mapeo original: el 'Descuento' que expone PO.* en la celda 9.
                        dgv.Rows[n].Cells[9].Value = item.Field<decimal?>("Descuento")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[10].Value = item.Field<decimal?>("Total")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[11].Value = item.Field<decimal?>("Impuesto")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[12].Value = item["Extension"].ToString();
                        dgv.Rows[n].Cells[13].Value = item["Archivo"].ToString();
                        dgv.Rows[n].Cells[14].Value = item.Field<int?>("CentroCostosAlt")?.ToString() ?? "0"; // int
                        dgv.Rows[n].Cells[15].Value = item.Field<int?>("ProveedorAlterno")?.ToString() ?? "0"; // int
                        dgv.Rows[n].Cells[16].Value = item.Field<int?>("CentroCostosAlterno")?.ToString() ?? "0"; // int
                        dgv.Rows[n].Cells[17].Value = item.Field<decimal?>("ImpuestoImporte")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[18].Value = item.Field<decimal?>("DescuentoImporte")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[19].Value = item["proyecto"].ToString();
                        dgv.Rows[n].Cells[20].Value = item["Fechacompra"].ToString(); // Consider parsing to DateTime and formatting
                        dgv.Rows[n].Cells[21].Value = item["formadepago"].ToString();
                        dgv.Rows[n].Cells[22].Value = item["referencia"].ToString();
                        dgv.Rows[n].Cells[23].Value = item.Field<decimal?>("IEPS")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[24].Value = item.Field<decimal?>("Retencion")?.ToString("N2") ?? "0.00"; // decimal
                        dgv.Rows[n].Cells[25].Value = item.Field<decimal?>("precio")?.ToString("N2") ?? "0.00"; // decimal (PO.precio)
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }

        public void ObtenerPartidasReembolso2(string folio, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();

                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '" + folio + "' AND CG.Clave = 'IVA16' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, cg.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.IEPS) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO, ConceptosGlobales AS CG WHERE PO.FolioGasto = '" + folio + "' AND PO.IEPS > 0 AND CG.Clave = 'IEPS' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.IEPS) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '181' AND CG.Clave = 'IVA8' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, 0.00 AS ImporteImpuesto, SUM(PO.Retencion) AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO INNER JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida INNER JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '" + folio + "' AND CG.Clase = 'Descuento' AND CG.Clave LIKE 'R%' AND PO.Retencion > 0 GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.Retencion) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '" + folio + "' AND CG.Clave not in ('IEPS','IVA8','IVA16') GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) = 0 ORDER BY FolioGasto, ClaseConceptoAgrupada, Tipo;", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["ClaseConceptoAgrupada"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["SubtotalCalculado"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["ImporteImpuesto"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["ImporteRetencion"].ToString(); // int
                        dgv.Rows[n].Cells[5].Value = item["Tipo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }

        public void CargarRecibosPartidasGasto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRegistroReembolso as PO, servicios as PS where PO.FolioGasto='" + Folio + "' and PO.ClaveProducto=PS.ClaveServicio order by Partida asc", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["Subtotal"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["ImpuestoImporte"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["DescuentoImporte"].ToString();
                        dgv.Rows[n].Cells[7].Value = item["Ieps"].ToString();
                        dgv.Rows[n].Cells[8].Value = item["Retencion"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string EliminarPartidaRegistroGasto(string Folio, string Partida)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(ObtenerCn()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from PartidaRegistroReembolso where FolioGasto=@Folio and Partida=@Partida", conexion))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        mensaje = filasAfectadas > 0 ? "Eliminación exitosa" : "No se encontró ninguna fila para eliminar";
                    }
                }

                string queryUpdate = "UPDATE PartidaRegistroReembolso SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and FolioGasto=@Folio";
                using (SqlConnection conexion = new SqlConnection(ObtenerCn()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(queryUpdate, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.ExecuteNonQuery();
                    }
                }

                using (SqlConnection conexion = new SqlConnection(ObtenerCn()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from ConceptoGlobalesGasto where Folio=@Folio and Partida=@Partida", conexion))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        mensaje = filasAfectadas > 0 ? "Eliminación exitosa" : "No se encontró ninguna fila para eliminar";
                    }
                }

                queryUpdate = "UPDATE ConceptoGlobalesGasto SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and Folio=@Folio";
                using (SqlConnection conexion = new SqlConnection(ObtenerCn()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(queryUpdate, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error." + ex.ToString();
            }
            return mensaje;
        }

        public string ObtenerTotalPartidaRegistroGasto(string Folio)
        {
            string maximo = "0";
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select isnull(max(Partida),0) as maximo from PartidaRegistroReembolso where FolioGasto='" + Folio + "'", conn))
            {
                conn.Open();
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

        public void ReciboSaldosReembolso(string txtFolio, Guna2TextBox txtSubtoral, Guna2TextBox txtDescuento, Guna2TextBox txtRecargo, Guna2TextBox txtIEPS, Guna2TextBox txtTotal, Guna2TextBox txtTotalPartidas, Guna2TextBox txtSaldo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas, IEPS from RegistroReembolso where Folio='" + txtFolio + "'", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtIEPS.Text = dr["TotalPartidas"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtSaldo.Text = dr["Saldo"].ToString();
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

        public void SeleccionarRecepcionProducto(ComboBox cb)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "Select (Clave + ' - ' + Nombre) as Nombre from Documento where /*TipoDocumento='Compra' and Clase='Compra' and*/ Tarea='Compras Reembolso'");
        }

        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Pedido a Proveedor'");
        }

        public void SeleccionarCondomini2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("GLOBAL");
            LlenarComboBox(cb, "Select Descripcion from Condominio");
        }

        public int ruta()
        {
            int contador = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Ruta from DatosEmpresa", conn))
                {
                    conn.Open();

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
                            Ruta = dt.Rows[0][0].ToString();
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

        public string[] InformacionProveedor(string Documento)
        {
            return ObtenerFila("Select * from Proveedor where IdProveedor= '" + Documento + "'", 1);
        }

        public void ConsultaGastos(string Folio, TextBox Documento, ComboBox Estatus, Guna2TextBox Fecha, Guna2TextBox Divisa, Guna2TextBox TipoCambio, Guna2TextBox Subtotal, Guna2TextBox Descuentos, Guna2TextBox Cargo, Guna2TextBox Total, Guna2TextBox Partidas, Guna2TextBox Notas, Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna2TextBox txtconsecutivo, Guna2TextBox txtReferencia, Guna2TextBox txtSaldo, Guna2TextBox DiasVence, Guna2TextBox FechaVence, Guna2TextBox Archivo, ComboBox CentroCosto, ComboBox cmbSemana, DateTimePicker dtpAnio, ComboBox pro, ComboBox proyecto, Guna2TextBox Tretenciones)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from RegistroReembolso where Folio='" + Folio + "'", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Documento.Text = dr["ClaveDocumento"].ToString();
                            Estatus.Text = dr["Estatus"].ToString();
                            Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                            Divisa.Text = dr["Divisa"].ToString();
                            TipoCambio.Text = dr["TipoCambio"].ToString();
                            Subtotal.Text = dr["Subtotal"].ToString();
                            Descuentos.Text = dr["Descuento"].ToString();
                            Cargo.Text = dr["Cargo"].ToString();
                            Total.Text = dr["Total"].ToString();
                            Partidas.Text = dr["TotalPartidas"].ToString();
                            Notas.Text = dr["Notas"].ToString();
                            Elaborado.Text = dr["Elaborado"].ToString();
                            txtSaldo.Text = dr["Saldo"].ToString();
                            MatriculaC = dr["ClaveProveedor"].ToString();
                            txtFolio.Text = dr["Folio"].ToString();
                            txtReciboCol.Text = dr["FolioOrden"].ToString();
                            txtconsecutivo.Text = dr["Consecutivo"].ToString();
                            txtReferencia.Text = dr["Referencia"].ToString();
                            DiasVence.Text = dr["DiasVence"].ToString();
                            FechaVence.Text = dr["FechaVence"].ToString();
                            Archivo.Text = dr["Archivo"].ToString();
                            CentroCosto.SelectedValue = dr["CentroCostos"].ToString();
                            dtpAnio.Value = new DateTime(Convert.ToInt32(dr["Anio"]), 1, 1); // si "Anio" es solo el año
                            pro.Text = dr["ProveedorAlterno"].ToString();
                            proyecto.Items.Add(dr["proyecto"].ToString());
                            proyecto.SelectedIndex = 0;

                            if (dr["Semana"] != DBNull.Value && int.TryParse(dr["Semana"].ToString(), out int semana))
                            {
                                if (semana > 0 && semana <= cmbSemana.Items.Count)
                                {
                                    cmbSemana.SelectedIndex = semana - 1;
                                }
                                else
                                {
                                    cmbSemana.SelectedIndex = -1;
                                }
                            }
                            else
                            {
                                cmbSemana.SelectedIndex = -1;
                            }

                            Tretenciones.Text = dr["TotalRetenciones"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ConsultaAbonoGasto(string Folio, Guna2TextBox txtAbono)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='G' order by Fecha desc", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtAbono.Text = dr["Pago"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string[] InformacionDocumento2(string Documento)
        {
            return ObtenerFila("Select * from Documento where Clave= '" + Documento + "'", 3, 2);
        }

        public void SeleccionarOrdenEntrega2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor");
        }

        public void SeleccionarProvedor2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor");
        }

        public string[] InformacionDocumento3(string Orden)
        {
            return ObtenerFila(" select OC.ClaveDocumento, D.Nombre from ordenCompra as OC, Documento as D,Proveedor as P where OC.ClaveDocumento=D.Clave and  OC.ClaveProveedor=P.IdProveedor and (convert(varchar,OC.Consecutivo) + ' - ' +  P.RazonSocial)= '" + Orden + "'", 0, 1);
        }

        public string[] InformacionCondominio2(string Matricula)
        {
            return ObtenerFila("Select Descripcion  from Condominio where ClaveCondominio= '" + Matricula + "'", 0);
        }

        public void CargarRecibosFiltroGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                LlenarGridRegistroReembolso(dgv, "select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor  ");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        public void CargarRecibosFiltroDocumentoGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                LlenarGridRegistroReembolso(dgv, "select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        public void CargarRecibosFiltroPGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                LlenarGridRegistroReembolso(dgv, "select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor ");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        public void obtenerRFC(string nombre, Guna2TextBox textBox)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("SELECT RFC FROM Proveedor where estatus ='Activo' and RazonSocial='" + nombre + "'", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            textBox.Text = dr[0].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Se conserva el comportamiento original: los errores se ignoran silenciosamente.
            }
        }

        public string insertaArchivos(string folioGasto, string partida, string clave, string nombreArchivo, string tipoArchivo, string ContenidoArchivo)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO PartidaRegistroReembolsoArchivos (FolioGasto, Partida, NombreArchivo, TipoArchivo, ContenidoArchivo) values ('" + folioGasto + "', '" + partida + "', '" + nombreArchivo + "', '" + tipoArchivo + "', '" + ContenidoArchivo + "')", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                mensaje = "Archivo Guardado.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }

        public void mostrarArchivos(DataGridView dgv, string Folio, string partida)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select secuencia, FolioGasto, Partida, ClaveProducto, NombreArchivo, TipoArchivo,ContenidoArchivo from PartidaRegistroReembolsoArchivos where FolioGasto = '" + Folio + "' and Partida = '" + partida + "'", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["secuencia"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["ClaveProducto"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["NombreArchivo"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["TipoArchivo"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["ContenidoArchivo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Registrar datos del aviso
        public string EliminarArchivosGastos(string Folio, string Partida, string Secuencia)
        {
            string resultado = "";
            try
            {
                int contador = 0;
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRegistroReembolsoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", conn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("delete PartidaRegistroReembolsoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                resultado = "Eliminado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

            return resultado;
        }

        public void ConsultaPartidaGasto(
            string Folio,
            string Partida,
            TextBox claveconcepto,
            ComboBox Concepto,
            Guna2TextBox Concepto2,
            Guna2TextBox cantidad,
            Guna2TextBox unidad,
            Guna2TextBox divisa,
            Guna2TextBox tipocambio,
            Guna2TextBox txtPrecio,
            Guna2TextBox total,
            TextBox archivo,
            ComboBox cmbProveedorAlterno,
            Guna2TextBox txtDescuentoIm,
            Guna2TextBox txtImpuestoIm,
            Guna2TextBox txtPrecioC,
            Guna2ComboBox cmdproyectoalterno,
            DateTimePicker dtfecha,
            Guna2ComboBox cmbformapago,
            Guna2ComboBox cmbreferencia,
            Guna2TextBox retencion,
            Guna2TextBox IEPS)
        {
            try
            {
                string query = @"
            SELECT P.ClaveProducto, P.Concepto2, P.Cantidad, P.Unidad, P.Divisa, 
                   P.TipoCambio, (P.Subtotal+P.DescuentoImporte) as Subtotal, P.Descuento, P.Total, P.Impuesto, 
                   P.Archivo, P.ProveedorAlterno, P.CentroCostosAlterno,
                   P.DescuentoImporte, P.ImpuestoImporte, C.Descripcion,
                   P.proyecto,P.Fechacompra,P.formadepago,P.referencia,P.Retencion,P.IEPS
                   FROM PartidaRegistroReembolso AS P
                    INNER JOIN Servicios AS C ON P.ClaveProducto = C.ClaveServicio
            WHERE P.FolioGasto = @Folio AND P.Partida = @Partida";

                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Partida", Partida);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.SelectedValue = dr["ClaveProducto"].ToString();
                            Concepto2.Text = dr["Concepto2"].ToString();
                            cantidad.Text = dr["Cantidad"].ToString();
                            unidad.Text = dr["Unidad"].ToString();
                            divisa.Text = dr["Divisa"].ToString();

                            decimal subtotal = Convert.ToDecimal(dr["Subtotal"]);
                            decimal descuentoUnitario = Convert.ToDecimal(dr["Descuento"]);
                            decimal cantidadValor = 0;
                            decimal.TryParse(cantidad.Text, out cantidadValor);

                            if (cantidadValor > 0)
                            {
                                // Precio unitario + descuento unitario (ya que el subtotal ya descuenta los descuentos globales)
                                decimal precioUnitario = (subtotal / cantidadValor) + descuentoUnitario;
                                txtPrecioC.Text = precioUnitario.ToString("N2");
                            }
                            else
                            {
                                txtPrecioC.Text = "0.00";
                            }

                            tipocambio.Text = dr["TipoCambio"].ToString();
                            txtPrecio.Text = dr["Subtotal"].ToString();
                            total.Text = dr["Total"].ToString();
                            archivo.Text = dr["Archivo"].ToString();

                            cmbProveedorAlterno.SelectedValue = dr["ProveedorAlterno"].ToString();
                            txtDescuentoIm.Text = dr["DescuentoImporte"].ToString();
                            txtImpuestoIm.Text = dr["ImpuestoImporte"].ToString();

                            cmdproyectoalterno.Items.Clear();
                            cmdproyectoalterno.Items.Add(dr["proyecto"].ToString());
                            if (cmdproyectoalterno.Items.Count > 0)
                            {
                                cmdproyectoalterno.SelectedIndex = 0;
                            }

                            cmbformapago.SelectedValue = dr["formadepago"].ToString();
                            cmbreferencia.Items.Add(dr["referencia"].ToString());
                            retencion.Text = dr["Retencion"].ToString();
                            IEPS.Text = dr["IEPS"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la partida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SeleccionarCatConceptosGlobales(ComboBox cb, string centrocostos)
        {
            cb.Items.Clear();
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select * from DatosProyecto where centrocostos='" + centrocostos + "' ", conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[3].ToString());
                    }
                }
            }
        }

        public DataTable ObtenerFormasPagoPorProyecto(string centro, string proyecto)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT * " +
                    "FROM FormaPagoProyecto AS fp " +
                    "INNER JOIN DatosProyecto dp ON fp.Folio = dp.Folio " +
                    "WHERE dp.CentroCostos = @centro AND dp.Proyecto = @proyecto", conn))
                {
                    cmd.Parameters.AddWithValue("@centro", centro);
                    cmd.Parameters.AddWithValue("@proyecto", proyecto);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener formas de pago: " + ex.Message);
            }

            return dt;
        }

        public void SeleccionarReferencia(ComboBox cb, string centro, string proyecto, string formapago)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "select DescripcionReferencia from FormaPagoProyecto as fp inner join DatosProyecto dp on fp.folio = dp.folio and CentroCostos ='" + centro + "' and Proyecto='" + proyecto + "' and DescripcionFormaPago='" + formapago + "'");
        }

        public void SeleccionarCatConceptosGlobales(ComboBox cb)
        {
            cb.Items.Clear();
            LlenarComboBox(cb, "select importe from ConceptosGlobales");
        }
    }
}