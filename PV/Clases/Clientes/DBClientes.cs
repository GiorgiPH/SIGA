using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Clientes
{
    class DBClientes
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

        public DBClientes()
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
        public int ClaveClienteSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(IdCliente) from Clientes", cn);
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
        public string RegistroCliente(string IdCliente, string RazonSocial, string TipoCliente, string RFC, string Calle, string NoExterior, string NoInterior, string Colonia, string Municipio, string CodigoPostal, string Ciudad, string Pais, string Referencias, string MetodoPago, string FormaPago, string CFDI, string ListaPrecio, string Del, string Al, string TipoCliente2, string Zona, string Contacto, string FormaEmbarque, string DomicilioEntragas, string AgenteVentas, string PorcentajeComision, string AnticipioPedidos, string DivisaOperacion, string DiasCredito, string LimiteCredito, string PorcentajeDescuentos, string BaseComision, string PorcentajeRecargos, string EncargadoCuentasxPagar, string BancoPagar, string DomicilioFiscal, string RegimelFiscal, string Exportacion, string Estado, string Telefono, string Celular, string Correo, RadioButton Si, RadioButton No, string txtCorreo2, RadioButton Si2, RadioButton No2, string txtCorreo3, RadioButton Si3, RadioButton No3, string Estatus)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Clientes where IdCliente='" + IdCliente + "'", cn);
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

                    if (Si2.Checked == true)
                    {
                        aviso2 = "Si";
                    }
                    else if (No2.Checked == true)
                    {
                        aviso2 = "No";
                    }
                    string aviso3 = string.Empty;

                    if (Si3.Checked == true)
                    {
                        aviso3 = "Si";
                    }
                    else if (No3.Checked == true)
                    {
                        aviso3 = "No";
                    }

                    cmd = new SqlCommand("Insert into Clientes (IdCliente, RazonSocial, TipoCliente, RFC, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Pais, Referencias, MetodoPago, FormaPago, CFDI, ListaPrecios, FechaDel, FechaAl, TipoCliente2, Zona, Contacto, FormaEmbarque, DomicilioEntrega, AgenteVentas, PorcentajeComision, AnticipoPedido, DivisaOperacion, DiasCredito, LimiteCredito, PorcentajeDescuentos, BaseComision, PorcentajeRecargos, EncargadoCuentasPagar, BancoPago, DomicilioFiscal, RegimenFiscal, Exportacion, Estado, Telefono, Celular, Correo, EnviarAviso, Correo2, EnviarAviso2, Correo3, EnviarAviso3, Estatus) values ('" + IdCliente + "','" + RazonSocial + "','" + TipoCliente + "','" + RFC + "','" + Calle + "','" + NoExterior + "','" + NoInterior + "','" + Colonia + "','" + Municipio + "','" + CodigoPostal + "','" + Ciudad + "','" + Pais + "','" + Referencias + "','" + MetodoPago + "','" + FormaPago + "','" + CFDI + "', '" + ListaPrecio + "',  '" + Del + "',  '" + Al + "',  '" + TipoCliente2 + "',  '" + Zona + "',  '" + Contacto + "',  '" + FormaEmbarque + "',  '" + DomicilioEntragas + "',  '" + AgenteVentas + "',  '" + PorcentajeComision + "',  '" + AnticipioPedidos + "',  '" + DivisaOperacion + "',  '" + DiasCredito + "',  '" + LimiteCredito + "',  '" + PorcentajeDescuentos + "',  '" + BaseComision + "',  '" + PorcentajeRecargos + "',  '" + EncargadoCuentasxPagar + "',  '" + BancoPagar + "',  '" + DomicilioFiscal + "',  '" + RegimelFiscal + "',  '" + Exportacion + "', '" + Estado + "', '" + Telefono + "', '" + Celular + "', '" + Correo + "', '" + aviso + "', '" + txtCorreo2 + "', '" + aviso2 + "', '" + txtCorreo3 + "', '" + aviso3 + "','" + Estatus + "')", cn);
                    cmd.ExecuteNonQuery();
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

                    string aviso3 = string.Empty;

                    if (Si3.Checked == true)
                    {
                        aviso3 = "Si";
                    }
                    else if (No3.Checked == true)
                    {
                        aviso3 = "No";
                    }

                    cmd = new SqlCommand("Update Clientes set RazonSocial='" + RazonSocial + "', TipoCliente='" + TipoCliente + "', RFC='" + RFC + "', Calle='" + Calle + "', NoExterior='" + NoExterior + "', NoInterior='" + NoInterior + "', Colonia='" + Colonia + "', Municipio='" + Municipio + "', CodigoPostal='" + CodigoPostal + "', Ciudad='" + Ciudad + "', Pais='" + Pais + "', Referencias='" + Referencias + "', MetodoPago='" + MetodoPago + "', FormaPago='" + FormaPago + "', CFDI='" + CFDI + "', ListaPrecios='" + ListaPrecio + "', FechaDel='" + Del + "', FechaAl='" + Al + "', TipoCliente2='" + TipoCliente2 + "', Zona='" + Zona + "', Contacto='" + Contacto + "', FormaEmbarque='" + FormaEmbarque + "', DomicilioEntrega='" + DomicilioEntragas + "', AgenteVentas='" + AgenteVentas + "', PorcentajeComision='" + PorcentajeComision + "', AnticipoPedido='" + AnticipioPedidos + "', DivisaOperacion='" + DivisaOperacion + "', DiasCredito='" + DiasCredito + "', LimiteCredito= '" + LimiteCredito + "', PorcentajeDescuentos='" + PorcentajeDescuentos + "', BaseComision='" + BaseComision + "', PorcentajeRecargos='" + PorcentajeRecargos + "', EncargadoCuentasPagar='" + EncargadoCuentasxPagar + "', BancoPago='" + BancoPagar + "', DomicilioFiscal='" + DomicilioFiscal + "', RegimenFiscal='" + RegimelFiscal + "', Exportacion='" + Exportacion + "', Estado='" + Estado + "', Telefono='" + Telefono + "', Celular='" + Celular + "', Correo='" + Correo + "', EnviarAviso='" + aviso + "', Correo2='" + txtCorreo2 + "', EnviarAviso2='" + aviso2 + "', Correo3='" + txtCorreo3 + "', EnviarAviso3='" + aviso3 + "', Estatus='" + Estatus + "' where IdCliente='" + IdCliente + "'", cn);
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
        //________________________________________________________________________________________________
        //Empleado Registrados
        public DataTable CargarClientes(string nombre)
        {
            try
            {
                da = new SqlDataAdapter(@"Select * from Clientes where razonsocial like '%"+nombre+"%'", cn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pagos del recibo: " + ex.Message);
            }
        }
        //public void CargarClientes(DataGridView dgv)
        //{
        //    try
        //    {
        //        dgv.Rows.Clear();
        //        da = new SqlDataAdapter("Select * from Clientes", cn);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            int n = dgv.Rows.Add();
        //            dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
        //            dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
        //            dgv.Rows[n].Cells[2].Value = item["TipoCliente"].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error" + ex.ToString());
        //    }
        //}
        public string[] InformacionCliente(string Orden)
        {
            cmd = new SqlCommand("select OC.IdCliente, OC.RazonSocial, Oc.TipoCliente, OC.Estatus, OC.RFC, OC.Correo, OC.Correo2, OC.Correo3 from Clientes as OC where IdCliente='" + Orden + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr["IdCliente"].ToString(),
                     dr["RazonSocial"].ToString(),
                     dr["TipoCliente"].ToString(),
                     dr["Estatus"].ToString(),
                     dr["RFC"].ToString(),
                     dr["Correo"].ToString(),
                     dr["Correo2"].ToString(),
                                          dr["Correo3"].ToString(),


                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaClienteSeleccionado(string IdCliente, Guna2TextBox RazonSocial, ComboBox TipoCliente, Guna2TextBox RFC, TextBox Calle, TextBox NoExterior, TextBox NoInterior, TextBox Colonia, TextBox Municipio, TextBox CodigoPostal, TextBox Ciudad, TextBox Pais, TextBox Referencias, ComboBox MetodoPago, ComboBox FormaPago, ComboBox CFDI, ComboBox ListaPrecio, DateTimePicker Del, DateTimePicker Al, ComboBox TipoCliente2, ComboBox Zona, TextBox Contacto, TextBox FormaEmbarque, TextBox DomicilioEntragas, ComboBox AgenteVentas, TextBox PorcentajeComision, TextBox AnticipioPedidos, ComboBox DivisaOperacion, TextBox DiasCredito, TextBox LimiteCredito, TextBox PorcentajeDescuentos, ComboBox BaseComision, TextBox PorcentajeRecargos, ComboBox EncargadoCuentasxPagar, TextBox BancoPagar, TextBox DomicilioFiscal, TextBox RegimelFiscal, TextBox Exportacion, TextBox Estado, TextBox Telefono, TextBox Celular, TextBox Correo, RadioButton Si, RadioButton No, TextBox txtCorreo2, RadioButton Si2, RadioButton No2, TextBox txtCorreo3, RadioButton Si3, RadioButton No3, ComboBox Estatus, TextBox Anticipo)
        {
            try
            {
                cmd = new SqlCommand("Select * from Clientes where IdCliente='" + IdCliente + "'", cn);
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

                    if (dr["EnviarAviso3"].ToString() == "Si")
                    {
                        Si3.Checked = true;
                    }
                    else
                    {
                        No3.Checked = true;
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
                    ListaPrecio.Text = dr["ListaPrecios"].ToString();
                    Del.Text = dr["FechaDel"].ToString();
                    Al.Text = dr["FechaAl"].ToString();
                    Zona.Text = dr["Zona"].ToString();
                    Contacto.Text = dr["Contacto"].ToString();
                    FormaEmbarque.Text = dr["FormaEmbarque"].ToString();
                    DomicilioEntragas.Text = dr["DomicilioEntrega"].ToString();
                    AgenteVentas.Text = dr["AgenteVentas"].ToString();
                    PorcentajeComision.Text = dr["PorcentajeComision"].ToString();
                    AnticipioPedidos.Text = dr["AnticipoPedido"].ToString();
                    DivisaOperacion.Text = dr["DivisaOperacion"].ToString();
                    DiasCredito.Text = dr["DiasCredito"].ToString();
                    LimiteCredito.Text = dr["LimiteCredito"].ToString();
                    PorcentajeDescuentos.Text = dr["PorcentajeDescuentos"].ToString();
                    BaseComision.Text = dr["BaseComision"].ToString();
                    PorcentajeRecargos.Text = dr["PorcentajeRecargos"].ToString();
                    EncargadoCuentasxPagar.Text = dr["EncargadoCuentasPagar"].ToString();
                    BancoPagar.Text = dr["BancoPago"].ToString();
                    DomicilioFiscal.Text = dr["DomicilioFiscal"].ToString();
                    RegimelFiscal.Text = dr["RegimenFiscal"].ToString();
                    Estado.Text = dr["Estado"].ToString();
                    Telefono.Text = dr["Telefono"].ToString();
                    Celular.Text = dr["Celular"].ToString();
                    Correo.Text = dr["Correo"].ToString();
                    txtCorreo2.Text = dr["Correo2"].ToString();
                    txtCorreo3.Text = dr["Correo3"].ToString();

                    Exportacion.Text = dr["Exportacion"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                }
                dr.Close();

                cmd = new SqlCommand("Select sum(Saldo) as Anticipo from Anticipo where ClavePropietario='" + IdCliente + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Anticipo.Text = dr["Anticipo"].ToString();
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
        public string FacturarCliente(string IdCliente, string Folio)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Clientes where IdCliente='" + IdCliente + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    mensaje = "El cliente no Existe";
                }

                else if (contador > 0)
                {

                    cmd = new SqlCommand("Update Opera1 set Factura= '" + IdCliente + " 'where Folio='" + Folio + "'", cn);
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
        public void SeleccionarPropietarios(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (convert(varchar, idCliente) + ' - ' + RazonSocial) as Nombre from Clientes", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarTipoCliente(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from TipoCliente", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarZona(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Zona", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
    }
}
