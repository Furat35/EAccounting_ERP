using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Enums;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.CreateInvoice
{
    public sealed class CreateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository,
        IProductDetailRepository productDetailRepository,
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateInvoiceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            #region Fatura ve Detay
            var invoice = mapper.Map<Invoice>(request);
            await invoiceRepository.AddAsync(invoice, cancellationToken);
            #endregion

            #region Customer
            var customer = await customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);
            if (customer is null)
                return Result<string>.Failure("Müşteri bulunamadı.");

            customer.DepositAmount += request.TypeValue == 2 ? invoice.Amount : 0;
            customer.WithdrawalAmount += request.TypeValue == 1 ? invoice.Amount : 0;
            customerRepository.Update(customer);

            CustomerDetail customerDetails = new()
            {
                CustomerId = customer.Id,
                Date = request.Date,
                DepositAmount = request.TypeValue == 2 ? invoice.Amount : 0,
                WithdrawalAmount = request.TypeValue == 1 ? invoice.Amount : 0,
                Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
                Type = request.TypeValue == 1 ? CustomerDetailTypeEnum.PurchaseInvoice : CustomerDetailTypeEnum.SellingInvoice,
                InvoiceId = invoice.Id
            };
            await customerDetailRepository.AddAsync(customerDetails, cancellationToken);
            #endregion

            #region Product
            foreach (var invoiceDetail in request.Details)
            {
                var product = await productRepository.GetByExpressionAsync(p => p.Id == invoiceDetail.ProductId, cancellationToken);

                product.Deposit += request.TypeValue == 1 ? invoiceDetail.Quantity : 0;
                product.Withdrawal += request.TypeValue == 2 ? invoiceDetail.Quantity : 0;
                productRepository.Update(product);

                ProductDetail productDetail = new()
                {
                    ProductId = product.Id,
                    Date = request.Date,
                    Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
                    Deposit = request.TypeValue == 1 ? invoiceDetail.Quantity : 0,
                    Withdrawal = request.TypeValue == 2 ? invoiceDetail.Quantity : 0,
                    InvoiceId = invoice.Id,
                    Price = invoiceDetail.Price
                };
                //await productRepository.AddAsync(product, cancellationToken);
                await productDetailRepository.AddAsync(productDetail, cancellationToken);
            }
            #endregion

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("invoices");
            cacheService.Remove("customers");
            cacheService.Remove("products");
            return $"{invoice.Type.Name} Fature kaydı başarıyla tamamlandı.";
        }
    }
}
