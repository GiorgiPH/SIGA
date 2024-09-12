using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Presupuesto;
using PV.Properties;

namespace PV
{
    public partial class AgregarConceptoPresProdc : Form
    {
        string concepto = string.Empty;
        DBPresupuesto c = new DBPresupuesto();

        public AgregarConceptoPresProdc(string Concepto)
        {
            InitializeComponent();
            concepto = Concepto;
        }

        private void AgregarConceptoPresProdc_Load(object sender, EventArgs e)
        {
            //c.SeleccionarProducto(cmbClase);
            c.CargarConceptoPresupuesto(dataGridView1, concepto);
            seleccionar();
        }

        void seleccionar ()
        {
            cmbClase.DataSource = AutoCompleClass.Datos();
            cmbClase.DisplayMember = "Descripcion";

            cmbClase.AutoCompleteCustomSource = AutoCompleClass.Autocomplete();
            cmbClase.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbClase.AutoCompleteSource = AutoCompleteSource.CustomSource;

            cmbClase.Text = "";
        }

        public static class AutoCompleClass
        {
            //metodo para cargar los datos de la bd
            public static DataTable Datos()
            {
                
                DataTable dt = new DataTable();

                SqlConnection cn = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

                string consulta = "Select PS.Descripcion from ProductosServicios as PS where not exists (select ProductoServicio from Concepto_Producto as CP where PS.ClaveProducto=CP.ProductoServicio)";
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
                    coleccion.Add(Convert.ToString(row["Descripcion"]));
                }

                return coleccion;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClase.Text != string.Empty)
            {
                string[] valores = c.InformacionPrductos(cmbClase.Text);
                try
                {
                    txtProducto.Text = valores[0];
                }
                catch (Exception)
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbClase.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el producto para continuar");
            }
            else
            {
                c.RegistroProducto(concepto, txtProducto.Text);
                c.CargarConceptoPresupuesto(dataGridView1, concepto);
                cmbClase.Text = null;
                txtProducto.Clear();
                seleccionar();
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Concepto1"].Value != null)
                    {
                        string Concepto = dataGridView1.Rows[e.RowIndex].Cells["Concepto1"].Value.ToString();
                        string producto = dataGridView1.Rows[e.RowIndex].Cells["ClaveProducto"].Value.ToString();
                        c.Eliminarproductoconcepto(Concepto, producto);

                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        c.CargarConceptoPresupuesto(dataGridView1, concepto);
                        seleccionar();
                    }
                }
            }
        }


    }
}
