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
        // NOTA: se eliminaron los campos de instancia (cn, cmd, dr, da, dt) que se
        // compartían entre métodos. Esa práctica dejaba conexiones y DataReaders
        // abiertos (el constructor abría "cn" una sola vez y nunca se cerraba).
        // Ahora cada método abre su propia conexión dentro de un bloque "using",
        // por lo que se cierra y libera automáticamente, incluso si hay una excepción.
        //
        // Las firmas de los métodos, los parámetros de entrada y los valores de
        // retorno son EXACTAMENTE los mismos que en la clase original, así como
        // la lógica de negocio (mismas consultas, mismo orden de operaciones).

        public static int Folio = 0;
        public static string Ruta = string.Empty;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBRespaldo()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Ahora cada método administra su propia
            // conexión, así que ya no es necesario abrir nada aquí. Se conserva
            // el constructor vacío para no romper código existente que hace
            // "new DBRespaldo()".
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

                string r1 = Carpeta.Substring(0, 1);

                int found = Carpeta.IndexOf(":");
                string r = Carpeta.Substring(found + 2);

                Carpeta = r1 + ":" + @"\\" + r + @"\\" + mensaje;

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE [ControlCondominios] TO DISK = N'" + Carpeta + "' WITH NOFORMAT, NOINIT, NAME=N'Copia de Seguridad', SKIP, NOREWIND, NOUNLOAD, STATS=10", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select Ruta from DatosEmpresa", cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                contador++;
                            }
                        }

                        if (contador > 0)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                Ruta = dt.Rows[0][0].ToString();
                            }
                        }
                    }
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