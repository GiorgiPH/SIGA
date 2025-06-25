namespace PV
{
    partial class ReporteDiarioReembolsos
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.dTSReporteDiarioReembolsos = new PV.DTSReporteDiarioReembolsos();
            this.spReporteDiarioReembolsosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteDiarioReembolsosTableAdapter = new PV.DTSReporteDiarioReembolsosTableAdapters.sp_ReporteDiarioReembolsosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteDiarioReembolsos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioReembolsosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.datosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.spReporteDiarioReembolsosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDiarioReembolsos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // controlCondominiosDataSet23
            // 
            this.controlCondominiosDataSet23.DataSetName = "ControlCondominiosDataSet23";
            this.controlCondominiosDataSet23.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // datosEmpresaBindingSource
            // 
            this.datosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.datosEmpresaBindingSource.DataSource = this.controlCondominiosDataSet23;
            // 
            // datosEmpresaTableAdapter
            // 
            this.datosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // dTSReporteDiarioReembolsos
            // 
            this.dTSReporteDiarioReembolsos.DataSetName = "DTSReporteDiarioReembolsos";
            this.dTSReporteDiarioReembolsos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteDiarioReembolsosBindingSource
            // 
            this.spReporteDiarioReembolsosBindingSource.DataMember = "sp_ReporteDiarioReembolsos";
            this.spReporteDiarioReembolsosBindingSource.DataSource = this.dTSReporteDiarioReembolsos;
            // 
            // sp_ReporteDiarioReembolsosTableAdapter
            // 
            this.sp_ReporteDiarioReembolsosTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteDiarioReembolsos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteDiarioReembolsos";
            this.Text = "ReporteDiarioReembolsos";
            this.Load += new System.EventHandler(this.ReporteDiarioReembolsos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteDiarioReembolsos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioReembolsosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private System.Windows.Forms.BindingSource spReporteDiarioReembolsosBindingSource;
        private DTSReporteDiarioReembolsos dTSReporteDiarioReembolsos;
        private DTSReporteDiarioReembolsosTableAdapters.sp_ReporteDiarioReembolsosTableAdapter sp_ReporteDiarioReembolsosTableAdapter;
    }
}