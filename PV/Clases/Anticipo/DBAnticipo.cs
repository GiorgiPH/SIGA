using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace PV.Clases.Anticipo
{
    /// <summary>
    /// Acceso a datos del módulo de Anticipos (Clientes/Propietarios y Proveedores)
    /// y de su Aplicación contra documentos (Remisión o Factura, o del lado
    /// Proveedores: RecepcionProducto / RegistroGastos / RegistroReembolso /
    /// NotasGasto).
    ///
    /// Historial de cambios relevante:
    ///  - No se mantiene una SqlConnection abierta como campo de instancia; cada
    ///    operación abre su propia conexión con "using" y la libera automáticamente
    ///    (igual con SqlCommand/SqlDataReader/SqlDataAdapter).
    ///  - Todas las consultas usan parámetros (SqlParameter) en vez de concatenar
    ///    texto, eliminando el riesgo de inyección SQL.
    ///  - Se corrigió CultureInfo("US-AR") -> "en-US" en CargarReciboProveedor y
    ///    CargarEgreso2 (esa culture no existe en .NET y lanzaba
    ///    CultureNotFoundException al imprimir recibos de proveedor / cargar egresos).
    ///  - Los métodos "Informacion..." ya no devuelven null cuando no hay
    ///    coincidencias (antes provocaba NullReferenceException en el formulario
    ///    al hacer valores[0]).
    ///  - ClaveProductoSiguiente/2 e InsertarCobroGeneral/Proveedor ya no ejecutan
    ///    la misma consulta dos veces (se usa ExecuteScalar una sola vez).
    ///  - MIGRACIÓN A ConceptoCobroPago: el concepto del Anticipo dejó de
    ///    referenciar "ConceptosIngreso" y ahora referencia "ConceptoCobroPago"
    ///    (Anticipo.Concepto = ConceptoCobroPago.IdConcepto). Todos los JOIN que
    ///    resolvían la descripción del concepto se migraron. La MISMA migración
    ///    queda pendiente de completar en AnticipoProveedor.Concepto — ver
    ///    02_Migracion_AnticipoProveedor.sql (la FK vieja hacia ConceptosIngreso
    ///    ya se retiró; falta agregar la nueva según el tipo real de
    ///    ConceptoCobroPago.IdConcepto).
    ///  - APLICACIÓN DE ANTICIPOS POLIMÓRFICA (lado Clientes): antes, aplicar un
    ///    Anticipo sólo contemplaba un documento destino de tipo Remisión. Ahora
    ///    el destino puede ser Remisión o Factura, por lo que AnticipoCobros
    ///    incorpora una columna discriminadora "TipoDocumento" ('REMISION' |
    ///    'FACTURA'). Como ya no existe una FK física hacia una sola tabla, la
    ///    integridad referencial del documento destino se valida en código (ver
    ///    <see cref="ExisteDocumento"/>), siguiendo el mismo patrón de
    ///    discriminador que ya usaba esta clase en CargarEgreso2/ActualizarEgreso2
    ///    (tipos 'P' / 'G' / 'RR' / 'NCG'). Este patrón resultó además ser
    ///    consistente con uno ya existente en otro módulo del sistema
    ///    (DBFacturas.CargarFacturaCobro para el flujo de RegistrarCobro), que
    ///    combina Remisión y Factura en una misma grilla marcando cada fila con
    ///    <c>DataGridViewRow.Tag</c>. Por eso aquí se siguió el mismo criterio:
    ///    esta clase valida la existencia del documento (<see cref="ExisteDocumento"/>)
    ///    y registra el movimiento (<see cref="InsertarCobro"/>), mientras que
    ///    <c>DBRemiision.ActualizarRemision</c> y
    ///    <c>DBFacturas.ActualizarFacturaAbono</c> —ya existentes— siguen siendo
    ///    responsables de actualizar el saldo propio de cada documento; es
    ///    <see cref="AplicarAnticipoSaldo"/> quien decide, por el Tag de cada
    ///    fila, a cuál de los dos llamar.
    ///  - APLICACIÓN DE ANTICIPOS — LADO PROVEEDORES (este cambio): se aplica el
    ///    MISMO criterio que el lado Clientes, pero con 4 tipos de documento en
    ///    vez de 2 (P=RecepcionProducto, G=RegistroGastos, RR=RegistroReembolso,
    ///    NCG=NotasGasto — el discriminador "Tipo" ya existía en
    ///    AnticipoProveedorCobros/CargarEgreso2/ActualizarEgreso2, solo le
    ///    faltaba el caso "RR" en ActualizarEgreso2, que se corrigió). Se agregó:
    ///      * <see cref="CargarEgreso2"/>: se le agregó el bloque UNION
    ///        faltante para 'RR' (RegistroReembolso), y se reescribieron los 4
    ///        bloques con columnas explícitas en vez de "R.*" (RegistroReembolso
    ///        no tiene las mismas columnas que RecepcionProducto/RegistroGastos,
    ///        así que un UNION con "R.*" en los 4 bloques se habría caído por
    ///        desalineación de columnas).
    ///      * <see cref="ExisteDocumentoEgreso"/>: valida que el folio exista en
    ///        su tabla de origen antes de registrar el pago (mismo rol que
    ///        <see cref="ExisteDocumento"/> del lado Clientes).
    ///      * Sobrecarga de InsertarEgreso con los mismos campos que ya tiene
    ///        <see cref="InsertarCobro"/> del lado Clientes (Anticipo origen,
    ///        SaldoRestante, DescuentoPago, IdConceptoCobroPago) — requiere la
    ///        migración de AnticipoProveedorCobros (ver
    ///        02_Migracion_AnticipoProveedor.sql). Se agregó como SOBRECARGA (no
    ///        se modificó la firma original de 6 parámetros) para no romper otros
    ///        lugares del sistema que ya llamen a InsertarEgreso con la firma vieja.
    ///  - Nota de negocio: la columna "ClaveProveedor" en las tablas Factura y
    ///    Remision en realidad identifica al CLIENTE (nombre heredado de una
    ///    etapa anterior del sistema). No se renombró la columna física para no
    ///    ampliar el alcance de este cambio, pero el código nuevo de esta clase
    ///    usa nombres de parámetro/variable que reflejan su significado real
    ///    (p. ej. "claveCliente") para no seguir arrastrando la confusión.
    ///  - La lógica de negocio (qué se inserta, qué se actualiza, cuándo se
    ///    pregunta, qué mensajes se muestran) se mantiene intacta salvo donde
    ///    se indica explícitamente lo contrario.
    /// </summary>
    class DBAnticipo
    {
        public static int Folio = 0;

        #region Configuración de conexión

        /// <summary>Obtiene el connection string configurado en Settings.</summary>
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        /// <summary>
        /// Valida la disponibilidad de la base de datos al crear el objeto.
        /// La conexión de prueba se cierra de inmediato (no se deja abierta
        /// para toda la vida del formulario).
        /// </summary>
        public DBAnticipo()
        {
            try
            {
                using (SqlConnection cnPrueba = new SqlConnection(ObtenerCn()))
                {
                    cnPrueba.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }

        /// <summary>Crea y abre una nueva conexión. El llamador debe liberarla con "using".</summary>
        private SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(ObtenerCn());
            conexion.Open();
            return conexion;
        }

        #endregion

        #region Generación de folios

        /// <summary>Obtiene el folio máximo de Anticipo y lo deja en la propiedad estática Folio.</summary>
        public int ClaveProductoSiguiente()
        {
            int contador = 0;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) FROM Anticipo", cn))
                {
                    Folio = Convert.ToInt32(cmd.ExecuteScalar());
                    contador = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }

        /// <summary>Obtiene el folio máximo de AnticipoProveedor y lo deja en la propiedad estática Folio.</summary>
        public int ClaveProductoSiguiente2()
        {
            int contador = 0;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) FROM AnticipoProveedor", cn))
                {
                    Folio = Convert.ToInt32(cmd.ExecuteScalar());
                    contador = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return contador;
        }

        #endregion

        #region Catálogos para ComboBox

        /// <summary>Llena un ComboBox con las cuentas bancarias disponibles ("Nombre - Cuenta").</summary>
        public void SeleccionarCuentaBancaria(ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (Nombre + ' - ' + Cuenta) AS Cuenta FROM CuentasBancarias", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuentas bancarias. " + ex.Message);
            }
        }

        /// <summary>Llena un ComboBox con las formas de pago disponibles.</summary>
        public void SeleccionarFormaPago(ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("SELECT Descripcion FROM FormasPago", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formas de pago. " + ex.Message);
            }
        }

        /// <summary>Llena un ComboBox con los propietarios ("Id - RazonSocial"), incluyendo la opción "TODOS".</summary>
        public void SeleccionarPropietarios(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (CONVERT(varchar, IdPropietario) + ' - ' + RazonSocial) AS Nombre FROM Propietarios", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>Llena un ComboBox con los proveedores ("Id - RazonSocial"), incluyendo la opción "TODOS".</summary>
        public void SeleccionarProveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (CONVERT(varchar, IdProveedor) + ' - ' + RazonSocial) AS Nombre FROM Proveedor", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cb.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        #endregion

        #region Registro de anticipos (alta / actualización)

        /// <summary>
        /// Da de alta o actualiza (previa confirmación del usuario) un Anticipo de
        /// Propietario/Cliente. El concepto (txtClavePropietario, etc.) ya llega
        /// resuelto contra ConceptoCobroPago desde el formulario de creación
        /// (fuera del alcance de este cambio: esa migración ya se aplicó).
        /// </summary>
        public string RegistroAnticipo(string txtfolio, string txtClavePropietario, string txtCaja, string txtFecha,
            string txtFormaPago, string txtConcepto, string txtReferencia, string txtCuentaBancaria,
            string txtNumeroOperacion, decimal importe, string Divisa, string TipoCambio, string FolioC, string FolioCGeneral)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    bool existe;
                    using (SqlCommand cmdExiste = new SqlCommand("SELECT COUNT(*) FROM Anticipo WHERE Folio = @Folio", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Folio", txtfolio);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(
                            "INSERT INTO Anticipo (Folio, ClavePropietario, Caja, Fecha, FormaPago, Concepto, Referencia, " +
                            "CuentaBancaria, NumeroOperacion, Importe, Saldo, Divisa, TipoCambio, FolioC, FolioCGeneral) " +
                            "VALUES (@Folio, @ClavePropietario, @Caja, @Fecha, @FormaPago, @Concepto, @Referencia, " +
                            "@CuentaBancaria, @NumeroOperacion, @Importe, @Saldo, @Divisa, @TipoCambio, @FolioC, @FolioCGeneral)", cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@Folio", txtfolio);
                            cmdInsert.Parameters.AddWithValue("@ClavePropietario", txtClavePropietario);
                            cmdInsert.Parameters.AddWithValue("@Caja", txtCaja);
                            cmdInsert.Parameters.AddWithValue("@Fecha", txtFecha);
                            cmdInsert.Parameters.AddWithValue("@FormaPago", txtFormaPago);
                            cmdInsert.Parameters.AddWithValue("@Concepto", txtConcepto);
                            cmdInsert.Parameters.AddWithValue("@Referencia", txtReferencia);
                            cmdInsert.Parameters.AddWithValue("@CuentaBancaria", txtCuentaBancaria);
                            cmdInsert.Parameters.AddWithValue("@NumeroOperacion", txtNumeroOperacion);
                            cmdInsert.Parameters.AddWithValue("@Importe", importe);
                            cmdInsert.Parameters.AddWithValue("@Saldo", importe);
                            cmdInsert.Parameters.AddWithValue("@Divisa", Divisa);
                            cmdInsert.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                            cmdInsert.Parameters.AddWithValue("@FolioC", FolioC);
                            cmdInsert.Parameters.AddWithValue("@FolioCGeneral", FolioCGeneral);
                            cmdInsert.ExecuteNonQuery();
                        }
                        mensaje = "Registro guardado.";
                    }
                    else if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlCommand cmdUpdate = new SqlCommand(
                            "UPDATE Anticipo SET ClavePropietario=@ClavePropietario, Caja=@Caja, Fecha=@Fecha, " +
                            "FormaPago=@FormaPago, Concepto=@Concepto, Referencia=@Referencia, CuentaBancaria=@CuentaBancaria, " +
                            "NumeroOperacion=@NumeroOperacion, Importe=@Importe, Saldo=@Saldo, Divisa=@Divisa, " +
                            "TipoCambio=@TipoCambio, FolioC=@FolioC, FolioCGeneral=@FolioCGeneral WHERE Folio=@Folio", cn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@ClavePropietario", txtClavePropietario);
                            cmdUpdate.Parameters.AddWithValue("@Caja", txtCaja);
                            cmdUpdate.Parameters.AddWithValue("@Fecha", txtFecha);
                            cmdUpdate.Parameters.AddWithValue("@FormaPago", txtFormaPago);
                            cmdUpdate.Parameters.AddWithValue("@Concepto", txtConcepto);
                            cmdUpdate.Parameters.AddWithValue("@Referencia", txtReferencia);
                            cmdUpdate.Parameters.AddWithValue("@CuentaBancaria", txtCuentaBancaria);
                            cmdUpdate.Parameters.AddWithValue("@NumeroOperacion", txtNumeroOperacion);
                            cmdUpdate.Parameters.AddWithValue("@Importe", importe);
                            cmdUpdate.Parameters.AddWithValue("@Saldo", importe);
                            cmdUpdate.Parameters.AddWithValue("@Divisa", Divisa);
                            cmdUpdate.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                            cmdUpdate.Parameters.AddWithValue("@FolioC", FolioC);
                            cmdUpdate.Parameters.AddWithValue("@FolioCGeneral", FolioCGeneral);
                            cmdUpdate.Parameters.AddWithValue("@Folio", txtfolio);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }

        /// <summary>Da de alta o actualiza (previa confirmación del usuario) un Anticipo de Proveedor.</summary>
        public string RegistroAnticipoProveedor(string txtfolio, string txtClaveProveedor, string txtCaja, string txtFecha,
            string txtFormaPago, string txtConcepto, string txtReferencia, string txtCuentaBancaria,
            string txtNumeroOperacion, decimal importe, string Divisa, string TipoCambio)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    bool existe;
                    using (SqlCommand cmdExiste = new SqlCommand("SELECT COUNT(*) FROM AnticipoProveedor WHERE Folio = @Folio", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Folio", txtfolio);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(
                            "INSERT INTO AnticipoProveedor (Folio, ClaveProveedor, Caja, Fecha, FormaPago, Concepto, " +
                            "Referencia, CuentaBancaria, NumeroOperacion, Importe, Saldo, Divisa, TipoCambio) " +
                            "VALUES (@Folio, @ClaveProveedor, @Caja, @Fecha, @FormaPago, @Concepto, @Referencia, " +
                            "@CuentaBancaria, @NumeroOperacion, @Importe, @Saldo, @Divisa, @TipoCambio)", cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@Folio", txtfolio);
                            cmdInsert.Parameters.AddWithValue("@ClaveProveedor", txtClaveProveedor);
                            cmdInsert.Parameters.AddWithValue("@Caja", txtCaja);
                            cmdInsert.Parameters.AddWithValue("@Fecha", txtFecha);
                            cmdInsert.Parameters.AddWithValue("@FormaPago", txtFormaPago);
                            cmdInsert.Parameters.AddWithValue("@Concepto", txtConcepto);
                            cmdInsert.Parameters.AddWithValue("@Referencia", txtReferencia);
                            cmdInsert.Parameters.AddWithValue("@CuentaBancaria", txtCuentaBancaria);
                            cmdInsert.Parameters.AddWithValue("@NumeroOperacion", txtNumeroOperacion);
                            cmdInsert.Parameters.AddWithValue("@Importe", importe);
                            cmdInsert.Parameters.AddWithValue("@Saldo", importe);
                            cmdInsert.Parameters.AddWithValue("@Divisa", Divisa);
                            cmdInsert.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                            cmdInsert.ExecuteNonQuery();
                        }
                        mensaje = "Registro guardado.";
                    }
                    else if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de la Tienda",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlCommand cmdUpdate = new SqlCommand(
                            "UPDATE AnticipoProveedor SET ClaveProveedor=@ClaveProveedor, Caja=@Caja, Fecha=@Fecha, " +
                            "FormaPago=@FormaPago, Concepto=@Concepto, Referencia=@Referencia, CuentaBancaria=@CuentaBancaria, " +
                            "NumeroOperacion=@NumeroOperacion, Importe=@Importe, Saldo=@Saldo, Divisa=@Divisa, " +
                            "TipoCambio=@TipoCambio WHERE Folio=@Folio", cn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@ClaveProveedor", txtClaveProveedor);
                            cmdUpdate.Parameters.AddWithValue("@Caja", txtCaja);
                            cmdUpdate.Parameters.AddWithValue("@Fecha", txtFecha);
                            cmdUpdate.Parameters.AddWithValue("@FormaPago", txtFormaPago);
                            cmdUpdate.Parameters.AddWithValue("@Concepto", txtConcepto);
                            cmdUpdate.Parameters.AddWithValue("@Referencia", txtReferencia);
                            cmdUpdate.Parameters.AddWithValue("@CuentaBancaria", txtCuentaBancaria);
                            cmdUpdate.Parameters.AddWithValue("@NumeroOperacion", txtNumeroOperacion);
                            cmdUpdate.Parameters.AddWithValue("@Importe", importe);
                            cmdUpdate.Parameters.AddWithValue("@Saldo", importe);
                            cmdUpdate.Parameters.AddWithValue("@Divisa", Divisa);
                            cmdUpdate.Parameters.AddWithValue("@TipoCambio", TipoCambio);
                            cmdUpdate.Parameters.AddWithValue("@Folio", txtfolio);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }

        #endregion

        #region Carga de grids (listado de anticipos)

        /// <summary>
        /// Lista Anticipos activos filtrados por nombre de Propietario (join contra
        /// Propietarios/Propietarios_Condominios).
        /// NOTA: este flujo coexiste con <see cref="CargarAnticipoCliente"/> (join
        /// contra Clientes) tal como en el código original; se conserva así para no
        /// alterar el comportamiento actual (ver aviso ampliado en el chat).
        /// </summary>
        public void CargarAnticipo(DataGridView dgv, string filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT A.Folio, A.Saldo, P.RazonSocial, PC.Descripcion AS Depto " +
                    "FROM Anticipo AS A " +
                    "INNER JOIN Propietarios AS P ON A.ClavePropietario = P.IdPropietario " +
                    "INNER JOIN Propietarios_Condominios AS PC ON P.IdPropietario = PC.ClavePropietario " +
                    "WHERE A.Activo IS NULL AND P.RazonSocial LIKE @Filtro " +
                    "ORDER BY P.IdPropietario ASC", cn))
                {
                    cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                            dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["Saldo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        /// <summary>Lista Anticipos activos filtrados por nombre de Cliente (join contra Clientes).</summary>
        public void CargarAnticipoCliente(DataGridView dgv, string filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT A.Folio, A.Saldo, C.RazonSocial " +
                    "FROM Anticipo AS A " +
                    "INNER JOIN Clientes AS C ON A.ClavePropietario = C.IdCliente " +
                    "WHERE A.Activo IS NULL AND C.RazonSocial LIKE @Filtro " +
                    "ORDER BY C.IdCliente ASC", cn))
                {
                    cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                            dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["Saldo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        /// <summary>Lista Anticipos de Proveedor activos filtrados por nombre.</summary>
        public void CargarAnticipoProveedor(DataGridView dgv, string filtro)
        {
            try
            {
                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT A.Folio, A.Saldo, P.RazonSocial " +
                    "FROM AnticipoProveedor AS A " +
                    "INNER JOIN Proveedor AS P ON A.ClaveProveedor = P.IdProveedor " +
                    "WHERE A.Activo IS NULL AND P.RazonSocial LIKE @Filtro " +
                    "ORDER BY A.Folio ASC", cn))
                {
                    cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[1].Value = item["RazonSocial"].ToString();
                            dgv.Rows[n].Cells[0].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["Saldo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        #endregion

        #region Aplicación de Anticipos: soporte para documento destino polimórfico (Clientes: Remisión / Factura)

        /// <summary>
        /// Resuelve el nombre físico de tabla correspondiente a un TipoDocumento
        /// ('REMISION' -> "Remision", 'FACTURA' -> "Factura"). Punto único de verdad
        /// para el despacho polimórfico, usado por <see cref="ExisteDocumento"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Si el TipoDocumento no es reconocido.</exception>
        private static string ResolverTablaDocumento(string tipoDocumento)
        {
            switch ((tipoDocumento ?? string.Empty).Trim().ToUpperInvariant())
            {
                case "REMISION":
                    return "Remision";
                case "FACTURA":
                    return "Factura";
                default:
                    throw new ArgumentException($"TipoDocumento '{tipoDocumento}' no reconocido. Se esperaba REMISION o FACTURA.");
            }
        }

        /// <summary>
        /// Valida que un folio exista realmente en la tabla origen indicada por
        /// TipoDocumento. Sustituye, en código, la integridad referencial que antes
        /// daba la FK física hacia una sola tabla (FK_AnticipoCobroRecibo), ya
        /// imposible de mantener con un destino polimórfico.
        /// </summary>
        /// <remarks>
        /// NOTA DE DISEÑO: la actualización del saldo propio del documento destino
        /// (Remision.Saldo / Factura.Saldo) NO se centraliza aquí. Cada dominio ya
        /// tiene su propio método para eso —DBRemiision.ActualizarRemision y
        /// DBFacturas.ActualizarFacturaAbono—, que además de restar el saldo aplican
        /// su propia semántica de descuento por pronto pago. Esta clase (DBAnticipo)
        /// sólo se encarga de: 1) validar que el documento exista antes de registrar
        /// el cobro, y 2) insertar la fila en AnticipoCobros y actualizar el Anticipo
        /// origen. Es <see cref="AplicarAnticipoSaldo"/> quien orquesta, por fila,
        /// cuál actualizador de saldo (Remisión o Factura) invocar según el
        /// TipoDocumento marcado en <c>DataGridViewRow.Tag</c> — mismo patrón de
        /// discriminador por Tag que ya usa DBFacturas.CargarFacturaCobro para el
        /// flujo de RegistrarCobro.
        /// </remarks>
        private bool ExisteDocumento(SqlConnection cn, string tipoDocumento, string folio)
        {
            string tabla = ResolverTablaDocumento(tipoDocumento);
            using (SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {tabla} WHERE Folio = @Folio", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        #endregion

        #region Aplicación de Anticipos: soporte para documento destino polimórfico (Proveedores: Egresos)

        /// <summary>
        /// Resuelve el nombre físico de tabla correspondiente a un Tipo de egreso
        /// ('P' -> RecepcionProducto, 'G' -> RegistroGastos, 'RR' -> RegistroReembolso,
        /// 'NCG' -> NotasGasto). Mismo rol que <see cref="ResolverTablaDocumento"/>
        /// pero para el lado Proveedores; estos 4 tipos son los mismos que ya cubre
        /// el método de referencia "ObtenerEgresos" que se compartió en el chat.
        /// </summary>
        /// <exception cref="ArgumentException">Si el tipo no es reconocido.</exception>
        private static string ResolverTablaEgreso(string tipo)
        {
            switch ((tipo ?? string.Empty).Trim().ToUpperInvariant())
            {
                case "P":
                    return "RecepcionProducto";
                case "G":
                    return "RegistroGastos";
                case "RR":
                    return "RegistroReembolso";
                case "NCG":
                    return "NotasGasto";
                default:
                    throw new ArgumentException($"Tipo de egreso '{tipo}' no reconocido. Se esperaba P, G, RR o NCG.");
            }
        }

        /// <summary>
        /// Valida que un folio exista realmente en la tabla de egreso origen
        /// indicada por "tipo", y que pertenezca al proveedor indicado. Rol
        /// equivalente a <see cref="ExisteDocumento"/> del lado Clientes.
        /// </summary>
        private bool ExisteDocumentoEgreso(SqlConnection cn, string tipo, string folio, string claveProveedor)
        {
            string tabla = ResolverTablaEgreso(tipo);
            using (SqlCommand cmd = new SqlCommand(
                $"SELECT COUNT(*) FROM {tabla} WHERE Folio = @Folio AND ClaveProveedor = @ClaveProveedor", cn))
            {
                cmd.Parameters.AddWithValue("@Folio", folio);
                cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        #endregion

        #region Consulta de un anticipo seleccionado (doble clic en el grid)

        /// <summary>
        /// Recupera un Anticipo de Propietario/Cliente por folio y llena los controles
        /// del formulario de detalle. Devuelve el valor de "Activo" ("1" = cancelado).
        /// Se conserva exactamente el mismo conjunto de columnas y asignaciones que el
        /// original (incluyendo que ImporteMXN se llena con "Importe", no con la
        /// columna calculada ImporteMXNCalculado).
        /// </summary>
        public string ConsultaProductoSeleccionado(string txtfolio, Guna2TextBox txtClavePropietario, Guna2TextBox txtCaja,
            Guna2DateTimePicker txtFecha, ComboBox txtFormaPago, ComboBox txtConcepto, Guna2TextBox txtReferencia,
            TextBox txtCuentaBancaria, Guna2TextBox txtNumeroOperacion, Guna2TextBox importe, ComboBox Divisa,
            Guna2TextBox TipoCambio, Guna2TextBox Saldo, Guna2TextBox ImporteMXN)
        {
            string cont = string.Empty;
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Folio, ClavePropietario, Caja, Fecha, FormaPago, Concepto, Referencia, CuentaBancaria, " +
                    "NumeroOperacion, Importe, Saldo, Divisa, TipoCambio, ImporteMxn, SaldoMxn, Activo, " +
                    "(CONVERT(float, Importe) * CONVERT(float, TipoCambio)) AS ImporteMXNCalculado " +
                    "FROM Anticipo WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", txtfolio);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtClavePropietario.Text = dr["ClavePropietario"].ToString();
                            txtCaja.Text = dr["Caja"].ToString();
                            txtFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                            txtFormaPago.Text = dr["FormaPago"].ToString();
                            txtConcepto.SelectedValue = dr["Concepto"].ToString();
                            txtReferencia.Text = dr["Referencia"].ToString();
                            txtCuentaBancaria.Text = dr["CuentaBancaria"].ToString();
                            txtNumeroOperacion.Text = dr["NumeroOperacion"].ToString();
                            importe.Text = dr["Importe"].ToString();
                            Divisa.Text = dr["Divisa"].ToString();
                            TipoCambio.Text = dr["TipoCambio"].ToString();
                            Saldo.Text = dr["Saldo"].ToString();
                            ImporteMXN.Text = dr["Importe"].ToString();
                            cont = dr["Activo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return cont;
        }

        /// <summary>Recupera un Anticipo de Proveedor por folio y llena los controles del formulario de detalle.</summary>
        public void ConsultaProductoSeleccionadoProveedor(string txtfolio, Guna2TextBox txtClaveProveedor, Guna2TextBox txtCaja,
            Guna2DateTimePicker txtFecha, ComboBox txtFormaPago, ComboBox txtConcepto, Guna2TextBox txtReferencia,
            TextBox txtCuentaBancaria, Guna2TextBox txtNumeroOperacion, Guna2TextBox importe, ComboBox Divisa,
            Guna2TextBox TipoCambio, Guna2TextBox saldo, Guna2TextBox ImporteMXN)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Folio, ClaveProveedor, Caja, Fecha, FormaPago, Concepto, Referencia, CuentaBancaria, " +
                    "NumeroOperacion, Importe, Saldo, Divisa, TipoCambio, (Importe * TipoCambio) AS ImporteMXNCalculado " +
                    "FROM AnticipoProveedor WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", txtfolio);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtClaveProveedor.Text = dr["ClaveProveedor"].ToString();
                            txtCaja.Text = dr["Caja"].ToString();
                            txtFecha.Text = Convert.ToDateTime(dr["Fecha"]).ToString("yyyy/MM/dd");
                            txtFormaPago.Text = dr["FormaPago"].ToString();
                            txtConcepto.SelectedValue = dr["Concepto"].ToString();
                            txtReferencia.Text = dr["Referencia"].ToString();
                            txtCuentaBancaria.Text = dr["CuentaBancaria"].ToString();
                            txtNumeroOperacion.Text = dr["NumeroOperacion"].ToString();
                            importe.Text = dr["Importe"].ToString();
                            Divisa.Text = dr["Divisa"].ToString();
                            TipoCambio.Text = dr["TipoCambio"].ToString();
                            saldo.Text = dr["Saldo"].ToString();
                            ImporteMXN.Text = dr["Importe"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        /// <summary>
        /// Obtiene el concepto de cobro/pago por defecto para Anticipos
        /// (DatosEmpresa.ConceptoAnt) y su descripción, ya migrado a ConceptoCobroPago.
        /// </summary>
        /// <remarks>
        /// SUPUESTO A CONFIRMAR: se asume que DatosEmpresa.ConceptoAnt ya almacena el
        /// IdConcepto (int), igual que Anticipo.Concepto tras la migración. Si en la
        /// base de datos ConceptoAnt sigue siendo la ClaveConcepto (varchar), el JOIN
        /// debe compararse contra CCP.ClaveConcepto en lugar de CCP.IdConcepto.
        /// </remarks>
        public void ConsultaConceptoAnticipo(Guna2TextBox txtconcepto, TextBox txtconceptoclave)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT DE.ConceptoAnt, (CCP.ClaveConcepto + ' - ' + CCP.Descripcion) AS Nombre " +
                    "FROM DatosEmpresa AS DE " +
                    "INNER JOIN ConceptoCobroPago AS CCP ON DE.ConceptoAnt = CCP.IdConcepto", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtconcepto.Text = dr["Nombre"].ToString();
                        txtconceptoclave.Text = dr["ConceptoAnt"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        #endregion

        #region Información complementaria (cuentas, conceptos, propietarios, proveedores)

        // NOTA: antes devolvían null si no había coincidencias, lo que provocaba
        // NullReferenceException en el formulario al hacer valores[0]. Ahora devuelven
        // un arreglo con cadena vacía en ese caso.

        /// <summary>Obtiene la Clave de una cuenta bancaria a partir de su descripción "Nombre - Cuenta".</summary>
        public string[] InformacionCuenta(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Clave FROM CuentasBancarias WHERE (Nombre + ' - ' + Cuenta) = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new[] { dr[0].ToString() };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return new[] { string.Empty };
        }

        /// <summary>Obtiene la descripción "Nombre - Cuenta" de una cuenta bancaria a partir de su Clave.</summary>
        public string[] InformacionCuenta2(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (Nombre + ' - ' + Cuenta) FROM CuentasBancarias WHERE Clave = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new[] { dr[0].ToString() };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return new[] { string.Empty };
        }

        /// <summary>
        /// Obtiene "ClaveConcepto - Descripcion" de un concepto de cobro/pago a partir
        /// de su IdConcepto. Migrado de ConceptosIngreso a ConceptoCobroPago.
        /// </summary>
        public string[] InformacionConcepto(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (CCP.ClaveConcepto + ' - ' + CCP.Descripcion) AS Nombre " +
                    "FROM ConceptoCobroPago AS CCP WHERE CCP.IdConcepto = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new[] { dr[0].ToString() };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return new[] { string.Empty };
        }

        /// <summary>Obtiene la RazonSocial de un Propietario a partir de su IdPropietario.</summary>
        public string[] InformacionPropietario(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT RazonSocial FROM Propietarios WHERE IdPropietario = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new[] { dr[0].ToString() };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return new[] { string.Empty };
        }

        /// <summary>Obtiene la RazonSocial de un Proveedor a partir de su IdProveedor.</summary>
        public string[] InformacionProveedor(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT RazonSocial FROM Proveedor WHERE IdProveedor = @Documento", cn))
                {
                    cmd.Parameters.AddWithValue("@Documento", documento);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new[] { dr[0].ToString() };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            return new[] { string.Empty };
        }

        #endregion

        #region Recibos y reportes

        /// <summary>
        /// Carga en el grid la lista de Anticipos con saldo disponible para un
        /// cliente/propietario. Migrado de ConceptosIngreso a ConceptoCobroPago
        /// (Anticipo.Concepto ahora referencia ConceptoCobroPago.IdConcepto).
        /// </summary>
        public void CargarReciboAlumno(DataGridView dgv, string matricula)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT A.*, CCP.Descripcion FROM Anticipo AS A " +
                    "INNER JOIN ConceptoCobroPago AS CCP ON A.Concepto = CCP.IdConcepto " +
                    "WHERE A.ClavePropietario = @Matricula AND A.Saldo > 0 " +
                    "AND (A.Activo <> 1 OR A.Activo IS NULL)", cn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["Concepto"].ToString();
                            dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                            dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                            dgv.Rows[n].Cells[5].Value = item["Divisa"].ToString();
                            dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        /// <summary>
        /// Carga en el grid la lista de Anticipos de Proveedor con saldo disponible.
        /// Migrado de ConceptosIngreso a ConceptoCobroPago. También se corrigió la
        /// culture "US-AR" (no existe en .NET y lanzaba CultureNotFoundException) por
        /// "en-US", mismo formato que usa CargarReciboAlumno.
        /// </summary>
        public void CargarReciboProveedor(DataGridView dgv, string claveProveedor)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT A.*, CCP.Descripcion FROM AnticipoProveedor AS A " +
                    "INNER JOIN ConceptoCobroPago AS CCP ON A.Concepto = CCP.IdConcepto " +
                    "WHERE A.ClaveProveedor = @ClaveProveedor AND A.Saldo > 0 AND Activo = 1", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["Concepto"].ToString();
                            dgv.Rows[n].Cells[3].Value = item["Descripcion"].ToString();
                            dgv.Rows[n].Cells[4].Value = Convert.ToDateTime(item["Fecha"]).ToString("yyyy/MM/dd");
                            dgv.Rows[n].Cells[5].Value = item["Divisa"].ToString();

                            dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        /// <summary>
        /// Carga en la grilla de <see cref="AplicarAnticipoSaldo"/> las Remisiones con
        /// saldo pendiente de un cliente. Es el "lado Remisión" del listado combinado
        /// de documentos contra los que puede aplicarse un Anticipo; su contraparte
        /// para Facturas es <c>DBFacturas.CargarFacturaPendienteAnticipo</c>. Ambas
        /// comparten el mismo layout de columnas por índice (0=FolioDocumento,
        /// 1=Consecutivo, 2=ClaveDocumento, 3=Nombre, 4=Importe, 6=Descuento,
        /// 8=Abono, 9=Saldo) para poder combinarse en una sola grilla. El lado
        /// Proveedores (<see cref="CargarEgreso2"/>) resuelve el mismo problema
        /// pero con un layout propio y distinto, ya existente en su pantalla
        /// (AplicarAnticipoProveedorSaldo): 0=Tipo, 1=Folio, 2=ClaveDocumento,
        /// 3=Nombre, 4=Importe, 6=Abono, 7=Saldo.
        /// </summary>
        /// <param name="limpiarPrimero">
        /// Si es true (default), limpia el grid antes de cargar. Pásalo en false
        /// cuando esta carga se combine con
        /// <c>DBFacturas.CargarFacturaPendienteAnticipo</c> en el mismo grid (limpiar
        /// una sola vez desde el formulario, antes de llamar a ambos métodos) —
        /// mismo patrón de "limpiarPrimero" que ya usa DBFacturas.CargarFacturaCobro.
        /// </param>
        /// <remarks>
        /// Cada fila agregada queda marcada en su .Tag con la cadena "REMISION",
        /// siguiendo el mismo patrón de discriminador por Tag que ya usa
        /// DBFacturas.CargarFacturaCobro (Tag = "Factura") para el flujo de
        /// RegistrarCobro: como Remision.Folio y Factura.Folio son secuencias
        /// independientes y pueden coincidir en valor, sin este marcador no habría
        /// forma de saber, al leer la fila de vuelta, a qué tabla pertenece.
        /// Anteriormente marcado [Obsolete] por asumir que sería reemplazado por un
        /// único loader combinado en esta clase; se revirtió esa decisión al
        /// confirmarse que el proyecto ya resuelve este mismo problema (Remisión +
        /// Factura en una grilla) con loaders especializados por dominio + Tag, en
        /// vez de un loader genérico centralizado. Se mantiene aquí, no en una clase
        /// aparte, porque ya vivía en DBAnticipo y no hay evidencia de que dependa de
        /// otra cosa que no sea Remision/Documento.
        /// </remarks>
        public void CargarReciboAlumno2(DataGridView dgv, string claveCliente, bool limpiarPrimero = true)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                if (limpiarPrimero)
                {
                    dgv.Rows.Clear();
                }

                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT R.*, D.Nombre FROM Remision AS R, Documento AS D " +
                    "WHERE ClaveProveedor = @ClaveCliente AND R.ClaveDocumento = D.Clave AND R.Saldo > 0", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveCliente", claveCliente);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
                            dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                            dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                            dgv.Rows[n].Cells[8].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                            dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                            dgv.Rows[n].Tag = "REMISION";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos aqui" + ex.ToString());
            }
        }

        /// <summary>
        /// Carga movimientos de egreso (RecepcionProducto/RegistroGastos/
        /// RegistroReembolso/NotasGasto) de un proveedor. Es el método que
        /// alimenta la grilla de AplicarAnticipoProveedorSaldo. Se corrigió la
        /// culture inválida "US-AR" -> "en-US" (mismo motivo que en
        /// CargarReciboProveedor).
        /// </summary>
        /// <remarks>
        /// FIX 1: antes solo cubría 'P', 'G' y 'NCG' — le faltaba 'RR'
        /// (RegistroReembolso), uno de los 4 tipos de egreso del sistema (ver
        /// el método de referencia "ObtenerEgresos" compartido en el chat, y
        /// <see cref="ActualizarEgreso2"/>/<see cref="ResolverTablaEgreso"/>,
        /// que ya sí lo contemplaban). Un RegistroReembolso pendiente
        /// simplemente no aparecía en esta grilla y no podía pagarse con un
        /// Anticipo.
        /// FIX 2: la versión anterior armaba los bloques P y G con "R.*"
        /// (todas las columnas de la tabla). Según el propio "ObtenerEgresos"
        /// de referencia, RegistroReembolso NO tiene las columnas Recargo,
        /// DescuentoPago, Almacen, Condominio ni Extension que sí tienen
        /// RecepcionProducto/RegistroGastos — un UNION con "R.*" en los 4
        /// bloques exige el mismo número de columnas en cada uno y se habría
        /// caído (o, peor, alineado columnas distintas sin error si el conteo
        /// hubiera coincidido por coincidencia). Como este método solo
        /// consume Tipo/Folio/ClaveDocumento/Saldo/Nombre más abajo, se
        /// reescribieron los 4 bloques para seleccionar EXPLÍCITAMENTE solo
        /// esas columnas, evitando por completo el riesgo de desalineación
        /// (y de paso ya no hace falta el relleno de "'' AS DiasVence, '' AS
        /// FechaVence" que tenía el bloque de NotasGasto).
        /// </remarks>
        public void CargarEgreso2(DataGridView dgv, string claveProveedor)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "(SELECT 'P' AS Tipo, R.Folio, R.ClaveDocumento, R.Saldo, D.Nombre " +
                    " FROM RecepcionProducto AS R, Documento AS D " +
                    " WHERE R.ClaveProveedor = @ClaveProveedor AND R.Saldo <> 0 AND R.ClaveDocumento = D.Clave) " +
                    "UNION " +
                    "(SELECT 'G' AS Tipo, R.Folio, R.ClaveDocumento, R.Saldo, D.Nombre " +
                    " FROM RegistroGastos AS R, Documento AS D " +
                    " WHERE R.ClaveProveedor = @ClaveProveedor AND R.Saldo <> 0 AND R.ClaveDocumento = D.Clave) " +
                    "UNION " +
                    "(SELECT 'RR' AS Tipo, R.Folio, R.ClaveDocumento, R.Saldo, D.Nombre " +
                    " FROM RegistroReembolso AS R, Documento AS D " +
                    " WHERE R.ClaveProveedor = @ClaveProveedor AND R.Saldo <> 0 AND R.ClaveDocumento = D.Clave) " +
                    "UNION " +
                    "(SELECT 'NCG' AS Tipo, R.Folio, R.ClaveDocumento, R.Saldo, D.Nombre " +
                    " FROM NotasGasto AS R, Documento AS D " +
                    " WHERE R.ClaveProveedor = @ClaveProveedor AND R.Saldo <> 0 AND R.ClaveDocumento = D.Clave)", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();

                            // Mapeo seguro según la nueva estructura de 11 columnas
                            dgv.Rows[n].Cells[0].Value = item["Tipo"].ToString();             // Tipo
                            dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();            // FolioDocumento
                            dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();   // Consecutivo
                            dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();           // Documento

                            decimal saldoOriginal = Convert.ToDecimal(item["Saldo"]);
                            dgv.Rows[n].Cells[5].Value = saldoOriginal.ToString("N", formato); // Importe (Cells[5])

                            dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(0.00).ToString("N", formato); // Descuento (Cells[7])
                            dgv.Rows[n].Cells[9].Value = Convert.ToDecimal(0.00).ToString("N", formato); // Abono (Cells[9])
                            dgv.Rows[n].Cells[10].Value = saldoOriginal.ToString("N", formato); // Saldo inicial (Cells[10])
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        #endregion

        #region Cobros y movimientos generales

        /// <summary>Genera folio e inserta un movimiento en Anticipo_General a partir del importe cobrado.</summary>
        public void InsertarCobroGeneral(decimal importe, TextBox folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    int folioSiguiente;
                    using (SqlCommand cmdMax = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) FROM Anticipo_General", cn))
                    {
                        folioSiguiente = Convert.ToInt32(cmdMax.ExecuteScalar()) + 1;
                    }
                    folio.Text = folioSiguiente.ToString();

                    bool existe;
                    using (SqlCommand cmdExiste = new SqlCommand("SELECT COUNT(*) FROM Anticipo_General WHERE Folio = @Folio", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Folio", folioSiguiente);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(
                            "INSERT INTO Anticipo_General (Folio, Importe) VALUES (@Folio, @Importe)", cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@Folio", folioSiguiente);
                            cmdInsert.Parameters.AddWithValue("@Importe", importe);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>Genera folio e inserta un movimiento en AnticipoProveedor_General a partir del importe cobrado.</summary>
        public void InsertarCobroGeneralProveedor(decimal importe, TextBox folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    int folioSiguiente;
                    using (SqlCommand cmdMax = new SqlCommand("SELECT ISNULL(MAX(Folio), 0) FROM AnticipoProveedor_General", cn))
                    {
                        folioSiguiente = Convert.ToInt32(cmdMax.ExecuteScalar()) + 1;
                    }
                    folio.Text = folioSiguiente.ToString();

                    bool existe;
                    using (SqlCommand cmdExiste = new SqlCommand("SELECT COUNT(*) FROM AnticipoProveedor_General WHERE Folio = @Folio", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Folio", folioSiguiente);
                        existe = Convert.ToInt32(cmdExiste.ExecuteScalar()) > 0;
                    }

                    if (!existe)
                    {
                        using (SqlCommand cmdInsert = new SqlCommand(
                            "INSERT INTO AnticipoProveedor_General (Folio, Importe) VALUES (@Folio, @Importe)", cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@Folio", folioSiguiente);
                            cmdInsert.Parameters.AddWithValue("@Importe", importe);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// Inserta un egreso aplicado a un movimiento de proveedor (RecepcionProducto/
        /// RegistroGastos/NotasGasto). FIRMA ORIGINAL, sin tocar — se conserva por si
        /// algún otro punto del sistema (fuera de los archivos revisados en este chat)
        /// todavía la invoca así. Para el flujo nuevo de Aplicación de Anticipo a
        /// Proveedores (con Anticipo origen, SaldoRestante, DescuentoPago e
        /// IdConceptoCobroPago) usa la sobrecarga de abajo.
        /// </summary>
        public void InsertarEgreso(string tipo, string folio, string claveProveedor, string fecha, decimal pago, string folioGeneral)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO AnticipoProveedorCobros (Tipo, Folio, ClaveProveedor, Fecha, Pago, FolioGeneral) " +
                    "VALUES (@Tipo, @Folio, @ClaveProveedor, @Fecha, @Pago, @FolioGeneral)", cn))
                {
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Pago", pago);
                    cmd.Parameters.AddWithValue("@FolioGeneral", folioGeneral);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// Sobrecarga: registra la aplicación de un Anticipo de Proveedor contra un
        /// documento de egreso destino (RecepcionProducto/RegistroGastos/
        /// RegistroReembolso/NotasGasto), con el mismo nivel de detalle que ya tiene
        /// <see cref="InsertarCobro"/> del lado Clientes.
        /// </summary>
        /// <param name="tipo">Discriminador del documento destino: "P", "G", "RR" o "NCG".</param>
        /// <param name="folioDocumento">Folio del documento destino en su tabla de origen.</param>
        /// <param name="claveProveedor">Clave del Proveedor dueño del Anticipo.</param>
        /// <param name="fecha">Fecha de la aplicación.</param>
        /// <param name="pago">Importe aplicado.</param>
        /// <param name="folioGeneral">Folio del movimiento general asociado (AnticipoProveedor_General).</param>
        /// <param name="anticipo">Folio del AnticipoProveedor origen del cual se está descontando el saldo.</param>
        /// <param name="saldoRestante">Saldo restante del AnticipoProveedor origen tras la aplicación.</param>
        /// <param name="descuentoPago">Descuento por pronto pago, si aplica.</param>
        /// <param name="idConceptoCobroPago">
        /// Concepto (ConceptoCobroPago.IdConcepto) vigente en el Anticipo origen al
        /// momento de la aplicación; se guarda como trazabilidad/auditoría.
        /// </param>
        /// <remarks>
        /// Requiere la migración de AnticipoProveedorCobros (columnas Anticipo,
        /// SaldoRestante, DescuentoPago, IdConceptoCobroPago) — ver
        /// 02_Migracion_AnticipoProveedor.sql. Valida la existencia del documento
        /// destino en su tabla origen (y que pertenezca al proveedor) antes de
        /// insertar (ver <see cref="ExisteDocumentoEgreso"/>), ya que aquí tampoco
        /// hay una FK física hacia una sola tabla posible. Esta validación NO
        /// reemplaza la actualización del saldo propio del documento — eso sigue a
        /// cargo de <see cref="ActualizarEgreso2"/>, que quien orqueste esta pantalla
        /// (análoga a AplicarAnticipoSaldo) debe invocar por separado según el Tag de
        /// cada fila — este método sólo registra el movimiento y descuenta el
        /// Anticipo origen (ver <see cref="ActualizarAnticipoProveedor"/>).
        /// </remarks>
        public void InsertarEgreso(string tipo, string folioDocumento, string claveProveedor, string fecha,
            decimal pago, string folioGeneral, string anticipo, decimal saldoRestante, decimal descuentoPago,
            int? idConceptoCobroPago = null)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    if (!ExisteDocumentoEgreso(cn, tipo, folioDocumento, claveProveedor))
                    {
                        MessageBox.Show($"El folio {folioDocumento} no existe como documento de tipo '{tipo}' para este proveedor.");
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO AnticipoProveedorCobros (Tipo, Folio, ClaveProveedor, Fecha, Pago, FolioGeneral, " +
                        "Anticipo, SaldoRestante, DescuentoPago, IdConceptoCobroPago) " +
                        "VALUES (@Tipo, @Folio, @ClaveProveedor, @Fecha, @Pago, @FolioGeneral, " +
                        "@Anticipo, @SaldoRestante, @DescuentoPago, @IdConceptoCobroPago)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", tipo);
                        cmd.Parameters.AddWithValue("@Folio", folioDocumento);
                        cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@Pago", pago);
                        cmd.Parameters.AddWithValue("@FolioGeneral", folioGeneral);
                        cmd.Parameters.AddWithValue("@Anticipo", anticipo);
                        cmd.Parameters.AddWithValue("@SaldoRestante", saldoRestante);
                        cmd.Parameters.AddWithValue("@DescuentoPago", descuentoPago);
                        cmd.Parameters.AddWithValue("@IdConceptoCobroPago", (object)idConceptoCobroPago ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// Registra la aplicación de un Anticipo de Propietario/Cliente contra un
        /// documento destino (Remisión o Factura).
        /// </summary>
        /// <param name="folioDocumento">
        /// Folio del documento destino (Remision.Folio o Factura.Folio). Es el mismo
        /// valor que se guarda en AnticipoCobros.Folio: en el diseño original (previo
        /// a este cambio) ese campo siempre era, de hecho, el folio del documento
        /// contra el que se aplicaba el anticipo (por eso la vieja FK apuntaba
        /// directo a Recibo). Con el destino polimórfico, ese folio por sí solo ya no
        /// basta para saber a qué tabla pertenece — de ahí "tipoDocumento".
        /// </param>
        /// <param name="tipoDocumento">Discriminador del documento destino: "REMISION" o "FACTURA".</param>
        /// <param name="claveProperietario">Clave del Propietario/Cliente dueño del Anticipo.</param>
        /// <param name="fecha">Fecha de la aplicación.</param>
        /// <param name="pago">Importe aplicado.</param>
        /// <param name="folioGeneral">Folio del movimiento general asociado (Anticipo_General).</param>
        /// <param name="anticipo">Folio del Anticipo origen (Anticipo.Folio) del cual se está descontando el saldo.</param>
        /// <param name="saldo">Saldo restante del Anticipo origen tras la aplicación.</param>
        /// <param name="descuentoPago">Descuento por pronto pago, si aplica.</param>
        /// <param name="idConceptoCobroPago">
        /// Concepto (ConceptoCobroPago.IdConcepto) vigente en el Anticipo origen al
        /// momento de la aplicación; se guarda como trazabilidad/auditoría.
        /// </param>
        /// <remarks>
        /// CAMBIO DE FIRMA respecto a la versión original: se agregó "tipoDocumento",
        /// requerido ahora que el destino es polimórfico. Se valida la existencia del
        /// documento destino en su tabla origen antes de insertar, ya que la FK
        /// física hacia un único tipo de tabla ya no es viable (ver
        /// <see cref="ExisteDocumento"/>). Esta validación NO reemplaza la
        /// actualización del saldo propio del documento (eso sigue a cargo de
        /// DBRemiision.ActualizarRemision / DBFacturas.ActualizarFacturaAbono, que
        /// <see cref="AplicarAnticipoSaldo"/> invoca por separado según el Tag de
        /// cada fila) — este método sólo registra el movimiento y descuenta el
        /// Anticipo origen.
        /// </remarks>
        public void InsertarCobro(string folioDocumento, string tipoDocumento, string claveProperietario,
            string fecha, decimal pago, string folioGeneral, string anticipo, decimal saldo, decimal descuentoPago,
            int? idConceptoCobroPago = null)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                {
                    if (!ExisteDocumento(cn, tipoDocumento, folioDocumento))
                    {
                        MessageBox.Show($"El folio {folioDocumento} no existe como {tipoDocumento}.");
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO AnticipoCobros (Folio, TipoDocumento, ClavePropietario, Fecha, Pago, FolioGeneral, " +
                        "Anticipo, SaldoRestante, DescuentoPago, IdConceptoCobroPago) " +
                        "VALUES (@Folio, @TipoDocumento, @ClavePropietario, @Fecha, @Pago, @FolioGeneral, " +
                        "@Anticipo, @SaldoRestante, @DescuentoPago, @IdConceptoCobroPago)", cn))
                    {
                        cmd.Parameters.AddWithValue("@Folio", folioDocumento);
                        cmd.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);
                        cmd.Parameters.AddWithValue("@ClavePropietario", claveProperietario);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@Pago", pago);
                        cmd.Parameters.AddWithValue("@FolioGeneral", folioGeneral);
                        cmd.Parameters.AddWithValue("@Anticipo", anticipo);
                        cmd.Parameters.AddWithValue("@SaldoRestante", saldo);
                        cmd.Parameters.AddWithValue("@DescuentoPago", descuentoPago);
                        cmd.Parameters.AddWithValue("@IdConceptoCobroPago", (object)idConceptoCobroPago ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        #endregion

        #region Actualización de saldos

        /// <summary>Descuenta un importe del saldo de un Proveedor.</summary>
        public void ActualizarSaldoProveedor(string clave, decimal saldo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE Proveedor SET Saldo = Saldo - @Saldo WHERE IdProveedor = @Clave", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR prov" + ex.ToString());
            }
        }

        /// <summary>
        /// Actualiza el saldo de un movimiento de egreso (RecepcionProducto/
        /// RegistroGastos/RegistroReembolso/NotasGasto) según su tipo.
        /// </summary>
        /// <remarks>
        /// FIX: antes no contemplaba "RR" (RegistroReembolso) — si tipo era "RR",
        /// "tabla" quedaba null y el método regresaba sin actualizar nada, dejando
        /// el saldo del RegistroReembolso desactualizado tras un pago. Se agregó el
        /// caso faltante, usando <see cref="ResolverTablaEgreso"/> como única fuente
        /// de verdad para el mapeo tipo -> tabla (ya usado por
        /// <see cref="ExisteDocumentoEgreso"/>), en vez de repetir el switch aquí.
        /// </remarks>
        public void ActualizarEgreso2(string tipo, string folio, decimal saldo)
        {
            string tabla;
            try
            {
                tabla = ResolverTablaEgreso(tipo);
            }
            catch (ArgumentException)
            {
                return;
            }

            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand($"UPDATE {tabla} SET Saldo = @Saldo WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// Actualiza el saldo de la tabla "Recibo" a partir de su Folio.
        /// </summary>
        /// <remarks>
        /// OBSOLETO: "Recibo" ya no puede ser el único destino posible de una
        /// aplicación de Anticipo. El saldo del documento destino ahora se actualiza
        /// con <c>DBRemiision.ActualizarRemision</c> (si TipoDocumento = REMISION) o
        /// <c>DBFacturas.ActualizarFacturaAbono</c> (si TipoDocumento = FACTURA),
        /// según decide <see cref="AplicarAnticipoSaldo"/> por el Tag de cada fila.
        /// Se deja esta implementación intacta y sin usar en el nuevo flujo,
        /// únicamente por si algún otro punto del sistema (fuera de los archivos
        /// revisados) aún la invoca. PENDIENTE DE CONFIRMAR: si "Recibo" ya no se usa
        /// en ningún otro módulo, este método (y la tabla) pueden retirarse por
        /// completo.
        /// </remarks>
        [Obsolete("El saldo del documento destino ahora se actualiza vía DBRemiision.ActualizarRemision o DBFacturas.ActualizarFacturaAbono, según TipoDocumento.")]
        public void ActualizarRecibo2(string folio, decimal saldo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE Recibo SET Saldo = @Saldo WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>Actualiza el saldo restante de un Anticipo de Propietario/Cliente (el origen de la aplicación).</summary>
        public void ActualizarAnticipo(string folio, decimal saldo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE Anticipo SET Saldo = @Saldo WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>Actualiza el saldo restante de un Anticipo de Proveedor.</summary>
        public void ActualizarAnticipoProveedor(string folio, decimal saldo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE AnticipoProveedor SET Saldo = @Saldo WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>
        /// Obtiene el concepto (ConceptoCobroPago.IdConcepto) ya guardado en un
        /// AnticipoProveedor por su Folio, para usarlo como IdConceptoCobroPago
        /// al aplicar su saldo (<see cref="InsertarEgreso(string, string, string, string, decimal, string, string, decimal, decimal, int?)"/>).
        /// </summary>
        /// <remarks>
        /// El concepto de un Anticipo se define una sola vez, al darlo de alta
        /// (<see cref="RegistroAnticipoProveedor"/>) — aplicarlo después no
        /// cambia de qué se trata el anticipo, así que no tiene sentido
        /// volver a preguntarlo (con un combo, por ejemplo) en la pantalla de
        /// aplicación; simplemente se hereda el que ya tiene. Esto es lo
        /// mismo que hace el lado Clientes (AplicarAnticipoSaldo no vuelve a
        /// pedir concepto).
        ///
        /// AnticipoProveedor.Concepto es varchar(20); tras la migración a
        /// ConceptoCobroPago (ver 02_Migracion_AnticipoProveedor.sql) debería
        /// contener el IdConcepto como texto. Si todavía no se ha migrado, o
        /// el valor no es numérico, este método regresa null en vez de
        /// truncar con una excepción.
        /// </remarks>
        public int? ObtenerConceptoAnticipoProveedor(string folioAnticipo)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("SELECT Concepto FROM AnticipoProveedor WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folioAnticipo);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value && int.TryParse(resultado.ToString(), out int idConcepto))
                    {
                        return idConcepto;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
            return null;
        }

        #endregion

        #region Cancelación (eliminación lógica) de anticipos

        /// <summary>Marca un Anticipo de Propietario/Cliente como cancelado (Activo = 1).</summary>
        public void EliminarAnticipo(string folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE Anticipo SET Activo = 1 WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        /// <summary>Marca un Anticipo de Proveedor como cancelado (Activo = 1).</summary>
        public void EliminarAnticipoProveedor(string folio)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand("UPDATE AnticipoProveedor SET Activo = 1 WHERE Folio = @Folio", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        #endregion

        #region Validación de teclado (uso desde el formulario)

        /// <summary>
        /// Permite en un TextBox de captura de montos: números, puntuación y teclas
        /// de control (backspace, flechas, etc.); bloquea cualquier otro carácter.
        /// </summary>
        public void Monto(KeyPressEventArgs e)
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
            else
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}