using System;
using System.Windows.Forms;
using PV.Clases.Inventario;
using PV.Clases.TipoMovimiento;
using PuntoVentas;

namespace PV
{
    public partial class ConsultaInventario : Form
    {
        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBTipoMovimiento s = new DBTipoMovimiento();
        int Consulta = 0;

        public ConsultaInventario(int consulta)
        {
            InitializeComponent();
            Consulta = consulta;
        }

        private void ConsultaInventario_Load(object sender, EventArgs e)
        {
            c.CargarDocumento(dataGridView1);
            c.SeleccionarDivisa(cmbDivisa);
            if (Consulta==1)
            {
                button2.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();

             //   c.Consulta(Folio, Tipo, Documento, txtFolioRegistrar, txtDescripcion, txtTotalPartidas, txtReferencia, txtAlmacen, txtTotal, txtNotas, cmbEstatus, txtElaborado, cmbDivisa, txtTipoCambio);
             
                txtTipoDocumento.Text = Tipo;

                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void btnarticulos_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text != string.Empty)
            {
                ConsultaMovimientos mov = new ConsultaMovimientos(txtTipoDocumento.Text, txtDescripcion.Text, txtFolioRegistrar.Text);
                mov.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un documento");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbFiltro.Text = null;
            dtFiltro.ResetText();
            c.CargarDocumento(dataGridView1);
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.Text != string.Empty)
            {
                c.CargarDocumentoFiltro(dataGridView1, cmbFiltro.Text, dtFiltro.Text);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
