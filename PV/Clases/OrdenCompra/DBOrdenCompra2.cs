using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace PV.Clases.OrdenCompra
{
    /// <summary>
    /// Capa de datos del módulo de ORDEN DE COMPRA (compras a proveedores):
    /// únicamente el encabezado [OrdenCompra] y el detalle [PartidaOrden].
    /// Esta clase ya NO contiene nada de Remisión ni de Pedido a Cliente (la
    /// versión anterior los mezclaba); esos métodos pertenecen a DBRemiision
    /// / DBPedidoCliente.
    ///
    /// Convención (igual que DBFacturas / DBCotizaciones):
    ///   - Cada método abre y cierra su propia conexión con "using".
    ///   - Todo SqlDataReader se consume dentro de un "using".
    ///   - Siempre parámetros SQL, nunca concatenar texto.
    ///   - Los controles se reciben como "Control" (sólo se toca .Text).
    ///   - Los errores de base de datos se propagan (excepción): el
    ///     formulario es quien decide cómo mostrarlos.
    ///
    /// Semántica de la partida (PartidaOrden), igual que en Factura/Remisión
    /// y que lo que asume sp_ReporteDiarioOrdenesCompra:
    ///   Subtotal  = importe BRUTO (precio * cantidad), antes de descuento.
    ///   Descuento = PORCENTAJE.
    ///   Impuesto  = PORCENTAJE (se aplica sobre el subtotal ya descontado).
    ///   Total     = subtotal - descuento + impuesto.
    /// En el encabezado: Subtotal = suma de brutos, Descuento = suma de los
    /// importes de descuento, Cargo = suma de los importes de impuesto
    /// (Recargo no se usa), Total = suma de totales.
    /// </summary>
    public class DBOrdenCompra2
    {
        /// <summary>
        /// Valor de Documento.Tarea que identifica a los documentos de Orden
        /// de Compra en el catálogo (igual que 'Factura' en DBFacturas).
        /// Si en tu tabla Documento se llama distinto, cámbialo aquí: es el
        /// único lugar.
        /// </summary>
        private const string TareaDocumento = "Pedidos Proveedores";

        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        #region Helpers privados

        private static string Txt(object valor)
        {
            return (valor == null || valor == DBNull.Value) ? string.Empty : valor.ToString();
        }

        /// <summary>Decimal como texto "0.00" (invariante); NULL se muestra como 0.00.</summary>
        private static string DecTxt(object valor)
        {
            return (valor == null || valor == DBNull.Value)
                ? "0.00"
                : Convert.ToDecimal(valor).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private static string FechaTxt(object valor)
        {
            return (valor == null || valor == DBNull.Value)
                ? string.Empty
                : Convert.ToDateTime(valor).ToString("yyyy/MM/dd");
        }

        private static object DateOrNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : Convert.ToDateTime(s);
        }

        private static object TextoOrNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;
        }

        /// <summary>Recorta a la longitud de la columna (para textos tomados del catálogo, p. ej. descripción/unidad).</summary>
        private static string Truncar(string valor, int maximo)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;
            return valor.Length <= maximo ? valor : valor.Substring(0, maximo);
        }

        #endregion

        #region Catálogos auxiliares (documento, proveedor, producto)

        /// <summary>Llena cmbDocumento con "Clave - Nombre" de los documentos de Orden de Compra.</summary>
        public void SeleccionarOrdenCompra(ComboBox cb)
        {
            cb.Items.Clear();

            const string sql = "SELECT (Clave + ' - ' + Nombre) AS Nombre FROM Documento WHERE Tarea = @Tarea ORDER BY Clave";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Tarea", TareaDocumento);
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

        /// <summary>Regresa [Nombre, Clave] a partir del texto "Clave - Nombre" del combo; null si no existe.</summary>
        public string[] InformacionDocumento(string textoCombo)
        {
            return ObtenerDocumento("(Clave + ' - ' + Nombre) = @Valor", textoCombo);
        }

        /// <summary>Regresa [Nombre, Clave] a partir de la clave del documento; null si no existe.</summary>
        public string[] InformacionDocumento2(string clave)
        {
            return ObtenerDocumento("Clave = @Valor", clave);
        }
   
        // "condicion" es siempre una constante interna (nunca texto del usuario);
        // el valor viaja como parámetro.
        private string[] ObtenerDocumento(string condicion, string valor)
        {
            string sql = "SELECT Nombre, Clave FROM Documento WHERE " + condicion + " AND Tarea = @Tarea";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Valor", (object)valor ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tarea", TareaDocumento);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return null;

                    return new[] { Txt(dr["Nombre"]), Txt(dr["Clave"]) };
                }
            }
        }

        /// <summary>Calcula el siguiente Consecutivo (folio visible) del documento y lo coloca en txtConsecutivo.</summary>
        public void ConsecutivoOrdenCompra(Control txtConsecutivo, string claveDocumento)
        {
            const string sql = "SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM OrdenCompra WHERE ClaveDocumento = @Clave";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Clave", (object)claveDocumento ?? DBNull.Value);
                cn.Open();
                txtConsecutivo.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }

        /// <summary>Regresa [RazonSocial] del proveedor; null si no existe o la clave no es numérica.</summary>
        public string[] InformacionProveedor(string idProveedor)
        {
            int id;
            if (!int.TryParse(idProveedor, out id))
                return null;

            const string sql = "SELECT RazonSocial FROM Proveedor WHERE IdProveedor = @Id";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return null;

                    return new[] { Txt(dr["RazonSocial"]) };
                }
            }
        }

        /// <summary>
        /// Datos del producto para precargar una partida de COMPRA. Regresa:
        ///   [0] Descripcion, [1] ClaveProducto,
        ///   [2] precio unitario = CostoUnitario (en una compra se paga el
        ///       costo, no el precio de venta),
        ///   [3] UnidadMedida, [4] ImpuestoPorc (%).
        /// El descuento no se precarga (lo captura el usuario, 0 por defecto).
        /// Regresa null si el producto no existe.
        /// </summary>
        public string[] InformacionProductoCompra(int claveProducto)
        {
            const string sql = @"
                SELECT Descripcion, ClaveProducto, CostoUnitario, UnidadMedida, ImpuestoPorc
                FROM ProductosServicios
                WHERE ClaveProducto = @Clave";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Clave", claveProducto);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return null;

                    return new[]
                    {
                        Txt(dr["Descripcion"]),
                        Txt(dr["ClaveProducto"]),
                        DecTxt(dr["CostoUnitario"]),
                        Txt(dr["UnidadMedida"]),
                        DecTxt(dr["ImpuestoPorc"])
                    };
                }
            }
        }

        #endregion

        #region Encabezado - OrdenCompra

        /// <summary>
        /// Da de alta el encabezado de una Orden de Compra. El Folio
        /// (MAX+1) y el Consecutivo por documento se calculan dentro de UNA
        /// transacción con TABLOCKX/HOLDLOCK, igual que DBFacturas.InsertarFactura,
        /// para que dos usuarios no obtengan el mismo número. Folio y
        /// Consecutivo definitivos se devuelven en txtFolio / txtConsecutivo
        /// (el consecutivo mostrado antes de guardar era sólo una vista previa).
        ///
        /// "almacen" se guarda en 0, igual que hacía la versión anterior: la
        /// orden de compra no captura almacén (eso es de la recepción).
        /// </summary>
        public void InsertarOrden(Control txtFolio, Control txtConsecutivo, string claveDocumento, string estatus,
            string fecha, string diasVence, string fechaVence, string claveProveedor, string divisa,
            string tipoCambio, string notas, string elaborado)
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
                            "SELECT ISNULL(MAX(Folio), 0) + 1 FROM OrdenCompra WITH (TABLOCKX, HOLDLOCK)", cn, tx))
                        {
                            folio = Convert.ToInt32(cmdFolio.ExecuteScalar());
                        }

                        int consecutivo;
                        using (SqlCommand cmdCons = new SqlCommand(
                            "SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM OrdenCompra WHERE ClaveDocumento = @Clave", cn, tx))
                        {
                            cmdCons.Parameters.AddWithValue("@Clave", (object)claveDocumento ?? DBNull.Value);
                            consecutivo = Convert.ToInt32(cmdCons.ExecuteScalar());
                        }

                        const string sql = @"
                            INSERT INTO OrdenCompra
                                (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClaveProveedor, Divisa,
                                 TipoCambio, Subtotal, Descuento, Cargo, Total, TotalPartidas, Notas, Elaborado,
                                 Recargo, DescuentoPago, Saldo, Consecutivo, almacen)
                            VALUES
                                (@Folio, @ClaveDocumento, @Estatus, @Fecha, @DiasVencen, @FechaVence, @ClaveProveedor, @Divisa,
                                 @TipoCambio, 0, 0, 0, 0, 0, @Notas, @Elaborado,
                                 0, 0, 0, @Consecutivo, 0)";

                        using (SqlCommand cmd = new SqlCommand(sql, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Folio", folio);
                            cmd.Parameters.AddWithValue("@ClaveDocumento", (object)claveDocumento ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Estatus", (object)estatus ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Fecha", DateOrNull(fecha));
                            cmd.Parameters.AddWithValue("@DiasVencen", string.IsNullOrWhiteSpace(diasVence) ? 0 : Convert.ToInt32(diasVence));
                            cmd.Parameters.AddWithValue("@FechaVence", DateOrNull(fechaVence));
                            cmd.Parameters.AddWithValue("@ClaveProveedor", Convert.ToInt32(claveProveedor));
                            cmd.Parameters.AddWithValue("@Divisa", (object)divisa ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(tipoCambio) ? 1.00m : Convert.ToDecimal(tipoCambio));
                            cmd.Parameters.AddWithValue("@Notas", TextoOrNull(notas));
                            cmd.Parameters.AddWithValue("@Elaborado", TextoOrNull(elaborado));
                            cmd.Parameters.AddWithValue("@Consecutivo", consecutivo);

                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();

                        txtFolio.Text = folio.ToString();
                        txtConsecutivo.Text = consecutivo.ToString();
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
        /// Carga el encabezado de una Orden de Compra en los controles del
        /// formulario. Regresa false si el folio no existe. El proveedor se
        /// devuelve por "out" (ya no por un campo estático). El control
        /// "txtRecargo" muestra el campo Cargo (impuesto total), igual que en
        /// Facturas.
        /// </summary>
        public bool ConsultaOrden(int folio, Control txtClave, Control cmbEstatus, Control txtFecha,
            Control txtDiasVence, Control txtFechaVence, Control txtDivisa, Control txtTipoCambio,
            Control txtSubtotal, Control txtDescuento, Control txtRecargo, Control txtTotal, Control txtPartidas,
            Control txtNotas, Control txtElaborado, Control txtFolio, Control txtConsecutivo,
            Control txtAutoriza, Control txtFechaAuto, out string proveedor)
        {
            proveedor = string.Empty;

            const string sql = @"
                SELECT ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClaveProveedor, Divisa, TipoCambio,
                       Subtotal, Descuento, Cargo, Total, TotalPartidas, Notas, Elaborado, Consecutivo,
                       UsuarioAutoriza, FechaAutoriza
                FROM OrdenCompra
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return false;

                    txtClave.Text = Txt(dr["ClaveDocumento"]);
                    cmbEstatus.Text = Txt(dr["Estatus"]);
                    txtFecha.Text = FechaTxt(dr["Fecha"]);
                    txtDiasVence.Text = Txt(dr["DiasVencen"]);
                    txtFechaVence.Text = FechaTxt(dr["FechaVence"]);
                    txtDivisa.Text = Txt(dr["Divisa"]);
                    txtTipoCambio.Text = DecTxt(dr["TipoCambio"]);
                    txtSubtotal.Text = DecTxt(dr["Subtotal"]);
                    txtDescuento.Text = DecTxt(dr["Descuento"]);
                    txtRecargo.Text = DecTxt(dr["Cargo"]);
                    txtTotal.Text = DecTxt(dr["Total"]);
                    txtPartidas.Text = Txt(dr["TotalPartidas"]);
                    txtNotas.Text = Txt(dr["Notas"]);
                    txtElaborado.Text = Txt(dr["Elaborado"]);
                    txtConsecutivo.Text = Txt(dr["Consecutivo"]);
                    txtAutoriza.Text = Txt(dr["UsuarioAutoriza"]);
                    txtFechaAuto.Text = FechaTxt(dr["FechaAutoriza"]);
                    proveedor = Txt(dr["ClaveProveedor"]);
                }
            }

            txtFolio.Text = folio.ToString();
            return true;
        }

        /// <summary>
        /// Carga los totales del encabezado (Subtotal, Descuento, Cargo,
        /// Total, TotalPartidas) en los controles indicados. "txtPartidas"
        /// puede ser null: así este mismo método sirve también para el panel
        /// de totales "en vivo" de la captura de partidas (txtSubtotalR,
        /// txtDescuentoR, txtImpuestoR, txtTotalR), sin duplicar la consulta.
        /// Los totales se mantienen al día con ActualizarTotalesOrden.
        /// </summary>
        public void ReciboSaldos(int folio, Control txtSubtotal, Control txtDescuento, Control txtRecargo,
            Control txtTotal, Control txtPartidas)
        {
            const string sql = "SELECT Subtotal, Descuento, Cargo, Total, TotalPartidas FROM OrdenCompra WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return;

                    txtSubtotal.Text = DecTxt(dr["Subtotal"]);
                    txtDescuento.Text = DecTxt(dr["Descuento"]);
                    txtRecargo.Text = DecTxt(dr["Cargo"]);
                    txtTotal.Text = DecTxt(dr["Total"]);

                    if (txtPartidas != null)
                    {
                        txtPartidas.Text = dr["TotalPartidas"] == DBNull.Value ? "0" : Txt(dr["TotalPartidas"]);
                    }
                }
            }
        }

        /// <summary>
        /// Llena una grilla de encabezados. "autorizadas" = true trae las
        /// órdenes con Autorizado = 'SI'; false trae las que todavía no están
        /// autorizadas (sin contar las canceladas). Los tres filtros se
        /// combinan con AND; el de folio busca tanto en Folio como en
        /// Consecutivo (que es el número que ve el usuario). La grilla debe
        /// tener AutoGenerateColumns = false.
        /// </summary>
        public void CargarOrdenes(DataGridView dgv, string filtroFolio, string filtroDocumento,
            string filtroProveedor, bool autorizadas)
        {
            string filtroAutorizacion = autorizadas
                ? " AND oc.Autorizado = 'SI'"
                : " AND ISNULL(oc.Autorizado, '') <> 'SI' AND ISNULL(oc.Estatus, '') <> 'Cancelado'";

            string sql = $@"
                SELECT oc.Folio, oc.Consecutivo, oc.ClaveDocumento AS Documento,
                       p.RazonSocial AS Proveedor, oc.Fecha, oc.Estatus
                FROM OrdenCompra AS oc
                LEFT JOIN Proveedor AS p ON p.IdProveedor = oc.ClaveProveedor
                WHERE 1 = 1 {filtroAutorizacion}
                  AND (@FiltroFolio = ''
                       OR CAST(oc.Folio AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%'
                       OR CAST(oc.Consecutivo AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%')
                  AND (@FiltroDocumento = '' OR oc.ClaveDocumento LIKE '%' + @FiltroDocumento + '%')
                  AND (@FiltroProveedor = '' OR p.RazonSocial LIKE '%' + @FiltroProveedor + '%')
                ORDER BY oc.Folio DESC";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@FiltroFolio", filtroFolio ?? string.Empty);
                cmd.Parameters.AddWithValue("@FiltroDocumento", filtroDocumento ?? string.Empty);
                cmd.Parameters.AddWithValue("@FiltroProveedor", filtroProveedor ?? string.Empty);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        /// <summary>
        /// Recalcula Subtotal / Descuento / Cargo(impuesto) / Total /
        /// TotalPartidas del encabezado a partir de [PartidaOrden]. El
        /// importe de descuento e impuesto de cada renglón se redondea a 2
        /// decimales con la MISMA regla que usa el formulario (descuento
        /// sobre el bruto; impuesto sobre el bruto ya descontado), de modo
        /// que siempre se cumple Total = Subtotal - Descuento + Cargo. El
        /// Saldo se iguala al Total (igual que Facturas, mientras no exista
        /// un módulo de pagos que lo ajuste).
        /// </summary>
        public void ActualizarTotalesOrden(int folio)
        {
            const string sql = @"
                UPDATE oc SET
                    oc.Subtotal      = t.Subtotal,
                    oc.Descuento     = t.Descuento,
                    oc.Cargo         = t.Impuesto,
                    oc.Total         = t.Total,
                    oc.Saldo         = t.Total,
                    oc.TotalPartidas = t.Partidas
                FROM OrdenCompra AS oc
                CROSS APPLY (
                    SELECT ISNULL(SUM(l.Bruto), 0)     AS Subtotal,
                           ISNULL(SUM(l.Descuento), 0) AS Descuento,
                           ISNULL(SUM(l.Impuesto), 0)  AS Impuesto,
                           ISNULL(SUM(l.Total), 0)     AS Total,
                           COUNT(*)                    AS Partidas
                    FROM (
                        SELECT ISNULL(po.Subtotal, 0) AS Bruto,
                               ROUND(ISNULL(po.Subtotal, 0) * ISNULL(po.Descuento, 0) / 100.0, 2) AS Descuento,
                               ROUND((ISNULL(po.Subtotal, 0)
                                      - ROUND(ISNULL(po.Subtotal, 0) * ISNULL(po.Descuento, 0) / 100.0, 2))
                                     * ISNULL(po.Impuesto, 0) / 100.0, 2) AS Impuesto,
                               ISNULL(po.Total, 0) AS Total
                        FROM PartidaOrden AS po
                        WHERE po.FolioOrden = oc.Folio
                    ) AS l
                ) AS t
                WHERE oc.Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Cambia el estatus ("Abierto" / "Bloqueado" / "Cancelado"). Sirve para confirmar y para cancelar.</summary>
        public void ActualizarOrdenEstatus(int folio, string estatus)
        {
            const string sql = "UPDATE OrdenCompra SET Estatus = @Estatus WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Registra la autorización de la orden (flujo de AutentificarAdmin).</summary>
        public void ActualizarOrdenAuto(int folio, string usuarioAutoriza, string fechaAutoriza)
        {
            const string sql = @"
                UPDATE OrdenCompra
                SET Autorizado = 'SI', UsuarioAutoriza = @Usuario, FechaAutoriza = @Fecha
                WHERE Folio = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuarioAutoriza ?? string.Empty);
                cmd.Parameters.AddWithValue("@Fecha", DateOrNull(fechaAutoriza));
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Detalle - PartidaOrden

        /// <summary>Calcula el siguiente número de partida de la orden y lo coloca en txtPartida.</summary>
        public void Consulta5Orden(int folio, Control txtPartida)
        {
            const string sql = "SELECT ISNULL(MAX(Partida), 0) + 1 FROM PartidaOrden WHERE FolioOrden = @Folio";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                txtPartida.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
            }
        }

        /// <summary>
        /// Guarda una partida: si (folio, partida) ya existe la ACTUALIZA
        /// (sin tocar CantidadRecibida, que pertenece a la recepción) y si
        /// no, la INSERTA con CantidadRecibida = 0. Un solo método y una sola
        /// lista de columnas en vez del trío Existe/Insertar/Actualizar.
        /// </summary>
        public void GuardarPartidaOrden(int folio, int partida, int claveProducto, string concepto2, int cantidad,
            string unidad, string divisa, decimal tipoCambio, decimal subtotal, decimal descuento, decimal total,
            decimal precio, decimal impuesto)
        {
            const string sql = @"
                UPDATE PartidaOrden SET
                    ClaveProducto = @ClaveProducto,
                    Concepto2     = @Concepto2,
                    Cantidad      = @Cantidad,
                    Unidad        = @Unidad,
                    Divisa        = @Divisa,
                    TipoCambio    = @TipoCambio,
                    Subtotal      = @Subtotal,
                    Descuento     = @Descuento,
                    Total         = @Total,
                    Precio        = @Precio,
                    Impuesto      = @Impuesto
                WHERE FolioOrden = @Folio AND Partida = @Partida;

                IF @@ROWCOUNT = 0
                    INSERT INTO PartidaOrden
                        (FolioOrden, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio,
                         Subtotal, Descuento, Total, Precio, CantidadRecibida, Impuesto)
                    VALUES
                        (@Folio, @Partida, @ClaveProducto, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio,
                         @Subtotal, @Descuento, @Total, @Precio, 0, @Impuesto);";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cmd.Parameters.AddWithValue("@Partida", partida);
                cmd.Parameters.AddWithValue("@ClaveProducto", claveProducto);
                cmd.Parameters.AddWithValue("@Concepto2", TextoOrNull(Truncar(concepto2, 50)));
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Unidad", TextoOrNull(Truncar(unidad, 50)));
                cmd.Parameters.AddWithValue("@Divisa", TextoOrNull(divisa));
                cmd.Parameters.AddWithValue("@TipoCambio", tipoCambio);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Descuento", descuento);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Impuesto", impuesto);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Carga una partida existente en los controles del panel de captura.
        /// Regresa false si no existe. La ClaveProducto se devuelve por "out"
        /// para que el formulario seleccione el producto en cmbConcepto por
        /// clave (no por texto, que puede repetirse).
        /// </summary>
        public bool ConsultaPartidaOrden(int folio, int partida, Control txtCantidad, Control txtUnidad,
            Control txtDivisa, Control txtTipoCambio, Control txtPrecio, Control txtDescuento,
            Control txtImpuesto, Control txtConcepto2, Control txtEntregado, out string claveProducto)
        {
            claveProducto = string.Empty;

            const string sql = @"
                SELECT ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Descuento,
                       Impuesto, CantidadRecibida
                FROM PartidaOrden
                WHERE FolioOrden = @Folio AND Partida = @Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cmd.Parameters.AddWithValue("@Partida", partida);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                        return false;

                    claveProducto = Txt(dr["ClaveProducto"]);

                    txtConcepto2.Text = Txt(dr["Concepto2"]);
                    txtCantidad.Text = Txt(dr["Cantidad"]);
                    txtUnidad.Text = Txt(dr["Unidad"]);
                    txtDivisa.Text = Txt(dr["Divisa"]);
                    txtTipoCambio.Text = DecTxt(dr["TipoCambio"]);
                    txtPrecio.Text = DecTxt(dr["Precio"]);
                    txtDescuento.Text = DecTxt(dr["Descuento"]);
                    txtImpuesto.Text = DecTxt(dr["Impuesto"]);
                    txtEntregado.Text = dr["CantidadRecibida"] == DBNull.Value ? "0" : Txt(dr["CantidadRecibida"]);
                }
            }

            return true;
        }

        /// <summary>
        /// Elimina una partida y renumera las siguientes para no dejar huecos.
        /// Regresa string.Empty si todo salió bien, o el mensaje de error /
        /// aviso para mostrarlo al usuario.
        /// </summary>
        public string EliminarPartidaOrden(int folio, int partida)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdDel = new SqlCommand(
                            "DELETE FROM PartidaOrden WHERE FolioOrden = @Folio AND Partida = @Partida", cn, tx))
                        {
                            cmdDel.Parameters.AddWithValue("@Folio", folio);
                            cmdDel.Parameters.AddWithValue("@Partida", partida);
                            if (cmdDel.ExecuteNonQuery() == 0)
                            {
                                tx.Rollback();
                                return "No se encontró la partida a eliminar.";
                            }
                        }

                        using (SqlCommand cmdReordena = new SqlCommand(
                            "UPDATE PartidaOrden SET Partida = Partida - 1 WHERE FolioOrden = @Folio AND Partida > @Partida", cn, tx))
                        {
                            cmdReordena.Parameters.AddWithValue("@Folio", folio);
                            cmdReordena.Parameters.AddWithValue("@Partida", partida);
                            cmdReordena.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        return "Error al eliminar la partida: " + ex.Message;
                    }
                }
            }
        }

        /// <summary>
        /// Llena la grilla de partidas de la orden (AutoGenerateColumns debe
        /// ser false): Partida, Producto, Cantidad, Precio, Subtotal, Descuento
        /// (%), Impuesto (%), Total y Recibido (CantidadRecibida).
        /// </summary>
        public void CargarPartidasOrden(DataGridView dgv, int folio)
        {
            const string sql = @"
                SELECT po.Partida,
                       ISNULL(ps.Descripcion, po.Concepto2) AS Producto,
                       po.Cantidad, po.Precio, po.Subtotal, po.Descuento, po.Impuesto, po.Total,
                       ISNULL(po.CantidadRecibida, 0) AS Recibido
                FROM PartidaOrden AS po
                LEFT JOIN ProductosServicios AS ps ON ps.ClaveProducto = po.ClaveProducto
                WHERE po.FolioOrden = @Folio
                ORDER BY po.Partida";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        #endregion
    }
}