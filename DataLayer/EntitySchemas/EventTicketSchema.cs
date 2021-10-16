using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntitySchemas
{
    public static class EventTicketSchema
    {
        public static void CreateDatabaseScheme(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTicket>(entity =>
            {
                entity.HasOne(x => x.Event)
                    .WithMany(x => x.EventTickets)
                    .HasForeignKey(x => x.EventId);

                entity.HasOne(x => x.EventParticipant)
                    .WithMany(x => x.EventTickets)
                    .HasForeignKey(x => x.EventParticipantId);

                entity.HasQueryFilter(x => !x.IsDeleted && !x.Event.IsDeleted);

                //entity.HasQueryFilter(x => !x.Event.IsDeleted);
            });
        }
    }
}
