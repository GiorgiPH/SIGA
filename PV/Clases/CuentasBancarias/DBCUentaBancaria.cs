using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.CuentasBancarias
{
    class DBCUentaBancaria
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

        public DBCUentaBancaria()
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
                cmd = new SqlCommand("select max(Clave) from CuentasBancarias", cn);
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
        //________________________________________________________________________________________________
        //divisas Registrados
        public void CargarCuentas(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from CuentasBancarias", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string RegistroCuenta(string txtClaveDivisa, string txtNombre, string cmdEstatus, string dtpFecha, string txtCuenta, string cuentasat, string cuentacontable)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from CuentasBancarias where Clave='" + txtClaveDivisa + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into CuentasBancarias (Clave, Nombre, Estatus, Fecha, Cuenta, CuentaSat, CuentaContable) values ('" + txtClaveDivisa + "', '" + txtNombre + "', '" + cmdEstatus + "', '" + dtpFecha+ "', '" + txtCuenta + "', '"+cuentasat+"', '"+cuentacontable+"')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Cuentas Bancarias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update CuentasBancarias set Nombre='" + txtNombre + "', Estatus='" + cmdEstatus + "', Fecha='" + dtpFecha + "', Cuenta='" + txtCuenta + "' where Clave= '" + txtClaveDivisa + "'", cn);
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
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaCuentaSeleccionado(string txtClaveDivisa, Guna2TextBox txtNombre, ComboBox cmdEstatus, Guna2DateTimePicker dtpFecha, Guna2TextBox txtcuenta, Guna2TextBox txtcuentaSAT, Guna2TextBox txtcuentaContable)
        {
            try
            {
                cmd = new SqlCommand("Select * from CuentasBancarias where Clave='" + txtClaveDivisa + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    cmdEstatus.Text = dr["Estatus"].ToString();
                    dtpFecha.Text = dr["Fecha"].ToString();
                    txtcuenta.Text = dr["Cuenta"].ToString();
                    txtcuentaSAT.Text = dr["CuentaSat"].ToString();
                    txtcuentaContable.Text = dr["CuentaContable"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select D.* from CuentasBancarias as D where D.Clave='"+txtClaveDivisa+"' and not exists (select CuentaBancaria from Egreso as DE where D.Clave=DE.CuentaBancaria) and not exists (select CuentaBancaria from Cobros as R where D.Clave=R.CuentaBancaria) ", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    mensaje = "El registro esta en uso, no es posible eliminar";
                }
                else if (contador > 0)
                {
                    cmd = new SqlCommand("Delete CuentasBancarias  where Clave='" + txtClaveDivisa + "'", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro Eliminado.";

                }

            }
            catch (Exception)
            {
                MessageBox.Show("El registro esta en uso, no es posible eliminar");
            }
            return mensaje;
        }
    }
}
