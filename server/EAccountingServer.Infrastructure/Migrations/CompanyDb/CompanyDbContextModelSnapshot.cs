﻿// <auto-generated />
using System;
using EAccountingServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    [DbContext(typeof(CompanyDbContext))]
    partial class CompanyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("WithdrawalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CashRegisterDetailOppositeId")
                        .IsUnique()
                        .HasFilter("[CashRegisterDetailOppositeId] IS NOT NULL");

                    b.HasIndex("CashRegisterId");

                    b.ToTable("CashRegisterDetails");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegisterDetail", b =>
                {
                    b.HasOne("EAccountingServer.Domain.Entities.CashRegisterDetail", "CashRegisterDetailOpposite")
                        .WithOne()
                        .HasForeignKey("EAccountingServer.Domain.Entities.CashRegisterDetail", "CashRegisterDetailOppositeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EAccountingServer.Domain.Entities.CashRegister", null)
                        .WithMany("Details")
                        .HasForeignKey("CashRegisterId");

                    b.Navigation("CashRegisterDetailOpposite");
                });

            modelBuilder.Entity("EAccountingServer.Domain.Entities.CashRegister", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
