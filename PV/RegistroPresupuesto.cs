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
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class RegistroPresupuesto : Form
    {
        DBPresupuesto c = new DBPresupuesto();
        string Tipo = string.Empty;

        public RegistroPresupuesto(string tipo)
        {
            InitializeComponent();
            Tipo = tipo;
        
        }

        private void RegistroPresupuesto_Load(object sender, EventArgs e)
        {
            c.CargarPresupuesto(dataGridView1, Tipo);
            c.SeleccionarCondomini(cmbCondominio);
            txtTipo.Text = Tipo;

            if (Tipo == "Ingreso")
            {
                c.SeleccionarConcepto(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);

            }
            else
            {
                c.SeleccionarConcepto2(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
                c.SeleccionarCondomini2(cmbCondominio);

            }
            seleccionar();
            Limpiar();
        }

        void seleccionar()
        {
            txtEjercicio.DataSource = AutoCompleClass.Datos();
            txtEjercicio.DisplayMember = "Clave";

            txtEjercicio.AutoCompleteCustomSource = AutoCompleClass.Autocomplete();
            txtEjercicio.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtEjercicio.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtEjercicio.Text = "";
        }
       

        public static class AutoCompleClass
        {
            //metodo para cargar los datos de la bd
            public static DataTable Datos()
            {

                DataTable dt = new DataTable();

                SqlConnection cn = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

                string consulta = "Select Clave from Periodo group by Clave";
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
                    coleccion.Add(Convert.ToString(row["Clave"]));
                }

                return coleccion;
            }
        }

        private void txtClaveFormasPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtEnero_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtFebrero_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtMarzo_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAbril_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtMayo_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtJunio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtJulio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAgosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtSeptiembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtOctubre_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtNoviembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDiciembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void Moneda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtEnero_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtEnero);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtFebrero_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtFebrero);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtMarzo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMarzo);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtAbril_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAbril);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtMayo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMayo);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtJunio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtJunio);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtJulio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtJulio);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtAgosto_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAgosto);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtSeptiembre_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSeptiembre);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtOctubre_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtOctubre);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtNoviembre_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtNoviembre);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtDiciembre_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDiciembre);
            txtTotal.Text = (Convert.ToDecimal(txtEnero.Text) + Convert.ToDecimal(txtFebrero.Text) + Convert.ToDecimal(txtMarzo.Text) + Convert.ToDecimal(txtAbril.Text) + Convert.ToDecimal(txtMayo.Text) + Convert.ToDecimal(txtJunio.Text) + Convert.ToDecimal(txtJulio.Text) + Convert.ToDecimal(txtAgosto.Text) + Convert.ToDecimal(txtSeptiembre.Text) + Convert.ToDecimal(txtOctubre.Text) + Convert.ToDecimal(txtNoviembre.Text) + Convert.ToDecimal(txtDiciembre.Text)).ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEjercicio.Text == string.Empty)
            {
                Limpiar();
                groupBox1.Enabled = true;
            }
            else
            {
                Limpiar();
                groupBox1.Enabled = true;
            }
        }
        void Limpiar()
        {
            txtEjercicio.Text= null;
            cmbConcepto.Text = null;
            txtConcepto.Clear();
            c.SeleccionarConcepto(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
            txtEnero.Text = "0.00";
            txtFebrero.Text = "0.00";
            txtMarzo.Text = "0.00";
            txtAbril.Text = "0.00";
            txtMayo.Text = "0.00";
            txtJulio.Text = "0.00";
            txtJunio.Text = "0.00";
            txtAgosto.Text = "0.00";
            txtSeptiembre.Text = "0.00";
            txtOctubre.Text = "0.00";
            txtNoviembre.Text = "0.00";
            txtDiciembre.Text = "0.00";
            txtTotal.Text = "0.00";
            groupBox1.Enabled = false;
            cmbConcepto.Enabled = true;
            txtEjercicio.Enabled = true;
            cmbCondominio.Text = null;
            txtCondominio.Clear();
            cmbCondominio.Enabled = true;
        }

        void Limpiar2()
        {
            cmbConcepto.Text = null;
            txtConcepto.Clear();
            c.SeleccionarConcepto(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
            txtEnero.Text = "0.00";
            txtFebrero.Text = "0.00";
            txtMarzo.Text = "0.00";
            txtAbril.Text = "0.00";
            txtMayo.Text = "0.00";
            txtJulio.Text = "0.00";
            txtJunio.Text = "0.00";
            txtAgosto.Text = "0.00";
            txtSeptiembre.Text = "0.00";
            txtOctubre.Text = "0.00";
            txtNoviembre.Text = "0.00";
            txtDiciembre.Text = "0.00";
            txtTotal.Text = "0.00";
            cmbConcepto.Enabled = true;
            txtEjercicio.Enabled = true;
            cmbCondominio.Text = null;
            txtCondominio.Clear();
            cmbCondominio.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionConceptoPresupuesto(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtEjercicio.Text == string.Empty)
            {
                MessageBox.Show("Registre el ejercicio para continuar");
            }
            else if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
            }
            else if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto para continuar");
            }
            else
            {
                MessageBox.Show(c.RegistroPresupuesto(txtCondominio.Text, txtEjercicio.Text, Tipo, txtConcepto.Text, Convert.ToDecimal(txtEnero.Text), Convert.ToDecimal(txtFebrero.Text), Convert.ToDecimal(txtMarzo.Text), Convert.ToDecimal(txtAbril.Text), Convert.ToDecimal(txtMayo.Text), Convert.ToDecimal(txtJunio.Text), Convert.ToDecimal(txtJulio.Text), Convert.ToDecimal(txtAgosto.Text), Convert.ToDecimal(txtSeptiembre.Text), Convert.ToDecimal(txtOctubre.Text), Convert.ToDecimal(txtNoviembre.Text), Convert.ToDecimal(txtDiciembre.Text), Convert.ToDecimal(txtTotal.Text)));
                Limpiar2();
                c.CargarPresupuesto(dataGridView1, Tipo);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                cmbConcepto.Enabled = false;
                txtEjercicio.Enabled = false;
                cmbCondominio.Enabled = false;
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Ejercicio"].Value.ToString();
                string Concepto = dataGridView1.Rows[e.RowIndex].Cells["CConcepto"].Value.ToString();
                string Condominio = dataGridView1.Rows[e.RowIndex].Cells["CCondominio"].Value.ToString();
                c.ConsultaPresupuestoSeleccionado(Clave, Concepto, Condominio, txtConcepto, txtEnero, txtFebrero, txtMarzo, txtAbril, txtMayo, txtJunio, txtJulio, txtAgosto, txtSeptiembre, txtOctubre, txtNoviembre, txtDiciembre, txtTotal);
                txtEjercicio.Text = Clave;
                txtCondominio.Text = Condominio;

                if (Tipo == "Ingreso")
                {
                    c.SeleccionarConcepto3(cmbConcepto, txtEjercicio.Text, txtConcepto.Text);
                }
                else
                {
                    c.SeleccionarConcepto4(cmbConcepto, txtEjercicio.Text, txtConcepto.Text);
                }

                if (txtConcepto.Text != string.Empty)
                {
                    string[] valores = c.InformacionConceptoPresupuesto2(txtConcepto.Text);
                    cmbConcepto.Text = valores[0];
                }

                if (txtCondominio.Text != string.Empty && txtCondominio.Text != "GLOBAL")
                {
                    string[] valores2 = c.InformacionCondominio2(txtCondominio.Text);
                    cmbCondominio.Text = valores2[0];
                }
                else if(txtCondominio.Text == "GLOBAL")
                {
                    cmbCondominio.Text = "GLOBAL";
                }

                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;

            }
            else
            {
                return;
            }
        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominio.Text!=string.Empty && cmbCondominio.Enabled==true)
            {
                if (cmbCondominio.Text != "GLOBAL")
                {
                    string[] valores = c.InformacionCondominio(cmbCondominio.Text);
                    txtCondominio.Text = valores[0];
                }
                else if (cmbCondominio.Text == "GLOBAL")
                {
                    txtCondominio.Text = "GLOBAL";
                }

                if (Tipo == "Ingreso")
                {
                    c.SeleccionarConcepto(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
                }
                else
                {
                    c.SeleccionarConcepto2(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
         
                }
            }
        }

        private void txtEjercicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEjercicio.Text!=string.Empty && txtEjercicio.Enabled==true)
            {
                if (Tipo == "Ingreso")
                {
                    c.SeleccionarConcepto(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
                }
                else
                {
                    c.SeleccionarConcepto2(cmbConcepto, txtEjercicio.Text, txtCondominio.Text);
           
                }
            }
           
        }

        private void cmbFIltroCOnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFiltroCOnd_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroCOnd.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroConcep.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroEjer.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else
            {
                c.CargarPresupuesto(dataGridView1, Tipo);
            }
        }

        private void txtFiltroEjer_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroCOnd.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroConcep.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroEjer.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else
            {
                c.CargarPresupuesto(dataGridView1, Tipo);
            }
        }

        private void txtFiltroConcep_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroCOnd.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroConcep.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else if (txtFiltroEjer.Text != string.Empty)
            {
                c.CargarPresupuestoFiltro(dataGridView1, Tipo, txtFiltroCOnd.Text, txtFiltroEjer.Text, txtFiltroConcep.Text);
            }
            else
            {
                c.CargarPresupuesto(dataGridView1, Tipo);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportePresupuesto reportePresupuesto = new ReportePresupuesto(Tipo);
            reportePresupuesto.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (txtEjercicio.Text==string.Empty && cmbCondominio.Text==string.Empty && cmbConcepto.Text==string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    MessageBox.Show( c.EliminarConcepto(txtCondominio.Text, txtEjercicio.Text, txtConcepto.Text, txtTipo.Text));
                    Limpiar();
                    c.CargarPresupuesto(dataGridView1, Tipo);
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }
            
        }
    }
}
