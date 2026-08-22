namespace PV
{
    partial class ReporteConceptosCobroPago
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
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.spConceptosCobroPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dTSConceptosCobroPago = new PV.DTSConceptosCobroPago();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.sp_ConceptosCobroPagoTableAdapter = new PV.DTSConceptosCobroPagoTableAdapters.sp_ConceptosCobroPagoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spConceptosCobroPagoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSConceptosCobroPago)).BeginInit();
            this.SuspendLayout();
            // 
            // datosEmpresaBindingSource
            // 
            this.datosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.datosEmpresaBindingSource.DataSource = this.controlCondominiosDataSet23;
            // 
            // controlCondominiosDataSet23
            // 
            this.controlCondominiosDataSet23.DataSetName = "ControlCondominiosDataSet23";
            this.controlCondominiosDataSet23.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spConceptosCobroPagoBindingSource
            // 
            this.spConceptosCobroPagoBindingSource.DataMember = "sp_ConceptosCobroPago";
            this.spConceptosCobroPagoBindingSource.DataSource = this.dTSConceptosCobroPago;
            // 
            // dTSConceptosCobroPago
            // 
            this.dTSConceptosCobroPago.DataSetName = "DTSConceptosCobroPago";
            this.dTSConceptosCobroPago.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.datosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.spConceptosCobroPagoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteConceptosCobroPago.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // datosEmpresaTableAdapter
            // 
            this.datosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // sp_ConceptosCobroPagoTableAdapter
            // 
            this.sp_ConceptosCobroPagoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteConceptosCobroPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteConceptosCobroPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReporteConceptosCobroPago";
            this.Load += new System.EventHandler(this.ReporteConceptosCobroPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spConceptosCobroPagoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSConceptosCobroPago)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private System.Windows.Forms.BindingSource spConceptosCobroPagoBindingSource;
        private DTSConceptosCobroPago dTSConceptosCobroPago;
        private DTSConceptosCobroPagoTableAdapters.sp_ConceptosCobroPagoTableAdapter sp_ConceptosCobroPagoTableAdapter;
    }
}