namespace Gadz.Crawler.Imobiliario.Extensions;

public static class DecodeExtensions
{
    public static string Decode(this string value)
    {

        string decoded = value;

        decoded = decoded.Replace("&#231;", "ç");
        decoded = decoded.Replace("&#241;", "ñ");
        decoded = decoded.Replace("&amp;", "&");

        decoded = decoded.Replace("&#193;", "Á");
        decoded = decoded.Replace("&#225;", "á");
        decoded = decoded.Replace("&#226;", "â");
        decoded = decoded.Replace("&#227;", "ã");

        decoded = decoded.Replace("&#233;", "é");
        decoded = decoded.Replace("&#234;", "ê");

        decoded = decoded.Replace("&#237;", "í");

        decoded = decoded.Replace("&#211;", "Ó");
        decoded = decoded.Replace("&#212;", "Ô");
        decoded = decoded.Replace("&#242;", "ò");
        decoded = decoded.Replace("&#243;", "ó");
        decoded = decoded.Replace("&#244;", "ô");
        decoded = decoded.Replace("&#245;", "õ");
        decoded = decoded.Replace("&#246;", "ö");

        decoded = decoded.Replace("&#249;", "ù");
        decoded = decoded.Replace("&#250;", "ú");
        decoded = decoded.Replace("&#251;", "û");
        decoded = decoded.Replace("&#252;", "ü");

        decoded = decoded.Replace('\t', ' ');
        decoded = decoded.Replace('\r', ' ');
        decoded = decoded.Replace('\n', ' ');
        decoded = decoded.Replace("                  ", " ");
        decoded = decoded.Replace("     ", " ");

        decoded = decoded.Normalize().Trim();

        return decoded;
    }

    public static string DecodeUrl(this string url)
    {

        url = url.Replace("%22", "'");
        url = url.Replace("%7B", "{");
        url = url.Replace("%7D", "}");
        url = url.Replace("%20", " ");

        return url;
    }
}
