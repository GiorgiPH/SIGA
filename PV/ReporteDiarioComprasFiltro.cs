using Condominios.Clases.CentroCostos;
using Condominios.Clases.Documentos;
using PV.Clases.Clientes;
using PV.Clases.Proveedores;
using PV.Clases.ReporteCompras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PV
{
    public partial class ReporteDiarioComprasFiltro : Form
    {
        DBReporteCompras c = new DBReporteCompras();
        DBProveedores p = new DBProveedores();
        DBDocumentos d = new DBDocumentos();
        DBClientes clientes = new DBClientes();

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
            else if (tipo == "Diario Compras")
            {
                pnCentroCostos.Visible = false;

            }
            else if (tipo == "Diario Egresos")
            {
                pnCentroCostos.Visible = false;
                pnAnioSemana.Visible = false;
                pnTipoDocumento.Visible = false;
                

            }
            else if (tipo == "Saldos Proveedor")
            {
                pnCentroCostos.Visible = false;
                pnAnioSemana.Visible = false;
                pnTipoDocumento.Visible = false;


            }
            else if (tipo == "Diario Facturas")
            {
                // Usa Cliente en vez de Proveedor
                pnProveedor.Visible = false;
                panel1.Visible = true;
                pnCentroCostos.Visible = false;
                pnAnioSemana.Visible = false;
                pnTipoDocumento.Visible = true;
            }
            else if (tipo == "Diario Remisiones")
            {
                // Usa Cliente en vez de Proveedor
                pnProveedor.Visible = false;
                panel1.Visible = true;
                pnCentroCostos.Visible = false;
                pnAnioSemana.Visible = false;
                pnTipoDocumento.Visible = true;
            }
            else if (tipo == "Diario Ingresos")
            {
                // Cobros: solo Cliente y Fechas, sin tipo de documento propio
                pnProveedor.Visible = false;
                panel1.Visible = true;
                pnCentroCostos.Visible = false;
                pnAnioSemana.Visible = false;
                pnTipoDocumento.Visible = false;
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
        private void LlenarComboClientes()
        {
            try
            {
                DataTable menus = clientes.CargarClientes("");
                // Crear fila "TODOS"
                DataRow filaTodos = menus.NewRow();
                filaTodos["IdCliente"] = "0"; // Asegúrate de que la columna "Clave" exista
                filaTodos["RazonSocial"] = "TODOS"; // Asegúrate de que la columna "Clave" exista

                menus.Rows.InsertAt(filaTodos, 0); // Insertar al principio
                // Evitar eventos mientras actualizas la fuente de datos

                // Configurar estilo y autocompletado
                cmbCliente.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbCliente.DataSource = menus;
                cmbCliente.DisplayMember = "RazonSocial"; // Campo visible
                cmbCliente.ValueMember = "IdCliente";   // Campo interno
                cmbCliente.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio

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
                // Para Facturas / Remisiones se consulta el catálogo de documentos de Venta;
                // para el resto de tipos (Compras/Gastos/Reembolsos/Egresos) se mantiene "Compra".
                string filtroDocumento = (tipo == "Diario Facturas" || tipo == "Diario Remisiones") ? "Venta" : "Compra";
                string tarea = null;
                if (filtroDocumento == "Venta")
                {
                    if (tipo == "Diario Facturas")
                    {
                        tarea = "Factura";

                    }
                    else if (tipo == "Diario Remisiones")
                    {
                        tarea = "Remisiones";
                    }
                }   
                DataTable menus = d.ConsultarDocumento(filtroDocumento, filtroDocumento);
                if (!string.IsNullOrEmpty(tarea))
                {
                    menus = null;
                     menus = d.ConsultarDocumento(tarea: tarea);

                }
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
            LlenarComboCentroCostos();
            LlenarComboSemanas();

            bool usaCliente = (tipo == "Diario Facturas" || tipo == "Diario Remisiones" || tipo == "Diario Ingresos");

            if (usaCliente)
            {
                LlenarComboClientes();
                cmbCliente.SelectedIndex = 0;
                provedor = cmbCliente.Text;
            }
            else
            {
                LlenarComboProveedores();
                cmbPropietario1.SelectedIndex = 0;
                provedor = cmbPropietario1.Text;
            }

            if (pnTipoDocumento.Visible)
            {
                LlenarComboDocumentos();
                cmbTipo.SelectedIndex = 0;
                cuentaBancaria = cmbTipo.Text;
            }

            cmbCentroCostos.SelectedIndex = 0;
        }

       

        private void dtFecha1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dtFecha2_ValueChanged(object sender, EventArgs e)
        {
           
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
                ReporteDiarioGastos r = new ReporteDiarioGastos(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "", fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                r.ShowDialog();
            }
            else if (tipo == "Diario Reembolsos")
            {
                ReporteDiarioReembolsos reporteDiarioCompras = new ReporteDiarioReembolsos(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), cmbCentroCostos?.SelectedValue?.ToString(), dtpAnio.Text, cmbSemana?.SelectedValue?.ToString(), fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "", fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                reporteDiarioCompras.ShowDialog();
            }
            else if (tipo == "Diario Compras")
            {
                ReporteDiarioCompras reporteDiarioCompras = new ReporteDiarioCompras(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "", fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                reporteDiarioCompras.ShowDialog();
            }
            else if (tipo == "Diario Egresos")
            {
                ReporteEgresos reporteDiarioCompras = new ReporteEgresos(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "", fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "", "");
                reporteDiarioCompras.ShowDialog();
            }
            else if (tipo == "Saldos Proveedor")
            {
                ReporteSaldosProveedores reporteDiarioCompras = new ReporteSaldosProveedores(cmbPropietario1?.SelectedValue?.ToString(), cmbTipo?.SelectedValue?.ToString(), fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "", fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "", "");
                reporteDiarioCompras.ShowDialog();
            }
            else if (tipo == "Diario Facturas")
            {
                // sp_ReporteDiarioFacturas: @ClaveProveedor (IdCliente), @FechaInicio, @FechaFin, @ClaveDocumento
                ReporteDiarioFacturas reporteDiarioFacturas = new ReporteDiarioFacturas(
                    cmbCliente?.SelectedValue?.ToString(),
                    cmbTipo?.SelectedValue?.ToString(),
                    fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "",
                    fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                reporteDiarioFacturas.ShowDialog();
            }
            else if (tipo == "Diario Remisiones")
            {
                // sp_ReporteDiarioRemisiones: @ClaveProveedor (IdCliente), @FechaInicio, @FechaFin, @ClaveDocumento
                ReporteDiarioRemisiones reporteDiarioRemisiones = new ReporteDiarioRemisiones(
                    cmbCliente?.SelectedValue?.ToString(),
                    cmbTipo?.SelectedValue?.ToString(),
                    fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "",
                    fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                reporteDiarioRemisiones.ShowDialog();
            }
            else if (tipo == "Diario Ingresos")
            {
                // sp_ReporteDiarioIngresos: @ClaveProveedor (IdCliente), @FechaInicio, @FechaFin, @ClaveDocumento
                // No hay combo de tipo de documento propio para Cobros, se manda vacio/null.
                ReporteDiarioIngresos reporteDiarioIngresos = new ReporteDiarioIngresos(
                    cmbCliente?.SelectedValue?.ToString(),
                    null,
                    fecha == "Si" ? dtFecha1.Value.ToString("yyyy-MM-dd") : "",
                    fecha == "Si" ? dtFecha2.Value.ToString("yyyy-MM-dd") : "");
                reporteDiarioIngresos.ShowDialog();
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
            d.CerrarConexion();
        }
    }
    public class SemanaItem
    {
        public int Valor { get; set; }
        public string Texto { get; set; }
    }
}