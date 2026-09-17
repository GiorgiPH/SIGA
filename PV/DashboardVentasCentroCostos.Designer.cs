namespace PV
{
    partial class DashboardVentasCentroCostos
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel principal;
        private System.Windows.Forms.TableLayoutPanel encabezado;
        private System.Windows.Forms.TableLayoutPanel contenido;

        private Guna.UI2.WinForms.Guna2Panel pnlCabecera;
        private Guna.UI2.WinForms.Guna2Panel pnlResumen;

        private System.Windows.Forms.TableLayoutPanel layoutCabecera;
        private System.Windows.Forms.TableLayoutPanel layoutResumen;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;

        private System.Windows.Forms.Label lblCentroTitulo;
        private System.Windows.Forms.Label lblCentroValor;

        private System.Windows.Forms.Label lblAnioTitulo;
        private System.Windows.Forms.Label lblAnioValor;

        private System.Windows.Forms.Label lblTotalTitulo;
        private System.Windows.Forms.Label lblTotalValor;

        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaGrafica;

        private System.Windows.Forms.TableLayoutPanel layoutMeses;
        private System.Windows.Forms.TableLayoutPanel layoutGrafica;

        private System.Windows.Forms.Label lblTituloMeses;
        private System.Windows.Forms.Label lblTituloGrafica;
        private System.Windows.Forms.Label lblEstado;

        private Guna.UI2.WinForms.Guna2DataGridView dgvMeses;

        private System.Windows.Forms.DataGridViewTextBoxColumn colMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImporte;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeses;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            System.Windows.Forms.DataVisualization.Charting.Series series1 =
                new System.Windows.Forms.DataVisualization.Charting.Series();

            System.Windows.Forms.DataGridViewCellStyle gridHeader =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle gridCell =
                new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle gridImporte =
                new System.Windows.Forms.DataGridViewCellStyle();

            this.components =
                new System.ComponentModel.Container();

            this.principal =
                new System.Windows.Forms.TableLayoutPanel();

            this.encabezado =
                new System.Windows.Forms.TableLayoutPanel();

            this.contenido =
                new System.Windows.Forms.TableLayoutPanel();

            this.pnlCabecera =
                new Guna.UI2.WinForms.Guna2Panel();

            this.pnlResumen =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutCabecera =
                new System.Windows.Forms.TableLayoutPanel();

            this.layoutResumen =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTitulo =
                new System.Windows.Forms.Label();

            this.lblSubtitulo =
                new System.Windows.Forms.Label();

            this.lblCentroTitulo =
                new System.Windows.Forms.Label();

            this.lblCentroValor =
                new System.Windows.Forms.Label();

            this.lblAnioTitulo =
                new System.Windows.Forms.Label();

            this.lblAnioValor =
                new System.Windows.Forms.Label();

            this.lblTotalTitulo =
                new System.Windows.Forms.Label();

            this.lblTotalValor =
                new System.Windows.Forms.Label();

            this.pnlTarjetaMeses =
                new Guna.UI2.WinForms.Guna2Panel();

            this.pnlTarjetaGrafica =
                new Guna.UI2.WinForms.Guna2Panel();

            this.layoutMeses =
                new System.Windows.Forms.TableLayoutPanel();

            this.layoutGrafica =
                new System.Windows.Forms.TableLayoutPanel();

            this.lblTituloMeses =
                new System.Windows.Forms.Label();

            this.lblTituloGrafica =
                new System.Windows.Forms.Label();

            this.lblEstado =
                new System.Windows.Forms.Label();

            this.dgvMeses =
                new Guna.UI2.WinForms.Guna2DataGridView();

            this.colMes =
                new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.colImporte =
                new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.chartMeses =
                new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.principal.SuspendLayout();
            this.encabezado.SuspendLayout();

            this.pnlCabecera.SuspendLayout();
            this.pnlResumen.SuspendLayout();

            this.layoutCabecera.SuspendLayout();
            this.layoutResumen.SuspendLayout();

            this.contenido.SuspendLayout();

            this.pnlTarjetaMeses.SuspendLayout();
            this.pnlTarjetaGrafica.SuspendLayout();

            this.layoutMeses.SuspendLayout();
            this.layoutGrafica.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvMeses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeses)).BeginInit();

            this.SuspendLayout();

            // ============================================================
            // principal
            // ============================================================

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
                this.contenido,
                0,
                1);

            this.principal.Controls.Add(
                this.lblEstado,
                0,
                2);

            this.principal.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.principal.Margin =
                new System.Windows.Forms.Padding(0);

            this.principal.Name =
                "principal";

            this.principal.RowCount =
                3;

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    145F));

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.principal.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    35F));

            // ============================================================
            // encabezado
            // ============================================================

            this.encabezado.BackColor =
                System.Drawing.Color.Transparent;

            this.encabezado.ColumnCount =
                2;

            this.encabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    65F));

            this.encabezado.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    35F));

            this.encabezado.Controls.Add(
                this.pnlCabecera,
                0,
                0);

            this.encabezado.Controls.Add(
                this.pnlResumen,
                1,
                0);

            this.encabezado.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.encabezado.Margin =
                new System.Windows.Forms.Padding(0);

            this.encabezado.Name =
                "encabezado";

            this.encabezado.RowCount =
                1;

            this.encabezado.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // ============================================================
            // pnlCabecera
            // ============================================================

            this.pnlCabecera.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlCabecera.BorderRadius =
                14;

            this.pnlCabecera.BorderThickness =
                1;

            this.pnlCabecera.Controls.Add(
                this.layoutCabecera);

            this.pnlCabecera.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlCabecera.FillColor =
                System.Drawing.Color.White;

            this.pnlCabecera.Margin =
                new System.Windows.Forms.Padding(
                    5,
                    5,
                    8,
                    10);

            this.pnlCabecera.Name =
                "pnlCabecera";

            this.pnlCabecera.Padding =
                new System.Windows.Forms.Padding(
                    22,
                    15,
                    22,
                    15);

            // ============================================================
            // layoutCabecera
            // ============================================================

            this.layoutCabecera.BackColor =
                System.Drawing.Color.White;

            this.layoutCabecera.ColumnCount =
                1;

            this.layoutCabecera.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutCabecera.Controls.Add(
                this.lblTitulo,
                0,
                0);

            this.layoutCabecera.Controls.Add(
                this.lblSubtitulo,
                0,
                1);

            this.layoutCabecera.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutCabecera.Margin =
                new System.Windows.Forms.Padding(0);

            this.layoutCabecera.Name =
                "layoutCabecera";

            this.layoutCabecera.RowCount =
                2;

            this.layoutCabecera.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    60F));

            this.layoutCabecera.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    40F));

            // ============================================================
            // lblTitulo
            // ============================================================

            this.lblTitulo.AutoEllipsis =
                true;

            this.lblTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblTitulo.Margin =
                new System.Windows.Forms.Padding(0);

            this.lblTitulo.Name =
                "lblTitulo";

            this.lblTitulo.Text =
                "Detalle por centro de costos";

            this.lblTitulo.TextAlign =
                System.Drawing.ContentAlignment.BottomLeft;

            // ============================================================
            // lblSubtitulo
            // ============================================================

            this.lblSubtitulo.AutoEllipsis =
                true;

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

            this.lblSubtitulo.Margin =
                new System.Windows.Forms.Padding(0);

            this.lblSubtitulo.Name =
                "lblSubtitulo";

            this.lblSubtitulo.Text =
                "Comportamiento mensual del centro seleccionado";

            this.lblSubtitulo.TextAlign =
                System.Drawing.ContentAlignment.TopLeft;

            // ============================================================
            // pnlResumen
            // ============================================================

            this.pnlResumen.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlResumen.BorderRadius =
                14;

            this.pnlResumen.BorderThickness =
                1;

            this.pnlResumen.Controls.Add(
                this.layoutResumen);

            this.pnlResumen.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlResumen.FillColor =
                System.Drawing.Color.White;

            this.pnlResumen.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    5,
                    5,
                    10);

            this.pnlResumen.Name =
                "pnlResumen";

            this.pnlResumen.Padding =
                new System.Windows.Forms.Padding(
                    18);

            // ============================================================
            // layoutResumen
            // ============================================================

            this.layoutResumen.BackColor =
                System.Drawing.Color.White;

            this.layoutResumen.ColumnCount =
                2;

            this.layoutResumen.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    42F));

            this.layoutResumen.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    58F));

            this.layoutResumen.Controls.Add(
                this.lblCentroTitulo,
                0,
                0);

            this.layoutResumen.Controls.Add(
                this.lblCentroValor,
                1,
                0);

            this.layoutResumen.Controls.Add(
                this.lblAnioTitulo,
                0,
                1);

            this.layoutResumen.Controls.Add(
                this.lblAnioValor,
                1,
                1);

            this.layoutResumen.Controls.Add(
                this.lblTotalTitulo,
                0,
                2);

            this.layoutResumen.Controls.Add(
                this.lblTotalValor,
                1,
                2);

            this.layoutResumen.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutResumen.Margin =
                new System.Windows.Forms.Padding(0);

            this.layoutResumen.Name =
                "layoutResumen";

            this.layoutResumen.RowCount =
                3;

            this.layoutResumen.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33F));

            this.layoutResumen.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.33F));

            this.layoutResumen.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    33.34F));

            // ============================================================
            // lblCentroTitulo
            // ============================================================

            this.lblCentroTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblCentroTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblCentroTitulo.Name =
                "lblCentroTitulo";

            this.lblCentroTitulo.Text =
                "Centro";

            this.lblCentroTitulo.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // lblCentroValor
            // ============================================================

            this.lblCentroValor.AutoEllipsis =
                true;

            this.lblCentroValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblCentroValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblCentroValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblCentroValor.Name =
                "lblCentroValor";

            this.lblCentroValor.Text =
                "—";

            this.lblCentroValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // ============================================================
            // lblAnioTitulo
            // ============================================================

            this.lblAnioTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblAnioTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblAnioTitulo.Name =
                "lblAnioTitulo";

            this.lblAnioTitulo.Text =
                "Año";

            this.lblAnioTitulo.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // lblAnioValor
            // ============================================================

            this.lblAnioValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblAnioValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblAnioValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblAnioValor.Name =
                "lblAnioValor";

            this.lblAnioValor.Text =
                "—";

            this.lblAnioValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // ============================================================
            // lblTotalTitulo
            // ============================================================

            this.lblTotalTitulo.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTotalTitulo.ForeColor =
                System.Drawing.Color.FromArgb(
                    95,
                    114,
                    139);

            this.lblTotalTitulo.Name =
                "lblTotalTitulo";

            this.lblTotalTitulo.Text =
                "Total anual";

            this.lblTotalTitulo.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // lblTotalValor
            // ============================================================

            this.lblTotalValor.AutoEllipsis =
                true;

            this.lblTotalValor.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTotalValor.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    12F,
                    System.Drawing.FontStyle.Bold);

            this.lblTotalValor.ForeColor =
                System.Drawing.Color.FromArgb(
                    9,
                    43,
                    91);

            this.lblTotalValor.Name =
                "lblTotalValor";

            this.lblTotalValor.Text =
                "—";

            this.lblTotalValor.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // ============================================================
            // contenido
            // ============================================================

            this.contenido.BackColor =
                System.Drawing.Color.Transparent;

            this.contenido.ColumnCount =
                2;

            this.contenido.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    36F));

            this.contenido.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    64F));

            this.contenido.Controls.Add(
                this.pnlTarjetaMeses,
                0,
                0);

            this.contenido.Controls.Add(
                this.pnlTarjetaGrafica,
                1,
                0);

            this.contenido.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.contenido.Margin =
                new System.Windows.Forms.Padding(0);

            this.contenido.Name =
                "contenido";

            this.contenido.RowCount =
                1;

            this.contenido.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // ============================================================
            // pnlTarjetaMeses
            // ============================================================

            this.pnlTarjetaMeses.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaMeses.BorderRadius =
                14;

            this.pnlTarjetaMeses.BorderThickness =
                1;

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
                new System.Windows.Forms.Padding(
                    16);

            // ============================================================
            // layoutMeses
            // ============================================================

            this.layoutMeses.BackColor =
                System.Drawing.Color.White;

            this.layoutMeses.ColumnCount =
                1;

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

            this.layoutMeses.Margin =
                new System.Windows.Forms.Padding(0);

            this.layoutMeses.Name =
                "layoutMeses";

            this.layoutMeses.RowCount =
                2;

            this.layoutMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    40F));

            this.layoutMeses.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // ============================================================
            // lblTituloMeses
            // ============================================================

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

            this.lblTituloMeses.Text =
                "Ingresos mensuales";

            this.lblTituloMeses.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // dgvMeses
            // ============================================================

            this.dgvMeses.AllowUserToAddRows =
                false;

            this.dgvMeses.AllowUserToDeleteRows =
                false;

            this.dgvMeses.AllowUserToResizeRows =
                false;

            this.dgvMeses.AutoGenerateColumns =
                false;

            this.dgvMeses.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvMeses.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvMeses.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            gridHeader.BackColor =
                System.Drawing.Color.FromArgb(
                    242,
                    246,
                    251);

            gridHeader.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            gridHeader.SelectionBackColor =
                gridHeader.BackColor;

            gridHeader.SelectionForeColor =
                gridHeader.ForeColor;

            this.dgvMeses.ColumnHeadersDefaultCellStyle =
                gridHeader;

            this.dgvMeses.ColumnHeadersHeight =
                32;

            this.dgvMeses.Columns.AddRange(
                new System.Windows.Forms.DataGridViewColumn[]
                {
                    this.colMes,
                    this.colImporte
                });

            gridCell.BackColor =
                System.Drawing.Color.White;

            gridCell.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    57,
                    91);

            gridCell.SelectionBackColor =
                System.Drawing.Color.FromArgb(
                    231,
                    239,
                    249);

            gridCell.SelectionForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.dgvMeses.DefaultCellStyle =
                gridCell;

            this.dgvMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvMeses.MultiSelect =
                false;

            this.dgvMeses.Name =
                "dgvMeses";

            this.dgvMeses.ReadOnly =
                true;

            this.dgvMeses.RowHeadersVisible =
                false;

            this.dgvMeses.RowTemplate.Height =
                28;

            this.dgvMeses.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // ============================================================
            // colMes
            // ============================================================

            this.colMes.DataPropertyName =
                "NombreMes";

            this.colMes.FillWeight =
                58F;

            this.colMes.HeaderText =
                "Mes";

            this.colMes.Name =
                "colMes";

            this.colMes.ReadOnly =
                true;

            this.colMes.SortMode =
                System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // ============================================================
            // colImporte
            // ============================================================

            this.colImporte.DataPropertyName =
                "TotalVentas";

            gridImporte.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            gridImporte.Format =
                "C2";

            this.colImporte.DefaultCellStyle =
                gridImporte;

            this.colImporte.FillWeight =
                42F;

            this.colImporte.HeaderText =
                "Ingresos";

            this.colImporte.Name =
                "colImporte";

            this.colImporte.ReadOnly =
                true;

            this.colImporte.SortMode =
                System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // ============================================================
            // pnlTarjetaGrafica
            // ============================================================

            this.pnlTarjetaGrafica.BorderColor =
                System.Drawing.Color.FromArgb(
                    220,
                    227,
                    235);

            this.pnlTarjetaGrafica.BorderRadius =
                14;

            this.pnlTarjetaGrafica.BorderThickness =
                1;

            this.pnlTarjetaGrafica.Controls.Add(
                this.layoutGrafica);

            this.pnlTarjetaGrafica.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlTarjetaGrafica.FillColor =
                System.Drawing.Color.White;

            this.pnlTarjetaGrafica.Margin =
                new System.Windows.Forms.Padding(5);

            this.pnlTarjetaGrafica.Name =
                "pnlTarjetaGrafica";

            this.pnlTarjetaGrafica.Padding =
                new System.Windows.Forms.Padding(
                    16);

            // ============================================================
            // layoutGrafica
            // ============================================================

            this.layoutGrafica.BackColor =
                System.Drawing.Color.White;

            this.layoutGrafica.ColumnCount =
                1;

            this.layoutGrafica.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            this.layoutGrafica.Controls.Add(
                this.lblTituloGrafica,
                0,
                0);

            this.layoutGrafica.Controls.Add(
                this.chartMeses,
                0,
                1);

            this.layoutGrafica.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.layoutGrafica.Margin =
                new System.Windows.Forms.Padding(0);

            this.layoutGrafica.Name =
                "layoutGrafica";

            this.layoutGrafica.RowCount =
                2;

            this.layoutGrafica.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    40F));

            this.layoutGrafica.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));

            // ============================================================
            // lblTituloGrafica
            // ============================================================

            this.lblTituloGrafica.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.lblTituloGrafica.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblTituloGrafica.ForeColor =
                System.Drawing.Color.FromArgb(
                    21,
                    48,
                    87);

            this.lblTituloGrafica.Name =
                "lblTituloGrafica";

            this.lblTituloGrafica.Text =
                "Evolución mensual";

            this.lblTituloGrafica.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // chartMeses
            // ============================================================

            chartArea1.Name =
                "Ventas";

            this.chartMeses.ChartAreas.Add(
                chartArea1);

            this.chartMeses.BackColor =
                System.Drawing.Color.White;

            this.chartMeses.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.chartMeses.Name =
                "chartMeses";

            series1.ChartArea =
                "Ventas";

            series1.ChartType =
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            series1.IsVisibleInLegend =
                false;

            series1.Name =
                "Ventas";

            this.chartMeses.Series.Add(
                series1);

            // ============================================================
            // lblEstado
            // ============================================================

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

            this.lblEstado.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // FORM
            // ============================================================

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
                    1050,
                    650);

            this.Controls.Add(
                this.principal);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.MinimumSize =
                new System.Drawing.Size(
                    900,
                    600);

            this.Name =
                "DashboardVentasCentroCostos";

            this.Padding =
                new System.Windows.Forms.Padding(
                    24);

            this.ShowInTaskbar =
                false;

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Detalle de Ventas";

            this.Shown +=
                new System.EventHandler(
                    this.DashboardVentasCentroCostos_Shown);

            this.FormClosed +=
                new System.Windows.Forms.FormClosedEventHandler(
                    this.DashboardVentasCentroCostos_FormClosed);

            // ============================================================
            // RESUME
            // ============================================================

            this.principal.ResumeLayout(false);

            this.encabezado.ResumeLayout(false);

            this.pnlCabecera.ResumeLayout(false);
            this.pnlResumen.ResumeLayout(false);

            this.layoutCabecera.ResumeLayout(false);
            this.layoutResumen.ResumeLayout(false);

            this.contenido.ResumeLayout(false);

            this.pnlTarjetaMeses.ResumeLayout(false);
            this.pnlTarjetaGrafica.ResumeLayout(false);

            this.layoutMeses.ResumeLayout(false);
            this.layoutGrafica.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(
                this.dgvMeses)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(
                this.chartMeses)).EndInit();

            this.ResumeLayout(false);
        }
    }
}