// ============================================================================
// RegistroGastos2.cs
// ----------------------------------------------------------------------------
// Refactor de estructura y limpieza de código sobre el formulario original.
// La lógica de negocio (flujo encabezado -> partidas -> archivos, y el
// bloqueo por cmbEstatus == "Abierto") se mantiene intacta.
//
// Todos los métodos que son manejadores de eventos conservan EXACTAMENTE el
// mismo nombre y firma que tenían, porque el archivo .Designer.cs los
// referencia directamente (this.boton.Click += new EventHandler(este_nombre)).
// No se renombró ni se eliminó ningún handler.
//
// Los cambios que alteran el COMPORTAMIENTO (no solo la forma del código)
// están marcados con comentarios "CORREGIDO:" y explicados en el resumen que
// te compartí en el chat. Revísalos antes de pasar a producción.
// ============================================================================

using Condominios;
using Condominios.Clases.CentroCostos;
using PuntoVentas.Clases.Login;
using PV.Clases;

using PV.Clases.OrdenCompra;
using PV.Clases.Proveedores;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistroGastos2 : Form
    {
        #region Campos y constructor

        public static string Matricula = string.Empty;
        DBOrdenCompra c = new DBOrdenCompra();
        public static string Carpeta = string.Empty;

        DBCentroCostos cc = new DBCentroCostos();
        DBProveedores p = new DBProveedores();

        string recibo = string.Empty;
        string reciboCol = string.Empty;
        public static string Carpeta1 = string.Empty;
        private string actualizarcombo = string.Empty;
        private string actualizarproveedor = string.Empty;
        private string consultaRegistros = string.Empty;

        // NOTA: estos dos campos no se leen en ningún lado; en button3_Click
        // se declara una variable LOCAL "opcion" que oculta a este campo, así
        // que el campo de clase nunca se usa. Se dejan declarados (no se
        // eliminan) por si algún otro partial class los llegara a referenciar,
        // pero probablemente se puedan quitar con seguridad.
        int opcion = 0;
        int Partidas = 0;

        // Indica si el documento seleccionado en cmbDocumento permite capturar
        // Centro de Costos (se llena desde valores[2] en cmbDocumento_SelectedIndexChanged
        // y controla el Enabled de cmbCentroCostos).
        private bool mostrrcentorcosto = false;

        private string rutaCompletaArchivo = string.Empty;
        private string nombreArchivo = string.Empty;
        private string extensionArchivo = string.Empty;

        public RegistroGastos2()
        {
            InitializeComponent();
            for (int i = 1; i <= 52; i++)
            {
                cmbSemana.Items.Add($"Semana {i}");
            }
        }

        #endregion

        #region Utilidades internas (fechas, parseo, moneda, totales de partidas)

        /// <summary>
        /// Calcula el número de semana del año en curso (1 a 52), tomando el día
        /// del año y dividiendo entre 7 (misma cantidad de semanas que se cargan
        /// en cmbSemana en el constructor). Se usa para preseleccionar la semana
        /// actual al iniciar un nuevo registro.
        /// </summary>
        private int ObtenerNumeroSemanaActual()
        {
            DateTime hoy = DateTime.Today;
            int semana = ((hoy.DayOfYear - 1) / 7) + 1;

            if (semana > 52) semana = 52;
            if (semana < 1) semana = 1;

            return semana;
        }

        /// <summary>
        /// Interpreta distintos formatos comunes que puede regresar la base de datos
        /// (1/0, true/false, si/no) como un valor booleano. Se usa para convertir
        /// valores[2] (InformacionDocumento) en el booleano mostrrcentorcosto.
        /// </summary>
        private bool ParsearBooleano(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            switch (valor.Trim().ToUpperInvariant())
            {
                case "1":
                case "TRUE":
                case "SI":
                case "SÍ":
                case "S":
                case "YES":
                    return true;
                default:
                    return false;
            }
        }

        private void CalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;

                int Dias = Convert.ToInt32(diasv);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }
        }

        private void Calcular()
        {
            // Validaciones básicas para evitar errores de conversión
            decimal precio = 0, cantidad = 0, descuentoPorcentaje = 0, impuestoPorcentaje = 0;

            decimal.TryParse(txtPrecio.Text, out precio);
            decimal.TryParse(txtCantidad.Text, out cantidad);
            decimal.TryParse(txtDescuento1.Text, out descuentoPorcentaje);
            decimal.TryParse(txtImpuesto1.Text, out impuestoPorcentaje);
            decimal sub = (precio * cantidad);

            decimal descuento = (descuentoPorcentaje / 100) * sub;
            txtDescuentoIm.Text = descuento.ToString("N2");
            sub = (precio * cantidad) - descuento;

            // Calcular impuesto
            decimal impuesto = (impuestoPorcentaje / 100) * sub;

            // Asignar los valores calculados a los controles de texto
            txtImpuestoIm.Text = impuesto.ToString("N2");
            txtTotal1.Text = (sub + impuesto).ToString("N2");
            txtSubtotal1.Text = sub.ToString("N2");
        }

        private void Moneda(ref Guna.UI2.WinForms.Guna2TextBox txt)
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

                // NOTA: aquí existía un bloque que intentaba quitar un cero a la
                // izquierda con "n.Substring(...)" sin asignar el resultado a
                // ninguna variable (los string son inmutables, así que ese
                // bloque no hacía nada). Se quitó porque además era
                // inofensivo: Convert.ToDouble ya ignora los ceros a la
                // izquierda, así que el resultado final es el mismo.

                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recalcula subtotal, descuento, impuesto y total de TODAS las
        /// partidas actualmente cargadas en guna2DataGridView1 y actualiza las
        /// etiquetas correspondientes (lblSubtotalPartidas, lblDescuentosPartidas,
        /// lblImpuestosPartidas, lblTotal).
        ///
        /// CORREGIDO: este método ya existía pero no se llamaba desde ningún
        /// lado, así que esas etiquetas nunca se actualizaban. Ahora se invoca
        /// cada vez que se recarga la grilla de partidas (al confirmar, al
        /// usar "Siguiente Partida", al eliminar una partida y al abrir un
        /// registro existente). Ver los comentarios "NUEVO:" en cada uno de
        /// esos métodos.
        /// </summary>
        private void SumarColumnasPartida()
        {
            decimal totalSubtotal = 0;
            decimal totalDescuento = 0;
            decimal totalImpuesto = 0;

            foreach (DataGridViewRow fila in guna2DataGridView1.Rows)
            {
                if (!fila.IsNewRow)
                {
                    if (fila.Cells["Subtotal"].Value != null && decimal.TryParse(fila.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                        totalSubtotal += subtotal;

                    if (fila.Cells["Descuento"].Value != null && decimal.TryParse(fila.Cells["Descuento"].Value.ToString(), out decimal descuento))
                        totalDescuento += descuento;

                    if (fila.Cells["Impuesto"].Value != null && decimal.TryParse(fila.Cells["Impuesto"].Value.ToString(), out decimal impuesto))
                        totalImpuesto += impuesto;
                }
            }

            txtSubtotalGrid.Text = Utilerias.FormatearMiles(totalSubtotal.ToString("N2"));
            txtDescuentoGrid.Text = Utilerias.FormatearMiles(totalDescuento.ToString("N2"));
            txtImpuestoGrid.Text = Utilerias.FormatearMiles(totalImpuesto.ToString("N2"));

            decimal total = totalSubtotal - totalDescuento + totalImpuesto;
            txtTotalGrid.Text = Utilerias.FormatearMiles(total.ToString());
        }

        #endregion

        #region Carga de catálogos (combos)

        private void LlenarComboCentro()
        {
            try
            {
                DataTable menus = cc.ConsultarTodos();

                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostos.DataSource = menus;
                cmbCentroCostos.DisplayMember = "Nombre";
                cmbCentroCostos.ValueMember = "Clave";
                cmbCentroCostos.SelectedIndex = -1;

                cmbCentroCostosAlterno.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostosAlterno.DataSource = menus;
                cmbCentroCostosAlterno.DisplayMember = "Nombre";
                cmbCentroCostosAlterno.ValueMember = "Clave";
                cmbCentroCostosAlterno.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarComboProveedores()
        {
            try
            {
                DataTable menus = p.ConsultarProveedores();

                cmbProveedroAlterno.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProveedroAlterno.DataSource = menus;
                cmbProveedroAlterno.DisplayMember = "RazonSocial";
                cmbProveedroAlterno.ValueMember = "IdProveedor";
                cmbProveedroAlterno.SelectedIndex = -1;

                // NOTA: aquí existía una segunda llamada a p.ConsultarProveedores()
                // cuyo resultado no se usaba (no se asignaba a nada). Se quitó
                // porque solo generaba una consulta extra a la base de datos sin
                // ningún efecto visible.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Filtros de búsqueda (grid principal de gastos)

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroGasto(dgvGastos, txtFiltro.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocuemtnoGasto(dgvGastos, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroPGasto(dgvGastos, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroGasto(dgvGastos, txtFiltro.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        private void txtFiltroDocumento_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocuemtnoGasto(dgvGastos, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        private void txtFiltroNombre_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                c.CargarRecibosFiltroPGasto(dgvGastos, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarGasto(dgvGastos);
            }
        }

        #endregion

        #region Ciclo de vida del formulario / preparación de un registro nuevo

        /// <summary>
        /// Deja el encabezado listo para capturar un registro de gasto nuevo:
        /// recarga los combos dependientes de documento/proveedor/orden,
        /// asigna los valores por defecto (moneda, tipo de cambio, elaborado,
        /// días de vencimiento) y preselecciona semana/año actuales.
        ///
        /// Antes este bloque estaba duplicado casi línea por línea entre
        /// RegistroGastos2_Load y la opción "NUEVO" del menú lateral
        /// (toolStrip2_ItemClicked_1). Se unificó aquí; ambos lugares hacen
        /// exactamente lo mismo que hacían antes.
        /// </summary>
        private void PrepararNuevoRegistro()
        {
            c.SeleccionarRegistroGastos(cmbDocumento);
            c.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
            c.ruta();
            c.CargarGasto(dgvGastos);

            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
            txtDiasVence.Text = "0";
            cmbProoveedorAlternoSiNo.Text = "Si";

            CalcularFechaVencimiento();

            // Preselecciona el año y la semana actual
            dtpAnio.Value = DateTime.Today;
            cmbSemana.SelectedIndex = ObtenerNumeroSemanaActual() - 1;
        }

        private void RegistroGastos2_Load(object sender, EventArgs e)
        {
            LlenarComboProveedores();
            LlenarComboCentro();

            PrepararNuevoRegistro();
        }

        private void RegistroGastos2_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                txtMatricular.Text = Matricula;

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
                else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                {
                    button1.BackColor = Color.Red;
                }
            }
        }

        #endregion

        #region Encabezado del gasto

        private void btnCrearEncabezado_Click(object sender, EventArgs e)
        {
            // Validar proveedor
            if (string.IsNullOrWhiteSpace(txtMatricular.Text))
            {
                MessageBox.Show(
                    "Registre al proveedor antes de continuar",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Validar estatus de la recepción
            if (!string.Equals(cmbEstatus.Text, "Abierto", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "No es posible agregar partidas a una recepción de productos bloqueada o cancelada.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string centroCosto = null;

            if (mostrrcentorcosto)
            {
                if (cmbCentroCostos.SelectedValue == null ||
                    string.IsNullOrWhiteSpace(cmbCentroCostos.SelectedValue.ToString()))
                {
                    MessageBox.Show(
                        "Seleccione un centro de costos antes de continuar.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                centroCosto = cmbCentroCostos.SelectedValue.ToString();
            }

            // Validar semana
            if (cmbSemana.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione la semana.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string folioOrden = txtOrdenCompra.Text.Trim();

            // Si todavía no existe el folio, crear el registro
            if (string.IsNullOrWhiteSpace(txtFolio.Text))
            {
                c.InsertarRegistroGasto(
                    txtFolio,
                    txtClave.Text,
                    cmbEstatus.Text,
                    txtFecha.Text,
                    txtMatricular.Text,
                    txtDivisa.Text,
                    txtTipoCambio.Text,
                    txtNotas.Text,
                    txtElaborado.Text,
                    folioOrden,
                    txtConsecutivo.Text,
                    txtReferencia.Text,
                    txtDiasVence.Text,
                    txtFechaVence.Text,
                    centroCosto,
                    cmbSemana.SelectedIndex + 1,
                    dtpAnio.Text,
                    cmbProoveedorAlternoSiNo.Text
                );
            }

            // Determinar si ya existe un archivo/comprobante
            bool tieneArchivo = !string.IsNullOrWhiteSpace(txtArchivo.Text);

            // Ir a la pestaña de partidas
            guna2TabControl1.SelectedIndex = 1;

            // Pasar datos al formulario de partidas
            TxtFolio1.Text = txtFolio.Text.Trim();
            txtOrden.Text = folioOrden;

            // Configurar productos dependiendo de si existe orden de compra
            if (!string.IsNullOrWhiteSpace(txtOrden.Text))
            {
                c.SeleccionarProductoGasto(cmbConcepto, txtOrden.Text);
                c.ConsultaGasto(TxtFolio1.Text, txtPartida);

                txtPrecio.Enabled = false;
            }
            else
            {
                c.SeleccionarProductoGasto(cmbConcepto);
                c.ConsultaGasto(TxtFolio1.Text, txtPartida);

                txtPrecio.Enabled = true;
            }

            // Valores iniciales de la partida
            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";

            // Si ya tiene comprobante, no permitir adjuntarlo nuevamente
            if (tieneArchivo)
            {
                btnAdjuntarComporbante.Enabled = false;
            }

            // Cerrar listas desplegables
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            cmbDocumento.DroppedDown = false;

            // Restaurar colores
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            button7.BackColor = Color.Gainsboro;
        }

        private void cmbOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbOrdenCompra.Text != string.Empty && cmbFiltroDocumentoC.Text != string.Empty && cmbProveedor.Text != string.Empty)
                {
                    string[] valores = c.InformacionOrdenCompra(cmbOrdenCompra.Text, cmbFiltroDocumentoC.Text);
                    txtOrdenCompra.Text = valores[0];
                    txtMatricular.Text = valores[3];
                    Matricula = txtMatricular.Text;
                    txtDivisa.Text = valores[4];
                    txtTipoCambio.Text = valores[5];

                    string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                    txtNombreAlumnno.Text = valores2[0];

                    txtElaborado.Text = DBLogin.usuario;
                }
            }
        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];

                    // valores[2] indica si el documento permite capturar Centro de Costos
                    if (valores.Length > 2)
                    {
                        mostrrcentorcosto = ParsearBooleano(valores[2]);
                        cmbCentroCostos.Enabled = mostrrcentorcosto;
                    }

                    if (txtFolio.Text == string.Empty)
                    {
                        c.ConsecutivoGasto(txtConsecutivo, txtClave.Text);
                    }
                }
            }
        }

        private void cmbFiltroDocumentoC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbFiltroDocumentoC.Text);
                    txtFiltroOrdenC.Text = valores[1];

                    c.SeleccionarProvedor(cmbProveedor, txtFiltroOrdenC.Text);
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }
            }
        }

        private void btLimpiarOrden_Click(object sender, EventArgs e)
        {
            if (cmbFiltroDocumentoC.Text != string.Empty)
            {
                txtOrdenCompra.Clear();
                txtFiltroOrdenC.Clear();
                txtMatricular.Clear();
                txtNombreAlumnno.Clear();
                Matricula = string.Empty;
                cmbFiltroDocumentoC.Text = null;
                cmbOrdenCompra.Text = null;
                cmbProveedor.Text = null;

                cmbFiltroDocumentoC.DroppedDown = false;
                cmbProveedor.DroppedDown = false;
                cmbOrdenCompra.DroppedDown = false;
                cmbDocumento.DroppedDown = false;
                button3.BackColor = Color.Gainsboro;
                txtReferencia.BackColor = Color.White;
                txtNotas.BackColor = Color.White;
                button2.BackColor = Color.Gainsboro;
                txtDiasVence.BackColor = Color.White;
                button7.BackColor = Color.Gainsboro;
            }
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty && txtOrdenCompra.Text == string.Empty)
            {
                string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores2[0];
            }
        }

        private void dgvGastos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            Limpiar();
            string Folio = dgvGastos.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
            txtFolio.Text = "X";
            c.ConsultaGastos(Folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo, txtReferencia, txtSaldo, txtDiasVence, txtFechaVence, txtArchivo, cmbCentroCostos, cmbSemana, dtpAnio, cmbProoveedorAlternoSiNo, txtMatricular);
            rutaCompletaArchivo = txtArchivo.Text;
            cmbOrdenCompra.Enabled = false;
            txtNotas.Enabled = false;
            button3.Enabled = false;
            c.ConsultaAbonoGasto(txtFolio.Text, txtAbono);

            string[] valores = c.InformacionDocumento2(txtClave.Text);
            txtDocumento.Text = valores[0];

            cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

            if (txtReciboCol.Text != "0")
            {
                c.SeleccionarOrdenEntrega2(cmbOrdenCompra, txtReciboCol.Text);
                cmbOrdenCompra.SelectedIndex = 0;

                c.SeleccionarProvedor2(cmbProveedor, txtReciboCol.Text);
                cmbProveedor.SelectedIndex = 0;

                string[] valores2 = c.InformacionDocumento3(cmbOrdenCompra.Text);
                txtFiltroOrdenC.Text = valores2[0];
                txtDocumentoCol.Text = valores2[1];

                cmbFiltroDocumentoC.Text = txtFiltroOrdenC.Text + " - " + txtDocumentoCol.Text;
            }

            guna2GradientPanel2.Visible = false;
            guna2GradientPanel2.SendToBack();
            c.CargarRecibosPartidasGasto(guna2DataGridView1, txtFolio.Text);

            // NUEVO: al abrir un registro existente, recalcular los totales
            // que se muestran en las etiquetas (antes quedaban en blanco
            // porque SumarColumnasPartida() nunca se llamaba).
            SumarColumnasPartida();
        }

        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la recepcion de productos");
                return;
            }
            else if (txtTotal.Text != txtSaldo.Text)
            {
                MessageBox.Show("No es posible cancelar una recepcion de productos con un pago total o parcial");
                return;
            }
            else if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una recepcion de productos que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El saldo de esta recepcion de productos sera cancelado, ¿Desea continuar?", "Recepcion de Gastos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Cancelado";
                MessageBox.Show(c.CancelarRegistroGasto(txtFolio.Text));

                Limpiar();
            }
        }

        private void btnTerminarGasto_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text == string.Empty)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }

            if (cmbOrdenCompra.Text == string.Empty && cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar registro de gastos Bloqueada o Cancelada");
                return;
            }

            if (MessageBox.Show("Al confirmar el registro de gasto no podra realizar modificaciones, ¿Desea continuar?", "Registro de Gasto ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                // CORREGIDO: antes, si el usuario respondía "No" en este
                // mensaje, el código igual seguía de largo y limpiaba todo el
                // formulario (encabezado, detalle, folio, grillas) como si
                // hubiera confirmado. Ahora, si responde "No", simplemente se
                // cancela la operación y el registro se queda tal como estaba.
                return;
            }

            Matricula = string.Empty;
            cmbEstatus.Text = "Bloqueado";
            c.ActualizarGasto(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
            c.ActualizarSaldoProveedor2(txtMatricular.Text, Convert.ToDecimal(txtTotal.Text));
            Limpiar();
            c.CargarGasto(dgvGastos);

            Limpiarcabezado();
            LimpiarDetalle();

            guna2TabControl1.SelectedIndex = 0;
            btnTerminarGasto.Visible = true;
            guna2TabControl1.Enabled = false;
            guna2DataGridView1.Rows.Clear();
            txtFolio.Text = String.Empty;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            dgvComprobantesPartidas.Rows.Clear();
        }

        #endregion

        #region Partidas del gasto

        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una recepcion de productos bloqueada o cancelada");
                return;
            }
            PanelPartidasGasto.Visible = true;
            LimpiarDetalle();
            c.ConsultaGasto(TxtFolio1.Text, txtPartida);
            btnAdjuntarComporbante.Enabled = true;
        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarGasto(TxtFolio1.Text, Partida.ToString());
                        c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                    }
                }
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (cmbProoveedorAlternoSiNo.Text == "Si" && string.IsNullOrEmpty(cmbProveedroAlterno.Text))
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor alterno");
                    return;
                }
                c.InsertarPartidaGasto(
                    TxtFolio1.Text,
                    txtPartida.Text,
                    txtClave1.Text,
                    txtConcepto2.Text,
                    txtCantidad.Text.Replace(",", ""),
                    txtUnidad.Text,
                    txtDivisa1.Text,
                    txtTipoCambio1.Text.Replace(",", ""),
                    Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")),
                    Convert.ToDecimal(txtDescuento1.Text.Replace(",", "")),
                    Convert.ToDecimal(txtTotal1.Text.Replace(",", "")),
                    Convert.ToDecimal(txtImpuesto1.Text.Replace(",", "")),
                    rutaCompletaArchivo,
                    cmbProveedroAlterno?.SelectedValue?.ToString(),
                    cmbCentroCostosAlterno?.SelectedValue?.ToString(),
                    txtDescuentoIm.Text.Replace(",", ""),
                    txtImpuestoIm.Text.Replace(",", "")
                );
                c.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);

                Limpiar();
                c.ConsultaGasto(TxtFolio1.Text, txtPartida);
            }

            PanelPartidasGasto.Visible = false;
            btnTerminarGasto.Visible = true;
            c.CargarRecibosPartidasGasto(guna2DataGridView1, TxtFolio1.Text);

            // NUEVO: recalcula los totales visibles de la lista de partidas.
            SumarColumnasPartida();

            dgvComprobantesPartidas.Rows.Clear();
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el Producto para continuar");
                return;
            }
            else if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }
            else if (txtOrden.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (cmbProoveedorAlternoSiNo.Text == "Si" && string.IsNullOrEmpty(cmbProveedroAlterno.Text))
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor alterno");
                    return;
                }
                c.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal(txtDescuento1.Text.Replace(",", "")), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto1.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), cmbCentroCostosAlterno?.SelectedValue?.ToString(), txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""));
                c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text.Replace(",", ""));
                c.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                c.Consulta5RegistroGasto(TxtFolio1.Text, txtPartida);
                c.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
                Limpiar();
                c.ConsultaGasto(TxtFolio1.Text, txtPartida);
                dgvComprobantesPartidas.Rows.Clear();

                // CORREGIDO: "Siguiente Partida" guarda la partida en la base
                // de datos pero antes NO refrescaba guna2DataGridView1 (la
                // lista de partidas ya capturadas), así que la partida recién
                // guardada no aparecía en pantalla hasta que el usuario
                // presionaba "Confirmar Partida" al final. Ahora se refresca
                // igual que en btnConfirmarPartida_Click.
                c.CargarRecibosPartidasGasto(guna2DataGridView1, TxtFolio1.Text);
                SumarColumnasPartida();

                if (txtOrden.Text != string.Empty)
                {
                    c.SeleccionarProductoGasto(cmbConcepto, txtOrden.Text);
                }
                else
                {
                    c.SeleccionarProductoGasto(cmbConcepto);
                }
            }
        }

        private void btnLimpiarPartida_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            MessageBox.Show(c.EliminarPartidaRegistroGasto(txtFolio.Text, txtPartida.Text));
            string maximo = c.ObtenerTotalPartidaRegistroGasto(txtFolio.Text);
            c.ActualizarGasto(txtFolio.Text, maximo);
            c.ReciboSaldosGastos(txtFolio.Text, txtSubtotalR, txtDescuentoR, txtImpuestoR, txtTotalR, txtPartidas, txtSaldo);

            c.CargarRecibosPartidasGasto(guna2DataGridView1, txtFolio.Text);

            // NUEVO: recalcula los totales visibles tras eliminar la partida.
            SumarColumnasPartida();

            LimpiarDetalle();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return; // Salida temprana si no es una fila válida

            try
            {
                // Llenar el combo de conceptos
                c.SeleccionarProductoGasto(cmbConcepto);

                // Obtener la partida seleccionada
                string partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value?.ToString();
                txtPartida.Text = partida;

                // Desconectar el evento temporalmente
                cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

                // Consultar y llenar los campos
                c.ConsultaPartidaGasto(
                    txtFolio.Text,
                    partida,
                    txtClave1,
                    cmbConcepto,
                    txtConcepto2,
                    txtCantidad,
                    txtUnidad,
                    txtDivisa1,
                    txtTipoCambio1,
                    txtSubtotal1,
                    txtDescuento1,
                    txtTotal1,
                    txtImpuesto1,
                    txtArchivo1,
                    cmbProveedroAlterno,
                    cmbCentroCostosAlterno,
                    txtDescuentoIm,
                    txtImpuestoIm, txtPrecio
                );

                // Reconectar el evento
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

                // Calcular el precio unitario de forma segura
                decimal subtotal = 0, cantidad = 0, descuentoIm = 0;

                decimal.TryParse(txtSubtotal1.Text.Replace(",", ""), out subtotal);
                decimal.TryParse(txtCantidad.Text.Replace(",", ""), out cantidad);
                decimal.TryParse(txtDescuentoIm.Text.Replace(",", ""), out descuentoIm);

                PanelPartidasGasto.Visible = true;
                btnCerrarPanelPartida.Visible = true;

                c.mostrarArchivos(dgvComprobantesPartidas, txtFolio.Text, txtPartida.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al seleccionar la partida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (txtOrden.Text != string.Empty)
                {
                    string[] valores = c.InformacionGastoo(cmbConcepto.Text, txtOrden.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtDescuento1.Text = valores[3];
                    txtTotal1.Text = valores[4];
                    txtConcepto2.Text = valores[5];
                    txtPartidaOrden.Text = valores[7];
                    txtCantidad2.Text = valores[8];
                    txtCantidad.Text = valores[8];
                    txtImpuesto1.Text = valores[9];
                    txtUnidad.Text = valores[10];
                }
                else
                {
                    string[] valores = c.InformacionGasto(cmbConcepto.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtUnidad.Text = valores[3];
                    txtImpuesto1.Text = valores[4];
                }

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();
            }
        }

        private void cmbConcepto_Click(object sender, EventArgs e)
        {
            if (actualizarcombo == "Si")
            {
                c.SeleccionarProductoGasto(cmbConcepto);
                actualizarcombo = string.Empty;
            }
        }

        private void cmbProveedroAlterno_Click(object sender, EventArgs e)
        {
            if (actualizarproveedor == "Si")
            {
                LlenarComboProveedores();
                actualizarproveedor = string.Empty;
            }
        }

        private void cmbProveedroAlterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (consultaRegistros != "SI")
            {
                c.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty && txtCantidad2.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtCantidad2.Text))
                    {
                        MessageBox.Show("No es posible registrar una cantidad mayor a la registrada en la orden de commpra");
                        txtCantidad.Text = txtCantidad2.Text;
                    }
                    else
                    {
                        Calcular();
                    }
                }
                else if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de cantidad incorrecto");
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty && txtCantidad2.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtCantidad2.Text))
                    {
                        MessageBox.Show("No es posible registrar una cantidad mayor a la registrada en la orden de commpra");
                        txtCantidad.Text = txtCantidad2.Text;
                    }
                    else
                    {
                        Calcular();
                    }
                }
                else if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    Calcular();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal1);
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuesto1);

            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento1);

            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de descuento incorrecto");
            }
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            // CORREGIDO: este evento es del campo txtTotal1 (total de la
            // PARTIDA), pero estaba formateando "txtTotal" (el total del
            // ENCABEZADO) por error. Ahora formatea el campo que realmente le
            // corresponde.
            Moneda(ref txtTotal1);
        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotalR);
        }

        private void txtDescuentoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoR);
        }

        private void txtTotalR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotalR);
        }

        private void txtImpuestoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoR);
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtSubtotal1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        #endregion

        #region Archivos y comprobantes

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            if (txtOrdenCompra.Text != string.Empty)
            {
                MessageBox.Show("No es posible cambiar proveedor de la orden de compra");
            }
            else
            {
                BuscarListaProveedores buscarListaAlumnos2 = new BuscarListaProveedores();
                buscarListaAlumnos2.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            cmbDocumento.DroppedDown = false;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;

            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + NoOrdenResl;

                    ArchivoUtil.AbrirArchivo(Carpeta + @"\" + txtArchivo.Text);
                }
                else if (txtFolio.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;

            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;
                    Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + NoOrdenResl;
                    if (Directory.Exists(Carpeta))
                    {
                        ArchivoUtil.EliminarArchivo(Carpeta + @"\" + txtArchivo.Text);
                        txtArchivo.Clear();
                        c.ModificarExtension4(txtFolio.Text, txtClave.Text);
                    }
                }
                else if (txtFolio.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro de gasto confirmada para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme el registro de gastos antes de continuar");
            }
            else
            {
                DocumentoGlobalesGastoVer documentoConceptoGlobalVer = new DocumentoGlobalesGastoVer(txtFolio.Text);
                documentoConceptoGlobalVer.ShowDialog();
            }

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            cmbDocumento.DroppedDown = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;

            if (DBOrdenCompra.Ruta != string.Empty)
            {
                string FolioOrden = txtOrdenCompra.Text;
                int opcion = 0;
                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarRegistroGasto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, FolioOrden, txtConsecutivo.Text, txtReferencia.Text, txtDiasVence.Text, txtFechaVence.Text, cmbCentroCostos.SelectedValue.ToString(), cmbSemana.SelectedIndex + 1, dtpAnio.Text, cmbProoveedorAlternoSiNo.Text);
                    opcion = 1;
                }

                if (txtFolio.Text != string.Empty)
                {
                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + NoOrdenResl;

                    try
                    {
                        ArchivoUtil.CrearCarpetaSiNoExiste(Carpeta);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + NoOrdenResl;

                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "All Files|*.*";

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string archivo = open.FileName;
                        string ext = Path.GetExtension(archivo);
                        try
                        {
                            ArchivoUtil.CopiarArchivo(archivo, Carpeta + @"\" + Descripcion + ext);
                            txtArchivo.Text = Descripcion + ext;
                            c.ModificarExtension5gasto(txtFolio.Text, txtClave.Text, ext);
                            c.ActualizarRecepcion3gasto(txtFolio.Text, txtClave.Text, txtArchivo.Text);

                            if (opcion == 1)
                            {
                                PartidaGastos partidas = new PartidaGastos(txtFolio.Text, txtDocumento.Text, FolioOrden, opcion);
                                partidas.ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ya hay un archivo guardado" + ex.ToString());
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void btnAdjuntarComporbante_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(DBOrdenCompra.Ruta)))
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtFolio1.Text))
            {
                MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                return;
            }

            string noOrden = TxtFolio1.Text;
            string descripcion = txtPartida.Text;
            string carpetaDestino = Path.Combine(DBOrdenCompra.Ruta, "G" + noOrden);

            try
            {
                ArchivoUtil.CrearCarpetaSiNoExiste(carpetaDestino);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear carpeta: " + ex.Message);
                return;
            }

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Todos los archivos|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = open.FileName;
                string extensionArchivo = Path.GetExtension(archivoSeleccionado);
                string nombreArchivo = Path.GetFileNameWithoutExtension(archivoSeleccionado);
                string rutaCompletaArchivo = Path.Combine(carpetaDestino, nombreArchivo);
                string contenidoArchivo = string.Empty;

                try
                {
                    ArchivoUtil.CopiarArchivo(archivoSeleccionado, rutaCompletaArchivo, true);
                    txtArchivo1.Text = nombreArchivo;

                    contenidoArchivo = ArchivoUtil.ConvertirArchivoABase64(archivoSeleccionado);

                    MessageBox.Show(c.insertaArchivos(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, nombreArchivo, extensionArchivo, contenidoArchivo));

                    c.mostrarArchivos(dgvComprobantesPartidas, TxtFolio1.Text, txtPartida.Text);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Ya hay un archivo guardado o en uso: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar archivo: " + ex.Message);
                    return;
                }
            }
        }

        private void dgvComprobantesPartidas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvComprobantesPartidas.Rows[e.RowIndex];

                if (dgvComprobantesPartidas.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    var secuencia = filaSeleccionada.Cells[0].Value?.ToString() ?? "";
                    var folio = filaSeleccionada.Cells[1].Value?.ToString() ?? "";
                    var partida = filaSeleccionada.Cells[2].Value?.ToString() ?? "";
                    var archivo = filaSeleccionada.Cells[4].Value?.ToString() ?? "";

                    DialogResult result = MessageBox.Show($"¿Deseas eliminar el registro : {archivo}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string resultado = c.EliminarArchivosGastos(folio, partida, secuencia);
                        if (resultado == "Eliminado")
                        {
                            dgvComprobantesPartidas.Rows.Remove(filaSeleccionada);
                            MessageBox.Show($"Archivo {archivo} eliminado correctamente.");
                            c.mostrarArchivos(dgvComprobantesPartidas, TxtFolio1.Text, txtPartida.Text);
                        }
                    }
                }

                if (dgvComprobantesPartidas.Columns[e.ColumnIndex].Name == "Ver")
                {
                    try
                    {
                        if (!dgvComprobantesPartidas.Columns.Contains("ContenidoArchivo"))
                        {
                            MessageBox.Show("La columna 'ContenidoArchivo' no existe.");
                            return;
                        }

                        string base64Archivo = filaSeleccionada.Cells["ContenidoArchivo"].Value?.ToString();

                        if (string.IsNullOrEmpty(base64Archivo))
                        {
                            MessageBox.Show("El archivo está vacío o nulo.");
                            return;
                        }

                        string nombreArchivo = "archivo_visualizado";

                        string rutaTemporal = ArchivoUtil.GuardarArchivoTemporalDesdeBase64(base64Archivo, nombreArchivo);
                        ArchivoUtil.AbrirArchivoConShell(rutaTemporal);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al mostrar el archivo: " + ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Limpieza y bloqueo de controles

        /// <summary>
        /// Limpia los campos de la PARTIDA actual después de guardarla, para
        /// dejar el detalle listo y capturar la siguiente partida sin cerrar
        /// el panel.
        /// </summary>
        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtSubtotal1.Text = "0.00";
            txtDescuento1.Text = "0.00";

            // CORREGIDO: aquí se reseteaba "txtTotal" (el total del
            // ENCABEZADO, que refleja la suma de todas las partidas ya
            // guardadas en la base de datos) en lugar de "txtTotal1" (el
            // total de la PARTIDA que se acaba de guardar). Esto dejaba en
            // "0.00" el total del encabezado cada vez que se guardaba una
            // partida, aunque el encabezado ya tuviera saldo. Ahora limpia el
            // campo que realmente corresponde al detalle.
            txtTotal1.Text = "0.00";

            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtImpuesto1.Text = "0";
            txtUnidad.Clear();
            txtArchivo1.Clear();
            rutaCompletaArchivo = string.Empty;
            nombreArchivo = string.Empty;
            extensionArchivo = string.Empty;
            cmbProveedroAlterno.SelectedIndex = -1;

            cmbCentroCostosAlterno.SelectedIndex = -1;
            cmbCentroCostosAlterno.Text = cmbCentroCostos.Text;
            txtSubtotalGrid.Text=string.Empty;
            txtDescuentoGrid.Text = string.Empty;
            txtImpuestoGrid.Text = string.Empty;
            txtTotalGrid.Text = string.Empty;
        }

        /// <summary>
        /// Limpia por completo el panel de detalle de partida (se usa al abrir
        /// el panel para una partida nueva y al terminar todo el registro de
        /// gasto).
        /// </summary>
        void LimpiarDetalle()
        {
            txtPartida.Text = string.Empty;
            txtConcepto2.Text = string.Empty;
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtUnidad.Text = string.Empty;
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtSubtotal.Text = "0.00";
            txtImpuesto1.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtDescuentoIm.Text = "0.00";
            txtImpuestoIm.Text = "0.00";
            txtTotal1.Text = "0.00";
            txtArchivo1.Text = string.Empty;

            txtSubtotalR.Text = "0.00";
            txtImpuestoR.Text = "0.00";
            txtDescuentoR.Text = "0.00";
            txtImpuestoR.Text = "0.00";
            txtTotalR.Text = "0.00";
            cmbProveedroAlterno.SelectedIndex = -1;
            cmbCentroCostosAlterno.SelectedIndex = -1;
            cmbCentroCostosAlterno.Text = cmbCentroCostos.Text;
            dgvComprobantesPartidas.Rows.Clear();
        }

        /// <summary>
        /// Limpia por completo el ENCABEZADO del gasto (se usa al terminar el
        /// registro y al iniciar uno nuevo desde el menú lateral).
        /// </summary>
        void Limpiarcabezado()
        {
            txtConsecutivo.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtDiasVence.Text = string.Empty;
            txtFechaVence.Text = string.Empty;

            txtMatricular.Text = string.Empty;
            txtNombreAlumnno.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtImpuestos.Text = "0.00";
            txtDivisa.Text = string.Empty;
            txtTipoCambio.Text = "0.00";
            txtTotal.Text = "0.00";
            txtAbono.Text = "0.00";
            txtSaldo.Text = "0.00";
            txtPartidas.Text = string.Empty;
            txtNotas.Text = string.Empty;
            txtElaborado.Text = string.Empty;
            txtArchivo.Text = string.Empty;
            cmbDocumento.SelectedIndex = -1;
            cmbFiltroDocumentoC.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbOrdenCompra.SelectedIndex = -1;
            cmbCentroCostos.SelectedIndex = -1;
            cmbSemana.SelectedIndex = -1;
            dtpAnio.Value = DateTime.Now;
            cmbProoveedorAlternoSiNo.SelectedIndex = -1;
            cmbProoveedorAlternoSiNo.Text = "Si";
        }

        void BloquearEncabezado()
        {
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;

            cmbFiltroDocumentoC.Enabled = false;
            cmbProveedor.Enabled = false;
            cmbOrdenCompra.Enabled = false;
            txtReferencia.Enabled = false;
            txtNotas.Enabled = false;
            txtArchivo.Enabled = false;
            btLimpiarOrden.Enabled = false;
            btnBuscarProveedor.Enabled = false;
            button7.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        void DesbloquearEncabezado()
        {
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;

            cmbFiltroDocumentoC.Enabled = true;
            cmbProveedor.Enabled = true;
            cmbOrdenCompra.Enabled = true;
            txtReferencia.Enabled = true;
            txtNotas.Enabled = true;
            txtArchivo.Enabled = true;
            btLimpiarOrden.Enabled = true;
            btnBuscarProveedor.Enabled = true;
            button7.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            cmbCentroCostos.Enabled = true;
            dtpAnio.Enabled = true;
            cmbSemana.Enabled = true;
            cmbProoveedorAlternoSiNo.Enabled = true;
        }

        void BloquearDetalle()
        {
            cmbConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtImpuesto1.Enabled = false;
            txtDescuento1.Enabled = false;
            cmbProveedroAlterno.Enabled = false;
            cmbCentroCostosAlterno.Enabled = false;
            cmbCentroCostos.Enabled = false;
            dtpAnio.Enabled = false;
            cmbSemana.Enabled = false;
            cmbProoveedorAlternoSiNo.Enabled = false;
        }

        void DesbloquearDetalle()
        {
            cmbConcepto.Enabled = true;
            txtConcepto2.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtImpuesto1.Enabled = true;
            txtDescuento1.Enabled = true;
            cmbProveedroAlterno.Enabled = true;
            cmbCentroCostosAlterno.Enabled = true;
        }

        #endregion

        #region Totales del encabezado (formato de moneda)

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }

        private void txtAbono_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAbono);
        }

        private void txtImpuestos_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestos);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        #endregion

        #region Barra lateral / panel expandible de accesos rápidos

        /// <summary>
        /// Colapsa el panel lateral de accesos rápidos (NUEVO, CONSULTAR,
        /// IMPRIMIR, PROVEEDORES, SERVICIOS) a su tamaño reducido. Antes este
        /// mismo bloque de ~17 líneas estaba copiado y pegado de forma
        /// idéntica en cada una de las 5 ramas de toolStrip2_ItemClicked_1.
        /// </summary>
        private void ColapsarPanelLateral()
        {
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 621);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;

            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 621);

            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;
            toolStripButton5.Visible = true;

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;

            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 621);

            guna2GradientPanel7.Size = new Size(23, 621);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);
        }

        private void toolStrip2_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                txtFolio.Text = "";
                consultaRegistros = "NO";
                guna2DataGridView1.Rows.Clear();

                ColapsarPanelLateral();

                // NOTA: "txtFolio.Text" se acaba de poner en "" dos líneas
                // arriba, así que esta condición "!string.IsNullOrEmpty(txtFolio.Text)"
                // nunca es verdadera y el mensaje de confirmación de abajo
                // nunca llega a mostrarse. Se deja igual que en el original
                // para no cambiar el comportamiento sin que lo confirmes,
                // pero probablemente quieras guardar el folio ANTES de
                // limpiarlo si el mensaje debería aparecer.
                if (!string.IsNullOrEmpty(txtFolio.Text) && cmbEstatus.Text == "Abierto")
                {
                    if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nuevo Registro de Gasto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                Limpiarcabezado();
                LimpiarDetalle();
                DesbloquearEncabezado();
                DesbloquearDetalle();

                PrepararNuevoRegistro();
                guna2TabControl1.Enabled = true;
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2DataGridView1.Rows.Clear();
                consultaRegistros = "SI";

                ColapsarPanelLateral();

                if (guna2GradientPanel2.Visible == true)
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel2.SendToBack();
                }
                else
                {
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = true;
                    guna2GradientPanel2.BringToFront();
                }

                guna2TabControl1.Enabled = true;

                BloquearDetalle();
                BloquearEncabezado();
                guna2TabControl1.Enabled = true;
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                ColapsarPanelLateral();
            }
            else if (e.ClickedItem.Text == "PROVEEDORES")
            {
                ColapsarPanelLateral();

                actualizarproveedor = "Si";

                Proveedores p = new Proveedores();
                p.ShowDialog();
            }
            else if (e.ClickedItem.Text == "SERVICIOS")
            {
                ColapsarPanelLateral();

                int Consulta = 0;
                actualizarcombo = "Si";
                CatalogoServicios s = new CatalogoServicios(Consulta);
                s.ShowDialog();
            }
        }

        private void toolStrip2_Click(object sender, EventArgs e)
        {
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        #endregion

        #region Accesos a catálogos externos (proveedores / centro de costos)

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Proveedores p = new Proveedores();
            p.ShowDialog();
            LlenarComboProveedores();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            CentroCostos p = new CentroCostos();
            p.ShowDialog();
            LlenarComboCentro();
        }

        #endregion

        #region Controles varios generados por el diseñador (sin lógica propia)

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmb(object sender, EventArgs e)
        {
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void guna2Button13_Click_1(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel2.SendToBack();
        }

        private void btnCerrarPanelPartida_Click(object sender, EventArgs e)
        {
            PanelPartidasGasto.Visible = false;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            if (guna2TabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtFolio.Text))
                {
                    MessageBox.Show("Es necesario crear el encabezado");
                    guna2TabControl1.SelectedIndex = 0;
                    toolStripButton1.Enabled = true;
                    toolStripButton2.Enabled = true;
                    toolStripButton3.Enabled = true;
                }
                else if (consultaRegistros != "SI")
                {
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                    toolStripButton3.Enabled = false;
                }
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            CalcularFechaVencimiento();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
        }

        #endregion
    }
}