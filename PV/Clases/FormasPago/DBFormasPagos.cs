using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;


namespace PuntoVentas.Clases.FormasPago
{
    class DBFormasPagos
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

        public DBFormasPagos()
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
        public void SeleccionarFormaPAgo2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Descripcion from FormasPago", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveFormaPagoSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(ClaveFormasPago) from FormasPago", cn);
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
        // registrar forma pago 
        public string RegistroFormaPago(string txtClaveFormasPago, string txtDescripcion, string cmdEstatus, string cmbClase, string cmbCambio, string cmbReferencia, Guna2ToggleSwitch tgAutorizacion, RadioButton rdbAdmin, RadioButton rdbOtro, string txtNotas)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from FormasPago where ClaveFormasPago='" + txtClaveFormasPago + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string Autorizacion = string.Empty;
                    string TipoAuto = string.Empty;

                    if (tgAutorizacion.Checked==true)
                    {
                        Autorizacion = "Si";

                        if (rdbAdmin.Checked==true)
                        {
                            TipoAuto = "Administrador/Supervisor";
                        }
                        else
                        {
                            TipoAuto = "Otro";
                        }
                    }
                    else
                    {
                        Autorizacion = "No";

                    }


                    cmd = new SqlCommand("Insert into FormasPago (ClaveFormasPago, Descripcion, Estatus, Clase, Cambio, Referencia, Autorizacion, TipoAutorizacion, Notas) values ('"+ txtClaveFormasPago + "', '" + txtDescripcion+"',  '"+ cmdEstatus+"',  '" +  cmbClase + "', '" + cmbCambio+"',  '"+ cmbReferencia+"',  '" +Autorizacion + "', '" + TipoAuto+"',  '"+txtNotas+"')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de Formas de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Autorizacion = string.Empty;
                        string TipoAuto = string.Empty;

                        if (tgAutorizacion.Checked == true)
                        {
                            Autorizacion = "Si";

                            if (rdbAdmin.Checked == true)
                            {
                                TipoAuto = "Administrador/Supervisor";
                            }
                            else
                            {
                                TipoAuto = "Otro";
                            }
                        }
                        else
                        {
                            Autorizacion = "No";

                        }

                        cmd = new SqlCommand("Update FormasPago set Descripcion='" + txtDescripcion + "', Estatus='" + cmdEstatus + "', Clase='" + cmbClase + "', Cambio='" + cmbCambio + "', Referencia='" + cmbReferencia + "', Autorizacion='" + Autorizacion + "', TipoAutorizacion='" + TipoAuto + "', Notas='" + txtNotas + "' where ClaveFormasPago= '" + txtClaveFormasPago + "'", cn);
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
        //formas pago Registrados
        public void CargarFormasPago(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from FormasPago", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveFormasPago"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaFormasPagoSeleccionado(string txtClaveFormasPago, Guna2TextBox txtDescripcion, ComboBox cmdEstatus, ComboBox cmbClase, ComboBox cmbCambio, ComboBox cmbReferencia, Guna2ToggleSwitch tgAutorizacion, RadioButton rdbAdmin, RadioButton rdbOtro, Guna2TextBox txtNotas)
        {
            try
            {
                cmd = new SqlCommand("Select * from FormasPago where ClaveFormasPago='" + txtClaveFormasPago + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmdEstatus.Text = dr["Estatus"].ToString();
                    cmbClase.Text = dr["Clase"].ToString();
                    cmbCambio.Text = dr["Cambio"].ToString();
                    cmbReferencia.Text = dr["Referencia"].ToString();

                    string autorizacion = dr["Autorizacion"].ToString();

                    if (autorizacion== "Si")
                    {
                        tgAutorizacion.Checked = true;
                    }
                    else
                    {
                        tgAutorizacion.Checked = true;
                    }

                    string tipoauto = dr["TipoAutorizacion"].ToString();

                    if (tipoauto == "Administrador/Supervisor")
                    {
                        rdbAdmin.Checked = true;
                    }
                    else
                    {
                        rdbOtro.Checked = true;
                    }

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
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete FormasPago where ClaveFormasPago='" + txtClaveDivisa + "'", cn);
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
