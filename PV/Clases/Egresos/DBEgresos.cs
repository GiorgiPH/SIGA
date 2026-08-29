using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Clases;
using PV.Properties;

namespace PV.Clases.Egresos
{
    /// <summary>
    /// Acceso a datos para el registro de Egresos (pagos a Proveedores):
    /// RecepcionProducto, RegistroGastos, RegistroReembolso y NotasGasto.
    ///
    /// Antes esta lógica vivía dentro de DBRegistrarIngresos junto con los
    /// métodos de Cobros/Ingresos. Se separó a esta clase para que cada una
    /// represente un solo dominio (Ingresos vs. Egresos). Cada método abre y
    /// cierra su propia conexión (using), igual que en la clase original.
    ///
    /// Recargo y DescuentoPago YA NO se capturan desde la interfaz (las
    /// columnas correspondientes se quitaron del DataGridView de
    /// RegistroEgreso y de RegistrarCobroEgreso). Por eso ActualizarEgreso2
    /// se renombró a ActualizarSaldoEgreso y ya no recibe esos parámetros.
    /// Las columnas Recargo/DescuentoPago se dejaron intactas en las tablas
    /// y en el SELECT de ObtenerEgresos/CargarPagosEgreso2 porque pueden
    /// tener valores históricos de antes del cambio; si ya no se necesitan
    /// en ningún lado, se pueden retirar del SELECT en un siguiente ajuste.
    /// </summary>
    class DBEgresos
    {
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

        //____________________________________________________________________
        /// <param name="claveCentroCostos">
        /// Clave de Centro de Costos para filtrar (int como string). Si viene
        /// null, vacio, o "0" (fila "TODOS" del combo), no se filtra y se
        /// traen los egresos de todos los centros de costos.
        ///
        /// OJO: RecepcionProducto ('P') y NotasGasto ('NCG') no tienen columna
        /// CentroCostos propia (el SELECT ya las regresaba como NULL antes de
        /// este cambio). Por eso, cuando SÍ se elige un Centro de Costos
        /// especifico, esas dos ramas quedan excluidas del resultado — no hay
        /// forma de saber a que centro de costos pertenecen. Solo aparecen
        /// cuando el filtro esta en "TODOS" (@CentroCostos IS NULL).
        /// </param>
        public DataTable ObtenerEgresos(string matricula, DateTime? vencimiento = null,
        bool usarFechas = false, DateTime? fecha1 = null, DateTime? fecha2 = null, string claveCentroCostos = null)
        {
            DataTable dtEgresos = new DataTable();
            try
            {
                string sql = @"
            (SELECT 
                'P' as Tipo, 
                R.Folio, 
                R.ClaveDocumento, 
                R.Estatus, 
                R.Fecha, 
                R.ClaveProveedor, 
                R.Divisa, 
                R.TipoCambio, 
                R.Subtotal, 
                R.Descuento, 
                R.Cargo, 
                R.Total, 
                R.TotalPartidas, 
                R.Notas, 
                R.Elaborado, 
                isnull(R.Recargo, 0.00) as Recargo, 
                isnull(R.DescuentoPago, 0.00) as DescuentoPago, 
                isnull(R.Saldo,0.00) as Saldo, 
                R.FolioOrden, 
                R.Consecutivo, 
                R.Almacen, 
                R.Referencia, 
                R.Condominio, 
                R.Extension, 
                R.Archivo, 
                R.DiasVence, 
                R.FechaVence,
                NULL as CentroCostos,
                NULL as Semana,
                NULL as Anio,
                NULL as ProveedorAlterno,
                D.Nombre,
                P.RazonSocial as NombreProveedor
            FROM 
                RecepcionProducto as R, 
                Documento as D,
                Proveedor as P
            WHERE 
                (@Matricula IS NULL OR @Matricula = '' OR R.ClaveProveedor = @Matricula) AND 
                (@Vencimiento IS NULL OR R.FechaVence < @Vencimiento) AND
                (@UsarFechas = 0 OR R.Fecha BETWEEN @Fecha1 AND @Fecha2) AND
                R.Saldo != 0 AND 
                R.Estatus = 'Bloqueado' AND
                R.ClaveDocumento = D.Clave AND
                P.IdProveedor = R.ClaveProveedor AND
                (@CentroCostos IS NULL))
 
            UNION 
 
            (SELECT 
                'G' as Tipo, 
                R.Folio, 
                R.ClaveDocumento, 
                R.Estatus, 
                R.Fecha, 
                R.ClaveProveedor, 
                R.Divisa, 
                R.TipoCambio, 
                R.Subtotal, 
                R.Descuento, 
                R.Cargo, 
                R.Total, 
                R.TotalPartidas, 
                R.Notas, 
                R.Elaborado, 
                isnull(R.Recargo, 0.00) as Recargo, 
                isnull(R.DescuentoPago, 0.00) as DescuentoPago, 
                isnull(R.Saldo,0.00) as Saldo, 
                R.FolioOrden, 
                R.Consecutivo, 
                R.Almacen, 
                R.Referencia, 
                R.Condominio, 
                R.Extension, 
                R.Archivo, 
                R.DiasVence, 
                R.FechaVence,
                R.CentroCostos,
                R.Semana,
                R.Anio,
                R.ProveedorAlterno,
                D.Nombre,
                P.RazonSocial as NombreProveedor
            FROM 
                RegistroGastos as R, 
                Documento as D,
                Proveedor as P
            WHERE 
                (@Matricula IS NULL OR @Matricula = '' OR R.ClaveProveedor = @Matricula) AND 
                (@Vencimiento IS NULL OR R.FechaVence < @Vencimiento) AND
                (@UsarFechas = 0 OR R.Fecha BETWEEN @Fecha1 AND @Fecha2) AND
                R.Saldo != 0 AND 
                R.Estatus = 'Bloqueado' AND
                R.ClaveDocumento = D.Clave AND
                P.IdProveedor = R.ClaveProveedor AND
                (@CentroCostos IS NULL OR R.CentroCostos = @CentroCostos))
 
            UNION 
 
            (SELECT 
                'RR' as Tipo, 
                R.Folio, 
                R.ClaveDocumento, 
                R.Estatus, 
                R.Fecha, 
                R.ClaveProveedor, 
                R.Divisa, 
                R.TipoCambio, 
                R.Subtotal, 
                R.Descuento, 
                R.Cargo, 
                R.Total, 
                R.TotalPartidas, 
                R.Notas, 
                R.Elaborado, 
                0.00 as Recargo,        -- RegistroReembolso no tiene columna Recargo
                0.00 as DescuentoPago,  -- RegistroReembolso no tiene columna DescuentoPago
                isnull(R.Saldo,0.00) as Saldo,  
                R.FolioOrden, 
                R.Consecutivo, 
                NULL as Almacen,        -- RegistroReembolso no tiene columna Almacen
                R.Referencia, 
                NULL as Condominio,     -- RegistroReembolso no tiene columna Condominio
                NULL as Extension,      -- RegistroReembolso no tiene columna Extension
                R.Archivo, 
                R.DiasVence, 
                R.FechaVence,
                R.CentroCostos,
                R.Semana,
                R.Anio,
                R.ProveedorAlterno,
                D.Nombre,
                P.RazonSocial as NombreProveedor
            FROM 
                RegistroReembolso as R, 
                Documento as D,
                Proveedor as P
            WHERE 
                (@Matricula IS NULL OR @Matricula = '' OR R.ClaveProveedor = @Matricula) AND 
                (@Vencimiento IS NULL OR TRY_CONVERT(date, R.FechaVence) < @Vencimiento) AND
                (@UsarFechas = 0 OR TRY_CONVERT(date, R.Fecha) BETWEEN @Fecha1 AND @Fecha2) AND
                R.Saldo != 0 AND 
                R.Estatus = 'Bloqueado' AND
                R.ClaveDocumento = D.Clave AND
                P.IdProveedor = R.ClaveProveedor AND
                (@CentroCostos IS NULL OR R.CentroCostos = @CentroCostos))
 
            UNION 
 
            (SELECT 
                'NCG' as Tipo, 
                R.Folio, 
                R.ClaveDocumento, 
                R.Estatus, 
                R.Fecha, 
                R.ClaveProveedor, 
                R.Divisa, 
                R.TipoCambio, 
                R.Subtotal, 
                R.Descuento, 
                R.Cargo, 
                R.Total, 
                R.TotalPartidas, 
                R.Notas, 
                R.Elaborado, 
                isnull(R.Recargo, 0.00) as Recargo, 
                isnull(R.DescuentoPago, 0.00) as DescuentoPago, 
                isnull(R.Saldo,0.00) as Saldo,  
                R.FolioOrden, 
                R.Consecutivo, 
                R.Almacen, 
                R.Referencia, 
                R.Condominio, 
                R.Extension, 
                R.Archivo, 
                NULL as DiasVence, 
                NULL as FechaVence,
                NULL as CentroCostos,
                NULL as Semana,
                NULL as Anio,
                NULL as ProveedorAlterno,
                D.Nombre,
                P.RazonSocial as NombreProveedor
            FROM 
                NotasGasto as R, 
                Documento as D,
                Proveedor as P
            WHERE 
                (@Matricula IS NULL OR @Matricula = '' OR R.ClaveProveedor = @Matricula) AND 
                (@UsarFechas = 0 OR R.Fecha BETWEEN @Fecha1 AND @Fecha2) AND
                R.Saldo != 0 AND 
                R.Estatus = 'Bloqueado' AND
                R.ClaveDocumento = D.Clave AND
                P.IdProveedor = R.ClaveProveedor AND
                (@CentroCostos IS NULL))";

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    // Todos opcionales: si vienen null (o "" en el caso de
                    // matricula), ese filtro no se aplica.
                    cmd.Parameters.AddWithValue("@Matricula", (object)matricula ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Vencimiento", (object)vencimiento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UsarFechas", usarFechas);
                    cmd.Parameters.AddWithValue("@Fecha1", (object)fecha1 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fecha2", (object)fecha2 ?? DBNull.Value);

                    object valorCentroCostos = (string.IsNullOrWhiteSpace(claveCentroCostos) || claveCentroCostos == "0")
                        ? (object)DBNull.Value
                        : Convert.ToInt32(claveCentroCostos);
                    cmd.Parameters.AddWithValue("@CentroCostos", valorCentroCostos);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtEgresos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener egresos: " + ex.Message);
            }
            return dtEgresos;
        }


        //_________________________________________________________________________________
        // Antes "ActualizarEgreso2(Tipo, Folio, Recargos, Descuento, Saldo)".
        // Como ya no se capturan recargos ni descuentos por documento, ahora
        // solo actualiza el Saldo pendiente del documento seleccionado.
        //
        // OJO: este método solo se llamaba antes desde RegistroEgreso.button5_Click
        // justo antes de abrir RegistrarCobroEgreso, para "congelar" el saldo con
        // el recargo/descuento capturado. Esa llamada ya se retiró de
        // RegistroEgreso.cs porque no aplica. Falta confirmar si
        // RegistrarCobroEgreso.cs también lo usa (por ejemplo al aplicar el
        // abono) -- si no lo usa en ningún lado, este método queda como código
        // muerto y se puede eliminar.
        public void ActualizarSaldoEgreso(string Tipo, string Folio, decimal Saldo)
        {
            try
            {
                string tabla = TablaPorTipo(Tipo);
                if (tabla == null)
                {
                    return;
                }

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    $"Update {tabla} set Saldo=@Saldo where Folio=@Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", Saldo);
                    cmd.Parameters.AddWithValue("@Folio", Folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        //_________________________________________________________________________________
        public string ActualizarCancelarEgreso2(string Clave, string Tipo, string Observaciones, decimal Abono, string Proveedor)
        {
            string mensaje = string.Empty;
            try
            {
                string tablaDocumento = null;
                string mensajeExito = null;

                // NOTA: igual que en el original, este método NO contempla Tipo
                // "RR" (RegistroReembolso); solo P, G y NCG. Se preservó tal cual
                // por si es intencional, pero conviene confirmarlo -- ver también
                // TablaPorTipo() más abajo, que sí incluye "RR" para
                // ActualizarSaldoEgreso.
                if (Tipo == "P")
                {
                    tablaDocumento = "RecepcionProducto";
                    mensajeExito = "Egreso Cancelado";
                }
                else if (Tipo == "G")
                {
                    tablaDocumento = "RegistroGastos";
                    mensajeExito = "Egreso Cancelado";
                }
                else if (Tipo == "NCG")
                {
                    tablaDocumento = "NotasGasto";
                    mensajeExito = "Nota de Cargo Cancelada";
                }

                if (tablaDocumento == null)
                {
                    return mensaje;
                }

                using (SqlConnection cn = AbrirConexion())
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "Update Egreso set Observaciones=@Observaciones, Pago=0.00 where Folio=@Folio and Tipo=@Tipo", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                            cmd.Parameters.AddWithValue("@Folio", Clave);
                            cmd.Parameters.AddWithValue("@Tipo", Tipo);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            $"Update {tablaDocumento} set Saldo = Saldo + @Abono where Folio=@Folio", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Abono", Abono);
                            cmd.Parameters.AddWithValue("@Folio", Clave);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "Update Proveedor set Saldo = Saldo + @Abono where IdProveedor=@Proveedor", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Abono", Abono);
                            cmd.Parameters.AddWithValue("@Proveedor", Proveedor);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        mensaje = mensajeExito;
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
        public void CargarPagosEgreso2(DataGridView dgv, string Matricula)
        {
            try
            {
                dgv.Rows.Clear();

                string sql = @"
                    (select E.FormaPago, 'P' as Tipo, R.Folio, R.ClaveDocumento, R.Estatus, R.Fecha, R.ClaveProveedor, R.Divisa, R.TipoCambio, R.Subtotal, R.Descuento, R.Cargo, R.Total, R.TotalPartidas, R.Notas, R.Elaborado, R.Recargo, R.DescuentoPago, R.Saldo, R.FolioOrden, R.Consecutivo, R.Almacen, R.Referencia, R.Condominio, R.Extension, R.Archivo, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E.NumAutorizacion, B.Nombre as Banco
                     from RecepcionProducto as R, Documento as D, Egreso E, CuentasBancarias as B
                     where B.Clave=E.CuentaBancaria and R.ClaveProveedor=@Matricula and R.ClaveDocumento=D.Clave and E.Tipo='P' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null)
                    union
                    (select E.FormaPago, 'G' as Tipo, R.Folio, R.ClaveDocumento, R.Estatus, R.Fecha, R.ClaveProveedor, R.Divisa, R.TipoCambio, R.Subtotal, R.Descuento, R.Cargo, R.Total, R.TotalPartidas, R.Notas, R.Elaborado, R.Recargo, R.DescuentoPago, R.Saldo, R.FolioOrden, R.Consecutivo, R.Almacen, R.Referencia, R.Condominio, R.Extension, R.Archivo, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E.NumAutorizacion, B.Nombre as Banco
                     from RegistroGastos as R, Documento as D, Egreso E, CuentasBancarias as B
                     where B.Clave=E.CuentaBancaria and R.ClaveProveedor=@Matricula and R.ClaveDocumento=D.Clave and E.Tipo='G' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null)
                    union
                    (select E.FormaPago, 'NCG' as Tipo, R.Folio, R.ClaveDocumento, R.Estatus, R.Fecha, R.ClaveProveedor, R.Divisa, R.TipoCambio, R.Subtotal, R.Descuento, R.Cargo, R.Total, R.TotalPartidas, R.Notas, R.Elaborado, R.Recargo, R.DescuentoPago, R.Saldo, R.FolioOrden, R.Consecutivo, R.Almacen, R.Referencia, R.Condominio, R.Extension, R.Archivo, D.Nombre, R.ClaveDocumento, R.Total, E.Pago, R.Saldo, E.NumAutorizacion, B.Nombre as Banco
                     from NotasGasto as R, Documento as D, Egreso E, CuentasBancarias as B
                     where B.Clave=E.CuentaBancaria and R.ClaveProveedor=@Matricula and R.ClaveDocumento=D.Clave and E.Tipo='NCG' and R.Folio=E.Folio and R.ClaveProveedor=E.ClaveProveedor and E.FormaPago is not null)";

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
                        dgv.Rows[n].Cells[1].Value = item["Tipo"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["ClaveDocumento"].ToString();
                        dgv.Rows[n].Cells[4].Value = item["Nombre"].ToString();
                        dgv.Rows[n].Cells[5].Value = item["Banco"].ToString();
                        dgv.Rows[n].Cells[6].Value = item["NumAutorizacion"].ToString();
                        dgv.Rows[n].Cells[7].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                        dgv.Rows[n].Cells[8].Value = Utilerias.FormatearMiles(item["Recargo"].ToString());
                        dgv.Rows[n].Cells[9].Value = Utilerias.FormatearMiles(item["DescuentoPago"].ToString());
                        dgv.Rows[n].Cells[10].Value = Utilerias.FormatearMiles(item["Total"].ToString());
                        dgv.Rows[n].Cells[11].Value = Utilerias.FormatearMiles(item["Pago"].ToString());
                        dgv.Rows[n].Cells[12].Value = Utilerias.FormatearMiles(item["Saldo"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        //___________________________________________________________________________________---
        public void InsertarEgreso(string Tipo, string Folio, string MatriculaAlumno, string Fecha, string Observaciones, string FormaPago, decimal Pago, string referencia, string NumOperacion, string NumAutorizacion, string Cuenta, string FolioGeneral, int ConceptoCobroPago)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            @"insert into Egreso (Tipo, Folio, ClaveProveedor, Fecha, Observaciones, FormaPago, Pago,
                                                   referencia, NumOperacion, NumAutorizacion, CuentaBancaria, FolioGeneral, ConceptoCobroPago)
                              values (@Tipo, @Folio, @ClaveProveedor, @Fecha, @Observaciones, @FormaPago, @Pago,
                                      @Referencia, @NumOperacion, @NumAutorizacion, @Cuenta, @FolioGeneral, @ConceptoCobroPago)", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Tipo", Tipo);
                            cmd.Parameters.AddWithValue("@Folio", Folio);
                            cmd.Parameters.AddWithValue("@ClaveProveedor", MatriculaAlumno);
                            cmd.Parameters.AddWithValue("@Fecha", Fecha);
                            cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                            cmd.Parameters.AddWithValue("@FormaPago", FormaPago);
                            cmd.Parameters.AddWithValue("@Pago", Pago);
                            cmd.Parameters.AddWithValue("@Referencia", referencia);
                            cmd.Parameters.AddWithValue("@NumOperacion", NumOperacion);
                            cmd.Parameters.AddWithValue("@NumAutorizacion", NumAutorizacion);
                            cmd.Parameters.AddWithValue("@Cuenta", Cuenta);
                            cmd.Parameters.AddWithValue("@FolioGeneral", FolioGeneral);
                            cmd.Parameters.AddWithValue("@ConceptoCobroPago", ConceptoCobroPago);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "Delete Egreso where Tipo=@Tipo and Folio=@Folio and ClaveProveedor=@ClaveProveedor and Fecha is null", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Tipo", Tipo);
                            cmd.Parameters.AddWithValue("@Folio", Folio);
                            cmd.Parameters.AddWithValue("@ClaveProveedor", MatriculaAlumno);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
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
        }

        //___________________________________________________________________________________---
        public void InsertarCobroGeneralEgreso(decimal Importe, TextBox folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    int folioNuevo = 0;

                    using (SqlCommand cmd = new SqlCommand("select max(Folio) from Egreso_General", cn))
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
                    using (SqlCommand cmd = new SqlCommand("select count(*) from Egreso_General where Folio=@Folio", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", folioNuevo);
                        existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "Insert into Egreso_General (Folio, Importe) values (@Folio, @Importe)", cn))
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

        //____________________________________________________________________________________________
        public void ActualizarSaldoProveedor(string Clave, decimal Saldo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "Update Proveedor set Saldo = Saldo - @Saldo where IdProveedor=@Clave", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", Saldo);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }

        //_________________________________________________________________________________
        // Usado por ActualizarSaldoEgreso; SÍ incluye "RR" (a diferencia del
        // switch de ActualizarCancelarEgreso2, ver nota arriba).
        private static string TablaPorTipo(string tipo)
        {
            switch (tipo)
            {
                case "P": return "RecepcionProducto";
                case "G": return "RegistroGastos";
                case "NCG": return "NotasGasto";
                case "RR": return "RegistroReembolso";
                default: return null;
            }
        }
    }
}