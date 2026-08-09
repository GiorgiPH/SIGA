using PV.Clases.OrdenCompra;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Condominios;
using ControlAcademico;

namespace PV.Clases.OrdenCompra
{
    class DBRegistroReembolso
    {
    
    SqlConnection cn;
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;

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


        //___________________________________________________________________________________________
         public void ActualizarReembolso(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {
                cmd = new SqlCommand("Update RegistroReembolso set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarSaldoProveedor2(string Clave, decimal Saldo)
        {
            try
            {
                cmd = new SqlCommand("Update Proveedor set Saldo= Saldo + " + Saldo + " where IdProveedor=" + Clave, cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarGasto(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
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
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //____________________________________________________________________________________________
        public string CancelarRegistroGasto(string folio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand cmd = new SqlCommand("CancelarRegistroReembolso", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetro de entrada
                        cmd.Parameters.AddWithValue("@FolioRegistro", folio);

                        // Parámetro de salida para el mensaje
                        SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200);
                        mensajeParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(mensajeParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Captura el mensaje de resultado
                        string mensaje = mensajeParam.Value?.ToString();
                        return mensaje ?? "No se recibió ningún mensaje del procedimiento almacenado.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar el error de manera diferente si no quieres usar MessageBox en esta capa
                return "Error: " + ex.Message;
            }

        }
        //____________________________________________________________________________________________
        //Registrar datos del aviso
        public void ModificarExtension4(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroReembolso where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RegistroReembolso set Extension='', Archivo='' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        public void InsertarRegistroGasto(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia, string DiasVence, string FechaVence, string centrocosto, int semana, string anio, string proveedorAlterno,string proyecto,string totalRetenciones)
        {
            try
            {
                int nuevoFolio = 1;
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) + 1 FROM RegistroReembolso", cn))
                {
                    nuevoFolio = (int)cmd.ExecuteScalar();
                }

                txtFolio.Text = nuevoFolio.ToString();

                string orden = string.IsNullOrEmpty(RecepcionProducto) ? "0" : RecepcionProducto;

                string query = @"INSERT INTO RegistroReembolso 
                        (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, DiasVence, FechaVence, CentroCostos, Semana, Anio, ProveedorAlterno,Proyecto,TotalRetenciones)
                        VALUES 
                        (@Folio, @ClaveDocumento, @Estatus, @Fecha, @ClaveProveedor, @Divisa, @TipoCambio, @Notas, @Elaborado, @FolioOrden, @Consecutivo, @Referencia, @DiasVence, @FechaVence, @CentroCostos, @Semana, @Anio, @ProveedorAlterno,@proyecto,@TotalRetenciones)";

                using (SqlCommand cmdInsert = new SqlCommand(query, cn))
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        //____________________________________________________________________________________________
        //Registrar datos del aviso
        public void ModificarExtension5gasto(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroReembolso where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    cmd = new SqlCommand("Update RegistroReembolso set Extension='" + Extension + "' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarRecepcion3gasto(string Folio, string Partida, string Archivo)
        {
            try
            {
                cmd = new SqlCommand("Update RegistroReembolso set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________
          public string[] InformacionDocumento(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[3].ToString(),
                     dr[2].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //____________________________________________________________________________________________
      
        public void ConsecutivoGasto(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from RegistroReembolso where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarOrdenEntrega(ComboBox cb, string Filtro, string Proveedor)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand(" Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" + Proveedor + "' and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void ConsultaGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {
                cmd = new SqlCommand("Select top 1 * from PartidaRegistroReembolso where FolioGasto='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;
                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________
        public string[] InformacionGastoo(string Producto, string Orden)
        {
            dr.Close();
            cmd = new SqlCommand("select PA.*, P.Descripcion, P.ClaveProducto, P.TipoCosteo from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and P.Descripcion= '" + Producto + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                   dr[15].ToString(),

                     dr[8].ToString(),
                     dr[11].ToString(),
                      dr[9].ToString(),
                       dr[10].ToString(),
                        dr[3].ToString(),
                         dr[4].ToString(),
                          dr[1].ToString(),
                            dr[12].ToString(),
                             dr[13].ToString(),
                             dr[5].ToString(),
                              dr[16].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //____________________________________________________________________________________________
        public string[] InformacionGasto(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from Servicios where Descripcion= '" + Recibo + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                    dr[2].ToString(),
                     dr[17].ToString(),
                         dr[5].ToString(),
                          dr[21].ToString(),
                          dr[14].ToString(),
                           dr[16].ToString(),
                           dr["IEPS"].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension4gasto(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroReembolso where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    cmd = new SqlCommand("Update PartidaRegistroReembolso set Extension='" + Extension + "' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion2gasto(string Folio, string Partida, string Archivo)
        {
            try
            {
                cmd = new SqlCommand("Update PartidaRegistroReembolso set  Archivo='" + Archivo + "' where FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn);
                cmd.ExecuteNonQuery();
           }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension3gasto(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroReembolso where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    cmd = new SqlCommand("Update PartidaRegistroReembolso set Extension='', Archivo='' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        public void InsertarPartidaGasto(
      string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad,
      string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento,
      decimal Total, decimal Impuesto, string archivo, string proveedorAlterno, string centroCostosAlterno, string DescuentoImporte, string ImpuestoImporte,
      string proyecto,string Fechacompra,string formadepago, string referencia, string IEPS, string Retencion,string precio)
        {
            try
            {
                // Verifica si existe la partida
                string queryCheck = "SELECT COUNT(*) FROM PartidaRegistroReembolso WHERE FolioGasto = @Folio AND Partida = @Partida";
                cmd = new SqlCommand(queryCheck, cn);
                cmd.Parameters.AddWithValue("@Folio", Folio);
                cmd.Parameters.AddWithValue("@Partida", Partida);
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                  //  MessageBox.Show("count:" + count);
                    // Si existe, actualiza
                    string updateQuery = @"UPDATE PartidaRegistroReembolso SET 
                ClaveProducto = @ClaveRecibo, Concepto2 = @Concepto2, Cantidad = @Cantidad,
                Unidad = @Unidad, Divisa = @Divisa, TipoCambio = @TipoCambio, Subtotal = @Subtotal,
                Descuento = @Descuento, Total = @Total, Impuesto = @Impuesto, Archivo = @Archivo,
                ProveedorAlterno = @ProveedorAlterno, CentroCostosAlterno = @CentroCostosAlterno, DescuentoImporte=@DescuentoImporte, ImpuestoImporte=@ImpuestoImporte,
proyecto=@proyecto,Fechacompra=@Fechacompra,formadepago=@formadepago,referencia=@referencia, IEPS = @IEPS,Retencion=@Retencion,precio=@precio 
                WHERE FolioGasto = @Folio AND Partida = @Partida";

                    cmd = new SqlCommand(updateQuery, cn);
                }
                else
                {
                    // Si no existe, inserta
                    string insertQuery = @"INSERT INTO PartidaRegistroReembolso
                (FolioGasto, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, 
                Subtotal, Descuento, Total, Impuesto, Archivo, ProveedorAlterno, CentroCostosAlterno, DescuentoImporte, ImpuestoImporte,
proyecto,Fechacompra,formadepago,referencia, IEPS,Retencion,precio)
                
VALUES 
                (@Folio, @Partida, @ClaveRecibo, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio, 
                @Subtotal, @Descuento, @Total, @Impuesto, @Archivo, @ProveedorAlterno, @CentroCostosAlterno,@DescuentoImporte, @ImpuestoImporte,
                @proyecto,@Fechacompra,@formadepago,@referencia, @IEPS,@Retencion,@precio)";

                    cmd = new SqlCommand(insertQuery, cn);
                }

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
            catch (Exception ex)
            {
              //  MessageBox.Show(" error al insertar :");
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //___________________________________________________________________________________________
        public void ActualizarPartidaOrden(string Folio, string Partida, string Cantidad)
        {
            try
            {

                cmd = new SqlCommand("Update PartidaOrden set CantidadRecibida= CantidadRecibida - '" + Cantidad + "' where FolioOrden='" + Folio + "' and  Partida='" + Partida + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ActualizarGasto(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand(@"SELECT 
                          ISNULL(SUM(DescuentoImporte), 0) AS Descuento, 
                          ISNULL(SUM(ImpuestoImporte), 0) AS Impuesto, 
                          ISNULL(SUM(IEPS), 0) AS IEPS, 
                          ISNULL(SUM(Subtotal), 0) AS Subtotal, 
                          ISNULL(SUM(Total), 0) AS Total, 
                          COUNT(*) AS TotalPartidas,
                          ISNULL(SUM(Retencion), 0) AS Retencion
                       FROM PartidaRegistroReembolso 
                       WHERE FolioGasto = @Folio", cn);

                cmd.Parameters.AddWithValue("@Folio", txtFolio);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = dr["Impuesto"].ToString();
                    string IEPS = dr["IEPS"].ToString();
                    string TotalPartidas = dr["TotalPartidas"].ToString();
                    string TotalRetenciones = dr["Retencion"].ToString();

                    dr.Close();
                    cmd = new SqlCommand("Update RegistroReembolso set TotalPartidas='" + TotalPartidas + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Impuesto + "', IEPS = '"+IEPS+"', Total='" + Total + "', Saldo='" + Total + "', TotalRetenciones='" + TotalRetenciones + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void Consulta5RegistroGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from PartidaRegistroReembolso where FolioGasto='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasGasto(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna2TextBox txtImpuesto, Guna2TextBox txtIEPS, Guna2TextBox txtRetencion, Guna2TextBox txtPartidas)
        {
            try
            {

                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(DescuentoImporte) as Descuento,sum(ImpuestoImporte) as Impuesto, sum(Total) as Total, sum(IEPS) as IEPS, sum(retencion) as retencion, count(*) as Partidas from PartidaRegistroReembolso where FolioGasto='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

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
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb, string Orden)
        {

            cb.Items.Clear();
            //cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "'", cn);
            cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, Servicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        public DataTable ObtenerProductosGastoPorOrden(string folioOrden)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT P.ClaveProducto AS ClaveServicio, P.Descripcion
                         FROM PartidaOrden AS PA
                         INNER JOIN Servicios AS P ON PA.ClaveProducto = P.ClaveProducto
                         WHERE PA.FolioOrden = @FolioOrden
                         AND PA.CantidadRecibida > 0
                         AND P.Estatus = 'Activo'";

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

        //___________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb)
        {
            
            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from Servicios where Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();

       
        }
        public DataTable ObtenerProductosGasto()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Servicios WHERE Estatus = 'Activo'", cn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes lanzar o registrar el error
                throw new Exception("Error al obtener productos de gasto: " + ex.Message);
            }

            return dt;
        }

        //_______________________________________________________
        public void ObtenerPartidasReembolso(string folio, DataGridView dgv)
        {
            try
            {
                /*
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT PO.*,
                     PS.Descripcion,
                     CG.Clave,
                     CG.Clase,
                     CG.Tipo,
                     CGG.Cargo as CargoConcepto,
                     CGG.Descuento as DescuentoConcepto,
                     PO.Cantidad,  
                     PO.precio,    
                     CGG.Descuento 
              FROM PartidaRegistroReembolso AS PO
              Left Join ConceptoGlobalesGasto as CGG On PO.FolioGasto=CGG.Folio and PO.Partida=CGG.Partida
              LEFT JOIN ConceptosGlobales as CG On CG.Clave=CGG.ClaveConceptoG
              JOIN servicios AS PS ON PO.ClaveProducto = PS.ClaveServicio
              WHERE PO.FolioGasto = @Folio
              ORDER BY Partida ASC", cn);

                da.SelectCommand.Parameters.AddWithValue("@Folio", folio);

                da.Fill(dt);
                return dt;*/

                dgv.Rows.Clear();
                //da = new SqlDataAdapter("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida asc", cn);
                da = new SqlDataAdapter("SELECT PO.*,PS.Descripcion,CG.Clave,CG.Clase,CG.Tipo,CGG.Cargo as CargoConcepto,CGG.Descuento as DescuentoConcepto,PO.Cantidad,  PO.precio,    CGG.Descuento FROM PartidaRegistroReembolso AS PO Left Join ConceptoGlobalesGasto as CGG On PO.FolioGasto=CGG.Folio and PO.Partida=CGG.Partida LEFT JOIN ConceptosGlobales as CG On CG.Clave=CGG.ClaveConceptoG JOIN servicios AS PS ON PO.ClaveProducto = PS.ClaveServicio WHERE PO.FolioGasto = '"+folio+"' ORDER BY Partida ASC", cn);
                dt = new DataTable();
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

                    // IMPORTANT: You have two 'Descuento' fields in your SQL: PO.Descuento and CGG.Descuento as DescuentoConcepto.
                    // Ensure you're mapping the correct one here based on your intent.
                    // I'm using the original 'Descuento' from PO.* for cell 9.
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
            catch (Exception ex)
            {
                // For better debugging, throw the original exception or log it fully
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }



        public void ObtenerPartidasReembolso2(string folio, DataGridView dgv)
        {
            try
            {


                dgv.Rows.Clear();
                //da = new SqlDataAdapter("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida asc", cn);
                //
                //da = new SqlDataAdapter("SELECT PO.FolioGasto, CG.Clase, '' AS ImpuestoPorcentaje, SUM(CASE WHEN CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = 'IVA16' THEN PO.Precio * PO.Cantidad - PO.DescuentoImporte WHEN CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = 'IVAE' THEN PO.Precio * PO.Cantidad - PO.DescuentoImporte WHEN CG.Clase = 'Descuento' THEN PO.Precio * PO.Cantidad - PO.DescuentoImporte ELSE 0 END) AS Subtotal, SUM(CASE WHEN CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = 'IVA16' THEN PO.ImpuestoImporte WHEN CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = 'IVAE' THEN PO.ImpuestoImporte ELSE 0 END) AS importeImpuesto, SUM(PO.Retencion) AS Retencion, CGG.ClaveConceptoG FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG JOIN servicios AS PS ON PO.ClaveProducto = PS.ClaveServicio WHERE PO.FolioGasto = '"+folio+"' AND ((CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = cg.Clave) OR (CG.Clase = 'Impuesto' AND CGG.ClaveConceptoG = cg.Clave) OR (CG.Clase = 'Descuento' AND PO.Retencion > 0 AND CGG.ClaveConceptoG = cg.Clave)) GROUP BY PO.FolioGasto, CG.Clase, CGG.ClaveConceptoG ORDER BY FolioGasto, Clase, CGG.ClaveConceptoG", cn);
                //                da = new SqlDataAdapter("SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clave = 'IVA16' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, cg.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.IEPS) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND PO.IEPS > 0 GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.IEPS) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clave = 'IVA8' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, 0.00 AS ImporteImpuesto, SUM(PO.Retencion) AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO INNER JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida INNER JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clase = 'Descuento' AND CG.Clave LIKE 'R%' AND PO.Retencion > 0 GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.Retencion) > 0 ORDER BY FolioGasto, ClaseConceptoAgrupada, Tipo;", cn);
                da = new SqlDataAdapter("SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clave = 'IVA16' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, cg.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.IEPS) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO, ConceptosGlobales AS CG WHERE PO.FolioGasto = '"+folio+"' AND PO.IEPS > 0 AND CG.Clave = 'IEPS' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.IEPS) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '181' AND CG.Clave = 'IVA8' GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal - PO.DescuentoImporte) AS SubtotalCalculado, 0.00 AS ImporteImpuesto, SUM(PO.Retencion) AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO INNER JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida INNER JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clase = 'Descuento' AND CG.Clave LIKE 'R%' AND PO.Retencion > 0 GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.Retencion) > 0 UNION ALL SELECT PO.FolioGasto, CG.Clase AS ClaseConceptoAgrupada, SUM(PO.Subtotal) AS SubtotalCalculado, SUM(PO.ImpuestoImporte) AS ImporteImpuesto, 0.00 AS ImporteRetencion, CG.Clave AS Tipo FROM PartidaRegistroReembolso AS PO LEFT JOIN ConceptoGlobalesGasto AS CGG ON PO.FolioGasto = CGG.Folio AND PO.Partida = CGG.Partida LEFT JOIN ConceptosGlobales AS CG ON CG.Clave = CGG.ClaveConceptoG WHERE PO.FolioGasto = '"+folio+"' AND CG.Clave not in ('IEPS','IVA8','IVA16') GROUP BY PO.FolioGasto, CG.Clase, CG.Clave HAVING SUM(PO.ImpuestoImporte) = 0 ORDER BY FolioGasto, ClaseConceptoAgrupada, Tipo;", cn);
                
                dt = new DataTable();
                
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
            catch (Exception ex)
            {
                // For better debugging, throw the original exception or log it fully
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }

        public void CargarRecibosPartidasGasto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                //da = new SqlDataAdapter("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida asc", cn);
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRegistroReembolso as PO, servicios as PS where PO.FolioGasto='" + Folio + "' and PO.ClaveProducto=PS.ClaveServicio order by Partida asc", cn);
                dt = new DataTable();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*    public DataTable ObtenerPartidasReembolso(string folio)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        @"SELECT PO.*, PS.Descripcion, CG.Clave, CG.Clase,CG.Tipo, CGG.Cargo as CargoConcepto, CGG.Descuento as DescuentoConcepto
                       PO.Cantidad,
                        PS.Precio,
                        CGG.Descuento
                        FROM PartidaRegistroReembolso AS PO 
                          Left Join ConceptoGlobalesGasto as CGG On PO.FolioGasto=CGG.Folio and PO.Partida=CGG.Partida
                          LEFT JOIN ConceptosGlobales as CG On CG.Clave=CGG.ClaveConceptoG
                          JOIN servicios AS PS ON PO.ClaveProducto = PS.ClaveServicio 
                          WHERE PO.FolioGasto = @Folio 
                          ORDER BY Partida ASC", cn);

                    da.SelectCommand.Parameters.AddWithValue("@Folio", folio);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    // Puedes manejar mejor el error o lanzarlo para la capa superior
                    throw new Exception("Error al obtener partidas de reembolso", ex);
                }
            }
        */

      
        //___________________________________________________________________________
        public string EliminarPartidaRegistroGasto(string Folio, string Partida)
        {
            int contador = 0;
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from PartidaRegistroReembolso where FolioGasto=@Folio and Partida=@Partida", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Eliminación exitosa";
                        }
                        else
                        {
                            mensaje = "No se encontró ninguna fila para eliminar";
                        }
                    }

                }
                string queryUpdate = "UPDATE PartidaRegistroReembolso SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and FolioGasto=@Folio";

                using (SqlConnection connection = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        command.Parameters.AddWithValue("@Folio", Folio);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from ConceptoGlobalesGasto where Folio=@Folio and Partida=@Partida", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Eliminación exitosa";
                        }
                        else
                        {
                            mensaje = "No se encontró ninguna fila para eliminar";
                        }
                    }
                }
                queryUpdate = "UPDATE ConceptoGlobalesGasto SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and Folio=@Folio";

                using (SqlConnection connection = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        command.Parameters.AddWithValue("@Folio", Folio);
                        connection.Open();
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
        //___________________________________________________________________________________________
        public string ObtenerTotalPartidaRegistroGasto(string Folio)
        {
            string maximo = "0";
            cmd = new SqlCommand("select isnull(max(Partida),0) as maximo from PartidaRegistroReembolso where FolioGasto='" + Folio + "'", cn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                maximo = dr[0].ToString();
            }
            dr.Close();
            return maximo;
        }

        //___________________________________________________________________________________________

        public void ReciboSaldosGastos(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtRecargo, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtTotalPartidas, Guna.UI2.WinForms.Guna2TextBox txtSaldo)
        {
            try
            {
                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from RegistroReembolso where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtRecargo.Text = dr["Cargo"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        public void SeleccionarRecepcionProducto(ComboBox cb)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Compra'", cn);
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where /*TipoDocumento='Compra' and Clase='Compra' and*/ Tarea='Compras Reembolso'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Pedido a Proveedor'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondomini2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("GLOBAL");
            cmd = new SqlCommand("Select Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________

        //_________________________________________________________________________________________
        public int ruta()
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select Ruta from DatosEmpresa", cn);
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
                    Ruta = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
        //_________________________________________________________________________________________
        public string[] InformacionProveedor(string Documento)
        {
            cmd = new SqlCommand("Select * from Proveedor where IdProveedor= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[1].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________-
        public void ConsultaGastos(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna.UI2.WinForms.Guna2TextBox txtconsecutivo, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtSaldo, Guna.UI2.WinForms.Guna2TextBox DiasVence, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Archivo, ComboBox CentroCosto, ComboBox cmbSemana, DateTimePicker dtpAnio, ComboBox pro,ComboBox proyecto, Guna.UI2.WinForms.Guna2TextBox Tretenciones )
        {
        try{
                cmd = new SqlCommand("Select * from RegistroReembolso where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
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
                    //cmbSemana.SelectedIndex = int.Parse(dr["Semana"].ToString());
                    dtpAnio.Value = new DateTime(Convert.ToInt32(dr["Anio"]), 1, 1); // si "Anio" es solo el año
                    pro.Text = dr["ProveedorAlterno"].ToString();
                    proyecto.Items.Add ( dr["proyecto"].ToString());
                    proyecto.SelectedIndex = 0;
                    if (dr["Semana"] != DBNull.Value && int.TryParse(dr["Semana"].ToString(), out int semana))
                    {
                        if (semana > 0 && semana <= cmbSemana.Items.Count)
                            cmbSemana.SelectedIndex = semana - 1;
                        else
                            cmbSemana.SelectedIndex = -1; // o cualquier valor por defecto
                    }
                    else
                    {
                        cmbSemana.SelectedIndex = -1; // o lo que tú quieras como fallback
                    }
                    Tretenciones.Text = dr["TotalRetenciones"].ToString();
                }
                dr.Close();
           }
            catch (Exception ex)
            {   dr.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        //______________________________________________________________________________________________
        public void ConsultaAbonoGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtAbono)
        {
            try
            {
                cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='G' order by Fecha desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAbono.Text = dr["Pago"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //__________________________________________________________________________________________________________-
        public string[] InformacionDocumento2(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where Clave= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[3].ToString(),
                     dr[2].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_______________________________________________________________________________________________________________________________------
        public void SeleccionarOrdenEntrega2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProvedor2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________
        public string[] InformacionDocumento3(string Orden)
        {
            cmd = new SqlCommand(" select OC.ClaveDocumento, D.Nombre from ordenCompra as OC, Documento as D,Proveedor as P where OC.ClaveDocumento=D.Clave and  OC.ClaveProveedor=P.IdProveedor and (convert(varchar,OC.Consecutivo) + ' - ' +  P.RazonSocial)= '" + Orden + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                     dr[1].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________________________
        public string[] InformacionCondominio2(string Matricula)
        {
            cmd = new SqlCommand("Select Descripcion  from Condominio where ClaveCondominio= '" + Matricula + "'", cn);
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
        //_______________________________________________________________________________________________________________
        public void CargarRecibosFiltroGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor  ", cn);
                dt = new DataTable();
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
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocumentoGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
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
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroPGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroReembolso as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor ", cn);
                dt = new DataTable();
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
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void obtenerRFC(string nombre, Guna2TextBox textBox)
        {


            try
            {

                cmd = new SqlCommand("SELECT RFC FROM Proveedor where estatus ='Activo' and RazonSocial='" + nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox.Text = dr[0].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
            //    MessageBox.Show("Error." + ex.ToString());
            }
            dr.Close();

        }
        //__________________________________________________________________________________________________________-

        public string insertaArchivos(string folioGasto, string partida, string clave, string nombreArchivo, string tipoArchivo, string ContenidoArchivo)
        {

            string mensaje = string.Empty;
            int contador = 0;

            try
            {

                dr.Close();

                cmd = new SqlCommand("INSERT INTO PartidaRegistroReembolsoArchivos (FolioGasto, Partida, NombreArchivo, TipoArchivo, ContenidoArchivo) values ('" + folioGasto + "', '" + partida + "', '" + nombreArchivo + "', '" + tipoArchivo + "', '" + ContenidoArchivo + "')", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Archivo Guardado.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }

        //__________________________________________________________________________________________________________-
        //__________________________________________________________________________________________________________-
        //__________________________________________________________________________________________________________-
        public void mostrarArchivos(DataGridView dgv, string Folio, string partida)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select secuencia, FolioGasto, Partida, ClaveProducto, NombreArchivo, TipoArchivo,ContenidoArchivo from PartidaRegistroReembolsoArchivos where FolioGasto = '" + Folio + "' and Partida = '" + partida + "'", cn);
                dt = new DataTable();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Registrar datos del aviso
        public string EliminarArchivosGastos(string Folio, string Partida, string Secuencia)
        {
            int contador = 0;
            string resultado = "";
            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroReembolsoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("delete PartidaRegistroReembolsoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();

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
   Guna.UI2.WinForms.Guna2TextBox Concepto2,
   Guna.UI2.WinForms.Guna2TextBox cantidad,
   Guna.UI2.WinForms.Guna2TextBox unidad,
   Guna.UI2.WinForms.Guna2TextBox divisa,
   Guna.UI2.WinForms.Guna2TextBox tipocambio,
   Guna.UI2.WinForms.Guna2TextBox txtPrecio,
   //Guna.UI2.WinForms.Guna2TextBox descuento,
   Guna.UI2.WinForms.Guna2TextBox total,
   //Guna.UI2.WinForms.Guna2ComboBox txtImpuestos,
   TextBox archivo,
   ComboBox cmbProveedorAlterno,
   Guna2TextBox txtDescuentoIm,
   Guna2TextBox txtImpuestoIm,
   Guna2TextBox txtPrecioC,
            Guna.UI2.WinForms.Guna2ComboBox cmdproyectoalterno,
            DateTimePicker dtfecha,
            Guna.UI2.WinForms.Guna2ComboBox cmbformapago,
            Guna2ComboBox cmbreferencia, 
            Guna.UI2.WinForms.Guna2TextBox retencion,
            Guna.UI2.WinForms.Guna2TextBox IEPS
            )
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

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Partida", Partida);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
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
                            //descuento.Text = dr["Descuento"].ToString();
                            total.Text = dr["Total"].ToString();
                            //txtImpuestos.Items.Add(dr["Impuesto"].ToString());
                            archivo.Text = dr["Archivo"].ToString();

                            // Evita error si no existen los valores en el ComboBox
                            cmbProveedorAlterno.SelectedValue = dr["ProveedorAlterno"].ToString();
                            //cmbCentroCostosAlterno.SelectedValue = dr["CentroCostosAlterno"].ToString();
                            txtDescuentoIm.Text = dr["DescuentoImporte"].ToString();
                            txtImpuestoIm.Text = dr["ImpuestoImporte"].ToString();
                            cmdproyectoalterno.Items.Clear();
                            cmdproyectoalterno.Items.Add(dr["proyecto"].ToString());                        // dtfecha.Text = dr["fecha"].ToString();
                            if (cmdproyectoalterno.Items.Count > 0)
                            {
                                cmdproyectoalterno.SelectedIndex = 0;
                            }
                            //cmbformapago.Items.Add(dr["formadepago"].ToString());
                            //if (cmbformapago.Items.Count > 0)
                            //{
                            //    cmbformapago.SelectedIndex = 0;
                            //}
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
        //**********************************************************************************
        public void SeleccionarCatConceptosGlobales(ComboBox cb,string centrocostos)
        {
            cb.Items.Clear();
            // cb.Items.Add("TODOS");
            cmd = new SqlCommand("select * from DatosProyecto where centrocostos='" + centrocostos + "' ", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[3].ToString());
            }
            dr.Close();
        }

        public DataTable ObtenerFormasPagoPorProyecto(string centro, string proyecto)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT * " +
                    "FROM FormaPagoProyecto AS fp " +
                    "INNER JOIN DatosProyecto dp ON fp.Folio = dp.Folio " +
                    "WHERE dp.CentroCostos = @centro AND dp.Proyecto = @proyecto", cn))
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

        public void SeleccionarReferencia(ComboBox cb, string centro, string proyecto,string formapago)
        {
            cb.Items.Clear();
            // cb.Items.Add("TODOS");
            cmd = new SqlCommand("select DescripcionReferencia from FormaPagoProyecto as fp inner join DatosProyecto dp on fp.folio = dp.folio and CentroCostos ='" + centro + "' and Proyecto='" + proyecto + "' and DescripcionFormaPago='"+formapago+"'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        public void SeleccionarCatConceptosGlobales(ComboBox cb)
        {
            cb.Items.Clear();
            // cb.Items.Add("TODOS");
            cmd = new SqlCommand("select importe from ConceptosGlobales", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

    }
}


