using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Divisas
{
    class DBDivisas
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

        public DBDivisas()
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
        public int ClaveDivisaSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(ClaveDivisa) from Divisas", cn);
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
        // registrar divisa 
        public string RegistroDivisa(string txtClaveDivisa, string txtNombre, string cmdEstatus, string txtTipoCambio, string dtpFechaCambio, string txtNotas)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Divisas where ClaveDivisa='" + txtClaveDivisa + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into Divisas (ClaveDivisa, Nombre, Estatus, TipoCambio, FechaCambio, Notas) values ('" + txtClaveDivisa + "', '" + txtNombre + "', '" + cmdEstatus + "', '" + txtTipoCambio + "', '" + dtpFechaCambio + "', '" + txtNotas + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Catalogo de Divisas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Divisas set Nombre='" + txtNombre + "', Estatus='" + cmdEstatus + "', TipoCambio='" + txtTipoCambio + "', FechaCambio='" + dtpFechaCambio + "', Notas='" + txtNotas + "' where ClaveDivisa= '" + txtClaveDivisa + "'", cn);
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
        // registrar divisa 
        public void RegistroDivisaHistorial(string txtClaveDivisa, string txtNombre, string cmdEstatus, string txtTipoCambio, string dtpFechaCambio, string txtNotas, string Elaborado)
        {

            try
            {
                cmd = new SqlCommand("Insert into DivisasHistorial (ClaveDivisa, Nombre, Estatus, TipoCambio, FechaCambio, Notas, RegistradoPor) values ('" + txtClaveDivisa + "', '" + txtNombre + "', '" + cmdEstatus + "', '" + txtTipoCambio + "', '" + dtpFechaCambio + "', '" + txtNotas + "', '"+Elaborado+"')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete Divisas where ClaveDivisa='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete DivisasHistorial where ClaveDivisa='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                MessageBox.Show("El registro esta en uso, no es posible eliminar");
            }
            return mensaje;
        }
        //________________________________________________________________________________________________
        //divisas Registrados
        public void CargarDivisa(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Divisas", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveDivisa"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
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
        public void ConsultaDivisaSeleccionado(string txtClaveDivisa, Guna2TextBox txtNombre, ComboBox cmdEstatus, Guna2TextBox txtTipoCambio, DateTimePicker dtpFechaCambio, Guna2TextBox txtNotas)
        {
            try
            {
                cmd = new SqlCommand("Select * from Divisas where ClaveDivisa='" + txtClaveDivisa + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    cmdEstatus.Text = dr["Estatus"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    dtpFechaCambio.Text = dr["FechaCambio"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();

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
        public void SeleccionarDivisa(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            //cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select * from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        public void SeleccionarDivisaActivas(ComboBox cb)
        {
            //dr.Close();
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select * from Divisas where estatus = 'Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
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
        public string[] InformacionDivisa(string Documento)
        {
            cmd = new SqlCommand("Select TipoCambio from Divisas where Nombre = '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            if (dr.Read())
            {
                string[] valores =
                {
                  decimal.Round(Convert.ToDecimal( dr[0]), 2).ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
    }
}
