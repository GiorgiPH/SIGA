using System;
using System.Drawing;
using System.Windows.Forms;
using PuntoVentas.Clases.ProductosServicios;
using PuntoVentas.Clases.Login;
using System.Linq;
using Condominios;
using PV;
using Guna.UI2.WinForms;
using PV.Clases;

namespace PuntoVentas
{
    public partial class CatalogoProductosServicios : Form
    {
        DBProductosServicios c = new DBProductosServicios();
        DBLogin s = new DBLogin();
        int Consulta = 0;

        public CatalogoProductosServicios(int consulta)
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button7, "Nuevo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Productos");
            T.SetToolTip(button6, "Consultar Categorias");
            T.SetToolTip(button9, "Imprimir");
            T.SetToolTip(guna2PictureBox1, "Clic para Desplegar");
            T.SetToolTip(guna2PictureBox2, "Clic para Ocultar");
            Consulta = consulta;
        }

        private void CatalogoProductosServicios_Load(object sender, EventArgs e)
        {
            //GenerarNoCategoria();
            cmbEstatus.Text = "Activo";
            c.SeleccionarCategorias(cmbCategorias);
            c.SeleccionarDivisa(cmbDivisa);
            c.CargarProductos(dataGridView2, txtFiltro.Text);
            c.SeleccionarConceptoGlobal(cmbConcepto);
        }

        void GenerarNoCategoria()
        {
            DBProductosServicios.Folio = 0;
            c.ClaveProductoSiguiente();
            if (DBProductosServicios.Folio == 0)
            {
                DBProductosServicios.Folio = 1;
                txtClaveProducto.Text = Convert.ToString(DBProductosServicios.Folio);

            }
            else
            {
                DBProductosServicios.Folio = DBProductosServicios.Folio + 1;
                txtClaveProducto.Text = Convert.ToString(DBProductosServicios.Folio);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveProducto.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion para continuar.");
            }
            else if (txtPrecioVenta.Text == string.Empty)
            {
                MessageBox.Show("Registre el precio de venta para continuar.");
            }
            else if (cmbCategorias.Text == string.Empty)
            {
                MessageBox.Show("Registre la categoria para continuar.");
            }
            else if (cmbFamilia.Text == string.Empty)
            {
                MessageBox.Show("Registre la familia para continuar.");
            }
            else if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el concepto global del impuesto para continuar.");
            }
            else
            {
                try
                {
                    // Limpieza/normalización de los campos numéricos usando el helper
                    string exMinimo = NumericHelper.ToInvariantString(txtExMinimo.Text, "Existencia Mínima");
                    string exMaximo = NumericHelper.ToInvariantString(txtExMaximo.Text, "Existencia Máxima");
                    string exActual = NumericHelper.ToInvariantString(txtExActual.Text, "Existencia Actual");
                    string costoUnitario = NumericHelper.ToInvariantString(txtCostoUnitario.Text, "Costo Unitario");
                    string descuentoPorc = NumericHelper.ToInvariantString(txtDescuentoPorc.Text, "Descuento %");
                    string descuentoCant = NumericHelper.ToInvariantString(txtDescuentoCant.Text, "Descuento Cantidad");
                    string impuestoPorc = NumericHelper.ToInvariantString(txtImpuestoPorc.Text, "Impuesto %");
                    string impuestoCant = NumericHelper.ToInvariantString(txtImpuestoCant.Text, "Impuesto Cantidad");
                    string precioVenta = NumericHelper.ToInvariantString(txtPrecioVenta.Text, "Precio de Venta");

                    if (tgInventariable.Checked == true)
                    {
                        if (cmbTipoCosteo.Text == string.Empty || cmbDivisa.Text == string.Empty)
                        {
                            MessageBox.Show("El tipo de costeo y divisa son obligatorios para productos inventariables");
                        }
                        else
                        {
                            MessageBox.Show(c.RegistroProducto(
                                txtClaveProducto.Text, txtAlias.Text, txtDescripcion.Text, cmbEstatus.Text,
                                txtMarca.Text, txtUnidadMedida.Text, txtPresentacion.Text, tgInventariable,
                                txtCaducidad.Text, txtCategoria.Text, txtFamilia.Text, txtProveedor.Text,
                                exMinimo, exMaximo, exActual, txtUbicacion.Text,
                                cmbTipoCosteo.Text, costoUnitario, cmbDivisa.Text,
                                descuentoPorc, descuentoCant, impuestoPorc, impuestoCant,
                                precioVenta, Foto, txtConcepto.Text));

                            Limpiar();
                            c.CargarProductos(dataGridView2, txtFiltro.Text);
                        }
                    }
                    else if (tgInventariable.Checked == false)
                    {
                        if (cmbDivisa.Text == string.Empty)
                        {
                            MessageBox.Show("Registre la divisa para continuar");
                        }
                        else
                        {
                            MessageBox.Show(c.RegistroProducto(
                                txtClaveProducto.Text, txtAlias.Text, txtDescripcion.Text, cmbEstatus.Text,
                                txtMarca.Text, txtUnidadMedida.Text, txtPresentacion.Text, tgInventariable,
                                txtCaducidad.Text, txtCategoria.Text, txtFamilia.Text, txtProveedor.Text,
                                exMinimo, exMaximo, exActual, txtUbicacion.Text,
                                cmbTipoCosteo.Text, costoUnitario, cmbDivisa.Text,
                                descuentoPorc, descuentoCant, impuestoPorc, impuestoCant,
                                precioVenta, Foto, txtConcepto.Text));

                            Limpiar();
                            c.CargarProductos(dataGridView2, txtFiltro.Text);
                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Moneda(ref Guna2TextBox txt)
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

        private void txtCostoUnitario_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtCostoUnitario);
        }

        private void txtDescuentoPorc_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoPorc);
        }

        private void txtDescuentoCant_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuentoCant);
        }

        private void txtImpuestoPorc_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoPorc);

            try
            {
                if (txtImpuestoPorc.Text != string.Empty)
                {
                    double costo = Convert.ToDouble(txtCostoUnitario.Text);
                    double descuento = Convert.ToDouble(txtImpuestoPorc.Text);
                    double total = (costo * descuento);
                    txtImpuestoCant.Text = Convert.ToString(total);
                }
                else
                {
                    txtImpuestoPorc.Text = "0";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("El formata del impuesto es incorrecto");
            }
           
        }

        private void txtImpuestoCant_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImpuestoCant);
        }

        private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecioVenta);
        }

        private void txtCostoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDescuentoPorc_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtImpuestoPorc_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtDescuentoPorc_Leave(object sender, EventArgs e)
        {
            try
            {
                double costo = Convert.ToDouble(txtCostoUnitario.Text);
                double descuento = Convert.ToDouble(txtDescuentoPorc.Text);
                double total = (costo * descuento);
                txtDescuentoCant.Text = Convert.ToString(total);
            }
            catch (Exception)
            {

                MessageBox.Show("El formato del descuento es incorrecto");
            }
          
        }

        private void txtImpuestoPorc_Leave(object sender, EventArgs e)
        {
            //double costo = Convert.ToDouble(txtCostoUnitario.Text);
            //double descuento = Convert.ToDouble(txtImpuestoPorc.Text);
            //double total = (costo * descuento);
            //txtImpuestoCant.Text = Convert.ToString(total);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();

            abrir.Filter = "Archivos de imagen (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Todos los archivos (*.*)|*.*";
            abrir.InitialDirectory = @"C:\";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(abrir.FileName))
                {
                    Foto.Image = new Bitmap(bmp);
                }
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }

        void Limpiar ()
        {
            txtClaveProducto.Clear(); 
            txtAlias.Clear(); 
            txtDescripcion.Clear(); 
            cmbEstatus.Text = "Activo";
            txtMarca.Clear(); 
            txtUnidadMedida.Clear(); 
            txtPresentacion.Clear();
            txtCaducidad.Clear();
            cmbCategorias.Text = null;
            txtProveedor.Clear(); 
            txtExMinimo.Text="0"; 
            txtExMaximo.Text = "0";
            txtExActual.Text = "0";
            txtUbicacion.Clear(); 
            cmbTipoCosteo.Text = null;
            txtCostoUnitario.Text = "0.00";
            cmbDivisa.Text = null;
            txtDescuentoPorc.Text = "0.00";
            txtDescuentoCant.Text = "0.00";
            txtImpuestoPorc.Text = "0.00";
            txtImpuestoCant.Text = "0.00";
            txtPrecioVenta.Text = "0.00";
            txtCategoria.Clear();
            txtFamilia.Clear();
            c.SeleccionarCategorias(cmbCategorias);
            c.SeleccionarFamilia(cmbFamilia, "0");
            Foto.Image = null;
            groupBox4.Enabled = false;
            PanelUsuario.Visible = false;
            cmbConcepto.Text = null;
            txtConcepto.Clear();
            txtPedidosProveedor.Text = "0.00";
            txtPedidosCliente.Text = "0.00";
            txtDisponibilidad.Text = "0.00";
            tgInventariable.Checked = false;

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCategoria();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (PanelUsuario.Visible == false)
            {
                PanelUsuario.Visible = true;
            }
            else if (PanelUsuario.Visible == true)
            {
                PanelUsuario.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Clave"].Value.ToString();

                c.ConsultaProductoSeleccionado(Clave, txtAlias, txtDescripcion, cmbEstatus, txtMarca, txtUnidadMedida, txtPresentacion, tgInventariable, txtCaducidad, txtCategoria, txtFamilia, cmbCategorias, cmbFamilia, txtProveedor, txtExMinimo, txtExMaximo, txtExActual, txtUbicacion, cmbTipoCosteo, txtCostoUnitario, cmbDivisa, txtDescuentoPorc, txtDescuentoCant, txtImpuestoPorc, txtImpuestoCant, txtPrecioVenta, Foto, txtConcepto, cmbConcepto, txtPedidosProveedor, txtPedidosCliente);

                CalcularDisponibilidad();
                groupBox4.Enabled = true;
                panel1.Enabled = true;
                txtClaveProducto.Text = Clave;
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void CatalogoProductosServicios_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategorias.Text != string.Empty)
            {
                string[] valores = c.InformacionCategorias(cmbCategorias.Text);
                txtCategoria.Text = valores[0];

                c.SeleccionarFamilia(cmbFamilia, txtCategoria.Text);
            }
            
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFamilia.Text != string.Empty)
            {
                string[] valores = c.InformacionFamilia(cmbFamilia.Text);
                txtFamilia.Text = valores[0];
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CatalogoFamiliasVer catalogoFamiliasVer = new CatalogoFamiliasVer();
            catalogoFamiliasVer.ShowDialog();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteProductosServicios reporteProductosServicios = new ReporteProductosServicios();
            reporteProductosServicios.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtClaveProducto.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;
                panel1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;
                panel1.Enabled = true;

            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionConcepto(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

       

        private void cmbTipoCosteo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tgInventariable.Checked== false && cmbTipoCosteo.Text!="Ultima Compra")
            {
                MessageBox.Show("El tipo de costeo para los productos no inventariables son Ulrima Compra");
                cmbTipoCosteo.SelectedIndex = 0;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveProducto.Text == string.Empty && txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show( c.EliminarDivisa(txtClaveProducto.Text));
                        c.CargarProductos(dataGridView2, txtFiltro.Text);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void tgInventariable_CheckedChanged(object sender, EventArgs e)
        {
            if (tgInventariable.Checked == true)
            {
                label1.Text = "Si";
            }
            else
            {
                label1.Text = "No";
            }
            if (tgInventariable.Checked == false && txtExActual.Text != "0")
            {
                MessageBox.Show("No es posible cambiar producto inventariable mientras tenga existencias");
                tgInventariable.Checked = true;
                label1.Text = "Si";
            }
            else if (tgInventariable.Checked == false && txtExActual.Text == "0")
            {
                cmbTipoCosteo.SelectedIndex = 0;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCategorias_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtCategoria.Text = string.Empty;
            string claveCategoria = string.Empty;
            if (cmbCategorias.Text != string.Empty)
            {
                string[] valores = c.InformacionCatergoria(cmbCategorias.Text);
                
                 claveCategoria = valores[0];

                txtCategoria.Text = claveCategoria; 
                if (claveCategoria != string.Empty)
                {
                 
                    c.CargarFamilias(cmbFamilia, claveCategoria);
                
                    if (cmbFamilia.Text  != string.Empty || cmbFamilia.Text != "")
                    {
                        cmbFamilia.SelectedIndex = 0;
                    }
                }

            }


          
        }

        private void cmbConcepto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionConcepto(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1013, 83);
            guna2GradientPanel6.Size = new Size(112, 583);

            // guna2GradientPanel7.Location = new Point(1013, 83);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

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
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;


            guna2GradientPanel6.Location = new Point(1076, 83);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;



            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);

            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);

          
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                //   Limpiar();


                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

            
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                if (txtClaveProducto.Text == string.Empty)
                {
                    Limpiar();
                    GenerarNoCategoria();
                    groupBox4.Enabled = true;
                    panel1.Enabled = true;
                }
                else
                {
                    Limpiar();
                    GenerarNoCategoria();
                    groupBox4.Enabled = true;
                    panel1.Enabled = true;

                }

            }
            else if (e.ClickedItem.Text == "CONSULTAR PRODUCTOS")
            {
             
                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

               
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;


                if (PanelUsuario.Visible == false)
                {
                    PanelUsuario.Visible = true;
                }
                else if (PanelUsuario.Visible == true)
                {
                    PanelUsuario.Visible = false;
                }
            }
            else if (e.ClickedItem.Text == "CONSULTAR CATEGORIAS")
            {

                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);


                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                CatalogoFamiliasVer catalogoFamiliasVer = new CatalogoFamiliasVer();
                catalogoFamiliasVer.ShowDialog();


            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;


                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                
                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

             
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
                ReporteProductosServicios reporteProductosServicios = new ReporteProductosServicios();
                reporteProductosServicios.ShowDialog();

            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            PanelUsuario.Visible = false;
        }
        private void CalcularDisponibilidad()
        {
            txtDisponibilidad.Text=(Convert.ToDecimal(txtExActual.Text) - Convert.ToDecimal(txtPedidosCliente.Text)).ToString();
        }

        private void cmbFamilia_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtFamilia.Text=string.Empty;
            if (!string.IsNullOrEmpty(cmbFamilia.Text))
            {
                string[] val=c.InformacionFamilia(cmbFamilia.Text);
                txtFamilia.Text=val[0];
            }
        }

        private void txtImpuestoPorc_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImpuestoCant);
        }

        private void txtDescuentoPorc_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuentoPorc);
        }

        private void txtDescuentoCant_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuentoCant);
        }

        private void txtImpuestoCant_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtImpuestoCant);
        }

        private void txtCostoUnitario_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtCostoUnitario);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            c.CargarProductos(dataGridView2, txtFiltro.Text);
        }

        private void txtPrecioVenta_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtPrecioVenta);
        }
    }
}
