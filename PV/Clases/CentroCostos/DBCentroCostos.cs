using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace Condominios.Clases.CentroCostos
{
    class DBCentroCostos
    {
        // Se eliminan los campos de conexión a nivel de instancia.
        // Se mantienen los estáticos para compatibilidad.
        public static int Folio = 0;
        public static int Eliminado = 0;

        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

      

        public int ClaveCentroSiguiente()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(Clave), 0) FROM CentroCostos";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        Folio = Convert.ToInt32(result);
                        return 1; // indica que hay datos
                    }
                    return 0; // sin datos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener clave: " + ex.Message);
                return 0;
            }
        }

        public string RegistroCentroCostos(string txtClave, string txtNombre, string cmbEstatus,
            string txtCuentacontable, string txtDescricpion,
            ArrayList ListaConceptos, ArrayList ListaConceptos2)
        {
            string mensaje = string.Empty;

            try
            {
                // Verificar existencia
                string countQuery = "SELECT COUNT(1) FROM CentroCostos WHERE Clave = @Clave";
                int count;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(countQuery, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClave;
                    cn.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (count == 0)
                {
                    // Insertar
                    string insertQuery = @"
                        INSERT INTO CentroCostos (Clave, Nombre, Estatus, CuentaContable, Descripcion)
                        VALUES (@Clave, @Nombre, @Estatus, @CuentaContable, @Descripcion)";
                    using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                    using (SqlCommand cmd = new SqlCommand(insertQuery, cn))
                    {
                        cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClave;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = txtNombre;
                        cmd.Parameters.Add("@Estatus", SqlDbType.VarChar, 20).Value = cmbEstatus;
                        cmd.Parameters.Add("@CuentaContable", SqlDbType.VarChar, 50).Value = txtCuentacontable;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = txtDescricpion;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // (Código comentado de departamentos - se mantiene como estaba)
                    // foreach (object item in ListaConceptos) { ... }

                    mensaje = "Registro guardado.";
                }
                else
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Centros de Costos",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Actualizar centro de costos
                        string updateQuery = @"
                            UPDATE CentroCostos 
                            SET Nombre = @Nombre,
                                Estatus = @Estatus,
                                CuentaContable = @CuentaContable,
                                Descripcion = @Descripcion
                            WHERE Clave = @Clave";
                        using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                        using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                        {
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClave;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = txtNombre;
                            cmd.Parameters.Add("@Estatus", SqlDbType.VarChar, 20).Value = cmbEstatus;
                            cmd.Parameters.Add("@CuentaContable", SqlDbType.VarChar, 50).Value = txtCuentacontable;
                            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = txtDescricpion;
                            cn.Open();
                            cmd.ExecuteNonQuery();
                        }

                        // Procesar departamentos (si las listas tienen elementos)
                        // Se usa un bucle for para evitar modificar ListaConceptos2 mientras se itera.
                        int minCount = Math.Min(ListaConceptos.Count, ListaConceptos2.Count);
                        for (int i = 0; i < minCount; i++)
                        {
                            string claveDep = ListaConceptos[i].ToString();
                            string nombreDep = ListaConceptos2[i].ToString();

                            // Eliminar si no existe en Departamentos_SubDepartamento
                            string deleteIfNotExists = @"
                                IF NOT EXISTS (SELECT 1 FROM Departamentos_SubDepartamento WHERE Departamento = @ClaveDep)
                                    DELETE FROM CentroCostos_Departamentos WHERE Clave = @ClaveDep";
                            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                            using (SqlCommand cmd = new SqlCommand(deleteIfNotExists, cn))
                            {
                                cmd.Parameters.Add("@ClaveDep", SqlDbType.VarChar, 50).Value = claveDep;
                                cn.Open();
                                cmd.ExecuteNonQuery();
                            }

                            // Insertar si no existe en CentroCostos_Departamentos
                            string insertIfNotExists = @"
                                IF NOT EXISTS (SELECT 1 FROM CentroCostos_Departamentos WHERE Clave = @ClaveDep)
                                    INSERT INTO CentroCostos_Departamentos (Clave, Nombre, CentroCosto)
                                    VALUES (@ClaveDep, @NombreDep, @CentroCosto)";
                            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                            using (SqlCommand cmd = new SqlCommand(insertIfNotExists, cn))
                            {
                                cmd.Parameters.Add("@ClaveDep", SqlDbType.VarChar, 50).Value = claveDep;
                                cmd.Parameters.Add("@NombreDep", SqlDbType.VarChar, 100).Value = nombreDep;
                                cmd.Parameters.Add("@CentroCosto", SqlDbType.VarChar, 50).Value = txtClave;
                                cn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                mensaje = "Error al guardar.";
            }
            return mensaje;
        }

        public void CargarCentros(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                string query = "SELECT Clave, Nombre FROM CentroCostos ORDER BY Clave";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                foreach (DataRow row in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = row["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = row["Nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar centros: " + ex.Message);
            }
        }

        public void ConsultaCentrosSeleccionada(string txtclave, Guna2TextBox txtNombre,
            ComboBox cmbEstatus, Guna2TextBox txtCuenta, Guna2TextBox txtDescripcion,
            DataGridView dgv)
        {
            try
            {
                string query = "SELECT Nombre, Estatus, CuentaContable, Descripcion FROM CentroCostos WHERE Clave = @Clave";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtclave;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtNombre.Text = dr["Nombre"]?.ToString() ?? string.Empty;
                            cmbEstatus.Text = dr["Estatus"]?.ToString() ?? string.Empty;
                            txtCuenta.Text = dr["CuentaContable"]?.ToString() ?? string.Empty;
                            txtDescripcion.Text = dr["Descripcion"]?.ToString() ?? string.Empty;
                        }
                        dr.Close();
                    }
                }

                // Código comentado para cargar departamentos (se mantiene como estaba)
                // if (dgv != null) { ... }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar centro: " + ex.Message);
            }
        }

        public int ConsultaExistencia(string txtclave)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM CentroCostos_Departamentos WHERE Clave = @Clave";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtclave;
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return 0;
            }
        }

        public void ConsultaSubDepartamentoSeleccionada(string txtclave, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                string query = @"
                    SELECT Clave, Nombre
                    FROM Departamentos_SubDepartamento
                    WHERE Departamento = @Departamento
                    ORDER BY CONVERT(INT, SUBSTRING(Clave, 5, 1000)) ASC";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Departamento", SqlDbType.VarChar, 50).Value = txtclave;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = row["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = row["Nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar subdepartamentos: " + ex.Message);
            }
        }

        public string EliminarDepartamento(string txtClave)
        {
            try
            {
                // Verificar si tiene subdepartamentos
                string countQuery = "SELECT COUNT(1) FROM Departamentos_SubDepartamento WHERE Departamento = @Departamento";
                int count;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(countQuery, cn))
                {
                    cmd.Parameters.Add("@Departamento", SqlDbType.VarChar, 50).Value = txtClave;
                    cn.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (count == 0)
                {
                    string deleteQuery = "DELETE FROM CentroCostos_Departamentos WHERE Clave = @Clave";
                    using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, cn))
                    {
                        cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClave;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    Eliminado = 0;
                    return "Eliminado";
                }
                else
                {
                    Eliminado = 1;
                    return "Elimine los SubDepartamentos para continuar";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar departamento: " + ex.Message);
                Eliminado = 1;
                return "Error";
            }
        }

        public void EliminarSubDepartamento(string txtClave)
        {
            try
            {
                string deleteQuery = "DELETE FROM Departamentos_SubDepartamento WHERE Clave = @Clave";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(deleteQuery, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClave;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar subdepartamento: " + ex.Message);
            }
        }

        public string RegistroSubDepartamento(string Departamento, ArrayList ListaConceptos, ArrayList ListaConceptos2)
        {
            string mensaje = string.Empty;

            try
            {
                // Verificar si ya existen registros para el departamento
                string countQuery = "SELECT COUNT(1) FROM Departamentos_SubDepartamento WHERE Departamento = @Departamento";
                int count;
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(countQuery, cn))
                {
                    cmd.Parameters.Add("@Departamento", SqlDbType.VarChar, 50).Value = Departamento;
                    cn.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (count == 0)
                {
                    // Insertar todos los pares
                    int minCount = Math.Min(ListaConceptos.Count, ListaConceptos2.Count);
                    for (int i = 0; i < minCount; i++)
                    {
                        string claveDep = ListaConceptos[i].ToString();
                        string nombreDep = ListaConceptos2[i].ToString();

                        string insertQuery = @"
                            INSERT INTO Departamentos_SubDepartamento (Clave, Nombre, Departamento)
                            VALUES (@Clave, @Nombre, @Departamento)";
                        using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                        using (SqlCommand cmd = new SqlCommand(insertQuery, cn))
                        {
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = claveDep;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreDep;
                            cmd.Parameters.Add("@Departamento", SqlDbType.VarChar, 50).Value = Departamento;
                            cn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    mensaje = "Registro guardado.";
                }
                else
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Centro de Costos",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Actualizar cada par
                        int minCount = Math.Min(ListaConceptos.Count, ListaConceptos2.Count);
                        for (int i = 0; i < minCount; i++)
                        {
                            string claveDep = ListaConceptos[i].ToString();
                            string nombreDep = ListaConceptos2[i].ToString();

                            string updateQuery = @"
                                UPDATE Departamentos_SubDepartamento
                                SET Nombre = @Nombre,
                                    Departamento = @Departamento
                                WHERE Clave = @Clave";
                            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                            using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                            {
                                cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = claveDep;
                                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombreDep;
                                cmd.Parameters.Add("@Departamento", SqlDbType.VarChar, 50).Value = Departamento;
                                cn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                mensaje = "Error al guardar.";
            }
            return mensaje;
        }

        public string EliminarDivisa(string txtClaveDivisa)
        {
            try
            {
                // Eliminar dependencias
                string deleteDep = "DELETE FROM CentroCostos_Departamentos WHERE CentroCosto = @CentroCosto";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(deleteDep, cn))
                {
                    cmd.Parameters.Add("@CentroCosto", SqlDbType.VarChar, 50).Value = txtClaveDivisa;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Eliminar centro de costos
                string deleteCentro = "DELETE FROM CentroCostos WHERE Clave = @Clave";
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(deleteCentro, cn))
                {
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = txtClaveDivisa;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                return "Registro Eliminado";
            }
            catch (Exception)
            {
                return "El registro esta en uso, no es posible eliminar";
            }
        }

        public DataTable ConsultarTodos()
        {
            string query = "SELECT * FROM CentroCostos ORDER BY Clave";
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}