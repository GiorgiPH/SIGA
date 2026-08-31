using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PV.AdministracionDataSet1TableAdapters;
using PV.AdministracionDataSet2TableAdapters;

namespace PV
{
    public partial class ReporteResultadosGlobal : Form
    {
        private AdministracionDataSet1 administracionDataSet1;
        private AdministracionDataSet2 administracionDataSet2;

        private PartidaRegistroGastosTableAdapter partidaRegistroGastosTableAdapter;
        private DatosEmpresaTableAdapter datosEmpresaTableAdapter;

        private int Mes = 0;
        private int Año = 0;
        private string Notas = "";

        public ReporteResultadosGlobal(string mes, string año, string notas)
        {
            InitializeComponent();

            // Inicializar DataSets
            administracionDataSet1 = new AdministracionDataSet1();
            administracionDataSet2 = new AdministracionDataSet2();

            // Convertir parámetros
            if (!int.TryParse(mes, out Mes))
            {
                Mes = 0;
            }

            if (!int.TryParse(año, out Año))
            {
                Año = 0;
            }

            // Evitar que Notas sea null
            Notas = notas ?? "";

            // Inicializar TableAdapters
            partidaRegistroGastosTableAdapter =
                new PartidaRegistroGastosTableAdapter();

            datosEmpresaTableAdapter =
                new DatosEmpresaTableAdapter();
        }

        private void ReporteResultadosGlobal_Load(object sender, EventArgs e)
        {
            try
            {
                // =========================================================
                // VALIDACIONES
                // =========================================================

                if (reportViewer1 == null)
                {
                    throw new Exception(
                        "El control reportViewer1 no está inicializado."
                    );
                }

                if (reportViewer1.LocalReport == null)
                {
                    throw new Exception(
                        "LocalReport no está inicializado."
                    );
                }

                if (partidaRegistroGastosTableAdapter == null)
                {
                    throw new Exception(
                        "PartidaRegistroGastosTableAdapter no está inicializado."
                    );
                }

                if (datosEmpresaTableAdapter == null)
                {
                    throw new Exception(
                        "DatosEmpresaTableAdapter no está inicializado."
                    );
                }

                // =========================================================
                // OBTENER INFORMACIÓN
                // =========================================================

                DataTable resultados =
                    partidaRegistroGastosTableAdapter.GetData(Mes, Año);

                DataTable empresa =
                    datosEmpresaTableAdapter.GetData();

                // Evitar DataTables null
                if (resultados == null)
                {
                    resultados = new DataTable();
                }

                if (empresa == null)
                {
                    empresa = new DataTable();
                }

                // =========================================================
                // CONFIGURAR REPORTE
                // =========================================================

                reportViewer1.LocalReport.ReportEmbeddedResource =
                    "PV.ReporteResultadosGlobal.rdlc";

                // =========================================================
                // LIMPIAR DATASOURCES
                // =========================================================

                reportViewer1.LocalReport.DataSources.Clear();

                // =========================================================
                // DATASOURCE RESULTADOS
                // =========================================================

                ReportDataSource dataSource1 =
                    new ReportDataSource(
                        "DataSet1",
                        resultados
                    );

                // =========================================================
                // DATASOURCE EMPRESA
                // =========================================================

                ReportDataSource dataSource2 =
                    new ReportDataSource(
                        "DataSet2",
                        empresa
                    );

                // =========================================================
                // AGREGAR DATASOURCES
                // =========================================================

                reportViewer1.LocalReport.DataSources.Add(dataSource1);
                reportViewer1.LocalReport.DataSources.Add(dataSource2);

                // =========================================================
                // PARÁMETROS DEL REPORTE
                // =========================================================

                ReportParameter parametroMes =
                    new ReportParameter(
                        "Mes",
                        Mes.ToString()
                    );

                ReportParameter parametroAño =
                    new ReportParameter(
                        "Año",
                        Año.ToString()
                    );

                ReportParameter parametroNotas =
                    new ReportParameter(
                        "Notas",
                        Notas ?? ""
                    );

                ReportParameter[] parametros =
                {
                    parametroMes,
                    parametroAño,
                    parametroNotas
                };

                // =========================================================
                // ESTABLECER PARÁMETROS
                // =========================================================

                reportViewer1.LocalReport.SetParameters(parametros);

                // =========================================================
                // ACTUALIZAR REPORTE
                // =========================================================

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al generar el reporte.\n\n" +
                    ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}