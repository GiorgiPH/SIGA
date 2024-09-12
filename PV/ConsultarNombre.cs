using System;
using System.Windows.Forms;
using PV.Clases.ReservaAreaComun;
using PuntoVentas;

namespace MEDCON
{
    public partial class ConsultarNombre : Form
    {
        public static string Nombre = string.Empty;
        string nombre = string.Empty;
        string fecha = string.Empty;
        string HoraCita = string.Empty;
        string CondominioCita = string.Empty;
        string AreaCita = string.Empty;

        DBReservaAreaComun c = new DBReservaAreaComun();

        public ConsultarNombre()
        {
            InitializeComponent();
        }

        void limpiar()
        {
            txtNombre.Clear();
            txtFecha.Clear();
            txtHoraCita.Clear();
            txtEstatus.Clear();
            txtNotas.Clear();
            txtCondominio.Clear();
            txtAreaComun.Clear();
            txtClavePaciente.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtCorreo.Clear();
        }

        private void ConsultarNombre_Load(object sender, EventArgs e)
        {
            BuscarNombre buscar = new BuscarNombre();
            buscar.ShowDialog();

            string hoy = string.Empty;
            hoy = DateTime.Now.ToString("yyyy/MM/dd");
            c.CargarCitas2(dataGridView2, hoy, Nombre);
            PanelConsulta.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
            BuscarNombre nombre = new BuscarNombre();
            nombre.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuPrincipal.Opcion = 0;
            Nombre = string.Empty;
            this.Close();
        }

        private void ConsultarNombre_Activated(object sender, EventArgs e)
        {
            if (Nombre!= string.Empty)
            {
                string hoy = string.Empty;
                hoy = DateTime.Now.ToString("yyyy/MM/dd");
                c.CargarCitas2(dataGridView2, hoy, Nombre);
                PanelConsulta.Visible = true;
            }

            if (txtFecha.Text == string.Empty && txtHoraCita.Text == string.Empty)
            {
                txtFecha.Text = DisponibilidadHorario.Fecha;
                txtHoraCita.Text = DisponibilidadHorario.Horario;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClavePropietario = dataGridView2.Rows[e.RowIndex].Cells["ClavePropietario"].Value.ToString();
                nombre = ClavePropietario;
                string FechaCita = dataGridView2.Rows[e.RowIndex].Cells["FechaCita"].Value.ToString();
                fecha = Convert.ToDateTime(FechaCita).ToString("yyyy/MM/dd");
                HoraCita = dataGridView2.Rows[e.RowIndex].Cells["Hora"].Value.ToString();
                CondominioCita = dataGridView2.Rows[e.RowIndex].Cells["Condominio"].Value.ToString();
                AreaCita = dataGridView2.Rows[e.RowIndex].Cells["AreaComun"].Value.ToString();

                c.CitaBuscar3(nombre, fecha, HoraCita, CondominioCita, AreaCita, txtNombre, txtClavePaciente, txtFecha, txtHoraCita, txtEstatus, txtNotas, txtCondominio, txtAreaComun, txtTelefono, txtCelular, txtCorreo);
                PanelConsulta.Visible = false;

            }
            else
            {
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (PanelConsulta.Visible == true)
            {
                PanelConsulta.Visible = false;
            }
            else if (PanelConsulta.Visible == false)
            {
                PanelConsulta.Visible = true;
            }
        }

        private void txtFiltroNombre2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClavePropietario = dataGridView2.Rows[e.RowIndex].Cells["ClavePropietario"].Value.ToString();
                nombre = ClavePropietario;
                string FechaCita = dataGridView2.Rows[e.RowIndex].Cells["FechaCita"].Value.ToString();
                fecha = Convert.ToDateTime(FechaCita).ToString("yyyy/MM/dd");
                HoraCita = dataGridView2.Rows[e.RowIndex].Cells["Hora"].Value.ToString();
                CondominioCita = dataGridView2.Rows[e.RowIndex].Cells["Condominio"].Value.ToString();
                AreaCita = dataGridView2.Rows[e.RowIndex].Cells["AreaComun"].Value.ToString();

                c.CitaBuscar3(nombre, fecha, HoraCita, CondominioCita, AreaCita, txtNombre, txtClavePaciente, txtFecha, txtHoraCita, txtEstatus, txtNotas, txtCondominio, txtAreaComun, txtTelefono, txtCelular, txtCorreo);
                PanelConsulta.Visible = false;

            }
            else
            {
                return;
            }
        }
    }
}
