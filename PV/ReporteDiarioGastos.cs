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
    public partial class ReporteDiarioGastos : Form
    {
        string IdProveedor = string.Empty;
        string FechaInicio = string.Empty;
        string FechaFin = string.Empty;
        string Documento = string.Empty;
        string ClaveCentroCostos = string.Empty;
        string ClaveProyecto = string.Empty;

        // idProveedor -> @ClaveProveedor
        // documento -> @ClaveDocumento
        // fechaInicio -> @FechaInicio
        // fechaFin -> @FechaFin
        // claveCentroCostos -> @ClaveCentroCostos
        // claveProyecto -> @ClaveProyecto
        public ReporteDiarioGastos(string idProveedor, string documento, string fechaInicio, string fechaFin, string claveCentroCostos, string claveProyecto)
        {
            InitializeComponent();
            IdProveedor = idProveedor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Documento = documento;
            ClaveCentroCostos = claveCentroCostos;
            ClaveProyecto = claveProyecto;
        }

        private void ReporteDiarioGastos_Load(object sender, EventArgs e)
        {
            int? claveProveedor = (string.IsNullOrWhiteSpace(IdProveedor) || IdProveedor == "0") ? (int?)null : Convert.ToInt32(IdProveedor);
            DateTime? fechaInicio = string.IsNullOrWhiteSpace(FechaInicio) ? (DateTime?)null : Convert.ToDateTime(FechaInicio);
            DateTime? fechaFin = string.IsNullOrWhiteSpace(FechaFin) ? (DateTime?)null : Convert.ToDateTime(FechaFin);
            string claveDocumento = string.IsNullOrWhiteSpace(Documento) ? null : Documento;
            int? claveCentroCostos = (string.IsNullOrWhiteSpace(ClaveCentroCostos) || ClaveCentroCostos == "0") ? (int?)null : Convert.ToInt32(ClaveCentroCostos);
            int? claveProyecto = (string.IsNullOrWhiteSpace(ClaveProyecto) || ClaveProyecto == "0") ? (int?)null : Convert.ToInt32(ClaveProyecto);

            this.dTSReporteDiarioGastos.EnforceConstraints = false;

            this.sp_ReporteDiarioGastosTableAdapter.Fill(
                this.dTSReporteDiarioGastos.sp_ReporteDiarioGastos,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento,
                claveCentroCostos,
                claveProyecto
            );

            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);
            this.reportViewer1.RefreshReport();
        }
    }
}