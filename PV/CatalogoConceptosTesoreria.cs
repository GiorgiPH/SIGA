using PuntoVentas.Clases.Login;
using PV.Clases.ConceptoPago;
using PV.dto;
using System;
using System.Windows.Forms;

namespace PV
{
    public partial class CatalogoConceptosTesoreria : Form
    {
        private readonly DBConceptoCobroPago _dao = new DBConceptoCobroPago();
        private readonly DBClaseConcepto _daoClase = new DBClaseConcepto();
        private ConceptoCobroPagoData _conceptoActual;

        public CatalogoConceptosTesoreria()
        {
            InitializeComponent();
        }

        // Enlazado en el Designer como "Almacenes_Load" (nombre heredado del copy-paste).
        private void Almacenes_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarGrid();
            groupBox1.Enabled = false;
        }

        private void CargarCombos()
        {
            cmbClase.DataSource = _daoClase.ObtenerActivos();
            cmbClase.DisplayMember = "Descripcion";
            cmbClase.ValueMember = "IdClase";
            cmbClase.SelectedIndex = -1;
        }

        private void CargarGrid()
        {
            var lista = _dao.Listar(null, null);

            dgvConceptos.Rows.Clear();
            foreach (var c in lista)
            {
                int fila = dgvConceptos.Rows.Add(c.ClaveConcepto, c.Descripcion, c.Estatus ? "Activo" : "Inactivo");
                dgvConceptos.Rows[fila].Tag = c.IdConcepto; // el IdConcepto real viaja oculto en el Tag de la fila
            }
        }

        private void btnNuevoConcepto_Click(object sender, EventArgs e)
        {
            _conceptoActual = new ConceptoCobroPagoData();
            groupBox1.Enabled = true;

            txtClaveDivisa.Clear();
            txtClaveDivisa.Enabled = true;
            txtNombre.Clear();
            txtNotas.Clear();
            cmbClase.SelectedIndex = -1;
            tgEstatus.Checked = true;       // dispara tgEstatus_CheckedChanged -> lblEstatus.Text = "Activo"
            tgAutorizacion.Checked = false; // dispara tgAutorizacion_CheckedChanged -> limpia radios y deshabilita panel

            txtClaveDivisa.Focus();
        }

        private void dgvConceptos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idConcepto = (int)dgvConceptos.Rows[e.RowIndex].Tag;
            _conceptoActual = _dao.ObtenerPorId(idConcepto);
            if (_conceptoActual == null) return;

            groupBox1.Enabled = true;
            txtClaveDivisa.Text = _conceptoActual.ClaveConcepto;
            txtClaveDivisa.Enabled = false; // la clave no se modifica una vez creada
            txtNombre.Text = _conceptoActual.Descripcion;
            cmbClase.SelectedValue = _conceptoActual.IdClase;
            tgEstatus.Checked = _conceptoActual.Estatus;
            tgAutorizacion.Checked = _conceptoActual.RequiereAutorizacion;
            //rbAdministradorSupervisor.Checked = _conceptoActual.TipoAutorizacion == 1;
            //rbOtro.Checked = _conceptoActual.TipoAutorizacion == 2;
            txtNotas.Text = _conceptoActual.Notas;
            PanelUsuario.Visible = false;
        }

        private void btnConfirmarConcepto_Click(object sender, EventArgs e)
        {
            if (_conceptoActual == null) return;

            RecolectarDatosDeForma();

            if (!ValidarDatos(out string mensajeError))
            {
                MessageBox.Show(mensajeError, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_dao.ExisteClave(_conceptoActual.ClaveConcepto, _conceptoActual.IdConcepto))
            {
                MessageBox.Show($"Ya existe un concepto con la clave '{_conceptoActual.ClaveConcepto}'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _conceptoActual.Usuario = DBLogin.usuario;

                if (_conceptoActual.IdConcepto == 0)
                    _dao.Insertar(_conceptoActual);
                else
                    _dao.Actualizar(_conceptoActual);

                MessageBox.Show("El concepto se guardó correctamente.", "Catálogo de conceptos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGrid();
                LimpiarCampos();
                groupBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                // Sustituir por el mecanismo de bitácora de tu ERP.
                MessageBox.Show("Ocurrió un error al guardar el concepto." + Environment.NewLine + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nombre de variable heredado; funciona como el botón "Eliminar" (observación 2).
        private void btnEliminarConcepto_Click(object sender, EventArgs e)
        {
            if (dgvConceptos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un concepto de la lista.", "Catálogo de conceptos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idConcepto = (int)dgvConceptos.CurrentRow.Tag;

            if (_dao.TieneMovimientosAsociados(idConcepto))
            {
                MessageBox.Show("No es posible eliminar el concepto porque tiene movimientos asociados. Puedes inactivarlo en su lugar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var respuesta = MessageBox.Show("¿Desea eliminar el concepto seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                _dao.Eliminar(idConcepto, DBLogin.usuario);
                CargarGrid();
                LimpiarCampos();
                groupBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el concepto." + Environment.NewLine + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tgEstatus_CheckedChanged(object sender, EventArgs e)
        {
            lblEstatus.Text = tgEstatus.Checked ? "Activo" : "Inactivo";
        }

        // Falta enlazar en el Designer: this.tgAutorizacion.CheckedChanged += new EventHandler(this.tgAutorizacion_CheckedChanged);
        private void tgAutorizacion_CheckedChanged(object sender, EventArgs e)
        {
            lblAutorizacion.Text = tgAutorizacion.Checked ? "Sí" : "No";
            //pnlTipoAutorizacion.Enabled = tgAutorizacion.Checked; // requiere el panel pendiente (observación 4)
            if (!tgAutorizacion.Checked)
            {
                //rbAdministradorSupervisor.Checked = false;
                //rbOtro.Checked = false;
            }
        }

        private void lblEstatus_Click(object sender, EventArgs e)
        {
            tgEstatus.Checked = !tgEstatus.Checked;
        }

        // Sugerido: separar de lblEstatus_Click y enlazar en el Designer
        // this.lblAutorizacion.Click += new EventHandler(this.lblAutorizacion_Click);
        private void lblAutorizacion_Click(object sender, EventArgs e)
        {
            tgAutorizacion.Checked = !tgAutorizacion.Checked;
        }

        private void RecolectarDatosDeForma()
        {
            _conceptoActual.ClaveConcepto = txtClaveDivisa.Text.Trim().ToUpper();
            _conceptoActual.Descripcion = txtNombre.Text.Trim();
            _conceptoActual.IdClase = cmbClase.SelectedValue == null ? (byte)0 : Convert.ToByte(cmbClase.SelectedValue);
            _conceptoActual.Estatus = tgEstatus.Checked;
            _conceptoActual.RequiereAutorizacion = tgAutorizacion.Checked;
            //_conceptoActual.TipoAutorizacion = !tgAutorizacion.Checked
            //    ? (byte?)null
            //    : rbAdministradorSupervisor.Checked ? (byte)1 : (byte)2; // 1=Administrador/Supervisor, 2=Otro
            _conceptoActual.Notas = txtNotas.Text.Trim();
        }

        // Lógica de negocio embebida en la presentación: sin excepciones propias, regresa
        // true/false + mensaje para que el propio formulario decida qué mostrar.
        private bool ValidarDatos(out string mensajeError)
        {
            mensajeError = null;

            if (string.IsNullOrWhiteSpace(_conceptoActual.ClaveConcepto))
            {
                mensajeError = "La clave del concepto es obligatoria.";
                return false;
            }
            if (_conceptoActual.ClaveConcepto.Length > 10)
            {
                mensajeError = "La clave del concepto no debe exceder 10 caracteres.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(_conceptoActual.Descripcion))
            {
                mensajeError = "La descripción es obligatoria.";
                return false;
            }
            if (_conceptoActual.IdClase == 0)
            {
                mensajeError = "Debe seleccionar una clase para el concepto.";
                return false;
            }
            if (_conceptoActual.RequiereAutorizacion && !_conceptoActual.TipoAutorizacion.HasValue)
            {
                mensajeError = "Debe indicar el tipo de autorización cuando el concepto requiere autorización.";
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtClaveDivisa.Clear();
            txtClaveDivisa.Enabled = true;
            txtNombre.Clear();
            txtNotas.Clear();
            cmbClase.SelectedIndex = -1;
            tgEstatus.Checked = false;
            tgAutorizacion.Checked = false;
            _conceptoActual = null;
        }

        // --- Handlers ya enlazados en el Designer que necesitan implementación ---

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // Botón "home": ajusta a la navegación real de tu ERP.
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: confirmar la función real de este botón antes de programarlo.
            PanelUsuario.Visible = !PanelUsuario.Visible;
        }

        private void btnImrimirConceptos_Click(object sender, EventArgs e)
        {
            // TODO: generar el reporte del catálogo de conceptos.
            ReporteConceptosCobroPago r = new ReporteConceptosCobroPago();
            r.ShowDialog();
        }
    }
}