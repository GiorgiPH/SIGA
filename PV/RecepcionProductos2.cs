using Condominios.Clases.CentroCostos;
using PuntoVentas.Clases.Login;
using PV.Clases;
using PV.Clases.Almacenes;
using PV.Clases.Inventario;
using PV.Clases.OrdenCompra;
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

namespace PV
{
    public partial class RecepcionProductos2 : Form
    {
        public static string Matricula = string.Empty;
        DBOrdenCompra c = new DBOrdenCompra();
        DBAlmacenes a = new DBAlmacenes();
        DBRegistrarEntradas r = new DBRegistrarEntradas();
        DBPartidas p = new DBPartidas();
        DBCentroCostos cc = new DBCentroCostos();
        public static string Carpeta = string.Empty;


        string recibo = string.Empty;
        string reciboCol = string.Empty;
        public static string Carpeta1 = string.Empty;
        int opcion = 0;

        int Partidas = 0;
        private bool mostrrcentorcosto = false;

        public RecepcionProductos2()
        {
            InitializeComponent();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {

            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
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
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistroGastos2_Load(object sender, EventArgs e)
        {
            c.SeleccionarRecepcionProductos(cmbDocumento);
            c.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
            a.SeleccionarAlmacen(cmbAlmacen);
            c.SeleccionarProducto(cmbConcepto);
            LlenarComboCentro();
            c.ruta();
            //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
            txtDiasVence.Text = "0";
            CalcularFechaVencimiento();



        }

        private void btnCrearEncabezado_Click(object sender, EventArgs e)
        {
            if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al proveedor antes de continuar");
                return;
            }
            else if (string.IsNullOrEmpty(cmbAlmacen.Text))
            {
                MessageBox.Show("Selecciona un almacén");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una recepción de productos Bloqueada o Cancelada");
                return;
            }
            else
            {
                string centroCosto = null;

                if (mostrrcentorcosto)
                {
                    if (cmbCentroCostos.SelectedValue == null ||
                        string.IsNullOrWhiteSpace(cmbCentroCostos.SelectedValue.ToString()))
                    {
                        MessageBox.Show(
                            "Seleccione un centro de costos antes de continuar.",
                            "Validación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    centroCosto = cmbCentroCostos.SelectedValue.ToString();
                }
                string FolioOrden = txtOrdenCompra.Text;
                string almacen = cmbAlmacen.Text.Split('-')[0];
                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarRecepcionProducto(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtNotas.Text, txtElaborado.Text, FolioOrden, txtConsecutivo.Text, almacen, txtReferencia.Text, txtDiasVence.Text, txtFechaVence.Text, centroCosto);
                }
                int opcion = 0;
                if (txtArchivo.Text != string.Empty)
                {
                    opcion = 1;
                }
                

                guna2TabControl1.SelectedIndex = 1;

                TxtFolio1.Text =  txtFolio.Text;
                txtOrden.Text = FolioOrden;
                opcion = opcion;

                
                if (txtOrden.Text != string.Empty)
                {
                    c.SeleccionarProductoRecepcion(cmbConcepto, txtOrden.Text);
                    c.ConsultaRecepcion(txtFolio.Text, txtPartida);
                    txtPrecio.Enabled = false;
                    txtCantidad.Text = "1";
                    txtUnidad.Text = "Servicio";
                    txtDivisa1.Text = "MXN";
                    txtTipoCambio1.Text = "1.00";
                }
                else
                {
                    c.SeleccionarProducto(cmbConcepto);
                    c.ConsultaRecepcion(txtFolio.Text, txtPartida);
                    txtPrecio.Enabled = true;
                    txtCantidad.Text = "1";
                    txtUnidad.Text = "Servicio";
                    txtDivisa1.Text = "MXN";
                    txtTipoCambio1.Text = "1.00";
                }







                if (opcion != 0)
                {
                    button11.Enabled = false;
                    button12.Enabled = false;
                    button13.Enabled = false;
                }
            }

            cmbFiltroDocumentoC.DroppedDown = false;
            cmbProveedor.DroppedDown = false;
            cmbOrdenCompra.DroppedDown = false;
          //  btLimpiarOrden.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            txtReferencia.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            cmbDocumento.DroppedDown = false;
         //   button6.BackColor = Color.Gainsboro;
            button2.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            button7.BackColor = Color.Gainsboro;

        }

        private void btnConfirmarPartida_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.SelectedIndex==-1)
            {
                if (MessageBox.Show("Si termina la partida sin registrar un importe no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRecepcion(TxtFolio1.Text, Partida.ToString());
                 
                    }

                }
            }
          
            else
            {
                c.InsertarPartidaRecepcion(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtImpuesto1.Text), txtArchivo1.Text);
                c.ActualizarRecepcion(TxtFolio1.Text, txtPartida.Text);
                c.ReciboSaldosRecepcion(TxtFolio1.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);
                PanelPartidasRecepcion.Visible = false;
                btnTerminarRecepcion.Visible = true;
                c.CargarRecibosPartidasRecepcion(dgvPartidas, TxtFolio1.Text);

            }
       
        }

        private void btnSiguientePartida_Click(object sender, EventArgs e)
        {

            if (cmbConcepto.SelectedIndex == -1)
            {
                MessageBox.Show("Registre el Producto para continuar");
                return;
            }
            else
            {
                c.InsertarPartidaRecepcion(TxtFolio1.Text, txtPartida.Text, txtClave1.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtImpuesto1.Text), txtArchivo1.Text);
                c.ActualizarPartidaOrden(txtOrden.Text, txtPartidaOrden.Text, txtCantidad.Text);
                //c.RegistroProducto(txtClave.Text, txtCantidad.Text, txtAlmacen.Text);
                Limpiar();
                c.ConsultaRecepcion(TxtFolio1.Text, txtPartida);
                c.ActualizarRecepcion(TxtFolio1.Text, txtPartida.Text);
                c.ReciboSaldosRecepcion(TxtFolio1.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);

                if (txtOrden.Text != string.Empty)
                {
                    c.SeleccionarProductoRecepcion(cmbConcepto, txtOrden.Text);
                }
                else
                {

                    c.SeleccionarProducto(cmbConcepto);
                }
            }
        }

        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtSubtotal1.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtTotal1.Text = "0.00";
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtImpuesto1.Text = "0.00";
            txtUnidad.Clear();
            //cmbConcepto.Text = null;
            txtArchivo1.Clear();
            
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolio.Text) && cmbEstatus.Text == "Abierto")
            {
                if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nueva Recepción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }
            Limpiar();
            LimpiarDetalle();
        }

        private void dgvPartidas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Partida = dgvPartidas.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
                cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
                c.ConsultaPartidaRecepcion(TxtFolio1.Text, Partida, txtClave1, txtConcepto, txtConcepto2, txtCantidad, txtUnidad, txtDivisa1, txtTipoCambio1, txtSubtotal1, txtDescuento1, txtTotal1, txtImpuesto1, txtArchivo1, cmbConcepto);
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
                txtPartida.Text = Partida;
                txtPrecio.Text = ((Convert.ToDecimal(txtSubtotal1.Text)+ Convert.ToDecimal(txtDescuento1.Text)) / Convert.ToDecimal(txtCantidad.Text)).ToString("N2");
                PanelPartidasRecepcion.Visible = true;
                guna2Button11.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void toolStrip2_Click(object sender, EventArgs e)
        {
        
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            /*if (e.ClickedItem.Text == "NUEVO")
            {
                //   Limpiar();


                guna2TabControl1.Enabled = true;
              //  guna2GradientPanel2.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;

                Limpiarcabezado();
                LimpiarDetalle();
                DesbloquearEncabezado();
                DesbloquearDetalle  ();


                c.SeleccionarRecepcionProducto(cmbDocumento);
                c.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
                c.SeleccionarCondomini2(cmbCondominio);
                c.ruta();
                //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
                c.CargarRrecepcion(dataGridView1, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
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
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            }
            */
        }

        private void cmbOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbOrdenCompra.Text != string.Empty && cmbFiltroDocumentoC.Text != string.Empty && cmbProveedor.Text != string.Empty)
                {
                    string[] valores = c.InformacionOrdenCompra(cmbOrdenCompra.Text, cmbFiltroDocumentoC.Text);
                    txtOrdenCompra.Text = valores[0];
                    //cmbEstatus.Text = valores[1];
                    //txtFecha.Text = Convert.ToDateTime(valores[2]).ToString("yyyy/MM/dd");
                    txtMatricular.Text = valores[3];
                    Matricula = txtMatricular.Text;
                    txtDivisa.Text = valores[4];
                    txtTipoCambio.Text = valores[5];
                    //txtSubtotal.Text = valores[6];
                    //txtDescuento.Text = valores[7];
                    //txtRecargo.Text = valores[8];
                    //txtTotal.Text = valores[9];
                    //txtPartidas.Text = valores[10];
                    //txtNotas.Text = valores[11];
                    //txtElaborado.Text = valores[12];

                    string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                    txtNombreAlumnno.Text = valores2[0];

                    txtElaborado.Text = DBLogin.usuario;
                }

            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

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

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            if (txtOrdenCompra.Text != string.Empty)
            {
                MessageBox.Show("No es posible cambiar proveedor de la orden de compra");
            }
            else
            {
                using (var buscador = new BuscarListaProveedores())
                {
                    if (buscador.ShowDialog() == DialogResult.OK)
                    {
                        txtMatricular.Text = buscador.Matricula;
                        txtNombreAlumnno.Text = buscador.Nombre;
                        Matricula = buscador.Matricula; // sigues alimentando tu campo static si otro código ya depende de él
                    }
                }
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
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

            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (txtFolio.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtFolio.Text;
                    string Descripcion = txtClave.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "EG" + NoOrdenResl;

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

        private void button1_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
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
                        c.ModificarExtension4Recepci(txtFolio.Text, txtClave.Text);
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

        private void btnEliminarPartida_Click(object sender, EventArgs e)
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
                DocumentoGlobalesRecepcionVer documentoConceptoGlobalVer = new DocumentoGlobalesRecepcionVer(txtFolio.Text);
                documentoConceptoGlobalVer.ShowDialog();
            }

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
        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];
                    mostrrcentorcosto = ParsearBooleano(valores[2]);


                    if (txtFolio.Text == string.Empty)
                    {
                        c.ConsecutivoRecepcion(txtConsecutivo, txtClave.Text);
                    }
                 //   groupBox2.Enabled = true;
                }

            }
        }

    

        private void cmbFiltroDocumentoC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbFiltroDocumentoC.Text);
                    txtFiltroOrdenC.Text = valores[1];

                    c.SeleccionarProvedor(cmbProveedor, txtFiltroOrdenC.Text);
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }

            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbFiltroDocumentoC.Text != string.Empty && txtFolio.Text == string.Empty)
                {
                    c.SeleccionarOrdenEntrega(cmbOrdenCompra, txtFiltroOrdenC.Text, cmbProveedor.Text);
                }

            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistroGastos2_Activated(object sender, EventArgs e)
        {
           
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty && txtOrdenCompra.Text == string.Empty)
            {
                string[] valores2 = c.InformacionProveedor(txtMatricular.Text);
                txtNombreAlumnno.Text = valores2[0];
            }
        }

        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una recepción de productos Bloqueada o Cancelada");
                return;
            }
            DesbloquearDetalle();
            LimpiarDetalle();
            PanelPartidasRecepcion.Visible = true;
            c.ConsultaRecepcion(TxtFolio1.Text, txtPartida);

        }

        private void PanelPartidasRequisicion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                if (txtOrden.Text != string.Empty)
                {
                    string[] valores = c.InformacionRecepcion(cmbConcepto.Text, txtOrden.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtDescuento1.Text = valores[3];
                    txtTotal1.Text = valores[4];
                    txtConcepto2.Text = valores[5];
                    txtPartidaOrden.Text = valores[7];
                    txtCantidad2.Text = valores[8];
                    txtCantidad.Text = valores[8];
                    txtImpuesto1.Text = valores[9];
                    txtUnidad.Text = valores[10];
                }
                else
                {
                    string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                    txtClave1.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtPrecio.Text = valores[2];
                    txtUnidad.Text = valores[3];
                    txtImpuesto1.Text = valores[4];
                }

                decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text);
                txtSubtotal.Text = sub.ToString();

            }
        }

        private void btnTerminarRecepcion_Click(object sender, EventArgs e)
        {
            if (dgvPartidas.Rows.Count == 0)
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
                c.ActualizarRecepcion(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                c.ActualizarSaldoProveedor2(txtMatricular.Text, Convert.ToDecimal(txtTotal.Text));
                List<List<string>> lista = c.ObtenerPartidas(txtFolio.Text);
                IngresarAlmacen(lista);
                if (MessageBox.Show("¿Imprimir Documento?", "Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   
                        ReciboRecepcionProductos r = new ReciboRecepcionProductos(txtFolio.Text);
                        r.ShowDialog();
                    

                }


                Limpiar();
                c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                Limpiarcabezado();
                LimpiarDetalle();
                guna2TabControl1.SelectedIndex = 0;
                btnTerminarRecepcion.Visible = true;
                guna2TabControl1.Enabled = false;
            }
            

            


        }
        private void IngresarAlmacen(List<List<string>> lista)
        {
            TextBox t = new TextBox();
            r.ValidarDocumentoEPR();

            r.RegistroMovimientoInventario("", "E", "EPR", txtFecha.Text, cmbEstatus.Text, "", txtAlmacen.Text, lista.Count.ToString(), txtDivisa.Text, txtTipoCambio.Text.Replace(",", ""), txtTotal.Text.Replace(",", ""), txtNotas.Text, txtElaborado.Text, t, "");

            for (int i = 0; i < lista.Count; i++)
            {
                string clave = lista[i][0];
                string cantidad = lista[i][1];
                string costeo = lista[i][2];
                string precio = lista[i][3];
                string partidaOrden = lista[i][4];
                string unidad = lista[i][5];
                string total = lista[i][6];
                c.RegistroProducto(clave, cantidad.Replace(",", ""), txtAlmacen.Text, costeo.Replace(",", ""), Convert.ToDecimal(precio.Replace(",", "")));
                //if (!string.IsNullOrEmpty(txtOrdenCompra.Text))
                //{
                //    c.ActualizarPartidaOrden(txtOrdenCompra.Text, partidaOrden, cantidad.Replace(",", ""), clave);
                //}


                //Realiza el movimiento inventario
                p.RegistroPartida(t.Text, "E", "EPR", (i + 1).ToString(), clave, cantidad.Replace(",", ""), unidad, Convert.ToDecimal(precio.Replace(",", "")), txtDivisa.Text, txtTipoCambio.Text, Convert.ToDecimal(total.Replace(",", "")), "");
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

        private void Calcular()
        {
            try
            {
                if (txtCantidad.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    decimal sub = Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtCantidad.Text);
                    sub = sub - Convert.ToDecimal(txtDescuento1.Text);
                    decimal Impuesto = (Convert.ToDecimal(txtImpuesto1.Text) / 100) * Convert.ToDecimal(sub);
                    txtImpuestoIm.Text = Impuesto.ToString("N2");
                    txtTotal1.Text = (Convert.ToDecimal(sub) + Impuesto).ToString("N2");
                    txtSubtotal1.Text = Convert.ToString(sub);
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Formato incorrecto");
            }

        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtPrecio);

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
            Utilerias.Moneda2(ref txtImpuesto1);
            try
            {
                Calcular();
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de impuesto incorrecto");
            }
        }

        private void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuento1);

            try
            {
                Calcular();
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de descuento incorrecto");
            }

        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
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

        private void button11_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio1.Text != string.Empty)
                {
                    c.InsertarPartidaRecepcion(TxtFolio1.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text, txtDivisa1.Text, txtTipoCambio1.Text, Convert.ToDecimal(txtSubtotal1.Text), Convert.ToDecimal(txtDescuento1.Text), Convert.ToDecimal(txtTotal1.Text), Convert.ToDecimal(txtImpuesto1.Text), txtArchivo1.Text);
                    string NoOrdenResl = TxtFolio1.Text;
                    string Descripcion = txtPartida.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "G" + NoOrdenResl;

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

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "G" + NoOrdenResl;

                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "All Files|*.*";

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string archivo = open.FileName;
                        string ext = Path.GetExtension(archivo);
                        try
                        {
                            File.Copy(archivo, Carpeta + @"\" + Descripcion + ext);
                            txtArchivo1.Text = Descripcion + ext;
                            //c.ModificarExtension4Recepci(TxtFolio1.Text, txtPartida.Text, ext);
                            c.ActualizarRecepcion2gasto(TxtFolio1.Text, txtPartida.Text, txtArchivo1.Text);
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

        private void button12_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio1.Text != string.Empty && txtArchivo1.Text != string.Empty)
                {
                    string NoOrdenResl = TxtFolio1.Text;
                    string Descripcion = txtPartida.Text;

                    Carpeta = DBOrdenCompra.Ruta + @"\" + "G" + NoOrdenResl;

                    Process.Start(Carpeta + @"\" + txtArchivo1.Text);
                }
                else if (TxtFolio1.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo1.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }

            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (DBOrdenCompra.Ruta != string.Empty)
            {
                if (TxtFolio1.Text != string.Empty && txtArchivo1.Text != string.Empty)
                {
                    string NoOrdenResl = TxtFolio1.Text;
                    string Descripcion = txtPartida.Text;
                    Carpeta = DBOrdenCompra.Ruta + @"\" + "G" + NoOrdenResl;
                    if (Directory.Exists(Carpeta))
                    {
                        File.Delete(Carpeta + @"\" + txtArchivo1.Text);
                        txtArchivo1.Clear();
                        c.ModificarExtension3gasto(TxtFolio1.Text, txtPartida.Text);
                    }
                }
                else if (TxtFolio1.Text != string.Empty)
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

        private void dgvRecepciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                Limpiarcabezado();
                string Folio = dgvRecepciones.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaRecepcion(Folio, txtClave, cmbEstatus, txtFecha, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtNotas, txtElaborado, txtFolio, txtReciboCol, txtConsecutivo, txtAlmacen, txtReferencia, txtSaldo, txtDiasVence, txtFechaVence, txtArchivo, cmbCentroCostos);
                cmbOrdenCompra.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
                c.ConsultaAbonoGasto(txtFolio.Text, txtAbono);
                txtMatricular.Text = DBOrdenCompra.MatriculaC;
                //    panel2.Visible = false;
                TxtFolio1.Text = Folio;
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

                if (txtReciboCol.Text != "0")
                {
                    c.SeleccionarOrdenEntrega2(cmbOrdenCompra, txtReciboCol.Text);
                    cmbOrdenCompra.SelectedIndex = 0;

                    c.SeleccionarProvedor2(cmbProveedor, txtReciboCol.Text);
                    cmbProveedor.SelectedIndex = 0;

                    string[] valores2 = c.InformacionDocumento3(cmbOrdenCompra.Text);
                    txtFiltroOrdenC.Text = valores2[0];
                    txtDocumentoCol.Text = valores2[1];

                    cmbFiltroDocumentoC.Text = txtFiltroOrdenC.Text + " - " + txtDocumentoCol.Text;
                }

                guna2GradientPanel2.Visible = false;
                guna2GradientPanel2.SendToBack();
                c.CargarRecibosPartidasRecepcion(dgvPartidas, txtFolio.Text);
            }
            else
            {
                return;
            }
            
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRecepcion.Visible = false;
        }

        void Limpiarcabezado()
        {
            cmbDocumento.SelectedIndex=-1;
            txtConsecutivo.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtDiasVence.Text ="0";
            txtFechaVence.Text = string.Empty;
            cmbEstatus.Text="Abierto";

            txtMatricular.Text = string.Empty;
            txtNombreAlumnno.Text = string.Empty;
            cmbFiltroDocumentoC.SelectedIndex=-1;
            cmbProveedor.SelectedIndex=-1;
            cmbOrdenCompra.SelectedIndex=-1;
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
            txtAlmacen.Text = string.Empty;
            cmbDocumento.SelectedIndex = -1;
            cmbFiltroDocumentoC.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbOrdenCompra.SelectedIndex = -1;
            cmbAlmacen.SelectedIndex = -1;
            dgvPartidas.Rows.Clear();
            txtFolio.Text = string.Empty;
            TxtFolio1.Text = string.Empty;
            guna2TabControl1.SelectedIndex = 0;

        }
        void LimpiarDetalle()
        {
            txtPartida.Text = string.Empty;
            cmbConcepto.SelectedIndex=-1;
            txtConcepto2.Text = string.Empty;
            txtCantidad.Text = "1";
            txtPrecio.Text = "0.00";
            txtUnidad.Text = string.Empty;
            txtDivisa1.Text = "MXN";
            txtTipoCambio1.Text = "1.00";
            txtImpuesto1.Text = "0.00";
            txtDescuento1.Text = "0.00";
            txtImpuestoIm.Text = "0.00";
            txtTotal1.Text = "0.00";
            txtArchivo1.Text = string.Empty;

        
        }

        void BloquearEncabezado()
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
            cmbAlmacen.Enabled = false;
            cmbCentroCostos.Enabled = false;

        }
        void DesbloquearEncabezado()
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
            cmbAlmacen.Enabled = true;
            button3.Enabled = true;
            cmbCentroCostos.Enabled = true;

        }

        void BloquearDetalle()
        {
            cmbConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtSubtotal1.Enabled = false;
            txtImpuesto1.Enabled = false;
            txtDescuento1.Enabled = false;
            txtImpuestoIm.Enabled = false;
        }
        void DesbloquearDetalle()
        {
            cmbConcepto.Enabled = true;
            txtConcepto2.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            //txtSubtotal1.Enabled = true;
            txtImpuesto1.Enabled = true;
            txtDescuento1.Enabled = true;
            //txtImpuestoIm.Enabled = true;
        }



        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmb(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button13_Click_1(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
            guna2GradientPanel2.SendToBack();
        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroDocumento_TextChanged_1(object sender, EventArgs e)
        {
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
        }

        private void txtFiltroNombre_TextChanged_1(object sender, EventArgs e)
        {
            c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
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

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;



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

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
        }

        private void toolStrip2_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
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

                if (!string.IsNullOrEmpty(txtFolio.Text) && cmbEstatus.Text == "Abierto")
                {
                    if (MessageBox.Show("El registro actual se perderá, ¿Desea continuar?", "Nueva Recepción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                Limpiarcabezado();
                LimpiarDetalle();
                DesbloquearEncabezado();
                DesbloquearDetalle();


                c.SeleccionarRecepcionProductos(cmbDocumento);
                c.SeleccionarConceptoDocumento(cmbFiltroDocumentoC);
                c.ruta();
                //c.SeleccionarOrdenEntrega(cmbOrdenCompra);
                c.CargarRrecepcion(dgvRecepciones, txtFiltro.Text, txtFiltroDocumento.Text, txtFiltroNombre.Text);
                cmbEstatus.SelectedIndex = 0;
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                txtDivisa.Text = "MXN";
                txtTipoCambio.Text = "1.00";
                txtElaborado.Text = DBLogin.usuario;
                cmbOrdenCompra.Text = txtFiltroOrdenC.Text;
                txtDiasVence.Text = "0";
                int Dias = Convert.ToInt32(txtDiasVence.Text);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                guna2TabControl1.Enabled = true;

            }
            else if (e.ClickedItem.Text == "CONSULTAR")
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
                if (string.IsNullOrEmpty(txtFolio.Text) )
                {
                    MessageBox.Show("Selecciona una recepción de producto");
                    return;


                }
                if(cmbEstatus.Text == "Abierto")
                {
                    MessageBox.Show("La Recepción debe de estar bloqueada");
                    return;
                }
                ReciboRecepcionProductos recepcion = new ReciboRecepcionProductos(txtFolio.Text);
                recepcion.ShowDialog();


            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            CalcularFechaVencimiento();
        }
        private void CalcularFechaVencimiento()
        {
            try
            {
                string diasv = string.IsNullOrEmpty(txtDiasVence.Text) ? "0" : txtDiasVence.Text;

                int Dias = Convert.ToInt32(diasv);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");


            }
            catch (Exception)
            {

                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtSubtotal1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtSubtotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtImpuesto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void txtDescuento1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilerias.SoloNumFracc(sender, e);
        }

        private void btnEliminarRecepcion_Click(object sender, EventArgs e)
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
                MessageBox.Show(c.CancelarRecepcion(txtFolio.Text));
                //c.ActualizarRegistroRecepcion3(txtFolio.Text, cmbEstatus.Text, txtAlmacen.Text);

                Limpiar();
            }
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(txtFolio.Text))
                {
                    MessageBox.Show("Es necesario crear el encabezado");
                    guna2TabControl1.SelectedIndex = 0;
                }
            }
        }

        private void txtPartida_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbAlmacen.Text))
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacen.Text = valores[0];
            }
        }

        private void txtImpuestoIm_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminarPartida_Click_1(object sender, EventArgs e)
        {
            c.ActualizarRecepcion(TxtFolio1.Text, txtPartida.Text);
            c.ReciboSaldosRecepcion(TxtFolio1.Text, txtSubtotal, txtDescuento, txtImpuestos, txtTotal, txtPartidas, txtSaldo);

        }
    }
    
}
