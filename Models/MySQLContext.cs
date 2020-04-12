using Microsoft.EntityFrameworkCore;

namespace back_end.Models
{
  /**
    This is where we connect with MySQL DB to make operations on Users
  */
  public class MySQLContext : DbContext
  {
    public MySQLContext()
    {}

    public MySQLContext(DbContextOptions<MySQLContext> options):base(options)
    {}

    public DbSet<User> Users {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if(!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseMySQL("");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<User>(entity =>{
        entity.HasKey(e => e.Id);

      });
    }

  }
}