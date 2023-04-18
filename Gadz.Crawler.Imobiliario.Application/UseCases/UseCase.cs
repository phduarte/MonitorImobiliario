namespace Gadz.Crawler.Imobiliario.Application.UseCases
{
    public abstract class UseCase<TInput, TOutput>
    {
        public abstract Task<TOutput> Executar(TInput input);
    }
}
