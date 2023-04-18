namespace Gadz.Crawler.Imobiliario.Domain;

public abstract class Entidade
{
    public virtual Guid Id { get; init; } = Guid.NewGuid();

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Entidade entidade && Id.Equals(entidade.Id);
    }
}
