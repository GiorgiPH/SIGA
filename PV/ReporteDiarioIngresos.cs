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

        public ReporteDiarioIngresos()
        {
            InitializeComponent();
        }

        // idCliente -> @ClaveProveedor (IdCliente)
        // claveDocumento -> @ClaveDocumento (Cobros no tiene combo propio; normalmente llega null)
        // fechaInicio -> @FechaInicio
        // fechaFin -> @FechaFin
        public ReporteDiarioIngresos(string idCliente, string claveDocumento, string fechaInicio, string fechaFin) : this()
        {
            _idCliente = idCliente;
            _claveDocumento = claveDocumento;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
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

            this.sp_ReporteDiarioIngresosTableAdapter.Fill(
                this.controlCondominiosDataSet46.sp_ReporteDiarioIngresos,
                claveProveedor,
                fechaInicio,
                fechaFin,
                claveDocumento);

            this.reportViewer1.RefreshReport();
        }
    }
}