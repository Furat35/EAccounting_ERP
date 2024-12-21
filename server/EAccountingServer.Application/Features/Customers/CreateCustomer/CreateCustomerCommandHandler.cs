using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<Customer>(request);
            await customerRepository.AddAsync(customer, cancellationToken); 
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("customers");

            return "Cari kaydı başarıyla tamamlandı.";
        }
    }
}
