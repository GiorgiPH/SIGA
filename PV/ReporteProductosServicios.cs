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
    public partial class ReporteProductosServicios : Form
    {
        public ReporteProductosServicios()
        {
            InitializeComponent();
        }

        private void ReporteProductosServicios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet6.ProductosServicios' Puede moverla o quitarla según sea necesario.
            this.ProductosServiciosTableAdapter.Fill(this.ControlCondominiosDataSet6.ProductosServicios);

            this.reportViewer1.RefreshReport();
        }
    }
}
