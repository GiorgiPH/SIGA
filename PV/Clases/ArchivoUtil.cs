using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PV.Clases
{
    /// <summary>
    /// Utilería con las operaciones de archivo que se repetían en RegistroGastos2
    /// (crear carpeta, copiar, abrir, eliminar, convertir a Base64 y detectar tipo).
    /// La lógica de cada operación es exactamente la misma que existía en el formulario,
    /// únicamente se centralizó aquí para evitar duplicidad y facilitar su mantenimiento.
    ///
    /// GuardarAdjunto se agregó al revisar RegistrarCobro.cs: ese formulario (y,
    /// según se comentó, varios otros) copiaba un archivo elegido por el usuario
    /// hacia una carpeta de "adjuntos" con el mismo patrón (crear carpeta, copiar
    /// preservando la extensión original, guardar solo el nombre generado). Se
    /// consolidó ese patrón aquí; la construcción de la ruta de la carpeta en sí
    /// (base + prefijo + identificador) se dejó en cada formulario porque el
    /// prefijo/convención puede variar entre pantallas y no lo tengo confirmado
    /// para las demás.
    /// </summary>
    public static class ArchivoUtil
    {
        /// <summary>
        /// Crea la carpeta indicada si todavía no existe (misma lógica que se repetía
        /// en los distintos manejadores de eventos de adjuntar archivo).
        /// </summary>
        public static void CrearCarpetaSiNoExiste(string ruta)
        {
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
        }

        /// <summary>
        /// Copia un archivo de origen a destino. Por default no sobrescribe,
        /// igual que el comportamiento original de File.Copy(origen, destino).
        /// </summary>
        public static void CopiarArchivo(string origen, string destino, bool sobrescribir = false)
        {
            File.Copy(origen, destino, sobrescribir);
        }

        /// <summary>
        /// Copia "archivoOrigen" hacia "carpetaDestino", usando "nombreDestino"
        /// (sin extensión) + la extensión original del archivo de origen como
        /// nombre final. Regresa el nombre de archivo generado (el que se debe
        /// guardar en la base de datos, p. ej. columna Archivo de Cobros).
        /// No crea la carpeta destino: llamar CrearCarpetaSiNoExiste antes si
        /// hace falta, igual que hacía el código original.
        /// </summary>
        public static string GuardarAdjunto(string archivoOrigen, string carpetaDestino, string nombreDestino)
        {
            string extension = Path.GetExtension(archivoOrigen);
            string nombreArchivo = nombreDestino + extension;
            string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);
            CopiarArchivo(archivoOrigen, rutaDestino);
            return nombreArchivo;
        }

        /// <summary>
        /// Abre un archivo con la aplicación asociada por default del sistema.
        /// </summary>
        public static void AbrirArchivo(string ruta)
        {
            Process.Start(ruta);
        }

        /// <summary>
        /// Abre un archivo forzando UseShellExecute = true (usado para archivos
        /// temporales generados a partir de contenido en Base64).
        /// </summary>
        public static void AbrirArchivoConShell(string ruta)
        {
            Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
        }

        /// <summary>
        /// Elimina el archivo si existe. Si no existe, no hace nada (mismo
        /// comportamiento que tenía File.Delete en el código original).
        /// </summary>
        public static void EliminarArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }

        /// <summary>
        /// Lee un archivo del disco y regresa su contenido codificado en Base64.
        /// </summary>
        public static string ConvertirArchivoABase64(string ruta)
        {
            byte[] archivoBytes = File.ReadAllBytes(ruta);
            return Convert.ToBase64String(archivoBytes);
        }

        /// <summary>
        /// Detecta el tipo de archivo (pdf, xml, png o desconocido) a partir de su
        /// contenido en Base64, revisando las firmas/encabezados de cada formato.
        /// Misma lógica que existía en el método privado DetectarTipo del formulario.
        /// </summary>
        public static string DetectarTipoArchivo(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            // PDF: inicia con %PDF
            if (bytes.Length > 4 &&
                bytes[0] == 0x25 &&
                bytes[1] == 0x50 &&
                bytes[2] == 0x44 &&
                bytes[3] == 0x46)
            {
                return "pdf";
            }

            // XML: inicia con <?xml
            string texto = Encoding.UTF8.GetString(bytes);
            if (texto.TrimStart().StartsWith("<?xml"))
            {
                return "xml";
            }

            // PNG: firma 89 50 4E 47 0D 0A 1A 0A
            if (bytes.Length > 8 &&
                bytes[0] == 0x89 &&
                bytes[1] == 0x50 &&
                bytes[2] == 0x4E &&
                bytes[3] == 0x47 &&
                bytes[4] == 0x0D &&
                bytes[5] == 0x0A &&
                bytes[6] == 0x1A &&
                bytes[7] == 0x0A)
            {
                return "png";
            }

            return "desconocido";
        }

        /// <summary>
        /// Regresa la extensión de archivo correspondiente al tipo detectado.
        /// Mismo mapeo (pdf/xml/png/bin) que existía en el switch original.
        /// </summary>
        public static string ObtenerExtensionPorTipo(string tipo)
        {
            switch (tipo)
            {
                case "pdf":
                    return ".pdf";
                case "xml":
                    return ".xml";
                case "png":
                    return ".png";
                default:
                    return ".bin";
            }
        }

        /// <summary>
        /// A partir de contenido Base64, detecta el tipo, arma un archivo temporal
        /// en la carpeta temporal del sistema y regresa la ruta completa generada.
        /// </summary>
        public static string GuardarArchivoTemporalDesdeBase64(string base64, string nombreArchivo)
        {
            string tipo = DetectarTipoArchivo(base64);
            string extension = ObtenerExtensionPorTipo(tipo);

            string rutaTemporal = Path.Combine(Path.GetTempPath(), nombreArchivo + extension);
            byte[] bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(rutaTemporal, bytes);

            return rutaTemporal;
        }
    }
}