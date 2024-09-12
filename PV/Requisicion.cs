using System;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using PuntoVentas.Clases.Login;
using PV;
using System.Drawing;

namespace PV
{
    public partial class Requisicion : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();

        public Requisicion()
        {
            InitializeComponent();
        }

        private void Requisicion_Load(object sender, EventArgs e)
        {
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme el recibo antes de continuar");
                return;
            }
            else
            {
                this.Close();
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
                    if (txtFolio.Text == string.Empty)
                    {
                     //   c.ConsecutivoRequision(txtConsecutivo, txtClave.Text);
                    }
                    groupBox2.Enabled = true;
                    txtDiasVence.Focus();
                }

            }
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

                PartidaRequisicion partidas = new PartidaRequisicion(txtFolio.Text, txtCentroCosto.Text, txtDepartamento.Text);
                partidas.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
          
            button6.BackColor = Color.Gainsboro;
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtDiasVence.Text == string.Empty)
            {
                MessageBox.Show("Registre los dias de vencimiento para continuar");
                return;
            }
            else if (txtPartidas.Text == string.Empty)
            {
                MessageBox.Show("Registre las partidas para continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible confirmar una orden de compra Bloqueada o Cancelada");
                return;
            }
            else if (MessageBox.Show("Al confirmar la orden de compra no podra realizar modificaciones, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmbEstatus.Text = "Bloqueado";
                c.ActualizarRequisicionEstatus(txtFolio.Text, cmbEstatus.Text, txtCentroCosto.Text);
                Limpiar();
                c.CargarRequisicion(dataGridView1);
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
            groupBox2.Enabled = false;
            cmbCentroCosto.Text = null;
            cmbDepartamento.Text = null;
            cmbCentroCosto.Enabled = false;
            cmbDepartamento.Enabled = false;
            c.SeleccionarCentroCosto(cmbCentroCosto);
            c.SeleccionarCentroCostoDepartamento(cmbDepartamento, cmbCentroCosto.Text);
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione una orden de compra para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme la orden de compra antes de continuar");
            }
            else
            {
                PartidasRequisicionVer ver = new PartidasRequisicionVer(txtFolio.Text);
                ver.ShowDialog();
            }

            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
          
        }

        private void button9_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
        }

        private void Requisicion_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {

                c.RequisicionSaldos(txtFolio.Text, txtPartidas);
                //c.ReciboSaldosImpuesto(txtFolio.Text, txtRecargo);

                if (txtPartidas.Text == string.Empty)
                {
                    txtPartidas.Text = "0";
                }
                else if (txtPartidas.Text != "0" && cmbEstatus.Text == "Abierto")
                {
                    button1.BackColor = Color.Red;
                }
            }
        }

        private void txtDiasVence_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDiasVence.Text != string.Empty)
                {
                    int Dias = Convert.ToInt32(txtDiasVence.Text);
                    DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
                    FechaVence = FechaVence.AddDays(Dias);
                    txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Formato de dias requiere incorrecto");
            }

            txtDiasVence.BackColor = Color.White;
            cmbCentroCosto.DroppedDown = true;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                //c.ConsultaRequisicion(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence,  txtCentroCosto,  txtDepartamento, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                txtNotas.Enabled = false;
                //c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                panel2.Visible = false;

                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;

                c.SeleccionarCentroCosto2(cmbCentroCosto, txtCentroCosto.Text);
                c.SeleccionarCentroCostoDepartamento2(cmbDepartamento, txtDepartamento.Text);

                cmbCentroCosto.SelectedIndex = 0;
                cmbDepartamento.SelectedIndex = 0;

                groupBox2.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto")
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
                cmbCentroCosto.Enabled = true;
                cmbDepartamento.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else if (cmbEstatus.Text == "Abierto" && cmbDocumento.Text == string.Empty)
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                txtNotas.Enabled = true;
                cmbCentroCosto.Enabled = true;
                cmbDepartamento.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;
            }
            else
            {
                MessageBox.Show("Confirme la orden de compra antes de continuar");
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

        private void cmbCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCentroCosto.Text != string.Empty)
            {
                string[] valores = c.InformacionCentroCosto(cmbCentroCosto.Text);
                txtCentroCosto.Text = valores[0];
                c.SeleccionarCentroCostoDepartamento(cmbDepartamento, txtCentroCosto.Text);
            }
        }

        private void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartamento.Text != string.Empty)
            {
                string[] valores = c.InformacionCentroCostoDepartamento(cmbDepartamento.Text);
                txtDepartamento.Text = valores[0];
            }
          
        }

        private void cmbDocumento_Leave(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.Orange;
        }

        private void txtDiasVence_TextChanged(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.White;
        }

        private void cmbCentroCosto_Leave(object sender, EventArgs e)
        {
            cmbDepartamento.DroppedDown = true;
        }

        private void cmbDepartamento_Leave(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.Orange;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Orange;
        }

        private void cmbDocumento_Click(object sender, EventArgs e)
        {
            
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
        }

        private void txtNotas_TextChanged(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
        }

        private void txtNotas_Leave(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Orange;
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

        private void cmbDepartamento_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
           
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
        }

        private void txtNotas_Click(object sender, EventArgs e)
        {
            cmbDocumento.DroppedDown = false;
            cmbCentroCosto.DroppedDown = false;
            cmbDepartamento.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
           
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.BackColor = Color.Gainsboro;
        }
    }
}
