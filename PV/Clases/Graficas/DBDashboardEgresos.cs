using PV.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PV.Clases.Graficas
{
    public sealed class DBDashboardEgresos : IDashboardEgresos
    {
        private readonly string connectionString;

        public DBDashboardEgresos()
            : this(Properties.Settings.Default.ControlCondominiosConnectionString)
        {
        }

        public DBDashboardEgresos(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(
                    "La cadena de conexión no puede estar vacía.",
                    nameof(connectionString));
            }

            this.connectionString = connectionString;
        }

        // ============================================================
        // AÑOS DISPONIBLES
        // ============================================================

        public async Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken)
        {
            var anios = new HashSet<int>();

            const string sql = @"
SELECT DISTINCT Anio
FROM
(
    SELECT YEAR(Fecha) AS Anio
    FROM Egreso
    WHERE Fecha IS NOT NULL

    UNION

    SELECT YEAR(Fecha) AS Anio
    FROM RegistroGastos
    WHERE Fecha IS NOT NULL
      AND Estatus = 'Bloqueado'
) AS A
WHERE Anio IS NOT NULL
ORDER BY Anio DESC;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                using (var reader = await command
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false))
                {
                    while (await reader
                        .ReadAsync(cancellationToken)
                        .ConfigureAwait(false))
                    {
                        if (!reader.IsDBNull(0))
                        {
                            anios.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            // Siempre incluimos el año actual para que el Dashboard
            // pueda consultarse aunque todavía no tenga movimientos.
            anios.Add(DateTime.Today.Year);

            return anios
                .OrderByDescending(x => x)
                .ToList()
                .AsReadOnly();
        }

        // ============================================================
        // RESUMEN COMPLETO DEL DASHBOARD
        // ============================================================

        public async Task<ResumenDashboardEgresos> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken)
        {
            if (anio < 1900 || anio > 9999)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(anio),
                    "El año indicado no es válido.");
            }

            int mesCorte = await ObtenerMesCorteAsync(
                anio,
                cancellationToken).ConfigureAwait(false);

            decimal totalEgresos = await ObtenerTotalEgresosAsync(
                anio,
                mesCorte,
                cancellationToken).ConfigureAwait(false);

            decimal promedioMensual =
                mesCorte > 0
                    ? totalEgresos / mesCorte
                    : 0m;

            IReadOnlyList<EgresoMensual> meses =
                await ObtenerEgresosMensualesAsync(
                    anio,
                    cancellationToken).ConfigureAwait(false);

            IReadOnlyList<EgresoProveedor> proveedores =
                await ObtenerCarteraProveedoresAsync(
                    anio,
                    mesCorte,
                    cancellationToken).ConfigureAwait(false);

            decimal totalCartera = proveedores.Sum(x => x.Saldo);

            IReadOnlyList<EgresoCuentaBancaria> cuentas =
                await ObtenerEgresosCuentasAsync(
                    anio,
                    mesCorte,
                    cancellationToken).ConfigureAwait(false);

            IReadOnlyList<EgresoAntiguedad> antiguedad =
                await ObtenerAntiguedadAsync(
                    anio,
                    mesCorte,
                    cancellationToken).ConfigureAwait(false);

            return new ResumenDashboardEgresos(
                anio,
                mesCorte,
                totalEgresos,
                promedioMensual,
                totalCartera,
                meses,
                proveedores,
                cuentas,
                antiguedad);
        }

        // ============================================================
        // MES DE CORTE
        // ============================================================

        private async Task<int> ObtenerMesCorteAsync(
            int anio,
            CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT ISNULL(MAX(Mes), 0)
FROM
(
    SELECT MAX(MONTH(Fecha)) AS Mes
    FROM Egreso
    WHERE YEAR(Fecha) = @Anio

    UNION ALL

    SELECT MAX(MONTH(Fecha)) AS Mes
    FROM RegistroGastos
    WHERE YEAR(Fecha) = @Anio
      AND Estatus = 'Bloqueado'
) AS M;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                object result = await command
                    .ExecuteScalarAsync(cancellationToken)
                    .ConfigureAwait(false);

                int mes = ConvertirEntero(result);

                // Si estamos consultando el año actual pero todavía no
                // existen movimientos, usamos el mes actual como corte.
                if (mes <= 0 && anio == DateTime.Today.Year)
                {
                    mes = DateTime.Today.Month;
                }

                // Para años futuros sin información no existe mes de corte.
                if (mes <= 0 && anio > DateTime.Today.Year)
                {
                    return 0;
                }

                // Para un año pasado sin registros consideramos el año completo.
                if (mes <= 0 && anio < DateTime.Today.Year)
                {
                    return 12;
                }

                return mes;
            }
        }

        // ============================================================
        // TOTAL DE EGRESOS PAGADOS
        // ============================================================

        private async Task<decimal> ObtenerTotalEgresosAsync(
            int anio,
            int mesCorte,
            CancellationToken cancellationToken)
        {
            if (mesCorte <= 0)
            {
                return 0m;
            }

            const string sql = @"
SELECT ISNULL(SUM(E.Pago), 0)
FROM Egreso E
WHERE E.Fecha IS NOT NULL
  AND YEAR(E.Fecha) = @Anio
  AND MONTH(E.Fecha) <= @Mes;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                command.Parameters.Add(
                    "@Mes",
                    SqlDbType.Int).Value = mesCorte;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                object result = await command
                    .ExecuteScalarAsync(cancellationToken)
                    .ConfigureAwait(false);

                return ConvertirDecimal(result);
            }
        }

        // ============================================================
        // EGRESOS POR MES
        // ============================================================

        private async Task<IReadOnlyList<EgresoMensual>>
            ObtenerEgresosMensualesAsync(
                int anio,
                CancellationToken cancellationToken)
        {
            List<EgresoMensual> meses = CrearListaMeses();

            const string sql = @"
SELECT
    MONTH(E.Fecha) AS Mes,
    ISNULL(SUM(E.Pago), 0) AS TotalEgresos
FROM Egreso E
WHERE E.Fecha IS NOT NULL
  AND YEAR(E.Fecha) = @Anio
GROUP BY MONTH(E.Fecha)
ORDER BY MONTH(E.Fecha);";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                using (var reader = await command
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false))
                {
                    while (await reader
                        .ReadAsync(cancellationToken)
                        .ConfigureAwait(false))
                    {
                        int numeroMes = reader.GetInt32(0);
                        decimal total = ConvertirDecimal(reader.GetValue(1));

                        EgresoMensual mes =
                            meses.FirstOrDefault(x => x.Mes == numeroMes);

                        if (mes != null)
                        {
                            mes.TotalEgresos = total;
                        }
                    }
                }
            }

            return meses.AsReadOnly();
        }

        // ============================================================
        // CARTERA DE PROVEEDORES
        // ============================================================

        private async Task<IReadOnlyList<EgresoProveedor>>
            ObtenerCarteraProveedoresAsync(
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            var resultado = new List<EgresoProveedor>();

            if (mesCorte <= 0)
            {
                return resultado.AsReadOnly();
            }

            const string sql = @"
SELECT
    P.IdProveedor,
    ISNULL(P.RazonSocial, '') AS Proveedor,
    COUNT(*) AS NumeroDocumentos,
    ISNULL(SUM(RG.Saldo), 0) AS Saldo
FROM RegistroGastos RG
INNER JOIN Proveedor P
    ON P.IdProveedor = RG.ClaveProveedor
WHERE RG.Fecha IS NOT NULL
  AND YEAR(RG.Fecha) = @Anio
  AND MONTH(RG.Fecha) <= @Mes
  AND RG.Estatus = 'Bloqueado'
  AND ISNULL(RG.Saldo, 0) > 0
GROUP BY
    P.IdProveedor,
    P.RazonSocial
ORDER BY
    Saldo DESC,
    P.RazonSocial;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                command.Parameters.Add(
                    "@Mes",
                    SqlDbType.Int).Value = mesCorte;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                using (var reader = await command
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false))
                {
                    while (await reader
                        .ReadAsync(cancellationToken)
                        .ConfigureAwait(false))
                    {
                        resultado.Add(new EgresoProveedor
                        {
                            IdProveedor = reader.GetInt32(0),

                            Proveedor = reader.IsDBNull(1)
                                ? string.Empty
                                : reader.GetString(1),

                            NumeroDocumentos =
                                ConvertirEntero(reader.GetValue(2)),

                            Saldo =
                                ConvertirDecimal(reader.GetValue(3))
                        });
                    }
                }
            }

            return resultado.AsReadOnly();
        }

        // ============================================================
        // EGRESOS POR CUENTA BANCARIA
        // ============================================================

        private async Task<IReadOnlyList<EgresoCuentaBancaria>>
            ObtenerEgresosCuentasAsync(
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            var resultado = new List<EgresoCuentaBancaria>();

            const string sql = @"
SELECT
    CB.Clave,
    CASE
        WHEN NULLIF(LTRIM(RTRIM(CB.Cuenta)), '') IS NULL
            THEN ISNULL(CB.Nombre, '')
        ELSE
            ISNULL(CB.Nombre, '') + ' - ' + CB.Cuenta
    END AS Cuenta,
    ISNULL(SUM(
        CASE
            WHEN E.Fecha IS NOT NULL
             AND YEAR(E.Fecha) = @Anio
             AND MONTH(E.Fecha) <= @Mes
            THEN ISNULL(E.Pago, 0)
            ELSE 0
        END
    ), 0) AS Egresos
FROM CuentasBancarias CB
LEFT JOIN Egreso E
    ON E.CuentaBancaria = CB.Clave
WHERE CB.Estatus = 'Activo'
GROUP BY
    CB.Clave,
    CB.Nombre,
    CB.Cuenta
ORDER BY
    CB.Clave;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                command.Parameters.Add(
                    "@Mes",
                    SqlDbType.Int).Value =
                    mesCorte > 0 ? mesCorte : 12;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                using (var reader = await command
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false))
                {
                    while (await reader
                        .ReadAsync(cancellationToken)
                        .ConfigureAwait(false))
                    {
                        resultado.Add(new EgresoCuentaBancaria
                        {
                            ClaveCuenta =
                                ConvertirEntero(reader.GetValue(0)),

                            Cuenta = reader.IsDBNull(1)
                                ? string.Empty
                                : reader.GetString(1),

                            Egresos =
                                ConvertirDecimal(reader.GetValue(2))
                        });
                    }
                }
            }

            return resultado.AsReadOnly();
        }

        // ============================================================
        // ANTIGÜEDAD DE CARTERA
        // ============================================================

        private async Task<IReadOnlyList<EgresoAntiguedad>>
            ObtenerAntiguedadAsync(
                int anio,
                int mesCorte,
                CancellationToken cancellationToken)
        {
            List<EgresoAntiguedad> resultado =
                CrearListaAntiguedad();

            if (mesCorte <= 0)
            {
                return resultado.AsReadOnly();
            }

            DateTime fechaCorte =
                new DateTime(
                    anio,
                    mesCorte,
                    DateTime.DaysInMonth(anio, mesCorte));

            const string sql = @"
SELECT
    CASE
        WHEN RG.FechaVence > @FechaCorte
            THEN 1

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 0 AND 30
            THEN 2

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 31 AND 60
            THEN 3

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 61 AND 90
            THEN 4

        ELSE 5
    END AS Orden,

    COUNT(*) AS NumeroDocumentos,

    ISNULL(SUM(RG.Saldo), 0) AS Saldo

FROM RegistroGastos RG
WHERE RG.Fecha IS NOT NULL
  AND YEAR(RG.Fecha) = @Anio
  AND MONTH(RG.Fecha) <= @Mes
  AND RG.Estatus = 'Bloqueado'
  AND ISNULL(RG.Saldo, 0) > 0
GROUP BY
    CASE
        WHEN RG.FechaVence > @FechaCorte
            THEN 1

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 0 AND 30
            THEN 2

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 31 AND 60
            THEN 3

        WHEN DATEDIFF(
            DAY,
            RG.FechaVence,
            @FechaCorte
        ) BETWEEN 61 AND 90
            THEN 4

        ELSE 5
    END
ORDER BY Orden;";

            using (var connection = CrearConexion())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add(
                    "@Anio",
                    SqlDbType.Int).Value = anio;

                command.Parameters.Add(
                    "@Mes",
                    SqlDbType.Int).Value = mesCorte;

                command.Parameters.Add(
                    "@FechaCorte",
                    SqlDbType.Date).Value = fechaCorte;

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                using (var reader = await command
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false))
                {
                    while (await reader
                        .ReadAsync(cancellationToken)
                        .ConfigureAwait(false))
                    {
                        int orden =
                            ConvertirEntero(reader.GetValue(0));

                        EgresoAntiguedad item =
                            resultado.FirstOrDefault(
                                x => x.Orden == orden);

                        if (item != null)
                        {
                            item.NumeroDocumentos =
                                ConvertirEntero(reader.GetValue(1));

                            item.Saldo =
                                ConvertirDecimal(reader.GetValue(2));
                        }
                    }
                }
            }

            return resultado.AsReadOnly();
        }

        // ============================================================
        // LISTAS BASE
        // ============================================================

        private static List<EgresoMensual> CrearListaMeses()
        {
            var cultura =
                CultureInfo.GetCultureInfo("es-MX");

            var resultado = new List<EgresoMensual>();

            for (int mes = 1; mes <= 12; mes++)
            {
                string nombre =
                    cultura.DateTimeFormat.GetMonthName(mes);

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    nombre =
                        char.ToUpper(nombre[0], cultura) +
                        nombre.Substring(1);
                }

                resultado.Add(new EgresoMensual
                {
                    Mes = mes,
                    NombreMes = nombre,
                    TotalEgresos = 0m
                });
            }

            return resultado;
        }

        private static List<EgresoAntiguedad>
            CrearListaAntiguedad()
        {
            return new List<EgresoAntiguedad>
            {
                new EgresoAntiguedad
                {
                    Orden = 1,
                    Rango = "Por Vencer",
                    NumeroDocumentos = 0,
                    Saldo = 0m
                },

                new EgresoAntiguedad
                {
                    Orden = 2,
                    Rango = "1 - 30 días",
                    NumeroDocumentos = 0,
                    Saldo = 0m
                },

                new EgresoAntiguedad
                {
                    Orden = 3,
                    Rango = "31 - 60 días",
                    NumeroDocumentos = 0,
                    Saldo = 0m
                },

                new EgresoAntiguedad
                {
                    Orden = 4,
                    Rango = "61 - 90 días",
                    NumeroDocumentos = 0,
                    Saldo = 0m
                },

                new EgresoAntiguedad
                {
                    Orden = 5,
                    Rango = "91+ días",
                    NumeroDocumentos = 0,
                    Saldo = 0m
                }
            };
        }

        // ============================================================
        // UTILIDADES
        // ============================================================

        private SqlConnection CrearConexion()
        {
            return new SqlConnection(connectionString);
        }

        private static decimal ConvertirDecimal(object valor)
        {
            if (valor == null ||
                valor == DBNull.Value)
            {
                return 0m;
            }

            return Convert.ToDecimal(
                valor,
                CultureInfo.InvariantCulture);
        }

        private static int ConvertirEntero(object valor)
        {
            if (valor == null ||
                valor == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(
                valor,
                CultureInfo.InvariantCulture);
        }
    }
}