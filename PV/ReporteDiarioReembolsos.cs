using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteDiarioReembolsos : Form
    {
        string IdProveedor = string.Empty;
        string FechaInicio = string.Empty;
        string FechaFin = string.Empty;
        string Documento = string.Empty;
        string CentroCostos = string.Empty;
        string Anio = string.Empty;
        string Semana = string.Empty;
        public ReporteDiarioReembolsos(string idProveedor, string documento, string centroCostos, string anio, string semana, string fechaInicio, string fechaFin)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Documento = documento;
            CentroCostos = centroCostos;
            Anio = anio;
            Semana = semana;
        }

        private void ReporteDiarioReembolsos_Load(object sender, EventArgs e)
        {
            int? claveProveedor = (string.IsNullOrWhiteSpace(IdProveedor) || IdProveedor == "0") ? (int?)null : Convert.ToInt32(IdProveedor);
            DateTime? fechaInicio = string.IsNullOrWhiteSpace(FechaInicio) ? (DateTime?)null : Convert.ToDateTime(FechaInicio);
            DateTime? fechaFin = string.IsNullOrWhiteSpace(FechaFin) ? (DateTime?)null : Convert.ToDateTime(FechaFin);
            string claveDocumento = string.IsNullOrWhiteSpace(Documento) ? null : Documento;
            int? centroCostos = (string.IsNullOrWhiteSpace(CentroCostos) || CentroCostos == "0") ? (int?)null : Convert.ToInt32(CentroCostos);
            int? semana = (string.IsNullOrWhiteSpace(Semana) || Semana == "0") ? (int?)null : Convert.ToInt32(Semana);
            int? anio = (string.IsNullOrWhiteSpace(Anio) || Anio == "0") ? (int?)null : Convert.ToInt32(Anio);

            this.dTSReporteDiarioReembolsos.EnforceConstraints = false;

            this.sp_ReporteDiarioReembolsosTableAdapter.Fill(
                this.dTSReporteDiarioReembolsos.sp_ReporteDiarioReembolsos,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento,
                centroCostos,
                anio,
                semana
            );
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            this.reportViewer1.RefreshReport();
        }
    }
}
