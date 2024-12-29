using AutoMapper;
using EAccountingServer.Application.Features.Banks.CreateBank;
using EAccountingServer.Application.Features.Banks.UpdateBank;
using EAccountingServer.Application.Features.CashRegisters.CreateCashRegister;
using EAccountingServer.Application.Features.CashRegisters.UpdateCashRegister;
using EAccountingServer.Application.Features.Companies.CreateCompany;
using EAccountingServer.Application.Features.Companies.UpdateCompany;
using EAccountingServer.Application.Features.Invoices.CreateInvoice;
using EAccountingServer.Application.Features.Products.CreateProduct;
using EAccountingServer.Application.Features.Products.UpdateProduct;
using EAccountingServer.Application.Features.Users.CreateUser;
using EAccountingServer.Application.Features.Users.UpdateUser;
using EAccountingServer.Application.Models.Dtos.Users;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Enums;

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

            CreateMap<CreateCashRegisterCommand, CashRegister>()
                .ForMember(dest => dest.CurrencyType,
                       opt => opt.MapFrom(src => CurrencyTypeEnum.FromValue(src.CurrencyType)));
            CreateMap<UpdateCashRegisterCommand, CashRegister>()
                .ForMember(dest => dest.CurrencyType,
                       opt => opt.MapFrom(src => CurrencyTypeEnum.FromValue(src.CurrencyType)));

            CreateMap<CreateBankCommand, Bank>()
                    .ForMember(dest => dest.CurrencyType,
                           opt => opt.MapFrom(src => CurrencyTypeEnum.FromValue(src.CurrencyType)));
            CreateMap<UpdateBankCommand, Bank>()
                .ForMember(dest => dest.CurrencyType,
                       opt => opt.MapFrom(src => CurrencyTypeEnum.FromValue(src.CurrencyType)));

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<CreateInvoiceCommand, Invoice>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => InvoiceTypeEnum.FromValue(src.TypeValue)))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details.Select(s => new InvoiceDetail
                {
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    Price = s.Price
                }).ToList()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Details.Sum(s => s.Quantity * s.Price)));
        }
    }
}
