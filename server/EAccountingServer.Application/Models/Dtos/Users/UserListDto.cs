using EAccountingServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Application.Models.Dtos.Users
{
    public class UserListDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string FullName { get; set; }
        public ICollection<CompanyUser> CompanyUsers { get; set; }
        public bool IsDeleted { get; set; }
    }
}
