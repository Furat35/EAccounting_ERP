using EAccountingServer.Application.Models.Dtos.Companies;
using EAccountingServer.Application.Models.Dtos.Users;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJwtProvider jwtProvider,
        ICompanyUserRepository companyUserRepository) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(p =>
                p.UserName == request.EmailOrUserName ||
                p.Email == request.EmailOrUserName,
                cancellationToken);

            if (user is null)
                return (400, "Geçersiz kullanıcı veya şifre!");

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (signInResult.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
                return timeSpan is not null
                     ? (500, $"Şifrenizi 3 defa yanlış girdiğiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir")
                     : (500, "Kullanıcınız 3 kez yanlış şifre girdiği için 5 dakika süreyle bloke edilmiştir");
            }

            if (signInResult.IsNotAllowed)
                return (500, "Mail adresiniz onaylı değil!");

            if (!signInResult.Succeeded)
                return (400, "Geçersiz kullanıcı veya şifre!");

            var companyUsers = await companyUserRepository.Where(cu => cu.UserId == user.Id).Include(cu => cu.Company).ToListAsync(cancellationToken);
            var companies = new List<CompanyListDto>();
            Guid? companyId = null;
            if (companyUsers.Count > 0)
            {
                companyId = companyUsers.First().CompanyId;
                companies = companyUsers.Select(cu => new CompanyListDto
                {
                    Id = cu.CompanyId,
                    Name = cu.Company.Name,
                    TaxDepartment = cu.Company.TaxDepartment,
                    TaxNumber = cu.Company.TaxNumber,
                    FullAddress = cu.Company.FullAddress
                }).ToList();
            }
            

            var loginResponse = await jwtProvider.CreateToken(user, companyId, companies);
            //loginResponse.User = new UserListDto
            //{
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    FullName = user.FullName,
            //    Id = user.Id,
            //    PhoneNumber = user.PhoneNumber,
            //    RefreshToken = loginResponse.RefreshToken,
            //    UserName = user.UserName,
            //}

            return loginResponse;
        }
    }
}
