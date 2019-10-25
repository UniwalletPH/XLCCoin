using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XLCCoin.Application.Logins.Commands;
using XLCCoin.Application.Register.Commands;

namespace XLCCoin.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            LoginCommand _login = new LoginCommand
            {
                Username = "vincent",
                Password = "pancitcanton"
            };

            var _return = mediator.Send(_login).Result;

            return View();
        }

        public IActionResult Register()
        {
            RegisterCommand _cmd = new RegisterCommand();

            var _result = mediator.Send(_cmd);


            return View();
        }
    }
}
