using System;
using System.Collections.Generic;
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
    public partial class DashboardVentasCentroCostos : Form
    {
        private readonly IDashboardVentas datos;
        private readonly int anio;
        private readonly int centroCostos;
        private readonly string nombreCentroCostos;

        private CancellationTokenSource cargaActual;
        private bool formularioInicializado;

        private readonly CultureInfo cultura =
            CultureInfo.GetCultureInfo("es-MX");

        public DashboardVentasCentroCostos(
            IDashboardVentas datos,
            int anio,
            int centroCostos,
            string nombreCentroCostos)
        {
            if (datos == null)
            {
                throw new ArgumentNullException("datos");
            }

            this.datos = datos;
            this.anio = anio;
            this.centroCostos = centroCostos;
            this.nombreCentroCostos =
                string.IsNullOrWhiteSpace(nombreCentroCostos)
                ? centroCostos.ToString()
                : nombreCentroCostos;

            InitializeComponent();
        }

        private async void DashboardVentasCentroCostos_Shown(
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
                ConfigurarVentana();
                ConfigurarTabla();
                ConfigurarGrafica();

                await CargarDatosAsync();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
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

        private void ConfigurarVentana()
        {
            lblCentroValor.Text =
                nombreCentroCostos;

            lblAnioValor.Text =
                anio.ToString();

            Text =
                "Detalle de Ventas - " +
                nombreCentroCostos;
        }

        private void ConfigurarTabla()
        {
            if (dgvMeses == null)
            {
                return;
            }

            DataGridViewColumn columnaImporte = null;

            if (dgvMeses.Columns.Contains("colImporte"))
            {
                columnaImporte =
                    dgvMeses.Columns["colImporte"];
            }
            else if (dgvMeses.Columns.Count > 1)
            {
                columnaImporte =
                    dgvMeses.Columns[1];
            }

            if (columnaImporte == null)
            {
                return;
            }

            columnaImporte.DefaultCellStyle.Format =
                "C2";

            columnaImporte.DefaultCellStyle.FormatProvider =
                cultura;

            columnaImporte.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void ConfigurarGrafica()
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

            area.AxisX.Interval =
                1;

            area.AxisX.MajorGrid.Enabled =
                false;

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

            serie.ChartType =
                SeriesChartType.Column;

            serie.Color =
                Color.FromArgb(
                    55,
                    112,
                    183);

            serie.IsVisibleInLegend =
                false;

            serie.IsValueShownAsLabel =
                false;

            serie["PointWidth"] =
                "0.55";

            serie.ToolTip =
                "#AXISLABEL: #VALY{C2}";
        }

        private async Task CargarDatosAsync()
        {
            CancellationTokenSource carga =
                IniciarCarga();

            MostrarEstado(
                "Consultando ingresos...");

            try
            {
                IReadOnlyList<VentaMensual> meses =
                    await datos.ObtenerResumenCentroCostosAsync(
                        anio,
                        centroCostos,
                        carga.Token);

                if (!EsCargaActual(carga))
                {
                    return;
                }

                MostrarDatos(meses);

                lblEstado.Text =
                    "Actualizado: " +
                    DateTime.Now.ToString(
                        "HH:mm",
                        cultura);
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
                    cargaActual = null;
                }

                carga.Dispose();
            }
        }

        private void MostrarDatos(
            IReadOnlyList<VentaMensual> meses)
        {
            if (meses == null)
            {
                return;
            }

            dgvMeses.DataSource =
                meses.ToList();

            decimal totalAnual =
                meses.Sum(
                    m => m.TotalVentas);

            lblTotalValor.Text =
                totalAnual.ToString(
                    "C2",
                    cultura);

            Series serie =
                chartMeses.Series["Ventas"];

            serie.Points.Clear();

            foreach (VentaMensual mes in meses)
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

                punto.Label =
                    string.Empty;

                punto.ToolTip =
                    mes.NombreMes +
                    ": " +
                    mes.TotalVentas.ToString(
                        "C2",
                        cultura);
            }

            dgvMeses.ClearSelection();
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

        private void DashboardVentasCentroCostos_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            CancelarCarga();
        }

        private void MostrarEstado(
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

        private void MostrarError(
            Exception ex)
        {
            Trace.TraceError(
                "DashboardVentasCentroCostos: {0}",
                ex);

            lblEstado.ForeColor =
                Color.FromArgb(
                    160,
                    45,
                    45);

            lblEstado.Text =
                ex is InvalidOperationException
                ? ex.Message
                : "No fue posible consultar el detalle del centro de costos.";
        }
    }
}