
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XLCCoin.Domain.Entities;
using XLCCoin.Web.Models;
using XLCCoin.Persistence;
using XLCCoin.Application.NodeCommands.Commands;

namespace XLCCoin.Web.Controllers
{
    public class AvailableNodesController : Controller
    {
        private readonly IMediator mediator;
        public AvailableNodesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        { 
            return View();

        }


        [HttpPost]
        public async Task<JsonResult> Index([FromBody]Node node)
        {

            NodeViewModel model = new NodeViewModel();
            model.IPAddress = node.IPAddress;
            model.ID = node.ID;
            model.Port = node.Port;
            model.Geolocation = node.Geolocation; 
            SaveNodesCommand saveNodeCommand = new SaveNodesCommand(model);
            var z = await mediator.Send(saveNodeCommand);



            FetchAllNodeCommand fetchAllNode = new FetchAllNodeCommand(node);
            var response = await mediator.Send(fetchAllNode);
            // string json = null;
            //try
            //{
            //json  = JsonConvert.SerializeObject(response);
            //    // An unhandled exception of type 'Newtonsoft.Json.JsonSerializationException' occurred in Newtonsoft.Json.dll
            //    // Additional information: Self referencing loop detected with type 'JSONPlayground.Author'.Path 'FavouriteAuthors'.
            //}
            //catch (JsonSerializationException e)
            //{
            //    // Console.WriteLine(e);
            //}

            return Json(response);
        }



    }
}