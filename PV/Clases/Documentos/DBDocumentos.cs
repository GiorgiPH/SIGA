using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;

namespace Condominios.Clases.Documentos
{
    /// <summary>
    /// Acceso a datos y soporte de UI para el catálogo de Documento.
    ///
    /// Misma clase, mismas firmas públicas, misma secuencia de negocio
    /// (mismos mensajes, mismas confirmaciones Sí/No, mismo orden de
    /// validaciones). Lo que cambió es la implementación interna:
    ///
    /// - Cada operación abre su propia conexión con "using" y la cierra
    ///   siempre, incluso si hay una excepción. Ya no existe una conexión
    ///   de instancia abierta durante toda la vida del objeto.
    /// - Todas las consultas están parametrizadas (@parametro). No hay
    ///   concatenación de valores dentro del texto SQL en ningún método.
    /// - Los conteos "leer todas las filas y contarlas en un while" se
    ///   reemplazaron por EXISTS, que resuelven lo mismo en un solo
    ///   round-trip al servidor.
    /// - EliminarDivisa ahora valida y borra dentro de una misma
    ///   transacción, así se elimina la ventana entre "puede eliminarse"
    ///   y el DELETE que existía en la versión anterior.
    /// </summary>
    class DBDocumentos
    {
        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        /// <summary>
        /// Se conserva por compatibilidad con código que ya la invoca después
        /// de usar la clase. Ya no hay una conexión de instancia que cerrar:
        /// cada método abre y cierra la suya con "using".
        /// </summary>
        public void CerrarConexion()
        {
            // No-op intencional.
        }

        public DBDocumentos()
        {
            // Ya no se abre conexión aquí: construir el objeto no debe poder
            // fallar por un problema de red o de credenciales. Cada método
            // abre su propia conexión en el momento en que la necesita.
        }

        #region Registro y actualización

        //_________________________________________________________________________________________________________________________--
        // registrar documento
        public string RegistroDocumento(string TipoDocumento, string Clase, string Clave, string Nombre, string Almace, string UltimoFolio, string Consecutivo, string Bloquear, string Cuenta, string Cuenta2, string tarea, bool CentroCosto)
        {
            string mensaje = "";

            try
            {
                bool existe = ExisteDocumento(TipoDocumento, Clave);

                if (!existe)
                {
                    InsertarDocumento(TipoDocumento, Clase, Clave, Nombre, Almace, UltimoFolio, Consecutivo, Bloquear, Cuenta, Cuenta2, tarea, CentroCosto);
                    mensaje = "Registro guardado.";
                }
                else
                {
                    bool folioSinUsar = FolioSinUsar(TipoDocumento, Clave);

                    if (!folioSinUsar)
                    {
                        if (MessageBox.Show("El documento ya tiene folios registrados solo es posible modificar los numeros de cuenta", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ActualizarCuentas(TipoDocumento, Clave, Cuenta, Cuenta2);
                            mensaje = "Registro modificado.";
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("El documento ya existe, si continua sera modificado", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ActualizarDocumentoCompleto(TipoDocumento, Clase, Clave, Almace, UltimoFolio, Consecutivo, Bloquear, Cuenta, Cuenta2, tarea, CentroCosto);
                            mensaje = "Registro modificado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return mensaje;
        }

        private bool ExisteDocumento(string tipoDocumento, string clave)
        {
            const string sql = @"SELECT CASE WHEN EXISTS (
                                      SELECT 1 FROM Documento
                                      WHERE Clave = @Clave AND TipoDocumento = @TipoDocumento
                                  ) THEN 1 ELSE 0 END";

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Clave", clave);
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);

                connection.Open();
                return (int)command.ExecuteScalar() == 1;
            }
        }

        private bool FolioSinUsar(string tipoDocumento, string clave)
        {
            const string sql = @"SELECT CASE WHEN EXISTS (
                                      SELECT 1 FROM Documento
                                      WHERE Clave = @Clave AND TipoDocumento = @TipoDocumento AND UltimoFolio = '0'
                                  ) THEN 1 ELSE 0 END";

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Clave", clave);
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);

                connection.Open();
                return (int)command.ExecuteScalar() == 1;
            }
        }

        private void InsertarDocumento(string tipoDocumento, string clase, string clave, string nombre, string almace, string ultimoFolio, string consecutivo, string bloquear, string cuenta, string cuenta2, string tarea, bool centroCosto)
        {
            const string sql = @"INSERT INTO Documento
                (TipoDocumento, Clase, Clave, Nombre, Almace, UltimoFolio, Consecutivo, Bloquear, Cuenta, Cuenta2, Tarea, MostrarCentroCosto)
                VALUES
                (@TipoDocumento, @Clase, @Clave, @Nombre, @Almace, @UltimoFolio, @Consecutivo, @Bloquear, @Cuenta, @Cuenta2, @Tarea, @MostrarCentroCosto)";

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento ?? string.Empty);
                command.Parameters.AddWithValue("@Clase", clase ?? string.Empty);
                command.Parameters.AddWithValue("@Clave", clave ?? string.Empty);
                command.Parameters.AddWithValue("@Nombre", nombre ?? string.Empty);
                command.Parameters.AddWithValue("@Almace", almace ?? string.Empty);
                command.Parameters.AddWithValue("@UltimoFolio", ultimoFolio ?? string.Empty);
                command.Parameters.AddWithValue("@Consecutivo", consecutivo ?? string.Empty);
                command.Parameters.AddWithValue("@Bloquear", bloquear ?? string.Empty);
                command.Parameters.AddWithValue("@Cuenta", cuenta ?? string.Empty);
                command.Parameters.AddWithValue("@Cuenta2", cuenta2 ?? string.Empty);
                command.Parameters.AddWithValue("@Tarea", tarea ?? string.Empty);
                command.Parameters.Add("@MostrarCentroCosto", SqlDbType.Bit).Value = centroCosto;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void ActualizarCuentas(string tipoDocumento, string clave, string cuenta, string cuenta2)
        {
            const string sql = @"UPDATE Documento SET Cuenta = @Cuenta, Cuenta2 = @Cuenta2
                                  WHERE Clave = @Clave AND TipoDocumento = @TipoDocumento";

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Cuenta", cuenta ?? string.Empty);
                command.Parameters.AddWithValue("@Cuenta2", cuenta2 ?? string.Empty);
                command.Parameters.AddWithValue("@Clave", clave);
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void ActualizarDocumentoCompleto(string tipoDocumento, string clase, string clave, string almace, string ultimoFolio, string consecutivo, string bloquear, string cuenta, string cuenta2, string tarea, bool centroCosto)
        {
            const string sql = @"UPDATE Documento SET
                                    Tarea = @Tarea, TipoDocumento = @TipoDocumento, Clase = @Clase,
                                    Almace = @Almace, UltimoFolio = @UltimoFolio, Consecutivo = @Consecutivo,
                                    Bloquear = @Bloquear, Cuenta = @Cuenta, Cuenta2 = @Cuenta2,
                                    MostrarCentroCosto = @MostrarCentroCosto
                                  WHERE Clave = @Clave AND TipoDocumento = @TipoDocumento";

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Tarea", tarea ?? string.Empty);
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento ?? string.Empty);
                command.Parameters.AddWithValue("@Clase", clase ?? string.Empty);
                command.Parameters.AddWithValue("@Almace", almace ?? string.Empty);
                command.Parameters.AddWithValue("@UltimoFolio", ultimoFolio ?? string.Empty);
                command.Parameters.AddWithValue("@Consecutivo", consecutivo ?? string.Empty);
                command.Parameters.AddWithValue("@Bloquear", bloquear ?? string.Empty);
                command.Parameters.AddWithValue("@Cuenta", cuenta ?? string.Empty);
                command.Parameters.AddWithValue("@Cuenta2", cuenta2 ?? string.Empty);
                command.Parameters.Add("@MostrarCentroCosto", SqlDbType.Bit).Value = centroCosto;
                command.Parameters.AddWithValue("@Clave", clave ?? string.Empty);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Consultas

        //________________________________________________________________________________________________
        //Documentos Registrados
        public void CargarDocumentos(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();

                // Antes: "Select * from Documento" y solo se usaban 4 columnas.
                // Antes: "Select * from Documento" y solo se usaban 4 columnas.
                // Se piden únicamente las columnas que el grid consume.
                const string sql = "SELECT TipoDocumento, Clase, Clave, Nombre FROM Documento order by tipoDocumento";

                using (var connection = new SqlConnection(ObtenerCn()))
                using (var command = new SqlCommand(sql, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt); // SqlDataAdapter abre y cierra la conexión por sí solo.

                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["TipoDocumento"].ToString();
                        dgv.Rows[n].Cells[1].Value = item["Clase"].ToString();
                        dgv.Rows[n].Cells[2].Value = item["Clave"].ToString();
                        dgv.Rows[n].Cells[3].Value = item["Nombre"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaDocumentoSeleccionado(ComboBox TipoDocumento, ComboBox Clase, string Clave, string Nombre, Guna2TextBox Almace, Guna2TextBox UltimoFolio, Guna2ToggleSwitch tgConsecutivo, Guna2ToggleSwitch tgBloquear, Guna2TextBox Cuenta, Guna2TextBox Cuenta2, ComboBox Tarea, Guna2ToggleSwitch tgCentroCosto)
        {
            const string sql = "SELECT * FROM Documento WHERE Clave = @Clave AND Nombre = @Nombre";

            try
            {
                using (var connection = new SqlConnection(ObtenerCn()))
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Clave", Clave);
                    command.Parameters.AddWithValue("@Nombre", Nombre);

                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            TipoDocumento.Text = reader["TipoDocumento"].ToString();

                            Almace.Text = reader["Almace"].ToString();
                            UltimoFolio.Text = reader["UltimoFolio"].ToString();

                            string consecutivo = reader["Consecutivo"].ToString();
                            if (consecutivo == "Si")
                            {
                                tgConsecutivo.Checked = true;
                            }
                            else if (consecutivo == "No")
                            {
                                tgConsecutivo.Checked = false;
                            }

                            string bloqueo = reader["Bloquear"].ToString();
                            if (bloqueo == "Si")
                            {
                                tgBloquear.Checked = true;
                            }
                            else if (bloqueo == "No")
                            {
                                tgBloquear.Checked = false;
                            }

                            tgCentroCosto.Checked = Convert.ToBoolean(reader["MostrarCentroCosto"]);

                            Cuenta.Text = reader["Cuenta"].ToString();
                            Cuenta2.Text = reader["Cuenta2"].ToString();
                            Clase.Text = reader["Clase"].ToString();
                            Tarea.Text = reader["Tarea"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaDocumentoSeleccionado2(ComboBox Clase, string Clave, string Nombre)
        {
            const string sql = "SELECT Clase FROM Documento WHERE Clave = @Clave AND Nombre = @Nombre";

            try
            {
                using (var connection = new SqlConnection(ObtenerCn()))
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Clave", Clave);
                    command.Parameters.AddWithValue("@Nombre", Nombre);

                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            Clase.Text = reader["Clase"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public DataTable ConsultarDocumento(string tipo = null, string clase = null, string tarea =null)
        {
            string query = "SELECT * FROM Documento WHERE 1=1";
            var parametros = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tipo))
            {
                query += " AND TipoDocumento = @TipoDocumento";
                parametros.Add(new SqlParameter("@TipoDocumento", tipo));
            }
            if (!string.IsNullOrEmpty(clase))
            {
                query += " AND clase = @Clase";
                parametros.Add(new SqlParameter("@Clase", clase));
            }
            if (!string.IsNullOrEmpty(tarea))
            {
                query += " AND tarea = @Tarea";
                parametros.Add(new SqlParameter("@Tarea", tarea));
            }

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(ObtenerCn()))
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddRange(parametros.ToArray());
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        #endregion

        #region Utilidades de UI

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
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Eliminación

        //_________________________________________________________________________________________________________________________--
        // eliminar documento
        public string EliminarDivisa(string txtClaveDivisa, string txtClase, string txtTipoDocumento)
        {
            string mensaje = string.Empty;

            const string sqlPuedeEliminar = @"SELECT CASE WHEN EXISTS (
                    SELECT 1 FROM Documento AS D
                    WHERE D.Clave = @Clave AND D.Clase = @Clase AND D.TipoDocumento = @TipoDocumento
                      AND NOT EXISTS (SELECT 1 FROM DatosEmpresa AS DE WHERE D.Clave = DE.Documento)
                      AND NOT EXISTS (SELECT 1 FROM Recibo AS R WHERE D.Clave = R.ClaveDocumento)
                      AND NOT EXISTS (SELECT 1 FROM OrdenCompra AS R WHERE D.Clave = R.ClaveDocumento)
                      AND NOT EXISTS (SELECT 1 FROM RecepcionProducto AS R WHERE D.Clave = R.ClaveDocumento)
                      AND NOT EXISTS (SELECT 1 FROM RegistroGastos AS R WHERE D.Clave = R.ClaveDocumento)
                      AND NOT EXISTS (SELECT 1 FROM Requisicion AS R WHERE D.Clave = R.ClaveDocumento)
                      AND NOT EXISTS (SELECT 1 FROM NotasGasto AS R WHERE D.Clave = R.ClaveDocumento)
                ) THEN 1 ELSE 0 END";

            const string sqlEliminar = "DELETE FROM Documento WHERE Clave = @Clave AND TipoDocumento = @TipoDocumento AND Clase = @Clase";

            try
            {
                using (var connection = new SqlConnection(ObtenerCn()))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            bool puedeEliminar;
                            using (var command = new SqlCommand(sqlPuedeEliminar, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Clave", txtClaveDivisa);
                                command.Parameters.AddWithValue("@Clase", txtClase);
                                command.Parameters.AddWithValue("@TipoDocumento", txtTipoDocumento);
                                puedeEliminar = (int)command.ExecuteScalar() == 1;
                            }

                            if (!puedeEliminar)
                            {
                                transaction.Rollback();
                                return "El registro esta en uso, no es posible eliminar";
                            }

                            using (var command = new SqlCommand(sqlEliminar, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Clave", txtClaveDivisa);
                                command.Parameters.AddWithValue("@TipoDocumento", txtTipoDocumento);
                                command.Parameters.AddWithValue("@Clase", txtClase);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            mensaje = "Registro Eliminado.";
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("El registro esta en uso, no es posible eliminar");
            }

            return mensaje;
        }

        #endregion
    }
}