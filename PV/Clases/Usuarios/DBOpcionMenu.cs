using System.Collections.Generic;

namespace PuntoVentas.Clases.Usuarios
{
    public class OpcionMenu
    {
        public int IdOpcionMenu { get; set; }
        public int? IdOpcionPadre { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public int Orden { get; set; }
        public bool EsOpcion { get; set; }
        public bool Activo { get; set; }

        public bool Permitido { get; set; }

        public List<OpcionMenu> Hijos { get; set; }

        public OpcionMenu()
        {
            Hijos = new List<OpcionMenu>();
        }
    }
}