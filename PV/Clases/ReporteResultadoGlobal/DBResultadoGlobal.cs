using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using static Guna.UI2.WinForms.Suite.Descriptions;

public class DBResultadoGlobal
{
    private readonly string conexion = Settings.Default.ControlCondominiosConnectionString;

    public DataTable ObtenerCentrosCostos()
    {
        DataTable dt = new DataTable();

        using (SqlConnection cn = new SqlConnection(conexion))
        {
            string query = "SELECT Clave, Nombre FROM CentroCostos ORDER BY Nombre";

            using (SqlDataAdapter da = new SqlDataAdapter(query, cn))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public DataTable ObtenerProyectos(int centroCostos)
    {
        DataTable dt = new DataTable();

        using (SqlConnection cn = new SqlConnection(conexion))
        {
            string query = "SELECT Id, Proyecto FROM DatosProyecto WHERE Folio = @CentroCostos ORDER BY Proyecto";

            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@CentroCostos", SqlDbType.Int).Value = centroCostos;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }
}