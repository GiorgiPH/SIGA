using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Properties;
using System.Collections;

namespace PV.Clases.Remision
{
    public class DBRemiision
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;

        public DBRemiision()
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
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public void ConsultaTotalRemision(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                cmd = new SqlCommand("Select Total from Remision where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Subtotal.Text = dr["Total"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        public void CargarRemisionCobro(DataGridView dgv, string Matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                da = new SqlDataAdapter("select R.*, D.Nombre from Remision as R, Documento as D where ClaveProveedor='" + Matricula + "' and Saldo!=0 and R.ClaveDocumento=D.Clave", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    decimal subtotal = Convert.ToDecimal(item["Subtotal"]);


                    dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Consecutivo"].ToString();
                    //dgv.Rows[n].Cells[4].Value = item["PropiedadNombre"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[7].Value = subtotal;
                    dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                    dgv.Rows[n].Cells[9].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");
                    dgv.Rows[n].Cells[13].Value = Convert.ToDecimal(item["Cargo"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[26].ReadOnly = Convert.ToDecimal(item["Recargo"]) > 0.00m ? true : false;

                    //dgv.Rows[n].Cells[13].Value = "0.00";
                    dgv.Rows[n].Cells[16].Value = Convert.ToDecimal(item["Descuento"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[17].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                    //dgv.Rows[n].Cells[18].Value = Convert.ToDecimal(item["Importe"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[19].Value = Convert.ToDecimal(item["ImporteA"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[20].Value = Convert.ToDecimal(item["Area"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[21].Value = item["TipoCuota"].ToString();
                    //dgv.Rows[n].Cells[22].Value = Convert.ToDecimal(item["ProIndiviso"]).ToString();
                    //dgv.Rows[n].Cells[23].Value = Convert.ToDecimal(item["CuotaMantenimiento"]).ToString("N", formato);
                    //dgv.Rows[n].Cells[24].Value = item["ClaveRecibo"].ToString();
                    //dgv.Rows[n].Cells[25].Value = Convert.ToDateTime(item["FechaRecargo"]).ToString("yyyy/MM/dd");
                    //dgv.Rows[n].Cells[26].Value = Convert.ToDecimal(item["Recargo"]) > 0.00m ? 1 : 0;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        public void CargarReciboCobroById(DataGridView dgv, string Matricula, ArrayList ListaConcep)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                string Concepto = string.Empty;
                dgv.Rows.Clear();

                foreach (object item2 in ListaConcep)
                {
                    Concepto = item2.ToString();

                    da = new SqlDataAdapter("select R.*, 0.00 as DescuentoPago, 0.00 as RecargosAcumulados, D.Nombre from Remision as R, Documento as D where ClaveProveedor='" + Matricula + "' and R.Folio='" + Concepto + "' and R.ClaveDocumento=D.Clave", cn);
                    dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Cargo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["Descuento"]).ToString("N", formato);
                        dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        dgv.Rows[n].Cells[7].Value = (Convert.ToDecimal(item["Saldo"]) + Convert.ToDecimal(item["RecargosAcumulados"])).ToString("N", formato);
                        dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                        dgv.Rows[n].Cells[11].Value = Convert.ToDecimal(0.00).ToString("N", formato);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }
        public void ActualizarRemision(string Folio, decimal Recargos, decimal Descuento, decimal Saldo)
        {
            try
            {

                cmd = new SqlCommand("Update Remision set DescuentoPago=" + Descuento + ", Saldo=Saldo - " + Saldo + " where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public string[] InformacionRemision(string Orden)
        {
            cmd = new SqlCommand("select Folio, ClaveDocumento, Fecha, ClaveProveedor, Consecutivo from Remision as OC where Folio='" + Orden + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr["Folio"].ToString(),
                     dr["ClaveDocumento"].ToString(),
                     dr["Fecha"].ToString(),
                     dr["ClaveProveedor"].ToString(),
                     dr["Consecutivo"].ToString(),
         

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
    }
}
