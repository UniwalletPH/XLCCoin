
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XLCCoin.Application.NodeCommands.Commands;
using XLCCoin.Application.NodeCommands.Queries;
using XLCCoin.Web.Models;

namespace XLCCoin.Web.Controllers
{
    public class NodeController : Controller
    {
        private readonly IMediator mediator;
        public NodeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _result = await mediator.Send(new GetNodesQuery());

            return Json(_result);
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody]NodeVM node)
        {
            NodeVM _model = new NodeVM
            {
                ID = node.ID,
                Port = node.Port,
                Geolocation = node.Geolocation,
                IPAddress = node.IPAddress
            };

            SaveNodeCommand _saveNodeCommand = new SaveNodeCommand(_model);
            var _nodeID = await mediator.Send(_saveNodeCommand);

            FetchAllNodeCommand fetchAllNode = new FetchAllNodeCommand();
            var response = await mediator.Send(fetchAllNode);


            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> Neighbors()
        {
            FetchAllNodeCommand fetchAllNode = new FetchAllNodeCommand();
            var response = await mediator.Send(fetchAllNode);


            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> Clear()
        {
            var _result = await mediator.Send(new ClearConnectedNodesCommand());

            return Json(_result);
        }
    }
}