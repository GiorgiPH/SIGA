using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios;
using Guna.UI2.WinForms;
using PuntoVentas.Clases.Login;
using PV.Clases.Servicios;

namespace PV
{
    public partial class CatalogoServicios : Form
    {
        DBServicios c = new DBServicios();
        DBLogin s = new DBLogin();
        int Consulta = 0;
        public CatalogoServicios(int consulta)
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button7, "Nuevo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Productos");
            T.SetToolTip(button6, "Consultar Categorias");
            T.SetToolTip(button9, "Imprimir");
            Consulta = consulta;

      
            T.SetToolTip(guna2PictureBox1, "Clic para Desplegar");
            T.SetToolTip(guna2PictureBox2, "Clic para Ocultar");
       
        }

       

        private void Servicios_Load(object sender, EventArgs e)
        {
            cmbEstatus.Text = "Activo";
            c.SeleccionarCategorias(cmbCategorias);
            c.SeleccionarDivisa(cmbDivisa);
            c.CargarProductos(dataGridView2);
            c.SeleccionarConceptoGlobal(cmbConcepto);
        }
        void GenerarNoCategoria()
        {
            DBServicios.Folio = 0;
            c.ClaveProductoSiguiente();
            if (DBServicios.Folio == 0)
            {
                DBServicios.Folio = 1;
                txtClaveProducto.Text = Convert.ToString(DBServicios.Folio);

            }
            else
            {
                DBServicios.Folio = DBServicios.Folio + 1;
                txtClaveProducto.Text = Convert.ToString(DBServicios.Folio);

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

                if (cmbDivisa.Text == string.Empty)
                {
                    MessageBox.Show("Registre la divisa para continuar");
                }
                else
                {
                    MessageBox.Show(c.RegistroProducto(txtClaveProducto.Text, txtAlias.Text, txtDescripcion.Text, cmbEstatus.Text, txtCategoria.Text, txtFamilia.Text, cmbTipoCosteo.Text, txtCostoUnitario.Text, cmbDivisa.Text, txtDescuentoPorc.Text, txtDescuentoCant.Text, txtImpuestoPorc.Text, txtImpuestoCant.Text, txtPrecioVenta.Text, Foto, txtConcepto.Text));
                    Limpiar();
                    c.CargarProductos(dataGridView2);
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
            OpenFileDialog Abrir = new OpenFileDialog();

            Abrir.Filter = "Archivos JPEG(* .JPEG) |*.jpg";
            Abrir.InitialDirectory = "C:/";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                Bitmap foto = new Bitmap(Dir);

                Foto.Image = (Image)foto;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }

        void Limpiar()
        {
            txtClaveProducto.Clear();
            txtAlias.Clear();
            txtDescripcion.Clear();
            cmbEstatus.Text = "Activo";
           
            cmbCategorias.Text = null;
            
            cmbTipoCosteo.Text = null;
            txtCostoUnitario.Clear();
            cmbDivisa.Text = null;
            txtDescuentoPorc.Clear();
            txtDescuentoCant.Text = "0.00";
            txtImpuestoPorc.Clear();
            txtImpuestoCant.Text = "0.00";
            txtPrecioVenta.Clear();
            txtCategoria.Clear();
            txtFamilia.Clear();
            c.SeleccionarCategorias(cmbCategorias);
            c.SeleccionarFamilia(cmbFamilia, "0");
            Foto.Image = null;
            groupBox4.Enabled = false;
            PanelUsuario.Visible = false;
            cmbConcepto.Text = null;
            txtConcepto.Clear();
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
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                c.ConsultaProductoSeleccionado(Clave, txtAlias, txtDescripcion, cmbEstatus, txtCategoria, txtFamilia, cmbCategorias, cmbFamilia, cmbTipoCosteo, txtCostoUnitario, cmbDivisa, txtDescuentoPorc, txtDescuentoCant, txtImpuestoPorc, txtImpuestoCant, txtPrecioVenta, Foto, txtConcepto, cmbConcepto);
                groupBox4.Enabled = true;
                
                txtClaveProducto.Text = Clave;
                PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
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
        private void button9_Click(object sender, EventArgs e)
        {
            ReporteServicios reporteServicios = new ReporteServicios();
            reporteServicios.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtClaveProducto.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox4.Enabled = true;

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
            /*if (tgInventariable.Checked == false && cmbTipoCosteo.Text != "Ultima Compra")
            {
                MessageBox.Show("El tipo de costeo para los productos no inventariables son Ultima Compra");
                cmbTipoCosteo.SelectedIndex = 0;
            }*/
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
                        MessageBox.Show(c.EliminarDivisa(txtClaveProducto.Text));
                        c.CargarProductos(dataGridView2);
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
                }
                else
                {
                    Limpiar();
                    GenerarNoCategoria();
                    groupBox4.Enabled = true;

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

                ReporteServicios reporteServicios = new ReporteServicios();
                reporteServicios.ShowDialog();
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            PanelUsuario.Visible = false;
        }
    }
    
}
