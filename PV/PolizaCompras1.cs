using Microsoft.Reporting.WinForms;
using PuntoVentas.Clases.Login;
using PV.Clases;
using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PV
{
    public partial class PolizaCompras1 : Form
    {
        string Periodo = string.Empty;
        string Rangodias = string.Empty;
        string TipoPD = string.Empty;
        string NoPoliza = string.Empty;
        string Diario = string.Empty;
        string Concepto = string.Empty;
        string FechaI = string.Empty;
        string FechaF = string.Empty;
        string TipoPoliza = string.Empty;
        string poliza = string.Empty;
        string Documento = string.Empty;
        DBLogin c = new DBLogin();

        string cuentafija = string.Empty;
        string Cuentapropietario = string.Empty;
        string Cuentadocumento = string.Empty;
        //   string CuentaIng = string.Empty;
        string Cuentabanco = string.Empty;
        string Cuentaalmacen = string.Empty;
        string CuentamovimientoInv = string.Empty;
        string Cuentaproveedores = string.Empty;
        string CuentaproductoServicios = string.Empty;
        string CuentaconceptosGoblales = string.Empty;

        string cuentafijaA = string.Empty;
        string CuentapropietarioA = string.Empty;
        string CuentadocumentoA = string.Empty;
        string CuentaCentroCosto = string.Empty;
        string CuentaCentroCostoDep = string.Empty;
        string CuentabancoA = string.Empty;
        string CuentaalmacenA = string.Empty;
        string CuentamovimientoInvA = string.Empty;
        string CuentaproveedoresA = string.Empty;
        string CuentaproductoServiciosA = string.Empty;
        string CuentaconceptosGoblalesA = string.Empty;
        string CuentaCentroCostoA = string.Empty;
        string CuentaCentroCostoDepA = string.Empty;
        string Separador = string.Empty;
        string Exportar=string.Empty;


        public PolizaCompras1(string periodo, string rangod, string TipopolizaDocu, string numpeoliza, string diario, string concepto, string fechai, string fechaf, string tipopoliza, string Poliza, string cccp, string CuentaPropietario, string CuentaDocumento, string CuentaBanco, string CuentaAlmacen, string CuentaMoviInv, string CuentaProv, string CuentaProductoServ, string CuentaConceptosGlobales, string CentroCosto, string CuentaCentroCostoDep, string Cuentafija, string CuentaPropietarioA, string CuentaDocumentoA, string CuentaBancoA, string CuentaAlmacenA, string CuentaMoviInvA, string CuentaProvA, string CuentaProductoServA, string CuentaConceptosGlobalesA, string CentroCostoA, string CuentaCentroCostoDepA, string separador, string exportar)
        {
            InitializeComponent();
            Periodo = periodo;
            Rangodias = rangod;
            TipoPD = TipopolizaDocu;
            NoPoliza = numpeoliza;
            Diario = diario;
            Concepto = concepto;
            FechaI = fechai;
            FechaF = fechaf;
            TipoPoliza = tipopoliza;
            poliza = Poliza;
            cuentafija = cccp;
            Cuentapropietario = CuentaPropietario;
            Cuentadocumento = CuentaDocumento;
            Cuentabanco = CuentaBanco;
            Cuentaalmacen = CuentaAlmacen;
            CuentamovimientoInv = CuentaMoviInv;
            Cuentaproveedores = CuentaProv;
            CuentaproductoServicios = CuentaProductoServ;
            CuentaconceptosGoblales = CuentaConceptosGlobales;
            CuentaCentroCosto = CentroCosto;
            cuentafijaA = Cuentafija;
            CuentapropietarioA = CuentaPropietarioA;
            CuentadocumentoA = CuentaDocumentoA;
            CuentabancoA = CuentaBancoA;
            CuentaalmacenA = CuentaAlmacenA;
            CuentamovimientoInvA = CuentaMoviInvA;
            CuentaproveedoresA = CuentaProvA;
            CuentaproductoServiciosA = CuentaProductoServA;
            CuentaconceptosGoblalesA = CuentaConceptosGlobalesA;
            CuentaCentroCostoA = CentroCostoA;
            Separador = separador;
            Exportar = exportar;
            this.CuentaCentroCostoDep = CuentaCentroCostoDep;
            this.CuentaCentroCostoDepA= CuentaCentroCostoDepA;
        }

        private void PolizaCompras1_Load(object sender, EventArgs e)
        {
            //   MessageBox.Show(Periodo);

            if (poliza == "Define Póliza Compras Almacén")
            {
                Documento = "  Compras Almacén";
            }
            else if (poliza == "'Define Póliza Compras Gastos")
            {
                Documento = " Compras Gastos";
            }
            else if (poliza == "Define Póliza Egresos")
            {
                Documento = " Egresos";
            }
            else if (poliza == "Define Póliza Salidas Almacén")
            {
                Documento = " Salidas Almacén";
            }else if (poliza == "Define Póliza Entradas Inventariables")
            {
                Documento = " Entradas almacen";
            }
            else
            {
                Documento = poliza;
            }
            string Resumida = "Si";

            //  Documento = poliza;
           // c.Torre(Torre);
            ReportParameter[] parameters = new ReportParameter[9];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("Periodo", Periodo);
            parameters[1] = new ReportParameter("Rangodias", Rangodias);
            parameters[2] = new ReportParameter("TipoP", TipoPoliza);
            parameters[3] = new ReportParameter("NoPoliza", NoPoliza);
            parameters[4] = new ReportParameter("Diario", Diario);
            parameters[5] = new ReportParameter("Concepto", Concepto);
            parameters[6] = new ReportParameter("Documento", Documento);
            parameters[7] = new ReportParameter("Torre", Torre.Text);
            parameters[8] = new ReportParameter("Resumida", Resumida);



            this.DTSPolizaCompra1.EnforceConstraints = false;
            this.reportViewer1.LocalReport.SetParameters(parameters);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet23.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'DTSPolizaCompra1.ReporteGeneracionPolizasCompras' Puede moverla o quitarla según sea necesario.
            this.ReporteGeneracionPolizasComprasTableAdapter.Fill(this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras, TipoPD, FechaI, FechaF, poliza, cuentafija, Cuentapropietario, Cuentadocumento, Cuentabanco, Cuentaalmacen, CuentamovimientoInv, Cuentaproveedores, CuentaproductoServicios, CuentaconceptosGoblales, CuentaCentroCosto, CuentaCentroCostoDep, cuentafijaA, CuentapropietarioA, CuentadocumentoA, CuentabancoA, CuentaalmacenA, CuentamovimientoInvA, CuentaproveedoresA, CuentaproductoServiciosA, CuentaconceptosGoblalesA, CuentaCentroCostoA, CuentaCentroCostoDepA, Separador);
            if (Exportar == "SI")
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos de Excel 2003 (*.xls)|*.xls|Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*";
                    saveFileDialog.Title = "Guardar Archivo Excel";
                    saveFileDialog.FileName = "reporte"; // Nombre predeterminado del archivo

                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        string rutaArchivo = saveFileDialog.FileName;

                        // Crear una instancia de Excel
                        //Excel.Application excelApp = new Excel.Application();
                        dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));

                        excelApp.Visible = false;

                        // Crear un nuevo libro de trabajo
                        Excel.Workbook workbook = excelApp.Workbooks.Add();
                        Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                        // Escribir los encabezados de las columnas

                        worksheet.Cells[1, 1] = "P";
                        //MessageBox.Show(FechaF);
                        worksheet.Cells[1, 2].NumberFormat = "aaaammdd";
                        string fechaFormateada = Convert.ToDateTime(FechaF).ToString("dd/MM/yyyy");
                        // Establecer el valor de la celda con la fecha formateada
                        worksheet.Cells[1, 2].Value = Convert.ToDateTime(fechaFormateada);
                        // Aplicar el formato personalizado a la celda

                        worksheet.Cells[1, 3] = "3";
                        worksheet.Cells[1, 4] = NoPoliza;
                        worksheet.Cells[1, 5] = "1";
                        worksheet.Cells[1, 6] = "0";
                        worksheet.Cells[1, 7] = "Notas";
                        worksheet.Cells[1, 8] = "11";
                        worksheet.Cells[1, 9] = "0";
                        worksheet.Cells[1, 10] = "0";


                        // Escribir los datos en las celdas
                        for (int i = 0; i < this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows.Count; i++)
                        {
                            //0 cargo
                            //1 abono
                            worksheet.Cells[i + 2, 1] = "M1";
                            worksheet.Cells[i + 2, 2] = Utilerias.QuitarNoNumeros(this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][2].ToString());
                            worksheet.Cells[i + 2, 3] = this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][4];
                            worksheet.Cells[i + 2, 4] = this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][5].ToString() != "0.00" ? "0" : "1";
                            worksheet.Cells[i + 2, 5] = this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][5].ToString() != "0.00" ? this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][5] : this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][6];
                            //worksheet.Cells[i + 2, 4] = i % 2 == 0 ? "0" : "1";
                            //worksheet.Cells[i + 2, 5] = i % 2 == 0 ? this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][5] : this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][6];
                            worksheet.Cells[i + 2, 6] = "0";
                            worksheet.Cells[i + 2, 7] = "0";
                            worksheet.Cells[i + 2, 8] = this.DTSPolizaCompra1.ReporteGeneracionPolizasCompras.Rows[i][8];


                        }
                        worksheet.Columns[2].AutoFit();
                        // Guardar el libro de trabajo en la ruta seleccionada por el usuario
                        workbook.SaveAs(rutaArchivo, Excel.XlFileFormat.xlWorkbookNormal);

                        // Cerrar Excel
                        excelApp.Quit();
                    }
                }
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
