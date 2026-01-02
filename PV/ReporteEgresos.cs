using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteEgresos : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string IdProveedor = string.Empty;
        string FechaInicio = string.Empty;
        string FechaFin = string.Empty;
        string Documento = string.Empty;
        string CuentaBancaria = string.Empty;


        public ReporteEgresos(string idProveedor, string documento, string fechaInicio, string fechaFin, string cuentaBancaria)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Documento = documento;
            CuentaBancaria = cuentaBancaria;
        }

        private void ReporteEgresos_Load(object sender, EventArgs e)
        {
            c.SeleccionarProveedor(cmbPropietario1);
            //cmbPropietario1.SelectedIndex = 0;
            c.SeleccionarCuentasBancarias(cmbTipo);
            //cmbTipo.SelectedIndex = 0;
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

          

            int? claveProveedor = (string.IsNullOrWhiteSpace(IdProveedor) || IdProveedor == "0") ? (int?)null : Convert.ToInt32(IdProveedor);
            DateTime? fechaInicio = string.IsNullOrWhiteSpace(FechaInicio) ? (DateTime?)null : Convert.ToDateTime(FechaInicio);
            DateTime? fechaFin = string.IsNullOrWhiteSpace(FechaFin) ? (DateTime?)null : Convert.ToDateTime(FechaFin);
            string claveDocumento = string.IsNullOrWhiteSpace(Documento) ? null : Documento;
            int? cuentaBancaria = (string.IsNullOrWhiteSpace(CuentaBancaria) || CuentaBancaria == "0") ? (int?)null : Convert.ToInt32(CuentaBancaria);

            this.ControlCondominiosDataSet38.EnforceConstraints = false;

            this.EgresoTableAdapter.Fill(
                this.ControlCondominiosDataSet38.Egreso,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento,
                cuentaBancaria

            );

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.reportViewer1.RefreshReport();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            this.reportViewer1.RefreshReport();
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
           
            this.reportViewer1.RefreshReport();
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            
            this.reportViewer1.RefreshReport();
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
           
            this.reportViewer1.RefreshReport();
        }
    }
}
