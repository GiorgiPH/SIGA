using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;


namespace Condominios.Clases.Condominios
{
    class DBCondominios
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

        public DBCondominios()
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
        public void SeleccionarEjercicio(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Clave from Periodo group by Clave", cn);
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
                cmd = new SqlCommand("select max(ClaveCondominio) from Condominio", cn);
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
        public string RegistroFormaPago(string txtClave, string txtDescripcion, string txtCalle, string txtNoExterior, string txtNoInterior, string txtColonia, string txtMunicipio, string txtCodigoPostal, string txtCiudad, string txtestado, string txtPais, string txtTelefono, string txtReferencias, string txtCaracteristicas, RadioButton rdbGenral, RadioButton rdbProIndiviso, RadioButton rdbOtros, string txtConcepto, string Anual, decimal Importe, string Metodo, string Periodo,  string Anual2, decimal Importe2, string Metodo2, string Periodo2, PictureBox Foto)
        {
            string mensaje = string.Empty;
            int contador = 0;
            string ClaveDep = string.Empty;
            string NombreDep = string.Empty;
            string Caracteristicas = string.Empty;
            decimal ProIndiviso = 0;
            string cuotaProindiviso = string.Empty;
            string cuotaGeneral = string.Empty;
            string cuotaOtros = string.Empty;
            decimal Mantenimiento = 0;
            string Del = string.Empty;
            string Al = string.Empty;

            try
            {
                cmd = new SqlCommand("select * from Condominio where ClaveCondominio='" + txtClave + "'", cn);
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
                        if (rdbGenral.Checked == true)
                        {
                            cuotaGeneral = "Si";
                        }
                        else if (rdbProIndiviso.Checked == true)
                        {
                            cuotaProindiviso = "Si";
                        }
                        else if (rdbOtros.Checked == true)
                        {
                            cuotaOtros = "Si";
                        }

                        cmd = new SqlCommand("Insert into Condominio (ClaveCondominio, Descripcion, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Estado, Pais, Telefono, Referencias, Caracteristicas, CuotaGeneral, CuotaProIndiviso, CuotaOtros, Concepto, Anual, Importe, Metodo, Periodo,  AnualE, ImporteE, MetodoE, PeriodoE) values ('" + txtClave + "',  '" + txtDescripcion + "',  '" + txtCalle + "',  '" + txtNoExterior + "',  '" + txtNoInterior + "',  '" + txtColonia + "',  '" + txtMunicipio + "',  '" + txtCodigoPostal + "',  '" + txtCiudad + "', '" + txtestado + "', '" + txtPais + "',  '" + txtTelefono + "',  '" + txtReferencias + "',  '" + txtCaracteristicas + "', '" + cuotaGeneral + "', '" + cuotaProindiviso + "', '" + cuotaOtros + "', '" + txtConcepto + "', '" + Anual + "', " + Importe + ", '" + Metodo + "', '" + Periodo + "', '" + Anual2 + "', " + Importe2 + ", '" + Metodo2 + "', '" + Periodo2 + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        if (rdbGenral.Checked == true)
                        {
                            cuotaGeneral = "Si";
                        }
                        else if (rdbProIndiviso.Checked == true)
                        {
                            cuotaProindiviso = "Si";
                        }
                        else if (rdbOtros.Checked == true)
                        {
                            cuotaOtros = "Si";
                        }

                        cmd = new SqlCommand("Insert into Condominio (ClaveCondominio, Descripcion, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Estado, Pais, Telefono, Referencias, Caracteristicas, CuotaGeneral, CuotaProIndiviso, CuotaOtros, Concepto, Anual, Importe, Metodo, Periodo, AnualE, ImporteE, MetodoE, PeriodoE, Foto) values ('" + txtClave + "',  '" + txtDescripcion + "',  '" + txtCalle + "',  '" + txtNoExterior + "',  '" + txtNoInterior + "',  '" + txtColonia + "',  '" + txtMunicipio + "',  '" + txtCodigoPostal + "',  '" + txtCiudad + "',  '" + txtestado + "', '" + txtPais + "',  '" + txtTelefono + "',  '" + txtReferencias + "',  '" + txtCaracteristicas + "', '" + cuotaGeneral + "', '" + cuotaProindiviso + "', '" + cuotaOtros + "', '" + txtConcepto + "', '" + Anual + "', " + Importe + ", '" + Metodo + "', '" + Periodo + "', '" + Anual2 + "', " + Importe2 + ", '" + Metodo2 + "', '" + Periodo2 + "', @Foto)", cn);
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
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Condominios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Foto.Image == null)
                        {
                            if (rdbGenral.Checked == true)
                            {
                                cuotaGeneral = "Si";
                            }
                            else if (rdbProIndiviso.Checked == true)
                            {
                                cuotaProindiviso = "Si";
                            }
                            else if (rdbOtros.Checked == true)
                            {
                                cuotaOtros = "Si";
                            }

                            cmd = new SqlCommand("Update Condominio set  Descripcion= '" + txtDescripcion + "', Calle= '" + txtCalle + "', NoExterior= '" + txtNoExterior + "', NoInterior='" + txtNoInterior + "', Colonia= '" + txtColonia + "', Municipio= '" + txtMunicipio + "', CodigoPostal='" + txtCodigoPostal + "', Ciudad= '" + txtCiudad + "', Estado='" + txtestado + "', Pais= '" + txtPais + "', Telefono= '" + txtTelefono + "', Referencias= '" + txtReferencias + "', Caracteristicas='" + txtCaracteristicas + "', CuotaGeneral= '" + cuotaGeneral + "', CuotaProIndiviso= '" + cuotaProindiviso + "', CuotaOtros= '" + cuotaOtros + "', Concepto= '" + txtConcepto + "', Anual='" + Anual + "', Importe=" + Importe + ", Metodo='" + Metodo + "', Periodo='" + Periodo + "', AnualE='" + Anual2 + "', ImporteE= " + Importe2 + ", MetodoE='" + Metodo2 + "', PeriodoE='" + Periodo2 + "' where ClaveCondominio= '" + txtClave + "'", cn);
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                        else
                        {
                            if (rdbGenral.Checked == true)
                            {
                                cuotaGeneral = "Si";
                            }
                            else if (rdbProIndiviso.Checked == true)
                            {
                                cuotaProindiviso = "Si";
                            }
                            else if (rdbOtros.Checked == true)
                            {
                                cuotaOtros = "Si";
                            }

                            cmd = new SqlCommand("Update Condominio set  Descripcion= '" + txtDescripcion + "', Calle= '" + txtCalle + "', NoExterior= '" + txtNoExterior + "', NoInterior='" + txtNoInterior + "', Colonia= '" + txtColonia + "', Municipio= '" + txtMunicipio + "', CodigoPostal='" + txtCodigoPostal + "', Ciudad= '" + txtCiudad + "',  Estado='" + txtestado + "', Pais= '" + txtPais + "', Telefono= '" + txtTelefono + "', Referencias= '" + txtReferencias + "', Caracteristicas='" + txtCaracteristicas + "', CuotaGeneral= '" + cuotaGeneral + "', CuotaProIndiviso= '" + cuotaProindiviso + "', CuotaOtros= '" + cuotaOtros + "', Concepto= '" + txtConcepto + "', Anual='" + Anual + "', Importe=" + Importe + ", Metodo='" + Metodo + "', Periodo='" + Periodo + "', AnualE='" + Anual2 + "', ImporteE= " + Importe2 + ", MetodoE='" + Metodo2 + "', PeriodoE='" + Periodo2 + "', Foto=@Foto where ClaveCondominio= '" + txtClave + "'", cn);
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
        //___________________________________________________________
        public void RegistroFormaPago2(string txtClave, string txtDescripcion, string txtCalle, string txtNoExterior, string txtNoInterior, string txtColonia, string txtMunicipio, string txtCodigoPostal, string txtCiudad, string txtestado, string txtPais, string txtTelefono, string txtReferencias, string txtCaracteristicas, RadioButton rdbGenral, RadioButton rdbProIndiviso, RadioButton rdbOtros, string txtConcepto, string Anual, decimal Importe, string Metodo, string Periodo, string Anual2, decimal Importe2, string Metodo2, string Periodo2, PictureBox Foto)
        {
            string mensaje = string.Empty;
            int contador = 0;
            string ClaveDep = string.Empty;
            string NombreDep = string.Empty;
            string Caracteristicas = string.Empty;
            decimal ProIndiviso = 0;
            string cuotaProindiviso = string.Empty;
            string cuotaGeneral = string.Empty;
            string cuotaOtros = string.Empty;
            decimal Mantenimiento = 0;
            string Del = string.Empty;
            string Al = string.Empty;

            try
            {
                cmd = new SqlCommand("select * from Condominio where ClaveCondominio='" + txtClave + "'", cn);
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
                        if (rdbGenral.Checked == true)
                        {
                            cuotaGeneral = "Si";
                        }
                        else if (rdbProIndiviso.Checked == true)
                        {
                            cuotaProindiviso = "Si";
                        }
                        else if (rdbOtros.Checked == true)
                        {
                            cuotaOtros = "Si";
                        }

                        cmd = new SqlCommand("Insert into Condominio (ClaveCondominio, Descripcion, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Estado, Pais, Telefono, Referencias, Caracteristicas, CuotaGeneral, CuotaProIndiviso, CuotaOtros, Concepto, Anual, Importe, Metodo, Periodo,  AnualE, ImporteE, MetodoE, PeriodoE) values ('" + txtClave + "',  '" + txtDescripcion + "',  '" + txtCalle + "',  '" + txtNoExterior + "',  '" + txtNoInterior + "',  '" + txtColonia + "',  '" + txtMunicipio + "',  '" + txtCodigoPostal + "',  '" + txtCiudad + "', '" + txtestado + "', '" + txtPais + "',  '" + txtTelefono + "',  '" + txtReferencias + "',  '" + txtCaracteristicas + "', '" + cuotaGeneral + "', '" + cuotaProindiviso + "', '" + cuotaOtros + "', '" + txtConcepto + "', '" + Anual + "', " + Importe + ", '" + Metodo + "', '" + Periodo + "', '" + Anual2 + "', " + Importe2 + ", '" + Metodo2 + "', '" + Periodo2 + "')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        if (rdbGenral.Checked == true)
                        {
                            cuotaGeneral = "Si";
                        }
                        else if (rdbProIndiviso.Checked == true)
                        {
                            cuotaProindiviso = "Si";
                        }
                        else if (rdbOtros.Checked == true)
                        {
                            cuotaOtros = "Si";
                        }

                        cmd = new SqlCommand("Insert into Condominio (ClaveCondominio, Descripcion, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Estado, Pais, Telefono, Referencias, Caracteristicas, CuotaGeneral, CuotaProIndiviso, CuotaOtros, Concepto, Anual, Importe, Metodo, Periodo, AnualE, ImporteE, MetodoE, PeriodoE, Foto) values ('" + txtClave + "',  '" + txtDescripcion + "',  '" + txtCalle + "',  '" + txtNoExterior + "',  '" + txtNoInterior + "',  '" + txtColonia + "',  '" + txtMunicipio + "',  '" + txtCodigoPostal + "',  '" + txtCiudad + "',  '" + txtestado + "', '" + txtPais + "',  '" + txtTelefono + "',  '" + txtReferencias + "',  '" + txtCaracteristicas + "', '" + cuotaGeneral + "', '" + cuotaProindiviso + "', '" + cuotaOtros + "', '" + txtConcepto + "', '" + Anual + "', " + Importe + ", '" + Metodo + "', '" + Periodo + "', '" + Anual2 + "', " + Importe2 + ", '" + Metodo2 + "', '" + Periodo2 + "', @Foto)", cn);
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
                        if (Foto.Image == null)
                        {
                            if (rdbGenral.Checked == true)
                            {
                                cuotaGeneral = "Si";
                            }
                            else if (rdbProIndiviso.Checked == true)
                            {
                                cuotaProindiviso = "Si";
                            }
                            else if (rdbOtros.Checked == true)
                            {
                                cuotaOtros = "Si";
                            }

                            cmd = new SqlCommand("Update Condominio set  Descripcion= '" + txtDescripcion + "', Calle= '" + txtCalle + "', NoExterior= '" + txtNoExterior + "', NoInterior='" + txtNoInterior + "', Colonia= '" + txtColonia + "', Municipio= '" + txtMunicipio + "', CodigoPostal='" + txtCodigoPostal + "', Ciudad= '" + txtCiudad + "', Estado='" + txtestado + "', Pais= '" + txtPais + "', Telefono= '" + txtTelefono + "', Referencias= '" + txtReferencias + "', Caracteristicas='" + txtCaracteristicas + "', CuotaGeneral= '" + cuotaGeneral + "', CuotaProIndiviso= '" + cuotaProindiviso + "', CuotaOtros= '" + cuotaOtros + "', Concepto= '" + txtConcepto + "', Anual='" + Anual + "', Importe=" + Importe + ", Metodo='" + Metodo + "', Periodo='" + Periodo + "', AnualE='" + Anual2 + "', ImporteE= " + Importe2 + ", MetodoE='" + Metodo2 + "', PeriodoE='" + Periodo2 + "' where ClaveCondominio= '" + txtClave + "'", cn);
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                        else
                        {
                            if (rdbGenral.Checked == true)
                            {
                                cuotaGeneral = "Si";
                            }
                            else if (rdbProIndiviso.Checked == true)
                            {
                                cuotaProindiviso = "Si";
                            }
                            else if (rdbOtros.Checked == true)
                            {
                                cuotaOtros = "Si";
                            }

                            cmd = new SqlCommand("Update Condominio set  Descripcion= '" + txtDescripcion + "', Calle= '" + txtCalle + "', NoExterior= '" + txtNoExterior + "', NoInterior='" + txtNoInterior + "', Colonia= '" + txtColonia + "', Municipio= '" + txtMunicipio + "', CodigoPostal='" + txtCodigoPostal + "', Ciudad= '" + txtCiudad + "',  Estado='" + txtestado + "', Pais= '" + txtPais + "', Telefono= '" + txtTelefono + "', Referencias= '" + txtReferencias + "', Caracteristicas='" + txtCaracteristicas + "', CuotaGeneral= '" + cuotaGeneral + "', CuotaProIndiviso= '" + cuotaProindiviso + "', CuotaOtros= '" + cuotaOtros + "', Concepto= '" + txtConcepto + "', Anual='" + Anual + "', Importe=" + Importe + ", Metodo='" + Metodo + "', Periodo='" + Periodo + "', AnualE='" + Anual2 + "', ImporteE= " + Importe2 + ", MetodoE='" + Metodo2 + "', PeriodoE='" + Periodo2 + "', Foto=@Foto where ClaveCondominio= '" + txtClave + "'", cn);
                            cmd.Parameters.Add("@Foto", SqlDbType.Image);
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Foto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            cmd.Parameters["@Foto"].Value = ms.GetBuffer();
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }

        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string agregarEstructura(string ClaveDep, string NombreDep, string txtClave, string Caracteristicas, decimal ProIndiviso, decimal Mantenimiento, string Del, string Al)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Condominios_SubCondominios where Clave='" + ClaveDep + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Condominios_SubCondominios (Clave, Descripcion, Condominio, Caracteristicas, ProIndiviso, CuotaMantenimiento, Del, Al) values ('" + ClaveDep + "', '" + NombreDep + "', " + txtClave + ", '" + Caracteristicas + "', " + ProIndiviso + ", " + Mantenimiento + ", '" + Del + "', '" + Al + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado";
                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Estructura del Condominio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update Condominios_SubCondominios set Descripcion= '" + NombreDep + "', Condominio=" + txtClave + ", Caracteristicas='" + Caracteristicas + "', ProIndiviso=" + ProIndiviso + ", CuotaMantenimiento='" + Mantenimiento + "', Del='" + Del + "', Al='" + Al + "' where Clave='" + ClaveDep + "'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado";
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
        //Obtener la clave consecutiva
        public int ClaveEstructura(string txtClave, TextBox clavedep)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select (count(*) + 1) as numero from Condominios_SubCondominios where Condominio=" + txtClave + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    clavedep.Text = dr["numero"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
            return contador;
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarCondominios(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Condominio", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveCondominio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaCondominioSeleccionado(string txtClave, Guna2TextBox txtDescripcion, Guna2TextBox txtCalle, Guna2TextBox txtNoExterior, Guna2TextBox txtNoInterior, Guna2TextBox txtColonia, Guna2TextBox txtMunicipio, Guna2TextBox txtCodigoPostal, Guna2TextBox txtCiudad, Guna2TextBox txtEstado, Guna2TextBox txtPais, Guna2TextBox txtTelefono, TextBox txtReferencias, Guna2TextBox txtCaracteristicas, RadioButton rdbGenral, RadioButton rdbProIndiviso, RadioButton rdbOtros, TextBox txtConcepto, ComboBox txtAnual, Guna2TextBox Importe, ComboBox cmbMetodo, RadioButton rdmensual, RadioButton rdbimestral, RadioButton rdtrimestral, RadioButton rdanual, RadioButton rdCuatrimestral, RadioButton rdSemestral, ComboBox txtAnual2, Guna2TextBox txtImporteEx, ComboBox cmbMetodo2, RadioButton rdMensual2, RadioButton rdBimestral2, RadioButton rdTrimestral2, RadioButton rdAnual2, RadioButton rdCuatrimestral2, RadioButton rdSemestral2, PictureBox Foto, DataGridView dgv)
        {
            try
            {
                cmd = new SqlCommand("Select * from Condominio where ClaveCondominio='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    txtCalle.Text = dr["Calle"].ToString();
                    txtNoExterior.Text = dr["NoExterior"].ToString();
                    txtNoInterior.Text = dr["NoInterior"].ToString();
                    txtColonia.Text = dr["Colonia"].ToString();
                    txtMunicipio.Text = dr["Municipio"].ToString();
                    txtCodigoPostal.Text = dr["CodigoPostal"].ToString();
                    txtCiudad.Text = dr["Ciudad"].ToString();
                    txtEstado.Text = dr["Estado"].ToString();
                    txtPais.Text = dr["Pais"].ToString();
                    txtTelefono.Text = dr["Telefono"].ToString();
                    txtReferencias.Text = dr["Referencias"].ToString();
                    txtCaracteristicas.Text = dr["Caracteristicas"].ToString();
                    txtConcepto.Text = dr["Concepto"].ToString();
                    txtAnual.Text = dr["Anual"].ToString();
                    Importe.Text = dr["Importe"].ToString();
                    cmbMetodo.Text = dr["Metodo"].ToString();
                    txtAnual2.Text = dr["AnualE"].ToString();
                    txtImporteEx.Text = dr["ImporteE"].ToString();
                    cmbMetodo2.Text = dr["MetodoE"].ToString();

                    string cuotaGeneral = dr["CuotaGeneral"].ToString();
                    string cuotaProIndiviso = dr["CuotaProIndiviso"].ToString();
                    string cuotaOtros = dr["CuotaProIndiviso"].ToString();

                    if (cuotaGeneral == "Si")
                    {
                        rdbGenral.Checked = true;
                    }
                    else if (cuotaProIndiviso == "Si")
                    {
                        rdbProIndiviso.Checked = true;
                    }
                    else if (cuotaOtros == "Si")
                    {
                        rdbOtros.Checked = true;
                    }

                    string Periodo = dr["Periodo"].ToString();
                    if (Periodo == "Mensual")
                    {
                        rdmensual.Checked = true;
                    }
                    else if (Periodo == "Bimestral")
                    {
                        rdbimestral.Checked = true;
                    }
                    else if (Periodo == "Trimestral")
                    {
                        rdtrimestral.Checked = true;
                    }
                    else if (Periodo == "Anual")
                    {
                        rdanual.Checked = true;
                    }
                    else if (Periodo == "Cutrimestral")
                    {
                        rdCuatrimestral.Checked = true;
                    }
                    else if (Periodo == "Semestral")
                    {
                        rdSemestral.Checked = true;
                    }

                    string Periodo2 = dr["PeriodoE"].ToString();
                    if (Periodo2 == "Mensual")
                    {
                        rdMensual2.Checked = true;
                    }
                    else if (Periodo2 == "Bimestral")
                    {
                        rdBimestral2.Checked = true;
                    }
                    else if (Periodo2 == "Trimestral")
                    {
                        rdTrimestral2.Checked = true;
                    }
                    else if (Periodo2 == "Anual")
                    {
                        rdAnual2.Checked = true;
                    }
                    else if (Periodo2 == "Cutrimestral")
                    {
                        rdCuatrimestral2.Checked = true;
                    }
                    else if (Periodo2 == "Semestral")
                    {
                        rdSemestral2.Checked = true;
                    }

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
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select CS.*, (select ClavePropietario from Propietarios_Condominios where ClaveCondominio=CS.Clave) as Propietario, CONVERT(INT, SUBSTRING (Clave, 3,1000)) as Orden from Condominios_SubCondominios as CS where Condominio = '" + txtClave + "' order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Caracteristicas"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Propietario"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["ProIndiviso"]).ToString("N", formato);
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["CuotaMantenimiento"]).ToString("N", formato);
                    dgv.Rows[n].Cells[6].Value = (Convert.ToDateTime(item["Del"]).ToString("dd/MM/yyyy")).ToString();
                    dgv.Rows[n].Cells[7].Value = (Convert.ToDateTime(item["Al"]).ToString("dd/MM/yyyy")).ToString();
                }

            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaConcepto( TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("Select Concepto from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Concepto"].ToString();

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
        public void ConsultaConceptoE(TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("Select ConceptoExt from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["ConceptoExt"].ToString();

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
        public void ConsultaPresupuesto(string ejercicio, string concepto, Guna2TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("select P.Total, CP.* from Presupuesto as P, ConceptoPresupuesto as CP where P.Concepto=CP.Clave and CP.VincularA='"+ concepto + "' and P.Ejercicio='"+ ejercicio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Total"].ToString();

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
        public void ConsultaPresupuesto2(string ejercicio, string concepto, Guna2TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("select P.Total, CP.* from Presupuesto as P, ConceptoPresupuesto as CP where P.Concepto=CP.Clave and CP.Clave='" + concepto + "' and P.Ejercicio='" + ejercicio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Total"].ToString();

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
        public void Consultaestructura(string txtClave, TextBox txtDescripcion, TextBox txtCaracteristicas, TextBox txtProIndiviso, TextBox txtCuotaMantenimiento, DateTimePicker dtDel, DateTimePicker dtAl)
        {
            try
            {
                cmd = new SqlCommand("Select * from Condominios_SubCondominios where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    txtCaracteristicas.Text = dr["Caracteristicas"].ToString();
                    txtProIndiviso.Text = dr["ProIndiviso"].ToString();
                    txtCuotaMantenimiento.Text = dr["CuotaMantenimiento"].ToString();
                    dtDel.Text = Convert.ToDateTime(dr["Del"]).ToString("yyyy/MM/dd");
                    dtAl.Text = Convert.ToDateTime(dr["Al"]).ToString("yyyy/MM/dd");
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
        public void ConsultaestructuraCondominioSeleccionado(string txtClave, DataGridView dgv)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select CS.*, (select ClavePropietario from Propietarios_Condominios where ClaveCondominio=CS.Clave) as Propietario, CONVERT(INT, SUBSTRING (Clave, 3,1000)) as Orden from Condominios_SubCondominios as CS where Condominio = '" + txtClave + "' order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Caracteristicas"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Propietario"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["ProIndiviso"]).ToString("N", formato);
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["CuotaMantenimiento"]).ToString("N", formato);
                    dgv.Rows[n].Cells[6].Value = (Convert.ToDateTime(item["Del"]).ToString("dd/MM/yyyy")).ToString();
                    dgv.Rows[n].Cells[7].Value = (Convert.ToDateTime(item["Al"]).ToString("dd/MM/yyyy")).ToString();
                }

            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string EliminarDepartamentoArea(string txtClave, string Condominio)
        {
            int contador = 0;
            string mensaje = "No es posible eliminar registro si cuenta con propietario";

            try
            {
                cmd = new SqlCommand("select * from Propietarios_Condominios where ClaveCondominio='" + Condominio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    contador = 0;
                    cmd = new SqlCommand("Delete Condominios_SubCondominios where Clave='" + txtClave + "'", cn);
                    dr = cmd.ExecuteReader();
                    mensaje = "Eliminado";
                    Eliminado = 0;
                    dr.Close();
                }
                else
                {
                    mensaje = "Elimine las areas asignadas al departamento para continuar";
                    Eliminado = 1;
                }
            }
            catch (Exception)
            {

            }
            return mensaje;
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public int ConsultaExistencia(string txtclave)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("Select * from Condominios_SubCondominios where Clave='" + txtclave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return contador;
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public int ConsultaExistencia2(string txtclave)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("Select * from Condominio where ClaveCondominio='" + txtclave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return contador;
        }
        //_____________________________________________________________________________________________________
        public void CargarAreas(string Clave, DataGridView dgv)
        {
            try
            {
                ArrayList ListaConceptos = new ArrayList();
                ArrayList ListaConceptos2 = new ArrayList();
                ArrayList ListaConceptos3 = new ArrayList();
                ArrayList ListaConceptos4 = new ArrayList();

                string desc = "";
                string Reserva = "";
                string Cuota = "";
                string Deposito = "";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("select ClaveAreas, Descripcion from AreasComunes", cn);
                dt = new DataTable();
                da.Fill(dt);

                cmd = new SqlCommand("select * from Condominio_Areas where Condominio ='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListaConceptos.Add((string)dr["Clave"]);
                    ListaConceptos2.Add((string)dr["Reserva"]);
                    ListaConceptos3.Add((string)dr["Cuota"]);
                    ListaConceptos4.Add((string)dr["Deposito"]);
                    //MessageBox.Show((string)dr["ClaveConcep"]);
                }
                dr.Close();

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add(); //agrega una nueva fila
                    foreach (object item2 in ListaConceptos)
                    {
                        desc = item2.ToString();
                        if (item["ClaveAreas"].ToString() == desc)
                        {
                            dgv.Rows[n].Cells[0].Value = true;

                            foreach (object item3 in ListaConceptos2)
                            {
                                Reserva = item3.ToString();
                                if (Reserva == "True")
                                {
                                    dgv.Rows[n].Cells[3].Value = true;
                                }
                                ListaConceptos2.Remove(item3);
                                break;
                            }

                            foreach (object item4 in ListaConceptos3)
                            {
                                Cuota = item4.ToString();
                                if (Cuota == "True")
                                {
                                    dgv.Rows[n].Cells[4].Value = true;
                                }
                                ListaConceptos3.Remove(item4);
                                break;
                            }

                            foreach (object item5 in ListaConceptos4)
                            {
                                Deposito = item5.ToString();
                                if (Deposito == "True")
                                {
                                    dgv.Rows[n].Cells[5].Value = true;
                                }
                                ListaConceptos4.Remove(item5);
                                break;
                            }

                        }
                    }

                    dgv.Rows[n].Cells[1].Value = item["ClaveAreas"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos3" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistrarAreasSeleccionadas(string txtClave, string clavea, string areacomun, string reserva, string cuota, string deposito)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Condominio_Areas where Condominio='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Condominio_Areas (Clave, Nombre, Area, Condominio, Reserva, Cuota, Deposito) values ('" + clavea + "', '" + areacomun + "', '" + clavea + "', '" + txtClave + "', '" + reserva + "', '" + cuota + "', '" + deposito + "')", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro Guardado";
                }

                else if (contador > 0)
                {

                    cmd = new SqlCommand("Delete Condominio_Areas where Clave='" + clavea + "' and Condominio='" + txtClave + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Insert into Condominio_Areas (Clave, Nombre, Area, Condominio, Reserva, Cuota, Deposito) values ('" + clavea + "', '" + areacomun + "', '" + clavea + "', '" + txtClave + "', '" + reserva + "', '" + cuota + "', '" + deposito + "')", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro Modificado";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //___________________________________________________________________________________________
        public void SeleccionarConcepto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (Clave + ' - ' + Descripcion) as Concepto from ConceptosIngreso", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________
        public string[] InformacionConcepto(string Documento)
        {
            cmd = new SqlCommand("Select * from ConceptosIngreso where (Clave + ' - ' + Descripcion)= '" + Documento + "'", cn);
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
        //_________________________________________________________________________________________
        public string[] InformacionConcepto2(string Documento)
        {
            cmd = new SqlCommand("Select (Clave + ' - ' + Descripcion) from ConceptosIngreso where Clave= '" + Documento + "'", cn);
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
    }
}
