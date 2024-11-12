namespace PV
{
    partial class ReporteIngresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteIngresos));
            this.CobrosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet21 = new PV.ControlCondominiosDataSet21();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CobrosTableAdapter = new PV.ControlCondominiosDataSet21TableAdapters.CobrosTableAdapter();
            this.cmbPropietario1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbpropietario2 = new System.Windows.Forms.ComboBox();
            this.cbFechas = new System.Windows.Forms.CheckBox();
            this.dtFecha2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CobrosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet21)).BeginInit();
            this.SuspendLayout();
            // 
            // CobrosBindingSource
            // 
            this.CobrosBindingSource.DataMember = "Cobros";
            this.CobrosBindingSource.DataSource = this.ControlCondominiosDataSet21;
            // 
            // ControlCondominiosDataSet21
            // 
            this.ControlCondominiosDataSet21.DataSetName = "ControlCondominiosDataSet21";
            this.ControlCondominiosDataSet21.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CobrosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteIngresos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1206, 759);
            this.reportViewer1.TabIndex = 0;
            // 
            // CobrosTableAdapter
            // 
            this.CobrosTableAdapter.ClearBeforeFill = true;
            // 
            // cmbPropietario1
            // 
            this.cmbPropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario1.FormattingEnabled = true;
            this.cmbPropietario1.Location = new System.Drawing.Point(115, 83);
            this.cmbPropietario1.Name = "cmbPropietario1";
            this.cmbPropietario1.Size = new System.Drawing.Size(299, 21);
            this.cmbPropietario1.TabIndex = 77;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "PROPIETARIOS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(427, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "AL";
            // 
            // cmbpropietario2
            // 
            this.cmbpropietario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario2.FormattingEnabled = true;
            this.cmbpropietario2.Location = new System.Drawing.Point(453, 83);
            this.cmbpropietario2.Name = "cmbpropietario2";
            this.cmbpropietario2.Size = new System.Drawing.Size(299, 21);
            this.cmbpropietario2.TabIndex = 78;
            this.cmbpropietario2.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario2_SelectedIndexChanged);
            // 
            // cbFechas
            // 
            this.cbFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFechas.AutoSize = true;
            this.cbFechas.Location = new System.Drawing.Point(42, 135);
            this.cbFechas.Name = "cbFechas";
            this.cbFechas.Size = new System.Drawing.Size(15, 14);
            this.cbFechas.TabIndex = 92;
            this.cbFechas.UseVisualStyleBackColor = true;
            this.cbFechas.CheckedChanged += new System.EventHandler(this.cbFechas_CheckedChanged);
            // 
            // dtFecha2
            // 
            this.dtFecha2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(313, 130);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(118, 20);
            this.dtFecha2.TabIndex = 91;
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged_1);
            this.dtFecha2.Leave += new System.EventHandler(this.dtFecha2_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(289, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Al";
            // 
            // dtFecha1
            // 
            this.dtFecha1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(165, 130);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(118, 20);
            this.dtFecha1.TabIndex = 89;
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged_1);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(63, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 88;
            this.label6.Text = "FECHA DE PAGO:";
            // 
            // ReporteIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 759);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.cbFechas);
            this.Controls.Add(this.dtFecha2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtFecha1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbpropietario2);
            this.Controls.Add(this.cmbPropietario1);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteIngresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte de Ingresos";
            this.Activated += new System.EventHandler(this.ReporteIngresos_Activated);
            this.Load += new System.EventHandler(this.ReporteIngresos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CobrosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet21)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource CobrosBindingSource;
        private ControlCondominiosDataSet21 ControlCondominiosDataSet21;
        private ControlCondominiosDataSet21TableAdapters.CobrosTableAdapter CobrosTableAdapter;
        private System.Windows.Forms.ComboBox cmbPropietario1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbpropietario2;
        private System.Windows.Forms.CheckBox cbFechas;
        private System.Windows.Forms.DateTimePicker dtFecha2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFecha1;
        private System.Windows.Forms.Label label6;
    }
}