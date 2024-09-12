using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;
using PuntoVentas;

namespace PV
{
    public partial class AutentificarAdmin : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();

        public AutentificarAdmin()
        {
            InitializeComponent();
        }

        private void AutentificarAdmin_Load(object sender, EventArgs e)
        {
        }

        public void IniciarSesion(string Usuario, string Contraseña)
        {

            if (c.InicioSesion(txtUsuario.Text, txtContraseña.Text) != 0)
            {
                string TipoUsuario = DBRegistrarIngresos.TipoUsuario;
                string Estatus = DBRegistrarIngresos.Estatus;

                if (Estatus == "Activo")
                {
                    if (TipoUsuario == "Administrador")
                    {
                        CancelarEgreso.Opcion = 1;
                        CancelarIngreso.Opcion = 1;
                        OrdenCompra.Opcion = 1;
                        RegistrarEntrada.Opcion = 1;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no tiene permisos de administrador");
                        return;
                    }

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

        private void button1_Click(object sender, EventArgs e)
        {
            IniciarSesion(txtUsuario.Text, txtContraseña.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClaveDivisa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
