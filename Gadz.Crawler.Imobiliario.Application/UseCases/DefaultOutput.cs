namespace Gadz.Crawler.Imobiliario.Application.UseCases
{
    public class DefaultOutput
    {
        public bool IsSuccess { get; set; } = true;

        public static DefaultOutput Success() => new()
        {
            IsSuccess = true
        };
    }
}
