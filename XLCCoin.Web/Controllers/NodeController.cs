
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
using XLCCoin.Application.NodeCommands.Queries;

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
        public IActionResult Index()
        { 
            return Json(true);
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
    }
}