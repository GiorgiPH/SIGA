using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace Condominios.Clases.Propietarios
{
    class DBPropietario
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

        public DBPropietario()
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
        //________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClavePropietarioSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(IdPropietario) from Propietarios", cn);
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
        //_______________________________________________________________________________---------
        public int ConsultaPropieddad(string ClavePropietario)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("Select * from Propietarios_Condominios where ClavePropietario='"+ClavePropietario+"'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
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
        //_______________________________________________________________________________---------
        public decimal ConsultaSaldo(string ClavePropietario)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select sum(saldo) as Saldo from Recibo where ClavePropietario='" + ClavePropietario + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
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
        //_______________________________________________________________________________---------
        public decimal ConsultaSaldoEliminarPropiedad(string ClavePropietario, string Propiedad)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select Saldo from Recibo where ClavePropietario='" + ClavePropietario + "' and Propiedad='"+Propiedad+"' and Saldo>0.00", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
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
        //_________________________________________________________________________________________________________________________--
        // registrar Empleado 
        public string RegistroPropietarios(string IdPropietario, string RazonSocial, string TipoCliente, string RFC, string Calle, string NoExterior, string NoInterior, string Colonia, string Municipio, string CodigoPostal, string Ciudad, string Pais, string Referencias, string MetodoPago, string FormaPago, string CFDI, string BancoPagar, string DomicilioFiscal, string RegimelFiscal, string Estado, string Telefono, string Celular, string Correo, RadioButton Si, RadioButton No, string txtCorreo2, RadioButton Si2, RadioButton No2, string Estatus, ArrayList ListaConceptos, ArrayList ListaConceptos2, ArrayList ListaConceptos3, ArrayList ListaConceptos4)
        {
            string ClaveDep = string.Empty;
            string NombreDep = string.Empty;
            string NumeroEscritura = string.Empty;
            string Caracteristicas = string.Empty;
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Propietarios where IdPropietario='" + IdPropietario + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string aviso = string.Empty;

                    if (Si.Checked == true)
                    {
                        aviso = "Si";
                    }
                    else if (No.Checked == true)
                    {
                        aviso = "No";
                    }

                    string aviso2 = string.Empty;

                    if (Si2.Checked==true)
                    {
                        aviso2 = "Si";
                    }
                    else if (No2.Checked == true)
                    {
                        aviso2 = "No";
                    }

                    cmd = new SqlCommand("Insert into Propietarios (IdPropietario, RazonSocial, TipoCliente, RFC, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Pais, Referencias, MetodoPago, FormaPago, CFDI, BancoPago, DomicilioFiscal, RegimenFiscal, Estado, Telefono, Celular, Correo, EnviarAviso, Correo2, EnviarAviso2, Estatus) values ('" + IdPropietario + "','" + RazonSocial + "','" + TipoCliente + "','" + RFC + "','" + Calle + "','" + NoExterior + "','" + NoInterior + "','" + Colonia + "','" + Municipio + "','" + CodigoPostal + "','" + Ciudad + "','" + Pais + "','" + Referencias + "','" + MetodoPago + "','" + FormaPago + "','" + CFDI + "', '" + BancoPagar + "',  '" + DomicilioFiscal + "',  '" + RegimelFiscal + "', '" + Estado + "', '" + Telefono + "', '" + Celular + "', '" + Correo + "', '" + aviso + "', '" + txtCorreo2 + "', '" + aviso2 + "', '" + Estatus + "')", cn);
                    cmd.ExecuteNonQuery();

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item2 in ListaConceptos2)
                        {
                            NombreDep = item2.ToString();
                            ListaConceptos2.Remove(item2);
                            break;
                        }

                        cmd = new SqlCommand("Insert into Propietarios_Condominios (ClavePropietario, ClaveCondominio, Descripcion, NumeroEscritura, Caracteristica) values ('" + IdPropietario + "','" + ClaveDep + "', '" + NombreDep + "', '" + NumeroEscritura + "', '" + Caracteristicas + "')", cn);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item3 in ListaConceptos3)
                        {
                            NumeroEscritura = item3.ToString();
                            ListaConceptos3.Remove(item3);
                            break;
                        }

                        cmd = new SqlCommand("Update Propietarios_Condominios set NumeroEscritura='" + NumeroEscritura + "' where ClavePropietario= '" + IdPropietario + "' and ClaveCondominio='" + ClaveDep + "'", cn);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item4 in ListaConceptos4)
                        {
                            Caracteristicas = item4.ToString();
                            ListaConceptos4.Remove(item4);
                            break;
                        }

                        cmd = new SqlCommand("Update Propietarios_Condominios set Caracteristica='" + Caracteristicas + "' where ClavePropietario= '" + IdPropietario + "' and ClaveCondominio='" + ClaveDep + "'", cn);
                        cmd.ExecuteNonQuery();
                    }

                    mensaje = "Registro guardado.";
                }

                else if (contador > 0)
                {
                    string aviso = string.Empty;

                    if (Si.Checked == true)
                    {
                        aviso = "Si";
                    }
                    else if (No.Checked == true)
                    {
                        aviso = "No";
                    }

                    string aviso2 = string.Empty;

                    if (Si2.Checked == true)
                    {
                        aviso2 = "Si";
                    }
                    else if (No2.Checked == true)
                    {
                        aviso2 = "No";
                    }

                    cmd = new SqlCommand("Update Propietarios set RazonSocial='" + RazonSocial + "', TipoCliente='" + TipoCliente + "', RFC='" + RFC + "', Calle='" + Calle + "', NoExterior='" + NoExterior + "', NoInterior='" + NoInterior + "', Colonia='" + Colonia + "', Municipio='" + Municipio + "', CodigoPostal='" + CodigoPostal + "', Ciudad='" + Ciudad + "', Pais='" + Pais + "', Referencias='" + Referencias + "', MetodoPago='" + MetodoPago + "', FormaPago='" + FormaPago + "', CFDI='" + CFDI + "', BancoPago='" + BancoPagar + "', DomicilioFiscal='" + DomicilioFiscal + "', RegimenFiscal='" + RegimelFiscal + "', Estado='" + Estado + "', Telefono='" + Telefono + "', Celular='" + Celular + "', Correo='" + Correo + "', EnviarAviso='" + aviso + "', Correo2='" + txtCorreo2 + "', EnviarAviso2='" + aviso2 + "', Estatus='" + Estatus + "' where IdPropietario='" + IdPropietario + "'", cn);
                    cmd.ExecuteNonQuery();

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item2 in ListaConceptos2)
                        {
                            NombreDep = item2.ToString();
                            ListaConceptos2.Remove(item2);
                            break;
                        }

                        cmd = new SqlCommand("Insert into Propietarios_Condominios (ClavePropietario, ClaveCondominio, Descripcion, NumeroEscritura, Caracteristica) values ('" + IdPropietario + "','" + ClaveDep + "', '" + NombreDep + "', '" + NumeroEscritura + "', '" + Caracteristicas + "')", cn);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item3 in ListaConceptos3)
                        {
                            NumeroEscritura = item3.ToString();
                            ListaConceptos3.Remove(item3);
                            break;
                        }

                        cmd = new SqlCommand("Update Propietarios_Condominios set NumeroEscritura='" + NumeroEscritura + "' where ClavePropietario= '" + IdPropietario + "' and ClaveCondominio='" + ClaveDep + "'", cn);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item4 in ListaConceptos4)
                        {
                            Caracteristicas = item4.ToString();
                            ListaConceptos4.Remove(item4);
                            break;
                        }

                        cmd = new SqlCommand("Update Propietarios_Condominios set Caracteristica='" + Caracteristicas + "' where ClavePropietario= '" + IdPropietario + "' and ClaveCondominio='" + ClaveDep + "'", cn);
                        cmd.ExecuteNonQuery();
                    }

                    mensaje = "Registro modificado.";
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
        public void CargarPropietarios(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Propietarios", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdPropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoCliente"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaPropietariosSeleccionado(string IdPropietario, TextBox RazonSocial, ComboBox TipoCliente, TextBox RFC, TextBox Calle, TextBox NoExterior, TextBox NoInterior, TextBox Colonia, TextBox Municipio, TextBox CodigoPostal, TextBox Ciudad, TextBox Pais, TextBox Referencias, ComboBox MetodoPago, ComboBox FormaPago, ComboBox CFDI, TextBox BancoPagar, TextBox DomicilioFiscal, TextBox RegimelFiscal, TextBox txtEstado, TextBox txtTelefono, TextBox txtCelular, TextBox txtCorreo, RadioButton Si, RadioButton No, TextBox txtCorreo2, RadioButton Si2, RadioButton No2, ComboBox cmbEstatus)
        {
            try
            {
                cmd = new SqlCommand("Select * from Propietarios where IdPropietario='" + IdPropietario + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["EnviarAviso"].ToString() == "Si")
                    {
                        Si.Checked = true;
                    }
                    else
                    {
                        No.Checked = true;
                    }

                    if (dr["EnviarAviso2"].ToString() == "Si")
                    {
                        Si2.Checked = true;
                    }
                    else
                    {
                        No2.Checked = true;
                    }

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
                    txtEstado.Text = dr["Estado"].ToString();
                    txtTelefono.Text = dr["Telefono"].ToString();
                    txtCelular.Text = dr["Celular"].ToString();
                    txtCorreo.Text = dr["Correo"].ToString();
                    txtCorreo2.Text = dr["Correo2"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
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

        //_________________________________________________________________________________________________________________________--
        // registrar Empleado 
        public string FacturarPropietarios(string IdPropietario, string Folio)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Propietarios where IdPropietario='" + IdPropietario + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    mensaje = "El propietario no Existe";
                }

                else if (contador > 0)
                {

                    cmd = new SqlCommand("Update Opera1 set Factura= '" + IdPropietario + " 'where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Facturado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_____________________________________________________________________________________________________
        //public void CargarCondominios(string Clave, string Condominio, DataGridView dgv)
        //{
        //    try
        //    {
        //        ArrayList ListaConceptos = new ArrayList();
        //        string desc = "";

        //        dgv.Rows.Clear();
        //        da = new SqlDataAdapter("if Exists( Select * from Propietarios_Condominios where ClavePropietario= '" + Clave + "') select CS.Clave, CS.Descripcion, PC.NumeroEscritura, PC.Caracteristica from Condominios_SubCondominios as CS, Propietarios_Condominios as PC where CS.Clave=PC.ClaveCondominio and CS.Condominio='" + Condominio + "' Else select CS.Clave, CS.Descripcion from Condominios_SubCondominios as CS where CS.Condominio='" + Condominio + "'", cn);
        //        dt = new DataTable();
        //        da.Fill(dt);

        //        cmd = new SqlCommand("select * from Propietarios_Condominios where ClavePropietario ='" + Clave + "'", cn);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            ListaConceptos.Add((string)dr["Descripcion"]);
        //        }
        //        dr.Close();

        //        foreach (DataRow item in dt.Rows)
        //        {

        //            int n = dgv.Rows.Add(); //agrega una nueva fila
        //            foreach (object item2 in ListaConceptos)
        //            {
        //                desc = item2.ToString();
        //                if (item["Descripcion"].ToString() == desc)
        //                {
        //                    dgv.Rows[n].Cells[0].Value = true;
        //                    dgv.Rows[n].Cells[3].Value = item["NumeroEscritura"].ToString();
        //                    dgv.Rows[n].Cells[4].Value = item["Caracteristica"].ToString();
        //                }
        //            }

        //            dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
        //            dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cargar Conceptos3" + ex.ToString());
        //    }
        //}
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
        //________________________________________________________________________________________________
        //Empleado Registrados
        public void CargarCondominiosDisponibles(string Condominio, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select CS.*, CONVERT(INT, SUBSTRING (CS.Clave, 3,1000)) as Orden from Condominios_SubCondominios as CS where CS.Condominio= '" + Condominio + "' and CS.Clave NOT IN (select ClaveCondominio from Propietarios_Condominios) order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Caracteristicas"].ToString();
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
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string EliminarDepartamento(string txtClave)
        {
            int contador = 0;
            string mensaje = string.Empty;

            try
            {
                cmd = new SqlCommand("select * from Propietarios_Condominios where ClaveCondominio='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Delete Propietarios_Condominios where ClaveCondominio='" + txtClave + "'", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Eliminado";
                }
            }
            catch (Exception)
            {

            }
            return mensaje;
        }
    }
}
