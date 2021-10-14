﻿using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Event : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TicketPool { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<EventTicket> EventTickets { get; set; }
    }
}
