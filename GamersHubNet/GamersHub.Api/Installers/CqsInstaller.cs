using Gybs;
using Gybs.Logic.Operations;
using Gybs.Logic.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamersHub.Api.Installers
{
    public class CqsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddGybs(builder =>
            {
                builder.AddServiceProviderOperationBus();
                builder.AddOperationFactory();
                builder.AddOperationHandlers();
                builder.AddValidation();
            });
        }
    }
}
