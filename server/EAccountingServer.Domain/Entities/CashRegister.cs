using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Enums;

namespace EAccountingServer.Domain.Entities
{
    public class CashRegister : Entity
    {
        public string Name { get; set; }
        public CurrencyTypeEnum CurrencyType { get; set; } = CurrencyTypeEnum.TL;
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public List<CashRegisterDetail>? Details { get; set; }
    }
}
