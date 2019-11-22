using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XLCCoin.Domain.Entities;
using XLCCoin.Web.Models;

namespace XLCCoin.Web.Controllers
{
    public class AvailableNodesController : Controller
    { 
        [HttpGet]
        public IActionResult Index()
        { 
            CenterViewModel model = new CenterViewModel();
            return View(model);
         
        }


        [HttpPost]
        public JsonResult Index([FromBody]NodesAvailable nodesAvailable)
        {
              
            CenterViewModel model = new CenterViewModel(); 
            model.ListofNodesAvailable = nodesAvailable.ListofNodesAvailable;
            return Json(model);
        }
        


    }
}
