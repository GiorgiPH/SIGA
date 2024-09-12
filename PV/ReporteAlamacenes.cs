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
    public partial class ReporteAlamacenes : Form
    {
        public ReporteAlamacenes()
        {
            InitializeComponent();
        }

        private void ReporteAlamacenes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet25.Almacenes' Puede moverla o quitarla según sea necesario.
            this.AlmacenesTableAdapter.Fill(this.ControlCondominiosDataSet25.Almacenes);

            this.reportViewer1.RefreshReport();
        }
    }
}
