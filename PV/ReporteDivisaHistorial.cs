using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuntoVentas.Clases.Divisas;


namespace PV
{
    public partial class ReporteDivisaHistorial : Form
    {
        DBDivisas c = new DBDivisas();

        public ReporteDivisaHistorial()
        {
            InitializeComponent();
        }

        private void ReporteDivisaHistorial_Load(object sender, EventArgs e)
        {
            c.SeleccionarDivisa(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet24.DivisasHistorial' Puede moverla o quitarla según sea necesario.
            this.DivisasHistorialTableAdapter.Fill(this.ControlCondominiosDataSet24.DivisasHistorial);

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS")
            {
                this.DivisasHistorialTableAdapter.Fill(this.ControlCondominiosDataSet24.DivisasHistorial);
            }
            else
            {
                this.DivisasHistorialTableAdapter.FillBy(this.ControlCondominiosDataSet24.DivisasHistorial, cmbPropietario1.Text);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
