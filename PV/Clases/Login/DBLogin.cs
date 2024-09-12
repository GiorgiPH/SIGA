using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using PV.Properties;

namespace PuntoVentas.Clases.Login
{
    class DBLogin
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

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

        //____________________________________________________________________________________________________________________________________________
        //Inicio de sesion, comprueba que el usuario y contraseña existan en la basa de datos
        public int InicioSesion(string Usuario, string Contraseña)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Usuario, TipoUsuario, Estatus from Usuarios where Usuario='" + Usuario + "' and Contraseña='" + Contraseña + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    usuario = Usuario;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    TipoUsuario = dt.Rows[0][1].ToString();
                    Estatus = dt.Rows[0][2].ToString();
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
                cmd = new SqlCommand("Insert into Acceso (Usuario, Contraseña, FechaEntrada, HoraEntrada) values ('" + txtUsuario + "','" + txtContraseña + "','" + FechaEntrada.ToString("yyyy/MM/dd") + "','" + Entrada + "')", cn);
                cmd.ExecuteNonQuery();
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
                cmd = new SqlCommand("select Foto from General", cn);
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
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        byte[] datos = new byte[0];
                        datos = (byte[])dr["Foto"];

                        System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                        Foto.Image = System.Drawing.Bitmap.FromStream(ms);
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
                cmd = new SqlCommand("Select * from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    DatosEmpresa = dr["RazonSocial"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
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
                cmd = new SqlCommand("Select sum(Total) as Total, sum(Saldo) as Saldo from Recibo where ClavePropietario= '"+ ClavePropietario + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Total = Convert.ToDecimal( dr["Total"].ToString());
                    Saldo = Convert.ToDecimal( dr["Saldo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                //MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void totalesFecha(string ClavePropietario,  string fecha1, string fecha2)
        {
            try
            {
                Total = 0;
                Saldo = 0;
                cmd = new SqlCommand("Select sum(Total) as Total, sum(Saldo) as Saldo from Recibo where ClavePropietario= '" + ClavePropietario + "' and Fecha between '"+fecha1+"' and '"+fecha2+"'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Total = Convert.ToDecimal(dr["Total"].ToString());
                    Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
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
                cmd = new SqlCommand("Update Acceso set FechaSalida='" + FechaSalida.ToString("yyyy/MM/dd") + "', horaSalida='" + Salida + "' where Usuario='" + txtUsuario + "' and FechaEntrada='" + FechaEntrada.ToString("yyyy/MM/dd") + "' and HoraEntrada='" + Entrada + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
    }
}
