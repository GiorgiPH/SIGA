namespace PV
{
    partial class ReporteDiarioIngresos
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
            this.controlCondominiosDataSet46 = new PV.ControlCondominiosDataSet46();
            this.spReporteDiarioIngresosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteDiarioIngresosTableAdapter = new PV.ControlCondominiosDataSet46TableAdapters.sp_ReporteDiarioIngresosTableAdapter();
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioIngresosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.datosEmpresaBindingSource;
            reportDataSource6.Name = "DataSet2";
            reportDataSource6.Value = this.spReporteDiarioIngresosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDiarioIngresos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // controlCondominiosDataSet46
            // 
            this.controlCondominiosDataSet46.DataSetName = "ControlCondominiosDataSet46";
            this.controlCondominiosDataSet46.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteDiarioIngresosBindingSource
            // 
            this.spReporteDiarioIngresosBindingSource.DataMember = "sp_ReporteDiarioIngresos";
            this.spReporteDiarioIngresosBindingSource.DataSource = this.controlCondominiosDataSet46;
            // 
            // sp_ReporteDiarioIngresosTableAdapter
            // 
            this.sp_ReporteDiarioIngresosTableAdapter.ClearBeforeFill = true;
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
            // ReporteDiarioIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteDiarioIngresos";
            this.Text = "ReporteDiarioIngresos";
            this.Load += new System.EventHandler(this.ReporteDiarioIngresos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteDiarioIngresosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ControlCondominiosDataSet46 controlCondominiosDataSet46;
        private System.Windows.Forms.BindingSource spReporteDiarioIngresosBindingSource;
        private ControlCondominiosDataSet46TableAdapters.sp_ReporteDiarioIngresosTableAdapter sp_ReporteDiarioIngresosTableAdapter;
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
    }
}