namespace EAccountingServer.Domain.Dtos
{
    public class InvoiceDetailDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
