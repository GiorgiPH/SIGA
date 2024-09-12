namespace PV
{
    partial class ReporteConceptosGlobales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteConceptosGlobales));
            this.ConceptosGlobalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet9 = new PV.ControlCondominiosDataSet9();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ConceptosGlobalesTableAdapter = new PV.ControlCondominiosDataSet9TableAdapters.ConceptosGlobalesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ConceptosGlobalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet9)).BeginInit();
            this.SuspendLayout();
            // 
            // ConceptosGlobalesBindingSource
            // 
            this.ConceptosGlobalesBindingSource.DataMember = "ConceptosGlobales";
            this.ConceptosGlobalesBindingSource.DataSource = this.ControlCondominiosDataSet9;
            // 
            // ControlCondominiosDataSet9
            // 
            this.ControlCondominiosDataSet9.DataSetName = "ControlCondominiosDataSet9";
            this.ControlCondominiosDataSet9.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ConceptosGlobalesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteConceptosGlobales.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(833, 585);
            this.reportViewer1.TabIndex = 0;
            // 
            // ConceptosGlobalesTableAdapter
            // 
            this.ConceptosGlobalesTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteConceptosGlobales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 585);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteConceptosGlobales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Conceptos de Globales";
            this.Load += new System.EventHandler(this.ReporteConceptosGlobales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConceptosGlobalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ConceptosGlobalesBindingSource;
        private ControlCondominiosDataSet9 ControlCondominiosDataSet9;
        private ControlCondominiosDataSet9TableAdapters.ConceptosGlobalesTableAdapter ConceptosGlobalesTableAdapter;
    }
}