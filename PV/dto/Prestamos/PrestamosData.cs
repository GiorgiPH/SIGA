using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.dto.Prestamos
{
    public class PrestamoData
    {
        public int Folio { get; set; }
        public int? Consecutivo { get; set; }
        public string ClaveDocumento { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha { get; set; }
        public int ClaveAcreedor { get; set; }
        public string NombreAcreedor { get; set; }          // solo para mostrar (join con Clientes), no se guarda en Prestamo
        public int? IdConceptoCapital { get; set; }
        public string DescripcionConceptoCapital { get; set; } // solo para mostrar
        public string Referencia { get; set; }
        public int Parcialidades { get; set; }
        public DateTime FechaPrimerPago { get; set; }
        public int Periodicidad { get; set; }
        public decimal ImporteCPago { get; set; }
        public decimal InteresCPago { get; set; }
        public int? IdConceptoInteres { get; set; }
        public string DescripcionConceptoInteres { get; set; } // solo para mostrar
        public bool AplicaImpuesto { get; set; }
        public string Divisa { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Intereses { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public int TotalPartidas { get; set; }
        public string Notas { get; set; }
        public string Elaborado { get; set; }
        public string RutaDocumento { get; set; }
        public int DiasVence { get; set; }
        public DateTime? FechaVence { get; set; }
    }

    /// <summary>
    /// Una parcialidad del calendario de pagos (tabla dbo.PartidaPrestamo).
    /// Corresponde 1 a 1 con los campos de PanelPartidasRequisicion en el
    /// tab "Partidas". Después de generado el calendario, solo Vencimiento
    /// es modificable.
    /// </summary>
    public class PartidaPrestamoData
    {
        public int FolioPrestamo { get; set; }
        public int Partida { get; set; }
        public int TotalParcialidades { get; set; }         // no se guarda: se arma en memoria como "Partida/TotalParcialidades" para el label
        public DateTime Vencimiento { get; set; }
        public decimal ImporteCapital { get; set; }
        public int? IdConceptoCapital { get; set; }
        public string DescripcionConceptoCapital { get; set; }
        public decimal InteresParcialidad { get; set; }
        public decimal Iva { get; set; }
        public int? IdConceptoInteres { get; set; }
        public string DescripcionConceptoInteres { get; set; }
        public decimal Total { get; set; }
        public string Estatus { get; set; }
    }
}
