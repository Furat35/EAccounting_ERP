﻿using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.UpdateCustomer
{
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
