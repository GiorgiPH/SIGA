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
    public partial class ReporteEntradaSalidaCond : Form
    {
        string Provedor = string.Empty;
        string Condominio = string.Empty;
        string Fecha = string.Empty;
        string Fecha1 = string.Empty;
        string Fecha2 = string.Empty;

        public ReporteEntradaSalidaCond(string provedor, string condominio, string fecha, string fecha1, string fecha2)
        {
            InitializeComponent();
            Provedor = provedor;
            Condominio = condominio;
            Fecha = fecha;
            Fecha1 = fecha1;
            Fecha2 = fecha2;
        }

        private void ReporteEntradaSalidaCond_Load(object sender, EventArgs e)
        {
           
            if (Provedor=="TODOS" && Condominio == "TODOS" && Fecha!="Si")
            {
                this.RegistroOcupacionTableAdapter.Fill(this.ControlCondominiosDataSet48.RegistroOcupacion);
            }
            else if (Provedor != "TODOS" && Condominio != "TODOS" && Fecha != "Si")
            {
                
                this.RegistroOcupacionTableAdapter.FillBy(this.ControlCondominiosDataSet48.RegistroOcupacion, Provedor, Condominio);
            }
            else if (Provedor != "TODOS" && Condominio != "TODOS" && Fecha == "Si")
            {
                this.RegistroOcupacionTableAdapter.FillBy1(this.ControlCondominiosDataSet48.RegistroOcupacion, Provedor, Condominio, Fecha1, Fecha2);
            }
            else if (Provedor == "TODOS" && Condominio == "TODOS" && Fecha == "Si")
            {
                this.RegistroOcupacionTableAdapter.FillBy2(this.ControlCondominiosDataSet48.RegistroOcupacion, Fecha1, Fecha2);
            }
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet48.RegistroOcupacion' Puede moverla o quitarla según sea necesario.
           

            this.reportViewer1.RefreshReport();
        }
    }
}
