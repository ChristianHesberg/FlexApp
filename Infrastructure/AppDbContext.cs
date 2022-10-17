using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Session>()
            .Property(s => s.UserId)
            .IsRequired();
        modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithMany(s => s.Sessions)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<AppUser>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<AppUser>()
            .Property(u => u.Active)
            .IsRequired();
        modelBuilder.Entity<AppUser>()
            .Property(u => u.Name)
            .IsRequired();
    }
    
    public DbSet<Session> Sessions { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}