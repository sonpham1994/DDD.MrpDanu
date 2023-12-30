﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Writes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231216022720_RemoveCodeUniquerInMaterial")]
    partial class RemoveCodeUniquerInMaterial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("bomrevisionseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("productseq")
                .IncrementsBy(10);

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte>("MaterialTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte>("RegionalMarketId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("MaterialTypeId");

                    b.HasIndex("RegionalMarketId");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.MaterialCostManagement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinQuantity")
                        .HasColumnType("int")
                        .HasColumnName("MinQuantity");

                    b.Property<Guid>("TransactionalPartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("TransactionalPartnerId");

                    b.ToTable("MaterialCostManagement", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.MaterialType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("MaterialType", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.RegionalMarket", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("RegionalMarket", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactPersonInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ContactPersonInformation", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.Country", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.LocationType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("LocationType", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("CurrencyTypeId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("LocationTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Name");

                    b.Property<byte>("TransactionalPartnerTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Website")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Website");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyTypeId");

                    b.HasIndex("LocationTypeId");

                    b.HasIndex("TransactionalPartnerTypeId");

                    b.ToTable("TransactionalPartner", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartnerType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TransactionalPartnerType", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoM", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("char(10)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("BoM", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevision", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<short>("Id"), "bomrevisionseq");

                    b.Property<long?>("BoMId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("char(14)");

                    b.Property<string>("Confirmation")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("BoMId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("BoMRevision", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevisionMaterial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("BoMRevisionId")
                        .HasColumnType("smallint");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TransactionalPartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Unit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Unit");

                    b.HasKey("Id");

                    b.HasIndex("BoMRevisionId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("TransactionalPartnerId");

                    b.ToTable("BoMRevisionMaterial", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "productseq");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Domain.SharedKernel.DomainClasses.CurrencyType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CurrencyType", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.Material", b =>
                {
                    b.HasOne("Domain.SupplyChainManagement.MaterialAggregate.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.MaterialAggregate.RegionalMarket", "RegionalMarket")
                        .WithMany()
                        .HasForeignKey("RegionalMarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SupplyChainManagement.MaterialAggregate.MaterialAttributes", "Attributes", b1 =>
                        {
                            b1.Property<Guid>("MaterialId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ColorCode")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("ColorCode");

                            b1.Property<string>("Unit")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Unit");

                            b1.Property<string>("Varian")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Varian");

                            b1.Property<string>("Weight")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Weight");

                            b1.Property<string>("Width")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Width");

                            b1.HasKey("MaterialId");

                            b1.ToTable("Material");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.Navigation("Attributes")
                        .IsRequired();

                    b.Navigation("MaterialType");

                    b.Navigation("RegionalMarket");
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.MaterialCostManagement", b =>
                {
                    b.HasOne("Domain.SupplyChainManagement.MaterialAggregate.Material", null)
                        .WithMany("MaterialCostManagements")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", "TransactionalPartner")
                        .WithMany()
                        .HasForeignKey("TransactionalPartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SharedKernel.DomainClasses.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("MaterialCostManagementId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("CurrencyTypeId")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("tinyint")
                                .HasColumnName("CurrencyTypeId");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Price");

                            b1.HasKey("MaterialCostManagementId");

                            b1.HasIndex("CurrencyTypeId");

                            b1.ToTable("MaterialCostManagement");

                            b1.HasOne("Domain.SharedKernel.DomainClasses.CurrencyType", "CurrencyType")
                                .WithMany()
                                .HasForeignKey("CurrencyTypeId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MaterialCostManagementId");

                            b1.Navigation("CurrencyType");
                        });

                    b.OwnsOne("Domain.SharedKernel.DomainClasses.Money", "Surcharge", b1 =>
                        {
                            b1.Property<Guid>("MaterialCostManagementId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("CurrencyTypeId")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("tinyint")
                                .HasColumnName("CurrencyTypeId");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Surcharge");

                            b1.HasKey("MaterialCostManagementId");

                            b1.HasIndex("CurrencyTypeId");

                            b1.ToTable("MaterialCostManagement");

                            b1.HasOne("Domain.SharedKernel.DomainClasses.CurrencyType", "CurrencyType")
                                .WithMany()
                                .HasForeignKey("CurrencyTypeId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MaterialCostManagementId");

                            b1.Navigation("CurrencyType");
                        });

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Surcharge")
                        .IsRequired();

                    b.Navigation("TransactionalPartner");
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactPersonInformation", b =>
                {
                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", null)
                        .WithOne("ContactPersonInformation")
                        .HasForeignKey("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactPersonInformation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactInformation", "ContactInformation", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonInformationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Email");

                            b1.Property<string>("TelNo")
                                .HasColumnType("varchar(50)")
                                .HasColumnName("TelNo");

                            b1.HasKey("ContactPersonInformationId");

                            b1.ToTable("ContactPersonInformation");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonInformationId");
                        });

                    b.Navigation("ContactInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", b =>
                {
                    b.HasOne("Domain.SharedKernel.DomainClasses.CurrencyType", "CurrencyType")
                        .WithMany()
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.LocationType", "LocationType")
                        .WithMany()
                        .HasForeignKey("LocationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartnerType", "TransactionalPartnerType")
                        .WithMany()
                        .HasForeignKey("TransactionalPartnerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("TransactionalPartnerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Address_City");

                            b1.Property<byte>("CountryId")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("tinyint")
                                .HasColumnName("CountryId");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Address_District");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Address_Street");

                            b1.Property<string>("Ward")
                                .IsRequired()
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Address_Ward");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Address_ZipCode");

                            b1.HasKey("TransactionalPartnerId");

                            b1.HasIndex("CountryId");

                            b1.ToTable("TransactionalPartner");

                            b1.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.Country", "Country")
                                .WithMany()
                                .HasForeignKey("CountryId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TransactionalPartnerId");

                            b1.Navigation("Country");
                        });

                    b.OwnsOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TaxNo", "TaxNo", b1 =>
                        {
                            b1.Property<Guid>("TransactionalPartnerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("CountryId")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("tinyint")
                                .HasColumnName("CountryId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("TaxNo");

                            b1.HasKey("TransactionalPartnerId");

                            b1.HasIndex("CountryId");

                            b1.ToTable("TransactionalPartner");

                            b1.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.Country", "Country")
                                .WithMany()
                                .HasForeignKey("CountryId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TransactionalPartnerId");

                            b1.Navigation("Country");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CurrencyType");

                    b.Navigation("LocationType");

                    b.Navigation("TaxNo")
                        .IsRequired();

                    b.Navigation("TransactionalPartnerType");
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoM", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.Product", null)
                        .WithOne("BoM")
                        .HasForeignKey("Domain.ProductionPlanning.BoM", "Id");
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevision", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.BoM", null)
                        .WithMany("BoMRevisions")
                        .HasForeignKey("BoMId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevisionMaterial", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.BoMRevision", null)
                        .WithMany("BoMRevisionMaterials")
                        .HasForeignKey("BoMRevisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.MaterialAggregate.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", "TransactionalPartner")
                        .WithMany()
                        .HasForeignKey("TransactionalPartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SharedKernel.DomainClasses.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("BoMRevisionMaterialId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("CurrencyTypeId")
                                .HasColumnType("tinyint")
                                .HasColumnName("CurrencyTypeId");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Price");

                            b1.HasKey("BoMRevisionMaterialId");

                            b1.HasIndex("CurrencyTypeId");

                            b1.ToTable("BoMRevisionMaterial");

                            b1.WithOwner()
                                .HasForeignKey("BoMRevisionMaterialId");

                            b1.HasOne("Domain.SharedKernel.DomainClasses.CurrencyType", "CurrencyType")
                                .WithMany()
                                .HasForeignKey("CurrencyTypeId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.Navigation("CurrencyType");
                        });

                    b.Navigation("Material");

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("TransactionalPartner");
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.Material", b =>
                {
                    b.Navigation("MaterialCostManagements");
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", b =>
                {
                    b.Navigation("ContactPersonInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoM", b =>
                {
                    b.Navigation("BoMRevisions");
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevision", b =>
                {
                    b.Navigation("BoMRevisionMaterials");
                });

            modelBuilder.Entity("Domain.ProductionPlanning.Product", b =>
                {
                    b.Navigation("BoM");
                });
#pragma warning restore 612, 618
        }
    }
}
