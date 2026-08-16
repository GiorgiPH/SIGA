using System;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;
using PV.Clases.Egresos;

namespace PV
{
    public partial class PagosEgresos : Form
    {
        DBEgresos c = new DBEgresos();

        public PagosEgresos(string Matricula, string Alumno)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
        }

        private void PagosEgresos_Load(object sender, EventArgs e)
        {
            c.CargarPagosEgreso2(dgvPagosPendientes, txtMatricula.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "Cancelar")
            {
                
                if (e.RowIndex != -1)
                {
                    int Mes = dtpFecha.Value.Month;
                    int Año = dtpFecha.Value.Year;

                    DateTime Fecha = Convert.ToDateTime( dgvPagosPendientes.Rows[e.RowIndex].Cells["Fecha"].Value.ToString());

                    int Mes2 = Fecha.Month;
                    int Año2 = Fecha.Year;

                    if (Año < Año2)
                    {
                        MessageBox.Show("No es posible cancelar un egreso de un mes anterior al actual");
                    }
                    else if (Mes > Mes2)
                    {
                        MessageBox.Show("No es posible cancelar un egreso de un mes anterior al actual");
                    }
                    else if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString() == "0.00")
                    {
                        MessageBox.Show("El egreso ya fue cancelado");
                    }
                    else if (dgvPagosPendientes.Rows[e.RowIndex].Cells["FormaPago"].Value != null && dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString() != "0.00")
                    {
                        string Clave = dgvPagosPendientes.Rows[e.RowIndex].Cells["FolioDocumento"].Value.ToString();
                        string Tipo = dgvPagosPendientes.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                        string Abono = dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString();
                        string Proveedor = txtMatricula.Text;

                        CancelarEgreso cancelarEgreso = new CancelarEgreso(Clave, Tipo, Abono, Proveedor);
                        cancelarEgreso.ShowDialog();
                    }
                }
            }
        }

        private void PagosEgresos_Activated(object sender, EventArgs e)
        {
            c.CargarPagosEgreso2(dgvPagosPendientes, txtMatricula.Text);
        }
    }
}
