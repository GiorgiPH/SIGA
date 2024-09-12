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
    public partial class ReporteServicios : Form
    {
        public ReporteServicios()
        {
            InitializeComponent();
        }

        private void ReporteServicios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet58.Servicios' Puede moverla o quitarla según sea necesario.
            this.ServiciosTableAdapter.Fill(this.ControlCondominiosDataSet58.Servicios);

            this.reportViewer1.RefreshReport();
        }
    }
}
