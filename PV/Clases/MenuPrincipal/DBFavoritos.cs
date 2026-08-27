using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.Usuarios
{
    public class DBUsuariosFavoritos
    {
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        private static SqlConnection CrearConexionAbierta()
        {
            string cadenaConexion = ObtenerCn();

            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión ControlCondominiosConnectionString.");
            }

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            return conexion;
        }

        public List<string> CargarFavoritos(string usuario)
        {
            List<string> favoritos = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario))
                return favoritos;

            try
            {
                using (SqlConnection cn = CrearConexionAbierta())
                using (SqlCommand cmd = new SqlCommand(
                    "dbo.sp_UsuariosFavoritos_Obtener",
                    cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@Usuario",
                        SqlDbType.NVarChar,
                        20).Value = usuario.Trim();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string clave = Convert.ToString(dr["ClaveOpcion"]);

                            if (!string.IsNullOrWhiteSpace(clave))
                                favoritos.Add(clave.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar favoritos.\n" + ex.Message,
                    "Usuarios Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return favoritos;
        }

        public bool AgregarFavorito(
            string usuario,
            string claveOpcion)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            if (string.IsNullOrWhiteSpace(claveOpcion))
                return false;

            try
            {
                using (SqlConnection cn = CrearConexionAbierta())
                using (SqlCommand cmd = new SqlCommand(
                    "dbo.sp_UsuariosFavoritos_Agregar",
                    cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@Usuario",
                        SqlDbType.NVarChar,
                        20).Value = usuario.Trim();

                    cmd.Parameters.Add(
                        "@ClaveOpcion",
                        SqlDbType.NVarChar,
                        100).Value = claveOpcion.Trim();

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar favorito.\n" + ex.Message,
                    "Usuarios Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        public bool EliminarFavorito(
            string usuario,
            string claveOpcion)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            if (string.IsNullOrWhiteSpace(claveOpcion))
                return false;

            try
            {
                using (SqlConnection cn = CrearConexionAbierta())
                using (SqlCommand cmd = new SqlCommand(
                    "dbo.sp_UsuariosFavoritos_Eliminar",
                    cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@Usuario",
                        SqlDbType.NVarChar,
                        20).Value = usuario.Trim();

                    cmd.Parameters.Add(
                        "@ClaveOpcion",
                        SqlDbType.NVarChar,
                        100).Value = claveOpcion.Trim();

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar favorito.\n" + ex.Message,
                    "Usuarios Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        public int CantidadFavoritos(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return 0;

            try
            {
                using (SqlConnection cn = CrearConexionAbierta())
                using (SqlCommand cmd = new SqlCommand(
                    "dbo.sp_UsuariosFavoritos_Obtener",
                    cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@Usuario",
                        SqlDbType.NVarChar,
                        20).Value = usuario.Trim();

                    int cantidad = 0;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cantidad++;
                        }
                    }

                    return cantidad;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar favoritos.\n" + ex.Message,
                    "Usuarios Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return 0;
            }
        }

        public bool EliminarTodosLosFavoritos(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            try
            {
                using (SqlConnection cn = CrearConexionAbierta())
                using (SqlCommand cmd = new SqlCommand(
                    "dbo.sp_UsuariosFavoritos_EliminarTodos",
                    cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@Usuario",
                        SqlDbType.NVarChar,
                        20).Value = usuario.Trim();

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar los favoritos.\n" + ex.Message,
                    "Usuarios Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }
    }
}
