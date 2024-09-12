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
    public partial class ConceptosPresupuesto : Form
    {
        DBPresupuesto c = new DBPresupuesto();

        public ConceptosPresupuesto()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion para continuar.");
            }
            else if (cmbClase.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase para continuar.");
            }
            else
            {
                if (cmbClase.Text != "Egreso")
                {
                    c.Eliminarproductoconcepto2(txtClave.Text);
                }
                MessageBox.Show(c.RegistroConcepto(txtClave.Text, txtNombre.Text, cmbClase.Text, txtTipo.Text));
                Limpiar();
                //GenerarNoCategoria();
                c.CargarConcepto(dataGridView1);
                c.SeleccionarConceptos(cmbTipo);
            }
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtTipo.Clear();
            txtNombre.Clear();
            cmbClase.Text = null;
            cmbTipo.Text = null;
            groupBox1.Enabled = false;
            PanelUsuario.Visible = false;
            txtClave.Enabled = true;
            c.SeleccionarConceptos(cmbTipo);
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
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                c.ConsultaConceptoSeleccionado(Clave, txtNombre, cmbClase, txtTipo);
                txtClave.Text = Clave;
                c.SeleccionarConceptos2(cmbTipo, txtTipo.Text);
                if (txtTipo.Text != string.Empty)
                {
                    string[] valores = c.InformacionConcepto2(txtTipo.Text);
                    cmbTipo.Text = valores[0];
                }
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                txtClave.Enabled = false;
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
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

        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClase.Text == "Ingreso" || cmbClase.Text == string.Empty)
            {
                cmbTipo.Visible = true;
                btVincular.Visible = false;
            }
            else
            {
                cmbTipo.Visible = false;
                cmbTipo.Text = null;
                txtTipo.Clear();
                btVincular.Visible = true;

            }
        }

        private void ConceptosPresupuesto_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptos(cmbTipo);
            c.CargarConcepto(dataGridView1);
        }

        private void btVincular_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion para continuar.");
            }
            else if (cmbClase.Text == string.Empty)
            {
                MessageBox.Show("Registre la clase para continuar.");
            }
            else
            {
                if (cmbClase.Text != "Egreso")
                {
                    c.Eliminarproductoconcepto2(txtClave.Text);
                }
                c.RegistroConcepto2(txtClave.Text, txtNombre.Text, cmbClase.Text, txtTipo.Text);
                AgregarConceptoPresProdc agregarConceptoPresProdc = new AgregarConceptoPresProdc(txtClave.Text);
                agregarConceptoPresProdc.ShowDialog();
                c.CargarConcepto(dataGridView1);
            }


        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text != string.Empty)
            {
                string[] valores = c.InformacionConcepto(cmbTipo.Text);
                txtTipo.Text = valores[0];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty && cmbFiltro.Text == "TODOS")
            {
                c.CargarConceptoFiltro(dataGridView1, txtFiltro.Text);
            }
            else if (txtFiltro.Text != string.Empty && cmbFiltro.Text != "TODOS")
            {
                c.CargarConceptoFiltroClaseConcep(dataGridView1, txtFiltro.Text, cmbFiltro.Text);
            }
            else if (txtFiltro.Text == string.Empty && cmbFiltro.Text != "TODOS")
            {
                c.CargarConceptoFiltroClase(dataGridView1, cmbFiltro.Text);
            }
            else
            {
                c.CargarConcepto(dataGridView1);
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != string.Empty && cmbFiltro.Text == "TODOS")
            {
                c.CargarConceptoFiltro(dataGridView1, txtFiltro.Text);
            }
            else if (txtFiltro.Text != string.Empty && cmbFiltro.Text != "TODOS")
            {
                c.CargarConceptoFiltroClaseConcep(dataGridView1, txtFiltro.Text, cmbFiltro.Text);
            }
            else if (txtFiltro.Text == string.Empty && cmbFiltro.Text != "TODOS")
            {
                c.CargarConceptoFiltroClase(dataGridView1, cmbFiltro.Text);
            }
            else
            {
                c.CargarConcepto(dataGridView1);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EgresoIngreso egresoIngreso = new EgresoIngreso();
            egresoIngreso.ShowDialog();
            //if (txtTipo.Text=="Ingreso")
            //{
            //    ReporteConceptosPresupuestos reporteConceptosPresupuestos = new ReporteConceptosPresupuestos();
            //    reporteConceptosPresupuestos.ShowDialog();
            //}
            //else
            //{
            //    ReporteConceptosPresupuestoEgreso reporteConceptosPresupuestoEgreso = new ReporteConceptosPresupuestoEgreso();
            //    reporteConceptosPresupuestoEgreso.ShowDialog();
            //}
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa2(txtClave.Text));

                        c.CargarConcepto(dataGridView1);
                        Limpiar();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El registro esta en uso, no es posible eliminar.(Si el registro es de tipo egreso elimine los Productos/Servicios vinculados para continuar)");
                    }
                }
                else
                {
                    MessageBox.Show("No tiene permisos de administrador");
                }
            }

        }
    }
}
