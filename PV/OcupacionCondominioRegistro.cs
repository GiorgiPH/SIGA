using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.ConsultaCondominio;

namespace PV
{
    public partial class OcupacionCondominioRegistro : Form
    {
        DateTime FechaEntrada;
        string Entrada = string.Empty;
        string Condominio = string.Empty;
        DBConsultaCondominio c = new DBConsultaCondominio();

        public OcupacionCondominioRegistro(string condominio, DateTime fechaentrada, string entrada)
        {
            InitializeComponent();
            FechaEntrada = fechaentrada;
            Entrada = entrada;
            Condominio = condominio;
        }

        private void OcupacionCondominioRegistro_Load(object sender, EventArgs e)
        {
            c.ConsultaCondominioPropietario(Condominio, txtPropietario);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text == string.Empty)
            {
                MessageBox.Show("Registre el numero de Placas");
            }
            else if (txtPersonas.Text == string.Empty)
            {
                MessageBox.Show("registre el numero de Personas");
            }
            else
            {
                MessageBox.Show(c.RegistroAccesoNotas(Condominio, FechaEntrada, Entrada, txtPlaca.Text, txtPersonas.Text, txtNotas.Text));
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }
    }
}
