using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace PV.Clases.OrdenCompra
{
    class DBOrdenCompra
    {
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
            // Solo se valida que la conexión pueda establecerse.
            // Cada método abre y cierra su propia conexión con "using",
            // por lo que ya no se mantiene una conexión abierta a nivel de clase.
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }

        public void CerrarConexion()
        {
            // Se conserva el método para no romper el código que lo invoca.
            // Ya no existe una conexión de clase que cerrar: cada método
            // abre y libera la suya propia mediante "using".
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Descripcion from Condominio", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cb.Items.Add(dr[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________
        public void BuscarProveedor(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select IdProveedor, RazonSocial from Proveedor", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("Select IdProveedor, RazonSocial from Proveedor where RazonSocial like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["IdProveedor"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                    }
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
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Convert(varchar,Clave) + ' - ' + Nombre) as Nombre from CentroCostos", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCosto2(ComboBox cb, string filtro)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Convert(varchar,Clave) + ' - ' + Nombre) as Nombre from CentroCostos where Clave='" + filtro + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionCentroCosto(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Clave from CentroCostos where (Convert(varchar,Clave) + ' - ' + Nombre)= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoDepartamento(ComboBox cb, string Centro)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from CentroCostos_Departamentos where CentroCosto='" + Centro + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarCentroCostoDepartamento2(ComboBox cb, string Clave)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select (clave + ' - ' + Nombre) as Nombre from CentroCostos_Departamentos where Clave='" + Clave + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionCentroCostoDepartamento(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Clave from CentroCostos_Departamentos where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Pedido a Proveedor'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumentoNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where Clase='Nota Cargo'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoDocumentoRequisicion(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Requisicion'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarRegistroGastos(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where /*TipoDocumento='Compra' and Clase='Compra' and*/ Tarea='Compras Gastos'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

        public void SeleccionarRecepcionProductos(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where  Tarea='Compras Gastos Inventariables'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where Clase='Nota Cargo'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarOrdenEntrega(ComboBox cb, string Filtro, string Proveedor)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" + Proveedor + "' and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProvedor(ComboBox cb, string Filtro)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarOrdenEntrega2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProvedor2(ComboBox cb, string Folio)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select distinct P.RazonSocial from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.ClaveProveedor=P.IdProveedor", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor and O.Consecutivo like '%" + consecutivo + "%' and P.RazonSocial like '%" + proveedor + "%' and O.ClaveDocumento like '%" + documento + "%'", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("Delete PartidaRegistroGastos where FolioGasto='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Update NotasGasto set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("Delete ConceptoGlobalesNotasGasto where Folio='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Saldo= 0.00, Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("update ProductosServicios set ProductosServicios.ExActual = ProductosServicios.ExActual - PartidaRecepcion.Cantidad from PartidaRecepcion where ProductosServicios.ClaveProducto=PartidaRecepcion.ClaveProducto and PartidaRecepcion.FolioRecepcion='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("update AlmacenProducto set AlmacenProducto.Entradas = AlmacenProducto.Entradas - PartidaRecepcion.Cantidad from PartidaRecepcion where AlmacenProducto.ClaveProducto=PartidaRecepcion.ClaveProducto and PartidaRecepcion.FolioRecepcion='" + Folio + "' and AlmacenProducto.ClaveAlmacen='" + Almacen + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("Delete PartidaRecepcion where FolioRecepcion='" + Folio + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor ", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.Consecutivo like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.Consecutivo like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor ", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.Folio like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado is null", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenCompra as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor and Autorizado<>''", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.ClaveDocumento like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.Consecutivo like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, DS.Nombre from Requisicion as O, CentroCostos_Departamentos as DS where O.Departamento=DS.Clave and O.Fecha like '%" + Filtro + "%'", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor ", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where P.RazonSocial like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Proveedor as P where O.ClaveDocumento like '%" + Filtro + "%' and O.ClaveProveedor=P.IdProveedor", cn))
                {
                    DataTable dt = new DataTable();
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
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionDocumento(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Nombre, Clave, MostrarCentroCosto from Documento where (Clave + ' - ' + Nombre)= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                            dr[1].ToString(),
                            dr[2].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionOrdenCompra(string Documento, string DocumentoClave)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select OC.* from OrdenCompra as OC, Documento as D, Proveedor as P  where OC.ClaveProveedor=P.IdProveedor and (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) = '" + Documento + "' and OC.ClaveDocumento=D.Clave and (D.Clave + ' - ' + D.Nombre)='" + DocumentoClave + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionProveedor(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Proveedor where IdProveedor= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[1].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________
        public void ActualizarOrdenAuto(string txtFolio, string txtAurizado, string fecha)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update OrdenCompra set Autorizado='Si', UsuarioAutoriza='" + txtAurizado + "', FechaAutoriza='" + fecha + "'  where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    int folioNuevo;
                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from OrdenCompra order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            folioNuevo = Convert.ToInt32(dr["Folio"].ToString()) + 1;
                        }
                        else
                        {
                            folioNuevo = 1;
                        }
                    }

                    txtFolio.Text = Convert.ToString(folioNuevo);

                    using (SqlCommand cmd = new SqlCommand("insert into OrdenCompra (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen) values ('" + folioNuevo + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "','" + almacen + "')", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    int folioNuevo;
                    bool existiaFolio;
                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from Remision order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            folioNuevo = Convert.ToInt32(dr["Folio"].ToString()) + 1;
                            existiaFolio = true;
                        }
                        else
                        {
                            folioNuevo = 1;
                            existiaFolio = false;
                        }
                    }

                    txtFolio.Text = Convert.ToString(folioNuevo);

                    if (existiaFolio)
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into Remision (Folio, ClaveDocumento, Estatus, Fecha, DiasVence, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, FolioOrdenPedidoCliente) values ('" + folioNuevo + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "','" + almacen + "', '" + FolioOrdenPedidoCliente + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into Remision (Folio, ClaveDocumento, Estatus, Fecha, DiasVence, FechaVence, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + almacen + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    int folioNuevo;
                    bool existiaFolio;
                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from Requisicion order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            folioNuevo = Convert.ToInt32(dr["Folio"].ToString()) + 1;
                            existiaFolio = true;
                        }
                        else
                        {
                            folioNuevo = 1;
                            existiaFolio = false;
                        }
                    }

                    txtFolio.Text = folioNuevo.ToString();

                    if (existiaFolio)
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into Requisicion (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, CentroCosto, Departamento, Notas, Elaborado, Consecutivo) values ('" + folioNuevo + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + centroCosto + "', '" + departamento + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into Requisicion (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, CentroCosto, Departamento, Notas, Elaborado, Consecutivo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + centroCosto + "', '" + departamento + "',  '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from Remision where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folioNuevo = Convert.ToInt32(dr["Consecutivo"].ToString()) + 1;
                            txtConsecutivo.Text = folioNuevo.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from RecepcionProducto where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folioNuevo = Convert.ToInt32(dr["Consecutivo"].ToString()) + 1;
                            txtConsecutivo.Text = folioNuevo.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from RegistroGastos where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folioNuevo = Convert.ToInt32(dr["Consecutivo"].ToString()) + 1;
                            txtConsecutivo.Text = folioNuevo.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from NotasGasto where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folioNuevo = Convert.ToInt32(dr["Consecutivo"].ToString()) + 1;
                            txtConsecutivo.Text = folioNuevo.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from Requisicion where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int folioNuevo = Convert.ToInt32(dr["Consecutivo"].ToString()) + 1;
                            txtConsecutivo.Text = folioNuevo.ToString();
                        }
                        else
                        {
                            txtConsecutivo.Text = "1";
                        }
                    }
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
                string orden = string.IsNullOrEmpty(RecepcionProducto) ? "0" : RecepcionProducto;

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    int folioNuevo;
                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from RecepcionProducto order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            folioNuevo = Convert.ToInt32(dr["Folio"].ToString()) + 1;
                        }
                        else
                        {
                            folioNuevo = 1;
                        }
                    }

                    txtFolio.Text = folioNuevo.ToString();

                    using (SqlCommand cmd = new SqlCommand("insert into RecepcionProducto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Almacen, Referencia, Condominio, DiasVence, FechaVence) values ('" + folioNuevo + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Almacen + "', '" + Referencia + "', '" + condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarRegistroGasto(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia, string DiasVence, string FechaVence, string centrocosto, int semana, string anio, string proveedorAlterno)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int nuevoFolio;
                    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) + 1 FROM RegistroGastos", cn))
                    {
                        nuevoFolio = (int)cmd.ExecuteScalar();
                    }

                    txtFolio.Text = nuevoFolio.ToString();

                    string orden = string.IsNullOrEmpty(RecepcionProducto) ? "0" : RecepcionProducto;

                    string query = @"INSERT INTO RegistroGastos 
                        (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, DiasVence, FechaVence, CentroCostos, Semana, Anio, ProveedorAlterno)
                        VALUES 
                        (@Folio, @ClaveDocumento, @Estatus, @Fecha, @ClaveProveedor, @Divisa, @TipoCambio, @Notas, @Elaborado, @FolioOrden, @Consecutivo, @Referencia, @DiasVence, @FechaVence, @CentroCostos, @Semana, @Anio, @ProveedorAlterno)";

                    using (SqlCommand cmdInsert = new SqlCommand(query, cn))
                    {
                        cmdInsert.Parameters.AddWithValue("@Folio", nuevoFolio);
                        cmdInsert.Parameters.AddWithValue("@ClaveDocumento", ClaveDocumento);
                        cmdInsert.Parameters.AddWithValue("@Estatus", Estatus);
                        cmdInsert.Parameters.AddWithValue("@Fecha", Fecha);
                        cmdInsert.Parameters.AddWithValue("@ClaveProveedor", ClavePropietario);
                        cmdInsert.Parameters.AddWithValue("@Divisa", Divisa);
                        cmdInsert.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                        cmdInsert.Parameters.AddWithValue("@Notas", Notas);
                        cmdInsert.Parameters.AddWithValue("@Elaborado", Elaborado);
                        cmdInsert.Parameters.AddWithValue("@FolioOrden", orden);
                        cmdInsert.Parameters.AddWithValue("@Consecutivo", Consecutivo);
                        cmdInsert.Parameters.AddWithValue("@Referencia", Referencia);
                        cmdInsert.Parameters.AddWithValue("@DiasVence", DiasVence);
                        cmdInsert.Parameters.AddWithValue("@FechaVence", FechaVence);
                        cmdInsert.Parameters.AddWithValue(
                            "@CentroCostos",
                            string.IsNullOrWhiteSpace(centrocosto)
                                ? (object)DBNull.Value
                                : centrocosto
                        );
                        cmdInsert.Parameters.AddWithValue("@Semana", semana);
                        cmdInsert.Parameters.AddWithValue("@Anio", anio);
                        cmdInsert.Parameters.AddWithValue("@ProveedorAlterno", proveedorAlterno);

                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        //______________________________________________________________________________________________
        public void InsertarNotaCargo(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string RecepcionProducto, string Consecutivo, string Referencia)
        {
            try
            {
                string orden = string.IsNullOrEmpty(RecepcionProducto) ? "0" : RecepcionProducto;

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    int folioNuevo;
                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from NotasGasto order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            folioNuevo = Convert.ToInt32(dr["Folio"].ToString()) + 1;
                        }
                        else
                        {
                            folioNuevo = 1;
                        }
                    }

                    txtFolio.Text = folioNuevo.ToString();

                    using (SqlCommand cmd = new SqlCommand("insert into NotasGasto (Folio, ClaveDocumento, Estatus, Fecha, ClaveProveedor, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Subtotal, Descuento, Cargo) values ('" + folioNuevo + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '0.00', '0.00', '0.00')", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Descripcion from ProductosServicios", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProducto(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Descripcion from ProductosServicios where Inventariable='Si'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Descripcion from Servicios where Estatus='Activo'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoRecepcion(ComboBox cb, string Orden)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Inventariable='Si'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoGasto(ComboBox cb, string Orden)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select P.Descripcion from PartidaOrden as PA, Servicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and PA.CantidadRecibida>0 and P.Estatus='Activo'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //___________________________________________________________________________________________
        public void Consulta5(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from PartidaOrden where FolioOrden='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void ConsultaRequisicion5(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from PartidaRequisicion where FolioRequisicion='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Correo, Correo2 from Propietarios where IdPropietario='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtcorreo.Text = dr["Correo"].ToString();
                            txtcorreo2.Text = dr["Correo2"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Correo, Correo2 from Proveedor where IdProveedor='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtcorreo.Text = dr["Correo"].ToString();
                            txtcorreo2.Text = dr["Correo2"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from PartidaRecepcion where FolioRecepcion='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from PartidaRegistroGastos where FolioGasto='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select count(*) as Partida from ConceptoGlobalesNotasGasto where Folio='" + Folio + "' and Total<>0.00", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select PG.*,  P.ConceptoGlobales from PartidaRegistroGastos as PG, ProductosServicios as P where PG.FolioGasto='" + Folio + "' and P.ConceptoGlobales='" + Documento + "' and PG.ClaveProducto=P.ClaveProducto order by PG.Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                            total = Convert.ToDecimal(dr["Subtotal"].ToString());
                            porcentaje = Convert.ToDecimal(dr["Impuesto"].ToString());
                            impuesto = impuesto + ((Convert.ToDecimal(porcentaje) / 100) * Convert.ToDecimal(total));
                        }
                    }
                }
                txtImpuesto.Text = impuesto.ToString("N2");
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select PG.*,  P.ConceptoGlobales from PartidaRecepcion as PG, ProductosServicios as P where PG.FolioRecepcion='" + Folio + "' and P.ConceptoGlobales='" + Documento + "' and PG.ClaveProducto=P.ClaveProducto order by PG.Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                            total = Convert.ToDecimal(dr["Subtotal"].ToString());
                            porcentaje = Convert.ToDecimal(dr["Impuesto"].ToString());
                            impuesto = impuesto + ((Convert.ToDecimal(porcentaje) / 100) * Convert.ToDecimal(total));
                        }
                    }
                }
                txtImpuesto.Text = impuesto.ToString("N2");
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
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from ProductosServicios where Descripcion= '" + Recibo + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        public string[] InformacionGasto(string Recibo)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Servicios where Descripcion= '" + Recibo + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        public string[] InformacionGastoo(string Producto, string Orden)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select PA.*, P.Descripcion, P.ClaveProducto, P.TipoCosteo from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and P.Descripcion= '" + Producto + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionRecepcion(string Producto, string Orden)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select PA.*, P.Descripcion, P.ClaveProducto, P.TipoCosteo from PartidaOrden as PA, ProductosServicios as P where PA.ClaveProducto=P.ClaveProducto and PA.FolioOrden='" + Orden + "' and P.Descripcion= '" + Producto + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaRemision(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Precio, decimal Impuesto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into PartidaRemision (FolioRemision, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Subtotal, Descuento, Total, CantidadRecibida, Impuesto) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Precio + "','" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Cantidad + "', '" + Impuesto + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into PartidaRequisicion (FolioRequisicion, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, CantidadRecibida) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Cantidad + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update PartidaOrden set CantidadRecibida= CantidadRecibida - '" + Cantidad + "' where FolioOrden='" + Folio + "' and  Partida='" + Partida + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaRecepcion(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Impuesto, string Archivo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into PartidaRecepcion (FolioRecepcion, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total, Impuesto, Archivo) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Impuesto + "', '" + Archivo + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartidaGasto(
    string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad,
    string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento,
    decimal Total, decimal Impuesto, string archivo, string proveedorAlterno, string centroCostosAlterno, string DescuentoImporte, string ImpuestoImporte)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int count;
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM PartidaRegistroGastos WHERE FolioGasto = @Folio AND Partida = @Partida", cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@Folio", Folio);
                        cmdCheck.Parameters.AddWithValue("@Partida", Partida);
                        count = (int)cmdCheck.ExecuteScalar();
                    }

                    string query;
                    if (count > 0)
                    {
                        // Si existe, actualiza
                        query = @"UPDATE PartidaRegistroGastos SET 
                ClaveProducto = @ClaveRecibo, Concepto2 = @Concepto2, Cantidad = @Cantidad,
                Unidad = @Unidad, Divisa = @Divisa, TipoCambio = @TipoCambio, Subtotal = @Subtotal,
                Descuento = @Descuento, Total = @Total, Impuesto = @Impuesto, Archivo = @Archivo,
                ProveedorAlterno = @ProveedorAlterno, CentroCostosAlterno = @CentroCostosAlterno, DescuentoImporte=@DescuentoImporte, ImpuestoImporte=@ImpuestoImporte
                WHERE FolioGasto = @Folio AND Partida = @Partida";
                    }
                    else
                    {
                        // Si no existe, inserta
                        query = @"INSERT INTO PartidaRegistroGastos 
                (FolioGasto, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, 
                Subtotal, Descuento, Total, Impuesto, Archivo, ProveedorAlterno, CentroCostosAlterno, DescuentoImporte, ImpuestoImporte)
                VALUES 
                (@Folio, @Partida, @ClaveRecibo, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio, 
                @Subtotal, @Descuento, @Total, @Impuesto, @Archivo, @ProveedorAlterno, @CentroCostosAlterno,@DescuentoImporte, @ImpuestoImporte)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@ClaveRecibo", ClaveRecibo);
                        cmd.Parameters.AddWithValue("@Concepto2", Concepto2);
                        cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                        cmd.Parameters.AddWithValue("@Unidad", Unidad);
                        cmd.Parameters.AddWithValue("@Divisa", Divisa);
                        cmd.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                        cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                        cmd.Parameters.AddWithValue("@Descuento", Descuento);
                        cmd.Parameters.AddWithValue("@Total", Total);
                        cmd.Parameters.AddWithValue("@Impuesto", Impuesto);
                        cmd.Parameters.AddWithValue("@Archivo", archivo);
                        cmd.Parameters.AddWithValue("@ProveedorAlterno", string.IsNullOrWhiteSpace(proveedorAlterno) ? DBNull.Value : (object)proveedorAlterno);
                        cmd.Parameters.AddWithValue("@CentroCostosAlterno", string.IsNullOrWhiteSpace(centroCostosAlterno) ? DBNull.Value : (object)centroCostosAlterno);
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        cmd.Parameters.AddWithValue("@DescuentoImporte", DescuentoImporte);
                        cmd.Parameters.AddWithValue("@ImpuestoImporte", ImpuestoImporte);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //___________________________________________________________________________________________
        public void eliminarOrden(string Folio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete OrdenCompra where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete Requisicion where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete RecepcionProducto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete RegistroGastos where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Delete NotasGasto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarTotalesRemision(string folio, string partida)
        {
            try
            {
                const string queryTotales = @"
            SELECT
                ISNULL(SUM(Descuento), 0) AS Descuento,
                ISNULL(SUM(Subtotal), 0) AS Subtotal,
                ISNULL(SUM(Total), 0) AS Total,
                ISNULL(SUM((CONVERT(decimal(18,2), Impuesto) / 100) * (Subtotal - Descuento)), 0) AS Impuesto
            FROM PartidaRemision
            WHERE FolioRemision = @Folio";

                decimal subtotal = 0;
                decimal descuento = 0;
                decimal total = 0;
                decimal impuesto = 0;

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(queryTotales, cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", folio);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                subtotal = Convert.ToDecimal(dr["Subtotal"]);
                                descuento = Convert.ToDecimal(dr["Descuento"]);
                                total = Convert.ToDecimal(dr["Total"]);
                                impuesto = Convert.ToDecimal(dr["Impuesto"]);
                            }
                        }
                    }

                    const string queryUpdate = @"
            UPDATE Remision
            SET
                TotalPartidas = @TotalPartidas,
                Subtotal = @Subtotal,
                Descuento = @Descuento,
                Cargo = @Cargo,
                Total = @Total,
                Saldo = @Saldo
            WHERE Folio = @Folio";

                    using (SqlCommand cmd = new SqlCommand(queryUpdate, cn))
                    {
                        cmd.Parameters.AddWithValue("@TotalPartidas", partida);
                        cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                        cmd.Parameters.AddWithValue("@Descuento", descuento);
                        cmd.Parameters.AddWithValue("@Cargo", impuesto);
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@Saldo", total);
                        cmd.Parameters.AddWithValue("@Folio", folio);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al actualizar los totales de la remisión.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        //____________________________________________________________________________________________
        public void ActualizarRequisicion(string txtFolio, string txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Requisicion set TotalPartidas='" + txtPartida + "' where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Subtotal = null, Descuentos = null, Total = null, Impuesto = null;

                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Subtotal) as Subtotal, sum(Total) as Total from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal = dr["Subtotal"].ToString();
                            Descuentos = dr["Descuento"].ToString();
                            Total = dr["Total"].ToString();
                        }
                    }

                    if (Subtotal != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn))
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Impuesto = dr["Impuesto"].ToString();
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Subtotal = null, Descuentos = null, Total = null, Impuesto = null;

                    using (SqlCommand cmd = new SqlCommand("select sum(DescuentoImporte) as Descuento, sum(ImpuestoImporte) as Impuesto,sum(Subtotal) as Subtotal, sum(Total) as Total from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal = dr["Subtotal"].ToString();
                            Descuentos = dr["Descuento"].ToString();
                            Total = dr["Total"].ToString();
                            Impuesto = dr["Impuesto"].ToString();
                        }
                    }

                    if (Subtotal != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Subtotal = null, Descuentos = null, Cargo = null, Total = null;

                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo, sum(Subtotal) as Subtotal, sum(Total) as Total from ConceptoGlobalesNotasGasto where Folio='" + txtFolio + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal = dr["Subtotal"].ToString();
                            Descuentos = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                            Total = dr["Total"].ToString();
                        }
                    }

                    if (Subtotal != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update NotasGasto set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "',  Cargo='" + Cargo + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Proveedor set Saldo= Saldo - " + Saldo + " where IdProveedor=" + Clave + "", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Proveedor SET Saldo = Saldo + @Saldo WHERE IdProveedor = @Clave",
                    cn))
                {
                    cmd.Parameters.Add("@Saldo", SqlDbType.Decimal).Value = Saldo;
                    cmd.Parameters.Add("@Clave", SqlDbType.Int).Value = Convert.ToInt32(Clave);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov: " + ex.Message);
            }
        }
        public void ValidarDocumentoEPR()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM TipoMovimiento WHERE Documento = 'EPR' AND TipoMovimiento = 'E') " +
                                                     "BEGIN " +
                                                     "    INSERT INTO TipoMovimiento VALUES ('E', 'EPR', 'ENTRADA POR RECEPCIÓN', 'Activo', '0', 'Si', 'Si', '', '') " +
                                                     "END", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from PartidaRecepcion as PR Join RecepcionProducto as R On R.Folio=PR.FolioRecepcion Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioRecepcion=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@Folio", Folio);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
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
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select IdProveedor, RazonSocial from Proveedor where IdProveedor= '" + Matricula + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                            dr[1].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________________________
        public string[] InformacionCondominio(string Matricula)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select ClaveCondominio from Condominio where Descripcion= '" + Matricula + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________________________
        public string[] InformacionCondominio2(string Matricula)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Descripcion  from Condominio where ClaveCondominio= '" + Matricula + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarReciboEstatus(string Folio, string Estatus, string MatriculaAlumno, string FolioMovimiento)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Remision set Estatus='" + Estatus + "', FolioMovimiento='" + FolioMovimiento + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update Requisicion set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update PartidaRecepcion set  Archivo='" + Archivo + "' where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update PartidaRegistroGastos set  Archivo='" + Archivo + "' where FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Archivo='" + Archivo + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update NotasGasto set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecibo3(string Folio, string Estatus)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("Select * from RecepcionProducto where FolioOrden='" + Folio + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador == 0)
                    {
                        contador = 0;
                        using (SqlCommand cmd = new SqlCommand("Select * from RegistroGastos where FolioOrden='" + Folio + "'", cn))
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                contador++;
                            }
                        }

                        if (contador == 0)
                        {
                            using (SqlCommand cmd = new SqlCommand("Update OrdenCompra set Saldo='0.00', Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }

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
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Descuento) as Descuento, sum(Total) as Total from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaRecepcion where FolioRecepcion='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
                SUM(ISNULL(Subtotal, 0)) AS Subtotal, 
                SUM(ISNULL(CAST((Descuento / 100.0) * Subtotal AS decimal(18, 2)), 0)) AS Descuento, 
                SUM(ISNULL(Total, 0)) AS Total, 
                SUM(ISNULL(CAST((Impuesto / 100.0) * (Subtotal - (Subtotal * Descuento / 100.0)) AS decimal(18, 2)), 0)) AS Impuesto
                FROM [PartidaRemision]
                where FolioRemision = '" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaOrden where FolioOrden='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        public void Consulta5RegistroGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from [PartidaRegistroGastos] where FolioGasto='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldosPartidasGasto(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna2TextBox txtImpuesto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(DescuentoImporte) as Descuento,sum(ImpuestoImporte) as Impuesto, sum(Total) as Total from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from PartidaRegistroGastos where FolioGasto='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Impuesto.Text = Convert.ToDecimal(dr["Impuesto"]).ToString("N", formato);
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from Remision where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select TotalPartidas from Requisicion where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select sum((Convert(decimal, Impuesto) / 100) * Subtotal) as Impuesto from PartidaOrden where FolioOrden='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtImpuesto.Text = dr["Impuesto"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from RecepcionProducto where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtSaldo.Text = dr["Saldo"].ToString();
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from RegistroGastos where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtSaldo.Text = dr["Saldo"].ToString();
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from NotasGasto where Folio='" + txtFolio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtSubtoral.Text = dr["Subtotal"].ToString();
                            txtDescuento.Text = dr["Descuento"].ToString();
                            txtRecargo.Text = dr["Cargo"].ToString();
                            txtTotal.Text = dr["Total"].ToString();
                            txtSaldo.Text = dr["Saldo"].ToString();
                            txtTotalPartidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='G' order by Fecha desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtAbono.Text = dr["Pago"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaNotaCargo(string Folio, Guna.UI2.WinForms.Guna2TextBox txtAbono)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='NCG' order by Fecha desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtAbono.Text = dr["Pago"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaAbonoRecepcion(string Folio, TextBox txtAbono)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select top 1 * from Egreso where Folio='" + Folio + "' and Tipo='P' order by Fecha desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtAbono.Text = dr["Pago"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecibo(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from OrdenCompra where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRequisicion(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, TextBox CentroCosto, TextBox Departamento, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from Requisicion where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecepcion(string Folio, TextBox Documento, ComboBox Estatus, Guna2TextBox Fecha, Guna2TextBox Divisa, Guna2TextBox TipoCambio, Guna2TextBox Subtotal, Guna2TextBox Descuentos, Guna2TextBox Cargo, Guna2TextBox Total, Guna2TextBox Partidas, Guna2TextBox Notas, Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna2TextBox txtconsecutivo, TextBox txtAlmacen, Guna2TextBox txtReferencia, Guna2TextBox saldo, Guna2TextBox condominio, Guna2TextBox DiasVence, Guna2TextBox FechaVence, Guna2TextBox Archivo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from RecepcionProducto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaGastos(
     string Folio,
     TextBox Documento,
     ComboBox Estatus,
     Guna.UI2.WinForms.Guna2TextBox Fecha,
     Guna.UI2.WinForms.Guna2TextBox Divisa,
     Guna.UI2.WinForms.Guna2TextBox TipoCambio,
     Guna.UI2.WinForms.Guna2TextBox Subtotal,
     Guna.UI2.WinForms.Guna2TextBox Descuentos,
     Guna.UI2.WinForms.Guna2TextBox Cargo,
     Guna.UI2.WinForms.Guna2TextBox Total,
     Guna.UI2.WinForms.Guna2TextBox Partidas,
     Guna.UI2.WinForms.Guna2TextBox Notas,
     Guna.UI2.WinForms.Guna2TextBox Elaborado,
     TextBox txtFolio,
     TextBox txtReciboCol,
     Guna.UI2.WinForms.Guna2TextBox txtconsecutivo,
     Guna.UI2.WinForms.Guna2TextBox txtReferencia,
     Guna.UI2.WinForms.Guna2TextBox txtSaldo,
     Guna.UI2.WinForms.Guna2TextBox DiasVence,
     Guna.UI2.WinForms.Guna2TextBox FechaVence,
     Guna.UI2.WinForms.Guna2TextBox Archivo,
     ComboBox CentroCosto,
     ComboBox cmbSemana,
     DateTimePicker dtpAnio,
     ComboBox pro, Guna.UI2.WinForms.Guna2TextBox IdProveedor)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM RegistroGastos WHERE Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Documento.Text = dr["ClaveDocumento"].ToString();

                            // ESTATUS
                            if (dr["Estatus"] != DBNull.Value)
                            {
                                Estatus.SelectedValue = dr["Estatus"];
                            }
                            else
                            {
                                Estatus.SelectedIndex = -1;
                            }

                            Fecha.Text = dr["Fecha"] != DBNull.Value
                                ? Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd")
                                : string.Empty;

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
                            DiasVence.Text = dr["DiasVence"].ToString();

                            FechaVence.Text = dr["FechaVence"] != DBNull.Value
                                ? Convert.ToDateTime(dr["FechaVence"]).ToString("yyyy-MM-dd")
                                : string.Empty;

                            Archivo.Text = dr["Archivo"].ToString();
                            IdProveedor.Text = dr["ClaveProveedor"].ToString();

                            // CENTRO DE COSTOS
                            if (dr["CentroCostos"] != DBNull.Value)
                            {
                                CentroCosto.SelectedValue = dr["CentroCostos"];
                            }
                            else
                            {
                                CentroCosto.SelectedIndex = -1;
                            }

                            // SEMANA
                            if (dr["Semana"] != DBNull.Value)
                            {
                                cmbSemana.SelectedValue = dr["Semana"];
                            }
                            else
                            {
                                cmbSemana.SelectedIndex = -1;
                            }

                            // AÑO
                            if (dr["Anio"] != DBNull.Value)
                            {
                                dtpAnio.Value = new DateTime(
                                    Convert.ToInt32(dr["Anio"]),
                                    1,
                                    1);
                            }

                            // PROVEEDOR ALTERNO
                            if (dr["ProveedorAlterno"] != DBNull.Value)
                            {
                                pro.SelectedValue = dr["ProveedorAlterno"];
                            }
                            else
                            {
                                pro.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaNotasGasto(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2TextBox Fecha, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, Guna.UI2.WinForms.Guna2TextBox txtconsecutivo, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtSaldo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select * from NotasGasto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_________________________________________________________________________________________
        public string[] InformacionDocumento2(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select * from Documento where Clave= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[3].ToString(),
                            dr[2].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionDocumento3(string Orden)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(" select OC.ClaveDocumento, D.Nombre from ordenCompra as OC, Documento as D,Proveedor as P where OC.ClaveDocumento=D.Clave and  OC.ClaveProveedor=P.IdProveedor and (convert(varchar,OC.Consecutivo) + ' - ' +  P.RazonSocial)= '" + Orden + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                            dr[1].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionRecepcion2(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave and OC.Folio='" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_______________________________________________________
        public void CargarRecibosPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRemision as PO, ProductosServicios as PS where FolioRemision='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRequisicion as PO, ProductosServicios as PS where FolioRequisicion='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRecepcion as PO, ProductosServicios as PS where FolioRecepcion='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from PartidaRegistroGastos as PO, servicios as PS where PO.FolioGasto='" + Folio + "' and PO.ClaveProducto=PS.ClaveServicio order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["Subtotal"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["Descuento"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["Impuesto"].ToString();
                        dgv.Rows[n].Cells[7].Value = item["Total"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartida(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox subtotal, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox TxtPrecio, Guna.UI2.WinForms.Guna2TextBox txtImpuesto, Guna.UI2.WinForms.Guna2TextBox txtEntregado)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select P.*, C.Descripcion from PartidaOrden as P, ProductosServicios as C where FolioOrden='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaRequisicion(string Folio, string Partida, Guna.UI2.WinForms.Guna2TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox existencia)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select P.*, C.Descripcion, C.ExActual from PartidaRequisicion as P, ProductosServicios as C where FolioRequisicion='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
                            Concepto2.Text = dr["Concepto2"].ToString();
                            cantidad.Text = dr["Cantidad"].ToString();
                            unidad.Text = dr["Unidad"].ToString();
                            existencia.Text = dr["ExActual"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaRecepcion(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna2TextBox Concepto2, Guna2TextBox cantidad, Guna2TextBox unidad, Guna2TextBox divisa, Guna2TextBox tipocambio, Guna2TextBox txtPrecio, Guna2TextBox descuento, Guna2TextBox total, Guna2TextBox txtImpuestos, Guna2TextBox archivo, ComboBox cmbConcepto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select P.*, C.Descripcion from PartidaRecepcion as P, ProductosServicios as C where FolioRecepcion='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
                            Concepto2.Text = dr["Concepto2"].ToString();
                            cantidad.Text = dr["Cantidad"].ToString();
                            unidad.Text = dr["Unidad"].ToString();
                            divisa.Text = dr["Divisa"].ToString();
                            tipocambio.Text = dr["TipoCambio"].ToString();
                            descuento.Text = dr["Descuento"].ToString();
                            total.Text = dr["Total"].ToString();
                            txtPrecio.Text = dr["Subtotal"].ToString();
                            txtImpuestos.Text = dr["Impuesto"].ToString();
                            archivo.Text = dr["Archivo"].ToString();
                            cmbConcepto.Text = dr["Descripcion"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartidaGasto(
     string Folio,
     string Partida,
     TextBox claveconcepto,
     ComboBox Concepto,
     Guna.UI2.WinForms.Guna2TextBox Concepto2,
     Guna.UI2.WinForms.Guna2TextBox cantidad,
     Guna.UI2.WinForms.Guna2TextBox unidad,
     Guna.UI2.WinForms.Guna2TextBox divisa,
     Guna.UI2.WinForms.Guna2TextBox tipocambio,
     Guna.UI2.WinForms.Guna2TextBox txtPrecio,
     Guna.UI2.WinForms.Guna2TextBox descuento,
     Guna.UI2.WinForms.Guna2TextBox total,
     Guna.UI2.WinForms.Guna2TextBox txtImpuestos,
     Guna.UI2.WinForms.Guna2TextBox archivo,
     ComboBox cmbProveedorAlterno,
     ComboBox cmbCentroCostosAlterno,
     Guna2TextBox txtDescuentoIm,
     Guna2TextBox txtImpuestoIm,
     Guna2TextBox txtPrecioC
 )
        {
            try
            {
                string query = @"
            SELECT P.ClaveProducto, P.Concepto2, P.Cantidad, P.Unidad, P.Divisa, 
                   P.TipoCambio, P.Subtotal, P.Descuento, P.Total, P.Impuesto, 
                   P.Archivo, P.ProveedorAlterno, P.CentroCostosAlterno,
                   P.DescuentoImporte, P.ImpuestoImporte, C.Descripcion
            FROM PartidaRegistroGastos AS P
            INNER JOIN Servicios AS C ON P.ClaveProducto = C.ClaveServicio
            WHERE P.FolioGasto = @Folio AND P.Partida = @Partida";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Partida", Partida);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
                            Concepto2.Text = dr["Concepto2"].ToString();
                            cantidad.Text = dr["Cantidad"].ToString();
                            unidad.Text = dr["Unidad"].ToString();
                            divisa.Text = dr["Divisa"].ToString();
                            decimal subtotal = Convert.ToDecimal(dr["Subtotal"]);
                            decimal descuentoUnitario = Convert.ToDecimal(dr["Descuento"]);
                            decimal cantidadValor = 0;

                            decimal.TryParse(cantidad.Text, out cantidadValor);

                            if (cantidadValor > 0)
                            {
                                // Precio unitario + descuento unitario (ya que el subtotal ya descuenta los descuentos globales)
                                decimal precioUnitario = (subtotal / cantidadValor) + descuentoUnitario;
                                txtPrecioC.Text = precioUnitario.ToString("N2");
                            }
                            else
                            {
                                txtPrecioC.Text = "0.00";
                            }
                            tipocambio.Text = dr["TipoCambio"].ToString();
                            txtPrecio.Text = dr["Subtotal"].ToString();
                            descuento.Text = dr["Descuento"].ToString();
                            total.Text = dr["Total"].ToString();
                            txtImpuestos.Text = dr["Impuesto"].ToString();
                            archivo.Text = dr["Archivo"].ToString();

                            // Evita error si no existen los valores en el ComboBox
                            cmbProveedorAlterno.SelectedValue = dr["ProveedorAlterno"].ToString();

                            cmbCentroCostosAlterno.SelectedValue = dr["CentroCostosAlterno"].ToString();

                            txtDescuentoIm.Text = dr["DescuentoImporte"].ToString();
                            txtImpuestoIm.Text = dr["ImpuestoImporte"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la partida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotal(string Folio, TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from RecepcionProducto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Total"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotalGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from RegistroGastos where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Total"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaSubtotalNotaCargo(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Total from NotasGasto where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Total"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoGlobalesRecibo(ComboBox cb)
        {
            cb.BeginUpdate();
            try
            {
                cb.Items.Clear();

                const string query = @"
            SELECT Clave + ' - ' + Nombre AS Concepto
            FROM ConceptosGlobales
            ORDER BY Nombre";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cb.Items.Add(dr["Concepto"].ToString());
                        }
                    }
                }

                if (cb.Items.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            finally
            {
                cb.EndUpdate();
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoGlobalesReciboreembolso(ComboBox cb, string importe)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Clave from ConceptosGlobales where Importe='" + importe + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoGlobalesReciboNotaCargo(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Clave from ConceptosGlobales where Clase='Cargo' or Clase='Impuesto'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionReciboConceptoGlobal(string concepto)
        {
            const string query = @"
        SELECT
            Clave,
            Nombre,
            Clase,
            Tipo,
            Importe,
            IncluyeIva
        FROM ConceptosGlobales
        WHERE Clave + ' - ' + Nombre = @Concepto";

            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cn.Open();
                cmd.Parameters.Add("@Concepto", SqlDbType.VarChar, 110).Value = concepto;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new string[]
                        {
                            dr["Clave"].ToString(),
                            dr["Nombre"].ToString(),
                            dr["Clase"].ToString(),
                            dr["Tipo"].ToString(),
                            dr["Importe"].ToString(),
                            dr["IncluyeIva"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal3(string ClaveConceptoG, string Folio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesGasto (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '0.00', '0.00', '0.00')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalGasto2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total, string clase)
        {
            try
            {
                string query = @"
            MERGE INTO ConceptoGlobalesGasto AS target
            USING (SELECT @ClaveConceptoG AS ClaveConceptoG, @Folio AS Folio) AS source
            ON (target.ClaveConceptoG = source.ClaveConceptoG AND target.Folio = source.Folio)
            WHEN MATCHED THEN 
                UPDATE SET Subtotal = @Subtotal, Descuento=@Descuento, Cargo = @Cargo, Total = @Total
            WHEN NOT MATCHED THEN 
                INSERT (ClaveConceptoG, Folio, Subtotal, Descuento, Cargo, Total)
                VALUES (@ClaveConceptoG, @Folio, @Subtotal, @Descuento, @Cargo, @Total);";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                    if (clase == "Impuesto" || clase == "Cargo")
                    {
                        cmd.Parameters.AddWithValue("@Cargo", DescuentCargo);
                        cmd.Parameters.AddWithValue("@Descuento", "0.00");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Cargo", "0.00");
                        cmd.Parameters.AddWithValue("@Descuento", DescuentCargo);
                    }
                    cmd.Parameters.AddWithValue("@Total", Total);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar o actualizar ConceptoGlobalesGasto", ex);
            }
        }
        public void InsertarReciboConceptoGlobalRemision2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total, string clase, string IncluyeIva)
        {
            try
            {
                string query = @"
            MERGE INTO ConceptoGlobalesRemision AS target
            USING (SELECT @ClaveConceptoG AS ClaveConceptoG, @Folio AS Folio) AS source
            ON (target.ClaveConceptoG = source.ClaveConceptoG AND target.Folio = source.Folio)
            WHEN MATCHED THEN 
                UPDATE SET Subtotal = @Subtotal, Descuento=@Descuento, Cargo = @Cargo, Total = @Total, IncluyeIva=@IncluyeIva
            WHEN NOT MATCHED THEN 
                INSERT (ClaveConceptoG, Folio, Subtotal, Descuento, Cargo, Total, IncluyeIva)
                VALUES (@ClaveConceptoG, @Folio, @Subtotal, @Descuento, @Cargo, @Total, @IncluyeIva);";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                    if (clase == "Impuesto" || clase == "Cargo")
                    {
                        cmd.Parameters.AddWithValue("@Cargo", DescuentCargo);
                        cmd.Parameters.AddWithValue("@Descuento", "0.00");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Cargo", "0.00");
                        cmd.Parameters.AddWithValue("@Descuento", DescuentCargo);
                    }
                    cmd.Parameters.AddWithValue("@Total", Total);
                    cmd.Parameters.AddWithValue("@IncluyeIva", IncluyeIva);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar o actualizar ConceptoGlobalesGasto", ex);
            }
        }
        public void InsertarReciboConceptoGlobalOrdenPedidoCliente2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total, string clase)
        {
            try
            {
                string query = @"
            MERGE INTO ConceptoGlobalesOrdenPedidoCliente AS target
            USING (SELECT @ClaveConceptoG AS ClaveConceptoG, @Folio AS Folio) AS source
            ON (target.ClaveConceptoG = source.ClaveConceptoG AND target.Folio = source.Folio)
            WHEN MATCHED THEN 
                UPDATE SET Subtotal = @Subtotal, Descuento=@Descuento, Cargo = @Cargo, Total = @Total
            WHEN NOT MATCHED THEN 
                INSERT (ClaveConceptoG, Folio, Subtotal, Descuento, Cargo, Total)
                VALUES (@ClaveConceptoG, @Folio, @Subtotal, @Descuento, @Cargo, @Total);";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                    if (clase == "Impuesto" || clase == "Cargo")
                    {
                        cmd.Parameters.AddWithValue("@Cargo", DescuentCargo);
                        cmd.Parameters.AddWithValue("@Descuento", "0.00");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Cargo", "0.00");
                        cmd.Parameters.AddWithValue("@Descuento", DescuentCargo);
                    }
                    cmd.Parameters.AddWithValue("@Total", Total);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar o actualizar ConceptoGlobalesGasto", ex);
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalNotaCargo2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRecepcion where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (Descuento == string.Empty)
                        {
                            Descuento = "0.00";
                        }
                        if (Cargo == string.Empty)
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Descuento= Descuento + '" + Descuento + "', Cargo= Cargo + '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRecepcion where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (Descuento == string.Empty)
                        {
                            Descuento = "0.00";
                        }
                        if (Cargo == string.Empty)
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Descuento= Descuento + '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesGasto where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (string.IsNullOrEmpty(Descuento))
                        {
                            Descuento = "0.00";
                        }
                        if (string.IsNullOrEmpty(Cargo))
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total=Subtotal+'" + Cargo + "'-'" + Descuento + "', Saldo = Subtotal+'" + Cargo + "'-'" + Descuento + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        public void ActualizarReciboConceptoGlobalRemision(int txtFolio, decimal txtTotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRemision where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (string.IsNullOrEmpty(Descuento))
                        {
                            Descuento = "0.00";
                        }
                        if (string.IsNullOrEmpty(Cargo))
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update Remision set Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total=Subtotal+'" + Cargo + "'-'" + Descuento + "', Saldo = Subtotal+'" + Cargo + "'-'" + Descuento + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        public void ActualizarReciboConceptoGlobalGastos(int txtFolio, decimal txtTotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesGasto where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (string.IsNullOrEmpty(Descuento))
                        {
                            Descuento = "0.00";
                        }
                        if (string.IsNullOrEmpty(Cargo))
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total=Subtotal+'" + Cargo + "'-'" + Descuento + "', Saldo = Subtotal+'" + Cargo + "'-'" + Descuento + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesNotasGasto where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (Descuento == string.Empty)
                        {
                            Descuento = "0.00";
                        }
                        if (Cargo == string.Empty)
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update NotasGasto set Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "', TotalPartidas='" + Partida + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update NotasGasto set Subtotal= '" + Subtotal + "' where Folio='" + txtFolio + "' and Subtotal='0.00'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesGasto where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (Descuento == string.Empty)
                        {
                            Descuento = "0.00";
                        }
                        if (Cargo == string.Empty)
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Descuento= Descuento + '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string Descuento = null, Cargo = null;
                    using (SqlCommand cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesNotasGasto where Folio=" + txtFolio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Descuento = dr["Descuento"].ToString();
                            Cargo = dr["Cargo"].ToString();
                        }
                    }

                    if (Descuento != null)
                    {
                        if (Descuento == string.Empty)
                        {
                            Descuento = "0.00";
                        }
                        if (Cargo == string.Empty)
                        {
                            Cargo = "0.00";
                        }

                        using (SqlCommand cmd = new SqlCommand("Update NotasGasto set  Descuento= '" + Descuento + "', Cargo= '" + Cargo + "', Total='" + txtTotal + "', Saldo = '" + txtTotal + "',  TotalPartidas='" + Partida + "' where Folio='" + txtFolio + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Update NotasGasto set Subtotal= '" + subtotal + "' where Folio='" + txtFolio + "' and Subtotal='0.00'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesRecepcion (ClaveConceptoG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalGasto(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total, string clase, string IncluyeIva)
        {
            try
            {
                string query = @"
            MERGE INTO ConceptoGlobalesGasto AS target
            USING (SELECT @ClaveConceptoG AS ClaveConceptoG, @Folio AS Folio) AS source
            ON (target.ClaveConceptoG = source.ClaveConceptoG AND target.Folio = source.Folio)
            WHEN MATCHED THEN 
                UPDATE SET Subtotal = @Subtotal, Descuento=@Descuento, Cargo = @Cargo, Total = @Total, IncluyeIva=@IncluyeIva
            WHEN NOT MATCHED THEN 
                INSERT (ClaveConceptoG, Folio, Subtotal, Descuento, Cargo, Total, IncluyeIva)
                VALUES (@ClaveConceptoG, @Folio, @Subtotal, @Descuento, @Cargo, @Total, @IncluyeIva);";

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                    if (clase == "Impuesto" || clase == "Cargo")
                    {
                        cmd.Parameters.AddWithValue("@Cargo", DescuentCargo);
                        cmd.Parameters.AddWithValue("@Descuento", "0.00");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Cargo", "0.00");
                        cmd.Parameters.AddWithValue("@Descuento", DescuentCargo);
                    }
                    cmd.Parameters.AddWithValue("@Total", Total);
                    cmd.Parameters.AddWithValue("@IncluyeIva", IncluyeIva);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
                // throw new Exception("Error al insertar o actualizar ConceptoGlobalesGasto", ex);
            }
        }
        public void InsertarOActualizarConceptoGlobalFolio(string ClaveConceptoG, string Folio, decimal Importe, string clase)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    string selectQuery = @"
            SELECT Id FROM ConceptosGlobalesFolio
            WHERE FolioGasto = @FolioGasto AND ClaveConceptoG = @ClaveConceptoG;
        ";

                    int? idExistente = null;
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, cn))
                    {
                        selectCmd.Parameters.AddWithValue("@FolioGasto", Folio);
                        selectCmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                        object result = selectCmd.ExecuteScalar();
                        if (result != null)
                            idExistente = Convert.ToInt32(result);
                    }

                    if (idExistente.HasValue)
                    {
                        // Actualizar porque ya existe el mismo concepto para ese folio
                        string updateQuery = @"
                UPDATE ConceptosGlobalesFolio
                SET Importe = @Importe,
                    Clase = @Clase
                WHERE Id = @Id;
            ";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, cn))
                        {
                            updateCmd.Parameters.AddWithValue("@Importe", Importe);
                            updateCmd.Parameters.AddWithValue("@Clase", clase);
                            updateCmd.Parameters.AddWithValue("@Id", idExistente.Value);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insertar nuevo
                        string insertQuery = @"
                INSERT INTO ConceptosGlobalesFolio (ClaveConceptoG, FolioGasto, Importe, Clase)
                VALUES (@ClaveConceptoG, @FolioGasto, @Importe, @Clase);
            ";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, cn))
                        {
                            insertCmd.Parameters.AddWithValue("@ClaveConceptoG", ClaveConceptoG);
                            insertCmd.Parameters.AddWithValue("@FolioGasto", Folio);
                            insertCmd.Parameters.AddWithValue("@Importe", Importe);
                            insertCmd.Parameters.AddWithValue("@Clase", clase);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR al guardar concepto global del folio: " + ex.Message);
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobalNotaCredito(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("insert into ConceptoGlobalesNotasGasto (ClaveConceptoNG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("delete ConceptoGlobalesRecepcion where Folio='" + Folio + "' and Total= '0.00'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void ActualizarPartidaReciboConceptoGlobalGasto(string txtFolio, decimal Porcentaje, string txtClase)
        {
            try
            {
                string query;

                if (txtClase == "Impuesto" || txtClase == "Cargo")
                {
                    query = "Update P set " +
                            "P.Impuesto = (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Cargo End), " +
                            "P.Total = P.Subtotal + (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Cargo End), " +
                            "P.ImpuestoImporte = (CASE WHEN CG.Tipo = 'Porcentaje' THEN (P.Subtotal * (CG.Importe / 100)) ELSE CGG.Cargo END) " +
                            "from PartidaRegistroGastos as P " +
                            "Join ConceptoGlobalesGasto as CGG On P.FolioGasto=CGG.Folio " +
                            "Join ConceptosGlobales as CG On CGG.ClaveConceptoG = CG.Clave " +
                            "where P.FolioGasto = @Folio";
                }
                else
                {
                    query = "Update P set " +
                            "P.Descuento = (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Descuento End), " +
                            "P.Total = P.Subtotal - (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Descuento End), " +
                            "P.DescuentoImporte = (CASE WHEN CG.Tipo = 'Porcentaje' THEN (P.Subtotal * (CG.Importe / 100)) ELSE CGG.Descuento END) " +
                            "from PartidaRegistroGastos as P " +
                            "Join ConceptoGlobalesGasto as CGG On P.FolioGasto=CGG.Folio " +
                            "Join ConceptosGlobales as CG On CGG.ClaveConceptoG = CG.Clave " +
                            "where P.FolioGasto = @Folio";
                }

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Folio", txtFolio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        public void ActualizarPartidaReciboConceptoGlobalRemision(int txtFolio, decimal Porcentaje, string txtClase)
        {
            try
            {
                string query;

                if (txtClase == "Impuesto" || txtClase == "Cargo")
                {
                    query = "Update P set " +
                            "P.Impuesto = (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Cargo End), " +
                            "P.Total = P.Subtotal + (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Cargo End) " +
                            "from PartidaRemision as P " +
                            "Join ConceptoGlobalesRemision as CGG On P.FolioRemision=CGG.Folio " +
                            "Join ConceptosGlobales as CG On CGG.ClaveConceptoG = CG.Clave " +
                            "where P.FolioRemision = @Folio";
                }
                else
                {
                    query = "Update P set " +
                            "P.Descuento = (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Descuento End), " +
                            "P.Total = P.Subtotal - (Case when CG.Tipo = 'Porcentaje' Then (P.Subtotal * (CG.Importe / 100)) Else CGG.Descuento End) " +
                            "from PartidaRemision as P " +
                            "Join ConceptoGlobalesRemision as CGG On P.FolioRemision=CGG.Folio " +
                            "Join ConceptosGlobales as CG On CGG.ClaveConceptoG = CG.Clave " +
                            "where P.FolioRemision = @Folio";
                }

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Folio", txtFolio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR act" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void EliminarReciboConceptoGlobalGastos3(string Folio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("delete ConceptoGlobalesGasto where Folio='" + Folio + "' and Total= '0.00'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void EliminarReciboConceptoGlobalRemision3(string Folio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("delete ConceptoGlobalesRemision where Folio='" + Folio + "' and Total= '0.00'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("delete ConceptoGlobalesNotasGasto where Folio='" + Folio + "' and Total= '0.00'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from ConceptoGlobalesRecepcion where Folio='" + Folio + "'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["ClaveConceptoG"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from ConceptoGlobalesGasto where Folio='" + Folio + "'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["ClaveConceptoG"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select * from ConceptoGlobalesNotasGasto where Folio='" + Folio + "'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["ClaveConceptoNG"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select CGR.*, CG.*, R.Divisa, R.TipoCambio from ConceptoGlobalesRecepcion as CGR, ConceptosGlobales as CG, RecepcionProducto as R where CGR.Folio=R.Folio and CGR.ClaveConceptoG=CG.Clave and CGR.Folio='" + Folio + "' and CGR.ClaveConceptoG='" + Concepto + "' and CGR.Total='" + Total + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaConceptoGasto(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select CGG.*, CG.*, RG.Divisa, RG.TipoCambio from ConceptoGlobalesGasto as CGG, ConceptosGlobales as CG, RegistroGastos as RG where CGG.ClaveConceptoG=CG.Clave and CGG.Folio='" + Folio + "' and CGG.ClaveConceptoG='" + Concepto + "' and CGG.Total='" + Total + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaConceptoNotaCargo(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select CGG.*, CG.*, RG.Divisa, RG.TipoCambio from ConceptoGlobalesNotasGasto as CGG, ConceptosGlobales as CG, NotasGasto as RG where CGG.ClaveConceptoNG=CG.Clave and CGG.Folio='" + Folio + "' and CGG.ClaveConceptoNG='" + Concepto + "' and CGG.Total='" + Total + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________________________--
        // registrar producto 
        public void RegistroProducto(string txtClaveProducto, string txtExActual, string txtAlmacen, string TipoCosteo, decimal precio)
        {
            decimal ExActual = 0;
            decimal Total = 0;
            decimal GranTotal = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from AlmacenProducto where ClaveProducto='" + txtClaveProducto + "' and ClaveAlmacen='" + txtAlmacen + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "', '" + txtExActual + "',  '0', '0','" + txtExActual + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        if (TipoCosteo == "Ultima Compra")
                        {
                            using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (TipoCosteo == "Promedio")
                        {
                            bool hayDatos = false;
                            using (SqlCommand cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' --and ExActual>0 ", cn))
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    hayDatos = true;
                                    GranTotal = (Convert.ToDecimal(dr["Total"])
                                                + Convert.ToDecimal(txtExActual) * precio)
                                                / (Convert.ToDecimal(dr["ExActual"]) + Convert.ToDecimal(txtExActual));
                                }
                            }

                            if (!hayDatos)
                            {
                                ExActual = Convert.ToDecimal(txtExActual);
                                Total = precio;
                                GranTotal = Total;
                            }

                            using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + GranTotal + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'-- and Inventariable='Si'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Update AlmacenProducto set Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaInicial + '" + txtExActual + "' + Entradas - Salidas where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        if (TipoCosteo == "Ultima Compra")
                        {
                            using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (TipoCosteo == "Promedio")
                        {
                            bool hayDatos = false;
                            using (SqlCommand cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' --and ExActual>0 ", cn))
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    hayDatos = true;
                                    GranTotal = (Convert.ToDecimal(dr["Total"])
                                                + Convert.ToDecimal(txtExActual) * precio)
                                                / (Convert.ToDecimal(dr["ExActual"]) + Convert.ToDecimal(txtExActual));
                                }
                            }

                            if (!hayDatos)
                            {
                                ExActual = Convert.ToDecimal(txtExActual);
                                Total = precio;
                                GranTotal = Total;
                            }

                            using (SqlCommand cmd = new SqlCommand("Update ProductosServicios set  CostoUnitario= " + GranTotal + ", ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' --and Inventariable='Si'", cn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
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
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select (convert(varchar, Clave) + ' - ' + Nombre) as Nombre from Almacenes", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select Clave from Almacenes where (convert(varchar, Clave) + ' - ' + Nombre) = '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen2(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select (convert(varchar, Clave) + ' - ' + Nombre) from Almacenes where  Clave= '" + Documento + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[0].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //_________________________________________________________________________________________
        public void eliminarPartidaRequisicion(string Folio, string partida)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("delete PartidaRequisicion where FolioRequisicion='" + Folio + "' and Partida='" + partida + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select Ruta from DatosEmpresa", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    contador = dt.Rows.Count;

                    if (contador > 0)
                    {
                        Ruta = dt.Rows[0][0].ToString();
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Extension='" + Extension + "' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Extension='', Archivo='' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update PartidaRecepcion set Extension='', Archivo='' where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Extension='', Archivo='' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RecepcionProducto where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Extension='', Archivo='' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRegistroGastos where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update PartidaRegistroGastos set Extension='', Archivo='' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRecepcion where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update PartidaRecepcion set Extension='" + Extension + "' where FolioRecepcion=" + Folio + " and Partida='" + Partida + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRegistroGastos where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update PartidaRegistroGastos set Extension='" + Extension + "' where FolioGasto=" + Folio + " and Partida='" + Partida + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RegistroGastos where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RegistroGastos set Extension='" + Extension + "' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from RecepcionProducto where Folio=" + Folio + "", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Update RecepcionProducto set Extension='" + Extension + "' where Folio=" + Folio + "", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select Correo, Servidor, Contraseña from DatosEmpresa", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    contador = dt.Rows.Count;

                    if (contador > 0)
                    {
                        Correo = dt.Rows[0][0].ToString();
                        Servidor = dt.Rows[0][1].ToString();
                        Contraseña = dt.Rows[0][2].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select CGNG.Folio, CG.Nombre, CGNG.Total from ConceptoGlobalesNotasGasto as CGNG, ConceptosGlobales as CG where CGNG.ClaveConceptoNG = CG.Clave and CGNG.Folio='" + Folio + "'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Total"].ToString();
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select top 1 * from [PartidaRemision] where FolioRemision='" + Folio + "' order by Partida Desc", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int Partida = Convert.ToInt32(dr["Partida"].ToString()) + 1;
                            txtPartida.Text = Partida.ToString();
                        }
                        else
                        {
                            txtPartida.Text = "1";
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter(query, cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select PO.*, PS.Descripcion from [PartidaOrden] as PO, ProductosServicios as PS where FolioOrden='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn))
                {
                    DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select P.*, C.Descripcion from [PartidaRemision] as P, ProductosServicios as C where FolioRemision='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            claveconcepto.Text = dr["ClaveProducto"].ToString();
                            Concepto.Text = dr["Descripcion"].ToString();
                            Concepto2.Text = dr["ClaveProducto"].ToString();
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ConsultaRemision(string Folio, TextBox Documento, ComboBox Estatus, Guna2DateTimePicker Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza, ComboBox cmbAlmacen, TextBox txtFolioPedido, Guna.UI2.WinForms.Guna2TextBox txtPedidoCliente, out string cliente)
        {
            cliente = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select O.*, (Cast(A.Clave as varchar)+' - '+A.Nombre) as Alm, (Cast(OC.ClaveDocumento as varchar)+' - '+Cast(OC.Consecutivo as varchar)) as FolioOrdenPedido from Remision as O Left Join Almacenes as A on A.Clave=O.Almacen Left Join OrdenPedidoCliente as OC on OC.Folio=O.FolioOrdenPedidoCliente where O.Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                            txtFolioPedido.Text = dr["FolioOrdenPedidoCliente"].ToString();
                            txtPedidoCliente.Text = dr["FolioOrdenPedido"].ToString();
                            cliente = dr["ClaveProveedor"].ToString();
                            Partidas.Text = dr["TotalPartidas"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public List<List<string>> ObtenerPartidasRemision(string Folio)
        {
            List<List<string>> listam = new List<List<string>>();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from PartidaOrden as PR Join OrdenCompra as R On R.Folio=PR.FolioOrden Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioOrden=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn))
            {
                cn.Open();
                cmd.Parameters.AddWithValue("@Folio", Folio);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return listam;
        }
        public void ActualizarRemision(string Folio, string Estatus)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Update OrdenCompra set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("select (R.Total+isnull(CG.Cargo,0.00)-isnull(CG.Descuento,0.00)) from Remision as R left Join ConceptoGlobalesRemision as CG On CG.Folio=R.Folio where R.Folio=" + Folio + "", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            total = Convert.ToDecimal(dr[0].ToString());
                        }
                    }
                }
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
                return "Error: " + ex.Message;
            }
        }
        public string EliminarPartidaRegistroGasto(string Folio, string Partida)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from PartidaRegistroGastos where FolioGasto=@Folio and Partida=@Partida", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        cmd.Parameters.AddWithValue("@Partida", Partida);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Eliminación exitosa";
                        }
                        else
                        {
                            mensaje = "No se encontró ninguna fila para eliminar";
                        }
                    }
                }

                string queryUpdate = "UPDATE PartidaRegistroGastos SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and FolioGasto=@Folio";

                using (SqlConnection connection = new SqlConnection(ObtenerCn()))
                using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                {
                    command.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                    command.Parameters.AddWithValue("@Folio", Folio);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error." + ex.ToString();
            }
            return mensaje;
        }
        public void ConsultaTotalRegistroGasto(string Folio, Guna.UI2.WinForms.Guna2TextBox Subtotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("Select Subtotal from RegistroReembolso where Folio='" + Folio + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Subtotal.Text = dr["Subtotal"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public string ObtenerTotalPartidaRegistroGasto(string Folio)
        {
            string maximo = "0";
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select max(Partida) as maximo from PartidaRegistroGastos where FolioGasto='" + Folio + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        maximo = dr[0].ToString();
                    }
                }
            }
            return maximo;
        }
        public string[] InformacionOrdenPedidoCliente(string Orden)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            using (SqlCommand cmd = new SqlCommand("select Folio, ClaveDocumento, Fecha, IdCliente, Consecutivo from OrdenPedidoCliente as OC where Folio='" + Orden + "'", cn))
            {
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }

        public void obtenerRFC(string nombre, Guna2TextBox textBox)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("SELECT RFC FROM Proveedor where estatus ='Activo' and RazonSocial='" + nombre + "'", cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            textBox.Text = dr[0].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Error." + ex.ToString());
            }
        }

        public string insertaArchivos(string folioGasto, string partida, string clave, string nombreArchivo, string tipoArchivo, string ContenidoArchivo)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO PartidaRegistroGastoArchivos (FolioGasto, Partida, NombreArchivo, TipoArchivo, ContenidoArchivo) values ('" + folioGasto + "', '" + partida + "', '" + nombreArchivo + "', '" + tipoArchivo + "', '" + ContenidoArchivo + "')", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Archivo Guardado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }

        public void mostrarArchivos(DataGridView dgv, string Folio, string partida)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlDataAdapter da = new SqlDataAdapter("select secuencia, FolioGasto, Partida, ClaveProducto, NombreArchivo, TipoArchivo,ContenidoArchivo from PartidaRegistroGastoArchivos where FolioGasto = '" + Folio + "' and Partida = '" + partida + "'", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();

                        dgv.Rows[n].Cells[0].Value = item["secuencia"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["FolioGasto"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Partida"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["ClaveProducto"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["NombreArchivo"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["TipoArchivo"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["ContenidoArchivo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Registrar datos del aviso
        public string EliminarArchivosGastos(string Folio, string Partida, string Secuencia)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmd = new SqlCommand("select * from PartidaRegistroGastoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("delete PartidaRegistroGastoArchivos where secuencia='" + Secuencia + "' and FolioGasto='" + Folio + "' and Partida='" + Partida + "'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    resultado = "Eliminado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

            return resultado;
        }
    }
}