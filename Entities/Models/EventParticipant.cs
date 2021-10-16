using Entities.Interfaces;
using Entities.Interfaces.Abstract;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class EventParticipant : IEntity<int>, IEventParticipant
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
