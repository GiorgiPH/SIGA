using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Almacenes
{
    class DBAlmacenes
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Partida = 0;
        public static int Opcion = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBAlmacenes()
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
        public int ClaveSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Clave) from Almacenes", cn);
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
                        Folio = Convert.ToInt32(dt.Rows[0][0].ToString());
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
        //________________________________________________________________________________________________
        //Categorias Registrados
        public void CargarAlmacenes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Almacenes", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
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
        public void ConsultaAlmacenSeleccionada(string Clave, Guna2ToggleSwitch cmbEstatus, Guna2TextBox txtNombre, Guna2TextBox txtCuentaContable)
        {
            try
            {
                cmd = new SqlCommand("Select * from Almacenes where Clave='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Estatus"].ToString() == "Activo")
                    {
                        cmbEstatus.Checked = true;

                    }
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtCuentaContable.Text = dr["CuentaContable"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string RegistroAlmacen(string Clave, string Estatus, string Nombre, string CuentaContable)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Almacenes where Clave='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    
                    cmd = new SqlCommand("Insert into Almacenes (Clave, Estatus, Nombre, CuentaContable) values ('" + Clave + "', '" + Estatus + "', '" + Nombre + "', '" + CuentaContable + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Almacenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update Almacenes set  Estatus= '" + Estatus + "', Nombre='" + Nombre + "', CuentaContable= '" + CuentaContable + "' where Clave= '" + Clave + "'", cn);
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
    }
}
