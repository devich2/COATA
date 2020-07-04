using System;
using DAL.Abstract;
using DAL.Abstract.IRepository;
using DAL.Abstract.Transactions;
using DAL.Impl;
using DAL.Impl.ImplRepository;
using DAL.Impl.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DalDependencyInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings")
                                   ?? configuration.GetConnectionString("DefaultConnection");
            //Configure database context
            services.AddDbContext<CoataDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    p =>
                    {
                        p.EnableRetryOnFailure();
                        p.MigrationsAssembly("DAL");
                    });
            });

            services.AddTransient<IUnitTreeRepository, UnitTreeRepository>();
            services.AddTransient<IUnitTypeRepository, UnitTypeRepository>();
            services.AddTransient<IUnitTypeParentShipRepository, UnitTypeParentShipRepository>();
            services.AddTransient<IUnitClassificationRepository, UnitClassificationRepository>();
            
            //Other dependencies
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionManager, DbTransactionManager>();

        }
    }
}