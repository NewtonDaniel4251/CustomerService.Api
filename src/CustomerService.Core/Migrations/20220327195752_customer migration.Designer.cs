﻿// <auto-generated />
using System;
using CustomerService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerService.Core.Migrations
{
    [DbContext(typeof(CustomerRegistrationContext))]
    [Migration("20220327195752_customer migration")]
    partial class customermigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CustomerService.Core.Models.CustomerDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsOTPValidated")
                        .HasColumnType("bit");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OtpGeneratedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("CustomerService.Core.Models.LGA", b =>
                {
                    b.Property<int>("LGAId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("LGAName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("LGAId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LGAName")
                        .IsUnique();

                    b.HasIndex("StateId");

                    b.ToTable("LGAs");
                });

            modelBuilder.Entity("CustomerService.Core.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StateId");

                    b.HasIndex("StateName")
                        .IsUnique();

                    b.ToTable("States");
                });

            modelBuilder.Entity("CustomerService.Core.Models.CustomerDetail", b =>
                {
                    b.HasOne("CustomerService.Core.Models.State", "TBL_STATE")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TBL_STATE");
                });

            modelBuilder.Entity("CustomerService.Core.Models.LGA", b =>
                {
                    b.HasOne("CustomerService.Core.Models.CustomerDetail", "TBL_Customer_Detail")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomerService.Core.Models.State", "TBL_State")
                        .WithMany("TBL_Lgas")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TBL_Customer_Detail");

                    b.Navigation("TBL_State");
                });

            modelBuilder.Entity("CustomerService.Core.Models.State", b =>
                {
                    b.Navigation("TBL_Lgas");
                });
#pragma warning restore 612, 618
        }
    }
}
