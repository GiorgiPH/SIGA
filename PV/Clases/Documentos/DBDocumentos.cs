using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace Condominios.Clases.Documentos
{
    class DBDocumentos
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
        public void CerrarConexion()
        {
            try
            {
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public DBDocumentos()
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
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string RegistroDocumento(string TipoDocumento, string Clase, string Clave, string Nombre, string Almace, string UltimoFolio, string Consecutivo, string Bloquear, string Cuenta, string Cuenta2, string tarea, bool CentroCosto)
        {
            string mensaje = "";
            int contador = 0;
            string MostrarCentroCosto = CentroCosto ? "1" : "0";

            try
            {
                cmd = new SqlCommand("select * from Documento where Clave='" + Clave + "' and TipoDocumento= '" + TipoDocumento + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Documento (TipoDocumento, Clase, Clave, Nombre, Almace, UltimoFolio, Consecutivo, Bloquear, Cuenta, Cuenta2, Tarea, MostrarCentroCosto) values ('" + TipoDocumento + "', '" + Clase + "', '" + Clave + "', '" + Nombre + "', '" + Almace + "', '" + UltimoFolio + "', '" + Consecutivo + "', '" + Bloquear + "', '" + Cuenta + "', '" + Cuenta2 + "', '" + tarea + "', " + MostrarCentroCosto + ")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";
                }
                else if (contador > 0)
                {
                    contador = 0;
                    cmd = new SqlCommand("select * from Documento where Clave='" + Clave + "' and TipoDocumento= '" + TipoDocumento + "' and UltimoFolio = '0'", cn);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        contador++;
                    }
                    dr.Close();

                    if (contador <= 0)
                    {
                        if (MessageBox.Show("El documento ya tiene folios registrados solo es posible modificar los numeros de cuenta", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cmd = new SqlCommand("Update Documento set  Cuenta='" + Cuenta + "', Cuenta2= '" + Cuenta2 + "' where Clave='" + Clave + "' and TipoDocumento= '" + TipoDocumento + "'", cn);
                            cmd.ExecuteNonQuery();
                            mensaje = "Registro modificado.";
                        }
                    }
                    else if (contador > 0)
                    {
                        if (MessageBox.Show("El documento ya existe, si continua sera modificado", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cmd = new SqlCommand("Update Documento set Tarea='" + tarea + "', TipoDocumento=  '" + TipoDocumento + "', Clase='" + Clase + "', Almace='" + Almace + "', UltimoFolio='" + UltimoFolio + "', Consecutivo='" + Consecutivo + "', Bloquear='" + Bloquear + "', Cuenta='" + Cuenta + "', Cuenta2= '" + Cuenta2 + "', MostrarCentroCosto=" + MostrarCentroCosto + " where Clave='" + Clave + "' and TipoDocumento= '" + TipoDocumento + "'", cn);
                            cmd.ExecuteNonQuery();
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
        //________________________________________________________________________________________________
        //Documentos Registrados
        public void CargarDocumentos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Documento", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clase"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaDocumentoSeleccionado(ComboBox TipoDocumento, ComboBox Clase, string Clave, string Nombre, Guna2TextBox Almace, Guna2TextBox UltimoFolio, Guna2ToggleSwitch tgConsecutivo, Guna2ToggleSwitch tgBloquear, Guna2TextBox Cuenta, Guna2TextBox Cuenta2, ComboBox Tarea, Guna2ToggleSwitch tgCentroCosto)
        {
            try
            {
                cmd = new SqlCommand("Select * from Documento where Clave='" + Clave + "' and Nombre= '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TipoDocumento.Text = dr["TipoDocumento"].ToString();

                    Almace.Text = dr["Almace"].ToString();
                    UltimoFolio.Text = dr["UltimoFolio"].ToString();

                    string Consecutivo = dr["Consecutivo"].ToString();
                    if (Consecutivo == "Si")
                    {
                        tgConsecutivo.Checked = true;
                    }
                    else if (Consecutivo == "No")
                    {
                        tgConsecutivo.Checked = false;
                    }

                    string Bloqueo = dr["Bloquear"].ToString();
                    if (Bloqueo == "Si")
                    {
                        tgBloquear.Checked = true;
                    }
                    else if (Bloqueo == "No")
                    {
                        tgBloquear.Checked = false;
                    }

                    tgCentroCosto.Checked = Convert.ToBoolean(dr["MostrarCentroCosto"]);

                    Cuenta.Text = dr["Cuenta"].ToString();
                    Cuenta2.Text = dr["Cuenta2"].ToString();
                    Clase.Text = dr["Clase"].ToString();
                    Tarea.Text = dr["Tarea"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaDocumentoSeleccionado2(ComboBox Clase, string Clave, string Nombre)
        {
            try
            {
                cmd = new SqlCommand("Select * from Documento where Clave='" + Clave + "' and Nombre= '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Clase.Text = dr["Clase"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
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
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa, string txtClase, string txtTipoDocumento)
        {
            string mensaje = string.Empty;
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select D.* from Documento as D where D.Clave='" + txtClaveDivisa + "' and D.Clase='" + txtClase + "' and D.TipoDocumento='" + txtTipoDocumento + "' and not exists (select Documento from DatosEmpresa as DE where D.Clave=DE.Documento) and not exists (select ClaveDocumento from Recibo as R where D.Clave=R.ClaveDocumento) and not exists (select ClaveDocumento from OrdenCompra as R where D.Clave=R.ClaveDocumento) and not exists (select ClaveDocumento from RecepcionProducto as R where D.Clave=R.ClaveDocumento) and not exists (select ClaveDocumento from RegistroGastos as R where D.Clave=R.ClaveDocumento) and not exists (select ClaveDocumento from Requisicion as R where D.Clave=R.ClaveDocumento)  and not exists (select ClaveDocumento from NotasGasto as R where D.Clave=R.ClaveDocumento)", cn);
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
                    cmd = new SqlCommand("Delete Documento  where Clave='" + txtClaveDivisa + "' and TipoDocumento= '" + txtTipoDocumento + "' and Clase='"+txtClase+"'", cn);
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
        public DataTable ConsultarDocumento(string tipo = null, string clase = null)
        {
            string query = "SELECT * FROM Documento where 1=1";
            if (!string.IsNullOrEmpty(tipo))
            {
                query += " and TipoDocumento='" + tipo + "'";
            }
            if (!string.IsNullOrEmpty(clase))
            {
                query += " and clase='" + clase + "'";
            }
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(ObtenerCn()))
            {
                var command = new SqlCommand(query, connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }
    }
}
