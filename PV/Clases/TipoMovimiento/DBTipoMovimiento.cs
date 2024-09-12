using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases.TipoMovimiento
{
    class DBTipoMovimiento
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Eliminado = 0;
        public static int Entrada = 0;
        public static int Salida = 0;
        public static int Traspaso = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBTipoMovimiento()
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
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveDocumentoEntradaSiguiente(string Clave)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(UltimoFolio) from TipoMovimiento where TipoMovimiento='E' and Documento='"+Clave+"'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() != string.Empty)
                    {
                        Entrada = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
            return contador;
        }
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveDocumentoSalidaSiguiente(string Clave)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(UltimoFolio) from TipoMovimiento where TipoMovimiento='S' and Documento='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() != string.Empty)
                    {
                        Salida = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
            return contador;
        }
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveDocumentoTraspasoSiguiente(string Clave)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(UltimoFolio) from TipoMovimiento where TipoMovimiento='T' and Documento='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() != string.Empty)
                    {
                        Traspaso = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
            return contador;
        }

        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string RegistroMovimiento(string txtTipoMovimiento, string txtDocumento, string txtDescripcion, string cmbEstatus, string txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear, RadioButton rdbSiAfectaCosto, RadioButton rdbNoAfectaCosto, string txtAlmacen, string txtNotas)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string Bloqueo = string.Empty;
                    if (rdbSiBloquear.Checked == true)
                    {
                        Bloqueo = "Si";
                    }
                    else
                    {
                        Bloqueo = "No";
                    }

                    string AfectaCosto = string.Empty;
                    if (rdbSiAfectaCosto.Checked == true)
                    {
                        AfectaCosto = "Si";
                    }
                    else
                    {
                        AfectaCosto = "No";
                    }

                    cmd = new SqlCommand("Insert into TipoMovimiento (TipoMovimiento, Documento, Descripcion, Estatus, UltimoFolio, Bloquear, AfectaCosto, Almacen, Notas) values ('" + txtTipoMovimiento + "', '" + txtDocumento + "', '" + txtDescripcion + "', '" + cmbEstatus + "', '" + txtUltimoFolio + "', '" + Bloqueo + "', '" + AfectaCosto + "', '" + txtAlmacen + "', '" + txtNotas + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos del Tipo de Movimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Bloqueo = string.Empty;
                        if (rdbSiBloquear.Checked == true)
                        {
                            Bloqueo = "Si";
                        }
                        else
                        {
                            Bloqueo = "No";
                        }

                        string AfectaCosto = string.Empty;
                        if (rdbSiAfectaCosto.Checked == true)
                        {
                            AfectaCosto = "Si";
                        }
                        else
                        {
                            AfectaCosto = "No";
                        }

                        cmd = new SqlCommand("Update TipoMovimiento set  Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', UltimoFolio='" + txtUltimoFolio + "', Bloquear='" + Bloqueo + "', AfectaCosto='" + AfectaCosto + "', Almacen='" + txtAlmacen + "', Notas='" + txtNotas + "' where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string RegistroMovimientoST(string txtTipoMovimiento, string txtDocumento, string txtDescripcion, string cmbEstatus, string txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear, string txtAlmacen, string txtNotas)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string Bloqueo = string.Empty;
                    if (rdbSiBloquear.Checked == true)
                    {
                        Bloqueo = "Si";
                    }
                    else
                    {
                        Bloqueo = "No";
                    }

                    cmd = new SqlCommand("Insert into TipoMovimiento (TipoMovimiento, Documento, Descripcion, Estatus, UltimoFolio, Bloquear, Almacen, Notas) values ('" + txtTipoMovimiento + "', '" + txtDocumento + "', '" + txtDescripcion + "', '" + cmbEstatus + "', '" + txtUltimoFolio + "', '" + Bloqueo + "', '" + txtAlmacen + "', '" + txtNotas + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos del Tipo de Movimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Bloqueo = string.Empty;
                        if (rdbSiBloquear.Checked == true)
                        {
                            Bloqueo = "Si";
                        }
                        else
                        {
                            Bloqueo = "No";
                        }

                        cmd = new SqlCommand("Update TipoMovimiento set  Descripcion='" + txtDescripcion + "', Estatus='" + cmbEstatus + "', UltimoFolio='" + txtUltimoFolio + "', Bloquear='" + Bloqueo + "', Almacen='" + txtAlmacen + "', Notas='" + txtNotas + "' where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
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
        //Movimiento Registrados
        public void CargarEntrada(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from TipoMovimiento where TipoMovimiento= 'E'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["TipoMovimiento"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Documento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarSalida(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from TipoMovimiento where TipoMovimiento= 'S'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["TipoMovimiento"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Documento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarTraspaso(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from TipoMovimiento where TipoMovimiento= 'T'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["TipoMovimiento"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Documento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaEntradaSeleccionado(string txtTipoMovimiento, string txtDocumento,Guna.UI2.WinForms.Guna2TextBox  txtDescripcion, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear, RadioButton rdbSiAfectaCosto, RadioButton rdbNoAfectaCosto, TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtNotas)
        {
            try
            {
                cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtUltimoFolio.Text = dr["UltimoFolio"].ToString();

                    string bloqueo = dr["Bloquear"].ToString();
                    if (bloqueo == "Si")
                    {
                        rdbSiBloquear.Checked = true;
                    }
                    else
                    {
                        rdbNoBloquear.Checked = true;
                    }

                    string afecta = dr["AfectaCosto"].ToString();
                    if (afecta == "Si")
                    {
                        rdbSiAfectaCosto.Checked = true;
                    }
                    else
                    {
                        rdbNoAfectaCosto.Checked = true;
                    }

                    txtAlmacen.Text = dr["Almacen"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaSTSeleccionado(string txtTipoMovimiento, string txtDocumento, Guna.UI2.WinForms.Guna2TextBox txtDescripcion, Guna.UI2.WinForms.Guna2ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear, TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtNotas)
        {
            try
            {
                cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtUltimoFolio.Text = dr["UltimoFolio"].ToString();

                    string bloqueo = dr["Bloquear"].ToString();
                    if (bloqueo == "Si")
                    {
                        rdbSiBloquear.Checked = true;
                    }
                    else
                    {
                        rdbNoBloquear.Checked = true;
                    }

                    txtAlmacen.Text = dr["Almacen"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
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
        //______________________________________________________________________________________________________________________________
        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (convert(varchar, Clave) + ' - ' + Nombre) as Nombre from Almacenes", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen(string Documento)
        {
            cmd = new SqlCommand("Select Clave from Almacenes where (convert(varchar, Clave) + ' - ' + Nombre) = '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen2(string Documento)
        {
            cmd = new SqlCommand("Select (convert(varchar, Clave) + ' - ' + Nombre) from Almacenes where  Clave= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
    }
}
