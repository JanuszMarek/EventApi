namespace Entities.Interfaces
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}
