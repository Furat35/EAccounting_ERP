using EAccountingServer.Domain.Entities;

namespace EAccountingServer.Application.Services
{
    public interface ICompanyService
    {
        void MigrateAll(List<Company> companies);
    }
}
