using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.ConsultaCondominio;

namespace PV
{
    public partial class OcupacionCondominio : Form
    {
        DBConsultaCondominio c = new DBConsultaCondominio();
        int Condominio = 0;
        string posicion = string.Empty;
        DateTime FechaEntrada;
        string Entrada = string.Empty;

        public OcupacionCondominio()
        {
            InitializeComponent();
        }

        private void OcupacionCondominio_Load(object sender, EventArgs e)
        {
            c.SeleccionarCondominio(cmbCondominio);
        }

        private void cmbCondominio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominio.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominio.Text);
                txtCondominio.Text = valores[0];
            }
            Limpiar3();
            c.CargarCondominios(dataGridView3, txtCondominio.Text);
            ValidarCondominio();
        }

        void ValidarCondominio()
        {
            int x = 0;
            int con = dataGridView3.RowCount;

            for (Condominio = 0; Condominio < 50; Condominio++)
            {
                if (x < 50)
                {
                    if (Condominio < con)
                    {
                        if (dataGridView3.Rows[Condominio].Cells[0].Value.ToString() != string.Empty)
                        {
                            Button tbx = this.Controls.Find("txtH" + x, true).FirstOrDefault() as Button;
                            tbx.Text = dataGridView3.Rows[Condominio].Cells[0].Value.ToString();

                            if (c.ColorOcupacion(tbx.Text) == 1)
                            {
                                tbx.BackColor = Color.Green;
                            }
                            else
                            {
                                tbx.BackColor = Color.Orange;
                            }
                            if (c.ColorOcupacionPropietario(tbx.Text) == 1)
                            {
                                tbx.BackColor = Color.Red;
                            }
                            x++;
                        }
                    }
                }
            }

        }


        void ValidarCondominio2()
        {
            int x = 0;
            if (posicion != string.Empty)
            {
                int posc = Convert.ToInt32(posicion);
                int con = dataGridView3.RowCount;

                for (int Condominio2 = 0; Condominio2 <= con; Condominio2++)
                {
                    if (x < 50)
                    {
                        if (posc < con)
                        {
                            if (dataGridView3.Rows[posc].Cells[0].Value.ToString() != string.Empty)
                            {
                                Button tbx = this.Controls.Find("txtH" + (x), true).FirstOrDefault() as Button;
                                tbx.Text = dataGridView3.Rows[posc].Cells[0].Value.ToString();

                                if (c.ColorOcupacion(tbx.Text) == 1)
                                {
                                    tbx.BackColor = Color.Green;
                                }
                                else
                                {
                                    tbx.BackColor = Color.Orange;
                                }
                                if (c.ColorOcupacionPropietario(tbx.Text) == 1)
                                {
                                    tbx.BackColor = Color.Red;
                                }
                                posc++;
                                x++;
                            }
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("No hay mas Casas/Departamentos");
            }

        }

        void ValidarCondominio3()
        {
            int x = 49;
            if (posicion != string.Empty && posicion != "1")
            {
                int posc = Convert.ToInt32(posicion);
                int con = dataGridView3.RowCount;

                for (int Condominio2 = 0; Condominio2 <= con; Condominio2++)
                {
                    if (posc > 1)
                    {
                        if (x >= 0)
                        {
                            if (dataGridView3.Rows[posc - 2].Cells[0].Value.ToString() != string.Empty)
                            {
                                Button tbx = this.Controls.Find("txtH" + (x), true).FirstOrDefault() as Button;
                                tbx.Text = dataGridView3.Rows[posc - 2].Cells[0].Value.ToString();
                                if (c.ColorOcupacion(tbx.Text) == 1)
                                {
                                    tbx.BackColor = Color.Green;
                                }
                                else
                                {
                                    tbx.BackColor = Color.Orange;
                                }
                                if (c.ColorOcupacionPropietario(tbx.Text) == 1)
                                {
                                    tbx.BackColor = Color.Red;
                                }
                                posc--;
                                x--;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay mas Casas/Departamentos");
            }
        }

        void Limpiar()
        {
            posicion = txtH49.Text.Split('-').Last();

            if (posicion != string.Empty)
            {
                txtH0.Text = "";
                txtH1.Text = "";
                txtH2.Text = "";
                txtH3.Text = "";
                txtH4.Text = "";
                txtH5.Text = "";
                txtH6.Text = "";
                txtH7.Text = "";
                txtH8.Text = "";
                txtH9.Text = "";
                txtH10.Text = "";
                txtH11.Text = "";
                txtH12.Text = "";
                txtH13.Text = "";
                txtH14.Text = "";
                txtH15.Text = "";
                txtH16.Text = "";
                txtH17.Text = "";
                txtH18.Text = "";
                txtH19.Text = "";
                txtH20.Text = "";
                txtH21.Text = "";
                txtH22.Text = "";
                txtH23.Text = "";
                txtH24.Text = "";
                txtH25.Text = "";
                txtH26.Text = "";
                txtH27.Text = "";
                txtH28.Text = "";
                txtH29.Text = "";
                txtH30.Text = "";
                txtH31.Text = "";
                txtH32.Text = "";
                txtH33.Text = "";
                txtH34.Text = "";
                txtH35.Text = "";
                txtH36.Text = "";
                txtH37.Text = "";
                txtH38.Text = "";
                txtH39.Text = "";
                txtH40.Text = "";
                txtH41.Text = "";
                txtH42.Text = "";
                txtH43.Text = "";
                txtH44.Text = "";
                txtH45.Text = "";
                txtH46.Text = "";
                txtH47.Text = "";
                txtH48.Text = "";
                txtH49.Text = "";

                int x = 0;
                int con = dataGridView3.RowCount;

                for (Condominio = 0; Condominio < 50; Condominio++)
                {
                    if (x < 50)
                    {
                        if (Condominio < con)
                        {
                            if (dataGridView3.Rows[Condominio].Cells[0].Value.ToString() != string.Empty)
                            {
                                Button tbx = this.Controls.Find("txtH" + x, true).FirstOrDefault() as Button;

                                tbx.BackColor = Color.Gainsboro;

                                x++;
                            }
                        }
                    }
                }
            }
        }

        void Limpiar2()
        {
            posicion = txtH0.Text.Split('-').Last();

            if (posicion != string.Empty && posicion != "1")
            {
                txtH0.Text = "";
                txtH1.Text = "";
                txtH2.Text = "";
                txtH3.Text = "";
                txtH4.Text = "";
                txtH5.Text = "";
                txtH6.Text = "";
                txtH7.Text = "";
                txtH8.Text = "";
                txtH9.Text = "";
                txtH10.Text = "";
                txtH11.Text = "";
                txtH12.Text = "";
                txtH13.Text = "";
                txtH14.Text = "";
                txtH15.Text = "";
                txtH16.Text = "";
                txtH17.Text = "";
                txtH18.Text = "";
                txtH19.Text = "";
                txtH20.Text = "";
                txtH21.Text = "";
                txtH22.Text = "";
                txtH23.Text = "";
                txtH24.Text = "";
                txtH25.Text = "";
                txtH26.Text = "";
                txtH27.Text = "";
                txtH28.Text = "";
                txtH29.Text = "";
                txtH30.Text = "";
                txtH31.Text = "";
                txtH32.Text = "";
                txtH33.Text = "";
                txtH34.Text = "";
                txtH35.Text = "";
                txtH36.Text = "";
                txtH37.Text = "";
                txtH38.Text = "";
                txtH39.Text = "";
                txtH40.Text = "";
                txtH41.Text = "";
                txtH42.Text = "";
                txtH43.Text = "";
                txtH44.Text = "";
                txtH45.Text = "";
                txtH46.Text = "";
                txtH47.Text = "";
                txtH48.Text = "";
                txtH49.Text = "";

                int x = 0;
                int con = dataGridView3.RowCount;

                for (Condominio = 0; Condominio < 50; Condominio++)
                {
                    if (x < 50)
                    {
                        if (Condominio < con)
                        {
                            if (dataGridView3.Rows[Condominio].Cells[0].Value.ToString() != string.Empty)
                            {
                                Button tbx = this.Controls.Find("txtH" + x, true).FirstOrDefault() as Button;

                                tbx.BackColor = Color.Gainsboro;

                                x++;
                            }
                        }
                    }
                }
            }
        }

        void Limpiar3()
        {
            txtH0.Text = "";
            txtH1.Text = "";
            txtH2.Text = "";
            txtH3.Text = "";
            txtH4.Text = "";
            txtH5.Text = "";
            txtH6.Text = "";
            txtH7.Text = "";
            txtH8.Text = "";
            txtH9.Text = "";
            txtH10.Text = "";
            txtH11.Text = "";
            txtH12.Text = "";
            txtH13.Text = "";
            txtH14.Text = "";
            txtH15.Text = "";
            txtH16.Text = "";
            txtH17.Text = "";
            txtH18.Text = "";
            txtH19.Text = "";
            txtH20.Text = "";
            txtH21.Text = "";
            txtH22.Text = "";
            txtH23.Text = "";
            txtH24.Text = "";
            txtH25.Text = "";
            txtH26.Text = "";
            txtH27.Text = "";
            txtH28.Text = "";
            txtH29.Text = "";
            txtH30.Text = "";
            txtH31.Text = "";
            txtH32.Text = "";
            txtH33.Text = "";
            txtH34.Text = "";
            txtH35.Text = "";
            txtH36.Text = "";
            txtH37.Text = "";
            txtH38.Text = "";
            txtH39.Text = "";
            txtH40.Text = "";
            txtH41.Text = "";
            txtH42.Text = "";
            txtH43.Text = "";
            txtH44.Text = "";
            txtH45.Text = "";
            txtH46.Text = "";
            txtH47.Text = "";
            txtH48.Text = "";
            txtH49.Text = "";

            int x = 0;
            int con = dataGridView3.RowCount;

            for (Condominio = 0; Condominio < 50; Condominio++)
            {
                if (x < 50)
                {
                    if (Condominio < con)
                    {
                        if (dataGridView3.Rows[Condominio].Cells[0].Value.ToString() != string.Empty)
                        {
                            Button tbx = this.Controls.Find("txtH" + x, true).FirstOrDefault() as Button;

                            tbx.BackColor = Color.Gainsboro;

                            x++;
                        }
                    }
                }
            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Limpiar();
            ValidarCondominio2();
        }

        private void button50_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Limpiar2();
            ValidarCondominio3();
        }

        private void txtH0_Click(object sender, EventArgs e)
        {
            if (txtH0.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH0.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje =  c.RegistroAcceso(txtH0.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH0.Text) == 1)
                    {
                        txtH0.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH0.BackColor = Color.Orange;
                        if (mensaje!= string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH0.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }

                    }
                }
            }
        }

        private void txtH1_Click(object sender, EventArgs e)
        {
            if (txtH1.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH1.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH1.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH1.Text) == 1)
                    {
                        txtH1.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH1.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH1.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH2_Click(object sender, EventArgs e)
        {
            if (txtH2.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH2.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {

                    string mensaje = c.RegistroAcceso(txtH2.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH2.Text) == 1)
                    {
                        txtH2.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH2.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH2.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH3_Click(object sender, EventArgs e)
        {
            if (txtH3.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH3.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH3.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH3.Text) == 1)
                    {
                        txtH3.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH3.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH3.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH4_Click(object sender, EventArgs e)
        {
            if (txtH4.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH4.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH4.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH4.Text) == 1)
                    {
                        txtH4.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH4.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH4.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH5_Click(object sender, EventArgs e)
        {
            if (txtH5.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH5.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH5.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH5.Text) == 1)
                    {
                        txtH5.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH5.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH5.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH6_Click(object sender, EventArgs e)
        {
            if (txtH6.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH6.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH6.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH6.Text) == 1)
                    {
                        txtH6.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH6.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH6.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH7_Click(object sender, EventArgs e)
        {
            if (txtH7.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH7.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH7.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH7.Text) == 1)
                    {
                        txtH7.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH7.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH7.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH8_Click(object sender, EventArgs e)
        {
            if (txtH8.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH8.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH8.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH8.Text) == 1)
                    {
                        txtH8.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH8.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH8.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH9_Click(object sender, EventArgs e)
        {
            if (txtH9.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH9.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH9.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH9.Text) == 1)
                    {
                        txtH9.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH9.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH9.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH10_Click(object sender, EventArgs e)
        {
            if (txtH10.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH10.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH10.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH10.Text) == 1)
                    {
                        txtH10.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH10.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH10.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH11_Click(object sender, EventArgs e)
        {
            if (txtH11.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH11.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH11.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH11.Text) == 1)
                    {
                        txtH11.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH11.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH11.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH12_Click(object sender, EventArgs e)
        {
            if (txtH12.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH12.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH12.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH12.Text) == 1)
                    {
                        txtH12.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH12.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH12.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH13_Click(object sender, EventArgs e)
        {
            if (txtH13.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH13.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH13.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH13.Text) == 1)
                    {
                        txtH13.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH13.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH13.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH14_Click(object sender, EventArgs e)
        {
            if (txtH14.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH14.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH14.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH14.Text) == 1)
                    {
                        txtH14.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH14.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH14.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH15_Click(object sender, EventArgs e)
        {
            if (txtH15.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH15.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH15.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH15.Text) == 1)
                    {
                        txtH15.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH15.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH15.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH16_Click(object sender, EventArgs e)
        {
            if (txtH16.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH16.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH16.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH16.Text) == 1)
                    {
                        txtH16.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH16.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH16.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH17_Click(object sender, EventArgs e)
        {
            if (txtH17.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH17.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH17.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH17.Text) == 1)
                    {
                        txtH17.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH17.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH17.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH18_Click(object sender, EventArgs e)
        {
            if (txtH18.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH18.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH18.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH18.Text) == 1)
                    {
                        txtH18.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH18.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH18.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH19_Click(object sender, EventArgs e)
        {
            if (txtH19.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH19.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH19.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH19.Text) == 1)
                    {
                        txtH19.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH19.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH19.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH20_Click(object sender, EventArgs e)
        {
            if (txtH20.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH20.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH20.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH20.Text) == 1)
                    {
                        txtH20.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH20.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH20.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH21_Click(object sender, EventArgs e)
        {
            if (txtH21.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH21.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH21.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH21.Text) == 1)
                    {
                        txtH21.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH21.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH21.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH22_Click(object sender, EventArgs e)
        {
            if (txtH22.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH22.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH22.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH22.Text) == 1)
                    {
                        txtH22.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH22.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH22.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH23_Click(object sender, EventArgs e)
        {
            if (txtH23.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH23.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH23.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH23.Text) == 1)
                    {
                        txtH23.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH23.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH23.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH24_Click(object sender, EventArgs e)
        {
            if (txtH24.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH24.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH24.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH24.Text) == 1)
                    {
                        txtH24.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH24.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH24.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH25_Click(object sender, EventArgs e)
        {
            if (txtH25.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH25.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH25.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH25.Text) == 1)
                    {
                        txtH25.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH25.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH25.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH26_Click(object sender, EventArgs e)
        {
            if (txtH26.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH26.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH26.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH26.Text) == 1)
                    {
                        txtH26.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH26.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH26.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH27_Click(object sender, EventArgs e)
        {
            if (txtH27.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH27.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH27.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH27.Text) == 1)
                    {
                        txtH27.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH27.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH27.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH28_Click(object sender, EventArgs e)
        {
            if (txtH28.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH28.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH28.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH28.Text) == 1)
                    {
                        txtH28.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH28.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH28.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH29_Click(object sender, EventArgs e)
        {
            if (txtH29.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH29.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH29.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH29.Text) == 1)
                    {
                        txtH29.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH29.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH29.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH30_Click(object sender, EventArgs e)
        {
            if (txtH30.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH30.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH30.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH30.Text) == 1)
                    {
                        txtH30.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH30.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH30.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH31_Click(object sender, EventArgs e)
        {
            if (txtH31.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH31.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH31.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH31.Text) == 1)
                    {
                        txtH31.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH31.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH31.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH32_Click(object sender, EventArgs e)
        {
            if (txtH32.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH32.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH32.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH32.Text) == 1)
                    {
                        txtH32.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH32.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH32.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH33_Click(object sender, EventArgs e)
        {
            if (txtH33.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH33.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH33.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH33.Text) == 1)
                    {
                        txtH33.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH33.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH33.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH34_Click(object sender, EventArgs e)
        {
            if (txtH34.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH34.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH34.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH34.Text) == 1)
                    {
                        txtH34.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH34.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH34.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH35_Click(object sender, EventArgs e)
        {
            if (txtH35.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH35.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH35.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH35.Text) == 1)
                    {
                        txtH35.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH35.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH35.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH36_Click(object sender, EventArgs e)
        {
            if (txtH36.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH36.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH36.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH36.Text) == 1)
                    {
                        txtH36.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH36.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH36.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH37_Click(object sender, EventArgs e)
        {
            if (txtH37.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH37.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH37.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH37.Text) == 1)
                    {
                        txtH37.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH37.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH37.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH38_Click(object sender, EventArgs e)
        {
            if (txtH38.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH38.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH38.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH38.Text) == 1)
                    {
                        txtH38.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH38.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH38.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH39_Click(object sender, EventArgs e)
        {
            if (txtH39.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH39.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH39.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH39.Text) == 1)
                    {
                        txtH39.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH39.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH39.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH40_Click(object sender, EventArgs e)
        {
            if (txtH40.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH40.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH40.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH40.Text) == 1)
                    {
                        txtH40.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH40.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH40.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH41_Click(object sender, EventArgs e)
        {
            if (txtH41.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH41.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {

                    string mensaje = c.RegistroAcceso(txtH41.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH41.Text) == 1)
                    {
                        txtH41.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH41.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH41.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH42_Click(object sender, EventArgs e)
        {
            if (txtH42.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH42.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH42.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH42.Text) == 1)
                    {
                        txtH42.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH42.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH42.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH43_Click(object sender, EventArgs e)
        {
            if (txtH43.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH43.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH43.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH43.Text) == 1)
                    {
                        txtH43.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH43.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH43.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH44_Click(object sender, EventArgs e)
        {
            if (txtH44.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH44.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH44.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH44.Text) == 1)
                    {
                        txtH44.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH44.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH44.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH45_Click(object sender, EventArgs e)
        {
            if (txtH45.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH45.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH45.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH45.Text) == 1)
                    {
                        txtH45.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH45.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH45.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH46_Click(object sender, EventArgs e)
        {
            if (txtH46.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH46.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH46.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH46.Text) == 1)
                    {
                        txtH46.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH46.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH46.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH47_Click(object sender, EventArgs e)
        {
            if (txtH47.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH47.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH47.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH47.Text) == 1)
                    {
                        txtH47.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH47.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH47.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH48_Click(object sender, EventArgs e)
        {
            if (txtH48.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH48.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH48.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH48.Text) == 1)
                    {
                        txtH48.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH48.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH48.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }

        private void txtH49_Click(object sender, EventArgs e)
        {
            if (txtH49.Text == string.Empty)
            {
                MessageBox.Show("No hay registro");
            }
            else
            {
                string Hoy = DateTime.Today.ToString();
                FechaEntrada = Convert.ToDateTime(Hoy);

                string HoraEntrada = DateTime.Now.ToString("HH");
                string MinutoEntrada = DateTime.Now.ToString("mm");
                string SegundoEntrada = DateTime.Now.ToString("ss tt");
                Entrada = HoraEntrada + ":" + MinutoEntrada + ":" + SegundoEntrada;

                if (txtH49.BackColor == Color.Red)
                {
                    MessageBox.Show("Esta propiedad no tiene asignado un propietario.");
                }
                else
                {
                    string mensaje = c.RegistroAcceso(txtH49.Text, FechaEntrada, Entrada, FechaEntrada, Entrada);

                    if (c.ColorOcupacion(txtH49.Text) == 1)
                    {
                        txtH49.BackColor = Color.Green;
                    }
                    else
                    {
                        txtH49.BackColor = Color.Orange;
                        if (mensaje != string.Empty)
                        {
                            OcupacionCondominioRegistro ocupacionCondominio = new OcupacionCondominioRegistro(txtH49.Text, FechaEntrada, Entrada);
                            ocupacionCondominio.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
