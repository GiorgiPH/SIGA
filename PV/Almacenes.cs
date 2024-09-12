using System;
using System.Windows.Forms;
using PV.Clases;
using PV.Clases.Almacenes;

namespace PV
{
    public partial class Almacenes : Form
    {
        DBAlmacenes c = new DBAlmacenes();

        public Almacenes()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nuevo Almacén");
            T.SetToolTip(button8, "Consultar Almacén");
            T.SetToolTip(button9, "Imprimir Almacén");
            T.SetToolTip(guna2CircleButton1, "Menú");
            guna2GradientPanel1.FillColor = colores.colorPrincipal;
            guna2GradientPanel1.FillColor2 = colores.colorSecundario;


        }

        private void Almacenes_Load(object sender, EventArgs e)
        {
            c.CargarAlmacenes(dataGridView1);

        }

        void GenerarNoActivo()
        {
            DBAlmacenes.Folio = 0;
            c.ClaveSiguiente();
            if (DBAlmacenes.Folio == 0)
            {
                DBAlmacenes.Folio = 1;
                txtClaveDivisa.Text = Convert.ToString(DBAlmacenes.Folio);

            }
            else
            {
                DBAlmacenes.Folio = DBAlmacenes.Folio + 1;
                txtClaveDivisa.Text = Convert.ToString(DBAlmacenes.Folio);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text == string.Empty)
            {

                GenerarNoActivo();
                guna2ToggleSwitch1.Checked = false;
                groupBox1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoActivo();
                guna2ToggleSwitch1.Checked = false;
                groupBox1.Enabled = true;
            }
        }

        void Limpiar()
        {
            txtClaveDivisa.Clear();
            guna2ToggleSwitch1.Checked = false;
            txtCuentaContable.Clear();
            txtNombre.Clear();
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
        }

       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                c.ConsultaAlmacenSeleccionada(Clave, guna2ToggleSwitch1, txtNombre, txtCuentaContable);
                txtClaveDivisa.Text = Clave;
                groupBox1.Enabled = true;
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
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

      

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteAlamacenes reporteAlamacenes = new ReporteAlamacenes();
            reporteAlamacenes.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre el nombre del almacen para continuar");
            }
            
            else
            {
                string Estatus = string.Empty;
                if (guna2ToggleSwitch1.Checked)
                {
                    Estatus = " Activo";
                }
                else
                {
                    Estatus = "Inactivo";
                }
                MessageBox.Show(c.RegistroAlmacen(txtClaveDivisa.Text, Estatus, txtNombre.Text, txtCuentaContable.Text));
                c.CargarAlmacenes(dataGridView1);
                Limpiar();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                label1.Text = "Activo";
            }
            else
            {
                label1.Text = "Inactivo";
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
