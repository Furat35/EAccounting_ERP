﻿// <auto-generated />
using System;
using EAccountingServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20241222151840_productNewaddrel")]
    partial class productNewaddrel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<string>("IBAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.BankDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankDetailOppositeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CashRegisterDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCreatedByThis")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("BankDetailOppositeId");

                    b.HasIndex("BankId");

                    b.HasIndex("CashRegisterDetailId");

                    b.ToTable("BankDetails");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegister", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("CashRegisters");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegisterDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CashRegisterDetailOppositeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CashRegisterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCreatedByThis")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("BankDetailId");

                    b.HasIndex("CashRegisterDetailOppositeId")
                        .IsUnique()
                        .HasFilter("[CashRegisterDetailOppositeId] IS NOT NULL");

                    b.HasIndex("CashRegisterId");

                    b.ToTable("CashRegisterDetails");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<string>("FullAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CustomerDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CashRegisterDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("money");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Withdrawal")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.ProductDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Withdrawal")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDetail");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.BankDetail", b =>
                {
                    b.HasOne("EAccountingServer.Domain.Entities.BankDetail", "BankDetailOpposite")
                        .WithMany()
                        .HasForeignKey("BankDetailOppositeId");

                    b.HasOne("EAccountingServer.Domain.Entities.Bank", null)
                        .WithMany("Details")
                        .HasForeignKey("BankId");

                    b.HasOne("EAccountingServer.Domain.Entities.CashRegisterDetail", "CashRegisterDetail")
                        .WithMany()
                        .HasForeignKey("CashRegisterDetailId");

                    b.Navigation("BankDetailOpposite");

                    b.Navigation("CashRegisterDetail");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegisterDetail", b =>
                {
                    b.HasOne("EAccountingServer.Domain.Entities.BankDetail", "BankDetail")
                        .WithMany()
                        .HasForeignKey("BankDetailId");

                    b.HasOne("EAccountingServer.Domain.Entities.CashRegisterDetail", "CashRegisterDetailOpposite")
                        .WithOne()
                        .HasForeignKey("EAccountingServer.Domain.Entities.CashRegisterDetail", "CashRegisterDetailOppositeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EAccountingServer.Domain.Entities.CashRegister", null)
                        .WithMany("Details")
                        .HasForeignKey("CashRegisterId");

                    b.Navigation("BankDetail");

                    b.Navigation("CashRegisterDetailOpposite");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CustomerDetail", b =>
                {
                    b.HasOne("EAccountingServer.Domain.Entities.Customer", null)
                        .WithMany("Details")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.ProductDetail", b =>
                {
                    b.HasOne("EAccountingServer.Domain.Entities.Product", null)
                        .WithMany("Details")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegister", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Product", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
