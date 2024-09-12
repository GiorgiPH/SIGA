using System;
using System.Windows.Forms;
using PV.Clases.FiltroMovimiento;


namespace PV
{
    public partial class FiltroKardex : Form
    {
        DBFiltroMovimientos c = new DBFiltroMovimientos();

        public FiltroKardex()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FiltroKardex_Load(object sender, EventArgs e)
        {
            c.SeleccionarTipoDocumento(cmbTipoMov);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Fecha = dtpFecha.Text;
            string Tipomov = cmbTipoMov.Text;
            string Documento = txtDocumento.Text;

            ReporteKardex diariomov = new ReporteKardex(Fecha, Tipomov, Documento);
            diariomov.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.SeleccionarTipoDocumento(cmbTipoMov);
            c.SeleccionarDocumento(txtDocumento, cmbTipoMov.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c.SeleccionarDocumento(txtDocumento, cmbTipoMov.Text);
        }

        private void cmbTipoMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoMov.Text != string.Empty)
            {
                c.SeleccionarDocumento(txtDocumento, cmbTipoMov.Text);
            }
            
        }
    }
}
