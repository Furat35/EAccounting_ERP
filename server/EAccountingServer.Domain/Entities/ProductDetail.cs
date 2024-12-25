using EAccountingServer.Domain.Abstractions;

namespace EAccountingServer.Domain.Entities
{
    public class ProductDetail : Entity
    {
        public Guid ProductId { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdrawal { get; set; }
    }
}
