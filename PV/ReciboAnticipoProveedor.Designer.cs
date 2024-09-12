namespace PV
{
    partial class ReciboAnticipoProveedor
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
            this.ControlCondominiosDataSet31 = new PV.ControlCondominiosDataSet31();
            this.AnticipoProveedorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet51 = new PV.ControlCondominiosDataSet51();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter();
            this.AnticipoProveedorTableAdapter = new PV.ControlCondominiosDataSet51TableAdapters.AnticipoProveedorTableAdapter();
            this.txtCorreo2 = new System.Windows.Forms.TextBox();
            this.txtCorreo1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnticipoProveedorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet51)).BeginInit();
            this.SuspendLayout();
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
            // AnticipoProveedorBindingSource
            // 
            this.AnticipoProveedorBindingSource.DataMember = "AnticipoProveedor";
            this.AnticipoProveedorBindingSource.DataSource = this.ControlCondominiosDataSet51;
            // 
            // ControlCondominiosDataSet51
            // 
            this.ControlCondominiosDataSet51.DataSetName = "ControlCondominiosDataSet51";
            this.ControlCondominiosDataSet51.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DatosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.AnticipoProveedorBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReciboAnticipoProveedor.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(994, 722);
            this.reportViewer1.TabIndex = 0;
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // AnticipoProveedorTableAdapter
            // 
            this.AnticipoProveedorTableAdapter.ClearBeforeFill = true;
            // 
            // txtCorreo2
            // 
            this.txtCorreo2.Location = new System.Drawing.Point(882, 690);
            this.txtCorreo2.Name = "txtCorreo2";
            this.txtCorreo2.Size = new System.Drawing.Size(100, 20);
            this.txtCorreo2.TabIndex = 4;
            this.txtCorreo2.Visible = false;
            // 
            // txtCorreo1
            // 
            this.txtCorreo1.Location = new System.Drawing.Point(882, 664);
            this.txtCorreo1.Name = "txtCorreo1";
            this.txtCorreo1.Size = new System.Drawing.Size(100, 20);
            this.txtCorreo1.TabIndex = 3;
            this.txtCorreo1.Visible = false;
            // 
            // ReciboAnticipoProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 722);
            this.Controls.Add(this.txtCorreo2);
            this.Controls.Add(this.txtCorreo1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReciboAnticipoProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Recibo de Anticipo Proveedor";
            this.Load += new System.EventHandler(this.ReciboAnticipoProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnticipoProveedorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet51)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet31 ControlCondominiosDataSet31;
        private System.Windows.Forms.BindingSource AnticipoProveedorBindingSource;
        private ControlCondominiosDataSet51 ControlCondominiosDataSet51;
        private ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private ControlCondominiosDataSet51TableAdapters.AnticipoProveedorTableAdapter AnticipoProveedorTableAdapter;
        private System.Windows.Forms.TextBox txtCorreo2;
        private System.Windows.Forms.TextBox txtCorreo1;
    }
}