using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.Install_Services
{
  interface IInstaller
  {
    void InstallServices(IServiceCollection services, IConfiguration configuration);
  }
}