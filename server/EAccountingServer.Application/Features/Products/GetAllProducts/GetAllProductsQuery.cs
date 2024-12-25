using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.GetAllProducts
{
    public sealed record GetAllProductsQuery : IRequest<Result<List<Product>>>;
}
