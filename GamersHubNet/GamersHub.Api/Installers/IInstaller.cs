using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamersHub.Api.Installers
{
    interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
