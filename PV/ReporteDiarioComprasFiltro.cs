using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Condominios.Clases.CentroCostos;
using Condominios.Clases.Documentos;
using PV.Clases.Proveedores;
using PV.Clases.ReporteCompras;

namespace PV
{
    public partial class ReporteDiarioComprasFiltro : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        DBProveedores p = new DBProveedores();
        DBDocumentos d = new DBDocumentos();
        DBCentroCostos centroCostos = new DBCentroCostos();

        string provedor = string.Empty;
        string cuentaBancaria = string.Empty;
        string fecha = string.Empty;
        string fecha1 = string.Empty;
        string fecha2 = string.Empty;
        string tipo = "";

        public ReporteDiarioComprasFiltro(string tipo="")
        {
            InitializeComponent();
            this.tipo = tipo;
            if (tipo == "Diario Gastos")
            {
               
            }
            else if (tipo == "Diario Reembolsos")
            {
                pnAnioSemana.Visible = true;
                pnCentroCostos.Visible = true;
            }
        }


        private void LlenarComboProveedores()
        {
            try
            {
                DataTable menus = p.ConsultarProveedores();
                // Crear fila "TODOS"
                DataRow filaTodos = menus.NewRow();
                filaTodos["IdProveedor"] = "0"; // Asegúrate de que la columna "Clave" exista
                filaTodos["RazonSocial"] = "TODOS"; // Asegúrate de que la columna "Clave" exista

                menus.Rows.InsertAt(filaTodos, 0); // Insertar al principio
                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbPropietario1.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbPropietario1.DataSource = menus;
                cmbPropietario1.DisplayMember = "RazonSocial"; // Campo visible
                cmbPropietario1.ValueMember = "IdProveedor";   // Campo interno
                cmbPropietario1.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarComboDocumentos()
        {
            try
            {
                DataTable menus = d.ConsultarDocumento("Compra", "Compra");
                // Crear fila "TODOS"
                DataRow filaTodos = menus.NewRow();
                filaTodos["Clave"] = ""; // Asegúrate de que la columna "Clave" exista
                filaTodos["Nombre"] = "TODOS"; // Asegúrate de que la columna "Clave" exista

                menus.Rows.InsertAt(filaTodos, 0); // Insertar al principio
                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbTipo.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbTipo.DataSource = menus;
                cmbTipo.DisplayMember = "Nombre"; // Campo visible
                cmbTipo.ValueMember = "Clave";   // Campo interno
                cmbTipo.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarComboCentroCostos()
        {
            try
            {
                DataTable menus =  centroCostos.ConsultarTodos();
                // Crear fila "TODOS"
                DataRow filaTodos = menus.NewRow();
                filaTodos["Clave"] = "0"; // Asegúrate de que la columna "Clave" exista
                filaTodos["Nombre"] = "TODOS"; // Asegúrate de que la columna "Clave" exista

                menus.Rows.InsertAt(filaTodos, 0); // Insertar al principio
                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbCentroCostos.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbCentroCostos.DataSource = menus;
                cmbCentroCostos.DisplayMember = "Nombre"; // Campo visible
                cmbCentroCostos.ValueMember = "Clave";   // Campo interno
                cmbCentroCostos.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

                //cmbCentroCostos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbCentroCostos.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Reanudar eventos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarComboSemanas()
        {
            try
            {
                List<SemanaItem> semanas = new List<SemanaItem>();

                // Agrega primero la opción "TODOS"
                semanas.Add(new SemanaItem
                {
                    Valor = 0, // o usa -1 si prefieres indicar que no es una semana válida
                    Texto = "TODOS"
                });

                // Agrega las semanas 1 a 52
                for (int i = 1; i <= 52; i++)
                {
                    semanas.Add(new SemanaItem
                    {
                        Valor = i,
                        Texto = $"Semana {i}"
                    });
                }

                cmbSemana.DataSource = semanas;
                cmbSemana.DisplayMember = "Texto";
                cmbSemana.ValueMember = "Valor";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReporteDiarioComprasFiltro_Load(object sender, EventArgs e)
        {
            LlenarComboDocumentos();
            LlenarComboProveedores();
            LlenarComboCentroCostos();
            LlenarComboSemanas();
           
            cmbPropietario1.SelectedIndex = 0;
            cmbTipo.SelectedIndex = 0;
            cmbCentroCostos.SelectedIndex = 0;

            provedor = cmbPropietario1.Text;
            cuentaBancaria = cmbTipo.Text;
        }

        private void cbFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
            if (cbFechas.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void cmbPropietario1_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedor = cmbPropietario1.Text;
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cuentaBancaria = cmbTipo.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tipo=="Diario Gastos")
            {
                ReporteDiarioGastos r = new ReporteDiarioGastos(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), fecha == "Si" ? fecha1 : "", fecha == "Si" ? fecha2 : "");
                r.ShowDialog();
            }
            else if (tipo == "Diario Reembolsos")
            {
                ReporteDiarioReembolsos reporteDiarioCompras = new ReporteDiarioReembolsos(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), cmbCentroCostos?.SelectedValue?.ToString(), dtpAnio.Text, cmbSemana?.SelectedValue?.ToString(), fecha == "Si" ? fecha1 : "", fecha == "Si" ? fecha2 : "");
                reporteDiarioCompras.ShowDialog();
            }
               
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                dtFecha1.Enabled = true;
                dtFecha2.Enabled = true;
                fecha = "Si";
                label2.Text = "Si";
                fecha1 = dtFecha1.Text;
                fecha2 = dtFecha2.Text;
            }
            else
            {
                label2.Text = "No";
                dtFecha1.Enabled = false;
                dtFecha2.Enabled = false;
                fecha = string.Empty;
                fecha1 = string.Empty;
                fecha2 = string.Empty;
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReporteDiarioComprasFiltro_FormClosing(object sender, FormClosingEventArgs e)
        {
            c.CerrarConexion();
            p.CerrarConexion();
            centroCostos.CerrarConexion();
            d.CerrarConexion();
        }
    }
    public class SemanaItem
    {
        public int Valor { get; set; }
        public string Texto { get; set; }
    }
}
