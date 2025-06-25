using Condominios.Clases.CentroCostos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.DatosEmpresa;
using PV.Clases.CentroCostos;
using PV.Clases.PedidoCliente;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PV
{
    public partial class DatosProyecto : Form
    {
        DBDatosProyecto c = new DBDatosProyecto();


        public string centrocosto = string.Empty;
        string folio;
        string proyecto = string.Empty;

        public DatosProyecto(string folio, string proyecto)
        {
            InitializeComponent();
            this.folio = folio;
            this.proyecto = proyecto;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void txtCuenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void DatosProyecto_Load(object sender, EventArgs e)
        {
            //txtfolio.Text = folio;
            c.ConsultaFolio(txtfolio);
            txtcentrocosto.Text = centrocosto;
         //   c.SeleccionarCentroCostos(cmbCentroCostos);
            c.SeleccionarUsuario(cmencargado);
            c.SeleccionarFormaPAgo2(cmbFormaPago);
            c.SeleccionarCatConceptosGlobales(cmbIVA);
            cmencargado.SelectedIndex = 0;
            cmbIVA.SelectedIndex = 0;
            CargarFormasPago(txtfolio.Text);

            if (!string.IsNullOrEmpty(proyecto))
            {

                txtfolio.Text = string.Empty;
                txtfolio.Text = folio;
                c.ConsultaproyectoSeleccionada(txtcentrocosto.Text, proyecto, folio, txtNombre, txtDescripcion, txtCuenta, txtencargado, txtclaveFP, txtFormaPago, RBreferencia, guna2TextBox3, txtiva, guna2TextBox2);
                CargarFormasPago(folio);
                dtgconsulta.Visible = false;

              
            }


        }
        private void CargarFormasPago(string folio)
        {
            try
            {
                var pagos = c.ObtenerFormasPagoProyectoo(folio);

                dgvFormasPagos.Rows.Clear();

                foreach (DataRow pago in pagos.Rows)
                {
                    int n = dgvFormasPagos.Rows.Add();
                    dgvFormasPagos.Rows[n].Cells[0].Value = pago["Id"];
                    dgvFormasPagos.Rows[n].Cells[1].Value = pago["Folio"];
                    dgvFormasPagos.Rows[n].Cells[2].Value = pago["IdFormaPago"];
                    dgvFormasPagos.Rows[n].Cells[3].Value = pago["DescripcionFormaPago"];
                    dgvFormasPagos.Rows[n].Cells[4].Value = pago["Referencia"];
                    dgvFormasPagos.Rows[n].Cells[5].Value = pago["DescripcionReferencia"];

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }
        private void dgvFormasPagos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvFormasPagos.Rows[e.RowIndex];
                MostrarFormasPago(fila);
                dgvFormasPagos.Visible = false;
                button4.Text = "Modificar";
            }
        }
        private void MostrarFormasPago(DataGridViewRow fila)
        {
            // Asegura que las celdas existen antes de acceder
            txtId.Text = fila.Cells[0].Value?.ToString() ?? "";

            txtclaveFP.Text = fila.Cells[2].Value?.ToString() ?? "";
            cmbFormaPago.Text = fila.Cells[3].Value?.ToString() ?? "";
            guna2TextBox3.Text = fila.Cells[5].Value?.ToString() ?? "";

            // Ejemplo adicional: casilla booleana o texto tipo estado
            bool estado = fila.Cells[4]?.Value?.ToString() == "Si"? true:false;
            if (estado)
            {
                guna2RadioButton1.Checked = true;

            }
            else
            {
                guna2RadioButton2.Checked = true;

            }


        }
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            c.SeleccionarFormaPAgo2(txtclaveFP, cmbFormaPago.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (guna2RadioButton1.Checked == false && guna2RadioButton2.Checked == false) {
                MessageBox.Show("Se debe selecionar la referencia");
            }
            else {
                string referenciaSeleccionada = string.Empty;
              
                if (guna2RadioButton1.Checked == true) {
                    referenciaSeleccionada = "SI";
                }
                else if (guna2RadioButton2.Checked == true) {
                    referenciaSeleccionada = "NO";
                }
                MessageBox.Show(c.RegistroFormaPagoproyecto(txtId.Text,txtfolio.Text, txtclaveFP.Text, cmbFormaPago.Text, referenciaSeleccionada, guna2TextBox3.Text));
                CargarFormasPago(txtfolio.Text);
                LimpiarFormaPago();
            }
        }

        void Limpiar()
        {
            txtfolio.Text = "";
            txtclaveFP.Text = "";
            cmbFormaPago.Items.Clear();
            guna2TextBox3.Text = "";
            txtcentrocosto.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtCuenta.Text = ""; 
            cmencargado.Items.Clear();
            cmbIVA.Items.Clear();
            guna2TextBox2.Text = "";
            txtId.Text = "";
        }
        void LimpiarFormaPago()
        {
            txtclaveFP.Text = "";
            cmbFormaPago.SelectedIndex=-1;
            guna2TextBox3.Text = "";
            guna2RadioButton2.Checked = false;
            txtFormaPago.Text = "";
            txtId.Text = "";
        }
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Se debe agregar el proyecto");
            }
            else
            {
                MessageBox.Show(c.RegistroDatosproyecto(txtfolio.Text, txtcentrocosto.Text, txtNombre.Text, txtDescripcion.Text, txtCuenta.Text, cmencargado.Text, cmbIVA.Text, guna2TextBox2.Text));
                Limpiar();
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dtgconsulta.Visible == false)
            {
                dtgconsulta.Visible = true;
                dtgconsulta.BringToFront();

                c.Cargardatosproyectos(dtgconsulta,txtcentrocosto.Text);

            }
            else {
                dtgconsulta.Visible = false;
            }

        }

        //________________________________________________________________________________________________
        //Categorias Registrados
      
        private void dtgconsulta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Text = "Modificar";

            txtfolio.Text = string.Empty;           
            string proyecto = dtgconsulta.Rows[e.RowIndex].Cells["Proyecto"].Value.ToString();
            string folio = dtgconsulta.Rows[e.RowIndex].Cells["Fol"].Value.ToString();
            txtfolio.Text = folio;
            c.ConsultaproyectoSeleccionada(txtcentrocosto.Text,proyecto,folio,txtNombre,txtDescripcion,txtCuenta,txtencargado,txtclaveFP, txtFormaPago, RBreferencia, guna2TextBox3, txtiva, guna2TextBox2);
            CargarFormasPago(folio);
            dtgconsulta.Visible = false;

            int index = cmencargado.FindStringExact(txtencargado.Text);
            if (index != -1)
            {
                cmencargado.SelectedIndex = index;
            }
            int index2 = cmbFormaPago.FindStringExact(txtFormaPago.Text);
            if (index2 != -1)
            {
                cmbFormaPago.SelectedIndex = index2;
            }
            int index3 = cmbIVA.FindStringExact(txtiva.Text);
            if (index3 != -1)
            {
                cmbIVA.SelectedIndex = index3;
            }
        }

        private void dtgconsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvFormasPagos.Visible = !dgvFormasPagos.Visible;
        }

       
    }
}
