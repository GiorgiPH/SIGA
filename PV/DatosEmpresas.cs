using System;
using System.Windows.Forms;
using System.Drawing;
using PuntoVentas.Clases.DatosEmpresa;
using PuntoVentas.Clases.Login;

namespace PuntoVentas
{
    public partial class DatosEmpresas : Form
    {
        DBDatosEmpresa c = new DBDatosEmpresa();
        DBLogin s = new DBLogin();

        public DatosEmpresas()
        {
            InitializeComponent();
        }

        void Limpiar()
        {
            txtRazonSocial.Clear();
            txtNombreComercial.Clear();
            txtRFC.Clear();
            txtTelefono1.Clear();
            txtTelefono2.Clear();
            txtTelefono3.Clear();
            txtCorreo.Clear();
            txtPaginaWeb.Clear();
            txtCalleNumero.Clear();
            txtColonia.Clear();
            txtMunicipio.Clear();
            txtContraseña.Clear();
            txtEstado.Clear();
            txtCodigoPostal.Clear();
            txtPais.Clear();
            txtReferencias.Clear();
            txtLeyendaTicket.Clear();
            Foto.Image = null;
            rdbRecargosSi.Checked = false;
            rdbRecargosNo.Checked = false;
            rdbSiDescuentos.Checked = false;
            rdbNoDescuentos.Checked = false;
            TXTrUTA.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text== string.Empty)
            {
                MessageBox.Show("Registre la razon social para continuar");
            }
            else if (txtNombreComercial.Text == string.Empty)
            {
                MessageBox.Show("Registre el nombre comercial para continuar");
            }
            else if (txtTelefono1.Text == string.Empty)
            {
                MessageBox.Show("Registre el telefono 1 para continuar");
            }
            else if (txtCorreo.Text != string.Empty && cmbServidorC.Text== string.Empty)
            {
                MessageBox.Show("Registre el servidor de correo para continuar");
            }
            else
            {
                string ReciboAuto = string.Empty;

                if (rdbConcepto.Checked == true)
                {
                    ReciboAuto = "Concepto";
                }
                else if (rdbEstructura.Checked == true)
                {
                    ReciboAuto = "Estructura";
                }
                else if (rdbProIndiviso.Checked == true)
                {
                    ReciboAuto = "ProIndiviso";
                }
                MessageBox.Show(c.RegistroEmpresa(txtRazonSocial.Text, txtNombreComercial.Text, txtRFC.Text, txtTelefono1.Text, txtTelefono2.Text, txtTelefono3.Text, txtCorreo.Text, cmbServidorC.Text, txtContraseña.Text, txtPaginaWeb.Text, txtCalleNumero.Text, txtColonia.Text, txtMunicipio.Text, txtEstado.Text, txtCodigoPostal.Text, txtPais.Text, txtReferencias.Text, rdbRecargosSi, rdbRecargosNo, rdbSiDescuentos, rdbNoDescuentos, ReciboAuto, txtClave.Text, txtLeyendaTicket.Text, Convert.ToDecimal(txtMontoMaximo.Text), txtDiasPlazo.Text, rdFechaCobranzaSi, rdFechaCobranzaNo, Convert.ToDecimal( txtMontoMaximoD.Text), txtConcepto.Text, txtClave2.Text, txtConcepto2.Text, TXTrUTA.Text, txtClave3.Text, txtConcepto3.Text, Foto));

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar los datos de el registro actual?", "Datos de la Empresa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                c.EliminarEmpresa(txtRazonSocial.Text);
                Limpiar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void DatosEmpresas_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string Hoy = DateTime.Today.ToString();
            //DateTime FechaSalida = Convert.ToDateTime(Hoy);

            //string HoraSalida = DateTime.Now.ToString("HH");
            //string MinutoSalida = DateTime.Now.ToString("mm");
            //string SegundoSalida = DateTime.Now.ToString("ss tt");
            //string Salida = HoraSalida + ":" + MinutoSalida + ":" + SegundoSalida;

            //s.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);

            //PuntoVentas.Opcion = 0;

            //Application.Exit();
        }

        private void DatosEmpresas_Load(object sender, EventArgs e)
        {

            c.SeleccionarConceptoDocumento(cmbDocumento);
            c.SeleccionarConceptoDocumento(cmbDocumentoExtra);
            c.SeleccionarConceptoRecibo(cmbConcepto);
            c.SeleccionarConceptoRecibo(cmbConceptoAnticipo);
            c.SeleccionarConceptoRecibo(CmbDocumentoAnticipo);
            c.SeleccionarConceptoReciboEX(cmbConceptoExtra);
            c.ConsultaUsuarioSeleccionado(txtRazonSocial, txtNombreComercial, txtRFC, txtTelefono1, txtTelefono2, txtTelefono3, txtCorreo, cmbServidorC, txtContraseña, txtPaginaWeb, txtCalleNumero, txtColonia, txtMunicipio, txtEstado, txtCodigoPostal, txtPais, txtReferencias, rdbRecargosSi, rdbRecargosNo, rdbSiDescuentos, rdbNoDescuentos, rdbConcepto, rdbEstructura, rdbProIndiviso, txtClave, txtLeyendaTicket, txtMontoMaximo, txtDiasPlazo, rdFechaCobranzaSi, rdFechaCobranzaNo, txtMontoMaximoD, txtConcepto, txtClave2, txtConcepto2, txtClave3, txtConcepto3, TXTrUTA, Foto);

            if (txtClave.Text != string.Empty)
            {
                string[] valores = c.InformacionDocumento2(txtClave.Text);
                txtDocumento.Text = valores[0];
                cmbDocumento.Text = txtClave.Text + " - " + txtDocumento.Text;
            }
            if (txtConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo2(txtConcepto.Text);
                txtConceptoDescrip.Text = valores[0];
                cmbConcepto.Text = txtConcepto.Text + " - " + txtConceptoDescrip.Text;
            }
  
            if (txtClave2.Text != string.Empty)
            {
                string[] valores = c.InformacionDocumento2(txtClave2.Text);
                txtDocumento2.Text = valores[0];
                cmbDocumentoExtra.Text = txtClave2.Text + " - " + txtDocumento2.Text;
            }
          
            if (txtConcepto2.Text != string.Empty)
            {
                /*string[] valores = c.InformacionReciboE2(txtConcepto2.Text);
                txtConceptoDescrip2.Text = valores[0];
                */
                c.InformacionReciboE2(txtConcepto2.Text, txtConceptoDescrip2);
                cmbConceptoExtra.Text = txtConcepto2.Text + " - " + txtConceptoDescrip2.Text;
            }
            if (txtClave3.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo2(txtClave3.Text);
                txtDocumento3.Text = valores[0];
                CmbDocumentoAnticipo.Text = txtClave3.Text + " - " + txtDocumento3.Text;
            }           
            if (txtConcepto3.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo2(txtConcepto3.Text);
                txtCOnceptoDescrip3.Text = valores[0];
                cmbConceptoAnticipo.Text = txtConcepto3.Text + " - " + txtCOnceptoDescrip3.Text;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocumento.Text != string.Empty)
            {
                string[] valores = c.InformacionDocumento(cmbDocumento.Text);
                txtClave.Text =valores[0];
            }
        }

        private void txtDiasPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void rdbRecargosSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRecargosSi.Checked== true)
            {
                txtMontoMaximo.Enabled = true;
            }
        }

        private void rdbRecargosNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRecargosNo.Checked==true)
            {
                txtMontoMaximo.Enabled = false;
            }
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

        private void txtMontoMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtMontoMaximo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMontoMaximo);
        }

        private void rdbSiDescuentos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSiDescuentos.Checked == true)
            {
                txtMontoMaximoD.Enabled = true;
            }
        }

        private void rdbNoDescuentos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNoDescuentos.Checked == true)
            {
                txtMontoMaximoD.Enabled = false;
            }
        }

        private void txtMontoMaximoD_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMontoMaximoD);
        }

        private void txtMontoMaximoD_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text!= string.Empty)
            {
                string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

        private void cmbDocumentoExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDocumentoExtra.Text != string.Empty)
            {
                string[] valores = c.InformacionDocumento(cmbDocumentoExtra.Text);
                txtClave2.Text = valores[0];
            }
        }

        private void cmbConceptoExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConceptoExtra.Text != string.Empty)
            {
                string[] valores = c.InformacionReciboE(cmbConceptoExtra.Text);
                txtConcepto2.Text = valores[0];
            }
        }

        private void CmbDocumentoAnticipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbDocumentoAnticipo.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo(CmbDocumentoAnticipo.Text);
                txtClave3.Text = valores[0];

                if (txtConcepto3.Text == txtClave3.Text)
                {
                    MessageBox.Show("No es posible utilizar el mismo concepto que Registrar Anticipo.");
                    CmbDocumentoAnticipo.Text = null;
                    txtClave3.Clear();
                }
            }
        }

        private void cmbConceptoAnticipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConceptoAnticipo.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo(cmbConceptoAnticipo.Text);
                txtConcepto3.Text = valores[0];

                if (txtConcepto3.Text == txtClave3.Text)
                {
                    MessageBox.Show("No es posible utilizar el mismo concepto que Aplicar Anticipo.");
                    cmbConceptoAnticipo.Text = null;
                    txtConcepto3.Clear();
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtClaveDivisa_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button13_Click(object sender, EventArgs e)
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }
    }
}
