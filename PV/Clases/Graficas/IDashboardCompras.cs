using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PV.dto;

namespace PV.Clases.Graficas
{
    public interface IDashboardCompras
    {
        Task<IReadOnlyList<int>> ObtenerAniosAsync(
            CancellationToken cancellationToken);

        Task<ResumenDashboardCompras> ObtenerResumenAnualAsync(
            int anio,
            CancellationToken cancellationToken);
    }
}