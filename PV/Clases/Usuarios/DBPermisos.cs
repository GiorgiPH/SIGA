using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PuntoVentas.Clases.Usuarios
{
    class DBPermisos
    {
        private SqlConnection cn;

        public DBPermisos()
        {
            try
            {
                cn = new SqlConnection(DBUsuarios.ObtenerCn());
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexión: " + ex.ToString());
            }
        }

        /// <summary>
        /// Carga los permisos de un usuario desde la base de datos
        /// </summary>
        /// <param name="usuario">Nombre del usuario</param>
        /// <returns>Diccionario con nombre de columna y valor ("activo"/"inactivo")</returns>
        public Dictionary<string, string> CargarPermisosUsuario(string usuario)
        {
            var permisos = new Dictionary<string, string>();

            try
            {
                string query = "SELECT * FROM UsuarioPermiso WHERE USUARIO = @Usuario";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Usuario", usuario);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // Obtener todos los campos de la tabla
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string columnName = dr.GetName(i);
                        if (columnName != "USUARIO") // Excluir la columna de usuario
                        {
                            string valor = dr.IsDBNull(i) ? "inactivo" : dr.GetString(i);
                            permisos[columnName] = valor;
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.ToString());
            }

            return permisos;
        }

        /// <summary>
        /// Guarda o actualiza los permisos de un usuario
        /// </summary>
        /// <param name="usuario">Nombre del usuario</param>
        /// <param name="permisos">Diccionario con nombre de columna y valor ("activo"/"inactivo")</param>
        /// <returns>Mensaje de resultado</returns>
        public string GuardarPermisosUsuario(string usuario, Dictionary<string, string> permisos)
        {
            try
            {
                // Verificar si el usuario ya tiene permisos registrados
                string checkQuery = "SELECT COUNT(*) FROM UsuarioPermiso WHERE USUARIO = @Usuario";
                SqlCommand checkCmd = new SqlCommand(checkQuery, cn);
                checkCmd.Parameters.AddWithValue("@Usuario", usuario);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    // Insertar nuevo registro
                    return InsertarPermisos(usuario, permisos);
                }
                else
                {
                    // Actualizar registro existente
                    return ActualizarPermisos(usuario, permisos);
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar permisos: " + ex.ToString();
            }
        }

        private string InsertarPermisos(string usuario, Dictionary<string, string> permisos)
        {
            try
            {
                // Construir la consulta de inserción
                string columns = "USUARIO";
                string values = "@Usuario";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Usuario", usuario)
                };

                int paramIndex = 1;
                foreach (var permiso in permisos)
                {
                    columns += $", [{permiso.Key}]";
                    values += $", @Param{paramIndex}";
                    parameters.Add(new SqlParameter($"@Param{paramIndex}", permiso.Value ?? "inactivo"));
                    paramIndex++;
                }

                string query = $"INSERT INTO UsuarioPermiso ({columns}) VALUES ({values})";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddRange(parameters.ToArray());

                cmd.ExecuteNonQuery();
                return "Permisos guardados correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar permisos: " + ex.ToString();
            }
        }

        private string ActualizarPermisos(string usuario, Dictionary<string, string> permisos)
        {
            try
            {
                // Construir la consulta de actualización
                string setClause = "";
                var parameters = new List<SqlParameter>();
                int paramIndex = 1;

                foreach (var permiso in permisos)
                {
                    if (setClause.Length > 0)
                        setClause += ", ";
                    
                    setClause += $"[{permiso.Key}] = @Param{paramIndex}";
                    parameters.Add(new SqlParameter($"@Param{paramIndex}", permiso.Value ?? "inactivo"));
                    paramIndex++;
                }

                parameters.Add(new SqlParameter("@Usuario", usuario));
                string query = $"UPDATE UsuarioPermiso SET {setClause} WHERE USUARIO = @Usuario";
                
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddRange(parameters.ToArray());

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Permisos actualizados correctamente." : "No se encontró el usuario.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar permisos: " + ex.ToString();
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios que tienen permisos registrados
        /// </summary>
        /// <returns>Lista de nombres de usuario</returns>
        public List<string> ObtenerUsuariosConPermisos()
        {
            var usuarios = new List<string>();

            try
            {
                string query = "SELECT USUARIO FROM UsuarioPermiso ORDER BY USUARIO";
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    usuarios.Add(dr["USUARIO"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener usuarios: " + ex.ToString());
            }

            return usuarios;
        }

        /// <summary>
        /// Elimina los permisos de un usuario
        /// </summary>
        /// <param name="usuario">Nombre del usuario</param>
        /// <returns>Mensaje de resultado</returns>
        public string EliminarPermisosUsuario(string usuario)
        {
            try
            {
                string query = "DELETE FROM UsuarioPermiso WHERE USUARIO = @Usuario";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Usuario", usuario);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Permisos eliminados correctamente." : "No se encontró el usuario.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar permisos: " + ex.ToString();
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        public void CerrarConexion()
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
}
