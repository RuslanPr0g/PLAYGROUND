using Microsoft.EntityFrameworkCore;
using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Persistence;

public class PristineItDbContext : DbContext
{
    public DbSet<TodoTask> Tasks => Set<TodoTask>();

    public PristineItDbContext(DbContextOptions<PristineItDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Task configuration
        modelBuilder.Entity<TodoTask>(b =>
        {
            b.HasKey(t => t.Id);
            b.Property(t => t.Title).HasConversion(
                v => v.Value,
                v => Title.Create(v).Value!);
            b.Property(t => t.Description).HasConversion(
                v => v!.Value,
                v => Description.Create(v).Value);
            b.Property(t => t.Priority).HasConversion(
                v => v.Level,
                v => Priority.Create(v).Value!);
            b.Property(t => t.IsCompleted);
            b.Property(t => t.DueDate);
            b.Property(t => t.CreatedAt);
            b.Property(t => t.CompletedAt);

            b.OwnsMany(t => t.Tags, tb =>
            {
                tb.WithOwner().HasForeignKey("TaskId");
                tb.Property<string>("Value").HasColumnName("Tag");
                tb.HasKey("TaskId", "Value");
            });
        });
    }
}