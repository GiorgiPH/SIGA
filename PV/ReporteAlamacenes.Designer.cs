namespace PV
{
    partial class ReporteAlamacenes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteAlamacenes));
            this.AlmacenesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet25 = new PV.ControlCondominiosDataSet25();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AlmacenesTableAdapter = new PV.ControlCondominiosDataSet25TableAdapters.AlmacenesTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.AlmacenesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet25)).BeginInit();
            this.SuspendLayout();
            // 
            // AlmacenesBindingSource
            // 
            this.AlmacenesBindingSource.DataMember = "Almacenes";
            this.AlmacenesBindingSource.DataSource = this.ControlCondominiosDataSet25;
            // 
            // ControlCondominiosDataSet25
            // 
            this.ControlCondominiosDataSet25.DataSetName = "ControlCondominiosDataSet25";
            this.ControlCondominiosDataSet25.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AlmacenesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteAlmacenes.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(1, 19);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1024, 717);
            this.reportViewer1.TabIndex = 0;
            // 
            // AlmacenesTableAdapter
            // 
            this.AlmacenesTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(1, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1046, 21);
            this.panel2.TabIndex = 96;
            // 
            // ReporteAlamacenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 729);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteAlamacenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Almacenes";
            this.Load += new System.EventHandler(this.ReporteAlamacenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AlmacenesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet25)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AlmacenesBindingSource;
        private ControlCondominiosDataSet25 ControlCondominiosDataSet25;
        private ControlCondominiosDataSet25TableAdapters.AlmacenesTableAdapter AlmacenesTableAdapter;
        private System.Windows.Forms.Panel panel2;
    }
}