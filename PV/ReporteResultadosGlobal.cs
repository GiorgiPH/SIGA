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
        private int CentroCostos = 0;
        private int Proyecto = 0;
        private string NombreCentroCostos = "";
        private string NombreProyecto = "";

        public ReporteResultadosGlobal(
            string mes,
            string año,
            string notas,
            int centrocostos,
            int proyecto,
            string Nombrecentrocostos,
            string Nombreproyecto)
        {
            InitializeComponent();

            administracionDataSet1 = new AdministracionDataSet1();
            administracionDataSet2 = new AdministracionDataSet2();

            if (!int.TryParse(mes, out Mes))
            {
                Mes = 0;
            }

            if (!int.TryParse(año, out Año))
            {
                Año = 0;
            }

            CentroCostos = centrocostos;
            Proyecto = proyecto;

            NombreCentroCostos = Nombrecentrocostos ?? "";
            NombreProyecto = Nombreproyecto ?? "";

            Notas = notas ?? "";

            partidaRegistroGastosTableAdapter =
                new PartidaRegistroGastosTableAdapter();

            datosEmpresaTableAdapter =
                new DatosEmpresaTableAdapter();
        }

        private void ReporteResultadosGlobal_Load(object sender, EventArgs e)
        {
            try
            {
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

                DataTable resultados;

                if (CentroCostos != 0)
                {
                    int? idProyecto;

                    if (Proyecto != 0)
                    {
                        idProyecto = Proyecto;
                    }
                    else
                    {
                        idProyecto = null;
                    }

                    resultados =
                        partidaRegistroGastosTableAdapter.GetDataBy(
                            Mes,
                            Año,
                            CentroCostos,
                            idProyecto
                        );
                 
                }
                else
                {
                    resultados =
                        partidaRegistroGastosTableAdapter.GetData(
                            Mes,
                            Año
                        );
                }

                DataTable empresa =
                    datosEmpresaTableAdapter.GetData();

                if (resultados == null)
                {
                    resultados = new DataTable();
                }

                if (empresa == null)
                {
                    empresa = new DataTable();
                }

                reportViewer1.LocalReport.ReportEmbeddedResource =
                    "PV.ReporteResultadosGlobal.rdlc";

                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource dataSource1 =
                    new ReportDataSource(
                        "DataSet1",
                        resultados
                    );

                ReportDataSource dataSource2 =
                    new ReportDataSource(
                        "DataSet2",
                        empresa
                    );

                reportViewer1.LocalReport.DataSources.Add(
                    dataSource1
                );

                reportViewer1.LocalReport.DataSources.Add(
                    dataSource2
                );

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
                        Notas
                    );

                ReportParameter parametroCentroCostos =
                    new ReportParameter(
                        "CentroCostos",
                        NombreCentroCostos
                    );

                ReportParameter parametroProyecto =
                    new ReportParameter(
                        "Proyecto",
                        NombreProyecto
                    );

                ReportParameter[] parametros =
                {
                    parametroMes,
                    parametroAño,
                    parametroNotas,
                    parametroCentroCostos,
                    parametroProyecto
                };

                reportViewer1.LocalReport.SetParameters(
                    parametros
                );

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