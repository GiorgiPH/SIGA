using System.Collections.Generic;
using System.Linq;

namespace PV.dto
{
    public class ResumenDashboardEgresos
    {
        public ResumenDashboardEgresos(
            int anio,
            int mesCorte,
            decimal totalEgresos,
            decimal promedioMensual,
            decimal totalCartera,
            IEnumerable<EgresoMensual> meses,
            IEnumerable<EgresoProveedor> proveedores,
            IEnumerable<EgresoCuentaBancaria> cuentas,
            IEnumerable<EgresoAntiguedad> antiguedad)
        {
            Anio = anio;
            MesCorte = mesCorte;
            TotalEgresos = totalEgresos;
            PromedioMensual = promedioMensual;
            TotalCartera = totalCartera;

            Meses = (meses ?? Enumerable.Empty<EgresoMensual>())
                .ToList()
                .AsReadOnly();

            Proveedores = (proveedores ?? Enumerable.Empty<EgresoProveedor>())
                .ToList()
                .AsReadOnly();

            Cuentas = (cuentas ?? Enumerable.Empty<EgresoCuentaBancaria>())
                .ToList()
                .AsReadOnly();

            Antiguedad = (antiguedad ?? Enumerable.Empty<EgresoAntiguedad>())
                .ToList()
                .AsReadOnly();
        }

        public int Anio { get; private set; }

        public int MesCorte { get; private set; }

        public decimal TotalEgresos { get; private set; }

        public decimal PromedioMensual { get; private set; }

        public decimal TotalCartera { get; private set; }

        public IReadOnlyList<EgresoMensual> Meses { get; private set; }

        public IReadOnlyList<EgresoProveedor> Proveedores { get; private set; }

        public IReadOnlyList<EgresoCuentaBancaria> Cuentas { get; private set; }

        public IReadOnlyList<EgresoAntiguedad> Antiguedad { get; private set; }
    }
}