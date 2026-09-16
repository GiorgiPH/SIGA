using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Almacenes
{
    class DBAlmacenes
    {
        // NOTA: se eliminaron los campos de instancia (cn, cmd, dr, da, dt) que se
        // compartían entre métodos. Esa práctica dejaba conexiones y DataReaders
        // abiertos (el constructor abría "cn" una sola vez y nunca se cerraba).
        // Ahora cada método abre su propia conexión dentro de un bloque "using",
        // por lo que se cierra y libera automáticamente, incluso si hay una excepción.
        //
        // Las firmas de los métodos, los parámetros de entrada y los valores de
        // retorno son EXACTAMENTE los mismos que en la clase original, así como
        // la lógica de negocio (mismas consultas, mismo orden de operaciones).

        public static int Folio = 0;
        public static int Partida = 0;
        public static int Opcion = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBAlmacenes()
        {
            // Antes este constructor abría y dejaba abierta una conexión durante
            // toda la vida del objeto. Ahora cada método administra su propia
            // conexión, así que ya no es necesario abrir nada aquí. Se conserva
            // el constructor vacío para no romper código existente que hace
            // "new DBAlmacenes()".
        }

        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveSiguiente()
        {
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select max(Clave) from Almacenes", cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                contador++;
                            }
                        }

                        if (contador > 0)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows[0][0].ToString() != string.Empty)
                                {
                                    Folio = Convert.ToInt32(dt.Rows[0][0].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }

        //________________________________________________________________________________________________
        //Categorias Registrados
        public void CargarAlmacenes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select * from Almacenes", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        public string[] InformacionAlmacen(string Orden)
        {
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" select OC.Clave, OC.Nombre, Oc.Estatus from Almacenes as OC where Clave='" + Orden + "'", cn))
            {
                cn.Open();
                string[] resultado = null;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr["Clave"].ToString(),
                            dr["Nombre"].ToString(),
                            dr["Estatus"].ToString(),
                        };
                        resultado = valores;
                    }
                }
                return resultado;
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaAlmacenSeleccionada(string Clave, Guna2ToggleSwitch cmbEstatus, Guna2TextBox txtNombre, Guna2TextBox txtCuentaContable)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from Almacenes where Clave='" + Clave + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["Estatus"].ToString().Trim() == "Activo")
                            {
                                cmbEstatus.Checked = true;
                            }
                            txtNombre.Text = dr["Nombre"].ToString();
                            txtCuentaContable.Text = dr["CuentaContable"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string RegistroAlmacen(string Clave, string Estatus, string Nombre, string CuentaContable)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from Almacenes where Clave='" + Clave + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into Almacenes (Clave, Estatus, Nombre, CuentaContable) values ('" + Clave + "', '" + Estatus + "', '" + Nombre + "', '" + CuentaContable + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        mensaje = "Registro guardado.";
                    }
                    else if (contador > 0)
                    {
                        if (MessageBox.Show("¿Desea actualizar el registro actual?", "Almacenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (SqlCommand cmd = new SqlCommand("Update Almacenes set  Estatus= '" + Estatus + "', Nombre='" + Nombre + "', CuentaContable= '" + CuentaContable + "' where Clave= '" + Clave + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            mensaje = "Registro modificado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }

        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.BeginUpdate();

            try
            {
                cb.Items.Clear();

                const string query = @"
            SELECT CONVERT(varchar(10), Clave) + ' - ' + Nombre AS Nombre
            FROM Almacenes
            ORDER BY Nombre";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cb.Items.Add(dr["Nombre"].ToString());
                        }
                    }
                }
            }
            finally
            {
                cb.EndUpdate();
            }
        }
        public DataTable ObtenerAlmacenes()
        {
            try
            {
                string consulta = @"
                    SELECT Clave, nombre, Estatus
                    FROM Almacenes 
                    WHERE Estatus='Activo'
                    ";
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new DataTable();
            }
        }
    }
}