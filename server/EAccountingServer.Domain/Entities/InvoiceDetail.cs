using EAccountingServer.Domain.Abstractions;

namespace EAccountingServer.Domain.Entities
{
    public sealed class InvoiceDetail : Entity
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
