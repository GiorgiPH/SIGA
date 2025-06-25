namespace PV
{
    partial class GraficaMontoGastosxMes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.Charts.WinForms.ChartFont chartFont25 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont26 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont27 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont28 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid10 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick10 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont29 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid11 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick11 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont30 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid12 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel4 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont31 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick12 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont32 = new Guna.Charts.WinForms.ChartFont();
            this.gunaBarDataset1 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaChart1 = new Guna.Charts.WinForms.GunaChart();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFiltroAño = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // gunaBarDataset1
            // 
            this.gunaBarDataset1.Label = "Bar1";
            // 
            // gunaChart1
            // 
            chartFont25.FontName = "Arial";
            this.gunaChart1.Legend.LabelFont = chartFont25;
            this.gunaChart1.Location = new System.Drawing.Point(12, 12);
            this.gunaChart1.Name = "gunaChart1";
            this.gunaChart1.Size = new System.Drawing.Size(703, 426);
            this.gunaChart1.TabIndex = 0;
            chartFont26.FontName = "Arial";
            chartFont26.Size = 12;
            chartFont26.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.gunaChart1.Title.Font = chartFont26;
            chartFont27.FontName = "Arial";
            this.gunaChart1.Tooltips.BodyFont = chartFont27;
            chartFont28.FontName = "Arial";
            chartFont28.Size = 9;
            chartFont28.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.gunaChart1.Tooltips.TitleFont = chartFont28;
            this.gunaChart1.XAxes.GridLines = grid10;
            chartFont29.FontName = "Arial";
            tick10.Font = chartFont29;
            this.gunaChart1.XAxes.Ticks = tick10;
            this.gunaChart1.YAxes.GridLines = grid11;
            chartFont30.FontName = "Arial";
            tick11.Font = chartFont30;
            this.gunaChart1.YAxes.Ticks = tick11;
            this.gunaChart1.ZAxes.GridLines = grid12;
            chartFont31.FontName = "Arial";
            pointLabel4.Font = chartFont31;
            this.gunaChart1.ZAxes.PointLabels = pointLabel4;
            chartFont32.FontName = "Arial";
            tick12.Font = chartFont32;
            this.gunaChart1.ZAxes.Ticks = tick12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(729, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 87;
            this.label2.Text = "Filtro:";
            // 
            // dtFiltroAño
            // 
            this.dtFiltroAño.CustomFormat = "yyyy";
            this.dtFiltroAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFiltroAño.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFiltroAño.Location = new System.Drawing.Point(723, 67);
            this.dtFiltroAño.Name = "dtFiltroAño";
            this.dtFiltroAño.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtFiltroAño.ShowUpDown = true;
            this.dtFiltroAño.Size = new System.Drawing.Size(65, 20);
            this.dtFiltroAño.TabIndex = 86;
            this.dtFiltroAño.ValueChanged += new System.EventHandler(this.dtFiltroAño_ValueChanged);
            // 
            // GraficaMontoGastosxMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFiltroAño);
            this.Controls.Add(this.gunaChart1);
            this.Name = "GraficaMontoGastosxMes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GraficaMontoGastosxMes";
            this.Load += new System.EventHandler(this.GraficaMontoGastosxMes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset1;
        private Guna.Charts.WinForms.GunaChart gunaChart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFiltroAño;
    }
}