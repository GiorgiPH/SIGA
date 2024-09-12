using System;
using System.Text;
using System.Windows.Forms;
using PV;
using Condominios.Clases.Condominios;
using System.Drawing;
using System.Collections;
using System.Globalization;
using Guna.UI2.WinForms;

namespace Condominios
{
    public partial class CatalogoCondominios : Form
    {
        DBCondominios c = new DBCondominios();

        public CatalogoCondominios()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button10, "Nuevo");
            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(button8, "Consultar Condominio");
            T.SetToolTip(button9, "Imprimir");
        }

        private void CatalogoCondominios_Load(object sender, EventArgs e)
        {
            //Mapa();
            //GenerarNoCondominio();
            c.CargarCondominios(dataGridView2);
            //c.SeleccionarConcepto(cmbConcepto);
            c.SeleccionarEjercicio(txtAnual);
            c.SeleccionarEjercicio(txtAnual2);
            c.ConsultaConcepto(txtConceptoM);
            c.ConsultaConceptoE(txtConceptoE);
        }

        void GenerarNoCondominio()
        {
            DBCondominios.Folio = 0;
            c.ClaveFormaPagoSiguiente();
            if (DBCondominios.Folio == 0)
            {
                DBCondominios.Folio = 1;
                txtClave.Text = Convert.ToString(DBCondominios.Folio);

            }
            else
            {
                DBCondominios.Folio = DBCondominios.Folio + 1;
                txtClave.Text = Convert.ToString(DBCondominios.Folio);

            }
        }

        void Mapa()
        {
            string Calle = txtCalle.Text + " " + txtNoExterior.Text + "," + txtColonia.Text;
            string Ciudad = txtCiudad.Text;
            string Estado = txtMunicipio.Text;
            string CP = txtCodigoPostal.Text;

            try
            {
                StringBuilder direccion = new StringBuilder();
                direccion.Append("https://www.google.com.mx/maps?q=");

                if (Calle != string.Empty && Calle != " ,")
                {
                    direccion.Append(Calle + "," + "+");
                }
                if (Ciudad != string.Empty)
                {
                    direccion.Append(Ciudad + "," + "+");
                }
                if (Estado != string.Empty)
                {
                    direccion.Append(Estado + "," + "+");
                }
                if (CP != string.Empty)
                {
                    direccion.Append(CP + "," + "+");
                }

                webBrowser1.Navigate(direccion.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mapa();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro");
            }
            else if (txtDescripcion.Text == string.Empty)
            {
                MessageBox.Show("Registre la descripcion del condominio");
            }
            else if (txtAnual.Text != string.Empty && (Convert.ToInt32(txtAnual.Text) < 2000 || Convert.ToInt32(txtAnual.Text) > 2099))
            {
                MessageBox.Show("El formato del ejercicio debe estar entre 2000 y 2099");
            }
            else if (txtAnual.Text == string.Empty)
            {
                MessageBox.Show("Registre el ejercicio del concepto de mantenimiento para continuar");
            }
            else if (txtImporte.Text == string.Empty || txtImporte.Text == "0.00")
            {
                MessageBox.Show("Registre el importe del concepto de mantenimiento para continuar");
            }
            else if (dataGridView1.RowCount > 1)
            {
                
                string Periodo = string.Empty;
                if (rdmensual.Checked == true)
                {
                    Periodo = "Mensual";
                }
                else if (rdbimestral.Checked == true)
                {
                    Periodo = "Bimestral";
                }
                else if (rdtrimestral.Checked == true)
                {
                    Periodo = "Trimestral";
                }
                else if (rdanual.Checked == true)
                {
                    Periodo = "Anual";
                }
                else if (rdCuatrimestral.Checked==true)
                {
                    Periodo = "Cuatrimestral";
                }
                else if (rdSemestral.Checked == true)
                {
                    Periodo = "Semestral";
                }
                else
                {
                    MessageBox.Show("Seleccione la frecuencia para la cuota de mantenimiento");
                    return;
                }

                string Periodo2 = string.Empty;
                if (rdMensual2.Checked == true)
                {
                    Periodo2 = "Mensual";
                }
                else if (rdBimestral2.Checked == true)
                {
                    Periodo2 = "Bimestral";
                }
                else if (rdTrimestral2.Checked == true)
                {
                    Periodo2 = "Trimestral";
                }
                else if (rdAnual2.Checked == true)
                {
                    Periodo2 = "Anual";
                }
                else if (rdCuatrimestral2.Checked == true)
                {
                    Periodo2 = "Cuatrimestral";
                }
                else if (rdSemestral2.Checked == true)
                {
                    Periodo2 = "Semestral";
                }

                MessageBox.Show(c.RegistroFormaPago(txtClave.Text, txtDescripcion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCodigoPostal.Text, txtCiudad.Text, txtEstado.Text, txtPais.Text, txtTelefono.Text, txtReferencia.Text, txtCaracteristicas.Text, rdbGeneral, rdbProIndiviso, rdbOtros, txtConcepto.Text, txtAnual.Text, Convert.ToDecimal(txtImporte.Text), cmbMetodo.Text, Periodo, txtAnual2.Text, Convert.ToDecimal(txtImporteEx.Text), cmbMetodo2.Text, Periodo2, Foto));
                Limpiar();
                //GenerarNoCondominio();
                c.CargarCondominios(dataGridView2);
            }
            else
            {
                MessageBox.Show("Es necesario registrar la estructura del condominio");
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                Limpiar();
                c.ConsultaCondominioSeleccionado(Clave, txtDescripcion, txtCalle, txtNoExterior, txtNoInterior, txtColonia, txtMunicipio, txtCodigoPostal, txtCiudad, txtEstado, txtPais, txtTelefono, txtReferencia, txtCaracteristicas, rdbGeneral, rdbProIndiviso, rdbOtros, txtConcepto, txtAnual, txtImporte, cmbMetodo, rdmensual, rdbimestral, rdtrimestral, rdanual, rdCuatrimestral, rdSemestral, txtAnual2, txtImporteEx, cmbMetodo2, rdMensual2, rdBimestral2, rdTrimestral2, rdAnual2, rdCuatrimestral2, rdSemestral2, Foto, dataGridView1);
                //string[] valores = c.InformacionConcepto2(txtConcepto.Text);
                //cmbConcepto.Text = valores[0];
                txtClave.Text = Clave;
                Mapa();
                PanelUsuario.Visible = false;
                groupBox1.Enabled = true;
                groupBox3.Enabled = true;
                groupBox5.Enabled = true;
                tabControl1.Enabled = true;
                txtDescripcion.Focus();
            }
            else
            {
                return;
            }
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtAnual.Text = null;
            txtImporte.Text = "0.00";
            txtImporteEx.Text = "0.00";
            ttximporte2.Text = "0.00";
            txtImporteE.Text = "0.00";
            cmbMetodo.Text = null;
            txtDescripcion.Clear();
            txtCalle.Clear();
            txtNoExterior.Clear();
            txtNoInterior.Clear();
            txtColonia.Clear();
            txtMunicipio.Clear();
            txtEstado.Clear();
            txtCodigoPostal.Clear();
            txtCiudad.Clear();
            txtPais.Clear();
            txtTelefono.Clear();
            txtReferencia.Clear();
            txtCaracteristicas.Clear();
            Foto.Image = null;
            dataGridView1.Rows.Clear();
            rdbGeneral.Checked = false;
            rdbProIndiviso.Checked = false;
            rdbOtros.Checked = false;
            groupBox3.Enabled = false;
            txtConcepto.Clear();
            cmbConcepto.Text = null;
            groupBox1.Enabled = false;
            tabControl1.Enabled = false;
            rdmensual.Checked = false;
            rdbimestral.Checked = false;
            rdtrimestral.Checked = false;
            rdanual.Checked = false;
            rdCuatrimestral.Checked = false;
            rdSemestral.Checked = false;
            rdMensual2.Checked = false;
            rdBimestral2.Checked = false;
            rdTrimestral2.Checked = false;
            rdAnual2.Checked = false;
            rdCuatrimestral2.Checked = false;
            rdSemestral2.Checked = false;
            PanelUsuario.Visible = false;
            txtAnual2.Text = null;
            cmbMetodo2.Text = null;
            groupBox3.Enabled = true;
            groupBox5.Enabled = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Limpiar();
            Mapa();
            //GenerarNoCondominio();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();

            Abrir.Filter = "Archivos JPEG(* .JPEG) |*.jpg";
            Abrir.InitialDirectory = "C:/";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                Bitmap foto = new Bitmap(Dir);

                Foto.Image = (Image)foto;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Foto.Image = null;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //int Fila = dataGridView1.RowCount;

            //if (Fila > 1)
            //{
            //    if (dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value == null && Fila == 1)
            //    {
            //        string F = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
            //        int Indice = Convert.ToInt32(F.Split('-')[1]) + 1;
            //        dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClave.Text + '-' + Indice;
            //    }
            //    else if (dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value == null && Fila > 1)
            //    {
            //        string F = dataGridView1.Rows[e.RowIndex - 1].Cells["ClaveFamilia"].Value.ToString();
            //        int Indice = Convert.ToInt32(F.Split('-')[1]) + 1;
            //        dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClave.Text + '-' + Indice;
            //    }
            //}
            //else
            //{
            //    dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = txtClave.Text + '-' + Fila;
            //}
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value == null)
            //{
            //    dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value = null;

            //}

            //NumberFormatInfo formato = new CultureInfo("US-AR").NumberFormat;

            //formato.CurrencyGroupSeparator = ",";
            //formato.NumberDecimalSeparator = ".";

            //decimal pro = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            //dataGridView1.Rows[e.RowIndex].Cells[4].Value = pro.ToString("N", formato);
            //decimal cuo = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            //dataGridView1.Rows[e.RowIndex].Cells[5].Value = cuo.ToString("N", formato);

            //if (dataGridView1.Rows[e.RowIndex].Cells[6].Value != null)
            //{
            //    string del = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            //    int numero1 = del.Length;

            //    if (numero1 == 8)
            //    {
            //        string parte1 = del.Substring(0, 2);
            //        string parte2 = del.Substring(2, 2);
            //        string parte3 = del.Substring(4, 4);
            //        string fecha1 = parte1 + "/" + parte2 + "/" + parte3;
            //        dataGridView1.Rows[e.RowIndex].Cells[6].Value = fecha1;
            //    }
            //    else if (numero1 < 8)
            //    {
            //        MessageBox.Show("El Formato de la fecha es incorrecto");
            //        dataGridView1.Rows[e.RowIndex].Cells[6].Value = DateTime.Today.ToString("dd/MM/yyyy");
            //    }

            //}
            //if (dataGridView1.Rows[e.RowIndex].Cells[7].Value != null)
            //{
            //    string al = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            //    int numero2 = al.Length;

            //    if (numero2 == 8)
            //    {
            //        string parte1 = al.Substring(0, 2);
            //        string parte2 = al.Substring(2, 2);
            //        string parte3 = al.Substring(4, 4);
            //        string fecha1 = parte1 + "/" + parte2 + "/" + parte3;
            //        dataGridView1.Rows[e.RowIndex].Cells[7].Value = fecha1;
            //    }
            //    else if (numero2 < 8)
            //    {
            //        MessageBox.Show("El Formato de la fecha es incorrecto");
            //        dataGridView1.Rows[e.RowIndex].Cells[7].Value = DateTime.Today.ToString("dd/MM/yyyy");
            //    }
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                    {
                        string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
                        MessageBox.Show(c.EliminarDepartamentoArea(Clave, txtClave.Text));

                        if (DBCondominios.Eliminado == 0)
                        {
                            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            c.ConsultaestructuraCondominioSeleccionado(txtClave.Text, dataGridView1);
                        }
                    }
                }
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Modificar")
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                    {
                        string ClaveDep = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
                        AgregarEstructura departamentoSub = new AgregarEstructura(txtClave.Text, ClaveDep);
                        departamentoSub.ShowDialog();
                    }
                }
            }
            //else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "AreasComunes")
            //{
            //    if (e.RowIndex != -1)
            //    {
            //        if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
            //        {
            //            string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveFamilia"].Value.ToString();
            //            string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

            //            if (c.ConsultaExistencia(Clave) != 0)
            //            {
            //                DepartamentoAreas departamentoSub = new DepartamentoAreas(Nombre, Clave);
            //                departamentoSub.ShowDialog();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Confirme el registro actual antes de registrar las areas comunes");
            //            }
            //        }
            //    }
            //}
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else if (c.ConsultaExistencia2(txtClave.Text) != 0)
            {
                DepartamentoAreas departamentoSub = new DepartamentoAreas(txtDescripcion.Text, txtClave.Text);
                departamentoSub.ShowDialog();
            }
            else
            {
                MessageBox.Show("Confirme el registro actual antes de registrar las areas comunes");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReporteCondominios reporteCondominios = new ReporteCondominios();
            reporteCondominios.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                Limpiar();
                GenerarNoCondominio();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
                txtDescripcion.Focus();
                rdmensual.Checked = true;
            }
            else
            {
                Limpiar();
                GenerarNoCondominio();
                groupBox1.Enabled = true;
                tabControl1.Enabled = true;
                txtDescripcion.Focus();
                rdmensual.Checked = true;
            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConcepto.Text != string.Empty)
            {
                string[] valores = c.InformacionConcepto(cmbConcepto.Text);
                txtConcepto.Text = valores[0];
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGeneral.Checked == true)
            {
                groupBox3.Enabled = true;
            }
        }

        private void rdbProIndiviso_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProIndiviso.Checked == true)
            {
                groupBox3.Enabled = true;
            }
        }

        private void rdbOtros_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbOtros.Checked == true)
            {
                groupBox3.Enabled = true;
            }
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtImporte);

            if (ttximporte2.Text!=string.Empty)
            {
                if (ttximporte2.Text != txtImporte.Text)
                {
                    cmbMetodo.Text = "Manual";
                }
            }
        }

        private void Moneda(ref Guna2TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAnual_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

        }

        private void webBrowser1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((int)e.KeyChar == (int)Keys.Enter)
            //{
            //    e.Handled = true;
            //    dataGridView1.EndEdit();
            //    int numColumn = dataGridView1.CurrentCell.ColumnIndex;
            //    int numRow = dataGridView1.CurrentCell.RowIndex;
            //    if (numColumn == dataGridView1.ColumnCount - 1)
            //    {
            //        if (dataGridView1.RowCount > (numRow + 1))
            //        {
            //            dataGridView1.CurrentCell = dataGridView1[1, numRow + 1];
            //        }
            //    }
            //    else
            //        dataGridView1.CurrentCell = dataGridView1[numColumn + 1, numRow];
            //}

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else if (MessageBox.Show("¿El registro sera confirmado para agregar la estructura, continuar?", "Estructura del Condominio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtClave.Text == string.Empty)
                {
                    MessageBox.Show("Genere un nuevo registro");
                }
                else if (txtDescripcion.Text == string.Empty)
                {
                    MessageBox.Show("Registre la descripcion del condominio");
                }
                else if (txtAnual.Text != string.Empty && (Convert.ToInt32(txtAnual.Text) < 2000 || Convert.ToInt32(txtAnual.Text) > 2099))
                {
                    MessageBox.Show("El formato del ejercicio debe estar entre 2000 y 2099");
                }
                else if (txtAnual.Text == string.Empty)
                {
                    MessageBox.Show("Registre el ejercicio del concepto de mantenimiento para continuar");
                }
                else if (txtImporte.Text == string.Empty || txtImporte.Text == "0.00")
                {
                    MessageBox.Show("Registre el importe del concepto de mantenimiento para continuar");
                }
                else
                {

                    string Periodo = string.Empty;
                    if (rdmensual.Checked == true)
                    {
                        Periodo = "Mensual";
                    }
                    else if (rdbimestral.Checked == true)
                    {
                        Periodo = "Bimestral";
                    }
                    else if (rdtrimestral.Checked == true)
                    {
                        Periodo = "Trimestral";
                    }
                    else if (rdanual.Checked == true)
                    {
                        Periodo = "Anual";
                    }
                    else if (rdCuatrimestral.Checked == true)
                    {
                        Periodo = "Cuatrimestral";
                    }
                    else if (rdSemestral.Checked == true)
                    {
                        Periodo = "Semestral";
                    }
                    else
                    {
                        MessageBox.Show("Seleccione la frecuencia para la cuota de mantenimiento");
                        return;
                    }

                    string Periodo2 = string.Empty;
                    if (rdMensual2.Checked == true)
                    {
                        Periodo2 = "Mensual";
                    }
                    else if (rdBimestral2.Checked == true)
                    {
                        Periodo2 = "Bimestral";
                    }
                    else if (rdTrimestral2.Checked == true)
                    {
                        Periodo2 = "Trimestral";
                    }
                    else if (rdAnual2.Checked == true)
                    {
                        Periodo2 = "Anual";
                    }
                    else if (rdCuatrimestral2.Checked == true)
                    {
                        Periodo2 = "Cuatrimestral";
                    }
                    else if (rdSemestral2.Checked == true)
                    {
                        Periodo2 = "Semestral";
                    }
                    c.RegistroFormaPago2(txtClave.Text, txtDescripcion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCodigoPostal.Text, txtCiudad.Text, txtEstado.Text, txtPais.Text, txtTelefono.Text, txtReferencia.Text, txtCaracteristicas.Text, rdbGeneral, rdbProIndiviso, rdbOtros, txtConcepto.Text, txtAnual.Text, Convert.ToDecimal(txtImporte.Text), cmbMetodo.Text, Periodo, txtAnual2.Text, Convert.ToDecimal(txtImporteEx.Text), cmbMetodo2.Text, Periodo2, Foto);
                    //GenerarNoCondominio();
                    c.CargarCondominios(dataGridView2);
                    string ClaveDep = string.Empty;
                   AgregarEstructura departamentoSub = new AgregarEstructura( txtClave.Text, ClaveDep);
                    departamentoSub.ShowDialog();
                }

               
            }
        }

        private void CatalogoCondominios_Activated(object sender, EventArgs e)
        {
            if (txtClave.Text!= string.Empty)
            {
                c.ConsultaestructuraCondominioSeleccionado(txtClave.Text, dataGridView1);
            }
        }

        private void txtAnual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtConceptoM.Text!= string.Empty && txtAnual.Text!= string.Empty && groupBox3.Enabled==true)
            {
                c.ConsultaPresupuesto(txtAnual.Text, txtConceptoM.Text, txtImporte);
                c.ConsultaPresupuesto(txtAnual.Text, txtConceptoM.Text, ttximporte2);

                    cmbMetodo.Text = "Calculado";

            }
        }

        private void ttximporte2_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref ttximporte2);
        }

        private void txtImporteEx_TextChanged(object sender, EventArgs e)
        {
            Moneda( ref txtImporteEx);

            if (txtImporteEx.Text != string.Empty)
            {
                if (txtImporteE.Text != txtImporteEx.Text)
                {
                    cmbMetodo2.Text = "Manual";
                }
            }
        }

        private void txtImporteEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void txtAnual2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtConceptoE.Text != string.Empty && txtAnual2.Text != string.Empty && groupBox5.Enabled==true)
            {
                c.ConsultaPresupuesto2(txtAnual2.Text, txtConceptoE.Text, txtImporteEx);
                c.ConsultaPresupuesto2(txtAnual2.Text, txtConceptoE.Text, txtImporteE);

                    cmbMetodo2.Text = "Calculado";

            }
        }
    }
}
