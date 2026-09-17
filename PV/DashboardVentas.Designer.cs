namespace PV
{ 
    partial class DashboardVentas
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

        // KPIs
        private Guna.UI2.WinForms.Guna2Panel pnlKpiIngresos;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiMejorMes;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiCentro;

        private System.Windows.Forms.TableLayoutPanel layoutKpiIngresos;
        private System.Windows.Forms.TableLayoutPanel layoutKpiMejorMes;
        private System.Windows.Forms.TableLayoutPanel layoutKpiCentro;

        private System.Windows.Forms.Label lblKpiIngresosTitulo;
        private System.Windows.Forms.Label lblKpiIngresosValor;
        private System.Windows.Forms.Label lblKpiIngresosDetalle;

        private System.Windows.Forms.Label lblKpiMejorMesTitulo;
        private System.Windows.Forms.Label lblKpiMejorMesValor;
        private System.Windows.Forms.Label lblKpiMejorMesDetalle;

        private System.Windows.Forms.Label lblKpiCentroTitulo;
        private System.Windows.Forms.Label lblKpiCentroValor;
        private System.Windows.Forms.Label lblKpiCentroDetalle;

        // TARJETAS
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaGraficaMeses;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaCentros;
        private Guna.UI2.WinForms.Guna2Panel pnlTarjetaGraficaCentros;

        private System.Windows.Forms.TableLayoutPanel layoutMeses;
        private System.Windows.Forms.TableLayoutPanel layoutGraficaMeses;
        private System.Windows.Forms.TableLayoutPanel layoutCentros;
        private System.Windows.Forms.TableLayoutPanel layoutGraficaCentros;

        private System.Windows.Forms.Label lblTituloMeses;
        private System.Windows.Forms.Label lblTituloGraficaMeses;
        private System.Windows.Forms.Label lblTituloCentros;
        private System.Windows.Forms.Label lblTituloGraficaCentros;

        private Guna.UI2.WinForms.Guna2DataGridView dgvMeses;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCentros;

        private System.Windows.Forms.DataGridViewTextBoxColumn colMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMesImporte;

        private System.Windows.Forms.DataGridViewTextBoxColumn colCentro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCentroImporte;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeses;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCentros;

        private System.Windows.Forms.Panel pnlGraficaCentros;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.principal = new System.Windows.Forms.TableLayoutPanel();
            this.encabezado = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCabeceraTitulo = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutCabeceraTitulo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlCabeceraFiltros = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutCabeceraFiltros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloFiltros = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.btnActualizar = new Guna.UI2.WinForms.Guna2Button();
            this.cmbAnio = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblAnio = new System.Windows.Forms.Label();
            this.pnlKpis = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiIngresos = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutKpiIngresos = new System.Windows.Forms.TableLayoutPanel();
            this.lblKpiIngresosTitulo = new System.Windows.Forms.Label();
            this.lblKpiIngresosValor = new System.Windows.Forms.Label();
            this.lblKpiIngresosDetalle = new System.Windows.Forms.Label();
            this.pnlKpiMejorMes = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutKpiMejorMes = new System.Windows.Forms.TableLayoutPanel();
            this.lblKpiMejorMesTitulo = new System.Windows.Forms.Label();
            this.lblKpiMejorMesValor = new System.Windows.Forms.Label();
            this.lblKpiMejorMesDetalle = new System.Windows.Forms.Label();
            this.pnlKpiCentro = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutKpiCentro = new System.Windows.Forms.TableLayoutPanel();
            this.lblKpiCentroTitulo = new System.Windows.Forms.Label();
            this.lblKpiCentroValor = new System.Windows.Forms.Label();
            this.lblKpiCentroDetalle = new System.Windows.Forms.Label();
            this.tarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTarjetaMeses = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutMeses = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloMeses = new System.Windows.Forms.Label();
            this.dgvMeses = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTarjetaGraficaMeses = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutGraficaMeses = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloGraficaMeses = new System.Windows.Forms.Label();
            this.chartMeses = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlTarjetaCentros = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutCentros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloCentros = new System.Windows.Forms.Label();
            this.dgvCentros = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlTarjetaGraficaCentros = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutGraficaCentros = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloGraficaCentros = new System.Windows.Forms.Label();
            this.pnlGraficaCentros = new System.Windows.Forms.Panel();
            this.chartCentros = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblEstado = new System.Windows.Forms.Label();
            this.principal.SuspendLayout();
            this.encabezado.SuspendLayout();
            this.pnlCabeceraTitulo.SuspendLayout();
            this.layoutCabeceraTitulo.SuspendLayout();
            this.pnlCabeceraFiltros.SuspendLayout();
            this.layoutCabeceraFiltros.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlKpis.SuspendLayout();
            this.pnlKpiIngresos.SuspendLayout();
            this.layoutKpiIngresos.SuspendLayout();
            this.pnlKpiMejorMes.SuspendLayout();
            this.layoutKpiMejorMes.SuspendLayout();
            this.pnlKpiCentro.SuspendLayout();
            this.layoutKpiCentro.SuspendLayout();
            this.tarjetas.SuspendLayout();
            this.pnlTarjetaMeses.SuspendLayout();
            this.layoutMeses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeses)).BeginInit();
            this.pnlTarjetaGraficaMeses.SuspendLayout();
            this.layoutGraficaMeses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeses)).BeginInit();
            this.pnlTarjetaCentros.SuspendLayout();
            this.layoutCentros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCentros)).BeginInit();
            this.pnlTarjetaGraficaCentros.SuspendLayout();
            this.layoutGraficaCentros.SuspendLayout();
            this.pnlGraficaCentros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCentros)).BeginInit();
            this.SuspendLayout();
            // 
            // principal
            // 
            this.principal.BackColor = System.Drawing.Color.Transparent;
            this.principal.ColumnCount = 1;
            this.principal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.principal.Controls.Add(this.encabezado, 0, 0);
            this.principal.Controls.Add(this.pnlKpis, 0, 1);
            this.principal.Controls.Add(this.tarjetas, 0, 2);
            this.principal.Controls.Add(this.lblEstado, 0, 3);
            this.principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.principal.Location = new System.Drawing.Point(24, 24);
            this.principal.Margin = new System.Windows.Forms.Padding(0);
            this.principal.Name = "principal";
            this.principal.RowCount = 4;
            this.principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.principal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.principal.Size = new System.Drawing.Size(1132, 772);
            this.principal.TabIndex = 0;
            // 
            // encabezado
            // 
            this.encabezado.BackColor = System.Drawing.Color.Transparent;
            this.encabezado.ColumnCount = 2;
            this.encabezado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.encabezado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.encabezado.Controls.Add(this.pnlCabeceraTitulo, 0, 0);
            this.encabezado.Controls.Add(this.pnlCabeceraFiltros, 1, 0);
            this.encabezado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encabezado.Location = new System.Drawing.Point(0, 0);
            this.encabezado.Margin = new System.Windows.Forms.Padding(0);
            this.encabezado.Name = "encabezado";
            this.encabezado.RowCount = 1;
            this.encabezado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.encabezado.Size = new System.Drawing.Size(1132, 115);
            this.encabezado.TabIndex = 0;
            // 
            // pnlCabeceraTitulo
            // 
            this.pnlCabeceraTitulo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlCabeceraTitulo.BorderRadius = 14;
            this.pnlCabeceraTitulo.BorderThickness = 1;
            this.pnlCabeceraTitulo.Controls.Add(this.layoutCabeceraTitulo);
            this.pnlCabeceraTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCabeceraTitulo.FillColor = System.Drawing.Color.White;
            this.pnlCabeceraTitulo.Location = new System.Drawing.Point(5, 5);
            this.pnlCabeceraTitulo.Margin = new System.Windows.Forms.Padding(5, 5, 8, 10);
            this.pnlCabeceraTitulo.Name = "pnlCabeceraTitulo";
            this.pnlCabeceraTitulo.Padding = new System.Windows.Forms.Padding(22, 15, 22, 15);
            this.pnlCabeceraTitulo.Size = new System.Drawing.Size(711, 100);
            this.pnlCabeceraTitulo.TabIndex = 0;
            // 
            // layoutCabeceraTitulo
            // 
            this.layoutCabeceraTitulo.BackColor = System.Drawing.Color.White;
            this.layoutCabeceraTitulo.ColumnCount = 1;
            this.layoutCabeceraTitulo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCabeceraTitulo.Controls.Add(this.lblTitulo, 0, 0);
            this.layoutCabeceraTitulo.Controls.Add(this.lblSubtitulo, 0, 1);
            this.layoutCabeceraTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCabeceraTitulo.Location = new System.Drawing.Point(22, 15);
            this.layoutCabeceraTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.layoutCabeceraTitulo.Name = "layoutCabeceraTitulo";
            this.layoutCabeceraTitulo.RowCount = 2;
            this.layoutCabeceraTitulo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.layoutCabeceraTitulo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.layoutCabeceraTitulo.Size = new System.Drawing.Size(667, 70);
            this.layoutCabeceraTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(43)))), ((int)(((byte)(91)))));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(667, 43);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Dashboard de Ventas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoEllipsis = true;
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(132)))), ((int)(((byte)(160)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(1, 43);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(666, 27);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Resumen general de ventas";
            // 
            // pnlCabeceraFiltros
            // 
            this.pnlCabeceraFiltros.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlCabeceraFiltros.BorderRadius = 14;
            this.pnlCabeceraFiltros.BorderThickness = 1;
            this.pnlCabeceraFiltros.Controls.Add(this.layoutCabeceraFiltros);
            this.pnlCabeceraFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCabeceraFiltros.FillColor = System.Drawing.Color.White;
            this.pnlCabeceraFiltros.Location = new System.Drawing.Point(732, 5);
            this.pnlCabeceraFiltros.Margin = new System.Windows.Forms.Padding(8, 5, 5, 10);
            this.pnlCabeceraFiltros.Name = "pnlCabeceraFiltros";
            this.pnlCabeceraFiltros.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.pnlCabeceraFiltros.Size = new System.Drawing.Size(395, 100);
            this.pnlCabeceraFiltros.TabIndex = 1;
            // 
            // layoutCabeceraFiltros
            // 
            this.layoutCabeceraFiltros.BackColor = System.Drawing.Color.White;
            this.layoutCabeceraFiltros.ColumnCount = 1;
            this.layoutCabeceraFiltros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCabeceraFiltros.Controls.Add(this.lblTituloFiltros, 0, 0);
            this.layoutCabeceraFiltros.Controls.Add(this.pnlFiltros, 0, 1);
            this.layoutCabeceraFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCabeceraFiltros.Location = new System.Drawing.Point(18, 12);
            this.layoutCabeceraFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.layoutCabeceraFiltros.Name = "layoutCabeceraFiltros";
            this.layoutCabeceraFiltros.RowCount = 2;
            this.layoutCabeceraFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutCabeceraFiltros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCabeceraFiltros.Size = new System.Drawing.Size(359, 76);
            this.layoutCabeceraFiltros.TabIndex = 0;
            // 
            // lblTituloFiltros
            // 
            this.lblTituloFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloFiltros.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloFiltros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(114)))), ((int)(((byte)(139)))));
            this.lblTituloFiltros.Location = new System.Drawing.Point(0, 0);
            this.lblTituloFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.lblTituloFiltros.Name = "lblTituloFiltros";
            this.lblTituloFiltros.Size = new System.Drawing.Size(359, 25);
            this.lblTituloFiltros.TabIndex = 0;
            this.lblTituloFiltros.Text = "FILTROS";
            this.lblTituloFiltros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.White;
            this.pnlFiltros.Controls.Add(this.btnActualizar);
            this.pnlFiltros.Controls.Add(this.cmbAnio);
            this.pnlFiltros.Controls.Add(this.lblAnio);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFiltros.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 25);
            this.pnlFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlFiltros.Size = new System.Drawing.Size(359, 51);
            this.pnlFiltros.TabIndex = 1;
            this.pnlFiltros.WrapContents = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BorderRadius = 9;
            this.btnActualizar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(67)))), ((int)(((byte)(112)))));
            this.btnActualizar.Location = new System.Drawing.Point(239, 10);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 38);
            this.btnActualizar.TabIndex = 0;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // cmbAnio
            // 
            this.cmbAnio.BackColor = System.Drawing.Color.Transparent;
            this.cmbAnio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(228)))));
            this.cmbAnio.BorderRadius = 9;
            this.cmbAnio.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAnio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnio.FocusedColor = System.Drawing.Color.Empty;
            this.cmbAnio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAnio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(67)))), ((int)(((byte)(89)))));
            this.cmbAnio.ItemHeight = 30;
            this.cmbAnio.Location = new System.Drawing.Point(105, 10);
            this.cmbAnio.Margin = new System.Windows.Forms.Padding(0);
            this.cmbAnio.Name = "cmbAnio";
            this.cmbAnio.Size = new System.Drawing.Size(120, 36);
            this.cmbAnio.TabIndex = 1;
            this.cmbAnio.SelectedIndexChanged += new System.EventHandler(this.cmbAnio_SelectedIndexChanged);
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(67)))), ((int)(((byte)(89)))));
            this.lblAnio.Location = new System.Drawing.Point(59, 19);
            this.lblAnio.Margin = new System.Windows.Forms.Padding(0, 9, 12, 0);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(34, 19);
            this.lblAnio.TabIndex = 2;
            this.lblAnio.Text = "Año";
            // 
            // pnlKpis
            // 
            this.pnlKpis.ColumnCount = 3;
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlKpis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.pnlKpis.Controls.Add(this.pnlKpiIngresos, 0, 0);
            this.pnlKpis.Controls.Add(this.pnlKpiMejorMes, 1, 0);
            this.pnlKpis.Controls.Add(this.pnlKpiCentro, 2, 0);
            this.pnlKpis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpis.Location = new System.Drawing.Point(0, 115);
            this.pnlKpis.Margin = new System.Windows.Forms.Padding(0);
            this.pnlKpis.Name = "pnlKpis";
            this.pnlKpis.RowCount = 1;
            this.pnlKpis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKpis.Size = new System.Drawing.Size(1132, 125);
            this.pnlKpis.TabIndex = 1;
            // 
            // pnlKpiIngresos
            // 
            this.pnlKpiIngresos.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlKpiIngresos.BorderRadius = 14;
            this.pnlKpiIngresos.BorderThickness = 1;
            this.pnlKpiIngresos.Controls.Add(this.layoutKpiIngresos);
            this.pnlKpiIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiIngresos.FillColor = System.Drawing.Color.White;
            this.pnlKpiIngresos.Location = new System.Drawing.Point(5, 5);
            this.pnlKpiIngresos.Margin = new System.Windows.Forms.Padding(5, 5, 8, 10);
            this.pnlKpiIngresos.Name = "pnlKpiIngresos";
            this.pnlKpiIngresos.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlKpiIngresos.Size = new System.Drawing.Size(364, 110);
            this.pnlKpiIngresos.TabIndex = 0;
            // 
            // layoutKpiIngresos
            // 
            this.layoutKpiIngresos.BackColor = System.Drawing.Color.White;
            this.layoutKpiIngresos.ColumnCount = 1;
            this.layoutKpiIngresos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiIngresos.Controls.Add(this.lblKpiIngresosTitulo, 0, 0);
            this.layoutKpiIngresos.Controls.Add(this.lblKpiIngresosValor, 0, 1);
            this.layoutKpiIngresos.Controls.Add(this.lblKpiIngresosDetalle, 0, 2);
            this.layoutKpiIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutKpiIngresos.Location = new System.Drawing.Point(20, 12);
            this.layoutKpiIngresos.Name = "layoutKpiIngresos";
            this.layoutKpiIngresos.RowCount = 3;
            this.layoutKpiIngresos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutKpiIngresos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiIngresos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.layoutKpiIngresos.Size = new System.Drawing.Size(324, 86);
            this.layoutKpiIngresos.TabIndex = 0;
            // 
            // lblKpiIngresosTitulo
            // 
            this.lblKpiIngresosTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiIngresosTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiIngresosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(96)))), ((int)(((byte)(126)))));
            this.lblKpiIngresosTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblKpiIngresosTitulo.Name = "lblKpiIngresosTitulo";
            this.lblKpiIngresosTitulo.Size = new System.Drawing.Size(318, 24);
            this.lblKpiIngresosTitulo.TabIndex = 0;
            this.lblKpiIngresosTitulo.Text = "INGRESOS DEL AÑO";
            // 
            // lblKpiIngresosValor
            // 
            this.lblKpiIngresosValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiIngresosValor.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblKpiIngresosValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(43)))), ((int)(((byte)(91)))));
            this.lblKpiIngresosValor.Location = new System.Drawing.Point(3, 24);
            this.lblKpiIngresosValor.Name = "lblKpiIngresosValor";
            this.lblKpiIngresosValor.Size = new System.Drawing.Size(318, 40);
            this.lblKpiIngresosValor.TabIndex = 1;
            this.lblKpiIngresosValor.Text = "—";
            this.lblKpiIngresosValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpiIngresosDetalle
            // 
            this.lblKpiIngresosDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiIngresosDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(132)))), ((int)(((byte)(160)))));
            this.lblKpiIngresosDetalle.Location = new System.Drawing.Point(3, 64);
            this.lblKpiIngresosDetalle.Name = "lblKpiIngresosDetalle";
            this.lblKpiIngresosDetalle.Size = new System.Drawing.Size(318, 22);
            this.lblKpiIngresosDetalle.TabIndex = 2;
            this.lblKpiIngresosDetalle.Text = "Total acumulado";
            // 
            // pnlKpiMejorMes
            // 
            this.pnlKpiMejorMes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlKpiMejorMes.BorderRadius = 14;
            this.pnlKpiMejorMes.BorderThickness = 1;
            this.pnlKpiMejorMes.Controls.Add(this.layoutKpiMejorMes);
            this.pnlKpiMejorMes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiMejorMes.FillColor = System.Drawing.Color.White;
            this.pnlKpiMejorMes.Location = new System.Drawing.Point(385, 5);
            this.pnlKpiMejorMes.Margin = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.pnlKpiMejorMes.Name = "pnlKpiMejorMes";
            this.pnlKpiMejorMes.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlKpiMejorMes.Size = new System.Drawing.Size(361, 110);
            this.pnlKpiMejorMes.TabIndex = 1;
            // 
            // layoutKpiMejorMes
            // 
            this.layoutKpiMejorMes.BackColor = System.Drawing.Color.White;
            this.layoutKpiMejorMes.ColumnCount = 1;
            this.layoutKpiMejorMes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiMejorMes.Controls.Add(this.lblKpiMejorMesTitulo, 0, 0);
            this.layoutKpiMejorMes.Controls.Add(this.lblKpiMejorMesValor, 0, 1);
            this.layoutKpiMejorMes.Controls.Add(this.lblKpiMejorMesDetalle, 0, 2);
            this.layoutKpiMejorMes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutKpiMejorMes.Location = new System.Drawing.Point(20, 12);
            this.layoutKpiMejorMes.Name = "layoutKpiMejorMes";
            this.layoutKpiMejorMes.RowCount = 3;
            this.layoutKpiMejorMes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutKpiMejorMes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiMejorMes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.layoutKpiMejorMes.Size = new System.Drawing.Size(321, 86);
            this.layoutKpiMejorMes.TabIndex = 0;
            // 
            // lblKpiMejorMesTitulo
            // 
            this.lblKpiMejorMesTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiMejorMesTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiMejorMesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(96)))), ((int)(((byte)(126)))));
            this.lblKpiMejorMesTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblKpiMejorMesTitulo.Name = "lblKpiMejorMesTitulo";
            this.lblKpiMejorMesTitulo.Size = new System.Drawing.Size(315, 24);
            this.lblKpiMejorMesTitulo.TabIndex = 0;
            this.lblKpiMejorMesTitulo.Text = "MEJOR MES";
            // 
            // lblKpiMejorMesValor
            // 
            this.lblKpiMejorMesValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiMejorMesValor.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblKpiMejorMesValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(43)))), ((int)(((byte)(91)))));
            this.lblKpiMejorMesValor.Location = new System.Drawing.Point(3, 24);
            this.lblKpiMejorMesValor.Name = "lblKpiMejorMesValor";
            this.lblKpiMejorMesValor.Size = new System.Drawing.Size(315, 40);
            this.lblKpiMejorMesValor.TabIndex = 1;
            this.lblKpiMejorMesValor.Text = "—";
            this.lblKpiMejorMesValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpiMejorMesDetalle
            // 
            this.lblKpiMejorMesDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiMejorMesDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(132)))), ((int)(((byte)(160)))));
            this.lblKpiMejorMesDetalle.Location = new System.Drawing.Point(3, 64);
            this.lblKpiMejorMesDetalle.Name = "lblKpiMejorMesDetalle";
            this.lblKpiMejorMesDetalle.Size = new System.Drawing.Size(315, 22);
            this.lblKpiMejorMesDetalle.TabIndex = 2;
            this.lblKpiMejorMesDetalle.Text = "—";
            // 
            // pnlKpiCentro
            // 
            this.pnlKpiCentro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlKpiCentro.BorderRadius = 14;
            this.pnlKpiCentro.BorderThickness = 1;
            this.pnlKpiCentro.Controls.Add(this.layoutKpiCentro);
            this.pnlKpiCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiCentro.FillColor = System.Drawing.Color.White;
            this.pnlKpiCentro.Location = new System.Drawing.Point(762, 5);
            this.pnlKpiCentro.Margin = new System.Windows.Forms.Padding(8, 5, 5, 10);
            this.pnlKpiCentro.Name = "pnlKpiCentro";
            this.pnlKpiCentro.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlKpiCentro.Size = new System.Drawing.Size(365, 110);
            this.pnlKpiCentro.TabIndex = 2;
            // 
            // layoutKpiCentro
            // 
            this.layoutKpiCentro.BackColor = System.Drawing.Color.White;
            this.layoutKpiCentro.ColumnCount = 1;
            this.layoutKpiCentro.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiCentro.Controls.Add(this.lblKpiCentroTitulo, 0, 0);
            this.layoutKpiCentro.Controls.Add(this.lblKpiCentroValor, 0, 1);
            this.layoutKpiCentro.Controls.Add(this.lblKpiCentroDetalle, 0, 2);
            this.layoutKpiCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutKpiCentro.Location = new System.Drawing.Point(20, 12);
            this.layoutKpiCentro.Name = "layoutKpiCentro";
            this.layoutKpiCentro.RowCount = 3;
            this.layoutKpiCentro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layoutKpiCentro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutKpiCentro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.layoutKpiCentro.Size = new System.Drawing.Size(325, 86);
            this.layoutKpiCentro.TabIndex = 0;
            // 
            // lblKpiCentroTitulo
            // 
            this.lblKpiCentroTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiCentroTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiCentroTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(96)))), ((int)(((byte)(126)))));
            this.lblKpiCentroTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblKpiCentroTitulo.Name = "lblKpiCentroTitulo";
            this.lblKpiCentroTitulo.Size = new System.Drawing.Size(319, 24);
            this.lblKpiCentroTitulo.TabIndex = 0;
            this.lblKpiCentroTitulo.Text = "PRINCIPAL CENTRO";
            // 
            // lblKpiCentroValor
            // 
            this.lblKpiCentroValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiCentroValor.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblKpiCentroValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(43)))), ((int)(((byte)(91)))));
            this.lblKpiCentroValor.Location = new System.Drawing.Point(3, 24);
            this.lblKpiCentroValor.Name = "lblKpiCentroValor";
            this.lblKpiCentroValor.Size = new System.Drawing.Size(319, 40);
            this.lblKpiCentroValor.TabIndex = 1;
            this.lblKpiCentroValor.Text = "—";
            this.lblKpiCentroValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpiCentroDetalle
            // 
            this.lblKpiCentroDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiCentroDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(132)))), ((int)(((byte)(160)))));
            this.lblKpiCentroDetalle.Location = new System.Drawing.Point(3, 64);
            this.lblKpiCentroDetalle.Name = "lblKpiCentroDetalle";
            this.lblKpiCentroDetalle.Size = new System.Drawing.Size(319, 22);
            this.lblKpiCentroDetalle.TabIndex = 2;
            this.lblKpiCentroDetalle.Text = "—";
            // 
            // tarjetas
            // 
            this.tarjetas.BackColor = System.Drawing.Color.Transparent;
            this.tarjetas.ColumnCount = 2;
            this.tarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tarjetas.Controls.Add(this.pnlTarjetaMeses, 0, 0);
            this.tarjetas.Controls.Add(this.pnlTarjetaGraficaMeses, 1, 0);
            this.tarjetas.Controls.Add(this.pnlTarjetaCentros, 0, 1);
            this.tarjetas.Controls.Add(this.pnlTarjetaGraficaCentros, 1, 1);
            this.tarjetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tarjetas.Location = new System.Drawing.Point(0, 240);
            this.tarjetas.Margin = new System.Windows.Forms.Padding(0);
            this.tarjetas.Name = "tarjetas";
            this.tarjetas.RowCount = 2;
            this.tarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tarjetas.Size = new System.Drawing.Size(1132, 498);
            this.tarjetas.TabIndex = 2;
            // 
            // pnlTarjetaMeses
            // 
            this.pnlTarjetaMeses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlTarjetaMeses.BorderRadius = 14;
            this.pnlTarjetaMeses.BorderThickness = 1;
            this.pnlTarjetaMeses.Controls.Add(this.layoutMeses);
            this.pnlTarjetaMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaMeses.FillColor = System.Drawing.Color.White;
            this.pnlTarjetaMeses.Location = new System.Drawing.Point(5, 5);
            this.pnlTarjetaMeses.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTarjetaMeses.Name = "pnlTarjetaMeses";
            this.pnlTarjetaMeses.Padding = new System.Windows.Forms.Padding(16);
            this.pnlTarjetaMeses.Size = new System.Drawing.Size(397, 239);
            this.pnlTarjetaMeses.TabIndex = 0;
            // 
            // layoutMeses
            // 
            this.layoutMeses.BackColor = System.Drawing.Color.White;
            this.layoutMeses.ColumnCount = 1;
            this.layoutMeses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMeses.Controls.Add(this.lblTituloMeses, 0, 0);
            this.layoutMeses.Controls.Add(this.dgvMeses, 0, 1);
            this.layoutMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMeses.Location = new System.Drawing.Point(16, 16);
            this.layoutMeses.Name = "layoutMeses";
            this.layoutMeses.RowCount = 2;
            this.layoutMeses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layoutMeses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutMeses.Size = new System.Drawing.Size(365, 207);
            this.layoutMeses.TabIndex = 0;
            // 
            // lblTituloMeses
            // 
            this.lblTituloMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloMeses.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloMeses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.lblTituloMeses.Location = new System.Drawing.Point(3, 0);
            this.lblTituloMeses.Name = "lblTituloMeses";
            this.lblTituloMeses.Size = new System.Drawing.Size(359, 36);
            this.lblTituloMeses.TabIndex = 0;
            this.lblTituloMeses.Text = "Ingresos mensuales";
            this.lblTituloMeses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvMeses
            // 
            this.dgvMeses.AllowUserToAddRows = false;
            this.dgvMeses.AllowUserToDeleteRows = false;
            this.dgvMeses.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvMeses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMeses.ColumnHeadersHeight = 30;
            this.dgvMeses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMeses.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMeses.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMeses.Location = new System.Drawing.Point(3, 39);
            this.dgvMeses.MultiSelect = false;
            this.dgvMeses.Name = "dgvMeses";
            this.dgvMeses.ReadOnly = true;
            this.dgvMeses.RowHeadersVisible = false;
            this.dgvMeses.RowTemplate.Height = 26;
            this.dgvMeses.Size = new System.Drawing.Size(359, 165);
            this.dgvMeses.TabIndex = 1;
            this.dgvMeses.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.dgvMeses.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvMeses.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvMeses.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvMeses.ThemeStyle.ReadOnly = true;
            this.dgvMeses.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvMeses.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvMeses.ThemeStyle.RowsStyle.Height = 26;
            this.dgvMeses.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgvMeses.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            // 
            // pnlTarjetaGraficaMeses
            // 
            this.pnlTarjetaGraficaMeses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlTarjetaGraficaMeses.BorderRadius = 14;
            this.pnlTarjetaGraficaMeses.BorderThickness = 1;
            this.pnlTarjetaGraficaMeses.Controls.Add(this.layoutGraficaMeses);
            this.pnlTarjetaGraficaMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaGraficaMeses.FillColor = System.Drawing.Color.White;
            this.pnlTarjetaGraficaMeses.Location = new System.Drawing.Point(412, 5);
            this.pnlTarjetaGraficaMeses.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTarjetaGraficaMeses.Name = "pnlTarjetaGraficaMeses";
            this.pnlTarjetaGraficaMeses.Padding = new System.Windows.Forms.Padding(16);
            this.pnlTarjetaGraficaMeses.Size = new System.Drawing.Size(715, 239);
            this.pnlTarjetaGraficaMeses.TabIndex = 1;
            // 
            // layoutGraficaMeses
            // 
            this.layoutGraficaMeses.BackColor = System.Drawing.Color.White;
            this.layoutGraficaMeses.ColumnCount = 1;
            this.layoutGraficaMeses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGraficaMeses.Controls.Add(this.lblTituloGraficaMeses, 0, 0);
            this.layoutGraficaMeses.Controls.Add(this.chartMeses, 0, 1);
            this.layoutGraficaMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutGraficaMeses.Location = new System.Drawing.Point(16, 16);
            this.layoutGraficaMeses.Name = "layoutGraficaMeses";
            this.layoutGraficaMeses.RowCount = 2;
            this.layoutGraficaMeses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layoutGraficaMeses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGraficaMeses.Size = new System.Drawing.Size(683, 207);
            this.layoutGraficaMeses.TabIndex = 0;
            // 
            // lblTituloGraficaMeses
            // 
            this.lblTituloGraficaMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloGraficaMeses.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloGraficaMeses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.lblTituloGraficaMeses.Location = new System.Drawing.Point(3, 0);
            this.lblTituloGraficaMeses.Name = "lblTituloGraficaMeses";
            this.lblTituloGraficaMeses.Size = new System.Drawing.Size(677, 36);
            this.lblTituloGraficaMeses.TabIndex = 0;
            this.lblTituloGraficaMeses.Text = "Evolución mensual";
            this.lblTituloGraficaMeses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartMeses
            // 
            chartArea1.Name = "Ventas";
            this.chartMeses.ChartAreas.Add(chartArea1);
            this.chartMeses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMeses.Location = new System.Drawing.Point(3, 39);
            this.chartMeses.Name = "chartMeses";
            series1.ChartArea = "Ventas";
            series1.IsVisibleInLegend = false;
            series1.Name = "Ventas";
            this.chartMeses.Series.Add(series1);
            this.chartMeses.Size = new System.Drawing.Size(677, 165);
            this.chartMeses.TabIndex = 1;
            // 
            // pnlTarjetaCentros
            // 
            this.pnlTarjetaCentros.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlTarjetaCentros.BorderRadius = 14;
            this.pnlTarjetaCentros.BorderThickness = 1;
            this.pnlTarjetaCentros.Controls.Add(this.layoutCentros);
            this.pnlTarjetaCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaCentros.FillColor = System.Drawing.Color.White;
            this.pnlTarjetaCentros.Location = new System.Drawing.Point(5, 254);
            this.pnlTarjetaCentros.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTarjetaCentros.Name = "pnlTarjetaCentros";
            this.pnlTarjetaCentros.Padding = new System.Windows.Forms.Padding(16);
            this.pnlTarjetaCentros.Size = new System.Drawing.Size(397, 239);
            this.pnlTarjetaCentros.TabIndex = 2;
            // 
            // layoutCentros
            // 
            this.layoutCentros.BackColor = System.Drawing.Color.White;
            this.layoutCentros.ColumnCount = 1;
            this.layoutCentros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCentros.Controls.Add(this.lblTituloCentros, 0, 0);
            this.layoutCentros.Controls.Add(this.dgvCentros, 0, 1);
            this.layoutCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCentros.Location = new System.Drawing.Point(16, 16);
            this.layoutCentros.Name = "layoutCentros";
            this.layoutCentros.RowCount = 2;
            this.layoutCentros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layoutCentros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCentros.Size = new System.Drawing.Size(365, 207);
            this.layoutCentros.TabIndex = 0;
            // 
            // lblTituloCentros
            // 
            this.lblTituloCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloCentros.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloCentros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.lblTituloCentros.Location = new System.Drawing.Point(3, 0);
            this.lblTituloCentros.Name = "lblTituloCentros";
            this.lblTituloCentros.Size = new System.Drawing.Size(359, 36);
            this.lblTituloCentros.TabIndex = 0;
            this.lblTituloCentros.Text = "Ingresos por centro de costos";
            this.lblTituloCentros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCentros
            // 
            this.dgvCentros.AllowUserToAddRows = false;
            this.dgvCentros.AllowUserToDeleteRows = false;
            this.dgvCentros.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvCentros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCentros.ColumnHeadersHeight = 30;
            this.dgvCentros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCentros.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCentros.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCentros.Location = new System.Drawing.Point(3, 39);
            this.dgvCentros.MultiSelect = false;
            this.dgvCentros.Name = "dgvCentros";
            this.dgvCentros.ReadOnly = true;
            this.dgvCentros.RowHeadersVisible = false;
            this.dgvCentros.RowTemplate.Height = 26;
            this.dgvCentros.Size = new System.Drawing.Size(359, 165);
            this.dgvCentros.TabIndex = 1;
            this.dgvCentros.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
            this.dgvCentros.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvCentros.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvCentros.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvCentros.ThemeStyle.ReadOnly = true;
            this.dgvCentros.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvCentros.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.dgvCentros.ThemeStyle.RowsStyle.Height = 26;
            this.dgvCentros.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgvCentros.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.dgvCentros.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCentros_CellDoubleClick);
            // 
            // pnlTarjetaGraficaCentros
            // 
            this.pnlTarjetaGraficaCentros.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(235)))));
            this.pnlTarjetaGraficaCentros.BorderRadius = 14;
            this.pnlTarjetaGraficaCentros.BorderThickness = 1;
            this.pnlTarjetaGraficaCentros.Controls.Add(this.layoutGraficaCentros);
            this.pnlTarjetaGraficaCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTarjetaGraficaCentros.FillColor = System.Drawing.Color.White;
            this.pnlTarjetaGraficaCentros.Location = new System.Drawing.Point(412, 254);
            this.pnlTarjetaGraficaCentros.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTarjetaGraficaCentros.Name = "pnlTarjetaGraficaCentros";
            this.pnlTarjetaGraficaCentros.Padding = new System.Windows.Forms.Padding(16);
            this.pnlTarjetaGraficaCentros.Size = new System.Drawing.Size(715, 239);
            this.pnlTarjetaGraficaCentros.TabIndex = 3;
            // 
            // layoutGraficaCentros
            // 
            this.layoutGraficaCentros.BackColor = System.Drawing.Color.White;
            this.layoutGraficaCentros.ColumnCount = 1;
            this.layoutGraficaCentros.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGraficaCentros.Controls.Add(this.lblTituloGraficaCentros, 0, 0);
            this.layoutGraficaCentros.Controls.Add(this.pnlGraficaCentros, 0, 1);
            this.layoutGraficaCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutGraficaCentros.Location = new System.Drawing.Point(16, 16);
            this.layoutGraficaCentros.Name = "layoutGraficaCentros";
            this.layoutGraficaCentros.RowCount = 2;
            this.layoutGraficaCentros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layoutGraficaCentros.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGraficaCentros.Size = new System.Drawing.Size(683, 207);
            this.layoutGraficaCentros.TabIndex = 0;
            // 
            // lblTituloGraficaCentros
            // 
            this.lblTituloGraficaCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloGraficaCentros.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloGraficaCentros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(48)))), ((int)(((byte)(87)))));
            this.lblTituloGraficaCentros.Location = new System.Drawing.Point(3, 0);
            this.lblTituloGraficaCentros.Name = "lblTituloGraficaCentros";
            this.lblTituloGraficaCentros.Size = new System.Drawing.Size(677, 36);
            this.lblTituloGraficaCentros.TabIndex = 0;
            this.lblTituloGraficaCentros.Text = "Distribución por centro de costos";
            this.lblTituloGraficaCentros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlGraficaCentros
            // 
            this.pnlGraficaCentros.AutoScroll = true;
            this.pnlGraficaCentros.BackColor = System.Drawing.Color.White;
            this.pnlGraficaCentros.Controls.Add(this.chartCentros);
            this.pnlGraficaCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGraficaCentros.Location = new System.Drawing.Point(3, 39);
            this.pnlGraficaCentros.Name = "pnlGraficaCentros";
            this.pnlGraficaCentros.Size = new System.Drawing.Size(677, 165);
            this.pnlGraficaCentros.TabIndex = 1;
            // 
            // chartCentros
            // 
            chartArea2.AxisX.IsReversed = true;
            chartArea2.Name = "Ventas";
            this.chartCentros.ChartAreas.Add(chartArea2);
            this.chartCentros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCentros.Location = new System.Drawing.Point(0, 0);
            this.chartCentros.Name = "chartCentros";
            series2.ChartArea = "Ventas";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series2.IsVisibleInLegend = false;
            series2.Name = "Ventas";
            this.chartCentros.Series.Add(series2);
            this.chartCentros.Size = new System.Drawing.Size(677, 165);
            this.chartCentros.TabIndex = 0;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(114)))), ((int)(((byte)(139)))));
            this.lblEstado.Location = new System.Drawing.Point(5, 738);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(1122, 34);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DashboardVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1180, 820);
            this.Controls.Add(this.principal);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1080, 750);
            this.Name = "DashboardVentas";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dashboard de Ventas";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DashboardVentas_FormClosed);
            this.Shown += new System.EventHandler(this.DashboardVentas_Shown);
            this.principal.ResumeLayout(false);
            this.encabezado.ResumeLayout(false);
            this.pnlCabeceraTitulo.ResumeLayout(false);
            this.layoutCabeceraTitulo.ResumeLayout(false);
            this.pnlCabeceraFiltros.ResumeLayout(false);
            this.layoutCabeceraFiltros.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlKpis.ResumeLayout(false);
            this.pnlKpiIngresos.ResumeLayout(false);
            this.layoutKpiIngresos.ResumeLayout(false);
            this.pnlKpiMejorMes.ResumeLayout(false);
            this.layoutKpiMejorMes.ResumeLayout(false);
            this.pnlKpiCentro.ResumeLayout(false);
            this.layoutKpiCentro.ResumeLayout(false);
            this.tarjetas.ResumeLayout(false);
            this.pnlTarjetaMeses.ResumeLayout(false);
            this.layoutMeses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeses)).EndInit();
            this.pnlTarjetaGraficaMeses.ResumeLayout(false);
            this.layoutGraficaMeses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMeses)).EndInit();
            this.pnlTarjetaCentros.ResumeLayout(false);
            this.layoutCentros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCentros)).EndInit();
            this.pnlTarjetaGraficaCentros.ResumeLayout(false);
            this.layoutGraficaCentros.ResumeLayout(false);
            this.pnlGraficaCentros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCentros)).EndInit();
            this.ResumeLayout(false);
            //this.dgvCentros.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCentros_CellDoubleClick);
            this.dgvCentros.Cursor = System.Windows.Forms.Cursors.Hand;
        }

    }
}