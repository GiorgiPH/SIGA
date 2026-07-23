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
    public partial class ReporteRemision : Form
    {
        string FolioOrdenPedido = string.Empty;
        string IdCliente = string.Empty;
        string[] valores = null;
        string[] valoresR = null;
        DBOrdenCompra co = new DBOrdenCompra();
        Moneda m = new Moneda();
        DBClientes cl = new DBClientes();
        DBRemiision r = new DBRemiision();  
        public ReporteRemision(string FolioOrdenPedido, string IdCliente)
        {
            InitializeComponent();
            this.FolioOrdenPedido = FolioOrdenPedido;
            this.IdCliente = IdCliente;
            valores = cl.InformacionCliente(IdCliente);
            valoresR = r.InformacionRemision(FolioOrdenPedido);
            // TODO: This line of code loads data into the 'controlCondominiosDataSet8.Clientes' table. You can move, or remove it, as needed.
            this.clientesTableAdapter.FillBy(this.controlCondominiosDataSet8.Clientes, int.Parse(IdCliente));
            this.controlCondominiosDataSet60.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'controlCondominiosDataSet60.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.FillBy(this.controlCondominiosDataSet60.DataTable1, int.Parse(FolioOrdenPedido));
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);
  
            decimal Total = co.ObtenerTotalRemision(FolioOrdenPedido);

            string numerotexto = m.Convertir(Total.ToString(), false, "pesos");

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("Total", numerotexto);


            this.reportViewer1.LocalReport.SetParameters(parameters);
        }

        private void ReporteRemision_Load(object sender, EventArgs e)
        {
            

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carpeta = Utilerias.SavePDF(reportViewer1, "Remision", valoresR[1], valoresR[4]);

            string mensaje = @"
<html>
<body style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #333333;'>
    <p>Estimado <strong>Socio Comercial</strong>,</p>

    <p>
        Enviamos la remisión de su pedido confirmado.<br />
        Su factura se emitirá a la brevedad y será enviada a su correo registrado.
    </p>

    <p>
        Si tiene alguna duda o requiere realizar algún ajuste, no dude en contactar a su vendedor, quien estará encantado de asistirle.
    </p>

    <p>
        Reciban un cordial saludo y que tengan un excelente día.
    </p>
</body>
</html>";

            bool enviado = CorreosMasivos.EnviarCorreosMime(
                "Remisión",
                mensaje,
                Utilerias.ConvertirReportViewerAPdf(reportViewer1),
                "Remisión-" + valoresR[4] + ".pdf",
                valores[5]);

            if (!string.IsNullOrEmpty(valores[6]))
            {
                CorreosMasivos.EnviarCorreosMime(
                    "Remisión",
                    mensaje,
                    Utilerias.ConvertirReportViewerAPdf(reportViewer1),
                    "Remisión-" + valoresR[4] + ".pdf",
                    valores[6]);
            }

            if (!string.IsNullOrEmpty(valores[7]))
            {
                CorreosMasivos.EnviarCorreosMime(
                    "Remisión",
                    mensaje,
                    Utilerias.ConvertirReportViewerAPdf(reportViewer1),
                    "Remisión-" + valoresR[4] + ".pdf",
                    valores[7]);
            }

            MessageBox.Show("Correo enviado exitosamente");
            
        }
    }
}
