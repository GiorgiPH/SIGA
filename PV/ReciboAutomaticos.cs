using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.RecibosAutomaticos;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class ReciboAutomaticos : Form
    {
        DBRecibosAutomaticos c = new DBRecibosAutomaticos();

        public ReciboAutomaticos()
        {
            InitializeComponent();
        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominio.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominio.Text);
                txtCondominio.Text = valores[0];
                c.ConsultaRecibosAutomaticosCondominio(txtCondominio.Text, txtRecibosAutomaticos);

                c.ConsultaAutoImporte(txtConceptosMantenimiento.Text, txtAutomatico, txtImporte);

                c.CargarCondominios(dataGridView2, txtCondominio.Text);
                c.DiasVence(txtDiasVence);
                try
                {
                    if (txtDiasVence.Text != string.Empty)
                    {
                        int Dias = Convert.ToInt32(txtDiasVence.Text);
                        DateTime FechaVence = Convert.ToDateTime(dtFecha.Text);
                        FechaVence = FechaVence.AddDays(Dias);
                        dtVence.Text = FechaVence.ToString("yyyy/MM/dd");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Formato de dias vencimiento incorrecto");
                }

            }
        }

        private void ReciboAutomaticos_Load(object sender, EventArgs e)
        {
            c.SeleccionarConceptoRecibo(cmbConcepto);
            c.SeleccionarCondominio(cmbCondominio);
            c.ConsultaRecibosAutomaticos(txtRecibosAutomaticos, txtDocumento);
            c.ConsultaConceptoMantenimiento(txtConceptosMantenimiento, txtConcepto);
            cmbConcepto.Text = txtConceptosMantenimiento.Text + " - " + txtConcepto.Text;
            c.ConsultaAutoImporte(txtConceptosMantenimiento.Text, txtAutomatico, txtImporte);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.CorreoContra();
            if (DBRecibosAutomaticos.Correo == string.Empty)
            {
                if (MessageBox.Show("No hay Correo definido para el envio de Recibos, ¿Desea continuar?", "Recibos Automaticos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }

            if (cmbCondominio.Text == string.Empty)
            {
                MessageBox.Show("Seleccione el condominio para continuar");
            }
            else if (Convert.ToDateTime(dtFecha.Text) > Convert.ToDateTime(dtVence.Text))
            {
                MessageBox.Show("La fecha del recibo no puede ser mayor a la fecha de vencimiento");
            }
            else if (txtMesAño.Text == string.Empty)
            {
                MessageBox.Show("Registre la referencia del recibo");
            }
            else
            {
                if (MessageBox.Show("Si el concepto seleccionado es diferente al concepto de mantenimiento registrado en el condominio, verifique que el importe ha utilizar sea del documento en Parametros -> Datos Condominio -> Registrar Cobranza. ¿Desea continuar?", "Recibos Automaticos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (txtAutomatico.Text != "Automatico")
                    {
                        MessageBox.Show("El concepto no esta especificado en generacion automatica");
                    }
                    else
                    {
                        if (txtRecibosAutomaticos.Text == "Estructura")
                        {
                            string Fecha = dtFecha.Text;
                            DateTime FechaVence = Convert.ToDateTime(dtVence.Text);
                            TimeSpan d = Convert.ToDateTime(dtVence.Text) - Convert.ToDateTime(dtFecha.Text);
                            string DiasVence = Convert.ToString(d.Days);


                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                string Cliente = row.Cells["ClavePropietario"].Value.ToString();
                                string Importe = row.Cells["CuotaMantenimiento"].Value.ToString();
                                string ClaveSub = row.Cells["ClaveSub"].Value.ToString();

                                c.InsertarRecibo(txtFolio, txtDocumento.Text, "Bloqueado", Fecha, DiasVence, FechaVence.ToString("yyyy/MM/dd"), Cliente, "MXN", "1.00", txtMesAño.Text, DBLogin.usuario, Importe, Importe, Importe, ClaveSub);
                                c.InsertarPartida(txtFolio.Text, "1", txtConceptosMantenimiento.Text, txtMesAño.Text, "1", "Servicio", "MXN", "1.00", Convert.ToDecimal(Importe), Convert.ToDecimal(Importe));

                                ReporteReciboAutomatico reporteReciboAutomatico = new ReporteReciboAutomatico(txtFolio.Text, Cliente, txtCondominio.Text, ClaveSub, row.Cells["Correo"].Value.ToString(), row.Cells["Correo2"].Value.ToString());
                                reporteReciboAutomatico.ShowDialog();
                            }
                            MessageBox.Show("Recibos Generados");
                            if (ReporteReciboAutomatico.Opcion == 1)
                            {
                                MessageBox.Show("Error en la ruta definida en Parametros -> Datos Condominio, no se exportaron los recibo en PDF.");
                            }
                            if (ReporteReciboAutomatico.Opcion2 == 1)
                            {
                                MessageBox.Show("Error en el envio de correo revise en Parametros -> Datos Condominio o en la configuracion de seguridad de su correo en Acceso de aplicaciones.");
                            }
                        }
                        else if (txtRecibosAutomaticos.Text == "Concepto")
                        {
                            if (txtImporte.Text != string.Empty || txtImporte.Text == "0.00")
                            {
                                string Fecha = dtFecha.Text;
                                DateTime FechaVence = Convert.ToDateTime(dtVence.Text);
                                TimeSpan d = Convert.ToDateTime(dtVence.Text) - Convert.ToDateTime(dtFecha.Text);
                                string DiasVence = Convert.ToString(d.Days);

                                foreach (DataGridViewRow row in dataGridView2.Rows)
                                {
                                    string Cliente = row.Cells["ClavePropietario"].Value.ToString();
                                    string ClaveSub = row.Cells["ClaveSub"].Value.ToString();

                                    c.InsertarRecibo(txtFolio, txtDocumento.Text, "Bloqueado", Fecha, DiasVence, FechaVence.ToString("yyyy/MM/dd"), Cliente, "MXN", "1.00", txtMesAño.Text, DBLogin.usuario, txtImporte.Text, txtImporte.Text, txtImporte.Text, ClaveSub);
                                    c.InsertarPartida(txtFolio.Text, "1", txtConceptosMantenimiento.Text, txtMesAño.Text, "1", "Servicio", "MXN", "1.00", Convert.ToDecimal(txtImporte.Text), Convert.ToDecimal(txtImporte.Text));

                                    ReporteReciboAutomatico reporteReciboAutomatico = new ReporteReciboAutomatico(txtFolio.Text, Cliente, txtCondominio.Text, ClaveSub, row.Cells["Correo"].Value.ToString(), row.Cells["Correo2"].Value.ToString());
                                    reporteReciboAutomatico.ShowDialog();
                                }
                                MessageBox.Show("Recibos Generados");
                                if (ReporteReciboAutomatico.Opcion == 1)
                                {
                                    MessageBox.Show("Error en la ruta definida en Parametros -> Datos Condominio, no se exportaron los recibo en PDF.");
                                }
                                if (ReporteReciboAutomatico.Opcion2 == 1)
                                {
                                    MessageBox.Show("Error en el envio de correo revise en Parametros -> Datos Condominio o en la configuracion de seguridad de su correo en Acceso de aplicaciones.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El importe del Concepto de Ingreso no esta registrado");
                            }
                        }
                        else if (txtRecibosAutomaticos.Text == "ProIndiviso")
                        {
                            c.ConsultaAutoImporteProIndivisoPresupuesto(txtCondominio.Text, txtImporte, txtAñoPr, txtPeriodo);

                            int año = dtFecha.Value.Year;

                            if (año != Convert.ToInt32(txtAñoPr.Text))
                            {
                                MessageBox.Show("El año del presupuesto del condominio no corresponde con el año actual");
                                return;
                            }

                            if (txtImporte.Text != string.Empty || txtImporte.Text == "0.00")
                            {
                                string Fecha = dtFecha.Text;
                                DateTime FechaVence = Convert.ToDateTime(dtVence.Text);
                                TimeSpan d = Convert.ToDateTime(dtVence.Text) - Convert.ToDateTime(dtFecha.Text);
                                string DiasVence = Convert.ToString(d.Days);

                                foreach (DataGridViewRow row in dataGridView2.Rows)
                                {
                                    string Cliente = row.Cells["ClavePropietario"].Value.ToString();
                                    string ProIndiviso = row.Cells["ProIndiviso"].Value.ToString();
                                    string ClaveSub = row.Cells["ClaveSub"].Value.ToString();

                                    decimal porcentaje = Convert.ToDecimal(ProIndiviso) / 100;
                                    decimal Importe = Convert.ToDecimal(txtImporte.Text) * porcentaje;
                                    Importe = Importe / Convert.ToDecimal(txtPeriodo.Text);

                                    c.InsertarRecibo(txtFolio, txtDocumento.Text, "Bloqueado", Fecha, DiasVence, FechaVence.ToString("yyyy/MM/dd"), Cliente, "MXN", "1.00", txtMesAño.Text, DBLogin.usuario, Convert.ToString(Importe), Convert.ToString(Importe), Convert.ToString(Importe), ClaveSub);
                                    c.InsertarPartida(txtFolio.Text, "1", txtConceptosMantenimiento.Text, txtMesAño.Text, "1", "Servicio", "MXN", "1.00", Importe, Importe);

                                    ReporteReciboAutomatico reporteReciboAutomatico = new ReporteReciboAutomatico(txtFolio.Text, Cliente, txtCondominio.Text, ClaveSub, row.Cells["Correo"].Value.ToString(), row.Cells["Correo2"].Value.ToString());
                                    reporteReciboAutomatico.ShowDialog();
                                }
                                MessageBox.Show("Recibos Generados");
                                if (ReporteReciboAutomatico.Opcion == 1)
                                {
                                    MessageBox.Show("Error en la ruta definida en Parametros -> Datos Condominio, no se exportaron los recibo en PDF.");
                                }
                                if (ReporteReciboAutomatico.Opcion2 == 1)
                                {
                                    MessageBox.Show("Error en el envio de correo revise en Parametros -> Datos Condominio o en la configuracion de seguridad de su correo en Acceso de aplicaciones.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El importe del Concepto de Ingreso no esta registrado");
                            }
                        }
                        
                    }
                }
            }

            txtFolio.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReporteReciboAutomatico.Opcion = 0;
            ReporteReciboAutomatico.Opcion2 = 0;
            this.Close();
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] valores = c.InformacionRecibo(cmbConcepto.Text);
            txtConceptosMantenimiento.Text = valores[0];
            txtImporte.Text = valores[3];
        }

        private void dtFecha_ValueChanged(object sender, EventArgs e)
        {
            dtVence.Text = dtFecha.Text;
        }

    }
}
