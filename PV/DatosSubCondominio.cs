using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.ConsultaCondominio;


namespace PV
{
    public partial class DatosSubCondominio : Form
    {
        DBConsultaCondominio c = new DBConsultaCondominio();

        public DatosSubCondominio(string clave)
        {
            InitializeComponent();
            txtClave.Text = clave;
        }

        private void DatosSubCondominio_Load(object sender, EventArgs e)
        {
            c.ConsultaCondominioSeleccionado(txtClave.Text, txtDescripcion, txtCaracteristicas, txtobservaciones);
            c.ConsultaPropietariosSeleccionado(txtClave.Text, txtClaveCliente, txtRazonSocial, cmbTipoCliente, txtRFC, txtCalle, txtNoExterior, txtNoInterior, txtColonia, txtMunicipio, txtCodigoPostal, txtCiudad, txtPais, txtReferencia, cmbMetodoPago, cmbFormaPago, cmbCFDI, txtBancoPago, txtDomicilioFiscal, txtRegimenFiscal, txtExportacion);
            c.CargarCondominios(txtClaveCliente.Text, dataGridView3);
            c.CargarReciboAlumno(dgvPagosPendientes, txtClaveCliente.Text, txtClave.Text);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtobservaciones.Text != string.Empty)
            {
               MessageBox.Show(c.RegistroFormaPago(txtClave.Text, txtobservaciones.Text));
            }
        }
    }
}
