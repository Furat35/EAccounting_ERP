using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
