using BLL.Abstract.UnitTree;
using BLL.DTO.Result;
using BLL.Impl.UnitTree;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Impl
{
    public class ServiceDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //Search
            services.AddTransient<IUnitSelectionService, UnitSelectionService>();
            //Other dependencies
        }
    }
}