using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas;
using PuntoVentas.Clases.DatosEmpresa;
using PV;
using PV.Clases;
using PV.Clases.ConceptoPago;
using PV.Clases.Remision;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarCobro : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        DBRemiision r = new DBRemiision();
        DBConceptoCobroPago dbConceptoCobroPago = new DBConceptoCobroPago();
        ArrayList Lista;
        DBDatosEmpresa d = new DBDatosEmpresa();
        public static string Carpeta = string.Empty;
        public static int CobroRealizado = 0;

        // TODO: confirmar el IdClase real de "Ingresos" en
        // CAT_ClaseConceptoTesoreria (dbConceptoCobroPago.ObtenerPorId /
        // Listar pueden ayudar a verificarlo). Se deja en 1 como marcador.
        private const byte IdClaseIngresos = 2;

        string ext = string.Empty;
        public static decimal DescuentoPago;
        string tipo = string.Empty;
        string monto = string.Empty;
        string[] datosE = null;

        public RegistrarCobro(ArrayList ListaConcep, string Matricula, string Alumno, string Fecha, string tipo, string monto)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            Lista = ListaConcep;
            dtpFecha.Text = Fecha;
            this.tipo = tipo;
            this.monto = monto;
            datosE = d.CorreoContra();
            if (tipo == "M")
            {
                label5.Visible = true;
                txtMontoPermitido.Visible = true;
                txtMontoPermitido.Text = this.monto;
            }
        }

        private void RegistrarCobro_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlAcademicoDataSet14.FormasPago' Puede moverla o quitarla según sea necesario.

            r.CargarReciboCobroById(dgvPagosPendientes, txtMatricula.Text, Lista);
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);

            // Nombre asumido para el combo agregado en el diseñador
            // ("cmbConceptoIngreso"): si le pusiste otro nombre, ajusta
            // esta referencia (y la de button5_Click) para que coincida.
            DataTable conceptosIngreso = dbConceptoCobroPago.ListarParaCombo(IdClaseIngresos, soloActivos: true);
            ComboUtil.LlenarComboBox(cmbConceptoIngreso, conceptosIngreso, "Descripcion", "IdConcepto");

        }

        /// <summary>
        /// Valida que la suma de Abono de todas las filas no exceda el monto
        /// permitido (solo aplica cuando tipo == "M", traspaso entre
        /// documentos con un monto fijo a repartir). Si se excede, resetea
        /// la celda "Abono" de la fila editada y avisa.
        /// Antes recibía también qué celda resetear porque existía una
        /// segunda variante para la columna "AbonoRecargo" (ya eliminada);
        /// al quedar solo "Abono" ese parámetro ya no hace falta.
        /// </summary>
        private void ValidarMontoPermitido(int rowIndex)
        {
            if (tipo != "M")
            {
                return;
            }

            decimal total = 0.00m;
            foreach (DataGridViewRow r in dgvPagosPendientes.Rows)
            {
                total += Convert.ToDecimal(r.Cells["Abono"].Value.ToString());
            }

            if (total > Convert.ToDecimal(txtMontoPermitido.Text))
            {
                dgvPagosPendientes.Rows[rowIndex].Cells["Abono"].Value = 0.00;
                MessageBox.Show("excediste el monto permitido");
            }
        }

        /// <summary>
        /// Recalcula los totales del encabezado (Importe total y total
        /// pagado) recorriendo todas las filas.
        /// </summary>
        private void RecalcularTotalesEncabezado(NumberFormatInfo formato)
        {
            decimal totalImporte = 0.00M;
            decimal totalAbono = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                totalImporte += Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                txtImporteTotal.Text = totalImporte.ToString("N", formato);

                totalAbono += Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                txtTotalPagado.Text = totalAbono.ToString("N", formato);
            }
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].ReadOnly = true;

            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "Abono")
            {
                ValidarMontoPermitido(e.RowIndex);

                // Ya no existe una columna separada de "saldo capital" (el
                // diseñador solo tiene FormaPago, FolioDocumento, Documento,
                // Concepto, Importe, MasAbono, Abono, Saldo). El tope para
                // el abono pasa a ser el Importe del documento, que en este
                // grid ("Lista de Pagos" pendientes) ya representa lo
                // pendiente por pagar. Avísame si Importe no es el pendiente
                // real y hay que traer otro dato desde DBRemiision.
                decimal importeDocumento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString());
                decimal abonoCapital = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString());

                if (importeDocumento < abonoCapital)
                {
                    MessageBox.Show("El abono a capital no puede ser mayor al saldo capital");
                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value = 0.00m;
                    return;
                }
            }

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString());
            }
            if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString());
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value = 0;
            }

            Saldo = Importe - Abono;

            string saldo = Saldo.ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells["Saldo"].Value = saldo.ToString();

            RecalcularTotalesEncabezado(formato);

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value = abono.ToString("N", formato);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {

                if (MessageBox.Show("¿Finalizar cobro?", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cmbCuentaBancaria.Text == string.Empty)
                    {
                        MessageBox.Show("Seleccione la cuenta bancaria");
                        return;
                    }

                    string conceptoSeleccionado = ComboUtil.ObtenerSelectedValue(cmbConceptoIngreso);
                    if (conceptoSeleccionado == null)
                    {
                        MessageBox.Show("Seleccione el concepto de ingreso");
                        return;
                    }
                    int idConceptoCobroPago = Convert.ToInt32(conceptoSeleccionado);

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        try
                        {
                            if (row.Cells["FormaPago"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Registrar forma de pago para todos los pagos");
                                return;
                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Registrar forma de pago para todos los pagos");
                            return;
                        }
                    }
                    if (tipo == "M")
                    {
                        decimal total = 0.00m;
                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            total += Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                        }
                        if (total < Convert.ToDecimal(txtMontoPermitido.Text))
                        {
                            MessageBox.Show("Es necesario completar el monto de traspaso");
                            return;
                        }
                    }
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Abono"].Value.ToString() == "0.00"
                            || row.Cells["Abono"].Value.ToString() == string.Empty
                            || Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) < 0)
                        {
                            MessageBox.Show("No es posible realizar un abono igual o menor a 0");
                            return;
                        }
                        //else if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                        //{
                        //    //MessageBox.Show("El Abono no puede ser mayor al Importe");
                        //    MessageBox.Show("El abono a capital no puede ser mayor al capital");
                        //    return;
                        //}
                        //else if (Convert.ToDecimal(row.Cells["SaldoCapital"].Value.ToString()) < Convert.ToDecimal(row.Cells["Abono"].Value.ToString()))
                        //{
                        //    MessageBox.Show("El Abono a Capital no puede ser mayor al Saldo de Capital");
                        //    return;
                        //}

                    }

                    c.InsertarCobroGeneral(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                    DateTime FechaHoy = DateTime.Now;
                    string Hoy = FechaHoy.ToString("yyyy/MM/dd");
                    string Documento = string.Empty;
                    int cont = 0;
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {

                    
                        if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                        {
                            row.Cells["Saldo"].Value = "0.00";
                        }

                        // Recargo y Descuento ya no se manejan desde esta pantalla
                        // (columnas retiradas del grid); se mandan en 0.00 para no
                        // cambiar la firma de ActualizarRemision/InsertarCobro sin
                        // ver DBRemiision.cs primero.
                        r.ActualizarRemision(row.Cells["FolioDocumento"].Value.ToString(), 0.00m, 0.00m, Convert.ToDecimal(row.Cells["Abono"].Value.ToString()));
                        c.InsertarCobro(row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, dtpFecha.Text, txtObservaciones.Text, row.Cells["FormaPago"].Value.ToString(), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()), txtReferncia.Text, txtNumOperacion.Text, txtNumAutorizacion.Text, txtCuenta.Text, txtFolioGeneral.Text, 0.00m, DescuentoPago, Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()), tipo, idConceptoCobroPago);
                        c.ModificarExtension(txtFolioGeneral.Text, ext);
                        c.ActualizarArchivoCobro(txtFolioGeneral.Text, txtArchivo.Text);
                        //````````````c.insertLogcobros(Login.UsuarioLogin, DateTime.Now.ToString("yyyy/MM/dd"), row.Cells["FolioDocumento"].Value.ToString(), row.Cells["Documento"].Value.ToString(), "", Convert.ToDecimal(row.Cells["Importe"].Value).ToString(), "0.00", "0.00", "0.00", "0.00", DescuentoPago.ToString(), Convert.ToDecimal(row.Cells["Saldo"].Value).ToString());




                        /*Llena la tabla log cobros*/
                        /*-----------------------------------------*/
                        cont++;

                        Documento = row.Cells["Documento"].Value.ToString();

                    }
                    if (cont > 0)
                    {
                        if (MessageBox.Show("¿Imprimir Recibo?", "Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ReciboCobranza reciboCobranza = new ReciboCobranza(txtFolioGeneral.Text, txtMatricula.Text);
                            reciboCobranza.ShowDialog();



                        }
                    }

                    registroIngresos.nombre = string.Empty;
                    registroIngresos.matricula = string.Empty;
                    CobroRealizado = 1;
                    button5.Enabled = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione la cuenta bancaria para continuar");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                //&c.Actualizarcancelaciono2(row.Cells["FolioDocumento"].Value.ToString());
            }

            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {


                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["FormaPago"].Value == null)
                {
                    MessageBox.Show("Registrar Forma de Pago");
                    return;
                }
                else if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString() == "0.00")
                {
                    MessageBox.Show("No existe saldo capital por pagar");
                    return;
                }

                else
                {
                    decimal AbonoCapital = 0.00m;
                    if (tipo == "M")
                    {
                        AbonoCapital = Convert.ToDecimal(txtMontoPermitido.Text);

                    }
                    else
                    {
                        AbonoCapital = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value);

                    }
                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value = AbonoCapital.ToString();
                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }

            }

        }

        private void cmbCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta(cmbCuentaBancaria.Text);
                txtCuenta.Text = valores[0];
            }
        }

        private void txtTotalPagado_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalPagado.Text == "0.00" || txtTotalPagado.Text == "0")
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
        }

        /// <summary>
        /// Ruta base donde se guardan los adjuntos de este cobro, con la
        /// misma convención que tenía el original ("G" + matrícula dentro
        /// de la carpeta configurada en Parametros->Datos Condominio). Solo
        /// arma la ruta, no crea la carpeta (eso se hace explícitamente en
        /// button11_Click, igual que en el código original).
        /// </summary>
        private string ObtenerCarpetaAdjuntos(string matricula)
        {
            return Path.Combine(datosE[6], "G" + matricula);
        }

        /// <summary>
        /// Verifica que exista una ruta base de adjuntos configurada
        /// (Parametros->Datos Condominio). Además de la validación original
        /// (cadena vacía), se agrega una validación de que "datosE" no sea
        /// null ni tenga menos de 7 elementos: si CorreoContra() llegara a
        /// regresar null o un arreglo corto, el código original tronaría
        /// con NullReferenceException/IndexOutOfRangeException apenas se
        /// usa "datosE[6]"; con este cambio simplemente se muestra el mismo
        /// mensaje que ya existía para "ruta no configurada".
        /// </summary>
        private bool RutaAdjuntosConfigurada()
        {
            if (datosE == null || datosE.Length <= 6 || string.IsNullOrEmpty(datosE[6]))
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return false;
            }
            return true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!RutaAdjuntosConfigurada())
            {
                return;
            }

            if (txtMatricula.Text == string.Empty)
            {
                MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                return;
            }

            string descripcion = dtpFecha.Text;
            Carpeta = ObtenerCarpetaAdjuntos(txtMatricula.Text);
            ArchivoUtil.CrearCarpetaSiNoExiste(Carpeta);

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All Files|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivoOrigen = open.FileName;
                ext = Path.GetExtension(archivoOrigen);
                try
                {
                    txtArchivo.Text = ArchivoUtil.GuardarAdjunto(archivoOrigen, Carpeta, descripcion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ya hay un archivo guardado" + ex.ToString());
                    return;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!RutaAdjuntosConfigurada())
            {
                return;
            }

            if (txtMatricula.Text != string.Empty && txtArchivo.Text != string.Empty)
            {
                Carpeta = ObtenerCarpetaAdjuntos(txtMatricula.Text);
                ArchivoUtil.AbrirArchivo(Path.Combine(Carpeta, txtArchivo.Text));
            }
            else if (txtMatricula.Text != string.Empty)
            {
                // NOTA: igual que en el original, este mensaje se dispara
                // cuando SÍ hay matrícula pero NO hay archivo adjunto; el
                // texto ("Seleccione un registro") no coincide con la
                // condición, y el mensaje de la siguiente rama tampoco
                // coincide con la suya. Parecen estar invertidos entre sí;
                // los dejé igual que el original, avísame si los corrijo.
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else if (txtArchivo.Text != string.Empty)
            {
                MessageBox.Show("Este registro no cuenta con un archivo adjunto");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!RutaAdjuntosConfigurada())
            {
                return;
            }

            if (txtMatricula.Text != string.Empty && txtArchivo.Text != string.Empty)
            {
                Carpeta = ObtenerCarpetaAdjuntos(txtMatricula.Text);
                if (Directory.Exists(Carpeta))
                {
                    ArchivoUtil.EliminarArchivo(Path.Combine(Carpeta, txtArchivo.Text));
                    txtArchivo.Clear();
                    c.ModificarExtension(txtFolioGeneral.Text, ext);
                }
            }
            else if (txtMatricula.Text != string.Empty)
            {
                // Ver misma nota que en button12_Click sobre estos mensajes.
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else if (txtArchivo.Text != string.Empty)
            {
                MessageBox.Show("Este registro no cuenta con un archivo adjunto");
            }
        }

        private void txtMontoPermitido_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMontoPermitido);
        }

        // NOTA: este método es casi idéntico a Utilerias.Moneda2(ref Guna2TextBox),
        // que ya existe en el proyecto (PV.Clases.Utilerias) - la única diferencia
        // real es el manejo de errores: Utilerias.Moneda2 vuelve a lanzar la
        // excepción (throw;) y este método la absorbe en silencio (catch vacío).
        // Se dejó tal cual para no cambiar ese comportamiento sin confirmarlo.
        private void Moneda(ref Guna2TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {


            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}