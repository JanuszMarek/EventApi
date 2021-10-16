namespace Entities.Interfaces.Abstract
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}
