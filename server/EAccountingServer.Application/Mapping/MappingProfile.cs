﻿using AutoMapper;
using EAccountingServer.Application.Features.Companies.CreateCompany;
using EAccountingServer.Application.Features.Companies.UpdateCompany;
using EAccountingServer.Application.Features.Users.CreateUser;
using EAccountingServer.Application.Features.Users.UpdateUser;
using EAccountingServer.Application.Models.Dtos.Users;
using EAccountingServer.Domain.Entities;

namespace EAccountingServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();
            CreateMap<AppUser, UserListDto>();

            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();
        }
    }
}
