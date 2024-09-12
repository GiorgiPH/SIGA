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
    public partial class PresupuestoHistorico : Form
    {
        string Tipo = string.Empty;

        public PresupuestoHistorico(string tipo)
        {
            InitializeComponent();
            Tipo = tipo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEjercicio.Text == string.Empty)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void PresupuestoHistorico_Load(object sender, EventArgs e)
        {
            txtTipo.Text = Tipo;
        }
    }
}
