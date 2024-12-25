using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.GetAllCustomers
{
    public sealed record GetAllCustomersQuery : IRequest<Result<List<Customer>>>;
}
