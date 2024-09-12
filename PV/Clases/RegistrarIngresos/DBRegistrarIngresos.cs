using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;

namespace Condominios.Clases.RegistrarIngresos
{
    class DBRegistrarIngresos
    {
        public static string usuario = string.Empty;
        public static string TipoUsuario = string.Empty;
        public static string Estatus = string.Empty;


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

        public DBRegistrarIngresos()
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
        //____________________________________________________________________
        //----------------------------------------------------------
        public int InicioSesion(string Usuario, string Contraseña)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Usuario, TipoUsuario, Estatus from Usuarios where Usuario='" + Usuario + "' and Contraseña='" + Contraseña + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {
                    usuario = Usuario;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    TipoUsuario = dt.Rows[0][1].ToString();
                    Estatus = dt.Rows[0][2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
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
                da = new SqlDataAdapter("select R.*, D.Nombre from Recibo as R, Documento as D where ClavePropietario='"+Matricula+"' and Saldo!=0 and R.ClaveDocumento=D.Clave", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[5].Value = Convert.ToDecimal( item["Total"]).ToString("N", formato);
                    dgv.Rows[n].Cells[6].Value = Convert.ToDecimal( item["Saldo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[7].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[10].Value = Convert.ToDecimal(0.00).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
       
        //_______________________________________________________________________________________________________________
        public void FechayRecargo(TextBox txtRecargosSiNo, TextBox txtDescuentoSiNo, TextBox txtMontoMaximo, TextBox txtMontoMaximoD, TextBox txtFecha)
        {
            try
            {

                cmd = new SqlCommand("select Recargos, Descuentos, MontoMaximo, MontoMaximoD, FechaCobranza from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtRecargosSiNo.Text = dr["Recargos"].ToString();
                    txtDescuentoSiNo.Text = dr["Descuentos"].ToString();
                    txtMontoMaximo.Text = dr["MontoMaximo"].ToString();
                    txtMontoMaximoD.Text = dr["MontoMaximoD"].ToString();
                    txtFecha.Text = dr["FechaCobranza"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void RecargoConcepto(TextBox Concepto, TextBox GenerarRecargo, TextBox DiarioMensual, TextBox ImportePorcentaje, TextBox Importe, TextBox Porcentaje, TextBox IMporteConcepto)
        {
            try
            {

                cmd = new SqlCommand("select D.Concepto, CI.GenerarRecargo, CI.DiarioMensual, CI.ImportePorcentaje, CI.Importe, CI.Porcentaje, CI.IMporteConcepto from DatosEmpresa as D, ConceptosIngreso as CI where D.Concepto=CI.Clave", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Concepto.Text = dr["Concepto"].ToString();
                    GenerarRecargo.Text = dr["GenerarRecargo"].ToString();
                    DiarioMensual.Text = dr["DiarioMensual"].ToString();
                    ImportePorcentaje.Text = dr["ImportePorcentaje"].ToString();
                    Importe.Text = dr["Importe"].ToString();
                    Porcentaje.Text = dr["Porcentaje"].ToString();
                    IMporteConcepto.Text = dr["IMporteConcepto"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void RecargoConceptoCalculo(string Concepto, string Folio, string Propietario, TextBox txtCalculo)
        {
            try
            {

                cmd = new SqlCommand("select count(P.ClaveRecibo) as Contador from Recibo as R, Documento as D, Partida as P where ClavePropietario='"+ Propietario + "' and R.Folio='"+ Folio + "' and P.ClaveRecibo='"+ Concepto + "' and Saldo!=0 and R.ClaveDocumento=D.Clave and R.Folio=P.FolioRecibo", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtCalculo.Text = dr["Contador"].ToString();
                  
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
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
                da = new SqlDataAdapter("(select 'P' as Tipo, R.*, D.Nombre from RecepcionProducto as R, Documento as D where R.ClaveProveedor='" + Matricula + "' and Saldo!=0 and R.ClaveDocumento=D.Clave) union (select 'G' as Tipo, R.*, D.Nombre from RegistroGastos as R, Documento as D where R.ClaveProveedor='" + Matricula + "' and Saldo!=0 and R.ClaveDocumento=D.Clave) union (select 'NCG' as Tipo, R.*, '' as DiasVence, '' as FechaVence, D.Nombre from NotasGasto as R, Documento as D where R.ClaveProveedor='" + Matricula + "' and Saldo!=0 and R.ClaveDocumento=D.Clave)", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Tipo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[5].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[6].Value = Convert.ToDouble( item["Total"]).ToString("N", formato);
                    dgv.Rows[n].Cells[7].Value = Convert.ToDouble(item["Saldo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[8].Value = Convert.ToDouble(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[9].Value = Convert.ToDouble(0.00).ToString("N", formato);
                    dgv.Rows[n].Cells[10].Value = Convert.ToDouble(0.00).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public void ActualizarRecibo2(string Folio, decimal Recargos, decimal Descuento, decimal Saldo)
        {
            try
            {

                cmd = new SqlCommand("Update Recibo set Recargo=" + Recargos + ", DescuentoPago=" + Descuento + ", Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public void ActualizarEgreso2(string Tipo, string Folio, decimal Recargos, decimal Descuento, decimal Saldo)
        {
            try
            {
                if (Tipo =="P")
                {
                    cmd = new SqlCommand("Update RecepcionProducto set Recargo=" + Recargos + ", DescuentoPago=" + Descuento + ", Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
                else if (Tipo == "G")
                {
                    cmd = new SqlCommand("Update RegistroGastos set Recargo=" + Recargos + ", DescuentoPago=" + Descuento + ", Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
                else if (Tipo=="NCG")
                {
                    cmd = new SqlCommand("Update NotasGasto set Recargo=" + Recargos + ", DescuentoPago=" + Descuento + ", Saldo=" + Saldo + " where Folio='" + Folio + "'", cn);
                    cmd.ExecuteNonQuery();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________
        public string ActualizarCancelarEgreso2(string Clave, string Tipo, string Observaciones, decimal Abono, string Proveedor)
        {
            string mensaje = string.Empty;
            try
            {
                if (Tipo == "P")
                {
                    cmd = new SqlCommand("Update Egreso set Observaciones='" + Observaciones + "', Pago= 0.00 where Folio='" + Clave + "' and Tipo='"+Tipo+"'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update RecepcionProducto set Saldo = Saldo + "+Abono+" where Folio='" + Clave + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update Proveedor set Saldo = Saldo + " + Abono + " where IdProveedor='" + Proveedor + "'", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Egreso Cancelado";
                }
                else if (Tipo == "G")
                {
                    cmd = new SqlCommand("Update Egreso set Observaciones='" + Observaciones + "', Pago= 0.00 where Folio='" + Clave + "' and Tipo='" + Tipo + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update RegistroGastos set Saldo = Saldo + " + Abono + " where Folio='" + Clave + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update Proveedor set Saldo = Saldo + " + Abono + " where IdProveedor='" + Proveedor + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Egreso Cancelado";
                }
                else if (Tipo == "NCG")
                {
                    cmd = new SqlCommand("Update Egreso set Observaciones='" + Observaciones + "', Pago= 0.00  where Folio='" + Clave + "' and Tipo='" + Tipo + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update NotasGasto set Saldo = Saldo + " + Abono + " where Folio='" + Clave + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update Proveedor set Saldo = Saldo + " + Abono + " where IdProveedor='" + Proveedor + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Nota de Cargo Cancelada";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }
        //_________________________________________________________________________________
        public string ActualizarCancelarIngreso2(string Clave, string Observaciones, decimal Abono, string Proveedor)
        {
            string mensaje = string.Empty;
            try
            {
                    cmd = new SqlCommand("Update Cobros set Observaciones='" + Observaciones + "', Pago= 0.00 where Folio='" + Clave + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update Recibo set Saldo = Saldo + " + Abono + " where Folio='" + Clave + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Ingreso Cancelado";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }
        //____________________________________________________________________________________________________
        public void CargarReciboAlumno2(DataGridView dgv, string Matricula, ArrayList ListaConcep)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                string Concepto = string.Empty;
                dgv.Rows.Clear();

                foreach (object item2 in ListaConcep)
                {
                    Concepto = item2.ToString();

                    da = new SqlDataAdapter("select R.*, D.Nombre from Recibo as R, Documento as D where ClavePropietario='"+Matricula+"' and R.Folio='"+Concepto+"' and R.ClaveDocumento=D.Clave", cn);
                    dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Recargo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["DescuentoPago"]).ToString("N", formato);
                        dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                        dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(0.00).ToString("N", formato);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________
        public void CargarEgreso2(DataGridView dgv, string Matricula, ArrayList ListaConcep, ArrayList ListaConcep2, ArrayList ListaConcep3)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                string Concepto = string.Empty;
                string Documento = string.Empty;
                string Tipo = string.Empty;

                dgv.Rows.Clear();

                foreach (object item2 in ListaConcep)
                {
                    Concepto = item2.ToString();

                    foreach (object item3 in ListaConcep2)
                    {
                        Documento = item3.ToString();

                        foreach (object item4 in ListaConcep3)
                        {
                            Tipo = item4.ToString();

                            da = new SqlDataAdapter("(select 'P' as Tipo, R.*, D.Nombre from RecepcionProducto as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.Folio='" + Concepto + "' and R.ClaveDocumento='" + Documento + "' and R.ClaveDocumento=D.Clave and 'P'='"+Tipo+"') union (select 'G' as Tipo, R.*, D.Nombre from RegistroGastos as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.Folio='" + Concepto + "' and R.ClaveDocumento='" + Documento + "' and R.ClaveDocumento=D.Clave and 'G'='" + Tipo + "') union (select 'NCG' as Tipo, R.*,'' as DiasVence, '' as FechaVence, D.Nombre from NotasGasto as R, Documento as D where ClaveProveedor='" + Matricula + "'  and Saldo!=0 and R.Folio='" + Concepto + "' and R.ClaveDocumento='" + Documento + "' and R.ClaveDocumento=D.Clave and 'NCG'='" + Tipo + "')", cn);
                            dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow item in dt.Rows)
                            {
                                int n = dgv.Rows.Add();
                                dgv.Rows[n].Cells[1].Value = item["Tipo"].ToString();
                                dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                                dgv.Rows[n].Cells[3].Value = item["ClaveDocumento"].ToString();
                                dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                                dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["Recargo"]).ToString("N", formato);
                                dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(item["DescuentoPago"]).ToString("N", formato);
                                dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                                dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                                dgv.Rows[n].Cells[10].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    ListaConcep3.RemoveAt(0);
                    ListaConcep2.RemoveAt(0);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________
        public void CargarPagosEgreso2(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("(select E.FormaPago, 'P' as Tipo, R.*, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E. NumAutorizacion, B.Nombre as Banco from RecepcionProducto as R, Documento as D, Egreso E, CuentasBancarias as B where B.Clave=E.CuentaBancaria and R.ClaveProveedor='" + Matricula+ "' and R.ClaveDocumento=D.Clave and E.Tipo='P' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null) union (select E.FormaPago, 'G' as Tipo, R.*, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E.NumAutorizacion, B.Nombre as Banco from RegistroGastos as R, Documento as D, Egreso E, CuentasBancarias as B where B.Clave=E.CuentaBancaria and R.ClaveProveedor='" + Matricula+ "' and R.ClaveDocumento=D.Clave and E.Tipo='G' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null) union (select E.FormaPago, 'NCG' as Tipo, R.*, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E.NumAutorizacion, B.Nombre as Banco from NotasGasto as R, Documento as D, Egreso E, CuentasBancarias as B where B.Clave=E.CuentaBancaria and R.ClaveProveedor='" + Matricula + "' and R.ClaveDocumento=D.Clave and E.Tipo='NCG' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null)", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FormaPago"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Tipo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Banco"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["NumAutorizacion"].ToString();
                    dgv.Rows[n].Cells[7].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[8].Value = Convert.ToDouble(item["Recargo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[9].Value = Convert.ToDouble(item["DescuentoPago"]).ToString("N", formato);
                    dgv.Rows[n].Cells[10].Value = Convert.ToDouble(item["Total"]).ToString("N", formato);
                    dgv.Rows[n].Cells[11].Value = Convert.ToDouble(item["Pago"]).ToString("N", formato);
                    dgv.Rows[n].Cells[12].Value = Convert.ToDouble(item["Saldo"]).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        //____________________________________________________________________________________________________
        public void CargarPagosRecibo2(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("select E.FormaPago, E.Fecha as FechaPago , R.*, D.Nombre, (R.ClaveDocumento + '-' + convert(varchar, R.Folio)) as Documento, R.Total, E.Pago, R.Saldo, E.Folio from Recibo as R, Documento as D, Cobros as E where R.ClavePropietario='"+Matricula+ "' and R.ClaveDocumento=D.Clave and R.Folio=E.Folio and R.ClavePropietario=E.ClavePropietario and E.FormaPago is not null Order by E.Folio", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FormaPago"].ToString();
                    dgv.Rows[n].Cells[1].Value = Convert.ToDateTime(item["FechaPago"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Documento"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[5].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[7].Value = Convert.ToDouble(item["Recargo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[8].Value = Convert.ToDouble(item["DescuentoPago"]).ToString("N", formato);
                    dgv.Rows[n].Cells[9].Value = Convert.ToDouble(item["Total"]).ToString("N", formato);
                    dgv.Rows[n].Cells[10].Value = Convert.ToDouble(item["Pago"]).ToString("N", formato);
                    dgv.Rows[n].Cells[11].Value = Convert.ToDouble(item["Saldo"]).ToString("N", formato);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobro(string Folio, string MatriculaAlumno, string Fecha, string Observaciones, string FormaPago, decimal Pago, string referencia, string NumOperacion, string NumAutorizacion, string Cuenta, string FolioGeneral)
        {
            try
            {
                cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario, Fecha, Observaciones, FormaPago, Pago, referencia, NumOperacion, NumAutorizacion, CuentaBancaria, FolioGeneral) values ('" + Folio + "', '" + MatriculaAlumno + "', '" + Fecha + "', '" + Observaciones + "', '" + FormaPago + "', '" + Pago + "','" + referencia + "','" + NumOperacion + "','" + NumAutorizacion + "', '"+Cuenta+"', '"+FolioGeneral+"')", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete Cobros where Folio='" + Folio + "' and ClavePropietario='" + MatriculaAlumno + "'and  Fecha is null", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobroGeneral(decimal Importe, TextBox folio)
        {
            int contador = 0;
            int Folio = 0;
            try
            {
                cmd = new SqlCommand("select max(Folio) from Cobro_General", cn);
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
                cmd = new SqlCommand("select * from Cobro_General where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Cobro_General (Folio, Importe) values ('" + Folio + "'," + Importe + ")", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarCobroGeneralEgreso(decimal Importe, TextBox folio)
        {
            int contador = 0;
            int Folio = 0;
            try
            {
                cmd = new SqlCommand("select max(Folio) from Egreso_General", cn);
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
                cmd = new SqlCommand("select * from Egreso_General where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Egreso_General (Folio, Importe) values ('" + Folio + "'," + Importe + ")", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________---
        public void InsertarEgreso(string Tipo, string Folio, string MatriculaAlumno, string Fecha, string Observaciones, string FormaPago, decimal Pago, string referencia, string NumOperacion, string NumAutorizacion, string Cuenta, string FolioGeneral)
        {
            try
            {
                cmd = new SqlCommand("insert into Egreso (Tipo, Folio, ClaveProveedor, Fecha, Observaciones, FormaPago, Pago, referencia, NumOperacion, NumAutorizacion, CuentaBancaria, FolioGeneral) values ('" + Tipo+"', '" + Folio + "', '" + MatriculaAlumno + "', '" + Fecha + "', '" + Observaciones + "', '" + FormaPago + "', '" + Pago + "','" + referencia + "','" + NumOperacion + "','" + NumAutorizacion + "', '"+Cuenta+"', '"+FolioGeneral+"')", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete Egreso where Tipo='" + Tipo + "' and Folio='" + Folio + "' and ClaveProveedor='" + MatriculaAlumno + "'and  Fecha is null", cn);
                cmd.ExecuteNonQuery();
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
      
    }
}
