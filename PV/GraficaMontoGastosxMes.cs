using PV.Clases.Graficas;
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
    public partial class GraficaMontoGastosxMes : Form
    {
        DBGraficas c = new DBGraficas();
        public GraficaMontoGastosxMes()
        {
            InitializeComponent();
        }
        private void CargarGrafica()
        {
            // Obtener el año seleccionado desde el control (asegúrate que sea numérico)
            int añoSeleccionado = int.Parse(dtFiltroAño.Text.ToString());

            // Obtener todos los datos
            DataTable dt = c.ObtenerDatosGastosPorProyectoYMes();

            // Filtrar por el año seleccionado
            var filasFiltradas = dt.AsEnumerable()
                .Where(r => r.Field<int>("Año") == añoSeleccionado);

            // Si no hay datos para ese año, salir y limpiar el gráfico
            if (!filasFiltradas.Any())
            {
                MessageBox.Show("No hay datos disponibles para el año seleccionado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gunaChart1.Datasets.Clear();
                gunaChart1.Update();
                return;
            }

            // Convertir a DataTable solo si hay datos
            var dtFiltrado = filasFiltradas.CopyToDataTable();

            // Preparar el gráfico
            gunaChart1.Datasets.Clear();
            gunaChart1.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            gunaChart1.Misc.BarCornerRadius = 10;
            gunaChart1.YAxes.GridLines.Display = false;

            // Obtener proyectos y meses distintos
            var proyectos = dtFiltrado.AsEnumerable()
                .Select(r => r.Field<string>("Proyecto"))
                .Distinct();

            var meses = dtFiltrado.AsEnumerable()
                .Select(r => r.Field<string>("Mes"))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            // Crear datasets por proyecto
            foreach (var proyecto in proyectos)
            {
                var dataset = new Guna.Charts.WinForms.GunaBarDataset
                {
                    Label = "Proyecto " + proyecto,
                    FillColors = Guna.Charts.WinForms.ChartUtils.RandomColors(meses.Count, 90)
                };

                foreach (var mes in meses)
                {
                    var monto = dtFiltrado.AsEnumerable()
                        .Where(r => r.Field<string>("Proyecto") == proyecto && r.Field<string>("Mes") == mes)
                        .Select(r => r.Field<decimal>("Monto"))
                        .FirstOrDefault();

                    dataset.DataPoints.Add(mes, (double)monto);
                }

                gunaChart1.Datasets.Add(dataset);
            }

            // Actualizar el gráfico
            gunaChart1.Update();
        }


        private void GraficaMontoGastosxMes_Load(object sender, EventArgs e)
        {
            CargarGrafica();
        }

        private void dtFiltroAño_ValueChanged(object sender, EventArgs e)
        {
            CargarGrafica();
        }
    }
}
