using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XLCCoin.Application;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Commands;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Persistence;

namespace XLCCoin.Node
{
    class Program
    {
        static IMediator Mediator
        {
            get
            {
                return ServiceRegistration.ServiceProvider.GetService<IMediator>();
            }
        }

        static async Task Main(string[] args)
        {
            //TestCommand _cmd = new TestCommand { Name = "Devs" };
            //var _response = await Mediator.Send(_cmd);
            //Console.WriteLine("Response: {0}", _response);


            //TestGetNodesQuery _query = new TestGetNodesQuery();
            //var _nodes = await Mediator.Send(_query);
            //Console.WriteLine("Nodes: {0}", _nodes.Count());


            //Console.ReadLine();
            try
            {
                TryConnectNodeCommand _conToNode = new TryConnectNodeCommand("192.168.2.163", "My nama jeff");
                await Mediator.Send(_conToNode);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}