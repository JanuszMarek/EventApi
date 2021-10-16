using DataLayer.EntitySchemas;
using Entities.Interfaces.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public virtual async Task<int> SaveChangesAsync()
        {
            OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync();
            OnAfterSaveChanges();

            return result;
        }

        private void OnBeforeSaveChanges()
        {
            MarkDeletedEntitiesAsSoftDeleted();
        }

        private void MarkDeletedEntitiesAsSoftDeleted()
        {
            var softDeletedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted && x.Entity is ISoftDeleteEntity);

            foreach (var softDeletedEntity in softDeletedEntries)
            {
                softDeletedEntity.State = EntityState.Modified;
                softDeletedEntity.CurrentValues[nameof(ISoftDeleteEntity.IsDeleted)] = true;
            }
        }

        private void OnAfterSaveChanges()
        {
        }

    }
}
