using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class BuscarListaProveedores : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();

        public BuscarListaProveedores()
        {
            InitializeComponent();
        }

        private void BuscarListaProveedores_Load(object sender, EventArgs e)
        {
            c.BuscarProveedor(dataGridView1);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == string.Empty)
            {
                c.BuscarProveedor(dataGridView1);
            }
            else
            {
                c.BuscarAlumnosFiltro(dataGridView1, txtFiltro.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                OrdenCompra.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RecepcionProductos2.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistroGastos.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistroGastos2.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistroEgreso.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistroEgreso.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                NotasCargo.Matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistrarAnticipo.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                RegistrarAnticipo.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                AplicarAnticipoProveedor.matricula = dataGridView1.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
                AplicarAnticipoProveedor.nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
