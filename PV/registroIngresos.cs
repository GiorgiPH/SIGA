using Condominios;
using Condominios.Clases.RegistrarIngresos;
using ControlAcademico;
using PuntoVentas.Clases.Login;
using PV;
using PV.Clases.Remision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PV
{
    public partial class registroIngresos : Form
    {
        DBRegistrarIngresos c = new DBRegistrarIngresos();
        DBRemiision r = new DBRemiision();

        public static string matricula = string.Empty;
        public static string nombre = string.Empty;
        public static string propiedad = string.Empty;
        public static int DescuentoNota = 0;
        public static decimal DescuentoPago = 0;
        string Tipo = string.Empty;
        string Monto = string.Empty;

        public registroIngresos(string tipo, string monto)
        {
            InitializeComponent();
            Tipo = tipo;
            Monto = monto;
            matricula = string.Empty;
            nombre = string.Empty;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            txtAlumno.Clear();
            this.Close();
            
        }
        private void CargarPagos()
        {
            if (Tipo == "Remision")
            {
                r.CargarRemisionCobro(dgvPagosPendientes,txtMatricula.Text);
            }else if(Tipo == "Recibo")
            {
                c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);

            }
        }
        private void registroIngresos_Load(object sender, EventArgs e)
        {
            txtMatricula.Text = matricula;
            txtAlumno.Text  = nombre;
            dtpFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dtpFecha.MaxDate = DateTime.Now;
            txtCaja.Text = "1";
            //c.ConsultarFechaRecargos(txtFechaRecargo);
            c.FechayRecargo(txtRecargosSiNo, txtDescuentosSiNo, txtMontoR, txtMontoD, txtFecha);
            btnBuscar.Enabled = Tipo == "M" ? false : true;

            if (txtFecha.Text == "No")
            {
                dtpFecha.Enabled = false;
            }
            else
            {
                dtpFecha.Enabled = true;
            }

            try
            {
                DateTime hoy = DateTime.Now;
                DateTime fecha = Convert.ToDateTime(txtFechaRecargo.Text);
                if (hoy.Year == fecha.Year && fecha.Month < hoy.Month && string.IsNullOrEmpty(Tipo))
                {
                    MessageBox.Show("Debe generar recargos del mes en el menu Datos Condominio antes del dia ultimo del mes actual, de lo contrario no se acumulara los recargos correspondientes");
                }
                else if ((hoy.Year > fecha.Year) && string.IsNullOrEmpty(Tipo))
                {
                    MessageBox.Show("Debe generar recargos del mes en el menu Datos Condominio antes del dia ultimo del mes actual, de lo contrario no se acumulara los recargos correspondientes");
                }
            }
            catch (Exception)
            {
            }



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            propiedad = string.Empty;
            if (Tipo == "Remision")
            {
                BuscarCliente buscar = new BuscarCliente();
                buscar.ShowDialog();

                if (!string.IsNullOrEmpty(BuscarCliente.Cliente))
                {
                    txtMatricula.Text= BuscarCliente.Cliente;
                    txtAlumno.Text = BuscarCliente.NombreCliente;
                }
            }
            else if(Tipo == "Recibo")
            {
                BuscarListaAlumnos2 buscar = new BuscarListaAlumnos2();
                buscar.ShowDialog();
            }
            
        }

        private void registroIngresos_Activated(object sender, EventArgs e)
        {
            //txtMatricula.Text = matricula;
            //txtAlumno.Text = nombre;

            if (txtAlumno.Text == string.Empty)
            {
                // btnBuscar.BackColor = Color.Red;
                button5.Enabled = false;
                button6.Enabled = false;
                dtpFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else
            {
                // btnBuscar.BackColor = Color.Gainsboro;
                button5.Enabled = true;
                button6.Enabled = true;
            }



        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;
            DateTime FechaHoy = DateTime.Now;
            string Hoy = FechaHoy.ToString("yyyy/MM/dd");
            button5.Enabled = false;
            button6.Enabled = false;

            if (txtMatricula.Text != string.Empty)
            {
                CargarPagos();
                button5.Enabled = true;
                button6.Enabled = true;
                /* decimal totalrecibos = 0;
                 foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                 {
                     c.RecargoConcepto(txtConcepto, txtGenerarRecargo, txtDiarioMensual, txtImportePorcentaje, txtImporte, txtPorcentaje, txtImporteConcepto, row.Cells["ClaveRecibo"].Value.ToString());
                     /* c.RecargoConceptoCalculo(txtConcepto.Text, row.Cells["FolioDocumento"].Value.ToString(), txtMatricula.Text, txtCalculo);

                      int Calculo = 0;
                      try
                      {
                          Calculo = Convert.ToInt32(txtCalculo.Text);
                      }
                      catch (Exception)
                      {
                      }


                     if (Convert.ToDateTime(row.Cells["Vencimiento"].Value) < Convert.ToDateTime(Hoy) && txtGenerarRecargo.Text == "Si")
                     {

                         if (txtFechaRecargo.Text != string.Empty)
                         {
                             CultureInfo ci = new CultureInfo("es-ES");
                             DateTime fecha = Convert.ToDateTime(Hoy);

                             string FechaFormateada = fecha.ToString("MMMM", ci) + " del " + fecha.ToString("yyyy");

                             if (txtDiarioMensual.Text == "Diario")
                             {
                                 DateTime oldDate3 = fecha;
                                 DateTime newDate3 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                 TimeSpan ts3 = newDate3 - oldDate3;
                                 int differenceInDays3 = ts3.Days;

                                 if (differenceInDays3 > 0)
                                 {

                                     DateTime oldDate = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     DateTime newDate = DateTime.Now;
                                     TimeSpan ts = newDate - oldDate;
                                     int differenceInDays = ts.Days;


                                     if (differenceInDays > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {
                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * differenceInDays;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * differenceInDays;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                 }
                             }
                             else if (txtDiarioMensual.Text == "Mensual")
                             {

                                 DateTime oldDate1 = fecha;
                                // int meses1 = Math.Abs((Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()).Month - oldDate1.Month) + 12 * (Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()).Year - oldDate1.Year));

                                //if (meses1 > 0)
                                if(oldDate1> Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()))
                                 {

                                     DateTime oldDate = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     DateTime newDate = DateTime.Now;
                                     TimeSpan ts = newDate - oldDate;
                                     int differenceInDays = ts.Days;

                                     DateTime oldDate2 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     int meses = Math.Abs((DateTime.Now.Month - oldDate2.Month) + 12 * (DateTime.Now.Year - oldDate2.Year));

                                     if (meses == 0 && differenceInDays > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {

                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                     else if (meses > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {

                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                 }
                             }
                         }
                         else
                         {
                             CultureInfo ci = new CultureInfo("es-ES");
                             //DateTime fecha = Convert.ToDateTime(txtFechaRecargo.Text);

                             //string FechaFormateada = fecha.ToString("MMMM", ci) + " del " + fecha.ToString("yyyy");

                             if (txtDiarioMensual.Text == "Diario")
                             {
                                 DateTime oldDate3 = DateTime.Now;
                                 DateTime newDate3 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                 TimeSpan ts3 = newDate3 - oldDate3;
                                 int differenceInDays3 = ts3.Days;

                                 if (differenceInDays3 > 0)
                                 {

                                     DateTime oldDate = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     DateTime newDate = DateTime.Now;
                                     TimeSpan ts = newDate - oldDate;
                                     int differenceInDays = ts.Days;


                                     if (differenceInDays > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {
                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * differenceInDays;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * differenceInDays;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                 }
                             }
                             else if (txtDiarioMensual.Text == "Mensual")
                             {

                                 DateTime oldDate1 = DateTime.Now;
                                // int meses1 = Math.Abs((Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()).Month - oldDate1.Month) + 12 * (Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()).Year - oldDate1.Year));

                                // if (meses1 > 0)
                                     if (oldDate1 > Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString()))
                                     {

                                     DateTime oldDate = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     DateTime newDate = DateTime.Now;
                                     TimeSpan ts = newDate - oldDate;
                                     int differenceInDays = ts.Days;

                                     DateTime oldDate2 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                     int meses = Math.Abs((DateTime.Now.Month - oldDate2.Month) + 12 * (DateTime.Now.Year - oldDate2.Year));

                                     if (meses == 0 && differenceInDays > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {
                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                     else if (meses > 0)
                                     {
                                         DateTime oldDate4 = Convert.ToDateTime(row.Cells["Vencimiento"].Value.ToString());
                                         DateTime newDate4 = Convert.ToDateTime(row.Cells["FechaRecargo"].Value.ToString());
                                         DateTime newHoy = DateTime.Now;
                                         string DiaVerf = Convert.ToString(oldDate4.Day);
                                         string MesVerf = Convert.ToString(newDate4.Month);
                                         string AñoVerf = Convert.ToString(newDate4.Year);

                                         string FechaVerf = AñoVerf + "/" + MesVerf + "/" + DiaVerf;
                                         DateTime newDateVerf = Convert.ToDateTime(FechaVerf);

                                         TimeSpan ts4 = newHoy - newDateVerf;
                                         int differenceInDays4 = ts4.Days;

                                         if (differenceInDays4 >= 30)
                                         {
                                             if (txtImportePorcentaje.Text == "IMPORTE")
                                             {
                                                 decimal recargo = Convert.ToDecimal(txtImporte.Text) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                             else if (txtImportePorcentaje.Text == "PORCENTAJE")
                                             {
                                                 decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                                                 decimal recargo = ((porcentaje * Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString()) / 100)) * 1;
                                                 row.Cells["Recargos"].Value = recargo.ToString("N", formato);
                                             }
                                         }
                                     }
                                 }
                             }
                         }
                     }
                     totalrecibos = totalrecibos + Convert.ToDecimal(row.Cells["SaldoActual"].Value) + Convert.ToDecimal(row.Cells["Recargos"].Value) + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value);
                 }
                 txtTotalRecibo.Text = totalrecibos.ToString("N", formato);*/
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Los descuentos seran aplicados y los recargos del mes seran trasladados a Recargos Acumulados de los registros seleccionados para registrar cobro", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
                ArrayList ListaConcept = new ArrayList();
                int contador = 0;
                foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                {
                    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                    {
                        contador++;
                    }
                }

                if (contador == 0)
                {
                    MessageBox.Show("Seleccione al menos un recibo para continuar");
                    return;
                }


            if (MessageBox.Show("Si continua los saldos del documento serán actualizados", "Registrar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DateTime FechaHoy = DateTime.Now;
                string Hoy = FechaHoy.ToString("yyyy/MM/dd");

                if (!ValidarRecargosYDescuentos())
                {
                    return;
                }

                ActualizarRecibos(Hoy);
                InsertarRecargos();
                ListaConcept = ObtenerListaConceptosSeleccionados();

                RegistrarCobro cobro = new RegistrarCobro(ListaConcept, txtMatricula.Text, txtAlumno.Text, dtpFecha.Text, Tipo, Monto)
                {
                    
                };
                RegistrarCobro.DescuentoPago = DescuentoPago;
                cobro.ShowDialog();

                LimpiarFormulario();
            }
            //}
        }
        private void LimpiarFormulario()
        {
            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            CargarPagos();
            dtpFecha.ResetText();
        }
        private bool ValidarRecargosYDescuentos()
        {
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                decimal recargos = Convert.ToDecimal(row.Cells["Recargos"].Value);
                decimal descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);
                decimal recargosAcumulados = Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value);

                if (recargos < 0)
                {
                    MessageBox.Show("No es posible ingresar un monto negativo de recargos");
                    return false;
                }

                //if (descuento > (recargos + recargosAcumulados))
                //{
                //    MessageBox.Show("No es posible ingresar un descuento mayor al total de los recargos");
                //    return false;
                //}
            }
            return true;
        }

        private void ActualizarRecibos(string hoy)
        {
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (EsFilaSeleccionada(row))
                {
                    decimal recargos = Convert.ToDecimal(row.Cells["Recargos"].Value);
                    decimal recargosAcumulados = Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value);
                    decimal descuento = Convert.ToDecimal(row.Cells["Descuento"].Value);
                    decimal saldoActual = Convert.ToDecimal(row.Cells["SaldoActual"].Value);
                    string folioDocumento = row.Cells["FolioDocumento"].Value.ToString();

                    if (recargos + recargosAcumulados <= 0)
                    {
                        // &c.ActualizarRecibo2Descuento(folioDocumento, recargos, descuento, saldoActual, hoy);
                    }
                    else
                    {
                        // &c.ActualizarRecibo2(folioDocumento, recargos, descuento, saldoActual, hoy);
                    }

                    if (descuento != 0.00m)
                    {
                        DescuentoNota = 1;
                    }
                    DescuentoPago = descuento;
                }
            }
        }

        private void InsertarRecargos()
        {
            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (EsFilaSeleccionada(row) && !Convert.ToBoolean(row.Cells["Rec"].Value))
                {
                    decimal recargos = Convert.ToDecimal(row.Cells["Recargos"].Value);
                    if (recargos > 0)
                    {
                        string folioDocumento = row.Cells["FolioDocumento"].Value.ToString();
                        DateTime fecha = Convert.ToDateTime(row.Cells["Fecha"].Value);
                        DateTime vencimiento = Convert.ToDateTime(row.Cells["Vencimiento"].Value);
                        decimal importe = Convert.ToDecimal(row.Cells["Importe"].Value);

                        // &c.insertRecargo(folioDocumento, fecha.ToString("yyyy/MM/dd"), vencimiento.ToString("yyyy/MM/dd"), importe, txtMatricula.Text, dtpFecha.Value.ToString("yyyy/MM/dd"), recargos);
                    }
                }
            }
        }

        private ArrayList ObtenerListaConceptosSeleccionados()
        {
            ArrayList listaConcept = new ArrayList();

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (EsFilaSeleccionada(row))
                {
                    listaConcept.Add(row.Cells["FolioDocumento"].Value.ToString());
                }
            }

            return listaConcept;
        }

        private bool EsFilaSeleccionada(DataGridViewRow row)
        {
            return row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true;
        }


        private void dgvPagosPendientes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //private void dgvPagosPendientes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

        //    formato.CurrencyGroupSeparator = ",";
        //    formato.NumberDecimalSeparator = ".";

        //    dgvPagosPendientes.Rows[e.RowIndex].Cells[12].ReadOnly = true;
        //    dgvPagosPendientes.Rows[e.RowIndex].Cells[14].ReadOnly = true;
        //    dgvPagosPendientes.Rows[e.RowIndex].Cells[15].ReadOnly = true;

        //    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
        //    {
        //        decimal Descuentos = 0.00M;
        //        decimal Recargos = 0.00M;
        //        decimal Importe = 0.00M;
        //        decimal Saldo = 0.00M;

        //        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value != null)
        //        {
        //            Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value.ToString());
        //        }
        //        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value != null)
        //        {
        //            Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value.ToString()) + Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
        //        }
        //        else
        //        {
        //            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = 0.00;
        //        }
        //        if (dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value != null)
        //        {
        //            Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value.ToString());
        //        }
        //        else
        //        {
        //            dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = 0.00;
        //        }


        //        Saldo = Importe + Recargos - Descuentos;

        //        string saldo = Saldo.ToString("N", formato);
        //        dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = saldo.ToString();
        //    }

        //    decimal TotalRecargos = 0.00M;
        //    decimal TotalDescuentos = 0.00M;
        //    decimal Subtotal = 0.00M;

        //    foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
        //    {
        //        if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
        //        {
        //            if (row.Cells["Recargos"].Value != null)
        //            {
        //                TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()) + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
        //                txtRecargos.Text = TotalRecargos.ToString("N", formato);
        //            }
        //            if (row.Cells["Descuento"].Value != null)
        //            {
        //                TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
        //                txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
        //            }
        //            Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
        //            txtSubtotal.Text = Subtotal.ToString("N", formato);

        //            txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
        //        }

        //    }

        //    string recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value).ToString("N", formato);
        //    dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = recargo;

        //    string descuento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value).ToString("N", formato);
        //    dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = descuento;
        //}

        private void dgvPagosPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null)
            //{
            //    bool isCellChecked = (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value;

            //    if (isCellChecked)
            //    {
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = false;
            //    }
            //    else
            //    {
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = true;
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value = "0.00";
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = true;
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[9].Value = "0.00";
            //        dgvPagosPendientes.Rows[e.RowIndex].Cells[10].Value = "0.00";

            //        int cont = 0;
            //        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            //        {
            //            if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
            //            {
            //                cont++;
            //            }
            //            if (cont == 0)
            //            {
            //                txtRecargos.Text = "0.00";
            //                txtDescuentos.Text = "0.00";
            //                txtSubtotal.Text = "0.00";
            //                txtTotal.Text = "0.00";
            //            }
            //        }
            //    }
            //}
        }

        void Limpiar()
        {
            txtMatricula.Clear();
            txtAlumno.Clear();
            txtSubtotal.Text = "0.00";
            txtRecargos.Text = "0.00";
            txtDescuentos.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalRecibo.Text = "0.00";
        }

        private void button6_Click(object sender, EventArgs e)
        {

            matricula = string.Empty;
            nombre = string.Empty;
            Limpiar();
            c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
            btnBuscar.BackColor = Color.Red;
            button5.Enabled = false;
            button6.Enabled = false;

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            matricula = string.Empty;
            nombre = string.Empty;
            GenerarRecibo.Matricula = string.Empty;
            RegistrarAnticipo.matricula = string.Empty;
            RegistrarAnticipo.nombre = string.Empty;
            AplicarAnticipo.matricula = string.Empty;
            AplicarAnticipo.nombre = string.Empty;
            //MenuPrincipal menu = new MenuPrincipal();
            //menu.Show();
            this.Close();
        }

        private void dgvPagosPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text != string.Empty)
            {
                PagosRecibos pagosRecibos = new PagosRecibos(txtMatricula.Text, txtAlumno.Text);
                pagosRecibos.ShowDialog();
                //Limpiar();
                CargarPagos();
            }
            else
            {
                MessageBox.Show("Seleccione el Cliente");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            //{
            //    if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
            //    {
            //        string folio = row.Cells["FolioDocumento"].Value.ToString();
            //        PartidasVer partidas = new PartidasVer(folio);
            //        partidas.ShowDialog();
            //        return;
            //    }
            //}
        }

        private void dgvPagosPendientes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";
            if (e.RowIndex != -1)
            {
                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    //bool isCellChecked = (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value;

                    //if (isCellChecked)
                    if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                    {
                        //dgvPagosPendientes.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        //dgvPagosPendientes.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                        //dgvPagosPendientes.Rows[e.RowIndex].Cells[10].ReadOnly = false;

                        decimal Descuentos = 0.00M;
                        decimal Recargos = 0.00M;
                        decimal Importe = 0.00M;
                        decimal Saldo = 0.00M;

                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value != null)
                        {
                            Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString());
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value != null)
                        {
                            Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = 0.00;
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value != null)
                        {
                            Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value.ToString());
                        }
                        else
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value = 0.00;
                        }


                        Saldo = Importe + Recargos - Descuentos;

                        string saldo = Saldo.ToString("N", formato);
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = saldo.ToString();

                        decimal TotalRecargos = 0.00M;
                        decimal TotalDescuentos = 0.00M;
                        decimal Subtotal = 0.00M;

                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                            {

                                if (row.Cells["RecargosAcumulados"].Value != null)
                                {
                                    TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
                                    txtRecargos.Text = TotalRecargos.ToString("N", formato);
                                }
                                if (row.Cells["Descuento"].Value != null)
                                {
                                    TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                    txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                                }
                                Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                                txtSubtotal.Text = Subtotal.ToString("N", formato);

                                txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                            }

                        }
                    }
                    else
                    {
                        //dgvPagosPendientes.Rows[e.RowIndex].Cells[11].ReadOnly = true;
                        //dgvPagosPendientes.Rows[e.RowIndex].Cells[11].Value = "0.00";
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[16].ReadOnly = true;
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = "0.00";
                        dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = "0.00";

                        int cont = 0;
                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                            {
                                cont++;
                            }

                            if (cont == 0)
                            {
                                txtRecargos.Text = "0.00";
                                txtDescuentos.Text = "0.00";
                                txtSubtotal.Text = "0.00";
                                txtTotal.Text = "0.00";
                            }
                        }

                        decimal TotalRecargos = 0.00M;
                        decimal TotalDescuentos = 0.00M;
                        decimal Subtotal = 0.00M;

                        foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                        {
                            if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                            {
                                if (row.Cells["RecargosAcumulados"].Value != null)
                                {
                                    TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
                                    txtRecargos.Text = TotalRecargos.ToString("N", formato);
                                }
                                if (row.Cells["Descuento"].Value != null)
                                {
                                    TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                    txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                                }
                                Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                                txtSubtotal.Text = Subtotal.ToString("N", formato);

                                txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                            }

                        }
                    }

                    if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasRecargo")
                    {
                        if (DBLogin.TipoUsuario != "Administrador")
                        {
                            MessageBox.Show("No tiene permisos de administrador");
                            return;
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Rec"].Value.ToString() == "1")
                        {
                            MessageBox.Show("No puedes agregar recargos hasta acumular los recargos actuales");
                            return;
                        }
                        if (txtRecargosSiNo.Text == "Si")
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].ReadOnly = false;
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Selected = true;
                            dgvPagosPendientes.BeginEdit(true);
                        }
                        else
                        {
                            MessageBox.Show("La configuracion no permite agregar recargos, consulte el menu Parametros->Datos Condominio");
                        }
                    }
                    else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MenosRecargo")
                    {
                        if (DBLogin.TipoUsuario != "Administrador")
                        {
                            MessageBox.Show("No tiene permisos de administrador");
                            return;
                        }
                        if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Rec"].Value.ToString() == "1")
                        {
                            MessageBox.Show("No puedes agregar recargos hasta acumular los recargos actuales");
                            return;
                        }
                        if (txtRecargosSiNo.Text == "Si")
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = "0.00";

                            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                            {
                                decimal Descuentos = 0.00M;
                                decimal Recargos = 0.00M;
                                decimal Importe = 0.00M;
                                decimal Saldo = 0.00M;

                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value != null)
                                {
                                    Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString());
                                }
                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value != null)
                                {
                                    Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value.ToString()) + Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                                }
                                else
                                {
                                    dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = 0.00;
                                }
                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value != null)
                                {
                                    Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value.ToString());
                                }
                                else
                                {
                                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value = 0.00;
                                }


                                Saldo = Importe + Recargos - Descuentos;

                                string saldo = Saldo.ToString("N", formato);
                                dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = saldo.ToString();
                            }
                            decimal TotalRecargos = 0.00M;
                            decimal TotalDescuentos = 0.00M;
                            decimal Subtotal = 0.00M;

                            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                            {
                                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                                {
                                    if (row.Cells["RecargosAcumulados"].Value != null)
                                    {
                                        TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
                                        txtRecargos.Text = TotalRecargos.ToString("N", formato);
                                    }
                                    if (row.Cells["Descuento"].Value != null)
                                    {
                                        TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                        txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                                    }
                                    Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                                    txtSubtotal.Text = Subtotal.ToString("N", formato);

                                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("La configuracion no permite agregar recargos, consulte el menu Parametros->Datos Condominio");
                        }
                    }

                    if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MasDescuentos")
                    {
                        if (DBLogin.TipoUsuario != "Administrador")
                        {
                            MessageBox.Show("No tiene permisos de administrador");
                            return;
                        }
                        if (txtDescuentosSiNo.Text == "Si")
                        {
                            //if (dgvPagosPendientes.Rows[e.RowIndex].Cells[1].Value.ToString() == "RCC")
                            //if (c.DescuentosPermitidos(dgvPagosPendientes.Rows[e.RowIndex].Cells[1].Value.ToString()) == "Si")
                            //{
                            //    decimal monto = c.Descuento(dgvPagosPendientes.Rows[e.RowIndex].Cells[1].Value.ToString());
                            //    dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value) * (monto / 100);
                            //}
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[16].ReadOnly = false;
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Selected = true;
                            dgvPagosPendientes.BeginEdit(true);
                        }
                        else
                        {
                            MessageBox.Show("La configuracion no permite agregar descuentos, consulte el menu Parametros->Datos Condominio");
                        }
                    }
                    else if (this.dgvPagosPendientes.Columns[e.ColumnIndex].Name == "MenosDescuentos")
                    {
                        if (DBLogin.TipoUsuario != "Administrador")
                        {
                            MessageBox.Show("No tiene permisos de administrador");
                            return;
                        }
                        if (txtDescuentosSiNo.Text == "Si")
                        {
                            dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = "0.00";
                            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
                            {
                                decimal Descuentos = 0.00M;
                                decimal Recargos = 0.00M;
                                decimal Importe = 0.00M;
                                decimal Saldo = 0.00M;

                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value != null)
                                {
                                    Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Importe"].Value.ToString());
                                }
                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value != null)
                                {
                                    Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["RecargosAcumulados"].Value.ToString()) + Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                                }
                                else
                                {
                                    dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = 0.00;
                                }
                                if (dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value != null)
                                {
                                    Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value.ToString());
                                }
                                else
                                {
                                    dgvPagosPendientes.Rows[e.RowIndex].Cells["Descuento"].Value = 0.00;
                                }


                                Saldo = Importe + Recargos - Descuentos;

                                string saldo = Saldo.ToString("N", formato);
                                dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = saldo.ToString();
                            }
                            decimal TotalRecargos = 0.00M;
                            decimal TotalDescuentos = 0.00M;
                            decimal Subtotal = 0.00M;

                            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
                            {
                                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                                {
                                    if (row.Cells["RecargosAcumulados"].Value != null)
                                    {
                                        TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
                                        txtRecargos.Text = TotalRecargos.ToString("N", formato);
                                    }
                                    if (row.Cells["Descuento"].Value != null)
                                    {
                                        TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                                        txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                                    }
                                    Subtotal = Subtotal + Convert.ToDecimal(row.Cells["Importe"].Value.ToString());
                                    txtSubtotal.Text = Subtotal.ToString("N", formato);

                                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("La configuracion no permite agregar descuentos, consulte el menu Parametros->Datos Condominio");
                        }
                    }
                }
            }


        }

        private void dgvPagosPendientes_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dgvPagosPendientes.IsCurrentCellDirty)
            {
                dgvPagosPendientes.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvPagosPendientes_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("en-US").NumberFormat;

            formato.CurrencyGroupSeparator = ",";
            formato.NumberDecimalSeparator = ".";

            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].ReadOnly = true;
            dgvPagosPendientes.Rows[e.RowIndex].Cells[14].ReadOnly = true;
            dgvPagosPendientes.Rows[e.RowIndex].Cells[15].ReadOnly = true;

            if (dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value != null && (bool)dgvPagosPendientes.Rows[e.RowIndex].Cells[0].Value == true)
            {
                decimal Descuentos = 0.00M;
                decimal Recargos = 0.00M;
                decimal Importe = 0.00M;
                decimal Saldo = 0.00M;

                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value != null)
                {
                    Importe = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[8].Value.ToString());
                }
                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value != null)
                {
                    Recargos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value.ToString()) + Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[13].Value.ToString());
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = 0.00;
                }
                if (dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value != null)
                {
                    Descuentos = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value.ToString());
                }
                else
                {
                    dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = 0.00;
                }


                Saldo = Importe + Recargos - Descuentos;

                string saldo = Saldo.ToString("N", formato);
                dgvPagosPendientes.Rows[e.RowIndex].Cells[17].Value = saldo.ToString();
            }

            decimal TotalRecargos = 0.00M;
            decimal TotalDescuentos = 0.00M;
            decimal Subtotal = 0.00M;

            foreach (DataGridViewRow row in dgvPagosPendientes.Rows)
            {
                if (row.Cells["Seleccionar"].Value != null && (bool)row.Cells["Seleccionar"].Value == true)
                {
                    if (row.Cells["Recargos"].Value != null)
                    {
                        TotalRecargos = TotalRecargos + Convert.ToDecimal(row.Cells["Recargos"].Value.ToString()) + Convert.ToDecimal(row.Cells["RecargosAcumulados"].Value.ToString());
                        txtRecargos.Text = TotalRecargos.ToString("N", formato);
                    }
                    if (row.Cells["Descuento"].Value != null)
                    {
                        TotalDescuentos = TotalDescuentos + Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                        txtDescuentos.Text = TotalDescuentos.ToString("N", formato);
                    }
                    Subtotal = Subtotal + Convert.ToDecimal(row.Cells["SaldoActual"].Value.ToString());
                    txtSubtotal.Text = Subtotal.ToString("N", formato);

                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtRecargos.Text) - Convert.ToDecimal(txtDescuentos.Text)).ToString("N", formato);
                }

            }

            string recargo = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value).ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[12].Value = recargo;

            string descuento = Convert.ToDecimal(dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value).ToString("N", formato);
            dgvPagosPendientes.Rows[e.RowIndex].Cells[16].Value = descuento;
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRecargos_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtMatricula.Text))
            //{
            //    MessageBox.Show("Seleciona un propietario");
            //}
            //else
            //{
            //    RecargosGenerados recargosGenerados = new RecargosGenerados(txtMatricula.Text, txtAlumno.Text);
            //    recargosGenerados.ShowDialog();
            //    c.CargarReciboAlumno(dgvPagosPendientes, txtMatricula.Text);
            //}

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
