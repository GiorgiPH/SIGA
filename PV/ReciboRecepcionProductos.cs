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
    public partial class ReciboRecepcionProductos : Form
    {
        string folio = string.Empty;

        public ReciboRecepcionProductos(string Folio)
        {
            InitializeComponent();
            folio = Folio;
        }
        private void ReciboRecepcionProductos_Load(object sender, EventArgs e)
        {
            controlCondominiosDataSet61.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'controlCondominiosDataSet23.DatosEmpresa' table. You can move, or remove it, as needed.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet62.RecepcionProducto' Puede moverla o quitarla según sea necesario.
            this.dataTable1TableAdapter.Fill(this.controlCondominiosDataSet61.DataTable1, Convert.ToInt32(folio));

            this.reportViewer1.RefreshReport();
        }
    }
}
