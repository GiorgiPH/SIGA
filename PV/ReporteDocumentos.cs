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
    public partial class ReporteDocumentos : Form
    {
        public ReporteDocumentos()
        {
            InitializeComponent();
        }

        private void ReporteDocumentos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet11.Documento' Puede moverla o quitarla según sea necesario.
            this.DocumentoTableAdapter.Fill(this.ControlCondominiosDataSet11.Documento);

            this.reportViewer1.RefreshReport();
        }
    }
}
