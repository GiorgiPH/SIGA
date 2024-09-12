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
    public partial class ReporteKardex : Form
    {
        string Fecha = string.Empty;
        string TipoMov = string.Empty;
        string Documento = string.Empty;

        public ReporteKardex(string fecha, string tipomovimiento, string documento)
        {
            InitializeComponent();
            Fecha = fecha;
            TipoMov = tipomovimiento;
            Documento = documento;
        }

        private void ReporteKardex_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet30.MovimientoInventario' Puede moverla o quitarla según sea necesario.
            if (Fecha != string.Empty && TipoMov == string.Empty && Documento == string.Empty)
            {
                this.MovimientoInventarioTableAdapter.FillBy1(this.ControlCondominiosDataSet30.MovimientoInventario, Fecha);
            }
            else if (Fecha != string.Empty && TipoMov != string.Empty && Documento == string.Empty)
            {
                this.MovimientoInventarioTableAdapter.FillBy(this.ControlCondominiosDataSet30.MovimientoInventario, TipoMov, Fecha);
            }
            else if (Fecha != string.Empty && TipoMov != string.Empty && Documento != string.Empty)
            {
                this.MovimientoInventarioTableAdapter.Fill(this.ControlCondominiosDataSet30.MovimientoInventario, TipoMov, Documento, Fecha);
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            this.reportViewer1.RefreshReport();
        }
    }
}
