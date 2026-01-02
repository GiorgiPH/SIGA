
namespace PV
{
    partial class PolizaCompras1
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
            this.DatosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.ReporteGeneracionPolizasComprasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DTSPolizaCompra1 = new PV.DTSPolizaCompra1();
            this.Torre = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.ReporteGeneracionPolizasComprasTableAdapter = new PV.DTSPolizaCompra1TableAdapters.ReporteGeneracionPolizasComprasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteGeneracionPolizasComprasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTSPolizaCompra1)).BeginInit();
            this.SuspendLayout();
            // 
            // DatosEmpresaBindingSource
            // 
            this.DatosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.DatosEmpresaBindingSource.DataSource = this.ControlCondominiosDataSet23;
            // 
            // ControlCondominiosDataSet23
            // 
            this.ControlCondominiosDataSet23.DataSetName = "ControlCondominiosDataSet23";
            this.ControlCondominiosDataSet23.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReporteGeneracionPolizasComprasBindingSource
            // 
            this.ReporteGeneracionPolizasComprasBindingSource.DataMember = "ReporteGeneracionPolizasCompras";
            this.ReporteGeneracionPolizasComprasBindingSource.DataSource = this.DTSPolizaCompra1;
            // 
            // DTSPolizaCompra1
            // 
            this.DTSPolizaCompra1.DataSetName = "DTSPolizaCompra1";
            this.DTSPolizaCompra1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Torre
            // 
            this.Torre.AutoSize = true;
            this.Torre.Location = new System.Drawing.Point(657, 13);
            this.Torre.Name = "Torre";
            this.Torre.Size = new System.Drawing.Size(11, 13);
            this.Torre.TabIndex = 1;
            this.Torre.Text = "*";
            this.Torre.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DatosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.ReporteGeneracionPolizasComprasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.PolizaCompra1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1184, 749);
            this.reportViewer1.TabIndex = 2;
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteGeneracionPolizasComprasTableAdapter
            // 
            this.ReporteGeneracionPolizasComprasTableAdapter.ClearBeforeFill = true;
            // 
            // PolizaCompras1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 749);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.Torre);
            this.Name = "PolizaCompras1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poliza Compras";
            this.Load += new System.EventHandler(this.PolizaCompras1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteGeneracionPolizasComprasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTSPolizaCompra1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Torre;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet23 ControlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource ReporteGeneracionPolizasComprasBindingSource;
        private DTSPolizaCompra1 DTSPolizaCompra1;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private DTSPolizaCompra1TableAdapters.ReporteGeneracionPolizasComprasTableAdapter ReporteGeneracionPolizasComprasTableAdapter;
    }
}