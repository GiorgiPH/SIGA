using Condominios;
using ControlAcademico;
using PuntoVentas;
using PV.Clases.Anticipo;
using System;
using System.Globalization;
using System.Windows.Forms;

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
            dtpFecha.MaxDate = DateTime.Today;
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
            BuscarCliente buscar = new BuscarCliente();
            buscar.ShowDialog();
            if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
            {
                txtMatricula.Text = BuscarCliente.Cliente;
                txtAlumno.Text = BuscarCliente.NombreCliente;
            }
        }

        private void AplicarAnticipo_Activated(object sender, EventArgs e)
        {
     

            
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricula.Text == string.Empty)
            {
                //btnBuscar.BackColor = Color.Red;
                button5.Enabled = false;
                button6.Enabled = false;
            }
            else
            {
                //btnBuscar.BackColor = Color.Gainsboro;
                button5.Enabled = true;
                button6.Enabled = true;
            }
            if (txtMatricula.Text != string.Empty)
            {
                dgvPagosPendientes.Rows.Clear();
                c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);

            }
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
            {
                decimal Subtotal = 0.00M;

                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = row.Cells["Saldo"].Value.ToString();
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
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value = row.Cells["Saldo"].Value.ToString();
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

            AplicarAnticipoSaldo cobro = new AplicarAnticipoSaldo(Importe, txtMatricula.Text, txtAlumno.Text, Anticipo, dtpFecha.Text);
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
            //btnBuscar.BackColor = Color.Red;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
        }
        private void AplicarAnticipo_Shown(object sender, EventArgs e)
        {
            foreach (Control c in guna2Panel1.Controls)
            {
                if (c is Label)
                {
                    Label l = (Label)c;
                    l.Text = l.Text.ToUpper();
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
