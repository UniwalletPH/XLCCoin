﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using XLCCoin.Domain.Entities;
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
            var _url = "http://192.168.1.7:5000/Node/Register";


            var _myEndpoint = await Mediator.Send(new GenerateSelfNodeEndpointCommand());
            var _sendSelf = new SendSelfCommand(_myEndpoint, _url);

            var _response = await Mediator.Send(_sendSelf);

            Console.WriteLine(JsonConvert.SerializeObject(_response, Formatting.Indented));

            Console.WriteLine("Hello");


            ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand(_myEndpoint);
            await Mediator.Send(_listenforcon);


            Console.WriteLine("Hi!");

            Console.ReadLine();







            //SendSelfCommand sendSelfCommand = new SendSelfCommand();
            //var _res = await Mediator.Send(sendSelfCommand);
            //Console.WriteLine("Response :{0}", _res.Count());


            //foreach (var item in _res)
            //{

            //    SaveNodesCommand saveNodesCommand = new SaveNodesCommand(item);
            //    var r = await Mediator.Send(saveNodesCommand);
            //    Console.WriteLine("Response :{0}", r);


            //}




            //TestCommand _cmd = new TestCommand();
            //var _response = await Mediator.Send(_cmd);
            //Console.WriteLine("Response: {0}", _response);


            //TestGetNodesQuery _query = new TestGetNodesQuery();
            //var _nodes = await Mediator.Send(_query);
            //Console.WriteLine("Nodes: {0}", _nodes.Count());




            Console.ReadLine();
        }
    }
}


//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using XLCCoin.Application;
//using XLCCoin.Application.Interfaces;
//using XLCCoin.Application.NodeCommands.Commands;
//using XLCCoin.Application.NodeCommands.Queries;
//using XLCCoin.Persistence;

//namespace XLCCoin.Node
//{
//    class Program
//    {
//        static IMediator Mediator
//        {
//            get
//            {
//                return ServiceRegistration.ServiceProvider.GetService<IMediator>();
//            }
//        }

//        static async Task Main(string[] args)
//        {
//            //TestCommand _cmd = new TestCommand { Name = "Devs" };
//            //var _response = await Mediator.Send(_cmd);
//            //Console.WriteLine("Response: {0}", _response);


//            //TestGetNodesQuery _query = new TestGetNodesQuery();
//            //var _nodes = await Mediator.Send(_query);
//            //Console.WriteLine("Nodes: {0}", _nodes.Count());


//            //Console.ReadLine();
//            try
//            {
//                //*for server*
//                //ListenForConnectionCommand _listenforcon = new ListenForConnectionCommand("192.168.2.200", 13000);
//                //await Mediator.Send(_listenforcon);



//                //*for client*
//                TryConnectNodeCommand _conToNode = new TryConnectNodeCommand("192.168.2.163");
//                await Mediator.Send(_conToNode);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Exception: {0}", e);
//            }
//        }
//    }
//}