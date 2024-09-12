using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.ReservaAreaComun
{
    class DBReservaAreaComun
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int IdPropietario = 0;
        public static string Telefono = "";
        public static string Celular = "";
        public static string Correo = "";
        public static string RazonSocial = "";
        public static int HorarioInicial = 0;
        public static int HorarioFinal = 0;
        public static int HorarioInicial2 = 0;
        public static int HorarioFinal2 = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBReservaAreaComun()
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
        //Obtener la clave del paciente
        public int Clave(string Nombre)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select IdPropietario, Telefono, Celular, Correo, RazonSocial from Propietarios where RazonSocial = '" + Nombre + "'", cn);
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
                        IdPropietario = Convert.ToInt32(dt.Rows[0][0].ToString());
                        Telefono = dt.Rows[0][1].ToString();
                        Celular = dt.Rows[0][2].ToString();
                        Correo = dt.Rows[0][3].ToString();
                        RazonSocial = dt.Rows[0][4].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
        //____________________________________________________________________________________________________________________________________________
        //Validar Horario Lunes
        public void HorarioLunes(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='"+ Condominio + "' and Area='"+ AreaComun + "'", cn);
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
                    if (dt.Rows[0][2].ToString() != "")
                    {
                        HoraCompleta = dt.Rows[0][2].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][9].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Martes
        public void HorarioMartes(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][3].ToString() != "")
                    {
                        HoraCompleta = dt.Rows[0][3].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][10].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Miercoles
        public void HorarioMiercoles(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][4].ToString() != "")
                    {
                        HoraCompleta = dt.Rows[0][4].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][11].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Jueves
        public void HorarioJueves(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][5].ToString() != "")
                    {
                        HoraCompleta = dt.Rows[0][5].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][12].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Viernes
        public void HorarioViernes(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][6].ToString() != "" )
                    {
                        HoraCompleta = dt.Rows[0][6].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][13].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Sabado
        public void HorarioSabado(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][7].ToString() != "" )
                    {
                        HoraCompleta = dt.Rows[0][7].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][14].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Validar Horario Domingo
        public void HorarioDomingo(string Condominio, string AreaComun)
        {
            int contador = 0;
            string HoraCompleta = "";
            string Hora = "";
            string Minuto = "";

            try
            {
                cmd = new SqlCommand("select * from Horario where Condominio='" + Condominio + "' and Area='" + AreaComun + "'", cn);
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
                    if (dt.Rows[0][8].ToString() != "" )
                    {
                        HoraCompleta = dt.Rows[0][8].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);
                        HorarioInicial = Convert.ToInt32(Hora + Minuto) / 100;


                        HoraCompleta = dt.Rows[0][15].ToString();
                        Hora = HoraCompleta.Substring(0, 2);
                        Minuto = HoraCompleta.Substring(3, 2);

                        HorarioFinal = Convert.ToInt32(Hora + Minuto) / 100;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //____________________________________________________________________________________________________________________________________________
        // registrar Cita 
        public string RegistrarCita(string txtCondominio, string txtArea, string txtPropietario, string txtTelefono, string txtCelular, string txtCorreo, string txtFechaCita, string txtHora,  string cmbEstatus, string txtNotas)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ReservarArea where Condominio='" + txtCondominio + "' and Area='"+ txtArea + "' and ClavePropietario='"+ txtPropietario + "' and FechaCita='" + txtFechaCita + "' and Hora='"+txtHora+"'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into ReservarArea ( Condominio, Area, ClavePropietario, Telefono, Celular, Correo, FechaCita, Hora, Estatus, Notas) values ('" + txtCondominio + "','" + txtArea + "','" + txtPropietario + "','" + txtTelefono + "','" + txtCelular + "','" + txtCorreo + "','" + txtFechaCita + "','" + txtHora + "', '" + cmbEstatus + "','" + txtNotas + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";
                }
                else
                {
                    mensaje = "Este Propietario ya tiene reservado el area para esta fecha";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //____________________________________________________________________________________________________________________________________________
        //Validar Pacientes citados
        public void PacientesCitados(DataGridView dgv, string Fecha, string Condominio, string AreaComun)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select P.RazonSocial, R.FechaCita, R.Hora, R.Condominio, R.Area from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.FechaCita='" + Fecha + "' and R.Condominio='"+Condominio+"' and Area='"+AreaComun+"'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["FechaCita"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Hora"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Area"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //_______________________________________________________________________________________________________________________________________________________
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
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondominio(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarAreaComun(ComboBox cb, string Condominio)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Condominio_Areas where Condominio='" + Condominio + "' and Reserva='True'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionCondominio(string nombre)
        {
            //dr.Close();
            cmd = new SqlCommand("Select * from Condominio where Descripcion = '" + nombre + "'", cn);
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
        public string[] InformacionAreaComun(string nombre)
        {
            //dr.Close();
            cmd = new SqlCommand("Select * from Condominio_Areas where Nombre = '" + nombre + "'", cn);
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
        //________________________________________________________________________________________________________________________________
        //citas Registrados
        public void CargarCitas(DataGridView dgv, string Fecha)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.FechaCita= '" + Fecha + "' and R.Estatus='Confirmada'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClavePropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = Convert.ToDateTime(item["FechaCita"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[3].Value = item["Hora"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Area"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________________________________________
        //citas Registrados
        public void CargarCitas2(DataGridView dgv, string Fecha, string Nombre)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.FechaCita >= '" + Fecha + "' and R.Estatus='Confirmada' and P.RazonSocial='" + Nombre + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClavePropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = Convert.ToDateTime(item["FechaCita"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[3].Value = item["Hora"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Area"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________________________________________
        //citas Registrados
        public void CargarCitasFiltroNombre(DataGridView dgv, string Fecha, string Nombre)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.FechaCita= '" + Fecha + "' and R.Estatus='Confirmada' and P.RazonSocial like '%"+Nombre+"%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClavePropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = Convert.ToDateTime(item["FechaCita"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[3].Value = item["Hora"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Area"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________________________________________
        //citas Registrados
        public void CargarCitasFiltroNombre2(DataGridView dgv, string Fecha, string Nombre)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.FechaCita >= '" + Fecha + "' and R.Estatus='Confirmada' and P.RazonSocial like '%" + Nombre + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClavePropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = Convert.ToDateTime(item["FechaCita"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[3].Value = item["Hora"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Area"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar cita seleccionado
        public void CitaBuscar(string Nombre, string Fecha, string Hora, string Condominio, string Area, TextBox txtNombre, TextBox txtClavePaciente, TextBox txtFechaCita, TextBox txtHora, TextBox cmbEstatus, TextBox txtNotas, TextBox txtCondominio, TextBox txtArea)
        {


            try
            {
                cmd = new SqlCommand("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and R.ClavePropietario = '" + Nombre + "' and R.FechaCita= '" + Fecha + "' and R.Hora='"+Hora+"' and R.Condominio='"+Condominio+"' and R.Area='"+Area+"'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtNombre.Text = dr["RazonSocial"].ToString();
                    txtClavePaciente.Text = dr["ClavePropietario"].ToString();
                    txtFechaCita.Text = Convert.ToDateTime(dr["FechaCita"]).ToString("yyyy/MM/dd");
                    txtHora.Text = dr["Hora"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    txtCondominio.Text = dr["Condominio"].ToString();
                    txtArea.Text = dr["Area"].ToString();
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
        //Mostrar cita seleccionado
        public void CitaBuscar2(string Nombre, string Fecha, TextBox txtNombre, TextBox txtClavePaciente, TextBox txtFechaCita, TextBox txtHora, TextBox cmbEstatus, TextBox txtNotas, TextBox txtTelefono, TextBox txtCelular, TextBox txtCorreo, TextBox txtCondominio, TextBox txtArea)
        {


            try
            {
                cmd = new SqlCommand("Select R.*, P.RazonSocial from ReservarArea as R, Propietarios as P where R.ClavePropietario=P.IdPropietario and P.RazonSocial = '" + Nombre + "' and R.FechaCita >= '" + Fecha + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtNombre.Text = dr["RazonSocial"].ToString();
                    txtClavePaciente.Text = dr["ClavePropietario"].ToString();
                    txtFechaCita.Text = Convert.ToDateTime(dr["FechaCita"]).ToString("yyyy/MM/dd");
                    txtHora.Text = dr["Hora"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    txtTelefono.Text = dr["Telefono"].ToString();
                    txtCelular.Text = dr["Celular"].ToString();
                    txtCorreo.Text = dr["Correo"].ToString();
                    txtCondominio.Text = dr["Condominio"].ToString();
                    txtArea.Text = dr["Area"].ToString();
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
        //Mostrar cita seleccionado
        public void CitaBuscar3(string Nombre, string Fecha, string Hora, string Condominio, string Area, TextBox txtNombre, TextBox txtClavePaciente, TextBox txtFechaCita, TextBox txtHora, TextBox cmbEstatus, TextBox txtNotas, TextBox txtCondominio, TextBox txtArea, TextBox txtTelefono, TextBox txtCelular, TextBox txtCorreo)
        {


            try
            {
                cmd = new SqlCommand("Select R.*, P.RazonSocial, P.Telefono, P.Celular, P.Correo, C.Descripcion as CondominioD, AC.Descripcion as AreaComunD from ReservarArea as R, Propietarios as P, Condominio as C, AreasComunes as AC where R.ClavePropietario=P.IdPropietario and R.Condominio=C.ClaveCondominio and R.Area=AC.ClaveAreas and R.ClavePropietario = '" + Nombre + "' and R.FechaCita= '" + Fecha + "' and R.Hora='" + Hora + "' and R.Condominio='" + Condominio + "' and R.Area='" + Area + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtNombre.Text = dr["RazonSocial"].ToString();
                    txtClavePaciente.Text = dr["ClavePropietario"].ToString();
                    txtFechaCita.Text = Convert.ToDateTime(dr["FechaCita"]).ToString("yyyy/MM/dd");
                    txtHora.Text = dr["Hora"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    txtCondominio.Text = dr["CondominioD"].ToString();
                    txtArea.Text = dr["AreaComunD"].ToString();
                    txtTelefono.Text = dr["Telefono"].ToString();
                    txtCelular.Text = dr["Celular"].ToString();
                    txtCorreo.Text = dr["Correo"].ToString();
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
        // registrar Cita 
        public string RegistrarCitaCancelada(string txtNombre, string Fecha, string Hora, string txtClavePaciente, string txtFechaCancel, string txtMotivo, string Condominio, string Area)
        {
            string mensaje = "";

            try
            {
                cmd = new SqlCommand("Update ReservarArea set Estatus='Cancelada', FechaCitaCancelar='" + txtFechaCancel + "', Motivos='" + txtMotivo + "' where  ClavePropietario='" + txtClavePaciente + "' and FechaCita='"+ Fecha + "' and Hora='"+ Hora+"' and Condominio='"+Condominio+"' and Area= '"+Area+"'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Cita Cancelada.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
    }
}
