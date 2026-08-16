using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PuntoVentas;
using Condominios;
using PV.Clases;
using PV.Clases.Egresos;
using System.Linq;

namespace PV
{
    public partial class RegistroEgreso : Form
    {
        private readonly DBEgresos dbEgresos = new DBEgresos();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;

        public RegistroEgreso()
        {
            InitializeComponent();
        }

        private void RegistroEgreso_Load(object sender, EventArgs e)
        {
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCaja.Text = "1";
        }

        private void RegistroEgreso_Activated(object sender, EventArgs e)
        {
        }

        //____________________________________________________________________
        // Llena el grid de documentos pendientes de pago del proveedor
        // seleccionado. Solo trae Tipo, Folio, Documento, Concepto, Fecha,
        // Importe y SaldoActual: ya no existen columnas de Recargo/Descuento.
        private void LLenarEgresos()
        {
            DataTable dtOriginal = dbEgresos.ObtenerEgresos(txtMatricula.Text);

            dgvPagosPendientes.Rows.Clear();

            foreach (DataRow row in dtOriginal.Rows)
            {
                int idx = dgvPagosPendientes.Rows.Add();
                DataGridViewRow dgRow = dgvPagosPendientes.Rows[idx];

                dgRow.Cells["Tipo"].Value = row["Tipo"];
                dgRow.Cells["FolioDocumento"].Value = row["Folio"];
                dgRow.Cells["Documento"].Value = row["ClaveDocumento"];
                dgRow.Cells["Concepto"].Value = row["Nombre"];
                dgRow.Cells["Fecha"].Value = Convert.ToDateTime(row["Fecha"]).ToString("yyyy/MM/dd");
                dgRow.Cells["Importe"].Value = row["Total"];
                dgRow.Cells["SaldoActual"].Value = row["Saldo"];
            }

            ActualizarTotales();
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                LLenarEgresos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var buscador = new BuscarListaProveedores())
            {
                if (buscador.ShowDialog() == DialogResult.OK)
                {
                    txtMatricula.Text = buscador.Matricula;
                    txtAlumno.Text = buscador.Nombre;
                }
            }
        }

        //____________________________________________________________________
        // Fuerza a confirmar de inmediato el check de "Seleccionar" para que
        // el resto de los eventos (CellContentClick) trabajen con el valor
        // ya actualizado.
        private void dgvPagosPendientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //____________________________________________________________________
        // Antes recalculaba Saldo por fila (Importe + Recargo - Descuento) y
        // validaba que Recargo/Descuento no superaran el saldo. Como esas
        // columnas ya no existen en el grid, no queda nada que editar por
        // celda; se deja el evento (puede seguir estando referenciado desde
        // el diseñador) solo por si en algún momento se agrega otra columna
        // editable, y de paso mantiene los totales sincronizados.
        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarTotales();
        }

        //____________________________________________________________________
        // Antes manejaba, además del check de "Seleccionar", los clics en las
        // columnas de botón MasRecargo/MenosRecargo/MasDescuentos/MenosDescuentos,
        // que ya no existen. Ahora solo recalcula totales al (des)marcar un
        // renglón.
        private void dgvPagosPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvPagosPendientes.Columns[e.ColumnIndex].Name == "Seleccionar")
            {
                ActualizarTotales();
            }
        }

        //____________________________________________________________________
        // Suma el SaldoActual de los renglones marcados. Ya no hay Recargos
        // ni Descuentos por documento, así que Total = Subtotal.
        private void ActualizarTotales()
        {
            decimal subtotal = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value is bool seleccionado && seleccionado
                    && row.Cells["SaldoActual"].Value != null)
                {
                    subtotal += Convert.ToDecimal(row.Cells["SaldoActual"].Value);
                }
            }

            // txtRecargos/txtDescuentos se dejan en 0 por compatibilidad,
            // por si los controles siguen en el diseñador. Si ya no se
            // necesitan en la pantalla, se pueden quitar junto con estas dos
            // líneas.
            txtRecargos.Text = Utilerias.FormatearMiles("0");
            txtDescuentos.Text = Utilerias.FormatearMiles("0");
            txtSubtotal.Text = Utilerias.FormatearMiles(subtotal.ToString());
            txtTotal.Text = Utilerias.FormatearMiles(subtotal.ToString());
        }

        private List<DataGridViewRow> ObtenerFilasSeleccionadas()
        {
            var filas = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value is bool seleccionado && seleccionado)
                {
                    filas.Add(row);
                }
            }

            return filas;
        }

        //____________________________________________________________________
        // "Registrar Cobro" (pago). Antes, antes de abrir RegistrarCobroEgreso,
        // se llamaba a c.ActualizarEgreso2(...) por cada fila seleccionada
        // para "congelar" el saldo con el recargo/descuento capturado en el
        // grid. Como ya no se capturan recargos ni descuentos aquí, ese saldo
        // ya no cambia en este paso: simplemente se reúnen los documentos
        // seleccionados y se abre la pantalla de pago con ellos, igual que
        // antes.
        private void button5_Click(object sender, EventArgs e)
        {
            var seleccionados = ObtenerFilasSeleccionadas();

            if (seleccionados.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un recibo para continuar");
                return;
            }

            if (MessageBox.Show(
                    "¿Desea continuar con el registro del pago de los documentos seleccionados?",
                    "Registrar Pago",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var listaFolios = new ArrayList();
            var listaDocumentos = new ArrayList();
            var listaTipos = new ArrayList();

            foreach (DataGridViewRow row in seleccionados)
            {
                listaFolios.Add(row.Cells["FolioDocumento"].Value.ToString());
                listaDocumentos.Add(row.Cells["Documento"].Value.ToString());
                listaTipos.Add(row.Cells["Tipo"].Value.ToString());
            }

            using (var cobro = new RegistrarCobroEgreso(listaFolios, listaDocumentos, listaTipos, txtMatricula.Text, txtAlumno.Text))
            {
                cobro.ShowDialog();
            }

            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            LLenarEgresos();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            LLenarEgresos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                using (var cobro = new PagosEgresos(txtMatricula.Text, txtAlumno.Text))
                {
                    cobro.ShowDialog();
                }

                Limpiar();
                LLenarEgresos();
            }
            else
            {
                MessageBox.Show("Seleccione el Proveedor");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            txtAlumno.Clear();

            this.Close();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
            txtSubtotal.Text = "0.00";
            txtRecargos.Text = "0.00";
            txtDescuentos.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        private void btnReportePreeliminar_Click(object sender, EventArgs e)
        {
            var seleccionados = ObtenerFilasSeleccionadas();

            if (seleccionados.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un documento para generar el reporte");
                return;
            }

            // Ej: "P,12;G,45;NCG,7"
            string documentos = string.Join(";", seleccionados.Select(row =>
                $"{row.Cells["Tipo"].Value},{row.Cells["FolioDocumento"].Value}"));

            using (var reporte = new ReportePagoEgresoPreeliminar(documentos))
            {
                reporte.ShowDialog();
            }
        }
    }
}