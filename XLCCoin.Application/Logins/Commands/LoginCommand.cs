using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace XLCCoin.Application.Logins.Commands
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResult
    {
        public string Fullname { get; set; }
        public int ID { get; set; }

    }
}
