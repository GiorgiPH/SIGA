using PuntoVentas;
using PV.Clases.Polizas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PV
{
    public partial class GENERARPOLIZAS : Form
    {


        Dictionary<string, string> etiquetaCuentaMapping = new Dictionary<string, string>
        {
            {"CCPVA", nameof(CuentaproveedoresA)},
            {"CCPSA", nameof(CuentaProductoServA)},
            {"CCCGA", nameof(CuentaConceptosGlobalesA)},
            {"CCCCA", nameof(CuentaCentroCosto)}
        };
        DBPolizas c = new DBPolizas();
        string OrdenCargo = string.Empty;
        string OrdenAbono = string.Empty;
        string TipoPolz = string.Empty;
        string CuentaPro = string.Empty;
        string CuentaDoc = string.Empty;
        string CuentaIng = string.Empty;
        string CuentaBanc = string.Empty;
        string CuentaCondo = string.Empty;

        string cuentaDepositosGasto = string.Empty;

        string CuentaProA = string.Empty;
        string CuentaDocA = string.Empty;
        string CuentaIngA = string.Empty;
        string CuentaBancA = string.Empty;
        string CuentaCondoA = string.Empty;
        string cuentaDepositosGastoA = string.Empty;
        string Grupo = string.Empty;

        //--- POLIZA COMPRAS
        string CuentaAlmacen = string.Empty;
        string CuentaMovimientoInv = string.Empty;
        string Cuentaproveedores = string.Empty;
        string CuentaProductoServ = string.Empty;
        string CuentaConceptosGlobales = string.Empty;
        string CuentaCentroCosto = string.Empty;
        string CuentaFamilia = string.Empty;

        string CuentaEmpleado = string.Empty;
        string CuentaCentroCostoDep = string.Empty;


        string CuentaAlmacenA = string.Empty;
        string CuentaMovimientoInvA = string.Empty;
        string CuentaproveedoresA = string.Empty;
        string CuentaProductoServA = string.Empty;
        string CuentaConceptosGlobalesA = string.Empty;
        string CuentaCentroCostoA = string.Empty;
        string CuentaFamiliaA = string.Empty;

        string CuentaEmpleadoA = string.Empty;
        string CuentaCentroCostoDepA = string.Empty;

        public static string TipopolizaCompras = string.Empty;

        public GENERARPOLIZAS(string TipoPoliza)
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(guna2Button1, "Buscar Cuenta Contable");
            tt.SetToolTip(button1, "Consultar");
            tt.SetToolTip(button9, "Imprimir");
            TipoPolz = TipoPoliza;
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
            dtpPeriodo.CustomFormat = "yyyy/MM";
            //dtpPeriodo.Value = DateTime.Now;
            dtpPeriodo.ShowUpDown = true;
            //Primero obtenemos el día actual

            // dtpFechaInicial.Format = DateTimePickerFormat.Custom;
            // dtpFechaInicial.CustomFormat = "dd";
            //dtpFechaInicial.ShowUpDown = true;
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            //Asi obtenemos el primer dia del mes actual
            DateTime dtpFechaInicial = new DateTime(date.Year, date.Month, 1);
            dtpPeriodo.Value = dtpFechaInicial;
            dtpFechaFinal.Format = DateTimePickerFormat.Custom;
            dtpFechaFinal.CustomFormat = "dd";
            //dtpFechaFinal.ShowUpDown = true;

        }

        private void GENERARPOLIZAS_Load(object sender, EventArgs e)
        {
            //  MessageBox.Show(TipoPolz);

            if (TipoPolz == "Polizas Condiminios")
            {


                cmbExportar.Items.Insert(0, "SI");
                cmbExportar.SelectedIndex = 0;
                //  c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);
                c.TipoTorre(cmbTorre);
                cmbTorre.SelectedIndex = 0;
                c.TipoDocumento(cmbdocumento);
                //   int Cons = Convert.ToInt32(lblconsecutivo.Text);
                //cmbclave.Text = Convert.ToString(Cons + 1);
                cmbdocumento.Items.Insert(0, "Todos");
                cmbdocumento.SelectedIndex = 0;

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
                label14.Location = new Point(20, 487);
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
                txtResumida.Text = "NO";
                cmbdocumento.Enabled = false;

                // label14.Text = "CONCEPTO GASTOS PERSONALES PROPIETARIO";

            }
            if (TipoPolz == "Polizas Inventarios")
            {
                cmbExportar.Items.Insert(0, "SI");
                cmbExportar.SelectedIndex = 0;
                //  c.Catalogo_DefPoliza(TipoPolz, lblconsecutivo);
                //c.TipoDocumentoCompras(cmbdocumento);
                cmbdocumento.Items.Insert(0, "Todos");
                cmbdocumento.SelectedIndex = 0;
                txtResumida.Text = "NO";
                cmbdocumento.Enabled = false;

                // label14.Text = "CONCEPTO GASTOS PERSONALES PROPIETARIO";

            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                groupBox1.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
            }
            //      MessageBox.Show(TipoPolz);
            if (TipoPolz == "Polizas Condiminios")
            {
                groupBox2.Enabled = true;
                c.ConsultaDefPoliza(dataGridView1);
                cmbTorre.Enabled = true;

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
                c.ConsultaDefPolizaCompras(dataGridView1, TipoPolz);

                if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                {
                    cmbdocumento.Enabled = false;
                }
                else
                {
                    cmbdocumento.Enabled = true;
                }
            }
            else if (TipoPolz == "Polizas Inventarios")
            {

                groupBox2.Enabled = true;
                c.ConsultaDefPolizaInventarios(dataGridView1);

                //if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                //{
                //    cmbdocumento.Enabled = false;
                //}
                //else
                //{
                //    cmbdocumento.Enabled = true;
                //}
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(TipoPolz);
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

                        //if (lblContatenaCuentaContables.Text == "CCP" || lblContatenaCuentaContables.Text == "CCD" || lblContatenaCuentaContables.Text == "CCI" || lblContatenaCuentaContables.Text == "CCB")
                        //{
                        //    lblContatenaCuentaContables.Text = "";
                        //    lblContatenaCuentaContablesA.Text = "";
                        //}
                        //if (lblContatenaCuentaContables.Text == "*")
                        //{
                        //    lblContatenaCuentaContables.Text = "";
                        //}
                        //if (lblContatenaCuentaContablesA.Text == "*")
                        //{
                        //    lblContatenaCuentaContablesA.Text = "";
                        //}

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
                            //ReportePolizasAnticipos RPA = new ReportePolizasAnticipos(dtpPeriodo.Text, DelDia, cmbdocumento.Text == "Todos" ? "" : cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaIng, CuentaBanc, CuentaCondo, CuentaProA, CuentaDocA, CuentaIngA, CuentaBancA, CuentaCondoA, TSeparador.Text, lblContatenaCuentaContablesA.Text, lblclavetorre.Text, OrdenCargo, OrdenAbono, cmbExportar.Text);
                            //RPA.ShowDialog();
                        }
                        else
                        {
                            //RGeneracionPolizasF GPC = new RGeneracionPolizasF(dtpPeriodo.Text, DelDia, cmbdocumento.Text == "Todos" ? "" : cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaIng, CuentaBanc, CuentaCondo, CuentaProA, CuentaDocA, CuentaIngA, CuentaBancA, CuentaCondoA, TSeparador.Text, lblContatenaCuentaContablesA.Text, lblclavetorre.Text, OrdenCargo, OrdenAbono, cmbExportar.Text);
                            //GPC.ShowDialog();


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
                            //ReporteGastoPersonales RGP = new ReporteGastoPersonales(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, cuentaDepositosGasto, CuentaBanc, CuentaProA, cuentaDepositosGastoA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text, cmbExportar.Text);
                            //RGP.ShowDialog();

                        }
                        else if (txtnombre.Text == "Póliza Gastos Personales")
                        {

                            //  MessageBox.Show("No poliza: " + txtNoPoliza.Text + "Fecha i: " + FechaI + " Fecha F; " + FechaF + "Tipo Poliza: " + TipoPoliza + "C Fija: " + lblContatenaCuentaContables.Text + "CCP: " + CuentaPro + "CDG: " + cuentaDepositosGasto + "CCB: " + CuentaBanc + " CCPA: " + CuentaProA + "CDGA: " + cuentaDepositosGastoA + "CCBA: " + CuentaBancA + "Seprador: " + TSeparador.Text + "C fija A" + lblContatenaCuentaContablesA.Text);
                            //ReporteGastoPersonalesGasto RGP = new ReporteGastoPersonalesGasto(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, cuentaDepositosGasto, CuentaBanc, CuentaProA, cuentaDepositosGastoA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text, OrdenCargo, OrdenAbono, cmbExportar.Text);
                            //RGP.ShowDialog();
                        }
                        else if (txtnombre.Text == "Póliza Traspasos Recibo")
                        {
                            //ReporteGastoPersonalesTraspaso RGP = new ReporteGastoPersonalesTraspaso(dtpPeriodo.Text, DelDia, cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, cuentaDepositosGasto, CuentaBanc, CuentaProA, cuentaDepositosGastoA, CuentaBancA, TSeparador.Text, lblContatenaCuentaContablesA.Text, OrdenCargo, OrdenAbono, cmbExportar.Text);
                            //RGP.ShowDialog();
                        }


                    }
                    else if (TipoPolz == "Polizas Compras" || TipoPolz == "Polizas Inventarios")
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



               


                        if (txtnombre.Text == "Define Póliza Anticipos Proveedores" || txtnombre.Text == "Define Póliza Aplicación Anticipos Proveedores")
                        {
                            //PolizaComprasAnticipos GPCC = new PolizaComprasAnticipos(dtpPeriodo.Text, DelDia, cmbdocumento.Text.ToUpper() == "TODOS" ? "" : cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, CuentaIng, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, CuentaIngA, TSeparador.Text, cmbExportar.Text);
                            //GPCC.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(CuentaConceptosGlobales+"          "+CuentaConceptosGlobalesA);
                            PolizaCompras1 GPC = new PolizaCompras1(dtpPeriodo.Text, DelDia, cmbdocumento.Text.ToUpper() == "TODOS" ? "" : cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, CuentaCentroCostoDep, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, CuentaCentroCostoDepA, TSeparador.Text, cmbExportar.Text);
                            GPC.ShowDialog();
                        }
                            //else
                            //{
                            //    PolizaComprasDetalle GPCCC = new PolizaComprasDetalle(dtpPeriodo.Text, DelDia, cmbdocumento.Text.ToUpper() == "TODOS" ? "" : cmbdocumento.Text, txtNoPoliza.Text, txtDiarioP.Text, txtConcepto.Text, FechaI, FechaF, cmbTipoP.Text, TipoPoliza, lblContatenaCuentaContables.Text, CuentaPro, CuentaDoc, CuentaBanc, CuentaAlmacen, CuentaMovimientoInv, Cuentaproveedores, CuentaProductoServ, CuentaConceptosGlobales, CuentaCentroCosto, lblContatenaCuentaContablesA.Text, CuentaProA, CuentaDocA, CuentaBancA, CuentaAlmacenA, CuentaMovimientoInvA, CuentaproveedoresA, CuentaProductoServA, CuentaConceptosGlobalesA, CuentaCentroCostoA, TSeparador.Text);
                            //    GPCCC.ShowDialog();
                            //}
                       


                            

                        

                    }

                    CuentaPro = string.Empty;
                    CuentaDoc = string.Empty;
                    CuentaIng = string.Empty;
                    CuentaBanc = string.Empty;
                    cuentaDepositosGasto = string.Empty;
                    CuentaProA = string.Empty;
                    CuentaDocA = string.Empty;
                    CuentaIngA = string.Empty;
                    CuentaCondo = string.Empty;
                    CuentaCondoA = string.Empty;

                    CuentaIngA = string.Empty;
                    CuentaBancA = string.Empty;
                    cuentaDepositosGastoA = string.Empty;
                }
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

        public void CargaCuentaC()
        {
            OrdenCargo = string.Empty;
            lblContatenaCuentaContables.Text = string.Empty;

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
            else if (txtCargo1.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC1.Text = "CCT";
            }
            else if (txtCargo1.Text == "Cuenta Contable Bancos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC1.Text = "CCB";
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
            else if (txtCargo1.Text == "Cuenta Contable Conceptos Globales" )
            {
                lblCuentaC1.Text = "CCCG";
            }
            else if (txtCargo1.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC1.Text = "CCCC";
            }
            else if (txtCargo1.Text == "Cuenta Contable Familia")
            {
                lblCuentaC1.Text = "CCF";
            }
            else if (txtCargo1.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC1.Text = "CCE";
            }
            else if (txtCargo1.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC1.Text = "CCDE";
            }
            else
            {
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC1.Text = txtCargo1.Text; // value.Substring(startIndex, length);
                    lblContatenaCuentaContables.Text = txtCargo1.Text;
                }


            }
            if (lblCuentaC1.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC1.Text;

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
            else if (txtCargo2.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC2.Text = "CCT";
            }
            else if (txtCargo2.Text == "Cuenta Contable Bancos")
            {
                // c.consultaCuentaBanco(lblCuentaC2);
                lblCuentaC2.Text = "CCB";
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
            else if (txtCargo2.Text == "Cuenta Contable Familia")
            {
                lblCuentaC2.Text = "CCF";
            }
            else if (txtCargo2.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC2.Text = "CCE";
            }
            else if (txtCargo2.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC2.Text = "CCDE";
            }
            else
            {
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC2.Text = txtCargo2.Text;
                    lblContatenaCuentaContables.Text = txtCargo2.Text;
                }
            }
            if (lblCuentaC2.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC2.Text;

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
            else if (txtCargo3.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC3.Text = "CCT";
            }
            else if (txtCargo3.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC3);
                lblCuentaC3.Text = "CCB";
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
            else if (txtCargo3.Text == "Cuenta Contable Familia")
            {
                lblCuentaC3.Text = "CCF";
            }
            else if (txtCargo3.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC3.Text = "CCE";
            }
            else if (txtCargo3.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC3.Text = "CCDE";
            }
            else
            {
                /*    String value = txtCargo3.Text;
                    int startIndex = 21;
                    int length = 10;
                    lblCuentaC3.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC3.Text = txtCargo3.Text;
                    lblContatenaCuentaContables.Text = txtCargo3.Text;
                }
            }
            if (lblCuentaC3.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC3.Text;

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
            else if (txtCargo4.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC4.Text = "CCT";
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
            else if (txtCargo4.Text == "Cuenta Contable Familia")
            {
                lblCuentaC4.Text = "CCF";
            }
            else if (txtCargo4.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC4.Text = "CCE";
            }
            else if (txtCargo4.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC4.Text = "CCDE";
            }
            else
            {
                /*     String value = txtCargo4.Text;
                     int startIndex = 21;
                     int length = 10;
                     lblCuentaC4.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC4.Text = txtCargo4.Text;
                    lblContatenaCuentaContables.Text = txtCargo4.Text;
                }
            }
            if (lblCuentaC4.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC4.Text;

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
            else if (txtCargo5.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC5.Text = "CCT";
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
            else if (txtCargo5.Text == "Cuenta Contable Familia")
            {
                lblCuentaC5.Text = "CCF";
            }
            else if (txtCargo5.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC5.Text = "CCE";
            }
            else if (txtCargo5.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC5.Text = "CCDE";
            }
            else
            {
                /*String value = txtCargo5.Text;
                int startIndex = 21;
                int length = 10;
                lblCuentaC5.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC5.Text = txtCargo5.Text;
                    lblContatenaCuentaContables.Text = txtCargo5.Text;
                }
            }
            if (lblCuentaC5.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC5.Text;

            }
            //////----------
            if (txtCargo6.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC6.Text = "CCP";
            }
            else if (txtCargo6.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaC6, cmbdocumento.Text);
                lblCuentaC6.Text = "CCD";
            }
            else if (txtCargo6.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaC6);
                lblCuentaC6.Text = "CCI";
            }
            else if (txtCargo6.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC6);
                lblCuentaC6.Text = "CCB";
            }
            else if (txtCargo6.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC6.Text = "CCT";
            }
            else if (txtCargo6.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC6.Text = "CDG";
            }
            else if (txtCargo6.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC6.Text = "CCA";
            }
            else if (txtCargo6.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC6.Text = "CCMI";
            }
            else if (txtCargo6.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC6.Text = "CCPV";
            }
            else if (txtCargo6.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC6.Text = "CCPS";
            }
            else if (txtCargo6.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC6.Text = "CCCG";
            }
            else if (txtCargo6.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC6.Text = "CCCC";
            }
            else if (txtCargo6.Text == "Cuenta Contable Familia")
            {
                lblCuentaC6.Text = "CCF";
            }
            else if (txtCargo6.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC6.Text = "CCE";
            }
            else if (txtCargo6.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC6.Text = "CCDE";
            }
            else
            {
                /*String value = txtCargo6.Text;
                int startIndex = 21;
                int length = 10;
                lblCuentaC6.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC6.Text = txtCargo6.Text;
                    lblContatenaCuentaContables.Text = txtCargo6.Text;
                }
            }
            if (lblCuentaC6.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC6.Text;

            }
            //////----------
            if (txtCargo7.Text == "Cuenta Contable Propietario")
            {
                lblCuentaC7.Text = "CCP";
            }
            else if (txtCargo7.Text == "Cuenta Contable Documento")
            {
                //c.consultaCuentaDocumento(lblCuentaC7, cmbdocumento.Text);
                lblCuentaC7.Text = "CCD";
            }
            else if (txtCargo7.Text == " Cuenta Contable Concepto Ingreso")
            {
                //c.consultaCuentaConceptosIngresos(lblCuentaC7);
                lblCuentaC7.Text = "CCI";
            }
            else if (txtCargo7.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaC7);
                lblCuentaC7.Text = "CCB";
            }
            else if (txtCargo7.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaC7.Text = "CCT";
            }
            else if (txtCargo7.Text == "Catalogo Gastos")
            {
                // c.consultaCuentaBanco(lblCuentaC1);
                lblCuentaC7.Text = "CDG";
            }
            else if (txtCargo7.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaC7.Text = "CCA";
            }
            else if (txtCargo7.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaC7.Text = "CCMI";
            }
            else if (txtCargo7.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaC7.Text = "CCPV";
            }
            else if (txtCargo7.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaC7.Text = "CCPS";
            }
            else if (txtCargo7.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaC7.Text = "CCCG";
            }
            else if (txtCargo7.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaC7.Text = "CCCC";
            }
            else if (txtCargo7.Text == "Cuenta Contable Familia")
            {
                lblCuentaC7.Text = "CCF";
            }
            else if (txtCargo7.Text == "Cuenta Contable Empleado")
            {
                lblCuentaC7.Text = "CCE";
            }
            else if (txtCargo7.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaC7.Text = "CCDE";
            }
            else
            {
                /*String value = txtCargo7.Text;
                int startIndex = 21;
                int length = 10;
                lblCuentaC7.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContables.Text))
                {
                    lblCuentaC7.Text = txtCargo7.Text;
                    lblContatenaCuentaContables.Text = txtCargo7.Text;
                }
            }
            if (lblCuentaC7.Text != "*")
            {
                OrdenCargo = OrdenCargo + "," + lblCuentaC7.Text;

            }

        }

        public void CargaCuentaA()
        {
            OrdenAbono = string.Empty;
            lblContatenaCuentaContablesA.Text = string.Empty;
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
            else if (txtAbono1.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaA1.Text = "CCTA";
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

            else if (txtAbono1.Text == "Cuenta Contable Almacenes")
            {
                lblCuentaA1.Text = "CCAAB";
            }
            else if (txtAbono1.Text == "Cuenta Contable Movimientos Inventarios")
            {
                lblCuentaA1.Text = "CCMIA";
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
            else if (txtAbono1.Text == "Cuenta Contable Familia")
            {
                lblCuentaA1.Text = "CCFA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Empleado")
            {
                lblCuentaA1.Text = "CCEA";
            }
            else if (txtAbono1.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaA1.Text = "CCDEA";
            }
            else
            {
                if (string.IsNullOrEmpty(lblContatenaCuentaContablesA.Text))
                {
                    lblCuentaA1.Text = txtAbono1.Text;
                    lblContatenaCuentaContablesA.Text = txtAbono1.Text;
                }
            }
            if (lblCuentaA1.Text != "*")
            {
                OrdenAbono = OrdenAbono + "," + lblCuentaA1.Text;

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
            else if (txtAbono2.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaA2.Text = "CCTA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA2);
                lblCuentaA2.Text = "CCBA";
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
                lblCuentaA2.Text = "CCMIA";
            }
            else if (txtAbono2.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA2.Text = "CCPVA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA2.Text = "CCPSA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA2.Text = "CCCGA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA2.Text = "CCCCA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Familia")
            {
                lblCuentaA2.Text = "CCFA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Empleado")
            {
                lblCuentaA2.Text = "CCEA";
            }
            else if (txtAbono2.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaA2.Text = "CCDEA";
            }
            else
            {
                if (string.IsNullOrEmpty(lblContatenaCuentaContablesA.Text))
                {
                    /*   String value = txtAbono2.Text;
                       int startIndex = 21;
                       int length = 10;
                       lblCuentaA2.Text = value.Substring(startIndex, length);*/
                    lblCuentaA2.Text = txtAbono2.Text;
                    lblContatenaCuentaContablesA.Text = txtAbono2.Text;
                }

            }
            if (lblCuentaA2.Text != "*")
            {
                OrdenAbono = OrdenAbono + "," + lblCuentaA2.Text;

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
            else if (txtAbono3.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaA3.Text = "CCTA";
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
                lblCuentaA3.Text = "CCMIA";
            }
            else if (txtAbono3.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA3.Text = "CCPVA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA3.Text = "CCPSA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA3.Text = "CCCGA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA3.Text = "CCCCA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Familia")
            {
                lblCuentaA3.Text = "CCFA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Empleado")
            {
                lblCuentaA3.Text = "CCEA";
            }
            else if (txtAbono3.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaA3.Text = "CCDEA";
            }
            else
            {
                /*    String value = txtAbono3.Text;
                    int startIndex = 21;
                    int length = 10;
                    lblCuentaA3.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContablesA.Text))
                {
                    lblCuentaA3.Text = txtAbono3.Text;
                    lblContatenaCuentaContablesA.Text = txtAbono3.Text;
                }
            }
            if (lblCuentaA3.Text != "*")
            {
                OrdenAbono = OrdenAbono + "," + lblCuentaA3.Text;

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
            else if (txtAbono4.Text == "Cuenta Contable Condominio Torre")
            {
                lblCuentaA4.Text = "CCTA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Bancos")
            {
                //c.consultaCuentaBanco(lblCuentaA4);
                lblCuentaA4.Text = "CCBA";
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
                lblCuentaA4.Text = "CCMIA";
            }
            else if (txtAbono4.Text == "Cuenta Contable de Proveedores")
            {
                lblCuentaA4.Text = "CCPVA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Productos y Servicios")
            {
                lblCuentaA4.Text = "CCPSA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Conceptos Globales")
            {
                lblCuentaA4.Text = "CCCGA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Centro de Costo")
            {
                lblCuentaA4.Text = "CCCCA";
            }
            else if (txtCargo1.Text == "Cuenta Contable Familia")
            {
                lblCuentaA4.Text = "CCFA";
            }
            else if (txtCargo1.Text == "Cuenta Contable Empleado")
            {
                lblCuentaA4.Text = "CCEA";
            }
            else if (txtAbono4.Text == "Cuenta Contable Centro Costos Departamento")
            {
                lblCuentaA4.Text = "CCDEA";
            }
            else
            {
                /*   String value = txtAbono4.Text;
             S      int startIndex = 21;
                   int length = 10;
                   lblCuentaA4.Text = value.Substring(startIndex, length);*/
                if (string.IsNullOrEmpty(lblContatenaCuentaContablesA.Text))
                {
                    lblCuentaA4.Text = txtAbono4.Text;
                    lblContatenaCuentaContablesA.Text = txtAbono4.Text;
                }
            }
            if (lblCuentaA4.Text != "*")
            {
                OrdenAbono = OrdenAbono + "," + lblCuentaA4.Text;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //   MenuPrincipal menu = new MenuPrincipal();
            //   menu.Show();
            this.Hide();
        }




        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //normal
            lblCuentaC1.Text = "*";
            lblCuentaC2.Text = "*";
            lblCuentaC3.Text = "*";
            lblCuentaC4.Text = "*";
            lblCuentaC5.Text = "*";
            lblCuentaA1.Text = "*";
            lblCuentaA2.Text = "*";
            lblCuentaA3.Text = "*";
            lblCuentaA4.Text = "*";


            lblContatenaCuentaContables.Text = "*";
            lblContatenaCuentaContablesA.Text = "*";



            groupBox1.Visible = false;
            string Cons = dataGridView1.Rows[e.RowIndex].Cells["Consec"].Value.ToString();
            string Clave = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
            string Nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            string Estatus = dataGridView1.Rows[e.RowIndex].Cells["Estatus"].Value.ToString();
            cmbclave.Items.Clear();
            cmbclave.Items.Add(Clave);
            cmbclave.SelectedIndex = 0;

            txtnombre.Text = Nombre;
            txtEstatus.Text = Estatus;
            if (TipoPolz == "Polizas Condiminios")
            {
                c.CargarInfoPoliza(Cons, Clave, Nombre, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtAbono1, txtAbono2, txtAbono3, txtAbono4, TSeparador, cmbTipoP, txtDiarioP, TipoPolz);

                cmbTipoP.SelectedIndex = 0;
                if (txtDiarioP.Text == string.Empty)
                {
                    txtDiarioP.Text = "NO";
                }
                if (cmbExportar.Text == string.Empty)
                {
                    cmbExportar.Text = "NO";
                }
                txtResumida.Text = "NO";
                txtXdias.Text = "NO";

                if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                {
                    cmbdocumento.Enabled = false;
                    cmbdocumento.Items.Insert(0, "Todos");
                    cmbdocumento.SelectedIndex = 0;
                    if (txtDiarioP.Text == string.Empty)
                    {
                        txtDiarioP.Text = "NO";

                    }
                    if (cmbExportar.Text == string.Empty)
                    {
                        cmbExportar.Text = "NO";
                    }
                    txtResumida.Text = "NO";
                    txtXdias.Text = "NO";

                    if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                    {
                        cmbdocumento.Enabled = false;
                    }
                }
                /* if (txtnombre.Text != "Define Póliza Recibos")
                 {
                     cmbTorre.Enabled = false;
                 }*/

                //  ExtraerCuentaC();
            }

            else if (TipoPolz == "Definiciones Gastos Personales")
            {
                c.CargarInfoPolizaGP(Cons, Clave, Nombre, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtAbono1, txtAbono2, txtAbono3, txtAbono4, TSeparador, cmbTipoP, txtDiarioP, TipoPolz);
                if (txtDiarioP.Text == string.Empty)
                {
                    txtDiarioP.Text = "NO";
                }
                if (cmbExportar.Text == string.Empty)
                {
                    cmbExportar.Text = "NO";
                }
                txtResumida.Text = "NO";
                txtXdias.Text = "NO";
                if (cmbTipoP.Items.Count > 0)
                {
                    cmbTipoP.SelectedIndex = 0;

                }
                if (txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos")
                {
                    cmbdocumento.Enabled = false;

                }
                cmbdocumento.Enabled = false;
                if (txtnombre.Text == "Póliza Depósitos")
                {
                    txtConcepto.Text = "Deposito Cuentas Personales";
                    label14.Text = "CONCEPTO DEPOSITOS PERSONALES PROPIETARIO";
                }
                else if (txtnombre.Text == "Póliza Gastos Personales")
                {
                    txtConcepto.Text = "Gasto Cuentas Personales";
                    label14.Text = "CONCEPTO GASTO PERSONALES PROPIETARIO";

                }

            }

            else if (TipoPolz == "Polizas Compras")
            {
                txtCargo6.Visible = true;
                txtCargo7.Visible = true;
                c.CargarInfoPolizaCompras(Cons, Clave, Nombre, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtCargo6, txtCargo7, txtAbono1, txtAbono2, txtAbono3, txtAbono4, TSeparador, cmbTipoP, txtDiarioP, TipoPolz);
                if (txtDiarioP.Text == string.Empty)
                {
                    txtDiarioP.Text = "NO";
                }
                if (cmbExportar.Text == string.Empty)
                {
                    cmbExportar.Text = "NO";
                }
                if (txtnombre.Text == "Define Póliza Compras Almacén" || txtnombre.Text == "Define Póliza Compras Gastos" || txtnombre.Text == "Define Póliza Egresos" || txtnombre.Text == "Define Póliza Anticipos" || txtnombre.Text == "Define Póliza Aplicación Anticipos" || txtnombre.Text == "Define Póliza Salidas Almacén")
                {
                    txtResumida.Text = "SI"; /*esta linea se modifico*/
                }
                else
                {
                    txtResumida.Text = "NO";
                }

                txtXdias.Text = "NO";
            }
            else if (TipoPolz == "Polizas Inventarios")
            {
                c.CargarInfoPolizaInventarios(Cons, Clave, Nombre, txtCargo1, txtCargo2, txtCargo3, txtCargo4, txtCargo5, txtAbono1, txtAbono2, txtAbono3, txtAbono4, TSeparador, cmbTipoP, txtDiarioP, TipoPolz);
                if (txtDiarioP.Text == string.Empty)
                {
                    txtDiarioP.Text = "NO";
                }
                if (cmbExportar.Text == string.Empty)
                {
                    cmbExportar.Text = "NO";
                }
                txtResumida.Text = "NO";
                txtXdias.Text = "NO";
                if (cmbTipoP.Items.Count > 0)
                {
                    cmbTipoP.SelectedIndex = 0;

                }
                
                cmbdocumento.Enabled = false;
                

            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void cmbTipoP_SelectedIndexChanged(object sender, EventArgs e)
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dtpPeriodo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpPeriodo_Leave(object sender, EventArgs e)
        {

            DateTime date = dtpPeriodo.Value.Date;
            dtpFechaInicial.Value = new DateTime(date.Year, date.Month, 1);
            dtpFechaFinal.Value = dtpPeriodo.Value.AddMonths(1).AddDays(-1);

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
            if (lblCuentaC1.Text == "CCT")
            {
                CuentaCondo = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCT")
            {
                CuentaCondo = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCT")
            {
                CuentaCondo = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCT")
            {
                CuentaCondo = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCT")
            {
                CuentaCondo = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCTA")
            {
                CuentaCondoA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCTA")
            {
                CuentaCondoA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCTA")
            {
                CuentaCondoA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCTA")
            {
                CuentaCondoA = lblCuentaA4.Text;
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
            if (lblCuentaA4.Text == "CCIA")
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


            if (lblCuentaC1.Text == "CCF")
            {
                CuentaFamilia = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCF")
            {
                CuentaFamilia = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCF")
            {
                CuentaFamilia = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCF")
            {
                CuentaFamilia = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCF")
            {
                CuentaFamilia = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCFA")
            {
                CuentaFamiliaA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCFA")
            {
                CuentaFamiliaA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCFA")
            {
                CuentaFamiliaA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCFA")
            {
                CuentaFamiliaA = lblCuentaA4.Text;
            }



            if (lblCuentaC1.Text == "CCE")
            {
                CuentaEmpleado = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCE")
            {
                CuentaEmpleado = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCE")
            {
                CuentaEmpleado = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCE")
            {
                CuentaEmpleado = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCE")
            {
                CuentaEmpleado = lblCuentaC5.Text;
            }
            if (lblCuentaA1.Text == "CCEA")
            {
                CuentaEmpleadoA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCEA")
            {
                CuentaEmpleadoA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCEA")
            {
                CuentaEmpleadoA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCEA")
            {
                CuentaEmpleadoA = lblCuentaA4.Text;
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
            if (lblCuentaC6.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC6.Text;
            }
            if (lblCuentaC7.Text == "CCCG")
            {
                CuentaConceptosGlobales = lblCuentaC7.Text;
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



            if (lblCuentaC1.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC1.Text;
            }
            if (lblCuentaC2.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC2.Text;
            }
            if (lblCuentaC3.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC3.Text;
            }
            if (lblCuentaC4.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC4.Text;
            }
            if (lblCuentaC5.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC5.Text;
            }
            if (lblCuentaC6.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC6.Text;
            }
            if (lblCuentaC7.Text == "CCDE")
            {
                CuentaCentroCostoDep = lblCuentaC7.Text;
            }
            if (lblCuentaA1.Text == "CCDEA")
            {
                CuentaCentroCostoDepA = lblCuentaA1.Text;
            }
            if (lblCuentaA2.Text == "CCDEA")
            {
                CuentaCentroCostoDepA = lblCuentaA2.Text;
            }
            if (lblCuentaA3.Text == "CCDEA")
            {
                CuentaCentroCostoDepA = lblCuentaA3.Text;
            }
            if (lblCuentaA4.Text == "CCDEA")
            {
                CuentaCentroCostoDepA = lblCuentaA4.Text;
            }

        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCuentaC1_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaA1_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaC2_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaC3_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaC4_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaC5_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaA2_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaA3_Click(object sender, EventArgs e)
        {

        }

        private void lblCuentaA4_Click(object sender, EventArgs e)
        {

        }

        private void lblContatenaCuentaContables_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
           // MenuPrincipal.Panel_A_mostrar = "Antes";
            //MenuPrincipal.Panel_A_mostrar2 = "CondominiosReporteEgreso";
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpPeriodo_ValueChanged_1(object sender, EventArgs e)
        {
            dtpFechaInicial.Value = new DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1);
            dtpFechaFinal.Value = new DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, DateTime.DaysInMonth(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month));

            //int selectedMonth = dtpPeriodo.Value.Month;
            //int selectedYear = dtpPeriodo.Value.Year;

            //// Calcular el último día del mes seleccionado
            //int lastDayOfMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);

            //// Establecer primero MinDate y luego MaxDate
            //dtpFechaInicial.MinDate = new DateTime(selectedYear, selectedMonth, 1);
            //dtpFechaInicial.MaxDate = new DateTime(selectedYear, selectedMonth, lastDayOfMonth);
            //dtpFechaFinal.MinDate = new DateTime(selectedYear, selectedMonth, 1);
            //dtpFechaFinal.MaxDate = new DateTime(selectedYear, selectedMonth, lastDayOfMonth);

            //// Establecer el valor después de configurar MinDate y MaxDate
            //dtpFechaInicial.Value = new DateTime(selectedYear, selectedMonth, 1);
            //dtpFechaFinal.Value = new DateTime(selectedYear, selectedMonth, lastDayOfMonth);

        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {

        }
        void ExtraerCuentaC()
        {
            if (lblCuentaA1.Text == "*" && txtAbono1.Text != string.Empty)
            {
                lblContatenaCuentaContablesA.Text = txtAbono1.Text;
            }
            /**/
            if (lblCuentaA2.Text == "*" && txtAbono2.Text != string.Empty)
            {
                lblContatenaCuentaContablesA.Text = txtAbono2.Text;
            }

            if (lblCuentaA3.Text == "*" && txtAbono3.Text != string.Empty)
            {
                lblContatenaCuentaContablesA.Text = txtAbono3.Text;
            }
            if (lblCuentaA4.Text == "*" && txtAbono4.Text != string.Empty)
            {
                lblContatenaCuentaContablesA.Text = txtAbono4.Text;
            }
            /**/
            if (lblCuentaC1.Text == "*" && txtCargo1.Text != string.Empty)
            {
                lblContatenaCuentaContables.Text = txtCargo1.Text;
            }
            /**/
            if (lblCuentaC2.Text == "*" && txtCargo2.Text != string.Empty)
            {
                lblContatenaCuentaContables.Text = txtCargo2.Text;
            }

            if (lblCuentaC3.Text == "*" && txtCargo3.Text != string.Empty)
            {
                lblContatenaCuentaContables.Text = txtCargo3.Text;
            }
            if (lblCuentaC4.Text == "*" && txtCargo4.Text != string.Empty)
            {
                lblContatenaCuentaContables.Text = txtCargo4.Text;
            }
            if (lblCuentaC5.Text == "*" && txtCargo5.Text != string.Empty)
            {
                lblContatenaCuentaContables.Text = txtCargo5.Text;
            }
        }

        private void cmbTorre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTorre.SelectedIndex == 0)
            {
                lblclavetorre.Text = string.Empty;
            }
            else
            {
                c.ClaveTipoTorre(cmbTorre.Text, lblclavetorre);

            }

        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //CatalogoCuentasContables c = new CatalogoCuentasContables();
            //c.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void txtCargo1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAbono1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbExportar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }



}
