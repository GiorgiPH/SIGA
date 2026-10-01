using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PV.dto;
using PV.Properties;

namespace PV.Clases.Graficas
{
    public sealed class DBDashboardCompras : IDashboardCompras
    {
        private readonly string cadenaConexion;

        public DBDashboardCompras()
            : this(Settings.Default.ControlCondominiosConnectionString)
        {
        }

        public DBDashboardCompras(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión " +
                    "'ControlCondominiosConnectionString'.");
            }

            this.cadenaConexion = cadenaConexion;
        }

        // =========================================================
        // AÑOS DISPONIBLES
        // =========================================================

        public async Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = @"
SELECT
    MIN(YEAR(RG.Fecha)) AS PrimerAnio,
    MAX(YEAR(RG.Fecha)) AS UltimoAnio
FROM RegistroGastos RG
WHERE
    RG.Fecha IS NOT NULL
    AND RG.Estatus = 'Bloqueado';";

            int anioActual = DateTime.Today.Year;

            int primerAnio = anioActual;
            int ultimoAnio = anioActual;

            using (SqlConnection cn = CrearConexion())
            {
                await AbrirConexionAsync(
                    cn,
                    cancellationToken).ConfigureAwait(false);

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 30;

                    using (SqlDataReader reader =
                        await cmd.ExecuteReaderAsync(
                            cancellationToken).ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync(
                            cancellationToken).ConfigureAwait(false))
                        {
                            if (!reader.IsDBNull(0))
                            {
                                primerAnio =
                                    Convert.ToInt32(
                                        reader.GetValue(0));
                            }

                            if (!reader.IsDBNull(1))
                            {
                                ultimoAnio =
                                    Convert.ToInt32(
                                        reader.GetValue(1));
                            }
                        }
                    }
                }
            }

            if (anioActual < primerAnio)
            {
                primerAnio = anioActual;
            }

            if (anioActual > ultimoAnio)
            {
                ultimoAnio = anioActual;
            }

            primerAnio = Math.Max(1, primerAnio);
            ultimoAnio = Math.Min(9998, ultimoAnio);

            if (primerAnio > ultimoAnio)
            {
                primerAnio = ultimoAnio;
            }

            List<int> resultado =
                Enumerable.Range(
                    primerAnio,
                    ultimoAnio - primerAnio + 1)
                .Reverse()
                .ToList();

            return resultado.AsReadOnly();
        }

        // =========================================================
        // RESUMEN ANUAL
        // =========================================================

        public async Task<ResumenDashboardCompras>
            ObtenerResumenAnualAsync(
                int anio,
                CancellationToken cancellationToken)
        {
            if (anio < 1 || anio > 9998)
            {
                throw new ArgumentOutOfRangeException("anio");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<CompraMensual> meses =
                CrearListaMeses();

            decimal totalCompras = 0M;
            decimal promedioMensual = 0M;

            List<CompraProveedor> proveedores =
                new List<CompraProveedor>();

            List<CompraClasificacion> clasificaciones =
                new List<CompraClasificacion>();

            ComparativoCompraAnual comparativo =
                new ComparativoCompraAnual
                {
                    AnioAnterior = anio - 1,
                    TotalAnioAnterior = 0M,
                    AnioActual = anio,
                    TotalAnioActual = 0M
                };

            int mesCorte;

            using (SqlConnection cn = CrearConexion())
            {
                await AbrirConexionAsync(
                    cn,
                    cancellationToken).ConfigureAwait(false);

                // -------------------------------------------------
                // MES DE CORTE
                // -------------------------------------------------

                mesCorte =
                    await ObtenerMesCorteAsync(
                        cn,
                        anio,
                        cancellationToken).ConfigureAwait(false);

                // -------------------------------------------------
                // KPI
                // -------------------------------------------------

                await CargarKpisAsync(
                    cn,
                    anio,
                    mesCorte,
                    cancellationToken,
                    resultado =>
                    {
                        totalCompras =
                            resultado.Item1;

                        promedioMensual =
                            resultado.Item2;
                    }).ConfigureAwait(false);

                // -------------------------------------------------
                // COMPRAS MENSUALES
                // -------------------------------------------------

                await CargarMesesAsync(
                    cn,
                    anio,
                    mesCorte,
                    meses,
                    cancellationToken).ConfigureAwait(false);

                // -------------------------------------------------
                // PRINCIPALES PROVEEDORES
                // -------------------------------------------------

                proveedores =
                    await CargarProveedoresAsync(
                        cn,
                        anio,
                        mesCorte,
                        cancellationToken).ConfigureAwait(false);

                // -------------------------------------------------
                // CLASIFICACIÓN DE COMPRAS
                // -------------------------------------------------

                clasificaciones =
                    await CargarClasificacionesAsync(
                        cn,
                        anio,
                        mesCorte,
                        cancellationToken).ConfigureAwait(false);

                // -------------------------------------------------
                // COMPARATIVO ANUAL
                // -------------------------------------------------

                comparativo =
                    await CargarComparativoAsync(
                        cn,
                        anio,
                        mesCorte,
                        cancellationToken).ConfigureAwait(false);
            }

            cancellationToken.ThrowIfCancellationRequested();

            return new ResumenDashboardCompras(
                anio,
                mesCorte,
                totalCompras,
                promedioMensual,
                meses,
                proveedores,
                clasificaciones,
                comparativo);
        }

        // =========================================================
        // KPI
        // =========================================================

        private async Task CargarKpisAsync(
            SqlConnection cn,
            int anio,
            int mesCorte,
            CancellationToken cancellationToken,
            Action<Tuple<decimal, decimal>> asignarResultado)
        {
            const string sql = @"
SELECT
    ISNULL(SUM(RG.Total), 0) AS TotalCompras,
    ISNULL(SUM(RG.Total), 0) / NULLIF(@Mes, 0) AS PromedioMensual
FROM RegistroGastos RG
WHERE
    YEAR(RG.Fecha) = @Anio
    AND MONTH(RG.Fecha) <= @Mes
    AND RG.Estatus = 'Bloqueado';";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                cmd.Parameters
                    .Add("@Mes", SqlDbType.Int)
                    .Value = mesCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    decimal total = 0M;
                    decimal promedio = 0M;

                    if (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        if (!reader.IsDBNull(0))
                        {
                            total =
                                Convert.ToDecimal(
                                    reader.GetValue(0));
                        }

                        if (!reader.IsDBNull(1))
                        {
                            promedio =
                                Convert.ToDecimal(
                                    reader.GetValue(1));
                        }
                    }

                    asignarResultado(
                        Tuple.Create(
                            total,
                            promedio));
                }
            }
        }

        // =========================================================
        // COMPRAS MENSUALES
        // =========================================================

        private async Task CargarMesesAsync(
            SqlConnection cn,
            int anio,
            int mesCorte,
            List<CompraMensual> meses,
            CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    MONTH(RG.Fecha) AS Mes,
    ISNULL(SUM(RG.Total), 0) AS TotalCompras
FROM RegistroGastos RG
WHERE
    YEAR(RG.Fecha) = @Anio
    AND MONTH(RG.Fecha) <= @Mes
    AND RG.Estatus = 'Bloqueado'
GROUP BY
    MONTH(RG.Fecha)
ORDER BY
    MONTH(RG.Fecha);";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                cmd.Parameters
                    .Add("@Mes", SqlDbType.Int)
                    .Value = mesCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        cancellationToken
                            .ThrowIfCancellationRequested();

                        int mes =
                            Convert.ToInt32(
                                reader.GetValue(0));

                        decimal total =
                            reader.IsDBNull(1)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(1));

                        if (mes >= 1 && mes <= 12)
                        {
                            meses[mes - 1].TotalCompras =
                                total;
                        }
                    }
                }
            }
        }

        // =========================================================
        // PROVEEDORES
        // =========================================================

        private async Task<List<CompraProveedor>>
            CargarProveedoresAsync(
                SqlConnection cn,
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT TOP 5
    P.IdProveedor,
    P.RazonSocial AS Proveedor,
    COUNT(*) AS NumeroCompras,
    ISNULL(SUM(RG.Total), 0) AS TotalCompras
FROM RegistroGastos RG
INNER JOIN Proveedor P
    ON P.IdProveedor = RG.ClaveProveedor
WHERE
    YEAR(RG.Fecha) = @Anio
    AND MONTH(RG.Fecha) <= @Mes
    AND RG.Estatus = 'Bloqueado'
GROUP BY
    P.IdProveedor,
    P.RazonSocial
ORDER BY
    TotalCompras DESC;";

            List<CompraProveedor> resultado =
                new List<CompraProveedor>();

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                cmd.Parameters
                    .Add("@Mes", SqlDbType.Int)
                    .Value = mesCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        CompraProveedor proveedor =
                            new CompraProveedor();

                        proveedor.IdProveedor =
                            Convert.ToInt32(
                                reader.GetValue(0));

                        proveedor.Proveedor =
                            reader.IsDBNull(1)
                                ? "Proveedor sin nombre"
                                : Convert.ToString(
                                    reader.GetValue(1));

                        proveedor.NumeroCompras =
                            Convert.ToInt32(
                                reader.GetValue(2));

                        proveedor.TotalCompras =
                            reader.IsDBNull(3)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(3));

                        resultado.Add(
                            proveedor);
                    }
                }
            }

            return resultado;
        }

        // =========================================================
        // CLASIFICACIÓN DE COMPRAS
        // =========================================================

        private async Task<List<CompraClasificacion>>
            CargarClasificacionesAsync(
                SqlConnection cn,
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    C.Nombre AS Categoria,
    F.Nombre AS Familia,
    ISNULL(SUM(PG.Total), 0) AS Total
FROM RegistroGastos RG
INNER JOIN PartidaRegistroGastos PG
    ON PG.FolioGasto = RG.Folio
INNER JOIN Servicios S
    ON S.ClaveServicio = PG.ClaveProducto
INNER JOIN Categorias C
    ON C.ClaveCategoria = S.Categoria
INNER JOIN Familias F
    ON F.ClaveFamilia = S.Familia
    AND F.ClaveCategoria = C.ClaveCategoria
WHERE
    RG.Estatus = 'Bloqueado'
    AND YEAR(RG.Fecha) = @Anio
    AND MONTH(RG.Fecha) <= @Mes
GROUP BY
    C.Nombre,
    F.Nombre
ORDER BY
    SUM(ISNULL(PG.Total, 0)) DESC;";

            List<CompraClasificacion> resultado =
                new List<CompraClasificacion>();

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                cmd.Parameters
                    .Add("@Mes", SqlDbType.Int)
                    .Value = mesCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        cancellationToken
                            .ThrowIfCancellationRequested();

                        CompraClasificacion clasificacion =
                            new CompraClasificacion();

                        clasificacion.Categoria =
                            reader.IsDBNull(0)
                                ? string.Empty
                                : Convert.ToString(
                                    reader.GetValue(0));

                        clasificacion.Familia =
                            reader.IsDBNull(1)
                                ? string.Empty
                                : Convert.ToString(
                                    reader.GetValue(1));

                        clasificacion.Total =
                            reader.IsDBNull(2)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(2));

                        resultado.Add(
                            clasificacion);
                    }
                }
            }

            return resultado;
        }

        // =========================================================
        // COMPARATIVO ANUAL
        // =========================================================

        private async Task<ComparativoCompraAnual>
            CargarComparativoAsync(
                SqlConnection cn,
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    @Anio - 1 AS AnioAnterior,

    ISNULL(
        SUM(
            CASE
                WHEN YEAR(RG.Fecha) = @Anio - 1
                     AND MONTH(RG.Fecha) <= @Mes
                THEN RG.Total
                ELSE 0
            END),
        0
    ) AS TotalAnioAnterior,

    @Anio AS AnioActual,

    ISNULL(
        SUM(
            CASE
                WHEN YEAR(RG.Fecha) = @Anio
                     AND MONTH(RG.Fecha) <= @Mes
                THEN RG.Total
                ELSE 0
            END),
        0
    ) AS TotalAnioActual

FROM RegistroGastos RG
WHERE
    RG.Estatus = 'Bloqueado'
    AND YEAR(RG.Fecha) IN (@Anio - 1, @Anio);";

            ComparativoCompraAnual resultado =
                new ComparativoCompraAnual
                {
                    AnioAnterior = anio - 1,
                    TotalAnioAnterior = 0M,
                    AnioActual = anio,
                    TotalAnioActual = 0M
                };

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                cmd.Parameters
                    .Add("@Mes", SqlDbType.Int)
                    .Value = mesCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    if (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        resultado.AnioAnterior =
                            Convert.ToInt32(
                                reader.GetValue(0));

                        resultado.TotalAnioAnterior =
                            reader.IsDBNull(1)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(1));

                        resultado.AnioActual =
                            Convert.ToInt32(
                                reader.GetValue(2));

                        resultado.TotalAnioActual =
                            reader.IsDBNull(3)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(3));
                    }
                }
            }

            return resultado;
        }

        // =========================================================
        // CREAR LOS 12 MESES
        // =========================================================

        private static List<CompraMensual> CrearListaMeses()
        {
            CultureInfo cultura =
                CultureInfo.GetCultureInfo(
                    "es-MX");

            return Enumerable
                .Range(1, 12)
                .Select(m =>
                    new CompraMensual
                    {
                        Mes = m,

                        NombreMes =
                            cultura.TextInfo.ToTitleCase(
                                cultura.DateTimeFormat
                                    .GetMonthName(m)),

                        TotalCompras = 0M
                    })
                .ToList();
        }

        // =========================================================
        // MES DE CORTE
        // =========================================================

        private static async Task<int> ObtenerMesCorteAsync(
            SqlConnection cn,
            int anio,
            CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    MAX(MONTH(RG.Fecha))
FROM RegistroGastos RG
WHERE
    RG.Fecha IS NOT NULL
    AND YEAR(RG.Fecha) = @Anio
    AND RG.Estatus = 'Bloqueado';";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 30;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

                object resultado =
                    await cmd.ExecuteScalarAsync(
                        cancellationToken).ConfigureAwait(false);

                if (resultado != null &&
                    resultado != DBNull.Value)
                {
                    int mes =
                        Convert.ToInt32(resultado);

                    if (mes >= 1 && mes <= 12)
                    {
                        return mes;
                    }
                }
            }

            /*
             * Si el año no tiene compras, usamos diciembre como
             * corte para mantener un valor válido y evitar divisiones
             * entre cero.
             */
            return 12;
        }

        // =========================================================
        // CONEXIÓN
        // =========================================================

        private SqlConnection CrearConexion()
        {
            return new SqlConnection(
                cadenaConexion);
        }

        private static async Task AbrirConexionAsync(
            SqlConnection conexion,
            CancellationToken cancellationToken)
        {
            try
            {
                await conexion
                    .OpenAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    "No fue posible establecer conexión con SQL Server. " +
                    "Verifica el servidor, la base de datos y la cadena de conexión.",
                    ex);
            }
        }
    }
}