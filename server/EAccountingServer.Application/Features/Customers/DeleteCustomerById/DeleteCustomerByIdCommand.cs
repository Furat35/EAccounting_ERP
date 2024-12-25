using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.DeleteCustomerById
{
    public record DeleteCustomerByIdCommand(
        Guid Id) : IRequest<Result<string>>;
}
