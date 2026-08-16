using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.dto
{
    public class ConceptoCobroPagoData
    {
        public int IdConcepto { get; set; }
        public string ClaveConcepto { get; set; }
        public string Descripcion { get; set; }
        public byte IdClase { get; set; }
        public string ClaseDescripcion { get; set; }
        public bool Estatus { get; set; }
        public bool RequiereAutorizacion { get; set; }
        public byte? TipoAutorizacion { get; set; }
        public string Notas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Usuario { get; set; } // usuario de sesión, usado al insertar/actualizar/eliminar
    }
}
