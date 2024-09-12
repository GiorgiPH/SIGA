using PV.Clases.Inventario;
using PV.Clases.TipoMovimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class ConsultaInventario2 : Form
    {
        DBRegistrarEntradas c = new DBRegistrarEntradas();
        DBTipoMovimiento s = new DBTipoMovimiento();
        int Consulta = 0;

  
        DBPartidas s1 = new DBPartidas();

        string TipoM = string.Empty;
        string Descripcion = string.Empty;
        string Folio1 = string.Empty;

        public ConsultaInventario2(int consulta)
        {
            InitializeComponent();
            Consulta = consulta;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void txtPartidas_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ConsultaInventario2_Load(object sender, EventArgs e)
        {
            c.CargarDocumento(dataGridView1);
            c.SeleccionarDivisa(cmbDivisa);
            if (Consulta == 1)
            {
               // button2.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();

                c.Consulta(Folio, Tipo, Documento, txtFolioRegistrar, txtDescripcion, txtTotalPartidas, txtReferencia, txtAlmacen, txtTotal, txtNotas, cmbEstatus, txtElaborado, cmbDivisa, txtTipoCambio);

                txtTipoDocumento.Text = Tipo;

                //     PanelUsuario.Visible = false;
                guna2GradientPanel2.Visible = false;    
                
            }
            else
            {
                return;
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.Text != string.Empty)
            {
                c.CargarDocumentoFiltro(dataGridView1, cmbFiltro.Text, dtFiltro.Text);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "CONSULTAR")
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
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);

                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                c.CargarDocumento(dataGridView1);
                CambioTamañotoolstripPequeño();
            }

        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {

           /* guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
        
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
           */
            guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            

            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {

          /*  guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip1.Size = new Size(22, 569);
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
          */
            guna2GradientPanel4.Location = new Point(1076, 83);
            guna2GradientPanel4.Size = new Size(23, 569);

            toolStrip1.Size = new Size(23, 569);
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

        }

        void CambioTamañotoolstripPequeño()
        {

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

        }
        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            /*   guna2GradientPanel4.Size = new Size(80, 569);
               toolStrip1.Size = new Size(112, 569);   
               toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;*/
            guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;


            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedIndex = 1;


                    TipoM = txtTipoDocumento.Text;
            Descripcion = txtDescripcion.Text;
            Folio1 = txtFolioRegistrar.Text;
            txtTipoDocumento.Text = TipoM;
            txtDescripcion.Text = Descripcion;
            txtUltimoFolio.Text = Folio1;
            c.CargarPartida(dataGridView1, TipoM, Descripcion, Folio1);

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

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Partida = dataGridView1.Rows[e.RowIndex].Cells["NoPartida"].Value.ToString();
                string Documento = dataGridView1.Rows[e.RowIndex].Cells["Documento1"].Value.ToString();
                txtNoPartida.Text = Partida;

                c.ConsultaPartida(txtUltimoFolio.Text, Tipo, Partida, Documento, cmbProducto, txtConcepto, txtAlias, txtTipoCosteo, txtExistencias, txtCantidad, txtUnidad, txtPrecio, cmbDivisa1, txtTipoCambio, txtTotal);
              //  PanelUsuario.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void txtTotal1_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPrecio);
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbFiltro.Text = null;
            dtFiltro.ResetText();
            c.CargarDocumento(dataGridView1);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
         
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;
         
            toolStripButton1.Visible = true;

            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;

            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
           
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

            c.CargarDocumento(dataGridView1);

            guna2GradientPanel6.Location = new Point(1077, 83);
            guna2GradientPanel6.Size = new Size(23, 569);
            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;

            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;
        }
    }
}
