using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PuntoVentas.Clases.Usuarios
{
    public class DBMenu
    {
        private readonly string cadenaConexion;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public DBMenu()
        {
            cadenaConexion =
                DBUsuarios.ObtenerCn();
        }

        // ============================================================
        // OBTENER TODAS LAS OPCIONES
        // ============================================================

        public List<OpcionMenu> ObtenerOpciones()
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
ORDER BY
    Nivel,
    IdOpcionPadre,
    Orden,
    IdOpcionMenu;";

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
                            LeerOpcionMenu(dr));
                    }
                }
            }

            return resultado;
        }

        // ============================================================
        // OBTENER UNA OPCIÓN
        // ============================================================

        public OpcionMenu ObtenerOpcion(
            int idOpcionMenu)
        {
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
WHERE IdOpcionMenu = @IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cn.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return LeerOpcionMenu(dr);
                    }
                }
            }

            return null;
        }

        // ============================================================
        // EXISTE CLAVE
        // ============================================================

        public bool ExisteClave(
            string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return false;

            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE Clave = @Clave;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@Clave",
                    SqlDbType.VarChar,
                    100).Value =
                    clave.Trim();

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // EXISTE CLAVE EXCLUYENDO EL REGISTRO ACTUAL
        // ============================================================

        public bool ExisteClave(
            string clave,
            int idOpcionMenuExcluir)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return false;

            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE Clave = @Clave
  AND IdOpcionMenu <> @IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@Clave",
                    SqlDbType.VarChar,
                    100).Value =
                    clave.Trim();

                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenuExcluir;

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // SIGUIENTE ORDEN
        // ============================================================

        public int ObtenerSiguienteOrden(
            int? idOpcionPadre)
        {
            const string sql = @"
SELECT
    ISNULL(MAX(Orden), 0) + 1
FROM dbo.OpcionMenu
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
);";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                SqlParameter parametroPadre =
                    cmd.Parameters.Add(
                        "@IdOpcionPadre",
                        SqlDbType.Int);

                parametroPadre.Value =
                    idOpcionPadre.HasValue
                        ? (object)idOpcionPadre.Value
                        : DBNull.Value;

                cn.Open();

                object resultado =
                    cmd.ExecuteScalar();

                if (resultado == null ||
                    resultado == DBNull.Value)
                {
                    return 1;
                }

                return Convert.ToInt32(
                    resultado);
            }
        }

        // ============================================================
        // INSERTAR
        //
        // 1. Valida la opción.
        // 2. Ajusta la posición solicitada.
        // 3. Recorre las opciones existentes.
        // 4. Inserta la nueva opción.
        // 5. Concede automáticamente permiso a admin.
        //
        // Todo se realiza dentro de la misma transacción.
        // ============================================================

        public int InsertarOpcion(
            OpcionMenu opcion)
        {
            ValidarOpcion(opcion);

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            {
                cn.Open();

                using (SqlTransaction tx =
                    cn.BeginTransaction())
                {
                    try
                    {
                        // ============================================
                        // CANTIDAD DE HERMANOS
                        // ============================================

                        int cantidadHermanos =
                            ObtenerCantidadHermanos(
                                cn,
                                tx,
                                opcion.IdOpcionPadre,
                                null);

                        int ordenMaximo =
                            cantidadHermanos + 1;

                        // ============================================
                        // NORMALIZAR POSICIÓN
                        // ============================================

                        if (opcion.Orden >
                            ordenMaximo)
                        {
                            opcion.Orden =
                                ordenMaximo;
                        }

                        if (opcion.Orden < 1)
                        {
                            opcion.Orden =
                                1;
                        }

                        // ============================================
                        // ABRIR ESPACIO
                        // ============================================

                        DesplazarParaInsertar(
                            cn,
                            tx,
                            opcion.IdOpcionPadre,
                            opcion.Orden);

                        // ============================================
                        // INSERTAR OPCIÓN
                        // ============================================

                        const string sql = @"
INSERT INTO dbo.OpcionMenu
(
    IdOpcionPadre,
    Clave,
    Nombre,
    Nivel,
    Orden,
    EsOpcion,
    Activo
)
VALUES
(
    @IdOpcionPadre,
    @Clave,
    @Nombre,
    @Nivel,
    @Orden,
    @EsOpcion,
    @Activo
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int idNuevo;

                        using (SqlCommand cmd =
                            new SqlCommand(
                                sql,
                                cn,
                                tx))
                        {
                            AgregarParametrosOpcion(
                                cmd,
                                opcion);

                            object resultado =
                                cmd.ExecuteScalar();

                            if (resultado == null ||
                                resultado == DBNull.Value)
                            {
                                throw new InvalidOperationException(
                                    "No fue posible obtener el identificador " +
                                    "de la nueva opción.");
                            }

                            idNuevo =
                                Convert.ToInt32(
                                    resultado);
                        }

                        // ============================================
                        // PERMISO AUTOMÁTICO PARA ADMIN
                        // ============================================

                        AsignarPermisoAdministrador(
                            cn,
                            tx,
                            idNuevo);

                        // ============================================
                        // CONFIRMAR
                        // ============================================

                        tx.Commit();

                        return idNuevo;
                    }
                    catch
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch
                        {
                            // Si la conexión ya no permite rollback,
                            // conservamos la excepción original.
                        }

                        throw;
                    }
                }
            }
        }

        // ============================================================
        // ACTUALIZAR
        // ============================================================

        public void ActualizarOpcion(
            OpcionMenu opcion)
        {
            ValidarOpcion(opcion);

            if (opcion.IdOpcionMenu <= 0)
            {
                throw new ArgumentException(
                    "El identificador de la opción no es válido.",
                    "opcion");
            }

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            {
                cn.Open();

                using (SqlTransaction tx =
                    cn.BeginTransaction())
                {
                    try
                    {
                        // ============================================
                        // DATOS ACTUALES
                        // ============================================

                        OpcionMenu anterior =
                            ObtenerOpcionInterna(
                                cn,
                                tx,
                                opcion.IdOpcionMenu);

                        if (anterior == null)
                        {
                            throw new InvalidOperationException(
                                "La opción que intenta modificar ya no existe.");
                        }

                        bool cambioPadre =
                            anterior.IdOpcionPadre !=
                            opcion.IdOpcionPadre;

                        // ============================================
                        // CAMBIO DE PADRE
                        // ============================================

                        if (cambioPadre)
                        {
                            // Cerramos el hueco en el grupo anterior.
                            CerrarHueco(
                                cn,
                                tx,
                                anterior.IdOpcionPadre,
                                anterior.Orden,
                                opcion.IdOpcionMenu);

                            // Cantidad de elementos del nuevo grupo.
                            int cantidadNuevoPadre =
                                ObtenerCantidadHermanos(
                                    cn,
                                    tx,
                                    opcion.IdOpcionPadre,
                                    opcion.IdOpcionMenu);

                            int ordenMaximo =
                                cantidadNuevoPadre + 1;

                            if (opcion.Orden >
                                ordenMaximo)
                            {
                                opcion.Orden =
                                    ordenMaximo;
                            }

                            if (opcion.Orden < 1)
                            {
                                opcion.Orden =
                                    1;
                            }

                            // Abrimos espacio en el nuevo grupo.
                            DesplazarParaInsertar(
                                cn,
                                tx,
                                opcion.IdOpcionPadre,
                                opcion.Orden,
                                opcion.IdOpcionMenu);
                        }

                        // ============================================
                        // MISMO PADRE, DIFERENTE POSICIÓN
                        // ============================================

                        else if (anterior.Orden !=
                            opcion.Orden)
                        {
                            int cantidadHermanos =
                                ObtenerCantidadHermanos(
                                    cn,
                                    tx,
                                    opcion.IdOpcionPadre,
                                    opcion.IdOpcionMenu);

                            int ordenMaximo =
                                cantidadHermanos + 1;

                            if (opcion.Orden >
                                ordenMaximo)
                            {
                                opcion.Orden =
                                    ordenMaximo;
                            }

                            if (opcion.Orden < 1)
                            {
                                opcion.Orden =
                                    1;
                            }

                            ReordenarDentroDelMismoPadre(
                                cn,
                                tx,
                                opcion.IdOpcionPadre,
                                opcion.IdOpcionMenu,
                                anterior.Orden,
                                opcion.Orden);
                        }

                        // ============================================
                        // ACTUALIZAR REGISTRO
                        // ============================================

                        const string sql = @"
UPDATE dbo.OpcionMenu
SET
    IdOpcionPadre = @IdOpcionPadre,
    Clave = @Clave,
    Nombre = @Nombre,
    Nivel = @Nivel,
    Orden = @Orden,
    EsOpcion = @EsOpcion,
    Activo = @Activo
WHERE IdOpcionMenu = @IdOpcionMenu;";

                        using (SqlCommand cmd =
                            new SqlCommand(
                                sql,
                                cn,
                                tx))
                        {
                            AgregarParametrosOpcion(
                                cmd,
                                opcion);

                            cmd.Parameters.Add(
                                "@IdOpcionMenu",
                                SqlDbType.Int).Value =
                                opcion.IdOpcionMenu;

                            int afectados =
                                cmd.ExecuteNonQuery();

                            if (afectados == 0)
                            {
                                throw new InvalidOperationException(
                                    "La opción que intenta modificar ya no existe.");
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch
                        {
                            // Conservamos la excepción original.
                        }

                        throw;
                    }
                }
            }
        }

        // ============================================================
        // ELIMINAR OPCIÓN
        //
        // Reglas:
        //
        // 1. La opción debe existir.
        // 2. No se permite eliminar una opción que tenga hijos.
        // 3. Se eliminan primero los permisos asociados.
        // 4. Se elimina la opción del menú.
        // 5. Se cierra el hueco dejado en el orden de sus hermanos.
        // 6. Todo se realiza dentro de una sola transacción.
        // ============================================================

        public void EliminarOpcion(
            int idOpcionMenu)
        {
            if (idOpcionMenu <= 0)
            {
                throw new ArgumentException(
                    "El identificador de la opción no es válido.",
                    "idOpcionMenu");
            }

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            {
                cn.Open();

                using (SqlTransaction tx =
                    cn.BeginTransaction())
                {
                    try
                    {
                        // ============================================
                        // OBTENER OPCIÓN ACTUAL
                        // ============================================

                        OpcionMenu opcion =
                            ObtenerOpcionInterna(
                                cn,
                                tx,
                                idOpcionMenu);

                        if (opcion == null)
                        {
                            throw new InvalidOperationException(
                                "La opción que intenta eliminar ya no existe.");
                        }

                        // ============================================
                        // VALIDAR QUE NO TENGA HIJOS
                        // ============================================

                        if (TieneHijosInterno(
                            cn,
                            tx,
                            idOpcionMenu))
                        {
                            throw new InvalidOperationException(
                                "No se puede eliminar la opción \"" +
                                opcion.Nombre +
                                "\" porque contiene otras opciones." +
                                Environment.NewLine +
                                Environment.NewLine +
                                "Primero debe eliminar o mover las opciones que dependen de ella.");
                        }

                        // ============================================
                        // ELIMINAR PERMISOS
                        // ============================================

                        EliminarPermisosOpcion(
                            cn,
                            tx,
                            idOpcionMenu);

                        // ============================================
                        // ELIMINAR OPCIÓN
                        // ============================================

                        const string sql = @"
DELETE FROM dbo.OpcionMenu
WHERE IdOpcionMenu = @IdOpcionMenu;";

                        using (SqlCommand cmd =
                            new SqlCommand(
                                sql,
                                cn,
                                tx))
                        {
                            cmd.Parameters.Add(
                                "@IdOpcionMenu",
                                SqlDbType.Int).Value =
                                idOpcionMenu;

                            int afectados =
                                cmd.ExecuteNonQuery();

                            if (afectados == 0)
                            {
                                throw new InvalidOperationException(
                                    "La opción que intenta eliminar ya no existe.");
                            }
                        }

                        // ============================================
                        // CERRAR HUECO EN EL ORDEN
                        // ============================================

                        CerrarHueco(
                            cn,
                            tx,
                            opcion.IdOpcionPadre,
                            opcion.Orden,
                            idOpcionMenu);

                        // ============================================
                        // CONFIRMAR
                        // ============================================

                        tx.Commit();
                    }
                    catch
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch
                        {
                            // Si la conexión ya no permite rollback,
                            // conservamos la excepción original.
                        }

                        throw;
                    }
                }
            }
        }

        // ============================================================
        // PERMISO AUTOMÁTICO PARA ADMIN
        //
        // La nueva opción siempre queda habilitada para admin.
        //
        // Para los demás usuarios no se crea ningún registro.
        // La ausencia de registro equivale a Permitido = 0.
        // ============================================================

        private void AsignarPermisoAdministrador(
            SqlConnection cn,
            SqlTransaction tx,
            int idOpcionMenu)
        {
            const string sql = @"
IF EXISTS
(
    SELECT 1
    FROM dbo.UsuarioPermiso
    WHERE Usuario = @Usuario
      AND IdOpcionMenu = @IdOpcionMenu
)
BEGIN
    UPDATE dbo.UsuarioPermiso
    SET Permitido = 1
    WHERE Usuario = @Usuario
      AND IdOpcionMenu = @IdOpcionMenu;
END
ELSE
BEGIN
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
        1
    );
END;";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                cmd.Parameters.Add(
                    "@Usuario",
                    SqlDbType.VarChar,
                    50).Value =
                    "admin";

                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // TIENE HIJOS
        // ============================================================

        public bool TieneHijos(
            int idOpcionMenu)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE IdOpcionPadre = @IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // TIENE HIJOS DENTRO DE TRANSACCIÓN
        // ============================================================

        private bool TieneHijosInterno(
            SqlConnection cn,
            SqlTransaction tx,
            int idOpcionMenu)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE IdOpcionPadre = @IdOpcionMenu;";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // ELIMINAR PERMISOS DE UNA OPCIÓN
        // ============================================================

        private void EliminarPermisosOpcion(
            SqlConnection cn,
            SqlTransaction tx,
            int idOpcionMenu)
        {
            const string sql = @"
DELETE FROM dbo.UsuarioPermiso
WHERE IdOpcionMenu = @IdOpcionMenu;";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // VALIDAR DESCENDENCIA
        //
        // Evita crear referencias circulares.
        //
        // A
        // └── B
        //     └── C
        //
        // A no puede moverse debajo de B o C.
        // ============================================================

        public bool EsDescendiente(
            int idOpcionMenu,
            int posiblePadre)
        {
            const string sql = @"
;WITH Descendientes AS
(
    SELECT
        IdOpcionMenu,
        IdOpcionPadre
    FROM dbo.OpcionMenu
    WHERE IdOpcionPadre = @IdOpcionMenu

    UNION ALL

    SELECT
        OM.IdOpcionMenu,
        OM.IdOpcionPadre
    FROM dbo.OpcionMenu OM
    INNER JOIN Descendientes D
        ON OM.IdOpcionPadre = D.IdOpcionMenu
)
SELECT COUNT(1)
FROM Descendientes
WHERE IdOpcionMenu = @PosiblePadre
OPTION (MAXRECURSION 100);";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cmd.Parameters.Add(
                    "@PosiblePadre",
                    SqlDbType.Int).Value =
                    posiblePadre;

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // OBTENER NIVEL
        // ============================================================

        public int? ObtenerNivel(
            int idOpcionMenu)
        {
            const string sql = @"
SELECT Nivel
FROM dbo.OpcionMenu
WHERE IdOpcionMenu = @IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cn.Open();

                object resultado =
                    cmd.ExecuteScalar();

                if (resultado == null ||
                    resultado == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(
                    resultado);
            }
        }

        // ============================================================
        // EXISTE OPCIÓN
        // ============================================================

        public bool ExisteOpcion(
            int idOpcionMenu)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE IdOpcionMenu = @IdOpcionMenu;";

            using (SqlConnection cn =
                new SqlConnection(cadenaConexion))
            using (SqlCommand cmd =
                new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cn.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar()) > 0;
            }
        }

        // ============================================================
        // OBTENER OPCIÓN DENTRO DE TRANSACCIÓN
        // ============================================================

        private OpcionMenu ObtenerOpcionInterna(
            SqlConnection cn,
            SqlTransaction tx,
            int idOpcionMenu)
        {
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
WHERE IdOpcionMenu = @IdOpcionMenu;";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return LeerOpcionMenu(
                            dr);
                    }
                }
            }

            return null;
        }

        // ============================================================
        // CANTIDAD DE HERMANOS
        // ============================================================

        private int ObtenerCantidadHermanos(
            SqlConnection cn,
            SqlTransaction tx,
            int? idOpcionPadre,
            int? idExcluir)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.OpcionMenu
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
)
AND
(
    @IdExcluir IS NULL
    OR IdOpcionMenu <> @IdExcluir
);";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                SqlParameter padre =
                    cmd.Parameters.Add(
                        "@IdOpcionPadre",
                        SqlDbType.Int);

                padre.Value =
                    idOpcionPadre.HasValue
                        ? (object)idOpcionPadre.Value
                        : DBNull.Value;

                SqlParameter excluir =
                    cmd.Parameters.Add(
                        "@IdExcluir",
                        SqlDbType.Int);

                excluir.Value =
                    idExcluir.HasValue
                        ? (object)idExcluir.Value
                        : DBNull.Value;

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }

        // ============================================================
        // ABRIR ESPACIO PARA INSERTAR
        //
        // Todos los elementos desde la posición solicitada avanzan 1.
        // ============================================================

        private void DesplazarParaInsertar(
            SqlConnection cn,
            SqlTransaction tx,
            int? idOpcionPadre,
            int orden,
            int? idExcluir = null)
        {
            const string sql = @"
UPDATE dbo.OpcionMenu
SET Orden = Orden + 1
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
)
AND Orden >= @Orden
AND
(
    @IdExcluir IS NULL
    OR IdOpcionMenu <> @IdExcluir
);";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                SqlParameter padre =
                    cmd.Parameters.Add(
                        "@IdOpcionPadre",
                        SqlDbType.Int);

                padre.Value =
                    idOpcionPadre.HasValue
                        ? (object)idOpcionPadre.Value
                        : DBNull.Value;

                cmd.Parameters.Add(
                    "@Orden",
                    SqlDbType.Int).Value =
                    orden;

                SqlParameter excluir =
                    cmd.Parameters.Add(
                        "@IdExcluir",
                        SqlDbType.Int);

                excluir.Value =
                    idExcluir.HasValue
                        ? (object)idExcluir.Value
                        : DBNull.Value;

                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // CERRAR HUECO
        //
        // Cuando una opción abandona un grupo, los elementos
        // posteriores retroceden una posición.
        // ============================================================

        private void CerrarHueco(
            SqlConnection cn,
            SqlTransaction tx,
            int? idOpcionPadre,
            int ordenAnterior,
            int idExcluir)
        {
            const string sql = @"
UPDATE dbo.OpcionMenu
SET Orden = Orden - 1
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
)
AND Orden > @OrdenAnterior
AND IdOpcionMenu <> @IdExcluir;";

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                SqlParameter padre =
                    cmd.Parameters.Add(
                        "@IdOpcionPadre",
                        SqlDbType.Int);

                padre.Value =
                    idOpcionPadre.HasValue
                        ? (object)idOpcionPadre.Value
                        : DBNull.Value;

                cmd.Parameters.Add(
                    "@OrdenAnterior",
                    SqlDbType.Int).Value =
                    ordenAnterior;

                cmd.Parameters.Add(
                    "@IdExcluir",
                    SqlDbType.Int).Value =
                    idExcluir;

                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // REORDENAR DENTRO DEL MISMO PADRE
        // ============================================================

        private void ReordenarDentroDelMismoPadre(
            SqlConnection cn,
            SqlTransaction tx,
            int? idOpcionPadre,
            int idOpcionMenu,
            int ordenAnterior,
            int ordenNuevo)
        {
            if (ordenAnterior ==
                ordenNuevo)
            {
                return;
            }

            string sql;

            // ========================================================
            // SUBIR
            //
            // Ejemplo:
            //
            // 1 A
            // 2 B
            // 3 C
            // 4 D
            //
            // D -> posición 2
            //
            // 1 A
            // 2 D
            // 3 B
            // 4 C
            // ========================================================

            if (ordenNuevo <
                ordenAnterior)
            {
                sql = @"
UPDATE dbo.OpcionMenu
SET Orden = Orden + 1
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
)
AND Orden >= @OrdenNuevo
AND Orden < @OrdenAnterior
AND IdOpcionMenu <> @IdOpcionMenu;";
            }

            // ========================================================
            // BAJAR
            // ========================================================

            else
            {
                sql = @"
UPDATE dbo.OpcionMenu
SET Orden = Orden - 1
WHERE
(
    (@IdOpcionPadre IS NULL AND IdOpcionPadre IS NULL)
    OR
    (@IdOpcionPadre IS NOT NULL AND
     IdOpcionPadre = @IdOpcionPadre)
)
AND Orden > @OrdenAnterior
AND Orden <= @OrdenNuevo
AND IdOpcionMenu <> @IdOpcionMenu;";
            }

            using (SqlCommand cmd =
                new SqlCommand(
                    sql,
                    cn,
                    tx))
            {
                SqlParameter padre =
                    cmd.Parameters.Add(
                        "@IdOpcionPadre",
                        SqlDbType.Int);

                padre.Value =
                    idOpcionPadre.HasValue
                        ? (object)idOpcionPadre.Value
                        : DBNull.Value;

                cmd.Parameters.Add(
                    "@OrdenAnterior",
                    SqlDbType.Int).Value =
                    ordenAnterior;

                cmd.Parameters.Add(
                    "@OrdenNuevo",
                    SqlDbType.Int).Value =
                    ordenNuevo;

                cmd.Parameters.Add(
                    "@IdOpcionMenu",
                    SqlDbType.Int).Value =
                    idOpcionMenu;

                cmd.ExecuteNonQuery();
            }
        }

        // ============================================================
        // VALIDACIÓN INTERNA
        // ============================================================

        private void ValidarOpcion(
            OpcionMenu opcion)
        {
            if (opcion == null)
            {
                throw new ArgumentNullException(
                    "opcion");
            }

            if (string.IsNullOrWhiteSpace(
                opcion.Clave))
            {
                throw new ArgumentException(
                    "La clave de la opción es obligatoria.",
                    "opcion");
            }

            if (string.IsNullOrWhiteSpace(
                opcion.Nombre))
            {
                throw new ArgumentException(
                    "El nombre de la opción es obligatorio.",
                    "opcion");
            }

            if (opcion.Nivel < 1)
            {
                throw new ArgumentException(
                    "El nivel de la opción no es válido.",
                    "opcion");
            }

            if (opcion.Orden < 1)
            {
                throw new ArgumentException(
                    "El orden de la opción no es válido.",
                    "opcion");
            }

            if (!opcion.IdOpcionPadre.HasValue &&
                opcion.Nivel != 1)
            {
                throw new ArgumentException(
                    "Una opción sin padre debe pertenecer al nivel 1.",
                    "opcion");
            }

            if (opcion.IdOpcionPadre.HasValue &&
                opcion.Nivel <= 1)
            {
                throw new ArgumentException(
                    "Una opción con padre debe pertenecer a un nivel mayor a 1.",
                    "opcion");
            }
        }

        // ============================================================
        // PARÁMETROS COMUNES INSERT / UPDATE
        // ============================================================

        private void AgregarParametrosOpcion(
            SqlCommand cmd,
            OpcionMenu opcion)
        {
            SqlParameter parametroPadre =
                cmd.Parameters.Add(
                    "@IdOpcionPadre",
                    SqlDbType.Int);

            parametroPadre.Value =
                opcion.IdOpcionPadre.HasValue
                    ? (object)opcion.IdOpcionPadre.Value
                    : DBNull.Value;

            cmd.Parameters.Add(
                "@Clave",
                SqlDbType.VarChar,
                100).Value =
                opcion.Clave
                .Trim()
                .ToUpperInvariant();

            cmd.Parameters.Add(
                "@Nombre",
                SqlDbType.NVarChar,
                150).Value =
                opcion.Nombre.Trim();

            cmd.Parameters.Add(
                "@Nivel",
                SqlDbType.Int).Value =
                opcion.Nivel;

            cmd.Parameters.Add(
                "@Orden",
                SqlDbType.Int).Value =
                opcion.Orden;

            cmd.Parameters.Add(
                "@EsOpcion",
                SqlDbType.Bit).Value =
                opcion.EsOpcion;

            cmd.Parameters.Add(
                "@Activo",
                SqlDbType.Bit).Value =
                opcion.Activo;
        }

        // ============================================================
        // LEER OPCIÓN
        // ============================================================

        private OpcionMenu LeerOpcionMenu(
            SqlDataReader dr)
        {
            OpcionMenu opcion =
                new OpcionMenu();

            opcion.IdOpcionMenu =
                Convert.ToInt32(
                    dr["IdOpcionMenu"]);

            opcion.IdOpcionPadre =
                dr["IdOpcionPadre"] == DBNull.Value
                    ? (int?)null
                    : Convert.ToInt32(
                        dr["IdOpcionPadre"]);

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

            return opcion;
        }

        // ============================================================
        // COMPATIBILIDAD
        // ============================================================

        public void CerrarConexion()
        {
        }
    }
}