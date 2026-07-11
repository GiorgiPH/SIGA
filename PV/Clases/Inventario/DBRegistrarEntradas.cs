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
        public static int Folio = 0;
        public static int Eliminado = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBRegistrarEntradas()
        {
            // Ya no se abre una conexión compartida aquí.
            // Cada método abre y cierra su propia conexión con "using"
            // para evitar conexiones o DataReaders que se quedan abiertos.
        }

        //______________________________________________________________________________________________________________________________
        public void SeleccionarDocumentoEntrada(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='E' and Estatus='Activo'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select (convert(varchar, Clave) + ' - ' + Nombre) as Nombre from Almacenes", cn))
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
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select Clave from Almacenes where (convert(varchar, Clave) + ' - ' + Nombre) = '" + Documento + "'", cn))
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
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select (convert(varchar, Clave) + ' - ' + Nombre) from Almacenes where  Clave= '" + Documento + "'", cn))
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
        public void SeleccionarDocumentoSalida(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='S' and Estatus='Activo'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDocumentoTraslado(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento='T' and Estatus='Activo'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }

        internal void SeleccionarProducto(Guna2ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from ProductosServicios where Inventariable = 'Si'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString() + '-' + dr[2].ToString() + ' ' + dr[4].ToString());
                    }
                }
            }
        }



        //______________________________________________________________________________________________________________________________
        public string[] InformacionEntrada(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'E' and Documento= '" + Documento + "'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionSalida(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'S' and Documento= '" + Documento + "'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[2].ToString(),
                            dr[4].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionTraspaso(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento= 'T' and Documento= '" + Documento + "'", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores =
                        {
                            dr[2].ToString(),
                            dr[4].ToString(),
                        };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }
        //______________________________________________________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from Divisas", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[1].ToString());
                    }
                }
            }
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionDivisa(string Nombre)
        {
            string[] resultado = null;
            using (SqlConnection cn = new SqlConnection(ObtenerCn()))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Select TipoCambio from Divisas where Nombre= '" + Nombre + "'", cn))
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma Movimiento 

        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion, string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas, string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado, TextBox FOlioP, string txtAlmacenSalida)
        {
            string mensaje = "";
            int contador = 0;
            int FolioM = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("Select top 1 * from MovimientoInventario order by Folio Desc", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }

                    using (SqlCommand cmd = new SqlCommand("select * from MovimientoInventario where Folio='" + FolioM + "' and TipoDocumento='" + txtTipoDocumento + "' and Referencias='" + txtReferencias + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    if (contador <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert into MovimientoInventario (Folio, TipoDocumento, Descripcion, Fecha, Estatus, Referencias, Almacen, TotalPartidas, Divisa, TipoCambio, Total, Notas, Elaborado, Consecutivo, AlmacenSalida) values ('" + FolioM + "', '" + txtTipoDocumento + "',  '" + cmbDescripcion + "',  '" + dtpFecha + "', '" + cmbEstatus + "',  '" + txtReferencias + "', '" + txtAlmacen + "', '" + txtTotalPartidas + "', '" + cmbDivisa + "', '" + txtTipoCambio + "', '" + txtTotal + "', '" + txtNotas + "', '" + txtElaborado + "', '" + txtFolio + "', '" + txtAlmacenSalida + "')", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        mensaje = "Registro guardado.";
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
        // registrar Movimiento 
        public string RegistroMovimiento(string txtTipoMovimiento, string txtDocumento, string txtUltimoFolio)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            contador++;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Update TipoMovimiento set  UltimoFolio='" + txtUltimoFolio + "' where TipoMovimiento= '" + txtTipoMovimiento + "' and Documento='" + txtDocumento + "'", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    mensaje = "Registro modificado.";
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
        public string ActualizarMovimiento(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas, string txtTotal)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Update MovimientoInventario set Estatus='Bloqueado', TotalPartidas= '" + txtPartidas + "', Total='" + txtTotal + "' where Folio='" + txtFolio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn))
                    {
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
        //_________________________________________________________________________________________________________________________--
        // registrar Movimiento 
        public string ActualizarMovimiento2(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas, string txtTotal)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("Update MovimientoInventario set Estatus='Cancelado', TotalPartidas= '" + txtPartidas + "', Total='" + txtTotal + "' where Folio='" + txtFolio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn))
                    {
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
        public void CancelarMovimientoInventario(string Folio, string Documento, string Descripcion)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("CancelarMovimientoInventario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Folio", Folio));
                        cmd.Parameters.Add(new SqlParameter("@TipoDocumento", Documento));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaEntradaSeleccionado(string Folio, string txtTipoMovimiento, string txtDocumento, Guna.UI2.WinForms.Guna2TextBox txtPartidas, Guna.UI2.WinForms.Guna2TextBox txtReferencia, TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtNotas, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtElaborado, Label cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio, TextBox txtFolioP)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from MovimientoInventario where Folio='" + Folio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }


        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void Consulta(string Folio, string txtTipoMovimiento, string txtDocumento, Guna.UI2.WinForms.Guna2TextBox txtFolio, Guna.UI2.WinForms.Guna2TextBox txtDescricpcion, Guna.UI2.WinForms.Guna2TextBox txtPartidas, Guna.UI2.WinForms.Guna2TextBox txtReferencia, Guna.UI2.WinForms.Guna2TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtTotal, Guna.UI2.WinForms.Guna2TextBox txtNotas, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtElaborado, ComboBox cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from MovimientoInventario where Folio= '" + Folio + "' and TipoDocumento= '" + txtTipoMovimiento + "' and Descripcion='" + txtDocumento + "'", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaPartida(string Folio, string Tipo, string Partida, string Documento, Guna.UI2.WinForms.Guna2TextBox cmnDescripcion, Guna.UI2.WinForms.Guna2TextBox txtConcepto, Guna.UI2.WinForms.Guna2TextBox txtAlias, Guna.UI2.WinForms.Guna2TextBox txtTipoCosteo, Guna.UI2.WinForms.Guna2TextBox txtExActual, Guna.UI2.WinForms.Guna2TextBox txtCantidad, Guna.UI2.WinForms.Guna2TextBox txtUnidad, Guna.UI2.WinForms.Guna2TextBox txtPrecio, Guna.UI2.WinForms.Guna2TextBox cmbDivisa, Guna.UI2.WinForms.Guna2TextBox txtTipoCambio, Guna.UI2.WinForms.Guna2TextBox txtTotal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("select P.*, PS.*, PS.Descripcion as Producto from PartidasMovimientoInventario as P, ProductosServicios as PS where P.FolioMovimiento= '" + Folio + "' and P.TipoDocumento= '" + Tipo + "' and P.Descripcion= '" + Documento + "' and P.NoPartida= '" + Partida + "' and P.ClaveProducto=PS.ClaveProducto", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from MovimientoInventario", cn))
                    {
                        DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento='" + FiltroTipo + "' and Fecha='" + FiltroFecha + "'", cn))
                    {
                        DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select M.*, P.Descripcion as des from PartidasMovimientoInventario as M Join ProductosServicios as P on P.ClaveProducto=M.ClaveProducto where M.TipoDocumento='" + tipo + "' and M.Descripcion = '" + descripcion + "' and M.FolioMovimiento = " + folio + " order by  NoPartida", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            //dgv.Rows[n].Cells[0].Value = item["TipoDocumento"].ToString();
                            dgv.Rows[n].Cells[0].Value = item["NoPartida"].ToString();
                            dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["des"].ToString();
                            dgv.Rows[n].Cells[3].Value = item["Cantidad"].ToString();
                            dgv.Rows[n].Cells[4].Value = item["FolioMovimiento"].ToString();
                        }
                    }
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'E'", cn))
                    {
                        DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'S'", cn))
                    {
                        DataTable dt = new DataTable();
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
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter("select * from MovimientoInventario where TipoDocumento= 'T'", cn))
                    {
                        DataTable dt = new DataTable();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

    }
}