using Condominios.Clases.ConceptosGlobales;
using PV.Clases.ConceptosGlobalesReembolso;
using PV.Enums;
using PV.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class ConceptosGlobalesPartida : Form
    {
        private IConceptosGlobalesDocumento conceptosDocumento;

        private DBConceptosGlobales conceptosGlobales =
            new DBConceptosGlobales();

        private DataTable catalogoConceptos =
            new DataTable();

        private string folio = string.Empty;
        private string partida = string.Empty;
        private string clase = string.Empty;
        private string estado = "Bloqueado";

        private TipoDocumentoConcepto tipoDocumento;


        // ============================================================
        // CONSTRUCTOR PRINCIPAL
        // ============================================================

        public ConceptosGlobalesPartida(
            string folio,
            string partida,
            string clase,
            string estado,
            TipoDocumentoConcepto tipoDocumento)
        {
            InitializeComponent();

            this.folio = folio;
            this.partida = partida;
            this.clase = clase;
            this.estado = estado;
            this.tipoDocumento = tipoDocumento;

            conceptosDocumento = CrearConceptosDocumento(tipoDocumento);

            // Eventos
            dgvDescuentos.CurrentCellDirtyStateChanged +=
                dgvDescuentos_CurrentCellDirtyStateChanged;

            dgvDescuentos.EditingControlShowing +=
                dgvDescuentos_EditingControlShowing;
        }


        // ============================================================
        // CONSTRUCTOR COMPATIBILIDAD
        // ============================================================
        // Este constructor permite que las partes de tu sistema que
        // actualmente abren la ventana sin especificar tipo de
        // documento sigan funcionando.
        //
        // Por defecto será REEMBOLSO.
        // ============================================================

        public ConceptosGlobalesPartida(
            string folio,
            string partida,
            string clase,
            string estado)
            : this(
                folio,
                partida,
                clase,
                estado,
                TipoDocumentoConcepto.Reembolso)
        {
        }


        // ============================================================
        // CREAR IMPLEMENTACIÓN SEGÚN TIPO DE DOCUMENTO
        // ============================================================

        private IConceptosGlobalesDocumento CrearConceptosDocumento(
            TipoDocumentoConcepto tipo)
        {
            switch (tipo)
            {
                case TipoDocumentoConcepto.Reembolso:

                    return new DBConceptosGloablesReembolso();


                case TipoDocumentoConcepto.Gasto:

                    return new DBConceptosGloablesGasto();


                default:

                    throw new ArgumentException(
                        "El tipo de documento no está soportado.");
            }
        }


        // ============================================================
        // CAMBIO INMEDIATO DEL COMBOBOX
        // ============================================================

        private void dgvDescuentos_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (estado != "Abierto")
                return;

            if (dgvDescuentos.IsCurrentCellDirty &&
                dgvDescuentos.CurrentCell is DataGridViewComboBoxCell &&
                dgvDescuentos.CurrentCell.ColumnIndex == 0)
            {
                dgvDescuentos.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }


        // ============================================================
        // CONFIGURAR COMBOBOX
        // ============================================================

        private void dgvDescuentos_EditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            if (estado != "Abierto")
                return;

            if (dgvDescuentos.CurrentCell == null)
                return;

            if (dgvDescuentos.CurrentCell.ColumnIndex == 0 &&
                e.Control is ComboBox)
            {
                ComboBox combo = e.Control as ComboBox;

                FiltrarComboBoxOpciones(
                    combo,
                    dgvDescuentos.CurrentCell.RowIndex);
            }
        }


        // ============================================================
        // FILTRAR CONCEPTOS YA UTILIZADOS
        // ============================================================

        private void FiltrarComboBoxOpciones(
            ComboBox combo,
            int filaActual)
        {
            List<string> clavesUtilizadas =
                new List<string>();


            for (int i = 0;
                 i < dgvDescuentos.Rows.Count;
                 i++)
            {
                if (i == filaActual ||
                    dgvDescuentos.Rows[i].IsNewRow)
                {
                    continue;
                }

                var cellValue =
                    dgvDescuentos.Rows[i]
                        .Cells["ClaveConceptoG"]
                        .Value;


                if (cellValue != null &&
                    !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    clavesUtilizadas.Add(
                        cellValue.ToString());
                }
            }


            DataTable catalogoFiltrado =
                catalogoConceptos.Clone();


            foreach (DataRow row in catalogoConceptos.Rows)
            {
                string clave =
                    row["Clave"].ToString();


                if (!clavesUtilizadas.Contains(clave))
                {
                    catalogoFiltrado.ImportRow(row);
                }
            }


            combo.DataSource =
                catalogoFiltrado;

            combo.DisplayMember =
                "Nombre";

            combo.ValueMember =
                "Clave";
        }


        // ============================================================
        // CARGAR CONCEPTOS EXISTENTES
        // ============================================================

        private void CargarDescuentos(
            string folio,
            string partida)
        {
            DataTable dt =
                conceptosDocumento.CargarConceptosExistentes(
                    folio,
                    partida,
                    clase);


            foreach (DataRow dr in dt.Rows)
            {
                int row =
                    dgvDescuentos.Rows.Add();


                string claseConcepto =
                    dr[5].ToString();


                dgvDescuentos.Rows[row]
                    .Cells["ClaveConceptoG"]
                    .Value = dr[0].ToString();


                dgvDescuentos.Rows[row]
                    .Cells["Tipo"]
                    .Value = dr[2].ToString();


                dgvDescuentos.Rows[row]
                    .Cells["Valor"]
                    .Value =
                    claseConcepto == "Descuento"
                        ? Convert.ToDecimal(dr[3])
                        : Convert.ToDecimal(dr[4]);


                dgvDescuentos.Rows[row]
                    .Cells["Clase1"]
                    .Value = claseConcepto;
            }
        }


        // ============================================================
        // CARGAR CATÁLOGO
        // ============================================================

        private void CargarCatalogo()
        {
            if (clase == "Descuento")
            {
                catalogoConceptos =
                    conceptosGlobales
                        .CargarConceptosDescuento();
            }
            else
            {
                catalogoConceptos =
                    conceptosGlobales
                        .CargarConceptosImpuesto();
            }


            ClaveConceptoG.DataSource =
                catalogoConceptos;

            ClaveConceptoG.DisplayMember =
                "Nombre";

            ClaveConceptoG.ValueMember =
                "Clave";
        }


        // ============================================================
        // ELIMINAR FILA
        // ============================================================

        private void dgvDescuentos_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (estado != "Abierto")
            {
                MessageBox.Show(
                    "No se pueden eliminar conceptos. " +
                    "El estado actual no permite modificaciones.",
                    "Acción no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            if (e.RowIndex >= 0 &&
                dgvDescuentos.Columns[e.ColumnIndex].Name ==
                "Eliminar")
            {
                dgvDescuentos.Rows.RemoveAt(
                    e.RowIndex);
            }
        }


        // ============================================================
        // CAMBIO DE VALOR DEL DATAGRIDVIEW
        // ============================================================

        private void dgvDescuentos_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (estado != "Abierto")
                return;


            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                e.RowIndex >= dgvDescuentos.Rows.Count)
            {
                return;
            }


            // Solo procesar columna ClaveConceptoG
            if (e.ColumnIndex != 0)
                return;


            var cell =
                dgvDescuentos.Rows[e.RowIndex]
                    .Cells[0]
                    as DataGridViewComboBoxCell;


            if (cell == null ||
                cell.Value == null)
            {
                return;
            }


            string clave =
                cell.Value.ToString();


            // Verificar duplicados
            if (ClaveYaUtilizada(
                clave,
                e.RowIndex))
            {
                MessageBox.Show(
                    "Esta clave ya está siendo utilizada " +
                    "en otra fila. Por favor seleccione una diferente.",
                    "Clave duplicada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                cell.Value = null;

                return;
            }


            // Buscar concepto
            DataRow[] filasEncontradas =
                catalogoConceptos.Select(
                    $"Clave = '{clave.Replace("'", "''")}'");


            if (filasEncontradas.Length == 0)
                return;


            DataGridViewRow row =
                dgvDescuentos.Rows[e.RowIndex];


            if (dgvDescuentos.Columns.Contains("Nombre") ||
                dgvDescuentos.Columns.Count > 1)
            {
                row.Cells[1].Value =
                    filasEncontradas[0]["Nombre"];
            }


            if (dgvDescuentos.Columns.Contains("Clase1") ||
                dgvDescuentos.Columns.Count > 2)
            {
                row.Cells[2].Value =
                    filasEncontradas[0]["Clase"];
            }


            if (dgvDescuentos.Columns.Contains("Tipo") ||
                dgvDescuentos.Columns.Count > 3)
            {
                row.Cells[3].Value =
                    filasEncontradas[0]["Tipo"];
            }


            if (dgvDescuentos.Columns.Contains("Valor") ||
                dgvDescuentos.Columns.Count > 4)
            {
                row.Cells[4].Value =
                    filasEncontradas[0]["Importe"];
            }
        }


        // ============================================================
        // VALIDAR CONCEPTO DUPLICADO
        // ============================================================

        private bool ClaveYaUtilizada(
            string clave,
            int filaActual)
        {
            for (int i = 0;
                 i < dgvDescuentos.Rows.Count;
                 i++)
            {
                if (i == filaActual ||
                    dgvDescuentos.Rows[i].IsNewRow)
                {
                    continue;
                }


                var cellValue =
                    dgvDescuentos.Rows[i]
                        .Cells["ClaveConceptoG"]
                        .Value;


                if (cellValue != null &&
                    cellValue.ToString() == clave)
                {
                    return true;
                }
            }


            return false;
        }


        // ============================================================
        // CONFIGURAR CONTROLES SEGÚN ESTADO
        // ============================================================

        private void ConfigurarControlesPorEstado()
        {
            bool esEditable =
                estado == "Abierto";


            dgvDescuentos.ReadOnly =
                !esEditable;


            dgvDescuentos.AllowUserToAddRows =
                esEditable;


            dgvDescuentos.AllowUserToDeleteRows =
                esEditable;


            if (dgvDescuentos.Columns.Contains("Eliminar"))
            {
                dgvDescuentos.Columns["Eliminar"]
                    .Visible = esEditable;
            }


            if (this.Controls.Find(
                "guna2Button6",
                true).Length > 0)
            {
                Button btnAgregar =
                    (Button)this.Controls.Find(
                        "guna2Button6",
                        true)[0];

                btnAgregar.Enabled =
                    esEditable;
            }


            if (this.Controls.Find(
                "btnAceptar",
                true).Length > 0)
            {
                Button btnAceptar =
                    (Button)this.Controls.Find(
                        "btnAceptar",
                        true)[0];

                btnAceptar.Enabled =
                    esEditable;
            }


            if (!esEditable)
            {
                if (!this.Text.Contains(
                    "SOLO LECTURA"))
                {
                    this.Text +=
                        " - SOLO LECTURA";
                }
            }
        }


        // ============================================================
        // LOAD
        // ============================================================

        private void ConceptosGlobalesPartida_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                CargarCatalogo();

                CargarDescuentos(
                    folio,
                    partida);

                // Si quieres activar la configuración de estado:
                // ConfigurarControlesPorEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los conceptos globales:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // GUARDAR
        // ============================================================

        private void btnAceptar_Click(
            object sender,
            EventArgs e)
        {
            if (estado != "Abierto")
            {
                MessageBox.Show(
                    "No se pueden guardar cambios. " +
                    "El estado actual no permite modificaciones.",
                    "Acción no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            try
            {
                // ====================================================
                // ELIMINAR LOS CONCEPTOS ANTERIORES
                // ====================================================

                conceptosDocumento
                    .EliminarConceptosGlobales(
                        folio,
                        partida,
                        clase);


                // ====================================================
                // INSERTAR LOS CONCEPTOS ACTUALES
                // ====================================================

                foreach (DataGridViewRow row
                    in dgvDescuentos.Rows)
                {
                    if (row.IsNewRow)
                        continue;


                    if (row.Cells["ClaveConceptoG"].Value == null ||
                        row.Cells["Valor"].Value == null)
                    {
                        continue;
                    }


                    string clave =
                        row.Cells["ClaveConceptoG"]
                            .Value
                            .ToString();


                    decimal valor =
                        Convert.ToDecimal(
                            row.Cells["Valor"].Value);


                    if (clase == "Descuento")
                    {
                        conceptosDocumento
                            .InsertarDescuento(
                                clave,
                                folio,
                                partida,
                                valor,
                                0.00m);
                    }
                    else
                    {
                        conceptosDocumento
                            .InsertarDescuento(
                                clave,
                                folio,
                                partida,
                                0.00m,
                                valor);
                    }
                }


                this.DialogResult =
                    DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // AGREGAR CONCEPTO
        // ============================================================

        private void guna2Button6_Click(
            object sender,
            EventArgs e)
        {
            if (estado != "Abierto")
            {
                MessageBox.Show(
                    "No se pueden agregar conceptos. " +
                    "El estado actual no permite modificaciones.",
                    "Acción no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            List<string> clavesUtilizadas =
                new List<string>();


            foreach (DataGridViewRow row
                in dgvDescuentos.Rows)
            {
                if (row.IsNewRow)
                    continue;


                var cellValue =
                    row.Cells["ClaveConceptoG"]
                        .Value;


                if (cellValue != null &&
                    !string.IsNullOrEmpty(
                        cellValue.ToString()))
                {
                    clavesUtilizadas.Add(
                        cellValue.ToString());
                }
            }


            int opcionesDisponibles = 0;


            foreach (DataRow dr
                in catalogoConceptos.Rows)
            {
                if (!clavesUtilizadas.Contains(
                    dr["Clave"].ToString()))
                {
                    opcionesDisponibles++;
                }
            }


            if (opcionesDisponibles == 0)
            {
                MessageBox.Show(
                    "No hay más conceptos disponibles para agregar.",
                    "Sin opciones",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            dgvDescuentos.Rows.Add();
        }


        // ============================================================
        // CAMBIAR ESTADO
        // ============================================================

        public void CambiarEstado(
            string nuevoEstado)
        {
            this.estado =
                nuevoEstado;

            ConfigurarControlesPorEstado();
        }
    }
}