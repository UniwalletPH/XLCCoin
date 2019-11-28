using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XLCCoin.Application.Interfaces;

namespace XLCCoin.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<XLCDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("XLCConStr"));
            });

            services.AddScoped<IXLCDbContext>(provider => provider.GetService<XLCDbContext>());

            return services;
        }
    }
}
 