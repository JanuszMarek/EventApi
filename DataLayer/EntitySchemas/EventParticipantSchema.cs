using Entities.Constants;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntitySchemas
{
    public static class EventParticipantSchema
    {
        public static void CreateDatabaseScheme(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>(entity =>
            {
                entity.Property(x => x.FirstName)
                    .HasMaxLength(EventParticipantConstants.FIRST_NAME_MAX_LENGTH)
                    .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasMaxLength(EventParticipantConstants.LAST_NAME_MAX_LENGTH)
                    .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasMaxLength(EventParticipantConstants.EMAIL_MAX_LENGTH)
                    .IsRequired();
            });
        }
    }
}
