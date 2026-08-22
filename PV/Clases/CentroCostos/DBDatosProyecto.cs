using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace PV.Clases.CentroCostos
{
    internal class DBDatosProyecto
    {
        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }


        public void SeleccionarCentroCostos(ComboBox cb)
        {
            cb.Items.Clear();
            string consulta = "SELECT Nombre FROM CentroCostos ORDER BY Nombre";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(consulta, cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void SeleccionarUsuario(ComboBox cb)
        {
            cb.Items.Clear();
            string consulta = "SELECT Nombre FROM Usuarios ORDER BY Nombre";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(consulta, cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void SeleccionarFormaPAgo2(ComboBox cb)
        {
            cb.Items.Clear();
            string consulta = "SELECT Descripcion FROM FormasPago ORDER BY Descripcion";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(consulta, cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void SeleccionarCatConceptosGlobales(ComboBox cb)
        {
            cb.Items.Clear();
            string consulta = "SELECT Clave FROM ConceptosGlobales ORDER BY Clave";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(consulta, cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void ConsultConceptoSeleccionado(string Clave, Guna2TextBox Importe)
        {
            try
            {
                string consulta = "SELECT Importe FROM ConceptosGlobales WHERE Clave = @Clave";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = Clave;
                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    Importe.Text = result?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void SeleccionarFormaPAgo2(Guna2TextBox clave, string formapago)
        {
            try
            {
                string consulta = "SELECT ClaveFormasPago FROM FormasPago WHERE Descripcion = @FormaPago";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@FormaPago", SqlDbType.VarChar, 100).Value = formapago;
                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    clave.Text = result?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ConsultaFolio(Guna2TextBox Folio)
        {
            try
            {
                string consulta = @"
                    SELECT ISNULL((SELECT TOP 1 Folio FROM DatosProyecto ORDER BY Folio DESC), 0) + 1 AS NuevoFolio";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    Folio.Text = result?.ToString() ?? "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener folio: " + ex.Message);
            }
        }

        public string RegistroFormaPagoproyecto(string Id, string Folio, string txtclave, string txtformapago,
            string txtreferencia, string txtdescripcionR)
        {
            try
            {
                // Verificar si existe el registro
                string countQuery = "SELECT COUNT(1) FROM FormaPagoProyecto WHERE Id = @Id";
                int count;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(countQuery, cn))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = Id;
                    cn.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (count == 0)
                {
                    string insertQuery = @"
                        INSERT INTO FormaPagoProyecto 
                            (Folio, IdFormaPago, DescripcionFormaPago, Referencia, DescripcionReferencia) 
                        VALUES 
                            (@Folio, @IdFormaPago, @DescripcionFormaPago, @Referencia, @DescripcionReferencia)";
                    using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                    using (SqlCommand cmd = new SqlCommand(insertQuery, cn))
                    {
                        cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 20).Value = Folio;
                        cmd.Parameters.Add("@IdFormaPago", SqlDbType.VarChar, 20).Value = txtclave;
                        cmd.Parameters.Add("@DescripcionFormaPago", SqlDbType.VarChar, 100).Value = txtformapago;
                        cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 100).Value = txtreferencia;
                        cmd.Parameters.Add("@DescripcionReferencia", SqlDbType.VarChar, 200).Value = txtdescripcionR;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return "Registro guardado.";
                }
                else
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de Formas de Pago",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string updateQuery = @"
                            UPDATE FormaPagoProyecto 
                            SET IdFormaPago = @IdFormaPago,
                                DescripcionFormaPago = @DescripcionFormaPago,
                                Referencia = @Referencia,
                                DescripcionReferencia = @DescripcionReferencia
                            WHERE Id = @Id";
                        using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                        using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                        {
                            cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = Id;
                            cmd.Parameters.Add("@IdFormaPago", SqlDbType.VarChar, 20).Value = txtclave;
                            cmd.Parameters.Add("@DescripcionFormaPago", SqlDbType.VarChar, 100).Value = txtformapago;
                            cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 100).Value = txtreferencia;
                            cmd.Parameters.Add("@DescripcionReferencia", SqlDbType.VarChar, 200).Value = txtdescripcionR;
                            cn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        return "Registro modificado.";
                    }
                    return "Operación cancelada.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return "Error al guardar.";
            }
        }

        public DataTable ObtenerFormasPagoProyectoo(string folio)
        {
            try
            {
                string consulta = "SELECT * FROM FormaPagoProyecto WHERE Folio = @Folio";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 20).Value = folio;
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }

        public string RegistroDatosproyecto(string folio, string centrocosto, string proyecto, string Caracteristicas,
     string Area, string Encargado, string iva, string CuentaContable)
        {
            try
            {
                // Si no viene folio, es un proyecto nuevo -> INSERT
                // Si viene folio, es un proyecto existente -> UPDATE (identificado por Folio + CentroCostos)
                if (string.IsNullOrWhiteSpace(folio))
                {
                    using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                    {
                        cn.Open();
                        using (SqlTransaction tx = cn.BeginTransaction())
                        {
                            try
                            {
                                // Calcula el siguiente folio SOLO para ese centro de costos.
                                // UPDLOCK+HOLDLOCK evita que dos inserciones simultáneas
                                // obtengan el mismo folio (condición de carrera).
                                string folioQuery = @"
                            SELECT ISNULL(MAX(Folio), 0) + 1
                            FROM DatosProyecto WITH (UPDLOCK, HOLDLOCK)
                            WHERE CentroCostos = @CentroCostos and Estatus=1";
                                int nuevoFolio;
                                using (SqlCommand cmdFolio = new SqlCommand(folioQuery, cn, tx))
                                {
                                    cmdFolio.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = centrocosto;
                                    nuevoFolio = Convert.ToInt32(cmdFolio.ExecuteScalar());
                                }

                                string insertQuery = @"
                            INSERT INTO DatosProyecto 
                                (Folio, CentroCostos, Proyecto, Caracteristicas, Area, Encargado, Iva, CuentaContable) 
                            VALUES 
                                (@Folio, @CentroCostos, @Proyecto, @Caracteristicas, @Area, @Encargado, @Iva, @CuentaContable)";
                                using (SqlCommand cmdInsert = new SqlCommand(insertQuery, cn, tx))
                                {
                                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = nuevoFolio;
                                    cmdInsert.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = centrocosto;
                                    cmdInsert.Parameters.Add("@Proyecto", SqlDbType.VarChar, 100).Value = proyecto;
                                    cmdInsert.Parameters.Add("@Caracteristicas", SqlDbType.VarChar, 500).Value = Caracteristicas;
                                    cmdInsert.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = Area;
                                    cmdInsert.Parameters.Add("@Encargado", SqlDbType.VarChar, 100).Value = Encargado;
                                    cmdInsert.Parameters.Add("@Iva", SqlDbType.VarChar, 20).Value = iva;
                                    cmdInsert.Parameters.Add("@CuentaContable", SqlDbType.VarChar, 50).Value = CuentaContable;
                                    cmdInsert.ExecuteNonQuery();
                                }

                                tx.Commit();
                            }
                            catch
                            {
                                tx.Rollback();
                                throw;
                            }
                        }
                    }
                    return "Registro guardado.";
                }
                else
                {
                    // UPDATE - el proyecto ya existe, se localiza por Folio + CentroCostos (combinación única)
                    string updateQuery = @"
                UPDATE DatosProyecto 
                SET Proyecto = @Proyecto,
                    Caracteristicas = @Caracteristicas,
                    Area = @Area,
                    Encargado = @Encargado,
                    Iva = @Iva,
                    CuentaContable = @CuentaContable
                WHERE Folio = @Folio AND CentroCostos = @CentroCostos";
                    using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                    using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                    {
                        cmd.Parameters.Add("@Folio", SqlDbType.Int).Value = Convert.ToInt32(folio);
                        cmd.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = centrocosto;
                        cmd.Parameters.Add("@Proyecto", SqlDbType.VarChar, 100).Value = proyecto;
                        cmd.Parameters.Add("@Caracteristicas", SqlDbType.VarChar, 500).Value = Caracteristicas;
                        cmd.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = Area;
                        cmd.Parameters.Add("@Encargado", SqlDbType.VarChar, 100).Value = Encargado;
                        cmd.Parameters.Add("@Iva", SqlDbType.VarChar, 20).Value = iva;
                        cmd.Parameters.Add("@CuentaContable", SqlDbType.VarChar, 50).Value = CuentaContable;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    return "Registro modificado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return "Error al guardar.";
            }
        }

        public void ConsultaproyectoSeleccionada(string centrocostos, string proyecto, string folio,
            Guna2TextBox txtproyecto, Guna2TextBox txtcaracteristicas, Guna2TextBox Area,
            Guna2TextBox Encargado, Guna2TextBox idformapago, Guna2TextBox descripcionformapago,
            Guna2TextBox RBreferencia, Guna2TextBox descripcionReferencia, Guna2TextBox IVA,
            Guna2TextBox Cuentacontable)
        {
            try
            {
                string consulta = @"
                    SELECT 
                        D.Proyecto,
                        D.Caracteristicas,
                        D.Area,
                        D.Encargado,
                        F.IdFormaPago,
                        F.DescripcionFormaPago,
                        F.Referencia,
                        F.DescripcionReferencia,
                        D.Iva,
                        D.CuentaContable
                    FROM DatosProyecto D
                    LEFT JOIN FormaPagoProyecto F ON D.Folio = F.Folio
                    WHERE 
                       D.Id = @Folio";

                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
       
                    cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 20).Value = folio;
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtproyecto.Text = row["Proyecto"]?.ToString() ?? string.Empty;
                    txtcaracteristicas.Text = row["Caracteristicas"]?.ToString() ?? string.Empty;
                    Area.Text = row["Area"]?.ToString() ?? string.Empty;
                    Encargado.Text = row["Encargado"]?.ToString() ?? string.Empty;
                    idformapago.Text = row["IdFormaPago"]?.ToString() ?? string.Empty;
                    descripcionformapago.Text = row["DescripcionFormaPago"]?.ToString() ?? string.Empty;
                    RBreferencia.Text = row["Referencia"]?.ToString() ?? string.Empty;
                    descripcionReferencia.Text = row["DescripcionReferencia"]?.ToString() ?? string.Empty;
                    IVA.Text = row["Iva"]?.ToString() ?? string.Empty;
                    Cuentacontable.Text = row["CuentaContable"]?.ToString() ?? string.Empty;
                }
                else
                {
                    // Limpiar controles si no hay datos
                    txtproyecto.Text = string.Empty;
                    txtcaracteristicas.Text = string.Empty;
                    Area.Text = string.Empty;
                    Encargado.Text = string.Empty;
                    idformapago.Text = string.Empty;
                    descripcionformapago.Text = string.Empty;
                    RBreferencia.Text = string.Empty;
                    descripcionReferencia.Text = string.Empty;
                    IVA.Text = string.Empty;
                    Cuentacontable.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar proyecto: " + ex.Message);
            }
        }

        public void Cargardatosproyectos(DataGridView dgv, string centrocostos)
        {
            try
            {
                dgv.Rows.Clear();
                string consulta = "SELECT Proyecto, Folio FROM DatosProyecto WHERE CentroCostos = @CentroCostos";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = centrocostos;
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = row["Proyecto"].ToString();
                    dgv.Rows[n].Cells[2].Value = row["Folio"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proyectos: " + ex.Message);
            }
        }

        public DataTable CargarDatosProyectos(string centrocostos)
        {
            try
            {
                string consulta = "SELECT Id,Proyecto, Folio FROM DatosProyecto WHERE CentroCostos = @CentroCostos AND Estatus = 1 ORDER BY Proyecto";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = centrocostos;
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new DataTable();
            }
        }

        public bool ActualizarEstatusProyecto(string folio)
        {
            try
            {
                string updateQuery = "UPDATE DatosProyecto SET Estatus = 0 WHERE Id = @Folio";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                {
                    cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 20).Value = folio;
                    cn.Open();
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar estatus: " + ex.Message);
                return false;
            }
        }

        public DataTable ObtenerProyectosPorCentroCostos(string centrocostos)
        {
            try
            {
                string consulta = @"
                    SELECT Id, Proyecto 
                    FROM DatosProyecto 
                    WHERE CentroCostos = @CentroCostos 
                    ORDER BY Proyecto";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.Add("@CentroCostos", SqlDbType.VarChar, 50).Value = (object)centrocostos ?? DBNull.Value;
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new DataTable();
            }
        }
    }
}