using Condominios.Clases.GenerarRecibo;
using Microsoft.Reporting.WinForms;
using PuntoVentas.Clases.Login;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
        string Propietario = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;
        string Cuenta = string.Empty;

        public ReporteIngresos(string propietario, string documento, string formaPago, string fechaPago, string fechaPago1, string fechaPago2, string fechaRecibo, string fechaRecibo1, string fechaRecibo2, string anticipo, string seccion, string cuenta)
        {
            InitializeComponent();
            //Propietario1 = propietario1;
            //Propietario2 = propietario2;
            Propietario = propietario;
            Documento = documento;
            FormaPago = formaPago;
            //Propietario1C = propietario1;
            //Propietario2C = propietario2;
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
            Cuenta = cuenta;

        }

        private void ReporteIngresos_Load(object sender, EventArgs e)
        {

            Filter();


        }
        private void CargarDatos(string query, string dataSetName)
        {
            DataTable dataTable = new DataTable();

            // Crea una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(Settings.Default.ControlCondominiosConnectionString))
            {
                // Crea un comando para la consulta SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Crea un adaptador de datos para recuperar los resultados de la consulta
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Llena el DataTable con los resultados de la consulta
                        adapter.Fill(dataTable);
                    }
                }
            }

            // Agrega el DataTable como fuente de datos al informe
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(dataSetName, dataTable));

            //return dataTable;
        }
        void Filter()
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            string query = @"SELECT 
                C.*, 
                CAST(Cl.RazonSocial AS VARCHAR(MAX)) AS RazonSocial,  
                CAST(R.ClaveDocumento AS VARCHAR(MAX)) AS ClaveDocumento,  
                CAST(D.Nombre AS VARCHAR(MAX)) AS Nombre,  -- Cambié el alias aquí
                CAST(R.Saldo AS VARCHAR(MAX)) AS Saldo,  
                CAST(R.Notas AS VARCHAR(MAX)) AS Notas,  
                CAST(CB.Nombre AS VARCHAR(MAX)) AS Nombre,  -- Cambié el alias aquí
                CAST(R.Consecutivo AS VARCHAR) AS Consecutivo
            FROM 
                Cobros AS C
            JOIN 
                Remision AS R ON C.conceptoId = R.Folio and C.TipoConcepto='Remision'
            JOIN 
                Documento AS D ON R.ClaveDocumento = D.Clave
            JOIN 
                Clientes AS Cl ON R.ClaveProveedor = Cl.IdCliente
            JOIN 
                CuentasBancarias AS CB ON CB.Clave = C.CuentaBancaria";

            if (Propietario != string.Empty)
            {
                query += " and (convert(varchar,Cl.IdCliente)+' - '+ Cl.RazonSocial)='" + Propietario + "'";
            }
            if (Documento != "TODOS")
            {
                query += " and (D.Clave +'-'+ D.Nombre) = '" + Documento + "'";

            }
            if (FormaPago != "TODOS")
            {
                query += " and C.FormaPago = '" + FormaPago + "'";

            }
            //if (Seccion != "TODOS")
            //{
            //    query += " and (convert(varchar,CD.ClaveCondominio)+'-'+ CD.Descripcion) = '" + Seccion + "'";

            //}
            //if (Propiedad != string.Empty)
            //{
            //    query += " and (CS.Clave +'-'+ CS.Descripcion) = '" + Propiedad + "'";

            //}
            if (FechaPago != string.Empty)
            {
                query += " and C.Fecha between '" + FechaPago1 + "' and '" + FechaPago2 + "'";

            }
            if (FechaRecibo != string.Empty)
            {
                query += " and R.Fecha between '" + FechaRecibo1 + "' and '" + FechaRecibo2 + "'";

            }
            if (Cuenta != "0")
            {
                query += " and CuentaBancaria =  '" + Cuenta + "'";

            }
            if (Anticipo == "Si")
            {

                query += " union select C.Folio, C.ClavePropietario, c.Fecha, '' AS Observaciones, c.FormaPago AS FormaPago, C.Importe as Pago, c.Referencia AS referencia,  c.NumeroOperacion AS NumOperacion, '' AS NumAutorizacion, c.CuentaBancaria AS CuentaBancaria, 0 as FolioGeneral,'' as Archivo, '' as Extension, convert(decimal,0.00) as  Recargo, convert(decimal,0.00)  as DescuentoPago,  convert(decimal,0.00) AS SaldoRestante, '' as tipoConcepto, 0 as ConceptoId, Cl.RazonSocial, C.Concepto as ClaveDocumento, D.Descripcion as Nombre,Cast('' as varchar) as Saldo, '' as Notas, CAST(CB.Nombre AS VARCHAR(MAX)) AS Nombre, Cast(C.Folio as varchar) as Consecutivo from Anticipo as C, ConceptosIngreso as D, Clientes as Cl, CuentasBancarias as CB  where CB.Clave=C.CuentaBancaria and C.ClavePropietario=Cl.IdCliente and C.Concepto=D.Clave and (C.Activo is null or C.Activo<>'1')";

                //query += " union select C.Folio, C.ClavePropietario, c.Fecha, '' AS Observaciones, '' AS FormaPago, C.Pago, '' AS referencia,  '' AS NumOperacion, '' AS NumAutorizacion, Cast(0 as int) AS CuentaBancaria, C.FolioGeneral,'' as Archivo, '' as Extension, convert(decimal,0.00) as  Recargo, convert(decimal,0.00)  as DescuentoPago,  convert(decimal,0.00) AS SaldoRestante, Cast(R.Saldo as varchar) ,R.Propiedad, CS.Descripcion, R.Notas,' ' AS Nombre, CD.Descripcion,   P.RazonSocial, R.ClaveDocumento, D.Nombre, Cast(R.Consecutivo as varchar) from AnticipoCobros as C, Recibo as R, Documento as D, Condominios_SubCondominios as CS, Propietarios as P, Condominio as CD  where C.Folio=R.Folio and R.ClaveDocumento=D.Clave and R.Propiedad=CS.Clave and R.ClavePropietario=P.IdPropietario and CS.Condominio=CD.ClaveCondominio";
                if (Propietario != string.Empty)
                {
                    query += " and (convert(varchar,Cl.IdCliente)+' - '+ Cl.RazonSocial)='" + Propietario + "'";
                }
                //if (Documento != "TODOS")
                //{
                //    query += " and (Cast(D.Clave as varchar) +'-'+ D.Nombre) = '" + Documento + "'";

                //}
                if (FormaPago != "TODOS")
                {
                    query += " and Cast(FormaPago as varchar) = '" + FormaPago + "'";

                }
                //if (Seccion != "TODOS")
                //{
                //    query += " and (convert(varchar,CD.ClaveCondominio)+'-'+ CD.Descripcion) = '" + Seccion + "'";

                //}
                //if (Propiedad != string.Empty)
                //{
                //    query += " and (Cast(CS.Clave as varchar) +'-'+ CS.Descripcion) = '" + Propiedad + "'";

                //}

                //if (FechaRecibo != string.Empty)
                //{
                //    query += " and R.Fecha between '" + FechaRecibo1 + "' and '" + FechaRecibo2 + "'";

                //}
                if (FechaPago != string.Empty)
                {
                    query += " and C.Fecha between '" + FechaPago1 + "' and '" + FechaPago2 + "'";

                }
                if (Cuenta != "0")
                {
                    query += " and CuentaBancaria =  '" + Cuenta + "'";

                }
            }
            query += " order by consecutivo";

            CargarDatos(query, "DataSet1");
            DataTable dataTable = new DataTable();

            // Crea una conexión a la base de datos
            SqlConnection connection = new SqlConnection(Settings.Default.ControlCondominiosConnectionString);

            // Crea un comando para la consulta SQL
            SqlCommand command = new SqlCommand("SELECT RazonSocial, NombreComercial, RFC, Telefono1, Telefono2, Telefono3, Correo, PaginaWeb, CalleNumero, Colonia, Municipio, Estado, CodigoPostal, Pais, Referencias, LeyendaTicket, Foto, Contraseña, Recargos, Descuentos, RecibosAutomaticos, Documento FROM dbo.DatosEmpresa", connection);

            // Crea un adaptador de datos para recuperar los resultados de la consulta
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // Llena el DataTable con los resultados de la consulta
            adapter.Fill(dataTable);

            // Crea un objeto DataSource para el DataTable

            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dataTable));

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
            this.reportViewer1.RefreshReport();

        }


        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            /* FechaPago1 = dtFecha1.Text;
             FechaPago2 = dtFecha2.Text;
             Reporte();*/
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            /*FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();*/
        }

        private void Propi1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*cmbpropietario2.Text = cmbpropietario1.Text;

            if (cmbpropietario1.Text != "TODOS")
            {
                pro1 = cmbpropietario1.Text.IndexOf(" -");
                Propi1 = cmbpropietario1.Text.Substring(0, pro1);
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

            Reporte();*/
        }

        private void cmbpropietario2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* if (cmbpropietario2.Text == "TODOS")
             {
                 cmbpropietario1.Text = "TODOS";
                 Propietario1 = "TODOS";
                 Propietario2 = "TODOS";
             }
             if (cmbpropietario1.Text != "TODOS")
             {
                 pro1 = cmbpropietario1.Text.IndexOf(" -");
                 Propi1 = cmbpropietario1.Text.Substring(0, pro1);
                 pro2 = cmbpropietario2.Text.IndexOf(" -");
                 Propi2 = cmbpropietario2.Text.Substring(0, pro2);
                 Propietario1 = Propi1;
                 Propietario2 = Propi2;
             }

             Reporte();*/
        }

        private void dtFecha2_Leave(object sender, EventArgs e)
        {
            /* if (dtFecha2.Value < dtFecha1.Value)
             {
                 dtFecha2.Text = dtFecha1.Text;
                 MessageBox.Show("La fecha final del rango no puede ser menor a la inicial.");
             }*/
        }

        void Reporte()
        {
            ControlCondominiosDataSet21.EnforceConstraints = false;


            if (Anticipo == "No")
            {
                if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.Fill(this.ControlCondominiosDataSet21.Cobros);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("1");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2));
                    this.reportViewer1.RefreshReport();
                    /// MessageBox.Show("2");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy1(this.ControlCondominiosDataSet21.Cobros, Documento);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("3");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy2(this.ControlCondominiosDataSet21.Cobros, FormaPago);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("4");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy3(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("5");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy4(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //MessageBox.Show("6");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy5(this.ControlCondominiosDataSet21.Cobros, Seccion);
                    this.reportViewer1.RefreshReport();
                    //MessageBox.Show("7");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy6(this.ControlCondominiosDataSet21.Cobros, Propiedad);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("8");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy143(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("9");
                }
                //___________

                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy7(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("10");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy8(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("11");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy9(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //     MessageBox.Show("12");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy10(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("13");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy11(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("14");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy12(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("15");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy13(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("16");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy14(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("17");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy15(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //     MessageBox.Show("18");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy16(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("19");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy17(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("20");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy19(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("21");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy20(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("22");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy21(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("23");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy22(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("23");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy23(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("25");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy24(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("26");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy25(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("27");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy26(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("28");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy27(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("29");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy28(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    // MessageBox.Show("30");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy87(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //     MessageBox.Show("31");

                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy88(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("32");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy89(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("33");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy90(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //MessageBox.Show("34");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy91(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("35");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy92(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("36");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy93(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //     MessageBox.Show("37");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy94(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("38");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy95(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("39");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy96(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("40");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy97(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("41");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy98(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("42");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy99(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("43");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy100(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("44");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy101(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("45");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy102(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("46");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy103(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //     MessageBox.Show("47");
                }
                else if ((Propietario1 != "TODOS" && Propietario2 != "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy104(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), FechaRecibo1, FechaRecibo2, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("48");
                }
                //______________
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy29(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("49");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy30(this.ControlCondominiosDataSet21.Cobros, Documento, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //      MessageBox.Show("50");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy31(this.ControlCondominiosDataSet21.Cobros, Documento, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("51");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy32(this.ControlCondominiosDataSet21.Cobros, Documento, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("52");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy33(this.ControlCondominiosDataSet21.Cobros, Documento, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("53");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy34(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaPago1, FechaPago2);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("54");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy35(this.ControlCondominiosDataSet21.Cobros, FormaPago, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("55");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy36(this.ControlCondominiosDataSet21.Cobros, FormaPago, Seccion);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("56");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy37(this.ControlCondominiosDataSet21.Cobros, FormaPago, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("57");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy38(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("58");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy39(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show(Seccion);
                    //MessageBox.Show("59");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy40(this.ControlCondominiosDataSet21.Cobros, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("60");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy41(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("61");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy42(this.ControlCondominiosDataSet21.Cobros, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //  MessageBox.Show("62");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento == "TODOS" && FormaPago == "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy43(this.ControlCondominiosDataSet21.Cobros, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("63");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy123(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("64");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy124(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("65");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy125(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("66");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy126(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Seccion);
                    this.reportViewer1.RefreshReport();
                    //   MessageBox.Show("67");
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo != string.Empty && Seccion == "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy127(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaRecibo1, FechaRecibo2, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("68");

                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago == string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad != string.Empty)
                {
                    this.CobrosTableAdapter.FillBy128(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, Seccion, Propiedad);
                    this.reportViewer1.RefreshReport();
                    //    MessageBox.Show("69");
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
                    this.CobrosTableAdapter.FillBy105(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FormaPago, FechaPago1, FechaPago2);
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
                    this.CobrosTableAdapter.FillBy110(this.ControlCondominiosDataSet21.Cobros, Convert.ToInt32(Propietario1), Convert.ToInt32(Propietario2), Documento, FechaPago1, FechaPago2, Seccion);
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
                    this.CobrosTableAdapter.FillBy133(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, FechaRecibo1, FechaRecibo2);
                    this.reportViewer1.RefreshReport();
                }
                else if ((Propietario1 == "TODOS" && Propietario2 == "TODOS") && Documento != "TODOS" && FormaPago != "TODOS" && FechaPago != string.Empty && FechaRecibo == string.Empty && Seccion != "TODOS" && Propiedad == string.Empty)
                {
                    this.CobrosTableAdapter.FillBy134(this.ControlCondominiosDataSet21.Cobros, Documento, FormaPago, FechaPago1, FechaPago2, Seccion);
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
            /* if (cbFechas.Checked == true)
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
             Reporte();*/
        }

        private void dtFecha1_ValueChanged_1(object sender, EventArgs e)
        {
            /*FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();*/
        }

        private void dtFecha2_ValueChanged_1(object sender, EventArgs e)
        {
            /*FechaPago1 = dtFecha1.Text;
            FechaPago2 = dtFecha2.Text;
            Reporte();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Propietario1 = "TODOS";
            Propietario2 = "TODOS";
            Propietario1C = string.Empty;
            Propietario2C = string.Empty;

            // cmbpropietario1.Text = Propietario1;
            // cmbpropietario2.Text = Propietario2;

            Documento = DocumentoC;
            FormaPago = FormaPagoC;
            Propiedad = string.Empty;

            /*if (Propietario1 == string.Empty)
            {
                cmbpropietario1.SelectedIndex = 0;
                cmbpropietario2.SelectedIndex = 0;
            }
            else if (Propietario1 != string.Empty && Propietario1 != "TODOS")
            {
                cmbpropietario1.SelectedIndex = Convert.ToInt32(Propietario1);
                cmbpropietario2.SelectedIndex = Convert.ToInt32(Propietario2);
            }*/

            /*if (FechaPago == "Si")
            {
                cbFechas.Checked = true;
            }

            dtFecha1.Text = FechaPago1;
            dtFecha2.Text = FechaPago2;*/

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
            /*cmbpropietario1.Text = Propietario1;
            cmbpropietario2.Text = Propietario2;

            if (cmbpropietario2.Text == "TODOS")
            {
                cmbpropietario1.Text = "TODOS";
                Propietario1 = "TODOS";
                Propietario2 = "TODOS";
            }
            if (cmbpropietario1.Text != "TODOS")
            {
                pro1 = cmbpropietario1.Text.IndexOf(" -");
                Propi1 = cmbpropietario1.Text.Substring(0, pro1);
                pro2 = cmbpropietario2.Text.IndexOf(" -");
                Propi2 = cmbpropietario2.Text.Substring(0, pro2);
                Propietario1 = Propi1;
                Propietario2 = Propi2;
            }

            if (Propietario1 == string.Empty)
            {
                cmbpropietario1.SelectedIndex = 0;
                cmbpropietario2.SelectedIndex = 0;
            }
            else if (Propietario1 != string.Empty && Propietario1 != "TODOS")
            {
                cmbpropietario1.SelectedIndex = Convert.ToInt32(Propietario1);
                cmbpropietario2.SelectedIndex = Convert.ToInt32(Propietario2);
            }

            if (FechaPago == "Si")
            {
                cbFechas.Checked = true;
            }

            dtFecha1.Text = FechaPago1;
            dtFecha2.Text = FechaPago2;*/

            //if (Propietario1 != "" && Propietario1 != "TODOS")
            //{
            //    pro1 = Propietario1.IndexOf(" -");
            //    Propi1 = Propietario1.Substring(0, pro1);
            //    pro2 = Propietario1.IndexOf(" -");
            //    Propi2 = Propietario1.Substring(0, pro2);
            //    Propietario1 = Propi1;
            //    Propietario2 = Propi2;
            //}
            //else
            //{
            //    Propietario1 = "TODOS";
            //    Propietario2 = "TODOS";
            //}

            //Reporte();
            Filter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltroPropiedad filtroPropiedad = new FiltroPropiedad();
            filtroPropiedad.ShowDialog();
        }
    }
}
