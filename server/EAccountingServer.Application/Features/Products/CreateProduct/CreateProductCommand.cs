using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.CreateProduct
{
    public record CreateProductCommand(string Name) : IRequest<Result<string>>;
}
