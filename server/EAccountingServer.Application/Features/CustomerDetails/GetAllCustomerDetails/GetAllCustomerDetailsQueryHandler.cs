using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.CustomerDetails.GetAllCustomerDetails
{
    public sealed class GetAllCustomerDetailsQueryHandler(
        ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomerDetailsQuery, Result<Customer>>
    {
        public async Task<Result<Customer>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customerDetails = await customerRepository.Where(p => p.Id == request.CustomerId).Include(p => p.Details).FirstOrDefaultAsync();
            if (customerDetails is null)
            {
                return Result<Customer>.Failure("Cari bulunamadı.");
            }

            return customerDetails;
        }
    }
}
