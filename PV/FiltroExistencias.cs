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
    public partial class FiltroExistencias : Form
    {
        DBFiltroMovimientos c = new DBFiltroMovimientos();

        public FiltroExistencias()
        {
            InitializeComponent();
        }

        private void FiltroExistencias_Load(object sender, EventArgs e)
        {
            
            c.SeleccionarAlmacen(cmbDe);
            c.Categorias(cmbCategoria);
            cmbDe.Items.Add("Todos");
            cmbDe.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbDe.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el almacen");
            }
            else
            {
                string Articulo = cmbDe.Text;
                int Categoria = 0;

                if (txtclave.Text != string.Empty)
                {
                    Categoria = Convert.ToInt32(txtclave.Text);
                }

                ReporteExistencias reporteExistencias = new ReporteExistencias(Articulo, Categoria);
                reporteExistencias.ShowDialog();
            }
           
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.Text!= string.Empty)
            {
                if (cmbCategoria.Text == "Todos")
                {
                    txtclave.Text = "0";
                }
                else
                {
                    string[] valores = c.InformacionCategoria(cmbCategoria.Text);
                    txtclave.Text = valores[0];
                }
            
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.SeleccionarProductoServ(cmbDe);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c.Categorias(cmbCategoria);
            txtclave.Clear();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (cmbDe.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el almacen");
            }
            else
            {
                string Articulo = cmbDe.Text;
                int Categoria = 0;

                if (txtclave.Text != string.Empty)
                {
                    Categoria = Convert.ToInt32(txtclave.Text);
                }

                ReporteExistencias reporteExistencias = new ReporteExistencias(Articulo, Categoria);
                reporteExistencias.ShowDialog();
            }
        }

        private void cmbDe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDe.Text == "Todos" )
            {
                cmbCategoria.Items.Add("Todos");
                cmbCategoria.SelectedIndex = 0;
            }
            else
            {
                cmbCategoria.Items.Clear();
                c.Categorias(cmbCategoria);
            }
          
        }
    }
}
