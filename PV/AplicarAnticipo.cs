using Condominios;
using ControlAcademico;
using PuntoVentas;
using PV.Clases.Anticipo;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Formulario para localizar los Anticipos con saldo disponible de un
    /// Propietario/Cliente y enviar el seleccionado a
    /// <see cref="AplicarAnticipoSaldo"/>, donde se decide contra qué documento
    /// (Remisión o Factura) se aplica ese saldo.
    ///
    /// Este formulario no requiere cambios estructurales por la migración a
    /// ConceptoCobroPago ni por el destino polimórfico Remisión/Factura: ambos
    /// cambios están encapsulados en <see cref="DBAnticipo"/>
    /// (<c>CargarReciboAlumno</c> y <c>CargarDocumentosPendientesAplicacion</c>
    /// respectivamente). El único ajuste aquí es reenviar el Concepto de la fila
    /// seleccionada, para que quede como trazabilidad en AnticipoCobros.
    /// </summary>
    public partial class AplicarAnticipo : Form
    {
        /// <summary>Acceso a datos del módulo de Anticipos.</summary>
        DBAnticipo c = new DBAnticipo();

        /// <summary>Matrícula/clave del cliente actualmente cargado en el formulario.</summary>
        public static string matricula = string.Empty;

        /// <summary>Nombre del cliente actualmente cargado en el formulario.</summary>
        public static string nombre = string.Empty;

        public AplicarAnticipo()
        {
            InitializeComponent();
        }

        /// <summary>Inicializa el formulario: limpia estado estático y fija la fecha por defecto (hoy).</summary>
        private void AplicarAnticipo_Load(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtpFecha.MaxDate = DateTime.Today;
            txtCaja.Text = "1";
        }

        /// <summary>Botón de cierre del formulario: limpia el estado estático compartido con formularios relacionados.</summary>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            GenerarRecibo.Matricula = string.Empty;
            RegistrarAnticipo.matricula = string.Empty;
            RegistrarAnticipo.nombre = string.Empty;
            registroIngresos.matricula = string.Empty;
            registroIngresos.nombre = string.Empty;

            txtAlumno.Clear();
            this.Close();
        }

        /// <summary>Abre el buscador de clientes y, si se selecciona uno, lo carga en los campos de matrícula/nombre.</summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCliente buscar = new BuscarCliente();
            buscar.ShowDialog();
            if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
            {
                txtMatricula.Text = BuscarCliente.Cliente;
                txtAlumno.Text = BuscarCliente.NombreCliente;
            }
        }

        private void AplicarAnticipo_Activated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Habilita/deshabilita los botones de acción según haya o no una matrícula
        /// capturada, y recarga la lista de Anticipos con saldo disponible.
        /// </summary>
        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricula.Text == string.Empty)
            {
                button5.Enabled = false;
                button6.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
                button6.Enabled = true;
            }
            if (txtMatricula.Text != string.Empty)
            {
                dgvPagosPendientes.Rows.Clear();
                c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
            }
        }

        /// <summary>
        /// Al marcar/desmarcar la casilla "Seleccionar" de una fila, recalcula el
        /// subtotal de los Anticipos seleccionados y refleja el saldo en la columna
        /// correspondiente. La regla de "solo uno seleccionado" se valida en
        /// <see cref="button5_Click"/>.
        /// </summary>
        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
            {
                decimal Subtotal = 0.00M;

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = row.Cells["Saldo"].Value.ToString();
                        Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Saldo"].Value.ToString());
                    }
                }
            }
            else
            {
                decimal Subtotal = 0.00M;

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = row.Cells["Saldo"].Value.ToString();
                    }
                }
            }
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Botón "Confirmar": valida que exactamente un Anticipo esté seleccionado y
        /// abre <see cref="AplicarAnticipoSaldo"/> para decidir el documento destino
        /// (Remisión o Factura) contra el que se aplicará el saldo.
        /// </summary>
        /// <remarks>
        /// CAMBIO: además de Importe, Matrícula, Nombre, Folio del Anticipo y Fecha,
        /// ahora también se reenvía el Concepto (ConceptoCobroPago.IdConcepto) de la
        /// fila seleccionada, para que <see cref="AplicarAnticipoSaldo"/> pueda
        /// guardarlo como trazabilidad al invocar
        /// <c>DBAnticipo.InsertarCobro(..., idConceptoCobroPago)</c>.
        /// Este cambio requiere agregar el parámetro "concepto" al constructor de
        /// AplicarAnticipoSaldo (no incluido en el material compartido; pendiente
        /// de adaptar en conjunto — ver aviso en el chat).
        /// </remarks>
        private void button5_Click(object sender, EventArgs e)
        {
            string Importe = string.Empty;
            string Anticipo = string.Empty;
            string Concepto = string.Empty;

            int contador = 0;
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                {
                    contador++;
                }
            }

            if (contador == 0)
            {
                MessageBox.Show("Seleccione al menos un Anticipo para continuar");
                return;
            }

            if (contador > 1)
            {
                MessageBox.Show("Seleccione solo un Anticipo");
                return;
            }

            DataGridViewCheckBoxCell oCell;

            foreach (DataGridViewRow row2 in dgvPagosPendientes.Rows)
            {
                oCell = row2.Cells["Seleccionar"] as DataGridViewCheckBoxCell;
                bool bChecked = (null != oCell && null != oCell.Value && true == (bool)oCell.Value);
                if (true == bChecked)
                {
                    Importe = row2.Cells["Saldo"].Value.ToString();
                    Anticipo = row2.Cells["FolioDocumento"].Value.ToString();
                    Concepto = row2.Cells["Concepto"].Value.ToString();
                }
            }

            AplicarAnticipoSaldo cobro = new AplicarAnticipoSaldo(Importe, txtMatricula.Text, txtAlumno.Text, Anticipo, dtpFecha.Text, Concepto);
            cobro.ShowDialog();

            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
        }

        /// <summary>Botón "Cancelar": limpia la selección actual y recarga la lista de Anticipos.</summary>
        private void button6_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
            button5.Enabled = false;
            button6.Enabled = false;
        }

        /// <summary>Limpia los campos de matrícula y nombre del cliente.</summary>
        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
        }

        /// <summary>Al mostrar el formulario, normaliza a mayúsculas todas las etiquetas del panel principal.</summary>
        private void AplicarAnticipo_Shown(object sender, EventArgs e)
        {
            foreach (Control c in guna2Panel1.Controls)
            {
                if (c is Label)
                {
                    Label l = (Label)c;
                    l.Text = l.Text.ToUpper();
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}