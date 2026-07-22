using PuntoVentas.Clases.DatosEmpresa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PV.Clases
{
    public static class CorreosMasivos
    {
        //private string remitente;
        //private string contraseña;
        //private string servidor;
        //private string host;
        //private string puerto;
        //private string ssl;

        public static bool EnviarCorreosMime(string asunto, string mensaje, byte[] reportePdf, string nombreArchivo, string destino)
        {
            try
            {
                DBDatosEmpresa d = new DBDatosEmpresa();
                string[] configuracionCorreo = d.CorreoContra();

                if (configuracionCorreo == null)
                {
                    MessageBox.Show("No se pudo obtener la configuración de correo.");
                    return false;
                }

                string usuarioCorreo = configuracionCorreo[0]; // "ventas.cuernavaca"
                string dominioCorreo = configuracionCorreo[1]; // "rcr-itsystems.com"
                string contraseña = configuracionCorreo[2]; // "Ptia#2023"
                string puerto = configuracionCorreo[3]; // "465" o "587"
                string host = configuracionCorreo[4];

                string correoCompleto = $"{usuarioCorreo}@{dominioCorreo}".Trim();

                var email = new MimeKit.MimeMessage();
                email.From.Add(new MimeKit.MailboxAddress("Ventas", correoCompleto));
                email.To.Add(MimeKit.MailboxAddress.Parse(destino));
                email.Subject = asunto;

                var builder = new MimeKit.BodyBuilder();
                builder.HtmlBody = mensaje;

                if (reportePdf != null && !string.IsNullOrEmpty(nombreArchivo))
                {
                    // MimeKit.ContentType especificado explícitamente para evitar ambigüedad
                    builder.Attachments.Add(nombreArchivo, reportePdf, MimeKit.ContentType.Parse("application/pdf"));
                }

                email.Body = builder.ToMessageBody();

                // MailKit.Net.Smtp.SmtpClient especificado explícitamente para evitar conflicto con System.Net.Mail
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    int portNum = int.Parse(puerto);

                    // Si usas puerto 465 usas SslOnConnect, si usas 587 usas StartTls
                    MailKit.Security.SecureSocketOptions opcionesSsl = portNum == 465
                        ? MailKit.Security.SecureSocketOptions.SslOnConnect
                        : MailKit.Security.SecureSocketOptions.StartTls;

                    client.Connect(host, portNum, opcionesSsl);
                    client.Authenticate(correoCompleto, contraseña);

                    client.Send(email);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //public void EnviarCorreosMasivos(string asunto, string mensaje, string archivo, List<string> destinatarios)
        //{

        //    try
        //    {
        //        using (SmtpClient smtpClient = new SmtpClient())
        //        {
        //            if (servidor == "hotmail.com")
        //            {
        //                smtpClient.Host = "smtp.live.com";
        //                smtpClient.Port = 587;
        //                smtpClient.EnableSsl = true;
        //            }
        //            else if (servidor == "gmail.com")
        //            {
        //                smtpClient.Host = "smtp.gmail.com";
        //                smtpClient.Port = 587;
        //                smtpClient.EnableSsl = true;

        //            }
        //            else
        //            {
        //                smtpClient.Host = host;
        //                smtpClient.Port = int.Parse(puerto);
        //                smtpClient.EnableSsl = bool.Parse(ssl);
        //            }
        //            smtpClient.Credentials = new NetworkCredential(remitente + "@" + servidor, contraseña);


        //            foreach (string destino in destinatarios)
        //            {
        //                using (MailMessage mailMessage = new MailMessage())
        //                {
        //                    mailMessage.From = new MailAddress(remitente + "@" + servidor);
        //                    mailMessage.To.Add(destino);
        //                    mailMessage.Subject = asunto;
        //                    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
        //                    mailMessage.Body = mensaje;
        //                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //                    if (archivo != string.Empty)
        //                    {
        //                        mailMessage.Attachments.Add(new Attachment(archivo));
        //                    }
        //                    smtpClient.Send(mailMessage);

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al enviar correos masivos: {ex.Message}");
        //    }
        //}

        public static bool EnviarCorreos(string asunto, string mensaje, byte[] reportePdf, string nombreArchivo, string destino)
        {
            try
            {
                DBDatosEmpresa d = new DBDatosEmpresa();
                string[] configuracionCorreo = d.CorreoContra();
                if (configuracionCorreo != null)
                {
                    string correo = configuracionCorreo[0];
                    string servidor = configuracionCorreo[1];
                    string contraseña = configuracionCorreo[2];
                    string puerto = configuracionCorreo[3];
                    string host = configuracionCorreo[4];
                    string ssl = configuracionCorreo[5];

                    string remitente = correo;

                    using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient())
                    {
                        smtpClient.Host = host;
                        smtpClient.Port = int.Parse(puerto);
                        smtpClient.EnableSsl = ssl.Equals("true", StringComparison.OrdinalIgnoreCase);
                        smtpClient.UseDefaultCredentials = false; // OBLIGATORIO colocar antes de Credentials
                        smtpClient.Credentials = new NetworkCredential(remitente + "@" + servidor, contraseña);

                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress(remitente + "@" + servidor);
                            mailMessage.To.Add(destino);
                            mailMessage.Subject = asunto;
                            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                            mailMessage.Body = mensaje;
                            mailMessage.IsBodyHtml = true;
                            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                            if (reportePdf != null && !string.IsNullOrEmpty(nombreArchivo))
                            {
                                mailMessage.Attachments.Add(new Attachment(new MemoryStream(reportePdf), nombreArchivo));
                            }

                            smtpClient.Send(mailMessage);
                        }

                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la configuración de correo.");
                    return false;
                }
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show($"Error al enviar el correo: {smtpEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar correos masivos: {ex.Message}");
                return false;
            }
        }


        //public void EnviarCorreoss(string asunto, string mensaje, string archivo, string destino)
        //{
        //    try
        //    {
        //        var from = new MailboxAddress(name: "From", address: remitente + "@" + servidor);
        //        var to = new MailboxAddress(name: "To", address: destino);
        //        var msj = new MimeMessage();
        //        msj.From.Add(from);
        //        msj.To.Add(to);
        //        msj.Subject = asunto;
        //        var bodyBuilder = new BodyBuilder();
        //        bodyBuilder.TextBody = mensaje;
        //        msj.Body = bodyBuilder.ToMessageBody();


        //        var attachment = new MimePart()
        //        {
        //            Content = new MimeContent(File.OpenRead(archivo), ContentEncoding.Default),
        //            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        //            ContentTransferEncoding = ContentEncoding.Base64,
        //            FileName = archivo
        //        };

        //        var multipart = new Multipart("mixed");
        //        multipart.Add(attachment);
        //        multipart.Add(msj.Body);
        //        msj.Body = multipart;


        //        var client = new MailKit.Net.Smtp.SmtpClient();

        //        client.Connect(host: host,
        //                       port: int.Parse(puerto),
        //                       options: MailKit.Security.SecureSocketOptions.StartTls);

        //        //https://support.microsoft.com/es-es/account-billing/uso-de-contrase%C3%B1as-de-la-aplicaci%C3%B3n-con-aplicaciones-que-no-admiten-la-verificaci%C3%B3n-en-dos-pasos-5896ed9b-4263-e681-128a-a6f2979a7944
        //        client.Authenticate(remitente + "@" + servidor, contraseña);

        //        client.Send(msj);

        //        client.Disconnect(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al enviar correos masivos: {ex.Message}");
        //    }
        //}
        //public void SavePDF(ReportViewer viewer)
        //{
        //    //c.AbrirConexion();
        //    string NoOrdenResl = System.DateTime.Today.Month.ToString() + System.DateTime.Today.Year.ToString();

        //    string Carpeta = DBOrdenCompra.Ruta + @"\" + NoOrdenResl;

        //    try
        //    {
        //        if (Directory.Exists(Carpeta))
        //        {

        //        }
        //        else
        //        {
        //            Directory.CreateDirectory(Carpeta);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    Carpeta = DBOrdenCompra.Ruta + @"\" + NoOrdenResl + @"\" + folio + "-" + cliente + "-" + condominio + "-" + claveSub + ".pdf";

        //    byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: @"<DeviceInfo><EmbedFonts>None</EmbedFonts><OutputFormat>PDF</OutputFormat> <PageWidth>8.5in</PageWidth>  <PageHeight>11in</PageHeight> <MarginTop>0in</MarginTop>  <MarginLeft>0in</MarginLeft> <MarginRight>0in</MarginRight> <MarginBottom>0.5in</MarginBottom></DeviceInfo>");

        //    using (FileStream stream = new FileStream(Carpeta, FileMode.Create))
        //    {
        //        stream.Write(Bytes, 0, Bytes.Length);
        //    }
        //    //c.CerrarConexion();
        //}

    }
}
