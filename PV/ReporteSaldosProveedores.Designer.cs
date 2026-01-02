namespace PV
{
    partial class ReporteSaldosProveedores
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
            this.ProveedorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ControlCondominiosDataSet56 = new PV.ControlCondominiosDataSet56();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
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
            this.ProveedorTableAdapter = new PV.ControlCondominiosDataSet56TableAdapters.ProveedorTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ProveedorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet56)).BeginInit();
            this.SuspendLayout();
            // 
            // ProveedorBindingSource
            // 
            this.ProveedorBindingSource.DataMember = "Proveedor";
            this.ProveedorBindingSource.DataSource = this.ControlCondominiosDataSet56;
            // 
            // ControlCondominiosDataSet56
            // 
            this.ControlCondominiosDataSet56.DataSetName = "ControlCondominiosDataSet56";
            this.ControlCondominiosDataSet56.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteSaldosProveedores.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(1, 49);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1122, 677);
            this.reportViewer1.TabIndex = 0;
            // 
            // cbFechas
            // 
            this.cbFechas.AutoSize = true;
            this.cbFechas.Location = new System.Drawing.Point(777, 25);
            this.cbFechas.Name = "cbFechas";
            this.cbFechas.Size = new System.Drawing.Size(15, 14);
            this.cbFechas.TabIndex = 68;
            this.cbFechas.UseVisualStyleBackColor = true;
            this.cbFechas.Visible = false;
            this.cbFechas.CheckedChanged += new System.EventHandler(this.cbFechas_CheckedChanged);
            // 
            // dtFecha2
            // 
            this.dtFecha2.CustomFormat = "yyyy/MM/dd";
            this.dtFecha2.Enabled = false;
            this.dtFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha2.Location = new System.Drawing.Point(994, 21);
            this.dtFecha2.Name = "dtFecha2";
            this.dtFecha2.Size = new System.Drawing.Size(118, 20);
            this.dtFecha2.TabIndex = 67;
            this.dtFecha2.Visible = false;
            this.dtFecha2.ValueChanged += new System.EventHandler(this.dtFecha2_ValueChanged);
            this.dtFecha2.Leave += new System.EventHandler(this.dtFecha2_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(970, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Al";
            this.label4.Visible = false;
            // 
            // dtFecha1
            // 
            this.dtFecha1.CustomFormat = "yyyy/MM/dd";
            this.dtFecha1.Enabled = false;
            this.dtFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha1.Location = new System.Drawing.Point(846, 21);
            this.dtFecha1.Name = "dtFecha1";
            this.dtFecha1.Size = new System.Drawing.Size(118, 20);
            this.dtFecha1.TabIndex = 65;
            this.dtFecha1.Visible = false;
            this.dtFecha1.ValueChanged += new System.EventHandler(this.dtFecha1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(794, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Fecha:";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(332, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Al";
            this.label2.Visible = false;
            // 
            // cmbpropietario2
            // 
            this.cmbpropietario2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpropietario2.FormattingEnabled = true;
            this.cmbpropietario2.Location = new System.Drawing.Point(359, 22);
            this.cmbpropietario2.Name = "cmbpropietario2";
            this.cmbpropietario2.Size = new System.Drawing.Size(239, 21);
            this.cmbpropietario2.TabIndex = 62;
            this.cmbpropietario2.Visible = false;
            this.cmbpropietario2.SelectedIndexChanged += new System.EventHandler(this.cmbpropietario2_SelectedIndexChanged);
            // 
            // cmbPropietario1
            // 
            this.cmbPropietario1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario1.FormattingEnabled = true;
            this.cmbPropietario1.Location = new System.Drawing.Point(99, 22);
            this.cmbPropietario1.Name = "cmbPropietario1";
            this.cmbPropietario1.Size = new System.Drawing.Size(227, 21);
            this.cmbPropietario1.TabIndex = 61;
            this.cmbPropietario1.Visible = false;
            this.cmbPropietario1.SelectedIndexChanged += new System.EventHandler(this.cmbPropietario1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Propietarios:";
            this.label1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(95)))));
            this.panel2.Location = new System.Drawing.Point(1, -3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1324, 21);
            this.panel2.TabIndex = 59;
            // 
            // ProveedorTableAdapter
            // 
            this.ProveedorTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteSaldosProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 725);
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
            this.Name = "ReporteSaldosProveedores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIGA - Reporte Saldos de Proveedores";
            this.Load += new System.EventHandler(this.ReporteSaldosProveedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProveedorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlCondominiosDataSet56)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ProveedorBindingSource;
        private ControlCondominiosDataSet56 ControlCondominiosDataSet56;
        private ControlCondominiosDataSet56TableAdapters.ProveedorTableAdapter ProveedorTableAdapter;
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