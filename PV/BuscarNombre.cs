using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using PV.Clases.ReservaAreaComun;
using PV.Properties;


namespace MEDCON
{
    public partial class BuscarNombre : Form
    {
        public BuscarNombre()
        {
            InitializeComponent();
        }

        private void BuscarNombre_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();

            txtNombre.DataSource = AutoCompleClass.Datos();
            txtNombre.DisplayMember = "RazonSocial";

            txtNombre.AutoCompleteCustomSource = AutoCompleClass.Autocomplete();
            txtNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtNombre.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text=="")
            {
                MessageBox.Show("Ingrese el nombre del propietario");
                txtNombre.Focus();
            }
            else
            {
                ConsultarNombre.Nombre = txtNombre.Text;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarNombre.Nombre = "";
            this.Close();
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

    }
}
