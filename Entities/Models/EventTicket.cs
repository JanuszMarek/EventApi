using Entities.Interfaces.Abstract;

namespace Entities.Models
{
    public partial class EventTicket : IEntity<long>, ISoftDeleteEntity
    {
        public long Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int EventParticipantId { get; set; }

        public EventParticipant EventParticipant { get; set; }
        public bool IsDeleted { get; set; }
    }
}
