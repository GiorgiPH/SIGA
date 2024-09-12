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
    public partial class ReporteOrdenCompra : Form
    {
        public ReporteOrdenCompra()
        {
            InitializeComponent();
        }

        private void ReporteOrdenCompra_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet27.OrdenCompra' Puede moverla o quitarla según sea necesario.
            this.OrdenCompraTableAdapter.Fill(this.ControlCondominiosDataSet27.OrdenCompra);

            this.reportViewer1.RefreshReport();
        }
    }
}
