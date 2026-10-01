using System.Collections.Generic;
using System.Linq;

namespace PV.dto
{
    public class ResumenDashboardCompras
    {
        public ResumenDashboardCompras(
            int anio,
            int mesCorte,
            decimal totalCompras,
            decimal promedioMensual,
            IEnumerable<CompraMensual> meses,
            IEnumerable<CompraProveedor> proveedores,
            IEnumerable<CompraClasificacion> clasificaciones,
            ComparativoCompraAnual comparativo)
        {
            Anio = anio;
            MesCorte = mesCorte;
            TotalCompras = totalCompras;
            PromedioMensual = promedioMensual;

            Meses = (meses ?? Enumerable.Empty<CompraMensual>())
                .ToList()
                .AsReadOnly();

            Proveedores = (proveedores ?? Enumerable.Empty<CompraProveedor>())
                .ToList()
                .AsReadOnly();

            Clasificaciones = (clasificaciones ?? Enumerable.Empty<CompraClasificacion>())
                .ToList()
                .AsReadOnly();

            Comparativo = comparativo;
        }

        public int Anio { get; private set; }

        public int MesCorte { get; private set; }

        public decimal TotalCompras { get; private set; }

        public decimal PromedioMensual { get; private set; }

        public IReadOnlyList<CompraMensual> Meses { get; private set; }

        public IReadOnlyList<CompraProveedor> Proveedores { get; private set; }

        public IReadOnlyList<CompraClasificacion> Clasificaciones { get; private set; }

        public ComparativoCompraAnual Comparativo { get; private set; }
    }
}