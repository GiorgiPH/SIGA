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
    public sealed class DBDashboardVentas : IDashboardVentas
    {
        private readonly string cadenaConexion;

        public DBDashboardVentas()
            : this(Settings.Default.ControlCondominiosConnectionString)
        {
        }

        public DBDashboardVentas(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión " +
                    "'ControlCondominiosConnectionString'.");
            }

            this.cadenaConexion = cadenaConexion;
        }

        /*
         * Criterio utilizado:
         *
         * Ventas:
         * - Factura
         * - Remision
         *
         * Otros ingresos:
         * - MovimientoBanco con Tipo = 'i'
         *
         * MovimientoBanco se relaciona con CentroCostos mediante:
         *
         * MovimientoBanco.ClaveCuentaBancaria
         *      -> CuentasBancarias.Clave
         *      -> CentroCostos.CuentaContable
         *
         * No se realiza validación de UsuarioPermiso.
         */
        private const string ConsultaResumen = @"
WITH Movimientos AS
(
    SELECT
        MONTH(F.Fecha) AS Mes,
        F.CentroCostos,
        CAST(0 AS INT) AS EsOtro,
        F.Total AS Importe
    FROM Factura F
    WHERE
        F.Fecha >= @Desde
        AND F.Fecha < @Hasta

    UNION ALL

    SELECT
        MONTH(R.Fecha) AS Mes,
        R.CentroCostos,
        CAST(0 AS INT) AS EsOtro,
        R.Total AS Importe
    FROM Remision R
    WHERE
        R.Fecha >= @Desde
        AND R.Fecha < @Hasta

    UNION ALL

    SELECT
        MONTH(MB.Fecha) AS Mes,
        CC.Clave AS CentroCostos,
        CAST(1 AS INT) AS EsOtro,
        MB.Importe AS Importe
    FROM MovimientoBanco MB
    LEFT JOIN CuentasBancarias CB
        ON CB.Clave = MB.ClaveCuentaBancaria
    LEFT JOIN CentroCostos CC
        ON CC.CuentaContable = CB.Clave
    WHERE
        MB.Tipo = 'i'
        AND MB.Fecha >= @Desde
        AND MB.Fecha < @Hasta
),
Resumen AS
(
    SELECT
        Mes,
        CentroCostos,
        EsOtro,
        ISNULL(SUM(Importe), 0) AS TotalIngresos
    FROM Movimientos
    GROUP BY
        Mes,
        CentroCostos,
        EsOtro
)
SELECT
    R.Mes,
    R.CentroCostos,
    R.EsOtro,
    R.TotalIngresos,
    (
        SELECT MAX(C.Nombre)
        FROM CentroCostos C
        WHERE C.Clave = R.CentroCostos
    ) AS Nombre
FROM Resumen R
ORDER BY
    R.Mes,
    R.EsOtro,
    R.CentroCostos;
";

        public async Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            const string sql = @"
SELECT
    MIN(A.Anio) AS PrimerAnio,
    MAX(A.Anio) AS UltimoAnio
FROM
(
    SELECT YEAR(F.Fecha) AS Anio
    FROM Factura F
    WHERE F.Fecha IS NOT NULL

    UNION ALL

    SELECT YEAR(R.Fecha) AS Anio
    FROM Remision R
    WHERE R.Fecha IS NOT NULL

    UNION ALL

    SELECT YEAR(MB.Fecha) AS Anio
    FROM MovimientoBanco MB
    WHERE
        MB.Tipo = 'i'
        AND MB.Fecha IS NOT NULL
) A;
";

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
                                    Convert.ToInt32(reader.GetValue(0));
                            }

                            if (!reader.IsDBNull(1))
                            {
                                ultimoAnio =
                                    Convert.ToInt32(reader.GetValue(1));
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

        public async Task<ResumenDashboardVentas> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken)
        {
            if (anio < 1 || anio > 9998)
            {
                throw new ArgumentOutOfRangeException("anio");
            }

            cancellationToken.ThrowIfCancellationRequested();

            CultureInfo cultura =
                CultureInfo.GetCultureInfo("es-MX");

            List<VentaMensual> meses =
                Enumerable.Range(1, 12)
                .Select(m => new VentaMensual
                {
                    Mes = m,
                    NombreMes =
                        cultura.TextInfo.ToTitleCase(
                            cultura.DateTimeFormat.GetMonthName(m)),
                    TotalVentas = 0M
                })
                .ToList();

            Dictionary<string, VentaCentroCostos> centros =
                new Dictionary<string, VentaCentroCostos>();

            DateTime desde =
                new DateTime(anio, 1, 1);

            DateTime hasta =
                new DateTime(anio + 1, 1, 1);

            using (SqlConnection cn = CrearConexion())
            {
                await AbrirConexionAsync(
                    cn,
                    cancellationToken).ConfigureAwait(false);

                using (SqlCommand cmd =
                    new SqlCommand(ConsultaResumen, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters
                        .Add("@Desde", SqlDbType.DateTime)
                        .Value = desde;

                    cmd.Parameters
                        .Add("@Hasta", SqlDbType.DateTime)
                        .Value = hasta;

                    using (SqlDataReader reader =
                        await cmd.ExecuteReaderAsync(
                            cancellationToken).ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(
                            cancellationToken).ConfigureAwait(false))
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            int mes =
                                Convert.ToInt32(reader.GetValue(0));

                            int? clave = null;

                            if (!reader.IsDBNull(1))
                            {
                                clave =
                                    Convert.ToInt32(reader.GetValue(1));
                            }

                            bool esOtro =
                                Convert.ToInt32(reader.GetValue(2)) == 1;

                            decimal importe = 0M;

                            if (!reader.IsDBNull(3))
                            {
                                importe =
                                    Convert.ToDecimal(reader.GetValue(3));
                            }

                            string nombre = null;

                            if (!reader.IsDBNull(4))
                            {
                                nombre =
                                    Convert.ToString(reader.GetValue(4));
                            }

                            if (mes < 1 || mes > 12)
                            {
                                continue;
                            }

                            string claveDiccionario;

                            if (clave.HasValue)
                            {
                                claveDiccionario =
                                    "centro:" +
                                    clave.Value.ToString(
                                        CultureInfo.InvariantCulture);
                            }
                            else if (esOtro)
                            {
                                claveDiccionario =
                                    "otros-sin-centro";
                            }
                            else
                            {
                                claveDiccionario =
                                    "ventas-sin-centro";
                            }

                            VentaCentroCostos centro;

                            if (!centros.TryGetValue(
                                claveDiccionario,
                                out centro))
                            {
                                centro = new VentaCentroCostos();

                                centro.ClaveCentroCostos = clave;

                                if (clave.HasValue)
                                {
                                    string nombreCentro =
                                        string.IsNullOrWhiteSpace(nombre)
                                            ? "Centro sin catálogo"
                                            : nombre;

                                    centro.NombreCentroCostos =
                                        clave.Value.ToString(cultura) +
                                        " — " +
                                        nombreCentro;
                                }
                                else if (esOtro)
                                {
                                    centro.NombreCentroCostos =
                                        "Otros ingresos sin centro";
                                }
                                else
                                {
                                    centro.NombreCentroCostos =
                                        "Ventas sin centro";
                                }

                                centro.TotalVentas = 0M;

                                centros.Add(
                                    claveDiccionario,
                                    centro);
                            }

                            /*
                             * El total mensual siempre incluye:
                             *
                             * Factura
                             * Remision
                             * MovimientoBanco Tipo = 'i'
                             *
                             * independientemente de si el ingreso tiene
                             * un Centro de Costos relacionado.
                             */
                            meses[mes - 1].TotalVentas += importe;

                            /*
                             * El mismo importe se acumula en el
                             * Centro de Costos correspondiente.
                             */
                            centro.TotalVentas += importe;
                        }
                    }
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<VentaCentroCostos> centrosOrdenados =
                centros.Values
                .OrderByDescending(c => c.TotalVentas)
                .ThenBy(c => c.NombreCentroCostos);

            return new ResumenDashboardVentas(
                anio,
                meses,
                centrosOrdenados);
        }

        public async Task<IReadOnlyList<VentaMensual>>
            ObtenerResumenCentroCostosAsync(
                int anio,
                int centroCostos,
                CancellationToken cancellationToken)
        {
            if (anio < 1 || anio > 9998)
            {
                throw new ArgumentOutOfRangeException("anio");
            }

            cancellationToken.ThrowIfCancellationRequested();

            CultureInfo cultura =
                CultureInfo.GetCultureInfo("es-MX");

            List<VentaMensual> meses =
                Enumerable.Range(1, 12)
                .Select(m => new VentaMensual
                {
                    Mes = m,
                    NombreMes =
                        cultura.TextInfo.ToTitleCase(
                            cultura.DateTimeFormat.GetMonthName(m)),
                    TotalVentas = 0M
                })
                .ToList();

            DateTime desde =
                new DateTime(anio, 1, 1);

            DateTime hasta =
                new DateTime(anio + 1, 1, 1);

            const string sql = @"
SELECT
    Mes,
    ISNULL(SUM(Importe), 0) AS TotalVentas
FROM
(
    SELECT
        MONTH(F.Fecha) AS Mes,
        F.Total AS Importe
    FROM Factura F
    WHERE
        F.Fecha >= @Desde
        AND F.Fecha < @Hasta
        AND F.CentroCostos = @CentroCostos

    UNION ALL

    SELECT
        MONTH(R.Fecha) AS Mes,
        R.Total AS Importe
    FROM Remision R
    WHERE
        R.Fecha >= @Desde
        AND R.Fecha < @Hasta
        AND R.CentroCostos = @CentroCostos

    UNION ALL

    SELECT
        MONTH(MB.Fecha) AS Mes,
        MB.Importe AS Importe
    FROM MovimientoBanco MB
    INNER JOIN CuentasBancarias CB
        ON CB.Clave = MB.ClaveCuentaBancaria
    INNER JOIN CentroCostos CC
        ON CC.CuentaContable = CB.Clave
    WHERE
        MB.Tipo = 'i'
        AND MB.Fecha >= @Desde
        AND MB.Fecha < @Hasta
        AND CC.Clave = @CentroCostos
) M
GROUP BY
    Mes
ORDER BY
    Mes;
";

            using (SqlConnection cn = CrearConexion())
            {
                await AbrirConexionAsync(
                    cn,
                    cancellationToken).ConfigureAwait(false);

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.CommandType =
                        CommandType.Text;

                    cmd.CommandTimeout =
                        60;

                    cmd.Parameters
                        .Add("@Desde", SqlDbType.DateTime)
                        .Value = desde;

                    cmd.Parameters
                        .Add("@Hasta", SqlDbType.DateTime)
                        .Value = hasta;

                    cmd.Parameters
                        .Add("@CentroCostos", SqlDbType.Int)
                        .Value = centroCostos;

                    using (SqlDataReader reader =
                        await cmd.ExecuteReaderAsync(
                            cancellationToken).ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(
                            cancellationToken).ConfigureAwait(false))
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            int mes =
                                Convert.ToInt32(
                                    reader.GetValue(0));

                            decimal total =
                                reader.IsDBNull(1)
                                    ? 0M
                                    : Convert.ToDecimal(
                                        reader.GetValue(1));

                            if (mes >= 1 &&
                                mes <= 12)
                            {
                                meses[mes - 1].TotalVentas =
                                    total;
                            }
                        }
                    }
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            return meses.AsReadOnly();
        }

        private SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
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