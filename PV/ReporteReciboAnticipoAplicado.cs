using System;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteReciboAnticipoAplicado : Form
    {
        string Anticipo = string.Empty;
        string FolioGeneral = string.Empty;

        public ReporteReciboAnticipoAplicado(string anticipo, string foliogeneral)
        {
            InitializeComponent();
            Anticipo = anticipo;
            FolioGeneral = foliogeneral;
        }

        private void ReporteReciboAnticipoAplicado_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet23.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet70.Anticipo_General' Puede moverla o quitarla según sea necesario.
            this.Anticipo_GeneralTableAdapter.Fill(this.ControlCondominiosDataSet70.Anticipo_General, Convert.ToInt32(Anticipo), Convert.ToInt32(FolioGeneral));

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
