using System;
using System.Windows.Forms;
using Condominios.Clases.AreasComunes;
using PV;

namespace Condominios
{
    public partial class CatalogoAreasComunes : Form
    {
        DBAreasComunes c = new DBAreasComunes();
        
        public CatalogoAreasComunes()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button3, "Nuevo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Área");
            T.SetToolTip(button1, "Imprimir");
        }

        private void CatalogoAreasComunes_Load(object sender, EventArgs e)
        {
            //GenerarNoAreas();
            c.CargarAreas(dataGridView2);
        }

        void GenerarNoAreas()
        {
            DBAreasComunes.Folio = 0;
            c.ClaveAreaComunSiguiente();
            if (DBAreasComunes.Folio == 0)
            {
                DBAreasComunes.Folio = 1;
                txtClave.Text = Convert.ToString(DBAreasComunes.Folio);

            }
            else
            {
                DBAreasComunes.Folio = DBAreasComunes.Folio + 1;
                txtClave.Text = Convert.ToString(DBAreasComunes.Folio);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            //this.Close();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoAreas();
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtDescripcion.Clear();
            txtCaracteristicas.Clear();
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro");
            }
            else if (txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion del area comun");
            }
            else
            {
               MessageBox.Show( c.RegistroAreasComunes(txtClave.Text, txtDescripcion.Text, txtCaracteristicas.Text));
                Limpiar();
                //GenerarNoAreas();
                c.CargarAreas(dataGridView2);
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                c.ConsultaAreasSeleccionada(Clave, txtDescripcion, txtCaracteristicas);
                txtClave.Text = Clave;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteAreasComunes reporteAreasComunes = new ReporteAreasComunes();
            reporteAreasComunes.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                Limpiar();
                GenerarNoAreas();
                groupBox1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoAreas();
                groupBox1.Enabled = true;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
