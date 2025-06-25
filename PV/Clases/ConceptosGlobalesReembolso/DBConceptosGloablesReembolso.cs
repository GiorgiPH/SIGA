using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PV.Properties;

namespace PV.Clases.ConceptosGlobalesReembolso
{
    internal class DBConceptosGloablesReembolso
    {
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public DataTable CargarConceptosExistentes(string folio, string partida, string clase)
        {
            try
            {
                string query = @"SELECT g.ClaveConceptoG, c.Nombre, c.Tipo, g.Descuento, g.Cargo, c.clase FROM ConceptoGlobalesGasto g " +
                   "JOIN ConceptosGlobales c ON g.ClaveConceptoG = c.Clave WHERE g.Folio = @folio and g.Partida=@Partida";
                if (!string.IsNullOrEmpty(clase))
                {
                    query += " and c.clase='" + clase + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(query
                    , ObtenerCn());

                da.SelectCommand.Parameters.AddWithValue("@Folio", folio);
                da.SelectCommand.Parameters.AddWithValue("@Partida", partida);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                // Puedes manejar mejor el error o lanzarlo para la capa superior
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }
        public void EliminarConceptosGlobales(string folio, string partida, string clase)
        {
            string query = "DELETE CGG FROM ConceptoGlobalesGasto AS CGG JOIN ConceptosGlobales AS CG ON CGG.ClaveConceptoG = CG.Clave WHERE Folio = @folio and Partida = @partida";
            if (!string.IsNullOrEmpty(clase))
            {
                query += " and CG.Clase='" + clase + "'";
            }
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@folio", folio);
                cmd.Parameters.AddWithValue("@partida", partida);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertarDescuento(string clave, string folio, string Partida, decimal valorDescuento, decimal valorCargo)
        {
            using (SqlConnection conn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO ConceptoGlobalesGasto (ClaveConceptoG, Folio, Partida, Descuento, Cargo) VALUES (@clave, @folio, @partida, @valorDescuento, @valorCargo)", conn))
            {
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@folio", folio);
                cmd.Parameters.AddWithValue("@partida", Partida);

                cmd.Parameters.AddWithValue("@valorDescuento", valorDescuento);
                cmd.Parameters.AddWithValue("@valorCargo", valorCargo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
