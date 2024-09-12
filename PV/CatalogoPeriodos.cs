using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Presupuesto;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class CatalogoPeriodos : Form
    {
        DBPresupuesto c = new DBPresupuesto();

        public CatalogoPeriodos()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nuevo Periodo");
            T.SetToolTip(button8, "Consultar Periodo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty)
            {
                Limpiar();
                groupBox1.Enabled = true;
            }
            else
            {
                Limpiar();
                groupBox1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Registre el mes para continuar.");
            }
            else
            {
                MessageBox.Show(c.RegistroPeriodo(txtClaveFormasPago.Text, cmbEstatus.Text, dtFechaInicio.Text, dtFechaFinal.Text));
                Limpiar2();
                //GenerarNoCategoria();
                c.CargarPeriodo(dataGridView1);
            }
        }

        void Limpiar()
        {
            txtClaveFormasPago.Clear();
            dtFechaInicio.ResetText();
            cmbEstatus.Text = null;
            dtFechaFinal.ResetText(); 
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
            cmbEstatus.Enabled = true;
        }

        void Limpiar2()
        {
            dtFechaInicio.ResetText();
            cmbEstatus.Text = null;
            dtFechaFinal.ResetText();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Periodo"].Value.ToString();
                string Mes = dataGridView1.Rows[e.RowIndex].Cells["Mes"].Value.ToString();
                c.ConsultaPeriodoSeleccionado(Clave, Mes, cmbEstatus, dtFechaInicio, dtFechaFinal);
                txtClaveFormasPago.Text = Clave;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                cmbEstatus.Enabled = false;
            }
            else
            {
                return;
            }
        }

        private void txtClaveFormasPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void CatalogoPeriodos_Load(object sender, EventArgs e)
        {
            c.CargarPeriodo(dataGridView1);
        }

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            dtFechaFinal.Text = dtFechaInicio.Text;
            DateTime date = dtFechaFinal.Value;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtFechaFinal.Value = oUltimoDiaDelMes;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveFormasPago.Text == string.Empty && cmbEstatus.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveFormasPago.Text));
                        c.CargarPeriodo(dataGridView1);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
