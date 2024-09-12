using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PuntoVentas.Clases.CategoriasFamilias
{
    class DBCategoriasFamilias
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

        public DBCategoriasFamilias()
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
        public int ClaveCategoriaSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(ClaveCategoria) from Categorias", cn);
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
        public string RegistroCategoriasFamilias(string txtClaveCategoria, string txtNombre, ArrayList ListaConceptos, ArrayList ListaConceptos2, ArrayList ListaConceptos3)
        {
            string ClaveFamilia = string.Empty;
            string NombreFamilia = string.Empty;
            string Vincular = string.Empty;
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Categorias where ClaveCategoria='" + txtClaveCategoria + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into Categorias (ClaveCategoria, Nombre) values ('" + txtClaveCategoria + "', '" + txtNombre + "')", cn);
                    cmd.ExecuteNonQuery();

                    foreach (object item in ListaConceptos)
                    {
                        ClaveFamilia = item.ToString();

                        foreach (object item2 in ListaConceptos2)
                        {
                            NombreFamilia = item2.ToString();
                            ListaConceptos2.Remove(item2);
                           break;
                        }
                        foreach (object item3 in ListaConceptos3)
                        {
                            Vincular = item3.ToString();
                            ListaConceptos3.Remove(item3);
                            break;
                        }


                        cmd = new SqlCommand("Insert into Familias (ClaveFamilia, Nombre, ClaveCategoria,Vincular) values ('" + ClaveFamilia + "', '" + NombreFamilia + "', '"+ txtClaveCategoria  + "', '" + Vincular + "')", cn);
                        cmd.ExecuteNonQuery();
                    }


                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Categorias y Familias", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Categorias set  Nombre='" + txtNombre + "'  where ClaveCategoria= '" + txtClaveCategoria + "'", cn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Delete Familias where ClaveCategoria= '" + txtClaveCategoria + "'", cn);
                        cmd.ExecuteNonQuery();

                        foreach (object item in ListaConceptos)
                        {
                            ClaveFamilia = item.ToString();

                            foreach (object item2 in ListaConceptos2)
                            {
                                NombreFamilia = item2.ToString();
                                ListaConceptos2.Remove(item2);
                              break;
                            }
                            foreach (object item3 in ListaConceptos3)
                            {
                                Vincular = item3.ToString();
                                ListaConceptos3.Remove(item3);
                                break;
                            }


                            //cmd = new SqlCommand("Insert into Familias (ClaveFamilia, Nombre, ClaveCategoria) values ('" + ClaveFamilia + "', '" + NombreFamilia + "', '" + txtClaveCategoria + "')", cn);
                            cmd = new SqlCommand("Insert into Familias (ClaveFamilia, Nombre, ClaveCategoria,Vincular) values ('" + ClaveFamilia + "', '" + NombreFamilia + "', '" + txtClaveCategoria + "', '" + Vincular + "')", cn);
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
        public void CargarCategorias(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Categorias", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveCategoria"].ToString();
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
        public void ConsultaCategoriaSeleccionada(string txtClaveCategoria, Guna2TextBox txtNombre, DataGridView dgv)
        {
            try
            {
                cmd = new SqlCommand("Select * from Categorias where ClaveCategoria='" + txtClaveCategoria + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombre.Text = dr["Nombre"].ToString();
                    
                }
                dr.Close();

                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select *, CONVERT(INT, SUBSTRING (claveFamilia, 3,1000)) as Orden from Familias where ClaveCategoria = '" + txtClaveCategoria+ "' order by Orden asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["ClaveFamilia"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Vincular"].ToString();
                }
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
            try
            {
                cmd = new SqlCommand("Delete Familias where ClaveCategoria='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete Categorias where ClaveCategoria='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception )
            {
                MessageBox.Show("El registro esta en uso, no es posible eliminar");
            }
            return mensaje;
        }
    }
}
