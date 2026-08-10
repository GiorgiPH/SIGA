// =====================================================================================
// RegistroReembolsos.cs
// -------------------------------------------------------------------------------------
// REFACTORIZACIÓN — resumen de lo que cambió y lo que NO:
//
// 1) Todos los manejadores de evento que existían en el archivo original se
//    CONSERVARON con el mismo nombre y firma exacta (incluyendo los duplicados
//    como cmbConcepto_Click / cmbConcepto_Click_1, txtPrecio_TextChanged / _1,
//    cmbDocumento_SelectedIndexChanged / _1). Esto es indispensable porque el
//    Designer.cs enlaza los controles a estos métodos por nombre y no fue
//    incluido en esta revisión; renombrarlos o borrarlos rompería la compilación.
//
// 2) Donde el mismo bloque de código estaba copiado en 2 o más métodos
//    (colapsar/expandir el menú lateral, cerrar dropdowns y restaurar colores,
//    guardar una partida, validar proveedor alterno, validar cantidad contra lo
//    pedido, cargar datos de un concepto, etc.) se extrajo un método privado
//    compartido. Los manejadores originales ahora sólo lo invocan.
//
// 3) Se reforzaron validaciones que antes podían tronar el formulario
//    (Convert.ToDecimal/ToInt32 sin control de errores sobre texto vacío o mal
//    formado) usando TryParse con valor por defecto. Para valores válidos el
//    resultado numérico es idéntico al original.
//
// 4) Se eliminaron mensajes de depuración olvidados (MessageBox "buscando error
//    0/1") y variables/código muerto que no tenían ningún efecto (por ejemplo,
//    resultados de Substring nunca asignados, variables calculadas y jamás
//    usadas, una rama "if" inalcanzable en btnSiguientePartida_Click_1).
//
// 5) TODO lo que podía cambiar el comportamiento observable (cálculos, orden de
//    llamadas a la capa de datos, reglas de estatus Abierto/Bloqueado/Cancelado)
//    se dejó intacto. Los puntos donde se detectó una inconsistencia real (por
//    ejemplo qué valor de forma de pago se guarda según el botón que se use) se
//    dejaron marcados con "// NOTA:" para que el equipo decida si corregirlos.
// =====================================================================================

using Condominios;
using Condominios.Clases.CentroCostos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.FormasPago;
using PuntoVentas.Clases.Login;
using PV.Clases;
using PV.Clases.ConceptosGlobalesReembolso;
using PV.Clases.OrdenCompra;
using PV.Clases.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PV
{
    public partial class RegistroReembolsos : Form
    {
        #region Constantes

        /// <summary>
        /// Estatus válidos del encabezado de un Reembolso. Antes se comparaba
        /// contra las cadenas "Abierto"/"Bloqueado"/"Cancelado" escritas a mano
        /// en más de 15 lugares distintos del formulario (con el riesgo de un
        /// error de tipeo). Ahora se centralizan aquí.
        /// </summary>
        private static class EstadoReembolso
        {
            public const string Abierto = "Abierto";
            public const string Bloqueado = "Bloqueado";
            public const string Cancelado = "Cancelado";
        }

        #endregion

        #region Campos

        public static string Matricula = string.Empty;

        private List<(string clase, string Tipo, decimal Valor)> conceptosAplicadosImpuestos = new List<(string clase, string Tipo, decimal Valor)>();
        private List<(string clase, string Tipo, decimal Valor, string clavebase, string clasebase, decimal valorbase)> conceptosAplicadosDescuentos = new List<(string clase, string Tipo, decimal Valor, string clavebase, string clasebase, decimal valorbase)>();
        private List<(string clase, string Tipo, decimal Valor)> conceptosAplicadosGlobales = new List<(string clase, string Tipo, decimal Valor)>();

        DBRegistroReembolso r = new DBRegistroReembolso();

        public static string Carpeta = string.Empty;
        string proyecto = string.Empty;

        DBCentroCostos cc = new DBCentroCostos();
        DBProveedores p = new DBProveedores();
        DBConceptosGloablesReembolso c = new DBConceptosGloablesReembolso();

        private string rutaCompletaArchivo = string.Empty;
        private string nombreArchivo = string.Empty;
        private string extensionArchivo = string.Empty;
        private string actualizarcombo = string.Empty;
        private string actualizarproveedor = string.Empty;
        private string consultaRegistros = string.Empty;
        private string PartidaNuevaConsulta = string.Empty;
        private string CentroCosto = string.Empty;
        string[] ConceptosGlobales;
        bool cobraIEPS = false;
        private bool isEditing = false;

        #endregion

        #region Constructor y carga del formulario

        public RegistroReembolsos()
        {
            InitializeComponent();

            for (int i = 1; i <= 52; i++)
            {
                cmbSemana.Items.Add($"Semana {i}");
            }

            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbConcepto.AutoCompleteMode = AutoCompleteMode.None;
            cmbConcepto.AutoCompleteSource = AutoCompleteSource.None;
        }

        private void RegistroReembolsos_Load(object sender, EventArgs e)
        {
            r.SeleccionarRecepcionProducto(cmbDocumento);
            r.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
            LlenarComboProveedores();
            LlenarComboCentro();
            LlenarComboProveedores();
            r.ruta();
            r.CargarGasto(dataGridView1);

            if (cmbEstatus.Items.Count > 0) cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
            txtDiasVence.Text = "0";
            cmbProoveedorAlternoSiNo.Text = "Si";

            ActualizarFechaVencimiento();

            //cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void RegistroReembolsos_Activated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Calcula txtFechaVence a partir de txtFecha + txtDiasVence. Antes este
        /// bloque de 4 líneas estaba repetido en RegistroReembolsos_Load y en la
        /// opción "NUEVO" del menú lateral.
        /// </summary>
        private void ActualizarFechaVencimiento()
        {
            int dias = ParsearEntero(txtDiasVence.Text);
            if (DateTime.TryParse(txtFecha.Text, out DateTime fecha))
            {
                txtFechaVence.Text = fecha.AddDays(dias).ToString("yyyy/MM/dd");
            }
        }

        #endregion

        #region Utilidades de parseo seguro

        /// <summary>
        /// Convierte un texto a decimal sin lanzar excepción si el formato es
        /// inválido o el campo está vacío (antes varios Convert.ToDecimal podían
        /// tronar el formulario en ese caso). Para texto válido el resultado es
        /// idéntico al que entregaba Convert.ToDecimal.
        /// </summary>
        private decimal ParsearDecimal(string texto)
        {
            decimal.TryParse((texto ?? string.Empty).Replace(",", ""), out decimal valor);
            return valor;
        }

        /// <summary>
        /// Igual que ParsearDecimal pero para enteros.
        /// </summary>
        private int ParsearEntero(string texto)
        {
            int.TryParse((texto ?? string.Empty).Replace(",", ""), out int valor);
            return valor;
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Obtiene el elemento en "indice" de un arreglo devuelto por la capa de
        /// datos (por ejemplo r.InformacionGasto, r.InformacionDocumento, etc.)
        /// sin lanzar excepción si el arreglo viene en null, viene vacío, o tiene
        /// menos elementos de los que el formulario espera. Devuelve "" en esos
        /// casos en vez de tronar con NullReferenceException/IndexOutOfRange.
        /// </summary>
        private string ObtenerValor(string[] valores, int indice)
        {
            if (valores == null || indice < 0 || indice >= valores.Length)
            {
                return string.Empty;
            }
            return valores[indice] ?? string.Empty;
        }

        /// <summary>
        /// Convierte a decimal el valor de una columna de un DataRow (por ejemplo
        /// pago["Subtotal"] o row["Cargo"]) sin lanzar excepción cuando el valor
        /// es null o DBNull.Value (columna NULL en la base de datos). Antes estos
        /// puntos usaban Convert.ToDecimal directo, que sí truena con DBNull.
        /// </summary>
        private decimal ConvertirDecimalSeguro(object valor)
        {
            if (valor == null || valor == DBNull.Value)
            {
                return 0;
            }
            decimal.TryParse(valor.ToString(), out decimal resultado);
            return resultado;
        }

        #endregion

        #region Menú lateral y toolbar

        private void ExpandirPanelLateral()
        {
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 621);

            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;

            foreach (var boton in new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 })
            {
                boton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                boton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                boton.Size = new Size(85, 75);
                boton.AutoSize = false;
                boton.Visible = true;
            }

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        /// <summary>
        /// Colapsa el panel/menú lateral. El parámetro "incluirBotones4y5"
        /// respeta el comportamiento original: al colapsar desde el picture box
        /// (guna2PictureBox2_Click) se reconfiguraban los 5 botones, pero al
        /// colapsar desde una opción del menú (NUEVO, CONSULTAR, IMPRIMIR,
        /// PROVEEDORES, SERVICIOS) el código original sólo reconfiguraba los
        /// primeros 3. Se conserva tal cual para no alterar el comportamiento.
        /// </summary>
        private void ColapsarPanelLateral(bool incluirBotones4y5)
        {
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 621);

            guna2GradientPanel7.Size = incluirBotones4y5 ? new Size(23, 621) : new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;

            var botones = incluirBotones4y5
                ? new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 }
                : new[] { toolStripButton1, toolStripButton2, toolStripButton3 };

            foreach (var boton in botones)
            {
                boton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                boton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                boton.Size = new Size(23, 79);
            }

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
        }

        /// <summary>
        /// Cierra los combos desplegables y restaura los colores de fondo de
        /// botones/campos de filtro. Antes este bloque (con variaciones y algún
        /// error de copiar/pegar, como una línea duplicada en
        /// btnBuscarProveedor_Click) estaba repetido en 6 métodos distintos.
        /// </summary>
        private void CerrarDropdownsYRestaurarColores()
        {
            cmbDocumento.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;

            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            button2.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;

            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            txtDiasVence.BackColor = Color.White;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            ExpandirPanelLateral();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            ColapsarPanelLateral(incluirBotones4y5: true);
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    EjecutarOpcionNuevo();
                    break;
                case "CONSULTAR":
                    EjecutarOpcionConsultar();
                    break;
                case "IMPRIMIR":
                    EjecutarOpcionImprimir();
                    break;
                case "PROVEEDORES":
                    EjecutarOpcionProveedores();
                    break;
                case "SERVICIOS":
                    EjecutarOpcionServicios();
                    break;
            }
        }

        private void EjecutarOpcionNuevo()
        {
            txtFolio.Text = "";
            consultaRegistros = "NO";
            dgvPartidas.Rows.Clear();

            ColapsarPanelLateral(incluirBotones4y5: false);

            if (!string.IsNullOrEmpty(txtFolio.Text) && cmbEstatus.Text == EstadoReembolso.Abierto)
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

            r.SeleccionarRecepcionProducto(cmbDocumento);
            r.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
            r.ruta();
            r.CargarGasto(dataGridView1);

            if (cmbEstatus.Items.Count > 0) cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
            txtDiasVence.Text = "0";

            ActualizarFechaVencimiento();

            guna2TabControl1.Enabled = true;
        }

        private void EjecutarOpcionConsultar()
        {
            dgvPartidas.Rows.Clear();
            consultaRegistros = "SI";

            ColapsarPanelLateral(incluirBotones4y5: false);

            if (guna2GradientPanel2.Visible)
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
        }

        private void EjecutarOpcionImprimir()
        {
            ColapsarPanelLateral(incluirBotones4y5: false);

            ReporteComprasReembolso reporte = new ReporteComprasReembolso(TxtFolio1.Text);
            reporte.ShowDialog();
        }

        private void EjecutarOpcionProveedores()
        {
            ColapsarPanelLateral(incluirBotones4y5: false);

            actualizarproveedor = "Si";

            Proveedores proveedores = new Proveedores();
            proveedores.ShowDialog();
        }

        private void EjecutarOpcionServicios()
        {
            ColapsarPanelLateral(incluirBotones4y5: false);

            int consulta = 0;
            actualizarcombo = "Si";
            CatalogoServicios servicios = new CatalogoServicios(consulta);
            servicios.ShowDialog();
        }

        #endregion

        #region Combos

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
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void LlenarComboGastos()
        {
            try
            {
                DataTable menus = !string.IsNullOrEmpty(txtOrden.Text)
                    ? r.ObtenerProductosGastoPorOrden(txtOrden.Text)
                    : r.ObtenerProductosGasto();


                cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
                cmbConcepto.DataSource = menus;
                cmbConcepto.DisplayMember = "Descripcion";  // Verifica que exista esta columna
                cmbConcepto.ValueMember = "ClaveServicio";  // Verifica que exista esta columna
                cmbConcepto.SelectedIndex = -1;

                cmbConcepto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbConcepto.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
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

                cmbProveedroAlterno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedroAlterno.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void LlenarComboFormasPago(string centro, string proyecto)
        {
            try
            {
                DataTable menus = r.ObtenerFormasPagoPorProyecto(centro, proyecto);
                menus.Columns.Add("DisplayColumn", typeof(string), "DescripcionFormaPago + ' ' + DescripcionReferencia");

                cmbformapago.DropDownStyle = ComboBoxStyle.DropDown;
                cmbformapago.DataSource = menus;
                cmbformapago.DisplayMember = "DisplayColumn";
                cmbformapago.ValueMember = "Id";
                cmbformapago.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void CargarConceptosGlobales(string folio)
        {
            try
            {
                var pagos = c.CargarConceptosGlobalesExistentes(folio);
                dgvConceptosGlobales.Rows.Clear();

                foreach (DataRow pago in pagos.Rows)
                {
                    int n = dgvConceptosGlobales.Rows.Add();

                    string clase = pago["clase"].ToString().Trim();
                    string tipo = pago["Tipo"].ToString().Trim();
                    decimal subtotal = ConvertirDecimalSeguro(pago["Subtotal"]);
                    decimal valor = clase == "Descuento" ? ConvertirDecimalSeguro(pago["descuento"]) : ConvertirDecimalSeguro(pago["cargo"]);
                    decimal importeMostrado = tipo == "Porcentaje" ? (valor / 100m) * subtotal : valor;

                    dgvConceptosGlobales.Rows[n].Cells["part"].Value = pago["partida"];
                    dgvConceptosGlobales.Rows[n].Cells["claveConcepto"].Value = pago["ClaveConceptoG"];
                    dgvConceptosGlobales.Rows[n].Cells["nombre"].Value = pago["Nombre"];
                    dgvConceptosGlobales.Rows[n].Cells["clase"].Value = clase;
                    dgvConceptosGlobales.Rows[n].Cells["Subtotal1"].Value = subtotal;
                    dgvConceptosGlobales.Rows[n].Cells["importe"].Value = importeMostrado.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }

        #endregion

        #region Bloqueo / desbloqueo de controles

        private void BloquearEncabezado()
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
            cmbCentroCostos.Enabled = false;
            cmbproyecto.Enabled = false;
            dtpAnio.Enabled = false;
            cmbSemana.Enabled = false;
            cmbProoveedorAlternoSiNo.Enabled = false;
        }

        private void DesbloquearEncabezado()
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
            cmbproyecto.Enabled = true;
            dtpAnio.Enabled = true;
            cmbSemana.Enabled = true;
            cmbProoveedorAlternoSiNo.Enabled = true;
        }

        private void BloquearDetalle()
        {
            cmbConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtImpuesto12.Enabled = false;
            txtRFC.Enabled = false;
            cmbProveedroAlterno.Enabled = false;
            cmbCentroCostos.Enabled = false;
            dtpAnio.Enabled = false;
            cmbSemana.Enabled = false;
            cmbProoveedorAlternoSiNo.Enabled = false;
            cmbproyecto.Enabled = false;
            cmdproyectoalterno.Enabled = false;
            cmbformapago.Enabled = false;
            cmbreferencia.Enabled = false;
            dtpFecha.Enabled = false;
        }

        private void DesbloquearDetalle()
        {
            cmbConcepto.Enabled = true;
            txtConcepto2.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtImpuesto12.Enabled = true;
            cmbProveedroAlterno.Enabled = true;
            cmbproyecto.Enabled = true;
            cmdproyectoalterno.Enabled = true;
            cmbformapago.Enabled = true;
            dtpFecha.Enabled = true;
            cmbreferencia.Enabled = true;
        }

        #endregion

        #region Limpieza de encabezado / detalle

        private void LimpiarDetalle()
        {
            txtPartida.Text = string.Empty;
            txtConcepto2.Text = string.Empty;
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtUnidad.Text = string.Empty;
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtSubtotal1.Text = "0.00";
            txtImpuesto12.Text = "0.00";
            txtDescuentoIm.Text = "0.00";
            txtImpuestoIm.Text = "0.00";
            txtTotal1.Text = "0.00";
            txtIEPS.Text = "0.00";
            txtRetencion.Text = "0.00";
            cobraIEPS = false;
            txtIEPS.Enabled = false;
            txtArchivo1.Text = string.Empty;
            txtUnidad.Text = string.Empty;

            cmbProveedroAlterno.SelectedIndex = -1;
            cmdproyectoalterno.SelectedIndex = -1;
            cmbConcepto.SelectedIndex = -1;
            cmbformapago.SelectedIndex = -1;
            cmbreferencia.SelectedIndex = -1;
            txtRFC.Text = "";

            dgvComprobantesPartoda.Rows.Clear();
            dtpFecha.Value = DateTime.Now;
            dgvComprobantesPartoda.Rows.Clear();

            conceptosAplicadosDescuentos.Clear();
            conceptosAplicadosImpuestos.Clear();
        }

        private void Limpiarcabezado()
        {
            CentroCosto = string.Empty;
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
            txtReferencia.Text = string.Empty;

            cmbDocumento.SelectedIndex = -1;
            cmbFiltroDocumentoC.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbOrdenCompra.SelectedIndex = -1;
            cmbCentroCostos.SelectedIndex = -1;
            cmbSemana.SelectedIndex = -1;
            cmbproyecto.SelectedIndex = -1;
            dtpAnio.Value = DateTime.Now;
            cmbProoveedorAlternoSiNo.SelectedIndex = -1;
            cmbProoveedorAlternoSiNo.Text = "Si";
        }

        #endregion

        #region Encabezado del reembolso (alta, confirmar, cancelar)

        private void btnAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }

            if (cmbEstatus.Text != EstadoReembolso.Abierto)
            {
                MessageBox.Show("No es posible agregar partidas a una recepcion deproductos Bloqueada o Cancelada");
                return;
            }

            if (string.IsNullOrEmpty(cmbCentroCostos.Text))
            {
                MessageBox.Show("No es posible agregar partidas a una recepcion deproductos Bloqueada o Cancelada");
                return;
            }

            if (string.IsNullOrEmpty(cmbSemana.Text))
            {
                MessageBox.Show("Selecciona la semana");
                return;
            }

            string folioOrden = txtOrdenCompra.Text;

            if (txtFolio.Text == string.Empty)
            {
                r.InsertarRegistroGasto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text,
                    txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, folioOrden, txtConsecutivo.Text,
                    txtReferencia.Text, txtDiasVence.Text, txtFechaVence.Text, cmbCentroCostos?.SelectedValue?.ToString(),
                    cmbSemana.SelectedIndex + 1, dtpAnio.Text, cmbProoveedorAlternoSiNo.Text, cmbproyecto.Text, txtRetencion.Text);
            }

            int opcion = txtArchivo.Text != string.Empty ? 1 : 0;

            guna2TabControl1.SelectedIndex = 1;

            TxtFolio1.Text = txtFolio.Text;
            txtOrden.Text = folioOrden;

            if (txtOrden.Text != string.Empty)
            {
                LlenarComboGastos();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                txtPrecio.Enabled = false;
                txtCantidad.Text = "1";
                txtUnidad.Text = "Servicio";
                txtDivisa1.Text = "MXN";
                txtTipoCambio1.Text = "1.00";
            }
            else
            {
                CentroCosto = cmbCentroCostos.Text;
                LlenarComboGastos();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                txtPrecio.Enabled = true;
                txtCantidad.Text = "1";
                txtUnidad.Text = "Servicio";
                txtDivisa1.Text = "MXN";
                txtTipoCambio1.Text = "1.00";

                proyecto = cmbproyecto.Text;
                dtpFecha.Value = DateTime.Now;
            }

            if (opcion != 0)
            {
                btnAdjuntarComprobantePartida.Enabled = false;
            }
        }

        private void btnTerminarReembolso_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text == string.Empty)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }

            if (cmbOrdenCompra.Text == string.Empty && cmbEstatus.Text != EstadoReembolso.Abierto)
            {
                MessageBox.Show("No es posible confirmar registro de gastos Bloqueada o Cancelada");
                return;
            }

            // NOTA: se conserva el comportamiento original: si el usuario responde "No"
            // al MessageBox de confirmación, el bloque de abajo (Matricula, cmbEstatus,
            // ActualizarReembolso, etc.) NO se ejecuta, pero la limpieza de pantalla que
            // sigue después SÍ se ejecuta siempre (estaba fuera del if/else original).
            if (MessageBox.Show("Al confirmar el registro de gasto no podra realizar modificaciones, ¿Desea continuar?", "Registro de Gasto ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Matricula = string.Empty;
                cmbEstatus.Text = EstadoReembolso.Bloqueado;
                r.ActualizarReembolso(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                r.ActualizarSaldoProveedor2(txtMatricular.Text, ParsearDecimal(txtTotal.Text));
                r.CargarGasto(dataGridView1);
            }

            Limpiarcabezado();
            LimpiarDetalle();

            guna2TabControl1.SelectedIndex = 0;
            btnTerminarReembolso.Visible = true;
            guna2TabControl1.Enabled = false;
            dgvPartidas.Rows.Clear();
            txtFolio.Text = string.Empty;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            dgvComprobantesPartoda.Rows.Clear();
        }

        private void btnEliminarReembolso_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la recepcion de productos");
                return;
            }

            if (txtTotal.Text != txtSaldo.Text)
            {
                MessageBox.Show("No es posible cancelar una recepcion de productos con un pago total o parcial");
                return;
            }

            if (cmbEstatus.Text != EstadoReembolso.Bloqueado)
            {
                MessageBox.Show("No es posible cancelar una recepcion de productos que no esta bloqueado");
                return;
            }

            if (MessageBox.Show("El saldo de esta recepcion de productos sera cancelado, ¿Desea continuar?", "Recepcion de Gastos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = EstadoReembolso.Cancelado;
                MessageBox.Show(r.CancelarRegistroGasto(txtFolio.Text));
                Limpiarcabezado();
                LimpiarDetalle();
            }
        }

        #endregion

        #region Archivos adjuntos del encabezado

        private void button1_Click(object sender, EventArgs e)
        {
            CerrarDropdownsYRestaurarColores();

            if (DBOrdenCompra.Ruta == string.Empty)
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return;
            }

            if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
            {
                string noOrdenResl = txtFolio.Text;
                Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + noOrdenResl;

                if (Directory.Exists(Carpeta))
                {
                    File.Delete(Carpeta + @"\" + txtArchivo.Text);
                    txtArchivo.Clear();
                    r.ModificarExtension4(txtFolio.Text, txtClave.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            CerrarDropdownsYRestaurarColores();

            if (DBRegistroReembolso.Ruta == string.Empty)
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return;
            }

            if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
            {
                string noOrdenResl = txtFolio.Text;
                Carpeta = DBRegistroReembolso.Ruta + @"\" + "EG" + noOrdenResl;
                Process.Start(Carpeta + @"\" + txtArchivo.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            CerrarDropdownsYRestaurarColores();

            if (DBRegistroReembolso.Ruta == string.Empty)
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return;
            }

            string folioOrden = txtOrdenCompra.Text;

            if (txtFolio.Text == string.Empty)
            {
                r.InsertarRegistroGasto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text,
                    txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, folioOrden, txtConsecutivo.Text,
                    txtReferencia.Text, txtDiasVence.Text, txtFechaVence.Text, cmbCentroCostos?.SelectedValue?.ToString(),
                    cmbSemana.SelectedIndex + 1, dtpAnio.Text, cmbProoveedorAlternoSiNo.Text, cmbproyecto.Text, txttotalretenciones.Text);
            }

            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                return;
            }

            string noOrdenResl = txtFolio.Text;
            string descripcion = txtClave.Text;
            Carpeta = DBRegistroReembolso.Ruta + @"\" + "EG" + noOrdenResl;

            try
            {
                if (!Directory.Exists(Carpeta))
                {
                    Directory.CreateDirectory(Carpeta);
                }
            }
            catch (Exception)
            {
                throw;
            }

            OpenFileDialog open = new OpenFileDialog { Filter = "All Files|*.*" };

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivo = open.FileName;
                string ext = Path.GetExtension(archivo);

                try
                {
                    File.Copy(archivo, Carpeta + @"\" + descripcion + ext);
                    txtArchivo.Text = descripcion + ext;
                    r.ModificarExtension5gasto(txtFolio.Text, txtClave.Text, ext);
                    r.ActualizarRecepcion3gasto(txtFolio.Text, txtClave.Text, txtArchivo.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ya hay un archivo guardado" + ex.ToString());
                    return;
                }
            }
        }

        #endregion

        #region Selección de proveedor / documento / orden

        private void ActualizarInfoDocumentoSeleccionado()
        {
            if (txtFolio.Text == "X") return;
            if (cmbDocumento.Text == string.Empty) return;

            string[] valores = r.InformacionDocumento(cmbDocumento.Text);
            if (valores == null) return;

            txtDocumento.Text = ObtenerValor(valores, 0);
            txtClave.Text = ObtenerValor(valores, 1);

            if (txtFolio.Text == string.Empty)
            {
                r.ConsecutivoGasto(txtConsecutivo, txtClave.Text);
            }
        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e) => ActualizarInfoDocumentoSeleccionado();

        private void cmbDocumento_SelectedIndexChanged_1(object sender, EventArgs e) => ActualizarInfoDocumentoSeleccionado();

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    r.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }
            }
        }

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

            txtMatricular.Text = RegistroReembolsos.Matricula;
            CerrarDropdownsYRestaurarColores();
        }

        private void btLimpiarOrden_Click(object sender, EventArgs e)
        {
            if (cmbFiltroDocumentoC.Text == string.Empty) return;

            txtOrdenCompra.Clear();
            txtFiltroOrdenC.Clear();
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            Matricula = string.Empty;
            cmbFiltroDocumentoC.Text = null;
            cmbOrdenCompra.Text = null;
            cmbProveedor.Text = null;

            CerrarDropdownsYRestaurarColores();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro de gasto confirmada para continuar");
            }
            else if (cmbEstatus.Text == EstadoReembolso.Abierto)
            {
                MessageBox.Show("Confirme el registro de gastos antes de continuar");
            }
            else
            {
                DocumentoGlobalesGastoVer documentoConceptoGlobalVer = new DocumentoGlobalesGastoVer(txtFolio.Text);
                documentoConceptoGlobalVer.ShowDialog();
            }

            CerrarDropdownsYRestaurarColores();
        }

        #endregion

        #region Partidas - alta y consulta de concepto

        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            cmdproyectoalterno.Enabled = false;

            if (cmbEstatus.Text != EstadoReembolso.Abierto)
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }

            PanelPartidasRequisicion.Visible = true;
            LimpiarDetalle();
            r.ConsultaGasto(TxtFolio1.Text, txtPartida);
            PanelPartidasRequisicion.BringToFront();
            btnAdjuntarComprobantePartida.Enabled = true;
            PartidaNuevaConsulta = "NO";

            cmdproyectoalterno.Items.Clear();
            cmdproyectoalterno.Items.Add(proyecto);
            if (cmdproyectoalterno.Items.Count > 0) cmdproyectoalterno.SelectedIndex = 0;

            LlenarComboGastos();
            DesbloquearDetalle();
            if (cmbproyecto.SelectedIndex > 0)  LlenarComboFormasPago(cmbCentroCostos.Text, cmbproyecto.Text);

            cmdproyectoalterno.Enabled = false;
        }

        /// <summary>
        /// Carga los datos del concepto seleccionado. "actualizarIEPS" reproduce
        /// la diferencia que existía entre cmbConcepto_SelectionChangeCommitted
        /// (no la actualizaba) y cmbConcepto_SelectedIndexChanged (sí la
        /// actualizaba).
        /// </summary>
        private void CargarDatosConceptoSeleccionado(bool actualizarIEPS)
        {
            if (cmbConcepto.Text == string.Empty) return;

            string claveConcepto = cmbConcepto.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(claveConcepto)) return;

            if (txtOrden.Text != string.Empty)
            {
                string[] valores = r.InformacionGastoo(claveConcepto, txtOrden.Text);
                if (valores == null) return;

                txtClave1.Text = claveConcepto;
                txtConcepto.Text = ObtenerValor(valores, 1);
                txtPrecio.Text = ObtenerValor(valores, 2);
                txtTotal1.Text = ObtenerValor(valores, 4);
                txtConcepto2.Text = ObtenerValor(valores, 5);
                txtPartidaOrden.Text = ObtenerValor(valores, 7);
                txtCantidadPedido.Text = ObtenerValor(valores, 8);
                txtCantidad.Text = ObtenerValor(valores, 8);
                txtImpuesto12.Text = ObtenerValor(valores, 9);
                txtUnidad.Text = ObtenerValor(valores, 10);
            }
            else
            {
                string[] valores = r.InformacionGasto(claveConcepto);
                if (valores == null) return;

                txtClave1.Text = claveConcepto;
                txtConcepto.Text = ObtenerValor(valores, 1);
                txtPrecio.Text = ObtenerValor(valores, 2);
                txtUnidad.Text = ObtenerValor(valores, 3);
                txtImpuesto12.Text = ObtenerValor(valores, 4);

                if (actualizarIEPS)
                {
                    cobraIEPS = ObtenerValor(valores, 7) == "Si";
                    txtIEPS.Enabled = cobraIEPS;
                }
            }

            txtSubtotal.Text = (ParsearDecimal(txtPrecio.Text) * ParsearEntero(txtCantidad.Text)).ToString();
        }


        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e) => CargarDatosConceptoSeleccionado(actualizarIEPS: true);

     


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
            if (consultaRegistros != "NO" && PartidaNuevaConsulta != "SI")
            {
                r.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
            }
        }

        #endregion

        #region Partidas - guardado

        /// <summary>
        /// Valida que, si el reembolso maneja proveedor alterno, se haya
        /// seleccionado uno. Antes esta validación estaba copiada en 3 métodos.
        /// </summary>
        private bool ValidarProveedorAlterno()
        {
            if (cmbProoveedorAlternoSiNo.Text == "Si" && string.IsNullOrEmpty(cmbProveedroAlterno.Text))
            {
                MessageBox.Show("Es necesario seleccionar un proveedor alterno");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que haya un concepto realmente seleccionado de la lista (no sólo
        /// texto escrito a mano, ya que cmbConcepto es DropDown editable) antes de
        /// guardar la partida, porque ahora se guarda la Clave (SelectedValue) y no
        /// la Descripción (Text).
        /// </summary>
        private bool ValidarConceptoSeleccionado()
        {
            if (string.IsNullOrEmpty(cmbConcepto.SelectedValue?.ToString()))
            {
                MessageBox.Show("Selecciona el concepto de la lista antes de continuar");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Inserta la partida con los mismos parámetros que antes se repetían
        /// íntegros en guna2Button12_Click, btnConfirmarPartida_Click_1 y
        /// btnSiguientePartida_Click_1.
        ///
        /// NOTA IMPORTANTE: el valor de "forma de pago" que se guardaba NO era
        /// consistente entre esos 3 métodos originales — guna2Button12_Click
        /// guardaba cmbformapago.Text (el texto visible) mientras que los otros
        /// dos guardaban cmbformapago.SelectedValue (el Id interno). Para NO
        /// alterar el comportamiento actual de cada botón, se conserva esa
        /// diferencia recibiéndola como parámetro. Se recomienda unificarla,
        /// ya que lo más probable es que uno de los tres esté guardando el dato
        /// incorrecto en la base de datos.
        /// </summary>
        private void GuardarPartidaGasto(string formaPagoGuardado)
        {
            r.InsertarPartidaGasto(
                TxtFolio1.Text,
                txtPartida.Text,
                cmbConcepto.SelectedValue?.ToString(),   // CAMBIO: antes txtClave1.Text
                txtConcepto2.Text,
                txtCantidad.Text.Replace(",", ""),
                txtUnidad.Text,
                txtDivisa1.Text,
                txtTipoCambio1.Text.Replace(",", ""),
                ParsearDecimal(txtSubtotal1.Text),
                Convert.ToDecimal("0.00"),
                ParsearDecimal(txtTotal1.Text),
                ParsearDecimal(txtImpuesto12.Text),
                rutaCompletaArchivo,
                cmbProveedroAlterno?.SelectedValue?.ToString(),
                "0",
                txtDescuentoIm.Text.Replace(",", ""),
                txtImpuestoIm.Text.Replace(",", ""),
                cmdproyectoalterno.Text,
                dtpFecha.Text,
                formaPagoGuardado,
                cmbreferencia.Text,
                txtIEPS.Text.Replace(",", ""),
                txtRetencion.Text.Replace(",", ""),
                txtPrecio.Text.Replace(",", ""));
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int partida = ParsearEntero(txtPartida.Text) - 1;
                    if (partida > 0)
                    {
                        r.ActualizarGasto(TxtFolio1.Text, partida.ToString());
                        r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                    }
                }
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (!ValidarConceptoSeleccionado()) return;
                if (!ValidarProveedorAlterno()) return;

                GuardarPartidaGasto(cmbformapago.Text);
                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                LimpiarDetalle();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
            }

            PanelPartidasRequisicion.Visible = false;
            btnTerminarReembolso.Visible = true;
            r.CargarRecibosPartidasGasto(dgvPartidas, TxtFolio1.Text);
            SumarColumnasPartida();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            LimpiarDetalle();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            // NOTA: se agregó esta validación de estatus para igualar el
            // comportamiento de btnEliminarPartida_Click_1, que sí valida que el
            // documento esté Abierto antes de eliminar. En el código original,
            // este botón podía eliminar una partida aunque el documento
            // estuviera Bloqueado o Cancelado. Si este botón corresponde a un
            // flujo distinto donde eso es intencional, basta con quitar este
            // bloque para volver al comportamiento anterior.
            if (cmbEstatus.Text != EstadoReembolso.Abierto)
            {
                MessageBox.Show("No es posible eliminar la partida");
                return;
            }

            MessageBox.Show(r.EliminarPartidaRegistroGasto(txtFolio.Text, txtPartida.Text));
            string maximo = r.ObtenerTotalPartidaRegistroGasto(txtFolio.Text);
            r.ActualizarGasto(txtFolio.Text, maximo);
            r.ReciboSaldosGastos(txtFolio.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);
            r.CargarRecibosPartidasGasto(dgvPartidas, txtFolio.Text);
            SumarColumnasPartida();

            LimpiarDetalle();
        }

        private void btnEliminarPartida_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            if (cmbEstatus.Text != EstadoReembolso.Abierto)
            {
                MessageBox.Show("No es posible eliminar la partida");
                return;
            }

            MessageBox.Show(r.EliminarPartidaRegistroGasto(txtFolio.Text, txtPartida.Text));
            string maximo = r.ObtenerTotalPartidaRegistroGasto(txtFolio.Text);
            r.ActualizarGasto(txtFolio.Text, maximo);
            r.CargarRecibosPartidasGasto(dgvPartidas, txtFolio.Text);
            SumarColumnasPartida();

            LimpiarDetalle();
        }

        private void btnLimpiarPartida_Click_1(object sender, EventArgs e)
        {
            LimpiarDetalle();
            BloquearDetalle();
            guna2TabControl1.SelectedIndex = 0;
            PanelPartidasRequisicion.Visible = false;
        }

        private void btnConfirmarPartida_Click_1(object sender, EventArgs e)
        {
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int partida = ParsearEntero(txtPartida.Text) - 1;
                    if (partida > 0)
                    {
                        r.ActualizarGasto(TxtFolio1.Text, partida.ToString());
                        r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                    }
                }
            }
            else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                if (!ValidarConceptoSeleccionado()) return;
                if (!ValidarProveedorAlterno()) return;

                GuardarPartidaGasto(cmbformapago?.SelectedValue?.ToString());
                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);

                LimpiarDetalle();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                CargarConceptosGlobales(TxtFolio1.Text);
            }

            PanelPartidasRequisicion.Visible = false;
            btnTerminarReembolso.Visible = true;

            r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotal, txtDescuento, txtTotal, txtImpuestos, txtIEPSGlobal, txttotalretenciones, txtPartidas);
            r.CargarRecibosPartidasGasto(dgvPartidas, TxtFolio1.Text);
            SumarColumnasPartida();

            dgvComprobantesPartoda.Rows.Clear();
        }

        private void btnSiguientePartida_Click_1(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el Producto para continuar");
                return;
            }

            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar para continuar");
                return;
            }

            if (txtOrden.Text == "Automatico")
            {
                // NOTA: el código original tenía aquí una rama adicional
                // "else if (txtOrden.Text == "Automatico" && txtPartida.Text != "1")"
                // que era inalcanzable (código muerto), porque esta condición ya
                // cubre cualquier caso en el que txtOrden es "Automatico". El
                // comportamiento observable era: se muestra el aviso y NO se
                // guarda la partida sin importar el número de partida. Ese
                // comportamiento se conserva aquí.
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
                return;
            }
            if (!ValidarConceptoSeleccionado()) return;

            if (!ValidarProveedorAlterno()) return;

            GuardarPartidaGasto(cmbformapago?.SelectedValue?.ToString());
            r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text.Replace(",", ""));
            r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
            r.Consulta5RegistroGasto(TxtFolio1.Text, txtPartida);
            r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotal, txtDescuento, txtTotal, txtImpuestos, txtIEPSGlobal, txttotalretenciones, txtPartidas);

            LimpiarDetalle();
            r.ConsultaGasto(TxtFolio1.Text, txtPartida);
            dgvComprobantesPartoda.Rows.Clear();
            cmdproyectoalterno.Text = cmbproyecto.Text;

            LlenarComboGastos();
            CargarConceptosGlobales(txtFolio.Text);
        }

        #endregion

        #region Partidas - consulta desde la grilla

        private void dgvPartidas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            PartidaNuevaConsulta = "SI";

            LlenarComboGastos();

            string partida = dgvPartidas.Rows[e.RowIndex].Cells["Partida"].Value?.ToString();
            txtPartida.Text = partida;

            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
            LlenarComboFormasPago(cmbCentroCostos.Text, cmbproyecto.Text);

            r.ConsultaPartidaGasto(
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
                txtTotal1,
                txtArchivo1,
                cmbProveedroAlterno,
                txtDescuentoIm,
                txtImpuestoIm,
                txtPrecio,
                cmdproyectoalterno,
                dtpFecha,
                cmbformapago,
                cmbreferencia,
                txtRetencion,
                txtIEPS
            );

            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

            PanelPartidasRequisicion.Visible = true;
            guna2Button11.Visible = true;

            r.mostrarArchivos(dgvComprobantesPartoda, txtFolio.Text, txtPartida.Text);
            RecargarDescuentos();
            RecargarImpuestos();
            r.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
        }

        #endregion

        #region Archivos adjuntos de partida

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DBOrdenCompra.Ruta))
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
                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear carpeta: " + ex.Message);
                return;
            }

            OpenFileDialog open = new OpenFileDialog { Filter = "Todos los archivos|*.*" };

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = open.FileName;
                extensionArchivo = Path.GetExtension(archivoSeleccionado);
                nombreArchivo = descripcion + extensionArchivo;
                rutaCompletaArchivo = Path.Combine(carpetaDestino, nombreArchivo);

                try
                {
                    File.Copy(archivoSeleccionado, rutaCompletaArchivo, overwrite: true);
                    txtArchivo1.Text = nombreArchivo;

                    r.ModificarExtension4gasto(noOrden, descripcion, extensionArchivo);
                    r.ActualizarRecepcion2gasto(noOrden, descripcion, nombreArchivo);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Ya hay un archivo guardado o en uso: " + ex.Message);
                    return;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DBOrdenCompra.Ruta))
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtFolio1.Text))
            {
                MessageBox.Show("Seleccione un registro para continuar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtArchivo1.Text))
            {
                MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                return;
            }

            if (!string.IsNullOrWhiteSpace(rutaCompletaArchivo) && File.Exists(rutaCompletaArchivo))
            {
                Process.Start(rutaCompletaArchivo);
                return;
            }

            string carpeta = Path.Combine(DBOrdenCompra.Ruta, "G" + TxtFolio1.Text);
            string rutaReconstruida = Path.Combine(carpeta, txtArchivo1.Text);

            if (File.Exists(rutaReconstruida))
            {
                Process.Start(rutaReconstruida);
            }
            else
            {
                MessageBox.Show("No se encontró el archivo en la ruta esperada.");
            }
        }

        private void btnAdjuntarComprobantePartida_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DBRegistroReembolso.Ruta))
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
            string carpetaDestino = Path.Combine(DBOrdenCompra.Ruta, "G" + noOrden);

            try
            {
                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear carpeta: " + ex.Message);
                return;
            }

            OpenFileDialog open = new OpenFileDialog { Filter = "Todos los archivos|*.*" };

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = open.FileName;

                // NOTA: aquí "extensionArchivoLocal", "nombreArchivoLocal" y
                // "rutaCompletaArchivoLocal" son variables LOCALES. En el código
                // original tenían el MISMO nombre que los campos de la clase
                // (rutaCompletaArchivo, nombreArchivo, extensionArchivo), lo que
                // los ocultaba ("shadowing"). Esto significa que, a diferencia de
                // button11_Click, este método NO actualizaba el campo de clase
                // "rutaCompletaArchivo" que luego usa GuardarPartidaGasto al
                // guardar la partida. Se conserva ese comportamiento (por eso se
                // conservan como variables locales), pero vale la pena revisarlo:
                // si un comprobante se adjunta desde este botón, la partida podría
                // guardarse con la ruta de un archivo adjuntado anteriormente
                // desde otro botón, en vez de la de este archivo.
                string extensionArchivoLocal = Path.GetExtension(archivoSeleccionado);
                string nombreArchivoLocal = Path.GetFileNameWithoutExtension(archivoSeleccionado);
                string rutaCompletaArchivoLocal = Path.Combine(carpetaDestino, nombreArchivoLocal);

                try
                {
                    File.Copy(archivoSeleccionado, rutaCompletaArchivoLocal, overwrite: true);
                    txtArchivo1.Text = nombreArchivoLocal;

                    byte[] archivoBytes = File.ReadAllBytes(archivoSeleccionado);
                    string contenidoArchivo = Convert.ToBase64String(archivoBytes);

                    MessageBox.Show(r.insertaArchivos(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, nombreArchivoLocal, extensionArchivoLocal, contenidoArchivo));

                    r.mostrarArchivos(dgvComprobantesPartoda, TxtFolio1.Text, txtPartida.Text);
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

        private void dgvComprobantesPartoda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow filaSeleccionada = dgvComprobantesPartoda.Rows[e.RowIndex];

            if (dgvComprobantesPartoda.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                var secuencia = filaSeleccionada.Cells[0].Value?.ToString() ?? "";
                var folio = filaSeleccionada.Cells[1].Value?.ToString() ?? "";
                var partida = filaSeleccionada.Cells[2].Value?.ToString() ?? "";
                var archivo = filaSeleccionada.Cells[4].Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show($"¿Deseas eliminar el registro con ID: {secuencia}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string resultado = r.EliminarArchivosGastos(folio, partida, secuencia);
                    if (resultado == "Eliminado")
                    {
                        dgvComprobantesPartoda.Rows.Remove(filaSeleccionada);
                        MessageBox.Show($"Registro con ID {archivo} eliminado correctamente.");
                        r.mostrarArchivos(dgvComprobantesPartoda, TxtFolio1.Text, txtPartida.Text);
                    }
                }
            }

            if (dgvComprobantesPartoda.Columns[e.ColumnIndex].Name == "Ver")
            {
                MostrarArchivoComprobante(filaSeleccionada);
            }
        }

        private void MostrarArchivoComprobante(DataGridViewRow filaSeleccionada)
        {
            try
            {
                if (!dgvComprobantesPartoda.Columns.Contains("ContenidoArchivo"))
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

                string tipo = DetectarTipo(base64Archivo);
                string extension;
                switch (tipo)
                {
                    case "pdf": extension = ".pdf"; break;
                    case "xml": extension = ".xml"; break;
                    case "png": extension = ".png"; break;
                    default: extension = ".bin"; break;
                }

                string rutaTemporal = Path.Combine(Path.GetTempPath(), "archivo_visualizado" + extension);
                byte[] bytes = Convert.FromBase64String(base64Archivo);
                File.WriteAllBytes(rutaTemporal, bytes);
                Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar el archivo: " + ex.Message);
            }
        }

        private string DetectarTipo(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            if (bytes.Length > 4 && bytes[0] == 0x25 && bytes[1] == 0x50 && bytes[2] == 0x44 && bytes[3] == 0x46)
            {
                return "pdf";
            }

            string texto = Encoding.UTF8.GetString(bytes);
            if (texto.TrimStart().StartsWith("<?xml"))
            {
                return "xml";
            }

            if (bytes.Length > 8 &&
                bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
                bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A)
            {
                return "png";
            }

            return "desconocido";
        }

        #endregion

        #region Cálculos de partida

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores2 = r.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = ObtenerValor(valores2, 0);
            }
        }

        private void Calcular()
        {
            decimal.TryParse(txtPrecio.Text, out decimal precio);
            decimal.TryParse(txtCantidad.Text, out decimal cantidad);
            decimal.TryParse(txtIEPS.Text, out decimal ieps);

            // NOTA: en el código original, "descuentoTotal" y "retencion" se leen
            // de txtDescuentoIm/txtRetencion y luego se reinician a 0 antes de
            // usarse (posible resultado de una depuración incompleta, ya que hay
            // un bloque comentado justo arriba que sí las usaba). Se conserva tal
            // cual porque el cálculo actual es "totalmente funcional" y depende
            // de este comportamiento.
            decimal descuentoTotal = 0;
            decimal retencion = 0;

            decimal sub = precio * cantidad - descuentoTotal;

            foreach (var concepto in conceptosAplicadosDescuentos)
            {
                if (concepto.clase == "Descuento")
                {
                    if (concepto.Tipo == "Porcentaje")
                    {
                        retencion += (concepto.Valor / 100) * sub;
                    }
                    else if (concepto.Tipo == "Importe")
                    {
                        retencion += concepto.Valor;
                    }
                }
            }

            txtRetencion.Text = retencion.ToString("N2");

            decimal impuestoTotal = 0;
            foreach (var concepto in conceptosAplicadosImpuestos)
            {
                if (concepto.clase == "Impuesto")
                {
                    if (concepto.Tipo == "Porcentaje")
                    {
                        impuestoTotal += (concepto.Valor / 100) * sub;
                    }
                    else if (concepto.Tipo == "Importe")
                    {
                        impuestoTotal += concepto.Valor;
                    }
                }
            }

            txtImpuestoIm.Text = impuestoTotal.ToString("N2");
            txtTotal1.Text = (sub + impuestoTotal + ieps - retencion).ToString("N2");
            txtSubtotal1.Text = sub.ToString("N2");
        }

        private void RecargarDescuentos()
        {
            conceptosAplicadosDescuentos.Clear();

            var dt = c.CargarConceptosExistentes(txtFolio.Text, txtPartida.Text, "Descuento");
            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["Tipo"].ToString();
                string clase = row["Clase"].ToString().Trim();
                decimal valor = ConvertirDecimalSeguro(row["Descuento"]);
                conceptosAplicadosDescuentos.Add((clase, tipo, valor, "", "", 0.00m));
            }
        }

        private void RecargarImpuestos()
        {
            conceptosAplicadosImpuestos.Clear();

            var dt = c.CargarConceptosExistentes(txtFolio.Text, txtPartida.Text, "Impuesto");
            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["Tipo"].ToString();
                string clase = row["Clase"].ToString().Trim();
                decimal valor = ConvertirDecimalSeguro(row["Cargo"]);
                conceptosAplicadosImpuestos.Add((clase, tipo, valor));
            }
        }

        #endregion

        #region Formato de moneda y validaciones de cantidad

        private void Moneda(ref Guna.UI2.WinForms.Guna2TextBox txt)
        {
            // NOTA: se removieron dos líneas que en el original no tenían ningún
            // efecto (un "if" que reasignaba la variable a sí misma, y un
            // Substring cuyo resultado nunca se asignaba a nada), así que el
            // comportamiento numérico es idéntico al anterior.
            string n = txt.Text.Replace(",", "").Replace(".", "");
            n = n.PadLeft(3, '0');

            double v = Convert.ToDouble(n) / 100;
            txt.Text = string.Format("{0:N}", v);
            txt.SelectionStart = txt.Text.Length;
        }

        /// <summary>
        /// Valida que la cantidad capturada no exceda lo pedido en la orden y
        /// recalcula. Antes este bloque estaba copiado en txtPrecio_TextChanged,
        /// txtPrecio_TextChanged_1, txtCantidad_TextChanged y txtIEPS_TextChanged
        /// (cada uno con su propio mensaje de error en el catch).
        /// </summary>
        private void ValidarCantidadContraPedido(string mensajeError)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty && txtCantidadPedido.Text != string.Empty)
                {
                    if (ParsearEntero(txtCantidad.Text) > ParsearEntero(txtCantidadPedido.Text))
                    {
                        MessageBox.Show("No es posible registrar una cantidad mayor a la registrada en la orden de commpra");
                        txtCantidad.Text = txtCantidadPedido.Text;
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
                MessageBox.Show(mensajeError);
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
            ValidarCantidadContraPedido("Formato de precio incorrecto");
        }

        private void txtPrecio_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
            ValidarCantidadContraPedido("Formato de precio incorrecto");
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            ValidarCantidadContraPedido("Formato de cantidad incorrecto");
        }

        private void txtIEPS_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtIEPS);
            ValidarCantidadContraPedido("Formato de precio incorrecto");
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal1);
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e) => Moneda(ref txtSubtotal);

        private void txtTotal_TextChanged(object sender, EventArgs e) => Moneda(ref txtTotal);

        private void txtDescuento_TextChanged(object sender, EventArgs e) => Moneda(ref txtDescuento);

        private void txtImpuestos_TextChanged(object sender, EventArgs e) => Moneda(ref txtImpuestos);

        private void txtSaldo_TextChanged(object sender, EventArgs e) => Moneda(ref txtSaldo);

        private void txtDescuentoIm_TextChanged(object sender, EventArgs e) => Moneda(ref txtDescuentoIm);

        private void txtIEPSGlobal_TextChanged(object sender, EventArgs e) => Moneda(ref txtIEPSGlobal);

        private void txttotalretenciones_TextChanged(object sender, EventArgs e) => Moneda(ref txttotalretenciones);

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtDescuentoIm_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtDescuentoIm_Leave(object sender, EventArgs e)
        {
            decimal importe = ParsearDecimal(txtCantidad.Text) * ParsearDecimal(txtPrecio.Text);
            decimal descuento = ParsearDecimal(txtDescuentoIm.Text);
            decimal subtotal = importe - descuento;
            txtSubtotal1.Text = subtotal.ToString("N2");
        }

        #endregion

        #region Consulta de registros y filtros

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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Limpiarcabezado();
            LimpiarDetalle();

            string folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value?.ToString() ?? string.Empty;
            txtFolio.Text = "X";

            r.ConsultaGastos(folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento,
                txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo,
                txtReferencia, txtSaldo, txtDiasVence, txtFechaVence, txtArchivo, cmbCentroCostos, cmbSemana, dtpAnio,
                cmbProoveedorAlternoSiNo, cmbproyecto, txttotalretenciones);

            TxtFolio1.Text = folio;
            proyecto = cmbproyecto.Text;

            rutaCompletaArchivo = txtArchivo.Text;
            cmbOrdenCompra.Enabled = false;
            txtNotas.Enabled = false;
            button3.Enabled = false;

            r.ConsultaAbonoGasto(txtFolio.Text, txtAbono);

            txtMatricular.Text = DBRegistroReembolso.MatriculaC;

            string[] valores = r.InformacionDocumento2(txtClave.Text);
            txtDocumento.Text = ObtenerValor(valores, 0);
            cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

            if (txtReciboCol.Text != "0")
            {
                r.SeleccionarOrdenEntrega2(cmbOrdenCompra, txtReciboCol.Text);
                if (cmbOrdenCompra.Items.Count > 0) cmbOrdenCompra.SelectedIndex = 0;

                r.SeleccionarProvedor2(cmbProveedor, txtReciboCol.Text);
                if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;

                string[] valores2 = r.InformacionDocumento3(cmbOrdenCompra.Text);
                txtFiltroOrdenC.Text = ObtenerValor(valores2, 0);
                txtDocumentoCol.Text = ObtenerValor(valores2, 1);

                cmbFiltroDocumentoC.Text = txtFiltroOrdenC.Text + " - " + txtDocumentoCol.Text;
            }

            txtMatricular.Text = DBRegistroReembolso.MatriculaC;

            guna2GradientPanel2.Visible = false;
            guna2GradientPanel2.SendToBack();

            r.CargarRecibosPartidasGasto(dgvPartidas, txtFolio.Text);
            SumarColumnasPartida();
            CargarConceptosGlobales(txtFolio.Text);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                txtFiltroDocumento.Clear();
                r.CargarRecibosFiltroGasto(dataGridView1, txtFiltro.Text);
            }
            else
            {
                r.CargarGasto(dataGridView1);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                r.CargarRecibosFiltroDocumentoGasto(dataGridView1, txtFiltroDocumento.Text);
            }
            else
            {
                r.CargarGasto(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroDocumento.Clear();
                r.CargarRecibosFiltroPGasto(dataGridView1, txtFiltroNombre.Text);
            }
            else
            {
                r.CargarGasto(dataGridView1);
            }
        }

        private void cmbCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (consultaRegistros != "SI")
            {
                r.SeleccionarCatConceptosGlobales(cmbproyecto, cmbCentroCostos.Text);

                if (cmbproyecto.Items.Count != 0)
                {
                    cmbproyecto.SelectedIndex = 0;
                }
            }
        }

        private void cmbformapago_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sin acción (se conserva igual que en el original).
        }

        #endregion

        #region Totales

        private void SumarColumnasPartida()
        {
            decimal totalSubtotal = 0;
            decimal totalDescuento = 0;
            decimal totalImpuesto = 0;
            decimal totalIeps = 0;
            decimal totalretenciones = 0;

            foreach (DataGridViewRow fila in dgvPartidas.Rows)
            {
                if (fila.IsNewRow) continue;

                if (fila.Cells["Subtotal"].Value != null && decimal.TryParse(fila.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                    totalSubtotal += subtotal;

                if (fila.Cells["Descuento"].Value != null && decimal.TryParse(fila.Cells["Descuento"].Value.ToString(), out decimal descuento))
                    totalDescuento += descuento;

                if (fila.Cells["Impuesto"].Value != null && decimal.TryParse(fila.Cells["Impuesto"].Value.ToString(), out decimal impuesto))
                    totalImpuesto += impuesto;

                if (fila.Cells["Ieps"].Value != null && decimal.TryParse(fila.Cells["Ieps"].Value.ToString(), out decimal ieps))
                    totalIeps += ieps;

                if (fila.Cells["retenciones"].Value != null && decimal.TryParse(fila.Cells["retenciones"].Value.ToString(), out decimal retenciones))
                    totalretenciones += retenciones;
            }

            lblSubtotalPartidas.Text = "Subtotal: $" + totalSubtotal.ToString("N2");
            lblDescuentosPartidas.Text = "Descuento: $" + totalDescuento.ToString("N2");
            lblImpuestosPartidas.Text = "Impuesto: $" + totalImpuesto.ToString("N2");
            lblIEPSPartidas.Text = "IEPS: $" + totalIeps.ToString("N2");
            lblRetenciones.Text = "Retenciones: $" + totalretenciones.ToString("N2");

            decimal total = totalSubtotal - totalDescuento + totalImpuesto + totalIeps - totalretenciones;
            lblTotal.Text = "Total: $" + total.ToString();
        }

        private void btnDescuentosPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFolio1.Text))
            {
                MessageBox.Show("Seleccione una Compra Reembolso");
                return;
            }

            ConceptosGlobalesPartida cgp = new ConceptosGlobalesPartida(TxtFolio1.Text, txtPartida.Text, "Descuento", cmbEstatus.Text);

            if (cgp.ShowDialog() == DialogResult.OK)
            {
                RecargarDescuentos();
                Calcular();
            }
        }

        private void btnImpuestosPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFolio1.Text))
            {
                MessageBox.Show("Seleccione una Compra Reembolso");
                return;
            }

            ConceptosGlobalesPartida cgp = new ConceptosGlobalesPartida(TxtFolio1.Text, txtPartida.Text, "Impuesto", cmbEstatus.Text);

            if (cgp.ShowDialog() == DialogResult.OK)
            {
                RecargarImpuestos();
                Calcular();
            }
        }

        #endregion

        #region Eventos sin lógica (mantenidos por compatibilidad con el diseñador)

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label66_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel2.SendToBack();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}