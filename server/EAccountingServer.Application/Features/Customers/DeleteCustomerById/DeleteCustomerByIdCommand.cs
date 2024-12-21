using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.DeleteCustomerById
{
    public record DeleteCustomerByIdCommand(
        Guid Id) : IRequest<Result<string>>;
}
