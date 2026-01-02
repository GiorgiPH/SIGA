using Condominios.Clases.GenerarRecibo;
using PuntoVentas;
using PV.Clases.Polizas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV
{
    public partial class DefinePoliza : Form
    {

        DBPolizas c = new DBPolizas();

        string TipoPolz = string.Empty;
        public static string TipopolizaCompras = string.Empty;
        public DefinePoliza(string TipoPoliza)
        {
            InitializeComponent();
            TipoPolz = TipoPoliza;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "NUEVO")
            {            
              /*  guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                NuevaPoliza();
                groupBox2.Enabled = true;
                cmbclave.SelectedIndex = 0;*/
            }
         /*   else if (e.ClickedItem.Text == "CONSULTAR")
            {
               /* if (groupBox1.Visible == true)
                {
                    groupBox1.Enabled = true;
                    groupBox1.Visible = false;
                    groupBox1.SendToBack();
                }
                else
                {
                    groupBox1.Enabled = true;
                    groupBox1.Visible = true;
                    groupBox1.BringToFront();
                }*/
             /*   guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;

                if (TipoPolz == "Definiciones Condominios")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;

                    }
                    else
                    {

                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPoliza(dataGridView1);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }
                else if (TipoPolz == "Definiciones Gastos Personales")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;

                    }
                    else
                    {

                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPolizaGP(dataGridView1);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }
                else if (TipoPolz == "Definiciones Compras")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;
                    }
                    else
                    {
                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPolizaCompras(dataGridView1, TipopolizaCompras);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }

            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2GradientPanel4.Size = new Size(22, 569);
                toolStrip1.Size = new Size(22, 569);
                toolStripButton11.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton12.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
                toolStripButton13.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            }
        */
        }

        private void cmbclave_SelectedIndexChanged(object sender, EventArgs e)
        {
            int clave = Convert.ToInt16(cmbclave.Text);
            c.Catalogo_DefPoliza2(clave, TipoPolz, txtnombre);
        }

        private void btnarticulos_Click(object sender, EventArgs e)
        {
            if (cmbclave.Text == "")
            {
                MessageBox.Show("Se debe selecionar la clave del tipo de poliza a agregar");
            }
            else if (cmbTpoliza.Text == "")
            {
                MessageBox.Show("Se debe seleccionar el tipo de poliza");
            }
            else if (cmbcargo1.Text == "")
            {
                MessageBox.Show("Se debe seleccionar un cargo");
            }
            else if (cmbabono1.Text == "")
            {
                MessageBox.Show("Se debe seleccionar un abono");
            }
            else
            {

                if (cmbcargo1.Text == "Cuenta Contable Fija")
                {
                    cmbcargo1.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo1.Text;
                }
                else if (cmbcargo2.Text == "Cuenta Contable Fija")
                {
                    cmbcargo2.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo2.Text;
                }
                else if (cmbcargo3.Text == "Cuenta Contable Fija")
                {
                    cmbcargo3.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo3.Text;
                }
                else if (cmbcargo4.Text == "Cuenta Contable Fija")
                {
                    cmbcargo4.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo4.Text;
                }
                else if (cmbcargo5.Text == "Cuenta Contable Fija")
                {
                    cmbcargo5.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo5.Text;
                }
                else if (cmbcargo7.Text == "Cuenta Contable Fija")
                {
                    cmbcargo7.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo5.Text;
                }
                else if (cmbcargo8.Text == "Cuenta Contable Fija")
                {
                    cmbcargo8.Text = cmbcargo6.Text;// + textBox10.Text + cmbcargo5.Text;
                }
                else if (cmbabono1.Text == "Cuenta Contable Fija")
                {
                    cmbabono1.Text = cmbabono5.Text;// + textBox10.Text + cmbabono1.Text;
                }
                else if (cmbabono2.Text == "Cuenta Contable Fija")
                {
                    cmbabono2.Text = cmbabono5.Text;// + textBox10.Text + cmbabono2.Text;
                }
                else if (cmbabono3.Text == "Cuenta Contable Fija")
                {
                    cmbabono3.Text = cmbabono5.Text;// + textBox10.Text + cmbabono3.Text;
                }
                else if (cmbabono4.Text == "Cuenta Contable Fija")
                {
                    cmbabono4.Text = cmbabono5.Text;// + textBox10.Text + cmbabono4.Text;
                }


                c.RegistroDefPoliza(cmbclave.Text, txtnombre.Text, cmbestatus.Text, txtseparador.Text, textBox10.Text, cmbTpoliza.Text, txtDpoliza.Text, txtCargo1.Text, cmbcargo1.Text, txtCargo2.Text, cmbcargo2.Text, txtCargo3.Text, cmbcargo3.Text, txtCargo4.Text, cmbcargo4.Text, txtCargo5.Text, cmbcargo5.Text, txtCargo7.Text, cmbcargo7.Text, txtCargo8.Text, cmbcargo8.Text, txtAbono1.Text, cmbabono1.Text, txtAbono2.Text, cmbabono2.Text, txtAbono3.Text, cmbabono3.Text, txtAbono4.Text, cmbabono4.Text, rdtNotas.Text, TipoPolz);
                MessageBox.Show("Registro Guardado");
                //   cmbclave.Text = "";
                txtnombre.Text = "";

                cmbTpoliza.Text = "";
                txtDpoliza.Text = "";
                txtCargo1.Text = "";
                cmbcargo1.Text = "";
                txtCargo2.Text = "";
                cmbcargo2.Text = "";
                txtCargo3.Text = "";
                cmbcargo3.Text = "";
                txtCargo4.Text = "";
                cmbcargo4.Text = "";
                txtCargo5.Text = "";
                cmbcargo5.Text = "";
                txtAbono1.Text = "";
                cmbabono1.Text = "";
                txtAbono2.Text = "";
                cmbabono2.Text = "";
                txtAbono3.Text = "";
                cmbabono3.Text = "";
                txtAbono4.Text = "";
                cmbabono4.Text = "";
                rdtNotas.Text = "";
                cmbcargo6.Text = "";
                cmbabono5.Text = "";
                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;
                txtnombre.Text = string.Empty;
                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;
              //  groupBox2.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtnombre.Text = "";

            cmbTpoliza.Text = "";
            txtDpoliza.Text = "";
            txtCargo1.Text = "";
            cmbcargo1.Text = "";
            txtCargo2.Text = "";
            cmbcargo2.Text = "";
            txtCargo3.Text = "";
            cmbcargo3.Text = "";
            txtCargo4.Text = "";
            cmbcargo4.Text = "";
            txtCargo5.Text = "";
            cmbcargo5.Text = "";
            txtAbono1.Text = "";
            cmbabono1.Text = "";
            txtAbono2.Text = "";
            cmbabono2.Text = "";
            txtAbono3.Text = "";
            cmbabono3.Text = "";
            txtAbono4.Text = "";
            cmbabono4.Text = "";
            rdtNotas.Text = "";
            txtCargo1.Enabled = false;
            txtCargo2.Enabled = false;
            txtCargo3.Enabled = false;
            txtCargo4.Enabled = false;
            txtCargo5.Enabled = false;
            txtAbono1.Enabled = false;
            txtAbono2.Enabled = false;
            txtAbono3.Enabled = false;
            txtAbono4.Enabled = false;


            cmbcargo2.Enabled = false;
            cmbcargo3.Enabled = false;
            cmbcargo4.Enabled = false;
            cmbcargo5.Enabled = false;

            cmbabono2.Enabled = false;
            cmbabono3.Enabled = false;
            cmbabono4.Enabled = false;
            txtnombre.Enabled = false;
            MessageBox.Show("Para crear una nueva definicion se debe seleccionar el boton nuevo, para salir precione el boton de 'Menu Principal'");
        }

        void NuevaPoliza()
        {


            if (TipoPolz == "Definiciones Condominios")
            {

                //                cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonos(cmbcargo1);
                c.Catalogo_cargosyabonos(cmbcargo2);
                c.Catalogo_cargosyabonos(cmbcargo3);
                c.Catalogo_cargosyabonos(cmbcargo4);
                c.Catalogo_cargosyabonos(cmbcargo5);
                c.Catalogo_cargosyabonos(cmbabono1);
                c.Catalogo_cargosyabonos(cmbabono2);
                c.Catalogo_cargosyabonos(cmbabono3);
                c.Catalogo_cargosyabonos(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;
                txtnombre.Items.Remove("Póliza Depósitos");
                txtnombre.Items.Remove("Póliza Gastos Personales");
            }
            if (TipoPolz == "Definiciones Gastos Personales")
            {

                // cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPolizaGP(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonosGP(cmbcargo1);
                c.Catalogo_cargosyabonosGP(cmbcargo2);
                c.Catalogo_cargosyabonosGP(cmbcargo3);
                c.Catalogo_cargosyabonosGP(cmbcargo4);
                c.Catalogo_cargosyabonosGP(cmbcargo5);
                c.Catalogo_cargosyabonosGP(cmbabono1);
                c.Catalogo_cargosyabonosGP(cmbabono2);
                c.Catalogo_cargosyabonosGP(cmbabono3);
                c.Catalogo_cargosyabonosGP(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;

                txtnombre.Items.Remove("Define Póliza Recibos");
                txtnombre.Items.Remove("Define Póliza Cobranza /ingresos");
                txtnombre.Items.Remove("Define Póliza Recargos");
                txtnombre.Items.Remove("Define Póliza Descuentos");
                txtnombre.Items.Remove("Define Póliza Anticipos");
                txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
            }
            if (TipoPolz == "Definiciones Compras")
            {

                //    cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPolizaCompras(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonosCompras(cmbcargo1);
                c.Catalogo_cargosyabonosCompras(cmbcargo2);
                c.Catalogo_cargosyabonosCompras(cmbcargo3);
                c.Catalogo_cargosyabonosCompras(cmbcargo4);
                c.Catalogo_cargosyabonosCompras(cmbcargo5);
                c.Catalogo_cargosyabonosCompras(cmbabono1);
                c.Catalogo_cargosyabonosCompras(cmbabono2);
                c.Catalogo_cargosyabonosCompras(cmbabono3);
                c.Catalogo_cargosyabonosCompras(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;
                txtnombre.Items.Remove("Define Póliza Recibos");
                txtnombre.Items.Remove("Define Póliza Cobranza /ingresos");
                txtnombre.Items.Remove("Define Póliza Recargos");
                txtnombre.Items.Remove("Define Póliza Descuentos");
                txtnombre.Items.Remove("Define Póliza Anticipos");
                txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                txtnombre.Items.Remove("Póliza Depósitos");
                txtnombre.Items.Remove("Póliza Gastos Personales");


            }
            if (TipoPolz == "Definiciones Condominios")
            {
                textBox1.Text = "";
                txtnombre.Text = "";
                cmbTpoliza.Text = "";
                txtDpoliza.Text = "";
                txtCargo1.Text = "";
                cmbcargo1.Text = "";
                txtCargo2.Text = "";
                cmbcargo2.Text = "";
                txtCargo3.Text = "";
                cmbcargo3.Text = "";
                txtCargo4.Text = "";
                cmbcargo4.Text = "";
                txtCargo5.Text = "";
                cmbcargo5.Text = "";
                txtAbono1.Text = "";
                cmbabono1.Text = "";
                txtAbono2.Text = "";
                cmbabono2.Text = "";
                txtAbono3.Text = "";
                cmbabono3.Text = "";
                txtAbono4.Text = "";
                cmbabono4.Text = "";
                rdtNotas.Text = "";


                groupBox2.Enabled = true;
                c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);

                //lblconsecutivo.Visible = true;
                //  MessageBox.Show("'" + lblconsecutivo.Text+ "'");
                if (lblconsecutivo.Text == string.Empty || lblconsecutivo.Text == "0" || lblconsecutivo.Text == "" || lblconsecutivo.Text == "*")
                {
                    lblconsecutivo.Text = "1";
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);

                    cmbclave.Text = Convert.ToString(Cons);

                }
                else
                {
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);

                    cmbclave.Text = Convert.ToString(Cons + 1);

                }
                button7.Enabled = true;
                button5.Enabled = false;

            }
            if (TipoPolz == "Definiciones Gastos Personales")
            {


                textBox1.Text = "";
                txtnombre.Text = "";
                cmbTpoliza.Text = "";
                txtDpoliza.Text = "";
                txtCargo1.Text = "";
                cmbcargo1.Text = "";
                txtCargo2.Text = "";
                cmbcargo2.Text = "";
                txtCargo3.Text = "";
                cmbcargo3.Text = "";
                txtCargo4.Text = "";
                cmbcargo4.Text = "";
                txtCargo5.Text = "";
                cmbcargo5.Text = "";
                txtAbono1.Text = "";
                cmbabono1.Text = "";
                txtAbono2.Text = "";
                cmbabono2.Text = "";
                txtAbono3.Text = "";
                cmbabono3.Text = "";
                txtAbono4.Text = "";
                cmbabono4.Text = "";
                rdtNotas.Text = "";
                groupBox2.Enabled = true;
                c.Catalogo_DefPolizaGP(TipoPolz, lblconsecutivo);

                //lblconsecutivo.Visible = true;
                //  MessageBox.Show("'" + lblconsecutivo.Text+ "'");
                if (lblconsecutivo.Text == string.Empty || lblconsecutivo.Text == "0" || lblconsecutivo.Text == "" || lblconsecutivo.Text == "*")
                {
                    lblconsecutivo.Text = "1";
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);

                    cmbclave.Text = Convert.ToString(Cons);

                }
                else
                {
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);

                    cmbclave.Text = Convert.ToString(Cons + 1);

                }


            }
            if (TipoPolz == "Definiciones Compras")
            {
                textBox1.Text = "";
                txtnombre.Text = "";
                cmbTpoliza.Text = "";
                txtDpoliza.Text = "";
                txtCargo1.Text = "";
                cmbcargo1.Text = "";
                txtCargo2.Text = "";
                cmbcargo2.Text = "";
                txtCargo3.Text = "";
                cmbcargo3.Text = "";
                txtCargo4.Text = "";
                cmbcargo4.Text = "";
                txtCargo5.Text = "";
                cmbcargo5.Text = "";
                txtAbono1.Text = "";
                cmbabono1.Text = "";
                txtAbono2.Text = "";
                cmbabono2.Text = "";
                txtAbono3.Text = "";
                cmbabono3.Text = "";
                txtAbono4.Text = "";
                cmbabono4.Text = "";
                rdtNotas.Text = "";
                groupBox2.Enabled = true;
                c.Catalogo_DefPolizaCompras(TipoPolz, lblconsecutivo);
                MessageBox.Show("" + lblconsecutivo.Text);
              
                if (Convert.ToInt32(lblconsecutivo.Text) <= 1)
                { 

                if (lblconsecutivo.Text == string.Empty || lblconsecutivo.Text == "0" || lblconsecutivo.Text == "" || lblconsecutivo.Text == "*")
                {
                 
                    lblconsecutivo.Text = "1";
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);
                    cmbclave.Items.Add(Cons);
                }
                }
                else if (Convert.ToInt32(lblconsecutivo.Text) > 1)
                {
                    int Cons = Convert.ToInt32(lblconsecutivo.Text);
                    MessageBox.Show("" + Convert.ToString(Cons + 1));
                    cmbclave.Text = Convert.ToString(Cons + 1);
                    cmbclave.Items.Add(Cons);
                
                }

                button7.Enabled = true;
                button5.Enabled = false;

                if (TipopolizaCompras == "Compras")
                {
                    cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
                    cmbcargo1.Items.Remove("Cuenta Contable Fija");
                    cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                    cmbcargo2.Items.Remove("Cuenta Contable Fija");
                    cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                    cmbcargo3.Items.Remove("Cuenta Contable Fija");
                    cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                    cmbcargo4.Items.Remove("Cuenta Contable Fija");
                    cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
                    cmbcargo5.Items.Remove("Cuenta Contable Fija");

                    txtnombre.Items.Remove("Define Póliza Recibos");
                    txtnombre.Items.Remove("Define Póliza Cobranza /ingresos");
                    txtnombre.Items.Remove("Define Póliza Recargos");
                    txtnombre.Items.Remove("Define Póliza Descuentos");
                    txtnombre.Items.Remove("Define Póliza Anticipos");
                    txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                    txtnombre.Items.Remove("Póliza Depósitos");
                    txtnombre.Items.Remove("Póliza Gastos Personales");


                    txtnombre.Items.Remove("Define Póliza Egresos");
                    txtnombre.Items.Remove("Define Póliza Anticipos");
                    txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                    txtnombre.Items.Remove("Define Póliza Salidas Almacén");


                }
            }

        }

        void ConsultarPoliza()
        {

            if (TipoPolz == "Definiciones Condominios")
            {
                if (groupBox1.Visible == true)
                {
                    groupBox1.Visible = false;

                }
                else
                {

                    groupBox1.Visible = true;
                    groupBox2.Enabled = true;
                    c.Consultar_DefinicionesPoliza(dataGridView1);
                    button7.Enabled = false;
                    button5.Enabled = true;
                }
            }
            else if (TipoPolz == "Definiciones Gastos Personales")
            {
                if (groupBox1.Visible == true)
                {
                    groupBox1.Visible = false;

                }
                else
                {

                    groupBox1.Visible = true;
                    groupBox2.Enabled = true;
                    c.Consultar_DefinicionesPolizaGP(dataGridView1);
                    button7.Enabled = false;
                    button5.Enabled = true;
                }
            }
            else if (TipoPolz == "Definiciones Compras")
            {
                if (groupBox1.Visible == true)
                {
                    groupBox1.Visible = false;
                }
                else
                {
                    groupBox1.Visible = true;
                    groupBox2.Enabled = true;
                    c.Consultar_DefinicionesPolizaCompras(dataGridView1, TipopolizaCompras);
                    button7.Enabled = false;
                    button5.Enabled = true;
                }
            }
        }
        private void cmbabono4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbabono4.Text;

            c.Catalogo_cargosyabonos2(cb, txtAbono4);
            if (cmbabono4.Text == "Cuenta Contable Fija")
            {
                cmbabono4.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbabono5.Visible = true;
            }
            else
            {
                cmbabono4.BackColor = Color.White;
                cmbabono5.Visible = false;
            }
            //actualizarListaAbonos();
            if (cmbabono4.Text == "Cuenta Contable Propietario")
            {
                cmbabono1.Items.Remove("Cuenta Contable Propietario");
                cmbabono2.Items.Remove("Cuenta Contable Propietario");
                cmbabono3.Items.Remove("Cuenta Contable Propietario");

            }
            if (cmbabono4.Text == "Cuenta Contable Documento")
            {
                cmbabono1.Items.Remove("Cuenta Contable Documento");
                cmbabono2.Items.Remove("Cuenta Contable Documento");
                cmbabono3.Items.Remove("Cuenta Contable Documento");

            }
            if (cmbabono4.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbabono1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono3.Items.Remove(" Cuenta Contable Concepto Ingreso");

            }
            if (cmbabono4.Text == "Cuenta Contable Bancos")
            {
                cmbabono1.Items.Remove("Cuenta Contable Bancos");
                cmbabono2.Items.Remove("Cuenta Contable Bancos");
                cmbabono3.Items.Remove("Cuenta Contable Bancos");

            }

            if (cmbabono4.Text == "Cuenta Contable Fija")
            {

                cmbabono1.Items.Remove("Cuenta Contable Fija");
                cmbabono2.Items.Remove("Cuenta Contable Fija");
                cmbabono3.Items.Remove("Cuenta Contable Fija");
            }
            else if (cmbabono4.Text == "Cuenta Contable Almacenes")
            {
                cmbabono2.Items.Remove("Cuenta Contable Almacenes");
                cmbabono3.Items.Remove("Cuenta Contable Almacenes");
                cmbabono1.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbabono4.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono1.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbabono4.Text == "Cuenta Contable de Proveedores")
            {
                cmbabono2.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono3.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono1.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbabono4.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono1.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbabono4.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbabono2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono1.Items.Remove("Cuenta Contable Conceptos Globales");
            }

        }

        private void DefinePoliza_Load(object sender, EventArgs e)
        {

            if (TipoPolz == "Definiciones Condominios")
            {
                cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonos(cmbcargo1);
                c.Catalogo_cargosyabonos(cmbcargo2);
                c.Catalogo_cargosyabonos(cmbcargo3);
                c.Catalogo_cargosyabonos(cmbcargo4);
                c.Catalogo_cargosyabonos(cmbcargo5);
                c.Catalogo_cargosyabonos(cmbabono1);
                c.Catalogo_cargosyabonos(cmbabono2);
                c.Catalogo_cargosyabonos(cmbabono3);
                c.Catalogo_cargosyabonos(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;
                txtnombre.Items.Remove("Póliza Depósitos");
                txtnombre.Items.Remove("Póliza Gastos Personales");
                txtnombre.Items.Remove("Define Póliza Compras Almacén");
                txtnombre.Items.Remove("Define Póliza Compras Gastos");
                txtnombre.Items.Remove("Define Póliza Egresos");
                txtnombre.Items.Remove("Define Póliza Anticipos");
                txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                txtnombre.Items.Remove("Define Póliza Salidas Almacén");
            }


            if (TipoPolz == "Definiciones Gastos Personales")
            {

                cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPolizaGP(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonosGP(cmbcargo1);
                c.Catalogo_cargosyabonosGP(cmbcargo2);
                c.Catalogo_cargosyabonosGP(cmbcargo3);
                c.Catalogo_cargosyabonosGP(cmbcargo4);
                c.Catalogo_cargosyabonosGP(cmbcargo5);
                c.Catalogo_cargosyabonosGP(cmbabono1);
                c.Catalogo_cargosyabonosGP(cmbabono2);
                c.Catalogo_cargosyabonosGP(cmbabono3);
                c.Catalogo_cargosyabonosGP(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;

                txtnombre.Items.Remove("Define Póliza Recibos");
                txtnombre.Items.Remove("Define Póliza Cobranza /ingresos");
                txtnombre.Items.Remove("Define Póliza Recargos");
                txtnombre.Items.Remove("Define Póliza Descuentos");
                txtnombre.Items.Remove("Define Póliza Anticipos");
                txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                txtnombre.Items.Remove("Define Póliza Compras Almacén");
                txtnombre.Items.Remove("Define Póliza Compras Gastos");
                txtnombre.Items.Remove("Define Póliza Egresos");
                txtnombre.Items.Remove("Define Póliza Anticipos");
                txtnombre.Items.Remove("Define Póliza Aplicación Anticipos");
                txtnombre.Items.Remove("Define Póliza Salidas Almacén");
            }
            if (TipoPolz == "Definiciones Compras")
            {

                cmbestatus.Items.Insert(0, "Activo");
                cmbestatus.SelectedIndex = 0;
                txtnombre.DropDownStyle = ComboBoxStyle.DropDownList;

                c.Catalogo_DefPolizaCompras(TipoPolz, lblconsecutivo);
                c.Catalogo_cargosyabonosCompras(cmbcargo1);
                c.Catalogo_cargosyabonosCompras(cmbcargo2);
                c.Catalogo_cargosyabonosCompras(cmbcargo3);
                c.Catalogo_cargosyabonosCompras(cmbcargo4);
                c.Catalogo_cargosyabonosCompras(cmbcargo5);
                c.Catalogo_cargosyabonosCompras(cmbabono1);
                c.Catalogo_cargosyabonosCompras(cmbabono2);
                c.Catalogo_cargosyabonosCompras(cmbabono3);
                c.Catalogo_cargosyabonosCompras(cmbabono4);
                txtCargo1.Enabled = false;
                txtCargo2.Enabled = false;
                txtCargo3.Enabled = false;
                txtCargo4.Enabled = false;
                txtCargo5.Enabled = false;
                txtAbono1.Enabled = false;
                txtAbono2.Enabled = false;
                txtAbono3.Enabled = false;
                txtAbono4.Enabled = false;


                cmbcargo2.Enabled = false;
                cmbcargo3.Enabled = false;
                cmbcargo4.Enabled = false;
                cmbcargo5.Enabled = false;

                cmbabono2.Enabled = false;
                cmbabono3.Enabled = false;
                cmbabono4.Enabled = false;
                txtnombre.Items.Remove("Define Póliza Recibos");
                txtnombre.Items.Remove("Define Póliza Cobranza /ingresos");
                txtnombre.Items.Remove("Define Póliza Recargos");
                txtnombre.Items.Remove("Define Póliza Descuentos");

                txtnombre.Items.Remove("Póliza Depósitos");
                txtnombre.Items.Remove("Póliza Gastos Personales");


            }
            if (TipopolizaCompras == "Compras")
            {
                label8.Text = "CARGOS PROD Y SERVICIOS";
                cmbcargo6.Location = new Point(164, 439);
                c.Catalogo_cargosyabonosCompras(cmbcargo7);
                c.Catalogo_cargosyabonosCompras(cmbcargo8);


              //  label12.Visible = true;
             //   cmbcargo7.Visible = true;
            //    cmbcargo8.Visible = true;
              //  txtCargo7.Visible = true;
               // txtCargo8.Visible = true;
                cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo1.Items.Remove("Cuenta Contable Fija");
                cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo2.Items.Remove("Cuenta Contable Fija");
                cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo3.Items.Remove("Cuenta Contable Fija");
                cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo4.Items.Remove("Cuenta Contable Fija");
                cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo5.Items.Remove("Cuenta Contable Fija");

                cmbcargo7.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo7.Items.Remove("Cuenta Contable Documento");
                cmbcargo7.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo7.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo7.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo7.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo7.Items.Remove("Cuenta Contable Bancos");
                cmbcargo7.Items.Remove("Centro de Costos");

                cmbcargo8.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo8.Items.Remove("Cuenta Contable Documento");
                cmbcargo8.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo8.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo8.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo8.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo8.Items.Remove("Cuenta Contable Bancos");
                cmbcargo8.Items.Remove("Centro de Costos");
            }

            else
            {
                rdtNotas.Location = new Point(124, 407);
                rdtNotas.Size = new Size(760, 141);
                cmbcargo6.Visible = true;
            }
        }

        private void txtnombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtnombre.Text == "Póliza Depósitos")
            {
                cmbcargo1.Items.Remove("Catalogo Gastos");
                cmbcargo2.Items.Remove("Catalogo Gastos");
                cmbcargo3.Items.Remove("Catalogo Gastos");
                cmbcargo4.Items.Remove("Catalogo Gastos");
                cmbcargo5.Items.Remove("Catalogo Gastos");
                cmbabono1.Items.Remove("Catalogo Gastos");
                cmbabono2.Items.Remove("Catalogo Gastos");
                cmbabono3.Items.Remove("Catalogo Gastos");
                cmbabono4.Items.Remove("Catalogo Gastos");

            }
            if (txtnombre.Text == "Define Póliza Recibos" || txtnombre.Text == "Define Póliza Recargos")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo2.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");
                cmbabono1.Items.Remove("Cuenta Contable Bancos");
                cmbabono2.Items.Remove("Cuenta Contable Bancos");
                cmbabono3.Items.Remove("Cuenta Contable Bancos");
                cmbabono4.Items.Remove("Cuenta Contable Bancos");
            }
            if (txtnombre.Text == "Define Póliza Compras Almacén")
            {

                cmbcargo1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo4.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo5.Items.Remove("Cuenta Contable Movimientos Inventarios");


                cmbabono1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono4.Items.Remove("Cuenta Contable Movimientos Inventarios");



                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo2.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");

                cmbabono1.Items.Remove("Cuenta Contable Bancos");
                cmbabono2.Items.Remove("Cuenta Contable Bancos");
                cmbabono3.Items.Remove("Cuenta Contable Bancos");
                cmbabono4.Items.Remove("Cuenta Contable Bancos");


                cmbcargo1.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo2.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo3.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo4.Items.Remove("Cuenta Contable Propietarios");
                cmbcargo5.Items.Remove("Cuenta Contable Propietarios");

                cmbabono1.Items.Remove("Cuenta Contable Propietarios");
                cmbabono2.Items.Remove("Cuenta Contable Propietarios");
                cmbabono3.Items.Remove("Cuenta Contable Propietarios");
                cmbabono4.Items.Remove("Cuenta Contable Propietarios");

            }
        }

        private void cmbcargo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbcargo1.Text;

            c.Catalogo_cargosyabonos2(cb, txtCargo1);

            cmbcargo2.Enabled = true;

            if (cmbcargo1.Text == "Cuenta Contable Fija")
            {
                cmbcargo1.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbcargo6.Visible = true;
                // cmbcargo.BackColor = Color.Blue;
            }
            else
            {
                cmbcargo1.BackColor = Color.White;
                cmbcargo6.Visible = false;
            }

            //actualizarListacargos();
            if (cmbcargo1.Text == "Cuenta Contable Propietario")
            {
                /*cmbcargo2.Items.RemoveAt(0);
                cmbcargo3.Items.RemoveAt(0);
                cmbcargo4.Items.RemoveAt(0);
                cmbcargo5.Items.RemoveAt(0);*/
                cmbcargo2.Items.Remove("Cuenta Contable Propietario");
                cmbcargo3.Items.Remove("Cuenta Contable Propietario");
                cmbcargo4.Items.Remove("Cuenta Contable Propietario");
                cmbcargo5.Items.Remove("Cuenta Contable Propietario");


                //   MessageBox.Show("1 CCP");

            }
            else if (cmbcargo1.Text == "Cuenta Contable Documento")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Documento");
                cmbcargo3.Items.Remove("Cuenta Contable Documento");
                cmbcargo4.Items.Remove("Cuenta Contable Documento");
                cmbcargo5.Items.Remove("Cuenta Contable Documento");
                //  MessageBox.Show("1 CCD");
            }
            else if (cmbcargo1.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbcargo2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove(" Cuenta Contable Concepto Ingreso");
                //  MessageBox.Show("1 CCI");
            }
            else if (cmbcargo1.Text == "Cuenta Contable Bancos")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove("Cuenta Contable Concepto Ingreso");
                //  MessageBox.Show("1 CCB");
            }
            else if (cmbcargo1.Text == "Cuenta Contable Fija")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove("Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove("Cuenta Contable Concepto Ingreso");

            }
            else if (cmbcargo1.Text == "Cuenta Contable Almacenes")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo3.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo4.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo5.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbcargo1.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo4.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo5.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbcargo1.Text == "Cuenta Contable de Proveedores")
            {
                cmbcargo2.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo3.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo4.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo5.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbcargo1.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo4.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo5.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbcargo1.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbcargo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbcargo2.Text;

            c.Catalogo_cargosyabonos2(cb, txtCargo2);
            cmbcargo3.Enabled = true;
            if (cmbcargo2.Text == "Cuenta Contable Fija")
            {
                cmbcargo2.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbcargo6.Visible = true;
            }
            else
            {
                cmbcargo2.BackColor = Color.White;
                cmbcargo6.Visible = false;
            }
            //   actualizarListacargos();
            if (cmbcargo2.Text == "Cuenta Contable Propietario")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Propietario");
                cmbcargo3.Items.Remove("Cuenta Contable Propietario");
                cmbcargo4.Items.Remove("Cuenta Contable Propietario");
                cmbcargo5.Items.Remove("Cuenta Contable Propietario");
                //    MessageBox.Show("2 CCP");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Documento")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Documento");
                cmbcargo3.Items.Remove("Cuenta Contable Documento");
                cmbcargo4.Items.Remove("Cuenta Contable Documento");
                cmbcargo5.Items.Remove("Cuenta Contable Documento");
                //     MessageBox.Show("2 CCD");
            }
            else if (cmbcargo2.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbcargo1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove(" Cuenta Contable Concepto Ingreso");
                //  MessageBox.Show("2 CCI");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Bancos")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");
                //  MessageBox.Show("2 CCB");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Bancos")
            {

                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");
                //     MessageBox.Show("2 CCF");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Almacenes")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo3.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo4.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo5.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo4.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo5.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbcargo2.Text == "Cuenta Contable de Proveedores")
            {
                cmbcargo1.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo3.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo4.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo5.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo4.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo5.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbcargo2.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbcargo3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cb = cmbcargo3.Text;

            c.Catalogo_cargosyabonos2(cb, txtCargo3);
            cmbcargo4.Enabled = true;
            if (cmbcargo3.Text == "Cuenta Contable Fija")
            {
                cmbcargo3.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbcargo6.Visible = true;
            }
            else
            {
                cmbcargo3.BackColor = Color.White;
                cmbcargo6.Visible = false;
            }
            //actualizarListacargos();
            if (cmbcargo3.Text == "Cuenta Contable Propietario")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Propietario");
                cmbcargo2.Items.Remove("Cuenta Contable Propietario");
                cmbcargo4.Items.Remove("Cuenta Contable Propietario");
                cmbcargo5.Items.Remove("Cuenta Contable Propietario");
                //    MessageBox.Show("3 CCP");
            }
            if (cmbcargo3.Text == "Cuenta Contable Documento")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Documento");
                cmbcargo2.Items.Remove("Cuenta Contable Documento");
                cmbcargo4.Items.Remove("Cuenta Contable Documento");
                cmbcargo5.Items.Remove("Cuenta Contable Documento");
                //   MessageBox.Show("3 CCD");
            }
            if (cmbcargo3.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbcargo1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove(" Cuenta Contable Concepto Ingreso");
                //   MessageBox.Show("3 CCI");
            }
            if (cmbcargo3.Text == "Cuenta Contable Bancos")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo2.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");
                //   MessageBox.Show("3 CCB");
            }
            if (cmbcargo3.Text == "Cuenta Contable Fija")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Fija");
                cmbcargo2.Items.Remove("Cuenta Contable Fija");
                cmbcargo4.Items.Remove("Cuenta Contable Fija");
                cmbcargo5.Items.Remove("Cuenta Contable Fija");
                //  MessageBox.Show("3 CCF");
            }
            else if (cmbcargo3.Text == "Cuenta Contable Almacenes")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo2.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo4.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo5.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbcargo3.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo4.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo5.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbcargo3.Text == "Cuenta Contable de Proveedores")
            {
                cmbcargo1.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo2.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo4.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo5.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbcargo3.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo4.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo5.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbcargo3.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbcargo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbcargo4.Text;

            c.Catalogo_cargosyabonos2(cb, txtCargo4);
            cmbcargo5.Enabled = true;
            if (cmbcargo4.Text == "Cuenta Contable Fija")
            {
                cmbcargo4.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbcargo6.Visible = true;
            }
            else
            {
                cmbcargo4.BackColor = Color.White;
                cmbcargo6.Visible = false;
            }
            //actualizarListacargos();
            if (cmbcargo4.Text == "Cuenta Contable Propietario")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Propietario");
                cmbcargo3.Items.Remove("Cuenta Contable Propietario");
                cmbcargo2.Items.Remove("Cuenta Contable Propietario");
                cmbcargo5.Items.Remove("Cuenta Contable Propietario");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Documento")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Documento");
                cmbcargo3.Items.Remove("Cuenta Contable Documento");
                cmbcargo2.Items.Remove("Cuenta Contable Documento");
                cmbcargo5.Items.Remove("Cuenta Contable Documento");
            }
            else if (cmbcargo4.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbcargo1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo5.Items.Remove(" Cuenta Contable Concepto Ingreso");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Bancos")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                cmbcargo2.Items.Remove("Cuenta Contable Bancos");
                cmbcargo5.Items.Remove("Cuenta Contable Bancos");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Fija")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Fija");
                cmbcargo3.Items.Remove("Cuenta Contable Fija");
                cmbcargo2.Items.Remove("Cuenta Contable Fija");
                cmbcargo5.Items.Remove("Cuenta Contable Fija");

            }
            else if (cmbcargo4.Text == "Cuenta Contable Almacenes")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo3.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo1.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo5.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo5.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbcargo4.Text == "Cuenta Contable de Proveedores")
            {
                cmbcargo2.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo3.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo1.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo5.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo1.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo5.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbcargo4.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo5.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbcargo5_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cb = cmbcargo5.Text;

            c.Catalogo_cargosyabonos2(cb, txtCargo5);
            if (cmbcargo5.Text == "Cuenta Contable Fija")
            {
                cmbcargo5.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbcargo6.Visible = true;
            }
            else
            {
                cmbcargo5.BackColor = Color.White;
                cmbcargo6.Visible = false;
            }
            //actualizarListacargos();
            if (cmbcargo5.Text == "Cuenta Contable Propietario")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Propietario");
                cmbcargo2.Items.Remove("Cuenta Contable Propietario");
                cmbcargo4.Items.Remove("Cuenta Contable Propietario");
                cmbcargo3.Items.Remove("Cuenta Contable Propietario");
                //  MessageBox.Show("5 CCP");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Documento")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Documento");
                cmbcargo2.Items.Remove("Cuenta Contable Documento");
                cmbcargo4.Items.Remove("Cuenta Contable Documento");
                cmbcargo3.Items.Remove("Cuenta Contable Documento");
                //  MessageBox.Show("5 CCD");
            }
            else if (cmbcargo5.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbcargo1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo4.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbcargo3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                //  MessageBox.Show("5 CCI");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Bancos")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Bancos");
                cmbcargo2.Items.Remove("Cuenta Contable Bancos");
                cmbcargo4.Items.Remove("Cuenta Contable Bancos");
                cmbcargo3.Items.Remove("Cuenta Contable Bancos");
                // MessageBox.Show("5 CCB");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Fija")
            {
                cmbcargo1.Items.Remove("Cuenta Contable Fija");
                cmbcargo2.Items.Remove("Cuenta Contable Fija");
                cmbcargo4.Items.Remove("Cuenta Contable Fija");
                cmbcargo3.Items.Remove("Cuenta Contable Fija");
                // MessageBox.Show("5 CCF");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Almacenes")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo3.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo4.Items.Remove("Cuenta Contable Almacenes");
                cmbcargo1.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo4.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbcargo1.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbcargo5.Text == "Cuenta Contable de Proveedores")
            {
                cmbcargo2.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo3.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo4.Items.Remove("Cuenta Contable de Proveedores");
                cmbcargo1.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo4.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbcargo1.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbcargo5.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbcargo2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo4.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbcargo1.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbabono1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbabono1.Text;

            c.Catalogo_cargosyabonos2(cb, txtAbono1);
            cmbabono2.Enabled = true;

            if (cmbabono1.Text == "Cuenta Contable Fija")
            {
                cmbabono1.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbabono5.Visible = true;
                //  cmbabono5.BackColor = Color.Aqua;
            }
            else
            {
                cmbabono1.BackColor = Color.White;
                cmbabono5.Visible = false;
            }
            //actualizarListaAbonos();
            if (cmbabono1.Text == "Cuenta Contable Propietario")
            {
                cmbabono2.Items.Remove("Cuenta Contable Propietario");
                cmbabono3.Items.Remove("Cuenta Contable Propietario");
                cmbabono4.Items.Remove("Cuenta Contable Propietario");

            }
            if (cmbabono1.Text == "Cuenta Contable Documento")
            {
                cmbabono2.Items.Remove("Cuenta Contable Documento");
                cmbabono3.Items.Remove("Cuenta Contable Documento");
                cmbabono4.Items.Remove("Cuenta Contable Documento");

            }
            if (cmbabono1.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbabono2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono4.Items.Remove(" Cuenta Contable Concepto Ingreso");

            }
            if (cmbabono1.Text == "Cuenta Contable Fija")
            {
                cmbabono2.Items.Remove("Cuenta Contable Fija");
                cmbabono3.Items.Remove("Cuenta Contable Fija");
                cmbabono4.Items.Remove("Cuenta Contable Fija");
            }

            if (cmbabono1.Text == "Cuenta Contable Bancos")
            {
                cmbabono2.Items.Remove("Cuenta Contable Bancos");
                cmbabono3.Items.Remove("Cuenta Contable Bancos");
                cmbabono4.Items.Remove("Cuenta Contable Bancos");
            }
            else if (cmbabono1.Text == "Cuenta Contable Almacenes")
            {
                cmbabono2.Items.Remove("Cuenta Contable Almacenes");
                cmbabono3.Items.Remove("Cuenta Contable Almacenes");
                cmbabono4.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbabono1.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono4.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbabono1.Text == "Cuenta Contable de Proveedores")
            {
                cmbabono2.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono3.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono4.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbabono1.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono4.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbabono1.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbabono2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono4.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbabono2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string cb = cmbabono2.Text;

            c.Catalogo_cargosyabonos2(cb, txtAbono2);
            cmbabono3.Enabled = true;
            if (cmbabono2.Text == "Cuenta Contable Fija")
            {
                cmbabono2.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbabono5.Visible = true;
            }
            else
            {
                cmbabono2.BackColor = Color.White;
                cmbabono5.Visible = false;
            }
            //actualizarListaAbonos();
            if (cmbabono2.Text == "Cuenta Contable Propietario")
            {
                cmbabono1.Items.Remove("Cuenta Contable Propietario");
                cmbabono3.Items.Remove("Cuenta Contable Propietario");
                cmbabono4.Items.Remove("Cuenta Contable Propietario");

            }
            if (cmbabono2.Text == "Cuenta Contable Documento")
            {
                cmbabono1.Items.Remove("Cuenta Contable Documento");
                cmbabono3.Items.Remove("Cuenta Contable Documento");
                cmbabono4.Items.Remove("Cuenta Contable Documento");

            }
            if (cmbabono2.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbabono1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono3.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono4.Items.Remove(" Cuenta Contable Concepto Ingreso");

            }
            if (cmbabono2.Text == "Cuenta Contable Fija")
            {
                cmbabono1.Items.Remove("Cuenta Contable Fija");
                cmbabono3.Items.Remove("Cuenta Contable Fija");
                cmbabono4.Items.Remove("Cuenta Contable Fija");

            }

            if (cmbabono2.Text == "Cuenta Contable Bancos")
            {
                cmbabono1.Items.Remove("Cuenta Contable Bancos");
                cmbabono3.Items.Remove("Cuenta Contable Bancos");
                cmbabono4.Items.Remove("Cuenta Contable Bancos");

            }
            else if (cmbabono2.Text == "Cuenta Contable Almacenes")
            {
                cmbabono1.Items.Remove("Cuenta Contable Almacenes");
                cmbabono3.Items.Remove("Cuenta Contable Almacenes");
                cmbabono4.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbabono2.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbabono1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono3.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono4.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbabono2.Text == "Cuenta Contable de Proveedores")
            {
                cmbabono1.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono3.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono4.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbabono2.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbabono1.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono3.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono4.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbabono2.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbabono1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono3.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono4.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void cmbabono3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cb = cmbabono3.Text;

            c.Catalogo_cargosyabonos2(cb, txtAbono3);
            cmbabono4.Enabled = true;
            if (cmbabono3.Text == "Cuenta Contable Fija")
            {
                cmbabono3.BackColor = Color.Red;
                MessageBox.Show("indique en el siguiente campo la cuenta contable a registrar");
                cmbabono5.Visible = true;
            }
            else
            {
                cmbabono3.BackColor = Color.White;
                cmbabono5.Visible = false;
            }
            //actualizarListaAbonos();
            if (cmbabono3.Text == "Cuenta Contable Propietario")
            {
                cmbabono1.Items.Remove("Cuenta Contable Propietario");
                cmbabono2.Items.Remove("Cuenta Contable Propietario");
                cmbabono4.Items.Remove("Cuenta Contable Propietario");

            }
            if (cmbabono3.Text == "Cuenta Contable Documento")
            {
                cmbabono1.Items.Remove("Cuenta Contable Documento");
                cmbabono2.Items.Remove("Cuenta Contable Documento");
                cmbabono4.Items.Remove("Cuenta Contable Documento");

            }
            if (cmbabono3.Text == " Cuenta Contable Concepto Ingreso")
            {
                cmbabono1.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono2.Items.Remove(" Cuenta Contable Concepto Ingreso");
                cmbabono4.Items.Remove(" Cuenta Contable Concepto Ingreso");

            }
            if (cmbabono3.Text == "Cuenta Contable Fija")
            {
                cmbabono1.Items.Remove("Cuenta Contable Fija");
                cmbabono2.Items.Remove("Cuenta Contable Fija");
                cmbabono4.Items.Remove("Cuenta Contable Fija");

            }

            if (cmbabono3.Text == "Cuenta Contable Bancos")
            {
                cmbabono1.Items.Remove("Cuenta Contable Bancos");
                cmbabono2.Items.Remove("Cuenta Contable Bancos");
                cmbabono4.Items.Remove("Cuenta Contable Bancos");
            }
            else if (cmbabono3.Text == "Cuenta Contable Almacenes")
            {
                cmbabono2.Items.Remove("Cuenta Contable Almacenes");
                cmbabono1.Items.Remove("Cuenta Contable Almacenes");
                cmbabono4.Items.Remove("Cuenta Contable Almacenes");
            }
            else if (cmbabono3.Text == "Cuenta Contable Movimientos Inventarios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono1.Items.Remove("Cuenta Contable Movimientos Inventarios");
                cmbabono4.Items.Remove("Cuenta Contable Movimientos Inventarios");
            }
            else if (cmbabono3.Text == "Cuenta Contable de Proveedores")
            {
                cmbabono2.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono1.Items.Remove("Cuenta Contable de Proveedores");
                cmbabono4.Items.Remove("Cuenta Contable de Proveedores");
            }
            else if (cmbabono3.Text == "Cuenta Contable Productos y Servicios")
            {
                cmbabono2.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono1.Items.Remove("Cuenta Contable Productos y Servicios");
                cmbabono4.Items.Remove("Cuenta Contable Productos y Servicios");
            }
            else if (cmbabono3.Text == "Cuenta Contable Conceptos Globales")
            {
                cmbabono2.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono1.Items.Remove("Cuenta Contable Conceptos Globales");
                cmbabono4.Items.Remove("Cuenta Contable Conceptos Globales");
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCargo1.Text = "";
            cmbcargo1.Text = "";
            txtCargo2.Text = "";
            cmbcargo2.Text = "";
            txtCargo3.Text = "";
            cmbcargo3.Text = "";
            txtCargo4.Text = "";
            cmbcargo4.Text = "";
            txtCargo5.Text = "";
            cmbcargo5.Text = "";
            txtAbono1.Text = "";
            cmbabono1.Text = "";
            txtAbono2.Text = "";
            cmbabono2.Text = "";
            txtAbono3.Text = "";
            cmbabono3.Text = "";
            txtAbono4.Text = "";
            cmbabono4.Text = "";
            rdtNotas.Text = "";


            string Cons = dataGridView1.Rows[e.RowIndex].Cells["Consec"].Value.ToString();
            string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
            string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();

            cmbclave.Text = Clave;
            txtnombre.Text = Nombre;
            textBox1.Text = Cons;

           // c.ConsultarInfoPoliza(Cons, Clave, Nombre, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtCargo7, txtCargo8, cmbcargo1, cmbcargo2, cmbcargo3, cmbcargo4, cmbcargo5, cmbcargo7, cmbcargo8, txtAbono1, txtAbono2, txtAbono3, txtAbono4, cmbabono1, cmbabono2, cmbabono3, cmbabono4, textBox10, cmbTpoliza, txtDpoliza, rdtNotas);
            groupBox1.Visible = false;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

            guna2GradientPanel6.Location = new Point(1017, 83);
            guna2GradientPanel6.Size = new Size(112, 583);
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

            guna2GradientPanel6.Location = new Point(1077, 83);
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
            if (e.ClickedItem.Text == "NUEVO")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 83);
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
                NuevaPoliza();
                groupBox2.Enabled = true;
                cmbclave.SelectedIndex = 0;
            }
            else if (e.ClickedItem.Text == "CONSULTAR")
            {

                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 83);
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

                if (TipoPolz == "Definiciones Condominios")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;

                    }
                    else
                    {

                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPoliza(dataGridView1);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }
                else if (TipoPolz == "Definiciones Gastos Personales")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;

                    }
                    else
                    {

                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPolizaGP(dataGridView1);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }
                else if (TipoPolz == "Definiciones Compras")
                {
                    if (groupBox1.Visible == true)
                    {
                        groupBox1.Visible = false;
                    }
                    else
                    {
                        groupBox1.Visible = true;
                        groupBox2.Enabled = true;
                        c.Consultar_DefinicionesPolizaCompras(dataGridView1, TipopolizaCompras);
                        button7.Enabled = false;
                        button5.Enabled = true;
                    }
                }

            }
            else if (e.ClickedItem.Text == "IMPRIMIR")
            {
                guna2PictureBox2.Visible = false;
                guna2PictureBox1.Visible = true;

                guna2GradientPanel6.Location = new Point(1077, 83);
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
        }
    }
}

