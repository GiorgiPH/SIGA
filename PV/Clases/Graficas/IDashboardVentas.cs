using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PV.dto;

namespace PV.Clases.Graficas
{
    public interface IDashboardVentas
    {
        Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken);

        Task<ResumenDashboardVentas> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<VentaMensual>> ObtenerResumenCentroCostosAsync(
            int anio,
            int centroCostos,
            CancellationToken cancellationToken);
    }
}