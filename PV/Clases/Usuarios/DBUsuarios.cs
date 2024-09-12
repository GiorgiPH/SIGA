using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;


namespace PuntoVentas.Clases.Usuarios
{
    class DBUsuarios
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static string TipoUsuario = "";
        public static string Estatus = "";

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBUsuarios()
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
        // registrar Usuarios 
        public string RegistroUsuario(string txtUsuario, string txtNombre, string txtContraseña, string cmbTipo, string dpFecha, string cmbEstatus, string txtNota, PictureBox Foto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Usuarios where Usuario='" + txtUsuario + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    if (Foto.Image == null)
                    {
                        cmd = new SqlCommand("Insert into Usuarios ( Usuario, Contraseña, Nombre,  TipoUsuario ,  FechaAlta,  Estatus,  Nota) values ('" + txtUsuario + "','" + txtContraseña + "','" + txtNombre + "','" + cmbTipo + "','" + dpFecha + "','" + cmbEstatus + "','" + txtNota + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Usuarios ( Usuario, Contraseña, Nombre ,  TipoUsuario ,  FechaAlta,  Estatus,  Nota,  Foto) values ('" + txtUsuario + "','" + txtContraseña + "','" + txtNombre + "','" + cmbTipo + "','" + dpFecha + "','" + cmbEstatus + "','" + txtNota + "', @Foto)", cn);

                        cmd.Parameters.Add("@Foto", SqlDbType.Image);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        cmd.Parameters["@Foto"].Value = ms.GetBuffer();
                        cmd.ExecuteNonQuery();

                        mensaje = "Registro guardado.";
                    }

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Usuarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Foto.Image == null)
                        {
                            cmd = new SqlCommand("Update Usuarios set  Contraseña='" + txtContraseña + "', Nombre='" + txtNombre + "',  TipoUsuario='" + cmbTipo + "',  FechaAlta='" + dpFecha + "',  Estatus='" + cmbEstatus + "',  Nota='" + txtNota + "' where Usuario='" + txtUsuario + "'", cn);
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Usuarios set  Contraseña='" + txtContraseña + "', Nombre='" + txtNombre + "',  TipoUsuario='" + cmbTipo + "',  FechaAlta='" + dpFecha + "',  Estatus='" + cmbEstatus + "',  Nota='" + txtNota + "',  Foto=@Foto where Usuario='" + txtUsuario + "'", cn);
                            cmd.Parameters.Add("@Foto", SqlDbType.Image);
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            cmd.Parameters["@Foto"].Value = ms.GetBuffer();
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }

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
        //Usuarios Registrados
        public void CargarUsuarioss(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Usuarios", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Usuario"].ToString();
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
        public void ConsultaUsuarioSeleccionado(string Usuario, Guna.UI2.WinForms.Guna2TextBox txtNombre, Guna.UI2.WinForms.Guna2TextBox txtContraseña, ComboBox cmbTipo, DateTimePicker dpFecha, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtNota, PictureBox Foto)
        {
            try
            {
                cmd = new SqlCommand("Select * from Usuarios where Usuario='" + Usuario + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtContraseña.Text = dr["Contraseña"].ToString();
                    cmbTipo.Text = dr["TipoUsuario"].ToString();
                    dpFecha.Text = dr["FechaAlta"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtNota.Text = dr["Nota"].ToString();

                    string Imagen = dr["Foto"].ToString();

                    if (Imagen != "")
                    {
                        byte[] datos = new byte[0];
                        datos = (byte[])dr["Foto"];

                        System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                        Foto.Image = System.Drawing.Bitmap.FromStream(ms);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
    }
}
