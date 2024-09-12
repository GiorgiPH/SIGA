namespace PV
{
    partial class ReporteResguardoActivoFijo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteResguardoActivoFijo));
            this.ResguardoActivoFijoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet19 = new PV.ControlCondominiosDataSet19();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ResguardoActivoFijoTableAdapter = new PV.ControlCondominiosDataSet19TableAdapters.ResguardoActivoFijoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ResguardoActivoFijoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet19)).BeginInit();
            this.SuspendLayout();
            // 
            // ResguardoActivoFijoBindingSource
            // 
            this.ResguardoActivoFijoBindingSource.DataMember = "ResguardoActivoFijo";
            this.ResguardoActivoFijoBindingSource.DataSource = this.ControlCondominiosDataSet19;
            // 
            // ControlCondominiosDataSet19
            // 
            this.ControlCondominiosDataSet19.DataSetName = "ControlCondominiosDataSet19";
            this.ControlCondominiosDataSet19.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ResguardoActivoFijoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteResguardoActivoFijo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1315, 741);
            this.reportViewer1.TabIndex = 0;
            // 
            // ResguardoActivoFijoTableAdapter
            // 
            this.ResguardoActivoFijoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteResguardoActivoFijo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 741);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteResguardoActivoFijo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Resguardo de Activo Fijo";
            this.Load += new System.EventHandler(this.ReporteResguardoActivoFijo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResguardoActivoFijoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet19)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ResguardoActivoFijoBindingSource;
        private ControlCondominiosDataSet19 ControlCondominiosDataSet19;
        private ControlCondominiosDataSet19TableAdapters.ResguardoActivoFijoTableAdapter ResguardoActivoFijoTableAdapter;
    }
}