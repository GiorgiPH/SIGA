using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.RecibosAutomaticos
{
    class DBRecibosAutomaticos
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Servidor = string.Empty;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBRecibosAutomaticos()
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
        public void ConsultaRecibosAutomaticos( TextBox txtReciboAutomatico, TextBox txtDocumento)
        {
            try
            {
                cmd = new SqlCommand("select * from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtReciboAutomatico.Text = dr["RecibosAutomaticos"].ToString();
                    txtDocumento.Text = dr["Documento"].ToString();

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
        public void ConsultaRecibosAutomaticosCondominio(string Condominio, TextBox txtReciboAutomatico)
        {
            try
            {
                cmd = new SqlCommand("select * from Condominio where ClaveCondominio='"+Condominio+"'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["CuotaGeneral"].ToString()== "Si")
                    {
                        txtReciboAutomatico.Text = "Concepto";
                    }
                    else if (dr["CuotaProIndiviso"].ToString() == "Si")
                    {
                        txtReciboAutomatico.Text = "ProIndiviso";
                    }
                    else if (dr["CuotaOtros"].ToString() == "Si")
                    {
                        txtReciboAutomatico.Text = "Estructura";
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
       
        //_______________________________________________________________________________________________________________
        public void DiasVence(TextBox txtDias)
        {
            try
            {

                cmd = new SqlCommand("select DiasPlazo from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtDias.Text = dr["DiasPlazo"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaConceptoMantenimiento(TextBox txtConceptoMantenimiento,TextBox txtConcepto)
        {
            try
            {
                cmd = new SqlCommand("select C.Concepto, CI.Descripcion from datosEmpresa as C, ConceptosIngreso as CI where C.Concepto=CI.Clave", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtConceptoMantenimiento.Text = dr["Concepto"].ToString();
                    txtConcepto.Text = dr["Descripcion"].ToString();

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
        public void ConsultaAutoImporte(string Concepto, TextBox txtAutomatico,TextBox txtImporte)
        {
            try
            {
                cmd = new SqlCommand("select * from ConceptosIngreso where Clave = '"+Concepto+"'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtAutomatico.Text = dr["ManualAutoma"].ToString();
                    txtImporte.Text = dr["ImporteConcepto"].ToString();

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
        public void ConsultaAutoImporteProIndivisoPresupuesto(string Concepto, TextBox txtimporte, TextBox txtAño, TextBox periodo)
        {
            try
            {
                cmd = new SqlCommand("select Importe, Anual, Periodo from Condominio where ClaveCondominio = '" + Concepto + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    decimal importe = Convert.ToDecimal(dr["Importe"].ToString());
                    string Periodo = dr["Periodo"].ToString();

                    if (Periodo == "Mensual")
                    {
                        periodo.Text ="12";
                    }
                    else if (Periodo == "Bimestral")
                    {
                        periodo.Text = "6";
                    }
                    else if (Periodo == "Trimestral")
                    {
                        periodo.Text = "4";
                    }
                    else if (Periodo == "Cuatrimestral")
                    {
                        periodo.Text = "3";
                    }
                    else if (Periodo == "Semestral")
                    {
                        periodo.Text = "2";
                    }
                    else if (Periodo == "Anual")
                    {
                        periodo.Text = "";
                    }

                    txtimporte.Text = importe.ToString();

                    txtAño.Text = dr["Anual"].ToString();

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________________________________________________

        public void CargarCondominios(DataGridView dgv, string Condominio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select PC.ClavePropietario, PC.ClaveCondominio, CS.Condominio, CS.ProIndiviso, CS.CuotaMantenimiento, CS.Del, CS.Al, P.Correo, P.Correo2 from Propietarios_Condominios as PC, Condominios_SubCondominios as CS, Propietarios as P where PC.ClavePropietario=P.IdPropietario and PC.ClaveCondominio=CS.Clave and CS.Condominio = '"+Condominio+"'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClavePropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveCondominio"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Condominio"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["ProIndiviso"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["CuotaMantenimiento"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Del"].ToString();
                    dgv.Rows[n].Cells[6].Value = item["Al"].ToString();
                    dgv.Rows[n].Cells[7].Value = item["Correo"].ToString();
                    dgv.Rows[n].Cells[8].Value = item["Correo2"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarRecibo(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string SubTotal, string Total, string Saldo, string ClaveSub)
        {
            try
            {
                string Cons = string.Empty;

                cmd = new SqlCommand("Select top 1 * from Recibo order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;
                    dr.Close();

                    cmd = new SqlCommand("Select top 1 * from Recibo where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        int Consecutivo = Convert.ToInt32(dr["Consecutivo"].ToString());
                        Consecutivo++;

                        Cons = Consecutivo.ToString();
                        dr.Close();
                    }
                    else
                    {
                        Cons = "1";
                        dr.Close();

                    }

                    txtFolio.Text = Convert.ToString(Folio);
                    cmd = new SqlCommand("insert into Recibo (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClavePropietario, Divisa, TipoCambio, Descuento, Cargo, Notas, Elaborado, SubTotal, Total, Saldo, TotalPartidas, Propiedad, Consecutivo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', 0.00, 0.00, '" + Notas + "', '" + Elaborado + "', '"+ SubTotal +"','"+ Total +"','"+ Saldo+ "', '1', '" + ClaveSub + "', '" + Cons + "')", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + ClavePropietario + "')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {

                    dr.Close();

                    cmd = new SqlCommand("Select top 1 * from Recibo where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        int Consecutivo = Convert.ToInt32(dr["Consecutivo"].ToString());
                        Consecutivo++;

                        Cons = Consecutivo.ToString();
                        dr.Close();
                    }
                    else
                    {
                        Cons = "1";
                        dr.Close();

                    }

                    txtFolio.Text = "1";
                    cmd = new SqlCommand("insert into Recibo (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClavePropietario, Divisa, TipoCambio, Descuento, Cargo, Notas, Elaborado, SubTotal, Total, Saldo, TotalPartidas, Propiedad, Consecutivo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', 0.00, 0.00, '" + Notas + "', '" + Elaborado + "', '" + SubTotal + "','" + Total + "','" + Saldo + "', '1', '" + ClaveSub + "', '" + Cons + "')", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values (1, '" + ClavePropietario + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartida(string Folio, string Partida, string ClaveRecibo, string MesAño, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into Partida (FolioRecibo, Partida, ClaveRecibo, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '"+MesAño+"', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Subtotal + "', '0.00', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
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
        //_____________________________________________________________________________________________________
        public string[] InformacionRecibo(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ConceptosIngreso where (Clave + ' - ' + Descripcion)= '" + Recibo + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                    dr[1].ToString(),
                     dr[4].ToString(),
                      dr[9].ToString(),


                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //____________________________________________________________________________________________________________________________________________
        //obtener correo y contraseña del sitema
        public int CorreoContra()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Correo, Servidor, Contraseña from DatosEmpresa", cn);
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
                    Correo = dt.Rows[0][0].ToString();
                    Servidor = dt.Rows[0][1].ToString();
                    Contraseña = dt.Rows[0][2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
    }
}
