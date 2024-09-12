using System;
using System.Collections;
using System.Windows.Forms;
using Condominios.Clases.Condominios;

namespace Condominios
{
    public partial class DepartamentoAreas : Form
    {
        DBCondominios c = new DBCondominios();

        string Nombre = string.Empty;
        string Clave = string.Empty;

        public DepartamentoAreas(string nombre, string clave)
        {
            InitializeComponent();
            Nombre = nombre;
            Clave = clave;
        }

        private void DepartamentoAreas_Load(object sender, EventArgs e)
        {
            txtClaveCategoria.Text = Clave;
            txtNombre.Text = Nombre;
            c.CargarAreas(txtClaveCategoria.Text, dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList ListaConcept = new ArrayList();
            ArrayList ListaConcept2 = new ArrayList();
            DataGridViewCheckBoxCell oCell;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                oCell = row.Cells["Seleccionar"] as DataGridViewCheckBoxCell;
                bool bChecked = (null != oCell && null != oCell.Value && true == (bool)oCell.Value);
                if (true == bChecked)
                {
                    //ListaConcept.Add(row.Cells["ClaveA"].Value.ToString());
                    //ListaConcept2.Add(row.Cells["AreaComun"].Value.ToString());

                    string clavea = row.Cells["ClaveA"].Value.ToString();
                    string AreaComun = row.Cells["AreaComun"].Value.ToString();
                    string Reservar = string.Empty;
                    string Cuota = string.Empty;
                    string Deposito = string.Empty;
                    if (row.Cells["Reservar"].Value != null)
                    {
                        Reservar = row.Cells["Reservar"].Value.ToString();
                    }
                    if (row.Cells["Cuota"].Value != null)
                    {
                        Cuota = row.Cells["Cuota"].Value.ToString();
                    }
                    if (row.Cells["Deposito"].Value != null)
                    {
                        Deposito = row.Cells["Deposito"].Value.ToString();
                    }

                    c.RegistrarAreasSeleccionadas(txtClaveCategoria.Text, clavea, AreaComun, Reservar, Cuota, Deposito);
                }
            }
            this.Close();
        }

    }
}
