using Condominios.Clases.CentroCostos;
using Condominios.Clases.Documentos;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.CentroCostos;
using PV.Clases.Requisicion;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Formulario para la gestión de requisiciones de compra.
    /// </summary>
    public partial class Requisicion2 : Form
    {
        private readonly DBRequisicion _dbRequisicion = new DBRequisicion();
        private readonly DBCentroCostos _dbCentroCostos = new DBCentroCostos();
        private readonly DBDatosProyecto _dbProyecto = new DBDatosProyecto();
        private readonly DBProductosServicios _dbProductos = new DBProductosServicios();
        private readonly DBDocumentos _dbDocumentos = new DBDocumentos();

        private string _tipoOperacion = string.Empty; // "Nuevo" o "Consulta"
        private bool _mostrarCentroCosto = false;

        public Requisicion2()
        {
            InitializeComponent();
        }

        // ==================== EVENTOS DEL FORMULARIO ====================

        private void Requisicion2_Load(object sender, EventArgs e)
        {
            CargarDatosIniciales();
        }

        // ==================== MÉTODOS PRIVADOS ====================

        /// <summary>
        /// Carga los datos iniciales: listas de requisiciones, documentos, estatus, fechas, etc.
        /// </summary>
        private void CargarDatosIniciales()
        {
            _dbRequisicion.CargarRequisiciones(dataGridView1);
            LlenarComboDocumento(); // Ahora usa DataSource
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtElaborado.Text = DBLogin.usuario;

            cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            ActualizarFechaVencimiento();

            LlenarComboCentroCostos();
        }

        /// <summary>
        /// Llena el combo de centros de costo con DataSource.
        /// </summary>
        private void LlenarComboCentroCostos()
        {
            try
            {
                DataTable centros = _dbCentroCostos.ConsultarTodos();
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostos.DataSource = centros;
                cmbCentroCostos.DisplayMember = "Nombre";
                cmbCentroCostos.ValueMember = "Clave";
                cmbCentroCostos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar centros de costo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Llena el combo de documentos con DataSource para poder usar SelectedValue.
        /// </summary>
        private void LlenarComboDocumento()
        {
            try
            {
                DataTable documentos = _dbDocumentos.ConsultarDocumento(tarea: "Requisiciones"); // Debe devolver Clave y Nombre
                cmbDocumento.DropDownStyle = ComboBoxStyle.DropDown;
                cmbDocumento.DataSource = documentos;
                cmbDocumento.DisplayMember = "Nombre";    // Ajusta según tu DataTable
                cmbDocumento.ValueMember = "Clave";       // Ajusta según tu DataTable
                cmbDocumento.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar documentos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Llena el combo de productos con DataSource.
        /// </summary>
        private void LlenarComboProductos()
        {
            try
            {
                DataTable productos = _dbProductos.ObtenerProductos(); // Debe devolver ClaveProducto y Descripcion
                cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
                cmbConcepto.DataSource = productos;
                cmbConcepto.DisplayMember = "Descripcion"; // Ajusta según tu DataTable
                cmbConcepto.ValueMember = "ClaveProducto";  // Ajusta según tu DataTable
                cmbConcepto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actualiza la fecha de vencimiento según los días ingresados.
        /// </summary>
        private void ActualizarFechaVencimiento()
        {
            if (int.TryParse(txtDiasVence.Text, out int dias))
            {
                DateTime fechaVence = DateTime.Parse(txtFecha.Text).AddDays(dias);
                txtFechaVence.Text = fechaVence.ToString("yyyy/MM/dd");
            }
        }

        /// <summary>
        /// Limpia todos los campos del encabezado.
        /// </summary>
        private void LimpiarEncabezado()
        {
            txtFolio.Clear();
            cmbDocumento.SelectedIndex = -1;
            txtDocumento.Clear();
            txtClave.Clear();
            cmbEstatus.SelectedIndex = 0;
            txtConsecutivo.Clear();
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDiasVence.Text = "0";
            ActualizarFechaVencimiento();
            txtNotas.Clear();
            txtPartidas.Text = "0";
            txtTotalConceptos.Text = "0";
            txtElaborado.Text = DBLogin.usuario;
            cmbCentroCostos.SelectedIndex = -1;
            cmbproyecto.DataSource = null;
            cmbproyecto.SelectedIndex = -1;
        }

        /// <summary>
        /// Limpia los campos del detalle de partidas.
        /// </summary>
        private void LimpiarDetalle()
        {
            TxtFolio2.Clear();
            txtFrecuencia.Clear();
            txtTipo.Clear();
            txtCentroCosto2.Clear();
            txtConcepto3.Clear();
            txtClave2.Clear();
            txtPartida.Clear();
            cmbConcepto.SelectedIndex = -1;
            txtClaveConcepto.Clear();
            txtConcepto2.Clear();
            txtCantidad.Clear();
            txtUnidad.Clear();
            txtExistencia.Clear();
        }

        /// <summary>
        /// Limpia los campos de nueva partida (manteniendo el número de partida).
        /// </summary>
        private void LimpiarDetalleNuevaPartida()
        {
            cmbConcepto.SelectedIndex = -1;
            txtClaveConcepto.Clear();
            txtConcepto2.Clear();
            txtUnidad.Clear();
            txtExistencia.Clear();
            LlenarComboProductos(); // Recarga el combo con todos los productos
        }

        /// <summary>
        /// Habilita o deshabilita los controles del encabezado según el estatus.
        /// </summary>
        private void SetHeaderControlsEnabled(bool enabled)
        {
            cmbDocumento.Enabled = enabled;
            txtDiasVence.Enabled = enabled;
            cmbCentroCostos.Enabled = enabled;
            cmbproyecto.Enabled = enabled;
            txtNotas.Enabled = enabled;
        }

        /// <summary>
        /// Habilita o deshabilita los controles del detalle según el estatus.
        /// </summary>
        private void SetDetailControlsEnabled(bool enabled)
        {
            cmbConcepto.Enabled = enabled;
            txtCantidad.Enabled = enabled;
        }

        // ==================== EVENTOS DE BOTONES Y CONTROLES ====================

        private void guna2CircleButton1_Click(object sender, EventArgs e) => Close();

        private void txtDiasVence_TextChanged(object sender, EventArgs e) => ActualizarFechaVencimiento();

        private void cmbCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar proyectos según centro de costo seleccionado
            if (cmbCentroCostos.SelectedIndex != -1)
            {
                string centro = cmbCentroCostos.SelectedValue.ToString();
                DataTable dtProyectos = _dbProyecto.ObtenerProyectosPorCentroCostos(cmbCentroCostos.Text);
                ComboUtil.LlenarComboBox(cmbproyecto, dtProyectos, "Proyecto", "Id");
            }
        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocumento.SelectedValue != null && string.IsNullOrEmpty(txtFolio.Text))
            {
                string claveDoc = cmbDocumento.SelectedValue.ToString();
                txtConsecutivo.Text = _dbRequisicion.ObtenerConsecutivo(claveDoc);
                txtDiasVence.Focus();
            }
        }

        // ==================== BOTÓN AGREGAR PARTIDA ====================

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!ValidarAntesDeAgregarPartida()) return;

            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();

            // Cargar combo de productos
            LlenarComboProductos();
            // Obtener siguiente partida
            int siguiente = _dbRequisicion.ObtenerSiguientePartida(TxtFolio2.Text);
            txtPartida.Text = siguiente.ToString();

            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtPartida.Enabled = false;
            cmbConcepto.Enabled = true;
            txtClaveConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtUnidad.Enabled = true;
            txtExistencia.Enabled = false;

            if (_tipoOperacion == "Nuevo")
            {
                btnEliminarPartida.Visible = false;
                btnEliminarPartida.Enabled = false;
                btnConfirmarPartida.Visible = true;
                btnSiguientePartida.Visible = true;
            }
        }

        private bool ValidarAntesDeAgregarPartida()
        {
            if (!int.TryParse(txtDiasVence.Text, out _))
            {
                MessageBox.Show("Registre los días de vencimiento antes de continuar.");
                return false;
            }
            if (cmbCentroCostos.SelectedValue == null)
            {
                MessageBox.Show("Registre el centro de costo antes de continuar.");
                return false;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una orden con estatus Bloqueado o Cancelado.");
                return false;
            }
            if (cmbDocumento.SelectedValue == null)
            {
                MessageBox.Show("Registre el documento para continuar.");
                return false;
            }
            return true;
        }

        // ==================== BOTÓN REGISTRAR REQUISICIÓN ====================

        private void btnRegistrarRequisicion_Click(object sender, EventArgs e)
        {
            if (!ValidarAntesDeRegistrar()) return;

            // Si no tiene folio, insertar nueva requisición
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                int nuevoFolio = _dbRequisicion.ObtenerSiguienteFolio();
                txtFolio.Text = nuevoFolio.ToString();

                _dbRequisicion.InsertarRequisicion(
                    txtFolio.Text,
                    cmbDocumento.SelectedValue?.ToString() ?? "",
                    cmbEstatus.Text,
                    txtFecha.Text,
                    txtDiasVence.Text,
                    txtFechaVence.Text,
                    cmbCentroCostos.SelectedValue?.ToString() ?? "",
                    cmbproyecto.SelectedValue?.ToString() ?? "",
                    txtNotas.Text,
                    txtElaborado.Text,
                    txtConsecutivo.Text
                );
            }

            // Actualizar la referencia de la requisición en la pestaña de partidas
            TxtFolio2.Text = txtFolio.Text;

            // Cambiar a pestaña de partidas
            guna2TabControl1.SelectedIndex = 1;

            // Cargar partidas existentes
            _dbRequisicion.CargarPartidas(dgvPartidas, TxtFolio2.Text);

            // Deshabilitar controles del encabezado (ya no se pueden modificar)
            SetHeaderControlsEnabled(false);
        }

        private bool ValidarAntesDeRegistrar()
        {
            if (!int.TryParse(txtDiasVence.Text, out _))
            {
                MessageBox.Show("Registre los días de vencimiento antes de continuar.");
                return false;
            }
            if (cmbCentroCostos.SelectedValue == null)
            {
                MessageBox.Show("Registre el centro de costo antes de continuar.");
                return false;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible registrar una orden con estatus Bloqueado o Cancelado.");
                return false;
            }
            if (cmbDocumento.SelectedValue == null)
            {
                MessageBox.Show("Registre el documento para continuar.");
                return false;
            }
            return true;
        }

        // ==================== PARTIDAS: CONFIRMAR Y SIGUIENTE ====================

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.SelectedValue == null)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int partida = int.Parse(txtPartida.Text) - 1;
                    if (partida > 0)
                    {
                        _dbRequisicion.ActualizarTotalPartidas(TxtFolio2.Text, partida);
                    }
                    Close();
                }
                return;
            }

            if (txtCantidad.Text == "0.00" || txtCantidad.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar una cantidad no se guardará.", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int partida = int.Parse(txtPartida.Text) - 1;
                    if (partida > 0)
                    {
                        _dbRequisicion.ActualizarTotalPartidas(TxtFolio2.Text, partida);
                    }
                    Close();
                }
                return;
            }

            if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automáticos deben registrarse en recibos individuales. Este recibo ya cuenta con una partida, seleccione otro concepto.");
                return;
            }

            // Insertar partida
            int partidaActual = int.Parse(txtPartida.Text);
            _dbRequisicion.InsertarPartida(
                TxtFolio2.Text,
                partidaActual,
                cmbConcepto.SelectedValue.ToString(),
                txtConcepto2.Text,
                txtCantidad.Text,
                txtUnidad.Text
            );

            // Actualizar total de partidas
            _dbRequisicion.ActualizarTotalPartidas(TxtFolio2.Text, partidaActual);

            // Cerrar panel y actualizar grid
            PanelPartidasRequisicion.Visible = false;
            _dbRequisicion.CargarPartidas(dgvPartidas, TxtFolio2.Text);
            btnTerminarRequisicion.Visible = true;

            LimpiarDetalleNuevaPartida();
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.SelectedValue == null)
            {
                MessageBox.Show("Registre el producto para continuar.");
                return;
            }
            if (txtFrecuencia.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automáticos deben registrarse en recibos individuales. Termine el registro o cambie el concepto.");
                return;
            }

            // Insertar partida actual y preparar la siguiente
            int partidaActual = int.Parse(txtPartida.Text);
            _dbRequisicion.InsertarPartida(
                TxtFolio2.Text,
                partidaActual,
                cmbConcepto.SelectedValue.ToString(),
                txtConcepto2.Text,
                txtCantidad.Text,
                txtUnidad.Text
            );

            // Actualizar total de partidas
            _dbRequisicion.ActualizarTotalPartidas(TxtFolio2.Text, partidaActual);

            // Obtener siguiente partida
            int siguiente = _dbRequisicion.ObtenerSiguientePartida(TxtFolio2.Text);
            txtPartida.Text = siguiente.ToString();

            LimpiarDetalleNuevaPartida();
        }

        // ==================== ELIMINAR PARTIDA ====================

        private void brnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se pueden eliminar partidas cuando la requisición está en estatus 'Abierto'.");
                return;
            }

            if (MessageBox.Show("¿Eliminar la partida seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int partida = int.Parse(txtPartida.Text);
                _dbRequisicion.EliminarPartida(TxtFolio2.Text, partida);

                // Actualizar total de partidas (recalcular)
                int total = _dbRequisicion.ObtenerSiguientePartida(TxtFolio2.Text) - 1;
                _dbRequisicion.ActualizarTotalPartidas(TxtFolio2.Text, total);

                PanelPartidasRequisicion.Visible = false;
                _dbRequisicion.CargarPartidas(dgvPartidas, TxtFolio2.Text);
            }
        }

        // ==================== TERMINAR REQUISICIÓN (BLOQUEAR) ====================

        private void btnTerminarRequisicion_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("La requisición ya no está abierta.");
                return;
            }

            cmbEstatus.Text = "Bloqueado";
            _dbRequisicion.ActualizarEstatus(txtFolio.Text, cmbEstatus.Text);

            LimpiarEncabezado();
            LimpiarDetalle();
            guna2TabControl1.Enabled = false;
            guna2TabControl1.SelectedIndex = 0;
            btnTerminarRequisicion.Visible = false;
            _dbRequisicion.CargarRequisiciones(dataGridView1);
        }

        // ==================== CONSULTAS Y FILTROS ====================

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                txtFiltroDocumento.Clear();
                txtFiltroNombre.Clear();
                _dbRequisicion.FiltrarPorConsecutivo(dataGridView1, txtFiltro.Text);
            }
            else
            {
                _dbRequisicion.CargarRequisiciones(dataGridView1);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFiltroDocumento.Text))
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                _dbRequisicion.FiltrarPorDocumento(dataGridView1, txtFiltroDocumento.Text);
            }
            else
            {
                _dbRequisicion.CargarRequisiciones(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFiltroNombre.Text))
            {
                txtFiltroDocumento.Clear();
                txtFiltro.Clear();
                _dbRequisicion.FiltrarPorProyecto(dataGridView1, txtFiltroNombre.Text);
            }
            else
            {
                _dbRequisicion.CargarRequisiciones(dataGridView1);
            }
        }

        // ==================== SELECCIÓN DE REQUISICIÓN EN GRID ====================

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();

            // Cargar encabezado
            _dbRequisicion.CargarEncabezado(folio,
                out string claveDoc, out string estatus, out string fecha,
                out string diasVenc, out string fechaVenc,
                out string centroCosto, out string proyecto,
                out string totalPartidas, out string notas,
                out string elaborado, out string consecutivo);

            txtFolio.Text = folio;
            txtClave.Text = claveDoc;
            cmbEstatus.Text = estatus;
            txtFecha.Text = fecha;
            txtDiasVence.Text = diasVenc;
            txtFechaVence.Text = fechaVenc;
            txtPartidas.Text = totalPartidas;
            txtNotas.Text = notas;
            txtElaborado.Text = elaborado;
            txtConsecutivo.Text = consecutivo;

            // Seleccionar centro de costo y documento usando SelectedValue
            cmbCentroCostos.SelectedValue = centroCosto;
            cmbDocumento.SelectedValue = claveDoc;

            // Cargar proyectos según centro de costo
            DataTable dtProyectos = _dbProyecto.ObtenerProyectosPorCentroCostos(centroCosto);
            ComboUtil.LlenarComboBox(cmbproyecto, dtProyectos, "Proyecto", "Id");
            cmbproyecto.SelectedValue = proyecto;

            // Cargar partidas
            _dbRequisicion.CargarPartidas(dgvPartidas, folio);

            // Habilitar/deshabilitar según estatus
            bool isAbierto = estatus.ToLower() == "abierto";
            SetHeaderControlsEnabled(isAbierto);
            SetDetailControlsEnabled(isAbierto);

            // Mostrar pestaña de detalle
            guna2TabControl1.Enabled = true;
            guna2GradientPanel2.Visible = false;
            btnTerminarRequisicion.Visible = isAbierto;

            _tipoOperacion = isAbierto ? "Nuevo" : "Consulta";
        }

        // ==================== DOBLE CLICK EN PARTIDA (EDITAR) ====================

        private void dgvPartidas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se pueden editar partidas cuando la requisición está en estatus 'Abierto'.");
                return;
            }

            int partida = Convert.ToInt32(dgvPartidas.Rows[e.RowIndex].Cells["Partida"].Value);
            txtPartida.Text = partida.ToString();

            var (claveProd, concepto, descripcion, cantidad, unidad, existencia) =
                _dbRequisicion.ObtenerPartida(txtFolio.Text, partida);

            txtClaveConcepto.Text = claveProd;
            txtConcepto3.Text = descripcion;
            txtConcepto2.Text = concepto;
            txtCantidad.Text = cantidad;
            txtUnidad.Text = unidad;
            txtExistencia.Text = existencia;

            // Mostrar panel de partidas en modo edición
            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();
            guna2Button11.Visible = true;

            // Cargar combo con el concepto actual (seleccionar por descripción)
            cmbConcepto.SelectedIndex = -1;
            foreach (DataRowView item in cmbConcepto.Items)
            {
                if (item["Descripcion"].ToString() == descripcion)
                {
                    cmbConcepto.SelectedItem = item;
                    break;
                }
            }

            // Ocultar botones de nuevo
            btnConfirmarPartida.Visible = false;
            btnSiguientePartida.Visible = false;
            btnEliminarPartida.Visible = true;
            btnEliminarPartida.Enabled = true;
        }

        // ==================== BOTÓN CERRAR PANEL PARTIDA ====================

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
            guna2Button11.Visible = false;
        }

        // ==================== OTROS BOTONES ====================

        private void btnLimpiarRequisicion_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && txtPartidas.Text != "0")
            {
                MessageBox.Show("No es posible limpiar la orden. Confirme la orden para continuar.");
                return;
            }
            if (cmbEstatus.Text == "Abierto" && txtPartidas.Text == "0")
            {
                _dbRequisicion.EliminarRequisicion(txtFolio.Text);
                LimpiarEncabezado();
                return;
            }
            LimpiarEncabezado();
        }

        private void btnEliminarRequisicion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                MessageBox.Show("Seleccione un recibo.");
                return;
            }
            if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una orden que no está bloqueada.");
                return;
            }
            if (MessageBox.Show("El saldo de esta orden será cancelado. ¿Desea continuar?",
                "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Cancelado";
                _dbRequisicion.ActualizarEstatus(txtFolio.Text, cmbEstatus.Text);
                LimpiarEncabezado();
                LimpiarDetalle();
                MessageBox.Show("Orden de compra cancelada.");
            }
        }

        private void btnLimpiarPartida_Click(object sender, EventArgs e)
        {
            LimpiarDetalleNuevaPartida();
            PanelPartidasRequisicion.Visible = false;
        }

        // ==================== EVENTOS DE TOOLSTRIP (MENÚ LATERAL) ====================

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    Nuevo();
                    break;
                case "CONSULTAR":
                    Consultar();
                    break;
                case "IMPRIMIR":
                    // Lógica de impresión
                    break;
            }
        }

        private void Nuevo()
        {
            // Ocultar panel lateral
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();
            toolStrip2.Visible = false;
            toolStrip2.Size = new Size(23, 569);
            toolStripButton1.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);

            // Limpiar y preparar para nuevo
            LimpiarEncabezado();
            LimpiarDetalle();
            _dbRequisicion.CargarRequisiciones(dataGridView1);
            LlenarComboDocumento();
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtElaborado.Text = DBLogin.usuario;
            cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            ActualizarFechaVencimiento();
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Enabled = true;

            // Habilitar botones de partidas
            guna2Button2.Visible = true;
            btnEliminarPartida.Visible = true;
            btnLimpiarPartida.Visible = true;
            btnConfirmarPartida.Visible = true;
            btnSiguientePartida.Visible = true;
            guna2Button2.Enabled = true;
            btnEliminarPartida.Enabled = true;
            btnLimpiarPartida.Enabled = true;
            btnConfirmarPartida.Enabled = true;
            btnSiguientePartida.Enabled = true;

            _tipoOperacion = "Nuevo";
            cmbDocumento.Enabled = true;
            cmbCentroCostos.Enabled = true;
            cmbproyecto.Enabled = true;

            // Limpiar grid de partidas
            dgvPartidas.Rows.Clear();
        }

        private void Consultar()
        {
            // Mostrar panel de búsqueda
            if (guna2GradientPanel2.Visible)
            {
                guna2GradientPanel2.Visible = false;
                guna2GradientPanel2.SendToBack();
            }
            else
            {
                guna2GradientPanel2.Visible = true;
                guna2GradientPanel2.BringToFront();
            }

            // Deshabilitar edición
            cmbDocumento.Enabled = false;
            cmbCentroCostos.Enabled = false;
            cmbproyecto.Enabled = false;
            guna2Button2.Enabled = false;
            btnEliminarPartida.Enabled = false;
            btnLimpiarPartida.Enabled = false;
            btnConfirmarPartida.Enabled = false;
            btnSiguientePartida.Enabled = false;

            _tipoOperacion = "Consulta";
        }

        // ==================== EVENTOS DE IMÁGENES (EXPANDIR/CONTRAER MENÚ) ====================

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            // Expandir menú lateral
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();
            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = ToolStripTextDirection.Horizontal;
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            // Contraer menú lateral
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();
            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = ToolStripTextDirection.Vertical270;
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
        }

        // ==================== EVENTOS ADICIONALES (CIERRE DE PANEL BÚSQUEDA) ====================

        private void guna2ControlBox1_Click(object sender, EventArgs e) => guna2GradientPanel2.Visible = false;
        private void guna2Button10_Click(object sender, EventArgs e) => guna2GradientPanel2.Visible = false;

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}