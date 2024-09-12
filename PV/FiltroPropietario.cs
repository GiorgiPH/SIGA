using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;

namespace PV
{
    public partial class FiltroPropietario : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public FiltroPropietario()
        {
            InitializeComponent();
        }

        private void FiltroPropietario_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietarios(cmbPropietario1);
            c.SeleccionarPropietarios(cmbpropietario2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbPropietario1.Text!= string.Empty)
            {
                ReporteSaldoCondominio.propietario1 = cmbPropietario1.Text;
                ReporteSaldoCondominio.propietario2 = cmbpropietario2.Text;
                ReporteSaldoPropietario.propietario1 = cmbPropietario1.Text;
                ReporteSaldoPropietario.propietario2 = cmbpropietario2.Text;
                ReporteIngresos.Propietario1= cmbPropietario1.Text;
                ReporteIngresos.Propietario2 = cmbpropietario2.Text;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpropietario2.Text = cmbPropietario1.Text;
        }
    }
}
