using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.GetAllCustomers
{
    public sealed class GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository,
        ICacheService cacheService) : IRequestHandler<GetAllCustomersQuery, Result<List<Customer>>>
    {
        public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = cacheService.Get<List<Customer>>("customers");
            if (customers is null)
            {
                customers = await customerRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
                cacheService.Set("customers", customers);
            }

            return customers;
        }
    }
}
