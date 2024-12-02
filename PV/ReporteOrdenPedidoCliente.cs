using Microsoft.Reporting.WinForms;
using PV.Clases;
using PV.Clases.Clientes;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;
using PV.Clases.Remision;
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
        string[] valores = null;
        string[] valoresR = null;
        DBPedidoCliente c = new DBPedidoCliente();
        DBClientes cl = new DBClientes();
        DBOrdenCompra r = new DBOrdenCompra();
        Moneda m = new Moneda();
        public ReporteOrdenPedidoCliente(string FolioOrdenPedido, string IdCliente)
        {
            InitializeComponent();
            this.FolioOrdenPedido=FolioOrdenPedido;
            this.IdCliente=IdCliente;
            valores = cl.InformacionCliente(IdCliente);
            valoresR = r.InformacionOrdenPedidoCliente(FolioOrdenPedido);
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




            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carpeta = Utilerias.SavePDF(reportViewer1, "Orden Pedido Cliente", valoresR[1], valoresR[4]);
            
            bool enviado = CorreosMasivos.EnviarCorreos(
                                "Solicitud de pedido",
                         @"
                    <p>Estimado Socio Comercial,</p>
                    <p>Le enviamos la solicitud de pedido y/o cotización que ha solicitado, quedando a la espera de su pronta confirmación.</p>
                    <p>Si tiene alguna duda o requiere realizar algún ajuste, no dude en ponerse en contacto con su agente de ventas, quien estará encantado de asistirle.</p>
                    <p>Reciba un cordial saludo,</p>", Utilerias.ConvertirReportViewerAPdf(reportViewer1),
                                "OrdenPedido-" + valoresR[4] + ".pdf",
                                valores[5]);

            if (enviado)
            {
                MessageBox.Show("Correo enviado exitosamente");
            }
        }
    }
}
