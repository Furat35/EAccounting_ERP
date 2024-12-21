using EAccountingServer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.Customers.GetAllCustomers
{
    public sealed record GetAllCustomersQuery : IRequest<Result<List<Customer>>>;
}
