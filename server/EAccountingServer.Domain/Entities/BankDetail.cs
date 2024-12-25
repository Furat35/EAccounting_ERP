using EAccountingServer.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAccountingServer.Domain.Entities
{
    public class BankDetail : Entity
    {
        public Guid? BankId { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public Guid? BankDetailOppositeId { get; set; }
        public BankDetail? BankDetailOpposite { get; set; }
        public Guid? CashRegisterDetailId { get; set; }
        [ForeignKey(nameof(CashRegisterDetailId))]
        public CashRegisterDetail? CashRegisterDetail { get; set; }
        public bool IsCreatedByThis { get; set; }
        public Guid? CustomerDetailId { get; set; }
    }
}
