using System;
using System.Windows.Forms;
using PV.Clases.ReservaAreaComun;

namespace MEDCON
{
    public partial class CancelarCitas : Form
    {
        DBReservaAreaComun c = new DBReservaAreaComun();

        public static string Fecha = string.Empty;
        string nombre = string.Empty;
        string fecha = string.Empty;
        string HoraCita = string.Empty;
        string CondominioCita = string.Empty;
        string AreaCita = string.Empty;

        public CancelarCitas()
        {
            InitializeComponent();
        }

        private void CancelarCitas_Load(object sender, EventArgs e)
        {
            BuscarFecha fecha = new BuscarFecha();
            fecha.ShowDialog();
        }

        void Ocultar()
        {
            PanelCitas.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (PanelCitas.Visible == true)
            {
                PanelCitas.Visible = false;
            }
            else if (PanelCitas.Visible == false)
            {
                PanelCitas.Visible = true;
            }
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

            }
            else
            {
                return;
            }
            c.CitaBuscar(nombre, fecha, HoraCita, CondominioCita, AreaCita, txtNombre, txtClavePaciente, txtFecha, txtHora, txtEstatus, txtNota, txtCondominio, txtAreaComun);
            PanelCitas.Visible = false;
            txtFechaCancelar.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void CancelarCitas_Activated(object sender, EventArgs e)
        {
            if (Fecha != "")
            {
                c.CargarCitas(dataGridView2, Fecha);
                Fecha = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Ocultar();
            BuscarFecha fecha = new BuscarFecha();
            fecha.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text != "")
            {
                MessageBox.Show(c.RegistrarCitaCancelada(txtNombre.Text, txtFecha.Text, txtHora.Text, txtClavePaciente.Text, txtFechaCancelar.Text, txtMotivo.Text, txtCondominio.Text, txtAreaComun.Text));
                limpiar();
                c.CargarCitas(dataGridView2, fecha);

            }
            else
            {
                MessageBox.Show("Ingrese el motivo de la cancelacion");
                txtMotivo.Focus();
                return;
            }

        }

        void limpiar()
        {
            txtNombre.Clear();
            txtFecha.Clear();
            txtHora.Clear();
            txtEstatus.Clear();
            txtNota.Clear();
            txtFechaCancelar.Clear();
            txtMotivo.Clear();
            txtCondominio.Clear();
            txtAreaComun.Clear();
            txtClavePaciente.Clear();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void CancelarCitas_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
