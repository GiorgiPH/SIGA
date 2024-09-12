using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PV.Clases.OrdenCompra;
using System.Net.Mail;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;

namespace PV
{
    public partial class ReciboEgreso : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        string folio = string.Empty;
        string Correo = string.Empty;
        string Correo2 = string.Empty;
        string cliente = string.Empty;
        public static string Carpeta = string.Empty;

        public ReciboEgreso(string Folio, string propietario)
        {
            InitializeComponent();
            folio = Folio;
            cliente = propietario;
        }

        private void ReciboEgreso_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet31.DatosEmpresa' Puede moverla o quitarla según sea necesario.
            this.DatosEmpresaTableAdapter.Fill(this.ControlCondominiosDataSet31.DatosEmpresa);
            // TODO: esta línea de código carga datos en la tabla 'ControlCondominiosDataSet49.Egreso_General' Puede moverla o quitarla según sea necesario.
            this.Egreso_GeneralTableAdapter.Fill(this.ControlCondominiosDataSet49.Egreso_General, Convert.ToInt32(folio));

            this.reportViewer1.RefreshReport();

            PageSettings pg = new PageSettings();
            pg.PaperSize = reportViewer1.GetPageSettings().PaperSize;
            pg.Margins.Left = 2;
            pg.Margins.Right = 2;
            pg.Margins.Top = 2;
            pg.Margins.Bottom = 2;
            this.reportViewer1.SetPageSettings(pg);


            c.ruta();
            c.Consultapropicorreo2(cliente, txtcorreo, txtcorreo2);
            Correo = txtcorreo.Text;
            Correo2 = txtcorreo2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SavePDF(reportViewer1);
            }
            catch (Exception)
            {

                MessageBox.Show("Revisar la ruta en Parametros -> Datos Condominio");
                return;
            }
            try
            {
                enviarcorreo();
            }
            catch (Exception)
            {
                MessageBox.Show("No es posible enviar correos, revise los datos de correo en Parametros -> Datos Condominio o los permisos de seguridad de su correo");
                return;
            }
        }

        public void SavePDF(ReportViewer viewer)
        {
            string NoOrdenResl = "Cobros" + System.DateTime.Today.Month.ToString() + System.DateTime.Today.Year.ToString();

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

            Carpeta = DBOrdenCompra.Ruta + @"\" + NoOrdenResl + @"\" + folio + "-" + cliente + ".pdf";

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

                    try
                    {
                        cliente.Send(mmsg);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error" + ex.ToString());
                    }
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

                    try
                    {
                        cliente.Send(mmsg);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error" + ex.ToString());
                    }
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

                    try
                    {
                        cliente.Send(mmsg);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error" + ex.ToString());
                    }
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

                    try
                    {
                        cliente.Send(mmsg);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error" + ex.ToString());
                    }
                }
            }
            MessageBox.Show("Correo Enviado");
        }

    }
}
