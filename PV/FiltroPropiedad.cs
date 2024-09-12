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
    public partial class FiltroPropiedad : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public FiltroPropiedad()
        {
            InitializeComponent();
        }

        private void FiltroPropiedad_Load(object sender, EventArgs e)
        {
            c.SeleccionarpROPIEDAD(cmbSeccion);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbSeccion.Text!= string.Empty)
            {
                ReporteIngresos.Propiedad = cmbSeccion.Text;
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
