using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.Remision
{
    /// <summary>
    /// Capa de datos de Remision. Mismas firmas, parámetros y lógica que la
    /// clase original.
    ///
    /// NOTA sobre el refactor de conexiones: el constructor original abría
    /// una SqlConnection y la dejaba abierta durante toda la vida del
    /// objeto (nunca se cerraba). Ahora cada método abre su propia conexión
    /// dentro de un "using", igual que en el resto de las clases DB del
    /// proyecto (DBFacturas, DBProductosServicios, DBRegistrarIngresos).
    /// También se parametrizaron las consultas que antes concatenaban texto
    /// directamente (Folio, Matricula) para evitar inyección SQL.
    /// </summary>
    public class DBRemiision
    {
        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public void ConsultaTotalRemision(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from Remision where Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Total"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Carga las Remisiones con saldo pendiente (Saldo != 0) de un
        /// cliente. Columnas por índice: 1=ClaveDocumento, 2=Folio,
        /// 3=Consecutivo, 5=Nombre, 6=Fecha, 7=Total, 8=Saldo, 9=FechaVence
        /// (igual que la versión original).
        ///
        /// Cada fila agregada queda marcada en su .Tag con "Remision", para
        /// poder distinguirla si el grid se combina con Facturas
        /// (DBFacturas.CargarFacturaCobro) — Factura.Folio y Remision.Folio
        /// son secuencias independientes y pueden coincidir.
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Si vas a
        /// combinar con CargarFacturaCobro en el mismo grid, limpia una
        /// sola vez desde el formulario y llama a ambos con
        /// limpiarPrimero=false.
        /// </param>
        public void CargarRemisionCobro(DataGridView dgv, string Matricula, bool limpiarPrimero = true)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                if (limpiarPrimero)
                {
                    dgv.Rows.Clear();
                }

                const string sql = @"
                    SELECT R.*, D.Nombre
                    FROM Remision AS R, Documento AS D
                    WHERE ClaveProveedor = @Matricula AND Saldo <> 0 AND R.ClaveDocumento = D.Clave";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@Matricula", Matricula);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        decimal Total = Convert.ToDecimal(item["Total"]);

                        dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Consecutivo"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[7].Value = Total;
                        dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[9].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");

                        dgv.Rows[n].Tag = "Remision";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        /// <summary>
        /// Carga en la grilla de RegistrarCobro los folios de Remision
        /// seleccionados en registroIngresos. Cada fila agregada queda
        /// marcada con .Tag = "Remision" (ver la nota equivalente en
        /// DBFacturas.CargarFacturaCobroById sobre por qué es necesario).
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Si vas a
        /// combinar con DBFacturas.CargarFacturaCobroById en el mismo grid,
        /// limpia una sola vez desde el formulario y llama a ambos con
        /// limpiarPrimero=false.
        /// </param>
        public void CargarReciboCobroById(DataGridView dgv, string Matricula, ArrayList ListaConcep, bool limpiarPrimero = true)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                if (limpiarPrimero)
                {
                    dgv.Rows.Clear();
                }

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    foreach (object item2 in ListaConcep)
                    {
                        string concepto = item2.ToString();

                        const string sql = @"
                            SELECT R.*, 0.00 AS DescuentoPago, 0.00 AS RecargosAcumulados, D.Nombre
                            FROM Remision AS R, Documento AS D
                            WHERE ClaveProveedor = @Matricula AND R.Folio = @Folio AND R.ClaveDocumento = D.Clave";

                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.AddWithValue("@Matricula", Matricula);
                            cmd.Parameters.AddWithValue("@Folio", concepto);

                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow item in dt.Rows)
                            {
                                int n = dgv.Rows.Add();
                                dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                                dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                                dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                                dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                                dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);

                                dgv.Rows[n].Tag = "Remision";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        public void ActualizarRemision(string Folio, decimal Recargos, decimal Descuento, decimal Saldo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(
                    "Update Remision set DescuentoPago = @Descuento, Saldo = Saldo - @Saldo where Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Descuento", Descuento);
                    cmd.Parameters.AddWithValue("@Saldo", Saldo);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public string[] InformacionRemision(string Orden)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(
                "select Folio, ClaveDocumento, Fecha, ClaveProveedor, Consecutivo from Remision as OC where Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", Orden);
                cn.Open();

                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr["Folio"].ToString(),
                            dr["ClaveDocumento"].ToString(),
                            dr["Fecha"].ToString(),
                            dr["ClaveProveedor"].ToString(),
                            dr["Consecutivo"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }
    }
}