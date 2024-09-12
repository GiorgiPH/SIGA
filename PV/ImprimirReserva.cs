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
    public partial class ImprimirReserva : Form
    {
        string condominio = string.Empty;
        string area = string.Empty;
        string propietario = string.Empty;
        string hora = string.Empty;
        string fecha = string.Empty;

        public ImprimirReserva(string Condominio, string Area, string Propietario, string Hora, string Fecha)
        {
            InitializeComponent();
             condominio = Condominio;
             area = Area;
             propietario = Propietario;
             hora = Hora;
             fecha = Fecha;
        }

        private void ImprimirReserva_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet43.ReservarArea' Puede moverla o quitarla según sea necesario.
            this.ReservarAreaTableAdapter.Fill(this.ControlCondominiosDataSet43.ReservarArea, Convert.ToInt32(condominio), area, Convert.ToInt32(propietario), fecha, hora);

            this.reportViewer1.RefreshReport();
        }
    }
}
