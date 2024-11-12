using Microsoft.Reporting.WinForms;
using PV.Clases.OrdenCompra;
using PV.Properties;
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
    public partial class ReporteRemision : Form
    {
        string FolioOrdenPedido = string.Empty;
        string IdCliente = string.Empty;
        DBOrdenCompra c = new DBOrdenCompra();
        Moneda m = new Moneda();
        public ReporteRemision(string FolioOrdenPedido, string IdCliente)
        {
            InitializeComponent();
            this.FolioOrdenPedido = FolioOrdenPedido;
            this.IdCliente = IdCliente;

            // TODO: This line of code loads data into the 'controlCondominiosDataSet8.Clientes' table. You can move, or remove it, as needed.
            this.clientesTableAdapter.FillBy(this.controlCondominiosDataSet8.Clientes, int.Parse(IdCliente));
            this.controlCondominiosDataSet60.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'controlCondominiosDataSet60.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.FillBy(this.controlCondominiosDataSet60.DataTable1, int.Parse(FolioOrdenPedido));
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            decimal Total = c.ObtenerTotalRemision(FolioOrdenPedido);

            string numerotexto = m.Convertir(Total.ToString(), false, "pesos");

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("Total", numerotexto);


            this.reportViewer1.LocalReport.SetParameters(parameters);
        }

        private void ReporteRemision_Load(object sender, EventArgs e)
        {
            

            this.reportViewer1.RefreshReport();
        }
    }
}
