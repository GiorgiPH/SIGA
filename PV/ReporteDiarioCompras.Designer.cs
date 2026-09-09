namespace PV
{
    partial class ReporteDiarioCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteDiarioCompras));
            this.RecepcionProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet36 = new PV.ControlCondominiosDataSet36();
            this.DatosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet31 = new PV.ControlCondominiosDataSet31();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RecepcionProductoTableAdapter = new PV.ControlCondominiosDataSet36TableAdapters.RecepcionProductoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.RecepcionProductoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).BeginInit();
            this.SuspendLayout();
            // 
            // RecepcionProductoBindingSource
            // 
            this.RecepcionProductoBindingSource.DataMember = "RecepcionProducto";
            this.RecepcionProductoBindingSource.DataSource = this.ControlCondominiosDataSet36;
            // 
            // ControlCondominiosDataSet36
            // 
            this.ControlCondominiosDataSet36.DataSetName = "ControlCondominiosDataSet36";
            this.ControlCondominiosDataSet36.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DatosEmpresaBindingSource
            // 
            this.DatosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.DatosEmpresaBindingSource.DataSource = this.ControlCondominiosDataSet31;
            // 
            // ControlCondominiosDataSet31
            // 
            this.ControlCondominiosDataSet31.DataSetName = "ControlCondominiosDataSet31";
            this.ControlCondominiosDataSet31.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.RecepcionProductoBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.DatosEmpresaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDiarioCompras.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 46);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1441, 701);
            this.reportViewer1.TabIndex = 0;
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(-2, -9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1575, 21);
            this.panel2.TabIndex = 89;
            // 
            // RecepcionProductoTableAdapter
            // 
            this.RecepcionProductoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteDiarioCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 747);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteDiarioCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Diario de Compras";
            this.Load += new System.EventHandler(this.ReporteDiarioCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RecepcionProductoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource RecepcionProductoBindingSource;
        private ControlCondominiosDataSet36 ControlCondominiosDataSet36;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet31 ControlCondominiosDataSet31;
        private ControlCondominiosDataSet36TableAdapters.RecepcionProductoTableAdapter RecepcionProductoTableAdapter;
        private ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private System.Windows.Forms.Panel panel2;
    }
}