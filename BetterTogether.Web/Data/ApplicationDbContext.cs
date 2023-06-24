using BetterTogether.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetterTogether.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventSignUp> EventSignUps { get; set; }
    public DbSet<SignUp> SignUps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable(nameof(Event));

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(512);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.CreationDate)
                .HasDefaultValue(DateTime.Now);
        });

        modelBuilder.Entity<EventSignUp>(entity =>
        {
            entity.ToTable(nameof(EventSignUp));

            entity.HasKey(e => new {e.EventId, e.SignUpId });

            entity.HasOne(e => e.Event)
                .WithMany(d => d.SignUps)
                .HasForeignKey(e => e.EventId);

            entity.HasOne(e => e.SignUp)
                .WithMany(d => d.Events)
                .HasForeignKey(e => e.SignUpId);
        });

        modelBuilder.Entity<SignUp>(entity =>
        {
            entity.ToTable(nameof(SignUp));

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(e => e.CreationDate)
                .HasDefaultValue(DateTime.Now);
        });
    }
}
