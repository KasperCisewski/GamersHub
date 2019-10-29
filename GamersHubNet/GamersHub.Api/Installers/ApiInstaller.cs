using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamersHub.Api.Installers
{
    public class ApiInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}
