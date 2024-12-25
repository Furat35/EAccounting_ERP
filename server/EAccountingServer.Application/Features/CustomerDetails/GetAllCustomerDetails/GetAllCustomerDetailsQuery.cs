using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CustomerDetails.GetAllCustomerDetails
{
    public record GetAllCustomerDetailsQuery(Guid CustomerId) : IRequest<Result<Customer>>;
}
