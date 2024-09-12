namespace PV
{
    partial class ReportePropietarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportePropietarios));
            this.PropietariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet41 = new PV.ControlCondominiosDataSet41();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PropietariosTableAdapter = new PV.ControlCondominiosDataSet41TableAdapters.PropietariosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PropietariosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet41)).BeginInit();
            this.SuspendLayout();
            // 
            // PropietariosBindingSource
            // 
            this.PropietariosBindingSource.DataMember = "Propietarios";
            this.PropietariosBindingSource.DataSource = this.ControlCondominiosDataSet41;
            // 
            // ControlCondominiosDataSet41
            // 
            this.ControlCondominiosDataSet41.DataSetName = "ControlCondominiosDataSet41";
            this.ControlCondominiosDataSet41.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PropietariosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReportePropietarios.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(909, 614);
            this.reportViewer1.TabIndex = 0;
            // 
            // PropietariosTableAdapter
            // 
            this.PropietariosTableAdapter.ClearBeforeFill = true;
            // 
            // ReportePropietarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 614);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportePropietarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Propietarios";
            this.Load += new System.EventHandler(this.ReportePropietarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PropietariosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet41)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PropietariosBindingSource;
        private ControlCondominiosDataSet41 ControlCondominiosDataSet41;
        private ControlCondominiosDataSet41TableAdapters.PropietariosTableAdapter PropietariosTableAdapter;
    }
}