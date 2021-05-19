using MediatR;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Customer
{
    public class AddCustomerCommandHandler : ICommandHandler<CustomerRequest, string>
    {
        private readonly ICustomerService customerService;
        public AddCustomerCommandHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<string> Handle(CustomerRequest request, CancellationToken cancellationToken)
        {
            return await this.customerService.AddCustomerAsync(request);
        }
    }
}
