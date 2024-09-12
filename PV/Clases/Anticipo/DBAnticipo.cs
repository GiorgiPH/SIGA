using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;


namespace PV.Clases.Anticipo
{
    class DBAnticipo
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

        public DBAnticipo()
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
        public int ClaveProductoSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Folio) from Anticipo", cn);
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
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveProductoSiguiente2()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Folio) from AnticipoProveedor", cn);
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
        //_______________________________________________________________________________________________
        public void SeleccionarCuentaBancaria(ComboBox cb)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select (Nombre + ' - '+ Cuenta) as Cuenta from CuentasBancarias", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarFormaPago(ComboBox cb)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from FormasPago", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public string RegistroAnticipo(string txtfolio, string txtClavePropietario, string txtCaja, string txtFecha, string txtFormaPago, string txtConcepto, string txtReferencia, string txtCuentaBancaria, string txtNumeroOperacion, decimal importe)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Anticipo where Folio='" + txtfolio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into Anticipo (Folio, ClavePropietario, Caja, Fecha, FormaPago, Concepto, Referencia, CuentaBancaria, NumeroOperacion, Importe, Saldo) values ('" + txtfolio + "', '" + txtClavePropietario + "',  '" + txtCaja + "',  '" + txtFecha + "', '" + txtFormaPago + "',  '" + txtConcepto + "',  '" + txtReferencia + "', '" + txtCuentaBancaria + "',  '" + txtNumeroOperacion + "',  " + importe + ", " + importe + ")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Anticipo set ClavePropietario='" + txtClavePropietario + "', Caja='" + txtCaja + "', Fecha='" + txtFecha + "', FormaPago='" + txtFormaPago + "', Concepto='" + txtConcepto + "', Referencia='" + txtReferencia + "', CuentaBancaria='" + txtCuentaBancaria + "', NumeroOperacion='" + txtNumeroOperacion + "', Importe= " + importe + ", Saldo= " + importe + " where Folio= '" + txtfolio + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public string RegistroAnticipoProveedor(string txtfolio, string txtClavePropietario, string txtCaja, string txtFecha, string txtFormaPago, string txtConcepto, string txtReferencia, string txtCuentaBancaria, string txtNumeroOperacion, decimal importe)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from AnticipoProveedor where Folio='" + txtfolio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into AnticipoProveedor (Folio, ClaveProveedor, Caja, Fecha, FormaPago, Concepto, Referencia, CuentaBancaria, NumeroOperacion, Importe, Saldo) values ('" + txtfolio + "', '" + txtClavePropietario + "',  '" + txtCaja + "',  '" + txtFecha + "', '" + txtFormaPago + "',  '" + txtConcepto + "',  '" + txtReferencia + "', '" + txtCuentaBancaria + "',  '" + txtNumeroOperacion + "',  " + importe + ", " + importe + ")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update AnticipoProveedor set ClaveProveedor='" + txtClavePropietario + "', Caja='" + txtCaja + "', Fecha='" + txtFecha + "', FormaPago='" + txtFormaPago + "', Concepto='" + txtConcepto + "', Referencia='" + txtReferencia + "', CuentaBancaria='" + txtCuentaBancaria + "', NumeroOperacion='" + txtNumeroOperacion + "', Importe= " + importe + ", Saldo= " + importe + " where Folio= '" + txtfolio + "'", cn);
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
        //________________________________________________________________________________________________
        //tiendas Registrados
        public void CargarAnticipo(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select A.*, P.RazonSocial from Anticipo as A, Propietarios as P where A.ClavePropietario=P.IdPropietario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Saldo"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //tiendas Registrados
        public void CargarAnticipoProveedor(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select A.*, P.RazonSocial from AnticipoProveedor as A, Proveedor as P where A.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Saldo"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaProductoSeleccionado(string txtfolio, TextBox txtClavePropietario, TextBox txtCaja, DateTimePicker txtFecha, ComboBox txtFormaPago, TextBox txtConcepto, TextBox txtReferencia, TextBox txtCuentaBancaria, TextBox txtNumeroOperacion, TextBox importe)
        {
            try
            {
                cmd = new SqlCommand("Select * from Anticipo where Folio='" + txtfolio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtClavePropietario.Text = dr["ClavePropietario"].ToString();
                    txtCaja.Text = dr["Caja"].ToString();
                    txtFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                    txtFormaPago.Text = dr["FormaPago"].ToString();
                    txtConcepto.Text = dr["Concepto"].ToString();
                    txtReferencia.Text = dr["Referencia"].ToString();
                    txtCuentaBancaria.Text = dr["CuentaBancaria"].ToString();
                    txtNumeroOperacion.Text = dr["NumeroOperacion"].ToString();
                    importe.Text = dr["Importe"].ToString();

                    dr.Close();
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
        public void ConsultaProductoSeleccionadoProveedor(string txtfolio, TextBox txtClavePropietario, TextBox txtCaja, DateTimePicker txtFecha, ComboBox txtFormaPago, TextBox txtConcepto, TextBox txtReferencia, TextBox txtCuentaBancaria, TextBox txtNumeroOperacion, TextBox importe)
        {
            try
            {
                cmd = new SqlCommand("Select * from AnticipoProveedor where Folio='" + txtfolio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtClavePropietario.Text = dr["ClaveProveedor"].ToString();
                    txtCaja.Text = dr["Caja"].ToString();
                    txtFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                    txtFormaPago.Text = dr["FormaPago"].ToString();
                    txtConcepto.Text = dr["Concepto"].ToString();
                    txtReferencia.Text = dr["Referencia"].ToString();
                    txtCuentaBancaria.Text = dr["CuentaBancaria"].ToString();
                    txtNumeroOperacion.Text = dr["NumeroOperacion"].ToString();
                    importe.Text = dr["Importe"].ToString();

                    dr.Close();
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
        public void ConsultaConceptoAnticipo(TextBox txtconcepto, TextBox txtconceptoclave)
        {
            try
            {
                cmd = new SqlCommand("select DE.ConceptoAnt, (DE.ConceptoAnt + ' - ' + CI.Descripcion) as Nombre from DatosEmpresa as DE, ConceptosIngreso as CI where DE.ConceptoAnt=CI.Clave", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtconcepto.Text = dr["Nombre"].ToString();
                    txtconceptoclave.Text = dr["ConceptoAnt"].ToString();

                    dr.Close();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________________________
        //_________________________________________________________________________________________
        public string[] InformacionCuenta(string Documento)
        {
            cmd = new SqlCommand("Select Clave from CuentasBancarias where (Nombre + ' - '+ Cuenta)= '" + Documento + "'", cn);
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
        public string[] InformacionCuenta2(string Documento)
        {
            cmd = new SqlCommand("Select  (Nombre + ' - '+ Cuenta)  from CuentasBancarias where Clave= '" + Documento + "'", cn);
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
        public string[] InformacionConcepto(string Documento)
        {
            cmd = new SqlCommand("select (CI.Clave + ' - ' + CI.Descripcion) as Nombre from  ConceptosIngreso as CI where CI.Clave= '" + Documento + "'", cn);
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
        public string[] InformacionPropietario(string Documento)
        {
            cmd = new SqlCommand("select RazonSocial from Propietarios where IdPropietario= '" + Documento + "'", cn);
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
        public string[] InformacionProveedor(string Documento)
        {
            cmd = new SqlCommand("select RazonSocial from Proveedor where IdProveedor= '" + Documento + "'", cn);
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

        //____________________________________________________________________
        //----------------------------------------------------------
        public void CargarReciboAlumno(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("select A.*, CI. Descripcion from Anticipo as A, ConceptosIngreso as CI where A.Concepto=CI.CLave and A.ClavePropietario='" + Matricula + "' and A.Saldo>0", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Concepto"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //____________________________________________________________________
        //----------------------------------------------------------
        public void CargarReciboProveedor(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("select A.*, CI. Descripcion from AnticipoProveedor as A, ConceptosIngreso as CI where A.Concepto=CI.CLave and A.ClaveProveedor='" + Matricula + "' and A.Saldo>0", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Concepto"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________
        public void CargarReciboAlumno2(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();

                da = new SqlDataAdapter("select R.*, D.Nombre from Recibo as R, Documento as D where ClavePropietario='" + Matricula + "' and R.ClaveDocumento=D.Clave and R.Saldo>0", cn);
                dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(0.00).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos aqui" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________
        public void CargarEgreso2(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();


                da = new SqlDataAdapter("(select 'P' as Tipo, R.*, D.Nombre from RecepcionProducto as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.ClaveDocumento=D.Clave) union (select 'G' as Tipo, R.*, D.Nombre from RegistroGastos as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.ClaveDocumento=D.Clave) union (select 'NCG' as Tipo, R.*, '' as DiasVence, '' as FechaVence, D.Nombre from NotasGasto as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.ClaveDocumento=D.Clave)", cn);
                dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Tipo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(0.00).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobroGeneral(decimal Importe, TextBox folio)
        {
            int contador = 0;
            int Folio = 0;
            try
            {
                cmd = new SqlCommand("select max(Folio) from Anticipo_General", cn);
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

                Folio++;
                folio.Text = Folio.ToString();
                contador = 0;
                cmd = new SqlCommand("select * from Anticipo_General where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Anticipo_General (Folio, Importe) values ('" + Folio + "'," + Importe + ")", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobroGeneralProveedor(decimal Importe, TextBox folio)
        {
            int contador = 0;
            int Folio = 0;
            try
            {
                cmd = new SqlCommand("select max(Folio) from AnticipoProveedor_General", cn);
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

                Folio++;
                folio.Text = Folio.ToString();
                contador = 0;
                cmd = new SqlCommand("select * from AnticipoProveedor_General where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into AnticipoProveedor_General (Folio, Importe) values ('" + Folio + "'," + Importe + ")", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarSaldoProveedor(string Clave, decimal Saldo)
        {
            try
            {
                cmd = new SqlCommand("Update Proveedor set Saldo= Saldo - " + Saldo + " where IdProveedor=" + Clave + "", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarEgreso(string Tipo, string Folio, string MatriculaAlumno, string Fecha, decimal Pago,  string FolioGeneral)
        {
            try
            {
                cmd = new SqlCommand("insert into AnticipoProveedorCobros (Tipo, Folio, ClaveProveedor, Fecha, Pago, FolioGeneral) values ('" + Tipo + "', '" + Folio + "', '" + MatriculaAlumno + "', '" + Fecha + "', '" + Pago + "', '" + FolioGeneral + "')", cn);
                cmd.ExecuteNonQuery();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________
        public void ActualizarEgreso2(string Tipo, string Folio, decimal Saldo)
        {
            try
            {
                if (Tipo == "P")
                {
                    cmd = new SqlCommand("Update RecepcionProducto set  Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
                else if (Tipo == "G")
                {
                    cmd = new SqlCommand("Update RegistroGastos set  Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
                else if (Tipo == "NCG")
                {
                    cmd = new SqlCommand("Update NotasGasto set  Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public void ActualizarRecibo2(string Folio, decimal Saldo)
        {
            try
            {

                cmd = new SqlCommand("Update Recibo set Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobro(string Folio, string MatriculaAlumno, string Fecha, decimal Pago, string FolioGeneral)
        {
            try
            {
                cmd = new SqlCommand("insert into AnticipoCobros (Folio, ClavePropietario, Fecha, Pago, FolioGeneral) values ('" + Folio + "', '" + MatriculaAlumno + "', '" + Fecha + "', '" + Pago + "', '" + FolioGeneral + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public void ActualizarAnticipo(string Folio, decimal Saldo)
        {
            try
            {

                cmd = new SqlCommand("Update Anticipo set Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public void ActualizarAnticipoProveedor(string Folio, decimal Saldo)
        {
            try
            {

                cmd = new SqlCommand("Update AnticipoProveedor set Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropietarios(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (convert(varchar, idPropietario) + ' - ' + RazonSocial) as Nombre from Propietarios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarProveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (convert(varchar, idProveedor) + ' - ' + RazonSocial) as Nombre from Proveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
    }
}
