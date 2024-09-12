using System;
using System.Linq;
using System.Windows.Forms;
using PV.Clases.ReservaAreaComun;
using PuntoVentas;
using System.Drawing;

namespace MEDCON
{
    public partial class ConsultarFecha : Form
    {
        DBReservaAreaComun c = new DBReservaAreaComun();

        public static int Disponibilidad = 0;
        public static string Horario = "";
        public static string Fecha = "";
        string Condominio = string.Empty;
        string AreaComun = string.Empty;


        public ConsultarFecha()
        {
            InitializeComponent();
        }

        private void ConsultarFecha_Load(object sender, EventArgs e)
        {
            Horario = "";
            Fecha = "";
          
            c.SeleccionarCondominio(cmbCondominios);
           
        }

        void LimpiarHorario()
        {
            DBReservaAreaComun.HorarioInicial = 0;
            DBReservaAreaComun.HorarioFinal = 0;
            DBReservaAreaComun.HorarioInicial2 = 0;
            DBReservaAreaComun.HorarioFinal2 = 0;

            txtH0.Text = " ";
            txtH1.Text = " ";
            txtH2.Text = " ";
            txtH3.Text = " ";
            txtH4.Text = " ";
            txtH5.Text = " ";
            txtH6.Text = " ";
            txtH7.Text = " ";
            txtH8.Text = " ";
            txtH9.Text = " ";
            txtH10.Text = " ";
            txtH11.Text = " ";
            txtH12.Text = " ";
            txtH13.Text = " ";
            txtH14.Text = " ";
            txtH15.Text = " ";
            txtH16.Text = " ";
            txtH17.Text = " ";
            txtH18.Text = " ";
            txtH19.Text = " ";
            txtH20.Text = " ";
            txtH21.Text = " ";
            txtH22.Text = " ";
            txtH23.Text = " ";

            txtH0M.Text = " ";
            txtH1M.Text = " ";
            txtH2M.Text = " ";
            txtH3M.Text = " ";
            txtH4M.Text = " ";
            txtH5M.Text = " ";
            txtH6M.Text = " ";
            txtH7M.Text = " ";
            txtH8M.Text = " ";
            txtH9M.Text = " ";
            txtH10M.Text = " ";
            txtH11M.Text = " ";
            txtH12M.Text = " ";
            txtH13M.Text = " ";
            txtH14M.Text = " ";
            txtH15M.Text = " ";
            txtH16M.Text = " ";
            txtH17M.Text = " ";
            txtH18M.Text = " ";
            txtH19M.Text = " ";
            txtH20M.Text = " ";
            txtH21M.Text = " ";
            txtH22M.Text = " ";
            txtH23M.Text = " ";
        }

        void validarHorario()
        {
            string Dia = Convert.ToString(dpFecha.Value.DayOfWeek);


            if (Dia == "Monday")
            {
                LimpiarHorario();
                c.HorarioLunes(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";

                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Tuesday")
            {
                LimpiarHorario();
                c.HorarioMartes(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Wednesday")
            {
                LimpiarHorario();
                c.HorarioMiercoles(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Thursday")
            {
                LimpiarHorario();
                c.HorarioJueves(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Friday")
            {
                LimpiarHorario();
                c.HorarioViernes(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Saturday")
            {
                LimpiarHorario();
                c.HorarioSabado(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {

                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }
            if (Dia == "Sunday")
            {
                LimpiarHorario();
                c.HorarioDomingo(Condominio, AreaComun);

                int rowcont = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcont; i++)
                {
                    string nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string hora = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string TipoC = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string Consulta = "";

                    if (hora == "00:00")
                    {
                        txtH0.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "00:30")
                    {
                        txtH0M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "01:00")
                    {
                        txtH1.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "01:30")
                    {
                        txtH1M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "02:00")
                    {
                        txtH2.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "02:30")
                    {
                        txtH2M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "03:00")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "03:30")
                    {
                        txtH3M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "04:00")
                    {
                        txtH4.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "04:30")
                    {
                        txtH4M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "05:00")
                    {
                        txtH5.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "05:30")
                    {
                        txtH5M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "06:00")
                    {
                        txtH6.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "06:30")
                    {
                        txtH6M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "07:00")
                    {
                        txtH7.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "07:30")
                    {
                        txtH7M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "08:00")
                    {
                        txtH8.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "08:30")
                    {
                        txtH8M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "09:00")
                    {
                        txtH9.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "09:30")
                    {
                        txtH9M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "10:00")
                    {
                        txtH10.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "10:30")
                    {
                        txtH10M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "11:00")
                    {
                        txtH11.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "11:30")
                    {
                        txtH11M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "12:00")
                    {
                        txtH12.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "12:30")
                    {
                        txtH12M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "13:00")
                    {
                        txtH13.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "13:30")
                    {
                        txtH13M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "14:00")
                    {
                        txtH14.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "14:30")
                    {
                        txtH14M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "15:00")
                    {
                        txtH15.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "15:30")
                    {
                        txtH15M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "16:00")
                    {
                        txtH16.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "16:30")
                    {
                        txtH16M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "17:00")
                    {
                        txtH17.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "17:30")
                    {
                        txtH17M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "18:00")
                    {
                        txtH18.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "18:30")
                    {
                        txtH18M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "19:00")
                    {
                        txtH19.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "19:30")
                    {
                        txtH19M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "20:00")
                    {
                        txtH20.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "20:30")
                    {
                        txtH20M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "21:00")
                    {
                        txtH21.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "21:30")
                    {
                        txtH21M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "22:00")
                    {
                        txtH22.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "22:30")
                    {
                        txtH22M.Text = Consulta + " " + nombre;
                    }

                    else if (hora == "23:00")
                    {
                        txtH23.Text = Consulta + " " + nombre;
                    }
                    else if (hora == "23:30")
                    {
                        txtH23M.Text = Consulta + " " + nombre;
                    }
                }

                for (Disponibilidad = 0; Disponibilidad < DBReservaAreaComun.HorarioInicial; Disponibilidad++)
                {

                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                    }
                }


                Disponibilidad = DBReservaAreaComun.HorarioFinal;

                while (Disponibilidad <= 23)
                {
                    if (Disponibilidad < DBReservaAreaComun.HorarioInicial2 || (DBReservaAreaComun.HorarioInicial2 == 0))
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;

                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";

                        Disponibilidad = Disponibilidad + 1;
                    }
                    else
                    {
                        Disponibilidad = Disponibilidad + 1;
                    }

                }

                if (DBReservaAreaComun.HorarioFinal2 != 0)
                {
                    Disponibilidad = DBReservaAreaComun.HorarioFinal2;

                    while (Disponibilidad <= 23)
                    {
                        Button tbx = this.Controls.Find("txtH" + Disponibilidad, true).FirstOrDefault() as Button;
                        Button tbxM = this.Controls.Find("txtH" + Disponibilidad + "M", true).FirstOrDefault() as Button;
                        tbx.ForeColor = Color.Red;
                        tbxM.ForeColor = Color.Red;
                        tbx.Text = "NO DISPONIBLE";
                        tbxM.Text = "NO DISPONIBLE";
                        Disponibilidad = Disponibilidad + 1;

                    }
                }
            }

        }

        private void dpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty && cmbAreasComunes.Text!= string.Empty)
            {
                LimpiarHorario();
                c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
                validarHorario();
            }
            else
            {
                MessageBox.Show("Seleccione el condominio y el area comun a consultar");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty && cmbAreasComunes.Text != string.Empty)
            {
                LimpiarHorario();
                dpFecha.Value = dpFecha.Value.AddDays(-1);
                c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
                validarHorario();
            }
            else
            {
                MessageBox.Show("Seleccione el condominio y el area comun a consultar");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty && cmbAreasComunes.Text != string.Empty)
            {
                LimpiarHorario();
                dpFecha.Value = dpFecha.Value.AddDays(1);
                c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
                validarHorario();
            }
            else
            {
                MessageBox.Show("Seleccione el condominio y el area comun a consultar");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MenuPrincipal.Opcion = 0;
            this.Close();
        }

        private void ConsultarFecha_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmbCondominios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCondominios.Text != string.Empty)
            {
                string[] valores = c.InformacionCondominio(cmbCondominios.Text);
                txtCondominio.Text = valores[0];
                Condominio = txtCondominio.Text;

                c.SeleccionarAreaComun(cmbAreasComunes, txtCondominio.Text);
            }
        }

        private void cmbAreasComunes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAreasComunes.Text != string.Empty)
            {
                string[] valores = c.InformacionAreaComun(cmbAreasComunes.Text);
                txtAreaComun.Text = valores[0];

                Condominio = txtCondominio.Text;
                AreaComun = txtAreaComun.Text;
                c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
                validarHorario();
            }
        }
    }
}
