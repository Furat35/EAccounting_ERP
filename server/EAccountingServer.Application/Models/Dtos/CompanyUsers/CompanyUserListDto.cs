using EAccountingServer.Application.Models.Dtos.Companies;
using EAccountingServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAccountingServer.Application.Models.Dtos.CompanyUsers
{
    public class CompanyUserListDto
    {
        public Guid CompanyId { get; set; }
        public CompanyListDto Company { get; set; }
        public Guid UserId { get; set; }
    }
}
