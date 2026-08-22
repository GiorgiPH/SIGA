using PV.Properties;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases.Facturas
{
    /// <summary>
    /// Capa de datos del módulo de Facturas. Encapsula únicamente lo que es
    /// propio de este dominio: el encabezado [Factura] y el detalle
    /// [PartidaFactura].
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
            Control txtSubtotal, Control txtDescuento, Control txtRecargo, Control txtTotal, Control txtPartidas,
            Control txtNotas, Control txtElaborado, Control txtFolio, Control txtConsecutivo, Control txtAutoriza,
            Control txtFechaAuto, Control cmbAlmacen, out string cliente, Control cmbCentroCostos, Control cmbProyecto)
        {
            cliente = string.Empty;

            const string sql = @"
                SELECT ClaveDocumento, Estatus, Fecha, Divisa, TipoCambio, Subtotal, Descuento, Recargo, Total,
                       TotalPartidas, Notas, Elaborado, Consecutivo, Autorizado, FechaAutoriza, UsuarioAutoriza,
                       Almacen, ClaveProveedor, CentroCostos, IdProyecto, DiasVence, FechaVence
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
                    txtNotas.Text = Txt(dr["Notas"]);
                    txtElaborado.Text = Txt(dr["Elaborado"]);
                    txtConsecutivo.Text = Txt(dr["Consecutivo"]);
                    txtAutoriza.Text = Txt(dr["UsuarioAutoriza"]);
                    txtFechaAuto.Text = dr["FechaAutoriza"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy/MM/dd");
                    txtDiasVence.Text = Txt(dr["DiasVence"]);
                    txtFechaVence.Text = dr["FechaVence"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy/MM/dd");
                    cmbAlmacen.Text = Txt(dr["Almacen"]);
                    cmbCentroCostos.Text = Txt(dr["CentroCostos"]);
                    cmbProyecto.Text = Txt(dr["IdProyecto"]);

                    cliente = Txt(dr["ClaveProveedor"]);
                }
            }

            txtFolio.Text = folio;
        }

        /// <summary>Carga únicamente los totales/saldo del encabezado (equivalente a ReciboSaldos).</summary>
        public void ReciboSaldos(string folio, Control txtSubtotal, Control txtDescuento, Control txtRecargo,
            Control txtTotal, Control txtPartidas)
        {
            const string sql = @"SELECT Subtotal, Descuento, Recargo, Total, TotalPartidas FROM Factura WHERE Folio = @Folio";

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
                    txtRecargo.Text = Txt(dr["Recargo"]);
                    txtTotal.Text = Txt(dr["Total"]);
                    txtPartidas.Text = Txt(dr["TotalPartidas"]);
                }
            }
        }

        /// <summary>
        /// Llena una grilla con Facturas. Cuando <paramref name="incluirTodas"/>
        /// es false trae únicamente las que están "Abierto" (alias de columna
        /// "Folio"); cuando es true trae todas sin filtrar por estatus (alias
        /// de columna "Folio2"). Los alias replican los nombres de columna que
        /// ya consume el code-behind original (Cells["Folio"] / Cells["Folio2"]).
        /// </summary>
        public void CargarFacturas(DataGridView dgv, string filtroFolio, string filtroDocumento,
            string filtroNombre, bool incluirTodas)
        {
            string colFolio = incluirTodas ? "Folio2" : "Folio";
            string filtroEstatus = incluirTodas ? string.Empty : " AND f.Estatus = 'Abierto'";

            string sql = $@"
                SELECT f.Folio AS [{colFolio}], f.ClaveDocumento AS Documento, f.Fecha,
                       cli.Nombre AS Cliente, f.Total, f.Saldo, f.UltimoAbono, f.Estatus, f.FechaVence
                FROM Factura f
                LEFT JOIN Clientes cli ON cli.IdCliente = f.ClaveProveedor
                WHERE 1 = 1 {filtroEstatus}
                  AND (@FiltroFolio = '' OR CAST(f.Folio AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%')
                  AND (@FiltroDocumento = '' OR f.ClaveDocumento LIKE '%' + @FiltroDocumento + '%')
                  AND (@FiltroNombre = '' OR cli.Nombre LIKE '%' + @FiltroNombre + '%')
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
        public void ActualizarTotalesFactura(string folioFactura, string totalPartidas)
        {
            const string sql = @"
                UPDATE Factura SET
                    Subtotal = ISNULL((SELECT SUM(Subtotal) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Descuento = ISNULL((SELECT SUM(Descuento) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Recargo = ISNULL((SELECT SUM(Impuesto) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    Total = ISNULL((SELECT SUM(Total) FROM PartidaFactura WHERE FolioFactura = @Folio), 0),
                    TotalPartidas = @TotalPartidas
                WHERE Folio = @Folio;

                UPDATE Factura SET Saldo = Total WHERE Folio = @Folio;";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Convert.ToInt32(folioFactura));
                cmd.Parameters.AddWithValue("@TotalPartidas", string.IsNullOrWhiteSpace(totalPartidas) ? 0 : Convert.ToInt32(totalPartidas));
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

        public void InsertarPartidaFactura(string folioFactura, string partida, string claveProducto, string concepto2,
            string cantidad, string unidad, string divisa, string tipoCambio, decimal subtotal, decimal descuento,
            decimal total, decimal precio, decimal impuesto)
        {
            const string sql = @"
                INSERT INTO PartidaFactura
                    (FolioFactura, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio,
                     Subtotal, Descuento, Total, Precio, CantidadRecibida, Impuesto)
                VALUES
                    (@FolioFactura, @Partida, @ClaveProducto, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio,
                     @Subtotal, @Descuento, @Total, @Precio, 0, @Impuesto)";

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

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Carga una partida existente en los controles del panel de edición.</summary>
        public void ConsultaPartidaFactura(string folioFactura, string partida, Control cmbConcepto2, Control txtConcepto,
            Control txtConcepto2, Control txtCantidad, Control txtUnidad, Control txtDivisa, Control txtTipoCambio,
            Control txtImporte, Control txtDescuento, Control txtTotal, Control txtPrecio, Control txtImpuesto,
            Control txtEntregado, Control cmbConcepto)
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

                    string clave = Txt(dr["ClaveProducto"]);
                    cmbConcepto2.Text = clave;
                    cmbConcepto.Text = clave;
                    txtConcepto2.Text = Txt(dr["Concepto2"]);
                    txtConcepto.Text = Txt(dr["Concepto2"]);
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

        /// <summary>Llena la grilla de partidas de una Factura.</summary>
        public void CargarPartidasFactura(DataGridView dgv, string folioFactura)
        {
            const string sql = @"
                SELECT Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Precio, Descuento, Impuesto, Total, CantidadRecibida
                FROM PartidaFactura
                WHERE FolioFactura = @Folio
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

        #endregion
    }
}