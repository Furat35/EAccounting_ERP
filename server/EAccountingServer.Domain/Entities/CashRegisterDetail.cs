using EAccountingServer.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAccountingServer.Domain.Entities
{
    public sealed class CashRegisterDetail : Entity
    {
        public Guid? CashRegisterId { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public Guid? CashRegisterDetailOppositeId { get; set; }
        public CashRegisterDetail? CashRegisterDetailOpposite { get; set; }
        public Guid? BankDetailId { get; set; }
        [ForeignKey(nameof(BankDetailId))]
        public BankDetail? BankDetail { get; set; }
        public bool IsCreatedByThis { get; set; }
        public Guid? CustomerDetailId { get; set; }
    }
}
