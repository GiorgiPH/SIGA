using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.FiltroMovimiento;

namespace PV
{
    public partial class FiltroValorProducto : Form
    {
        DBFiltroMovimientos c = new DBFiltroMovimientos();

        public FiltroValorProducto()
        {
            InitializeComponent();
        }

        private void FiltroValorProducto_Load(object sender, EventArgs e)
        {
            c.Categorias(cmbCategoria);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Categoria = 0;

            if (txtclave.Text != string.Empty)
            {
                Categoria = Convert.ToInt32(txtclave.Text);
            }

            ReporteValorProducto reporteExistencias = new ReporteValorProducto(Categoria);
            reporteExistencias.ShowDialog();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.Text != string.Empty)
            {
                string[] valores = c.InformacionCategoria(cmbCategoria.Text);
                txtclave.Text = valores[0];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c.Categorias(cmbCategoria);
            txtclave.Clear();
        }

        private void txtclave_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

