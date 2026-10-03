using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PV.Clases.Graficas;
using PV.Clases.Proveedores;
using PV.dto;

namespace PV
{
    public partial class DashboardCompras : Form
    {
        private IDashboardCompras datos;
        private CancellationTokenSource cargaActual;

        private bool inicializando = true;
        private bool formularioInicializado = false;

        private readonly CultureInfo cultura =
            CultureInfo.GetCultureInfo("es-MX");

        public DashboardCompras()
        {
            InitializeComponent();
        }

        public DashboardCompras(IDashboardCompras datos)
            : this()
        {
            if (datos == null)
            {
                throw new ArgumentNullException("datos");
            }

            this.datos = datos;
        }

        private async void DashboardCompras_Shown(
            object sender,
            EventArgs e)
        {
            if (EstaEnModoDiseno())
            {
                return;
            }

            if (formularioInicializado)
            {
                return;
            }

            formularioInicializado = true;

            try
            {
                ConfigurarTablas();
                ConfigurarGraficas();

                if (datos == null)
                {
                    datos = CrearServicioDatos();
                }

                await InicializarAsync();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private IDashboardCompras CrearServicioDatos()
        {
            return new DBDashboardCompras();
        }

        private bool EstaEnModoDiseno()
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                return true;
            }

            if (DesignMode)
            {
                return true;
            }

            if (Site != null &&
                Site.DesignMode)
            {
                return true;
            }

            return false;
        }

        // =========================================================
        // TABLAS
        // =========================================================

        private void ConfigurarTablas()
        {
            ConfigurarTablaMeses();
            ConfigurarTablaProveedores();
        }

        private void ConfigurarTablaMeses()
        {
            dgvMeses.AutoGenerateColumns = false;
            dgvMeses.Columns.Clear();

            DataGridViewTextBoxColumn columnaMes =
                new DataGridViewTextBoxColumn();

            columnaMes.Name = "colNombreMes";
            columnaMes.HeaderText = "Mes";
            columnaMes.DataPropertyName = "NombreMes";
            columnaMes.ReadOnly = true;
            columnaMes.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaMes.FillWeight = 55F;

            DataGridViewTextBoxColumn columnaTotal =
                new DataGridViewTextBoxColumn();

            columnaTotal.Name = "colTotalCompras";
            columnaTotal.HeaderText = "Total Compras";
            columnaTotal.DataPropertyName = "TotalCompras";
            columnaTotal.ReadOnly = true;
            columnaTotal.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaTotal.FillWeight = 45F;

            columnaTotal.DefaultCellStyle.Format = "C2";
            columnaTotal.DefaultCellStyle.FormatProvider = cultura;

            columnaTotal.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotal.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvMeses.Columns.Add(columnaMes);
            dgvMeses.Columns.Add(columnaTotal);
        }

        private void ConfigurarTablaProveedores()
        {
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.Columns.Clear();

            DataGridViewTextBoxColumn columnaProveedor =
                new DataGridViewTextBoxColumn();

            columnaProveedor.Name = "colProveedor";
            columnaProveedor.HeaderText = "Proveedor";
            columnaProveedor.DataPropertyName = "Proveedor";
            columnaProveedor.ReadOnly = true;
            columnaProveedor.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaProveedor.FillWeight = 68F;

            DataGridViewTextBoxColumn columnaTotal =
                new DataGridViewTextBoxColumn();

            columnaTotal.Name = "colTotalProveedor";
            columnaTotal.HeaderText = "Total";
            columnaTotal.DataPropertyName = "TotalCompras";
            columnaTotal.ReadOnly = true;
            columnaTotal.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaTotal.FillWeight = 32F;

            columnaTotal.DefaultCellStyle.Format = "C2";
            columnaTotal.DefaultCellStyle.FormatProvider = cultura;

            columnaTotal.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotal.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvProveedores.Columns.Add(columnaProveedor);
            dgvProveedores.Columns.Add(columnaTotal);
        }

        // =========================================================
        // GRÁFICAS
        // =========================================================

        private void ConfigurarGraficas()
        {
            ConfigurarGraficaMensual();
            ConfigurarGraficaClasificacion();
            ConfigurarGraficaComparativo();
        }

        private void ConfigurarGraficaMensual()
        {
            if (chartMeses == null ||
                chartMeses.ChartAreas.Count == 0 ||
                chartMeses.Series.IndexOf("Compras") < 0)
            {
                return;
            }

            ChartArea area =
                chartMeses.ChartAreas["Compras"];

            Series serie =
                chartMeses.Series["Compras"];

            area.BackColor = Color.White;

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;
            area.AxisX.IsMarginVisible = true;

            area.AxisX.LabelStyle.Font =
                new Font("Segoe UI", 8F);

            area.AxisX.LabelStyle.ForeColor =
                Color.FromArgb(75, 88, 105);

            area.AxisX.LineColor =
                Color.FromArgb(215, 223, 233);

            area.AxisX.MajorTickMark.Enabled = false;

            area.AxisY.LabelStyle.Font =
                new Font("Segoe UI", 8F);

            area.AxisY.LabelStyle.ForeColor =
                Color.FromArgb(75, 88, 105);

            area.AxisY.LabelStyle.Format = "$0,K";

            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(229, 235, 242);

            area.AxisY.LineColor =
                Color.FromArgb(215, 223, 233);

            area.AxisY.MajorTickMark.Enabled = false;

            serie.ChartType =
                SeriesChartType.Column;

            serie.Color =
                Color.FromArgb(55, 112, 183);

            serie.IsValueShownAsLabel = false;

            serie["PointWidth"] = "0.55";

            serie.ToolTip =
                "#AXISLABEL: #VALY{C2}";
        }

        private void ConfigurarGraficaClasificacion()
        {
            if (chartClasificacion == null ||
                chartClasificacion.ChartAreas.Count == 0 ||
                chartClasificacion.Series.IndexOf("Clasificacion") < 0)
            {
                return;
            }

            ChartArea area =
                chartClasificacion.ChartAreas["Clasificacion"];

            Series serie =
                chartClasificacion.Series["Clasificacion"];

            chartClasificacion.BackColor =
                Color.White;

            area.BackColor =
                Color.White;

            // En una gráfica Bar:
            // AxisY = valores
            // AxisX = categorías

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;

            area.AxisX.LabelStyle.Font =
                new Font(
                    "Segoe UI",
                    8F);

            area.AxisX.LabelStyle.ForeColor =
                Color.FromArgb(
                    75,
                    88,
                    105);

            area.AxisX.LineColor =
                Color.FromArgb(
                    215,
                    223,
                    233);

            area.AxisX.MajorTickMark.Enabled =
                false;

            area.AxisY.LabelStyle.Font =
                new Font(
                    "Segoe UI",
                    8F);

            area.AxisY.LabelStyle.ForeColor =
                Color.FromArgb(
                    75,
                    88,
                    105);

            area.AxisY.LabelStyle.Format =
                "$0,K";

            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(
                    229,
                    235,
                    242);

            area.AxisY.LineColor =
                Color.FromArgb(
                    215,
                    223,
                    233);

            area.AxisY.MajorTickMark.Enabled =
                false;

            area.AxisY.Minimum = 0;

            serie.ChartType =
                SeriesChartType.Bar;

            serie.Color =
                Color.FromArgb(
                    55,
                    112,
                    183);

            serie.IsValueShownAsLabel =
                false;

            serie["PointWidth"] =
                "0.55";

            serie.ToolTip =
                "#AXISLABEL: #VALY{C2}";
        }

        private void ConfigurarGraficaComparativo()
        {
            if (chartComparativo == null ||
                chartComparativo.ChartAreas.Count == 0 ||
                chartComparativo.Series.IndexOf("Compras") < 0)
            {
                return;
            }

            ChartArea area =
                chartComparativo.ChartAreas["Compras"];

            Series serie =
                chartComparativo.Series["Compras"];

            area.BackColor = Color.White;

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;

            area.AxisX.LabelStyle.Font =
                new Font("Segoe UI", 9F);

            area.AxisX.LabelStyle.ForeColor =
                Color.FromArgb(75, 88, 105);

            area.AxisX.LineColor =
                Color.FromArgb(215, 223, 233);

            area.AxisX.MajorTickMark.Enabled = false;

            area.AxisY.LabelStyle.Font =
                new Font("Segoe UI", 8F);

            area.AxisY.LabelStyle.ForeColor =
                Color.FromArgb(75, 88, 105);

            area.AxisY.LabelStyle.Format = "$0,K";

            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(229, 235, 242);

            area.AxisY.LineColor =
                Color.FromArgb(215, 223, 233);

            area.AxisY.MajorTickMark.Enabled = false;

            serie.ChartType =
                SeriesChartType.Column;

            serie.Color =
                Color.FromArgb(55, 112, 183);

            serie.IsValueShownAsLabel = false;

            serie["PointWidth"] = "0.45";
        }

        // =========================================================
        // INICIALIZACIÓN
        // =========================================================

        private async Task InicializarAsync()
        {
            if (datos == null)
            {
                return;
            }

            inicializando = true;

            cmbAnio.Enabled = false;
            btnActualizar.Enabled = false;

            CancellationTokenSource carga =
                IniciarCarga();

            MostrarEstadoCarga(
                "Consultando años disponibles...");

            try
            {
                var anios =
                    await datos.ObtenerAniosAsync(
                        carga.Token);

                if (!EsCargaActual(carga))
                {
                    return;
                }

                cmbAnio.BeginUpdate();

                try
                {
                    cmbAnio.Items.Clear();

                    foreach (int anio in anios)
                    {
                        cmbAnio.Items.Add(anio);
                    }

                    int anioActual =
                        DateTime.Today.Year;

                    if (cmbAnio.Items.Contains(anioActual))
                    {
                        cmbAnio.SelectedItem =
                            anioActual;
                    }
                    else if (cmbAnio.Items.Count > 0)
                    {
                        cmbAnio.SelectedIndex = 0;
                    }
                }
                finally
                {
                    cmbAnio.EndUpdate();
                }

                inicializando = false;
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                if (EsCargaActual(carga))
                {
                    MostrarError(ex);
                }

                return;
            }
            finally
            {
                if (EsCargaActual(carga))
                {
                    cmbAnio.Enabled =
                        !inicializando;

                    btnActualizar.Enabled = true;

                    cargaActual = null;
                }

                carga.Dispose();
            }

            if (!inicializando &&
                !IsDisposed &&
                !Disposing)
            {
                await CargarResumenAsync();
            }
        }

        private async void cmbAnio_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (EstaEnModoDiseno())
            {
                return;
            }

            if (inicializando)
            {
                return;
            }

            await CargarResumenAsync();
        }

        private async void btnActualizar_Click(
            object sender,
            EventArgs e)
        {
            if (EstaEnModoDiseno())
            {
                return;
            }

            if (inicializando)
            {
                await InicializarAsync();
            }
            else
            {
                await CargarResumenAsync();
            }
        }

        // =========================================================
        // CARGAR RESUMEN
        // =========================================================

        private async Task CargarResumenAsync()
        {
            if (datos == null)
            {
                return;
            }

            if (cmbAnio.SelectedItem == null)
            {
                return;
            }

            int anio;

            if (!int.TryParse(
                cmbAnio.SelectedItem.ToString(),
                out anio))
            {
                return;
            }

            CancellationTokenSource carga =
                IniciarCarga();

            LimpiarResultados();

            btnActualizar.Enabled = false;
            cmbAnio.Enabled = false;

            MostrarEstadoCarga(
                "Cargando compras de " +
                anio +
                "...");

            try
            {
                ResumenDashboardCompras resumen =
                    await datos.ObtenerResumenAnualAsync(
                        anio,
                        carga.Token);

                if (!EsCargaActual(carga))
                {
                    return;
                }

                MostrarResumen(resumen);

                MostrarEstadoFinalizado();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                if (EsCargaActual(carga))
                {
                    MostrarError(ex);
                }
            }
            finally
            {
                if (EsCargaActual(carga))
                {
                    btnActualizar.Enabled = true;
                    cmbAnio.Enabled = true;
                    cargaActual = null;
                }

                carga.Dispose();
            }
        }

        // =========================================================
        // MOSTRAR RESULTADOS
        // =========================================================

        private void MostrarResumen(
            ResumenDashboardCompras resumen)
        {
            if (resumen == null)
            {
                return;
            }

            dgvMeses.DataSource =
                resumen.Meses.ToList();

            dgvProveedores.DataSource =
                resumen.Proveedores.ToList();

            ActualizarKpis(resumen);

            ActualizarGraficaMeses(resumen);

            ActualizarGraficaClasificacion(resumen);

            ActualizarGraficaComparativo(resumen);

            dgvMeses.ClearSelection();
            dgvProveedores.ClearSelection();
        }

        private void ActualizarKpis(
            ResumenDashboardCompras resumen)
        {
            lblKpiComprasValor.Text =
                resumen.TotalCompras.ToString(
                    "C2",
                    cultura);

            lblKpiComprasDetalle.Text =
                "Total acumulado " +
                resumen.Anio;

            lblKpiPromedioValor.Text =
                resumen.PromedioMensual.ToString(
                    "C2",
                    cultura);

            lblKpiPromedioDetalle.Text =
                "Promedio mensual";

            CompraProveedor principalProveedor =
                resumen.Proveedores
                    .OrderByDescending(
                        p => p.TotalCompras)
                    .ThenBy(
                        p => p.Proveedor)
                    .FirstOrDefault();

            if (principalProveedor != null)
            {
                lblKpiProveedorValor.Text =
                    principalProveedor.Proveedor;

                lblKpiProveedorDetalle.Text =
                    principalProveedor.TotalCompras.ToString(
                        "C2",
                        cultura);
            }
            else
            {
                lblKpiProveedorValor.Text =
                    "Sin movimientos";

                lblKpiProveedorDetalle.Text =
                    0M.ToString(
                        "C2",
                        cultura);
            }
        }

        private void ActualizarGraficaMeses(
            ResumenDashboardCompras resumen)
        {
            if (chartMeses == null ||
                chartMeses.Series.IndexOf("Compras") < 0)
            {
                return;
            }

            Series serie =
                chartMeses.Series["Compras"];

            serie.Points.Clear();

            foreach (CompraMensual mes in resumen.Meses)
            {
                string nombreCorto =
                    cultura.DateTimeFormat
                        .GetAbbreviatedMonthName(
                            mes.Mes);

                int indice =
                    serie.Points.AddXY(
                        nombreCorto,
                        Convert.ToDouble(
                            mes.TotalCompras));

                DataPoint punto =
                    serie.Points[indice];

                punto.ToolTip =
                    mes.NombreMes +
                    ": " +
                    mes.TotalCompras.ToString(
                        "C2",
                        cultura);

                punto.Label =
                    string.Empty;
            }
        }

        private void ActualizarGraficaClasificacion(
            ResumenDashboardCompras resumen)
        {
            if (chartClasificacion == null ||
                chartClasificacion.Series.IndexOf(
                    "Clasificacion") < 0)
            {
                return;
            }

            Series serie =
                chartClasificacion
                    .Series["Clasificacion"];

            serie.Points.Clear();

            if (resumen.Clasificaciones == null ||
                resumen.Clasificaciones.Count == 0)
            {
                return;
            }

            /*
             * Para una gráfica horizontal de tipo Bar,
             * agregamos primero los importes menores para
             * que visualmente el importe mayor quede arriba.
             *
             * El DTO conserva tanto Categoria como Familia,
             * aunque la etiqueta visible utiliza Familia
             * porque es la clasificación económica más útil
             * para esta gráfica.
             */
            var clasificaciones =
                resumen.Clasificaciones
                    .Where(
                        c => c != null)
                    .OrderBy(
                        c => c.Total)
                    .ThenBy(
                        c => c.Familia)
                    .ToList();

            foreach (
                CompraClasificacion clasificacion
                in clasificaciones)
            {
                string familia =
                    string.IsNullOrWhiteSpace(
                        clasificacion.Familia)
                        ? "Sin clasificación"
                        : clasificacion.Familia.Trim();

                int indice =
                    serie.Points.AddXY(
                        familia,
                        Convert.ToDouble(
                            clasificacion.Total));

                DataPoint punto =
                    serie.Points[indice];

                string categoria =
                    string.IsNullOrWhiteSpace(
                        clasificacion.Categoria)
                        ? string.Empty
                        : clasificacion.Categoria.Trim();

                if (string.IsNullOrWhiteSpace(
                    categoria))
                {
                    punto.ToolTip =
                        familia +
                        ": " +
                        clasificacion.Total.ToString(
                            "C2",
                            cultura);
                }
                else
                {
                    punto.ToolTip =
                        categoria +
                        " / " +
                        familia +
                        ": " +
                        clasificacion.Total.ToString(
                            "C2",
                            cultura);
                }

                punto.Label =
                    string.Empty;
            }
        }

        private void ActualizarGraficaComparativo(
            ResumenDashboardCompras resumen)
        {
            if (chartComparativo == null ||
                chartComparativo.Series.IndexOf("Compras") < 0)
            {
                return;
            }

            Series serie =
                chartComparativo.Series["Compras"];

            serie.Points.Clear();

            if (resumen.Comparativo == null)
            {
                return;
            }

            AgregarPuntoComparativo(
                serie,
                resumen.Comparativo.AnioAnterior,
                resumen.Comparativo.TotalAnioAnterior);

            AgregarPuntoComparativo(
                serie,
                resumen.Comparativo.AnioActual,
                resumen.Comparativo.TotalAnioActual);
        }

        private void AgregarPuntoComparativo(
            Series serie,
            int anio,
            decimal total)
        {
            int indice =
                serie.Points.AddXY(
                    anio.ToString(),
                    Convert.ToDouble(total));

            DataPoint punto =
                serie.Points[indice];

            punto.ToolTip =
                anio.ToString() +
                ": " +
                total.ToString(
                    "C2",
                    cultura);

            punto.Label =
                string.Empty;
        }

        // =========================================================
        // ESTADO / CANCELACIÓN
        // =========================================================

        private void MostrarEstadoCarga(
            string mensaje)
        {
            lblEstado.ForeColor =
                Color.FromArgb(
                    95,
                    114,
                    139);

            lblEstado.Text =
                mensaje;
        }

        private void MostrarEstadoFinalizado()
        {
            lblEstado.ForeColor =
                Color.FromArgb(
                    95,
                    114,
                    139);

            lblEstado.Text =
                "Actualizado: " +
                DateTime.Now.ToString(
                    "HH:mm",
                    cultura);
        }

        private CancellationTokenSource IniciarCarga()
        {
            CancelarCarga();

            CancellationTokenSource nuevaCarga =
                new CancellationTokenSource();

            cargaActual =
                nuevaCarga;

            return nuevaCarga;
        }

        private bool EsCargaActual(
            CancellationTokenSource carga)
        {
            if (carga == null)
            {
                return false;
            }

            if (IsDisposed ||
                Disposing)
            {
                return false;
            }

            return
                ReferenceEquals(
                    cargaActual,
                    carga)
                &&
                !carga.IsCancellationRequested;
        }

        private void CancelarCarga()
        {
            CancellationTokenSource carga =
                cargaActual;

            cargaActual =
                null;

            if (carga == null)
            {
                return;
            }

            try
            {
                carga.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void DashboardCompras_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            CancelarCarga();
        }

        // =========================================================
        // LIMPIAR
        // =========================================================

        private void LimpiarResultados()
        {
            if (dgvMeses != null)
            {
                dgvMeses.DataSource =
                    null;
            }

            if (dgvProveedores != null)
            {
                dgvProveedores.DataSource =
                    null;
            }

            if (chartMeses != null &&
                chartMeses.Series.IndexOf(
                    "Compras") >= 0)
            {
                chartMeses
                    .Series["Compras"]
                    .Points
                    .Clear();
            }

            if (chartClasificacion != null &&
                chartClasificacion.Series.IndexOf(
                    "Clasificacion") >= 0)
            {
                chartClasificacion
                    .Series["Clasificacion"]
                    .Points
                    .Clear();
            }

            if (chartComparativo != null &&
                chartComparativo.Series.IndexOf(
                    "Compras") >= 0)
            {
                chartComparativo
                    .Series["Compras"]
                    .Points
                    .Clear();
            }

            if (lblKpiComprasValor != null)
            {
                lblKpiComprasValor.Text =
                    "—";
            }

            if (lblKpiComprasDetalle != null)
            {
                lblKpiComprasDetalle.Text =
                    "Total acumulado";
            }

            if (lblKpiPromedioValor != null)
            {
                lblKpiPromedioValor.Text =
                    "—";
            }

            if (lblKpiPromedioDetalle != null)
            {
                lblKpiPromedioDetalle.Text =
                    "Promedio mensual";
            }

            if (lblKpiProveedorValor != null)
            {
                lblKpiProveedorValor.Text =
                    "—";
            }

            if (lblKpiProveedorDetalle != null)
            {
                lblKpiProveedorDetalle.Text =
                    "—";
            }
        }

        private void MostrarError(
            Exception ex)
        {
            Trace.TraceError(
                "DashboardCompras: {0}",
                ex);

            LimpiarResultados();

            if (lblEstado == null)
            {
                return;
            }

            lblEstado.ForeColor =
                Color.FromArgb(
                    160,
                    45,
                    45);

            if (ex is InvalidOperationException)
            {
                lblEstado.Text =
                    ex.Message;
            }
            else
            {
                lblEstado.Text =
                    "No fue posible consultar las compras. " +
                    "Revisa la conexión y pulsa Actualizar.";
            }
        }
    }
}