using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.ActivoFijo
{
    class DBActivoFijo
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Partida = 0;
        public static int Opcion = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBActivoFijo()
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
        public int ClaveSiguiente()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Folio) from ActivoFijo", cn);
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
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int ClaveSiguienteResguardo()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Folio) from ResguardoActivoFijo", cn);
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
        //____________________________________________________________________________________________________________________________________________
        //Obtener la clave consecutiva
        public int PartidaSiguiente(string ActivoFijo)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select max(Partida) from ResguardoActivoFijo where ActivoFijo='" + ActivoFijo + "'", cn);
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
                        Partida = Convert.ToInt32(dt.Rows[0][0].ToString());
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
        public string RegistroActivoFijo(string txtFolio, string cmbEstatus, string txtProvedor, string Factura, string dtpFecha, string txtClave, string txtDescripcion, string cmbArticulo, string txtUnidad, string txtGarantia, string txtElaborado, string numeroserie, string Documento)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ActivoFijo where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("select * from ActivoFijo where Clave='" + txtClave + "'", cn);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        contador++;
                    }
                    dr.Close();

                    if (contador <= 0)
                    {
                        cmd = new SqlCommand("Insert into ActivoFijo (Folio, Estatus, Proveedor, Factura, Fecha, Clave, Descripcion, Articulo, Unidad, Garantia, Elaborado, NumeroSerie, Documento) values ('" + txtFolio + "', '" + cmbEstatus + "', '" + txtProvedor + "', '" + Factura + "', '" + dtpFecha + "',  '" + txtClave + "',  '" + txtDescripcion + "', '" + cmbArticulo + "',  '" + txtUnidad + "',  '" + txtGarantia + "',  '" + txtElaborado + "', '" + numeroserie + "', '"+Documento+"')", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro guardado.";
                        Opcion = 0;
                    }
                    else
                    {
                        mensaje = "La clave del activo ya existe, registre una clave diferente";
                        Opcion = 1;
                    }

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Activo Fijo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update ActivoFijo set Estatus='" + cmbEstatus + "', Proveedor='" + txtProvedor + "', Factura='" + Factura + "', Fecha='" + dtpFecha + "',  Clave='" + txtClave + "', Descripcion='" + txtDescripcion + "', Articulo='" + cmbArticulo + "', Unidad='" + txtUnidad + "', Garantia='" + txtGarantia + "', Elaborado='" + txtElaborado + "', NumeroSerie='" + numeroserie + "', Documento='"+Documento+"' where Folio= '" + txtFolio + "'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado.";
                        Opcion = 0;

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
        public int ClaveActivo(string txtClave)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ActivoFijo where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return contador;

        }
        //________________________________________________________________________________________________
        //Categorias Registrados
        public void CargarActivoFijo(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ActivoFijo", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Articulo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Categorias Registrados
        public void CargarResguardoActivoFijo(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ResguardoActivoFijo", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ActivoFijo"].ToString();
                    dgv.Rows[n].Cells[2].Value = Convert.ToDateTime(item["ResguardoFecha"]).ToString("yyyy/MM/dd");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaActivoSeleccionada(string Clave, ComboBox cmbEstatus, ComboBox txtProveedor, TextBox Factura, DateTimePicker dtpFecha, TextBox txtClave, TextBox txtDescripcion, ComboBox cmbArticulo, TextBox txtUnidad, TextBox txtGarantia, TextBox txtElaborado, TextBox txtnumeroserie, TextBox txtDocumento)
        {
            try
            {
                cmd = new SqlCommand("Select * from ActivoFijo where Folio='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtProveedor.Text = dr["Proveedor"].ToString();
                    Factura.Text = dr["Factura"].ToString();
                    dtpFecha.Text = dr["Fecha"].ToString();
                    txtClave.Text = dr["Clave"].ToString();
                    txtDescripcion.Text = dr["Descripcion"].ToString();
                    cmbArticulo.Text = dr["Articulo"].ToString();
                    txtUnidad.Text = dr["Unidad"].ToString();
                    txtGarantia.Text = dr["Garantia"].ToString();
                    txtElaborado.Text = dr["Elaborado"].ToString();
                    txtnumeroserie.Text = dr["NumeroSerie"].ToString();
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
        //Mostrar Usuario seleccionado
        public void ConsultaUltimoResguardoActivoSeleccionada(string Clave, TextBox Empleado, TextBox CentroCosto, TextBox Resguardo, TextBox Devolucion)
        {
            try
            {
                cmd = new SqlCommand("Select top(1) Folio, Empleado, CentroCosto, ResguardoFecha, Devolucion, DevolucionFecha from ResguardoActivoFijo where ActivoFijo='" + Clave + "' Order By Folio Desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Empleado.Text = dr["Empleado"].ToString();
                    CentroCosto.Text = dr["CentroCosto"].ToString();
                    Resguardo.Text = Convert.ToDateTime(dr["ResguardoFecha"]).ToString("yyyy/MM/dd");

                    if (dr["Devolucion"].ToString() != string.Empty)
                    {
                        Devolucion.Text = Convert.ToDateTime(dr["DevolucionFecha"]).ToString("yyyy/MM/dd");
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
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaResguardoActivoSeleccionada(string Folio, ComboBox ActivoFijo, TextBox Partida, ComboBox Empleado, ComboBox CentroCosto, TextBox Ubicacion, CheckBox Resguardo, DateTimePicker ResguardoFecha, CheckBox Devolucion, DateTimePicker DevolucionFecha, TextBox Elaborado, TextBox Notas, TextBox DevolucionRegistrado)
        {
            try
            {
                cmd = new SqlCommand("Select * from ResguardoActivoFijo where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string resguardo = string.Empty;
                    string devolucion = string.Empty;

                    Partida.Text = dr["Partida"].ToString();
                    Empleado.Text = dr["Empleado"].ToString();
                    CentroCosto.Text = dr["CentroCosto"].ToString();
                    Ubicacion.Text = dr["Ubicacion"].ToString();

                    if (dr["Resguardo"].ToString() == "Si")
                    {
                        Resguardo.Checked = true;
                    }
                    ResguardoFecha.Text = dr["ResguardoFecha"].ToString();

                    if (dr["Devolucion"].ToString() == "Si")
                    {
                        Devolucion.Checked = true;
                    }
                    DevolucionFecha.Text = dr["DevolucionFecha"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    DevolucionRegistrado.Text = dr["DevolucionRegistrado"].ToString();

                    ActivoFijo.Text = dr["ActivoFijo"].ToString();

                }
                dr.Close();
            }
            catch (Exception)
            {
                dr.Close();
            }
        }
        //___________________________________________________________________________________________
        public void SeleccionarArticulo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select PS.Descripcion from ProductosServicios as PS, Categorias as C where C.Nombre='Activo Fijo' and PS.Categoria=C.ClaveCategoria", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void Proveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select RazonSocial from Proveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void DocumentoRecepciopn(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (convert(varchar,RP.Consecutivo) + ' - ' + RP.ClaveDocumento + ' - ' + D.Nombre) as Nombre from RecepcionProducto as RP, Documento as D where RP.ClaveDocumento=D.Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDocumentoRecepciopn(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("select RP.Folio, RP.ClaveProveedor,  P.RazonSocial, RP.Referencia from RecepcionProducto as RP, Documento as D, Proveedor as P where (convert(varchar,RP.Consecutivo) + ' - ' + RP.ClaveDocumento + ' - ' + D.Nombre)='" + nombre + "' and RP.ClaveDocumento=D.Clave and Rp.ClaveProveedor=P.IdProveedor", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                    dr[1].ToString(),
                     dr[2].ToString(),
                      dr[3].ToString(),
            };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDocumentoRecepciopn2(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("select (convert(varchar,RP.Consecutivo) + ' - ' + RP.ClaveDocumento + ' - ' + D.Nombre) from RecepcionProducto as RP, Documento as D, Proveedor as P where Rp.Folio='" + nombre + "' and RP.ClaveDocumento=D.Clave and Rp.ClaveProveedor=P.IdProveedor", cn);
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
        //___________________________________________________________________________________________
        public void SeleccionarPersonal(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select ( Nombre + ' ' + ApellidoP + ' ' + ApellidoM) as Nombre from Empleados where Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarCentroCosto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select Nombre from CentroCostos", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarActivoFijo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (Clave + ' - ' + Descripcion) as ActivoFijo from ActivoFijo as AF where not exists (select ActivoFijo from ResguardoActivoFijo as RA where (AF.Clave + ' - ' + AF.Descripcion)=RA.ActivoFijo and RA.Resguardo!='' and RA.Devolucion='')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        //___________________________________________________________________________________________
        public void SeleccionarActivoFijo2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (Clave + ' - ' + Descripcion) as ActivoFijo from ActivoFijo as AF where not exists (select ActivoFijo from ResguardoActivoFijo as RA where (AF.Clave + ' - ' + AF.Descripcion)=RA.ActivoFijo and RA.Resguardo!='' and RA.Devolucion='')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarProveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select DISTINCT Proveedor from ActivoFijo", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarActivoFijo2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (Clave + ' - ' + Descripcion) as ActivoFijo from ActivoFijo as AF ", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionActivoFijo(string nombre)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ActivoFijo where (Clave + ' - ' + Descripcion) = '" + nombre + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[1].ToString(),
                    dr[6].ToString(),
            };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________________________________________--
        // registrar categorias 
        public string RegistroResguardoActivoFijo(string Folio, string ActivoFijo, string Partida, string Empleado, string CentroCosto, string Ubicacion, CheckBox Resguardo, string ResguardoFecha, CheckBox Devolucion, string DevolucionFecha, string Elaborado, string Notas, string DevolucionRegistro)
        {
            string mensaje = string.Empty;
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ResguardoActivoFijo where Folio='" + Folio + "' and Devolucion=''", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    string resguardo = string.Empty;
                    string devolucion = string.Empty;

                    if (Resguardo.Checked == true)
                    {
                        resguardo = "Si";
                    }

                    if (Devolucion.Checked == true)
                    {
                        devolucion = "Si";
                    }

                    cmd = new SqlCommand("Insert into ResguardoActivoFijo (Folio, ActivoFijo, Partida, Empleado, CentroCosto, Ubicacion, Resguardo, ResguardoFecha, Devolucion, DevolucionFecha, Elaborado) values ('" + Folio + "', '" + ActivoFijo + "', '" + Partida + "', '" + Empleado + "',  '" + CentroCosto + "', '" + Ubicacion + "', '" + resguardo + "', '" + ResguardoFecha + "',  '" + devolucion + "',  '" + DevolucionFecha + "',  '" + Elaborado + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                    cmd = new SqlCommand("Update ActivoFijo set Estatus= 'BAJA' where (Clave + ' - ' + Descripcion)='" + ActivoFijo + "'", cn);
                    cmd.ExecuteNonQuery();
                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Resguardo de Activo Fijo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string devolucion = string.Empty;

                        if (Devolucion.Checked == true)
                        {
                            devolucion = "Si";
                        }

                        cmd = new SqlCommand("Update ResguardoActivoFijo set  Devolucion= '" + devolucion + "', DevolucionFecha='" + DevolucionFecha + "', Notas= '" + Notas + "', DevolucionRegistrado='" + DevolucionRegistro + "' where Folio= '" + Folio + "'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado.";

                        cmd = new SqlCommand("Update ActivoFijo set Estatus= 'ALTA' where  (Clave + ' - ' + Descripcion)='" + ActivoFijo + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

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
