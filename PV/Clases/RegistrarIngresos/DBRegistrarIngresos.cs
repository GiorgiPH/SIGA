using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Clases;
using PV.Properties;

namespace Condominios.Clases.RegistrarIngresos
{
    /// <summary>
    /// Acceso a datos para el registro de ingresos (cobros a clientes).
    ///
    /// La parte de Egresos (pagos a Proveedores: RecepcionProducto,
    /// RegistroGastos, RegistroReembolso, NotasGasto, Egreso) que antes vivía
    /// aquí se movió a Condominios.Clases.RegistrarEgresos.DBEgresos, para
    /// que esta clase represente un solo dominio (Ingresos/Cobros).
    ///
    /// Cada método abre y cierra su propia conexión (using), igual que en la
    /// clase original.
    ///
    /// RESUELTO: se revisó RegistrarCobro.cs (único lugar donde se llama
    /// InsertarCobro) y no existe ninguna inserción "placeholder" en Cobros
    /// con Fecha = NULL. Por eso se eliminó el DELETE que antes traía este
    /// método (borraba por Folio = ConceptoId, un residuo copiado de
    /// InsertarEgreso que no aplicaba a Cobros). Si en DBRemiision.cs
    /// apareciera algo que dependiera de esa condición, avisar para
    /// revertir este cambio.
    ///
    /// La columna Cobros.ClavePropietario se renombró a Cobros.IdCliente
    /// (ver Cobros_Migracion.sql) porque ahora referencia a Clientes, no a
    /// Propietarios. Ojo: la columna ClavePropietario que aparece en
    /// CargarReciboAlumno2 pertenece a la tabla Recibo, NO a Cobros, así
    /// que ese método no se tocó.
    ///
    /// NOTA: SeleccionarCuentaBancaria e InformacionCuenta se dejaron aquí
    /// tal cual, sin mover, porque son catálogos genéricos (CuentasBancarias)
    /// que probablemente también use el flujo de Egresos (RegistrarCobroEgreso).
    /// No se movieron a un lugar "neutral" para no romper referencias en
    /// archivos que no se revisaron todavía (p. ej. RegistrarCobro.cs,
    /// RegistrarCobroEgreso.cs). Se puede extraer a una clase de catálogos
    /// compartida más adelante, una vez confirmados todos los que las usan.
    /// </summary>
    class DBRegistrarIngresos
    {
        public static string usuario = string.Empty;
        public static string TipoUsuario = string.Empty;
        public static string Estatus = string.Empty;
        public static int Folio = 0;

        private static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        private static SqlConnection AbrirConexion()
        {
            var cn = new SqlConnection(ObtenerCn());
            cn.Open();
            return cn;
        }

        //_________________________________________________________________________________
        public string ActualizarCancelarIngreso2(string Clave, string Observaciones, decimal Abono, string Proveedor)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "Update Cobros set Observaciones=@Observaciones, Pago=0.00 where Folio=@Folio", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                            cmd.Parameters.AddWithValue("@Folio", Clave);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "Update Recibo set Saldo = Saldo + @Abono where Folio=@Folio", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Abono", Abono);
                            cmd.Parameters.AddWithValue("@Folio", Clave);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        mensaje = "Ingreso Cancelado";
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return mensaje;
        }

        //____________________________________________________________________________________________________
        public void CargarReciboAlumno2(DataGridView dgv, string Matricula, ArrayList ListaConcep)
        {
            try
            {
                dgv.Rows.Clear();

                using (SqlConnection cn = AbrirConexion())
                {
                    foreach (object item2 in ListaConcep)
                    {
                        string concepto = item2.ToString();

                        using (SqlCommand cmd = new SqlCommand(
                            "select R.*, D.Nombre from Recibo as R, Documento as D where ClavePropietario=@Matricula and R.Folio=@Folio and R.ClaveDocumento=D.Clave", cn))
                        {
                            cmd.Parameters.AddWithValue("@Matricula", Matricula);
                            cmd.Parameters.AddWithValue("@Folio", concepto);

                            DataTable dt = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }

                            foreach (DataRow item in dt.Rows)
                            {
                                int n = dgv.Rows.Add();
                                dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                                dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                                dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                                dgv.Rows[n].Cells[4].Value = Utilerias.FormatearMiles(item["Recargo"].ToString());
                                dgv.Rows[n].Cells[5].Value = Utilerias.FormatearMiles(item["DescuentoPago"].ToString());
                                dgv.Rows[n].Cells[6].Value = Utilerias.FormatearMiles(item["Saldo"].ToString());
                                dgv.Rows[n].Cells[7].Value = Utilerias.FormatearMiles("0");
                                dgv.Rows[n].Cells[8].Value = Utilerias.FormatearMiles("0");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        //____________________________________________________________________________________________________
        public void CargarPagosRemisiones(DataGridView dgv, string Matricula)
        {
            try
            {
                dgv.Rows.Clear();

                string sql = @"
                    select E.FormaPago, E.Fecha as FechaPago , R.*, D.Nombre,
                           (R.ClaveDocumento + '-' + convert(varchar, R.Consecutivo)) as Documento,
                           R.Total, 0.00 as Descuento1, (E.Pago+0.00) as Pago, R.Saldo,
                           E.FolioGeneral as FolioCobro, 0.00 as RecargoCobro, E.SaldoRestante,
                           E.Folio as FolioD, 'Pago' as Tipo, E.TipoConcepto
                    from Remision as R, Documento as D, Cobros as E
                    where R.ClaveProveedor=@Matricula and R.ClaveDocumento=D.Clave
                          and E.TipoConcepto='Remision' and E.ConceptoId=R.Folio
                          and R.ClaveProveedor=E.IdCliente and E.FormaPago is not null
                    order by E.Folio, E.FolioGeneral asc";
                /* Se preserva tal cual el UNION comentado del original,
                   correspondiente a AnticipoCobros, por si se reactiva:
                union select 'ANTICIPO APLICADO' FormaPago, E1.Fecha as FechaPago , R.*, D.Nombre,
                       (R.ClaveDocumento + '-' + convert(varchar, R.Consecutivo)) as Documento,
                       R.Total, convert(decimal,'0.00') as Descuento1, (E1.Pago) as Pago, R.Saldo,
                       E1.FolioGeneral as FolioCobro, convert(decimal,'0.00') as RecargoCobro,
                       SaldoRestante, E1.Anticipo as FolioD, 'Anticipo' as Tipo
                from Recibo as R, Documento as D, AnticipoCobros as E1
                where R.ClavePropietario=@Matricula and R.ClaveDocumento=D.Clave and R.Folio=E1.Folio
                      and R.ClavePropietario=E1.ClavePropietario */

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", Matricula);

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["FormaPago"].ToString();
                        dgv.Rows[n].Cells[1].Value = Convert.ToDateTime(item["FechaPago"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[2].Value = item["FolioD"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Documento"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[5].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[6].Value = Convert.ToDateTime(item["FechaVence"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[7].Value = Utilerias.FormatearMiles(item["Total"].ToString());
                        dgv.Rows[n].Cells[8].Value = Utilerias.FormatearMiles(item["RecargoCobro"].ToString());
                        dgv.Rows[n].Cells[9].Value = Utilerias.FormatearMiles(item["Descuento1"].ToString());
                        dgv.Rows[n].Cells[10].Value = Utilerias.FormatearMiles(item["Pago"].ToString());
                        dgv.Rows[n].Cells[11].Value = Utilerias.FormatearMiles(item["Saldo"].ToString());
                        dgv.Rows[n].Cells[12].Value = Utilerias.FormatearMiles(item["SaldoRestante"].ToString());
                        dgv.Rows[n].Cells[13].Value = item["FolioCobro"].ToString();
                        dgv.Rows[n].Cells[14].Value = item["Tipo"].ToString();
                        dgv.Rows[n].Cells[15].Value = item["TipoConcepto"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        //___________________________________________________________________________________---
        public void InsertarCobro(string ConceptoId, string MatriculaAlumno, string Fecha, string Observaciones, string FormaPago, decimal Pago, string referencia, string NumOperacion, string NumAutorizacion, string Cuenta, string FolioGeneral, decimal Recargo, decimal DescuentoPago, decimal Saldo, string Tipo, int ConceptoCobroPago)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    @"insert into Cobros (IdCliente, Fecha, Observaciones, FormaPago, Pago, referencia,
                                           NumOperacion, NumAutorizacion, CuentaBancaria, FolioGeneral, Recargo,
                                           DescuentoPago, SaldoRestante, TipoConcepto, ConceptoId, ConceptoCobroPago)
                      values (@IdCliente, @Fecha, @Observaciones, @FormaPago, @Pago, @Referencia,
                              @NumOperacion, @NumAutorizacion, @Cuenta, @FolioGeneral, @Recargo,
                              @DescuentoPago, @Saldo, @Tipo, @ConceptoId, @ConceptoCobroPago)", cn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", MatriculaAlumno);
                    cmd.Parameters.AddWithValue("@Fecha", Fecha);
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                    cmd.Parameters.AddWithValue("@FormaPago", FormaPago);
                    cmd.Parameters.AddWithValue("@Pago", Pago);
                    cmd.Parameters.AddWithValue("@Referencia", referencia);
                    cmd.Parameters.AddWithValue("@NumOperacion", NumOperacion);
                    cmd.Parameters.AddWithValue("@NumAutorizacion", NumAutorizacion);
                    cmd.Parameters.AddWithValue("@Cuenta", Cuenta);
                    cmd.Parameters.AddWithValue("@FolioGeneral", FolioGeneral);
                    cmd.Parameters.AddWithValue("@Recargo", Recargo);
                    cmd.Parameters.AddWithValue("@DescuentoPago", DescuentoPago);
                    cmd.Parameters.AddWithValue("@Saldo", Saldo);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    cmd.Parameters.AddWithValue("@ConceptoId", ConceptoId);
                    cmd.Parameters.AddWithValue("@ConceptoCobroPago", ConceptoCobroPago);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //___________________________________________________________________________________---
        // Se preserva el algoritmo original (MAX(Folio)+1 manual) tal cual;
        // Cobro_General no forma parte del cambio solicitado sobre Cobros.
        public void InsertarCobroGeneral(decimal Importe, TextBox folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    int folioNuevo = 0;

                    using (SqlCommand cmd = new SqlCommand("select max(Folio) from Cobro_General", cn))
                    {
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            folioNuevo = Convert.ToInt32(resultado);
                        }
                    }

                    folioNuevo++;
                    folio.Text = folioNuevo.ToString();

                    bool existe;
                    using (SqlCommand cmd = new SqlCommand("select count(*) from Cobro_General where Folio=@Folio", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", folioNuevo);
                        existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "Insert into Cobro_General (Folio, Importe) values (@Folio, @Importe)", cn))
                        {
                            cmd.Parameters.AddWithValue("@Folio", folioNuevo);
                            cmd.Parameters.AddWithValue("@Importe", Importe);
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

        //_______________________________________________________________________________________________
        public void SeleccionarCuentaBancaria(ComboBox cb)
        {
            cb.Items.Clear();
            using (SqlConnection cn = AbrirConexion())
            using (SqlCommand cmd = new SqlCommand("Select (Nombre + ' - '+ Cuenta) as Cuenta from CuentasBancarias", cn))
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    cb.Items.Add(dr[0].ToString());
                }
            }
        }

        //_________________________________________________________________________________________
        public string[] InformacionCuenta(string Documento)
        {
            string[] resultado = null;
            using (SqlConnection cn = AbrirConexion())
            using (SqlCommand cmd = new SqlCommand(
                "Select Clave from CuentasBancarias where (Nombre + ' - '+ Cuenta) = @Documento", cn))
            {
                cmd.Parameters.AddWithValue("@Documento", Documento);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] valores = { dr[0].ToString() };
                        resultado = valores;
                    }
                }
            }
            return resultado;
        }

        public void ModificarExtension(string Folio, string Extension)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    bool existe;
                    using (SqlCommand cmd = new SqlCommand("select count(*) from Cobros where FolioGeneral=@Folio", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", Folio);
                        existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }

                    if (existe)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "Update Cobros set Extension=@Extension where FolioGeneral=@Folio", cn))
                        {
                            cmd.Parameters.AddWithValue("@Extension", Extension);
                            cmd.Parameters.AddWithValue("@Folio", Folio);
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

        public void ActualizarArchivoCobro(string Folio, string Archivo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "Update Cobros set Archivo=@Archivo where FolioGeneral=@Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Archivo", Archivo);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }
    }
}