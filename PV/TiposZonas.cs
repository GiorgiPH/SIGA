using System;
using System.Windows.Forms;
using Condominios.Clases.TiposZonas;
using PuntoVentas.Clases.Login;
using PV;

namespace Condominios
{
    public partial class TiposZonas : Form
    {
        DBTiposZonas c = new DBTiposZonas();

        public TiposZonas()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Nuevo");
            T.SetToolTip(button6, "Imprimir");
            T.SetToolTip(button7, "Nuevo");
            T.SetToolTip(button9, "Imprimir");
        }

        private void TiposZonas_Load(object sender, EventArgs e)
        {
            //GenerarNoCliente();
            //GenerarNoZona();
            c.CargarClientes(dataGridView2);
            c.CargarZonas(dataGridView1);
        }

        void GenerarNoCliente()
        {
            DBTiposZonas.Cliente = 0;
            c.ClaveTipoSiguiente();
            if (DBTiposZonas.Cliente == 0)
            {
                DBTiposZonas.Cliente = 1;
                txtClaveCliente.Text = Convert.ToString(DBTiposZonas.Cliente);

            }
            else
            {
                DBTiposZonas.Cliente = DBTiposZonas.Cliente + 1;
                txtClaveCliente.Text = Convert.ToString(DBTiposZonas.Cliente);

            }
        }

        void GenerarNoZona()
        {
            DBTiposZonas.Zona = 0;
            c.ClaveZonaSiguiente();
            if (DBTiposZonas.Zona == 0)
            {
                DBTiposZonas.Zona = 1;
                txtClaveZona.Text = Convert.ToString(DBTiposZonas.Zona);

            }
            else
            {
                DBTiposZonas.Zona = DBTiposZonas.Zona + 1;
                txtClaveZona.Text = Convert.ToString(DBTiposZonas.Zona);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtDescripcionZona.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion de la zona para continuar");
            }
            else
            {
                c.RegistroZona(txtClaveZona.Text, txtDescripcionZona.Text);
                Limpiar();
                GenerarNoCliente();
                GenerarNoZona();
                c.CargarClientes(dataGridView2);
                c.CargarZonas(dataGridView1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtDescripcionCliente.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion del tipo de cliente para continuar");
            }
            else
            {
                c.RegistroCliente(txtClaveCliente.Text, txtDescripcionCliente.Text);
                Limpiar();
                GenerarNoCliente();
                GenerarNoZona();
                c.CargarClientes(dataGridView2);
                c.CargarZonas(dataGridView1);
            }
        }

        void Limpiar()
        {
            txtClaveCliente.Clear();
            txtClaveZona.Clear();
            txtDescripcionZona.Clear();
            txtDescripcionCliente.Clear();
            txtDescripcionCliente.Enabled = false;
            txtDescripcionZona.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            GenerarNoCliente();
            GenerarNoZona();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            GenerarNoCliente();
            GenerarNoZona();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["ClaveCliente"].Value.ToString();
                c.ConsultaClienteSeleccionado(Clave, txtDescripcionCliente);
                txtClaveCliente.Text = Clave;
            }
            else
            {
                return;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveZona"].Value.ToString();
                c.ConsultaZonaSeleccionado(Clave, txtDescripcionZona);
                txtClaveZona.Text = Clave;
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteTipoClientes reporteTipoClientes = new ReporteTipoClientes();
            reporteTipoClientes.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReporteZonas reporteZonas = new ReporteZonas();
            reporteZonas.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
                Limpiar();
                GenerarNoCliente();
            txtDescripcionCliente.Enabled = true;
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Limpiar();
            GenerarNoZona();
            txtDescripcionZona.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveCliente.Text == string.Empty && txtDescripcionCliente.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveCliente.Text));

                        c.CargarClientes(dataGridView2);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtClaveZona.Text == string.Empty && txtDescripcionZona.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa2(txtClaveZona.Text));

                        c.CargarZonas(dataGridView1);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }

        }
    }
}
