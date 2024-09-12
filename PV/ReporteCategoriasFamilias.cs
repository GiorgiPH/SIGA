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
    public partial class ReporteCategoriasFamilias : Form
    {
        public ReporteCategoriasFamilias()
        {
            InitializeComponent();
        }

        private void ReporteCategoriasFamilias_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet4.Categorias' Puede moverla o quitarla según sea necesario.
            this.CategoriasTableAdapter.Fill(this.ControlCondominiosDataSet4.Categorias);

            this.reportViewer1.RefreshReport();

        }
    }
}
