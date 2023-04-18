namespace Gadz.Crawler.Imobiliario.Extensions;

public static class JustNumbersExtensions
{
    public static int JustNumber(this string value)
    {

        string resultado = string.Empty;

        for (int i = 0; i < value.Length; i++)
        {

            char c = char.Parse(value.Substring(i, 1));

            if (char.IsNumber(c))
            {
                resultado += c;
            }
        }

        return int.Parse(resultado);
    }
}
