using System;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;

namespace PV
{
    public partial class CancelarIngreso : Form
    {
        string clave = string.Empty;
        string abono = string.Empty;
        string propietario = string.Empty;
        public static int Opcion = 0;
        DBRegistrarIngresos c = new DBRegistrarIngresos();


        public CancelarIngreso(string Clave, string Abono, string Propietario)
        {
            InitializeComponent();
            clave = Clave;
            abono = Abono;
            propietario = Propietario;
        }

        private void CancelarIngreso_Load(object sender, EventArgs e)
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

        private void CancelarIngreso_Activated(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                MessageBox.Show(c.ActualizarCancelarIngreso2(clave, txtObservaciones.Text, Convert.ToDecimal(abono), propietario));
                Opcion = 0;
                this.Close();
            }
            
        }
    }
}
