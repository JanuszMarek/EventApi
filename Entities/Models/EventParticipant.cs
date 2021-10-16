using Entities.Interfaces;
using Entities.Interfaces.Abstract;
using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class EventParticipant : IEntity<int>, IEventParticipant
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EventParticipant participant &&
                   Id == participant.Id &&
                   FirstName == participant.FirstName &&
                   LastName == participant.LastName &&
                   Email == participant.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email);
        }
    }
}
