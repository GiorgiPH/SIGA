using Microsoft.Reporting.WinForms;
using PV.Clases.PedidoCliente;
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
    public partial class ReporteOrdenPedidoCliente : Form
    {
        string FolioOrdenPedido = string.Empty;
        string IdCliente = string.Empty;
        DBPedidoCliente c = new DBPedidoCliente();
        Moneda m = new Moneda();
        public ReporteOrdenPedidoCliente(string FolioOrdenPedido, string IdCliente)
        {
            InitializeComponent();
            this.FolioOrdenPedido=FolioOrdenPedido;
            this.IdCliente=IdCliente;
            this.controlCondominiosDataSet59.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'controlCondominiosDataSet8.Clientes' table. You can move, or remove it, as needed.
            this.clientesTableAdapter.FillBy(this.controlCondominiosDataSet8.Clientes, int.Parse(IdCliente));
            // TODO: This line of code loads data into the 'controlCondominiosDataSet59.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.FillBy(this.controlCondominiosDataSet59.DataTable1, int.Parse(FolioOrdenPedido));
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            decimal Total = c.ObtenerTotalOrdenPedidoCliente(FolioOrdenPedido);

            string numerotexto = m.Convertir(Total.ToString(), false, "pesos");

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("Total", numerotexto);


            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();

        }

        private void ReporteOrdenPedidoCliente_Load(object sender, EventArgs e)
        {
            


            
        }
    }
}
