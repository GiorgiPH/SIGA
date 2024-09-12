using Guna.UI2.WinForms;
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
    public partial class MenuDemo : Form
    {
        public MenuDemo()
        {
            InitializeComponent();
        }

        private void MenuDemo_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Show(Panel1);
        }
        public void ocultarTodo()
        {
            Panel1.Visible = false;

            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel4_6.Visible = false;

            Panel5.Visible = false;

            Panel6.Visible = false;

        }
        public void hide()
        {
            if (Panel1.Visible == true)
            {
                Panel1.Visible = false;
            }
            if (Panel2.Visible == true)
            {
                Panel2.Visible = false;
            }
            if (Panel3.Visible == true)
            {
                Panel3.Visible = false;
                Panel3_4.Visible = false;
                Panel3_4_5.Visible = false;
            }
            if (Panel4.Visible == true)
            {
                Panel4.Visible = false;
                Panel4_6.Visible = false;
            }
            if (Panel5.Visible == true)
            {
                Panel5.Visible = false;
            }
            if (Panel6.Visible == true)
            {
                Panel6.Visible = false;
            }
            if (Panel7.Visible == true)
            {
                Panel7.Visible = false;
            }
        }
        private void Show(Guna2Panel P)
        {
            if (P.Visible == false)
            {
                hide();
                P.Visible = true;
            }
            else
            {
                P.Visible = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Show(Panel2);

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Show(Panel3);

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Show(Panel4);

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Show(Panel5);

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Show(Panel6);

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Show(Panel7);

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
