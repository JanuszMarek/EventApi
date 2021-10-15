using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class EventParticipant : IEntity<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
