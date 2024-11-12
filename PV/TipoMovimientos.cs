using System;
using System.Windows.Forms;
using PV.Clases.TipoMovimiento;
using PV;
using System.Drawing;

namespace PuntoVentas
{
    public partial class TipoMovimientos : Form
    {
        DBTipoMovimiento c = new DBTipoMovimiento();

        public TipoMovimientos()
        {
            InitializeComponent();
        }

        private void TipoMovimientos_Load(object sender, EventArgs e)
        {
            c.CargarEntrada(guna2DataGridView1);
            c.CargarSalida(guna2DataGridView2);
            c.CargarTraspaso(dataGridView3);
            c.SeleccionarAlmacen(cmbAlmacen);
            c.SeleccionarAlmacen(cmbAlmacenSalida);
            c.SeleccionarAlmacen(cmbAlmacenTraspado);

            ToolTip T = new ToolTip();
            T.SetToolTip(guna2PictureBox1, "Clic para Desplegar");
            T.SetToolTip(guna2PictureBox2, "Clic para Ocultar");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtDocumentoEntrada.Text== string.Empty)
            {
                MessageBox.Show("Registre el documento de entrada.");
            }
            else if (txtDescripcionEntrada.Text== string.Empty)
            {
                MessageBox.Show("Registre la descripcion de entrada.");
            }
            else
            {
                c.ClaveDocumentoEntradaSiguiente(txtDocumentoEntrada.Text);
                txtUltimoFolioEntrada.Text = Convert.ToString(DBTipoMovimiento.Entrada);

                MessageBox.Show(c.RegistroMovimiento(txtTipoMovimientoEntrada.Text, txtDocumentoEntrada.Text, txtDescripcionEntrada.Text, cmbEstatusEntrada.Text, txtUltimoFolioEntrada.Text, rdbBloquearSiEntrada, rdbBloquearNoEntrada, rdbSiAfectaCosto, rdbNoAfectaCosto, txtAlmacenEntrada.Text, txtNotasEntradas.Text));
                LimpiarEntrada();
                txtUltimoFolioEntrada.Text = Convert.ToString(DBTipoMovimiento.Entrada);
                c.CargarEntrada(guna2DataGridView1);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtDocumentoSalida.Text == string.Empty)
            {
                MessageBox.Show("Registre el documento de salida.");
            }
            else if (txtDescripcionSalida.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion de salida.");
            }
            else
            {
                c.ClaveDocumentoSalidaSiguiente(txtDocumentoEntrada.Text);
                txtUltimoFolioSalida.Text = Convert.ToString(DBTipoMovimiento.Salida);

                MessageBox.Show(c.RegistroMovimientoST(txtTipoMovimientoSalida.Text, txtDocumentoSalida.Text, txtDescripcionSalida.Text, cmbEstatusSalida.Text, txtUltimoFolioSalida.Text, rdbBloquearSiSalida, rdbBloquearNoSalida, txtAlmacenSalida.Text, txtNotasSalida.Text));
                LimpiarSalida();
                //c.ClaveDocumentoSalidaSiguiente(txtDocumentoSalida.Text);
                //txtUltimoFolioSalida.Text = Convert.ToString(DBTipoMovimiento.Salida);
                c.CargarSalida(guna2DataGridView2);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtDocumentoTraspaso.Text == string.Empty)
            {
                MessageBox.Show("Registre el documento de Traspaso.");
            }
            else if (txtDescripcionTraspaso.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion de Traspaso.");
            }
            else
            {
                c.ClaveDocumentoTraspasoSiguiente(txtDocumentoTraspaso.Text);
                txtUltimoFolioTraspaso.Text = Convert.ToString(DBTipoMovimiento.Traspaso);

                MessageBox.Show(c.RegistroMovimientoST(txtTipoMovimientoTraspaso.Text, txtDocumentoTraspaso.Text, txtDescripcionTraspaso.Text, cmbEstatusTraspaso.Text, txtUltimoFolioTraspaso.Text, rdbBloquearSiTraspaso, rdbBloquearNoTraspaso, txtAlmacenTraspaso.Text, txtNotasTraspaso.Text));
                LimpiarTraspasos();
                //c.ClaveDocumentoTraspasoSiguiente();
                //txtUltimoFolioTraspaso.Text = Convert.ToString(DBTipoMovimiento.Traspaso);
                c.CargarTraspaso(dataGridView3);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimpiarEntrada();
            //c.ClaveDocumentoEntradaSiguiente();
            //txtUltimoFolioEntrada.Text = Convert.ToString(DBTipoMovimiento.Entrada);
        }

        void BloquearEntrada()
        {
            txtTipoMovimientoEntrada.Enabled = false;
            txtDocumentoEntrada.Enabled = false;
            txtDescripcionEntrada.Enabled = false;
            cmbEstatusEntrada.Enabled = false;
            txtUltimoFolioEntrada.Enabled = false;
            rdbBloquearSiEntrada.Enabled = false;
            rdbBloquearNoEntrada.Enabled = false;
            rdbSiAfectaCosto.Enabled = false;
            rdbNoAfectaCosto.Enabled = false;
            txtAlmacenEntrada.Enabled = false;
            txtNotasEntradas.Enabled = false;
            txtUltimoFolioEntrada.Enabled = false;
            cmbAlmacen.Enabled = false;
            txtAlmacenEntrada.Enabled = false;
            groupBox5.Enabled = false;
        }

        void BloquearSalida()
        {
            txtTipoMovimientoSalida.Enabled = false;
            txtDocumentoSalida.Enabled = false;
            txtDescripcionSalida.Enabled = false;
            cmbEstatusSalida.Enabled = false;
            txtUltimoFolioSalida.Enabled = false;
            rdbBloquearSiSalida.Enabled = false;
            rdbBloquearNoSalida.Enabled = false;
            txtAlmacenSalida.Enabled = false;
            txtNotasSalida.Enabled = false;
            txtUltimoFolioSalida.Enabled = false;
            cmbAlmacenSalida.Enabled = false;
            txtAlmacenSalida.Enabled = false;
            groupBox8.Enabled = false;
        }

        void BloquearTraspasos()
        {
            txtTipoMovimientoTraspaso.Enabled = false;
            txtDocumentoTraspaso.Enabled = false;
            txtDescripcionTraspaso.Enabled = false;
            cmbEstatusTraspaso.Enabled = false;
            txtUltimoFolioTraspaso.Enabled = false;
            rdbBloquearSiTraspaso.Enabled = false;
            rdbBloquearNoTraspaso.Enabled = false;
            txtAlmacenTraspaso.Enabled = false;
            txtNotasTraspaso.Enabled = false;
            txtUltimoFolioTraspaso.Enabled = false;
            cmbAlmacenTraspado.Enabled = false;
            txtAlmacenTraspaso.Enabled = false;
            groupBox10.Enabled = false;
        }

        void DesbloquearEntrada()
        {
            txtTipoMovimientoEntrada.Enabled = true;
            txtDocumentoEntrada.Enabled = true;
            txtDescripcionEntrada.Enabled = true;
            cmbEstatusEntrada.Enabled = true;
            txtUltimoFolioEntrada.Enabled = true;
            rdbBloquearSiEntrada.Enabled = true;
            rdbBloquearNoEntrada.Enabled = true;
            rdbSiAfectaCosto.Enabled = true;
            rdbNoAfectaCosto.Enabled = true;
            txtAlmacenEntrada.Enabled = true;
            txtNotasEntradas.Enabled = true;
            txtUltimoFolioEntrada.Enabled = true;
            cmbAlmacen.Enabled = true;
            txtAlmacenEntrada.Enabled = true;
            groupBox5.Enabled = true;
        }

        void DesbloquearSalida()
        {
            txtTipoMovimientoSalida.Enabled = true;
            txtDocumentoSalida.Enabled = true;
            txtDescripcionSalida.Enabled = true;
            cmbEstatusSalida.Enabled = true;
            txtUltimoFolioSalida.Enabled = true;
            rdbBloquearSiSalida.Enabled = true;
            rdbBloquearNoSalida.Enabled = true;
            txtAlmacenSalida.Enabled = true;
            txtNotasSalida.Enabled = true;
            txtUltimoFolioSalida.Enabled = true;
            cmbAlmacenSalida.Enabled = true;
            txtAlmacenSalida.Enabled = true;
            groupBox8.Enabled = true;
        }

        void DesbloquearTraspasos()
        {
            txtTipoMovimientoTraspaso.Enabled = true;
            txtDocumentoTraspaso.Enabled = true;
            txtDescripcionTraspaso.Enabled = true;
            cmbEstatusTraspaso.Enabled = true;
            txtUltimoFolioTraspaso.Enabled = true;
            rdbBloquearSiTraspaso.Enabled = true;
            rdbBloquearNoTraspaso.Enabled = true;
            txtAlmacenTraspaso.Enabled = true;
            txtNotasTraspaso.Enabled = true;
            txtUltimoFolioTraspaso.Enabled = true;
            cmbAlmacenTraspado.Enabled = true;
            txtAlmacenTraspaso.Enabled = true;
            groupBox10.Enabled = true;
        }


        void LimpiarEntrada ()
        {
            txtTipoMovimientoEntrada.Text = "E";
            txtDocumentoEntrada.Clear();
            txtDescripcionEntrada.Clear();
            cmbEstatusEntrada.ResetText(); 
            txtUltimoFolioEntrada.Clear();
            rdbBloquearSiEntrada.Checked = false;
            rdbBloquearNoEntrada.Checked = false;
            rdbSiAfectaCosto.Checked = false;
            rdbNoAfectaCosto.Checked = false;
            txtAlmacenEntrada.Clear();
            txtNotasEntradas.Clear();
            txtUltimoFolioEntrada.Clear();
            cmbAlmacen.Text = null;
            txtAlmacenEntrada.Clear();
            groupBox5.Enabled = false;
        }

        void LimpiarSalida()
        {
            txtTipoMovimientoSalida.Text = "S";
            txtDocumentoSalida.Clear();
            txtDescripcionSalida.Clear();
            cmbEstatusSalida.ResetText();
            txtUltimoFolioSalida.Clear();
            rdbBloquearSiSalida.Checked = false;
            rdbBloquearNoSalida.Checked = false;
            txtAlmacenSalida.Clear();
            txtNotasSalida.Clear();
            txtUltimoFolioSalida.Clear();
            cmbAlmacenSalida.Text = null;
            txtAlmacenSalida.Clear();
            groupBox8.Enabled = false;
        }

        void LimpiarTraspasos ()
        {
            txtTipoMovimientoTraspaso.Text = "T";
            txtDocumentoTraspaso.Clear();
            txtDescripcionTraspaso.Clear();
            cmbEstatusTraspaso.ResetText();
            txtUltimoFolioTraspaso.Clear();
            rdbBloquearSiTraspaso.Checked = false;
            rdbBloquearNoTraspaso.Checked = false;
            txtAlmacenTraspaso.Clear();
            txtNotasTraspaso.Clear();
            txtUltimoFolioTraspaso.Clear();
            cmbAlmacenTraspado.Text = null;
            txtAlmacenTraspaso.Clear();
            groupBox10.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarSalida();
            //c.ClaveDocumentoSalidaSiguiente();
            //txtUltimoFolioSalida.Text = Convert.ToString(DBTipoMovimiento.Salida);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LimpiarTraspasos();
            //c.ClaveDocumentoTraspasoSiguiente();
            //txtUltimoFolioTraspaso.Text = Convert.ToString(DBTipoMovimiento.Traspaso);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex==0)
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
            else if (tabControl1.SelectedIndex==1)
            {
                if (panel3.Visible == false)
                {
                    panel3.Visible = true;
                }
                else if (panel3.Visible == true)
                {
                    panel3.Visible = false;
                }
            }
            else if (tabControl1.SelectedIndex==2)
            {
                if (panel5.Visible == false)
                {
                    panel5.Visible = true;
                }
                else if (panel5.Visible == true)
                {
                    panel5.Visible = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = guna2DataGridView2.Rows[e.RowIndex].Cells["Tipo2"].Value.ToString();
                string Documento = guna2DataGridView2.Rows[e.RowIndex].Cells["Documento2"].Value.ToString();
                c.ConsultaSTSeleccionado(Tipo, Documento, txtDescripcionSalida, cmbEstatusSalida, txtUltimoFolioSalida, rdbBloquearSiSalida, rdbBloquearNoSalida, txtAlmacenSalida, txtNotasSalida);
                txtDocumentoSalida.Text = Documento;
                panel3.Visible = false;

                if (txtAlmacenSalida.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenSalida.Text);
                    cmbAlmacenSalida.Text = valores[0];
                }
                groupBox8.Enabled = true;
                guna2GradientPanel3.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = dataGridView3.Rows[e.RowIndex].Cells["Tipo3"].Value.ToString();
                string Documento = dataGridView3.Rows[e.RowIndex].Cells["Documento3"].Value.ToString();
                c.ConsultaSTSeleccionado(Tipo, Documento, txtDescripcionTraspaso, cmbEstatusTraspaso, txtUltimoFolioTraspaso, rdbBloquearSiTraspaso, rdbBloquearNoTraspaso, txtAlmacenTraspaso, txtNotasTraspaso);
                txtDocumentoTraspaso.Text = Documento;
                panel5.Visible = false;

                if (txtAlmacenTraspaso.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenTraspaso.Text);
                    cmbAlmacenTraspado.Text = valores[0];
                }
                groupBox10.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void txtAlmacenTraspaso_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAlmacenSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAlmacenEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReporteTipoMovimento reporteTipoMovimento = new ReporteTipoMovimento();
            reporteTipoMovimento.ShowDialog();
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacenEntrada.Text = valores[0];
            }
        }

        private void cmbAlmacenSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacenSalida.Text = valores[0];
            }
        }

        private void cmbAlmacenTraspado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text != string.Empty)
            {
                string[] valores = c.InformacionAlmacen(cmbAlmacen.Text);
                txtAlmacenTraspaso.Text = valores[0];
            }
        }

        private void txtDocumentoEntrada_Leave(object sender, EventArgs e)
        {
            if (txtDocumentoEntrada.Text!= string.Empty)
            {
                c.ConsultaEntradaSeleccionado(txtTipoMovimientoEntrada.Text, txtDocumentoEntrada.Text, txtDescripcionEntrada, cmbEstatusEntrada, txtUltimoFolioEntrada, rdbBloquearSiEntrada, rdbBloquearNoEntrada, rdbSiAfectaCosto, rdbNoAfectaCosto, txtAlmacenEntrada, txtNotasEntradas);
                PanelUsuario.Visible = false;

                if (txtAlmacenEntrada.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenEntrada.Text);
                    cmbAlmacen.Text = valores[0];
                }
                groupBox5.Enabled = true;
                txtDescripcionEntrada.Focus();
            }
        }

        private void txtDocumentoSalida_Leave(object sender, EventArgs e)
        {
            if (txtDocumentoSalida.Text != string.Empty)
            {
                c.ConsultaSTSeleccionado(txtTipoMovimientoSalida.Text, txtDocumentoSalida.Text, txtDescripcionSalida, cmbEstatusSalida, txtUltimoFolioSalida, rdbBloquearSiSalida, rdbBloquearNoSalida, txtAlmacenSalida, txtNotasSalida);
                PanelUsuario.Visible = false;

                if (txtAlmacenSalida.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenSalida.Text);
                    cmbAlmacenSalida.Text = valores[0];
                }
                groupBox8.Enabled = true;
                txtDescripcionSalida.Focus();
            }
        }

        private void txtDocumentoTraspaso_Leave(object sender, EventArgs e)
        {
            if (txtDocumentoTraspaso.Text != string.Empty)
            {
                c.ConsultaSTSeleccionado(txtTipoMovimientoTraspaso.Text, txtDocumentoTraspaso.Text, txtDescripcionTraspaso, cmbEstatusTraspaso, txtUltimoFolioTraspaso, rdbBloquearSiTraspaso, rdbBloquearNoTraspaso, txtAlmacenTraspaso, txtNotasTraspaso);
                PanelUsuario.Visible = false;

                if (txtAlmacenTraspaso.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenTraspaso.Text);
                    cmbAlmacenTraspado.Text = valores[0];
                }
                groupBox10.Enabled = true;
                txtDescripcionTraspaso.Focus();
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tgInventariable_CheckedChanged(object sender, EventArgs e)
        {
            if (tgInventariable.Checked == true)
            {
                label1.Text = "Si";
                rdbBloquearSiTraspaso.Checked = true;
                rdbBloquearNoTraspaso.Checked = false;
            }
            else
            {
                label1.Text = "No";
                rdbBloquearSiTraspaso.Checked = false;
                rdbBloquearNoTraspaso.Checked = true;
            }

        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                label3.Text = "Si";
                rdbBloquearSiSalida.Checked = true;
                rdbBloquearNoSalida.Checked = false;
            }
            else
            {
                label3.Text = "No";
                rdbBloquearSiSalida.Checked = false;
                rdbBloquearNoSalida.Checked = true;
            }
        }

        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch2.Checked == true)
            {
                label12.Text = "Si";
                rdbBloquearSiEntrada.Checked = true;
                rdbBloquearNoEntrada.Checked = false;
            }
            else
            {
                label12.Text = "No";
                rdbBloquearSiEntrada.Checked = false;
                rdbBloquearNoEntrada.Checked = true;
            }
        }

        private void guna2ToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch3.Checked == true)
            {
                label21.Text = "Si";
                rdbSiAfectaCosto.Checked = true;
                rdbNoAfectaCosto.Checked = false;
            }
            else
            {
                label21.Text = "No";
                rdbSiAfectaCosto.Checked = false;
                rdbNoAfectaCosto.Checked = true;
            }
            
        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
            guna2GradientPanel4.Size = new Size(22, 569);
            toolStrip1.Size = new Size(22, 569);
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
        
            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);


            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            guna2GradientPanel4.Size = new Size(80, 569);
            toolStrip1.Size = new Size(112, 569);
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

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {
                //   Limpiar();
              
                tabControl1.Enabled = true;
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                tabControl1.SelectedIndex = 0;
                tabControl1.Enabled = true;
                DesbloquearEntrada();
                DesbloquearSalida();
                DesbloquearTraspasos();

                CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2GradientPanel2.Visible = true;
                guna2GradientPanel5.Visible = true;
                guna2GradientPanel3.Visible = true;

                tabControl1.Enabled = true;
                if (PanelUsuario.Visible == true && tabControl1.SelectedIndex == 0)
                {
                    PanelUsuario.Enabled = true;
                    PanelUsuario.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearEntrada();
                }
                else
                {
                    PanelUsuario.Enabled = true;
                    PanelUsuario.Visible = true;
                    PanelUsuario.BringToFront();
                    BloquearEntrada();
                    
                }
                if (panel3.Visible == true && tabControl1.SelectedIndex == 1)
                {
                    panel3.Enabled = true;
                    panel3.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearSalida();
                }
                else
                {
                    panel3.Enabled = true;
                    panel3.Visible = false;
                    panel3.BringToFront();
                    BloquearSalida();
                    
                }
                if (panel5.Visible == true && tabControl1.SelectedIndex == 2)
                {
                    panel5.Enabled = true;
                    panel5.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearTraspasos();
                }
                else
                {
                    panel5.Enabled = true;
                    panel5.Visible = false;
                    panel5.BringToFront();
                    BloquearTraspasos();
                    
                }
                guna2GradientPanel4.Visible = false;
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                CambioTamañotoolstripPequeño();
            }
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = guna2DataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                string Documento = guna2DataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                c.ConsultaEntradaSeleccionado(Tipo, Documento, txtDescripcionEntrada, cmbEstatusEntrada, txtUltimoFolioEntrada, rdbBloquearSiEntrada, rdbBloquearNoEntrada, rdbSiAfectaCosto, rdbNoAfectaCosto, txtAlmacenEntrada, txtNotasEntradas);
                txtDocumentoEntrada.Text = Documento;
                PanelUsuario.Visible = false;

                if (txtAlmacenEntrada.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenEntrada.Text);
                    cmbAlmacen.Text = valores[0];
                }
                groupBox5.Enabled = true;
                guna2GradientPanel2.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void dataGridView3_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Tipo = dataGridView3.Rows[e.RowIndex].Cells["Tipo3"].Value.ToString();
                string Documento = dataGridView3.Rows[e.RowIndex].Cells["Documento3"].Value.ToString();
                c.ConsultaSTSeleccionado(Tipo, Documento, txtDescripcionTraspaso, cmbEstatusTraspaso, txtUltimoFolioTraspaso, rdbBloquearSiTraspaso, rdbBloquearNoTraspaso, txtAlmacenTraspaso, txtNotasTraspaso);
                txtDocumentoTraspaso.Text = Documento;
                panel5.Visible = false;

                if (txtAlmacenTraspaso.Text != string.Empty)
                {
                    string[] valores = c.InformacionAlmacen2(txtAlmacenTraspaso.Text);
                    cmbAlmacenTraspado.Text = valores[0];
                }
                groupBox10.Enabled = true;
                guna2GradientPanel5.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            guna2GradientPanel2.Visible = false;
        }

        void CambioTamañotoolstripPequeño()
        {

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);

            toolStrip1.Size = new Size(22, 569);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);


            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton6.Size = new Size(23, 79);
        }

        private void guna2GradientPanel6_MouseMove(object sender, MouseEventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1013, 83);
            guna2GradientPanel6.Size = new Size(86, 583);
            toolStrip2.Size = new Size(112, 569);
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton6.Size = new Size(85, 75);
            toolStripButton6.AutoSize = false;
        }

        private void toolStrip2_MouseLeave(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1076, 83);
            guna2GradientPanel6.Size = new Size(23, 569);

            toolStrip2.Size = new Size(23, 569);
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

          //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);

           // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);


            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton6.Size = new Size(23, 79);
        }

   

        private void toolStrip2_MouseEnter(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1013, 83);
            guna2GradientPanel6.Size = new Size(86, 583);

            toolStrip2.Size = new Size(112, 569);
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton6.Size = new Size(85, 75);
            toolStripButton6.AutoSize = false;
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
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);

                this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton6.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                tabControl1.Enabled = true;
                tabControl1.SelectedIndex = 0;
                tabControl1.Enabled = true;
                DesbloquearEntrada();
                DesbloquearSalida();
                DesbloquearTraspasos();
                // CambioTamañotoolstripPequeño();
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {
                guna2GradientPanel2.Visible = true;
                guna2GradientPanel5.Visible = true;
                guna2GradientPanel3.Visible = true;

                tabControl1.Enabled = true;
                if (PanelUsuario.Visible == true && tabControl1.SelectedIndex == 0)
                {
                    PanelUsuario.Enabled = true;
                    PanelUsuario.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearEntrada();
                }
                else
                {
                    PanelUsuario.Enabled = true;
                    PanelUsuario.Visible = true;
                    PanelUsuario.BringToFront();
                    BloquearEntrada();
                }
                if (panel3.Visible == true && tabControl1.SelectedIndex == 1)
                {
                    panel3.Enabled = true;
                    panel3.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearSalida();
                }
                else
                {
                    panel3.Enabled = true;
                    panel3.Visible = true;
                    panel3.BringToFront();
                    BloquearSalida();
                }
                if (panel5.Visible == true && tabControl1.SelectedIndex == 2)
                {
                    panel5.Enabled = true;
                    panel5.Visible = false;
                    guna2GradientPanel2.Visible = false;
                    guna2GradientPanel5.Visible = false;
                    guna2GradientPanel3.Visible = false;
                    // guna2GradientPanel2.SendToBack();
                    BloquearTraspasos();
                }
                else
                {
                    panel5.Enabled = true;
                    panel5.Visible = true;
                    panel5.BringToFront();
                    BloquearTraspasos();
                }


                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);

                this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton6.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel6.Location = new Point(1076, 83);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton4.Size = new Size(23, 79);

                this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton5.Size = new Size(23, 79);

                this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton6.Size = new Size(23, 79);
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;
            }
        }


        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1013, 83);
            guna2GradientPanel6.Size = new Size(112, 583);

          // guna2GradientPanel7.Location = new Point(1013, 83);
            guna2GradientPanel7.Size = new Size(112, 239);
            guna2GradientPanel7.BringToFront();
            toolStrip2.Size = new Size(112, 239);
            toolStrip2.Visible = true;
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;

            //
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton4.Size = new Size(85, 75);
            toolStripButton4.AutoSize = false;

            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Size = new Size(85, 75);
            toolStripButton5.AutoSize = false;

            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton6.Size = new Size(85, 75);
            toolStripButton6.AutoSize = false;

            toolStripButton4.Visible = true;
            toolStripButton5.Visible = true;
            toolStripButton6.Visible = true;

            guna2PictureBox2.Location = new Point(2,6);
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
            toolStripButton4.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton6.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

            //  toolStrip1.Size = new Size(22, 569);
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton4.Size = new Size(23, 79);

            // toolStrip1.Size = new Size(22, 569);
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton5.Size = new Size(23, 79);


            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton6.Size = new Size(23, 79);
        }
    }
    }
