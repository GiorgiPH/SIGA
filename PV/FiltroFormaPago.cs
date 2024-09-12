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
    public partial class FiltroFormaPago : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public FiltroFormaPago()
        {
            InitializeComponent();
        }

        private void FiltroFormaPago_Load(object sender, EventArgs e)
        {
            c.SeleccionarFormaPAgo2(cmbFormaPago);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbFormaPago.Text!= string.Empty)
            {
                ReporteIngresos.FormaPago = cmbFormaPago.Text;
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            this.Close();
        }
    }
}
