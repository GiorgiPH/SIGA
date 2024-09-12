using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Presupuesto;

namespace PV
{
    public partial class ReportePresupuesto : Form
    {
        DBPresupuesto c = new DBPresupuesto();
        string Tipo = string.Empty;

        public ReportePresupuesto(string tipo)
        {
            InitializeComponent();
            Tipo = tipo;
        }

        private void ReportePresupuesto_Load(object sender, EventArgs e)
        {
            c.SeleccionarCondomini3(cmbpropietario2);
            c.SeleccionarEjercicio2(cmbPropietario1);
            cmbPropietario1.SelectedIndex = 0;
            cmbpropietario2.SelectedIndex = 0;
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet42.Presupuesto' Puede moverla o quitarla según sea necesario.

            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.Fill(this.ControlCondominiosDataSet42.Presupuesto, Tipo);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy1(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text, cmbpropietario2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy2(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbpropietario2.Text);
            }

            this.reportViewer1.RefreshReport();
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.Fill(this.ControlCondominiosDataSet42.Presupuesto, Tipo);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy1(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text, cmbpropietario2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy2(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbpropietario2.Text);
            }

            this.reportViewer1.RefreshReport();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.Fill(this.ControlCondominiosDataSet42.Presupuesto, Tipo);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text == "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text);
            }
            else if (cmbPropietario1.Text != "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy1(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbPropietario1.Text, cmbpropietario2.Text);
            }
            else if (cmbPropietario1.Text == "TODOS" && cmbpropietario2.Text != "TODOS")
            {
                this.PresupuestoTableAdapter.FillBy2(this.ControlCondominiosDataSet42.Presupuesto, Tipo, cmbpropietario2.Text);
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
