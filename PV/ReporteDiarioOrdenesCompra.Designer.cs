namespace PV
{
    partial class ReporteDiarioOrdenesCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteDiarioOrdenesCompra));
            this.OrdenCompraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet34 = new PV.ControlCondominiosDataSet34();
            this.DatosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet31 = new PV.ControlCondominiosDataSet31();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter();
            this.OrdenCompraTableAdapter = new PV.ControlCondominiosDataSet34TableAdapters.OrdenCompraTableAdapter();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.OrdenCompraBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdenCompraBindingSource
            // 
            this.OrdenCompraBindingSource.DataMember = "OrdenCompra";
            this.OrdenCompraBindingSource.DataSource = this.ControlCondominiosDataSet34;
            // 
            // ControlCondominiosDataSet34
            // 
            this.ControlCondominiosDataSet34.DataSetName = "ControlCondominiosDataSet34";
            this.ControlCondominiosDataSet34.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.OrdenCompraBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.DatosEmpresaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteDiarioOrdenesCompra.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 29);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1431, 720);
            this.reportViewer1.TabIndex = 0;
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // OrdenCompraTableAdapter
            // 
            this.OrdenCompraTableAdapter.ClearBeforeFill = true;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 80;
            this.guna2GradientPanel1.CustomizableEdges.BottomRight = false;
            this.guna2GradientPanel1.CustomizableEdges.TopLeft = false;
            this.guna2GradientPanel1.CustomizableEdges.TopRight = false;
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1431, 29);
            this.guna2GradientPanel1.TabIndex = 115;
            // 
            // ReporteDiarioOrdenesCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 749);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteDiarioOrdenesCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Diario de Ordenes de Compras";
            this.Load += new System.EventHandler(this.ReporteDiarioOrdenesCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrdenCompraBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource OrdenCompraBindingSource;
        private ControlCondominiosDataSet34 ControlCondominiosDataSet34;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet31 ControlCondominiosDataSet31;
        private ControlCondominiosDataSet34TableAdapters.OrdenCompraTableAdapter OrdenCompraTableAdapter;
        private ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
    }
}