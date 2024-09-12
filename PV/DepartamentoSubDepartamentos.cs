using System;
using System.Collections;
using System.Windows.Forms;
using Condominios.Clases.CentroCostos;

namespace Condominios
{
    public partial class DepartamentoSubDepartamentos : Form
    {
        string clave = string.Empty;
        string nombre = string.Empty;
        DBCentroCostos c = new DBCentroCostos();

        public DepartamentoSubDepartamentos(string Clave, string Nombre)
        {
            InitializeComponent();
            clave = Clave;
            nombre = Nombre;
        }

        private void DepartamentoSubDepartamentos_Load(object sender, EventArgs e)
        {
            txtClaveCategoria.Text = clave;
            txtNombre.Text = nombre;
            c.ConsultaSubDepartamentoSeleccionada(txtClaveCategoria.Text, dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                    {
                        string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
                        c.EliminarSubDepartamento(Clave);

                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            int Fila = dataGridView1.CurrentRow.Index;

                            if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                            {
                                dataGridView1.Rows[Fila].Cells["ClaveFamilia"].Value = txtClaveCategoria.Text + '-' + (Fila + 1);
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value == null)
            {

                dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = null;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int Fila = dataGridView1.CurrentRow.Index + 1;

            dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClaveCategoria.Text + '-' + Fila;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != string.Empty)
            {
                ArrayList ListaConcept = new ArrayList();
                ArrayList ListaConcept2 = new ArrayList();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ClaveFamilia"].Value != null && row.Cells["Nombre"].Value != null)
                    {
                        ListaConcept.Add(row.Cells["ClaveFamilia"].Value.ToString());
                        ListaConcept2.Add(row.Cells["Nombre"].Value.ToString());

                    }
                }

                MessageBox.Show(c.RegistroSubDepartamento(txtClaveCategoria.Text, ListaConcept, ListaConcept2));
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
