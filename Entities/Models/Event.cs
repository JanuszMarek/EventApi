using Entities.Constants;
using Entities.Interfaces.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class Event : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(EventConstants.TICKET_POOL_MIN_VALUE, EventConstants.TICKET_POOL_MAX_VALUE)]
        public int TicketPool { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
