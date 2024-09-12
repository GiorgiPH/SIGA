namespace PV
{
    partial class ReporteCondominios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteCondominios));
            this.CondominioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet2 = new PV.ControlCondominiosDataSet2();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CondominioTableAdapter = new PV.ControlCondominiosDataSet2TableAdapters.CondominioTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.CondominioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // CondominioBindingSource
            // 
            this.CondominioBindingSource.DataMember = "Condominio";
            this.CondominioBindingSource.DataSource = this.ControlCondominiosDataSet2;
            // 
            // ControlCondominiosDataSet2
            // 
            this.ControlCondominiosDataSet2.DataSetName = "ControlCondominiosDataSet2";
            this.ControlCondominiosDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CondominioBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteCondominios.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(967, 645);
            this.reportViewer1.TabIndex = 0;
            // 
            // CondominioTableAdapter
            // 
            this.CondominioTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteCondominios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 645);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteCondominios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Condominios";
            this.Load += new System.EventHandler(this.ReporteCondominios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CondominioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CondominioBindingSource;
        private ControlCondominiosDataSet2 ControlCondominiosDataSet2;
        private ControlCondominiosDataSet2TableAdapters.CondominioTableAdapter CondominioTableAdapter;
    }
}