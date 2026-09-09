using System;
using System.Windows.Forms;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteDiarioOrdenesCompra : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        string provedor = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;

        public ReporteDiarioOrdenesCompra(string Proveedor, string Fecha, string Fecha1, string Fecha2)
        {
            InitializeComponent();
            provedor = Proveedor;
            fecha = Fecha;
            fecha1 = Fecha1;
            fecha2 = Fecha2;
        }

        private void ReporteDiarioOrdenesCompra_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            // Conversion de parametros recibidos a los tipos que espera el SP
            int? claveProveedor = null;
            if (!string.IsNullOrWhiteSpace(provedor) && provedor != "0")
            {
                claveProveedor = Convert.ToInt32(provedor);
            }

            DateTime? fechaInicio = null;
            if (!string.IsNullOrWhiteSpace(fecha1))
            {
                fechaInicio = Convert.ToDateTime(fecha1);
            }

            DateTime? fechaFin = null;
            if (!string.IsNullOrWhiteSpace(fecha2))
            {
                fechaFin = Convert.ToDateTime(fecha2);
            }

            string claveDocumento = string.IsNullOrWhiteSpace(fecha) ? null : fecha;

            this.OrdenCompraTableAdapter.Fill(
      this.ControlCondominiosDataSet34.OrdenCompra,
      claveProveedor,
      fechaInicio,
      fechaFin,
      claveDocumento);

            this.reportViewer1.RefreshReport();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}