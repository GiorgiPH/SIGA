using System;
using System.Windows.Forms;
using Condominios.Clases.ConceptosIngresos;
using PuntoVentas.Clases.Login;
using PuntoVentas;
using PV;


namespace Condominios
{
    public partial class ConceptosIngresos : Form
    {
        DBConceptosIngresos c = new DBConceptosIngresos();
        DBLogin s = new DBLogin();

        public ConceptosIngresos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        void Limpiar()
        {
            txtClaveDivisa.Clear();
            txtNombre.Clear();
            groupBox2.Enabled = false;
            groupBox6.Enabled = false;
            rdbDiario.Checked = false;
            rdbSiRecargo.Checked = false;
            rdbNoRecargo.Checked = false;
            rdbMensual.Checked = false;
            rdbAutomatico.Checked = false;
            rdbManual.Checked = false;
            cbSiDiario.Checked = false;
            cbMensual.Checked = false;
            cmbDiario.Text = null;
            cmbMensaul.Text = null;
            txtImporteDiario.Clear();
            txtImporteMensual.Clear();
            txtPorcentajeDiario.Clear();
            txtPorcentajeMensual.Clear();
            txtImporteConcepto.Text = "0.00";
            groupBox1.Enabled = false;
            groupBox7.Enabled = false;
        }

        private void ConceptosIngresos_Load(object sender, EventArgs e)
        {
            c.CargarConceptos(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre el nombre de la divisa para continuar.");
            }
            else if (rdbSiRecargo.Checked== false && rdbNoRecargo.Checked==false)
            {
                MessageBox.Show("Indique si el conceto genera recargos para continuar.");
            }
            else
            {
                string Recargo = string.Empty;
                string DiarioMensaual = string.Empty;
                string ManualAutomatico = string.Empty;
                string CalcularPor = string.Empty;

                if (rdbSiRecargo.Checked == true)
                {
                    Recargo = "Si";

                    if (rdbManual.Checked == true)
                    {
                        ManualAutomatico = "Manual";
                    }
                    else if (rdbAutomatico.Checked == true)
                    {
                        ManualAutomatico = "Automatico";
                    }
                    else
                    {
                        MessageBox.Show("seleccione si los recargos se generan de forma manual o automatica para continuar.");
                        return;
                    }

                    if (rdbDiario.Checked == true)
                    {
                        DiarioMensaual = "Diario";

                        if (cbSiDiario.Checked == true)
                        {
                            CalcularPor = "Si";
                        }
                        if (cmbDiario.Text== string.Empty)
                        {
                            MessageBox.Show("Seleccione importe o porcentaje para continuar");
                            return;
                        }
                        if (txtImporteDiario.Text== string.Empty && txtPorcentajeDiario.Text== string.Empty)
                        {
                            MessageBox.Show("Registre el importe o porcentaje para continuar");
                            return;
                        }
                        MessageBox.Show(c.RegistroConceptoIngreso(txtClaveDivisa.Text, txtNombre.Text, Recargo, DiarioMensaual, ManualAutomatico, CalcularPor, cmbDiario.Text, Convert.ToDecimal(txtImporteDiario.Text), txtPorcentajeDiario.Text, Convert.ToDecimal(txtImporteConcepto.Text)));
                    }
                    else if (rdbMensual.Checked == true)
                    {
                        DiarioMensaual = "Mensual";

                        if (cbMensual.Checked == true)
                        {
                            CalcularPor = "Si";
                        }
                        if (cmbMensaul.Text == string.Empty)
                        {
                            MessageBox.Show("Seleccione importe o porcentaje para continuar");
                            return;
                        }
                        if (txtImporteMensual.Text == string.Empty && txtPorcentajeMensual.Text == string.Empty)
                        {
                            MessageBox.Show("Registre el importe o porcentaje para continuar");
                            return;
                        }
                        MessageBox.Show(c.RegistroConceptoIngreso(txtClaveDivisa.Text, txtNombre.Text, Recargo, DiarioMensaual, ManualAutomatico, CalcularPor, cmbMensaul.Text, Convert.ToDecimal(txtImporteMensual.Text), txtPorcentajeMensual.Text, Convert.ToDecimal(txtImporteConcepto.Text)));
                    }
                    else
                    {
                        MessageBox.Show("Seleccione si los recargos se generan diario o mensual para continuar.");
                        return;
                    }

                    Limpiar();
                    c.CargarConceptos(dataGridView1);
                }
                else
                {
                    Recargo = "No";
                    MessageBox.Show(c.RegistroConceptoIngreso2(txtClaveDivisa.Text, txtNombre.Text, Recargo, Convert.ToDecimal(txtImporteConcepto.Text)));
                    Limpiar();
                    c.CargarConceptos(dataGridView1);
                }

                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
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

        private void ConceptosIngresos_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string Hoy = DateTime.Today.ToString();
            //DateTime FechaSalida = Convert.ToDateTime(Hoy);

            //string HoraSalida = DateTime.Now.ToString("HH");
            //string MinutoSalida = DateTime.Now.ToString("mm");
            //string SegundoSalida = DateTime.Now.ToString("ss tt");
            //string Salida = HoraSalida + ":" + MinutoSalida + ":" + SegundoSalida;

            //s.RegistroSalida(Login.UsuarioLogin, Login.FechaEntrada, Login.Entrada, FechaSalida, Salida);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                string ClaveDivisa = dataGridView1.Rows[e.RowIndex].Cells["ClaveDivisa"].Value.ToString();
                c.ConsultaConceptosSeleccionada(ClaveDivisa, txtNombre, txtImporteConcepto, rdbSiRecargo, rdbNoRecargo, rdbDiario, rdbMensual, rdbManual, rdbAutomatico, cbSiDiario, cbMensual, cmbDiario, cmbMensaul, txtImporteDiario, txtImporteMensual, txtPorcentajeDiario, txtPorcentajeMensual);
                txtClaveDivisa.Text = ClaveDivisa;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteConceptoIngresos reporteConceptoIngresos = new ReporteConceptoIngresos();
            reporteConceptoIngresos.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            PanelUsuario.Visible = false;
            groupBox1.Enabled = true;
            txtClaveDivisa.Focus();
        }

        private void rdbSiRecargo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSiRecargo.Checked == true)
            {
                groupBox2.Enabled = true;
                groupBox6.Enabled = true;
                groupBox5.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;
                groupBox6.Enabled = false;
                groupBox5.Enabled = false;
            }
        }

        private void rdbNoRecargo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNoRecargo.Checked == true)
            {
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
                rdbDiario.Checked = false;
                rdbMensual.Checked = false;
                rdbAutomatico.Checked = false;
                rdbManual.Checked = false;
                cbSiDiario.Checked = false;
                cbMensual.Checked = false;
                cmbDiario.Text = null;
                cmbMensaul.Text = null;
                txtImporteDiario.Clear();
                txtImporteMensual.Clear();
                txtPorcentajeDiario.Clear();
                txtPorcentajeMensual.Clear();
            }
        }

        private void cmbDiario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDiario.Text == "IMPORTE")
            {
                txtImporteDiario.Enabled = true;
                txtPorcentajeDiario.Enabled = false;
                txtPorcentajeDiario.Clear();
            }
            else if (cmbDiario.Text == "PORCENTAJE")
            {
                txtImporteDiario.Enabled = false;
                txtPorcentajeDiario.Enabled = true;
                txtImporteDiario.Text = "0.00";
            }
        }

        private void rdbDiario_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDiario.Checked == true)
            {
                groupBox3.Enabled = true;
                groupBox4.Enabled = false;
                cbSiDiario.Checked = true;
            }
        }

        private void rdbMensual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMensual.Checked == true)
            {
                groupBox3.Enabled = false;
                groupBox4.Enabled = true;
                cbMensual.Checked = true;
            }
        }

        private void cmbMensaul_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMensaul.Text == "IMPORTE")
            {
                txtImporteMensual.Enabled = true;
                txtPorcentajeMensual.Enabled = false;
                txtPorcentajeMensual.Clear();
            }
            else if (cmbMensaul.Text == "PORCENTAJE")
            {
                txtImporteMensual.Enabled = false;
                txtPorcentajeMensual.Enabled = true;
                txtImporteMensual.Text = "0.00";
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

        private void txtImporteDiario_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporteDiario);
        }

        private void txtImporteMensual_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporteMensual);
        }

        private void txtImporteDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtImporteMensual_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPorcentajeDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtPorcentajeMensual_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtImporteConcepto_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporteConcepto);
        }

        private void txtImporteConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtClaveDivisa_Leave(object sender, EventArgs e)
        {
            if (txtClaveDivisa.Text!= string.Empty)
            {
                groupBox7.Enabled = true;
                txtNombre.Focus();
            }
        }
    }
}
