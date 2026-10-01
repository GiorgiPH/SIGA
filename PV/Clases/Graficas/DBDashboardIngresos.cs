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
    public sealed class DBDashboardIngresos : IDashboardIngresos
    {
        private readonly string cadenaConexion;

        public DBDashboardIngresos()
            : this(Settings.Default.ControlCondominiosConnectionString)
        {
        }

        public DBDashboardIngresos(string cadenaConexion)
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
    MIN(Anios.Anio) AS PrimerAnio,
    MAX(Anios.Anio) AS UltimoAnio
FROM
(
    SELECT YEAR(C.Fecha) AS Anio
    FROM Cobros C
    WHERE C.Fecha IS NOT NULL

    UNION

    SELECT YEAR(F.Fecha) AS Anio
    FROM Factura F
    WHERE F.Fecha IS NOT NULL
) Anios;";

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
        // RESUMEN
        // =========================================================

        public async Task<ResumenDashboardIngresos>
            ObtenerResumenAnualAsync(
                int anio,
                CancellationToken cancellationToken)
        {
            if (anio < 1 || anio > 9998)
            {
                throw new ArgumentOutOfRangeException("anio");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<IngresoMensual> meses =
                CrearListaMeses();

            List<CarteraCliente> clientes =
                new List<CarteraCliente>();

            List<IngresoCuenta> cuentas =
                new List<IngresoCuenta>();

            List<AntiguedadCartera> antiguedad =
                CrearListaAntiguedad();

            decimal totalIngresos = 0M;
            decimal promedioMensual = 0M;
            decimal totalCartera = 0M;

            int mesCorte = 1;

            using (SqlConnection cn = CrearConexion())
            {
                await AbrirConexionAsync(
                    cn,
                    cancellationToken).ConfigureAwait(false);

                mesCorte =
                    await ObtenerMesCorteAsync(
                        cn,
                        anio,
                        cancellationToken).ConfigureAwait(false);

                await CargarKpisAsync(
                    cn,
                    anio,
                    mesCorte,
                    cancellationToken,
                    resultado =>
                    {
                        totalIngresos = resultado.Item1;
                        promedioMensual = resultado.Item2;
                    }).ConfigureAwait(false);

                await CargarMesesAsync(
                    cn,
                    anio,
                    meses,
                    cancellationToken).ConfigureAwait(false);

                clientes =
                    await CargarCarteraClientesAsync(
                        cn,
                        anio,
                        mesCorte,
                        cancellationToken).ConfigureAwait(false);

                totalCartera =
                    clientes.Sum(x => x.Saldo);

                cuentas =
                    await CargarCuentasAsync(
                        cn,
                        anio,
                        mesCorte,
                        cancellationToken).ConfigureAwait(false);

                await CargarAntiguedadAsync(
                    cn,
                    anio,
                    mesCorte,
                    antiguedad,
                    cancellationToken).ConfigureAwait(false);
            }

            cancellationToken.ThrowIfCancellationRequested();

            return new ResumenDashboardIngresos(
                anio,
                mesCorte,
                totalIngresos,
                promedioMensual,
                totalCartera,
                meses,
                clientes,
                cuentas,
                antiguedad);
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
SELECT MAX(Mes)
FROM
(
    SELECT MAX(MONTH(C.Fecha)) AS Mes
    FROM Cobros C
    WHERE YEAR(C.Fecha) = @Anio

    UNION ALL

    SELECT MAX(MONTH(F.Fecha)) AS Mes
    FROM Factura F
    WHERE YEAR(F.Fecha) = @Anio
) X;";

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

                if (resultado == null ||
                    resultado == DBNull.Value)
                {
                    return 1;
                }

                int mes =
                    Convert.ToInt32(resultado);

                return Math.Max(
                    1,
                    Math.Min(12, mes));
            }
        }

        // =========================================================
        // KPI INGRESOS
        // =========================================================

        private static async Task CargarKpisAsync(
            SqlConnection cn,
            int anio,
            int mesCorte,
            CancellationToken cancellationToken,
            Action<Tuple<decimal, decimal>> asignarResultado)
        {
            const string sql = @"
SELECT
    ISNULL(SUM(C.Pago), 0) AS TotalIngresos,
    ISNULL(SUM(C.Pago), 0) / NULLIF(@Mes, 0) AS PromedioMensual
FROM Cobros C
WHERE
    YEAR(C.Fecha) = @Anio
    AND MONTH(C.Fecha) <= @Mes;";

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
        // INGRESOS MENSUALES
        // =========================================================

        private static async Task CargarMesesAsync(
            SqlConnection cn,
            int anio,
            List<IngresoMensual> meses,
            CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    MONTH(C.Fecha) AS Mes,
    ISNULL(SUM(C.Pago), 0) AS TotalIngresos
FROM Cobros C
WHERE
    YEAR(C.Fecha) = @Anio
GROUP BY
    MONTH(C.Fecha)
ORDER BY
    MONTH(C.Fecha);";

            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters
                    .Add("@Anio", SqlDbType.Int)
                    .Value = anio;

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
                            meses[mes - 1].TotalIngresos =
                                total;
                        }
                    }
                }
            }
        }

        // =========================================================
        // CARTERA DE CLIENTES
        // =========================================================

        private static async Task<List<CarteraCliente>>
            CargarCarteraClientesAsync(
                SqlConnection cn,
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    C.IdCliente,
    ISNULL(C.RazonSocial, 'Cliente sin nombre') AS Cliente,
    COUNT(*) AS NumeroDocumentos,
    ISNULL(SUM(F.Saldo), 0) AS Saldo
FROM Factura F
INNER JOIN Clientes C
    ON C.IdCliente = F.ClaveProveedor
WHERE
    YEAR(F.Fecha) = @Anio
    AND MONTH(F.Fecha) <= @Mes
    AND F.Saldo > 0
GROUP BY
    C.IdCliente,
    C.RazonSocial
ORDER BY
    Saldo DESC;";

            List<CarteraCliente> resultado =
                new List<CarteraCliente>();

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
                        CarteraCliente item =
                            new CarteraCliente
                            {
                                IdCliente =
                                    Convert.ToInt32(
                                        reader.GetValue(0)),

                                Cliente =
                                    Convert.ToString(
                                        reader.GetValue(1)),

                                NumeroDocumentos =
                                    Convert.ToInt32(
                                        reader.GetValue(2)),

                                Saldo =
                                    reader.IsDBNull(3)
                                        ? 0M
                                        : Convert.ToDecimal(
                                            reader.GetValue(3))
                            };

                        resultado.Add(item);
                    }
                }
            }

            return resultado;
        }

        // =========================================================
        // INGRESOS POR CUENTA BANCARIA
        // =========================================================

        private static async Task<List<IngresoCuenta>>
            CargarCuentasAsync(
                SqlConnection cn,
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT
    CB.Clave,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(CB.Nombre)), '') IS NOT NULL
             AND NULLIF(LTRIM(RTRIM(CB.Cuenta)), '') IS NOT NULL
            THEN CB.Nombre + ' - ' + CB.Cuenta
        WHEN NULLIF(LTRIM(RTRIM(CB.Nombre)), '') IS NOT NULL
            THEN CB.Nombre
        WHEN NULLIF(LTRIM(RTRIM(CB.Cuenta)), '') IS NOT NULL
            THEN CB.Cuenta
        ELSE 'Cuenta ' + CONVERT(VARCHAR(20), CB.Clave)
    END AS Cuenta,
    ISNULL(SUM(C.Pago), 0) AS TotalIngresos
FROM CuentasBancarias CB
LEFT JOIN Cobros C
    ON C.CuentaBancaria = CB.Clave
    AND YEAR(C.Fecha) = @Anio
    AND MONTH(C.Fecha) <= @Mes
WHERE
    CB.Estatus = 'Activo'
GROUP BY
    CB.Clave,
    CB.Nombre,
    CB.Cuenta
ORDER BY
    TotalIngresos DESC,
    CB.Nombre;";

            List<IngresoCuenta> resultado =
                new List<IngresoCuenta>();

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
                        IngresoCuenta item =
                            new IngresoCuenta
                            {
                                ClaveCuentaBancaria =
                                    Convert.ToInt32(
                                        reader.GetValue(0)),

                                Cuenta =
                                    reader.IsDBNull(1)
                                        ? "Cuenta sin nombre"
                                        : Convert.ToString(
                                            reader.GetValue(1)),

                                TotalIngresos =
                                    reader.IsDBNull(2)
                                        ? 0M
                                        : Convert.ToDecimal(
                                            reader.GetValue(2))
                            };

                        resultado.Add(item);
                    }
                }
            }

            return resultado;
        }

        // =========================================================
        // ANTIGÜEDAD DE CARTERA
        // =========================================================

        private static async Task CargarAntiguedadAsync(
            SqlConnection cn,
            int anio,
            int mesCorte,
            List<AntiguedadCartera> antiguedad,
            CancellationToken cancellationToken)
        {
            DateTime fechaCorte =
                new DateTime(
                    anio,
                    mesCorte,
                    DateTime.DaysInMonth(
                        anio,
                        mesCorte));

            const string sql = @"
SELECT
    CASE
        WHEN ISNULL(F.FechaVence, F.Fecha) > @FechaCorte
            THEN 0

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 0 AND 30
            THEN 1

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 31 AND 60
            THEN 2

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 61 AND 90
            THEN 3

        ELSE 4
    END AS Rango,

    COUNT(*) AS NumeroDocumentos,
    ISNULL(SUM(F.Saldo), 0) AS Saldo

FROM Factura F

WHERE
    YEAR(F.Fecha) = @Anio
    AND MONTH(F.Fecha) <= @Mes
    AND F.Saldo > 0

GROUP BY
    CASE
        WHEN ISNULL(F.FechaVence, F.Fecha) > @FechaCorte
            THEN 0

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 0 AND 30
            THEN 1

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 31 AND 60
            THEN 2

        WHEN DATEDIFF(
            DAY,
            ISNULL(F.FechaVence, F.Fecha),
            @FechaCorte
        ) BETWEEN 61 AND 90
            THEN 3

        ELSE 4
    END

ORDER BY Rango;";

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

                cmd.Parameters
                    .Add("@FechaCorte", SqlDbType.Date)
                    .Value = fechaCorte;

                using (SqlDataReader reader =
                    await cmd.ExecuteReaderAsync(
                        cancellationToken).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(
                        cancellationToken).ConfigureAwait(false))
                    {
                        int rango =
                            Convert.ToInt32(
                                reader.GetValue(0));

                        if (rango < 0 ||
                            rango >= antiguedad.Count)
                        {
                            continue;
                        }

                        antiguedad[rango].NumeroDocumentos =
                            Convert.ToInt32(
                                reader.GetValue(1));

                        antiguedad[rango].Saldo =
                            reader.IsDBNull(2)
                                ? 0M
                                : Convert.ToDecimal(
                                    reader.GetValue(2));
                    }
                }
            }
        }

        // =========================================================
        // LISTAS
        // =========================================================

        private static List<IngresoMensual>
            CrearListaMeses()
        {
            CultureInfo cultura =
                CultureInfo.GetCultureInfo(
                    "es-MX");

            return Enumerable
                .Range(1, 12)
                .Select(m =>
                    new IngresoMensual
                    {
                        Mes = m,

                        NombreMes =
                            cultura.TextInfo.ToTitleCase(
                                cultura.DateTimeFormat
                                    .GetMonthName(m)),

                        TotalIngresos = 0M
                    })
                .ToList();
        }

        private static List<AntiguedadCartera>
            CrearListaAntiguedad()
        {
            return new List<AntiguedadCartera>
            {
                new AntiguedadCartera
                {
                    Rango = "Por Vencer"
                },

                new AntiguedadCartera
                {
                    Rango = "1 - 30 días"
                },

                new AntiguedadCartera
                {
                    Rango = "31 - 60 días"
                },

                new AntiguedadCartera
                {
                    Rango = "61 - 90 días"
                },

                new AntiguedadCartera
                {
                    Rango = "91 o más días"
                }
            };
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