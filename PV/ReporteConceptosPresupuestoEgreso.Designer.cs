namespace PV
{
    partial class ReporteConceptosPresupuestoEgreso
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
            this.ConceptoPresupuestoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet45 = new PV.ControlCondominiosDataSet45();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ConceptoPresupuestoTableAdapter = new PV.ControlCondominiosDataSet45TableAdapters.ConceptoPresupuestoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ConceptoPresupuestoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet45)).BeginInit();
            this.SuspendLayout();
            // 
            // ConceptoPresupuestoBindingSource
            // 
            this.ConceptoPresupuestoBindingSource.DataMember = "ConceptoPresupuesto";
            this.ConceptoPresupuestoBindingSource.DataSource = this.ControlCondominiosDataSet45;
            // 
            // ControlCondominiosDataSet45
            // 
            this.ControlCondominiosDataSet45.DataSetName = "ControlCondominiosDataSet45";
            this.ControlCondominiosDataSet45.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ConceptoPresupuestoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteConceptosPresupuestoEgresos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(899, 618);
            this.reportViewer1.TabIndex = 0;
            // 
            // ConceptoPresupuestoTableAdapter
            // 
            this.ConceptoPresupuestoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteConceptosPresupuestoEgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 618);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteConceptosPresupuestoEgreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Conceptos Presupuesto";
            this.Load += new System.EventHandler(this.ReporteConceptosPresupuestoEgreso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConceptoPresupuestoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet45)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ConceptoPresupuestoBindingSource;
        private ControlCondominiosDataSet45 ControlCondominiosDataSet45;
        private ControlCondominiosDataSet45TableAdapters.ConceptoPresupuestoTableAdapter ConceptoPresupuestoTableAdapter;
    }
}