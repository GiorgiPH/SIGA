using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class PartidaRequisicion : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        string CentroCosto = string.Empty;
        string Departamento = string.Empty;
        string SubDepartamento = string.Empty;

        public PartidaRequisicion(string Folio, string centroCosto, string departamento)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            txtCentroCosto.Text = centroCosto;
            txtDepartamento.Text = departamento;
        }

        private void PartidaRequisicion_Load(object sender, EventArgs e)
        {
            c.SeleccionarProducto2(cmbConcepto);
        //    c.ConsultaRequisicion5(TxtFolio.Text, txtPartida);
            txtCantidad.Text = "1";
            txtUnidad.Text = "Servicio";
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionRecibo(cmbConcepto.Text);
                txtClave.Text = valores[0];
                txtConcepto.Text = valores[1];
                txtUnidad.Text = valores[3];
                txtExistencia.Text = valores[5];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                MessageBox.Show("Registre el producto para continuar");
                return;
            }
            else if (txtFrecuencia.Text == "Automatico")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, termine el registro o cambie el concepto");
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRequisicion(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text);
                Limpiar();
            //    c.ConsultaRequisicion5(TxtFolio.Text, txtPartida);
            }
        }

        void Limpiar()
        {
            txtPartida.Clear();
            txtConcepto2.Clear();
            txtCantidad.Text = "1";
            cmbConcepto.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbConcepto.Text == string.Empty)
            {
                if (MessageBox.Show("¿Desea terminar el registro de partidas?", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRequisicion(TxtFolio.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtCantidad.Text == "0.00" || txtCantidad.Text == "0")
            {
                if (MessageBox.Show("Si termina la partida sin registrar una cantidad no se guardara", "Partida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Partida = Convert.ToInt32(txtPartida.Text) - 1;
                    if (Partida > 0)
                    {
                        c.ActualizarRequisicion(TxtFolio.Text, Partida.ToString());
                    }
                    this.Close();
                }
            }
            else if (txtFrecuencia.Text == "Automatico" && txtPartida.Text != "1")
            {
                MessageBox.Show("Los conceptos con cargos automaticos deben registrarse en recibos individuales, este recibo ya cuenta con una partida, seleccione otro concepto");
            }
            else
            {
                c.InsertarPartidaRequisicion(TxtFolio.Text, txtPartida.Text, txtClave.Text, txtConcepto2.Text, txtCantidad.Text, txtUnidad.Text);
                c.ActualizarRequisicion(TxtFolio.Text, txtPartida.Text);
                this.Close();
            }

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PartidasRequisicionVer partidasRequisicionVer = new PartidasRequisicionVer(TxtFolio.Text);
            partidasRequisicionVer.ShowDialog();
        }
    }
}
