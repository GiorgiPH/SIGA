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
    public partial class ReporteValorProducto : Form
    {
        int Categoria = 0;

        public ReporteValorProducto(int categoria)
        {
            InitializeComponent();
            Categoria = categoria;
        }

        private void ReporteValorProducto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet33.ProductosServicios' Puede moverla o quitarla según sea necesario.
            if (Categoria==0)
            {
                this.ProductosServiciosTableAdapter.Fill(this.ControlCondominiosDataSet33.ProductosServicios);
            }
            else if (Categoria != 0)
            {
                this.ProductosServiciosTableAdapter.FillBy(this.ControlCondominiosDataSet33.ProductosServicios, Categoria);
            }
           
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            this.reportViewer1.RefreshReport();
        }
    }
}
