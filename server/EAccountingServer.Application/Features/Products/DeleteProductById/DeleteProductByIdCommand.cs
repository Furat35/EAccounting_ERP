using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.DeleteProductById
{
    public record DeleteProductByIdCommand(Guid Id) : IRequest<Result<string>>;
}
