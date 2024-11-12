using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PuntoVentas.Clases.DatosEmpresa
{
    class DBDatosEmpresa
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBDatosEmpresa()
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
        // registrar empresa 
        public string RegistroEmpresa(string txtRazonSocial, string txtNombreComercial, string txtRFC, string txtTelefono1, string txtTelefono2, string txtTelefono3, string txtCorreo, string Servidor,  string txtContraseña, string txtPaginaWeb, string txtCalleNumero, string txtColonia, string txtMunicipio, string txtEstado, string txtCodigoPostal, string txtPais, string txtReferencias, RadioButton rdbSiRecargo, RadioButton rdbNoRecargo, RadioButton rdbSiDescuento, RadioButton rdbNoDescuento, string ReciboAuto, string txtClave, string txtLeyendaTicket, decimal MontoMaximo, string DiasPlazo, RadioButton FechaCobranzaSi, RadioButton FechaCobranzaNo, decimal txtMontoMaximoD, string Concepto, string DocumentoExt, string ConceptoEXt, string Ruta, string DocumentoAnt, string ConceptoAnt, PictureBox Foto, string Puerto, string Host, string Ssl)
        {
            string mensaje = "";
            int contador = 0;
            string Recargos = string.Empty;
            string Descuento = string.Empty;
            string Fecha = string.Empty;

            try
            {
                cmd = new SqlCommand("select * from DatosEmpresa", cn);
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
                        if (rdbSiRecargo.Checked == true)
                        {
                            Recargos = "Si";
                        }
                        else
                        {
                            Recargos = "No";
                        }

                        if (rdbSiDescuento.Checked == true)
                        {
                            Descuento = "Si";
                        }
                        else
                        {
                            Descuento = "No";
                        }

                        if (FechaCobranzaSi.Checked == true)
                        {
                            Fecha = "Si";
                        }
                        else
                        {
                            Fecha = "No";
                        }

                        cmd = new SqlCommand("Insert into DatosEmpresa ( RazonSocial, NombreComercial, RFC, Telefono1, Telefono2, Telefono3, Correo, Servidor, Contraseña, PaginaWeb, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Referencias, Recargos, Descuentos, RecibosAutomaticos, Documento, LeyendaTicket, MontoMaximo, DiasPlazo, FechaCobranza, MontoMaximoD, Concepto, DocumentoExt, ConceptoExt, Ruta, DocumentoAnt, ConceptoAnt, Puerto, Host, Ssl) values ('" + txtRazonSocial + "', '" + txtNombreComercial + "', '" + txtRFC + "', '" + txtTelefono1 + "', '" + txtTelefono2 + "', '" + txtTelefono3 + "', '" + txtCorreo + "', '" + Servidor + "','" + txtContraseña + "', '" + txtPaginaWeb + "', '" + txtCalleNumero + "', '" + txtColonia + "', '" + txtMunicipio + "', '" + txtEstado + "', '" + txtCodigoPostal + "', '" + txtPais + "', '" + txtReferencias + "', '"+Recargos+ "', '" + Descuento + "','" + ReciboAuto + "', '"+txtClave+"', '" + txtLeyendaTicket + "', " +MontoMaximo+ ", '"+DiasPlazo+"', '"+Fecha+"', "+txtMontoMaximoD+", '"+Concepto+"', '"+DocumentoExt+"', '"+ConceptoEXt+"', '"+Ruta+ "', '" + DocumentoAnt + "', '" + ConceptoAnt + "', '"+Puerto+"', '"+Host+"', '"+Ssl+"')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        if (rdbSiRecargo.Checked == true)
                        {
                            Recargos = "Si";
                        }
                        else
                        {
                            Recargos = "No";
                        }

                        if (rdbSiDescuento.Checked == true)
                        {
                            Descuento = "Si";
                        }
                        else
                        {
                            Descuento = "No";
                        }

                        if (FechaCobranzaSi.Checked == true)
                        {
                            Fecha = "Si";
                        }
                        else
                        {
                            Fecha = "No";
                        }

                        cmd = new SqlCommand("Insert into DatosEmpresa ( RazonSocial, NombreComercial, RFC, Telefono1, Telefono2, Telefono3, Correo, Servidor, Contraseña, PaginaWeb, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Referencias, Recargos, Descuentos, RecibosAutomaticos, Documento, LeyendaTicket, MontoMaximo, DiasPlazo, FechaCobranza, MontoMaximoD, Concepto, DocumentoExt, ConceptoEx, Ruta, DocumentoAnt, ConceptoAnt, Foto, Puerto, Host, Ssl) values ('" + txtRazonSocial + "', '" + txtNombreComercial + "', '" + txtRFC + "', '" + txtTelefono1 + "', '" + txtTelefono2 + "', '" + txtTelefono3 + "', '" + txtCorreo + "','" + Servidor + "', '" + txtContraseña + "', '" + txtPaginaWeb + "', '" + txtCalleNumero + "', '" + txtColonia + "', '" + txtMunicipio + "', '" + txtEstado + "', '" + txtCodigoPostal + "', '" + txtPais + "', '" + txtReferencias + "', '" + Recargos + "', '" + Descuento + "', '" + ReciboAuto + "', '" + txtClave + "', '" + txtLeyendaTicket + "'," + MontoMaximo + ", '" + DiasPlazo + "', '" + Fecha + "', "+txtMontoMaximoD+", '"+Concepto+"', '"+DocumentoExt+"', '"+ConceptoEXt+"', '"+Ruta+ "', '" + DocumentoAnt + "', '" + ConceptoAnt + "', @Foto, '"+Puerto+"', '"+Host+"', '"+Ssl+"')", cn);
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
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Empresa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Foto.Image == null)
                        {
                            if (rdbSiRecargo.Checked == true)
                            {
                                Recargos = "Si";
                            }
                            else
                            {
                                Recargos = "No";
                            }

                            if (rdbSiDescuento.Checked == true)
                            {
                                Descuento = "Si";
                            }
                            else
                            {
                                Descuento = "No";
                            }

                            if (FechaCobranzaSi.Checked == true)
                            {
                                Fecha = "Si";
                            }
                            else
                            {
                                Fecha = "No";
                            }

                            cmd = new SqlCommand("Update DatosEmpresa set   RazonSocial = '" + txtRazonSocial + "',NombreComercial='" + txtNombreComercial + "', RFC='" + txtRFC + "', Telefono1='" + txtTelefono1 + "', Telefono2='" + txtTelefono2 + "', Telefono3='" + txtTelefono3 + "', Correo='" + txtCorreo + "', Servidor='" + Servidor + "', Contraseña='" + txtContraseña+"', PaginaWeb='" + txtPaginaWeb + "', CalleNumero= '" + txtCalleNumero + "', Colonia='" + txtColonia + "', Municipio='" + txtMunicipio + "', Estado='" + txtEstado + "', CodigoPostal='" + txtCodigoPostal + "', Pais='" + txtPais + "', Referencias='" + txtReferencias + "', Recargos='"+Recargos+"', Descuentos='"+Descuento+ "', RecibosAutomaticos='"+ReciboAuto+ "', Documento='" + txtClave + "', LeyendaTicket='" + txtLeyendaTicket + "', MontoMaximo="+MontoMaximo+", DiasPlazo='"+DiasPlazo+"', FechaCobranza='"+Fecha+ "', MontoMaximoD=" + txtMontoMaximoD + ", Concepto='"+Concepto+"', DocumentoExt='"+DocumentoExt+"', ConceptoExt='"+ConceptoEXt+"', Ruta='"+Ruta+ "', DocumentoAnt='" + DocumentoAnt + "', ConceptoAnt='" + ConceptoAnt + "', Puerto='"+Puerto+"', Host='"+Host+"', Ssl='"+Ssl+"'", cn);
                            cmd.ExecuteNonQuery();

                            mensaje = "Registro modificado.";
                        }
                        else
                        {
                            if (rdbSiRecargo.Checked == true)
                            {
                                Recargos = "Si";
                            }
                            else
                            {
                                Recargos = "No";
                            }

                            if (rdbSiDescuento.Checked == true)
                            {
                                Descuento = "Si";
                            }
                            else
                            {
                                Descuento = "No";
                            }

                            if (FechaCobranzaSi.Checked == true)
                            {
                                Fecha = "Si";
                            }
                            else
                            {
                                Fecha = "No";
                            }

                            cmd = new SqlCommand("Update DatosEmpresa set  RazonSocial = '" + txtRazonSocial + "', NombreComercial='" + txtNombreComercial + "', RFC='" + txtRFC + "', Telefono1='" + txtTelefono1 + "', Telefono2='" + txtTelefono2 + "', Telefono3='" + txtTelefono3 + "', Correo='" + txtCorreo + "',Servidor='" + Servidor + "', Contraseña='" + txtContraseña+"', PaginaWeb='" + txtPaginaWeb + "', CalleNumero= '" + txtCalleNumero + "', Colonia='" + txtColonia + "', Municipio='" + txtMunicipio + "', Estado='" + txtEstado + "', CodigoPostal='" + txtCodigoPostal + "', Pais='" + txtPais + "', Referencias='" + txtReferencias + "', Recargos='" + Recargos + "', Descuentos='" + Descuento + "', RecibosAutomaticos='" + ReciboAuto + "',  Documento='" + txtClave + "', LeyendaTicket='" + txtLeyendaTicket + "',MontoMaximo=" + MontoMaximo + ", DiasPlazo='" + DiasPlazo + "', FechaCobranza='" + Fecha + "', MontoMaximoD=" + txtMontoMaximoD + ", Concepto='"+Concepto+ "',  DocumentoExt='" + DocumentoExt + "', ConceptoExt='" + ConceptoEXt + "', Ruta='" + Ruta + "', DocumentoAnt='" + DocumentoAnt + "', ConceptoAnt='" + ConceptoAnt + "', Foto=@Foto, Puerto='"+Puerto+"', Host='"+Host+"', Ssl='"+Ssl+"'", cn);
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
        // Eliminar empresa  
        public string EliminarEmpresa(string txtRazonSocial)
        {
            string mensaje = "";

            try
            {
                cmd = new SqlCommand("Delete DatosEmpresa where RazonSocial='"+txtRazonSocial+"'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro eliminado.";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }

        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void ConsultaUsuarioSeleccionado(Guna.UI2.WinForms.Guna2TextBox txtRazonSocial, Guna.UI2.WinForms.Guna2TextBox txtNombreComercial, Guna.UI2.WinForms.Guna2TextBox txtRFC, Guna.UI2.WinForms.Guna2TextBox txtTelefono1, Guna.UI2.WinForms.Guna2TextBox txtTelefono2, Guna.UI2.WinForms.Guna2TextBox txtTelefono3, Guna.UI2.WinForms.Guna2TextBox txtCorreo, Guna2TextBox Servidor, Guna.UI2.WinForms.Guna2TextBox txtContraseña, Guna.UI2.WinForms.Guna2TextBox txtPaginaWeb, Guna.UI2.WinForms.Guna2TextBox txtCalleNumero, Guna.UI2.WinForms.Guna2TextBox txtColonia, Guna.UI2.WinForms.Guna2TextBox txtMunicipio, Guna.UI2.WinForms.Guna2TextBox txtEstado, Guna.UI2.WinForms.Guna2TextBox txtCodigoPostal, Guna.UI2.WinForms.Guna2TextBox txtPais, Guna.UI2.WinForms.Guna2TextBox txtReferencias, RadioButton rdbSiRecargos, RadioButton rdbNoRecargos, RadioButton rdbSiDescuentos, RadioButton rdbNoDescuentos, RadioButton rdbConcepto, RadioButton rdbEstructura, RadioButton rdbProIndiviso, TextBox txtClave, Guna.UI2.WinForms.Guna2TextBox txtLeyendaTicket, TextBox txtMontoMaximo, TextBox txtDiasPlazo, RadioButton rdfechasi, RadioButton rdFechano, TextBox txtMontoMaximoD, TextBox txtConcepto, TextBox txtDocumentoExt, TextBox txtConceptoExt, TextBox txtDocumentoAnt, TextBox txtConceptoAnt, Guna.UI2.WinForms.Guna2TextBox txtRuta, PictureBox Foto, Guna2TextBox txtPuerto, Guna2TextBox txtHost, ComboBox cmbSSL)
        {
            try
            {
                cmd = new SqlCommand("Select * from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtRazonSocial.Text = dr["RazonSocial"].ToString();
                    txtNombreComercial.Text = dr["NombreComercial"].ToString();
                    txtRFC.Text = dr["RFC"].ToString();
                    txtTelefono1.Text = dr["Telefono1"].ToString();
                    txtTelefono2.Text = dr["Telefono2"].ToString();
                    txtTelefono3.Text = dr["Telefono3"].ToString();
                    txtCorreo.Text = dr["Correo"].ToString();
                    Servidor.Text = dr["Servidor"].ToString();
                    txtContraseña.Text = dr["Contraseña"].ToString();
                    txtPaginaWeb.Text = dr["PaginaWeb"].ToString();
                    txtCalleNumero.Text = dr["CalleNumero"].ToString();
                    txtColonia.Text = dr["Colonia"].ToString();
                    txtMunicipio.Text = dr["Municipio"].ToString();
                    txtEstado.Text = dr["Estado"].ToString();
                    txtCodigoPostal.Text = dr["CodigoPostal"].ToString();
                    txtPais.Text = dr["Pais"].ToString();
                    txtReferencias.Text = dr["Referencias"].ToString();
                    txtClave.Text = dr["Documento"].ToString();
                    txtLeyendaTicket.Text = dr["LeyendaTicket"].ToString();
                    txtMontoMaximo.Text = dr["MontoMaximo"].ToString();
                    txtDiasPlazo.Text = dr["DiasPlazo"].ToString();
                    txtMontoMaximoD.Text = dr["MontoMaximoD"].ToString();
                    txtConcepto.Text = dr["Concepto"].ToString();
                    txtDocumentoExt.Text = dr["DocumentoExt"].ToString();
                    txtConceptoExt.Text = dr["ConceptoExt"].ToString();
                    txtDocumentoAnt.Text = dr["DocumentoAnt"].ToString();
                    txtConceptoAnt.Text = dr["ConceptoAnt"].ToString();
                    txtRuta.Text = dr["Ruta"].ToString();

                    string Recargos = dr["Recargos"].ToString();
                    string Descuento = dr["Descuentos"].ToString();
                    string ReciboAuto = dr["RecibosAutomaticos"].ToString();
                    string Fecha = dr["FechaCobranza"].ToString();
                    txtPuerto.Text = dr["puerto"].ToString();
                    txtHost.Text = dr["host"].ToString();
                    cmbSSL.Text = dr["Ssl"].ToString();

                    if (Recargos == "Si")
                    {
                        rdbSiRecargos.Checked = true;
                    }
                    else
                    {
                        rdbNoRecargos.Checked = true;

                    }

                    if (Descuento == "Si")
                    {
                        rdbSiDescuentos.Checked = true;
                    }
                    else
                    {
                        rdbNoDescuentos.Checked = true;

                    }

                    if (Fecha == "Si")
                    {
                        rdfechasi.Checked = true;
                    }
                    else
                    {
                        rdFechano.Checked = true;

                    }

                    if (ReciboAuto == "Concepto")
                    {
                        rdbConcepto.Checked = true;
                    }
                    else if (ReciboAuto == "Estructura")
                    {
                        rdbEstructura.Checked = true;
                    }
                    else if (ReciboAuto == "ProIndiviso")
                    {
                        rdbProIndiviso.Checked = true;
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
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
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
        //_________________________________________________________________________________________
        public string[] InformacionDocumento2(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where Clave= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[3].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_____________________________________________________________________________________
        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Venta' and Clase='Remision'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        //_________________________________________________________________________________________
        public string[] InformacionDocumento(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[2].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoRecibo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Descripcion) as Clave from ConceptosIngreso", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoReciboEX(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Descripcion) as Clave from ConceptoPresupuesto where Clase='Ingreso'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionRecibo(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select Clave from ConceptosIngreso where (Clave + ' - ' + Descripcion)= '" + Recibo + "'", cn);
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
        public string[] InformacionRecibo2(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select Descripcion from ConceptosIngreso where Clave= '" + Recibo + "'", cn);
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
        public string[] InformacionReciboE(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select Clave from ConceptoPresupuesto where (Clave + ' - ' + Descripcion)= '" + Recibo + "'", cn);
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
      /*  public string[] InformacionReciboE2(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select Descripcion from ConceptoPresupuesto where Clave= '" + Recibo + "'", cn);
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
        }*/

        //_______________________________________________________________________________________________
        public void InformacionReciboE2(string Recibo,TextBox Descripcion )
        {
         
            cmd = new SqlCommand("Select Descripcion from ConceptoPresupuesto where Clave= '" + Recibo + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Descripcion.Text  = dr[0].ToString();
            }
            dr.Close();
        }
        public string[] CorreoContra()
        {
            string[] resultado = null;

            try
            {
                cmd = new SqlCommand("select Correo, Servidor, Contraseña, Puerto, Host, Ssl, Ruta from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        // Cargar los valores en el array
                        resultado = new string[]
                        {
                    dr["Correo"].ToString(),
                    dr["Servidor"].ToString(),
                    dr["Contraseña"].ToString(),
                    dr["Puerto"].ToString(),
                    dr["Host"].ToString(),
                    dr["Ssl"].ToString(),
                    dr["Ruta"].ToString()
                        };
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return resultado;
        }
    }
}
