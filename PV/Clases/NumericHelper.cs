using System;
using System.Globalization;

public static class NumericHelper
{
    // Cultura que usa coma como separador de miles y punto como decimal (es-MX, en-US)
    private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en-US");

    public static decimal ParseDecimal(string text, string nombreCampo = "")
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0m;

        if (decimal.TryParse(text, NumberStyles.Number, Culture, out decimal valor))
            return valor;

        throw new FormatException($"El valor '{text}' en el campo {nombreCampo} no es un número válido.");
    }

    public static int ParseInt(string text, string nombreCampo = "")
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        if (int.TryParse(text, NumberStyles.Number, Culture, out int valor))
            return valor;

        throw new FormatException($"El valor '{text}' en el campo {nombreCampo} no es un número entero válido.");
    }

    // Si necesitas regresar el string limpio para pasarlo a un parámetro nvarchar de un SP
    public static string ToInvariantString(string text, string nombreCampo = "")
    {
        return ParseDecimal(text, nombreCampo).ToString(CultureInfo.InvariantCulture);
    }
}