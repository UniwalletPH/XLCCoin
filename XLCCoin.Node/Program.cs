using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        class NodeInfo
        {
            public string Ip { get; set; }
            public string Port { get; set; }

        }

        static async Task Main(string[] args)
        {

            var Node = new NodeInfo();
            Node.Ip = "090889";
            Node.Port = "778";

            var json = JsonConvert.SerializeObject(Node);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://httpbin.org/post";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);


            //TestCommand _cmd = new TestCommand { Ip = "0.0.1", Port ="69" };
            //var _response = await Mediator.Send(_cmd);
            //Console.WriteLine("Response: {0}", _response);


            //TestGetNodesQuery _query = new TestGetNodesQuery();
            //var _nodes = await Mediator.Send(_query);
            //Console.WriteLine("Nodes: {0}", _nodes.Count());


            Console.ReadLine();
        }
    }
}