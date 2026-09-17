using Condominios.Clases.CentroCostos;
using System;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class FiltrarReporteResultadosGlobal : Form
    {
        private readonly DBResultadoGlobal dbResultadoGlobal = new DBResultadoGlobal();

        public FiltrarReporteResultadosGlobal()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(cmbMes.SelectedValue);
            int año = Convert.ToInt32(cmbAños.SelectedValue);
            int centroCostos = Convert.ToInt32(cmbCentroCostos.SelectedValue);
            int idProyecto = Convert.ToInt32(cmbProyecto.SelectedValue);
            string NombrecentroCostos = cmbCentroCostos.Text;
            string NombreidProyecto = cmbProyecto.Text;

            ReporteResultadosGlobal reporteResultadoGlobal = new ReporteResultadosGlobal(
                mes.ToString(),
                año.ToString(),
                txtNotas.Text,
                centroCostos,
                idProyecto,
                NombrecentroCostos,
                NombreidProyecto
            );

            reporteResultadoGlobal.ShowDialog();
        }

        private void FiltrarReporteResultadosGlobal_Load(object sender, EventArgs e)
        {
            CargarMeses();
            CargarAños();
            CargarCentrosCostos();
            LimpiarProyectos();

            cmbProyecto.Enabled = false;
        }

        private void CargarMeses()
        {
            DataTable dtMeses = new DataTable();

            dtMeses.Columns.Add("Numero", typeof(int));
            dtMeses.Columns.Add("Nombre", typeof(string));

            dtMeses.Rows.Add(1, "Enero");
            dtMeses.Rows.Add(2, "Febrero");
            dtMeses.Rows.Add(3, "Marzo");
            dtMeses.Rows.Add(4, "Abril");
            dtMeses.Rows.Add(5, "Mayo");
            dtMeses.Rows.Add(6, "Junio");
            dtMeses.Rows.Add(7, "Julio");
            dtMeses.Rows.Add(8, "Agosto");
            dtMeses.Rows.Add(9, "Septiembre");
            dtMeses.Rows.Add(10, "Octubre");
            dtMeses.Rows.Add(11, "Noviembre");
            dtMeses.Rows.Add(12, "Diciembre");

            cmbMes.DataSource = dtMeses;
            cmbMes.DisplayMember = "Nombre";
            cmbMes.ValueMember = "Numero";

            cmbMes.SelectedValue = DateTime.Now.Month;
        }

        private void CargarAños()
        {
            DataTable dtAños = new DataTable();

            dtAños.Columns.Add("Numero", typeof(int));
            dtAños.Columns.Add("Nombre", typeof(string));

            int añoActual = DateTime.Now.Year;

            for (int año = añoActual; año >= 1990; año--)
            {
                dtAños.Rows.Add(año, año.ToString());
            }

            cmbAños.DataSource = dtAños;
            cmbAños.DisplayMember = "Nombre";
            cmbAños.ValueMember = "Numero";

            cmbAños.SelectedValue = añoActual;
        }

        private void CargarCentrosCostos()
        {
            DataTable dtOrigen = dbResultadoGlobal.ObtenerCentrosCostos();

            DataTable dtCentros = new DataTable();

            dtCentros.Columns.Add("Clave", typeof(int));
            dtCentros.Columns.Add("Nombre", typeof(string));

            dtCentros.Rows.Add(0, "Seleccionar");

            foreach (DataRow row in dtOrigen.Rows)
            {
                dtCentros.Rows.Add(
                    row["Clave"],
                    row["Nombre"]
                );
            }

            cmbCentroCostos.DataSource = dtCentros;
            cmbCentroCostos.DisplayMember = "Nombre";
            cmbCentroCostos.ValueMember = "Clave";

            cmbCentroCostos.SelectedValue = 0;
        }

        private void LimpiarProyectos()
        {
            DataTable dtProyectos = new DataTable();

            dtProyectos.Columns.Add("Id", typeof(int));
            dtProyectos.Columns.Add("Proyecto", typeof(string));

            dtProyectos.Rows.Add(0, "Seleccionar");

            cmbProyecto.DataSource = dtProyectos;
            cmbProyecto.DisplayMember = "Proyecto";
            cmbProyecto.ValueMember = "Id";

            cmbProyecto.SelectedValue = 0;
        }

        private void CargarProyectos(int centroCostos)
        {
            DataTable dtOrigen = dbResultadoGlobal.ObtenerProyectos(centroCostos);

            DataTable dtProyectos = new DataTable();

            dtProyectos.Columns.Add("Id", typeof(int));
            dtProyectos.Columns.Add("Proyecto", typeof(string));

            dtProyectos.Rows.Add(0, "Seleccionar");

            foreach (DataRow row in dtOrigen.Rows)
            {
                dtProyectos.Rows.Add(
                    row["Id"],
                    row["Proyecto"]
                );
            }

            cmbProyecto.DataSource = dtProyectos;
            cmbProyecto.DisplayMember = "Proyecto";
            cmbProyecto.ValueMember = "Id";

            cmbProyecto.SelectedValue = 0;
        }


        private void cmbCentroCostos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbCentroCostos.SelectedValue == null)
            {
                LimpiarProyectos();
                cmbProyecto.Enabled = false;
                return;
            }
            int centroCostos;

            if (!int.TryParse(cmbCentroCostos.SelectedValue.ToString(), out centroCostos))
            {
                LimpiarProyectos();
                cmbProyecto.Enabled = false;
                return;
            }

            if (centroCostos == 0)
            {
                LimpiarProyectos();
                cmbProyecto.Enabled = false;
                return;
            }

            CargarProyectos(centroCostos);
            cmbProyecto.Enabled = true;
        }
    }
}