using System;
using System.Data;
using System.Windows.Forms;
using PV.Clases;
using PV.Clases.CuentasBancarias;
using PV.Clases.MovimientosBanco;

namespace PV
{
    public partial class RegistroMovimientoBancos : Form
    {
        DBCUentaBancaria dbCuentaBancaria = new DBCUentaBancaria();
        DBMovimientosBanco dbMovimientosBanco = new DBMovimientosBanco();

        public RegistroMovimientoBancos()
        {
            InitializeComponent();

            ToolTip T = new ToolTip();
            T.SetToolTip(btnNuevoMovimiento, "Nuevo Movimiento");
            T.SetToolTip(btnConsultarMovimeintosRecientes, "Consultar Movimientos Recientes");
            T.SetToolTip(btnReporteMovimientos, "Imprimir Movimientos Recientes");
            T.SetToolTip(guna2CircleButton1, "Menú");
            guna2GradientPanel1.FillColor = colores.colorPrincipal;
            guna2GradientPanel1.FillColor2 = colores.colorSecundario;

            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            btnNuevoMovimiento.Click += btnNuevoMovimiento_Click;
            btnConfirmarMovimiento.Click += btnConfirmarMovimiento_Click;
            btnLimpiarMovimiento.Click += btnLimpiarMovimiento_Click;
            btnConsultarMovimeintosRecientes.Click += btnConsultarMovimeintosRecientes_Click;
        }

        private void Almacenes_Load(object sender, EventArgs e)
        {
            DataTable dtCuentas = dbCuentaBancaria.ObtenerCuentasBancarias();
            ComboUtil.LlenarComboBox(cmbCuentaBancaria, dtCuentas, "Nombre", "Clave");

            // El grid inicia oculto (PanelUsuario.Visible = false en el diseñador);
            // se precarga igual para que ya esté listo al primer clic de "Consultar".
            CargarMovimientosRecientes();
        }

        //____________________________________________________________________
        private void btnNuevoMovimiento_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            groupBox1.Enabled = true;
            dtpFecha.Value = DateTime.Now;
            cmbTipo.SelectedIndex = 0; // dispara cmbTipo_SelectedIndexChanged y carga conceptos
        }

        //____________________________________________________________________
        // Al cambiar Tipo (Egreso/Ingreso) se recarga Concepto solo con lo
        // aplicable: Egreso -> CARGO/TRASPASO/OTRO, Ingreso -> ABONO/TRASPASO/OTRO
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedIndex < 0) return;

            string tipo = cmbTipo.SelectedItem.ToString() == "Ingreso"
                ? DBMovimientosBanco.TipoIngreso
                : DBMovimientosBanco.TipoEgreso;

            DataTable dtConceptos = dbMovimientosBanco.ObtenerConceptosPorTipo(tipo);
            ComboUtil.LlenarComboBox(cmbConcepto, dtConceptos, "Descripcion", "IdConcepto");
        }

        //____________________________________________________________________
        private void btnConfirmarMovimiento_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string tipo = cmbTipo.SelectedItem.ToString() == "Ingreso"
                ? DBMovimientosBanco.TipoIngreso
                : DBMovimientosBanco.TipoEgreso;

            int claveCuenta = Convert.ToInt32(cmbCuentaBancaria.SelectedValue);
            int idConcepto = Convert.ToInt32(cmbConcepto.SelectedValue);
            decimal importe = Convert.ToDecimal(txtImporte.Text);

            bool ok = dbMovimientosBanco.InsertarMovimiento(
                dtpFecha.Value.Date,
                claveCuenta,
                tipo,
                idConcepto,
                importe,
                txtNotas.Text.Trim(),
                Environment.UserName // TODO: reemplazar por el usuario de sesión del sistema si se maneja login propio
            );

            if (ok)
            {
                MessageBox.Show("Movimiento registrado correctamente.", "SIGA",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarMovimientosRecientes();
                LimpiarCampos();
                groupBox1.Enabled = false;
            }
        }

        //____________________________________________________________________
        // Solo limpia/resetea los campos del form (según lo solicitado)
        private void btnLimpiarMovimiento_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            groupBox1.Enabled = false;
        }

        //____________________________________________________________________
        // Toggle: muestra/oculta el grid de movimientos recientes
        private void btnConsultarMovimeintosRecientes_Click(object sender, EventArgs e)
        {
            PanelUsuario.Visible = !PanelUsuario.Visible;
            if (PanelUsuario.Visible)
            {
                CargarMovimientosRecientes();
            }
        }

        //____________________________________________________________________
        private void CargarMovimientosRecientes()
        {
            DataTable dt = dbMovimientosBanco.ObtenerMovimientosRecientes(10);
            dgvMovimientosRecientes.DataSource = dt;
        }

        //____________________________________________________________________
        private void LimpiarCampos()
        {
            cmbTipo.SelectedIndex = -1;
            cmbConcepto.SelectedIndex = -1;

            //cmbConcepto.Items.Clear();
            txtImporte.Text = "0.00";
            txtNotas.Text = string.Empty;
            dtpFecha.Value = DateTime.Now;
        }

        //____________________________________________________________________
        private bool ValidarCampos()
        {
            if (cmbCuentaBancaria.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona una cuenta bancaria.");
                return false;
            }
            if (cmbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el tipo de movimiento.");
                return false;
            }
            if (cmbConcepto.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un concepto.");
                return false;
            }
            if (!decimal.TryParse(txtImporte.Text, out decimal importe) || importe <= 0)
            {
                MessageBox.Show("Captura un importe válido.");
                return false;
            }
            return true;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImporte);
        }
    }
}