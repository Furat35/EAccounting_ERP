using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.CreateCustomer
{
    public record CreateCustomerCommand(
        string Name,
        int TypeValue,
        string City,
        string Town,
        string FullAddress,
        string TaxDepartment,
        string TaxNumber) : IRequest<Result<string>>;
}
