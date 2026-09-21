using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PuntoVentas.Clases.Usuarios
{
    public class DBPermisos
    {
        private readonly string cadenaConexion;

        public DBPermisos()
        {
            cadenaConexion =
                DBUsuarios.ObtenerCn();
        }

        // ============================================================
        // OBTENER TODAS LAS OPCIONES ACTIVAS
        // ============================================================

        public List<OpcionMenu> ObtenerOpcionesMenu()
        {
            List<OpcionMenu> resultado =
                new List<OpcionMenu>();

            const string sql = @"
SELECT
    IdOpcionMenu,
    IdOpcionPadre,
    Clave,
    Nombre,
    Nivel,
    Orden,
    EsOpcion,
    Activo
FROM dbo.OpcionMenu
WHERE Activo = 1
ORDER BY Nivel, IdOpcionPadre, Orden, IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cn.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultado.Add(
                            LeerOpcionMenu(dr, false));
                    }
                }
            }

            return resultado;
        }

        // ============================================================
        // OBTENER OPCIONES + PERMISOS DEL USUARIO
        // ============================================================

        public List<OpcionMenu> ObtenerPermisosUsuario(
            string usuario)
        {
            List<OpcionMenu> resultado =
                new List<OpcionMenu>();

            const string sql = @"
SELECT
    OM.IdOpcionMenu,
    OM.IdOpcionPadre,
    OM.Clave,
    OM.Nombre,
    OM.Nivel,
    OM.Orden,
    OM.EsOpcion,
    OM.Activo,
    CAST(
        CASE
            WHEN UP.Permitido = 1 THEN 1
            ELSE 0
        END
    AS BIT) AS Permitido
FROM dbo.OpcionMenu OM
LEFT JOIN dbo.UsuarioPermiso UP
    ON UP.IdOpcionMenu = OM.IdOpcionMenu
    AND UP.Usuario = @Usuario
WHERE OM.Activo = 1
ORDER BY
    OM.Nivel,
    OM.IdOpcionPadre,
    OM.Orden,
    OM.IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@Usuario",
                    SqlDbType.VarChar,
                    50).Value =
                    usuario ?? "";

                cn.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultado.Add(
                            LeerOpcionMenu(dr, true));
                    }
                }
            }

            return resultado;
        }

        // ============================================================
        // CONSTRUIR ÁRBOL
        // ============================================================

        public List<OpcionMenu> ObtenerArbolPermisosUsuario(
            string usuario)
        {
            List<OpcionMenu> opciones =
                ObtenerPermisosUsuario(usuario);

            Dictionary<int, OpcionMenu> indice =
                opciones.ToDictionary(
                    x => x.IdOpcionMenu,
                    x => x);

            foreach (OpcionMenu opcion in opciones)
            {
                opcion.Hijos.Clear();
            }

            List<OpcionMenu> raices =
                new List<OpcionMenu>();

            foreach (OpcionMenu opcion in opciones)
            {
                if (!opcion.IdOpcionPadre.HasValue)
                {
                    raices.Add(opcion);
                    continue;
                }

                OpcionMenu padre;

                if (indice.TryGetValue(
                    opcion.IdOpcionPadre.Value,
                    out padre))
                {
                    padre.Hijos.Add(opcion);
                }
            }

            OrdenarArbol(raices);

            return raices;
        }

        private void OrdenarArbol(
            List<OpcionMenu> opciones)
        {
            opciones.Sort(
                delegate (
                    OpcionMenu a,
                    OpcionMenu b)
                {
                    int resultado =
                        a.Orden.CompareTo(b.Orden);

                    if (resultado != 0)
                        return resultado;

                    return a.IdOpcionMenu.CompareTo(
                        b.IdOpcionMenu);
                });

            foreach (OpcionMenu opcion in opciones)
            {
                if (opcion.Hijos != null &&
                    opcion.Hijos.Count > 0)
                {
                    OrdenarArbol(opcion.Hijos);
                }
            }
        }

        // ============================================================
        // PERMISO DIRECTO
        // ============================================================

        public bool TienePermiso(
            string usuario,
            string clave)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.UsuarioPermiso UP
INNER JOIN dbo.OpcionMenu OM
    ON OM.IdOpcionMenu = UP.IdOpcionMenu
WHERE UP.Usuario = @Usuario
  AND OM.Clave = @Clave
  AND OM.Activo = 1
  AND UP.Permitido = 1;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@Usuario",
                    SqlDbType.VarChar,
                    50).Value =
                    usuario ?? "";

                cmd.Parameters.Add(
                    "@Clave",
                    SqlDbType.VarChar,
                    100).Value =
                    clave ?? "";

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // PERMISO EFECTIVO
        // Considera toda la jerarquía de padres.
        // ============================================================

        public bool TienePermisoEfectivo(
            string usuario,
            string clave)
        {
            List<OpcionMenu> arbol =
                ObtenerArbolPermisosUsuario(usuario);

            OpcionMenu opcion =
                BuscarPorClave(arbol, clave);

            if (opcion == null)
                return false;

            return TienePermisoEfectivo(
                opcion,
                arbol);
        }

        private bool TienePermisoEfectivo(
            OpcionMenu opcion,
            List<OpcionMenu> arbol)
        {
            if (opcion == null ||
                !opcion.Activo ||
                !opcion.Permitido)
            {
                return false;
            }

            if (!opcion.IdOpcionPadre.HasValue)
                return true;

            OpcionMenu padre =
                BuscarPorId(
                    arbol,
                    opcion.IdOpcionPadre.Value);

            if (padre == null)
                return false;

            return TienePermisoEfectivo(
                padre,
                arbol);
        }

        private OpcionMenu BuscarPorClave(
            IEnumerable<OpcionMenu> opciones,
            string clave)
        {
            foreach (OpcionMenu opcion in opciones)
            {
                if (string.Equals(
                    opcion.Clave,
                    clave,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return opcion;
                }

                OpcionMenu encontrado =
                    BuscarPorClave(
                        opcion.Hijos,
                        clave);

                if (encontrado != null)
                    return encontrado;
            }

            return null;
        }

        private OpcionMenu BuscarPorId(
            IEnumerable<OpcionMenu> opciones,
            int id)
        {
            foreach (OpcionMenu opcion in opciones)
            {
                if (opcion.IdOpcionMenu == id)
                    return opcion;

                OpcionMenu encontrado =
                    BuscarPorId(
                        opcion.Hijos,
                        id);

                if (encontrado != null)
                    return encontrado;
            }

            return null;
        }

        // ============================================================
        // GUARDAR PERMISOS
        // ============================================================

        public string GuardarPermisosUsuario(
            string usuario,
            IEnumerable<OpcionMenu> opciones)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new ArgumentException(
                    "El usuario es obligatorio.",
                    "usuario");
            }

            if (opciones == null)
            {
                throw new ArgumentNullException(
                    "opciones");
            }

            List<OpcionMenu> lista =
                opciones.ToList();

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            {
                cn.Open();

                using (SqlTransaction tx =
                    cn.BeginTransaction())
                {
                    try
                    {
                        const string sqlEliminar = @"
DELETE FROM dbo.UsuarioPermiso
WHERE Usuario = @Usuario;";

                        using (SqlCommand cmdEliminar =
                            new SqlCommand(
                                sqlEliminar,
                                cn,
                                tx))
                        {
                            cmdEliminar.Parameters.Add(
                                "@Usuario",
                                SqlDbType.VarChar,
                                50).Value =
                                usuario;

                            cmdEliminar.ExecuteNonQuery();
                        }

                        const string sqlInsertar = @"
INSERT INTO dbo.UsuarioPermiso
(
    Usuario,
    IdOpcionMenu,
    Permitido
)
VALUES
(
    @Usuario,
    @IdOpcionMenu,
    @Permitido
);";

                        foreach (OpcionMenu opcion in lista)
                        {
                            using (SqlCommand cmdInsertar =
                                new SqlCommand(
                                    sqlInsertar,
                                    cn,
                                    tx))
                            {
                                cmdInsertar.Parameters.Add(
                                    "@Usuario",
                                    SqlDbType.VarChar,
                                    50).Value =
                                    usuario;

                                cmdInsertar.Parameters.Add(
                                    "@IdOpcionMenu",
                                    SqlDbType.Int).Value =
                                    opcion.IdOpcionMenu;

                                cmdInsertar.Parameters.Add(
                                    "@Permitido",
                                    SqlDbType.Bit).Value =
                                    opcion.Permitido;

                                cmdInsertar.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();

                        return
                            "Permisos guardados correctamente.";
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // ============================================================
        // ELIMINAR PERMISOS
        // ============================================================

        public void EliminarPermisosUsuario(
            string usuario)
        {
            const string sql = @"
DELETE FROM dbo.UsuarioPermiso
WHERE Usuario = @Usuario;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@Usuario",
                    SqlDbType.VarChar,
                    50).Value =
                    usuario ?? "";

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // LECTOR
        // ============================================================

        private OpcionMenu LeerOpcionMenu(
            SqlDataReader dr,
            bool incluyePermiso)
        {
            OpcionMenu opcion =
                new OpcionMenu();

            opcion.IdOpcionMenu =
                Convert.ToInt32(
                    dr["IdOpcionMenu"]);

            if (dr["IdOpcionPadre"] ==
                DBNull.Value)
            {
                opcion.IdOpcionPadre = null;
            }
            else
            {
                opcion.IdOpcionPadre =
                    Convert.ToInt32(
                        dr["IdOpcionPadre"]);
            }

            opcion.Clave =
                Convert.ToString(
                    dr["Clave"]);

            opcion.Nombre =
                Convert.ToString(
                    dr["Nombre"]);

            opcion.Nivel =
                Convert.ToInt32(
                    dr["Nivel"]);

            opcion.Orden =
                Convert.ToInt32(
                    dr["Orden"]);

            opcion.EsOpcion =
                Convert.ToBoolean(
                    dr["EsOpcion"]);

            opcion.Activo =
                Convert.ToBoolean(
                    dr["Activo"]);

            opcion.Permitido =
                incluyePermiso &&
                Convert.ToBoolean(
                    dr["Permitido"]);

            return opcion;
        }

        // Compatibilidad con código anterior.
        public void CerrarConexion()
        {
        }
    }
}