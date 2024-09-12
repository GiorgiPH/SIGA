using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;
using PV;

namespace ControlAcademico
{
    public partial class RegistrarCobro : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        ArrayList Lista;

        public RegistrarCobro(ArrayList ListaConcep, string Matricula, string Alumno)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            Lista = ListaConcep;
        }

        private void RegistrarCobro_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlAcademicoDataSet14.FormasPago' Puede moverla o quitarla según sea necesario.

            c.CargarReciboAlumno2(dgvPagosPendientes, txtMatricula.Text, Lista);
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = 0;
            }

            Saldo = Importe - Abono;

            string saldo = Saldo.ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = saldo.ToString();

            decimal TotalImporte = 0.00M;
            decimal TotalAbono = 0.00M;


            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                TotalImporte = TotalImporte + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                txtImporteTotal.Text = TotalImporte.ToString("N", formato);

                TotalAbono = TotalAbono + Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                txtTotalPagado.Text = TotalAbono.ToString("N", formato);

            }

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = abono.ToString("N", formato);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text!=string.Empty)
            {

                if (MessageBox.Show("¿Finalizar cobro?", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cmbCuentaBancaria.Text == string.Empty)
                    {
                        MessageBox.Show("Seleccione la cuenta bancaria");
                        return;
                    }

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

                    c.InsertarCobroGeneral(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {

                        c.ActualizarRecibo2(row.Cells["FolioDocumento"].Value.ToString(), Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()), Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()), Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()));
                        c.InsertarCobro(row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, dtpFecha.Text, txtObservaciones.Text, row.Cells["FormaPago"].Value.ToString(), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()), txtReferncia.Text, txtNumOperacion.Text, txtNumAutorizacion.Text, txtCuenta.Text, txtFolioGeneral.Text);
                    }

                    if (MessageBox.Show("¿Imprimir Recibo?", "Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ReciboCobranza reciboCobranza = new ReciboCobranza(txtFolioGeneral.Text, txtMatricula.Text);
                        reciboCobranza.ShowDialog();
                    }
                    registroIngresos.nombre = string.Empty;
                    registroIngresos.matricula = string.Empty;
                    button5.Enabled = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione la cuenta bancaria para continuar");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

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
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Selected = true;
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
    }
}
