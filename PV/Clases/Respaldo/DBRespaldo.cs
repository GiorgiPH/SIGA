using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.Respaldo
{
    class DBRespaldo
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static string Ruta = string.Empty;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBRespaldo()
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
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public string Respaldo()
        {
            string mensaje = "Respaldo" + DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString();

            try
            {

                string Carpeta = Ruta + @"\\" + "Backup";
                
                try
                {
                    if (Directory.Exists(Carpeta))
                    {

                    }
                    else
                    {
                        Directory.CreateDirectory(Carpeta);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

             
                string r1 = Carpeta.Substring(0,1);

                int found = Carpeta.IndexOf(":");
                string r = Carpeta.Substring(found + 2);

                Carpeta = r1 + ":" + @"\\" + r + @"\\" + mensaje;

                cmd = new SqlCommand("BACKUP DATABASE [ControlCondominios] TO DISK = N'"+Carpeta+"' WITH NOFORMAT, NOINIT, NAME=N'Copia de Seguridad', SKIP, NOREWIND, NOUNLOAD, STATS=10", cn);
                cmd.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Aqui." + ex.ToString());
            }
            return "Respaldo Guardado";

        }
        //_________________________________________________________________________________________
        public int ruta()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Ruta from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    Ruta = dt.Rows[0][0].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
    }
}
