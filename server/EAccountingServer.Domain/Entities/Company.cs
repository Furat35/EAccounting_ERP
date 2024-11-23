using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.ValueObjects;

namespace EAccountingServer.Domain.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string TaxDepartment { get; set; }
        public string TaxNumber { get; set; }
        public Database Database { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);
    }
}
