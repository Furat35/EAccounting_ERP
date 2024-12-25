using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid Id,
        string Name) : IRequest<Result<string>>;
}
