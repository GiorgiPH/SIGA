using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.HorariosAreasComunes
{
    class DBHorariosAreasComunes
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;


        public static int Folio = 0;
        public static int Eliminado = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBHorariosAreasComunes()
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
            cmd = new SqlCommand("Select * from Condominio_Areas where Condominio='"+ Condominio + "' and Reserva='True'", cn);
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
        //____________________________________________________________________________________________________
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
        //________________________________________________________________________________________________________________________________________________________
        public string RegistroHorario(string txtCondominio, string txtAreaComun, string txtD1, string txtD2, string txtD3, string txtD4, string txtD5, string txtD6, string txtD7, string txtH1, string txtH2, string txtH3, string txtH4, string txtH5, string txtH6, string txtH7)
        {
            int contador2 = 0;
            string mensaje = string.Empty;

            cmd = new SqlCommand("select * from Horario where Condominio='" + txtCondominio + "' and Area='" + txtAreaComun + "'", cn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                contador2++;
            }
            dr.Close();

            if (contador2 <= 0)
            {
                cmd = new SqlCommand("Insert into Horario ( Condominio, Area, D1,  D2,  D3,  D4,  D5,  D6,  D7,  H1,  H2,  H3,  H4,  H5,  H6,  H7) values ('" + txtCondominio + "','" + txtAreaComun + "','" + txtD1 + "','" + txtD2 + "','" + txtD3 + "','" + txtD4 + "','" + txtD5 + "','" + txtD6 + "','" + txtD7 + "','" + txtH1 + "','" + txtH2 + "','" + txtH3 + "','" + txtH4 + "','" + txtH5 + "','" + txtH6 + "','" + txtH7 + "')", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Guardado";
            }
            else
            {
                cmd = new SqlCommand("Update Horario set  D1='" + txtD1 + "',  D2='" + txtD2 + "',  D3='" + txtD3 + "',  D4='" + txtD4 + "',  D5='" + txtD5 + "',  D6='" + txtD6 + "',  D7='" + txtD7 + "',  H1='" + txtH1 + "',  H2='" + txtH2 + "',  H3='" + txtH3 + "',  H4='" + txtH4 + "',  H5='" + txtH5 + "',  H6='" + txtH6 + "',  H7='" + txtH7 + "' where Condominio='" + txtCondominio + "' and Area='" + txtAreaComun + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Guardado";
            }

            return mensaje;
        }
        //_______________________________________________________________________________________________________________________________________________________________
        //Consulta el consultario
        public void ConsultaGeneral(string txtCondominio, string txtAreaComun, DateTimePicker txtD1, DateTimePicker txtD2, DateTimePicker txtD3, DateTimePicker txtD4, DateTimePicker txtD5, DateTimePicker txtD6, DateTimePicker txtD7, DateTimePicker txtH1, DateTimePicker txtH2, DateTimePicker txtH3, DateTimePicker txtH4, DateTimePicker txtH5, DateTimePicker txtH6, DateTimePicker txtH7)
        {
            try
            {
                cmd = new SqlCommand("Select * from Horario where Condominio='" + txtCondominio + "' and Area='"+ txtAreaComun + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["D1"].ToString() != string.Empty)
                    {
                        txtD1.CustomFormat = "HH:mm";
                        txtD1.Text = dr["D1"].ToString();
                    }
                    if (dr["D2"].ToString() != string.Empty)
                    {
                        txtD2.CustomFormat = "HH:mm";
                        txtD2.Text = dr["D2"].ToString();
                    }
                    if (dr["D3"].ToString() != string.Empty)
                    {
                        txtD3.CustomFormat = "HH:mm";
                        txtD3.Text = dr["D3"].ToString();
                    }
                    if (dr["D4"].ToString() != string.Empty)
                    {
                        txtD4.CustomFormat = "HH:mm";
                        txtD4.Text = dr["D4"].ToString();
                    }
                    if (dr["D5"].ToString() != string.Empty)
                    {
                        txtD5.CustomFormat = "HH:mm";
                        txtD5.Text = dr["D5"].ToString();
                    }
                    if (dr["D6"].ToString() != string.Empty)
                    {
                        txtD6.CustomFormat = "HH:mm";
                        txtD6.Text = dr["D6"].ToString();
                    }
                    if (dr["D7"].ToString() != string.Empty)
                    {
                        txtD7.CustomFormat = "HH:mm";
                        txtD7.Text = dr["D7"].ToString();
                    }
                    if (dr["H1"].ToString() != string.Empty)
                    {
                        txtH1.CustomFormat = "HH:mm";
                        txtH1.Text = dr["H1"].ToString();
                    }
                    if (dr["H2"].ToString() != string.Empty)
                    {
                        txtH2.CustomFormat = "HH:mm";
                        txtH2.Text = dr["H2"].ToString();
                    }
                    if (dr["H3"].ToString() != string.Empty)
                    {
                        txtH3.CustomFormat = "HH:mm";
                        txtH3.Text = dr["H3"].ToString();
                    }
                    if (dr["H4"].ToString() != string.Empty)
                    {
                        txtH4.CustomFormat = "HH:mm";
                        txtH4.Text = dr["H4"].ToString();
                    }
                    if (dr["H5"].ToString() != string.Empty)
                    {
                        txtH5.CustomFormat = "HH:mm";
                        txtH5.Text = dr["H5"].ToString();
                    }
                    if (dr["H6"].ToString() != string.Empty)
                    {
                        txtH6.CustomFormat = "HH:mm";
                        txtH6.Text = dr["H6"].ToString();
                    }
                    if (dr["H7"].ToString() != string.Empty)
                    {
                        txtH7.CustomFormat = "HH:mm";
                        txtH7.Text = dr["H7"].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
    }
}
