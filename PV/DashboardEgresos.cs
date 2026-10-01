using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class DashboardEgresos : Form
    {
        private readonly CultureInfo cultura =
            CultureInfo.GetCultureInfo("es-MX");

        private IDashboardEgresos datos;

        private CancellationTokenSource cargaActual;

        private bool inicializando;

        // =========================================================
        // CONSTRUCTORES
        // =========================================================

        public DashboardEgresos()
        {
            InitializeComponent();

            Shown += DashboardEgresos_Shown;
            FormClosed += DashboardEgresos_FormClosed;
        }

        public DashboardEgresos(
            IDashboardEgresos datos)
            : this()
        {
            this.datos = datos;
        }

        // =========================================================
        // INICIO
        // =========================================================

        private async void DashboardEgresos_Shown(
            object sender,
            EventArgs e)
        {
            if (EsModoDiseno())
            {
                return;
            }

            try
            {
                ConfigurarTablaMeses();
                ConfigurarTablaProveedores();
                ConfigurarTablaCuentas();
                ConfigurarTablaAntiguedad();

                ConfigurarGraficaMensual();
                ConfigurarGraficaAntiguedad();

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

        private IDashboardEgresos CrearServicioDatos()
        {
            return new DBDashboardEgresos();
        }

        private bool EsModoDiseno()
        {
            return LicenseManager.UsageMode ==
                   LicenseUsageMode.Designtime ||
                   DesignMode;
        }

        // =========================================================
        // TABLA MESES
        // =========================================================

        private void ConfigurarTablaMeses()
        {
            dgvMeses.AutoGenerateColumns = false;
            dgvMeses.Columns.Clear();

            DataGridViewTextBoxColumn columnaMes =
                new DataGridViewTextBoxColumn();

            columnaMes.Name = "colMes";
            columnaMes.HeaderText = "Mes";
            columnaMes.DataPropertyName = "NombreMes";
            columnaMes.ReadOnly = true;
            columnaMes.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaMes.FillWeight = 45F;

            DataGridViewTextBoxColumn columnaTotal =
                new DataGridViewTextBoxColumn();

            columnaTotal.Name = "colTotalEgreso";
            columnaTotal.HeaderText = "Total Egresos";
            columnaTotal.DataPropertyName = "TotalEgresos";
            columnaTotal.ReadOnly = true;
            columnaTotal.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaTotal.FillWeight = 55F;

            columnaTotal.DefaultCellStyle.Format = "C2";
            columnaTotal.DefaultCellStyle.FormatProvider =
                cultura;

            columnaTotal.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotal.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvMeses.Columns.Add(columnaMes);
            dgvMeses.Columns.Add(columnaTotal);
        }

        // =========================================================
        // TABLA PROVEEDORES
        // =========================================================

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

            DataGridViewTextBoxColumn columnaSaldo =
                new DataGridViewTextBoxColumn();

            columnaSaldo.Name = "colSaldoProveedor";
            columnaSaldo.HeaderText = "Saldo";
            columnaSaldo.DataPropertyName = "Saldo";
            columnaSaldo.ReadOnly = true;
            columnaSaldo.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaSaldo.FillWeight = 32F;

            columnaSaldo.DefaultCellStyle.Format = "C2";
            columnaSaldo.DefaultCellStyle.FormatProvider =
                cultura;

            columnaSaldo.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaSaldo.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvProveedores.Columns.Add(columnaProveedor);
            dgvProveedores.Columns.Add(columnaSaldo);
        }

        // =========================================================
        // TABLA CUENTAS
        // =========================================================

        private void ConfigurarTablaCuentas()
        {
            dgvCuentas.AutoGenerateColumns = false;
            dgvCuentas.Columns.Clear();

            DataGridViewTextBoxColumn columnaCuenta =
                new DataGridViewTextBoxColumn();

            columnaCuenta.Name = "colCuenta";
            columnaCuenta.HeaderText = "Cuenta";
            columnaCuenta.DataPropertyName = "Cuenta";
            columnaCuenta.ReadOnly = true;
            columnaCuenta.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaCuenta.FillWeight = 68F;

            DataGridViewTextBoxColumn columnaTotal =
                new DataGridViewTextBoxColumn();

            columnaTotal.Name = "colTotalCuenta";
            columnaTotal.HeaderText = "Egresos";
            columnaTotal.DataPropertyName = "Egresos";
            columnaTotal.ReadOnly = true;
            columnaTotal.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaTotal.FillWeight = 32F;

            columnaTotal.DefaultCellStyle.Format = "C2";
            columnaTotal.DefaultCellStyle.FormatProvider =
                cultura;

            columnaTotal.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaTotal.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvCuentas.Columns.Add(columnaCuenta);
            dgvCuentas.Columns.Add(columnaTotal);
        }

        // =========================================================
        // TABLA ANTIGÜEDAD
        // =========================================================

        private void ConfigurarTablaAntiguedad()
        {
            dgvAntiguedad.AutoGenerateColumns = false;
            dgvAntiguedad.Columns.Clear();

            DataGridViewTextBoxColumn columnaRango =
                new DataGridViewTextBoxColumn();

            columnaRango.Name = "colRango";
            columnaRango.HeaderText = "Antigüedad";
            columnaRango.DataPropertyName = "Rango";
            columnaRango.ReadOnly = true;
            columnaRango.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaRango.FillWeight = 60F;

            DataGridViewTextBoxColumn columnaSaldo =
                new DataGridViewTextBoxColumn();

            columnaSaldo.Name = "colSaldoAntiguedad";
            columnaSaldo.HeaderText = "Saldo";
            columnaSaldo.DataPropertyName = "Saldo";
            columnaSaldo.ReadOnly = true;
            columnaSaldo.SortMode =
                DataGridViewColumnSortMode.NotSortable;
            columnaSaldo.FillWeight = 40F;

            columnaSaldo.DefaultCellStyle.Format = "C2";
            columnaSaldo.DefaultCellStyle.FormatProvider =
                cultura;

            columnaSaldo.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            columnaSaldo.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvAntiguedad.Columns.Add(columnaRango);
            dgvAntiguedad.Columns.Add(columnaSaldo);
        }

        // =========================================================
        // GRÁFICA MENSUAL
        // =========================================================

        private void ConfigurarGraficaMensual()
        {
            chartMensual.Series.Clear();

            if (chartMensual.ChartAreas.Count == 0)
            {
                chartMensual.ChartAreas.Add(
                    new ChartArea("Principal"));
            }

            ChartArea area =
                chartMensual.ChartAreas[0];

            area.AxisX.MajorGrid.Enabled = false;

            area.AxisY.MajorGrid.LineColor =
                System.Drawing.Color.Gainsboro;

            area.AxisX.Interval = 1;

            area.AxisY.LabelStyle.Format = "C0";

            area.AxisX.LabelStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            area.AxisY.LabelStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            Series serie =
                new Series("Egresos");

            serie.ChartType =
                SeriesChartType.Column;

            serie.XValueType =
                ChartValueType.String;

            serie.YValueType =
                ChartValueType.Double;

            serie.IsValueShownAsLabel = false;

            serie["PointWidth"] = "0.6";

            chartMensual.Series.Add(serie);
        }

        // =========================================================
        // GRÁFICA ANTIGÜEDAD
        // =========================================================

        private void ConfigurarGraficaAntiguedad()
        {
            chartAntiguedad.Series.Clear();

            if (chartAntiguedad.ChartAreas.Count == 0)
            {
                chartAntiguedad.ChartAreas.Add(
                    new ChartArea("Principal"));
            }

            ChartArea area =
                chartAntiguedad.ChartAreas[0];

            area.AxisX.MajorGrid.Enabled = false;

            area.AxisY.MajorGrid.LineColor =
                System.Drawing.Color.Gainsboro;

            area.AxisX.Interval = 1;

            area.AxisY.LabelStyle.Format = "C0";

            area.AxisX.LabelStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            area.AxisY.LabelStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8F);

            Series serie =
                new Series("Cartera");

            serie.ChartType =
                SeriesChartType.Column;

            serie.XValueType =
                ChartValueType.String;

            serie.YValueType =
                ChartValueType.Double;

            serie.IsValueShownAsLabel = false;

            serie["PointWidth"] = "0.6";

            chartAntiguedad.Series.Add(serie);
        }

        // =========================================================
        // INICIALIZACIÓN
        // =========================================================

        private async Task InicializarAsync()
        {
            if (inicializando)
            {
                return;
            }

            inicializando = true;

            try
            {
                EstablecerCargando(true);

                IReadOnlyList<int> anios =
                    await datos.ObtenerAniosAsync(
                        CancellationToken.None);

                cmbAnio.DataSource = null;

                cmbAnio.DataSource =
                    anios.ToList();

                int anioActual =
                    DateTime.Today.Year;

                if (anios.Contains(anioActual))
                {
                    cmbAnio.SelectedItem =
                        anioActual;
                }
                else if (anios.Count > 0)
                {
                    cmbAnio.SelectedIndex = 0;
                }

                if (cmbAnio.SelectedItem != null)
                {
                    await CargarResumenAsync();
                }
            }
            finally
            {
                inicializando = false;

                EstablecerCargando(false);
            }
        }

        // =========================================================
        // BOTÓN ACTUALIZAR
        // =========================================================

        private async void btnActualizar_Click(
            object sender,
            EventArgs e)
        {
            if (inicializando)
            {
                return;
            }

            await CargarResumenAsync();
        }

        // =========================================================
        // CARGAR RESUMEN
        // =========================================================

        private async Task CargarResumenAsync()
        {
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

            CancelarCargaActual();

            cargaActual =
                new CancellationTokenSource();

            CancellationToken token =
                cargaActual.Token;

            try
            {
                EstablecerCargando(true);

                ResumenDashboardEgresos resumen =
                    await datos.ObtenerResumenAnualAsync(
                        anio,
                        token);

                token.ThrowIfCancellationRequested();

                MostrarResumen(resumen);
            }
            catch (OperationCanceledException)
            {
                // Carga cancelada.
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                if (!token.IsCancellationRequested)
                {
                    EstablecerCargando(false);
                }
            }
        }

        // =========================================================
        // MOSTRAR RESUMEN
        // =========================================================

        private void MostrarResumen(
            ResumenDashboardEgresos resumen)
        {
            if (resumen == null)
            {
                return;
            }

            MostrarKpis(resumen);

            MostrarMeses(
                resumen.Meses);

            MostrarProveedores(
                resumen.Proveedores);

            MostrarCuentas(
                resumen.Cuentas);

            MostrarAntiguedad(
                resumen.Antiguedad);

            MostrarGraficaMensual(
                resumen.Meses);

            MostrarGraficaAntiguedad(
                resumen.Antiguedad);

            lblEstado.Text =
                "Actualizado: " +
                DateTime.Now.ToString(
                    "HH:mm",
                    cultura);
        }

        // =========================================================
        // KPI
        // =========================================================

        private void MostrarKpis(
            ResumenDashboardEgresos resumen)
        {
            lblValorEgresos.Text =
                resumen.TotalEgresos.ToString(
                    "C2",
                    cultura);

            lblValorPromedio.Text =
                resumen.PromedioMensual.ToString(
                    "C2",
                    cultura);

            lblValorCartera.Text =
                resumen.TotalCartera.ToString(
                    "C2",
                    cultura);
        }

        // =========================================================
        // MOSTRAR TABLAS
        // =========================================================

        private void MostrarMeses(
            IReadOnlyList<EgresoMensual> meses)
        {
            dgvMeses.DataSource = null;

            dgvMeses.DataSource =
                meses == null
                    ? new List<EgresoMensual>()
                    : meses.ToList();
        }

        private void MostrarProveedores(
            IReadOnlyList<EgresoProveedor> proveedores)
        {
            dgvProveedores.DataSource = null;

            dgvProveedores.DataSource =
                proveedores == null
                    ? new List<EgresoProveedor>()
                    : proveedores.ToList();
        }

        private void MostrarCuentas(
            IReadOnlyList<EgresoCuentaBancaria> cuentas)
        {
            dgvCuentas.DataSource = null;

            dgvCuentas.DataSource =
                cuentas == null
                    ? new List<EgresoCuentaBancaria>()
                    : cuentas.ToList();
        }

        private void MostrarAntiguedad(
            IReadOnlyList<EgresoAntiguedad> antiguedad)
        {
            dgvAntiguedad.DataSource = null;

            dgvAntiguedad.DataSource =
                antiguedad == null
                    ? new List<EgresoAntiguedad>()
                    : antiguedad.ToList();
        }

        // =========================================================
        // MOSTRAR GRÁFICA MENSUAL
        // =========================================================

        private void MostrarGraficaMensual(
            IReadOnlyList<EgresoMensual> meses)
        {
            if (chartMensual.Series.Count == 0)
            {
                return;
            }

            Series serie =
                chartMensual.Series[0];

            serie.Points.Clear();

            if (meses == null)
            {
                return;
            }

            foreach (EgresoMensual mes in meses)
            {
                int indice =
                    serie.Points.AddXY(
                        AbreviarMes(
                            mes.NombreMes),
                        mes.TotalEgresos);

                serie.Points[indice].ToolTip =
                    mes.NombreMes +
                    ": " +
                    mes.TotalEgresos.ToString(
                        "C2",
                        cultura);
            }
        }

        // =========================================================
        // MOSTRAR GRÁFICA ANTIGÜEDAD
        // =========================================================

        private void MostrarGraficaAntiguedad(
            IReadOnlyList<EgresoAntiguedad> antiguedad)
        {
            if (chartAntiguedad.Series.Count == 0)
            {
                return;
            }

            Series serie =
                chartAntiguedad.Series[0];

            serie.Points.Clear();

            if (antiguedad == null)
            {
                return;
            }

            foreach (EgresoAntiguedad item in antiguedad)
            {
                int indice =
                    serie.Points.AddXY(
                        AbreviarRango(
                            item.Rango),
                        item.Saldo);

                serie.Points[indice].ToolTip =
                    item.Rango +
                    ": " +
                    item.Saldo.ToString(
                        "C2",
                        cultura);
            }
        }

        // =========================================================
        // ABREVIACIONES
        // =========================================================

        private static string AbreviarMes(
            string nombreMes)
        {
            if (string.IsNullOrWhiteSpace(nombreMes))
            {
                return string.Empty;
            }

            return nombreMes.Length <= 3
                ? nombreMes
                : nombreMes.Substring(0, 3);
        }

        private static string AbreviarRango(
            string rango)
        {
            if (string.IsNullOrWhiteSpace(rango))
            {
                return string.Empty;
            }

            if (rango == "Por Vencer")
            {
                return "Por vencer";
            }

            if (rango.StartsWith("1 - 30"))
            {
                return "1-30";
            }

            if (rango.StartsWith("31 - 60"))
            {
                return "31-60";
            }

            if (rango.StartsWith("61 - 90"))
            {
                return "61-90";
            }

            if (rango.StartsWith("91"))
            {
                return "91+";
            }

            return rango;
        }

        // =========================================================
        // ESTADO DE CARGA
        // =========================================================

        private void EstablecerCargando(
            bool cargando)
        {
            btnActualizar.Enabled =
                !cargando;

            cmbAnio.Enabled =
                !cargando;

            Cursor =
                cargando
                    ? Cursors.WaitCursor
                    : Cursors.Default;

            if (cargando)
            {
                lblEstado.Text =
                    "Cargando...";
            }
        }

        // =========================================================
        // CANCELACIÓN
        // =========================================================

        private void CancelarCargaActual()
        {
            if (cargaActual == null)
            {
                return;
            }

            try
            {
                cargaActual.Cancel();
            }
            catch
            {
                // No hacemos nada.
            }

            cargaActual.Dispose();

            cargaActual = null;
        }

        private void DashboardEgresos_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            CancelarCargaActual();
        }

        // =========================================================
        // ERRORES
        // =========================================================

        private void MostrarError(
            Exception ex)
        {
            if (IsDisposed)
            {
                return;
            }

            lblEstado.Text =
                "Error al cargar";

            MessageBox.Show(
                this,
                ex.Message,
                "Dashboard de Egresos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}