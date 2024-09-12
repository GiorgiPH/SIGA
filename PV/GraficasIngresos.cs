using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.Graficas;

namespace PV
{
    public partial class GraficasIngresos : Form
    {
        DBGraficas c = new DBGraficas();

        public GraficasIngresos()
        {
            InitializeComponent();
        }

        void graficas()
        {
            //chart1.Series["Series1"].Points.Clear();
            //chart1.ChartAreas.Clear();
            //chart1.Series.Clear();

            //chart1.Series.Add("Series1");
            //chart1.ChartAreas.Add("ChartArea1");
            chart2.Series["Series1"].LegendText = "Ingresos";

            Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
            dic.Add("Ene", DBGraficas.Enero);
            dic.Add("Feb", DBGraficas.Febrero);
            dic.Add("Mar", DBGraficas.Marzo);
            dic.Add("Abr", DBGraficas.Abril);
            dic.Add("May", DBGraficas.Mayo);
            dic.Add("Jun", DBGraficas.Junio);
            dic.Add("Jul", DBGraficas.Julio);
            dic.Add("Ago", DBGraficas.Agosto);
            dic.Add("Sep", DBGraficas.Septiembre);
            dic.Add("Oct", DBGraficas.Octubre);
            dic.Add("Nov", DBGraficas.Noviembre);
            dic.Add("Dic", DBGraficas.Diciembre);

            foreach (KeyValuePair<string, decimal> d in dic)
            {
                chart2.Series["Series1"].Points.AddXY(d.Key, d.Value);
            }
        }

        private void GraficasIngresos_Load_1(object sender, EventArgs e)
        {
            dtFiltroAño.Format = DateTimePickerFormat.Custom;
            dtFiltroAño.CustomFormat = "yyyy";
            dtFiltroAño.ShowUpDown = true;
            c.GraficaConsultas(dtFiltroAño.Text);
            graficas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtFiltroAño_ValueChanged(object sender, EventArgs e)
        {
            chart2.Series["Series1"].Points.Clear();
            //chart1.ChartAreas.Clear();
            chart2.Series.Clear();

            chart2.Series.Add("Series1");
            chart2.Series["Series1"].Label = "#VALY";
            chart2.Series["Series1"].SetCustomProperty("LabelStyle", "Bottom");
            //chart1.ChartAreas.Add("ChartArea1");
            c.GraficaConsultas(dtFiltroAño.Text);
            graficas();
        }
    }
}
