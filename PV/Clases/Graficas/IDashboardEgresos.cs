using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PV.dto;

namespace PV.Clases.Graficas
{
    public interface IDashboardEgresos
    {
        Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken);

        Task<ResumenDashboardEgresos> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken);
    }
}