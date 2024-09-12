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
    public partial class ReporteProveedor : Form
    {
        public ReporteProveedor()
        {
            InitializeComponent();
        }

        private void ReporteProveedor_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet26.Proveedor' Puede moverla o quitarla según sea necesario.
            this.ProveedorTableAdapter.Fill(this.ControlCondominiosDataSet26.Proveedor);

            this.reportViewer1.RefreshReport();
        }
    }
}
