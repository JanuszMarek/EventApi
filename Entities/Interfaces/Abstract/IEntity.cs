namespace Entities.Interfaces.Abstract
{
    public interface IEntity<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
