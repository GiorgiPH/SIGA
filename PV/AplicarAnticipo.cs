using System;
using System.Windows.Forms;
using PV.Clases.Anticipo;
using ControlAcademico;
using System.Globalization;
using Condominios;
using System.Collections;

namespace PV
{
    public partial class AplicarAnticipo : Form
    {
        DBAnticipo c = new DBAnticipo();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;

        public AplicarAnticipo()
        {
            InitializeComponent();
        }

        private void AplicarAnticipo_Load(object sender, EventArgs e)
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
            GenerarRecibo.Matricula = string.Empty;
            RegistrarAnticipo.matricula = string.Empty;
            RegistrarAnticipo.nombre = string.Empty;
            registroIngresos.matricula = string.Empty;
            registroIngresos.nombre = string.Empty;
            txtAlumno.Clear();
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarListaAlumnos2 buscar = new BuscarListaAlumnos2();
            buscar.ShowDialog();
        }

        private void AplicarAnticipo_Activated(object sender, EventArgs e)
        {
            txtMatricula.Text = matricula;
            txtAlumno.Text = nombre;
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            if (txtMatricula.Text != string.Empty)
            {
                c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);

            }
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
            {
                decimal Subtotal = 0.00M;

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[5].Value = row.Cells["Saldo"].Value.ToString();
                        Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Saldo"].Value.ToString());
                    }
                }
            }
            else
            {

                decimal Subtotal = 0.00M;

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[5].Value = row.Cells["Saldo"].Value.ToString();
                    }

                }
            }
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Importe = string.Empty;
            string Anticipo = string.Empty;

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
                MessageBox.Show("Seleccione al menos un Anticipo para continuar");
                return;
            }

            if (contador > 1)
            {
                MessageBox.Show("Seleccione solo un Anticipo");
                return;
            }

            DataGridViewCheckBoxCell oCell;

            foreach (DataGridViewRow row2 in dgvPagosPendientes.Rows)
            {
                oCell = row2.Cells["Seleccionar"] as DataGridViewCheckBoxCell;
                bool bChecked = (null != oCell && null != oCell.Value && true == (bool)oCell.Value);
                if (true == bChecked)
                {
                    Importe = row2.Cells["Saldo"].Value.ToString();
                    Anticipo = row2.Cells["FolioDocumento"].Value.ToString();
                }
            }

            AplicarAnticipoSaldo cobro = new AplicarAnticipoSaldo(Importe, txtMatricula.Text, txtAlumno.Text, Anticipo);
            cobro.ShowDialog();

            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
        }

        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
        }

    }
}
