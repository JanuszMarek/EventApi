using Entities.Constants;
using Entities.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Event : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [MaxLength(EventConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Range(0, EventConstants.TICKET_POOL_MAX_VALUE)]
        public int TicketPool { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
