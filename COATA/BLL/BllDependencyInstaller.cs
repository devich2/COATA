using AutoMapper;
using BLL.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public class BllDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            ServiceDependencyInstaller.Install(services);
            services.AddAutoMapper(typeof(BllDependencyInstaller));
        }
    }
}