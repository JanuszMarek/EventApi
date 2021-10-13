using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(70)]
        public string Email { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
