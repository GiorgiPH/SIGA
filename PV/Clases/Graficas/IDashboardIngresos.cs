using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PV.dto;

namespace PV.Clases.Graficas
{
    public interface IDashboardIngresos
    {
        Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken);

        Task<ResumenDashboardIngresos> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken);
    }
}