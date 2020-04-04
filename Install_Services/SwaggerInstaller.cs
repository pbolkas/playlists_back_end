using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace back_end.Install_Services
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title="Playlists API",
                    Version = "v1",
                    Description = "Playlists App API",
                    Contact=new OpenApiContact
                    {
                        Name = "Paul Bolkas",
                        Email = "dev@playlists.com (doesn't exist)"
                    }
                });
            });
        }
    }
}
