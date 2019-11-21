using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using XLCCoin.Application;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Commands;
using XLCCoin.Persistence;

namespace XLCCoin.Node
{
    class Program
    {
        static ServiceProvider ServiceProvider
        {
            get
            {
                return ServiceRegistration.ServiceProvider;
            }
        }

        static void Main(string[] args)
        {
            IXLCDbContext _context = ServiceProvider.GetService<IXLCDbContext>();
            IMediator _mediator = ServiceProvider.GetService<IMediator>();

            var _w = _context.Nodes.ToList();

            Console.WriteLine("Nodes: {0}", _w.Count);

            TestCommand _cmd = new TestCommand
            {
                Name = "Devs"
            };

            var _response = _mediator.Send(_cmd).Result;

            Console.WriteLine("Response: {0}", _response);

            Console.ReadLine();
        }
    }
}