using System.Text.Json.Serialization;

namespace EAccountingServer.Domain.Entities
{
    public class CompanyUser
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
    }
}
