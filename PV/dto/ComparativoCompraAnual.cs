using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.dto
{
    public class ComparativoCompraAnual
    {
        public int AnioAnterior { get; set; }

        public decimal TotalAnioAnterior { get; set; }

        public int AnioActual { get; set; }

        public decimal TotalAnioActual { get; set; }
    }
}