using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace MEDCON.Clases.Avisos
{
    class DBAvisos
    {
        public static int Clave = 0;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Servidor = string.Empty;

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBAvisos()
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
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public string RegistroAvisos(string txtClave, string txtNombre, string dpFecha, string txtDescripcion, string cmbRegistrado)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Avisos where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into Avisos ( Clave, Nombre, FechaRegistro, Descripcion, RegistradoPor) values ('" + txtClave + "','" + txtNombre + "','" + dpFecha + "', '" + txtDescripcion + "', '" + cmbRegistrado + "')", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Avisos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Avisos set  Nombre='" + txtNombre + "', FechaRegistro='" + dpFecha + "', Descripcion='" + txtDescripcion + "', RegistradoPor='" + cmbRegistrado + "'  where Clave= '" + txtClave + "'", cn);
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
        //_________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveAvisosiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Clave) from Avisos", cn);
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
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        Clave = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
       //______________________________________________________________________________________________________________________________________
        //Avisos Registrados
        public void CargarAvisos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select Clave, Nombre from Avisos", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________________
        //Correos Registrados paciente
        public void Correos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdPropietario, RazonSocial, Correo from Propietarios where Correo!='' and EnviarAviso='Si'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdPropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Correo"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________________
        //Correos Registrados paciente
        public void Correos2(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdPropietario, RazonSocial, Correo2 from Propietarios where Correo2!='' and EnviarAviso2='Si'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdPropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Correo2"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________________
        //Correos Registrados cliente
        public void CorreosClientes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdCliente, RazonSocial, Correo from Clientes where Correo!='' and EnviarAviso='Si'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Correo"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________________
        //Correos Registrados cliente
        public void CorreosClientes2(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdCliente, RazonSocial, Correo2 from Clientes where Correo2!='' and EnviarAviso2='Si'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Correo2"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar aviso seleccionado
        public void ConsultaAvisoSeleccionado(string txtClave, TextBox txtNombre,DateTimePicker dpFecha , TextBox txtDescripcion, TextBox cmbRegistroPor, TextBox txtNombre2, TextBox txtClave2, TextBox txtDescripcion2)
        {
            try
            {
                cmd = new SqlCommand("Select * from Avisos where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    dpFecha.Text = dr["FechaRegistro"].ToString();
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmbRegistroPor.Text = dr["RegistradoPor"].ToString();
                    txtNombre2.Text = dr["Nombre"].ToString();
                    txtClave2.Text = dr["Clave"].ToString();
                    txtDescripcion2.Text = dr["Descripcion"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________________________________________________
        //obtener correo y contraseña del sitema
        public int CorreoContra()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Correo, Servidor, Contraseña from DatosEmpresa", cn);
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
                    Correo = dt.Rows[0][0].ToString();
                    Servidor = dt.Rows[0][1].ToString();
                    Contraseña = dt.Rows[0][2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
        //________________________________________________________________________________________________________________________________
        //Pacientes Registrados
        public void CargarPacientes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdPropietario, RazonSocial, Estatus, (case when EnviarAviso='Si' then Correo End) as Correo, (case when EnviarAviso2='Si' then Correo2 End) as Correo2 from Propietarios where (Correo!='' or Correo2!='') and Estatus='Activo' order by IdPropietario, Estatus, RazonSocial ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    
                    
                    dgv.Rows[n].Cells[1].Value = item["IdPropietario"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Correo"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Correo2"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

    }
}
