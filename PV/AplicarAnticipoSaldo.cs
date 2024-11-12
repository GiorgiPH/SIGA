using PV.Clases.Anticipo;
using PV.Clases.Remision;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    public partial class AplicarAnticipoSaldo : Form
    {
        DBAnticipo c = new DBAnticipo();
        DBRemiision r = new DBRemiision();
        string Anticipo = string.Empty;

        public AplicarAnticipoSaldo(string importe, string Matricula, string Alumno, string anticipo, string Fecha)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            txtImporteTotal.Text = importe;
            Anticipo = anticipo;
            dtpFecha.Value = Convert.ToDateTime(Fecha);
            dtpFecha.Enabled = false;
        }

        private void AplicarAnticipoSaldo_Load(object sender, EventArgs e)
        {
            c.CargarReciboAlumno2(dgvPagosPendientes, txtMatricula.Text);
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;
            decimal Descuento = 0.00M;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value.ToString());
            }

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = 0;
            }

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value != null)
            {
                Descuento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = 0;
            }

            if (Abono > 0)
            {
                Saldo = Importe - Abono - Descuento;
            }

            string saldo = Saldo.ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = saldo.ToString();

            decimal TotalImporte = 0.00M;
            decimal TotalAbono = 0.00M;


            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                //TotalImporte = TotalImporte + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                //txtImporteTotal.Text = TotalImporte.ToString("N", formato);

                TotalAbono = TotalAbono + Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                txtTotalPagado.Text = TotalAbono.ToString("N", formato);

            }

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = abono.ToString("N", formato);
            decimal desc = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = desc.ToString("N", formato);
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {

                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Selected = false;
                dgvPagosPendientes.BeginEdit(false);
            }

            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasDescuento")
            {

                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Selected = false;
                dgvPagosPendientes.BeginEdit(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtImporteTotal.Text) < Convert.ToDecimal(txtTotalPagado.Text))
            {
                MessageBox.Show("No es posible aplicar un importe mayor al del anticipo");
            }

            else
            {
                if (MessageBox.Show("¿Finalizar aplicacion de Anticipo?", "Aplicar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("La aplicacion de un anticipo una vez realizado no puede cancelarse", "Aplicar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) < 0)
                            {
                                MessageBox.Show("No es posible realizar un abono menor a 0");
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
                            if (Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) > 0)
                            {
                                r.ActualizarRemision(row.Cells["FolioDocumento"].Value.ToString(), 0.00m, Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()));
                                c.InsertarCobro(row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, dtpFecha.Text, Convert.ToDecimal(row.Cells["Abono"].Value.ToString()), txtFolioGeneral.Text, Anticipo, Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()), Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()));

                                if (Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()) > 0)
                                {
                                    //ReciboNotaCredito reciboNotaCredito = new ReciboNotaCredito(row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, "1");
                                    //reciboNotaCredito.ShowDialog();
                                }

                            }
                        }
                        decimal SaldoAnticipo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);
                        c.ActualizarAnticipo(Anticipo, SaldoAnticipo);
                        AplicarAnticipo.nombre = string.Empty;
                        AplicarAnticipo.matricula = string.Empty;
                        button5.Enabled = false;
                        MessageBox.Show("Anticipo Aplicado");
                        ReporteReciboAnticipoAplicado reporteReciboAnticipoAplicado = new ReporteReciboAnticipoAplicado(Anticipo, txtFolioGeneral.Text);
                        reporteReciboAnticipoAplicado.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void txtTotalPagado_TextChanged(object sender, EventArgs e)
        {
            decimal NuevoSaldo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);
            txtNuevoSaldo.Text = NuevoSaldo.ToString();
            if (txtTotalPagado.Text != "0.00" || txtTotalPagado.Text != "0")
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void txtNuevoSaldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtNuevoSaldo);
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

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
