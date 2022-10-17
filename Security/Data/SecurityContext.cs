using Microsoft.EntityFrameworkCore;
using Security.Entities;

namespace Security.Data;

public class SecurityContext : DbContext
{

    public SecurityContext(DbContextOptions contextOptions) : base(contextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<User> Users { get; set; }
        

}