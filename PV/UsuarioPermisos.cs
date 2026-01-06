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
    public partial class UsuarioPermisos : Form
    {
        public UsuarioPermisos()
        {
            InitializeComponent();
        }

        private void PARAMETROS_Click(object sender, EventArgs e)
        {

        }

        private void CATALOGOS_Click(object sender, EventArgs e)
        {

        }

        private void COMPRAS_Click(object sender, EventArgs e)
        {

        }

        private void ParamSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (ParamSwitch.Checked == true)
            {
                ParamDatosEmpresa.Checked = true;
                ParamUsuarios.Checked = true;
           
                if (ParamDatosEmpresa.Checked == true)
                {
                    lblParam2.Text = "Activo";
                }
                else
                {
                    lblParam2.Text = "Inactivo";
                }
                if (ParamUsuarios.Checked == true)
                {
                    lblParam1.Text = "Activo";
                }
                else
                {
                    lblParam1.Text = "Inactivo";
                }
            }
            else
            {
                ParamDatosEmpresa.Checked = false;
                ParamUsuarios.Checked = false;
            }      

        }

        private void ParamSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (ParamDatosEmpresa.Checked == true)
            {
                lblParam1.Text = "Activo";
            }
            else
            {
                lblParam1.Text = "Inactivo";
            }

        }

        private void ParamSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (ParamUsuarios.Checked == true)
            {
                lblParam2.Text = "Activo";
            }
            else
            {
                lblParam2.Text = "Inactivo";
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void CatSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (CatSwitch1.Checked == true)
            {
                CatDivisas.Checked = true;
                CatAlmacenes.Checked = true;
                CatCategoriasyFamilias.Checked = true;
                CatProductos.Checked = true;
                CatServicios.Checked = true;
                CatCentroCostos.Checked = true;
                CatDocumentos.Checked = true;
                CatConceptosGlobales.Checked = true;
                CatFormasPago.Checked = true;
                CatEmpleados.Checked = true;
                CatTipoZonas.Checked = true;
                CatClientes.Checked = true;
                CatProveedores.Checked = true;
                CatCuentasBancarias.Checked = true;



                if (CatDivisas.Checked == true)
                {
                    lblcat1.Text = "Activo";
                }
                else
                {
                    lblcat1.Text = "Inactivo";
                }
                if (CatAlmacenes.Checked == true)
                {
                    lblcat2.Text = "Activo";
                }
                else
                {
                    lblcat2.Text = "Inactivo";
                }
                if (CatCategoriasyFamilias.Checked == true)
                {
                    lblcat3.Text = "Activo";
                }
                else
                {
                    lblcat3.Text = "Inactivo";
                }
                if (CatProductos.Checked == true)
                {
                    lblcat4.Text = "Activo";
                }
                else
                {
                    lblcat4.Text = "Inactivo";
                }
                if (CatServicios.Checked == true)
                {
                    lblcat5.Text = "Activo";
                }
                else
                {
                    lblcat5.Text = "Inactivo";
                }
                if (CatCentroCostos.Checked == true)
                {
                    lblcat6.Text = "Activo";
                }
                else
                {
                    lblcat6.Text = "Inactivo";
                }
                if (CatDocumentos.Checked == true)
                {
                    lblcat7.Text = "Activo";
                }
                else
                {
                    lblcat7.Text = "Inactivo";
                }
                if (CatConceptosGlobales.Checked == true)
                {
                    lblcat8.Text = "Activo";
                }
                else
                {
                    lblcat8.Text = "Inactivo";
                }
                if (CatFormasPago.Checked == true)
                {
                    lblcat9.Text = "Activo";
                }
                else
                {
                    lblcat9.Text = "Inactivo";
                }
                if (CatEmpleados.Checked == true)
                {
                    lblcat10.Text = "Activo";
                }
                else
                {
                    lblcat10.Text = "Inactivo";
                }
                if (CatTipoZonas.Checked == true)
                {
                    lblcat11.Text = "Activo";
                }
                else
                {
                    lblcat11.Text = "Inactivo";
                }
                if (CatClientes.Checked == true)
                {
                    lblcat12.Text = "Activo";
                }
                else
                {
                    lblcat12.Text = "Inactivo";
                }
                if (CatProveedores.Checked == true)
                {
                    lblcat13.Text = "Activo";
                }
                else
                {
                    lblcat13.Text = "Inactivo";
                }
                if (CatCuentasBancarias.Checked == true)
                {
                    lblcat14.Text = "Activo";
                }
                else
                {
                    lblcat14.Text = "Inactivo";
                }


            }
            else
            {              
                    lblcat1.Text = "Inactivo";
                   lblcat2.Text = "Inactivo";
                    lblcat3.Text = "Inactivo";
                    lblcat4.Text = "Inactivo";
                    lblcat5.Text = "Inactivo";
                    lblcat6.Text = "Inactivo";
                    lblcat7.Text = "Inactivo";
                    lblcat8.Text = "Inactivo";
                    lblcat9.Text = "Inactivo";
                    lblcat11.Text = "Inactivo";
                    lblcat12.Text = "Inactivo";
                    lblcat13.Text = "Inactivo";
                    lblcat14.Text = "Inactivo";

                CatDivisas.Checked = false;
                CatAlmacenes.Checked = false;
                CatCategoriasyFamilias.Checked = false;
                CatProductos.Checked = false;
                CatServicios.Checked = false;
                CatCentroCostos.Checked = false;
                CatDocumentos.Checked = false;
                CatConceptosGlobales.Checked = false;
                CatFormasPago.Checked = false;
                CatEmpleados.Checked = false;
                CatTipoZonas.Checked = false;
                CatClientes.Checked = false;
                CatProveedores.Checked = false;
                CatCuentasBancarias.Checked = false;
            }
        }

        private void CatSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (CatDivisas.Checked == true)
            {
                lblcat1.Text = "Activo";
            }
            else
            {
                lblcat1.Text = "Inactivo";
            }
        }

        private void CatSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (CatAlmacenes.Checked == true)
            {
                lblcat2.Text = "Activo";
            }
            else
            {
                lblcat2.Text = "Inactivo";
            }
        }

        private void CatSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (CatCategoriasyFamilias.Checked == true)
            {
                lblcat3.Text = "Activo";
            }
            else
            {
                lblcat3.Text = "Inactivo";
            }
        }

        private void CatSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (CatProductos.Checked == true)
            {
                lblcat4.Text = "Activo";
            }
            else
            {
                lblcat4.Text = "Inactivo";
            }
        }

        private void CatSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (CatServicios.Checked == true)
            {
                lblcat5.Text = "Activo";
            }
            else
            {
                lblcat5.Text = "Inactivo";
            }
        }

        private void CatSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (CatCentroCostos.Checked == true)
            {
                lblcat6.Text = "Activo";
            }
            else
            {
                lblcat6.Text = "Inactivo";
            }
        }

        private void CatSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            if (CatDocumentos.Checked == true)
            {
                lblcat7.Text = "Activo";
            }
            else
            {
                lblcat7.Text = "Inactivo";
            }
        }

        private void CatSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if (CatConceptosGlobales.Checked == true)
            {
                lblcat8.Text = "Activo";
            }
            else
            {
                lblcat8.Text = "Inactivo";
            }
        }

        private void CatSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            if (CatFormasPago.Checked == true)
            {
                lblcat9.Text = "Activo";
            }
            else
            {
                lblcat9.Text = "Inactivo";
            }
        }

        private void CatSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            if (CatEmpleados.Checked == true)
            {
                lblcat10.Text = "Activo";
            }
            else
            {
                lblcat10.Text = "Inactivo";
            }
        }

        private void CatSwitch12_CheckedChanged(object sender, EventArgs e)
        {
            if (CatTipoZonas.Checked == true)
            {
                lblcat11.Text = "Activo";
            }
            else
            {
                lblcat11.Text = "Inactivo";
            }
        }

        private void CatSwitch13_CheckedChanged(object sender, EventArgs e)
        {
            if (CatClientes.Checked == true)
            {
                lblcat12.Text = "Activo";
            }
            else
            {
                lblcat12.Text = "Inactivo";
            }
        }

        private void CatSwitch14_CheckedChanged(object sender, EventArgs e)
        {
            if (CatProveedores.Checked == true)
            {
                lblcat13.Text = "Activo";
            }
            else
            {
                lblcat13.Text = "Inactivo";
            }
        }

        private void CatSwitch15_CheckedChanged(object sender, EventArgs e)
        {
            if (CatCuentasBancarias.Checked == true)
            {
                lblcat14.Text = "Activo";
            }
            else
            {
                lblcat14.Text = "Inactivo";
            }
        }

        private void InvSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (InvSwitch1.Checked == true)
            {

                InvSwitch2.Checked = true;
                InvTiposMovimientos.Checked = true;
                InvRegistrarEntradas.Checked = true;
                InvRegistrarSalidas.Checked = true;
                InvRegistrarTraspasos.Checked = true;
                InvConsultarInventarios.Checked = true;
                InvReportes.Checked = true;

                lblInv1.Text = "Activo";
                lblInv2.Text = "Activo";
                lblInv3.Text = "Activo";
                lblInv4.Text = "Activo";
                lblInv5.Text = "Activo";
                lblInv6.Text = "Activo";
                lblInv7.Text = "Activo";
            }
            else
            {
                InvSwitch2.Checked = false;
                InvTiposMovimientos.Checked = false;
                InvRegistrarEntradas.Checked = false;
                InvRegistrarSalidas.Checked = false;
                InvRegistrarTraspasos.Checked = false;
                InvConsultarInventarios.Checked = false;
                InvReportes.Checked = false;

                lblInv1.Text = "Inactivo";
                lblInv2.Text = "Inactivo";
                lblInv3.Text = "Inactivo";
                lblInv4.Text = "Inactivo";
                lblInv5.Text = "Inactivo";
                lblInv6.Text = "Inactivo";
                lblInv7.Text = "Inactivo";     
            }


            }

        private void CompSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (CompSwitch1.Checked == true)
            {
                CompRequisiciones.Checked = true;
                CompCotizaciones.Checked = true;
                CompPedidosProveedores.Checked = true;
                CompCompras.Checked = true;
                CompNotasCRyCA.Checked = true;
                CompReportes.Checked = true;
                CompDefinePoliza.Checked = true;
                CompGeneraPoliza.Checked = true;
                lblComp1.Text = "Activo";
                lblComp2.Text = "Activo";
                lblComp3.Text = "Activo";
                lblComp4.Text = "Activo";
                lblComp5.Text = "Activo";
                lblComp6.Text = "Activo";
                lblComp7.Text = "Activo";
            }
            else
            {
                CompRequisiciones.Checked = false;
                CompCotizaciones.Checked = false;
                CompPedidosProveedores.Checked = false;
                CompCompras.Checked = false;
                CompNotasCRyCA.Checked = false;
                CompReportes.Checked = false;
                CompDefinePoliza.Checked = false;
                CompGeneraPoliza.Checked = false;
                lblComp1.Text = "Inactivo";
                lblComp2.Text = "Inactivo";
                lblComp3.Text = "Inactivo";
                lblComp4.Text = "Inactivo";
                lblComp5.Text = "Inactivo";
                lblComp6.Text = "Inactivo";
                lblComp7.Text = "Inactivo";
            }
        }

        private void InvSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (InvSwitch2.Checked == true)
            {

                //InvSwitch2.Checked = true;
                InvTiposMovimientos.Checked = true;
                InvRegistrarEntradas.Checked = true;
                InvRegistrarSalidas.Checked = true;
                InvRegistrarTraspasos.Checked = true;
                InvConsultarInventarios.Checked = true;
                InvReportes.Checked = true;

                lblInv1.Text = "Activo";
                lblInv2.Text = "Activo";
                lblInv3.Text = "Activo";
                lblInv4.Text = "Activo";
                lblInv5.Text = "Activo";
                lblInv6.Text = "Activo";
                lblInv7.Text = "Activo";
            }
            else
            {
                //InvSwitch2.Checked = false;
                InvTiposMovimientos.Checked = false;
                InvRegistrarEntradas.Checked = false;
                InvRegistrarSalidas.Checked = false;
                InvRegistrarTraspasos.Checked = false;
                InvConsultarInventarios.Checked = false;
                InvReportes.Checked = false;

                lblInv1.Text = "Inactivo";
                lblInv2.Text = "Inactivo";
                lblInv3.Text = "Inactivo";
                lblInv4.Text = "Inactivo";
                lblInv5.Text = "Inactivo";
                lblInv6.Text = "Inactivo";
                lblInv7.Text = "Inactivo";
            }
        }

        private void InvSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (InvTiposMovimientos.Checked == true)
            {
                lblInv2.Text = "Activo";
            }
            else
            {
                lblInv2.Text = "Inactivo";
            }
        }

        private void InvSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (InvRegistrarEntradas.Checked == true)
            {
                lblInv3.Text = "Activo";
            }
            else
            {
                lblInv3.Text = "Inactivo";
            }
        }

        private void InvSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (InvRegistrarSalidas.Checked == true)
            {
                lblInv4.Text = "Activo";
            }
            else
            {
                lblInv4.Text = "Inactivo";
            }
        }

        private void InvSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (InvRegistrarTraspasos.Checked == true)
            {
                lblInv5.Text = "Activo";
            }
            else
            {
                lblInv5.Text = "Inactivo";
            }

        }

        private void InvSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (InvConsultarInventarios.Checked == true)
            {
                lblInv6.Text = "Activo";
            }
            else
            {
                lblInv6.Text = "Inactivo";
            }
        }

        private void InvSwitch8_CheckedChanged(object sender, EventArgs e)
        {

            if (InvReportes.Checked == true)
            {
                lblInv7.Text = "Activo";
            }
            else
            {
                lblInv7.Text = "Inactivo";
            }
        }

        private void CompSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (CompRequisiciones.Checked == true)
            {
                lblComp1.Text = "Activo";
            }
            else
            {
                lblComp1.Text = "Inactivo";
            }
        }

        private void CompSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (CompCotizaciones.Checked == true)
            {
                lblComp2.Text = "Activo";
            }
            else
            {
                lblComp2.Text = "Inactivo";
            }
        }

        private void CompSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (CompPedidosProveedores.Checked == true)
            {
                lblComp3.Text = "Activo";
            }
            else
            {
                lblComp3.Text = "Inactivo";
            }
        }

        private void CompSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (CompCompras.Checked == true)
            {
                lblComp4.Text = "Activo";
            }
            else
            {
                lblComp4.Text = "Inactivo";
            }
        }

        private void CompSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (CompNotasCRyCA.Checked == true)
            {
                lblComp5.Text = "Activo";
            }
            else
            {
                lblComp5.Text = "Inactivo";
            }
        }

        private void CompSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (CompReportes.Checked == true)
            {
                lblComp6.Text = "Activo";
            }
            else
            {
                lblComp6.Text = "Inactivo";
            }
        }

        private void CompSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            if (CompDefinePoliza.Checked == true)
            {
                lblComp7.Text = "Activo";
            }
            else
            {
                lblComp7.Text = "Inactivo";
            }
        }

        private void CompSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if (CompGeneraPoliza.Checked == true)
            {
                lblComp8.Text = "Activo";
            }
            else
            {
                lblComp8.Text = "Inactivo";
            }
        }

        private void VENTAS_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch35_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

