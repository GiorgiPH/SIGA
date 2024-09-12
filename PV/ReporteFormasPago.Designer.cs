namespace PV
{
    partial class ReporteFormasPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteFormasPago));
            this.FormasPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet = new PV.ControlCondominiosDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FormasPagoTableAdapter = new PV.ControlCondominiosDataSetTableAdapters.FormasPagoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FormasPagoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // FormasPagoBindingSource
            // 
            this.FormasPagoBindingSource.DataMember = "FormasPago";
            this.FormasPagoBindingSource.DataSource = this.ControlCondominiosDataSet;
            // 
            // ControlCondominiosDataSet
            // 
            this.ControlCondominiosDataSet.DataSetName = "ControlCondominiosDataSet";
            this.ControlCondominiosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.FormasPagoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteFormasPago.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1175, 597);
            this.reportViewer1.TabIndex = 0;
            // 
            // FormasPagoTableAdapter
            // 
            this.FormasPagoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteFormasPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 597);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteFormasPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Formas de Pago";
            this.Load += new System.EventHandler(this.ReporteFormasPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FormasPagoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FormasPagoBindingSource;
        private ControlCondominiosDataSet ControlCondominiosDataSet;
        private ControlCondominiosDataSetTableAdapters.FormasPagoTableAdapter FormasPagoTableAdapter;
    }
}