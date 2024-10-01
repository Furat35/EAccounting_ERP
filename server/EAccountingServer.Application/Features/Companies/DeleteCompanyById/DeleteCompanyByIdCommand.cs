using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.DeleteCompanyById
{
    public sealed record DeleteCompanyByIdCommand(Guid Id) : IRequest<Result<string>>;
}
