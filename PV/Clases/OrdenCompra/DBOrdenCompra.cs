using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;


namespace PV.Clases.OrdenCompra
{
    class DBOrdenCompra
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static string MatriculaC = string.Empty;
        public static string Ruta = string.Empty;
        public static string Correo = string.Empty;
        public static string Contraseña = string.Empty;
        public static string Servidor = string.Empty;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBOrdenCompra()
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
        private void CapturarMensajes(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError info in e.Errors)
            {
                MessageBox.Show(info.Message);
            }
        }
        //___________________________________________________________________________________________
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondomini2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("GLOBAL");
            cmd = new SqlCommand("Select Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________
        public void BuscarProveedor(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdProveedor, RazonSocial from Proveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }
        //______________________________________________________________________________________________________________
        public void BuscarAlumnosFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdProveedor, RazonSocial from Proveedor where RazonSocial like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCosto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Convert(varchar,Clave) + ' - ' + Nombre) as Nombre from CentroCostos", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCosto2(ComboBox cb, string filtro)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Convert(varchar,Clave) + ' - ' + Nombre) as Nombre from CentroCostos where Clave='" + filtro + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________
        public string[] InformacionCentroCosto(string Documento)
        {
            cmd = new SqlCommand("Select Clave from CentroCostos where (Convert(varchar,Clave) + ' - ' + Nombre)= '" + Documento + "'", cn);
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
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoDepartamento(ComboBox cb, string Centro)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from CentroCostos_Departamentos where CentroCosto='" + Centro + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoDepartamento2(ComboBox cb, string Clave)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from CentroCostos_Departamentos where Clave='" + Clave + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________
        public string[] InformacionCentroCostoDepartamento(string Documento)
        {
            cmd = new SqlCommand("Select Clave from CentroCostos_Departamentos where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn);
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
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoSubDepartamento(ComboBox cb, string Centro)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from Departamentos_SubDepartamento where Departamento='" + Centro + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoSubDepartamento2(ComboBox cb, string Centro)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from Departamentos_SubDepartamento where Clave='" + Centro + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_________________________________________________________________________________________
        public string[] InformacionCentroCostoSubDepartamento(string Documento)
        {
            cmd = new SqlCommand("Select Clave from Departamentos_SubDepartamento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn);
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
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Pedido a Proveedor'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumentoNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where Clase='Nota Cargo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumentoRequisicion(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Requisicion'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarRecepcionProducto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Compra'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where Clase='Nota Cargo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarOrdenEntrega(ComboBox cb, string Filtro, string Proveedor)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand(" Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" + Proveedor + "' and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProvedor(ComboBox cb, string Filtro)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand(" Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarOrdenEntrega2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProvedor2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibos2(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRrecepcion(DataGridView dgv, string consecutivo, string documento, string proveedor)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and O.Consecutivo like '%"+consecutivo+"%' and P.RazonSocial like '%"+proveedor+"%' and O.ClaveDocumento like '%"+documento+"%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRegistroGasto3(string Folio, string Estatus)
        {
            try
            {
                cmd = new SqlCommand("Update RegistroGastos set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete PartidaRegistroGastos where FolioGasto='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRegistroNotaCargo3(string Folio, string Estatus)
        {
            try
            {
                cmd = new SqlCommand("Update NotasGasto set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete ConceptoGlobalesNotasGasto where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRegistroRecepcion3(string Folio, string Estatus, string Almacen)
        {
            try
            {
                cmd = new SqlCommand("Update RecepcionProducto set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update ProductosServicios set ProductosServicios.ExActual = ProductosServicios.ExActual - PartidaRecepcion.Cantidad from PartidaRecepcion where ProductosServicios.ClaveProducto=PartidaRecepcion.ClaveProducto and PartidaRecepcion.FolioRecepcion='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update AlmacenProducto set AlmacenProducto.Entradas = AlmacenProducto.Entradas - PartidaRecepcion.Cantidad from PartidaRecepcion where AlmacenProducto.ClaveProducto=PartidaRecepcion.ClaveProducto and PartidaRecepcion.FolioRecepcion='" + Folio + "' and AlmacenProducto.ClaveAlmacen='" + Almacen + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete PartidaRecepcion where FolioRecepcion='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarGasto(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }

        //__________________________________________________________________________________________________________-
        public void CargarNotaCargo(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRequisicion(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.Consecutivo like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltro2(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.Consecutivo like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroRecep(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroNotaCargo(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroP(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroP2(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocumento(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocumento2(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocumentoRecepcion(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.ClaveDocumento like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroRecepcion(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.Consecutivo like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroCentroCostoRecepcion(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.Fecha like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroPRecep(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocumentoRecep(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroPGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroPNotaCargo(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocuemtnoGasto(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroDocuemtnoNotaCargo(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionDocumento(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[3].ToString(),
                     dr[2].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionOrdenCompra(string Documento, string DocumentoClave)
        {
            cmd = new SqlCommand("Select OC.* from OrdenCompra as OC, Documento as D, Proveedor as P  where OC.ClaveProveedor=P.IdProveedor and (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) = '" + Documento + "' and OC.ClaveDocumento=D.Clave and (D.Clave + ' - ' + D.Nombre)='" + DocumentoClave + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                     dr[2].ToString(),
                      dr[3].ToString(),
                       dr[6].ToString(),
                        dr[7].ToString(),
                         dr[8].ToString(),
                           dr[9].ToString(),
                             dr[10].ToString(),
                               dr[11].ToString(),
                                 dr[12].ToString(),
                                   dr[13].ToString(),
                                     dr[14].ToString(),
                                       dr[15].ToString(),
                                        dr[18].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionProveedor(string Documento)
        {
            cmd = new SqlCommand("Select * from Proveedor where IdProveedor= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[1].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________
        public void ActualizarOrdenAuto(string txtFolio, string txtAurizado, string fecha)
        {
            try
            {

                cmd = new SqlCommand("Update OrdenCompra set Autorizado='Si', UsuarioAutoriza='" + txtAurizado + "', FechaAutoriza='" + fecha + "'  where Folio='" + txtFolio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarOrden(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string Consecutivo, string almacen, string FolioOrdenPedidoCliente)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from OrdenCompra order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    //txtFolio.Text = Folio.ToString();
                    txtFolio.Text = Convert.ToString(Folio);
                        dr.Close();

                    cmd = new SqlCommand("insert into OrdenCompra (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, FolioOrdenPedidoCliente) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "','"+almacen+"', '"+ FolioOrdenPedidoCliente + "')", cn);
                    cmd.ExecuteNonQuery();
                   
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into OrdenCompra (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '"+almacen+"')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void InsertarRemision(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string Consecutivo, string almacen, string FolioOrdenPedidoCliente)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Remision order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    //txtFolio.Text = Folio.ToString();
                    txtFolio.Text = Convert.ToString(Folio);
                    dr.Close();

                    cmd = new SqlCommand("insert into Remision (Folio, ClaveDocumento, Estatus, Fecha, DiasVence, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, FolioOrdenPedidoCliente) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "','" + almacen + "', '" + FolioOrdenPedidoCliente + "')", cn);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into Remision (Folio, ClaveDocumento, Estatus, Fecha, DiasVence, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + almacen + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarRequisicion(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string centroCosto, string departamento, string Notas, string Elaborado, string Consecutivo)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Requisicion order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    txtFolio.Text = Folio.ToString();
                    dr.Close();

                    cmd = new SqlCommand("insert into Requisicion (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, CentroCosto, Departamento, Notas, Elaborado, Consecutivo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + centroCosto + "', '" + departamento + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into Requisicion (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, CentroCosto, Departamento, Notas, Elaborado, Consecutivo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + centroCosto + "', '" + departamento + "',  '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void ConsecutivoCompra(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Remision where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void ConsecutivoRecepcion(Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from RecepcionProducto where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void ConsecutivoGasto(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from RegistroGastos where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void ConsecutivoNotaCargo(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from NotasGasto where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void ConsecutivoRequision(Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Requisicion where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Consecutivo"].ToString());
                    Folio++;

                    txtConsecutivo.Text = Folio.ToString();
                    dr.Close();
                }
                else
                {
                    txtConsecutivo.Text = "1";
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarRecepcionProducto(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Almacen, string Referencia, string condominio, string DiasVence, string FechaVence)
        {
            try
            {
                string orden = string.Empty;
                cmd = new SqlCommand("Select top 1 * from RecepcionProducto order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (RecepcionProducto == string.Empty)
                {
                    orden = "0";
                }
                else
                {
                    orden = RecepcionProducto;
                }

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    txtFolio.Text = Folio.ToString();
                    dr.Close();

                    cmd = new SqlCommand("insert into RecepcionProducto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Almacen, Referencia, Condominio, DiasVence, FechaVence) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Almacen + "', '" + Referencia + "', '" + condominio + "', '"+DiasVence+"', '"+FechaVence+"')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into RecepcionProducto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Almacen, Referencia, Condominio, DiasVence, FechaVence) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Almacen + "', '" + Referencia + "', '" + condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarRegistroGasto(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia, string Condominio, string DiasVence, string FechaVence)
        {
            try
            {
                string orden = string.Empty;
                cmd = new SqlCommand("Select top 1 * from RegistroGastos order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (RecepcionProducto == string.Empty)
                {
                    orden = "0";
                }
                else
                {
                    orden = RecepcionProducto;
                }

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    txtFolio.Text = Folio.ToString();
                    dr.Close();

                    cmd = new SqlCommand("insert into RegistroGastos (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Condominio, DiasVence, FechaVence) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '" + Condominio + "', '"+DiasVence+"', '"+FechaVence+"')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into RegistroGastos (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Condominio, DiasVence, FechaVence) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '" + Condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarNotaCargo(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia)
        {
            try
            {
                string orden = string.Empty;
                cmd = new SqlCommand("Select top 1 * from NotasGasto order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (RecepcionProducto == string.Empty)
                {
                    orden = "0";
                }
                else
                {
                    orden = RecepcionProducto;
                }

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    txtFolio.Text = Folio.ToString();
                    dr.Close();

                    cmd = new SqlCommand("insert into NotasGasto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Subtotal, Descuento, Cargo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '0.00', '0.00', '0.00')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into NotasGasto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Subtotal, Descuento, Cargo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '0.00', '0.00', '0.00')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProducto2(ComboBox cb)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from ProductosServicios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProducto(ComboBox cb)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from ProductosServicios where Inventariable='Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from ProductosServicios where Inventariable='No'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoRecepcion(ComboBox cb, string Orden)
        {

            cb.Items.Clear();
            //cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "'", cn);
            cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Inventariable='Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb, string Orden)
        {

            cb.Items.Clear();
            //cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "'", cn);
            cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Inventariable='No'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void Consulta5(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from PartidaOrden where FolioOrden='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ConsultaRequisicion5(string Folio,Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from PartidaRequisicion where FolioRequisicion='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void Consultapropicorreo(string Folio, TextBox txtcorreo, TextBox txtcorreo2)
        {
            try
            {

                cmd = new SqlCommand("Select Correo, Correo2 from Propietarios where IdPropietario='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcorreo.Text=dr["Correo"].ToString();
                    txtcorreo2.Text = dr["Correo2"].ToString();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void Consultapropicorreo2(string Folio, TextBox txtcorreo, TextBox txtcorreo2)
        {
            try
            {

                cmd = new SqlCommand("Select Correo, Correo2 from Proveedor where IdProveedor='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcorreo.Text = dr["Correo"].ToString();
                    txtcorreo2.Text = dr["Correo2"].ToString();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ConsultaRecepcion(string Folio, Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ConsultaGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from PartidaRegistroGastos where FolioGasto='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ConsultaNotaCargoPartida(string Folio, TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select count(*) as Partida from ConceptoGlobalesNotasGasto where Folio='" + Folio + "' and Total<>0.00", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public int ConsultaConceptoPartidasGasto(string Folio, string Documento, Guna.UI2.WinForms.Guna2TextBox txtImpuesto)
        {
            int contador = 0;
            decimal total = 0;
            decimal porcentaje = 0;
            decimal impuesto = 0;
            try
            {
                cmd = new SqlCommand("Select PG.*,  P.ConceptoGlobales from PartidaRegistroGastos as PG, ProductosServicios as P where PG.FolioGasto='" + Folio + "' and P.ConceptoGlobales='" + Documento + "' and PG.ClaveProducto=P.ClaveProducto order by PG.Partida Desc", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                    total = Convert.ToDecimal(dr["Subtotal"].ToString());
                    porcentaje = Convert.ToDecimal(dr["Impuesto"].ToString());
                    impuesto = impuesto + ((Convert.ToDecimal(porcentaje) / 100) * Convert.ToDecimal(total));
                }
                txtImpuesto.Text = impuesto.ToString("N2");
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return contador;
        }
        //___________________________________________________________________________________________
        public int ConsultaConceptoPartidasRecepcion(string Folio, string Documento, TextBox txtImpuesto)
        {
            int contador = 0;
            decimal total = 0;
            decimal porcentaje = 0;
            decimal impuesto = 0;
            try
            {
                cmd = new SqlCommand("Select PG.*,  P.ConceptoGlobales from PartidaRecepcion as PG, ProductosServicios as P where PG.FolioRecepcion='" + Folio + "' and P.ConceptoGlobales='" + Documento + "' and PG.ClaveProducto=P.ClaveProducto order by PG.Partida Desc", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                    total = Convert.ToDecimal(dr["Subtotal"].ToString());
                    porcentaje = Convert.ToDecimal(dr["Impuesto"].ToString());
                    impuesto = impuesto + ((Convert.ToDecimal(porcentaje) / 100) * Convert.ToDecimal(total));
                }
                txtImpuesto.Text = impuesto.ToString("N2");
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return contador;
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionRecibo(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ProductosServicios where Descripcion= '" + Recibo + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                    dr[2].ToString(),
                     dr[17].ToString(),
                         dr[5].ToString(),
                          dr[21].ToString(),
                          dr[14].ToString(),
                           dr[16].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionRecepcion(string Producto, string Orden)
        {
            dr.Close();
            cmd = new SqlCommand("select PA.*, P.Descripcion, P.ClaveProducto, P.TipoCosteo from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and P.Descripcion= '" + Producto + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                   dr[15].ToString(),
                    
                     dr[8].ToString(),
                     dr[11].ToString(),
                      dr[9].ToString(),
                       dr[10].ToString(),
                        dr[3].ToString(),
                         dr[4].ToString(),
                          dr[1].ToString(),
                            dr[12].ToString(),
                             dr[13].ToString(),
                             dr[5].ToString(),
                              dr[16].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaRemision(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Precio, decimal Impuesto)
        {
            try
            {
                cmd = new SqlCommand("insert into PartidaRemision (FolioRemision, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Subtotal, Descuento, Total, CantidadRecibida, Impuesto) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Precio + "','" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Cantidad + "', '" + Impuesto + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaRequisicion(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad)
        {
            try
            {
                cmd = new SqlCommand("insert into PartidaRequisicion (FolioRequisicion, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, CantidadRecibida) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Cantidad + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ActualizarPartidaOrden(string Folio, string Partida, string Cantidad)
        {
            try
            {

                cmd = new SqlCommand("Update PartidaOrden set CantidadRecibida= CantidadRecibida - '" + Cantidad + "' where FolioOrden='" + Folio + "' and  Partida='" + Partida + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaRecepcion(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Impuesto, string Archivo)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("insert into PartidaRecepcion (FolioRecepcion, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total, Impuesto, Archivo) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Impuesto + "', '" + Archivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaGasto(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Impuesto, string archivo)
        {
            int contador = 0;
            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroGastos where FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("insert into PartidaRegistroGastos (FolioGasto, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total, Impuesto, Archivo) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Impuesto + "', '" + archivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void eliminarOrden(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Delete OrdenCompra where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void eliminarRequisicion(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Delete Requisicion where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void eliminarRecepcion(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Delete RecepcionProducto where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void eliminarGasto(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Delete RegistroGastos where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void eliminarNotaCargo(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Delete NotasGasto where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarTotalesRemision(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Subtotal) as Subtotal, sum(Total) as Total from PartidaRemision where FolioRemision='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = string.Empty;
                    dr.Close();

                    cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * (Subtotal-Descuento)) as Impuesto from PartidaRemision where FolioRemision='" + txtFolio + "'", cn);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Impuesto = dr["Impuesto"].ToString();

                    }
                    dr.Close();

                    cmd = new SqlCommand("Update Remision set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "', Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarRequisicion(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("Update Requisicion set TotalPartidas='" + txtPartida + "' where Folio='" + txtFolio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRORac" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarRecepcion(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Subtotal) as Subtotal, sum(Total) as Total from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = string.Empty;
                    dr.Close();

                    cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Impuesto = dr["Impuesto"].ToString();

                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RecepcionProducto set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarGasto(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Subtotal) as Subtotal, sum(Total) as Total from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = string.Empty;
                    dr.Close();

                    cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Impuesto = dr["Impuesto"].ToString();

                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RegistroGastos set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarNotaCargo(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo, sum(Subtotal) as Subtotal, sum(Total) as Total from ConceptoGlobalesNotasGasto where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = string.Empty;
                    dr.Close();

                    //cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn);
                    //dr = cmd.ExecuteReader();

                    //if (dr.Read())
                    //{
                    //    Impuesto = dr["Impuesto"].ToString();

                    //}
                    //dr.Close();

                    cmd = new SqlCommand("Update NotasGasto set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Cargo + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarSaldoProveedor(string Clave, decimal Saldo)
        {
            try
            {
                cmd = new SqlCommand("Update Proveedor set Saldo= Saldo - " + Saldo + " where IdProveedor=" + Clave + "", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarSaldoProveedor2(string Clave, decimal Saldo)
        {
            try
            {
                cmd = new SqlCommand("Update Proveedor set Saldo= Saldo + " + Saldo + " where IdProveedor=" + Clave + "", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }
        public void ValidarDocumentoEPR()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM TipoMovimiento WHERE Documento = 'EPR' AND TipoMovimiento = 'E') " +
                                                     "BEGIN " +
                                                     "    INSERT INTO TipoMovimiento VALUES ('E', 'EPR', 'ENTRADA POR RECEPCIÓN', 'Activo', '0', 'Si', 'Si', '', '') " +
                                                     "END", cn);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public List<List<string>> ObtenerPartidas(string Folio)
        {
            List<List<string>> listam = new List<List<string>>();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from PartidaRecepcion as PR Join RecepcionProducto as R On R.Folio=PR.FolioRecepcion Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioRecepcion=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn);

            //cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PO.Partida, PS.UnidadMedida, PR.Total from PartidaRecepcion as PR Join RecepcionProducto as R On R.Folio=PR.FolioRecepcion Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto Left Join OrdenCompra as O On O.Folio=R.FolioOrden Left Join PartidaOrden as PO On O.Folio=PO.FolioOrden where FolioRecepcion=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn);
            cmd.Parameters.AddWithValue("@Folio", Folio);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List<string> lista = new List<string>();
                lista.Add(dr[0].ToString());
                lista.Add(dr[1].ToString());
                lista.Add(dr[2].ToString());
                lista.Add(dr[3].ToString());
                lista.Add(dr[4].ToString());
                lista.Add(dr[5].ToString());
                lista.Add(dr[6].ToString());
                listam.Add(lista);
            }
            dr.Close();
            return listam;
        }
        public string CancelarRecepcion(string folio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand cmd = new SqlCommand("CancelarRecepcion", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetro de entrada
                        cmd.Parameters.AddWithValue("@FolioRecepcion", folio);

                        // Parámetro de salida para el mensaje
                        SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200);
                        mensajeParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(mensajeParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Captura el mensaje de resultado
                        string mensaje = mensajeParam.Value?.ToString();
                        return mensaje ?? "No se recibió ningún mensaje del procedimiento almacenado.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar el error de manera diferente si no quieres usar MessageBox en esta capa
                return "Error: " + ex.Message;
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
        //_________________________________________________________________________________________________________
        public string[] InformacionPropietarioRecibo(string Matricula)
        {
            cmd = new SqlCommand("Select IdProveedor, RazonSocial from Proveedor where IdProveedor= '" + Matricula + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                    dr[1].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________________________
        public string[] InformacionCondominio(string Matricula)
        {
            cmd = new SqlCommand("Select ClaveCondominio from Condominio where Descripcion= '" + Matricula + "'", cn);
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
        //_________________________________________________________________________________________________________
        public string[] InformacionCondominio2(string Matricula)
        {
            cmd = new SqlCommand("Select Descripcion  from Condominio where ClaveCondominio= '" + Matricula + "'", cn);
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
        //_______________________________________________________________________________________________________________
        public void ActualizarReciboEstatus(string Folio, string Estatus, string MatriculaAlumno, string FolioMovimiento)
        {
            try
            {

                cmd = new SqlCommand("Update Remision set Estatus='" + Estatus + "', FolioMovimiento='"+FolioMovimiento+"' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRequisicionEstatus(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {

                cmd = new SqlCommand("Update Requisicion set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {

                cmd = new SqlCommand("Update RecepcionProducto set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion2(string Folio, string Partida, string Archivo)
        {
            try
            {

                cmd = new SqlCommand("Update PartidaRecepcion set  Archivo='" + Archivo + "' where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion2gasto(string Folio, string Partida, string Archivo)
        {
            try
            {

                cmd = new SqlCommand("Update PartidaRegistroGastos set  Archivo='" + Archivo + "' where FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion3gasto(string Folio, string Partida, string Archivo)
        {
            try
            {

                cmd = new SqlCommand("Update RegistroGastos set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecepcion3recepcion(string Folio, string Partida, string Archivo)
        {
            try
            {

                cmd = new SqlCommand("Update RecepcionProducto set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarGasto(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {

                cmd = new SqlCommand("Update RegistroGastos set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarGasto2(string Folio, string Archivo)
        {
            try
            {

                cmd = new SqlCommand("Update RegistroGastos set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarNotaCargo(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {

                cmd = new SqlCommand("Update NotasGasto set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecibo3(string Folio, string Estatus)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("Select * from RecepcionProducto where FolioOrden='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador == 0)
                {
                    contador = 0;
                    cmd = new SqlCommand("Select * from RegistroGastos where FolioOrden='" + Folio + "'", cn);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        contador++;
                    }
                    dr.Close();

                    if (contador == 0)
                    {
                        cmd = new SqlCommand("Update OrdenCompra set Saldo='0.00', Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Orden de Compra Cancelada");
                    }
                    else
                    {
                        MessageBox.Show("La orden de compra esta ligada a un Registro de Gastos");
                    }
                }
                else
                {
                    MessageBox.Show("La orden de compra esta ligada a una Recepcion de Productos");
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasRecepcion(string txtFolio, Guna2TextBox txtSubtoral, Guna2TextBox txtDescuento, Guna2TextBox txtTotal)
        {
            try
            {

                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Descuento) as Descuento, sum(Total) as Total from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasRecepcion2(string txtFolio, Guna2TextBox Impuesto)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            try
            {

                cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasOrden(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtImpuesto)
        {
            try
            {
                cmd = new SqlCommand(@"SELECT 
                SUM(ISNULL(Subtotal, 0)) AS Subtotal, 
                SUM(ISNULL(CAST((Descuento / 100.0) * Subtotal AS decimal(18, 2)), 0)) AS Descuento, 
                SUM(ISNULL(Total, 0)) AS Total, 
                SUM(ISNULL(CAST((Impuesto / 100.0) * (Subtotal - (Subtotal * Descuento / 100.0)) AS decimal(18, 2)), 0)) AS Impuesto
                FROM [PartidaRemision]
                where FolioRemision = '" + txtFolio + "'", cn);

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtImpuesto.Text = dr["Impuesto"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasOrden2(string txtFolio, Guna.UI2.WinForms.Guna2TextBox Impuesto)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            try
            {

                cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaOrden where FolioOrden='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasGasto(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal)
        {
            try
            {

                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Descuento) as Descuento, sum(Total) as Total from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasGasto2(string txtFolio, Guna.UI2.WinForms.Guna2TextBox Impuesto)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            try
            {

                cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldos(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtRecargo, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtTotalPartidas)
        {
            try
            {

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from Remision where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtRecargo.Text = dr["Cargo"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void RequisicionSaldos(string txtFolio, TextBox txtTotalPartidas)
        {
            try
            {

                cmd = new SqlCommand("select TotalPartidas from Requisicion where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosImpuesto(string txtFolio, TextBox txtImpuesto)
        {
            try
            {

                cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaOrden where FolioOrden='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtImpuesto.Text = dr["Impuesto"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosRecepcion(string txtFolio, Guna2TextBox txtSubtoral, Guna2TextBox txtDescuento, Guna2TextBox txtRecargo, Guna2TextBox txtTotal, Guna2TextBox txtTotalPartidas, Guna2TextBox txtSaldo)
        {
            try
            {

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from RecepcionProducto where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtRecargo.Text = dr["Cargo"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosGastos(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtRecargo, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtTotalPartidas, Guna.UI2.WinForms.Guna2TextBox txtSaldo)
        {
            try
            {

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from RegistroGastos where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtRecargo.Text = dr["Cargo"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosNotaCargo(string txtFolio, TextBox txtSubtoral, TextBox txtDescuento, TextBox txtRecargo, TextBox txtTotal, TextBox txtTotalPartidas, TextBox txtSaldo)
        {
            try
            {

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from NotasGasto where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtSubtoral.Text = dr["Subtotal"].ToString();
                    txtDescuento.Text = dr["Descuento"].ToString();
                    txtRecargo.Text = dr["Cargo"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    txtTotalPartidas.Text = dr["TotalPartidas"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaAbonoGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtAbono)
        {
            try
            {
                cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='G' order by Fecha desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAbono.Text = dr["Pago"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaNotaCargo(string Folio, Guna.UI2.WinForms.Guna2TextBox txtAbono)
        {
            try
            {
                cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='NCG' order by Fecha desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAbono.Text = dr["Pago"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaAbonoRecepcion(string Folio, TextBox txtAbono)
        {
            try
            {
                cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='P' order by Fecha desc", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAbono.Text = dr["Pago"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecibo(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza)
        {
            try
            {

                cmd = new SqlCommand("Select * from OrdenCompra where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Dias.Text = dr["DiasVencen"].ToString();
                    FechaVence.Text = Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy-MM-dd");

                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Subtotal.Text = dr["Subtotal"].ToString();
                    Descuentos.Text = dr["Descuento"].ToString();
                    Cargo.Text = dr["Cargo"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();

                    MatriculaC = dr["ClaveProveedor"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtConsecutivo.Text = dr["Consecutivo"].ToString();
                    txtAutoriza.Text = dr["UsuarioAutoriza"].ToString();
                    if (dr["FechaAutoriza"].ToString() != string.Empty)
                    {
                        txtFechaAutoriza.Text = Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy-MM-dd");
                    }

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRequisicion(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, TextBox CentroCosto, TextBox Departamento, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo)
        {
            try
            {

                cmd = new SqlCommand("Select * from Requisicion where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Dias.Text = dr["DiasVencen"].ToString();
                    FechaVence.Text = Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy-MM-dd");
                    CentroCosto.Text = dr["CentroCosto"].ToString();
                    Departamento.Text = dr["Departamento"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtConsecutivo.Text = dr["Consecutivo"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecepcion(string Folio, TextBox Documento, ComboBox Estatus, Guna2TextBox Fecha, Guna2TextBox Divisa, Guna2TextBox TipoCambio, Guna2TextBox Subtotal, Guna2TextBox Descuentos, Guna2TextBox Cargo, Guna2TextBox Total, Guna2TextBox Partidas, Guna2TextBox Notas, Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna2TextBox txtconsecutivo, TextBox txtAlmacen, Guna2TextBox txtReferencia, Guna2TextBox saldo, Guna2TextBox condominio, Guna2TextBox DiasVence, Guna2TextBox FechaVence, Guna2TextBox Archivo)
        {
            try
            {

                cmd = new SqlCommand("Select * from RecepcionProducto where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Subtotal.Text = dr["Subtotal"].ToString();
                    Descuentos.Text = dr["Descuento"].ToString();
                    Cargo.Text = dr["Cargo"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();
                    saldo.Text = dr["Saldo"].ToString();
                    MatriculaC = dr["ClaveProveedor"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtReciboCol.Text = dr["FolioOrden"].ToString();
                    txtconsecutivo.Text = dr["Consecutivo"].ToString();
                    txtAlmacen.Text = dr["Almacen"].ToString();
                    txtReferencia.Text = dr["Referencia"].ToString();
                    condominio.Text = dr["Condominio"].ToString();
                    DiasVence.Text = dr["DiasVence"].ToString();
                    FechaVence.Text = dr["FechaVence"].ToString();
                    Archivo.Text = dr["Archivo"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaGastos(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna.UI2.WinForms.Guna2TextBox txtconsecutivo, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtSaldo, Guna.UI2.WinForms.Guna2TextBox condominio, Guna.UI2.WinForms.Guna2TextBox DiasVence, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Archivo)
        {
            try
            {

                cmd = new SqlCommand("Select * from RegistroGastos where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Subtotal.Text = dr["Subtotal"].ToString();
                    Descuentos.Text = dr["Descuento"].ToString();
                    Cargo.Text = dr["Cargo"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    MatriculaC = dr["ClaveProveedor"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtReciboCol.Text = dr["FolioOrden"].ToString();
                    txtconsecutivo.Text = dr["Consecutivo"].ToString();
                    txtReferencia.Text = dr["Referencia"].ToString();
                    condominio.Text = dr["Condominio"].ToString();
                    DiasVence.Text = dr["DiasVence"].ToString();
                    FechaVence.Text = dr["FechaVence"].ToString();
                    Archivo.Text = dr["Archivo"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaNotasGasto(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna.UI2.WinForms.Guna2TextBox txtconsecutivo, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtSaldo)
        {
            try
            {

                cmd = new SqlCommand("Select * from NotasGasto where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Subtotal.Text = dr["Subtotal"].ToString();
                    Descuentos.Text = dr["Descuento"].ToString();
                    Cargo.Text = dr["Cargo"].ToString();
                    Total.Text = dr["Total"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();
                    txtSaldo.Text = dr["Saldo"].ToString();
                    MatriculaC = dr["ClaveProveedor"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtReciboCol.Text = dr["FolioOrden"].ToString();
                    txtconsecutivo.Text = dr["Consecutivo"].ToString();
                    txtReferencia.Text = dr["Referencia"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionDocumento2(string Documento)
        {
            cmd = new SqlCommand("Select * from Documento where Clave= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[3].ToString(),
                     dr[2].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }

        //_________________________________________________________________________________________
        public string[] InformacionDocumento3(string Orden)
        {
            cmd = new SqlCommand(" select OC.ClaveDocumento, D.Nombre from ordenCompra as OC, Documento as D,Proveedor as P where OC.ClaveDocumento=D.Clave and  OC.ClaveProveedor=P.IdProveedor and (convert(varchar,OC.Consecutivo) + ' - ' +  P.RazonSocial)= '" + Orden + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                     dr[1].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionRecepcion2(string Documento)
        {
            cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave and OC.Folio='" + Documento + "'", cn);
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
        //_______________________________________________________
        public void CargarRecibosPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRemision as PO, ProductosServicios as PS where FolioRemision='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioRemision"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Cantidad"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRequisicionPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRequisicion as PO, ProductosServicios as PS where FolioRequisicion='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioRequisicion"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosPartidasRecepcion(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                //da = new SqlDataAdapter("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida asc", cn);
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRecepcion as PO, ProductosServicios as PS where FolioRecepcion='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioRecepcion"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Cantidad"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosPartidasGasto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                //da = new SqlDataAdapter("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida asc", cn);
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRegistroGastos as PO, ProductosServicios as PS where PO.FolioGasto='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioGasto"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartida(string Folio, string Partida, TextBox claveconcepto,TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox subtotal, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox TxtPrecio, Guna.UI2.WinForms.Guna2TextBox txtImpuesto, Guna.UI2.WinForms.Guna2TextBox txtEntregado)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from PartidaOrden as P, ProductosServicios as C where FolioOrden='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveProducto"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    TxtPrecio.Text = dr["Precio"].ToString();
                    subtotal.Text = dr["Subtotal"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();
                    txtImpuesto.Text = dr["Impuesto"].ToString();
                    txtEntregado.Text = dr["CantidadRecibida"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaRequisicion(string Folio, string Partida, Guna.UI2.WinForms.Guna2TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox existencia)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion, C.ExActual from PartidaRequisicion as P, ProductosServicios as C where FolioRequisicion='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveProducto"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    existencia.Text = dr["ExActual"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaRecepcion(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna2TextBox Concepto2, Guna2TextBox cantidad, Guna2TextBox unidad, Guna2TextBox divisa, Guna2TextBox tipocambio, Guna2TextBox txtPrecio, Guna2TextBox descuento, Guna2TextBox total, Guna2TextBox txtImpuestos, Guna2TextBox archivo, ComboBox cmbConcepto)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from PartidaRecepcion as P, ProductosServicios as C where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveProducto"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    //Precio.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();
                    txtPrecio.Text = dr["Subtotal"].ToString();
                   
                    txtImpuestos.Text = dr["Impuesto"].ToString();
                    archivo.Text = dr["Archivo"].ToString();
                    cmbConcepto.Text= dr["Descripcion"].ToString(); 
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaGasto(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox txtPrecio, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox txtImpuestos, Guna.UI2.WinForms.Guna2TextBox archico
            )
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from PartidaRegistroGastos as P, ProductosServicios as C where P.FolioGasto='" + Folio + "' and P.Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveProducto"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    txtPrecio.Text = dr["Subtotal"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();
                    txtImpuestos.Text = dr["Impuesto"].ToString();
                    archico.Text = dr["Archivo"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotal(string Folio, TextBox Subtotal)
        {
            try
            {
                cmd = new SqlCommand("Select Total from RecepcionProducto where Folio='" + Folio + "'", cn);
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
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotalGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                cmd = new SqlCommand("Select Total from RegistroGastos where Folio='" + Folio + "'", cn);
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
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotalNotaCargo(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                cmd = new SqlCommand("Select Total from NotasGasto where Folio='" + Folio + "'", cn);
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

        //_______________________________________________________________________________________________
        public void SeleccionarConceptoGlobalesRecibo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Clave from ConceptosGlobales", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoGlobalesReciboNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Clave from ConceptosGlobales where Clase='Cargo' or Clase='Impuesto'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionReciboConceptoGlobal(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ConceptosGlobales where (Clave + ' - ' + Nombre)= '" + Recibo + "'", cn);
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
                       dr[6].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal3(string ClaveConceptoG, string Folio)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalGastos3(string ClaveConceptoG, string Folio)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesGasto (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalNotaCargo3(string ClaveConceptoG, string Folio)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalGasto2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesGasto (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalNotaCargo2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobal(int txtFolio, decimal txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRecepcion where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RecepcionProducto set Descuento= Descuento + '" + Descuento + "', Cargo= Cargo + '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobal2(int txtFolio, decimal txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRecepcion where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RecepcionProducto set Descuento= Descuento + '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobalGasto(int txtFolio, decimal txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesGasto where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RegistroGastos set Descuento= Descuento + '" + Descuento + "', Cargo= Cargo + '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobalNotaCArgo(int txtFolio, decimal Subtotal, decimal txtTotal, string Partida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesNotasGasto where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();

                    cmd = new SqlCommand("Update NotasGasto set Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "', TotalPartidas='" + Partida + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update NotasGasto set Subtotal= '" + Subtotal + "' where Folio='" + txtFolio + "' and Subtotal='0.00'", cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobalGasto2(int txtFolio, decimal txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesGasto where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();

                    cmd = new SqlCommand("Update RegistroGastos set Descuento= Descuento + '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarReciboConceptoGlobalNotaCargo2(int txtFolio, decimal subtotal, decimal txtTotal, string Partida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesNotasGasto where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    if (Descuento == string.Empty)
                    {
                        Descuento = "0.00";
                    }
                    if (Cargo == string.Empty)
                    {
                        Cargo = "0.00";
                    }
                    dr.Close();


                    cmd = new SqlCommand("Update NotasGasto set  Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "',  TotalPartidas='" + Partida + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update NotasGasto set Subtotal= '" + subtotal + "' where Folio='" + txtFolio + "' and Subtotal='0.00'", cn);
                    cmd.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalGasto(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesGasto (ClaveConceptoG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalNotaCredito(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void EliminarReciboConceptoGlobal3(string Folio)
        {
            try
            {
                cmd = new SqlCommand("delete ConceptoGlobalesRecepcion where Folio='" + Folio + "' and Total= '0.00'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void EliminarReciboConceptoGlobalGastos3(string Folio)
        {
            try
            {
                cmd = new SqlCommand("delete ConceptoGlobalesGasto where Folio='" + Folio + "' and Total= '0.00'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void EliminarReciboConceptoGlobalNotaCargo3(string Folio)
        {
            try
            {
                cmd = new SqlCommand("delete ConceptoGlobalesNotasGasto where Folio='" + Folio + "' and Total= '0.00'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosConcepto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from ConceptoGlobalesRecepcion where Folio='" + Folio + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveConceptoG"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosConceptoGasto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from ConceptoGlobalesGasto where Folio='" + Folio + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveConceptoG"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosNotaCargo(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from ConceptoGlobalesNotasGasto where Folio='" + Folio + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["ClaveConceptoNG"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaConcepto(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select CGR.*, CG.*, R.Divisa, R.TipoCambio from ConceptoGlobalesRecepcion as CGR, ConceptosGlobales as CG, RecepcionProducto as R where CGR.Folio=R.Folio and CGR.ClaveConceptoG=CG.Clave and CGR.Folio='" + Folio + "' and CGR.ClaveConceptoG='" + Concepto + "' and CGR.Total='" + Total + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtCleveConcepto.Text = dr["ClaveConceptoG"].ToString();
                    txtConcepto.Text = dr["Nombre"].ToString();
                    txtClase.Text = dr["Clase"].ToString();
                    txtTipo.Text = dr["Tipo"].ToString();
                    txtDivisa.Text = dr["Divisa"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    txtSubtotal.Text = dr["Subtotal"].ToString();
                    if (dr["Descuento"].ToString() != string.Empty)
                    {
                        txtDescuento.Text = dr["Descuento"].ToString();
                    }
                    else
                    {
                        txtDescuento.Text = dr["Cargo"].ToString();
                    }

                    txtTotal.Text = dr["Total"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaConceptoGasto(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select CGG.*, CG.*, RG.Divisa, RG.TipoCambio from ConceptoGlobalesGasto as CGG, ConceptosGlobales as CG, RegistroGastos as RG where CGG.ClaveConceptoG=CG.Clave and CGG.Folio='" + Folio + "' and CGG.ClaveConceptoG='" + Concepto + "' and CGG.Total='" + Total + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtCleveConcepto.Text = dr["ClaveConceptoG"].ToString();
                    txtConcepto.Text = dr["Nombre"].ToString();
                    txtClase.Text = dr["Clase"].ToString();
                    txtTipo.Text = dr["Tipo"].ToString();
                    txtDivisa.Text = dr["Divisa"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    txtSubtotal.Text = dr["Subtotal"].ToString();
                    if (dr["Descuento"].ToString() != string.Empty)
                    {
                        txtDescuento.Text = dr["Descuento"].ToString();
                    }
                    else
                    {
                        txtDescuento.Text = dr["Cargo"].ToString();
                    }

                    txtTotal.Text = dr["Total"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaConceptoNotaCargo(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select CGG.*, CG.*, RG.Divisa, RG.TipoCambio from ConceptoGlobalesNotasGasto as CGG, ConceptosGlobales as CG, NotasGasto as RG where CGG.ClaveConceptoNG=CG.Clave and CGG.Folio='" + Folio + "' and CGG.ClaveConceptoNG='" + Concepto + "' and CGG.Total='" + Total + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtCleveConcepto.Text = dr["ClaveConceptoNG"].ToString();
                    txtConcepto.Text = dr["Nombre"].ToString();
                    txtClase.Text = dr["Clase"].ToString();
                    txtTipo.Text = dr["Tipo"].ToString();
                    txtDivisa.Text = dr["Divisa"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    txtSubtotal.Text = dr["Subtotal"].ToString();
                    if (dr["Descuento"].ToString() != string.Empty)
                    {
                        txtDescuento.Text = dr["Descuento"].ToString();
                    }
                    else
                    {
                        txtDescuento.Text = dr["Cargo"].ToString();
                    }

                    txtTotal.Text = dr["Total"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public void RegistroProducto(string txtClaveProducto, string txtExActual, string txtAlmacen, string TipoCosteo, decimal precio)
        {
            int contador = 0;
            decimal ExActual = 0;
            decimal Total = 0;
            decimal GranTotal = 0;

            try
            {
                cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "', '" + txtExActual + "',  '0', '0','" + txtExActual + "')", cn);
                    cmd.ExecuteNonQuery();

                    if (TipoCosteo == "Ultima Compra")
                    {
                        cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else if (TipoCosteo == "Promedio")
                    {
                        cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' --and ExActual>0 ", cn);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            GranTotal = (Convert.ToDecimal(dr["Total"])
                                        + Convert.ToDecimal(txtExActual) * precio)
                                        / (Convert.ToDecimal(dr["ExActual"]) + Convert.ToDecimal(txtExActual));

                            //ExActual = Convert.ToDecimal(dr["ExActual"].ToString()) + Convert.ToDecimal(txtExActual);
                            //Total =  + precio;
                            //GranTotal = Total / ExActual;
                            //57.52/55

                            //codigo de jorge
                            //ExActual = Convert.ToDecimal(dr["ExActual"].ToString()) + Convert.ToDecimal(txtExActual);
                            ////pendiente de cambio
                            //Total = Convert.ToDecimal(dr["Total"].ToString()) + (precio * Convert.ToDecimal(txtExActual));
                            //GranTotal = Total / ExActual;
                        }
                        else
                        {
                            ExActual = Convert.ToDecimal(txtExActual);
                            Total = precio;
                            GranTotal = Total;

                        }
                        dr.Close();

                        cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'-- and Inventariable='Si'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    cmd = new SqlCommand("Update AlmacenProducto set Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaInicial + '" + txtExActual + "' + Entradas - Salidas where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);

                    // cmd = new SqlCommand("Update AlmacenProducto set ExistenciaInicial= ExistenciaActual, Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);
                    cmd.ExecuteNonQuery();

                    if (TipoCosteo == "Ultima Compra")
                    {
                        cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else if (TipoCosteo == "Promedio")
                    {
                        cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' --and ExActual>0 ", cn);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            GranTotal = (Convert.ToDecimal(dr["Total"])
                                        + Convert.ToDecimal(txtExActual) * precio)
                                        / (Convert.ToDecimal(dr["ExActual"]) + Convert.ToDecimal(txtExActual));

                        }
                        else
                        {
                            ExActual = Convert.ToDecimal(txtExActual);
                            Total = precio;
                            GranTotal = Total;
                        }
                        dr.Close();

                        cmd = new SqlCommand("Update ProductosServicios set  CostoUnitario= " + GranTotal + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' --and Inventariable='Si'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (convert(varchar, Clave) + ' - ' + Nombre) as Nombre from Almacenes", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen(string Documento)
        {
            cmd = new SqlCommand("Select Clave from Almacenes where (convert(varchar, Clave) + ' - ' + Nombre) = '" + Documento + "'", cn);
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

        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen2(string Documento)
        {
            cmd = new SqlCommand("Select (convert(varchar, Clave) + ' - ' + Nombre) from Almacenes where  Clave= '" + Documento + "'", cn);
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

        //_________________________________________________________________________________________
        public void eliminarPartidaRequisicion(string Folio,string partida)
        {
            try
            {
                cmd = new SqlCommand("delete PartidaRequisicion where FolioRequisicion='"+Folio+"' and Partida='"+partida+"'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }


        //_________________________________________________________________________________________
        public int ruta()
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select Ruta from DatosEmpresa", cn);
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
                    Ruta = dt.Rows[0][0].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension(string Folio, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RegistroGastos set Extension='" + Extension + "' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension2(string Folio)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RegistroGastos set Extension='', Archivo='' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension3(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update PartidaRecepcion set Extension='', Archivo='' where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }

        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension4(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RegistroGastos set Extension='', Archivo='' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension4Recepci(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RecepcionProducto where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RecepcionProducto set Extension='', Archivo='' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension3gasto(string Folio, string Partida)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroGastos where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update PartidaRegistroGastos set Extension='', Archivo='' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension4(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update PartidaRecepcion set Extension='" + Extension + "' where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension4gasto(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from PartidaRegistroGastos where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update PartidaRegistroGastos set Extension='" + Extension + "' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension5gasto(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RegistroGastos set Extension='" + Extension + "' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________________________------
        //Registrar datos del aviso
        public void ModificarExtension5Recepcion(string Folio, string Partida, string Extension)
        {
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from RecepcionProducto where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador > 0)
                {

                    cmd = new SqlCommand("Update RecepcionProducto set Extension='" + Extension + "' where Folio=" + Folio + "", cn);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
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
        //---------------------------------
        public void CargarConceptosGloblaesNotascargo(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select CGNG.Folio, CG.Nombre, CGNG.Total from ConceptoGlobalesNotasGasto as CGNG, ConceptosGlobales as CG where CGNG.ClaveConceptoNG = CG.Clave and CGNG.Folio='" + Folio + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Consulta5OrdenCompra(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from [PartidaRemision] where FolioRemision='" + Folio + "' order by Partida Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Partida = Convert.ToInt32(dr["Partida"].ToString());
                    Partida++;

                    txtPartida.Text = Partida.ToString();
                    dr.Close();
                }
                else
                {
                    txtPartida.Text = "1";
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void CargarRemisiones(DataGridView dgv, string tipo, string consecutivo, string documento, string proveedor, bool autorizado)
        {
            try
            {
                string query = "select O.*, P.RazonSocial from Remision as O, Clientes as P where O.ClaveProveedor=P.IdCliente and Autorizado is null and O.Consecutivo like '%" + consecutivo + "%' and O.ClaveDocumento like '%" + documento + "%' and P.RazonSocial like '%" + proveedor + "%'";
                if (autorizado)
                {
                    query += " and (Autorizado<>'' and autorizado is not null)";
                }
                
                dgv.Rows.Clear();
                da = new SqlDataAdapter(query, cn);
                dt = new DataTable();
                da.Fill(dt);
                
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        public void CargarRemisionPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from [PartidaOrden] as PO, ProductosServicios as PS where FolioOrden='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioOrden"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ConsultaPartidaOrden(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox subtotal, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox TxtPrecio, Guna.UI2.WinForms.Guna2TextBox txtImpuesto, Guna.UI2.WinForms.Guna2TextBox txtEntregado, ComboBox cmbConcepto2)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from [PartidaRemision] as P, ProductosServicios as C where FolioRemision='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveProducto"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    TxtPrecio.Text = dr["Precio"].ToString();
                    subtotal.Text = dr["Subtotal"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();
                    txtImpuesto.Text = dr["Impuesto"].ToString();
                    txtEntregado.Text = dr["CantidadRecibida"].ToString();
                    cmbConcepto2.Text = dr["Descripcion"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        public void ConsultaRemision(string Folio, TextBox Documento, ComboBox Estatus, Guna2DateTimePicker Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza, ComboBox cmbAlmacen, TextBox txtFolioPedido, Guna.UI2.WinForms.Guna2TextBox txtPedidoCliente, out string cliente)
        {
            cliente = string.Empty;
            try
            {

                cmd = new SqlCommand("Select O.*, (Cast(A.Clave as varchar)+' - '+A.Nombre) as Alm, (Cast(OC.ClaveDocumento as varchar)+' - '+Cast(OC.Consecutivo as varchar)) as FolioOrdenPedido from Remision as O Left Join Almacenes as A on A.Clave=O.Almacen Left Join OrdenPedidoCliente as OC on OC.Folio=O.FolioOrdenPedidoCliente where O.Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Documento.Text = dr["ClaveDocumento"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                    Dias.Text = dr["DiasVence"].ToString();
                    FechaVence.Text = Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy-MM-dd");

                    Divisa.Text = dr["Divisa"].ToString();
                    TipoCambio.Text = dr["TipoCambio"].ToString();
                    Subtotal.Text = dr["Subtotal"].ToString();
                    Descuentos.Text = dr["Descuento"].ToString();
                    Cargo.Text = dr["Cargo"].ToString();
                    Total.Text = dr["Total"].ToString();
                    
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();

                    MatriculaC = dr["ClaveProveedor"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtConsecutivo.Text = dr["Consecutivo"].ToString();
                    txtAutoriza.Text = dr["UsuarioAutoriza"].ToString();
                    if (dr["FechaAutoriza"].ToString() != string.Empty)
                    {
                        txtFechaAutoriza.Text = Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy-MM-dd");
                    }
                    cmbAlmacen.Text = dr["Alm"].ToString();
                    txtFolioPedido.Text= dr["FolioOrdenPedidoCliente"].ToString();
                    txtPedidoCliente.Text = dr["FolioOrdenPedido"].ToString();
                    cliente = dr["ClaveProveedor"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dr.Close();
            }
        }
        public List<List<string>> ObtenerPartidasRemision(string Folio)
        {
            List<List<string>> listam = new List<List<string>>();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from PartidaOrden as PR Join OrdenCompra as R On R.Folio=PR.FolioOrden Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioOrden=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn);

            //cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PO.Partida, PS.UnidadMedida, PR.Total from PartidaRecepcion as PR Join RecepcionProducto as R On R.Folio=PR.FolioRecepcion Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto Left Join OrdenCompra as O On O.Folio=R.FolioOrden Left Join PartidaOrden as PO On O.Folio=PO.FolioOrden where FolioRecepcion=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn);
            cmd.Parameters.AddWithValue("@Folio", Folio);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List<string> lista = new List<string>();
                lista.Add(dr[0].ToString());
                lista.Add(dr[1].ToString());
                lista.Add(dr[2].ToString());
                lista.Add(dr[3].ToString());
                lista.Add(dr[4].ToString());
                lista.Add(dr[5].ToString());
                lista.Add(dr[6].ToString());
                listam.Add(lista);
            }
            dr.Close();
            return listam;
        }
        public void ActualizarRemision(string Folio, string Estatus)
        {
            try
            {

                cmd = new SqlCommand("Update OrdenCompra set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void CancelarOrdenPedidoCliente(string Folio)
        {

            try
            {


                using (SqlConnection connection = new SqlConnection(ObtenerCn()))
                {
                    try
                    {
                        connection.Open();

                        // Evento para capturar mensajes PRINT
                        connection.InfoMessage += new SqlInfoMessageEventHandler(CapturarMensajes);

                        using (SqlCommand command = new SqlCommand("CancelarOrdenPedidoCliente", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@FolioOrdenPedido", Folio));


                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }



        }

        public decimal ObtenerTotalRemision(string Folio)
        {
            decimal total = 0.00m;

            try
            {
                cmd = new SqlCommand("select Total from Remision where Folio=" + Folio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    total = Convert.ToDecimal(dr[0].ToString());
                }
                dr.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return total;
        }

        public string CancelarRegistroGasto(string folio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand cmd = new SqlCommand("CancelarRegistroGasto", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetro de entrada
                        cmd.Parameters.AddWithValue("@FolioRegistro", folio);

                        // Parámetro de salida para el mensaje
                        SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 200);
                        mensajeParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(mensajeParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Captura el mensaje de resultado
                        string mensaje = mensajeParam.Value?.ToString();
                        return mensaje ?? "No se recibió ningún mensaje del procedimiento almacenado.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar el error de manera diferente si no quieres usar MessageBox en esta capa
                return "Error: " + ex.Message;
            }

        }
        public string[] InformacionOrdenPedidoCliente(string Orden)
        {
            cmd = new SqlCommand("select Folio, ClaveDocumento, Fecha, IdCliente, Consecutivo from OrdenPedidoCliente as OC where Folio='" + Orden + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr["Folio"].ToString(),
                     dr["ClaveDocumento"].ToString(),
                     dr["Fecha"].ToString(),
                     dr["IdCliente"].ToString(),
                     dr["Consecutivo"].ToString(),


                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        
    }


}
