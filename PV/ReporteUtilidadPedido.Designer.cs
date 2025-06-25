namespace PV
{
    partial class ReporteUtilidadPedido
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
            this.cajaRDataSet = new PV.CajaRDataSet();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaTableAdapter = new PV.CajaRDataSetTableAdapters.EmpresaTableAdapter();
            this.dTSReporteOrdenesPedidoUtilidad = new PV.DTSReporteOrdenesPedidoUtilidad();
            this.spReporteOrdenesPedidoUtilidadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_ReporteOrdenesPedidoUtilidadTableAdapter = new PV.DTSReporteOrdenesPedidoUtilidadTableAdapters.sp_ReporteOrdenesPedidoUtilidadTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.cajaRDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteOrdenesPedidoUtilidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteOrdenesPedidoUtilidadBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.empresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.spReporteOrdenesPedidoUtilidadBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteUtilidadPedido.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // cajaRDataSet
            // 
            this.cajaRDataSet.DataSetName = "CajaRDataSet";
            this.cajaRDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // empresaBindingSource
            // 
            this.empresaBindingSource.DataMember = "Empresa";
            this.empresaBindingSource.DataSource = this.cajaRDataSet;
            // 
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // dTSReporteOrdenesPedidoUtilidad
            // 
            this.dTSReporteOrdenesPedidoUtilidad.DataSetName = "DTSReporteOrdenesPedidoUtilidad";
            this.dTSReporteOrdenesPedidoUtilidad.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spReporteOrdenesPedidoUtilidadBindingSource
            // 
            this.spReporteOrdenesPedidoUtilidadBindingSource.DataMember = "sp_ReporteOrdenesPedidoUtilidad";
            this.spReporteOrdenesPedidoUtilidadBindingSource.DataSource = this.dTSReporteOrdenesPedidoUtilidad;
            // 
            // sp_ReporteOrdenesPedidoUtilidadTableAdapter
            // 
            this.sp_ReporteOrdenesPedidoUtilidadTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteUtilidadPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteUtilidadPedido";
            this.Text = "ReporteUtilidadPedido";
            this.Load += new System.EventHandler(this.ReporteUtilidadPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cajaRDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTSReporteOrdenesPedidoUtilidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReporteOrdenesPedidoUtilidadBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private CajaRDataSet cajaRDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private CajaRDataSetTableAdapters.EmpresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource spReporteOrdenesPedidoUtilidadBindingSource;
        private DTSReporteOrdenesPedidoUtilidad dTSReporteOrdenesPedidoUtilidad;
        private DTSReporteOrdenesPedidoUtilidadTableAdapters.sp_ReporteOrdenesPedidoUtilidadTableAdapter sp_ReporteOrdenesPedidoUtilidadTableAdapter;
    }
}