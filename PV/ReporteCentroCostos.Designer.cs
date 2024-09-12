namespace PV
{
    partial class ReporteCentroCostos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteCentroCostos));
            this.CentroCostosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet7 = new PV.ControlCondominiosDataSet7();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CentroCostosTableAdapter = new PV.ControlCondominiosDataSet7TableAdapters.CentroCostosTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.CentroCostosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet7)).BeginInit();
            this.SuspendLayout();
            // 
            // CentroCostosBindingSource
            // 
            this.CentroCostosBindingSource.DataMember = "CentroCostos";
            this.CentroCostosBindingSource.DataSource = this.ControlCondominiosDataSet7;
            // 
            // ControlCondominiosDataSet7
            // 
            this.ControlCondominiosDataSet7.DataSetName = "ControlCondominiosDataSet7";
            this.ControlCondominiosDataSet7.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CentroCostosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteCentroCostos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 23);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(942, 623);
            this.reportViewer1.TabIndex = 0;
            // 
            // CentroCostosTableAdapter
            // 
            this.CentroCostosTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(2, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 31);
            this.panel2.TabIndex = 100;
            // 
            // ReporteCentroCostos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 646);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteCentroCostos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Centro de Costos";
            this.Load += new System.EventHandler(this.ReporteCentroCostos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CentroCostosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CentroCostosBindingSource;
        private ControlCondominiosDataSet7 ControlCondominiosDataSet7;
        private ControlCondominiosDataSet7TableAdapters.CentroCostosTableAdapter CentroCostosTableAdapter;
        private System.Windows.Forms.Panel panel2;
    }
}