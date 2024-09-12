
namespace PV
{
    partial class ReporteServicios
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
            this.ServiciosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet58 = new PV.ControlCondominiosDataSet58();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ServiciosTableAdapter = new PV.ControlCondominiosDataSet58TableAdapters.ServiciosTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ServiciosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet58)).BeginInit();
            this.SuspendLayout();
            // 
            // ServiciosBindingSource
            // 
            this.ServiciosBindingSource.DataMember = "Servicios";
            this.ServiciosBindingSource.DataSource = this.ControlCondominiosDataSet58;
            // 
            // ControlCondominiosDataSet58
            // 
            this.ControlCondominiosDataSet58.DataSetName = "ControlCondominiosDataSet58";
            this.ControlCondominiosDataSet58.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ServiciosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteServicios.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 27);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(984, 584);
            this.reportViewer1.TabIndex = 0;
            // 
            // ServiciosTableAdapter
            // 
            this.ServiciosTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(2, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 31);
            this.panel2.TabIndex = 99;
            // 
            // ReporteServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel2);
            this.Name = "ReporteServicios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Servicios";
            this.Load += new System.EventHandler(this.ReporteServicios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServiciosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet58)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ServiciosBindingSource;
        private ControlCondominiosDataSet58 ControlCondominiosDataSet58;
        private ControlCondominiosDataSet58TableAdapters.ServiciosTableAdapter ServiciosTableAdapter;
        private System.Windows.Forms.Panel panel2;
    }
}