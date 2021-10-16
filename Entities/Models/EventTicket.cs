using Entities.Interfaces.Abstract;
using System;

namespace Entities.Models
{
    public class EventTicket : IEntity<long>, ISoftDeleteEntity
    {
        public long Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int EventParticipantId { get; set; }

        public EventParticipant EventParticipant { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EventTicket ticket &&
                   Id == ticket.Id &&
                   EventId == ticket.EventId &&
                   EventParticipantId == ticket.EventParticipantId &&
                   IsDeleted == ticket.IsDeleted;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, EventId, EventParticipantId, IsDeleted);
        }
    }
}
