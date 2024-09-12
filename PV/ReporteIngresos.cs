using System;
using System.Windows.Forms;
using Condominios.Clases.GenerarRecibo;
using Microsoft.Reporting.WinForms;
using PuntoVentas.Clases.Login;

namespace PV
{
    public partial class ReporteIngresos : Form
    {
        DBGenerarRecibo c = new DBGenerarRecibo();
        public static string Propietario1 = string.Empty;
        public static string Propietario2 = string.Empty;
        public static string Documento = string.Empty;
        public static string FormaPago = string.Empty;
        string Propietario1C = string.Empty;
        string Propietario2C = string.Empty;
        string DocumentoC = string.Empty;
        string FormaPagoC = string.Empty;
        string Seccion = string.Empty;
        string FechaPago = string.Empty;
        string FechaPago1 = string.Empty;
        string FechaPago2 = string.Empty;
        string FechaRecibo = string.Empty;
        string FechaRecibo1 = string.Empty;
        public static string Propiedad = string.Empty;
        string empresa = string.Empty;
        string FechaRecibo2 = string.Empty;
        string Anticipo = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;

        public ReporteIngresos(string propietario1, string propietario2, string documento, string formaPago, string fechaPago, string fechaPago1, string fechaPago2, string fechaRecibo, string fechaRecibo1, string fechaRecibo2, string anticipo, string seccion)
        {
            InitializeComponent();
            Propietario1 = propietario1;
            Propietario2 = propietario2;
            Documento = documento;
            FormaPago = formaPago;
            Propietario1C = propietario1;
            Propietario2C = propietario2;
            DocumentoC = documento;
            FormaPagoC = formaPago;
            FechaPago = fechaPago;
            FechaPago1 = fechaPago1;
            FechaPago2 = fechaPago2;
            FechaRecibo = fechaRecibo;
            FechaRecibo1 = fechaRecibo1;
            FechaRecibo2 = fechaRecibo2;
            Anticipo = anticipo;
            empresa = DBLogin.DatosEmpresa;
            Seccion = seccion;

        }

        private void ReporteIngresos_Load(object sender, EventArgs e)
        {
            c.SeleccionarPropietarios(cmbPropietario1);
            c.SeleccionarPropietarios(cmbpropietario2);

            if (Propietario1 == string.Empty)
            {
                cmbPropietario1.SelectedIndex = 0;
                cmbpropietario2.SelectedIndex = 0;
            }
            else if (Propietario1 != string.Empty && Propietario1!="TODOS")
            {
                cmbPropietario1.SelectedIndex = Convert.ToInt32(Propietario1);
                cmbpropietario2.SelectedIndex = Convert.ToInt32(Propietario2);
            }

            if (FechaPago == "Si")
            {
                cbFechas.Checked = true;
            }

            dtFecha1.Text = FechaPago1;
            dtFecha2.Text = FechaPago2;

            ReportParameter[] parameters = new ReportParameter[7];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("FechaP1", FechaPago1);
            parameters[1] = new ReportParameter("FechaP2", FechaPago2);
            parameters[2] = new ReportParameter("FechaR1", FechaRecibo1);
            parameters[3] = new ReportParameter("FechaR2", FechaRecibo2);
            parameters[4] = new ReportParameter("empresa", empresa);
            parameters[5] = new ReportParameter("FechaP", FechaPago);
            parameters[6] = new ReportParameter("FechaR", FechaRecibo);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            Reporte();

        }


        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbpropietario2.Text = cmbPropietario1.Text;

            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
                Propietario1 = Propi1;
                Propietario2 = Propi2;
            }
            else
            {
                Propietario1 = "TODOS";
                Propietario2 = "TODOS";
            }

            Reporte();
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpropietario2.Text == "TODOS")
            {
                cmbPropietario1.Text = "TODOS";
                Propietario1 = "TODOS";
                Propietario2 = "TODOS";
            }
            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
                Propietario1 = Propi1;
                Propietario2 = Propi2;
            }

            Reporte();
        }

        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            if (dtFecha2.Value < dtFecha1.Value)
            {
                dtFecha2.Text = dtFecha1.Text;
                MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
            }
        }

        void Reporte()
        {
            if (Anticipo == "No")
            {
                if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.Fill(this.ControlCondominiosDataSet21.Cobros);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2));
                    this.reportViewer1.RefreshReport();
                }
                else if ( (Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy1(this.ControlCondominiosDataSet21.Cobros, Documento);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy2(this.ControlCondominiosDataSet21.Cobros, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy3(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy4(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy5(this.ControlCondominiosDataSet21.Cobros, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy6(this.ControlCondominiosDataSet21.Cobros, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy143(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                //___________

                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy7(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy8(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy9(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy10(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy11(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy12(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy13(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy14(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy15(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy16(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy17(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy19(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy20(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy21(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy22(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy23(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy24(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy25(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy26(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy27(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy28(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy87(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy88(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy89(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy90(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy91(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy92(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy93(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy94(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy95(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy96(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy97(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy98(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy99(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy100(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy101(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy102(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy103(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy104(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                //______________
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy29(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy30(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy31(this.ControlCondominiosDataSet21.Cobros, Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy32(this.ControlCondominiosDataSet21.Cobros, Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy33(this.ControlCondominiosDataSet21.Cobros, Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy34(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy35(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy36(this.ControlCondominiosDataSet21.Cobros, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy37(this.ControlCondominiosDataSet21.Cobros, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy38(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy39(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy40(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy41(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy42(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy43(this.ControlCondominiosDataSet21.Cobros, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy123(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy124(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy125(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy126(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy127(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy128(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy129(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy130(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy131(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy131(this.ControlCondominiosDataSet21.Cobros, Documento, FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
            }
            else
            {
                if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy18(this.ControlCondominiosDataSet21.Cobros);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy44(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2));
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy45(this.ControlCondominiosDataSet21.Cobros, Documento);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy46(this.ControlCondominiosDataSet21.Cobros, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy47(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy48(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy49(this.ControlCondominiosDataSet21.Cobros, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy50(this.ControlCondominiosDataSet21.Cobros, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy144(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                //__________________________
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy51(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy52(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy53(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy54(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy55(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy56(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy57(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy58(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy59(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy60(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy61(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy62(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy63(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy64(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy65(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy66(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy67(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy68(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy69(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy70(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy71(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy105(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2),Documento, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy106(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy107(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy108(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy109(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy110(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Seccion );
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy111(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy112(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy113(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy114(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy115(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy116(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy117(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy118(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy119(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy120(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy121(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy122(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                //________________________
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy72(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy73(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy74(this.ControlCondominiosDataSet21.Cobros, Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy75(this.ControlCondominiosDataSet21.Cobros, Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy76(this.ControlCondominiosDataSet21.Cobros, Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy77(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy78(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy79(this.ControlCondominiosDataSet21.Cobros, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy80(this.ControlCondominiosDataSet21.Cobros, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy81(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy82(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy83(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy84(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy85(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy86(this.ControlCondominiosDataSet21.Cobros, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy133(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2 );
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy134(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Seccion );
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy135(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy136(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy137(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy138(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy139(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy140(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy141(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy142(this.ControlCondominiosDataSet21.Cobros, Documento, FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                }
            }
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                FechaPago = "Si";
            }
            else
            {
                dtFecha1.ResetText();
                dtFecha2.ResetText();
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                FechaPago = string.Empty;
            }
            Reporte();
        }

        private void dtFecha1_ValueChanged_1(object sender, EventArgs e)
        {
            FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();
        }

        private void dtFecha2_ValueChanged_1(object sender, EventArgs e)
        {
            FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Propietario1C != string.Empty)
            {
                Propietario1 = Propietario1C;
                Propietario2 = Propietario2C;
            }
            else
            {
                Propietario1 = "TODOS";
                Propietario2 = "TODOS";
            }

            cmbPropietario1.Text = Propietario1;
            cmbpropietario2.Text = Propietario2;

            Documento = DocumentoC;
            FormaPago = FormaPagoC;
            Propiedad = string.Empty;

            if (Propietario1 == string.Empty)
            {
                cmbPropietario1.SelectedIndex = 0;
                cmbpropietario2.SelectedIndex = 0;
            }
            else if (Propietario1 != string.Empty && Propietario1!="TODOS")
            {
                cmbPropietario1.SelectedIndex = Convert.ToInt32(Propietario1);
                cmbpropietario2.SelectedIndex = Convert.ToInt32(Propietario2);
            }

            if (FechaPago == "Si")
            {
                cbFechas.Checked = true;
            }

            dtFecha1.Text = FechaPago1;
            dtFecha2.Text = FechaPago2;

            Reporte();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FiltroFormaPago filtroFormaPago = new FiltroFormaPago();
            filtroFormaPago.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FiltroPropietario filtroPropietario = new FiltroPropietario();
            filtroPropietario.ShowDialog();
        }

        private void ReporteIngresos_Activated(object sender, EventArgs e)
        {
            cmbPropietario1.Text = Propietario1;
            cmbpropietario2.Text = Propietario2;

            if (cmbpropietario2.Text == "TODOS")
            {
                cmbPropietario1.Text = "TODOS";
                Propietario1 = "TODOS";
                Propietario2 = "TODOS";
            }
            if (cmbPropietario1.Text != "TODOS")
            {
                pro1 = cmbPropietario1.Text.IndexOf(" -");
                Propi1 = cmbPropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
                Propietario1 = Propi1;
                Propietario2 = Propi2;
            }

            if (Propietario1 == string.Empty)
            {
                cmbPropietario1.SelectedIndex = 0;
                cmbpropietario2.SelectedIndex = 0;
            }
            else if (Propietario1 != string.Empty && Propietario1 != "TODOS")
            {
                cmbPropietario1.SelectedIndex = Convert.ToInt32(Propietario1);
                cmbpropietario2.SelectedIndex = Convert.ToInt32(Propietario2);
            }

            if (FechaPago == "Si")
            {
                cbFechas.Checked = true;
            }

            dtFecha1.Text = FechaPago1;
            dtFecha2.Text = FechaPago2;

            Reporte();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroPropiedad filtroPropiedad = new FiltroPropiedad();
            filtroPropiedad.ShowDialog();
        }
    }
}
