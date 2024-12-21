using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.GetAllBanks
{
    public record GetAllBanksQuery() : IRequest<Result<List<Bank>>>;
}
