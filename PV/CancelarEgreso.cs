using System;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;
using PV.Clases.Egresos;

namespace PV
{

    public partial class CancelarEgreso : Form
    {
        string clave = string.Empty;
        string tipo = string.Empty;
        string abono = string.Empty;
        string proveedor = string.Empty;
        public static int Opcion = 0;

        DBEgresos c = new DBEgresos();

        public CancelarEgreso(string Clave, string Tipo, string Abono, string Proveedor)
        {
            InitializeComponent();
            clave = Clave;
            tipo = Tipo;
            abono = Abono;
            proveedor = Proveedor;
        }

        private void CancelarEgreso_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtObservaciones.Text == string.Empty)
            {
                MessageBox.Show("Registre la obsercacion para la cancelacion");
            }
            else
            {
                AutentificarAdmin admin = new AutentificarAdmin();
                admin.ShowDialog();
            }
        }

        private void CancelarEgreso_Activated(object sender, EventArgs e)
        {
            if (Opcion== 1)
            {
                MessageBox.Show(c.ActualizarCancelarEgreso2(clave, tipo, txtObservaciones.Text, Convert.ToDecimal(abono), proveedor));
                Opcion = 0;
                this.Close();
            }
        }
    }
}
