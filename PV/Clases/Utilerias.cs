using Guna.UI2.WinForms;
using Microsoft.Reporting.WinForms;
using PV.Clases.OrdenCompra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases
{
    public class Utilerias
    {
        public static void ManejoNumero(Guna2TextBox textBox)
        {
            textBox.TextChanged += (sender, e) =>
            {
                if (int.TryParse(textBox.Text, out int number))
                {
                    textBox.Text = number.ToString();
                }
                else
                {
                    textBox.Text = "0";
                }
            };
        }
        public static void Moneda2(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Moneda2(ref Guna2TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n.Substring(1, n.Length - 1);
                }
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void Moneda4(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(5, '0'); // Cambiado a 5 para cuatro decimales
                if (n.Length > 5 && n.Substring(0, 1) == "0") // Cambiado a 5
                {
                    n.Substring(1, n.Length - 1); // Cambiado a 1
                }
                v = Convert.ToDouble(n) / 10000; // Cambiado a 10000 para cuatro decimales
                txt.Text = string.Format("{0:N4}", v); // Cambiado a {0:N4} para cuatro decimales
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Moneda4(ref Guna2TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(5, '0'); // Cambiado a 5 para cuatro decimales
                if (n.Length > 5 && n.Substring(0, 1) == "0") // Cambiado a 5
                {
                    n.Substring(1, n.Length - 1); // Cambiado a 1
                }
                v = Convert.ToDouble(n) / 10000; // Cambiado a 10000 para cuatro decimales
                txt.Text = string.Format("{0:N4}", v); // Cambiado a {0:N4} para cuatro decimales
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void ValidarFormatoMoneda(object sender, KeyPressEventArgs e)
        {
            // Prohibir el signo "-"
            if (e.KeyChar == '-')
            {
                e.Handled = true;
                return;
            }

            // Permitir números, punto decimal, y caracteres de control (como backspace)
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // Validar que solo se permita un punto decimal
            Guna2TextBox textBox = sender as Guna2TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        public static bool EsCorreoValido(string correo)
        {
            // Define la expresión regular para validar un correo electrónico
            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Crea un objeto Regex con el patrón
            Regex regex = new Regex(patronCorreo);

            // Usa el método IsMatch para verificar si el correo coincide con el patrón
            return regex.IsMatch(correo);
        }
        public static string SavePDF(ReportViewer viewer, string subcarpeta, string documento, string folio)
        {
            string carpeta = string.Empty;

            try
            {
                string noOrdenResl = $"{DateTime.Today.Month}{DateTime.Today.Year}";
                carpeta = Path.Combine(DBOrdenCompra.Ruta, subcarpeta);

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }
                carpeta = Path.Combine(carpeta, noOrdenResl);

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                string nombreArchivo = $"{documento}-{folio}.pdf";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                byte[] bytes = viewer.LocalReport.Render("PDF", @"<DeviceInfo><EmbedFonts>None</EmbedFonts><OutputFormat>PDF</OutputFormat> <PageWidth>8.5in</PageWidth>  <PageHeight>11in</PageHeight> <MarginTop>0in</MarginTop>  <MarginLeft>0in</MarginLeft> <MarginRight>0in</MarginRight> <MarginBottom>0.5in</MarginBottom></DeviceInfo>");

                File.WriteAllBytes(rutaCompleta, bytes);
                return rutaCompleta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el PDF: {ex.Message}");
                // Maneja la excepción de manera apropiada según tu aplicación
            }
            return "";
        }
        public static string QuitarNoNumeros(string input)
        {
            // Utilizar expresión regular para quitar todo excepto números
            string patron = @"[^\d]"; // El patrón [^\d] coincide con cualquier cosa que no sea un dígito
            return Regex.Replace(input, patron, "");
        }


    }
}
