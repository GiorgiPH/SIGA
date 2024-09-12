using PuntoVentas;
using PuntoVentas.Clases.Login;
using PV;
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
    public partial class LoginDemo : Form
    {
        public static string UsuarioLogin = string.Empty;
        public static string Entrada = string.Empty;
        public static DateTime FechaEntrada = DateTime.Today;

        DBLogin c = new DBLogin();
        public LoginDemo()
        {
            InitializeComponent();
        }

        public void IniciarSesion(string Usuario, string Contraseña)
        {

            if (c.InicioSesion(txtUsuario.Text, txtContraseña.Text) != 0)
            {
                string TipoUsuario = DBLogin.TipoUsuario;
                string Estatus = DBLogin.Estatus;
                if (Estatus == "Activo")
                {
                   // PV.Opcion = 1;
                    UsuarioLogin = txtUsuario.Text;

                    string Hoy = DateTime.Today.ToString();
                    FechaEntrada = Convert.ToDateTime(Hoy);

                    string HoraEntrada = DateTime.Now.ToString("HH");
                    string MinutoEntrada = DateTime.Now.ToString("mm");
                    string SegundoEntrada = DateTime.Now.ToString("ss tt");
                    Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                    c.RegistroAcceso(txtUsuario.Text, txtContraseña.Text, FechaEntrada, Entrada);

                    this.Hide();

                    MenuPrincipal menu = new MenuPrincipal();
                    //MenuDemo menu = new MenuDemo();

                    menu.Show();
                }
                else
                {
                    MessageBox.Show("El usuario esta Inactivo");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrecta");
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            IniciarSesion(txtUsuario.Text, txtContraseña.Text);
        }
    }
}
