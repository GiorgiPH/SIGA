using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.Clases.ConceptoPago
{
    public class DBClaseConcepto
    {
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public List<ClaseConceptoData> ObtenerActivos()
        {
            const string sql = "SELECT IdClase, Clave, Descripcion FROM dbo.ClaseConceptoTesoreria WHERE Activo = 1 ORDER BY Descripcion;";
            var lista = new List<ClaseConceptoData>();

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ClaseConceptoData
                        {
                            IdClase = Convert.ToByte(reader["IdClase"]),
                            Clave = reader["Clave"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
    }

    public class ClaseConceptoData
    {
        public byte IdClase { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
