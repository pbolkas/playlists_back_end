using back_end.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.Install_Services
{
  public class MySQLInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<MySQLContext>(options =>
            options.UseMySql("",x => x.ServerVersion("5.7.0-mysql")));
    }
  }
}