using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Enums;

namespace EAccountingServer.Domain.Entities
{
    public sealed class Invoice : Entity
    {
        public DateOnly Date { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public decimal Amount { get; set; }
        public InvoiceTypeEnum Type { get; set; } = InvoiceTypeEnum.Purchase;
        public List<InvoiceDetail> Details { get; set; }
    }
}
