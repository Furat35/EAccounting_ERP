using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.GetAllCompanies
{
    // todo: şirket isminin ilk harfine göre sıralama yapılabilir
    public class GetAllCompaniesQuery() : IRequest<Result<List<Company>>>;
}
