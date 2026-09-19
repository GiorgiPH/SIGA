using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PV.Clases.Cotizaciones
{
  
    public class DBCotizaciones
    {
        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        #region Helpers privados de conversión segura

        private static object IntOrNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : Convert.ToInt32(s);
        }

        private static object DateOrNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : Convert.ToDateTime(s);
        }

        private static string Txt(object valor)
        {
            return (valor == null || valor == DBNull.Value) ? string.Empty : valor.ToString();
        }

        #endregion

        #region Encabezado - Cotizacion

        /// <summary>
        /// Da de alta el encabezado de una nueva Cotización. El folio se
        /// calcula dentro de una transacción con TABLOCKX/HOLDLOCK para
        /// evitar folios duplicados si dos usuarios confirman al mismo
        /// tiempo (mismo patrón que DBFacturas.InsertarFactura).
        ///
        /// claveCliente puede venir null/vacío: en ese caso se guarda el
        /// resto de los parámetros como datos de prospecto y ClaveCliente
        /// se inserta como NULL. Si claveCliente sí viene, los campos de
        /// prospecto se ignoran y se guardan como NULL (un folio de
        /// Cotización es de un cliente registrado O de un prospecto, nunca
        /// ambos a la vez).
        /// </summary>
        public void InsertarCotizacion(Control txtFolio, string claveDocumento, string estatus, string fecha,
            string diasVence, string fechaVence, string claveCliente,
            string nombreProspecto, string rfcProspecto, string domicilioProspecto,
            string contactoProspecto, string celularProspecto,
            string divisa, string tipoCambio, string notas, string elaborado, string consecutivo,
            string centroCostos)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        int folio;
                        using (SqlCommand cmdFolio = new SqlCommand(
                            "SELECT ISNULL(MAX(Folio), 0) + 1 FROM Cotizacion WITH (TABLOCKX, HOLDLOCK)", cn, tx))
                        {
                            folio = (int)cmdFolio.ExecuteScalar();
                        }

                        bool hayCliente = !string.IsNullOrWhiteSpace(claveCliente);

                        const string sql = @"
                            INSERT INTO Cotizacion
                                (Folio, ClaveDocumento, Estatus, Fecha, DiasVence, FechaVence,
                                 ClaveCliente, NombreProspecto, RFCProspecto, DomicilioProspecto,
                                 ContactoProspecto, CelularProspecto,
                                 Divisa, TipoCambio, Subtotal, Descuento, Cargo, Total, TotalPartidas,
                                 Notas, Elaborado, Consecutivo, CentroCostos)
                            VALUES
                                (@Folio, @ClaveDocumento, @Estatus, @Fecha, @DiasVence, @FechaVence,
                                 @ClaveCliente, @NombreProspecto, @RFCProspecto, @DomicilioProspecto,
                                 @ContactoProspecto, @CelularProspecto,
                                 @Divisa, @TipoCambio, 0, 0, 0, 0, 0,
                                 @Notas, @Elaborado, @Consecutivo, @CentroCostos)";

                        using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Folio", folio);
                            cmd.Parameters.AddWithValue("@ClaveDocumento", (object)claveDocumento ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Estatus", (object)estatus ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Fecha", DateOrNull(fecha));
                            cmd.Parameters.AddWithValue("@DiasVence", string.IsNullOrWhiteSpace(diasVence) ? 0 : Convert.ToInt32(diasVence));
                            cmd.Parameters.AddWithValue("@FechaVence", DateOrNull(fechaVence));

                            cmd.Parameters.AddWithValue("@ClaveCliente", hayCliente ? (object)Convert.ToInt32(claveCliente) : DBNull.Value);
                            cmd.Parameters.AddWithValue("@NombreProspecto", hayCliente ? (object)DBNull.Value : (object)(nombreProspecto ?? string.Empty));
                            cmd.Parameters.AddWithValue("@RFCProspecto", hayCliente ? (object)DBNull.Value : (object)(rfcProspecto ?? string.Empty));
                            cmd.Parameters.AddWithValue("@DomicilioProspecto", hayCliente ? (object)DBNull.Value : (object)(domicilioProspecto ?? string.Empty));
                            cmd.Parameters.AddWithValue("@ContactoProspecto", hayCliente ? (object)DBNull.Value : (object)(contactoProspecto ?? string.Empty));
                            cmd.Parameters.AddWithValue("@CelularProspecto", hayCliente ? (object)DBNull.Value : (object)(celularProspecto ?? string.Empty));

                            cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                            cmd.Parameters.AddWithValue("@Notas", (object)notas ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Elaborado", (object)elaborado ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Consecutivo", IntOrNull(consecutivo));
                            cmd.Parameters.AddWithValue("@CentroCostos", IntOrNull(centroCostos));

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        txtFolio.Text = folio.ToString();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Carga el encabezado completo de una Cotización por Folio.
        /// Regresa por "out" si el folio tiene cliente registrado o es un
        /// prospecto (esProspecto) y, si tiene cliente, su clave
        /// (claveCliente) para que el formulario la coloque en txtMatricular
        /// (lo que a su vez dispara la carga del nombre vía
        /// txtMatricular_TextChanged). Si es prospecto, esta misma llamada
        /// ya deja los txtXXXProspecto llenos.
        /// </summary>
        public void ConsultaCotizacion(string folio, Control txtClave, Control cmbEstatus, Control txtFecha,
            Control txtDiasVence, Control txtFechaVence, Control txtDivisa, Control txtTipoCambio,
            Control txtSubtotal, Control txtDescuento, Control txtRecargo, Control txtTotal, Control txtPartidas,
            Control txtNotas, Control txtElaborado, Control txtFolio, Control txtConsecutivo,
            Control txtNombreProspecto, Control txtRFCProspecto, Control txtDomicilioProspecto,
            Control txtContactoProspecto, Control txtCelularProspecto,
            out string claveCliente, out bool esProspecto, ComboBox cmbCentroCostos)
        {
            claveCliente = string.Empty;
            esProspecto = false;

            const string sql = @"
                SELECT ClaveDocumento, Estatus, Fecha, Divisa, TipoCambio, Subtotal, Descuento, Cargo, Total,
                       TotalPartidas, Notas, Elaborado, Consecutivo, DiasVence, FechaVence, CentroCostos,
                       ClaveCliente, NombreProspecto, RFCProspecto, DomicilioProspecto, ContactoProspecto, CelularProspecto
                FROM Cotizacion
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folio));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    txtClave.Text = Txt(dr["ClaveDocumento"]);
                    cmbEstatus.Text = Txt(dr["Estatus"]);
                    txtFecha.Text = dr["Fecha"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                    txtDivisa.Text = Txt(dr["Divisa"]);
                    txtTipoCambio.Text = Txt(dr["TipoCambio"]);
                    txtSubtotal.Text = Txt(dr["Subtotal"]);
                    txtDescuento.Text = Txt(dr["Descuento"]);
                    txtRecargo.Text = Txt(dr["Cargo"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPartidas.Text = Txt(dr["TotalPartidas"]);
                    txtNotas.Text = Txt(dr["Notas"]);
                    txtElaborado.Text = Txt(dr["Elaborado"]);
                    txtConsecutivo.Text = Txt(dr["Consecutivo"]);
                    txtDiasVence.Text = Txt(dr["DiasVence"]);
                    txtFechaVence.Text = dr["FechaVence"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy/MM/dd");

                    if (dr["CentroCostos"] != DBNull.Value)
                        cmbCentroCostos.SelectedValue = Convert.ToInt32(dr["CentroCostos"]);
                    else
                        cmbCentroCostos.SelectedIndex = -1;

                    if (dr["ClaveCliente"] != DBNull.Value)
                    {
                        claveCliente = dr["ClaveCliente"].ToString();
                        esProspecto = false;
                        txtNombreProspecto.Text = string.Empty;
                        txtRFCProspecto.Text = string.Empty;
                        txtDomicilioProspecto.Text = string.Empty;
                        txtContactoProspecto.Text = string.Empty;
                        txtCelularProspecto.Text = string.Empty;
                    }
                    else
                    {
                        claveCliente = string.Empty;
                        esProspecto = true;
                        txtNombreProspecto.Text = Txt(dr["NombreProspecto"]);
                        txtRFCProspecto.Text = Txt(dr["RFCProspecto"]);
                        txtDomicilioProspecto.Text = Txt(dr["DomicilioProspecto"]);
                        txtContactoProspecto.Text = Txt(dr["ContactoProspecto"]);
                        txtCelularProspecto.Text = Txt(dr["CelularProspecto"]);
                    }
                }
            }

            txtFolio.Text = folio;
        }

        /// <summary>Carga únicamente los totales del encabezado (sin Saldo: una Cotización no maneja saldo pendiente).</summary>
        public void ReciboSaldos(string folio, Control txtSubtotal, Control txtDescuento, Control txtRecargo,
            Control txtTotal, Control txtPartidas)
        {
            const string sql = @"SELECT Subtotal, Descuento, Cargo, Total, TotalPartidas FROM Cotizacion WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folio));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    txtSubtotal.Text = Txt(dr["Subtotal"]);
                    txtDescuento.Text = Txt(dr["Descuento"]);
                    txtRecargo.Text = Txt(dr["Cargo"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPartidas.Text = Txt(dr["TotalPartidas"]);
                }
            }
        }

        /// <summary>
        /// Llena la grilla de encabezados de Cotización. La columna
        /// "Cliente" usa COALESCE(RazonSocial, NombreProspecto) para que en
        /// la lista se vea el nombre correcto sin importar si el folio
        /// pertenece a un cliente registrado o a un prospecto; el mismo
        /// COALESCE se usa para el filtro por nombre.
        /// </summary>
        public void CargarCotizaciones(DataGridView dgv, string filtroFolio, string filtroDocumento,
            string filtroNombre, bool incluirTodas)
        {
            string filtroEstatus = incluirTodas ? string.Empty : " AND c.Estatus = 'Abierto'";

            string sql = $@"
                SELECT c.Folio, c.Consecutivo, c.ClaveDocumento AS Documento,
                       COALESCE(cli.RazonSocial, c.NombreProspecto) AS Cliente, c.Fecha
                FROM Cotizacion c
                LEFT JOIN Clientes cli ON cli.IdCliente = c.ClaveCliente
                WHERE 1 = 1 {filtroEstatus}
                  AND (@FiltroFolio = '' OR CAST(c.Folio AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%')
                  AND (@FiltroDocumento = '' OR c.ClaveDocumento LIKE '%' + @FiltroDocumento + '%')
                  AND (@FiltroNombre = '' OR COALESCE(cli.RazonSocial, c.NombreProspecto) LIKE '%' + @FiltroNombre + '%')
                ORDER BY c.Folio DESC";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@FiltroFolio", filtroFolio ?? string.Empty);
                cmd.Parameters.AddWithValue("@FiltroDocumento", filtroDocumento ?? string.Empty);
                cmd.Parameters.AddWithValue("@FiltroNombre", filtroNombre ?? string.Empty);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        /// <summary>
        /// Recalcula Subtotal/Descuento/Cargo(impuesto)/Total y TotalPartidas
        /// a partir de PartidaCotizacion. A diferencia de
        /// DBFacturas.ActualizarTotalesFactura, aquí NO se toca ningún
        /// Saldo: una Cotización no se cobra.
        /// </summary>
        public void ActualizarTotalesCotizacion(string folioCotizacion)
        {
            const string sql = @"
                UPDATE Cotizacion SET
                    Subtotal = ISNULL((SELECT SUM(Subtotal) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio), 0),
                    Descuento = ISNULL((SELECT SUM(DescuentoImporte) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio), 0),
                    Cargo = ISNULL((SELECT SUM(ImpuestoImporte) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio), 0),
                    Total = ISNULL((SELECT SUM(Total) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio), 0),
                    TotalPartidas = ISNULL((SELECT COUNT(*) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio), 0)
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Cambia el estatus de la Cotización (p. ej. al terminar/bloquear la captura).</summary>
        public void ActualizarCotizacionEstatus(string folioCotizacion, string estatus)
        {
            const string sql = "UPDATE Cotizacion SET Estatus = @Estatus WHERE Folio = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Cancela la Cotización.</summary>
        public void CancelarCotizacion(string folioCotizacion)
        {
            const string sql = "UPDATE Cotizacion SET Estatus = 'Cancelado' WHERE Folio = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Calcula el siguiente Consecutivo para un ClaveDocumento dado.
        /// NOTA: el método equivalente en DBFacturas (ConsecutivoFactura)
        /// concatenaba ClaveDocumento directo en el texto del SQL; aquí se
        /// corrige usando un parámetro, para evitar inyección SQL.
        /// </summary>
        public void ConsecutivoCotizacion(Guna2TextBox txtConsecutivo, string claveDocumento)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 Consecutivo FROM Cotizacion WHERE ClaveDocumento = @ClaveDocumento ORDER BY Consecutivo DESC", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveDocumento", claveDocumento);
                    cn.Open();
                    object resultado = cmd.ExecuteScalar();

                    txtConsecutivo.Text = (resultado != null && resultado != DBNull.Value)
                        ? (Convert.ToInt32(resultado) + 1).ToString()
                        : "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.ToString());
            }
        }

        /// <summary>
        /// Llena cmbDocumento con los documentos del catálogo aplicables a
        /// Cotización (Tarea = 'Cotizacion'). Requiere que el catálogo
        /// Documento tenga renglones dados de alta con esa Tarea (ver script
        /// de SQL adjunto).
        /// </summary>
        public void SeleccionarCotizacion(ComboBox cb)
        {
            cb.Items.Clear();

            const string sql = "SELECT (Clave + ' - ' + Nombre) AS Nombre FROM Documento WHERE Tarea = 'Cotizaciones a Cliente'";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
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

        #endregion

        #region Detalle - PartidaCotizacion

        /// <summary>Calcula el siguiente número de partida (consecutivo) para la Cotización.</summary>
        public void Consulta5Cotizacion(string folioCotizacion, Control txtPartida)
        {
            const string sql = "SELECT ISNULL(MAX(Partida), 0) + 1 FROM PartidaCotizacion WHERE FolioCotizacion = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cn.Open();
                object resultado = cmd.ExecuteScalar();
                txtPartida.Text = resultado == null ? "1" : resultado.ToString();
            }
        }

        /// <summary>Indica si ya existe una partida (Folio + Partida), para decidir INSERT vs UPDATE.</summary>
        public bool ExistePartidaCotizacion(string folioCotizacion, string partida)
        {
            if (string.IsNullOrWhiteSpace(folioCotizacion) || folioCotizacion == "X" || string.IsNullOrWhiteSpace(partida))
                return false;

            const string sql = "SELECT COUNT(*) FROM PartidaCotizacion WHERE FolioCotizacion = @Folio AND Partida = @Partida";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        /// <summary>
        /// Inserta una partida. tipoConcepto debe ser 'Producto' o
        /// 'Servicio' (viene de cmbTipo.Text en el formulario) y
        /// claveConcepto es la clave del catálogo correspondiente
        /// (ClaveProducto o ClaveServicio, según tipoConcepto).
        /// </summary>
        public void InsertarPartidaCotizacion(string folioCotizacion, string partida, string tipoConcepto,
            string claveConcepto, string concepto2, string cantidad, string unidad, string divisa,
            string tipoCambio, decimal subtotal, decimal descuento, decimal total, decimal precio,
            decimal impuesto, decimal descuentoImporte, decimal impuestoImporte)
        {
            const string sql = @"
                INSERT INTO PartidaCotizacion
                    (FolioCotizacion, Partida, TipoConcepto, ClaveConcepto, Concepto2, Cantidad, Unidad,
                     Divisa, TipoCambio, Precio, Subtotal, Descuento, DescuentoImporte, Impuesto, ImpuestoImporte, Total)
                VALUES
                    (@FolioCotizacion, @Partida, @TipoConcepto, @ClaveConcepto, @Concepto2, @Cantidad, @Unidad,
                     @Divisa, @TipoCambio, @Precio, @Subtotal, @Descuento, @DescuentoImporte, @Impuesto, @ImpuestoImporte, @Total)";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FolioCotizacion", Convert.ToInt32(folioCotizacion));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cmd.Parameters.AddWithValue("@TipoConcepto", tipoConcepto);
                cmd.Parameters.AddWithValue("@ClaveConcepto", IntOrNull(claveConcepto));
                cmd.Parameters.AddWithValue("@Concepto2", (object)concepto2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", string.IsNullOrWhiteSpace(cantidad) ? 0 : Convert.ToInt32(Convert.ToDecimal(cantidad)));
                cmd.Parameters.AddWithValue("@Unidad", (object)unidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@DescuentoImporte", descuentoImporte);
                cmd.Parameters.AddWithValue("@Impuesto", impuesto);
                cmd.Parameters.AddWithValue("@ImpuestoImporte", impuestoImporte);
                cmd.Parameters.AddWithValue("@Total", total);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Actualiza una partida existente (contraparte de InsertarPartidaCotizacion vía UPDATE).</summary>
        public void ActualizarPartidaCotizacion(string folioCotizacion, string partida, string tipoConcepto,
            string claveConcepto, string concepto2, string cantidad, string unidad, string divisa,
            string tipoCambio, decimal subtotal, decimal descuento, decimal total, decimal precio,
            decimal impuesto, decimal descuentoImporte, decimal impuestoImporte)
        {
            const string sql = @"
                UPDATE PartidaCotizacion SET
                    TipoConcepto = @TipoConcepto,
                    ClaveConcepto = @ClaveConcepto,
                    Concepto2 = @Concepto2,
                    Cantidad = @Cantidad,
                    Unidad = @Unidad,
                    Divisa = @Divisa,
                    TipoCambio = @TipoCambio,
                    Precio = @Precio,
                    Subtotal = @Subtotal,
                    Descuento = @Descuento,
                    DescuentoImporte = @DescuentoImporte,
                    Impuesto = @Impuesto,
                    ImpuestoImporte = @ImpuestoImporte,
                    Total = @Total
                WHERE FolioCotizacion = @FolioCotizacion AND Partida = @Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FolioCotizacion", Convert.ToInt32(folioCotizacion));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cmd.Parameters.AddWithValue("@TipoConcepto", tipoConcepto);
                cmd.Parameters.AddWithValue("@ClaveConcepto", IntOrNull(claveConcepto));
                cmd.Parameters.AddWithValue("@Concepto2", (object)concepto2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", string.IsNullOrWhiteSpace(cantidad) ? 0 : Convert.ToInt32(Convert.ToDecimal(cantidad)));
                cmd.Parameters.AddWithValue("@Unidad", (object)unidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@DescuentoImporte", descuentoImporte);
                cmd.Parameters.AddWithValue("@Impuesto", impuesto);
                cmd.Parameters.AddWithValue("@ImpuestoImporte", impuestoImporte);
                cmd.Parameters.AddWithValue("@Total", total);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Carga una partida existente en los controles del panel de
        /// edición y regresa por "out" el TipoConcepto y la ClaveConcepto
        /// crudos. El formulario debe:
        ///   1) Poner cmbTipo.Text = tipoConcepto
        ///   2) Repoblar cmbConcepto según ese tipo (Producto/Servicio)
        ///   3) Poner cmbConcepto.SelectedValue = claveConcepto (como int)
        /// en ese orden, porque cmbConcepto depende de qué tipo esté
        /// seleccionado. Por eso esta capa de datos NO manipula cmbTipo ni
        /// cmbConcepto directamente (a diferencia de
        /// DBFacturas.ConsultaPartidaFactura, que sí recibía el ComboBox):
        /// aquí hay una dependencia entre dos combos que le corresponde
        /// resolver a la UI, no a la capa de datos.
        ///
        /// Nota: a diferencia de DBFacturas.ConsultaPartidaFactura (que no
        /// restauraba Concepto2 al editar una partida existente), aquí sí
        /// se restaura explícitamente vía el parámetro txtConcepto2.
        /// </summary>
        public void ConsultaPartidaCotizacion(string folioCotizacion, string partida,
            Control txtCantidad, Control txtUnidad, Control txtDivisa, Control txtTipoCambio,
            Control txtImporte, Control txtDescuento, Control txtTotal, Control txtPrecio, Control txtImpuesto,
            Control txtConcepto2, out string tipoConcepto, out string claveConcepto)
        {
            tipoConcepto = string.Empty;
            claveConcepto = string.Empty;

            const string sql = @"
                SELECT TipoConcepto, ClaveConcepto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio,
                       Subtotal, Descuento, Total, Precio, Impuesto
                FROM PartidaCotizacion
                WHERE FolioCotizacion = @Folio AND Partida = @Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    tipoConcepto = Txt(dr["TipoConcepto"]);
                    claveConcepto = Txt(dr["ClaveConcepto"]);

                    txtConcepto2.Text = Txt(dr["Concepto2"]);
                    txtCantidad.Text = Txt(dr["Cantidad"]);
                    txtUnidad.Text = Txt(dr["Unidad"]);
                    txtDivisa.Text = Txt(dr["Divisa"]);
                    txtTipoCambio.Text = Txt(dr["TipoCambio"]);
                    txtImporte.Text = Txt(dr["Subtotal"]);
                    txtDescuento.Text = Txt(dr["Descuento"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPrecio.Text = Txt(dr["Precio"]);
                    txtImpuesto.Text = Txt(dr["Impuesto"]);
                }
            }
        }

        /// <summary>Elimina una partida y renumera las restantes para no dejar huecos en el consecutivo.</summary>
        public string EliminarPartidaCotizacion(string folioCotizacion, string partida)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        int folio = Convert.ToInt32(folioCotizacion);
                        int partidaEliminar = Convert.ToInt32(partida);

                        using (SqlCommand cmdDel = new SqlCommand(
                            "DELETE FROM PartidaCotizacion WHERE FolioCotizacion = @Folio AND Partida = @Partida", cn, tx))
                        {
                            cmdDel.Parameters.AddWithValue("@Folio", folio);
                            cmdDel.Parameters.AddWithValue("@Partida", partidaEliminar);
                            int filas = cmdDel.ExecuteNonQuery();
                            if (filas == 0)
                            {
                                tx.Rollback();
                                return "No se encontró la partida a eliminar.";
                            }
                        }

                        using (SqlCommand cmdReordena = new SqlCommand(
                            "UPDATE PartidaCotizacion SET Partida = Partida - 1 WHERE FolioCotizacion = @Folio AND Partida > @Partida", cn, tx))
                        {
                            cmdReordena.Parameters.AddWithValue("@Folio", folio);
                            cmdReordena.Parameters.AddWithValue("@Partida", partidaEliminar);
                            cmdReordena.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        return $"Error al eliminar la partida: {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// Llena la grilla de partidas de una Cotización, incluyendo la
        /// columna TipoConcepto (Producto/Servicio) para que se muestre en
        /// la grilla (columna "Tipo" en ConfigurarGrillaPartidas).
        /// </summary>
        public void CargarPartidasCotizacion(DataGridView dgv, string folioCotizacion)
        {
            const string sql = @"
                SELECT c.Consecutivo, p.FolioCotizacion, p.Partida, p.TipoConcepto, p.Concepto2,
                       p.Cantidad, p.Subtotal, p.Descuento, p.Impuesto, p.Total
                FROM Cotizacion AS c
                INNER JOIN PartidaCotizacion AS p ON c.Folio = p.FolioCotizacion
                WHERE c.Folio = @Folio
                ORDER BY p.Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Folio", string.IsNullOrWhiteSpace(folioCotizacion) ? 0 : Convert.ToInt32(folioCotizacion));
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        /// <summary>Recalcula los totales "en vivo" que se muestran mientras se van agregando partidas (panel derecho).</summary>
        public void ReciboSaldosPartidas(string folioCotizacion, Control txtSubtotalR, Control txtDescuentoR,
            Control txtTotalR, Control txtImpuestoR)
        {
            const string sql = @"
                SELECT ISNULL(SUM(Subtotal), 0) AS Subtotal, ISNULL(SUM(Descuento), 0) AS Descuento,
                       ISNULL(SUM(Impuesto), 0) AS Impuesto, ISNULL(SUM(Total), 0) AS Total
                FROM PartidaCotizacion
                WHERE FolioCotizacion = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioCotizacion));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    txtSubtotalR.Text = Txt(dr["Subtotal"]);
                    txtDescuentoR.Text = Txt(dr["Descuento"]);
                    txtImpuestoR.Text = Txt(dr["Impuesto"]);
                    txtTotalR.Text = Txt(dr["Total"]);
                }
            }
        }

        #endregion
    }
}