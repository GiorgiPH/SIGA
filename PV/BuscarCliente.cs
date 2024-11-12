using Guna.UI2.WinForms;
using PV.Clases.PedidoCliente;
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
    public partial class BuscarCliente : Form
    {
        DBPedidoCliente c = new DBPedidoCliente();
        public static string Cliente = string.Empty;
        public static string NombreCliente = string.Empty;
        public BuscarCliente()
        {
            InitializeComponent();
            c.BuscarClienteFiltro(dgvClientes, txtFiltro1.Text);
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            c.BuscarClienteFiltro(dgvClientes, txtFiltro1.Text);

        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Cliente = dgvClientes.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
                NombreCliente = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
