using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.RegistrarIngresos;

namespace PV
{
    public partial class PagosRecibos : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();

        public PagosRecibos(string Matricula, string Alumno)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
        }

        private void PagosRecibos_Load(object sender, EventArgs e)
        {
            c.CargarPagosRecibo2(dgvPagosPendientes, txtMatricula.Text);
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

                    DateTime Fecha = Convert.ToDateTime(dgvPagosPendientes.Rows[e.RowIndex].Cells["Fecha"].Value.ToString());

                    int Mes2 = Fecha.Month;
                    int Año2 = Fecha.Year;

                    if (Año < Año2)
                    {
                        MessageBox.Show("No es posible cancelar un ingreso de un año anterior al actual");
                    }
                    else if (Mes > Mes2)
                    {
                        MessageBox.Show("No es posible cancelar un ingreso de un mes anterior al actual");
                    }
                    else if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString() == "0.00")
                    {
                        MessageBox.Show("El ingreso ya fue cancelado");
                    }
                    else if (dgvPagosPendientes.Rows[e.RowIndex].Cells["FormaPago"].Value != null && dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString() != "0.00")
                    {
                        string Clave = dgvPagosPendientes.Rows[e.RowIndex].Cells["FolioDocumento"].Value.ToString();
                        string Abono = dgvPagosPendientes.Rows[e.RowIndex].Cells["Abono"].Value.ToString();
                        string Propi = txtMatricula.Text;

                        CancelarIngreso cancelarEgreso = new CancelarIngreso(Clave, Abono, Propi);
                        cancelarEgreso.ShowDialog();
                    }
                }
            }
        }

        private void PagosRecibos_Activated(object sender, EventArgs e)
        {
            c.CargarPagosRecibo2(dgvPagosPendientes, txtMatricula.Text);
        }
    }
}
