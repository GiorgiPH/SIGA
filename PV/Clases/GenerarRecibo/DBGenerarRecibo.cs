using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;
using PV;

namespace Condominios.Clases.GenerarRecibo
{
    class DBGenerarRecibo
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

        public DBGenerarRecibo()
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
        //___________________________________________________________________________________________
        public void SeleccionarConceptoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Nombre) as Nombre from Documento where TipoDocumento='Venta' and Clase='Remision'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarFormaPAgo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from FormasPago", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarFormaPAgo2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Descripcion from FormasPago", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarpROPIEDAD(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (Clave +'-'+ Descripcion) Descripcion from Condominios_SubCondominios order by Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarCondominio(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select (convert(varchar,ClaveCondominio) +'-'+ Descripcion) as Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropietariosNombre(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select RazonSocial from Propietarios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropiedadNombre(ComboBox cb, string Propietario)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select (ClaveCondominio +'-'+ Descripcion) as Condominio from Propietarios_Condominios where ClavePropietario='" + Propietario + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarDivisa(ComboBox cb)
        {
            cb.Items.Clear();
           cmd = new SqlCommand("Select Nombre from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarDivisa2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODAS");
            cmd = new SqlCommand("Select Nombre from Divisas", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropietariosNombreProveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select convert(varchar, IdProveedor) + ' - ' + RazonSocial from Proveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarConceptoDocumento2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select distinct (R.ClaveDocumento + ' - ' + D.Nombre) as Docuemnto from Recibo as R, Documento as D where R.ClaveDocumento=D.Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarConceptoDocumento3(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select distinct (D.Clave +'-'+ D.Nombre) as Docuemnto from Recibo as R, Documento as D where R.ClaveDocumento=D.Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropiedad(ComboBox cb, string clavePropietario)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select * from Propietarios_Condominios where ClavePropietario='"+ clavePropietario + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
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
        //______________________________________________________________________________________________
        public void Consecutivo(TextBox txtConsecutivo, string ClaveDocumento)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Recibo where ClaveDocumento='" + ClaveDocumento + "' order by Consecutivo Desc", cn);
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
        public void InsertarRecibo(TextBox txtFolio, string ClaveDocumento, string Estatus, string Fecha, string DiasVencen, string FechaVence, string ClavePropietario, string Divisa, string TipoCambio, string Descuento, string Recargo, string Notas, string Elaborado, string Propiedad, string Consecutivo)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Recibo order by Folio Desc", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int Folio = Convert.ToInt32(dr["Folio"].ToString());
                    Folio++;

                    txtFolio.Text = Folio.ToString();
                    dr.Close();

                    cmd = new SqlCommand("insert into Recibo (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClavePropietario, Divisa, TipoCambio, Descuento, Cargo, Notas, Elaborado, Propiedad, Consecutivo) values ('" + Folio + "', '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '"+Descuento+"', '"+Recargo+"', '" + Notas + "', '" + Elaborado + "', '" + Propiedad + "', '" + Consecutivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    txtFolio.Text = "1";
                    dr.Close();

                    cmd = new SqlCommand("insert into Recibo (Folio, ClaveDocumento, Estatus, Fecha, DiasVencen, FechaVence, ClavePropietario, Divisa, TipoCambio, Descuento, Cargo, Notas, Elaborado, Propiedad, Consecutivo) values (1, '" + ClaveDocumento + "', '" + Estatus + "', '" + Fecha + "', '" + DiasVencen + "', '" + FechaVence + "', '" + ClavePropietario + "', '" + Divisa + "', '" + TipoCambio + "', '" + Descuento + "', '" + Recargo + "','" + Notas + "', '" + Elaborado + "', '" + Propiedad + "', '" + Consecutivo + "')", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_________________________________________________________________________________________________________
        public string[] InformacionPropietarioRecibo(string Matricula)
        {
            cmd = new SqlCommand("Select IdPropietario, RazonSocial from Propietarios where IdPropietario= '" + Matricula + "'", cn);
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
        public string[] InformacionPropietarioNombre(string Matricula)
        {
            cmd = new SqlCommand("Select IdPropietario from Propietarios where RazonSocial= '" + Matricula + "'", cn);
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
        public string[] InformacionPropiedadNombre(string Matricula)
        {
            cmd = new SqlCommand("Select ClaveCondominio from Propietarios_Condominios where (ClaveCondominio +'-'+ Descripcion)= '" + Matricula + "'", cn);
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
        public void ActualizarReciboEstatus(string Folio, string Estatus, string MatriculaAlumno)
        {
            try
            {

                cmd = new SqlCommand("Update Recibo set Estatus='" + Estatus + "' where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("insert into Cobros (Folio, ClavePropietario) values ('" + Folio + "', '" + MatriculaAlumno + "')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ActualizarRecibo3(string Folio)
        {
            try
            {
                cmd = new SqlCommand("Update Recibo set Saldo= 0.00 where Folio='" + Folio + "'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select R.*, P.RazonSocial from Recibo as R, Propietarios as P where R.ClavePropietario=P.IdPropietario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

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
                da = new SqlDataAdapter("select R.*, P.RazonSocial from Recibo as R, Propietarios as P where R.Folio like '%" + Filtro + "%' and R.ClavePropietario=P.IdPropietario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

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
                da = new SqlDataAdapter("select R.*, P.RazonSocial from Recibo as R, Propietarios as P where P.RazonSocial like '%" + Filtro + "%' and R.ClavePropietario=P.IdPropietario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //__________________________________________________________________________________________________________-
        public void CargarRecibosFiltroFecha(DataGridView dgv, string Filtro)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select R.*, P.RazonSocial from Recibo as R, Propietarios as P where R.Fecha = '" + Filtro + "' and R.ClavePropietario=P.IdPropietario", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["RazonSocial"].ToString();
                    dgv.Rows[n].Cells[3].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar");
            }
        }
        //_______________________________________________________________________________________________________________
        public void DiasVence(TextBox txtDias)
        {
            try
            {

                cmd = new SqlCommand("select DiasPlazo from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtDias.Text = dr["DiasPlazo"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void ReciboSaldos(string txtFolio, TextBox txtSubtoral, TextBox txtDescuento, TextBox txtRecargo, TextBox txtTotal, TextBox txtSaldo, TextBox txtTotalPartidas)
        {
            try
            {

                cmd = new SqlCommand("select Subtotal, Descuento, Cargo, Total, Saldo, TotalPartidas from Recibo where Folio='" + txtFolio + "'", cn);
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
        public void ReciboSaldosPartidas(string txtFolio, TextBox txtSubtoral, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {

                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Descuento) as Descuento, sum(Total) as Total from Partida where FolioRecibo='" + txtFolio + "'", cn);
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
        public void TipoCambio(string divisa)
        {
            try
            {

                cmd = new SqlCommand("select TipoCambio from Divisas where Nombre='" + divisa + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if(dr["TipoCambio"].ToString()== string.Empty)
                    {
                        ReporteSaldoPropietarioFiltro.TipoCambio = "1.0000";
                    }
                    else
                    {
                        ReporteSaldoPropietarioFiltro.TipoCambio = dr["TipoCambio"].ToString();
                    }
                   
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void TipoCambio2(string divisa)
        {
            try
            {

                cmd = new SqlCommand("select TipoCambio from Divisas where Nombre='" + divisa + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["TipoCambio"].ToString() == string.Empty)
                    {
                        ReporteSaldoCondominioFiltro.TipoCambio = "1.0000";
                    }
                    else
                    {
                        ReporteSaldoCondominioFiltro.TipoCambio = dr["TipoCambio"].ToString();
                    }

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void TipoCambio3(string divisa)
        {
            try
            {

                cmd = new SqlCommand("select TipoCambio from Divisas where Nombre='" + divisa + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["TipoCambio"].ToString() == string.Empty)
                    {
                        ReporteSaldoDetalladoFiltro.TipoCambio = "1.0000";
                    }
                    else
                    {
                        ReporteSaldoDetalladoFiltro.TipoCambio = dr["TipoCambio"].ToString();
                    }

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void TipoCambio4(string divisa)
        {
            try
            {

                cmd = new SqlCommand("select TipoCambio from Divisas where Nombre='" + divisa + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["TipoCambio"].ToString() == string.Empty)
                    {
                        ReporteSaldoDetalladoProveedoresFiltro.TipoCambio = "1.0000";
                    }
                    else
                    {
                        ReporteSaldoDetalladoProveedoresFiltro.TipoCambio = dr["TipoCambio"].ToString();
                    }

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void TipoCambio5(string divisa)
        {
            try
            {

                cmd = new SqlCommand("select TipoCambio from Divisas where Nombre='" + divisa + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["TipoCambio"].ToString() == string.Empty)
                    {
                        ReporteSaldoProveedorFiltro.TipoCambio = "1.0000";
                    }
                    else
                    {
                        ReporteSaldoProveedorFiltro.TipoCambio = dr["TipoCambio"].ToString();
                    }

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_______________________________________________________________________________________________________________
        public void TotalConceptosGlobales(string txtFolio, TextBox txtConceptosGlobales)
        {
            try
            {

                cmd = new SqlCommand("select COUNT(*) as Conceptos from ConceptoGlobalesRecibo where Folio='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtConceptosGlobales.Text = dr["Conceptos"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("2" + ex.ToString());
            }
        }
        //______________________________________________________________________________________________________________
        public void BuscarAlumnos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select IdPropietario, RazonSocial from Propietarios", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdPropietario"].ToString();
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
                da = new SqlDataAdapter("Select IdPropietario, RazonSocial from Propietarios where RazonSocial like '%" + Filtro + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["IdPropietario"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar Alumnos");
            }
        }
        //_______________________________________________________________________________________________
        public void SeleccionarConceptoRecibo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Clave + ' - ' + Descripcion) as Clave from ConceptosIngreso", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
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
        //___________________________________________________________________________________________
        public void Consulta5(string Folio, TextBox txtPartida)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Partida where FolioRecibo='" + Folio + "' order by Partida Desc", cn);
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
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarPartida(string Folio, string Partida, string ClaveRecibo, string Concepto2, string Cantidad, string Unidad, string Divisa, string TipoCambio, decimal Subtotal, decimal Descuento, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into Partida (FolioRecibo, Partida, ClaveRecibo, Concepto2, Cantidad, Unidad, Divisa, TipoCambio, Subtotal, Descuento, Total) values ('" + Folio + "', '" + Partida + "', '" + ClaveRecibo + "', '" + Concepto2 + "', '" + Cantidad + "', '" + Unidad + "', '" + Divisa + "', '" + TipoCambio + "', '" + Subtotal + "', '" + Descuento + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecibo (ClaveConceptoG, Folio, Subtotal, Descuento, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal2(string ClaveConceptoG, string Folio, decimal Subtotal, decimal DescuentCargo, decimal Total)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecibo (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('" + ClaveConceptoG + "','" + Folio + "', '" + Subtotal + "', '" + DescuentCargo + "', '" + Total + "')", cn);
                cmd.ExecuteNonQuery();
    
            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //___________________________________________________________________________________________
        public void InsertarReciboConceptoGlobal3(string ClaveConceptoG, string Folio)
        {
            try
            {
                cmd = new SqlCommand("insert into ConceptoGlobalesRecibo (ClaveConceptoG, Folio, Subtotal, Cargo, Total) values ('"+ClaveConceptoG+"','" + Folio + "', '0.00', '0.00', '0.00')", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //___________________________________________________________________________________________
        public void EliminarReciboConceptoGlobal3( string Folio)
        {
            try
            {
                cmd = new SqlCommand("delete ConceptoGlobalesRecibo where Folio='" + Folio + "' and Total= '0.00'", cn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        public string[] InformacionRecibo(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("Select * from ConceptosIngreso where (Clave + ' - ' + Descripcion)= '" + Recibo + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                     dr[0].ToString(),
                    dr[1].ToString(),
                     dr[4].ToString(),
                      dr[9].ToString(),

                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
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
        //_____________________________________________________________________________________________________
        public string[] InformacionCondominio(string Recibo)
        {
            dr.Close();
            cmd = new SqlCommand("select CS.Condominio from Propietarios_Condominios as PC, Condominios_SubCondominios as CS where CS.Clave=PC.ClaveCondominio and PC.ClaveCondominio='"+ Recibo + "'", cn);
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

        //____________________________________________________________________________________________
        public void ActualizarRecibo(string txtFolio, string txtPartida)
        {
            try
            {
                cmd = new SqlCommand("select sum(Subtotal) as Subtotal, sum(Total) as Total from Partida where FolioRecibo='" + txtFolio + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Subtotal = dr["Subtotal"].ToString();
                    string Total = dr["Total"].ToString();

                    dr.Close();

                    cmd = new SqlCommand("Update Recibo set TotalPartidas='" + txtPartida + "', Subtotal='" + Subtotal + "', Total='" + Total + "', Saldo='" + Total + "' where Folio='" + txtFolio + "'", cn);
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
                cmd = new SqlCommand("select sum(Descuento) as Descuento, sum(Cargo) as Cargo from ConceptoGlobalesRecibo where Folio=" + txtFolio + "", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string Descuento = dr["Descuento"].ToString();
                    string Cargo = dr["Cargo"].ToString();

                    dr.Close();

                    cmd = new SqlCommand("Update Recibo set Descuento='"+Descuento+"', Cargo='"+Cargo+"', Total='" + txtTotal + "', Saldo = '" + txtTotal + "' where Folio='" + txtFolio + "'", cn);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                //MessageBox.Show("ERROR" + ex.ToString());
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
        //_______________________________________________________
        public void CargarRecibosPartidas(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from Partida where FolioRecibo='" + Folio + "' order by Partida asc", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["FolioRecibo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Partida"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ClaveRecibo"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto2"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //_______________________________________________________
        public void CargarRecibosConcepto(DataGridView dgv, string Folio)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from ConceptoGlobalesRecibo where Folio='" + Folio + "'", cn);
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
        //___________________________________________________________________________
        //............................................................................................................
        public void ConsultaPartida(string Folio, string Partida, TextBox claveconcepto, TextBox Concepto, TextBox Concepto2, TextBox cantidad, TextBox unidad, TextBox divisa, TextBox tipocambio, TextBox subtotal, TextBox descuento, TextBox total)
        {
            try
            {
                cmd = new SqlCommand("Select P.*, C.Descripcion from Partida as P, ConceptosIngreso as C where FolioRecibo='" + Folio + "' and Partida='" + Partida + "'and P.ClaveRecibo=C.Clave", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    claveconcepto.Text = dr["ClaveRecibo"].ToString();
                    Concepto.Text = dr["Descripcion"].ToString();
                    Concepto2.Text = dr["Concepto2"].ToString();
                    cantidad.Text = dr["Cantidad"].ToString();
                    unidad.Text = dr["Unidad"].ToString();
                    divisa.Text = dr["Divisa"].ToString();
                    tipocambio.Text = dr["TipoCambio"].ToString();
                    subtotal.Text = dr["Subtotal"].ToString();
                    descuento.Text = dr["Descuento"].ToString();
                    total.Text = dr["Total"].ToString();

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
        public void ConsultaConcepto(string Folio, string Concepto, string Total, TextBox txtCleveConcepto, TextBox txtConcepto, TextBox txtClase, TextBox txtTipo, TextBox txtDivisa, TextBox txtTipoCambio, TextBox txtSubtotal, TextBox txtDescuento, TextBox txtTotal)
        {
            try
            {
                cmd = new SqlCommand("select CGR.*, CG.*, R.Divisa, R.TipoCambio from ConceptoGlobalesRecibo as CGR, ConceptosGlobales as CG, Recibo as R where CGR.Folio=R.Folio and CGR.ClaveConceptoG=CG.Clave and CGR.Folio='" + Folio + "' and CGR.ClaveConceptoG='" + Concepto + "' and CGR.Total='" + Total + "'", cn);
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
                    txtDescuento.Text = dr["DescuentCargo"].ToString();
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
        public void ConsultaSubtotal(string Folio, TextBox Subtotal)
        {
            try
            {
                cmd = new SqlCommand("Select Total from Recibo where Folio='" + Folio + "'", cn);
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
        //______________________________________________________________________________________________________-
        public void ConsultaAbono(string Folio, TextBox Abono, TextBox Fecha)
        {
            try
            {

                cmd = new SqlCommand("Select top 1 * from Cobros where Folio='" + Folio + "' and Pago is not null and Pago <> 0.00", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Abono.Text = dr["Pago"].ToString();
                    Fecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy-MM-dd");
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("3" + ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecibo(string Folio, TextBox Documento, ComboBox Estatus, TextBox Fecha, TextBox Dias, TextBox FechaVence, TextBox Divisa, TextBox TipoCambio, TextBox Subtotal, TextBox Descuentos, TextBox Cargo, TextBox Total, TextBox Saldo, TextBox Partidas, TextBox Notas, TextBox Elaborado, TextBox txtFolio, TextBox Consecutivo)
        {
            try
            {

                cmd = new SqlCommand("Select * from Recibo where Folio='" + Folio + "'", cn);
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
                    Saldo.Text = dr["Saldo"].ToString();
                    Partidas.Text = dr["TotalPartidas"].ToString();
                    Notas.Text = dr["Notas"].ToString();
                    Elaborado.Text = dr["Elaborado"].ToString();

                    MatriculaC = dr["ClavePropietario"].ToString();
                    txtFolio.Text = dr["Folio"].ToString();
                    Consecutivo.Text = dr["Consecutivo"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1"+ex.ToString());
                dr.Close();
            }
        }
        //______________________________________________________________________________________________________-
        public void ConsultaRecibo2(string Folio, ComboBox cmbPropiedad)
        {
            try
            {

                cmd = new SqlCommand("Select * from Recibo where Folio='" + Folio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    cmbPropiedad.Text = dr["Propiedad"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("4"+ex.ToString());
                dr.Close();
            }
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropietarios(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select (convert(varchar, idPropietario) + ' - ' + RazonSocial) as Nombre from Propietarios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
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
    }
}
