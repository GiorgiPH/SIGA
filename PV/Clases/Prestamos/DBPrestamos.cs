using PV.dto;
using PV.dto.Prestamos;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PV.Clases.Prestamos
{
    /// <summary>
    /// Acceso a datos de dbo.Prestamo / dbo.PartidaPrestamo.
    ///
    /// A diferencia de DBFacturas (que recibía controles de WinForms
    /// directamente como parámetros), esta clase solo trabaja con tipos de
    /// datos puros (PrestamoData / PartidaPrestamoData / DataTable) y es
    /// Prestamos.cs quien se encarga de leer/escribir los controles. Esto
    /// sigue el mismo estilo que DBConceptoCobroPago.
    ///
    /// Reglas de negocio implementadas aquí (ver Prestamos.cs para el resto):
    ///   1) Al generar el calendario de pagos se crean exactamente
    ///      [Parcialidades] partidas idénticas; solo cambian Vencimiento
    ///      (calculado por el sistema: FechaPrimerPago + Periodicidad*(n-1))
    ///      y el número de Partida (generado por el sistema: 1..N).
    ///   2) Una vez generado el calendario, en cada partida solo puede
    ///      modificarse Vencimiento, y el nuevo valor debe ser >= Prestamo.Fecha
    ///      (ver ActualizarVencimientoPartida).
    /// </summary>
    public class DBPrestamos
    {
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        #region Encabezado

        /// <summary>
        /// Calcula el siguiente folio "amigable" (Consecutivo) para un tipo
        /// de documento. Ajusta esta lógica si ya existe un folio-generador
        /// centralizado en el proyecto (como el usado por Facturas/Pedidos).
        /// </summary>
        public int ObtenerSiguienteConsecutivo(string claveDocumento)
        {
            const string sql = @"
                SELECT ISNULL(MAX(Consecutivo), 0) + 1
                FROM dbo.Prestamo
                WHERE ClaveDocumento = @ClaveDocumento;";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ClaveDocumento", SqlDbType.VarChar, 10).Value = claveDocumento;
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Inserta el encabezado del préstamo y devuelve el Folio (PK,
        /// IDENTITY) recién generado. Los totales (Subtotal/Intereses/
        /// Impuestos/Total/Saldo/TotalPartidas) se insertan en 0: se
        /// calculan hasta que se genera el calendario de pagos.
        /// </summary>
        public int InsertarPrestamo(PrestamoData dato)
        {
            const string sql = @"
                INSERT INTO dbo.Prestamo
                    (Consecutivo, ClaveDocumento, Estatus, Fecha, ClaveAcreedor, IdConceptoCapital,
                     Referencia, Parcialidades, FechaPrimerPago, Periodicidad, ImporteCPago, InteresCPago,
                     IdConceptoInteres, AplicaImpuesto, Divisa, TipoCambio, Subtotal, Intereses, Impuestos,
                     Total, Saldo, TotalPartidas, Notas, Elaborado, RutaDocumento, DiasVence, FechaVence)
                VALUES
                    (@Consecutivo, @ClaveDocumento, @Estatus, @Fecha, @ClaveAcreedor, @IdConceptoCapital,
                     @Referencia, @Parcialidades, @FechaPrimerPago, @Periodicidad, @ImporteCPago, @InteresCPago,
                     @IdConceptoInteres, @AplicaImpuesto, @Divisa, @TipoCambio, 0, 0, 0,
                     0, 0, 0, @Notas, @Elaborado, @RutaDocumento, @DiasVence, @FechaVence);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Consecutivo", SqlDbType.Int).Value = (object)dato.Consecutivo ?? DBNull.Value;
                cmd.Parameters.Add("@ClaveDocumento", SqlDbType.VarChar, 10).Value = dato.ClaveDocumento;
                cmd.Parameters.Add("@Estatus", SqlDbType.VarChar, 10).Value = dato.Estatus;
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = dato.Fecha;
                cmd.Parameters.Add("@ClaveAcreedor", SqlDbType.Int).Value = dato.ClaveAcreedor;
                cmd.Parameters.Add("@IdConceptoCapital", SqlDbType.Int).Value = (object)dato.IdConceptoCapital ?? DBNull.Value;
                cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 40).Value = (object)dato.Referencia ?? DBNull.Value;
                cmd.Parameters.Add("@Parcialidades", SqlDbType.Int).Value = dato.Parcialidades;
                cmd.Parameters.Add("@FechaPrimerPago", SqlDbType.Date).Value = dato.FechaPrimerPago;
                cmd.Parameters.Add("@Periodicidad", SqlDbType.Int).Value = dato.Periodicidad;
                cmd.Parameters.Add("@ImporteCPago", SqlDbType.Decimal).Value = dato.ImporteCPago;
                cmd.Parameters.Add("@InteresCPago", SqlDbType.Decimal).Value = dato.InteresCPago;
                cmd.Parameters.Add("@IdConceptoInteres", SqlDbType.Int).Value = (object)dato.IdConceptoInteres ?? DBNull.Value;
                cmd.Parameters.Add("@AplicaImpuesto", SqlDbType.Bit).Value = dato.AplicaImpuesto;
                cmd.Parameters.Add("@Divisa", SqlDbType.VarChar, 10).Value = (object)dato.Divisa ?? DBNull.Value;
                cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = dato.TipoCambio;
                cmd.Parameters.Add("@Notas", SqlDbType.VarChar, 200).Value = (object)dato.Notas ?? DBNull.Value;
                cmd.Parameters.Add("@Elaborado", SqlDbType.VarChar, 50).Value = (object)dato.Elaborado ?? DBNull.Value;
                cmd.Parameters.Add("@RutaDocumento", SqlDbType.VarChar, 200).Value = (object)dato.RutaDocumento ?? DBNull.Value;
                cmd.Parameters.Add("@DiasVence", SqlDbType.Int).Value = dato.DiasVence;
                cmd.Parameters.Add("@FechaVence", SqlDbType.Date).Value = (object)dato.FechaVence ?? DBNull.Value;

                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void ActualizarNotas(int folio, string notas)
        {
            const string sql = "UPDATE dbo.Prestamo SET Notas = @Notas WHERE Folio = @Folio;";
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Notas", SqlDbType.VarChar, 200).Value = (object)notas ?? DBNull.Value;
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarDocumentoAdjunto(int folio, string rutaDocumento)
        {
            const string sql = "UPDATE dbo.Prestamo SET RutaDocumento = @Ruta WHERE Folio = @Folio;";
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, 200).Value = (object)rutaDocumento ?? DBNull.Value;
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarEstatus(int folio, string nuevoEstatus)
        {
            const string sql = "UPDATE dbo.Prestamo SET Estatus = @Estatus WHERE Folio = @Folio;";
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Estatus", SqlDbType.VarChar, 10).Value = nuevoEstatus;
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Trae el encabezado completo (con descripciones de Proveedor y
        /// Conceptos ya resueltas) para repintar el tab Encabezado al
        /// reabrir un préstamo existente.
        /// </summary>
        public PrestamoData ObtenerPorFolio(int folio)
        {
            const string sql = @"
                SELECT p.*, prov.RazonSocial AS NombreAcreedor,
                       cc1.Descripcion AS DescripcionConceptoCapital,
                       cc2.Descripcion AS DescripcionConceptoInteres
                FROM dbo.Prestamo p
                LEFT JOIN dbo.Proveedor prov ON prov.IdProveedor = p.ClaveAcreedor
                LEFT JOIN dbo.ConceptoCobroPago cc1 ON cc1.IdConcepto = p.IdConceptoCapital
                LEFT JOIN dbo.ConceptoCobroPago cc2 ON cc2.IdConcepto = p.IdConceptoInteres
                WHERE p.Folio = @Folio;";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    return reader.Read() ? MapearPrestamo(reader) : null;
            }
        }

        /// <summary>
        /// Lista para la grilla de consulta (dgvAutorizados). Devuelve
        /// columnas ya con alias listos para bindear (Folio, Documento,
        /// Acreedor, Fecha, Estatus).
        /// </summary>
        public DataTable ListarPrestamos(string filtroFolio, string filtroDocumento, string filtroAcreedor)
        {
            const string sql = @"
                SELECT p.Folio, p.Consecutivo, p.ClaveDocumento AS Documento, prov.RazonSocial AS Acreedor,
                       p.Fecha, p.Estatus
                FROM dbo.Prestamo p
                LEFT JOIN dbo.Proveedor prov ON prov.IdProveedor = p.ClaveAcreedor
                WHERE (@FiltroFolio IS NULL OR CAST(p.Consecutivo AS VARCHAR(20)) LIKE '%' + @FiltroFolio + '%')
                  AND (@FiltroDocumento IS NULL OR p.ClaveDocumento LIKE '%' + @FiltroDocumento + '%')
                  AND (@FiltroAcreedor IS NULL OR prov.RazonSocial LIKE '%' + @FiltroAcreedor + '%')
                ORDER BY p.Folio DESC;";

            var tabla = new DataTable();
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@FiltroFolio", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(filtroFolio) ? (object)DBNull.Value : filtroFolio;
                cmd.Parameters.Add("@FiltroDocumento", SqlDbType.VarChar, 40).Value = string.IsNullOrEmpty(filtroDocumento) ? (object)DBNull.Value : filtroDocumento;
                cmd.Parameters.Add("@FiltroAcreedor", SqlDbType.VarChar, 150).Value = string.IsNullOrEmpty(filtroAcreedor) ? (object)DBNull.Value : filtroAcreedor;

                cn.Open();
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(tabla);
            }
            return tabla;
        }

        private PrestamoData MapearPrestamo(IDataRecord r)
        {
            return new PrestamoData
            {
                Folio = Convert.ToInt32(r["Folio"]),
                Consecutivo = r["Consecutivo"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["Consecutivo"]),
                ClaveDocumento = r["ClaveDocumento"] as string,
                Estatus = r["Estatus"] as string,
                Fecha = Convert.ToDateTime(r["Fecha"]),
                ClaveAcreedor = r["ClaveAcreedor"] == DBNull.Value ? 0 : Convert.ToInt32(r["ClaveAcreedor"]),
                NombreAcreedor = r["NombreAcreedor"] as string,
                IdConceptoCapital = r["IdConceptoCapital"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["IdConceptoCapital"]),
                DescripcionConceptoCapital = r["DescripcionConceptoCapital"] as string,
                Referencia = r["Referencia"] as string,
                Parcialidades = Convert.ToInt32(r["Parcialidades"]),
                FechaPrimerPago = Convert.ToDateTime(r["FechaPrimerPago"]),
                Periodicidad = Convert.ToInt32(r["Periodicidad"]),
                ImporteCPago = Convert.ToDecimal(r["ImporteCPago"]),
                InteresCPago = Convert.ToDecimal(r["InteresCPago"]),
                IdConceptoInteres = r["IdConceptoInteres"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["IdConceptoInteres"]),
                DescripcionConceptoInteres = r["DescripcionConceptoInteres"] as string,
                AplicaImpuesto = Convert.ToBoolean(r["AplicaImpuesto"]),
                Divisa = r["Divisa"] as string,
                TipoCambio = Convert.ToDecimal(r["TipoCambio"]),
                Subtotal = Convert.ToDecimal(r["Subtotal"]),
                Intereses = Convert.ToDecimal(r["Intereses"]),
                Impuestos = Convert.ToDecimal(r["Impuestos"]),
                Total = Convert.ToDecimal(r["Total"]),
                Saldo = Convert.ToDecimal(r["Saldo"]),
                TotalPartidas = r["TotalPartidas"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalPartidas"]),
                Notas = r["Notas"] as string,
                Elaborado = r["Elaborado"] as string,
                RutaDocumento = r["RutaDocumento"] as string,
                DiasVence = r["DiasVence"] == DBNull.Value ? 0 : Convert.ToInt32(r["DiasVence"]),
                FechaVence = r["FechaVence"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["FechaVence"])
            };
        }

        #endregion

        #region Calendario de pagos / Partidas

        public bool ExistenPartidas(int folio)
        {
            const string sql = "SELECT COUNT(1) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio;";
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        /// <summary>
        /// Genera el calendario de pagos completo de un préstamo: crea
        /// exactamente [parcialidades] renglones en PartidaPrestamo,
        /// idénticos entre sí salvo:
        ///   - Vencimiento (calculado por el sistema: fechaPrimerPago +
        ///     periodicidadDias * (n-1))
        ///   - Partida (generado por el sistema: 1..parcialidades)
        /// Si el préstamo ya tenía un calendario generado, se reemplaza por
        /// completo (se borran las partidas previas) dentro de la misma
        /// transacción. Al final recalcula los totales del encabezado.
        /// </summary>
        public void GenerarCalendarioPagos(int folio, int parcialidades, DateTime fechaPrimerPago, int periodicidadDias,
            decimal importeCapital, int? idConceptoCapital, decimal interesParcialidad, decimal iva, int? idConceptoInteres)
        {
            if (parcialidades <= 0)
                throw new ArgumentException("El número de parcialidades debe ser mayor a cero.", nameof(parcialidades));

            decimal totalPorPartida = importeCapital + interesParcialidad + iva;

            using (var cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (var cmdDelete = new SqlCommand("DELETE FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio;", cn, tx))
                        {
                            cmdDelete.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                            cmdDelete.ExecuteNonQuery();
                        }

                        const string sqlInsert = @"
                            INSERT INTO dbo.PartidaPrestamo
                                (FolioPrestamo, Partida, Vencimiento, ImporteCapital, IdConceptoCapital,
                                 InteresParcialidad, Iva, IdConceptoInteres, Total, Estatus)
                            VALUES
                                (@FolioPrestamo, @Partida, @Vencimiento, @ImporteCapital, @IdConceptoCapital,
                                 @InteresParcialidad, @Iva, @IdConceptoInteres, @Total, 'Pendiente');";

                        for (int n = 1; n <= parcialidades; n++)
                        {
                            using (var cmdInsert = new SqlCommand(sqlInsert, cn, tx))
                            {
                                cmdInsert.Parameters.Add("@FolioPrestamo", SqlDbType.Int).Value = folio;
                                cmdInsert.Parameters.Add("@Partida", SqlDbType.Int).Value = n;
                                cmdInsert.Parameters.Add("@Vencimiento", SqlDbType.Date).Value = fechaPrimerPago.AddDays(periodicidadDias * (n - 1));
                                cmdInsert.Parameters.Add("@ImporteCapital", SqlDbType.Decimal).Value = importeCapital;
                                cmdInsert.Parameters.Add("@IdConceptoCapital", SqlDbType.Int).Value = (object)idConceptoCapital ?? DBNull.Value;
                                cmdInsert.Parameters.Add("@InteresParcialidad", SqlDbType.Decimal).Value = interesParcialidad;
                                cmdInsert.Parameters.Add("@Iva", SqlDbType.Decimal).Value = iva;
                                cmdInsert.Parameters.Add("@IdConceptoInteres", SqlDbType.Int).Value = (object)idConceptoInteres ?? DBNull.Value;
                                cmdInsert.Parameters.Add("@Total", SqlDbType.Decimal).Value = totalPorPartida;
                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        ActualizarTotalesEncabezado(folio, cn, tx);

                        tx.Commit();
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
        /// Elimina por completo el calendario de pagos de un préstamo
        /// (todas sus partidas) y regresa los totales del encabezado a
        /// cero. Pensado para permitir "empezar de nuevo" mientras el
        /// préstamo siga Abierto.
        /// </summary>
        public void EliminarCalendarioPagos(int folio)
        {
            using (var cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio;", cn, tx))
                        {
                            cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                            cmd.ExecuteNonQuery();
                        }

                        ActualizarTotalesEncabezado(folio, cn, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ListarPartidas(int folio)
        {
            const string sql = @"
                SELECT Partida, Vencimiento, ImporteCapital, InteresParcialidad, Iva, Total, Estatus
                FROM dbo.PartidaPrestamo
                WHERE FolioPrestamo = @Folio
                ORDER BY Partida;";

            var tabla = new DataTable();
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(tabla);
            }
            return tabla;
        }

        /// <summary>
        /// Trae todas las partidas ya materializadas como lista en memoria,
        /// para que Prestamos.cs pueda navegar Anterior/Siguiente sin ir a
        /// la base de datos en cada clic.
        /// </summary>
        public List<PartidaPrestamoData> ObtenerPartidas(int folio)
        {
            const string sql = @"
                SELECT pp.Partida, pp.Vencimiento, pp.ImporteCapital, pp.IdConceptoCapital,
                       cc1.Descripcion AS DescripcionConceptoCapital,
                       pp.InteresParcialidad, pp.Iva, pp.IdConceptoInteres,
                       cc2.Descripcion AS DescripcionConceptoInteres,
                       pp.Total, pp.Estatus,
                       (SELECT COUNT(1) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio) AS TotalParcialidades
                FROM dbo.PartidaPrestamo pp
                LEFT JOIN dbo.ConceptoCobroPago cc1 ON cc1.IdConcepto = pp.IdConceptoCapital
                LEFT JOIN dbo.ConceptoCobroPago cc2 ON cc2.IdConcepto = pp.IdConceptoInteres
                WHERE pp.FolioPrestamo = @Folio
                ORDER BY pp.Partida;";

            var lista = new List<PartidaPrestamoData>();
            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PartidaPrestamoData
                        {
                            FolioPrestamo = folio,
                            Partida = Convert.ToInt32(reader["Partida"]),
                            TotalParcialidades = Convert.ToInt32(reader["TotalParcialidades"]),
                            Vencimiento = Convert.ToDateTime(reader["Vencimiento"]),
                            ImporteCapital = Convert.ToDecimal(reader["ImporteCapital"]),
                            IdConceptoCapital = reader["IdConceptoCapital"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["IdConceptoCapital"]),
                            DescripcionConceptoCapital = reader["DescripcionConceptoCapital"] as string,
                            InteresParcialidad = Convert.ToDecimal(reader["InteresParcialidad"]),
                            Iva = Convert.ToDecimal(reader["Iva"]),
                            IdConceptoInteres = reader["IdConceptoInteres"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["IdConceptoInteres"]),
                            DescripcionConceptoInteres = reader["DescripcionConceptoInteres"] as string,
                            Total = Convert.ToDecimal(reader["Total"]),
                            Estatus = reader["Estatus"] as string
                        });
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Actualiza el Vencimiento de una partida ya generada. Es el ÚNICO
        /// campo modificable de una partida (regla de negocio). Valida que
        /// el nuevo Vencimiento no sea anterior a la Fecha del documento; si
        /// la validación falla devuelve el mensaje de error, si todo sale
        /// bien devuelve null.
        /// </summary>
        public string ActualizarVencimientoPartida(int folio, int partida, DateTime nuevoVencimiento)
        {
            using (var cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();

                DateTime fechaDocumento;
                using (var cmdFecha = new SqlCommand("SELECT Fecha FROM dbo.Prestamo WHERE Folio = @Folio;", cn))
                {
                    cmdFecha.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                    var valor = cmdFecha.ExecuteScalar();
                    if (valor == null || valor == DBNull.Value)
                        return "No se encontró el préstamo indicado.";
                    fechaDocumento = Convert.ToDateTime(valor);
                }

                if (nuevoVencimiento.Date < fechaDocumento.Date)
                    return $"El vencimiento no puede ser anterior a la fecha del documento ({fechaDocumento:yyyy-MM-dd}).";

                const string sqlUpdate = "UPDATE dbo.PartidaPrestamo SET Vencimiento = @Vencimiento WHERE FolioPrestamo = @Folio AND Partida = @Partida;";
                using (var cmdUpdate = new SqlCommand(sqlUpdate, cn))
                {
                    cmdUpdate.Parameters.Add("@Vencimiento", SqlDbType.Date).Value = nuevoVencimiento.Date;
                    cmdUpdate.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                    cmdUpdate.Parameters.Add("@Partida", SqlDbType.Int).Value = partida;
                    cmdUpdate.ExecuteNonQuery();
                }

                return null;
            }
        }

        /// <summary>
        /// Recalcula Subtotal/Intereses/Impuestos/Total/TotalPartidas del
        /// encabezado a partir de la suma real de sus partidas, y ajusta
        /// Saldo (por ahora Saldo = Total; el módulo de aplicación de pagos
        /// a futuro deberá restar aquí los abonos ya registrados).
        /// </summary>
        public void ActualizarTotalesEncabezado(int folio)
        {
            using (var cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                ActualizarTotalesEncabezado(folio, cn, null);
            }
        }

        private void ActualizarTotalesEncabezado(int folio, SqlConnection cn, SqlTransaction tx)
        {
            const string sql = @"
                UPDATE dbo.Prestamo
                SET Subtotal     = ISNULL((SELECT SUM(ImporteCapital) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio), 0),
                    Intereses    = ISNULL((SELECT SUM(InteresParcialidad) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio), 0),
                    Impuestos    = ISNULL((SELECT SUM(Iva) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio), 0),
                    Total        = ISNULL((SELECT SUM(Total) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio), 0),
                    Saldo        = ISNULL((SELECT SUM(Total) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio AND Estatus <> 'Pagado'), 0),
                    TotalPartidas = ISNULL((SELECT COUNT(*) FROM dbo.PartidaPrestamo WHERE FolioPrestamo = @Folio), 0)
                WHERE Folio = @Folio;";

            using (var cmd = tx == null ? new SqlCommand(sql, cn) : new SqlCommand(sql, cn, tx))
            {
                cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = folio;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}