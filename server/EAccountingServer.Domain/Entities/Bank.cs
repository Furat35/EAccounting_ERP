using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Enums;

namespace EAccountingServer.Domain.Entities
{
    public class Bank : Entity
    {
        public string Name { get; set; }
        public string IBAN { get; set; }
        public CurrencyTypeEnum CurrencyType { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public List<BankDetail> Details { get; set; }
    }
}
