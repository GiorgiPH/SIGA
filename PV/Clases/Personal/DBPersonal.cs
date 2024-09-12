using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PuntoVentas.Clases.Personal
{
    class DBPersonal
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

        public DBPersonal()
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
        public int ClavePersonalSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(ClaveEmpleado) from Empleados", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar Empleado 
        public string RegistroEmpleado(string txtClaveEmpleado, string txtNombre, string txtApellidoP, string txtApellidoM, string cmbEstatus, string dtpFechaNacimiento, string dtpFechaIngreso, string txtEscolaridad, string txtTelefonoCasa, string txtCelular, string txtEmail, string txtCalleNumero, string txtColonia, string txtMunicipio, string txtEstado, string txtCodigoPostal, string txtPais, string txtNotas, string cmbClaveUsuario, string txtPuesto, string txtNIP, string txtTipoContrato, string txtRFC, string txtCURP, string txtIMSS, decimal txtSueldo, string txtBono, string txtVacaciones, string txtHorario, PictureBox Foto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Empleados where ClaveEmpleado='" + txtClaveEmpleado + "'", cn);
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

                        cmd = new SqlCommand("Insert into Empleados (ClaveEmpleado, Nombre, ApellidoP, ApellidoM, Estatus, FechaNacimiento, FechaIngreso, Escolaridad, TelefonoCasa, Celular, Email, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Notas, ClaveUsuario, Puesto, NIP, TipoContrato, RFC, CURP, IMSS, Sueldo, Bono, Vacaciones, Horario) values ('" + txtClaveEmpleado + "', '" + txtNombre + "',  '" + txtApellidoP + "', '" + txtApellidoM + "', '" + cmbEstatus + "', '" + dtpFechaNacimiento + "',  '" + dtpFechaIngreso + "', '" + txtEscolaridad + "',  '" + txtTelefonoCasa + "',  '" + txtCelular + "', '" + txtEmail + "',  '" + txtCalleNumero + "',  '" + txtColonia + "', '" + txtMunicipio + "',  '" + txtEstado + "',  '" + txtCodigoPostal + "', '" + txtPais + "', '" + txtNotas + "', '" + cmbClaveUsuario + "', '" + txtPuesto + "',  '" + txtNIP + "',  '" + txtTipoContrato + "', '" + txtRFC + "',  '" + txtCURP + "',  '" + txtIMSS + "', " + txtSueldo + ",  '" + txtBono + "',  '" + txtVacaciones + "', '" + txtHorario + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into Empleados (ClaveEmpleado, Nombre, ApellidoP, ApellidoM, Estatus, FechaNacimiento, FechaIngreso, Escolaridad, TelefonoCasa, Celular, Email, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Notas, ClaveUsuario, Puesto, NIP, TipoContrato, RFC, CURP, IMSS, Sueldo, Bono, Vacaciones, Horario, Foto) values ('" + txtClaveEmpleado + "', '" + txtNombre + "',  '" + txtApellidoP + "', '" + txtApellidoM + "', '" + cmbEstatus + "', '" + dtpFechaNacimiento + "',  '" + dtpFechaIngreso + "', '" + txtEscolaridad + "',  '" + txtTelefonoCasa + "',  '" + txtCelular + "', '" + txtEmail + "',  '" + txtCalleNumero + "',  '" + txtColonia + "', '" + txtMunicipio + "',  '" + txtEstado + "',  '" + txtCodigoPostal + "', '" + txtPais + "', '" + txtNotas + "', '" + cmbClaveUsuario + "', '" + txtPuesto + "',  '" + txtNIP + "',  '" + txtTipoContrato + "', '" + txtRFC + "',  '" + txtCURP + "',  '" + txtIMSS + "', " + txtSueldo + ",  '" + txtBono + "',  '" + txtVacaciones + "', '" + txtHorario + "', @Foto)", cn);
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
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos del Empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Foto.Image == null)
                        {
                            cmd = new SqlCommand("Update Empleados set  Nombre= '" + txtNombre + "', ApellidoP='" + txtApellidoP + "', ApellidoM='" + txtApellidoM + "', Estatus='" + cmbEstatus + "', FechaNacimiento='" + dtpFechaNacimiento + "', FechaIngreso='" + dtpFechaIngreso + "', Escolaridad='" + txtEscolaridad + "', TelefonoCasa='" + txtTelefonoCasa + "', Celular= '" + txtCelular + "', Email='" + txtEmail + "', CalleNumero= '" + txtCalleNumero + "', Colonia='" + txtColonia + "', Municipio='" + txtMunicipio + "', Estado='" + txtEstado + "', CodigoPostal='" + txtCodigoPostal + "', Pais='" + txtPais + "', Notas= '" + txtNotas + "', ClaveUsuario='" + cmbClaveUsuario + "', Puesto='" + txtPuesto + "', NIP='" + txtNIP + "', TipoContrato='" + txtTipoContrato + "', RFC='" + txtRFC + "', CURP='" + txtCURP + "', IMSS='" + txtIMSS + "', Sueldo='" + txtSueldo + "', Bono= '" + txtBono + "', Vacaciones= '" + txtVacaciones + "', Horario='" + txtHorario + "' where ClaveEmpleado=  '" + txtClaveEmpleado + "'", cn);
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Empleados set  Nombre= '" + txtNombre + "', ApellidoP='" + txtApellidoP + "', ApellidoM='" + txtApellidoM + "', Estatus='" + cmbEstatus + "', FechaNacimiento='" + dtpFechaNacimiento + "', FechaIngreso='" + dtpFechaIngreso + "', Escolaridad='" + txtEscolaridad + "', TelefonoCasa='" + txtTelefonoCasa + "', Celular= '" + txtCelular + "', Email='" + txtEmail + "', CalleNumero= '" + txtCalleNumero + "', Colonia='" + txtColonia + "', Municipio='" + txtMunicipio + "', Estado='" + txtEstado + "', CodigoPostal='" + txtCodigoPostal + "', Pais='" + txtPais + "', Notas= '" + txtNotas + "', ClaveUsuario='" + cmbClaveUsuario + "', Puesto='" + txtPuesto + "', NIP='" + txtNIP + "', TipoContrato='" + txtTipoContrato + "', RFC='" + txtRFC + "', CURP='" + txtCURP + "', IMSS='" + txtIMSS + "', Sueldo='" + txtSueldo + "', Bono= '" + txtBono + "', Vacaciones= '" + txtVacaciones + "', Horario='" + txtHorario + "', Foto=@Foto where ClaveEmpleado=  '" + txtClaveEmpleado + "'", cn);
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
        //Empleado Registrados
        public void CargarEmpleados(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Empleados", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveEmpleado"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString() + ' ' + item["ApellidoP"].ToString() + ' ' + item["ApellidoM"].ToString();
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
        public void ConsultaEmpleadoSeleccionado(string txtClaveEmpleado, Guna2TextBox txtNombre, Guna2TextBox txtApellidoP, Guna2TextBox txtApellidoM, ComboBox cmbEstatus, Guna2DateTimePicker dtpFechaNacimiento, Guna2DateTimePicker dtpFechaIngreso, Guna2TextBox txtEscolaridad, Guna2TextBox txtTelefonoCasa, Guna2TextBox txtCelular, Guna2TextBox txtEmail, Guna2TextBox txtCalleNumero, Guna2TextBox txtColonia, Guna2TextBox txtMunicipio, Guna2TextBox txtEstado, Guna2TextBox txtCodigoPostal, Guna2TextBox txtPais, Guna2TextBox txtNotas, ComboBox cmbClaveUsuario, Guna2TextBox txtPuesto, Guna2TextBox txtNIP, Guna2TextBox txtTipoContrato, Guna2TextBox txtRFC, Guna2TextBox txtCURP, Guna2TextBox txtIMSS, Guna2TextBox txtSueldo, Guna2TextBox txtBono, Guna2TextBox txtVacaciones, Guna2TextBox txtHorario, PictureBox Foto)
        {
            try
            {
                cmd = new SqlCommand("Select * from Empleados where ClaveEmpleado='" + txtClaveEmpleado + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    txtApellidoP.Text = dr["ApellidoP"].ToString();
                    txtApellidoM.Text = dr["ApellidoM"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    dtpFechaNacimiento.Text = dr["FechaNacimiento"].ToString();
                    dtpFechaIngreso.Text = dr["FechaIngreso"].ToString();
                    txtEscolaridad.Text = dr["Escolaridad"].ToString();
                    txtTelefonoCasa.Text = dr["TelefonoCasa"].ToString();
                    txtCelular.Text = dr["Celular"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    txtCalleNumero.Text = dr["CalleNumero"].ToString();
                    txtColonia.Text = dr["Colonia"].ToString();
                    txtMunicipio.Text = dr["Municipio"].ToString();
                    txtEstado.Text = dr["Estado"].ToString();
                    txtCodigoPostal.Text = dr["CodigoPostal"].ToString();
                    txtPais.Text = dr["Pais"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    cmbClaveUsuario.Text = dr["ClaveUsuario"].ToString();
                    txtPuesto.Text = dr["Puesto"].ToString();
                    txtNIP.Text = dr["NIP"].ToString();
                    txtTipoContrato.Text = dr["TipoContrato"].ToString();
                    txtRFC.Text = dr["RFC"].ToString();
                    txtCURP.Text = dr["CURP"].ToString();
                    txtIMSS.Text = dr["IMSS"].ToString();
                    txtSueldo.Text = dr["Sueldo"].ToString();
                    txtBono.Text = dr["Bono"].ToString();
                    txtVacaciones.Text = dr["Vacaciones"].ToString();
                    txtHorario.Text = dr["Horario"].ToString();

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
        public void SeleccionarUsuario(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Usuarios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete Empleados where ClaveEmpleado='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                MessageBox.Show("El registro esta en uso, no es posible eliminar");
            }
            return mensaje;
        }
    }
}
