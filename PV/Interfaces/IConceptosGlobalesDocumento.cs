using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.Interfaces
{
    public interface IConceptosGlobalesDocumento
    {
        DataTable CargarConceptosExistentes(
            string folio,
            string partida,
            string clase);

        void EliminarConceptosGlobales(
            string folio,
            string partida,
            string clase);

        void InsertarDescuento(
            string clave,
            string folio,
            string partida,
            decimal descuento,
            decimal impuesto);
    }
}
