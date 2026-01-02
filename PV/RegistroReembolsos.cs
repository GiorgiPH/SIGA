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
        public static string Matricula = string.Empty;
        private List<(string clase, string Tipo, decimal Valor)>  conceptosAplicadosImpuestos = new List<(string clase, string Tipo, decimal Valor)>();
        private List<(string clase, string Tipo, decimal Valor, string clavebase, string clasebase, decimal valorbase)> conceptosAplicadosDescuentos = new List<(string clase, string Tipo, decimal Valor, string clavebase, string clasebase, decimal valorbase)>();
        private List<(string clase, string Tipo, decimal Valor)> conceptosAplicadosGlobales = new List<(string clase, string Tipo, decimal Valor)>();

        // DBOrdenCompra c = new DBOrdenCompra();
        DBRegistroReembolso r = new DBRegistroReembolso();

        public static string Carpeta = string.Empty;
        string proyecto = string.Empty;

        DBCentroCostos cc = new DBCentroCostos();
        DBProveedores p = new DBProveedores();
        DBConceptosGloablesReembolso c = new DBConceptosGloablesReembolso();



        string recibo = string.Empty;
        string reciboCol = string.Empty;
        public static string Carpeta1 = string.Empty;
        int opcion = 0;
        int Partidas = 0;
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


        public RegistroReembolsos()
        {
            InitializeComponent();
            for (int i = 1; i <= 52; i++)
            {
                cmbSemana.Items.Add($"Semana {i}");
            }
            // En el diseñador o constructor:
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbConcepto.AutoCompleteMode = AutoCompleteMode.None;
            cmbConcepto.AutoCompleteSource = AutoCompleteSource.None;


            /*cmbConcepto.AutoCompleteMode = AutoCompleteMode.None;
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbConcepto.TextChanged += cmbConcepto_TextUpdate;
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
            */
        }
        private void LlenarComboCentro()
        {
            try
            {
                DataTable menus = cc.ConsultarTodos();

                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbCentroCostos.DataSource = menus;
                cmbCentroCostos.DisplayMember = "Nombre"; // Campo visible
                cmbCentroCostos.ValueMember = "Clave";   // Campo interno
                cmbCentroCostos.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio
                //cmbCentroCostosAlterno.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                //cmbCentroCostosAlterno.DataSource = menus;
                //cmbCentroCostosAlterno.DisplayMember = "Nombre"; // Campo visible
                //cmbCentroCostosAlterno.ValueMember = "Clave";   // Campo interno
                //cmbCentroCostosAlterno.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarComboGastos()
        {
            try
            {
                DataTable menus;

                if (!string.IsNullOrEmpty(txtOrden.Text))
                {
                    // Obtener productos vinculados a la orden
                    menus = r.ObtenerProductosGastoPorOrden(txtOrden.Text);
                }
                else
                {
                    // Obtener todos los productos activos
                    menus = r.ObtenerProductosGasto();
                }

                // Configurar estilo y autocompletado
                cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbConcepto.DataSource = menus;
                cmbConcepto.DisplayMember = "Descripcion"; // Campo visible
                cmbConcepto.ValueMember = "ClaveServicio";   // Campo interno
                cmbConcepto.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio


                cmbConcepto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbConcepto.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
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

                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbProveedroAlterno.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbProveedroAlterno.DataSource = menus;
                cmbProveedroAlterno.DisplayMember = "RazonSocial"; // Campo visible
                cmbProveedroAlterno.ValueMember = "IdProveedor";   // Campo interno
                cmbProveedroAlterno.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                cmbProveedroAlterno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedroAlterno.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarComboFormasPago(string centro, string proyecto)
        {
            try
            {
                DataTable menus = r.ObtenerFormasPagoPorProyecto(centro, proyecto);

                menus.Columns.Add("DisplayColumn", typeof(string), "DescripcionFormaPago + ' ' + DescripcionReferencia");

                // Configurar estilo y autocompletado
                cmbformapago.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbformapago.DataSource = menus;
                cmbformapago.DisplayMember = "DisplayColumn"; // Campo visible
                cmbformapago.ValueMember = "Id";   // Campo interno
                cmbformapago.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    decimal subtotal = Convert.ToDecimal(pago["Subtotal"]);
                    decimal valor = 0;

                    // Determinar valor del concepto
                    if (clase == "Descuento")
                        valor = Convert.ToDecimal(pago["descuento"]);
                    else
                        valor = Convert.ToDecimal(pago["cargo"]);

                    decimal importeMostrado = 0;

                    if (tipo == "Porcentaje")
                        importeMostrado = (valor / 100m) * subtotal;
                    else // Importe fijo
                        importeMostrado = valor;

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



        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            //
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

        private void btnarticulos_Click(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una recepcion deproductos Bloqueada o Cancelada");
                return;
            }
            else if (string.IsNullOrEmpty(cmbCentroCostos.Text))
            {
                MessageBox.Show("No es posible agregar partidas a una recepcion deproductos Bloqueada o Cancelada");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(cmbSemana.Text))
                {
                    MessageBox.Show("Selecciona la semana");
                    return;
                }
                string FolioOrden = txtOrdenCompra.Text;

                if (txtFolio.Text == string.Empty)
                {
                    r.InsertarRegistroGasto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, FolioOrden, txtConsecutivo.Text, txtReferencia.Text, txtCondominio.Text, txtDiasVence.Text, txtFechaVence.Text, cmbCentroCostos?.SelectedValue?.ToString(), cmbSemana.SelectedIndex + 1, dtpAnio.Text, cmbProoveedorAlternoSiNo.Text, cmbproyecto.Text,txtRetencion.Text);
                }
                int opcion = 0;
                if (txtArchivo.Text != string.Empty)
                {
                    opcion = 1;
                }
                guna2TabControl1.SelectedIndex = 1;


                TxtFolio1.Text = txtFolio.Text;
                txtOrden.Text = FolioOrden;
                opcion = opcion;

                if (txtOrden.Text != string.Empty)
                {
                    LlenarComboGastos();
                    MessageBox.Show("buscando error 0");
                    r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                    MessageBox.Show("buscando error 1");
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
                    //   MessageBox.Show("buscando error 2");
                    r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                    //MessageBox.Show("buscando error 3");
                    txtPrecio.Enabled = true;
                    txtCantidad.Text = "1";
                    txtUnidad.Text = "Servicio";
                    txtDivisa1.Text = "MXN";
                    txtTipoCambio1.Text = "1.00";


                    proyecto = cmbproyecto.Text;
                    dtpFecha.Value = DateTime.Now;

                    //txtImpuesto1.SelectedIndex = 0;
                }

                if (opcion != 0)
                {
                    button11.Enabled = false;
                    button12.Enabled = false;
                    button13.Enabled = false;
                }
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text == string.Empty)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            else if (cmbOrdenCompra.Text == string.Empty && cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar registro de gastos Bloqueada o Cancelada");
                return;
            }
            else if (MessageBox.Show("Al confirmar el registro de gasto no podra realizar modificaciones, ¿Desea continuar?", "Registro de Gasto ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Matricula = string.Empty;
                cmbEstatus.Text = "Bloqueado";
                r.ActualizarReembolso(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                r.ActualizarSaldoProveedor2(txtMatricular.Text, Convert.ToDecimal(txtTotal.Text));
                r.CargarGasto(dataGridView1);
            }
            Limpiarcabezado();
            LimpiarDetalle();

            guna2TabControl1.SelectedIndex = 0;
            guna2Button9.Visible = true;
            guna2TabControl1.Enabled = false;
            guna2DataGridView1.Rows.Clear();
            txtFolio.Text = String.Empty;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            dataGridView2.Rows.Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
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
                MessageBox.Show(r.CancelarRegistroGasto(txtFolio.Text));
                Limpiarcabezado();
                LimpiarDetalle();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCondominio.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            //  button6.BackColor = Color.Gainsboro;
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
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCondominio.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            // button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;

            if (DBRegistroReembolso.Ruta != string.Empty)
            {
                if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;

                    Carpeta = DBRegistroReembolso.Ruta + @"\" + "EG" + NoOrdenResl;

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
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCondominio.DroppedDown = false;
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

            if (DBRegistroReembolso.Ruta != string.Empty)
            {
                string FolioOrden = txtOrdenCompra.Text;
                int opcion = 0;
                if (txtFolio.Text == string.Empty)
                {
                    r.InsertarRegistroGasto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, FolioOrden, txtConsecutivo.Text, txtReferencia.Text, txtCondominio.Text, txtDiasVence.Text, txtFechaVence.Text, cmbCentroCostos.SelectedValue.ToString(), cmbSemana.SelectedIndex + 1, dtpAnio.Text, cmbProoveedorAlternoSiNo.Text, cmbproyecto.Text,txttotalretenciones.Text);
                    opcion = 1;
                }

                if (txtFolio.Text != string.Empty)
                {

                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;

                    Carpeta = DBRegistroReembolso.Ruta + @"\" + "EG" + NoOrdenResl;

                    try
                    {
                        if (Directory.Exists(Carpeta))
                        {

                        }
                        else
                        {
                            Directory.CreateDirectory(Carpeta);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    Carpeta = DBRegistroReembolso.Ruta + @"\" + "EG" + NoOrdenResl;

                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "All Files|*.*";

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string archivo = open.FileName;
                        string ext = Path.GetExtension(archivo);
                        try
                        {
                            File.Copy(archivo, Carpeta + @"\" + Descripcion + ext);
                            txtArchivo.Text = Descripcion + ext;
                            r.ModificarExtension5gasto(txtFolio.Text, txtClave.Text, ext);
                            r.ActualizarRecepcion3gasto(txtFolio.Text, txtClave.Text, txtArchivo.Text);

                            if (opcion == 1)
                            {
                                /*
                                PartidaGastos partidas = new PartidaGastos(txtFolio.Text, txtDocumento.Text, FolioOrden, opcion);
                                partidas.ShowDialog();*/

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

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty)
                {
                    string[] valores = r.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];

                    if (txtFolio.Text == string.Empty)
                    {
                        r.ConsecutivoGasto(txtConsecutivo, txtClave.Text);
                    }
                }
            }
        }

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

        private void button4_Click(object sender, EventArgs e)
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
            cmbCondominio.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbDocumento.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            cmbDocumento.DroppedDown = false;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            //  button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
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

                cmbCondominio.DroppedDown = false;
                cmbFiltroDocumentoC.DroppedDown = false;
                cmbProveedor.DroppedDown = false;
                cmbOrdenCompra.DroppedDown = false;
                cmbDocumento.DroppedDown = false;
                button3.BackColor = Color.Gainsboro;
                txtReferencia.BackColor = Color.White;
                txtNotas.BackColor = Color.White;
                button2.BackColor = Color.Gainsboro;
                //  button6.BackColor = Color.Gainsboro;
                txtDiasVence.BackColor = Color.White;
                button7.BackColor = Color.Gainsboro;
                //Limpiar();
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

            cmbCondominio.DroppedDown = false;
            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
            btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            //button6.BackColor = Color.Gainsboro;
            cmbDocumento.DroppedDown = false;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            cmdproyectoalterno.Enabled = false;

            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("Solo se puede agregar partidas si el documento esta abierto");
                return;
            }
            PanelPartidasRequisicion.Visible = true;
            LimpiarDetalle();
            r.ConsultaGasto(TxtFolio1.Text, txtPartida);
            PanelPartidasRequisicion.BringToFront();
            button11.Enabled = true;
            PartidaNuevaConsulta = "NO";
            cmdproyectoalterno.Items.Clear();

            cmdproyectoalterno.Items.Add(proyecto);
            cmdproyectoalterno.SelectedIndex = 0;
            //r.SeleccionarProductoGasto(cmbConcepto);
            LlenarComboGastos();
            DesbloquearDetalle();
            LlenarComboFormasPago(cmbCentroCostos.Text, cmbproyecto.Text);

            cmdproyectoalterno.Enabled = false;

        }

        private void cmbConcepto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (txtOrden.Text != string.Empty)
                {
                    string[] valores = r.InformacionGastoo(cmbConcepto.Text, txtOrden.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    //txtDescuento1.Text = valores[3];
                    txtTotal1.Text = valores[4];
                    txtConcepto2.Text = valores[5];
                    txtPartidaOrden.Text = valores[7];
                    txtCantidad2.Text = valores[8];
                    txtCantidad.Text = valores[8];
                    txtImpuesto12.Text = valores[9];
                    txtUnidad.Text = valores[10];
                }
                else
                {
                    string[] valores = r.InformacionGasto(cmbConcepto.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtUnidad.Text = valores[3];
                    txtImpuesto12.Text = valores[4];
                }

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();

            }
        }

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

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Todos los archivos|*.*";

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

                    // Si quieres actualizar la base con el nombre/extension ya, puedes dejar esto:
                    r.ModificarExtension4gasto(noOrden, descripcion, extensionArchivo);
                    r.ActualizarRecepcion2gasto(noOrden, descripcion, nombreArchivo);
                    /*
                    byte[] contenidoArchivo;
                    string nombreArchivo = Path.GetFileName(rutaArchivoSeleccionado);
                    string tipoArchivo = Path.GetExtension(rutaArchivoSeleccionado).TrimStart('.');

                    using (FileStream fs = new FileStream(rutaArchivoSeleccionado, FileMode.Open, FileAccess.Read))
                    {
                        contenidoArchivo = new byte[fs.Length];
                        fs.Read(contenidoArchivo, 0, (int)fs.Length);
                    }

                    GuardarArchivoEnBaseDeDatos(folioGasto, partida, claveProducto, nombreArchivo, tipoArchivo, contenidoArchivo);
                    */
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
            }
            else
            {
                // Si rutaCompletaArchivo está vacía, intenta reconstruirla desde los datos
                string noOrden = TxtFolio1.Text;
                string carpeta = Path.Combine(DBOrdenCompra.Ruta, "G" + noOrden);
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
        }

        private void button13_Click(object sender, EventArgs e)
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

            // Usamos rutaCompletaArchivo si está definida, de lo contrario la reconstruimos
            string rutaArchivo = !string.IsNullOrWhiteSpace(rutaCompletaArchivo)
                ? rutaCompletaArchivo
                : Path.Combine(DBOrdenCompra.Ruta, "G" + TxtFolio1.Text, txtArchivo1.Text);

            try
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                    txtArchivo1.Clear();
                    rutaCompletaArchivo = string.Empty;
                    nombreArchivo = string.Empty;
                    extensionArchivo = string.Empty;

                    r.ModificarExtension3gasto(TxtFolio1.Text, txtPartida.Text);

                    MessageBox.Show("Archivo eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("El archivo no existe o ya fue eliminado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el archivo: " + ex.Message);
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
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
                //r.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal(txtDescuento1.Text.Replace(",", "")), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto1.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), cmbCentroCostosAlterno?.SelectedValue?.ToString(), txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""));
                r.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal("0.00"), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto12.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), "0", txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""), cmdproyectoalterno.Text, dtpFecha.Text, cmbformapago.Text, cmbreferencia.Text, txtIEPS.Text.Replace(",", ""), txtRetencion.Text.Replace(",", ""), txtPrecio.Text.Replace(",", ""));
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text.Replace(",", ""));
                
                //c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text);
                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                r.Consulta5RegistroGasto(TxtFolio1.Text, txtPartida);
                r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR, new Guna2TextBox(), new Guna2TextBox(), new Guna2TextBox());
                LimpiarDetalle();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);


                LlenarComboGastos();
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        r.ActualizarGasto(TxtFolio1.Text, Partida.ToString());
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
                if (cmbProoveedorAlternoSiNo.Text == "Si" && string.IsNullOrEmpty(cmbProveedroAlterno.Text))
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor alterno");
                    return;
                }
                /*r.InsertarPartidaGasto(
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
                 );*/
                r.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal("0.00"), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto12.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), "0", txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""), cmdproyectoalterno.Text, dtpFecha.Text, cmbformapago.Text, cmbreferencia.Text, txtIEPS.Text.Replace(",", ""), txtRetencion.Text.Replace(",", ""), txtPrecio.Text.Replace(",", ""));
                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                LimpiarDetalle();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);

            }
            PanelPartidasRequisicion.Visible = false;
            guna2Button9.Visible = true;
            r.CargarRecibosPartidasGasto(guna2DataGridView1, TxtFolio1.Text);
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
            MessageBox.Show(r.EliminarPartidaRegistroGasto(txtFolio.Text, txtPartida.Text));
            string maximo = r.ObtenerTotalPartidaRegistroGasto(txtFolio.Text);
            r.ActualizarGasto(txtFolio.Text, maximo);
            r.ReciboSaldosGastos(txtFolio.Text, txtSubtotalR, txtDescuentoR, txtImpuestoR, txtTotalR, txtPartidas, txtSaldo);
            r.CargarRecibosPartidasGasto(guna2DataGridView1, txtFolio.Text);
            SumarColumnasPartida();

            LimpiarDetalle();
        }

        void LimpiarDetalle()
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
            //txtDescuento1.Text = "0.00";
            txtDescuentoIm.Text = "0.00";
            txtImpuestoIm.Text = "0.00";
            txtTotal1.Text = "0.00";
            txtIEPS.Text = "0.00";
            txtRetencion.Text = "0.00";
            cobraIEPS = false;
            txtIEPS.Enabled = false;
            txtArchivo1.Text = string.Empty;
            txtUnidad.Text = string.Empty;
            txtSubtotalR.Text = "0.00";
            txtImpuestoR.Text = "0.00";
            txtDescuentoR.Text = "0.00";
            txtTotalR.Text = "0.00";
            cmbProveedroAlterno.SelectedIndex = -1;
            //cmbCentroCostosAlterno.SelectedIndex = -1;
            cmdproyectoalterno.SelectedIndex = -1;
            cmbConcepto.SelectedIndex = -1;
            //txtImpuesto1.SelectedIndex = -1;
            cmbformapago.SelectedIndex = -1;
            cmbreferencia.SelectedIndex = -1;
            txtRFC.Text = "";

            //cmbCentroCostosAlterno.Text = cmbproyecto.Text;
            dataGridView2.Rows.Clear();
            dtpFecha.Value = DateTime.Now;
            dataGridView2.Rows.Clear();
            conceptosAplicadosDescuentos.Clear();
            conceptosAplicadosImpuestos.Clear();

        }

        void BloquearEncabezado()
        {
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            cmbCondominio.Enabled = false;
            txtCondominio.Enabled = false;
            cmbFiltroDocumentoC.Enabled = false;
            cmbProveedor.Enabled = false;
            cmbOrdenCompra.Enabled = false;
            txtReferencia.Enabled = false;
            txtNotas.Enabled = false;
            txtArchivo.Enabled = false;
            btLimpiarOrden.Enabled = false;
            button4.Enabled = false;
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
        void DesbloquearEncabezado()
        {
            cmbDocumento.Enabled = true;
            txtDiasVence.Enabled = true;
            cmbCondominio.Enabled = true;
            txtCondominio.Enabled = true;
            cmbFiltroDocumentoC.Enabled = true;
            cmbProveedor.Enabled = true;
            cmbOrdenCompra.Enabled = true;
            txtReferencia.Enabled = true;
            txtNotas.Enabled = true;
            txtArchivo.Enabled = true;
            btLimpiarOrden.Enabled = true;
            button4.Enabled = true;
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

        void BloquearDetalle()
        {
            cmbConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtImpuesto12.Enabled = false;
            //txtDescuento1.Enabled = false;
            txtRFC.Enabled = false;
            cmbProveedroAlterno.Enabled = false;
            //cmbCentroCostosAlterno.Enabled = false;
            cmbCentroCostos.Enabled = false;
            dtpAnio.Enabled = false;
            cmbSemana.Enabled = false;
            cmbProoveedorAlternoSiNo.Enabled = false;
            cmbproyecto.Enabled = false;
            cmdproyectoalterno.Enabled = false;
            cmbformapago.Enabled = false;
            cmbreferencia.Enabled = false;
            dtpFecha.Enabled = false;
            cmbreferencia.Enabled = false;
            //txtImpuesto1.Enabled = false;
        }
        void DesbloquearDetalle()
        {
            cmbConcepto.Enabled = true;
            txtConcepto2.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtImpuesto12.Enabled = true;
            //txtDescuento1.Enabled = true;
            cmbProveedroAlterno.Enabled = true;
            //cmbCentroCostosAlterno.Enabled = true;
            cmbproyecto.Enabled = true;
            cmdproyectoalterno.Enabled = true;

            cmbformapago.Enabled = true;
            dtpFecha.Enabled = true;
            cmbreferencia.Enabled = true;
            //txtImpuesto1.Enabled = true;

        }

        void Limpiarcabezado()
        {
            CentroCosto = string.Empty;
            txtConsecutivo.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtDiasVence.Text = string.Empty;
            txtFechaVence.Text = string.Empty;
            txtCondominio.Text = string.Empty;

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

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                txtFolio.Text = "";
                consultaRegistros = "NO";
                guna2DataGridView1.Rows.Clear();

                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 621);
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

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

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


                r.SeleccionarRecepcionProducto(cmbDocumento);
                r.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
                r.SeleccionarCondomini2(cmbCondominio);
                r.ruta();
                //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
                r.CargarGasto(dataGridView1);
                cmbEstatus.SelectedIndex = 0;
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                txtDivisa.Text = "MXN";
                txtTipoCambio.Text = "1.00";
                txtElaborado.Text = DBLogin.usuario;
                cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
                cmbCondominio.SelectedIndex = 0;
                txtDiasVence.Text = "0";
                int Dias = Convert.ToInt32(txtDiasVence.Text);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                guna2TabControl1.Enabled = true;

            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2DataGridView1.Rows.Clear();
                consultaRegistros = "SI";

                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 621);
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


                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

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
                //  guna2GradientPanel4.Size = new Size(22, 569);


                guna2TabControl1.Enabled = true;

                BloquearDetalle();
                BloquearEncabezado();
                guna2TabControl1.Enabled = true;

            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
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

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                ReporteComprasReembolso r = new ReporteComprasReembolso(TxtFolio1.Text);
                r.ShowDialog();
            }
            else if (e.ClickedItem.Text == "PROVEEDORES")
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

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                actualizarproveedor = "Si";

                Proveedores p = new Proveedores();
                p.ShowDialog();
            }
            else if (e.ClickedItem.Text == "SERVICIOS")
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

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                int Consulta = 0;
                actualizarcombo = "Si";
                CatalogoServicios s = new CatalogoServicios(Consulta);
                s.ShowDialog();

            }
        }

        private void RegistroReembolsos_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                // txtMatricular.Text = Matricula;

                //// r.ReciboSaldosGastos(txtFolio.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);

                // if (txtPartidas.Text == string.Empty)
                // {
                //     txtPartidas.Text = "0";
                // }
                // else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                // {
                //     button1.BackColor = Color.Red;
                // }
            }
        }

        private void RegistroReembolsos_Load(object sender, EventArgs e)
        {
            r.SeleccionarRecepcionProducto(cmbDocumento);
            r.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
            r.SeleccionarCondomini2(cmbCondominio);
            LlenarComboProveedores();
            LlenarComboCentro();
            LlenarComboProveedores();
            r.ruta();
            //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
            r.CargarGasto(dataGridView1);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
            cmbCondominio.SelectedIndex = 0;
            txtDiasVence.Text = "0";
            cmbProoveedorAlternoSiNo.Text = "Si";
            int Dias = Convert.ToInt32(txtDiasVence.Text);
            DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
            FechaVence = FechaVence.AddDays(Dias);
            txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");


            /*
                        cmbProveedroAlterno.DropDownStyle = Guna.UI2.WinForms.Guna2ComboBox.
                            .DropDown; // permite escribir texto
                        cmbProveedroAlterno.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // sugerencias y autocompletado
                        cmbProveedroAlterno.AutoCompleteSource = AutoCompleteSource.ListItems; // usar los ítems cargados
                        cmbProveedroAlterno.TextChanged += cmbProveedroAlterno_TextChanged;
            */
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbConcepto.TextChanged += cmbConcepto_TextUpdate;

        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores2 = r.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores2[0];
            }
        }
        private void Calcular()
        {
            decimal precio = 0, cantidad = 0, impuestoPorcentaje = 0, IEPS = 0, descuentoTotal = 0, retencion;
            decimal.TryParse(txtPrecio.Text, out precio);
            decimal.TryParse(txtCantidad.Text, out cantidad);
            decimal.TryParse(txtDescuentoIm.Text, out descuentoTotal);
            decimal.TryParse(txtRetencion.Text, out retencion);
            decimal.TryParse(txtIEPS.Text, out IEPS);
            descuentoTotal = 0; retencion = 0 ; impuestoPorcentaje = 0;
            decimal sub = precio * cantidad- descuentoTotal;
            //decimal descuentoTotal = 0;

            /*foreach (var concepto in conceptosAplicadosDescuentos)
            {
                if (concepto.clase == "Descuento")
                {
                    decimal baseCalculo = sub;

                    // Si el descuento depende de otro concepto (ej: IVA16), ajustar base
                    if (!string.IsNullOrWhiteSpace(concepto.clavebase))
                    {
                        if (concepto.clasebase == "Porcentaje")
                        {
                            // Si el concepto base (ej. IVA16) es porcentaje, lo aplicas al subtotal
                            baseCalculo = sub * (concepto.valorbase / 100m);
                        }
                        else if (concepto.clasebase == "Importe")
                        {
                            // Si es importe, lo sumas al subtotal
                            baseCalculo = sub + concepto.valorbase;
                        }
                    }

                    // Ahora aplicas el descuento como porcentaje o importe sobre base ajustada
                    if (concepto.Tipo == "Porcentaje")
                    {
                        retencion += (concepto.Valor / 100m) * baseCalculo;
                    }
                    else if (concepto.Tipo == "Importe")
                    {
                        retencion += concepto.Valor;
                    }
                }
            }*/
            foreach (var concepto in conceptosAplicadosDescuentos)
            {
                if (concepto.clase == "Descuento")
                {
                    if (concepto.Tipo == "Porcentaje")
                    {                //MessageBox.Show("valor ret: " +concepto.Valor / 100 +":sub :"+ sub);
                        retencion += (concepto.Valor / 100) * sub;
                    }
                    else if (concepto.Tipo == "Importe")
                        retencion += concepto.Valor;
                }
            }


            txtRetencion.Text = retencion.ToString("N2");


            Dictionary<string, decimal> impuestosCalculados = new Dictionary<string, decimal>();
            decimal totalImpuesto = 0;
            decimal impuestoTotal = 0;
            foreach (var concepto in conceptosAplicadosImpuestos)
            {
                if (concepto.clase == "Impuesto")
                {   //   sub -= descuentoTotal;
                    if (concepto.Tipo == "Porcentaje")                 
                    impuestoTotal += (concepto.Valor / 100) * sub;
                    else if (concepto.Tipo == "Importe")
                        impuestoTotal += concepto.Valor;
                }
            }
            txtImpuestoIm.Text = impuestoTotal.ToString("N2");
            txtTotal1.Text = (sub + impuestoTotal + IEPS- retencion).ToString("N2");
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
                decimal valor = Convert.ToDecimal(row["Descuento"]);
                //string clavebase = row["clavebase"].ToString();
                //string clasebase = row["clasebase"].ToString().Trim();
                //decimal valorbase = Convert.ToDecimal(row["valorbase"]);
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
                decimal valor = Convert.ToDecimal(row["Cargo"]);
                
                conceptosAplicadosImpuestos.Add((clase, tipo, valor));

            }
        }
        private void RecargarConceptosGlobales()
        {
            conceptosAplicadosGlobales.Clear();

            var dt = c.CargarConceptosGlobalesExistentes(txtFolio.Text);
            foreach (DataRow row in dt.Rows)
            {

                string tipo = row["Tipo"].ToString();
                string clase = row["Clase"].ToString().Trim();
                decimal valor = Convert.ToDecimal(row["importe"]);
                conceptosAplicadosGlobales.Add((clase, tipo, valor));

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
            //   Moneda(ref txtImpuesto12);

            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }



        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
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
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
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

        private void cmbConcepto_Click(object sender, EventArgs e)
        {
            if (actualizarcombo == "Si")
            {
                LlenarComboGastos();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiarcabezado();
                LimpiarDetalle();
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                r.ConsultaGastos(Folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo, txtReferencia, txtSaldo, txtCondominio, txtDiasVence, txtFechaVence, txtArchivo, cmbCentroCostos, cmbSemana, dtpAnio, cmbProoveedorAlternoSiNo, cmbproyecto,txttotalretenciones);
                TxtFolio1.Text = Folio;
                proyecto = cmbproyecto.Text;

                rutaCompletaArchivo = txtArchivo.Text;
                cmbOrdenCompra.Enabled = false;
                cmbCondominio.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
                r.ConsultaAbonoGasto(txtFolio.Text, txtAbono);


                txtMatricular.Text = DBRegistroReembolso.MatriculaC;

                string[] valores = r.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

                if (txtReciboCol.Text != "0")
                {
                    r.SeleccionarOrdenEntrega2(cmbOrdenCompra, txtReciboCol.Text);
                    cmbOrdenCompra.SelectedIndex = 0;

                    r.SeleccionarProvedor2(cmbProveedor, txtReciboCol.Text);
                    cmbProveedor.SelectedIndex = 0;

                    string[] valores2 = r.InformacionDocumento3(cmbOrdenCompra.Text);
                    txtFiltroOrdenC.Text = valores2[0];
                    txtDocumentoCol.Text = valores2[1];

                    cmbFiltroDocumentoC.Text = txtFiltroOrdenC.Text + " - " + txtDocumentoCol.Text;
                }
                txtMatricular.Text = DBRegistroReembolso.MatriculaC;
                if (txtCondominio.Text == "GLOBAL")
                {
                    cmbCondominio.Text = "GLOBAL";
                }
                else if (txtCondominio.Text != "GLOBAL" && txtCondominio.Text != string.Empty)
                {
                    string[] valores3 = r.InformacionCondominio2(txtCondominio.Text);
                    cmbCondominio.Text = valores3[0];
                }
                txtMatricular.Text = DBRegistroReembolso.MatriculaC;
                guna2GradientPanel2.Visible = false;
                guna2GradientPanel2.SendToBack();
                r.CargarRecibosPartidasGasto(guna2DataGridView1, txtFolio.Text);
                SumarColumnasPartida();
                CargarConceptosGlobales(txtFolio.Text);
            }
            else
            {
                return;
            }
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


        private void cmbProveedroAlterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (consultaRegistros != "NO" && PartidaNuevaConsulta != "SI")
            {
                //    MessageBox.Show("valor:" + consultaRegistros);
                r.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
                //    MessageBox.Show("vuelve a consultar aqui 2");
            }
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1)
                return; // Salida temprana si no es una fila válida
            PartidaNuevaConsulta = "SI";

            // Llenar el combo de conceptos
            LlenarComboGastos();

            // Obtener la partida seleccionada
            string partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value?.ToString();
            txtPartida.Text = partida;

            // Desconectar el evento temporalmente
            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
            LlenarComboFormasPago(cmbCentroCostos.Text, cmbproyecto.Text);
            // Consultar y llenar los campos
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
                    //txtDescuento1,
                    txtTotal1,
                    //txtImpuesto1,
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
            
            // Reconectar el evento
            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;

            // Calcular el precio unitario de forma segura
            decimal subtotal = 0, cantidad = 0, descuentoIm = 0;

            decimal.TryParse(txtSubtotal1.Text.Replace(",", ""), out subtotal);
            decimal.TryParse(txtCantidad.Text.Replace(",", ""), out cantidad);
            decimal.TryParse(txtDescuentoIm.Text.Replace(",", ""), out descuentoIm);

            PanelPartidasRequisicion.Visible = true;
            guna2Button11.Visible = true;
            //pnPartidas.Visible = false; ;
            r.mostrarArchivos(dataGridView2, txtFolio.Text, txtPartida.Text);
            RecargarDescuentos();
            RecargarImpuestos();
            r.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
            //  Calcular();

            //            txtImpuesto1.SelectedIndex = 0;
            //            cmdproyectoalterno.SelectedIndex = 0;
            //            cmbformapago.SelectedIndex = 0;
            //            cmbreferencia.SelectedIndex = 0;    
        }


        private void button11_Click_1(object sender, EventArgs e)
        {
            // Primero, obtén la ruta de la base de datos.

            if ((string.IsNullOrWhiteSpace(DBRegistroReembolso.Ruta))) // Ahora usa la propiedad
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

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Todos los archivos|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = open.FileName;
                string extensionArchivo = Path.GetExtension(archivoSeleccionado);
                // string nombreArchivo = descripcion + extensionArchivo;
                string nombreArchivo = Path.GetFileNameWithoutExtension(archivoSeleccionado);
                string rutaCompletaArchivo = Path.Combine(carpetaDestino, nombreArchivo);
                string contenidoArchivo = string.Empty;



                try
                {
                    // Copia el archivo al sistema de archivos.
                    File.Copy(archivoSeleccionado, rutaCompletaArchivo, overwrite: true);
                    txtArchivo1.Text = nombreArchivo; // Actualiza el TextBox

                    //  contenidoArchivo = File.ReadAllText(archivoSeleccionado);

                    byte[] archivoBytes = File.ReadAllBytes(archivoSeleccionado);
                    contenidoArchivo = Convert.ToBase64String(archivoBytes);


                    MessageBox.Show(r.insertaArchivos(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, nombreArchivo, extensionArchivo, contenidoArchivo));

                    // Actualiza otros datos en la tabla PartidaRegistroGastos (si es necesario).
                    // Esto asume que tienes una instancia de la clase 'c' disponible.
                    // Si no, necesitarás instanciarla o usar un enfoque diferente.
                    //c.ModificarExtension4gasto(noOrden, descripcion, extensionArchivo);
                    //c.ActualizarRecepcion2gasto(noOrden, descripcion, nombreArchivo);
                    r.mostrarArchivos(dataGridView2, TxtFolio1.Text, txtPartida.Text);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Ya hay un archivo guardado o en uso: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar archivo: " + ex.Message);
                    return; // Importante: Salir en caso de error.
                }
            }
        }

        private void cmbConcepto_Click_1(object sender, EventArgs e)
        {
            if (actualizarcombo == "Si")
            {
                LlenarComboGastos();
                actualizarcombo = string.Empty;
            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (txtOrden.Text != string.Empty)
                {
                    string[] valores = r.InformacionGastoo(cmbConcepto.Text, txtOrden.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    //txtDescuento1.Text = valores[3];
                    txtTotal1.Text = valores[4];
                    txtConcepto2.Text = valores[5];
                    txtPartidaOrden.Text = valores[7];
                    txtCantidad2.Text = valores[8];
                    txtCantidad.Text = valores[8];
                    txtImpuesto12.Text = valores[9];
                    txtUnidad.Text = valores[10];
                }
                else
                {
                    string[] valores = r.InformacionGasto(cmbConcepto.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtUnidad.Text = valores[3];
                    txtImpuesto12.Text = valores[4];
                    cobraIEPS = valores[7] == "Si" ? true : false;

                }
                txtIEPS.Enabled = cobraIEPS;
                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();
            }
        }

        private void cmbProveedroAlterno_Click_1(object sender, EventArgs e)
        {
            if (actualizarproveedor == "Si")
            {
                LlenarComboProveedores();
                actualizarproveedor = string.Empty;
            }
        }

        private void cmbProveedroAlterno_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (consultaRegistros != "SI")
            {
                r.obtenerRFC(cmbProveedroAlterno.Text, txtRFC);
            }
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPartida.Text))
            {
                MessageBox.Show("Selecciona una partida");
                return;
            }
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible eliminar la partida");
                return;
            }
            MessageBox.Show(r.EliminarPartidaRegistroGasto(txtFolio.Text, txtPartida.Text));
            string maximo = r.ObtenerTotalPartidaRegistroGasto(txtFolio.Text);
            r.ActualizarGasto(txtFolio.Text, maximo);
            r.ReciboSaldosGastos(txtFolio.Text, txtSubtotalR, txtDescuentoR, txtImpuestoR, txtTotalR, txtPartidas, txtSaldo);
            r.CargarRecibosPartidasGasto(guna2DataGridView1, txtFolio.Text);
            SumarColumnasPartida();

            LimpiarDetalle();
        }

        private void guna2Button10_Click_1(object sender, EventArgs e)
        {
            LimpiarDetalle();
            BloquearDetalle();
            guna2TabControl1.SelectedIndex = 0;
            PanelPartidasRequisicion.Visible = false;


        }

        private void guna2Button12_Click_1(object sender, EventArgs e)
        {
            if (txtTotal1.Text == "0.00" || txtTotal1.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        r.ActualizarGasto(TxtFolio1.Text, Partida.ToString());
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
                if (cmbProoveedorAlternoSiNo.Text == "Si" && string.IsNullOrEmpty(cmbProveedroAlterno.Text))
                {
                    MessageBox.Show("Es necesario seleccionar un proveedor alterno");
                    return;
                }


                r.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal("0.00"), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto12.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), "0", txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""), cmdproyectoalterno.Text, dtpFecha.Text, cmbformapago?.SelectedValue?.ToString(), cmbreferencia.Text, txtIEPS.Text.Replace(",", ""), txtRetencion.Text.Replace(",", ""), txtPrecio.Text.Replace(",", ""));

                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);

               // c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text);
                //this.Close();

               // MessageBox.Show("1");
                //ConceptosGlobalesPartidaGastos documentoConceptoGlobal = new ConceptosGlobalesPartidaGastos(TxtFolio1.Text, recibo, reciboCol, ConceptosGlobales);
                //documentoConceptoGlobal.ShowDialog();
               
                LimpiarDetalle();
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                CargarConceptosGlobales(TxtFolio1.Text);

            }
            PanelPartidasRequisicion.Visible = false;

            guna2Button9.Visible = true;
            r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR, new Guna2TextBox(), new Guna2TextBox(), new Guna2TextBox());

            r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotal, txtDescuento, txtTotal, txtImpuestos, txtIEPSGlobal, txttotalretenciones, txtPartidas);

            r.CargarRecibosPartidasGasto(guna2DataGridView1, TxtFolio1.Text);
            SumarColumnasPartida();

            dataGridView2.Rows.Clear();
        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
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


                




                r.InsertarPartidaGasto(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text.Replace(",", ""), txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text.Replace(",", ""), Convert.ToDecimal(txtSubtotal1.Text.Replace(",", "")), Convert.ToDecimal("0.00"), Convert.ToDecimal(txtTotal1.Text.Replace(",", "")), Convert.ToDecimal(txtImpuesto12.Text.Replace(",", "")), rutaCompletaArchivo, cmbProveedroAlterno?.SelectedValue?.ToString(), "0", txtDescuentoIm.Text.Replace(",", ""), txtImpuestoIm.Text.Replace(",", ""), cmdproyectoalterno.Text, dtpFecha.Text, cmbformapago?.SelectedValue?.ToString(), cmbreferencia.Text, txtIEPS.Text.Replace(",", ""), txtRetencion.Text.Replace(",", ""), txtPrecio.Text.Replace(",", ""));
               
                
                r.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text.Replace(",", ""));
                //c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text);
                
                r.ActualizarGasto(TxtFolio1.Text, txtPartida.Text);
                
                r.Consulta5RegistroGasto(TxtFolio1.Text, txtPartida);
                
                r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotalR, txtDescuentoR, txtTotalR, txtImpuestoR, new Guna2TextBox(), new Guna2TextBox(), new Guna2TextBox());
                
                r.ReciboSaldosPartidasGasto(TxtFolio1.Text, txtSubtotal, txtDescuento, txtTotal, txtImpuestos, txtIEPSGlobal, txttotalretenciones, txtPartidas);
                LimpiarDetalle();
                
                r.ConsultaGasto(TxtFolio1.Text, txtPartida);
                dataGridView2.Rows.Clear();
                cmdproyectoalterno.Text = cmbproyecto.Text;

                LlenarComboGastos();
                CargarConceptosGlobales(txtFolio.Text);

            }
        }

        private void txtPrecio_TextChanged_1(object sender, EventArgs e)
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtSubtotal1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtDescuentoR_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoR);
        }

        private void txtSubtotalR_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotalR);
        }

        private void txtTotalR_TextChanged_1(object sender, EventArgs e)
        {
            Moneda(ref txtTotalR);
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView2.Rows[e.RowIndex];

                if (dataGridView2.Columns[e.ColumnIndex].Name == "Eliminar")
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
                            dataGridView2.Rows.Remove(filaSeleccionada);
                            MessageBox.Show($"Registro con ID {archivo} eliminado correctamente.");
                            r.mostrarArchivos(dataGridView2, TxtFolio1.Text, txtPartida.Text);
                        }
                    }
                }

                if (dataGridView2.Columns[e.ColumnIndex].Name == "Ver")
                {
                    try
                    {
                        if (!dataGridView2.Columns.Contains("ContenidoArchivo"))
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
                        string tipo = DetectarTipo(base64Archivo);

                        string extension;
                        switch (tipo)
                        {
                            case "pdf":
                                extension = ".pdf";
                                break;
                            case "xml":
                                extension = ".xml";
                                break;
                            case "png":
                                extension = ".png";
                                break;
                            default:
                                extension = ".bin";
                                break;
                        }

                        string rutaTemporal = Path.Combine(Path.GetTempPath(), nombreArchivo + extension);
                        byte[] bytes = Convert.FromBase64String(base64Archivo);
                        File.WriteAllBytes(rutaTemporal, bytes);
                        Process.Start(new ProcessStartInfo(rutaTemporal) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al mostrar el archivo: " + ex.Message);
                    }
                }
            }
        }
        private string DetectarTipo(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            // PDF: inicia con %PDF
            if (bytes.Length > 4 &&
                bytes[0] == 0x25 &&
                bytes[1] == 0x50 &&
                bytes[2] == 0x44 &&
                bytes[3] == 0x46)
            {
                return "pdf";
            }

            // XML: inicia con <?xml
            string texto = Encoding.UTF8.GetString(bytes);
            if (texto.TrimStart().StartsWith("<?xml"))
            {
                return "xml";
            }

            // PNG: firma 89 50 4E 47 0D 0A 1A 0A
            if (bytes.Length > 8 &&
                bytes[0] == 0x89 &&
                bytes[1] == 0x50 &&
                bytes[2] == 0x4E &&
                bytes[3] == 0x47 &&
                bytes[4] == 0x0D &&
                bytes[5] == 0x0A &&
                bytes[6] == 0x1A &&
                bytes[7] == 0x0A)
            {
                return "png";
            }

            return "desconocido";
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

        private void cmbDocumento_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //   MessageBox.Show(txtFolio.Text);
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty)
                {
                    string[] valores = r.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];

                    //MessageBox.Show(txtFolio.Text);

                    if (txtFolio.Text == string.Empty)
                    {
                        r.ConsecutivoGasto(txtConsecutivo, txtClave.Text);
                    }
                    //   groupBox2.Enabled = true;
                }

            }
        }

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbProveedroAlterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtImpuesto1_TextChanged_1(object sender, EventArgs e)
        {
            // Moneda(ref txtImpuesto12);

            try
            {
                Calcular();
            }
            catch (Exception)
            {
                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }

        private void cmbCentroCostos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbproyecto.Items.Clear();
            if (consultaRegistros != "SI")
            {
                r.SeleccionarCatConceptosGlobales(cmbproyecto, cmbCentroCostos.Text);

                if (cmbproyecto.Items.Count != 0)
                {
                    cmbproyecto.SelectedIndex = 0;
                }
            }
        }

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbformapago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbformapago.Text != string.Empty && consultaRegistros != "SI")
            {
                // r.SeleccionarReferencia(cmbreferencia,CentroCosto,cmdproyectoalterno.Text,cmbformapago.Text);
            }
        }

        private void txtImpuesto1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    Moneda(ref txtImpuesto1);

            try
            {
                Calcular();
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }
        private void SumarColumnasPartida()
        {
            decimal totalSubtotal = 0;
            decimal totalDescuento = 0;
            decimal totalImpuesto = 0;
            decimal totalIeps = 0;
            decimal totalretenciones = 0;

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

                    if (fila.Cells["Ieps"].Value != null && decimal.TryParse(fila.Cells["Ieps"].Value.ToString(), out decimal ieps))
                        totalIeps += ieps;

                    if (fila.Cells["retenciones"].Value != null && decimal.TryParse(fila.Cells["retenciones"].Value.ToString(), out decimal retenciones))
                        totalretenciones += retenciones;
                   
                }

            }

            lblSubtotalPartidas.Text = "Subtotal: $" + totalSubtotal.ToString("N2");
            lblDescuentosPartidas.Text = "Descuento: $" + totalDescuento.ToString("N2");
            lblImpuestosPartidas.Text = "Impuesto: $" + totalImpuesto.ToString("N2");
            lblIEPSPartidas.Text = "IEPS: $" + totalIeps.ToString("N2");
            lblRetenciones.Text = "Retenciones: $" + totalretenciones.ToString("N2");
           // txttotalretenciones.Text = totalretenciones.ToString("N2");
            decimal total = totalSubtotal - totalDescuento + totalImpuesto + totalIeps - totalretenciones;
            lblTotal.Text ="Total: $"+ total.ToString();


        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIEPS_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtIEPS);

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

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdproyectoalterno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCentroCostosAlterno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtImpuestoR_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoR);
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtImpuestos_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestos);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }

        private void btnDescuentos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFolio1.Text))
            {
                MessageBox.Show("Seleccione una Compra Reembolso");
                return;
            }
            //if (cmbEstatus.Text != "Abierto")
            //{
            //    MessageBox.Show("No es posible agregar descuentos a la compra reembolsos");
            //    return;
            //}
            //ConceptosGlobalesPartida cgp = new ConceptosGlobalesPartida(TxtFolio1.Text, txtPartida.Text.ToString(), "Descuento", cmbEstatus.Text);
            ConceptosGlobalesPartida cgp = new ConceptosGlobalesPartida(TxtFolio1.Text, txtPartida.Text.ToString(), "Descuento", cmbEstatus.Text);

            if (cgp.ShowDialog() == DialogResult.OK)
            {

                RecargarDescuentos();
                Calcular();
            }
        }

        private void btnImpuestos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFolio1.Text))
            {
                MessageBox.Show("Seleccione una Compra Reembolso");
                return;
            }
            // if ( cmbEstatus.Text != "Abierto")
            //{
            //    MessageBox.Show("No es posible agregar impuestos a la compra reembolsos");
            //    return;
            //}
            ConceptosGlobalesPartida cgp = new ConceptosGlobalesPartida(TxtFolio1.Text, txtPartida.Text.ToString(), "Impuesto", cmbEstatus.Text);

            if (cgp.ShowDialog() == DialogResult.OK)
            {
                RecargarImpuestos();
                Calcular();
            }
        }

        private void txtDescuentoIm_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoIm);
        }

        private void txtDescuentoIm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtDescuentoIm_Leave(object sender, EventArgs e)
        {
           decimal importe =Convert.ToDecimal(txtCantidad.Text) *Convert.ToDecimal( txtPrecio.Text);
           decimal descuento = Convert.ToDecimal(txtDescuentoIm.Text);
            decimal subtotal = importe - descuento;
            txtSubtotal1.Text = subtotal.ToString("N2");
        }

        private void cmbConcepto_TextUpdate(object sender, EventArgs e)
        {
         /*   string texto = cmbConcepto.Text;

            var coincidencias = DBRegistroReembolso.datosCombo
                .Where(item => item.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            cmbConcepto.Items.Clear();
            cmbConcepto.Items.AddRange(coincidencias.ToArray());

            cmbConcepto.DroppedDown = true;
            cmbConcepto.SelectionStart = texto.Length;
            cmbConcepto.SelectionLength = 0;
         */
        }

        private void txtIEPSGlobal_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void txtIEPSGlobal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtIEPSGlobal);

        }

        private void txttotalretenciones_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txttotalretenciones);
        }
    }
}
    