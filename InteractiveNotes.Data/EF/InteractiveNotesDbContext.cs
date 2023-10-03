using InteractiveNotes.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNotes.Data.EF
{
    public class InteractiveNotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public InteractiveNotesDbContext(DbContextOptions<InteractiveNotesDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.CreatingDate)
                      .HasColumnType("timestamp without time zone")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); 
            });
        }

    }
}
