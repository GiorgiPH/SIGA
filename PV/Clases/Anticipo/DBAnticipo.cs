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
    /// Acceso a datos del módulo de Anticipos (Clientes/Propietarios y Proveedores).
    ///
    /// Cambios respecto a la versión original (ver detalle completo en el chat):
    ///  - Ya no se mantiene una SqlConnection abierta como campo de instancia durante
    ///    toda la vida del formulario. Cada operación abre su propia conexión con
    ///    "using" y la libera automáticamente (igual con SqlCommand/SqlDataReader/SqlDataAdapter).
    ///  - Todas las consultas usan parámetros (SqlParameter) en vez de concatenar texto,
    ///    eliminando el riesgo de inyección SQL.
    ///  - Se corrigió CultureInfo("US-AR") -> "en-US" en CargarReciboProveedor y CargarEgreso2
    ///    (esa culture no existe en .NET y lanzaba CultureNotFoundException al imprimir
    ///    recibos de proveedor / cargar egresos).
    ///  - Los métodos "Informacion..." ya no devuelven null cuando no hay coincidencias
    ///    (antes provocaba NullReferenceException en el formulario al hacer valores[0]).
    ///  - ClaveProductoSiguiente/2 e InsertarCobroGeneral/Proveedor ya no ejecutan la
    ///    misma consulta dos veces (se usa ExecuteScalar una sola vez).
    ///  - La lógica de negocio (qué se inserta, qué se actualiza, cuándo se pregunta,
    ///    qué mensajes se muestran) se mantiene intacta.
    /// </summary>
    class DBAnticipo
    {
        public static int Folio = 0;

        #region Configuración de conexión

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBAnticipo()
        {
            // Se conserva el comportamiento original: validar la conexión al crear el
            // objeto para avisar de inmediato si la base de datos no está disponible.
            // A diferencia del original, esta conexión de prueba se cierra al instante
            // (no se deja abierta para toda la vida del formulario).
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

        // Obtiene el folio máximo de Anticipo y lo deja en la propiedad estática Folio.
        // El formulario (RegistrarAnticipo.GenerarNuevoFolio) es quien suma 1.
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

        // NOTA: esta consulta une Anticipo con Propietarios/Propietarios_Condominios.
        // Se usa en btnConfirmarAnticipo_Click y txtFiltro_TextChanged cuando Opcion=="Propietario",
        // mientras que el constructor y Limpiar() usan CargarAnticipoCliente (join contra Clientes)
        // para ese mismo flujo. Ver aviso en el chat: esto ya existía en el código original,
        // se conserva tal cual para no alterar el comportamiento actual.
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

        #region Consulta de un anticipo seleccionado (doble clic en el grid)

        // Devuelve el valor de "Activo" ("1" = cancelado). Se conserva exactamente
        // el mismo conjunto de columnas y asignaciones que el original (incluyendo
        // que ImporteMXN se llena con "Importe", no con la columna calculada ImporteMXN).
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
                            txtConcepto.SelectedValue= dr["Concepto"].ToString();
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

        public void ConsultaConceptoAnticipo(Guna2TextBox txtconcepto, TextBox txtconceptoclave)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT DE.ConceptoAnt, (DE.ConceptoAnt + ' - ' + CI.Descripcion) AS Nombre " +
                    "FROM DatosEmpresa AS DE, ConceptosIngreso AS CI WHERE DE.ConceptoAnt = CI.Clave", cn))
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

        public string[] InformacionConcepto(string documento)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT (CI.Clave + ' - ' + CI.Descripcion) AS Nombre FROM ConceptosIngreso AS CI WHERE CI.Clave = @Documento", cn))
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
                    "SELECT A.*, CI.Descripcion FROM Anticipo AS A, ConceptosIngreso AS CI " +
                    "WHERE A.Concepto = CI.Clave AND A.ClavePropietario = @Matricula AND A.Saldo > 0 " +
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

        // CORRECCIÓN: la culture "US-AR" no existe en .NET y lanzaba
        // CultureNotFoundException cada vez que se imprimía un recibo de proveedor.
        // Se cambió a "en-US" (mismo formato que usa CargarReciboAlumno).
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
                    "SELECT A.*, CI.Descripcion FROM AnticipoProveedor AS A, ConceptosIngreso AS CI " +
                    "WHERE A.Concepto = CI.Clave AND A.ClaveProveedor = @ClaveProveedor AND A.Saldo > 0", cn))
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
                            dgv.Rows[n].Cells[5].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos1" + ex.ToString());
            }
        }

        public void CargarReciboAlumno2(DataGridView dgv, string claveProveedor)
        {
            try
            {
                NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
                formato.CurrencyGroupSeparator = ",";
                formato.NumberDecimalSeparator = ".";

                dgv.Rows.Clear();
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT R.*, D.Nombre FROM Remision AS R, Documento AS D " +
                    "WHERE ClaveProveedor = @ClaveProveedor AND R.ClaveDocumento = D.Clave AND R.Saldo > 0", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Conceptos aqui" + ex.ToString());
            }
        }

        // CORRECCIÓN: misma culture inválida "US-AR" -> "en-US".
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
                    "(SELECT 'P' AS Tipo, R.*, D.Nombre FROM RecepcionProducto AS R, Documento AS D " +
                    " WHERE ClaveProveedor = @ClaveProveedor AND Saldo <> 0 AND R.ClaveDocumento = D.Clave) " +
                    "UNION " +
                    "(SELECT 'G' AS Tipo, R.*, D.Nombre FROM RegistroGastos AS R, Documento AS D " +
                    " WHERE ClaveProveedor = @ClaveProveedor AND Saldo <> 0 AND R.ClaveDocumento = D.Clave) " +
                    "UNION " +
                    "(SELECT 'NCG' AS Tipo, R.*, '' AS DiasVence, '' AS FechaVence, D.Nombre FROM NotasGasto AS R, Documento AS D " +
                    " WHERE ClaveProveedor = @ClaveProveedor AND Saldo <> 0 AND R.ClaveDocumento = D.Clave)", cn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            int n = dgv.Rows.Add();
                            dgv.Rows[n].Cells[0].Value = item["Tipo"].ToString();
                            dgv.Rows[n].Cells[1].Value = item["Folio"].ToString();
                            dgv.Rows[n].Cells[2].Value = item["ClaveDocumento"].ToString();
                            dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                            dgv.Rows[n].Cells[4].Value = Convert.ToDecimal(item["Saldo"]).ToString("N", formato);
                            dgv.Rows[n].Cells[6].Value = Convert.ToDecimal(0.00).ToString("N", formato);
                            dgv.Rows[n].Cells[7].Value = Convert.ToDecimal(0.00).ToString("N", formato);
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

        public void InsertarCobro(string folio, string claveProperietario, string fecha, decimal pago, string folioGeneral,
            string anticipo, decimal saldo, decimal descuentoPago)
        {
            try
            {
                using (SqlConnection cn = AbrirConexion())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO AnticipoCobros (Folio, ClavePropietario, Fecha, Pago, FolioGeneral, Anticipo, SaldoRestante, DescuentoPago) " +
                    "VALUES (@Folio, @ClavePropietario, @Fecha, @Pago, @FolioGeneral, @Anticipo, @SaldoRestante, @DescuentoPago)", cn))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@ClavePropietario", claveProperietario);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Pago", pago);
                    cmd.Parameters.AddWithValue("@FolioGeneral", folioGeneral);
                    cmd.Parameters.AddWithValue("@Anticipo", anticipo);
                    cmd.Parameters.AddWithValue("@SaldoRestante", saldo);
                    cmd.Parameters.AddWithValue("@DescuentoPago", descuentoPago);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex.ToString());
            }
        }

        #endregion

        #region Actualización de saldos

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

        public void ActualizarEgreso2(string tipo, string folio, decimal saldo)
        {
            string tabla = null;
            if (tipo == "P") tabla = "RecepcionProducto";
            else if (tipo == "G") tabla = "RegistroGastos";
            else if (tipo == "NCG") tabla = "NotasGasto";

            if (tabla == null)
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

        #endregion

        #region Cancelación (eliminación lógica) de anticipos

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

        // Se conserva igual que el original (permite números, puntuación y teclas de
        // control; bloquea separadores). Se quitó el try/catch que solo relanzaba la
        // excepción sin aportar nada.
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