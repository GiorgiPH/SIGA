using System;
using System.Drawing;
using System.Windows.Forms;
using PuntoVentas.Clases.Personal;
using PuntoVentas.Clases.Login;
using PV;
using Guna.UI2.WinForms;

namespace PuntoVentas
{
    public partial class CatalogoPersonal : Form
    {
        DBPersonal c = new DBPersonal();

        public CatalogoPersonal()
        {
            InitializeComponent();
            ToolTip T = new ToolTip();
            T.SetToolTip(button7, "Nuevo Empleado");
            T.SetToolTip(button8, "Consultar Empleado");
            T.SetToolTip(button9, "Imprimir");

            T.SetToolTip(guna2CircleButton1, "Menú Principal");
            T.SetToolTip(guna2PictureBox1, "Clic para Desplegar");
            T.SetToolTip(guna2PictureBox2, "Clic para Ocultar");
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void CatalogoPersonal_Load(object sender, EventArgs e)
        {
            //GenerarNoEmpleado();
            cmbEstatus.Text = "Activo";
            c.CargarEmpleados(dataGridView1);
            c.SeleccionarUsuario(cmbClaveUsuario);
            
        }

        void GenerarNoEmpleado()
        {
            DBPersonal.Folio = 0;
            c.ClavePersonalSiguiente();
            if (DBPersonal.Folio == 0)
            {
                DBPersonal.Folio = 1;
                txtClaveEmpleado.Text = Convert.ToString(DBPersonal.Folio);

            }
            else
            {
                DBPersonal.Folio = DBPersonal.Folio + 1;
                txtClaveEmpleado.Text = Convert.ToString(DBPersonal.Folio);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtClaveEmpleado.Text == string.Empty)
            {
                MessageBox.Show("Genere un nuevo registro.");
            }
            else if (txtNombre.Text== string.Empty)
            {
                MessageBox.Show("Registre el nombre del empleado para continuar.");
            }
            else if (txtApellidoP.Text == string.Empty)
            {
                MessageBox.Show("Registre el apellido paterno del empleado para continuar.");
            }
            else if (txtCelular.Text == string.Empty)
            {
                MessageBox.Show("Registre el celular del empleado para continuar.");
            }
            else if (txtEscolaridad.Text == string.Empty)
            {
                MessageBox.Show("Registre la escolaridad del empleado para continuar.");
            }
            else
            {
               MessageBox.Show(c.RegistroEmpleado(txtClaveEmpleado.Text, txtNombre.Text, txtApellidoP.Text, txtApellidoM.Text, cmbEstatus.Text, dtpFechaNacimiento.Text, dtpFechaIngreso.Text, txtEscolaridad.Text, txtTelefonoCasa.Text, txtCelular.Text, txtEmail.Text, txtCalleNumero.Text, txtColonia.Text, txtMunicipio.Text, txtEstado.Text, txtCodigoPostal.Text, txtPais.Text, txtNotas.Text, cmbClaveUsuario.Text, txtPuesto.Text, txtNIP.Text, txtTipoContrato.Text, txtRFC.Text, txtCURP.Text, txtIMSS.Text, Convert.ToDecimal(txtSueldo.Text), txtBono.Text, txtVacaciones.Text, txtHorario.Text, Foto));
                Limpiar();
                //GenerarNoEmpleado();
                c.CargarEmpleados(dataGridView1);
            }
        }

        void Limpiar ()
        {
            txtClaveEmpleado.Clear();
            txtNombre.Clear();
            txtApellidoP.Clear(); 
            txtApellidoM.Clear(); 
            cmbEstatus.Text= "Activo"; 
            dtpFechaNacimiento.ResetText(); 
            dtpFechaIngreso.ResetText(); 
            txtEscolaridad.Clear();
            txtTelefonoCasa.Clear(); 
            txtCelular.Clear();
            txtEmail.Clear(); 
            txtCalleNumero.Clear(); 
            txtColonia.Clear(); 
            txtMunicipio.Clear(); 
            txtEstado.Clear(); 
            txtCodigoPostal.Clear();
            txtPais.Clear();
            txtNotas.Clear(); 
            cmbClaveUsuario.ResetText(); 
            txtPuesto.Clear(); 
            txtNIP.Clear(); 
            txtTipoContrato.Clear();
            txtRFC.Clear(); 
            txtCURP.Clear(); 
            txtIMSS.Clear(); 
            txtSueldo.Text="0.00"; 
            txtBono.Clear(); 
            txtVacaciones.Clear(); 
            txtHorario.Clear();
            Foto.Image = null;
            tabControl1.Enabled = false;
            PanelUsuario.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
            //GenerarNoEmpleado();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lbNIP.Visible==false)
            {
                lbNIP.Visible = true;
                txtNIP.Visible = true;
            }
            else if (lbNIP.Visible == true)
            {
                lbNIP.Visible = false;
                txtNIP.Visible = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;

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
                string Clave = dataGridView1.Rows[e.RowIndex].Cells["ClaveEmpleado"].Value.ToString();
                c.ConsultaEmpleadoSeleccionado(Clave, txtNombre, txtApellidoP, txtApellidoM, cmbEstatus, dtpFechaNacimiento, dtpFechaIngreso, txtEscolaridad, txtTelefonoCasa, txtCelular, txtEmail, txtCalleNumero, txtColonia, txtMunicipio, txtEstado, txtCodigoPostal, txtPais, txtNotas, cmbClaveUsuario, txtPuesto, txtNIP, txtTipoContrato, txtRFC, txtCURP, txtIMSS, txtSueldo, txtBono, txtVacaciones, txtHorario, Foto);
                txtClaveEmpleado.Text = Clave;
                PanelUsuario.Visible = false;
                tabControl1.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
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

        private void txtSueldo_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtSueldo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReportePersonal reporte = new ReportePersonal();
            reporte.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtClaveEmpleado.Text == string.Empty)
            {
                Limpiar();
                GenerarNoEmpleado();
                tabControl1.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarNoEmpleado();
                tabControl1.Enabled = true;
            }
        }

        private void txtBono_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtBono);
        }

        private void txtBono_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (DBLogin.TipoUsuario != "Administrador" && tabControl1.SelectedIndex != 0)
            {
                tabControl1.SelectedIndex = 0;
                MessageBox.Show("No tiene permisos de administrador");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtClaveEmpleado.Text == string.Empty && txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Seleccione un registro para continuar");
            }
            else
            {
                if (DBLogin.TipoUsuario == "Administrador")
                {
                    try
                    {
                        MessageBox.Show(c.EliminarDivisa(txtClaveEmpleado.Text));
                        c.CargarEmpleados(dataGridView1);
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

        private void txtVacaciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            guna2GradientPanel6.Location = new Point(1196, 109);
          
            guna2GradientPanel6.Size = new Size(112, 583);

            // guna2GradientPanel7.Location = new Point(1013, 83);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;


            //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Size = new Size(85, 75);
            toolStripButton3.AutoSize = false;

       

            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
 


            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;


            guna2GradientPanel6.Location = new Point(1258, 109);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
          



            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton3.Size = new Size(23, 79);


        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO EMPLEADO")
            {
                guna2GradientPanel6.Location = new Point(1258, 109);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                if (txtClaveEmpleado.Text == string.Empty)
                {
                    Limpiar();
                    GenerarNoEmpleado();
                    tabControl1.Enabled = true;
                }
                else
                {
                    Limpiar();
                    GenerarNoEmpleado();
                    tabControl1.Enabled = true;
                }

            }
            else if (e.ClickedItem.Text == "CONSULTAR EMPLEADO")
            {

                guna2GradientPanel6.Location = new Point(1258, 109);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
           

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);

           
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                tabControl1.SelectedIndex = 0;

                if (PanelUsuario.Visible == false)
                {
                    PanelUsuario.Visible = true;
                }
                else if (PanelUsuario.Visible == true)
                {
                    PanelUsuario.Visible = false;
                }
            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel6.Location = new Point(1258, 109);
                guna2GradientPanel6.Size = new Size(23, 569);
                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();
                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;

                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
               


                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton3.Size = new Size(23, 79);



                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                ReportePersonal reporte = new ReportePersonal();
                reporte.ShowDialog();
            }
        }
    }
}
