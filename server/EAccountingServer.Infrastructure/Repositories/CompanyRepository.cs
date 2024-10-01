using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Infrastructure.Repositories
{
    public sealed class CompanyRepository(ApplicationDbContext context) : Repository<Company, ApplicationDbContext>(context), ICompanyRepository
    {
    }
}
