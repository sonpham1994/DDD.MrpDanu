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
    [Migration("20231220081943_RefactorToStronglyTypedId")]
    partial class RefactorToStronglyTypedId
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

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("MaterialTypeId");

                    b.HasIndex("RegionalMarketId");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.MaterialSupplierCost", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinQuantity")
                        .HasColumnType("int")
                        .HasColumnName("MinQuantity");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.ToTable("MaterialSupplierCost", (string)null);
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

                    b.Property<Guid>("TransactionalPartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionalPartnerId")
                        .IsUnique();

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
                    b.Property<uint>("Id")
                        .HasColumnType("bigint");

                    b.Property<uint?>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("BoM", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevision", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<ushort>("Id"), "bomrevisionseq");

                    b.Property<uint>("BoMId")
                        .HasColumnType("bigint");

                    b.Property<string>("Confirmation")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("BoMId");

                    b.ToTable("BoMRevision", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevisionMaterial", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<ushort>("BoMRevisionId")
                        .HasColumnType("smallint");

                    b.Property<decimal>("Unit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Unit");

                    b.HasKey("Id");

                    b.HasIndex("BoMRevisionId");

                    b.ToTable("BoMRevisionMaterial", (string)null);
                });

            modelBuilder.Entity("Domain.ProductionPlanning.Product", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<uint>("Id"), "productseq");

                    b.Property<uint?>("BoMId")
                        .HasColumnType("bigint");

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

                    b.HasIndex("BoMId")
                        .IsUnique()
                        .HasFilter("[BoMId] IS NOT NULL");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Domain.SharedKernel.Enumerations.CurrencyType", b =>
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

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.MaterialSupplierCost", b =>
                {
                    b.HasOne("Domain.SupplyChainManagement.MaterialAggregate.Material", null)
                        .WithMany("MaterialSupplierCosts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.SharedKernel.ValueObjects.MaterialCost", "MaterialCost", b1 =>
                        {
                            b1.Property<Guid>("MaterialSupplierCostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("_transactionalPartnerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("SupplierId");

                            b1.HasKey("MaterialSupplierCostId");

                            b1.HasIndex("_transactionalPartnerId");

                            b1.ToTable("MaterialSupplierCost");

                            b1.WithOwner()
                                .HasForeignKey("MaterialSupplierCostId");

                            b1.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", null)
                                .WithMany()
                                .HasForeignKey("_transactionalPartnerId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsOne("Domain.SharedKernel.ValueObjects.Money", "Price", b2 =>
                                {
                                    b2.Property<Guid>("MaterialCostMaterialSupplierCostId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<byte>("CurrencyTypeId")
                                        .ValueGeneratedOnUpdateSometimes()
                                        .HasColumnType("tinyint")
                                        .HasColumnName("CurrencyTypeId");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("Price");

                                    b2.HasKey("MaterialCostMaterialSupplierCostId");

                                    b2.HasIndex("CurrencyTypeId");

                                    b2.ToTable("MaterialSupplierCost");

                                    b2.HasOne("Domain.SharedKernel.Enumerations.CurrencyType", "CurrencyType")
                                        .WithMany()
                                        .HasForeignKey("CurrencyTypeId")
                                        .OnDelete(DeleteBehavior.NoAction)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("MaterialCostMaterialSupplierCostId");

                                    b2.Navigation("CurrencyType");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.OwnsOne("Domain.SharedKernel.ValueObjects.Money", "Surcharge", b1 =>
                        {
                            b1.Property<Guid>("MaterialSupplierCostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte>("CurrencyTypeId")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("tinyint")
                                .HasColumnName("CurrencyTypeId");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Surcharge");

                            b1.HasKey("MaterialSupplierCostId");

                            b1.HasIndex("CurrencyTypeId");

                            b1.ToTable("MaterialSupplierCost");

                            b1.HasOne("Domain.SharedKernel.Enumerations.CurrencyType", "CurrencyType")
                                .WithMany()
                                .HasForeignKey("CurrencyTypeId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MaterialSupplierCostId");

                            b1.Navigation("CurrencyType");
                        });

                    b.Navigation("MaterialCost")
                        .IsRequired();

                    b.Navigation("Surcharge")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactPersonInformation", b =>
                {
                    b.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", null)
                        .WithOne("ContactPersonInformation")
                        .HasForeignKey("Domain.SupplyChainManagement.TransactionalPartnerAggregate.ContactPersonInformation", "TransactionalPartnerId")
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
                    b.HasOne("Domain.SharedKernel.Enumerations.CurrencyType", "CurrencyType")
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
                        .WithOne()
                        .HasForeignKey("Domain.ProductionPlanning.BoM", "ProductId");

                    b.OwnsOne("Domain.ProductionPlanning.BoMCode", "Revision", b1 =>
                        {
                            b1.Property<uint>("BoMId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("char(10)")
                                .HasColumnName("Revision");

                            b1.HasKey("BoMId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("BoM");

                            b1.WithOwner()
                                .HasForeignKey("BoMId");
                        });

                    b.Navigation("Revision")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevision", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.BoM", null)
                        .WithMany("BoMRevisions")
                        .HasForeignKey("BoMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.ProductionPlanning.BoMRevisionCode", "Revision", b1 =>
                        {
                            b1.Property<ushort>("BoMRevisionId")
                                .HasColumnType("smallint");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("char(14)")
                                .HasColumnName("Revision");

                            b1.HasKey("BoMRevisionId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("BoMRevision");

                            b1.WithOwner()
                                .HasForeignKey("BoMRevisionId");
                        });

                    b.Navigation("Revision")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.ProductionPlanning.BoMRevisionMaterial", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.BoMRevision", null)
                        .WithMany("BoMRevisionMaterials")
                        .HasForeignKey("BoMRevisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.SharedKernel.ValueObjects.MaterialCost", "MaterialCost", b1 =>
                        {
                            b1.Property<Guid>("BoMRevisionMaterialId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("MaterialId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("MaterialId");

                            b1.Property<Guid>("_transactionalPartnerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("SupplierId");

                            b1.HasKey("BoMRevisionMaterialId");

                            b1.HasIndex("MaterialId");

                            b1.HasIndex("_transactionalPartnerId");

                            b1.ToTable("BoMRevisionMaterial");

                            b1.WithOwner()
                                .HasForeignKey("BoMRevisionMaterialId");

                            b1.HasOne("Domain.SupplyChainManagement.MaterialAggregate.Material", null)
                                .WithMany()
                                .HasForeignKey("MaterialId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.HasOne("Domain.SupplyChainManagement.TransactionalPartnerAggregate.TransactionalPartner", null)
                                .WithMany()
                                .HasForeignKey("_transactionalPartnerId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsOne("Domain.SharedKernel.ValueObjects.Money", "Price", b2 =>
                                {
                                    b2.Property<Guid>("MaterialCostBoMRevisionMaterialId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<byte>("CurrencyTypeId")
                                        .HasColumnType("tinyint")
                                        .HasColumnName("CurrencyTypeId");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("Price");

                                    b2.HasKey("MaterialCostBoMRevisionMaterialId");

                                    b2.HasIndex("CurrencyTypeId");

                                    b2.ToTable("BoMRevisionMaterial");

                                    b2.HasOne("Domain.SharedKernel.Enumerations.CurrencyType", "CurrencyType")
                                        .WithMany()
                                        .HasForeignKey("CurrencyTypeId")
                                        .OnDelete(DeleteBehavior.NoAction)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("MaterialCostBoMRevisionMaterialId");

                                    b2.Navigation("CurrencyType");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.Navigation("MaterialCost")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.ProductionPlanning.Product", b =>
                {
                    b.HasOne("Domain.ProductionPlanning.BoM", null)
                        .WithOne()
                        .HasForeignKey("Domain.ProductionPlanning.Product", "BoMId");
                });

            modelBuilder.Entity("Domain.SupplyChainManagement.MaterialAggregate.Material", b =>
                {
                    b.Navigation("MaterialSupplierCosts");
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
#pragma warning restore 612, 618
        }
    }
}
