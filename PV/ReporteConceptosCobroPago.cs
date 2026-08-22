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
    public partial class ReporteConceptosCobroPago : Form
    {
        private readonly byte? idClase;
        private readonly bool? estatus;

        // Ambos opcionales: si no se los mandas, el reporte trae TODO
        // (igual que @IdClase=NULL, @Estatus=NULL en el SP).
        public ReporteConceptosCobroPago(byte? idClase = null, bool? estatus = null)
        {
            InitializeComponent();
            this.idClase = idClase;
            this.estatus = estatus;
        }

        private void ReporteConceptosCobroPago_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlCondominiosDataSet23.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.datosEmpresaTableAdapter.Fill(this.controlCondominiosDataSet23.DatosEmpresa);

            this.sp_ConceptosCobroPagoTableAdapter.Fill(
                this.dTSConceptosCobroPago.sp_ConceptosCobroPago,
                idClase,
                estatus);

            this.reportViewer1.RefreshReport();
        }
    }
}
