﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GamersHub.Api.Installers
{
    public static class InstallerExtentions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration) =>
            typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList()
                .ForEach(installer => installer.InstallServices(services, configuration));
    }
}
