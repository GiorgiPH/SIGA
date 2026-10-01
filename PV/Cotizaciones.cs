using Condominios.Clases.CentroCostos;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PuntoVentas.Clases.ProductosServicios;
using PV.Clases;
using PV.Clases.CentroCostos;
using PV.Clases.Clientes;
using PV.Clases.Cotizaciones;
using PV.Clases.PedidoCliente;
using PV.Clases.Servicios;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PV
{
    /// <summary>
    
    /// </summary>
    public partial class Cotizaciones : Form
    {
        public static string Matricula = string.Empty;
        public static string M2 = string.Empty;

        // Específico de Cotizaciones (Cotizacion / PartidaCotizacion)
        DBCotizaciones cot = new DBCotizaciones();

        DBPedidoCliente c = new DBPedidoCliente();
        DBClientes cl = new DBClientes();
        DBCentroCostos cc = new DBCentroCostos();

        // Catálogos para las partidas: Cotizaciones puede capturar contra
        // Productos o contra Servicios, según cmbTipo.
        DBServicios srv = new DBServicios();
        DBProductosServicios prod = new DBProductosServicios();

        private bool mostrarCentroCosto = false;

        // true cuando la Cotización no tiene cliente registrado y se está
        // capturando/mostrando información de prospecto.
        private bool esProspecto = false;

        public Cotizaciones()
        {
            InitializeComponent();

            ToolTip T = new ToolTip();
            label18.Text = "Cotización";
            T.SetToolTip(btnBuevaFactura, "Nueva Cotización");
            T.SetToolTip(guna2Button16, "Consultar Cotización");
            T.SetToolTip(button10, "Imprimir Cotización");
            T.SetToolTip(btnCliente, "Buscar Cliente");

            c.BuscarProveedor(guna2DataGridView2);
        }

        #region Ciclo de vida del formulario

        private void Cotizaciones_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaEncabezado(DataGridView2);
            ConfigurarGrillaPartidas(guna2DataGridView1);

            cot.CargarCotizaciones(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);

            cot.SeleccionarCotizacion(cmbDocumento);

            ConfigurarComboTipo();

            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;

            txtDiasVence.Text = "0";
            tgProspecto.Checked = false;
            RecalcularFechaVencimiento();

            ConfigurarToolStripExpandido();
            LlenarComboCentro();

            ConfigurarClienteOProspecto(hayCliente: false);
        }

        /// <summary>Llena cmbTipo con las dos opciones fijas Producto/Servicio.</summary>
        private void ConfigurarComboTipo()
        {
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Producto");
            cmbTipo.Items.Add("Servicio");
            cmbTipo.SelectedIndex = -1;
        }

        /// <summary>
        /// Arma por código las columnas de las grillas de encabezado.
        /// Se corrige el HeaderText "Proveedor" que traía la copia de
        /// Facturas (arrastrado de un copy-paste anterior) por "Cliente",
        /// que es lo que realmente representa la columna en Cotizaciones.
        /// </summary>
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
                Width = 90
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Documento",
                DataPropertyName = "Documento",
                HeaderText = "Documento",
                Width = 110
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                DataPropertyName = "Cliente",
                HeaderText = "Cliente",
                Width = 220,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy/MM/dd" }
            });
        }

        /// <summary>
        /// Arma por código las columnas de la grilla de partidas. Se agrega
        /// la columna "Tipo" (Producto/Servicio) respecto a Facturas.
        /// </summary>
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
                Name = "Consecutivo",
                DataPropertyName = "Consecutivo",
                HeaderText = "Folio",
                Width = 70,
                Visible = true
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Folio",
                DataPropertyName = "FolioCotizacion",
                HeaderText = "Folio",
                Width = 70,
                Visible = false
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Partida",
                DataPropertyName = "Partida",
                HeaderText = "Partida",
                Width = 70
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                DataPropertyName = "TipoConcepto",
                HeaderText = "Tipo",
                Width = 80
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Producto",
                DataPropertyName = "Concepto2",
                HeaderText = "Producto / Servicio",
                Width = 220,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                Width = 80
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descuento",
                DataPropertyName = "Descuento",
                HeaderText = "Descuento",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Impuesto",
                DataPropertyName = "Impuesto",
                HeaderText = "Impuesto",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void Cotizaciones_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X" && !string.IsNullOrEmpty(txtFolio.Text))
            {
                cot.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
            }

        
        }



        #endregion

        #region Catálogo de documento / consecutivo

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text == "X" || cmbDocumento.Text == string.Empty)
                return;

            string[] valores = c.InformacionDocumento(cmbDocumento.Text);
            txtDocumento.Text = valores[0];
            txtClave.Text = valores[1];

            if (txtFolio.Text == string.Empty)
            {
                if (valores.Length > 2)
                {
                    mostrarCentroCosto = ParsearBooleano(valores[2]);
                    cmbCentroCostos.Enabled = mostrarCentroCosto;
                }

                cot.ConsecutivoCotizacion(txtConsecutivo, txtClave.Text);
            }

            txtDiasVence.Focus();
        }

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

        #endregion

        #region Cliente / Prospecto

        /// <summary>
        /// Habilita/deshabilita los campos de prospecto según haya o no un
        /// cliente seleccionado. Se dispara automáticamente desde
        /// txtMatricular_TextChanged: seleccionar un cliente (txtMatricular
        /// con contenido) apaga el modo prospecto, y limpiar txtMatricular
        
        /// </summary>
        private void ConfigurarClienteOProspecto(bool hayCliente)
        {
            esProspecto = !hayCliente;

            txtNombreProspecto.Enabled = esProspecto;
            txtRFCProspecto.Enabled = esProspecto;
            txtDomicilioProspecto.Enabled = esProspecto;
            txtContactoProspecto.Enabled = esProspecto;
            txtCelularProspecto.Enabled = esProspecto;

            if (hayCliente)
            {
                txtNombreProspecto.Clear();
                txtRFCProspecto.Clear();
                txtDomicilioProspecto.Clear();
                txtContactoProspecto.Clear();
                txtCelularProspecto.Clear();
            }
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricular.Text))
            {
                txtNombreAlumnno.Clear();
                ConfigurarClienteOProspecto(hayCliente: false);
                return;
            }

            string[] valores = cl.InformacionCliente(txtMatricular.Text);
            txtNombreAlumnno.Text = valores[1];
            ConfigurarClienteOProspecto(hayCliente: true);
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            BuscarCliente b = new BuscarCliente();
            b.ShowDialog();

            if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
            {
                txtMatricular.Text = BuscarCliente.Cliente;
            }
        }

        private void guna2DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            M2 = guna2DataGridView2.Rows[e.RowIndex].Cells["Matricula1"].Value.ToString();
            txtMatricular.Text = M2;
            guna2GradientPanel5.Visible = false;
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel5.Visible = false;
        }

        private void txtFiltro1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro1.Text == string.Empty)
                c.BuscarProveedor(guna2DataGridView2);
            else
                c.BuscarAlumnosFiltro(guna2DataGridView2, txtFiltro1.Text);
        }

        #endregion

        #region Encabezado - alta / limpieza

        private void btnRegistrarFactura_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMatricular.Text) && string.IsNullOrWhiteSpace(txtNombreProspecto.Text))
            {
                MessageBox.Show("Seleccione un cliente o registre al menos el nombre del prospecto antes de continuar.");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una cotización Bloqueada o Cancelada");
                return;
            }
            if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }

            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                string centroCosto = null;

                if (mostrarCentroCosto)
                {
                    centroCosto = ComboUtil.ObtenerSelectedValue(cmbCentroCostos);

                    if (string.IsNullOrWhiteSpace(centroCosto))
                    {
                        MessageBox.Show("Seleccione un centro de costos antes de continuar.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                cot.InsertarCotizacion(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text,
                    txtFechaVence.Text,
                    string.IsNullOrWhiteSpace(txtMatricular.Text) ? null : txtMatricular.Text,
                    txtNombreProspecto.Text, txtRFCProspecto.Text, txtDomicilioProspecto.Text,
                    txtContactoProspecto.Text, txtCelularProspecto.Text,
                    txtDivisa1.Text, txtTipoCambio1.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text,
                    centroCosto);
            }

            guna2TabControl1.SelectedIndex = 1;

            cot.Consulta5Cotizacion(txtFolio.Text, txtPartida);

            txtCantidad.Text = "1";
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";

            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
        }

        void Limpiar()
        {
            txtFolio.Clear();
            txtConsecutivo.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtFechaVence.Clear();
            txtMatricular.Text = "";
            txtNombreAlumnno.Text ="";
            txtNombreProspecto.Text = "";
            txtRFCProspecto.Text = "";
            txtDomicilioProspecto.Text = "";
            txtContactoProspecto.Text = "";
            txtCelularProspecto.Text = "";
            
            ConfigurarClienteOProspecto(hayCliente: false);

            txtPartidas.Text = "0";
            txtRecargo.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";
            txtNotas.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            txtFecha.Enabled = false;
            btnCliente.Enabled = false;
            txtNotas.Enabled = false;

            Matricula = string.Empty;

            cmbDocumento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();

            cmbCentroCostos.SelectedIndex = -1;
            tgProspecto.Checked = false;

            PanelPartidasRequisicion.Visible = false;
            guna2TabControl1.SelectedIndex = 0;
            guna2DataGridView1.DataSource = null;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La pantalla se limpiará, ¿Desea continuar?", "Documento",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar();
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            LimpiarPartida();
            Limpiar();
            guna2DataGridView1.DataSource = null;
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;
            txtNotas.Enabled = true;
            cmbDocumento.Focus();
        }

        private void btnBuevaFactura_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolio.Text) && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nueva Cotización",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            Limpiar();
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;
            txtFecha.Enabled = true;
            txtNotas.Enabled = true;
            cmbDocumento.DroppedDown = true;
            btnCliente.Enabled = true;
        }

        #endregion

        #region Partidas - captura

        /// <summary>Llena cmbConcepto según el tipo elegido en cmbTipo (Producto -> ProductosServicios, Servicio -> Servicios).</summary>
        private void CargarComboConceptos()
        {
            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

            if (cmbTipo.Text == "Producto")
            {
                DataTable dtProductos = prod.ObtenerProductos();
                ComboUtil.LlenarComboBox(cmbConcepto, dtProductos, "Descripcion", "ClaveProducto");
            }
            else if (cmbTipo.Text == "Servicio")
            {
                DataTable dtServicios = srv.ObtenerProductosGasto();
                ComboUtil.LlenarComboBox(cmbConcepto, dtServicios, "Descripcion", "ClaveServicio");
            }
            else
            {
                cmbConcepto.DataSource = null;
                cmbConcepto.Items.Clear();
            }

            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboConceptos();
            txtConcepto2.Clear();
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";
            txtUnidad.Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }

            PanelPartidasRequisicion.Visible = true;
            ConfigurarPartida(false);

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;

            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();
            txtConcepto2.Text = string.Empty;

            txtCantidad.Text = "1";
            txtUnidad.Text = "";
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuesto1.Text = "0";

            cot.Consulta5Cotizacion(txtFolio.Text, txtPartida);
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
                return;

            string[] valores = cmbTipo.Text == "Producto"
                ? prod.InformacionProducto(cmbConcepto.Text)
                : srv.InformacionServicio(cmbConcepto.Text);

            if (valores == null)
                return;

            txtConcepto2.Text = valores[0];
            txtPrecio.Text = valores[2];
            txtUnidad.Text = valores[3];
            txtImpuesto1.Text = valores[4];
            txtDescuento1.Text = valores[5];
            txtCantidad.Text = "1";

            Calcular();
        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TerminarCapturaPartidas();
                }
                return;
            }

            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
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
            btnTerminarFactura.Visible = true;
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto o servicio para continuar");
                return;
            }
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                MessageBox.Show("Registre el importe para continuar");
                return;
            }

            if (!GuardarPartidaActual())
                return;

            cot.ReciboSaldosPartidas(txtFolio.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);
            CargarPartidas();
            LimpiarPartida();
            cot.Consulta5Cotizacion(txtFolio.Text, txtPartida);
        }

        /// <summary>
        /// Inserta o actualiza la partida actualmente capturada, igual que
        /// GuardarPartidaActual en Facturas, pero validando también que se
        /// haya elegido cmbTipo y enviando TipoConcepto/ClaveConcepto a la
        /// capa de datos.
        /// </summary>
        private bool GuardarPartidaActual()
        {
            if (string.IsNullOrEmpty(cmbTipo.Text))
            {
                MessageBox.Show("Seleccione el tipo (Producto o Servicio) antes de continuar.");
                return false;
            }

            if (cmbConcepto.SelectedValue == null || cmbConcepto.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Seleccione un producto o servicio válido del catálogo antes de continuar.");
                return false;
            }

            if (cot.ExistePartidaCotizacion(txtFolio.Text, txtPartida.Text))
            {
                cot.ActualizarPartidaCotizacion(
                    txtFolio.Text,
                    txtPartida.Text,
                    cmbTipo.Text,
                    cmbConcepto.SelectedValue.ToString(),
                    txtConcepto2.Text,
                    txtCantidad.Text,
                    txtUnidad.Text,
                    txtDivisa1.Text,
                    txtTipoCambio1.Text,
                    Convert.ToDecimal(txtImporte1.Text),
                    Convert.ToDecimal(txtDescuento1.Text),
                    Convert.ToDecimal(txtTotal1.Text),
                    Convert.ToDecimal(txtPrecio.Text),
                    Convert.ToDecimal(txtImpuesto1.Text),
                    Convert.ToDecimal(txtDescuentoIm.Text),
                    Convert.ToDecimal(txtImpuestoIm.Text));
            }
            else
            {
                cot.InsertarPartidaCotizacion(
                    txtFolio.Text,
                    txtPartida.Text,
                    cmbTipo.Text,
                    cmbConcepto.SelectedValue.ToString(),
                    txtConcepto2.Text,
                    txtCantidad.Text,
                    txtUnidad.Text,
                    txtDivisa1.Text,
                    txtTipoCambio1.Text,
                    Convert.ToDecimal(txtImporte1.Text),
                    Convert.ToDecimal(txtDescuento1.Text),
                    Convert.ToDecimal(txtTotal1.Text),
                    Convert.ToDecimal(txtPrecio.Text),
                    Convert.ToDecimal(txtImpuesto1.Text),
                    Convert.ToDecimal(txtDescuentoIm.Text),
                    Convert.ToDecimal(txtImpuestoIm.Text));
            }

            cot.ActualizarTotalesCotizacion(txtFolio.Text);
            cot.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);

            return true;
        }

        private void TerminarCapturaPartidas()
        {
            int partida = Convert.ToInt32(txtPartida.Text) - 1;
            if (partida > 0)
            {
                cot.ActualizarTotalesCotizacion(txtFolio.Text);
                cot.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
            }
            PanelPartidasRequisicion.Visible = false;
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
        }

        private void btnLimpiarPartida_Click(object sender, EventArgs e)
        {
            LimpiarPartida();
        }

        private void btnEliminarPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }

            string mensaje = cot.EliminarPartidaCotizacion(txtFolio.Text, txtPartida.Text);
            if (!string.IsNullOrEmpty(mensaje))
            {
                MessageBox.Show(mensaje);
            }

            cot.ActualizarTotalesCotizacion(txtFolio.Text);
            cot.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas);
            cot.ReciboSaldosPartidas(txtFolio.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR);

            ConfigurarPartida(true);
            LimpiarPartida();
            CargarPartidas();
            cot.Consulta5Cotizacion(txtFolio.Text, txtPartida);
        }

        private void CargarPartidas()
        {
            cot.CargarPartidasCotizacion(guna2DataGridView1, txtFolio.Text);
        }

        void LimpiarPartida()
        {
            txtCantidad.Text = "1";
            txtUnidad.Clear();
            txtPrecio.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImporte1.Text = "0.00";
            txtImpuesto1.Text = "0.00";
            txtImpuestoIm.Text = "0.00";

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;

            cmbConcepto.DataSource = null;
            cmbConcepto.Items.Clear();
            txtConcepto2.Text = string.Empty;
        }

        /// <summary>
        /// Doble clic sobre una partida para editarla. Primero se
        /// desactivan los eventos de cmbTipo y cmbConcepto, se consulta la
        /// partida (que regresa TipoConcepto/ClaveConcepto crudos), se
        /// pone cmbTipo.Text, se repuebla cmbConcepto con
        /// CargarComboConceptos() y por último se selecciona la clave — en
        /// ese orden, porque cmbConcepto depende de cmbTipo.
        /// </summary>
        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            string partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();

            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;

            cot.ConsultaPartidaCotizacion(txtFolio.Text, partida, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1,
                txtImporte1, txtDescuento1, txtTotal1, txtPrecio, txtImpuesto1, txtConcepto2,
                out string tipoConcepto, out string claveConcepto);

            cmbTipo.Text = tipoConcepto;
            CargarComboConceptos();

            if (!string.IsNullOrEmpty(claveConcepto) && int.TryParse(claveConcepto, out int clave))
            {
                cmbConcepto.SelectedValue = clave;
            }

            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

            PanelPartidasRequisicion.Visible = true;
            txtPartida.Text = partida;

            ConfigurarPartida(!cmbEstatus.Text.Equals("Abierto", StringComparison.OrdinalIgnoreCase));
        }


        private void ConfigurarPartida(bool bloquear)
        {
            btnEliminarPartida.Visible = !bloquear;
            btnLimpiarPartida.Visible = !bloquear;
            btnConfirmarPartida.Visible = !bloquear;
            btnSiguientePartids.Visible = !bloquear;

            txtCantidad.Enabled = !bloquear;
            txtUnidad.Enabled = !bloquear;
            txtPrecio.Enabled = !bloquear;
            txtDescuento1.Enabled = !bloquear;
            txtImpuesto1.Enabled = !bloquear;
            cmbTipo.Enabled = !bloquear;
            cmbConcepto.Enabled = !bloquear;
            txtConcepto2.Enabled = !bloquear;
        }

        #endregion

        #region Cálculo de importes por partida (idéntico a Facturas)

        private void Calcular()
        {
            decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtCantidad.Text);
            txtImporte1.Text = Convert.ToString(sub);

            decimal descuento = (Convert.ToDecimal(txtDescuento1.Text) / 100) * sub;
            txtDescuentoIm.Text = descuento.ToString("N2");

            sub -= descuento;
            txtSubtotal1.Text = sub.ToString("N2");

            decimal impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * sub;
            txtImpuestoIm.Text = impuesto.ToString("N2");

            txtTotal1.Text = (sub + impuesto).ToString("N2");
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento1);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtImpuesto1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImpuesto1);
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                    Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de precio incorrecto");
            }
        }

        private void txtSubtotal1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSubtotal1);
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoIm);
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal1);
        }

        private void txtSubtotalR_TextChanged(object sender, EventArgs e) => Moneda(ref txtSubtotalR);
        private void txtDescuentoR_TextChanged(object sender, EventArgs e) => Moneda(ref txtDescuentoR);
        private void txtTotalR_TextChanged(object sender, EventArgs e) => Moneda(ref txtTotalR);

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e) => Utilerias.SoloNumFracc(sender, e);
        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e) => c.Monto(e);
        private void txtTotal1_KeyPress(object sender, KeyPressEventArgs e) => c.Monto(e);

        private void Moneda(ref Guna2TextBox txt)
        {
            try
            {
                string n = txt.Text.Replace(",", "").Replace(".", "");
                n = n.PadLeft(3, '0');
                double v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Vencimiento

        private void RecalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;
                int dias = Convert.ToInt32(diasv);
                DateTime fechaVence = Convert.ToDateTime(txtFecha.Text);
                fechaVence = fechaVence.AddDays(dias);
                txtFechaVence.Text = fechaVence.ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e) => RecalcularFechaVencimiento();
        private void txtFecha_ValueChanged(object sender, EventArgs e) => RecalcularFechaVencimiento();

        #endregion

        #region Confirmación (bloqueo)

        private void btnTerminarFactura_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar una cotización Bloqueada o Cancelada");
                return;
            }
            if (MessageBox.Show("Al confirmar la cotización no podra realizar modificaciones, ¿Desea continuar?",
                "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            Matricula = string.Empty;
            cmbEstatus.Text = "Bloqueado";

            cot.ActualizarCotizacionEstatus(txtFolio.Text, "Bloqueado");

            MessageBox.Show("La cotización se confirmó exitosamente");

            if (MessageBox.Show("¿Imprimir Documento?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                ReporteComprobanteCotizacion r = new ReporteComprobanteCotizacion(txtFolio.Text);
                r.ShowDialog();
            }

            Limpiar();
            LimpiarPartida();

            cot.CargarCotizaciones(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);

            guna2TabControl1.SelectedIndex = 0;
            btnTerminarFactura.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la cotización");
                return;
            }
            if (cmbEstatus.Text != "Bloqueado" && cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible cancelar una cotización que no esta bloqueada o abierta");
                return;
            }
            if (MessageBox.Show("La cotización será cancelada, ¿Desea continuar?", "Cotización",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cot.CancelarCotizacion(txtFolio.Text);
                cmbEstatus.Text = "Cancelado";
                Limpiar();
            }
        }

        #endregion

        #region Consulta / filtros / grillas de encabezado

        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            CargarEncabezadoDesdeGrilla(DataGridView2.Rows[e.RowIndex].Cells["Folio"].Value.ToString());
        }

        private void CargarEncabezadoDesdeGrilla(string folio)
        {
            txtFolio.Text = "X";

            cot.ConsultaCotizacion(folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa,
                txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtPartidas, txtNotas, txtElaborado,
                txtFolio, txtConsecutivo,
                txtNombreProspecto, txtRFCProspecto, txtDomicilioProspecto, txtContactoProspecto, txtCelularProspecto,
                out string claveCliente, out bool prospecto, cmbCentroCostos);

            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            txtNotas.Enabled = false;
            tgProspecto.Checked = prospecto;

            if (prospecto)
            {
                txtMatricular.Text = string.Empty;
                Matricula = string.Empty;
                ConfigurarClienteOProspecto(hayCliente: false);
            }
            else
            {
                txtMatricular.Text = claveCliente;
                Matricula = claveCliente;
 
            }

            string[] valores = c.InformacionDocumento2(txtClave.Text);
            txtDocumento.Text = valores[0];
            txtClave.Text = valores[1];
            cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

            guna2GradientPanel2.Visible = false;
            CargarPartidas();

            if (cmbEstatus.Text != "Abierto")
            {
                // Fuera de "Abierto" no se va a capturar nada nuevo; se
                // deja el combo de conceptos vacío hasta que se elija Tipo.
                cmbConcepto.DataSource = null;
                cmbConcepto.Items.Clear();
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e) => RecargarGrillasCotizaciones();
        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e) => RecargarGrillasCotizaciones();
        private void txtFiltroNombre_TextChanged(object sender, EventArgs e) => RecargarGrillasCotizaciones();

        private void RecargarGrillasCotizaciones()
        {
            cot.CargarCotizaciones(DataGridView2, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text, true);
        }

        #endregion

        #region Imprimir / Enviar correo

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                MessageBox.Show("Es necesario seleccionar una cotización");
                return;
            }

            ReporteComprobanteCotizacion r = new ReporteComprobanteCotizacion(txtFolio.Text);
            r.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty || (txtMatricular.Text == string.Empty && txtNombreProspecto.Text == string.Empty))
            {
                MessageBox.Show("Seleccione la cotización para enviar el correo");
                return;
            }

            if (esProspecto)
            {
                MessageBox.Show("No es posible enviar correo automáticamente: el prospecto no tiene una dirección de correo registrada. Envíelo manualmente o registre al prospecto como cliente.");
                return;
            }

            string[] valores = cl.InformacionCliente(txtMatricular.Text);

            ReporteComprobanteCotizacion r = new ReporteComprobanteCotizacion(txtFolio.Text);
            string carpeta = Utilerias.SavePDF(r.reportViewer1, "Cotizacion", txtDocumento.Text, txtConsecutivo.Text);

            bool enviado = CorreosMasivos.EnviarCorreos(
                "Cotización",
                @"<html>
                    <body style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>
                        <p>Estimado Cliente,</p>
                        <p>Enviamos su cotización correspondiente.</p>
                        <p>Si tiene alguna duda o requiere realizar algún ajuste, no dude en contactar a su vendedor,
                           quien estará encantado en asistirle.</p>
                        <p>Reciban un cordial saludo y que tengan un excelente día.</p>
                    </body>
                  </html>",
                Utilerias.ConvertirReportViewerAPdf(r.reportViewer1),
                "Cotizacion-" + txtConsecutivo.Text + ".pdf",
                valores[5]);

            if (enviado)
            {
                MessageBox.Show("Correo enviado exitosamente");
            }
        }

        #endregion

        #region Centro de costos

        private void LlenarComboCentro()
        {
            try
            {
                DataTable centros = cc.ConsultarTodos();
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbCentroCostos.DataSource = centros;
                cmbCentroCostos.DisplayMember = "Nombre";
                cmbCentroCostos.ValueMember = "Clave";
                cmbCentroCostos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Barra lateral (toolStrip2) - menú NUEVO / CONSULTAR / IMPRIMIR / ENVIAR CORREO

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "NUEVO":
                    OnMenuNuevo();
                    break;
                case "CONSULTAR":
                    guna2GradientPanel2.Visible = !guna2GradientPanel2.Visible;
                    if (guna2GradientPanel2.Visible) guna2GradientPanel2.BringToFront(); else guna2GradientPanel2.SendToBack();
                    ConfigurarConsulta();
                    break;
                case "IMPRIMIR":
                    ReporteComprobanteCotizacion reporte = new ReporteComprobanteCotizacion(txtFolio.Text);
                    reporte.ShowDialog();
                    break;
                case "ENVIAR CORREO":
                    button11_Click(sender, e);
                    break;

            }

            ConfigurarToolStripCompacto();
        }

        private void OnMenuNuevo()
        {
            if (cmbEstatus.Text != "Abierto")
            {
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
            }
            else if (cmbEstatus.Text == "Abierto" && cmbDocumento.Text == string.Empty)
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
            }
            else
            {
                MessageBox.Show("Confirme la cotización antes de continuar");
            }

            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Enabled = true;

            guna2Button2.Visible = true;
            btnEliminarPartida.Visible = true;
            btnLimpiarPartida.Visible = true;
            btnConfirmarPartida.Visible = true;
            btnSiguientePartids.Visible = true;

            guna2Button2.Enabled = true;
            btnEliminarPartida.Enabled = true;
            btnLimpiarPartida.Enabled = true;
            btnConfirmarPartida.Enabled = true;
            btnSiguientePartids.Enabled = true;

            cot.CargarPartidasCotizacion(guna2DataGridView1, txtFolio.Text);
        }

        private void ConfigurarConsulta()
        {
            cmbDocumento.Enabled = false;
            btnEliminarPartida.Visible = true;
            btnLimpiarPartida.Visible = true;
            btnConfirmarPartida.Visible = true;
            btnSiguientePartids.Visible = true;

            guna2Button2.Enabled = false;
            btnEliminarPartida.Enabled = false;
            btnLimpiarPartida.Enabled = false;
            btnConfirmarPartida.Enabled = false;
            btnSiguientePartids.Enabled = false;
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

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1 && string.IsNullOrEmpty(txtFolio.Text))
            {
                MessageBox.Show("Es necesario crear el encabezado");
                guna2TabControl1.SelectedIndex = 0;
            }
        }

        private void txtPartidas_TextChanged(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && !string.IsNullOrEmpty(txtPartidas.Text) && txtPartidas.Text != "0")
            {
                btnTerminarFactura.Visible = true;
            }
        }

        #endregion

        #region Compatibilidad con el Designer copiado de Facturas/OrdenPedidoCliente


        private void OrdenCompra2_Load(object sender, EventArgs e) => Cotizaciones_Load(sender, e);
        private void OrdenCompra2_Activated(object sender, EventArgs e) => Cotizaciones_Activated(sender, e);

        // Handlers que en el original eran no-ops y que el Designer puede
        // seguir teniendo enganchados:
        private void label9_Click(object sender, EventArgs e) { }
        private void label37_Click(object sender, EventArgs e) { }
        private void label49_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e) { }
        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e) { }
        private void txtNombreAlumnno_TextChanged(object sender, EventArgs e) { }
        private void txtTipoCambio_TextChanged(object sender, EventArgs e) { }
        private void txtSubtotal1_TextChanged_1(object sender, EventArgs e) { }
        private void btnDocumento_Click(object sender, EventArgs e) { }
        private void txtCantidad_Leave(object sender, EventArgs e) { }



        #endregion

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtRecargo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtRecargo);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtImporte1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporte1);
        }

        private void txtDescuentoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoIm);
        }

        private void tgProspecto_CheckedChanged(object sender, EventArgs e)
        {
            if (tgProspecto.Checked)
            {
                txtMatricular.Visible = false;
                txtNombreAlumnno.Visible = false;
                btnCliente.Visible = false;

                txtNombreProspecto.Visible = true;
                txtRFCProspecto.Visible = true;
                txtContactoProspecto.Visible = true;
                txtCelularProspecto.Visible = true;
                txtDomicilioProspecto.Visible = true;

                lblNombre.Visible = true;
                lblRFC.Visible = true;
                lblDomicilio.Visible = true;
                lblContacto.Visible = true;
                lblCelular.Visible = true;
            }
            else
            {
                txtMatricular.Visible = true;
                txtNombreAlumnno.Visible = true;
                btnCliente.Visible = true;
                txtNombreProspecto.Visible = false;
                txtRFCProspecto.Visible = false;
                txtContactoProspecto.Visible = false;
                txtCelularProspecto.Visible = false;
                txtDomicilioProspecto.Visible = false;

                lblNombre.Visible = false;
                lblRFC.Visible = false;
                lblDomicilio.Visible = false;
                lblContacto.Visible = false;
                lblCelular.Visible = false;
            }
        }
    }
}