using System;
using System.Drawing;
using System.Windows.Forms;
using PuntoVentas.Clases.Usuarios;
using PuntoVentas.Clases.Login;
using PV;

namespace PuntoVentas
{
    public partial class Usuarios : Form
    {
        DBUsuarios c = new DBUsuarios();
        DBLogin s = new DBLogin();
        string usuario = string.Empty;


        public Usuarios()
        {
            InitializeComponent();
        }

        void Limpiar()
        {
            txtUsuario.Enabled = true;
            txtUsuario.Clear();
            txtNombre.Enabled = true;
            txtNombre.Clear();
            txtContraseña.Enabled = true;
            txtContraseña.Clear();
            cmbTipo.Enabled = true;
            cmbTipo.ResetText();
            dpFecha.ResetText();
            cmbEstatus.ResetText();
            txtNota.Clear();
            Foto.Image = null;
            PanelUsuario.Visible = false;
            cmbTipo.Text = null;



           
            // PanelUsuario.Visible = false;
            ToolTip T = new ToolTip();
            T.SetToolTip(guna2Button6, "Nuevo Usuario");
            T.SetToolTip(guna2Button5, "Consultar");
            T.SetToolTip(guna2Button7, "Permisos");

        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            c.CargarUsuarioss(dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
      /*      if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(c.RegistroUsuario(txtUsuario.Text, txtNombre.Text, txtContraseña.Text, cmbTipo.Text, dpFecha.Text, cmbEstatus.Text, txtNota.Text, Foto));
            Limpiar();
            c.CargarUsuarioss(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void Usuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            string Hoy = DateTime.Today.ToString();
            DateTime FechaSalida = Convert.ToDateTime(Hoy);

            string HoraSalida = DateTime.Now.ToString("HH");
            string MinutoSalida = DateTime.Now.ToString("mm");
            string SegundoSalida = DateTime.Now.ToString("ss tt");
            string Salida = HoraSalida + ":" + MinutoSalida + ":" + SegundoSalida;

            s.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);

            PuntoVentas.Opcion = 0;
            PuntoVentas med = new PuntoVentas();
            med.Show();
            this.Hide();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
               

                Limpiar();
                string Usuario = dataGridView1.Rows[e.RowIndex].Cells["Usuarios1"].Value.ToString();
                usuario = Usuario;
                txtUsuario.Text = usuario;
                txtUsuario.Enabled = false;
                guna2Button7.Enabled = true;
                c.ConsultaUsuarioSeleccionado(usuario, txtNombre, txtContraseña, cmbTipo, dpFecha, cmbEstatus, txtNota, Foto);
                PanelUsuario.Visible = false;
                if (txtUsuario.Text == "Admin")
                {
                    txtNombre.Enabled = false;
                    cmbTipo.Enabled = false;
                    guna2Button7.Enabled = false;
                }
            }
            else
            {
                return;
            }

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();

            Abrir.Filter = "Archivos JPEG(* .JPEG) |*.jpg";
            Abrir.InitialDirectory = "C:/";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                Bitmap foto = new Bitmap(Dir);

                Foto.Image = (Image)foto;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtUsuario.Text == string.Empty)
            {
                txtUsuario.Focus();
            }
            else
            {
                Limpiar();
                txtUsuario.Focus();
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();

            Abrir.Filter = "Archivos JPEG(* .JPEG) |*.jpg";
            Abrir.InitialDirectory = "C:/";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                Bitmap foto = new Bitmap(Dir);

                Foto.Image = (Image)foto;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(c.RegistroUsuario(txtUsuario.Text, txtNombre.Text, txtContraseña.Text, cmbTipo.Text, dpFecha.Text, cmbEstatus.Text, txtNota.Text, Foto));
            Limpiar();
            c.CargarUsuarioss(dataGridView1);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == string.Empty)
            {
                txtUsuario.Focus();
                txtUsuario.Enabled = true;
                txtNombre.Enabled = true;
                txtContraseña.Enabled = true;
            }
            else
            {
                Limpiar();
                txtUsuario.Focus();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            PanelUsuario.BringToFront();
            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
             
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;

            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            PanelUsuario.Visible = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            UsuarioPermisos UP = new UsuarioPermisos();
            UP.ShowDialog();
        }
    }
}
