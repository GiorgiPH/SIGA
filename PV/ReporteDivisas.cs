using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Divisas;

namespace PV
{
    public partial class ReporteDivisas : Form
    {
        DBDivisas c = new DBDivisas();

        public ReporteDivisas()
        {
            InitializeComponent();
        }

        private void ReporteDivisas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet3.Divisas' Puede moverla o quitarla según sea necesario.

            c.SeleccionarDivisa(cmbPropietario1);
            cmbPropietario1.Items.Insert(0, "TODOS");

            cmbPropietario1.SelectedIndex = 0;

            this.DivisasTableAdapter.Fill(this.ControlCondominiosDataSet3.Divisas);

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text== "TODOS")
            {
                this.DivisasTableAdapter.Fill(this.ControlCondominiosDataSet3.Divisas);

            }
            else
            {
                this.DivisasTableAdapter.FillBy(this.ControlCondominiosDataSet3.Divisas,cmbPropietario1.Text);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
