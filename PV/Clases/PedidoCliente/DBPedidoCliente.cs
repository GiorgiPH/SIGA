using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;

namespace PV.Clases.PedidoCliente
{
    internal class DBPedidoCliente
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

        public DBPedidoCliente()
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
       
        public void BuscarClienteFiltro(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes where RazonSocial like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
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
                da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
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
                da = new SqlDataAdapter("Select IdCliente, RazonSocial from Clientes where RazonSocial like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdCliente"].ToString();
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
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Compra' and Clase='Pedido a Clientes'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        public void SeleccionarConceptoDocumentoV(ComboBox cb, string tipo)
        {
            cb.Items.Clear();
            if (tipo == "Remision")
            {
                cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Venta' and Clase='Remision'", cn);

            }
            else if (tipo == "Pedido")
            {
                cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Venta' and Clase='Pedido a Clientes'", cn);

            }
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
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenPedidoCliente as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand(" Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenPedidoCliente as OC, Documento as D, Clientes as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" +Proveedor+ "' and OC.ClaveDocumento='" + Filtro + "' and OC.IdCliente=P.IdCliente and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);
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
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenPedidoCliente as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand(" Select distinct P.RazonSocial from OrdenPedidoCliente as OC, Documento as D,Clientes as P where OC.ClaveDocumento=D.Clave and OC.ClaveDocumento='" + Filtro + "' and OC.IdCliente=P.IdCliente and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);
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
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenPedidoCliente as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenPedidoCliente as OC, Documento as D, Clientes as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.IdCliente=P.IdCliente", cn);
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
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenPedidoCliente as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select distinct P.RazonSocial from OrdenPedidoCliente as OC, Documento as D, Clientes as P where OC.ClaveDocumento=D.Clave and OC.Folio='" + Folio + "' and OC.IdCliente=P.IdCliente", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibos(DataGridView dgv, string tipo, string consecutivo, string documento, string proveedor)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.IdCliente=P.IdCliente and Autorizado is null and tipo like '%"+tipo+"%' and O.Consecutivo like '%"+consecutivo+"%' and O.ClaveDocumento like '%" + documento + "%' and P.RazonSocial like '%" + proveedor + "%'", cn);
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
        public void CargarRecibos2(DataGridView dgv, string tipo, string consecutivo, string documento, string proveedor)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, CLientes as P where O.IdCliente=P.IdCliente and Autorizado<>'' and tipo like '%"+tipo+"%' and O.Consecutivo like '%"+consecutivo+"%' and O.ClaveDocumento like '%" + documento + "%' and P.RazonSocial like '%" + proveedor + "%'", cn);
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
        public void CargarRrecepcion(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Clientes as P where O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Clientes as P where O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Clientes as P where O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.Consecutivo like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado is null", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.Consecutivo like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado<>''", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Clientes as P where O.Folio like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Clientes as P where O.Folio like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Clientes as P where O.Folio like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where P.RazonSocial like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado is null", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where P.RazonSocial like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado<>''", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.ClaveDocumento like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado is null", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from OrdenPedidoCliente as O, Clientes as P where O.ClaveDocumento like '%" + Filtro + "%' and O.IdCliente=P.IdCliente and Autorizado<>''", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Clientes as P where P.RazonSocial like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RecepcionProducto as O, Clientes as P where O.ClaveDocumento like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Clientes as P where P.RazonSocial like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Clientes as P where P.RazonSocial like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from RegistroGastos as O, Clientes as P where O.ClaveDocumento like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
                da = new SqlDataAdapter("select O.*, P.RazonSocial from NotasGasto as O, Clientes as P where O.ClaveDocumento like '%" + Filtro + "%' and O.IdCliente=P.IdCliente", cn);
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
        public string[] InformacionOrdenPedidoCliente(string Documento, string DocumentoClave)
        {
            cmd = new SqlCommand("Select OC.* from OrdenPedidoCliente as OC, Documento as D, Clientes as P  where OC.IdCliente=P.IdCliente and (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) = '" + Documento + "' and OC.ClaveDocumento=D.Clave and (D.Clave + ' - ' + D.Nombre)='" + DocumentoClave + "'", cn);
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
            cmd = new SqlCommand("Select * fromClienteswhere IdCliente= '" + Documento + "'", cn);
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

                cmd = new SqlCommand("Update OrdenPedidoCliente set Autorizado='Si', UsuarioAutoriza='" + txtAurizado + "', FechaAutoriza='" + fecha + "'  where Folio='" + txtFolio + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________
        public void InsertarOrden(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Notas, string Elaborado, string Consecutivo, string Almacen, string Tipo)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from OrdenPedidoCliente order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    //txtFolio.Text = Folio.ToString();
                    txtFolio.Text = Convert.ToString(Folio);
                    dr.Close();

                    cmd = new SqlCommand("insert into OrdenPedidoCliente (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, IdCliente, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, Tipo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + Almacen + "', '" + Tipo + "')", cn);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into OrdenPedidoCliente (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, IdCliente, Divisa, TipoCambio, Notas, Elaborado, Consecutivo, almacen, Tipo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + Consecutivo + "', '" + Almacen + "', '" + Tipo + "')", cn);
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

                cmd = new SqlCommand("Select top 1 * from OrdenPedidoCliente where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
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
        public void ConsecutivoRecepcion(TextBox txtConsecutivo, string ClaveDocumento)
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

                    cmd = new SqlCommand("insert into RecepcionProducto (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Almacen, Referencia, Condominio, DiasVence, FechaVence) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Almacen + "', '" + Referencia + "', '" + condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into RecepcionProducto (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Almacen, Referencia, Condominio, DiasVence, FechaVence) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Almacen + "', '" + Referencia + "', '" + condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
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

                    cmd = new SqlCommand("insert into RegistroGastos (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Condominio, DiasVence, FechaVence) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '" + Condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into RegistroGastos (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Condominio, DiasVence, FechaVence) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '" + Condominio + "', '" + DiasVence + "', '" + FechaVence + "')", cn);
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

                    cmd = new SqlCommand("insert into NotasGasto (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Subtotal, Descuento, Cargo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '0.00', '0.00', '0.00')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into NotasGasto (Folio, ClaveDocumento, Estatus, Fecha, IdCliente, Divisa, TipoCambio, Notas, Elaborado, FolioOrden, Consecutivo, Referencia, Subtotal, Descuento, Cargo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Notas + "', '" + Elaborado + "', '" + orden + "', '" + Consecutivo + "', '" + Referencia + "', '0.00', '0.00', '0.00')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
       
        //_______________________________________________________________________________________________
        public void SeleccionarProducto2(ComboBox cb, string Folio)
        {

            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from ProductosServicios where ClaveProducto not in (Select ClaveProducto from PartidaOrdenPedidoCliente where FolioOrden='" + Folio+"') and inventariable='Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();

        }
        //_______________________________________________________________________________________________
        public void SeleccionarProductoOrdenPedido(ComboBox cb, string Folio)
        {

            cb.Items.Clear();
            cmd = new SqlCommand(" Select P.Descripcion from ProductosServicios as P Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto=P.ClaveProducto Join OrdenPedidoCliente as O on PO.FolioOrden=O.Folio where PO.FolioOrden='"+Folio+"' and PO.CantidadPendiente>0", cn);
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
        public void Consulta5OrdenCliente(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from [PartidaOrdenPedidoCliente] where FolioOrden='" + Folio + "' order by Partida Desc", cn);
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
        public void ConsultaRequisicion5(string Folio, Guna.UI2.WinForms.Guna2TextBox txtPartida)
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
        public void Consultapropicorreo2(string Folio, TextBox txtcorreo, TextBox txtcorreo2)
        {
            try
            {

                cmd = new SqlCommand("Select Correo, Correo2 fromClienteswhere IdCliente='" + Folio + "'", cn);
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
        public void ConsultaRecepcion(string Folio, TextBox txtPartida)
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
        public string[] InformacionRecibo(string Recibo, string folio)
        {
            dr.Close();
            string query = @"
            Select top 1 P.*, PO.CantidadEntregada, PO.CantidadPendiente from ProductosServicios as P
            Left Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto=P.ClaveProducto
            where P.Descripcion= '" + Recibo + "'";
            if (!string.IsNullOrEmpty(folio))
            {
                query += " and PO.FolioOrden='" + folio + "'";
            }
            cmd = new SqlCommand(query, cn);
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
                          dr["ExActual"].ToString(),
                           dr[16].ToString(),
                           dr["PedidosProveedor"].ToString(),
                          dr["PedidosCliente"].ToString(),
                          dr["CantidadEntregada"].ToString(),
                          dr["CantidadPendiente"].ToString(),
                          dr["PrecioVenta"].ToString(),
                          dr["DescuentoPorc"].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        public string[] InformacionPartidaOrden(string Recibo, string folio)
        {
            dr.Close();
            string query = @"
            Select top 1 P.ClaveProducto,Po.Unidad,P.ExActual, P.PedidosProveedor, P.PedidosCliente, P.PrecioVenta, PO.Cantidad, PO.sUBTOTAL, PO.Descuento, PO.Impuesto, PO.Total, PO.Precio,PO.CantidadEntregada, PO.CantidadPendiente from ProductosServicios as P
            Left Join PartidaOrdenPedidoCliente as PO on PO.ClaveProducto=P.ClaveProducto
            where P.Descripcion= '" + Recibo + "'";
            if (!string.IsNullOrEmpty(folio))
            {
                query += " and PO.FolioOrden='" + folio + "'";
            }
            cmd = new SqlCommand(query, cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr["CLaveProducto"].ToString(),
                     dr["Cantidad"].ToString(),
                    dr["sUBTOTAL"].ToString(),
                     dr["Unidad"].ToString(),
                     dr["Descuento"].ToString(),
                         dr["Impuesto"].ToString(),
                          dr["Total"].ToString(),
                          dr["ExActual"].ToString(),
                           dr["PedidosProveedor"].ToString(),
                          dr["PedidosCliente"].ToString(),
                          dr["CantidadEntregada"].ToString(),
                          dr["CantidadPendiente"].ToString(),
                          dr["Precio"].ToString(),
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
                    dr[11].ToString(),
                     dr[8].ToString(),
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
        public void InsertarPartida(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Precio, decimal Impuesto)
        {
            try
            {
                cmd = new SqlCommand("insert into PartidaOrden (FolioOrden, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Subtotal, Descuento, Total, CantidadRecibida, Impuesto) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Precio + "','" + Subtotal + "', '" + Descuento + "', '" + Total + "', '" + Cantidad + "', '" + Impuesto + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void BuscarOrdenPedidoPendiente(DataGridView dgv, string documento, string consecutivo, string cliente)
        {
            //cmd = new SqlCommand(" Select (convert(varchar, OC.Consecutivo)+ ' - ' + P.RazonSocial ) as Nombre from OrdenCompra as OC, Documento as D, Proveedor as P where OC.ClaveDocumento=D.Clave and P.RazonSocial='" + Proveedor + "' and OC.ClaveDocumento='" + Filtro + "' and OC.ClaveProveedor=P.IdProveedor and OC.Autorizado='Si' and (select Count(*) from PartidaOrden where CantidadRecibida>0 and FolioORden=OC.Folio)>0", cn);

            dgv.Rows.Clear();
            da = new SqlDataAdapter(@" Select OC.Folio, Oc.Consecutivo, OC.ClaveDocumento
 from OrdenPedidoCliente as OC, Documento as D 
 where OC.ClaveDocumento=D.Clave and OC.ClaveDocumento like '%" + documento + "%' and OC.Estatus in ('Bloqueado') and OC.Consecutivo like '%" + consecutivo + "%' and Cast(OC.iDCLiente as varchar) like '%"+cliente+"%' and (select Count(*) from PartidaOrdenPedidoCliente where CantidadPendiente>0 and FolioOrden=OC.Folio)>0", cn);
            dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                dgv.Rows[n].Cells[1].Value = item["ClaveDocumento"].ToString();
                dgv.Rows[n].Cells[2].Value = item["Consecutivo"].ToString();
                //dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
            }


        }
        public void ActualizaCantidadPendientePartidaOrden(string Cantidad, string CantidadEntre, string Folio, string ClaveP)
        {
            try
            {
                cmd = new SqlCommand("update PartidaOrdenPedidoCliente set CantidadPendiente=CantidadPendiente-'" + Cantidad + "', CantidadEntregada=CantidadEntregada+'" + CantidadEntre + "' where FolioOrden='" + Folio + "' and ClaveProducto='" + ClaveP + "'", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        public string InsertarPartidaOrdenCliente(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total, decimal Precio, decimal Impuesto)
        {
            string mensaje = string.Empty;
            try
            {
                // Consulta para verificar si la partida ya existe
                string queryExiste = "SELECT COUNT(*) FROM [PartidaOrdenPedidoCliente] WHERE FolioOrden = @Folio AND Partida = @Partida";
                cmd = new SqlCommand(queryExiste, cn);
                cmd.Parameters.AddWithValue("@Folio", Folio);
                cmd.Parameters.AddWithValue("@Partida", Partida);

                int existe = Convert.ToInt32(cmd.ExecuteScalar());

                if (existe > 0) // Si el registro existe, actualizamos
                {
                    string queryActualizar = @"
                UPDATE [PartidaOrdenPedidoCliente]
                SET 
                    ClaveProducto = @ClaveRecibo,
                    Concepto2 = @Concepto2,
                    Cantidad = @Cantidad,
                    Unidad = @Unidad,
                    Divisa = @Divisa,
                    TipoCambio = @TipoCambio,
                    Precio = @Precio,
                    Subtotal = @Subtotal,
                    Descuento = @Descuento,
                    Total = @Total,
                    CantidadRecibida = @Cantidad,
                    Impuesto = @Impuesto,
                    CantidadPendiente = @Cantidad
                WHERE FolioOrden = @Folio AND Partida = @Partida";

                    cmd = new SqlCommand(queryActualizar, cn);
                    mensaje= "Registro actualizado correctamente.";
                }
                else // Si no existe, insertamos
                {
                    string queryInsertar = @"
                INSERT INTO [PartidaOrdenPedidoCliente] 
                (FolioOrden, Partida, ClaveProducto, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Precio, Subtotal, Descuento, Total, CantidadRecibida, Impuesto, CantidadPendiente)
                VALUES 
                (@Folio, @Partida, @ClaveRecibo, @Concepto2, @Cantidad, @Unidad, @Divisa, @TipoCambio, @Precio, @Subtotal, @Descuento, @Total, @Cantidad, @Impuesto, @Cantidad)";

                    cmd = new SqlCommand(queryInsertar, cn);
                }

                // Parámetros compartidos
                cmd.Parameters.AddWithValue("@Folio", Folio);
                cmd.Parameters.AddWithValue("@Partida", Partida);
                cmd.Parameters.AddWithValue("@ClaveRecibo", ClaveRecibo);
                cmd.Parameters.AddWithValue("@Concepto2", Concepto2);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@Unidad", Unidad);
                cmd.Parameters.AddWithValue("@Divisa", Divisa);
                cmd.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                cmd.Parameters.AddWithValue("@Precio", Precio);
                cmd.Parameters.AddWithValue("@Subtotal", Subtotal);
                cmd.Parameters.AddWithValue("@Descuento", Descuento);
                cmd.Parameters.AddWithValue("@Total", Total);
                cmd.Parameters.AddWithValue("@Impuesto", Impuesto);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return mensaje;
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
                cmd = new SqlCommand("Delete OrdenPedidoCliente where Folio='" + Folio + "'", cn);
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
        public void ActualizarOrden(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum((Convert(decimal, Impuesto) / 100) * (Subtotal-Descuento)) as Impuesto, sum((Convert(decimal, Descuento) / 100) * Subtotal) as Descuento, sum(Total) as Total from PartidaOrden where FolioOrden='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = dr["Impuesto"].ToString();
                    dr.Close();

                   

                    cmd = new SqlCommand("Update OrdenPedidoCliente set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "', Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public void ActualizarTotalesOrdenPedidoCliente(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select coalesce(sum(Subtotal),0.00) as Subtotal, coalesce(sum((Convert(decimal, Impuesto) / 100) * (Subtotal-Descuento)),0.00) as Impuesto, coalesce(sum((Convert(decimal, Descuento) / 100) * (Subtotal)),0.00) as Descuento, coalesce(sum(Total),0.00) as Total from [PartidaOrdenPedidoCliente] where FolioOrden='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Descuentos = dr["Descuento"].ToString();
                    string Total = dr["Total"].ToString();
                    string Impuesto = dr["Impuesto"].ToString();
                    dr.Close();

                    cmd = new SqlCommand("Update OrdenPedidoCliente set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Descuento='" + Descuentos + "', Cargo='" + Impuesto + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
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
                cmd = new SqlCommand("UpdateClientesset Saldo= Saldo - " + Saldo + " where IdCliente=" + Clave + "", cn);
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
                cmd = new SqlCommand("UpdateClientesset Saldo= Saldo + " + Saldo + " where IdCliente=" + Clave + "", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
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
        public List<List<string>> ObtenerPartidas(string Folio)
        {
            List<List<string>> listam = new List<List<string>>();
            //cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenCompra as OC, Documento as D where OC.ClaveDocumento=D.Clave", cn);
            cmd = new SqlCommand("Select PS.ClaveProducto, PR.Cantidad, PS.TipoCosteo, (PR.Subtotal/PR.Cantidad), PR.Partida, PS.UnidadMedida, PR.Total from [PartidaRemision] as PR Join Remision as R On R.Folio=PR.[FolioRemision] Join ProductosServicios as PS On PR.ClaveProducto=PS.ClaveProducto where FolioRemision=@Folio and PR.ClaveProducto=PS.ClaveProducto", cn);

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
        //_______________________________________________________________________________________________________________
        public void ActualizarReciboEstatus(string Folio, string Estatus)
        {
            try
            {

                cmd = new SqlCommand("Update OrdenPedidoCliente set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        public bool PartidasPendientesOrdenPedido(string Folio)
        {
            bool pendiente = false;
            try
            {

                cmd = new SqlCommand("Select 1 from partidaOrdenPedidoCliente where CantidadPendiente>0 and FolioOrden='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    pendiente = true;
                dr.Close();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return pendiente;
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
                        cmd = new SqlCommand("Update OrdenPedidoCliente set Saldo='0.00', Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
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
        public void ReciboSaldosPartidasRecepcion(string txtFolio, TextBox txtSubtoral, TextBox txtDescuento, TextBox txtTotal)
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
        public void ReciboSaldosPartidasRecepcion2(string txtFolio, TextBox Impuesto)
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
                    SUM(ISNULL(CAST((Impuesto / 100.0) * (Subtotal - (Subtotal * (Descuento / 100.0))) AS decimal(18, 2)), 0)) AS Impuesto
                    FROM [PartidaOrdenPedidoCliente]
                     where FolioOrden = '" + txtFolio + "'", cn);
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
        public void ReciboSaldosPartidasCliente(string txtFolio, Guna.UI2.WinForms.Guna2TextBox txtSubtoral, Guna.UI2.WinForms.Guna2TextBox txtDescuento, Guna.UI2.WinForms.Guna2TextBox txtTotal)
        {
            try
            {

                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Descuento) as Descuento, sum(Total) as Total from [PartidaOrdenPedidoCliente] where FolioOrden='" + txtFolio + "'", cn);
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
        public void ReciboSaldosPartidasOrden2(string txtFolio, Guna.UI2.WinForms.Guna2TextBox Impuesto)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            try
            {

                cmd = new SqlCommand("select sum(convert(decimal,(Impuesto / 100) * Subtotal)) as Impuesto from [PartidaOrdenPedidoCliente] where FolioOrden='" + txtFolio + "'", cn);
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

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from OrdenPedidoCliente where Folio='" + txtFolio + "'", cn);
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
        public void ReciboSaldosRecepcion(string txtFolio, TextBox txtSubtoral, TextBox txtDescuento, TextBox txtRecargo, TextBox txtTotal, TextBox txtTotalPartidas, TextBox txtSaldo)
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
        public void ConsultaRecibo(string Folio, TextBox Documento, ComboBox Estatus, Guna.UI2.WinForms.Guna2DateTimePicker Fecha, Guna.UI2.WinForms.Guna2TextBox Dias, Guna.UI2.WinForms.Guna2TextBox FechaVence, Guna.UI2.WinForms.Guna2TextBox Divisa, Guna.UI2.WinForms.Guna2TextBox TipoCambio, Guna.UI2.WinForms.Guna2TextBox Subtotal, Guna.UI2.WinForms.Guna2TextBox Descuentos, Guna.UI2.WinForms.Guna2TextBox Cargo, Guna.UI2.WinForms.Guna2TextBox Total, Guna.UI2.WinForms.Guna2TextBox Partidas, Guna.UI2.WinForms.Guna2TextBox Notas, Guna.UI2.WinForms.Guna2TextBox Elaborado, TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtConsecutivo, Guna.UI2.WinForms.Guna2TextBox txtAutoriza, Guna.UI2.WinForms.Guna2TextBox txtFechaAutoriza, ComboBox cmbAlmacen, out string cliente)
        {
            cliente = string.Empty;
            try
            {

                cmd = new SqlCommand("Select *, (Cast(A.Clave as varchar)+' - '+A.Nombre) as Alm from OrdenPedidoCliente as O Left Join Almacenes as A on A.Clave=O.Almacen where Folio='" + Folio + "'", cn);
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
                    
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();

                    MatriculaC = dr["IdCliente"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    txtConsecutivo.Text = dr["Consecutivo"].ToString();
                    txtAutoriza.Text = dr["UsuarioAutoriza"].ToString();
                    if (dr["FechaAutoriza"].ToString() != string.Empty)
                    {
                        txtFechaAutoriza.Text = Convert.ToDateTime(dr["FechaAutoriza"]).ToString("yyyy-MM-dd");
                    }
                    cmbAlmacen.Text = dr["Alm"].ToString();
                    cliente = dr["IdCliente"].ToString();
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
        public void ConsultaRecepcion(string Folio, TextBox Documento, ComboBox Estatus, TextBox Fecha, TextBox Divisa, TextBox TipoCambio, TextBox Subtotal, TextBox Descuentos, TextBox Cargo, TextBox Total, TextBox Partidas, TextBox Notas, TextBox Elaborado, TextBox txtFolio, TextBox txtReciboCol, TextBox txtconsecutivo, TextBox txtAlmacen, TextBox txtReferencia, TextBox saldo, TextBox condominio, TextBox DiasVence, TextBox FechaVence, TextBox Archivo)
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
                    MatriculaC = dr["IdCliente"].ToString();
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
                    MatriculaC = dr["IdCliente"].ToString();
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
                    MatriculaC = dr["IdCliente"].ToString();
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
        public void ConsultarPartidasOrdenPedido(DataGridView dgv, string Orden)
        {
            // Usar parámetros en lugar de concatenación para evitar SQL Injection
            string query = "Select * from PartidaOrdenPedidoCliente where FolioOrden = @FolioOrden";
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // Asignar el parámetro de la consulta
                cmd.Parameters.AddWithValue("@FolioOrden", Orden);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    // Limpiar el DataGridView antes de cargar nuevos datos
                    dgv.Rows.Clear();

                    // Leer cada fila de la consulta
                    while (dr.Read())
                    {
                        // Asumiendo que dr[0], dr[1], etc. corresponden a las columnas que necesitas
                        string[] valores =
                        {
                    dr[0].ToString(),
                    dr[1].ToString(),
                    dr[2].ToString(),
                    // Agrega más columnas si es necesario
                };

                        // Agregar la fila al DataGridView
                        dgv.Rows.Add(valores);
                    }
                }
            }
        }

        //_________________________________________________________________________________________
        public string[] InformacionOrdenPedido(string Orden)
        {
            cmd = new SqlCommand(" select OC.Folio, OC.ClaveDocumento, Oc.IdCliente, OC.Almacen, OC.Tipo, OC.Notas from OrdenPedidoCliente as OC where Folio='"+Orden+"'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr["Folio"].ToString(),
                     dr["ClaveDocumento"].ToString(),
                     dr["IdCliente"].ToString(),
                     dr["Almacen"].ToString(),
                     dr["Tipo"].ToString(),
                     dr["Notas"].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //_________________________________________________________________________________________
        public string[] InformacionRecepcion2(string Documento)
        {
            cmd = new SqlCommand("Select (convert(varchar, OC.Folio) + ' - ' + D.Nombre) as Nombre from OrdenPedidoCliente as OC, Documento as D where OC.ClaveDocumento=D.Clave and OC.Folio='" + Documento + "'", cn);
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
        public void CargarOrdenPedidoClientePartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select PO.*, PS.Descripcion from [PartidaOrdenPedidoCliente] as PO, ProductosServicios as PS where FolioOrden='" + Folio + "' and PO.ClaveProducto=PS.ClaveProducto order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioOrden"].ToString();
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
                    dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
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
        public void ConsultaPartidaOrdenPedido(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, Guna.UI2.WinForms.Guna2TextBox Concepto2, Guna.UI2.WinForms.Guna2TextBox cantidad, Guna.UI2.WinForms.Guna2TextBox unidad, Guna.UI2.WinForms.Guna2TextBox divisa, Guna.UI2.WinForms.Guna2TextBox tipocambio, Guna.UI2.WinForms.Guna2TextBox subtotal, Guna.UI2.WinForms.Guna2TextBox descuento, Guna.UI2.WinForms.Guna2TextBox total, Guna.UI2.WinForms.Guna2TextBox TxtPrecio, Guna.UI2.WinForms.Guna2TextBox txtImpuesto, Guna.UI2.WinForms.Guna2TextBox txtEntregado, ComboBox cmbConcepto2)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from [PartidaOrdenPedidoCliente] as P, ProductosServicios as C where FolioOrden='" + Folio + "' and Partida='" + Partida + "'and P.ClaveProducto=C.ClaveProducto", cn);
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
                    cmbConcepto2.Items.Add(dr["Descripcion"].ToString());
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
        public void ConsultaPartidaRecepcion(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, TextBox Concepto2, TextBox cantidad, TextBox unidad, TextBox divisa, TextBox tipocambio, TextBox txtPrecio, TextBox descuento, TextBox total, TextBox txtImpuestos, TextBox archivo)
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
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    txtPrecio.Text = dr["Subtotal"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();
                    txtImpuestos.Text = dr["Impuesto"].ToString();
                    archivo.Text = dr["Archivo"].ToString();
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
            int ExActual = 0;
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
                    cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "', '" + txtExActual + "',  '" + txtExActual + "', '0','" + txtExActual + "')", cn);
                    cmd.ExecuteNonQuery();

                    if (TipoCosteo == "Ultima Compra")
                    {
                        cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else if (TipoCosteo == "Promedio")
                    {
                        cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            ExActual = Convert.ToInt32(dr["ExActual"].ToString()) + Convert.ToInt32(txtExActual);
                            Total = Convert.ToDecimal(dr["Total"].ToString()) + precio;
                            GranTotal = Total / ExActual;
                        }
                        else
                        {
                            ExActual = Convert.ToInt32(txtExActual);
                            Total = precio;
                            GranTotal = Total;
                        }
                        dr.Close();

                        cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    cmd = new SqlCommand("Update AlmacenProducto set ExistenciaInicial= ExistenciaActual, Entradas= Entradas + '" + txtExActual + "', ExistenciaActual= ExistenciaActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);
                    cmd.ExecuteNonQuery();

                    if (TipoCosteo == "Ultima Compra")
                    {
                        cmd = new SqlCommand("Update ProductosServicios set CostoUnitario= " + precio + " where ClaveProducto= '" + txtClaveProducto + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else if (TipoCosteo == "Promedio")
                    {
                        cmd = new SqlCommand("select ExActual, (ExActual * CostoUnitario) as Total from ProductosServicios where ClaveProducto='" + txtClaveProducto + "' and Inventariable='Si' and ExActual>0 ", cn);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            ExActual = Convert.ToInt32(dr["ExActual"].ToString()) + Convert.ToInt32(txtExActual);
                            Total = Convert.ToDecimal(dr["Total"].ToString()) + precio;
                            GranTotal = Total / ExActual;
                        }
                        else
                        {
                            ExActual = Convert.ToInt32(txtExActual);
                            Total = precio;
                            GranTotal = Total;
                        }
                        dr.Close();

                        cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual + '" + txtExActual + "' where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

        }
        public void ValidarDocumentoSPR()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM TipoMovimiento WHERE Documento = 'SPR' AND TipoMovimiento = 'S') " +
                                                     "BEGIN " +
                                                     "    INSERT INTO TipoMovimiento VALUES ('S', 'SPR', 'SALIDA POR REMISIÓN', 'Activo', '0', 'Si', 'Si', '', '') " +
                                                     "END", cn);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void ActualizarMovimientoJ(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas)
        {
            try
            {
                cmd = new SqlCommand(@"
    UPDATE R 
    SET 
        Estatus='Bloqueado', 
        R.Descuento = (
            SELECT COALESCE(SUM(CAST(PP.Descuento AS DECIMAL)), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        R.Impuestos = (
            SELECT COALESCE(SUM(CAST(PP.IVA AS DECIMAL)), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        Total = (
            SELECT COALESCE(SUM(PP.Total), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        Subtotal = (
            SELECT COALESCE(SUM(PP.Subtotal), 0.00) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        ), 
        TotalPartidas = (
            SELECT COALESCE(Count(*), 0) 
            FROM PartidasMovimientoInventario AS PP 
            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
        )
    FROM 
        MovimientoInventario AS R 
    JOIN 
        PartidasMovimientoInventario AS P 
    ON 
        R.Folio = P.FolioMovimiento AND R.Descripcion = P.Descripcion 
    WHERE 
        R.Folio = @txtFolio 
        AND R.TipoDocumento = @txtTipoMovimiento 
        AND R.Descripcion = @txtDocumento", cn);
                cmd.Parameters.AddWithValue("@txtPartidas", txtPartidas);
                cmd.Parameters.AddWithValue("@txtFolio", txtFolio);
                cmd.Parameters.AddWithValue("@txtTipoMovimiento", txtTipoMovimiento);
                cmd.Parameters.AddWithValue("@txtDocumento", txtDocumento);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRORac" + ex.ToString());
            }
        }
        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion, string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas, string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado, TextBox FOlioP, string txtAlmacenSalida, string txtCentroCosto, string Propietario, string OrdenTrabajo, string CajaR)
        {
            string mensaje = "";
            int contador = 0;
            int FolioM = 0;

            try
            {
                cmd = new SqlCommand("Select top 1 * from MovimientoInventario order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    FolioM = Convert.ToInt32(dr["Folio"].ToString());
                    FolioM++;

                    FOlioP.Text = FolioM.ToString();
                }
                else
                {
                    FolioM = 1;
                    FOlioP.Text = FolioM.ToString();
                }
                dr.Close();

                int cons = 1;
                cmd = new SqlCommand("Select UltimoFolio from TipoMovimiento where TipoMovimiento='" + txtTipoDocumento + "' and Documento='" + cmbDescripcion + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cons = Convert.ToInt32(dr["UltimoFolio"].ToString());
                    cons++;


                }

                dr.Close();

                cmd = new SqlCommand("select * from MovimientoInventario where Folio='" + FolioM + "' and TipoDocumento='" + txtTipoDocumento + "' and Referencias='" + txtReferencias + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into MovimientoInventario (Folio, TipoDocumento, Descripcion, Fecha, Estatus, Referencias, Almacen, TotalPartidas, Divisa, TipoCambio, Total, Notas, Elaborado, Consecutivo, AlmacenSalida) values ('" + FolioM + "', '" + txtTipoDocumento + "',  '" + cmbDescripcion + "',  '" + dtpFecha + "', '" + cmbEstatus + "',  '" + txtReferencias + "', '" + txtAlmacen + "', '" + txtTotalPartidas + "', '" + cmbDivisa + "', '" + txtTipoCambio + "', '" + txtTotal + "', '" + txtNotas + "', '" + txtElaborado + "', '" + cons + "', '" + txtAlmacenSalida + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return FolioM.ToString();

        }
        public void RegistroPartida(string txtFolioMovimiento, string txtTipoDocumento, string txtDescripcion, string txtNoPartida, string cmbClaveProducto, string txtCantidad, string txtunidad, decimal txtPrecio, string cmbDivisa, string txtTipoCambio, decimal txtTotal, string Concepto)
        {
            try
            {

                cmd = new SqlCommand("Insert into PartidasMovimientoInventario (FolioMovimiento, TipoDocumento, Descripcion, NoPartida, ClaveProducto, Cantidad, unidad, Precio, Divisa, TipoCambio, Total, Concepto) values ('" + txtFolioMovimiento + "', '" + txtTipoDocumento + "', '" + txtDescripcion + "','" + txtNoPartida + "', '" + cmbClaveProducto + "', '" + txtCantidad + "','" + txtunidad + "', " + txtPrecio + ", '" + cmbDivisa + "', '" + txtTipoCambio + "', " + txtTotal + ", '" + Concepto + "' )", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        public void RegistroProductoSalidas(string txtClaveProducto, string txtExActual, string txtAlmacen)
        {
            int contador = 0;

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
                    cmd = new SqlCommand("INSERT INTO AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) VALUES (@ClaveAlmacen, @ClaveProducto, 0, 0, 0 - @ExistenciaActual, 0 - @ExistenciaActual)", cn);
                    cmd.Parameters.AddWithValue("@ClaveAlmacen", txtAlmacen);
                    cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                    cmd.Parameters.AddWithValue("@ExistenciaActual", Convert.ToDecimal(txtExActual));
                    //cmd = new SqlCommand("Insert into AlmacenProducto (ClaveAlmacen, ClaveProducto, ExistenciaInicial, Entradas, Salidas, ExistenciaActual) values ('" + txtAlmacen + "', '" + txtClaveProducto + "',  '" + txtExActual + "', '0','" + txtExActual + "', '" + txtExActual + "')", cn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update ProductosServicios set ExActual= ExActual - '" + txtExActual + "'  where ClaveProducto= '" + txtClaveProducto + "' and Inventariable='Si'", cn);
                    cmd.ExecuteNonQuery();
                }


                else
                {
                    //cmd = new SqlCommand("Update AlmacenProducto set  Salidas= Salidas + '" + txtExActual + "', ExistenciaActual= Entradas -'" + txtExActual + "'  where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);
                    //cmd = new SqlCommand("Update AlmacenProducto set  Salidas= Salidas + '" + txtExActual + "', ExistenciaActual= ExistenciaActual -'" + txtExActual + "'  where ClaveProducto= '" + txtClaveProducto + "' and ClaveAlmacen = '" + txtAlmacen + "'", cn);

                    cmd = new SqlCommand("Update AlmacenProducto set Salidas = Salidas + @ExActual, ExistenciaActual = ExistenciaInicial + Entradas - @ExActual - Salidas where ClaveProducto = @ClaveProducto and ClaveAlmacen = @ClaveAlmacen", cn);
                    cmd.Parameters.AddWithValue("@ExActual", Convert.ToDecimal(txtExActual));
                    cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                    cmd.Parameters.AddWithValue("@ClaveAlmacen", txtAlmacen);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("Update ProductosServicios set ExActual = ExActual - @ExActual where ClaveProducto = @ClaveProducto and Inventariable = 'Si'", cn);
                    cmd.Parameters.AddWithValue("@ExActual", Convert.ToDecimal(txtExActual));
                    //cmd.Parameters.AddWithValue("@CantidadInv", Convert.ToDecimal(txtcantidadinv));
                    cmd.Parameters.AddWithValue("@ClaveProducto", txtClaveProducto);
                    cmd.ExecuteNonQuery();
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
      


        //_________________________________________________________________________________________
       
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
        public decimal ObtenerTotalOrdenPedidoCliente(string Folio)
        {
            decimal total = 0.00m;

            try
            {
                cmd = new SqlCommand("select Total from OrdenPedidoCliente where Folio=" + Folio + "", cn);
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
        public string EliminarPartidaOrdenPedidoCliente(string Folio, string Partida)
        {
            int contador = 0;
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Delete from PartidaOrdenPedidoCliente where FolioOrden=@Folio and Partida=@Partida", cn))
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
                string queryUpdate = "UPDATE PartidaOrdenPedidoCliente SET Partida = Partida - 1 WHERE Partida > @NumeroOrdenCompra and FolioOrden=@Folio";

                using (SqlConnection connection = new SqlConnection(ObtenerCn()))
                {
                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroOrdenCompra", Partida);
                        command.Parameters.AddWithValue("@Folio", Folio);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }



            }
            catch (Exception ex)
            {
                mensaje = "Error." + ex.ToString();
            }
            return mensaje;

        }
        
        public string ObtenerTotalPartidasOrdenPedidoCliente(string Folio)
        {
            string maximo = "0";
            cmd = new SqlCommand("select max(Partida) as maximo from PartidaOrdenPedidoCliente where FolioOrden='" + Folio + "'", cn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                maximo = dr[0].ToString();
            }
            dr.Close();
            return maximo;
        }
    }

}
