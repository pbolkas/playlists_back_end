using back_end.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.Install_Services
{
  public class MongoInstaller : IInstaller
  {
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {

      services.Configure<MongoDBSettings>( options =>
      {
        options.ConnectionString = configuration.GetSection("MongoDBSettings:ConnectionString").Value;
        options.CollectionName = configuration.GetSection("MongoDBSettings:MongoCollection").Value;
      });
    }
  }

}