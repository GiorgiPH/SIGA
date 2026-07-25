using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using PV.Properties;

namespace PuntoVentas.Clases.Login
{
    class DBLogin
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

        public static string usuario = string.Empty;
        public static string TipoUsuario = string.Empty;
        public static string Estatus = string.Empty;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Fecha2 = string.Empty;
        public static string DatosEmpresa = string.Empty;
        public static int Citados = 0;
        public static int Asistio = 0;
        public static int Cancelada = 0;
        public static decimal Total = 0;
        public static decimal Saldo = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBLogin()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Ahora cada método administra su propia
            // conexión, así que ya no es necesario abrir nada aquí. Se conserva
            // el constructor vacío para no romper código existente que hace
            // "new DBLogin()".
        }

        //____________________________________________________________________________________________________________________________________________
        //Inicio de sesion, comprueba que el usuario y contraseña existan en la basa de datos
        public int InicioSesion(string Usuario, string Contraseña)
        {
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select Usuario, TipoUsuario, Estatus from Usuarios where Usuario='" + Usuario + "' and Contraseña='" + Contraseña + "'", cn))
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
                            usuario = Usuario;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                TipoUsuario = dt.Rows[0][1].ToString();
                                Estatus = dt.Rows[0][2].ToString();
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

        //____________________________________________________________________________________________________________________________________________
        //Registro de Acceso en la base de datos
        public string RegistroAcceso(string txtUsuario, string txtContraseña, DateTime FechaEntrada, string Entrada)
        {
            string mensaje = "Registro guardado.";
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Insert into Acceso (Usuario, Contraseña, FechaEntrada, HoraEntrada) values ('" + txtUsuario + "','" + txtContraseña + "','" + FechaEntrada.ToString("yyyy/MM/dd") + "','" + Entrada + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }

        //__________________________________________________________________________________________________________________________________________________
        public void Logo(PictureBox Foto)
        {
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select Foto from General", cn))
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
                                if (dt.Rows[0][0].ToString() != "")
                                {
                                    // NOTA: el código original leía "dr["Foto"]" en este punto,
                                    // pero el SqlDataReader ya se había cerrado (dr.Close()) antes
                                    // de llegar aquí, lo que provocaba una excepción silenciosa
                                    // (se perdía el intento de cargar el logo). Con "using" el
                                    // reader queda fuera de alcance en este punto por la misma
                                    // razón, así que se toma el valor equivalente ya cargado en
                                    // memoria por el DataAdapter (dt.Rows[0][0]), que es la misma
                                    // columna "Foto" que se seleccionó en la consulta.
                                    byte[] datos = (byte[])dt.Rows[0][0];

                                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(datos))
                                    {
                                        Foto.Image = System.Drawing.Bitmap.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void empresa()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from DatosEmpresa", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            DatosEmpresa = dr["RazonSocial"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void totales(string ClavePropietario)
        {
            try
            {
                Total = 0;
                Saldo = 0;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select sum(Total) as Total, sum(Saldo) as Saldo from Recibo where ClavePropietario= '" + ClavePropietario + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Total = Convert.ToDecimal(dr["Total"].ToString());
                            Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error" + ex.ToString());
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void totalesFecha(string ClavePropietario, string fecha1, string fecha2)
        {
            try
            {
                Total = 0;
                Saldo = 0;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select sum(Total) as Total, sum(Saldo) as Saldo from Recibo where ClavePropietario= '" + ClavePropietario + "' and Fecha between '" + fecha1 + "' and '" + fecha2 + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Total = Convert.ToDecimal(dr["Total"].ToString());
                            Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error" + ex.ToString());
            }
        }

        //____________________________________________________________________________________________________________________________________________
        //Registro de Salida
        public string RegistroSalida(string txtUsuario, DateTime FechaEntrada, string Entrada, DateTime FechaSalida, string Salida)
        {
            string mensaje = "Registro guardado.";
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Acceso set FechaSalida='" + FechaSalida.ToString("yyyy/MM/dd") + "', horaSalida='" + Salida + "' where Usuario='" + txtUsuario + "' and FechaEntrada='" + FechaEntrada.ToString("yyyy/MM/dd") + "' and HoraEntrada='" + Entrada + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }
    }
}