using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using PV.Clases.ReservaAreaComun;
using PV.Properties;
using System.Drawing;
using PV;
using Condominios;

namespace MEDCON
{
    public partial class RegistrarCitas : Form
    {
        DBReservaAreaComun c = new DBReservaAreaComun();

        public static string NombreP = string.Empty;
        public static string ApellidoPa = string.Empty;
        public static string Condominio = string.Empty;

        public RegistrarCitas()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtClavePaciente.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtCorreo.Clear();
            txtFecha.Clear();
            txtHoraCita.Clear();
            cmbEstatus.Text = string.Empty;
            txtNotas.Clear();
            DisponibilidadHorario.Fecha = string.Empty;
            DisponibilidadHorario.Horario = string.Empty;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClavePaciente_Enter(object sender, EventArgs e)
        {
            string Nombre = txtNombre.Text;
            DBReservaAreaComun.IdPropietario = 0;
            c.Clave(Nombre);

            if (DBReservaAreaComun.IdPropietario != 0)
            {
                txtClavePaciente.Text = Convert.ToString(DBReservaAreaComun.IdPropietario);
                txtTelefono.Text = DBReservaAreaComun.Telefono;
                txtCelular.Text = DBReservaAreaComun.Celular;
                txtCorreo.Text = DBReservaAreaComun.Correo;
                txtNombre.Text = DBReservaAreaComun.RazonSocial;

            }
            else
            {
                MessageBox.Show("El propietario no esta registrado");
                txtClavePaciente.Text = "N/A";
            }
        }

        private void RegistrarCitas_Load(object sender, EventArgs e)
        {
            limpiar();
            txtNombre.Focus();
            c.SeleccionarCondominio(cmbCondominios);
            cmbEstatus.SelectedIndex = 0;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtCondominio.Text != string.Empty && txtAreaComun.Text != string.Empty)
            {
                DisponibilidadHorario disp = new DisponibilidadHorario(txtCondominio.Text, txtAreaComun.Text);
                disp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un Condominio y un Area Comun");
            }

        }

        private void RegistrarCitas_Activated(object sender, EventArgs e)
        {
            txtFecha.Text = DisponibilidadHorario.Fecha;
            txtHoraCita.Text = DisponibilidadHorario.Horario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClavePaciente.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el Propietario para continuar");
                txtNombre.Focus();
                return;
            }
            else if (cmbCondominios.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el Condominio para continuar");
                return;
            }
            else if (cmbAreasComunes.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el Area Comun para continuar");
                return;
            }
            else if (txtFecha.Text == string.Empty && txtHoraCita.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el horario para continuar");
                return;
            }
            else if (cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el estatus para continuar");
                cmbEstatus.Focus();
                return;
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Ingrese el nombre de propietario o quien reserva para continuar");
                cmbEstatus.Focus();
                return;
            }
            else
            {
                MessageBox.Show(c.RegistrarCita(txtCondominio.Text, txtAreaComun.Text, txtClavePaciente.Text, txtTelefono.Text, txtCelular.Text, txtCorreo.Text, txtFecha.Text, txtHoraCita.Text, cmbEstatus.Text, txtNotas.Text));
                ImprimirReserva imprimirReserva = new ImprimirReserva(txtCondominio.Text, txtAreaComun.Text, txtClavePaciente.Text, txtHoraCita.Text, txtFecha.Text);
                imprimirReserva.ShowDialog();
                limpiar();
                DisponibilidadHorario.Fecha = string.Empty;
                DisponibilidadHorario.Horario = string.Empty;
                txtNombre.Focus();
            }

        }

        private void RegistrarCitas_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txtNombre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public static class AutoCompleClass
        {
            //metodo para cargar los datos de la bd
            public static DataTable Datos()
            {
                DataTable dt = new DataTable();

                SqlConnection cn = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

                string consulta = "select distinct P.RazonSocial from Propietarios_Condominios as PC, Propietarios as P where PC.ClavePropietario=P.IdPropietario";
                SqlCommand cmd = new SqlCommand(consulta, cn);

                SqlDataAdapter adap = new SqlDataAdapter(cmd);

                adap.Fill(dt);
                return dt;
            }

            //metodo para cargar la coleccion de datos para el autocomplete
            public static AutoCompleteStringCollection Autocomplete()
            {
                DataTable dt = Datos();

                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                //recorrer y cargar los items para el autocompletado
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["RazonSocial"]));
                }

                return coleccion;
            }
        }

        private void cmbCondominios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominios.Text);
                txtCondominio.Text = valores[0];
                Condominio = txtCondominio.Text;

                c.SeleccionarAreaComun(cmbAreasComunes, txtCondominio.Text);

                txtNombre.DataSource = AutoCompleClass.Datos();
                txtNombre.DisplayMember = "RazonSocial";

                txtNombre.AutoCompleteCustomSource = AutoCompleClass.Autocomplete();
                txtNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

                txtNombre.Text = string.Empty;
            }
        }

        private void cmbAreasComunes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAreasComunes.Text != string.Empty)
            {
                string[] valores = c.InformacionAreaComun(cmbAreasComunes.Text);
                txtAreaComun.Text = valores[0];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GenerarRecibo generarRecibo = new GenerarRecibo();
            generarRecibo.ShowDialog();
        }
    }
}
