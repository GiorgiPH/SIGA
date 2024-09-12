using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using System.Net.Mail;

namespace PV
{
    public partial class ReporteReciboAutomatico : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        public static string Carpeta = string.Empty;
        public static int Opcion = 0;
        public static int Opcion2 = 0;
        string folio = string.Empty;
        string cliente = string.Empty;
        string condominio = string.Empty;
        string claveSub = string.Empty;
        string Correo = string.Empty;
        string Correo2 = string.Empty;

        public ReporteReciboAutomatico(string Folio, string Cliente, string Condominio, string ClaveSub, string correo, string correo2)
        {
            InitializeComponent();
            folio = Folio;
            cliente = Cliente;
            condominio = Condominio;
            claveSub = ClaveSub;
            Correo = correo;
            Correo2 = correo2;
        }

        private void ReporteReciboAutomatico_Load(object sender, EventArgs e)
        {
            c.ruta();

            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet47.Recibo' Puede moverla o quitarla según sea necesario.
            this.ReciboTableAdapter.Fill(this.ControlCondominiosDataSet47.Recibo, Convert.ToInt32(folio));

            this.reportViewer1.RefreshReport();

            try
            {
                SavePDF(reportViewer1);
            }
            catch (Exception)
            {
                Opcion = 1;
            }

            if (Opcion!=1)
            {
                try
                {
                    enviarcorreo();
                }
                catch (Exception)
                {
                    Opcion2 = 1;
                }
            }
            
            this.Close();
        }

        public void SavePDF(ReportViewer viewer)
        {
            string NoOrdenResl = System.DateTime.Today.Month.ToString() + System.DateTime.Today.Year.ToString();

            Carpeta = DBOrdenCompra.Ruta + @"\" + NoOrdenResl;

            try
            {
                if (Directory.Exists(Carpeta))
                {

                }
                else
                {
                    Directory.CreateDirectory(Carpeta);
                }
            }
            catch (Exception)
            {

                throw;
            }

            Carpeta = DBOrdenCompra.Ruta + @"\" + NoOrdenResl + @"\" + folio + "-" + cliente + "-" + condominio + "-" + claveSub + ".pdf";

            byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "<DeviceInfo><OutputFormat>PDF</OutputFormat> <PageWidth>8.5in</PageWidth>  <PageHeight>11in</PageHeight> <MarginTop>0in</MarginTop>  <MarginLeft>0in</MarginLeft> <MarginRight>0in</MarginRight> <MarginBottom>0.5in</MarginBottom></DeviceInfo>");

            using (FileStream stream = new FileStream(Carpeta, FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }

        void enviarcorreo()
        {
            string Mail = "";
            c.CorreoContra();

            if (DBOrdenCompra.Servidor == "hotmail.com")
            {
                if (Correo != string.Empty)
                {
                    Mail = Correo;

                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    mmsg.To.Add(Mail);
                    mmsg.Subject = "Recibo";
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    mmsg.Body = string.Empty;
                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mmsg.IsBodyHtml = false;

                    mmsg.Attachments.Add(new Attachment(Carpeta));


                    mmsg.From = new System.Net.Mail.MailAddress(DBOrdenCompra.Correo + "@hotmail.com");


                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                    cliente.Credentials = new System.Net.NetworkCredential(DBOrdenCompra.Correo + "@hotmail.com", DBOrdenCompra.Contraseña);
                    cliente.Port = 587;
                    cliente.EnableSsl = true;
                    cliente.Host = "smtp.live.com";

                    //try
                    //{
                        cliente.Send(mmsg);
                    //}
                    //catch (Exception ex)
                    //{

                    //    //MessageBox.Show("Error" + ex.ToString());
                    //}
                }

                if (Correo2 != string.Empty)
                {
                    Mail = Correo2;

                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    mmsg.To.Add(Mail);
                    mmsg.Subject = "Recibo";
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    mmsg.Body = string.Empty;
                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mmsg.IsBodyHtml = false;

                    mmsg.Attachments.Add(new Attachment(Carpeta));


                    mmsg.From = new System.Net.Mail.MailAddress(DBOrdenCompra.Correo + "@hotmail.com");


                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                    cliente.Credentials = new System.Net.NetworkCredential(DBOrdenCompra.Correo + "@hotmail.com", DBOrdenCompra.Contraseña);
                    cliente.Port = 587;
                    cliente.EnableSsl = true;
                    cliente.Host = "smtp.live.com";

                    //try
                    //{
                        cliente.Send(mmsg);
                    //}
                    //catch (Exception ex)
                    //{

                    //    //MessageBox.Show("Error" + ex.ToString());
                    //}
                }
            }
            else if (DBOrdenCompra.Servidor == "gmail.com")
            {
                if (Correo != string.Empty)
                {
                    Mail = Correo;

                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    mmsg.To.Add(Mail);
                    mmsg.Subject = "Recibo";
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    mmsg.Body = string.Empty;
                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mmsg.IsBodyHtml = false;

                    mmsg.Attachments.Add(new Attachment(Carpeta));

                    mmsg.From = new System.Net.Mail.MailAddress(DBOrdenCompra.Correo + "@" + DBOrdenCompra.Servidor);


                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                    cliente.Credentials = new System.Net.NetworkCredential(DBOrdenCompra.Correo + "@" + DBOrdenCompra.Servidor, DBOrdenCompra.Contraseña);
                    cliente.Port = 587;
                    cliente.EnableSsl = true;
                    cliente.Host = "smtp.gmail.com";

                    //try
                    //{
                        cliente.Send(mmsg);
                    //}
                    //catch (Exception ex)
                    //{

                    //    //MessageBox.Show("Error" + ex.ToString());
                    //}
                }
                if (Correo2 != string.Empty)
                {
                    Mail = Correo2;

                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    mmsg.To.Add(Mail);
                    mmsg.Subject = "Recibo";
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    mmsg.Body = string.Empty;
                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mmsg.IsBodyHtml = false;

                    mmsg.Attachments.Add(new Attachment(Carpeta));

                    mmsg.From = new System.Net.Mail.MailAddress(DBOrdenCompra.Correo + "@" + DBOrdenCompra.Servidor);


                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                    cliente.Credentials = new System.Net.NetworkCredential(DBOrdenCompra.Correo + "@" + DBOrdenCompra.Servidor, DBOrdenCompra.Contraseña);
                    cliente.Port = 587;
                    cliente.EnableSsl = true;
                    cliente.Host = "smtp.gmail.com";

                    //try
                    //{
                        cliente.Send(mmsg);
                    //}
                    //catch (Exception ex)
                    //{

                    //    //MessageBox.Show("Error" + ex.ToString());
                    //}
                }
            }
        }
    }
}
