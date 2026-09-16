using Condominios;
using PV.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace PV.Clases.Facturas
{
    /// <summary>
    /// Capa de datos del módulo de Facturas. Encapsula únicamente lo que es
    /// propio de este dominio: el encabezado [Factura] y el detalle
    /// [PartidaFactura].
    ///
    /// Deliberadamente NO se duplican aquí los métodos genéricos que ya
    /// existen en otras clases DB (catálogo de documentos, información de
    /// producto/existencias, búsqueda de clientes, movimientos de almacén,
    /// etc.). Esos siguen viviendo en DBPedidoCliente / DBClientes /
    /// DBAlmacenes tal como ya se usan hoy para Remision, porque son
    /// genéricos y no dependen de esta tabla. El formulario Facturas los
    /// sigue consumiendo igual que OrdenPedidoCliente.
    ///
    /// Convención seguida en todo el archivo:
    ///   - Cada método abre su propia conexión con "using" (nunca se
    ///     mantiene una conexión abierta a nivel de clase/formulario).
    ///   - Todo SqlDataReader se consume dentro de un "using".
    ///   - Se usan parámetros (SqlParameter) en vez de concatenar texto.
    ///   - Los controles de formulario se reciben como "Control" (no como
    ///     Guna2TextBox/TextBox concretos) porque lo único que se toca desde
    ///     aquí es la propiedad .Text, disponible en la clase base Control.
    ///     Esto evita acoplar la capa de datos al tipo exacto de control de
    ///     UI que uses (Guna2TextBox, TextBox, Guna2ComboBox, ComboBox, etc.)
    /// </summary>
    public class DBFacturas
    {
        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        #region Helpers privados de conversión segura

        private static object DecOrNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : Convert.ToDecimal(s);
        }

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

        #region Encabezado - Factura

        /// <summary>
        /// Da de alta el encabezado de una nueva Factura. El folio se calcula
        /// dentro de una transacción con UPDLOCK/HOLDLOCK para evitar folios
        /// duplicados si dos usuarios confirman al mismo tiempo (mejora sobre
        /// el patrón original que solo hacía MAX+1 sin bloqueo). El folio
        /// generado se regresa directamente en txtFolio, igual que hacía
        /// InsertarRemision.
        /// </summary>
        public void InsertarFactura(Control txtFolio, string claveDocumento, string estatus, string fecha,
            string diasVence, string fechaVence, string claveCliente, string divisa, string tipoCambio,
            string notas, string elaborado, string consecutivo, string almacen, string centroCostos, string proyecto)
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
                            "SELECT ISNULL(MAX(Folio), 0) + 1 FROM Factura WITH (TABLOCKX, HOLDLOCK)", cn, tx))
                        {
                            folio = (int)cmdFolio.ExecuteScalar();
                        }

                        const string sql = @"
                            INSERT INTO Factura
                                (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio,
                                 Subtotal, Descuento, Cargo, Total, TotalPartidas, Notas, Elaborado, Recargo,
                                 DescuentoPago, Saldo, UltimoAbono, Consecutivo, Almacen, DiasVence, FechaVence,
                                 CentroCostos, IdProyecto)
                            VALUES
                                (@Folio, @ClaveDocumento, @Estatus, @Fecha, @ClaveCliente, @Divisa, @TipoCambio,
                                 0, 0, 0, 0, 0, @Notas, @Elaborado, 0,
                                 0, 0, 0, @Consecutivo, @Almacen, @DiasVence, @FechaVence,
                                 @CentroCostos, @IdProyecto)";

                        using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Folio", folio);
                            cmd.Parameters.AddWithValue("@ClaveDocumento", (object)claveDocumento ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Estatus", (object)estatus ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Fecha", DateOrNull(fecha));
                            cmd.Parameters.AddWithValue("@ClaveCliente", IntOrNull(claveCliente));
                            cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                            cmd.Parameters.AddWithValue("@Notas", (object)notas ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Elaborado", (object)elaborado ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Consecutivo", IntOrNull(consecutivo));
                            cmd.Parameters.AddWithValue("@Almacen", IntOrNull(almacen));
                            cmd.Parameters.AddWithValue("@DiasVence", string.IsNullOrWhiteSpace(diasVence) ? 0 : Convert.ToInt32(diasVence));
                            cmd.Parameters.AddWithValue("@FechaVence", DateOrNull(fechaVence));
                            cmd.Parameters.AddWithValue("@CentroCostos", IntOrNull(centroCostos));
                            cmd.Parameters.AddWithValue("@IdProyecto", IntOrNull(proyecto));


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
        /// Carga el encabezado completo de una Factura por Folio en los
        /// controles del formulario. Regresa el IdCliente (para reasignar
        /// Matricula) vía "out".
        /// </summary>
        public void ConsultaFactura(string folio, Control txtClave, Control cmbEstatus, Control txtFecha,
            Control txtDiasVence, Control txtFechaVence, Control txtDivisa, Control txtTipoCambio,
            Control txtSubtotal, Control txtDescuento, Control txtRecargo, Control txtTotal, Control txtPartidas, Control txtSaldo,
            Control txtNotas, Control txtElaborado, Control txtFolio, Control txtConsecutivo, Control txtAutoriza,
            Control txtFechaAuto, out string cliente, ComboBox cmbCentroCostos, ComboBox cmbProyecto, ComboBox cmbAlmacen)
        {
            cliente = string.Empty;

            const string sql = @"
                SELECT ClaveDocumento, Estatus, Fecha, Divisa, TipoCambio, Subtotal, Descuento, Recargo, Total,
                       TotalPartidas, Saldo, Notas, Elaborado, Consecutivo, Autorizado, FechaAutoriza, UsuarioAutoriza,
                       ClaveProveedor, CentroCostos, IdProyecto, DiasVence, FechaVence, Almacen
                FROM Factura
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
                    txtRecargo.Text = Txt(dr["Recargo"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPartidas.Text = Txt(dr["TotalPartidas"]);
                    txtSaldo.Text = Txt(dr["Saldo"]);
                    txtNotas.Text = Txt(dr["Notas"]);
                    txtElaborado.Text = Txt(dr["Elaborado"]);
                    txtConsecutivo.Text = Txt(dr["Consecutivo"]);
                    txtAutoriza.Text = Txt(dr["UsuarioAutoriza"]);
                    txtFechaAuto.Text = dr["FechaAutoriza"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy/MM/dd");
                    txtDiasVence.Text = Txt(dr["DiasVence"]);
                    txtFechaVence.Text = dr["FechaVence"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy/MM/dd");
                    // CENTRO DE COSTOS
                    if (dr["CentroCostos"] != DBNull.Value)
                    {
                        cmbCentroCostos.SelectedValue = Convert.ToInt32(dr["CentroCostos"]);
                    }
                    else
                    {
                        cmbCentroCostos.SelectedIndex = -1;
                    }

                    if (dr["IdProyecto"] != DBNull.Value)
                    {
                        cmbProyecto.SelectedValue = Convert.ToInt32(dr["IdProyecto"]);
                    }
                    else
                    {
                        cmbProyecto.SelectedIndex = -1;
                    }

                    if (dr["Almacen"] != DBNull.Value)
                    {
                        cmbAlmacen.SelectedValue = Convert.ToInt32(dr["Almacen"]);
                    }
                    else
                    {
                        cmbAlmacen.SelectedIndex = -1;
                    }
                    cliente = Txt(dr["ClaveProveedor"]);
                }
            }

            txtFolio.Text = folio;
        }

        /// <summary>Carga únicamente los totales/saldo del encabezado (equivalente a ReciboSaldos).</summary>
        public void ReciboSaldos(string folio, Control txtSubtotal, Control txtDescuento, Control txtRecargo,
            Control txtTotal, Control txtPartidas, Control txtSaldo)
        {
            const string sql = @"SELECT Subtotal, Descuento, Cargo, Total, TotalPartidas, Saldo FROM Factura WHERE Folio = @Folio";

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
                    txtSaldo.Text = Txt(dr["Saldo"]);

                }
            }
        }

        /// <summary>
        /// Llena una grilla de encabezados de Factura. Trae únicamente los
        /// datos que necesitan las columnas armadas por código en el
        /// formulario (Facturas.ConfigurarGrillaEncabezado): Folio (interno,
        /// oculto), Consecutivo (se muestra como "Folio"), Documento,
        /// Cliente y Fecha. AutoGenerateColumns debe quedar en false en el
        /// DataGridView para que no se generen columnas extra.
        /// </summary>
        public void CargarFacturas(DataGridView dgv, string filtroFolio, string filtroDocumento,
            string filtroNombre, bool incluirTodas)
        {
            string filtroEstatus = incluirTodas ? string.Empty : " AND f.Estatus = 'Abierto'";

            string sql = $@"
                SELECT f.Folio, f.Consecutivo, f.ClaveDocumento AS Documento,
                       cli.RazonSocial AS Cliente, f.Fecha
                FROM Factura f
                LEFT JOIN Clientes cli ON cli.IdCliente = f.ClaveProveedor
                WHERE 1 = 1 {filtroEstatus}
                  AND (@FiltroFolio = '' OR CAST(f.Folio AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%')
                  AND (@FiltroDocumento = '' OR f.ClaveDocumento LIKE '%' + @FiltroDocumento + '%')
                  AND (@FiltroNombre = '' OR cli.RazonSocial LIKE '%' + @FiltroNombre + '%')
                ORDER BY f.Folio DESC";

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
        /// Recalcula Subtotal/Descuento/Recargo(impuesto)/Total a partir del
        /// detalle [PartidaFactura] y actualiza el encabezado, incluyendo
        /// TotalPartidas. El Saldo se iguala al Total porque en este alcance
        /// no existe todavía un módulo de abonos: cuando se implemente el
        /// registro de abonos, ese proceso deberá ajustar Saldo = Total -
        /// SUM(abonos) y UltimoAbono con el último pago, en vez de este
        /// método recalcularlo aquí.
        /// </summary>
        public void ActualizarTotalesFactura(string folioFactura)
        {
            const string sql = @"
                UPDATE Factura SET
                    Subtotal = ISNULL((SELECT SUM(Subtotal) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Descuento = ISNULL((SELECT SUM(DescuentoImporte) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Cargo = ISNULL((SELECT SUM(ImpuestoImporte) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Total = ISNULL((SELECT SUM(Total) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Saldo = ISNULL((SELECT SUM(Total) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),

                    TotalPartidas = ISNULL((SELECT count(*) FROM PartidaFactura WHERE FolioFactura = @Folio), 0)
                WHERE Folio = @Folio;

                UPDATE Factura SET Saldo = Total WHERE Folio = @Folio;";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Cambia el estatus de la Factura (uso general, p.ej. "Abierto"/"Bloqueado").</summary>
        public void ActualizarFacturaEstatus(string folioFactura, string estatus)
        {
            const string sql = "UPDATE Factura SET Estatus = @Estatus WHERE Folio = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Sobrecarga usada al confirmar/bloquear la Factura y generar la
        /// salida de almacén: además del estatus, guarda el Folio del
        /// movimiento de inventario generado (FolioMovimiento) y, si aplica,
        /// una referencia/comentario.
        /// </summary>
        public void ActualizarFacturaEstatus(string folioFactura, string estatus, string referencia, string folioMovimiento)
        {
            const string sql = @"
                UPDATE Factura
                SET Estatus = @Estatus,
                    Referencia = @Referencia,
                    FolioMovimiento = @FolioMovimiento
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Referencia", string.IsNullOrEmpty(referencia) ? (object)DBNull.Value : referencia);
                cmd.Parameters.AddWithValue("@FolioMovimiento", IntOrNull(folioMovimiento));
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Cancela la Factura.</summary>
        public void CancelarFactura(string folioFactura)
        {
            const string sql = "UPDATE Factura SET Estatus = 'Cancelado' WHERE Folio = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Registra la autorización de la Factura (flujo de AutentificarAdmin).</summary>
        public void ActualizarFacturaAutorizacion(string folioFactura, string usuarioAutoriza, string fechaAutoriza)
        {
            const string sql = @"
                UPDATE Factura
                SET Autorizado = 'SI', UsuarioAutoriza = @Usuario, FechaAutoriza = @Fecha
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuarioAutoriza ?? string.Empty);
                cmd.Parameters.AddWithValue("@Fecha", DateOrNull(fechaAutoriza));
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Detalle - PartidaFactura

        /// <summary>Calcula el siguiente número de partida (consecutivo) para la Factura y lo coloca en txtPartida.</summary>
        public void Consulta5Factura(string folioFactura, Control txtPartida)
        {
            const string sql = "SELECT ISNULL(MAX(Partida), 0) + 1 FROM PartidaFactura WHERE FolioFactura = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                object resultado = cmd.ExecuteScalar();
                txtPartida.Text = resultado == null ? "1" : resultado.ToString();
            }
        }

        /// <summary>
        /// Indica si ya existe una partida (FolioFactura + Partida) en la
        /// tabla PartidaFactura. Se usa desde el formulario para decidir si
        /// GuardarPartidaActual debe hacer INSERT (partida nueva) o UPDATE
        /// (partida existente que se está editando, p.ej. tras doble clic
        /// en la grilla de partidas) — antes siempre se hacía INSERT, lo que
        /// provocaba una violación de llave primaria/única al intentar
        /// "guardar" una partida ya existente.
        /// </summary>
        public bool ExistePartidaFactura(string folioFactura, string partida)
        {
            if (string.IsNullOrWhiteSpace(folioFactura) || folioFactura == "X" || string.IsNullOrWhiteSpace(partida))
                return false;

            const string sql = "SELECT COUNT(*) FROM PartidaFactura WHERE FolioFactura = @Folio AND Partida = @Partida";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void InsertarPartidaFactura(string folioFactura, string partida, string claveProducto, string concepto2,
            string cantidad, string unidad, string divisa, string tipoCambio, decimal subtotal, decimal descuento,
            decimal total, decimal precio, decimal impuesto, decimal descuentoImporte, decimal ImpuestoImporte)
        {
            const string sql = @"
                INSERT INTO PartidaFactura
                    (FolioFactura, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio,
                     Subtotal, Descuento, Total, Precio, CantidadRecibida, Impuesto, DescuentoImporte, ImpuestoImporte)
                VALUES
                    (@FolioFactura, @Partida, @ClaveProducto, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio,
                     @Subtotal, @Descuento, @Total, @Precio, 0, @Impuesto,  @DescuentoImporte, @ImpuestoImporte)";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FolioFactura", Convert.ToInt32(folioFactura));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cmd.Parameters.AddWithValue("@ClaveProducto", IntOrNull(claveProducto));
                cmd.Parameters.AddWithValue("@Concepto2", (object)concepto2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", string.IsNullOrWhiteSpace(cantidad) ? 0 : Convert.ToInt32(Convert.ToDecimal(cantidad)));
                cmd.Parameters.AddWithValue("@Unidad", (object)unidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Impuesto", impuesto);
                cmd.Parameters.AddWithValue("@DescuentoImporte", descuentoImporte);
                cmd.Parameters.AddWithValue("@ImpuestoImporte", ImpuestoImporte);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Actualiza una partida ya existente (FolioFactura + Partida). Es
        /// la contraparte de InsertarPartidaFactura: mismos campos, pero vía
        /// UPDATE en vez de INSERT. Se agrega porque el formulario permite
        /// cargar una partida existente para edición (doble clic en la
        /// grilla de partidas) y, al confirmar, antes se intentaba volver a
        /// insertarla con la misma llave (FolioFactura, Partida), lo que
        /// producía una violación de llave primaria/única.
        /// </summary>
        public void ActualizarPartidaFactura(string folioFactura, string partida, string claveProducto, string concepto2,
            string cantidad, string unidad, string divisa, string tipoCambio, decimal subtotal, decimal descuento,
            decimal total, decimal precio, decimal impuesto, decimal DescuentoImporte, decimal ImpuestoImporte)
        {
            const string sql = @"
                UPDATE PartidaFactura SET
                    ClaveProducto = @ClaveProducto,
                    Concepto2 = @Concepto2,
                    Cantidad = @Cantidad,
                    Unidad = @Unidad,
                    Divisa = @Divisa,
                    TipoCambio = @TipoCambio,
                    Subtotal = @Subtotal,
                    Descuento = @Descuento,
                    Total = @Total,
                    Precio = @Precio,
                    Impuesto = @Impuesto,
                    DescuentoImporte = @DescuentoImporte,   
                    ImpuestoImporte = @ImpuestoImporte
                WHERE FolioFactura = @FolioFactura AND Partida = @Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FolioFactura", Convert.ToInt32(folioFactura));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cmd.Parameters.AddWithValue("@ClaveProducto", IntOrNull(claveProducto));
                cmd.Parameters.AddWithValue("@Concepto2", (object)concepto2 ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", string.IsNullOrWhiteSpace(cantidad) ? 0 : Convert.ToInt32(Convert.ToDecimal(cantidad)));
                cmd.Parameters.AddWithValue("@Unidad", (object)unidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Impuesto", impuesto);
                cmd.Parameters.AddWithValue("@DescuentoImporte", DescuentoImporte);
                cmd.Parameters.AddWithValue("@ImpuestoImporte", ImpuestoImporte);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Carga una partida existente en los controles del panel de edición.
        /// Selecciona el producto en el combo por Id (SelectedValue) en vez
        /// de por texto, para que quede exactamente el mismo producto aunque
        /// existan descripciones repetidas o parecidas.
        /// </summary>
        public void ConsultaPartidaFactura(string folioFactura, string partida,
            Control txtCantidad, Control txtUnidad, Control txtDivisa, Control txtTipoCambio,
            Control txtImporte, Control txtDescuento, Control txtTotal, Control txtPrecio, Control txtImpuesto,
            Control txtEntregado, ComboBox cmbConcepto)
        {
            const string sql = @"
                SELECT ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total,
                       Precio, Impuesto, CantidadRecibida
                FROM PartidaFactura
                WHERE FolioFactura = @Folio AND Partida = @Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cmd.Parameters.AddWithValue("@Partida", Convert.ToInt32(partida));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    // El ValueMember del combo (ClaveProducto) es numérico:
                    // hay que convertir antes de asignar SelectedValue, o la
                    // comparación de tipos falla y no selecciona nada.
                    if (dr["ClaveProducto"] != DBNull.Value)
                    {
                        cmbConcepto.SelectedValue = Convert.ToInt32(dr["ClaveProducto"]);
                    }

                    txtCantidad.Text = Txt(dr["Cantidad"]);
                    txtUnidad.Text = Txt(dr["Unidad"]);
                    txtDivisa.Text = Txt(dr["Divisa"]);
                    txtTipoCambio.Text = Txt(dr["TipoCambio"]);
                    txtImporte.Text = Txt(dr["Subtotal"]);
                    txtDescuento.Text = Txt(dr["Descuento"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPrecio.Text = Txt(dr["Precio"]);
                    txtImpuesto.Text = Txt(dr["Impuesto"]);
                    txtEntregado.Text = Txt(dr["CantidadRecibida"]);
                }
            }
        }

        /// <summary>Elimina una partida y renumera las restantes para no dejar huecos en el consecutivo.</summary>
        public string EliminarPartidaFactura(string folioFactura, string partida)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        int folio = Convert.ToInt32(folioFactura);
                        int partidaEliminar = Convert.ToInt32(partida);

                        using (SqlCommand cmdDel = new SqlCommand(
                            "DELETE FROM PartidaFactura WHERE FolioFactura = @Folio AND Partida = @Partida", cn, tx))
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
                            "UPDATE PartidaFactura SET Partida = Partida - 1 WHERE FolioFactura = @Folio AND Partida > @Partida", cn, tx))
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

        /// <summary>Regresa el total de partidas restantes (usado para refrescar TotalPartidas tras eliminar).</summary>
        public string ObtenerTotalPartidasFactura(string folioFactura)
        {
            const string sql = "SELECT COUNT(*) FROM PartidaFactura WHERE FolioFactura = @Folio";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }

        /// <summary>
        /// Llena la grilla de partidas de una Factura. Trae únicamente los
        /// datos que necesitan las columnas armadas por código en el
        /// formulario (Facturas.ConfigurarGrillaPartidas): Folio, Partida,
        /// Producto (Concepto2), Cantidad, Subtotal, Descuento, Impuesto,
        /// Total.
        /// </summary>
        public void CargarPartidasFactura(DataGridView dgv, string folioFactura)
        {
            const string sql = @"
                SELECT F.Consecutivo,P.FolioFactura, P.Partida, P.Concepto2, P.Cantidad, P.Subtotal, P.Descuento, P.Impuesto, P.Total
                FROM Factura as F Join PartidaFactura as P ON F.Folio = P.FolioFactura
                WHERE F.Folio = @Folio
                ORDER BY Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Folio", string.IsNullOrWhiteSpace(folioFactura) ? 0 : Convert.ToInt32(folioFactura));
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        /// <summary>Recalcula los totales "en vivo" que se muestran mientras se van agregando partidas (panel derecho).</summary>
        public void ReciboSaldosPartidas(string folioFactura, Control txtSubtotalR, Control txtDescuentoR,
            Control txtTotalR, Control txtImpuestoR)
        {
            const string sql = @"
                SELECT ISNULL(SUM(Subtotal), 0) AS Subtotal, ISNULL(SUM(Descuento), 0) AS Descuento,
                       ISNULL(SUM(Impuesto), 0) AS Impuesto, ISNULL(SUM(Total), 0) AS Total
                FROM PartidaFactura
                WHERE FolioFactura = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
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

        /// <summary>
        /// Regresa las partidas de la Factura en el formato
        /// [ClaveProducto, Cantidad, Costeo, Precio, Partida, Unidad, Total]
        /// requerido por la salida de almacén (mismo formato que consumía
        /// IngresarAlmacen en OrdenPedidoCliente).
        ///
        /// NOTA/TODO: el costo unitario ("Costeo") no se guarda en
        /// PartidaFactura (igual que no se guardaba en PartidaRemision); en
        /// el formulario original se tomaba de la tabla de Producto al
        /// momento de capturar la partida. Aquí se regresa "0" como
        /// marcador de posición — ajusta el JOIN de abajo con tu tabla
        /// Producto real (por ejemplo Producto.CostoUnitario) si necesitas
        /// el costeo real para el movimiento de inventario.
        /// </summary>
        public List<List<string>> ObtenerPartidas(string folioFactura)
        {
            const string sql = @"
                SELECT ClaveProducto, Cantidad, Precio, Partida, Unidad, Total
                FROM PartidaFactura
                WHERE FolioFactura = @Folio
                ORDER BY Partida";

            List<List<string>> resultado = new List<List<string>>();

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        List<string> fila = new List<string>
                        {
                            Txt(dr["ClaveProducto"]),   // [0] clave
                            Txt(dr["Cantidad"]),        // [1] cantidad
                            "0",                        // [2] costeo (TODO: ver nota arriba)
                            Txt(dr["Precio"]),          // [3] precio
                            Txt(dr["Partida"]),         // [4] partida
                            Txt(dr["Unidad"]),          // [5] unidad
                            Txt(dr["Total"])            // [6] total
                        };
                        resultado.Add(fila);
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Llena cmbDocumento con los documentos del catálogo aplicables a
        /// Factura (Tarea = 'Factura'), mostrando "Clave - Nombre" como texto
        /// plano de cada item (mismo patrón que SeleccionarCategorias /
        /// SeleccionarDivisa en DBProductosServicios: un combo de solo texto,
        /// sin DataSource/ValueMember, porque cmbDocumento.Text ya se usa así
        /// más adelante en cmbDocumento_SelectedIndexChanged).
        /// </summary>
        public void SeleccionarFactura(ComboBox cb)
        {
            cb.Items.Clear();

            const string sql = "SELECT (Clave + ' - ' + Nombre) AS Nombre FROM Documento WHERE Tarea = 'Factura'";

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

        /// <summary>
        /// Carga las Facturas con saldo pendiente (Saldo != 0) de un
        /// cliente, en el MISMO formato de celdas por índice que
        /// DBRemiision.CargarRemisionCobro (columnas 1..9: ClaveDocumento,
        /// Folio, Consecutivo, -, Nombre, Fecha, Total, Saldo, FechaVence),
        /// para poder mostrarse en la misma grilla "dgvPagosPendientes"
        /// junto con las Remisiones.
        ///
        /// Cada fila agregada queda marcada en su .Tag con la cadena
        /// "Factura". Esto es necesario porque Factura.Folio y
        /// Remision.Folio son secuencias independientes y pueden coincidir
        /// — sin este marcador, al seleccionar filas para pagar no habría
        /// forma de saber a qué tabla pertenece cada Folio.
        ///
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar — úsalo
        /// cuando esta sea la única fuente de datos del grid. Si vas a
        /// combinar con CargarRemisionCobro en el mismo grid, limpia una
        /// sola vez desde el formulario y llama a ambos métodos con
        /// limpiarPrimero=false.
        /// </param>
        /// <param name="claveCentroCostos">
        /// Clave de Centro de Costos para filtrar (int como string). Si
        /// viene null, vacio, o "0" (fila "TODOS" del combo), no se filtra
        /// y se traen las Facturas de todos los centros de costos.
        /// </param>
        /// </summary>
        public void CargarFacturaCobro(DataGridView dgv, string matricula, bool limpiarPrimero = true, string claveCentroCostos = null)
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
                    SELECT F.*, D.Nombre
                    FROM Factura AS F
                    INNER JOIN Documento AS D ON F.ClaveDocumento = D.Clave
                    WHERE F.ClaveProveedor = @Matricula AND F.Saldo <> 0
                      AND (@CentroCostos IS NULL OR F.CentroCostos = @CentroCostos)";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);

                    object valorCentroCostos = (string.IsNullOrWhiteSpace(claveCentroCostos) || claveCentroCostos == "0")
                        ? (object)DBNull.Value
                        : Convert.ToInt32(claveCentroCostos);
                    cmd.Parameters.AddWithValue("@CentroCostos", valorCentroCostos);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        decimal total = Convert.ToDecimal(item["Total"]);

                        dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[7].Value = total;
                        dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[9].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");

                        dgv.Rows[n].Tag = "Factura";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Facturas pendientes: " + ex.ToString());
            }
        }

        /// <summary>
        /// Equivalente a DBRemiision.CargarReciboCobroById pero para
        /// Factura: carga en la grilla de RegistrarCobro (columnas por
        /// índice: 1=FolioDocumento, 2=Documento(clave), 3=Concepto(nombre
        /// del tipo de documento), 4 y 7=Importe/Saldo inicial) los folios
        /// de Factura que el usuario seleccionó en registroIngresos.
        ///
        /// Cada fila agregada queda marcada con .Tag = "Factura", para que
        /// RegistrarCobro sepa, al confirmar el cobro, que a esa fila le
        /// corresponde actualizar Factura (no Remision) y guardar
        /// TipoConcepto = "Factura" en Cobros.
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Si vas a
        /// combinar con DBRemiision.CargarReciboCobroById en el mismo grid,
        /// limpia una sola vez desde el formulario y llama a ambos con
        /// limpiarPrimero=false.
        /// </param>
        public void CargarFacturaCobroById(DataGridView dgv, string matricula, ArrayList listaFolios, bool limpiarPrimero = true)
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

                    const string sql = @"
                        SELECT F.*, 0.00 AS DescuentoPago, 0.00 AS RecargosAcumulados, D.Nombre
                        FROM Factura AS F, Documento AS D
                        WHERE F.ClaveProveedor = @Matricula AND F.Folio = @Folio AND F.ClaveDocumento = D.Clave";

                    foreach (object item2 in listaFolios)
                    {
                        string folio = item2.ToString();

                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@Matricula", matricula);
                            cmd.Parameters.AddWithValue("@Folio", folio);

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

                                dgv.Rows[n].Tag = "Factura";
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

        /// <summary>
        /// Aplica un abono a una Factura: reduce el Saldo, registra el
        /// Descuento de pago (igual semántica que DBRemiision.ActualizarRemision)
        /// y además guarda UltimoAbono — el campo que agregamos al esquema
        /// de Factura y que quedaba pendiente de llenar hasta que existiera
        /// un flujo real de abonos. Este es ese flujo.
        /// </summary>
        public void ActualizarFacturaAbono(string folio, decimal descuento, decimal abono)
        {
            const string sql = @"
                UPDATE Factura
                SET DescuentoPago = @Descuento,
                    Saldo = Saldo - @Abono,
                    UltimoAbono = @Abono
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@Abono", abono);
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folio));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void ConsecutivoFactura(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from Factura where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
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
        public void CargarFacturaPendienteAnticipo(DataGridView dgv, string claveCliente, bool limpiarPrimero = false)
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
            SELECT F.Folio, F.Consecutivo, F.ClaveDocumento, D.Nombre, F.Saldo
            FROM Factura AS F
            INNER JOIN Documento AS D ON F.ClaveDocumento = D.Clave
            WHERE F.ClaveProveedor = @ClaveCliente AND F.Saldo > 0";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                        dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                        dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                        dgv.Rows[n].Tag = "FACTURA";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Facturas pendientes de aplicación de Anticipo: " + ex.ToString());
            }
        }


        /// <summary>
        /// Genera el XML de una Factura (encabezado + emisor + cliente + partidas),
        /// misma estructura que GenerarXmlRemision pero usando Factura/PartidaFactura/Servicios.
        /// </summary>
        public string GenerarXmlFactura(string folio)
        {
            const string sqlFactura = @"
                SELECT F.Folio, F.ClaveDocumento, F.Estatus, F.Fecha,
                       F.Divisa, F.TipoCambio, F.Subtotal, F.Descuento, F.Cargo, F.Total,
                       F.TotalPartidas, F.Notas, F.Elaborado, F.Saldo, F.FolioOrden, F.Consecutivo,
                       F.Almacen, F.Referencia, F.FechaVence, F.Autorizado, F.CentroCostos, F.IdProyecto,
                       C.IdCliente, C.RazonSocial, C.RFC, C.Calle, C.NoExterior, C.NoInterior,
                       C.Colonia, C.Municipio, C.CodigoPostal, C.Ciudad, C.Pais
                FROM Factura AS F
                LEFT JOIN Clientes AS C ON F.ClaveProveedor = C.IdCliente
                WHERE F.Folio = @Folio";

            const string sqlEmpresa = @"
                SELECT TOP 1 RazonSocial, NombreComercial, RFC, Telefono1, Correo, PaginaWeb,
                       CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais
                FROM DatosEmpresa";

            const string sqlPartidas = @"
                SELECT PF.Partida, PF.ClaveProducto, PF.Concepto2, PF.Cantidad, PF.Unidad,
                       PF.Divisa, PF.TipoCambio, PF.Subtotal, PF.Descuento, PF.Total,
                       PF.Precio, PF.CantidadRecibida, PF.Impuesto,
                       S.Descripcion AS DescripcionProducto, S.Alias, S.UnidadMedida
                FROM PartidaFactura AS PF
                LEFT JOIN Servicios AS S ON PF.ClaveProducto = S.ClaveServicio
                WHERE PF.FolioFactura = @Folio
                ORDER BY PF.Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();

                // --- Datos de la Factura (encabezado) ---
                DataRow facturaRow = null;
                using (SqlCommand cmd = new SqlCommand(sqlFactura, cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count == 0)
                            return null; // no existe esa factura

                        facturaRow = dt.Rows[0];
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

                // --- Partidas de la Factura ---
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
                    writer.WriteStartElement("Factura");

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

                    // Datos generales de la factura
                    writer.WriteElementString("Folio", facturaRow["Folio"].ToString());
                    writer.WriteElementString("ClaveDocumento", facturaRow["ClaveDocumento"].ToString());
                    writer.WriteElementString("Estatus", facturaRow["Estatus"].ToString());
                    writer.WriteElementString("Fecha", facturaRow["Fecha"] == DBNull.Value ? "" : Convert.ToDateTime(facturaRow["Fecha"]).ToString("yyyy-MM-dd"));
                    writer.WriteElementString("FechaVence", facturaRow["FechaVence"] == DBNull.Value ? "" : Convert.ToDateTime(facturaRow["FechaVence"]).ToString("yyyy-MM-dd"));
                    writer.WriteElementString("Divisa", facturaRow["Divisa"].ToString());
                    writer.WriteElementString("TipoCambio", facturaRow["TipoCambio"].ToString());
                    writer.WriteElementString("Subtotal", facturaRow["Subtotal"].ToString());
                    writer.WriteElementString("Descuento", facturaRow["Descuento"].ToString());
                    writer.WriteElementString("Cargo", facturaRow["Cargo"].ToString());
                    writer.WriteElementString("Total", facturaRow["Total"].ToString());
                    writer.WriteElementString("Saldo", facturaRow["Saldo"].ToString());
                    writer.WriteElementString("Notas", facturaRow["Notas"].ToString());
                    writer.WriteElementString("Elaborado", facturaRow["Elaborado"].ToString());
                    writer.WriteElementString("Almacen", facturaRow["Almacen"].ToString());
                    writer.WriteElementString("Referencia", facturaRow["Referencia"].ToString());
                    writer.WriteElementString("Autorizado", facturaRow["Autorizado"].ToString());
                    writer.WriteElementString("CentroCostos", facturaRow["CentroCostos"].ToString());
                    writer.WriteElementString("IdProyecto", facturaRow["IdProyecto"].ToString());

                    // Bloque Cliente (receptor)
                    writer.WriteStartElement("Cliente");
                    writer.WriteElementString("IdCliente", facturaRow["IdCliente"].ToString());
                    writer.WriteElementString("RazonSocial", facturaRow["RazonSocial"].ToString());
                    writer.WriteElementString("RFC", facturaRow["RFC"].ToString());
                    writer.WriteElementString("Calle", facturaRow["Calle"].ToString());
                    writer.WriteElementString("NoExterior", facturaRow["NoExterior"].ToString());
                    writer.WriteElementString("NoInterior", facturaRow["NoInterior"].ToString());
                    writer.WriteElementString("Colonia", facturaRow["Colonia"].ToString());
                    writer.WriteElementString("Municipio", facturaRow["Municipio"].ToString());
                    writer.WriteElementString("CodigoPostal", facturaRow["CodigoPostal"].ToString());
                    writer.WriteElementString("Ciudad", facturaRow["Ciudad"].ToString());
                    writer.WriteElementString("Pais", facturaRow["Pais"].ToString());
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

                    writer.WriteEndElement(); // Factura
                    writer.WriteEndDocument();
                    writer.Flush();

                    return sw.ToString();
                }
            }
        }

        #endregion
    }
}