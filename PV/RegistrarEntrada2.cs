using PuntoVentas;
using PuntoVentas.Clases.Login;
using PV.Clases.Inventario;
using PV.Clases.TipoMovimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarEntrada2 : Form
    {
        // ==========================================================
        // Constantes - se centralizan los "strings mágicos" que se
        // repetían por todo el formulario para evitar errores de
        // tipeo y facilitar cambios futuros.
        // ==========================================================
        private const string TIPO_ENTRADA = "E";
        private const string TIPO_SALIDA = "S";
        private const string TIPO_TRASPASO = "T";

        private const string ESTATUS_ACTIVO = "Activo";
        private const string ESTATUS_CANCELADO = "Cancelado";
        private const string ESTATUS_BLOQUEADO = "Bloqueado";

        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBTipoMovimiento s = new DBTipoMovimiento();
        public static int Bloqueo = 0;
        public static int Opcion = 0;

        string Documento = string.Empty;

        // Indica si el panel de partida está en modo "editar una partida ya
        // guardada" (true) o "agregar una partida nueva" (false). Se usa
        // para que btnConfirmarPartida_Click sepa si debe INSERTAR o
        // ACTUALIZAR, y para cambiar el texto del botón en consecuencia.
        private bool editandoPartidaExistente = false;

        DBPartidas c1 = new DBPartidas();

        // NOTA: se cambian de double a decimal porque son montos monetarios.
        // "double" puede introducir errores de redondeo binario en operaciones
        // acumulativas (Total = Total + ...). Si algún otro formulario o clase
        // de datos referencia estos campos estáticos, deberá actualizarse para
        // usar "decimal" también.
        public static decimal Subtotal = 0.00m;
        public static decimal Descuento = 0.00m;
        public static decimal Impuesto = 0.00m;
        public static decimal Total = 0.00m;
        public static int Partida1 = 0;

        string UltimoFolio;
        string Descripcion;
        string Descripcio2n;
        string TipoDocumento;
        string Divisa;
        string Almacen;
        string Costeo;
        string AlmacenSalida;

        public RegistrarEntrada2(string TipoDocumento)
        {
            InitializeComponent();
            Documento = TipoDocumento;

            ToolTip T = new ToolTip();
            /*   T.SetToolTip(button7, "Nuevo");
               T.SetToolTip(guna2Button1, "Productos / Servicios");
               T.SetToolTip(guna2CircleButton1, "Menú Principal");
               T.SetToolTip(guna2Button2, "Bloqueo / Desbloqueo");
               T.SetToolTip(guna2Button3, "Bloquear");
               T.SetToolTip(guna2Button4, "Desbloquear");
               T.SetToolTip(guna2Button3, "Bloquear");
              
               T.SetToolTip(btnarticulos, "Registrar Articulos");
               T.SetToolTip(button1, "Cancelar Registro");*/

            T.SetToolTip(btnIniciarMovimiento, "Confirmar Registro");
        }

        private void btnAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferencia.Text))
            {
                MessageBox.Show("Registre la referencia para continuar");
                return;
            }

            if (string.IsNullOrEmpty(txtFolioRegistrar.Text))
            {
                MessageBox.Show("Seleccione el documente y confirme el registro para continuar");
                return;
            }

            if (string.IsNullOrEmpty(cmbDivisa.Text))
            {
                MessageBox.Show("Registre la divisa para continuar");
                return;
            }

            if (string.IsNullOrEmpty(cmbAlmacen.Text))
            {
                MessageBox.Show("Registre el almacen para continuar");
                return;
            }

            if (txtTipoDocumento.Text == TIPO_TRASPASO && string.IsNullOrEmpty(cmbAlmacenSalida.Text))
            {
                MessageBox.Show("Registre el almacen de entrada para continuar");
                return;
            }

            if (txtTipoDocumento.Text == TIPO_TRASPASO && cmbAlmacenSalida.Text == cmbAlmacen.Text)
            {
                MessageBox.Show("El almacen de Salida y Entrada deben ser diferentes");
                return;
            }

            if (string.IsNullOrEmpty(txtFolioP.Text))
            {
                c.RegistroMovimientoInventario(txtFolioRegistrar.Text, txtTipoDocumento.Text, cmbDescripcion.Text, dtpFecha.Text, cmbEstatus.Text, txtReferencia.Text, txtAlmacen.Text, txtTotalPartidas.Text, cmbDivisa.Text, txtTipoCambio.Text, txtTotal.Text, txtNotas.Text, txtElaborado.Text, txtFolioP, txtAlmacenSalida.Text);
                c.RegistroMovimiento(txtTipoDocumento.Text, cmbDescripcion.Text, txtFolioRegistrar.Text);
            }

            UltimoFolio = txtFolioP.Text;
            Descripcion = cmbDescripcion.Text;
            Descripcio2n = txtDescripcion.Text;
            TipoDocumento = txtTipoDocumento.Text;
            Divisa = cmbDivisa.Text;

            if (!TryParseInt(txtTotalPartidas.Text, out Partida1))
            {
                Partida1 = 0;
            }

            Almacen = txtAlmacen.Text;
            Costeo = txtCosteo.Text;
            AlmacenSalida = txtAlmacenSalida.Text;

            c.SeleccionarDivisa(cmbDivisa1);
            c.SeleccionarProducto(cmbProducto);

            txtTipoDocumento1.Text = TipoDocumento;
            cmbDescripcion1.Text = Descripcion;
            txtDescripcion1.Text = Descripcio2n;
            txtUltimoFolio1.Text = UltimoFolio;
            cmbDivisa1.Text = Divisa;
            Subtotal = 0.00m;
            Descuento = 0.00m;
            Impuesto = 0.00m;
            Total = 0.00m;
            txtTotal1.Clear();

            // CORRECCIÓN (sincronización con guna2DataGridView1): antes se
            // calculaba el siguiente número de partida a partir de un
            // contador en memoria (Partida1) que no reflejaba la realidad
            // tras eliminar partidas o tras abrir/cerrar el panel sin
            // confirmar. Ahora siempre se calcula leyendo la grilla, que es
            // la fuente de verdad.
            Partida1 = ObtenerSiguienteNumeroPartida();
            txtNoPartida.Text = Partida1.ToString();

            if (cmbDescripcion.Text == TIPO_SALIDA || cmbDescripcion.Text == TIPO_TRASPASO)
            {
                txtPrecio.Enabled = false;
            }

            // CORRECCIÓN: antes, al llegar aquí desde "Nuevo Documento" justo
            // después de haber consultado una partida de OTRO documento ya
            // bloqueado, el panel de captura y sus botones (Confirmar,
            // Siguiente) seguían deshabilitados -- porque solo
            // btnAgregarPartida_Click (el botón "+") los habilitaba, y nada
            // garantizaba que el usuario pasara por ahí antes de intentar
            // capturar la primera partida. Ahora este botón deja el panel
            // listo para capturar de inmediato.
            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();
            btnCerrarPartida.Enabled = true;
            btnConfirmarPartida.Enabled = true;
            btnSiguientePartida.Enabled = true;
            btnEliminarPartida.Enabled = false;
            txtAlias.Enabled = true;
            cmbDivisa1.Enabled = true;
            editandoPartidaExistente = false;
            btnConfirmarPartida.Text = "Confirmar Partida";

            guna2TabControl1.SelectedIndex = 1;
        }

        private void btnCancelarMovimiento_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("¿Desea cancelar el movimiento " + txtTipoDocumento.Text + "-" + txtFolioRegistrar.Text + "?", "Movimientos Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                return;
            }

            if (cmbEstatus.Text == ESTATUS_CANCELADO)
            {
                MessageBox.Show("No se puede cancelar un documento dos veces");
                return;
            }

            c.CancelarMovimientoInventario(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text);
            MessageBox.Show("REGISTRO CANCELADO");
            Limpiar();
            DesbloquearEncabezado();
        }

        private void cmbDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Documento == TIPO_ENTRADA)
            {
                string[] valores = c.InformacionEntrada(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
                txtCosteo.Text = valores[2];
            }
            else if (Documento == TIPO_SALIDA)
            {
                string[] valores = c.InformacionSalida(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
            }
            else if (Documento == TIPO_TRASPASO)
            {
                string[] valores = c.InformacionTraspaso(cmbDescripcion.Text);
                txtDescripcion.Text = valores[0];
                txtUltimoFolio.Text = valores[1];
            }
        }

        private void btnIniciarMovimiento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUltimoFolio.Text))
            {
                MessageBox.Show("Seleccione el documento de entrada para continuar");
                return;
            }

            if (!TryParseInt(txtUltimoFolio.Text, out int ultimoFolio))
            {
                MessageBox.Show("El folio existente tiene un formato incorrecto");
                return;
            }

            ultimoFolio += 1;
            txtFolioRegistrar.Text = ultimoFolio.ToString();
            cmbEstatus.Text = ESTATUS_ACTIVO;

            dtpFecha.Text = DateTime.Today.ToString();

            cmbDivisa.Text = "MXN";
            txtTotalPartidas.Text = "0";
            txtTotal.Text = "0.00";

            txtElaborado.Text = DBLogin.usuario;
     
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacen.Text = valores[0];
            }
        }

        private void cmbAlmacenSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacenSalida.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacenSalida.Text);
                txtAlmacenSalida.Text = valores[0];
            }
        }

        private void cmbDivisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivisa.Enabled == true)
            {
                string[] valores = c.InformacionDivisa(cmbDivisa.Text);
                txtTipoCambio.Text = valores[0];
            }
        }

        /// <summary>
        /// Carga el combo/documento y la grilla correspondiente según el tipo
        /// de movimiento del formulario (E/S/T). Antes este bloque estaba
        /// duplicado de forma idéntica en Limpiar() y en RegistrarEntrada2_Load.
        /// </summary>
        private void CargarInformacionPorTipoDocumento()
        {
            if (Documento == TIPO_ENTRADA)
            {
                c.SeleccionarDocumentoEntrada(cmbDescripcion);
                txtTipoDocumento.Text = TIPO_ENTRADA;
                c.CargarEntrada(dataGridView1);
            }
            else if (Documento == TIPO_SALIDA)
            {
                c.SeleccionarDocumentoSalida(cmbDescripcion);
                txtTipoDocumento.Text = TIPO_SALIDA;
                c.CargarSalida(dataGridView1);
            }
            else if (Documento == TIPO_TRASPASO)
            {
                c.SeleccionarDocumentoTraslado(cmbDescripcion);
                txtTipoDocumento.Text = TIPO_TRASPASO;
                lbSalida.Visible = true;
                lbEntrada.Visible = true;
                cmbAlmacenSalida.Visible = true;
                c.CargarTraspaso(dataGridView1);
            }

            c.SeleccionarDivisa(cmbDivisa);
        }

        private void Limpiar()
        {
            CargarInformacionPorTipoDocumento();

            txtUltimoFolio.Clear();
            txtReferencia.Clear();
            txtFolioRegistrar.Text = string.Empty;

            cmbAlmacen.SelectedIndex = -1;
            txtTotalPartidas.Text = "0";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            txtElaborado.Clear();
            txtDescripcion.Clear();
            txtAlmacen.Clear();
            txtAlmacenSalida.Clear();
            txtCosteo.Clear();
            txtAlmacenSalida.Clear();
            txtFolioP.Clear();
            cmbAlmacenSalida.SelectedIndex = -1;
            guna2DataGridView1.Rows.Clear();
            cmbEstatus.Text = string.Empty;
            // CORRECCIÓN: Limpiar() nunca reseteaba txtNoPartida ni Partida1,
            // así que podían quedar arrastrados de un documento visto
            // anteriormente (por ejemplo, la partida "2" de un documento ya
            // bloqueado) y aparecer incorrectamente en un documento nuevo.
            txtNoPartida.Clear();
            cmbEstatus.
                Text = ESTATUS_ACTIVO;
            Partida1 = 0;
            // groupBox2.Enabled = false;
            MovimientosInventario.Subtotal = 0.00;
            MovimientosInventario.Descuento = 0.00;
            MovimientosInventario.Impuesto = 0.00;
            MovimientosInventario.Total = 0.00;
            MovimientosInventario.Partida = 0;
        }

        private void LimpiarDetalle()
        {
            c.SeleccionarProducto(cmbProducto);
            txtExistencias.Clear();
            txtAlias.Clear();
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtUnidad.Clear();
            // CORRECCIÓN: esto limpiaba "txtTotal" (el total del ENCABEZADO)
            // en vez de "txtTotal1" (el total de la PARTIDA actual). Antes
            // era inofensivo porque BLoqueo() volvía a pisar el valor al
            // final, pero rompía cualquier intento de mostrar el total del
            // encabezado sincronizado en tiempo real.
            txtTotal1.Text = "0.00";
            txtExAlmacen.Clear();
            txtConcepto.Clear();
        }

        /// <summary>
        /// Habilita los campos de encabezado. (Antes existían dos métodos
        /// idénticos: Desbloquear() y DesbloquearEncabezado(); se dejó uno solo).
        /// </summary>
        private void DesbloquearEncabezado()
        {
            cmbDescripcion.Enabled = true;
            txtReferencia.Enabled = true;
            cmbAlmacen.Enabled = true;
            cmbDivisa.Enabled = true;
            txtNotas.Enabled = true;
            btnAgregarPartidas.Enabled = true;
            //cmbEstatus.Enabled = true;
            btnIniciarMovimiento.Enabled = true;
        }

        private void BloquearEncabezado()
        {
            cmbDescripcion.Enabled = false;
            txtReferencia.Enabled = false;
            cmbAlmacen.Enabled = false;
            cmbDivisa.Enabled = false;
            txtNotas.Enabled = false;
            btnAgregarPartidas.Enabled = false;
            //cmbEstatus.Enabled = false;
            btnIniciarMovimiento.Enabled = false;
        }

        private void BloquearDetalle()
        {
            cmbProducto.Enabled = false;
            txtConcepto.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            btnAgregarPartida.Enabled = false;
        }

        private void DesbloquearDetalle()
        {
            cmbProducto.Enabled = true;
            txtConcepto.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            // CORRECCIÓN: BloquearDetalle() también deshabilita btnAgregarPartida,
            // pero este método (su contraparte) nunca lo volvía a habilitar. Esto
            // causaba que, tras consultar un documento ya bloqueado, el botón
            // quedara deshabilitado para siempre, incluso al crear un documento
            // nuevo con estatus Activo.
            btnAgregarPartida.Enabled = true;
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (!DocumentoPermiteEdicionDePartidas())
            {
                MessageBox.Show("Solo se pueden agregar partidas cuando el documento está activo.");
                return;
            }

            if (!ValidarDatosPartida("Producto debe tener precio"))
            {
                return;
            }

            if (ExcedeExistenciaAlmacen(cmbDescripcion.Text))
            {
                return;
            }

            if (!ObtenerClaveProducto(out string clave))
            {
                return;
            }

            if (!TryParseDecimal(txtPrecio.Text, out decimal precio) ||
                !TryParseDecimal(txtTotal1.Text, out decimal totalPartida))
            {
                MessageBox.Show("Formato de precio o total incorrecto");
                return;
            }

            // Registrar partida sin procesar inventarios (documento está en estado "Activo")
            c1.RegistroPartida(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, precio, cmbDivisa1.Text, txtTipoCambio1.Text, totalPartida, txtConcepto.Text);

            // Se recalcula el total del encabezado consultando la BD (fuente
            // de verdad) en vez de acumular en memoria, para que nunca se
            // desincronice sin importar qué operación se haga después.
            RecalcularTotalesEncabezado();

            // CORRECCIÓN (sincronización con guna2DataGridView1): antes esta
            // grilla no se refrescaba al usar "Siguiente" (a diferencia de
            // "Confirmar", que sí lo hacía). Sin refrescarla, calcular el
            // siguiente número de partida desde la grilla habría dado un
            // valor desactualizado. Ahora se refresca aquí también.
            c.CargarPartida(guna2DataGridView1, txtTipoDocumento.Text, cmbDescripcion.Text, txtFolioP.Text);

            // NO procesar inventarios aquí - se hará cuando se termine el documento

            LimpiarDetalle();

            // CORRECCIÓN: antes se calculaba el siguiente número de partida
            // a partir de "txtNoPartida"/"Partida1" en memoria, lo cual se
            // desincronizaba con la realidad. Ahora se calcula leyendo
            // guna2DataGridView1 (la fuente de verdad), igual que en el resto
            // del formulario.
            Partida1 = ObtenerSiguienteNumeroPartida();
            txtNoPartida.Text = Partida1.ToString();
        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (!DocumentoPermiteEdicionDePartidas())
            {
                MessageBox.Show(editandoPartidaExistente
                    ? "Solo se pueden actualizar partidas cuando el documento está activo."
                    : "Solo se pueden agregar partidas cuando el documento está activo.");
                return;
            }

            if (!ValidarDatosPartida("Registre el precio para continuar"))
            {
                return;
            }

            if (ExcedeExistenciaAlmacen(Documento))
            {
                return;
            }

            if (!ObtenerClaveProducto(out string clave))
            {
                return;
            }

            if (!TryParseDecimal(txtPrecio.Text, out decimal precio) ||
                !TryParseDecimal(txtTotal1.Text, out decimal totalPartida))
            {
                MessageBox.Show("Formato de precio o total incorrecto");
                return;
            }

            if (editandoPartidaExistente)
            {
                // Actualizar una partida ya guardada (documento está en estado "Activo")
                c1.ActualizarPartida(txtFolioP.Text, Documento, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, precio, cmbDivisa1.Text, txtTipoCambio1.Text, totalPartida, txtConcepto.Text);
            }
            else
            {
                // Registrar partida nueva sin procesar inventarios (documento está en estado "Activo")
                c1.RegistroPartida(txtFolioP.Text, Documento, cmbDescripcion.Text, txtNoPartida.Text, clave, txtCantidad.Text, txtUnidad.Text, precio, cmbDivisa1.Text, txtTipoCambio1.Text, totalPartida, txtConcepto.Text);
            }

            // Se recalcula el total del encabezado consultando la BD (fuente
            // de verdad) en vez de acumular en memoria, para que nunca se
            // desincronice sin importar qué operación se haga después.
            RecalcularTotalesEncabezado();

            // NO procesar inventarios aquí - se hará cuando se termine el documento

            PanelPartidasRequisicion.Visible = false;

            Descripcion = cmbDescripcion.Text;

            c.CargarPartida(guna2DataGridView1, Documento, Descripcion, txtFolioP.Text);
            LimpiarDetalle();

            // Se vuelve al modo "agregar nueva" por defecto y se restaura el
            // texto del botón, para que la próxima vez que se abra el panel
            // (vía btnAgregarPartida_Click) no quede la etiqueta de "Actualizar".
            editandoPartidaExistente = false;
            btnConfirmarPartida.Text = "Confirmar Partida";

            btnTerminarDocumento.Visible = true;
            btnTerminarDocumento.Enabled = true;
        }

        private void btnCerrarPartida_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;

            // Se resetea el modo por si se estaba editando una partida y se
            // cierra el panel sin confirmar; evita que quede la etiqueta
            // "Actualizar Partida" pegada la próxima vez que se abra.
            editandoPartidaExistente = false;
            btnConfirmarPartida.Text = "Confirmar Partida";
            // this.Close();
        }

        private void RegistrarEntrada2_Load(object sender, EventArgs e)
        {
            CargarInformacionPorTipoDocumento();

            c.SeleccionarAlmacen(cmbAlmacen);
            c.SeleccionarAlmacen(cmbAlmacenSalida);
            Bloqueo = 0;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            if (!DocumentoPermiteEdicionDePartidas())
            {
                MessageBox.Show("Solo se pueden agregar partidas cuando el documento está activo.");
                return;
            }

            guna2Button11.Visible = false;
            LimpiarDetalle();
            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();

            // CORRECCIÓN (sincronización con guna2DataGridView1): antes se
            // calculaba a partir de "Partida1" en memoria, que se
            // incrementaba especulativamente cada vez que se abría este
            // panel, sin importar si el usuario realmente confirmaba una
            // partida. Esto causaba dos desincronizaciones reportadas:
            // (1) al eliminar una partida el número no bajaba, y (2) al
            // abrir el panel y darle "Cerrar" sin agregar nada, el número
            // ya había subido de todos modos y volvía a subir en el
            // siguiente intento. Ahora siempre se calcula leyendo la
            // grilla real.
            Partida1 = ObtenerSiguienteNumeroPartida();
            txtNoPartida.Text = Partida1.ToString();

            btnCerrarPartida.Enabled = true;
            btnConfirmarPartida.Enabled = true;
            btnSiguientePartida.Enabled = true;
            btnEliminarPartida.Enabled = false;
            c.SeleccionarProducto(cmbProducto);

            // CORRECCIÓN: al consultar/editar una partida existente
            // (guna2DataGridView1_CellDoubleClick) se deshabilitan txtAlias y
            // cmbDivisa1, pero nunca se volvían a habilitar. Este es el punto
            // natural para revertirlo, ya que aquí se empieza una partida
            // realmente nueva (no una edición).
            txtAlias.Enabled = true;
            cmbDivisa1.Enabled = true;

            // Modo "agregar nueva partida": btnConfirmarPartida insertará.
            editandoPartidaExistente = false;
            btnConfirmarPartida.Text = "Confirmar Partida";
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ObtenerClaveProducto(out string clave))
            {
                return;
            }

            string[] valores = c1.InformacionProducto(clave);
            txtExistencias.Text = valores[0];
            txtAlias.Text = valores[1];
            txtUnidad.Text = valores[2];
            txtCantidad.Text = "1";
            txtPrecio.Text = valores[3];
            txtTipoCosteo.Text = valores[4];
            cmbDivisa1.Text = valores[5];

            string[] valores2 = c1.InformacionProductoAlmacen(clave, Almacen);

            if (DBPartidas.Cantidad == 1)
            {
                txtExAlmacen.Text = valores2[0];
                DBPartidas.Cantidad = 0;
            }
        }

        private void Calcular()
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                return;
            }

            if (!TryParseDecimal(txtCantidad.Text, out decimal cantidad) ||
                !TryParseDecimal(txtPrecio.Text, out decimal precio))
            {
                throw new FormatException("Formato de cantidad o precio incorrecto");
            }

            txtTotal1.Text = (cantidad * precio).ToString();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de cantidad incorrecto");
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c1.Monto(e);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);

            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            c1.Monto(e);
        }

        /// <summary>
        /// Formatea un textbox de dinero mientras el usuario escribe.
        /// CORRECCIÓN: la versión original tenía "n.Substring(1, n.Length - 1);"
        /// sin asignar el resultado, por lo que el cero a la izquierda nunca se
        /// quitaba realmente. También se usa "decimal" en vez de "double" para
        /// evitar errores de precisión en montos, y ya no puede lanzar una
        /// excepción no controlada (antes el try/catch interno solo hacía
        /// "throw;", lo cual no protegía a quien la llama).
        /// </summary>
        private void Moneda(ref Guna.UI2.WinForms.Guna2TextBox txt)
        {
            string digitos = txt.Text.Replace(",", string.Empty).Replace(".", string.Empty);
            digitos = digitos.PadLeft(3, '0');

            while (digitos.Length > 3 && digitos.Substring(0, 1) == "0")
            {
                digitos = digitos.Substring(1, digitos.Length - 1);
            }

            if (!decimal.TryParse(digitos, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal valor))
            {
                return;
            }

            valor /= 100;
            txt.Text = string.Format("{0:N}", valor);
            txt.SelectionStart = txt.Text.Length;
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void cmbDivisa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] valores = c.InformacionDivisa(cmbDivisa1.Text);
            txtTipoCambio1.Text = valores[0];
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            // CORRECCIÓN: la versión original formateaba "txtTotal" (otro
            // control) en vez de "txtTotal1", que es el que realmente
            // disparó el evento. Por favor confirma que este era el
            // comportamiento deseado antes de aceptar este cambio.
            Moneda(ref txtTotal1);
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string NoPartida = guna2DataGridView1.Rows[e.RowIndex].Cells["NoPartida"].Value.ToString();
            string cantidad = string.Empty;
            cmbProducto.SelectedIndexChanged -= cmbProducto_SelectedIndexChanged;
            c1.ConsultarPartida(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, NoPartida, txtclave, txtCantidad, txtUnidad, txtPrecio, lblDivisa1, txtTipoCambio1, txtTotal1, txtConcepto);
            cantidad = txtCantidad.Text;
            cmbDivisa1.Items.Add(lblDivisa1.Text);
            PanelPartidasRequisicion.Visible = true;
            guna2Button11.Visible = true;
            txtNoPartida.Text = NoPartida;

            if (txtclave.Text != string.Empty || txtclave.Text != "")
            {
                cmbProducto.Items.Clear();
                c1.SeleccionarProducto3(cmbProducto, txtclave.Text);
                cmbProducto.SelectedIndex = 0;
            }
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;

            txtCantidad.Text = cantidad;
            btnCerrarPartida.Enabled = false;
            btnSiguientePartida.Enabled = false; // no aplica: se está editando una partida puntual, no agregando en cadena
            // Antes esto quedaba en "true" sin importar el estatus del
            // documento. Ahora solo se habilita si el documento está activo.
            btnEliminarPartida.Enabled = DocumentoPermiteEdicionDePartidas();
            txtAlias.Enabled = false;
            cmbDivisa1.Enabled = false;

            // Modo "editar partida existente": btnConfirmarPartida actualizará
            // en vez de insertar. Solo se habilita si el documento sigue activo.
            editandoPartidaExistente = true;
            btnConfirmarPartida.Enabled = DocumentoPermiteEdicionDePartidas();
            btnConfirmarPartida.Text = "Actualizar Partida";
        }

        private void btnTerminarDocumento_Click(object sender, EventArgs e)
        {
            // CORRECCIÓN: antes se usaba "Partida1" (que en realidad es el
            // siguiente número de secuencia de partida, no un conteo real)
            // para fijar txtTotalPartidas. Si se había eliminado alguna
            // partida en el camino, ese número ya no coincidía con el conteo
            // real ni el total quedaba correcto. Se recalcula todo desde la
            // BD justo antes de bloquear el documento.
            RecalcularTotalesEncabezado();

            // CORRECCIÓN: antes se limpiaba la pantalla (Limpiar/LimpiarDetalle)
            // sin importar si BLoqueo() realmente había bloqueado el
            // documento. Si el usuario cancelaba el "¿desea continuar?" o el
            // documento tenía 0 partidas, la pantalla igual se vaciaba,
            // aunque el documento siguiera Activo y sin bloquear en la BD.
            // Ahora solo se limpia si el bloqueo se completó de verdad.
            if (!BLoqueo())
            {
                return;
            }

            guna2TabControl1.SelectedIndex = 0;

            txtTipoDocumento1.Text = "";
            cmbDescripcion1.Text = "";
            txtDescripcion1.Text = "";
            guna2TabControl1.Enabled = false;

            Limpiar();
            LimpiarDetalle();

            btnTerminarDocumento.Enabled = false;
            guna2DataGridView1.Rows.Clear();
        }

        private void btnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (!DocumentoPermiteEdicionDePartidas())
            {
                MessageBox.Show("Solo se pueden eliminar partidas cuando el documento está activo.");
                return;
            }

            c1.Eliminarpartida(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtNoPartida.Text);
            MessageBox.Show("Partida Eliminada");
            PanelPartidasRequisicion.Visible = false;

            string TipoM = txtTipoDocumento.Text;
            Descripcion = cmbDescripcion.Text;

            c.CargarPartida(guna2DataGridView1, TipoM, Descripcion, txtFolioP.Text);

            // CORRECCIÓN: antes, al eliminar una partida, el total del
            // encabezado (Total/Subtotal en memoria) nunca se decrementaba.
            // Al recalcular desde la BD, la partida eliminada ya no se
            // vuelve a contar.
            RecalcularTotalesEncabezado();

            LimpiarDetalle();
        }

        private bool BLoqueo()
        {
            // CORRECCIÓN: antes, si por cualquier motivo se volvía a invocar
            // BLoqueo() sobre un documento que ya no está "Activo" (por
            // ejemplo, ya "Bloqueado" o "Cancelado"), el método seguía
            // ejecutando ActualizarMovimiento y podía volver a llamar
            // RegistrarInventarioDelDocumento, generando movimientos de
            // almacén duplicados. Ahora solo se permite procesar el
            // documento cuando su transición es Activo -> Bloqueado.
            if (!DocumentoPermiteEdicionDePartidas())
            {
                MessageBox.Show("Este documento ya no está activo; no se puede volver a bloquear ni reprocesar sus movimientos de almacén.");
                return false;
            }

            if (txtTotalPartidas.Text == "0")
            {
                MessageBox.Show("Es necesario registrar articulos para bloquear.");
                return false;
            }

            if (MessageBox.Show("El registro quedara bloqueado, ¿desea continuar?", "Movimiento de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return false;
            }

            txtTotal.Text = Total.ToString();
            c.ActualizarMovimiento(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtTotalPartidas.Text, txtTotal.Text);

            // Al llegar aquí, ya se validó arriba que el estatus era "Activo",
            // así que los inventarios siempre se procesan exactamente una vez
            // por esta llamada (transición Activo -> Bloqueado).
            c1.RegistrarInventarioDelDocumento(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, txtAlmacen.Text, txtAlmacenSalida.Text, txtCosteo.Text, txtTipoCosteo.Text);
            MessageBox.Show("Inventarios registrados correctamente.");
            Limpiar();
            // CORRECCIÓN: antes BLoqueo() llamaba aquí mismo a Limpiar(), y
            // btnTerminarDocumento_Click también llamaba a Limpiar() otra vez
            // justo después de invocar BLoqueo() -- sin importar si este
            // realmente había terminado el proceso o había salido antes por
            // cualquiera de los "return" de arriba (0 partidas, usuario dijo
            // "No", documento no activo). Eso hacía que la pantalla se
            // limpiara igual aunque el documento NO se hubiera bloqueado,
            // dando la falsa impresión de que se perdió el trabajo. Ahora
            // BLoqueo() ya no limpia nada por sí mismo: solo informa con este
            // "return true" que sí terminó, y quien la llama decide si limpia.
            return true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Este botón es un atajo directo a "Bloquear" (ver el ToolTip
            // original en el constructor). Debe comportarse igual que
            // btnTerminarDocumento_Click: solo limpiar la pantalla si el
            // documento realmente quedó bloqueado.
            if (!BLoqueo())
            {
                return;
            }

            Limpiar();
            LimpiarDetalle();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != ESTATUS_CANCELADO)
            {
                AutentificarAdmin autentificarAdmin = new AutentificarAdmin();
                autentificarAdmin.ShowDialog();
            }
            else
            {
                MessageBox.Show("El documento esta cancelado no se puede desbloquear");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            cmbDivisa.Items.Clear();

            string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
            string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();

            // CORRECCIÓN: la variable local se llamaba "Documento", igual que
            // el campo de la clase que guarda el tipo de movimiento del
            // formulario (E/S/T). Esto ocultaba (shadowing) el campo dentro de
            // este método, lo cual no causaba un error hoy pero es una fuente
            // común de bugs si el método crece. Se renombró para evitar
            // confusión; el comportamiento es idéntico.
            string documentoSeleccionado = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();
            string Consecutivo = dataGridView1.Rows[e.RowIndex].Cells["Consecutivo"].Value.ToString();

            c.ConsultaEntradaSeleccionado(Folio, Tipo, documentoSeleccionado, txtTotalPartidas, txtReferencia, txtAlmacen, txtTotal, txtNotas, cmbEstatus, txtElaborado, lbldivisa, txtTipoCambio, txtFolioP);

            txtFolioRegistrar.Text = Consecutivo;
            txtTipoDocumento.Text = Tipo;
            cmbDescripcion.Text = documentoSeleccionado;

            if (!TryParseInt(txtTotalPartidas.Text, out int totalPartidas))
            {
                totalPartidas = 0;
            }
            MovimientosInventario.Partida = totalPartidas;

            if (txtAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen2(txtAlmacen.Text);
                cmbAlmacen.Text = valores[0];
            }

            cmbDivisa.Items.Add(lbldivisa.Text);
            cmbDivisa.SelectedIndex = 0;

            Descripcion = cmbDescripcion.Text;

            // CORRECCIÓN: antes se bloqueaba TODO (encabezado y detalle) sin
            // importar el estatus real del documento consultado. Buena
            // práctica: el encabezado (folio, tipo de documento, almacén)
            // permanece siempre protegido aquí, para no permitir volver a
            // "Iniciar Movimiento" ni alterar datos maestros de un documento
            // ya creado -- eso evitaría, por ejemplo, generar un folio nuevo
            // encima de uno existente. Pero el DETALLE (partidas) sí debe
            // quedar editable si el documento sigue "Activo", que es
            // justamente cuando SÍ se permite seguir agregando, actualizando
            // o eliminando partidas (ver DocumentoPermiteEdicionDePartidas).
            BloquearEncabezado();

            if (DocumentoPermiteEdicionDePartidas())
            {
                DesbloquearDetalle();
                btnTerminarDocumento.Visible = true;
                btnTerminarDocumento.Enabled = true;
            }
            else
            {
                BloquearDetalle();
                btnTerminarDocumento.Visible = false;
                btnTerminarDocumento.Enabled = false;
            }

            c.CargarPartida(guna2DataGridView1, txtTipoDocumento.Text, cmbDescripcion.Text, Folio);

            // CORRECCIÓN: al reabrir un documento existente, Total/Subtotal
            // (las variables en memoria) nunca se sincronizaban con lo que
            // ya existía en la BD para ese folio. Si después se seguían
            // agregando partidas, el acumulado partía de un valor
            // desactualizado. Se recalcula aquí para dejarlo consistente.
            RecalcularTotalesEncabezado();

            guna2GradientPanel2.Visible = false;
        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Colapsa el menú lateral (panel angosto con solo íconos). Antes este
        /// bloque de código estaba duplicado de forma idéntica cinco veces:
        /// en guna2PictureBox2_Click y en cada una de las cuatro ramas de
        /// toolStrip2_ItemClicked (NUEVO, CONSULTAR, IMPRIMIR, CATALOGO
        /// PRODUCTOS).
        /// </summary>
        private void ColapsarPanelLateral()
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;

            guna2GradientPanel6.Location = new Point(1077, 82);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1017, 82);

            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

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

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            ColapsarPanelLateral();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ColapsarPanelLateral();

            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;
                Limpiar();
                DesbloquearEncabezado();
                cmbDescripcion.DroppedDown = true;
                cmbDescripcion.Focus();
                DesbloquearDetalle();
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
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
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                if (cmbEstatus.Text != ESTATUS_BLOQUEADO)
                {
                    MessageBox.Show("El movimiento no esta bloqueado");
                    return;
                }

                ReporteMovimientoInventario r = new ReporteMovimientoInventario(txtFolioP.Text, txtTipoDocumento.Text);
                r.ShowDialog();
            }
            else if (e.ClickedItem.Text == "CATALOGO PRODUCTOS")
            {
                int consulta = 1;
                CatalogoProductosServicios prod = new CatalogoProductosServicios(consulta);
                prod.ShowDialog();
            }
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtFolioRegistrar.Text))
                {
                    MessageBox.Show("Es necesario crear el encabezado");
                    guna2TabControl1.SelectedIndex = 0;
                }
            }
        }

        // ==========================================================
        // Métodos auxiliares (validación y parseo seguro)
        // Se agregaron para eliminar duplicación y evitar excepciones
        // no controladas por conversiones (Convert.ToInt32 / ToDecimal)
        // sobre texto capturado por el usuario.
        // ==========================================================

        /// <summary>
        /// Extrae la clave del producto seleccionado en cmbProducto
        /// (formato "CLAVE-DESCRIPCION"). Antes se repetía este cálculo en
        /// tres lugares distintos sin validar si existía el separador "-",
        /// lo que podía lanzar una excepción (Substring con índice -1).
        /// </summary>
        private bool ObtenerClaveProducto(out string clave)
        {
            clave = string.Empty;
            string producto = cmbProducto.Text;
            int index = producto.IndexOf("-", StringComparison.Ordinal);

            if (index <= 0)
            {
                MessageBox.Show("Seleccione un producto válido");
                return false;
            }

            clave = producto.Substring(0, index);
            return true;
        }

        /// <summary>
        /// Valida cantidad, precio, divisa y existencia de almacén antes de
        /// registrar una partida. Antes esta validación estaba duplicada,
        /// casi idéntica, en btnSiguientePartida_Click y btnConfirmarPartida_Click.
        /// El único mensaje que difiere entre ambos casos es el de precio
        /// inválido, por eso se recibe como parámetro.
        /// </summary>
        private bool ValidarDatosPartida(string mensajeErrorPrecio)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || txtCantidad.Text == "0")
            {
                MessageBox.Show("Registre la cantidad para continuar");
                return false;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text) || txtPrecio.Text == "0.00")
            {
                MessageBox.Show(mensajeErrorPrecio);
                return false;
            }

            if (string.IsNullOrEmpty(cmbDivisa.Text))
            {
                MessageBox.Show("Registre la divisa para continuar");
                return false;
            }

            if ((cmbDescripcion.Text == TIPO_TRASPASO || cmbDescripcion.Text == TIPO_SALIDA)
                && (txtExAlmacen.Text == "0" || string.IsNullOrEmpty(txtExAlmacen.Text)))
            {
                MessageBox.Show("El almacen de salida debe tener existencia del producto");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que la cantidad solicitada no exceda la existencia del
        /// almacén de salida, solo aplica para Salidas y Traspasos.
        /// Se respeta la diferencia original entre btnSiguientePartida_Click
        /// (que evaluaba cmbDescripcion.Text) y btnConfirmarPartida_Click
        /// (que evaluaba Documento) recibiendo ese valor como parámetro.
        /// </summary>
        private bool ExcedeExistenciaAlmacen(string tipoDocumentoParaValidar)
        {
            if (tipoDocumentoParaValidar != TIPO_SALIDA && tipoDocumentoParaValidar != TIPO_TRASPASO)
            {
                return false;
            }

            if (!TryParseInt(txtCantidad.Text, out int cantidadSolicitada) ||
                !TryParseInt(txtExAlmacen.Text, out int existenciaAlmacen))
            {
                MessageBox.Show("Formato de cantidad o existencia incorrecto");
                return true;
            }

            if (cantidadSolicitada > existenciaAlmacen)
            {
                MessageBox.Show("La cantidad no puede ser mayor a la existencia del almacen");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Recalcula Total, Subtotal y el número de partidas del documento
        /// consultando directamente la tabla de partidas en la base de datos
        /// (fuente de verdad), en vez de ir acumulando/restando en memoria.
        /// Esto evita que el encabezado se desincronice al agregar, confirmar,
        /// eliminar partidas, o al reabrir un documento ya existente.
        /// NOTA: se asigna el mismo valor a Total y Subtotal porque este
        /// documento no maneja Descuento/Impuesto de forma independiente en
        /// el código revisado; si en tu negocio Subtotal debe excluir algún
        /// concepto, ajusta este método para restar/sumar esos campos aquí.
        /// </summary>
        private void RecalcularTotalesEncabezado()
        {
            if (string.IsNullOrEmpty(txtFolioP.Text))
            {
                return;
            }

            c1.ObtenerTotalesPartidas(txtFolioP.Text, txtTipoDocumento.Text, cmbDescripcion.Text, out decimal totalCalculado, out int totalPartidasCalculado);

            Total = totalCalculado;
            Subtotal = totalCalculado;

            txtTotal.Text = Total.ToString();
            txtTotalPartidas.Text = totalPartidasCalculado.ToString();
        }

        /// <summary>
        /// Indica si el documento actual permite agregar, actualizar o
        /// eliminar partidas. Se valida contra ESTATUS_ACTIVO ("Activo") y
        /// NO contra "Abierto", porque revisando el flujo completo del
        /// formulario, "Abierto" nunca se guarda como estatus real de un
        /// documento en la base de datos: es solo el texto que Limpiar()
        /// pone en cmbEstatus cuando no hay ningún documento cargado. El
        /// estatus real con el que se crea un documento (RegistroMovimientoInventario,
        /// disparado desde btnIniciarMovimiento_Click) es "Activo". Si en tu
        /// negocio existen documentos reales guardados con Estatus = 'Abierto',
        /// avísame y cambio esta comparación aquí mismo.
        /// </summary>
        private bool DocumentoPermiteEdicionDePartidas()
        {
            return cmbEstatus.Text == ESTATUS_ACTIVO;
        }

        /// <summary>
        /// Calcula el siguiente número de partida basándose en lo que
        /// realmente existe en guna2DataGridView1 (la grilla de partidas ya
        /// guardadas para el documento actual), que es la fuente de verdad.
        /// Antes se usaba un contador en memoria (Partida1) que se
        /// incrementaba especulativamente cada vez que se abría el panel de
        /// captura, sin importar si el usuario realmente confirmaba una
        /// partida. Eso causaba dos desincronizaciones: (1) al eliminar una
        /// partida el número no bajaba (si tenías 1,2,3 y borrabas la 3, la
        /// siguiente debía ser 3 otra vez, no 4), y (2) al abrir el panel y
        /// darle "Cerrar" sin agregar nada, el contador ya había subido de
        /// todos modos, y volvía a subir la próxima vez que se abría.
        /// Calculando siempre desde la grilla, ambos casos quedan resueltos
        /// automáticamente sin necesidad de rastrear manualmente cada acción.
        /// </summary>
        private int ObtenerSiguienteNumeroPartida()
        {
            int maxNoPartida = 0;

            foreach (DataGridViewRow fila in guna2DataGridView1.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }

                object valorCelda = fila.Cells["NoPartida"].Value;
                if (valorCelda != null && TryParseInt(valorCelda.ToString(), out int noPartidaFila))
                {
                    if (noPartidaFila > maxNoPartida)
                    {
                        maxNoPartida = noPartidaFila;
                    }
                }
            }

            return maxNoPartida + 1;
        }

        private static bool TryParseInt(string valor, out int resultado)
        {
            return int.TryParse(valor, NumberStyles.Any, CultureInfo.CurrentCulture, out resultado);
        }

        private static bool TryParseDecimal(string valor, out decimal resultado)
        {
            return decimal.TryParse(valor, NumberStyles.Any, CultureInfo.CurrentCulture, out resultado);
        }
    }
}