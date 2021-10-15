using Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EventTicket : IEntity<long>, ISoftDeleteEntity
    {
        public long Id { get; set; }

        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        public int EventParticipantId { get; set; }

        [ForeignKey(nameof(EventParticipantId))]
        public EventParticipant EventParticipant { get; set; }
        public bool IsDeleted { get; set; }
    }
}
