using System;
using System.Collections.Generic;
using System.Linq;

namespace PV.dto
{
    public sealed class VentaMensual
    {
        public int Mes { get; set; }
        public string NombreMes { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public sealed class VentaCentroCostos
    {
        public int? ClaveCentroCostos { get; set; }
        public string NombreCentroCostos { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public sealed class ResumenDashboardVentas
    {
        public int Anio { get; private set; }
        public IReadOnlyList<VentaMensual> Meses { get; private set; }
        public IReadOnlyList<VentaCentroCostos> Centros { get; private set; }
        public decimal TotalAnual { get; private set; }

        public ResumenDashboardVentas(int anio, IEnumerable<VentaMensual> meses,
            IEnumerable<VentaCentroCostos> centros)
        {
            Anio = anio;
            Meses = meses.ToList().AsReadOnly();
            Centros = centros.ToList().AsReadOnly();
            TotalAnual = Meses.Sum(m => m.TotalVentas);
            if (TotalAnual != Centros.Sum(c => c.TotalVentas))
                throw new InvalidOperationException("Los totales de ventas por mes y centro no coinciden.");
        }
    }
}
