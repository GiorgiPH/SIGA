namespace PV
{
    partial class ReporteEntradaSalidaCond
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
            this.RegistroOcupacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet48 = new PV.ControlCondominiosDataSet48();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosEmpresaTableAdapter = new PV.ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter();
            this.RegistroOcupacionTableAdapter = new PV.ControlCondominiosDataSet48TableAdapters.RegistroOcupacionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistroOcupacionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet48)).BeginInit();
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
            // RegistroOcupacionBindingSource
            // 
            this.RegistroOcupacionBindingSource.DataMember = "RegistroOcupacion";
            this.RegistroOcupacionBindingSource.DataSource = this.ControlCondominiosDataSet48;
            // 
            // ControlCondominiosDataSet48
            // 
            this.ControlCondominiosDataSet48.DataSetName = "ControlCondominiosDataSet48";
            this.ControlCondominiosDataSet48.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DatosEmpresaBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.RegistroOcupacionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteEntradaSalidaCond.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(967, 760);
            this.reportViewer1.TabIndex = 0;
            // 
            // DatosEmpresaTableAdapter
            // 
            this.DatosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // RegistroOcupacionTableAdapter
            // 
            this.RegistroOcupacionTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteEntradaSalidaCond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 760);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteEntradaSalidaCond";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Ocupacion de Entradas y Salidas";
            this.Load += new System.EventHandler(this.ReporteEntradaSalidaCond_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegistroOcupacionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet48)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DatosEmpresaBindingSource;
        private ControlCondominiosDataSet31 ControlCondominiosDataSet31;
        private System.Windows.Forms.BindingSource RegistroOcupacionBindingSource;
        private ControlCondominiosDataSet48 ControlCondominiosDataSet48;
        private ControlCondominiosDataSet31TableAdapters.DatosEmpresaTableAdapter DatosEmpresaTableAdapter;
        private ControlCondominiosDataSet48TableAdapters.RegistroOcupacionTableAdapter RegistroOcupacionTableAdapter;
    }
}