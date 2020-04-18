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

    public virtual DbSet<UserModel> Users {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if(!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseMySql("",x => x.ServerVersion("5.7.0-mysql"));
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserModel>(entity =>{
        entity.HasKey(e => e.Id);
        entity.HasIndex( e=> e.UserId).IsUnique();
        entity.HasIndex( e=> e.Email).IsUnique();

      });
    }

  }
}