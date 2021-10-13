using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class Context : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTicket> EventTickets { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}
