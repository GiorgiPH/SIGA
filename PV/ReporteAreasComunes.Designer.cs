namespace PV
{
    partial class ReporteAreasComunes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteAreasComunes));
            this.AreasComunesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet1 = new PV.ControlCondominiosDataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AreasComunesTableAdapter = new PV.ControlCondominiosDataSet1TableAdapters.AreasComunesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.AreasComunesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // AreasComunesBindingSource
            // 
            this.AreasComunesBindingSource.DataMember = "AreasComunes";
            this.AreasComunesBindingSource.DataSource = this.ControlCondominiosDataSet1;
            // 
            // ControlCondominiosDataSet1
            // 
            this.ControlCondominiosDataSet1.DataSetName = "ControlCondominiosDataSet1";
            this.ControlCondominiosDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AreasComunesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteAreasComunes.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(991, 665);
            this.reportViewer1.TabIndex = 0;
            // 
            // AreasComunesTableAdapter
            // 
            this.AreasComunesTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteAreasComunes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 665);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteAreasComunes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Areas Comunes";
            this.Load += new System.EventHandler(this.ReporteAreasComunes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AreasComunesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AreasComunesBindingSource;
        private ControlCondominiosDataSet1 ControlCondominiosDataSet1;
        private ControlCondominiosDataSet1TableAdapters.AreasComunesTableAdapter AreasComunesTableAdapter;
    }
}