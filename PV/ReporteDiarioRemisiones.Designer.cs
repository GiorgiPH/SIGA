namespace PV
{
    partial class ReporteDiarioRemisiones
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.controlCondominiosDataSet60 = new PV.ControlCondominiosDataSet60();
            this.spReporteDiarioRemisionesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteDiarioRemisionesTableAdapter = new PV.ControlCondominiosDataSet60TableAdapters.sp_ReporteDiarioRemisionesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet60)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioRemisionesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.datosEmpresaBindingSource;
            reportDataSource6.Name = "DataSet2";
            reportDataSource6.Value = this.spReporteDiarioRemisionesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDiarioRemisiones.rdlc";
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
            // controlCondominiosDataSet60
            // 
            this.controlCondominiosDataSet60.DataSetName = "ControlCondominiosDataSet60";
            this.controlCondominiosDataSet60.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteDiarioRemisionesBindingSource
            // 
            this.spReporteDiarioRemisionesBindingSource.DataMember = "sp_ReporteDiarioRemisiones";
            this.spReporteDiarioRemisionesBindingSource.DataSource = this.controlCondominiosDataSet60;
            // 
            // sp_ReporteDiarioRemisionesTableAdapter
            // 
            this.sp_ReporteDiarioRemisionesTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteDiarioRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteDiarioRemisiones";
            this.Text = "ReporteDiarioRemisiones";
            this.Load += new System.EventHandler(this.ReporteDiarioRemisiones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet60)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioRemisionesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private System.Windows.Forms.BindingSource spReporteDiarioRemisionesBindingSource;
        private ControlCondominiosDataSet60 controlCondominiosDataSet60;
        private ControlCondominiosDataSet60TableAdapters.sp_ReporteDiarioRemisionesTableAdapter sp_ReporteDiarioRemisionesTableAdapter;
    }
}