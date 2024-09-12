using System;
using System.Windows.Forms;
using PV.Clases.ActivoFijo;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class ResguardoActivoFijo : Form
    {
        DBActivoFijo c = new DBActivoFijo();

        public ResguardoActivoFijo()
        {
            InitializeComponent();
        }

        private void ResguardoActivoFijo_Load(object sender, EventArgs e)
        {
            c.SeleccionarActivoFijo(cmbActivoFijo);
            c.SeleccionarPersonal(cmbEmpleado);
            c.SeleccionarCentroCosto(cmbCentroCosto);
            dtReguardo.CustomFormat = " ";
            dtDevolucion.CustomFormat = " ";
            txtElaborado.Text = DBLogin.usuario;
            c.CargarResguardoActivoFijo(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbResguardo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbResguardo.Checked == true)
            {
                dtReguardo.Enabled = true;
                dtReguardo.CustomFormat = "yyyy/MM/dd";
            }
            else
            {
                dtReguardo.Enabled = false;
                dtReguardo.CustomFormat = " ";
            }
            
        }

        private void cbDevolucion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDevolucion.Checked == true)
            {
                dtDevolucion.Enabled = true;
                dtDevolucion.CustomFormat = "yyyy/MM/dd";
                dtDevolucion.Text = dtReguardo.Text;
            }
            else
            {
                dtDevolucion.Enabled = false;
                dtDevolucion.CustomFormat = " ";
            }
           
        }

        void GenerarNoActivo()
        {
            DBActivoFijo.Folio = 0;
            c.ClaveSiguienteResguardo();
            if (DBActivoFijo.Folio == 0)
            {
                DBActivoFijo.Folio = 1;
                txtFolio.Text = Convert.ToString(DBActivoFijo.Folio);

            }
            else
            {
                DBActivoFijo.Folio = DBActivoFijo.Folio + 1;
                txtFolio.Text = Convert.ToString(DBActivoFijo.Folio);

            }
        }

        void GenerarPartida()
        {
            DBActivoFijo.Partida = 0;
            c.PartidaSiguiente(cmbActivoFijo.Text);
            if (DBActivoFijo.Partida == 0)
            {
                DBActivoFijo.Partida = 1;
                txtPartida.Text = Convert.ToString(DBActivoFijo.Partida);

            }
            else
            {
                DBActivoFijo.Partida = DBActivoFijo.Partida + 1;
                txtPartida.Text = Convert.ToString(DBActivoFijo.Partida);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                GenerarNoActivo();
                cmbEstatus.SelectedIndex = 0;
                groupBox1.Enabled = true;
               
            }
            else
            {
                Limpiar();
                GenerarNoActivo();
                cmbEstatus.SelectedIndex = 0;
                groupBox1.Enabled = true;
            }
        }

        void Limpiar()
        {
            txtFolio.Clear();
            cmbEstatus.Text = null;
            cmbActivoFijo.Text = null;
            txtArticulo.Clear();
            txtPartida.Clear();
            cmbEmpleado.Text = null;
            cmbCentroCosto.Text = null;
            cbResguardo.Checked = false;
            dtReguardo.CustomFormat = " ";
            cbDevolucion.Checked = false;
            dtDevolucion.CustomFormat = " ";
            txtFolio.Clear();
            txtPartida.Clear();
            txtUbicacion.Clear();
            txtNotas.Clear();
            txtNotas.Enabled= false;
            txtDevolucionRegistrado.Clear();
            cbDevolucion.Enabled = false;
            groupBox1.Enabled = false;
            c.SeleccionarActivoFijo(cmbActivoFijo);
        }

        private void cmbActivoFijo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbActivoFijo.Text != string.Empty)
            {
                string[] valores = c.InformacionActivoFijo(cmbActivoFijo.Text);
                cmbEstatus.Text = valores[0];
                txtArticulo.Text = valores[1];
               
            }
            if (cbResguardo.Checked != true && cbDevolucion.Checked != true)
            {
                GenerarPartida();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Generar nuevo registro para continuar");
            }
            else if (cmbActivoFijo.Text == string.Empty)
            {
                MessageBox.Show("Registrar Activo fijo para continuar");
            }
            else if (cmbEmpleado.Text == string.Empty && cmbCentroCosto.Text == string.Empty)
            {
                MessageBox.Show("Registrar un empleado o centro de costo");
            }
            else if (cbResguardo.Checked == false && cbDevolucion.Checked == false)
            {
                MessageBox.Show("Registrar resguardo o devolucion para continuar");
            }
            else
            {
                MessageBox.Show(c.RegistroResguardoActivoFijo(txtFolio.Text, cmbActivoFijo.Text, txtPartida.Text, cmbEmpleado.Text, cmbCentroCosto.Text, txtUbicacion.Text, cbResguardo, dtReguardo.Text, cbDevolucion, dtDevolucion.Text, txtElaborado.Text, txtNotas.Text, txtDevolucionRegistrado.Text));
                Limpiar();
                c.CargarResguardoActivoFijo(dataGridView2);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Limpiar();
                groupBox1.Enabled = true;
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Folio"].Value.ToString();
                txtFolio.Text = Clave;
                c.SeleccionarActivoFijo2(cmbActivoFijo, Clave);
                c.ConsultaResguardoActivoSeleccionada(Clave, cmbActivoFijo, txtPartida, cmbEmpleado, cmbCentroCosto, txtUbicacion, cbResguardo, dtReguardo, cbDevolucion, dtDevolucion, txtElaborado, txtNotas, txtDevolucionRegistrado);

                //c.ConsultaResguardoActivoSeleccionada(Clave, cmbActivoFijo, txtPartida, cmbEmpleado, cmbCentroCosto, txtUbicacion, cbResguardo, dtReguardo, cbDevolucion, dtDevolucion, txtElaborado, txtNotas, txtDevolucionRegistrado);
                PanelUsuario.Visible = false;
                
            }
            else
            {
                return;
            }
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

        private void button9_Click(object sender, EventArgs e)
        {
            if (cbResguardo.Checked!= false && txtFolio.Text != string.Empty)
            {
                ReporteResguardoActivoFijo reporteResguardoActivoFijo = new ReporteResguardoActivoFijo(txtFolio.Text);
                reporteResguardoActivoFijo.ShowDialog();
            }
            else if (txtFolio.Text==string.Empty)
            {
                MessageBox.Show("Seleccione un registro");
            }
            else
            {
                MessageBox.Show("El registro no tiene resguardo");
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cbResguardo.Checked==true)
            {
                cbDevolucion.Enabled = true;
                cbDevolucion.Checked = true;
                txtNotas.Enabled = true;
                txtDevolucionRegistrado.Text= DBLogin.usuario;
            }
            else
            {
                MessageBox.Show("No existe resguardo para este activo");
            }
        }

        private void dtDevolucion_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtDevolucion.Text) < Convert.ToDateTime(dtReguardo.Text))
            {
                MessageBox.Show("La Fecha de devolucion no puede ser menor a la de resguardo");
                dtDevolucion.Text = dtReguardo.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text != string.Empty && cbDevolucion.Checked==true)
            {
                ReporteResguardoActivoFijoDevolucion reporteResguardoActivoFijoDevolucion = new ReporteResguardoActivoFijoDevolucion(txtFolio.Text);
                reporteResguardoActivoFijoDevolucion.ShowDialog();
            }
            else if (txtFolio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro");
            }
            else
            {
                MessageBox.Show("El registro no tiene devolucion registrada");
            }
           
        }
    }
}
