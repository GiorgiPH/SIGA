using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;
using PuntoVentas;
using Condominios;

namespace PV
{
    public partial class RegistroEgreso : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;

        public RegistroEgreso()
        {
            InitializeComponent();
        }

        private void RegistroEgreso_Load(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCaja.Text = "1";

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            txtAlumno.Clear();

            GenerarRecibo.Matricula = string.Empty;
            NotasCargo.Matricula = string.Empty;
            OrdenCompra.Matricula = string.Empty;
            RegistroGastos.Matricula = string.Empty;
            RecepcionProductos2.Matricula = string.Empty;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarListaProveedores buscar = new BuscarListaProveedores();
            buscar.ShowDialog();
        }

        private void RegistroEgreso_Activated(object sender, EventArgs e)
        {
            txtMatricula.Text = matricula;
            txtAlumno.Text = nombre;
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                c.CargarReciboProveedor(dgvPagosPendientes, txtMatricula.Text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ArrayList ListaConcept = new ArrayList();
            ArrayList ListaConcept2 = new ArrayList();
            ArrayList ListaConcept3 = new ArrayList();

            int contador = 0;
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                {
                    contador++;
                }
            }

            if (contador == 0)
            {
                MessageBox.Show("Seleccione al menos un recibo para continuar");
                return;
            }


            if (MessageBox.Show("Si continua los saldos del recibo seran actualizados", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        c.ActualizarEgreso2(row.Cells["Tipo"].Value.ToString(), row.Cells["FolioDocumento"].Value.ToString(), Convert.ToDecimal( row.Cells["Recargos"].Value.ToString()), Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()), Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()));
                    }
                }

                DataGridViewCheckBoxCell oCell;

                foreach (DataGridViewRow row2 in dgvPagosPendientes.Rows)
                {
                    oCell = row2.Cells["Seleccionar"] as DataGridViewCheckBoxCell;
                    bool bChecked = (null != oCell && null != oCell.Value && true == (bool)oCell.Value);
                    if (true == bChecked)
                    {
                        ListaConcept.Add(row2.Cells["FolioDocumento"].Value.ToString());
                        ListaConcept2.Add(row2.Cells["Documento"].Value.ToString());
                        ListaConcept3.Add(row2.Cells["Tipo"].Value.ToString());
                    }
                }

                RegistrarCobroEgreso cobro = new RegistrarCobroEgreso(ListaConcept, ListaConcept2, ListaConcept3, txtMatricula.Text, txtAlumno.Text);
                cobro.ShowDialog();

                matricula = string.Empty;
                nombre = string.Empty;
                Limpiar();
                c.CargarReciboProveedor(dgvPagosPendientes, txtMatricula.Text);
            }
        }

        private void dgvPagosPendientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = true;
            dgvPagosPendientes.Rows[e.RowIndex].Cells[11].ReadOnly = true;
            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].ReadOnly = true;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
            {
                decimal Descuentos = 0.00M;
                decimal Recargos = 0.00M;
                decimal Importe = 0.00M;
                decimal Saldo = 0.00M;

                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
                {
                    Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
                }

                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value != null)
                {
                    Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value.ToString());
                    if (Recargos > Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value.ToString()))
                    {
                        MessageBox.Show("El recargo no puede ser mayor al Saldo del recibo");
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = 0.00;
                        Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value.ToString());
                    }
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = 0.00;
                }

                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value != null)
                {
                    Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                    if (Descuentos > Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value.ToString()))
                    {
                        MessageBox.Show("El descuento no puede ser mayor al Saldo del recibo");
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = 0.00;
                        Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                    }
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = 0.00;
                }


                Saldo = Importe + Recargos - Descuentos;

                string saldo = Saldo.ToString("N", formato);

                dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value = saldo;
            }
            decimal TotalRecargos = 0.00M;
            decimal TotalDescuentos = 0.00M;
            decimal Subtotal = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                {
                    if (row.Cells["Recargos"].Value != null)
                    {
                        TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString());
                        txtRecargos.Text = TotalRecargos.ToString("N", formato);
                    }
                    if (row.Cells["Descuento"].Value != null)
                    {
                        TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                        txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                    }
                    Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                    txtSubtotal.Text = Subtotal.ToString("N", formato);

                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                }

            }

            string recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value).ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = recargo;

            string descuento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value).ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = descuento;
        }

        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
            txtSubtotal.Text = "0.00";
            txtRecargos.Text = "0.00";
            txtDescuentos.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboProveedor(dgvPagosPendientes, txtMatricula.Text);
        }

        private void dgvPagosPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null)
            {
                //bool isCellChecked = (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value;

                //if (isCellChecked)
                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                {
                    //dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                    //dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    //dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = false;

                    decimal Descuentos = 0.00M;
                    decimal Recargos = 0.00M;
                    decimal Importe = 0.00M;
                    decimal Saldo = 0.00M;

                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
                    {
                        Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
                    }
                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value != null)
                    {
                        Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value.ToString());
                    }
                    else
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = 0.00;
                    }
                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value != null)
                    {
                        Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                    }
                    else
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = 0.00;
                    }


                    Saldo = Importe + Recargos - Descuentos;
                    string saldo = Saldo.ToString("N", formato);

                    dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value = saldo;
    

                    decimal TotalRecargos = 0.00M;
                    decimal TotalDescuentos = 0.00M;
                    decimal Subtotal = 0.00M;

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                        {
                            if (row.Cells["Recargos"].Value != null)
                            {
                                TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString());
                                txtRecargos.Text = TotalRecargos.ToString("N", formato);
                            }
                            if (row.Cells["Descuento"].Value != null)
                            {
                                TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                            }
                            Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                            txtSubtotal.Text = Subtotal.ToString("N", formato);

                            txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                        }

                    }
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = true;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = "0.00";
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].ReadOnly = true;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = "0.00";
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value = "0.00";

                    int cont = 0;
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                        {
                            cont++;
                        }

                        if (cont == 0)
                        {
                            txtRecargos.Text = "0.00";
                            txtDescuentos.Text = "0.00";
                            txtSubtotal.Text = "0.00";
                            txtTotal.Text = "0.00";
                        }
                    }

                    decimal TotalRecargos = 0.00M;
                    decimal TotalDescuentos = 0.00M;
                    decimal Subtotal = 0.00M;

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                        {
                            if (row.Cells["Recargos"].Value != null)
                            {
                                TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString());
                                txtRecargos.Text = TotalRecargos.ToString("N", formato);
                            }
                            if (row.Cells["Descuento"].Value != null)
                            {
                                TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                            }
                            Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                            txtSubtotal.Text = Subtotal.ToString("N", formato);

                            txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                        }

                    }
                }


                if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasRecargo")
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }
                else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MenosRecargo")
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = "0.00";

                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                    {
                        decimal Descuentos = 0.00M;
                        decimal Recargos = 0.00M;
                        decimal Importe = 0.00M;
                        decimal Saldo = 0.00M;

                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
                        {
                            Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value != null)
                        {
                            Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = 0.00;
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value != null)
                        {
                            Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = 0.00;
                        }


                        Saldo = Importe + Recargos - Descuentos;
                        string saldo = Saldo.ToString("N", formato);

                        dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value = saldo;
                    }
                    decimal TotalRecargos = 0.00M;
                    decimal TotalDescuentos = 0.00M;
                    decimal Subtotal = 0.00M;

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                        {
                            if (row.Cells["Recargos"].Value != null)
                            {
                                TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString());
                                txtRecargos.Text = TotalRecargos.ToString("N", formato);
                            }
                            if (row.Cells["Descuento"].Value != null)
                            {
                                TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                            }
                            Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                            txtSubtotal.Text = Subtotal.ToString("N", formato);

                            txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                        }

                    }
                }

                if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasDescuentos")
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }
                else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MenosDescuentos")
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = "0.00";
                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                    {
                        decimal Descuentos = 0.00M;
                        decimal Recargos = 0.00M;
                        decimal Importe = 0.00M;
                        decimal Saldo = 0.00M;

                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
                        {
                            Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value != null)
                        {
                            Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = 0.00;
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value != null)
                        {
                            Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value = 0.00;
                        }


                        Saldo = Importe + Recargos - Descuentos;

                        string saldo = Saldo.ToString("N", formato);

                        dgvPagosPendientes.Rows[e.RowIndex].Cells[14].Value = saldo;
                    }
                    decimal TotalRecargos = 0.00M;
                    decimal TotalDescuentos = 0.00M;
                    decimal Subtotal = 0.00M;

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                        {
                            if (row.Cells["Recargos"].Value != null)
                            {
                                TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString());
                                txtRecargos.Text = TotalRecargos.ToString("N", formato);
                            }
                            if (row.Cells["Descuento"].Value != null)
                            {
                                TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                            }
                            Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                            txtSubtotal.Text = Subtotal.ToString("N", formato);

                            txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                PagosEgresos cobro = new PagosEgresos( txtMatricula.Text, txtAlumno.Text);
                cobro.ShowDialog();
                Limpiar();
                c.CargarReciboProveedor(dgvPagosPendientes, txtMatricula.Text);
            }
            else
            {
                MessageBox.Show("Seleccione el Proveedor");
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
