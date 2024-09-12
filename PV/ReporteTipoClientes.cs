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
    public partial class ReporteTipoClientes : Form
    {
        public ReporteTipoClientes()
        {
            InitializeComponent();
        }

        private void ReporteTipoClientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet13.TipoCliente' Puede moverla o quitarla según sea necesario.
            this.TipoClienteTableAdapter.Fill(this.ControlCondominiosDataSet13.TipoCliente);

            this.reportViewer1.RefreshReport();
        }
    }
}
