using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.OrdenCompra;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    /// Orden de Compra (compras a proveedores). Trabaja ÚNICAMENTE contra las
    /// tablas OrdenCompra / PartidaOrden, a través de DBOrdenCompra (la
    /// versión anterior de este formulario llamaba métodos de Remisión:
    /// InsertarPartidaRemision, ActualizarTotalesRemision, etc.).
    ///
    /// Se conservan los nombres de controles y de handlers del Designer. Lo
    /// que cambió respecto a la versión anterior:
    ///   - La partida se calcula igual que en Facturas/Remisión: descuento e
    ///     impuesto son PORCENTAJES (antes el descuento se restaba como
    ///     monto). Precio sugerido = CostoUnitario del producto (compra).
    ///   - "Confirmar" guarda la partida y cierra el panel (antes llamaba a
    ///     Limpiar() y borraba el encabezado); "Siguiente" guarda y deja
    ///     el panel listo para la siguiente partida.
    ///   - Los totales del encabezado se leen en los controles correctos
    ///     (Activated usaba los de la partida) y se mantienen al día en cada
    ///     alta/edición/baja de partida.
    ///   - Al abrir una orden desde las grillas se cargan sus partidas, se
    ///     pueden editar mientras esté Abierta y se pueden eliminar.
    ///   - txtClave ya sólo guarda la clave del DOCUMENTO (antes también se
    ///     sobreescribía con la clave del producto); el producto se toma de
    ///     cmbConcepto.SelectedValue.
    ///   - La fecha de vencimiento se recalcula al salir de txtDiasVence
    ///     (antes sólo se calculaba al cargar el formulario).
    ///   - Se retiró lo heredado de recibos/condominios (txtFrecuencia,
    ///     txtReciboInsc/txtReciboCol, txtDocumentoCol) que aquí no aplica.
    /// </summary>
    public partial class OrdenCompra2 : Form
    {
        public static string Matricula = string.Empty;
        public static int Opcion = 0;

        private readonly DBOrdenCompra2 c = new DBOrdenCompra2();
        private readonly DBProductosServicios prod = new DBProductosServicios();

        public OrdenCompra2()
        {
            InitializeComponent();

            // Estos controles existían en el Designer pero no tenían handler
            // (btnEliminarPartida no hacía nada; el tab no validaba que
            // existiera el encabezado; txtDiasVence nunca recalculaba la fecha
            // de vencimiento). Se enganchan aquí por código para no depender
            // de editar el Designer.
            btnEliminarPartida.Click += btnEliminarPartida_Click;
            guna2TabControl1.SelectedIndexChanged += guna2TabControl1_SelectedIndexChanged;
            txtDiasVence.Leave += txtDiasVence_Leave;
        }

        #region Utilidades

        /// <summary>Folio de la orden en pantalla; 0 si todavía no hay encabezado ("" o "X" durante una carga).</summary>
        private int FolioActual
        {
            get
            {
                int folio;
                return int.TryParse(txtFolio.Text, out folio) ? folio : 0;
            }
        }

        /// <summary>Ejecuta una operación de base de datos y, si falla, muestra el error en vez de dejar que truene el formulario.</summary>
        private bool Intentar(Action accion)
        {
            try
            {
                accion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Orden de compra",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>Convierte el texto de un campo (con o sin comas de miles) a decimal; vacío o inválido = 0.</summary>
        private static decimal ADecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0m;

            decimal valor;
            return decimal.TryParse(texto.Replace(",", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out valor)
                ? valor
                : 0m;
        }

        private static decimal Redondear(decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }

        private bool HayProductoSeleccionado()
        {
            int clave;
            return cmbConcepto.SelectedValue != null
                && int.TryParse(cmbConcepto.SelectedValue.ToString(), out clave);
        }

        #endregion

        #region Ciclo de vida del formulario

        private void OrdenCompra2_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaEncabezado(dgvOrdenesCompraNoAutorizados);
            ConfigurarGrillaEncabezado(dgvOrdenesCompraAutorizados);
            ConfigurarGrillaPartidas(dgvPartidas);

            txtNotas.MaxLength = 100;
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDownList;

            // La validación de estatus vive en cada handler; los botones de
            // partida se dejan habilitados (antes dependían de pulsar NUEVO).
            btnAgregarPartida.Enabled = true;
            btnEliminarPartida.Enabled = true;
            btnCancelarPartida.Enabled = true;
            btnConfirmarPartida.Enabled = true;
            btnSiguientePartida.Enabled = true;

            Intentar(() => c.SeleccionarOrdenCompra(cmbDocumento));
            RecargarGrillas();

            cmbEstatus.SelectedIndex = 0;
            PrepararNuevaCaptura();
            ConfigurarToolStripCompacto();
        }

        private void OrdenCompra2_Activated(object sender, EventArgs e)
        {
            if (FolioActual > 0)
            {
                Intentar(() =>
                {
                    c.ReciboSaldos(FolioActual, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
                    if (txtPartidas.Text == string.Empty)
                    {
                        txtPartidas.Text = "0";
                    }
                });
            }

            if (Opcion == 1)
            {
                // Se apaga primero para que un aviso/error no se repita en cada Activated.
                Opcion = 0;
                AutorizarOrdenActual();
            }
        }

        /// <summary>Valores iniciales de una captura nueva (también se usan al limpiar la pantalla).</summary>
        private void PrepararNuevaCaptura()
        {
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            txtDiasVence.Text = "0";
            RecalcularFechaVencimiento();
        }

        #endregion

        #region Grillas

        private void ConfigurarGrillaEncabezado(DataGridView dgv)
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
                Width = 80
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Documento",
                DataPropertyName = "Documento",
                HeaderText = "Documento",
                Width = 100
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                DataPropertyName = "Proveedor",
                HeaderText = "Proveedor",
                Width = 200,
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
                Width = 80
            });
        }

        private void ConfigurarGrillaPartidas(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridViewCellStyle importe = new DataGridViewCellStyle
            {
                Format = "N2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Partida", DataPropertyName = "Partida", HeaderText = "Partida", Width = 60 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Producto",
                DataPropertyName = "Producto",
                HeaderText = "Producto",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", DataPropertyName = "Precio", HeaderText = "Precio", Width = 85, DefaultCellStyle = importe });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 90, DefaultCellStyle = importe });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descuento", DataPropertyName = "Descuento", HeaderText = "Desc. %", Width = 65, DefaultCellStyle = importe });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Impuesto", DataPropertyName = "Impuesto", HeaderText = "Impuesto %", Width = 75, DefaultCellStyle = importe });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", DataPropertyName = "Total", HeaderText = "Total", Width = 95, DefaultCellStyle = importe });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Recibido", DataPropertyName = "Recibido", HeaderText = "Recibido", Width = 70 });
        }

        private void RecargarGrillas()
        {
            Intentar(() =>
            {
                c.CargarOrdenes(dgvOrdenesCompraNoAutorizados, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, false);
                c.CargarOrdenes(dgvOrdenesCompraAutorizados, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
            });
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e) { RecargarGrillas(); }
        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e) { RecargarGrillas(); }
        private void txtFiltroNombre_TextChanged(object sender, EventArgs e) { RecargarGrillas(); }

        private void dgvOrdenesCompraNoAutorizados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarOrdenDesdeGrilla(dgvOrdenesCompraNoAutorizados.Rows[e.RowIndex].Cells["Folio"].Value.ToString());
            }
        }

        private void dgvOrdenesCompraAutorizados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarOrdenDesdeGrilla(dgvOrdenesCompraAutorizados.Rows[e.RowIndex].Cells["Folio"].Value.ToString());
            }
        }

        private void CargarOrdenDesdeGrilla(string folio)
        {
            int folioOrden;
            if (!int.TryParse(folio, out folioOrden))
                return;

            // "X" evita que cmbDocumento_SelectedIndexChanged recalcule el
            // consecutivo mientras se cargan los datos.
            txtFolio.Text = "X";

            Intentar(() =>
            {
                string proveedor;
                bool encontrada = c.ConsultaOrden(folioOrden, txtClave, cmbEstatus, txtFecha, txtDiasVence,
                    txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal,
                    txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo, txtAutoriza, txtFechaAuto,
                    out proveedor);

                if (!encontrada)
                {
                    txtFolio.Clear();
                    MessageBox.Show("No se encontró la orden de compra seleccionada.");
                    return;
                }

                guna2TabControl1.Enabled = true;
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;

                txtMatricular.Text = proveedor;
                Matricula = proveedor;

                string[] documento = c.InformacionDocumento2(txtClave.Text);
                if (documento != null)
                {
                    txtDocumento.Text = documento[0];
                    txtClave.Text = documento[1];
                    cmbDocumento.Text = documento[1] + " - " + documento[0];
                }

                guna2GradientPanel2.Visible = false;
                PanelPartidasRequisicion.Visible = false;
                CargarPartidas();
            });
        }

        #endregion

        #region Encabezado

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text == "X" || cmbDocumento.Text == string.Empty)
                return;

            Intentar(() =>
            {
                string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                if (valores == null)
                    return;

                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                if (txtFolio.Text == string.Empty)
                {
                    c.ConsecutivoOrdenCompra(txtConsecutivo, txtClave.Text);
                }
            });

            txtDiasVence.Focus();
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty)
            {
                txtNombreAlumnno.Clear();
                return;
            }

            Intentar(() =>
            {
                string[] valores = c.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores != null ? valores[0] : string.Empty;
            });
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (FolioActual > 0)
            {
                MessageBox.Show("El proveedor no puede cambiarse una vez creado el encabezado.");
                return;
            }

            using (var buscador = new BuscarListaProveedores())
            {
                if (buscador.ShowDialog() == DialogResult.OK)
                {
                    txtMatricular.Text = buscador.Matricula;
                    txtNombreAlumnno.Text = buscador.Nombre;
                    Matricula = buscador.Matricula; // se sigue alimentando el campo estático por si otro código depende de él
                }
            }
        }

        private bool RecalcularFechaVencimiento()
        {
            try
            {
                int dias = string.IsNullOrWhiteSpace(txtDiasVence.Text) ? 0 : Convert.ToInt32(txtDiasVence.Text);
                if (dias < 0)
                    throw new FormatException();

                DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                txtFechaVence.Text = fecha.AddDays(dias).ToString("yyyy/MM/dd");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de dias vencimiento incorrecto");
                return false;
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            RecalcularFechaVencimiento();
        }

        private void btnCrearEncabezado_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una orden de compra Bloqueado o Cancelado");
                return;
            }
            if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }
            if (!RecalcularFechaVencimiento())
                return;

            if (FolioActual == 0)
            {
                bool creada = Intentar(() => c.InsertarOrden(txtFolio, txtConsecutivo, txtClave.Text,
                    cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text,
                    txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text));

                if (!creada)
                    return;
            }

            // Una vez creado el encabezado ya no se puede editar (no hay
            // actualización de encabezado), así que se bloquean esos campos.
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            txtNotas.Enabled = false;

            guna2TabControl1.SelectedIndex = 1;
            CargarProductos();
            CargarPartidas();

            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
        }

        /// <summary>Limpia toda la pantalla (encabezado + captura de partidas) y la deja lista para una orden nueva.</summary>
        private void Limpiar()
        {
            txtFolio.Clear();
            txtConsecutivo.Clear();
            cmbEstatus.Text = "Abierto";
            txtTotalConceptos.Text = "0";
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            txtAutoriza.Clear();
            txtFechaAuto.Clear();

            cmbDocumento.SelectedIndex = -1;
            cmbDocumento.Text = null;
            cmbDocumento.DroppedDown = false;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            txtNotas.Enabled = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            Matricula = string.Empty;
            Opcion = 0;

            PrepararNuevaCaptura();

            LimpiarPartida();
            PanelPartidasRequisicion.Visible = false;
            guna2TabControl1.SelectedIndex = 0;
            dgvPartidas.DataSource = null;
            btnTerminarOrden.Visible = false;
        }

        private void NuevaOrden()
        {
            if (FolioActual > 0 && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("La pantalla se limpiará. La orden abierta actual queda guardada y podrá retomarla desde CONSULTAR. ¿Desea continuar?",
                    "Nueva orden de compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            Limpiar();

            guna2TabControl1.Enabled = true;
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;
            txtNotas.Enabled = true;
            cmbDocumento.DroppedDown = true;
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1 && FolioActual == 0)
            {
                MessageBox.Show("Es necesario crear el encabezado");
                guna2TabControl1.SelectedIndex = 0;
            }
        }

        #endregion

        #region Partidas - captura

        private void CargarProductos()
        {
            try
            {
                cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
                ComboUtil.LlenarComboBox(cmbConcepto, prod.ObtenerProductos(), "Descripcion", "ClaveProducto");
                cmbConcepto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible cargar el catálogo de productos: " + ex.Message);
            }
            finally
            {
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
            }
        }

        private void CargarPartidas()
        {
            Intentar(() => c.CargarPartidasOrden(dgvPartidas, FolioActual));
            ActualizarBotonTerminar();
        }

        private void ActualizarBotonTerminar()
        {
            btnTerminarOrden.Visible = cmbEstatus.Text == "Abierto" && dgvPartidas.Rows.Count > 0;
        }

        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }
            if (FolioActual == 0)
            {
                MessageBox.Show("Es necesario crear el encabezado");
                return;
            }

            CargarProductos();
            PanelPartidasRequisicion.Visible = true;
            PrepararNuevaPartida();
        }

        /// <summary>Deja el panel listo para capturar una partida NUEVA (campos en blanco y siguiente número de partida).</summary>
        private void PrepararNuevaPartida()
        {
            LimpiarPartida();
            ConfigurarPartida(false, true);
            Intentar(() => c.Consulta5Orden(FolioActual, txtPartida));
        }

        private void LimpiarPartida()
        {
            txtCantidad.Text = "1";
            txtUnidad.Clear();
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0.00";
            txtConcepto2.Clear();
            txtEntregado.Text = "0";
            cmbConcepto.SelectedIndex = -1;
            Calcular();
        }

        private void ConfigurarPartida(bool bloquear, bool esNueva)
        {
            label56.Visible = bloquear;
            txtEntregado.Visible = bloquear;

            btnEliminarPartida.Visible = !bloquear && !esNueva;
            btnCancelarPartida.Visible = !bloquear;
            btnConfirmarPartida.Visible = !bloquear;
            btnSiguientePartida.Visible = !bloquear;
            guna2Button11.Visible = true;

            txtPartida.Enabled = false;
            cmbConcepto.Enabled = !bloquear;
            txtCantidad.Enabled = !bloquear;
            txtUnidad.Enabled = !bloquear;
            txtPrecio.Enabled = !bloquear;
            txtDescuento1.Enabled = !bloquear;
            txtImpuesto1.Enabled = !bloquear;
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int clave;
            if (cmbConcepto.SelectedValue == null || !int.TryParse(cmbConcepto.SelectedValue.ToString(), out clave))
                return;

            Intentar(() =>
            {
                string[] valores = c.InformacionProductoCompra(clave);
                if (valores == null)
                    return;

                txtConcepto2.Text = valores[0];
                txtPrecio.Text = valores[2];
                txtUnidad.Text = valores[3];
                txtImpuesto1.Text = valores[4];
                txtDescuento1.Text = "0.00";
                txtCantidad.Text = "1";
            });

            Calcular();
        }

        /// <summary>Valida y guarda la partida en pantalla (alta o edición) y refresca los totales del encabezado.</summary>
        private bool GuardarPartidaActual()
        {
            if (!HayProductoSeleccionado())
            {
                MessageBox.Show("Seleccione un producto válido del catálogo antes de continuar.");
                return false;
            }

            decimal cantidad = ADecimal(txtCantidad.Text);
            if (cantidad <= 0 || cantidad != decimal.Truncate(cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero mayor a cero.");
                txtCantidad.Focus();
                return false;
            }

            decimal descuento = ADecimal(txtDescuento1.Text);
            if (descuento < 0 || descuento > 100)
            {
                MessageBox.Show("El descuento debe estar entre 0 y 100%.");
                txtDescuento1.Focus();
                return false;
            }

            decimal impuesto = ADecimal(txtImpuesto1.Text);
            if (impuesto < 0 || impuesto > 100)
            {
                MessageBox.Show("El impuesto debe estar entre 0 y 100%.");
                txtImpuesto1.Focus();
                return false;
            }

            Calcular();

            int partida;
            if (!int.TryParse(txtPartida.Text, out partida) || partida <= 0)
            {
                MessageBox.Show("No se pudo determinar el número de partida. Cierre el panel y vuelva a agregarla.");
                return false;
            }

            int folio = FolioActual;
            int clave = Convert.ToInt32(cmbConcepto.SelectedValue);
            decimal tipoCambio = ADecimal(txtTipoCambio1.Text);
            if (tipoCambio <= 0)
                tipoCambio = 1m;

            return Intentar(() =>
            {
                c.GuardarPartidaOrden(folio, partida, clave, txtConcepto2.Text, Convert.ToInt32(cantidad),
                    txtUnidad.Text, txtDivisa1.Text, tipoCambio, ADecimal(txtSubtotal1.Text), descuento,
                    ADecimal(txtTotal1.Text), ADecimal(txtPrecio.Text), impuesto);

                c.ActualizarTotalesOrden(folio);
                c.ReciboSaldos(folio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
            });
        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (!HayProductoSeleccionado())
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TerminarCapturaPartidas();
                }
                return;
            }

            if (ADecimal(txtTotal1.Text) <= 0)
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TerminarCapturaPartidas();
                }
                return;
            }

            if (!GuardarPartidaActual())
                return;

            CargarPartidas();
            PanelPartidasRequisicion.Visible = false;
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (!HayProductoSeleccionado())
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            if (ADecimal(txtTotal1.Text) <= 0)
            {
                MessageBox.Show("Registre el importe para continuar");
                return;
            }

            if (!GuardarPartidaActual())
                return;

            Intentar(() => c.ReciboSaldos(FolioActual, txtSubtotalR, txtDescuentoR, txtImpuestoR, txtTotalR, null));
            CargarPartidas();
            PrepararNuevaPartida();
        }

        private void TerminarCapturaPartidas()
        {
            PanelPartidasRequisicion.Visible = false;
            ActualizarBotonTerminar();
        }

        private void btnCancelarPartida_Click(object sender, EventArgs e)
        {
            // Descarta lo capturado y deja el panel como una partida nueva
            // (así no se puede sobreescribir por accidente una partida existente).
            PrepararNuevaPartida();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void btnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se pueden eliminar partidas si el documento esta abierto");
                return;
            }

            int partida;
            if (!int.TryParse(txtPartida.Text, out partida) || partida <= 0)
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            if (MessageBox.Show("¿Desea eliminar la partida " + partida + "?", "Partida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int folio = FolioActual;
            string mensaje = string.Empty;

            if (!Intentar(() => mensaje = c.EliminarPartidaOrden(folio, partida)))
                return;

            if (!string.IsNullOrEmpty(mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            Intentar(() =>
            {
                c.ActualizarTotalesOrden(folio);
                c.ReciboSaldos(folio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
            });

            PanelPartidasRequisicion.Visible = false;
            CargarPartidas();
        }

        private void dgvPartidas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int partida;
            if (!int.TryParse(dgvPartidas.Rows[e.RowIndex].Cells["Partida"].Value.ToString(), out partida))
                return;

            bool abierta = cmbEstatus.Text.Equals("Abierto", StringComparison.OrdinalIgnoreCase);

            // El combo debe tener su catálogo cargado ANTES de poder
            // seleccionar el producto de la partida.
            CargarProductos();

            string clave = string.Empty;
            bool encontrada = false;

            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
            try
            {
                if (!Intentar(() => encontrada = c.ConsultaPartidaOrden(FolioActual, partida, txtCantidad, txtUnidad,
                        txtDivisa1, txtTipoCambio1, txtPrecio, txtDescuento1, txtImpuesto1, txtConcepto2,
                        txtEntregado, out clave)))
                {
                    return;
                }

                int claveNumerica;
                if (int.TryParse(clave, out claveNumerica))
                {
                    cmbConcepto.SelectedValue = claveNumerica;
                }
            }
            finally
            {
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
            }

            if (!encontrada)
                return;

            Calcular();
            txtPartida.Text = partida.ToString();
            PanelPartidasRequisicion.Visible = true;
            ConfigurarPartida(!abierta, false);
        }

        #endregion

        #region Cálculo de la partida

        /// <summary>
        /// Subtotal (bruto) = precio x cantidad; descuento e impuesto son
        /// porcentajes y se redondean a 2 decimales (descuento sobre el bruto,
        /// impuesto sobre el bruto ya descontado), con la MISMA regla que
        /// DBOrdenCompra.ActualizarTotalesOrden, para que el encabezado
        /// siempre cuadre con las partidas. Nunca lanza excepción: un campo
        /// vacío o incompleto cuenta como 0.
        /// </summary>
        private void Calcular()
        {
            decimal bruto = Redondear(ADecimal(txtPrecio.Text) * ADecimal(txtCantidad.Text));
            decimal descuento = Redondear(bruto * ADecimal(txtDescuento1.Text) / 100m);
            decimal impuesto = Redondear((bruto - descuento) * ADecimal(txtImpuesto1.Text) / 100m);
            decimal total = bruto - descuento + impuesto;

            txtSubtotal1.Text = bruto.ToString("N2");
            txtImpuestoIm.Text = impuesto.ToString("N2");
            txtTotal1.Text = total.ToString("N2");
        }

        /// <summary>Máscara de moneda: el texto se interpreta como centavos (1500 -> 15.00) y se muestra con comas.</summary>
        private void Moneda(ref Guna2TextBox txt)
        {
            string digitos = txt.Text.Replace(",", "").Replace(".", "").PadLeft(3, '0');

            long centavos;
            if (!long.TryParse(digitos, out centavos))
                return;

            txt.Text = (centavos / 100m).ToString("N2");
            txt.SelectionStart = txt.Text.Length;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e) { Calcular(); }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
            Calcular();
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento1);
            Calcular();
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImpuesto1);
            Calcular();
        }

        // Campos calculados: sólo se les aplica el formato.
        private void txtSubtotal1_TextChanged(object sender, EventArgs e) { Moneda(ref txtSubtotal1); }
        private void txtImpuestoIm_TextChanged(object sender, EventArgs e) { Moneda(ref txtImpuestoIm); }
        private void txtTotal1_TextChanged(object sender, EventArgs e) { Moneda(ref txtTotal1); }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e) { Moneda(ref txtSubtotalR); }
        private void txtDescuentoR_TextChanged(object sender, EventArgs e) { Moneda(ref txtDescuentoR); }
        private void txtTotalR_TextChanged(object sender, EventArgs e) { Moneda(ref txtTotalR); }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e) { Utilerias.SoloNumFracc(sender, e); }
        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e) { Utilerias.SoloNumFracc(sender, e); }
        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e) { Utilerias.SoloNumFracc(sender, e); }

        // La columna Cantidad de PartidaOrden es entera: sólo dígitos.
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        // Subtotal y Total de la partida son calculados: no se capturan a mano.
        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = true; }
        private void txtTotal1_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = true; }

        #endregion

        #region Confirmación, cancelación y autorización

        private void btnTerminarOrden_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            if (dgvPartidas.Rows.Count == 0)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar una orden de compra Bloqueada o Cancelada");
                return;
            }
            if (MessageBox.Show("Al confirmar la orden de compra no podra realizar modificaciones, ¿Desea continuar?",
                "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int folio = FolioActual;
            if (!Intentar(() =>
            {
                c.ActualizarTotalesOrden(folio);
                c.ActualizarOrdenEstatus(folio, "Bloqueado");
            }))
            {
                return;
            }

            cmbEstatus.Text = "Bloqueado";
            Matricula = string.Empty;

            Limpiar();
            RecargarGrillas();
            guna2TabControl1.SelectedIndex = 0;
            btnTerminarOrden.Visible = false;
        }

        // Cancelar (el botón conserva su nombre original del Designer).
        private void btnLimpuarOrden_Click(object sender, EventArgs e)
        {
            if (FolioActual == 0)
            {
                MessageBox.Show("Seleccione el recibo");
                return;
            }
            if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una orden de compra que no esta bloqueado");
                return;
            }
            if (MessageBox.Show("El saldo de esta orden de compra sera cancelado, ¿Desea continuar?", "Recibo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int folio = FolioActual;
            if (!Intentar(() => c.ActualizarOrdenEstatus(folio, "Cancelado")))
                return;

            Limpiar();
            RecargarGrillas();
        }

        /// <summary>Se llama desde AUTORIZAR (antes de abrir el diálogo de administrador) para no pedir credenciales en balde.</summary>
        private void SolicitarAutorizacion()
        {
            if (FolioActual == 0)
            {
                MessageBox.Show("Seleccione la orden de compra a autorizar");
                return;
            }
            if (cmbEstatus.Text == "Cancelado")
            {
                MessageBox.Show("No es posible autorizar una orden de compra cancelada");
                return;
            }
            if (!string.IsNullOrEmpty(txtAutoriza.Text))
            {
                MessageBox.Show("La orden de compra ya fue autorizada por " + txtAutoriza.Text);
                return;
            }

            using (AutentificarAdmin autentificarAdmin = new AutentificarAdmin())
            {
                autentificarAdmin.ShowDialog();
            }
        }

        /// <summary>El diálogo de administrador deja Opcion = 1 y el usuario en DBRegistrarIngresos.usuario; aquí se registra.</summary>
        private void AutorizarOrdenActual()
        {
            if (FolioActual == 0)
            {
                MessageBox.Show("Seleccione la orden de compra a autorizar");
                return;
            }

            int folio = FolioActual;
            string usuario = DBRegistrarIngresos.usuario;
            string fecha = DateTime.Today.ToString("yyyy/MM/dd");

            if (!Intentar(() => c.ActualizarOrdenAuto(folio, usuario, fecha)))
                return;

            MessageBox.Show("Orden de Compra Autorizada");
            Limpiar();
            RecargarGrillas();
        }

        #endregion

        #region Barra lateral (toolStrip2)

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    NuevaOrden();
                    break;

                case "CONSULTAR":
                    guna2GradientPanel2.Enabled = true;
                    guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
                    if (guna2GradientPanel2.Visible)
                    {
                        RecargarGrillas();
                        guna2GradientPanel2.BringToFront();
                    }
                    else
                    {
                        guna2GradientPanel2.SendToBack();
                    }
                    break;

                case "IMPRIMIR":
                    if (FolioActual == 0)
                    {
                        MessageBox.Show("Seleccione una orden de compra para continuar");
                    }
                    else
                    {
                        using (var reporte = new ReporteComprobanteOrdenCompra(txtFolio.Text))
                        {
                            reporte.ShowDialog();
                        }
                    }
                    break;

                case "ENVIAR CORREO":
                    // Pendiente: este formulario no envía correo de la orden de compra.
                    break;

                case "AUTORIZAR":
                    SolicitarAutorizacion();
                    break;
            }

            ConfigurarToolStripCompacto();
        }

        private void ConfigurarToolStripCompacto()
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
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
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
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
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e) { ConfigurarToolStripExpandido(); }
        private void guna2PictureBox2_Click(object sender, EventArgs e) { ConfigurarToolStripCompacto(); }

        private void guna2Button10_Click(object sender, EventArgs e) { guna2GradientPanel2.Visible = false; }

        private void guna2CircleButton1_Click(object sender, EventArgs e) { this.Close(); }

        #endregion

        #region Compatibilidad con el Designer

        // Handlers que el Designer ya tiene enganchados y que no hacen nada
        // (se conservan para que el formulario compile sin tocar el .Designer.cs).
        private void label9_Click(object sender, EventArgs e) { }
        private void label37_Click(object sender, EventArgs e) { }
        private void txtNombreAlumnno_TextChanged(object sender, EventArgs e) { }
        private void txtTipoCambio_TextChanged(object sender, EventArgs e) { }
        private void toolStrip2_MouseEnter(object sender, EventArgs e) { }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Duplicado de guna2Button10_Click (cerrar el panel de consulta); se
        // deja por si el Designer lo tiene enganchado. Si no, se puede borrar.
        private void _Click(object sender, EventArgs e) { guna2GradientPanel2.Visible = false; }

        #endregion
    }
}