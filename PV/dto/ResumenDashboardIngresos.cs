using System.Collections.Generic;
using System.Linq;

namespace PV.dto
{
    public class ResumenDashboardIngresos
    {
        public ResumenDashboardIngresos(
            int anio,
            int mesCorte,
            decimal totalIngresos,
            decimal promedioMensual,
            decimal totalCartera,
            IEnumerable<IngresoMensual> meses,
            IEnumerable<CarteraCliente> clientes,
            IEnumerable<IngresoCuenta> cuentas,
            IEnumerable<AntiguedadCartera> antiguedad)
        {
            Anio = anio;
            MesCorte = mesCorte;
            TotalIngresos = totalIngresos;
            PromedioMensual = promedioMensual;
            TotalCartera = totalCartera;

            Meses =
                (meses ?? Enumerable.Empty<IngresoMensual>())
                .ToList()
                .AsReadOnly();

            Clientes =
                (clientes ?? Enumerable.Empty<CarteraCliente>())
                .ToList()
                .AsReadOnly();

            Cuentas =
                (cuentas ?? Enumerable.Empty<IngresoCuenta>())
                .ToList()
                .AsReadOnly();

            Antiguedad =
                (antiguedad ?? Enumerable.Empty<AntiguedadCartera>())
                .ToList()
                .AsReadOnly();
        }

        public int Anio { get; private set; }

        public int MesCorte { get; private set; }

        public decimal TotalIngresos { get; private set; }

        public decimal PromedioMensual { get; private set; }

        public decimal TotalCartera { get; private set; }

        public IReadOnlyList<IngresoMensual> Meses
        {
            get;
            private set;
        }

        public IReadOnlyList<CarteraCliente> Clientes
        {
            get;
            private set;
        }

        public IReadOnlyList<IngresoCuenta> Cuentas
        {
            get;
            private set;
        }

        public IReadOnlyList<AntiguedadCartera> Antiguedad
        {
            get;
            private set;
        }
    }
}