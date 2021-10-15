using Entities.Constants;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntitySchemas
{
    public static class EventSchema
    {
        public static void CreateDatabaseScheme(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(x => x.Name)
                    .HasMaxLength(EventConstants.NAME_MAX_LENGTH)
                    .IsRequired();

                entity.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}
