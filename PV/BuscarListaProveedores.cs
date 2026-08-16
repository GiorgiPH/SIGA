using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class BuscarListaProveedores : Form
    {
        private readonly DBOrdenCompra c = new DBOrdenCompra();

        public string Matricula { get; private set; }
        public string Nombre { get; private set; }

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
                c.BuscarProveedor(dataGridView1);
            else
                c.BuscarAlumnosFiltro(dataGridView1, txtFiltro.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Matricula = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            Nombre = dataGridView1.Rows[e.RowIndex].Cells["Proveedor"].Value.ToString();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Antes solo cerraba sin fijar DialogResult; ahora queda explícito
            // como Cancel para que "if (frm.ShowDialog() == DialogResult.OK)" funcione bien.
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}