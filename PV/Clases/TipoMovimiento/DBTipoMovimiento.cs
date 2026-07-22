using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PV.Clases.TipoMovimiento
{
    class DBTipoMovimiento
    {
        public static int Folio = 0;
        public static int Eliminado = 0;
        public static int Entrada = 0;
        public static int Salida = 0;
        public static int Traspaso = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBTipoMovimiento()
        {
            // Ya no se guarda una conexión abierta como campo de la clase.
            // Solo se valida que la cadena de conexión funcione.
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }

        //____________________________________________________________________________________________________________________________________________
        // Lógica común para obtener el último folio de un tipo de movimiento (E, S, T).
        // Antes esto se repetía 3 veces casi idéntico y además se ejecutaba la consulta 2 veces
        // (una con ExecuteReader solo para contar, y otra con SqlDataAdapter para traer el valor).
        // Ahora se hace una sola consulta con ExecuteScalar.
        private int ObtenerUltimoFolio(char tipoMovimiento, string clave, ref int valorEstatico)
        {
            int contador = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "select max(UltimoFolio) from TipoMovimiento where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", tipoMovimiento.ToString());
                        cmd.Parameters.AddWithValue("@Documento", clave);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            contador = 1;
                            valorEstatico = Convert.ToInt32(resultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return contador;
        }

        //Obtener la clave consecutiva
        public int ClaveDocumentoEntradaSiguiente(string Clave)
        {
            return ObtenerUltimoFolio('E', Clave, ref Entrada);
        }

        //Obtener la clave consecutiva
        public int ClaveDocumentoSalidaSiguiente(string Clave)
        {
            return ObtenerUltimoFolio('S', Clave, ref Salida);
        }

        //Obtener la clave consecutiva
        public int ClaveDocumentoTraspasoSiguiente(string Clave)
        {
            return ObtenerUltimoFolio('T', Clave, ref Traspaso);
        }

        //_________________________________________________________________________________________________________________________--
        // Lógica común de RegistroMovimiento / RegistroMovimientoST.
        // afectaCosto es null cuando el llamador no maneja ese campo (caso ST).
        private string GuardarMovimiento(string txtTipoMovimiento, string txtDocumento, string txtDescripcion,
            string cmbEstatus, string txtUltimoFolio, bool bloquear, bool? afectaCosto,
            string txtAlmacen, string txtNotas)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    int contador = 0;
                    using (SqlCommand cmdExiste = new SqlCommand(
                        "select count(*) from TipoMovimiento where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                    {
                        cmdExiste.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                        cmdExiste.Parameters.AddWithValue("@Documento", txtDocumento);
                        contador = Convert.ToInt32(cmdExiste.ExecuteScalar());
                    }

                    string bloqueo = bloquear ? "Si" : "No";

                    if (contador <= 0)
                    {
                        if (afectaCosto.HasValue)
                        {
                            string afecta = afectaCosto.Value ? "Si" : "No";
                            using (SqlCommand cmdInsert = new SqlCommand(
                                "Insert into TipoMovimiento (TipoMovimiento, Documento, Descripcion, Estatus, UltimoFolio, Bloquear, AfectaCosto, Almacen, Notas) " +
                                "values (@Tipo, @Documento, @Descripcion, @Estatus, @UltimoFolio, @Bloquear, @AfectaCosto, @Almacen, @Notas)", cn))
                            {
                                cmdInsert.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                                cmdInsert.Parameters.AddWithValue("@Documento", txtDocumento);
                                cmdInsert.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                                cmdInsert.Parameters.AddWithValue("@Estatus", cmbEstatus);
                                cmdInsert.Parameters.AddWithValue("@UltimoFolio", txtUltimoFolio);
                                cmdInsert.Parameters.AddWithValue("@Bloquear", bloqueo);
                                cmdInsert.Parameters.AddWithValue("@AfectaCosto", afecta);
                                cmdInsert.Parameters.AddWithValue("@Almacen", txtAlmacen);
                                cmdInsert.Parameters.AddWithValue("@Notas", txtNotas);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand cmdInsert = new SqlCommand(
                                "Insert into TipoMovimiento (TipoMovimiento, Documento, Descripcion, Estatus, UltimoFolio, Bloquear, Almacen, Notas) " +
                                "values (@Tipo, @Documento, @Descripcion, @Estatus, @UltimoFolio, @Bloquear, @Almacen, @Notas)", cn))
                            {
                                cmdInsert.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                                cmdInsert.Parameters.AddWithValue("@Documento", txtDocumento);
                                cmdInsert.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                                cmdInsert.Parameters.AddWithValue("@Estatus", cmbEstatus);
                                cmdInsert.Parameters.AddWithValue("@UltimoFolio", txtUltimoFolio);
                                cmdInsert.Parameters.AddWithValue("@Bloquear", bloqueo);
                                cmdInsert.Parameters.AddWithValue("@Almacen", txtAlmacen);
                                cmdInsert.Parameters.AddWithValue("@Notas", txtNotas);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        mensaje = "Registro guardado.";
                    }
                    else
                    {
                        if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos del Tipo de Movimiento",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (afectaCosto.HasValue)
                            {
                                string afecta = afectaCosto.Value ? "Si" : "No";
                                using (SqlCommand cmdUpdate = new SqlCommand(
                                    "Update TipoMovimiento set Descripcion=@Descripcion, Estatus=@Estatus, UltimoFolio=@UltimoFolio, " +
                                    "Bloquear=@Bloquear, AfectaCosto=@AfectaCosto, Almacen=@Almacen, Notas=@Notas " +
                                    "where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                                    cmdUpdate.Parameters.AddWithValue("@Estatus", cmbEstatus);
                                    cmdUpdate.Parameters.AddWithValue("@UltimoFolio", txtUltimoFolio);
                                    cmdUpdate.Parameters.AddWithValue("@Bloquear", bloqueo);
                                    cmdUpdate.Parameters.AddWithValue("@AfectaCosto", afecta);
                                    cmdUpdate.Parameters.AddWithValue("@Almacen", txtAlmacen);
                                    cmdUpdate.Parameters.AddWithValue("@Notas", txtNotas);
                                    cmdUpdate.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                                    cmdUpdate.Parameters.AddWithValue("@Documento", txtDocumento);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (SqlCommand cmdUpdate = new SqlCommand(
                                    "Update TipoMovimiento set Descripcion=@Descripcion, Estatus=@Estatus, UltimoFolio=@UltimoFolio, " +
                                    "Bloquear=@Bloquear, Almacen=@Almacen, Notas=@Notas " +
                                    "where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@Descripcion", txtDescripcion);
                                    cmdUpdate.Parameters.AddWithValue("@Estatus", cmbEstatus);
                                    cmdUpdate.Parameters.AddWithValue("@UltimoFolio", txtUltimoFolio);
                                    cmdUpdate.Parameters.AddWithValue("@Bloquear", bloqueo);
                                    cmdUpdate.Parameters.AddWithValue("@Almacen", txtAlmacen);
                                    cmdUpdate.Parameters.AddWithValue("@Notas", txtNotas);
                                    cmdUpdate.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                                    cmdUpdate.Parameters.AddWithValue("@Documento", txtDocumento);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }

                            mensaje = "Registro modificado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }

            return mensaje;
        }

        // registrar Movimiento (Entrada/Salida, maneja AfectaCosto)
        public string RegistroMovimiento(string txtTipoMovimiento, string txtDocumento, string txtDescripcion,
            string cmbEstatus, string txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear,
            RadioButton rdbSiAfectaCosto, RadioButton rdbNoAfectaCosto, string txtAlmacen, string txtNotas)
        {
            return GuardarMovimiento(txtTipoMovimiento, txtDocumento, txtDescripcion, cmbEstatus, txtUltimoFolio,
                rdbSiBloquear.Checked, rdbSiAfectaCosto.Checked, txtAlmacen, txtNotas);
        }

        // registrar Movimiento (Traspaso, sin AfectaCosto)
        public string RegistroMovimientoST(string txtTipoMovimiento, string txtDocumento, string txtDescripcion,
            string cmbEstatus, string txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear,
            string txtAlmacen, string txtNotas)
        {
            return GuardarMovimiento(txtTipoMovimiento, txtDocumento, txtDescripcion, cmbEstatus, txtUltimoFolio,
                rdbSiBloquear.Checked, null, txtAlmacen, txtNotas);
        }

        //________________________________________________________________________________________________
        // Lógica común de CargarEntrada / CargarSalida / CargarTraspaso (eran 3 copias idénticas salvo la letra).
        private void CargarPorTipo(DataGridView dgv, char tipoMovimiento)
        {
            try
            {
                dgv.Rows.Clear();

                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from TipoMovimiento where TipoMovimiento=@Tipo", cn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", tipoMovimiento.ToString());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow item in dt.Rows)
                            {
                                int n = dgv.Rows.Add();
                                dgv.Rows[n].Cells[0].Value = item["TipoMovimiento"].ToString();
                                dgv.Rows[n].Cells[1].Value = item["Documento"].ToString();
                                dgv.Rows[n].Cells[2].Value = item["Estatus"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //Movimiento Registrados
        public void CargarEntrada(DataGridView dgv)
        {
            CargarPorTipo(dgv, 'E');
        }

        //Movimiento Registrados
        public void CargarSalida(DataGridView dgv)
        {
            CargarPorTipo(dgv, 'S');
        }

        //Movimiento Registrados
        public void CargarTraspaso(DataGridView dgv)
        {
            CargarPorTipo(dgv, 'T');
        }

        //_____________________________________________________________________________________________________
        //Mostrar Usuario seleccionado
        public void ConsultaEntradaSeleccionado(string txtTipoMovimiento, string txtDocumento,
            Guna.UI2.WinForms.Guna2TextBox txtDescripcion, ComboBox cmbEstatus, Guna.UI2.WinForms.Guna2TextBox txtUltimoFolio,
            RadioButton rdbSiBloquear, RadioButton rdbNoBloquear, RadioButton rdbSiAfectaCosto, RadioButton rdbNoAfectaCosto,
            TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtNotas)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "select * from TipoMovimiento where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                        cmd.Parameters.AddWithValue("@Documento", txtDocumento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtDescripcion.Text = dr["Descripcion"].ToString();
                                cmbEstatus.Text = dr["Estatus"].ToString();
                                txtUltimoFolio.Text = dr["UltimoFolio"].ToString();

                                string bloqueo = dr["Bloquear"].ToString();
                                if (bloqueo == "Si")
                                    rdbSiBloquear.Checked = true;
                                else
                                    rdbNoBloquear.Checked = true;

                                string afecta = dr["AfectaCosto"].ToString();
                                if (afecta == "Si")
                                    rdbSiAfectaCosto.Checked = true;
                                else
                                    rdbNoAfectaCosto.Checked = true;

                                txtAlmacen.Text = dr["Almacen"].ToString();
                                txtNotas.Text = dr["Notas"].ToString();
                            }
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
        public void ConsultaSTSeleccionado(string txtTipoMovimiento, string txtDocumento,
            Guna.UI2.WinForms.Guna2TextBox txtDescripcion, Guna.UI2.WinForms.Guna2ComboBox cmbEstatus,
            Guna.UI2.WinForms.Guna2TextBox txtUltimoFolio, RadioButton rdbSiBloquear, RadioButton rdbNoBloquear,
            TextBox txtAlmacen, Guna.UI2.WinForms.Guna2TextBox txtNotas)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "select * from TipoMovimiento where TipoMovimiento=@Tipo and Documento=@Documento", cn))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", txtTipoMovimiento);
                        cmd.Parameters.AddWithValue("@Documento", txtDocumento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtDescripcion.Text = dr["Descripcion"].ToString();
                                cmbEstatus.Text = dr["Estatus"].ToString();
                                txtUltimoFolio.Text = dr["UltimoFolio"].ToString();

                                string bloqueo = dr["Bloquear"].ToString();
                                if (bloqueo == "Si")
                                    rdbSiBloquear.Checked = true;
                                else
                                    rdbNoBloquear.Checked = true;

                                txtAlmacen.Text = dr["Almacen"].ToString();
                                txtNotas.Text = dr["Notas"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //___________________________________________________________________________________________________________________
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

        //______________________________________________________________________________________________________________________________
        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.Items.Clear();

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "select (convert(varchar, Clave) + ' - ' + Nombre) as Nombre from Almacenes", cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cb.Items.Add(dr[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen(string Documento)
        {
            string[] resultado = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "Select Clave from Almacenes where (convert(varchar, Clave) + ' - ' + Nombre) = @Documento", cn))
                    {
                        cmd.Parameters.AddWithValue("@Documento", Documento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                resultado = new string[] { dr[0].ToString() };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }

            return resultado;
        }

        //______________________________________________________________________________________________________________________________
        public string[] InformacionAlmacen2(string Documento)
        {
            string[] resultado = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "Select (convert(varchar, Clave) + ' - ' + Nombre) from Almacenes where Clave=@Documento", cn))
                    {
                        cmd.Parameters.AddWithValue("@Documento", Documento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                resultado = new string[] { dr[0].ToString() };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }

            return resultado;
        }

        public void ValidarDocumentoSPR()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ObtenerCn()))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM TipoMovimiento WHERE Documento = 'SPR' AND TipoMovimiento = 'S') " +
                        "BEGIN " +
                        "    INSERT INTO TipoMovimiento VALUES ('S', 'SPR', 'SALIDA POR REMISIÓN', 'Activo', '0', 'Si', 'Si', '', '') " +
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
    }
}