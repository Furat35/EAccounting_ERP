using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Infrastructure.Context
{
    public class CompanyDbContext : DbContext, IUnitOfWorkCompany
    {
        private string connectionString;

        public CompanyDbContext(Company company)
        {
            CreateConnectionStringWithCompany(company);
        }

        public CompanyDbContext(IHttpContextAccessor httpContext, ApplicationDbContext context)
        {
            CreateConnectionString(httpContext, context);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        private void CreateConnectionString(IHttpContextAccessor httpContext, ApplicationDbContext context)
        {
            if (httpContext.HttpContext is null) return;

            var companyId = httpContext.HttpContext.User.FindFirstValue("CompanyId");
            if (string.IsNullOrEmpty(companyId)) return;

            var company = context.Companies.Find(Guid.Parse(companyId));
            if (company is null) return;
            CreateConnectionStringWithCompany(company);
        }

        private void CreateConnectionStringWithCompany(Company company)
        {
            if (string.IsNullOrEmpty(company.Database.UserId))
            {
                connectionString = $"Data Source={company.Database.Server};" +
                    $"Initial Catalog={company.Database.DatabaseName};" +
                    $"Integrated Security=True;" +
                    $"Connect Timeout=30;" +
                    $"Encrypt=True;" +
                    $"Trust Server Certificate=True;" +
                    $"Application Intent=ReadWrite;" +
                    $"Multi Subnet Failover=False";
            }
            else
            {
                connectionString = $"Data Source={company.Database.Server};" +
                    $"Initial Catalog={company.Database.DatabaseName};" +
                    $"Integrated Security=False;" +
                    $"User Id={company.Database.UserId};" +
                    $"Password={company.Database.Password};" +
                    $"Connect Timeout=30;" +
                    $"Encrypt=True;" +
                    $"Trust Server Certificate=True;" +
                    $"Application Intent=ReadWrite;" +
                    $"Multi Subnet Failover=False";
            }
        }
    }
}
