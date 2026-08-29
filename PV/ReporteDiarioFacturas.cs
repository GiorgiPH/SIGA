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
    public partial class ReporteDiarioFacturas : Form
    {
        // Parametros recibidos desde el filtro (ReporteDiarioComprasFiltro)
        private string _idCliente;
        private string _claveDocumento;
        private string _fechaInicio;
        private string _fechaFin;
        private string _claveCentroCostos;
        private string _claveProyecto;

        public ReporteDiarioFacturas()
        {
            InitializeComponent();
        }

        // idCliente -> @ClaveProveedor (IdCliente)
        // claveDocumento -> @ClaveDocumento
        // fechaInicio -> @FechaInicio
        // fechaFin -> @FechaFin
        // claveCentroCostos -> @ClaveCentroCostos
        // claveProyecto -> @ClaveProyecto
        public ReporteDiarioFacturas(string idCliente, string claveDocumento, string fechaInicio, string fechaFin, string claveCentroCostos, string claveProyecto) : this()
        {
            _idCliente = idCliente;
            _claveDocumento = claveDocumento;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _claveCentroCostos = claveCentroCostos;
            _claveProyecto = claveProyecto;
        }

        private void ReporteDiarioFacturas_Load(object sender, EventArgs e)
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

            int? claveProyecto = null;
            if (!string.IsNullOrWhiteSpace(_claveProyecto) && _claveProyecto != "0")
            {
                claveProyecto = Convert.ToInt32(_claveProyecto);
            }

            this.sp_ReporteDiarioFacturasTableAdapter.Fill(
                this.dTSFactura.sp_ReporteDiarioFacturas,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento,
                claveCentroCostos,
                claveProyecto);

            this.reportViewer1.RefreshReport();
        }
    }
}