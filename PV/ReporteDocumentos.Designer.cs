namespace PV
{
    partial class ReporteDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteDocumentos));
            this.DocumentoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet11 = new PV.ControlCondominiosDataSet11();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DocumentoTableAdapter = new PV.ControlCondominiosDataSet11TableAdapters.DocumentoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // DocumentoBindingSource
            // 
            this.DocumentoBindingSource.DataMember = "Documento";
            this.DocumentoBindingSource.DataSource = this.ControlCondominiosDataSet11;
            // 
            // ControlCondominiosDataSet11
            // 
            this.ControlCondominiosDataSet11.DataSetName = "ControlCondominiosDataSet11";
            this.ControlCondominiosDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DocumentoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDocumentos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(909, 612);
            this.reportViewer1.TabIndex = 0;
            // 
            // DocumentoTableAdapter
            // 
            this.DocumentoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 612);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Documentos";
            this.Load += new System.EventHandler(this.ReporteDocumentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DocumentoBindingSource;
        private ControlCondominiosDataSet11 ControlCondominiosDataSet11;
        private ControlCondominiosDataSet11TableAdapters.DocumentoTableAdapter DocumentoTableAdapter;
    }
}