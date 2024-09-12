using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.ConsultaCondominio
{
    class DBConsultaCondominio
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Eliminado = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBConsultaCondominio()
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

        public void CargarCondominios(DataGridView dgv, string Condominio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter(" select Clave, Descripcion, CONVERT(INT, SUBSTRING (Clave, 3,1000)) as Orden from Condominios_SubCondominios where Condominio='" + Condominio + "' order by Orden asc ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________
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
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaCondominioSeleccionado(string txtClave, TextBox txtDescripcion, TextBox txtCaracteristicas, TextBox txtobservaciones)
        {
            try
            {
                cmd = new SqlCommand("Select * from Condominios_SubCondominios where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    txtCaracteristicas.Text = dr["Caracteristicas"].ToString();
                    txtobservaciones.Text = dr["Observaciones"].ToString();

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
        //Mostrar formas seleccionado
        public void ConsultaCondominioPropietario(string condominio, TextBox propietario)
        {
            try
            {
                cmd = new SqlCommand("select P.RazonSocial from Propietarios_Condominios as PC, Propietarios as P where  PC.ClavePropietario=P.IdPropietario and PC.ClaveCondominio='"+ condominio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    propietario.Text = dr["RazonSocial"].ToString();
                  

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
        public void ConsultaPropietariosSeleccionado(string clave, TextBox txtclave, TextBox RazonSocial, ComboBox TipoCliente, TextBox RFC, TextBox Calle, TextBox NoExterior, TextBox NoInterior, TextBox Colonia, TextBox Municipio, TextBox CodigoPostal, TextBox Ciudad, TextBox Pais, TextBox Referencias, ComboBox MetodoPago, ComboBox FormaPago, ComboBox CFDI, TextBox BancoPagar, TextBox DomicilioFiscal, TextBox RegimelFiscal, TextBox Exportacion)
        {
            try
            {
                cmd = new SqlCommand("SELECT P.* FROM Propietarios as P, Propietarios_Condominios as PC where PC.ClaveCondominio='" + clave + "' and P.IdPropietario=PC.ClavePropietario", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtclave.Text = dr["IdPropietario"].ToString();
                    RazonSocial.Text = dr["RazonSocial"].ToString();
                    TipoCliente.Text = dr["TipoCliente"].ToString();
                    RFC.Text = dr["RFC"].ToString();
                    Calle.Text = dr["Calle"].ToString();
                    NoExterior.Text = dr["NoExterior"].ToString();
                    NoInterior.Text = dr["NoInterior"].ToString();
                    Colonia.Text = dr["Colonia"].ToString();
                    Municipio.Text = dr["Municipio"].ToString();
                    CodigoPostal.Text = dr["CodigoPostal"].ToString();
                    Ciudad.Text = dr["Ciudad"].ToString();
                    Pais.Text = dr["Pais"].ToString();
                    Referencias.Text = dr["Referencias"].ToString();
                    MetodoPago.Text = dr["MetodoPago"].ToString();
                    FormaPago.Text = dr["FormaPago"].ToString();
                    CFDI.Text = dr["CFDI"].ToString();
                    BancoPagar.Text = dr["BancoPago"].ToString();
                    DomicilioFiscal.Text = dr["DomicilioFiscal"].ToString();
                    RegimelFiscal.Text = dr["RegimenFiscal"].ToString();
                    Exportacion.Text = dr["Exportacion"].ToString();
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Empleado Registrados
        public void CargarCondominios(string Clave, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select CS.Clave, CS.Descripcion, PC.NumeroEscritura, PC.Caracteristica, CONVERT(INT, SUBSTRING (CS.Clave, 3,1000)) as Orden from Condominios_SubCondominios as CS, Propietarios_Condominios as PC where CS.Clave=PC.ClaveCondominio and PC.ClavePropietario='" + Clave + "'  order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["NumeroEscritura"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Caracteristica"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //____________________________________________________________________
        //----------------------------------------------------------
        public void CargarReciboAlumno(DataGridView dgv, string Matricula, string Condominio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select R.*, D.Nombre from Recibo as R, Documento as D where ClavePropietario='" + Matricula + "' and Propiedad='"+Condominio+"' and Saldo!=0 and R.ClaveDocumento=D.Clave", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[5].Value = item["Total"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Saldo"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroFormaPago(string txtClave, string txtobservaciones)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Condominios_SubCondominios where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update Condominios_SubCondominios set  Observaciones= '" + txtobservaciones + "' where Clave= '" + txtClave + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro modificado.";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //____________________________________________________________________________________________________________________________________________
        //Registro de Acceso en la base de datos
        public string RegistroAcceso(string txtCondominio, DateTime FechaEntrada, string HoraEntrada, DateTime FechaSalida, string HoraSalida)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroOcupacion where ClaveCondominio='" + txtCondominio + "' and FechaEntrada is not null and FechaSalida is null", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    if (MessageBox.Show("¿Registrar Entrada?", "Ocupacion Condominio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Insert into RegistroOcupacion (ClaveCondominio, FechaEntrada, HoraEntrada) values ('" + txtCondominio + "', '" + FechaEntrada.ToString("yyyy/MM/dd") + "','" + HoraEntrada + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Entrada Registrada";
                    }

                }
                else
                {
                    if (MessageBox.Show("¿Registrar Salida?", "Ocupacion Condominio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update RegistroOcupacion set FechaSalida='" + FechaEntrada.ToString("yyyy/MM/dd") + "', HoraSalida='" + HoraEntrada + "' where ClaveCondominio='" + txtCondominio + "' and FechaSalida is null", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Salida Registrada";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //____________________________________________________________________________________________________________________________________________
        //Registro de Acceso en la base de datos
        public string RegistroAccesoNotas(string txtCondominio, DateTime FechaEntrada, string HoraEntrada, string NumeroPlacas, string NumeroPersonas, string Notas)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroOcupacion where ClaveCondominio='" + txtCondominio + "' and FechaEntrada is not null and FechaSalida is null", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                        cmd = new SqlCommand("update RegistroOcupacion set NumeroPlacas='"+NumeroPlacas+ "', NummeroPersonas='" + NumeroPersonas+"', Notas='"+Notas+ "' where ClaveCondominio='" + txtCondominio + "' and FechaEntrada='"+FechaEntrada.ToString("yyyy/MM/dd") + "' and HoraEntrada='"+HoraEntrada+"'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Entrada Registrada";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }

        //____________________________________________________________________________________________________________________________________________
        //Registro de Acceso en la base de datos
        public int ColorOcupacion(string txtCondominio)
        {
            int mensaje = 0;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroOcupacion where ClaveCondominio='" + txtCondominio + "' and FechaEntrada is not null and FechaSalida is null", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    mensaje = 1;
                }
                else
                {
                    mensaje = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //____________________________________________________________________________________________________________________________________________
        //Registro de Acceso en la base de datos
        public int ColorOcupacionPropietario(string txtCondominio)
        {
            int mensaje = 0;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select CS.* from Propietarios_Condominios as CS where CS.ClaveCondominio='" + txtCondominio + "' ", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    mensaje = 1;
                }
                else
                {
                    mensaje = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

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
    }
}
