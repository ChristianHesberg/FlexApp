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
            .Property(s => s.EmployeeId)
            .IsRequired();
        modelBuilder.Entity<Session>()
            .HasOne(s => s.Employee)
            .WithMany(s => s.Sessions)
            .HasForeignKey(s => s.EmployeeId);

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Schedule>()
            .Property(s => s.StartTime)
            .IsRequired();
        modelBuilder.Entity<Schedule>()
            .Property(s => s.EndTime)
            .IsRequired();
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.Employee)
            .WithMany(u => u.ScheduledDays)
            .HasForeignKey(s => s.EmployeeId);

        modelBuilder.Entity<Employee>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Employee>()
            .Property(u => u.Name)
            .IsRequired();
    }
    
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Employee> Employees { get; set; }
}