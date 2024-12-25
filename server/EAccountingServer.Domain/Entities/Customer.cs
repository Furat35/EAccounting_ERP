using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Enums;

namespace EAccountingServer.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public CustomerTypeEnum Type { get; set; } = CustomerTypeEnum.Alicilar;
        public string City { get; set; }
        public string Town { get; set; }
        public string FullAddress { get; set; }
        public string TaxDepartment { get; set; }
        public string TaxNumber { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public List<CustomerDetail> Details { get; set; }
    }
}
