namespace PV
{
    partial class ReporteMovimientoInventario
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
            this.controlCondominiosDataSet31 = new PV.ControlCondominiosDataSet31();
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter();
            this.dTSReporteMovimientoInventario = new PV.DTSReporteMovimientoInventario();
            this.spReporteMovimientoInventarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteMovimientoInventarioTableAdapter = new PV.DTSReporteMovimientoInventarioTableAdapters.sp_ReporteMovimientoInventarioTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteMovimientoInventario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteMovimientoInventarioBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.datosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.spReporteMovimientoInventarioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteMovimientoInventario.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // controlCondominiosDataSet31
            // 
            this.controlCondominiosDataSet31.DataSetName = "ControlCondominiosDataSet31";
            this.controlCondominiosDataSet31.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // datosEmpresaBindingSource
            // 
            this.datosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.datosEmpresaBindingSource.DataSource = this.controlCondominiosDataSet31;
            // 
            // datosEmpresaTableAdapter
            // 
            this.datosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // dTSReporteMovimientoInventario
            // 
            this.dTSReporteMovimientoInventario.DataSetName = "DTSReporteMovimientoInventario";
            this.dTSReporteMovimientoInventario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteMovimientoInventarioBindingSource
            // 
            this.spReporteMovimientoInventarioBindingSource.DataMember = "sp_ReporteMovimientoInventario";
            this.spReporteMovimientoInventarioBindingSource.DataSource = this.dTSReporteMovimientoInventario;
            // 
            // sp_ReporteMovimientoInventarioTableAdapter
            // 
            this.sp_ReporteMovimientoInventarioTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteMovimientoInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteMovimientoInventario";
            this.Text = "ReporteMovimientoInventario";
            this.Load += new System.EventHandler(this.ReporteMovimientoInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteMovimientoInventario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteMovimientoInventarioBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ControlCondominiosDataSet31 controlCondominiosDataSet31;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private System.Windows.Forms.BindingSource spReporteMovimientoInventarioBindingSource;
        private DTSReporteMovimientoInventario dTSReporteMovimientoInventario;
        private DTSReporteMovimientoInventarioTableAdapters.sp_ReporteMovimientoInventarioTableAdapter sp_ReporteMovimientoInventarioTableAdapter;
    }
}