namespace PV
{
    partial class DashboardIngresos
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.TableLayoutPanel tlpEncabezado;
        private System.Windows.Forms.TableLayoutPanel tlpFiltros;
        private System.Windows.Forms.TableLayoutPanel tlpKpis;
        private System.Windows.Forms.TableLayoutPanel tlpContenido;

        private Guna.UI2.WinForms.Guna2Panel pnlTitulo;
        private Guna.UI2.WinForms.Guna2Panel pnlFiltros;

        private Guna.UI2.WinForms.Guna2Panel pnlKpiIngresos;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiPromedio;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiCartera;

        private Guna.UI2.WinForms.Guna2Panel pnlMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlGraficaMensual;
        private Guna.UI2.WinForms.Guna2Panel pnlClientes;
        private Guna.UI2.WinForms.Guna2Panel pnlCuentas;
        private Guna.UI2.WinForms.Guna2Panel pnlAntiguedad;
        private Guna.UI2.WinForms.Guna2Panel pnlGraficaAntiguedad;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblAnio;

        private Guna.UI2.WinForms.Guna2ComboBox cmbAnio;
        private Guna.UI2.WinForms.Guna2Button btnActualizar;

        private System.Windows.Forms.Label lblTituloIngresos;
        private System.Windows.Forms.Label lblValorIngresos;

        private System.Windows.Forms.Label lblTituloPromedio;
        private System.Windows.Forms.Label lblValorPromedio;

        private System.Windows.Forms.Label lblTituloCartera;
        private System.Windows.Forms.Label lblValorCartera;

        private System.Windows.Forms.Label lblTituloMeses;
        private System.Windows.Forms.Label lblTituloGraficaMensual;
        private System.Windows.Forms.Label lblTituloClientes;
        private System.Windows.Forms.Label lblTituloCuentas;
        private System.Windows.Forms.Label lblTituloAntiguedad;
        private System.Windows.Forms.Label lblTituloGraficaAntiguedad;

        private Guna.UI2.WinForms.Guna2DataGridView dgvMeses;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClientes;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCuentas;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAntiguedad;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMensual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAntiguedad;

        private System.Windows.Forms.Label lblEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataGridViewCellStyle headerStyle1 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle rowStyle1 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle headerStyle2 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle rowStyle2 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle headerStyle3 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle rowStyle3 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle headerStyle4 =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle rowStyle4 =
                new System.Windows.Forms.DataGridViewCellStyle();

            // =====================================================
            // CREAR CONTROLES
            // =====================================================

            this.tlpPrincipal =
                new System.Windows.Forms.TableLayoutPanel();

            this.tlpEncabezado =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlTitulo =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTitulo =
                new System.Windows.Forms.Label();

            this.lblSubtitulo =
                new System.Windows.Forms.Label();

            this.pnlFiltros =
                new Guna.UI2.WinForms.Guna2Panel();

            this.tlpFiltros =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblAnio =
                new System.Windows.Forms.Label();

            this.cmbAnio =
                new Guna.UI2.WinForms.Guna2ComboBox();

            this.btnActualizar =
                new Guna.UI2.WinForms.Guna2Button();

            this.tlpKpis =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlKpiIngresos =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloIngresos =
                new System.Windows.Forms.Label();

            this.lblValorIngresos =
                new System.Windows.Forms.Label();

            this.pnlKpiPromedio =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloPromedio =
                new System.Windows.Forms.Label();

            this.lblValorPromedio =
                new System.Windows.Forms.Label();

            this.pnlKpiCartera =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloCartera =
                new System.Windows.Forms.Label();

            this.lblValorCartera =
                new System.Windows.Forms.Label();

            this.tlpContenido =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlMeses =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloMeses =
                new System.Windows.Forms.Label();

            this.dgvMeses =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlGraficaMensual =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloGraficaMensual =
                new System.Windows.Forms.Label();

            this.chartMensual =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlClientes =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloClientes =
                new System.Windows.Forms.Label();

            this.dgvClientes =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlCuentas =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloCuentas =
                new System.Windows.Forms.Label();

            this.dgvCuentas =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlAntiguedad =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloAntiguedad =
                new System.Windows.Forms.Label();

            this.dgvAntiguedad =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlGraficaAntiguedad =
                new Guna.UI2.WinForms.Guna2Panel();

            this.lblTituloGraficaAntiguedad =
                new System.Windows.Forms.Label();

            this.chartAntiguedad =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.lblEstado =
                new System.Windows.Forms.Label();

            // =====================================================
            // SUSPENDER LAYOUT
            // =====================================================

            this.tlpPrincipal.SuspendLayout();
            this.tlpEncabezado.SuspendLayout();
            this.pnlTitulo.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.tlpFiltros.SuspendLayout();

            this.tlpKpis.SuspendLayout();
            this.pnlKpiIngresos.SuspendLayout();
            this.pnlKpiPromedio.SuspendLayout();
            this.pnlKpiCartera.SuspendLayout();

            this.tlpContenido.SuspendLayout();

            this.pnlMeses.SuspendLayout();
            this.pnlGraficaMensual.SuspendLayout();
            this.pnlClientes.SuspendLayout();
            this.pnlCuentas.SuspendLayout();
            this.pnlAntiguedad.SuspendLayout();
            this.pnlGraficaAntiguedad.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvMeses)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartMensual)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvClientes)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvCuentas)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvAntiguedad)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartAntiguedad)).BeginInit();

            this.SuspendLayout();

            // =====================================================
            // tlpPrincipal
            // =====================================================

            this.tlpPrincipal.BackColor =
                System.Drawing.Color.FromArgb(244, 247, 251);

            this.tlpPrincipal.ColumnCount = 1;

            this.tlpPrincipal.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.tlpPrincipal.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tlpPrincipal.Location =
                new System.Drawing.Point(0, 0);

            this.tlpPrincipal.Name =
                "tlpPrincipal";

            this.tlpPrincipal.Padding =
                new System.Windows.Forms.Padding(24);

            this.tlpPrincipal.RowCount = 4;

            this.tlpPrincipal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    82F));

            this.tlpPrincipal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    118F));

            this.tlpPrincipal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.tlpPrincipal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    28F));

            this.tlpPrincipal.Size =
                new System.Drawing.Size(1180, 900);

            this.tlpPrincipal.TabIndex = 0;

            // =====================================================
            // tlpEncabezado
            // =====================================================

            this.tlpEncabezado.ColumnCount = 2;

            this.tlpEncabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.tlpEncabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    350F));

            this.tlpEncabezado.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tlpEncabezado.Margin =
                new System.Windows.Forms.Padding(0, 0, 0, 12);

            this.tlpEncabezado.MinimumSize =
                new System.Drawing.Size(100, 60);

            this.tlpEncabezado.Name =
                "tlpEncabezado";

            this.tlpEncabezado.RowCount = 1;

            this.tlpEncabezado.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // =====================================================
            // pnlTitulo
            // =====================================================

            this.pnlTitulo.BackColor =
                System.Drawing.Color.Transparent;

            this.pnlTitulo.BorderRadius = 14;

            this.pnlTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTitulo.FillColor =
                System.Drawing.Color.White;

            this.pnlTitulo.Margin =
                new System.Windows.Forms.Padding(0, 0, 8, 0);

            this.pnlTitulo.Name =
                "pnlTitulo";

            // lblTitulo

            this.lblTitulo.AutoSize = true;

            this.lblTitulo.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitulo.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTitulo.Location =
                new System.Drawing.Point(22, 10);

            this.lblTitulo.Name =
                "lblTitulo";

            this.lblTitulo.Text =
                "Dashboard de Ingresos";

            // lblSubtitulo

            this.lblSubtitulo.AutoSize = true;

            this.lblSubtitulo.BackColor =
                System.Drawing.Color.Transparent;

            this.lblSubtitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblSubtitulo.ForeColor =
                System.Drawing.Color.FromArgb(105, 117, 134);

            this.lblSubtitulo.Location =
                new System.Drawing.Point(24, 49);

            this.lblSubtitulo.Name =
                "lblSubtitulo";

            this.lblSubtitulo.Text =
                "Ingresos cobrados, cartera de clientes y antigüedad de saldos";

            this.pnlTitulo.Controls.Add(
                this.lblTitulo);

            this.pnlTitulo.Controls.Add(
                this.lblSubtitulo);

            // =====================================================
            // pnlFiltros
            // =====================================================

            this.pnlFiltros.BackColor =
                System.Drawing.Color.Transparent;

            this.pnlFiltros.BorderRadius = 14;

            this.pnlFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlFiltros.FillColor =
                System.Drawing.Color.White;

            this.pnlFiltros.Margin =
                new System.Windows.Forms.Padding(8, 0, 0, 0);

            this.pnlFiltros.Name =
                "pnlFiltros";

            this.pnlFiltros.Padding =
                new System.Windows.Forms.Padding(12, 4, 12, 4);

            // tlpFiltros

            this.tlpFiltros.BackColor =
                System.Drawing.Color.Transparent;

            this.tlpFiltros.ColumnCount = 3;

            this.tlpFiltros.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    42F));

            this.tlpFiltros.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.tlpFiltros.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    115F));

            this.tlpFiltros.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tlpFiltros.Name =
                "tlpFiltros";

            this.tlpFiltros.RowCount = 1;

            this.tlpFiltros.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // lblAnio

            this.lblAnio.Anchor =
                System.Windows.Forms.AnchorStyles.Left;

            this.lblAnio.AutoSize = true;

            this.lblAnio.BackColor =
                System.Drawing.Color.Transparent;

            this.lblAnio.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblAnio.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblAnio.Margin =
                new System.Windows.Forms.Padding(0, 0, 8, 0);

            this.lblAnio.Name =
                "lblAnio";

            this.lblAnio.Text =
                "Año";

            // cmbAnio

            this.cmbAnio.Anchor =
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.cmbAnio.BackColor =
                System.Drawing.Color.Transparent;

            this.cmbAnio.BorderRadius = 8;

            this.cmbAnio.DrawMode =
                System.Windows.Forms.DrawMode.OwnerDrawFixed;

            this.cmbAnio.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbAnio.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            this.cmbAnio.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            this.cmbAnio.ItemHeight = 30;

            this.cmbAnio.Margin =
                new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.cmbAnio.Name =
                "cmbAnio";

            this.cmbAnio.Size =
                new System.Drawing.Size(140, 36);

            // btnActualizar

            this.btnActualizar.Anchor =
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.btnActualizar.BorderRadius = 8;

            this.btnActualizar.FillColor =
                System.Drawing.Color.FromArgb(42, 91, 215);

            this.btnActualizar.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.btnActualizar.ForeColor =
                System.Drawing.Color.White;

            this.btnActualizar.Name =
                "btnActualizar";

            this.btnActualizar.Size =
                new System.Drawing.Size(105, 36);

            this.btnActualizar.Text =
                "Actualizar";

            this.btnActualizar.Click +=
                new System.EventHandler(
                    this.btnActualizar_Click);

            this.tlpFiltros.Controls.Add(
                this.lblAnio,
                0,
                0);

            this.tlpFiltros.Controls.Add(
                this.cmbAnio,
                1,
                0);

            this.tlpFiltros.Controls.Add(
                this.btnActualizar,
                2,
                0);

            this.pnlFiltros.Controls.Add(
                this.tlpFiltros);

            this.tlpEncabezado.Controls.Add(
                this.pnlTitulo,
                0,
                0);

            this.tlpEncabezado.Controls.Add(
                this.pnlFiltros,
                1,
                0);

            // =====================================================
            // KPI LAYOUT
            // =====================================================

            this.tlpKpis.ColumnCount = 3;

            this.tlpKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tlpKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tlpKpis.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33334F));

            this.tlpKpis.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tlpKpis.Margin =
                new System.Windows.Forms.Padding(0, 0, 0, 12);

            this.tlpKpis.Name =
                "tlpKpis";

            this.tlpKpis.RowCount = 1;

            this.tlpKpis.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // =====================================================
            // KPI INGRESOS
            // =====================================================

            this.pnlKpiIngresos.BorderRadius = 14;

            this.pnlKpiIngresos.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiIngresos.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiIngresos.Margin =
                new System.Windows.Forms.Padding(0, 0, 8, 0);

            this.pnlKpiIngresos.Name =
                "pnlKpiIngresos";

            this.lblTituloIngresos.AutoSize = true;

            this.lblTituloIngresos.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloIngresos.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloIngresos.ForeColor =
                System.Drawing.Color.FromArgb(105, 117, 134);

            this.lblTituloIngresos.Location =
                new System.Drawing.Point(20, 19);

            this.lblTituloIngresos.Text =
                "INGRESOS DEL AÑO";

            this.lblValorIngresos.AutoSize = true;

            this.lblValorIngresos.BackColor =
                System.Drawing.Color.Transparent;

            this.lblValorIngresos.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblValorIngresos.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblValorIngresos.Location =
                new System.Drawing.Point(18, 48);

            this.lblValorIngresos.Text =
                "$0.00";

            this.pnlKpiIngresos.Controls.Add(
                this.lblTituloIngresos);

            this.pnlKpiIngresos.Controls.Add(
                this.lblValorIngresos);

            // =====================================================
            // KPI PROMEDIO
            // =====================================================

            this.pnlKpiPromedio.BorderRadius = 14;

            this.pnlKpiPromedio.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiPromedio.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiPromedio.Margin =
                new System.Windows.Forms.Padding(8, 0, 8, 0);

            this.pnlKpiPromedio.Name =
                "pnlKpiPromedio";

            this.lblTituloPromedio.AutoSize = true;

            this.lblTituloPromedio.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloPromedio.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloPromedio.ForeColor =
                System.Drawing.Color.FromArgb(105, 117, 134);

            this.lblTituloPromedio.Location =
                new System.Drawing.Point(20, 19);

            this.lblTituloPromedio.Text =
                "PROMEDIO MENSUAL";

            this.lblValorPromedio.AutoSize = true;

            this.lblValorPromedio.BackColor =
                System.Drawing.Color.Transparent;

            this.lblValorPromedio.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblValorPromedio.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblValorPromedio.Location =
                new System.Drawing.Point(18, 48);

            this.lblValorPromedio.Text =
                "$0.00";

            this.pnlKpiPromedio.Controls.Add(
                this.lblTituloPromedio);

            this.pnlKpiPromedio.Controls.Add(
                this.lblValorPromedio);

            // =====================================================
            // KPI CARTERA
            // =====================================================

            this.pnlKpiCartera.BorderRadius = 14;

            this.pnlKpiCartera.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlKpiCartera.FillColor =
                System.Drawing.Color.White;

            this.pnlKpiCartera.Margin =
                new System.Windows.Forms.Padding(8, 0, 0, 0);

            this.pnlKpiCartera.Name =
                "pnlKpiCartera";

            this.lblTituloCartera.AutoSize = true;

            this.lblTituloCartera.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloCartera.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloCartera.ForeColor =
                System.Drawing.Color.FromArgb(105, 117, 134);

            this.lblTituloCartera.Location =
                new System.Drawing.Point(20, 19);

            this.lblTituloCartera.Text =
                "CARTERA DE CLIENTES";

            this.lblValorCartera.AutoSize = true;

            this.lblValorCartera.BackColor =
                System.Drawing.Color.Transparent;

            this.lblValorCartera.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblValorCartera.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblValorCartera.Location =
                new System.Drawing.Point(18, 48);

            this.lblValorCartera.Text =
                "$0.00";

            this.pnlKpiCartera.Controls.Add(
                this.lblTituloCartera);

            this.pnlKpiCartera.Controls.Add(
                this.lblValorCartera);

            this.tlpKpis.Controls.Add(
                this.pnlKpiIngresos,
                0,
                0);

            this.tlpKpis.Controls.Add(
                this.pnlKpiPromedio,
                1,
                0);

            this.tlpKpis.Controls.Add(
                this.pnlKpiCartera,
                2,
                0);

            // =====================================================
            // CONTENIDO
            // =====================================================

            this.tlpContenido.ColumnCount = 2;

            this.tlpContenido.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    50F));

            this.tlpContenido.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    50F));

            this.tlpContenido.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.tlpContenido.Margin =
                new System.Windows.Forms.Padding(0);

            this.tlpContenido.MinimumSize =
                new System.Drawing.Size(600, 450);

            this.tlpContenido.Name =
                "tlpContenido";

            this.tlpContenido.RowCount = 3;

            this.tlpContenido.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tlpContenido.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33333F));

            this.tlpContenido.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33334F));

            // =====================================================
            // PANEL MESES
            // =====================================================

            this.pnlMeses.BorderRadius = 14;

            this.pnlMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlMeses.FillColor =
                System.Drawing.Color.White;

            this.pnlMeses.Margin =
                new System.Windows.Forms.Padding(0, 0, 8, 8);

            this.pnlMeses.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlMeses.Name =
                "pnlMeses";

            this.pnlMeses.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloMeses.AutoSize = true;

            this.lblTituloMeses.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloMeses.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloMeses.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloMeses.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloMeses.Text =
                "Ingresos mensuales";

            // dgvMeses

            headerStyle1.BackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle1.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            headerStyle1.SelectionBackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle1.SelectionForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle1.BackColor =
                System.Drawing.Color.White;

            rowStyle1.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle1.SelectionBackColor =
                System.Drawing.Color.FromArgb(232, 239, 253);

            rowStyle1.SelectionForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.dgvMeses.AllowUserToAddRows = false;
            this.dgvMeses.AllowUserToDeleteRows = false;
            this.dgvMeses.AllowUserToResizeRows = false;

            this.dgvMeses.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvMeses.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvMeses.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvMeses.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvMeses.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvMeses.ColumnHeadersDefaultCellStyle =
                headerStyle1;

            this.dgvMeses.ColumnHeadersHeight = 32;

            this.dgvMeses.DefaultCellStyle =
                rowStyle1;

            this.dgvMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvMeses.MinimumSize =
                new System.Drawing.Size(100, 80);

            this.dgvMeses.Name =
                "dgvMeses";

            this.dgvMeses.ReadOnly = true;

            this.dgvMeses.RowHeadersVisible = false;

            this.dgvMeses.RowTemplate.Height = 28;

            this.dgvMeses.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvMeses.Size =
                new System.Drawing.Size(500, 160);

            this.pnlMeses.Controls.Add(
                this.dgvMeses);

            this.pnlMeses.Controls.Add(
                this.lblTituloMeses);

            // =====================================================
            // PANEL GRAFICA MENSUAL
            // =====================================================

            this.pnlGraficaMensual.BorderRadius = 14;

            this.pnlGraficaMensual.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlGraficaMensual.FillColor =
                System.Drawing.Color.White;

            this.pnlGraficaMensual.Margin =
                new System.Windows.Forms.Padding(8, 0, 0, 8);

            // CRÍTICO:
            // evita que TableLayoutPanel reduzca el panel a 0.
            this.pnlGraficaMensual.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlGraficaMensual.Name =
                "pnlGraficaMensual";

            this.pnlGraficaMensual.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloGraficaMensual.AutoSize = true;

            this.lblTituloGraficaMensual.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloGraficaMensual.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloGraficaMensual.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloGraficaMensual.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloGraficaMensual.Text =
                "Ingresos por mes";

            // chartMensual

            chartArea1.Name =
                "Principal";

            this.chartMensual.ChartAreas.Add(
                chartArea1);

            this.chartMensual.BackColor =
                System.Drawing.Color.White;

            // CRÍTICO:
            // Chart lanza Height must be greater than 0px
            // si recibe temporalmente tamaño 0.
            this.chartMensual.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.chartMensual.Name =
                "chartMensual";

            this.chartMensual.Size =
                new System.Drawing.Size(500, 160);

            this.chartMensual.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlGraficaMensual.Controls.Add(
                this.chartMensual);

            this.pnlGraficaMensual.Controls.Add(
                this.lblTituloGraficaMensual);

            // =====================================================
            // PANEL CLIENTES
            // =====================================================

            this.pnlClientes.BorderRadius = 14;

            this.pnlClientes.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlClientes.FillColor =
                System.Drawing.Color.White;

            this.pnlClientes.Margin =
                new System.Windows.Forms.Padding(0, 8, 8, 8);

            this.pnlClientes.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlClientes.Name =
                "pnlClientes";

            this.pnlClientes.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloClientes.AutoSize = true;

            this.lblTituloClientes.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloClientes.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloClientes.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloClientes.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloClientes.Text =
                "Cartera de clientes";

            // dgvClientes

            headerStyle2.BackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle2.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            headerStyle2.SelectionBackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle2.SelectionForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle2.BackColor =
                System.Drawing.Color.White;

            rowStyle2.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle2.SelectionBackColor =
                System.Drawing.Color.FromArgb(232, 239, 253);

            rowStyle2.SelectionForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AllowUserToResizeRows = false;

            this.dgvClientes.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvClientes.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvClientes.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvClientes.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvClientes.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvClientes.ColumnHeadersDefaultCellStyle =
                headerStyle2;

            this.dgvClientes.ColumnHeadersHeight = 32;

            this.dgvClientes.DefaultCellStyle =
                rowStyle2;

            this.dgvClientes.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvClientes.MinimumSize =
                new System.Drawing.Size(100, 80);

            this.dgvClientes.Name =
                "dgvClientes";

            this.dgvClientes.ReadOnly = true;

            this.dgvClientes.RowHeadersVisible = false;

            this.dgvClientes.RowTemplate.Height = 28;

            this.dgvClientes.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvClientes.Size =
                new System.Drawing.Size(500, 160);

            this.pnlClientes.Controls.Add(
                this.dgvClientes);

            this.pnlClientes.Controls.Add(
                this.lblTituloClientes);

            // =====================================================
            // PANEL CUENTAS
            // =====================================================

            this.pnlCuentas.BorderRadius = 14;

            this.pnlCuentas.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlCuentas.FillColor =
                System.Drawing.Color.White;

            this.pnlCuentas.Margin =
                new System.Windows.Forms.Padding(8, 8, 0, 8);

            this.pnlCuentas.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlCuentas.Name =
                "pnlCuentas";

            this.pnlCuentas.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloCuentas.AutoSize = true;

            this.lblTituloCuentas.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloCuentas.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloCuentas.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloCuentas.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloCuentas.Text =
                "Ingresos y cuenta de depósito";

            // dgvCuentas

            headerStyle3.BackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle3.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            headerStyle3.SelectionBackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle3.SelectionForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle3.BackColor =
                System.Drawing.Color.White;

            rowStyle3.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle3.SelectionBackColor =
                System.Drawing.Color.FromArgb(232, 239, 253);

            rowStyle3.SelectionForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.dgvCuentas.AllowUserToAddRows = false;
            this.dgvCuentas.AllowUserToDeleteRows = false;
            this.dgvCuentas.AllowUserToResizeRows = false;

            this.dgvCuentas.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvCuentas.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvCuentas.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvCuentas.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvCuentas.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvCuentas.ColumnHeadersDefaultCellStyle =
                headerStyle3;

            this.dgvCuentas.ColumnHeadersHeight = 32;

            this.dgvCuentas.DefaultCellStyle =
                rowStyle3;

            this.dgvCuentas.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvCuentas.MinimumSize =
                new System.Drawing.Size(100, 80);

            this.dgvCuentas.Name =
                "dgvCuentas";

            this.dgvCuentas.ReadOnly = true;

            this.dgvCuentas.RowHeadersVisible = false;

            this.dgvCuentas.RowTemplate.Height = 28;

            this.dgvCuentas.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvCuentas.Size =
                new System.Drawing.Size(500, 160);

            this.pnlCuentas.Controls.Add(
                this.dgvCuentas);

            this.pnlCuentas.Controls.Add(
                this.lblTituloCuentas);

            // =====================================================
            // PANEL ANTIGUEDAD
            // =====================================================

            this.pnlAntiguedad.BorderRadius = 14;

            this.pnlAntiguedad.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlAntiguedad.FillColor =
                System.Drawing.Color.White;

            this.pnlAntiguedad.Margin =
                new System.Windows.Forms.Padding(0, 8, 8, 0);

            this.pnlAntiguedad.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlAntiguedad.Name =
                "pnlAntiguedad";

            this.pnlAntiguedad.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloAntiguedad.AutoSize = true;

            this.lblTituloAntiguedad.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloAntiguedad.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloAntiguedad.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloAntiguedad.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloAntiguedad.Text =
                "Antigüedad de cartera";

            // dgvAntiguedad

            headerStyle4.BackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle4.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            headerStyle4.SelectionBackColor =
                System.Drawing.Color.FromArgb(247, 249, 252);

            headerStyle4.SelectionForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle4.BackColor =
                System.Drawing.Color.White;

            rowStyle4.ForeColor =
                System.Drawing.Color.FromArgb(68, 78, 94);

            rowStyle4.SelectionBackColor =
                System.Drawing.Color.FromArgb(232, 239, 253);

            rowStyle4.SelectionForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.dgvAntiguedad.AllowUserToAddRows = false;
            this.dgvAntiguedad.AllowUserToDeleteRows = false;
            this.dgvAntiguedad.AllowUserToResizeRows = false;

            this.dgvAntiguedad.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvAntiguedad.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvAntiguedad.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvAntiguedad.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvAntiguedad.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvAntiguedad.ColumnHeadersDefaultCellStyle =
                headerStyle4;

            this.dgvAntiguedad.ColumnHeadersHeight = 32;

            this.dgvAntiguedad.DefaultCellStyle =
                rowStyle4;

            this.dgvAntiguedad.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvAntiguedad.MinimumSize =
                new System.Drawing.Size(100, 80);

            this.dgvAntiguedad.Name =
                "dgvAntiguedad";

            this.dgvAntiguedad.ReadOnly = true;

            this.dgvAntiguedad.RowHeadersVisible = false;

            this.dgvAntiguedad.RowTemplate.Height = 28;

            this.dgvAntiguedad.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvAntiguedad.Size =
                new System.Drawing.Size(500, 160);

            this.pnlAntiguedad.Controls.Add(
                this.dgvAntiguedad);

            this.pnlAntiguedad.Controls.Add(
                this.lblTituloAntiguedad);

            // =====================================================
            // PANEL GRAFICA ANTIGUEDAD
            // =====================================================

            this.pnlGraficaAntiguedad.BorderRadius = 14;

            this.pnlGraficaAntiguedad.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlGraficaAntiguedad.FillColor =
                System.Drawing.Color.White;

            this.pnlGraficaAntiguedad.Margin =
                new System.Windows.Forms.Padding(8, 8, 0, 0);

            // CRÍTICO:
            // evita dimensiones temporales de 0.
            this.pnlGraficaAntiguedad.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.pnlGraficaAntiguedad.Name =
                "pnlGraficaAntiguedad";

            this.pnlGraficaAntiguedad.Padding =
                new System.Windows.Forms.Padding(14, 42, 14, 12);

            this.lblTituloGraficaAntiguedad.AutoSize = true;

            this.lblTituloGraficaAntiguedad.BackColor =
                System.Drawing.Color.Transparent;

            this.lblTituloGraficaAntiguedad.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloGraficaAntiguedad.ForeColor =
                System.Drawing.Color.FromArgb(25, 42, 70);

            this.lblTituloGraficaAntiguedad.Location =
                new System.Drawing.Point(18, 14);

            this.lblTituloGraficaAntiguedad.Text =
                "Distribución de cartera";

            // chartAntiguedad

            chartArea2.Name =
                "Principal";

            this.chartAntiguedad.ChartAreas.Add(
                chartArea2);

            this.chartAntiguedad.BackColor =
                System.Drawing.Color.White;

            this.chartAntiguedad.MinimumSize =
                new System.Drawing.Size(100, 100);

            this.chartAntiguedad.Name =
                "chartAntiguedad";

            this.chartAntiguedad.Size =
                new System.Drawing.Size(500, 160);

            this.chartAntiguedad.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlGraficaAntiguedad.Controls.Add(
                this.chartAntiguedad);

            this.pnlGraficaAntiguedad.Controls.Add(
                this.lblTituloGraficaAntiguedad);

            // =====================================================
            // AGREGAR PANELES AL CONTENIDO
            // =====================================================

            this.tlpContenido.Controls.Add(
                this.pnlMeses,
                0,
                0);

            this.tlpContenido.Controls.Add(
                this.pnlGraficaMensual,
                1,
                0);

            this.tlpContenido.Controls.Add(
                this.pnlClientes,
                0,
                1);

            this.tlpContenido.Controls.Add(
                this.pnlCuentas,
                1,
                1);

            this.tlpContenido.Controls.Add(
                this.pnlAntiguedad,
                0,
                2);

            this.tlpContenido.Controls.Add(
                this.pnlGraficaAntiguedad,
                1,
                2);

            // =====================================================
            // ESTADO
            // =====================================================

            this.lblEstado.AutoSize = true;

            this.lblEstado.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblEstado.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    8.5F);

            this.lblEstado.ForeColor =
                System.Drawing.Color.FromArgb(105, 117, 134);

            this.lblEstado.Margin =
                new System.Windows.Forms.Padding(2, 7, 0, 0);

            this.lblEstado.Name =
                "lblEstado";

            this.lblEstado.Text =
                "Listo";

            // =====================================================
            // AGREGAR SECCIONES PRINCIPALES
            // =====================================================

            this.tlpPrincipal.Controls.Add(
                this.tlpEncabezado,
                0,
                0);

            this.tlpPrincipal.Controls.Add(
                this.tlpKpis,
                0,
                1);

            this.tlpPrincipal.Controls.Add(
                this.tlpContenido,
                0,
                2);

            this.tlpPrincipal.Controls.Add(
                this.lblEstado,
                0,
                3);

            // =====================================================
            // FORMULARIO
            // =====================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6F, 13F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.FromArgb(244, 247, 251);

            this.ClientSize =
                new System.Drawing.Size(1180, 900);

            this.Controls.Add(
                this.tlpPrincipal);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.MinimumSize =
                new System.Drawing.Size(1080, 820);

            this.Name =
                "DashboardIngresos";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Dashboard de Ingresos";

            // =====================================================
            // FINALIZAR INICIALIZACIÓN
            // =====================================================

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvMeses)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartMensual)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvClientes)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvCuentas)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvAntiguedad)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.chartAntiguedad)).EndInit();

            // =====================================================
            // REANUDAR LAYOUT
            // =====================================================

            this.pnlGraficaAntiguedad.ResumeLayout(false);
            this.pnlGraficaAntiguedad.PerformLayout();

            this.pnlAntiguedad.ResumeLayout(false);
            this.pnlAntiguedad.PerformLayout();

            this.pnlCuentas.ResumeLayout(false);
            this.pnlCuentas.PerformLayout();

            this.pnlClientes.ResumeLayout(false);
            this.pnlClientes.PerformLayout();

            this.pnlGraficaMensual.ResumeLayout(false);
            this.pnlGraficaMensual.PerformLayout();

            this.pnlMeses.ResumeLayout(false);
            this.pnlMeses.PerformLayout();

            this.tlpContenido.ResumeLayout(false);

            this.pnlKpiCartera.ResumeLayout(false);
            this.pnlKpiCartera.PerformLayout();

            this.pnlKpiPromedio.ResumeLayout(false);
            this.pnlKpiPromedio.PerformLayout();

            this.pnlKpiIngresos.ResumeLayout(false);
            this.pnlKpiIngresos.PerformLayout();

            this.tlpKpis.ResumeLayout(false);

            this.tlpFiltros.ResumeLayout(false);
            this.tlpFiltros.PerformLayout();

            this.pnlFiltros.ResumeLayout(false);

            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();

            this.tlpEncabezado.ResumeLayout(false);

            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();

            this.ResumeLayout(false);
        }

        #endregion
    }
}