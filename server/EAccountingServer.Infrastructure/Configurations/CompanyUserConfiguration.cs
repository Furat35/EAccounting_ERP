using EAccountingServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAccountingServer.Infrastructure.Configurations
{
    public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            builder.HasKey(c => new { c.UserId, c.CompanyId });
            builder.HasQueryFilter(c => !c.Company.IsDeleted);
        }
    }
}
