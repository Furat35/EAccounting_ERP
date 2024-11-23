using EAccountingServer.Application.Models.Dtos.Companies;

namespace EAccountingServer.Application.Models.Dtos.CompanyUsers
{
    public class CompanyUserListDto
    {
        public Guid CompanyId { get; set; }
        public CompanyListDto Company { get; set; }
        public Guid UserId { get; set; }
    }
}
