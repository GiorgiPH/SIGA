using Condominios.Clases.ConceptosGlobales;
using PV.Clases.ConceptosGlobalesReembolso;
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

namespace PV
{
    public partial class ConceptosGlobalesPartida : Form
    {
        DBConceptosGloablesReembolso C = new DBConceptosGloablesReembolso();
        DBConceptosGlobales conceptosGlobales = new DBConceptosGlobales();
        DataTable catalogoConeptos = new DataTable();
        string folio = string.Empty;
        string partida = string.Empty;
        string clase = string.Empty;
        string estado = "Bloqueado";

        public ConceptosGlobalesPartida(string folio, string partida, string clase, string estado)
        {
            InitializeComponent();
            this.folio = folio;
            this.partida = partida;
            this.clase = clase;
            this.estado = estado;

            // Agregar eventos
            dgvDescuentos.CurrentCellDirtyStateChanged += dgvDescuentos_CurrentCellDirtyStateChanged;
            dgvDescuentos.EditingControlShowing += dgvDescuentos_EditingControlShowing;
        }

        // Nuevo evento para manejar cambios inmediatos en ComboBox
        private void dgvDescuentos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Verificar si el estado permite edición
            if (estado != "Abierto") return;

            // Solo procesar si la celda actual es un ComboBox y está "dirty" (modificada)
            if (dgvDescuentos.IsCurrentCellDirty &&
                dgvDescuentos.CurrentCell is DataGridViewComboBoxCell &&
                dgvDescuentos.CurrentCell.ColumnIndex == 0) // Primera columna (ClaveConceptoG)
            {
                // Confirmar el cambio inmediatamente
                dgvDescuentos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Evento para configurar el ComboBox cuando se está editando
        private void dgvDescuentos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Verificar si el estado permite edición
            if (estado != "Abierto") return;

            if (dgvDescuentos.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
            {
                ComboBox combo = e.Control as ComboBox;

                // Filtrar las opciones del ComboBox para excluir claves ya utilizadas
                FiltrarComboBoxOpciones(combo, dgvDescuentos.CurrentCell.RowIndex);
            }
        }

        // Método para filtrar las opciones del ComboBox
        private void FiltrarComboBoxOpciones(ComboBox combo, int filaActual)
        {
            // Obtener las claves ya utilizadas en otras filas
            List<string> clavesUtilizadas = new List<string>();

            for (int i = 0; i < dgvDescuentos.Rows.Count; i++)
            {
                // Saltar la fila actual y las filas nuevas
                if (i == filaActual || dgvDescuentos.Rows[i].IsNewRow) continue;

                var cellValue = dgvDescuentos.Rows[i].Cells["ClaveConceptoG"].Value;
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    clavesUtilizadas.Add(cellValue.ToString());
                }
            }

            // Crear una copia filtrada del catálogo
            DataTable catalogoFiltrado = catalogoConeptos.Clone();

            foreach (DataRow row in catalogoConeptos.Rows)
            {
                string clave = row["Clave"].ToString();
                // Solo agregar si la clave no está siendo utilizada
                if (!clavesUtilizadas.Contains(clave))
                {
                    catalogoFiltrado.ImportRow(row);
                }
            }

            // Configurar el ComboBox con los datos filtrados
            combo.DataSource = catalogoFiltrado;
            combo.DisplayMember = "Nombre";
            combo.ValueMember = "Clave";
        }

        private void CargarDescuentos(string folio, string partida)
        {
            DataTable dt = C.CargarConceptosExistentes(folio, partida, clase);

            foreach (DataRow dr in dt.Rows)
            {
                int row = dgvDescuentos.Rows.Add();
                string claseConcepto = dr[5].ToString();
                dgvDescuentos.Rows[row].Cells["ClaveConceptoG"].Value = dr[0].ToString();
                dgvDescuentos.Rows[row].Cells["Tipo"].Value = dr[2].ToString();
                dgvDescuentos.Rows[row].Cells["Valor"].Value = claseConcepto == "Descuento" ? Convert.ToDecimal(dr[3]) : Convert.ToDecimal(dr[4]);
                dgvDescuentos.Rows[row].Cells["Clase1"].Value = claseConcepto;
            }
        }

        private void CargarCatalogo()
        {
            catalogoConeptos = clase == "Descuento" ? conceptosGlobales.CargarConceptosDescuento() : conceptosGlobales.CargarConceptosImpuesto();

            ClaveConceptoG.DataSource = catalogoConeptos;
            ClaveConceptoG.DisplayMember = "Nombre";
            ClaveConceptoG.ValueMember = "Clave";
        }

        private void dgvDescuentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el estado permite eliminación
            if (estado != "Abierto")
            {
                MessageBox.Show("No se pueden eliminar conceptos. El estado actual no permite modificaciones.",
                              "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.RowIndex >= 0 && dgvDescuentos.Columns[e.ColumnIndex].Name == "Eliminar")
                dgvDescuentos.Rows.RemoveAt(e.RowIndex);
        }

        private void dgvDescuentos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el estado permite edición
            if (estado != "Abierto") return;

            // Validar que los índices sean válidos
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.RowIndex < dgvDescuentos.Rows.Count)
            {
                // Solo procesar cambios en la primera columna (ClaveConceptoG)
                if (e.ColumnIndex == 0)
                {
                    var cell = dgvDescuentos.Rows[e.RowIndex].Cells[0] as DataGridViewComboBoxCell;
                    if (cell?.Value == null) return;

                    string clave = cell.Value.ToString();

                    // Verificar si la clave ya está siendo utilizada en otra fila
                    if (ClaveYaUtilizada(clave, e.RowIndex))
                    {
                        MessageBox.Show("Esta clave ya está siendo utilizada en otra fila. Por favor seleccione una diferente.",
                                      "Clave duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Limpiar la selección
                        cell.Value = null;
                        return;
                    }

                    // Buscar en el catálogo de conceptos
                    DataRow[] filasEncontradas = catalogoConeptos.Select($"Clave = '{clave}'");
                    if (filasEncontradas.Length > 0)
                    {
                        var row = dgvDescuentos.Rows[e.RowIndex];

                        // Llenar las celdas con los datos encontrados
                        // Asegúrate de que los nombres de las columnas coincidan con tu DataGridView
                        if (dgvDescuentos.Columns.Contains("Nombre") || dgvDescuentos.Columns.Count > 1)
                            row.Cells[1].Value = filasEncontradas[0]["Nombre"];

                        if (dgvDescuentos.Columns.Contains("Clase1") || dgvDescuentos.Columns.Count > 2)
                            row.Cells[2].Value = filasEncontradas[0]["Clase"];

                        if (dgvDescuentos.Columns.Contains("Tipo") || dgvDescuentos.Columns.Count > 3)
                            row.Cells[3].Value = filasEncontradas[0]["Tipo"];

                        if (dgvDescuentos.Columns.Contains("Valor") || dgvDescuentos.Columns.Count > 4)
                            row.Cells[4].Value = filasEncontradas[0]["Importe"];
                    }
                }
            }
        }

        // Método para verificar si una clave ya está siendo utilizada
        private bool ClaveYaUtilizada(string clave, int filaActual)
        {
            for (int i = 0; i < dgvDescuentos.Rows.Count; i++)
            {
                // Saltar la fila actual y las filas nuevas
                if (i == filaActual || dgvDescuentos.Rows[i].IsNewRow) continue;

                var cellValue = dgvDescuentos.Rows[i].Cells["ClaveConceptoG"].Value;
                if (cellValue != null && cellValue.ToString() == clave)
                {
                    return true;
                }
            }
            return false;
        }

        // Método para configurar controles según el estado
        private void ConfigurarControlesPorEstado()
        {
            bool esEditable = (estado == "Abierto");

            // Configurar DataGridView
            dgvDescuentos.ReadOnly = !esEditable;
            dgvDescuentos.AllowUserToAddRows = esEditable;
            dgvDescuentos.AllowUserToDeleteRows = esEditable;

            // Si no es editable, ocultar o deshabilitar la columna de eliminar
            if (dgvDescuentos.Columns.Contains("Eliminar"))
            {
                dgvDescuentos.Columns["Eliminar"].Visible = esEditable;
            }

            // Configurar botón de agregar (asumiendo que el botón se llama guna2Button6)
            if (this.Controls.Find("guna2Button6", true).Length > 0)
            {
                Button btnAgregar = (Button)this.Controls.Find("guna2Button6", true)[0];
                btnAgregar.Enabled = esEditable;
            }

            // Configurar botón Aceptar
            if (this.Controls.Find("btnAceptar", true).Length > 0)
            {
                Button btnAceptar = (Button)this.Controls.Find("btnAceptar", true)[0];
                btnAceptar.Enabled = esEditable;
            }

            // Opcional: Cambiar el texto del título o agregar una etiqueta de estado
            if (!esEditable)
            {
                this.Text += " - SOLO LECTURA";
            }
        }

        private void ConceptosGlobalesPartida_Load(object sender, EventArgs e)
        {
            CargarCatalogo();
            CargarDescuentos(folio, partida);
            //ConfigurarControlesPorEstado(); // Configurar controles según el estado
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificar si el estado permite guardar
            if (estado != "Abierto")
            {
                MessageBox.Show("No se pueden guardar cambios. El estado actual no permite modificaciones.",
                              "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                C.EliminarConceptosGlobales(folio, partida, clase);

                foreach (DataGridViewRow row in dgvDescuentos.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Verificar que los valores no sean nulos antes de convertir
                    if (row.Cells["ClaveConceptoG"].Value == null ||
                        row.Cells["Valor"].Value == null) continue;

                    string clave = row.Cells["ClaveConceptoG"].Value.ToString();
                    decimal valor = Convert.ToDecimal(row.Cells["Valor"].Value);

                    if (clase == "Descuento")
                    {
                        C.InsertarDescuento(clave, folio, partida, valor, 0.00m);
                    }
                    else
                    {
                        C.InsertarDescuento(clave, folio, partida, 0.00m, valor);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Verificar si el estado permite agregar
            if (estado != "Abierto")
            {
                MessageBox.Show("No se pueden agregar conceptos. El estado actual no permite modificaciones.",
                              "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Verificar si quedan opciones disponibles antes de agregar una nueva fila
            List<string> clavesUtilizadas = new List<string>();

            foreach (DataGridViewRow row in dgvDescuentos.Rows)
            {
                if (row.IsNewRow) continue;

                var cellValue = row.Cells["ClaveConceptoG"].Value;
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    clavesUtilizadas.Add(cellValue.ToString());
                }
            }

            // Contar cuántas opciones quedan disponibles
            int opcionesDisponibles = 0;
            foreach (DataRow dr in catalogoConeptos.Rows)
            {
                if (!clavesUtilizadas.Contains(dr["Clave"].ToString()))
                {
                    opcionesDisponibles++;
                }
            }

            if (opcionesDisponibles == 0)
            {
                MessageBox.Show("No hay más conceptos disponibles para agregar.",
                              "Sin opciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvDescuentos.Rows.Add();
        }

        // Método opcional para cambiar el estado dinámicamente si es necesario
        public void CambiarEstado(string nuevoEstado)
        {
            this.estado = nuevoEstado;
            ConfigurarControlesPorEstado();
        }
    }
}