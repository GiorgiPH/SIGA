using PV.Clases.Polizas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class GENERARPOLIZAS : Form
    {
        DBPolizas c = new DBPolizas();

        string TipoPolz = string.Empty;
        string CuentaPro = string.Empty;
        string CuentaDoc = string.Empty;
        string CuentaIng = string.Empty;
        string CuentaBanc = string.Empty;
        string cuentaDepositosGasto = string.Empty;

        string CuentaProA = string.Empty;
        string CuentaDocA = string.Empty;
        string CuentaIngA = string.Empty;
        string CuentaBancA = string.Empty;
        string cuentaDepositosGastoA = string.Empty;
        string Grupo = string.Empty;
        //--- POLIZA COMPRAS
        string CuentaAlmacen = string.Empty;
        string CuentaMovimientoInv = string.Empty;
        string Cuentaproveedores = string.Empty;
        string CuentaProductoServ = string.Empty;
        string CuentaConceptosGlobales = string.Empty;
        string CuentaCentroCosto = string.Empty;


        string CuentaAlmacenA = string.Empty;
        string CuentaMovimientoInvA = string.Empty;
        string CuentaproveedoresA = string.Empty;
        string CuentaProductoServA = string.Empty;
        string CuentaConceptosGlobalesA = string.Empty;
        string CuentaCentroCostoA = string.Empty;


        public static string TipopolizaCompras = string.Empty;

        public GENERARPOLIZAS(string TipoPoliza)
        {
            InitializeComponent();

            TipoPolz = TipoPoliza;
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
            dtpPeriodo.CustomFormat = "yyyy/MM";
            dtpPeriodo.ShowUpDown = true;
            //Primero obtenemos el día actual

            // dtpFechaInicial.Format = DateTimePickerFormat.Custom;
            // dtpFechaInicial.CustomFormat = "dd";
            //dtpFechaInicial.ShowUpDown = true;
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime dtpFechaInicial = new DateTime(date.Year, date.Month, 1);

            dtpFechaFinal.Format = DateTimePickerFormat.Custom;
            dtpFechaFinal.CustomFormat = "dd";
            dtpFechaFinal.ShowUpDown = true;


        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpPeriodo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

            guna2GradientPanel6.Location = new Point(1044, 78);
            guna2GradientPanel6.Size = new Size(112, 583);
            guna2GradientPanel7.Size = new Size(112, 583);
            guna2GradientPanel7.BringToFront();

            toolStrip2.Size = new Size(112, 583);
            toolStrip2.Visible = true;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
          
            //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Size = new Size(85, 75);
            toolStripButton1.AutoSize = false;

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton2.Size = new Size(85, 75);
            toolStripButton2.AutoSize = false;

          

            toolStripButton1.Visible = true;
          //  toolStripButton2.Visible = true;
            



            guna2PictureBox2.Location = new Point(2, 6);
            guna2PictureBox2.Visible = true;
            guna2PictureBox1.Visible = false;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

            guna2PictureBox2.Visible = false;
            guna2PictureBox1.Visible = true;

            guna2GradientPanel6.Location = new Point(1104, 78);
            guna2GradientPanel6.Size = new Size(23, 569);

            guna2GradientPanel7.Size = new Size(23, 569);
            guna2GradientPanel7.SendToBack();

            toolStrip2.Size = new Size(23, 569);
            toolStrip2.Visible = false;
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
         

            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Size = new Size(23, 79);

            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton2.Size = new Size(23, 79);

        }

        private void GENERARPOLIZAS_Load(object sender, EventArgs e)
        {
            if (TipoPolz == "Polizas Condiminios")
            {


                cmbExportar.Items.Insert(0, "SI");
                cmbExportar.SelectedIndex = 0;
                //  c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);

                c.TipoDocumento(cmbdocumento);
                //   int Cons = Convert.ToInt32(lblconsecutivo.Text);
                //cmbclave.Text = Convert.ToString(Cons + 1);
                cmbdocumento.Items.Insert(0, "Todos");
                // cmbExportar.SelectedIndex = 3;
                cmbdocumento.Enabled = true;
            }

            if (TipoPolz == "Definiciones Gastos Personales")
            {
                cmbExportar.Items.Insert(0, "SI");
                cmbExportar.SelectedIndex = 0;
                //  c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);

                //c.TipoDocumento(cmbdocumento);
                //   int Cons = Convert.ToInt32(lblconsecutivo.Text);
                //cmbclave.Text = Convert.ToString(Cons + 1);
                cmbdocumento.Enabled = false;
                cmbdocumento.Items.Insert(0, "Todos");
                cmbdocumento.SelectedIndex = 0;

                label13.Text = "Gasto";
                label13.Visible = false;
                cmbdocumento.Visible = false;
                label14.Text = "CONCEPTO GASTOS PERSONALES PROPIETARIO";
                label14.Location = new Point(20, 465);
                //    txtConcepto.Location = new Point(469, 392);
                txtConcepto.Size = new Size(436, 21);
            }
            if (TipoPolz == "Polizas Compras")
            {
                cmbExportar.Items.Insert(0, "SI");
                cmbExportar.SelectedIndex = 0;
                //  c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);
                c.TipoDocumentoCompras(cmbdocumento);
                cmbdocumento.Items.Insert(0, "Todos");
                cmbdocumento.SelectedIndex = 0;
                txtResumida.Text = "SI";
                cmbdocumento.Enabled = false;

                // label14.Text = "CONCEPTO GASTOS PERSONALES PROPIETARIO";

            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbclave_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtnombre.Text = "";
            txtEstatus.Text = "";
            txtCargo1.Text = "";
            txtCargo2.Text = "";
            txtCargo3.Text = "";
            txtCargo4.Text = "";
            txtCargo5.Text = "";
            txtAbono1.Text = "";
            txtAbono2.Text = "";
            txtAbono3.Text = "";
            txtAbono4.Text = "";
            cmbTipoP.Text = "";
            txtDiarioP.Text = "";

            int clave = Convert.ToInt16(cmbclave.Text);
            c.Catalogo_DefPolizaGen2(clave, TipoPolz, txtnombre, txtEstatus, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtAbono1, txtAbono2, txtAbono3, txtAbono4, cmbTipoP, txtDiarioP, TSeparador);

        }

        private void dtpPeriodo_Leave(object sender, EventArgs e)
        {

            DateTime date = dtpPeriodo.Value.Date;
            dtpFechaInicial.Value = new DateTime(date.Year, date.Month, 1);
            dtpFechaFinal.Value = dtpPeriodo.Value.AddMonths(1).AddDays(-1);


        }

        private void cmbdocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbdocumento.Text == "Todos")
            {
                txtConcepto.Text = "Todos los Documentos";

            }
            else
            {
                if (TipoPolz == "Polizas Compras")
                {
                    c.DescripcionDocumentoCompras(cmbdocumento.Text, txtConcepto);
                }
                else
                {
                    c.DescripcionDocumento(cmbdocumento.Text, txtConcepto);
                }



            }


        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Se puede colocar uno de los siguientes simbolos para el seprador Ejemplo: '-','/'. En el caso de no ocupar nada se recomienda dejar un espacio en blanco precionando la barra espaciadora");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "SELECCIONAR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1104, 78);
                guna2GradientPanel6.Size = new Size(23, 569);

                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
               

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

                groupBox1.BringToFront();
              //  MessageBox.Show(TipoPolz);
                if (TipoPolz == "Polizas Condiminios")
                {
                    groupBox2.Enabled = true;
                    groupBox1.Visible = true;
                    c.ConsultaDefPoliza(dataGridView1);
                    if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                    {
                        cmbdocumento.Enabled = false;
                    }
                    else
                    {
                        cmbdocumento.Enabled = true;
                    }
                }
                else if (TipoPolz == "Definiciones Gastos Personales")
                {

                    groupBox2.Enabled = true;
                    groupBox1.Visible = true;
                    c.ConsultaDefPolizaGP(dataGridView1);
                    if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                    {
                        cmbdocumento.Enabled = false;
                    }
                    else
                    {
                        cmbdocumento.Enabled = true;
                    }
                }
                else if (TipoPolz == "Polizas Compras")
                {

                    groupBox2.Enabled = true;
                    groupBox1.Visible = true;
                    c.ConsultaDefPolizaCompras(dataGridView1, TipopolizaCompras);

                    if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                    {
                        cmbdocumento.Enabled = false;
                    }
                    else
                    {
                        cmbdocumento.Enabled = true;
                    }
                }


            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1104, 78);
                guna2GradientPanel6.Size = new Size(23, 569);

                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
              

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

             
           

            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1104, 78);
                guna2GradientPanel6.Size = new Size(23, 569);

                guna2GradientPanel7.Size = new Size(23, 569);
                guna2GradientPanel7.SendToBack();

                toolStrip2.Size = new Size(23, 569);
                toolStrip2.Visible = false;
                toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                

                this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton1.Size = new Size(23, 79);

                this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
                toolStripButton2.Size = new Size(23, 79);

               
            }
        }

        private void cmbcargo6_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtNoPoliza.Text == "" || txtNoPoliza.Text == string.Empty)
            {
                MessageBox.Show("Se debe agregar el Numero de Poliza");
            }
            else if (txtConcepto.Text == "" || txtConcepto.Text == string.Empty)
            {
                MessageBox.Show("Se debe agregar el Documento");
            }
            else if (cmbdocumento.Text == "" && txtnombre.Text != "Define Póliza Anticipos")
            {
                MessageBox.Show("Se debe agregar el Tipo de Documento");
            }
            else if (cmbdocumento.Text == "" && txtnombre.Text != "Define Póliza Aplicación Anticipos")
            {
                MessageBox.Show("Se debe agregar el Tipo de Documento");
            }
            else
            {
                // string a probar
                string date = dtpPeriodo.Text;
                // iniciando un objeto de la clase Regex enviándole en el constructor la expresión regular 
                // para dd/mm/yyyy para años que empiecen en: 19xx a más
                Regex regex = new Regex(@"^\d{2,4}\/\d{1,2}$");
                // guardando en el objecto de la clase Match el resultado de usar el método Match con el objeto
                // de la clase Regex enviándole como parámetro el string date
                Match m = regex.Match(date);
                // si el objeto de la clase Match tiene la fecha entonces será válida
                if (string.IsNullOrEmpty(m.Value))
                {
                    MessageBox.Show(date + "Formato invalido debe colocar yyyy/MM");
                }
                else if (cmbclave.Text == "")
                {
                    MessageBox.Show("Se debe selecionar la clave del tipo de poliza a agregar");
                }
                else
                {
                    if (TipoPolz == "Polizas Condiminios")
                    {
                        CargaCuentaC();
                        CargaCuentaA();

                        if (lblCuentaA1.Text != string.Empty && lblCuentaC1.Text != string.Empty)
                        {
                            RegistrarCuentaContable();
                        }

                        if (TipoPolz == "Polizas Condiminios")
                        {
                            Grupo = "Definiciones Condominios";
                        }

                        c.ConcatenaCuentasContables(lblContatenaCuentaContables, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, Grupo);
                        //    c.ConcatenaCuentasContablesA(lblContatenaCuentaContablesA, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, Grupo);

                        if (lblContatenaCuentaContables.Text == "CCP" || lblContatenaCuentaContables.Text == "CCD" || lblContatenaCuentaContables.Text == "CCI" || lblContatenaCuentaContables.Text == "CCB")
                        {
                            lblContatenaCuentaContables.Text = "";
                            lblContatenaCuentaContablesA.Text = "";
                        }
                        if (lblContatenaCuentaContables.Text == "*")
                        {
                            lblContatenaCuentaContables.Text = "";
                        }
                        if (lblContatenaCuentaContablesA.Text == "*")
                        {
                            lblContatenaCuentaContablesA.Text = "";
                        }


                        c.RegistroGeneracionPoliza(cmbclave.Text, txtnombre.Text, txtEstatus.Text, txtCargo1.Text, txtCargo2.Text, txtCargo3.Text, txtCargo4.Text, txtCargo5.Text, txtAbono1.Text, txtAbono2.Text, txtAbono3.Text, txtAbono4.Text, dtpPeriodo.Text, dtpFechaInicial.Text, dtpFechaInicial.Text, txtNoPoliza.Text, cmbTipoP.Text, txtDiarioP.Text, txtResumida.Text, txtXdias.Text, cmbdocumento.Text, txtConcepto.Text, cmbExportar.Text, TSeparador.Text, TipoPolz);
                        MessageBox.Show("Registro exitoso");

                        string DelDia = dtpFechaInicial.Text + " al " + dtpFechaFinal.Text;
                        string FechaI = dtpPeriodo.Text + "/" + dtpFechaInicial.Text;
                        string FechaF = dtpPeriodo.Text + "/" + dtpFechaFinal.Text;

                        string TipoPoliza = txtnombre.Text;
                        ConversionParametros();
                        if (cmbExportar.Text == "NO")
                        {
                            TSeparador.Text = "";
                        }

                        if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                        {
                            string Cargos = txtCargo1.Text + txtCargo2.Text + txtCargo3.Text + txtCargo4.Text + txtCargo5.Text;
                            string Abonos = txtAbono1.Text + txtAbono2.Text + txtAbono3.Text + txtAbono4.Text;
                            string Concepto = txtConcepto.Text;

                            //                            ReportePolizasAnticipos RPA = new ReportePolizasAnticipos(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, Cargos, Abonos, Concepto, TSeparador.Text);
                            //   MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + " Tipo Poliza: " + TipoPoliza + " C Fija: " + lblContatenaCuentaContables.Text + " CCP: " + CuentaPro + " CDG: " + cuentaDepositosGasto + " CCB: " + CuentaBanc + " CCPA: " + CuentaProA + " CDGA: " + cuentaDepositosGastoA + " CCBA: " + CuentaBancA + " Seprador: " + TSeparador.Text + " C fija A:" + lblContatenaCuentaContablesA.Text);
                           // ReportePolizasAnticipos RPA = new ReportePolizasAnticipos(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaIng, CuentaBanc, CuentaProA, CuentaDocA, CuentaIngA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text, Concepto);
                           // RPA.ShowDialog();
                        }
                        else
                        {
                           // RGeneracionPolizasF GPC = new RGeneracionPolizasF(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaIng, CuentaBanc, CuentaProA, CuentaDocA, CuentaIngA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text);
                           // GPC.ShowDialog();


                        }
                    }
                    else if (TipoPolz == "Definiciones Gastos Personales")
                    {
                        CargaCuentaC();
                        CargaCuentaA();

                        if (lblCuentaA1.Text != string.Empty && lblCuentaC1.Text != string.Empty)
                        {
                            RegistrarCuentaContable();
                        }
                        c.ConcatenaCuentasContables(lblContatenaCuentaContables, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, TipoPolz);

                        c.ConcatenaCuentasContablesA(lblContatenaCuentaContablesA, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, TipoPolz);

                        if (lblContatenaCuentaContables.Text == "CCP" || lblContatenaCuentaContables.Text == "CCD" || lblContatenaCuentaContables.Text == "CCI" || lblContatenaCuentaContables.Text == "CCB")
                        {
                            lblContatenaCuentaContables.Text = "";
                            lblContatenaCuentaContablesA.Text = "";
                        }
                        if (lblContatenaCuentaContables.Text == "*")
                        {
                            lblContatenaCuentaContables.Text = "";
                        }
                        if (lblContatenaCuentaContablesA.Text == "*")
                        {
                            lblContatenaCuentaContablesA.Text = "";
                        }


                        c.RegistroGeneracionPoliza(cmbclave.Text, txtnombre.Text, txtEstatus.Text, txtCargo1.Text, txtCargo2.Text, txtCargo3.Text, txtCargo4.Text, txtCargo5.Text, txtAbono1.Text, txtAbono2.Text, txtAbono3.Text, txtAbono4.Text, dtpPeriodo.Text, dtpFechaInicial.Text, dtpFechaInicial.Text, txtNoPoliza.Text, cmbTipoP.Text, txtDiarioP.Text, txtResumida.Text, txtXdias.Text, cmbdocumento.Text, txtConcepto.Text, cmbExportar.Text, TSeparador.Text, TipoPolz);
                        MessageBox.Show("Registro exitoso");

                        string DelDia = dtpFechaInicial.Text + " al " + dtpFechaFinal.Text;
                        string FechaI = dtpPeriodo.Text + "/" + dtpFechaInicial.Text;
                        string FechaF = dtpPeriodo.Text + "/" + dtpFechaFinal.Text;

                        string TipoPoliza = txtnombre.Text;
                        ConversionParametros();
                        if (txtnombre.Text == "Póliza Depósitos")
                        {
                            TipoPoliza = TipoPoliza;
                            //MessageBox.Show(TipoPoliza);
                        }
                        else if (txtnombre.Text == "Póliza Gastos Personales")
                        {
                            TipoPoliza = TipoPoliza + " " + "Gastos";
                            //   MessageBox.Show(TipoPoliza);
                        }

                        if (cmbExportar.Text == "NO")
                        {
                            TSeparador.Text = "";
                        }
                        if (txtnombre.Text == "Póliza Depósitos")
                        {
                            //    MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CDG: " + cuentaDepositosGasto + "CCB: " + CuentaBanc + " CCPA: " + CuentaProA + "CDGA: " + cuentaDepositosGastoA + "CCBA: " + CuentaBancA + "Seprador: " + TSeparador.Text + "C fija A" + lblContatenaCuentaContablesA.Text);
                        //    ReporteGastoPersonales RGP = new ReporteGastoPersonales(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, cuentaDepositosGasto, CuentaBanc, CuentaProA, cuentaDepositosGastoA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text);
                         //   RGP.ShowDialog();

                        }
                        else if (txtnombre.Text == "Póliza Gastos Personales")
                        {
                            //  MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CDG: " + cuentaDepositosGasto + "CCB: " + CuentaBanc + " CCPA: " + CuentaProA + "CDGA: " + cuentaDepositosGastoA + "CCBA: " + CuentaBancA + "Seprador: " + TSeparador.Text + "C fija A" + lblContatenaCuentaContablesA.Text);
                           // ReporteGastoPersonalesGasto RGP = new ReporteGastoPersonalesGasto(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, cuentaDepositosGasto, CuentaBanc, CuentaProA, cuentaDepositosGastoA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text);
                          //  RGP.ShowDialog();
                        }


                    }
                    else if (TipoPolz == "Polizas Compras")
                    {
                        CargaCuentaC();
                        CargaCuentaA();

                        if (lblCuentaA1.Text != string.Empty && lblCuentaC1.Text != string.Empty)
                        {
                            RegistrarCuentaContable();
                        }
                        if (TipoPolz == "Polizas Compras")
                        {
                            Grupo = "Definiciones Compras";
                        }

                        c.ConcatenaCuentasContables(lblContatenaCuentaContables, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, Grupo);
                        c.ConcatenaCuentasContablesA(lblContatenaCuentaContablesA, txtNoPoliza.Text, cmbclave.Text, txtnombre.Text, TSeparador.Text, Grupo);

                        if (lblContatenaCuentaContables.Text == "CCP" || lblContatenaCuentaContables.Text == "CCD" || lblContatenaCuentaContables.Text == "CCI" || lblContatenaCuentaContables.Text == "CCB")
                        {
                            lblContatenaCuentaContables.Text = "";
                            lblContatenaCuentaContablesA.Text = "";
                        }
                        if (lblContatenaCuentaContables.Text == "*")
                        {
                            lblContatenaCuentaContables.Text = "";
                        }
                        if (lblContatenaCuentaContablesA.Text == "*")
                        {
                            lblContatenaCuentaContablesA.Text = "";
                        }


                        c.RegistroGeneracionPoliza(cmbclave.Text, txtnombre.Text, txtEstatus.Text, txtCargo1.Text, txtCargo2.Text, txtCargo3.Text, txtCargo4.Text, txtCargo5.Text, txtAbono1.Text, txtAbono2.Text, txtAbono3.Text, txtAbono4.Text, dtpPeriodo.Text, dtpFechaInicial.Text, dtpFechaInicial.Text, txtNoPoliza.Text, cmbTipoP.Text, txtDiarioP.Text, txtResumida.Text, txtXdias.Text, cmbdocumento.Text, txtConcepto.Text, cmbExportar.Text, TSeparador.Text, TipoPolz);
                        MessageBox.Show("Registro exitoso");

                        string DelDia = dtpFechaInicial.Text + " al " + dtpFechaFinal.Text;
                        string FechaI = dtpPeriodo.Text + "/" + dtpFechaInicial.Text;
                        string FechaF = dtpPeriodo.Text + "/" + dtpFechaFinal.Text;

                        string TipoPoliza = txtnombre.Text;
                        ConversionParametros();
                        if (cmbExportar.Text == "NO")
                        {
                            TSeparador.Text = "";
                        }
                        if (txtResumida.Text == "NO")
                        {
                            // MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CCD: " + CuentaDoc + "CCB: " + CuentaBanc + "CCA: " + CuentaAlmacen + "CCMI: " + CuentaMovimientoInv + "CCPV: " + Cuentaproveedores + "CCPS: " + CuentaProductoServ + "CCCG: " + CuentaConceptosGlobales + " CCPA: " + CuentaProA + "CCD: " + CuentaDocA + "CCBA: " + CuentaBancA + "CCA: " + CuentaAlmacenA + "CCMI: " + CuentaMovimientoInvA + "CCPVA: " + CuentaproveedoresA + "CCPSA: " + CuentaProductoServA + "CCCG: " + CuentaConceptosGlobalesA + "Separador: " + TSeparador.Text + "C fija A: " + lblContatenaCuentaContablesA.Text);
                            //     MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CCD: " + CuentaDoc + "CCB: " + CuentaBanc  + "CCA: " + CuentaAlmacen + "CCMI: " + CuentaMovimientoInv + "CCPV: " + Cuentaproveedores + "CCPS: " + CuentaProductoServ + "CCCG: " + CuentaConceptosGlobales + " CCPA: " + CuentaProA + "CCD: " + CuentaDocA + "CCBA: " + CuentaBancA + "CCA: " + CuentaAlmacenA + "CCMI: " + CuentaMovimientoInvA + " CCPVA: " + CuentaproveedoresA  +" CCPSA: " + CuentaProductoServA + "CCCG: " + CuentaConceptosGlobalesA + "Separador: " + TSeparador.Text + "C fija A: " + lblContatenaCuentaContablesA.Text);
                          /*pendiente por agregar */

                            // PolizaComprasDetalle GPC = new PolizaComprasDetalle(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, TSeparador.Text);
                        //    GPC.ShowDialog();


                        }
                        else if (txtResumida.Text == "SI")
                        {
                            //  MessageBox.Show(dtpPeriodo.Text);
                            //  MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CCD: " + CuentaDoc + "CCB: " + CuentaBanc  + "CCA: " + CuentaAlmacen + "CCMI: " + CuentaMovimientoInv + "CCPV: " + Cuentaproveedores + "CCPS: " + CuentaProductoServ + "CCCG: " + CuentaConceptosGlobales + " CCPA: " + CuentaProA + "CCD: " + CuentaDocA + "CCBA: " + CuentaBancA + "CCA: " + CuentaAlmacenA + "CCMI: " + CuentaMovimientoInvA + " CCPVA: " + CuentaproveedoresA  +" CCPSA: " + CuentaProductoServA + "CCCG: " + CuentaConceptosGlobalesA + "Separador: " + TSeparador.Text + "C fija A: " + lblContatenaCuentaContablesA.Text);
                            if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                            {
                                //       MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CCD: " + CuentaDoc + "CCB: " + CuentaBanc  + "CCA: " + CuentaAlmacen + "CCMI: " + CuentaMovimientoInv + "CCPV: " + Cuentaproveedores + "CCPS: " + CuentaProductoServ + "CCCG: " + CuentaConceptosGlobales + " CCPA: " + CuentaProA + "CCD: " + CuentaDocA + "CCBA: " + CuentaBancA + "CCA: " + CuentaAlmacenA + "CCMI: " + CuentaMovimientoInvA + " CCPVA: " + CuentaproveedoresA  +" CCPSA: " + CuentaProductoServA + "CCCG: " + CuentaConceptosGlobalesA + "Separador: " + TSeparador.Text + "C fija A: " + lblContatenaCuentaContablesA.Text);

                             //   PolizaComprasAnticipos GPC = new PolizaComprasAnticipos(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, CuentaIng, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, CuentaIngA, TSeparador.Text);

                              //  GPC.ShowDialog();
                            }
                            else
                            {
                                //   MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CCD: " + CuentaDoc + "CCB: " + CuentaBanc + "CCA: " + CuentaAlmacen + "CCMI: " + CuentaMovimientoInv + "CCPV: " + Cuentaproveedores + "CCPS: " + CuentaProductoServ + "CCCG: " + CuentaConceptosGlobales + " CCPA: " + CuentaProA + "CCD: " + CuentaDocA + "CCBA: " + CuentaBancA + "CCA: " + CuentaAlmacenA + "CCMI: " + CuentaMovimientoInvA + " CCPVA: " + CuentaproveedoresA + " CCPSA: " + CuentaProductoServA + "CCCG: " + CuentaConceptosGlobalesA + "Separador: " + TSeparador.Text + "C fija A: " + lblContatenaCuentaContablesA.Text);

                                //PolizaCompras1 GPC = new PolizaCompras1(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, TSeparador.Text);
                                //GPC.ShowDialog();
                            }
                        }

                    }

                    CuentaPro = string.Empty;
                    CuentaDoc = string.Empty;
                    CuentaIng = string.Empty;
                    CuentaBanc = string.Empty;
                    cuentaDepositosGasto = string.Empty;
                    CuentaProA = string.Empty;
                    CuentaDocA = string.Empty;
                    CuentaIngA = string.Empty;
                    CuentaBancA = string.Empty;
                    cuentaDepositosGastoA = string.Empty;
                }
            }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void CargaCuentaC()
        {
            if (txtCargo1.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC1.Text = "CCP";
            }
            else if (txtCargo1.Text == "Cuenta Contable Documento")
            {
                // c.consultaCuentaDocumento(lblCuentaC1,cmbdocumento.Text);
                lblCuentaC1.Text = "CCD";
            }
            else if (txtCargo1.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaC1);
                lblCuentaC1.Text = "CCI";
            }
            else if (txtCargo1.Text == "Cuenta Contable Bancos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC1.Text = "CCB";
            }
            else if (txtCargo1.Text == "Cuenta Contable Fija")
            {
                /*  String value = txtCargo1.Text;
                  int startIndex = 21;
                  int length = 10;*/
                lblCuentaC1.Text = txtCargo1.Text; // value.Substring(startIndex, length);
                lblContatenaCuentaContables.Text = txtCargo1.Text;
            }
            else if (txtCargo1.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC1.Text = "CDG";
            }
            else if (txtCargo1.Text == "Cuenta Contable Almacenes")

            {
                lblCuentaC1.Text = "CCA";
            }
            else if (txtCargo1.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC1.Text = "CCMI";
            }
            else if (txtCargo1.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC1.Text = "CCPV";
            }
            else if (txtCargo1.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC1.Text = "CCPS";
            }
            else if (txtCargo1.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC1.Text = "CCCG";
            }
            else if (txtCargo1.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC1.Text = "CCCC";
            }
            //////----------
            if (txtCargo2.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC2.Text = "CCP";
            }
            else if (txtCargo2.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaC2, cmbdocumento.Text);
                lblCuentaC2.Text = "CCD";
            }
            else if (txtCargo2.Text == " Cuenta Contable Concepto Ingreso")
            {
                //   c.consultaCuentaConceptosIngresos(lblCuentaC2);
                lblCuentaC2.Text = "CCI";
            }
            else if (txtCargo2.Text == "Cuenta Contable Bancos")
            {
                // c.consultaCuentaBanco(lblCuentaC2);
                lblCuentaC2.Text = "CCB";
            }
            else if (txtCargo2.Text == "Cuenta Contable Fija")
            {
                lblCuentaC2.Text = txtCargo2.Text;
                lblContatenaCuentaContables.Text = txtCargo2.Text;
            }
            else if (txtCargo2.Text == "Catalogo Depósitos y Gastos")
            {
                lblCuentaC2.Text = txtCargo2.Text;
            }
            else if (txtCargo2.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC2.Text = "CDG";
            }
            else if (txtCargo2.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC2.Text = "CCA";
            }
            else if (txtCargo2.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC2.Text = "CCMI";
            }
            else if (txtCargo2.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC2.Text = "CCPV";
            }
            else if (txtCargo2.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC2.Text = "CCPS";
            }
            else if (txtCargo2.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC2.Text = "CCCG";
            }
            else if (txtCargo2.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC2.Text = "CCCC";
            }

            //////----------
            if (txtCargo3.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC3.Text = "CCP";
            }
            else if (txtCargo3.Text == "Cuenta Contable Documento")
            {
                //   c.consultaCuentaDocumento(lblCuentaC3, cmbdocumento.Text);
                lblCuentaC3.Text = "CCD";
            }
            else if (txtCargo3.Text == " Cuenta Contable Concepto Ingreso")
            {
                //   c.consultaCuentaConceptosIngresos(lblCuentaC3);
                lblCuentaC3.Text = "CCI";
            }
            else if (txtCargo3.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC3);
                lblCuentaC3.Text = "CCB";
            }
            else if (txtCargo3.Text == "Cuenta Contable Fija")
            {
                /*    String value = txtCargo3.Text;
                    int startIndex = 21;
                    int length = 10;
                    lblCuentaC3.Text = value.Substring(startIndex, length);*/

                lblCuentaC3.Text = txtCargo3.Text;
                lblContatenaCuentaContables.Text = txtCargo3.Text;
            }
            else if (txtCargo3.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC3.Text = "CDG";
            }

            else if (txtCargo3.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC3.Text = "CCA";
            }
            else if (txtCargo3.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC3.Text = "CCMI";
            }
            else if (txtCargo3.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC3.Text = "CCPV";
            }
            else if (txtCargo3.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC3.Text = "CCPS";
            }
            else if (txtCargo3.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC3.Text = "CCCG";
            }
            else if (txtCargo3.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC3.Text = "CCCC";
            }

            //////----------
            if (txtCargo4.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC4.Text = "CCP";
            }
            else if (txtCargo4.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaC4, cmbdocumento.Text);
                lblCuentaC4.Text = "CCD";
            }
            else if (txtCargo4.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaC4);
                lblCuentaC4.Text = "CCI";
            }
            else if (txtCargo4.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC4);
                lblCuentaC4.Text = "CCB";
            }
            else if (txtCargo4.Text == "Cuenta Contable Fija")
            {
                /*     String value = txtCargo4.Text;
                     int startIndex = 21;
                     int length = 10;
                     lblCuentaC4.Text = value.Substring(startIndex, length);*/
                lblCuentaC4.Text = txtCargo4.Text;
                lblContatenaCuentaContables.Text = txtCargo4.Text;
            }
            else if (txtCargo4.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC4.Text = "CDG";
            }
            else if (txtCargo4.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC4.Text = "CCA";
            }
            else if (txtCargo4.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC4.Text = "CCMI";
            }
            else if (txtCargo4.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC4.Text = "CCPV";
            }
            else if (txtCargo4.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC4.Text = "CCPS";
            }
            else if (txtCargo4.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC4.Text = "CCCG";
            }
            else if (txtCargo4.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC4.Text = "CCCC";
            }



            //////----------
            if (txtCargo5.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC5.Text = "CCP";
            }
            else if (txtCargo5.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaC5, cmbdocumento.Text);
                lblCuentaC5.Text = "CCD";
            }
            else if (txtCargo5.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaC5);
                lblCuentaC5.Text = "CCI";
            }
            else if (txtCargo5.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC5);
                lblCuentaC5.Text = "CCB";
            }
            else if (txtCargo5.Text == "Cuenta Contable Fija")
            {
                /*String value = txtCargo5.Text;
                int startIndex = 21;
                int length = 10;
                lblCuentaC5.Text = value.Substring(startIndex, length);*/
                lblCuentaC5.Text = txtCargo5.Text;
                lblContatenaCuentaContables.Text = txtCargo5.Text;
            }
            else if (txtCargo5.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC5.Text = "CDG";
            }
            else if (txtCargo5.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC5.Text = "CCA";
            }
            else if (txtCargo5.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC5.Text = "CCMI";
            }
            else if (txtCargo5.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC5.Text = "CCPV";
            }
            else if (txtCargo5.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC5.Text = "CCPS";
            }
            else if (txtCargo5.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC5.Text = "CCCG";
            }
            else if (txtCargo5.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC5.Text = "CCCC";
            }
        }

        public void CargaCuentaA()
        {

            if (txtAbono1.Text == "Cuenta Contable Propietario")
            {
                lblCuentaA1.Text = "CCPA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Documento")
            {
                //   c.consultaCuentaDocumento(lblCuentaA1, cmbdocumento.Text);
                lblCuentaA1.Text = "CCDA";
            }
            else if (txtAbono1.Text == " Cuenta Contable Concepto Ingreso")
            {
                //    c.consultaCuentaConceptosIngresos(lblCuentaA1);
                lblCuentaA1.Text = "CCIA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA1);
                lblCuentaA1.Text = "CCBA";
            }

            else if (txtAbono1.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaA1.Text = "CDGA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Fija")
            {

                lblCuentaA1.Text = txtAbono1.Text;
                lblContatenaCuentaContablesA.Text = txtAbono1.Text;
            }
            else if (txtAbono1.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaA1.Text = "CCAAB";
            }
            else if (txtAbono1.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaA1.Text = "CCMI";
            }
            else if (txtAbono1.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA1.Text = "CCPVA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA1.Text = "CCPSA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA1.Text = "CCCGA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA1.Text = "CCCCA";
            }
            //////----------
            if (txtAbono2.Text == "Cuenta Contable Propietario")
            {
                lblCuentaA2.Text = "CCPA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaA2, cmbdocumento.Text);
                lblCuentaA2.Text = "CCDA";
            }
            else if (txtAbono2.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaA2);
                lblCuentaA2.Text = "CCIA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA2);
                lblCuentaA2.Text = "CCBA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Fija")
            {
                /*   String value = txtAbono2.Text;
                   int startIndex = 21;
                   int length = 10;
                   lblCuentaA2.Text = value.Substring(startIndex, length);*/
                lblCuentaA2.Text = txtAbono2.Text;
                lblContatenaCuentaContablesA.Text = txtAbono2.Text;

            }
            else if (txtAbono2.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaA2.Text = "CDGA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaA2.Text = "CCAAB";
            }
            else if (txtAbono2.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaA2.Text = "CCMI";
            }
            else if (txtAbono2.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA2.Text = "CCPV";
            }
            else if (txtAbono2.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA2.Text = "CCPS";
            }
            else if (txtAbono2.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA2.Text = "CCCG";
            }
            else if (txtAbono2.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA2.Text = "CCCCA";
            }
            //////----------
            if (txtAbono3.Text == "Cuenta Contable Propietario")
            {
                lblCuentaA3.Text = "CCPA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaA3, cmbdocumento.Text);
                lblCuentaA3.Text = "CCDA";
            }
            else if (txtAbono3.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaA3);
                lblCuentaA3.Text = "CCIA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA3);
                lblCuentaA3.Text = "CCBA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Fija")
            {
                /*    String value = txtAbono3.Text;
                    int startIndex = 21;
                    int length = 10;
                    lblCuentaA3.Text = value.Substring(startIndex, length);*/
                lblCuentaA3.Text = txtAbono3.Text;
                lblContatenaCuentaContablesA.Text = txtAbono3.Text;
            }
            else if (txtAbono3.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaA3.Text = "CDGA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaA3.Text = "CCAAB";
            }
            else if (txtAbono3.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaA3.Text = "CCMI";
            }
            else if (txtAbono3.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA3.Text = "CCPV";
            }
            else if (txtAbono3.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA3.Text = "CCPS";
            }
            else if (txtAbono3.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA3.Text = "CCCG";
            }
            else if (txtAbono3.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA3.Text = "CCCCA";
            }
            //////----------
            if (txtAbono4.Text == "Cuenta Contable Propietario")
            {
                lblCuentaA4.Text = "CCPA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Documento")
            {
                //   c.consultaCuentaDocumento(lblCuentaA4, cmbdocumento.Text);
                lblCuentaA4.Text = "CCDA";
            }
            else if (txtAbono4.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaA4);
                lblCuentaA4.Text = "CCIA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA4);
                lblCuentaA4.Text = "CCBA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Fija")
            {
                /*   String value = txtAbono4.Text;
                   int startIndex = 21;
                   int length = 10;
                   lblCuentaA4.Text = value.Substring(startIndex, length);*/
                lblCuentaA4.Text = txtAbono4.Text;
                lblContatenaCuentaContablesA.Text = txtAbono4.Text;
            }
            else if (txtAbono4.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaA4.Text = "CDGA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaA4.Text = "CCAAB";
            }
            else if (txtAbono4.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaA4.Text = "CCMI";
            }
            else if (txtAbono4.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA4.Text = "CCPV";
            }
            else if (txtAbono4.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA4.Text = "CCPS";
            }
            else if (txtAbono4.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA4.Text = "CCCG";
            }
            else if (txtAbono4.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA4.Text = "CCCCA";
            }

        }
        public void RegistrarCuentaContable()
        {
            if (lblCuentaC1.Text != "*" || txtCargo1.Text != string.Empty || txtCargo1.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, txtCargo1.Text, "", txtNoPoliza.Text, lblCuentaC1.Text, TipoPolz);
            }
            if (lblCuentaC2.Text != "*" || txtCargo2.Text != string.Empty || txtCargo2.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, txtCargo2.Text, "", txtNoPoliza.Text, lblCuentaC2.Text, TipoPolz);
            }
            if (lblCuentaC3.Text != "*" || txtCargo3.Text != string.Empty || txtCargo3.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, txtCargo3.Text, "", txtNoPoliza.Text, lblCuentaC3.Text, TipoPolz);
            }
            if (lblCuentaC4.Text != "*" || txtCargo4.Text != string.Empty || txtCargo4.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, txtCargo4.Text, "", txtNoPoliza.Text, lblCuentaC4.Text, TipoPolz);
            }
            if (lblCuentaC5.Text != "*" || txtCargo5.Text != string.Empty || txtCargo5.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, txtCargo5.Text, "", txtNoPoliza.Text, lblCuentaC5.Text, TipoPolz);
            }

            if (lblCuentaA1.Text != "*" || txtAbono1.Text != string.Empty || txtAbono1.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, "", txtAbono1.Text, txtNoPoliza.Text, lblCuentaA1.Text, TipoPolz);
            }
            if (lblCuentaA2.Text != "*" || txtAbono2.Text != string.Empty || txtAbono2.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, "", txtAbono2.Text, txtNoPoliza.Text, lblCuentaA2.Text, TipoPolz);
            }
            if (lblCuentaA3.Text != "*" || txtAbono3.Text != string.Empty || txtAbono3.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, "", txtAbono3.Text, txtNoPoliza.Text, lblCuentaA3.Text, TipoPolz);
            }
            if (lblCuentaA4.Text != "*" || txtAbono4.Text != string.Empty || txtAbono4.Text != "")
            {
                c.RegistroCuentaContable(cmbclave.Text, txtnombre.Text, "", txtAbono4.Text, txtNoPoliza.Text, lblCuentaA4.Text, TipoPolz);
            }
        }
        private void ConversionParametros()
        {
            //para el parametro cuando sea Cuenta contable propietario
            if (lblCuentaC1.Text == "CCP")
            {
                CuentaPro = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCP")
            {
                CuentaPro = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCP")
            {
                CuentaPro = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCP")
            {
                CuentaPro = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCP")
            {
                CuentaPro = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCPA")
            {
                CuentaProA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCPA")
            {
                CuentaProA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCPA")
            {
                CuentaProA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCPA")
            {
                CuentaProA = lblCuentaA4.Text;
            }

            //para el parametro cuando sea Cuenta contable documentos
            if (lblCuentaC1.Text == "CCD")
            {
                CuentaDoc = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCD")
            {
                CuentaDoc = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCD")
            {
                CuentaDoc = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCD")
            {
                CuentaDoc = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCD")
            {
                CuentaDoc = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCDA")
            {
                CuentaDocA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCDA")
            {
                CuentaDocA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCDA")
            {
                CuentaDocA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCDA")
            {
                CuentaDocA = lblCuentaA4.Text;
            }
            //-----------
            if (lblCuentaC1.Text == "CCI")
            {
                CuentaIng = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCI")
            {
                CuentaIng = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCI")
            {
                CuentaIng = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCI")
            {
                CuentaIng = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCI")
            {
                CuentaIng = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCIA")
            {
                CuentaIngA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCIA")
            {
                CuentaIngA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCIA")
            {
                CuentaIngA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCA")
            {
                CuentaIngA = lblCuentaA4.Text;
            }
            //-----------
            if (lblCuentaC1.Text == "CCB")
            {
                CuentaBanc = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCB")
            {
                CuentaBanc = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCB")
            {
                CuentaBanc = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCB")
            {
                CuentaBanc = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCB")
            {
                CuentaBanc = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCBA")
            {
                CuentaBancA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCBA")
            {
                CuentaBancA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCBA")
            {
                CuentaBancA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCBA")
            {
                CuentaBancA = lblCuentaA4.Text;
            }
            //-----------
            if (lblCuentaC1.Text == "CDG")
            {
                cuentaDepositosGasto = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CDG")
            {
                cuentaDepositosGasto = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CDG")
            {
                cuentaDepositosGasto = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CDG")
            {
                cuentaDepositosGasto = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CDG")
            {
                cuentaDepositosGasto = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CDGA")
            {
                cuentaDepositosGastoA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CDGA")
            {
                cuentaDepositosGastoA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CDGA")
            {
                cuentaDepositosGastoA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CDGA")
            {
                cuentaDepositosGastoA = lblCuentaA4.Text;
            }
            //----------------CONVERSION DE PARAMETROS DE POLIZAS COMPRAS
            if (lblCuentaC1.Text == "CCA")
            {
                CuentaAlmacen = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCA")
            {
                CuentaAlmacen = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCA")
            {
                CuentaAlmacen = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCA")
            {
                CuentaAlmacen = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCA")
            {
                CuentaAlmacen = lblCuentaC5.Text;
            }

            if (lblCuentaC1.Text == "CCMI")
            {
                CuentaMovimientoInv = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCMI")
            {
                CuentaMovimientoInv = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCMI")
            {
                CuentaMovimientoInv = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCMI")
            {
                CuentaMovimientoInv = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCMI")
            {
                CuentaMovimientoInv = lblCuentaC5.Text;
            }
            if (lblCuentaC1.Text == "CCPV")
            {
                Cuentaproveedores = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCPV")
            {
                Cuentaproveedores = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCPV")
            {
                Cuentaproveedores = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCPV")
            {
                Cuentaproveedores = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCPV")
            {
                Cuentaproveedores = lblCuentaC5.Text;
            }
            if (lblCuentaC1.Text == "CCPS")
            {
                CuentaProductoServ = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCPS")
            {
                CuentaProductoServ = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCPS")
            {
                CuentaProductoServ = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCPS")
            {
                CuentaProductoServ = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCPS")
            {
                CuentaProductoServ = lblCuentaC5.Text;
            }
            if (lblCuentaC1.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC5.Text;
            }

            if (lblCuentaC1.Text == "CCCC")
            {
                CuentaCentroCosto = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCCC")
            {
                CuentaCentroCosto = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCCC")
            {
                CuentaCentroCosto = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCCC")
            {
                CuentaCentroCosto = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCCC")
            {
                CuentaCentroCosto = lblCuentaC5.Text;
            }

            //-----------------
            if (lblCuentaA1.Text == "CCAAB")
            {
                CuentaAlmacenA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCAAB")
            {
                CuentaAlmacenA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCAAB")
            {
                CuentaAlmacenA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCAAB")
            {
                CuentaAlmacenA = lblCuentaC4.Text;
            }


            if (lblCuentaA1.Text == "CCMIA")
            {
                CuentaConceptosGlobalesA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCMIA")
            {
                CuentaConceptosGlobalesA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCMIA")
            {
                CuentaConceptosGlobalesA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCMIA")
            {
                CuentaConceptosGlobalesA = lblCuentaA4.Text;
            }

            if (lblCuentaA1.Text == "CCPVA")
            {
                CuentaproveedoresA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCPVA")
            {
                CuentaproveedoresA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCPVA")
            {
                CuentaproveedoresA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCPVA")
            {
                CuentaproveedoresA = lblCuentaA4.Text;
            }

            if (lblCuentaA1.Text == "CCPSA")
            {
                CuentaProductoServA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCPSA")
            {
                CuentaProductoServA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCPSA")
            {
                CuentaProductoServA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCPSA")
            {
                CuentaProductoServA = lblCuentaA4.Text;
            }

            if (lblCuentaA1.Text == "CCCGA")
            {
                CuentaConceptosGlobalesA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCCGA")
            {
                CuentaConceptosGlobalesA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCCGA")
            {
                CuentaConceptosGlobalesA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCCGA")
            {
                CuentaConceptosGlobalesA = lblCuentaA4.Text;
            }


            if (lblCuentaA1.Text == "CCCCA")
            {
                CuentaCentroCosto = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCCCA")
            {
                CuentaCentroCosto = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCCCA")
            {
                CuentaCentroCosto = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCCCA")
            {
                CuentaCentroCosto = lblCuentaA4.Text;
            }


        }

    }
}


