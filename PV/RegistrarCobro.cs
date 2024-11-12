using Condominios.Clases.RegistrarIngresos;
using Guna.UI2.WinForms;
using PuntoVentas;
using PuntoVentas.Clases.DatosEmpresa;
using PV;
using PV.Clases.Remision;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PV
{
    public partial class RegistrarCobro : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        DBRemiision r = new DBRemiision();
        ArrayList Lista;
        DBDatosEmpresa d= new DBDatosEmpresa();
        public static string Carpeta = string.Empty;
        public static int CobroRealizado = 0;

        string ext = string.Empty;
        public static decimal DescuentoPago;
        string tipo = string.Empty;
        string monto=string.Empty;
        string[] datosE= null;

        public RegistrarCobro(ArrayList ListaConcep, string Matricula, string Alumno, string Fecha, string tipo, string monto)
        {
            InitializeComponent();
            txtMatricula.Text = Matricula;
            txtAlumno.Text = Alumno;
            Lista = ListaConcep;
            dtpFecha.Text = Fecha;
            this.tipo = tipo;
            this.monto = monto;
            datosE = d.CorreoContra();
            if (tipo == "M")
            {
                label5.Visible = true;
                txtMontoPermitido.Visible = true;
                txtMontoPermitido.Text = this.monto;
            }
        }

        private void RegistrarCobro_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet.FormasPago' Puede moverla o quitarla según sea necesario.
            this.formasPagoTableAdapter.Fill(this.controlCondominiosDataSet.FormasPago);
            // TODO: esta línea de código carga datos en la tabla 'controlAcademicoDataSet14.FormasPago' Puede moverla o quitarla según sea necesario.

            r.CargarReciboCobroById(dgvPagosPendientes, txtMatricula.Text, Lista);
            c.SeleccionarCuentaBancaria(cmbCuentaBancaria);
            
        }

        private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[11].ReadOnly = true;
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = true;

            decimal Importe = 0.00M;
            decimal Abono = 0.00M;
            decimal Recargo = 0.00M;
            decimal Saldo = 0.00M;
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "AbonoRecargo")
            {
                decimal total = 0.00m;
                if (tipo == "M")
                {
                    foreach (DataGridViewRow r in dgvPagosPendientes.Rows)
                    {
                        total += Convert.ToDecimal(r.Cells[11].Value.ToString()) + Convert.ToDecimal(r.Cells[9].Value.ToString());
                    }
                    if (total > Convert.ToDecimal(txtMontoPermitido.Text))
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = 0.00;
                        MessageBox.Show("excediste el monto permitido");


                    }
                }
                decimal abono1 = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value.ToString());
                decimal abono2 = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value.ToString());
                //  MessageBox.Show("" + recargo);
                // MessageBox.Show("" + recargo2);

                if (abono1 < abono2)
                {
                    MessageBox.Show("El abono a capital no puede ser mayor al saldo capital");
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = 0.00m;
                    return;
                }
            }
            else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "Abono")
            {
                decimal total = 0.00m;
                if (tipo == "M")
                {
                    foreach (DataGridViewRow r in dgvPagosPendientes.Rows)
                    {
                        total += Convert.ToDecimal(r.Cells[11].Value.ToString()) + Convert.ToDecimal(r.Cells[9].Value.ToString());
                    }
                    if (total > Convert.ToDecimal(txtMontoPermitido.Text))
                    {
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = 0.00;
                        MessageBox.Show("excediste el monto permitido");
                        

                    }
                }
                
                decimal recargo1 = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value.ToString());
                decimal recargo2 = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value.ToString());
                //  MessageBox.Show("" + recargo);
                // MessageBox.Show("" + recargo2);

                if (recargo1 < recargo2)
                {
                    MessageBox.Show("El abono a recargo no puede ser mayor a los recargos");
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = 0.00m;
                    return;
                }
            }

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value != null)
            {
                Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value != null)
            {
                Abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value.ToString());

            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = 0;
            }
            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value != null)
            {
                Recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value.ToString());
            }
            else
            {
                dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = 0;
            }

            Saldo = Importe - Abono - Recargo;

            string saldo = Saldo.ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = saldo.ToString();

            decimal TotalImporte = 0.00M;
            decimal TotalAbono = 0.00M;


            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                TotalImporte = TotalImporte + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                txtImporteTotal.Text = TotalImporte.ToString("N", formato);

                TotalAbono = TotalAbono + Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) + Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString());
                txtTotalPagado.Text = TotalAbono.ToString("N", formato);

            }

            decimal abono = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = abono.ToString("N", formato);
            decimal recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = recargo.ToString("N", formato);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {

                if (MessageBox.Show("¿Finalizar cobro?", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cmbCuentaBancaria.Text == string.Empty)
                    {
                        MessageBox.Show("Seleccione la cuenta bancaria");
                        return;
                    }

                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        try
                        {
                            if (row.Cells["FormaPago"].Value.ToString() == string.Empty)
                            {
                                MessageBox.Show("Registrar forma de pago para todos los pagos");
                                return;
                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Registrar forma de pago para todos los pagos");
                            return;
                        }
                    }
                    if (tipo == "M")
                    {
                        decimal total = 0.00m;
                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            total += Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) + Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString());
                        }
                        if (total < Convert.ToDecimal(txtMontoPermitido.Text))
                        {
                            MessageBox.Show("Es necesario completar el monto de traspaso");
                            return;
                        }
                    }
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {
                        if ((row.Cells["Abono"].Value.ToString() == "0.00" && row.Cells["Recargos"].Value.ToString() == "0.00") || (row.Cells["Abono"].Value.ToString() == string.Empty && row.Cells["Recargos"].Value.ToString() == string.Empty) || (Convert.ToDecimal(row.Cells["Abono"].Value.ToString()) < 0 && Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()) < 0))
                        {
                            MessageBox.Show("No es posible realizar un abono igual o menor a 0");
                            return;
                        }
                        //else if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                        //{
                        //    //MessageBox.Show("El Abono no puede ser mayor al Importe");
                        //    MessageBox.Show("El abono a capital no puede ser mayor al capital");
                        //    return;
                        //}
                        else if (Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()) < Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString()))
                        {
                            MessageBox.Show("El Abono a Recargos no puede ser mayor a los Recargos Calculados");
                            return;
                        }
                        //else if (Convert.ToDecimal(row.Cells["SaldoCapital"].Value.ToString()) < Convert.ToDecimal(row.Cells["Abono"].Value.ToString()))
                        //{
                        //    MessageBox.Show("El Abono a Capital no puede ser mayor al Saldo de Capital");
                        //    return;
                        //}

                    }

                    c.InsertarCobroGeneral(Convert.ToDecimal(txtTotalPagado.Text), txtFolioGeneral);

                    DateTime FechaHoy = DateTime.Now;
                    string Hoy = FechaHoy.ToString("yyyy/MM/dd");
                    string Documento = string.Empty;
                    int cont = 0;
                    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                    {

                        //if (Convert.ToDecimal(row.Cells["SaldoCapital"].Value.ToString()) < Convert.ToDecimal(row.Cells["Abono"].Value.ToString()))
                        //{

                        //    if (MessageBox.Show("Ingresaste un saldo a favor, realizar un anticipo?", "Anticipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    {
                        //        //decimal monto = Convert.ToDecimal(row.Cells["Importe"].Value.ToString()) - Convert.ToDecimal(row.Cells["Abono"].Value.ToString());

                        //        decimal monto = Convert.ToDecimal(row.Cells["SaldoCapital"].Value.ToString()) - Convert.ToDecimal(row.Cells["Abono"].Value.ToString());
                        //        RegistrarAnticipo registrarAnticipo = new RegistrarAnticipo("Propietario");

                        //        RegistrarAnticipo.matricula = txtMatricula.Text;
                        //        RegistrarAnticipo.nombre = txtAlumno.Text;
                        //        RegistrarAnticipo.FolioC = row.Cells["FolioDocumento"].Value.ToString();
                        //        RegistrarAnticipo.FolioCGeneral = txtFolioGeneral.Text;
                        //        registrarAnticipo.fl.Enabled = false;
                        //        registrarAnticipo.button1.PerformClick();
                        //        registrarAnticipo.cmbFormaPago.Text = row.Cells["FormaPago"].Value.ToString();
                        //        registrarAnticipo.cmbFormaPago.Enabled = false;
                        //        registrarAnticipo.cmbCuentaBancaria.Text = cmbCuentaBancaria.Text;
                        //        registrarAnticipo.dtpFecha.Value=dtpFecha.Value;
                        //        registrarAnticipo.dtpFecha.Enabled = false;
                        //        registrarAnticipo.cmbCuentaBancaria.Enabled = false;
                        //        registrarAnticipo.btnBuscar.Enabled = false;
                        //        registrarAnticipo.txtimporte.Enabled = false;
                        //        registrarAnticipo.dtpFecha.Enabled = false;
                        //        registrarAnticipo.cmbDivisas.Enabled = false;
                        //        registrarAnticipo.txtReferencia.Text = txtReferncia.Text;
                        //        registrarAnticipo.txtReferencia.Enabled = false;


                        //        registrarAnticipo.txtimporte.Text = Math.Abs(monto).ToString();
                        //        registrarAnticipo.txtImporteMXN.Text = registrarAnticipo.txtimporte.Text;

                        //        //registrarAnticipo.txtimporte.Enabled = false;
                        //        registrarAnticipo.ShowDialog();
                        //        if (!RegistrarAnticipo.AnticipoRealizado)
                        //        {
                        //            MessageBox.Show("No se realizo el anticipo, no es posible agregar un saldo negativo");
                        //            continue;
                        //        }
                        //        RegistrarAnticipo.AnticipoRealizado = false;
                        //        /*    if (registroIngresos.DescuentoNota==1)
                        //            {
                        //                ReciboNotaCredito reciboNotaCredito = new ReciboNotaCredito(txtFolioGeneral.Text, txtMatricula.Text, "0");
                        //                reciboNotaCredito.ShowDialog();
                        //            }*/
                        //        row.Cells["Abono"].Value = row.Cells["SaldoCapital"].Value;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("No es posible agregar un saldo a favor");
                        //        return ;
                        //    }
                        //}
                        if (Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()) < 0)
                        {
                            row.Cells["Saldo"].Value = "0.00";
                        }
                        r.ActualizarRemision(row.Cells["FolioDocumento"].Value.ToString(), Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString()), Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()));
                        c.InsertarCobro(row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, dtpFecha.Text, txtObservaciones.Text, row.Cells["FormaPago"].Value.ToString(), Convert.ToDecimal(row.Cells["Abono"].Value.ToString()), txtReferncia.Text, txtNumOperacion.Text, txtNumAutorizacion.Text, txtCuenta.Text, txtFolioGeneral.Text, Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString()), DescuentoPago, Convert.ToDecimal(row.Cells["Saldo"].Value.ToString()), tipo);
                        c.ModificarExtension(txtFolioGeneral.Text, ext);
                        c.ActualizarArchivoCobro(txtFolioGeneral.Text, txtArchivo.Text);
                        //````````````c.insertLogcobros(Login.UsuarioLogin, DateTime.Now.ToString("yyyy/MM/dd"), row.Cells["FolioDocumento"].Value.ToString(), row.Cells["Documento"].Value.ToString(), "", Convert.ToDecimal(row.Cells["Importe"].Value).ToString(), Convert.ToDecimal(row.Cells["Recargos"].Value).ToString(), Convert.ToDecimal(row.Cells["Recargos"].Value).ToString(), "0.00", "0.00", DescuentoPago.ToString(), Convert.ToDecimal(row.Cells["Saldo"].Value).ToString());




                        /*Llena la tabla log cobros*/
                        /*-----------------------------------------*/
                        cont++;
                        if (Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString()) > 0)
                        {
                            //c.insertRecargo(row.Cells["FolioDocumento"].Value.ToString(), Convert.ToDateTime(row.Cells["Fecha"].Value.ToString()).ToString("yyyy/MM/dd"), Convert.ToDateTime(row.Cells["FechaVence"].Value.ToString()).ToString("yyyy/MM/dd"), Convert.ToDecimal(row.Cells["Total"].Value.ToString()), txtMatricula.Text, dtpFecha.Value.ToString("yyyy/MM/dd"), Convert.ToDecimal(row.Cells["AbonoRecargo"].Value.ToString()));
                        }
                        
                        Documento = row.Cells["Documento"].Value.ToString();

                    }
                    if (cont > 0)
                    {
                        if (MessageBox.Show("¿Imprimir Recibo?", "Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ReciboCobranza reciboCobranza = new ReciboCobranza(txtFolioGeneral.Text, txtMatricula.Text);
                            reciboCobranza.ShowDialog();

                           

                        }   
                    }
                   
                    registroIngresos.nombre = string.Empty;
                    registroIngresos.matricula = string.Empty;
                    CobroRealizado = 1;
                    button5.Enabled = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione la cuenta bancaria para continuar");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                //&c.Actualizarcancelaciono2(row.Cells["FolioDocumento"].Value.ToString());
            }

            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasAbono")
            {
                decimal recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value.ToString());
                decimal recargo2 = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value.ToString());
                //  MessageBox.Show("" + recargo);
                // MessageBox.Show("" + recargo2);

                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Registrar Forma de Pago");
                    return;
                }
                else if (dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value.ToString() == "0.00")
                {
                    MessageBox.Show("No existe saldo capital por pagar");
                    return;
                }
                else if (recargo2 > recargo)
                {
                    MessageBox.Show("El Abono Recargo no debe ser mayor que el Recargo");
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = 0.00m;
                }
                else
                {
                    decimal AbonoCapital = 0.00m;
                    if (tipo == "M")
                    {
                        AbonoCapital = Convert.ToDecimal(txtMontoPermitido.Text);

                    }
                    else
                    {
                        AbonoCapital = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value);

                    }
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = AbonoCapital.ToString();
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[11].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }

            }
            else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasRecargo")
            {
                decimal recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value.ToString());
                decimal recargo2 = 0.00m;
                //  MessageBox.Show(""+ recargo);
                //  MessageBox.Show("" + recargo2);


                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Registrar Forma de Pago");
                    return;
                }

                else if (recargo == recargo2)
                {
                    MessageBox.Show("No se puede agregar un abono al recargo por que esta en  0.00");
                    return;
                }
                else
                {
                    decimal AbonoRecargo = 0.00m;
                    if (tipo == "M")
                    {
                        AbonoRecargo = Convert.ToDecimal(txtMontoPermitido.Text);

                    }
                    else
                    {
                        AbonoRecargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[6].Value);

                    }
                    AbonoRecargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[4].Value);
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = AbonoRecargo.ToString();
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Selected = true;
                    dgvPagosPendientes.BeginEdit(true);
                }

            }
        }

        private void cmbCuentaBancaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentaBancaria.Text != string.Empty)
            {
                string[] valores = c.InformacionCuenta(cmbCuentaBancaria.Text);
                txtCuenta.Text = valores[0];
            }
        }

        private void txtTotalPagado_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalPagado.Text == "0.00" || txtTotalPagado.Text == "0")
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (datosE[6] != string.Empty)
            {
                if (txtMatricula.Text != string.Empty)
                {
                    string NoOrdenResl = txtMatricula.Text;
                    string Descripcion = dtpFecha.Text;

                    Carpeta = datosE[6] + @"\" + "G" + NoOrdenResl;

                    try
                    {
                        if (Directory.Exists(Carpeta))
                        {

                        }
                        else
                        {
                            Directory.CreateDirectory(Carpeta);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    Carpeta = datosE[6] + @"\" + "G" + NoOrdenResl;

                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "All Files|*.*";

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string archivo = open.FileName;
                        ext = Path.GetExtension(archivo);
                        try
                        {
                            File.Copy(archivo, Carpeta + @"\" + Descripcion + ext);
                            txtArchivo.Text = Descripcion + ext;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ya hay un archivo guardado" + ex.ToString());
                            return;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Continue con el registro antes de adjuntar archivos");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (datosE[6] != string.Empty)
            {
                if (txtMatricula.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtMatricula.Text;
                    string Descripcion = dtpFecha.Text;

                    Carpeta = datosE[6] + @"\" + "G" + NoOrdenResl;

                    Process.Start(Carpeta + @"\" + txtArchivo.Text);
                }
                else if (txtMatricula.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }

            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

            if (datosE[6] != string.Empty)
            {
                if (txtMatricula.Text != string.Empty && txtArchivo.Text != string.Empty)
                {
                    string NoOrdenResl = txtMatricula.Text;
                    string Descripcion = dtpFecha.Text;

                    Carpeta = datosE[6] + @"\" + "G" + NoOrdenResl;
                    if (Directory.Exists(Carpeta))
                    {
                        File.Delete(Carpeta + @"\" + txtArchivo.Text);
                        txtArchivo.Clear();
                        c.ModificarExtension(txtFolioGeneral.Text, ext);
                    }
                }
                else if (txtMatricula.Text != string.Empty)
                {
                    MessageBox.Show("Seleccione un registro para continuar");
                }
                else if (txtArchivo.Text != string.Empty)
                {
                    MessageBox.Show("Este registro no cuenta con un archivo adjunto");
                }
            }
            else
            {
                MessageBox.Show("No existe una ruta para guardar archivos definida en Parametros->Datos Condominio");
            }
        }

        private void txtMontoPermitido_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMontoPermitido);
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


            }
        }
    }
}
