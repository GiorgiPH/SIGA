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
    public partial class ReportePropietarios : Form
    {
        public ReportePropietarios()
        {
            InitializeComponent();
        }

        private void ReportePropietarios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet12.Propietarios' Puede moverla o quitarla según sea necesario.
            this.PropietariosTableAdapter.Fill(this.ControlCondominiosDataSet41.Propietarios);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
