using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using PV.Clases.PedidoCliente;

namespace PV
{
    public partial class BuscarDocumento : Form
    {
        DBPedidoCliente c = new DBPedidoCliente();
        public static string Conscutivo = string.Empty;
        public static string Nombre = string.Empty;
        public static string DocumentoO = string.Empty;
        
        public static string FolioO = string.Empty;
        string tipo = string.Empty;
        string cliente= string.Empty;
        public BuscarDocumento(string tipo, string cliente)
        {
            InitializeComponent();
            this.tipo = tipo;
            this.cliente = cliente;
            c.BuscarOrdenPedidoPendiente(dataGridView1, "", txtFiltro.Text, cliente);
        }

        private void BuscarListaProveedores_Load(object sender, EventArgs e)
        {
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                FolioO = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                Conscutivo = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                DocumentoO = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                //Nombre = dataGridView1.Rows[e.RowIndex].Cells["Proveedor1"].Value.ToString();
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
