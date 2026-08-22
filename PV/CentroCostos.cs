using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Condominios.Clases.CentroCostos;
using PuntoVentas.Clases.FormasPago;
using PuntoVentas.Clases.Login;
using PV;
using PV.Clases.CentroCostos;

namespace Condominios
{
    public partial class CentroCostos : Form
    {
        DBCentroCostos c = new DBCentroCostos();
        DBDatosProyecto d = new DBDatosProyecto();
        public CentroCostos()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button1, "Nuevo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Centros Costos");
            T.SetToolTip(button9, "Imprimir");
        }

        private void CentroCostos_Load(object sender, EventArgs e)
        {
            //GenerarNoCategoria();
            cmbEstatus.Text = "Activo";
            c.CargarCentros(dataGridView2);
        }

        void GenerarNoCategoria()
        {
            DBCentroCostos.Folio = 0;
            c.ClaveCentroSiguiente();
            if (DBCentroCostos.Folio == 0)
            {
                DBCentroCostos.Folio = 1;
                txtClaveCategoria.Text = Convert.ToString(DBCentroCostos.Folio);

            }
            else
            {
                DBCentroCostos.Folio = DBCentroCostos.Folio + 1;
                txtClaveCategoria.Text = Convert.ToString(DBCentroCostos.Folio);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            //{
            //    if (e.RowIndex != -1)
            //    {
            //        if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
            //        {
            //            string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
            //            MessageBox.Show(c.EliminarDepartamento(Clave));

            //            if (DBCentroCostos.Eliminado == 0)
            //            {
            //                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

            //            }

            //        }
            //    }
            //}
            //else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Dependen")
            //{
            //    if (e.RowIndex != -1)
            //    {
            //        if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
            //        {
            //            string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
            //            string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

            //            if (c.ConsultaExistencia(Clave) != 0)
            //            {
            //                DepartamentoSubDepartamentos departamentoSub = new DepartamentoSubDepartamentos(Clave, Nombre);
            //                departamentoSub.ShowDialog();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Confirme el registro actual antes de registrar dependencias");
            //            }

            //        }
            //    }
            //}
            //else
            //{
            //    return;
            //}
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                    {
                        string Clave = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                        if (d.ActualizarEstatusProyecto(Clave))
                        {
                            MessageBox.Show("Se elimino el poryecto correctamente");
                            CargarDatosProyecto(txtNombre.Text);

                        }
                        else
                        {
                            MessageBox.Show("No es posible eliminar el poryecto correctamente");
                        }
                    }



                        

                    
                }
            }
            
            else
            {
                return;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int Fila = dataGridView1.RowCount;

            if (Fila > 1)
            {
                try
                {
                    string F = dataGridView1.Rows[e.RowIndex - 1].Cells["ClaveFamilia"].Value.ToString();
                    int Indice = Convert.ToInt32(F.Split('-')[1]) + 1;
                    dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClaveCategoria.Text + '-' + Indice;
                }catch(Exception Ex)
                {

                }
                
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClaveCategoria.Text + '-' + Fila;
            }
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value == null)
            {

                dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty)
            {
                MessageBox.Show("Generar un nuevo registro.");
            }
            if (txtNombre.Text != string.Empty)
            {
                ArrayList ListaConcept = new ArrayList();
                ArrayList ListaConcept2 = new ArrayList();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ClaveFamilia"].Value != null && row.Cells["Nombre"].Value != null)
                    {
                        ListaConcept.Add(row.Cells["ClaveFamilia"].Value.ToString());
                        ListaConcept2.Add(row.Cells["Nombre"].Value.ToString());

                    }
                }

                MessageBox.Show(c.RegistroCentroCostos(txtClaveCategoria.Text, txtNombre.Text, cmbEstatus.Text, txtCuenta.Text, txtDescripcion.Text, ListaConcept, ListaConcept2));
                Limpiar();
                //GenerarNoCategoria();
                c.CargarCentros(dataGridView2);
            }
            else
            {
                MessageBox.Show("Registre el nombre de la categoria para continuar.");
            }
        }

        void Limpiar()
        {
            txtNombre.Clear();
            txtClaveCategoria.Clear();
            cmbEstatus.Text = "Activo";
            txtCuenta.Clear();
            txtDescripcion.Clear();
            dataGridView1.Rows.Clear();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoCategoria();
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClaveCategoria = dataGridView2.Rows[e.RowIndex].Cells["ClaveCategoria"].Value.ToString();
                c.ConsultaCentrosSeleccionada(ClaveCategoria, txtNombre, cmbEstatus, txtCuenta, txtDescripcion, dataGridView1);
                txtClaveCategoria.Text = ClaveCategoria;
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                string nombre = dataGridView2.Rows[e.RowIndex].Cells["NombreC"].Value.ToString();

                CargarDatosProyecto(nombre);
            }
            else
            {
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteCentroCostos reporteCentroCostos = new ReporteCentroCostos();
            reporteCentroCostos.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoCategoria();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtClaveCategoria.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveCategoria.Text));

                        c.CargarCentros(dataGridView2);
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


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty) {
                MessageBox.Show("Se debe seleccionar un centro de costos");
            }
            else { 
            DatosProyecto datosproyecto = new DatosProyecto("","");
            datosproyecto.centrocosto = txtNombre.Text;
            datosproyecto.ShowDialog();
                // Al cerrar el formulario hijo, se refresca el grid
                CargarDatosProyecto(txtNombre.Text);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CargarDatosProyecto(string folio)
        {
            try
            {
                var pagos = d.CargarDatosProyectos(folio);

                dataGridView1.Rows.Clear();

                foreach (DataRow pago in pagos.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = pago["Folio"];
                    dataGridView1.Rows[n].Cells[1].Value = pago["Id"];
                    dataGridView1.Rows[n].Cells[2].Value = pago["Proyecto"];


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message);
            }
        }

        private void txtClaveCategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string ClaveCategoria = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                DatosProyecto datosproyecto = new DatosProyecto(ClaveCategoria, nombre);
                datosproyecto.centrocosto = txtNombre.Text;
                datosproyecto.ShowDialog();
            }
            else
            {
                return;
            }
        }
    }
}
