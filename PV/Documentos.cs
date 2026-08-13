using System;
using System.Windows.Forms;
using Condominios.Clases.Documentos;
using PuntoVentas;
using PV;
using PuntoVentas.Clases.Login;

namespace Condominios
{
    public partial class Documentos : Form
    {
        DBDocumentos c = new DBDocumentos();

        public Documentos()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nueva Forma de Pago");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Documentos");
            T.SetToolTip(button9, "Imprimir");
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoDocumento.Text == "Compra")
            {
                cmbCompras.Enabled = true;
                cmbCompras.Visible = true;
                cmbVentas.Visible = false;
                cmbVentas.Enabled = false;
                cmbInventarios.Visible = false;
                cmbInventarios.Enabled = false;
                groupBox4.Enabled = true;
                cmbtarea.Enabled = true;

                CargarTareas("Compras Gastos", "Compras Reembolso", "Compras Gastos Inventariables",
                             "Requisiciones", "Pedidos Proveedores");
            }
            else if (cmbTipoDocumento.Text == "Venta")
            {
                cmbVentas.Visible = true;
                cmbVentas.Enabled = true;
                cmbCompras.Visible = false;
                cmbCompras.Enabled = false;
                cmbInventarios.Visible = false;
                cmbInventarios.Enabled = false;
                groupBox4.Enabled = true;
                cmbtarea.Enabled = true;

                CargarTareas("Pedidos clientes", "Remisiones", "Factura", "Nota Crédito", "Nota Cargo");
            }
            else if (cmbTipoDocumento.Text == "Inventario")
            {
                cmbInventarios.Visible = true;
                cmbInventarios.Enabled = true;
                cmbCompras.Visible = false;
                cmbCompras.Enabled = false;
                cmbVentas.Visible = false;
                cmbVentas.Enabled = false;
                groupBox4.Enabled = true;
                cmbtarea.Enabled = true;

                CargarTareas("Entradas", "Salidas", "Traspasos");
            }
        }

        // Centraliza el llenado de cmbtarea para no repetir Items.Clear() + AddRange en cada rama
        void CargarTareas(params string[] tareas)
        {
            cmbtarea.Items.Clear();
            cmbtarea.Items.AddRange(tareas);
            cmbtarea.SelectedIndex = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbTipoDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el tipo de documento para continuar");
            }
            else if (cmbInventarios.Visible == true && cmbInventarios.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase del documento para continuar");
            }
            else if (cmbVentas.Visible == true && cmbVentas.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase del documento para continuar");
            }
            else if (cmbCompras.Visible == true && cmbCompras.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase del documento para continuar");
            }
            else if (txtClave.Text == string.Empty || txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre la calve y nombre del documento para continuar");
            }
            else if (txtAlmacen.Text == string.Empty)
            {
                MessageBox.Show("Registre el almacen para continuar");
            }
            else if (cmbtarea.Text == string.Empty && cmbTipoDocumento.Text == "Compra")
            {
                MessageBox.Show("Se debe agregar un valor en el campo tarea");
            }
            else
            {
                string Clase = string.Empty;
                if (cmbInventarios.Visible == true)
                {
                    Clase = cmbInventarios.Text;
                }
                else if (cmbVentas.Visible == true)
                {
                    Clase = cmbVentas.Text;
                }
                else if (cmbCompras.Visible == true)
                {
                    Clase = cmbCompras.Text;
                }

                string Consecutivo = string.Empty;
                if (tgConsecutivo.Checked == true)
                {
                    Consecutivo = "Si";
                }
                else
                {
                    Consecutivo = "No";
                }

                string Bloqueo = string.Empty;
                if (tgBloquear.Checked == true)
                {
                    Bloqueo = "Si";
                }
                else
                {
                    Bloqueo = "No";
                }

                MessageBox.Show(c.RegistroDocumento(cmbTipoDocumento.Text, Clase, txtClave.Text, txtNombre.Text, txtAlmacen.Text, txtUltimoFolio.Text, Consecutivo, Bloqueo, txtCuenta.Text, txtCuenta2.Text, cmbtarea.Text, tgCentroCosto.Checked));
                Limpiar();
                c.CargarDocumentos(dataGridView1);
                cmbtarea.SelectedIndex = -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
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

        private void Documentos_Load(object sender, EventArgs e)
        {
            c.CargarDocumentos(dataGridView1);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                cmbtarea.SelectedIndex = -1;
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();

                if (cmbInventarios.Visible == true)
                {
                    cmbInventarios.Enabled = true;
                    cmbInventarios.Visible = true;
                    cmbVentas.Visible = false;
                    cmbCompras.Visible = false;
                    c.ConsultaDocumentoSeleccionado(cmbTipoDocumento, cmbInventarios, Clave, Nombre, txtAlmacen, txtUltimoFolio, tgConsecutivo, tgBloquear, txtCuenta, txtCuenta2, cmbtarea, tgCentroCosto);

                }
                else if (cmbVentas.Visible == true)
                {
                    cmbVentas.Enabled = true;
                    cmbInventarios.Visible = false;
                    cmbVentas.Visible = true;
                    cmbCompras.Visible = false;
                    c.ConsultaDocumentoSeleccionado(cmbTipoDocumento, cmbVentas, Clave, Nombre, txtAlmacen, txtUltimoFolio, tgConsecutivo, tgBloquear, txtCuenta, txtCuenta2, cmbtarea, tgCentroCosto);

                }
                else if (cmbCompras.Visible == true)
                {
                    cmbCompras.Enabled = true;
                    cmbInventarios.Visible = false;
                    cmbVentas.Visible = false;
                    cmbCompras.Visible = true;
                    c.ConsultaDocumentoSeleccionado(cmbTipoDocumento, cmbCompras, Clave, Nombre, txtAlmacen, txtUltimoFolio, tgConsecutivo, tgBloquear, txtCuenta, txtCuenta2, cmbtarea, tgCentroCosto);

                }

                if (cmbInventarios.Visible == true)
                {

                    c.ConsultaDocumentoSeleccionado2(cmbInventarios, Clave, Nombre);

                }
                else if (cmbVentas.Visible == true)
                {

                    c.ConsultaDocumentoSeleccionado2(cmbVentas, Clave, Nombre);
                }
                else if (cmbCompras.Visible == true)
                {

                    c.ConsultaDocumentoSeleccionado2(cmbCompras, Clave, Nombre);
                }

                txtClave.Text = Clave;
                txtNombre.Text = Nombre;
                txtClave.Enabled = false;
                txtNombre.Enabled = false;
                PanelUsuario.Visible = false;
                groupBox3.Enabled = true;

            }
            else
            {
                return;
            }
        }

        void Limpiar()
        {
            cmbTipoDocumento.Text = null;
            cmbInventarios.Text = null;
            cmbCompras.Text = null;
            cmbVentas.Text = null;
            txtClave.Clear();
            txtNombre.Clear();
            txtAlmacen.Clear();
            tgConsecutivo.Checked = false;
            tgBloquear.Checked = false;
            tgCentroCosto.Checked = false;
            txtCuenta.Clear();
            txtCuenta2.Clear();
            txtClave.Enabled = true;
            txtNombre.Enabled = true;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteDocumentos reporteDocumentos = new ReporteDocumentos();
            reporteDocumentos.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            cmbTipoDocumento.DroppedDown = true;
            cmbTipoDocumento.Focus();
            groupBox3.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClave.Text, cmbVentas.Text, cmbTipoDocumento.Text));
                        c.CargarDocumentos(dataGridView1);
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

        private void lblConsecutivo_Click(object sender, EventArgs e)
        {

        }

        private void tgConsecutivo_CheckedChanged(object sender, EventArgs e)
        {
            if (tgConsecutivo.Checked == true)
            {
                lblConsecutivo.Text = "Si";
            }
            else
            {
                lblConsecutivo.Text = "No";
            }
        }

        private void tgBloquear_CheckedChanged(object sender, EventArgs e)
        {
            if (tgBloquear.Checked == true)
            {
                lblBloquear.Text = "Si";
            }
            else
            {
                lblBloquear.Text = "No";
            }
        }

        private void tgCentroCosto_CheckedChanged(object sender, EventArgs e)
        {
            if (tgCentroCosto.Checked == true)
            {
                lblCentroCosto.Text = "Si";
            }
            else
            {
                lblCentroCosto.Text = "No";
            }
        }

        private void tgCentroCosto_CheckedChanged_1(object sender, EventArgs e)
        {
            if (tgCentroCosto.Checked == true)
            {
                lblCentroCosto.Text = "Si";
            }
            else
            {
                lblCentroCosto.Text = "No";
            }
        }
    }
}