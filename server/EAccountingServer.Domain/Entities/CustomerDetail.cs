using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Enums;

namespace EAccountingServer.Domain.Entities
{
    public class CustomerDetail : Entity
    {
        public Guid? CustomerId { get; set; }
        public DateOnly Date { get; set; }
        public CustomerDetailTypeEnum Type { get; set; } = CustomerDetailTypeEnum.CashRegister;
        public string Description { get; set; }
        public decimal DepositAmount { get; set; } // Giriş 
        public decimal WithdrawalAmount { get; set; } // Çıkış
        public Guid? BankDetailId { get; set; }
        public Guid? CashRegisterDetailId { get; set; }
        public Guid? InvoiceId { get; set; }
    }
}
