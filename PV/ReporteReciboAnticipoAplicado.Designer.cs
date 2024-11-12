namespace PV
{
    partial class ReporteReciboAnticipoAplicado
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
            this.ControlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.Anticipo_GeneralBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet70 = new PV.ControlCondominiosDataSet70();
            this.Anticipo_GeneralTableAdapter = new PV.ControlCondominiosDataSet70TableAdapters.Anticipo_GeneralTableAdapter();
            this.anticipoGeneralBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anticipo_GeneralBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet70)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anticipoGeneralBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DatosEmpresaBindingSource
            // 
            this.DatosEmpresaBindingSource.DataMember = "DatosEmpresa";
            this.DatosEmpresaBindingSource.DataSource = this.ControlCondominiosDataSet23;
            // 
            // ControlCondominiosDataSet23
            // 
            this.ControlCondominiosDataSet23.DataSetName = "ControlCondominiosDataSet23";
            this.ControlCondominiosDataSet23.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DatosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.anticipoGeneralBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteReciboAnticipoAplicado.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(900, 648);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // Anticipo_GeneralBindingSource
            // 
            this.Anticipo_GeneralBindingSource.DataMember = "Anticipo_General";
            this.Anticipo_GeneralBindingSource.DataSource = this.ControlCondominiosDataSet70;
            // 
            // ControlCondominiosDataSet70
            // 
            this.ControlCondominiosDataSet70.DataSetName = "ControlCondominiosDataSet70";
            this.ControlCondominiosDataSet70.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Anticipo_GeneralTableAdapter
            // 
            this.Anticipo_GeneralTableAdapter.ClearBeforeFill = true;
            // 
            // anticipoGeneralBindingSource
            // 
            this.anticipoGeneralBindingSource.DataMember = "Anticipo_General";
            this.anticipoGeneralBindingSource.DataSource = this.ControlCondominiosDataSet70;
            // 
            // ReporteReciboAnticipoAplicado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 648);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteReciboAnticipoAplicado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SICC - Recibo Anticipo Aplicado";
            this.Load += new System.EventHandler(this.ReporteReciboAnticipoAplicado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anticipo_GeneralBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet70)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anticipoGeneralBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet23 ControlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource Anticipo_GeneralBindingSource;
        private ControlCondominiosDataSet70 ControlCondominiosDataSet70;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private ControlCondominiosDataSet70TableAdapters.Anticipo_GeneralTableAdapter Anticipo_GeneralTableAdapter;
        private System.Windows.Forms.BindingSource anticipoGeneralBindingSource;
    }
}