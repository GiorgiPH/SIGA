namespace PV
{
    partial class ReporteOrdenPedidoCliente
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
            this.datosEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet23 = new PV.ControlCondominiosDataSet23();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet59 = new PV.ControlCondominiosDataSet59();
            this.clientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.controlCondominiosDataSet8 = new PV.ControlCondominiosDataSet8();
            this.datosEmpresaTableAdapter = new PV.ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter();
            this.dataTable1TableAdapter = new PV.ControlCondominiosDataSet59TableAdapters.DataTable1TableAdapter();
            this.clientesTableAdapter = new PV.ControlCondominiosDataSet8TableAdapters.ClientesTableAdapter();
            this.txtcorreo2 = new System.Windows.Forms.TextBox();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet59)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet8)).BeginInit();
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
            this.dataTable1BindingSource.DataSource = this.controlCondominiosDataSet59;
            // 
            // controlCondominiosDataSet59
            // 
            this.controlCondominiosDataSet59.DataSetName = "ControlCondominiosDataSet59";
            this.controlCondominiosDataSet59.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // datosEmpresaTableAdapter
            // 
            this.datosEmpresaTableAdapter.ClearBeforeFill = true;
            // 
            // dataTable1TableAdapter
            // 
            this.dataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // txtcorreo2
            // 
            this.txtcorreo2.Location = new System.Drawing.Point(720, 85);
            this.txtcorreo2.Name = "txtcorreo2";
            this.txtcorreo2.Size = new System.Drawing.Size(78, 20);
            this.txtcorreo2.TabIndex = 6;
            this.txtcorreo2.Visible = false;
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(719, 59);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(78, 20);
            this.txtcorreo.TabIndex = 5;
            this.txtcorreo.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(720, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 43);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enviar por Correo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PV.ReporteOrdenPedidoCliente.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(713, 666);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReporteOrdenPedidoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 665);
            this.Controls.Add(this.txtcorreo2);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteOrdenPedidoCliente";
            this.Text = "ReporteOrdenPedidoCliente";
            this.Load += new System.EventHandler(this.ReporteOrdenPedidoCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datosEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet59)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlCondominiosDataSet8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ControlCondominiosDataSet23 controlCondominiosDataSet23;
        private System.Windows.Forms.BindingSource datosEmpresaBindingSource;
        private ControlCondominiosDataSet23TableAdapters.DatosEmpresaTableAdapter datosEmpresaTableAdapter;
        private ControlCondominiosDataSet59 controlCondominiosDataSet59;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private ControlCondominiosDataSet59TableAdapters.DataTable1TableAdapter dataTable1TableAdapter;
        private ControlCondominiosDataSet8 controlCondominiosDataSet8;
        private System.Windows.Forms.BindingSource clientesBindingSource;
        private ControlCondominiosDataSet8TableAdapters.ClientesTableAdapter clientesTableAdapter;
        private System.Windows.Forms.TextBox txtcorreo2;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.Button button1;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}