using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Inventario
{
    /// <summary>
    /// Capa de datos a nivel de ENCABEZADO del módulo de Inventario
    /// (MovimientoInventario) y orquestación completa de un movimiento
    /// (encabezado + sus partidas + el impacto en existencias). Las
    /// partidas en sí (PartidasMovimientoInventario + AlmacenProducto/
    /// ProductosServicios.ExActual) las resuelve DBPartidas; esta clase la
    /// usa internamente.
    ///
    /// AJUSTE (homologación Remisión/Facturas, soporte Producto-Servicio):
    ///
    ///   1) RegistroMovimientoInventario tenía dos copias distintas regadas
    ///      por el proyecto (una más corta aquí, otra más completa pegada
    ///      directamente en la clase que usa Remisión) y la de aquí tenía
    ///      dos bugs: calculaba el siguiente "Consecutivo" leyendo
    ///      TipoMovimiento.UltimoFolio pero nunca lo volvía a guardar (el
    ///      siguiente movimiento del mismo Documento repetiría el mismo
    ///      consecutivo), y el folio del encabezado (MAX(Folio)+1) se
    ///      calculaba sin ningún bloqueo, con riesgo de que dos
    ///      confirmaciones simultáneas generaran el mismo Folio. Se dejó
    ///      UNA sola implementación real (la de 19 parámetros, con
    ///      transacción + TABLOCKX/HOLDLOCK igual que ya usan
    ///      DBFacturas.InsertarFactura/DBCotizaciones.InsertarCotizacion, y
    ///      que sí persiste el nuevo UltimoFolio) y la firma antigua (15
    ///      parámetros) quedó como sobrecarga de compatibilidad que
    ///      delega en ella, por si algún formulario que no vimos todavía
    ///      la llama así.
    ///
    ///   2) ValidarDocumentoEPR (sólo para 'E'/'EPR') se generalizó a
    ///      ValidarDocumentoMovimiento(tipoMovimiento, documento,
    ///      nombreDescriptivo), y se agregaron ValidarDocumentoSPR
    ///      ('S'/'SPR', ya la usaba Remisión) y ValidarDocumentoSPF
    ///      ('S'/'SPF', nueva, para Facturas) como envoltorios de una línea
    ///      sobre el mismo método genérico -nada de copiar el bloque
    ///      IF NOT EXISTS/INSERT tres veces-.
    ///
    ///   3) ActualizarMovimientoJ (la que recalculaba Subtotal/Impuestos/
    ///      Total/TotalPartidas del encabezado a partir de sus partidas y
    ///      lo bloqueaba) se trae aquí como ActualizarMovimientoTotales,
    ///      quitando un JOIN que no se usaba para nada (la query ya hacía
    ///      todo con subconsultas) y cambiando CAST por TRY_CAST en
    ///      Descuento/IVA (son varchar en la tabla; TRY_CAST no truena si
    ///      algún valor viejo no es numérico).
    ///
    ///   4) GenerarSalidaAlmacen es NUEVO: es la única implementación del
    ///      proceso completo de "dar salida de almacén" (crear el
    ///      encabezado, recorrer las partidas afectando existencias y
    ///      registrándolas, y cerrar/recalcular el encabezado). Facturas
    ///      la usa para las partidas de tipo 'Producto' con
    ///      ProductosServicios.Inventariable = 'Si' (ver
    ///      DBFacturas.ObtenerPartidasInventariables); Remisión debería
    ///      migrar su método privado "IngresarAlmacen" para llamar a este
    ///      método en vez de mantener su propia copia del mismo bucle
    ///      (hoy esa copia vive pegada en OrdenPedidoCliente.cs/el c./o.
    ///      que use Remisión).
    /// </summary>
    class DBRegistrarEntradas
    {
        public static int Folio = 0;
        public static int Eliminado = 0;

        // Capa de partidas: esta clase orquesta el encabezado y delega en
        // DBPartidas todo lo que es por-renglón (existencias + la fila en
        // PartidasMovimientoInventario).
        private readonly DBPartidas partidasDb = new DBPartidas();

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

        #region Catálogo TipoMovimiento (alta bajo demanda)

        /// <summary>
        /// Da de alta en TipoMovimiento la combinación (tipoMovimiento,
        /// documento) si todavía no existe, con "nombreDescriptivo" como
        /// Descripcion del catálogo (lo que se ve en los combos de
        /// documento). Reemplaza el patrón que antes se repetía copiado
        /// (ValidarDocumentoEPR, ValidarDocumentoSPR...) con el mismo
        /// bloque IF NOT EXISTS/INSERT cambiando sólo las constantes.
        /// </summary>
        public void ValidarDocumentoMovimiento(string tipoMovimiento, string documento, string nombreDescriptivo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "IF NOT EXISTS (SELECT 1 FROM TipoMovimiento WHERE Documento = @Documento AND TipoMovimiento = @TipoMovimiento) " +
                        "BEGIN " +
                        "    INSERT INTO TipoMovimiento (TipoMovimiento, Documento, Descripcion, Estatus, UltimoFolio, Bloquear, AfectaCosto, Almacen, Notas) " +
                        "    VALUES (@TipoMovimiento, @Documento, @NombreDescriptivo, 'Activo', 0, 'Si', 'Si', '', '') " +
                        "END", cn))
                    {
                        cmd.Parameters.AddWithValue("@TipoMovimiento", tipoMovimiento);
                        cmd.Parameters.AddWithValue("@Documento", documento);
                        cmd.Parameters.AddWithValue("@NombreDescriptivo", nombreDescriptivo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>Entrada por recepción ('E'/'EPR'). Se conserva el nombre original para no romper llamadas existentes.</summary>
        public void ValidarDocumentoEPR() => ValidarDocumentoMovimiento("E", "EPR", "ENTRADA POR RECEPCIÓN");

        /// <summary>Salida por remisión ('S'/'SPR'), la que ya usa el módulo de Remisión.</summary>
        public void ValidarDocumentoSPR() => ValidarDocumentoMovimiento("S", "SPR", "SALIDA POR REMISIÓN");

        /// <summary>Salida por factura ('S'/'SPF'), nueva: la usa Facturas al confirmar.</summary>
        public void ValidarDocumentoSPF() => ValidarDocumentoMovimiento("S", "SPF", "SALIDA POR FACTURA");

        #endregion

        //_________________________________________________________________________________________________________________________--
        // registrar forma Movimiento
        // Sobrecarga de COMPATIBILIDAD: firma original (15 parámetros, sin
        // CentroCosto/Propietario/OrdenTrabajo/CajaR). Delega en la versión
        // completa de abajo pasando esos 4 últimos vacíos. Úsala sólo si ya
        // tienes código llamándola así; para código nuevo usa la de 19
        // parámetros (o, mejor aún, GenerarSalidaAlmacen).
        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion, string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas, string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado, TextBox FOlioP, string txtAlmacenSalida)
        {
            return RegistroMovimientoInventario(txtFolio, txtTipoDocumento, cmbDescripcion, dtpFecha, cmbEstatus,
                txtReferencias, txtAlmacen, txtTotalPartidas, cmbDivisa, txtTipoCambio, txtTotal, txtNotas,
                txtElaborado, FOlioP, txtAlmacenSalida, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Da de alta el encabezado de un MovimientoInventario (Entrada,
        /// Salida o Traslado, según txtTipoDocumento = 'E'/'S'/'T') y
        /// regresa el Folio generado (también se escribe en FOlioP.Text por
        /// compatibilidad con el código que ya lo leía así).
        ///
        /// AJUSTES respecto a la versión anterior:
        ///   - El Folio (MAX(Folio)+1) y el Consecutivo por Documento
        ///     (TipoMovimiento.UltimoFolio+1) se calculan e insertan dentro
        ///     de UNA transacción con TABLOCKX/HOLDLOCK sobre
        ///     MovimientoInventario, igual que ya hace
        ///     DBFacturas.InsertarFactura, para que dos confirmaciones
        ///     simultáneas no puedan terminar con el mismo Folio.
        ///   - El nuevo Consecutivo SÍ se guarda de vuelta en
        ///     TipoMovimiento.UltimoFolio (antes se calculaba y nunca se
        ///     persistía: el siguiente movimiento del mismo Documento
        ///     repetía el mismo consecutivo).
        ///   - El INSERT quedó parametrizado.
        ///
        /// "txtCentroCosto", "propietario", "ordenTrabajo" y "cajaR" se
        /// reciben por compatibilidad con el llamado ya existente en
        /// Remisión, pero HOY no se escriben a ninguna columna: la tabla
        /// MovimientoInventario no tiene esas columnas todavía. Si llegan a
        /// agregarse, éste es el único lugar que hay que tocar.
        /// </summary>
        public string RegistroMovimientoInventario(string txtFolio, string txtTipoDocumento, string cmbDescripcion,
            string dtpFecha, string cmbEstatus, string txtReferencias, string txtAlmacen, string txtTotalPartidas,
            string cmbDivisa, string txtTipoCambio, string txtTotal, string txtNotas, string txtElaborado,
            TextBox FOlioP, string txtAlmacenSalida, string txtCentroCosto, string propietario, string ordenTrabajo, string cajaR)
        {
            string folioGenerado = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                    using (SqlTransaction tx = cn.BeginTransaction())
                    {
                        try
                        {
                            int folioM;
                            using (SqlCommand cmdFolio = new SqlCommand(
                                "SELECT ISNULL(MAX(Folio), 0) + 1 FROM MovimientoInventario WITH (TABLOCKX, HOLDLOCK)", cn, tx))
                            {
                                folioM = (int)cmdFolio.ExecuteScalar();
                            }

                            int consecutivo = 1;
                            using (SqlCommand cmdCons = new SqlCommand(
                                "SELECT UltimoFolio FROM TipoMovimiento WHERE TipoMovimiento = @Tipo AND Documento = @Documento", cn, tx))
                            {
                                cmdCons.Parameters.AddWithValue("@Tipo", txtTipoDocumento);
                                cmdCons.Parameters.AddWithValue("@Documento", cmbDescripcion);
                                object resultado = cmdCons.ExecuteScalar();
                                if (resultado != null && resultado != DBNull.Value)
                                {
                                    consecutivo = Convert.ToInt32(resultado) + 1;
                                }
                            }

                            const string sqlInsert = @"
                                INSERT INTO MovimientoInventario
                                    (Folio, TipoDocumento, Descripcion, Fecha, Estatus, Referencias, Almacen, TotalPartidas,
                                     Divisa, TipoCambio, Total, Notas, Elaborado, Consecutivo, AlmacenSalida)
                                VALUES
                                    (@Folio, @TipoDocumento, @Descripcion, @Fecha, @Estatus, @Referencias, @Almacen, @TotalPartidas,
                                     @Divisa, @TipoCambio, @Total, @Notas, @Elaborado, @Consecutivo, @AlmacenSalida)";

                            using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, cn, tx))
                            {
                                cmdInsert.Parameters.AddWithValue("@Folio", folioM);
                                cmdInsert.Parameters.AddWithValue("@TipoDocumento", txtTipoDocumento);
                                cmdInsert.Parameters.AddWithValue("@Descripcion", cmbDescripcion);
                                cmdInsert.Parameters.AddWithValue("@Fecha", string.IsNullOrWhiteSpace(dtpFecha) ? (object)DBNull.Value : Convert.ToDateTime(dtpFecha));
                                cmdInsert.Parameters.AddWithValue("@Estatus", (object)cmbEstatus ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Referencias", (object)txtReferencias ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Almacen", (object)txtAlmacen ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@TotalPartidas", string.IsNullOrWhiteSpace(txtTotalPartidas) ? 0 : Convert.ToInt32(txtTotalPartidas));
                                cmdInsert.Parameters.AddWithValue("@Divisa", (object)cmbDivisa ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@TipoCambio", string.IsNullOrWhiteSpace(txtTipoCambio) ? 1.00m : Convert.ToDecimal(txtTipoCambio));
                                cmdInsert.Parameters.AddWithValue("@Total", string.IsNullOrWhiteSpace(txtTotal) ? 0m : Convert.ToDecimal(txtTotal));
                                cmdInsert.Parameters.AddWithValue("@Notas", (object)txtNotas ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Elaborado", (object)txtElaborado ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Consecutivo", consecutivo);
                                cmdInsert.Parameters.AddWithValue("@AlmacenSalida", string.IsNullOrWhiteSpace(txtAlmacenSalida) ? (object)DBNull.Value : txtAlmacenSalida);

                                cmdInsert.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdUpdateTipo = new SqlCommand(
                                "UPDATE TipoMovimiento SET UltimoFolio = @Consecutivo WHERE TipoMovimiento = @Tipo AND Documento = @Documento", cn, tx))
                            {
                                cmdUpdateTipo.Parameters.AddWithValue("@Consecutivo", consecutivo);
                                cmdUpdateTipo.Parameters.AddWithValue("@Tipo", txtTipoDocumento);
                                cmdUpdateTipo.Parameters.AddWithValue("@Documento", cmbDescripcion);
                                cmdUpdateTipo.ExecuteNonQuery();
                            }

                            tx.Commit();

                            folioGenerado = folioM.ToString();
                            if (FOlioP != null)
                            {
                                FOlioP.Text = folioGenerado;
                            }
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

            return folioGenerado;
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

        #region Cierre del movimiento (totales + bloqueo)

        /// <summary>
        /// Recalcula Subtotal/Impuestos("IVA")/Total/TotalPartidas de un
        /// MovimientoInventario a partir de sus PartidasMovimientoInventario
        /// y lo deja en Estatus = 'Bloqueado'. Reemplaza/generaliza a
        /// "ActualizarMovimientoJ" (la que ya usaba Remisión): mismo
        /// cálculo, pero sin el JOIN a PartidasMovimientoInventario que no
        /// se llegaba a usar (toda la query ya resolvía con subconsultas
        /// propias), y con TRY_CAST en vez de CAST para Descuento/IVA
        /// (son varchar en la tabla; TRY_CAST no truena si algún valor
        /// viejo no es numérico, sólo lo cuenta como 0).
        /// </summary>
        public void ActualizarMovimientoTotales(string folio, string tipoMovimiento, string documento)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE R SET
                        Estatus = 'Bloqueado',
                        R.Descuento = (
                            SELECT COALESCE(SUM(TRY_CAST(PP.Descuento AS DECIMAL(18,2))), 0.00)
                            FROM PartidasMovimientoInventario AS PP
                            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
                        ),
                        R.Impuestos = (
                            SELECT COALESCE(SUM(TRY_CAST(PP.IVA AS DECIMAL(18,2))), 0.00)
                            FROM PartidasMovimientoInventario AS PP
                            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
                        ),
                        R.Total = (
                            SELECT COALESCE(SUM(PP.Total), 0.00)
                            FROM PartidasMovimientoInventario AS PP
                            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
                        ),
                        R.Subtotal = (
                            SELECT COALESCE(SUM(PP.Subtotal), 0.00)
                            FROM PartidasMovimientoInventario AS PP
                            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
                        ),
                        R.TotalPartidas = (
                            SELECT COALESCE(COUNT(*), 0)
                            FROM PartidasMovimientoInventario AS PP
                            WHERE R.Folio = PP.FolioMovimiento AND R.Descripcion = PP.Descripcion
                        )
                    FROM MovimientoInventario AS R
                    WHERE R.Folio = @Folio AND R.TipoDocumento = @TipoMovimiento AND R.Descripcion = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@TipoMovimiento", tipoMovimiento);
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRORac" + ex.ToString());
            }
        }

        /// <summary>Alias con el nombre original, por si algo más lo sigue llamando así.</summary>
        public void ActualizarMovimientoJ(string txtFolio, string txtTipoMovimiento, string txtDocumento, string txtPartidas)
            => ActualizarMovimientoTotales(txtFolio, txtTipoMovimiento, txtDocumento);

        #endregion

        #region Proceso completo de salida de almacén (homologado)

        /// <summary>
        /// Genera un movimiento de salida de almacén (TipoDocumento = 'S')
        /// completo: valida/da de alta el TipoMovimiento si hace falta, crea
        /// el encabezado, recorre las partidas descontando existencia y
        /// registrándolas, y cierra/recalcula el encabezado. Es la ÚNICA
        /// implementación de este proceso: cualquier módulo que necesite dar
        /// salida de almacén (Facturas, Remisión, lo que siga) debe llamar
        /// este método en vez de reimplementar su propio bucle de
        /// RegistroProductoSalidas + RegistroPartida.
        ///
        /// "partidas" debe venir YA FILTRADA por quien llama a lo que
        /// realmente deba mover inventario -por ejemplo,
        /// DBFacturas.ObtenerPartidasInventariables ya filtra sólo las
        /// partidas de tipo 'Producto' con ProductosServicios.Inventariable
        /// = 'Si'-. Formato esperado de cada fila (mismo que ya usan
        /// DBFacturas.ObtenerPartidas/ObtenerPartidasInventariables):
        ///   [0] ClaveProducto, [1] Cantidad, [2] Costeo (no se usa aquí),
        ///   [3] Precio, [4] Partida de origen (no se usa, se renumera
        ///   1..N para el movimiento), [5] Unidad, [6] Total.
        ///
        /// Regresa el Folio del MovimientoInventario generado, o
        /// string.Empty si "partidas" viene vacía (no se genera nada: no
        /// tiene caso crear un movimiento sin renglones).
        /// </summary>
        public string GenerarSalidaAlmacen(List<List<string>> partidas, string almacen, string documento,
            string nombreDescriptivo, string fecha, string estatus, string divisa, string tipoCambio, string total,
            string notas, string elaborado, string referencia = "")
        {
            if (partidas == null || partidas.Count == 0)
                return string.Empty;

            ValidarDocumentoMovimiento("S", documento, nombreDescriptivo);

            TextBox folioHolder = new TextBox();
            string folio = RegistroMovimientoInventario(string.Empty, "S", documento, fecha, estatus, referencia,
                almacen, partidas.Count.ToString(), divisa, tipoCambio, total, notas, elaborado, folioHolder,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            if (string.IsNullOrEmpty(folio))
                return string.Empty;

            for (int i = 0; i < partidas.Count; i++)
            {
                string clave = partidas[i][0];
                string cantidad = partidas[i][1].Replace(",", "");
                string precio = partidas[i][3].Replace(",", "");
                string unidad = partidas[i][5];
                string totalPartida = partidas[i][6].Replace(",", "");

                partidasDb.RegistroProductoSalidas(clave, cantidad, almacen);
                partidasDb.RegistroPartida(folio, "S", documento, (i + 1).ToString(), clave, cantidad, unidad,
                    Convert.ToDecimal(precio), divisa, tipoCambio, Convert.ToDecimal(totalPartida), string.Empty);
            }

            ActualizarMovimientoTotales(folio, "S", documento);

            return folio;
        }

        #endregion

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