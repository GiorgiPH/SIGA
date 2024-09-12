namespace PV
{
    partial class ReporteZonas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteZonas));
            this.ZonaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet14 = new PV.ControlCondominiosDataSet14();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ZonaTableAdapter = new PV.ControlCondominiosDataSet14TableAdapters.ZonaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ZonaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet14)).BeginInit();
            this.SuspendLayout();
            // 
            // ZonaBindingSource
            // 
            this.ZonaBindingSource.DataMember = "Zona";
            this.ZonaBindingSource.DataSource = this.ControlCondominiosDataSet14;
            // 
            // ControlCondominiosDataSet14
            // 
            this.ControlCondominiosDataSet14.DataSetName = "ControlCondominiosDataSet14";
            this.ControlCondominiosDataSet14.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ZonaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteZonas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(892, 556);
            this.reportViewer1.TabIndex = 0;
            // 
            // ZonaTableAdapter
            // 
            this.ZonaTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteZonas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 556);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteZonas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Zonas";
            this.Load += new System.EventHandler(this.ReporteZonas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ZonaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet14)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ZonaBindingSource;
        private ControlCondominiosDataSet14 ControlCondominiosDataSet14;
        private ControlCondominiosDataSet14TableAdapters.ZonaTableAdapter ZonaTableAdapter;
    }
}