using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PV.Clases.OrdenCompra;
using PuntoVentas.Clases.Login;
using PV;
using System.Drawing;


namespace PV
{
    public partial class Requisicion2 : Form
    {

        DBOrdenCompra c = new DBOrdenCompra();

        string Tipo = string.Empty;

        public Requisicion2()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = true;
            PanelPartidasRequisicion.BringToFront();
            cmbConcepto.Items.Clear();
            c.SeleccionarProducto2(cmbConcepto);
            c.ConsultaRequisicion5(TxtFolio2.Text, txtPartida);
            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
            txtPartida.Enabled = false;
            cmbConcepto.Enabled = true;
            txtClaveConcepto.Enabled = false;
            txtConcepto2.Enabled = false;
            txtUnidad.Enabled = true;
            txtExistencia.Enabled = false;
            txtDepartamento2.Enabled = false;
            txtSubDepartamento.Enabled = false;
          
            if (Tipo =="Nuevo")
            {

            guna2Button5.Visible = false;
            guna2Button5.Enabled = false;

            guna2Button7.Visible = true;
            guna2Button8.Visible = true;
            }

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroDocumento.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroRecepcion(dataGridView1, txtFiltro.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }
        }

        private void txtFiltroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocumentoRecepcion(dataGridView1, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltroDocumento.Clear();
                txtFiltro.Clear();
                c.CargarRecibosFiltroCentroCostoRecepcion(dataGridView1, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }
        }

        private void Requisicion2_Load(object sender, EventArgs e)
        {
            c.CargarRequisicion(dataGridView1);
            c.SeleccionarConceptoDocumentoRequisicion(cmbDocumento);
            c.CargarRequisicion(dataGridView1);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtElaborado.Text = DBLogin.usuario;

            cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            int Dias = Convert.ToInt32(txtDiasVence.Text);
            DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
            FechaVence = FechaVence.AddDays(Dias);
            txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
            c.SeleccionarCentroCosto(cmbCentroCosto);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            else if (txtCentroCosto.Text == string.Empty)
            {
                MessageBox.Show("Registre el centro de costo antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una orden de compra Bloqueado o Cancelado");
                return;
            }
            else if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }
            else
            {
                string ReciboCol = string.Empty;

                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarRequisicion(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtCentroCosto.Text, txtDepartamento.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text);
                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

               // Guna.UI2.WinForms.Guna2TabControl
               // PartidaRequisicion partidas = new PartidaRequisicion(txtFolio.Text, txtCentroCosto.Text, txtDepartamento.Text);
               // partidas.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            button6.BackColor = Color.Gainsboro;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
         
            if (e.ClickedItem.Text == "NUEVO")
            {
             //   Limpiar();
                LimpiarEncabezado();
                Limpiardetalle();

                c.CargarRequisicion(dataGridView1);
                c.SeleccionarConceptoDocumentoRequisicion(cmbDocumento);
                c.CargarRequisicion(dataGridView1);
                cmbEstatus.SelectedIndex = 0;
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                txtElaborado.Text = DBLogin.usuario;

                cmbDocumento.Text = txtDocumentoInsc.Text;
                txtDiasVence.Text = "0";
                int Dias = Convert.ToInt32(txtDiasVence.Text);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                c.SeleccionarCentroCosto(cmbCentroCosto);
                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = true;
                guna2Button5.Enabled = true;
                guna2Button6.Enabled = true;
                guna2Button7.Enabled = true;
                guna2Button8.Enabled = true;
                Tipo = "Nuevo";
                cmbDocumento.Enabled = true;
                cmbCentroCosto.Enabled = true;
                cmbDepartamento.Enabled = true;
                c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
              
               /* CambioTamañotoolstripPequeño();
                guna2TabControl1.Enabled = true;

                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;*/
             
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
                cmbDocumento.Enabled = false;
                cmbCentroCosto.Enabled = false;
                cmbDepartamento.Enabled = false;

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                guna2Button8.Enabled = false;
                Tipo = "Consulta";


            /*    guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                CambioTamañotoolstripPequeño();*/
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
               /* guna2GradientPanel4.Size = new Size(22, 569);
              /  toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                CambioTamañotoolstripPequeño();*/
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento antes de continuar");
                return;
            }
            else if (txtCentroCosto.Text == string.Empty)
            {
                MessageBox.Show("Registre el centro de costo antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a una orden de compra Bloqueado o Cancelado");
                return;
            }
            else if (cmbDocumento.Text == string.Empty)
            {
                MessageBox.Show("Registre el Documento para continuar");
                return;
            }
            else
            {
                string ReciboCol = string.Empty;

                if (txtFolio.Text == string.Empty)
                {
                    c.InsertarRequisicion(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtCentroCosto.Text, txtDepartamento.Text, txtNotas.Text, txtElaborado.Text, txtConsecutivo.Text);
                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                // Guna.UI2.WinForms.Guna2TabControl
                // PartidaRequisicion partidas = new PartidaRequisicion(txtFolio.Text, txtCentroCosto.Text, txtDepartamento.Text);
                // partidas.ShowDialog();

            }
            
            


              TxtFolio2.Text = txtFolio.Text ;
            txtCentroCosto2.Text = txtCentroCosto.Text;
            txtDepartamento2.Text = txtDepartamento.Text;


            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;

            button6.BackColor = Color.Gainsboro;

            guna2TabControl1.SelectedIndex = 1;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && txtPartidas.Text != "0")
            {
                MessageBox.Show("No es posible limpiar la orden de compra, confirme la orden para continuar");
            }
            else if (cmbEstatus.Text == "Abierto" && txtPartidas.Text == "0")
            {
                c.eliminarRequisicion(txtFolio.Text);
                Limpiar();
                txtDiasVence.Focus();
            }
            else
            {
                Limpiar();
                txtDiasVence.Focus();
            }
           

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el recibo");
                return;
            }
            else if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar una orden de compra que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El saldo de esta orden de compra sera cancelado, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Cancelado";
                c.ActualizarRequisicionEstatus(txtFolio.Text, cmbEstatus.Text, txtCentroCosto.Text);
                Limpiar();
                MessageBox.Show("Orden de Compra Cancelado");
            }

        }


        void Limpiar()
        {
            txtFolio.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtCentroCosto.Clear();
            txtPartidas.Text = "0";
            txtNotas.Clear();
            txtDepartamento.Clear();
            txtConsecutivo.Clear();
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            button1.BackColor = Color.Gainsboro;
            txtNotas.Enabled = false;
          //  groupBox2.Enabled = false;
            cmbCentroCosto.Text = null;
            cmbDepartamento.Text = null;
            cmbCentroCosto.Enabled = false;
            cmbDepartamento.Enabled = false;
            c.SeleccionarCentroCosto(cmbCentroCosto);
            c.SeleccionarCentroCostoDepartamento(cmbDepartamento, cmbCentroCosto.Text);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRequisicion(TxtFolio2.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtCantidad.Text == "0.00" || txtCantidad.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar una cantidad no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRequisicion(TxtFolio2.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRequisicion(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text);
                c.ActualizarRequisicion(TxtFolio2.Text, txtPartida.Text);
                //   this.Close();
                PanelPartidasRequisicion.Visible = false;
                c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
                guna2Button9.Visible = true;
                LimpiarDetalleNuevapartida();
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            else if (txtFrecuencia.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRequisicion(TxtFolio2.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text);
             //   Limpiar();
                c.ConsultaRequisicion5(TxtFolio2.Text, txtPartida);
                LimpiarDetalleNuevapartida();
            }

        }

        private void cmbDocumento_Click_1(object sender, EventArgs e)
        {
  cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
        /*  txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;*/
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
                    if (txtFolio.Text == string.Empty)
                    {
                        c.ConsecutivoRequision(txtConsecutivo, txtClave.Text);
                    }
                 //   groupBox2.Enabled = true;
                    txtDiasVence.Focus();
                }

            }
        }

        private void cmbDocumento_Leave(object sender, EventArgs e)
        {
       //   txtDiasVence.BackColor = Color.Orange;
        }

        private void cmbCentroCosto_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;

            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
        }

        private void cmbCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCentroCosto.Text != string.Empty)
            {
                string[] valores = c.InformacionCentroCosto(cmbCentroCosto.Text);
                txtCentroCosto.Text = valores[0];
                c.SeleccionarCentroCostoDepartamento(cmbDepartamento, txtCentroCosto.Text);
            }
        }

        private void cmbCentroCosto_Leave(object sender, EventArgs e)
        {
            cmbDepartamento.DroppedDown = true;
        }

        private void cmbDepartamento_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;

      //      txtDiasVence.BackColor = Color.White;
         //   txtNotas.BackColor = Color.White;
          //  button2.BackColor = Color.Gainsboro;
          //  button6.BackColor = Color.Gainsboro;
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartamento.Text != string.Empty)
            {
                string[] valores = c.InformacionCentroCostoDepartamento(cmbDepartamento.Text);
                txtDepartamento.Text = valores[0];
            }
        }

        private void cmbDepartamento_Leave(object sender, EventArgs e)
        {
          //  txtNotas.BackColor = Color.Orange;
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                txtClave.Text = valores[0];
                txtConcepto3.Text = valores[1];
                txtUnidad.Text = valores[3];
                txtExistencia.Text = valores[5];

            }
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            /*            guna2GradientPanel4.Size = new Size (80, 569);
                        toolStrip1.Size = new Size(112, 569);
                        toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                        toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

                        toolStripButton1.Size = new Size(50, 60);*/

     /*/       guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;
     */



        }

        private void guna2GradientPanel4_MouseMove(object sender, MouseEventArgs e)
        {
           // guna2GradientPanel4.Size = new Size(112, 569);
        }

        private void guna2GradientPanel4_MouseLeave(object sender, EventArgs e)
        {
          //  guna2GradientPanel4.Size = new Size(22, 569);
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
          /*  guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip1.Size = new Size(22, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;*/
  /*  
            guna2GradientPanel4.Location = new Point(1076, 83);
        guna2GradientPanel4.Size = new Size(23, 569);

            toolStrip1.Size = new Size(23, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);

            // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);


            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);*/


        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            /*       guna2GradientPanel4.Size = new Size(80, 569); 
               toolStrip1.Size = new Size(112, 569);
               toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                   toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
                   toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

                   toolStripButton1.Size = new Size(50, 60);*/

         /*   guna2GradientPanel4.Location = new Point(1013, 83);
            guna2GradientPanel4.Size = new Size(86, 583);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton11.Size = new Size(85, 75);
            toolStripButton11.AutoSize = false;

            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton12.Size = new Size(85, 75);
            toolStripButton12.AutoSize = false;

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton13.Size = new Size(85, 75);
            toolStripButton13.AutoSize = false;
         */
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
         //  Limpiar();
            LimpiarEncabezado();
            Limpiar();
            Limpiardetalle();
            guna2TabControl1.Enabled = false;
            guna2TabControl1.SelectedIndex = 0;
            guna2Button9.Visible = false;
            c.CargarRequisicion(dataGridView1);

        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroDocumento.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroRecepcion(dataGridView1, txtFiltro.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }

        }

        private void txtFiltroDocumento_TextChanged_1(object sender, EventArgs e)
        {

            if (txtFiltroDocumento.Text != string.Empty)
            {
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltroDocumentoRecepcion(dataGridView1, txtFiltroDocumento.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltroDocumento.Clear();
                txtFiltro.Clear();
                c.CargarRecibosFiltroCentroCostoRecepcion(dataGridView1, txtFiltroNombre.Text);
            }
            else
            {
                c.CargarRequisicion(dataGridView1);
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaRequisicion(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtCentroCosto, txtDepartamento, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
               // panel2.Visible = false;

                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                cmbDocumento.Enabled = true;
                c.SeleccionarCentroCosto2(cmbCentroCosto, txtCentroCosto.Text);
                c.SeleccionarCentroCostoDepartamento2(cmbDepartamento, txtDepartamento.Text);

                cmbCentroCosto.SelectedIndex = 0;
                cmbDepartamento.SelectedIndex = 0;
                c.CargarRequisicionPartidas(guna2DataGridView1, txtFolio.Text);
                //groupBox2.Enabled = true;
                guna2TabControl1.Enabled = true;
                guna2GradientPanel2.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
 txtConcepto3.Text = null;

            if (e.RowIndex != -1)
            {


                string Partida = guna2DataGridView1.Rows[e.RowIndex].Cells["Partida"].Value.ToString();
                c.ConsultaPartidaRequisicion(txtFolio.Text, Partida, txtClaveConcepto, txtConcepto3, txtConcepto2, txtCantidad, txtUnidad, txtExistencia);

                txtPartida.Text = Partida;
                PanelPartidasRequisicion.Visible = true;
                PanelPartidasRequisicion.BringToFront();
                guna2Button11.Visible = true;
                cmbConcepto.Items.Clear();
                //cmbConcepto.Text = txtConcepto3.Text;
                cmbConcepto.Items.Add(txtConcepto3.Text);
                cmbConcepto.SelectedIndex = 0;
                txtPartida.Enabled = false;
                cmbConcepto.Enabled = false;
                txtClaveConcepto.Enabled = false;
                txtConcepto2.Enabled = false;
                txtUnidad.Enabled = false;
                txtExistencia.Enabled = false;
                txtDepartamento2.Enabled = false;
                txtSubDepartamento.Enabled = false;
                if (Tipo == "Nuevo")
                {
                    guna2Button7.Visible = false;
                    guna2Button8.Visible = false;
                    guna2Button5.Visible = true;
                    guna2Button5.Enabled = true;
                }
             
            }
            else
            {
                return;
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            PanelPartidasRequisicion.Visible = false;
            guna2Button11.Visible = false;
        }


        void LimpiarEncabezado()
        {
            txtFolio.Clear();
txtCentroCosto.Clear();
            txtDepartamento.Clear();
            txtReciboCol.Clear();
         //   txtDocumentoInsc.Text = null;
            txtReciboInsc.Clear();
            txtDocumentoCol.Clear();
          //  cmbDocumento.Items.Clear();
            cmbDocumento.Text = null;
          //  cmbDocumento.Enabled = false;
            txtDocumento.Clear();
            txtClave.Clear();
            cmbEstatus.Text = null;
            txtConsecutivo.Clear();
            txtFecha.Clear();
            txtDiasVence.Clear();
            txtFechaVence.Clear();
            cmbCentroCosto.Text = null;
            cmbDepartamento.Text = null;
            txtNotas.Clear();
            txtPartidas.Clear();
            txtTotalConceptos.Clear();
            txtElaborado.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtPartidas.Text = "0";
            c.SeleccionarCentroCosto(cmbCentroCosto);
            c.SeleccionarCentroCostoDepartamento(cmbDepartamento, cmbCentroCosto.Text);
        }

        void Limpiardetalle() 
        {
            TxtFolio2.Clear();
            txtFrecuencia.Clear();
            txtTipo.Clear();
            txtCentroCosto2.Clear();
            txtConcepto3.Clear();
            txtClave2.Clear();
            txtPartida.Clear();
            cmbConcepto.Text = null;
            txtConcepto3.Clear();
            txtClaveConcepto.Clear();
            txtConcepto2.Clear();
            txtCantidad.Clear();
            txtUnidad.Clear();
            txtExistencia.Clear();
            txtDepartamento2.Clear();
            txtSubDepartamento.Clear(); 
        }
        
       
        

       void LimpiarDetalleNuevapartida()
        {
          //  txtPartida.Text = string.Empty;
            cmbConcepto.Items.Clear();       
            txtClaveConcepto.Clear();
            txtConcepto2.Clear();
          //  txtCantidad.Clear();
            txtUnidad.Clear();
            txtExistencia.Clear();
            txtDepartamento2.Clear();
            txtSubDepartamento.Clear();
            c.SeleccionarProducto2(cmbConcepto);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LimpiarDetalleNuevapartida();
            PanelPartidasRequisicion.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
           c.eliminarPartidaRequisicion(TxtFolio2.Text,txtPartida.Text);
            PanelPartidasRequisicion.Visible = false;
            c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);
        }

   /*     void CambioTamañotoolstripPequeño()
        {

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton11.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton12.Size = new Size(23, 79);

            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton13.Size = new Size(23, 79);
        }*/

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
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
            guna2GradientPanel6.Size = new Size(23, 569);

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
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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


                LimpiarEncabezado();
                Limpiardetalle();

                c.CargarRequisicion(dataGridView1);
                c.SeleccionarConceptoDocumentoRequisicion(cmbDocumento);
                c.CargarRequisicion(dataGridView1);
                cmbEstatus.SelectedIndex = 0;
                txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
                txtElaborado.Text = DBLogin.usuario;

                cmbDocumento.Text = txtDocumentoInsc.Text;
                txtDiasVence.Text = "0";
                int Dias = Convert.ToInt32(txtDiasVence.Text);
                DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                FechaVence = FechaVence.AddDays(Dias);
                txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                c.SeleccionarCentroCosto(cmbCentroCosto);
                guna2TabControl1.SelectedIndex = 0;
                guna2TabControl1.Enabled = true;

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = true;
                guna2Button5.Enabled = true;
                guna2Button6.Enabled = true;
                guna2Button7.Enabled = true;
                guna2Button8.Enabled = true;
                Tipo = "Nuevo";
                cmbDocumento.Enabled = true;
                cmbCentroCosto.Enabled = true;
                cmbDepartamento.Enabled = true;
                c.CargarRequisicionPartidas(guna2DataGridView1, TxtFolio2.Text);

            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {

                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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
                cmbDocumento.Enabled = false;
                cmbCentroCosto.Enabled = false;
                cmbDepartamento.Enabled = false;

                guna2Button2.Visible = true;
                guna2Button5.Visible = true;
                guna2Button6.Visible = true;
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2Button2.Enabled = false;
                guna2Button5.Enabled = false;
                guna2Button6.Enabled = false;
                guna2Button7.Enabled = false;
                guna2Button8.Enabled = false;
                Tipo = "Consulta";
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {

                guna2GradientPanel6.Location = new Point(1077, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
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

            }
        }
    }
}


