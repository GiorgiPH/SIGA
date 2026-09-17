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
using PV.dto;

namespace PV
{
    public partial class DashboardVentas : Form
    {
        private IDashboardVentas datos;
        private CancellationTokenSource cargaActual;

        private bool inicializando = true;
        private bool formularioInicializado = false;

        private readonly CultureInfo cultura =
            CultureInfo.GetCultureInfo("es-MX");

        public DashboardVentas()
        {
            InitializeComponent();
        }

        public DashboardVentas(IDashboardVentas datos)
            : this()
        {
            if (datos == null)
            {
                throw new ArgumentNullException("datos");
            }

            this.datos = datos;
        }

        private async void DashboardVentas_Shown(
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

        private IDashboardVentas CrearServicioDatos()
        {
            return new DBDashboardVentas();
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

        private void ConfigurarTablas()
        {
            // =========================================================
            // INGRESOS MENSUALES
            // =========================================================

            dgvMeses.AutoGenerateColumns = false;

            dgvMeses.Columns.Clear();

            DataGridViewTextBoxColumn columnaMes =
                new DataGridViewTextBoxColumn();

            columnaMes.Name =
                "colNombreMes";

            columnaMes.HeaderText =
                "Mes";

            columnaMes.DataPropertyName =
                "NombreMes";

            columnaMes.ReadOnly =
                true;

            columnaMes.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            columnaMes.FillWeight =
                55F;


            DataGridViewTextBoxColumn columnaTotalMes =
                new DataGridViewTextBoxColumn();

            columnaTotalMes.Name =
                "colTotalVentasMes";

            columnaTotalMes.HeaderText =
                "Total Ventas";

            columnaTotalMes.DataPropertyName =
                "TotalVentas";

            columnaTotalMes.ReadOnly =
                true;

            columnaTotalMes.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            columnaTotalMes.FillWeight =
                45F;

            columnaTotalMes.DefaultCellStyle.Format =
                "C2";

            columnaTotalMes.DefaultCellStyle.FormatProvider =
                cultura;

            columnaTotalMes.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotalMes.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvMeses.Columns.Add(
                columnaMes);

            dgvMeses.Columns.Add(
                columnaTotalMes);


            // =========================================================
            // INGRESOS POR CENTRO DE COSTOS
            // =========================================================

            dgvCentros.AutoGenerateColumns = false;

            dgvCentros.Columns.Clear();

            DataGridViewTextBoxColumn columnaCentro =
                new DataGridViewTextBoxColumn();

            columnaCentro.Name =
                "colCentro";

            columnaCentro.HeaderText =
                "Centro";

            columnaCentro.DataPropertyName =
                "ClaveCentroCostos";

            columnaCentro.ReadOnly =
                true;

            columnaCentro.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            columnaCentro.FillWeight =
                20F;


            DataGridViewTextBoxColumn columnaNombreCentro =
                new DataGridViewTextBoxColumn();

            columnaNombreCentro.Name =
                "colNombreCentro";

            columnaNombreCentro.HeaderText =
                "Nombre";

            columnaNombreCentro.DataPropertyName =
                "NombreCentroCostos";

            columnaNombreCentro.ReadOnly =
                true;

            columnaNombreCentro.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            columnaNombreCentro.FillWeight =
                45F;


            DataGridViewTextBoxColumn columnaTotalCentro =
                new DataGridViewTextBoxColumn();

            columnaTotalCentro.Name =
                "colTotalVentasCentro";

            columnaTotalCentro.HeaderText =
                "Total Ventas";

            columnaTotalCentro.DataPropertyName =
                "TotalVentas";

            columnaTotalCentro.ReadOnly =
                true;

            columnaTotalCentro.SortMode =
                DataGridViewColumnSortMode.NotSortable;

            columnaTotalCentro.FillWeight =
                35F;

            columnaTotalCentro.DefaultCellStyle.Format =
                "C2";

            columnaTotalCentro.DefaultCellStyle.FormatProvider =
                cultura;

            columnaTotalCentro.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotalCentro.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvCentros.Columns.Add(
                columnaCentro);

            dgvCentros.Columns.Add(
                columnaNombreCentro);

            dgvCentros.Columns.Add(
                columnaTotalCentro);
        }

        private void ConfigurarGraficas()
        {
            ConfigurarGraficaMensual();
            ConfigurarGraficaCentros();
        }

        private void ConfigurarGraficaMensual()
        {
            if (chartMeses == null ||
                chartMeses.ChartAreas.Count == 0 ||
                chartMeses.Series.IndexOf("Ventas") < 0)
            {
                return;
            }

            ChartArea area =
                chartMeses.ChartAreas["Ventas"];

            Series serie =
                chartMeses.Series["Ventas"];

            area.BackColor =
                Color.White;

            area.AxisX.MajorGrid.Enabled =
                false;

            area.AxisX.Interval =
                1;

            area.AxisX.IsMarginVisible =
                true;

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

            /*
             * Los importes grandes se presentan abreviados.
             *
             * Ejemplo:
             * $200K
             * $400K
             */
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

            serie.ChartType =
                SeriesChartType.Column;

            serie.Color =
                Color.FromArgb(
                    55,
                    112,
                    183);

            /*
             * No mostrar etiquetas con importes
             * encima de las columnas.
             */
            serie.IsValueShownAsLabel =
                false;

            serie["PointWidth"] =
                "0.55";

            serie.ToolTip =
                "#AXISLABEL: #VALY{C2}";
        }

        private void ConfigurarGraficaCentros()
        {
            if (chartCentros == null ||
                chartCentros.ChartAreas.Count == 0 ||
                chartCentros.Series.IndexOf("Ventas") < 0)
            {
                return;
            }

            ChartArea area =
                chartCentros.ChartAreas["Ventas"];

            Series serie =
                chartCentros.Series["Ventas"];

            area.BackColor =
                Color.White;

            area.AxisX.IsReversed =
                true;

            area.AxisX.Interval =
                1;

            area.AxisX.MajorGrid.Enabled =
                false;

            area.AxisX.LabelStyle.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

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

            area.AxisY.IsMarginVisible =
                true;

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

            /*
             * Colocamos la escala arriba,
             * como en el diseño propuesto.
             */
            area.AxisY.IsLabelAutoFit =
                true;

            serie.ChartType =
                SeriesChartType.Bar;

            serie.Color =
                Color.FromArgb(
                    55,
                    112,
                    183);

            /*
             * No mostrar valores directamente
             * sobre las barras.
             */
            serie.IsValueShownAsLabel =
                false;

            serie["PointWidth"] =
                "0.55";

            serie.ToolTip =
                "#AXISLABEL: #VALY{C2}";
        }

        private async Task InicializarAsync()
        {
            if (datos == null)
            {
                return;
            }

            inicializando = true;

            cmbAnio.Enabled =
                false;

            btnActualizar.Enabled =
                false;

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

                    if (cmbAnio.Items.Contains(
                        anioActual))
                    {
                        cmbAnio.SelectedItem =
                            anioActual;
                    }
                    else if (
                        cmbAnio.Items.Count > 0)
                    {
                        cmbAnio.SelectedIndex =
                            0;
                    }
                }
                finally
                {
                    cmbAnio.EndUpdate();
                }

                inicializando =
                    false;
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

                    btnActualizar.Enabled =
                        true;

                    cargaActual =
                        null;
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

            btnActualizar.Enabled =
                false;

            cmbAnio.Enabled =
                false;

            MostrarEstadoCarga(
                "Cargando ingresos de " +
                anio +
                "...");

            try
            {
                ResumenDashboardVentas resumen =
                    await datos.ObtenerResumenAnualAsync(
                        anio,
                        carga.Token);

                if (!EsCargaActual(carga))
                {
                    return;
                }

                MostrarResumen(
                    resumen);

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
                    btnActualizar.Enabled =
                        true;

                    cmbAnio.Enabled =
                        true;

                    cargaActual =
                        null;
                }

                carga.Dispose();
            }
        }

        private void MostrarResumen(
            ResumenDashboardVentas resumen)
        {
            if (resumen == null)
            {
                return;
            }

            /*
             * No filtramos ni ocultamos valores cero.
             */
            dgvMeses.DataSource =
                resumen.Meses.ToList();

            dgvCentros.DataSource =
                resumen.Centros.ToList();

            ActualizarKpis(
                resumen);

            ActualizarGraficaMeses(
                resumen);

            ActualizarGraficaCentros(
                resumen);

            dgvMeses.ClearSelection();
            dgvCentros.ClearSelection();
        }

        private void ActualizarKpis(
            ResumenDashboardVentas resumen)
        {
            lblKpiIngresosValor.Text =
                resumen.TotalAnual.ToString(
                    "C2",
                    cultura);

            lblKpiIngresosDetalle.Text =
                "Total acumulado " +
                resumen.Anio;

            VentaMensual mejorMes =
                resumen.Meses
                .OrderByDescending(
                    m => m.TotalVentas)
                .ThenBy(
                    m => m.Mes)
                .FirstOrDefault();

            if (mejorMes != null)
            {
                lblKpiMejorMesValor.Text =
                    mejorMes.NombreMes;

                lblKpiMejorMesDetalle.Text =
                    mejorMes.TotalVentas.ToString(
                        "C2",
                        cultura);
            }
            else
            {
                lblKpiMejorMesValor.Text =
                    "—";

                lblKpiMejorMesDetalle.Text =
                    0M.ToString(
                        "C2",
                        cultura);
            }

            VentaCentroCostos principalCentro =
                resumen.Centros
                .OrderByDescending(
                    c => c.TotalVentas)
                .ThenBy(
                    c => c.NombreCentroCostos)
                .FirstOrDefault();

            if (principalCentro != null)
            {
                lblKpiCentroValor.Text =
                    principalCentro.NombreCentroCostos;

                lblKpiCentroDetalle.Text =
                    principalCentro.TotalVentas.ToString(
                        "C2",
                        cultura);
            }
            else
            {
                lblKpiCentroValor.Text =
                    "Sin movimientos";

                lblKpiCentroDetalle.Text =
                    0M.ToString(
                        "C2",
                        cultura);
            }
        }

        private void ActualizarGraficaMeses(
            ResumenDashboardVentas resumen)
        {
            Series serie =
                chartMeses.Series["Ventas"];

            serie.Points.Clear();

            foreach (
                VentaMensual mes
                in resumen.Meses)
            {
                string nombreCorto =
                    cultura.DateTimeFormat
                    .GetAbbreviatedMonthName(
                        mes.Mes);

                int indice =
                    serie.Points.AddXY(
                        nombreCorto,
                        Convert.ToDouble(
                            mes.TotalVentas));

                DataPoint punto =
                    serie.Points[indice];

                punto.ToolTip =
                    mes.NombreMes +
                    ": " +
                    mes.TotalVentas.ToString(
                        "C2",
                        cultura);

                /*
                 * Sin etiquetas sobre las barras.
                 */
                punto.Label =
                    string.Empty;
            }
        }

        private void ActualizarGraficaCentros(
            ResumenDashboardVentas resumen)
        {
            Series serie =
                chartCentros.Series["Ventas"];

            serie.Points.Clear();

            foreach (
                VentaCentroCostos centro
                in resumen.Centros)
            {
                int indice =
                    serie.Points.AddXY(
                        centro.NombreCentroCostos,
                        Convert.ToDouble(
                            centro.TotalVentas));

                DataPoint punto =
                    serie.Points[indice];

                punto.ToolTip =
                    centro.NombreCentroCostos +
                    ": " +
                    centro.TotalVentas.ToString(
                        "C2",
                        cultura);

                /*
                 * Sin etiquetas sobre las barras.
                 */
                punto.Label =
                    string.Empty;
            }

            AjustarAltoGraficaCentros(
                resumen.Centros.Count);
        }

        private void AjustarAltoGraficaCentros(
            int cantidadCentros)
        {
            int altoDisponible =
                pnlGraficaCentros.ClientSize.Height;

            if (altoDisponible <= 0)
            {
                altoDisponible = 200;
            }

            if (cantidadCentros <= 6)
            {
                pnlGraficaCentros.AutoScroll =
                    false;

                chartCentros.Dock =
                    DockStyle.Fill;

                chartCentros.Height =
                    altoDisponible;

                return;
            }

            pnlGraficaCentros.AutoScroll =
                true;

            chartCentros.Dock =
                DockStyle.Top;

            chartCentros.Height =
                Math.Max(
                    altoDisponible,
                    cantidadCentros * 38 + 50);

            pnlGraficaCentros.AutoScrollPosition =
                Point.Empty;
        }

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

        private void DashboardVentas_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            CancelarCarga();
        }

        private void LimpiarResultados()
        {
            if (dgvMeses != null)
            {
                dgvMeses.DataSource =
                    null;
            }

            if (dgvCentros != null)
            {
                dgvCentros.DataSource =
                    null;
            }

            if (chartMeses != null &&
                chartMeses.Series.IndexOf(
                    "Ventas") >= 0)
            {
                chartMeses
                    .Series["Ventas"]
                    .Points
                    .Clear();
            }

            if (chartCentros != null &&
                chartCentros.Series.IndexOf(
                    "Ventas") >= 0)
            {
                chartCentros
                    .Series["Ventas"]
                    .Points
                    .Clear();
            }

            if (lblKpiIngresosValor != null)
            {
                lblKpiIngresosValor.Text =
                    "—";
            }

            if (lblKpiIngresosDetalle != null)
            {
                lblKpiIngresosDetalle.Text =
                    "Total acumulado";
            }

            if (lblKpiMejorMesValor != null)
            {
                lblKpiMejorMesValor.Text =
                    "—";
            }

            if (lblKpiMejorMesDetalle != null)
            {
                lblKpiMejorMesDetalle.Text =
                    "—";
            }

            if (lblKpiCentroValor != null)
            {
                lblKpiCentroValor.Text =
                    "—";
            }

            if (lblKpiCentroDetalle != null)
            {
                lblKpiCentroDetalle.Text =
                    "—";
            }
        }

        private void MostrarError(
            Exception ex)
        {
            Trace.TraceError(
                "DashboardVentas: {0}",
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
                    "No fue posible consultar los ingresos. " +
                    "Revisa la conexión y pulsa Actualizar.";
            }
        }

        private void dgvCentros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            VentaCentroCostos centro =
                dgvCentros.Rows[e.RowIndex]
                .DataBoundItem as VentaCentroCostos;

            if (centro == null)
            {
                return;
            }

            if (!centro.ClaveCentroCostos.HasValue)
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

            using (DashboardVentasCentroCostos ventana = new DashboardVentasCentroCostos( datos, anio, centro.ClaveCentroCostos.Value,centro.NombreCentroCostos))
            {
                ventana.ShowDialog(this);
            }
        }
    }
}