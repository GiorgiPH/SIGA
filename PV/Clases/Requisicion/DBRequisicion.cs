using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PV.Clases.Requisicion
{
    /// <summary>
    /// Clase de acceso a datos para el módulo de requisiciones.
    /// </summary>
    public class DBRequisicion
    {
        private static string ObtenerCn() => Settings.Default.ControlCondominiosConnectionString;

        // ==================== ENCABEZADO ====================

        /// <summary>
        /// Obtiene el siguiente folio disponible para una nueva requisición.
        /// </summary>
        public int ObtenerSiguienteFolio()
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) + 1 FROM Requisicion", cn))
            {
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Inserta una nueva requisición en la base de datos.
        /// </summary>
        public void InsertarRequisicion(
            string folio,
            string claveDocumento,
            string estatus,
            string fecha,
            string diasVencen,
            string fechaVence,
            string centroCosto,
            string proyecto,
            string notas,
            string elaborado,
            string consecutivo)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Requisicion 
                    (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, 
                     CentroCosto, IdProyecto, Notas, Elaborado, Consecutivo, TotalPartidas)
                  VALUES 
                    (@Folio, @ClaveDocumento, @Estatus, @Fecha, @DiasVencen, @FechaVence,
                     @CentroCosto, @Proyecto, @Notas, @Elaborado, @Consecutivo, 0)", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cmd.Parameters.AddWithValue("@ClaveDocumento", claveDocumento);
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@DiasVencen", diasVencen);
                cmd.Parameters.AddWithValue("@FechaVence", fechaVence);
                cmd.Parameters.AddWithValue("@CentroCosto", centroCosto);
                cmd.Parameters.AddWithValue("@Notas", proyecto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Proyecto",
                    string.IsNullOrEmpty(proyecto) ? (object)DBNull.Value : proyecto); cmd.Parameters.AddWithValue("@Elaborado", elaborado);
                cmd.Parameters.AddWithValue("@Consecutivo", consecutivo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Actualiza el total de partidas de una requisición.
        /// </summary>
        public void ActualizarTotalPartidas(string folio, int totalPartidas)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Requisicion SET TotalPartidas = @TotalPartidas WHERE Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@TotalPartidas", totalPartidas);
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Actualiza el estatus de una requisición.
        /// </summary>
        public void ActualizarEstatus(string folio, string estatus)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Requisicion SET Estatus = @Estatus WHERE Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Estatus", estatus);
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Elimina una requisición (solo si no tiene partidas).
        /// </summary>
        public void EliminarRequisicion(string folio)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Requisicion WHERE Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Obtiene el consecutivo siguiente para un tipo de documento.
        /// </summary>
        public string ObtenerConsecutivo(string claveDocumento)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(Consecutivo), 0) + 1 FROM Requisicion WHERE ClaveDocumento = @ClaveDocumento", cn))
            {
                cmd.Parameters.AddWithValue("@ClaveDocumento", claveDocumento);
                cn.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        /// <summary>
        /// Obtiene información de un documento por su clave.
        /// </summary>
        public (string Nombre, string Clave, string MostrarCentroCosto) InformacionDocumento(string documentoCompleto)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Nombre, Clave, MostrarCentroCosto FROM Documento WHERE (Clave + ' - ' + Nombre) = @Documento", cn))
            {
                cmd.Parameters.AddWithValue("@Documento", documentoCompleto);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return (dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    }
                    return (null, null, null);
                }
            }
        }

        public (string Nombre, string Clave) InformacionDocumentoPorClave(string clave)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Nombre, Clave FROM Documento WHERE Clave = @Clave", cn))
            {
                cmd.Parameters.AddWithValue("@Clave", clave);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return (dr[0].ToString(), dr[1].ToString());
                    return (null, null);
                }
            }
        }

        /// <summary>
        /// Carga el encabezado de una requisición en los controles del formulario.
        /// </summary>
        public void CargarEncabezado(
            string folio,
            out string claveDocumento,
            out string estatus,
            out string fecha,
            out string diasVencen,
            out string fechaVence,
            out string centroCosto,
            out string proyecto,
            out string totalPartidas,
            out string notas,
            out string elaborado,
            out string consecutivo)
        {
            claveDocumento = estatus = fecha = diasVencen = fechaVence = centroCosto = proyecto = totalPartidas = notas = elaborado = consecutivo = string.Empty;

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Requisicion WHERE Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        claveDocumento = dr["ClaveDocumento"].ToString();
                        estatus = dr["Estatus"].ToString();
                        fecha = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                        diasVencen = dr["DiasVencen"].ToString();
                        fechaVence = Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy/MM/dd");
                        centroCosto = dr["CentroCosto"].ToString();
                        proyecto = dr["IdProyecto"].ToString();
                        totalPartidas = dr["TotalPartidas"].ToString();
                        notas = dr["Notas"].ToString();
                        elaborado = dr["Elaborado"].ToString();
                        consecutivo = dr["Consecutivo"].ToString();
                    }
                }
            }
        }

        // ==================== PARTIDAS ====================

        /// <summary>
        /// Obtiene el siguiente número de partida para una requisición.
        /// </summary>
        public int ObtenerSiguientePartida(string folioRequisicion)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(Partida), 0) + 1 FROM PartidaRequisicion WHERE FolioRequisicion = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folioRequisicion);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Inserta una nueva partida en la requisición.
        /// </summary>
        public void InsertarPartida(
            string folioRequisicion,
            int partida,
            string claveProducto,
            string concepto,
            string cantidad,
            string unidad)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO PartidaRequisicion 
                    (FolioRequisicion, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, CantidadRecibida)
                  VALUES 
                    (@Folio, @Partida, @ClaveProducto, @Concepto, @Cantidad, @Unidad, @Cantidad)", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folioRequisicion);
                cmd.Parameters.AddWithValue("@Partida", partida);
                cmd.Parameters.AddWithValue("@ClaveProducto", claveProducto);
                cmd.Parameters.AddWithValue("@Concepto", concepto);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Unidad", unidad);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Elimina una partida específica.
        /// </summary>
        public void EliminarPartida(string folioRequisicion, int partida)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM PartidaRequisicion WHERE FolioRequisicion = @Folio AND Partida = @Partida", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folioRequisicion);
                cmd.Parameters.AddWithValue("@Partida", partida);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Carga las partidas de una requisición en un DataGridView.
        /// </summary>
        public void CargarPartidas(DataGridView dgv, string folioRequisicion)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT PR.*, PS.Descripcion 
                  FROM PartidaRequisicion PR
                  INNER JOIN ProductosServicios PS ON PR.ClaveProducto = PS.ClaveProducto
                  WHERE PR.FolioRequisicion = @Folio
                  ORDER BY PR.Partida", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folioRequisicion);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = dr["FolioRequisicion"].ToString();
                        dgv.Rows[n].Cells[1].Value = dr["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = dr["Descripcion"].ToString();
                        dgv.Rows[n].Cells[3].Value = dr["Concepto2"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene los datos de una partida específica.
        /// </summary>
        public (string ClaveProducto, string Concepto, string Concepto2, string Cantidad, string Unidad, string Existencia)
            ObtenerPartida(string folioRequisicion, int partida)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT PR.ClaveProducto, PR.Concepto2, PS.Descripcion, PR.Cantidad, PR.Unidad, PS.ExActual
                  FROM PartidaRequisicion PR
                  INNER JOIN ProductosServicios PS ON PR.ClaveProducto = PS.ClaveProducto
                  WHERE PR.FolioRequisicion = @Folio AND PR.Partida = @Partida", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folioRequisicion);
                cmd.Parameters.AddWithValue("@Partida", partida);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return (
                            dr[0].ToString(),
                            dr[1].ToString(),
                            dr[2].ToString(),
                            dr[3].ToString(),
                            dr[4].ToString(),
                            dr[5].ToString()
                        );
                    }
                    return (null, null, null, null, null, null);
                }
            }
        }

        // ==================== CONSULTAS Y FILTROS ====================

        /// <summary>
        /// Carga todas las requisiciones en el DataGridView (consulta general).
        /// </summary>
        public void CargarRequisiciones(DataGridView dgv)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT R.*, P.Proyecto AS ProyectoNombre 
                  FROM Requisicion R
                  LEFT JOIN [DatosProyecto] P ON R.IdProyecto = P.Id", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = dr["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = dr["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = dr["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = dr["ProyectoNombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        /// <summary>
        /// Filtra requisiciones por consecutivo.
        /// </summary>
        public void FiltrarPorConsecutivo(DataGridView dgv, string filtro)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT R.*, P.Proyecto AS ProyectoNombre 
                  FROM Requisicion R
                  LEFT JOIN DatosProyecto P ON R.IdProyecto = P.Clave
                  WHERE R.Consecutivo LIKE @Filtro", cn))
            {
                cmd.Parameters.AddWithValue("@Filtro", $"%{filtro}%");
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = dr["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = dr["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = dr["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = dr["ProyectoNombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        /// <summary>
        /// Filtra requisiciones por clave de documento.
        /// </summary>
        public void FiltrarPorDocumento(DataGridView dgv, string filtro)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT R.*, P.Proyecto AS ProyectoNombre 
                  FROM Requisicion R
                  LEFT JOIN DatosProyecto P ON R.IdProyecto = P.Clave
                  WHERE R.ClaveDocumento LIKE @Filtro", cn))
            {
                cmd.Parameters.AddWithValue("@Filtro", $"%{filtro}%");
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = dr["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = dr["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = dr["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = dr["ProyectoNombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        /// <summary>
        /// Filtra requisiciones por nombre de proyecto.
        /// </summary>
        public void FiltrarPorProyecto(DataGridView dgv, string filtro)
        {
            dgv.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT R.*, P.Proyecto AS ProyectoNombre 
                  FROM Requisicion R
                  LEFT JOIN DatosProyecto P ON R.IdProyecto = P.Clave
                  WHERE P.Nombre LIKE @Filtro", cn))
            {
                cmd.Parameters.AddWithValue("@Filtro", $"%{filtro}%");
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = dr["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = dr["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[2].Value = dr["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = dr["ProyectoNombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        /// <summary>
        /// Llena un ComboBox con los documentos de tipo "Compra" y clase "Requisicion".
        /// </summary>
        public void LlenarComboDocumentos(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT (Clave + ' - ' + Nombre) AS Nombre FROM Documento WHERE TipoDocumento = 'Compra' AND Clase = 'Requisicion'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        cb.Items.Add(dr[0].ToString());
                }
            }
        }
    }
}