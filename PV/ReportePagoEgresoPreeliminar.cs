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
    public partial class ReportePagoEgresoPreeliminar : Form
    {
        private readonly string documentosSeleccionados;

        public ReportePagoEgresoPreeliminar(string documentosSeleccionados)
        {
            InitializeComponent();
            this.documentosSeleccionados = documentosSeleccionados;
        }

        private void ReportePagoEgresoPreeliminar_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            this.sp_ListaPreliminarPagoProveedoresTableAdapter.Fill(
                     this.controlCondominiosDataSet49.sp_ListaPreliminarPagoProveedores,
                     documentosSeleccionados);

            this.reportViewer1.RefreshReport();
        }
    }
}
