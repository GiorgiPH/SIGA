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
        public string RegistrConcepto(string Clave, string Nombre, string Clase, string Tipo, string Relativo, string Cuenta, Decimal Importe, int Iva)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ConceptosGlobales where Clave='" + Clave + "' and Nombre= '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into ConceptosGlobales (Clave, Nombre, Clase, Tipo, Relativo, Cuenta, Importe, IncluyeIva) values ('" + Clave + "', '" + Nombre + "', '" + Clase + "', '" + Tipo + "', '" + Relativo + "', '" + Cuenta + "', '" + Importe + "', "+Convert.ToInt16(Iva)+")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }
                else if (contador > 0)
                {
                    if (MessageBox.Show("El concepto ya existe, si continua sera modificado", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update ConceptosGlobales set  Clase='" + Clase + "', Tipo='" + Tipo + "', Relativo='" + Relativo + "', Cuenta='" + Cuenta + "', Importe='" + Importe + "', IncluyeIva="+Iva+" where Clave='" + Clave + "' and Nombre= '" + Nombre + "'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado.";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
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

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultConceptoSeleccionado(string Clave, string Nombre, ComboBox Clase, ComboBox Tipo, ComboBox Relativa, Guna2TextBox Cuenta, Guna2TextBox Importe, Guna2ToggleSwitch IncluyeiVA)
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
