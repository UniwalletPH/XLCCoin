using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XLCCoin.Application.Common.Base;
using XLCCoin.Application.Interfaces;
using XLCCoin.Application.NodeCommands.Queries;

namespace XLCCoin.Application.NodeCommands.Commands
{
    public class FindTipCommand : IRequest<List<TranSiteVM>>
    {
        public FindTipCommand()
        {

        }

        public class FindTipCommandHandler : BaseRequestHandler, IRequestHandler<FindTipCommand, List<TranSiteVM>>
        {
            public FindTipCommandHandler(IXLCDbContext dbContext) : base(dbContext)
            {

            }

            public async Task<List<TranSiteVM>> Handle(FindTipCommand request, CancellationToken cancellationToken)
            {
                List<TranSiteVM> _tranSiteResponse = new List<TranSiteVM>
                {
                    new TranSiteVM{ TransactionDescription = "Buy Load"  }
                };

                return _tranSiteResponse;
            }
        }
    }
}