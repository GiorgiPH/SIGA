using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PV.Clases;
using PV.Clases.Clientes;
using PV.Clases.ConceptoPago;
using PV.Clases.PedidoCliente;
using PV.Clases.Prestamos;
using PV.Clases.Proveedores;
using PV.dto.Prestamos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Registro de Préstamos Bancarios. Adaptado de Facturas.cs (que a su vez
    /// venía de OrdenPedidoCliente.cs), pero se retiró toda la lógica que no
    /// aplica al dominio de préstamos:
    ///   - Ya NO se selecciona ningún producto/servicio en las partidas: cada
    ///     partida es una parcialidad del calendario de pagos, generada
    ///     automáticamente (no se capturan una por una).
    ///   - Ya NO existen Centro de Costos / Proyecto / Almacenes / Autorización.
    ///   - El concepto (tanto de Capital como de Interés) ya no sale de
    ///     Servicios, sino del catálogo de Conceptos de Cobro/Pago
    ///     (DBConceptoCobroPago.ListarParaCombo), filtrado por la clase
    ///     IdClase = 7 (conceptos de Préstamos).
    ///
    /// Reglas de negocio clave de la parte de Partidas:
    ///   a) Al generar el calendario se crean exactamente [Parcialidades]
    ///      partidas idénticas entre sí; solo cambian Vencimiento (calculado
    ///      por el sistema) y el número de Parcialidad (generado por el
    ///      sistema).
    ///   b) Una vez generado el calendario, en cada partida SOLO puede
    ///      modificarse el campo Vencimiento, y el nuevo valor debe ser
    ///      mayor o igual a la Fecha del documento (ver
    ///      DBPrestamos.ActualizarVencimientoPartida).
    /// </summary>
    public partial class Prestamos : Form
    {
        // Acceso a datos propio de Préstamos.
        DBPrestamos db = new DBPrestamos();

        // Catálogo de Conceptos de Cobro/Pago, tal cual lo entregaste.
        DBConceptoCobroPago dbConceptos = new DBConceptoCobroPago();

        // Infraestructura ya existente que se reutiliza tal cual para el
        // catálogo de Acreedores (reutiliza el mismo catálogo de Clientes /
        // buscador que usaba Facturas para Cliente/Proveedor).
        DBProveedores cl = new DBProveedores();
        DBPedidoCliente c = new DBPedidoCliente();

        /// <summary>
        /// IdClase de ConceptoCobroPago que agrupa los conceptos de
        /// Préstamos (Capital e Interés). Indicado explícitamente: 7.
        /// </summary>
        private const byte IdClaseConceptosPrestamo = 7;

        /// <summary>
        /// Tasa de IVA aplicada al interés de cada parcialidad cuando
        /// AplicaImpuesto = true. 16% reproduce el ejemplo del mock-up
        /// (Interés $3,200.00 -> IVA $512.00). Muévela a configuración si
        /// llega a variar por tipo de préstamo o cambia con el tiempo.
        /// </summary>
        private const decimal TasaIVA = 0.16m;

        /// <summary>PK real del préstamo actualmente en pantalla (dbo.Prestamo.Folio). Null = aún no guardado.</summary>
        private int? folioActual = null;

        /// <summary>Catálogo de conceptos (IdClase = 7) cacheado para no volver a consultarlo en cada evento.</summary>
        private DataTable dtConceptosCobro = null;
        private DataTable dtConceptosPago = null;


        /// <summary>Partidas ya generadas del préstamo actual, cargadas en memoria para navegar Anterior/Siguiente sin ir a BD en cada clic.</summary>
        private List<PartidaPrestamoData> partidasEnMemoria = new List<PartidaPrestamoData>();
        private int indicePartidaActual = -1;

        public Prestamos()
        {
            InitializeComponent();

            lblTitulo.Text = "PRESTAMOS";

            // Las partidas de un préstamo no se agregan una por una: se
            // generan todas juntas desde "Generar Calendario Pagos". El "+"
            // heredado del Designer se deja oculto (ver btnAgregarPartida_Click).
            btnAgregarPartida.Visible = false;

            guna2GradientPanel2.Visible = false;

            // Reportes/XML/correo no aplican todavía a Préstamos (no existe
            // un DBPrestamos.GenerarXml ni un ReportePrestamo): se ocultan en
            // vez de dejar botones que truenan al usarse.
            button11.Visible = false;       // Enviar correo
            btnRemisionXML.Visible = false; // Generar XML

        }

        #region Ciclo de vida del formulario

        private void Prestamos_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaConsulta(dgvAutorizados);
            ConfigurarGrillaPartidas(guna2DataGridView1);

            // Documento: por ahora un único tipo (el del mock-up). Si más
            // adelante existen varios tipos de documento para Préstamos,
            // sustituye este bloque por la carga desde el catálogo real
            // (como hacía Facturas con su combo de Documento).
            cmbDocumento.Items.Clear();
            cmbDocumento.Items.Add("PRB - PRESTAMO BANCARIO");
            cmbDocumento.SelectedIndex = 0;

            CargarCombosConcepto();

            ConfigurarToolStripExpandido();
            RecargarGrillaConsulta();

            LimpiarEncabezado();
        }

        private void Prestamos_Activated(object sender, EventArgs e)
        {
            if (folioActual.HasValue)
            {
                ReciboSaldos(folioActual.Value);
            }
        }

        private void Prestamos_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cada método de DBPrestamos abre y cierra su propia conexión:
            // no hay ningún recurso persistente que liberar aquí.
        }

        #endregion

        #region Documento / Conceptos (catálogo de Cobro-Pago, IdClase = 7)

        /// <summary>
        /// Carga los combos de Concepto del ENCABEZADO (Capital e Interés)
        /// desde dbo.ConceptoCobroPago, usando el método que indicaste:
        /// ListarParaCombo(idClase = 7). Ambos combos muestran solo la
        /// clave corta (el ancho del control en el Designer es de 70px);
        /// cmbConceptoPrestamo además refleja la descripción larga en
        /// txtDescConceptoPrestamo, tal como en el mock-up. cmbConceptoInteres
        /// no tiene un textbox de descripción propio en el encabezado (el
        /// Designer no trae uno para ese campo).
        /// </summary>
        private void CargarCombosConcepto()
        {
            dtConceptosCobro = dbConceptos.ListarParaCombo(2);
            dtConceptosPago = dbConceptos.ListarParaCombo(3);


            ComboUtil.LlenarComboBox(cmbConceptoPrestamo, dtConceptosCobro.Copy(), "Descripcion", "IdConcepto");
            ComboUtil.LlenarComboBox(cmbConceptoInteres, dtConceptosPago.Copy(), "Descripcion", "IdConcepto");

            cmbConceptoPrestamo.SelectedIndex = -1;
            cmbConceptoInteres.SelectedIndex = -1;
        }

        /// <summary>Misma fuente (IdClase = 7) para los combos de concepto -ya resueltos y deshabilitados- dentro del panel de partida.</summary>
        private void CargarCombosConceptoPartida()
        {
            DataTable dtCobro = dtConceptosCobro ?? dbConceptos.ListarParaCombo(2);
            DataTable dtPago = dtConceptosPago ?? dbConceptos.ListarParaCombo(3);

            ComboUtil.LlenarComboBox(cmbConceptoCapitalPartida, dtCobro.Copy(), "Descripcion", "IdConcepto");
            ComboUtil.LlenarComboBox(cmbConceptoInteresPartida, dtPago.Copy(), "Descripcion", "IdConcepto");
        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Con un solo tipo de documento disponible no hay nada más que
            // resolver aquí. El evento se deja enganchado (el Designer lo
            // espera) por si más adelante se agregan más tipos de documento
            // y hay que recalcular el consecutivo al cambiarlo.
        }

        private void cmbConceptoPrestamo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbConceptoInteres_SelectedIndexChanged(object sender, EventArgs e)
        {
            // El encabezado no tiene un campo de descripción propio para el
            // concepto de interés (solo el combo, según el mock-up). Se deja
            // el evento enganchado por si se agrega ese control más adelante.
        }

        #endregion

        #region Encabezado - captura / limpieza

        private void LimpiarEncabezado()
        {
            folioActual = null;
            partidasEnMemoria.Clear();
            indicePartidaActual = -1;

            txtFolio.Clear();
            cmbEstatus.SelectedIndex = 0; // Abierto
            cmbDocumento.Enabled = true;
            if (cmbDocumento.Items.Count > 0) cmbDocumento.SelectedIndex = 0;

            txtFecha.Enabled = true;
            txtFecha.Value = DateTime.Today;
            txtDiasVence.Text = "0";
            RecalcularFechaVencimiento();

            txtClaveAcreedor.Clear();
            txtNombreAcreedor.Clear();

            cmbConceptoPrestamo.SelectedIndex = -1;
            cmbConceptoInteres.SelectedIndex = -1;

            txtReferencia.Clear();
            txtParcialidades.Clear();
            txtFechaPrimerPago.Value = DateTime.Today;
            txtPeriodicidad.Text = "30";
            txtImporteCPago.Text = "0.00";
            txtInteresCPago.Text = "0.00";

            // txtImpuesto está deshabilitado (solo lectura): por ahora se
            // asume que el préstamo SÍ causa IVA sobre el interés, que es el
            // escenario del mock-up. Si el IVA debe depender del concepto o
            // de una configuración distinta, aquí es donde hay que resolverlo.

            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";

            txtSubtotal.Text = "0.00";
            txtIntereses.Text = "0.00";
            txtImpuestos.Text = "0.00";
            txtTotal.Text = "0.00";
            txtSaldo.Text = "0.00";

            txtNotas.Clear();
            txtElaborado.Text = DBLogin.usuario;
            txtRutaDocumento.Clear();

            PanelPartidasRequisicion.Visible = false;
            guna2DataGridView1.DataSource = null;
            btnTerminarPrestamo.Visible = false;
        }

        private bool ValidarEncabezado()
        {
            if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el documento para continuar.");
                return false;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible modificar un préstamo que no está Abierto.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtClaveAcreedor.Text))
            {
                MessageBox.Show("Registre al acreedor antes de continuar.");
                return false;
            }
            if (cmbConceptoPrestamo.SelectedValue == null)
            {
                MessageBox.Show("Seleccione el concepto de capital.");
                return false;
            }
            if (cmbConceptoInteres.SelectedValue == null)
            {
                MessageBox.Show("Seleccione el concepto de interés.");
                return false;
            }
            if (!int.TryParse(txtParcialidades.Text, out int parcialidades) || parcialidades <= 0)
            {
                MessageBox.Show("Registre un número de parcialidades válido.");
                return false;
            }
            if (!int.TryParse(txtPeriodicidad.Text, out int periodicidad) || periodicidad <= 0)
            {
                MessageBox.Show("Registre una periodicidad (días) válida.");
                return false;
            }
            if (!decimal.TryParse(txtImporteCPago.Text, out decimal importe) || importe <= 0)
            {
                MessageBox.Show("Registre el importe de capital por pago.");
                return false;
            }
            if ((!decimal.TryParse(txtInteresCPago.Text, out decimal interes) && interes > 0) && cmbConceptoInteres.SelectedIndex==-1)
            {
                MessageBox.Show("Registre el concetpo de interés.");
                return false;
            }
            if (txtFechaPrimerPago.Value.Date < txtFecha.Value.Date)
            {
                MessageBox.Show("La fecha del primer pago no puede ser anterior a la fecha del documento.");
                return false;
            }

            return true;
        }

        private PrestamoData ConstruirPrestamoDataDesdeControles()
        {
            return new PrestamoData
            {
                ClaveDocumento = "PRB", // único documento disponible por ahora (ver cmbDocumento_SelectedIndexChanged)
                Estatus = "Abierto",
                Fecha = txtFecha.Value.Date,
                ClaveAcreedor = Convert.ToInt32(txtClaveAcreedor.Text),
                IdConceptoCapital = (int?)cmbConceptoPrestamo.SelectedValue,
                Referencia = txtReferencia.Text,
                Parcialidades = Convert.ToInt32(txtParcialidades.Text),
                FechaPrimerPago = txtFechaPrimerPago.Value.Date,
                Periodicidad = Convert.ToInt32(txtPeriodicidad.Text),
                ImporteCPago = Convert.ToDecimal(txtImporteCPago.Text),
                InteresCPago = Convert.ToDecimal(txtInteresCPago.Text),
                IdConceptoInteres = (int?)cmbConceptoInteres.SelectedValue,
                AplicaImpuesto = tgImpuesto.Checked,
                Divisa = txtDivisa.Text,
                TipoCambio = Convert.ToDecimal(txtTipoCambio.Text),
                Notas = txtNotas.Text,
                Elaborado = txtElaborado.Text,
                RutaDocumento = txtRutaDocumento.Text,
                DiasVence = Convert.ToInt32(string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text),
                FechaVence = string.IsNullOrEmpty(txtFechaVence.Text) ? (DateTime?)null : Convert.ToDateTime(txtFechaVence.Text)
            };
        }

        private void ReciboSaldos(int folio)
        {
            PrestamoData dato = db.ObtenerPorFolio(folio);
            if (dato == null) return;

            txtSubtotal.Text = dato.Subtotal.ToString("N2");
            txtIntereses.Text = dato.Intereses.ToString("N2");
            txtImpuestos.Text = dato.Impuestos.ToString("N2");
            txtTotal.Text = dato.Total.ToString("N2");
            txtSaldo.Text = dato.Saldo.ToString("N2");
        }

        #endregion

        #region Generar Calendario Pagos / Terminar / Cancelar / Nuevo

        /// <summary>
        /// Guarda (o actualiza) el encabezado y genera el calendario de
        /// pagos completo: exactamente [Parcialidades] partidas, todas
        /// idénticas salvo Vencimiento y el número de Parcialidad (ambos
        /// calculados por el sistema). Si ya existía un calendario para este
        /// préstamo, se pide confirmación para reemplazarlo por completo.
        /// </summary>
        private void btnGenerarCalendarioPagos_Click(object sender, EventArgs e)
        {
            if (!ValidarEncabezado())
                return;

            if (!folioActual.HasValue)
            {
                PrestamoData dato = ConstruirPrestamoDataDesdeControles();
                dato.Consecutivo = db.ObtenerSiguienteConsecutivo(dato.ClaveDocumento);
                folioActual = db.InsertarPrestamo(dato);

                txtFolio.Text = (dato.Consecutivo ?? folioActual.Value).ToString();
                cmbDocumento.Enabled = false;
                txtFecha.Enabled = false;
            }
            else if (db.ExistenPartidas(folioActual.Value))
            {
                if (MessageBox.Show(
                        "Ya existe un calendario de pagos generado para este préstamo. ¿Desea reemplazarlo?",
                        "Calendario de pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            decimal interesParcialidad = Convert.ToDecimal(txtInteresCPago.Text);
            bool aplicaImpuesto = tgImpuesto.Checked;
            decimal iva = aplicaImpuesto ? Math.Round(interesParcialidad * TasaIVA, 2) : 0m;

            db.GenerarCalendarioPagos(
                folioActual.Value,
                Convert.ToInt32(txtParcialidades.Text),
                txtFechaPrimerPago.Value.Date,
                Convert.ToInt32(txtPeriodicidad.Text),
                Convert.ToDecimal(txtImporteCPago.Text),
                (int?)cmbConceptoPrestamo.SelectedValue,
                interesParcialidad,
                iva,
                (int?)cmbConceptoInteres.SelectedValue);

            MessageBox.Show("Calendario de pagos generado correctamente.");

            ReciboSaldos(folioActual.Value);
            CargarPartidas();
            CargarPartidasEnMemoriaYMostrarPrimera();

            guna2TabControl1.SelectedIndex = 1;
            btnTerminarPrestamo.Visible = true;
        }

        private void btnTerminarPrestamo_Click(object sender, EventArgs e)
        {
            if (!folioActual.HasValue)
                return;

            if (partidasEnMemoria.Count == 0)
            {
                MessageBox.Show("Genera el calendario de pagos antes de continuar.");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Noes posible confirmar un préstamo que no está Abierto.");
                return;
            }
            if (MessageBox.Show("Al confirmar el préstamo no podrá realizar modificaciones, ¿Desea continuar?",
                    "Préstamo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            db.ActualizarEstatus(folioActual.Value, "Bloqueado");
            MessageBox.Show("El préstamo se confirmó exitosamente.");
            if (MessageBox.Show("¿Imprimir Documento?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ReporteComprobantePrestamo r = new ReporteComprobantePrestamo(txtFolio.Text);
                r.ShowDialog();
            }

            LimpiarEncabezado();
            RecargarGrillaConsulta();
            guna2TabControl1.SelectedIndex = 0;
        }

        private void btnCancelarPrestamo_Click(object sender, EventArgs e)
        {
            if (!folioActual.HasValue)
            {
                LimpiarEncabezado();
                return;
            }

            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible cancelar un préstamo que no está Abierto.");
                return;
            }

            if (MessageBox.Show("El préstamo será cancelado, ¿Desea continuar?", "Préstamo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                db.ActualizarEstatus(folioActual.Value, "Cancelado");
                LimpiarEncabezado();
                RecargarGrillaConsulta();
            }
        }

        private void btnNuevoPrestamo_Click(object sender, EventArgs e)
        {
            if (folioActual.HasValue && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nuevo Préstamo",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            LimpiarEncabezado();
            guna2TabControl1.SelectedIndex = 0;
        }

        #endregion

        #region Partidas - navegación y edición (solo Vencimiento es modificable)

        private void ConfigurarGrillaPartidas(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Partida",
                DataPropertyName = "Partida",
                HeaderText = "Parcialidad",
                Width = 90
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Vencimiento",
                DataPropertyName = "Vencimiento",
                HeaderText = "Vencimiento",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy/MM/dd" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ImporteCapital",
                DataPropertyName = "ImporteCapital",
                HeaderText = "Importe Capital",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InteresParcialidad",
                DataPropertyName = "InteresParcialidad",
                HeaderText = "Interés",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Iva",
                DataPropertyName = "Iva",
                HeaderText = "IVA",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 110,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                DataPropertyName = "Estatus",
                HeaderText = "Estatus",
                Width = 90
            });
        }

        private void CargarPartidas()
        {
            if (!folioActual.HasValue)
            {
                guna2DataGridView1.DataSource = null;
                return;
            }
            guna2DataGridView1.DataSource = db.ListarPartidas(folioActual.Value);
        }

        private void CargarPartidasEnMemoriaYMostrarPrimera()
        {
            if (!folioActual.HasValue)
            {
                partidasEnMemoria.Clear();
                indicePartidaActual = -1;
                MostrarPartidaActual();
                return;
            }

            partidasEnMemoria = db.ObtenerPartidas(folioActual.Value);
            indicePartidaActual = partidasEnMemoria.Count > 0 ? 0 : -1;

            if (cmbConceptoCapitalPartida.Items.Count == 0)
                CargarCombosConceptoPartida();

            MostrarPartidaActual();
        }

        private void MostrarPartidaActual()
        {
            if (indicePartidaActual < 0 || indicePartidaActual >= partidasEnMemoria.Count)
            {
                PanelPartidasRequisicion.Visible = false;
                return;
            }

            var p = partidasEnMemoria[indicePartidaActual];
            PanelPartidasRequisicion.Visible = true;

            txtPartida.Text = p.Partida.ToString();
            txtVencimientoPartida.Value = p.Vencimiento;
            txtParcialidad.Text = $"{p.Partida}/{p.TotalParcialidades}";
            txtImporteCapital.Text = p.ImporteCapital.ToString("N2");
            txtDescConceptoCapitalPartida.Text = p.DescripcionConceptoCapital;
            txtInteresParcialidad.Text = p.InteresParcialidad.ToString("N2");
            txtIvaParcialidad.Text = p.Iva.ToString("N2");
            txtDescConceptoInteresPartida.Text = p.DescripcionConceptoInteres;
            txtTotalPartida.Text = p.Total.ToString("N2");

            SeleccionarValorCombo(cmbConceptoCapitalPartida, p.IdConceptoCapital);
            SeleccionarValorCombo(cmbConceptoInteresPartida, p.IdConceptoInteres);

            ConfigurarPartida();

            btnAnteriorPartida.Enabled = indicePartidaActual > 0;
            btnSiguientePartida.Enabled = indicePartidaActual < partidasEnMemoria.Count - 1;
        }

        private void SeleccionarValorCombo(Guna2ComboBox combo, int? valor)
        {
            if (valor.HasValue && combo.Items.Count > 0)
                combo.SelectedValue = valor.Value;
            else
                combo.SelectedIndex = -1;
        }

        /// <summary>
        /// Regla de negocio (b): una vez generado el calendario, en cada
        /// partida SOLO puede modificarse Vencimiento. El resto de los
        /// campos ya vienen deshabilitados desde el Designer (No. Partida,
        /// Parcialidad, Conceptos, IVA, Total); aquí solo se asegura que
        /// Importe Capital e Interés Parcialidad -habilitados por defecto en
        /// el Designer- también queden bloqueados.
        /// </summary>
        private void ConfigurarPartida()
        {
            bool puedeEditarVencimiento = cmbEstatus.Text == "Abierto";

            txtVencimientoPartida.Enabled = puedeEditarVencimiento;
            txtImporteCapital.Enabled = false;
            txtInteresParcialidad.Enabled = false;
            cmbConceptoCapitalPartida.Enabled = false;
            cmbConceptoInteresPartida.Enabled = false;

            btnConfirmarPartida.Visible = puedeEditarVencimiento;
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || !folioActual.HasValue)
                return;

            int partida = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value);
            int indice = partidasEnMemoria.FindIndex(x => x.Partida == partida);
            if (indice == -1)
            {
                // La lista en memoria podría estar desactualizada (por ejemplo
                // si se llegó directo a esta pestaña); se refresca una vez.
                CargarPartidasEnMemoriaYMostrarPrimera();
                indice = partidasEnMemoria.FindIndex(x => x.Partida == partida);
            }

            indicePartidaActual = indice;
            MostrarPartidaActual();
        }

        private void btnAnteriorPartida_Click(object sender, EventArgs e)
        {
            if (indicePartidaActual > 0)
            {
                indicePartidaActual--;
                MostrarPartidaActual();
            }
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (indicePartidaActual < partidasEnMemoria.Count - 1)
            {
                indicePartidaActual++;
                MostrarPartidaActual();
            }
        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (indicePartidaActual < 0 || !folioActual.HasValue)
                return;

            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible modificar partidas de un préstamo que no está Abierto.");
                return;
            }

            var partidaActual = partidasEnMemoria[indicePartidaActual];
            DateTime nuevoVencimiento = txtVencimientoPartida.Value.Date;

            string error = db.ActualizarVencimientoPartida(folioActual.Value, partidaActual.Partida, nuevoVencimiento);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Vencimiento inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Vencimiento actualizado correctamente.");

            CargarPartidas();
            int partidaEditada = partidaActual.Partida;
            partidasEnMemoria = db.ObtenerPartidas(folioActual.Value);
            indicePartidaActual = partidasEnMemoria.FindIndex(x => x.Partida == partidaEditada);
            MostrarPartidaActual();
        }

        /// <summary>
        /// Nota de diseño: una parcialidad NO puede eliminarse de forma
        /// individual (rompería la numeración "n/Parcialidades" del
        /// calendario). "Eliminar" actúa sobre el CALENDARIO COMPLETO, para
        /// poder regenerarlo desde cero mientras el préstamo siga Abierto.
        /// Si necesitas poder borrar una sola parcialidad, dímelo y ajusto
        /// esta lógica (y probablemente la numeración) en consecuencia.
        /// </summary>
        private void btnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (!folioActual.HasValue)
                return;

            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible eliminar el calendario de un préstamo que no está Abierto.");
                return;
            }

            if (MessageBox.Show(
                    "Se eliminará el calendario de pagos completo (todas las parcialidades). ¿Desea continuar?",
                    "Calendario de pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            db.EliminarCalendarioPagos(folioActual.Value);
            partidasEnMemoria.Clear();
            indicePartidaActual = -1;

            PanelPartidasRequisicion.Visible = false;
            btnTerminarPrestamo.Visible = false;
            CargarPartidas();
            ReciboSaldos(folioActual.Value);
        }

        /// <summary>
        /// El botón permanece oculto (ver constructor): en Préstamos las
        /// partidas no se agregan una por una, se generan todas juntas
        /// desde "Generar Calendario Pagos". Se deja el handler por si el
        /// control llega a mostrarse de nuevo.
        /// </summary>
        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Las partidas de un préstamo se generan automáticamente: completa el encabezado y usa \"Generar Calendario Pagos\".");
        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {
            // Sin lógica de dibujo personalizada (heredado tal cual).
        }

        #endregion

        #region Vencimiento del documento

        private void RecalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;
                int dias = Convert.ToInt32(diasv);
                DateTime vence = txtFecha.Value.AddDays(dias);
                txtFechaVence.Text = vence.ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de días de vencimiento incorrecto");
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e) => RecalcularFechaVencimiento();
        private void txtFecha_ValueChanged(object sender, EventArgs e) => RecalcularFechaVencimiento();
        private void txtFechaPrimerPago_ValueChanged(object sender, EventArgs e) { /* se valida hasta Generar Calendario Pagos */ }

        #endregion

        #region Formato de moneda / validación de teclas

        private void Moneda(ref Guna2TextBox txt)
        {
            try
            {
                string n = txt.Text.Replace(",", "").Replace(".", "");
                n = n.PadLeft(3, '0');
                double v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N2}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                // Formato temporal mientras el usuario todavía está escribiendo.
            }
        }

        private void SoloDigitos(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtParcialidades_TextChanged(object sender, EventArgs e) { /* los totales se recalculan al generar el calendario, no en vivo */ }
        private void txtParcialidades_KeyPress(object sender, KeyPressEventArgs e) => SoloDigitos(sender, e);
        private void txtPeriodicidad_KeyPress(object sender, KeyPressEventArgs e) => SoloDigitos(sender, e);

        private void txtImporteCPago_TextChanged(object sender, EventArgs e) => Moneda(ref txtImporteCPago);
        private void txtImporteCPago_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtInteresCPago_TextChanged(object sender, EventArgs e) => Moneda(ref txtInteresCPago);
        private void txtInteresCPago_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtImporteCapital_TextChanged(object sender, EventArgs e) => Moneda(ref txtImporteCapital);
        private void txtImporteCapital_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        private void txtInteresParcialidad_TextChanged(object sender, EventArgs e) => Moneda(ref txtInteresParcialidad);
        private void txtInteresParcialidad_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);

        #endregion

        #region Documento adjunto

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Todos los archivos (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaDocumento.Text = ofd.FileName;
                    if (folioActual.HasValue)
                        db.ActualizarDocumentoAdjunto(folioActual.Value, txtRutaDocumento.Text);
                }
            }
        }

        private void btnVerArchivo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaDocumento.Text))
            {
                MessageBox.Show("No hay un documento adjunto.");
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(txtRutaDocumento.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible abrir el archivo: " + ex.Message);
            }
        }

        private void btnEliminarDocumento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaDocumento.Text))
                return;

            if (MessageBox.Show("¿Eliminar el documento adjunto?", "Documento",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtRutaDocumento.Clear();
                if (folioActual.HasValue)
                    db.ActualizarDocumentoAdjunto(folioActual.Value, null);
            }
        }

        #endregion

        #region Acreedor (reutiliza el catálogo/buscador de Clientes ya existente)

        /// <summary>
        /// ⚠️ Asume el mismo esquema que usaba Facturas para su buscador de
        /// Cliente/Proveedor (columna "Matricula1" = clave, "Nombre" =
        /// nombre). No tengo el código de DBClientes/DBPedidoCliente para
        /// confirmarlo: ajusta estos dos DataPropertyName si tu catálogo
        /// real de Acreedores expone otros nombres de columna.
        /// </summary>
       
        private void btnBuscarAcreedor_Click(object sender, EventArgs e)
        {
    

        
                using (var buscador = new BuscarListaProveedores())
                {
                    if (buscador.ShowDialog() == DialogResult.OK)
                    {
                        txtClaveAcreedor.Text = buscador.Matricula;
                        txtNombreAcreedor.Text = buscador.Nombre;
                    }
                }
            
        }

      

       

       


        private void txtNombreAcreedor_TextChanged(object sender, EventArgs e)
        {
            // No-op: es un campo de solo lectura, calculado a partir de la clave.
        }

        #endregion

        #region Consulta / filtros de préstamos (panel de "Filtros")

        private void ConfigurarGrillaConsulta(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Folio",
                DataPropertyName = "Folio",
                HeaderText = "Folio (interno)",
                Visible = false
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Consecutivo",
                DataPropertyName = "Consecutivo",
                HeaderText = "Folio",
                Width = 70
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Documento",
                DataPropertyName = "Documento",
                HeaderText = "Documento",
                Width = 90
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Acreedor",
                DataPropertyName = "Acreedor",
                HeaderText = "Acreedor",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy/MM/dd" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                DataPropertyName = "Estatus",
                HeaderText = "Estatus",
                Width = 90
            });
        }

        private void RecargarGrillaConsulta()
        {
            dgvAutorizados.DataSource = db.ListarPrestamos(txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroPorAcreedor.Text);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e) => RecargarGrillaConsulta();
        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e) => RecargarGrillaConsulta();
        private void txtFiltroPorAcreedor_TextChanged(object sender, EventArgs e) => RecargarGrillaConsulta();

        private void dgvAutorizados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            int folio = Convert.ToInt32(dgvAutorizados.Rows[e.RowIndex].Cells["Folio"].Value);
            CargarEncabezadoDesdePrestamo(folio);
            guna2GradientPanel2.Visible = false;
        }

        private void CargarEncabezadoDesdePrestamo(int folio)
        {
            PrestamoData dato = db.ObtenerPorFolio(folio);
            if (dato == null) return;

            folioActual = dato.Folio;

            if (cmbDocumento.Items.Count > 0) cmbDocumento.SelectedIndex = 0;
            cmbEstatus.Text = dato.Estatus;
            txtFolio.Text = (dato.Consecutivo ?? dato.Folio).ToString();
            txtFecha.Value = dato.Fecha;
            txtDiasVence.Text = dato.DiasVence.ToString();
            txtFechaVence.Text = dato.FechaVence?.ToString("yyyy/MM/dd") ?? string.Empty;

            txtClaveAcreedor.Text = dato.ClaveAcreedor.ToString();
            txtNombreAcreedor.Text = dato.NombreAcreedor;

            SeleccionarValorCombo(cmbConceptoPrestamo, dato.IdConceptoCapital);
            SeleccionarValorCombo(cmbConceptoInteres, dato.IdConceptoInteres);

            txtReferencia.Text = dato.Referencia;
            txtParcialidades.Text = dato.Parcialidades.ToString();
            txtFechaPrimerPago.Value = dato.FechaPrimerPago;
            txtPeriodicidad.Text = dato.Periodicidad.ToString();
            txtImporteCPago.Text = dato.ImporteCPago.ToString("N2");
            txtInteresCPago.Text = dato.InteresCPago.ToString("N2");
            tgImpuesto.Checked = dato.AplicaImpuesto;
            txtDivisa.Text = dato.Divisa;
            txtTipoCambio.Text = dato.TipoCambio.ToString("N2");
            txtNotas.Text = dato.Notas;
            txtElaborado.Text = dato.Elaborado;
            txtRutaDocumento.Text = dato.RutaDocumento;

            ReciboSaldos(folio);
            CargarPartidas();
            CargarPartidasEnMemoriaYMostrarPrimera();

            bool esAbierto = dato.Estatus == "Abierto";
            cmbDocumento.Enabled = false;
            txtFecha.Enabled = false;
            btnTerminarPrestamo.Visible = esAbierto && partidasEnMemoria.Count > 0;

            guna2TabControl1.SelectedIndex = partidasEnMemoria.Count > 0 ? 1 : 0;
        }

        private void btnCerrarFiltros_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        #endregion

        #region Barra lateral (toolStrip2) / navegación general

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1 && !folioActual.HasValue)
            {
                MessageBox.Show("Es necesario crear el encabezado.");
                guna2TabControl1.SelectedIndex = 0;
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    btnNuevoPrestamo_Click(sender, e);
                    break;
                case "CONSULTAR":
                    guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
                    if (guna2GradientPanel2.Visible)
                    {
                        guna2GradientPanel2.BringToFront();
                        RecargarGrillaConsulta();
                    }
                    else
                    {
                        guna2GradientPanel2.SendToBack();
                    }
                    break;
                default:
                    // IMPRIMIR / ENVIAR CORREO / AUTORIZAR: no aplican todavía
                    // a Préstamos (no existe un reporte/flujo de autorización
                    // definido para este módulo).
                    MessageBox.Show("Esta función aún no está disponible para Préstamos.");
                    break;
            }

            ConfigurarToolStripCompacto();
        }

        private void ConfigurarToolStripCompacto()
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;

            foreach (var boton in new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 })
            {
                boton.TextDirection = ToolStripTextDirection.Vertical270;
                boton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                boton.Size = new Size(23, 79);
            }
        }

        private void ConfigurarToolStripExpandido()
        {
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;

            foreach (var boton in new[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 })
            {
                boton.TextDirection = ToolStripTextDirection.Horizontal;
                boton.DisplayStyle = ToolStripItemDisplayStyle.Image;
                boton.Size = new Size(85, 75);
                boton.AutoSize = false;
                boton.Visible = true;
            }

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
            ConfigurarToolStripExpandido();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            ConfigurarToolStripCompacto();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
        }

        #endregion

        #region Reportes / correo / XML - no aplican todavía a Préstamos

        // Los tres botones siguientes se ocultan en el constructor
        // (button10.Visible = false, etc.) porque no existe todavía un
        // reporte de Préstamo, un flujo de envío de correo ni una
        // exportación XML para este módulo. Se dejan los handlers vacíos
        // únicamente para que el Designer siga compilando.
        private void button10_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                MessageBox.Show("Es necesario seleccionar un Prestamo");
                return;
            }
            ReporteComprobantePrestamo r = new ReporteComprobantePrestamo(txtFolio.Text);
            r.ShowDialog();
        }
        private void button11_Click(object sender, EventArgs e) { }
        private void btnRemisionXML_Click(object sender, EventArgs e) { }

        #endregion

        private void lblDias_Click(object sender, EventArgs e)
        {

        }

        private void tgImpuesto_CheckedChanged(object sender, EventArgs e)
        {
            if (tgImpuesto.Checked)
            {
                lblImpuestoo.Text = "SI";
            }
            else
            {
                lblImpuestoo.Text = "NO";
            }
        }
    }
}