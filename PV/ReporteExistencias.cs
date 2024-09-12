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
    public partial class ReporteExistencias : Form
    {
        string Articulo = string.Empty;
        int Categoria = 0;

        public ReporteExistencias(string articulo, int categoria)
        {
            InitializeComponent();
            Articulo = articulo;
            Categoria = categoria;
        }

        private void ReporteExistencias_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet32.ProductosServicios' Puede moverla o quitarla según sea necesario.

             if (Articulo == "Todos" && Categoria == 0)
            {
                MessageBox.Show("4");
                this.ProductosServiciosTableAdapter.Fill(this.ControlCondominiosDataSet32.ProductosServicios);
            }
            else if (Articulo == "Todos" && Categoria > 0)
            {
                MessageBox.Show("5");
                this.ProductosServiciosTableAdapter.FillBy3(this.ControlCondominiosDataSet32.ProductosServicios, Categoria);
            }
            else if (Articulo != string.Empty && Categoria == 0)
            {
                MessageBox.Show("1");
                this.ProductosServiciosTableAdapter.FillBy2(this.ControlCondominiosDataSet32.ProductosServicios, Articulo);
            }
            else if (Articulo == string.Empty && Categoria != 0)
            {
                MessageBox.Show("2");
                this.ProductosServiciosTableAdapter.FillBy3(this.ControlCondominiosDataSet32.ProductosServicios, Categoria);
            }
            else if (Articulo != string.Empty && Articulo != "Todos" && Categoria != 0)
            {
                MessageBox.Show("3");
                this.ProductosServiciosTableAdapter.FillBy4(this.ControlCondominiosDataSet32.ProductosServicios, Articulo, Categoria);
            }
           

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
