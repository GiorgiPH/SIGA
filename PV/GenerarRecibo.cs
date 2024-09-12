using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;
using PuntoVentas.Clases.Login;
using ControlAcademico;
using PV;
using System.Drawing;

namespace Condominios
{
    public partial class GenerarRecibo : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        public static string Matricula = string.Empty;
        string Correo = string.Empty;
        string Correo2 = string.Empty;

        public GenerarRecibo()
        {
            InitializeComponent();
        }

        private void GenerarRecibo_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoDocumento(cmbDocumento);
            c.CargarRecibos(dataGridView1);
            cmbEstatus.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy/MM/dd");
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtElaborado.Text = DBLogin.usuario;
            Matricula = string.Empty;
            cmbDocumento.Text = txtDocumentoInsc.Text;
            txtDiasVence.Text = "0";
            int Dias = Convert.ToInt32(txtDiasVence.Text);
            DateTime FechaVence = Convert.ToDateTime(txtFecha.Text);
            FechaVence = FechaVence.AddDays(Dias);
            txtFechaVence.Text = FechaVence.ToString("yyyy/MM/dd");

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
                Matricula = string.Empty;
                OrdenCompra.Matricula = string.Empty;
                RegistroGastos.Matricula = string.Empty;
                RecepcionProductos.Matricula = string.Empty;
                ReporteReciboAutomatico.Opcion2 = 0;
                this.Close();
            }
        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                if (cmbDocumento.Text != string.Empty && txtFolio.Text== string.Empty)
                {
                    string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                    txtDocumento.Text = valores[0];
                    txtClave.Text = valores[1];

                    c.Consecutivo(txtConsecutivo, txtClave.Text);
                    groupBox2.Enabled = true;
                    c.DiasVence(txtDiasVence);

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

                        MessageBox.Show("Formato de dias vencimiento incorrecto");
                    }
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
            else if (txtMatricular.Text == string.Empty)
            {
                MessageBox.Show("Registre al propietario antes de continuar");
                return;
            }
            else if (cmbEstatus.Text != "Abierto")
            {
                MessageBox.Show("No es posible agregar partidas a un Recibo Bloqueado o Cancelado");
                return;
            }
            else if (cmbPropiedad.Text == string.Empty)
            {
                MessageBox.Show("Registre la propiedad para continuar");
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
                    c.InsertarRecibo(txtFolio, txtClave.Text, cmbEstatus.Text, txtFecha.Text, txtDiasVence.Text, txtFechaVence.Text, txtMatricular.Text, txtDivisa.Text, txtTipoCambio.Text, txtDescuento.Text, txtRecargo.Text, txtNotas.Text, txtElaborado.Text, cmbPropiedad.Text, txtConsecutivo.Text);
                }

                if (cmbDocumento.Text == txtDocumentoCol.Text)
                {
                    ReciboCol = txtReciboCol.Text;
                }

                button2.BackColor = Color.Gainsboro;

                Partidas partidas = new Partidas(txtFolio.Text, txtReciboInsc.Text, ReciboCol);
                partidas.ShowDialog();
            }

            cmbPropiedad.DroppedDown = false;

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;

            txtNotas.BackColor = Color.White;
            txtDiasVence.BackColor = Color.White;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gainsboro;
            BuscarListaAlumnos2 buscarListaAlumnos2 = new BuscarListaAlumnos2();
            buscarListaAlumnos2.ShowDialog();

            cmbPropiedad.DroppedDown = false;
            txtDiasVence.BackColor = Color.White;
            cmbDocumento.DroppedDown = false;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void txtMatricular_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricular.Text != string.Empty)
            {
                string[] valores = c.InformacionPropietarioRecibo(txtMatricular.Text);
                txtNombreAlumnno.Text = valores[1];
                c.SeleccionarPropiedad(cmbPropiedad, txtMatricular.Text);
            }
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
                MessageBox.Show("No es posible confirmar un Recibo Bloqueado o Cancelado");
                return;
            }
            else if (MessageBox.Show("Al confirmar el Recibo no podra realizar modificaciones, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Matricula = string.Empty;
                cmbEstatus.Text = "Bloqueado";
                c.ActualizarReciboEstatus(txtFolio.Text, cmbEstatus.Text, txtMatricular.Text);
                Limpiar();
                c.CargarRecibos(dataGridView1);
            }
        }

        void Limpiar()
        {
            txtFolio.Clear();
            cmbEstatus.Text = "Abierto";
            txtDiasVence.Text = "0";
            txtTotalConceptos.Text = "0";
            txtFechaVence.Clear();
            txtTotal.Clear();
            txtMatricular.Clear();
            txtNombreAlumnno.Clear();
            txtPartidas.Text = "0";
            txtSubtotal.Clear();
            txtDescuento.Clear();
            txtTotal.Clear();
            txtSaldo.Clear();
            txtAbono.Clear();
            txtFechaAbono.Clear();
            txtConsecutivo.Clear();
            txtRecargo.Clear();
            txtNotas.Clear();
            cmbPropiedad.Text = null;
            cmbDocumento.Text = null;
            cmbDocumento.Enabled = false;
            txtDiasVence.Enabled = false;
            cmbPropiedad.Enabled = false;
            txtNotas.Enabled = false;
            button3.Enabled = false;
            groupBox2.Enabled = false;
            button1.BackColor = Color.Gainsboro;
            cmbPropiedad.DroppedDown = false;
            txtCondominio.Clear();
            cmbDocumento.DroppedDown = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text == "Abierto" && txtFolio.Text!=string.Empty)
            {
                MessageBox.Show("No es posible limpiar un recibo abierto, confirme el recibo para continuar");
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
                MessageBox.Show("Seleccione un recibo para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme el recibo antes de continuar");
            }
            else
            {
                button6.BackColor = Color.Gainsboro;

                PartidasVer ver = new PartidasVer(txtFolio.Text);
                ver.ShowDialog();
            }

            cmbPropiedad.DroppedDown = false;

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;

            button7.BackColor = Color.Gainsboro;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el recibo");
                return;
            }
            else if (txtTotal.Text != txtSaldo.Text)
            {
                MessageBox.Show("No es posible cancelar un recibo con un pago total o parcial");
                return;
            }
            else if (cmbEstatus.Text != "Bloqueado")
            {
                MessageBox.Show("No es posible cancelar un recibo que no esta bloqueado");
                return;
            }
            else if (MessageBox.Show("El saldo de este recibo sera cancelado, ¿Desea continuar?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                c.ActualizarRecibo3(txtFolio.Text);
                cmbEstatus.Text = "Cancelado";
                c.ActualizarReciboEstatus(txtFolio.Text, cmbEstatus.Text, DBGenerarRecibo.MatriculaC);
                Limpiar();
                MessageBox.Show("Recibo Cancelado");
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                txtFiltroNombre.Clear();
                c.CargarRecibosFiltro(dataGridView1, txtFiltro.Text);
                cbFecha.Checked = false;
            }
            else
            {
                c.CargarRecibos(dataGridView1);
            }
        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroNombre.Text != string.Empty)
            {
                txtFiltro.Clear();
                c.CargarRecibosFiltroP(dataGridView1, txtFiltroNombre.Text);
                cbFecha.Checked = false;
            }
            else
            {
                c.CargarRecibos(dataGridView1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmbPropiedad.DroppedDown = false;

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;

            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else if (panel2.Visible == false)
            {
                panel2.Visible = true;
            }
        }

        private void GenerarRecibo_Activated(object sender, EventArgs e)
        {
            if (txtFolio.Text != "X")
            {
                txtMatricular.Text = Matricula;
          
                c.ReciboSaldos(txtFolio.Text, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtSaldo, txtPartidas);
                c.TotalConceptosGlobales(txtFolio.Text, txtTotalConceptos);

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

                MessageBox.Show("Formato de dias vencimiento incorrecto");
            }

            txtDiasVence.BackColor = Color.White;
            button3.BackColor = Color.Orange;

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Folio = dataGridView1.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = "X";
                c.ConsultaRecibo(Folio, txtClave, cmbEstatus, txtFecha, txtDiasVence, txtFechaVence, txtDivisa, txtTipoCambio, txtSubtotal, txtDescuento, txtRecargo, txtTotal, txtSaldo, txtPartidas, txtNotas, txtElaborado, txtFolio, txtConsecutivo);
                cmbDocumento.Enabled = false;
                txtDiasVence.Enabled = false;
                cmbPropiedad.Enabled = false;
                txtNotas.Enabled = false;
                button3.Enabled = false;
                button10.Enabled = true;
                button11.Enabled = true;
                groupBox2.Enabled = true;
                c.TotalConceptosGlobales(txtFolio.Text, txtTotalConceptos);
                c.ConsultaAbono(txtFolio.Text, txtAbono, txtFechaAbono);
                txtMatricular.Text = DBGenerarRecibo.MatriculaC;
                panel2.Visible = false;

                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                txtClave.Text = valores[1];

                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
                c.ConsultaRecibo2(Folio, cmbPropiedad);
            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un recibo para continuar");
            }
            else if (txtFolio.Text != string.Empty && cmbEstatus.Text == "Abierto")
            {
                MessageBox.Show("Confirme el recibo antes de continuar");
            }
            else
            {
                button7.BackColor = Color.Gainsboro;

                DocumentoConceptoGlobalVer documentoConceptoGlobalVer = new DocumentoConceptoGlobalVer(txtFolio.Text);
                documentoConceptoGlobalVer.ShowDialog();
            }

            cmbPropiedad.DroppedDown = false;

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;

        }

        private void Moneda(ref TextBox txt)
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

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSubtotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtDescuento);
        }

        private void txtRecargo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtRecargo);
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtTotal);
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSaldo);
        }

        private void txtAbono_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtAbono);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (cmbEstatus.Text != "Abierto" )
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                cmbPropiedad.Enabled = true;
                txtNotas.Enabled = true;
                button3.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else if (cmbEstatus.Text == "Abierto" && cmbDocumento.Text == string.Empty)
            {
                Limpiar();
                cmbDocumento.Enabled = true;
                txtDiasVence.Enabled = true;
                cmbPropiedad.Enabled = true;
                txtNotas.Enabled = true;
                button3.Enabled = true;
                cmbDocumento.Focus();
                cmbDocumento.DroppedDown = true;

            }
            else
            {
                MessageBox.Show("Confirme el recibo antes de continuar");
            }
        }

        private void cmbDocumento_Leave(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.Orange;
            txtDiasVence.Focus();
        }

        private void txtDiasVence_Enter(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.White;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gainsboro;
            cmbPropiedad.BackColor = Color.White;
            cmbPropiedad.DroppedDown = true;
        }

        private void cmbPropiedad_Leave(object sender, EventArgs e)
        {
            cmbPropiedad.BackColor = Color.Gainsboro;
            txtNotas.BackColor = Color.Orange;

        }

        private void txtNotas_Leave(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Orange;

        }

        private void txtNotas_TextChanged(object sender, EventArgs e)
        {
            txtNotas.BackColor = Color.White;
        }

        private void txtDiasVence_TextChanged(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.White;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Orange;

        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Orange;

        }

        private void button7_Leave(object sender, EventArgs e)
        {
            button7.BackColor = Color.Gainsboro;
        }

        private void cmbDocumento_Click(object sender, EventArgs e)
        {
            txtDiasVence.BackColor = Color.White;
            cmbPropiedad.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
        }

        private void txtDiasVence_Click(object sender, EventArgs e)
        {
            cmbPropiedad.DroppedDown = false;

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void cmbPropiedad_Click(object sender, EventArgs e)
        {

            cmbDocumento.DroppedDown = false;

            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            txtNotas.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void txtNotas_Click(object sender, EventArgs e)
        {

            cmbDocumento.DroppedDown = false;
            cmbPropiedad.DroppedDown = false;
            button3.BackColor = Color.Gainsboro;
            txtDiasVence.BackColor = Color.White;
            button2.BackColor = Color.Gainsboro;
            button6.BackColor = Color.Gainsboro;
            button7.BackColor = Color.Gainsboro;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReporteRecibo reporteRecibo = new ReporteRecibo(txtFolio.Text, txtMatricular.Text, txtCondominio.Text ,cmbPropiedad.Text);
            reporteRecibo.ShowDialog();
        }

        private void cmbPropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropiedad.Text!= string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbPropiedad.Text);
                txtCondominio.Text = valores[0];
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            c.ruta();
            c.Consultapropicorreo(txtMatricular.Text, txtcorreo, txtcorreo2);
            Correo = txtcorreo.Text;
            Correo2 = txtcorreo2.Text;
            c.CorreoContra();

            if (DBGenerarRecibo.Correo == string.Empty)
            {
                MessageBox.Show("No hay Correo definido para el envio de Recibos.");
               
            }
            else
            {
                ReporteReciboAutomatico reporteReciboAutomatico = new ReporteReciboAutomatico(txtFolio.Text, txtMatricular.Text, txtCondominio.Text, cmbPropiedad.Text, txtcorreo.Text, txtcorreo2.Text);
                reporteReciboAutomatico.ShowDialog();
            }

            if (ReporteReciboAutomatico.Opcion2 == 1)
            {
                MessageBox.Show("Error en el envio de correo revise en Parametros -> Datos Condominio o en la configuracion de seguridad de su correo en Acceso de aplicaciones.");
            }
            else
            {
                MessageBox.Show("Recibo Enviado.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFecha.Checked==true)
            {
                dtFiltroFecha.Enabled = true;
                txtFiltro.Clear();
                txtFiltroNombre.Clear();
            }
            else
            {
                dtFiltroFecha.Enabled = false;
                dtFiltroFecha.ResetText();
            }
        }

        private void dtFiltroFecha_ValueChanged(object sender, EventArgs e)
        {
            if (cbFecha.Checked==true)
            {

                c.CargarRecibosFiltroFecha(dataGridView1, dtFiltroFecha.Text);
            }
            else
            {
                c.CargarRecibos(dataGridView1);
            }
        }
    }
}
