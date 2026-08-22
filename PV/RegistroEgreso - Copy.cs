using Condominios;
using PuntoVentas;
using PV.Clases;
using PV.Clases.Egresos;
using PV.Clases.Proveedores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PV
{
  
    public partial class ConsultarEgreso : Form
    {
        private readonly DBEgresos dbEgresos = new DBEgresos();
        private readonly DBProveedores dbProveedores = new DBProveedores();

        // Proveedor(es) elegidos en cmbpropietario1 / cmbpropietario2.
        private string idProveedor1 = string.Empty;
        private string nombreProveedor1 = string.Empty;
        private string idProveedor2 = string.Empty;
        private string nombreProveedor2 = string.Empty;

        // Evita que CargarProveedores()/LimpiarProveedores() disparen
        // CargarPagosPendientes() varias veces al tocar los combos por código.
        private bool suprimiendoEventos = false;

  
        public static string matricula = string.Empty;
        public static string nombre = string.Empty;

        private static readonly NumberFormatInfo FormatoMoneda = new CultureInfo("en-US").NumberFormat;

        public ConsultarEgreso()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(btnReportePreeliminar, "Imprimir Seleccionados");
        }

        #region Carga inicial

        private void RegistroEgreso_Load(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;

            txtCaja.Text = "1";
            dtpFecha1.Value = DateTime.Now;
            dtpFecha2.Value = DateTime.Now;
            dtpVencimiento.Value = DateTime.Now;

            ConfigurarColumnasGrid();
            CargarProveedores();
            LimpiarProveedores();
            dtpVencimiento.
                Value = DateTime.Now;   
        }

        // Llena los combos de proveedor con el catálogo activo, usando el
        // mismo helper (ComboUtil) que el resto del sistema.
        private void CargarProveedores()
        {
            suprimiendoEventos = true;
            try
            {
                DataTable proveedores = dbProveedores.ConsultarProveedores();
                ComboUtil.LlenarComboBox(cmbpropietario1, proveedores, "RazonSocial", "IdProveedor");
                ComboUtil.LlenarComboBox(cmbpropietario2, proveedores, "RazonSocial", "IdProveedor");
            }
            finally
            {
                suprimiendoEventos = false;
            }
        }

        // Si las columnas ya vienen definidas desde el diseñador, no se tocan.
        // Esto solo entra en acción si el DataGridView se dejó sin columnas
        // configuradas en el .Designer.cs.
        private void ConfigurarColumnasGrid()
        {
            if (dgvPagosPendientes.Columns.Count > 0)
                return;

            dgvPagosPendientes.AutoGenerateColumns = false;

            dgvPagosPendientes.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Seleccionar", HeaderText = "Sel", Width = 40 });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Documento", HeaderText = "Documento" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Concepto", HeaderText = "Concepto" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Proveedor", HeaderText = "Proveedor" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vence", HeaderText = "Vence" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Importe", HeaderText = "Importe" });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "SaldoActual", HeaderText = "Saldo Actual" });

            // Columnas ocultas de apoyo (no se muestran, pero se necesitan para
            // operar sin tener que re-parsear texto formateado).
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", Visible = false });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Folio", Visible = false });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Doc", Visible = false });
            dgvPagosPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProveedor", Visible = false });
        }

        #endregion

        #region Selección de proveedores (combo con SelectedValue + BuscarListaProveedores)

        // Único lugar donde se lee el proveedor 1. Se dispara al elegir en el
        // combo directamente o al preseleccionarlo desde la lupa (ver más abajo).
        private void cmbpropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suprimiendoEventos)
                return;

            idProveedor1 = ComboUtil.ObtenerSelectedValue(cmbpropietario1);
            nombreProveedor1 = idProveedor1 != null ? cmbpropietario1.Text : string.Empty;

            ActualizarMatriculaAlumno();
            CargarPagosPendientes();
        }

        // Único lugar donde se lee el proveedor 2.
        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suprimiendoEventos)
                return;

            idProveedor2 = ComboUtil.ObtenerSelectedValue(cmbpropietario2);
            nombreProveedor2 = idProveedor2 != null ? cmbpropietario2.Text : string.Empty;

            ActualizarMatriculaAlumno();
            CargarPagosPendientes();
        }

        // txtMatricula/txtAlumno los usan otras pantallas (PagosEgresos, etc.);
        // solo se llenan cuando hay un único proveedor filtrando.
        private void ActualizarMatriculaAlumno()
        {
            bool unSoloProveedor = !string.IsNullOrEmpty(idProveedor1) && string.IsNullOrEmpty(idProveedor2);

            txtMatricula.Text = unSoloProveedor ? idProveedor1 : string.Empty;
            txtAlumno.Text = unSoloProveedor ? nombreProveedor1 : string.Empty;
        }

        private void btnBuscar_Click(object sender, EventArgs e) => BuscarProveedor(esProveedor1: true);

        private void guna2Button1_Click(object sender, EventArgs e) => BuscarProveedor(esProveedor1: true);

        private void guna2Button4_Click(object sender, EventArgs e) => BuscarProveedor(esProveedor1: false);

        // La lupa ya NO escribe texto ni guarda el resultado directamente: solo
        // preselecciona el proveedor en el combo correspondiente. Es
        // SelectedIndexChanged quien realmente actualiza idProveedorX y
        // recarga la grid, para que exista un único camino de verdad.
        private void BuscarProveedor(bool esProveedor1)
        {
            using (var buscar = new BuscarListaProveedores())
            {
                if (buscar.ShowDialog() != DialogResult.OK)
                    return;

                ComboBox combo = esProveedor1 ? cmbpropietario1 : cmbpropietario2;
                combo.SelectedValue = buscar.Matricula;

                if (ComboUtil.ObtenerSelectedValue(combo) != buscar.Matricula)
                {
                    MessageBox.Show("El proveedor seleccionado no está activo o no existe en el catálogo.");
                }
            }
        }

        #endregion

        #region Carga y filtrado de pagos pendientes

        private void CargarPagosPendientes()
        {
            dgvPagosPendientes.Rows.Clear();

            DateTime? vencimiento =  dtpVencimiento.Value;
            DateTime? fecha1 = chFecha.Checked ? dtpFecha1.Value : (DateTime?)null;
            DateTime? fecha2 = chFecha.Checked ? dtpFecha2.Value : (DateTime?)null;

            var proveedores = new List<(string Id, string Nombre)>();
            if (!string.IsNullOrEmpty(idProveedor1))
                proveedores.Add((idProveedor1, nombreProveedor1));
            if (!string.IsNullOrEmpty(idProveedor2) && idProveedor2 != idProveedor1)
                proveedores.Add((idProveedor2, nombreProveedor2));

            if (proveedores.Count == 0)
            {
                // Sin proveedor: el filtro se apoya en lo que sí esté puesto
                // (vencimiento, rango de fechas). Si tampoco hay nada de eso,
                // trae todo.
                DataTable dtTodos = dbEgresos.ObtenerEgresos(null, vencimiento, chFecha.Checked, fecha1, fecha2);

                foreach (DataRow row in dtTodos.Rows)
                {
                    AgregarFilaGrid(row, row["NombreProveedor"].ToString());
                }
            }
            else
            {
                foreach (var proveedor in proveedores)
                {
                    DataTable dt = dbEgresos.ObtenerEgresos(proveedor.Id, vencimiento, chFecha.Checked, fecha1, fecha2);

                    foreach (DataRow row in dt.Rows)
                    {
                        AgregarFilaGrid(row, proveedor.Nombre);
                    }
                }
            }

            if (dgvPagosPendientes.Columns.Contains("Seleccionar"))
                dgvPagosPendientes.Columns["Seleccionar"].Visible = true;

            ActualizarTotales();
        }

        private bool PasaFiltros(DataRow row)
        {
            if (chFecha.Checked)
            {
                DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                if (fecha.Date < dtpFecha1.Value.Date || fecha.Date > dtpFecha2.Value.Date)
                    return false;
            }

            if (row["FechaVence"] != DBNull.Value)
            {
                DateTime vence = Convert.ToDateTime(row["FechaVence"]);
                if (vence.Date > dtpVencimiento.Value.Date)
                    return false;
            }

            return true;
        }

        private void AgregarFilaGrid(DataRow row, string nombreProveedor)
        {
            int i = dgvPagosPendientes.Rows.Add();
            DataGridViewRow fila = dgvPagosPendientes.Rows[i];

            fila.Cells["Seleccionar"].Value = false;
            fila.Cells["Documento"].Value = row["ClaveDocumento"].ToString() + " - "+row["Folio"].ToString();
            fila.Cells["Concepto"].Value = row["Nombre"].ToString(); // Nombre del tipo de documento (tabla Documento)
            fila.Cells["Proveedor"].Value = nombreProveedor;
            fila.Cells["Fecha"].Value = Convert.ToDateTime(row["Fecha"]).ToString("yyyy/MM/dd");
            fila.Cells["Vence"].Value = row["FechaVence"] != DBNull.Value
                ? Convert.ToDateTime(row["FechaVence"]).ToString("yyyy/MM/dd")
                : string.Empty;
            fila.Cells["Importe"].Value = Convert.ToDecimal(row["Total"]).ToString("N", FormatoMoneda);
            fila.Cells["SaldoActual"].Value = Convert.ToDecimal(row["Saldo"]).ToString("N", FormatoMoneda);

            fila.Cells["Tipo"].Value = row["Tipo"].ToString();
            fila.Cells["Folio"].Value = row["Folio"].ToString();
            // "Doc" es el nombre real de esta columna en tu DataGridView (viene
            // así desde el Designer original, ver CargarReciboProveedorFiltro:
            // Cells["Doc"].Value = item["ClaveDocumento"]). RegistrarCobroEgreso
            // usa este valor (junto con Folio y Tipo) para volver a filtrar los
            // renglones seleccionados aquí. Es distinta de "Documento" (esa solo
            // muestra el Folio en pantalla).
            fila.Cells["Doc"].Value = row["ClaveDocumento"].ToString();
            fila.Cells["IdProveedor"].Value = row["ClaveProveedor"].ToString();
        }

        #endregion

        #region Totales

        private void dgvPagosPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dgvPagosPendientes.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            // El valor del checkbox no queda confirmado hasta CommitEdit; se
            // fuerza aquí para que el total se recalcule al primer clic.
            dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            ActualizarTotales();
        }

        private void dgvPagosPendientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ActualizarTotales()
        {
            decimal subtotal = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value is bool seleccionado && seleccionado)
                {
                    subtotal += Convert.ToDecimal(row.Cells["SaldoActual"].Value, FormatoMoneda);
                }
            }

            // Recargos y Descuentos ya no se capturan por documento (ver notas
            // en DBEgresos), así que el total queda igual al subtotal. Se
            // dejan los TextBox en 0.00 por si el formulario los sigue mostrando.
            txtSubtotal.Text = subtotal.ToString("N", FormatoMoneda);
            txtRecargos.Text = "0.00";
            txtDescuentos.Text = "0.00";
            txtTotal.Text = subtotal.ToString("N", FormatoMoneda);
        }

        #endregion

        #region Registrar cobro

        private void button5_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> filasSeleccionadas = dgvPagosPendientes.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["Seleccionar"].Value is bool b && b)
                .ToList();

            if (filasSeleccionadas.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un recibo para continuar");
                return;
            }

            string idProveedorSeleccionado = filasSeleccionadas[0].Cells["IdProveedor"].Value.ToString();
            bool mismoProveedor = filasSeleccionadas.All(r => r.Cells["IdProveedor"].Value.ToString() == idProveedorSeleccionado);

            if (!mismoProveedor)
            {
                MessageBox.Show("Los recibos de pago no son del mismo proveedor");
                return;
            }

            if (MessageBox.Show("Si continua, los saldos por pagar serán actualizados", "Registrar Cobro",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var folios = new ArrayList();
            var documentos = new ArrayList();
            var tipos = new ArrayList();

            foreach (DataGridViewRow fila in filasSeleccionadas)
            {
                string tipo = fila.Cells["Tipo"].Value.ToString();
                string folio = fila.Cells["Folio"].Value.ToString();
                decimal saldo = Convert.ToDecimal(fila.Cells["SaldoActual"].Value, FormatoMoneda);

                // ActualizarEgreso2(Tipo, Folio, Recargos, Descuento, Saldo) ya no
                // existe; ahora solo se actualiza el Saldo del documento.
                dbEgresos.ActualizarSaldoEgreso(tipo, folio, saldo);

                folios.Add(folio);
                // "Doc" (no "Documento"): así se llama la columna oculta real en
                // el grid, ver nota en AgregarFilaGrid.
                documentos.Add(fila.Cells["Doc"].Value.ToString());
                tipos.Add(tipo);
            }

            string nombreProveedorSeleccionado = filasSeleccionadas[0].Cells["Proveedor"].Value.ToString();

            using (var cobro = new RegistrarCobroEgreso(
                       folios, documentos, tipos,
                       idProveedorSeleccionado, nombreProveedorSeleccionado,
                       DateTime.Now.ToString("yyyy/MM/dd")))
            {
                cobro.ShowDialog();
            }

            LimpiarProveedores();
            CargarPagosPendientes();
        }

        #endregion

        #region Limpiar / filtros de fecha

        private void button6_Click(object sender, EventArgs e) => LimpiarYRecargar();

        private void guna2Button2_Click(object sender, EventArgs e) => LimpiarYRecargar();

        private void LimpiarYRecargar()
        {
            LimpiarProveedores();
            CargarPagosPendientes();
        }

        private void LimpiarProveedores()
        {
            idProveedor1 = string.Empty;
            nombreProveedor1 = string.Empty;
            idProveedor2 = string.Empty;
            nombreProveedor2 = string.Empty;

            suprimiendoEventos = true;
            try
            {
                cmbpropietario1.SelectedIndex = -1;
                cmbpropietario2.SelectedIndex = -1;
            }
            finally
            {
                suprimiendoEventos = false;
            }

            txtMatricula.Clear();
            txtAlumno.Clear();

            txtSubtotal.Text = "0.00";
            txtRecargos.Text = "0.00";
            txtDescuentos.Text = "0.00";
            txtTotal.Text = "0.00";

            chFecha.Checked = false;
            dtpFecha1.Value = DateTime.Now;
            dtpFecha2.Value = DateTime.Now;
            dtpVencimiento.Value = DateTime.Now;

            dgvPagosPendientes.Rows.Clear();
        }

        private void chFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFecha1.Enabled = chFecha.Checked;
            dtpFecha2.Enabled = chFecha.Checked;
            CargarPagosPendientes();
        }

        private void dtpFecha1_ValueChanged(object sender, EventArgs e)
        {
            ValidarFecha();
            CargarPagosPendientes();
        }

        private void dtpFecha2_ValueChanged(object sender, EventArgs e)
        {
            ValidarFecha();
            CargarPagosPendientes();
        }

        private void dtpVencimiento_ValueChanged(object sender, EventArgs e) => CargarPagosPendientes();

        private void ValidarFecha()
        {
            if (dtpFecha1.Value > dtpFecha2.Value)
                dtpFecha1.Value = dtpFecha2.Value;
            else if (dtpFecha2.Value < dtpFecha1.Value)
                dtpFecha2.Value = dtpFecha1.Value;
        }

        #endregion

        #region Otros botones del formulario

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            txtAlumno.Clear();



            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                MessageBox.Show("Seleccione el Proveedor");
                return;
            }

            using (var cobro = new PagosEgresos(txtMatricula.Text, txtAlumno.Text))
            {
                cobro.ShowDialog();
            }

            LimpiarYRecargar();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int proveedor1 = ObtenerIdProveedor(cmbpropietario1);
            int proveedor2 = ObtenerIdProveedor(cmbpropietario2);

            using (var r = new ReporteCuentasPorPagar(
                       dtpVencimiento.Text, dtpFecha1.Text, dtpFecha2.Text,
                       proveedor1, proveedor2, chFecha.Checked))
            {
                r.ShowDialog();
            }
        }
        private static int ObtenerIdProveedor(ComboBox combo)
        {
            object valor = combo.SelectedValue;

            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(valor);
        }

        // Handlers que se dejan vacíos a propósito para no tener que editar el
        // .Designer.cs (siguen enganchados a los mismos eventos que antes).
        private void RegistroEgreso_Activated(object sender, EventArgs e) { }

        private void txtMatricula_TextChanged(object sender, EventArgs e) { }

        private void dtpFecha_ValueChanged(object sender, EventArgs e) { }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e) { }

        #endregion

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
                $"{row.Cells["Tipo"].Value},{row.Cells["Folio"].Value}"));

            using (var reporte = new ReportePagoEgresoPreeliminar(documentos))
            {
                reporte.ShowDialog();
            }
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
    }
}