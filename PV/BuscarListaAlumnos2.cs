using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;
using Condominios;
using PV;

namespace ControlAcademico
{
    public partial class BuscarListaAlumnos2 : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();

        public BuscarListaAlumnos2()
        {
            InitializeComponent();
            
        }

        private void ListaAlumno2_Load(object sender, EventArgs e)
        {
            c.BuscarAlumnos(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == string.Empty)
            {
                c.BuscarAlumnos(dataGridView1);
            }
            else
            {
                c.BuscarAlumnosFiltro(dataGridView1, txtFiltro.Text);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                GenerarRecibo.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                registroIngresos.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                registroIngresos.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                RegistrarAnticipo.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistrarAnticipo.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                AplicarAnticipo.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                AplicarAnticipo.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
