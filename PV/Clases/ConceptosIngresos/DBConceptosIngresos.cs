using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace Condominios.Clases.ConceptosIngresos
{
    class DBConceptosIngresos
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

        public DBConceptosIngresos()
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
        public int ClaveAreaComunSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Clave) from ConceptosIngreso", cn);
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
        // registrar categorias 
        public string RegistroConceptoIngreso(string txtClave, string txtNombre, string GenerarRecargo, string DiarioMensual, string ManualAutoma, string CalcularPor, string ImportePorcentaje, decimal Importe, string Porcentaje, decimal ImporteConcepto)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ConceptosIngreso where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into ConceptosIngreso (Clave, Descripcion, GenerarRecargo, DiarioMensual, ManualAutoma, CalcularPor, ImportePorcentaje, Importe, Porcentaje, ImporteConcepto) values ('" + txtClave + "', '" + txtNombre + "', '" + GenerarRecargo + "', '" + DiarioMensual + "', '" + ManualAutoma + "', '" + CalcularPor + "', '" + ImportePorcentaje + "', '" + Importe + "', '" + Porcentaje + "', "+ImporteConcepto+")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Concepto Ingresos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update ConceptosIngreso set  Descripcion='" + txtNombre + "', GenerarRecargo='" + GenerarRecargo + "', DiarioMensual='" + DiarioMensual + "', ManualAutoma='" + ManualAutoma + "', CalcularPor='" + CalcularPor + "', ImportePorcentaje='" + ImportePorcentaje + "', Importe='" + Importe + "', Porcentaje='" + Porcentaje + "', ImporteConcepto="+ImporteConcepto+" where Clave= '" + txtClave + "'", cn);
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
        // registrar categorias 
        public string RegistroConceptoIngreso2(string txtClave, string txtNombre, string GenerarRecargo, decimal ImporteConcepto)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ConceptosIngreso where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into ConceptosIngreso (Clave, Descripcion, GenerarRecargo, ImporteConcepto) values ('" + txtClave + "', '" + txtNombre + "', '" + GenerarRecargo + "',  " + ImporteConcepto + ")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Concepto Ingresos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update ConceptosIngreso set  Descripcion='" + txtNombre + "', GenerarRecargo='" + GenerarRecargo + "', ImporteConcepto=" + ImporteConcepto + " where Clave= '" + txtClave + "'", cn);
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
        //________________________________________________________________________________________________
        //Categorias Registrados
        public void CargarConceptos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptosIngreso", cn);
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
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaConceptosSeleccionada(string txtClave, TextBox txtDescripcion, TextBox ImporteConcepto, RadioButton rdbSiRecargos, RadioButton rdbNoRecargos, RadioButton rdbDiario, RadioButton rdbMensual, RadioButton rdbManual, RadioButton rdbAutomatico, CheckBox cbSiDiario, CheckBox cbSiMensual, ComboBox cmbDiario, ComboBox cmbMensual, TextBox txtImporteDiario, TextBox txtImporteMensual, TextBox txtPorcentajeDiario, TextBox txtPorcentajeMensual)
        {
            try
            {
                cmd = new SqlCommand("Select * from ConceptosIngreso where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    ImporteConcepto.Text = dr["ImporteConcepto"].ToString();

                    if (dr["GenerarRecargo"].ToString() == "Si")
                    {
                        rdbSiRecargos.Checked = true;

                        if (dr["DiarioMensual"].ToString() == "Diario")
                        {
                            rdbDiario.Checked = true;
                            if (dr["ManualAutoma"].ToString() == "Manual")
                            {
                                rdbManual.Checked = true;
                            }
                            else if (dr["ManualAutoma"].ToString() == "Automatico")
                            {
                                rdbAutomatico.Checked = true;
                            }

                            if (dr["CalcularPor"].ToString() == "Si")
                            {
                                cbSiDiario.Checked = true;
                            }

                            if (dr["ImportePorcentaje"].ToString() == "IMPORTE")
                            {
                                cmbDiario.Text = dr["ImportePorcentaje"].ToString();
                                txtImporteDiario.Text = dr["Importe"].ToString();
                            }
                            else if (dr["ImportePorcentaje"].ToString() == "PORCENTAJE")
                            {
                                cmbDiario.Text = dr["ImportePorcentaje"].ToString();
                                txtPorcentajeDiario.Text = dr["Porcentaje"].ToString();
                            }
                        }
                        else if (dr["DiarioMensual"].ToString() == "Mensual")
                        {
                            rdbMensual.Checked = true;
                            if (dr["ManualAutoma"].ToString() == "Manual")
                            {
                                rdbManual.Checked = true;
                            }
                            else if (dr["ManualAutoma"].ToString() == "Automatico")
                            {
                                rdbAutomatico.Checked = true;
                            }

                            if (dr["CalcularPor"].ToString() == "Si")
                            {
                                cbSiMensual.Checked = true;
                            }

                            if (dr["ImportePorcentaje"].ToString() == "IMPORTE")
                            {
                                cmbMensual.Text = dr["ImportePorcentaje"].ToString();
                                txtImporteMensual.Text = dr["Importe"].ToString();
                            }
                            else if (dr["ImportePorcentaje"].ToString() == "PORCENTAJE")
                            {
                                cmbMensual.Text = dr["ImportePorcentaje"].ToString();
                                txtPorcentajeMensual.Text = dr["Porcentaje"].ToString();
                            }
                        }
                    }
                    else
                    {
                        rdbNoRecargos.Checked = true;
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
    }
}
