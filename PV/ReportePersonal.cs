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
    public partial class ReportePersonal : Form
    {
        public ReportePersonal()
        {
            InitializeComponent();
        }

        private void ReportePersonal_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet5.Empleados' Puede moverla o quitarla según sea necesario.
            this.EmpleadosTableAdapter.Fill(this.ControlCondominiosDataSet5.Empleados);

            this.reportViewer1.RefreshReport();
        }
    }
}
