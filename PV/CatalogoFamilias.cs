using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using PuntoVentas.Clases.CategoriasFamilias;
using PV;
using PuntoVentas.Clases.Login;

namespace PuntoVentas
{
    public partial class CatalogoFamilias : Form
    {
        DBCategoriasFamilias c = new DBCategoriasFamilias();

        public CatalogoFamilias()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nueva Familia");
            T.SetToolTip(button8, "Consultar Familia");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button9, "Imprimir");
            
        }

        private void CatalogoFamilias_Load(object sender, EventArgs e)
        {
            //GenerarNoCategoria();
            c.CargarCategorias(dataGridView2);
        }

        void GenerarNoCategoria()
        {
            DBCategoriasFamilias.Folio = 0;
            c.ClaveCategoriaSiguiente();
            if (DBCategoriasFamilias.Folio == 0)
            {
                DBCategoriasFamilias.Folio = 1;
                txtClaveCategoria.Text = Convert.ToString(DBCategoriasFamilias.Folio);

            }
            else
            {
                DBCategoriasFamilias.Folio = DBCategoriasFamilias.Folio + 1;
                txtClaveCategoria.Text = Convert.ToString(DBCategoriasFamilias.Folio);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                    {
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
                else
                {
                    return;
                }
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Agregar")
            {
                if (e.RowIndex != -1)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Selected = true;
                    dataGridView1.BeginEdit(true);
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int Fila = dataGridView1.CurrentRow.Index + 1;

            dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClaveCategoria.Text + '-' + Fila;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value == null)
            {

                dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = null;
            }
        }

        void Limpiar()
        {
            txtClaveCategoria.Clear();
            txtNombre.Clear();
            dataGridView1.Rows.Clear();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtNombre.Text != string.Empty)

            {
                ArrayList ListaConcept = new ArrayList();
                ArrayList ListaConcept2 = new ArrayList();
                ArrayList ListaConcept3 = new ArrayList();
               // MessageBox.Show("1");
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                 
                    if (row.Cells["ClaveFamilia"].Value != null && row.Cells["Nombre"].Value != null)
                    {
                        ListaConcept.Add(row.Cells["ClaveFamilia"].Value.ToString());
                        ListaConcept2.Add(row.Cells["Nombre"].Value.ToString());
                        ListaConcept3.Add(row.Cells["Vincular"].Value.ToString());
                   
                    }

                }
                
                MessageBox.Show(c.RegistroCategoriasFamilias(txtClaveCategoria.Text, txtNombre.Text, ListaConcept, ListaConcept2, ListaConcept3));
                
                Limpiar();
                
                c.CargarCategorias(dataGridView2);
            }
            else
            {
                MessageBox.Show("Registre el nombre de la categoria para continuar.");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCategoria();
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                string ClaveCategoria = dataGridView2.Rows[e.RowIndex].Cells["ClaveCategoria"].Value.ToString();
                c.ConsultaCategoriaSeleccionada(ClaveCategoria, txtNombre, dataGridView1);
                txtClaveCategoria.Text = ClaveCategoria;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
               
               
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteCategoriasFamilias reporteCategoriasFamilias = new ReporteCategoriasFamilias();
            reporteCategoriasFamilias.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
               
            }
            else
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
      
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveCategoria.Text));
                      
                        c.CargarCategorias(dataGridView2);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }

        }
    }
}
