using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.MovimientosBanco
{
    /// <summary>
    /// Acceso a datos para el registro de Movimientos a Bancos (Ingresos/Egresos
    /// directos a una cuenta bancaria: comisiones, intereses, depósitos,
    /// retiros, traspasos, etc.)
    ///
    ///
    /// Mapeo Tipo de movimiento -> Clase (por Clave, no por Id, para no
    /// depender de que los IdClase sean iguales en todos los ambientes):
    ///   Egreso  -> Clave IN ('CARGO', 'TRASPASO', 'OTRO')
    ///   Ingreso -> Clave IN ('ABONO', 'TRASPASO', 'OTRO')
    /// Si el criterio de negocio cambia, el único lugar que hay que tocar
    /// es el WHERE de ObtenerConceptosPorTipo.
    /// </summary>
    class DBMovimientosBanco
    {
        public const string TipoIngreso = "I";
        public const string TipoEgreso = "E";

        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        private static SqlConnection AbrirConexion()
        {
            var cn = new SqlConnection(ObtenerCn());
            cn.Open();
            return cn;
        }

        //____________________________________________________________________
        /// <summary>
        /// Devuelve los conceptos activos de ConceptoCobroPago aplicables al
        /// tipo de movimiento bancario seleccionado ('I' o 'E'), filtrando
        /// por la Clave de ClaseConceptoTesoreria (CARGO/ABONO/TRASPASO/OTRO).
        /// </summary>
        public DataTable ObtenerConceptosPorTipo(string tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"
                    SELECT CC.IdConcepto, CC.ClaveConcepto, CC.Descripcion
                    FROM ConceptoCobroPago CC
                        INNER JOIN ClaseConceptoTesoreria CL ON CL.IdClase = CC.IdClase
                    WHERE CC.Estatus = 1
                      AND CL.Activo = 1
                      AND (
                            (@Tipo = 'E' AND CL.IdClase = 3)
                         OR (@Tipo = 'I' AND CL.IdClase = 2)
                          )
                    ORDER BY CC.Descripcion";

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener conceptos de movimiento: " + ex.Message);
            }
            return dt;
        }

        //____________________________________________________________________
        /// <summary>
        /// Inserta un nuevo movimiento bancario. Devuelve true si se insertó
        /// correctamente.
        /// </summary>
        public bool InsertarMovimiento(DateTime fecha, int claveCuentaBancaria, string tipo,
            int idConcepto, decimal importe, string descripcion, string elaborado)
        {
            bool ok = false;
            try
            {
                string sql = @"
                    INSERT INTO MovimientoBanco
                        (Fecha, ClaveCuentaBancaria, Tipo, IdConcepto, Importe, Descripcion, Elaborado)
                    VALUES
                        (@Fecha, @ClaveCuentaBancaria, @Tipo, @IdConcepto, @Importe, @Descripcion, @Elaborado)";

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                    cmd.Parameters.AddWithValue("@ClaveCuentaBancaria", claveCuentaBancaria);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@IdConcepto", idConcepto);
                    cmd.Parameters.AddWithValue("@Importe", importe);
                    cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Elaborado", (object)elaborado ?? DBNull.Value);

                    int filas = cmd.ExecuteNonQuery();
                    ok = filas > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el movimiento: " + ex.Message);
            }
            return ok;
        }

        //____________________________________________________________________
        /// <summary>
        /// Devuelve los últimos "top" movimientos bancarios registrados
        /// (opcionalmente filtrados por cuenta bancaria), para alimentar el
        /// grid de "Movimientos Recientes".
        /// </summary>
        public DataTable ObtenerMovimientosRecientes(int top = 10, int? claveCuentaBancaria = null)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"
                    SELECT TOP (@Top)
                        M.IdMovimiento,
                        M.Fecha,
                        C.Nombre        AS Cuenta,
                        CC.Descripcion  AS Concepto,
                        M.Descripcion,
                        M.Tipo,
                        M.Importe
                    FROM MovimientoBanco M
                        INNER JOIN CuentasBancarias C   ON C.Clave = M.ClaveCuentaBancaria
                        INNER JOIN ConceptoCobroPago CC ON CC.IdConcepto = M.IdConcepto
                    WHERE (@ClaveCuenta IS NULL OR M.ClaveCuentaBancaria = @ClaveCuenta)
                    ORDER BY M.FechaRegistro DESC";

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Top", top);
                    cmd.Parameters.AddWithValue("@ClaveCuenta", (object)claveCuentaBancaria ?? DBNull.Value);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar movimientos recientes: " + ex.Message);
            }
            return dt;
        }
    }
}