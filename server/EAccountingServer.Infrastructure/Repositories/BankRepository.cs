﻿using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class BankRepository(CompanyDbContext context)
        : Repository<Bank, CompanyDbContext>(context), IBankRepository
    {
    }
}