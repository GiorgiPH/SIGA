namespace PV
{
    partial class ReporteOrdenCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteOrdenCompra));
            this.OrdenCompraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet27 = new PV.ControlCondominiosDataSet27();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.OrdenCompraBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet27)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdenCompraBindingSource
            // 
            this.OrdenCompraBindingSource.DataMember = "OrdenCompra";
            this.OrdenCompraBindingSource.DataSource = this.ControlCondominiosDataSet27;
            // 
            // ControlCondominiosDataSet27
            // 
            this.ControlCondominiosDataSet27.DataSetName = "ControlCondominiosDataSet27";
            this.ControlCondominiosDataSet27.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.OrdenCompraBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteOrdenCompra.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(992, 638);
            this.reportViewer1.TabIndex = 0;
            // 
            // OrdenCompraTableAdapter
            // 
            // 
            // ReporteOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 638);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteOrdenCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Orden de Compra";
            this.Load += new System.EventHandler(this.ReporteOrdenCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrdenCompraBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet27)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource OrdenCompraBindingSource;
        private ControlCondominiosDataSet27 ControlCondominiosDataSet27;
    }
}