using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using XLCCoin.Application.Interfaces;
using XLCCoin.Persistence;

namespace XLCCoin.Node
{
    class Program
    {
        static IServiceCollection _services;

        static ServiceProvider ServiceProvider
        {
            get
            {
                if (_services == null) _services = new ServiceCollection();

                var _builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true);

                var _config = _builder.Build();

                _services.AddDbContext<XLCDbContext>(options =>
                {
                    options.UseSqlServer(_config.GetConnectionString("XLCConStr"));
                })
                .AddScoped<ISecurity, NetworkSecurity>()
                .AddScoped<IXLCDbContext>(provider => provider.GetService<XLCDbContext>());

                var _serviceProvider = _services.BuildServiceProvider();

                return _serviceProvider;
            }
        }

        static void Main(string[] args)
        {
            IXLCDbContext _context = ServiceProvider.GetService<IXLCDbContext>();

            var _w = _context.Nodes.ToList();

            Console.WriteLine("Nodes: {0}", _w.Count);
            Console.ReadLine();
        }
    }
}