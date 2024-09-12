using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases.Inventario
{
    class DBRegistrarEntradas
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

        public DBRegistrarEntradas()
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
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDocumentoEntrada(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='E' and Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
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
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDocumentoSalida(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='S' and Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDocumentoTraslado(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='T' and Estatus='Activo'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }

        internal void SeleccionarProducto(Guna2ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from ProductosServicios where Inventariable = 'Si'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
            }
            dr.Close();
        }

        

        //______________________________________________________________________________________________________________________________
        public string[] InformacionEntrada(string Documento)
        {
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'E' and Documento= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[2].ToString(),
                    dr[4].ToString(),
                    dr[6].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionSalida(string Documento)
        {
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'S' and Documento= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[2].ToString(),
                    dr[4].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionTraspaso(string Documento)
        {
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'T' and Documento= '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[2].ToString(),
                    dr[4].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDivisa(string Nombre)
        {
            cmd = new SqlCommand("Select TipoCambio from Divisas where Nombre= '" + Nombre + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma Movimiento 
        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion, string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas, string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado, TextBox FOlioP, string txtAlmacenSalida)
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

                cmd = new SqlCommand("select * from MovimientoInventario where Folio='" + FolioM + "' and TipoDocumento='" + txtTipoDocumento + "' and Referencias='" + txtReferencias + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into MovimientoInventario (Folio, TipoDocumento, Descripcion, Fecha, Estatus, Referencias, Almacen, TotalPartidas, Divisa, TipoCambio, Total, Notas, Elaborado, Consecutivo, AlmacenSalida) values ('" + FolioM + "', '" + txtTipoDocumento + "',  '" + cmbDescripcion + "',  '" + dtpFecha + "', '" + cmbEstatus + "',  '" + txtReferencias + "', '" + txtAlmacen + "', '" + txtTotalPartidas + "', '" + cmbDivisa + "', '" + txtTipoCambio + "', '" + txtTotal + "', '" + txtNotas + "', '" + txtElaborado + "', '" + txtFolio + "', '" + txtAlmacenSalida + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string RegistroMovimiento(string txtTipoMovimiento, string txtDocumento, string txtUltimoFolio)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();



                cmd = new SqlCommand("Update TipoMovimiento set  UltimoFolio='" + txtUltimoFolio + "' where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn);
                cmd.ExecuteNonQuery();

                mensaje = "Registro modificado.";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string ActualizarMovimiento(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas, string txtTotal)
        {
            string mensaje = "";

            try
            {
                cmd = new SqlCommand("Update MovimientoInventario set Estatus='Bloqueado', TotalPartidas= '" + txtPartidas + "', Total='" + txtTotal + "' where Folio='"+txtFolio+"' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string ActualizarMovimiento2(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas, string txtTotal)
        {
            string mensaje = "";

            try
            {
                cmd = new SqlCommand("Update MovimientoInventario set Estatus='Cancelado', TotalPartidas= '" + txtPartidas + "', Total='" + txtTotal + "' where Folio='" + txtFolio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaEntradaSeleccionado(string Folio, string txtTipoMovimiento, string txtDocumento, Guna.UI2.WinForms.Guna2TextBox txtPartidas, Guna.UI2.WinForms.Guna2TextBox txtReferencia, TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtNotas, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtElaborado, Label cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio, TextBox txtFolioP)
        {
            try
            {
                cmd = new SqlCommand("select * from MovimientoInventario where Folio='"+Folio+"' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtPartidas.Text = dr["TotalPartidas"].ToString();
                    txtReferencia.Text = dr["Referencias"].ToString();
                    txtAlmacen.Text = dr["Almacen"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtElaborado.Text = dr["Elaborado"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    cmbDivisa.Text = dr["Divisa"].ToString();
                    txtFolioP.Text = dr["Folio"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        internal void RegistroPartida(string text1, string text2, string text3, string text4, string clave, string text5, string text6, decimal v1, string text7, string text8, decimal v2, string text9)
        {
            throw new NotImplementedException();
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void Consulta(string Folio, string txtTipoMovimiento, string txtDocumento, Guna.UI2.WinForms.Guna2TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtDescricpcion, Guna.UI2.WinForms.Guna2TextBox txtPartidas, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtNotas, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtElaborado, ComboBox cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio)
        {
            try
            {
                cmd = new SqlCommand("select * from MovimientoInventario where Folio= '" + Folio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtFolio.Text = dr["Folio"].ToString();
                    txtDescricpcion.Text = dr["Descripcion"].ToString();
                    txtPartidas.Text = dr["TotalPartidas"].ToString();
                    txtReferencia.Text = dr["Referencias"].ToString();
                    txtAlmacen.Text = dr["Almacen"].ToString();
                    txtTotal.Text = dr["Total"].ToString();
                    txtNotas.Text = dr["Notas"].ToString();
                    cmbEstatus.Text = dr["Estatus"].ToString();
                    txtElaborado.Text = dr["Elaborado"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    cmbDivisa.Text = dr["Divisa"].ToString();
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
        public void ConsultaPartida(string Folio, string Tipo, string Partida, string Documento, Guna.UI2.WinForms.Guna2TextBox cmnDescripcion, Guna.UI2.WinForms.Guna2TextBox txtConcepto, Guna.UI2.WinForms.Guna2TextBox txtAlias, Guna.UI2.WinForms.Guna2TextBox txtTipoCosteo, Guna.UI2.WinForms.Guna2TextBox txtExActual, Guna.UI2.WinForms.Guna2TextBox txtCantidad, Guna.UI2.WinForms.Guna2TextBox txtUnidad, Guna.UI2.WinForms.Guna2TextBox txtPrecio, Guna.UI2.WinForms.Guna2TextBox cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio, Guna.UI2.WinForms.Guna2TextBox txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select P.*, PS.*, PS.Descripcion as Producto from PartidasMovimientoInventario as P, ProductosServicios as PS where P.FolioMovimiento= '" + Folio + "' and P.TipoDocumento= '" + Tipo + "' and P.Descripcion= '" + Documento + "' and P.NoPartida= '" + Partida + "' and P.ClaveProducto=PS.ClaveProducto", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtConcepto.Text = dr["Concepto"].ToString();
                    cmnDescripcion.Text = dr["Producto"].ToString();
                    txtAlias.Text = dr["Alias"].ToString();
                    txtTipoCosteo.Text = dr["TipoCosteo"].ToString();
                    txtExActual.Text = dr["ExActual"].ToString();
                    txtCantidad.Text = dr["Cantidad"].ToString();
                    txtUnidad.Text = dr["unidad"].ToString();
                    txtPrecio.Text = dr["Precio"].ToString();
                    cmbDivisa.Text = dr["Divisa"].ToString();
                    txtTipoCambio.Text = dr["TipoCambio"].ToString();
                    txtTotal.Text = dr["Total"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                //dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarDocumento(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from MovimientoInventario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarDocumentoFiltro(DataGridView dgv, string FiltroTipo, string FiltroFecha)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento='" + FiltroTipo + "' and Fecha='" + FiltroFecha + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        internal string[] InformacionProducto(string clave)
        {
            throw new NotImplementedException();
        }

        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarPartida(DataGridView dgv, string tipo, string descripcion, string folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from PartidasMovimientoInventario where TipoDocumento='" + tipo + "' and Descripcion = '" + descripcion + "' and FolioMovimiento = " + folio + "", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["NoPartida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        internal string[] InformacionProductoAlmacen(string clave, string almacen)
        {
            throw new NotImplementedException();
        }

        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarEntrada(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'E'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarSalida(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'S'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //Movimiento Registrados
        public void CargarTraspaso(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'T'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["TipoDocumento"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Estatus"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
    }
}
