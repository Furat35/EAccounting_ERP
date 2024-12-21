using AutoMapper;
using EAccountingServer.Application.Features.Customers.CreateCustomer;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(
        Guid Id,
        string Name,
        int TypeValue,
        string City,
        string Town,
        string FullAddress,
        string TaxDepartment,
        string TaxNumber): IRequest<Result<string>>;

    public class UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
            if (customer is null)
                return Result<string>.Failure("Cari bulunamadı.");

            mapper.Map(request, customer);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("customers");

            return "Cari başarıyla güncellendi.";
        }
    }
}
