using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.dto
{
    public class CompraProveedor
    {
        public int IdProveedor { get; set; }

        public string Proveedor { get; set; }

        public int NumeroCompras { get; set; }

        public decimal TotalCompras { get; set; }
    }
}
