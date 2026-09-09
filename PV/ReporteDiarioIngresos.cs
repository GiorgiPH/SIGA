using Microsoft.Reporting.WinForms;
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
    public partial class ReporteDiarioIngresos : Form
    {
        // Parametros recibidos desde el filtro (ReporteDiarioComprasFiltro)
        private string _idCliente;
        private string _claveDocumento;
        private string _fechaInicio;
        private string _fechaFin;
        private string _claveCentroCostos;
        private string _claveCuentaBancaria;

        public ReporteDiarioIngresos()
        {
            InitializeComponent();
        }

        // idCliente -> @ClaveProveedor (IdCliente)
        // claveDocumento -> @ClaveDocumento (Cobros no tiene combo propio; normalmente llega null)
        // fechaInicio -> @FechaInicio
        // fechaFin -> @FechaFin
        // claveCentroCostos -> @ClaveCentroCostos
        // claveCuentaBancaria -> @ClaveCuentaBancaria
        public ReporteDiarioIngresos(string idCliente, string claveDocumento, string fechaInicio, string fechaFin, string claveCentroCostos, string claveCuentaBancaria) : this()
        {
            _idCliente = idCliente;
            _claveDocumento = claveDocumento;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _claveCentroCostos = claveCentroCostos;
            _claveCuentaBancaria = claveCuentaBancaria;
        }

        private void ReporteDiarioIngresos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            // Conversion de parametros recibidos del filtro a los tipos que espera el SP
            int? claveProveedor = null;
            if (!string.IsNullOrWhiteSpace(_idCliente) && _idCliente != "0")
            {
                claveProveedor = Convert.ToInt32(_idCliente);
            }

            DateTime? fechaInicio = null;
            if (!string.IsNullOrWhiteSpace(_fechaInicio))
            {
                fechaInicio = Convert.ToDateTime(_fechaInicio);
            }

            DateTime? fechaFin = null;
            if (!string.IsNullOrWhiteSpace(_fechaFin))
            {
                fechaFin = Convert.ToDateTime(_fechaFin);
            }

            string claveDocumento = string.IsNullOrWhiteSpace(_claveDocumento) ? null : _claveDocumento;

            int? claveCentroCostos = null;
            if (!string.IsNullOrWhiteSpace(_claveCentroCostos) && _claveCentroCostos != "0")
            {
                claveCentroCostos = Convert.ToInt32(_claveCentroCostos);
            }

            int? claveCuentaBancaria = null;
            if (!string.IsNullOrWhiteSpace(_claveCuentaBancaria) && _claveCuentaBancaria != "0")
            {
                claveCuentaBancaria = Convert.ToInt32(_claveCuentaBancaria);
            }
            ReportParameter rpFechaInicio = new ReportParameter(
    "FechaInicio",
    fechaInicio.HasValue ? fechaInicio.Value.ToString("yyyy-MM-dd") : string.Empty);

            ReportParameter rpFechaFin = new ReportParameter(
                "FechaFin",
                fechaFin.HasValue ? fechaFin.Value.ToString("yyyy-MM-dd") : string.Empty);

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rpFechaInicio, rpFechaFin });


            this.sp_ReporteDiarioIngresosTableAdapter.Fill(
                this.controlCondominiosDataSet46.sp_ReporteDiarioIngresos,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento,
                claveCentroCostos,
                claveCuentaBancaria);

            this.reportViewer1.RefreshReport();
        }
    }
}