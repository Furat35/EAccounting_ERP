using EAccountingServer.Application.Features.Auth.Login;
using EAccountingServer.Application.Models.Dtos.Companies;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TS.Result;

namespace EAccountingServer.Application.Features.Auth.ChangeCompany
{
    public sealed class ChangeCompanyCommandHandler(
        ICompanyUserRepository companyUserRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor, IJwtProvider jwtProvider) 
        : IRequestHandler<ChangeCompanyCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(ChangeCompanyCommand request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
                return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok!");

            var userId = httpContextAccessor.HttpContext.User.FindFirstValue("Id");

            if (string.IsNullOrEmpty(userId))
                return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok!");

            var user = await userManager.FindByIdAsync(userId);

            var companyUsers = await companyUserRepository
                .Where(cu => cu.UserId == user.Id)
                .Include(cu => cu.Company)
                .ToListAsync(cancellationToken);

            var companies = companyUsers.Select(cu => new CompanyListDto
            {
                Id = cu.CompanyId,
                Name = cu.Company.Name,
                TaxDepartment = cu.Company.TaxDepartment,
                TaxNumber = cu.Company.TaxNumber,
                FullAddress = cu.Company.FullAddress
            }).ToList();

            var response = await jwtProvider.CreateToken(user, request.CompanyId, companies);
            return response;
        }
    }
}
