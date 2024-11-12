namespace PV
{
    partial class ReporteRemision
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet60 = new PV.ControlCondominiosDataSet60();
            this.clientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet8 = new PV.ControlCondominiosDataSet8();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.controlCondominiosDataSet23BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.dataTable1TableAdapter = new PV.ControlCondominiosDataSet60TableAdapters.DataTable1TableAdapter();
            this.controlCondominiosDataSet14 = new PV.ControlCondominiosDataSet14();
            this.controlCondominiosDataSet14BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientesTableAdapter = new PV.ControlCondominiosDataSet8TableAdapters.ClientesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet60)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet14BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // datosEmpresaBindingSource
            // 
            this.datosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.datosEmpresaBindingSource.DataSource = this.controlCondominiosDataSet23;
            // 
            // controlCondominiosDataSet23
            // 
            this.controlCondominiosDataSet23.DataSetName = "ControlCondominiosDataSet23";
            this.controlCondominiosDataSet23.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.controlCondominiosDataSet60;
            // 
            // controlCondominiosDataSet60
            // 
            this.controlCondominiosDataSet60.DataSetName = "ControlCondominiosDataSet60";
            this.controlCondominiosDataSet60.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientesBindingSource
            // 
            this.clientesBindingSource.DataMember = "Clientes";
            this.clientesBindingSource.DataSource = this.controlCondominiosDataSet8;
            // 
            // controlCondominiosDataSet8
            // 
            this.controlCondominiosDataSet8.DataSetName = "ControlCondominiosDataSet8";
            this.controlCondominiosDataSet8.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.datosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.dataTable1BindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.clientesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteRemision.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // controlCondominiosDataSet23BindingSource
            // 
            this.controlCondominiosDataSet23BindingSource.DataSource = this.controlCondominiosDataSet23;
            this.controlCondominiosDataSet23BindingSource.Position = 0;
            // 
            // datosEmpresaTableAdapter
            // 
            this.datosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // dataTable1TableAdapter
            // 
            this.dataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // controlCondominiosDataSet14
            // 
            this.controlCondominiosDataSet14.DataSetName = "ControlCondominiosDataSet14";
            this.controlCondominiosDataSet14.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // controlCondominiosDataSet14BindingSource
            // 
            this.controlCondominiosDataSet14BindingSource.DataSource = this.controlCondominiosDataSet14;
            this.controlCondominiosDataSet14BindingSource.Position = 0;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteRemision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteRemision";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReporteRemision";
            this.Load += new System.EventHandler(this.ReporteRemision_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet60)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet14BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource controlCondominiosDataSet23BindingSource;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private ControlCondominiosDataSet60 controlCondominiosDataSet60;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private ControlCondominiosDataSet60TableAdapters.DataTable1TableAdapter dataTable1TableAdapter;
        private ControlCondominiosDataSet14 controlCondominiosDataSet14;
        private System.Windows.Forms.BindingSource controlCondominiosDataSet14BindingSource;
        private ControlCondominiosDataSet8 controlCondominiosDataSet8;
        private System.Windows.Forms.BindingSource clientesBindingSource;
        private ControlCondominiosDataSet8TableAdapters.ClientesTableAdapter clientesTableAdapter;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}