using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;


namespace PV.Clases.Proveedores
{
    class DBProveedores
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
        public void CerrarConexion()
        {
            try
            {
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public DBProveedores()
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
                cmd = new SqlCommand("select max(IdProveedor) from Proveedor", cn);
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
        public string RegistroCliente(string IdCliente, string RazonSocial, string RFC, string Calle, string NoExterior, string NoInterior, string Colonia, string Municipio, string CodigoPostal, string Ciudad, string Pais, string Referencias, string MetodoPago, string FormaPago, string CFDI, string ListaPrecio, string Del, string Al, string Zona, string Contacto, string FormaEmbarque,  string DivisaOperacion, string DiasCredito, string LimiteCredito, string PorcentajeDescuentos, string BancoPagar, string DomicilioFiscal, string RegimelFiscal, string Estado, string Telefono, string Celular, string Correo, string txtCorreo2, string Estatus, string TipoProveedor)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Proveedor where IdProveedor='" + IdCliente + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Proveedor (IdProveedor, RazonSocial, RFC, Calle, NoExterior, NoInterior, Colonia, Municipio, CodigoPostal, Ciudad, Pais, Referencias, MetodoPago, FormaPago, CFDI, ListaPrecios, FechaDel, FechaAl, Zona, Contacto, FormaEmbarque, DivisaOperacion, DiasCredito, LimiteCredito, PorcentajeDescuentos, BancoPago, DomicilioFiscal, RegimenFiscal, Estado, Telefono, Celular, Correo,  Correo2, Estatus, Saldo, TipoProveedor) values ('" + IdCliente + "','" + RazonSocial + "','" + RFC + "','" + Calle + "','" + NoExterior + "','" + NoInterior + "','" + Colonia + "','" + Municipio + "','" + CodigoPostal + "','" + Ciudad + "','" + Pais + "','" + Referencias + "','" + MetodoPago + "','" + FormaPago + "','" + CFDI + "', '" + ListaPrecio + "',  '" + Del + "',  '" + Al + "',  '" + Zona + "',  '" + Contacto + "',  '" + FormaEmbarque + "', '" + DivisaOperacion + "',  '" + DiasCredito + "',  '" + LimiteCredito + "',  '" + PorcentajeDescuentos + "', '" + BancoPagar + "',  '" + DomicilioFiscal + "',  '" + RegimelFiscal + "', '" + Estado + "', '" + Telefono + "', '" + Celular + "', '" + Correo + "', '" + txtCorreo2 + "','" + Estatus + "', '0.00', '"+TipoProveedor+"')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";
                }

                else if (contador > 0)
                {
                    cmd = new SqlCommand("Update Proveedor set RazonSocial='" + RazonSocial + "', RFC='" + RFC + "', Calle='" + Calle + "', NoExterior='" + NoExterior + "', NoInterior='" + NoInterior + "', Colonia='" + Colonia + "', Municipio='" + Municipio + "', CodigoPostal='" + CodigoPostal + "', Ciudad='" + Ciudad + "', Pais='" + Pais + "', Referencias='" + Referencias + "', MetodoPago='" + MetodoPago + "', FormaPago='" + FormaPago + "', CFDI='" + CFDI + "', ListaPrecios='" + ListaPrecio + "', FechaDel='" + Del + "', FechaAl='" + Al + "', Zona='" + Zona + "', Contacto='" + Contacto + "', FormaEmbarque='" + FormaEmbarque + "', DivisaOperacion='" + DivisaOperacion + "', DiasCredito='" + DiasCredito + "', LimiteCredito= '" + LimiteCredito + "', PorcentajeDescuentos='" + PorcentajeDescuentos + "', BancoPago='" + BancoPagar + "', DomicilioFiscal='" + DomicilioFiscal + "', RegimenFiscal='" + RegimelFiscal + "', Estado='" + Estado + "', Telefono='" + Telefono + "', Celular='" + Celular + "', Correo='" + Correo + "', Correo2='" + txtCorreo2 + "', Estatus='" + Estatus + "', TipoProveedor='"+ TipoProveedor + "' where IdProveedor='" + IdCliente + "'", cn);
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
        public void CargarClientes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Proveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Empleado Registrados
        public void CargarClientesFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Proveedor where RazonSocial like '%"+Filtro+"%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
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
        public void ConsultaClienteSeleccionado(string IdCliente, Guna2TextBox RazonSocial, Guna2TextBox RFC, TextBox Calle, TextBox NoExterior, TextBox NoInterior, TextBox Colonia, TextBox Municipio, TextBox CodigoPostal, TextBox Ciudad, TextBox Pais, TextBox Referencias, ComboBox MetodoPago, ComboBox FormaPago, ComboBox CFDI, ComboBox ListaPrecio, DateTimePicker Del, DateTimePicker Al, ComboBox Zona, TextBox Contacto, TextBox FormaEmbarque, ComboBox DivisaOperacion, TextBox DiasCredito, TextBox LimiteCredito, TextBox PorcentajeDescuentos, TextBox BancoPagar, TextBox DomicilioFiscal, TextBox RegimelFiscal, TextBox Estado, TextBox Telefono, TextBox Celular, TextBox Correo, RadioButton Si, RadioButton No, TextBox txtCorreo2, RadioButton Si2, RadioButton No2, ComboBox Estatus, TextBox Saldo, TextBox Anticipo, ComboBox cmbTipoProveedor)
        {
            try
            {
                cmd = new SqlCommand("Select * from Proveedor where IdProveedor='" + IdCliente + "'", cn);
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
                    DivisaOperacion.Text = dr["DivisaOperacion"].ToString();
                    DiasCredito.Text = dr["DiasCredito"].ToString();
                    LimiteCredito.Text = dr["LimiteCredito"].ToString();
                    PorcentajeDescuentos.Text = dr["PorcentajeDescuentos"].ToString();
                    BancoPagar.Text = dr["BancoPago"].ToString();
                    DomicilioFiscal.Text = dr["DomicilioFiscal"].ToString();
                    RegimelFiscal.Text = dr["RegimenFiscal"].ToString();
                    Estado.Text = dr["Estado"].ToString();
                    Telefono.Text = dr["Telefono"].ToString();
                    Celular.Text = dr["Celular"].ToString();
                    Correo.Text = dr["Correo"].ToString();
                    txtCorreo2.Text = dr["Correo2"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Saldo.Text = dr["Saldo"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TipoProveedor")))
                    {
                        cmbTipoProveedor.SelectedValue = dr["TipoProveedor"].ToString();
                    }
                    else
                    {
                        cmbTipoProveedor.SelectedIndex = -1;
                    }
                }
                dr.Close();

                cmd = new SqlCommand("Select sum(Saldo) as Anticipo from AnticipoProveedor where ClaveProveedor='" + IdCliente + "'", cn);
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
        //_____________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Nombre from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        public DataTable ConsultarProveedores()
        {
            string query = "SELECT * FROM Proveedor where estatus ='Activo'";
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(ObtenerCn()))
            {
                var command = new SqlCommand(query, connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            return dataTable;

        }

    }
}
