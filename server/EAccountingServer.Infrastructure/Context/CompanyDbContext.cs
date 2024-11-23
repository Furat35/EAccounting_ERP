using EAccountingServer.Domain.Abstractions;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Enums;
using EAccountingServer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            modelBuilder.Entity<CashRegister>().Property(c => c.DepositAmount).HasColumnType("money");
            modelBuilder.Entity<CashRegister>().Property(c => c.WithdrawalAmount).HasColumnType("money");
            modelBuilder.Entity<CashRegister>()
                .Property(c => c.CurrencyType)
                .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
            modelBuilder.Entity<CashRegister>().HasQueryFilter(filter => !filter.IsDeleted);
            modelBuilder.Entity<CashRegister>().HasMany(_ => _.Details).WithOne().HasForeignKey(_ => _.CashRegisterId);

            modelBuilder.Entity<CashRegisterDetail>().Property(c => c.DepositAmount).HasColumnType("money");
            modelBuilder.Entity<CashRegisterDetail>().Property(c => c.WithdrawalAmount).HasColumnType("money");
            modelBuilder.Entity<CashRegisterDetail>().HasQueryFilter(filter => !filter.IsDeleted);
            modelBuilder.Entity<CashRegisterDetail>()
                .HasOne(c => c.CashRegisterDetailOpposite)
                .WithOne()
                .HasForeignKey<CashRegisterDetail>(c => c.CashRegisterDetailOppositeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<CashRegisterDetail> CashRegisterDetails { get; set; }

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
