using PV.dto;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV.Clases.ConceptoPago
{
    public class DBConceptoCobroPago
    {
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public int Insertar(ConceptoCobroPagoData dato)
        {
            const string sql = @"
                INSERT INTO dbo.ConceptoCobroPago
                    (ClaveConcepto, Descripcion, IdClase, Estatus, RequiereAutorizacion, TipoAutorizacion, Notas, UsuarioCreacion)
                VALUES
                    (@ClaveConcepto, @Descripcion, @IdClase, @Estatus, @RequiereAutorizacion, @TipoAutorizacion, @Notas, @UsuarioCreacion);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ClaveConcepto", SqlDbType.VarChar, 10).Value = dato.ClaveConcepto;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 150).Value = dato.Descripcion;
                cmd.Parameters.Add("@IdClase", SqlDbType.TinyInt).Value = dato.IdClase;
                cmd.Parameters.Add("@Estatus", SqlDbType.Bit).Value = dato.Estatus;
                cmd.Parameters.Add("@RequiereAutorizacion", SqlDbType.Bit).Value = dato.RequiereAutorizacion;
                cmd.Parameters.Add("@TipoAutorizacion", SqlDbType.TinyInt).Value =
                    dato.TipoAutorizacion.HasValue ? (object)dato.TipoAutorizacion.Value : DBNull.Value;
                cmd.Parameters.Add("@Notas", SqlDbType.NVarChar, 500).Value = (object)dato.Notas ?? DBNull.Value;
                cmd.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 50).Value = dato.Usuario;

                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Actualizar(ConceptoCobroPagoData dato)
        {
            const string sql = @"
                UPDATE dbo.ConceptoCobroPago
                SET Descripcion          = @Descripcion,
                    IdClase              = @IdClase,
                    Estatus              = @Estatus,
                    RequiereAutorizacion = @RequiereAutorizacion,
                    TipoAutorizacion     = @TipoAutorizacion,
                    Notas                = @Notas,
                    FechaModificacion    = GETDATE(),
                    UsuarioModificacion  = @UsuarioModificacion
                WHERE IdConcepto = @IdConcepto;";
            // Nota: ClaveConcepto no se actualiza — es inmutable una vez creado el registro.

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@IdConcepto", SqlDbType.Int).Value = dato.IdConcepto;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 150).Value = dato.Descripcion;
                cmd.Parameters.Add("@IdClase", SqlDbType.TinyInt).Value = dato.IdClase;
                cmd.Parameters.Add("@Estatus", SqlDbType.Bit).Value = dato.Estatus;
                cmd.Parameters.Add("@RequiereAutorizacion", SqlDbType.Bit).Value = dato.RequiereAutorizacion;
                cmd.Parameters.Add("@TipoAutorizacion", SqlDbType.TinyInt).Value =
                    dato.TipoAutorizacion.HasValue ? (object)dato.TipoAutorizacion.Value : DBNull.Value;
                cmd.Parameters.Add("@Notas", SqlDbType.NVarChar, 500).Value = (object)dato.Notas ?? DBNull.Value;
                cmd.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = dato.Usuario;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idConcepto, string usuario) // baja lógica
        {
            const string sql = @"
                UPDATE dbo.ConceptoCobroPago
                SET Estatus             = 0,
                    FechaModificacion   = GETDATE(),
                    UsuarioModificacion = @UsuarioModificacion
                WHERE IdConcepto = @IdConcepto;";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@IdConcepto", SqlDbType.Int).Value = idConcepto;
                cmd.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = usuario;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ConceptoCobroPagoData ObtenerPorId(int idConcepto)
        {
            const string sql = @"
                SELECT c.IdConcepto, c.ClaveConcepto, c.Descripcion, c.IdClase, cc.Descripcion AS ClaseDescripcion,
                       c.Estatus, c.RequiereAutorizacion, c.TipoAutorizacion, c.Notas,
                       c.FechaCreacion, c.UsuarioCreacion, c.FechaModificacion, c.UsuarioModificacion
                FROM dbo.ConceptoCobroPago c
                INNER JOIN dbo.ClaseConceptoTesoreria cc ON cc.IdClase = c.IdClase
                WHERE c.IdConcepto = @IdConcepto;";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@IdConcepto", SqlDbType.Int).Value = idConcepto;

                cn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    return reader.Read() ? Mapear(reader) : null;
            }
        }

        public List<ConceptoCobroPagoData> Listar(string filtro, bool? soloActivos)
        {
            const string sql = @"
                SELECT c.IdConcepto, c.ClaveConcepto, c.Descripcion, c.IdClase, cc.Descripcion AS ClaseDescripcion,
                       c.Estatus, c.RequiereAutorizacion, c.TipoAutorizacion, c.Notas,
                       c.FechaCreacion, c.UsuarioCreacion, c.FechaModificacion, c.UsuarioModificacion
                FROM dbo.ConceptoCobroPago c
                INNER JOIN dbo.ClaseConceptoTesoreria cc ON cc.IdClase = c.IdClase
                WHERE (@Filtro IS NULL OR c.ClaveConcepto LIKE '%' + @Filtro + '%' OR c.Descripcion LIKE '%' + @Filtro + '%')
                  AND (@SoloActivos IS NULL OR c.Estatus = @SoloActivos)
                ORDER BY c.ClaveConcepto;";

            var lista = new List<ConceptoCobroPagoData>();

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 150).Value = (object)filtro ?? DBNull.Value;
                cmd.Parameters.Add("@SoloActivos", SqlDbType.Bit).Value = (object)soloActivos ?? DBNull.Value;

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(Mapear(reader));
                }
            }
            return lista;
        }

        public bool ExisteClave(string claveConcepto, int idConceptoExcluir)
        {
            const string sql = @"
                SELECT COUNT(1) FROM dbo.ConceptoCobroPago
                WHERE ClaveConcepto = @ClaveConcepto AND IdConcepto <> @IdConceptoExcluir;";
            // idConceptoExcluir = 0 para altas nuevas (los IDENTITY empiezan en 1, nunca colisiona).

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@ClaveConcepto", SqlDbType.VarChar, 10).Value = claveConcepto;
                cmd.Parameters.Add("@IdConceptoExcluir", SqlDbType.Int).Value = idConceptoExcluir;

                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool TieneMovimientosAsociados(int idConcepto)
        {
            // Ajustar el nombre real de la tabla de movimientos de tesorería cuando exista.
            const string sql = "SELECT COUNT(1) FROM dbo.MovimientoTesoreria WHERE IdConcepto = @IdConcepto;";

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@IdConcepto", SqlDbType.Int).Value = idConcepto;
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        /// <summary>
        /// Devuelve los conceptos de cobro/pago de una clase específica
        /// (p. ej. Ingresos) como DataTable, listos para pintar en un
        /// ComboBox con ComboUtil.LlenarComboBox (DisplayMember =
        /// "Descripcion", ValueMember = "IdConcepto").
        /// </summary>
        public DataTable ListarParaCombo(byte idClase, bool soloActivos = true)
        {
            const string sql = @"
                SELECT IdConcepto, ClaveConcepto, Descripcion
                FROM dbo.ConceptoCobroPago
                WHERE IdClase = @IdClase
                  AND (@SoloActivos = 0 OR Estatus = 1)
                ORDER BY ClaveConcepto;";

            var tabla = new DataTable();

            using (var cn = new SqlConnection(ObtenerCn()))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@IdClase", SqlDbType.TinyInt).Value = idClase;
                cmd.Parameters.Add("@SoloActivos", SqlDbType.Bit).Value = soloActivos;

                cn.Open();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(tabla);
                }
            }

            return tabla;
        }

        private ConceptoCobroPagoData Mapear(IDataRecord r)
        {
            return new ConceptoCobroPagoData
            {
                IdConcepto = Convert.ToInt32(r["IdConcepto"]),
                ClaveConcepto = r["ClaveConcepto"].ToString(),
                Descripcion = r["Descripcion"].ToString(),
                IdClase = Convert.ToByte(r["IdClase"]),
                ClaseDescripcion = r["ClaseDescripcion"] as string,
                Estatus = Convert.ToBoolean(r["Estatus"]),
                RequiereAutorizacion = Convert.ToBoolean(r["RequiereAutorizacion"]),
                TipoAutorizacion = r["TipoAutorizacion"] == DBNull.Value ? (byte?)null : Convert.ToByte(r["TipoAutorizacion"]),
                Notas = r["Notas"] as string,
                FechaCreacion = Convert.ToDateTime(r["FechaCreacion"]),
                UsuarioCreacion = r["UsuarioCreacion"].ToString(),
                FechaModificacion = r["FechaModificacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["FechaModificacion"]),
                UsuarioModificacion = r["UsuarioModificacion"] as string
            };
        }
    }
}