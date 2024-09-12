namespace PV
{
    partial class ReporteConceptoIngresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteConceptoIngresos));
            this.ConceptosIngresoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet10 = new PV.ControlCondominiosDataSet10();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ConceptosIngresoTableAdapter = new PV.ControlCondominiosDataSet10TableAdapters.ConceptosIngresoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ConceptosIngresoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet10)).BeginInit();
            this.SuspendLayout();
            // 
            // ConceptosIngresoBindingSource
            // 
            this.ConceptosIngresoBindingSource.DataMember = "ConceptosIngreso";
            this.ConceptosIngresoBindingSource.DataSource = this.ControlCondominiosDataSet10;
            // 
            // ControlCondominiosDataSet10
            // 
            this.ControlCondominiosDataSet10.DataSetName = "ControlCondominiosDataSet10";
            this.ControlCondominiosDataSet10.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ConceptosIngresoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteConceptoIngresos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1311, 736);
            this.reportViewer1.TabIndex = 0;
            // 
            // ConceptosIngresoTableAdapter
            // 
            this.ConceptosIngresoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteConceptoIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 736);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteConceptoIngresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Concepto de Ingresos";
            this.Load += new System.EventHandler(this.ReporteConceptoIngresos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConceptosIngresoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ConceptosIngresoBindingSource;
        private ControlCondominiosDataSet10 ControlCondominiosDataSet10;
        private ControlCondominiosDataSet10TableAdapters.ConceptosIngresoTableAdapter ConceptosIngresoTableAdapter;
    }
}