﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Payments.Infra.Data;

#nullable disable

namespace Payments.Infra.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    partial class PaymentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Payments.Domain.Entities.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContractNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal>("GrossIncome")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("Payments.Domain.Entities.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ContractNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Quota")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Payments", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}