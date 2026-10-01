namespace PV
{
    partial class DashboardCompras
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel principal;
        private System.Windows.Forms.TableLayoutPanel encabezado;
        private System.Windows.Forms.TableLayoutPanel pnlKpis;
        private System.Windows.Forms.TableLayoutPanel tarjetas;

        // ENCABEZADO
        private Guna.UI2.WinForms.Guna2Panel pnlCabeceraTitulo;
        private Guna.UI2.WinForms.Guna2Panel pnlCabeceraFiltros;

        private System.Windows.Forms.TableLayoutPanel layoutCabeceraTitulo;
        private System.Windows.Forms.TableLayoutPanel layoutCabeceraFiltros;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTituloFiltros;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.Label lblEstado;

        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;

        private Guna.UI2.WinForms.Guna2ComboBox cmbAnio;
        private Guna.UI2.WinForms.Guna2Button btnActualizar;

        // KPI
        private Guna.UI2.WinForms.Guna2Panel pnlKpiCompras;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiPromedio;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiProveedor;

        private System.Windows.Forms.TableLayoutPanel layoutKpiCompras;
        private System.Windows.Forms.TableLayoutPanel layoutKpiPromedio;
        private System.Windows.Forms.TableLayoutPanel layoutKpiProveedor;

        private System.Windows.Forms.Label lblKpiComprasTitulo;
        private System.Windows.Forms.Label lblKpiComprasValor;
        private System.Windows.Forms.Label lblKpiComprasDetalle;

        private System.Windows.Forms.Label lblKpiPromedioTitulo;
        private System.Windows.Forms.Label lblKpiPromedioValor;
        private System.Windows.Forms.Label lblKpiPromedioDetalle;

        private System.Windows.Forms.Label lblKpiProveedorTitulo;
        private System.Windows.Forms.Label lblKpiProveedorValor;
        private System.Windows.Forms.Label lblKpiProveedorDetalle;

        // TARJETAS
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaGraficaMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaProveedores;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaClasificacion;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaComparativo;

        private System.Windows.Forms.TableLayoutPanel layoutMeses;
        private System.Windows.Forms.TableLayoutPanel layoutGraficaMeses;
        private System.Windows.Forms.TableLayoutPanel layoutProveedores;
        private System.Windows.Forms.TableLayoutPanel layoutClasificacion;
        private System.Windows.Forms.TableLayoutPanel layoutComparativo;

        private System.Windows.Forms.Label lblTituloMeses;
        private System.Windows.Forms.Label lblTituloGraficaMeses;
        private System.Windows.Forms.Label lblTituloProveedores;
        private System.Windows.Forms.Label lblTituloClasificacion;
        private System.Windows.Forms.Label lblTituloComparativo;

        private Guna.UI2.WinForms.Guna2DataGridView dgvMeses;
        private Guna.UI2.WinForms.Guna2DataGridView dgvProveedores;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeses;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartClasificacion;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartComparativo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.Series series1 =
                new System.Windows.Forms.DataVisualization.Charting.Series();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.Series series2 =
                new System.Windows.Forms.DataVisualization.Charting.Series();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.Series series3 =
                new System.Windows.Forms.DataVisualization.Charting.Series();

            this.principal =
                new System.Windows.Forms.TableLayoutPanel();

            this.encabezado =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlCabeceraTitulo =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutCabeceraTitulo =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTitulo =
                new System.Windows.Forms.Label();

            this.lblSubtitulo =
                new System.Windows.Forms.Label();

            this.pnlCabeceraFiltros =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutCabeceraFiltros =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloFiltros =
                new System.Windows.Forms.Label();

            this.pnlFiltros =
                new System.Windows.Forms.FlowLayoutPanel();

            this.btnActualizar =
                new Guna.UI2.WinForms.Guna2Button();

            this.cmbAnio =
                new Guna.UI2.WinForms.Guna2ComboBox();

            this.lblAnio =
                new System.Windows.Forms.Label();

            this.pnlKpis =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlKpiCompras =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutKpiCompras =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblKpiComprasTitulo =
                new System.Windows.Forms.Label();

            this.lblKpiComprasValor =
                new System.Windows.Forms.Label();

            this.lblKpiComprasDetalle =
                new System.Windows.Forms.Label();

            this.pnlKpiPromedio =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutKpiPromedio =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblKpiPromedioTitulo =
                new System.Windows.Forms.Label();

            this.lblKpiPromedioValor =
                new System.Windows.Forms.Label();

            this.lblKpiPromedioDetalle =
                new System.Windows.Forms.Label();

            this.pnlKpiProveedor =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutKpiProveedor =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblKpiProveedorTitulo =
                new System.Windows.Forms.Label();

            this.lblKpiProveedorValor =
                new System.Windows.Forms.Label();

            this.lblKpiProveedorDetalle =
                new System.Windows.Forms.Label();

            this.tarjetas =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlTarjetaMeses =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutMeses =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloMeses =
                new System.Windows.Forms.Label();

            this.dgvMeses =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlTarjetaGraficaMeses =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutGraficaMeses =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloGraficaMeses =
                new System.Windows.Forms.Label();

            this.chartMeses =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlTarjetaProveedores =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutProveedores =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloProveedores =
                new System.Windows.Forms.Label();

            this.dgvProveedores =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlTarjetaClasificacion =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutClasificacion =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloClasificacion =
                new System.Windows.Forms.Label();

            this.chartClasificacion =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlTarjetaComparativo =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutComparativo =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloComparativo =
                new System.Windows.Forms.Label();

            this.chartComparativo =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.lblEstado =
                new System.Windows.Forms.Label();

            this.principal.SuspendLayout();
            this.encabezado.SuspendLayout();
            this.pnlCabeceraTitulo.SuspendLayout();
            this.layoutCabeceraTitulo.SuspendLayout();
            this.pnlCabeceraFiltros.SuspendLayout();
            this.layoutCabeceraFiltros.SuspendLayout();
            this.pnlFiltros.SuspendLayout();

            this.pnlKpis.SuspendLayout();
            this.pnlKpiCompras.SuspendLayout();
            this.layoutKpiCompras.SuspendLayout();
            this.pnlKpiPromedio.SuspendLayout();
            this.layoutKpiPromedio.SuspendLayout();
            this.pnlKpiProveedor.SuspendLayout();
            this.layoutKpiProveedor.SuspendLayout();

            this.tarjetas.SuspendLayout();

            this.pnlTarjetaMeses.SuspendLayout();
            this.layoutMeses.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvMeses)).BeginInit();

            this.pnlTarjetaGraficaMeses.SuspendLayout();
            this.layoutGraficaMeses.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartMeses)).BeginInit();

            this.pnlTarjetaProveedores.SuspendLayout();
            this.layoutProveedores.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvProveedores)).BeginInit();

            this.pnlTarjetaClasificacion.SuspendLayout();
            this.layoutClasificacion.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartClasificacion)).BeginInit();

            this.pnlTarjetaComparativo.SuspendLayout();
            this.layoutComparativo.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartComparativo)).BeginInit();

            this.SuspendLayout();

            // =====================================================
            // principal
            // =====================================================

            this.principal.BackColor =
                System.Drawing.Color.Transparent;

            this.principal.ColumnCount = 1;

            this.principal.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.principal.Controls.Add(
                this.encabezado,
                0,
                0);

            this.principal.Controls.Add(
                this.pnlKpis,
                0,
                1);

            this.principal.Controls.Add(
                this.tarjetas,
                0,
                2);

            this.principal.Controls.Add(
                this.lblEstado,
                0,
                3);

            this.principal.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.principal.Location =
                new System.Drawing.Point(
                    24,
                    24);

            this.principal.Margin =
                new System.Windows.Forms.Padding(0);

            this.principal.Name =
                "principal";

            this.principal.RowCount = 4;

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    115F));

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    125F));

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    34F));

            this.principal.Size =
                new System.Drawing.Size(
                    1132,
                    952);

            this.principal.TabIndex = 0;

            // =====================================================
            // encabezado
            // =====================================================

            this.encabezado.BackColor =
                System.Drawing.Color.Transparent;

            this.encabezado.ColumnCount = 2;

            this.encabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    64F));

            this.encabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    36F));

            this.encabezado.Controls.Add(
                this.pnlCabeceraTitulo,
                0,
                0);

            this.encabezado.Controls.Add(
                this.pnlCabeceraFiltros,
                1,
                0);

            this.encabezado.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.encabezado.Location =
                new System.Drawing.Point(
                    0,
                    0);

            this.encabezado.Margin =
                new System.Windows.Forms.Padding(0);

            this.encabezado.Name =
                "encabezado";

            this.encabezado.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    20F));

            this.encabezado.Size =
                new System.Drawing.Size(
                    1132,
                    115);

            this.encabezado.TabIndex = 0;

            // =====================================================
            // pnlCabeceraTitulo
            // =====================================================

            this.pnlCabeceraTitulo.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlCabeceraTitulo.BorderRadius = 14;
            this.pnlCabeceraTitulo.BorderThickness = 1;

            this.pnlCabeceraTitulo.Controls.Add(
                this.layoutCabeceraTitulo);

            this.pnlCabeceraTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlCabeceraTitulo.FillColor =
                System.Drawing.Color.White;

            this.pnlCabeceraTitulo.Location =
                new System.Drawing.Point(
                    5,
                    5);

            this.pnlCabeceraTitulo.Margin =
                new System.Windows.Forms.Padding(
                    5,
                    5,
                    8,
                    10);

            this.pnlCabeceraTitulo.Name =
                "pnlCabeceraTitulo";

            this.pnlCabeceraTitulo.Padding =
                new System.Windows.Forms.Padding(
                    22,
                    15,
                    22,
                    15);

            this.pnlCabeceraTitulo.Size =
                new System.Drawing.Size(
                    711,
                    100);

            this.pnlCabeceraTitulo.TabIndex = 0;

            // =====================================================
            // layoutCabeceraTitulo
            // =====================================================

            this.layoutCabeceraTitulo.BackColor =
                System.Drawing.Color.White;

            this.layoutCabeceraTitulo.ColumnCount = 1;

            this.layoutCabeceraTitulo.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutCabeceraTitulo.Controls.Add(
                this.lblTitulo,
                0,
                0);

            this.layoutCabeceraTitulo.Controls.Add(
                this.lblSubtitulo,
                0,
                1);

            this.layoutCabeceraTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutCabeceraTitulo.Location =
                new System.Drawing.Point(
                    22,
                    15);

            this.layoutCabeceraTitulo.Name =
                "layoutCabeceraTitulo";

            this.layoutCabeceraTitulo.RowCount = 2;

            this.layoutCabeceraTitulo.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    62F));

            this.layoutCabeceraTitulo.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    38F));

            this.layoutCabeceraTitulo.Size =
                new System.Drawing.Size(
                    667,
                    70);

            this.layoutCabeceraTitulo.TabIndex = 0;

            // =====================================================
            // lblTitulo
            // =====================================================

            this.lblTitulo.AutoEllipsis = true;

            this.lblTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    21F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblTitulo.Location =
                new System.Drawing.Point(
                    3,
                    0);

            this.lblTitulo.Name =
                "lblTitulo";

            this.lblTitulo.Size =
                new System.Drawing.Size(
                    661,
                    43);

            this.lblTitulo.TabIndex = 0;

            this.lblTitulo.Text =
                "Dashboard de Compras";

            this.lblTitulo.TextAlign =
                System.Drawing.ContentAlignment.BottomLeft;

            // =====================================================
            // lblSubtitulo
            // =====================================================

            this.lblSubtitulo.AutoEllipsis = true;

            this.lblSubtitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblSubtitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.lblSubtitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    111,
                    132,
                    160);

            this.lblSubtitulo.Location =
                new System.Drawing.Point(
                    3,
                    43);

            this.lblSubtitulo.Name =
                "lblSubtitulo";

            this.lblSubtitulo.Size =
                new System.Drawing.Size(
                    661,
                    27);

            this.lblSubtitulo.TabIndex = 1;

            this.lblSubtitulo.Text =
                "Resumen general de compras";

            // =====================================================
            // pnlCabeceraFiltros
            // =====================================================

            this.pnlCabeceraFiltros.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlCabeceraFiltros.BorderRadius = 14;
            this.pnlCabeceraFiltros.BorderThickness = 1;

            this.pnlCabeceraFiltros.Controls.Add(
                this.layoutCabeceraFiltros);

            this.pnlCabeceraFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlCabeceraFiltros.FillColor =
                System.Drawing.Color.White;

            this.pnlCabeceraFiltros.Location =
                new System.Drawing.Point(
                    732,
                    5);

            this.pnlCabeceraFiltros.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    5,
                    5,
                    10);

            this.pnlCabeceraFiltros.Name =
                "pnlCabeceraFiltros";

            this.pnlCabeceraFiltros.Padding =
                new System.Windows.Forms.Padding(
                    18,
                    12,
                    18,
                    12);

            this.pnlCabeceraFiltros.Size =
                new System.Drawing.Size(
                    395,
                    100);

            this.pnlCabeceraFiltros.TabIndex = 1;

            // =====================================================
            // layoutCabeceraFiltros
            // =====================================================

            this.layoutCabeceraFiltros.BackColor =
                System.Drawing.Color.White;

            this.layoutCabeceraFiltros.ColumnCount = 1;

            this.layoutCabeceraFiltros.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutCabeceraFiltros.Controls.Add(
                this.lblTituloFiltros,
                0,
                0);

            this.layoutCabeceraFiltros.Controls.Add(
                this.pnlFiltros,
                0,
                1);

            this.layoutCabeceraFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutCabeceraFiltros.Location =
                new System.Drawing.Point(
                    18,
                    12);

            this.layoutCabeceraFiltros.Name =
                "layoutCabeceraFiltros";

            this.layoutCabeceraFiltros.RowCount = 2;

            this.layoutCabeceraFiltros.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    25F));

            this.layoutCabeceraFiltros.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutCabeceraFiltros.Size =
                new System.Drawing.Size(
                    359,
                    76);

            this.layoutCabeceraFiltros.TabIndex = 0;

            // =====================================================
            // lblTituloFiltros
            // =====================================================

            this.lblTituloFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloFiltros.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloFiltros.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblTituloFiltros.Location =
                new System.Drawing.Point(
                    3,
                    0);

            this.lblTituloFiltros.Name =
                "lblTituloFiltros";

            this.lblTituloFiltros.Size =
                new System.Drawing.Size(
                    353,
                    25);

            this.lblTituloFiltros.TabIndex = 0;

            this.lblTituloFiltros.Text =
                "FILTROS";

            this.lblTituloFiltros.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // pnlFiltros
            // =====================================================

            this.pnlFiltros.BackColor =
                System.Drawing.Color.White;

            this.pnlFiltros.Controls.Add(
                this.btnActualizar);

            this.pnlFiltros.Controls.Add(
                this.cmbAnio);

            this.pnlFiltros.Controls.Add(
                this.lblAnio);

            this.pnlFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlFiltros.FlowDirection =
                System.Windows.Forms.FlowDirection.RightToLeft;

            this.pnlFiltros.Location =
                new System.Drawing.Point(
                    3,
                    28);

            this.pnlFiltros.Name =
                "pnlFiltros";

            this.pnlFiltros.Padding =
                new System.Windows.Forms.Padding(
                    0,
                    4,
                    0,
                    0);

            this.pnlFiltros.Size =
                new System.Drawing.Size(
                    353,
                    45);

            this.pnlFiltros.TabIndex = 1;

            this.pnlFiltros.WrapContents = false;

            // =====================================================
            // btnActualizar
            // =====================================================

            this.btnActualizar.BorderRadius = 9;

            this.btnActualizar.FillColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.btnActualizar.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F);

            this.btnActualizar.ForeColor =
                System.Drawing.Color.White;

            this.btnActualizar.HoverState.FillColor =
                System.Drawing.Color.FromArgb(
                    35,
                    67,
                    112);

            this.btnActualizar.Location =
                new System.Drawing.Point(
                    233,
                    10);

            this.btnActualizar.Margin =
                new System.Windows.Forms.Padding(
                    14,
                    0,
                    0,
                    0);

            this.btnActualizar.Name =
                "btnActualizar";

            this.btnActualizar.Size =
                new System.Drawing.Size(
                    120,
                    38);

            this.btnActualizar.TabIndex = 0;

            this.btnActualizar.Text =
                "Actualizar";

            this.btnActualizar.Click +=
                new System.EventHandler(
                    this.btnActualizar_Click);

            // =====================================================
            // cmbAnio
            // =====================================================

            this.cmbAnio.BackColor =
                System.Drawing.Color.Transparent;

            this.cmbAnio.BorderColor =
                System.Drawing.Color.FromArgb(
                    210,
                    218,
                    228);

            this.cmbAnio.BorderRadius = 9;

            this.cmbAnio.DrawMode =
                System.Windows.Forms.DrawMode.OwnerDrawFixed;

            this.cmbAnio.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbAnio.FocusedColor =
                System.Drawing.Color.Empty;

            this.cmbAnio.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.cmbAnio.ForeColor =
                System.Drawing.Color.FromArgb(
                    50,
                    67,
                    89);

            this.cmbAnio.ItemHeight = 30;

            this.cmbAnio.Location =
                new System.Drawing.Point(
                    99,
                    10);

            this.cmbAnio.Margin =
                new System.Windows.Forms.Padding(0);

            this.cmbAnio.Name =
                "cmbAnio";

            this.cmbAnio.Size =
                new System.Drawing.Size(
                    120,
                    36);

            this.cmbAnio.TabIndex = 1;

            this.cmbAnio.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cmbAnio_SelectedIndexChanged);

            // =====================================================
            // lblAnio
            // =====================================================

            this.lblAnio.AutoSize = true;

            this.lblAnio.ForeColor =
                System.Drawing.Color.FromArgb(
                    50,
                    67,
                    89);

            this.lblAnio.Location =
                new System.Drawing.Point(
                    53,
                    19);

            this.lblAnio.Margin =
                new System.Windows.Forms.Padding(
                    0,
                    7,
                    12,
                    0);

            this.lblAnio.Name =
                "lblAnio";

            this.lblAnio.Size =
                new System.Drawing.Size(
                    34,
                    19);

            this.lblAnio.TabIndex = 2;

            this.lblAnio.Text =
                "Año";

            // =====================================================
            // pnlKpis
            // =====================================================

            this.pnlKpis.ColumnCount = 3;

            this.pnlKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.pnlKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.pnlKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33334F));

            this.pnlKpis.Controls.Add(
                this.pnlKpiCompras,
                0,
                0);

            this.pnlKpis.Controls.Add(
                this.pnlKpiPromedio,
                1,
                0);

            this.pnlKpis.Controls.Add(
                this.pnlKpiProveedor,
                2,
                0);

            this.pnlKpis.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpis.Location =
                new System.Drawing.Point(
                    0,
                    115);

            this.pnlKpis.Margin =
                new System.Windows.Forms.Padding(0);

            this.pnlKpis.Name =
                "pnlKpis";

            this.pnlKpis.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    20F));

            this.pnlKpis.Size =
                new System.Drawing.Size(
                    1132,
                    125);

            this.pnlKpis.TabIndex = 1;

            // =====================================================
            // pnlKpiCompras
            // =====================================================

            this.pnlKpiCompras.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlKpiCompras.BorderRadius = 14;
            this.pnlKpiCompras.BorderThickness = 1;

            this.pnlKpiCompras.Controls.Add(
                this.layoutKpiCompras);

            this.pnlKpiCompras.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiCompras.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiCompras.Location =
                new System.Drawing.Point(
                    5,
                    5);

            this.pnlKpiCompras.Margin =
                new System.Windows.Forms.Padding(
                    5,
                    5,
                    8,
                    10);

            this.pnlKpiCompras.Name =
                "pnlKpiCompras";

            this.pnlKpiCompras.Padding =
                new System.Windows.Forms.Padding(
                    20,
                    12,
                    20,
                    12);

            this.pnlKpiCompras.Size =
                new System.Drawing.Size(
                    364,
                    110);

            this.pnlKpiCompras.TabIndex = 0;

            // =====================================================
            // layoutKpiCompras
            // =====================================================

            this.layoutKpiCompras.BackColor =
                System.Drawing.Color.White;

            this.layoutKpiCompras.ColumnCount = 1;

            this.layoutKpiCompras.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiCompras.Controls.Add(
                this.lblKpiComprasTitulo,
                0,
                0);

            this.layoutKpiCompras.Controls.Add(
                this.lblKpiComprasValor,
                0,
                1);

            this.layoutKpiCompras.Controls.Add(
                this.lblKpiComprasDetalle,
                0,
                2);

            this.layoutKpiCompras.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutKpiCompras.Location =
                new System.Drawing.Point(
                    20,
                    12);

            this.layoutKpiCompras.Name =
                "layoutKpiCompras";

            this.layoutKpiCompras.RowCount = 3;

            this.layoutKpiCompras.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    24F));

            this.layoutKpiCompras.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiCompras.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    22F));

            this.layoutKpiCompras.Size =
                new System.Drawing.Size(
                    324,
                    86);

            this.layoutKpiCompras.TabIndex = 0;

            // =====================================================
            // lblKpiComprasTitulo
            // =====================================================

            this.lblKpiComprasTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiComprasTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiComprasTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    74,
                    96,
                    126);

            this.lblKpiComprasTitulo.Location =
                new System.Drawing.Point(
                    3,
                    0);

            this.lblKpiComprasTitulo.Name =
                "lblKpiComprasTitulo";

            this.lblKpiComprasTitulo.Size =
                new System.Drawing.Size(
                    318,
                    24);

            this.lblKpiComprasTitulo.TabIndex = 0;

            this.lblKpiComprasTitulo.Text =
                "COMPRAS DEL AÑO";

            // =====================================================
            // lblKpiComprasValor
            // =====================================================

            this.lblKpiComprasValor.AutoEllipsis = true;

            this.lblKpiComprasValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiComprasValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiComprasValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblKpiComprasValor.Location =
                new System.Drawing.Point(
                    3,
                    24);

            this.lblKpiComprasValor.Name =
                "lblKpiComprasValor";

            this.lblKpiComprasValor.Size =
                new System.Drawing.Size(
                    318,
                    40);

            this.lblKpiComprasValor.TabIndex = 1;

            this.lblKpiComprasValor.Text =
                "—";

            this.lblKpiComprasValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // lblKpiComprasDetalle
            // =====================================================

            this.lblKpiComprasDetalle.AutoEllipsis = true;

            this.lblKpiComprasDetalle.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiComprasDetalle.ForeColor =
                System.Drawing.Color.FromArgb(
                    111,
                    132,
                    160);

            this.lblKpiComprasDetalle.Location =
                new System.Drawing.Point(
                    3,
                    64);

            this.lblKpiComprasDetalle.Name =
                "lblKpiComprasDetalle";

            this.lblKpiComprasDetalle.Size =
                new System.Drawing.Size(
                    318,
                    22);

            this.lblKpiComprasDetalle.TabIndex = 2;

            this.lblKpiComprasDetalle.Text =
                "—";

            // =====================================================
            // pnlKpiPromedio
            // =====================================================

            this.pnlKpiPromedio.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlKpiPromedio.BorderRadius = 14;
            this.pnlKpiPromedio.BorderThickness = 1;

            this.pnlKpiPromedio.Controls.Add(
                this.layoutKpiPromedio);

            this.pnlKpiPromedio.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiPromedio.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiPromedio.Location =
                new System.Drawing.Point(
                    385,
                    5);

            this.pnlKpiPromedio.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    5,
                    8,
                    10);

            this.pnlKpiPromedio.Name =
                "pnlKpiPromedio";

            this.pnlKpiPromedio.Padding =
                new System.Windows.Forms.Padding(
                    20,
                    12,
                    20,
                    12);

            this.pnlKpiPromedio.Size =
                new System.Drawing.Size(
                    361,
                    110);

            this.pnlKpiPromedio.TabIndex = 1;

            // =====================================================
            // layoutKpiPromedio
            // =====================================================

            this.layoutKpiPromedio.BackColor =
                System.Drawing.Color.White;

            this.layoutKpiPromedio.ColumnCount = 1;

            this.layoutKpiPromedio.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiPromedio.Controls.Add(
                this.lblKpiPromedioTitulo,
                0,
                0);

            this.layoutKpiPromedio.Controls.Add(
                this.lblKpiPromedioValor,
                0,
                1);

            this.layoutKpiPromedio.Controls.Add(
                this.lblKpiPromedioDetalle,
                0,
                2);

            this.layoutKpiPromedio.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutKpiPromedio.Location =
                new System.Drawing.Point(
                    20,
                    12);

            this.layoutKpiPromedio.Name =
                "layoutKpiPromedio";

            this.layoutKpiPromedio.RowCount = 3;

            this.layoutKpiPromedio.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    24F));

            this.layoutKpiPromedio.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiPromedio.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    22F));

            this.layoutKpiPromedio.Size =
                new System.Drawing.Size(
                    321,
                    86);

            this.layoutKpiPromedio.TabIndex = 0;

            // =====================================================
            // lblKpiPromedioTitulo
            // =====================================================

            this.lblKpiPromedioTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiPromedioTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiPromedioTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    74,
                    96,
                    126);

            this.lblKpiPromedioTitulo.Location =
                new System.Drawing.Point(
                    3,
                    0);

            this.lblKpiPromedioTitulo.Name =
                "lblKpiPromedioTitulo";

            this.lblKpiPromedioTitulo.Size =
                new System.Drawing.Size(
                    315,
                    24);

            this.lblKpiPromedioTitulo.TabIndex = 0;

            this.lblKpiPromedioTitulo.Text =
                "PROMEDIO MENSUAL";

            // =====================================================
            // lblKpiPromedioValor
            // =====================================================

            this.lblKpiPromedioValor.AutoEllipsis = true;

            this.lblKpiPromedioValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiPromedioValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiPromedioValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblKpiPromedioValor.Location =
                new System.Drawing.Point(
                    3,
                    24);

            this.lblKpiPromedioValor.Name =
                "lblKpiPromedioValor";

            this.lblKpiPromedioValor.Size =
                new System.Drawing.Size(
                    315,
                    40);

            this.lblKpiPromedioValor.TabIndex = 1;

            this.lblKpiPromedioValor.Text =
                "—";

            this.lblKpiPromedioValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // lblKpiPromedioDetalle
            // =====================================================

            this.lblKpiPromedioDetalle.AutoEllipsis = true;

            this.lblKpiPromedioDetalle.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiPromedioDetalle.ForeColor =
                System.Drawing.Color.FromArgb(
                    111,
                    132,
                    160);

            this.lblKpiPromedioDetalle.Location =
                new System.Drawing.Point(
                    3,
                    64);

            this.lblKpiPromedioDetalle.Name =
                "lblKpiPromedioDetalle";

            this.lblKpiPromedioDetalle.Size =
                new System.Drawing.Size(
                    315,
                    22);

            this.lblKpiPromedioDetalle.TabIndex = 2;

            this.lblKpiPromedioDetalle.Text =
                "—";

            // =====================================================
            // pnlKpiProveedor
            // =====================================================

            this.pnlKpiProveedor.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlKpiProveedor.BorderRadius = 14;
            this.pnlKpiProveedor.BorderThickness = 1;

            this.pnlKpiProveedor.Controls.Add(
                this.layoutKpiProveedor);

            this.pnlKpiProveedor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiProveedor.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiProveedor.Location =
                new System.Drawing.Point(
                    762,
                    5);

            this.pnlKpiProveedor.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    5,
                    5,
                    10);

            this.pnlKpiProveedor.Name =
                "pnlKpiProveedor";

            this.pnlKpiProveedor.Padding =
                new System.Windows.Forms.Padding(
                    20,
                    12,
                    20,
                    12);

            this.pnlKpiProveedor.Size =
                new System.Drawing.Size(
                    365,
                    110);

            this.pnlKpiProveedor.TabIndex = 2;

            // =====================================================
            // layoutKpiProveedor
            // =====================================================

            this.layoutKpiProveedor.BackColor =
                System.Drawing.Color.White;

            this.layoutKpiProveedor.ColumnCount = 1;

            this.layoutKpiProveedor.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiProveedor.Controls.Add(
                this.lblKpiProveedorTitulo,
                0,
                0);

            this.layoutKpiProveedor.Controls.Add(
                this.lblKpiProveedorValor,
                0,
                1);

            this.layoutKpiProveedor.Controls.Add(
                this.lblKpiProveedorDetalle,
                0,
                2);

            this.layoutKpiProveedor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutKpiProveedor.Location =
                new System.Drawing.Point(
                    20,
                    12);

            this.layoutKpiProveedor.Name =
                "layoutKpiProveedor";

            this.layoutKpiProveedor.RowCount = 3;

            this.layoutKpiProveedor.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    24F));

            this.layoutKpiProveedor.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutKpiProveedor.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    22F));

            this.layoutKpiProveedor.Size =
                new System.Drawing.Size(
                    325,
                    86);

            this.layoutKpiProveedor.TabIndex = 0;

            // =====================================================
            // lblKpiProveedorTitulo
            // =====================================================

            this.lblKpiProveedorTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiProveedorTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiProveedorTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    74,
                    96,
                    126);

            this.lblKpiProveedorTitulo.Location =
                new System.Drawing.Point(
                    3,
                    0);

            this.lblKpiProveedorTitulo.Name =
                "lblKpiProveedorTitulo";

            this.lblKpiProveedorTitulo.Size =
                new System.Drawing.Size(
                    319,
                    24);

            this.lblKpiProveedorTitulo.TabIndex = 0;

            this.lblKpiProveedorTitulo.Text =
                "PRINCIPAL PROVEEDOR";

            // =====================================================
            // lblKpiProveedorValor
            // =====================================================

            this.lblKpiProveedorValor.AutoEllipsis = true;

            this.lblKpiProveedorValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiProveedorValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    14F,
                    System.Drawing.FontStyle.Bold);

            this.lblKpiProveedorValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblKpiProveedorValor.Location =
                new System.Drawing.Point(
                    3,
                    24);

            this.lblKpiProveedorValor.Name =
                "lblKpiProveedorValor";

            this.lblKpiProveedorValor.Size =
                new System.Drawing.Size(
                    319,
                    40);

            this.lblKpiProveedorValor.TabIndex = 1;

            this.lblKpiProveedorValor.Text =
                "—";

            this.lblKpiProveedorValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // lblKpiProveedorDetalle
            // =====================================================

            this.lblKpiProveedorDetalle.AutoEllipsis = true;

            this.lblKpiProveedorDetalle.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblKpiProveedorDetalle.ForeColor =
                System.Drawing.Color.FromArgb(
                    111,
                    132,
                    160);

            this.lblKpiProveedorDetalle.Location =
                new System.Drawing.Point(
                    3,
                    64);

            this.lblKpiProveedorDetalle.Name =
                "lblKpiProveedorDetalle";

            this.lblKpiProveedorDetalle.Size =
                new System.Drawing.Size(
                    319,
                    22);

            this.lblKpiProveedorDetalle.TabIndex = 2;

            this.lblKpiProveedorDetalle.Text =
                "—";

            // =====================================================
            // tarjetas
            // =====================================================

            this.tarjetas.BackColor =
                System.Drawing.Color.Transparent;

            this.tarjetas.ColumnCount = 2;

            this.tarjetas.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    36F));

            this.tarjetas.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    64F));

            this.tarjetas.Controls.Add(
                this.pnlTarjetaMeses,
                0,
                0);

            this.tarjetas.Controls.Add(
                this.pnlTarjetaGraficaMeses,
                1,
                0);

            this.tarjetas.Controls.Add(
                this.pnlTarjetaProveedores,
                0,
                1);

            this.tarjetas.Controls.Add(
                this.pnlTarjetaClasificacion,
                1,
                1);

            this.tarjetas.Controls.Add(
                this.pnlTarjetaComparativo,
                0,
                2);

            this.tarjetas.SetColumnSpan(
                this.pnlTarjetaComparativo,
                2);

            this.tarjetas.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tarjetas.Location =
                new System.Drawing.Point(
                    0,
                    240);

            this.tarjetas.Margin =
                new System.Windows.Forms.Padding(0);

            this.tarjetas.Name =
                "tarjetas";

            this.tarjetas.RowCount = 3;

            this.tarjetas.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tarjetas.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tarjetas.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33334F));

            this.tarjetas.Size =
                new System.Drawing.Size(
                    1132,
                    678);

            this.tarjetas.TabIndex = 2;

            // =====================================================
            // pnlTarjetaMeses
            // =====================================================

            this.pnlTarjetaMeses.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaMeses.BorderRadius = 14;
            this.pnlTarjetaMeses.BorderThickness = 1;

            this.pnlTarjetaMeses.Controls.Add(
                this.layoutMeses);

            this.pnlTarjetaMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaMeses.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaMeses.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaMeses.Name =
                "pnlTarjetaMeses";

            this.pnlTarjetaMeses.Padding =
                new System.Windows.Forms.Padding(16);

            this.pnlTarjetaMeses.TabIndex = 0;

            // =====================================================
            // layoutMeses
            // =====================================================

            this.layoutMeses.BackColor =
                System.Drawing.Color.White;

            this.layoutMeses.ColumnCount = 1;

            this.layoutMeses.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutMeses.Controls.Add(
                this.lblTituloMeses,
                0,
                0);

            this.layoutMeses.Controls.Add(
                this.dgvMeses,
                0,
                1);

            this.layoutMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutMeses.Name =
                "layoutMeses";

            this.layoutMeses.RowCount = 2;

            this.layoutMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    36F));

            this.layoutMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutMeses.TabIndex = 0;

            // =====================================================
            // lblTituloMeses
            // =====================================================

            this.lblTituloMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloMeses.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloMeses.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloMeses.Name =
                "lblTituloMeses";

            this.lblTituloMeses.TabIndex = 0;

            this.lblTituloMeses.Text =
                "Compras mensuales";

            this.lblTituloMeses.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // dgvMeses
            // =====================================================

            this.dgvMeses.AllowUserToAddRows = false;
            this.dgvMeses.AllowUserToDeleteRows = false;
            this.dgvMeses.AllowUserToResizeRows = false;

            dataGridViewCellStyle1.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            dataGridViewCellStyle1.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            dataGridViewCellStyle1.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            dataGridViewCellStyle1.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            dataGridViewCellStyle1.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvMeses.ColumnHeadersDefaultCellStyle =
                dataGridViewCellStyle1;

            this.dgvMeses.ColumnHeadersHeight = 30;

            this.dgvMeses.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridViewCellStyle2.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dataGridViewCellStyle2.BackColor =
                System.Drawing.Color.White;

            dataGridViewCellStyle2.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            dataGridViewCellStyle2.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            dataGridViewCellStyle2.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    231,
                    239,
                    249);

            dataGridViewCellStyle2.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            dataGridViewCellStyle2.WrapMode =
                System.Windows.Forms.DataGridViewTriState.False;

            this.dgvMeses.DefaultCellStyle =
                dataGridViewCellStyle2;

            this.dgvMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvMeses.GridColor =
                System.Drawing.Color.FromArgb(
                    231,
                    229,
                    255);

            this.dgvMeses.MultiSelect = false;

            this.dgvMeses.Name =
                "dgvMeses";

            this.dgvMeses.ReadOnly = true;
            this.dgvMeses.RowHeadersVisible = false;
            this.dgvMeses.RowTemplate.Height = 26;

            this.dgvMeses.TabIndex = 1;

            this.dgvMeses.ThemeStyle.HeaderStyle.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            this.dgvMeses.ThemeStyle.HeaderStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.dgvMeses.ThemeStyle.HeaderStyle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvMeses.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvMeses.ThemeStyle.ReadOnly = true;

            this.dgvMeses.ThemeStyle.RowsStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.dgvMeses.ThemeStyle.RowsStyle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvMeses.ThemeStyle.RowsStyle.Height = 26;

            this.dgvMeses.ThemeStyle.RowsStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    231,
                    239,
                    249);

            this.dgvMeses.ThemeStyle.RowsStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            // =====================================================
            // pnlTarjetaGraficaMeses
            // =====================================================

            this.pnlTarjetaGraficaMeses.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaGraficaMeses.BorderRadius = 14;
            this.pnlTarjetaGraficaMeses.BorderThickness = 1;

            this.pnlTarjetaGraficaMeses.Controls.Add(
                this.layoutGraficaMeses);

            this.pnlTarjetaGraficaMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaGraficaMeses.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaGraficaMeses.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaGraficaMeses.Name =
                "pnlTarjetaGraficaMeses";

            this.pnlTarjetaGraficaMeses.Padding =
                new System.Windows.Forms.Padding(16);

            this.pnlTarjetaGraficaMeses.TabIndex = 1;

            // =====================================================
            // layoutGraficaMeses
            // =====================================================

            this.layoutGraficaMeses.BackColor =
                System.Drawing.Color.White;

            this.layoutGraficaMeses.ColumnCount = 1;

            this.layoutGraficaMeses.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutGraficaMeses.Controls.Add(
                this.lblTituloGraficaMeses,
                0,
                0);

            this.layoutGraficaMeses.Controls.Add(
                this.chartMeses,
                0,
                1);

            this.layoutGraficaMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutGraficaMeses.Name =
                "layoutGraficaMeses";

            this.layoutGraficaMeses.RowCount = 2;

            this.layoutGraficaMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    36F));

            this.layoutGraficaMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutGraficaMeses.TabIndex = 0;

            // =====================================================
            // lblTituloGraficaMeses
            // =====================================================

            this.lblTituloGraficaMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloGraficaMeses.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloGraficaMeses.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloGraficaMeses.Name =
                "lblTituloGraficaMeses";

            this.lblTituloGraficaMeses.TabIndex = 0;

            this.lblTituloGraficaMeses.Text =
                "Evolución mensual";

            this.lblTituloGraficaMeses.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // chartMeses
            // =====================================================

            chartArea1.Name =
                "Compras";

            this.chartMeses.ChartAreas.Add(
                chartArea1);

            this.chartMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.chartMeses.Name =
                "chartMeses";

            series1.ChartArea =
                "Compras";

            series1.IsVisibleInLegend =
                false;

            series1.Name =
                "Compras";

            this.chartMeses.Series.Add(
                series1);

            this.chartMeses.TabIndex = 1;

            // =====================================================
            // pnlTarjetaProveedores
            // =====================================================

            this.pnlTarjetaProveedores.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaProveedores.BorderRadius = 14;
            this.pnlTarjetaProveedores.BorderThickness = 1;

            this.pnlTarjetaProveedores.Controls.Add(
                this.layoutProveedores);

            this.pnlTarjetaProveedores.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaProveedores.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaProveedores.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaProveedores.Name =
                "pnlTarjetaProveedores";

            this.pnlTarjetaProveedores.Padding =
                new System.Windows.Forms.Padding(16);

            this.pnlTarjetaProveedores.TabIndex = 2;

            // =====================================================
            // layoutProveedores
            // =====================================================

            this.layoutProveedores.BackColor =
                System.Drawing.Color.White;

            this.layoutProveedores.ColumnCount = 1;

            this.layoutProveedores.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutProveedores.Controls.Add(
                this.lblTituloProveedores,
                0,
                0);

            this.layoutProveedores.Controls.Add(
                this.dgvProveedores,
                0,
                1);

            this.layoutProveedores.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutProveedores.Name =
                "layoutProveedores";

            this.layoutProveedores.RowCount = 2;

            this.layoutProveedores.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    36F));

            this.layoutProveedores.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutProveedores.TabIndex = 0;

            // =====================================================
            // lblTituloProveedores
            // =====================================================

            this.lblTituloProveedores.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloProveedores.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloProveedores.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloProveedores.Name =
                "lblTituloProveedores";

            this.lblTituloProveedores.TabIndex = 0;

            this.lblTituloProveedores.Text =
                "Principales proveedores";

            this.lblTituloProveedores.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // dgvProveedores
            // =====================================================

            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AllowUserToResizeRows = false;

            dataGridViewCellStyle3.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            dataGridViewCellStyle3.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            dataGridViewCellStyle3.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            dataGridViewCellStyle3.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            dataGridViewCellStyle3.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvProveedores.ColumnHeadersDefaultCellStyle =
                dataGridViewCellStyle3;

            this.dgvProveedores.ColumnHeadersHeight = 30;

            this.dgvProveedores.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridViewCellStyle4.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            dataGridViewCellStyle4.BackColor =
                System.Drawing.Color.White;

            dataGridViewCellStyle4.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            dataGridViewCellStyle4.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            dataGridViewCellStyle4.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    231,
                    239,
                    249);

            dataGridViewCellStyle4.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            dataGridViewCellStyle4.WrapMode =
                System.Windows.Forms.DataGridViewTriState.False;

            this.dgvProveedores.DefaultCellStyle =
                dataGridViewCellStyle4;

            this.dgvProveedores.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvProveedores.GridColor =
                System.Drawing.Color.FromArgb(
                    231,
                    229,
                    255);

            this.dgvProveedores.MultiSelect = false;

            this.dgvProveedores.Name =
                "dgvProveedores";

            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.RowHeadersVisible = false;
            this.dgvProveedores.RowTemplate.Height = 26;

            this.dgvProveedores.TabIndex = 1;

            this.dgvProveedores.ThemeStyle.HeaderStyle.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            this.dgvProveedores.ThemeStyle.HeaderStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.dgvProveedores.ThemeStyle.HeaderStyle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvProveedores.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvProveedores.ThemeStyle.ReadOnly = true;

            this.dgvProveedores.ThemeStyle.RowsStyle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.dgvProveedores.ThemeStyle.RowsStyle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            this.dgvProveedores.ThemeStyle.RowsStyle.Height = 26;

            this.dgvProveedores.ThemeStyle.RowsStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    231,
                    239,
                    249);

            this.dgvProveedores.ThemeStyle.RowsStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            // =====================================================
            // pnlTarjetaClasificacion
            // =====================================================

            this.pnlTarjetaClasificacion.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaClasificacion.BorderRadius = 14;
            this.pnlTarjetaClasificacion.BorderThickness = 1;

            this.pnlTarjetaClasificacion.Controls.Add(
                this.layoutClasificacion);

            this.pnlTarjetaClasificacion.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaClasificacion.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaClasificacion.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaClasificacion.Name =
                "pnlTarjetaClasificacion";

            this.pnlTarjetaClasificacion.Padding =
                new System.Windows.Forms.Padding(16);

            this.pnlTarjetaClasificacion.TabIndex = 3;

            // =====================================================
            // layoutClasificacion
            // =====================================================

            this.layoutClasificacion.BackColor =
                System.Drawing.Color.White;

            this.layoutClasificacion.ColumnCount = 1;

            this.layoutClasificacion.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutClasificacion.Controls.Add(
                this.lblTituloClasificacion,
                0,
                0);

            this.layoutClasificacion.Controls.Add(
                this.chartClasificacion,
                0,
                1);

            this.layoutClasificacion.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutClasificacion.Name =
                "layoutClasificacion";

            this.layoutClasificacion.RowCount = 2;

            this.layoutClasificacion.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    36F));

            this.layoutClasificacion.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutClasificacion.TabIndex = 0;

            // =====================================================
            // lblTituloClasificacion
            // =====================================================

            this.lblTituloClasificacion.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloClasificacion.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloClasificacion.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloClasificacion.Name =
                "lblTituloClasificacion";

            this.lblTituloClasificacion.TabIndex = 0;

            this.lblTituloClasificacion.Text =
                "Compras por clasificación";

            this.lblTituloClasificacion.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // chartClasificacion
            // =====================================================

            chartArea2.Name =
                "Clasificacion";

            this.chartClasificacion.ChartAreas.Add(
                chartArea2);

            this.chartClasificacion.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.chartClasificacion.Name =
                "chartClasificacion";

            series2.ChartArea =
                "Clasificacion";

            series2.IsVisibleInLegend =
                false;

            series2.Name =
                "Clasificacion";

            this.chartClasificacion.Series.Add(
                series2);

            this.chartClasificacion.TabIndex = 1;

            // =====================================================
            // pnlTarjetaComparativo
            // =====================================================

            this.pnlTarjetaComparativo.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaComparativo.BorderRadius = 14;
            this.pnlTarjetaComparativo.BorderThickness = 1;

            this.pnlTarjetaComparativo.Controls.Add(
                this.layoutComparativo);

            this.pnlTarjetaComparativo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaComparativo.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaComparativo.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaComparativo.Name =
                "pnlTarjetaComparativo";

            this.pnlTarjetaComparativo.Padding =
                new System.Windows.Forms.Padding(16);

            this.pnlTarjetaComparativo.TabIndex = 4;

            // =====================================================
            // layoutComparativo
            // =====================================================

            this.layoutComparativo.BackColor =
                System.Drawing.Color.White;

            this.layoutComparativo.ColumnCount = 1;

            this.layoutComparativo.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutComparativo.Controls.Add(
                this.lblTituloComparativo,
                0,
                0);

            this.layoutComparativo.Controls.Add(
                this.chartComparativo,
                0,
                1);

            this.layoutComparativo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutComparativo.Name =
                "layoutComparativo";

            this.layoutComparativo.RowCount = 2;

            this.layoutComparativo.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    36F));

            this.layoutComparativo.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutComparativo.TabIndex = 0;

            // =====================================================
            // lblTituloComparativo
            // =====================================================

            this.lblTituloComparativo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloComparativo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloComparativo.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloComparativo.Name =
                "lblTituloComparativo";

            this.lblTituloComparativo.TabIndex = 0;

            this.lblTituloComparativo.Text =
                "Comparativo anual";

            this.lblTituloComparativo.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // chartComparativo
            // =====================================================

            chartArea3.Name =
                "Compras";

            this.chartComparativo.ChartAreas.Add(
                chartArea3);

            this.chartComparativo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.chartComparativo.Name =
                "chartComparativo";

            series3.ChartArea =
                "Compras";

            series3.IsVisibleInLegend =
                false;

            series3.Name =
                "Compras";

            this.chartComparativo.Series.Add(
                series3);

            this.chartComparativo.TabIndex = 1;

            // =====================================================
            // lblEstado
            // =====================================================

            this.lblEstado.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblEstado.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblEstado.Margin =
                new System.Windows.Forms.Padding(
                    5,
                    0,
                    5,
                    0);

            this.lblEstado.Name =
                "lblEstado";

            this.lblEstado.TabIndex = 3;

            this.lblEstado.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // =====================================================
            // DashboardCompras
            // =====================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(
                    96F,
                    96F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Dpi;

            this.BackColor =
                System.Drawing.Color.FromArgb(
                    244,
                    247,
                    251);

            this.ClientSize =
                new System.Drawing.Size(
                    1180,
                    1000);

            this.Controls.Add(
                this.principal);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.MinimumSize =
                new System.Drawing.Size(
                    1080,
                    900);

            this.Name =
                "DashboardCompras";

            this.Padding =
                new System.Windows.Forms.Padding(24);

            this.ShowInTaskbar = false;

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Dashboard de Compras";

            this.FormClosed +=
                new System.Windows.Forms.FormClosedEventHandler(
                    this.DashboardCompras_FormClosed);

            this.Shown +=
                new System.EventHandler(
                    this.DashboardCompras_Shown);

            // =====================================================
            // RESUME
            // =====================================================

            this.principal.ResumeLayout(false);

            this.encabezado.ResumeLayout(false);

            this.pnlCabeceraTitulo.ResumeLayout(false);
            this.layoutCabeceraTitulo.ResumeLayout(false);

            this.pnlCabeceraFiltros.ResumeLayout(false);
            this.layoutCabeceraFiltros.ResumeLayout(false);

            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();

            this.pnlKpis.ResumeLayout(false);

            this.pnlKpiCompras.ResumeLayout(false);
            this.layoutKpiCompras.ResumeLayout(false);

            this.pnlKpiPromedio.ResumeLayout(false);
            this.layoutKpiPromedio.ResumeLayout(false);

            this.pnlKpiProveedor.ResumeLayout(false);
            this.layoutKpiProveedor.ResumeLayout(false);

            this.tarjetas.ResumeLayout(false);

            this.pnlTarjetaMeses.ResumeLayout(false);
            this.layoutMeses.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvMeses)).EndInit();

            this.pnlTarjetaGraficaMeses.ResumeLayout(false);
            this.layoutGraficaMeses.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.chartMeses)).EndInit();

            this.pnlTarjetaProveedores.ResumeLayout(false);
            this.layoutProveedores.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvProveedores)).EndInit();

            this.pnlTarjetaClasificacion.ResumeLayout(false);
            this.layoutClasificacion.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.chartClasificacion)).EndInit();

            this.pnlTarjetaComparativo.ResumeLayout(false);
            this.layoutComparativo.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.chartComparativo)).EndInit();

            this.ResumeLayout(false);
        }
    }
}