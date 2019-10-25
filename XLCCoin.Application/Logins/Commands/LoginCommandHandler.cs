using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XLCCoin.Application.Logins.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        public Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Username == "vincent" && request.Password == "panc")
            {
                return null;
            }
            else
            {
                return Task.Run(() =>
                {
                    return new LoginCommandResult
                    {
                         Fullname = "VIncent Dagpin",
                         ID  = 7
                    };
                });
            }
        }
    }
}
