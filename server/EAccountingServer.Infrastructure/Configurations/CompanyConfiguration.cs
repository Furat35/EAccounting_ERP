using EAccountingServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAccountingServer.Infrastructure.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.TaxNumber).HasColumnType("varchar(11)");
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.OwnsOne(e => e.Database, builder =>
            {
                builder.Property(p => p.Server).HasColumnName("Server");
                builder.Property(p => p.DatabaseName).HasColumnName("DatabaseName");
                builder.Property(p => p.UserId).HasColumnName("UserId");
                builder.Property(p => p.Password).HasColumnName("Password");
            });
        }
    }
}
