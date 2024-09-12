using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using PV.Clases.ReservaAreaComun;

namespace MEDCON
{
    public partial class DisponibilidadHorario : Form
    {
        DBReservaAreaComun c = new DBReservaAreaComun();

        public static int Disponibilidad = 0;
        public static string Horario = "";
        public static string Fecha = "";
        string Condominio = string.Empty;
        string AreaComun = string.Empty;

        public DisponibilidadHorario(string condominio, string areacomun)
        {
            InitializeComponent();
            Condominio = condominio;
            AreaComun = areacomun;
        }

        private void DisponibilidadHorario_Load(object sender, EventArgs e)
        {
             Horario = "";
             Fecha = "";
            c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
            validarHorario();
        }

        void LimpiarHorario()
        {
            DBReservaAreaComun.HorarioInicial = 0;
            DBReservaAreaComun.HorarioFinal = 0;
            DBReservaAreaComun.HorarioInicial2 = 0;
            DBReservaAreaComun.HorarioFinal2 = 0;

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

            txtH0M.Text = "";
            txtH1M.Text = "";
            txtH2M.Text = "";
            txtH3M.Text = "";
            txtH4M.Text = "";
            txtH5M.Text = "";
            txtH6M.Text = "";
            txtH7M.Text = "";
            txtH8M.Text = "";
            txtH9M.Text = "";
            txtH10M.Text = "";
            txtH11M.Text = "";
            txtH12M.Text = "";
            txtH13M.Text = "";
            txtH14M.Text = "";
            txtH15M.Text = "";
            txtH16M.Text = "";
            txtH17M.Text = "";
            txtH18M.Text = "";
            txtH19M.Text = "";
            txtH20M.Text = "";
            txtH21M.Text = "";
            txtH22M.Text = "";
            txtH23M.Text = "";

            txtHora.Clear();
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
            LimpiarHorario();
            c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
            validarHorario();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtHora.Text == "")
            {
                if (MessageBox.Show("No ha seleccionado ningun horario, ¿desea salir?", "Disponibilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dpFecha.Value >= DateTime.Now)
            {
                dpFecha.Value = dpFecha.Value.AddDays(-1);
                c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
                validarHorario();
            }
            else
            {
                MessageBox.Show("No se puede seleccionar una fecha menor a la actual");
                dpFecha.Value = DateTime.Now;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dpFecha.Value = dpFecha.Value.AddDays(1);
            c.PacientesCitados(dataGridView1, dpFecha.Value.Date.ToString("yyyy/MM/dd"), Condominio, AreaComun);
            validarHorario();
        }

        private void txtH0_Click(object sender, EventArgs e)
        {
            DateTime FechaActual =Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual =Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora=Convert.ToDateTime("00:00");


            if (HoraActual>Hora && FechaActual==dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH0.Text == "")
                {
                    txtHora.Text = "00:00";
                }
                
            }
            
        }

        private void txtH0M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("00:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH0M.Text == "")
                {
                    txtHora.Text = "00:30";
                }
            }
        }

        private void txtH1_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("01:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH1.Text == "")
                {
                    txtHora.Text = "01:00";
                }
            }
        }

        private void txtH1M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("01:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH1M.Text == "")
                {
                    txtHora.Text = "01:30";
                }
            }
        }

        private void txtH2_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("02:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH2.Text == "")
                {
                    txtHora.Text = "02:00";
                }
            }
        }

        private void txtH2M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("02:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH2M.Text == "")
                {
                    txtHora.Text = "02:30";
                }
            }
        }

        private void txtH3_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("03:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH3.Text == "")
                {
                    txtHora.Text = "03:00";
                }
            }
        }

        private void txtH3M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("03:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH3M.Text == "")
                {
                    txtHora.Text = "03:30";
                }
            }
        }

        private void txtH4_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("04:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH4.Text == "")
                {
                    txtHora.Text = "04:00";
                }
            }
        }

        private void txtH4M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("04:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH4M.Text == "")
                {
                    txtHora.Text = "04:30";
                }
            }
        }

        private void txtH5_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("05:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH5.Text == "")
                {
                    txtHora.Text = "05:00";
                }
            }
        }

        private void txtH5M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("05:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH5M.Text == "")
                {
                    txtHora.Text = "05:30";
                }
            }
        }

        private void txtH6_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("06:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH6.Text == "")
                {
                    txtHora.Text = "06:00";
                }
            }
        }

        private void txtH6M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("06:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH6M.Text == "")
                {
                    txtHora.Text = "06:30";
                }
            }
        }

        private void txtH7_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("07:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH7.Text == "")
                {
                    txtHora.Text = "07:00";
                }
            }
        }

        private void txtH7M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("07:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH7M.Text == "")
                {
                    txtHora.Text = "07:30";
                }
            }
        }

        private void txtH8_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("08:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH8.Text == "")
                {
                    txtHora.Text = "08:00";
                }
            }
        }

        private void txtH8M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("08:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH8M.Text == "")
                {
                    txtHora.Text = "08:30";
                }
            }
        }

        private void txtH9_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("09:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH9.Text == "")
                {
                    txtHora.Text = "09:00";
                }
            }
        }

        private void txtH9M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("09:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH9M.Text == "")
                {
                    txtHora.Text = "09:30";
                }
            }
        }

        private void txtH10_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("10:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH10.Text == "")
                {
                    txtHora.Text = "10:00";
                }
            }
        }

        private void txtH10M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("10:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH10M.Text == "")
                {
                    txtHora.Text = "10:30";
                }
            }
        }

        private void txtH11_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("11:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH11.Text == "")
                {
                    txtHora.Text = "11:00";
                }
            }
        }

        private void txtH11M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("11:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH11M.Text == "")
                {
                    txtHora.Text = "11:30";
                }
            }
        }

        private void txtH12_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("12:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH12.Text == "")
                {
                    txtHora.Text = "12:00";
                }
            }
        }

        private void txtH12M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("12:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH12M.Text == "")
                {
                    txtHora.Text = "12:30";
                }
            }
        }

        private void txtH13_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("13:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH13.Text == "")
                {
                    txtHora.Text = "13:00";
                }
            }
        }

        private void txtH13M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("13:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH13M.Text == "")
                {
                    txtHora.Text = "13:30";
                }
            }
        }

        private void txtH14_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("14:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH14.Text == "")
                {
                    txtHora.Text = "14:00";
                }
            }
        }

        private void txtH14M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("14:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH14M.Text == "")
                {
                    txtHora.Text = "14:30";
                }
            }
        }

        private void txtH15_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("15:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH15.Text == "")
                {
                    txtHora.Text = "15:00";
                }
            }
        }

        private void txtH15M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("15:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH15M.Text == "")
                {
                    txtHora.Text = "15:30";
                }
            }
        }

        private void txtH16_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("16:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH16.Text == "")
                {
                    txtHora.Text = "16:00";
                }
            }
        }

        private void txtH16M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("16:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH16M.Text == "")
                {
                    txtHora.Text = "16:30";
                }
            }
        }

        private void txtH17_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("17:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH17.Text == "")
                {
                    txtHora.Text = "17:00";
                }
            }
        }

        private void txtH17M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("17:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH17M.Text == "")
                {
                    txtHora.Text = "17:30";
                }
            }
        }

        private void txtH18_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("18:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH18.Text == "")
                {
                    txtHora.Text = "18:00";
                }
            }
        }

        private void txtH18M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("18:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH18M.Text == "")
                {
                    txtHora.Text = "18:30";
                }
            }
        }

        private void txtH19_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("19:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH19.Text == "")
                {
                    txtHora.Text = "19:00";
                }
            }
        }

        private void txtH19M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("19:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH19M.Text == "")
                {
                    txtHora.Text = "19:30";
                }
            }
        }

        private void txtH20_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("20:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH20.Text == "")
                {
                    txtHora.Text = "20:00";
                }
            }
        }

        private void txtH20M_Click(object sender, EventArgs e)
        {

            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("20:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH20M.Text == "")
                {
                    txtHora.Text = "20:30";
                }
            }
        }

        private void txtH21_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("21:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH21.Text == "")
                {
                    txtHora.Text = "21:00";
                }
            }
        }

        private void txtH21M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("21:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH21M.Text == "")
                {
                    txtHora.Text = "21:30";
                }
            }
        }

        private void txtH22_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("22:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH22.Text == "")
                {
                    txtHora.Text = "22:00";
                }
            }
        }

        private void txtH22M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("22:30");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH22M.Text == "")
                {
                    txtHora.Text = "22:30";
                }
            }
        }

        private void txtH23_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("23:00");


            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH23.Text == "")
                {
                    txtHora.Text = "23:00";
                }
            }
        }

        private void txtH23M_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime HoraActual = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            DateTime Hora = Convert.ToDateTime("23:30");

           

            if (HoraActual > Hora && FechaActual == dpFecha.Value.Date)
            {
                MessageBox.Show("Horario no valido");
            }
            else
            {
                if (txtH23M.Text == "")
                {
                    txtHora.Text = "23:30";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtHora.Text == "")
            {
                MessageBox.Show("No ha seleccionado ningun horario");
            }
            else
            {
                Horario = txtHora.Text;
                Fecha = dpFecha.Value.Date.ToString("yyyy/MM/dd");
                this.Close();
            }

        }
    }
}
