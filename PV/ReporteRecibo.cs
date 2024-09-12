using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using System.Net.Mail;
using System.Drawing.Printing;

namespace PV
{
    public partial class ReporteRecibo : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        public static string Carpeta = string.Empty;
        public static int Opcion = 0;
        public static int Opcion2 = 0;
        string folio = string.Empty;
        string cliente = string.Empty;
        string condominio = string.Empty;
        string claveSub = string.Empty;
        string Correo = string.Empty;
        string Correo2 = string.Empty;

        public ReporteRecibo(string Folio, string Cliente, string Condominio, string ClaveSub)
        {
            InitializeComponent();
            folio = Folio;
            cliente = Cliente;
            condominio = Condominio;
            claveSub = ClaveSub;
        }

        private void ReporteRecibo_Load(object sender, EventArgs e)
        {

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet47.Recibo' Puede moverla o quitarla según sea necesario.
            this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet47.Recibo, Convert.ToInt32(folio));
            this.reportViewer1.RefreshReport();

           
            PageSettings pg = new PageSettings();
            pg.PaperSize = reportViewer1.GetPageSettings().PaperSize;
            pg.Margins.Left = 2;
            pg.Margins.Right = 2;
            pg.Margins.Top = 2;
            pg.Margins.Bottom = 2;
            this.reportViewer1.SetPageSettings(pg);

        }
    }
}
