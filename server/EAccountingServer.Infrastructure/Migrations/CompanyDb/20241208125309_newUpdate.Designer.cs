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
    [Migration("20241208125309_newUpdate")]
    partial class newUpdate
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

            modelBuilder.Entity("EAccountingServer.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegister", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
