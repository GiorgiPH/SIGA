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
        // NOTA: se eliminó el campo de instancia "cn" que se compartía entre
        // métodos (incluyendo los privados InsertarPermisos/ActualizarPermisos,
        // llamados desde GuardarPermisosUsuario). Esa práctica dejaba la conexión
        // abierta durante toda la vida del objeto sin garantía de cierre.
        //
        // Ahora cada método público abre su propia conexión dentro de un bloque
        // "using". Cuando una operación pública necesita invocar un método
        // interno (InsertarPermisos/ActualizarPermisos) como parte de la MISMA
        // operación lógica (GuardarPermisosUsuario), se le pasa la conexión ya
        // abierta como parámetro, en vez de depender de un campo compartido.
        // Estos dos métodos son privados (detalle de implementación), así que
        // agregarles el parámetro de conexión no cambia la API pública de la
        // clase ni la lógica de negocio.
        //
        // Las firmas de los métodos PÚBLICOS, sus parámetros de entrada y sus
        // valores de retorno son EXACTAMENTE los mismos que en la clase original,
        // así como la lógica de negocio (mismas consultas, mismo orden de
        // operaciones).

        public DBPermisos()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Ahora cada método administra su propia
            // conexión, así que ya no es necesario abrir nada aquí. Se conserva
            // el constructor vacío para no romper código existente que hace
            // "new DBPermisos()".
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
                using (SqlConnection cn = new SqlConnection(DBUsuarios.ObtenerCn()))
                {
                    cn.Open();

                    string query = "SELECT * FROM UsuarioPermiso WHERE USUARIO = @Usuario";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
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
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(DBUsuarios.ObtenerCn()))
                {
                    cn.Open();

                    // Verificar si el usuario ya tiene permisos registrados
                    int count;
                    string checkQuery = "SELECT COUNT(*) FROM UsuarioPermiso WHERE USUARIO = @Usuario";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, cn))
                    {
                        checkCmd.Parameters.AddWithValue("@Usuario", usuario);
                        count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    }

                    if (count == 0)
                    {
                        // Insertar nuevo registro
                        return InsertarPermisos(cn, usuario, permisos);
                    }
                    else
                    {
                        // Actualizar registro existente
                        return ActualizarPermisos(cn, usuario, permisos);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar permisos: " + ex.ToString();
            }
        }

        private string InsertarPermisos(SqlConnection cn, string usuario, Dictionary<string, string> permisos)
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
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.ExecuteNonQuery();
                }
                return "Permisos guardados correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar permisos: " + ex.ToString();
            }
        }

        private string ActualizarPermisos(SqlConnection cn, string usuario, Dictionary<string, string> permisos)
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

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Permisos actualizados correctamente." : "No se encontró el usuario.";
                }
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
                using (SqlConnection cn = new SqlConnection(DBUsuarios.ObtenerCn()))
                {
                    cn.Open();

                    string query = "SELECT USUARIO FROM UsuarioPermiso ORDER BY USUARIO";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            usuarios.Add(dr["USUARIO"].ToString());
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(DBUsuarios.ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM UsuarioPermiso WHERE USUARIO = @Usuario", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Usuario", usuario);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Permisos eliminados correctamente." : "No se encontró el usuario.";
                }
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
            // Ya no se mantiene una conexión abierta a nivel de instancia: cada
            // método abre y cierra la suya propia mediante "using". Se conserva
            // este método público (vacío) para no romper código existente que
            // lo invoque explícitamente.
        }
    }
}