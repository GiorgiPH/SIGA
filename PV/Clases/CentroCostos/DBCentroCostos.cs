using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace Condominios.Clases.CentroCostos
{
    class DBCentroCostos
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Eliminado = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBCentroCostos()
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
        public int ClaveCentroSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Clave) from CentroCostos", cn);
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
        public string RegistroCentroCostos(string txtClave, string txtNombre, string cmbEstatus, string txtCuentacontable, string txtDescricpion, ArrayList ListaConceptos, ArrayList ListaConceptos2)
        {
            string ClaveDep = string.Empty;
            string NombreDep = string.Empty;
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from CentroCostos where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into CentroCostos (Clave, Nombre, Estatus, CuentaContable, Descripcion) values ('" + txtClave + "', '" + txtNombre + "', '" + cmbEstatus + "', '" + txtCuentacontable + "', '" + txtDescricpion + "')", cn);
                    cmd.ExecuteNonQuery();

                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item2 in ListaConceptos2)
                        {
                            NombreDep = item2.ToString();
                            ListaConceptos2.Remove(item2);
                            break;
                        }

                        cmd = new SqlCommand("Insert into CentroCostos_Departamentos (Clave, Nombre, CentroCosto) values ('" + ClaveDep + "', '" + NombreDep + "', '" + txtClave + "')", cn);
                        cmd.ExecuteNonQuery();
                    }

                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Centros de Costos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update CentroCostos set  Nombre='" + txtNombre + "', Estatus= '" + cmbEstatus + "', CuentaContable= '" + txtCuentacontable + "', Descripcion='" + txtDescricpion + "' where Clave= '" + txtClave + "'", cn);
                        cmd.ExecuteNonQuery();

                        foreach (object item in ListaConceptos)
                        {
                            ClaveDep = item.ToString();

                            foreach (object item2 in ListaConceptos2)
                            {
                                NombreDep = item2.ToString();
                                ListaConceptos2.Remove(item2);
                                break;
                            }
                            cmd = new SqlCommand("if not Exists( Select Departamento from Departamentos_SubDepartamento where Departamento='" + ClaveDep + "') Delete CentroCostos_Departamentos where Clave='" + ClaveDep + "'", cn);
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("if not Exists( Select Clave from CentroCostos_Departamentos where Clave='" + ClaveDep + "') Insert into CentroCostos_Departamentos (Clave, Nombre, CentroCosto) values ( '" + ClaveDep + "','" + NombreDep + "','" + txtClave + "')", cn);
                            cmd.ExecuteNonQuery();
                        }

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
        public void CargarCentros(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from CentroCostos", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaCentrosSeleccionada(string txtclave, Guna2TextBox txtNombre, ComboBox cmbEstatus, Guna2TextBox txtCuenta, Guna2TextBox txtDescripcion, DataGridView dgv)
        {
            try
            {
                cmd = new SqlCommand("Select * from CentroCostos where Clave='" + txtclave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtCuenta.Text = dr["CuentaContable"].ToString();
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                }
                dr.Close();

                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select *, CONVERT(INT, SUBSTRING (Clave, 3,1000)) as Orden from CentroCostos_Departamentos where CentroCosto = '" + txtclave + "' order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public int ConsultaExistencia(string txtclave)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("Select * from CentroCostos_Departamentos where Clave='" + txtclave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return contador;
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaSubDepartamentoSeleccionada(string txtclave, DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select *, CONVERT(INT, SUBSTRING (Clave, 5,1000)) as Orden from Departamentos_SubDepartamento where Departamento = '" + txtclave + "' order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string EliminarDepartamento(string txtClave)
        {
            int contador = 0;
            string mensaje = string.Empty;

            try
            {
                cmd = new SqlCommand("select * from Departamentos_SubDepartamento where Departamento='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Delete CentroCostos_Departamentos where Clave='" + txtClave + "'", cn);
                    cmd.ExecuteNonQuery();
                    Eliminado = 0;
                    mensaje = "Eliminado";
                }
                else
                {
                    mensaje = "Elimine los SubDepartamentos para continuar";
                    Eliminado = 1;
                }
            }
            catch (Exception)
            {

            }
            return mensaje;
        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public void EliminarSubDepartamento(string txtClave)
        {
            try
            {
                cmd = new SqlCommand("Delete Departamentos_SubDepartamento where Clave='" + txtClave + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }

        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string RegistroSubDepartamento(string Departamento, ArrayList ListaConceptos, ArrayList ListaConceptos2)
        {
            string ClaveDep = string.Empty;
            string NombreDep = string.Empty;
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Departamentos_SubDepartamento where  Departamento='" + Departamento + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    foreach (object item in ListaConceptos)
                    {
                        ClaveDep = item.ToString();

                        foreach (object item2 in ListaConceptos2)
                        {
                            NombreDep = item2.ToString();
                            ListaConceptos2.Remove(item2);
                            break;
                        }

                        cmd = new SqlCommand("Insert into Departamentos_SubDepartamento (Clave, Nombre, Departamento) values ('" + ClaveDep + "', '" + NombreDep + "', '" + Departamento + "')", cn);
                        cmd.ExecuteNonQuery();
                    }


                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Centro de Costos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (object item in ListaConceptos)
                        {
                            ClaveDep = item.ToString();

                            foreach (object item2 in ListaConceptos2)
                            {
                                NombreDep = item2.ToString();
                                ListaConceptos2.Remove(item2);
                                break;
                            }

                            cmd = new SqlCommand("Update Departamentos_SubDepartamento set Nombre='" + NombreDep + "', Departamento='" + Departamento + "' where Clave= '" + ClaveDep + "'", cn);
                            cmd.ExecuteNonQuery();
                        }

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
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete CentroCostos_Departamentos where CentroCosto='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete CentroCostos where Clave='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje="El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
    }
}
