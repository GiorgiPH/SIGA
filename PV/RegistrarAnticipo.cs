using Condominios;
using ControlAcademico;
using Guna.UI2.WinForms;
using PuntoVentas;
using PV.Clases;
using PV.Clases.Anticipo;
using PV.Clases.Clientes;
using PV.Clases.ConceptoPago;
using PV.Clases.Divisas;

using System;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarAnticipo : Form
    {
        #region Campos y propiedades

        readonly DBAnticipo c = new DBAnticipo();
        readonly DBDivisas d = new DBDivisas();
        readonly DBClientes cl = new DBClientes();
        DBConceptoCobroPago dbConceptoCobroPago = new DBConceptoCobroPago();


        // Estado compartido con otros formularios del módulo (se conserva tal cual).
        public static string matricula = string.Empty;
        public static string nombre = string.Empty;
        public static string FolioC = string.Empty;
        public static string FolioCGeneral = string.Empty;
        public static bool AnticipoRealizado = false;

        // "Propietario" = flujo de anticipos de Clientes; cualquier otro valor = Proveedores.
        readonly string Opcion = string.Empty;
        private const byte IdClaseIngresos = 2;

        #endregion

        #region Constructor

        public RegistrarAnticipo(string opcion)
        {
            InitializeComponent();

            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnNuevoAnticipo, "Nuevo");
            tt.SetToolTip(btnImprimirAnticipo, "Imprimir Anticipo");
            tt.SetToolTip(button11, "Enviar Anticipo");
            tt.SetToolTip(btnConsultarAnticipos, "Consultar Anticipo");

            Opcion = opcion;
            matricula = string.Empty;
            nombre = string.Empty;



            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCaja.Text = "1";

            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
            c.SeleccionarFormaPago(cmbFormaPago);
            d.SeleccionarDivisa(cmbDivisas);
            if (cmbDivisas.Items.Count > 0)
            {
                cmbDivisas.SelectedIndex = 0;
            }

            if (Opcion == "Propietario")
            {
                c.CargarAnticipoCliente(dgvAnticipos, txtFiltro.Text);

                label12.Text = "CLIENTE";
                lbProv.Text = "Cliente";
                dgvAnticipos.Columns[1].HeaderText = "Cliente";
                lblTitulo.Text = "Registro Anticipos Clientes";
            }
            else
            {
                c.CargarAnticipoProveedor(dgvAnticipos, txtFiltro.Text);
                lbProv.Visible = true;
                label12.Text = "PROVEEDOR";
                lbProv.Text = "Proveedor";
                lblTitulo.Text = "Registro Anticipos Proveedores";
            }
        }

     

        #endregion

        #region Eventos de carga / activación del formulario

        private void RegistrarAnticipo_Load(object sender, EventArgs e)
        {
            // Sin acciones adicionales al cargar el formulario.
            DataTable conceptosIngreso = dbConceptoCobroPago.ListarParaCombo(IdClaseIngresos, soloActivos: true);
            // Crear columna para mostrar en el ComboBox
            if (!conceptosIngreso.Columns.Contains("DescripcionCombo"))
            {
                conceptosIngreso.Columns.Add("DescripcionCombo", typeof(string));
            }

            foreach (DataRow row in conceptosIngreso.Rows)
            {
                row["DescripcionCombo"] =
                    $"{row["ClaveConcepto"]} - {row["Descripcion"]}";
            }
            ComboUtil.LlenarComboBox(cmbConceptoCobro, conceptosIngreso, "Descripcion", "IdConcepto");
        }

        private void RegistrarAnticipo_Activated(object sender, EventArgs e)
        {
            // Intencionalmente vacío: se conserva el comportamiento original.
        }

        #endregion

        #region Búsqueda de propietario / proveedor

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                BuscarCliente buscar = new BuscarCliente();
                buscar.ShowDialog();
                if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
                {
                    txtMatricula.Text = BuscarCliente.Cliente;
                    txtAlumno.Text = BuscarCliente.NombreCliente;
                }
            }
            else
            {
                BuscarListaProveedores buscar = new BuscarListaProveedores();
                buscar.ShowDialog();
            }
        }

        #endregion

        #region Generación de folio / nuevo registro

        // Reemplaza a los antiguos GenerarNoCategoria() / GenerarNoCategoria2(), que
        // tenían exactamente la misma lógica y solo diferían en qué método de DBAnticipo
        // consultaban. Se unificaron para eliminar la duplicación.
        private void GenerarNuevoFolio(Func<int> obtenerUltimoFolio)
        {
            DBAnticipo.Folio = 0;
            obtenerUltimoFolio();
            DBAnticipo.Folio = DBAnticipo.Folio == 0 ? 1 : DBAnticipo.Folio + 1;
            txtFolio.Text = Convert.ToString(DBAnticipo.Folio);
        }

        private void btnNuevoAnticipo_Click(object sender, EventArgs e)
        {
            // Nota: en el original, las ramas "txtFolio vacío" / "no vacío" ejecutaban
            // exactamente el mismo código, así que se dejó una sola ruta (mismo resultado).
            Limpiar();

            if (Opcion == "Propietario")
            {
                GenerarNuevoFolio(c.ClaveProductoSiguiente);
            }
            else
            {
                GenerarNuevoFolio(c.ClaveProductoSiguiente2);
            }

            panel1.Enabled = true;
            btnImprimirAnticipo.Enabled = true;
            button11.Enabled = true;
            pnRegistrar.Enabled = false;
            dtpFecha.Enabled = true;
            cmbConceptoCobro.Enabled = true;
        }

        #endregion

        #region Confirmar / registrar anticipo

        private void btnConfirmarAnticipo_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
                return;
            }
            if (txtMatricula.Text == string.Empty)
            {
                MessageBox.Show("Registre al propietario para continuar.");
                return;
            }
            if (cmbCuentaBancaria.Text == string.Empty)
            {
                MessageBox.Show("Registre la cuenta bancaria para continuar.");
                return;
            }
            if (cmbFormaPago.Text == string.Empty)
            {
                MessageBox.Show("Registre la forma de pago para continuar.");
                return;
            }
            // CORRECCIÓN: Convert.ToDecimal lanzaba una excepción no controlada si
            // txtimporte.Text estaba vacío o no era numérico. Se usa TryParse conservando
            // el mismo mensaje y la misma condición (importe <= 0).
            if (!decimal.TryParse(txtimporte.Text, out decimal importeIngresado) || importeIngresado <= 0)
            {
                MessageBox.Show("Registre el importe para continuar.");
                return;
            }
            if (!decimal.TryParse(txtImporteMXN.Text, out decimal importeMXN))
            {
                MessageBox.Show("El importe en MXN no es válido.");
                return;
            }
            if (cmbConceptoCobro.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un concepto de cobro para continuar.");
                return;
            }

            if (Opcion == "Propietario")
            {
                MessageBox.Show(c.RegistroAnticipo(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text,
                    cmbFormaPago.Text, cmbConceptoCobro.SelectedValue.ToString(), txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text,
                    importeMXN, cmbDivisas.Text, txtTipoCambio.Text, FolioC, FolioCGeneral));
                c.CargarAnticipo(dgvAnticipos, txtFiltro.Text);
            }
            else
            {
                MessageBox.Show(c.RegistroAnticipoProveedor(txtFolio.Text, txtMatricula.Text, txtCaja.Text, dtpFecha.Text,
                    cmbFormaPago.Text, cmbConceptoCobro.SelectedValue.ToString(), txtReferencia.Text, txtCuenta.Text, txtNumOperacion.Text,
                    importeMXN, cmbDivisas.Text, txtTipoCambio.Text));
                c.CargarAnticipoProveedor(dgvAnticipos, txtFiltro.Text);
            }

            AnticipoRealizado = true;

            if (MessageBox.Show("¿Imprimir Recibo?", "Registrar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MostrarRecibo(imprimir: false);
            }

            Limpiar();
        }

        #endregion

        #region Limpieza de formulario

        void Limpiar()
        {
            txtFolio.Clear();
            txtMatricula.Text = string.Empty;
            txtAlumno.Text = string.Empty;
            txtConceptoClave.Clear();
            cmbFormaPago.Text = null;
            txtConcepto.Clear();
            txtReferencia.Text = string.Empty;
            txtCuenta.Clear();
            cmbCuentaBancaria.Text = null;
            txtNumOperacion.Text = string.Empty;
            txtimporte.Text = "0.00";
            txtAlumno.Clear();
            panel1.Enabled = false;
            btnImprimirAnticipo.Enabled = false;
            button11.Enabled = false;
            btnBuscar.Enabled = true;
            cmbDivisas.Text = null;
            txtTipoCambio.Clear();
            txtImporteMXN.Clear();
            txtSaldo.Text = "0.00";
            txtimporte.Enabled = true;
            cmbConceptoCobro.SelectedIndex = -1;

            if (Opcion == "Propietario")
            {
                c.ConsultaConceptoAnticipo(txtConcepto, txtConceptoClave);
                c.CargarAnticipoCliente(dgvAnticipos, txtFiltro.Text);
            }
            else
            {
                c.CargarAnticipoProveedor(dgvAnticipos, txtFiltro.Text);
                lbProv.Visible = true;
                txtConcepto.Text = "APR - Anticipo Proveedores";
                txtConceptoClave.Text = "APR";
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        #endregion

        #region Grid de anticipos (selección, filtro)

        private void dgvAnticipos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string clave = dgvAnticipos.Rows[e.RowIndex].Cells["Folio"].Value.ToString();

            if (Opcion == "Propietario")
            {
                string activo = c.ConsultaProductoSeleccionado(clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago,
                    cmbConceptoCobro, txtReferencia, txtCuenta, txtNumOperacion, txtImporteMXN, cmbDivisas,
                    txtTipoCambio, txtSaldo, txtimporte);
                lbEstatus.Text = activo == "1" ? "Estatus:  CANCELADO" : " ";
            }
            else
            {
                c.ConsultaProductoSeleccionadoProveedor(clave, txtMatricula, txtCaja, dtpFecha, cmbFormaPago,
                    cmbConceptoCobro, txtReferencia, txtCuenta, txtNumOperacion, txtImporteMXN, cmbDivisas,
                    txtTipoCambio, txtSaldo, txtimporte);
            }

            pnRegistrar.Enabled = false;
            dtpFecha.Enabled = false;
            panel1.Enabled = true;
            txtFolio.Text = clave;
            PanelUsuario.Visible = false;
            btnImprimirAnticipo.Enabled = true;
            button11.Enabled = true;
            txtimporte.Enabled = false;
            btnBuscar.Enabled = false;
            cmbConceptoCobro.Enabled = false;

            if (txtCuenta.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta2(txtCuenta.Text);
                cmbCuentaBancaria.Text = valores[0];
            }

            if (txtConceptoClave.Text != string.Empty && Opcion == "Propietario")
            {
                string[] valores = c.InformacionConcepto(txtConceptoClave.Text);
                txtConcepto.Text = valores[0];
            }

            if (Opcion == "Propietario")
            {
                if (txtMatricula.Text != string.Empty)
                {
                    string[] valores = cl.InformacionCliente(txtMatricula.Text);
                    txtAlumno.Text = valores[1];
                }
            }
            else
            {
                if (txtMatricula.Text != string.Empty)
                {
                    string[] valores = c.InformacionProveedor(txtMatricula.Text);
                    txtAlumno.Text = valores[0];
                }
            }

            if (cmbDivisas.Text != string.Empty)
            {
                string[] valores = d.InformacionDivisa(cmbDivisas.Text);
                txtTipoCambio.Text = valores[0];
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (Opcion == "Propietario")
            {
                c.CargarAnticipo(dgvAnticipos, txtFiltro.Text);
            }
            else
            {
                c.CargarAnticipoProveedor(dgvAnticipos, txtFiltro.Text);
            }
        }

        private void btnConsultarAnticipos_Click(object sender, EventArgs e)
        {
            PanelUsuario.Visible = !PanelUsuario.Visible;
        }

        #endregion

        #region Cancelación de anticipo

        // Regla de negocio (se conserva intacta, solo se hizo robusta la comparación):
        // un anticipo solo puede cancelarse si NO ha sido aplicado, es decir, si su
        // saldo actual es igual al importe original. Si el saldo ya cambió (aplicación
        // parcial o total), no se permite cancelar.
        private void btnCancelarAnticipo_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el anticipo para continuar");
                return;
            }

            // CORRECCIÓN: la comparación original era de texto (txtImporteMXN.Text !=
            // txtSaldo.Text), sensible a formato (p. ej. "1,000.00" vs "1000.00").
            // Se compara por valor numérico para que la regla sea confiable.
            if (!decimal.TryParse(txtImporteMXN.Text, out decimal importeOriginal) ||
                !decimal.TryParse(txtSaldo.Text, out decimal saldoActual))
            {
                MessageBox.Show("No fue posible validar los importes del anticipo.");
                return;
            }

            if (saldoActual != importeOriginal)
            {
                MessageBox.Show("No es posible cancelar anticipos aplicados");
                return;
            }

            if (Opcion == "Propietario")
            {
                c.EliminarAnticipo(txtFolio.Text);
                MessageBox.Show("Anticipo cancelado");
                MostrarRecibo(imprimir: false);
            }
            else
            {
                c.EliminarAnticipoProveedor(txtFolio.Text);
                MessageBox.Show("Anticipo cancelado");
                MostrarRecibo(imprimir: false);
            }

            Limpiar();
        }

        #endregion

        #region Impresión / envío de recibo

        // Reemplaza la lógica repetida en btnConfirmarAnticipo_Click, button10_Click,
        // btnCancelarAnticipo_Click y button11_Click (mismo comportamiento, un solo lugar).
        private void MostrarRecibo(bool imprimir)
        {
            if (txtFolio.Text == string.Empty || txtimporte.Text == "0.00")
            {
                return;
            }

            string modo = imprimir ? "1" : "0";

            if (Opcion == "Propietario")
            {
                ReciboAnticipo reciboAnticipo = new ReciboAnticipo(txtFolio.Text, modo, txtMatricula.Text);
                reciboAnticipo.ShowDialog();
            }
            else
            {
                ReciboAnticipoProveedor reciboAnticipo = new ReciboAnticipoProveedor(txtFolio.Text, modo, txtMatricula.Text);
                reciboAnticipo.ShowDialog();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MostrarRecibo(imprimir: false);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MostrarRecibo(imprimir: true);
        }

        #endregion

        #region Eventos de controles (combos, montos)

        private void cmbCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta(cmbCuentaBancaria.Text);
                txtCuenta.Text = valores[0];
            }
        }

        private void cmbDivisas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivisas.Text != string.Empty)
            {
                string[] valores = d.InformacionDivisa(cmbDivisas.Text);
                txtTipoCambio.Text = valores[0];
                txtimporte.Text = "0.00";
                txtImporteMXN.Text = "0.00";
            }
        }

        private void txtimporte_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtimporte);

            if (txtTipoCambio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione una divisa con tipo de cambio registrado");
                txtimporte.Text = "0.00";
                txtImporteMXN.Text = "0.00";
                return;
            }

            if (decimal.TryParse(txtimporte.Text, out decimal importe) && decimal.TryParse(txtTipoCambio.Text, out decimal tipoCambio))
            {
                decimal importeMxn = importe * decimal.Round(tipoCambio, 2);
                txtImporteMXN.Text = importeMxn.ToString("0.00");
            }
        }

        private void txtimporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtImporteMXN_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImporteMXN);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSaldo);
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            // Sin acciones adicionales.
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            pnRegistrar.Enabled = !string.IsNullOrEmpty(txtMatricula.Text);
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            btnCancelarAnticipo.Enabled = !string.IsNullOrEmpty(txtFolio.Text);
        }

        #endregion

        #region Cierre de formulario

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            matricula = string.Empty;
            nombre = string.Empty;
            GenerarRecibo.Matricula = string.Empty;
            registroIngresos.matricula = string.Empty;
            registroIngresos.nombre = string.Empty;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            // Sin acciones adicionales.
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}