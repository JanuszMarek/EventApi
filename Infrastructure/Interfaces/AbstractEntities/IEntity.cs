namespace Infrastructure.Interfaces.AbstractEntities
{
    public interface IEntity<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
