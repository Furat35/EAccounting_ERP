using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Infrastructure.Services
{
    public sealed class CompanyService : ICompanyService
    {
        public void MigrateAll(List<Company> companies)
        {
            foreach (Company company in companies)
            {
                var context = new CompanyDbContext(company);
                context.Database.Migrate(); 
            }
        }
    }
}
