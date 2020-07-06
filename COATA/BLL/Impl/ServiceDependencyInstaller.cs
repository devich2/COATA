using BLL.Abstract.Converter;
using BLL.Abstract.UnitTree;
using BLL.Abstract.UnitType;
using BLL.DTO.Result;
using BLL.Impl.Converter;
using BLL.Impl.UnitTree;
using BLL.Impl.UnitType;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Impl
{
    public class ServiceDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            //Search
            services.AddTransient<IUnitSelectionService, UnitSelectionService>();
            services.AddTransient<IUnitEditService, UnitEditService>();
            services.AddTransient<IUnitTypeService, UnitTypeService>();
            //Other dependencies
            services.AddTransient<IUnitTypeCache, UnitTypeCache>();
            services.AddTransient<IConverterService<int, ResponseMessageType>, HttpStatusConverterService>();
        }
    }
}