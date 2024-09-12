namespace PV
{
    partial class ReporteSaldoDetalladoProveedor
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
            this.EgresoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet57 = new PV.ControlCondominiosDataSet57();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EgresoTableAdapter = new PV.ControlCondominiosDataSet57TableAdapters.EgresoTableAdapter();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSaldos = new System.Windows.Forms.ComboBox();
            this.cbFechas = new System.Windows.Forms.CheckBox();
            this.dtFecha2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbpropietario2 = new System.Windows.Forms.ComboBox();
            this.cmbPropietario1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.EgresoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet57)).BeginInit();
            this.SuspendLayout();
            // 
            // EgresoBindingSource
            // 
            this.EgresoBindingSource.DataMember = "Egreso";
            this.EgresoBindingSource.DataSource = this.ControlCondominiosDataSet57;
            // 
            // ControlCondominiosDataSet57
            // 
            this.ControlCondominiosDataSet57.DataSetName = "ControlCondominiosDataSet57";
            this.ControlCondominiosDataSet57.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EgresoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteSaldoDetalladoProveedores.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-3, 51);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1327, 697);
            this.reportViewer1.TabIndex = 0;
            // 
            // EgresoTableAdapter
            // 
            this.EgresoTableAdapter.ClearBeforeFill = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(733, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 153;
            this.label6.Text = "Saldados:";
            // 
            // cmbSaldos
            // 
            this.cmbSaldos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaldos.FormattingEnabled = true;
            this.cmbSaldos.Items.AddRange(new object[] {
            "Si",
            "No"});
            this.cmbSaldos.Location = new System.Drawing.Point(818, 23);
            this.cmbSaldos.Name = "cmbSaldos";
            this.cmbSaldos.Size = new System.Drawing.Size(133, 21);
            this.cmbSaldos.TabIndex = 152;
            this.cmbSaldos.SelectedIndexChanged += new System.EventHandler(this.cmbSaldos_SelectedIndexChanged);
            // 
            // cbFechas
            // 
            this.cbFechas.AutoSize = true;
            this.cbFechas.Location = new System.Drawing.Point(981, 28);
            this.cbFechas.Name = "cbFechas";
            this.cbFechas.Size = new System.Drawing.Size(15, 14);
            this.cbFechas.TabIndex = 151;
            this.cbFechas.UseVisualStyleBackColor = true;
            this.cbFechas.CheckedChanged += new System.EventHandler(this.cbFechas_CheckedChanged);
            // 
            // dtFecha2
            // 
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(1198, 24);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(118, 20);
            this.dtFecha2.TabIndex = 150;
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged);
            this.dtFecha2.Leave += new System.EventHandler(this.dtFecha2_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1174, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 149;
            this.label4.Text = "Al";
            // 
            // dtFecha1
            // 
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(1050, 24);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(118, 20);
            this.dtFecha1.TabIndex = 148;
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(998, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 147;
            this.label3.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(346, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 146;
            this.label2.Text = "Al";
            // 
            // cmbpropietario2
            // 
            this.cmbpropietario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario2.FormattingEnabled = true;
            this.cmbpropietario2.Location = new System.Drawing.Point(373, 23);
            this.cmbpropietario2.Name = "cmbpropietario2";
            this.cmbpropietario2.Size = new System.Drawing.Size(284, 21);
            this.cmbpropietario2.TabIndex = 145;
            this.cmbpropietario2.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario2_SelectedIndexChanged);
            // 
            // cmbPropietario1
            // 
            this.cmbPropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario1.FormattingEnabled = true;
            this.cmbPropietario1.Location = new System.Drawing.Point(105, 24);
            this.cmbPropietario1.Name = "cmbPropietario1";
            this.cmbPropietario1.Size = new System.Drawing.Size(235, 21);
            this.cmbPropietario1.TabIndex = 144;
            this.cmbPropietario1.SelectedIndexChanged += new System.EventHandler(this.cmbPropietario1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 143;
            this.label1.Text = "Proveedor:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(-3, -4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1543, 21);
            this.panel2.TabIndex = 142;
            // 
            // ReporteSaldoDetalladoProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 749);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSaldos);
            this.Controls.Add(this.cbFechas);
            this.Controls.Add(this.dtFecha2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtFecha1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbpropietario2);
            this.Controls.Add(this.cmbPropietario1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteSaldoDetalladoProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Saldo Detallado Proveedor";
            this.Load += new System.EventHandler(this.ReporteSaldoDetalladoProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EgresoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet57)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EgresoBindingSource;
        private ControlCondominiosDataSet57 ControlCondominiosDataSet57;
        private ControlCondominiosDataSet57TableAdapters.EgresoTableAdapter EgresoTableAdapter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSaldos;
        private System.Windows.Forms.CheckBox cbFechas;
        private System.Windows.Forms.DateTimePicker dtFecha2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFecha1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbpropietario2;
        private System.Windows.Forms.ComboBox cmbPropietario1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}