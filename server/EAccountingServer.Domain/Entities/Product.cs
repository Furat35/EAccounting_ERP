using EAccountingServer.Domain.Abstractions;

namespace EAccountingServer.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdrawal { get; set; }
        public List<ProductDetail>? Details { get; set; }
    }
}
