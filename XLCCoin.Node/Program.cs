using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using XLCCoin.Application.Interfaces;
using XLCCoin.Persistence;

namespace XLCCoin.Node
{
    class Program
    {
        static void Main(string[] args)
        {
            var _serviceProvider = new ServiceCollection()
                .AddScoped<ISecurity, NetworkSecurity>()
                .AddScoped<IXLCDbContext, XLCDbContext>()
                .BuildServiceProvider();


            IXLCDbContext _context = _serviceProvider.GetService<IXLCDbContext>();

            _context.AddressKeys.ToList();




            Console.WriteLine("Hello World!");
        }
    }
}
