namespace PV
{
    partial class ReporteTipoMovimento
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteTipoMovimento));
            this.TipoMovimientoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet28 = new PV.ControlCondominiosDataSet28();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TipoMovimientoTableAdapter = new PV.ControlCondominiosDataSet28TableAdapters.TipoMovimientoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.TipoMovimientoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet28)).BeginInit();
            this.SuspendLayout();
            // 
            // TipoMovimientoBindingSource
            // 
            this.TipoMovimientoBindingSource.DataMember = "TipoMovimiento";
            this.TipoMovimientoBindingSource.DataSource = this.ControlCondominiosDataSet28;
            // 
            // ControlCondominiosDataSet28
            // 
            this.ControlCondominiosDataSet28.DataSetName = "ControlCondominiosDataSet28";
            this.ControlCondominiosDataSet28.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.TipoMovimientoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteTipoMovimientos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1023, 677);
            this.reportViewer1.TabIndex = 0;
            // 
            // TipoMovimientoTableAdapter
            // 
            this.TipoMovimientoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteTipoMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 677);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteTipoMovimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Tipo de Movimento";
            this.Load += new System.EventHandler(this.ReporteTipoMovimento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TipoMovimientoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet28)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TipoMovimientoBindingSource;
        private ControlCondominiosDataSet28 ControlCondominiosDataSet28;
        private ControlCondominiosDataSet28TableAdapters.TipoMovimientoTableAdapter TipoMovimientoTableAdapter;
    }
}