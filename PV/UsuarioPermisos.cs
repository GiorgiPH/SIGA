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
                ParamSwitch1.Checked = true;
                ParamSwitch2.Checked = true;
           
                if (ParamSwitch1.Checked == true)
                {
                    lblParam2.Text = "Activo";
                }
                else
                {
                    lblParam2.Text = "Inactivo";
                }
                if (ParamSwitch2.Checked == true)
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
                ParamSwitch1.Checked = false;
                ParamSwitch2.Checked = false;
            }      

        }

        private void ParamSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (ParamSwitch1.Checked == true)
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
            if (ParamSwitch2.Checked == true)
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
                CatSwitch2.Checked = true;
                CatSwitch3.Checked = true;
                CatSwitch4.Checked = true;
                CatSwitch5.Checked = true;
                CatSwitch6.Checked = true;
                CatSwitch7.Checked = true;
                CatSwitch8.Checked = true;
                CatSwitch9.Checked = true;
                CatSwitch10.Checked = true;
                CatSwitch11.Checked = true;
                CatSwitch12.Checked = true;
                CatSwitch13.Checked = true;
                CatSwitch14.Checked = true;
                CatSwitch15.Checked = true;



                if (CatSwitch2.Checked == true)
                {
                    lblcat1.Text = "Activo";
                }
                else
                {
                    lblcat1.Text = "Inactivo";
                }
                if (CatSwitch3.Checked == true)
                {
                    lblcat2.Text = "Activo";
                }
                else
                {
                    lblcat2.Text = "Inactivo";
                }
                if (CatSwitch4.Checked == true)
                {
                    lblcat3.Text = "Activo";
                }
                else
                {
                    lblcat3.Text = "Inactivo";
                }
                if (CatSwitch5.Checked == true)
                {
                    lblcat4.Text = "Activo";
                }
                else
                {
                    lblcat4.Text = "Inactivo";
                }
                if (CatSwitch6.Checked == true)
                {
                    lblcat5.Text = "Activo";
                }
                else
                {
                    lblcat5.Text = "Inactivo";
                }
                if (CatSwitch7.Checked == true)
                {
                    lblcat6.Text = "Activo";
                }
                else
                {
                    lblcat6.Text = "Inactivo";
                }
                if (CatSwitch8.Checked == true)
                {
                    lblcat7.Text = "Activo";
                }
                else
                {
                    lblcat7.Text = "Inactivo";
                }
                if (CatSwitch9.Checked == true)
                {
                    lblcat8.Text = "Activo";
                }
                else
                {
                    lblcat8.Text = "Inactivo";
                }
                if (CatSwitch10.Checked == true)
                {
                    lblcat9.Text = "Activo";
                }
                else
                {
                    lblcat9.Text = "Inactivo";
                }
                if (CatSwitch11.Checked == true)
                {
                    lblcat10.Text = "Activo";
                }
                else
                {
                    lblcat10.Text = "Inactivo";
                }
                if (CatSwitch12.Checked == true)
                {
                    lblcat11.Text = "Activo";
                }
                else
                {
                    lblcat11.Text = "Inactivo";
                }
                if (CatSwitch13.Checked == true)
                {
                    lblcat12.Text = "Activo";
                }
                else
                {
                    lblcat12.Text = "Inactivo";
                }
                if (CatSwitch14.Checked == true)
                {
                    lblcat13.Text = "Activo";
                }
                else
                {
                    lblcat13.Text = "Inactivo";
                }
                if (CatSwitch15.Checked == true)
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

                CatSwitch2.Checked = false;
                CatSwitch3.Checked = false;
                CatSwitch4.Checked = false;
                CatSwitch5.Checked = false;
                CatSwitch6.Checked = false;
                CatSwitch7.Checked = false;
                CatSwitch8.Checked = false;
                CatSwitch9.Checked = false;
                CatSwitch10.Checked = false;
                CatSwitch11.Checked = false;
                CatSwitch12.Checked = false;
                CatSwitch13.Checked = false;
                CatSwitch14.Checked = false;
                CatSwitch15.Checked = false;
            }
        }

        private void CatSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (CatSwitch2.Checked == true)
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
            if (CatSwitch3.Checked == true)
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
            if (CatSwitch4.Checked == true)
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
            if (CatSwitch5.Checked == true)
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
            if (CatSwitch6.Checked == true)
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
            if (CatSwitch7.Checked == true)
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
            if (CatSwitch8.Checked == true)
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
            if (CatSwitch9.Checked == true)
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
            if (CatSwitch10.Checked == true)
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
            if (CatSwitch11.Checked == true)
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
            if (CatSwitch12.Checked == true)
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
            if (CatSwitch13.Checked == true)
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
            if (CatSwitch14.Checked == true)
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
            if (CatSwitch15.Checked == true)
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
                InvSwitch3.Checked = true;
                InvSwitch4.Checked = true;
                InvSwitch5.Checked = true;
                InvSwitch6.Checked = true;
                InvSwitch7.Checked = true;
                InvSwitch8.Checked = true;

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
                InvSwitch3.Checked = false;
                InvSwitch4.Checked = false;
                InvSwitch5.Checked = false;
                InvSwitch6.Checked = false;
                InvSwitch7.Checked = false;
                InvSwitch8.Checked = false;

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
                CompSwitch2.Checked = true;
                CompSwitch3.Checked = true;
                CompSwitch4.Checked = true;
                CompSwitch5.Checked = true;
                CompSwitch6.Checked = true;
                CompSwitch7.Checked = true;
                CompSwitch8.Checked = true;
                CompSwitch9.Checked = true;
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
                CompSwitch2.Checked = false;
                CompSwitch3.Checked = false;
                CompSwitch4.Checked = false;
                CompSwitch5.Checked = false;
                CompSwitch6.Checked = false;
                CompSwitch7.Checked = false;
                CompSwitch8.Checked = false;
                CompSwitch9.Checked = false;
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
                InvSwitch3.Checked = true;
                InvSwitch4.Checked = true;
                InvSwitch5.Checked = true;
                InvSwitch6.Checked = true;
                InvSwitch7.Checked = true;
                InvSwitch8.Checked = true;

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
                InvSwitch3.Checked = false;
                InvSwitch4.Checked = false;
                InvSwitch5.Checked = false;
                InvSwitch6.Checked = false;
                InvSwitch7.Checked = false;
                InvSwitch8.Checked = false;

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
            if (InvSwitch3.Checked == true)
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
            if (InvSwitch4.Checked == true)
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
            if (InvSwitch5.Checked == true)
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
            if (InvSwitch6.Checked == true)
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
            if (InvSwitch7.Checked == true)
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

            if (InvSwitch8.Checked == true)
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
            if (CompSwitch2.Checked == true)
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
            if (CompSwitch3.Checked == true)
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
            if (CompSwitch4.Checked == true)
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
            if (CompSwitch5.Checked == true)
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
            if (CompSwitch6.Checked == true)
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
            if (CompSwitch7.Checked == true)
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
            if (CompSwitch8.Checked == true)
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
            if (CompSwitch9.Checked == true)
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
    }
}

