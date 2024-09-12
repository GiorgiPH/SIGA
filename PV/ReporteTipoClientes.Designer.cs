namespace PV
{
    partial class ReporteTipoClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteTipoClientes));
            this.TipoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet13 = new PV.ControlCondominiosDataSet13();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TipoClienteTableAdapter = new PV.ControlCondominiosDataSet13TableAdapters.TipoClienteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.TipoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet13)).BeginInit();
            this.SuspendLayout();
            // 
            // TipoClienteBindingSource
            // 
            this.TipoClienteBindingSource.DataMember = "TipoCliente";
            this.TipoClienteBindingSource.DataSource = this.ControlCondominiosDataSet13;
            // 
            // ControlCondominiosDataSet13
            // 
            this.ControlCondominiosDataSet13.DataSetName = "ControlCondominiosDataSet13";
            this.ControlCondominiosDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.TipoClienteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteTipoClientes.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(908, 590);
            this.reportViewer1.TabIndex = 0;
            // 
            // TipoClienteTableAdapter
            // 
            this.TipoClienteTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteTipoClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 590);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteTipoClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reportede Tipo de Clientes";
            this.Load += new System.EventHandler(this.ReporteTipoClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TipoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet13)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TipoClienteBindingSource;
        private ControlCondominiosDataSet13 ControlCondominiosDataSet13;
        private ControlCondominiosDataSet13TableAdapters.TipoClienteTableAdapter TipoClienteTableAdapter;
    }
}