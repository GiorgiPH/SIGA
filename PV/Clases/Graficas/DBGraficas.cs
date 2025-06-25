using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases.Graficas
{
    class DBGraficas
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static decimal Enero = 0;
        public static decimal Febrero = 0;
        public static decimal Marzo = 0;
        public static decimal Abril = 0;
        public static decimal Mayo = 0;
        public static decimal Junio = 0;
        public static decimal Julio = 0;
        public static decimal Agosto = 0;
        public static decimal Septiembre = 0;
        public static decimal Octubre = 0;
        public static decimal Noviembre = 0;
        public static decimal Diciembre = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBGraficas()
        {
            try
            {
                cn = new SqlConnection(ObtenerCn());
                cn.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________

        public void GraficaConsultas(string Año)
        {
            Enero = 0;
            Febrero = 0;
            Marzo = 0;
            Abril = 0;
            Mayo = 0;
            Junio = 0;
            Julio = 0;
            Agosto = 0;
            Septiembre = 0;
            Octubre = 0;
            Noviembre = 0;
            Diciembre = 0;
            try
            {
                cmd = new SqlCommand("select sum(Pago) as Pago, Month(Fecha) as Mes from Cobros where Year(Fecha)='"+Año+"' group by Year(Fecha), Month(Fecha), Pago", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["Mes"].ToString() == "1")
                    {
                        Enero = Enero + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "2")
                    {
                        Febrero = Febrero + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "3")
                    {
                        Marzo = Marzo + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "4")
                    {
                        Abril = Abril + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "5")
                    {
                        Mayo = Mayo + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "6")
                    {
                        Junio = Junio + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "7")
                    {
                        Julio = Julio + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "8")
                    {
                        Agosto = Agosto + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "9")
                    {
                        Septiembre = Septiembre + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "10")
                    {
                        Octubre = Octubre + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "11")
                    {
                        Noviembre = Noviembre + Convert.ToDecimal(dr["Pago"].ToString());
                    }
                    else if (dr["Mes"].ToString() == "12")
                    {
                        Diciembre = Diciembre + Convert.ToDecimal(dr["Pago"].ToString());
                    }


                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
                dr.Close();
            }
        }
        public DataTable ObtenerDatosGastosPorProyectoYMes()
        {
            string connStr = ObtenerCn();
            string query = @"
        SELECT 
            CAST(CentroCostos AS VARCHAR) AS Proyecto,
            FORMAT(Fecha, 'yyyy-MM') AS Mes,
            YEAR(Fecha) AS Año,
            SUM(Total) AS Monto
        FROM RegistroGastos
        WHERE Estatus = 'Bloqueado'
        GROUP BY CentroCostos, FORMAT(Fecha, 'yyyy-MM'), YEAR(Fecha)
    ";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        //___________________________________________________________________--
    }
}
