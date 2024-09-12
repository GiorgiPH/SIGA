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
    public partial class ReporteAnticiposProveedoresAplicado : Form
    {
        string Propietario1 = string.Empty;
        string Propietario2 = string.Empty;
        string Fecha = string.Empty;
        string Fecha1 = string.Empty;
        string Fecha2 = string.Empty;
        int pro1 = 0;
        string Propi1 = string.Empty;
        int pro2 = 0;
        string Propi2 = string.Empty;


        public ReporteAnticiposProveedoresAplicado(string propietario1, string propietario2, string fecha, string fecha1, string fecha2)
        {
            InitializeComponent();
            Propietario1 = propietario1;
            Propietario2 = propietario2;
            Fecha = fecha;
            Fecha1 = fecha1;
            Fecha2 = fecha2;
        }

        private void ReporteAnticiposProveedoresAplicado_Load(object sender, EventArgs e)
        {
            if (Propietario1 != "TODOS")
            {
                pro1 = Propietario1.IndexOf(" -");
                Propi1 = Propietario1.Substring(0, pro1);
                pro2 = Propietario2.IndexOf(" -");
                Propi2 = Propietario2.Substring(0, pro2);
            }

            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);

            if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha == string.Empty)
            {
                this.AnticipoProveedor_GeneralTableAdapter.Fill(this.ControlCondominiosDataSet55.AnticipoProveedor_General);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha == string.Empty)
            {
                this.AnticipoProveedor_GeneralTableAdapter.FillBy(this.ControlCondominiosDataSet55.AnticipoProveedor_General, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2));
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 != string.Empty && Propi2 != string.Empty && Fecha != string.Empty)
            {
                this.AnticipoProveedor_GeneralTableAdapter.FillBy1(this.ControlCondominiosDataSet55.AnticipoProveedor_General, Convert.ToInt32(Propi1), Convert.ToInt32(Propi2), Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
            else if (Propi1 == string.Empty && Propi2 == string.Empty && Fecha != string.Empty)
            {
                this.AnticipoProveedor_GeneralTableAdapter.FillBy2(this.ControlCondominiosDataSet55.AnticipoProveedor_General, Fecha1, Fecha2);
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
