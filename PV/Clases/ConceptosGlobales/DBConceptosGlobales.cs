using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace Condominios.Clases.ConceptosGlobales
{
    class DBConceptosGlobales
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBConceptosGlobales()
        {
            try
            {
                cn = new SqlConnection(ObtenerCn());
                cn.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string RegistrConcepto(string Clave, string Nombre, string Clase, string Tipo, string Relativo, string Cuenta, decimal Importe, int Iva, string depende)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                // 1. Verificar si ya existe
                using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM ConceptosGlobales WHERE Clave = @Clave AND Nombre = @Nombre", cn))
                {
                    cmdCheck.Parameters.AddWithValue("@Clave", Clave);
                    cmdCheck.Parameters.AddWithValue("@Nombre", Nombre);
                    contador = (int)cmdCheck.ExecuteScalar();
                }

                if (contador == 0)
                {
                    // 2. Insertar nuevo
                    using (SqlCommand cmdInsert = new SqlCommand(
                        @"INSERT INTO ConceptosGlobales 
                  (Clave, Nombre, Clase, Tipo, Relativo, Cuenta, Importe, IncluyeIva, DependeDe)
                  VALUES 
                  (@Clave, @Nombre, @Clase, @Tipo, @Relativo, @Cuenta, @Importe, @IncluyeIva, @DependeDe)", cn))
                    {
                        cmdInsert.Parameters.AddWithValue("@Clave", Clave);
                        cmdInsert.Parameters.AddWithValue("@Nombre", Nombre);
                        cmdInsert.Parameters.AddWithValue("@Clase", Clase);
                        cmdInsert.Parameters.AddWithValue("@Tipo", Tipo);
                        cmdInsert.Parameters.AddWithValue("@Relativo", Relativo);
                        cmdInsert.Parameters.AddWithValue("@Cuenta", Cuenta);
                        cmdInsert.Parameters.AddWithValue("@Importe", Importe);
                        cmdInsert.Parameters.AddWithValue("@IncluyeIva", Iva);
                        cmdInsert.Parameters.AddWithValue("@DependeDe", string.IsNullOrWhiteSpace(depende) ? (object)DBNull.Value : depende);

                        cmdInsert.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                }
                else
                {
                    // 3. Confirmar modificación
                    if (MessageBox.Show("El concepto ya existe. Si continúa, será modificado.", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlCommand cmdUpdate = new SqlCommand(
                            @"UPDATE ConceptosGlobales 
                      SET Clase = @Clase, Tipo = @Tipo, Relativo = @Relativo, Cuenta = @Cuenta, 
                          Importe = @Importe, IncluyeIva = @IncluyeIva, DependeDe = @DependeDe 
                      WHERE Clave = @Clave AND Nombre = @Nombre", cn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@Clase", Clase);
                            cmdUpdate.Parameters.AddWithValue("@Tipo", Tipo);
                            cmdUpdate.Parameters.AddWithValue("@Relativo", Relativo);
                            cmdUpdate.Parameters.AddWithValue("@Cuenta", Cuenta);
                            cmdUpdate.Parameters.AddWithValue("@Importe", Importe);
                            cmdUpdate.Parameters.AddWithValue("@IncluyeIva", Iva);
                            cmdUpdate.Parameters.AddWithValue("@DependeDe", string.IsNullOrWhiteSpace(depende) ? (object)DBNull.Value : depende);
                            cmdUpdate.Parameters.AddWithValue("@Clave", Clave);
                            cmdUpdate.Parameters.AddWithValue("@Nombre", Nombre);

                            cmdUpdate.ExecuteNonQuery();
                            mensaje = "Registro modificado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return mensaje;
        }

        //________________________________________________________________________________________________
        //Documentos Registrados
        public void CargarConceptos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptosGlobales", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                   
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clase"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        public DataTable CargarConceptos()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT * FROM ConceptosGlobales", cn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                // Puedes manejar mejor el error o lanzarlo para la capa superior
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }
        public string[] InformacionReciboConceptoGlobal(string recibo)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ConceptosGlobales WHERE Clave = @recibo", cn))
            {
                cmd.Parameters.AddWithValue("@recibo", recibo);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new string[]
                        {
                    dr[0]?.ToString(),
                    dr[1]?.ToString(),
                    dr[2]?.ToString(),
                    dr[3]?.ToString(),
                    dr[4]?.ToString(),
                    dr[5]?.ToString(),
                    dr[6]?.ToString(),
                    dr[7]?.ToString()
                    //, dr[8]?.ToString(),
                        };
                    }
                }
            }

            return null; // o string[0] si prefieres evitar null
        }

        public DataTable CargarConceptosDescuento()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT * FROM ConceptosGlobales WHERE Clase = 'Descuento'", cn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                // Puedes manejar mejor el error o lanzarlo para la capa superior
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }
        public DataTable CargarConceptosImpuesto()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT * FROM ConceptosGlobales WHERE Clase = 'Impuesto'", cn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                // Puedes manejar mejor el error o lanzarlo para la capa superior
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultConceptoSeleccionado(string Clave, string Nombre, ComboBox Clase, ComboBox Tipo, ComboBox Relativa, Guna2TextBox Cuenta, Guna2TextBox Importe, Guna2ToggleSwitch IncluyeiVA, ComboBox cmbDepende)
        {
            try
            {
                cmd = new SqlCommand("Select * from ConceptosGlobales where Clave='" + Clave + "' and Nombre= '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    
                    Clase.Text = dr["Clase"].ToString();
                    Tipo.Text = dr["Tipo"].ToString();
                    Relativa.Text = dr["RelativO"].ToString();
                    Cuenta.Text = dr["Cuenta"].ToString();
                    Importe.Text = dr["Importe"].ToString();
                    IncluyeiVA.Checked = Convert.ToBoolean(dr["IncluyeIva"]);
                    cmbDepende.SelectedValue= dr["DependeDe"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________________________________
        public void Monto(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //_____________________________________________________________________________________________________-
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete ConceptosGlobales where Clave='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
               mensaje="El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
        
    }
}
