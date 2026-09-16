using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Anticipo;

namespace PV
{
    public partial class AplicarAnticipoProveedorSaldo2 : Form
    {
        DBAnticipo c = new DBAnticipo();
        string Anticipo = string.Empty;

        public AplicarAnticipoProveedorSaldo2(string importe, string Matricula, string Alumno, string anticipo)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            txtImporteTotal.Text = importe;
            Anticipo = anticipo;
        }

        private void AplicarAnticipoProveedorSaldo_Load(object sender, EventArgs e)
        {
            c.CargarEgreso2(dgvPagosPendientes, txtMatricula.Text);
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            // Abono ahora está en Cells[9]
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Saldo = 0.00M;

            // Importe ahora está en Cells[5]
            var valImporte = dgvPagosPendientes.Rows[e.RowIndex].Cells[5].Value;
            if (valImporte != null && !string.IsNullOrWhiteSpace(valImporte.ToString()))
            {
                Importe = Convert.ToDecimal(valImporte);
            }

            // Abono ahora está en Cells[9]
            var valAbono = dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value;
            if (valAbono != null && !string.IsNullOrWhiteSpace(valAbono.ToString()))
            {
                Abono = Convert.ToDecimal(valAbono);
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = "0.00";
            }

            if (Abono > 0)
            {
                Saldo = Importe - Abono;
            }

            string saldo = Saldo.ToString("N", formato);
            // Saldo ahora está en Cells[10]
            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = saldo;

            decimal TotalAbono = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.IsNewRow) continue;

                var celdaAbono = row.Cells[9].Value; // Cells[9] = Abono
                if (celdaAbono != null && !string.IsNullOrWhiteSpace(celdaAbono.ToString()))
                {
                    TotalAbono += Convert.ToDecimal(celdaAbono);
                }
            }

            txtTotalPagado.Text = TotalAbono.ToString("N", formato);

            // Formatear celda de Abono (Cells[9])
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = Abono.ToString("N", formato);
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {
                // Abono ahora está en Cells[9]
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Selected = true;
                dgvPagosPendientes.BeginEdit(true);
            }
            else
            {
                // Abono ahora está en Cells[9]
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = true;
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Selected = false;
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
                return;
            }

            int? idConceptoCobroPago = c.ObtenerConceptoAnticipoProveedor(Anticipo);

            if (MessageBox.Show("¿Finalizar aplicacion de Anticipo?", "Aplicar Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Cells[9] = Abono, Cells[10] = Saldo
                    decimal abonoFila = row.Cells[9].Value != null && !string.IsNullOrWhiteSpace(row.Cells[9].Value.ToString())
                        ? Convert.ToDecimal(row.Cells[9].Value)
                        : 0.00m;

                    decimal saldoFila = row.Cells[10].Value != null && !string.IsNullOrWhiteSpace(row.Cells[10].Value.ToString())
                        ? Convert.ToDecimal(row.Cells[10].Value)
                        : 0.00m;

                    if (abonoFila < 0)
                    {
                        MessageBox.Show("No es posible realizar un abono menor a 0");
                        return;
                    }
                    else if (saldoFila < 0)
                    {
                        MessageBox.Show("El Abono no puede ser mayor al Importe");
                        return;
                    }
                }

                c.InsertarCobroGeneralProveedor(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                decimal SaldoAnticipo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.IsNewRow) continue;

                    decimal abono = row.Cells[9].Value != null && !string.IsNullOrWhiteSpace(row.Cells[9].Value.ToString())
                        ? Convert.ToDecimal(row.Cells[9].Value)
                        : 0.00m;

                    if (abono > 0)
                    {
                        // Cells[0] = Tipo
                        // Cells[1] = FolioDocumento
                        // Cells[10] = Saldo
                        string tipo = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                        string folioDocumento = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;

                        decimal saldoDocumento = row.Cells[10].Value != null && !string.IsNullOrWhiteSpace(row.Cells[10].Value.ToString())
                            ? Convert.ToDecimal(row.Cells[10].Value)
                            : 0.00m;

                        c.ActualizarEgreso2(tipo, folioDocumento, saldoDocumento);

                        c.InsertarEgreso(tipo, folioDocumento, txtMatricula.Text, dtpFecha.Text, abono,
                            txtFolioGeneral.Text, Anticipo, SaldoAnticipo, 0.00m, idConceptoCobroPago);
                    }
                }

                c.ActualizarAnticipoProveedor(Anticipo, SaldoAnticipo);
                AplicarAnticipo.nombre = string.Empty;
                AplicarAnticipo.matricula = string.Empty;
                button5.Enabled = false;
                MessageBox.Show("Anticipo Aplicado");
                this.Close();
            }
        }

        private void txtNuevoSaldo_TextChanged(object sender, EventArgs e)
        {
            decimal NuevoSaldo = Convert.ToDecimal(txtImporteTotal.Text) - Convert.ToDecimal(txtTotalPagado.Text);
            txtNuevoSaldo.Text = NuevoSaldo.ToString();
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

        private void txtTotalPagado_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtNuevoSaldo);
        }
    }
}