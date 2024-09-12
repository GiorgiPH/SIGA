namespace PV
{
    partial class ReporteCategoriasFamilias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteCategoriasFamilias));
            this.CategoriasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet4 = new PV.ControlCondominiosDataSet4();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CategoriasTableAdapter = new PV.ControlCondominiosDataSet4TableAdapters.CategoriasTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet4)).BeginInit();
            this.SuspendLayout();
            // 
            // CategoriasBindingSource
            // 
            this.CategoriasBindingSource.DataMember = "Categorias";
            this.CategoriasBindingSource.DataSource = this.ControlCondominiosDataSet4;
            // 
            // ControlCondominiosDataSet4
            // 
            this.ControlCondominiosDataSet4.DataSetName = "ControlCondominiosDataSet4";
            this.ControlCondominiosDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CategoriasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteCategoriasFamilias.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 21);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1131, 544);
            this.reportViewer1.TabIndex = 0;
            // 
            // CategoriasTableAdapter
            // 
            this.CategoriasTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1131, 31);
            this.panel2.TabIndex = 97;
            // 
            // ReporteCategoriasFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 565);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteCategoriasFamilias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Categorias y Familias";
            this.Load += new System.EventHandler(this.ReporteCategoriasFamilias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CategoriasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CategoriasBindingSource;
        private ControlCondominiosDataSet4 ControlCondominiosDataSet4;
        private ControlCondominiosDataSet4TableAdapters.CategoriasTableAdapter CategoriasTableAdapter;
        private System.Windows.Forms.Panel panel2;
    }
}