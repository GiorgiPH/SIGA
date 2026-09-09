using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;
namespace PV
{
    public partial class ReporteDiarioCompras : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        

        string IdProveedor = string.Empty;
        string FechaInicio = string.Empty;
        string FechaFin = string.Empty;
        string Documento = string.Empty;
     

        public ReporteDiarioCompras(string idProveedor, string documento, string fechaInicio, string fechaFin)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Documento = documento;
       
        }

        private void ReporteDiarioCompras_Load(object sender, EventArgs e)
        {

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            int? claveProveedor = (string.IsNullOrWhiteSpace(IdProveedor) || IdProveedor == "0") ? (int?)null : Convert.ToInt32(IdProveedor);
            DateTime? fechaInicio = string.IsNullOrWhiteSpace(FechaInicio) ? (DateTime?)null : Convert.ToDateTime(FechaInicio);
            DateTime? fechaFin = string.IsNullOrWhiteSpace(FechaFin) ? (DateTime?)null : Convert.ToDateTime(FechaFin);
            string claveDocumento = string.IsNullOrWhiteSpace(Documento) ? null : Documento;

            this.ControlCondominiosDataSet36.EnforceConstraints = false;

            this.RecepcionProductoTableAdapter.Fill(
                this.ControlCondominiosDataSet36.RecepcionProducto,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento
                
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
