using PV.Clases.Anticipo;
using PV.Clases.Facturas;
using PV.Clases.Remision;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Aplica el saldo disponible de un Anticipo (de Propietario/Cliente) contra uno
    /// o varios documentos con saldo pendiente: Remisiones y/o Facturas.
    ///
    /// La grilla <c>dgvPagosPendientes</c> se llena combinando dos fuentes en un
    /// mismo formato de columnas (0=FolioDocumento, 1=Consecutivo, 2=ClaveDocumento,
    /// 3=Nombre, 4=Importe, 6=Descuento, 8=Abono, 9=Saldo):
    ///   - <see cref="DBAnticipo.CargarReciboAlumno2"/> para Remisiones.
    ///   - <see cref="DBFacturas.CargarFacturaPendienteAnticipo"/> para Facturas.
    /// Cada fila queda marcada en su <c>Tag</c> con "REMISION" o "FACTURA" — mismo
    /// patrón de discriminador por Tag que ya usa <c>DBFacturas.CargarFacturaCobro</c>
    /// en el flujo de RegistrarCobro — y es ese Tag el que decide, al aplicar el
    /// abono, a qué actualizador de saldo y con qué TipoDocumento llamar.
 
    /// </summary>
    public partial class AplicarAnticipoSaldo : Form
    {
        /// <summary>Acceso a datos de Anticipos (Anticipo / AnticipoCobros / Anticipo_General).</summary>
        DBAnticipo c = new DBAnticipo();

        /// <summary>Acceso a datos de Remisiones.</summary>
        DBRemiision r = new DBRemiision();

        /// <summary>Acceso a datos de Facturas.</summary>
        DBFacturas f = new DBFacturas();

        /// <summary>Folio del Anticipo origen del cual se está descontando el saldo.</summary>
        string Anticipo = string.Empty;

        /// <summary>
        /// Concepto (ConceptoCobroPago.IdConcepto) vigente en el Anticipo origen,
        /// recibido desde <see cref="AplicarAnticipo"/> para guardarse como
        /// trazabilidad en cada renglón de AnticipoCobros.
        /// </summary>
        /// <remarks>
        /// NOTA DE NOMBRE: no se llama "Concepto" a secas porque el Designer de este
        /// formulario ya genera un campo con ese nombre para la columna del grid
        /// (DataGridViewTextBoxColumn "Concepto"), y al ser una clase parcial ambos
        /// coexistirían provocando el error del compilador CS0229 ("Ambigüedad entre
        /// 'AplicarAnticipoSaldo.Concepto' y 'AplicarAnticipoSaldo.Concepto'").
        /// </remarks>
        string ConceptoAnticipo = string.Empty;

        /// <summary>
        /// Inicializa el formulario con los datos del Anticipo a aplicar.
        /// </summary>
        /// <param name="importe">Saldo disponible del Anticipo (tope máximo a aplicar).</param>
        /// <param name="Matricula">Clave del Propietario/Cliente dueño del Anticipo.</param>
        /// <param name="Alumno">Nombre del Propietario/Cliente, sólo para mostrar.</param>
        /// <param name="anticipo">Folio del Anticipo origen (Anticipo.Folio).</param>
        /// <param name="Fecha">Fecha de la aplicación (se fija y no es editable).</param>
        /// <param name="concepto">
        /// Concepto (ConceptoCobroPago.IdConcepto) del Anticipo origen. Parámetro
        /// nuevo respecto a la versión original: se agregó al migrar a
        /// ConceptoCobroPago, para poder dejarlo como trazabilidad en cada
        /// AnticipoCobros generado (ver <see cref="DBAnticipo.InsertarCobro"/>).
        /// </param>
        public AplicarAnticipoSaldo(string importe, string Matricula, string Alumno, string anticipo, string Fecha, string concepto)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            txtImporteTotal.Text = importe;
            Anticipo = anticipo;
            ConceptoAnticipo = concepto;
            dtpFecha.Value = Convert.ToDateTime(Fecha);
            dtpFecha.Enabled = false;
        }

        /// <summary>
        /// Carga en la grilla, combinadas, las Remisiones y Facturas con saldo
        /// pendiente del cliente. Se limpia el grid una sola vez aquí; ambos loaders
        /// se llaman con <c>limpiarPrimero: false</c> para no pisarse entre sí.
        /// </summary>
        private void AplicarAnticipoSaldo_Load(object sender, EventArgs e)
        {
            dgvPagosPendientes.Rows.Clear();
            c.CargarReciboAlumno2(dgvPagosPendientes, txtMatricula.Text, limpiarPrimero: false);
            f.CargarFacturaPendienteAnticipo(dgvPagosPendientes, txtMatricula.Text, limpiarPrimero: false);
        }

        /// <summary>
        /// Recalcula el Saldo de la fila editada (Importe - Abono - Descuento) y el
        /// total pagado acumulado, y da formato de moneda a Abono/Descuento.
        /// </summary>
        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;
            decimal Descuento = 0.00M;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value.ToString());
            }

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value != null)
            {
                Descuento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = 0;
            }

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = 0;
            }

            // NUEVO: si el abono capturado excede el saldo disponible del documento
            // (Importe - Descuento), se rechaza, se avisa y se regresa el Abono a 0.
            if (Abono > (Importe - Descuento))
            {
                MessageBox.Show("El abono no puede ser mayor al saldo del documento");
                Abono = 0.00M;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = 0;
            }

            if (Abono > 0)
            {
                Saldo = Importe - Abono - Descuento;
            }
            else
            {
                Saldo = Importe - Descuento;
            }

            string saldo = Saldo.ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = saldo.ToString();

            decimal TotalImporte = 0.00M;
            decimal TotalAbono = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                TotalAbono = TotalAbono + Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                txtTotalPagado.Text = TotalAbono.ToString("N", formato);
            }

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = abono.ToString("N", formato);
            decimal desc = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = desc.ToString("N", formato);
        }

        /// <summary>
        /// Habilita para edición la celda de Abono o Descuento de la fila, según el
        /// botón ("MasAbono"/"MasDescuento") en el que se haya dado clic.
        /// </summary>
        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Selected = false;
                dgvPagosPendientes.BeginEdit(false);
            }

            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasDescuento")
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Selected = false;
                dgvPagosPendientes.BeginEdit(false);
            }
        }

        /// <summary>Cierra el formulario sin aplicar nada.</summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Confirma la aplicación del Anticipo: valida montos, y por cada fila con
        /// Abono &gt; 0 actualiza el saldo del documento correspondiente (Remisión o
        /// Factura, según su Tag) y registra el movimiento en AnticipoCobros.
        /// Finalmente descuenta el saldo aplicado del Anticipo origen.
        /// </summary>       
        /// </remarks>
        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtImporteTotal.Text) < Convert.ToDecimal(txtTotalPagado.Text))
            {
                MessageBox.Show("No es posible aplicar un importe mayor al del anticipo");
            }
            else
            {
                if (MessageBox.Show("¿Finalizar aplicacion de Anticipo?", "Aplicar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("La aplicacion de un anticipo una vez realizado no puede cancelarse", "Aplicar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) < 0)
                            {
                                MessageBox.Show("No es posible realizar un abono menor a 0");
                                return;
                            }
                            else if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                            {
                                MessageBox.Show("El Abono no puede ser mayor al Importe");
                                return;
                            }
                        }

                        c.InsertarCobroGeneral(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                        int? idConceptoCobroPago = int.TryParse(ConceptoAnticipo, out int conceptoParseado) ? (int?)conceptoParseado : null;

                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) > 0)
                            {
                                string tipoDocumento = (row.Tag as string) ?? "REMISION";
                                string folioDocumento = row.Cells["FolioDocumento"].Value.ToString();
                                decimal abono = Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                                decimal descuento = Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                decimal saldoRestante = Convert.ToDecimal(row.Cells["Saldo"].Value.ToString());

                                if (tipoDocumento == "FACTURA")
                                {
                                    f.ActualizarFacturaAbono(folioDocumento, descuento, abono);
                                }
                                else
                                {
                                    r.ActualizarRemision(folioDocumento, 0.00m, descuento, abono);
                                }

                                c.InsertarCobro(folioDocumento, tipoDocumento, txtMatricula.Text, dtpFecha.Text,
                                    abono, txtFolioGeneral.Text, Anticipo, saldoRestante, descuento, idConceptoCobroPago);

                                if (descuento > 0)
                                {
                                    //ReciboNotaCredito reciboNotaCredito = new ReciboNotaCredito(folioDocumento, txtMatricula.Text, "1");
                                    //reciboNotaCredito.ShowDialog();
                                }
                            }
                        }

                        decimal SaldoAnticipo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);
                        c.ActualizarAnticipo(Anticipo, SaldoAnticipo);
                        AplicarAnticipo.nombre = string.Empty;
                        AplicarAnticipo.matricula = string.Empty;
                        button5.Enabled = false;
                        MessageBox.Show("Anticipo Aplicado");
                        ReporteReciboAnticipoAplicado reporteReciboAnticipoAplicado = new ReporteReciboAnticipoAplicado(Anticipo, txtFolioGeneral.Text);
                        reporteReciboAnticipoAplicado.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        /// <summary>Recalcula el nuevo saldo del Anticipo (Importe - Total pagado) y habilita/deshabilita "Confirmar".</summary>
        private void txtTotalPagado_TextChanged(object sender, EventArgs e)
        {
            decimal NuevoSaldo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);
            txtNuevoSaldo.Text = NuevoSaldo.ToString();
            if (txtTotalPagado.Text != "0.00" || txtTotalPagado.Text != "0")
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void txtNuevoSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtNuevoSaldo);
        }

        /// <summary>Da formato de moneda "en vivo" mientras el usuario captura un TextBox de importe.</summary>
        private void Moneda(ref TextBox txt)
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

                throw;
            }
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }
    }
}