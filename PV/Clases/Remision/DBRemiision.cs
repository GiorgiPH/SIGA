using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.Remision
{
    /// <summary>
    /// Capa de datos de Remision. Mismas firmas, parámetros y lógica que la
    /// clase original.
    ///
    /// NOTA sobre el refactor de conexiones: el constructor original abría
    /// una SqlConnection y la dejaba abierta durante toda la vida del
    /// objeto (nunca se cerraba). Ahora cada método abre su propia conexión
    /// dentro de un "using", igual que en el resto de las clases DB del
    /// proyecto (DBFacturas, DBProductosServicios, DBRegistrarIngresos).
    /// También se parametrizaron las consultas que antes concatenaban texto
    /// directamente (Folio, Matricula) para evitar inyección SQL.
    /// </summary>
    public class DBRemiision
    {
        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public void ConsultaTotalRemision(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from Remision where Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", Folio);
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

        /// <summary>
        /// Carga las Remisiones con saldo pendiente (Saldo != 0) de un
        /// cliente. Columnas por índice: 1=ClaveDocumento, 2=Folio,
        /// 3=Consecutivo, 5=Nombre, 6=Fecha, 7=Total, 8=Saldo, 9=FechaVence
        /// (igual que la versión original).
        ///
        /// Cada fila agregada queda marcada en su .Tag con "Remision", para
        /// poder distinguirla si el grid se combina con Facturas
        /// (DBFacturas.CargarFacturaCobro) — Factura.Folio y Remision.Folio
        /// son secuencias independientes y pueden coincidir.
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Si vas a
        /// combinar con CargarFacturaCobro en el mismo grid, limpia una
        /// sola vez desde el formulario y llama a ambos con
        /// limpiarPrimero=false.
        /// </param>
        /// <param name="claveCentroCostos">
        /// Clave de Centro de Costos para filtrar (int como string). Si
        /// viene null, vacio, o "0" (fila "TODOS" del combo), no se filtra
        /// y se traen las Remisiones de todos los centros de costos.
        /// </param>
        public void CargarRemisionCobro(DataGridView dgv, string Matricula, bool limpiarPrimero = true, string claveCentroCostos = null)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                if (limpiarPrimero)
                {
                    dgv.Rows.Clear();
                }

                const string sql = @"
                    SELECT R.*, D.Nombre
                    FROM Remision AS R, Documento AS D
                    WHERE ClaveProveedor = @Matricula AND Saldo <> 0 AND R.ClaveDocumento = D.Clave
                      AND (@CentroCostos IS NULL OR R.CentroCostos = @CentroCostos)";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@Matricula", Matricula);

                    object valorCentroCostos = (string.IsNullOrWhiteSpace(claveCentroCostos) || claveCentroCostos == "0")
                        ? (object)DBNull.Value
                        : Convert.ToInt32(claveCentroCostos);
                    cmd.Parameters.AddWithValue("@CentroCostos", valorCentroCostos);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        decimal Total = Convert.ToDecimal(item["Total"]);

                        dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[7].Value = Total;
                        dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[9].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");

                        dgv.Rows[n].Tag = "Remision";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        /// <summary>
        /// Carga en la grilla de RegistrarCobro los folios de Remision
        /// seleccionados en registroIngresos. Cada fila agregada queda
        /// marcada con .Tag = "Remision" (ver la nota equivalente en
        /// DBFacturas.CargarFacturaCobroById sobre por qué es necesario).
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Si vas a
        /// combinar con DBFacturas.CargarFacturaCobroById en el mismo grid,
        /// limpia una sola vez desde el formulario y llama a ambos con
        /// limpiarPrimero=false.
        /// </param>
        public void CargarReciboCobroById(DataGridView dgv, string Matricula, ArrayList ListaConcep, bool limpiarPrimero = true)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                if (limpiarPrimero)
                {
                    dgv.Rows.Clear();
                }

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    foreach (object item2 in ListaConcep)
                    {
                        string concepto = item2.ToString();

                        const string sql = @"
                            SELECT R.*, 0.00 AS DescuentoPago, 0.00 AS RecargosAcumulados, D.Nombre
                            FROM Remision AS R, Documento AS D
                            WHERE ClaveProveedor = @Matricula AND R.Folio = @Folio AND R.ClaveDocumento = D.Clave";

                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@Matricula", Matricula);
                            cmd.Parameters.AddWithValue("@Folio", concepto);

                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow item in dt.Rows)
                            {
                                int n = dgv.Rows.Add();
                                dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                                dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                                dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                                dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                                dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);

                                dgv.Rows[n].Tag = "Remision";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        public void ActualizarRemision(string Folio, decimal Recargos, decimal Descuento, decimal Saldo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(
                    "Update Remision set DescuentoPago = @Descuento, Saldo = Saldo - @Saldo where Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Descuento", Descuento);
                    cmd.Parameters.AddWithValue("@Saldo", Saldo);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public string[] InformacionRemision(string Orden)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "select Folio, ClaveDocumento, Fecha, ClaveProveedor, Consecutivo from Remision as OC where Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Orden);
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
                            dr["Fecha"].ToString(),
                            dr["ClaveProveedor"].ToString(),
                            dr["Consecutivo"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }

        }
        public string GenerarXmlRemision(string folio)
        {
            const string sqlRemision = @"
        SELECT R.Folio, R.ClaveDocumento, D.Nombre AS NombreDocumento, R.Estatus, R.Fecha,
               R.Divisa, R.TipoCambio, R.Subtotal, R.Descuento, R.Cargo, R.Total,
               R.TotalPartidas, R.Notas, R.Elaborado, R.Saldo, R.FolioOrden, R.Consecutivo,
               R.Almacen, R.Referencia, R.FechaVence, R.Autorizado, R.CentroCostos, R.IdProyecto,
               C.IdCliente, C.RazonSocial, C.RFC, C.Calle, C.NoExterior, C.NoInterior,
               C.Colonia, C.Municipio, C.CodigoPostal, C.Ciudad, C.Pais
        FROM Remision AS R
        INNER JOIN Documento AS D ON R.ClaveDocumento = D.Clave
        LEFT JOIN Clientes AS C ON R.ClaveProveedor = C.IdCliente
        WHERE R.Folio = @Folio";

            const string sqlEmpresa = @"
        SELECT TOP 1 RazonSocial, NombreComercial, RFC, Telefono1, Correo, PaginaWeb,
               CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais
        FROM DatosEmpresa";

            const string sqlPartidas = @"
        SELECT PR.Partida, PR.ClaveProducto, PR.Concepto2, PR.Cantidad, PR.Unidad,
               PR.Divisa, PR.TipoCambio, PR.Subtotal, PR.Descuento, PR.Total,
               PR.Precio, PR.CantidadRecibida, PR.Impuesto,
               PS.Descripcion AS DescripcionProducto, PS.Alias, PS.UnidadMedida
        FROM PartidaRemision AS PR
        LEFT JOIN ProductosServicios AS PS ON PR.ClaveProducto = PS.ClaveProducto
        WHERE PR.FolioRemision = @Folio
        ORDER BY PR.Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();

                // --- Datos de la Remision (encabezado) ---
                DataRow remisionRow = null;
                using (SqlCommand cmd = new SqlCommand(sqlRemision, cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                            return null; // no existe esa remision

                        remisionRow = dt.Rows[0];
                    }
                }

                // --- Datos de la Empresa (Emisor) ---
                DataRow empresaRow = null;
                using (SqlCommand cmd = new SqlCommand(sqlEmpresa, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        empresaRow = dt.Rows[0];
                }

                // --- Partidas de la Remision ---
                DataTable partidasDt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sqlPartidas, cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(partidasDt);
                    }
                }

                // --- Construcción del XML ---
                var settings = new System.Xml.XmlWriterSettings
                {
                    Indent = true,
                    Encoding = System.Text.Encoding.UTF8,
                    OmitXmlDeclaration = false
                };

                using (var sw = new System.IO.StringWriter())
                using (var writer = System.Xml.XmlWriter.Create(sw, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Remision");

                    // Bloque Emisor (empresa)
                    writer.WriteStartElement("Emisor");
                    if (empresaRow != null)
                    {
                        writer.WriteElementString("RazonSocial", empresaRow["RazonSocial"].ToString());
                        writer.WriteElementString("NombreComercial", empresaRow["NombreComercial"].ToString());
                        writer.WriteElementString("RFC", empresaRow["RFC"].ToString());
                        writer.WriteElementString("Telefono", empresaRow["Telefono1"].ToString());
                        writer.WriteElementString("Correo", empresaRow["Correo"].ToString());
                        writer.WriteElementString("PaginaWeb", empresaRow["PaginaWeb"].ToString());
                        writer.WriteElementString("Domicilio", empresaRow["CalleNumero"].ToString());
                        writer.WriteElementString("Colonia", empresaRow["Colonia"].ToString());
                        writer.WriteElementString("Municipio", empresaRow["Municipio"].ToString());
                        writer.WriteElementString("Estado", empresaRow["Estado"].ToString());
                        writer.WriteElementString("CodigoPostal", empresaRow["CodigoPostal"].ToString());
                        writer.WriteElementString("Pais", empresaRow["Pais"].ToString());
                    }
                    writer.WriteEndElement(); // Emisor

                    // Datos generales de la remision
                    writer.WriteElementString("Folio", remisionRow["Folio"].ToString());
                    writer.WriteElementString("ClaveDocumento", remisionRow["ClaveDocumento"].ToString());
                    writer.WriteElementString("TipoDocumento", remisionRow["NombreDocumento"].ToString());
                    writer.WriteElementString("Estatus", remisionRow["Estatus"].ToString());
                    writer.WriteElementString("Fecha", remisionRow["Fecha"] == DBNull.Value ? "" : Convert.ToDateTime(remisionRow["Fecha"]).ToString("yyyy-MM-dd"));
                    writer.WriteElementString("FechaVence", remisionRow["FechaVence"] == DBNull.Value ? "" : Convert.ToDateTime(remisionRow["FechaVence"]).ToString("yyyy-MM-dd"));
                    writer.WriteElementString("Divisa", remisionRow["Divisa"].ToString());
                    writer.WriteElementString("TipoCambio", remisionRow["TipoCambio"].ToString());
                    writer.WriteElementString("Subtotal", remisionRow["Subtotal"].ToString());
                    writer.WriteElementString("Descuento", remisionRow["Descuento"].ToString());
                    writer.WriteElementString("Cargo", remisionRow["Cargo"].ToString());
                    writer.WriteElementString("Total", remisionRow["Total"].ToString());
                    writer.WriteElementString("Saldo", remisionRow["Saldo"].ToString());
                    writer.WriteElementString("Notas", remisionRow["Notas"].ToString());
                    writer.WriteElementString("Elaborado", remisionRow["Elaborado"].ToString());
                    writer.WriteElementString("Almacen", remisionRow["Almacen"].ToString());
                    writer.WriteElementString("Referencia", remisionRow["Referencia"].ToString());
                    writer.WriteElementString("Autorizado", remisionRow["Autorizado"].ToString());
                    writer.WriteElementString("CentroCostos", remisionRow["CentroCostos"].ToString());
                    writer.WriteElementString("IdProyecto", remisionRow["IdProyecto"].ToString());

                    // Bloque Cliente (receptor)
                    writer.WriteStartElement("Cliente");
                    writer.WriteElementString("IdCliente", remisionRow["IdCliente"].ToString());
                    writer.WriteElementString("RazonSocial", remisionRow["RazonSocial"].ToString());
                    writer.WriteElementString("RFC", remisionRow["RFC"].ToString());
                    writer.WriteElementString("Calle", remisionRow["Calle"].ToString());
                    writer.WriteElementString("NoExterior", remisionRow["NoExterior"].ToString());
                    writer.WriteElementString("NoInterior", remisionRow["NoInterior"].ToString());
                    writer.WriteElementString("Colonia", remisionRow["Colonia"].ToString());
                    writer.WriteElementString("Municipio", remisionRow["Municipio"].ToString());
                    writer.WriteElementString("CodigoPostal", remisionRow["CodigoPostal"].ToString());
                    writer.WriteElementString("Ciudad", remisionRow["Ciudad"].ToString());
                    writer.WriteElementString("Pais", remisionRow["Pais"].ToString());
                    writer.WriteEndElement(); // Cliente

                    // Bloque Partidas (detalle)
                    writer.WriteStartElement("Partidas");
                    foreach (DataRow p in partidasDt.Rows)
                    {
                        writer.WriteStartElement("Partida");
                        writer.WriteElementString("NumeroPartida", p["Partida"].ToString());
                        writer.WriteElementString("ClaveProducto", p["ClaveProducto"].ToString());
                        writer.WriteElementString("Descripcion",
                            p["DescripcionProducto"] != DBNull.Value ? p["DescripcionProducto"].ToString() : p["Concepto2"].ToString());
                        writer.WriteElementString("Alias", p["Alias"].ToString());
                        writer.WriteElementString("Cantidad", p["Cantidad"].ToString());
                        writer.WriteElementString("CantidadRecibida", p["CantidadRecibida"].ToString());
                        writer.WriteElementString("Unidad",
                            !string.IsNullOrWhiteSpace(p["Unidad"].ToString()) ? p["Unidad"].ToString() : p["UnidadMedida"].ToString());
                        writer.WriteElementString("Precio", p["Precio"].ToString());
                        writer.WriteElementString("Divisa", p["Divisa"].ToString());
                        writer.WriteElementString("TipoCambio", p["TipoCambio"].ToString());
                        writer.WriteElementString("Subtotal", p["Subtotal"].ToString());
                        writer.WriteElementString("Descuento", p["Descuento"].ToString());
                        writer.WriteElementString("Impuesto", p["Impuesto"].ToString());
                        writer.WriteElementString("Total", p["Total"].ToString());
                        writer.WriteEndElement(); // Partida
                    }
                    writer.WriteEndElement(); // Partidas

                    writer.WriteEndElement(); // Remision
                    writer.WriteEndDocument();
                    writer.Flush();

                    return sw.ToString();
                }
            }
        }
    }
}