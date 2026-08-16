using Condominios;
using Condominios.Clases.RegistrarIngresos;
using ControlAcademico;
using PuntoVentas.Clases.Login;
using PV;
using PV.Clases.Remision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    public partial class registroIngresos : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        DBRemiision r = new DBRemiision();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;
        public static string propiedad = string.Empty;
        public static int DescuentoNota = 0;
        public static decimal DescuentoPago = 0;
        string Tipo = string.Empty;
        string Monto = string.Empty;

        public registroIngresos(string tipo, string monto)
        {
            InitializeComponent();
            Tipo = tipo;
            Monto = monto;
            matricula = string.Empty;
            nombre = string.Empty;
        }

        private static NumberFormatInfo ObtenerFormatoMoneda()
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";
            return formato;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            txtAlumno.Clear();
            this.Close();
        }

        private void CargarPagos()
        {
            if (Tipo == "Remision")
            {
                r.CargarRemisionCobro(dgvPagosPendientes, txtMatricula.Text);
            }
        }

        private void registroIngresos_Load(object sender, EventArgs e)
        {
            txtMatricula.Text = matricula;
            txtAlumno.Text = nombre;
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtpFecha.MaxDate = DateTime.Now;
            txtCaja.Text = "1";
            btnBuscar.Enabled = Tipo == "M" ? false : true;

            if (txtFecha.Text == "No")
            {
                dtpFecha.Enabled = false;
            }
            else
            {
                dtpFecha.Enabled = true;
            }

            // Mensaje de recargos (se mantiene por si se usa, pero ya no afecta la lógica)
            try
            {
                DateTime hoy = DateTime.Now;
                DateTime fecha = Convert.ToDateTime(txtFechaRecargo.Text);
                if (hoy.Year == fecha.Year && fecha.Month < hoy.Month && string.IsNullOrEmpty(Tipo))
                {
                    MessageBox.Show("Debe generar recargos del mes en el menu Datos Condominio antes del dia ultimo del mes actual, de lo contrario no se acumulara los recargos correspondientes");
                }
                else if ((hoy.Year > fecha.Year) && string.IsNullOrEmpty(Tipo))
                {
                    MessageBox.Show("Debe generar recargos del mes en el menu Datos Condominio antes del dia ultimo del mes actual, de lo contrario no se acumulara los recargos correspondientes");
                }
            }
            catch (Exception) { }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            propiedad = string.Empty;
            if (Tipo == "Remision")
            {
                BuscarCliente buscar = new BuscarCliente();
                buscar.ShowDialog();

                if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
                {
                    txtMatricula.Text = BuscarCliente.Cliente;
                    txtAlumno.Text = BuscarCliente.NombreCliente;
                }
            }
            else if (Tipo == "Recibo")
            {
                BuscarListaAlumnos2 buscar = new BuscarListaAlumnos2();
                buscar.ShowDialog();
            }
        }

        private void registroIngresos_Activated(object sender, EventArgs e)
        {
            if (txtAlumno.Text == string.Empty)
            {
                btnConfirmar.Enabled = false;
                btnLimpiar.Enabled = false;
                dtpFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else
            {
                btnConfirmar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            DateTime FechaHoy = DateTime.Now;
            string Hoy = FechaHoy.ToString("yyyy/MM/dd");
            btnConfirmar.Enabled = false;
            btnLimpiar.Enabled = false;

            if (txtMatricula.Text != string.Empty)
            {
                CargarPagos();
                btnConfirmar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Contar filas seleccionadas
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
                MessageBox.Show("Seleccione al menos un recibo para continuar");
                return;
            }

            if (MessageBox.Show("Si continua los saldos del documento serán actualizados", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DateTime FechaHoy = DateTime.Now;
                string Hoy = FechaHoy.ToString("yyyy/MM/dd");

                // Ya no se validan recargos
                // Actualizar recibos y recargos ya no se usa
                // ActualizarRecibos(Hoy);
                // InsertarRecargos();

                ArrayList ListaConcept = ObtenerListaConceptosSeleccionados();

                RegistrarCobro cobro = new RegistrarCobro(ListaConcept, txtMatricula.Text, txtAlumno.Text, dtpFecha.Text, Tipo, Monto)
                {
                    // DescuentoPago se asigna desde el cálculo de totales
                };
                // DescuentoPago se puede obtener de la suma de descuentos seleccionados
                // Lo dejamos como estaba originalmente, pero podemos calcularlo:
                DescuentoPago = ObtenerTotalDescuentosSeleccionados();
                RegistrarCobro.DescuentoPago = DescuentoPago;
                cobro.ShowDialog();

                LimpiarFormulario();
            }
        }

        // Método auxiliar para calcular el total de descuentos de las filas seleccionadas
        private decimal ObtenerTotalDescuentosSeleccionados()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (EsFilaSeleccionada(row))
                {
                    if (row.Cells["Descuento"].Value != null)
                        total += Convert.ToDecimal(row.Cells["Descuento"].Value);
                }
            }
            return total;
        }

        private void LimpiarFormulario()
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            CargarPagos();
            dtpFecha.ResetText();
        }

        // Ya no se usan estos métodos, los dejamos vacíos o comentados
        // private bool ValidarRecargosYDescuentos() { return true; }
        // private void ActualizarRecibos(string hoy) { }
        // private void InsertarRecargos() { }

        private ArrayList ObtenerListaConceptosSeleccionados()
        {
            ArrayList listaConcept = new ArrayList();
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (EsFilaSeleccionada(row))
                {
                    listaConcept.Add(row.Cells["FolioDocumento"].Value.ToString());
                }
            }
            return listaConcept;
        }

        private bool EsFilaSeleccionada(DataGridViewRow row)
        {
            return row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true;
        }

        private void dgvPagosPendientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
            txtSubtotal.Text = "0.00";
            txtRecargos.Text = "0.00";   // Se mantiene pero no se usa
            txtDescuentos.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalRecibo.Text = "0.00";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            btnBuscar.BackColor = Color.Red;
            btnConfirmar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            GenerarRecibo.Matricula = string.Empty;
            RegistrarAnticipo.matricula = string.Empty;
            RegistrarAnticipo.nombre = string.Empty;
            AplicarAnticipo.matricula = string.Empty;
            AplicarAnticipo.nombre = string.Empty;
            this.Close();
        }

        private void btnPagosRegistrados_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                PagosRecibos pagosRecibos = new PagosRecibos(txtMatricula.Text, txtAlumno.Text);
                pagosRecibos.ShowDialog();
                CargarPagos();
            }
            else
            {
                MessageBox.Show("Seleccione el Cliente");
            }
        }

        // ====== MÉTODOS RECALCULO (adaptados a las columnas actuales) ======

        /// <summary>
        /// Recalcula el Saldo de una fila: Importe - Descuento.
        /// (Antes incluía recargos, ahora se simplifica)
        /// </summary>
        private void RecalcularSaldoFila(DataGridViewRow row)
        {
            NumberFormatInfo formato = ObtenerFormatoMoneda();

            decimal importe = 0M;
            decimal descuento = 0M;

            if (row.Cells["Importe"].Value != null)
                importe = Convert.ToDecimal(row.Cells["Importe"].Value);

            if (row.Cells["Descuento"].Value != null)
                descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);

            decimal saldo = importe - descuento;
            row.Cells["Saldo"].Value = saldo.ToString("N", formato);
        }

        /// <summary>
        /// Recalcula los totales (Subtotal, Descuentos, Total) basado en las filas seleccionadas.
        /// </summary>
        private void RecalcularTotales()
        {
            NumberFormatInfo formato = ObtenerFormatoMoneda();

            decimal subtotal = 0M;
            decimal totalDescuentos = 0M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (!EsFilaSeleccionada(row))
                    continue;

                if (row.Cells["Importe"].Value != null)
                    subtotal += Convert.ToDecimal(row.Cells["Importe"].Value);

                if (row.Cells["Descuento"].Value != null)
                    totalDescuentos += Convert.ToDecimal(row.Cells["Descuento"].Value);
            }

            txtSubtotal.Text = subtotal.ToString("N", formato);
            txtDescuentos.Text = totalDescuentos.ToString("N", formato);
            txtTotal.Text = (subtotal - totalDescuentos).ToString("N", formato);
        }

        // ====== EVENTOS DEL DataGridView ======

        private void dgvPagosPendientes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == null)
                return;

            DataGridViewRow filaActual = dgvPagosPendientes.Rows[e.RowIndex];
            string columna = dgvPagosPendientes.Columns[e.ColumnIndex].Name;

            // Manejo del check de selección
            if (columna == "Seleccionar")
            {
                bool seleccionado = (bool)filaActual.Cells["Seleccionar"].Value;
                if (seleccionado)
                {
                    // Al seleccionar, permitir editar descuento
                    filaActual.Cells["Descuento"].ReadOnly = false;
                    RecalcularSaldoFila(filaActual);
                    RecalcularTotales();
                }
                else
                {
                    // Al deseleccionar, limpiar descuento y saldo
                    filaActual.Cells["Descuento"].Value = "0.00";
                    filaActual.Cells["Descuento"].ReadOnly = true;
                    filaActual.Cells["Saldo"].Value = "0.00";

                    // Recalcular totales con las filas aún seleccionadas
                    RecalcularTotales();
                }
                return;
            }

            // Botón "MasDescuentos" (agregar descuento)
            if (columna == "MasDescuentos")
            {
                if (DBLogin.TipoUsuario != "Administrador")
                {
                    MessageBox.Show("No tiene permisos de administrador");
                    return;
                }

                // Se asume que siempre se permite descuento (se eliminó la variable txtDescuentosSiNo)
                filaActual.Cells["Descuento"].ReadOnly = false;
                filaActual.Cells["Descuento"].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
                return;
            }

            // Botón "MenosDescuentos" (quitar descuento)
            if (columna == "MenosDescuentos")
            {
                if (DBLogin.TipoUsuario != "Administrador")
                {
                    MessageBox.Show("No tiene permisos de administrador");
                    return;
                }

                filaActual.Cells["Descuento"].Value = "0.00";
                if (EsFilaSeleccionada(filaActual))
                {
                    RecalcularSaldoFila(filaActual);
                    RecalcularTotales();
                }
                return;
            }
        }

        private void dgvPagosPendientes_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvPagosPendientes_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            // Solo interesa si se editó la columna "Descuento" y la fila está seleccionada
            if (e.RowIndex >= 0 && dgvPagosPendientes.Columns[e.ColumnIndex].Name == "Descuento")
            {
                DataGridViewRow row = dgvPagosPendientes.Rows[e.RowIndex];
                if (EsFilaSeleccionada(row))
                {
                    // Validar que el descuento no sea mayor que el importe (opcional)
                    decimal importe = Convert.ToDecimal(row.Cells["Importe"].Value);
                    decimal descuento = 0M;
                    if (row.Cells["Descuento"].Value != null)
                        descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);

                    if (descuento < 0)
                    {
                        MessageBox.Show("El descuento no puede ser negativo");
                        row.Cells["Descuento"].Value = "0.00";
                        descuento = 0;
                    }
                    else if (descuento > importe)
                    {
                        MessageBox.Show("El descuento no puede ser mayor que el importe");
                        row.Cells["Descuento"].Value = importe.ToString("N", ObtenerFormatoMoneda());
                        descuento = importe;
                    }

                    RecalcularSaldoFila(row);
                    RecalcularTotales();
                }
            }
        }

        // Los eventos que no se usan los dejamos vacíos
        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvPagosPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtSubtotal_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) { }
        private void guna2CircleButton1_Click(object sender, EventArgs e) { this.Close(); }
    }
}