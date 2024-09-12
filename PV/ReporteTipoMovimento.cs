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
    public partial class ReporteTipoMovimento : Form
    {
        public ReporteTipoMovimento()
        {
            InitializeComponent();
        }

        private void ReporteTipoMovimento_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet28.TipoMovimiento' Puede moverla o quitarla según sea necesario.
            this.TipoMovimientoTableAdapter.Fill(this.ControlCondominiosDataSet28.TipoMovimiento);

            this.reportViewer1.RefreshReport();
        }
    }
}
