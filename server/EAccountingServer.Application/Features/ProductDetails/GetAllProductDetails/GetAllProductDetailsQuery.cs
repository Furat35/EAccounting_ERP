using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.ProductDetails.GetAllProductDetails
{
    public sealed record GetAllProductDetailsQuery(Guid ProductId) : IRequest<Result<Product>>;
}
