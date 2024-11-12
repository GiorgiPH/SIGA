using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;

namespace PV
{
    public partial class RegistrarCobroEgreso : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        ArrayList Lista;
        ArrayList Lista2;
        ArrayList Lista3;

        public RegistrarCobroEgreso(ArrayList ListaConcep, ArrayList ListaConcep2, ArrayList ListaConcep3, string Matricula, string Alumno)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            Lista = ListaConcep;
            Lista2 = ListaConcep2;
            Lista3 = ListaConcep3;
        }

        private void RegistrarCobroEgreso_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet29.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet29.FormasPago);
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet29.FormasPago);

            c.CargarEgreso2(dgvPagosPendientes, txtMatricula.Text, Lista, Lista2, Lista3);
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = 0;
            }

            Saldo = Importe - Abono;

            string saldo = Saldo.ToString("N", formato);

            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = saldo;

            decimal TotalImporte = 0.00M;
            decimal TotalAbono = 0.00M;


            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                TotalImporte = TotalImporte + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                txtImporteTotal.Text = TotalImporte.ToString("N", formato);

                TotalAbono = TotalAbono + Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                txtTotalPagado.Text = TotalAbono.ToString("N", formato);

            }

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = abono.ToString("N", formato);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Finalizar pago?", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (cmbCuentaBancaria.Text!= string.Empty)
                {

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        try
                        {
                            if (row.Cells["FormaPago"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Registrar forma de pago para todos los pagos");
                                return;
                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Registrar forma de pago para todos los pagos");
                            return;
                        }

                    }
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Abono"].Value.ToString() == "0.00" || row.Cells["Abono"].Value.ToString() == string.Empty || Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) < 0)
                        {
                            MessageBox.Show("No es posible realizar un abono igual o menor a 0");
                            return;
                        }
                        else if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                        {
                            MessageBox.Show("El Abono no puede ser mayor al Importe");
                            return;
                        }
                    }

                    c.InsertarCobroGeneralEgreso(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        c.ActualizarEgreso2(row.Cells["Tipo"].Value.ToString(), row.Cells["FolioDocumento"].Value.ToString(), Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()), Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()), Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()));
                        c.InsertarEgreso(row.Cells["Tipo"].Value.ToString(), row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, dtpFecha.Text, txtObservaciones.Text, row.Cells["FormaPago"].Value.ToString(), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()), txtReferncia.Text, txtNumOperacion.Text, txtNumAutorizacion.Text, txtCuenta.Text, txtFolioGeneral.Text);
                        c.ActualizarSaldoProveedor(txtMatricula.Text, Convert.ToDecimal(row.Cells["Abono"].Value.ToString()));
                    }
                    if (MessageBox.Show("¿Imprimir Recibo?", "Egreso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ReciboEgreso reciboCobranza = new ReciboEgreso(txtFolioGeneral.Text, txtMatricula.Text);
                        reciboCobranza.ShowDialog();
                    }

                    RegistroEgreso.nombre = string.Empty;
                    RegistroEgreso.matricula = string.Empty;
                    button5.Enabled = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Seleccione la cuenta bancaria");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {
                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Registrar Forma de Pago");
                    return;
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }

            }
        }

        private void cmbCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta(cmbCuentaBancaria.Text);
                txtCuenta.Text = valores[0];
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
