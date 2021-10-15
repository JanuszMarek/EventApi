using DataLayer.EntitySchemas;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTicket> EventTickets { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EventSchema.CreateDatabaseScheme(modelBuilder);
            EventTicketSchema.CreateDatabaseScheme(modelBuilder);
            EventParticipantSchema.CreateDatabaseScheme(modelBuilder);
        }
    }
}
