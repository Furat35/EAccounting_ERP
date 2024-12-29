using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.DeleteInvoiceById
{
    public sealed class DeleteInvoiceByIdCommandHandler(
        IUnitOfWorkCompany unitOfWorkCompany,
        IProductRepository productRepository,
        IProductDetailRepository productDetailRepository,
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository,
        ICacheService cacheService) : IRequestHandler<DeleteInvoiceByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
        {
            var invoice = await invoiceRepository.Where(p => p.Id == request.Id)
                .Include(p => p.Details)
                .FirstOrDefaultAsync(cancellationToken);

            if (invoice is null)
                return Result<string>.Failure("Fatura bulunamadı.");

            var customerDetail = await customerDetailRepository.Where(p => p.InvoiceId == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (customerDetail is not null)
                customerDetailRepository.Delete(customerDetail);

            var customer = await customerRepository.Where(p => p.Id == invoice.CustomerId).FirstOrDefaultAsync(cancellationToken);
            if (customer is not null)
            {
                customer.DepositAmount -= invoice.Type.Value == 1 ? invoice.Amount : 0;
                customer.WithdrawalAmount -= invoice.Type.Value == 2 ? invoice.Amount : 0;

                customerRepository.Update(customer);
            }

            var productDetails = await productDetailRepository.Where(p => p.InvoiceId == invoice.Id).ToListAsync(cancellationToken);

            foreach (var detail in productDetails)
            {
                var product = await productRepository.GetByExpressionWithTrackingAsync(p => p.Id == detail.ProductId, cancellationToken);
                if (product is not null)
                {
                    product.Deposit -= detail.Deposit;
                    product.Withdrawal -= detail.Withdrawal;

                    productRepository.Update(product);
                }
            }

            invoiceRepository.Delete(invoice);
            productDetailRepository.DeleteRange(productDetails);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            cacheService.Remove("invoices");
            cacheService.Remove("customers");
            cacheService.Remove("products");
            return $"{invoice.Type.Name} kaydı başarıyla silindi.";
        }
    }
}
